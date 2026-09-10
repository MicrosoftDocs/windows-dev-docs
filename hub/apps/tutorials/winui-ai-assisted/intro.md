---
title: Build a WinUI 3 task app with an AI assistant
description: Build a C# WinUI 3 task app while learning to prompt an AI assistant, inspect generated code, verify APIs, and refine the result.
author: GrantMeStrength
ms.author: jken
ms.date: 09/10/2026
ms.topic: tutorial
ms.localizationpriority: medium
---

# Build a WinUI 3 task app with an AI assistant

<a id="build-a-winui-app-with-an-ai-coding-assistant"></a>

AI coding assistants can create a working app quickly, but they can also combine APIs from different generations of Windows development. A response might look convincing while mixing WinUI 3 with UWP, WPF, WinUI 2, or .NET MAUI.

In this tutorial, you use an AI coding assistant to build **Task Tally**, a small WinUI 3 task app. More importantly, you learn how to direct the assistant, inspect its work, and verify the result against Microsoft Learn.

The tutorial uses this cycle:

> **Ask → Generate → Build → Inspect → Verify → Refine**

The assistant can write code and explain it, but the compiler, the running app, and the current documentation remain your sources of truth.

## What you build

Task Tally lets a user:

- Add and delete tasks.
- Mark tasks complete.
- Save tasks in local app storage.
- Resize the window without losing access to commands.
- Use the app with a keyboard and accessibility tools.

:::image type="content" source="media/task-tally-finished.png" alt-text="The finished Task Tally app showing four tasks, one completed task, the remaining task count, and a successful update message.":::

You use C#, WinUI 3, the Windows App SDK, and the MVVM Toolkit. The app is packaged with MSIX for local development.

## What you learn

> [!div class="checklist"]
>
> - Write requirements that keep generated code within WinUI 3.
> - Break a feature into prompts that you can build and verify independently.
> - Inspect generated project files, namespaces, controls, and data binding.
> - Verify Windows APIs against Microsoft Learn.
> - Recognize common AI-generated mistakes in Windows app code.
> - Test generated code instead of accepting it because it looks plausible.

## Prerequisites

Complete [Quickstart: Create and run a WinUI 3 project](../../get-started/start-here.md) first, using either its Visual Studio or command-line setup. You also need an AI coding assistant that can read and edit files in your project.

You create a separate `TaskTally` project in this tutorial. The [Notes tutorial](../winui-notes/intro.md) is an alternative for learning XAML and C# fundamentals, not a prerequisite.

You don't need previous WinUI experience. The prompts in this tutorial are tool-neutral, so adapt their command wording to your assistant.

> [!TIP]
> **Explore the Windows App Development CLI**
>
> Prefer command-line tools? The [Windows App Development CLI (winapp)](../../dev-tools/winapp-cli/index.md) provides tools for Windows SDK setup, package identity, manifests, certificates, and MSIX packaging. It's currently available in public preview and is optional for this tutorial. You can continue with the Visual Studio or .NET CLI workflow described here.

> [!IMPORTANT]
> Review changes before you accept them. Don't paste credentials, private source code, personal data, or other sensitive information into a service that isn't approved for that data.

> [!div class="nextstepaction"]
> [Define the app and write the first prompt](plan.md)
