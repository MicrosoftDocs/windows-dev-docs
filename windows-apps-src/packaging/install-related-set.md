---
author: laurenhughes
title: Install a related set using an App Installer file
description: In this section, we will review the steps you need to take to allow the installation of a related set via App Installer. We will also go through the steps to construct a *.appinstaller file that will define your related set.
ms.author: lahugh
ms.date: 1/4/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, app installer, AppInstaller, sideload, related set, optional packages
ms.localizationpriority: medium
---

# Install a related set using an App Installer file

If you're just starting out with UWP optional packages or related sets, the following articles are good resources to get started. 

1.  [Extend your application using Optional Packages](https://blogs.msdn.microsoft.com/appinstaller/2017/04/05/uwpoptionalpackages/)
2.  [Build your first Optional Package](https://blogs.msdn.microsoft.com/appinstaller/2017/05/09/build-your-first-optional-package/)
3.  [Loading code from an optional package](https://blogs.msdn.microsoft.com/appinstaller/2017/05/11/loading-code-from-an-optional-package/)
4.  [Tooling to create a Related Set](https://blogs.msdn.microsoft.com/appinstaller/2017/05/12/tooling-to-create-a-related-set/)
5.  [Optional packages and related set authoring](https://docs.microsoft.com/windows/uwp/packaging/optional-packages)

With the Windows 10 Fall Creators Update, related sets can now be installed via App Installer. This allows distribution and deployment of related set app packages to users. 

## How to install a related set 
A related set is not one entity, but rather a combination of a main package and optional packages. To be able to install a related set as one entity, we must be able to specify the main package and optional package as one. To do this, we will need to create an XML file with a ".appinstaller" extension to define a related set. App Installer consumes the *.appinstaller file and allows the user to install all of the defined packages with a single click. 

Before we go in to more detail, here is a complete sample *.appinstaller file:

```xml
<?xml version="1.0" encoding="utf-8"?>
<AppInstaller
    xmlns="http://schemas.microsoft.com/appx/appinstaller/2017/2"
    Version="1.0.0.0" 
    Uri="http://mywebservice.azurewebsites.net/appset.appinstaller" > 
   
    <MainBundle 
        Name="Contoso.MainApp"
        Publisher="CN=Contoso"
        Version="2.23.12.43"
        Uri="http://mywebservice.azurewebsites.net/mainapp.appxbundle" />
        
    <OptionalPackages>
        <Bundle
            Name="Contoso.OptionalApp1"
            Publisher="CN=Contoso"
            Version="2.23.12.43"
            Uri="http://mywebservice.azurewebsites.net/OptionalApp1.appxbundle" />

        <Bundle
            Name="Contoso.OptionalApp2"
            Publisher="CN=Contoso"
            Version="2.23.12.43"
            Uri="http://mywebservice.azurewebsites.net/OptionalApp2.appxbundle" />

        <Package
            Name="Fabrikam.OptionalApp3"
            Publisher="CN=Fabrikam"
            Version="10.34.54.23"
            Uri="http://mywebservice.azurewebsites.net/OptionalApp3.appx" />

    </OptionalPackages>

</AppInstaller>
```

During deployment, the App Installer file is validated against the app packages referenced in the `Uri` element. So, the `Name`, `Publisher` and `Version` should match the [Package/Identity](https://docs.microsoft.com/uwp/schemas/appxpackage/uapmanifestschema/element-identity) element in the app package manifest. 

## How to create an App Installer file 

To distribute your related set as one entity, you must create an App Installer file that contains the elements that are required by that [appinstaller schema](https://docs.microsoft.com/uwp/schemas/appinstallerschema/app-installer-file).

### Step 1: Create the *.appinstaller file
Using a text editor, create a file (which will contain XML) and name it &lt;filename&gt;.appinstaller 

### Step 2: Add the basic template
The basic template includes the App Installer file information. 
```xml
<?xml version="1.0" encoding="utf-8"?>
<AppInstaller
    xmlns="http://schemas.microsoft.com/appx/appinstaller/2017/2"
    Version="1.0.0.0" 
    Uri="http://mywebservice.azurewebsites.net/appset.appinstaller" > 
</AppInstaller>
```

### Step 3: Add the main package information 
If the main app package is an .appxbundle file, then use the `<MainBundle>` shown below. If the main app package is an .appx file, then use `<MainPackage>` in place of `<MainBundle>` in the snippet. 

```xml
<?xml version="1.0" encoding="utf-8"?>
<AppInstaller
    xmlns="http://schemas.microsoft.com/appx/appinstaller/2017/2"
    Version="1.0.0.0" 
    Uri="http://mywebservice.azurewebsites.net/appset.appinstaller" > 
   
    <MainBundle 
        Name="Contoso.MainApp"
        Publisher="CN=Contoso"
        Version="2.23.12.43"
        Uri="http://mywebservice.azurewebsites.net/mainapp.appxbundle" />

</AppInstaller>
```
The information in the `<MainBundle>` or `<MainPackage>` attribute should match the [Package/Identity](https://docs.microsoft.com/uwp/schemas/appxpackage/uapmanifestschema/element-identity) element in the app bundle manifest or app package manifest respectively. 

### Step 4: Add the optional packages 
Similar to the main app package attribute, if the optional package can be either an app package or an app bundle, the child element within the `<OptionalPackages>` attribute should be `<Package>` or `<Bundle>` respectively. The package information in the child elements should match the identity element in the bundle or package manifest. 

``` xml
<?xml version="1.0" encoding="utf-8"?>
<AppInstaller
    xmlns="http://schemas.microsoft.com/appx/appinstaller/2017/2"
    Version="1.0.0.0" 
    Uri="http://mywebservice.azurewebsites.net/appset.appinstaller" > 
   
    <MainBundle 
        Name="Contoso.MainApp"
        Publisher="CN=Contoso"
        Version="2.23.12.43"
        Uri="http://mywebservice.azurewebsites.net/mainapp.appxbundle" />
        
    <OptionalPackages>
        <Bundle
            Name="Contoso.OptionalApp1"
            Publisher="CN=Contoso"
            Version="2.23.12.43"
            Uri="http://mywebservice.azurewebsites.net/OptionalApp1.appxbundle" />

        <Bundle
            Name="Contoso.OptionalApp2"
            Publisher="CN=Contoso"
            Version="2.23.12.43"
            Uri="http://mywebservice.azurewebsites.net/OptionalApp2.appxbundle" />

        <Package
            Name="Fabrikam.OptionalApp3"
            Publisher="CN=Fabrikam"
            Version="10.34.54.23"
            Uri="http://mywebservice.azurewebsites.net/OptionalApp3.appx" />

    </OptionalPackages>

</AppInstaller>
```

### Step 5: Add dependencies 
In the dependencies element, you can specify the required framework packages for the main package or the optional packages. 

``` xml
<?xml version="1.0" encoding="utf-8"?>
<AppInstaller
    xmlns="http://schemas.microsoft.com/appx/appinstaller/2017/2"
    Version="1.0.0.0" 
    Uri="http://mywebservice.azurewebsites.net/appset.appinstaller" > 
   
    <MainBundle 
        Name="Contoso.MainApp"
        Publisher="CN=Contoso"
        Version="2.23.12.43"
        Uri="http://mywebservice.azurewebsites.net/mainapp.appxbundle" />
        
    <OptionalPackages>
        <Bundle
            Name="Contoso.OptionalApp1"
            Publisher="CN=Contoso"
            Version="2.23.12.43"
            Uri="http://mywebservice.azurewebsites.net/OptionalApp1.appxbundle" />

        <Bundle
            Name="Contoso.OptionalApp2"
            Publisher="CN=Contoso"
            Version="2.23.12.43"
            Uri="http://mywebservice.azurewebsites.net/OptionalApp2.appxbundle" />

        <Package
            Name="Fabrikam.OptionalApp3"
            Publisher="CN=Fabrikam"
            Version="10.34.54.23"
            ProcessorArchitecture="x86"
            Uri="http://mywebservice.azurewebsites.net/OptionalApp3.appx" />

    </OptionalPackages>

    <Dependencies>
        <Package Name="Microsoft.VCLibs.140.00" Publisher="CN=Microsoft Corporation, O=Microsoft Corporation, L=Redmond, S=Washington, C=US" Version="14.0.24605.0" ProcessorArchitecture="x86" Uri="http://foobarbaz.com/fwkx86.appx" />
        <Package Name="Microsoft.VCLibs.140.00" Publisher="CN=Microsoft Corporation, O=Microsoft Corporation, L=Redmond, S=Washington, C=US" Version="14.0.24605.0" ProcessorArchitecture="x64" Uri="http://foobarbaz.com/fwkx64.appx" />
    </Dependencies>

</AppInstaller>
```

### Step 6: Add Update setting 
The App Installer file can also specify update setting so that the related sets can be automatically updated when a newer App Installer file is published. **<UpdateSettings>** is an optional element. Within  **<UpdateSettings>** the OnLaunch option specifies that update checks should be made on app launch, and HoursBetweenUpdateChecks="12" specifies that an update check should be made every 12 hours. If HoursBetweenUpdateChecks is not specified, the default interval used to check for updates is 24 hours.
``` xml
<?xml version="1.0" encoding="utf-8"?>
<AppInstaller
    xmlns="http://schemas.microsoft.com/appx/appinstaller/2017/2"
    Version="1.0.0.0" 
    Uri="http://mywebservice.azurewebsites.net/appset.appinstaller" > 
   
    <MainBundle 
        Name="Contoso.MainApp"
        Publisher="CN=Contoso"
        Version="2.23.12.43"
        Uri="http://mywebservice.azurewebsites.net/mainapp.appxbundle" />
        
    <OptionalPackages>
        <Bundle
            Name="Contoso.OptionalApp1"
            Publisher="CN=Contoso"
            Version="2.23.12.43"
            Uri="http://mywebservice.azurewebsites.net/OptionalApp1.appxbundle" />

        <Bundle
            Name="Contoso.OptionalApp2"
            Publisher="CN=Contoso"
            Version="2.23.12.43"
            Uri="http://mywebservice.azurewebsites.net/OptionalApp2.appxbundle" />

        <Package
            Name="Fabrikam.OptionalApp3"
            Publisher="CN=Fabrikam"
            Version="10.34.54.23"
            ProcessorArchitecture="x86"
            Uri="http://mywebservice.azurewebsites.net/OptionalApp3.appx" />

    </OptionalPackages>

    <Dependencies>
        <Package Name="Microsoft.VCLibs.140.00" Publisher="CN=Microsoft Corporation, O=Microsoft Corporation, L=Redmond, S=Washington, C=US" Version="14.0.24605.0" ProcessorArchitecture="x86" Uri="http://foobarbaz.com/fwkx86.appx" />
        <Package Name="Microsoft.VCLibs.140.00" Publisher="CN=Microsoft Corporation, O=Microsoft Corporation, L=Redmond, S=Washington, C=US" Version="14.0.24605.0" ProcessorArchitecture="x64" Uri="http://foobarbaz.com/fwkx64.appx" />
    </Dependencies>
    
    <UpdateSettings>
        <OnLaunch HoursBetweenUpdateChecks="12" />
    </UpdateSettings>

</AppInstaller>
```

For all of the details on the XML schema, see [App Installer file reference](https://docs.microsoft.com/uwp/schemas/appinstallerschema/app-installer-file).

> [!NOTE]
> 
> The App Installer file type is new in the Windows 10 Fall Creators Update. There is no support for deployment of UWP apps using an App Installer file on previous versions of Windows 10.
> It also should be noted that the **HoursBetweenUpdateChecks** element is new in the next major update to Windows 10.
