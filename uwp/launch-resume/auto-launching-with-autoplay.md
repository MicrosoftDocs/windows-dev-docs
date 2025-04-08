---
title: Auto-launching with AutoPlay
description: You can use AutoPlay to provide your app as an option when a user connects a device to their PC. This includes non-volume devices such as a camera or media player, or volume devices such as a USB thumb drive, SD card, or DVD.
ms.assetid: AD4439EA-00B0-4543-887F-2C1D47408EA7
ms.date: 08/25/2022
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# <span id="dev_launch_resume.auto-launching_with_autoplay"></span>Auto-launching with AutoPlay

You can use **AutoPlay** to provide your app as an option when a user connects a device to their PC. This includes non-volume devices such as a camera or media player, or volume devices such as a USB thumb drive, SD card, or DVD. You can also use **AutoPlay** to offer your app as an option when users share files between two PCs by using proximity (tapping).

> [!NOTE]
> If you are a device manufacturer and you want to associate your [Microsoft Store device app](/windows-hardware/drivers/devapps/) as an **AutoPlay** handler for your device, you can identify that app in the device metadata. For more info, see [AutoPlay for Microsoft Store device apps](/windows-hardware/drivers/devapps/autoplay-for-uwp-device-apps).

## Register for AutoPlay content

You can register apps as options for **AutoPlay** content events. **AutoPlay** content events are raised when a volume device such as a camera memory card, thumb drive, or DVD is inserted into the PC. Here we show how to identify your app as an **AutoPlay** option when a volume device from a camera is inserted.

In this tutorial, you created an app that displays image files or copies them to Pictures. You registered the app for the AutoPlay **ShowPicturesOnArrival** content event.

AutoPlay also raises content events for content shared between PCs using proximity (tapping). You can use the steps and code in this section to handle files that are shared between PCs that use proximity. The following table lists the AutoPlay content events that are available for sharing content by using proximity.

| Action         | AutoPlay content event  |
|----------------|-------------------------|
| Sharing music  | PlayMusicFilesOnArrival |
| Sharing videos | PlayVideoFilesOnArrival |

When files are shared by using proximity, the **Files** property of the **FileActivatedEventArgs** object contains a reference to a root folder that contains all of the shared files.

### Step 1: Create a new project and add AutoPlay declarations

1. Open Microsoft Visual Studio and select **New Project** from the **File** menu. In the **Visual C#** section, under **Windows**, select **Blank App (Universal Windows)**. Name the app **AutoPlayDisplayOrCopyImages** and click **OK.**
2. Open the Package.appxmanifest file and select the **Capabilities** tab. Select the **Removable Storage** and **Pictures Library** capabilities. This gives the app access to removable storage devices for camera memory, and access to local pictures.
3. In the manifest file, select the **Declarations** tab. In the **Available Declarations** drop-down list, select **AutoPlay Content** and click **Add**. Select the new **AutoPlay Content** item that was added to the **Supported Declarations** list.
4. An **AutoPlay Content** declaration identifies your app as an option when AutoPlay raises a content event. The event is based on the content of a volume device such as a DVD or a thumb drive. AutoPlay examines the content of the volume device and determines which content event to raise. If the root of the volume contains a DCIM, AVCHD, or PRIVATE\\ACHD folder, or if a user has enabled **Choose what to do with each type of media** in the AutoPlay Control Panel and pictures are found in the root of the volume, then AutoPlay raises the **ShowPicturesOnArrival** event. In the **Launch Actions** section, enter the values from Table 1 below for the first launch action.
5. In the **Launch Actions** section for the **AutoPlay Content** item, click **Add New** to add a second launch action. Enter the values in Table 2 below for the second launch action.
6. In the **Available Declarations** drop-down list, select **File Type Associations** and click **Add**. In the Properties of the new **File Type Associations** declaration, set the **Display Name** field to **AutoPlay Copy or Show Images** and the **Name** field to **image\_association1**. In the **Supported File Types** section, click **Add New**. Set the **File Type** field to **.jpg**. In the **Supported File Types** section, set the **File Type** field of the new file association to **.png**. For content events, AutoPlay filters out any file types that are not explicitly associated with your app.
7. Save and close the manifest file.

