---
description: Learn how to build a hosted app that inherits the executable, entry point and runtime attributes of a host app.
title: Create hosted apps
ms.date: 05/26/2023
ms.topic: article
keywords: windows 10, desktop, package, identity, MSIX, Win32
ms.localizationpriority: medium
ms.custom: RS5
---

# Create hosted apps

Starting in Windows 10, version 2004, you can create *hosted apps*. A hosted app shares the same executable and definition as a parent *host* app, but it looks and behaves like a separate app on the system.

Hosted apps are useful for scenarios where you want a component (such as an executable file or a script file) to behave like a standalone Windows 10 app, but the component requires a host process in order to execute. For example, a PowerShell or Python script could be delivered as a hosted app that requires a host to be installed in order to run. A hosted app can have its own start tile, identity, and deep integration with Windows 10 features such as background tasks, notifications, tiles, and share targets.

The hosted apps feature is supported by several elements and attributes in the package manifest that enable a hosted app to use an executable and definition in a host app package. When a user runs the hosted app, the OS automatically launches the host executable under the identity of the hosted app. The host can then load visual assets, content, or call APIs as the hosted app. The hosted app gets the intersection of capabilities declared between the host and hosted app. This means that a hosted app cannot ask for more capabilities than what the host provides.

## Define a host

The *host* is the main executable or runtime process for the hosted app. Currently, the only supported hosts are desktop apps (.NET or C++ desktop) that have *package identity*. There are several ways for a desktop app to have package identity:

* The most common way to grant package identity to a desktop app is by [packaging it in an MSIX package](/windows/msix).
* In some cases, you may alternatively choose to grant package identity by creating a package with external location (see [Grant package identity by packaging with external location](/windows/apps/desktop/modernize/grant-identity-to-nonpackaged-apps)). This option is useful if you're unable to adopt MSIX for installing your desktop app.

The host is declared in its package manifest by the [**uap10:HostRuntime**](/uwp/schemas/appxpackage/uapmanifestschema/element-uap10-hostruntime) extension. This extension has an **Id** attribute that must be assigned a value that is also referenced by the package manifest for the hosted app. When the hosted app is activated, the host is launched under the identity of the hosted app and can load content or binaries from the hosted app package.

The following example demonstrates how to define a host in a package manifest. The **uap10:HostRuntime** extension is package-wide and is therefore declared as a child of the [**Package**](/uwp/schemas/appxpackage/uapmanifestschema/element-package) element.

``` xml
<Package xmlns:uap10="http://schemas.microsoft.com/appx/manifest/uap/windows10/10">

  <Extensions>
    <uap10:Extension Category="windows.hostRuntime"  
        Executable="PyScriptEngine\PyScriptEngine.exe"  
        uap10:RuntimeBehavior="packagedClassicApp"  
        uap10:TrustLevel="mediumIL">
      <uap10:HostRuntime Id="PythonHost" />
    </uap10:Extension>
  </Extensions>

</Package>
```

Make note of these important details about the following elements.

| Element              | Details |
|----------------------|-------|
| [**uap10:Extension**](/uwp/schemas/appxpackage/uapmanifestschema/element-uap10-package-extension) | The `windows.hostRuntime` category declares a package-wide extension that defines the runtime information to be used when activating a hosted app. A hosted app will run with the definitions declared in the extension. When using the host app declared in the previous example, a hosted app will run as the executable **PyScriptEngine.exe** at the **mediumIL** trust level.<br/><br/>The **Executable**, **uap10:RuntimeBehavior**, and **uap10:TrustLevel** attributes specify the name of the host process binary in the package and how the hosted apps will run. For example, a hosted app using the attributes in the previous example will run as the executable PyScriptEngine.exe at mediumIL trust level. |
| [**uap10:HostRuntime**](/uwp/schemas/appxpackage/uapmanifestschema/element-uap10-hostruntime) | The **Id** attribute declares the unique identifier of this specific host app in the package. A package can have multiple host apps, and each must have a **uap10:HostRuntime** element with a unique **Id**.

## Declare a hosted app

A *hosted app* declares a package dependency on a *host*. The hosted app leverages the host's ID (that is, the **Id** attribute of the **uap10:HostRuntime** extension in the host package) for activation instead of specifying an entry point executable in its own package. The hosted app typically contains content, visual assets, scripts, or binaries that may be accessed by the host. The [**TargetDeviceFamily**](/uwp/schemas/appxpackage/uapmanifestschema/element-targetdevicefamily) value in the hosted app package should target the same value as the host.

Hosted app packages can be signed or unsigned:

