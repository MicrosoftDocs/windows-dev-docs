---
ms.assetid: 2f76c520-84a3-4066-8eb3-ecc0ecd198a7
title: Windows Desktop Bridge app tests
description: Use the Desktop Bridge's built-in tests to ensure that your desktop app is optimized for its conversion to a UWP app.
ms.date: 12/18/2017
ms.topic: article
keywords: windows 10, uwp, app certification
ms.localizationpriority: medium
---
# Windows Desktop Bridge app tests

Desktop Bridge apps (see [Building an MSIX package from your code](/windows/msix/desktop/source-code-overview)) are Windows desktop apps converted to Universal Windows Platform (UWP) apps by using the [Desktop Bridge](/windows/msix/desktop/source-code-overview). After conversion, the Windows desktop application is packaged, serviced, and deployed in the form of a UWP app package (a .appx or .appxbundle) targeting Windows 10 Desktop.

## Required versus optional tests
Optional tests for Windows Desktop Bridge apps are informational only and will not be used to evaluate your app during Microsoft Store onboarding. We recommend investigating these test results to produce better quality apps. The overall pass/fail criteria for store onboarding is determined by the required tests and not by these optional tests.

## Current optional tests

### 1. Digitally signed file test 
**Background**  
This test verifies that all portable executable (PE) files contain a valid signature. The presence of digitally signed files allows users to know that the software is genuine.

**Test details**  
The test scans all of the portable executable files in the package and checks their headers for a signature. All the PE files are recommended to be digitally signed. A warning will be generated if any of the PE files is not signed.
 
**Corrective actions**  
Having digitally signed files is always recommended. For more information, see [Introduction to Code Signing](/previous-versions/windows/internet-explorer/ie-developer/platform-apis/ms537361(v=vs.85)).

### 2. File association verbs 
**Background**  
This test scans the package registry to check if any file association verbs are registered. 

**Test details**  
Converted desktop apps can be enhanced with a wide range of Windows Runtime APIs. This test checks that the UWP binaries in the app don’t call non-Windows Runtime APIs. UWP binaries have the **AppContainer** flag set.

**Corrective actions**  
See [Desktop to UWP Bridge: App extensions](/windows/apps/desktop/modernize/desktop-to-uwp-extensions) for an explanation of these extensions and how to use them properly. 

### 3. Debug configuration test
This test verifies that the .msix or .appx is not a debug build.
 
**Background**  
To be certified for the Microsoft Store, apps must not be compiled for debug and they must not reference debug versions of an executable file. In addition, you must build your code as optimized for your app to pass this test.
 
**Test details**  
Test the app to make sure it is not a debug build and is not linked to any debug frameworks.
 
**Corrective actions**  
* Build the app as a release build before you submit it to the Microsoft Store.
* Make sure that you have the correct version of .NET framework installed.
* Make sure the app isn't linking to debug versions of a framework and that it is building with a release version. If this app contains .NET components, make sure that you have installed the correct version of the .NET framework.

### 4. Package sanity test
#### 4.1 Archive files usage

