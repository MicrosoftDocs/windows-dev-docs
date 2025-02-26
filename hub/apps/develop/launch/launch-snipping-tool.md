---
title: Launch Snipping Tool
description: This topic describes how to use the new protocol launch framework for Snipping Tool. Your app can use these URI schemes to launch Snipping Tool's capture overlay to create a new snip or recording.
ms.date: 02/14/2025
ms.topic: concept-article
keywords: windows 11, uri, snipping tool, capture
ms.localizationpriority: medium
ms.custom: RS5
# customer-intent: As a Windows developer, I want to learn about the ms-screenclip URI scheme so that I can integrate it into my application to programmatically launch Snipping Tool. This will allow my application to trigger a snip experience directly, enabling users to take screenshots seamlessly without manually opening Snipping Tool.
---

# Launch Snipping Tool

This article specifies the protocol for integrating first and third-party applications with the Windows Snipping Tool using the **ms-screenclip:** URI (Uniform Resource Identifier) scheme. The protocol facilitates the capture of images and video (with audio) via Snipping Tool. App callers can customize and choose which Snipping Tool features their app will display. The protocol is designed to offer flexibility, security, and ease of use, aligning closely with familiar HTTP-based interactions. This shift can make the protocol more intuitive for developers and facilitate its integration with web technologies.

> [!NOTE]
> This protocol launch replaces the previous existing experience documented [here](launch-screen-snipping.md).

## Supported features

The Snipping Tool protocol supports the following features:

- Rectangle Capture
- Freeform Capture
- Window Capture
- Ability to launch directly into a Screenshot or Recording
- Customizing features available
- Autosave feature is available, but can be disabled

## Protocol Specification

**URI Scheme:** `{scheme}://{host}/{path}?{query}`

**ms-screenclip:** takes the following parameters:

- **Scheme:** `ms-screenclip` - The custom scheme for Snipping Tool's protocol.
- **Host:** Defines the Snipping Tool operation to perform (`capture` for snipping and recording, `discover` for querying capabilities).
- **Path:** Specifies the media type to capture, such as an image (`/image`) or video (`/video`).
- **Query:** Parameters for the specified schema.

## Host (required)

- Capture
- Discover

## Path (required)

- Image
- Video

## Query

### Capture Host

| **Parameter**              | **Type**      | **Required** | **Description**                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                | **Default**                                        |
| -------------------------- | ------------- | ------------ | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | -------------------------------------------------- |
| `api-version`              | string        | no           | Protocol version (e.g., `"1.0"`). Ensures compatibility with future enhancements.                                                                                                                                                                                                                                                                                                                                                                                                                              | Latest version                                     |
| `user-agent`               | string        | yes          | Identifier for the calling application, used for logging and analytics.                                                                                                                                                                                                                                                                                                                                                                                                                                        | n/a                                                |
| `x-request-correlation-id` | string        | no           | Unique identifier for requests, allowing reference to a particular transaction or event chain.                                                                                                                                                                                                                                                                                                                                                                                                                 | Generated GUID if not provided.                    |
| `host`                     | string (enum) | yes          | Specifies the **Snipping Tool action** to perform. Supported values: <br>- `capture` – Opens Snipping Tool to take a screenshot or recording. <br>- `discover` – Queries supported features.                                                                                                                                                                                                                                                                                                                   | `capture`                                          |
| `path`                     | string (enum) | yes          | Specifies the **type of media** being captured: <br>- `image` – Screenshot capture. <br>- `video` – Screen recording.                                                                                                                                                                                                                                                                                                                                                                                          | n/a                                                |
| `enabledModes`             | string (list) | no           | Controls which snipping or recording modes are **enabled** in the UI. <br> - `RectangleSnip` <br> - `WindowSnip` <br> - `FreeformSnip` <br> - `FullscreenSnip` <br> - `RectangleRecord` <br> - `SnippingAllModes` (all image modes) <br> - `RecordAllModes` (all recording modes) <br> - `All` (all supported modes)                                                                                                                                                                                                                   | Defaults to the mode specified in the URI (`path`) |
| `auto-save`                | boolean       | no           | Determines whether the captured **Screenshot or Recording** is automatically saved to the user's device. If set to `false`, the file is stored temporarily and can only be accessed using the token provided in the response. <br> **Note:** The screenshot or recording will only be automatically saved if the user has enabled the corresponding Snipping Tool settings: <br> - **"Automatically save original screenshots"** for screenshots. <br> - **"Automatically save original screen recordings"** for recordings. | `false`                                            |
| `redirect-uri`             | URI           | yes          | Callback URI where the response will be sent. The calling application must register a handler for this protocol to receive and process the response.                                                                                                                                                                                                                                                                                                                                                                                        | n/a                                                |

### Discover Host

