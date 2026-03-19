---
title: How to use GitHub Copilot to create WinUI apps in Visual Studio
description: Get started with WinUI / Windows App SDK by using the GitHub Copilot code completion and chat capabilities in Visual Studio. 
ms.topic: how-to
ms.date: 03/19/2026
keywords: windows app sdk, winappsdk, winui, copilot, github copilot
ms.localizationpriority: medium
audience: new-desktop-app-developers
#Customer intent: As a Windows developer, I want to learn how to use GitHub Copilot in Visual Studio to create WinUI apps so that I can leverage the code completion capabilities to build my apps more efficiently.
---

# Use GitHub Copilot to create WinUI 3 / Windows App SDK apps in Visual Studio

In this how-to, we'll demonstrate how [GitHub Copilot](https://github.com/features/copilot) can be used to build WinUI / Windows App SDK desktop apps in Visual Studio. This guide builds upon *[GitHub Copilot in Visual Studio](/visualstudio/ide/visual-studio-github-copilot-extension)*, offering tailored tips and best practices for Copilot-assisted WinUI app development.

:::image type="content" source="images/github-copilot-winui-vs/github-copilot-extension-example.gif" alt-text="Animated screenshot that shows the code completion capabilities of the GitHub Copilot in Visual Studio.":::

## Prerequisites

