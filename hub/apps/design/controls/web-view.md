---
description: A web view control embeds a view into your app that renders web content using the Microsoft Edge rendering engine. Hyperlinks can also appear and function in a web view control.
title: Web view
ms.assetid: D3CFD438-F9D6-4B72-AF1D-16EF2DFC1BB1
label: Web view
template: detail.hbs
ms.date: 03/30/2021
ms.topic: article
ms.localizationpriority: medium
---
# Web view

A web view control embeds a view into your app that renders web content using the Microsoft Edge Legacy rendering engine. Hyperlinks can also appear and function in a web view control.

> [!IMPORTANT]
> The `WebView2` control is available as part of the [Windows UI Library 3 (WinUI3)](../../winui/index.md). `WebView2` uses Microsoft Edge (Chromium) as the rendering engine to display web content in apps. For more info, see [Introduction to Microsoft Edge WebView2](/microsoft-edge/webview2/), [Getting started with WebView2 in WinUI 3 (Preview)](/microsoft-edge/webview2/gettingstarted/winui), and [WebView2](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.webview2) in the WinUI API reference.

## Is this the right control?

Use a web view control to display richly formatted HTML content from a remote web server, dynamically generated code, or content files in your app package. Rich content can also contain script code and communicate between the script and your app's code.

## Recommendations

- Make sure that the website loaded is formatted correctly for the device and uses colors, typography, and navigation that are consistent with the rest of your app.
- Input fields should be appropriately sized. Users may not realize that they can zoom in to enter text.
- If a web view doesn't look like the rest of your app, consider alternative controls or ways to accomplish relevant tasks. If your web view matches the rest of your app, users will see it all as one seamless experience.

## Create a web view

> [!div class="checklist"]
>
> - **UWP APIs:** [WebView class](/uwp/api/windows.ui.xaml.controls.webview)
> - [Open the WinUI 2 Gallery app and see the WebView in action](winui2gallery:/item/WebView). [!INCLUDE [winui-2-gallery](../../../includes/winui-2-gallery.md)]

**Modify the appearance of a web view**

[WebView](/uwp/api/windows.ui.xaml.controls.webview) is not a [Control](/uwp/api/windows.UI.Xaml.Controls.Control) subclass, so it doesn't have a control template. However, you can set various properties to control some visual aspects of the web view.

- To constrain the display area, set the [Width](/uwp/api/windows.ui.xaml.frameworkelement.width) and [Height](/uwp/api/windows.ui.xaml.frameworkelement.height) properties.
- To translate, scale, skew, and rotate a web view, use the [RenderTransform](/uwp/api/windows.ui.xaml.uielement.rendertransform) property.
- To control the opacity of the web view, set the [Opacity](/uwp/api/windows.ui.xaml.uielement.opacity) property.
- To specify a color to use as the web page background when the HTML content does not specify a color, set the [DefaultBackgroundColor](/uwp/api/windows.ui.xaml.controls.webview.defaultbackgroundcolor) property.

**Get the web page title**

You can get the title of the HTML document currently displayed in the web view by using the [DocumentTitle](/uwp/api/windows.ui.xaml.controls.webview.documenttitle) property.

**Input events and tab order**

Although WebView is not a Control subclass, it will receive keyboard input focus and participate in the tab sequence. It provides a [Focus](/uwp/api/windows.ui.xaml.controls.webview.focus) method, and [GotFocus](/uwp/api/windows.ui.xaml.uielement.gotfocus) and [LostFocus](/uwp/api/windows.ui.xaml.uielement.lostfocus) events, but it has no tab-related properties. Its position in the tab sequence is the same as its position in the XAML document order. The tab sequence includes all elements in the web view content that can receive input focus.

