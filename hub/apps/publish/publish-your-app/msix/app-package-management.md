---
description: Learn how your MSIX app's packages are made available to your customers, and how to manage specific package scenarios.
title: Guidance for app package management for MSIX app
ms.date: 10/30/2022
ms.topic: article
ms.localizationpriority: medium
---

# Guidance for app package management for MSIX app

Learn how your app's packages are made available to your customers, and how to manage specific package scenarios.

## OS versions and package distribution

Different operating systems can run different types of packages. If more than one of your packages can run on a customer's device, the Microsoft Store will provide the best available match.

Generally speaking, later OS versions can run packages that target previous OS versions for the same device family. Windows 11 devices can run all previous supported OS versions (per device family). 

## Removing packages for a previously-supported device family

If you remove all packages for a certain device family (see [Programming with extension SDKs](/uwp/extension-sdks/device-families-overview)) that your app previously supported, you'll be prompted to confirm that this is your intention before you can save your changes on the **Packages** page.

When you publish a submission that removes all of the packages that could run on a device family that your app previously supported, new customers will not be able to acquire the app on that device family. You can always publish another update later to provide packages for that device family again.

Be aware that even if you remove all of the packages that support a certain device family, any existing customers who have already installed the app on that type of device can still use it, and they will get any updates you provide later.
