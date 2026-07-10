#!/usr/bin/env python3
"""
validate-csharp-snippets.py — Compile C# code snippets from hub/ markdown files
against Windows App SDK / WinUI 3 to catch namespace errors, bad API names, and
missing using statements.

Usage:
    # Check all hub/ markdown files
    python tools/validate-csharp-snippets.py

    # Check specific files
    python tools/validate-csharp-snippets.py --files hub/apps/develop/foo.md

    # Only check files changed vs origin/main (for CI on PRs)
    python tools/validate-csharp-snippets.py --changed-only

    # Emit GitHub Actions annotations
    python tools/validate-csharp-snippets.py --changed-only --output-format github
"""

import os
import re
import sys
import json
import shutil
import argparse
import subprocess
from pathlib import Path
from dataclasses import dataclass

REPO_ROOT = Path(__file__).resolve().parent.parent
HUB_DIR = REPO_ROOT / "hub"
HARNESS_DIR = Path(__file__).resolve().parent / "snippet-harness"
GENERATED_DIR = HARNESS_DIR / "Generated"

# Directories that intentionally contain UWP "before" code (migration guides,
# API reference stubs, C++/CX migration articles). Files under these paths are
# excluded from --scan-only analysis to suppress false positives.
_SCAN_EXCLUDED_DIRS: list[str] = [
    "hub/apps/api-reference/",
    "hub/apps/windows-app-sdk/migrate-to-windows-app-sdk/",
    "hub/apps/develop/cpp-winrt/",
]


def _is_scan_excluded(path: Path) -> bool:
    """Return True if the file is under a directory intentionally skipped by --scan-only."""
    try:
        rel = str(path.resolve().relative_to(REPO_ROOT)).replace("\\", "/")
    except ValueError:
        return False
    return any(rel.startswith(excl) for excl in _SCAN_EXCLUDED_DIRS)

# Errors that indicate a real problem (not just a scaffolding artefact)
REAL_ERROR_CODES = {
    "CS0246",  # type/namespace not found
    "CS0103",  # name not in current context
    "CS1061",  # X does not contain definition for Y
    "CS0117",  # X does not contain definition for Y (static)
    "CS0426",  # type name not found in type
    "CS0234",  # namespace not found
    "CS0535",  # does not implement interface member
    "CS1503",  # argument type mismatch
    "CS0029",  # cannot implicitly convert
    "CS0161",  # not all code paths return a value (genuine logic error)
    "CS1002",  # ; expected (syntax)
    "CS1003",  # syntax error
    "CS0019",  # operator cannot be applied
}

# Suppress noisy warnings that are expected in fragment code
SUPPRESSED_CODES = {
    "CS0168",  # variable declared but never used
    "CS0169",  # field never used
    "CS0649",  # field never assigned to
    "CS8600",  # converting null literal
    "CS8602",  # dereference of possibly null
    "CS8603",  # possible null reference return
    "CS8618",  # non-nullable not initialized
    "CS0414",  # field assigned but value never used
    "CS1998",  # async method lacks await
    "CS0219",  # variable assigned but value never used
    "CS0108",  # hides inherited member (use new keyword)
    "CS8625",  # cannot convert null literal to non-nullable
    "CS0067",  # event never used
}


@dataclass
class Snippet:
    file: Path
    line: int      # 1-based line of opening fence in the markdown file
    index: int     # global index across all files
    code: str


# ---------------------------------------------------------------------------
# Extraction
# ---------------------------------------------------------------------------

def extract_snippets(md_file: Path) -> list[Snippet]:
    """Return all ```csharp fenced blocks from a markdown file."""
    snippets = []
    try:
        text = md_file.read_text(encoding="utf-8", errors="replace")
    except OSError:
        return snippets

    lines = text.splitlines()
    in_block = False
    block_start = 0
    block_lines: list[str] = []

    for i, line in enumerate(lines):
        if not in_block:
            if re.match(r"^```\s*(csharp|cs)\b", line, re.IGNORECASE):
                in_block = True
                block_start = i + 1  # 1-based
                block_lines = []
        else:
            if re.match(r"^```\s*$", line):
                code = "\n".join(block_lines)
                if code.strip():
                    snippets.append(Snippet(md_file, block_start, 0, code))
                in_block = False
                block_lines = []
            else:
                block_lines.append(line)

    return snippets


# ---------------------------------------------------------------------------
# Scaffold
# ---------------------------------------------------------------------------