As indicated in the Events table on the [WebView](/uwp/api/windows.ui.xaml.controls.webview) class page, web view doesn't support most of the user input events inherited from [UIElement](/uwp/api/windows.UI.Xaml.UIElement), such as [KeyDown](/uwp/api/windows.ui.xaml.uielement.keydown), [KeyUp](/uwp/api/windows.ui.xaml.uielement.keyup), and [PointerPressed](/uwp/api/windows.ui.xaml.uielement.pointerpressed). Instead, you can use [InvokeScriptAsync](/uwp/api/windows.ui.xaml.controls.webview.invokescriptasync) with the JavaScript **eval** function to use the HTML event handlers, and to use **window.external.notify** from the HTML event handler to notify the application using [WebView.ScriptNotify](/uwp/api/windows.ui.xaml.controls.webview.scriptnotify).

### Navigating to content

Web view provides several APIs for basic navigation: [GoBack](/uwp/api/windows.ui.xaml.controls.webview.goback), [GoForward](/uwp/api/windows.ui.xaml.controls.webview.goforward), [Stop](/uwp/api/windows.ui.xaml.controls.webview.stop), [Refresh](/uwp/api/windows.ui.xaml.controls.webview.refresh), [CanGoBack](/uwp/api/windows.ui.xaml.controls.webview.cangoback), and [CanGoForward](/uwp/api/windows.ui.xaml.controls.webview.cangoforward). You can use these to add typical web browsing capabilities to your app.

To set the initial content of the web view, set the [Source](/uwp/api/windows.ui.xaml.controls.webview.source) property in XAML. The XAML parser automatically converts the string to a [Uri](/uwp/api/windows.Foundation.Uri).

```xaml
<!-- Source file is on the web. -->
<WebView x:Name="webView1" Source="http://www.contoso.com"/>

<!-- Source file is in local storage. -->
<WebView x:Name="webView2" Source="ms-appdata:///local/intro/welcome.html"/>

<!-- Source file is in the app package. -->
<WebView x:Name="webView3" Source="ms-appx-web:///help/about.html"/>
```

The Source property can be set in code, but rather than doing so, you typically use one of the **Navigate** methods to load content in code.

To load web content, use the [Navigate](/uwp/api/windows.ui.xaml.controls.webview.navigate) method with a **Uri** that uses the http or https scheme.

```csharp
webView1.Navigate(new Uri("http://www.contoso.com"));
```

To navigate to a URI with a POST request and HTTP headers, use the [NavigateWithHttpRequestMessage](/uwp/api/windows.ui.xaml.controls.webview.navigatewithhttprequestmessage) method. This method supports only [HttpMethod.Post](/uwp/api/windows.web.http.httpmethod.post) and [HttpMethod.Get](/uwp/api/windows.web.http.httpmethod.get) for the [HttpRequestMessage.Method](/uwp/api/windows.web.http.httprequestmessage.method) property value.

To load uncompressed and unencrypted content from your app's [LocalFolder](/uwp/api/windows.storage.applicationdata.localfolder) or [TemporaryFolder](/uwp/api/windows.storage.applicationdata.temporaryfolder) data stores, use the **Navigate** method with a **Uri** that uses the [ms-appdata scheme](/windows/uwp/app-resources/uri-schemes). The web view support for this scheme requires you to place your content in a subfolder under the local or temporary folder. This enables navigation to URIs such as ms-appdata:///local/*folder*/*file*.html and ms-appdata:///temp/*folder*/*file*.html . (To load compressed or encrypted files, see [NavigateToLocalStreamUri](/uwp/api/windows.ui.xaml.controls.webview.navigatetolocalstreamuri).)

Each of these first-level subfolders is isolated from the content in other first-level subfolders. For example, you can navigate to ms-appdata:///temp/folder1/file.html, but you can't have a link in this file to ms-appdata:///temp/folder2/file.html. However, you can still link to HTML content in the app package using the **ms-appx-web scheme**, and to web content using the **http** and **https** URI schemes.

```csharp
webView1.Navigate(new Uri("ms-appdata:///local/intro/welcome.html"));
```

To load content from the your app package, use the **Navigate** method with a **Uri** that uses the [ms-appx-web scheme](/previous-versions/windows/apps/jj655406(v=win.10)).

```csharp
webView1.Navigate(new Uri("ms-appx-web:///help/about.html"));
```

