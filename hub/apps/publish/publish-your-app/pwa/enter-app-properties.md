---
description: The App properties page of the app submission process lets you define your PWA app's category and indicate hardware preferences or other declarations.
title: Enter app properties for PWA
ms.date: 10/30/2022
ms.topic: article
ms.localizationpriority: medium
---

# Enter app properties for PWA

The **Properties** page of the [app submission process](./create-app-submission.md) is where you define your app's category and enter other info and declarations. Be sure to provide complete and accurate details about your app on this page.

:::image type="content" source="../msix/images/msix-properties-overview.png" lightbox="../msix/images/msix-properties-overview.png" alt-text="A screenshot showing the overview of Pricing and availability section for MSIX/PWA app.":::

## Category, subcategory and secondary category

You must indicate the category (and subcategory/genre, if applicable) which the Store should use to categorize your app. Specifying a category is required in order to submit your app. You can optionally choose a secondary category for your app. Secondary category has the same list of categories as the Primary category.

For more info, see [Category and subcategory table](./categories-and-subcategories.md).

## Support info

This section lets you provide info such as Privacy policy URL, website and support contact info to help customers understand more about your app and how to get support. You are responsible for ensuring your app complies with applicable privacy laws and regulations, and for providing a valid privacy policy URL here if required.

For more info, see [Support info](./support-info.md) section.

## Game settings

This section will only appear if you selected **Games** as your product’s category. Here you can specify which features your game supports. The information that you provide in this section will be displayed on the product’s Store listing.

If your game supports any of the multiplayer options, be sure to indicate the minimum and maximum number of players for a session. You can't enter more than 1,000 minimum or maximum players.

**Cross-platform multiplayer** means that the game supports multiplayer sessions between players on Windows 10 or Windows 11 PCs and Xbox.

## Display mode

This section lets you indicate whether your product is designed to run in an immersive (not a 2D) view for [Windows Mixed Reality](https://developer.microsoft.com/mixed-reality) on PC and/or HoloLens devices. If you indicate that it is, you'll also need to:

- Select either **Minimum hardware** or **Recommended hardware** for **Windows Mixed Reality immersive headset** in the [System requirements](./system-requirements.md) section that appears lower on the **Properties** page.
- Specify the **Boundary setup** (if PC is selected) so that users know whether it's meant to be used in a seated or standing position only, or whether it allows (or requires) the user to move around while using it.

If you have selected **Games** as your product's category, you'll see additional options in the **Display mode** selection that let you indicate whether your product supports 4K resolution video output, High Dynamic Range (HDR) video output, or variable refresh rate displays.

If your product does not support any of these display mode options, leave all of the boxes unchecked.

## Product declarations

You can check boxes in this section to indicate if any of the declarations apply to your app. This may affect the way your app is displayed, whether it is offered to certain customers, or how customers can use it.

For more info, see [Product declarations](./product-declarations.md) section.

## System requirements

In this section, you have the option to indicate if certain hardware features are required or recommended to run and interact with your app properly.

For more info, see [System requirements](./system-requirements.md) section.