**Table 1**

| Setting             | Value                 |
|---------------------|-----------------------|
| Verb                | show                  |
| Action Display Name | Show Pictures         |
| Content Event       | ShowPicturesOnArrival |

The **Action Display Name** setting identifies the string that AutoPlay displays for your app. The **Verb** setting identifies a value that is passed to your app for the selected option. You can specify multiple launch actions for an AutoPlay event and use the **Verb** setting to determine which option a user has selected for your app. You can tell which option the user selected by checking the **verb** property of the startup event arguments passed to your app. You can use any value for the **Verb** setting except, **open**, which is reserved.

**Table 2**

| Setting             | Value                      |
|--------------------:|----------------------------|
| Verb                | copy                       |
| Action Display Name | Copy Pictures Into Library |
| Content Event       | ShowPicturesOnArrival      |

### Step 2: Add XAML UI

Open the MainPage.xaml file and add the following XAML to the default &lt;Grid&gt; section.

``` xaml
<TextBlock FontSize="18">File List</TextBlock>
<TextBlock x:Name="FilesBlock" HorizontalAlignment="Left" TextWrapping="Wrap"
           VerticalAlignment="Top" Margin="0,20,0,0" Height="280" Width="240" />
<Canvas x:Name="FilesCanvas" HorizontalAlignment="Left" VerticalAlignment="Top"
        Margin="260,20,0,0" Height="280" Width="100"/>
```

### Step 3: Add initialization code

The code in this step checks the verb value in the **Verb** property, which is one of the startup arguments passed to the app during the **OnFileActivated** event. The code then calls a method related to the option that the user selected. For the camera memory event, AutoPlay passes the root folder of the camera storage to the app. You can retrieve this folder from the first element of the **Files** property.

Open the App.xaml.cs file and add the following code to the **App** class.

``` cs
protected override void OnFileActivated(FileActivatedEventArgs args)
{
    if (args.Verb == "show")
    {
        Frame rootFrame = (Frame)Window.Current.Content;
        MainPage page = (MainPage)rootFrame.Content;

        // Call DisplayImages with root folder from camera storage.
        page.DisplayImages((Windows.Storage.StorageFolder)args.Files[0]);
    }

    if (args.Verb == "copy")
    {
        Frame rootFrame = (Frame)Window.Current.Content;
        MainPage page = (MainPage)rootFrame.Content;

        // Call CopyImages with root folder from camera storage.
        page.CopyImages((Windows.Storage.StorageFolder)args.Files[0]);
    }

    base.OnFileActivated(args);
}
```

> **Note**  The `DisplayImages` and `CopyImages` methods are added in the following steps.

### Step 4: Add code to display images

In the MainPage.xaml.cs file add the following code to the **MainPage** class.

``` cs
async internal void DisplayImages(Windows.Storage.StorageFolder rootFolder)
{
    // Display images from first folder in root\DCIM.
    var dcimFolder = await rootFolder.GetFolderAsync("DCIM");
    var folderList = await dcimFolder.GetFoldersAsync();
    var cameraFolder = folderList[0];
    var fileList = await cameraFolder.GetFilesAsync();
    for (int i = 0; i < fileList.Count; i++)
    {
        var file = (Windows.Storage.StorageFile)fileList[i];
        WriteMessageText(file.Name + "\n");
        DisplayImage(file, i);
    }
}

async private void DisplayImage(Windows.Storage.IStorageItem file, int index)
{
    try
    {
        var sFile = (Windows.Storage.StorageFile)file;
        Windows.Storage.Streams.IRandomAccessStream imageStream =
            await sFile.OpenAsync(Windows.Storage.FileAccessMode.Read);
        Windows.UI.Xaml.Media.Imaging.BitmapImage imageBitmap =
            new Windows.UI.Xaml.Media.Imaging.BitmapImage();
        imageBitmap.SetSource(imageStream);
        var element = new Image();
        element.Source = imageBitmap;
        element.Height = 100;
        Thickness margin = new Thickness();
        margin.Top = index * 100;
        element.Margin = margin;
        FilesCanvas.Children.Add(element);
    }
    catch (Exception e)
    {
       WriteMessageText(e.Message + "\n");
    }
}

// Write a message to MessageBlock on the UI thread.
private Windows.UI.Core.CoreDispatcher messageDispatcher = Window.Current.CoreWindow.Dispatcher;

private async void WriteMessageText(string message, bool overwrite = false)
{
    await messageDispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
        () =>
        {
            if (overwrite)
                FilesBlock.Text = message;
            else
                FilesBlock.Text += message;
        });
}
```

