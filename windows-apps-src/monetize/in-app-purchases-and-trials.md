---
author: mcleanbyron
ms.assetid: F45E6F35-BC18-45C8-A8A5-193D528E2A4E
description: Learn how to enable in-app purchases and trials in UWP apps.
title: In-app purchases and trials
ms.author: mcleans
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, in-app purchases, IAPs, add-ons, trials, consumable, durable
---

# In-app purchases and trials

The Windows SDK provides APIs you can use to implement the following features to make more money from your Universal Windows Platform (UWP) app:

* **In-app purchases**&nbsp;&nbsp;Whether your app is free or not, you can sell content or new app functionality (such as unlocking the next level of a game) from right within the app.

* **Trial functionality**&nbsp;&nbsp;If you configure your app as a [free trial in the Windows Dev Center dashboard](../publish/set-app-pricing-and-availability.md#free-trial), you can entice your customers to purchase the full version of your app by excluding or limiting some features during the trial period. You can also enable features, such as banners or watermarks, that are shown only during the trial, before a customer buys your app.

This article provides an overview of how in-app purchases and trials work in UWP apps.

<span id="choose-namespace" />
## Choose which namespace to use

There are two different namespaces you can use to add in-app purchases and trial functionality to your UWP apps, depending on which version of Windows 10 your apps target. Although the APIs in these namespaces serve the same goals, they are designed quite differently, and code is not compatible between the two APIs.

* **[Windows.Services.Store](https://msdn.microsoft.com/library/windows/apps/windows.services.store.aspx)**&nbsp;&nbsp;Starting in Windows 10, version 1607, apps can use the API in this namespace to implement in-app purchases and trials. We recommend that you use the members in this namespace if your app targets Windows 10, version 1607, or a later release. This namespace supports the latest add-on types, such as Store-managed consumable add-ons, and is designed to be compatible with future types of products and features supported by Windows Dev Center and the Store. For more information about this namespace, see the [In-app purchases and trials using the Windows.Services.Store namespace](#api_intro) section in this article.

* **[Windows.ApplicationModel.Store](https://msdn.microsoft.com/library/windows/apps/windows.applicationmodel.store.aspx)**&nbsp;&nbsp;All versions of Windows 10 also support an older API for in-app purchases and trials in this namespace. Although any UWP app for Windows 10 can use this namespace, this namespace may not be updated to support new types of products and features in Dev Center and the Store in the future. For information about this namespace, see [In-app purchases and trials using the Windows.ApplicationModel.Store namespace](in-app-purchases-and-trials-using-the-windows-applicationmodel-store-namespace.md).

<span id="concepts" />
## Basic concepts

This section introduces basic concepts for in-app purchases and trials in UWP apps. Most of these concepts apply to both the **Windows.Services.Store** and **Windows.ApplicationModel.Store** namespaces, except where noted.

Every item that is offered in the Store is generally called a *product*. Most developers work with the following types of products: *apps* and *add-ons* (also known as in-app products or IAPs).

An add-on refers to a product or feature that you make available to your customers in the context of your app: for example, currency to be used in an app or game, new maps or weapons for a game, the ability to use your app without ads, or digital content such as music or videos for apps that have the ability to offer that type of content. Every app and add-on has an associated license that indicates whether the user is entitled to use the app or add-on. If the user is entitled to use the app or add-on as a trial, the license also provides additional info about the trial.

To offer an add-on to customers in your app, you must [define the add-on for your app in the Dev Center dashboard](../publish/iap-submissions.md) so the Store knows about it. Then, your app can use APIs in the **Windows.Services.Store** or **Windows.ApplicationModel.Store** namespace to offer the add-on for sale to the user as an in-app purchase.

UWP apps can offer the following types of add-ons.

| Add-on type |  Description  |
|---------|-------------------|
| Durable  |  An add-on that persists for the lifetime that you specify in the [Windows Dev Center dashboard](../publish/enter-iap-properties.md). <p/><p/>By default, durable add-ons never expire, in which case they can only be purchased once. If you specify a particular duration for the add-on, the user can repurchase the add-on after it expires. |
| Developer-managed consumable  |  An add-on that can be purchased, used, and purchased again. This type of add-on is often used for in-app currency. <p/><p/>For this type of consumable, you are responsible for keeping track of the user's balance of items that the add-on represents, and for reporting the purchase of the add-on as fulfilled to the Store after the user has consumed all the items. The user cannot purchase the add-on again until your app has reported the previous add-on purchase as fulfilled. <p/><p/>For example, if your add-on represents 100 coins in a game and the user consumes 10 coins, your app or service must maintain the new remaining balance of 90 coins for the user. After the user has consumed all 100 coins, your app must report the add-on as fulfilled, and then the user can purchase the 100 coin add-on again.    |
| Store-managed consumable  |  An add-on that can be purchased, used, and purchased again. This type of add-on is often used for in-app currency.<p/><p/>For this type of consumable, the Store keeps track of the user's balance of items that the add-on represents. When the user consumes any items, you are responsible for reporting those items as fulfilled to the Store, and the Store updates the user's balance. Your app can query for the current balance for the user at any time. After the user consumes all of the items, the user can purchase the add-on again.  <p/><p/> For example, if your add-on represents an initial quantity of 100 coins in a game and the user consumes 10 coins, your app reports to the Store that 10 units of the add-on were fulfilled, and the Store updates the remaining balance. After the user has consumed all 100 coins, the user can purchase the 100 coin add-on again. <p/><p/>**Note**&nbsp;&nbsp;Store-managed consumables are available starting in Windows 10, version 1607. To use Store-managed consumables, your app must target Windows 10, version 1607, or a later version, and it must use the API in the **Windows.Services.Store** namespace instead of the **Windows.ApplicationModel.Store** namespace.  |

<span />

> [!NOTE]
> Other types of add-ons, such as durable add-ons with packages (also known as downloadable content or DLC) are only available to a restricted set of developers, and are not covered in this documentation.

<span id="api_intro" />
## In-app purchases and trials using the Windows.Services.Store namespace

The remainder of this article describes how to implement in-app purchases and trials using the [Windows.Services.Store](https://msdn.microsoft.com/library/windows/apps/windows.services.store.aspx) namespace. This namespace is available only to apps that target Windows 10, version 1607, or later, and we recommend that apps use this namespace instead of the [Windows.ApplicationModel.Store](https://msdn.microsoft.com/library/windows/apps/windows.applicationmodel.store.aspx) namespace if possible.

If you're looking for information about the **Windows.ApplicationModel.Store** namespace, see [this article](in-app-purchases-and-trials-using-the-windows-applicationmodel-store-namespace.md).

### Get started with the StoreContext class

The main entry point to the **Windows.Services.Store** namespace is the [StoreContext](https://msdn.microsoft.com/library/windows/apps/windows.services.store.storecontext.aspx) class. This class provides methods you can use to get info for the current app and its available add-ons, get license info for the current app or its add-ons, purchase an app or add-on for the current user, and perform other tasks. To get a [StoreContext](https://msdn.microsoft.com/library/windows/apps/windows.services.store.storecontext.aspx) object, do one of the following:

* In a single-user app (that is, an app that runs only in the context of the user that launched the app), use the static [GetDefault](https://msdn.microsoft.com/library/windows/apps/windows.services.store.storecontext.getdefault.aspx) method to get a **StoreContext** object that you can use to access Windows Store-related data for the user. Most Universal Windows Platform (UWP) apps are single-user apps.

  ```csharp
  Windows.Services.Store.StoreContext context = StoreContext.GetDefault();
  ```

* In a [multi-user app](../xbox-apps/multi-user-applications.md), use the static [GetForUser](https://msdn.microsoft.com/library/windows/apps/windows.services.store.storecontext.getforuser.aspx) method to get a **StoreContext** object that you can use to access Windows Store-related data for a specific user who is signed in with their Microsoft account while using the app. The following example gets a **StoreContext** object for the first available user.

  ```csharp
  var users = await Windows.System.User.FindAllAsync();
  Windows.Services.Store.StoreContext context = StoreContext.GetForUser(users[0]);
  ```

> [!NOTE]
> Windows desktop applications that use the [Desktop Bridge](https://developer.microsoft.com/windows/bridges/desktop) must perform additional steps to configure the [StoreContext](https://msdn.microsoft.com/library/windows/apps/windows.services.store.storecontext.aspx) object before they can use this object. For more information, see [this section](#desktop).

After you have a [StoreContext](https://msdn.microsoft.com/library/windows/apps/windows.services.store.storecontext.aspx) object, you can start calling methods of this object to get Store product info for the current app and its add-ons, retrieve license info for the current app and its add-ons, purchase an app or add-on for the current user, and perform other tasks. For more information about common tasks you can perform using this object, see the following articles:

* [Get product info for apps and add-ons](get-product-info-for-apps-and-add-ons.md)
* [Get license info for apps and add-ons](get-license-info-for-apps-and-add-ons.md)
* [Enable in-app purchases of apps and add-ons](enable-in-app-purchases-of-apps-and-add-ons.md)
* [Enable consumable add-on purchases](enable-consumable-add-on-purchases.md)
* [Implement a trial version of your app](implement-a-trial-version-of-your-app.md)

For a sample app that demonstrates how to use **StoreContext** and other types in the **Windows.Services.Store** namespace, see the [Store sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/Store).

<span id="implement-iap" />
### Implement in-app purchases

To offer an in-app purchase to customers in your app using the **Windows.Services.Store** namespace:

1. If your app offers add-ons that customers can purchase, [create add-on submissions for your app in the Dev Center dashboard](https://msdn.microsoft.com/windows/uwp/publish/add-on-submissions).

2. Write code in your app to [retrieve product info for your app or an add-on offered by your app](get-product-info-for-apps-and-add-ons.md) and then [determine whether the license is active](get-license-info-for-apps-and-add-ons.md) (that is, whether the user has a license to use the app or add-on). If the license isn't active, display a UI that offers the app or add-on for sale to the user as an in-app purchase.

3. If the user chooses to purchase your app or add-on, use the appropriate method to purchase the product:

    * If the user is purchasing your app or a durable add-on, follow the instructions in [Enable in-app purchases of apps and add-ons](enable-in-app-purchases-of-apps-and-add-ons.md).
    * If the user is purchasing a consumable add-on, follow the instructions in [Enable consumable add-on purchases](enable-consumable-add-on-purchases.md).

4. Test your implementation by following the [testing guidance](#testing) in this article.

<span id="implement-trial" />
### Implement trial functionality

To exclude or limit features in a trial version of your app using the **Windows.Services.Store** namespace:

1. [Configure your app as a free trial in the Windows Dev Center dashboard](../publish/set-app-pricing-and-availability.md#free-trial).

2. Write code in your app to [retrieve product info for your app or an add-on offered by your app](get-product-info-for-apps-and-add-ons.md) and then [determine whether the license associated with the app is a trial license](get-license-info-for-apps-and-add-ons.md).

3. Exclude or limit certain features in your app if it is a trial, and then enable the features when the user purchases a full license. For more information and a code example, see [Implement a trial version of your app](implement-a-trial-version-of-your-app.md).

4. Test your implementation by following the [testing guidance](#testing) in this article.

<span id="testing" />
### Test your in-app purchase or trial implementation

The **Windows.Services.Store** namespace does not provide a class that you can use to simulate license info during testing. Instead, you must publish an app to the Store and download that app to your development device to use its license for testing. This is a different experience from apps that use the **Windows.ApplicationModel.Store** namespace, which can use the [CurrentAppSimulator](https://msdn.microsoft.com/library/windows/apps/hh779766) class to simulate license info during testing.

If your app uses APIs in the **Windows.Services.Store** namespace to access info for your app and its add-ons, follow this process to test your code:

1. If your app is not yet published and available in the Store, make sure your app meets the minimum [Windows App Certification Kit](https://developer.microsoft.com/windows/develop/app-certification-kit) requirements, [submit your app](https://msdn.microsoft.com/windows/uwp/publish/app-submissions) to the Windows Dev Center dashboard, and make sure your app passes the certification process so it is available in the Store. You can [hide your app from the Store](https://msdn.microsoft.com/windows/uwp/publish/set-app-pricing-and-availability) so it is unavailable to customers while you test it.

2. Next, make sure you have completed the following:

    * Write code in your app that uses the [StoreContext](https://msdn.microsoft.com/library/windows/apps/windows.services.store.storecontext.aspx) class and other related types in the **Windows.Services.Store** namespace to implement [in-app purchases](#implement-iap) or [trial functionality](#implement-trial).
    * If your app offers an add-on that customers can purchase, [create an add-on submission for your app in the Dev Center dashboard](https://msdn.microsoft.com/windows/uwp/publish/add-on-submissions).
    * If you want to exclude or limit some features in a trial version of your app, [configure your app as a free trial in the Windows Dev Center dashboard](../publish/set-app-pricing-and-availability.md#free-trial).

3. With your project open in Visual Studio, click the **Project menu**, point to **Store**, and then click **Associate App with the Store**. Complete the instructions in the wizard to associate the app project with the app in your Windows Dev Center account that you want to use for testing.
    > [!NOTE]
    > If you do not associate your project with an app in the Store, the [StoreContext](https://msdn.microsoft.com/library/windows/apps/windows.services.store.storecontext.aspx) methods set the **ExtendedError** property of their return values to the error code value 0x803F6107. This value indicates that the Store doesn't have any knowledge about the app.
4. If you have not done so already, install the app from the Store that you specified in the previous step, run the app once, and then close this app. This ensures that a valid license for the app is installed to your development device.

5. In Visual Studio, start running or debugging your project. Your code should retrieve app and add-on data from the Store app that you associated with your local project. If you are prompted to reinstall the app, follow the instructions and then run or debug your project.

> [!NOTE]
> After you complete these steps, you can continue to update your app's code and then debug your updated project on your development computer without submitting new app packages to the Store. You only need to download the Store version of your app to your development computer once to obtain the local license that will be used for testing. You only need to submit new app packages to the Store after you complete your testing and you want to make the in-app purchase or trial-related features in your app available to your customers.

<span id="receipts" />
### Receipts for in-app purchases

The **Windows.Services.Store** namespace does not provide an API you can use to obtain a transaction receipt for successful purchases in your app's code. This is a different experience from apps that use the **Windows.ApplicationModel.Store** namespace, which can [use a client-side API to retrieve a transaction receipt](use-receipts-to-verify-product-purchases.md).

If you implement in-app purchases using the **Windows.Services.Store** namespace and you want to validate whether a given customer has purchased an app or add-on, you can use the [query for products method](query-for-products.md) in the [Windows Store collection REST API](view-and-grant-products-from-a-service.md). The return data for this method confirms whether the specified customer has an entitlement for a given product, and provides data for the transaction in which the user acquired the product. The Windows Store collection API uses Azure AD authentication to retrieve this information.

<span id="desktop" />
### Using the StoreContext class with the Desktop Bridge

Desktop applications that use the [Desktop Bridge](https://developer.microsoft.com/windows/bridges/desktop) can use the [StoreContext](https://msdn.microsoft.com/library/windows/apps/windows.services.store.storecontext.aspx) class to implement in-app purchases and trials. However, if you have a Win32 desktop application or a desktop application that has a window handle (HWND) that is associated with the rendering framework (such as a WPF application), your application must configure the **StoreContext** object to specify which application window is the owner window for modal dialogs that are shown by the object.

Many **StoreContext** members (and members of other related types that are accessed through the **StoreContext** object) display a modal dialog to the user for Store-related operations such as purchasing a product. If a desktop application does not configure the **StoreContext** object to specify the owner window for modal dialogs, this object will return inaccurate data or errors.

To configure a **StoreContext** object in a desktop application that uses the Desktop Bridge, follow these steps.

1. Do one of the following to enable your app to access the [IInitializeWithWindow](https://msdn.microsoft.com/library/windows/desktop/hh706981.aspx) interface:

    * If your application is written in a managed language such as C# or Visual Basic, declare the **IInitializeWithWindow** interface in your app's code with the [ComImport](https://msdn.microsoft.com/library/system.runtime.interopservices.comimportattribute.aspx) attribute as shown in the following C# example. This example assumes that your code file has a **using** statement for the **System.Runtime.InteropServices** namespace.

    ```csharp
    [ComImport]
    [Guid("3E68D4BD-7135-4D10-8018-9FB6D9F33FA1")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IInitializeWithWindow
    {
        void Initialize(IntPtr hwnd);
    }
    ```

    * If your application is written in C++, add a reference to the shobjidl.h header file in your code. This header file contains the declaration of the **IInitializeWithWindow** interface.

2. Get a [StoreContext](https://msdn.microsoft.com/library/windows/apps/windows.services.store.storecontext.aspx) object by using the [GetDefault](https://msdn.microsoft.com/library/windows/apps/windows.services.store.storecontext.getdefault.aspx) method (or [GetForUser](https://msdn.microsoft.com/library/windows/apps/windows.services.store.storecontext.getforuser.aspx) if your app is a [multi-user app](../xbox-apps/multi-user-applications.md)) as described earlier in this article, and cast this object to an [IInitializeWithWindow](https://msdn.microsoft.com/library/windows/desktop/hh706981.aspx) object. Then, call the [IInitializeWithWindow.Initialize](https://msdn.microsoft.com/library/windows/desktop/hh706982.aspx) method, and pass the handle of the window that you want to be the owner for any modal dialogs that are shown by **StoreContext** methods. The following C# example shows how to pass the handle of your app's main window to the method.
    ```csharp
    StoreContext context = StoreContext.GetDefault();
    IInitializeWithWindow initWindow = (IInitializeWithWindow)(object)context;
    initWindow.Initialize(System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle);
    ```

<span id="products-skus" />
### Products, SKUs, and availabilities

Every product in the Store has at least one *SKU*, and each SKU has at least one *availability*. These concepts are abstracted away from most developers in the Windows Dev Center dashboard, and most developers will never define SKUs or availabilities for their apps or add-ons. However, because the object model for Store products in the **Windows.Services.Store** namespace includes SKUs and availabilities, a basic understanding of these concepts can be helpful.

| Object type |  Description  |
|---------|-------------------|
| [StoreProduct](https://msdn.microsoft.com/library/windows/apps/windows.services.store.storeproduct.aspx)  |  This class represents any type of product that is available in the Store, including an app or add-on. This class provides properties you can use to access data such as the Store ID of the product, the images and videos for the Store listing, and pricing info. It also provides methods you can use to purchase the product. |
| [StoreSku](https://msdn.microsoft.com/library/windows/apps/windows.services.store.storesku.aspx) |  This class represents a *SKU* for a product. A SKU is a specific version of a product with its own description, price, and other unique product details. Each app or add-on has a default SKU. The only time most developers will ever have multiple SKUs for an app is if they publish a full version of their app and a trial version (in the Store catalog, each of these versions is a different SKU of the same app). <p/><p/> Some publishers have the ability to define their own SKUs. For example, a large game publisher might release a game with one SKU that shows green blood in markets that don't allow red blood and a different SKU that shows red blood in all other markets. Alternatively, a publisher who sells digital video content might publish two SKUs for a video, one SKU for the high-definition version and a different SKU for the standard-definition version. <p/><p/> Each product has a [Skus](https://msdn.microsoft.com/library/windows/apps/windows.services.store.storeproduct.skus.aspx) property you can use to access the SKUs. |
| [StoreAvailability](https://msdn.microsoft.com/library/windows/apps/windows.services.store.storeavailability.aspx)  |  This class represents an *availability* for a SKU. An availability is a specific version of a SKU with its own unique pricing info. Each SKU has a default availability. Some publishers have the ability to define their own availabilities to introduce different price options for a given SKU. <p/><p/> Each SKU has an [Availabilities](https://msdn.microsoft.com/library/windows/apps/windows.services.store.storesku.availabilities.aspx) property you can use to access the availabilities. For most developers, each SKU has a single default availability.  |

<span id="store_ids" />
### Store IDs

Every app and add-on in the Store has an associated **Store ID**. Many of the APIs in the **Windows.Services.Store** namespace require the Store ID in order to perform an operation on an app or add-on. Products, SKUs, and availabilities have different Store ID formats.

| Object type |  Store ID format  |
|---------|-------------------|
| [StoreProduct](https://msdn.microsoft.com/library/windows/apps/windows.services.store.storeproduct.aspx)  |  The Store ID of any product in the Store is 12-character alpha-numeric string, such as ```9NBLGGH4R315```. This Store ID is available in the Windows Dev Center dashboard page for the app or add-on, and it is returned by the [StoreId](https://msdn.microsoft.com/library/windows/apps/windows.services.store.storeproduct.storeid.aspx) property [StoreProduct](https://msdn.microsoft.com/library/windows/apps/windows.services.store.storeproduct.aspx) object. This ID is sometimes called the *product Store ID*. |
| [StoreSku](https://msdn.microsoft.com/library/windows/apps/windows.services.store.storesku.aspx) |  For a SKU, the Store ID has the format ```<product Store ID>/xxxx```, where ```xxxx``` is a 4-character alpha-numeric string that identifies a SKU for the product. For example, ```9NBLGGH4R315/000N```. This ID is returned by the [StoreId](https://msdn.microsoft.com/library/windows/apps/windows.services.store.storesku.storeid.aspx) property of a  [StoreSku](https://msdn.microsoft.com/library/windows/apps/windows.services.store.storesku.aspx) object, and it is sometimes called the *SKU Store ID*. |
| [StoreAvailability](https://msdn.microsoft.com/library/windows/apps/windows.services.store.storeavailability.aspx)  |  For an availability, the Store ID has the format ```<product Store ID>/xxxx/yyyyyyyyyyyy```, where ```xxxx``` is a 4-character alpha-numeric string that identifies a SKU for the product and ```yyyyyyyyyyyy``` is a 12-character alpha-numeric string that identifies an availability for the SKU. For example, ```9NBLGGH4R315/000N/4KW6QZD2VN6X```. This ID is returned by the [StoreId](https://msdn.microsoft.com/library/windows/apps/windows.services.store.storeavailability.storeid.aspx) property of a  [StoreAvailability](https://msdn.microsoft.com/library/windows/apps/windows.services.store.storeavailability.aspx) object, and it is sometimes called the *availability Store ID*.  |

## Related topics

* [Get product info for apps and add-ons](get-product-info-for-apps-and-add-ons.md)
* [Get license info for apps and add-ons](get-license-info-for-apps-and-add-ons.md)
* [Enable in-app purchases of apps and add-ons](enable-in-app-purchases-of-apps-and-add-ons.md)
* [Enable consumable add-on purchases](enable-consumable-add-on-purchases.md)
* [Implement a trial version of your app](implement-a-trial-version-of-your-app.md)
* [In-app purchases and trials using the Windows.ApplicationModel.Store namespace](in-app-purchases-and-trials-using-the-windows-applicationmodel-store-namespace.md)
