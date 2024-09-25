---
description: Learn how to catch and handle errors from an AdControl in a JavaScript and HTML5 app by following this walkthrough.
title: Error handling in JavaScript walkthrough
ms.date: 02/18/2020
ms.topic: article
---

# Error handling in JavaScript walkthrough

>[!WARNING]
> As of June 1, 2020, the Microsoft Ad Monetization platform for Windows UWP apps will be shut down. [Learn more](https://social.msdn.microsoft.com/Forums/windowsapps/en-US/db8d44cb-1381-47f7-94d3-c6ded3fea36f/microsoft-ad-monetization-platform-shutting-down-june-1st?forum=aiamgr)

This walkthrough demonstrates how to catch ad-related errors in your JavaScript app. This walkthrough uses an [AdControl](/uwp/api/microsoft.advertising.winrt.ui.adcontrol) to display a banner ad, but the general concepts in it also apply to interstitial ads and native ads.

These examples assume that you have a JavaScript app that contains an **AdControl**. For step-by-step instructions that demonstrate how to add an **AdControl** to your app, see [AdControl in HTML 5 and JavaScript](adcontrol-in-html-5-and-javascript.md). For a complete sample project that demonstrates how to add banner ads to a JavaScript/HTML app, see the [advertising samples on GitHub](https://github.com/microsoft/Windows-universal-samples/tree/b1cb20f191d3fd99ce89df50c5b7d1a6e2382c01/archived/Advertising).

1.  In the default.html file, add a value for the **onErrorOccurred** event where you define the **data-win-options** in the **div** for the **AdControl**. Find the following code in the default.html file.
    ``` HTML
    <div id="myAd" style="position: absolute; top: 53px; left: 0px; width: 300px; height: 250px; z-index: 1"
      data-win-control="MicrosoftNSJS.Advertising.AdControl"
      data-win-options="{applicationId: '3f83fe91-d6be-434d-a0ae-7351c5a997f1', adUnitId: 'test'}">
    </div>
    ```
    Following the **adUnitId** attribute, add the value for the **onErrorOccurred** event.
    ``` HTML
    <div id="myAd" style="position: absolute; top: 53px; left: 0px; width: 300px; height: 250px; z-index: 1"
      data-win-control="MicrosoftNSJS.Advertising.AdControl"
      data-win-options="{applicationId: '3f83fe91-d6be-434d-a0ae-7351c5a997f1', adUnitId: 'test', onErrorOccurred: errorLogger}">
  </div>
  ```

2.  Create a **div** that will display text so you can see the messages being generated. To do this, add the following code after the **div** for **myAd**.
    ``` HTML
    <div style="position:absolute; width:100%; height:130px; top:300px; left:0px">
        <b>Ad Events</b><br />
        <div id="adEvents" style="width:100%; height:110px; overflow:auto"></div>
    </div>
    ```

3.  Create an **AdControl** that will trigger an error event. There can only be one application id for all **AdControl** objects in an app. So creating an additional one with a different application id will trigger an error at runtime. To do this, after the previous **div** sections you have added, add the following code to the body of the default.html page.
    ``` HTML
    <!-- Because only one applicationId can be used, the following ad control will fire an error event. -->
    <div id="liveAd" style="position: absolute; top:500px; left:0px; width:480px; height:80px"
      data-win-control="MicrosoftNSJS.Advertising.AdControl"
      data-win-options="{applicationId: '00000000-0000-0000-0000-000000000000', adUnitId: 'test', onErrorOccurred: errorLogger }" >
    </div>
    ```

4.  In the project’s default.js file, after the default initialization function, you will add the event handler for **errorLogger**. Scroll to the end of the file and after the last semi-colon is where you will put the following code.
    ``` javascript
    WinJS.Utilities.markSupportedForProcessing(
    window.errorLogger = function (sender, evt) {
        adEvents.innerHTML = (new Date()).toLocaleTimeString() + ": " +
        sender.element.id + " error: " + evt.errorMessage + " error code: " +
        evt.errorCode + "<br>" + adEvents.innerHTML;
        console.log("errorhandler hit. \n");
    });
    ```

5.  Build and run the file. You will see the original ad from the sample app you built previously and text under that ad describing the error. You will not see the ad with the id of **liveAd**.

## Related topics

* [Advertising samples on GitHub](https://github.com/microsoft/Windows-universal-samples/tree/b1cb20f191d3fd99ce89df50c5b7d1a6e2382c01/archived/Advertising)
