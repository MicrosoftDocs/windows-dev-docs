---
title: Widget providers
description: Learn how to implement a Windows widget service provider that responds to requests from the Widgets Board and returns widget content.
ms.topic: article
ms.date: 10/23/2026
ms.localizationpriority: medium
author: GrantMeStrength
ms.author: jken
---



# Widget providers

Windows widgets are small UI containers that display content supplied by a Windows app or Progressive Web App (PWA). You can provide an Adaptive Card template and data, or use a web widget to display HTML from a URL. Web widgets still use an Adaptive Card JSON payload to specify the content URL.

For a packaged desktop app, you implement a widget provider that responds to requests from the widget host and sends updates through [WidgetManager](/windows/windows-app-sdk/api/winrt/microsoft.windows.widgets.providers.widgetmanager). A PWA uses the widget definitions in its web app manifest and handles widget events in its service worker.

For an overview of the Windows widgets experience and design guidance for creating your own widgets, see [Windows widgets](../../design/widgets/index.md).

Currently you can implement a widget provider using a packaged Win32 desktop app or a Progressive Web App (PWA). For more information see:

* [Implement a widget provider in a win32 app (C#)](implement-widget-provider-cs.md)
* [Implement a widget provider in a win32 app (C++/WinRT)](implement-widget-provider-win32.md)
* [Build PWA-driven widgets](/microsoft-edge/progressive-web-apps/how-to/widgets)
* [Web widget providers](web-widget-providers.md)
* [Widget header customization](widget-header-customization.md)



## Desktop widget provider APIs and capabilities

Implement [IWidgetProvider](/windows/windows-app-sdk/api/winrt/microsoft.windows.widgets.providers.iwidgetprovider) for widget creation, deletion, activation, context changes, and user actions. The base interface remains required when you add optional interfaces for customization or web content.

| Capability | API or guidance | Availability |
| --- | --- | --- |
| Widget lifecycle and Adaptive Card updates | **IWidgetProvider** and **WidgetManager** | Windows App SDK [1.2](../../windows-app-sdk/release-notes/windows-app-sdk-1-2.md) |
| User customization | [IWidgetProvider2](/windows/windows-app-sdk/api/winrt/microsoft.windows.widgets.providers.iwidgetprovider2) and the manifest's **IsCustomizable** attribute | Windows App SDK [1.4](../../windows-app-sdk/release-notes/windows-app-sdk-1-4.md) |
| Web content, resource requests, and messaging | [Web widget providers](web-widget-providers.md), **IWidgetResourceProvider**, **IWidgetProviderMessage**, and **WidgetManager.SendMessageToContent** | Windows App SDK [1.6.3](../../windows-app-sdk/release-notes/windows-app-sdk-1-6.md?pivots=stable#version-163-16241114003); also check the web-widget host and regional requirements. |
| Indicating content that isn't yet personalized or actionable | [WidgetUpdateRequestOptions.IsPlaceholderContent](/windows/windows-app-sdk/api/winrt/microsoft.windows.widgets.providers.widgetupdaterequestoptions.isplaceholdercontent) | Windows App SDK [1.6.6](../../windows-app-sdk/release-notes/windows-app-sdk-1-6.md?pivots=stable#version-166-16250228001) |
| Widget analytics callbacks | [IWidgetProviderAnalytics](/windows/windows-app-sdk/api/winrt/microsoft.windows.widgets.providers.iwidgetprovideranalytics) | The Windows Widgets Board supports these callbacks starting with Windows 11 build 26900. Other hosts must also support the interface; see the [API remarks](/windows/windows-app-sdk/api/winrt/microsoft.windows.widgets.providers.iwidgetprovideranalytics#remarks). |

The SDK versions identify when APIs became available, not which release you should install. Use a currently supported stable release from [Windows App SDK release channels](../../windows-app-sdk/release-channels.md), and test the capabilities you use on your target Windows device and widget host. For header presentation options, see [Customize the widget header area](widget-header-customization.md). The PWA service-worker API is a separate implementation path; the desktop interfaces in this table don't apply to it.

The widget host can activate and release your provider independently of the lifetime of pinned widgets. Restore widget state when the provider starts, and don't retain callback argument objects after an **IWidgetProvider** callback returns. For more information, see the [IWidgetProvider remarks](/windows/windows-app-sdk/api/winrt/microsoft.windows.widgets.providers.iwidgetprovider#remarks).

For the full API reference, see the [Microsoft.Windows.Widgets.Providers](/windows/windows-app-sdk/api/winrt/microsoft.windows.widgets.providers) namespace.

For a walkthrough of the basics of creating a widget and implementing a widget provider, watch the *Tabs vs Spaces* episode "Create Widgets for Windows 11".

> [!VIDEO https://learn-video.azurefd.net/vod/player?show=tabs-vs-spaces&ep=create-widgets-for-windows-11]

## Additional resources
- [Open an issue in the Windows App SDK github repo](https://github.com/microsoft/WindowsAppSDK/issues/new/choose)


## Related articles

* [Windows widgets](../../design/widgets/index.md)
