---
title: Web widget providers
description: Learn how to implement a widget that displays content from a web source 
ms.topic: article
ms.date: 11/19/2024
ms.localizationpriority: medium
---

# Web widget providers

In the latest release, apps that implement Windows widgets can choose to populate the widget content with HTML served from a remote URL. Previously, the widget content could only be supplied in the Adaptive Card schema format in the JSON payload passed from the provider to the Widgets Board. Because web widget providers must still provide an Adaptive Card JSON payload, you should follow the steps for implementing a widget provider in [Implement a widget provider in a C# Windows App](implement-widget-provider-cs.md) or [Implement a widget provider in a win32 app (C++/WinRT)](implement-widget-provider-win32.md).

## Specify the content URL

Widget providers pass a JSON payload to the Widgets Board with a call to [WidgetManager.UpdateWidget](/windows/windows-app-sdk/api/winrt/microsoft.windows.widgets.providers.widgetmanager.updatewidget). For a web widget, instead of providing a **body** object defining the widget content, you should specify an empty **body** object and instead include a **metadata** object with a **webUrl** field that points to the URL that will supply the HTML content for the widget.

```json
{ 
    "type": "AdaptiveCard", 
    "$schema": "http://adaptivecards.io/schemas/adaptive-card.json", 
    "version": "1.6", 
    "body": [], 
    "metadata": 
    { 
        "webUrl": "https://www.contoso.com/widgetprovider.html" 
    } 
} 
```

## Handle resource requests

Widget providers can specify a web request filter string for a widget in the *WebRequestFilter* attribute of the **Definition** element in the provider's package manifest file. Whenever the widget content requests a resource by URI that matches the filter, the request will be intercepted and redirected to the widget provider's implementation of [IWidgetResourceProvider.OnResourceRequested](/windows/windows-app-sdk/api/winrt/microsoft.windows.widgets.providers.iwidgetresourceprovider.onresourcerequested).

The filter pattern is expressed using the format described in [Match Patterns](https://developer.mozilla.org/en-US/docs/Mozilla/Add-ons/WebExtensions/Match_patterns). The filter string in the registration must use [Punycode](https://en.wikipedia.org/wiki/Punycode) where necessary. All content types will be redirected when matched so the filter should only resolve to content intended to be obtained through the **IWidgetResourceProvider** in the application. For more information on the widget provider package manifest format, see [Widget provider package manifest XML format](/windows/apps/develop/widgets/widget-provider-manifest).

To handle resource requests, widget providers must implement the [IWidgetResourceProvider](/windows/windows-app-sdk/api/winrt/microsoft.windows.widgets.providers.iwidgetresourceprovider) interface.

```csharp
internal class WidgetProvider : IWidgetProvider, IWidgetResourceProvider
```

In the implementation of the **OnResourceRequested** method, widget providers can provide the requested resources by setting the [WidgetResourceRequestedArgs.Response](/windows/windows-app-sdk/api/winrt/microsoft.windows.widgets.providers.widgetresourcerequestedargs.response) property to a [WidgetResourceResponse](/windows/windows-app-sdk/api/winrt/microsoft.windows.widgets.providers.widgetresourceresponse) object containing the requested resource. When obtaining the resource asynchronously, the provider should request a deferral by calling [WidgetResourceRequestedArgs.GetDeferral](/windows/windows-app-sdk/api/winrt/microsoft.windows.widgets.providers.widgetresourcerequestedargs.getdeferral) and then complete the deferral when the resource has been set.

```csharp
async void IWidgetResourceProvider.OnResourceRequested(WidgetResourceRequestedArgs args)
{
    var deferral = args.GetDeferral();

    if (args.Request.Uri.Length > 0)
    {
        if (args.Request.Uri == "https://contoso.com/logo-image")
        {
            string fullPath = Windows.ApplicationModel.Package.Current.InstalledPath + "/Assets/image.png";
            var file = await StorageFile.GetFileFromPathAsync(fullPath);
            var response = new WidgetResourceResponse(RandomAccessStreamReference.CreateFromFile(file), "OK", 200);
            response.Headers.Add("Content-Type", "image/png");
            args.Response = response;
        }
    }

    deferral.Complete();
}
```

If the provider does not set a response on the **WidgetResourceRequestedArgs** object passed into the method, the system will retrieve the resource from the web. In this case, the provider can choose to modify the [Headers](/windows/windows-app-sdk/api/winrt/microsoft.windows.widgets.providers.widgetresourcerequestedargs.headers) property of the [WidgetResourceRequestedArgs.Request](/windows/windows-app-sdk/api/winrt/microsoft.windows.widgets.providers.widgetresourcerequestedargs.request) object, such as to provide user context or tokens, and the system will use the updated headers when retrieving the resource from the web.

## Handle messages to and from web content

To receive string messages from the widget's content that has been posted using the [window.chrome.webview.postMessage](/microsoft-edge/webview2/reference/javascript/webview) JavaScript method, widget providers can implement the [IWidgetProviderMessage](/windows/windows-app-sdk/api/winrt/microsoft.windows.widgets.providers.iwidgetprovidermessage) interface and implement the [OnMessageReceived](/windows/windows-app-sdk/api/winrt/microsoft.windows.widgets.providers.iwidgetprovidermessage.onmessagereceived) method.

```csharp
internal class WidgetProvider : IWidgetProvider, IWidgetProviderMessage
...
public void OnMessageReceived(WidgetMessageReceivedArgs args)
{
    Console.WriteLine($"Message received from widget {args.WidgetContext.Id}: {args.Message}");
}
```

Widget providers can send a message to the web content of the widget by calling [WidgetManager.SendMessage](/windows/windows-app-sdk/api/winrt/microsoft.windows.widgets.providers.widgetmanager.sendmessage). You must provide the ID of the widget to which the message is sent, which is the value specified in the *Id* attribute of the **Definition** element in the provider's package manifest file. For more information see [Widget provider package manifest XML format](/windows/apps/develop/widgets/widget-provider-manifest). The message string can be simple text or the serialized form of an object interpreted by the web content. For more information, see [PostWebMessageAsString](/dotnet/api/microsoft.web.webview2.core.corewebview2.postwebmessageasstring).

```csharp
var message = $"{{ \"current_location\": \"{ location }\" }}";
WidgetManager.GetDefault().SendMessageToContent("Weather_Widget", message);
```

## Limitations and requirements

* This feature is available only to users in the European Economic Area (EEA). In the EEA, installed apps that implement a feed provider can provide content feed in the Widgets Board.











