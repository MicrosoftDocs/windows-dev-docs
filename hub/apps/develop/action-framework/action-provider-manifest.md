---
title: Action provider package manifest XML format
description: Describes the package manifest XML format for action providers. 
ms.topic: article
ms.date: 02/05/2024
ms.localizationpriority: medium
---

# Action provider package manifest XML format

This article describes the package manifest XML format for action providers for the Windows Copilot Action Framework.

## App extension

The app package manifest file supports many different extensions and features for Windows apps. The app package manifest format is defined by a set of schemas that are documented in the [Package manifest schema reference](/uwp/schemas/appxpackage/uapmanifestschema/schema-root).  Action providers declare their registration information within the [uap3:AppExtension](/uwp/schemas/appxpackage/uapmanifestschema/element-uap3-appextension-manual). The **Name** attribute of the extension must be set to "com.microsoft.windows.ai.actions".

Action providers should include the [uap3:Properties](/uwp/schemas/appxpackage/uapmanifestschema/element-uap3-properties-manual) as the child of **uap3:AppExtension**. The package manifest schema does not enforce the structure of the **uap3:Properties** element other than requiring well-formed XML. 

Action providers must provide a **Registration** element which specifies the path to the action definition JSON file. For more information, see [Action JSON schema for Action Framework providers](action-json.md).

```xml
<uap3:Extension Category="windows.appExtension"> 
    <uap3:AppExtension 
        Name="com.microsoft.windows.ai.actions" 
        DisplayName="..." 
        Id="..." 
        PublicFolder="Assets"> 
      <uap3:Properties> 
        <Registration>path\to\registration.json</Registration> 
      </uap3:Properties> 
    </uap3:AppExtension> 
</uap3:Extension> 

```

## Additional requirements

Both COM and URI-launched action providers must have package identity. Package identity is declare in the app package manifest file using the [Identity](/uwp/schemas/appxpackage/uapmanifestschema/element-identity) element. For more information, see [An overview of Package Identity in Windows apps](/windows/apps/desktop/modernize/package-identity-overview).

COM-based action providers must have be *full trust apps* which have an integrity level of *mediumIL*. This is declared in the app package manifest file by setting the [*uap10:TrustLevel](/uwp/schemas/appxpackage/uapmanifestschema/element-uap10-extension) attribute to "mediumIL".

URI-launched action providers must also have a trust level of *mediumIL*. If a URI-launched action provider will return outputs, the app must implement the ability to be launched for results. For more information, see [Launch an app for results](/windows/uwp/launch-resume/how-to-launch-an-app-for-results). URI-launched action providers that return outputs must also instantiate the runtime.