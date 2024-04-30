---
title: Feed provider package manifest XML format
description: Describes the package manifest XML format for Windows feed providers. 
ms.topic: article
ms.date: 08/12/2022
ms.author: drewbat
author: drewbatgit
ms.localizationpriority: medium
---

# Feed provider package manifest XML format

> [!NOTE]
> **Some information relates to pre-released product, which may be substantially modified before it's commercially released. Microsoft makes no warranties, express or implied, with respect to the information provided here.**

In order to be displayed on the Widgets Board, apps that support Windows feeds must register their feed provider with the system. For Win32 apps, only packaged apps are currently supported and feed providers specify their registration information in the app package manifest file. This article documents the XML format for feed registration. See the [Example](#example) section for a code listing of an example package manifest for a Win32 feed provider.

## App extension

The app package manifest file supports many different extensions and features for Windows apps. The app package manifest format is defined by a set of schemas that are documented in the [Package manifest schema reference](/uwp/schemas/appxpackage/uapmanifestschema/schema-root).  Feed providers declare their registration information within the [uap3:AppExtension](/uwp/schemas/appxpackage/uapmanifestschema/element-uap3-appextension-manual). The **Name** attribute of the extension must be set to "com.microsoft.windows.widgets.feeds".

Feed providers should include the [uap3:Properties](/uwp/schemas/appxpackage/uapmanifestschema/element-uap3-properties-manual) as the child of **uap3:AppExtension**. The package manifest schema does not enforce the structure of the **uap3:Properties** element other than requiring well-formed XML. The rest of this article describes the XML format that the Widgets Board expects in order to successfully register a feed provider.

```xml
<uap3:Extension Category="windows.appExtension">
  <uap3:AppExtension Name="com.microsoft.windows.widgets.feeds" DisplayName="ContosoApp" Id="ContosoApp" PublicFolder="Public">
    <uap3:Properties>
      <!-- Feed provider registration content goes here -->
    </uap3:Properties>
  </uap3:AppExtension>
</uap3:Extension>
```

## Element hierarchy

FeedProvider

&nbsp;&nbsp;Activation

&nbsp;&nbsp;&nbsp;&nbsp;CreateInstance

&nbsp;&nbsp;Definitions

&nbsp;&nbsp;&nbsp;&nbsp;Definition



## FeedProvider

The root element of the feed provider registration information.

| Attribute | Type | Required | Description | Default value |
|-----------|------|----------|-------------|---------------|
| **Description** | string | Yes | A short description of the feed provider. | N/A |
| **DisplayName** | string | Yes | The name of the feed provider that is displayed on the Widgets Board. | N/A |
| **Icon** | string | Yes | The package-relative path to an icon image file that is displayed in the Widgets Board. | N/A |
| **Id**| string | Yes | An ID that identifies the feed provider. Feed provider implementations use this string to determine or specify which of the app's feed providers is being referenced for each operation. This string must be unique for all feed providers defined within the app manifest file.  | N/A |


## Activation

Specifies activation information for the feed provider. 

## CreateInstance

**CreateInstance** should be specified for Win32-based feed providers that implement the **IFeedProvider** interface. The system will activate the interface with a call to [CoCreateInstance](/windows/win32/api/combaseapi/nf-combaseapi-cocreateinstance). The **ClassId** attribute specifies the [CLSID](/windows/win32/com/com-class-objects-and-clsids) for the CreateInstance server that implements the **IFeedProvider** interface. 

| Attribute | Type | Required | Description | Default value |
|---|---|---|---|---|
| **ClassId**| GUID | Yes | The CLSID for the CreateInstance server that implements the feed provider. | N/A |



## Definitions

The container element for one or more feed registrations.

## Definition

Represents the registration for a single feed.

| Attribute | Type | Required | Description | Default value |
|---|---|---|---|---|
| **Id**| string | Yes | An ID that identifies the feed. Feed provider implementations use this string to determine or specify which of the app's feeds is being referenced for each operation. This string must be unique for all feeds defined within the app manifest file.  | N/A |
| **DisplayName** | string | Yes | The name of the feed that is displayed on the Widgets Board. | N/A |
| **Description** | string | Yes | A short description of the feed. | N/A |
| **ContentUri** | string | Yes | The URI from which feed content is retrieved. | N/A |
| **Icon** | string | Yes | The package-relative path to an icon image file that is displayed in the Widgets Board. | N/A |
| **WebRequestFilter** | string | No | A web request filter string specifying the set of URLs for which the resource requests will be redirected to the feed provider's implementation of **IFeedResourceProvider**. The pattern is expressed using the format described in [Match Patterns](https://developer.mozilla.org/en-US/docs/Mozilla/Add-ons/WebExtensions/Match_patterns). The filter string in the registration must use [Punycode](https://en.wikipedia.org/wiki/Punycode) where necessary. All content types will be redirected when matched so the filter should only resolve to content intended to be obtained through the **IFeedResourceProvider** in the application. | N/A |


You can use localized resources instead of string literals for the UI-facing attribute values. For more information, see [Localize strings in your UI and app package manifest](/windows/uwp/app-resources/localize-strings-ui-manifest).

## Example

The following code example illustrates the usage of the feed package manifest XML format.

```xml
<uap3:AppExtension Name="com.microsoft.windows.widgets.feeds" DisplayName="ContosoApp" Id="ContosoApp" PublicFolder="Public">
  <uap3:Properties>
      <FeedProvider Description="ms-resource:ProviderDescription" Icon="ms-appx:Images\ContosoProviderIcon.png">
          <Activation>
              <CreateInstance ClassId="ECB883FD-3755-4E1C-BECA-D3397A3FF15C" />
          </Activation>
          <Definitions>
              <Definition Id="Contoso_Feed" DisplayName="ms-resource:FeedDisplayName"                  Description="ms-resource:FeedDescription"
                  ContentUri="https://contoso.com/news"
                  Icon="ms-appx:Images\ContosoFeedIcon.png"
                  WebRequestFilter="https://contoso.com/*/feed/appResource/*" >
              </Definition>
          </Definitions>
      </FeedProvider>
  </uap3:Properties>
</uap3:AppExtension>

```
