---
Description: View the names that you've reserved for your app, reserve additional names (for other languages or to change your app's name), and delete reserved names that you don't need anymore.
title: Manage app names
ms.assetid: D95A6227-746E-4729-AE55-648A7102401C
ms.date: 10/02/2018
ms.topic: article
keywords: windows 10, uwp, app names, change app name, update app name, game name, product name
ms.localizationpriority: medium
---
# Manage app names

The **Manage app names** lets you view all of the names that you've reserved for your app, reserve additional names (for other languages or to change your app's name), and delete names you don't need. You can find this page in [Partner Center](https://partner.microsoft.com/dashboard) by expanding the **App management** section in the left navigation menu for any of your apps.

> [!IMPORTANT]
> You can reserve additional names for an app, and you may choose to use one of those in the published version of your app instead of the one you reserved when you first created your app in Partner Center. However, be aware that the first name that you reserve for your product will be used in some of it's [identity details](view-app-identity-details.md), such as the **Package Family Name (PFN)**. These values may be visible to some users, and cannot be changed, so make sure that the name you reserve first is appropriate for this use.


## Reserve additional names for your app

You can reserve multiple app names to use for the same app. This is especially useful if you are offering your app in multiple languages and want to use different names for different languages. You can also reserve a new name in order to change the name of an app, as described below.

To reserve a new app name, find the text box in the **Reserve more names** section of the **Manage app names** page. Enter the name you'd like to reserve, then click **Check availability**. If the name is available, click **Reserve product name**. You can reserve multiple app names by repeating these steps, if desired.

> [!NOTE]
> For more info about reserving app names, and why a certain name may not be available, see [Create your app by reserving a name](create-your-app-by-reserving-a-name.md).


## Delete app names

If you no longer want to use a name you've previously reserved, you can release it by deleting it here. Make sure you're certain before you do so, since this means that the name will immediately become available for someone else to reserve and use.

To delete one of your app's reserved names, find the name you no longer want to use and then click **Delete**. In the confirmation dialog, click **Delete** again to confirm.

Note that your app must have at least one reserved name. To completely remove an app from Partner Center (and release all the names you've reserved for that app), click **Delete this app** from the **App overview** page. If you have a submission for the app in progress, you'll need to delete that submission first. Note that if you've already published the app to the Store, you can't delete it from Partner Center (though you can use the **Show/hide products** functionality on your **Overview** page to hide it). 


## Rename an app that has already been published

If your app is already in the Store and you want to rename it, you can do so by reserving a new name for it (by following the steps described above) and then creating a new submission for the app. 

You must update your app's package(s) to replace the old name with the new one and upload the updated package(s) to your submission.
- First, update the Package.StoreAssociation.xml file to use the new name, either manually or by using Visual Studio (**Project > Store > Associate App with the Store...**). For more info, see [Package a UWP app with Visual Studio](/windows/msix/package/packaging-uwp-apps).
- You'll also need to update the [**Package/Properties/DisplayName**](/uwp/schemas/appxpackage/uapmanifestschema/element-displayname) element in your app manifest, and update any graphics or text that includes the app's name. 
  > [!IMPORTANT]
  > Be sure to update the Package.StoreAssociation.xml file before you change the **Package/Properties/DisplayName** in the app manifest, or you may get an error.

To update a Store listing so that it uses the new name, go to the [Store listing page](create-app-store-listings.md) for that language and select the name from the **Product name** dropdown. Be sure to review your description and other parts of the listing for any mentions of the name and make updates if needed.

> [!NOTE]
> If your app has packages and/or Store listings in multiple languages, you'll need to update the packages and/or Store listings for every language in which the name needs to be updated.

Once your app has been published with the new name, you can delete any older names that you no longer need to use.

> [!TIP]
> Each app appears in Partner Center using the first name which you reserved for it. If you've followed the steps above to rename an app, and you'd like it to appear in Partner Center using the new name, you must delete the original name (by clicking **Delete** on the **Manage app names** page). 

 

 