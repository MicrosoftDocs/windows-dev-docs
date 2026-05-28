---
applyTo: "**"
---
# Reviewer instructions for windows-dev-docs-pr

You are reviewing pull requests for Microsoft Learn content covering Windows app
development — Windows App SDK, WinUI 3, MSIX, UWP migration, packaging,
deployment, and Win32 modernization. Apply these rules in addition to the
Microsoft Writing Style Guide.

## 1. Terminology precision (most common error in this repo)

These terms are routinely conflated. Flag any PR that uses them interchangeably.

- **Windows App SDK** is the SDK. **WinUI 3** is the UI framework it ships.
  They are not synonyms. "Windows App SDK app" ≠ "WinUI 3 app" — a Win32 or
  WPF app can consume the Windows App SDK without using WinUI 3.
- **UWP** and **WinUI 3** are distinct. UWP uses `Windows.UI.Xaml.*`; WinUI 3
  uses `Microsoft.UI.Xaml.*`. Code samples must not mix namespaces. Reject
  samples imported from UWP docs without namespace conversion.
- The packaging axis has **three** values, not two: **packaged (MSIX)**,
  **packaged with external location**, and **unpackaged**. Do not collapse
  "packaged with external location" into either neighbor — it has its own
  identity model.
- The deployment axis is **framework-dependent** vs **self-contained**. It is
  orthogonal to the packaging axis. Any sentence that treats them as the same
  choice is wrong.
- **`WindowsAppSDKSelfContained`** (MSBuild property, Windows App SDK runtime)
  is not the same as .NET's **`SelfContained`** (publish property, .NET
  runtime) and neither is **`PublishSingleFile`**. PRs frequently swap these.
- **Single-project MSIX** ≠ **Windows Application Packaging Project (WAP)**.
  They produce equivalent outputs but have different csproj/wapproj shapes,
  and `WindowsAppSDKSelfContained` must be set in both projects when WAP is used.

## 2. Do not fabricate

- **Never invent csproj/MSBuild property names.** If a property is not in
  the Windows App SDK source or a current Learn page, flag it. Common
  hallucinations to watch for: `WindowsAppSdkBundle`, `EnableMsixTooling`
  variants, made-up `Self` prefixes.
- **Never invent API namespaces, classes, or method signatures.** If unsure,
  ask the author to cite the WinRT metadata source or a Learn API reference URL.
- **Never invent version numbers** ("starting in Windows App SDK 1.4…") unless
  the PR cites the release notes or GitHub release. Stale or invented version
  pivots are a recurring source of incorrect "is X supported?" answers.
