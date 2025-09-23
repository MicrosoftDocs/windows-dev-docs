---
title: Splash screens
description: This section describes how to set and configure your app's splash screen.
ms.assetid: 6b954bb3-e5b0-46d1-8afc-fb805536cf6d
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Splash screens

All UWP apps must have a splash screen, which is a composite of an image and a background color, both of which can be customized.

Your splash screen is displayed immediately when the user launches your app. This provides immediate feedback to users while app resources are initialized. As soon as your app is ready for interaction, the splash screen is dismissed.

A well-designed splash screen can make your app more inviting. Here's a simple, understated splash screen:

![a 75% scaled screen capture of the splash screen from the splash screen sample.](images/regularsplashscreen.png)

This splash screen is created by combining a green background color with a transparent-background PNG image.

A simple image with a background color looks good regardless of the device your app is running on. Only the size of the background changes to compensate for a variety of screen sizes. Your image always remains intact.

Additionally, you can use the [**SplashScreen**](/uwp/api/Windows.ApplicationModel.Activation.SplashScreen) class to customize your app's launch experience. You can position an extended splash screen, which you create, to give your app more time to complete additional tasks like preparing app UI or completing networking operations. You can also use the **SplashScreen** class to notify you when the splash screen is dismissed, so that you can begin entrance animations.

| Topic | Description |
|-------|-------------|
| [Add a splash screen](add-a-splash-screen.md) | Set your app's splash screen image and background color. |
| [Display a splash screen for more time](create-a-customized-splash-screen.md) | Display a splash screen for more time by creating an extended splash screen for your app. This extended screen imitates the splash screen shown when your app is launched, and can be customized. |