_LEVEL_TYPE = 1    # snippet is a complete type (class / struct / enum)
_LEVEL_MEMBER = 2  # snippet contains member declarations (methods, properties)
_LEVEL_BODY = 3    # snippet is a sequence of statements (method body)


def _strip_using_directives(code: str) -> tuple[str, list[str]]:
    """
    Remove namespace `using X;` lines from the top of a snippet.
    Returns (cleaned_code, list_of_using_lines).
    These are handled globally by GlobalUsings.cs and are invalid inside method bodies.
    """
    lines = code.splitlines()
    usings: list[str] = []
    rest: list[str] = []
    past_usings = False
    for line in lines:
        if not past_usings and re.match(r"^\s*using\s+[\w.]+;", line):
            usings.append(line)
        else:
            if line.strip():
                past_usings = True
            rest.append(line)
    return "\n".join(rest), usings


def _detect_level(code: str) -> int:
    code, _ = _strip_using_directives(code)
    stripped = code.strip()
    # Skip leading blank/comment lines so a leading "// ..." comment doesn't
    # mask an access modifier on the next real line of code.
    first_line = ""
    for line in stripped.splitlines():
        candidate = line.strip()
        if not candidate or candidate.startswith("//"):
            continue
        first_line = line
        break

    # Complete type: has class/struct/enum keyword followed eventually by {
    if re.search(
        r"\b(class|struct|enum|interface|record)\b.+\{",
        stripped,
        re.DOTALL | re.IGNORECASE,
    ):
        return _LEVEL_TYPE

    # Member-level: starts with an access modifier or attribute, and contains a
    # recognisable member shape (method sig, property, field)
    if re.match(
        r"\s*(public|private|protected|internal|static|override|virtual|async|readonly|const|\[)",
        first_line,
    ):
        # A method/property body uses { } at depth 1 — good heuristic
        if re.search(r"\)\s*\{", stripped) or re.search(r"\}\s*$", stripped):
            return _LEVEL_MEMBER
        # Attribute above a declaration
        if re.match(r"\s*\[", first_line):
            return _LEVEL_MEMBER
        # Bare field/property declarations (e.g. "private Foo bar;") have no
        # braces at all, or only balanced ones — they can't legally appear as
        # statements inside a method body, so treat them as members instead.
        if stripped.count("{") == stripped.count("}") and stripped.rstrip().endswith(";"):
            return _LEVEL_MEMBER

    return _LEVEL_BODY


def _pragma_header() -> str:
    codes = ",".join(sorted(SUPPRESSED_CODES))
    return f"#pragma warning disable {codes}"


