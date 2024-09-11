---

title: Customize the print preview UI
description: This topic describes how to customize the print options and settings in the print preview UI.
ms.date: 09/10/2024
ms.topic: article
author: jwmsft
ms.author: jimwalk
ms.localizationpriority: medium
---

# Customize the print preview UI

This topic describes how to customize the print options and settings in the print preview UI. For more info about printing, see [Print from your app](print-from-your-app.md).

> [!div class="checklist"]
>
> - **Important APIs**: [Windows.Graphics.Printing namespace](/uwp/api/windows.graphics.printing), [PrintTask](/uwp/api/windows.graphics.printing.printtask), [StandardPrintTaskOptions class](/uwp/api/Windows.Graphics.Printing.StandardPrintTaskOptions), [Microsoft.UI.Xaml.Printing namespace](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.printing), [PrintDocument class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.printing.PrintDocument)

## Customize print options

By default, the print preview UI shows the [**ColorMode**](/uwp/api/windows.graphics.printing.standardprinttaskoptions.colormode), [**Copies**](/uwp/api/windows.graphics.printing.standardprinttaskoptions.copies), and [**Orientation**](/uwp/api/windows.graphics.printing.standardprinttaskoptions.orientation) print options. In addition to those, there are several other common printer options that you can add to the print preview UI:

- [**Binding**](/uwp/api/windows.graphics.printing.standardprinttaskoptions.binding)
- [**Bordering**](/uwp/api/windows.graphics.printing.standardprinttaskoptions.bordering)
- [**Collation**](/uwp/api/windows.graphics.printing.standardprinttaskoptions.collation)
- [**CustomPageRanges**](/uwp/api/windows.graphics.printing.standardprinttaskoptions.custompageranges)
- [**Duplex**](/uwp/api/windows.graphics.printing.standardprinttaskoptions.duplex)
- [**HolePunch**](/uwp/api/windows.graphics.printing.standardprinttaskoptions.holepunch)
- [**InputBin**](/uwp/api/windows.graphics.printing.standardprinttaskoptions.inputbin)
- [**MediaSize**](/uwp/api/windows.graphics.printing.standardprinttaskoptions.mediasize)
- [**MediaType**](/uwp/api/windows.graphics.printing.standardprinttaskoptions.mediatype)
- [**NUp**](/uwp/api/windows.graphics.printing.standardprinttaskoptions.nup)
- [**PrintQuality**](/uwp/api/windows.graphics.printing.standardprinttaskoptions.printquality)
- [**Staple**](/uwp/api/windows.graphics.printing.standardprinttaskoptions.staple)

These options are defined in the [StandardPrintTaskOptions](/uwp/api/windows.graphics.printing.standardprinttaskoptions) class. You can add to or remove options from the list of options displayed in the print preview UI. You can also change the order in which they appear, and set the default settings that are shown to the user.

However, the modifications that you make in this way affect only the print preview UI. The user can always access all of the options that the printer supports by tapping **More settings** in the print preview UI.

### Define the options to display

When you register your app for printing (see [Print from your app](print-from-your-app.md)), part of that registration includes defining the [PrintTaskRequested](/uwp/api/windows.graphics.printing.printmanager.printtaskrequested) event handler. The code to customize the options displayed in the print preview UI is added to the PrintTaskRequested event handler.

After you create the [PrintTask](/uwp/api/windows.graphics.printing.printtask) in the [PrintTaskRequested](/uwp/api/windows.graphics.printing.printmanager.printtaskrequested) event handler , you can get the [DisplayedOptions](/uwp/api/windows.graphics.printing.printtaskoptions.displayedoptions) list, which contains the option items shown in the print preview UI. You can modify this list by inserting, appending, removing, or re-ordering options.

> [!NOTE]
> Although your app can specify any print options to be displayed, only those that are supported by the selected printer are shown in the print preview UI. The print UI won't show options that the selected printer doesn't support.

``` csharp
private void PrintTask_Requested(PrintManager sender, PrintTaskRequestedEventArgs args)
{
    // Create the PrintTask.
    // Defines the title and delegate for PrintTaskSourceRequested.
    PrintTask printTask = args.Request.CreatePrintTask("WinUI 3 Printing example", PrintTaskSourceRequested);

    // Handle PrintTask.Completed to catch failed print jobs.
    printTask.Completed += PrintTask_Completed;

    DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal, () =>
    {
        InvokePrintingButton.IsEnabled = false;
    });

    // Customize options displayed in print preview UI.
    // Get the list of displayed options.
    IList<string> displayedOptions = printTask.Options.DisplayedOptions;

    // Choose the printer options to be shown.
    // The order in which the options are appended determines
    // the order in which they appear in the UI.
    displayedOptions.Clear();
    displayedOptions.Add(StandardPrintTaskOptions.Copies);
    displayedOptions.Add(StandardPrintTaskOptions.Orientation);
    displayedOptions.Add(StandardPrintTaskOptions.MediaSize);
    displayedOptions.Add(StandardPrintTaskOptions.Collation);
    displayedOptions.Add(StandardPrintTaskOptions.Duplex);

    // Preset the default value of the print media size option.
    printTask.Options.MediaSize = PrintMediaSize.NorthAmericaLegal;
}
```

