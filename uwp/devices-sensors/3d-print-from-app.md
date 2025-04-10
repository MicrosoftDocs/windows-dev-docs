---
title: 3D printing from your Universal Windows Platform app
description: Learn how to add 3D printing functionality to your Universal Windows Platform app.
ms.date: 05/04/2023
ms.topic: article
ms.localizationpriority: medium
---

# 3D printing from your Universal Windows Platform app

Learn how to add 3D printing functionality to your Universal Windows Platform (UWP) app.

This topic covers how to load 3D geometry data into your app and launch the 3D print dialog after ensuring your 3D model is printable and in the correct format. For a working example of these procedures, see the [3D printing UWP sample](https://github.com/Microsoft/Windows-universal-samples/tree/main/Samples/3DPrinting).

**Important APIs**

- [**Windows.Graphics.Printing3D**](/uwp/api/Windows.Graphics.Printing3D)

## Setup

Add the [**Windows.Graphics.Printing3D**](/uwp/api/Windows.Graphics.Printing3D) namespace to your application class that requires 3D print functionality.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/devices-sensors/3dprinthowto/cs/MainPage.xaml.cs" id="Snippet3DPrintNamespace":::

The following namespaces will also be used in this guide.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/devices-sensors/3dprinthowto/cs/MainPage.xaml.cs" id="SnippetOtherNamespaces":::

Next, give your class helpful member fields.

- Declare a [**Print3DTask**](/uwp/api/Windows.Graphics.Printing3D.Print3DTask) object to represent the printing task to be passed to the print driver.
- Declare a [**StorageFile**](/uwp/api/Windows.Storage.StorageFile) object to hold the original 3D data file that will be loaded into the app.
- Declare a [**Printing3D3MFPackage**](/uwp/api/Windows.Graphics.Printing3D.Printing3D3MFPackage) object to represent a print-ready 3D model with all necessary metadata.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/devices-sensors/3dprinthowto/cs/MainPage.xaml.cs" id="SnippetDeclareVars":::

## Create a simple UI

This sample uses a *Load* button to bring a file into program memory, a *Fix* button to make any needed modifications to the file, and a *Print* button to initiate the print job. The following code creates these buttons (with their on-click event handlers) in the corresponding XAML file of your .cs class.

:::code language="xml" source="~/../snippets-windows/windows-uwp/devices-sensors/3dprinthowto/cs/MainPage.xaml" id="SnippetButtons":::

The sample also includes a **TextBlock** for UI feedback.

:::code language="xml" source="~/../snippets-windows/windows-uwp/devices-sensors/3dprinthowto/cs/MainPage.xaml" id="SnippetOutputText":::

## Get the 3D data

The method by which your app acquires 3D geometry data can vary. Your app may retrieve data from a 3D scan, download model data from a web resource, or generate a 3D mesh programmatically using mathematical formulas or user input. Here, we show how to load a 3D data file (of any of several common file types) into program memory from device storage. The [3D Builder model library](https://developer.microsoft.com/windows/hardware/3d-print/windows-3d-printing) provides a variety of models for you to download.

In the `OnLoadClick` method, the [**FileOpenPicker**](/uwp/api/Windows.Storage.Pickers.FileOpenPicker) class loads a single file into app memory.

The following code shows how to load a single file into app memory using the [**FileOpenPicker**](/uwp/api/Windows.Storage.Pickers.FileOpenPicker) class in the `OnLoadClick` method.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/devices-sensors/3dprinthowto/cs/MainPage.xaml.cs" id="SnippetFileLoad":::

## Use 3D Builder to convert to 3D Manufacturing Format (.3mf)

3D geometry data can come in many different formats, and not all are efficient for 3D printing. Windows uses the 3D Manufacturing Format (.3mf) file type for all 3D printing tasks.

See the [3MF Specification](https://3mf.io/spec/) to learn more about 3MF and the supported features for producers and consumers of 3D product. To learn how to utilize these features with Windows APIs, see the [Generate a 3MF package](./3d-generate-3mf.md) tutorial.

> [!NOTE]  
> The [3D Builder](https://www.microsoft.com/store/apps/3d-builder/9wzdncrfj3t6) app can open files of most popular 3D formats and save them as .3mf files. It also provides tools to edit your models, add color data, and perform other print-specific operations.
>
> In this example, where the file type could vary, you could open the 3D Builder app and prompt the user to save the imported data as a .3mf file and then reload it.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/devices-sensors/3dprinthowto/cs/MainPage.xaml.cs" id="SnippetFileCheck":::

## Repair model data for 3D printing

Not all 3D model data is printable, even in the .3mf type. In order for the printer to correctly determine what space to fill and what to leave empty, each model to be printed must be a single seamless mesh, have outward-facing surface normals, and have manifold geometry. Issues in these areas can arise in a variety of different forms and can be hard to spot in complex shapes. However, modern software solutions are often adequate for converting raw geometry to printable 3D shapes. This is known as *repairing* the model and is implemented in the `OnFixClick` method shown here.

> [!NOTE]
> The 3D data file must be converted to implement [**IRandomAccessStream**](/uwp/api/Windows.Storage.Streams.IRandomAccessStream), which can then be used to generate a [**Printing3DModel**](/uwp/api/Windows.Graphics.Printing3D.Printing3DModel) object.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/devices-sensors/3dprinthowto/cs/MainPage.xaml.cs" id="SnippetRepairModel":::

The **Printing3DModel** object should now be repaired and printable. Use [**SaveModelToPackageAsync**](/uwp/api/windows.graphics.printing3d.printing3d3mfpackage.savemodeltopackageasync) to assign the model to the **Printing3D3MFPackage** object that you declared when creating the class.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/devices-sensors/3dprinthowto/cs/MainPage.xaml.cs" id="SnippetSaveModel":::

## Execute printing task: create a TaskRequested handler

Later, when the 3D print dialog is displayed to the user and the user elects to begin printing, your app will need to pass in the desired parameters to the 3D print pipeline. The 3D print API will raise the [**TaskRequested**](/uwp/api/Windows.Graphics.Printing3D.Print3DManager.TaskRequested) event, which requires handling appropriately.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/devices-sensors/3dprinthowto/cs/MainPage.xaml.cs" id="SnippetMyTaskTitle":::

The core purpose of this method is to use the *args* parameter to send a **Printing3D3MFPackage** down the pipeline. The **Print3DTaskRequestedEventArgs** type has one property: [**Request**](/uwp/api/windows.graphics.printing3d.print3dtaskrequestedeventargs.request). It is of the type [**Print3DTaskRequest**](/uwp/api/Windows.Graphics.Printing3D.Print3DTaskRequest) and represents one print job request. Its method [**CreateTask**](/uwp/api/windows.graphics.printing3d.print3dtaskrequest.createtask) lets the app submit the correct information for your print job and return a reference to the **Print3DTask** object that was sent down the 3D print pipeline.

**CreateTask** has the following input parameters: a string for the print job name, a string for the ID of the printer to use, and a [**Print3DTaskSourceRequestedHandler**](/uwp/api/windows.graphics.printing3d.print3dtasksourcerequestedhandler) delegate. The delegate is automatically invoked when the **3DTaskSourceRequested** event is raised (this is done by the API itself). The important thing to note is that this delegate is invoked when a print job is initiated, and it is responsible for providing the right 3D print package.

**Print3DTaskSourceRequestedHandler** takes one parameter, a [**Print3DTaskSourceRequestedArgs**](/uwp/api/Windows.Graphics.Printing3D.Print3DTaskSourceRequestedArgs) object, which contains the data to be sent. The [**SetSource**](/uwp/api/windows.graphics.printing3d.print3dtasksourcerequestedargs.setsource) method accepts the package to be printed. The following code shows a **Print3DTaskSourceRequestedHandler** delegate implementation (`sourceHandler`).

:::code language="csharp" source="~/../snippets-windows/windows-uwp/devices-sensors/3dprinthowto/cs/MainPage.xaml.cs" id="SnippetSourceHandler":::

Next, call **CreateTask**, using the newly-defined delegate.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/devices-sensors/3dprinthowto/cs/MainPage.xaml.cs" id="SnippetCreateTask":::

The **Print3DTask** returned is assigned to the class variable declared in the beginning. This reference can be used to handle certain events thrown by the task.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/devices-sensors/3dprinthowto/cs/MainPage.xaml.cs" id="SnippetOptional":::

> [!NOTE]  
> You must implement a `Task_Submitting` and `Task_Completed` method if you wish to register them to these events.

## Execute printing task: open 3D print dialog

Finally, you need to launch the 3D print dialog that provides a number of printing options.

Here, we register a `MyTaskRequested` method with the **TaskRequested** event.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/devices-sensors/3dprinthowto/cs/MainPage.xaml.cs" id="SnippetRegisterMyTaskRequested":::

After registering the **TaskRequested** event handler, you can invoke the method [**ShowPrintUIAsync**](/uwp/api/windows.graphics.printing3d.print3dmanager.showprintuiasync), which brings up the 3D print dialog in the current application window.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/devices-sensors/3dprinthowto/cs/MainPage.xaml.cs" id="SnippetShowDialog":::

It is also good practice to de-register your event handlers once your app resumes control.  

:::code language="csharp" source="~/../snippets-windows/windows-uwp/devices-sensors/3dprinthowto/cs/MainPage.xaml.cs" id="SnippetDeregisterMyTaskRequested":::

## Related topics

[3D printing with Windows 10](https://www.microsoft.com/3d-print/windows-3d-printing)
[Generate a 3MF package](./3d-generate-3mf.md)  
[3D printing UWP sample](https://github.com/Microsoft/Windows-universal-samples/tree/main/Samples/3DPrinting)
