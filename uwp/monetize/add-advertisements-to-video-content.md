---
ms.assetid: cc24ba75-a185-4488-b70c-fd4078bc4206
description: Learn how to use the AdScheduler class to show ads in video content in a Universal Windows Platform (UWP) app that was written using JavaScript with HTML.
title: Show ads in video content
ms.date: 02/18/2020
ms.topic: article
keywords: windows 10, uwp, ads, advertising, video, scheduler, javascript
ms.localizationpriority: medium
---
# Show ads in video content

>[!WARNING]
> As of June 1, 2020, the Microsoft Ad Monetization platform for Windows UWP apps will be shut down. [Learn more](https://social.msdn.microsoft.com/Forums/windowsapps/en-US/db8d44cb-1381-47f7-94d3-c6ded3fea36f/microsoft-ad-monetization-platform-shutting-down-june-1st?forum=aiamgr)

This walkthrough shows how to use the **AdScheduler** class to show ads in video content in a Universal Windows Platform (UWP) app that was written using JavaScript with HTML.

> [!NOTE]
> This feature is currently supported only for UWP apps that are written using JavaScript with HTML.

**AdScheduler** works with both progressive and streaming media, and uses IAB standard Video Ad Serving Template (VAST) 2.0/3.0 and VMAP payload formats. By using standards, **AdScheduler** is agnostic to the ad service with which it interacts.

Advertising for video content differs based upon whether the program is under ten minutes (short form), or over ten minutes (long form). Although the latter is more complicated to set up on the service, there is actually no difference in how one writes the client side code. If the **AdScheduler** receives a VAST payload with a single ad instead of a manifest, it is treated as if the manifest called for a single pre-roll ad (one break at time 00:00).

## Prerequisites

* Install the [Microsoft Advertising SDK](https://marketplace.visualstudio.com/items?itemName=AdMediator.MicrosoftAdvertisingSDK) with Visual Studio 2015 or a later release.

* Your project must use the [MediaPlayer](https://github.com/Microsoft/TVHelpers/wiki/MediaPlayer-Overview) control to serve the video content in which the ads will be scheduled. This control is available in the [TVHelpers](https://github.com/Microsoft/TVHelpers) collection of libraries available from Microsoft on GitHub.

  The following example shows how to declare a [MediaPlayer](https://github.com/Microsoft/TVHelpers/wiki/MediaPlayer-Overview) in HTML markup. Typically, this markup belongs in the `<body>` section in the index.html file (or another html file as appropriate for your project).

  ``` html
  <div id="MediaPlayerDiv" data-win-control="TVJS.MediaPlayer">
    <video src="URL to your content">
    </video>
  </div>
  ```

  The following example shows how to establish a [MediaPlayer](https://github.com/Microsoft/TVHelpers/wiki/MediaPlayer-Overview) in JavaScript code.

  :::code language="javascript" source="~/../snippets-windows/windows-uwp/monetize/AdvertisingSamples/AdSchedulerSamples/js/js/main.js" id="Snippet1":::

## How to use the AdScheduler class in your code

1. In Visual Studio, open your project or create a new project.

2. If your project targets **Any CPU**, update your project to use an architecture-specific build output (for example, **x86**). If your project targets **Any CPU**, you will not be able to successfully add a reference to the Microsoft advertising library in the following steps. For more information, see [Reference errors caused by targeting Any CPU in your project](known-issues-for-the-advertising-libraries.md#reference_errors).

3. Add a reference to the **Microsoft Advertising SDK for JavaScript** library to your project.

    1. From the **Solution Explorer** window, right click **References**, and select **Add Reference…**
    2. In **Reference Manager**, expand **Universal Windows**, click **Extensions**, and then select the check box next to **Microsoft Advertising SDK for JavaScript** (Version 10.0).
    3. In **Reference Manager**, click OK.

4.  Add the AdScheduler.js file to your project:

    1. In Visual Studio, click **Project** and **Manage NuGet Packages**.
    2. In the search box, type **Microsoft.StoreServices.VideoAdScheduler** and install the Microsoft.StoreServices.VideoAdScheduler package. The AdScheduler.js file is added to the ../js subdirectory in your project.

5.  Open the index.html file (or other html file as appropriate for your project). In the `<head>` section, after the project’s JavaScript references of default.css and main.js, add the reference to ad.js and adscheduler.js.

    ``` html
    <script src="//Microsoft.Advertising.JavaScript/ad.js"></script>
    <script src="/js/adscheduler.js"></script>
    ```

    > [!NOTE]
    > This line must be placed in the `<head>` section after the include of main.js; otherwise, you will encounter an error when   you build your project.

6.  In the main.js file in your project, add code that creates a new **AdScheduler** object. Pass in the **MediaPlayer** that hosts your video content. The code must be placed so that it runs after [WinJS.UI.processAll](/previous-versions/windows/apps/hh440975(v=win.10)).

    :::code language="javascript" source="~/../snippets-windows/windows-uwp/monetize/AdvertisingSamples/AdSchedulerSamples/js/js/main.js" id="Snippet2":::

7.  Use the **requestSchedule** or **requestScheduleByUrl** methods of the **AdScheduler** object to request an ad schedule from the server and insert it into the **MediaPlayer** timeline, and then play the video media.

    * If you are a Microsoft partner who has received permission to request an ad schedule from the Microsoft ad server, use **requestSchedule** and specify the application ID and ad unit ID that were provided to you by your Microsoft representative.

        This method takes the form of a [Promise](../threading-async/asynchronous-programming-universal-windows-platform-apps.md#asynchronous-patterns-in-uwp-using-javascript), which is an asynchronous construct where two function pointers are passed: a pointer for the **onComplete** function to call when the promise completes successfully and a pointer for the **onError** function to call if an error is encountered. In the **onComplete** function, start playback of your video content. The ad will start playing at the scheduled time. In your **onError** function, handle the error and then start playback of your video. Your video content will play without an ad. The argument of the **onError** function is an object that contains the following members.

        :::code language="javascript" source="~/../snippets-windows/windows-uwp/monetize/AdvertisingSamples/AdSchedulerSamples/js/js/main.js" id="Snippet3":::

    * To request an ad schedule from a non-Microsoft ad server, use **requestScheduleByUrl**, and pass in the server URI. This method also takes the form of a **Promise** that accepts pointers for the **onComplete** and **onError** functions. The ad payload that is returned from the server must conform to the Video Ad Serving Template (VAST) or Video Multiple Ad Playlist (VMAP) payload formats.

        :::code language="javascript" source="~/../snippets-windows/windows-uwp/monetize/AdvertisingSamples/AdSchedulerSamples/js/js/main.js" id="Snippet4":::

    > [!NOTE]
    > You should wait for **requestSchedule** or **requestScheduleByUrl** to return before starting to play the primary video content in the **MediaPlayer**. Starting to play media before **requestSchedule** returns (in the case of a pre-roll advertisement) will result in the pre-roll interrupting the primary video content. You must call **play** even if the function fails, because the **AdScheduler** will tell the **MediaPlayer** to skip the ad(s) and move straight to the content. You may have a different business requirement, such as inserting a built-in ad if an ad can't be successfully fetched remotely.

8.  During playback, you can handle additional events that let your app track progress and/or errors which may occur after the initial ad matching process. The following code shows some of these events, including **onPodStart**, **onPodEnd**, **onPodCountdown**, **onAdProgress**, **onAllComplete**, and **onErrorOccurred**.

    :::code language="javascript" source="~/../snippets-windows/windows-uwp/monetize/AdvertisingSamples/AdSchedulerSamples/js/js/main.js" id="Snippet5":::

## AdScheduler members

This section provides some details about the members of the **AdScheduler** object. For more information about these members, see the comments and definitions in the AdScheduler.js file in your project.

### requestSchedule

This method requests an ad schedule from the Microsoft ad server and inserts it into the timeline of the **MediaPlayer** that was passed to the **AdScheduler** constructor.

The optional third parameter (*adTags*) is a JSON collection of name/value pairs that can be used for apps that have advanced targeting. For example, an app that plays a variety of auto-related videos might supplement the ad unit ID with the make and model of the car being shown. This parameter is intended to be used only by partners who receive approval from Microsoft to use ad tags.

The following items should be noted when referring to *adTags*:

* This parameter is a very rarely used option. The publisher must work closely with Microsoft before using adTags.
* Both the names and the values must be predetermined on the ad service. Ad tags are not open ended search terms or keywords.
* The maximum supported number of tags is 10.
* Tag names are restricted to 16 characters.
* Tag values have a maximum of 128 characters.

### requestScheduleByUri

This method requests an ad schedule from the non-Microsoft ad server specified in the URI and inserts it into the timeline of the **MediaPlayer** that was passed to the **AdScheduler** constructor. The ad payload that is returned by the ad server must conform to the Video Ad Serving Template (VAST) or Video Multiple Ad Playlist (VMAP) payload formats.

### mediaTimeout

This property gets or sets the number of milliseconds that the media must be playable. A value of 0 informs the system to never timeout. The default is 30000 ms (30 seconds).

### playSkippedMedia

This property gets or sets a **Boolean** value that indicates whether scheduled media will play if user skips ahead to a point past a scheduled start time.

The ad client and media player will enforce rules in terms what happens to advertisements during fast forwarding and rewinding of the primary video content. In most cases, app developers do not allow advertisements to be entirely skipped over but do want to provide a reasonable experience for the user. The following two options fall within the needs of most developers:

1. Allow end-users to skip over advertisement pods at will.
2. Allow users to skip over advertisement pods but play the most recent pod when playback resumes.

The **playSkippedMedia** property has the following conditions:

* Advertisements cannot be skipped or fast forwarded once the advertisement begins.
* All advertisements in an advertisement pod will play once the pod has started.
* Once played, an advertisement will not play again during the primary content (movie, episode, etc.); advertisement markers will be marked as played or removed.
* Pre-rollout advertisements cannot be skipped.

When resuming content that contains advertising, set **playSkippedMedia** to **false** to skip pre-rolls and prevent the most recent ad break from playing. Then, after the content starts, set **playSkippedMedia** to **true** to ensure that users cannot fast-forward through subsequent ads.

> [!NOTE]
> A pod is a group of ads that play in a sequence, such as a group of ads that play during a commercial break. For more details, see the IAB Digital Video Ad Serving Template (VAST) specification.

### requestTimeout

This property gets or sets the number of milliseconds to wait for an ad request response before timing out. A value of 0 informs the system to never timeout. The default is 30000 ms (30 seconds).

### schedule

This property gets the schedule data that was retrieved from the ad server. This object includes the full hierarchy of data that corresponds to the structure of the Video Ad Serving Template (VAST) or Video Multiple Ad Playlist (VMAP) payload.

### onAdProgress  

This event is raised when ad playback reaches quartile checkpoints. The second parameter of the event handler (*eventInfo*) is a JSON object with the following members:

* **progress**: The ad playback status (one of the **MediaProgress** enum values defined in AdScheduler.js).
* **clip**: The video clip that is being played. This object is not intended to be used in your code.
* **adPackage**: An object that represents the part of the ad payload that corresponds to the ad that is playing. This object is not intended to be used in your code.

### onAllComplete  

This event is raised when the main content reaches the end and any scheduled post-roll ads are also ended.

### onErrorOccurred  

This event is raised when the **AdScheduler** encounters an error. For more information about the error code values, see [ErrorCode](/uwp/api/microsoft.advertising.errorcode).

### onPodCountdown

This event is raised when an ad is playing and indicates how much time remains in the current pod. The second parameter of the event handler (*eventData*) is a JSON object with the following members:

* **remainingAdTime**: The number of seconds left for the current ad.
* **remainingPodTime**: The number of seconds left for the current pod.

> [!NOTE]
> A pod is a group of ads that play in a sequence, such as a group of ads that play during a commercial break. For more details, see the IAB Digital Video Ad Serving Template (VAST) specification.

### onPodEnd  

This event is raised when an ad pod ends. The second parameter of the event handler (*eventData*) is a JSON object with the following members:

* **startTime**: The pod's start time, in seconds.
* **pod**: An object that represents the pod. This object is not intended to be used in your code.

### onPodStart

This event is raised when an ad pod starts. The second parameter of the event handler (*eventData*) is a JSON object with the following members:

* **startTime**: The pod's start time, in seconds.
* **pod**: An object that represents the pod. This object is not intended to be used in your code.
