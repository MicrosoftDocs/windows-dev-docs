---
description: This article describes the UI elements that can be used in an app notification and provides code examples for generating the XML format for an app notification.
title: App notification content
ms.assetid: 1FCE66AF-34B4-436A-9FC9-D0CF4BDA5A01
label: App notification content
template: detail.hbs
ms.date: 11/14/2022
ms.topic: article
keywords: windows 10, uwp, toast notifications, interactive toasts, adaptive toasts, toast content, toast payload, app notifications
ms.localizationpriority: medium
---
# App notification content

App notifications are flexible notifications with text, images, and buttons/inputs. This article describes the UI elements that can be used in an app notification and provides code examples for generating the XML format for an app notification.

> [!NOTE]
> The term "toast notification" is being replaced with "app notification". These terms both refer to the same feature of Windows, but over time we will phase out the use of "toast notification" in the documentation.

## Getting started

App notifications are defined with an XML payload which is defined by the [App notification schema](/uwp/schemas/tiles/toastschema/schema-root). Currently, there are three ways of generating the XML payload fo an app notification. The code examples in this article demonstrate all three methods:

- The [Microsoft.Windows.AppNotifications.Builder](/windows/windows-app-sdk/api/winrt/microsoft.windows.appnotifications.builder) APIs - Introduced in Windows App SDK 1.2, this namespace provides APIs that allow you to easily build the XML payload for a notification programmatically without having to worry about the specifics of the XML format. Code examples using these APIs are in the tabs labeled "Windows App SDK".
- The `Microsoft.Toolkit.Uwp.Notifications` builder syntax - These APIs are part of the UWP Community Toolkit and provides support for UWP apps.  Although these APIs can also be used for Windows App SDK apps, and continue to be supported, we recommend that new implementations use the **Microsoft.Windows.AppNotifications.Builder** APIs. To use Community Toolkit APIs, add the [UWP Community Toolkit Notifications nuget package](https://www.nuget.org/packages/Microsoft.Toolkit.Uwp.Notifications/) to your project. The C# samples provided in this article use version 7.0.0 of the NuGet package. Code examples using these APIs are in the tabs labeled "Windows Community Toolkit".
- Raw XML - If you prefer, you can create your own custom code to generate XML strings in the required format. The raw XML examples are in the tabs labeled "XML".

**Install Notifications Visualizer.** This free Windows app helps you design interactive app notifications by providing an instant visual preview of your toast as you edit it, similar to Visual Studio's XAML editor/design view. See [Notifications Visualizer](notifications-visualizer.md) for more information, or [download Notifications Visualizer from the Store](https://www.microsoft.com/store/apps/notifications-visualizer/9nblggh5xsl1).

This article only covers creating the app notification content. For information on sending a notification after you have generated the XML payload, see [Send a local app notification](send-local-toast.md). 

## App notification structure

Some important, high-level components of an app notification XML payload include:

- **toast**: The **launch** attribute of this element defines what arguments will be passed back to your app when the user clicks your toast, allowing you to deep link into the correct content that the toast was displaying. To learn more, see [Send a local app notification](send-local-toast.md).
- **visual**: This element represents visual portion of the toast, including the generic binding that contains text and images.
- **actions**: This element represents interactive portion of the toast, including inputs and actions.
- **audio**: This element specifies the audio played when the toast is shown to the user.

### [Windows App SDK](#tab/appsdk)

```csharp
var builder = new AppNotificationBuilder()
    .AddArgument("conversationId", "9813")

    .AddText("Some text")

    .AddButton(new AppNotificationButton("Archive")
        .AddArgument("action", "archive"))

    .SetAudioUri(new Uri("ms-appx:///Sound.mp3"));
```

### [Community Toolkit](#tab/toolkit)

```csharp
var builder = new ToastContentBuilder()
    .AddArgument("conversationId", 9813)

    .AddText("Some text")

    .AddButton(new ToastButton()
        .SetContent("Archive")
        .AddArgument("action", "archive"))

    .AddAudio(new Uri("ms-appx:///Audio/NotificationSound.mp3"));
```

### [XML](#tab/xml)

```xml
<toast launch='conversationId=9813'>
    <visual>
        <binding template='ToastGeneric'>
            <text>Some text</text>
        </binding>
    </visual>
    <audio src='ms-appx:///Audio/NotificationSound.mp3'/>
    <actions>
        <action content='Archive' arguments='action=archive'/>
    </actions>
</toast>
```

---

Here is a visual representation of the app notification's content:

![An screenshot of an app notification with labels for the attribution area at the top showing the app icon and and app name Notifications Visualizer. The middle part of the toast is labeled as the visual area, which includes three lines of text. The bottom section of the toast is labeled as the action area and contains two buttons labeled Accept and Decline. ](images/toast-content-structure.png)


## Attribution area

The attribution area is at the top of the app notification. Starting with Windows 11, your app's name and icon are displayed in this area. The attribution area also includes a close button that allows the user to quickly dismiss the notification and an ellipses menu that allows the user to quickly disable notifications for your app or go to the Windows Settings page for your app's notifications. The attribution area is configured by the shell and can't be overridden in the toast XML payload, although your app can add items to the attribution area context menu. For more information see [Context menu actions](#context-menu-actions).

## Visual

Each app notification must specify a **visual** element, where you must provide a generic toast binding and which can contain text and images. These elements will be rendered on various Windows devices, including desktop, phones, tablets, and Xbox.

For all attributes supported in the **visual** section and its child elements, see [App notification schema](toast-schema.md#toastvisual).

## Text elements

Each app notification must have at least one text element, and can contain two additional text elements, all of type [**AdaptiveText**](toast-schema.md#adaptivetext).

![A screenshot of an app notification with three lines of text. The top line of text is bold.](images/toast-content-text-elements.png)

Since the Windows 10 Anniversary Update, you can control how many lines of text are displayed by using the **HintMaxLines** property on the text. The default (and maximum) is up to 2 lines of text for the title, and up to 4 lines (combined) for the two additional description elements (the second and third **AdaptiveText**).


### [Windows App SDK](#tab/appsdk)

```csharp
var builder = new AppNotificationBuilder()
    .AddArgument("conversationId", 9813)
    .AddText("Adaptive Tiles Meeting", new AppNotificationTextProperties().SetMaxLines(1))
    .AddText("Conf Room 2001 / Building 135")
    .AddText("10:00 AM - 10:30 AM");
```

### [Community Toolkit](#tab/toolkit)

```csharp
var builder = new ToastContentBuilder()
    .AddArgument("meetingId", 9813)
    .AddText("Adaptive Tiles Meeting", hintMaxLines: 1)
    .AddText("Conf Room 2001 / Building 135")
    .AddText("10:00 AM - 10:30 AM");
```

### [XML](#tab/xml)

```xml
<toast launch='meetingId=9813'>
    <visual>
        <binding template='ToastGeneric'>
            <text hint-maxLines='1'>Adaptive Tiles Meeting</text>
            <text>Conf Room 2001 / Building 135</text>
            <text>10:00 AM - 10:30 AM</text>
        </binding>
    </visual>
</toast>
```

---

## Inline image

By default, images are displayed inline, after any text elements, filling the full width of the visual area.

![A screenshot of an app notification showing the default image placement, inline, filling the full width of the visual area.](images/toast-content-inline-image.png)

### [Windows App SDK](#tab/appsdk)

```csharp
var builder = new AppNotificationBuilder()
    .AddText("Featured image of the day.")
    .SetInlineImage(new Uri("ms-appx:///Images/InlineImage.png"));

AppNotificationManager.Default.Show(builder.BuildNotification());
```

### [Community Toolkit](#tab/toolkit)

```csharp
var builder = new ToastContentBuilder()
    .AddText("Featured image of the day.")
    .AddInlineImage(new Uri("ms-appx:///Images/InlineImage.png"));
```

### [XML](#tab/xml)

```xml
<toast>
    <visual>
        <binding template='ToastGeneric'>
            <text>Featured image of the day.</text>
            <image src='ms-appx:///Images/InlineImage.png'/>
        </binding>
    </visual>
</toast>
```

---


## App logo override

Specifying a **placement** value of "appLogoOverride" will cause the image to be displayed in a square on the left side of the visual area. The name of this property reflects the behavior in previous versions of Windows, where the image would replace the default app logo image. In Windows 11, the app logo is displayed in the attribution area, so it is not overridden by the **appLogoOverride** image placement.

Image dimensions are 48x48 pixels at 100% scaling. We generally recommend providing a version each icon asset for each scale factor: 100%, 125%, 150%, 200%, and 400%. 

![A screenshot of an app notification showing the app logo override image placement in a square on the left side of the visual area of the notification.](images/toast-content-applogooverride.png)

### [Windows App SDK](#tab/appsdk)

```csharp
var builder = new AppNotificationBuilder()
    .AddText("Featured image of the day.")
    .SetAppLogoOverride(new Uri("ms-appx:///Images/AppLogo.png"));
```

### [Community Toolkit](#tab/toolkit)

```csharp
var builder = new ToastContentBuilder()
    .AddText("Featured image of the day.")
    .AddAppLogoOverride(new Uri("ms-appx:///Images/AppLogo.png"));
```

#### [XML](#tab/xml)

```xml
<toast>
    <visual>
        <binding template='ToastGeneric'>
            <text>Featured image of the day.</text>
            <image placement='appLogoOverride' src='ms-appx:///Images/AppLogo.png'/>
        </binding>
    </visual>
</toast>
```

---

## Hint crop

Microsoft style guidelines recommend representing profile pictures with a circular image to provide a consistent representation of people across apps and the shell. Set the **HintCrop** property to **Circle** to render the image with a circular crop.  

![A screenshot of an app notification showing the app logo override image placement cropped into a circle on the left side of the visual area of the toast.](images/toast-content-hint-crop.png)

### [Windows App SDK](#tab/appsdk)

```csharp
var builder = new AppNotificationBuilder()
    .AddText("Matt sent you a friend request")
    .AddText("Hey, wanna dress up as wizards and ride around on hoverboards?")
    .SetAppLogoOverride(new Uri("ms-appx:///Images/Profile.png"), AppNotificationImageCrop.Circle);
```

### [Community Toolkit](#tab/toolkit)

```csharp
var builder = new ToastContentBuilder()
    .AddText("Matt sent you a friend request")
    .AddText("Hey, wanna dress up as wizards and ride around on hoverboards?")
    .AddAppLogoOverride(new Uri("ms-appx:///Images/Profile.png"), ToastGenericAppLogoCrop.Circle);
```

### [XML](#tab/xml)

```xml
<toast>
    <visual>
        <binding template='ToastGeneric'>
            <text>Matt sent you a friend request</text>
            <text>Hey, wanna dress up as wizards and ride around on hoverboards?</text>
            <image placement='appLogoOverride' src='ms-appx:///Images/Profile.png' hint-crop='circle'/>
        </binding>
    </visual>
</toast>
```

---

## Hero image

**New in Anniversary Update**: App notifications can display a hero image, which is a featured [**ToastGenericHeroImage**](toast-schema.md#toastgenericheroimage) displayed prominently within the toast banner and while inside Notification Center. Image dimensions are 364x180 pixels at 100% scaling.

![A screenshot of an app notification showing the hero image placement, above the attribution area.](images/toast-content-hero-image.png)

### [Windows App SDK](#tab/appsdk)

```csharp
var builder = new AppNotificationBuilder()
    .AddText("Marry Anne")
    .AddText("Check out where we camped last night!")
    .SetHeroImage(new Uri("ms-appx:///Images/HeroImage.png"));
```

### [Community Toolkit](#tab/toolkit)

```csharp
new ToastContentBuilder()
    .AddText("Marry Anne")
    .AddText("Check out where we camped last night!")
    .AddHeroImage(new Uri("ms-appx:///Images/HeroImage.png"));
```

#### [XML](#tab/xml)

```xml
<toast>
    <visual>
        <binding template='ToastGeneric'>
            <text>Mary Anne</text>
            <text>Check out where we camped last night!</text>
            <image placement='hero' src='ms-appx:///Images/HeroImage.png'/>
        </binding>
    </visual>
</toast>
```

---

## Image size restrictions

The images you use in your toast notification can be sourced from...

 - http://
 - ms-appx:///
 - ms-appdata:///

For http and https remote web images, there are limits on the file size of each individual image. In the Fall Creators Update (16299), we increased the limit to be 3 MB on normal connections and 1 MB on metered connections. Before that, images were always limited to 200 KB.

| Normal connection | Metered connection | Before Fall Creators Update |
| - | - | - |
| 3 MB | 1 MB | 200 KB |

If an image exceeds the file size, or fails to download, or times out, the image will be dropped and the rest of the notification will be displayed.


## Attribution text

**New in Anniversary Update**: If you need to reference the source of your content, you can use attribution text. This text is always displayed below any text elements, but above inline images. The text uses a slightly smaller size than standard text elements to help to distinguish from regular text elements.

On older versions of Windows that don't support attribution text, the text will simply be displayed as another text element (assuming you don't already have the maximum of three text elements).


![A screenshot of a toast notification showing the attribution text "via SMS" below the other lines of text in the visual area of the toast.](images/toast-content-attribution-text.png)

### [Windows App SDK](#tab/appsdk)

```csharp
var builder = new AppNotificationBuilder()
    .AddText("Marry Anne")
    .AddText("Check out where we camped last night!")
    .SetAttributionText("via SMS");
    .SetHeroImage(new Uri("ms-appx:///Images/HeroImage.png"));
```

### [Community Toolkit](#tab/toolkit)

```csharp
new ToastContentBuilder()
    .AddText("Marry Anne")
    .AddText("Check out where we camped last night!")
    .AddAttributionText("Via SMS");
    .AddHeroImage(new Uri("ms-appx:///Images/HeroImage.png"));
```

### [XML](#tab/xml)

```xml
<toast>  
  <visual>
    <binding template="ToastGeneric">
      <image src="ms-appx:///Images/HeroImage.png"/>
      <text>Mary Anne</text>
      <text>Check out where we camped last night!</text>
      <text placement="attribution">Via SMS</text>
    </binding>
  </visual>
</toast>
```

---


## Custom timestamp

**New in Creators Update**: You can now override the system-provided timestamp with your own timestamp that accurately represents when the message/information/content was generated. This timestamp is visible within Notification Center.

![Screenshot of a notification in the Notifications Center with a custom timestamp](images/toast-content-custom-timestamp.png)

To learn more about using a custom timestamp, please see [custom timestamps on toasts](custom-timestamps-on-toasts.md).

### [Windows App SDK](#tab/appsdk)

```csharp
var builder = new AppNotificationBuilder()
    .AddText("Matt sent you a friend request")
    .AddText("Hey, wanna dress up as wizards and ride around on hoverboards?")
    .SetTimeStamp(new DateTime(2017, 04, 15, 19, 45, 00, DateTimeKind.Utc));
```

### [Community Toolkit](#tab/toolkit)

```csharp
var builder = new ToastContentBuilder()
    .AddText("Matt sent you a friend request")
    .AddText("Hey, wanna dress up as wizards and ride around on hoverboards?")
    .AddCustomTimeStamp(new DateTime(2017, 04, 15, 19, 45, 00, DateTimeKind.Utc));
```

### [XML](#tab/xml)

```xml
<toast displayTimestamp='2017-04-15T12:45:00-07:00'>
    <visual>
        <binding template='ToastGeneric'>
            <text>Matt sent you a friend request</text>
            <text>Hey, wanna dress up as wizards and ride around on hoverboards?</text>
        </binding>
    </visual>
</toast>
```

---


## Progress bar

**New in Creators Update**: You can provide a progress bar on your app notification to keep the user informed of the progress of operations such as downloads.

![A screenshot of a toast notification showing a progress bar.](images/toast-content-progress-bar.png)

To learn more about using a progress bar, please see [Toast progress bar](toast-progress-bar.md).


## Headers

**New in Creators Update**: You can group notifications under headers within Notification Center. For example, you can group messages from a group chat under a header, or group notifications of a common theme under a header, or more.

![A screenshot of a action center showing multiple notifications for the application Notifications Viewer organized under a header labeled "Camping!".](images/toast-content-headers.png)

To learn more about using headers, please see [Toast headers](toast-headers.md).


## Adaptive content

**New in Anniversary Update**: In addition to the content specified above, you can also display additional adaptive content that is visible when the toast is expanded.

This additional content is specified using Adaptive, which you can learn more about by reading the [Adaptive Tiles documentation](/windows/uwp/launch-resume/create-adaptive-tiles).

Note that any adaptive content must be contained within an [**AdaptiveGroup**](./toast-schema.md#adaptivegroup). Otherwise it will not be rendered using adaptive.


### Columns and text elements

Here's an example where columns and some advanced adaptive text elements are used. Since the text elements are within an **AdaptiveGroup**, they support all the rich adaptive styling properties.

![A screenshot of a toast notification showing groups of text elements aligned to the left and right of the visual area of the toast.](images/toast-content-columns.png)

### [Windows App SDK](#tab/appsdk)

```csharp
// The Microsoft.Windows.AppNotifications.Builder syntax does not currently support adaptive text elements.
```

### [Builder syntax](#tab/toolkit)

```csharp
new ToastContentBuilder()
    ...
    
    .AddVisualChild(new AdaptiveGroup()
    {
        Children =
        {
            new AdaptiveSubgroup()
            {
                Children =
                {
                    new AdaptiveText()
                    {
                        Text = "52 attendees",
                        HintStyle = AdaptiveTextStyle.Base
                    },
                    new AdaptiveText()
                    {
                        Text = "23 minute drive",
                        HintStyle = AdaptiveTextStyle.CaptionSubtle
                    }
                }
            },
            new AdaptiveSubgroup()
            {
                Children =
                {
                    new AdaptiveText()
                    {
                        Text = "1 Microsoft Way",
                        HintStyle = AdaptiveTextStyle.CaptionSubtle,
                        HintAlign = AdaptiveTextAlign.Right
                    },
                    new AdaptiveText()
                    {
                        Text = "Bellevue, WA 98008",
                        HintStyle = AdaptiveTextStyle.CaptionSubtle,
                        HintAlign = AdaptiveTextAlign.Right
                    }
                }
            }
        }
    });
```

### [XML](#tab/xml)

```xml
<toast launch="app-defined-string">
  <visual>
    <binding template="ToastGeneric">
      <text>Reminder: Windows Launch Party</text>
      <text>4:00 PM, 10/21/2021</text>
      <group>
        <subgroup>
          <text hint-style="base">52 attendees</text>
          <text hint-style="captionSubtle">23 minute drive</text>
        </subgroup>
        <subgroup>
          <text hint-style="captionSubtle" hint-align="right">1 Microsoft Way</text>
          <text hint-style="captionSubtle" hint-align="right">Bellevue, WA 98008</text>
        </subgroup>
      </group>
    </binding>
  </visual>
</toast>
```

---


## Buttons

Buttons make your toast interactive, letting the user take quick actions on your app notification without interrupting their current workflow. For example, users can reply to a message directly from within a toast, or delete an email without even opening the email app. Buttons appear in the expanded portion of your notification.

To learn more about implementing buttons end-to-end, see [Send local toast](send-local-toast.md).

Buttons can activate an app in the following ways:

- The app is activated in the foreground, with an argument that can be used to navigate to a specific page/context.
- Another app is activated via protocol launch. 
- Background activation is supported explicitly for UWP apps. For Windows App SDK apps, the app is always launched in the foreground. The app can call [AppInstance.GetActivatedEventArgs](/windows/windows-app-sdk/api/winrt/microsoft.windows.applifecycle.appinstance.getactivatedeventargs) to detect if the activation was launched by a notification and determine from the passed arguments whether to fully launch the foreground app or just handle the notification and exit. 
- System actions, such as snoozing or dismissing the notification, are supported both for UWP apps and for Windows App SDK. The **AppNotificationBuilder** APIs don't support this scenario but Windows App SDK apps can implement this scenario using the **Microsoft.Windows.AppNotifications.Builder** APIs or raw XML.

> [!NOTE]
> You can only have up to 5 buttons (including context menu items which we discuss later).

![A screenshot of a toast notification showing a line of text followed a row with two buttons defined by action elements".](images/toast-content-buttons.png)

### [Windows App SDK](#tab/appsdk)

```csharp
new ToastContentBuilder()
    var builder = new AppNotificationBuilder()
        .AddText("New product in stock!")
        .AddButton(new AppNotificationButton("See more details")
            .AddArgument("action", "viewDetails"))
            .AddArgument("contentId", "351")
        .AddButton(new AppNotificationButton("Remind me later")
            .AddArgument("action", "remindLater"))
            .AddArgument("contentId", "351");
```

### [Community Toolkit](#tab/toolkit)

```csharp
var builder = new ToastContentBuilder()
    .AddText("New product in stock!")
    .AddButton(new ToastButton()
        .SetContent("See more details")
        .AddArgument("action", "viewDetails"))

    .AddButton(new ToastButton()
        .SetContent("Remind me later")
        .AddArgument("action", "remindLater"));
```

### [XML](#tab/xml)

```xml
<toast>
    <visual>
        <binding template='ToastGeneric'>
            <text>New product in stock!</text>
        </binding>
    </visual>
      <actions>
          <action content='See more details' arguments='action=viewDetails;contentId=351'/>
          <action content='Remind me later' arguments='action=remindLater;contentId=351'/>
      </actions>
</toast>
```

---


### Buttons with icons

You can add icons to your buttons. These icons are white transparent 16x16 pixel images at 100% scaling, and should have no padding included in the image itself. If you choose to provide icons on a toast notification, you must provide icons for ALL of your buttons in the notification, as it transforms the style of your buttons into icon buttons.

> [!NOTE]
> For accessibility, be sure to include a contrast-white version of the icon (a black icon for white backgrounds), so that when the user turns on High Contrast White mode, your icon is visible. For more information, see [Tile and toast notification support for language, scale, and high contrast](/windows/uwp/launch-resume/tile-toast-language-scale-contrast).

![Screenshot of an app notification that uses buttons with icons.](images/toast-content-button-icons.png)

### [Windows App SDK](#tab/appsdk)

```csharp
new ToastContentBuilder()
    var builder = new AppNotificationBuilder()
        .AddText("Return books to the library.")
        .AddButton(new AppNotificationButton("Accept")
            .AddArgument("action", "accept")
            .SetIcon(new Uri("ms-appx:///Images/Accept.png")))
        .AddButton(new AppNotificationButton("Snooze")
            .AddArgument("action", "snooze")
            .SetIcon(new Uri("ms-appx:///Images/Snooze.png")))
        .AddButton(new AppNotificationButton("Dismiss")
            .AddArgument("action", "dismiss")
            .SetIcon(new Uri("ms-appx:///Images/Dismiss.png")));
```

### [Community Toolkit](#tab/toolkit)

```csharp
var builder = new ToastContentBuilder()
    .AddText("New product in stock!")
    .AddButton(new ToastButton()
        .SetContent("Accept")
        .AddArgument("action", "accept")
        .SetImageUri(new Uri("ms-appx:///Images/Accept.png")))
    .AddButton(new ToastButton()
        .SetContent("Snooze")
        .AddArgument("action", "snooze")
        .SetImageUri(new Uri("ms-appx:///Images/Snooze.png")))
    .AddButton(new ToastButton()
        .SetContent("Dismiss")
        .AddArgument("action", "dismiss")
        .SetImageUri(new Uri("ms-appx:///Images/Dismiss.png")));
```

#### [XML](#tab/xml)

```xml
<toast>
    <visual>
        <binding template='ToastGeneric'>
            <text>Return books to the library.</text>
        </binding>
    </visual>
    <actions>
        <action content='Accept' arguments='action=accept' imageUri='ms-appx:///Assets/Accept.png'/>
        <action content='Snooze' arguments='action=snooze' imageUri='ms-appx:///Assets/Snooze.png'/>
        <action content='Dismiss' arguments='action=dismiss' imageUri='ms-appx:///Assets/Dismiss.png'/>
    </actions>
</toast>
```

---

**New in Windows 11 Update**: You can add tooltips to your icons with the **HintToolTip** property in XML. This is ideal if your buttons have icons but no content, as this will make sure you can pass text that Windows Narrator can read. However, if content is present, then Narrator will read the content, no matter what is passed in the tooltip.

### [Windows App SDK](#tab/appsdk)

```csharp
var button = new AppNotificationButton("Reply")
    .AddArgument("action", "reply");

if (AppNotificationButton.IsToolTipSupported())
{
    button.ToolTip = "Click to reply.";
}

var builder = new AppNotificationBuilder()
    .AddText("Notification text.")
    .AddButton(button); 
```

### [Community Toolkit](#tab/toolkit)

```csharp
// The Community Toolkit builder syntax does not currently support icon tooltips. 
```

### [XML](#tab/xml)

```xml
<toast>
    <visual>
        <binding template='ToastGeneric'>
            <text>Notification text.</text>
        </binding>
    </visual>
    <actions>
        <action content='Reply' arguments='action=reply' hint-toolTip='Click to reply'/>
    </actions>
</toast>
```

---

### Buttons with colors

**New in Windows 11 Update**: You can add red or green colors to your buttons by adding the **useButtonStyle** attribute to the **toast** XML element and the **hint-buttonStyle** attribute to the action XML element as seen below.


![A screenshot of a notification with three buttons, the two left buttons are green with icons for starting a video call or starting an audio call. The third button is red and has an icon for rejecting the call.](images/toast-content-button-colors.png)

### [Windows App SDK](#tab/appsdk)

```csharp
var builder = new AppNotificationBuilder()
    .SetScenario(AppNotificationScenario.IncomingCall)
    .AddText("Andrew Bares", new AppNotificationTextProperties()
        .SetIncomingCallAlignment())
      .AddText("Incoming Call - Mobile", new AppNotificationTextProperties()
        .SetIncomingCallAlignment())
      .SetInlineImage(new Uri("ms-appx:///Images/Profile.png"),
        AppNotificationImageCrop.Circle)
    .AddButton(new AppNotificationButton()
        .SetToolTip("Answer Video Call")
        .SetButtonStyle(AppNotificationButtonStyle.Success)
        .SetIcon(new Uri("ms-appx:///Images/Video.png"))
        .AddArgument("videoId", "123"))
    .AddButton(new AppNotificationButton()
        .SetToolTip("Answer Phone Call")
        .SetButtonStyle(AppNotificationButtonStyle.Success)
        .SetIcon(new Uri("ms-appx:///Images/Call.png"))
        .AddArgument("callId", "123"))
    .AddButton(new AppNotificationButton()
        .SetToolTip("Hang Up")
        .SetButtonStyle(AppNotificationButtonStyle.Critical)
        .SetIcon(new Uri("ms-appx:///Images/HangUp.png"))
        .AddArgument("hangUpId", "123"));
```

### [Community Toolkit](#tab/toolkit)

```csharp
// The WIndows Community Toolkit builder syntax does not currently support red and green button colors.
```

### [XML](#tab/xml)

```xml
<toast scenario='incomingCall' useButtonStyle='true'>
    <visual>
        <binding template='ToastGeneric'>
            <text hint-callScenarioCenterAlign='true'>Andrew Bares</text>
            <text hint-callScenarioCenterAlign='true'>Incoming Call - Mobile</text>
            <image src='ms-appx:///Images/InlineImage.png' hint-crop='circle'/>
        </binding>
    </visual>
    <actions>
        <action 
            content='' 
            arguments='videoId=123' 
            imageUri='ms-appx:///Images/Video.png' 
            hint-buttonStyle='Success' 
            hint-toolTip='Answer Video Call'/>
        <action 
            content='' 
            arguments='callId=123' 
            imageUri='ms-appx:///Images/Call.png' 
            hint-buttonStyle='Success' 
            hint-toolTip='Answer Phone Call'/>
        <action 
            content='' 
            arguments='hangUpId=123' 
            imageUri='ms-appx:///Images/HangUp.png' 
            hint-buttonStyle='Critical' 
            hint-toolTip='Hang Up'/>
    </actions>
</toast>
```

---



## Context menu actions

**New in Anniversary Update**: You can add additional context menu actions to the existing context menu that appears when the user right clicks your toast notification or selects the context menu icon.

> [!NOTE]
> On older devices, these additional context menu actions will simply appear as normal buttons on your notification.

The additional context menu actions you add (such as "Mute group chat for 1 hour") appear above the two default system entries.

![Toast with context menu](images/toast-content-context-menu.png)

### [Windows App SDK](#tab/appsdk)

```csharp
var builder = new AppNotificationBuilder()
    .AddText("Camping this weekend?")
    .SetAppLogoOverride(new Uri("ms-appx:///images/Reply.png"), AppNotificationImageCrop.Circle)
    .AddButton(new AppNotificationButton("Mute group chat for 1 hour")
        .AddArgument("action", "mute")
        .SetContextMenuPlacement());
```

### [Community Toolkit](#tab/toolkit)

The Windows Community Toolkit builder syntax doesn't currently support context menu actions, so we recommend using initializer syntax.

```csharp
ToastContent content = new ToastContent()
{
    ...
 
    Actions = new ToastActionsCustom()
    {
        ContextMenuItems =
        {
            new ToastContextMenuItem("Mute group chat for 1 hour", "action=muteId")
        }
    }
};
```

### [XML](#tab/xml)

```xml
<toast>
    <visual>
        <binding template='ToastGeneric'>
            <text>Camping this weekend?</text>
            <image placement='appLogoOverride' src='ms-appx:///images/Reply.png' hint-crop='circle'/>
        </binding>
    </visual>
    <actions>
        <action content='Mute group chat for 1 hour' arguments='action=mute' placement='contextMenu'/>
    </actions>
</toast>
```

---

> [!NOTE]
> Additional context menu items contribute to the total limit of 5 buttons on a toast.

Activation of additional context menu items is handled identical to toast buttons.


## Inputs

Inputs are specified within the **Actions** region of the app notification, meaning they are only visible when the notification is expanded.


### Quick reply text box

To enable a quick reply text box (for example, in a messaging app) add a text input and a button, and reference the ID of the text input field so that the button is displayed next to the input field. The optional icon for the button, if provided, should be a 32x32 pixel image with no padding, white pixels set to transparent, and 100% scale.

![A screenshot of a toast notification with a profile picture and some lines of text. A text box for typing directly into the toast is included as well as a button to send the reply.](images/toast-content-text-box.png)

### [Windows App SDK](#tab/appsdk)

```csharp
var builder = new AppNotificationBuilder()
    .AddTextBox("textBox", "Type a reply", "Reply")
    .AddButton(AppNotificationButton("Send")
        .AddArguments("action", "Send")
        .SetInputId("textBox"))
    .BuildNotification();
```

### [Community Toolkit](#tab/toolkit)

```csharp
var builder = new ToastContentBuilder()
    .AddInputTextBox("tbReply", "Type a reply")
        .AddButton(new ToastButton()
        .SetContent("Reply")
        .SetTextBoxId("tbReply") // To place button next to text box, reference text box's id
        .SetImageUri(new Uri("Assets/Reply.png", UriKind.Relative))
        .AddArgument("action", "reply"));
```

### [XML](#tab/xml)

```xml
<toast launch="app-defined-string">
  <visual>
    <binding template="ToastGeneric">
      <image placement="appLogoOverride" hint-crop="circle" src="https://picsum.photos/48?image=883"/>
      <text>Andrew Bares</text>
      <text>Shall we meet up at 8?</text>
    </binding>
  </visual>
  <actions>
    <input id="textBox" type="text" placeHolderContent="Type a reply"/>
    <action
      content="Send"
      arguments="action=reply&amp;convId=9318"
      hint-inputId="textBox"
      imageUri="Assets/Reply.png"/>
  </actions>
</toast>
```

---



### Inputs with buttons bar

You also can have one (or many) inputs with normal buttons displayed below the inputs.

![A screenshot of an app notification showing a line of text, a text box, and a row with two buttons labeled "Reply" and "Video call".](images/toast-content-input-with-buttons.png)

### [Windows App SDK](#tab/appsdk)

```csharp
// The Microsoft.Windows.AppNotifications.Builder syntax does not currently support quick reply text boxes.
```

### [Community Toolkit](#tab/toolkit)

```csharp
new ToastContentBuilder()
    ...
    
    .AddInputTextBox("tbReply", "Type a reply")

    .AddButton(new ToastButton()
        .SetContent("Reply")
        .AddArgument("action", "reply"))

    .AddButton(new ToastButton()
        .SetContent("Video call")
        .AddArgument("action", "videoCall"));
```

### [XML](#tab/xml)

```xml
<toast launch="app-defined-string">
  <visual>
    <binding template="ToastGeneric">
      <image placement="appLogoOverride" hint-crop="circle" src="https://picsum.photos/48?image=883"/>
      <text>Andrew Bares</text>
      <text>Shall we meet up at 8?</text>
    </binding>
  </visual>
  <actions>
    <input id="textBox" type="text" placeHolderContent="Type a reply"/>
    <action
      content="Reply"
      arguments="action=reply&amp;threadId=9218"/>
    <action
      content="Video call"
      arguments="action=videocall&amp;threadId=9218"
      activationType="foreground"/>
  </actions>
</toast>
```

---


### Selection input

In addition to text boxes, you can also use a selection menu.

![A screenshot of an app notification showing a line of text, a selection input with "Lunch" as the selected item, and a row with two buttons labeled "Reserve" and "Call restaurant".](images/toast-content-selection-input.png)


### [Windows App SDK](#tab/appsdk)

```csharp
var builder = new AppNotificationBuilder()
    .AddText("4th coffee?")
    .AddText("When do you plan to come in tomorrow?")
    .AddComboBox(new AppNotificationComboBox("time")
        .SetTitle("Select an item:")
        .AddItem("breakfast", "Breakfast")
        .AddItem("lunch", "Lunch")
        .AddItem("dinner", "Dinner")
        .SetSelectedItem("lunch"))
    .AddButton(new AppNotificationButton("Reply")
        .AddArgument("action", "reply")
        .AddArgument("threadId", "9218")
        .SetContextMenuPlacement())
    .AddButton(new AppNotificationButton("Call restaurant")
        .AddArgument("action", "videocall")
        .AddArgument("threadId", "9218")
        .SetContextMenuPlacement());
```

### [Community Toolkit](#tab/toolkit)

```csharp
var builder = new ToastContentBuilder()
    .AddText("4th coffee?")
    .AddText("When do you plan to come in tomorrow?")
    .AddToastInput(new ToastSelectionBox("time")
    {
        DefaultSelectionBoxItemId = "lunch",
        Items =
            {
                new ToastSelectionBoxItem("breakfast", "Breakfast"),
                new ToastSelectionBoxItem("lunch", "Lunch"),
                new ToastSelectionBoxItem("dinner", "Dinner")
            }
    })
    .AddButton(new ToastButton()
        .SetContent("Reply")
        .AddArgument("action", "reply")
        .AddArgument("threadId", "9218"))
    .AddButton(new ToastButton()
        .SetContent("Call restaurant")
        .AddArgument("action", "videoCall")
        .AddArgument("threadId", "9218"));
```

#### [XML](#tab/xml)

```xml
<toast>
    <visual>
        <binding template='ToastGeneric'>
            <text>4th coffee?</text>
            <text>When do you plan to come in tomorrow?</text>
        </binding>
    </visual>
    <actions>
        <input id='time' type='selection' title='Select an item:' defaultInput='lunch'>
            <selection id='breakfast' content='Breakfast'/><selection id='dinner' content='Dinner'/>
            <selection id='lunch' content='Lunch'/>
        </input>
        <action content='Reply' arguments='action=reply;threadId=9218' placement='contextMenu'/>
        <action content='Call Restaurant' arguments='action=videocall;threadId=9218' placement='contextMenu'/>
    </actions>
</toast>
```

---



### Snooze/dismiss

Using a selection menu and two buttons, we can create a reminder notification that utilizes the system snooze and dismiss actions. Make sure to set the scenario to "Reminder" for the notification to behave like a reminder.

![A screenshot of an app notification with lines of text describing the time and location of a meeting. A selection box has "15 minutes" selected and there are buttons labeled Snooze and Dismiss.](images/toast-content-snooze-dismiss.png)

We link the Snooze button to the selection menu input using the **SelectionBoxId** property on the toast button.

The **Microsoft.Windows.AppNotifications.Builder** syntax does not currently support system activation. But this scenario is supported for Windows App SDK apps, and you can build notifications for this scenario using the `Microsoft.Toolkit.Uwp.Notifications` APIs or raw XML.

### [Windows App SDK](#tab/appsdk)

```csharp
// The Microsoft.Windows.AppNotifications.Builder syntax does not currently support system activation. 
// But this scenario is supported for Windows App SDK apps, and you can build notifications for this 
// scenario using the `Microsoft.Toolkit.Uwp.Notifications` APIs or raw XML.
```

### [Community Toolkit](#tab/toolkit)

```csharp
new ToastContentBuilder()
    .SetToastScenario(ToastScenario.Reminder)
    
    ...
    
    .AddToastInput(new ToastSelectionBox("snoozeTime")
    {
        DefaultSelectionBoxItemId = "15",
        Items =
        {
            new ToastSelectionBoxItem("5", "5 minutes"),
            new ToastSelectionBoxItem("15", "15 minutes"),
            new ToastSelectionBoxItem("60", "1 hour"),
            new ToastSelectionBoxItem("240", "4 hours"),
            new ToastSelectionBoxItem("1440", "1 day")
        }
    })

    .AddButton(new ToastButtonSnooze() { SelectionBoxId = "snoozeTime" })
    .AddButton(new ToastButtonDismiss());
```

#### [XML](#tab/xml)

```xml
<toast>
  <visual>
    <binding template="ToastGeneric">
      <text hint-maxLines="1">Adaptive Tiles Meeting</text>
      <text>Conf Room 2001 / Building 135</text>
      <text>10:00 AM - 10:30 AM</text>
    </binding>
  </visual>
  <actions>
    <input id="snoozeTime" type="selection" defaultInput="15">
      <selection id="1" content="1 minute"/>
      <selection id="15" content="15 minutes"/>
      <selection id="60" content="1 hour"/>
      <selection id="240" content="4 hours"/>
      <selection id="1440" content="1 day"/>
    </input>
    <action activationType="system" arguments="snooze" hint-inputId="snoozeTime" content="" />
    <action activationType="system" arguments="dismiss" content=""/>
  </actions>
</toast>
```

---

To use the system snooze and dismiss actions:

-   Specify a **ToastButtonSnooze** or **ToastButtonDismiss**
-   Optionally specify a custom content string:
  -   If you don't provide a string, we'll automatically use localized strings for "Snooze" and "Dismiss".
-   Optionally specify the **SelectionBoxId**:
  -   If you don't want the user to select a snooze interval and instead just want your notification to snooze only once for a system-defined time interval (that is consistent across the OS), then don't construct any &lt;input&gt; at all.
  -   If you want to provide snooze interval selections:
    -   Specify **SelectionBoxId** in the snooze action
    -   Match the id of the input with the **SelectionBoxId** of the snooze action
    -   Specify **ToastSelectionBoxItem**'s value to be a nonNegativeInteger which represents snooze interval in minutes.



## Audio

Custom audio has always been supported on Mobile, and is supported in Desktop Version 1511 (build 10586) or later. Custom audio can be referenced via the following paths:

- ms-appx:///
- ms-appdata:///

### [Windows App SDK](#tab/appsdk)

```csharp
var builder = new AppNotificationBuilder()
    .AddText("Notification text.")
    .SetAudioUri(new Uri("ms-appx:///Audio/NotificationSound.mp3"));
```

### [Community Toolkit](#tab/toolkit)

```csharp
var builder = new ToastContentBuilder()
    .AddText("Notification text")
    .AddAudio(new Uri("ms-appx:///Audio/NotificationSound.mp3"));
```

#### [XML](#tab/xml)

```xml
<toast>
    <visual>
        <binding template='ToastGeneric'>
            <text>Notification text.</text>
        </binding>
    </visual>
    <audio src='ms-appx:///Audio/NotificationSound.mp3'/>
</toast>
```

---

Alternatively, you can pick from the [list of ms-winsoundevents](/uwp/schemas/tiles/toastschema/element-audio#attributes-and-elements), which have always been supported on both platforms.

### [Windows App SDK](#tab/appsdk)

```csharp
var builder = new AppNotificationBuilder()
    .AddText("Notification text.")
    .SetAudioEvent(AppNotificationSoundEvent.Alarm, AppNotificationAudioLooping.Loop);
```

### [Community Toolkit](#tab/toolkit)

```csharp
var builder = new ToastContentBuilder()
    .AddText("Notification text")
    .AddAudio(new Uri("ms-winsoundevent:Notification.Looping.Alarm"));
```

### [XML](#tab/xml)

```xml
<toast>
    <visual>
        <binding template='ToastGeneric'>
            <text>Notification text.</text>
        </binding>
    </visual>
    <audio src='ms-winsoundevent:Notification.Looping.Alarm' loop='true'/>
</toast>
```

---

See the [audio schema page](/uwp/schemas/tiles/toastschema/element-audio) for information on audio in app notifications. To learn how to send an app notification that uses custom audio, see [custom audio on toasts](custom-audio-on-toasts.md).


## Scenarios

To create important notifications, alarms, reminders, and incoming call notifications, you simply use a normal app notification with a **Scenario** value assigned to it. The scenario adjusts a few behaviors to create a consistent and unified user experience. There are four possible **Scenario** values: 

- **Reminder**
- **Alarm**
- **IncomingCall** 
- **Urgent**

### Reminders

In the reminder scenario, the notification will stay on screen until the user dismisses it or takes action. On Windows Mobile, the app notification will also show pre-expanded. A reminder sound will be played. You must provide at least one button on your app notification. Otherwise, the notification will be treated as a normal notification.

#### [Windows App SDK](#tab/appsdk)

```csharp
var builder = new AppNotificationBuilder()
    .AddText("Notification text.")
    .SetScenario(AppNotificationScenario.Reminder);
```

#### [Community Toolkit](#tab/toolkit)

```csharp
var builder = new ToastContentBuilder()
    .AddText("Notification text.")
    .SetToastScenario(ToastScenario.Reminder)
```

#### [XML](#tab/xml)

```xml
<toast scenario='reminder'>
    <visual>
        <binding template='ToastGeneric'>
            <text>Notification text.</text>
        </binding>
    </visual>
</toast>
```

---


### Alarms
Alarms behave the same as reminders, except alarms will additionally loop audio with a default alarm sound. You must provide at least one button on your app notification. Otherwise, the notification will be treated as a normal notification.

#### [Windows App SDK](#tab/appsdk)

```csharp
var builder = new AppNotificationBuilder()
    .AddText("Notification text.")
    .SetScenario(AppNotificationScenario.Alarm)
    .AddButton(new AppNotificationButton("Dismiss")
        .AddArgument("action", "dismiss"));
```

#### [Community Toolkit](#tab/toolkit)

```csharp
var builder = new ToastContentBuilder()
    .AddText("Notification text")
    .SetToastScenario(ToastScenario.Alarm)
    .AddButton(new ToastButton()
        .SetContent("Dismiss")
        .AddArgument("action", "dismiss"));
```

#### [XML](#tab/xml)

```xml
<toast scenario='alarm'>
    <visual>
        <binding template='ToastGeneric'>
            <text>Notification text.</text>
        </binding>
    </visual>
    <actions>
        <action content='Dismiss' arguments='action=dismiss'/>
    </actions>
</toast>
```

---



### Incoming calls
Incoming call notifications are displayed pre-expanded in a special call format and stay on the user's screen till dismissed. Ringtone audio will loop by default. On Windows Mobile devices, they display full screen.

![Incoming call toast notification](images/toast-content-incoming-call.png)

#### [Windows App SDK](#tab/appsdk)

```csharp
var builder = new AppNotificationBuilder()
    .SetScenario(AppNotificationScenario.IncomingCall)
    .AddText("Andrew Bares", new AppNotificationTextProperties()
        .SetIncomingCallAlignment())
    .AddText("incoming call - mobile", new AppNotificationTextProperties()
        .SetIncomingCallAlignment())
      .SetInlineImage(new Uri("ms-appx:///images/profile.png"),
        AppNotificationImageCrop.Circle)
    .AddButton(new AppNotificationButton("Text reply")
        .SetToolTip("Text reply")
        .SetIcon(new Uri("ms-appx:///images/reply.png"))
        .AddArgument("textId", "123"))
    .AddButton(new AppNotificationButton("Reminder")
        .SetToolTip("Reminder")
        .SetIcon(new Uri("ms-appx:///images/reminder.png"))
        .AddArgument("reminderId", "123"))
    .AddButton(new AppNotificationButton("Ignore")
        .SetToolTip("Ignore")
        .SetIcon(new Uri("ms-appx:///images/ignore.png"))
        .AddArgument("ignoreId", "123"))
    .AddButton(new AppNotificationButton("Answer")
        .SetToolTip("Answer")
        .SetIcon(new Uri("ms-appx:///images/answer.png"))
        .AddArgument("answerId", "123"));
```

#### [Community Toolkit](#tab/toolkit)

```csharp
var builder = new ToastContentBuilder()
    .SetToastScenario(ToastScenario.IncomingCall)
    .AddText("Andrew Bares")
    .AddText("incoming call - mobile")
    .AddInlineImage(new Uri("ms-appx:///images/profile.png"), 
        "Andrew Bares",
        null,
        AdaptiveImageCrop.Circle)
    .AddButton(new ToastButton()
        .SetContent("Text reply")
        .SetImageUri(new Uri("ms-appx:///images/reply.png"))
        .AddArgument("textId", "123"))
    .AddButton(new ToastButton()
        .SetContent("Reminder")
        .SetImageUri(new Uri("ms-appx:///images/reminder.png"))
        .AddArgument("reminderId", "123"))
    .AddButton(new ToastButton()
        .SetContent("Ignore")
        .SetImageUri(new Uri("ms-appx:///images/ignore.png"))
        .AddArgument("ignoreId", "123"))
    .AddButton(new ToastButton()
        .SetContent("Answer")
        .SetImageUri(new Uri("ms-appx:///images/answer.png"))
        .AddArgument("answerId", "123"));
```

#### [XML](#tab/xml)

```xml
<toast scenario='incomingCall'>
    <visual>
        <binding template='ToastGeneric'>
            <text hint-callScenarioCenterAlign='true'>Andrew Bares</text>
            <text hint-callScenarioCenterAlign='true'>incoming call - mobile</text>
            <image src='ms-appx:///images/profile.png' hint-crop='circle'/>
        </binding>
    </visual>
    <actions>
        <action 
            content='Text reply' 
            arguments='textId=123' 
            imageUri='ms-appx:///images/reply.png' 
            hint-toolTip='Text reply'/>
        <action 
                content='Reminder' 
                arguments='reminderId=123' 
                imageUri='ms-appx:///images/reminder.png' 
                hint-toolTip='Reminder'/>
        <action 
                content='Ignore' 
                arguments='ignoreId=123' 
                imageUri='ms-appx:///images/ignore.png' 
                hint-toolTip='Ignore'/>
        <action 
            content='Answer' 
            arguments='answerId=123' 
            imageUri='ms-appx:///images/answer.png' 
            hint-toolTip='Answer'/>
    </actions>
</toast>
```


---

### Important Notifications

> [!IMPORTANT]
> **Requires**: You must be running Windows Insider Preview Build 22546 or later to use important notifications.

Important notifications allow users to have more control over what 1st party and 3rd party apps can send them high-priority app notifications (urgent/important) that can break through Focus Assist (Do not Disturb). This can be modified in the notifications settings.


![A screenshot of an urgent app notification that has an exclamation point in the attribution area next to the app name. The image also shows the system-initiated app notification that provides buttons for the user to allow or disallow urgent notifications from the app.](images/toast-content-urgent.png)

#### [Windows App SDK](#tab/appsdk)

```csharp
var builder = new AppNotificationBuilder()
    .AddText("Adaptive Tiles Meeting", 
        new AppNotificationTextProperties()
            .SetMaxLines(1))
    .AddText("Conf Room 2001 / Building 135")
    .AddText("10:00 AM - 10:30 AM");

if (AppNotificationBuilder.IsUrgentScenarioSupported())
{
    builder.SetScenario(AppNotificationScenario.Urgent);
}
```

#### [Community Toolkit](#tab/toolkit)

```csharp
// The builder syntax does not support important notifications yet.
```

#### [XML](#tab/xml)

```xml
<toast scenario='urgent'>
    <visual>
        <binding template='ToastGeneric'>
            <text hint-maxLines='1'>Adaptive Tiles Meeting</text>
            <text>Conf Room 2001 / Building 135</text>
            <text>10:00 AM - 10:30 AM</text>
        </binding>
    </visual>
</toast>
```

---

## Localization and accessibility

Your tiles and app notifications can load strings and images tailored for display language, display scale factor, high contrast, and other runtime contexts. For more info, see [Tile and toast notification support for language, scale, and high contrast](/windows/uwp/launch-resume/tile-toast-language-scale-contrast).


## Handling activation
To learn how to handle app activations (the user clicking your toast or buttons on the toast), see [Send local toast](send-local-toast.md).
 
## Related topics

* [Send a local toast and handle activation](send-local-toast.md)
* [Notifications library on GitHub (part of the UWP Community Toolkit)](https://github.com/windows-toolkit/WindowsCommunityToolkit/tree/master/Microsoft.Toolkit.Uwp.Notifications)
* [Tile and toast notification support for language, scale, and high contrast](/windows/uwp/launch-resume/tile-toast-language-scale-contrast)
