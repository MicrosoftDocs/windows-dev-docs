---
title: Widget provider package manifest XML format
description: Describes the package manifest XML format for Windows widget providers. 
ms.topic: article
ms.date: 08/12/2022
ms.author: drewbat
author: drewbatgit
ms.localizationpriority: medium
---

# Widget provider package manifest XML format

In order to be displayed in the widgets host, apps that support Windows feeds must register their feed provider with the system. For Win32 apps, only packaged apps are currently supported and feed providers specify their registration information in the app package manifest file. This article documents the XML format for feed registration. See the [Example](#example) section for a code listing of an example package manifest for a Win32 widget provider.

## App extension

The app package manifest file supports many different extensions and features for Windows apps. The app package manifest format is defined by a set of schemas that are documented in the [Package manifest schema reference](/uwp/schemas/appxpackage/uapmanifestschema/schema-root).  Widget providers declare their registration information within the [uap3:AppExtension](/uwp/schemas/appxpackage/uapmanifestschema/element-uap3-appextension-manual). The **Name** attribute of the extension must be set to "com.microsoft.windows.widgets".

Feed providers should include the [uap3:Properties](/uwp/schemas/appxpackage/uapmanifestschema/element-uap3-properties-manual) as the child of **uap3:AppExtension**. The package manifest schema does not enforce the structure of the **uap3:Properties** element other than requiring well-formed XML. The rest of this article describes the XML format that the Widget host expects in order to successfully register a widget provider.

```xml
<uap3:Extension Category="windows.appExtension">
  <uap3:AppExtension Name="com.microsoft.windows.widgets.feeds" DisplayName="ContosoApp" Id="ContosoApp" PublicFolder="Public">
    <uap3:Properties>
      <!-- Widget provider registration content goes here -->
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

[TBD - Image of feed picker UI with contoso]
![A screenshot of the Add Widget dialog in the Widgets board. It shows two columns of entries, each with an icon and an app name, with a plus sign indicating that a widget can be added](images/widget-picker.png)

## Activation

Specifies activation information for the feed provider. 

## CreateInstance

**CreateInstance** should be specified for Win32-based widget providers that implement the **IFeedProvider** interface. The system will activate the interface with a call to [CoCreateInstance](/windows/win32/api/combaseapi/nf-combaseapi-cocreateinstance). The **ClassId** attribute specifies the [CLSID](/windows/win32/com/com-class-objects-and-clsids) for the CreateInstance server that implements the **IWidgetProvider** interface. 

| Attribute | Type | Required | Description | Default value |
|---|---|---|---|---|
| **ClassId**| GUID | Yes | The CLSID for the CreateInstance server that implements the widget provider. | N/A |



## Definitions

The container element for one or more widget registrations.

## Definition

Represents the registration for a single widget.

| Attribute | Type | Required | Description | Default value |
|---|---|---|---|---|
| **Id**| string | Yes | An ID that identifies the feed. Feed provider implementations use this string to determine or specify which of the app's feeds is being referenced for each operation. This string must be unique for all feeds defined within the app manifest file.  | N/A |
| **DisplayName** | string | Yes | The name of the feed that is displayed on the widgets host. | N/A |
| **Description** | string | Yes | Short description of the feed. | N/A |
| **ContentUri** | string | Yes | The URI from which feed content is retrieved. | N/A |
| **Icon** | string | Yes | The package-relative path to an icon image file. that is displayed in the Widgets Board. | N/A |


## Example

The following code example illustrates the usage of the widget package manifest XML format.

```xml
<uap3:AppExtension Name="com.microsoft.windows.widgets.feeds" DisplayName="ContosoApp" Id="ContosoApp" PublicFolder="Public">
    <uap3:Properties>
        <FeedProvider Description="Contoso FeedProvider Description" SettingsUri="https://contoso.com/feeds/settings" Icon="ms-appx:Images\ContosoProviderIcon.png">
            <Activation>
                <CreateInstance ClassId="ECB883FD-3755-4E1C-BECA-D3397A3FF15C" />
            </Activation>
            <Definitions>
                <Definition Id="Contoso_Feed" DisplayName="Contoso" Description="Contoso Feed"
                            ContentUri="https://contoso.com/news"
                            Icon="ms-appx:Images\ContosoFeedIcon.png">
                </Definition>
            </Definitions>
        </FeedProvider>
    </uap3:Properties>
</uap3:AppExtension>

```
