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

## Frequently asked questions

1. **How do I reserve an app name, and why is it important?**

    Every app in the Microsoft Store must have a unique name. Reserving an app name ensures that the name you want to use for your app is locked down for you, so no one else can claim it while you finish developing your app. You can reserve a name even before your app is ready – up to three months in advance of publishing.
    
    To reserve a name:
    - Go to your Apps & games section in Partner Center and select “New product.”
    - Choose the type of app (EXE or MSI).
    - Enter the app title and click **Check availability**.
    - If available, click **Reserve product name** to hold the name for 3 months.
    
    Reserving the name helps protect your chosen Store name while you finish developing your app.

2. **How do I choose a great app name for the Microsoft Store?**

    Choosing a compelling name is crucial to attracting customers and making your app easily discoverable. Here are some best practices:
    
    - **Keep it short**. Although your app's name can have up to 256 characters, display space is limited, and longer names may be truncated depending on the user's screen and settings. Shorter names are more memorable and easier to display clearly.
      
    > [!TIP]
    > Windows uses variable width fonts, so the number of visible characters in your title depends on which characters you use. For example, using Segoe UI, about 30 `i` characters will fit in the same space as 10 `w` characters. If you have multiple apps, be sure to test the visibility of each app's title, even if they are the same number of characters. Also be sure to test all localizations of your app's name. Keep in mind that East-Asian characters tend to be wider than Latin characters, so fewer characters will be displayed.
    
    - **Be original**. Choose a distinctive name that clearly differentiates your app from others. An original name reduces confusion and enhances your brand identity.
    
    - **Do not use trademarked names**. Ensure you have the rights to use the app name. Using a trademarked name could lead to your app being removed from the Store, forcing you to rename and re-submit your app.
    
    - **Avoid trailing differentiators**. Don't include differentiating information (such as version numbers or dates) at the end of your app's title. Such details may be cut off in some views, causing confusion. If necessary, differentiate your apps using distinct logos or images.
    
    - **Avoid emojis and special characters**. The Microsoft Store does not allow emojis or other unsupported special characters in app names.

## Next steps for MSI/EXE app development

After reserving your app name, you may want to explore technical implementation topics for your MSI or EXE application:

- **UI controls and accessibility**: Learn about implementing docking controls, drag and drop, and other UI patterns. See [Control patterns and interfaces](../../../design/accessibility/control-patterns-and-interfaces.md) for UI Automation patterns including docking functionality.
- **App updates**: Plan how your application will handle updates after publication. See [Publish update to your MSI/EXE app on the Store](./publish-update-to-your-app-on-store.md) for Store update guidance.
- **Desktop app development**: Find comprehensive guidance for building Windows desktop applications at [Build desktop apps for Windows](../../../desktop/index.yml).
- **Continue with app submission**: Once your name is reserved, proceed to [Create your MSI/EXE app submission](./create-app-submission.md) to begin the publishing process.