### Specify default options

You can also set the default values of the options in the print preview UI. The following line of code, from the last example, sets the default value of the [MediaSize](/uwp/api/windows.graphics.printing.printtaskoptions.mediasize) option.

``` csharp
// Preset the default value of the print media size option.
printTask.Options.MediaSize = PrintMediaSize.NorthAmericaLegal;
```

> [!NOTE]
> When you set the values in the DisplayedOptions list, you use the name you get from [StandardPrintTaskOptions](/uwp/api/windows.graphics.printing.standardprinttaskoptions) (for example, [StandardPrintTaskOptions.MediaSize](/uwp/api/windows.graphics.printing.standardprinttaskoptions.mediasize)).
>
> When you set the default value of an option, you use [PrintTaskOptions](/uwp/api/windows.graphics.printing.printtaskoptions) (for example, [PrintTaskOptions.MediaSize](/uwp/api/windows.graphics.printing.printtaskoptions.mediasize)).

## Add custom print options

Here, we demonstrate how to create a new custom print option, define a list of values that the option supports, and then add the option to the print preview. In this example, the custom print option lets the user specify whether to print only the text on the page, only the image, or both the text and image. The options are presented in a drop-down list.

In order to ensure a good user experience, the system requires that the app handle the [PrintTaskRequested](/uwp/api/windows.graphics.printing.printmanager.printtaskrequested) event within the time specified by [PrintTaskRequestedEventArgs.Request.Deadline](/uwp/api/windows.graphics.printing.printtasksourcerequestedargs.deadline). Therefore, we use the PrintTaskRequested handler only to create the print task. The print settings customization can be done when the print document source is requested. Here, we use a [lambda expression](/dotnet/csharp/language-reference/operators/lambda-expressions) to define the PrintTaskSourceRequestedHandler inline, which provides easier access to the PrintTask.

First, get a [PrintTaskOptionDetails](/uwp/api/windows.graphics.printing.optiondetails.printtaskoptiondetails) object and its list of [DisplayedOptions](/uwp/api/windows.graphics.printing.optiondetails.printtaskoptiondetails.displayedoptions). You use this to add the new print option to the print preview UI.

Next, to present the custom print options in a drop-down list, call [PrintTaskOptionDetails.CreateItemListOption](/uwp/api/windows.graphics.printing.optiondetails.printtaskoptiondetails.createitemlistoption) to create a [PrintCustomItemListOptionDetails](/uwp/api/windows.graphics.printing.optiondetails.printcustomitemlistoptiondetails) object. Create the new print option and initialize the list of option values. Finally, add the new option to the [DisplayedOptions](/uwp/api/windows.graphics.printing.optiondetails.printtaskoptiondetails.displayedoptions) list and assign a handler for the [OptionChanged](/uwp/api/windows.graphics.printing.optiondetails.printtaskoptiondetails.optionchanged) event. Because you are just adding a new print option at the end of the list of default options, you don't need to clear the DisplayedOptions list; just add the new option.

``` csharp
private void PrintTask_Requested(PrintManager sender, PrintTaskRequestedEventArgs args)
{
    // Create the PrintTask.
    PrintTask printTask = null;
    printTask = args.Request.CreatePrintTask("WinUI 3 Printing example", sourceRequestedArgs =>
    {
        PrintTaskSourceRequestedDeferral deferral = 
            sourceRequestedArgs.GetDeferral();
        PrintTaskOptionDetails printDetailedOptions = 
            PrintTaskOptionDetails.GetFromPrintTaskOptions(printTask.Options);
        IList<string> displayedOptions = printDetailedOptions.DisplayedOptions;

        // Create a new list option.
        PrintCustomItemListOptionDetails pageFormat =
            printDetailedOptions.CreateItemListOption("PageContent", "Page content");
        pageFormat.AddItem("PicturesText", "Pictures and text");
        pageFormat.AddItem("PicturesOnly", "Pictures only");
        pageFormat.AddItem("TextOnly", "Text only");

        // Add the custom option to the option list
        displayedOptions.Add("PageContent");

        printDetailedOptions.OptionChanged += PrintDetailedOptions_OptionChanged;
        sourceRequestedArgs.SetSource(printDocumentSource);

        deferral.Complete();
    });

    // Handle PrintTask.Completed to catch failed print jobs.
    printTask.Completed += PrintTask_Completed;

    DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal, () =>
    {
        InvokePrintingButton.IsEnabled = false;
    });
}
```

