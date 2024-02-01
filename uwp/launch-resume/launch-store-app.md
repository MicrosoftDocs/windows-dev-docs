---
title: Using ms-windows-store URIs
description: Launch the Microsoft Store app directly to a landing page or your product's page.
ms.date: 01/11/2022
ms.topic: article
---

# Using ms-windows-store URIs

This topic describes the **ms-windows-store:** URI scheme. Your app can use this URI scheme to launch the Microsoft Store app to specific pages in the store by using the [**LaunchUriAsync**](/uwp/api/windows.system.launcher.launchuriasync) method on Windows 10 and Windows 11.

For example, you can open the Store to the Games page using the following code:

```csharp
bool result = await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-windows-store://navigatetopage/?Id=Gaming"));
```

## Opening the Store to specific sections

You can launch the Store App to a specific page or section using the following options.

| URI Scheme                                       | Description                                     | Notes |
|--------------------------------------------------|-------------------------------------------------|-------|
| ms-windows-store://home                          | Launches the home page of the Store.            |       |
| ms-windows-store://navigatetopage/?Id=[vertical] | Launches a top-level vertical page of the store | Verticals include: `Home`, `Gaming`, `Entertainment`, `Productivity`, and `LOB`, but note that available verticals can vary by region. |
| ms-windows-store://downloadsandupdates           | Launches the downloads and updates page.        | Starting with the October 2021 update to the Store app, this will launch the Library page. |
| ms-windows-store://settings                      | Launches the Store settings page.               |       |

## Opening to a specific product

You can launch the Store directly to the product detail page (PDP) for a specific product by using the Product ID for an App. While the Store app on Windows 10 and Windows 11 still supports Package Family Name (PFN) and App IDs, these are deprecated and may not be supported in the future. These values can be found in Partner Center on the App identity page in the Product management section for each app.

Starting with the October 2021 update to the Store app, there are two modes available for displaying the PDP. By default the Store app is opened to the product detail page. You can also launch the store into with a popup experience that displays a smaller PDP dialog with that only displays the essential details for your app and a single action button for users. For the popup experience, you can optionally specify the location of a window that the dialog should be centered above.

| URI Scheme                                                          | Description | Notes |
|---------------------------------------------------------------------|-------------|-------|
| ms-windows-store://pdp/?ProductId=9WZDNCRFHVJL                      | Launches the full product details page (PDP) for a product | This is the recommended way to link to a specific product.
| ms-windows-store://pdp/?PFN= Microsoft.Office.OneNote_8wekyb3d8bbwe | Launches the full product details page (PDP) for a product | Using the package family name is deprecated.
| ms-windows-store://pdp/?AppId=f022389f-f3a6-417e-ad23-704fbdf57117  | Launches the full product details page (PDP) for a product | Using the App ID is deprecated.
| ms-windows-store://pdp/?ProductId=9WZDNCRFHVJL&mode=mini            | Launches the popup Store dialog experience | The popup experience only supports Product ID |

## Launching the rating and review experience for a product

To enable users to review your app, you can link to that PDP and launch directly into the rating and review dialog. Store ID is the recommended method to launch the Store app to a specific product detail page.

| URI Scheme                                                             | Description | Notes |
|------------------------------------------------------------------------|-------------|-------|
| ms-windows-store://review/?ProductId=9WZDNCRFHVJL                      | Launches the write a review experience for a product. | Using StoreId is recommended |
| ms-windows-store://review/?PFN= Microsoft.Office.OneNote_8wekyb3d8bbwe | Launches the write a review experience for a product. | Using product family name is deprecated. |
| ms-windows-store://review/?AppId=f022389f-f3a6-417e-ad23-704fbdf57117  | Launches the write a review experience for a product. | Using productid is deprecated |

## Searching the Store

You can launch the Store app directly into search results with several supported methods for searching store content.

| URI Scheme                                                                | Description | Notes |
|---------------------------------------------------------------------------|-------------|-------|
| ms-windows-store://assoc/?Tags=Photos_Rich_Media_Edit, Camera_Capture_App | Launches a search for products associated with one or more tags.  | Tags must be separated by commas. |
| ms-windows-store://search/?query=OneNote                                  | Launches a search for the specified query. | Spaces in the query are allowed. |
| ms-windows-store://browse/?type=Apps&cat=Health+%26+fitness               | Launches a search for products in a category. | |
| ms-windows-store://publisher/?name=Microsoft Corporation                  | Launches a search for products from the specified publisher. | Spaces in the name are allowed. |
| ms-windows-store://assoc/?FileExt=pdf                                     | Launches a search for products associated with a file extension. | |
| ms-windows-store://assoc/?Protocol=ms-word                                | Launches a search for products associated with a protocol. | |
