---
description: Discover how to integrate a Progressive Web App (PWA) with the Windows Share feature.
title: Integrate Progressive Web Apps (PWAs) with Windows Share
ms.topic: article
ms.date: 04/16/2024
ms.localizationpriority: medium
---

# Integrate Progressive Web Apps with Windows Share

This article explains how to integrate a Progressive Web App (PWA) with the **Windows Share** feature. This feature allows users to share content from one Windows app to another. The PWA registers as a **Share Target** to receive and handle shared files within the app.

## What is Share Target?

Share Target is a feature that was introduced in Windows 8, and it allows an app to receive data from another app. Share Target works like a Clipboard but with dynamic content.

## Share content from your PWA

To share content, a PWA generates content (such as text, links, or files) and hands off the shared content to the operating system. The operating system lets the user decide which app they want to use to receive that content. PWAs can use the [Web Share API](https://developer.mozilla.org/docs/Web/API/Web_Share_API) to trigger displaying the Share Sheet in Windows. The Web Share API is supported in Microsoft Edge and other Chromium-based browsers.

See [Sharing content](/microsoft-edge/progressive-web-apps-chromium/how-to/share#sharing-content) in the Microsoft Edge documentation for a complete example of how to share content from a PWA.

## Receive shared files in your PWA

To receive content, a PWA acts as a content target. The PWA must be registered with the operating system as a content-sharing target.

The `share_target` member must contain the necessary information for the system to pass the shared content to your app. Consider the following sample manifest `share_target` configuration:

```json
"share_target": {
  "action": "./share_target_path/?custom_param=foo",
  "method": "POST",
  "enctype": "multipart/form-data",
  "params": {
    "files": [
      {
        "name": "mapped_files",
        "accept": ["image/jpeg"]
      }
    ]
  }
},
```

When your app is selected by the user as the target for shared content, the PWA is launched. A `GET` HTTP request is made to the URL specified by the `action` property. The shared data is passed as the `title`, `text`, and `url` query parameters. The following request is made: `/handle-shared-content/?title=shared title&text=shared text&url=shared url`.

The following example illustrates how to register the scoped service worker:

```javascript
navigator.serviceWorker.register('scoped-service-worker.js',
                { scope: "./share_target_path/" })
```

The service worker handles the share data as desired, and then either fulfills the request, or it can redirect the request back out of the custom path. The following example illustrates how to redirect the request back out of the custom path:

```javascript
self.addEventListener('fetch', (event) => {
    event.respondWith((async () => {
        // Read the shared data here, then
        // Redirect back out of the share_target_path to the actual site
        return Response.redirect(event.request.url.replace("share_target_path/", ""));
    })());
    return;
});
```

See the [Receiving shared content](/microsoft-edge/progressive-web-apps-chromium/how-to/share#receiving-shared-content) example in the Microsoft Edge documentation for more information.

## Performance considerations

If the fetch handler is added solely for share support, potential latency issues may arise as the requests get interrupted by the service worker. To address this issue, consider setting the `share_target` as a pseudo sub-path and registering a service worker specifically scoped to that path. This approach enables the use of a *fetch* handler for the share target without registering the same *fetch* handler for other calls.

## Sample PWA app

The [PWA logo printer](https://github.com/diekus/pwinter) sample app on GitHub demonstrates how to integrate a PWA with the Windows Share Sheet. The app allows users to print the PWA logo to a printer. The app uses the Windows Share Sheet to share the logo with other apps.

## See also

- [How to receive shared files](https://web.dev/patterns/files/receive-shared-files)
- [PWA logo printer sample app](https://github.com/diekus/pwinter)
- [Share content with other apps](/microsoft-edge/progressive-web-apps-chromium/how-to/share)
- [Web Share API](https://developer.mozilla.org/docs/Web/API/Web_Share_API)
