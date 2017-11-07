---
author: Jwmsft
Description: A web view control embeds a view into your app that renders web content using the Microsoft Edge rendering engine. Hyperlinks can also appear and function in a web view control.
title: Web view
ms.assetid: D3CFD438-F9D6-4B72-AF1D-16EF2DFC1BB1
label: Web view
template: detail.hbs
ms.author: jimwalk
ms.date: 05/19/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
localizationpriority: medium
---
# Web view
 

A web view control embeds a view into your app that renders web content using the Microsoft Edge rendering engine. Hyperlinks can also appear and function in a web view control.

> **Important APIs**: [WebView class](https://msdn.microsoft.com/library/windows/apps/br227702)

## Is this the right control?

Use a web view control to display richly formatted HTML content from a remote web server, dynamically generated code, or content files in your app package. Rich content can also contain script code and communicate between the script and your app's code.

## Create a web view

**Modify the appearance of a web view**

[WebView](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webview.aspx) is not a [Control](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.control.aspx) subclass, so it doesn't have a control template. However, you can set various properties to control some visual aspects of the web view.
- To constrain the display area, set the [Width](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.frameworkelement.width.aspx) and [Height](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.frameworkelement.height.aspx) properties. 
- To translate, scale, skew, and rotate a web view, use the [RenderTransform](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.uielement.rendertransform.aspx) property.
- To control the opacity of the web view, set the [Opacity](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.uielement.opacity.aspx) property.
- To specify a color to use as the web page background when the HTML content does not specify a color, set the [DefaultBackgroundColor](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webview.defaultbackgroundcolor.aspx) property. 

**Get the web page title**

You can get the title of the HTML document currently displayed in the web view by using the [DocumentTitle](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webview.documenttitle.aspx) property. 

**Input events and tab order**

Although WebView is not a Control subclass, it will receive keyboard input focus and participate in the tab sequence. It provides a [Focus](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webview.focus.aspx) method, and [GotFocus](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.uielement.gotfocus.aspx) and [LostFocus](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.uielement.lostfocus.aspx) events, but it has no tab-related properties. Its position in the tab sequence is the same as its position in the XAML document order. The tab sequence includes all elements in the web view content that can receive input focus. 

As indicated in the Events table on the [WebView](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webview.aspx) class page, web view doesn’t support most of the user input events inherited from [UIElement](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.uielement.aspx), such as [KeyDown](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.uielement.keydown.aspx), [KeyUp](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.uielement.keyup.aspx), and [PointerPressed](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.uielement.pointerpressed.aspx). Instead, you can use [InvokeScriptAsync](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webview.invokescriptasync.aspx) with the JavaScript **eval** function to use the HTML event handlers, and to use **window.external.notify** from the HTML event handler to notify the application using [WebView.ScriptNotify](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webview.scriptnotify.aspx).

### Navigating to content

Web view provides several APIs for basic navigation: [GoBack](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webview.goback.aspx), [GoForward](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webview.goforward.aspx), [Stop](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webview.stop.aspx), [Refresh](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webview.refresh.aspx), [CanGoBack](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webview.cangoback.aspx), and [CanGoForward](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webview.cangoforward.aspx). You can use these to add typical web browsing capabilities to your app. 

To set the initial content of the web view, set the [Source](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webview.source.aspx) property in XAML. The XAML parser automatically converts the string to a [Uri](https://msdn.microsoft.com/library/windows/apps/xaml/windows.foundation.uri.aspx). 

```xaml
<!-- Source file is on the web. -->
<WebView x:Name="webView1" Source="http://www.contoso.com"/>

<!-- Source file is in local storage. -->
<WebView x:Name="webView2" Source="ms-appdata:///local/intro/welcome.html"/>

<!-- Source file is in the app package. -->
<WebView x:Name="webView3" Source="ms-appx-web:///help/about.html"/>
```

The Source property can be set in code, but rather than doing so, you typically use one of the **Navigate** methods to load content in code. 

To load web content, use the [Navigate](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webview.navigate.aspx) method with a **Uri** that uses the http or https scheme. 

```csharp
webView1.Navigate("http://www.contoso.com");
```

To navigate to a URI with a POST request and HTTP headers, use the [NavigateWithHttpRequestMessage](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webview.navigatewithhttprequestmessage.aspx) method. This method supports only [HttpMethod.Post](https://msdn.microsoft.com/library/windows/apps/xaml/windows.web.http.httpmethod.post.aspx) and [HttpMethod.Get](https://msdn.microsoft.com/library/windows/apps/xaml/windows.web.http.httpmethod.get.aspx) for the [HttpRequestMessage.Method](https://msdn.microsoft.com/library/windows/apps/xaml/windows.web.http.httprequestmessage.method.aspx) property value. 

To load uncompressed and unencrypted content from your app’s [LocalFolder]() or [TemporaryFolder]() data stores, use the **Navigate** method with a **Uri** that uses the [ms-appdata scheme](). The web view support for this scheme requires you to place your content in a subfolder under the local or temporary folder. This enables navigation to URIs such as ms-appdata:///local/*folder*/*file*.html and ms-appdata:///temp/*folder*/*file*.html . (To load compressed or encrypted files, see [NavigateToLocalStreamUri](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webview.navigatetolocalstreamuri.aspx).) 

Each of these first-level subfolders is isolated from the content in other first-level subfolders. For example, you can navigate to ms-appdata:///temp/folder1/file.html, but you can’t have a link in this file to ms-appdata:///temp/folder2/file.html. However, you can still link to HTML content in the app package using the **ms-appx-web scheme**, and to web content using the **http** and **https** URI schemes.

```csharp
webView1.Navigate("ms-appdata:///local/intro/welcome.html");
```

To load content from the your app package, use the **Navigate** method with a **Uri** that uses the [ms-appx-web scheme](https://msdn.microsoft.com/library/windows/apps/xaml/jj655406.aspx#ms_appx_web). 

```csharp
webView1.Navigate("ms-appx-web:///help/about.html");
```

You can load local content through a custom resolver using the [NavigateToLocalStreamUri](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webview.navigatetolocalstreamuri.aspx) method. This enables advanced scenarios such as downloading and caching web-based content for offline use, or extracting content from a compressed file.

### Responding to navigation events

The web view control provides several events that you can use to respond to navigation and content loading states. The events occur in the following order for the root web view content: [NavigationStarting](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webview.navigationstarting.aspx), [ContentLoading](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webview.contentloading.aspx), [DOMContentLoaded](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webview.domcontentloaded.aspx), [NavigationCompleted](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webview.navigationcompleted.aspx)


**NavigationStarting** - Occurs before the web view navigates to new content. You can cancel navigation in a handler for this event by setting the WebViewNavigationStartingEventArgs.Cancel property to true. 

```csharp
webView1.NavigationStarting += webView1_NavigationStarting;

private void webView1_NavigationStarting(object sender, WebViewNavigationStartingEventArgs args)
{
    // Cancel navigation if URL is not allowed. (Implemetation of IsAllowedUri not shown.)
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

**NavigationCompleted** - Occurs when the web view has finished loading the current content or if navigation has failed. To determine whether navigation has failed, check the [IsSuccess](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webviewnavigationcompletedeventargs.issuccess.aspx) and [WebErrorStatus](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webviewnavigationcompletedeventargs.weberrorstatus.aspx) properties of the [WebViewNavigationCompletedEventArgs](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webviewnavigationcompletedeventargs.aspx) class. 

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
- [FrameNavigationStarting](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webview.framenavigationstarting.aspx) - Occurs before a frame in the web view navigates to new content. 
- [FrameContentLoading](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webview.framecontentloading.aspx) - Occurs when a frame in the web view has started loading new content. 
- [FrameDOMContentLoaded](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webview.framedomcontentloaded.aspx) - Occurs when a frame in the web view has finished parsing its current HTML content. 
- [FrameNavigationCompleted](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webview.framenavigationcompleted.aspx) - Occurs when a frame in the web view has finished loading its content. 

### Responding to potential problems

You can respond to potential problems with the content such as long running scripts, content that web view can't load, and warnings of unsafe content. 

Your app might appear unresponsive while scripts are running. The [LongRunningScriptDetected](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webview.longrunningscriptdetected.aspx) event occurs periodically while the web view executes JavaScript and provides an opportunity to interrupt the script. To determine how long the script has been running, check the [ExecutionTime](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webviewlongrunningscriptdetectedeventargs.executiontime.aspx) property of the [WebViewLongRunningScriptDetectedEventArgs](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webviewlongrunningscriptdetectedeventargs.aspx). To halt the script, set the event args [StopPageScriptExecution](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webviewlongrunningscriptdetectedeventargs.stoppagescriptexecution.aspx) property to **true**. The halted script will not execute again unless it is reloaded during a subsequent web view navigation. 

The web view control cannot host arbitrary file types. When an attempt is made to load content that the web view can't host, the [UnviewableContentIdentified](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webview.unviewablecontentidentified.aspx) event occurs. You can handle this event and notify the user, or use the [Launcher](https://msdn.microsoft.com/library/windows/apps/xaml/windows.system.launcher.aspx) class to redirect the file to an external browser or another app.

Similarly, the [UnsupportedUriSchemeIdentified](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webview.unsupportedurischemeidentified.aspx) event occurs when a URI scheme that's not supported is invoked in the web content, such as fbconnect:// or mailto://. You can handle this event to provide custom behavior instead of allowing the default system launcher to launch the URI.

The [UnsafeContentWarningDisplayingevent](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webview.unsafecontentwarningdisplaying.aspx) occurs when the web view shows a warning page for content that was reported as unsafe by the SmartScreen Filter. If the user chooses to continue the navigation, subsequent navigation to the page will not display the warning nor fire the event.

### Handling special cases for web view content

You can use the [ContainsFullScreenElement](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webview.containsfullscreenelement.aspx) property and [ContainsFullScreenElementChanged](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webview.containsfullscreenelementchanged.aspx) event to detect, respond to, and enable full-screen experiences in web content, such as full-screen video playback. For example, you may use the ContainsFullScreenElementChanged event to resize the web view to occupy the entirety of your app view, or, as the following example illustrates, put a windowed app in full screen mode when a full screen web experience is desired.

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

You can use the [NewWindowRequested](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webview.newwindowrequested.aspx) event to handle cases where hosted web content requests a new window to be displayed, such as a popup window. You can use another WebView control to display the contents of the requested window.

Use [PermissionRequested](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webview.permissionrequested.aspx) event to enable web features that require special capabilities. These currently include geolocation, IndexedDB storage, and user audio and video (for example, from a microphone or webcam). If your app accesses user location or user media, you still are required to declare this capability in the app manifest. For example, an app that uses geolocation needs the following capability declarations at minimum in Package.appxmanifest:

```xml
  <Capabilities>
    <Capability Name="internetClient" />
    <DeviceCapability Name="location" />
  </Capabilities>
```

In addition to the app handling the [PermissionRequested](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webview.permissionrequested.aspx) event, the user will have to approve standard system dialogs for apps requesting location or media capabilities in order for these features to be enabled.

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

If your app requires user input or other asynchronous operations to respond to a permission request, use the [Defer](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webviewpermissionrequest.defer.aspx) method of [WebViewPermissionRequest](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webviewpermissionrequest.aspx) to create a [WebViewDeferredPermissionRequest](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webviewdeferredpermissionrequest.aspx) that can be acted upon at a later time. See [WebViewPermissionRequest.Defer](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webviewpermissionrequest.defer.aspx). 

If users must securely log out of a website hosted in a web view, or in other cases when security is important, call the static method [ClearTemporaryWebDataAsync](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webview.cleartemporarywebdataasync.aspx) to clear out all locally cached content from a web view session. This prevents malicious users from accessing sensitive data. 

### Interacting with web view content

You can interact with the content of the web view by using the [InvokeScriptAsync](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webview.invokescriptasync.aspx) method to invoke or inject script into the web view content, and the [ScriptNotify](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webview.scriptnotify.aspx) event to get information back from the web view content.

To invoke JavaScript inside the web view content, use the [InvokeScriptAsync](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webview.invokescriptasync.aspx) method. The invoked script can return only string values. 

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

Scripts in the web view content can use **window.external.notify** with a string parameter to send information back to your app. To receive these messages, handle the [ScriptNotify](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webview.scriptnotify.aspx) event. 

To enable an external web page to fire the **ScriptNotify** event when calling window.external.notify, you must include the page's URI in the **ApplicationContentUriRules** section of the app manifest. (You can do this in Microsoft Visual Studio on the Content URIs tab of the Package.appxmanifest designer.) The URIs in this list must use HTTPS, and may contain subdomain wildcards (for example, `https://*.microsoft.com`) but they cannot contain domain wildcards (for example, `https://*.com` and `https://*.*`). The manifest requirement does not apply to content that originates from the app package, uses an ms-local-stream:// URI, or is loaded using [NavigateToString](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webview.navigatetostring.aspx). 

### Accessing the Windows Runtime in a web view

You can use the [AddWebAllowedObject](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webview.addweballowedobject.aspx) method to inject an instance of a native class from a Windows Runtime component into the JavaScript context of the web view. This allows full access to the native methods, properties, and events of that object in the JavaScript content of that web view. The class must be decorated with the [AllowForWeb](https://msdn.microsoft.com/library/windows/apps/xaml/windows.foundation.metadata.allowforwebattribute.aspx) attribute. 

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

For more info, see [WebView.AddWebAllowedObject](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webview.addweballowedobject.aspx). 

In addition, trusted JavaScript content in a web view can be allowed to directly access Windows Runtime APIs. This provides powerful native capabilities for web apps hosted in a web view. To enable this feature, the URI for trusted content must be whitelisted in the ApplicationContentUriRules of the app in Package.appxmanifest, with WindowsRuntimeAccess specifically set to "all". 

This example shows a section of the app manifest. Here, a local URI is given access to the Windows Runtime. 

```csharp
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

You can use the [WebView.Settings](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webview.settings.aspx) property (of type [WebViewSettings](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webviewsettings.aspx)) to control whether JavaScript and IndexedDB are enabled. For example, if you use a web view to display strictly static content, you might want to disable JavaScript for best performance.

### Capturing web view content

To enable sharing web view content with other apps, use the [CaptureSelectedContentToDataPackageAsync](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webview.captureselectedcontenttodatapackageasync.aspx) method, which returns the selected content as a [DataPackage](https://msdn.microsoft.com/library/windows/apps/xaml/windows.applicationmodel.datatransfer.datapackage.aspx). This method is asynchronous, so you must use a deferral to prevent your [DataRequested](https://msdn.microsoft.com/library/windows/apps/xaml/windows.applicationmodel.datatransfer.datatransfermanager.datarequested.aspx) event handler from returning before the asynchronous call is complete. 

To get a preview image of the web view's current content, use the [CapturePreviewToStreamAsync](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webview.capturepreviewtostreamasync.aspx) method. This method creates an image of the current content and writes it to the specified stream. 

### Threading behavior

By default, web view content is hosted on the UI thread on devices in the desktop device family, and off the UI thread on all other devices. You can use the [WebView.DefaultExecutionMode](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webview.defaultexecutionmode.aspx) static property to query the default threading behavior for the current client. If necessary, you can use the [WebView(WebViewExecutionMode)](https://msdn.microsoft.com/library/windows/apps/xaml/dn932036.aspx) constructor to override this behavior. 

> **Note**&nbsp;&nbsp;There might be performance issues when hosting content on the UI thread on mobile devices, so be sure to test on all target devices when you change DefaultExecutionMode.

A web view that hosts content off the UI thread is not compatible with parent controls that require gestures to propagate up from the web view control to the parent, such as [FlipView](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.flipview.aspx), [ScrollViewer](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.scrollviewer.aspx), and other related controls. These controls will not be able to receive gestures initiated in the off-thread web view. In addition, printing off-thread web content is not directly supported – you should print an element with [WebViewBrush](https://msdn.microsoft.com/library/windows/apps/xaml/windows.ui.xaml.controls.webviewbrush.aspx) fill instead.

## Recommendations


-   Make sure that the website loaded is formatted correctly for the device and uses colors, typography, and navigation that are consistent with the rest of your app.
-   Input fields should be appropriately sized. Users may not realize that they can zoom in to enter text.
-   If a web view doesn't look like the rest of your app, consider alternative controls or ways to accomplish relevant tasks. If your web view matches the rest of your app, users will see it all as one seamless experience.



## Related topics

* [WebView class](https://msdn.microsoft.com/library/windows/apps/br227702)
 

 




