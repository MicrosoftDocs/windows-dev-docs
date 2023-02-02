---
title: Widget providers
description: Learn how to implement a Windows widget service provider to support your app. 
ms.topic: article
ms.date: 07/06/2022
ms.author: drewbat
author: drewbatgit
ms.localizationpriority: medium
---



# Widget providers

> [!NOTE]
> **Some information relates to pre-released product, which may be substantially modified before it's commercially released. Microsoft makes no warranties, express or implied, with respect to the information provided here.**
> [!IMPORTANT]
> The feature described in this topic is available in Dev Channel preview builds of Windows starting with build 25217. For information on preview builds of Windows, see [Windows 10 Insider Preview](https://insider.windows.com/en-us/preview-windows).


Windows widgets are small UI containers that display text and graphics from an app or web service. The Adaptive Cards format used by Windows widgets enables dynamic binding of the data that populates the widget UI. To update your widget, your app or service will implement a widget service provider that responds to requests from the Widgets host and returns JSON strings specifying both the visual template and the associated data for your widget.

For an overview of the Windows widgets experience and design guidance for creating your own widgets, see [Windows widgets](../../design/widgets/index.md).

Currently you can implement a widget provider using a packaged Win32 desktop app.  Support for Progressive Web App (PWA) is planned for future releases. For more information see:

* [Implement a widget provider in a win32 app (C#)](implement-widget-provider-cs.md)
* [Implement a widget provider in a win32 app (C++/WinRT)](implement-widget-provider-win32.md)

For API reference documentation for implementing widget providers, see the [Microsoft.Windows.Widgets.Providers](/windows/windows-app-sdk/api/winrt/microsoft.windows.widgets.providers) namespace.

For a walkthrough of the basics of creating a widget and implementing a widget provider, watch the video "How to create Widgets for Windows 11".

<iframe width="560" height="315" src="https://www.youtube.com/embed/PUhYfRepxvc" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" allowfullscreen></iframe>

## Additional resources
- [Report an issue via email](mailto:widgetssupport@microsoft.com)
- [Open an issue in the Windows App SDK github repo](https://github.com/microsoft/WindowsAppSDK/issues/new/choose)


## Related articles

* [Windows widgets](../../design/widgets/index.md)
