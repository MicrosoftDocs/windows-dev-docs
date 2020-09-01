---
title: Launch the Microsoft Store app
description: This topic describes the ms-windows-store URI scheme. Your app can use this URI scheme to launch the Microsoft Store app to specific pages in the Store.
ms.assetid: 9A9C6576-1637-47D1-AC3B-D1A20D49E0FF
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Launch the Microsoft Store app



This topic describes the **ms-windows-store:** URI scheme. Your app can use this URI scheme to launch the Microsoft Store app to specific pages in the store by using the [**LaunchUriAsync**](/uwp/api/windows.system.launcher.launchuriasync) method.

This example shows how to open the Store to the Games page:

```cs
bool result = await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-windows-store://navigatetopage/?Id=Games"));
```

## ms-windows-store: URI scheme reference

<table>
<tr><th>Description</th><th></th><th>URI scheme</th></tr>
<tr><td>Launches the home page of the Store.</td><td /><td>ms-windows-store://home</td></tr>
<tr><td>Launches a top-level vertical in the Store.<p>Note: Not all users have access to all verticals.</p>
</td><td /><td>
<p>ms-windows-store://navigatetopage/?Id=Apps </p>
<p>ms-windows-store://navigatetopage/?Id=Games</p>
<p>ms-windows-store://navigatetopage/?Id=Music</p>
<p>ms-windows-store://navigatetopage/?Id=Video</p>
<p>ms-windows-store://navigatetopage/?Id=LOB</p>
</td>
</tr>
<tr>
<td rowspan="4">Launches the product details page (PDP) for a product. <p>Store ID is recommended for customers on Windows 10, and will work on all OS versions, but the earlier ways of doing it (ex: PFN) are still supported.</p>
<p>These values can be found in <a href="https://partner.microsoft.com/dashboard">Partner Center</a> on the <a href="https://docs.microsoft.com/windows/uwp/publish/view-app-identity-details">App identity</a> page in the App management section for each app.</p>
</td>
<td>
Store ID <p>(Recommended)</p>
</td>
<td>
<p>ms-windows-store://pdp/?ProductId=9WZDNCRFHVJL</p>
</td>
</tr>
<tr>
<td>Package Family Name (PFN)</td>
<td>ms-windows-store://pdp/?PFN= Microsoft.Office.OneNote_8wekyb3d8bbwe
</td>
</tr>
<tr>
<td>Product ID (Windows Phone 7.x/8.x)</td>
<td>ms-windows-store://pdp/?PhoneAppId=ca05b3ab-f157-450c-8c49-a1f127f5e71d</td>
</tr>
<tr>
<td>Product ID (Windows 8.x)</td>
<td>ms-windows-store://pdp/?AppId=f022389f-f3a6-417e-ad23-704fbdf57117
</td>
</tr>
<tr>
<td rowspan="4">Launches the write a review experience for a product.</td>
<td>Store ID <p>(Recommended)</p></td>
<td>ms-windows-store://review/?ProductId=9WZDNCRFHVJL </td>
</tr>
<tr>
<td>Package Family Name (PFN)</td>
<td>ms-windows-store://review/?PFN= Microsoft.Office.OneNote_8wekyb3d8bbwe
</td>
</tr>
<tr>
<td>Product ID (Windows Phone 7.x/8.x)</td>
<td>ms-windows-store://reviewapp/?AppId=ca05b3ab-f157-450c-8c49-a1f127f5e71d </td>
</tr>
<tr>
<td>Product ID (Windows 8.x)</td>
<td>ms-windows-store://review/?AppId=f022389f-f3a6-417e-ad23-704fbdf57117 </td>
</tr>
<tr>
<td>Launches a search for products associated with a file extension. </td>
<td />
<td>ms-windows-store://assoc/?FileExt=pdf
</td>
</tr>
<tr>
<td>Launches a search for products associated with a protocol.</td>
<td />
<td>ms-windows-store://assoc/?Protocol=ms-word </td>
</tr>
<tr>
<td>Launches a search for products associated with one or more tags. Tags should be separated by commas.
</td>
<td />
<td>
<p>ms-windows-store://assoc/?Tags=Photos_Rich_Media_Edit </p>
<p>ms-windows-store://assoc/?Tags=Photos_Rich_Media_Edit, Camera_Capture_App</p>
</td>
</tr>
<tr>
<td>
Launches a search for the specified query. Spaces in the query are allowed.
</td>
<td />
<td>ms-windows-store://search/?query=OneNote </td>
</tr>
<tr>
<td>Launches a search for products in a category.</td>
<td />
<td>
<p>ms-windows-store://browse/?type=Apps&amp;cat=Productivity</p>
<p>ms-windows-store://browse/?type=Apps&amp;cat=Health+%26+fitness </p>
</td>
</tr>
<tr>
<td>Launches a search for products from the specified publisher. Spaces in the name are allowed.
</td>
<td />
<td>ms-windows-store://publisher/?name=Microsoft Corporation
</td>
</tr>
<tr><td>Launches the downloads and updates page.</td>
<td />
<td>ms-windows-store://downloadsandupdates </td>
</tr>
<tr>
<td>Launches the Store settings page.</td>
<td />
<td>ms-windows-store://settings </td>
</tr>
</table>

 

 