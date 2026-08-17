---
title: Chat Assistant App extension (preview)
description: Learn how to register a packaged Windows app as a Chat Assistant App and meet the Limited Access Feature eligibility requirements.
author: GrantMeStrength
ms.author: jken
ms.topic: concept-article
ms.date: 08/17/2026
ms.localizationpriority: medium
ai-usage: ai-assisted

#customer intent: As a Windows app developer, I want to understand what qualifies my app as a Chat Assistant App and how to declare the extension so that I can register my app with Windows and meet the Limited Access Feature eligibility requirements.
---

# Chat Assistant App extension (preview) for Windows apps

A Chat Assistant App is a packaged Windows app whose primary user-facing purpose is to provide an AI-powered conversational assistant. By declaring the Chat Assistant App extension (preview) in its package manifest, an app identifies itself to Windows as a known chat assistant, so supported Windows shell experiences can tailor their behavior to chat assistants.

This article explains what qualifies an app as a Chat Assistant App, the eligibility requirements your app must meet, and how to structure the extension declaration in the package manifest.

> [!IMPORTANT]
> The Chat Assistant App extension is currently in LIMITED PREVIEW. This information relates to a prerelease feature that may be substantially modified before it's released. Microsoft makes no warranties, expressed or implied, with respect to the information provided here.

## Registration and Windows integration

The Chat Assistant App extension (preview) lets your app register with Windows as a known chat assistant. Registration is separate from integrating with individual Windows experiences, so follow the documentation and guidance for each experience you integrate with. Supported Windows shell integrations might use this registration to provide behavior tailored to chat assistants, and some integration points might apply only to Chat Assistant Apps.

## Eligibility and requirements

The Chat Assistant App extension is a Limited Access Feature (see [LimitedAccessFeatures class](/uwp/api/windows.applicationmodel.limitedaccessfeatures)). For more information or to request an unlock token, use the [LAF Access Token Request Form](https://go.microsoft.com/fwlink/?linkid=2271232&clcid=0x409).

The Limited Access Feature token attestation for the Chat Assistant App extension (preview) covers both your app's purpose and how you operate it. Request a token and declare the Chat Assistant App extension only if your app meets the following guidelines.

> [!NOTE]
> These guidelines apply to version 1 of the Chat Assistant App Limited Access Feature, which the manifest identifies by `com.microsoft.windows.chatassistant_v1`. The extension name remains `com.microsoft.windows.chatassistant`, while the Limited Access Feature identifier versions the eligibility and attestation requirements. Windows supports only the current feature version.

### Chat Assistant App purpose

A Chat Assistant App is an app whose primary user-facing purpose is to provide an AI-powered conversational assistant that responds to open-ended user requests across multiple topics or tasks.

Apps that provide AI functionality solely as a feature supporting another primary purpose, such as messaging, web browsing, search, productivity, content creation, shopping, customer support, or content consumption, aren't Chat Assistant Apps for purposes of this extension.

Person-to-person communication services aren't Chat Assistant Apps for these purposes.

### Native Windows integration

Package your app as MSIX with a package manifest, and sign it through a verified publisher. Unpackaged apps can't declare this extension. The app must provide native Windows integration rather than only wrapping a website or providing a thin Progressive Web App (PWA). For more information, see [An overview of Package Identity in Windows apps](/windows/apps/desktop/modernize/package-identity-overview).

### Privacy, safety, and responsible AI

The chat assistant must maintain publicly available documentation describing its privacy, safety, and responsible AI practices applicable to the chat assistant and provide users with access to those disclosures.

### Security

The chat assistant must not bypass operating system, app, or service security controls. Where the chat assistant accesses user data, it must do so consistent with applicable permissions and access controls. The chat assistant must protect user data and credentials transmitted over a network using industry-standard encryption.

### Accessibility

The chat assistant must support the applicable Windows accessibility features, accessibility APIs, and user accessibility settings that the Windows integration surface uses so that users can access and interact with the experience using those capabilities.

### User intent

The chat assistant must act consistent with the user's expressed intent and must not initiate actions on behalf of a user without user authorization. Where the chat assistant can perform sensitive or irreversible actions, it must provide appropriate notice and obtain user confirmation before executing those actions.

### Access revocation

Microsoft may suspend or remove a Chat Assistant App's access to Windows integration features where reasonably necessary to protect the security, integrity, accessibility, or proper functioning of Windows integration experiences, or where the Chat Assistant App no longer satisfies the applicable participation requirements.

## Register as a Chat Assistant App

Declare the Chat Assistant App extension (preview) registration in your app package manifest file (`Package.appxmanifest`) using a [`uap3:AppExtension`](/uwp/schemas/appxpackage/uapmanifestschema/element-uap3-appextension-manual). Set the **Name** attribute to `com.microsoft.windows.chatassistant`.

Include a `LimitedAccessFeatureUnlock` element in the extension properties, and replace the brace-delimited placeholder values in the following example with your extension ID, extension display name, publisher ID, and the Limited Access Feature unlock token that Microsoft provides. In the `Attestation` element, replace only `{publisher ID}` and keep the rest of the statement exactly as shown.

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
          <uap3:AppExtension
            Name="com.microsoft.windows.chatassistant"
            Id="{extension ID}"
            DisplayName="{extension display name}">
            <uap3:Properties>
              <LimitedAccessFeatureUnlock>
                <Feature>com.microsoft.windows.chatassistant_v1</Feature>
                <Attestation>{publisher ID} has registered their use of com.microsoft.windows.chatassistant_v1 with Microsoft and agrees to the terms of use.</Attestation>
                <Token>{provided token}</Token>
              </LimitedAccessFeatureUnlock>
            </uap3:Properties>
          </uap3:AppExtension>
        </uap3:Extension>
      </Extensions>
      ...
    </Application>
  </Applications>
  ...
</Package>
```

The following table describes the values in the Chat Assistant App extension declaration.

| Value | Description | Required |
|-------|-------------|----------|
| **Name** | Set this attribute to `com.microsoft.windows.chatassistant`. | Yes |
| **Id** | A unique identifier for the extension instance within your app. | Yes |
| **DisplayName** | The display name for the Chat Assistant App extension. | Yes |
| **Feature** | Set this element to `com.microsoft.windows.chatassistant_v1`. | Yes |
| **Attestation** | The required attestation statement. Replace only `{publisher ID}` with your publisher ID, and keep the remaining text exactly as shown. | Yes |
| **Token** | The Limited Access Feature unlock token that Microsoft provides. | Yes |

The attestation statement must read exactly as follows. Replace only `{publisher ID}` with your publisher ID:

```
{publisher ID} has registered their use of com.microsoft.windows.chatassistant_v1 with Microsoft and agrees to the terms of use.
```

## Related content

- [An overview of Package Identity in Windows apps](/windows/apps/desktop/modernize/package-identity-overview)
- [uap3:AppExtension manifest element](/uwp/schemas/appxpackage/uapmanifestschema/element-uap3-appextension-manual)