# Narrative snippets sometimes reference a field/method that is declared in a
# *different* code fence within the same source article (e.g. an event
# handler wired up in one snippet and defined in a later one). Since each
# fence compiles as its own isolated class, add a placeholder only when the
# name is referenced but not already declared by this particular snippet
# (otherwise we'd get a duplicate-definition error in the fence that already
# declares it). Each tuple is (usage_regex, own_declaration_regex, member_code).
_CONDITIONAL_MEMBERS: list[tuple[str, str, str]] = [
    (r"\bspeechRecognizer\b", r"\bSpeechRecognizer\s+speechRecognizer\b",
     "    private global::Windows.Media.SpeechRecognition.SpeechRecognizer speechRecognizer = null!;"),
    (r"\bdispatcherQueue\b", r"\bDispatcherQueue\s+dispatcherQueue\b",
     "    private global::Microsoft.UI.Dispatching.DispatcherQueue dispatcherQueue = null!;"),
    (r"\bresultTextBlock\b", r"\bTextBlock\s+resultTextBlock\b",
     "    private global::Microsoft.UI.Xaml.Controls.TextBlock resultTextBlock = null!;"),
    (r"\bhapticsController\b", r"\bSimpleHapticsController\s+hapticsController\b",
     "    private global::Windows.Devices.Haptics.SimpleHapticsController hapticsController = null!;"),
    (r"\bcurrentWaveform\b", r"\bSimpleHapticsControllerFeedback\s+currentWaveform\b",
     "    private global::Windows.Devices.Haptics.SimpleHapticsControllerFeedback currentWaveform = null!;"),
    (r"\bdictatedTextBuilder\b", r"\bStringBuilder\s+dictatedTextBuilder\b",
     "    private global::System.Text.StringBuilder dictatedTextBuilder = null!;"),
    (r"\bdictationTextBox\b", r"\bTextBox\s+dictationTextBox\b",
     "    private global::Microsoft.UI.Xaml.Controls.TextBox dictationTextBox = null!;"),
    (r"\bbtnClearText\b", r"\bButton\s+btnClearText\b",
     "    private global::Microsoft.UI.Xaml.Controls.Button btnClearText = null!;"),
    (r"\bstatusTextBlock\b", r"\bTextBlock\s+statusTextBlock\b",
     "    private global::Microsoft.UI.Xaml.Controls.TextBlock statusTextBlock = null!;"),
    (r"\bContinuousRecognitionSession_ResultGenerated\b",
     r"\bvoid\s+ContinuousRecognitionSession_ResultGenerated\s*\(",
     "    private void ContinuousRecognitionSession_ResultGenerated("
     "global::Windows.Media.SpeechRecognition.SpeechContinuousRecognitionSession sender, "
     "global::Windows.Media.SpeechRecognition.SpeechContinuousRecognitionResultGeneratedEventArgs args) { }"),
    (r"\bContinuousRecognitionSession_Completed\b",
     r"\bvoid\s+ContinuousRecognitionSession_Completed\s*\(",
     "    private void ContinuousRecognitionSession_Completed("
     "global::Windows.Media.SpeechRecognition.SpeechContinuousRecognitionSession sender, "
     "global::Windows.Media.SpeechRecognition.SpeechContinuousRecognitionCompletedEventArgs args) { }"),
    (r"\bSpeechRecognizer_HypothesisGenerated\b",
     r"\bvoid\s+SpeechRecognizer_HypothesisGenerated\s*\(",
     "    private void SpeechRecognizer_HypothesisGenerated("
     "global::Windows.Media.SpeechRecognition.SpeechRecognizer sender, "
     "global::Windows.Media.SpeechRecognition.SpeechRecognitionHypothesisGeneratedEventArgs args) { }"),
    (r"\bSpeechRecognizer_RecognitionQualityDegrading\b",
     r"\bvoid\s+SpeechRecognizer_RecognitionQualityDegrading\s*\(",
     "    private void SpeechRecognizer_RecognitionQualityDegrading("
     "global::Windows.Media.SpeechRecognition.SpeechRecognizer sender, "
     "global::Windows.Media.SpeechRecognition.SpeechRecognitionQualityDegradingEventArgs args) { }"),
]


def _conditional_members(code: str) -> str:
    """Return extra placeholder members needed by this snippet (see above)."""
    extra = [
        member
        for usage_re, decl_re, member in _CONDITIONAL_MEMBERS
        if re.search(usage_re, code) and not re.search(decl_re, code)
    ]
    return "\n".join(extra)


def scaffold(snippet: Snippet) -> str:
    """Wrap the snippet in enough C# scaffolding to allow compilation."""
    code, _usings = _strip_using_directives(snippet.code)
    level = _detect_level(snippet.code)  # detect on original (including usings)
    rel = snippet.file.resolve().relative_to(REPO_ROOT)
    src = f"// Source: {rel}:{snippet.line}"
    n = snippet.index
    pragma = _pragma_header()

    if level == _LEVEL_TYPE:
        return f"""// <auto-generated/>
{src}
{pragma}
namespace SnippetNS_{n};
{code}
"""

    # For member and body levels we wrap inside a Window subclass so that
    # DispatcherQueue, XamlRoot, etc. are in scope via inheritance.
    base_members = """
    // Placeholders so snippets that reference common variables compile.
    private static global::Microsoft.UI.Xaml.Window App_Window => null!;
    private global::Microsoft.UI.Xaml.Controls.Button button = null!;
    private global::Microsoft.UI.Xaml.Controls.TextBox textBox = null!;
    private global::Microsoft.UI.Xaml.Controls.ListView listView = null!;
    private global::Microsoft.UI.Xaml.Controls.Frame rootFrame = null!;
    private global::Microsoft.UI.Xaml.Controls.Canvas canvas = null!;
""" + _conditional_members(code)

    if level == _LEVEL_MEMBER:
        return f"""// <auto-generated/>
{src}
{pragma}
#nullable enable
namespace SnippetNS_{n};

partial class Snippet_{n} : global::Microsoft.UI.Xaml.Window
{{
{base_members}
{code}
}}
"""

    # _LEVEL_BODY: wrap statements in an async method
    indented = "\n".join("        " + l for l in code.splitlines())
    return f"""// <auto-generated/>
{src}
{pragma}
#nullable enable
namespace SnippetNS_{n};

partial class Snippet_{n} : global::Microsoft.UI.Xaml.Window
{{
{base_members}
    private async System.Threading.Tasks.Task RunSnippet()
    {{
{indented}
    }}
}}
"""


