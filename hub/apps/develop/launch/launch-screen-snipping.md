---
title: Launch screen snipping from a Windows app (Deprecated)
description: This legacy protocol has been deprecated. Use the updated Snipping Tool protocol instead.
ms.date: 04/22/2026
ms.topic: concept-article
keywords: windows 10, uwp, uri, snip, sketch, windows 11, winui, snipping tool
ms.localizationpriority: medium
# customer-intent: As a Windows developer, I want to learn about the replacement for the ms-screenclip and ms-screensketch URI schemes for launching the Snip & Sketch app or opening a new snip.
---

# Launch screen snipping (Deprecated)

> [!IMPORTANT]
> This legacy protocol is **deprecated** and is no longer supported. Please use the updated Snipping Tool protocol documented in [Launch Snipping Tool](launch-snipping-tool.md).

The legacy URI scheme previously used to launch a new snip has been replaced by the new [Snipping Tool protocol](launch-snipping-tool.md) on May 1, 2025. The new protocol provides:

- A structured request/response model with status codes and correlation IDs
- Support for image capture, video recording, and capability discovery
- Response delivery via `redirect-uri` with file access tokens
- Customizable capture modes via the `enabledModes` parameter

## Migration

If your app used the legacy `ms-screenclip:` or `ms-screensketch:` URI schemes to launch a snip or open an image for annotation, update it to call the new Snipping Tool protocol. See [Launch Snipping Tool](launch-snipping-tool.md) for the URI format, required and optional parameters, response handling, and full examples for image capture, video recording, and capability discovery.

## Related content

- [Launch Snipping Tool](launch-snipping-tool.md)
