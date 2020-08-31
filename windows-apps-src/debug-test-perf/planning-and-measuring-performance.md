---
ms.assetid: A37ADD4A-2187-4767-9C7D-EDE8A90AA215
title: Planning for performance
description: Users expect their apps to remain responsive, to feel natural, and not to drain their battery.
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Planning for performance



Users expect their apps to remain responsive, to feel natural, and not to drain their battery. Technically, performance is a non-functional requirement but treating performance as a feature will help you deliver on your users' expectations. Specifying goals, and measuring, are key factors. Determine what your performance-critical scenarios are; define what good performance mean. Then measure early and often enough throughout the lifecycle of your project to be confident you'll hit your goals.

## Specifying goals

The user experience is a basic way to define good performance. An app's startup time can influence a user's perception of its performance. A user might consider an app launch time of less than one second to be excellent, less than 5 seconds to be good, and greater than 5 seconds to be poor.

Other metrics have a less obvious impact on user experience, for example memory. The chances of an app being terminated while either suspended or inactive rise with the amount of memory used by the active app. It's a general rule that high memory usage degrades the experience for all apps on the system, so having a goal on memory consumption is reasonable. Take into consideration the rough size of your app as perceived by users: small, medium, or large. Expectations around performance will correlate to this perception. For example, you might want a small app that doesn't use a lot of media to consume less than 100MB of memory.

It's better to set an initial goal, and then revise it later, than not to have a goal at all. Your app's performance goals should be specific and measurable and they should fall into three categories: how long it takes users, or the app, to complete tasks (time); the rate and continuity with which the app redraws itself in response to user interaction (fluidity); and how well the app conserves system resources, including battery power (efficiency).

## Time

Think of the acceptable ranges of elapsed time (*interaction classes*) it takes for users to complete their tasks in your app. For each interaction class assign a label, a perceived user sentiment, and ideal and maximum durations. Here are some suggestions.

| Interaction class label | User perception                 | Ideal            | Maximum          | Examples                                                                     |
|-------------------------|---------------------------------|------------------|------------------|------------------------------------------------------------------------------|
| Fast                    | Minimally noticeable delay      | 100 milliseconds | 200 milliseconds | Bring up the app bar; press a button (first response)                        |
| Typical                 | Quick, but not fast             | 300 milliseconds | 500 milliseconds | Resize; semantic zoom                                                        |
| Responsive              | Not quick, but feels responsive | 500 milliseconds | 1 second         | Navigate to a different page; resume the app from a suspended state          |
| Launch                  | Competitive experience          | 1 second         | 3 seconds        | Launch the app for the first time or after it has been previously terminated |
| Continuous              | No longer feels responsive      | 500 milliseconds | 5 seconds        | Download a file from the Internet                                            |
| Captive                 | Long; user could switch away    | 500 milliseconds | 10 seconds       | Install multiple apps from the Store                                         |

 

You can now assign interaction classes to your app's performance scenarios. You can assign the app's point-in-time reference, a portion of the user experience, and an interaction class to each scenario. Here are some suggestions for an example food and dining app.


<!-- DHALE: used HTML table here b/c WDCML src used rowspans -->
<table>
<tr><th>Scenario</th><th>Time point</th><th>User experience</th><th>Interaction class</th></tr>
<tr><td rowspan="3">Navigate to recipe page </td><td>First response</td><td>Page transition animation started</td><td>Fast (100-200 milliseconds)</td></tr>
<tr><td>Responsive</td><td>Ingredients list loaded; no images</td><td>Responsive (500 milliseconds - 1 second)</td></tr>
<tr><td>Visible complete</td><td>All content loaded; images shown</td><td>Continuous (500 milliseconds - 5 seconds)</td></tr>
<tr><td rowspan="2">Search for recipe</td><td>First response</td><td>Search button clicked</td><td>Fast (100 - 200 milliseconds)</td></tr>
<tr><td>Visible complete</td><td>List of local recipe titles shown</td><td>Typical (300 - 500 milliseconds)</td></tr>
</table>

