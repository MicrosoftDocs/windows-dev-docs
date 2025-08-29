---
title: PowerToys Text Extractor Utility for Windows
description: Learn how to use PowerToys Text Extractor to copy text from anywhere on your Windows screen, including images and videos. Extract text with OCR technology using simple keyboard shortcuts.
ms.date: 08/20/2025
ms.topic: concept-article
no-loc: [PowerToys, Windows, Text Extractor, Win]
# customer intent: As a Windows power user, I want to learn how to use the Text Extractor utility for Windows.
---

# Text Extractor utility

PowerToys Text Extractor enables you to copy text from anywhere on your Windows screen, including inside images or videos. This powerful OCR utility helps you extract text quickly using keyboard shortcuts, making it easier to capture and use text content from any application or media file. This code is based on [Joe Finney's Text Grab](https://github.com/TheJoeFin/Text-Grab).

> [!NOTE]
> It's recommended to use the [Snipping Tool](https://support.microsoft.com/windows/use-snipping-tool-to-capture-screenshots-00246869-1843-655f-f220-97299b865f6b) instead of the Text Extractor for capturing screenshots.

## How to activate

With the activation shortcut (default: <kbd>⊞ Win</kbd>+<kbd>Shift</kbd>+<kbd>T</kbd>), you'll see an overlay on the screen. Click and hold your primary mouse button and drag to activate your capture. The text will be saved to your clipboard.

## How to deactivate

Capture mode is closed immediately after text in the selected region is recognized and copied to the clipboard. Close capture mode with <kbd>Esc</kbd> at any moment.

## Adjust while trying to capture

By holding <kbd>Shift</kbd>, you change from adjusting the capture region's size to moving the capture region. When you release <kbd>Shift</kbd>, you'll be able to resize again.

> [!IMPORTANT]
>
> 1. The produced text may not be perfect, so you have to do a quick proof read of the output.
> 1. This tool uses OCR (Optical Character Recognition) to read text on the screen.
> 1. The default language used will be based on your [Windows system language > Keyboard settings](https://support.microsoft.com/windows/manage-the-input-and-display-language-settings-in-windows-12a10cb4-8626-9b77-0ccb-5013e0c7c7a2). OCR language packs are available for installation.

## Settings

From the Settings menu, the following options can be configured:

| Setting | Description |
| :--- | :--- |
| Activation shortcut | The customizable keyboard command to turn on or off this module. |
| Preferred language | The language used for OCR. |

## Supported languages

Text Extractor can only recognize languages that have the OCR language pack installed.

The list can be obtained via PowerShell by running the following commands:

```powershell
# Please use Windows PowerShell, not PowerShell 7 as these aren't .NET Core libraries

[Windows.Media.Ocr.OcrEngine, Windows.Foundation, ContentType = WindowsRuntime]

[Windows.Media.Ocr.OcrEngine]::AvailableRecognizerLanguages
```

## Query for OCR language packs

To return the list of all supported language packs, open PowerShell as an Administrator (right-click, then select "Run as Administrator") and enter the following command:

```powershell
Get-WindowsCapability -Online | Where-Object { $_.Name -Like 'Language.OCR*' }
```

An example output:

```powershell
Name  : Language.OCR~~~el-GR~0.0.1.0
State : NotPresent

Name  : Language.OCR~~~en-GB~0.0.1.0
State : NotPresent

Name  : Language.OCR~~~en-US~0.0.1.0
State : Installed

Name  : Language.OCR~~~es-ES~0.0.1.0
State : NotPresent

Name  : Language.OCR~~~es-MX~0.0.1.0
State : NotPresent
```

The language and location is abbreviated, so "en-US" would be "English-United States" and "en-GB" would be "English-Great Britain". If a language is not available in the output, then it's not supported by OCR. `State: NotPresent` languages must be installed first.

## Install an OCR language pack

The following commands install the OCR pack for "en-US":

```powershell
$Capability = Get-WindowsCapability -Online | Where-Object { $_.Name -Like 'Language.OCR*en-US*' }
```

```powershell
$Capability | Add-WindowsCapability -Online
```

> [!NOTE]
> Executing the commands to install an OCR language pack may take several minutes to complete.

## Remove an OCR language pack

The following commands remove the OCR pack for "en-US":

```powershell
$Capability = Get-WindowsCapability -Online | Where-Object { $_.Name -Like 'Language.OCR*en-US*' }
```

```powershell
$Capability | Remove-WindowsCapability -Online
```

## Troubleshooting

This section will list possible errors and solutions.

### "No Possible OCR languages are installed"

This message is shown when there are no available languages for recognition.

If an OCR pack is supported and installed, but still is not available and your system drive _X:_ is different than "C:", then copy `X:/Windows/OCR` folder to `C:/Windows/OCR` to fix the issue.

[!INCLUDE [install-powertoys.md](../includes/install-powertoys.md)]
