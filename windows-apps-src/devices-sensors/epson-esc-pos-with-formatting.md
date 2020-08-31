---
ms.assetid: 70667353-152B-4B18-92C1-0178298052D4
title: Epson ESC/POS with formatting
description: Learn how to use the ESC/POS command language to format text, such as bold and double size characters, for your Point of Service printer.
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Epson ESC/POS with formatting


**Important APIs**

-   [**PointofService Printer**](/uwp/api/Windows.Devices.PointOfService)
-   [**Windows.Devices.PointOfService**](/uwp/api/Windows.Devices.PointOfService)

Learn how to use the ESC/POS command language to format text, such as bold and double size characters, for your Point of Service printer.

## ESC/POS usage

Windows Point of Service provides use of a variety of printers, including several Epson TM series printers (for a full list of supported printers, see the [PointofService Printer](/uwp/api/Windows.Devices.PointOfService) page). Windows supports printing through the ESC/POS printer control language, which provides efficient and functional commands for communicating with your printer.

ESC/POS is a command system created by Epson used across a wide range of POS printer systems, aimed at avoiding incompatible command sets by providing universal applicability. Most modern printers support ESC/POS.

All commands start with the ESC character (ASCII 27, HEX 1B) or GS (ASCII 29, HEX 1D), followed by another character that specifies the command. Normal text is simply sent to the printer, separated by line breaks.

The [**Windows PointOfService API**](/uwp/api/Windows.Devices.PointOfService) provides much of that functionality for you via the **Print()** or **PrintLine()** methods. However, to get certain formatting or to send specific commands, you must use ESC/POS commands, built as a string and sent to the printer.

## Example using bold and double size characters

The example below shows how to use ESC/POS commands to print in bold and double sized characters. Note that each command is built as a string, then inserted into the printJob calls.

```csharp
// … prior plumbing code removed for brevity
// this code assumed you've already created a receipt print job (printJob)
// and also that you've already checked the PosPrinter Capabilities to
// verify that the printer supports Bold and DoubleHighDoubleWide print modes

const string ESC = "\u001B";
const string GS = "\u001D";
const string InitializePrinter = ESC + "@";
const string BoldOn = ESC + "E" + "\u0001";
const string BoldOff = ESC + "E" + "\0";
const string DoubleOn = GS + "!" + "\u0011";  // 2x sized text (double-high + double-wide)
const string DoubleOff = GS + "!" + "\0";

printJob.Print(InitializePrinter);
printJob.PrintLine("Here is some normal text.");
printJob.PrintLine(BoldOn + "Here is some bold text." + BoldOff);
printJob.PrintLine(DoubleOn + "Here is some large text." + DoubleOff);

printJob.ExecuteAsync();
```

For more information on ESC/POS, including available commands, check out the [Epson ESC/POS FAQ](https://content.epson.de/fileadmin/content/files/RSD/downloads/escpos.pdf). For details on [**Windows.Devices.PointOfService**](/uwp/api/Windows.Devices.PointOfService) and all the available functionality, see [PointofService Printer](/uwp/api/Windows.Devices.PointOfService) on MSDN.