If you're displaying live content then also consider content freshness goals. Is the goal to refresh content every few seconds? Or is refreshing content every few minutes, every few hours, or even once a day an acceptable user experience?

With your goals specified, you are now better able to test, analyze, and optimize your app.

## Fluidity

Specific measurable fluidity goals for your app might include:

-   No screen redraw stops-and-starts (glitches).
-   Animations render at 60 frames per second (FPS).
-   When a user pans/scrolls, the app presents 3-6 pages of content per second.

## Efficiency

Specific measurable efficiency goals for your app might include:

-   For your app's process, CPU percentage is at or below *N* and memory usage in MB is at or below *M* at all times.
-   When the app is inactive, *N* and *M* are zero for your app's process.
-   Your app can be used actively for *X* hours on battery power; when your app is inactive, the device retains its charge for *Y* hours.

## Design your app for performance

You can now use your performance goals to influence your app's design. Using the example food and dining app, after the user navigates to the recipe page, you might choose to [update items incrementally](optimize-gridview-and-listview.md#update-items-incrementally) so that the recipe's name is rendered first, displaying the ingredients is deferred, and displaying images is deferred further. This maintains responsiveness and a fluid UI while panning/scrolling, with the full fidelity rendering taking place after the interaction slows to a pace that allow the UI thread to catch up. Here are some other aspects to consider.

**UI**

-   Maximize parse and load time and memory efficiency for each page of your app's UI (especially the initial page) by [optimizing your XAML markup](optimize-xaml-loading.md). In a nutshell, defer loading UI and code until it's needed.
-   For [**ListView**](/uwp/api/Windows.UI.Xaml.Controls.ListView) and [**GridView**](/uwp/api/Windows.UI.Xaml.Controls.GridView), make all the items the same size and use as many [ListView and GridView optimization techniques](optimize-gridview-and-listview.md) as you can.
-   Declare UI in the form of markup, which the framework can load and re-use in chunks, rather than constructing it imperatively in code.
-   Delay creating UI elements until the user needs them. See the [**x:Load**](../xaml-platform/x-load-attribute.md) attribute.
-   Prefer theme transitions and animations to storyboarded animations. For more info, see [Animations overview](../design/motion/xaml-animation.md). Remember that storyboarded animations require constant updates to the screen, and keep the CPU and graphics pipeline active. To preserve the battery, don't have animations running if the user is not interacting with the app.
-   Images you load should be loaded at a size that is appropriate for the view in which you are presenting it, using the [**GetThumbnailAsync**](/uwp/api/windows.storage.storagefile.getthumbnailasync) method.

**CPU, memory, and power**

-   Schedule lower-priority work to run on lower-priority threads and/or cores. See [Asynchronous programming](../threading-async/asynchronous-programming-universal-windows-platform-apps.md), the [**Dispatcher**](/uwp/api/windows.ui.xaml.window.dispatcher) property, and the [**CoreDispatcher**](/uwp/api/Windows.UI.Core.CoreDispatcher) class.
-   Minimize your app's memory footprint by releasing expensive resources (such as media) on suspend.
-   Minimize your code's working set.
-   Avoid memory leaks by unregistering event handlers and dereferencing UI elements whenever possible.
-   For the sake of the battery, be conservative with how often you poll for data, query a sensor, or schedule work on the CPU when it is idle.

**Data access**