You can load local content through a custom resolver using the [NavigateToLocalStreamUri](/uwp/api/windows.ui.xaml.controls.webview.navigatetolocalstreamuri) method. This enables advanced scenarios such as downloading and caching web-based content for offline use, or extracting content from a compressed file.

### Responding to navigation events

The web view control provides several events that you can use to respond to navigation and content loading states. The events occur in the following order for the root web view content: [NavigationStarting](/uwp/api/windows.ui.xaml.controls.webview.navigationstarting), [ContentLoading](/uwp/api/windows.ui.xaml.controls.webview.contentloading), [DOMContentLoaded](/uwp/api/windows.ui.xaml.controls.webview.domcontentloaded), [NavigationCompleted](/uwp/api/windows.ui.xaml.controls.webview.navigationcompleted)


**NavigationStarting** - Occurs before the web view navigates to new content. You can cancel navigation in a handler for this event by setting the WebViewNavigationStartingEventArgs.Cancel property to true.

```csharp
webView1.NavigationStarting += webView1_NavigationStarting;

private void webView1_NavigationStarting(object sender, WebViewNavigationStartingEventArgs args)
{
    // Cancel navigation if URL is not allowed. (Implementation of IsAllowedUri not shown.)
    if (!IsAllowedUri(args.Uri))
        args.Cancel = true;
}
```

**ContentLoading** - Occurs when the web view has started loading new content.

```csharp
webView1.ContentLoading += webView1_ContentLoading;

private void webView1_ContentLoading(WebView sender, WebViewContentLoadingEventArgs args)
{
    // Show status.
    if (args.Uri != null)
    {
        statusTextBlock.Text = "Loading content for " + args.Uri.ToString();
    }
}
```

**DOMContentLoaded** - Occurs when the web view has finished parsing the current HTML content.

```csharp
webView1.DOMContentLoaded += webView1_DOMContentLoaded;

private void webView1_DOMContentLoaded(WebView sender, WebViewDOMContentLoadedEventArgs args)
{
    // Show status.
    if (args.Uri != null)
    {
        statusTextBlock.Text = "Content for " + args.Uri.ToString() + " has finished loading";
    }
}
```

**NavigationCompleted** - Occurs when the web view has finished loading the current content or if navigation has failed. To determine whether navigation has failed, check the [IsSuccess](/uwp/api/windows.ui.xaml.controls.webviewnavigationcompletedeventargs.issuccess) and [WebErrorStatus](/uwp/api/windows.ui.xaml.controls.webviewnavigationcompletedeventargs.weberrorstatus) properties of the [WebViewNavigationCompletedEventArgs](/uwp/api/windows.ui.xaml.controls.webviewNavigationCompletedEventArgs) class.

```csharp
webView1.NavigationCompleted += webView1_NavigationCompleted;

private void webView1_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
{
    if (args.IsSuccess == true)
    {
        statusTextBlock.Text = "Navigation to " + args.Uri.ToString() + " completed successfully.";
    }
    else
    {
        statusTextBlock.Text = "Navigation to: " + args.Uri.ToString() +
                               " failed with error " + args.WebErrorStatus.ToString();
    }
}
```

Similar events occur in the same order for each **iframe** in the web view content:
- [FrameNavigationStarting](/uwp/api/windows.ui.xaml.controls.webview.framenavigationstarting) - Occurs before a frame in the web view navigates to new content.
- [FrameContentLoading](/uwp/api/windows.ui.xaml.controls.webview.framecontentloading) - Occurs when a frame in the web view has started loading new content.
- [FrameDOMContentLoaded](/uwp/api/windows.ui.xaml.controls.webview.framedomcontentloaded) - Occurs when a frame in the web view has finished parsing its current HTML content.
- [FrameNavigationCompleted](/uwp/api/windows.ui.xaml.controls.webview.framenavigationcompleted) - Occurs when a frame in the web view has finished loading its content.

### Responding to potential problems

You can respond to potential problems with the content such as long running scripts, content that web view can't load, and warnings of unsafe content.

