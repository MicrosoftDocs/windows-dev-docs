---
title: How to use GitHub Copilot to create WinUI 3 apps in Visual Studio
description: Get started with WinUI 3 / Windows App SDK by using the GitHub Copilot code completion capabilities in Visual Studio. 
ms.topic: how-to
ms.date: 11/26/2024
keywords: windows app sdk, winappsdk, winui3, copilot, github copilot
ms.localizationpriority: medium
audience: new-desktop-app-developers
#Customer intent: As a Windows developer, I want to learn how to use GitHub Copilot in Visual Studio to create WinUI 3 apps so that I can leverage the code completion capabilities to build my apps more efficiently.
---

# Use GitHub Copilot to create WinUI 3 / Windows App SDK apps in Visual Studio

In this how-to, we'll demonstrate how [GitHub Copilot](https://github.com/features/copilot) can be used to build WinUI 3 / Windows App SDK desktop apps in Visual Studio. This guide builds upon *[What is GitHub Copilot Completions for Visual Studio?](/visualstudio/ide/visual-studio-github-copilot-extension)*, offering tailored tips and best practices for Copilot-assisted WinUI 3 app development.

:::image type="content" source="images/github-copilot-winui-vs/github-copilot-extension-example.gif" alt-text="Animated screenshot that shows the code completion capabilities of the GitHub Copilot in Visual Studio.":::

## Prerequisites

- Visual Studio 2022 (v17.10+) with the Windows application development workload applied (see [Get started with WinUI](../get-started/start-here.md) for additional configuration details). GitHub Copilot is included in Visual Studio 2022 v17.10 and later by default.
- An active subscription to [GitHub Copilot](https://github.com/features/copilot/plans) associated with the GitHub account that you sign in to Visual Studio with.
- Familiarity with C#, WinUI, and Windows App SDK.

## Use GitHub Copilot

### Autocomplete your code snippets

GitHub Copilot in Visual Studio provides real-time code suggestions and completions based on the code you write. The most straightforward way to use Copilot is to start typing code in the editor, and Copilot will try to autocomplete your code snippet. You can then accept or dismiss the suggestions:

<!-- todo: animated gifs as an optimization -->

:::image type="content" source="images/github-copilot-winui-vs/1-basic-autocomplete.png" alt-text="Screenshot that shows the code completion capabilities of GitHub Copilot (basic autocomplete).":::

> [!TIP]
> If you don't see the GitHub Copilot suggestions, you can enable different aspects of the feature in Visual Studio's options under `Tools` -> `Options` -> `GitHub` -> `Copilot`.

### Ask Copilot for suggestions

Right-click in the code editor and select `Ask Copilot`. A prompt window will open where you can chat inline with Copilot to get a list of suggestions based on the current cursor position and your prompt:

:::image type="content" source="images/github-copilot-winui-vs/2-generate-multiple-suggestions.png" alt-text="Screenshot that shows the inline prompting capabilities of GitHub Copilot.":::

### Prompt Copilot with plain-language comments

Although Copilot is used primarily for code completion, you can also use natural language comments to guide Copilot in generating specific code snippets. For example, you can use comments to request a specific feature or functionality:

:::image type="content" source="images/github-copilot-winui-vs/3-prompt-copilot-with-inline-comment.png" alt-text="Screenshot that shows the code completion capabilities of GitHub Copilot (prompting).":::

### Use temporary comments to add code from other files to Copilot's context

If you're working on a code-behind file and want Copilot to incorporate context from the associated XAML file, you can use temporary comments to include this additional code within Copilot's context. Here's an example of how you can specify the XAML code first, and then have Copilot generate the corresponding C# code:

:::image type="content" source="images/github-copilot-winui-vs/4-add-context-temporary-comments.png" alt-text="Screenshot that shows the code completion capabilities of GitHub Copilot (context expansion).":::

### Ask Copilot to explain how something works with inline comments

You can use inline comments to ask Copilot to explain how a specific piece of code works. This is similar to using the inline Ask Copilot feature or the Copilot Chat window, except your prompt is typed directly into the code editor:

:::image type="content" source="images/github-copilot-winui-vs/5-ask-copilot-inline-explanation.png" alt-text="Screenshot that shows the code completion capabilities of GitHub Copilot (explain).":::

### Use Copilot to test code standards

You can use Copilot to generate code that adheres to your project's coding standards, and to test any given snippet's adherence to those standards. Here's an example of how you can use inline comments to specify two conventions, and then have Copilot validate the code snippet against these conventions:

:::image type="content" source="images/github-copilot-winui-vs/6-enforce-code-standards.png" alt-text="Screenshot that shows the code completion capabilities of GitHub Copilot (standards).":::

## Recap

In this how-to, we demonstrated how to use GitHub Copilot in Visual Studio to assist you with WinUI 3 / Windows App SDK desktop app development. We covered how to:

- Autocomplete your code snippets.
- Generate autocomplete suggestions inline with Ask Copilot.
- Prompt Copilot with plain-language comments.
- Use temporary comments to add code from other files to Copilot's context.
- Ask Copilot to explain how something works with inline comments.
- Use Copilot to test and enforce code standards.

## Related content

- [Sample applications for Windows development](../get-started/samples.md)
- [Windows developer FAQ](../get-started/windows-developer-faq.yml)
- [Windows development best practices](../get-started/best-practices.md)