### Step 5: Add code to copy images

In the MainPage.xaml.cs file add the following code to the **MainPage** class.

``` cs
async internal void CopyImages(Windows.Storage.StorageFolder rootFolder)
{
    // Copy images from first folder in root\DCIM.
    var dcimFolder = await rootFolder.GetFolderAsync("DCIM");
    var folderList = await dcimFolder.GetFoldersAsync();
    var cameraFolder = folderList[0];
    var fileList = await cameraFolder.GetFilesAsync();

    try
    {
        var folderName = "Images " + DateTime.Now.ToString("yyyy-MM-dd HHmmss");
        Windows.Storage.StorageFolder imageFolder = await
            Windows.Storage.KnownFolders.PicturesLibrary.CreateFolderAsync(folderName);

        foreach (Windows.Storage.IStorageItem file in fileList)
        {
            CopyImage(file, imageFolder);
        }
    }
    catch (Exception e)
    {
        WriteMessageText("Failed to copy images.\n" + e.Message + "\n");
    }
}

async internal void CopyImage(Windows.Storage.IStorageItem file,
                              Windows.Storage.StorageFolder imageFolder)
{
    try
    {
        Windows.Storage.StorageFile sFile = (Windows.Storage.StorageFile)file;
        await sFile.CopyAsync(imageFolder, sFile.Name);
        WriteMessageText(sFile.Name + " copied.\n");
    }
    catch (Exception e)
    {
        WriteMessageText("Failed to copy file.\n" + e.Message + "\n");
    }
}
```

### Step 6: Build and run the app

1. Press F5 to build and deploy the app (in debug mode).
2. To run your app, insert a camera memory card or another storage device from a camera into your PC. Then, select one of the content event options that you specified in your package.appxmanifest file from the AutoPlay list of options. This sample code only displays or copies pictures in the DCIM folder of a camera memory card. If your camera memory card stores pictures in an AVCHD or PRIVATE\\ACHD folder, you will need to update the code accordingly.

> [!NOTE]
> If you don't have a camera memory card, you can use a flash drive if it has a folder named **DCIM** in the root and if the DCIM folder has a subfolder that contains images.

## Register for an AutoPlay device

You can register apps as options for **AutoPlay** device events. **AutoPlay** device events are raised when a device is connected to a PC.

Here we show how to identify your app as an **AutoPlay** option when a camera is connected to a PC. The app registers as a handler for the **WPD\\ImageSourceAutoPlay** event. This is a common event that the Windows Portable Device (WPD) system raises when cameras and other imaging devices notify it that they are an ImageSource using MTP. For more info, see [Windows Portable Devices](/previous-versions/ff597729(v=vs.85)).

**Important**  The [**Windows.Devices.Portable.StorageDevice**](/uwp/api/Windows.Devices.Portable.StorageDevice) APIs are part of the [desktop device family](../get-started/universal-application-platform-guide.md). Apps can use these APIs only on Windows 10 devices in the desktop device family, such as PCs.

### Step 1: Create another new project and add AutoPlay declarations

