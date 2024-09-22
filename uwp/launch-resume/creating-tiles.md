---
description: A tile is an app's representation on the Start menu. Every app has a tile. When you create a new Windows app project in Microsoft Visual Studio, it includes a default tile that displays your app's name and logo.
title: Tiles for Windows apps
ms.assetid: 09C7E1B1-F78D-4659-8086-2E428E797653
label: Tiles
template: detail.hbs
ms.date: 08/08/2024
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Tiles for Windows apps

[!INCLUDE [notes](includes/live-tiles-note.md)]

A *tile* is an app's representation on the Start menu. Every app has a tile. When you create a new Windows app project in Microsoft Visual Studio, it includes a default tile that displays your app's name and logo. Windows displays this tile when your app is first installed. After your app is installed, you can change your tile's content through notifications; for example, you can change the tile to communicate new information to the user, such as news headlines, or the subject of the most recent unread message.

## Configure the default tile

When you create a new project in Visual Studio, it creates a simple default tile that displays your app's name and logo.

To edit your tile, double click the **Package.appxmanifest** file in your main UWP project to open the designer (or right click the file and select View Code).

```xml
  <Applications>
    <Application Id="App"
      Executable="$targetnametoken$.exe"
      EntryPoint="ExampleApp.App">
      <uap:VisualElements
        DisplayName="ExampleApp"
        Square150x150Logo="Assets\Square150x150Logo.png"
        Square44x44Logo="Assets\Square44x44Logo.png"
        Description="ExampleApp"
        BackgroundColor="#464646">
        <uap:SplashScreen Image="Assets\SplashScreen.png" />
      </uap:VisualElements>
    </Application>
  </Applications>
```

There are a few items you should update:

- DisplayName: Replace this value with the name you want to display on your tile.
- ShortName: Because there is limited room for your display name to fit on tiles, we recommend that you to specify a ShortName as well, to make sure your app's name doesn’t get truncated.
- Logo images:

    You should replace these images with your own. You have the option of supplying images for different visual scales, but you are not required to supply them all. To ensure that you app looks good on a range of devices, we recommend that you provide 100%, 200%, and 400% scale versions of each image. See [Construct your Windows app's icon](/windows/apps/design/style/iconography/app-icon-construction) to learn more about generating these assets.

    Scaled images follow this naming convention:

    *&lt;image name&gt;*.scale-*&lt;scale factor&gt;*.*&lt;image file extension&gt;*

    For example: SplashScreen.scale-100.png

    When you refer to the image, you refer to it as *&lt;image name&gt;*.*&lt;image file extension&gt;* ("SplashScreen.png" in this example). The system will automatically select the appropriate scaled image for the device from the images you've provided.

- You don't have to, but we highly recommend supplying logos for wide and large tile sizes so that the user can resize your app's tile to those sizes. To provide these additional images, you create a **DefaultTile** element and use the **Wide310x150Logo** and **Square310x310Logo** attributes to specify the additional images:

```xml
  <Applications>
        <Application Id="App"
          Executable="$targetnametoken$.exe"
          EntryPoint="ExampleApp.App">
          <uap:VisualElements
            DisplayName="ExampleApp"
            Square150x150Logo="Assets\Square150x150Logo.png"
            Square44x44Logo="Assets\Square44x44Logo.png"
            Description="ExampleApp"
            BackgroundColor="#464646">
            <uap:DefaultTile
              Wide310x150Logo="Assets\Wide310x150Logo.png"
              Square310x310Logo="Assets\Square310x310Logo.png">
            </uap:DefaultTile>
            <uap:SplashScreen Image="Assets\SplashScreen.png" />
          </uap:VisualElements>
        </Application>
      </Applications>
```

## Use notifications to customize your tile

After your app is installed, you can use notifications to customize your tile. You can do this the first time your app launches or in response to an event, such as a push notification.

To learn how to send tile notifications, see [Send a local tile notification](sending-a-local-tile-notification.md).
