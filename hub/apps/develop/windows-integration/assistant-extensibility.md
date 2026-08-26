---
title: Assistant app extensibility
description: Learn how to register your packaged app as an assistant app using the com.microsoft.windows.assistantExtension manifest extension.
author: GrantMeStrength
ms.author: jken
ms.topic: article
ms.date: 07/10/2026
ms.localizationpriority: medium
---

# Assistant app extensibility

Windows allows apps to declare themselves as assistant-capable apps through the app package manifest. When an app registers as an assistant, Windows becomes aware of it and may tailor system experiences accordingly.

> [!NOTE]
> This feature requires Windows Build 26100 or later.

## Overview

Assistant app extensibility is a lightweight declaration mechanism. By registering, your app tells Windows that it provides assistant functionality. Windows may then surface your app in relevant assistant experiences.

This registration does not guarantee any specific behavior or placement. It signals to the operating system that your app is an assistant, similar in concept to how apps register as share targets or Copilot key providers.

## Register as an assistant app

Your app must be packaged to register as an assistant app. For information on app packaging, see [An overview of Package Identity in Windows apps](/windows/apps/desktop/modernize/package-identity-overview).

Declare the registration in your app package manifest file (`Package.appxmanifest`) using a [uap3:AppExtension](/uwp/schemas/appxpackage/uapmanifestschema/element-uap3-appextension-manual). Set the **Name** attribute to `com.microsoft.windows.assistantExtension`.

```xml
<Package
  ...
  xmlns:uap3="http://schemas.microsoft.com/appx/manifest/uap/windows10/3"
  ...>
  <Applications>
    <Application ...>
      ...
      <Extensions>
        <uap3:Extension Category="windows.appExtension">
          <uap3:AppExtension Name="com.microsoft.windows.assistantExtension"
            Id="MyAssistantApp"
            DisplayName="My Assistant App"
            Description="Provides assistant functionality"
            PublicFolder="Public" />
        </uap3:Extension>
      </Extensions>
      ...
    </Application>
  </Applications>
  ...
</Package>
```

The following table describes the attributes of the **uap3:AppExtension** element.

| Attribute | Description | Required |
|-----------|-------------|----------|
| **Name** | Must be set to `com.microsoft.windows.assistantExtension`. | Yes |
| **Id** | A unique identifier for the extension instance within your app. | Yes |
| **DisplayName** | The display name shown to the user. | Yes |
| **Description** | A brief description of your assistant app. | Yes |
| **PublicFolder** | A folder in your package from which content can be shared with the host. | Yes |

## Requirements

- Your app must be packaged with an MSIX package. Unpackaged apps cannot use this extension.
- Your app package must be signed. For development, a self-signed certificate is sufficient.
- The target device must be running Windows Build 26100 or later.

## What Windows does with the registration

When your app registers as an assistant app, Windows adds it to an internal list of known assistant apps. The operating system may use this information to tailor experiences — for example, by surfacing your app in contexts where assistant functionality is relevant.

No specific behavior or integration point is guaranteed by this registration alone. The registration establishes your app's intent and makes it discoverable to Windows assistant experiences as they evolve.

## See also

- [Microsoft Copilot hardware key providers](microsoft-copilot-key-provider.md)
- [An overview of Package Identity in Windows apps](/windows/apps/desktop/modernize/package-identity-overview)
- [uap3:AppExtension manifest element](/uwp/schemas/appxpackage/uapmanifestschema/element-uap3-appextension-manual)
