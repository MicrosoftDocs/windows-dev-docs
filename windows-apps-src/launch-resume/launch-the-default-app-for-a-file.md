---
title: Launch the default app for a file
description: Learn how to use the Windows.System.Launcher API to launch the default handler for a file that your app can't handle itself.
ms.assetid: BB45FCAF-DF93-4C99-A8B5-59B799C7BD98
ms.date: 07/05/2018
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Launch the default app for a file

**Important APIs**

-   [**Windows.System.Launcher.LaunchFileAsync**](/uwp/api/windows.system.launcher.launchfileasync)

Learn how to launch the default app for a file. Many apps need to work with files that they can't handle themselves. For example, e-mail apps receive a variety of file types and need a way to launch these files in their default handlers. These steps show how to use the [**Windows.System.Launcher**](/uwp/api/Windows.System.Launcher) API to launch the default handler for a file that your app can't handle itself.

## Get the file object

First, get a [**Windows.Storage.StorageFile**](/uwp/api/Windows.Storage.StorageFile) object for the file.

If the file is included in the package for your app, you can use the [**Package.InstalledLocation**](/uwp/api/windows.applicationmodel.package.installedlocation) property to get a [**Windows.Storage.StorageFolder**](/uwp/api/Windows.Storage.StorageFolder) object and the [**Windows.Storage.StorageFolder.GetFileAsync**](/uwp/api/windows.storage.storagefolder.getfileasync) method to get the [**StorageFile**](/uwp/api/Windows.Storage.StorageFile) object.

If the file is in a known folder, you can use the properties of the [**Windows.Storage.KnownFolders**](/uwp/api/Windows.Storage.KnownFolders) class to get a [**StorageFolder**](/uwp/api/Windows.Storage.StorageFolder) and the [**GetFileAsync**](/uwp/api/windows.storage.storagefolder.getfileasync) method to get the [**StorageFile**](/uwp/api/Windows.Storage.StorageFile) object.

## Launch the file

Windows provides several different options for launching the default handler for a file. These options are described in this chart and in the sections that follow.

