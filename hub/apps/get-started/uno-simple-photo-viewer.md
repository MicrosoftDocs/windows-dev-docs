---
title: Tutorial--Make a simple photo viewer that targets multiple platforms
description: Learn how to reach users on other platforms like Web, iOS, Android, and Linux with minimal changes to the C#/WinUI 3 simple photo viewer built in the previous tutorial. We'll use Uno Platform to create a new multi-platform app, which we can move code from the existing desktop project to.
ms.topic: article
ms.date: 05/21/2023
keywords: Windows, App, SDK, WinUI 3, WinUI, photo, viewer, Windows 11, Windows 10, XAML, C#, uno platform, uno
ms.author: aashcraft
author: alvinashcraft
ms.localizationpriority: medium
---

# Tutorial: Make a simple photo viewer that targets multiple platforms

After you've [created](/hub/apps/get-started/simple-photo-viewer-winui3.md) a starter simple photo viewer WinUI 3 app, you might be wondering how to reach more users without having to rewrite your app. This tutorial will use [Uno Platform](https://platform.uno/) to expand the reach of your existing C# WinUI 3 application enabling reuse of the business logic and UI layer across native mobile, web, and desktop. With only minimal changes to the simple photo viewer app, we'll be able to run a pixel-perfect copy of the app ported to platforms like the Web, iOS, Android, macOS, and Linux.

[Insert picture here]

## Prerequisites

