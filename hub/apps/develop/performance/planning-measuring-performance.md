---
title: Plan and measure app performance
description: Set performance goals, measure your app's actual performance, and optimize based on profiling data in Windows App SDK applications.
author: GrantMeStrength
ms.author: jken
ms.topic: concept-article
ms.date: 07/08/2026
---

# Plan and measure app performance

Users expect their apps to remain responsive, to feel natural, and not to drain their battery. Technically, performance is a non-functional requirement, but treating performance as a feature helps you deliver on your users' expectations. Specify goals and measure results — these are key factors. Determine your performance-critical scenarios, define what good performance means, and then measure early and often throughout your project's lifecycle to be confident you'll hit your goals.

## Specify goals

The user experience is a basic way to define good performance. An app's startup time can influence a user's perception of its performance. A user might consider an app launch time of less than one second to be excellent, less than five seconds to be good, and greater than five seconds to be poor.

Other metrics have less obvious impact on user experience, such as memory. The chances of an app being terminated while suspended or inactive rise with the amount of memory the active app uses. High memory usage degrades the experience for all apps on the system, so having a memory consumption goal is reasonable.

Set initial goals that are specific and measurable. They should fall into three categories:

- **Time** — how long it takes users or the app to complete tasks
- **Fluidity** — the rate and continuity with which the app redraws itself in response to user interaction
- **Efficiency** — how well the app conserves system resources, including battery power

## Time

Think of acceptable ranges of elapsed time (*interaction classes*) for users to complete their tasks.

| Interaction class | User perception | Ideal | Maximum | Examples |
|---|---|---|---|---|
| Fast | Minimally noticeable delay | 100 ms | 200 ms | Bring up the app bar; press a button (first response) |
| Typical | Quick, but not fast | 300 ms | 500 ms | Resize; semantic zoom |
| Responsive | Not quick, but feels responsive | 500 ms | 1 second | Navigate to a different page; resume the app |
| Launch | Competitive experience | 1 second | 3 seconds | Launch the app for the first time |
| Continuous | No longer feels responsive | 500 ms | 5 seconds | Download a file from the Internet |
| Captive | Long; user could switch away | 500 ms | 10 seconds | Install multiple apps from the Store |

Assign interaction classes to your app's performance scenarios. For each scenario, assign the app's point-in-time reference, a portion of the user experience, and an interaction class.

## Fluidity

Specific measurable fluidity goals for your app might include:

- No screen redraw stops-and-starts (glitches)
- Animations render at 60 frames per second (FPS)
- When a user pans or scrolls, the app presents 3–6 pages of content per second

## Efficiency

Specific measurable efficiency goals for your app might include:

- Your app's CPU percentage is at or below a target value and memory usage in MB is at or below a target at all times
- When the app is inactive, CPU and memory use are minimal
- Your app can be used actively for a target number of hours on battery power

## Design your app for performance

Use your performance goals to influence your app's design. Consider these aspects:

**UI**

- Maximize parse and load time and memory efficiency for each page by [optimizing your XAML markup](optimize-xaml-loading.md). Defer loading UI and code until it's needed.
- For `ListView` and `GridView`, make all items the same size and use as many [optimization techniques](optimize-gridview-and-listview.md) as you can.
- Declare UI in markup rather than constructing it imperatively in code.
- Delay creating UI elements until the user needs them using the [x:Load](/windows/uwp/xaml-platform/x-load-attribute) attribute.
- Prefer theme transitions and animations to storyboarded animations. Storyboarded animations require constant updates to the screen and keep the CPU and graphics pipeline active.
- Load images at a size appropriate for the view you present them in.

**CPU, memory, and power**

- Schedule lower-priority work on lower-priority threads. See [Asynchronous programming](/dotnet/csharp/asynchronous-programming/) and the [DispatcherQueue](/windows/windows-app-sdk/api/winrt/microsoft.ui.dispatching.dispatcherqueue) class.
- Minimize your app's memory footprint by releasing expensive resources (such as media) when they're not needed.
- Avoid memory leaks by unregistering event handlers and dereferencing UI elements whenever possible.
- For battery efficiency, be conservative with how often you poll for data, query a sensor, or schedule work on the CPU when it's idle.

**Data access**

- If possible, prefetch content.
- Cache content that's expensive to access.
- For cache misses, show a placeholder UI as quickly as possible that indicates the app is still loading content.

## Instrument for performance

As you code, add code that logs messages and events at certain points while your app runs. Later, use profiling tools such as Windows Performance Recorder and Windows Performance Analyzer (both included in the [Windows Performance Toolkit](/windows-hardware/test/wpt/)) to create and view a report about your app's performance.

Windows provides logging APIs backed by [Event Tracing for Windows (ETW)](/windows/win32/etw/event-tracing-portal) that offer a rich event logging and tracing solution. The APIs in the [Windows.Foundation.Diagnostics](/uwp/api/windows.foundation.diagnostics) namespace include the `FileLoggingSession`, `LoggingActivity`, `LoggingChannel`, and `LoggingSession` classes.

```csharp
// using Windows.Foundation.Diagnostics;

LoggingChannel myLoggingChannel = new LoggingChannel("MyLoggingChannel");
myLoggingChannel.LogMessage("Here's my logged message.", LoggingLevel.Information);
```

To log start and stop events over a period of time:

```csharp
LoggingChannel myLoggingChannel = new LoggingChannel("MyLoggingChannel");
LoggingActivity myLoggingActivity;

using (myLoggingActivity = new LoggingActivity("MyLoggingActivity", myLoggingChannel))
{
    // A start event is logged when the activity begins.
    // Add code here to do something of interest.
}
// An end event is logged when the activity ends.
```

## Test and measure against performance goals

Use these techniques and tools to test how your app stacks up against your performance goals:

- Test against a wide variety of hardware configurations, including desktops, laptops, ultrabooks, and tablets.
- Test against a wide variety of screen sizes. Wider screens show more content, which can negatively impact performance.
- Eliminate as many testing variables as you can:
  - Turn off background apps on the testing device.
  - Build your app in the Release configuration before deploying it to the testing device.
  - Run the app multiple times to help eliminate random testing variables and ensure consistent measurements.
- Test for reduced power availability. Users' devices might have significantly less power than your development machine.
- Use a combination of tools like Visual Studio diagnostic tools and Windows Performance Analyzer to measure app performance.

## Respond to performance test results

After you analyze your performance test results, determine if any changes are needed:

- Should you change your app design decisions or optimize your code?
- Should you add, remove, or change instrumentation in the code?
- Should you revise your performance goals?

If changes are needed, make them and return to instrumenting or testing.

## Optimize

Optimize only the performance-critical code paths in your app — those where the most time is spent. Profiling tells you which areas these are. Often, there is a trade-off between good design practices and code that performs at the highest optimization. Prioritize developer productivity and good software design in areas where performance is not a concern.

## Related content

- [Tools for profiling and performance](profiling-tools.md)
- [Keep the UI thread responsive](keep-ui-thread-responsive.md)
- [Optimize your XAML markup](optimize-xaml-loading.md)
