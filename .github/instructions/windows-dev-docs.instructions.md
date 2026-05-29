---
applyTo: "**"
---
# Reviewer instructions for windows-dev-docs-pr

You are reviewing PRs for Microsoft Learn content covering Windows app
development (Windows App SDK, WinUI 3, MSIX, UWP migration, packaging,
deployment, Win32 modernization). Apply these rules alongside the Microsoft
Writing Style Guide.

## 1. Terminology precision

Flag any PR that conflates these terms.

- **Windows App SDK** is the SDK; **WinUI 3** is its UI framework. Not synonyms. A Win32/WPF app can use the SDK without WinUI 3.
- **UWP** ≠ **WinUI 3**. UWP uses `Windows.UI.Xaml.*`; WinUI 3 uses `Microsoft.UI.Xaml.*`. Reject samples that mix namespaces.
- Packaging has **three** values: packaged (MSIX), packaged with external location, and unpackaged. Do not collapse the middle one.
- Deployment (framework-dependent vs self-contained) is orthogonal to packaging.
- **`WindowsAppSDKSelfContained`** (SDK runtime) ≠ .NET's **`SelfContained`** ≠ **`PublishSingleFile`**. PRs frequently swap these.
- **Single-project MSIX** ≠ **WAP**. Different project shapes; `WindowsAppSDKSelfContained` must be set in both when WAP is used.

## 2. Do not fabricate

- **Never invent MSBuild property names.** Flag unverifiable properties (common hallucinations: `WindowsAppSdkBundle`, `EnableMsixTooling` variants).
- **Never invent API names or signatures.** Ask the author to cite WinRT metadata or a Learn API reference URL.
- **Never invent version numbers.** Require a release-notes or GitHub-release citation.
- **Never invent VS template names.** Verify against the current gallery.

## 3. Code samples must be runnable and current

- C#: `Microsoft.UI.Xaml`, `DispatcherQueue`, `AppWindow` (Microsoft.UI.Windowing), not their UWP equivalents.
- C++/WinRT: same namespace migration. Flag wholesale UWP imports without conversion.
- csproj snippets must specify the language pivot; `.csproj` and `.vcxproj` use different property groups.
- Bootstrapper samples must use `Bootstrap.Initialize` / `MddBootstrapInitialize2`, not legacy variants.

## 3a. HWND initialization in WinUI 3

UWP had an implicit `CoreWindow`; WinUI 3 desktop apps do not. Several `Windows.*` APIs throw at runtime unless associated with a window handle first. Flag samples that omit this step for:

- **Pickers** (`FileOpenPicker`, `FileSavePicker`, `FolderPicker`), **`MessageDialog`**: require `WinRT.Interop.InitializeWithWindow.Initialize(obj, hWnd)` in C#, or `IInitializeWithWindow::Initialize` in C++.
- **`DataTransferManager.ShowShareUI`**, **`PrintManager.ShowPrintUIAsync`**: require the corresponding `Interop.GetForWindow` call.
- **`CameraCaptureUI`** and other picker-style APIs with UI: HWND interop variants per API.

The HWND comes from `WindowNative.GetWindowHandle(this)` (C#) or `IWindowNative::get_WindowHandle` (C++/WinRT).

**UWP → WinUI 3 API substitutions to flag:**

- `MessageDialog` → prefer `ContentDialog` (needs `XamlRoot`, not HWND interop).
- `ApplicationView.GetForCurrentView()`, `CoreWindow.GetForCurrentThread()` → use `AppWindow` (Microsoft.UI.Windowing) or the `Window` reference directly.
- `CoreApplication.MainView.CoreWindow.Dispatcher` → `DispatcherQueue.GetForCurrentThread()`.
- `ToastNotificationManager` → `AppNotificationManager` (works packaged and unpackaged).
- `ApplicationData.Current.LocalSettings/LocalFolder` → requires package identity; unpackaged samples must use a Win32 alternative or state the assumption.
- `BackgroundTaskBuilder` → requires package identity and a manifest declaration; flag if used in unpackaged context.

If a sample was adapted from UWP docs, the HWND interop step is almost certainly missing.

## 4. UWP → WinUI 3 migration content

- The .NET Upgrade Assistant is C#-only and does not migrate `ApplicationView`/`AppWindow` APIs. Name the gap; don't hide it.
- API mapping tables must be exhaustive for the surface the PR covers.

## 5. Packaging and deployment guidance

- `PublishSingleFile` does not produce a working unpackaged WinUI 3 app. Reference `WindowsAppSDKSingleFileVerifyConfiguration` where relevant.
- "How to ship" guidance must specify both axes (packaging mode and deployment mode).
- Flag stale links to retired roadmaps or pre-2024 blog posts used as primary references.

## 6. Honesty about constraints

Do not soften known limitations.

- If unsupported, say so directly. Do not write "consider exploring" when the answer is "not supported."
- If no WinUI 3 equivalent exists for a UWP control, say so and link the community alternative.
- Replace undated "coming soon" with "tracked in \<GitHub issue link\>."
- **Contentious feature framing.** When Microsoft has removed, not implemented, or significantly changed something the community wants, the framing matters enormously.
  - Don't use "replacement" for something that isn't one. Don't imply a workaround is equivalent to the missing feature. Acknowledge the gap factually and link the tracking issue.
  - Never write "simply do X", "just do Y", "it's easy to", or "all you need to do is." These phrases dismiss the reader's experience, especially when the task is genuinely complex or the situation is frustrating.
  - Before approving content that touches a known pain point, ask: "How would someone who voted for the feature request read this?" If the title, intro, or framing could be read as tone-deaf, flag it to the author with a concrete suggestion for neutral phrasing. Example: "XAML Designer replacement" when there is no designer was flagged as potentially inflammatory; "XAML runtime design tools" is neutral and accurate.

## 7. Style and structure

- Microsoft Writing Style Guide voice: second person, active, present tense, sentence-case headings.
- Use Learn note syntax (`> [!IMPORTANT]`, `> [!NOTE]`, `> [!TIP]`). Flag bold-text-as-callout substitutes.
- Update `ms.date` on substantive changes.
- Link to `/windows/apps/...`, not deprecated MSDN URLs. Flag `/uwp/api/` links from WinUI 3 content unless the API is genuinely UWP-only.
- Keep comparison-table column order consistent across sibling pages.
- Prefer commas, parentheses, or separate sentences over em dashes. Em dashes are fine when they genuinely improve readability, but overuse makes prose feel breathless. Flag excessive em-dash usage in PRs.

## 8. What to ask the author

When a PR is ambiguous, ask:

1. Which Windows App SDK version is targeted?
2. C#, C++/WinRT, or both? Samples for each?
3. Packaged, packaged-with-external-location, unpackaged, or all three?
4. Tested against the current stable channel template?
5. GitHub issue or release-notes citation for new behavior?