- [Visual Studio 2022 17.4 or later](https://visualstudio.microsoft.com/#vs-section)
- [Tools for Windows App SDK](../windows-app-sdk/set-up-your-development-environment.md)
- ASP.NET and web development workload (for WebAssembly development)
:::image type="content" source="../images/uno/uno-vs-install-web.png" alt-text="Web development workload in VS":::
- .NET Multi-platform App UI development installed (for iOS, Android, Mac Catalyst development).
:::image type="content" source="../images/uno/uno-vs-install-dotnet-mobile.png" alt-text="dotnet mobile workload in VS":::
- .NET desktop development installed (for Gtk, Wpf, and Linux Framebuffer development)
:::image type="content" source="../how-tos/images/hello-world/vs-install-dotnet.png" alt-text=".net desktop workload in VS":::

## Finalize your environment

1. Open a command-line prompt, Windows Terminal if you have it installed, or else Command Prompt or Windows Powershell from the Start Menu.

2. Install the `uno-check` tool:
    - Use the following command:

        `dotnet tool install -g uno.check`

    - To update the tool, if you already have an existing one:

        `dotnet tool update -g uno.check`

3. Run the tool with the following command:

    `uno-check`

4. Follow the instructions indicated by the tool. Because it needs to modify your system, you may be prompted for elevated permissions.

## Install the Uno Platform solution templates

Launch Visual Studio, then click `Continue without code`. Click `Extensions` -> `Manage Extensions` from the Menu Bar.

:::image type="content" source="../how-tos/images/hello-world/manage-extensions.png" alt-text="Visual Studio Menu bar item that reads manage extensions":::

In the Extension Manager expand the **Online** node and search for `Uno`, install the `Uno Platform` extension, or download it from the [Visual Studio Marketplace](https://marketplace.visualstudio.com/items?itemName=unoplatform.uno-platform-addin-2022), then restart Visual Studio.

:::image type="content" source="../images/uno/uno-extensions.png" alt-text="Manage Extensions window in Visual Studio with Uno Platform extension as a search result":::

## Create an application

Now that we are ready to create a multi-platform application, the approach we'll take is to create a new Uno Platform application. We will copy code from the previous tutorial's **SimplePhotos** WinUI 3 project into our multi-platform project. This is possible because Uno Platform lets you reuse your existing codebase. For features dependent on OS APIs provided by each platform, you can easily make them work over time. This approach is especially useful if you have an existing application that you want to port to other platforms.

Soon enough, you will be able to reap the benefits of this approach, as you can target more platforms with a familiar XAML flavor and the codebase you already have.

Open Visual Studio and create a new project via `File` > `New` > `Project`:

:::image type="content" source="../images/uno/uno-create-project.png" alt-text="Create a new project":::

Search for Uno and select the Uno Platform App project template:

:::image type="content" source="../images/uno/uno-new-project.png" alt-text="Uno platform app":::

Specify a project name, solution name, and directory. In this example, our SimplePhotos multi-platform project belongs to a SimplePhotos multi-platform solution, which will live in C:\Projects:

:::image type="content" source="images/hello-world/configure-project.png" alt-text="Specify project details":::

Create a new C# solution using the **Uno Platform App** type from Visual Studio's **Start Page**. To avoid conflicting with the code from the previous tutorial, we'll give this solution a different name, "SimplePhotosUno".

Now you'll choose a base template to take your Hello World application multi-platform. The Uno Platform App template comes with two preset options that allow you to quickly get started with either a **Blank** solution or the **Default** configuration which includes references to the Uno.Material and Uno.Toolkit libraries. The Default configuration also includes Uno.Extensions which is used for dependency injection, configuration, navigation, and logging, and it uses MVUX in place of MVVM, making it a great starting point for rapidly building real-world applications. 

:::image type="content" source="../images/uno/uno-vsix-new-project-options.png" alt-text="Uno solution template for project startup type":::

To keep things simple, select the **Blank** preset. Then, click the **Create** button. Wait for the projects to be created and their dependencies to be restored.

A banner at the top of the editor may ask to reload projects, click **Reload projects**:
:::image type="content" source="../images/vs2022-project-reload.png" alt-text="Visual Studio banner offering to reload your projects to complete changes":::

## Preparing your app

You should see the following default file structure in your **Solution Explorer**:

:::image type="content" source="images/hello-world/uno-file-structure.png" alt-text="Default file structure":::

Now that you've generated the functional starting point of your multi-platform WinUI application, you can copy code into it from the desktop project. Because Uno Platform allows you to use the XAML flavor you're already familiar with, you can copy over the same code you created in the [previous tutorial](/hub/apps/get-started/simple-photo-viewer-winui3.md). 

To do so, go back to the **SimplePhotos** project from the previous tutorial. In the **Solution Explorer**, find the file named `MainWindow.xaml` and open it. Observe that the contents of the view are defined within a `Window` element rather than a `Page`. This is because the desktop project is a WinUI 3 application, which can use `Window` elements to define the contents of the view.

```xml
<Window x:Class="SimplePhotos.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:local="using:SimplePhotos"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        mc:Ignorable="d">

    <Grid>
        <Grid.Resources>
            <DataTemplate x:Key="ImageGridView_ItemTemplate" 
                          x:DataType="local:ImageFileInfo">
                <Grid Height="300"
                      Width="300"
                      Margin="8">
                    <Grid.RowDefinitions>
                        <RowDefinition />
                        <RowDefinition Height="Auto" />
                    </Grid.RowDefinitions>

                    <Image x:Name="ItemImage"
                           Source="Assets/StoreLogo.png"
                           Stretch="Uniform" />

                    <StackPanel Orientation="Vertical"
                                Grid.Row="1">
                        <TextBlock Text="{x:Bind ImageTitle}"
                                   HorizontalAlignment="Center"
                                   Style="{StaticResource SubtitleTextBlockStyle}" />
                        <StackPanel Orientation="Horizontal"
                                    HorizontalAlignment="Center">
                            <TextBlock Text="{x:Bind ImageFileType}"
                                       HorizontalAlignment="Center"
                                       Style="{StaticResource CaptionTextBlockStyle}" />
                            <TextBlock Text="{x:Bind ImageDimensions}"
                                       HorizontalAlignment="Center"
                                       Style="{StaticResource CaptionTextBlockStyle}"
                                       Margin="8,0,0,0" />
                        </StackPanel>

                        <RatingControl Value="{x:Bind ImageRating}" 
                                       IsReadOnly="True"/>
                    </StackPanel>
                </Grid>
            </DataTemplate>

            <Style x:Key="ImageGridView_ItemContainerStyle"
                   TargetType="GridViewItem">
                <Setter Property="Background" Value="Gray"/>
                <Setter Property="Margin" Value="8"/>
            </Style>

            <ItemsPanelTemplate x:Key="ImageGridView_ItemsPanelTemplate">
                    <ItemsWrapGrid Orientation="Horizontal"
                                   HorizontalAlignment="Center"/>
                </ItemsPanelTemplate>
        </Grid.Resources>

        <GridView x:Name="ImageGridView"
                  ItemsSource="{x:Bind Images}"
                  ItemTemplate="{StaticResource ImageGridView_ItemTemplate}"
                  ItemContainerStyle="{StaticResource ImageGridView_ItemContainerStyle}"
                  ItemsPanel="{StaticResource ImageGridView_ItemsPanelTemplate}"
                  ContainerContentChanging="ImageGridView_ContainerContentChanging" />
    </Grid>
</Window>
```

Copy the contents of the `Window` element and paste them into the `Page` element of the `MainPage.xaml` file in the **SimplePhotosUno** Uno Platform project. The `MainPage` view XAML should look like this:

```xml
<Page
    x:Class="SimplePhotosUno.MainPage"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:local="using:SimplePhotosUno"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    mc:Ignorable="d">

    <Grid>
        <Grid.Resources>
            <DataTemplate x:Key="ImageGridView_ItemTemplate"
                          x:DataType="local:ImageFileInfo">
                <Grid Height="300"
                      Width="300"
                      Margin="8">
                    <Grid.RowDefinitions>
                        <RowDefinition />
                        <RowDefinition Height="Auto" />
                    </Grid.RowDefinitions>

                    <Image x:Name="ItemImage"
                           Source="Assets/StoreLogo.png"
                           Stretch="Uniform" />

                    <StackPanel Orientation="Vertical"
                                Grid.Row="1">
                        <TextBlock Text="{x:Bind ImageTitle}"
                                   HorizontalAlignment="Center"
                                   Style="{StaticResource SubtitleTextBlockStyle}" />
                        <StackPanel Orientation="Horizontal"
                                    HorizontalAlignment="Center">
                            <TextBlock Text="{x:Bind ImageFileType}"
                                       HorizontalAlignment="Center"
                                       Style="{StaticResource CaptionTextBlockStyle}" />
                            <TextBlock Text="{x:Bind ImageDimensions}"
                                       HorizontalAlignment="Center"
                                       Style="{StaticResource CaptionTextBlockStyle}"
                                       Margin="8,0,0,0" />
                        </StackPanel>

                        <RatingControl Value="{x:Bind ImageRating}" 
                                       IsReadOnly="True"/>
                    </StackPanel>
                </Grid>
            </DataTemplate>

            <Style x:Key="ImageGridView_ItemContainerStyle"
                   TargetType="GridViewItem">
                <Setter Property="Background" 
                        Value="Gray"/>
                <Setter Property="Margin" 
                        Value="8"/>
            </Style>

            <ItemsPanelTemplate x:Key="ImageGridView_ItemsPanelTemplate">
                    <ItemsWrapGrid Orientation="Horizontal"
                                   HorizontalAlignment="Center"/>
                </ItemsPanelTemplate>
        </Grid.Resources>

        <GridView x:Name="ImageGridView"
                  ItemsSource="{x:Bind Images}"
                  ItemTemplate="{StaticResource ImageGridView_ItemTemplate}"
                  ItemContainerStyle="{StaticResource ImageGridView_ItemContainerStyle}"
                  ItemsPanel="{StaticResource ImageGridView_ItemsPanelTemplate}"
                  ContainerContentChanging="ImageGridView_ContainerContentChanging">
        </GridView>
    </Grid>
</Page>
```

Recall that the desktop application also had a `MainWindow.xaml.cs` file that contained the code-behind for the view. In the Uno Platform project, the code-behind for the `MainPage` view is contained in the `MainPage.xaml.cs` file. Copy the methods and `Images` member from the `MainWindow.xaml.cs` file and paste them into the `MainPage.xaml.cs` file. The `MainPage.xaml.cs` file should look like this:

```csharp
using Microsoft.UI.Xaml.Controls;
using System.Collections.ObjectModel;
using Windows.Storage;
using Windows.Storage.Search;

namespace SimplePhotosUno;

public sealed partial class MainPage : Page
{
    public ObservableCollection<ImageFileInfo> Images { get; } 
    = new ObservableCollection<ImageFileInfo>();

	public MainPage()
	{
		this.InitializeComponent();
        GetItemsAsync();
    }

    private void ImageGridView_ContainerContentChanging(ListViewBase sender,
        ContainerContentChangingEventArgs args)
    {
        if (args.InRecycleQueue)
        {
            var templateRoot = args.ItemContainer.ContentTemplateRoot as Grid;
            var image = templateRoot.FindName("ItemImage") as Image;
            image.Source = null;
        }

        if (args.Phase == 0)
        {
            args.RegisterUpdateCallback(ShowImage);
            args.Handled = true;
        }
    }

    private async void ShowImage(ListViewBase sender, ContainerContentChangingEventArgs args)
    {
        if (args.Phase == 1)
        {
            // It's phase 1, so show this item's image.
            var templateRoot = args.ItemContainer.ContentTemplateRoot as Grid;
            var image = templateRoot.FindName("ItemImage") as Image;
            var item = args.Item as ImageFileInfo;
            image.Source = await item.GetImageThumbnailAsync();
        }
    }

    private async Task GetItemsAsync()
    {
        StorageFolder appInstalledFolder = Package.Current.InstalledLocation;
        StorageFolder picturesFolder = await appInstalledFolder.GetFolderAsync("Assets\\Samples");

        var result = picturesFolder.CreateFileQueryWithOptions(new QueryOptions());

        IReadOnlyList<StorageFile> imageFiles = await result.GetFilesAsync();
        foreach (StorageFile file in imageFiles)
        {
            Images.Add(await LoadImageInfoAsync(file));
        }

        ImageGridView.ItemsSource = Images;
    }

    public async static Task<ImageFileInfo> LoadImageInfoAsync(StorageFile file)
    {
        var properties = await file.Properties.GetImagePropertiesAsync();
        ImageFileInfo info = new(properties,
                                    file, file.DisplayName, file.DisplayType);

        return info;
    }
}
```

The `MainPage.xaml.cs` file now contains methods from the desktop projects which we can modify in subsequent steps for multi-platform compatibility. We now need to copy the `ImageFileInfo` class from the desktop project and paste it into the `MainPage.xaml.cs` file. The `MainPage.xaml.cs` file should now look like this:

```csharp
using Microsoft.UI.Xaml.Media.Imaging;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Streams;

namespace SimplePhotosUno;

public class ImageFileInfo : INotifyPropertyChanged
{
    public ImageFileInfo(ImageProperties properties,
        StorageFile imageFile,
        string name,
        string type)
    {
        ImageProperties = properties;
        ImageName = name;
        ImageFileType = type;
        ImageFile = imageFile;
        var rating = (int)properties.Rating;
        var random = new Random();
        ImageRating = rating == 0 ? random.Next(1, 5) : rating;
    }

    public StorageFile ImageFile { get; }

    public ImageProperties ImageProperties { get; }

    public async Task<BitmapImage> GetImageSourceAsync()
    {
        using IRandomAccessStream fileStream = await ImageFile.OpenReadAsync();

        // Create a bitmap to be the image source.
        BitmapImage bitmapImage = new();
        bitmapImage.SetSource(fileStream);

        return bitmapImage;
    }

    public async Task<BitmapImage> GetImageThumbnailAsync()
    {
        StorageItemThumbnail thumbnail =
            await ImageFile.GetThumbnailAsync(ThumbnailMode.PicturesView);
        // Create a bitmap to be the image source.
        var bitmapImage = new BitmapImage();
        bitmapImage.SetSource(thumbnail);
        thumbnail.Dispose();

        return bitmapImage;
    }

    public string ImageName { get; }

    public string ImageFileType { get; }

    public string ImageDimensions => $"{ImageProperties.Width} x {ImageProperties.Height}";

    public string ImageTitle
    {
        get => string.IsNullOrEmpty(ImageProperties.Title) ? ImageName : ImageProperties.Title;
        set
        {
            if (ImageProperties.Title != value)
            {
                ImageProperties.Title = value;
                _ = ImageProperties.SavePropertiesAsync();
                OnPropertyChanged();
            }
        }
    }

    public int ImageRating
    {
        get => (int)ImageProperties.Rating;
        set
        {
            if (ImageProperties.Rating != value)
            {
                ImageProperties.Rating = (uint)value;
                _ = ImageProperties.SavePropertiesAsync();
                OnPropertyChanged();
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
```

This class will serve as a model to represent the image files in the `GridView`. In the next sections, we will make a set of changes to these copied files to make them compatible in a multi-platform context.

### Add image assets to the project

In the `SimplePhotosUno` project, create a new folder named `Assets` and copy the JPEG image files to a `Samples` subfolder. The `Assets` folder structure should now look like this:

[Insert image here]

### Making the code multi-platform capable

In the desktop project from the previous tutorial, the `MainPage.xaml.cs` file contains a `GetItemsAsync` method that enumerates items from a `StorageFolder` representing the installed package location. Because that location is not available on certain platforms such as WebAssembly, we will need to make changes to this method to make it compatible with all platforms. We will accordingly make some changes to the `ImageFileInfo` class to ensure compatibility.

First, we will make changes to the `GetItemsAsync` method. Replace the `GetItemsAsync` method in the `MainPage.xaml.cs` file with the following code:

```csharp
private async Task GetItemsAsync()
{
    foreach (int i in Enumerable.Range(1, 20))
    {
        var uri = new Uri($"ms-appx:///SimplePhotos/Assets/Samples/{i}.jpg");

        var file = await StorageFile.GetFileFromApplicationUriAsync(uri);
        Images.Add(new(file, file.Name, file.DisplayType, uri));
    }
}
```

This method now counts up to 20 and formulates a URI for each image file. The URI is then used to get the image file from the `Samples` folder. Because we're going to display an image from its URI rather than a `BitmapImage`, we can remove the other methods such as `ImageGridView_ContainerContentChanging` and `ShowImageAsync` from `MainPage.xaml.cs`. 

`ImageFileInfo` will need to be modified to accommodate this change. Replace this class file with the following code:

```csharp
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.Storage;

namespace SimplePhotosUno;

public class ImageFileInfo : INotifyPropertyChanged
{
    public ImageFileInfo(StorageFile imageFile,
        string name,
        string type,
        Uri uri)
    {
        ImageName = name;
        ImageFileType = type;
        ImageFile = imageFile;
        ImageUri = uri;
    }

    public StorageFile ImageFile { get; }

    public Uri ImageUri { get; }

    public string ImageName { get; }

    public string ImageFileType { get; }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
```

The `ImageFileInfo` class now contains a `Uri` property that will be used to load the image file. Both of the methods pertaining to getting an image source or thumbnail manually are now unneeded and can be removed. We have also omitted the `ImageDimensions` property because the `ImageProperties` type is not supported on all platforms. This `ImageFileInfo` class will be used to represent the image files in the `GridView`. We will now make changes to the `MainPage.xaml` file to accommodate these changes.

### Displaying images in GridView

In `MainPage.xaml`, replace the GridView element with the following code:

```xml
<GridView x:Name="ImageGridView"
          ItemsSource="{x:Bind Images, Mode=OneWay}"
          ItemContainerStyle="{StaticResource ImageGridView_ItemContainerStyle}"
          ItemTemplate="{StaticResource ImageGridView_ItemTemplate}"
          ItemsPanel="{StaticResource ImageGridView_ItemsPanelTemplate}"/>
```

Finally, modify the `ImageGridView_ItemTemplate` resource to use the `ImageUri` property of the `ImageFileInfo` class. We also removed elements that use the `ImageDimensions` property:

```xml
<DataTemplate x:Key="ImageGridView_ItemTemplate"
              x:DataType="local:ImageFileInfo">
    <Grid Height="300"
          Width="300"
          Margin="8">
        <Grid.RowDefinitions>
            <RowDefinition />
            <RowDefinition Height="Auto" />
        </Grid.RowDefinitions>

        <Image x:Name="ItemImage"
               Source="{x:Bind ImageUri}"
               Stretch="Uniform" />

        <StackPanel Orientation="Vertical"
                    Grid.Row="1">
            <TextBlock Text="{x:Bind ImageTitle}"
                       HorizontalAlignment="Center"
                       Style="{StaticResource SubtitleTextBlockStyle}" />
            <TextBlock Text="{x:Bind ImageFileType}"
                       HorizontalAlignment="Center"
                       Style="{StaticResource CaptionTextBlockStyle}" />
            <RatingControl Value="{x:Bind ImageRating}" 
                           IsReadOnly="True"/>
        </StackPanel>
    </Grid>
</DataTemplate>
```

### Running the app

Launch the `SimplePhotosUno.Windows` target. Observe that this WinUI app is very similar to the previous tutorial.

You can now build and run your app on any of the supported platforms. To do so, you can use the debug toolbar drop-down to select a target platform to deploy:

* To run the **WebAssembly** (Wasm) head:
    - Right-click on the `SimplePhotosUno.Wasm` project, select **Set as startup project**
    - Press the `SimplePhotosUno.Wasm` button to deploy the app
    - If desired, you can use the `SimplePhotosUno.Server` project as an alternative
* To debug for **iOS**:
    - Right-click on the `SimplePhotosUno.Mobile` project, select **Set as startup project**
    - In the debug toolbar drop-down, select an active iOS device or the simulator. You'll need to be paired with a Mac for this to work.

      :::image type="content" source="../how-tos/images/hello-world/net7-ios-debug.png" alt-text="Visual Studio dropdown to select a target framework to deploy":::

* To debug for **Mac Catalyst**: 
    - Right-click on the `SimplePhotosUno.Mobile` project, select **Set as startup project**
    - In the debug toolbar drop-down, select a remote macOS device. You'll need to be paired with one for this to work.
* To debug the **Android** platform:
    - Right-click on the `SimplePhotosUno.Mobile` project, select **Set as startup project**
    - In the debug toolbar drop-down, select either an active Android device or the emulator
        - Select an active device in the "Device" sub-menu
* To debug on **Linux** with **Skia GTK**:
    - Right-click on the `SimplePhotosUno.Skia.Gtk` project, and select **Set as startup project**
    - Press the `SimplePhotosUno.Skia.Gtk` button to deploy the app

## See also

- [Uno Platform documentation](https://platform.uno/docs/articles/intro.html)
- [Simple photo viewer tutorial](../get-started/simple-photo-viewer-winui3.md)