Your app might appear unresponsive while scripts are running. The [LongRunningScriptDetected](/uwp/api/windows.ui.xaml.controls.webview.longrunningscriptdetected) event occurs periodically while the web view executes JavaScript and provides an opportunity to interrupt the script. To determine how long the script has been running, check the [ExecutionTime](/uwp/api/windows.ui.xaml.controls.webviewlongrunningscriptdetectedeventargs.executiontime) property of the [WebViewLongRunningScriptDetectedEventArgs](/uwp/api/windows.ui.xaml.controls.webviewLongRunningScriptDetectedEventArgs). To halt the script, set the event args [StopPageScriptExecution](/uwp/api/windows.ui.xaml.controls.webviewlongrunningscriptdetectedeventargs.stoppagescriptexecution) property to **true**. The halted script will not execute again unless it is reloaded during a subsequent web view navigation.

The web view control cannot host arbitrary file types. When an attempt is made to load content that the web view can't host, the [UnviewableContentIdentified](/uwp/api/windows.ui.xaml.controls.webview.unviewablecontentidentified) event occurs. You can handle this event and notify the user, or use the [Launcher](/uwp/api/windows.System.Launcher) class to redirect the file to an external browser or another app.

Similarly, the [UnsupportedUriSchemeIdentified](/uwp/api/windows.ui.xaml.controls.webview.unsupportedurischemeidentified) event occurs when a URI scheme that's not supported is invoked in the web content, such as fbconnect:// or mailto://. You can handle this event to provide custom behavior instead of allowing the default system launcher to launch the URI.

The [UnsafeContentWarningDisplayingevent](/uwp/api/windows.ui.xaml.controls.webview.unsafecontentwarningdisplaying) occurs when the web view shows a warning page for content that was reported as unsafe by the SmartScreen Filter. If the user chooses to continue the navigation, subsequent navigation to the page will not display the warning nor fire the event.

### Handling special cases for web view content

You can use the [ContainsFullScreenElement](/uwp/api/windows.ui.xaml.controls.webview.containsfullscreenelement) property and [ContainsFullScreenElementChanged](/uwp/api/windows.ui.xaml.controls.webview.containsfullscreenelementchanged) event to detect, respond to, and enable full-screen experiences in web content, such as full-screen video playback. For example, you may use the ContainsFullScreenElementChanged event to resize the web view to occupy the entirety of your app view, or, as the following example illustrates, put a windowed app in full screen mode when a full screen web experience is desired.

```csharp
// Assume webView is defined in XAML
webView.ContainsFullScreenElementChanged += webView_ContainsFullScreenElementChanged;

private void webView_ContainsFullScreenElementChanged(WebView sender, object args)
{
    var applicationView = ApplicationView.GetForCurrentView();

    if (sender.ContainsFullScreenElement)
    {
        applicationView.TryEnterFullScreenMode();
    }
    else if (applicationView.IsFullScreenMode)
    {
        applicationView.ExitFullScreenMode();
    }
}
```

You can use the [NewWindowRequested](/uwp/api/windows.ui.xaml.controls.webview.newwindowrequested) event to handle cases where hosted web content requests a new window to be displayed, such as a popup window. You can use another WebView control to display the contents of the requested window.

Use [PermissionRequested](/uwp/api/windows.ui.xaml.controls.webview.permissionrequested) event to enable web features that require special capabilities. These currently include geolocation, IndexedDB storage, and user audio and video (for example, from a microphone or webcam). If your app accesses user location or user media, you still are required to declare this capability in the app manifest. For example, an app that uses geolocation needs the following capability declarations at minimum in Package.appxmanifest:

```xml
  <Capabilities>
    <Capability Name="internetClient" />
    <DeviceCapability Name="location" />
  </Capabilities>
```

In addition to the app handling the [PermissionRequested](/uwp/api/windows.ui.xaml.controls.webview.permissionrequested) event, the user will have to approve standard system dialogs for apps requesting location or media capabilities in order for these features to be enabled.

Here is an example of how an app would enable geolocation in a map from Bing:

