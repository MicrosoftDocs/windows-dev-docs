---
title: 3D printing from your app
description: Learn how to add 3D printing functionality to your Universal Windows app. This topic covers how to launch the 3D print dialog after ensuring your 3D model is printable and in the correct format.
ms.assetid: D78C4867-4B44-4B58-A82F-EDA59822119C
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, 3dprinting, 3d printing
ms.localizationpriority: medium
---
# 3D printing from your app

**Important APIs**

-   [**Windows.Graphics.Printing3D**](/uwp/api/Windows.Graphics.Printing3D)

Learn how to add 3D printing functionality to your Universal Windows app. This topic covers how to load 3D geometry data into your app and launch the 3D print dialog after ensuring your 3D model is printable and in the correct format. For a working example of these procedures, see the [3D printing UWP sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/3DPrinting).

> [!NOTE]
> In the sample code in this guide, error reporting and handling is greatly simplified for the sake of simplicity.

## Setup


In your application class that is to have 3D print functionality, add the [**Windows.Graphics.Printing3D**](/uwp/api/Windows.Graphics.Printing3D) namespace.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/devices-sensors/3dprinthowto/cs/MainPage.xaml.cs" id="Snippet3DPrintNamespace":::

The following additional namespaces will be used in this guide.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/devices-sensors/3dprinthowto/cs/MainPage.xaml.cs" id="SnippetOtherNamespaces":::

Next, give your class helpful member fields. Declare a [**Print3DTask**](/uwp/api/Windows.Graphics.Printing3D.Print3DTask) object to represent the printing task that is to be passed to the print driver. Declare a [**StorageFile**](/uwp/api/Windows.Storage.StorageFile) object to hold the original 3D data file that will be loaded into the app. Declare a [**Printing3D3MFPackage**](/uwp/api/Windows.Graphics.Printing3D.Printing3D3MFPackage) object, which represents a print-ready 3D model with all necessary metadata.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/devices-sensors/3dprinthowto/cs/MainPage.xaml.cs" id="SnippetDeclareVars":::

## Create a simple UI

This sample features three user controls: a Load button which will bring a file into program memory, a Fix button which will modify the file as necessary, and a Print button which will initiate the print job. The following code creates these buttons (with their on-click event handlers) in your .cs class' corresponding XAML file.

:::code language="xml" source="~/../snippets-windows/windows-uwp/devices-sensors/3dprinthowto/cs/MainPage.xaml" id="SnippetButtons":::

Add a **TextBlock** for UI feedback.

:::code language="xml" source="~/../snippets-windows/windows-uwp/devices-sensors/3dprinthowto/cs/MainPage.xaml" id="SnippetOutputText":::



## Get the 3D data