1. Open Visual Studio and select **New Project** from the **File** menu. In the **Visual C#** section, under **Windows**, select **Blank App (Universal Windows)**. Name the app **AutoPlayDevice\_Camera** and click **OK.**
2. Open the Package.appxmanifest file and select the **Capabilities** tab. Select the **Removable Storage** capability. This gives the app access to the data on the camera as a removable storage volume device.
3. In the manifest file, select the **Declarations** tab. In the **Available Declarations** drop-down list, select **AutoPlay Device** and click **Add**. Select the new **AutoPlay Device** item that was added to the **Supported Declarations** list.
4. An **AutoPlay Device** declaration identifies your app as an option when AutoPlay raises a device event for known events. In the **Launch Actions** section, enter the values in the table below for the first launch action.
5. In the **Available Declarations** drop-down list, select **File Type Associations** and click **Add**. In the Properties of the new **File Type Associations** declaration, set the **Display Name** field to **Show Images from Camera** and the **Name** field to **camera\_association1**. In the **Supported File Types** section, click **Add New** (if needed). Set the **File Type** field to **.jpg**. In the **Supported File Types** section, click **Add New** again. Set the **File Type** field of the new file association to **.png**. For content events, AutoPlay filters out any file types that are not explicitly associated with your app.
6. Save and close the manifest file.

| Setting             | Value            |
|---------------------|------------------|
| Verb                | show             |
| Action Display Name | Show Pictures    |
| Content Event       | WPD\\ImageSource |