```csharp
// Assume webView is defined in XAML
webView.PermissionRequested += webView_PermissionRequested;

private void webView_PermissionRequested(WebView sender, WebViewPermissionRequestedEventArgs args)
{
    if (args.PermissionRequest.PermissionType == WebViewPermissionType.Geolocation &&
        args.PermissionRequest.Uri.Host == "www.bing.com")
    {
        args.PermissionRequest.Allow();
    }
}
```

If your app requires user input or other asynchronous operations to respond to a permission request, use the [Defer](/uwp/api/windows.ui.xaml.controls.webviewpermissionrequest.defer) method of [WebViewPermissionRequest](/uwp/api/windows.ui.xaml.controls.webviewPermissionRequest) to create a [WebViewDeferredPermissionRequest](/uwp/api/windows.ui.xaml.controls.webviewDeferredPermissionRequest) that can be acted upon at a later time. See [WebViewPermissionRequest.Defer](/uwp/api/windows.ui.xaml.controls.webviewpermissionrequest.defer).

If users must securely log out of a website hosted in a web view, or in other cases when security is important, call the static method [ClearTemporaryWebDataAsync](/uwp/api/windows.ui.xaml.controls.webview.cleartemporarywebdataasync) to clear out all locally cached content from a web view session. This prevents malicious users from accessing sensitive data.

### Interacting with web view content

You can interact with the content of the web view by using the [InvokeScriptAsync](/uwp/api/windows.ui.xaml.controls.webview.invokescriptasync) method to invoke or inject script into the web view content, and the [ScriptNotify](/uwp/api/windows.ui.xaml.controls.webview.scriptnotify) event to get information back from the web view content.

To invoke JavaScript inside the web view content, use the [InvokeScriptAsync](/uwp/api/windows.ui.xaml.controls.webview.invokescriptasync) method. The invoked script can return only string values.

For example, if the content of a web view named `webView1` contains a function named `setDate` that takes 3 parameters, you can invoke it like this.

```csharp
string[] args = {"January", "1", "2000"};
string returnValue = await webView1.InvokeScriptAsync("setDate", args);
```


You can use **InvokeScriptAsync** with the JavaScript **eval** function to inject content into the web page.

Here, the text of a XAML text box (`nameTextBox.Text`) is written to a div in an HTML page hosted in `webView1`.

```csharp
private async void Button_Click(object sender, RoutedEventArgs e)
{
    string functionString = String.Format("document.getElementById('nameDiv').innerText = 'Hello, {0}';", nameTextBox.Text);
    await webView1.InvokeScriptAsync("eval", new string[] { functionString });
}
```

Scripts in the web view content can use **window.external.notify** with a string parameter to send information back to your app. To receive these messages, handle the [ScriptNotify](/uwp/api/windows.ui.xaml.controls.webview.scriptnotify) event.

To enable an external web page to fire the **ScriptNotify** event when calling window.external.notify, you must include the page's URI in the **ApplicationContentUriRules** section of the app manifest. (You can do this in Microsoft Visual Studio on the Content URIs tab of the Package.appxmanifest designer.) The URIs in this list must use HTTPS, and may contain subdomain wildcards (for example, `https://*.microsoft.com`) but they cannot contain domain wildcards (for example, `https://*.com` and `https://*.*`). The manifest requirement does not apply to content that originates from the app package, uses an ms-local-stream:// URI, or is loaded using [NavigateToString](/uwp/api/windows.ui.xaml.controls.webview.navigatetostring).

### Accessing the Windows Runtime in a web view

You can use the [AddWebAllowedObject](/uwp/api/windows.ui.xaml.controls.webview.addweballowedobject) method to inject an instance of a native class from a Windows Runtime component into the JavaScript context of the web view. This allows full access to the native methods, properties, and events of that object in the JavaScript content of that web view. The class must be decorated with the [AllowForWeb](/uwp/api/windows.Foundation.Metadata.AllowForWebAttribute) attribute.

For example, this code injects an instance of `MyClass` imported from a Windows Runtime component into a web view.

