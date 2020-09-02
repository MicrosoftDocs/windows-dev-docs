---
title: Porting WPSL business and data layers to UWP
description: Learn how to port the business and data layers from a Windows Phone Silverlight app to the Universal Windows Platform (UWP).
ms.assetid: 27c66759-2b35-41f5-9f7a-ceb97f4a0e3f
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Porting Windows Phone Silverlight business and data layers to UWP

The previous topic was [Porting for I/O, device, and app model](wpsl-to-uwp-input-and-sensors.md).

Behind your UI are your business and data layers. The code in these layers calls operating system and .NET Framework APIs (for example, background processing, location, the camera, the file system, network, and other data access). The vast majority of those are [available to a Universal Windows Platform (UWP) app](/previous-versions/windows/br211369(v=win.10)), so you can expect to be able to port much of this code without change.

## Asynchronous methods

One of the priorities of the Universal Windows Platform (UWP) is to enable you to build apps that are truly, and consistently, responsive. Animations are always smooth, and touch interactions such as panning and swiping are instantaneous and free of lag, making it feel like the UI is glued to your finger. To achieve this, any UWP API that can't guarantee to complete within 50ms has been made asynchronous and its name suffixed with **Async**. Your UI thread will return immediately from calling an **Async** method, and the work will take place on another thread. Consuming an **Async** method is made very easy, syntactically, using the C# **await** operator, JavaScript promise objects, and C++ continuations. For more info, see [Asynchronous programming](../threading-async/asynchronous-programming-universal-windows-platform-apps.md).

## Background processing

A Windows Phone Silverlight app can use a managed **ScheduledTaskAgent** object to perform a task while the app is not in the foreground. A UWP app uses the [**BackgroundTaskBuilder**](/uwp/api/Windows.ApplicationModel.Background.BackgroundTaskBuilder) class to create and register a background task in a similar way. You define a class that implements the work of your background task. The system runs your background task periodically, calling the [**Run**](/uwp/api/windows.applicationmodel.background.ibackgroundtask.run) method of your class to execute the work. In a UWP app, remember to set the **Background Tasks** declaration in the app package manifest. For more info, see [Support your app with background tasks](../launch-resume/support-your-app-with-background-tasks.md).

To transfer large data files in the background, a Windows Phone Silverlight app uses the **BackgroundTransferService** class. A UWP app uses APIs in the [**Windows.Networking.BackgroundTransfer**](/uwp/api/Windows.Networking.BackgroundTransfer) namespace to do this. The features use a similar pattern to initiate transfers, but the new API has improved capabilities and performance. For more info, see [Transferring data in the background](/previous-versions/windows/apps/hh452975(v=win.10)).

A Windows Phone Silverlight app uses the managed classes in the **Microsoft.Phone.BackgroundAudio** namespace to play audio while the app is not in the foreground. The UWP uses the Windows Phone Store app model, see [Background Audio](../audio-video-camera/background-audio.md) and the [Background audio](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/BackgroundAudio) sample.

## Cloud services, networking, and databases

Hosting data and app services in the cloud is possible using Azure. See [Getting Started with Mobile Services](/azure/). For solutions that require both online and offline data see: [Using offline data sync in Mobile Services](/azure/).

The UWP has partial support for the **System.Net.HttpWebRequest** class, but the **System.Net.WebClient** class is not supported. The recommended, forward-looking alternative is the [**Windows.Web.Http.HttpClient**](/uwp/api/Windows.Web.Http.HttpClient) class (or [System.Net.Http.HttpClient](/previous-versions/visualstudio/hh193681(v=vs.118)) if you need your code to be portable to other platforms that support .NET). These APIs use [System.Net.Http.HttpRequestMessage](/previous-versions/visualstudio/hh159020(v=vs.118)) to represent an HTTP request.