| Option | Method | Description |
|--------|--------|-------------|
| Default launch | [**LaunchFileAsync(IStorageFile)**](/uwp/api/windows.system.launcher.launchfileasync) | Launch the specified file with the default handler. |
| Open With launch | [**LaunchFileAsync(IStorageFile, LauncherOptions)**](/uwp/api/windows.system.launcher.launchfileasync#Windows_System_Launcher_LaunchFileAsync_Windows_Storage_IStorageFile_Windows_System_LauncherOptions_) | Launch the specified file letting the user pick the handler through the Open With dialog. |
| Launch with a recommended app fallback | [**LaunchFileAsync(IStorageFile, LauncherOptions)**](/uwp/api/windows.system.launcher.launchfileasync#Windows_System_Launcher_LaunchFileAsync_Windows_Storage_IStorageFile_Windows_System_LauncherOptions_) | Launch the specified file with the default handler. If no handler is installed on the system, recommend an app in the store to the user. |
| Launch with a desired remaining view | [**LaunchFileAsync(IStorageFile, LauncherOptions)**](/uwp/api/windows.system.launcher.launchfileasync#Windows_System_Launcher_LaunchFileAsync_Windows_Storage_IStorageFile_Windows_System_LauncherOptions_) (Windows-only) | Launch the specified file with the default handler. Specify a preference to stay on screen after the launch and request a specific window size. [**LauncherOptions.DesiredRemainingView**](/uwp/api/windows.system.launcheroptions.desiredremainingview) isn't supported on the mobile device family. |

### Default launch

Call the [**Windows.System.Launcher.LaunchFileAsync(IStorageFile)**](/uwp/api/windows.system.launcher.launchfileasync) method to launch the default app. This example uses the [**Windows.Storage.StorageFolder.GetFileAsync**](/uwp/api/windows.storage.storagefolder.getfileasync) method to launch an image file, test.png, that is included in the app package.

```csharp
async void DefaultLaunch()
{
   // Path to the file in the app package to launch
   string imageFile = @"images\test.png";
   
   var file = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync(imageFile);
   
   if (file != null)
   {
      // Launch the retrieved file
      var success = await Windows.System.Launcher.LaunchFileAsync(file);

      if (success)
      {
         // File launched
      }
      else
      {
         // File launch failed
      }
   }
   else
   {
      // Could not find file
   }
}
```

```vb
async Sub DefaultLaunch()
   ' Path to the file in the app package to launch
   Dim imageFile = "images\test.png"
   Dim file = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync(imageFile)
   
   If file IsNot Nothing Then
      ' Launch the retrieved file
      Dim success = await Windows.System.Launcher.LaunchFileAsync(file)

      If success Then
         ' File launched
      Else
         ' File launch failed
      End If
   Else
      ' Could not find file
   End If
End Sub
```

```cppwinrt
Windows::Foundation::IAsyncAction MainPage::DefaultLaunch()
{
    auto installFolder{ Windows::ApplicationModel::Package::Current().InstalledLocation() };

    Windows::Storage::StorageFile file{ co_await installFolder.GetFileAsync(L"images\\test.png") };

    if (file)
    {
        // Launch the retrieved file
        bool success = co_await Windows::System::Launcher::LaunchFileAsync(file);
        if (success)
        {
            // File launched
        }
        else
        {
            // File launch failed
        }
    }
    else
    {
        // Could not find file
    }
}
```

```cpp
void MainPage::DefaultLaunch()
{
   auto installFolder = Windows::ApplicationModel::Package::Current->InstalledLocation;

   concurrency::task<Windows::Storage::StorageFile^getFileOperation(installFolder->GetFileAsync("images\\test.png"));
   getFileOperation.then([](Windows::Storage::StorageFile^ file)
   {
      if (file != nullptr)
      {
         // Launch the retrieved file
         concurrency::task<bool> launchFileOperation(Windows::System::Launcher::LaunchFileAsync(file));
         launchFileOperation.then([](bool success)
         {
            if (success)
            {
               // File launched
            }
            else
            {
               // File launch failed
            }
         });
      }
      else
      {
         // Could not find file
      }
   });
}
```

### Open With launch

Call the [**Windows.System.Launcher.LaunchFileAsync(IStorageFile, LauncherOptions)**](/uwp/api/windows.system.launcher.launchfileasync#Windows_System_Launcher_LaunchFileAsync_Windows_Storage_IStorageFile_Windows_System_LauncherOptions_) method with [**LauncherOptions.DisplayApplicationPicker**](/uwp/api/windows.system.launcheroptions.displayapplicationpicker) set to **true** to launch the app that the user selects from the **Open With** dialog box.

We recommend that you use the **Open With** dialog box when the user may want to select an app other than the default for a particular file. For example, if your app allows the user to launch an image file, the default handler will likely be a viewer app. In some cases, the user may want to edit the image instead of viewing it. Use the **Open With** option along with an alternative command in the **AppBar** or in a context menu to let the user bring up the **Open With** dialog and select the editor app in these types of scenarios.

![the open with dialog for a .png file launch. the dialog contains a checkbox which specifies if the user’s choice should be used for all .png files or just this one .png file. the dialog contains four app options for launching the file and a ‘more options’ link.](images/checkboxopenwithdialog.png)

```csharp
async void DefaultLaunch()
{
   // Path to the file in the app package to launch
      string imageFile = @"images\test.png";
      
   var file = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync(imageFile);

   if (file != null)
   {
      // Set the option to show the picker
      var options = new Windows.System.LauncherOptions();
      options.DisplayApplicationPicker = true;

      // Launch the retrieved file
      bool success = await Windows.System.Launcher.LaunchFileAsync(file, options);
      if (success)
      {
         // File launched
      }
      else
      {
         // File launch failed
      }
   }
   else
   {
      // Could not find file
   }
}
```

```vb
async Sub DefaultLaunch()

   ' Path to the file in the app package to launch
   Dim imageFile = "images\test.png"

   Dim file = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync(imageFile)

   If file IsNot Nothing Then
      ' Set the option to show the picker
      Dim options = Windows.System.LauncherOptions()
      options.DisplayApplicationPicker = True

      ' Launch the retrieved file
      Dim success = await Windows.System.Launcher.LaunchFileAsync(file)

      If success Then
         ' File launched
      Else
         ' File launch failed
      End If
   Else
      ' Could not find file
   End If
End Sub
```

```cppwinrt
Windows::Foundation::IAsyncAction MainPage::DefaultLaunch()
{
    auto installFolder{ Windows::ApplicationModel::Package::Current().InstalledLocation() };

    Windows::Storage::StorageFile file{ co_await installFolder.GetFileAsync(L"images\\test.png") };

    if (file)
    {
        // Set the option to show the picker
        Windows::System::LauncherOptions launchOptions;
        launchOptions.DisplayApplicationPicker(true);

        // Launch the retrieved file
        bool success = co_await Windows::System::Launcher::LaunchFileAsync(file, launchOptions);
        if (success)
        {
            // File launched
        }
        else
        {
            // File launch failed
        }
    }
    else
    {
        // Could not find file
    }
}
```

```cpp
void MainPage::DefaultLaunch()
{
   auto installFolder = Windows::ApplicationModel::Package::Current->InstalledLocation;

   concurrency::task<Windows::Storage::StorageFile^> getFileOperation(installFolder->GetFileAsync("images\\test.png"));
   getFileOperation.then([](Windows::Storage::StorageFile^ file)
   {
      if (file != nullptr)
      {
         // Set the option to show the picker
         auto launchOptions = ref new Windows::System::LauncherOptions();
         launchOptions->DisplayApplicationPicker = true;

         // Launch the retrieved file
         concurrency::task<bool> launchFileOperation(Windows::System::Launcher::LaunchFileAsync(file, launchOptions));
         launchFileOperation.then([](bool success)
         {
            if (success)
            {
               // File launched
            }
            else
            {
               // File launch failed
            }
         });
      }
      else
      {
         // Could not find file
      }
   });
}
```

**Launch with a recommended app fallback**

In some cases the user may not have an app installed to handle the file that you are launching. By default, Windows will handle these cases by providing the user with a link to search for an appropriate app on the store. If you would like to give the user a specific recommendation for which app to acquire in this scenario, you may do so by passing that recommendation along with the file that you are launching. To do this, call the [**Windows.System.Launcher.launchFileAsync(IStorageFile, LauncherOptions)**](/uwp/api/windows.system.launcher.launchfileasync#Windows_System_Launcher_LaunchFileAsync_Windows_Storage_IStorageFile_Windows_System_LauncherOptions_) method with [**LauncherOptions.PreferredApplicationPackageFamilyName**](/uwp/api/windows.system.launcheroptions.preferredapplicationpackagefamilyname) set to the package family name of the app in the Store that you want to recommend. Then, set the [**LauncherOptions.PreferredApplicationDisplayName**](/uwp/api/windows.system.launcheroptions.preferredapplicationdisplayname) to the name of that app. Windows will use this information to replace the general option to search for an app in the store with a specific option to acquire the recommended app from the Store.

> [!NOTE]
> You must set both of these options to recommend an app. Setting one without the other will result in a failure.

![the open with dialog for a .contoso file launch. since .contoso does not have a handler installed on the machine the dialog contains an option with the store icon and text which points the user to the correct handler on the store. the dialog also contains a ‘more options’ link'.](images/howdoyouwanttoopen.png)

```csharp
async void DefaultLaunch()
{
   // Path to the file in the app package to launch
   string imageFile = @"images\test.contoso";

   // Get the image file from the package's image directory
   var file = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync(imageFile);

   if (file != null)
   {
      // Set the recommended app
      var options = new Windows.System.LauncherOptions();
      options.PreferredApplicationPackageFamilyName = "Contoso.FileApp_8wknc82po1e";
      options.PreferredApplicationDisplayName = "Contoso File App";

      // Launch the retrieved file pass in the recommended app
      // in case the user has no apps installed to handle the file
      bool success = await Windows.System.Launcher.LaunchFileAsync(file, options);
      if (success)
      {
         // File launched
      }
      else
      {
         // File launch failed
      }
   }
   else
   {
      // Could not find file
   }
}
```

```vb
async Sub DefaultLaunch()

   ' Path to the file in the app package to launch
   Dim imageFile = "images\test.contoso"

   ' Get the image file from the package's image directory
   Dim file = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync(imageFile)

   If file IsNot Nothing Then
      ' Set the recommended app
      Dim options = Windows.System.LauncherOptions()
      options.PreferredApplicationPackageFamilyName = "Contoso.FileApp_8wknc82po1e";
      options.PreferredApplicationDisplayName = "Contoso File App";

      ' Launch the retrieved file pass in the recommended app
      ' in case the user has no apps installed to handle the file
      Dim success = await Windows.System.Launcher.LaunchFileAsync(file)

      If success Then
         ' File launched
      Else
         ' File launch failed
      End If
   Else
      ' Could not find file
   End If
End Sub
```

```cppwinrt
Windows::Foundation::IAsyncAction MainPage::DefaultLaunch()
{
    auto installFolder{ Windows::ApplicationModel::Package::Current().InstalledLocation() };

    Windows::Storage::StorageFile file{ co_await installFolder.GetFileAsync(L"images\\test.png") };

    if (file)
    {
        // Set the recommended app
        Windows::System::LauncherOptions launchOptions;
        launchOptions.PreferredApplicationPackageFamilyName(L"Contoso.FileApp_8wknc82po1e");
        launchOptions.PreferredApplicationDisplayName(L"Contoso File App");

        // Launch the retrieved file, and pass in the recommended app
        // in case the user has no apps installed to handle the file.
        bool success = co_await Windows::System::Launcher::LaunchFileAsync(file, launchOptions);
        if (success)
        {
            // File launched
        }
        else
        {
            // File launch failed
        }
    }
    else
    {
        // Could not find file
    }
}
```

```cpp
void MainPage::DefaultLaunch()
{
   auto installFolder = Windows::ApplicationModel::Package::Current->InstalledLocation;

   concurrency::task<Windows::Storage::StorageFile^> getFileOperation(installFolder->GetFileAsync("images\\test.contoso"));
   getFileOperation.then([](Windows::Storage::StorageFile^ file)
   {
      if (file != nullptr)
      {
         // Set the recommended app
         auto launchOptions = ref new Windows::System::LauncherOptions();
         launchOptions->PreferredApplicationPackageFamilyName = "Contoso.FileApp_8wknc82po1e";
         launchOptions->PreferredApplicationDisplayName = "Contoso File App";
         
         // Launch the retrieved file pass, and in the recommended app
         // in case the user has no apps installed to handle the file.
         concurrency::task<bool> launchFileOperation(Windows::System::Launcher::LaunchFileAsync(file, launchOptions));
         launchFileOperation.then([](bool success)
         {
            if (success)
            {
               // File launched
            }
            else
            {
               // File launch failed
            }
         });
      }
      else
      {
         // Could not find file
      }
   });
}
```

### Launch with a Desired Remaining View (Windows-only)

Source apps that call [**LaunchFileAsync**](/uwp/api/windows.system.launcher.launchfileasync) can request that they remain on screen after a file launch. By default, Windows attempts to share all available space equally between the source app and the target app that handles the file. Source apps can use the [**DesiredRemainingView**](/uwp/api/windows.system.launcheroptions.desiredremainingview) property to indicate to the operating system that they prefer their app window to take up more or less of the available space. **DesiredRemainingView** can also be used to indicate that the source app does not need to remain on screen after the file launch and can be completely replaced by the target app. This property only specifies the preferred window size of the calling app. It doesn't specify the behavior of other apps that may happen to also be on screen at the same time.

> [!NOTE]
> Windows takes into account multiple different factors when it determines the source app's final window size, for example, the preference of the source app, the number of apps on screen, the screen orientation, and so on. By setting [**DesiredRemainingView**](/uwp/api/windows.system.launcheroptions.desiredremainingview), you aren't guaranteed a specific windowing behavior for the source app.

**Mobile device family:  **[**LauncherOptions.DesiredRemainingView**](/uwp/api/windows.system.launcheroptions.desiredremainingview) isn't supported on the mobile device family.

```csharp
async void DefaultLaunch()
{
   // Path to the file in the app package to launch
   string imageFile = @"images\test.png";
   
   var file = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync(imageFile);

   if (file != null)
   {
      // Set the desired remaining view
      var options = new Windows.System.LauncherOptions();
      options.DesiredRemainingView = Windows.UI.ViewManagement.ViewSizePreference.UseLess;

      // Launch the retrieved file
      bool success = await Windows.System.Launcher.LaunchFileAsync(file, options);
      if (success)
      {
         // File launched
      }
      else
      {
         // File launch failed
      }
   }
   else
   {
      // Could not find file
   }
}
```

```cppwinrt
Windows::Foundation::IAsyncAction MainPage::DefaultLaunch()
{
    auto installFolder{ Windows::ApplicationModel::Package::Current().InstalledLocation() };

    Windows::Storage::StorageFile file{ co_await installFolder.GetFileAsync(L"images\\test.png") };

    if (file)
    {
        // Set the desired remaining view.
        Windows::System::LauncherOptions launchOptions;
        launchOptions.DesiredRemainingView(Windows::UI::ViewManagement::ViewSizePreference::UseLess);

        // Launch the retrieved file.
        bool success = co_await Windows::System::Launcher::LaunchFileAsync(file, launchOptions);
        if (success)
        {
            // File launched
        }
        else
        {
            // File launch failed
        }
    }
    else
    {
        // Could not find file
    }
}
```

```cpp
void MainPage::DefaultLaunch()
{
   auto installFolder = Windows::ApplicationModel::Package::Current->InstalledLocation;

   concurrency::task<Windows::Storage::StorageFile^> getFileOperation(installFolder->GetFileAsync("images\\test.png"));
   getFileOperation.then([](Windows::Storage::StorageFile^ file)
   {
      if (file != nullptr)
      {
         // Set the desired remaining view.
         auto launchOptions = ref new Windows::System::LauncherOptions();
         launchOptions->DesiredRemainingView = Windows::UI::ViewManagement::ViewSizePreference::UseLess;

         // Launch the retrieved file.
         concurrency::task<bool> launchFileOperation(Windows::System::Launcher::LaunchFileAsync(file, launchOptions));
         launchFileOperation.then([](bool success)
         {
            if (success)
            {
               // File launched
            }
            else
            {
               // File launch failed
            }
         });
      }
      else
      {
         // Could not find file
      }
   });
}
```

## Remarks

Your app can't select the app that is launched. The user determines which app is launched. The user can select either a Universal Windows Platform (UWP) app or a Windows desktop app.

When launching a file, your app must be the foreground app, that is, it must be visible to the user. This requirement helps ensure that the user remains in control. To meet this requirement, make sure that you tie all file launches directly to the UI of your app. Most likely, the user must always take some action to initiate a file launch.

You can't launch file types that contain code or script if they are executed automatically by the operating system, such as, .exe, .msi, and .js files. This restriction protects users from potentially malicious files that could modify the operating system. You can use this method to launch file types that can contain script if they are executed by an app that isolates the script, such as, .docx files. Apps like Microsoft Word keep the script in .docx files from modifying the operating system.

If you try to launch a restricted file type, the launch will fail and your error callback will be invoked. If your app handles many different types of files and you expect that you will hit this error, we recommend that you provide a fallback experience to your user. For example, you could give the user an option to save the file to the desktop, and they could open it there.

## Related topics

### Tasks

* [Launch the default app for a URI](launch-default-app.md)
* [Handle file activation](handle-file-activation.md)

### Guidelines

* [Guidelines for file types and URIs](../files/index.md)

### Reference

* [**Windows.Storage.StorageFile**](/uwp/api/Windows.Storage.StorageFile)
* [**Windows.System.Launcher.LaunchFileAsync**](/uwp/api/windows.system.launcher.launchfileasync)