# ---------------------------------------------------------------------------
# Harness build
# ---------------------------------------------------------------------------

def generate_files(snippets: list[Snippet]) -> dict[str, Snippet]:
    """Write generated .cs files; return {normalised_path: snippet}."""
    if GENERATED_DIR.exists():
        shutil.rmtree(GENERATED_DIR)
    GENERATED_DIR.mkdir(parents=True)

    mapping: dict[str, Snippet] = {}
    for s in snippets:
        cs_file = GENERATED_DIR / f"Snippet_{s.index:04d}.cs"
        cs_file.write_text(scaffold(s), encoding="utf-8")
        mapping[str(cs_file.resolve()).lower()] = s

    # Keep the directory around even when empty (so dotnet build succeeds)
    (GENERATED_DIR / ".gitkeep").write_text("")
    return mapping


def build_harness() -> tuple[str, int]:
    result = subprocess.run(
        [
            "dotnet", "build", str(HARNESS_DIR),
            "--nologo", "-v", "q",
            "/p:GeneratePackageOnBuild=false",
        ],
        capture_output=True,
        text=True,
    )
    return result.stdout + result.stderr, result.returncode


def parse_errors(output: str, mapping: dict[str, Snippet]) -> list[dict]:
    errors = []
    # MSBuild diagnostic format:  path(line,col): severity CSXXXX: message
    pattern = re.compile(
        r"(.+?)\((\d+),\d+\):\s+(error|warning)\s+(CS\d+):\s+(.+)"
    )
    for line in output.splitlines():
        m = pattern.match(line.strip())
        if not m:
            continue
        raw_path = m.group(1).strip()
        code = m.group(4)
        severity = m.group(3)

        # Only report codes we care about
        if code in SUPPRESSED_CODES:
            continue
        if severity == "warning" and code not in REAL_ERROR_CODES:
            continue

        norm = str(Path(raw_path).resolve()).lower()
        snippet = mapping.get(norm)
        if not snippet:
            continue

        errors.append(
            {
                "source_file": str(snippet.file.relative_to(REPO_ROOT)),
                "source_line": snippet.line,
                "severity": severity,
                "code": code,
                "message": m.group(5).strip(),
            }
        )
    return errors


# ---------------------------------------------------------------------------
# Static scan (--scan-only) — no dotnet required, works on any platform
# ---------------------------------------------------------------------------

# Each rule: (id, severity, regex_pattern, message, false_positive_note)
_SCAN_RULES: list[tuple[str, str, str, str, str]] = [
    (
        "uwp-xaml-ns",
        "error",
        r"\bWindows\.UI\.Xaml\b",
        "UWP namespace Windows.UI.Xaml — use Microsoft.UI.Xaml in WinUI 3",
        "Allowed: Windows.UI.Text, Windows.UI.ViewManagement (not XAML)",
    ),
    (
        "window-current",
        "error",
        r"\bWindow\.Current\b",
        "Window.Current is UWP/CoreWindow — store the Window instance explicitly in WinUI 3",
        "",
    ),
    (
        "get-for-current-view",
        "warning",
        r"\.GetForCurrentView\(\)",
        "GetForCurrentView() is CoreWindow-dependent — verify WinUI 3 equivalent (may need HWND interop)",
        "False positive: ConnectedAnimationService.GetForCurrentView() works under Microsoft.UI.Xaml.Media.Animation",
    ),
    (
        "core-application-mainview",
        "error",
        r"\bCoreApplication\.MainView\b",
        "CoreApplication.MainView is UWP-only — use DispatcherQueue.GetForCurrentThread() for dispatch",
        "",
    ),
    (
        "core-window",
        "warning",
        r"\bCoreWindow\b",
        "CoreWindow is UWP-only — WinUI 3 desktop apps use HWND-based windowing",
        "False positive: mentions in comments or migration-guide 'before' examples",
    ),
    (
        "core-dispatcher",
        "warning",
        r"\bCoreDispatcher\b",
        "CoreDispatcher is UWP-only — use DispatcherQueue in WinUI 3",
        "",
    ),
    (
        "application-view",
        "error",
        r"\bApplicationView\.GetForCurrentView\b",
        "ApplicationView.GetForCurrentView() is UWP-only — use AppWindow (Microsoft.UI.Windowing) in WinUI 3",
        "",
    ),
    (
        "toast-manager",
        "warning",
        r"\bToastNotificationManager\.CreateToastNotifier\b",
        "ToastNotificationManager is the UWP notifications API — prefer AppNotificationManager (Windows App SDK)",
        "",
    ),
    (
        "message-dialog",
        "warning",
        r"\bnew\s+MessageDialog\b",
        "MessageDialog requires HWND interop in WinUI 3 — prefer ContentDialog (needs XamlRoot)",
        "",
    ),
    (
        "core-app-create-view",
        "error",
        r"\bCoreApplication\.CreateNewView\b",
        "CoreApplication.CreateNewView is UWP-only — use AppWindow.Create() in WinUI 3",
        "",
    ),
    (
        "uwp-media-ns",
        "warning",
        r"\bWindows\.UI\.Composition\b",
        "Windows.UI.Composition is the UWP composition namespace — in WinUI 3 prefer Microsoft.UI.Composition",
        "False positive: accessing via interop from a Win32 HWND is sometimes intentional",
    ),
    (
        "on-suspending",
        "warning",
        r"\bApplication\.Suspending\b|\bOnSuspending\b",
        "Application.Suspending / OnSuspending does not fire for WinUI 3 desktop apps — use Window.Closed or process exit handling",
        "",
    ),
]

