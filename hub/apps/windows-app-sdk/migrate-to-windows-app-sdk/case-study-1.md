---
title: A Windows App SDK migration of the UWP PhotoLab sample app (C#)
description: A case study of taking the C# [UWP PhotoLab sample app](/samples/microsoft/windows-appsample-photo-lab/photolab-sample/), and migrating it to the Windows App SDK.
ms.topic: article
ms.date: 10/01/2021
keywords: Windows, App, SDK, migrate, migrating, migration, port, porting, C#, PhotoLab, UWP
ms.author: stwhi
author: stevewhims
ms.localizationpriority: medium
---

# A Windows App SDK migration of the UWP PhotoLab sample app (C#)

This topic is a case study of taking the C# [UWP PhotoLab sample app](/samples/microsoft/windows-appsample-photo-lab/photolab-sample/), and migrating it to the Windows App SDK.

Begin by [cloning the UWP sample app's repo](https://github.com/microsoft/windows-appsample-photo-lab/tree/master/), and opening the solution in [Visual Studio](https://visualstudio.microsoft.com/downloads/).

> [!IMPORTANT]
> For considerations and strategies for approaching the migration process, and how to set up your development environment for migrating, see [Overall migration strategy](overall-migration-strategy.md). It's particularly important to see [What's supported when porting from UWP to WinUI 3](what-is-supported.md) so that you can ensure that all the features you need for your app are supported before you attempt migration.

## Install tools for the Windows App SDK

To set up your development computer, see [Install tools for the Windows App SDK](../set-up-your-development-environment.md).

> [!IMPORTANT]
> You'll find release notes topics along with the [Windows App SDK release channels](../release-channels.md) topic. There are release notes for each channel. Be sure to check any *limitations and known issues* in those release notes, since those might affect the results of following along with this case study and/or running the migrated app.

## Create a new project

In Visual Studio, create a new C# project from the **Blank App, Packaged (WinUI 3 in Desktop)** project template. Name the project *PhotoLabWinUI*, uncheck **Place solution and project in the same directory**. You can target the most recent release (not preview) of the client operating system.

> [!NOTE]
> We'll be referring to the UWP version of the sample project (the one that you cloned from its [repo](https://github.com/microsoft/windows-appsample-photo-lab/tree/master/)) as the *source* solution/project. We'll be referring to the Windows App SDK version as the *target* solution/project.

## The order in which we'll migrate the code

**MainPage** is an important and prominent piece of the app. But if we were to begin by migrating that, then we'd soon realize that **MainPage** has a dependency on the **DetailPage** view; and then that **DetailPage** has a dependency on the **ImageFileInfo** model. So for this walkthrough we'll take this approach.

* We'll begin by copying over the asset files.
* Then we'll migrate the **ImageFileInfo** model.
* Next we'll migrate the **App** class (since that needs changes making to it that **DetailPage**, **MainPage**, and **LoadedImageBrush** will depend on).
* Then we'll migrate the **LoadedImageBrush** class.
* Then we'll begin migrating the views, starting with **DetailPage** first.
* And we'll finish up by migrating the **MainPage** view.

## Copy asset files

1. In your target project in Visual Studio, in **Solution Explorer**, right-click the **Assets** folder, and add a new folder named `Samples`.

2. In your clone of the source project, in **File Explorer**, locate the folder **Windows-appsample-photo-lab** > **PhotoLab** > **Assets**. You'll find seven asset files in that folder, together with a subfolder named **Samples** containing sample images. Select those seven asset files, and the **Samples** subfolder, and copy them to the clipboard.

3. Also in **File Explorer**, now locate the corresponding folder in the target project that you created. The path to that folder is **PhotoLabWinUI** > **PhotoLabWinUI** > **Assets**. Paste into that folder the asset files and the subfolder that you just copied, and accept the prompt to replace any files that already exist in the destination.

4. In your target project in Visual Studio, in **Solution Explorer**, with the **Assets** folder expanded, you'll see in the **Samples** folder the contents of the **Samples** subfolder (which you just pasted). You can hover the mouse pointer over the asset files. A thumbnail preview will appear for each, confirming that you've replaced/added the asset files correctly.

## Migrate the ImageFileInfo model

**ImageFileInfo** is a *model* (in the sense of models, views, and viewmodels) that represents an image file, such as a photo.

### Copy ImageFileInfo source code files

1. In your clone of the source project, in **File Explorer**, locate the folder **Windows-appsample-photo-lab** > **PhotoLab**. In that folder you'll find the source code file `ImageFileInfo.cs`; that file contains the implementation of **ImageFileInfo**. Select that file, and copy it to the clipboard.

2. In Visual Studio, right-click the target project node, and click **Open Folder in File Explorer**. This opens the target project folder in **File Explorer**. Paste into that folder the file that you just copied.

### Migrate ImageFileInfo source code

1. Make the following find/replacements (match case and whole word) in the `ImageFileInfo.cs` file that you just pasted.

* `namespace PhotoLab` => `namespace PhotoLabWinUI`
* `Windows.UI.Xaml` => `Microsoft.UI.Xaml`

**Windows.UI.Xaml** is the namespace for UWP XAML; **Microsoft.UI.Xaml** is the namespace for WinUI XAML.

> [!NOTE]
> The [Mapping UWP APIs to the Windows App SDK](api-mapping-table.md) topic provides a mapping of UWP APIs to their Windows App SDK equivalents. The change we made above is an example of a namespace name change necessary during the migration process.

2. Now confirm that you can build the target solution (but don't run yet).

## Migrate the **App** class

1. From the source project, in the `<Application.Resources>` element in `App.xaml`, find the following four lines. Copy them, and paste them into the target project.

```xaml
<SolidColorBrush x:Key="RatingControlSelectedForeground" Color="White"/>
<!--  Window width adaptive breakpoints.  -->
<x:Double x:Key="MinWindowBreakpoint">0</x:Double>
<x:Double x:Key="MediumWindowBreakpoint">641</x:Double>
<x:Double x:Key="LargeWindowBreakpoint">1008</x:Double>
```

> [!NOTE]
> Because the target project will use different (and simpler) navigation from the source project, there's no need to copy over any further code from the source project's `App.xaml.cs`.

2. In the target project, **App** stores the main window object in its private field *m_window*. Later in the migration process (when we migrate the source project's use of **Window.Current**), it'll be convenient if that private field is instead a public static property. So replace the *m_window* field with a *Window* property, and change references to *m_window*, as shown below.

```csharp
// App.xaml.cs
public partial class App : Application
{
    ...
    protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
    {
        Window = new MainWindow();
        Window.Activate();
    }

    public static MainWindow Window { get; private set; }
}
```

3. Later in the migration process (when we migrate the code that displays a [**FileSavePicker**](/uwp/api/windows.storage.pickers.filesavepicker)), it'll be convenient if **App** exposes the main window's *handle* (**HWND**). So add a *WindowHandle* property, and initialize it in the **OnLaunched** method, as shown below.

```csharp
// App.xaml.cs
public partial class App : Application
{
    ...
    protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
    {
        Window = new MainWindow();
        Window.Activate();
        WindowHandle = WinRT.Interop.WindowNative.GetWindowHandle(Window);
    }

    public static IntPtr WindowHandle { get; private set; }
}
```

## Migrate the LoadedImageBrush model

**LoadedImageBrush** is a specialization of [**XamlCompositionBrushBase**](/uwp/api/windows.ui.xaml.media.xamlcompositionbrushbase). The *PhotoLab* sample app uses the **LoadedImageBrush** class to apply effects to photos.

### Reference the Win2D NuGet package

To support code in **LoadedImageBrush**, the source project has a dependency on [Win2D](https://microsoft.github.io/Win2D/WinUI3/html/Introduction.htm). So we'll also need a dependency on Win2D in our target project.

In the target solution in Visual Studio, click **Tools** > **NuGet Package Manager** > **Manage NuGet Packages for Solution...** > **Browse**, and type or paste *Microsoft.Graphics.Win2D*. Select the correct item in search results, check the *PhotoLabWinUI* project, and click **Install** to install the package into that project.

### Copy LoadedImageBrush source code files

Copy `LoadedImageBrush.cs` from the source project to the target project in the same way that you copied `ImageFileInfo.cs`.

### Migrate LoadedImageBrush source code

1. Make the following find/replacements (match case and whole word) in the `LoadedImageBrush.cs` file that you just pasted.

* `namespace PhotoLab` => `namespace PhotoLabWinUI`
* `Windows.UI.Composition` => `Microsoft.UI.Composition`
* `Windows.UI.Xaml` => `Microsoft.UI.Xaml`
* `Window.Current.Compositor` => `App.Window.Compositor` (see [Change Windows.UI.Xaml.Window.Current to App.Window](guides/winui3.md#change-windowsuixamlwindowcurrent-to-appwindow))

2. Confirm that you can build the target solution (but don't run yet).

## Migrate the DetailPage view

**DetailPage** is the class that represents the photo editor page, where Win2D effects are toggled, set, and chained together. You get to the photo editor page by selecting a photo thumbnail on **MainPage**. **DetailPage** is a *model* (in the sense of models, views, and viewmodels).

### Copy DetailPage source code files

Copy `DetailPage.xaml` and `DetailPage.xaml.cs` from the source project to the target project in the same way that you copied files in previous steps.

### Migrate DetailPage source code

1. Make the following find/replacements (match case and whole word) in the `DetailPage.xaml` file that you just pasted.

* `PhotoLab` => `PhotoLabWinUI`

2. Make the following find/replacements (match case and whole word) in the `DetailPage.xaml.cs` file that you just pasted.

* `namespace PhotoLab` => `namespace PhotoLabWinUI`
* `Windows.UI.Colors` => `Microsoft.UI.Colors`
* `Windows.UI.Xaml` => `Microsoft.UI.Xaml`

3. For the next step, we'll make the change that's explained in [ContentDialog, and Popup](guides/winui3.md#contentdialog-and-popup). So, still in `DetailPage.xaml.cs`, in the **ShowSaveDialog** method, immediately *before* the line `ContentDialogResult result = await saveDialog.ShowAsync();`, add this code.

```cppwinrt
if (Windows.Foundation.Metadata.ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 8))
{
    saveDialog.XamlRoot = this.Content.XamlRoot;
}
```

4. Still in `DetailPage.xaml.cs`, in the **OnNavigatedTo** method, delete the following two lines of code. Just those two lines; later in this case study, we'll reintroduce the back button functionality that we just removed.

```csharp
...
SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
    AppViewBackButtonVisibility.Visible;
...
SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = 
    AppViewBackButtonVisibility.Collapsed;
...
```

5. For this step, we'll make the change that's explained in [MessageDialog, and Pickers](guides/winui3.md#messagedialog-and-pickers). Still in `DetailPage.xaml.cs`, in the **ExportImage** method, immediately *before* the line `var outputFile = await fileSavePicker.PickSaveFileAsync();`, add this line of code.

```csharp
WinRT.Interop.InitializeWithWindow.Initialize(fileSavePicker, App.WindowHandle);
```

**MainPage** has dependencies on **DetailPage**, which is why we migrated **DetailPage** first. But **DetailPage** has dependencies on **MainPage**, too, so we won't be able to build yet.

## Migrate the MainPage view

The main page of the app represents the view that you see first when you run the app. It's the page that loads the photos from the **Samples** folder that's built into the sample app, and displays a tiled thumbnail view.

### Copy MainPage source code files

Copy `MainPage.xaml` and `MainPage.xaml.cs` from the source project to the target project in the same way that you copied files in previous steps.

### Migrate MainPage source code

1. Make the following find/replacements (match case and whole word) in the `MainPage.xaml` file that you just pasted.

* `PhotoLab` => `PhotoLabWinUI`

2. Still in `MainPage.xaml`, find the markup `animations:ReorderGridAnimation.Duration="400"`, and delete that.

3. Make the following find/replacements (match case and whole word) in the `MainPage.xaml.cs` file that you just pasted.

* `namespace PhotoLab` => `namespace PhotoLabWinUI`
* `Windows.UI.Xaml` => `Microsoft.UI.Xaml`

4. For this step, we'll make the change that's explained in [ContentDialog, and Popup](guides/winui3.md#contentdialog-and-popup). So, still in `MainPage.xaml.cs`, in the **GetItemsAsync** method, immediately *before* the line `ContentDialogResult resultNotUsed = await unsupportedFilesDialog.ShowAsync();`, add this code.

```cppwinrt
if (Windows.Foundation.Metadata.ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 8))
{
    unsupportedFilesDialog.XamlRoot = this.Content.XamlRoot;
}
```

5. Still in `MainPage.xaml.cs`, in the **OnNavigatedTo** method, delete the following line of code.

```csharp
SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
    AppViewBackButtonVisibility.Collapsed;
```

Later in this case study, we'll reintroduce the back button functionality that we just removed.

6. Confirm that you can build the target solution (but don't run yet).

## Navigate to MainPage

The *PhotoLab* sample app uses navigation logic to navigate initially to **MainPage** (and then between **MainPage** and **DetailPage**). For more info about Windows App SDK apps that need navigation (and those that don't), see [Do I need to implement page navigation?](guides/winui3.md#do-i-need-to-implement-page-navigation).

So the changes we'll make next support that navigation.

1. In `MainWindow.xaml`, delete the `<StackPanel>` element, and replace it with just a named `<Frame>` element. The result looks like this:

```xaml
<Window ...>
    <Frame x:Name="rootFrame"/>
</Window>
```

2. In `MainWindow.xaml.cs`, delete the **myButton_Click** method.

3. Still in `MainWindow.xaml.cs`, add the following line of code to the constructor.

```csharp
public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        this.InitializeComponent();
        rootFrame.Navigate(typeof(MainPage));
    }
}
```

4. Confirm that you can build the target solution (but don't run yet).

## Restoring back button functionality

1. In `DetailPage.xaml`, the root element is a **RelativePanel**. Add the following markup inside that **RelativePanel**, immediately after the **StackPanel** element.

```xaml
<AppBarButton x:Name="BackButton" Click="BackButton_Click" Margin="0,0,12,0">
    <SymbolIcon Symbol="Back"/>
</AppBarButton>
```

2. In `DetailPage.xaml.cs`, add the following two lines of code to the **OnNavigatedTo** method, in the place indicated.

```csharp
if (this.Frame.CanGoBack)
{
    BackButton.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
}
else
{
    BackButton.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
}
```

3. Still in `DetailPage.xaml.cs`, add the following event handler.

```csharp
private void BackButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
{
    Frame.GoBack();
}
```

## Test the migrated app

Now build the project, and run the app to test it. Select an image, set a zoom level, choose effects, and configure them.