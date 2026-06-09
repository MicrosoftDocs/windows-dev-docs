---
description: The App properties page of the app submission process lets you define your PWA app's category and indicate hardware preferences or other declarations.
title: Enter app properties for PWA
ms.date: 10/30/2022
ms.topic: article
ms.localizationpriority: medium
---

# Enter app properties for PWAs

The **Properties** page of the [app submission process](./create-app-submission.md) is where you define your app's category and enter other info and declarations. Be sure to provide complete and accurate details about your app on this page.

## Category, subcategory and secondary category

You must indicate the category (and subcategory/genre, if applicable) which the Store should use to categorize your app. Specifying a category is required in order to submit your app. You can optionally choose a secondary category for your app. Secondary category has the same list of categories as the Primary category.

:::image type="content" source="../msix/images/msix-categories-subcategories.png" lightbox="../msix/images/msix-categories-subcategories.png" alt-text="A screenshot showing the category and subcategory options for MSIX/PWA app.":::

For more info, see [Category and subcategory table](./categories-and-subcategories.md).

## Privacy policy

You are responsible for ensuring your app complies with privacy laws and regulations, and for providing a valid privacy policy URL here if required.

In this section, you must indicate whether or not your app accesses, collects, or transmits any [personal information](../../store-policies.md#105-personal-information). If you answer **Yes**, a privacy policy URL is required. Otherwise, it is optional (though if we determine that your app requires a privacy policy, and you have not provided one, your submission may fail certification).

> [!NOTE]
> If we detect that your packages declare [capabilities](/windows/uwp/packaging/app-capability-declarations) that could allow personal information to be accessed, transmitted, or collected, we will mark this question as **Yes**, and you will be required to enter a privacy policy URL.

To help you determine if your app requires a privacy policy, review the [App Developer Agreement](https://go.microsoft.com/fwlink/?linkid=528905) and the [Microsoft Store Policies](../../store-policies.md#105-personal-information).

> [!NOTE]
> Microsoft doesn't provide a default privacy policy for your app. Likewise, your app is not covered by any Microsoft privacy policy.

:::image type="content" source="../msix/images/msix-privacy-support-info.png" lightbox="../msix/images/msix-privacy-support-info.png" alt-text="A screenshot showing the privacy policy and support info fields for MSIX/PWA app.":::

## Support info

This section lets you provide info such as  website and support contact info to help customers understand more about your app and how to get support. You are responsible for ensuring your app complies with applicable privacy laws and regulations, and for providing a valid privacy policy URL here if required.

### Website

Enter the URL of the web page for your app. This URL must point to a page on your own website, not your app's web listing in the Store. This field is optional, but recommended.

### Support contact info

Enter the URL of the web page where your customers can go for support with your app, or an email address that customers can contact for support. We recommend including this info for all submissions, so that your customers know how to get support if they need it. Note that Microsoft does not provide your customers with support for your app.

> [!IMPORTANT]
> The **Support contact info** field is required if your app or game is available on Xbox. Otherwise, it is optional (but recommended).

### Phone number and address info

Enter Phone number, Address, Apartment / Suite, City, State / Province, Country and Postal code so customers can reach out to you in case of any concern or dispute.

> [!IMPORTANT]
> Businesses / Company accounts offering products in France market need to ensure to provide this info for compliance with France Consumer Protection Laws and Regulations 2023 - 2024. This is optional for individual developers.

## Display mode

This section lets you indicate whether your product is designed to run in an immersive (not a 2D) view for [Windows Mixed Reality](https://developer.microsoft.com/mixed-reality) on PC and/or HoloLens devices. If you indicate that it is, you'll also need to:

- Select either **Minimum hardware** or **Recommended hardware** for **Windows Mixed Reality immersive headset** in the [System requirements](./system-requirements.md) section that appears lower on the **Properties** page.
- Specify the **Boundary setup** (if PC is selected) so that users know whether it's meant to be used in a seated or standing position only, or whether it allows (or requires) the user to move around while using it.

:::image type="content" source="../msix/images/msix-display-mode.png" lightbox="../msix/images/msix-display-mode.png" alt-text="A screenshot showing the display modes options for MSIX/PWA app.":::

If your product does not support any of these display mode options, leave all of the boxes unchecked.

## Product declarations

You can check boxes in this section to indicate if any of the declarations apply to your app. This may affect the way your app is displayed, whether it is offered to certain customers, or how customers can use it.

:::image type="content" source="../msix/images/msix-product-declaration.png" lightbox="../msix/images/msix-product-declaration.png" alt-text="A screenshot showing the product declarations options for MSIX/PWA app.":::

For more info, see [Product declarations](./product-declarations.md) section.

## System requirements

In this section, you have the option to indicate if certain hardware features are required or recommended to run and interact with your app properly.

:::image type="content" source="../msix/images/msix-system-requirements.png" lightbox="../msix/images/msix-system-requirements.png" alt-text="A screenshot showing the system requirements for MSIX/PWA app.":::

For more info, see [System requirements](./system-requirements.md) section.