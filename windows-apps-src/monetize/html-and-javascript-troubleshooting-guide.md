---
ms.assetid: 7a61c328-77be-4614-b117-a32a592c9efe
description: Read about solutions to common development issues with the Microsoft advertising libraries in JavaScript/HTML apps.
title: HTML and JavaScript troubleshooting guide
ms.date: 02/18/2020
ms.topic: article
keywords: windows 10, uwp, ads, advertising, AdControl, troubleshooting, HTML, javascript
ms.localizationpriority: medium
---
# HTML and JavaScript troubleshooting guide

>[!WARNING]
> As of June 1, 2020, the Microsoft Ad Monetization platform for Windows UWP apps will be shut down. [Learn more](https://social.msdn.microsoft.com/Forums/windowsapps/en-US/db8d44cb-1381-47f7-94d3-c6ded3fea36f/microsoft-ad-monetization-platform-shutting-down-june-1st?forum=aiamgr)

This topic contains solutions to common development issues with the Microsoft advertising libraries in JavaScript/HTML apps.

* [HTML](#html)
  * [AdControl not appearing](#html-notappearing)
  * [Black box blinks and disappears](#html-blackboxblinksdisappears)
  * [Ads not refreshing](#html-adsnotrefreshing)

* [JavaScript](#js)
  * [AdControl not appearing](#js-adcontrolnotappearing)
  * [Black box blinks and disappears](#js-blackboxblinksdisappears)
  * [Ads not refreshing](#js-adsnotrefreshing)

## HTML

<span id="html-notappearing"/>

### AdControl not appearing

1.  Ensure that the **Internet (Client)** capability is selected in Package.appxmanifest.

2.  Ensure the JavaScript reference is present. Without the ad.js reference in the &lt;head&gt; section (after the default.js reference) the **AdControl** will be unable to display and an error will occur during build.

    > [!div class="tabbedCodeSnippets"]
    ``` html
    <head>
        ...
        <script src="//Microsoft.Advertising.JavaScript/ad.js"></script>
        ...
    </head>
    ```

3.  Check the application ID and ad unit ID. These IDs must match the application ID and ad unit ID that you obtained in Partner Center. For more information, see [Set up ad units in your app](set-up-ad-units-in-your-app.md#live-ad-units).

    > [!div class="tabbedCodeSnippets"]
    ``` html
    <div id="myAd" style="position: absolute; top: 50px; left: 0px;
                          width: 250px; height: 250px; z-index: 1"
         data-win-control="MicrosoftNSJS.Advertising.AdControl"
         data-win-options="{applicationId: 'ApplicationID',
                            adUnitId: 'AdUnitID'}">
    </div>
    ```

4.  Check the **height** and **width** properties. These must be set to one of the [supported ad sizes for banner ads](supported-ad-sizes-for-banner-ads.md).

    > [!div class="tabbedCodeSnippets"]
    ``` html
    <div id="myAd" style="position: absolute; top: 50px; left: 0px;
                          width: 250px; height: 250px; z-index: 1"
         data-win-control="MicrosoftNSJS.Advertising.AdControl"
         data-win-options="{applicationId: 'ApplicationID',
                            adUnitId: 'AdUnitID'}">
    </div>
    ```

5.  Check the element positioning. The [AdControl](/uwp/api/microsoft.advertising.winrt.ui.adcontrol) must be inside the viewable area.

6.  Check the **visibility** property. This property must not be set to collapsed or hidden. This property can be set inline (as shown below) or in an external style sheet.

    > [!div class="tabbedCodeSnippets"]
    ``` html
    <div id="myAd" style="visibility: visible; position: absolute; top: 1025px;
                          left: 500px; width: 250px; height: 250px; z-index: 1"
         data-win-control="MicrosoftNSJS.Advertising.AdControl"
         data-win-options="{applicationId: 'ApplicationID',
                            adUnitId: 'AdUnitID'}">
    </div>
    ```

7.  Check the **position** property. The position property must be set to an appropriate value depending on the element’s other properties (for example, margins in parent element and z-index). This property can be set inline (as shown below) or in an external style sheet.

    > [!div class="tabbedCodeSnippets"]
    ``` html
    <div id="myAd" style="visibility: visible; position: absolute; top: 1025px;
                          left: 500px; width: 250px; height: 250px; z-index: 1"
         data-win-control="MicrosoftNSJS.Advertising.AdControl"
         data-win-options="{applicationId: 'ApplicationID',
                            adUnitId: 'AdUnitID'}">
    </div>
    ```

8.  Check the **z-index** property. The **z-index** property must be set high enough so the **AdControl** always appears on top of other elements. This property can be set inline (as shown below) or in an external style sheet.

    > [!div class="tabbedCodeSnippets"]
    ``` html
    <div id="myAd" style="visibility: visible; position: absolute; top: 1025px;
                          left: 500px; width: 250px; height: 250px; z-index: 1"
         data-win-control="MicrosoftNSJS.Advertising.AdControl"
         data-win-options="{applicationId: 'ApplicationID',
                            adUnitId: 'AdUnitID'}">
    </div>
    ```

9.  Check external style sheets. If properties are set on the **AdControl** element through an external style sheet, ensure all of the above properties are correctly set.

    > [!div class="tabbedCodeSnippets"]
    ``` html
    <div id="myAd" style="visibility: visible; position: absolute; top: 1025px;
                          left: 500px; width: 250px; height: 250px; z-index: 1"
         data-win-control="MicrosoftNSJS.Advertising.AdControl"
         data-win-options="{applicationId: 'ApplicationID',
                            adUnitId: 'AdUnitID'}">
    </div>
    ```

10. Check the parent of the **AdControl**. If the **AdControl** resides in a parent element, the parent must be active and visible.

    > [!div class="tabbedCodeSnippets"]
    ``` html
    <div style="position: absolute; width: 500px; height: 500px;">
        <div id="myAd" style="position: relative; top: 0px; left: 100px;
                              width: 250px; height: 250px; z-index: 1"
             data-win-control="MicrosoftNSJS.Advertising.AdControl"
             data-win-options="{applicationId: 'ApplicationID',
                                adUnitId: 'AdUnitID'}">
        </div>
    </div>
    ```

11. Ensure the **AdControl** is not hidden from the viewport. The **AdControl** must be visible for ads to display properly.

12. Live values for [ApplicationId](/uwp/api/microsoft.advertising.winrt.ui.adcontrol.applicationid) and [AdUnitId](/uwp/api/microsoft.advertising.winrt.ui.adcontrol.adunitid) should not be tested in the emulator. To ensure the **AdControl** is functioning as expected, use the [test values](set-up-ad-units-in-your-app.md#test-ad-units) for both **ApplicationId** and **AdUnitId**.

<span id="html-blackboxblinksdisappears"/>

### Black box blinks and disappears

1.  Double-check all steps in the previous [AdControl not appearing](#html-notappearing) section.

2.  Handle the **onErrorOccurred** event, and use the message that is passed to the event handler to determine whether an error occurred and what type of error was thrown. More details can be found in [Error handling in JavaScript walkthrough](error-handling-in-javascript-walkthrough.md).

    > [!div class="tabbedCodeSnippets"]
    ``` html
    <div id="myAd" style="position: absolute; top: 0px; left: 0px;
                          width: 728px; height: 90px; z-index: 1"
         data-win-control="MicrosoftNSJS.Advertising.AdControl"
         data-win-options="{applicationId: 'ApplicationID',
                            adUnitId: 'AdUnitID',
                            onErrorOccurred: errorLogger}">
    </div>
    <div style="position:absolute; width:100%; height:130px; top:300px; left:0px">
        <b>Ad Events</b><br />
        <div id="adEvents" style="width:100%; height:110px; overflow:auto"></div>
    </div>
    ```

    The most common error that causes a black box is “No ad available.” This error means there is no ad available to return from the request.

3.  The **AdControl** is behaving normally. By default, the **AdControl** will collapse when it cannot display an ad. If other elements are children of the same parent they may move to fill the gap of the collapsed **AdControl** and expand when the next request is made.

<span id="html-adsnotrefreshing"/>

### Ads not refreshing

1.  Check the **isAutoRefreshEnabled** property. By default, this optional property is set to true. When set to false, the **refresh** method must be used to retrieve another ad.

    > [!div class="tabbedCodeSnippets"]
    ``` html
    <div id="myAd" style="position: absolute; top: 0px; left: 0px;
                          width: 250px; height: 250px; z-index: 1"
         data-win-control="MicrosoftNSJS.Advertising.AdControl"
         data-win-options="{ applicationId: 'ApplicationID',
                            adUnitId: 'AdUnitID',
                            onErrorOccurred: errorLogger,
                            isAutoRefreshEnabled: true}">
    </div>
    ```

2.  Check calls to the **refresh** method. When using automatic refresh, **refresh** cannot be used to retrieve another ad. When using manual refresh, **refresh** should be called only after a minimum of 30 to 60 seconds depending on the device’s current data connection.

    This example demonstrates how to use the **refresh** method. The following HTML code shows an example of how to instantiate the **AdControl** with **isAutoRefreshEnabled** set to false.

    > [!div class="tabbedCodeSnippets"]
    ``` html
    <div id="myAd" style="position: absolute; top: 0px; left: 0px;
                          width: 250px; height: 250px; z-index: 1"
         data-win-control="MicrosoftNSJS.Advertising.AdControl"
         data-win-options="{ applicationId: 'ApplicationID',
                            adUnitId: 'AdUnitID',
                            onErrorOccurred: errorLogger,
                            isAutoRefreshEnabled: false}">
    </div>
    ```

    Theis example demonstrates how to use the **refresh** function.

    > [!div class="tabbedCodeSnippets"]
    ``` javascript
    args.setPromise(WinJS.UI.processAll()
        .then(function (args) {
            window.setInterval(function()
            {
                document.getElementById("myAd").winControl.refresh();
            }, 60000)
        })
    );
    ```

3.  The **AdControl** is behaving normally. Sometimes the same ad will appear more than once in a row giving the appearance that ads are not refreshing.

<span id="js"/>

## JavaScript

<span id="js-adcontrolnotappearing"/>

### AdControl not appearing

1.  Ensure that the **Internet (Client)** capability is selected in Package.appxmanifest.

2.  Ensure the **AdControl** is instantiated. If the **AdControl** is not instantiated. it will not be available.

    The following snippets show an example of instantiating the **AdControl**. This HTML code shows an example of setting up the UI for the **AdControl**

    > [!div class="tabbedCodeSnippets"]
    ``` html
    <div id="myAd" style="position: absolute; top: 0px; left: 0px;
                          width: 250px; height: 250px; z-index: 1"
         data-win-control="MicrosoftNSJS.Advertising.AdControl">
    </div>
    ```

    The following JavaScript code shows an example of instantiating the **AdControl**

    > [!div class="tabbedCodeSnippets"]
    ``` javascript
    app.onactivated = function (args) {
        if (args.detail.kind === activation.ActivationKind.launch) {
            if (args.detail.previousExecutionState !==
                    activation.ApplicationExecutionState.terminated)
            {
                var adDiv = document.getElementById("myAd");
                var myAdControl = new MicrosoftNSJS.Advertising.AdControl(adDiv,
                {
                    applicationId: "{ApplicationID}",
                    adUnitId: "{AdUnitID}"
                 });                
                 myAdControl.onErrorOccurred = myAdError;
            } else {
                ...
            }
        }
    }
    ```

3.  Check the parent element. The parent **&lt;div&gt;** must be correctly assigned, active, and visible.

    > [!div class="tabbedCodeSnippets"]
    ``` javascript
    var adDiv = document.getElementById("myAd");
    var myAdControl = new MicrosoftNSJS.Advertising.AdControl(adDiv, {
        applicationId: "{ApplicationID}",
        adUnitId: "{AdUnitID}"
    });  
    ```

4.  Check the application ID and ad unit ID. These IDs must match the application ID and ad unit ID that you obtained in Partner Center. For more information, see [Set up ad units in your app](set-up-ad-units-in-your-app.md#live-ad-units).

    > [!div class="tabbedCodeSnippets"]
    ``` javascript
    var myAdControl = new MicrosoftNSJS.Advertising.AdControl(adDiv, {
        applicationId: "{ApplicationID}",
        adUnitId: "{AdUnitID}"
    });  
    ```

5.  Check the parent element of the **AdControl**. The parent must be active and visible.

6.  Live values for **ApplicationId** and **AdUnitId** should not be tested in the emulator. To ensure the **AdControl** is functioning as expected, use the [test values](set-up-ad-units-in-your-app.md#test-ad-units) for both **ApplicationId** and **AdUnitId**.

<span id="js-blackboxblinksdisappears"/>

### Black box blinks and disappears

1.  Double-check all steps in the [AdControl not appearing](#js-adcontrolnotappearing) section.

2.  Handle the **onErrorOccurred** event, and use the message that is passed to the event handler to determine whether an error occurred and what type of error was thrown. More details can be found in [Error handling in JavaScript walkthrough](error-handling-in-javascript-walkthrough.md).

    This example demonstrates how to implement an error handler that reports error messages. This snippet of HTML code provides an example of how to set up the UI to display error messages.

    > [!div class="tabbedCodeSnippets"]
    ``` html
    <div style="position:absolute; width:100%; height:130px; top:300px">
        <b>Ad Events</b><br />
        <div id="adEvents" style="width:100%; height:110px; overflow:auto"></div>
    </div>
    ```

    This example demonstrates how to instantiate the **AdControl**. This function would be inserted in the app.onactivated file.

    > [!div class="tabbedCodeSnippets"]
    ``` javascript
    var myAdControl = new MicrosoftNSJS.Advertising.AdControl(adDiv,
    {
        applicationId: "{ApplicationID}",
        adUnitId: "{AdUnitID}"
    });                
    myAdControl.onErrorOccurred = myAdError;
    ```

    This example demonstrates how to report errors. This function would be inserted below the self-running function in the default.js file.

    > [!div class="tabbedCodeSnippets"]
    ``` javascript
    WinJS.Utilities.markSupportedForProcessing
    (
        window.errorLogger = function (sender, evt)
        {
            adEvents.innerHTML = (new Date()).toLocaleTimeString() + ": " +
            sender.element.id + " error: " + evt.errorMessage + " error code: " +
            evt.errorCode + "<br>" + adEvents.innerHTML;
        }
    );
    ```

    The most common error that causes a black box is “No ad available.” This error means there is no ad available to return from the request.

3.  The **AdControl** is behaving normally. Sometimes the same ad will appear more than once in a row giving the appearance that ads are not refreshing.

<span id="js-adsnotrefreshing"/>

### Ads not refreshing

1.  Check whether the [IsAutoRefreshEnabled](/uwp/api/microsoft.advertising.winrt.ui.adcontrol.isautorefreshenabled.aspx) property of your **AdControl** is set to false. By default, this optional property is set to **true**. When set to **false**, the **Refresh** method must be used to retrieve another ad.

2.  Check calls to the [Refresh](/uwp/api/microsoft.advertising.winrt.ui.adcontrol.refresh.aspx) method. When using automatic refresh (**IsAutoRefreshEnabled** is **true**), **Refresh** cannot be used to retrieve another ad. When using manual refresh (**IsAutoRefreshEnabled** is **false**), **Refresh** should be called only after a minimum of 30 to 60 seconds depending on the device’s current data connection.

    This example demonstrates how to create the **div** for the **AdControl**.

    > [!div class="tabbedCodeSnippets"]
    ``` html
    <div id="myAd" style="position: absolute; top: 0px; left: 0px;
                          width: 250px; height: 250px; z-index: 1"
         data-win-control="MicrosoftNSJS.Advertising.AdControl">
    </div>
    ```

    This example shows how to use the **Refresh** function

    > [!div class="tabbedCodeSnippets"]
    ``` javascript
    var myAdControl = new MicrosoftNSJS.Advertising.AdControl(adDiv,
    {
      applicationId: "{ApplicationID}",
      adUnitId: "{AdUnitID}",
      isAutoRefreshEnabled: false
    });
    ...
    args.setPromise(WinJS.UI.processAll()
        .then(function (args) {
            window.setInterval(function()
            {
                document.getElementById("myAd").winControl.refresh();
            }, 60000)
        })
    );
    ```

3.  The **AdControl** is behaving normally. Sometimes the same ad will appear more than once in a row giving the appearance that ads are not refreshing.

 

 