UWP apps do not currently include built-in support for data-intensive scenarios such as line of business (LOB) scenarios. However, you can make use SQLite for local transactional database services. For more info, see [SQLite](https://marketplace.visualstudio.com/items?itemName=SQLiteDevelopmentTeam.SQLiteforUniversalWindowsPlatform).

Pass absolute URIs, not relative URIs, to Windows Runtime types. See [Passing a URI to the Windows Runtime](/dotnet/standard/cross-platform/passing-a-uri-to-the-windows-runtime).

## Launchers and Choosers

With Launchers and Choosers (found in the **Microsoft.Phone.Tasks** namespace), a Windows Phone Silverlight app can interact with the operating system to perform common operations such as composing an email, choosing a photo, or sharing certain kinds of data with another app. Search for **Microsoft.Phone.Tasks** in the topic [Windows Phone Silverlight to Windows 10 namespace and class mappings](wpsl-to-uwp-namespace-and-class-mappings.md) to find the equivalent UWP type. These range from similar mechanisms, called launchers and pickers, to implementing a contract for sharing data between apps.

A Windows Phone Silverlight app can be put into a dormant state or even tombstoned when using, for example, the photo Chooser task. A UWP app remains active and running while using the [**FileOpenPicker**](/uwp/api/Windows.Storage.Pickers.FileOpenPicker) class.

## Monetization (trial mode and in-app purchases)

A Windows Phone Silverlight app can use the UWP [**CurrentApp**](/uwp/api/Windows.ApplicationModel.Store.CurrentApp) class for most of its trial mode and in-app purchase functionality, so that code doesn't need to be ported. But, a Windows Phone Silverlight app calls **MarketplaceDetailTask.Show** to offer the app for purchase:

```csharp
    private void Buy()
    {
        MarketplaceDetailTask marketplaceDetailTask = new MarketplaceDetailTask();
        marketplaceDetailTask.ContentType = MarketplaceContentType.Applications;
        marketplaceDetailTask.Show();
    }
```

Port that code to call the UWP [**RequestAppPurchaseAsync**](/uwp/api/windows.applicationmodel.store.currentapp.requestapppurchaseasync) method:

```csharp
    private async void Buy()
    {
        await Windows.ApplicationModel.Store.CurrentApp.RequestAppPurchaseAsync(false);
    }
```

If you have code that simulates your app purchase and in-app purchase features for testing purposes, then you can port that to use the [**CurrentAppSimulator**](/uwp/api/Windows.ApplicationModel.Store.CurrentAppSimulator) class instead.

## Notifications for tile or toast updates

Notifications are an extension of the push notification model for Windows Phone Silverlight apps. When you receive a notification from the Windows Push Notification Service (WNS), you can surface the info to the UI with a tile update or with a toast. For porting the UI side of your notification features, see [Tiles and toasts](w8x-to-uwp-porting-xaml-and-ui.md).

For more details on the use of notifications in a UWP app, see [Sending toast notifications](/previous-versions/windows/apps/hh868266(v=win.10)).

For info and tutorials on using tiles, toasts, badges, banners, and notifications in a Windows Runtime app using C++, C#, or Visual Basic, see [Working with tiles, badges, and toast notifications](/previous-versions/windows/apps/hh868259(v=win.10)).

## Storage (file access)

Windows Phone Silverlight code that stores app settings as key-value pairs in isolated storage is easily ported. Here is a before-and-after example, first the Windows Phone Silverlight version:

```csharp
    var propertySet = IsolatedStorageSettings.ApplicationSettings;
    const string key = "favoriteAuthor";
    propertySet[key] = "Charles Dickens";
    propertySet.Save();
    string myFavoriteAuthor = propertySet.Contains(key) ? (string)propertySet[key] : "<none>";
```

And the UWP equivalent:

```csharp
    var propertySet = Windows.Storage.ApplicationData.Current.LocalSettings.Values;
    const string key = "favoriteAuthor";
    propertySet[key] = "Charles Dickens";
    string myFavoriteAuthor = propertySet.ContainsKey(key) ? (string)propertySet[key] : "<none>";
```

Although a subset of the **Windows.Storage** namespace is available to them, many Windows Phone Silverlight apps perform file i/o with the **IsolatedStorageFile** class because it has been supported for longer. Assuming that **IsolatedStorageFile** is being used, here's a before-and-after example of writing and reading a file, first the Windows Phone Silverlight version:

```csharp
    const string filename = "FavoriteAuthor.txt";
    using (var store = IsolatedStorageFile.GetUserStoreForApplication())
    {
        using (var streamWriter = new StreamWriter(store.CreateFile(filename)))
        {
            streamWriter.Write("Charles Dickens");
        }
        using (var StreamReader = new StreamReader(store.OpenFile(filename, FileMode.Open, FileAccess.Read)))
        {
            string myFavoriteAuthor = StreamReader.ReadToEnd();
        }
    }
```

And the same functionality using the UWP:

```csharp
    const string filename = "FavoriteAuthor.txt";
    var store = Windows.Storage.ApplicationData.Current.LocalFolder;
    Windows.Storage.StorageFile file = await store.CreateFileAsync(filename, Windows.Storage.CreationCollisionOption.ReplaceExisting);
    await Windows.Storage.FileIO.WriteTextAsync(file, "Charles Dickens");
    file = await store.GetFileAsync(filename);
    string myFavoriteAuthor = await Windows.Storage.FileIO.ReadTextAsync(file);
```

A Windows Phone Silverlight app has read-only access to the optional SD card. A UWP app has read-write access to the SD card. For more info, see [Access the SD card](../files/access-the-sd-card.md).

For info about accessing photos, music, and video files in a UWP app, see [Files and folders in the Music, Pictures, and Videos libraries](../files/quickstart-managing-folders-in-the-music-pictures-and-videos-libraries.md).

For more info, see [Files, folders, and libraries](../files/index.md).

The next topic is [Porting for form factor and UX](wpsl-to-uwp-form-factors-and-ux.md).

## Related topics

* [Namespace and class mappings](wpsl-to-uwp-namespace-and-class-mappings.md)
 