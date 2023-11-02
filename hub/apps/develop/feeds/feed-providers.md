---
title: Feed providers
description: Learn how to implement a Windows feed service provider to support your app. 
ms.topic: article
ms.date: 07/06/2022
ms.author: drewbat
author: drewbatgit
ms.localizationpriority: medium
---



# Feed providers

Windows feeds are a collection of text an images from a remote provider that are displayed in an iframe on the Windows Widgets Board. Third parties can integrate feeds into the Widgets Board by implementing an app that is published through the Windows Store. The feed provider app registers the URLs from which content is retrieved in its app manifest. The feed provider also responds to events such as requests for custom query parameters, typically for authentication scenarios. The feed content itself is retrieved directly from the specified endpoint URI and is not managed by the feed provider app. 

For an overview of the Windows widgets experience and design guidance for creating your own widgets, see [Windows feeds](../../design/feeds/index.md).

Currently you can implement a feed provider using a packaged Win32 desktop app or a Progressive Web App (PWA). For more information see:

* [Implement a feed provider in a win32 app (C#)](implement-feed-provider-cs.md)
* [Implement a feed provider in a win32 app (C++/WinRT)](implement-feed-provider-win32.md)
* [Build PWA-driven widgets](/microsoft-edge/progressive-web-apps-chromium/how-to/widgets)


For API reference documentation for implementing widget providers, see the [Microsoft.Windows.Widgets.Providers](/windows/windows-app-sdk/api/winrt/microsoft.windows.widgets.providers) namespace.


## Additional resources
- [Report an issue via email](mailto:widgetssupport@microsoft.com)
- [Open an issue in the Windows App SDK github repo](https://github.com/microsoft/WindowsAppSDK/issues/new/choose)


## Related articles

* [Windows widgets](../../design/widgets/index.md)