The Snipping Tool protocol supports a `discover` endpoint that allows applications to retrieve available features, supported modes, and protocol versions dynamically. This is useful for ensuring compatibility with future updates or querying what capture methods are available.

### How to Use

To retrieve Snipping Tool's supported capabilities, use the following URI:

`ms-screenclip://discover?redirect-uri=my-snip-protocol-test-app://response`

### Response Format

The response is a JSON object containing:

- Version number (version) of the protocol.
- List of available capture paths (path), including:
  - capture/image (for snipping)
  - capture/video (for recording)
- Supported HTTP-like methods (methods).
- Available parameters (parameters) that can be used in requests.
- Description of each capability.

### Example Response

```json
{
  "version": 1.2,
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

The **enabledModes** parameter is designed to give developers granular control over the available UI options when invoking the ms-screenclip protocol. This allows for a tailored user experience that matches the specific requirements of the calling application. By specifying the **enabledModes** parameter, developers can restrict the user's choices in Snipping Tool's UI to ensure the output format meets their expectations.

### Supported modes

The **enabledModes** parameter can accept the following modes:

- **RectangleSnip:** Enables rectangle capture mode.
- **WindowSnip:** Enables window capture mode.
- **FreeformSnip:** Enables freeform capture mode.
- **FullscreenSnip:** Enables fullscreen capture mode.
- **SnippingAllModes:** Enables all snipping (image capture) modes (RectangleSnip, WindowSnip, FreeformSnip, FullscreenSnip).
- **RectangleRecord:** Enables rectangle recording mode.
- **RecordAllModes:** Enables all recording modes (currently only RectangleRecord is available).
- **All:** Enables all supported modes (RectangleSnip, WindowSnip, FreeformSnip, FullscreenSnip, RectangleRecord).

> [!IMPORTANT]
> If the **enabledModes** parameter is omitted, Snipping Tool will default to the mode explicitly specified in the URI (e.g., `rectangle`, `freeform`).
>
> - If a mode is explicitly specified in the URI (such as `rectangle`), only that mode will be available in Snipping Tool's UI.
> - If no default mode is specified in the URI, the request is considered invalid and will be ignored, even if **enabledModes** is provided.
> - The default mode parameter (e.g., `rectangle`, `freeform`) is required for **enabledModes** to function and for the request to be considered valid.

## Examples

### Example 1: Enable Only Rectangle Snip

`ms-screenclip://capture/image?rectangle&enabledModes=RectangleSnip&redirect-uri=my-snip-protocol-test-app://response`

_Explanation: This command launches Snipping Tool's overlay with only the rectangle snipping mode enabled. The user will only be able to select a rectangular region for capture._

### Example 2: Enable Rectangle Snip and Window Snip

`ms-screenclip://capture/image?rectangle&enabledModes=RectangleSnip,WindowSnip&redirect-uri=my-snip-protocol-test-app://response`

_Explanation: This command launches Snipping Tool's overlay with both the rectangle and window snipping modes enabled. The user can choose between capturing a rectangular region or an entire window._

### Example 3: Enable All Snipping Modes

`ms-screenclip://capture/image?rectangle&enabledModes=SnippingAllModes&redirect-uri=my-snip-protocol-test-app://response`

_Explanation: This command launches Snipping Tool's overlay with all supported image snipping modes (RectangleSnip, WindowSnip, FreeformSnip, FullscreenSnip). The FullScreenSnip mode is excluded from interactive mode and will not be enabled._

### Example 4: Enable Recording Mode Only

`ms-screenclip://capture/video?enabledModes=RecordAllModes&redirect-uri=my-snip-protocol-test-app://response`

_Explanation: This command launches Snipping Tool's overlay with only the recording mode enabled. The user will be limited to selecting a rectangular region for recording, with no option to take a screenshot._

### Example 5: Enable Multiple Snipping and Recording Modes

`ms-screenclip://capture/image?freeform&enabledModes=RectangleSnip,RectangleRecord&redirect-uri=my-snip-protocol-test-app://response`

_Explanation: This command launches the Snipping Tool overlay with freeform snip, rectangle snip, and rectangle recording modes available. Since freeform mode is specified in the URI, it will be pre-selected by default. Users can choose to snip in freeform, rectangle, or record the selected region._

### Example 6: Enable Rectangle Snipping Only

`ms-screenclip://capture/image?rectangle&redirect-uri=my-snip-protocol-test-app://response`

_Explanation: Since rectangle is specified in the URI, only rectangle snipping mode will be available in Snipping Tool's UI._

### Example 7: No `mode` specified

`ms-screenclip://capture/image?&enabledModes=All&redirect-uri=my-snip-protocol-test-app://response`

_Explanation: This request is invalid because it does not specify a mode (e.g., `rectangle`, `window`, or `freeform`). Even though `enabledModes=All` is provided, a default mode must always be specified. As a result, Snipping Tool will ignore the call._

