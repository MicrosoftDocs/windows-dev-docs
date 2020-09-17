---
ms.assetid: 9A0F1852-A76B-4F43-ACFC-2CC56AAD1C03
title: Print from your app
description: Learn how to print documents from a Universal Windows app. This topic also shows how to print specific pages.
ms.date: 01/29/2018
ms.topic: article
keywords: windows 10, uwp, printing
ms.localizationpriority: medium
---
# Print from your app



**Important APIs**

-   [**Windows.Graphics.Printing**](/uwp/api/Windows.Graphics.Printing)
-   [**Windows.UI.Xaml.Printing**](/uwp/api/Windows.UI.Xaml.Printing)
-   [**PrintDocument**](/uwp/api/Windows.UI.Xaml.Printing.PrintDocument)

Learn how to print documents from a Universal Windows app. This topic also shows how to print specific pages. For more advanced changes to the print preview UI, see [Customize the print preview UI](customize-the-print-preview-ui.md).

> [!TIP]
> Most of the examples in this topic are based on the [Universal Windows Platform (UWP) print sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/Printing), which is part of the [Universal Windows Platform (UWP) app samples](https://github.com/Microsoft/Windows-universal-samples) repo on GitHub.

## Register for printing

The first step to add printing to your app is to register for the Print contract. Your app must do this on every screen from which you want your user to be able to print. Only the screen that is displayed to the user can be registered for printing. If one screen of your app has registered for printing, it must unregister for printing when it exits. If it is replaced by another screen, the next screen must register for a new Print contract when it opens.

> [!TIP]
> If you need to support printing from more than one page in your app, you can put this print code in a common helper class and have your app pages reuse it. For an example of how to do this, see the `PrintHelper` class in the [UWP print sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/Printing).

First, declare the [**PrintManager**](/uwp/api/Windows.Graphics.Printing.PrintManager) and [**PrintDocument**](/uwp/api/Windows.UI.Xaml.Printing.PrintDocument). The **PrintManager** type is in the [**Windows.Graphics.Printing**](/uwp/api/Windows.Graphics.Printing) namespace along with types to support other Windows printing functionality. The **PrintDocument** type is in the [**Windows.UI.Xaml.Printing**](/uwp/api/Windows.UI.Xaml.Printing) namespace along with other types that support preparing XAML content for printing. You can make it easier to write your printing code by adding the following **using** or **Imports** statements to your page.

```csharp
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
```

The [**PrintDocument**](/uwp/api/Windows.UI.Xaml.Printing.PrintDocument) class is used to handle much of the interaction between the app and the [**PrintManager**](/uwp/api/Windows.Graphics.Printing.PrintManager), but it exposes several callbacks of its own. During registration, create instances of **PrintManager** and **PrintDocument** and register handlers for their printing events.

In the [UWP print sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/Printing), registration is performed by the `RegisterForPrinting` method.

```csharp
public virtual void RegisterForPrinting()
{
   printDocument = new PrintDocument();
   printDocumentSource = printDocument.DocumentSource;
   printDocument.Paginate += CreatePrintPreviewPages;
   printDocument.GetPreviewPage += GetPrintPreviewPage;
   printDocument.AddPages += AddPrintPages;

   PrintManager printMan = PrintManager.GetForCurrentView();
   printMan.PrintTaskRequested += PrintTaskRequested;
}
```

When the user goes to a page that supports printing, it initiates the registration within the `OnNavigatedTo` method.

```csharp
protected override void OnNavigatedTo(NavigationEventArgs e)
{
   // Initialize common helper class and register for printing
   printHelper = new PrintHelper(this);
   printHelper.RegisterForPrinting();

   // Initialize print content for this scenario
   printHelper.PreparePrintContent(new PageToPrint());

   // Tell the user how to print
   MainPage.Current.NotifyUser("Print contract registered with customization, use the Print button to print.", NotifyType.StatusMessage);
}
```

In the sample, the event handlers are unregistered in the `UnregisterForPrinting` method.

```csharp
public virtual void UnregisterForPrinting()
{
    if (printDocument == null)
    {
        return;
    }

    printDocument.Paginate -= CreatePrintPreviewPages;
    printDocument.GetPreviewPage -= GetPrintPreviewPage;
    printDocument.AddPages -= AddPrintPages;

    PrintManager printMan = PrintManager.GetForCurrentView();
    printMan.PrintTaskRequested -= PrintTaskRequested;
}
```

When the user leaves a page that supports printing, the event handlers are unregistered within the `OnNavigatedFrom` method. 

> [!NOTE]
> If you have a multiple-page app and don't disconnect printing, an exception is thrown when the user leaves the page and then returns to it.

```csharp
protected override void OnNavigatedFrom(NavigationEventArgs e)
{
   if (printHelper != null)
   {
         printHelper.UnregisterForPrinting();
   }
}
```

## Create a print button

Add a print button to your app's screen where you'd like it to appear. Make sure that it doesn't interfere with the content that you want to print.

```xml
<Button x:Name="InvokePrintingButton" Content="Print" Click="OnPrintButtonClick"/>
```

Next, add an event handler to your app's code to handle the click event. Use the [**ShowPrintUIAsync**](/uwp/api/windows.graphics.printing.printmanager.showprintuiasync) method to start printing from your app. **ShowPrintUIAsync** is an asynchronous method that displays the appropriate printing window. We recommend calling the [**IsSupported**](/uwp/api/windows.graphics.printing.printmanager.issupported) method first in order to check that the app is being run on a device that supports printing (and handle the case in which it is not). If printing can't be performed at that time for any other reason, **ShowPrintUIAsync** will throw an exception. We recommend catching these exceptions and letting the user know when printing can't proceed.

In this example, a print window is displayed in the event handler for a button click. If the method throws an exception (because printing can't be performed at that time), a [**ContentDialog**](/uwp/api/Windows.UI.Xaml.Controls.ContentDialog) control informs the user of the situation.

```csharp
async private void OnPrintButtonClick(object sender, RoutedEventArgs e)
{
    if (Windows.Graphics.Printing.PrintManager.IsSupported())
    {
        try
        {
            // Show print UI
            await Windows.Graphics.Printing.PrintManager.ShowPrintUIAsync();

        }
        catch
        {
            // Printing cannot proceed at this time
            ContentDialog noPrintingDialog = new ContentDialog()
            {
                Title = "Printing error",
                Content = "\nSorry, printing can' t proceed at this time.", PrimaryButtonText = "OK"
            };
            await noPrintingDialog.ShowAsync();
        }
    }
    else
    {
        // Printing is not supported on this device
        ContentDialog noPrintingDialog = new ContentDialog()
        {
            Title = "Printing not supported",
            Content = "\nSorry, printing is not supported on this device.",PrimaryButtonText = "OK"
        };
        await noPrintingDialog.ShowAsync();
    }
}
```

## Format your app's content

When **ShowPrintUIAsync** is called, the [**PrintTaskRequested**](/uwp/api/Windows.Foundation.IAsyncOperationWithProgress_TResult_TProgress_#Windows_Foundation_IAsyncOperationWithProgress_2_Progress) event is raised. The **PrintTaskRequested** event handler shown in this step creates a [**PrintTask**](/uwp/api/Windows.Graphics.Printing.PrintTask) by calling the [**PrintTaskRequest.CreatePrintTask**](/uwp/api/windows.graphics.printing.printtaskrequest.createprinttask) method and passes the title for the print page and the name of a [**PrintTaskSourceRequestedHandler**](/uwp/api/windows.graphics.printing.printtask.source) delegate. Notice that in this example, the **PrintTaskSourceRequestedHandler** is defined inline. The **PrintTaskSourceRequestedHandler** provides the formatted content for printing and is described later.

In this example, a completion handler is also defined to catch errors. It's a good idea to handle completion events because then your app can let the user know if an error occurred and provide possible solutions. Likewise, your app could use the completion event to indicate subsequent steps for the user to take after the print job is successful.

```csharp
protected virtual void PrintTaskRequested(PrintManager sender, PrintTaskRequestedEventArgs e)
{
   PrintTask printTask = null;
   printTask = e.Request.CreatePrintTask("C# Printing SDK Sample", sourceRequested =>
   {
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

         sourceRequested.SetSource(printDocumentSource);
   });
}
```

After the print task is created, the [**PrintManager**](/uwp/api/Windows.Graphics.Printing.PrintManager) requests a collection of print pages to show in the print preview UI by raising the [**Paginate**](/uwp/api/windows.ui.xaml.printing.printdocument.paginate) event. This corresponds with the **Paginate** method of the **IPrintPreviewPageCollection** interface. The event handler you created during registration will be called at this time.

> [!IMPORTANT]
> If the user changes print settings, the paginate event handler will be called again to allow you to reflow the content. For the best user experience, we recommend checking the settings before you reflow the content and avoid reinitializing the paginated content when it's not necessary.

In the [**Paginate**](/uwp/api/windows.ui.xaml.printing.printdocument.paginate) event handler (the `CreatePrintPreviewPages` method in the [UWP print sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/Printing)), create the pages to show in the print preview UI and to send to the printer. The code you use to prepare your app's content for printing is specific to your app and the content you print. Refer to the [UWP print sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/Printing) source code to see how it formats its content for printing.

```csharp
protected virtual void CreatePrintPreviewPages(object sender, PaginateEventArgs e)
{
   // Clear the cache of preview pages
   printPreviewPages.Clear();

   // Clear the print canvas of preview pages
   PrintCanvas.Children.Clear();

   // This variable keeps track of the last RichTextBlockOverflow element that was added to a page which will be printed
   RichTextBlockOverflow lastRTBOOnPage;

   // Get the PrintTaskOptions
   PrintTaskOptions printingOptions = ((PrintTaskOptions)e.PrintTaskOptions);

   // Get the page description to deterimine how big the page is
   PrintPageDescription pageDescription = printingOptions.GetPageDescription(0);

   // We know there is at least one page to be printed. passing null as the first parameter to
   // AddOnePrintPreviewPage tells the function to add the first page.
   lastRTBOOnPage = AddOnePrintPreviewPage(null, pageDescription);

   // We know there are more pages to be added as long as the last RichTextBoxOverflow added to a print preview
   // page has extra content
   while (lastRTBOOnPage.HasOverflowContent && lastRTBOOnPage.Visibility == Windows.UI.Xaml.Visibility.Visible)
   {
         lastRTBOOnPage = AddOnePrintPreviewPage(lastRTBOOnPage, pageDescription);
   }

   if (PreviewPagesCreated != null)
   {
         PreviewPagesCreated.Invoke(printPreviewPages, null);
   }

   PrintDocument printDoc = (PrintDocument)sender;

   // Report the number of preview pages created
   printDoc.SetPreviewPageCount(printPreviewPages.Count, PreviewPageCountType.Intermediate);
}
```

When a particular page is to be shown in the print preview window, the [**PrintManager**](/uwp/api/Windows.Graphics.Printing.PrintManager) raises the [**GetPreviewPage**](/uwp/api/windows.ui.xaml.printing.printdocument.getpreviewpage) event. This corresponds with the **MakePage** method of the **IPrintPreviewPageCollection** interface. The event handler you created during registration will be called at this time.

In the [**GetPreviewPage**](/uwp/api/windows.ui.xaml.printing.printdocument.getpreviewpage) event handler (the `GetPrintPreviewPage` method in the [UWP print sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/Printing)), set the appropriate page on the print document.

```csharp
protected virtual void GetPrintPreviewPage(object sender, GetPreviewPageEventArgs e)
{
   PrintDocument printDoc = (PrintDocument)sender;
   printDoc.SetPreviewPage(e.PageNumber, printPreviewPages[e.PageNumber - 1]);
}
```

Finally, once the user clicks the print button, the [**PrintManager**](/uwp/api/Windows.Graphics.Printing.PrintManager) requests the final collection of pages to send to the printer by calling the **MakeDocument** method of the **IDocumentPageSource** interface. In XAML, this raises the [**AddPages**](/uwp/api/windows.ui.xaml.printing.printdocument.addpages) event. The event handler you created during registration will be called at this time.

In the [**AddPages**](/uwp/api/windows.ui.xaml.printing.printdocument.addpages) event handler (the `AddPrintPages` method in the [UWP print sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/Printing)), add pages from the page collection to the [**PrintDocument**](/uwp/api/Windows.UI.Xaml.Printing.PrintDocument) object to be sent to the printer. If a user specifies particular pages or a range of pages to print, you use that information here to add only the pages that will actually be sent to the printer.

```csharp
protected virtual void AddPrintPages(object sender, AddPagesEventArgs e)
{
   // Loop over all of the preview pages and add each one to  add each page to be printied
   for (int i = 0; i < printPreviewPages.Count; i++)
   {
         // We should have all pages ready at this point...
         printDocument.AddPage(printPreviewPages[i]);
   }

   PrintDocument printDoc = (PrintDocument)sender;

   // Indicate that all of the print pages have been provided
   printDoc.AddPagesComplete();
}
```

## Prepare print options

Next prepare print options. As an example, this section will describe how to set the page range option to allow printing of specific pages. For more advanced options, see [Customize the print preview UI](customize-the-print-preview-ui.md).

This step creates a new print option, defines a list of values that the option supports, and then adds the option to the print preview UI. The page range option has these settings:

| Option name          | Action |
|----------------------|--------|
| **Print all**        | Print all pages in the document.|
| **Print Selection**  | Print only the content the user selected.|
| **Print Range**      | Display an edit control into which the user can enter the pages to print.|

First, modify the [**PrintTaskRequested**](/uwp/api/Windows.Foundation.IAsyncOperationWithProgress_TResult_TProgress_#Windows_Foundation_IAsyncOperationWithProgress_2_Progress) event handler to add the code to get a [**PrintTaskOptionDetails**](/uwp/api/Windows.Graphics.Printing.OptionDetails.PrintTaskOptionDetails) object.

```csharp
PrintTaskOptionDetails printDetailedOptions = PrintTaskOptionDetails.GetFromPrintTaskOptions(printTask.Options);
```

Clear the list of options that are shown in the print preview UI and add the options that you want to display when the user wants to print from the app.

> [!NOTE]
> The options appear in the print preview UI in the same order they are appended, with the first option shown at the top of the window.

```csharp
IList<string> displayedOptions = printDetailedOptions.DisplayedOptions;

displayedOptions.Clear();
displayedOptions.Add(Windows.Graphics.Printing.StandardPrintTaskOptions.Copies);
displayedOptions.Add(Windows.Graphics.Printing.StandardPrintTaskOptions.Orientation);
displayedOptions.Add(Windows.Graphics.Printing.StandardPrintTaskOptions.ColorMode);
```

Create the new print option and initialize the list of option values.

```csharp
// Create a new list option
PrintCustomItemListOptionDetails pageFormat = printDetailedOptions.CreateItemListOption("PageRange", "Page Range");
pageFormat.AddItem("PrintAll", "Print all");
pageFormat.AddItem("PrintSelection", "Print Selection");
pageFormat.AddItem("PrintRange", "Print Range");
```

Add your custom print option and assign the event handler. The custom option is appended last so that it appears at the bottom of the list of options. But you can put it anywhere in the list, custom print options don't need to be added last.

```csharp
// Add the custom option to the option list
displayedOptions.Add("PageRange");

// Create new edit option
PrintCustomTextOptionDetails pageRangeEdit = printDetailedOptions.CreateTextOption("PageRangeEdit", "Range");

// Register the handler for the option change event
printDetailedOptions.OptionChanged += printDetailedOptions_OptionChanged;
```

The [**CreateTextOption**](/uwp/api/windows.graphics.printing.optiondetails.printtaskoptiondetails.createtextoption) method creates the **Range** text box. This is where the user enters the specific pages they want to print when they select the **Print Range** option.

## Handle print option changes

The **OptionChanged** event handler does two things. First, it shows and hides the text edit field for the page range depending on the page range option that the user selected. Second, it tests the text entered into the page range text box to make sure that it represents a valid page range for the document.

This example shows how print option change events are handled in the [UWP print sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/Printing).

```csharp
async void printDetailedOptions_OptionChanged(PrintTaskOptionDetails sender, PrintTaskOptionChangedEventArgs args)
{
   if (args.OptionId == null)
   {
         return;
   }

   string optionId = args.OptionId.ToString();

   // Handle change in Page Range Option
   if (optionId == "PageRange")
   {
         IPrintOptionDetails pageRange = sender.Options[optionId];
         string pageRangeValue = pageRange.Value.ToString();

         selectionMode = false;

         switch (pageRangeValue)
         {
            case "PrintRange":
               // Add PageRangeEdit custom option to the option list
               sender.DisplayedOptions.Add("PageRangeEdit");
               pageRangeEditVisible = true;
               await scenarioPage.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
               {
                     ShowContent(null);
               });
               break;
            case "PrintSelection":
               await scenarioPage.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
               {
                     Scenario4PageRange page = (Scenario4PageRange)scenarioPage;
                     PageToPrint pageContent = (PageToPrint)page.PrintFrame.Content;
                     ShowContent(pageContent.TextContentBlock.SelectedText);
               });
               RemovePageRangeEdit(sender);
               break;
            default:
               await scenarioPage.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
               {
                     ShowContent(null);
               });
               RemovePageRangeEdit(sender);
               break;
         }

         Refresh();
   }
   else if (optionId == "PageRangeEdit")
   {
         IPrintOptionDetails pageRange = sender.Options[optionId];
         // Expected range format (p1,p2...)*, (p3-p9)* ...
         if (!Regex.IsMatch(pageRange.Value.ToString(), @"^\s*\d+\s*(\-\s*\d+\s*)?(\,\s*\d+\s*(\-\s*\d+\s*)?)*$"))
         {
            pageRange.ErrorText = "Invalid Page Range (eg: 1-3, 5)";
         }
         else
         {
            pageRange.ErrorText = string.Empty;
            try
            {
               GetPagesInRange(pageRange.Value.ToString());
               Refresh();
            }
            catch (InvalidPageException ipex)
            {
               pageRange.ErrorText = ipex.Message;
            }
         }
   }
}
```

> [!TIP]
> See the `GetPagesInRange` method in the [UWP print sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/Printing) for details on how to parse the page range the user enters in the Range text box.

## Preview selected pages

How you format your app's content for printing depends on the nature of your app and its content. A print helper class in used in the [UWP print sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/Printing) to format the content for printing.

When printing a subset of pages, there are several ways to show the content in the print preview. Regardless of the method you chose to show the page range in the print preview, the printed output must contain only the selected pages.

-   Show all the pages in the print preview whether a page range is specified or not, leaving the user to know which pages will actually be printed.
-   Show only the pages selected by the user's page range in the print preview, updating the display whenever the user changes the page range.
-   Show all the pages in print preview, but grey out the pages that are not in page range selected by the user.

## Related topics

* [Design guidelines for printing](./printing-and-scanning.md)
* [//Build 2015 video: Developing apps that print in Windows 10](https://channel9.msdn.com/Events/Build/2015/2-94)
* [UWP print sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/Printing)