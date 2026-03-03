---
title: Creating a Phi Silica addon for Electron
description: Learn how to create a C# native addon that uses the Phi Silica AI model to summarize text on-device in your Electron app.
ms.date: 02/20/2026
ms.topic: how-to
---

# Creating a Phi Silica addon for Electron

This guide shows you how to create a C# native addon that uses the Phi Silica AI model to summarize text on-device in your Electron app. Phi Silica is a small language model that runs locally on Windows 11 devices with NPUs.

## Prerequisites

- Completed the [development environment setup](electron-setup.md)
- **Copilot+ PC** (Windows 11 with NPU support)

> [!NOTE]
> Phi Silica requires a Copilot+ PC with NPU support. The API will return an error on devices without NPU support.

## Step 1: Create a C# native addon

```bash
npx winapp node create-addon --template cs --name csAddon
```

This creates a `csAddon/` folder with:

- `addon.cs` - Your C# code that will call Windows APIs
- `csAddon.csproj` - Project file with references to Windows SDK and Windows App SDK
- `README.md` - Documentation

The command also adds a `build-addon` script to your `package.json`.

Build the addon:

```bash
npm run build-addon
```

## Step 2: Add Phi Silica code

Open `csAddon/addon.cs` and add the Phi Silica summarization code:

```csharp
using Microsoft.JavaScript.NodeApi;
using Microsoft.Windows.AI.Generative;

namespace CsAddon;

[JSExport]
public static class Addon
{
    public static async Task<string> SummarizeText(string text)
    {
        var session = await LanguageModel.CreateAsync();
        var result = await session.GenerateResponseAsync($"Summarize: {text}");
        return result.Response;
    }
}
```

## Step 3: Build the addon

```bash
npm run build-addon
```

This compiles your C# code using **Native AOT** (Ahead-of-Time compilation), which creates a `.node` binary that requires no .NET runtime on target machines.

## Step 4: Test the addon

Open `src/index.js` and load the addon:

```javascript
const csAddon = require('../csAddon/dist/csAddon.node');
```

Add a test function:

```javascript
const callPhiSilica = async () => {
  console.log('Summarizing with Phi Silica: ')
  const result = await csAddon.Addon.summarizeText("The Windows App Development CLI is a powerful tool that bridges cross-platform development with Windows-native capabilities.");
  console.log('Summary:', result);
};
```

Call it in `createWindow()`:

```javascript
callPhiSilica();
```

## Step 5: Add required capability

Open `appxmanifest.xml` and add the `systemAIModels` capability:

```xml
<Capabilities>
  <rescap:Capability Name="runFullTrust" />
  <rescap:Capability Name="systemAIModels" />
</Capabilities>
```

## Step 6: Update debug identity

After modifying `appxmanifest.xml`, update the debug identity:

```bash
npx winapp node add-electron-debug-identity
```

Run the app:

```bash
npm start
```

Check the console output for the Phi Silica summary.

> [!NOTE]
> There is a known Windows bug with sparse packaging Electron applications that can cause crashes or blank windows. See the [setup guide](electron-setup.md) for the workaround.

## Next steps

- [Creating a WinML addon](electron-winml-addon.md) - Run machine learning models
- [Packaging for distribution](electron-packaging.md) - Create a signed MSIX package

## Related topics

- [Setting up Electron for Windows API development](electron-setup.md)
- [winapp CLI reference](../usage.md)
- [Windows AI APIs](/windows/ai/apis/)