The **Action Display Name** setting identifies the string that AutoPlay displays for your app. The **Verb** setting identifies a value that is passed to your app for the selected option. You can specify multiple launch actions for an AutoPlay event and use the **Verb** setting to determine which option a user has selected for your app. You can tell which option the user selected by checking the **verb** property of the startup event arguments passed to your app. You can use any value for the **Verb** setting except, **open**, which is reserved. For an example of using multiple verbs in a single app, see [Register for AutoPlay content](#register-for-autoplay-content).

### Step 2: Add assembly reference for the desktop extensions

The APIs required to access storage on a Windows Portable Device, [**Windows.Devices.Portable.StorageDevice**](/uwp/api/Windows.Devices.Portable.StorageDevice), are part of the [desktop device family](../get-started/universal-application-platform-guide.md). This means a special assembly is required to use the APIs and those calls will only work on a device in the desktop device family (such as a PC).

1. In **Solution Explorer**, right click on **References** and then **Add Reference...**.
2. Expand **Universal Windows** and click **Extensions**.
3. Then select **Windows Desktop Extensions for the UWP** and click **OK**.

### Step 3: Add XAML UI

Open the MainPage.xaml file and add the following XAML to the default &lt;Grid&gt; section.

``` xaml
<StackPanel Orientation="Vertical" Margin="10,0,-10,0">
    <TextBlock FontSize="24">Device Information</TextBlock>
    <StackPanel Orientation="Horizontal">
        <TextBlock x:Name="DeviceInfoTextBlock" FontSize="18" Height="400" Width="400" VerticalAlignment="Top" />
        <ListView x:Name="ImagesList" HorizontalAlignment="Left" Height="400" VerticalAlignment="Top" Width="400">
            <ListView.ItemTemplate>
                <DataTemplate>
                    <StackPanel Orientation="Vertical">
                        <Image Source="{Binding Path=Source}" />
                        <TextBlock Text="{Binding Path=Name}" />
                    </StackPanel>
                </DataTemplate>
            </ListView.ItemTemplate>
            <ListView.ItemsPanel>
                <ItemsPanelTemplate>
                    <WrapGrid Orientation="Horizontal" ItemHeight="100" ItemWidth="120"></WrapGrid>
                </ItemsPanelTemplate>
            </ListView.ItemsPanel>
        </ListView>
    </StackPanel>
</StackPanel>
```

### Step 4: Add activation code

The code in this step references the camera as a [**StorageDevice**](/uwp/api/Windows.Devices.Portable.StorageDevice) by passing the device information Id of the camera to the [**FromId**](/uwp/api/windows.devices.portable.storagedevice.fromid) method. The device information Id of the camera is obtained by first casting the event arguments as [**DeviceActivatedEventArgs**](/uwp/api/Windows.ApplicationModel.Activation.DeviceActivatedEventArgs), and then getting the value from the [**DeviceInformationId**](/uwp/api/windows.applicationmodel.activation.deviceactivatedeventargs.deviceinformationid) property.

Open the App.xaml.cs file and add the following code to the **App** class.

``` cs
protected override void OnActivated(IActivatedEventArgs args)
{
   if (args.Kind == ActivationKind.Device)
   {
      Frame rootFrame = null;
      // Ensure that the current page exists and is activated
      if (Window.Current.Content == null)
      {
         rootFrame = new Frame();
         rootFrame.Navigate(typeof(MainPage));
         Window.Current.Content = rootFrame;
      }
      else
      {
         rootFrame = Window.Current.Content as Frame;
      }
      Window.Current.Activate();

      // Make sure the necessary APIs are present on the device
      bool storageDeviceAPIPresent =
      Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Devices.Portable.StorageDevice");

      if (storageDeviceAPIPresent)
      {
         // Reference the current page as type MainPage
         var mPage = rootFrame.Content as MainPage;

         // Cast the activated event args as DeviceActivatedEventArgs and show images
         var deviceArgs = args as DeviceActivatedEventArgs;
         if (deviceArgs != null)
         {
            mPage.ShowImages(Windows.Devices.Portable.StorageDevice.FromId(deviceArgs.DeviceInformationId));
         }
      }
      else
      {
         // Handle case where APIs are not present (when the device is not part of the desktop device family)
      }

   }

   base.OnActivated(args);
}
```

> [!NOTE]
> The `ShowImages` method is added in the following step.

### Step 5: Add code to display device information

You can obtain information about the camera from the properties of the [**StorageDevice**](/uwp/api/Windows.Devices.Portable.StorageDevice) class. The code in this step displays the device name and other info to the user when the app runs. The code then calls the GetImageList and GetThumbnail methods, which you will add in the next step, to display thumbnails of the images stored on the camera

In the MainPage.xaml.cs file, add the following code to the **MainPage** class.

``` cs
private Windows.Storage.StorageFolder rootFolder;

internal async void ShowImages(Windows.Storage.StorageFolder folder)
{
    DeviceInfoTextBlock.Text = "Display Name = " + folder.DisplayName + "\n";
    DeviceInfoTextBlock.Text += "Display Type =  " + folder.DisplayType + "\n";
    DeviceInfoTextBlock.Text += "FolderRelativeId = " + folder.FolderRelativeId + "\n";

    // Reference first folder of the device as the root
    rootFolder = (await folder.GetFoldersAsync())[0];
    var imageList = await GetImageList(rootFolder);

    foreach (Windows.Storage.StorageFile img in imageList)
    {
        ImagesList.Items.Add(await GetThumbnail(img));
    }
}
```

> [!NOTE]
> The `GetImageList` and `GetThumbnail` methods are added in the following step.

### Step 6: Add code to display images

The code in this step displays thumbnails of the images stored on the camera. The code makes asynchronous calls to the camera to get the thumbnail image. However, the next asynchronous call doesn't occur until the previous asynchronous call completes. This ensures that only one request is made to the camera at a time.

In the MainPage.xaml.cs file, add the following code to the **MainPage** class.

``` cs
async private System.Threading.Tasks.Task<List<Windows.Storage.StorageFile>> GetImageList(Windows.Storage.StorageFolder folder)
{
    var result = await folder.GetFilesAsync();
    var subFolders = await folder.GetFoldersAsync();
    foreach (Windows.Storage.StorageFolder f in subFolders)
        result = result.Union(await GetImageList(f)).ToList();

    return (from f in result orderby f.Name select f).ToList();
}

async private System.Threading.Tasks.Task<Image> GetThumbnail(Windows.Storage.StorageFile img)
{
    // Get the thumbnail to display
    var thumbnail = await img.GetThumbnailAsync(Windows.Storage.FileProperties.ThumbnailMode.SingleItem,
                                                100,
                                                Windows.Storage.FileProperties.ThumbnailOptions.UseCurrentScale);

    // Create a XAML Image object bind to on the display page
    var result = new Image();
    result.Height = thumbnail.OriginalHeight;
    result.Width = thumbnail.OriginalWidth;
    result.Name = img.Name;
    var imageBitmap = new Windows.UI.Xaml.Media.Imaging.BitmapImage();
    imageBitmap.SetSource(thumbnail);
    result.Source = imageBitmap;

    return result;
}
```

### Step 7: Build and run the app

1. Press F5 to build and deploy the app (in debug mode).
2. To run your app, connect a camera to your machine. Then select the app from the AutoPlay list of options.

> [!NOTE]
> Not all cameras advertise for the **WPD\\ImageSource** AutoPlay device event.

## Configure removable storage

You can identify a volume device such as a memory card or thumb drive as an **AutoPlay** device when the volume device is connected to a PC. This is especially useful when you want to associate a specific app for **AutoPlay** to present to the user for your volume device.

Here we show how to identify your volume device as an **AutoPlay** device.

To identify your volume device as an **AutoPlay** device, add an autorun.inf file to the root drive of your device. In the autorun.inf file, add a **CustomEvent** key to the **AutoRun** section. When your volume device connects to a PC, **AutoPlay** will find the autorun.inf file and treat your volume as a device. **AutoPlay** will create an **AutoPlay** event by using the name that you supplied for the **CustomEvent** key. You can then create an app and register the app as a handler for that **AutoPlay** event. When the device is connected to the PC, **AutoPlay** will show your app as a handler for your volume device. For more info on autorun.inf files, see [autorun.inf entries](/windows/desktop/shell/autorun-cmds).

### Step 1: Create an autorun.inf file

In the root drive of your volume device, add a file named autorun.inf. Open the autorun.inf file and add the following text.

``` syntax
[AutoRun]
CustomEvent=AutoPlayCustomEventQuickstart
```

### Step 2: Create a new project and add AutoPlay declarations

1. Open Visual Studio and select **New Project** from the **File** menu. In the **Visual C#** section, under **Windows**, select **Blank App (Universal Windows)**. Name the application **AutoPlayCustomEvent** and click **OK.**
2. Open the Package.appxmanifest file and select the **Capabilities** tab. Select the **Removable Storage** capability. This gives the app access to the files and folders on removable storage devices.
3. In the manifest file, select the **Declarations** tab. In the **Available Declarations** drop-down list, select **AutoPlay Content** and click **Add**. Select the new **AutoPlay Content** item that was added to the **Supported Declarations** list.

> [!NOTE]
> Alternatively, you can also choose to add an **AutoPlay Device** declaration for your custom AutoPlay event.

4. In the **Launch Actions** section for your **AutoPlay Content** event declaration, enter the values in the table below for the first launch action.
5. In the **Available Declarations** drop-down list, select **File Type Associations** and click **Add**. In the Properties of the new **File Type Associations** declaration, set the **Display Name** field to **Show .ms Files** and the **Name** field to **ms\_association**. In the **Supported File Types** section, click **Add New**. Set the **File Type** field to **.ms**. For content events, AutoPlay filters out any file types that aren't explicitly associated with your app.
6. Save and close the manifest file.

| Setting             | Value                         |
|---------------------|-------------------------------|
| Verb                | show                          |
| Action Display Name | Show Files                    |
| Content Event       | AutoPlayCustomEventQuickstart |

The **Content Event** value is the text that you supplied for the **CustomEvent** key in your autorun.inf file. The **Action Display Name** setting identifies the string that AutoPlay displays for your app. The **Verb** setting identifies a value that is passed to your app for the selected option. You can specify multiple launch actions for an AutoPlay event and use the **Verb** setting to determine which option a user has selected for your app. You can tell which option the user selected by checking the **verb** property of the startup event arguments passed to your app. You can use any value for the **Verb** setting except, **open**, which is reserved.

### Step 3: Add the XAML UI

Open the MainPage.xaml file and add the following XAML to the default &lt;Grid&gt; section.

``` xaml
<StackPanel Orientation="Vertical">
    <TextBlock FontSize="28" Margin="10,0,800,0">Files</TextBlock>
    <TextBlock x:Name="FilesBlock" FontSize="22" Height="600" Margin="10,0,800,0" />
</StackPanel>
```

### Step 4: Add the activation code

The code in this step calls a method to display the folders in the root drive of your volume device. For the AutoPlay content events, AutoPlay passes the root folder of the storage device in the startup arguments passed to the application during the **OnFileActivated** event. You can retrieve this folder from the first element of the **Files** property.

Open the App.xaml.cs file and add the following code to the **App** class.

``` cs
protected override void OnFileActivated(FileActivatedEventArgs args)
{
    var rootFrame = Window.Current.Content as Frame;
    var page = rootFrame.Content as MainPage;

    // Call ShowFolders with root folder from device storage.
    page.DisplayFiles(args.Files[0] as Windows.Storage.StorageFolder);

    base.OnFileActivated(args);
}
```

> [!NOTE]
> The `DisplayFiles` method is added in the following step.

### Step 5: Add code to display folders

In the MainPage.xaml.cs file add the following code to the **MainPage** class.

``` cs
internal async void DisplayFiles(Windows.Storage.StorageFolder folder)
{
    foreach (Windows.Storage.StorageFile f in await ReadFiles(folder, ".ms"))
    {
        FilesBlock.Text += "  " + f.Name + "\n";
    }
}

internal async System.Threading.Tasks.Task<IReadOnlyList<Windows.Storage.StorageFile>>
    ReadFiles(Windows.Storage.StorageFolder folder, string fileExtension)
{
    var options = new Windows.Storage.Search.QueryOptions();
    options.FileTypeFilter.Add(fileExtension);
    var query = folder.CreateFileQueryWithOptions(options);
    var files = await query.GetFilesAsync();

    return files;
}
```

### Step 6: Build and run the application

1. Press F5 to build and deploy the app (in debug mode).
2. To run your app, insert a memory card or another storage device into your PC. Then select your app from the list of AutoPlay handler options.

## AutoPlay event reference

The **AutoPlay** system allows apps to register for a variety of device and volume (disk) arrival events. To register for **AutoPlay** content events, you must enable the **Removable Storage** capability in your package manifest. This table shows the events that you can register for and when they are raised.

| Scenario                                                           | Event | Description |
|--------------------------------------------------------------------|------------------------------------|---------------|
| Using photos on a Camera                                           | **WPD\ImageSource** | Raised for cameras that are identified as Windows Portable Devices and offer the ImageSource capability. |
| Using music on an audio player                                     | **WPD\AudioSource** | Raised for media players that are identified as Windows Portable Devices and offer the AudioSource capability. |
| Using videos on a video camera                                     | **WPD\VideoSource** | Raised for video cameras that are identified as Windows Portable Devices and offer the VideoSource capability. |
| Access a connected flash drive or external hard drive              | **StorageOnArrival** | Raised when a drive or volume is connected to the PC.   If the drive or volume contains a DCIM, AVCHD, or PRIVATE\ACHD folder in the root of the disk, the **ShowPicturesOnArrival** event is raised instead. |
| Using photos from mass storage (legacy)                            | **ShowPicturesOnArrival** | Raised when a drive or volume contains a DCIM, AVCHD, or PRIVATE\ACHD folder in the root of the disk. IIf a user  has enabled **Choose what to do with each type of media** in the AutoPlay Control Panel, AutoPlay will examine a volume connected to the PC to determine the type of content on the disk. When pictures are found, **ShowPicturesOnArrival** is raised. |
| Receiving photos with Proximity Sharing (tap and send)             | **ShowPicturesOnArrival** | When users send content with using proximity (tap and send), AutoPlay will examine the shared files to determine the type of content. If pictures are found, **ShowPicturesOnArrival** is raised. |
| Using music from mass storage (legacy)                             | **PlayMusicFilesOnArrival** | If a user has enabled **Choose what to do with each type of media** in the AutoPlay Control Panel, AutoPlay will examine a volume connected to the PC to determine the type of content on the disk.  When music files are found, **PlayMusicFilesOnArrival** is raised. |
| Receiving music with Proximity Sharing (tap and send)              | **PlayMusicFilesOnArrival** | When users send content with using proximity (tap and send), AutoPlay will examine the shared files to determine the type of content. If music files are found, **PlayMusicFilesOnArrival** is raised. |
| Using videos from mass storage (legacy)                            | **PlayVideoFilesOnArrival** | If a user has enabled **Choose what to do with each type of media** in the AutoPlay Control Panel, AutoPlay will examine a volume connected to the PC to determine the type of content on the disk. When video files are found, **PlayVideoFilesOnArrival** is raised. |
| Receiving videos with Proximity Sharing (tap and send)             | **PlayVideoFilesOnArrival** | When users send content with using proximity (tap and send), AutoPlay will examine the shared files to determine the type of content. If video files are found, **PlayVideoFilesOnArrival** is raised. |
| Handling mixed sets of files from a connected device               | **MixedContentOnArrival** | If a user has enabled **Choose what to do with each type of media** in the AutoPlay Control Panel, AutoPlay will examine a volume connected to the PC to determine the type of content on the disk. If no specific content type is found (for example, pictures), **MixedContentOnArrival** is raised. |
| Handling mixed sets of files with Proximity Sharing (tap and send) | **MixedContentOnArrival** | When users send content with using proximity (tap and send), AutoPlay will examine the shared files to determine the type of content. If no specific content type is found (for example, pictures), **MixedContentOnArrival** is raised. |
| Handle video from optical media                                    | **PlayDVDMovieOnArrival**<br/>**PlayBluRayOnArrival**<br/>**PlayVideoCDMovieOnArrival**<br/>**PlaySuperVideoCDMovieOnArrival** | When a disk is inserted into the optical drive, AutoPlay will examine the files to determine the type of content. When video files are found, the event corresponding to the type of optical disk is raised. |
| Handle music from optical media                                    | **PlayCDAudioOnArrival**<br/>**PlayDVDAudioOnArrival** | When a disk is inserted into the optical drive, AutoPlay will examine the files to determine the type of content. When music files are found, the event corresponding to the type of optical disk is raised. |
| Play enhanced disks                                                | **PlayEnhancedCDOnArrival**<br/>**PlayEnhancedDVDOnArrival** | When a disk is inserted into the optical drive, AutoPlay will examine the files to determine the type of content. When an enhanced disk is found, the event corresponding to the type of optical disk is raised. |
| Handle writeable optical disks                                     | **HandleCDBurningOnArrival**<br/>**HandleDVDBurningOnArrival**<br/>**HandleBDBurningOnArrival** | When a disk is inserted into the optical drive, AutoPlay will examine the files to determine the type of content. When a writable disk is found, the event corresponding to the type of optical disk is raised. |
| Handle any other device or volume connection                       | **UnknownContentOnArrival** | Raised for all events in case content is found that does not match any of the AutoPlay content events. Use of this event is not recommended. You should only register your application for the specific AutoPlay events that it can handle. |

You can specify that AutoPlay raise a custom AutoPlay Content event using the **CustomEvent** entry in the autorun.inf file for a volume. For more info, see [Autorun.inf entries](/windows/desktop/shell/autorun-cmds).

You can register your app as an AutoPlay Content or AutoPlay Device event handler by adding an extension to the package.appxmanifest file for your app. If you are using Visual Studio, you can add an **AutoPlay Content** or **AutoPlay Device** declaration in the **Declarations** tab. If you are editing the package.appxmanifest file for your app directly, add an [**Extension**](/uwp/schemas/appxpackage/appxmanifestschema/element-1-extension) element to your package manifest that specifies either **windows.autoPlayContent** or **windows.autoPlayDevice** as the **Category**. For example, the following entry in the package manifest adds an **AutoPlay Content** extension to register the app as a handler for the **ShowPicturesOnArrival** event.

``` xaml
  <Applications>
    <Application Id="AutoPlayHandlerSample.App">
      <Extensions>
        <Extension Category="windows.autoPlayContent">
          <AutoPlayContent>
            <LaunchAction Verb="show" ActionDisplayName="Show Pictures"
                          ContentEvent="ShowPicturesOnArrival" />
          </AutoPlayContent>
        </Extension>
      </Extensions>
    </Application>
  </Applications>
```
