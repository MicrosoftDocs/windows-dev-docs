---
title: How to use GitHub Copilot to create WinUI 3 / Windows App SDK apps in Visual Studio
description: Get started with WinUI 3 / Windows App SDK by integrating GitHub Copilot code autocompletion capabilities into Visual Studio. 
ms.topic: article
ms.date: 3/12/2024
keywords: windows app sdk, winappsdk, winui3, copilot
ms.author: mikben
author: matchamatch
ms.localizationpriority: medium
ms.custom: template-quickstart
audience: new-desktop-app-developers
content-type: how-to
---

# Use GitHub Copilot to create WinUI 3 / Windows App SDK apps in Visual Studio

In this how-to, we'll demonstrate how [GitHub Copilot](https://github.com/features/copilot) can be used to build WinUI 3 / Windows App SDK desktop apps in Visual Studio. This guide builds upon *[What is the GitHub Copilot extension for Visual Studio?](https://learn.microsoft.com/visualstudio/ide/visual-studio-github-copilot-extension)*, offering tailored tips and best practices for Copilot-assisted WinUI 3 app development.

:::image type="content" source="images/github-copilot-winui-vs/github-copilot-extension-example.gif" alt-text="Animated screenshot that shows the code completion capabilities of the GitHub Copilot extension.":::

## Prerequisites

- [Visual Studio 2022 (v17.6+) and Tools for Windows App SDK](../windows-app-sdk/set-up-your-development-environment.md)
- An active subscription to [GitHub Copilot](https://github.com/features/copilot/plans) associated with the GitHub account that you sign in to Visual Studio with.
- Familiarity with C# and WinUI 3 / Windows App SDK.

## Installation

1. In Visual Studio, select **Extensions** > **Manage Extensions**.
2. Search for "GitHub Copilot" and click `Download`. Optionally, you can also install the [GitHub Copilot Chat Extension](https://marketplace.visualstudio.com/items?itemName=GitHub.copilot-chat) to enable Copilot's interface.
3. Restart Visual Studio.
4. Click `Modify` when prompted to install the extension.


## Use GitHub Copilot

### Autocomplete your code snippets

The GitHub Copilot extension provides real-time code suggestions and completions based on the code you write. The most straightforward way to use Copilot is to start typing code in the editor, and Copilot will try to autocomplete your code snippet. You can then accept or dismiss the suggestions:

<!-- todo: animated gifs as an optimization -->

:::image type="content" source="images/github-copilot-winui-vs/1-basic-autocomplete.png" alt-text="Screenshot that shows the code completion capabilities of the GitHub Copilot extension.":::

### Generate multiple autocomplete suggestions

Select `Edit` -> `Copilot suggestions` -> `Open Copilot`. A window will open with a list of suggestions based on the latest cursor position:

:::image type="content" source="images/github-copilot-winui-vs/2-generate-multiple-autocomplete-suggestions.png" alt-text="Screenshot that shows the code completion capabilities of the GitHub Copilot extension.":::

### Prompt Copilot with plain-language comments

Although Copilot is used primarily for code completion, you can also use natural language comments to guide Copilot in generating specific code snippets. For example, you can use comments to request a specific feature or functionality:

:::image type="content" source="images/github-copilot-winui-vs/3-prompt-copilot-with-inline-comment.png" alt-text="Screenshot that shows the code completion capabilities of the GitHub Copilot extension.":::

### Use temporary comments to add code from other files to Copilot's context

If you're working on a code-behind file and want Copilot to incorporate context from the associated XAML file, you can use temporary comments to include this additional code within Copilot's context. Here's an example of how you can specify the XAML code first, and then have Copilot generate the corresponding C# code:

:::image type="content" source="images/github-copilot-winui-vs/4-add-context-temporary-comments.png" alt-text="Screenshot that shows the code completion capabilities of the GitHub Copilot extension.":::

### Ask Copilot to explain how something works with inline comments

You can use inline comments to ask Copilot to explain how a specific piece of code works. This is similar to using the Copilot Chat Extension, except your prompt is typed directly into the code editor:

:::image type="content" source="images/github-copilot-winui-vs/5-ask-copilot-inline-explanation.png" alt-text="Screenshot that shows the code completion capabilities of the GitHub Copilot extension.":::

### Use Copilot to test code standards

You can use Copilot to generate code that adheres to your project's coding standards, and to test any given snippet's adherence to those standards. Here's an example of how you can use inline comments to specify two conventions, and then have Copilot validate the code snippet against these conventions:

:::image type="content" source="images/github-copilot-winui-vs/6-enforce-code-standards.png" alt-text="Screenshot that shows the code completion capabilities of the GitHub Copilot extension.":::

## Recap

In this how-to, we demonstrated how to use the GitHub Copilot extension to assist you with WinUI 3 / Windows App SDK desktop app development in Visual Studio. We covered how to:

- Autocomplete your code snippets.
- Generate multiple autocomplete suggestions.
- Prompt Copilot with plain-language comments.
- Use temporary comments to add code from other files to Copilot's context.
- Ask Copilot to explain how something works with inline comments.
- Use Copilot to test and enforce code standards.


## Related

- [Sample applications for Windows development](../get-started/samples.md)
- [Windows developer FAQ](../get-started/windows-developer-faq.yml)
- [Windows developer glossary](../get-started/windows-developer-glossary.md)
- [Windows development best practices](../get-started/best-practices.md)
- [How to target multiple platforms with your WinUI 3 app](uno-multiplatform.md)