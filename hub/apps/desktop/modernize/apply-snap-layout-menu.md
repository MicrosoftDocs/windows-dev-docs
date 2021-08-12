---
description: Apply the snap layout menu in your desktop application.
title: Apply the snap layout menu in desktop apps
ms.topic: article
ms.date: 08/12/2021
ms.author: jimwalk
author: jwmsft
ms.localizationpriority: medium
---

# Apply the Snap Layout Menu in desktop apps for Windows 11

The Snap Layout Menu is a key feature in Windows 11 to help introduce users to the power of window snapping. With the menu, users can quickly layout their windows to multitask on larger screens and create an entire layout with Guided Snap Assist by simply hovering the mouse over the maximize button or pressing Win + Z​.  

:::image type="content" source="./images/apply-design/snap-layout-menu.png" alt-text="The Notepad app on Windows 11 with rounded corners.":::

On Windows 11, if the application has the maximize caption button available, the system automatically provides the snap layout menu for all inbox apps, including all UWP apps, and most other apps. However, some desktop apps may not have the snap layout menu. This topic describes how to allow the snap layout menu to appear if the system does not provide it automatically.

## Why doesn't my app have the snap layout menu?

If your application's main window has the maximize caption button available but does not provide the snap layout menu, it may be because you've customized your caption buttons or title bar in a way that prevents it.  

## How do I fix it?

If you have a custom title bar, you can:​

- Use the [Windows App SDK windowing APIs](../../windows-app-sdk/windowing/windowing-overview.md) and have the platform draw and implement the caption buttons for you​.
- For Win32 apps, make sure you are responding appropriately to [WM_NCHITTEST](/windows/win32/inputdev/wm-nchittest) (with a return value of `HTMAXBUTTON` for the maximize/restore button)​.
- If your app uses [Electron](https://www.electronjs.org/), you can update to the v13 stable release of Electron to enable snap flyouts for your app.

## What if my window has the snap layout menu but isn't snapping properly?  

If your application can invoke the snap layout menu but isn't able to snap properly to the snap zones, it may be because your application's minimum size is too large for the snap zone to fit properly.  

Your application should support a minimum width of at most 500epx for snapping to work properly. However, we recommend that you support an even smaller minimum width (330epx or less) as it will be compatible with a larger set of devices and snap layouts.
