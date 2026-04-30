---
title: "Tutorial: Build a Windows app with GitHub Copilot"
description: A step-by-step tutorial for building a WinUI 3 Windows app using GitHub Copilot, the WinUI 3 plugin, and the Microsoft Learn MCP server.
ms.topic: tutorial
ms.date: 03/10/2026
ms.author: jken
author: GrantMeStrength
keywords: windows, github copilot, winui, tutorial, agent mode, mcp server
ms.localizationpriority: medium
---

# Tutorial: Build a Windows app with GitHub Copilot

In this tutorial, you use GitHub Copilot in agent mode to build a complete WinUI 3 app — a simple **notes app** with a list of notes, the ability to add and delete entries, and a settings page. You'll see exactly which prompts to use at each step, and how the WinUI 3 plugin and Learn MCP Server keep Copilot accurate throughout.

> [!div class="checklist"]
> * Scaffold a WinUI 3 project with Copilot
> * Generate a XAML UI with Copilot
> * Add business logic with Copilot
> * Add a Windows notification using the Learn MCP Server
> * Package the app with winapp CLI

## Prerequisites

- GitHub Copilot set up with the WinUI 3 plugin and Learn MCP Server — see [Set up GitHub Copilot for Windows development](ai-setup.md)
- [Visual Studio 2026](/visualstudio/install/install-visual-studio) with the **WinUI application development** workload
- [winapp CLI](../dev-tools/winapp-cli/index.md) installed (`winget install Microsoft.winappcli`)

---

## Part 1: Scaffold the project

### Create the WinUI project

Create a new WinUI 3 project: in Visual Studio, select **File** > **New** > **Project**, filter by **WinUI**, and choose the **WinUI Blank App (Packaged)** C# template. Name the project `NotesApp` and open it.

### Open Copilot agent mode and scaffold the structure

Open the Copilot Chat panel in Visual Studio and switch to **agent mode**. Then enter:

> *"I have a new blank WinUI 3 project called NotesApp. Set up an MVVM architecture with the following structure: a MainWindow with a NavigationView, three pages (Notes, Favorites, Settings), a ViewModels folder with a base ViewModel class and a NotesViewModel, and a Models folder with a Note model that has Id, Title, Content, and CreatedAt properties. Use CommunityToolkit.Mvvm."*

Copilot will create the folder structure, add the CommunityToolkit.Mvvm NuGet package reference, generate the base classes, and wire up the NavigationView. Review the output — if anything looks wrong, ask Copilot to correct it in the same chat session.

> [!TIP]
> If Copilot generates `Windows.UI.Xaml` namespaces instead of `Microsoft.UI.Xaml`, the WinUI 3 plugin's custom instructions should catch this. If you see it, prompt: *"Fix any instances of Windows.UI.Xaml — this is a WinUI 3 project, so all XAML namespaces should use Microsoft.UI.Xaml."*

---

## Part 2: Build the UI

### Generate the Notes page

> *"Generate the XAML for the Notes page. It should have a ListView showing notes with Title and a truncated Content preview, a TextBox and Button at the bottom to add a new note, and a Delete button on each item. Bind it to the NotesViewModel. Use WinUI 3 controls and Fluent Design spacing."*

Copilot generates the XAML, bound to your ViewModel. Ask it to adjust styling or layout as needed — for example:

> *"Make the list items use a card style with a subtle shadow, similar to WinUI Gallery's card examples."*

### Generate the Settings page

> *"Generate a Settings page with a toggle for dark/light theme and a 'Clear all notes' button with a confirmation dialog. Use ContentDialog for the confirmation — not MessageDialog."*

The WinUI 3 plugin's instructions steer Copilot toward `ContentDialog` (the correct WinUI 3 approach) and away from the deprecated `MessageDialog`.

---

## Part 3: Add business logic

### Implement note persistence

> *"Implement note persistence in NotesViewModel using System.Text.Json to serialize and deserialize the notes collection to a file in ApplicationData.Current.LocalFolder. Load notes on startup and save whenever the collection changes."*

