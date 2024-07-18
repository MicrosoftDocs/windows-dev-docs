---
ms.assetid: F45E6F35-BC18-45C8-A8A5-193D528E2A4E
description: Learn the basic tasks and concepts needed to implement and test in-app purchases and trial functionality in UWP apps.
title: In-app purchases and trials
ms.date: 05/09/2018
ms.topic: article
keywords: windows 10, uwp, in-app purchases, IAPs, add-ons, trials, consumable, durable, subscription
ms.localizationpriority: medium
---

# In-app purchases and trials

The Windows SDK provides APIs you can use to implement the following features to make more money from your Universal Windows Platform (UWP) app:

* **In-app purchases**&nbsp;&nbsp;Whether your app is free or not, you can sell content or new app functionality (such as unlocking the next level of a game) from right within the app.

* **Trial functionality**&nbsp;&nbsp;If you [configure your app as a free trial in Partner Center](/windows/apps/publish/publish-your-app/price-and-availability?pivots=store-installer-msix#free-trial), you can entice your customers to purchase the full version of your app by excluding or limiting some features during the trial period. You can also enable features, such as banners or watermarks, that are shown only during the trial, before a customer buys your app.

This article provides an overview of how in-app purchases and trials work in UWP apps.

<span id="choose-namespace"></span>

## Choose which namespace to use

There are two different namespaces you can use to add in-app purchases and trial functionality to your UWP apps, depending on which version of Windows 10 or Windows 11 your apps target. Although the APIs in these namespaces serve the same goals, they are designed quite differently, and code is not compatible between the two APIs.

* **[Windows.Services.Store](/uwp/api/windows.services.store)**&nbsp;&nbsp;Starting in Windows 10, version 1607, apps can use the API in this namespace to implement in-app purchases and trials. We recommend that you use the members in this namespace if your app project targets **Windows 10 Anniversary Edition (10.0; Build 14393)** or a later release in Visual Studio. This namespace supports the latest add-on types, such as Store-managed consumable add-ons, and is designed to be compatible with future types of products and features supported by Partner Center and the Store. For more information about this namespace, see the [In-app purchases and trials using the Windows.Services.Store namespace](#api_intro) section in this article.

* **[Windows.ApplicationModel.Store](/uwp/api/windows.applicationmodel.store)**&nbsp;&nbsp;All versions of Windows 10 and Windows 11 also support an older API for in-app purchases and trials in this namespace. For information about the **Windows.ApplicationModel.Store** namespace, see [In-app purchases and trials using the Windows.ApplicationModel.Store namespace](in-app-purchases-and-trials-using-the-windows-applicationmodel-store-namespace.md).

> [!IMPORTANT]
> The **Windows.ApplicationModel.Store** namespace is no longer being updated with new features, and we recommend that you use the **Windows.Services.Store** namespace instead if possible for your app. The **Windows.ApplicationModel.Store** namespace is not supported in Windows desktop applications that use the [Desktop Bridge](/windows/msix/desktop/source-code-overview) or in apps or games that use a development sandbox in Partner Center (for example, this is the case for any game that integrates with Xbox Live).

<span id="concepts"></span>

## Basic concepts

Every item that is offered in the Store is generally called a *product*. Most developers only work with the following types of products: *apps* and *add-ons*.

An add-on is a product or feature that you make available to your customers in the context of your app: for example, currency to be used in an app or game, new maps or weapons for a game, the ability to use your app without ads, or digital content such as music or videos for apps that have the ability to offer that type of content. Every app and add-on has an associated license that indicates whether the user is entitled to use the app or add-on. If the user is entitled to use the app or add-on as a trial, the license also provides additional info about the trial.

To offer an add-on to customers in your app, you must [define the add-on for your app in Partner Center](/windows/apps/publish/publish-your-app/create-app-submission?pivots=store-installer-add-on) so the Store knows about it. Then, your app can use APIs in the **Windows.Services.Store** or **Windows.ApplicationModel.Store** namespace to offer the add-on for sale to the user as an in-app purchase.

UWP apps can offer the following types of add-ons.

| Add-on type |  Description  |
|---------|-------------------|
| Durable  |  An add-on that persists for the lifetime that you [specify in Partner Center](/windows/apps/publish/publish-your-app/enter-app-properties?pivots=store-installer-add-on). </p></p>By default, durable add-ons never expire, in which case they can only be purchased once. If you specify a particular duration for the add-on, the user can repurchase the add-on after it expires. |
| Developer-managed consumable  |  An add-on that can be purchased, used, and then purchased again after it is consumed. You are responsible for keeping track of the user's balance of items that the add-on represents.</p></p>When the user consumes any items that are associated with the add-on, you are responsible for maintaining the user's balance and for reporting the purchase of the add-on as fulfilled to the Store after the user has consumed all the items. The user cannot purchase the add-on again until your app has reported the previous add-on purchase as fulfilled. </p></p>For example, if your add-on represents 100 coins in a game and the user consumes 10 coins, your app or service must maintain the new remaining balance of 90 coins for the user. After the user has consumed all 100 coins, your app must report the add-on as fulfilled, and then the user can purchase the 100 coin add-on again.    |
| Store-managed consumable  |  An add-on that can be purchased, used, and purchased again at any time. The Store keeps track of the user's balance of items that the add-on represents.</p></p>When the user consumes any items that are associated with the add-on, you are responsible for reporting those items as fulfilled to the Store, and the Store updates the user's balance. The user can purchase the add-on as many times as they want (they do not need to consume the items first). Your app can query for the current balance for the user at any time. </p></p> For example, if your add-on represents an initial quantity of 100 coins in a game and the user consumes 50 coins, your app reports to the Store that 50 units of the add-on were fulfilled, and the Store updates the remaining balance. If the user then repurchases your add-on to acquire 100 more coins, they will now have 150 coins total. </p></p>**Note**&nbsp;&nbsp;To use Store-managed consumables, your app must target **Windows 10 Anniversary Edition (10.0; Build 14393)** or a later release in Visual Studio, and it must use the **Windows.Services.Store** namespace instead of the **Windows.ApplicationModel.Store** namespace.  |
| Subscription | A durable add-on where the customer continues to be charged at recurring intervals in order to keep using the add-on. The customer can cancel the subscription at any time to avoid further charges. </p></p>**Note**&nbsp;&nbsp;To use subscription add-ons, your app must target **Windows 10 Anniversary Edition (10.0; Build 14393)** or a later release in Visual Studio, and it must use the **Windows.Services.Store** namespace instead of the **Windows.ApplicationModel.Store** namespace.  |

<span />

> [!NOTE]
> Other types of add-ons, such as durable add-ons with packages (also known as downloadable content or DLC) are only available to a restricted set of developers, and are not covered in this documentation.

<span id="api_intro"></span>

## In-app purchases and trials using the Windows.Services.Store namespace

This section provides an overview of important tasks and concepts for the [Windows.Services.Store](/uwp/api/windows.services.store) namespace. This namespace is available only to apps that target **Windows 10 Anniversary Edition (10.0; Build 14393)** or a later release in Visual Studio (this corresponds to Windows 10, version 1607). We recommend that apps use the **Windows.Services.Store** namespace instead of the [Windows.ApplicationModel.Store](/uwp/api/windows.applicationmodel.store) namespace if possible. For information about the **Windows.ApplicationModel.Store** namespace, see [this article](in-app-purchases-and-trials-using-the-windows-applicationmodel-store-namespace.md).

**In this section**

* [Video](#video)
* [Get started with the StoreContext class](#get-started-storecontext)
* [Implement in-app purchases](#implement-iap)
* [Implement trial functionality](#implement-trial)
* [Test your in-app purchase or trial implementation](#testing)
* [Receipts for in-app purchases](#receipts)
* [Using the StoreContext class with the Desktop Bridge](#desktop)
* [Products, SKUs, and availabilities](#products-skus)
* [Store IDs](#store-ids)

<span id="video"></span>



<span id="get-started-storecontext"></span>

### Get started with the StoreContext class

The main entry point to the **Windows.Services.Store** namespace is the [StoreContext](/uwp/api/windows.services.store.storecontext) class. This class provides methods you can use to get info for the current app and its available add-ons, get license info for the current app or its add-ons, purchase an app or add-on for the current user, and perform other tasks. To get a [StoreContext](/uwp/api/windows.services.store.storecontext) object, do one of the following:

* In a single-user app (that is, an app that runs only in the context of the user that launched the app), use the static [GetDefault](/uwp/api/windows.services.store.storecontext.getdefault) method to get a **StoreContext** object that you can use to access Microsoft Store-related data for the user. Most Universal Windows Platform (UWP) apps are single-user apps.

  ```csharp
  Windows.Services.Store.StoreContext context = StoreContext.GetDefault();
  ```

* In a [multi-user app](../xbox-apps/multi-user-applications.md), use the static [GetForUser](/uwp/api/windows.services.store.storecontext.getforuser) method to get a **StoreContext** object that you can use to access Microsoft Store-related data for a specific user who is signed in with their Microsoft account while using the app. The following example gets a **StoreContext** object for the first available user.

  ```csharp
  var users = await Windows.System.User.FindAllAsync();
  Windows.Services.Store.StoreContext context = StoreContext.GetForUser(users[0]);
  ```

> [!NOTE]
> Windows desktop applications that use the [Desktop Bridge](/windows/msix/desktop/source-code-overview) must perform additional steps to configure the [StoreContext](/uwp/api/windows.services.store.storecontext) object before they can use this object. For more information, see [this section](#desktop).

After you have a [StoreContext](/uwp/api/windows.services.store.storecontext) object, you can start calling methods of this object to get Store product info for the current app and its add-ons, retrieve license info for the current app and its add-ons, purchase an app or add-on for the current user, and perform other tasks. For more information about common tasks you can perform using this object, see the following articles:

* [Get product info for apps and add-ons](get-product-info-for-apps-and-add-ons.md)
* [Get license info for apps and add-ons](get-license-info-for-apps-and-add-ons.md)
* [Enable in-app purchases of apps and add-ons](enable-in-app-purchases-of-apps-and-add-ons.md)
* [Enable consumable add-on purchases](enable-consumable-add-on-purchases.md)
* [Enable subscription add-ons for your app](enable-subscription-add-ons-for-your-app.md)
* [Implement a trial version of your app](implement-a-trial-version-of-your-app.md)

For a sample app that demonstrates how to use **StoreContext** and other types in the **Windows.Services.Store** namespace, see the [Store sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/Store).

<span id="implement-iap"></span>

### Implement in-app purchases

To offer an in-app purchase to customers in your app using the **Windows.Services.Store** namespace:

1. If your app offers add-ons that customers can purchase, [create add-on submissions for your app in Partner Center ](/windows/apps/publish/publish-your-app/create-app-submission?pivots=store-installer-add-on).

2. Write code in your app to [retrieve product info for your app or an add-on offered by your app](get-product-info-for-apps-and-add-ons.md) and then [determine whether the license is active](get-license-info-for-apps-and-add-ons.md) (that is, whether the user has a license to use the app or add-on). If the license isn't active, display a UI that offers the app or add-on for sale to the user as an in-app purchase.

3. If the user chooses to purchase your app or add-on, use the appropriate method to purchase the product:

    * If the user is purchasing your app or a durable add-on, follow the instructions in [Enable in-app purchases of apps and add-ons](enable-in-app-purchases-of-apps-and-add-ons.md).
    * If the user is purchasing a consumable add-on, follow the instructions in [Enable consumable add-on purchases](enable-consumable-add-on-purchases.md).
    * If the user is purchasing a subscription add-on, follow the instructions in [Enable subscription add-ons for your app](enable-subscription-add-ons-for-your-app.md).

4. Test your implementation by following the [testing guidance](#testing) in this article.

<span id="implement-trial"></span>

### Implement trial functionality

To exclude or limit features in a trial version of your app using the **Windows.Services.Store** namespace:

1. [Configure your app as a free trial in Partner Center](/windows/apps/publish/publish-your-app/price-and-availability?pivots=store-installer-msix#free-trial).

2. Write code in your app to [retrieve product info for your app or an add-on offered by your app](get-product-info-for-apps-and-add-ons.md) and then [determine whether the license associated with the app is a trial license](get-license-info-for-apps-and-add-ons.md).

3. Exclude or limit certain features in your app if it is a trial, and then enable the features when the user purchases a full license. For more information and a code example, see [Implement a trial version of your app](implement-a-trial-version-of-your-app.md).

4. Test your implementation by following the [testing guidance](#testing) in this article.

<span id="testing"></span>

### Test your in-app purchase or trial implementation

If your app uses APIs in the **Windows.Services.Store** namespace to implement in-app purchase or trial functionality, you must publish your app to the Store and download the app to your development device to use its license for testing. Follow this process to test your code:

1. If your app is not yet published and available in the Store, make sure your app meets the minimum [Windows App Certification Kit](https://developer.microsoft.com/windows/develop/app-certification-kit) requirements, [submit your app](/windows/apps/publish/publish-your-app/create-app-submission?pivots=store-installer-msix) in Partner Center, and make sure your app passes the certification process. You can [configure your app so it is not discoverable in the Store](/windows/apps/publish/publish-your-app/price-and-availability?pivots=store-installer-msix) while you test it. Please note the proper configuration of [package flights](/windows/apps/publish/package-flights). Incorrectly configured package flights may be not be able to be downloaded.

2. Next, make sure you have completed the following:

    * Write code in your app that uses the [StoreContext](/uwp/api/windows.services.store.storecontext) class and other related types in the **Windows.Services.Store** namespace to implement [in-app purchases](#implement-iap) or [trial functionality](#implement-trial).
    * If your app offers an add-on that customers can purchase, [create an add-on submission for your app in Partner Center](/windows/apps/publish/publish-your-app/create-app-submission?pivots=store-installer-add-on).
    * If you want to exclude or limit some features in a trial version of your app, [configure your app as a free trial in Partner Center](/windows/apps/publish/publish-your-app/price-and-availability?pivots=store-installer-msix#free-trial).

3. With your project open in Visual Studio, click the **Project menu**, point to **Store**, and then click **Associate App with the Store**. Complete the instructions in the wizard to associate the app project with the app in your Partner Center account that you want to use for testing.
    > [!NOTE]
    > If you do not associate your project with an app in the Store, the [StoreContext](/uwp/api/windows.services.store.storecontext) methods set the **ExtendedError** property of their return values to the error code value 0x803F6107. This value indicates that the Store doesn't have any knowledge about the app.
4. If you have not done so already, install the app from the Store that you specified in the previous step, run the app once, and then close this app. This ensures that a valid license for the app is installed to your development device.

5. In Visual Studio, start running or debugging your project. Your code should retrieve app and add-on data from the Store app that you associated with your local project. If you are prompted to reinstall the app, follow the instructions and then run or debug your project.
    > [!NOTE]
    > After you complete these steps, you can continue to update your app's code and then debug your updated project on your development computer without submitting new app packages to the Store. You only need to download the Store version of your app to your development computer once to obtain the local license that will be used for testing. You only need to submit new app packages to the Store after you complete your testing and you want to make the in-app purchase or trial-related features in your app available to your customers.

If your app uses the **Windows.ApplicationModel.Store** namespace, you can use the [CurrentAppSimulator](/uwp/api/Windows.ApplicationModel.Store.CurrentAppSimulator) class in your app to simulate license info during testing before you submit your app to the Store. For more information, see [Get started with the CurrentApp and CurrentAppSimulator classes](in-app-purchases-and-trials-using-the-windows-applicationmodel-store-namespace.md#get-started-with-the-currentapp-and-currentappsimulator-classes).  

> [!NOTE]
> The **Windows.Services.Store** namespace does not provide a class that you can use to simulate license info during testing. If you use the **Windows.Services.Store** namespace to implement in-app purchases or trials, you must publish your app to the Store and download the app to your development device to use its license for testing as described above.

<span id="receipts"></span>

### Receipts for in-app purchases

The **Windows.Services.Store** namespace does not provide an API you can use to obtain a transaction receipt for successful purchases in your app's code. This is a different experience from apps that use the **Windows.ApplicationModel.Store** namespace, which can [use a client-side API to retrieve a transaction receipt](use-receipts-to-verify-product-purchases.md).

If you implement in-app purchases using the **Windows.Services.Store** namespace and you want to validate whether a given customer has purchased an app or add-on, you can use the [query for products method](query-for-products.md) in the [Microsoft Store collection REST API](view-and-grant-products-from-a-service.md). The return data for this method confirms whether the specified customer has an entitlement for a given product, and provides data for the transaction in which the user acquired the product. The Microsoft Store collection API uses Azure AD authentication to retrieve this information.

<span id="desktop"></span>

### Using the StoreContext class with the Desktop Bridge

Desktop applications that use the [Desktop Bridge](/windows/msix/desktop/source-code-overview) can use the [StoreContext](/uwp/api/windows.services.store.storecontext) class to implement in-app purchases and trials. However, if you have a Win32 desktop application or a desktop application that has a window handle (HWND) that is associated with the rendering framework (such as a WPF or Windows App SDK application), your application must configure the **StoreContext** object to specify which application window is the owner window for modal dialogs that are shown by the object.

Many **StoreContext** members (and members of other related types that are accessed through the **StoreContext** object) display a modal dialog to the user for Store-related operations such as purchasing a product. If a desktop application does not configure the **StoreContext** object to specify the owner window for modal dialogs, this object will return inaccurate data or errors.

To configure a **StoreContext** object in a desktop application that uses the Desktop Bridge, follow these steps.

#### For .NET 6 or later

If your application is written in C# with .NET 6 or later, then follow these steps.

1. Make sure that the `TargetFramework` property in the project file is [set to a specific Windows SDK version to access the Windows Runtime APIs](/windows/apps/desktop/modernize/desktop-to-uwp-enhance#net-6-and-later-use-the-target-framework-moniker-option), which provides access to the **WinRT.Interop** namespace. For example:

    ```xml
    <PropertyGroup>
      <!-- You can also target other versions of the Windows SDK and .NET, e.g. "net6.0-windows10.0.19041.0" -->
      <TargetFramework>net6.0-windows10.0.22000.0</TargetFramework>
    </PropertyGroup>
    ```

2. Get a [StoreContext](/uwp/api/windows.services.store.storecontext) object by using the [GetDefault](/uwp/api/windows.services.store.storecontext.getdefault) method (or [GetForUser](/uwp/api/windows.services.store.storecontext.getforuser) if your app is a [multi-user app](../xbox-apps/multi-user-applications.md)) as described earlier in this article). To initialize the dialog with the specified window handle, use the **WinRT.Interop.WindowNative.GetWindowHandle** and **WinRT.Interop.InitializeWithWindow.Initialize** methods (see [Retrieve a window handle (HWND)](/windows/apps/develop/ui-input/retrieve-hwnd) and [Display WinRT UI objects that depend on CoreWindow](/windows/apps/develop/ui-input/display-ui-objects)).

      ```csharp
      StoreContext context = StoreContext.GetDefault();
      // Obtain window handle by passing in pointer to the window object
      var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(windowObject);
      // Initialize the dialog using wrapper function for IInitializeWithWindow
      WinRT.Interop.InitializeWithWindow.Initialize(context, hwnd); 
      ```

#### For earlier versions of .NET or C++

If your application is written with an earlier version of .NET or in C++, follow these steps.

1. Do one of the following to enable your app to access the [IInitializeWithWindow](/windows/desktop/api/shobjidl_core/nn-shobjidl_core-iinitializewithwindow) interface:
 
    * If your application is written in a managed language such as C# or Visual Basic (prior to .NET 6), then declare the **IInitializeWithWindow** interface in your app's code with the [ComImport](/dotnet/api/system.runtime.interopservices.comimportattribute) attribute as shown in the following C# example. This example assumes that your code file has a **using** statement for the **System.Runtime.InteropServices** namespace.

        ```csharp
        [ComImport]
        [Guid("3E68D4BD-7135-4D10-8018-9FB6D9F33FA1")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IInitializeWithWindow
        {
            void Initialize(IntPtr hwnd);
        }
        ```

    * If your application is written in C++, then add a reference to the `shobjidl.h` header file in your code. This header file contains the declaration of the **IInitializeWithWindow** interface.

2. Get a [StoreContext](/uwp/api/windows.services.store.storecontext) object by using the [GetDefault](/uwp/api/windows.services.store.storecontext.getdefault) method (or [GetForUser](/uwp/api/windows.services.store.storecontext.getforuser) if your app is a [multi-user app](../xbox-apps/multi-user-applications.md)) as described earlier in this article, and cast this object to an [IInitializeWithWindow](/windows/desktop/api/shobjidl_core/nn-shobjidl_core-iinitializewithwindow) object. Then, call the [IInitializeWithWindow.Initialize](/windows/desktop/api/shobjidl_core/nf-shobjidl_core-iinitializewithwindow-initialize) method, and pass the handle of the window that you want to be the owner for any modal dialogs that are shown by **StoreContext** methods. The following C# example shows how to pass the handle of your app's main window to the method. Also see [Retrieve a window handle (HWND)](/windows/apps/develop/ui-input/retrieve-hwnd) and [Display WinRT UI objects that depend on CoreWindow](/windows/apps/develop/ui-input/display-ui-objects).

    ```csharp
    StoreContext context = StoreContext.GetDefault();
    IInitializeWithWindow initWindow = (IInitializeWithWindow)(object)context;
    initWindow.Initialize(System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle);
    ```

<span id="products-skus"></span>

### Products, SKUs, and availabilities

Every product in the Store has at least one *SKU*, and each SKU has at least one *availability*. These concepts are abstracted away from most developers in Partner Center, and most developers will never define SKUs or availabilities for their apps or add-ons. However, because the object model for Store products in the **Windows.Services.Store** namespace includes SKUs and availabilities, a basic understanding of these concepts can be helpful for some scenarios.

| Object |  Description  |
|---------|-------------------|
| Product  |  A *product* refers to any type of product that is available in the Store, including an app or add-on. </p></p> Each product in the Store has a corresponding [StoreProduct](/uwp/api/windows.services.store.storeproduct) object. This class provides properties you can use to access data such as the Store ID of the product, the images and videos for the Store listing, and pricing info. It also provides methods you can use to purchase the product. |
| SKU |  A *SKU* is a specific version of a product with its own description, price, and other unique product details. Each app or add-on has a default SKU. The only time most developers will ever have multiple SKUs for an app is if they publish a full version of their app and a trial version (in the Store catalog, each of these versions is a different SKU of the same app). </p></p> Some publishers have the ability to define their own SKUs. For example, a large game publisher might release a game with one SKU that shows green blood in markets that don't allow red blood and a different SKU that shows red blood in all other markets. Alternatively, a publisher who sells digital video content might publish two SKUs for a video, one SKU for the high-definition version and a different SKU for the standard-definition version. </p></p> Each SKU in the Store has a corresponding [StoreSku](/uwp/api/windows.services.store.storesku) object. Every [StoreProduct](/uwp/api/windows.services.store.storeproduct) has a [Skus](/uwp/api/windows.services.store.storeproduct.skus) property you can use to access the SKUs for the product. |
| Availability  |  An *availability* is a specific version of a SKU with its own unique pricing info. Each SKU has a default availability. Some publishers have the ability to define their own availabilities to introduce different price options for a given SKU. </p></p> Each availability in the Store has a corresponding [StoreAvailability](/uwp/api/windows.services.store.storeavailability) object. Every [StoreSku](/uwp/api/windows.services.store.storesku) has an [Availabilities](/uwp/api/windows.services.store.storesku.availabilities) property you can use to access the availabilities for the SKU. For most developers, each SKU has a single default availability.  |

<span id="store_ids"></span>

### Store IDs

Every app, add-on, or other product in the Store has an associated **Store ID** (this is also sometimes called a *product Store ID*). Many APIs require the Store ID in order to perform an operation on an app or add-on.

The Store ID of any product in the Store is 12-character alpha-numeric string, such as ```9NBLGGH4R315```. There are several different ways to get the Store ID for a product in the Store:

* For an app, you can get the Store ID on the [App identity page](/windows/apps/publish/view-app-identity-details) in Partner Center.
* For an add-on, you can get the Store ID on the add-on's overview page in Partner Center.
* For any product, you can also get the Store ID programmatically by using the [StoreId](/uwp/api/windows.services.store.storeproduct.storeid) property of the [StoreProduct](/uwp/api/windows.services.store.storeproduct) object that represents the product.

For products with SKUs and availabilities, the SKUs and availabilities also have their own Store IDs with different formats.

| Object |  Store ID format  |
|---------|-------------------|
| SKU |  The Store ID for a SKU has the format ```<product Store ID>/xxxx```, where ```xxxx``` is a 4-character alpha-numeric string that identifies a SKU for the product. For example, ```9NBLGGH4R315/000N```. This ID is returned by the [StoreId](/uwp/api/windows.services.store.storesku.storeid) property of a  [StoreSku](/uwp/api/windows.services.store.storesku) object, and it is sometimes called the *SKU Store ID*. |
| Availability  |  The Store ID for an availability has the format ```<product Store ID>/xxxx/yyyyyyyyyyyy```, where ```xxxx``` is a 4-character alpha-numeric string that identifies a SKU for the product and ```yyyyyyyyyyyy``` is a 12-character alpha-numeric string that identifies an availability for the SKU. For example, ```9NBLGGH4R315/000N/4KW6QZD2VN6X```. This ID is returned by the [StoreId](/uwp/api/windows.services.store.storeavailability.storeid) property of a  [StoreAvailability](/uwp/api/windows.services.store.storeavailability) object, and it is sometimes called the *availability Store ID*.  |

<span id="product-ids"></span>

## How to use product IDs for add-ons in your code

If you want to make an add-on available to your customers in the context of your app, you must [enter a unique product ID](/windows/apps/publish/publish-your-app/create-app-store-listing?pivots=store-installer-add-on#product-id) for your add-on when you [create your add-on submission](/windows/apps/publish/publish-your-app/create-app-submission?pivots=store-installer-add-on) in Partner Center. You can use this product ID to refer to the add-on in your code, although the specific scenarios in which you can use the product ID depend on which namespace you use for in-app purchases in your app.

> [!NOTE]
> The product ID that you enter in Partner Center for an add-on is different than the add-on's [Store ID](#store-ids). The Store ID is generated by Partner Center.

### Apps that use the Windows.Services.Store namespace

If your app uses the **Windows.Services.Store** namespace, you can use the product ID to easily identify the [StoreProduct](/uwp/api/Windows.Services.Store.StoreProduct) that represents your add-on or the [StoreLicense](/uwp/api/windows.services.store.storelicense) that represents your add-on's license. The product ID is exposed by the [StoreProduct.InAppOfferToken](/uwp/api/Windows.Services.Store.StoreProduct.InAppOfferToken) and [StoreLicense.InAppOfferToken](/uwp/api/windows.services.store.storelicense.InAppOfferToken) properties.

> [!NOTE]
> Although the product ID is a useful way to identify an add-on in your code, most operations in the **Windows.Services.Store** namespace use the [Store ID](#store-ids) of an add-on instead of the product ID. For example, to programmatically retrieve one or more known add-ons for an app, pass the Store IDs (rather than the product IDs) of the add-ons to the [GetStoreProductsAsync](/uwp/api/windows.services.store.storecontext.getstoreproductsasync) method. Similarly, to report a consumable add-on as fulfilled, pass the Store ID of the add-on (rather than the product ID) to the [ReportConsumableFulfillmentAsync](/uwp/api/windows.services.store.storecontext.reportconsumablefulfillmentasync) method.

### Apps that use the Windows.ApplicationModel.Store namespace

If your app uses the **Windows.ApplicationModel.Store** namespace, you'll need to use the product ID that you assign to an add-on in Partner Center for most operations. For example:

* Use the product ID to identify the [ProductListing](/uwp/api/windows.applicationmodel.store.productlisting) that represents your add-on or the [ProductLicense](/uwp/api/windows.applicationmodel.store.productlicense) that represents your add-on's license. The product ID is exposed by the [ProductListing.ProductId](/uwp/api/windows.applicationmodel.store.productlisting.ProductId) and [ProductLicense.ProductId](/uwp/api/windows.applicationmodel.store.productlicense.ProductId) properties.

* Specify the product ID when you purchase your add-on for the user by using the [RequestProductPurchaseAsync](/uwp/api/windows.applicationmodel.store.currentapp.requestproductpurchaseasync) method. For more information, see [Enable in-app product purchases](enable-in-app-product-purchases.md).

* Specify the product ID when you report your consumable add-on as fulfilled by using the [ReportConsumableFulfillmentAsync](/uwp/api/windows.applicationmodel.store.currentapp.reportconsumablefulfillmentasync) method. For more information, see [Enable consumable in-app product purchases](enable-consumable-in-app-product-purchases.md).

## Related topics

* [Get product info for apps and add-ons](get-product-info-for-apps-and-add-ons.md)
* [Get license info for apps and add-ons](get-license-info-for-apps-and-add-ons.md)
* [Enable in-app purchases of apps and add-ons](enable-in-app-purchases-of-apps-and-add-ons.md)
* [Enable consumable add-on purchases](enable-consumable-add-on-purchases.md)
* [Enable subscription add-ons for your app](enable-subscription-add-ons-for-your-app.md)
* [Implement a trial version of your app](implement-a-trial-version-of-your-app.md)
* [Error codes for Store operations](error-codes-for-store-operations.md)
* [In-app purchases and trials using the Windows.ApplicationModel.Store namespace](in-app-purchases-and-trials-using-the-windows-applicationmodel-store-namespace.md)
