---
description: Reserve your app's name in the Microsoft Store
title: Reserve your app's name
ms.topic: article
ms.date: 10/30/2022
zone_pivot_groups: store-installer-packaging
---

# Reserve your app's name

> [!IMPORTANT]
> There is currently a bug preventing new products being created in some circumstances. Please see [Troubleshooting App Creation in Partner Center](../partner-center/troubleshoot-app-creation.md) for more information, including a workaround.

All apps on the Microsoft Store must have a unique name. The first step toward putting your app on the store is to reserve the name you'd like to use. You can reserve your app's name up to three months before you are ready to publish, even if you have not started to write your app yet. We recommend reserving your name as soon as possible to ensure it will be available when you're ready to publish. Reserved names not used within three months will have the reservation removed.

If you are not sure what you want you app's name to be, you can reserve multiple names. You'll be able to choose the final name when you're ready to publish.

Follow the following steps to reserve your app's name:

1. Sign in to [Partner Center](https://partner.microsoft.com/).
:::zone pivot="store-installer-msix,store-installer-pwa,store-installer-add-on"
2. Navigate to the [Partner Center apps and games page](https://partner.microsoft.com/dashboard/apps-and-games/overview).
3. Click **New product**.
4. Click on MSIX or PWA app. If you want to submit MSIX game, click on Game.

:::image type="content" source="../../../includes/store/msix/images/msix-new-product.png" lightbox="../../../includes/store/msix/images/msix-new-product.png" alt-text="A screenshot showing how to create a MSIX/PWA app.":::

5. Enter the name you'd like to use and click **Check availability**. If the name is available, you'll see a green check mark. If the name is already in use, you'll see a message indicating so.

:::image type="content" source="../../../includes/store/msix/images/msix-app-name-reservation.png" lightbox="../../../includes/store/msix/images/msix-app-name-reservation.png" alt-text="A screenshot showing how to reserve a name for MSIX/PWA app.":::

6. Once you've selected an available name that you'd like to reserve, click **Reserve product name**.
:::zone-end

:::zone pivot="store-installer-msi-exe"
2. MSI or EXE app publishing experience is only available in the new Workspace interface and you can click the “Workspaces” button on the top right of the page to toggle to the new interface. For more information, see [Partner Center Workspaces](../partner-center/partner-center-workspaces.md)
3. Navigate to the [Partner Center apps and games page](https://partner.microsoft.com/dashboard/apps-and-games/overview).
4. Click **New product**.
5. Click on EXE or MSI app.

:::image type="content" source="../../../includes/store/msi/images/msiexe-new-product.png" lightbox="../../../includes/store/msi/images/msiexe-new-product.png" alt-text="A screenshot showing how to create an EXE/MSI app.":::

6. Enter the name you'd like to use and click **Check availability**. If the name is available, you'll see a green check mark. If the name is already in use, you'll see a message indicating so.

:::image type="content" source="../../../includes/store/msi/images/msiexe-app-name-reservation-page.png" lightbox="../../../includes/store/msi/images/msiexe-app-name-reservation-page.png" alt-text="A screenshot showing how to reserve a name for EXE/MSI app.":::

7. Once you've selected an available name that you'd like to reserve, click **Reserve product name**.
:::zone-end

> [!NOTE]
> You might find that you cannot reserve a name, even though you do not see any apps listed by that name in the Microsoft Store. This is usually because another developer has reserved the name for their app but has not submitted it yet. If you are unable to reserve a name for which you hold the trademark or other legal right, or if you see another app in the Microsoft Store using that name, [contact Microsoft](https://www.microsoft.com/info/cpyrtInfrg.html).

## Tips for choosing a great app name

Choosing the right name for your app is important. Pick a name that will capture your customers' interest and draw them in to learn more about your app. Here are some tips for choosing a great app name.

**Keep it short**. While your app's name can have up to 256 characters, the space used to display your app's name is limited. Long names may be truncated based on where in the store your app is being displayed and the user's display size and settings.

> [!TIP]
> Windows uses variable width fonts, so the number of visible characters in your title depends on which characters you use. For example, using Segoe UI, about 30 `i` characters will fit in the same space as 10 `w` characters. If you have multiple apps, be sure to test the visibility of each app's title, even if they are the same number of characters. Also be sure to test all localizations of your app's name. Keep in mind that East-Asian characters tend to be wider than Latin characters, so fewer characters will be displayed.

**Be original**. Make sure your app name is distinctive enough that it won't be easily confused with an existing app.

**Do not use names trademarked by others**. Make sure that you have the right to use the name that you reserve. If someone else has trademarked the name, they can report an infringement and you will not be able to keep using that name. If that happens after your app has been published, it will be removed from the Store until you've changed all instances of the name in your app, its content, and its store listing before you can submit your app for certification again.

**Avoid trailing differentiators**. Information that distinguishes different versions of your app should not be put at the end of your title. This information can be truncated by the UI, and users can miss it even if it is displayed.

If this is unavoidable, use different logos and app images to make it easier to differentiate one app from another.

**Do not include emojis in your name**. You will not be able to reserve a name that includes emojis or other unsupported characters.
