---
title: Setting up Electron for Windows API development
description: Learn how to set up your Electron development environment for Windows API development using the winapp CLI, including SDK initialization and debug identity.
ms.date: 02/20/2026
ms.topic: how-to
---

# Setting up Electron for Windows API development

This guide walks you through setting up your Electron development environment for Windows API development. You'll install the necessary tools, initialize your project, and configure Windows SDKs.

By the end of this guide, you'll have an Electron app that:

- Calls modern Windows APIs (Windows SDK and Windows App SDK)
- Can use native addons with AI capabilities (Phi Silica or WinML)
- Runs with app identity for testing protected APIs
- Can be packaged as a signed MSIX for distribution

## Prerequisites

- **Windows 11** (Copilot+ PC if using Phi Silica)
- **Node.js** - `winget install OpenJS.NodeJS --source winget`
- **.NET SDK v10** - `winget install Microsoft.DotNet.SDK.10 --source winget`
- **Visual Studio with the Native Desktop Workload** - `winget install --id Microsoft.VisualStudio.Community --source winget --override "--add Microsoft.VisualStudio.Workload.NativeDesktop --includeRecommended --passive --wait"`

## Step 1: Create a new Electron app

Start with a fresh Electron app using Electron Forge. If you have an existing app, skip this step.

```bash
npm create electron-app@latest my-windows-app
cd my-windows-app
```

Verify the app runs:

```bash
npm start
```

## Step 2: Install winapp CLI

```bash
npm install --save-dev @microsoft/winappcli
```

## Step 3: Initialize the project for Windows development

```bash
npx winapp init
```

When prompted:

- **Package name**: Press Enter to accept the default (my-windows-app)
- **Publisher name**: Press Enter to accept the default or enter your name
- **Version**: Press Enter to accept 1.0.0.0
- **Entry point**: Press Enter to accept the default (my-windows-app.exe)
- **Setup SDKs**: Select "Stable SDKs"

### What does `winapp init` do?

This command sets up everything you need for Windows development:

1. **Creates `.winapp/` folder** containing headers and libraries from the Windows SDK and Windows App SDK
2. **Generates `appxmanifest.xml`** - The app manifest required for app identity and MSIX packaging
3. **Creates `Assets/` folder** - Contains app icons and visual assets
4. **Creates `winapp.yaml`** - Tracks SDK versions and project configuration
5. **Installs Windows App SDK runtime** - Required runtime components for modern APIs
6. **Enables Developer Mode** in Windows - Required for debugging

> [!NOTE]
> The `.winapp/` folder is automatically added to `.gitignore` and should not be checked in to source control.

## Step 4: Add restore to your build pipeline

Add a `postinstall` script to your `package.json` to ensure the Windows SDKs are available when other developers clone your project:

```json
{
  "scripts": {
    "postinstall": "winapp restore && winapp node add-electron-debug-identity"
  }
}
```

This script runs after `npm install` and:

1. **`winapp restore`** - Downloads and restores all Windows SDK packages
2. **`winapp node add-electron-debug-identity`** - Registers your Electron app with debug identity

For cross-platform projects, create `scripts/postinstall.js`:

```javascript
if (process.platform === 'win32') {
  const { execSync } = require('child_process');
  try {
    execSync('npx winapp restore && npx winapp cert generate --if-exists skip && npx winapp node add-electron-debug-identity', {
      stdio: 'inherit'
    });
  } catch (error) {
    console.warn('Warning: Windows-specific setup failed.');
  }
} else {
  console.log('Skipping Windows-specific setup on non-Windows platform.');
}
```

Then update `package.json`:

```json
{
  "scripts": {
    "postinstall": "node scripts/postinstall.js"
  }
}
```

## Step 5: Understanding debug identity

The `winapp node add-electron-debug-identity` command:

1. Reads your `appxmanifest.xml` to get app details and capabilities
2. Registers `electron.exe` in your `node_modules` with a temporary identity
3. Enables you to test identity-required APIs without creating a full MSIX package

Run this command manually whenever you modify `appxmanifest.xml` or linked assets:

```bash
npx winapp node add-electron-debug-identity
```

Test your setup:

```bash
npm start
```

> [!NOTE]
> There is a known Windows bug with sparse packaging Electron applications that can cause crashes or blank windows. Add `--no-sandbox` to your start script as a workaround: `"start": "electron-forge start -- --no-sandbox"`. This issue does not affect full MSIX packaging. To undo debug identity, run `npx winapp node clear-electron-debug-identity`.

## Next steps

Now that your development environment is set up, create native addons and call Windows APIs:

- [Creating a C++ notification addon](electron-cpp-notification-addon.md) - Call Windows notification APIs from a C++ addon
- [Creating a Phi Silica addon](electron-phi-silica-addon.md) - Use on-device AI for text summarization
- [Creating a WinML addon](electron-winml-addon.md) - Run ONNX machine learning models
- [Packaging for distribution](electron-packaging.md) - Create a signed MSIX package

## Related topics

- [winapp CLI reference](../usage.md)
- [winapp CLI overview](../index.md)
- [Windows App SDK documentation](/windows/apps/windows-app-sdk/)
