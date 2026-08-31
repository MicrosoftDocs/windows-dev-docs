---
title: "AI-assisted WinUI tutorial - Verify and refine"
description: Catch framework mistakes in AI-generated Windows code, verify each API against Microsoft Learn, and test the finished WinUI 3 app.
author: GrantMeStrength
ms.author: jken
ms.date: 08/31/2026
ms.topic: tutorial
---

# Catch an AI mistake and verify the app

Generated code is ready for review, not automatically ready to ship. In this final step, you practice detecting a plausible framework mistake and verify Task Tally beyond compilation.

## Deliberately test the assistant

Start a new conversation with your assistant so it doesn't rely on the constraints from earlier prompts. Ask:

```text
Add a confirmation dialog before deleting a task from my Windows XAML app.
Show only the using directives and the dialog construction code.
```

An unconstrained assistant might return:

```csharp
using Windows.UI.Xaml.Controls;

var dialog = new ContentDialog
{
    Title = "Delete task?",
    PrimaryButtonText = "Delete",
    CloseButtonText = "Cancel"
};
```

This is a UWP namespace. `ContentDialog` also exists in WinUI 3, which makes the error easy to miss in a code review.

For this project, the namespace must be:

```csharp
using Microsoft.UI.Xaml.Controls;

var dialog = new ContentDialog
{
    Title = "Delete task?",
    PrimaryButtonText = "Delete",
    CloseButtonText = "Cancel",
    XamlRoot = this.XamlRoot
};
```

The WinUI 3 [ContentDialog](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.contentdialog) reference URL includes `microsoft.ui.xaml.controls`. The UWP [ContentDialog](/uwp/api/windows.ui.xaml.controls.contentdialog) reference uses `windows.ui.xaml.controls`.

This is why “the type exists in Windows documentation” isn't enough. Check the framework, namespace, version, and app model.

## Use a framework check for generated code

When the assistant adds a Windows API, inspect it with these questions:

| Check | What to look for |
|---|---|
| Framework | Is the documentation for WinUI 3 and the Windows App SDK? |
| Namespace | Does UI code use `Microsoft.UI.Xaml`, not `Windows.UI.Xaml` or `System.Windows`? |
| Version | Is the API available in the Windows App SDK version referenced by the project? |
| App model | Does the API require package identity, an app capability, or window initialization? |
| Threading | Must the call run on the UI thread? Is asynchronous work awaited? |
| UX | Is there a built-in WinUI control or pattern for the scenario? |
| Accessibility | Does the generated UI have names, labels, keyboard access, and visible focus? |
| Evidence | Did you build, run, and exercise the changed path? |

Ask the assistant to provide the Learn URL that supports its choice, and then open the URL yourself.

## Verify Task Tally

Run a final verification pass:

1. Delete the app's local data or start with a fresh install. Confirm that the empty state appears and that entering only spaces keeps **Add task** disabled.
1. Add several tasks, mark one complete, and delete another. Confirm that the remaining count updates and the correct item is removed.
1. Restart the app. Confirm that task titles and completion states are restored.
1. Add `if (System.Diagnostics.Debugger.IsAttached) throw new IOException("Test save failure.");` as the first line of `TaskStorage.SaveAsync`, rebuild, and run the app in the debugger. Trigger a save and confirm that the `InfoBar` reports the failure instead of a successful save. Then remove the temporary condition and rebuild.
1. Navigate through every interactive element with <kbd>Tab</kbd> and <kbd>Shift</kbd>+<kbd>Tab</kbd>.
1. Use a screen reader or Accessibility Insights for Windows to inspect names, roles, and focus order.
1. Resize the app, test light, dark, and contrast themes, and build for x64 with no compiler, XAML, binding, or analyzer warnings.

Learn more: [Accessibility overview](../../design/accessibility/accessibility-overview.md) and [Accessibility testing](../../design/accessibility/accessibility-testing.md).

## Refine the prompt, not just the code

If a check fails, save both the correction and the reason in your next prompt:

```text
The generated code used Windows.UI.Xaml.Controls, which is the UWP
namespace. This project is WinUI 3. Replace it with the corresponding
Microsoft.UI.Xaml API, verify the API in the Windows App SDK reference,
build the x64 project, and explain why the original namespace was wrong.
```

This gives the assistant useful evidence and reinforces the project boundary. For a longer-lived project, put stable constraints such as the framework, packaging model, target architecture, and build command in the repository instructions that your coding assistant reads.

## Next steps

You have built a small WinUI 3 app and practiced a workflow you can reuse:

> **Ask → Generate → Build → Inspect → Verify → Refine**

Continue with:

- [Create your first WinUI app](../winui-notes/intro.md) for a deterministic, code-focused introduction to XAML, navigation, and file storage.
- [Data binding, dependency injection, and unit testing in WinUI](../winui-mvvm-toolkit/intro.md) for a larger MVVM architecture.
- [WinUI 3 Gallery](../../dev-tools/samples.md#winui-3-gallery) to verify controls and interaction patterns in a running sample.
- [Windows App SDK API reference](/windows/windows-app-sdk/api/winrt/) to confirm generated API choices.
