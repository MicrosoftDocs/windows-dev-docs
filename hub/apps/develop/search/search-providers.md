---
title: Windows Search providers
description: Learn how to integrate into the Windows Search experience. 
ms.topic: article
ms.date: 09/07/2023
ms.author: drewbat
author: drewbatgit
ms.localizationpriority: medium
---



# Windows Search providers

> [!NOTE]
> **Some information relates to pre-released product, which may be substantially modified before it's commercially released. Microsoft makes no warranties, express or implied, with respect to the information provided here.**
> [!IMPORTANT]
> The feature described in this topic is available in *TBD - Dev Channel preview builds of Windows starting with build 25217*. For information on preview builds of Windows, see [Windows 10 Insider Preview](https://insider.windows.com/en-us/preview-windows).

Starting with **TBD - Feature availability information** 3rd party search engines can integrate into the built-in Windows Search experience to provide suggested results and preview pages in response to the query the user types into the Windows Search box.

:::image type="content" source="images/search-integration.png" alt-text="Screenshot of the Windows Search UI with 3rd party search provider integration.":::

Search providers integrate with the Search experience by creating an [MSIX package](/windows/msix/) with a package manifest file that provides the required information for the OS to register the search provider. Users can add a search provider to Windows by installing the associated app package through [Microsoft Store](https://www.microsoft.com/store/apps/windows) and can remove the search provider through the **Add or remove programs** page in Windows Settings app.

Once the search provider is registered with the OS, user queries are passed to the HTTP endpoint specified by the provider in their package manifest using a standardized query string. The endpoint returns suggested results in a JSON document. With each suggested URL in the response document, the search provider includes the preview endpoint URL, which returns an HTML document that is displayed in the preview pane in the search results UI.

This article provides guidance for creating a search provider app package and details about the protocols for implementing search provider HTTP endpoints.

## Create a search extensibility app package

Search providers register with the OS by providing an MSIX package that contains required information about the provider, such as the name of the search provider and the HTTP endpoints for suggestions and previews.

TBD - Steps for creating the package. We should pick the steps that align to the typical 3rd party scenario. I.e. Packaging at the command line or using a Windows Packaging Project in Visual Studio. 

### Search provider app extension

The app package manifest file supports many different extensions and features for Windows apps. The app package manifest format is defined by a set of schemas that are documented in the [Package manifest schema reference](/uwp/schemas/appxpackage/uapmanifestschema/schema-root).  Search providers declare their registration information within the [uap3:AppExtension](/uwp/schemas/appxpackage/uapmanifestschema/element-uap3-appextension-manual). The **Name** attribute of the extension must be set to "com.microsoft.windows.websearchprovider".

Search providers should include the [uap3:Properties](/uwp/schemas/appxpackage/uapmanifestschema/element-uap3-properties-manual) as the child of **uap3:AppExtension**. The package manifest schema does not enforce the structure of the **uap3:Properties** element other than requiring well-formed XML. The rest of this section describes the XML format that the OS expects in order to successfully register a search provider.

```xml
<uap3:Extension Category="windows.appExtension">
  <uap3:AppExtension Name="com.microsoft.windows.websearchprovider" DisplayName="SearchExampleApp" Id="ContosoSearchApp" PublicFolder="Public">
    <uap3:Properties>
    <!-- Search provider registration content goes here -->
    </uap3:Properties>
  </uap3:AppExtension>
</uap3:Extension>
```

### Element hierarchy

uap3:Properties

&nbsp;&nbsp;EndPoint

&nbsp;&nbsp;Protocol

#### Endpoint

The URL of the HTTPS endpoint to which the OS will send search query requests.

#### Protocol

The protocol schema that will be used when launching the provided web search results. If the specified protocol is not registered by an app on the OS, then the default browser will be launched for search results. For more information on registering protocol schemas, see [uap:Protocol](/uwp/schemas/appxpackage/uapmanifestschema/element-uap-protocol).


### Example package manifest file

The following is an example `appmanifest.xml` package manifest file for registering a Windows Search provider.

TBD - Update with good example.

```xml
<!-- appxmanifest.xml -->
<?xml version="1.0" encoding="utf-8"?>

<Package
  xmlns="http://schemas.microsoft.com/appx/manifest/foundation/windows10"
  xmlns:uap="http://schemas.microsoft.com/appx/manifest/uap/windows10"
  xmlns:uap3="http://schemas.microsoft.com/appx/manifest/uap/windows10/3"
  xmlns:mp="http://schemas.microsoft.com/appx/2014/phone/manifest"
  IgnorableNamespaces="uap mp">

  <Identity
    Name="ef69a2f4-106b-47e1-8c66-8a830fb59531"
    Publisher="CN=v-xinwli"
    Version="1.0.38.0" />

	<mp:PhoneIdentity PhoneProductId="d201f4da-cb64-4cac-952c-127bdb312883" PhonePublisherId="00000000-0000-0000-0000-000000000000"/>

  <Properties>
    <DisplayName>CustomSearch</DisplayName>
    <PublisherDisplayName>v-xinwli</PublisherDisplayName>
    <Logo>Assets\StoreLogo.png</Logo>
  </Properties>

  <Dependencies>
    <TargetDeviceFamily Name="Windows.Universal" MinVersion="10.0.0.0" MaxVersionTested="10.0.0.0" />
  </Dependencies>

  <Resources>
    <Resource Language="x-generate"/>
  </Resources>

  <Applications>
    <Application Id="App"
      Executable="$targetnametoken$.exe"
      EntryPoint="CustomSearchProvider.App">
      <uap:VisualElements
        DisplayName="CustomSearch"
        Square150x150Logo="Assets\Square150x150Logo.png"
        Square44x44Logo="Assets\Square44x44Logo.png"
        Description="Custom search provider"
        BackgroundColor="transparent">
        <uap:DefaultTile Wide310x150Logo="Assets\Wide310x150Logo.png"/>
        <uap:SplashScreen Image="Assets\SplashScreen.png" />
      </uap:VisualElements>
      <Extensions>
		  <uap3:Extension Category="windows.appExtension">
			  <uap3:AppExtension Name="com.microsoft.windows.websearchprovider" DisplayName="CustomSearch" Id="CustomSearchApp" PublicFolder="Public">
				  <uap3:Properties>
					  <Endpoint>https://customsearchendpoint</Endpoint>
					  <Protocol>customsearch</Protocol>
				  </uap3:Properties>
			  </uap3:AppExtension>
		  </uap3:Extension>
		  <uap:Extension Category="windows.protocol">
			  <uap:Protocol Name="customsearch"/>
		  </uap:Extension>
    </Extensions>
    </Application>
  </Applications>

  <Capabilities>
    <Capability Name="internetClient" />
	<Capability Name="privateNetworkClientServer" />
  </Capabilities>
</Package>
```

## Implement a Windows Search provider suggestion endpoint

Search providers must expose and register an HTTPS endpoint that is called by the OS when a user types into the Windows Search box. This endpoint should return a JSON-formatted string containing the search suggestions for the provided user query. Content must be delivered over HTTPS. Search integration does not support content delivered over HTTP.

### Suggestion HTTPS request format

The HTTPS request to the suggestion endpoint uses the following format.

`https://contoso.com?setlang=en-US&cc=US&qry=`

The query string parameters passed to the suggestion endpoint are the following.

| Parameter | Description |
|-----------|-------------|
| setlang   | The locale associated with the query. |
| cc        | The country code associated with query. |
| qry       | The query provided by the user. If the parameter has no value, i.e. appears in the query string as `qry=`, then the user query is empty. Search providers can still provide suggestions and preview pages in response to an empty query. **NOTE** The OS does not perform any sanitization of query strings. Search providers can implement their own sanitization when the query is received. |

### Suggestion HTTPS response headers

Search provider must include the following headers in the response from the suggestion HTTPS endpoint.

- Access-Control-Allow-Origin: https://www.bing.com 
- Access-Control-Allow-Credentials: true 
- Access-Control-Allow-Methods: GET 
- Content-Type: application/json; charset=utf-8 
- Content-Length: \[Must be the exact length of response\] 

### Suggestion response JSON format

The search provider HTTPS endpoint for suggestions must return a JSON document with the following format. The key names must match the format exactly.

| Key | Description |
|-----|-------------|
| Suggestions | Contains a list of JSON objects with key `Attributes` representing the suggestions associated with the user query. |
| Attributes | Contains the attributes of a suggestion. |
| url | The URL for the search suggestion on the provider web site. |
| query | The user query associated with the search suggestion. |
| previewPaneUrl | The URL of the preview endpoint from which an HTML preview of the suggestion can be retrieved. |
| Text | The text description of the suggestion. |

```json
{"Suggestions": 
   [{"Attributes": 
     {"url":"https://www.contoso.com/search?q=projection+matrix","query":"projection matrix","previewPaneUrl":"http://www.contoso.com/preview"} ,"Text":"projection matrix"}, 
    {"Attributes": 
     {"url":"https://www.contoso.com/search?q=rotation+matrix","query":"rotation matrix","previewPaneUrl":"http://www.contoso.com/preview"} ,"Text":"rotation matrix"}
    ] 
} 
```

## Implement a Windows Search provider preview endpoint

Search providers return the URL of an HTTPS endpoint that provides an HTML preview of the page associated with each suggestion in the search results. The preview endpoint response must return the HTML code for a functioning page.

### Preview HTTPS request format

The HTTPS request to the preview endpoint uses the following format.

`https://contoso.com?Darkschemeovr=1`

The query string parameters passed to the suggestion endpoint are the following.

| Parameter | Description |
|-----------|-------------|
| Darkschemeovr | Specifies if the calling Windows system has dark theme enabled. The value is 1 if dark theme is enabled and 0 if the dark theme is disabled. |

### Preview HTTPS response headers


- Access-Control-Allow-Origin: https://www.bing.com 
- Access-Control-Allow-Credentials: true 
- Access-Control-Allow-Methods: GET 
- Content-Type: text/html; charset=utf-8 
- Content-Length: \[Must be the exact length of preview html\]

### OPTIONS request and Cross-Origin Resource Sharing (CORS)

Search providers must support the OPTIONS request method and respond to this request with HTTP OK. If the search provider endpoint is using CORS, the Windows search client will send out a HTTP OPTIONS request before each GET request.

## Related articles


