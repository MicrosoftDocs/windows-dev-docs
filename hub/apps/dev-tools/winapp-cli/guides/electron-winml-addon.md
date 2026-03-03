---
title: Creating a WinML addon for Electron
description: Learn how to create a C# native addon that uses Windows Machine Learning (WinML) to run ONNX models in your Electron app.
ms.date: 02/20/2026
ms.topic: how-to
---

# Creating a WinML addon for Electron

This guide shows you how to create a C# native addon that uses Windows Machine Learning (WinML) in your Electron app. WinML allows you to run machine learning models (ONNX format) locally on Windows devices for tasks like image classification, object detection, and more.

## Prerequisites

- Completed the [development environment setup](electron-setup.md)
- **Windows 11** or Windows 10 (version 1809 or later)

> [!NOTE]
> WinML runs on any Windows 10 (1809+) or Windows 11 device. For best performance, devices with GPUs or NPUs are recommended, but the API works on CPU as well.

## Step 1: Create a C# native addon

```bash
npx winapp node create-addon --template cs --name winMlAddon
```

This creates a `winMlAddon/` folder with a C# project configured with Windows SDK and Windows App SDK references.

Build the addon:

```bash
npm run build-winMlAddon
```

## Step 2: Download the SqueezeNet model

1. Install the [AI Dev Gallery](https://aka.ms/aidevgallery)
2. Navigate to the **Classify Image** sample
3. Download the **SqueezeNet 1.1** model
4. Copy the `.onnx` file to a `models/` folder in your project root

> [!NOTE]
> The model can also be downloaded from the [ONNX Model Zoo GitHub repo](https://github.com/onnx/models/blob/main/validated/vision/classification/squeezenet/model/squeezenet1.1-7.onnx).

## Step 3: Add required NuGet packages

Update `Directory.packages.props` in your project root:

```xml
<PackageVersion Include="Microsoft.ML.OnnxRuntime.Extensions" Version="0.14.0" />
<PackageVersion Include="System.Drawing.Common" Version="9.0.9" />
```

Update `winMlAddon/winMlAddon.csproj` to add the package references:

```xml
<PackageReference Include="Microsoft.ML.OnnxRuntime.Extensions" />
<PackageReference Include="System.Drawing.Common" />
```

## Step 4: Add the sample code

The [AI Dev Gallery](https://aka.ms/aidevgallery) provides the complete implementation for image classification with SqueezeNet. You can find the adapted code in the [electron-winml sample](https://github.com/microsoft/WinAppCli/tree/main/samples/electron-winml/).

Copy the `winMlAddon/` folder from the sample, or manually update `winMlAddon/addon.cs` with the sample code.

### Key implementation details

**Project root path**: The addon requires the JavaScript code to pass the project root path so it can locate the ONNX model and native dependencies.

**Preloading native dependencies**: The addon includes a method to load required DLLs that works for both development and production scenarios.

**Electron Forge configuration**: Configure your packager to unpack native files:

```javascript
module.exports = {
  packagerConfig: {
    asar: {
      unpack: "**/*.{dll,exe,node,onnx}"
    },
    ignore: [
      /^\/.winapp\//,
      "\\.msix$",
      /^\/winMlAddon\/(?!dist).+/
    ]
  },
};
```

## Step 5: Build the addon

```bash
npm run build-winMlAddon
```

## Step 6: Test the addon

Open `src/index.js` and load the addon:

```javascript
const winMlAddon = require('../winMlAddon/dist/winMlAddon.node');
```

Add a test function:

```javascript
const testWinML = async () => {
  try {
    let projectRoot = path.join(__dirname, '..');
    if (projectRoot.includes('app.asar')) {
      projectRoot = projectRoot.replace('app.asar', 'app.asar.unpacked');
    }

    const addon = await winMlAddon.Addon.createAsync(projectRoot);
    console.log('Model loaded successfully!');

    const imagePath = path.join(projectRoot, 'test-images', 'sample.jpg');
    const predictions = await addon.classifyImage(imagePath);

    console.log('Top predictions:');
    predictions.slice(0, 5).forEach((pred, i) => {
      console.log(`${i + 1}. ${pred.label}: ${(pred.confidence * 100).toFixed(2)}%`);
    });
  } catch (error) {
    console.error('Error testing WinML:', error.message);
  }
};
```

Prepare test images by creating a `test-images/` folder with sample images, then run:

```bash
npm start
```

## Step 7: Update debug identity

```bash
npx winapp node add-electron-debug-identity
```

> [!NOTE]
> There is a known Windows bug with sparse packaging Electron applications that can cause crashes or blank windows. See the [setup guide](electron-setup.md) for the workaround.

## Next steps

- [Creating a Phi Silica addon](electron-phi-silica-addon.md) - Use on-device AI APIs
- [Packaging for distribution](electron-packaging.md) - Create a signed MSIX package

## Related topics

- [Setting up Electron for Windows API development](electron-setup.md)
- [winapp CLI reference](../usage.md)
- [WinML documentation](/windows/ai/windows-ml/)
- [AI Dev Gallery](https://aka.ms/aidevgallery)