```csharp
private void webView_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
{
    if (args.Uri.Host == "www.contoso.com")
    {
        webView.AddWebAllowedObject("nativeObject", new MyClass());
    }
}
```

For more info, see [WebView.AddWebAllowedObject](/uwp/api/windows.ui.xaml.controls.webview.addweballowedobject).

In addition, trusted JavaScript content in a web view can be allowed to directly access Windows Runtime APIs. This provides powerful native capabilities for web apps hosted in a web view. To enable this feature, the URI for trusted content must be allowlisted in the ApplicationContentUriRules of the app in Package.appxmanifest, with WindowsRuntimeAccess specifically set to "all".

This example shows a section of the app manifest. Here, a local URI is given access to the Windows Runtime.

```xml
  <Applications>
    <Application Id="App"
      ...

      <uap:ApplicationContentUriRules>
        <uap:Rule Match="ms-appx-web:///Web/App.html" WindowsRuntimeAccess="all" Type="include"/>
      </uap:ApplicationContentUriRules>
    </Application>
  </Applications>
```

### Options for web content hosting

You can use the [WebView.Settings](/uwp/api/windows.ui.xaml.controls.webview.settings) property (of type [WebViewSettings](/uwp/api/windows.ui.xaml.controls.webviewSettings)) to control whether JavaScript and IndexedDB are enabled. For example, if you use a web view to display strictly static content, you might want to disable JavaScript for best performance.

### Capturing web view content

To enable sharing web view content with other apps, use the [CaptureSelectedContentToDataPackageAsync](/uwp/api/windows.ui.xaml.controls.webview.captureselectedcontenttodatapackageasync) method, which returns the selected content as a [DataPackage](/uwp/api/windows.ApplicationModel.DataTransfer.DataPackage). This method is asynchronous, so you must use a deferral to prevent your [DataRequested](/uwp/api/windows.applicationmodel.datatransfer.datatransfermanager.datarequested) event handler from returning before the asynchronous call is complete.

To get a preview image of the web view's current content, use the [CapturePreviewToStreamAsync](/uwp/api/windows.ui.xaml.controls.webview.capturepreviewtostreamasync) method. This method creates an image of the current content and writes it to the specified stream.

### Threading behavior

By default, web view content is hosted on the UI thread on devices in the desktop device family, and off the UI thread on all other devices. You can use the [WebView.DefaultExecutionMode](/uwp/api/windows.ui.xaml.controls.webview.defaultexecutionmode) static property to query the default threading behavior for the current client. If necessary, you can use the [WebView(WebViewExecutionMode)](/uwp/api/windows.ui.xaml.controls.webview.-ctor#Windows_UI_Xaml_Controls_WebView__ctor_Windows_UI_Xaml_Controls_WebViewExecutionMode_) constructor to override this behavior.

> **Note**&nbsp;&nbsp;There might be performance issues when hosting content on the UI thread on mobile devices, so be sure to test on all target devices when you change DefaultExecutionMode.

A web view that hosts content off the UI thread is not compatible with parent controls that require gestures to propagate up from the web view control to the parent, such as [FlipView](/uwp/api/windows.UI.Xaml.Controls.FlipView), [ScrollViewer](/uwp/api/windows.UI.Xaml.Controls.ScrollViewer), and other related controls. These controls will not be able to receive gestures initiated in the off-thread web view. In addition, printing off-thread web content is not directly supported – you should print an element with [WebViewBrush](/uwp/api/windows.ui.xaml.controls.webviewbrush) fill instead.

## Get the sample code

- [WinUI 2 Gallery sample](https://github.com/Microsoft/WinUI-Gallery) - See all the XAML controls in an interactive format.

## Related topics

- [WebView class](/uwp/api/windows.ui.xaml.controls.webview)
- [Introduction to Microsoft Edge WebView2](/microsoft-edge/webview2/)
- [Getting started with WebView2 in WinUI 3 (Preview)](/microsoft-edge/webview2/gettingstarted/winui)
- [WebView2](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.webview2)
