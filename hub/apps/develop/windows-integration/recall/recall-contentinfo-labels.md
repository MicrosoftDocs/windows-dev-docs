---
title: Provide sensitivity labels to Recall with UserActivity ContentInfo
description: Learn how to supply sensitivity label metadata through UserActivity.ContentInfo so Windows Recall can enforce enterprise DLP policies.
ms.date: 10/24/2025
ms.topic: how-to
ai-usage: ai-assisted
no-loc: [Recall, UserActivity, ContentInfo]
---

# Provide sensitivity labels to Recall with UserActivity ContentInfo

Windows Recall evaluates JSON metadata supplied through `UserActivity.ContentInfo` to decide whether to capture a window and how to classify snapshots under enterprise Data Loss Prevention (DLP) policies. By supplying sensitivity labels (when known), Recall can query the DLP provider for enforcement decisions (for example, block capture) and display appropriate metadata to users.

## When to send metadata

Send or update a `UserActivity` when:

- The active document or item changes
- A sensitivity label is added, removed, or changed
- State transitions from undetermined to known
- Your app starts up (to establish a baseline)

## Sensitivity model

Your app can report content in one of three states:

1. **Sensitive**: Include the `informationProtection` object with a `labels` array
2. **Non-sensitive**: Omit the `informationProtection` object entirely
3. **Undetermined**: Include `informationProtection.state = "undetermined"` (Recall blocks capture until the state is resolved)

The absence of the `informationProtection` object means the content is non-sensitive.

## JSON structure

Recommended top-level fields:

- `@context`: `https://schema.org`
- `@type`: For example, "DocumentObject", "Event", "Article"
- `identifier`: Stable unique ID

Information protection object (only for sensitive or undetermined) pattern (illustrative, not literal JSON):

```jsonc
"informationProtection": {
  "@type": "SensitivityLabel",
  "state": "<state>",              // "sensitive" or "undetermined"
  "labels": [                       // include only when state == "sensitive"
    {
      "labelID": "<label GUID>",
      "organizationID": "<tenant GUID>"
    }
  ]
}
```

Rules:

- Replace `<state>` with `sensitive` or `undetermined`.
- Include the `labels` array only when state is `sensitive`.
- `@type` inside object is always `SensitivityLabel`.
- Multiple labels allowed; Recall applies the most restrictive returned by DLP provider.

## Minimal examples

Sensitive (single):
```json
{
  "@context": "https://schema.org",
  "@type": "DocumentObject",
  "identifier": "doc-123",
  "informationProtection": {
    "@type": "SensitivityLabel",
    "state": "sensitive",
    "labels": [
      {
        "labelID": "F96E0B19-8C3A-4D5A-8B9A-2E8CFC43247B",
        "organizationID": "D3FE4C20-9C77-45AB-A8E7-9870D3C9C856"
      }
    ]
  }
}
```

### Non-sensitive

```json
{
  "@context": "https://schema.org",
  "@type": "DocumentObject",
  "identifier": "doc-123"
}
```

### Undetermined

```json
{
  "@context": "https://schema.org",
  "@type": "DocumentObject",
  "identifier": "doc-123",
  "informationProtection": {
    "@type": "SensitivityLabel",
    "state": "undetermined"
  }
}
```

### Multiple labels example

```json
"informationProtection": {
  "@type": "SensitivityLabel",
  "state": "sensitive",
  "labels": [
    {
      "labelID": "F96E0B19-8C3A-4D5A-8B9A-2E8CFC43247B",
      "organizationID": "D3FE4C20-9C77-45AB-A8E7-9870D3C9C856"
    },
    {
      "labelID": "9A724CF8-E7D2-4B1C-8F4A-1D2E7B3A6C8D",
      "organizationID": "7C56AB24-9E32-44FA-B7D8-1E9F43C7A92B"
    }
  ]
}
```

## API usage (C#)

### Helper method

The following helper method demonstrates how to update `ContentInfo` with sensitivity labels:

```csharp
private async Task UpdateContentInfoAsync(
    string contentId,
    string state, // "sensitive" | "undetermined" | "none"
    IEnumerable<(string LabelId, string OrgId)>? labels = null)
{
    var channel = UserActivityChannel.GetDefault();
    var activity = await channel.GetOrCreateUserActivityAsync(contentId);
    activity.ActivationUri = new Uri($"my-app://content/{contentId}");

    string json;
    if (state == "sensitive" && labels != null)
    {
        var labelItems = string.Join(",",
            labels.Select(l => $@"{{ \"labelID\": \"{l.LabelId}\", \"organizationID\": \"{l.OrgId}\" }}"));
        json = $@"{{
  \"@context\": \"https://schema.org\",
  \"@type\": \"DocumentObject\",
  \"identifier\": \"{contentId}\",
  \"informationProtection\": {{
    \"@type\": \"SensitivityLabel\",
    \"state\": \"sensitive\",
    \"labels\": [ {labelItems} ]
  }}
}}";
    }
    else if (state == "undetermined")
    {
        json = $@"{{
  \"@context\": \"https://schema.org\",
  \"@type\": \"DocumentObject\",
  \"identifier\": \"{contentId}\",
  \"informationProtection\": {{
    \"@type\": \"SensitivityLabel\",
    \"state\": \"undetermined\"
  }}
}}";
    }
    else
    {
        json = $@"{{
  \"@context\": \"https://schema.org\",
  \"@type\": \"DocumentObject\",
  \"identifier\": \"{contentId}\"
}}";
    }

    activity.ContentInfo = UserActivityContentInfo.FromJson(json);
    await activity.SaveAsync();
}
```

### Pull handler

The following pull handler demonstrates how to respond to on-demand `UserActivity` requests:

```csharp
private async void UserActivityRequested(
    UserActivityRequestManager sender,
    UserActivityRequestedEventArgs args)
{
    var deferral = args.GetDeferral();
    try
    {
        string id = GetCurrentContentId();
        var (state, labels) = GetCurrentSensitivity(); // app logic
        var channel = UserActivityChannel.GetDefault();
        var activity = await channel.GetOrCreateUserActivityAsync(id);
        activity.ActivationUri = new Uri($"my-app://content/{id}");
        string json = BuildContentInfoJson(id, state, labels);
        activity.ContentInfo = UserActivityContentInfo.FromJson(json);
        args.Request.SetUserActivity(activity);
    }
    finally
    {
        deferral.Complete();
    }
}
```

## Push vs pull

Adopt a hybrid approach:

- Initial pull establishes baseline
- Push updates immediately on changes

Benefits: low latency, avoids stale labels, reduces polling overhead, supports rapid on-demand capture.

## Related links

- [Enable relaunching your content from Recall](recall-relaunch.md)
- [Recall DLP Provider API](dlp-provider-api.md)
- [Manage Recall for Windows clients](/windows/client-management/manage-recall)
