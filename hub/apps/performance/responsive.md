---
title: Power consumption improvements and Windows Application Performance
description: Improve power consumption and battery life by minimizing the use of system resources and not waking the CPU when your Windows app is in the background.
ms.author: mattwoj
author: mattwojo
ms.reviewer: sandeepp
ms.topic: conceptual
ms.date: 05/11/2022
#Customer intent: As a Windows application developer, I want to know how to improve the way my app consumes power by identifying and minimizing the use of system resources and not waking the CPU when my app is in the background.
---

# Improve responsiveness by optimizing latency for launch and interactions

This guide will demonstrate how to improve the responsiveness of your Windows application. Quick, responsive interactions (otherwise known as low-latency interactions) creates a better user experience.

Customers can feel when an application launch, menu navigation, or page/content load is slow. They have come to expect a fast, seamless experience and we have

 The basic steps to drive latency are:

1. Define the scenario and add ETW events.

    Make a list of the key interactions that a user will go through while using your app, such as launch, opening a menu, navigating to a new page and rendering content, etc. For each of these interactions, add a start event and stop event to be used for measurement and analysis. Learn more about how to add [Event Tracing for Windows (ETW)](/windows/win32/etw/event-tracing-portal) events.

2. Set goals based on the interaction class.

    Users have different expectations for an app's performance and responsiveness depending on the type of interaction. For example, how quickly an app launches versus how quickly a page loads. Think of the acceptable range of elapsed time that it takes for users to complete the key interactions in your app. This may range from 200 milliseconds (ms) to 5 seconds (sec). Then assign each task an interaction class label with an associated goal. Below are a few basic guidelines, along with suggestions for how you might include a user interface (UI) to improve the perception of responsiveness:

| Interaction class label | User perception| Range of delay | Examples | Suggested UI |
|---|---|---|---|---|
| Fast | Minimally noticeable delay| 100 - 200 ms | Open app bar, right click menu | |
| Interactive | Quick, but not fast | 300 - 500 ms | Exit an app, display cached search results | |
| Pause | Not quick, but feels responsive | 500 ms - 1 sec | Navigate to a different page, resume the app from a suspended state, display web search results | An entrance animation (e.g. fly in new content) may be used to mask the time taken for this scenario. |
| Wait | Not quick due to amount of work for scenario | 1 - 3 sec | Launching the app | A spinning/waiting cursor may be used to note progress. Both an exit and entrance animation (e.g. fly old page out, fly new page in) may be used to mask the time taken for this scenario. |
| Long wait | No longer feels responsive | 2 - 5 sec| Large app launches (use extended splash screen), starting an HD video stream | A "loading UI" is displayed – where possible, include a "cancel" option for the user. The loading UI should appear within the Fast interaction class. The loading UI does not need to display a percentage or time remaining. |
| Captive | A long wait – reserved for unavoidably long/complex scenarios | 5 - 10 sec| System login | A "loading UI" or dialog is displayed – where possible, include a "cancel" option for the user. The dialog should appear within the Fast interaction class. The dialog should display a percentage or time remaining if this would provide useful context to the user. |
| Long-running | Long operations – users will probably multitask (switch away during operation) | 10 - 30+ sec| Installing new features or updates, large file downloads | UI should be designed to reflect multitasking possibility. A progress dialog should be displayed including an estimate of completion (percentage, time remaining, etc.). Alternatively, the UI can minimize completely and only notify the user when the scenario has finished by using a toast notification. |

3. To check the exact durations for specific interactions, you can capture and analyze a trace using [Windows Performance Analyzer (WPA)](/windows-hardware/test/wpt/windows-performance-analyzer).

    - Before capturing your trace, get your test device to idle by opening Task Manager and ensuring that CPU utilization is less than 5%. Doing so will minimize measurement interference and provide reasonable size traces. This will help you to better isolate the interaction that you aim to measure.

    - To capture a trace, open a [command line](/windows/terminal/) (PowerShell or Command Prompt) in [administrator mode](/windows/terminal/faq#how-do-i-run-a-shell-in-windows-terminal-in-administrator-mode).

    - Enter the command: `wpr -start GeneralProfile -filemode`

    - Run the interaction scenario on your app.

    - Enter the command: `wpr -stop Trace.etl`

## Additional resources

- [Windows Performance Analyzer step-by-step guide](/windows-hardware/test/wpt/wpa-step-by-step-guide)

- [Windows Performance Analyzer: Optimizing Performance and Responsiveness](/windows-hardware/test/wpt/optimizing-performance-and-responsiveness)

- [Event Tracing for Windows](/windows-hardware/test/wpt/event-tracing-for-windows)

- [Q&A forum](/answers/questions/812324/i-don39t-have-sampled-cpu-usage-data-in-my-profile.html)

- [List of Windows Performance Toolkit (WPA) Graphs](/windows-hardware/test/wpt/list-of-wpa-graphs)
