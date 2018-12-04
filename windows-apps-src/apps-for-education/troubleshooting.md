---
Description: Troubleshoot Microsoft Take a Test events and errors with the event viewer.
title: Troubleshoot Microsoft Take a Test with the event viewer.


ms.assetid: 9218e542-f520-4616-98fc-b113d5a08e0f
ms.date: 10/06/2017
ms.topic: article
keywords: windows 10, uwp, education
ms.localizationpriority: medium
---
# Troubleshoot Microsoft Take a Test with the event viewer

You can use the Event Viewer to view Take a Test events and errors. Take a Test logs events when a lockdown request has been received, device enrollment has succeeded, lockdown policies were successfully applied, and more.

To enable viewing events in the Event Viewer:
1. Open the `Event Viewer`
2. Navigate to `Applications and Services Logs > Microsoft > Windows > Management-SecureAssessment`
3. Right-click `Operational` and select `Enable Log`

To save the event logs:
1. Right-click `Operational`
2. Click `Save All Events As…`
