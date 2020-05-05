---
Description: At build time, the Resource Management System creates an index of all the different variants of the resources that are packaged up with your app. At run-time, the system detects the user and machine settings that are in effect and loads the resources that are the best match for those settings.
title: Resource Management System
template: detail.hbs
ms.date: 10/20/2017
ms.topic: article
keywords: windows 10, uwp, resource, image, asset, MRT, qualifier
ms.localizationpriority: medium
---
# Resource Management System
The Resource Management System has both build-time and run-time features. At build time, the system creates an index of all the different variants of the resources that are packaged up with your app. This index is the Package Resource Index, or PRI, and it's also included in your app's package. At run-time, the system detects the user and machine settings that are in effect, consults the information in the PRI, and automatically loads the resources that are the best match for those settings.

## Package Resource Index (PRI) file
Every app package should contain a binary index of the resources in the app. This index is created at build time and it is contained in one or more Package Resource Index (PRI) files.

- A PRI file contains actual string resources, and an indexed set of file paths that refer to various files in the package.
- A package typically contains a single PRI file per language, named resources.pri.
- The resources.pri file at the root of each package is automatically loaded when the [**ResourceManager**](/uwp/api/windows.applicationmodel.resources.core.resourcemanager?branch=live) is instantiated.
- PRI files can be created and dumped with the tool [MakePRI.exe](compile-resources-manually-with-makepri.md).
- For typical app development you won't need MakePRI.exe because it's already integrated into the Visual Studio compile workflow. And Visual Studio supports editing PRI files in a dedicated UI. However, your localizers and the tools they use might rely upon MakePRI.exe.
- Each PRI file contains a named collection of resources, referred to as a resource map. When a PRI file from a package is loaded, the resource map name is verified to match the package identity name.
- PRI files contain only data, so they don't use the portable executable (PE) format. They are specifically designed to be data-only as the resource format for Windows. They replace resources contained within DLLs in the Win32 app model.

## UWP API access to app resources

### Basic functionality (ResourceLoader)
The simplest way to access your app resources programmatically is by using the [**Windows.ApplicationModel.Resources**](/uwp/api/windows.applicationmodel.resources?branch=live) namespace and the [**ResourceLoader**](/uwp/api/windows.applicationmodel.resources.resourceloader?branch=live) class. **ResourceLoader** provides you basic access to string resources from the set of resource files, referenced libraries, or other packages.

### Advanced functionality (ResourceManager)
The [**ResourceManager**](/uwp/api/windows.applicationmodel.resources.core.resourcemanager?branch=live) class (in the [**Windows.ApplicationModel.Resources.Core**](/uwp/api/windows.applicationmodel.resources.core?branch=live) namespace) provides additional info about resources, such as enumeration and inspection. This goes beyond what the **ResourceLoader** class provides.

A [**NamedResource**](/uwp/api/windows.applicationmodel.resources.core.namedresource?branch=live) object represents an individual logical resource with multiple language or other qualified variants. It describes the logical view of the asset or resource, with a string resource identifier such as `Header1`, or a resource file name such as `logo.jpg`.

A [**ResourceCandidate**](/uwp/api/windows.applicationmodel.resources.core.resourcecandidate?branch=live) object represents a single concrete resource value and its qualifiers, such as the string "Hello World" for English, or "logo.scale-100.jpg" as a qualified image resource that's specific to the **scale-100** resolution.

Resources available to an app are stored in hierarchical collections, which you can access with a [**ResourceMap**](/uwp/api/windows.applicationmodel.resources.core.resourcemap?branch=live) object. The **ResourceManager** class provides access to the various top-level **ResourceMap** instances used by the app, which correspond to the various packages for the app. The [**MainResourceMap**](/uwp/api/windows.applicationmodel.resources.core.resourcemanager.MainResourceMap) value corresponds to the resource map for the current app package, and it excludes any referenced framework packages. Each **ResourceMap** is named for the package name that is specified in the package's manifest. Within a **ResourceMap** are subtrees (see [**ResourceMap.GetSubtree**](/uwp/api/windows.applicationmodel.resources.core.resourcemap.getsubtree?branch=live)), which further contain **NamedResource** objects. The subtrees typically correspond to the resource files that contains the resource. For more info see [Localize strings in your UI and app package manifest](localize-strings-ui-manifest.md) and [Load images and assets tailored for scale, theme, high contrast, and others](images-tailored-for-scale-theme-contrast.md).

Here's an example.

```csharp
// using Windows.ApplicationModel.Resources.Core;
ResourceMap resourceMap =  ResourceManager.Current.MainResourceMap.GetSubtree("Resources");
ResourceContext resourceContext = ResourceContext.GetForCurrentView()
var str = resourceMap.GetValue("String1", resourceContext).ValueAsString;
```

**Note** The resource identifier is treated as a Uniform Resource Identifier (URI) fragment, subject to URI semantics. For example, `GetValue("Caption%20")` is treated as `GetValue("Caption ")`. Do not use "?" or "#" in resource identifiers, because they terminate the resource path evaluation. For example, "MyResource?3" is treated as "MyResource".

The **ResourceManager** not only supports access to an app's string resources, it also maintains the ability to enumerate and inspect the various file resources as well. In order to avoid collisions between files and other resources that originate from within a file, indexed file paths all reside within a reserved "Files" **ResourceMap** subtree. For example, the file `\Images\logo.png` corresponds to the resource name `Files/images/logo.png`.

The [**StorageFile**](/uwp/api/Windows.Storage.StorageFile?branch=live) APIs transparently handle references to files as resources, and are appropriate for typical usage scenarios. The **ResourceManager** should only be used for advanced scenarios, such when you want to override the current context.

### ResourceContext
Resource candidates are chosen based on a particular [**ResourceContext**](/uwp/api/Windows.ApplicationModel.Resources.Core.ResourceContext?branch=live), which is a collection of resource qualifier values (language, scale, contrast, and so on). A default context uses the app's current configuration for each qualifier value, unless overridden. For example, resources such as images can be qualified for scale, which varies from one monitor to another and hence from one application view to another. For this reason, each application view has a distinct default context. The default context for a given view can be obtained using [**ResourceContext.GetForCurrentView**](/uwp/api/windows.applicationmodel.resources.core.resourcecontext.GetForCurrentView). Whenever you retrieve a resource candidate, you should pass in a **ResourceContext** instance to obtain the most appropriate value for a given view.

## Important APIs
* [ResourceLoader](/uwp/api/windows.applicationmodel.resources.resourceloader?branch=live)
* [ResourceManager](/uwp/api/windows.applicationmodel.resources.core.resourcemanager?branch=live)
* [ResourceContext](/uwp/api/windows.applicationmodel.resources.core.resourcecontext?branch=live)

## Related topics
* [Localize strings in your UI and app package manifest](localize-strings-ui-manifest.md)
* [Load images and assets tailored for scale, theme, high contrast, and others](images-tailored-for-scale-theme-contrast.md)
