---
title: WebView2 in WinUI 3
description: Add a WebView2 control to a WinUI 3 app to display web content using the Microsoft Edge (Chromium) rendering engine.
ms.topic: how-to
ms.date: 07/16/2026
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

### Evergreen vs Fixed Version runtime

| Consideration | Evergreen | Fixed Version |
|---|---|---|
| **Updates** | Automatic (via Microsoft Edge update) | You control updates by shipping a new version |
| **Disk footprint** | Shared with Edge; no extra disk cost | ~100–250 MB bundled with your app |
| **Best for** | Most apps, especially consumer apps | Kiosk, air-gapped, or compliance-locked environments |
| **Availability** | Pre-installed on Windows 11; Windows 10 1803+ with Nov 2022 update | Must be distributed with your installer |
| **Version control** | You cannot pin a specific version | Pin to an exact Chromium version for reproducibility |

> [!IMPORTANT]
> **Always check that the WebView2 Runtime is available at startup.** The runtime may be missing on clean Windows 10 installs, Windows Server, or LTSC editions. Use `CoreWebView2Environment.GetAvailableBrowserVersionString()` to detect availability and display a user-friendly error or redirect to the [Evergreen Bootstrapper download](/microsoft-edge/webview2/concepts/distribution#online-only-deployment) if it is not present:
>
> ```csharp
> try
> {
>     string version = Microsoft.Web.WebView2.Core.CoreWebView2Environment.GetAvailableBrowserVersionString();
>     // Runtime is available — proceed with WebView2 initialization
> }
> catch (Microsoft.Web.WebView2.Core.WebView2RuntimeNotFoundException)
> {
>     // Runtime not found — prompt the user to install it
> }
> ```

## Threading model

WebView2 uses a single-threaded apartment (STA) model. You must create and interact with the WebView2 control on the UI thread.

> [!IMPORTANT]
> **Do not block the UI thread** while waiting for WebView2 operations to complete. Methods like `EnsureCoreWebView2Async` and `ExecuteScriptAsync` are asynchronous — use `await` and never call `.Result` or `.Wait()` on these tasks. Blocking the UI thread causes deadlocks because WebView2's internal message pump cannot process responses while the thread is blocked.

Key threading rules:

- Create `WebView2` on the UI thread (the XAML thread in WinUI 3).
- All property access and method calls on `CoreWebView2` must happen on the same thread that created it.
- Event handlers (`NavigationStarting`, `WebMessageReceived`, etc.) are dispatched on the UI thread.
- If you need to update the UI from a background thread, use `DispatcherQueue.TryEnqueue` to marshal back to the UI thread.

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

## Communicate between web and app code

Use `WebMessageReceived` to receive messages from the web page, and `PostWebMessageAsString` or `PostWebMessageAsJson` to send messages from your app to the web page:

```csharp
await MyWebView2.EnsureCoreWebView2Async();

// Receive messages from the web page
MyWebView2.CoreWebView2.WebMessageReceived += (sender, args) =>
{
    string message = args.TryGetWebMessageAsString();
    // Process message from web content
};

// Send a message to the web page (after it has loaded and set up its listener)
MyWebView2.CoreWebView2.PostWebMessageAsString("Hello from the app!");
```

In your web page, use `window.chrome.webview.postMessage` to send messages to the app:

```html
<script>
    // Receive messages from the host app
    window.chrome.webview.addEventListener("message", (event) => {
        console.log("Message from app:", event.data);
    });

    // Send a message to the host app
    window.chrome.webview.postMessage("Hello from the web page!");
</script>
```

## Security best practices

When embedding web content in a desktop app, the web content runs with access to the WebView2 messaging channel. Follow these practices to avoid security issues:

- **Restrict navigation to trusted origins.** Use the `NavigationStarting` event to block navigation to unexpected URLs. If your app only needs to display content from specific domains, maintain an allowlist:

    ```csharp
    MyWebView2.NavigationStarting += (sender, args) =>
    {
        var uri = new Uri(args.Uri);
        if (uri.Host != "learn.microsoft.com" && uri.Host != "example.com")
        {
            args.Cancel = true; // Block navigation to untrusted origins
        }
    };
    ```

- **Prefer `PostWebMessageAsJson` over `PostWebMessageAsString`.** JSON messages are structured and easier to validate on both sides. String messages risk injection if concatenated into JavaScript without escaping.

- **Validate incoming web messages.** Never trust `WebMessageReceived` data without validation. Treat web content as untrusted input:

    ```csharp
    MyWebView2.CoreWebView2.WebMessageReceived += (sender, args) =>
    {
        string json = args.WebMessageAsJson;
        // Parse and validate JSON structure before acting on it
    };
    ```

- **Avoid `ExecuteScriptAsync` with user-provided strings.** If you must pass dynamic values to JavaScript, use `PostWebMessageAsJson` instead of string-interpolating values into script code, which risks script injection.

- **Disable features you don't need.** Use `CoreWebView2Settings` to disable DevTools, context menus, or status bar if they aren't needed by your scenario:

    ```csharp
    await MyWebView2.EnsureCoreWebView2Async();
    MyWebView2.CoreWebView2.Settings.AreDevToolsEnabled = false;
    MyWebView2.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;
    ```

## Hosting differences: WinUI 3 vs WinForms vs WPF vs Win32

The WebView2 control is available across multiple frameworks. Each uses a different wrapper:

| Framework | Package / control | Notes |
|---|---|---|
| **WinUI 3** | Built-in `Microsoft.UI.Xaml.Controls.WebView2` | Ships with Windows App SDK; uses `DispatcherQueue` for async |
| **WPF** | `Microsoft.Web.WebView2.Wpf` (NuGet) | Uses WPF `Dispatcher`; supports XAML data binding |
| **WinForms** | `Microsoft.Web.WebView2.WinForms` (NuGet) | Drag-and-drop designer support; uses `SynchronizationContext` |
| **Win32 (C++)** | `Microsoft.Web.WebView2` (NuGet) + COM APIs | Most control; requires manual message pump management |

> [!NOTE]
> All frameworks use the same underlying `CoreWebView2` COM object. The hosting wrapper handles thread marshaling and lifecycle management differently. If you are migrating between frameworks, the `CoreWebView2` API surface stays the same — only the hosting and initialization pattern changes.

## Common mistakes

> [!TIP]
> **Mistakes frequently produced by LLMs and code generators:**
>
> 1. **Not awaiting `EnsureCoreWebView2Async`** — accessing `CoreWebView2` before initialization completes returns `null` and throws `NullReferenceException`.
> 2. **Blocking on async calls** — calling `.Result` or `.Wait()` on WebView2 tasks deadlocks the UI thread.
> 3. **Assuming the runtime is always present** — the WebView2 Runtime may not exist on Windows Server, LTSC, or clean Windows 10 installs. Always check with `GetAvailableBrowserVersionString()`.
> 4. **Creating WebView2 on a background thread** — the control must be created on the UI/STA thread. Creating it on a thread pool thread silently fails or throws.
> 5. **String-interpolating user input into `ExecuteScriptAsync`** — this is a script injection vulnerability. Use `PostWebMessageAsJson` for app-to-web communication with dynamic data.
> 6. **Navigating before `CoreWebView2` is ready** — setting `Source` in XAML works (the control queues it), but calling `CoreWebView2.Navigate()` in code before `EnsureCoreWebView2Async` completes throws.

## Open the WinUI 3 Gallery

> [!div class="nextstepaction"]
> [Open the WinUI 3 Gallery app and see WebView2 in action](winui3gallery://item/WebView2)

The [WinUI 3 Gallery](https://apps.microsoft.com/detail/9P3JFPWWDZRC) app includes interactive examples of most WinUI 3 controls and features. You can also browse the [WinUI-Gallery source on GitHub](https://github.com/microsoft/WinUI-Gallery).

## Authentication and SSO in WebView2

WebView2 stores cookies and other profile data in an app-specific user data folder by default, so users stay signed in across app launches. To isolate authentication state (for example, to support multiple accounts), create a separate user data folder:

```csharp
var env = await CoreWebView2Environment.CreateAsync(
    userDataFolder: Path.Combine(
        ApplicationData.Current.LocalFolder.Path, "WebView2_UserA"));
await MyWebView2.EnsureCoreWebView2Async(env);
```

> [!NOTE]
> This example requires package identity (MSIX). For unpackaged apps, use `Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)` instead of `ApplicationData.Current.LocalFolder.Path`.

> [!IMPORTANT]
> Do not use WebView2 to display OAuth login pages and then scrape tokens from redirect URLs, navigation events, or cookies. This is the "embedded browser" anti-pattern that many identity providers actively block. For OAuth flows in desktop apps, use [OAuth2Manager](../../security/oauth2.md) or [MSAL.NET with WAM](/entra/msal/dotnet/acquiring-tokens/desktop-mobile/wam), which use the system browser or OS-level broker. Reserve WebView2 for hosting your own authenticated web content after the user has already obtained tokens through a proper flow.

## Related articles

- [Microsoft Edge WebView2 overview](/microsoft-edge/webview2/)
- [Get started with WebView2 in WinUI 3 apps](/microsoft-edge/webview2/get-started/winui)
- [WebView2 distribution and deployment](/microsoft-edge/webview2/concepts/distribution)
- [WebView2Samples on GitHub](https://github.com/MicrosoftEdge/WebView2Samples) — official samples for WebView2 across frameworks
- [WebView2 API reference (Microsoft.Web.WebView2.Core)](/microsoft-edge/webview2/reference/winrt/microsoft_web_webview2_core/)
- [Communicate between web content and host app](/microsoft-edge/webview2/concepts/overview-features-apis#web-messaging)
- [WebView2 environment options](/microsoft-edge/webview2/concepts/overview-features-apis#webview2-environment-options)