**Background**  
This test helps you build better Desktop Bridge Apps to run on [Windows 10 S](https://www.microsoft.com/windows/windows-10-s) machines.

**Test details**  
This test checks for all executable files inside of archived files or self-extracting content. As executable files contained within this type of content are not signed during onboarding to Windows store, the app might not run as expected on Windows 10 S systems.
 
**Corrective actions**
* Consider evaluating the files flagged by the test to determine if there is impact to your app running in a Windows 10 S environment.
* If your app would be affected, remove the executable files from the archived files and do not use self-extracting archives to place executable files on disk. This should prevent the loss of app functionality.

#### 4.2 Blocked executables

**Background**  
This test helps you build better Desktop Bridge Apps to run on [Windows 10 S](https://www.microsoft.com/windows/windows-10-s) machines. 

**Test details**  
This test checks whether the app is attempting to launch executable files, which is restricted on Windows 10 S systems. Apps that rely on this capability may not run as expected on Windows 10 S systems. 

**Corrective actions**  
* Identify which of the flagged entries from the test represent a call to launch an executable file that is not part of your app, and remove those calls. 
* If the flagged file(s) is part of your application, you may ignore the warning.


## Current required tests

### 1. App Capabilities test (special use capabilities)

**Background**  
Special use capabilities are intended for very specific scenarios. Only company accounts are allowed to use these capabilities. 

**Test details**  
Validate if the app is declaring any of the below capabilities: 
* EnterpriseAuthentication
* SharedUserCertificates
* DocumentsLibrary

If any of these capabilities are declared, the test will display a warning to the user. 

**Corrective actions**  
Consider removing the special use capability if your app doesn't require it. Additionally, use of these capabilities is subject to additional onboarding policy review.

### 2. App manifest resources tests 
#### 2.1 App resources validation
Your app might not install properly if the strings or images declared in the app’s manifest are incorrect. If the app does install with these errors, your app’s logo or other images may not display correctly.	

**Test details**  
Inspects the resources defined in the app manifest to make sure they are present and valid.

**Corrective action**  
Use the following table as guide.

Error message | Comments
--------------|---------
The image {image name} defines both Scale and TargetSize qualifiers; you can define only one qualifier at a time. | You can customize images for different resolutions. In the actual message, {image name} contains the name of the image with the error. Make sure that each image defines either Scale or TargetSize as the qualifier. 
The image {image name} failed the size restrictions.  | Ensure that all the app images adhere to the proper size restrictions. In the actual message, {image name} contains the name of the image with the error. 
The image {image name} is missing from the package.  | A required image is missing. In the actual message, {image name} contains the name of the image that is missing. 
The image {image name} is not a valid image file.  | Ensure that all the app images adhere to the proper file format type restrictions. In the actual message, {image name} contains the name of the image that is not valid. 
The image "BadgeLogo" has an ABGR value {value} at position (x, y) that is not valid. The pixel must be white (##FFFFFF) or transparent (00######)  | The badge logo is an image that appears next to the badge notification to identify the app on the lock screen. This image must be monochromatic (it can contain only white and transparent pixels). In the actual message, {value} contains the color value in the image that is not valid. 
The image "BadgeLogo" has an ABGR value {value} at position (x, y) that is not valid for a high-contrast white image. The pixel must be (##2A2A2A) or darker, or transparent (00######).  | The badge logo is an image that appears next to the badge notification to identify the app on the lock screen. Because the badge logo appears on a white background when in high-contrast white, it must be a dark version of the normal badge logo. In high-contrast white, the badge logo can only contain pixels that are darker than (##2A2A2A) or transparent. In the actual message, {value} contains the color value in the image that is not valid. 
The image must define at least one variant without a TargetSize qualifier. It must define a Scale qualifier or leave Scale and TargetSize unspecified, which defaults to Scale-100.  | For more info, see the guides on [responsive design](/windows/apps/design/layout/screen-sizes-and-breakpoints-for-responsive-design) and [app resources](/windows/apps/design/app-settings/store-and-retrieve-app-data). 
The package is missing a "resources.pri" file.  | If you have localizable content in your app manifest, make sure that your app's package includes a valid resources.pri file. 
The "resources.pri" file must contain a resource map with a name that matches the package name {package full name}  | You can get this error if the manifest changed and the name of the resource map in resources.pri no longer matches the package name in the manifest. In the actual message, {package full name} contains the package name that resources.pri must contain. To fix this, you need to rebuild resources.pri and the easiest way to do that is by rebuilding the app's package. 
The "resources.pri" file must not have AutoMerge enabled.  | MakePRI.exe supports an option called AutoMerge. The default value of AutoMerge is off. When enabled, AutoMerge merges an app's language pack resources into a single resources.pri at runtime. We don't recommend this for apps that you intend to distribute through the Microsoft Store. The resources.pri of an app that is distributed through the Microsoft Store must be in the root of the app's package and contain all the language references that the app supports. 
The string {string} failed the max length restriction of {number} characters.  | Refer to the [App package requirements](/windows/apps/publish/publish-your-app/app-package-requirements?pivots=store-installer-msix). In the actual message, {string} is replaced by the string with the error and {number} contains the maximum length. 
The string {string} must not have leading/trailing whitespace.  | The schema for the elements in the app manifest don't allow leading or trailing white space characters. In the actual message, {string} is replaced by the string with the error. Make sure that none of the localized values of the manifest fields in resources.pri have leading or trailing white space characters. 
The string must be non-empty (greater than zero in length)  | For more info, see [App package requirements](/windows/apps/publish/publish-your-app/app-package-requirements?pivots=store-installer-msix). 
There is no default resource specified in the "resources.pri" file.  | For more info, see the guide on [app resources](/windows/apps/design/app-settings/store-and-retrieve-app-data). In the default build configuration, Visual Studio only includes scale-200 image resources in the app package when generating bundles, putting other resources in the resource package. Make sure you either include scale-200 image resources or configure your project to include the resources you have. 
There is no resource value specified in the "resources.pri" file.  | Make sure that the app manifest has valid resources defined in resources.pri. 
The image file {filename} must be smaller than 204800 bytes.  | Reduce the size of the indicated images. 
The {filename} file must not contain a reverse map section.  | While the reverse map is generated during Visual Studio 'F5 debugging' when calling into makepri.exe, it can be removed by running makepri.exe without the /m parameter when generating a pri file. 



#### 2.2 Branding validation
**Background**  
Desktop Bridge Apps are expected to be complete and fully functional. Apps using the default images (from templates or SDK samples) present a poor user experience and cannot be easily identified in the store catalog.

**Test details**  
The test will validate if the images used by the app are not default images either from SDK samples or from Visual Studio. 

**Corrective actions**  
Replace default images with something more distinct and representative of your app.

### 3. Package compliance tests
#### 3.1 App Manifest
Tests the contents of app manifest to make sure its contents are correct.

**Background**  
Apps must have a correctly formatted app manifest.

**Test details**  
Examines the app manifest to verify the contents are correct as described in the [App package requirements](/windows/apps/publish/publish-your-app/app-package-requirements?pivots=store-installer-msix). The following checks are done in this test:
* **File extensions and protocols**  
Your app may declare the file types that it can be associated with. A declaration of a large number of uncommon file types makes for a poorer user experience. This test limits the number of file extensions that an app can be associated with.
* **Framework dependency rule**  
This test enforces the requirement that apps declare appropriate dependencies on the UWP. If there is an inappropriate dependency, this test will fail. If there is a mismatch between the OS version the app targets and the framework dependencies made, the test will fail. The test also fails if the app refers to any "preview" versions of the framework dlls.
* **Inter-process communication (IPC) verification**  
This test enforces the requirement that Desktop Bridge apps do not communicate outside of the app container to desktop components. Inter-process communication is intended for side-loaded apps only. Apps that specify the [**ActivatableClassAttribute**](/uwp/schemas/appxpackage/appxmanifestschema/element-activatableclassattribute) with name equal to `DesktopApplicationPath` will fail this test.  

**Corrective action**  
Review the app's manifest against the requirements described in the [App package requirements](/windows/apps/publish/publish-your-app/app-package-requirements?pivots=store-installer-msix).


#### 3.2 Application Count
This test verifies that an app package (.appx, app bundle) contains one application. 

**Background**  
This test is implemented as per Store policy. 

**Test details**  
This test verifies that the total number of .appx packages in the bundle is less than 512, and that there is only one "main" package in the bundle. It also verifies that the revision number of the bundle version is set to 0. 

**Corrective actions**  
Ensure the app package and bundle meet requirements stated in **Test details**.


#### 3.3 Registry Checks
**Background**  
This test checks if the application installs or updates any new services or drivers.

**Test details**  
The test looks inside the registry.dat file for updates to specific registry locations that indicate a new service or driver being registered. If the app is attempting to install a driver or service, the test will fail.  

**Corrective actions**  
Review the failures and remove the services or drivers in question if they are unnecessary. If the app depends on these, you will need to revise the app if you want to onboard to the Store.


### 4. Platform appropriate files test
Apps that install mixed binaries may crash or not run correctly depending on the user’s processor architecture. 

**Background**  
This test scans the binaries in an app package for architecture conflicts. An app package should not include binaries that can't be used on the processor architecture specified in the manifest. Including unsupported binaries can lead to your app crashing or an unnecessary increase in the app package size. 

**Test details**  
Validates that each file's "bitness" in the portable executable header is appropriate when cross-referenced with the app package processor architecture declaration. 

**Corrective actions**  
Follow these guidelines to ensure that your app package only contains files supported by the architecture specified in the app manifest: 
* If the Target Processor Architecture for your app is Neutral Processor Type, the app package cannot contain x86, x64, or Arm binary or image type files.
* If the Target Processor Architecture for your app is x86 processor type, the app package must only contain x86 binary or image type files. If the package contains x64 or Arm binary or image types, it will fail the test.
* If the Target Processor Architecture for your app is x64 processor type, the app package must contain x64 binary or image type files. Note that in this case the package can also include x86 files, but the primary app experience should utilize the x64 binary. If the package contains Arm binary or image type files, or *only* contains x86 binaries or image type files, it will fail the test.
* If the Target Processor Architecture for your app is Arm processor type, the app package must only contain Arm binary or image type files. If the package contains x64 or x86 binary or image type files, it will fail the test. 

### 5. Supported API test
Checks the app for the use of any non-compliant APIs. 

**Background**  
Desktop Bridge apps can leverage some legacy Win32 APIs along with modern APIs (UWP components). This test identifies managed binaries that use unsupported APIs.
 
**Test details**  
This test checks all the UWP components in the app:
* Verifies that each managed binary within the app package doesn't have a dependency on a Win32 API that is not supported for UWP app development by checking the import address table of the binary.
* Verifies that each managed binary within the app package doesn't have a dependency on a function outside of the approved profile. 

**Corrective actions**  
This can be corrected by ensuring that the app was compiled as a release build and not as a debug build. 

> [!NOTE]
> The debug build of an app will fail this test even if the app uses only [APIs for UWP apps](/uwp/). Review the error messages to identify the API present that is not an allowed API for UWP apps. 

> [!NOTE]
> C++ apps that are built in a debug configuration will fail this test even if the configuration only uses APIs from the Windows SDK for UWP apps. See [Alternatives to Windows APIs in UWP apps](/uwp/win32-and-com/win32-and-com-for-uwp-apps) for more information.

### 6. User account control (UAC) test  

**Background**  
Ensures that the app is not requesting user account control at runtime.

**Test details**  
An app cannot request admin elevation or UIAccess per Microsoft Store policy. Elevated security permissions are not supported. 

**Corrective actions**  
Apps must run as an interactive user. See [UI Automation Security Overview](/dotnet/framework/ui-automation/ui-automation-security-overview) for details.

 
### 7. Windows Runtime metadata validation
**Background**  
Ensures that the components that ship in an app conform to the UWP type system.

**Test details**  
This test throws a number of flags related to proper type usage.

**Corrective actions**  
* **ExclusiveTo attribute**  
Ensure that UWP classes don't implement interfaces that are marked as ExclusiveTo another class
* **General Metadata correctness**  
Ensure that the compiler you are using to generate your types is up-to-date with the UWP specifications.
* **Properties**  
Ensure that all properties in a UWP class have a `get` method (`set` methods are optional). For all properties, ensure that the type returned by the `get` method matches the type of the `set` method input parameter.
* **Type location**  
Ensure that the metadata for all UWP types is located in the .winmd file that has the longest namespace-matching name in the app package.
* **Type name case-sensitivity**  
Ensure that all UWP types have unique, case-insensitive names within your app package. Also ensure that no UWP type name is also used as a namespace name within your app package.
* **Type name correctness**  
Ensure there are no UWP types in the global namespace or in the Windows top-level namespace.
 

### 8. Windows security features tests
Changing the default Windows security protections can put customers at increased risk. 

#### 8.1 Banned File Analyzer
**Background**  
Certain files have been updated with important security, reliability or other improvements. Windows Desktop Bridge apps must contain the latest versions these files, as outdated versions present a risk. The Windows App Certification Kit blocks these files to ensure that all apps use the current version.

**Test details**  
The Banned File Check in the Windows App Certification Kit currently checks for the following files:
* *Bing.Maps.JavaScript\js\veapicore.js*  
This check commonly fails when an app is using a "Release Preview" version of the file instead of the latest official release. 

**Corrective actions**  
To correct this, use the latest version of the [Bing Maps SDK](https://www.bingmapsportal.com/) for UWP apps.

#### 8.2 Private Code Signing
Tests for the existence of private code signing binaries within the app package. 

**Background**  
Private code signing files should be kept private as they may be used for malicious purposes in the event they are compromised. 

**Test details**  
Checks for files within the app package that have an extension of .pfx or .snk that would indicate that private signing keys are included. 

**Corrective actions**  
Remove any private code signing keys (such as .pfx and .snk files) from the package.


## Related topics

* [Microsoft Store Policies](/windows/apps/publish/store-policies-and-code-of-conduct)