---
title: Launch Snipping Tool
description: This topic describes how to use the protocol launch framework for Snipping Tool. Your app can use these URI schemes to launch Snipping Tool's capture overlay to create a new snip or recording.
ms.date: 04/22/2026
ms.topic: concept-article
keywords: windows 11, uri, snipping tool, capture
ms.localizationpriority: medium
# customer-intent: As a Windows developer, I want to learn about the ms-screenclip URI scheme so that I can integrate it into my application to programmatically launch Snipping Tool.
---

# Launch Snipping Tool

This article specifies the protocol for integrating first and third-party applications with the Windows Snipping Tool using the **ms-screenclip:** URI (Uniform Resource Identifier) scheme. The protocol supports the capture of images and video (with audio) via Snipping Tool, and app callers can choose which Snipping Tool features their app will display.

> [!IMPORTANT]
> This protocol requires a **packaged Windows app** (MSIX). When your app is packaged, the operating system automatically provides your app's identity to Snipping Tool, which uses it to securely route the capture response back to your app. Unpackaged (Win32) callers cannot receive responses via `redirect-uri`. If an unpackaged app provides a `redirect-uri`, Snipping Tool will not deliver the response and may exit without showing the capture UI.

> [!NOTE]
> This protocol replaces the experience documented in [Launch screen snipping (Deprecated)](launch-screen-snipping.md), which is now deprecated.

## Supported features

The Snipping Tool protocol supports the following features:

- Rectangle Capture
- Freeform Capture
- Window Capture
- Screen Recording
- Customizing available capture modes
- Auto-save (optional)

## Protocol specification

**URI Format:** `ms-screenclip://{host}/{path}?{query parameters}`

| Component | Description | Values |
|-----------|-------------|--------|
| **Scheme** | The custom scheme for Snipping Tool | `ms-screenclip` |
| **Host** | The Snipping Tool operation to perform | `capture` or `discover` |
| **Path** | The media type to capture (applies to the `capture` host only; the `discover` host has no path) | `/image` or `/video` |
| **Query** | Parameters for the operation | See tables below |

> [!NOTE]
> Paths and query parameter names are case-insensitive. For example, `ms-screenclip://capture/Image?Redirect-Uri=my-app://response` behaves the same as `ms-screenclip://capture/image?redirect-uri=my-app://response`.

## Capture host

Use the `capture` host to launch Snipping Tool's capture overlay.

### Path

| Path | Description |
|------|-------------|
| `/image` | Launches image capture (screenshot). Requires a mode parameter. |
| `/video` | Launches video capture (screen recording). Always uses rectangle mode. |

### Mode parameters (capture/image)

For the `/image` path, you must specify exactly **one** mode parameter. Mode parameters are bare query parameters with **no value**.

| Parameter | Description |
|-----------|-------------|
| `rectangle` | Interactive rectangle capture mode. |
| `freeform` | Interactive freeform capture mode. |
| `window` | Interactive window capture mode. |

> [!IMPORTANT]
> Mode parameters must be specified without a value. For example, use `&rectangle`, **not** `&rectangle=value`. Providing a value will result in an error response.
>
> For `/image`, you must specify exactly one mode parameter. Specifying zero or more than one mode will result in a `400 Bad Request` error response. For `/video`, any mode parameter is ignored.

### Query parameters (capture)

> [!NOTE]
> Query parameters may be provided in any order.