* Signed packages may contain executable files. This is useful in scenarios that have a binary extension mechanism, which enables the host to load a DLL or registered component in the hosted app package.
* In most scenarios, the unsigned package will contain executable content. But an unsigned package that contains only *non-executable* files is useful in scenarios where the host needs to load only images, assets, and content or script files. Unsigned packages must include a special `OID` value in their [**Identity**](/uwp/schemas/appxpackage/uapmanifestschema/element-identity) element or they won't be allowed to register. This prevents unsigned packages from conflicting with or spoofing the identity of a signed package.

To define a hosted app, declare the following items in the package manifest:

* The [**uap10:HostRuntimeDependency**](/uwp/schemas/appxpackage/uapmanifestschema/element-uap10-hostruntimedependency) element. This is a child of the [Dependencies](/uwp/schemas/appxpackage/uapmanifestschema/element-dependencies) element.
* The **uap10:HostId** attribute of the [**Application**](/uwp/schemas/appxpackage/uapmanifestschema/element-application) element (for an app) or [**Extension**](/uwp/schemas/appxpackage/uapmanifestschema/element-1-extension) element (for an activatable extension).

The following example demonstrates the relevant sections of a package manifest for an unsigned hosted app.

``` xml
<Package xmlns:uap10="http://schemas.microsoft.com/appx/manifest/uap/windows10/10">

  <Identity Name="NumberGuesserManifest"
    Publisher="CN=AppModelSamples, OID.2.25.311729368913984317654407730594956997722=1"
    Version="1.0.0.0" />

  <Dependencies>
    <TargetDeviceFamily Name="Windows.Desktop" MinVersion="10.0.19041.0" MaxVersionTested="10.0.19041.0" />
    <uap10:HostRuntimeDependency Name="PyScriptEnginePackage" Publisher="CN=AppModelSamples" MinVersion="1.0.0.0"/>
  </Dependencies>

  <Applications>
    <Application Id="NumberGuesserApp"  
      uap10:HostId="PythonHost"  
      uap10:Parameters="-Script &quot;NumberGuesser.py&quot;">
    </Application>
  </Applications>

</Package>
```

Make note of these important details about the following elements.

| Element              | Details |
|----------------------|-------|
| [**Identity**](/uwp/schemas/appxpackage/uapmanifestschema/element-identity) | Because the hosted app package in this example is unsigned, the **Publisher** attribute must include the `OID.2.25.311729368913984317654407730594956997722=1` string. This ensures that the unsigned package cannot spoof the identity of a signed package. |
| [**TargetDeviceFamily**](/uwp/schemas/appxpackage/uapmanifestschema/element-targetdevicefamily) | The **MinVersion** attribute must specify 10.0.19041.0 or a later OS version. |
| [**uap10:HostRuntimeDependency**](/uwp/schemas/appxpackage/uapmanifestschema/element-uap10-hostruntimedependency)  | This element element declares a dependency on the host app package. This consists of the **Name** and **Publisher** of the host package, and the **MinVersion** it depends on. These values can be found under the [Identity](/uwp/schemas/appxpackage/uapmanifestschema/element-identity) element in the host package. |
| [**Application**](/uwp/schemas/appxpackage/uapmanifestschema/element-application) | The **uap10:HostId** attribute expresses the dependency on the host. The hosted app package must declare this attribute instead of the usual **Executable** and **EntryPoint** attributes for an [**Application**](/uwp/schemas/appxpackage/uapmanifestschema/element-application) or [**Extension**](/uwp/schemas/appxpackage/uapmanifestschema/element-1-extension) element. As a result, the hosted app inherits the **Executable**, **EntryPoint** and runtime attributes from the host with the corresponding **HostId** value.<br/><br/>The **uap10:Parameters** attribute specifies parameters that are passed to the entry point function of the host executable. Because the host needs to know what to do with these parameters, there is an implied contract between the host and hosted app. |

## Register an unsigned hosted app package at run time

One benefit of the **uap10:HostRuntime** extension is that it enables a host to dynamically generate a hosted app package at runtime and register it by using the [**PackageManager**](/uwp/api/windows.management.deployment.packagemanager) API, without needing to sign it. This enables a host to dynamically generate the content and manifest for the hosted app package and then register it.

Use the following methods of the [**PackageManager**](/uwp/api/windows.management.deployment.packagemanager) class to register an unsigned hosted app package. These methods are available starting in Windows 10, version 2004.

