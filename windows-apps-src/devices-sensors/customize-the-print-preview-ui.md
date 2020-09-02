---
ms.assetid: 88132B6F-FB50-4B03-BC21-233988746230
title: Customize the print preview UI
description: This section describes how to customize the print options and settings in the print preview UI.
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, printing
ms.localizationpriority: medium
---
# Customize the print preview UI



**Important APIs**

-   [**Windows.Graphics.Printing**](/uwp/api/Windows.Graphics.Printing)
-   [**Windows.UI.Xaml.Printing**](/uwp/api/Windows.UI.Xaml.Printing)
-   [**PrintManager**](/uwp/api/Windows.Graphics.Printing.PrintManager)

This section describes how to customize the print options and settings in the print preview UI. For more info about printing, see [Print from your app](print-from-your-app.md).

**Tip**  Most of the examples in this topic are based on the print sample. To see the full code, download the [Universal Windows Platform (UWP) print sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/Printing) from the [Windows-universal-samples repo](https://github.com/Microsoft/Windows-universal-samples) on GitHub.

 

## Customize print options

By default, the print preview UI shows the [**ColorMode**](/uwp/api/windows.graphics.printing.standardprinttaskoptions.colormode), [**Copies**](/uwp/api/windows.graphics.printing.standardprinttaskoptions.copies), and [**Orientation**](/uwp/api/windows.graphics.printing.standardprinttaskoptions.orientation) print options. In addition to those, there are several other common printer options that you can add to the print preview UI:

-   [**Binding**](/uwp/api/windows.graphics.printing.standardprinttaskoptions.binding)
-   [**Collation**](/uwp/api/windows.graphics.printing.standardprinttaskoptions.collation)
-   [**Duplex**](/uwp/api/windows.graphics.printing.standardprinttaskoptions.duplex)
-   [**HolePunch**](/uwp/api/windows.graphics.printing.standardprinttaskoptions.holepunch)
-   [**InputBin**](/uwp/api/windows.graphics.printing.standardprinttaskoptions.inputbin)
-   [**MediaSize**](/uwp/api/windows.graphics.printing.standardprinttaskoptions.mediasize)
-   [**MediaType**](/uwp/api/windows.graphics.printing.standardprinttaskoptions.mediatype)
-   [**NUp**](/uwp/api/windows.graphics.printing.standardprinttaskoptions.nup)
-   [**PrintQuality**](/uwp/api/windows.graphics.printing.standardprinttaskoptions.printquality)
-   [**Staple**](/uwp/api/windows.graphics.printing.standardprinttaskoptions.staple)

These options are defined in the [**StandardPrintTaskOptions**](/uwp/api/Windows.Graphics.Printing.StandardPrintTaskOptions) class. You can add to or remove options from the list of options displayed in the print preview UI. You can also change the order in which they appear, and set the default settings that are shown to the user.

However, the modifications that you make by using this method affect only the print preview UI. The user can always access all of the options that the printer supports by tapping **More settings** in the print preview UI.

**Note**  Although your app can specify any print options to be displayed, only those that are supported by the selected printer are shown in the print preview UI. The print UI won't show options that the selected printer doesn't support.

 

### Define the options to display

When the app's screen is loaded, it registers for the Print contract. Part of that registration includes defining the [**PrintTaskRequested**](/uwp/api/Windows.Foundation.IAsyncOperationWithProgress_TResult_TProgress_#Windows_Foundation_IAsyncOperationWithProgress_2_Progress) event handler. The code to customize the options displayed in the print preview UI is added to the **PrintTaskRequested** event handler.

Modify the [**PrintTaskRequested**](/uwp/api/Windows.Foundation.IAsyncOperationWithProgress_TResult_TProgress_#Windows_Foundation_IAsyncOperationWithProgress_2_Progress) event handler to include the [**printTask.options**](/uwp/api/windows.graphics.printing.printtask.options) instructions that configure the print settings that you want to display in the print preview UI.For the screen in your app for which you want to show a customized list of print options, override the **PrintTaskRequested** event handler in the helper class to include code that specifies the options to display when the screen is printed.

``` csharp
protected override void PrintTaskRequested(PrintManager sender, PrintTaskRequestedEventArgs e)
{
   PrintTask printTask = null;
   printTask = e.Request.CreatePrintTask("C# Printing SDK Sample", sourceRequestedArgs =>
   {
         IList<string> displayedOptions = printTask.Options.DisplayedOptions;

         // Choose the printer options to be shown.
         // The order in which the options are appended determines the order in which they appear in the UI
         displayedOptions.Clear();
         displayedOptions.Add(Windows.Graphics.Printing.StandardPrintTaskOptions.Copies);
         displayedOptions.Add(Windows.Graphics.Printing.StandardPrintTaskOptions.Orientation);
         displayedOptions.Add(Windows.Graphics.Printing.StandardPrintTaskOptions.MediaSize);
         displayedOptions.Add(Windows.Graphics.Printing.StandardPrintTaskOptions.Collation);
         displayedOptions.Add(Windows.Graphics.Printing.StandardPrintTaskOptions.Duplex);

         // Preset the default value of the printer option
         printTask.Options.MediaSize = PrintMediaSize.NorthAmericaLegal;

         // Print Task event handler is invoked when the print job is completed.
         printTask.Completed += async (s, args) =>
         {
            // Notify the user when the print operation fails.
            if (args.Completion == PrintTaskCompletion.Failed)
            {
               await scenarioPage.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
               {
                     MainPage.Current.NotifyUser("Failed to print.", NotifyType.ErrorMessage);
               });
            }
         };

         sourceRequestedArgs.SetSource(printDocumentSource);
   });
}
```

**Important**  Calling [**displayedOptions.clear**](/uwp/api/windows.graphics.printing.printtaskoptions.displayedoptions)() removes all of the print options from the print preview UI, including the **More settings** link. Be sure to append the options that you want to show on the print preview UI.

### Specify default options

You can also set the default values of the options in the print preview UI. The following line of code, from the last example, sets the default value of the [**MediaSize**](/uwp/api/windows.graphics.printing.standardprinttaskoptions.mediasize) option.

``` csharp
         // Preset the default value of the printer option
         printTask.Options.MediaSize = PrintMediaSize.NorthAmericaLegal;
```         

## Add new print options

This section shows how to create a new print option, define a list of values that the option supports, and then add the option to the print preview. As in the previous section, add the new print option in the [**PrintTaskRequested**](/uwp/api/Windows.Foundation.IAsyncOperationWithProgress_TResult_TProgress_#Windows_Foundation_IAsyncOperationWithProgress_2_Progress) event handler.

First, get a [**PrintTaskOptionDetails**](/uwp/api/Windows.Graphics.Printing.OptionDetails.PrintTaskOptionDetails) object. This is used to add the new print option to the print preview UI. Then clear the list of options that are shown in the print preview UI and add the options that you want to display when the user wants to print from the app. After that, create the new print option and initialize the list of option values. Finally, add the new option and assign a handler for the **OptionChanged** event.

``` csharp
protected override void PrintTaskRequested(PrintManager sender, PrintTaskRequestedEventArgs e)
{
   PrintTask printTask = null;
   printTask = e.Request.CreatePrintTask("C# Printing SDK Sample", sourceRequestedArgs =>
   {
         PrintTaskOptionDetails printDetailedOptions = PrintTaskOptionDetails.GetFromPrintTaskOptions(printTask.Options);
         IList<string> displayedOptions = printDetailedOptions.DisplayedOptions;

         // Choose the printer options to be shown.
         // The order in which the options are appended determines the order in which they appear in the UI
         displayedOptions.Clear();

         displayedOptions.Add(Windows.Graphics.Printing.StandardPrintTaskOptions.Copies);
         displayedOptions.Add(Windows.Graphics.Printing.StandardPrintTaskOptions.Orientation);
         displayedOptions.Add(Windows.Graphics.Printing.StandardPrintTaskOptions.ColorMode);

         // Create a new list option
         PrintCustomItemListOptionDetails pageFormat = printDetailedOptions.CreateItemListOption("PageContent", "Pictures");
         pageFormat.AddItem("PicturesText", "Pictures and text");
         pageFormat.AddItem("PicturesOnly", "Pictures only");
         pageFormat.AddItem("TextOnly", "Text only");

         // Add the custom option to the option list
         displayedOptions.Add("PageContent");

         printDetailedOptions.OptionChanged += printDetailedOptions_OptionChanged;

         // Print Task event handler is invoked when the print job is completed.
         printTask.Completed += async (s, args) =>
         {
            // Notify the user when the print operation fails.
            if (args.Completion == PrintTaskCompletion.Failed)
            {
               await scenarioPage.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
               {
                     MainPage.Current.NotifyUser("Failed to print.", NotifyType.ErrorMessage);
               });
            }
         };

         sourceRequestedArgs.SetSource(printDocumentSource);
   });
}
```

The options appear in the print preview UI in the same order they are appended, with the first option shown at the top of the window. In this example, the custom option is appended last so that it appears at the bottom of the list of options. However, you could put it anywhere in the list; custom print options don't need to be added last.

When the user changes the selected option in your custom option, update the print preview image. Call the [**InvalidatePreview**](/uwp/api/windows.ui.xaml.printing.printdocument.invalidatepreview) method to redraw the image in the print preview UI, as shown below.

``` csharp
async void printDetailedOptions_OptionChanged(PrintTaskOptionDetails sender, PrintTaskOptionChangedEventArgs args)
{
   // Listen for PageContent changes
   string optionId = args.OptionId as string;
   if (string.IsNullOrEmpty(optionId))
   {
         return;
   }

   if (optionId == "PageContent")
   {
         await scenarioPage.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
         {
            printDocument.InvalidatePreview();
         });
   }
}
```

## Related topics

* [Design guidelines for printing](./printing-and-scanning.md)
* [//Build 2015 video: Developing apps that print in Windows 10](https://channel9.msdn.com/Events/Build/2015/2-94)
* [UWP print sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/Printing)