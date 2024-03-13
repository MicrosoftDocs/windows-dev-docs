---
description: Learn how to store and retrieve local and temporary app data.
title: Store and retrieve settings and other app data
ms.assetid: 41676A02-325A-455E-8565-C9EC0BC3A8FE
label: App settings and data
template: detail.hbs
ms.date: 11/14/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Store and retrieve settings and other app data

*App data* is mutable data that is created and managed by a specific app. It includes runtime state, app settings, user preferences, reference content (such as the dictionary definitions in a dictionary app), and other settings. App data is different from *user data*, data that the user creates and manages when using an app. User data includes document or media files, email or communication transcripts, or database records holding content created by the user. User data may be useful or meaningful to more than one app. Often, this is data that the user wants to manipulate or transmit as an entity independent of the app itself, such as a document.

**Important note about app data:** The lifetime of the app data is tied to the lifetime of the app. If the app is removed, all of the app data will be lost as a consequence. Don't use app data to store user data or anything that users might perceive as valuable and irreplaceable. We recommend that the user's libraries and Microsoft OneDrive be used to store this sort of information. App data is ideal for storing app-specific user preferences, settings, and favorites.

## Types of app data

There are two types of app data: settings and files.

### Settings

Use settings to store user preferences and application state info. The app data API enables you to easily create and retrieve settings (we'll show you some examples later in this article).

Here are data types you can use for app settings:

- **UInt8**, **Int16**, **UInt16**, **Int32**, **UInt32**, **Int64**, **UInt64**, **Single**, **Double**
- **Boolean**
- **Char16**, **String**
- [**DateTime**](/uwp/api/Windows.Foundation.DateTime), [**TimeSpan**](/uwp/api/Windows.Foundation.TimeSpan)
    - For C#/.NET, use: [**System.DateTimeOffset**](/dotnet/api/system.datetimeoffset?view=dotnet-uwp-10.0&preserve-view=true), [**System.TimeSpan**](/dotnet/api/system.timespan?view=dotnet-uwp-10.0&preserve-view=true)
- **GUID**, [**Point**](/uwp/api/Windows.Foundation.Point), [**Size**](/uwp/api/Windows.Foundation.Size), [**Rect**](/uwp/api/Windows.Foundation.Rect)
- [**ApplicationDataCompositeValue**](/uwp/api/Windows.Storage.ApplicationDataCompositeValue): A set of related app settings that must be serialized and deserialized atomically. Use composite settings to easily handle atomic updates of interdependent settings. The system ensures the integrity of composite settings during concurrent access and roaming. Composite settings are optimized for small amounts of data, and performance can be poor if you use them for large data sets.

### Files

Use files to store binary data or to enable your own, customized serialized types.

## Storing app data in the app data stores


When an app is installed, the system gives it its own per-user data stores for settings and files. You don't need to know where or how this data exists, because the system is responsible for managing the physical storage, ensuring that the data is kept isolated from other apps and other users. The system also preserves the contents of these data stores when the user installs an update to your app and removes the contents of these data stores completely and cleanly when your app is uninstalled.

Within its app data store, each app has system-defined root directories: one for local files, one for roaming files, and one for temporary files. Your app can add new files and new containers to each of these root directories.

## Local app data


Local app data should be used for any information that needs to be preserved between app sessions and is not suitable for roaming app data. Data that is not applicable on other devices should be stored here as well. There is no general size restriction on local data stored. Use the local app data store for data that it does not make sense to roam and for large data sets.

### Retrieve the local app data store

Before you can read or write local app data, you must retrieve the local app data store. To retrieve the local app data store, use the [**ApplicationData.LocalSettings**](/uwp/api/windows.storage.applicationdata.localsettings) property to get the app's local settings as an [**ApplicationDataContainer**](/uwp/api/Windows.Storage.ApplicationDataContainer) object. Use the [**ApplicationData.LocalFolder**](/uwp/api/windows.storage.applicationdata.localfolder) property to get the files in a [**StorageFolder**](/uwp/api/Windows.Storage.StorageFolder) object. Use the [**ApplicationData.LocalCacheFolder**](/uwp/api/windows.storage.applicationdata.localcachefolder) property to get the folder in the local app data store where you can save files that are not included in backup and restore.

```CSharp
Windows.Storage.ApplicationDataContainer localSettings = 
    Windows.Storage.ApplicationData.Current.LocalSettings;
Windows.Storage.StorageFolder localFolder = 
    Windows.Storage.ApplicationData.Current.LocalFolder;
```

### Create and retrieve a simple local setting

To create or write a setting, use the [**ApplicationDataContainer.Values**](/uwp/api/windows.storage.applicationdatacontainer.values) property to access the settings in the `localSettings` container we got in the previous step. This example creates a setting named `exampleSetting`.

```CSharp
// Simple setting

localSettings.Values["exampleSetting"] = "Hello Windows";
```

To retrieve the setting, you use the same [**ApplicationDataContainer.Values**](/uwp/api/windows.storage.applicationdatacontainer.values) property that you used to create the setting. This example shows how to retrieve the setting we just created.

```CSharp
// Simple setting
Object value = localSettings.Values["exampleSetting"];
```

### Create and retrieve a local composite value

To create or write a composite value, create an [**ApplicationDataCompositeValue**](/uwp/api/Windows.Storage.ApplicationDataCompositeValue) object. This example creates a composite setting named `exampleCompositeSetting` and adds it to the `localSettings` container.

```CSharp
// Composite setting

Windows.Storage.ApplicationDataCompositeValue composite = 
    new Windows.Storage.ApplicationDataCompositeValue();
composite["intVal"] = 1;
composite["strVal"] = "string";

localSettings.Values["exampleCompositeSetting"] = composite;
```

This example shows how to retrieve the composite value we just created.

```CSharp
// Composite setting

Windows.Storage.ApplicationDataCompositeValue composite = 
   (Windows.Storage.ApplicationDataCompositeValue)localSettings.Values["exampleCompositeSetting"];

if (composite == null)
{
   // No data
}
else
{
   // Access data in composite["intVal"] and composite["strVal"]
}
```

### Create and read a local file

To create and update a file in the local app data store, use the file APIs, such as [**Windows.Storage.StorageFolder.CreateFileAsync**](/uwp/api/windows.storage.storagefolder.createfileasync) and [**Windows.Storage.FileIO.WriteTextAsync**](/uwp/api/windows.storage.fileio.writetextasync). This example creates a file named `dataFile.txt` in the `localFolder` container and writes the current date and time to the file. The **ReplaceExisting** value from the [**CreationCollisionOption**](/uwp/api/Windows.Storage.CreationCollisionOption) enumeration indicates to replace the file if it already exists.

```csharp
async void WriteTimestamp()
{
   Windows.Globalization.DateTimeFormatting.DateTimeFormatter formatter = 
       new Windows.Globalization.DateTimeFormatting.DateTimeFormatter("longtime");

   StorageFile sampleFile = await localFolder.CreateFileAsync("dataFile.txt", 
       CreationCollisionOption.ReplaceExisting);
   await FileIO.WriteTextAsync(sampleFile, formatter.Format(DateTimeOffset.Now));
}
```

To open and read a file in the local app data store, use the file APIs, such as [**Windows.Storage.StorageFolder.GetFileAsync**](/uwp/api/windows.storage.storagefolder.getfileasync), [**Windows.Storage.StorageFile.GetFileFromApplicationUriAsync**](/uwp/api/windows.storage.storagefile.getfilefromapplicationuriasync), and [**Windows.Storage.FileIO.ReadTextAsync**](/uwp/api/windows.storage.fileio.readtextasync). This example opens the `dataFile.txt` file created in the previous step and reads the date from the file. For details on loading file resources from various locations, see [How to load file resources](/previous-versions/windows/apps/hh965322(v=win.10)).

```csharp
async void ReadTimestamp()
{
   try
   {
      StorageFile sampleFile = await localFolder.GetFileAsync("dataFile.txt");
      String timestamp = await FileIO.ReadTextAsync(sampleFile);
      // Data is contained in timestamp
   }
   catch (Exception)
   {
      // Timestamp not found
   }
}
```

## Roaming data

> [!WARNING]
> Roaming data and settings is [no longer supported as of Windows 11](/windows/deployment/planning/windows-10-deprecated-features).
> The recommended replacement is [Azure App Service](/azure/app-service/). Azure App Service is widely supported, well documented, reliable, and supports cross-platform/cross-ecosystem scenarios such as iOS, Android and web.
>
> The following documentation applies to Windows 10 versions 1909 and lower.

If you use roaming data in your app, your users can easily keep your app's app data in sync across multiple devices. If a user installs your app on multiple devices, the OS keeps the app data in sync, reducing the amount of setup work that the user needs to do for your app on their second device. Roaming also enables your users to continue a task, such as composing a list, right where they left off even on a different device. The OS replicates roaming data to the cloud when it is updated, and synchronizes the data to the other devices on which the app is installed.

The OS limits the size of the app data that each app may roam. See [**ApplicationData.RoamingStorageQuota**](/uwp/api/windows.storage.applicationdata.roamingstoragequota). If the app hits this limit, none of the app's app data will be replicated to the cloud until the app's total roamed app data is less than the limit again. For this reason, it is a best practice to use roaming data only for user preferences, links, and small data files.

Roaming data for an app is available in the cloud as long as it is accessed by the user from some device within the required time interval. If the user does not run an app for longer than this time interval, its roaming data is removed from the cloud. If a user uninstalls an app, its roaming data isn't automatically removed from the cloud, it's preserved. If the user reinstalls the app within the time interval, the roaming data is synchronized from the cloud.

### Roaming data do's and don'ts

> See important note about [roaming data](#roaming-data).

- Use roaming for user preferences and customizations, links, and small data files. For example, use roaming to preserve a user's background color preference across all devices.
- Use roaming to let users continue a task across devices. For example, roam app data like the contents of an drafted email or the most recently viewed page in a reader app.
- Handle the [**DataChanged**](/uwp/api/windows.storage.applicationdata.datachanged) event by updating app data. This event occurs when app data has just finished syncing from the cloud.
- Roam references to content rather than raw data. For example, roam a URL rather than the content of an online article.
- For important, time critical settings, use the *HighPriority* setting associated with [**RoamingSettings**](/uwp/api/windows.storage.applicationdata.roamingsettings).
- Don't roam app data that is specific to a device. Some info is only pertinent locally, such as a path name to a local file resource. If you do decide to roam local information, make sure that the app can recover if the info isn't valid on the secondary device.
- Don't roam large sets of app data. There's a limit to the amount of app data an app may roam; use [**RoamingStorageQuota**](/uwp/api/windows.storage.applicationdata.roamingstoragequota) property to get this maximum. If an app hits this limit, no data can roam until the size of the app data store no longer exceeds the limit. When you design your app, consider how to put a bound on larger data so as to not exceed the limit. For example, if saving a game state requires 10KB each, the app might only allow the user store up to 10 games.
- Don't use roaming for data that relies on instant syncing. Windows doesn't guarantee an instant sync; roaming could be significantly delayed if a user is offline or on a high latency network. Ensure that your UI doesn't depend on instant syncing.
- Don't use roaming for frequently changing data. For example, if your app tracks frequently changing info, such as the position in a song by second, don't store this as roaming app data. Instead, pick a less frequent representation that still provides a good user experience, like the currently playing song.

### Roaming pre-requisites

> See important note about [roaming data](#roaming-data).

Any user can benefit from roaming app data if they use a Microsoft account to log on to their device. However, users and group policy administrators can switch off roaming app data on a device at any time. If a user chooses not to use a Microsoft account or disables roaming data capabilities, she will still be able to use your app, but app data will be local to each device.

Data stored in the [**PasswordVault**](/uwp/api/Windows.Security.Credentials.PasswordVault) will only transition if a user has made a device "trusted". If a device isn't trusted, data secured in this vault will not roam.

### Conflict resolution

> See important note about [roaming data](#roaming-data).

Roaming app data is not intended for simultaneous use on more than one device at a time. If a conflict arises during synchronization because a particular data unit was changed on two devices, the system will always favor the value that was written last. This ensures that the app utilizes the most up-to-date information. If the data unit is a setting composite, conflict resolution will still occur on the level of the setting unit, which means that the composite with the latest change will be synchronized.

### When to write data

> See important note about [roaming data](#roaming-data).

Depending on the expected lifetime of the setting, data should be written at different times. Infrequently or slowly changing app data should be written immediately. However, app data that changes frequently should only be written periodically at regular intervals (such as once every 5 minutes), as well as when the app is suspended. For example, a music app might write the "current song" settings whenever a new song starts to play, however, the actual position in the song should only be written on suspend.

### Excessive usage protection

> See important note about [roaming data](#roaming-data).

The system has various protection mechanisms in place to avoid inappropriate use of resources. If app data does not transition as expected, it is likely that the device has been temporarily restricted. Waiting for some time will usually resolve this situation automatically and no action is required.

### Versioning

> See important note about [roaming data](#roaming-data).

App data can utilize versioning to upgrade from one data structure to another. The version number is different from the app version and can be set at will. Although not enforced, it is highly recommended that you use increasing version numbers, since undesirable complications (including data loss)could occur if you try to transition to a lower data version number that represents newer data.

App data only roams between installed apps with the same version number. For example, devices on version 2 will transition data between each other and devices on version 3 will do the same, but no roaming will occur between a device running version 2 and a device running version 3. If you install a new app that utilized various version numbers on other devices, the newly installed app will sync the app data associated with the highest version number.

### Testing and tools

> See important note about [roaming data](#roaming-data).

Developers can lock their device in order to trigger a synchronization of roaming app data. If it seems that the app data does not transition within a certain time frame, please check the following items and make sure that:

- Your roaming data does not exceed the maximum size (see [**RoamingStorageQuota**](/uwp/api/windows.storage.applicationdata.roamingstoragequota) for details).
- Your files are closed and released properly.
- There are at least two devices running the same version of the app.

### Register to receive notification when roaming data changes

> See important note about [roaming data](#roaming-data).

To use roaming app data, you need to register for roaming data changes and retrieve the roaming data containers so you can read and write settings.

1.  Register to receive notification when roaming data changes.

    The [**DataChanged**](/uwp/api/windows.storage.applicationdata.datachanged) event notifies you when roaming data changes. This example sets `DataChangeHandler` as the handler for roaming data changes.

```csharp
void InitHandlers()
    {
       Windows.Storage.ApplicationData.Current.DataChanged += 
          new TypedEventHandler<ApplicationData, object>(DataChangeHandler);
    }

    void DataChangeHandler(Windows.Storage.ApplicationData appData, object o)
    {
       // TODO: Refresh your data
    }
```

2.  Get the containers for the app's settings and files.

    Use the [**ApplicationData.RoamingSettings**](/uwp/api/windows.storage.applicationdata.roamingsettings) property to get the settings and the [**ApplicationData.RoamingFolder**](/uwp/api/windows.storage.applicationdata.roamingfolder) property to get the files.

```csharp
Windows.Storage.ApplicationDataContainer roamingSettings = 
        Windows.Storage.ApplicationData.Current.RoamingSettings;
    Windows.Storage.StorageFolder roamingFolder = 
        Windows.Storage.ApplicationData.Current.RoamingFolder;
```

### Create and retrieve roaming settings

> See important note about [roaming data](#roaming-data).

Use the [**ApplicationDataContainer.Values**](/uwp/api/windows.storage.applicationdatacontainer.values) property to access the settings in the `roamingSettings` container we got in the previous section. This example creates a simple setting named `exampleSetting` and a composite value named `composite`.

```csharp
// Simple setting

roamingSettings.Values["exampleSetting"] = "Hello World";
// High Priority setting, for example, last page position in book reader app
roamingSettings.values["HighPriority"] = "65";

// Composite setting

Windows.Storage.ApplicationDataCompositeValue composite = 
    new Windows.Storage.ApplicationDataCompositeValue();
composite["intVal"] = 1;
composite["strVal"] = "string";

roamingSettings.Values["exampleCompositeSetting"] = composite;

```

This example retrieves the settings we just created.

```csharp
// Simple setting

Object value = roamingSettings.Values["exampleSetting"];

// Composite setting

Windows.Storage.ApplicationDataCompositeValue composite = 
   (Windows.Storage.ApplicationDataCompositeValue)roamingSettings.Values["exampleCompositeSetting"];

if (composite == null)
{
   // No data
}
else
{
   // Access data in composite["intVal"] and composite["strVal"]
}
```

### Create and retrieve roaming files

> See important note about [roaming data](#roaming-data).

To create and update a file in the roaming app data store, use the file APIs, such as [**Windows.Storage.StorageFolder.CreateFileAsync**](/uwp/api/windows.storage.storagefolder.createfileasync) and [**Windows.Storage.FileIO.WriteTextAsync**](/uwp/api/windows.storage.fileio.writetextasync). This example creates a file named `dataFile.txt` in the `roamingFolder` container and writes the current date and time to the file. The **ReplaceExisting** value from the [**CreationCollisionOption**](/uwp/api/Windows.Storage.CreationCollisionOption) enumeration indicates to replace the file if it already exists.

```csharp
async void WriteTimestamp()
{
   Windows.Globalization.DateTimeFormatting.DateTimeFormatter formatter = 
       new Windows.Globalization.DateTimeFormatting.DateTimeFormatter("longtime");

   StorageFile sampleFile = await roamingFolder.CreateFileAsync("dataFile.txt", 
       CreationCollisionOption.ReplaceExisting);
   await FileIO.WriteTextAsync(sampleFile, formatter.Format(DateTimeOffset.Now));
}
```

To open and read a file in the roaming app data store, use the file APIs, such as [**Windows.Storage.StorageFolder.GetFileAsync**](/uwp/api/windows.storage.storagefolder.getfileasync), [**Windows.Storage.StorageFile.GetFileFromApplicationUriAsync**](/uwp/api/windows.storage.storagefile.getfilefromapplicationuriasync), and [**Windows.Storage.FileIO.ReadTextAsync**](/uwp/api/windows.storage.fileio.readtextasync). This example opens the `dataFile.txt` file created in the previous section and reads the date from the file. For details on loading file resources from various locations, see [How to load file resources](/previous-versions/windows/apps/hh965322(v=win.10)).

```csharp
async void ReadTimestamp()
{
   try
   {
      StorageFile sampleFile = await roamingFolder.GetFileAsync("dataFile.txt");
      String timestamp = await FileIO.ReadTextAsync(sampleFile);
      // Data is contained in timestamp
   }
   catch (Exception)
   {
      // Timestamp not found
   }
}
```

## Temporary app data


The temporary app data store works like a cache. Its files do not roam and could be removed at any time. The System Maintenance task can automatically delete data stored at this location at any time. The user can also clear files from the temporary data store using Disk Cleanup. Temporary app data can be used for storing temporary information during an app session. There is no guarantee that this data will persist beyond the end of the app session as the system might reclaim the used space if needed. The location is available via the [**temporaryFolder**](/uwp/api/windows.storage.applicationdata.temporaryfolder) property.

### Retrieve the temporary data container

Use the [**ApplicationData.TemporaryFolder**](/uwp/api/windows.storage.applicationdata.temporaryfolder) property to get the files. The next steps use the `temporaryFolder` variable from this step.

```csharp
Windows.Storage.StorageFolder temporaryFolder = ApplicationData.Current.TemporaryFolder;
```

### Create and read temporary files

To create and update a file in the temporary app data store, use the file APIs, such as [**Windows.Storage.StorageFolder.CreateFileAsync**](/uwp/api/windows.storage.storagefolder.createfileasync) and [**Windows.Storage.FileIO.WriteTextAsync**](/uwp/api/windows.storage.fileio.writetextasync). This example creates a file named `dataFile.txt` in the `temporaryFolder` container and writes the current date and time to the file. The **ReplaceExisting** value from the [**CreationCollisionOption**](/uwp/api/Windows.Storage.CreationCollisionOption) enumeration indicates to replace the file if it already exists.


```csharp
async void WriteTimestamp()
{
   Windows.Globalization.DateTimeFormatting.DateTimeFormatter formatter = 
       new Windows.Globalization.DateTimeFormatting.DateTimeFormatter("longtime");

   StorageFile sampleFile = await temporaryFolder.CreateFileAsync("dataFile.txt", 
       CreateCollisionOption.ReplaceExisting);
   await FileIO.WriteTextAsync(sampleFile, formatter.Format(DateTimeOffset.Now));
}
```

To open and read a file in the temporary app data store, use the file APIs, such as [**Windows.Storage.StorageFolder.GetFileAsync**](/uwp/api/windows.storage.storagefolder.getfileasync), [**Windows.Storage.StorageFile.GetFileFromApplicationUriAsync**](/uwp/api/windows.storage.storagefile.getfilefromapplicationuriasync), and [**Windows.Storage.FileIO.ReadTextAsync**](/uwp/api/windows.storage.fileio.readtextasync). This example opens the `dataFile.txt` file created in the previous step and reads the date from the file. For details on loading file resources from various locations, see [How to load file resources](/previous-versions/windows/apps/hh965322(v=win.10)).

```csharp
async void ReadTimestamp()
{
   try
   {
      StorageFile sampleFile = await temporaryFolder.GetFileAsync("dataFile.txt");
      String timestamp = await FileIO.ReadTextAsync(sampleFile);
      // Data is contained in timestamp
   }
   catch (Exception)
   {
      // Timestamp not found
   }
}
```

## Organize app data with containers


To help you organize your app data settings and files, you create containers (represented by [**ApplicationDataContainer**](/uwp/api/Windows.Storage.ApplicationDataContainer) objects) instead of working directly with directories. You can add containers to the local, roaming, and temporary app data stores. Containers can be nested up to 32 levels deep.

To create a settings container, call the [**ApplicationDataContainer.CreateContainer**](/uwp/api/windows.storage.applicationdatacontainer.createcontainer) method. This example creates a local settings container named `exampleContainer` and adds a setting named `exampleSetting`. The **Always** value from the [**ApplicationDataCreateDisposition**](/uwp/api/Windows.Storage.ApplicationDataCreateDisposition) enumeration indicates that the container is created if it doesn't already exist.

```csharp
Windows.Storage.ApplicationDataContainer localSettings = 
    Windows.Storage.ApplicationData.Current.LocalSettings;
Windows.Storage.StorageFolder localFolder = 
    Windows.Storage.ApplicationData.Current.LocalFolder;

// Setting in a container
Windows.Storage.ApplicationDataContainer container = 
   localSettings.CreateContainer("exampleContainer", Windows.Storage.ApplicationDataCreateDisposition.Always);

if (localSettings.Containers.ContainsKey("exampleContainer"))
{
   localSettings.Containers["exampleContainer"].Values["exampleSetting"] = "Hello Windows";
}
```

## Delete app settings and containers


To delete a simple setting that your app no longer needs, use the [**ApplicationDataContainerSettings.Remove**](/uwp/api/windows.storage.applicationdatacontainersettings.remove) method. This example deletes the `exampleSetting` local setting that we created earlier.

```csharp
Windows.Storage.ApplicationDataContainer localSettings = 
    Windows.Storage.ApplicationData.Current.LocalSettings;
Windows.Storage.StorageFolder localFolder = 
    Windows.Storage.ApplicationData.Current.LocalFolder;

// Delete simple setting

localSettings.Values.Remove("exampleSetting");
```

To delete a composite setting, use the [**ApplicationDataCompositeValue.Remove**](/uwp/api/windows.storage.applicationdatacompositevalue.remove) method. This example deletes the local `exampleCompositeSetting` composite setting we created in an earlier example.

```csharp
Windows.Storage.ApplicationDataContainer localSettings = 
    Windows.Storage.ApplicationData.Current.LocalSettings;
Windows.Storage.StorageFolder localFolder = 
    Windows.Storage.ApplicationData.Current.LocalFolder;

// Delete composite setting

localSettings.Values.Remove("exampleCompositeSetting");
```

To delete a container, call the [**ApplicationDataContainer.DeleteContainer**](/uwp/api/windows.storage.applicationdatacontainer.deletecontainer) method. This example deletes the local `exampleContainer` settings container we created earlier.

```csharp
Windows.Storage.ApplicationDataContainer localSettings = 
    Windows.Storage.ApplicationData.Current.LocalSettings;
Windows.Storage.StorageFolder localFolder = 
    Windows.Storage.ApplicationData.Current.LocalFolder;

// Delete container

localSettings.DeleteContainer("exampleContainer");
```

## Versioning your app data


You can optionally version the app data for your app. This would enable you to create a future version of your app that changes the format of its app data without causing compatibility problems with the previous version of your app. The app checks the version of the app data in the data store, and if the version is less than the version the app expects, the app should update the app data to the new format and update the version. For more info, see the[**Application.Version**](/uwp/api/windows.storage.applicationdata.version) property and the [**ApplicationData.SetVersionAsync**](/uwp/api/windows.storage.applicationdata.setversionasync) method.

## Related articles

* [**Windows.Storage.ApplicationData**](/uwp/api/Windows.Storage.ApplicationData)
* [**Windows.Storage.ApplicationData.RoamingSettings**](/uwp/api/windows.storage.applicationdata.roamingsettings)
* [**Windows.Storage.ApplicationData.RoamingFolder**](/uwp/api/windows.storage.applicationdata.roamingfolder)
* [**Windows.Storage.ApplicationData.RoamingStorageQuota**](/uwp/api/windows.storage.applicationdata.roamingstoragequota)
* [**Windows.Storage.ApplicationDataCompositeValue**](/uwp/api/Windows.Storage.ApplicationDataCompositeValue)
