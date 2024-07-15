---
title: Create and host a package extension 
description: This article shows you how to create a Windows 11 package extension and host it in an app. Package extensions are supported in UWP apps and packaged desktop apps.
keywords: app extension, app service, background
ms.date: 10/05/2023
ms.topic: article
ms.localizationpriority: medium
---

# Create and host a package extension 

This article shows you how to create a Windows 11 package extension and host it in an app. Package extensions are supported in UWP apps and [packaged desktop apps](/windows/apps/desktop/modernize/#msix-packages). 

Package extensions are like app extension but come with more flexibility.
Any package with an extension declaration can now seamlessly share content and deployment events with a host app. This capability extends beyond app packages, encompassing main, optional, framework, and resource packages. 
While package extensions are declared with a different set of XML elements, the implementation, management, and debugging of package extensions are the same application extensions. 
For more information about application extensions, see [Create and host an app extension](/windows/uwp/launch-resume/how-to-create-an-extension).

Package extension is available on Windows 11 Server now and will be available for Windows 11 in a future release. 

## Declare a package to be an extension host

An app identifies itself as a package extension host by declaring the `<PkgExtensionHost>` element in its **Package.appxmanifest file**.  

The following shows an example package extension declaration in a Package.appxmanifest file.

```xml
 <Package 
   ... 
   xmlns:uap17="http:schemas.microsoft.com/appx/manifest/uap/windows10/17" 
   IgnorableNamespaces="uap uap17"> 
   ... 
   <Extensions> 
    <uap17:Extension Category="windows.packageExtensionHost">
      <uap17:PackageExtensionHost>
        <uap17:Name>com.microsoft.mathext</uap17:Name>
      </uap17:PackageExtensionHost>
    </uap17:Extension>
   </Extensions> 
     ... 
 </Package> 
```

Notice the `xmlns:uap17="http://..."` and the presence of `uap17` in IgnorableNamespaces. These are necessary because we are using the uap17 namespace.

`<uap17:Extension Category="windows.packageExtensionHost">` identifies this package as an extension host. 

The Name element in `<uap17:PackageExtensionHost>` is the extension contract name. When an extension specifies the same extension contract name, the host will be able to find it. By convention, we recommend building the extension contract name using your app or publisher name to avoid potential collisions with other extension contract names. 

You can define multiple hosts and multiple extensions in the same package. In this example, we declare one host. The extension is defined in another package. 

## Declare a package to be an extension 

A package identifies itself as a package extension by declaring the `<uap17:PackageExtension>` element in its Package.appxmanifest file. The following example shows an example package declaring itself as an extension

```xml
 <Package 
   ... 
   xmlns:uap17="http:schemas.microsoft.com/appx/manifest/uap/windows10/17" 
   IgnorableNamespaces="uap uap17"> 
   ... 
   <Extensions> 
     <uap17:Extension Category="windows.packageExtension"> 
       <uap17:PackageExtension Name="com.microsoft.ai.aiexplorer.model" 
           Id="power" 
           DisplayName="x^y" 
           PublicFolder="Public"
		   Description="Exponent"> 
             <uap17:Properties> 
                 <Service>com.microsoft.powservice</Service>
             </uap17:Properties> 
       </uap17:PackageExtension> 
     </uap17:Extension> 
   </Extensions> 
     ... 
 </Package> 
```

Again, notice the `xmlns:uap17="http://..."` line, and the presence of `uap17` in `IgnorableNamespaces`. These are necessary because we are using the uap17 namespace. 

`<uap17:Extension Category="windows.packageExtension">` identifies this package as an extension. 

The meaning of the `<uap17:PackageExtension>` attributes are as follows

|Attribute|Description|Required|
|---------|-----------|:------:|
|**Name**|This is the extension contract name. When it matches the **Name** declared in a host, that host will be able to find this extension.| :heavy_check_mark: |
|**ID**| Uniquely identifies this extension. Because there can be multiple extensions that use the same extension contract name (imagine a paint app that supports several extensions), you can use the ID to tell them apart. App extension hosts can use the ID to infer something about the type of extension. For example, you could have one Extension designed for desktop and another for mobile, with the ID being the differentiator. You could also use the **Properties** element, discussed below, for that.| :heavy_check_mark: |
|**DisplayName**| Can be used from your host app to identify the extension to the user. It is queryable from, and can use, the [new resource management system](../app-resources/using-mrt-for-converted-desktop-apps-and-games.md) (`ms-resource:TokenName`) for localization. The localized content is loaded from the app extension package, not the host app. | |
|**Description** | Can be used from your host app to describe the extension to the user. It is queryable from, and can use, the [new resource management system](../app-resources/using-mrt-for-converted-desktop-apps-and-games.md) (`ms-resource:TokenName`) for localization. The localized content is loaded from the app extension package, not the host app. | |
|**PublicFolder**|The name of a folder, relative to the package root, where you can share content with the extension host. By convention the name is "Public", but you can use any name that matches a folder in your extension.| :heavy_check_mark: |

`<uap17:Properties>` is an optional element that contains custom metadata that hosts can read at runtime. 
In the code sample, the extension is implemented as an app service so host needs a way to get the name of that app service so it can call it. 
The name of the app service is defined in the `<Service>` element, which we defined (we could have called it anything we wanted). 
The host in the code sample looks for this property at runtime to learn the name of the app service.

## Remarks

This topic provides an introduction to package extensions. The implementation, management, and debugging of Package Extension is similar to that of App Extension and can be referred to from [here](/windows/uwp/launch-resume/how-to-create-an-extension). 
The key things to note are the creation of the host and marking it as such in its Package.appxmanifest file, creating the extension and marking it as such in its Package.appxmanifest file, determining how to implement the extension (such as an app service, background task, COM or WinRT server, or other means), defining how the host will communicate with extensions, and using the PackageExtensions API to access and manage extensions.


