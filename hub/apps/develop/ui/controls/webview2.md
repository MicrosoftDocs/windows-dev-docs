---
title: WebView2 in WinUI 3
description: Add a WebView2 control to a WinUI 3 app to display web content using the Microsoft Edge (Chromium) rendering engine.
ms.topic: how-to
ms.date: 07/06/2026
author: GrantMeStrength
ms.author: jken
ms.localizationpriority: medium
---

# WebView2 in WinUI 3

The `WebView2` control embeds web content in a WinUI 3 application using the Microsoft Edge (Chromium) rendering engine. You can display URLs, render local HTML content, execute JavaScript, and handle web-to-app communication through the control's API.

> [!div class="checklist"]
>
> - **Important APIs**: [WebView2 class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.webview2)

> [!NOTE]
> `WebView2` in WinUI 3 is backed by the [Microsoft WebView2 SDK](https://developer.microsoft.com/microsoft-edge/webview2/). The WinUI 3 `WebView2` control wraps the Win32 WebView2 API and exposes it as a XAML element. Refer to the [WebView2 documentation](/microsoft-edge/webview2/) for the full set of capabilities.

## Prerequisites

- Windows App SDK 1.0 or later
- WebView2 Runtime installed on the target machine (use the [Evergreen distribution model](/microsoft-edge/webview2/concepts/distribution) for automatic updates)

The Evergreen model is recommended for most apps: the WebView2 Runtime updates automatically through Microsoft Edge and is pre-installed on Windows 11 and Windows 10 (version 1803 and later with the November 2022 update).

## Add a WebView2 control

Add `WebView2` to your XAML layout and set the `Source` property to the URL you want to display:

```xaml
<WebView2 x:Name="MyWebView2"
          Source="https://learn.microsoft.com/windows/apps/"
          HorizontalAlignment="Stretch"
          VerticalAlignment="Stretch"
          MinHeight="400" />
```

The control loads the URL when it is first rendered. To navigate programmatically, set the `Source` property in code or call `CoreWebView2.Navigate`:

```csharp
MyWebView2.Source = new Uri("https://example.com");
```

## Initialize the control

Before using advanced WebView2 features (such as executing JavaScript or intercepting navigation), wait for the control to finish initializing by handling the `CoreWebView2Initialized` event or awaiting `EnsureCoreWebView2Async`:

```csharp
await MyWebView2.EnsureCoreWebView2Async();
// Now CoreWebView2 is available
MyWebView2.CoreWebView2.Navigate("https://example.com");
```

## Execute JavaScript

After the control is initialized, use `ExecuteScriptAsync` to run JavaScript in the current page:

```csharp
await MyWebView2.EnsureCoreWebView2Async();
string result = await MyWebView2.CoreWebView2.ExecuteScriptAsync("document.title");
```

## Handle navigation events

Subscribe to `NavigationStarting` and `NavigationCompleted` to track or intercept navigation:

```csharp
MyWebView2.NavigationStarting += (sender, args) =>
{
    // args.Uri contains the destination URL
    // Set args.Cancel = true to block navigation
};

MyWebView2.NavigationCompleted += (sender, args) =>
{
    if (!args.IsSuccess)
    {
        // Handle navigation error
    }
};
```

## Open the WinUI 3 Gallery

> [!div class="nextstepaction"]
> [Open the WinUI 3 Gallery app and see WebView2 in action](winui3gallery://item/WebView2)

The [WinUI 3 Gallery](https://apps.microsoft.com/detail/9P3JFPWWDZRC) app includes interactive examples of most WinUI 3 controls and features.

## Related articles

- [Microsoft Edge WebView2 overview](/microsoft-edge/webview2/)
- [Get started with WebView2 in WinUI 3 apps](/microsoft-edge/webview2/get-started/winui)
- [WebView2 distribution and deployment](/microsoft-edge/webview2/concepts/distribution)
- [Communicate between web content and host app](/microsoft-edge/webview2/concepts/overview-features-apis#web-messaging)
- [WebView2 environment options](/microsoft-edge/webview2/concepts/overview-features-apis#webview2-environment-options)
