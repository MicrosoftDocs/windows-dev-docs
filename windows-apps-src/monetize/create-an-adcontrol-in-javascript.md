---
author: mcleanbyron
ms.assetid: 48a1ef86-8514-4af8-9c93-81e869d36de7
description: Learn how to programmatically create an **AdControl** using JavaScript.
title: Create an AdControl in Javascript
ms.author: mcleans
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, ads, advertising, AdControl, javascript
---

# Create an AdControl in Javascript




The examples in this article shows how to programmatically create an [AdControl](https://msdn.microsoft.com/library/windows/apps/microsoft.advertising.winrt.ui.adcontrol.aspx) using JavaScript. This article assumes that you have already added the necessary references to your project to use an **AdControl**. For more information, including a detailed walkthrough for creating and initializing an **AdControl** in HTML markup instead of JavaScript, see [AdControl in HTML 5 and Javascript](adcontrol-in-html-5-and-javascript.md).

## HTML div for an AdControl

An **AdControl** needs to have a **div** on the html page that will show the ad. The code below provides an example of such a **div**.

> [!div class="tabbedCodeSnippets"]
``` html
<div id="myAd" style="position: absolute; top: 50px; left: 0px; width: 300px; height: 250px; z-index: 1"
    data-win-control="MicrosoftNSJS.Advertising.AdControl">
</div>
```

## JavaScript for creating an AdControl

The following example assumes that you are using an existing **div** in your HTML with the ID **myAd**.

Instantiate the **AdControl** in the **app.onactivated** function.

> [!div class="tabbedCodeSnippets"]
[!code-javascript[AdControl](./code/AdvertisingSamples/AdControlSamples/js/main.js#DeclareAdControl)]

This example assumes that you have already declared event handler methods named **myAdError**, **myAdRefreshed**, and **myAdEngagedChanged**.

>**Note**&nbsp;&nbsp;The *applicationId* and *adUnitId* values shown in this example are [test mode values](test-mode-values.md). You must [replace these values with live values](set-up-ad-units-in-your-app.md) from Windows Dev Center before submitting your app for submission.

If you use this code and do not see ads, you can try inserting an attribute of **position:relative** in the **div** that contains the **AdControl**. This will override the default setting of the **IFrame**. Ads will be displayed correctly, unless they are not being shown due to the value of this attribute. Note that new ad units may not be available for up to 30 minutes.

## Related topics

* [Advertising samples on GitHub](http://aka.ms/githubads)

 

 