-   If possible, prefetch content. For automatic prefetching, see the [**ContentPrefetcher**](/uwp/api/Windows.Networking.BackgroundTransfer.ContentPrefetcher) class. For manual prefetching, see the [**Windows.ApplicationModel.Background**](/uwp/api/Windows.ApplicationModel.Background) namespace and the [**MaintenanceTrigger**](/uwp/api/Windows.ApplicationModel.Background.MaintenanceTrigger) class.
-   If possible, cache content that's expensive to access. See the [**LocalFolder**](/uwp/api/windows.storage.applicationdata.localfolder) and [**LocalSettings**](/uwp/api/windows.storage.applicationdata.localsettings) properties.
-   For cache misses, show a placeholder UI as quickly as possible that indicates that the app is still loading content. Transition from placeholder to live content in a way that is not jarring to the user. For example, don't change the position of content under the user's finger or mouse pointer as the app loads live content.

**App launch and resume**

-   Defer the app's splash screen, and don't extend the app's splash screen unless necessary. For details, see [Creating a fast and fluid app launch experience](https://blogs.msdn.com/b/windowsappdev/archive/2012/05/21/creating-a-fast-and-fluid-app-launch-experience.aspx) and [Display a splash screen for more time](../launch-resume/create-a-customized-splash-screen.md).
-   Disable animations that occur immediately after the splash screen is dismissed, as these will only lead to a perception of delay in app launch time.

**Adaptive UI, and orientation**

-   Use the [**VisualStateManager**](/uwp/api/Windows.UI.Xaml.VisualStateManager) class.
-   Complete only required work immediately, deferring intensive app work until later—your app has between 200 and 800 milliseconds to complete work before the user sees your app's UI in a cropped state.

With your performance-related designs in place, you can start coding your app.

## Instrument for performance

As you code, add code that logs messages and events at certain points while your app runs. Later, when you're testing your app, you can use profiling tools such as Windows Performance Recorder and Windows Performance Analyzer (both are included in the [Windows Performance Toolkit](/previous-versions/windows/it-pro/windows-8.1-and-8/hh162945(v=win.10))) to create and view a report about your app's performance. In this report, you can look for these messages and events to help you more easily analyze the report's results.

The Universal Windows Platform (UWP) provides logging APIs, backed by [Event Tracing for Windows (ETW)](/windows/desktop/ETW/event-tracing-portal), that together offer a rich event logging and tracing solution. The APIs, which are part of the [**Windows.Foundation.Diagnostics**](/uwp/api/Windows.Foundation.Diagnostics) namespace, include the [**FileLoggingSession**](/uwp/api/Windows.Foundation.Diagnostics.FileLoggingSession), [**LoggingActivity**](/uwp/api/Windows.Foundation.Diagnostics.LoggingActivity), [**LoggingChannel**](/uwp/api/Windows.Foundation.Diagnostics.LoggingChannel), and [**LoggingSession**](/uwp/api/Windows.Foundation.Diagnostics.LoggingSession) classes.

To log a message in the report at a specific point while the app is running, create a **LoggingChannel** object, and then call the object's [**LogMessage**](/uwp/api/windows.foundation.diagnostics.loggingchannel.logmessage) method, like this.

```csharp
// using Windows.Foundation.Diagnostics;
// ...

LoggingChannel myLoggingChannel = new LoggingChannel("MyLoggingChannel");

myLoggingChannel.LogMessage(LoggingLevel.Information, "Here' s my logged message.");

// ...
```

To log start and stop events in the report over a period of time while the app is running, create a **LoggingActivity** object, and then call the object's [**LoggingActivity**](/uwp/api/windows.foundation.diagnostics.loggingactivity.loggingactivity) constructor, like this.

```csharp
// using Windows.Foundation.Diagnostics;
// ...

LoggingActivity myLoggingActivity;

// myLoggingChannel is defined and initialized in the previous code example.
using (myLoggingActivity = new LoggingActivity("MyLoggingActivity"), myLoggingChannel))
{   // After this logging activity starts, a start event is logged.
    
    // Add code here to do something of interest.
    
}   // After this logging activity ends, an end event is logged.

// ...
```

