---
Description: The first step in creating a new app in Partner Center is reserving an app name. See how to reserve app names and find suggestions for choosing a great name for your app.
title: Create your app by reserving a name
keywords: windows 10, uwp, name reservation, app name, app names, names, product name, naming, reserved name, title, names, titles
ms.assetid: 6DC58A9A-DF47-4652-8D13-0AC9289F5950
ms.date: 10/31/2018
ms.topic: article


ms.localizationpriority: medium
---
# Create your app by reserving a name

The first step in creating a new app in [Partner Center](https://partner.microsoft.com/dashboard) is reserving an app name. Each reserved name (sometimes referred to as your app's *title*) must be unique throughout the Microsoft Store.

You can reserve a name for your app even if you haven't started to build your app yet. We recommend doing so as soon as possible, so that nobody else can use the name. Note that you will need to submit the app within three months in order to keep that name reserved for your use.

When you [upload your app's packages](upload-app-packages.md), the [**Package/Properties/DisplayName**](/uwp/schemas/appxpackage/uapmanifestschema/element-displayname) value must match the name that you reserved for your app. If you use Microsoft Visual Studio to create your app's package, this attribute will be filled in for you.

> [!IMPORTANT]
> You can reserve additional names for an app, and you may choose to use one of those in the published version of your app instead of the one you reserve when you first create your app in Partner Center. However, be aware that the first name you enter here will be used in some of your app's [identity details](view-app-identity-details.md), such as the **Package Family Name (PFN)**. These values may be visible to some users, and cannot be changed, so make sure that the name you reserve is appropriate for this use.


## Create your app by reserving a new name

Reserving a name is the first step in creating an app in Partner Center. 

1.  From the **Overview** page, click **Create a new app**.
2.  In the text box, enter the name that you want to use, and then select **Check availability**. If the name is available, you'll see a green check mark. (If the name you entered is already reserved or in use by another developer, you'll see a message that the name is not available.)
3.  Click **Reserve product name**.

The name is now reserved for you, and you can start working on your [submission](app-submissions.md) whenever you're ready. 

> [!NOTE]
> You might find that you can't reserve a name, even though you don't see any apps listed by that name in the Microsoft Store. This is usually because another developer has reserved the name for their app but hasn't submitted it yet. If you are unable to reserve a name for which you hold the trademark or other legal right, or if you see another app in the Microsoft Store using that name, [contact Microsoft](https://www.microsoft.com/info/cpyrtInfrg.html).

After you reserve a name, you have three months to submit that app. If you don't submit it within three months, the name reservation will expire, and another developer may be able to use that name for an app. You may encounter an error if you try to submit an app under a name which you have let expire.


## Choosing your app's name

Choosing the right name for your app is an important task. Pick a name that will capture your customers' interest and draw them in to learn more about your app. Here are some tips for choosing a great app name.

-   **Keep it short.** The space to display your app's name is limited in many places, so we suggest using the shortest name as you can. While your app's name can have up to 256 characters, the end of a very long name may not always be visible to customers.
    > [!NOTE]
    > The actual number of characters displayed in various locations may vary, depending on the length allotted and on the types of characters used in your app's name. For example, in the Segoe UI font that Windows uses, about 30 "I" characters will fit in the same space as 10 "W" characters. Because of this variation, be sure to test your app and verify how its name appears on its tiles (if you choose to overlay the app name), in search results, and within the app itself. Also consider each language in which you offer your app. Keep in mind that East-Asian characters tend to be wider than Latin characters, so fewer characters will be displayed.
-   **Be original.** Make sure your app name is distinctive enough so that it isn't easily confused with an existing app.
-   **Don't use names trademarked by others.** Make sure that you have the rights to use the name that you reserve. If someone else has trademarked the name, they can report an infringement and you won't be able to keep using that name. If that happens after your app has been published, it will be removed from the Store. You'll then need to change the name of your app, and all instances of the name throughout your app and its content, before you can [submit your app](app-submissions.md) for certification again.
-   **Avoid adding differentiating info at the end of the name.** If the info that differentiates multiple apps is added to the end of a name, customers might miss it, especially if the name is long; all of the apps could appear to have the same name. If this is unavoidable, use different logos and app images to make it easier to differentiate one app from another.
-   **Don't include emojis in your name.** You won't be able to reserve a name that includes emojis or other unsupported characters.


## Manage additional app names

You can add and manage additional names on the **Manage app names** page in the **App management** section for each of your apps in Partner Center.

In some cases, you may want to reserve multiple names to use for the same app, such as when you want to offer your app in multiple languages and want to use different names for each language. You will need to reserve an additional name if you want to change an app's name completely.

On this page, you can also delete any names that you have reserved but no longer want to use.

For more info, see [Manage app names](manage-app-names.md).

 

 