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

> [!NOTE]
> **Some information relates to pre-released product, which may be substantially modified before it's commercially released. Microsoft makes no warranties, express or implied, with respect to the information provided here.**
> [!IMPORTANT]
> The self-contained feature described in this topic is available only in Windows App SDK 1.2 Preview 1.

In order to be displayed in the Widget board, apps that support Windows widgets must register their widget provider with the system. For Win32 apps, only packaged apps are currently supported and widget providers specify their registration information in the app package manifest file. This article documents the XML format for widget registration. See the [Example](#example) section for a code listing of an example package manifest for a Win32 widget provider.

## App extension

The app package manifest file supports many different extensions and features for Windows apps. The app package manifest format is defined by a set of schemas that are documented in the [Package manifest schema reference](/uwp/schemas/appxpackage/uapmanifestschema/schema-root).  Widget providers declare their registration information within the [uap3:AppExtension](/uwp/schemas/appxpackage/uapmanifestschema/element-uap3-appextension-manual). The **Name** attribute of the extension must be set to "com.microsoft.windows.widgets".

Widget providers should include the [uap3:Properties](/uwp/schemas/appxpackage/uapmanifestschema/elemnt-uap3-properties-manual) as the child of **uap3:AppExtension**. The package manifest schema does not enforce the structure of the **uap3:Properties** element other than requiring well-formed XML. The rest of this article describes the XML format that the Widget board expects in order to successfully register a widget provider.

```xml
<uap3:Extension Category="windows.appExtension">
  <uap3:AppExtension Name="com.microsoft.windows.widgets" DisplayName="WidgetTestApp" Id="ContosoWidgetApp" PublicFolder="Public">
    <uap3:Properties>
    <!-- Widget provider registration content goes here -->
    </uap3:Properties>
  </uap3:AppExtension>
</uap3:Extension>
```

## Element hierarchy

WidgetProvider
> Activation
> > COM
> Widgets
> > Widget
> > > WidgetCapabilities
> > > > WidgetCapability
> > > WidgetThemeResources
> > > > Icon
> > > > Screenshots
> > > > > Screenshot
> > > > DarkMode
> > > > > Icon
> > > > > Screenshots
> > > > > > Screenshot
> > > > LightMode
> > > > > Icon
> > > > > Screenshots
> > > > > > Screenshot



## WidgetProvider

The root element of the widget provider registration information.

| Attribute | Type | Required | Description | Default value |
|---|---|---|---|---|
| **Icon**| string | Yes | The package-relative path to an icon image file. This icon is shown in the widget board in the **Add Widgets dialog** | N/A |

![A screenshot of the Add Widget dialog in the widget board. It shows two columns of entries, each with an icon and an app name, with a plus sign indicating that a widget can be added](images/widget-picker.png)

## Activation

Specifies activation information for the widget provider.

## COM

Specifies the [CLSID](/windows/win32/com/com-class-objects-and-clsids) for the COM server that implements the widget provider.

| Attribute | Type | Required | Description | Default value |
|---|---|---|---|---|
| **ClassId**| GUID | Yes | TBD P | N/A |

## Widgets

The container element for one or more widget registrations.

## Widget

Represents the registration for a single widget.

| Attribute | Type | Required | Description | Default value |
|---|---|---|---|---|
| **Name**| string | Yes | A name that identifies the widget. Widget provider implementations use this name to determine or specify which of the app's widgets is being referenced for each operation.  | N/A |
| **DisplayTitle** | string | Yes | The title of the widget that is displayed on the widget board. | N/A |
| **Description** | string | Yes | Optionally create custom actions with buttons and inputs. | N/A |
| **AllowMultiple** | boolean | No | Set to true if the widget provider supports multiple widgets. This attribute is optional and the default value is false. | false |

## WidgetCapablities

Specifies capabilities for a single widget. This element is optional.

## WidgetCapability

Specifies a capability for a widget.

| Attribute | Type | Required | Description | Default value |
|---|---|---|---|---|
| **WidgetSize**| string | Yes | Specifies a supported size for a widget. The value must be one of the following: "Small", "Medium", "Large" | N/A |

Specify one **WidgetCapability** element for each size the provider supports. If the **WidgetCapabilities** element is omitted, the default behavior is the same as if a single **WidgetCapability** element with the value "Large" is provided. 

## WidgetThemeResources

TBD

## Icon

TBD

| Attribute | Type | Required | Description | Default value |
|---|---|---|---|---|
| **Source**| string | Yes | TBD package-relative | N/A |

## Screenshots

TBD

## Screenshot

TBD

| Attribute | Type | Required | Description | Default value |
|---|---|---|---|---|
| **Source**| string | Yes | TBD package-relative | N/A |
| **DisplayLabel**| string | No | TBD package-relative | N/A |

## DarkMode

TBD

## LightMode

## Example

The following code example illustrates the usage of the widget package manifest XML format.

```xml
<uap3:Extension Category="windows.appExtension">
  <uap3:AppExtension Name="com.microsoft.windows.widgets" DisplayName="WidgetTestApp" Id="ContosoWidgetApp" PublicFolder="Public">
    <uap3:Properties>
      <!--
        Icon is mandatory, and host uses to group all Widgets in the same Icon in WidgetPicker
      -->
      <WidgetProvider Icon="Images\StoreIcon.png">
    
            
    
	    <!-- COM and ActivateApplication are supported. User has to use one of them to deliver message to WidgetProvider -->
	    <Activation>
		  <!-- Apps exports COM interface which implements WidgetProvider -->
		  <COM ClassId="ECB883FD-3755-4E1C-BECA-D3397A3FF15C" />
	    </Activation>
        <Widgets>
          <!--
            AllowMultiple is optional, default to False.
                       Name, WidgetThemeResources, DisplayTitle and Description are mandatory.
                    -->
          <Widget
            Name="Weather_Widget"
            DisplayTitle="Microsoft Weather Widget"
            Description="Weather Widget Description"
            AllowMultiple="True">
            <!-- WidgetCapabilities is optional in Widget. If not defined, only `<WidgetCapability WidgetSize="Large" />` is included.

                        If we add the support for taskbar in the near future, which will add a new container in AC designer,
                        WidgetContainerType is going to be added to WidgetCapability for the new container.
                        For backward compatibility, WidgetContainerType is optional in WidgetCapability. WidgetContainerType is "berlin" by default.
                        <WidgetCapability WidgetSize="Small" WidgetContainerType="taskbar" />
                        -->
            <WidgetCapabilities>
              <WidgetCapability WidgetSize="Small" />
              <WidgetCapability WidgetSize="Medium" />
              <WidgetCapability WidgetSize="Large" />
            </WidgetCapabilities>

            <WidgetThemeResources>
              <!-- Icon and Screenshots are mandatory -->
              <Icon Source="ProviderAssets\icon.png" />
              <Screenshots>
                <!--In <Screenshot>, Source is mandatory, and DisplayLabel is optional -->
                <Screenshot Source="Asserts\background.png" DisplayLabel="For accessibility"/>
              </Screenshots>

              <!-- DarkMode and LightMode are optional -->
              <DarkMode>
                <Icon Source="ProviderAssets\dark.png" />
                <Screenshots>
                  <Screenshot Source="Asserts\darkBackground.png" DisplayLabel="For accessibility"/>
                </Screenshots>
              </DarkMode>

              <LightMode>
                <Icon Source="ProviderAssets\light.png" />
                <Screenshots>
                  <Screenshot Source="Asserts\lightBackground.png"/>
                </Screenshots>
              </LightMode>
            </WidgetThemeResources>
          </Widget>
        </Widgets>
      </WidgetProvider>
    </uap3:Properties>
  </uap3:AppExtension>
</uap3:Extension>
```
