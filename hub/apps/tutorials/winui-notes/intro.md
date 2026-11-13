---
title: Build a WinUI 3 notes app
description: Learn XAML and C# fundamentals by building a two-page WinUI 3 notes app with local file storage, data binding, and navigation.
author: GrantMeStrength
ms.author: jken
ms.date: 11/13/2026
ms.topic: tutorial
ms.localizationpriority: medium
---
# Build a WinUI 3 notes app

<a id="create-your-first-winui-3-app"></a>

This tutorial series demonstrates how to create a WinUI app using XAML and C#. The app you'll create is a note app, where the user can create, save, and load multiple notes. You can download or view the code for this tutorial from the [GitHub repo](https://github.com/MicrosoftDocs/windows-topic-specific-samples/tree/winui-3/tutorials/winui-notes).

Start here after you can build and run a blank project. This is a step-by-step introduction to WinUI 3 fundamentals using Visual Studio; no AI coding assistant or MVVM Toolkit is required.

In this tutorial, you learn how to:

> [!div class="checklist"]
>
> - Use XAML markup to define the user interface of the app.
> - Interact with XAML elements through C# code.
> - Save and load files from the local file system.
> - Create views and bind them to data.
> - Use navigation to move to and from pages in the app.
> - Use resources like documentation and sample apps to create your own app.

You'll create an app that you can use to enter a note and save it to local app storage. The app will have two pages:

- `NotePage` - a page for editing a single note.
- `AllNotesPage` - a page to show all the saved notes.

The final application is shown below:

_AllNotesPage_

:::image type="content" border="false" source="media/intro/final-all-notes.png" alt-text="Final screenshot of the notes app, showing three save notes.":::

_NotePage_

:::image type="content" border="false" source="media/intro/final-note.png" alt-text="Final screenshot of the notes app, showing a new blank note.":::

## Create the Visual Studio project

Follow the Visual Studio path in [Build your first WinUI app](../../get-started/start-here.md?tabs=visual-studio) to install your tools and create a packaged C# project with these settings:

- **Project name:** `WinUINotes`. Use this exact name so the project's namespace matches the tutorial code.
- **Windows App SDK:** In **Solution Explorer**, right-click the **WinUINotes** project and select **Manage NuGet Packages**. Install or update `Microsoft.WindowsAppSDK` to version **2.6.5**. For details, see the [release notes](../../windows-app-sdk/release-notes/windows-app-sdk-2-0.md?pivots=stable#version-265).

Build and launch the blank `WinUINotes` project before continuing. You should see an empty window that looks like this:

:::image type="content" source="media/intro/step-0.png" alt-text="The notes app window with a title bar and empty content area.":::

> [!TIP]
> You'll frequently refer to API reference docs and conceptual docs while building Windows apps. In this tutorial, you'll see links inline in the text, and in groups labeled, "Learn more in the docs:". These links are optional; you don't need to follow them to complete the tutorial. They're provided in case you want to make note of where to find the information you'll need when you start to create your own apps.

> [!div class="nextstepaction"]
> [Continue to step 1 - Project setup](project.md)
