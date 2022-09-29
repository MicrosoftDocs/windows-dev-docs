---
title: PowerToys Text Extractor utility for Windows
description: Text Extractor is a convenient way to copy text from anywhere on your screen.
ms.date: 08/30/2022
ms.topic: article
no-loc: [PowerToys, Windows, Text Extractor, Win]
---

# Text Extractor utility

Text Extractor is a convenient way to copy text from anywhere on your screen. This code is based on [Joe Finney's Text Grab](https://github.com/TheJoeFin/Text-Grab).

## How to activate

With the activation shortcut (default: <kbd>⊞ Win</kbd>+<kbd>Shift</kbd>+<kbd>T</kbd>), you'll see an overlay on the screen. Click and hold your primary mouse button and drag to activate your capture. The text will be saved to your clipboard.

## How to deactivate

Capture mode is deactivated immediately after text in the selected region is recognized and copied to the clipboard.

You can exit capture mode by pressing <kbd>Esc</kbd> at any moment.

## Adjust while trying to capture

By holding <kbd>Shift</kbd>, you will change from adjusting the capture region's size to moving the capture region. When you release <kbd>Shift</kbd>, you will be able to resize again.

> [!IMPORTANT]
>
> 1. The produced text may not be perfect, so you have to do a quick proof read of the output.
> 2. This tool uses OCR (Optical Character Recognition) to read text on the screen.
> 3. The default language used will be based on your [Windows system language > keyboard settings](https://support.microsoft.com/windows/manage-the-input-and-display-language-settings-in-windows-12a10cb4-8626-9b77-0ccb-5013e0c7c7a2) (OCR language packs are available for install).

## Settings

From the Settings menu, the following options can be configured:

| Setting | Description |
| :--- | :--- |
| Activation shortcut | The customizable keyboard command to turn on or off this module. |

## Supported languages

Text Extractor can only recognize languages that have the OCR pack installed.

The list can be obtained via PowerShell by running following commands:
```console
PS C:\Windows\system32> [Windows.Media.Ocr.OcrEngine, Windows.Foundation, ContentType = WindowsRuntime]
PS C:\Windows\system32> [Windows.Media.Ocr.OcrEngine]::AvailableRecognizerLanguages
```

For supported languages, the OCR language pack can be installed along with the language via <kbd>Language</kbd>-><kbd>Preferred languages</kbd>-><kbd>Add language menu</kbd>.

It's possible to get a list of all supported languages by running this command in PowerShell in Administrator mode:
```console
PS C:\Windows\system32> Get-WindowsCapability -Online | Where-Object { $_.Name -Like 'Language.OCR*' }
```

Example output:
```console
....
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
....
```

If a language is not available in the output, then it's not supported by OCR.

If the state is <kbd>NotPresent</kbd>, then it's possible to install OCR pack for the language.

For example, the following commands install the OCR pack for <kbd>en-US</kbd> in PowerShell run as Administrator:
```console
PS C:\Windows\system32> $Capability = Get-WindowsCapability -Online | Where-Object { $_.Name -Like 'Language.OCR*en-US*' }
PS C:\Windows\system32> $Capability | Add-WindowsCapability -Online
```
These commands remove it:
```console
PS C:\Windows\system32> $Capability = Get-WindowsCapability -Online | Where-Object { $_.Name -Like 'Language.OCR*en-US*' }
PS C:\Windows\system32> $Capability | Remove-WindowsCapability -Online
```

## Troubleshooting

- "No Possible OCR languages are installed." message

This message is shown when there are no available languages for recognition.

If an OCR pack is supported, installed, but still not available and your system drive <kbd>X:</kbd> is different than <kbd>C:</kbd>, then copy <kbd>X:/Windows/OCR</kbd> folder to <kbd>C:/Windows/OCR</kbd> to fix the issue.