## Key considerations

- **Mode Restrictions:** Developers should ensure that enabling specific modes aligns with the expected behavior of their application. Restricting UI options helps maintain a consistent user experience and ensures the resulting capture matches the application's needs.
- **Default Behavior:** If no `enabledModes` parameter is specified, only the `mode` specified in the URI will be available on the UI.

## Security Considerations

Important: Snipping Tool does not validate the redirect-uri parameter. It is the responsibility of the calling application to ensure that:

- The `redirect-uri` points to a trusted destination.
- The `redirect-uri` does not expose the captured content to unauthorized applications.
- The application handling the response sanitizes and validates all incoming parameters before processing them.
- The protocol handler for the `redirect-uri` is properly registered and secured against misuse.
- **Required Fields:** Check that all required fields are provided and valid before proceeding with the snip operation.

Response if expected parameters **aren't** supplied or overloaded.

## Responses to the caller

To ensure the response back to the caller via `redirect-uri` follows HTTP-based interaction principles, the response will mimic the structure of an HTTP response, but will be conveyed through URI query parameters. This approach keeps the interaction web-standard compliant and familiar to developers.

### Use of shared tokens

The use of the `SharedStorageAccessManager` class and of sharing tokens is subject to the following requirements and restrictions:

- A sharing token can only be redeemed once. After that, the token is no longer valid.
- A sharing token expires after 14 days and is no longer valid.
- The source app cannot provide more than 1000 sharing tokens. After a token is redeemed, removed, or expires, however, it no longer counts against the quota of the source app.

### Response parameters

The following parameters are returned in the redirect URI:

| Parameter                | Type   | Description                                                                                                                   |
| ------------------------ | ------ | ----------------------------------------------------------------------------------------------------------------------------- |
| Reason                   | String | The outcome and explanation for the snip.                                                                                     |
| Token (for success)      | String | A token representing the captured media, which the application can use to access the file.                                    |
| code                     | Int    | The HTTP status code equivalent to provide a more granular understanding of the outcome.                                      |
| x-request-correlation-id | String | A unique identifier value attached to requests and messages that allows reference to a particular transaction or event chain. |

## Retrieving a token

A sample application is available to test the process of calling the protocol and converting the response token into media. Use the [SharedStorageAccessManager](/uwp/api/windows.applicationmodel.datatransfer.sharedstorageaccessmanager) library to obtain the token.

## Status codes

The following table lists the status codes that can be returned in the redirect URI:

| HTTP Status Code | Reason                                          | Description                                             |
| ---------------- | ----------------------------------------------- | ------------------------------------------------------- |
| 200              | Success                                         | The operation was successful.                           |
| 400              | Bad Request - Invalid or Missing Parameters     | The request cannot be processed due to client error.    |
| 408              | Request Timeout - Operation Took Too Long       | The operation timed out before completion.              |
| 499              | Client Closed Request - User Cancelled the Snip | The user cancelled the snip, closing the request.       |
| 500              | Internal Server Error - Processing Failed       | An error occurred on the server, preventing completion. |

## Example responses

| Use Case           | Full URI Example                                                                                                                                                               |
| ------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| Successful Capture | `my-snip-protocol-test-app://response/?code=200&reason=Success&request-correlation-id=CF99C5A5-3907-461E-A9A9-D8AAFFAD5031&token=8930-ASDFA-ASDF4545`                          |
| Failed Capture     | `my-snip-protocol-test-app://response/?code=400&reason=Bad%20Request%20-%20Invalid%20value%20Missing%20Parameters&request-correlation-id=C7696B38-52C8-419A-880F-F3350D7A6626` |

## Caller example

Below is a table displaying examples of full URIs constructed to initiate different types of snipping sessions using the screenclip: protocol. Each URI example demonstrates a combination of parameters to illustrate how you can customize Snipping Tool's behavior for different use cases.

| Use Case       | Example URI                                                                                                          | Description                                                                                                                                             |
| -------------- | -------------------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Rectangle Mode | `ms-screenclip://capture/image?user-agent=TestApp&Rectangle&uri=my-snip-protocol-test-app://response`                | An application initiates an interactive rectangle capture session, where the user selects the capture area. The result is redirected to a specific URI. |
| Video Mode     | `ms-screenclip://capture/video?api-version=1.0&user-agent=TestApp&redirect-uri=my-snip-protocol-test-app://response` | A video capture. Always in rectangle mode.                                                                                                              |

## Related content

- [Use Snipping Tool to capture screenshots](https://support.microsoft.com/windows/use-snipping-tool-to-capture-screenshots-00246869-1843-655f-f220-97299b865f6b)
- [Launch screen snipping (Legacy)](launch-screen-snipping.md)
