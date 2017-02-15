---
author: jnHs
Description: View the names that you've reserved for your app, reserve additional names (for other languages or to change your app's name), and delete reserved names that you don't need anymore.
title: Manage app names
ms.assetid: D95A6227-746E-4729-AE55-648A7102401C
ms.author: wdg-dev-content
ms.date: 02/15/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---

# Manage app names


You can view all of the names that you've reserved for your app, reserve additional names (for other languages or to change your app's name), and delete names you don't need. To do this, go to the **Manage app names** page in the **App management** section for any of your apps in the Windows Dev Center dashboard.

## Reserve additional names for your app

You can reserve multiple app names to use for the same app. This is especially useful if you are offering your app in multiple languages and want to use different names for different languages. You can also use this to change the name of an app which you haven't published yet.

In the **Reserve more names** section of the **Manage app names** page, you'll see a text box. Enter the name you'd like to reserve, then click **Check availability**. If the name is available, click **Reserve name**.

> **Note**  For more info about reserving app names, and why a certain name may not be available, see [Create your app by reserving a name](create-your-app-by-reserving-a-name.md).

You can continue reserving additional app names here if desired.

## Delete app names

If you no longer want to use a name you've previously reserved, you can release it by deleting it here. Make sure you're certain before you do so, since this means that the name will immediately become available for someone else to reserve and use.

To delete one of your app's reserved names, find the name you no longer want to use and then click **Delete**. In the confirmation dialog, click **Delete** again to confirm.

Note that your app needs to have at least one reserved name. To completely remove an app from your dashboard (and release all the names you've reserved for that app), you can click **Delete this app** from its **Overview** page. If you have a submission for the app in progress, you'll need to delete that submission first (and if you've already published the app to the Store, you can't delete it from your dashboard).

## Rename an app that has already been published

If your app is already in the Windows Store and you want to rename it, you can do so by reserving a new name for it (by following the steps described above) and then creating a new submission for the app. Note that you'll have to update your package to include the new name in order for the Store to display the app under the new name. Be sure to use the new name in the [**Package/Properties/DisplayName**](https://msdn.microsoft.com/en-us/library/windows/apps/dn934748.aspx) element in the app manifest, and update any graphics or text that includes the app's name. You'll also want to review your app's description and change the name if you mention it anywhere there.

Once your app has been published with the new name, you can delete the old name that you no longer need to use.

 

 




