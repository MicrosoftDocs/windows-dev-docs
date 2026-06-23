---
title: "Quickstart: Build and publish a Windows app with AI"
description: Go from zero to a published Windows app in under 30 minutes using free tools — VS Code, the WinUI agent plugin for GitHub Copilot, dotnet new templates, and the Windows App Development CLI.
ms.topic: quickstart
ms.date: 05/13/2026
ms.author: jken
author: GrantMeStrength
---

# Quickstart: Build and publish a Windows app with AI

In this quickstart you go from an empty folder to a published Windows app using AI assistance throughout. No Visual Studio required.

> [!NOTE]
> Prefer working in **Visual Studio**? See [Set up GitHub Copilot for Windows development](../../how-tos/ai-setup.md). This quickstart uses VS Code and the winapp CLI.

> [!div class="checklist"]
> * Install the required tools (~5 minutes)
> * Scaffold a WinUI app from the command line
> * Use the `winui-dev` AI agent to add features
> * Package and publish to the Microsoft Store

**Time to complete:** approximately 30 minutes  
**Cost:** free (GitHub Copilot free tier is sufficient)

---

## Prerequisites

Install the following tools before you start. All are free.

**1. Visual Studio Code**

```powershell
winget install Microsoft.VisualStudioCode
```

**2. .NET SDK 10 or later**

```powershell
winget install Microsoft.DotNet.SDK.10
```

**3. Windows App Development CLI (winapp CLI)**

```powershell
winget install Microsoft.winappcli --source winget
```

**4. WinUI dotnet new templates**

```powershell
dotnet new install Microsoft.WindowsAppSDK.WinUI.CSharp.Templates
```

**5. GitHub CLI** (required for `gh copilot` commands — needs a [GitHub Copilot subscription](https://github.com/features/copilot), free tier available)

```powershell
winget install GitHub.cli
```

> [!IMPORTANT]
> Close and reopen your terminal after the install completes. The `gh` command won't be available until a new shell session picks up the updated PATH.

```powershell
gh auth login
gh extension install github/gh-copilot
```

**6. WinUI agent plugin for GitHub Copilot**

```powershell
gh copilot plugin install winui@awesome-copilot
```

**7. WinApp extension for VS Code**

```powershell
code --install-extension microsoft-winappcli.winapp
```

Or search **WinApp** in the Extensions panel (**Ctrl+Shift+X**). See [VS Code tools](vs-code-tools.md) for a full command reference.

**Verify your setup**

```powershell
winapp --version
```

> [!TIP]
> For best results, also connect your AI agent to the [Microsoft Learn MCP server](vs-code-tools.md#microsoft-learn-mcp-server) — it fetches current WinUI 3 API docs at query time rather than relying on training data.

---

## Step 1: Scaffold a new WinUI app

Create a new folder and scaffold a WinUI app with a NavigationView layout:

```powershell
mkdir MyFirstApp
cd MyFirstApp
dotnet new winui-navview
```

---

## Step 2: Run the app

Build and run the app to confirm everything is working before you start adding features:

```powershell
dotnet run
```

The app launches as a loose-layout package — no MSIX install required. You should see a WinUI 3 app with a NavigationView containing Home, About, and Settings pages:

:::image type="content" source="images/myfirstapp.png" alt-text="Screenshot of MyFirstApp running — a WinUI 3 window with a NavigationView showing Home, About, and Settings items, with the Home page selected displaying 'This is the Home page'.":::

Once it launches successfully, open the project in VS Code:

```powershell
code .
```

> [!NOTE]
> Don't press **F5** before the first successful `dotnet run`. VS Code's debugger looks for an `.exe` that doesn't exist yet. After `dotnet run` succeeds, F5 and the **Run** button in the WinApp extension panel both work normally.

---

## Step 3: Use the AI agent to add a feature

In VS Code, open GitHub Copilot Chat (**Ctrl+Alt+I**), switch to **Agent mode**, and select the **winui-dev** agent. Type a request such as:

```
Add a settings page to my WinUI NavigationView app with a toggle for dark mode
```

The agent generates the code, creates any required files, and updates your navigation structure. Review the changes, then verify the result:

```powershell
dotnet run
```

Navigate to the Settings page in the running app to confirm the feature was added correctly.

> [!TIP]
> Want to automate UI verification? See [Testing WinUI apps](testing.md) for `winapp ui` commands that inspect, search, and screenshot your app's UI tree — useful for CI pipelines.

---

## Step 4: Package the app

Publish your app to a folder, then package it as an MSIX installer.

> [!IMPORTANT]
> The packaging step installs a certificate to your machine's trusted root store and requires an **elevated (Administrator) terminal**. Right-click PowerShell or Windows Terminal and select **Run as administrator**, then navigate back to your project folder.

```powershell
dotnet publish -o ./publish
winapp pack ./publish --generate-cert --install-cert
```

`--generate-cert --install-cert` creates and installs a local development certificate for testing. For Store submission, use your Partner Center certificate instead.

---

## Step 5: Publish to the Microsoft Store

Submit your app directly from the command line:

```powershell
winapp store publish ./*.msix --appId <your-app-id>
```

> [!NOTE]
> Publishing requires a [Partner Center account](https://partner.microsoft.com/dashboard). App certification typically takes 1–3 business days.

---

## Next steps

You've built and published a Windows app using only free tools and AI assistance. Here's where to go next:

- **Go deeper on AI**: [WinUI agent plugin](winui-agent-plugin.md) — learn all 8 skills and when to use each
- **Use VS Code fully**: [VS Code tools](vs-code-tools.md) — run, debug, package, and sign without the terminal
- **Have an existing app?**: [Migrate from WPF](migrate/wpf-to-winui.md) or [migrate from UWP](migrate/uwp-to-winui.md) with AI assistance
- **Write better tests**: [AI-assisted testing](testing.md) — generate and automate UI tests
- **Understand the risks**: [Security and responsible AI](security-and-responsible-ai.md) — what to review before shipping AI-generated code
