---
title: Web widget providers
description: Learn how to implement a Windows widget that serves its content from a remote URL, including setup, limitations, and example code.
ms.topic: how-to
ms.date: 10/23/2026
ms.localizationpriority: medium
author: GrantMeStrength
ms.author: jken
---

# Web widget providers

You can populate a Windows widget with HTML served from a URL instead of defining its body with Adaptive Cards. Your provider still supplies an Adaptive Card JSON payload that specifies the content URL. Start with [Implement a widget provider in a C# Windows App](implement-widget-provider-cs.md) or [Implement a widget provider in a win32 app (C++/WinRT)](implement-widget-provider-win32.md), then add the web-content capabilities described here.

The resource and messaging APIs in this article were introduced in [Windows App SDK 1.6.3](../../windows-app-sdk/release-notes/windows-app-sdk-1-6.md?pivots=stable#version-163-16241114003). Use a currently supported stable release from [Windows App SDK release channels](../../windows-app-sdk/release-channels.md), and test on a Windows 11 device with a widget host that supports web content.

The C# examples are fragments for a packaged desktop widget provider. Add `using Microsoft.Windows.Widgets.Providers;` and `using Windows.Storage.Streams;` to the provider's source file. Continue to implement **IWidgetProvider** when you add **IWidgetResourceProvider** or **IWidgetProviderMessage**.

## Specify the content URL

Send your JSON payload with [WidgetManager.UpdateWidget](/windows/windows-app-sdk/api/winrt/microsoft.windows.widgets.providers.widgetmanager.updatewidget). Specify a **metadata** object with a **webUrl** field that points to the HTML content. Include an Adaptive Card **body** as fallback content for hosts that don't support loading web content, rather than leaving those hosts with an empty widget.

```json
{
    "type": "AdaptiveCard",
    "$schema": "http://adaptivecards.io/schemas/adaptive-card.json",
    "version": "1.6",
    "body": [
        {
            "type": "TextBlock",
            "text": "Open the app to view this content.",
            "wrap": true
        }
    ],
    "metadata": {
        "webUrl": "https://contoso.com/widgetprovider.html"
    }
}
```

The `$host.isWebSupported` template property indicates whether the host supports web content. Use it when you need to further adapt the fallback presentation. For details, see [Windows widget host properties](widgets-create-a-template.md#windows-widget-host-properties).

## Handle resource requests

Widget providers can specify a web request filter string for a widget in the *WebRequestFilter* attribute of the **Definition** element in the provider's package manifest file. Whenever the widget content requests a resource by URI that matches the filter, the request will be intercepted and redirected to the widget provider's implementation of [IWidgetResourceProvider.OnResourceRequested](/windows/windows-app-sdk/api/winrt/microsoft.windows.widgets.providers.iwidgetresourceprovider.onresourcerequested).

The filter pattern uses the format described in [Match Patterns](https://developer.mozilla.org/en-US/docs/Mozilla/Add-ons/WebExtensions/Match_patterns). Use [Punycode](https://en.wikipedia.org/wiki/Punycode) where necessary. The filter must match the origin specified by **webUrl**. Requests for all content types are redirected when they match, so limit the filter to resources your provider handles. For details, see [Widget provider package manifest XML format](widget-provider-manifest.md#definition).

To handle resource requests, widget providers must implement the [IWidgetResourceProvider](/windows/windows-app-sdk/api/winrt/microsoft.windows.widgets.providers.iwidgetresourceprovider) interface.

```csharp
internal class WidgetProvider : IWidgetProvider, IWidgetResourceProvider
```

In **OnResourceRequested**, set [WidgetResourceRequestedArgs.Response](/windows/windows-app-sdk/api/winrt/microsoft.windows.widgets.providers.widgetresourcerequestedargs.response) to a [WidgetResourceResponse](/windows/windows-app-sdk/api/winrt/microsoft.windows.widgets.providers.widgetresourceresponse) containing the resource. This example maps one requested URL to a packaged image. Include **Assets\image.png** as content in your app package. [RandomAccessStreamReference.CreateFromUri](/uwp/api/windows.storage.streams.randomaccessstreamreference.createfromuri) supports the **ms-appx** scheme, so you can create the stream reference without awaiting a file lookup.

```csharp
void IWidgetResourceProvider.OnResourceRequested(WidgetResourceRequestedArgs args)
{
    if (args.Request.Uri == "https://contoso.com/logo-image")
    {
        var imageUri = new System.Uri("ms-appx:///Assets/image.png");
        var content = RandomAccessStreamReference.CreateFromUri(imageUri);
        var response = new WidgetResourceResponse(content, "OK", 200);
        response.Headers.Add("Content-Type", "image/png");
        args.Response = response;
    }
}
```

This synchronous example doesn't need a deferral. If your implementation awaits work before setting the response, call [GetDeferral](/windows/windows-app-sdk/api/winrt/microsoft.windows.widgets.providers.widgetresourcerequestedargs.getdeferral) before the callback returns, and complete it in a `finally` block. Handle and report asynchronous failures according to your app's error-handling policy.

If you don't set a response, the system retrieves the resource from the web. You can modify [WidgetResourceRequest.Headers](/windows/windows-app-sdk/api/winrt/microsoft.windows.widgets.providers.widgetresourcerequest.headers) on **args.Request** before that fetch. Only add credentials to requests for origins you trust; a filter match alone isn't authorization to send a token.

## Handle messages to and from web content

To receive string messages from the widget's content that has been posted using the [window.chrome.webview.postMessage](/microsoft-edge/webview2/reference/javascript/webview) JavaScript method, widget providers can implement the [IWidgetProviderMessage](/windows/windows-app-sdk/api/winrt/microsoft.windows.widgets.providers.iwidgetprovidermessage) interface and implement the [OnMessageReceived](/windows/windows-app-sdk/api/winrt/microsoft.windows.widgets.providers.iwidgetprovidermessage.onmessagereceived) method.

```csharp
internal class WidgetProvider : IWidgetProvider, IWidgetProviderMessage
```

Add the callback to your provider class:

```csharp
public void OnMessageReceived(WidgetMessageReceivedArgs args)
{
    System.Console.WriteLine($"Message received from widget {args.WidgetContext.Id}: {args.Message}");
}
```

Send a message to a widget's web content with [WidgetManager.SendMessageToContent](/windows/windows-app-sdk/api/winrt/microsoft.windows.widgets.providers.widgetmanager.sendmessagetocontent). Pass the widget instance ID from [WidgetContext.Id](/windows/windows-app-sdk/api/winrt/microsoft.windows.widgets.providers.widgetcontext.id), not the definition ID from the package manifest. Multiple instances of one definition have different instance IDs.

Call the following helper from a provider callback, passing the **WidgetContext** for the target instance, such as the context received by **CreateWidget** or **OnActionInvoked**. Serialize structured messages rather than constructing JSON with string interpolation, so values containing quotes or backslashes remain valid.

```csharp
private static void SendLocationToWidget(WidgetContext widgetContext, string location)
{
    string message = System.Text.Json.JsonSerializer.Serialize(
        new { current_location = location });
    WidgetManager.GetDefault().SendMessageToContent(widgetContext.Id, message);
}
```

The web content receives the message as a string through the **window.chrome.webview** `message` event. If you serialize JSON as in this example, parse the string in your web content. For details, see [PostWebMessageAsString](/dotnet/api/microsoft.web.webview2.core.corewebview2.postwebmessageasstring) and the [WebView2 JavaScript API](/microsoft-edge/webview2/reference/javascript/webview).

## Limitations and requirements

> [!IMPORTANT]
> This feature is available only to users in the European Economic Area (EEA).

## Related implementation paths

Web widget providers aren't feed providers. For dashboard feeds and their regional requirements, see [Dashboards and feed providers](../feeds/feed-providers.md). For the separate PWA manifest and service-worker implementation path, see [Build PWA-driven widgets](/microsoft-edge/progressive-web-apps/how-to/widgets).







