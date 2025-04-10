---
title: Widget providers
description: Learn how to implement a Windows widget service provider to support your app. 
ms.topic: article
ms.date: 07/06/2022
ms.localizationpriority: medium
---



# Widget providers

Windows widgets are small UI containers that display text and graphics supplied by a Windows app or Progressive Web App (PWA). The Adaptive Cards format used by Windows widgets enables dynamic binding of the data that populates the widget UI. To update your widget, your app or service will implement a widget service provider that responds to requests from the Widgets host and returns JSON strings specifying both the visual template and the associated data for your widget.

For an overview of the Windows widgets experience and design guidance for creating your own widgets, see [Windows widgets](../../design/widgets/index.md).

Currently you can implement a widget provider using a packaged Win32 desktop app or a Progressive Web App (PWA). For more information see:

* [Implement a widget provider in a win32 app (C#)](implement-widget-provider-cs.md)
* [Implement a widget provider in a win32 app (C++/WinRT)](implement-widget-provider-win32.md)
* [Build PWA-driven widgets](/microsoft-edge/progressive-web-apps-chromium/how-to/widgets)



For API reference documentation for implementing widget providers, see the [Microsoft.Windows.Widgets.Providers](/windows/windows-app-sdk/api/winrt/microsoft.windows.widgets.providers) namespace.

For a walkthrough of the basics of creating a widget and implementing a widget provider, watch the *Tabs vs Spaces* episode "Create Widgets for Windows 11".

> [!VIDEO https://learn-video.azurefd.net/vod/player?show=tabs-vs-spaces&ep=create-widgets-for-windows-11]

## Additional resources
- [Report an issue via email](mailto:widgetssupport@microsoft.com)
- [Open an issue in the Windows App SDK github repo](https://github.com/microsoft/WindowsAppSDK/issues/new/choose)


## Related articles

* [Windows widgets](../../design/widgets/index.md)