| Parameter | Type | Required | Description | Default |
|-----------|------|----------|-------------|---------|
| `redirect-uri` | URI | Yes | Callback URI where Snipping Tool sends the capture response. Your app must register a protocol handler for this URI scheme. If omitted, Snipping Tool doesn't display the capture UI and doesn't return a response. | n/a |
| `user-agent` | string | No (strongly recommended) | Identifier for the calling application, used for logging and analytics. Required to diagnose issues via support channels; omit at your own risk. | n/a |
| `api-version` | string | No | Protocol version to use, for example `"1.2"`. If omitted, the request is processed as version `1.2`. | `1.2` |
| `x-request-correlation-id` | string | No | Unique identifier for the request, allowing reference to a particular transaction or event chain. | Auto-generated GUID |
| `enabledModes` | string (list) | No | Controls which capture modes are available in the UI. See [EnabledModes](#enabledmodes) below. | Only the mode specified in the URI |
| `auto-save` | flag | No | When present, the captured screenshot or recording is automatically saved to the user's device. | Not present (no auto-save) |

> [!NOTE]
> The default `api-version` of `1.2` doesn't change when newer protocol versions are released. Requests that omit `api-version` are always processed as `1.2`. To use features added in a later version, set `api-version` to that version. We recommend specifying `api-version` explicitly in every request so your app stays tied to a known protocol version rather than the implicit default.

> [!NOTE]
> When you supply `api-version`, it must exactly match one of the values in the `/discover` response's `supportedVersions` array (currently `1.0`, `1.1`, and `1.2`). Any other value — including intermediate values like `1.15` or malformed values like `1.0abc` — returns a `400 Bad Request` response. To discover the set of versions a specific Snipping Tool build accepts, call the [discover host](#discover-host).

> [!NOTE]
> The `auto-save` flag respects the user's Snipping Tool settings. If the user has disabled automatic saving in Snipping Tool, the capture isn't saved to the device even when your request includes `auto-save`.

## Discover host

Use the `discover` host to query Snipping Tool's supported capabilities, modes, and protocol version at runtime. This is useful for checking compatibility before making a capture request.

### Query parameters (discover)

| Parameter | Type | Required | Description | Default |
|-----------|------|----------|-------------|---------|
| `redirect-uri` | URI | Yes | Callback URI where Snipping Tool sends the capabilities response. Your app must register a protocol handler for this URI scheme. If omitted, Snipping Tool doesn't return a response. | n/a |
| `user-agent` | string | No (strongly recommended) | Identifier for the calling application, used for logging and analytics. | n/a |
| `x-request-correlation-id` | string | No | Unique identifier for the request. | Auto-generated GUID |

### Discover example

```
ms-screenclip://discover?user-agent=MyApp&redirect-uri=my-app://response
```

### Discover response format

The response is a JSON object appended to the redirect URI as the `discover` query parameter. It contains:

- `version`: Latest protocol version this Snipping Tool build supports.
- `defaultVersion`: Protocol version assumed when a request omits `api-version`. Read this to understand how your unpinned requests are interpreted.
- `supportedVersions`: Array of protocol versions this Snipping Tool build accepts.
- `capabilities`: Array of supported capture operations, each with:
  - `path`: The capture endpoint (e.g., `capture/image`, `capture/video`).
  - `methods`: Supported HTTP-like methods.
  - `parameters`: Available parameters for the endpoint.
  - `description`: Description of the capability.

```json
{
  "version": 1.2,
  "defaultVersion": 1.2,
  "supportedVersions": [1.0, 1.1, 1.2],
  "capabilities": [
    {
      "path": "capture/image",
      "methods": ["GET"],
      "parameters": ["rectangle", "freeform", "window"],
      "description": "Captures an image with options for shape."
    },
    {
      "path": "capture/video",
      "methods": ["GET"],
      "parameters": [],
      "description": "Captures a video in a defined area."
    }
  ]
}
```

## EnabledModes

The `enabledModes` parameter lets you control which capture modes are available in the Snipping Tool UI. Use it to restrict or expand the user's choices to match your application's requirements.

### Supported modes

| Mode | Description |
|------|-------------|
| `RectangleSnip` | Rectangle capture mode. |
| `WindowSnip` | Window capture mode. |
| `FreeformSnip` | Freeform capture mode. |
| `FullscreenSnip` | Fullscreen capture mode. |
| `SnippingAllModes` | All image capture modes: `RectangleSnip`, `WindowSnip`, `FreeformSnip`, `FullscreenSnip`. |
| `RectangleRecord` | Rectangle recording mode. |
| `RecordAllModes` | All recording modes: currently `RectangleRecord` only. |
| `All` | All supported modes: the union of `SnippingAllModes` and `RecordAllModes`. |

> [!TIP]
> `All`, `SnippingAllModes`, and `RecordAllModes` are aggregate values. The modes they include can change across Snipping Tool releases. An app that uses one of these values automatically picks up modes added in future releases. To keep the set of available modes fixed across updates, list the specific modes explicitly (for example, `RectangleSnip,FreeformSnip`).

> [!IMPORTANT]
> - For `/image`, a mode parameter (e.g., `rectangle`, `freeform`, `window`) is **required** in the URI, even when `enabledModes` is specified. The mode parameter determines the initially selected mode.
> - The mode specified in the URI is always available in the UI, even if it isn't listed in `enabledModes`. For example, `?freeform&enabledModes=RectangleSnip` makes both freeform (from the URI) and rectangle snip available, with freeform pre-selected.
> - If `enabledModes` is omitted, only the mode specified in the URI will be available in the UI.
> - For `/image`, if no mode parameter is specified, the request is invalid and will result in an error, regardless of `enabledModes`.

### EnabledModes examples

**Enable only rectangle snip:**

```
ms-screenclip://capture/image?rectangle&enabledModes=RectangleSnip&user-agent=MyApp&redirect-uri=my-app://response
```

**Enable rectangle and window snip:**

```
ms-screenclip://capture/image?rectangle&enabledModes=RectangleSnip,WindowSnip&user-agent=MyApp&redirect-uri=my-app://response
```

**Enable all snipping modes:**

```
ms-screenclip://capture/image?rectangle&enabledModes=SnippingAllModes&user-agent=MyApp&redirect-uri=my-app://response
```

**Enable recording mode only:**

```
ms-screenclip://capture/video?enabledModes=RecordAllModes&user-agent=MyApp&redirect-uri=my-app://response
```

**Enable multiple snipping and recording modes:**

```
ms-screenclip://capture/image?freeform&enabledModes=RectangleSnip,RectangleRecord&user-agent=MyApp&redirect-uri=my-app://response
```

_Since freeform is specified in the URI, it will be pre-selected. Users can switch between freeform, rectangle snip, and rectangle recording._

## Responses

After the user completes or cancels a capture, Snipping Tool sends a response back to your application via the `redirect-uri`. The response is structured as URI query parameters appended to your redirect URI.

If your `redirect-uri` already includes query parameters (for example, `my-app://response?sessionId=abc`), those parameters are preserved and the response parameters are appended with `&`. You can use this to round-trip caller-specific state through the callback — the value `sessionId=abc` is echoed back in the response URI along with `code`, `reason`, `x-request-correlation-id`, and (for a successful capture) `file-access-token`.

### Response parameters

| Parameter | Type | Present | Description |
|-----------|------|---------|-------------|
| `code` | int | Always | HTTP-style status code indicating the outcome. |
| `reason` | string | Always | Human-readable description of the outcome. |
| `x-request-correlation-id` | string | Always | The correlation ID from the original request (or an auto-generated one). |
| `file-access-token` | string | Success only | A `SharedStorageAccessManager` token representing the captured media. Use this to retrieve the file. |
| `discover` | string | Discover only | URL-encoded JSON containing the capabilities response. |

### Status codes

| Code | Reason | Description |
|------|--------|-------------|
| 200 | Success | The capture completed successfully. A `file-access-token` is included in the response. |
| 400 | Bad Request - Invalid or Missing Parameters | The request could not be processed. Check that all required parameters are present and valid. |
| 408 | Request Timeout - Operation Took Too Long | The operation timed out before completion. |
| 499 | Client Closed Request - User Cancelled the Snip | The user cancelled the capture by pressing Escape or clicking away. Applies to `/image` and `/video` only. |
| 500 | Internal Server Error - Processing Failed | An unexpected error occurred during capture. |

### Example responses

**Successful capture:**

```
my-app://response?code=200&reason=Success&x-request-correlation-id=aaaa0000-bb11-2222-33cc-444444dddddd&file-access-token=cccc2222-dd33-4444-55ee-666666ffffff
```

**User cancelled:**

```
my-app://response?code=499&reason=Client%20Closed%20Request%20-%20User%20Cancelled%20the%20Snip&x-request-correlation-id=bbbb1111-cc22-3333-44dd-555555eeeeee
```

**Invalid request (missing mode parameter):**

```
my-app://response?code=400&reason=Bad%20Request%20-%20Invalid%20or%20Missing%20Parameters&x-request-correlation-id=bbbb1111-cc22-3333-44dd-555555eeeeee
```

## Full URI examples

| Use Case | URI | Description |
|----------|-----|-------------|
| Rectangle screenshot | `ms-screenclip://capture/image?rectangle&user-agent=MyApp&redirect-uri=my-app://response` | Interactive rectangle capture. Result returned to caller. |
| Freeform screenshot | `ms-screenclip://capture/image?freeform&user-agent=MyApp&redirect-uri=my-app://response` | Interactive freeform capture. Result returned to caller. |
| Window screenshot | `ms-screenclip://capture/image?window&user-agent=MyApp&redirect-uri=my-app://response` | Interactive window capture. Result returned to caller. |
| Screen recording | `ms-screenclip://capture/video?user-agent=MyApp&redirect-uri=my-app://response` | Interactive screen recording. Result returned to caller. |
| Discover capabilities | `ms-screenclip://discover?user-agent=MyApp&redirect-uri=my-app://response` | Query supported features. Capabilities JSON returned to caller. |
| Rectangle with auto-save | `ms-screenclip://capture/image?rectangle&auto-save&user-agent=MyApp&redirect-uri=my-app://response` | Rectangle capture with auto-save enabled. |
| Rectangle with all modes | `ms-screenclip://capture/image?rectangle&enabledModes=All&user-agent=MyApp&redirect-uri=my-app://response` | Rectangle capture pre-selected, all modes available in UI. |

## Launching from your app

You must use [Launcher.LaunchUriAsync](/uwp/api/windows.system.launcher.launchuriasync) to launch Snipping Tool from your packaged app. Other launch methods (such as `Process.Start` or shell execution) will not provide your app's identity, and Snipping Tool will not deliver the response.

### Step 1: Register a protocol handler

Register a custom protocol in your `Package.appxmanifest` so your app can receive the callback response. The protocol name must match the scheme used in your `redirect-uri`.

```xml
<Extensions>
  <uap:Extension Category="windows.protocol">
    <uap:Protocol Name="my-app" DesiredView="default">
      <uap:DisplayName>My App Protocol</uap:DisplayName>
    </uap:Protocol>
  </uap:Extension>
</Extensions>
```

See [Handle URI activation](/windows/apps/windows-app-sdk/migrate-to-windows-app-sdk/guides/applifecycle#protocol-activation) for more details on registering and handling protocol activations.

### Step 2: Launch Snipping Tool

```csharp
// Capture a screenshot in rectangle mode
var uri = new Uri(
    "ms-screenclip://capture/image"
    + "?rectangle"
    + "&user-agent=MyApp"
    + "&redirect-uri=my-app://capture-response"
    + "&x-request-correlation-id=" + Guid.NewGuid().ToString()
);
await Launcher.LaunchUriAsync(uri);
```

```csharp
// Record a video
var uri = new Uri(
    "ms-screenclip://capture/video"
    + "?user-agent=MyApp"
    + "&redirect-uri=my-app://capture-response"
);
await Launcher.LaunchUriAsync(uri);
```

```csharp
// Discover capabilities (returns immediately, no capture UI)
var uri = new Uri(
    "ms-screenclip://discover"
    + "?user-agent=MyApp"
    + "&redirect-uri=my-app://discover-response"
);
await Launcher.LaunchUriAsync(uri);
```

### Step 3: Handle the response

When the capture completes (or the user cancels), Snipping Tool activates your app via your `redirect-uri` with result parameters appended as query strings. Most integrations are already running when the response arrives — the caller launched Snipping Tool, then waited for the callback — so your app must handle both cold-start activation (the app wasn't running) and warm re-activation (the app is already running). Subscribe to both paths in `App.xaml.cs`.

**Handle a capture response (image or video):**

```csharp
// In App.xaml.cs: handle protocol activation for both cold-start and warm re-activation
protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
{
    // Cold-start path: the app was launched by Snipping Tool's callback.
    var activatedArgs = Microsoft.Windows.AppLifecycle.AppInstance.GetCurrent().GetActivatedEventArgs();
    if (activatedArgs.Kind == Microsoft.Windows.AppLifecycle.ExtendedActivationKind.Protocol)
    {
        if (activatedArgs.Data is Windows.ApplicationModel.Activation.IProtocolActivatedEventArgs protocolArgs)
        {
            _ = HandleProtocolActivationAsync(protocolArgs.Uri);
        }
    }

    // Warm re-activation path: the app is already running when the callback arrives.
    Microsoft.Windows.AppLifecycle.AppInstance.GetCurrent().Activated += (sender, e) =>
    {
        if (e.Kind == Microsoft.Windows.AppLifecycle.ExtendedActivationKind.Protocol &&
            e.Data is Windows.ApplicationModel.Activation.IProtocolActivatedEventArgs protocolArgs)
        {
            _ = HandleProtocolActivationAsync(protocolArgs.Uri);
        }
    };
}

private async Task HandleProtocolActivationAsync(Uri uri)
{
    var query = new WwwFormUrlDecoder(uri.Query);

    var code = query.GetFirstValueByName("code");
    var reason = query.GetFirstValueByName("reason");

    if (code == "200")
    {
        var token = query.GetFirstValueByName("file-access-token");
        var file = await SharedStorageAccessManager.RedeemTokenForFileAsync(token);

        // Use the captured file (see "Retrieving captured media" below)
    }
    else
    {
        // Handle error (400, 408, 499, 500)
        Debug.WriteLine($"Snipping Tool returned {code}: {reason}");
    }
}
```

**Handle a discover response:**

```csharp
private void HandleDiscoverResponse(Uri uri)
{
    var query = new WwwFormUrlDecoder(uri.Query);

    var code = query.GetFirstValueByName("code");

    if (code == "200")
    {
        var discover = query.GetFirstValueByName("discover");
        // discover contains a URL-encoded JSON capabilities payload
        var capabilities = Uri.UnescapeDataString(discover);
        // Parse the JSON to inspect supported capture modes
    }
}
```

> [!TIP]
> If you sent an `x-request-correlation-id` with the request, verify that the response echoes the same value so you can match the response to the correct in-flight request. If you let Snipping Tool auto-generate one, the response carries the generated value — treat it as matching your most recent in-flight request.

### Retrieving captured media using the token

Use the [SharedStorageAccessManager](/uwp/api/windows.applicationmodel.datatransfer.sharedstorageaccessmanager) class to redeem the `file-access-token` and access the captured file.

**Token restrictions:**

- A token can only be redeemed **once**. After redemption, it is no longer valid.
- A token expires after **14 days**.
- An app cannot have more than **1000** active tokens. After a token is redeemed, removed, or expires, it no longer counts against the quota.

```csharp
// Redeem the token and display the captured image
var file = await SharedStorageAccessManager.RedeemTokenForFileAsync(token);

using (var stream = await file.OpenReadAsync())
{
    var bitmap = new BitmapImage();
    await bitmap.SetSourceAsync(stream);
    MyImage.Source = bitmap;
}

// Or copy to your app's local storage
var localFolder = ApplicationData.Current.LocalFolder;
await file.CopyAsync(localFolder, file.Name, NameCollisionOption.GenerateUniqueName);
```

## Security considerations

Snipping Tool validates all `redirect-uri` values before launching them. The following protections are enforced:

- **Packaged app callers**: When your app is a packaged Windows app (MSIX), the operating system securely routes the capture response back to your app, ensuring only your app can receive it. This is the recommended integration path.
- **Input validation**: Snipping Tool rejects redirect URIs that contain UNC paths, leading/trailing whitespace, or control characters.
- **No fragments**: Redirect URIs that contain a URL fragment (for example, `my-app://response#section`) are rejected. Snipping Tool appends the response parameters as a query string, and a fragment would swallow them.
- **Self-referencing protection**: Redirect URIs that would cause recursive activation of Snipping Tool are blocked.

> [!IMPORTANT]
> **For calling applications:**
>
> - Register a protocol handler for your redirect URI scheme so your app can receive the response.
> - Validate and sanitize all parameters received in the response before processing them.
> - Verify that the response's `x-request-correlation-id` matches your in-flight request to avoid handling a stale response or mixing up concurrent requests. Correlation-id prevents mix-ups; it doesn't establish token provenance — secure token routing comes from the packaged-app callback channel.

## Related content

- [Handle URI activation](/windows/apps/windows-app-sdk/migrate-to-windows-app-sdk/guides/applifecycle#protocol-activation)
- [SharedStorageAccessManager class](/uwp/api/windows.applicationmodel.datatransfer.sharedstorageaccessmanager)
- [Use Snipping Tool to capture screenshots](https://support.microsoft.com/windows/use-snipping-tool-to-capture-screenshots-00246869-1843-655f-f220-97299b865f6b)