Copilot generates the load/save logic. Ask it to add error handling:

> *"Add try/catch around the file operations and log errors to the Debug output."*

### Implement theme switching

> *"Implement the theme toggle in SettingsViewModel. When the toggle changes, update the RequestedTheme on the application's main window using the correct WinUI 3 approach."*

---

## Part 4: Add a Windows notification

Ask Copilot to add a notification that fires when a note is saved:

> *"Add a Windows app notification that shows 'Note saved' with the note title as a subtitle when a note is successfully persisted to disk. Use the Windows App SDK AppNotificationManager."*

With the Learn MCP Server connected, Copilot can look up the current `AppNotificationBuilder` API and generate correct notification code. It should produce something like:

```csharp
var notification = new AppNotificationBuilder()
    .AddText("Note saved")
    .AddText(note.Title)
    .BuildNotification();
AppNotificationManager.Default.Show(notification);
```

---

## Part 5: Package the app

When you're ready to distribute or publish to the Microsoft Store, build a proper MSIX package:

```bash
winapp pack --output ./publish
```

This generates a signed MSIX package ready for sideloading or Store submission. Ask Copilot for help updating the package manifest:

> *"Show me how to update the Package.appxmanifest to set the display name, description, and publisher for Store submission."*

---

## Summary

You've built a complete WinUI 3 notes app using:

- **Copilot agent mode** for scaffolding, UI generation, and business logic
- **WinUI 3 plugin** to keep Copilot on modern, correct APIs throughout
- **Learn MCP Server** to look up Windows App SDK notification APIs
- **winapp CLI** for package identity and MSIX packaging

---

## Optional: Add on-device AI to your app

The notes app is fully functional — but you can take it further by adding an AI feature that runs entirely on the user's device. [Foundry Local](/windows/ai/foundry-local/get-started) makes this straightforward: it runs a language model locally and exposes an OpenAI-compatible API.

### Install Foundry Local and download a model

```bash
winget install Microsoft.AIFoundry.Local
foundry model run phi-4-mini
```

Once the model starts, it listens at `http://localhost:5272/openai/v1`.

### Add the NuGet package

```bash
dotnet add package Azure.AI.OpenAI
```

### Add a "Summarize" button to the Notes page

Ask Copilot:

> *"Add a 'Summarize' button to the Notes page. When clicked, it should send the selected note's content to a local AI endpoint at http://localhost:5272/openai/v1 using the Azure.AI.OpenAI package, and display the summary in a ContentDialog. Model name is phi-4-mini."*

Copilot generates the `AzureOpenAIClient` call and dialog — the OpenAI-compatible API means the code looks identical to a cloud API call, just pointed at localhost:

```csharp
var client = new AzureOpenAIClient(
    new Uri("http://localhost:5272/openai/v1"),
    new ApiKeyCredential("foundry-local"));

var completion = await client.GetChatClient("phi-4-mini")
    .CompleteChatAsync($"Summarize this note in 2 sentences: {note.Content}");
```

### What the user sees

No internet connection required. No API key. The model runs on their PC — fast, private, and free.

> [!TIP]
> For apps targeting Copilot+ PCs, you can swap Foundry Local for [Phi Silica](/windows/ai/apis/phi-silica) to use the NPU directly for even faster inference. The API surface is different (Windows AI APIs rather than OpenAI-compatible), but Copilot can help you make the switch.

---

> [!div class="nextstepaction"]
> [Modernize or port a Windows app with Copilot](../windows-app-sdk/migrate-to-windows-app-sdk/ai-modernize.md)

- [Agentic AI tools for Windows development](../dev-tools/agentic-tools.md)
- [Foundry Local overview](/windows/ai/foundry-local/get-started) — run any model locally on Windows
- [Phi Silica](/windows/ai/apis/phi-silica) — NPU-accelerated inference on Copilot+ PCs
- [Windows AI APIs overview](/windows/ai/)
- [Windows App Development CLI (winapp CLI)](../dev-tools/winapp-cli/index.md)
- [Windows App SDK documentation](../windows-app-sdk/index.md)