Also see the [Logging sample](https://github.com/Microsoft/Windows-universal-samples).

With your app instrumented, you can test and measure your app's performance.

## Test and measure against performance goals

Part of your performance plan is to define the points during development where you'll measure performance. This serves different purposes depending on whether you're measuring during prototyping, development, or deployment. Measuring performance during the early stages of prototyping can be tremendously valuable, so we recommend that you do so as soon as you have code that does meaningful work. Early measurements give you a good idea of where the important costs are in your app, and inform design decisions. This results in high performing and scaling apps. It's generally costlier to change designs later than earlier. Measuring performance late in the product cycle can result in last-minute hacks and poor performance.

Use these techniques and tools to test how your app stacks up against your original performance goals.

-   Test against a wide variety of hardware configurations including all-in-one and desktop PCs, laptops, ultrabooks, and tablets and other mobile devices.
-   Test against a wide variety of screen sizes. While wider screen sizes can show much more content, bringing in all of that extra content can negatively impact performance.
-   Eliminate as many testing variables as you can.
    -   Turn off background apps on the testing device. To do this, in Windows, select **Settings** from the Start menu &gt; **Personalization** &gt; **Lock screen**. Select each active app and select **None**.
    -   Compile your app to native code by building it in release configuration before deploying it to the testing device.
    -   To ensure that automatic maintenance does not affect the performance of the testing device, trigger it manually and wait for it to complete. In Windows, in the Start menu search for **Security and Maintenance**. In the **Maintenance** area, under **Automatic Maintenance**, select **Start maintenance** and wait for the status to change from **Maintenance in progress**.
    -   Run the app multiple times to help eliminate random testing variables and help ensure consistent measurements.
-   Test for reduced power availability. Your users' device might have significantly less power than your development machine. Windows was designed with low-power devices, such as mobile devices, in mind. Apps that run on the platform should ensure they perform well on these devices. As a heuristic, expect that a low power device runs at about a quarter the speed of a desktop computer, and set your goals accordingly.
-   Use a combination of tools like Microsoft Visual Studio and Windows Performance Analyzer to measure app performance. Visual Studio is designed to provide app-focused analysis, such as source code linking. Windows Performance Analyzer is designed to provide system-focused analysis, such as providing system info, info about touch manipulation events, and info about disk input/output (I/O) and graphics processing unit (GPU) cost. Both tools provide trace capture and export, and can reopen shared and post-mortem traces.
-   Before you submit your app to the Store for certification, be sure to incorporate into your test plans the performance-related test cases as described in the "Performance tests" section of [Windows App Certification Kit tests](windows-app-certification-kit-tests.md) and in the "Performance and stability" section of [UWP app test cases](/previous-versions/windows/apps/dn275879(v=win.10)).

For more info, see these resources and profiling tools.

-   [Windows Performance Analyzer](/previous-versions/windows/it-pro/windows-8.1-and-8/hh448170(v=win.10))
-   [Windows Performance Toolkit](/previous-versions/windows/it-pro/windows-8.1-and-8/hh162945(v=win.10))
-   [Analyze performance using Visual Studio diagnostic tools](/visualstudio/profiling/profiling-tools?view=vs-2015)
-   The //build/ session [XAML Performance](https://channel9.msdn.com/Events/Build/2015/3-698)
-   The //build/ session [New XAML Tools in Visual Studio 2015](https://channel9.msdn.com/Events/Build/2015/2-697)

## Respond to the performance test results

After you analyze your performance test results, determine if any changes are needed, for example:

-   Should you change any of your app design decisions, or optimize your code?
-   Should you add, remove, or change any of the instrumentation in the code?
-   Should you revise any of your performance goals?

If any changes are needed, make them and then go back to instrumenting or testing and repeat.

## Optimizing

Optimize only the performance-critical code paths in your app: those where most time is spent. Profiling will tell you which. Often, there is a trade-off between creating software that follows good design practices and writing code that performs at the highest optimization. It is generally better to prioritize developer productivity and good software design in areas where performance is not a concern.