- Visual Studio 2022 (v17.10 or later) or Visual Studio 2026, with the **WinUI application development** workload applied (see [Set up your environment and create your first WinUI project](../get-started/start-here.md) for setup details). GitHub Copilot is included in Visual Studio 2022 v17.10 and later by default.
- An active subscription to [GitHub Copilot](https://github.com/features/copilot/plans) associated with the GitHub account that you sign in to Visual Studio with.
- Familiarity with C#, WinUI, and Windows App SDK.

## Use GitHub Copilot

### Autocomplete your code snippets

GitHub Copilot in Visual Studio provides real-time code suggestions and completions based on the code you write. The most straightforward way to use Copilot is to start typing code in the editor, and Copilot will try to autocomplete your code snippet. You can then accept or dismiss the suggestions:

<!-- todo: animated gifs as an optimization -->

:::image type="content" source="images/github-copilot-winui-vs/1-basic-autocomplete.png" alt-text="Screenshot that shows the code completion capabilities of GitHub Copilot (basic autocomplete).":::

> [!TIP]
> If you don't see the GitHub Copilot suggestions, you can enable different aspects of the feature in Visual Studio's options under `Tools` -> `Options` -> `GitHub` -> `Copilot`.

### Use Copilot Chat for WinUI questions

The **Copilot Chat** panel (View > GitHub Copilot Chat) is the most flexible way to get WinUI-specific help. Unlike inline completions, Chat lets you ask multi-step questions, reference specific files, and get explanations with full context.

Open the Chat panel and try prompts like:

- *"Using WinUI 3 and Windows App SDK, add a NavigationView with three pages to my MainWindow"*
- *"Explain how XamlRoot works in WinUI 3 and show me how to set it on a ContentDialog"*
- *"Convert this event handler to use DispatcherQueue instead of CoreDispatcher"*

To reference a specific file in Chat, type `#` and select the file from the picker. This is particularly useful when you want Copilot to generate C# code that matches your existing XAML:

```
#MainWindow.xaml Add click handlers in code-behind for all the buttons in this file
```

> [!TIP]
> Always include **WinUI 3** and **Windows App SDK** in your Chat prompts. Without this context, Copilot may suggest UWP patterns (such as `Window.Current` or `CoreDispatcher`) that don't work in WinUI 3. See [WinUI 3 patterns to watch for](#winui-3-patterns-to-watch-for) below.

### Ask Copilot for inline suggestions

Right-click in the code editor and select **Chat** to open an inline chat window at your cursor position. Type your question and press **Enter**:

:::image type="content" source="images/github-copilot-winui-vs/2-generate-multiple-suggestions.png" alt-text="Screenshot that shows the inline chat capabilities of GitHub Copilot.":::

> [!TIP]
> You can promote an inline chat thread to the full Chat panel at any time by selecting **Copilot Actions** > **Add to Chat**. This preserves the conversation context so you can continue in the panel.

### Prompt Copilot with plain-language comments

Although Copilot is used primarily for code completion, you can also use natural language comments to guide Copilot in generating specific code snippets. For example, you can use comments to request a specific feature or functionality:

:::image type="content" source="images/github-copilot-winui-vs/3-prompt-copilot-with-inline-comment.png" alt-text="Screenshot that shows the code completion capabilities of GitHub Copilot (prompting).":::

### Use Copilot Chat to add context from other files

If you're working on a code-behind file and want Copilot to incorporate context from the associated XAML file, reference it directly in the Chat panel using `#`:

```
#MainPage.xaml Generate the C# event handlers for all interactive controls in this file
```

Alternatively, for inline completions you can use temporary comments to paste the relevant XAML into the code-behind file. Copilot will use this as context when generating C# code, and you can delete the comments afterward:

:::image type="content" source="images/github-copilot-winui-vs/4-add-context-temporary-comments.png" alt-text="Screenshot that shows the code completion capabilities of GitHub Copilot (context expansion).":::

### Ask Copilot to explain how something works

The best way to ask Copilot to explain code is to select it in the editor, then right-click and choose **Copilot Actions** > **Explain**. This opens the Chat panel with your selected code already attached as context, and Copilot provides a detailed explanation.

You can also ask for explanations directly in the Chat panel using the `/explain` slash command:

```
/explain What does this DispatcherQueue.TryEnqueue call do?
```

> [!TIP]
> Slash commands like `/explain`, `/fix`, and `/doc` are shortcuts in the Chat panel that tell Copilot exactly what kind of help you want. Type `/` in the Chat input to see all available commands.

### Use Copilot to test code standards

You can use Copilot to generate code that adheres to your project's coding standards, and to test any given snippet's adherence to those standards. Here's an example of how you can use inline comments to specify two conventions, and then have Copilot validate the code snippet against these conventions:

:::image type="content" source="images/github-copilot-winui-vs/6-enforce-code-standards.png" alt-text="Screenshot that shows the code completion capabilities of GitHub Copilot (standards).":::

## WinUI 3 patterns to watch for

Copilot is trained on a large body of Windows code that includes older UWP and WinUI 2 samples. When working in a WinUI 3 project, watch for these suggestions and use the correct WinUI 3 equivalents instead:

| Copilot may suggest | WinUI 3 equivalent | Notes |
|---|---|---|
| `Window.Current` | `App.MainWindow` or store a reference to your window | `Window.Current` is UWP only — it's always null in WinUI 3 |
| `CoreDispatcher` / `Dispatcher.RunAsync` | `DispatcherQueue.TryEnqueue` | WinUI 3 uses `DispatcherQueue` for UI thread dispatch |
| `Windows.UI.Xaml.*` namespaces | `Microsoft.UI.Xaml.*` | All WinUI 3 types are in the `Microsoft.UI.Xaml` namespace |
| `MessageDialog` (Windows.UI.Popups) | `ContentDialog` with `XamlRoot` set | Set `dialog.XamlRoot = this.Content.XamlRoot` before calling `ShowAsync` |
| `FileOpenPicker` without HWND | Initialize with `WinRT.Interop.InitializeWithWindow.Initialize(picker, hwnd)` | WinUI 3 pickers require an HWND; use `WinRT.Interop` to get it |
| `net8.0` or `net10.0` TFM | `net10.0-windows10.0.19041.0` | The unqualified TFM excludes Windows Runtime APIs |

> [!TIP]
> If Copilot generates a pattern from this list, paste it into the Chat panel and ask: *"Is this correct for WinUI 3 with Windows App SDK, or is this a UWP pattern?"* Copilot will usually identify and correct the issue when asked directly.

## Recap

In this how-to, we demonstrated how to use GitHub Copilot in Visual Studio to assist you with WinUI / Windows App SDK desktop app development. We covered how to:

- Autocomplete your code snippets.
- Use the Copilot Chat panel for multi-step WinUI questions and file references.
- Generate autocomplete suggestions inline with Ask Copilot.
- Prompt Copilot with plain-language comments.
- Use `#file` references or temporary comments to add context from other files.
- Ask Copilot to explain code using **Copilot Actions** > **Explain** or the `/explain` slash command in Chat.
- Use Copilot to test and enforce code standards.
- Identify and correct UWP patterns that Copilot may suggest in WinUI 3 projects.

## Related content

- [Sample applications for Windows development](../get-started/samples.md)
- [Windows developer FAQ](../get-started/windows-developer-faq.md)
- [Windows development best practices](../get-started/best-practices.md)
