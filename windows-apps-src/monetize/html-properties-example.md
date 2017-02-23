---
author: mcleanbyron
ms.assetid: 5fa16a27-fdc0-43b2-84cd-8547fd4915de
description: Learn how to assign **AdControl** properties in HTML.
title: AdControl HTML properties example
ms.author: mcleans
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, ads, advertising, AdControl, HTML, properties
---

# AdControl HTML properties example

The following example shows how to assign [AdControl](https://msdn.microsoft.com/library/windows/apps/microsoft.advertising.winrt.ui.adcontrol.aspx)  properties in HTML. The **applicationId** and **adUnitId** are required properties. The other properties and events are optional. If you do not provide values for optional properties, the control will use default values that create an ad consistent with the user experience of the app.

The last five lines are an example of registering functions to **AdControl** events. You can only register functions that you have defined in your JavaScript code.

These values are examples. In your code you will set the values of these functions and properties so they are appropriate for your app.

> [!div class="tabbedCodeSnippets"]
``` html
<div id="myAd" style="position: absolute; top: 50px; left: 0px; width: 300px; height: 250px; z-index: 1"
    data-win-control="MicrosoftNSJS.Advertising.AdControl"
    data-win-options="{applicationId: '3f83fe91-d6be-434d-a0ae-7351c5a997f1',
                       adUnitId: '10865270',
                       isAutoRefreshEnabled: false,
                       onAdRefreshed: myAdRefreshed,
                       onErrorOccurred: myAdError,
                       onEngagedChanged: myAdEngagedChanged,
                       onPointerDown: myPointerDown,
                       onPointerUp: myPointerUp }" />
```

## Related topics

* [AdControl in HTML 5 and JavaScript](adcontrol-in-html-5-and-javascript.md)
* [Advertising samples on GitHub](http://aka.ms/githubads)

 
