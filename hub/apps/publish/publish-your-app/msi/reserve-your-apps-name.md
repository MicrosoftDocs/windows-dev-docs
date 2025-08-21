---
description: Reserve your MSI/EXE app's name in the Microsoft Store
title: Reserve your MSI/EXE app's name
ms.topic: how-to
ms.date: 10/30/2022
---

# Reserve your MSI/EXE app's name

All apps on the Microsoft Store must have a unique name. The first step toward publishing your app on the store is to reserve the name you'd like to use. You can reserve your app's name up to three months before you are ready to publish, even if you have not started to write your app yet. We recommend reserving your name as soon as possible to ensure it will be available when you're ready to publish. Reserved names not used within three months will have the reservation removed.

If you are not sure what you want your app's name to be, you can reserve multiple names. You'll be able to choose the final name when you're ready to publish.

Follow the following steps to reserve your app's name:

1. Navigate to the [Partner Center apps and games page](https://aka.ms/submitwindowsapp).
2. Click **New product**.
3. Click on **EXE or MSI app**.

:::image type="content" source="images/msiexe-new-product.png" lightbox="images/msiexe-new-product.png" alt-text="A screenshot showing how to create an EXE/MSI app.":::

4. Enter the name you'd like to use and click **Check availability**. If the name is available, you'll see a green check mark. If the name is already in use, you'll see a message indicating so.

:::image type="content" source="images/msiexe-app-name-reservation-page.png" lightbox="images/msiexe-app-name-reservation-page.png" alt-text="A screenshot showing how to reserve a name for EXE/MSI app.":::

5. Once you've selected an available name that you'd like to reserve, click **Reserve product name**.

> [!NOTE]
> You might find that you cannot reserve a name, even though you do not see any apps listed by that name in the Microsoft Store. This is usually because another developer has reserved the name for their app but has not submitted it yet. If you are unable to reserve a name for which you hold the trademark or other legal right, or if you see another app in the Microsoft Store using that name, [contact Microsoft](https://www.microsoft.com/info/cpyrtInfrg.html).

> [!TIP]
> For guidance on selecting an effective app name, see [How do I choose a great app name for the Microsoft Store](../../faq/submit-your-app.md) in the FAQ section.

> Windows uses variable width fonts, so the number of visible characters in your title depends on which characters you use. For example, using Segoe UI, about 30 `i` characters will fit in the same space as 10 `w` characters. If you have multiple apps, be sure to test the visibility of each app's title, even if they are the same number of characters. Also be sure to test all localizations of your app's name. Keep in mind that East-Asian characters tend to be wider than Latin characters, so fewer characters will be displayed.

**Be original**. Make sure your app name is distinctive enough that it won't be easily confused with an existing app.

**Do not use names trademarked by others**. Make sure that you have the right to use the name that you reserve. If someone else has trademarked the name, they can report an infringement and you will not be able to keep using that name. If that happens after your app has been published, it will be removed from the Store until you've changed all instances of the name in your app, its content, and its store listing before you can submit your app for certification again.

**Avoid trailing differentiators**. Information that distinguishes different versions of your app should not be put at the end of your title. This information can be truncated by the UI, and users can miss it even if it is displayed.

If this is unavoidable, use different logos and app images to make it easier to differentiate one app from another.

**Do not include emojis in your name**. You will not be able to reserve a name that includes emojis or other unsupported characters.

## Next steps for MSI/EXE app development

After reserving your app name, you may want to explore technical implementation topics for your MSI or EXE application:

- **UI controls and accessibility**: Learn about implementing docking controls, drag and drop, and other UI patterns. See [Control patterns and interfaces](../../../design/accessibility/control-patterns-and-interfaces.md) for UI Automation patterns including docking functionality.
- **App updates**: Plan how your application will handle updates after publication. See [Publish update to your MSI/EXE app on the Store](./publish-update-to-your-app-on-store.md) for Store update guidance.
- **Desktop app development**: Find comprehensive guidance for building Windows desktop applications at [Build desktop apps for Windows](../../../desktop/index.yml).
- **Continue with app submission**: Once your name is reserved, proceed to [Create your MSI/EXE app submission](./create-app-submission.md) to begin the publishing process.