# Regexes compiled once
_COMPILED_RULES = [
    (rule_id, sev, re.compile(pat), msg, note)
    for rule_id, sev, pat, msg, note in _SCAN_RULES
]


def _relative_or_absolute(path: Path) -> Path:
    """Return path relative to REPO_ROOT if possible, else return absolute path."""
    try:
        return path.resolve().relative_to(REPO_ROOT)
    except ValueError:
        return path.resolve()


def _is_in_line_comment(code: str, match_start: int) -> bool:
    """Return True if match_start falls after a // comment marker on the same line.

    Skips // sequences that appear inside string literals (e.g. "http://") so
    that URLs in string arguments don't suppress real rule matches that follow.
    """
    line_start = code.rfind("\n", 0, match_start) + 1
    line_before = code[line_start:match_start]
    # Walk the text before the match; track whether we're inside a string literal.
    in_string = False
    i = 0
    while i < len(line_before):
        ch = line_before[i]
        if ch == '\\' and in_string:
            i += 2  # skip escape sequence inside string
            continue
        if ch == '"':
            in_string = not in_string
        elif not in_string and line_before[i:i+2] == "//":
            return True
        i += 1
    return False


def _is_in_string_literal(code: str, match_start: int) -> bool:
    """Return True if match_start is inside a double-quoted string literal.

    This is a conservative check: we look at the text before the match on
    the current line and count unescaped double-quote characters.  An odd
    count means we're inside an open string.  This correctly handles common
    cases like ApiInformation.IsTypePresent("Windows.UI.Xaml...") where the
    namespace name is a string argument, not actual API usage.
    """
    line_start = code.rfind("\n", 0, match_start) + 1
    text_before = code[line_start:match_start]
    # Count unescaped double-quotes
    count = 0
    i = 0
    while i < len(text_before):
        if text_before[i] == '\\':
            i += 2  # skip escaped character
            continue
        if text_before[i] == '"':
            count += 1
        i += 1
    return count % 2 == 1  # inside a string if odd number of quotes precede the match


def static_scan(snippets: list[Snippet]) -> list[dict]:
    """
    Apply regex-based rules to snippets without running dotnet.
    Returns list of finding dicts compatible with the compiler-error format.
    """
    findings: list[dict] = []
    for snippet in snippets:
        for rule_id, sev, pattern, msg, _note in _COMPILED_RULES:
            for match in pattern.finditer(snippet.code):
                if _is_in_line_comment(snippet.code, match.start()):
                    continue  # Skip matches inside // line comments
                if _is_in_string_literal(snippet.code, match.start()):
                    continue  # Skip matches inside string literals (e.g. ApiInformation type-name args)
                # Find which line within the snippet the match is on, then
                # map to the source markdown line number.
                snippet_line = snippet.code[: match.start()].count("\n")
                findings.append(
                    {
                        "source_file": str(_relative_or_absolute(snippet.file)),
                        "source_line": snippet.line + snippet_line,
                        "severity": sev,
                        "code": f"UWP-{rule_id}",
                        "message": msg,
                        "match": match.group(0),
                    }
                )
    return findings


