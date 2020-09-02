---
description: This article explains how to support copy and paste in Universal Windows Platform (UWP) apps using the clipboard.
title: Copy and paste
ms.assetid: E882DC15-E12D-4420-B49D-F495BB484BEE
ms.date: 12/19/2019
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Copy and paste

This article explains how to support copy and paste in Universal Windows Platform (UWP) apps using the clipboard. Copy and paste is the classic way to exchange data either between apps, or within an app, and almost every app can support clipboard operations to some degree. For complete code examples that demonstrate several different copy and paste scenarios, see the [clipboard sample](https://github.com/microsoft/Windows-universal-samples/tree/master/Samples/Clipboard).

## Check for built-in clipboard support

In many cases, you do not need to write code to support clipboard operations. Many of the default XAML controls you can use to create apps already support clipboard operations. 

## Get set up

First, include the [**Windows.ApplicationModel.DataTransfer**](/uwp/api/Windows.ApplicationModel.DataTransfer) namespace in your app. Then, add an instance of the [**DataPackage**](/uwp/api/Windows.ApplicationModel.DataTransfer.DataPackage) object. This object contains both the data the user wants to copy and any properties (such as a description) that you want to include.

```cs
DataPackage dataPackage = new DataPackage();
```

<!-- AuthenticateAsync-->

## Copy and cut

Copy and cut (also referred to as *move*) work almost exactly the same. Choose which operation you want by using the [**RequestedOperation**](/uwp/api/windows.applicationmodel.datatransfer.datapackage.requestedoperation) property.

```cs
// copy 
dataPackage.RequestedOperation = DataPackageOperation.Copy;
// or cut
dataPackage.RequestedOperation = DataPackageOperation.Move;
```

## Set the copied content

Next, you can add the data that a user has selected to the [**DataPackage**](/uwp/api/Windows.ApplicationModel.DataTransfer.DataPackage) object. If this data is supported by the **DataPackage** class, you can use one of the corresponding [methods](/uwp/api/windows.applicationmodel.datatransfer.datapackage#methods) of the **DataPackage** object. Here's how to add text by using the [**SetText**](/uwp/api/windows.applicationmodel.datatransfer.datapackage.settext) method:

```cs
dataPackage.SetText("Hello World!");
```

The last step is to add the [**DataPackage**](/uwp/api/Windows.ApplicationModel.DataTransfer.DataPackage) to the clipboard by calling the static [**SetContent**](/uwp/api/windows.applicationmodel.datatransfer.clipboard.setcontent) method.

```cs
Clipboard.SetContent(dataPackage);
```

## Paste

To get the contents of the clipboard, call the static [**GetContent**](/uwp/api/windows.applicationmodel.datatransfer.clipboard.getcontent) method. This method returns a [**DataPackageView**](/uwp/api/Windows.ApplicationModel.DataTransfer.DataPackageView) that contains the content. This object is almost identical to a [**DataPackage**](/uwp/api/Windows.ApplicationModel.DataTransfer.DataPackage) object, except that its contents are read-only. With that object, you can use either the [**AvailableFormats**](/uwp/api/windows.applicationmodel.datatransfer.datapackageview.availableformats) or the [**Contains**](/uwp/api/windows.applicationmodel.datatransfer.datapackageview.contains) method to identify what formats are available. Then, you can call the corresponding [**DataPackageView**](/uwp/api/Windows.ApplicationModel.DataTransfer.DataPackageView) method to get the data.

```cs
async void OutputClipboardText()
{
    DataPackageView dataPackageView = Clipboard.GetContent();
    if (dataPackageView.Contains(StandardDataFormats.Text))
    {
        string text = await dataPackageView.GetTextAsync();
        // To output the text from this example, you need a TextBlock control
        TextOutput.Text = "Clipboard now contains: " + text;
    }
}
```

## Track changes to the clipboard

In addition to copy and paste commands, you may also want to track clipboard changes. Do this by handling the clipboard's [**ContentChanged**](/uwp/api/windows.applicationmodel.datatransfer.clipboard.contentchanged) event.

```cs
Clipboard.ContentChanged += async (s, e) => 
{
    DataPackageView dataPackageView = Clipboard.GetContent();
    if (dataPackageView.Contains(StandardDataFormats.Text))
    {
        string text = await dataPackageView.GetTextAsync();
        // To output the text from this example, you need a TextBlock control
        TextOutput.Text = "Clipboard now contains: " + text;
    }
}
```

## See also

* [Clipboard sample](https://github.com/microsoft/Windows-universal-samples/tree/master/Samples/Clipboard)
* [App-to-app communication](index.md)
* [DataTransfer](/uwp/api/windows.applicationmodel.datatransfer)
* [DataPackage](/uwp/api/windows.applicationmodel.datatransfer.datapackage)
* [DataPackageView](/uwp/api/windows.applicationmodel.datatransfer.datapackageview)
* [DataPackagePropertySet]( /uwp/api/Windows.ApplicationModel.DataTransfer.DataPackagePropertySet)
* [DataRequest](/uwp/api/windows.applicationmodel.datatransfer.datarequest) 
* [DataRequested]( /uwp/api/Windows.ApplicationModel.DataTransfer.DataTransferManager)
* [FailWithDisplayText](/uwp/api/windows.applicationmodel.datatransfer.datarequest.failwithdisplaytext)
* [ShowShareUi](/uwp/api/windows.applicationmodel.datatransfer.datatransfermanager.showshareui)
* [RequestedOperation](/uwp/api/windows.applicationmodel.datatransfer.datapackage.requestedoperation) 
* [ControlsList](../design/controls-and-patterns/index.md)
* [SetContent](/uwp/api/windows.applicationmodel.datatransfer.clipboard.setcontent)
* [GetContent](/uwp/api/windows.applicationmodel.datatransfer.clipboard.getcontent)
* [AvailableFormats](/uwp/api/windows.applicationmodel.datatransfer.datapackageview.availableformats)
* [Contains](/uwp/api/windows.applicationmodel.datatransfer.datapackageview.contains)
* [ContentChanged](/uwp/api/windows.applicationmodel.datatransfer.clipboard.contentchanged)