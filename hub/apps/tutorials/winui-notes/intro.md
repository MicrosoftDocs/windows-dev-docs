---
title: WinUI Notes tutorial - Introduction
description: WinUI Notes tutorial introduction.
author: GrantMeStrength
ms.author: jken
ms.date: 09/02/2025
ms.topic: tutorial
ms.localizationpriority: medium
---
# Create your first WinUI 3 app

This tutorial series demonstrates how to create a WinUI app using XAML and C#. The app you'll create is a note app, where the user can create, save, and load multiple notes. You can download or view the code for this tutorial from the [GitHub repo](https://github.com/MicrosoftDocs/windows-topic-specific-samples/tree/winui-3/tutorials/winui-notes).

In this tutorial, you learn how to:

> [!div class="checklist"]
>
> - Use XAML markup to define the user interface of the app.
> - Interact with XAML elements through C# code.
> - Save and load files from the local file system.
> - Create views and bind them to data.
> - Use navigation to move to and from pages in the app.
> - Use resources like documentation and sample apps to create your own app.

You'll use Visual Studio 2026 to create an app that you can use to enter a note and save it to local app storage. You can find the source code for this app here. The app will have two pages:

- `NotePage` - a page for editing a single note.
- `AllNotesPage` - a page to show all the saved notes.

The final application is shown below:

_AllNotesPage_

:::image type="content" border="false" source="media/intro/final-all-notes.png" alt-text="Final screenshot of the notes app, showing three save notes.":::

_NotePage_

:::image type="content" border="false" source="media/intro/final-note.png" alt-text="Final screenshot of the notes app, showing a new blank note.":::

## Create the Visual Studio project

To begin this tutorial, you must create a WinUI app project in Visual Studio using the `WinUI Blank App (Packaged)` C# project template. When creating the project, use the following settings:

- **Project Name**

  This must be set to `WinUINotes`. If the project is named something different, the code you copy and paste from this tutorial may result in build errors. This is because Visual Studio uses the project name as the default _namespace_ for your app code.

- **Windows App SDK**

  This tutorial uses features that are new in Windows App SDK 1.7. You must make sure the Windows App SDK NuGet package is updated to version 1.7 or later.  

> [!IMPORTANT]
> If you have not created a WinUI project before, follow the steps in **[Start developing Windows apps](../../get-started/start-here.md)** to make sure your dev environment and Visual Studio project are set up correctly.

When you run your blank app project (as outlined in [Start developing Windows apps](../../get-started/start-here.md)), you should see an empty window that looks like this:

:::image type="content" source="media/intro/step-0.png" alt-text="The notes app window with a title bar and empty content area.":::

> [!TIP]
> You'll frequently refer to API reference docs and conceptual docs while building Windows apps. In this tutorial, you'll see links inline in the text, and in groups labeled, "Learn more in the docs:". These links are optional; you don't need to follow them to complete the tutorial. They're provided in case you want to make note of where to find the information you'll need when you start to create your own apps.

> [!div class="nextstepaction"]
> [Continue to step 1 - Project setup](project.md)
