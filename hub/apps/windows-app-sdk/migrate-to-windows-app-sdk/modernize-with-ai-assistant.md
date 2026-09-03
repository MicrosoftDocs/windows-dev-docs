---
title: Plan your modernization with an AI assistant
description: Plan an incremental migration for a WPF, Windows Forms, or Win32 app by asking an AI coding assistant to analyze your codebase.
author: GrantMeStrength
ms.author: jken
ms.topic: how-to
ms.date: 09/03/2026
# customer intent: As a developer with an existing Windows desktop app, I want an AI coding assistant to produce a realistic modernization plan for my codebase.
---

# Plan your modernization with an AI assistant

You can use an AI coding assistant to analyze your existing app and draft a modernization plan for your codebase. The prompt in this article directs the assistant to inspect your project, ask about constraints it can't determine from code, ground its recommendations in Microsoft Learn, and propose a first step that fits in a single pull request.

The prompt is tool-neutral. You can use it with any assistant that can read your project files and access documentation.

This article covers planning. For tool-specific migration workflows, see [Modernize or port a Windows app with GitHub Copilot](ai-modernize.md).

> [!IMPORTANT]
> AI-generated plans and code can contain errors. Treat the output as a draft, verify each recommendation against the linked documentation, review every generated change, and test your app before you ship it.

## Before you start

- Open your solution or project folder so that the assistant can read your code.
- Confirm that the assistant can open current Microsoft Learn pages through a web tool or connected documentation source.
- Be ready to describe the capability you want to add and the constraints you can't change. Constraints might include your deployment channel, minimum operating system version, control vendors, compliance requirements, or release cadence.
- Expect a conversation rather than a single answer. The prompt stops after the initial investigation and waits for your input.

## Copy the planning prompt

Copy the following prompt into your AI coding assistant's chat:

```text
Analyze this existing Windows desktop application and produce an evidence-based,
incremental modernization plan. The goal is to add modern capabilities, not to
rewrite the app. The app must build, run, and remain releasable after every
phase.

## Phase 1: Investigate and stop

In your first response, do only the following:

1. Inspect my project files and report the findings listed below.
2. Identify information that you can't determine from the project.
3. Ask all necessary questions in one batch.
4. Stop and wait for my response. Don't recommend a migration path yet.

Report the investigation as a table with Finding, Evidence in the project, and
Uncertainty columns. Cover:

- App and UI technology and version (WPF, Windows Forms, or Win32, including
  MFC), and approximately how many windows, forms, and dialogs the app contains.
- Target framework (.NET Framework version, .NET version, or C++ toolset) and,
  when applicable, whether anything blocks moving to the current .NET release.
- Third-party UI control vendors and versions.
- Interop surface, including P/Invoke declarations, COM references, ActiveX
  hosting, and native DLL dependencies. Identify dependencies that support only
  32-bit processes.
- Current packaging model (packaged with MSIX, packaged with external location,
  or unpackaged), installer or distribution channel (ClickOnce, MSI, Microsoft
  Store, Intune, or enterprise sideloading), and, when the app uses the Windows
  App SDK, deployment model (framework-dependent or self-contained).
- Architecture signals. Determine whether presentation logic is separable from
  business logic or tightly coupled to the UI technology.

Include these questions:

1. What capability am I trying to add?
2. What can I not change? Consider the deployment channel, minimum operating
   system version, control vendors, compliance requirements, and release
   cadence.

After I answer, complete phases 2 through 4.

## Phase 2: Ground your recommendations

Base your guidance on the current Windows App SDK and Windows developer
documentation at https://learn.microsoft.com/windows/apps/. Start with the
migration decision guide:
https://learn.microsoft.com/windows/apps/windows-app-sdk/migrate-to-windows-app-sdk/migration-decision-guide

Follow these grounding rules:

- Cite the specific documentation page supporting each material recommendation.
- Confirm that each returned URL and page title match the documentation you
  intended to cite. If a URL redirects to different or older content, report
  the redirect and don't treat the content as current guidance.
- Check my project explicitly against documented framework, runtime, operating
  system, packaging, and deployment prerequisites.
- Separate documented facts from judgment. If the documentation or project
  evidence doesn't support a statement, label it [ASSUMPTION]. If the
  documentation doesn't cover something, say so rather than guessing.
- Never invent APIs, MSBuild properties, package names, or NuGet identifiers.
  If you can't verify that one exists, say that you are unsure.
- Identify any preview, experimental, unsupported, or deprecated dependency.
- If a third-party dependency has no documented Windows App SDK equivalent,
  state that rather than suggesting an unverified substitute.
- Don't recommend .NET Upgrade Assistant for new migrations because it is
  deprecated. For a supported .NET project, evaluate the GitHub Copilot
  modernization agent by using current Microsoft Learn guidance.
- For a native C++ or MFC project, mark .NET-specific criteria and tooling as
  not applicable.

## Phase 3: Recommend a path

Use the exact path labels from the migration decision guide: Upgrade in place,
Modernize in place, or Move to WinUI 3.

- Recommend the one path that fits best.
- If my app needs a sequence of paths, provide the order and the criteria for
  moving from one path to the next.
- If different requested capabilities require different paths, identify that
  conflict instead of forcing one answer.
- Explain why the other paths are BLOCKED (a documented prerequisite or
  unsupported combination prevents them) or NOT RECOMMENDED (they are possible,
  but their cost or risk isn't justified).
- If the choice is close, identify the deciding factor.

## Phase 4: Produce the plan

Provide:

1. A phased plan in which the app builds, runs, and remains releasable at the
   end of every phase. For each phase, describe what changes, what it enables,
   and what could break.
2. The smallest first step that fits in one pull request and can be completed
   in one or two days. Name the files and projects to change and explain how to
   verify the result.
3. A blockers list. For each blocker, identify what is needed to resolve it,
   such as a vendor update, a missing API, or a decision that I must make.
4. A "don't do this" list of migration mistakes that are specifically likely
   in my codebase.

## Output rules

- Name the actual files, classes, projects, and dependencies in my code.
  Generic migration advice doesn't satisfy this request.
- Prefer a shorter plan supported by evidence over a longer speculative plan.
- If you can't complete a section, include its heading and state what
  information is missing. Don't invent content to fill it.
```

## Review the proposed plan

A useful response has these characteristics:

- **It names your code.** The response identifies your projects, UI types, dependencies, and interop declarations. If it refers only to "your app," confirm that the assistant can access the correct folder.
- **It asks before recommending.** Vendor support, deployment requirements, and release constraints can change the appropriate migration path.
- **It identifies evidence gaps.** The response distinguishes documented guidance from assumptions and information that needs independent verification.
- **It proposes a small first step.** Ask the assistant to reduce the scope if the first pull request would take weeks instead of days.

The following table lists common warning signs:

| What you see | What to do |
|---|---|
| A full rewrite recommendation made before the assistant asks about constraints | Ask the assistant to complete the investigation first. |
| Advice that could apply to any app | Confirm that the assistant can access your solution and is analyzing the intended projects. |
| Recommendations without citations | Treat them as unverified and ask for the supporting Microsoft Learn pages. |
| An API, MSBuild property, or package that you can't find | Ask the assistant to verify that it exists in current reference documentation. |
| No blockers for a complex legacy app | Ask specifically about the oldest dependencies, interop boundaries, packaging, and deployment constraints. |

## Report documentation gaps

If the assistant identifies a question that Microsoft Learn doesn't answer, use the **Feedback** section on the relevant page to report it. Include the question you were trying to answer and enough information about your app model to explain the gap, but don't include confidential code or data.

## Related content

- [Choose your migration path](migration-decision-guide.md)
- [Migration and modernization overview](overall-migration-strategy.md)
- [Modernize or port a Windows app with GitHub Copilot](ai-modernize.md)
- [Migrate WPF app patterns to WinUI 3](wpf-patterns-winui3.md)
- [Windows Forms patterns and their WinUI 3 equivalents](../../get-started/line-of-business/migrate-winforms-patterns.md)