The method by which your app acquires 3D geometry data will vary. Your app may retrieve data from a 3D scan, download model data from a web resource, or generate a 3D mesh programmatically using mathematical formulas or user input. For the sake of simplicity, this guide will show how to load a 3D data file (of any of several common file types) into program memory from device storage. The [3D Builder model library](https://developer.microsoft.com/windows/hardware/3d-print/windows-3d-printing) provides a variety of models that you can easily download to your device.

In your `OnLoadClick` method, use the [**FileOpenPicker**](/uwp/api/Windows.Storage.Pickers.FileOpenPicker) class to load a single file into your app's memory.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/devices-sensors/3dprinthowto/cs/MainPage.xaml.cs" id="SnippetFileLoad":::

## Use 3D Builder to convert to 3D Manufacturing Format (.3mf)

At this point, you are able to load a 3D data file into your app's memory. However, 3D geometry data can come in many different formats, and not all are efficient for 3D printing. Windows 10 uses the 3D Manufacturing Format (.3mf) file type for all 3D printing tasks.

> [!NOTE]  
> The .3mf file type offers more functionality than is covered in this tutorial. To learn more about 3MF and the features it provides to producers and consumers of 3D products, see the [3MF Specification](https://3mf.io/what-is-3mf/3mf-specification/). To learn how to utilize these features with Windows 10 APIs, see the [Generate a 3MF package](./generate-3mf.md) tutorial.

The [3D Builder](https://www.microsoft.com/store/apps/3d-builder/9wzdncrfj3t6) app can open files of most popular 3D formats and save them as .3mf files. In this example, where the file type could vary, a very simple solution is to open the 3D Builder app and prompt the user to save the imported data as a .3mf file and then reload it.

> [!NOTE]  
> In addition to converting file formats, 3D Builder provides simple tools to edit your models, add color data, and perform other print-specific operations, so it is often worth integrating into an app that deals with 3D printing.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/devices-sensors/3dprinthowto/cs/MainPage.xaml.cs" id="SnippetFileCheck":::

## Repair model data for 3D printing

Not all 3D model data is printable, even in the .3mf type. In order for the printer to correctly determine what space to fill and what to leave empty, the model(s) to be printed must (each) be a single seamless mesh, have outward-facing surface normals, and have manifold geometry. Issues in these areas can arise in a variety of different forms and can be hard to spot in complex shapes. However, modern software solutions are often adequate for converting raw geometry to printable 3D shapes. This is known as *repairing* the model and will be done in the `OnFixClick` method.

The 3D data file must be converted to implement [**IRandomAccessStream**](/uwp/api/Windows.Storage.Streams.IRandomAccessStream), which can then be used to generate a [**Printing3DModel**](/uwp/api/Windows.Graphics.Printing3D.Printing3DModel) object.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/devices-sensors/3dprinthowto/cs/MainPage.xaml.cs" id="SnippetRepairModel":::

The **Printing3DModel** object is now repaired and printable. Use [**SaveModelToPackageAsync**](/uwp/api/windows.graphics.printing3d.printing3d3mfpackage.savemodeltopackageasync) to assign the model to the **Printing3D3MFPackage** object that you declared when creating the class.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/devices-sensors/3dprinthowto/cs/MainPage.xaml.cs" id="SnippetSaveModel":::

## Execute printing task: create a TaskRequested handler


Later on, when the 3D print dialog is displayed to the user and the user elects to begin printing, your app will need to pass in the desired parameters to the 3D print pipeline. The 3D print API will raise the **[TaskRequested](/uwp/api/Windows.Graphics.Printing3D.Print3DManager.TaskRequested)** event. You must write a method to handle this event appropriately. As always, the handler method must be of the same type as its event: The **TaskRequested** event has parameters [**Print3DManager**](/uwp/api/Windows.Graphics.Printing3D.Print3DManager) (a reference to its sender object) and a [**Print3DTaskRequestedEventArgs**](/uwp/api/Windows.Graphics.Printing3D.Print3DTaskRequestedEventArgs) object, which holds most of the relevant information.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/devices-sensors/3dprinthowto/cs/MainPage.xaml.cs" id="SnippetMyTaskTitle":::

The core purpose of this method is to use the *args* parameter to send a **Printing3D3MFPackage** down the pipeline. The **Print3DTaskRequestedEventArgs** type has one property: [**Request**](/uwp/api/windows.graphics.printing3d.print3dtaskrequestedeventargs.request). It is of the type [**Print3DTaskRequest**](/uwp/api/Windows.Graphics.Printing3D.Print3DTaskRequest) and represents one print job request. Its method [**CreateTask**](/uwp/api/windows.graphics.printing3d.print3dtaskrequest.createtask) allows the program to submit the correct information for your print job, and it returns a reference to the **Print3DTask** object which was sent down the 3D print pipeline.

**CreateTask** has the following input parameters: a string for the print job name, a string for the ID of the printer to use, and a [**Print3DTaskSourceRequestedHandler**](/uwp/api/windows.graphics.printing3d.print3dtasksourcerequestedhandler) delegate. The delegate is automatically invoked when the **3DTaskSourceRequested** event is raised (this is done by the API itself). The important thing to note is that this delegate is invoked when a print job is initiated, and it is responsible for providing the right 3D print package.

**Print3DTaskSourceRequestedHandler** takes one parameter, a [**Print3DTaskSourceRequestedArgs**](/uwp/api/Windows.Graphics.Printing3D.Print3DTaskSourceRequestedArgs) object which provides the data to be sent. The one public method of this class, [**SetSource**](/uwp/api/windows.graphics.printing3d.print3dtasksourcerequestedargs.setsource), accepts the package to be printed. Implement a **Print3DTaskSourceRequestedHandler** delegate as follows.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/devices-sensors/3dprinthowto/cs/MainPage.xaml.cs" id="SnippetSourceHandler":::

Next, call **CreateTask**, using the newly-defined delegate, `sourceHandler`.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/devices-sensors/3dprinthowto/cs/MainPage.xaml.cs" id="SnippetCreateTask":::

The returned **Print3DTask** is assigned to the class variable declared in the beginning. You can now (optionally) use this reference to handle certain events thrown by the task.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/devices-sensors/3dprinthowto/cs/MainPage.xaml.cs" id="SnippetOptional":::

> [!NOTE]  
> You must implement a `Task_Submitting` and `Task_Completed` method if you wish to register them to these events.

## Execute printing task: open 3D print dialog


The final piece of code needed is that which launches the 3D print dialog. Like a conventional printing dialog window, the 3D print dialog provides a number of last-minute printing options and allows the user to choose which printer to use (whether connected by USB or the network).

Register your `MyTaskRequested` method with the **TaskRequested** event.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/devices-sensors/3dprinthowto/cs/MainPage.xaml.cs" id="SnippetRegisterMyTaskRequested":::

After registering your **TaskRequested** event handler, you can invoke the method [**ShowPrintUIAsync**](/uwp/api/windows.graphics.printing3d.print3dmanager.showprintuiasync), which brings up the 3D print dialog in the current application window.

:::code language="csharp" source="~/../snippets-windows/windows-uwp/devices-sensors/3dprinthowto/cs/MainPage.xaml.cs" id="SnippetShowDialog":::

Finally, it is a good practice to de-register your event handlers once your app resumes control.  

:::code language="csharp" source="~/../snippets-windows/windows-uwp/devices-sensors/3dprinthowto/cs/MainPage.xaml.cs" id="SnippetDeregisterMyTaskRequested":::

## Related topics

[Generate a 3MF package](./generate-3mf.md)  
[3D printing UWP sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/3DPrinting)
 

 