The options appear in the print preview UI in the same order they are appended, with the first option shown at the top of the window. In this example, the custom option is appended last so that it appears at the bottom of the list of options. However, you could put it anywhere in the list; custom print options don't need to be added last.

When the user changes the selected option in your custom option, use the selected option to update the print preview image. After the print preview layout in updated, call the [InvalidatePreview](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.printing.printdocument.invalidatepreview) method to redraw the image in the print preview UI, as shown here.

``` csharp
void PrintDetailedOptions_OptionChanged(PrintTaskOptionDetails sender, 
                                        PrintTaskOptionChangedEventArgs args)
{
    string optionId = args.OptionId as string;
    if (string.IsNullOrEmpty(optionId))
    {
        return;
    }

    if (optionId == "PageContent")
    {
        PrintCustomItemListOptionDetails pageContentOption =
            (PrintCustomItemListOptionDetails)sender.Options["PageContent"];
        string pageContentValue = pageContentOption.Value.ToString();

        if (pageContentValue == "PicturesOnly")
        {
            pageLayoutOption = PageLayoutOption.Images;
        }
        else if (pageContentValue == "TextOnly")
        {
            pageLayoutOption = PageLayoutOption.Text;
        }
        else
        {
            pageLayoutOption = PageLayoutOption.TextAndImages;
        }

        DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal, () =>
        {
            printDocument.InvalidatePreview();
        });
    }
}
```

To support use of the custom options in your page, add an enum for the available options and a page-level variable to hold the selected option.

```csharp
internal enum PageLayoutOption : int
{
    Text = 1,
    Images = 2,
    TextAndImages = 3
}

PageLayoutOption pageLayoutOption = PageLayoutOption.TextAndImages;
```

Then, use the selected option in your Paginate handler where you add the content for the print preview.

```csharp
private void PrintDocument_Paginate(object sender, PaginateEventArgs e)
{
    // Clear the cache of preview pages.
    printPreviewPages.Clear();

    // Get the PrintTaskOptions.
    PrintTaskOptions printingOptions = ((PrintTaskOptions)e.PrintTaskOptions);
    // Get the page description to determine the size of the print page.
    PrintPageDescription pageDescription = printingOptions.GetPageDescription(0);

    // Create the print layout.
    StackPanel printLayout = new StackPanel();
    printLayout.Width = pageDescription.PageSize.Width;
    printLayout.Height = pageDescription.PageSize.Height;
    printLayout.BorderBrush = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.DimGray);
    printLayout.BorderThickness = new Thickness(48);

    // Use the custom print layout options to determine
    // which elements to add to the print page. 
    if (pageLayoutOption == PageLayoutOption.Images ||
        pageLayoutOption == PageLayoutOption.TextAndImages)
    {
        Image printImage = new Image();
        printImage.Source = printContent.Source;

        printImage.Width = pageDescription.PageSize.Width / 2;
        printImage.Height = pageDescription.PageSize.Height / 2;
        printLayout.Children.Add(printImage);
    }

    if (pageLayoutOption == PageLayoutOption.Text ||
        pageLayoutOption == PageLayoutOption.TextAndImages)
    {
        TextBlock imageDescriptionText = new TextBlock();
        imageDescriptionText.Text = imageDescription.Text;
        imageDescriptionText.FontSize = 24;
        imageDescriptionText.HorizontalAlignment = HorizontalAlignment.Center;
        imageDescriptionText.Width = pageDescription.PageSize.Width / 2;
        imageDescriptionText.TextWrapping = TextWrapping.WrapWholeWords;

        printLayout.Children.Add(imageDescriptionText);
    }

    // Add the print layout to the list of preview pages.
    printPreviewPages.Add(printLayout);

    // Report the number of preview pages created.
    PrintDocument printDocument = (PrintDocument)sender;
    printDocument.SetPreviewPageCount(printPreviewPages.Count,
                                          PreviewPageCountType.Intermediate);
}

```

## See also

- [Printing and scanning](printing-and-scanning.md)
- [Print from your app](print-from-your-app.md)
