---
title: "AI-assisted WinUI tutorial - Plan and prompt"
description: Define verifiable requirements, constrain an AI coding assistant to WinUI 3, create the project, and inspect the generated files.
author: GrantMeStrength
ms.author: jken
ms.date: 08/31/2026
ms.topic: tutorial
---

# Define the app and write the first prompt

A useful first prompt identifies the technology, app behavior, constraints, and the evidence that will show the work is complete. This reduces the chance that the assistant chooses a familiar but incorrect Windows framework.

## Start with acceptance criteria

Before asking for code, define what you expect to observe:

1. The project builds for x64 without errors or warnings.
1. The app uses WinUI 3 and `Microsoft.UI.Xaml`, not UWP and `Windows.UI.Xaml`.
1. A user can add, complete, and delete a task.
1. Tasks remain after the app restarts.
1. Every input and command has an accessible name.
1. Commands remain available when the window is narrow.
1. The assistant explains any API that requires package identity or window initialization.

These criteria turn a broad request into checks that you and the assistant can perform.

## Give the assistant durable constraints

Open the folder where you keep development projects, start your AI coding assistant, and use this prompt:

```text
Create a C# WinUI 3 desktop app named TaskTally.

Technology constraints:
- Use WinUI 3 and the current stable Windows App SDK.
- Use Microsoft.UI.Xaml namespaces. Don't use UWP, Windows.UI.Xaml,
  WPF, Windows Forms, .NET MAUI, or third-party UI controls.
- Create a packaged app and build for x64.
- Start from the dotnet new winui-mvvm template.
- Use CommunityToolkit.Mvvm for observable properties and commands.

App requirements:
- Show a single-page task list.
- Let the user add, complete, and delete tasks.
- Save tasks as JSON in the app's local storage.
- Use ListView for the collection and InfoBar for save status.
- Include an empty state, keyboard access, accessible names, and a
  layout that works at narrow window widths.

Working agreement:
- First show your implementation plan and the files you will change.
- Make one small, buildable change at a time.
- Build after each change and fix errors before continuing.
- Explain which Learn documentation supports any Windows API choice.
- Don't suppress warnings or replace packaged deployment with an
  unpackaged workaround.
```

The last section matters. It asks the assistant to expose its assumptions and produce checkpoints that are easier to inspect.

## Inspect the proposed plan

Before approving edits, check that the plan includes:

- A model for one task.
- A view model with an observable collection and commands.
- A storage service.
- XAML for the page.
- A small amount of code-behind for view-specific events, if needed.
- Build and launch steps for a packaged WinUI app.

Ask the assistant to revise its plan if it proposes a database, dependency injection, multiple pages, or custom controls. Those choices can be valid in a larger app, but they add concepts that this beginner app doesn't need.

## Create the project

The assistant should run commands equivalent to:

```powershell
dotnet new winui-mvvm -n TaskTally
cd TaskTally
dotnet build -p:Platform=x64
```

If you use Visual Studio, you can instead create a packaged C# WinUI project and add the MVVM Toolkit. Keep the project name `TaskTally` so that the namespaces in this tutorial match.

> [!NOTE]
> A packaged WinUI app must be deployed with its package identity. Use Visual Studio, `dotnet run` with the supported WinUI templates, or the `winapp` CLI. Don't launch the generated `.exe` directly.

## Inspect the generated project

Ask:

```text
Explain the purpose of App.xaml, MainWindow.xaml, MainPage.xaml,
Package.appxmanifest, and TaskTally.csproj. For each file, identify
the WinUI 3 or Windows App SDK setting that proves this isn't a UWP,
WPF, or MAUI project.
```

Verify the explanation yourself:

- The project file contains `<UseWinUI>true</UseWinUI>`.
- The project references `Microsoft.WindowsAppSDK`.
- XAML code uses `Microsoft.UI.Xaml` types in C#.
- `Package.appxmanifest` supplies package identity.
- The target framework includes Windows, such as `net10.0-windows10.0.26100.0`.

Build the unchanged template before adding features. If the baseline doesn't build, fix the environment or project creation problem before asking the assistant to generate more code.

> [!div class="nextstepaction"]
> [Add the model, view model, and persistence](data.md)