- **Never invent template names.** Visual Studio template names have changed
  (e.g., "Blank App, Packaged (WinUI 3 in Desktop)" → "WinUI Blank App
  (Packaged)"). Verify against the current VS gallery before approving.

## 3. Code samples must be runnable and current

- C# samples: `using Microsoft.UI.Xaml;` not `Windows.UI.Xaml;`.
  `DispatcherQueue`, not `CoreDispatcher`. `AppWindow` (Microsoft.UI.Windowing),
  not UWP `ApplicationView`.
- C++/WinRT samples: the same namespace migration applies. Flag any sample
  imported wholesale from UWP C++/WinRT docs without conversion — this is a
  documented top-3 community complaint.
- csproj snippets must specify the language pivot (`C#`/`C++`) above the code
  block. `.csproj` and `.vcxproj` use different property groups; cross-mixing
  is a frequent error.
- Bootstrapper API samples must use the current API surface
  (`Bootstrap.Initialize` / `MddBootstrapInitialize2`), not legacy variants.

## 3a. WinRT APIs that need HWND initialization in WinUI 3 (the FilePicker trap)

Many WinRT APIs live in `Windows.*` namespaces unchanged from UWP, so the
namespace itself is not a red flag. The breaking difference is that UWP had an
implicit `CoreWindow` context; WinUI 3 desktop apps do not. These APIs must be
associated with a window handle (HWND) before they are shown or invoked, or
they throw at runtime — typically `COMException` 0x80070005 or "The application
called an interface that was marshalled for a different thread."

Flag any sample that uses the following APIs without window-handle initialization:

| API | Required initialization (C#) | Required initialization (C++) |
|---|---|---|
| `FileOpenPicker`, `FileSavePicker`, `FolderPicker` (`Windows.Storage.Pickers`) | `WinRT.Interop.InitializeWithWindow.Initialize(picker, hWnd)` | `IInitializeWithWindow::Initialize` |
| `MessageDialog` (`Windows.UI.Popups`) | `WinRT.Interop.InitializeWithWindow.Initialize(dialog, hWnd)` | `IInitializeWithWindow::Initialize` |
| `DataTransferManager.ShowShareUI` (`Windows.ApplicationModel.DataTransfer`) | `IDataTransferManagerInterop.GetForWindow(hWnd, ...)` | `IDataTransferManagerInterop::GetForWindow` |
| `PrintManager.ShowPrintUIAsync` (`Windows.Graphics.Printing`) | `IPrintManagerInterop.GetForWindow(hWnd, ...)` | `IPrintManagerInterop::GetForWindow` |
| `Launcher.LaunchFolderAsync` / picker-style launches with UI | HWND interop via `IInitializeWithWindow` where supported | Same |
| `PickerLockedFolder`, `CameraCaptureUI` | HWND interop variants per API | Same |
| `FolderPicker` with `PickFolderAsync` *(WinUI 3 desktop)* | Same as above | Same |

The HWND comes from `WinRT.Interop.WindowNative.GetWindowHandle(this)` on a
`Microsoft.UI.Xaml.Window` (C#), or from `this->m_inner.as<IWindowNative>()
->get_WindowHandle(&hWnd)` (C++/WinRT).

Additional WinUI-3-specific substitutions to flag:

- **`MessageDialog` → prefer `ContentDialog`.** `ContentDialog` is the WinUI 3
  recommendation and only needs `XamlRoot` set (not HWND interop). Samples that
  port `MessageDialog` from UWP without considering `ContentDialog` should be
  questioned.
- **`ApplicationView.GetForCurrentView()` does not work in WinUI 3 desktop
  apps.** Use `AppWindow` (`Microsoft.UI.Windowing`) — get it from the
  `WindowId` via `Win32Interop.GetWindowIdFromWindow(hWnd)`.
- **`CoreWindow.GetForCurrentThread()` does not work** in WinUI 3 desktop apps.
  Use the `Microsoft.UI.Xaml.Window` reference directly.
- **`CoreApplication.MainView.CoreWindow.Dispatcher`** — replace with
  `DispatcherQueue.GetForCurrentThread()` or `this.DispatcherQueue` on the
  Window/Page.
- **`ToastNotificationManager` (UWP)** — in WinUI 3, prefer
  `AppNotificationManager` (Windows App SDK), which works for both packaged
  and unpackaged apps.
- **`ApplicationData.Current.LocalSettings` / `LocalFolder`** — these require
  package identity. They throw in unpackaged WinUI 3 apps. Samples in
  unpackaged-app context must use a Win32 alternative
  (`Environment.SpecialFolder.LocalApplicationData`, registry, etc.) or call
  out that the sample assumes packaged identity.
- **`BackgroundTaskBuilder` (UWP background tasks)** — requires package
  identity and a manifest declaration. Flag samples that imply unpackaged
  WinUI 3 apps can register background tasks this way.

When in doubt, ask the author: *"Has this sample been compiled and run as a
WinUI 3 desktop app (both packaged and unpackaged) on the current stable
Windows App SDK?"* If the answer is "I adapted it from a UWP sample," the
HWND interop step is almost certainly missing.

## 4. UWP → WinUI 3 migration content

- The .NET Upgrade Assistant is C#-only and explicitly does not migrate
  `ApplicationView` or `AppWindow`-related APIs. Any PR that implies otherwise
  is wrong. Migration guidance should name the gap, not paper over it.
- API mapping tables (`CoreDispatcher` → `DispatcherQueue`, `CoreWindow` →
  `AppWindow`, `Windows.UI.Xaml.Window` → `Microsoft.UI.Xaml.Window`, picker
  pattern changes, etc.) must be exhaustive for the surface the PR covers.

## 5. Packaging and deployment guidance

- Do not state or imply that `PublishSingleFile` produces a working unpackaged
  WinUI 3 app — it does not, and the `WindowsAppSDKSingleFileVerifyConfiguration`
  MSBuild target now warns about this. Reference the warning by name where
  relevant.
- Any "how to ship" guidance must specify both axes (packaging mode AND
  deployment mode). "Self-contained" alone is ambiguous.
- Cross-reference the current deployment overview, the unpackaged distribution
  page, and the self-contained deployment guide. Flag stale links to retired
  ProductBoard roadmaps or pre-2024 community blog posts as primary references.

## 6. Honesty about constraints

Microsoft engineers and community contributors have repeatedly praised candor
in this content. Do not soften known limitations.

- If a feature is not supported, say so directly. Do not write "consider
  exploring" when the answer is "not supported in this version."
- If there is no WinUI 3 equivalent for a UWP control (e.g., `MapControl`,
  Toolkit `DataGrid`), say so and link the community alternative — do not
  imply parity that doesn't exist.
- "Coming soon" without a dated commitment should be flagged. Use
  "tracked in <GitHub issue link>" instead.

## 7. Style and structure

- Apply Microsoft Writing Style Guide voice (second person, active, present
  tense, sentence-case headings).
- Use Learn note syntax (`> [!IMPORTANT]`, `> [!NOTE]`, `> [!TIP]`) — flag
  bold-text-as-callout substitutes.
- Update `ms.date` on substantive content changes (not just typo fixes).
- Internal links should target `/en-us/windows/apps/...` not deprecated MSDN
  URLs. Flag any link to `/uwp/api/` from WinUI 3 reference content unless
  the API is genuinely UWP-only.
- Tables comparing options should have stable column order across the doc
  set (packaging mode | deployment mode | runtime install | single-file |
  Store-eligible | identity). Inconsistent column order across sibling pages
  is a usability bug.

## 8. What to ask the author

When a PR is ambiguous, prefer asking these questions over guessing:

1. Which Windows App SDK version is this content targeting?
2. Does this apply to C#, C++/WinRT, or both? If both, are samples provided
   for each?
3. Does this apply to packaged, packaged-with-external-location, unpackaged,
   or all three?
4. Is the sample tested against the current stable channel template?
5. Is there a GitHub issue or release-notes entry to cite for any new
   behavior or limitation?