* [**AddPackageByUriAsync**](/uwp/api/windows.management.deployment.packagemanager.addpackagebyuriasync): Registers an unsigned MSIX package by using the **AllowUnsigned** property of the *options* parameter.
* [**RegisterPackageByUriAsync**](/uwp/api/windows.management.deployment.packagemanager.registerpackagebyuriasync): Performs a loose package manifest file registration. If the package is signed, the folder containing the manifest must include a [.p7x file](/windows/msix/overview#inside-an-msix-package) and catalog. If unsigned, the **AllowUnsigned** property of the *options* parameter must be set.

### Requirements for unsigned hosted apps

* The [**Application**](/uwp/schemas/appxpackage/uapmanifestschema/element-application) or [**Extension**](/uwp/schemas/appxpackage/uapmanifestschema/element-1-extension) elements in the package manifest cannot contain activation data such as the **Executable**, **EntryPoint**, or **TrustLevel** attributes. Instead, these elements can only contain a **uap10:HostId** attribute that expresses the dependency on the host and a **uap10:Parameters** attribute.
* The package must be a main package. It cannot be a bundle, framework package, resource, or optional package.

### Requirements for a host that installs and registers an unsigned hosted app package

* The host must have [package identity](#define-a-host).
* The host must have the **packageManagement** [restricted capability](../packaging/app-capability-declarations.md#restricted-capabilities).
    ```xml
    <rescap:Capability Name="packageManagement" />
    ```

<!--
* If the host runs in app container (for example, it is a UWP app), it must also have the unsigned package management [custom capability](../packaging/app-capability-declarations.md#custom-capabilities) and a [Signed Custom Capability Descriptor (SCCD) file](/windows-hardware/drivers/devapps/hardware-support-app--hsa--steps-for-driver-developers#preparing-the-signed-custom-capability-descriptor-sccd-file).
    ```xml
    <uap4:CustomCapability Name="Microsoft.unsignedPackageManagement_cw5n1h2txyewy" />
    ```
-->

## Sample

For a fully functional sample app that declares itself as a host and then dynamically registers a hosted app package at runtime, see the [hosted app sample](https://github.com/microsoft/AppModelSamples/tree/master/Samples/HostedApps).

### The host

The host is named **PyScriptEngine**. This is a wrapper written in C# that runs python scripts. When run with the `-Register` parameter, the script engine installs a hosted app containing a python script. When a user tries to launch the newly installed hosted app, the host is launched and executes the **NumberGuesser** python script.

The package manifest for the host app (the Package.appxmanifest file in the PyScriptEnginePackage folder) contains a **uap10:HostRuntime** extension that declares the app as a host with the ID **PythonHost** and the executable **PyScriptEngine.exe**.  

> [!NOTE]
> In this sample, the package manifest is named Package.appxmanifest and it is part of a [Windows Application Packaging Project](/windows/msix/desktop/desktop-to-uwp-packaging-dot-net). When this project is built, it [generates a manifest named AppxManifest.xml](/uwp/schemas/appxpackage/uapmanifestschema/generate-package-manifest) and builds the MSIX package for the host app.

### The hosted app

The hosted app consists of a python script and package artifacts such as the package manifest. It doesn’t contain any PE files.

The package manifest for the hosted app (the NumberGuesser/AppxManifest.xml file) contains the following items:

* The **Publisher** attribute of the [**Identity**](/uwp/schemas/appxpackage/uapmanifestschema/element-identity) element contains the `OID.2.25.311729368913984317654407730594956997722=1` identifier, which is required for an unsigned package.
* The **uap10:HostId** attribute of the [**Application**](/uwp/schemas/appxpackage/uapmanifestschema/element-application) element identifies **PythonHost** as its host.

### Run the sample

The sample requires version 10.0.19041.0 or later of Windows 10 and the Windows SDK.

1. Download the [sample](https://aka.ms/hostedappsample) to a folder on your development computer.
2. Open the PyScriptEngine.sln solution in Visual Studio and set the **PyScriptEnginePackage** project as the startup project.
3. Build the **PyScriptEnginePackage** project.
4. In Solution Explorer, right-click the **PyScriptEnginePackage** project and choose **Deploy**.
5. Open a Command Prompt window to the directory where you copied the sample files and run the following command to register the sample **NumberGuesser** app (the hosted app). Change `D:\repos\HostedApps` to the path where you copied the sample files.

    ```CMD
    D:\repos\HostedApps>pyscriptengine -Register D:\repos\HostedApps\NumberGuesser\AppxManifest.xml
    ```

    > [!NOTE]
    > You can run `pyscriptengine` on the command line because the host in the sample declares an [**AppExecutionAlias**](/uwp/schemas/appxpackage/uapmanifestschema/element-uap5-appexecutionalias).

6. Open the **Start** menu and click **NumberGuesser** to run the hosted app.

## Related topics

* [Create an unsigned MSIX package](/windows/msix/package/unsigned-package)
