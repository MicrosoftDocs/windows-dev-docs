---
title: Configure your Xbox development console
author: KevinAsgari
description: Learn how to configure your Xbox development console to support Xbox Live development.
ms.assetid: f8fd1caa-b1e9-4882-a01f-8f17820dfb55
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
ms.localizationpriority: low
---

# Configure your Xbox development console

To configuring your development console:
- Get your IDs
- Set your sandbox on your development kits
- Sign in with a development account

## Get your IDs
To enable sandboxes and Xbox Live services, you will need to obtain several IDs to configure your development kit and your title. These can be done with the same process.

Follow [Xbox Live service configuration](../xbox-live-service-configuration.md) to get your IDs

## Set your sandbox on your development kits
You will not be able to boot your development kit without setting your Sandbox ID. To do this, you can use the "Xbox One Manager" that's installed on your PC by the XDK, or you can open an XDK command window and use the Configuration (xbconfig.exe) command as follows:

Check your current sandbox. Type xbconfig sandboxid at the command prompt.

If it’s not what you expect, change your sandbox id. Type xbconfig sandboxid=<your sandbox id> at the command prompt.

Reboot your console using Reboot (xbreboot.exe) at the command prompt.

Verify your sandbox has been correctly reset. Type xbconfig sandboxid at the command prompt.

## Sign in with a development account

You can create development accounts used to sign-in on [Xbox Developer Portal (XDP)](https://xdp.xboxlive.com/User/Contact/MyAccess?selectedMenu=devaccounts) or [Windows Dev Center](https://developer.microsoft.com/en-us/windows)
