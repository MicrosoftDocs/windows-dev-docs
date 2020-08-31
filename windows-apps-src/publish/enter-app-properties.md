---
Description: The App properties page of the app submission process lets you define your app's category and indicate hardware preferences or other declarations.
title: Enter app properties
ms.assetid: CDE4AF96-95A0-4635-9D07-A27B810CAE26
ms.date: 10/31/2018
ms.topic: article
keywords: windows 10, uwp, game settings, display mode, system requirements, hardware requirements, minimum hardware, recommended hardware, privacy policy, support contact info, app website, support info
ms.localizationpriority: medium
---
# Enter app properties

The **Properties** page of the [app submission process](app-submissions.md) is where you define your app's category and enter other info and declarations. Be sure to provide complete and accurate details about your app on this page.


## Category and subcategory

You must indicate the category (and subcategory/genre, if applicable) which the Store should use to categorize your app. Specifying a category is required in order to submit your app.

For more info, see [Category and subcategory table](category-and-subcategory-table.md).


## Support info

This section lets you provide info to help customers understand more about your app and how to get support.

### Privacy policy URL

You are responsible for ensuring your app complies with privacy laws and regulations, and for providing a valid privacy policy URL here if required.

In this section, you must indicate whether or not your app accesses, collects, or transmits any [personal information](/legal/windows/agreements/store-policies#105-personal-information). If you answer **Yes**, a privacy policy URL is required. Otherwise, it is optional (though if we determine that your app requires a privacy policy, and you have not provided one, your submission may fail certification).

> [!NOTE]
> If we detect that your packages declare [capabilities](../packaging/app-capability-declarations.md) that could allow personal information to be accessed, transmitted, or collected, we will mark this question as **Yes**, and you will be required to enter a privacy policy URL.

To help you determine if your app requires a privacy policy, review the [App Developer Agreement](/legal/windows/agreements/app-developer-agreement) and the [Microsoft Store Policies](/legal/windows/agreements/store-policies#105-personal-information). 

> [!NOTE]
> Microsoft doesn't provide a default privacy policy for your app. Likewise, your app is not covered by any Microsoft privacy policy. 


### Website

Enter the URL of the web page for your app. This URL must point to a page on your own website, not your app's web listing in the Store. This field is optional, but recommended.

### Support contact info

Enter the URL of the web page where your customers can go for support with your app, or an email address that customers can contact for support. We recommend including this info for all submissions, so that your customers know how to get support if they need it. Note that Microsoft does not provide your customers with support for your app.

> [!IMPORTANT]
> The **Support contact info** field is required if your app or game is available on Xbox. Otherwise, it is optional (but recommended).


## Game settings

This section will only appear if you selected **Games** as your product’s category. Here you can specify which features your game supports. The information that you provide in this section will be displayed on the product’s Store listing.

If your game supports any of the multiplayer options, be sure to indicate the minimum and maximum number of players for a session. You can't enter more than 1,000 minimum or maximum players.

**Cross-platform multiplayer** means that the game supports multiplayer sessions between players on Windows 10 PCs and Xbox.


## Display mode

This section lets you indicate whether your product is designed to run in an immersive (not a 2D) view for [Windows Mixed Reality](https://developer.microsoft.com/mixed-reality) on PC and/or HoloLens devices. If you indicate that it is, you'll also need to:
- Select either **Minimum hardware** or **Recommended hardware** for **Windows Mixed Reality immersive headset** in the [System requirements](#system-requirements) section that appears lower on the **Properties** page.
- Specify the **Boundary setup** (if PC is selected) so that users know whether it's meant to be used in a seated or standing position only, or whether it allows (or requires) the user to move around while using it. 

If you have selected **Games** as your product's category, you'll see additional options in the **Display mode** selection that let you indicate whether your product supports 4K resolution video output, High Dynamic Range (HDR) video output, or variable refresh rate displays.

If your product does not support any of these display mode options, leave all of the boxes unchecked.


## Product declarations

You can check boxes in this section to indicate if any of the declarations apply to your app. This may affect the way your app is displayed, whether it is offered to certain customers, or how customers can use it.

For more info, see [Product declarations](./product-declarations.md).

## System requirements

In this section, you have the option to indicate if certain hardware features are required or recommended to run and interact with your app properly. You can check the box (or indicate the appropriate option) for each hardware item where you would like to specify **Minimum hardware** and/or **Recommended hardware**.

If you make selections for **Recommended hardware**, those items will be displayed in your product's Store listing as recommended hardware for customers on Windows 10, version 1607 or later. Customers on earlier OS versions will not see this info.

If you make selections for **Minimum hardware**, those items will be displayed in your product's Store listing as required hardware for customers on Windows 10, version 1607 or later. Customers on earlier OS versions will not see this info. The Store may also display a warning to customers who are viewing your app's listing on a device that doesn’t have the required hardware. This won't prevent people from downloading your app on devices that don't have the appropriate hardware, but they won't be able to rate or review your app on those devices. 

The behavior for customers will vary depending on the specific requirements and the customer's version of Windows:

- **For customers on Windows 10, version 1607 or later:**
     - All minimum and recommended requirements will be displayed in the Store listing.
     - The Store will check for all minimum requirements and will display a warning to customers on a device that doesn't meet the requirements.
- **For customers on earlier versions of Windows 10:**
     - For most customers, all minimum and recommended hardware requirements will be displayed in the Store listing (though customers viewing an older versions of the Store client will only see the minimum hardware requirements).
     - The Store will attempt to verify items that you designate as **Minimum hardware**, with the exception of **Memory**, **DirectX**, **Video memory**, **Graphics**, and **Processor**; none of those will be verified, and customers won't see any warning on devices which don't meet those requirements. 
- **For customers on Windows 8.x and earlier or Windows Phone 8.x and earlier:**
     - If you check the **Minimum hardware** box for **Touch screen**, this requirement will be displayed in your app's Store listing, and customers on devices without a touch screen will see a warning if they try to download the app. No other requirements will be verified or displayed in your Store listing.

We also recommend adding runtime checks for the specified hardware into your app, since the Store may not always be able to detect that a customer's device is missing the selected feature(s) and they could still be able to download your app even if a warning is displayed. If you want to completely prevent your UWP app from being downloaded on a device which doesn't meet minimum requirements for memory or DirectX level, you can designate the minimum requirements in a [StoreManifest XML file](/uwp/schemas/storemanifest/storemanifestschema2015/schema-root).

> [!TIP]
> If your product requires additional items that aren't listed in this section in order to run properly, such as 3D printers or USB devices, you can also enter [additional system requirements](create-app-store-listings.md#additional-system-requirements) when you create your Store listing.