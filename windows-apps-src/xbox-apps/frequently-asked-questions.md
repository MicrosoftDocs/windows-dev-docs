---
title: Frequently asked questions
description: If things aren't working as you expected, look through this page of frequently asked questions about UWP on Xbox.
ms.date: 03/29/2017
ms.topic: article
keywords: windows 10, uwp
ms.assetid: 265fe827-bd4a-48d4-b362-8793b9b25705
ms.localizationpriority: medium
---
# Frequently asked questions

Things not working the way you expected? 
Look through this page of frequently asked questions. 
Also check out the [Known issues](known-issues.md) topic and the [Developing Universal Windows apps](https://social.msdn.microsoft.com/Forums/windowsapps/en-US/home?forum=wpdevelop) forum. 

### Why aren't my games and apps working?

If your games and apps aren't working, or if you don't have access to the store or to Live services, you are probably running in Developer Mode. 
To figure out which mode you're currently in, press the **Home** button on your controller. If this takes you to Dev Home instead of 
the retail Home experience, you're in Developer Mode. If you want to play games, you can open Dev Home and switch back to Retail Mode by using the **Leave developer mode** button.

### Why can't I connect to my Xbox One using Visual Studio?

Start by verifying that you are running in Developer Mode, and not in Retail Mode. 
You cannot connect to your Xbox One when it is in Retail Mode. 
To figure out which mode you're currently in, press the **Home** button on your controller. If you see Gold/Live content instead of Dev Home,
you're in Retail Mode and you need to run the Dev Mode Activation app to switch to Developer Mode.

> [!NOTE]
> You must have a user signed in to deploy an app.

For more information, see [Fixing deployment failures](#fixing-deployment-failures) later on this page.

### How do I switch between Retail Mode and Developer Mode?

Follow the [Xbox One Developer Mode Activation](devkit-activation.md) instructions to understand more about these states.

### How do I know if I am in Retail Mode or Developer Mode?

Follow the [Xbox One Developer Mode Activation](devkit-activation.md) instructions to understand more about these states. 

To figure out which mode you're currently in, press the **Home** button on your controller. 
- If this takes you to Dev Home, you're in Developer Mode.
- If you see Gold/Live content, you're in Retail Mode.

### Will my games and apps still work if I activate Developer Mode?

Yes, you can switch from Developer Mode to Retail Mode, where you can play your games. 
For more information, see the [Xbox One Developer Mode Activation](devkit-activation.md) page. 

### Can I develop and publish x86 apps for Xbox?
Xbox no longer supports x86 app development or x86 app submissions to the store. 

### Will I lose my games and apps or saved changes?

If you decide to leave the Developer Program, you won't lose your installed games and apps. 
In addition, as long as you were online when you played them, your saved games are all saved on your Live account cloud profile, so you won't lose them.

### How do I leave the Developer Program?

See the [Xbox One Developer Mode Deactivation](devkit-deactivation.md) topic for details about how to leave the Developer Program.

### I sold my Xbox One and left it in Developer Mode. How do I deactivate Developer Mode?

If you no longer have access to your Xbox One, you can deactivate it in Windows Partner Center. 
For details, see the **Deactivate your console using Partner Center** section in the [Xbox One Developer Mode Deactivation](devkit-deactivation.md#deactivate-your-console-using-partner-center) topic. 

### I left the Developer Program using Partner Center but I'm in still Developer Mode. What do I do?

Start Dev Home and select the **Leave developer mode** button. 
This will restart your console in Retail Mode. 

### Can I publish my app?

You can [publish apps](../publish/index.md) through Partner Center if you have a [developer account](https://developer.microsoft.com/store/register). UWP apps created and tested on a retail Xbox One console will go through the same ingestion, review, and publication process that Windows conducts today, with additional reviews to meet today's Xbox One standards.

### Can I publish my game?

You can use UWP and your Xbox One in Developer Mode to build and test your games on Xbox One. To publish UWP games, you must register with [ID@XBOX](https://www.xbox.com/Developers/id) or be part of the [Xbox Live Creators Program](https://developer.microsoft.com/games/xbox/xboxlive/creator). For more info, see [Developer Program Overview](https://developer.microsoft.com/games/xbox/docs/xboxlive/get-started/developer-program-overview.html).

### Will the standard Game engines work?

Check out the [Known issues](known-issues.md) page for this release.

### What capabilities and system resources are available to UWP games on Xbox One? 

For information, see [System resources for UWP apps and games on Xbox One](system-resource-allocation.md).

### If I create a DirectX 12 UWP game, will it run on my Xbox One in Developer Mode?

For information, see [System resources for UWP apps and games on Xbox One](system-resource-allocation.md).

### Will the entire UWP API surface be available on Xbox?

Check out the [Known issues](known-issues.md) page for this release.

### Fixing deployment failures

If you can't deploy your app from Visual Studio, these steps may help you fix the problem. 
If you get stuck, ask for help on the forum.

> [!NOTE]
> You must have a user signed in to deploy an app. If you receive a 0x87e10008 error message, make sure you have a user signed in and try again.

If Visual Studio cannot connect to your Xbox One:

1. Make sure that you are in Developer Mode (discussed earlier on this page).
2. Make sure that you have set up your development PC correctly. Did you follow *all* of the directions in [Getting started with UWP app development on Xbox One](getting-started.md)? 

3. If you haven't yet, read through the [Development environment setup](development-environment-setup.md) topic and the [Introduction to Xbox One tools](introduction-to-xbox-tools.md) topic.

4. Make sure that you can “ping” your console IP address from your development PC.
  > [!NOTE]
  > In order to get the best deployment performance, we recommend that you use a wired connection to your console.

5. Make sure that you are using the Universal (Unencrypted Protocol) in the Authentication drop-down list on the **Debug** tab. For more details, see [Development environment setup](development-environment-setup.md).


### If I'm building an app using HTML/JavaScript, how do I enable Gamepad navigation?

TVHelpers is a set of JavaScript and XAML/C# samples and libraries to help you build great Xbox One and TV experiences in JavaScript and C#. 
TVJS is a library that helps you build premium UWP apps for Xbox One. TVJS includes support for automatic controller navigation, rich media playback, search, and more. 
You can use TVJS with your hosted web app just as easily as with a packaged web UWP app with full access to the Windows Runtime APIs.

For more information, see the [TVHelpers](https://github.com/Microsoft/TVHelpers) project and the project [wiki](https://github.com/Microsoft/TVHelpers/wiki).

## See also
- [Known issues with UWP on Xbox One](known-issues.md)
- [UWP on Xbox One](index.md)
- [UWP on Xbox One](index.md)
