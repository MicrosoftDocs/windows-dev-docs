---
author: mcleanbyron
ms.assetid: 2ed21281-f996-402d-a968-d1320a4691df
description: Use the test application ID and ad unit ID values from this article to see how your app renders ads during testing.
title: Test mode values
ms.author: mcleans
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, ads, advertising, testing
---

# Test mode values

When you use an [AdControl](https://msdn.microsoft.com/library/windows/apps/microsoft.advertising.winrt.ui.adcontrol.aspx),  [InterstitialAd](https://msdn.microsoft.com/library/windows/apps/microsoft.advertising.winrt.ui.interstitialad.aspx), or [NativeAd](https://msdn.microsoft.com/library/windows/apps/microsoft.advertising.winrt.ui.nativead.aspx) to display ads in your app, you must specify an ad unit ID and application ID in the **AdUnitId** and **ApplicationId** properties. While you are developing your app, use the test application ID and ad unit ID values from this article to see how your app renders ads during testing.

If you try to use test values in your app after you publish it, your live app not receive ads. To receive ads in your published app, you must update your code to use an application ID and ad unit ID provided by the Windows Dev Center dashboard. For more information, see [Set up ad units in your app](set-up-ad-units-in-your-app.md).
 
Here are the test values to use for the different ad types.

* For interstitial ads:

    <table>
    <colgroup>
    <col width="33%" />
    <col width="33%" />
    <col width="33%" />
    </colgroup>
    <thead>
    <tr class="header">
    <th align="left">Target OS</th>
    <th align="left">AdUnitId</th>
    <th align="left">ApplicationId</th>
    </tr>
    </thead>
    <tbody>
    <tr class="odd">
    <td align="left"><p>UWP (Windows 10)</p></td>
    <td align="left"><p>test</p></td>
    <td align="left"><p>d25517cb-12d4-4699-8bdc-52040c712cab</p></td>
    </tr>
    <tr class="odd">
    <td align="left"><p>Windows 8.x and Windows Phone 8.x</p></td>
    <td align="left"><p>11389925</p></td>
    <td align="left"><p>d25517cb-12d4-4699-8bdc-52040c712cab</p></td>
    </tr>
    </tbody>
    </table>

     
* For banner ads:

    <table>
    <colgroup>
    <col width="33%" />
    <col width="33%" />
    <col width="33%" />
    </colgroup>
    <thead>
    <tr class="header">
    <th align="left">Target OS</th>
    <th align="left">AdUnitId</th>
    <th align="left">ApplicationId</th>
    </tr>
    </thead>
    <tbody>
    <tr class="odd">
    <td align="left"><p>UWP (Windows 10)</p></td>
    <td align="left"><p>test</p></td>
    <td align="left"><p>3f83fe91-d6be-434d-a0ae-7351c5a997f1</p></td>
    </tr>
    <tr class="even">
    <td align="left"><p>Windows 8.x and Windows Phone 8.x</p></td>
    <td align="left"><p>10865270</p></td>
    <td align="left"><p>3f83fe91-d6be-434d-a0ae-7351c5a997f1</p></td>
    </tr>
    </tbody>
    </table>

* For native ads:

    <table>
    <col width="33%" />
    <col width="33%" />
    <col width="33%" />
    </colgroup>
    <thead>
    <tr class="header">
    <th align="left">Target OS</th>
    <th align="left">AdUnitId</th>
    <th align="left">ApplicationId</th>
    </tr>
    </thead>
    <tbody>
    <tr class="odd">
    <td align="left"><p>UWP (Windows 10)</p></td>
    <td align="left"><p>test</p></td>
    <td align="left"><p>d25517cb-12d4-4699-8bdc-52040c712cab</p></td>
    </tbody>
    </table>

> [!IMPORTANT]
> For an **AdControl**, the size of a live ad is defined by the **Width** and **Height** properties. For best results, make sure that the **Width** and **Height** properties in your code are one of the [supported ad sizes for banner ads](supported-ad-sizes-for-banner-ads.md). The **Width** and **Height** properties will not change based on the size of a live ad.


 

 
