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
