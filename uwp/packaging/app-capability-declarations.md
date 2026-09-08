---
title: App capability declarations
description: Find capability declaration guidance for UWP apps, including links to general-use, device, restricted, and custom capability requirements.
ms.date: 09/08/2026
ms.topic: article
keywords: windows 11, windows 10, uwp
ms.localizationpriority: medium
ms.custom: 19H1
ms.assetid: 25B18BA5-E584-4537-9F19-BB2C8C52DFE1
---

# App capability declarations

For the capability tables, manifest examples, and Microsoft Store approval requirements, see the shared [App capability declarations reference](/windows/apps/package-and-deploy/app-capability-declarations). That reference covers UWP apps as well as other packaged Windows apps.

## Which kinds of apps do app capabilities apply to?

UWP apps have package identity and run in an AppContainer. Declare the capabilities your UWP app needs to access protected APIs, resources, and devices. Packaged desktop apps that run at medium integrity level (full trust) have different requirements; packaging alone doesn't make every capability necessary. See [Which kinds of apps do app capabilities apply to?](/windows/apps/package-and-deploy/app-capability-declarations#which-kinds-of-apps-do-app-capabilities-apply-to).

## Declaring capabilities

Declare only the capabilities your app needs in `Package.appxmanifest`. For manifest editing guidance, see [Declaring capabilities](/windows/apps/package-and-deploy/app-capability-declarations#declaring-capabilities).

## Privacy-sensitive capabilities

Users can change access to sensitive resources in Windows privacy settings, so your app must handle access being unavailable. See [Privacy-sensitive capabilities](/windows/apps/package-and-deploy/app-capability-declarations#privacy-sensitive-capabilities).

## Different kinds of capabilities

Use the following links to find the requirements for each [capability category](/windows/apps/package-and-deploy/app-capability-declarations#different-kinds-of-capabilities).

### General-use capabilities

See [General-use capabilities](/windows/apps/package-and-deploy/app-capability-declarations#general-use-capabilities) for common scenarios such as libraries and networking.

### Device capabilities

See [Device capabilities](/windows/apps/package-and-deploy/app-capability-declarations#device-capabilities) for access to internal and peripheral devices.

<span id="special-and-restricted-capabilities"></span>
<span id="restricted-capability-declarations"></span>

### Restricted capabilities

See [Restricted capabilities](/windows/apps/package-and-deploy/app-capability-declarations#restricted-capabilities) for manifest declarations and Microsoft Store submission requirements. Store approval isn't required to sideload an app that declares restricted capabilities.

#### Restricted capability approval process

See [Restricted capability approval process](/windows/apps/package-and-deploy/app-capability-declarations#restricted-capability-approval-process) for Partner Center submission steps and development sandbox requirements.

#### Restricted capability list

See the [Restricted capability list](/windows/apps/package-and-deploy/app-capability-declarations#restricted-capability-list) for individual capabilities and limitations on approval.

### Custom capabilities

See [Custom capabilities](/windows/apps/package-and-deploy/app-capability-declarations#custom-capabilities) for declarations, approval requirements, and supported scenarios.

## Related topics

See [Related topics](/windows/apps/package-and-deploy/app-capability-declarations#related-topics) for submission guidance and capability manifest schema documentation.
