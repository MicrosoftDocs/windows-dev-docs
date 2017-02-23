---
author: mijacobs
Description: A tile is an app's representation on the Start menu. Every app has a tile. When you create a new Universal Windows Platform (UWP) app project in Microsoft Visual Studio, it includes a default tile that displays your app's name and logo.
title: Tiles
ms.assetid: 09C7E1B1-F78D-4659-8086-2E428E797653
label: Tiles
template: detail.hbs
ms.author: mijacobs
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---
# Tiles for UWP apps

<link rel="stylesheet" href="https://az835927.vo.msecnd.net/sites/uwp/Resources/css/custom.css"> 

A *tile* is an app's representation on the Start menu. Every app has a tile. When you create a new Universal Windows Platform (UWP) app project in Microsoft Visual Studio, it includes a default tile that displays your app's name and logo. Windows displays this tile when your app is first installed. After your app is installed, you can change your tile's content through notifications; for example, you can change the tile to communicate new information to the user, such as news headlines, or the subject of the most recent unread message.

## Configure the default tile


When you create a new project in Visual Studio, it creates a simple default tile that displays your app's name and logo.

```XML
  <Applications>
    <Application Id="App"
      Executable="$targetnametoken$.exe"
      EntryPoint="ExampleApp.App">
      <uap:VisualElements
        DisplayName="ExampleApp"
        Square150x150Logo="Assets\Logo.png"
        Square44x44Logo="Assets\SmallLogo.png"
        Description="ExampleApp"
        BackgroundColor="#464646">
        <uap:SplashScreen Image="Assets\SplashScreen.png" />
      </uap:VisualElements>
    </Application>
  </Applications>
```

There are a few items you should update:

-   DisplayName: Replace this value with the name you want to display on your tile.
-   ShortName: Because there is limited room for your display name to fit on tiles, we recommend that you to specify a ShortName as well, to make sure your app's name doesn’t get truncated.
-   Logo images:

    You should replace these images with your own. You have the option of supplying images for different visual scales, but you are not required to supply them all. To ensure that you app looks good on a range of devices, we recommend that you provide 100%, 200%, and 400% scale versions of each image.

    Scaled images follow this naming convention: testing
    
    *&lt;image name&gt;*.scale-*&lt;scale factor&gt;*.*&lt;image file extension&gt;* 

    For example: SmallLogo.scale-100.png

    When you refer to the image, you refer to it as *&lt;image name&gt;*.*&lt;image file extension&gt;* ("SmallLogo.png" in this example). The system will automatically select the appropriate scaled image for the device from the images you've provided.

-   You don't have to, but we highly recommend supplying logos for wide and large tile sizes so that the user can resize your app's tile to those sizes. To provide these additional images, you create a `DefaultTile` element and use the `Wide310x150Logo` and `Square310x310Logo` attributes to specify the additional images:
```    XML
  <Applications>
        <Application Id="App"
          Executable="$targetnametoken$.exe"
          EntryPoint="ExampleApp.App">
          <uap:VisualElements
            DisplayName="ExampleApp"
            Square150x150Logo="Assets\Logo.png"
            Square44x44Logo="Assets\SmallLogo.png"
            Description="ExampleApp"
            BackgroundColor="#464646">
            <uap:DefaultTile
              Wide310x150Logo="Assets\WideLogo.png"
              Square310x310Logo="Assets\LargeLogo.png">
            </uap:DefaultTile>
            <uap:SplashScreen Image="Assets\SplashScreen.png" />
          </uap:VisualElements>
        </Application>
      </Applications>
```

## Use notifications to customize your tile


After your app is installed, you can use notifications to customize your tile. You can do this the first time your app launches or in response to some event, such as a push notification.

1.  Create an XML payload (in the form of an [**Windows.Data.Xml.Dom.XmlDocument**](https://msdn.microsoft.com/library/windows/apps/br206173)) that describes the tile.

    -   Windows 10 introduces a new adaptive tile schema you can use. For instructions, see [Adaptive tiles](tiles-and-notifications-create-adaptive-tiles.md). For the schema, see the [Adaptive tiles schema](tiles-and-notifications-adaptive-tiles-schema.md). 

    -   You can use the Windows 8.1 tile templates to define your tile. For more info, see [Creating tiles and badges (Windows 8.1)](https://msdn.microsoft.com/library/windows/apps/xaml/hh868260).

2.  Create a tile notification object and pass it the [**XmlDocument**](https://msdn.microsoft.com/library/windows/apps/br206173) you created. There are several types of notification objects:
    -   A [**Windows.UI.NotificationsTileNotification**](https://msdn.microsoft.com/library/windows/apps/br208616) object for updating the tile immediately.
    -   A [**Windows.UI.Notifications.ScheduledTileNotification**](https://msdn.microsoft.com/library/windows/apps/hh701637) object for updating the tile at some point in the future.

3.  Use the [**Windows.UI.Notifications.TileUpdateManager.CreateTileUpdaterForApplication**](https://msdn.microsoft.com/library/windows/apps/br208623) to create a [**TileUpdater**](https://msdn.microsoft.com/library/windows/apps/br208628) object.
4.  Call the [**TileUpdater.Update**](https://msdn.microsoft.com/library/windows/apps/br208632) method and pass it the tile notification object you created in step 2.

 

 