# ---------------------------------------------------------------------------
# main
# ---------------------------------------------------------------------------

def collect_files(args) -> list[Path]:
    if args.files:
        candidates = [Path(f).resolve() for f in args.files]
    elif args.changed_only:
        result = subprocess.run(
            ["git", "diff", "--name-only", "origin/main...HEAD"],
            capture_output=True,
            text=True,
            cwd=REPO_ROOT,
        )
        candidates = [
            REPO_ROOT / f
            for f in result.stdout.splitlines()
            if f.startswith("hub/") and f.endswith(".md")
        ]
    else:
        candidates = sorted(HUB_DIR.rglob("*.md"))
    # Apply exclusion list in all modes so migration/UWP-before directories
    # don't produce false positives regardless of how the script is invoked.
    return [f for f in candidates if not _is_scan_excluded(f)]


def main():
    parser = argparse.ArgumentParser(
        description="Validate C# snippets in hub/ markdown files by compiling them "
                    "against Windows App SDK / WinUI 3."
    )
    parser.add_argument("--files", nargs="+", help="Specific markdown files to check")
    parser.add_argument(
        "--changed-only",
        action="store_true",
        help="Only check files changed vs origin/main (suitable for CI)",
    )
    parser.add_argument(
        "--output-format",
        choices=["text", "json", "github"],
        default="text",
        help="Output format: text (default), json, or github (Actions annotations)",
    )
    parser.add_argument(
        "--scan-only",
        action="store_true",
        help="Static regex scan only — no dotnet required. Flags known UWP-only patterns.",
    )
    args = parser.parse_args()

    files = collect_files(args)
    if not files:
        print("No markdown files to check.")
        sys.exit(0)

    print(f"Scanning {len(files)} file(s) for C# snippets...", flush=True, file=sys.stderr)

    all_snippets: list[Snippet] = []
    for f in files:
        for s in extract_snippets(f):
            s.index = len(all_snippets)
            all_snippets.append(s)

    if not all_snippets:
        print("No C# snippets found.", file=sys.stderr)
        sys.exit(0)

    print(f"Found {len(all_snippets)} snippet(s).", flush=True, file=sys.stderr)

    if not args.scan_only:
        print("Generating harness...", flush=True, file=sys.stderr)
        mapping = generate_files(all_snippets)
        print("Compiling...", flush=True, file=sys.stderr)
        output, returncode = build_harness()
        errors = parse_errors(output, mapping)
        # If the build failed but we got no parseable diagnostics, that means
        # the failure was at the MSBuild/SDK/restore level rather than a C#
        # compile error — e.g. missing workload, SDK not found, restore failure.
        # Treat this as a hard infrastructure error so CI doesn't silently pass.
        if returncode != 0 and not errors:
            print(
                f"ERROR: dotnet build exited with code {returncode} but produced "
                "no parseable diagnostics. This is likely a build-infrastructure "
                "failure (missing SDK, NuGet restore error, etc.).\n"
                "Build output:\n" + output,
                file=sys.stderr,
            )
            sys.exit(2)
    else:
        errors = static_scan(all_snippets)

    if args.output_format == "json":
        print(json.dumps(errors, indent=2))
    elif args.output_format == "github":
        for e in errors:
            level = "error" if e["severity"] == "error" else "warning"
            print(
                f"::{level} file={e['source_file']},line={e['source_line']}"
                f"::{e['code']}: {e['message']}"
            )
    else:
        if not errors:
            print(f"\n✅  All {len(all_snippets)} snippet(s) passed.")
        else:
            grouped: dict[str, list] = {}
            for e in errors:
                grouped.setdefault(e["source_file"], []).append(e)
            for src, errs in sorted(grouped.items()):
                print(f"\n{src}")
                for e in errs:
                    icon = "❌" if e["severity"] == "error" else "⚠️ "
                    match_str = f" (matched: {e['match']!r})" if "match" in e else ""
                    print(f"  {icon} line {e['source_line']:4d}  [{e['code']}] {e['message']}{match_str}")
            total_err = sum(1 for e in errors if e["severity"] == "error")
            total_warn = sum(1 for e in errors if e["severity"] == "warning")
            print(f"\n{total_err} error(s), {total_warn} warning(s) across {len(grouped)} file(s).")

    sys.exit(1 if any(e["severity"] == "error" for e in errors) else 0)


if __name__ == "__main__":
    main()
