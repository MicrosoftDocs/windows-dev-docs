---
title: Testing on Xbox One Console
description: Learn how to test Xbox Live services on the Xbox Live Console
ms.date: 08/15/2018
ms.topic: article
keywords: windows 10, uwp, games, xbox, xbox live, xbox one
ms.localizationpriority: low
---
# Testing on the Xbox One console

When developing your title for the Xbox One family of consoles it is only natural that you would want to be able to test your title and Xbox Live features on an actual console. There are a few options for testing your title on hardware. You can use any retail Xbox One Console to test a Universal Windows Platform (UWP) title or app by activating the console's developer mode. This option is accessible to all developers and is the only option for Xbox Live Creators Program Developers. ID@Xbox and managed partners have the option of ordering and using an Xbox Development Kit.

## Retail console testing: Xbox Live Creators

Activating developer mode on an Xbox One retail console will allow you to deploy UWP titles and apps to the Xbox One Console by pairing it with a Visual Studio build. This is the console testing option for Xbox Live Creators Program Developers. You will not be able to test XDK games on a retail Xbox One console.

* Follow the [developer mode activation instructions](../xbox-apps/devkit-activation.md) to allow development testing on your retail console.  
* Load your title onto the Xbox One by following the [Setting up your Xbox One instructions](../xbox-apps/development-environment-setup.md#setting-up-your-xbox-one).  
* Follow the [developer mode deactivation instructions](../xbox-apps/devkit-deactivation.md) to put your console back into retail mode or uninstall the development environment on your retail console.  
* While your console is in developer mode you can access it remotely through your PC by using the [Windows Devices portal for Xbox](../debug-test-perf/device-portal-xbox.md).  

## Xbox Development kit testing: ID@Xbox and Managed Partners

Managed partners and ID@Xbox developers have the option to purchase an Xbox developer kit from the [Xbox Dev Store](https://gamedevstore.partners.extranet.microsoft.com/), which is only accessible to those with managed developer accounts. Xbox developer kits will allow you to load XDK games to the console for testing, UWP games can also be tested by dev kit. Developer kits come with hardware options and testing features that allow for more in-depth performance testing and console management.

To begin your journey with the Xbox developer kit read the [Getting Started with Xbox One Development article](https://developer.microsoft.com/en-us/games/xbox/docs/xdk/atoc-getting-started) on the managed partner documentation site. This documentation is only accessible to authorized developers in the ID@Xbox program and managed partners.