---
ms.assetid: 1526FF4B-9E68-458A-B002-0A5F3A9A81FD
title: Windows App Certification Kit tests
description: The Windows App Certification Kit contains a number of tests that can help ensure that your app is ready to be published on the Microsoft Store.
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, app certification
ms.localizationpriority: medium
---

# Windows App Certification Kit tests

The [Windows App Certification Kit](windows-app-certification-kit.md) contains a number of tests that help ensure your app is ready to be published to the Microsoft Store. The tests are listed below with their criteria, details, and suggested actions in the case of failure.

## Deployment and launch tests

Monitors the app during certification testing to record when it crashes or hangs.

### Background

Apps that stop responding or crash can cause the user to lose data and have a poor experience.

We expect apps to be fully functional without the use of Windows compatibility modes, AppHelp messages, or compatibility fixes.

Apps must not list DLLs to load in the HKEY\-LOCAL\-MACHINE\\Software\\Microsoft\\Windows NT\\CurrentVersion\\Windows\\AppInit\-DLLs registry key.

### Test details

We test the app resilience and stability throughout the certification testing.

The Windows App Certification Kit calls [**IApplicationActivationManager::ActivateApplication**](/windows/desktop/api/shobjidl_core/nf-shobjidl_core-iapplicationactivationmanager-activateapplication) to launch apps. For **ActivateApplication** to launch an app, User Account Control (UAC) must be enabled and the screen resolution must be at least 1024 x 768 or 768 x 1024. If either condition is not met, your app will fail this test.

### Corrective actions

Make sure UAC is enabled on the test computer.

Make sure you are running the test on a computer with large enough screen.

If your app fails to launch and your test platform satisfies the prerequisites of [**ActivateApplication**](/windows/desktop/api/shobjidl_core/nf-shobjidl_core-iapplicationactivationmanager-activateapplication), you can troubleshoot the problem by reviewing the activation event log. To find these entries in the event log:

1. Open eventvwr.exe and navigate to the Application and Services Log\\Microsoft\\Windows\\Immersive-Shell folder.
2. Filter the view to show Event Ids: 5900-6000.
3. Review the log entries for info that might explain why the app didn't launch.

Troubleshoot the file with the problem, identify and fix the problem. Rebuild and re-test the app. You can also check if a dump file was generated in the Windows App Certification Kit log folder that can be used to debug your app.

## Platform Version Launch test

Checks that the Windows app can run on a future version of the OS. This test has historically been only applied to the Desktop app workflow, but this is now enabled for the Store and Universal Windows Platform (UWP) workflows.

### Background

Operating system version info has restricted usage for the Microsoft Store. This has often been incorrectly used by apps to check OS version so that the app can provide users with functionality that is specific to an OS version.

### Test details

The Windows App Certification Kit uses the HighVersionLie to detect how the app checks the OS version. If the app crashes, it will fail this test.

### Corrective action

Apps should use Version API helper functions to check this. See [Operating System Version](/windows/desktop/SysInfo/operating-system-version) for more information.

## Background tasks cancellation handler validation

This verifies that the app has a cancellation handler for declared background tasks. There needs to be a dedicated function that will be called when the task is cancelled. This test is applied only for deployed apps.

### Background

Store apps can register a process that runs in the background. For example, an email app may ping a server from time to time. However, if the OS needs these resources, it will cancel the background task, and apps should gracefully handle this cancellation. Apps that don't have a cancellation handler may crash or not close when the user tries to close the app.

### Test details

The app is launched, suspended and the non-background portion of the app is terminated. Then the background tasks associated with this app are cancelled. The state of the app is checked, and if the app is still running then it will fail this test.

### Corrective action

Add the cancellation handler to your app. For more information see [Support your app with background tasks](../launch-resume/support-your-app-with-background-tasks.md).

## App count

This verifies that an app package (.msix, .appx, or app bundle) contains one application. This was changed in the kit to be a standalone test.

### Background

This test was implemented as per Store policy.

### Test details

For Windows Phone 8.1 apps the test verifies the total number of .appx packages in the bundle is &lt; 512, there is only one main package in the bundle, and that the architecture of the main package in the bundle is marked as Arm or neutral.

For Windows 10 apps the test verifies that the revision number in the version of the bundle is set to 0.

### Corrective action

Ensure the app package and bundle meet requirements above in Test details.

## App manifest compliance test

Test the contents of app manifest to make sure its contents are correct.

### Background

Apps must have a correctly formatted app manifest.

### Test details

Examines the app manifest to verify the contents are correct as described in the [App package requirements](/windows/apps/publish/publish-your-app/app-package-requirements?pivots=store-installer-msix).

-   **File extensions and protocols**

    Your app can declare the file extensions that it wants to associate with. Used improperly, an app can declare a large number of file extensions, most of which it may not even use, resulting in a bad user experience. This test will add a check to limit the number of file extensions that an app can associate with.

-   **Framework Dependency rule**

    This test enforces the requirement that apps take appropriate dependencies on the UWP. If there is an inappropriate dependency, this test will fail.

    If there is a mismatch between the OS version the app applies to and the framework dependencies made, the test will fail. The test would also fail if the app refers to any preview versions of the framework dlls.

-   **Inter-process Communication (IPC) verification**

    This test enforces the requirement that UWP apps do not communicate outside of the app container to Desktop components. Inter-process communication is intended for side-loaded apps only. Apps that specify the [**ActivatableClassAttribute**](/uwp/schemas/appxpackage/appxmanifestschema/element-activatableclassattribute) with name equal to "DesktopApplicationPath" will fail this test.

### Corrective action

Review the app's manifest against the requirements described in the [App package requirements](/windows/apps/publish/publish-your-app/app-package-requirements?pivots=store-installer-msix).

## Windows Security features test

### Background

Changing the default Windows security protections can put customers at increased risk.

### Test details

Tests the app's security by running the [BinScope Binary Analyzer](#binscope-binary-analyzer-tests).

The BinScope Binary Analyzer tests examine the app's binary files to check for coding and building practices that make the app less vulnerable to attack or to being used as an attack vector.

The BinScope Binary Analyzer tests check for the correct use of the following security-related features.

-   BinScope Binary Analyzer tests
-   Private Code Signing

### BinScope Binary Analyzer tests

The [BinScope Binary Analyzer](https://www.microsoft.com/download/details.aspx?id=44995) tests examine the app's binary files to check for coding and building practices that make the app less vulnerable to attack or to being used as an attack vector.

The BinScope Binary Analyzer tests check for the correct use of these security-related features:

-   [AllowPartiallyTrustedCallersAttribute](#binscope-1)
-   [/SafeSEH Exception Handling Protection](#binscope-2)
-   [Data Execution Prevention](#binscope-3)
-   [Address Space Layout Randomization](#binscope-4)
-   [Read/Write Shared PE Section](#binscope-5)
-   [AppContainerCheck](#appcontainercheck)
-   [ExecutableImportsCheck](#binscope-7)
-   [WXCheck](#binscope-8)

### <span id="binscope-1"></span>AllowPartiallyTrustedCallersAttribute

**Windows App Certification Kit error message:** APTCACheck Test failed

The AllowPartiallyTrustedCallersAttribute (APTCA) attribute enables access to fully trusted code from partially trusted code in signed assemblies. When you apply the APTCA attribute to an assembly, partially trusted callers can access that assembly for the life of the assembly, which can compromise security.

**What to do if your app fails this test**

Don't use the APTCA attribute on strong named assemblies unless your project requires it and the risks are well understood. In cases where it's required, make sure that all APIs are protected with appropriate code access security demands. APTCA has no effect when the assembly is a part of a Universal Windows Platform (UWP) app.

**Remarks**

This test is performed only on managed code (C#, .NET, etc.).

### <span id="binscope-2"></span>/SafeSEH Exception Handling Protection

**Windows App Certification Kit error message:** SafeSEHCheck Test failed

An exception handler runs when the app encounters an exceptional condition, such as a divide-by-zero error. Because the address of the exception handler is stored on the stack when a function is called, it could be vulnerable to a buffer overflow attacker if some malicious software were to overwrite the stack.

**What to do if your app fails this test**

Enable the /SAFESEH option in the linker command when you build your app. This option is on by default in the Release configurations of Visual Studio. Verify this option is enabled in the build instructions for all executable modules in your app.

**Remarks**

The test is not performed on 64-bit binaries or Arm chipset binaries because they don't store exception handler addresses on the stack.

### <span id="binscope-3"></span>Data Execution Prevention

**Windows App Certification Kit error message:** NXCheck Test failed

This test verifies that an app doesn't run code that is stored in a data segment.

**What to do if your app fails this test**

Enable the /NXCOMPAT option in the linker command when you build your app. This option is on by default in linker versions that support Data Execution Prevention (DEP).

**Remarks**

We recommend that you test your apps on a DEP-capable CPU and fix any failures you find that result from DEP.

### <span id="binscope-4"></span>Address Space Layout Randomization

**Windows App Certification Kit error message:** DBCheck Test failed

Address Space Layout Randomization (ASLR) loads executable images into unpredictable locations in memory, which makes it harder for malicious software that expects a program to be loaded at a certain virtual address to operate predictably. Your app and all components that your app uses must support ASLR.

**What to do if your app fails this test**

Enable the /DYNAMICBASE option in the linker command when you build your app. Verify that all modules that your app uses also use this linker option.

**Remarks**

Normally, ASLR doesn't affect performance. But in some scenarios there is a slight performance improvement on 32-bit systems. It is possible that performance could degrade in a highly congested system that have many images loaded in many different memory locations.

This test is performed only on apps written in unmanaged languages, such as by using C or C++.

### <span id="binscope-5"></span>Read/Write Shared PE Section

**Windows App Certification Kit error message:** SharedSectionsCheck Test failed.

Binary files with writable sections that are marked as shared are a security threat. Don't build apps with shared writable sections unless necessary. Use [**CreateFileMapping**](/windows/desktop/api/winbase/nf-winbase-createfilemappinga) or [**MapViewOfFile**](/windows/desktop/api/memoryapi/nf-memoryapi-mapviewoffile) to create a properly secured shared memory object.

**What to do if your app fails this test**

Remove any shared sections from the app and create shared memory objects by calling [**CreateFileMapping**](/windows/desktop/api/winbase/nf-winbase-createfilemappinga) or [**MapViewOfFile**](/windows/desktop/api/memoryapi/nf-memoryapi-mapviewoffile) with the proper security attributes and then rebuild your app.

**Remarks**

This test is performed only on apps written in unmanaged languages, such as by using C or C++.

### AppContainerCheck

**Windows App Certification Kit error message:** AppContainerCheck Test failed.

The AppContainerCheck verifies that the **appcontainer** bit in the portable executable (PE) header of an executable binary is set. Apps must have the **appcontainer** bit set on all .exe files and all unmanaged DLLs to execute properly.

**What to do if your app fails this test**

If a native executable file fails the test, make sure that you used the latest compiler and linker to build the file and that you use the */appcontainer* flag on the linker.

If a managed executable fails the test, make sure that you used the latest compiler and linker, such as Microsoft Visual Studio, to build the UWP app.

**Remarks**

This test is performed on all .exe files and on unmanaged DLLs.

### <span id="binscope-7"></span>ExecutableImportsCheck

**Windows App Certification Kit error message:** ExecutableImportsCheck Test failed.

A portable executable (PE) image fails this test if its import table has been placed in an executable code section. This can occur if you enabled .rdata merging for the PE image by setting the */merge* flag of the Visual C++ linker as */merge:.rdata=.text*.

**What to do if your app fails this test**

Don't merge the import table into an executable code section. Make sure that the */merge* flag of the Visual C++ linker is not set to merge the ".rdata" section into a code section.

**Remarks**

This test is performed on all binary code except purely managed assemblies.

### <span id="binscope-8"></span>WXCheck

**Windows App Certification Kit error message:** WXCheck Test failed.

The check helps to ensure that a binary does not have any pages that are mapped as writable and executable. This can occur if the binary has a writable and executable section or if the binary’s *SectionAlignment* is less than *PAGE\-SIZE*.

**What to do if your app fails this test**

Make sure that the binary does not have a writeable or executable section and that the binary's *SectionAlignment* value is at least equal to its *PAGE\-SIZE*.

**Remarks**

This test is performed on all .exe files and on native, unmanaged DLLs.

An executable may have a writable and executable section if it has been built with Edit and Continue enabled (/ZI). Disabling Edit and Continue will cause the invalid section to not be present.

*PAGE\-SIZE* is the default *SectionAlignment* for executables.

### Private Code Signing

Tests for the existence of private code signing binaries within the app package.

### Background

Private code signing files should be kept private as they may be used for malicious purposes in the event they are compromised.

### Test details

Tests for files within the app package that have an extension of .pfx or.snk that would indicate that private signing keys were included.

### Corrective actions

Remove any private code signing keys (for example, .pfx and .snk files) from the package.

## Supported API test

Test the app for the use of any non-compliant APIs.

### Background

Apps must use the APIs for UWP apps (Windows Runtime or supported Win32 APIs) to be certified for the Microsoft Store. This test also identifies situations where a managed binary takes a dependency on a function outside of the approved profile.

### Test details

-   Verifies that each binary within the app package doesn't have a dependency on a Win32 API that is not supported for UWP app development by checking the import address table of the binary.
-   Verifies that each managed binary within the app package doesn't have a dependency on a function outside of the approved profile.

### Corrective actions

Make sure that the app was compiled as a release build and not a debug build.

> **Note**  The debug build of an app will fail this test even if the app uses only [APIs for UWP apps](/uwp/).

Review the error messages to identify the API the app uses that is not an [API for UWP apps](/uwp/).

> **Note**  C++ apps that are built in a debug configuration will fail this test even if the configuration only uses APIs from the Windows SDK for UWP apps. See, [Alternatives to Windows APIs in UWP apps](/uwp/win32-and-com/win32-and-com-for-uwp-apps) for more info.

## Performance tests

The app must respond quickly to user interaction and system commands in order to present a fast and fluid user experience.

The characteristics of the computer on which the test is performed can influence the test results. The performance test thresholds for app certification are set such that low-power computers meet the customer’s expectation of a fast and fluid experience. To determine your app’s performance, we recommend that you test on a low-power computer, such as an Intel Atom processor-based computer with a screen resolution of 1366x768 (or higher) and a rotational hard drive (as opposed to a solid-state hard drive).

### Bytecode generation

As a performance optimization to accelerate JavaScript execution time, JavaScript files ending in the .js extension generate bytecode when the app is deployed. This significantly improves startup and ongoing execution times for JavaScript operations.

### Test Details

Checks the app deployment to verify that all .js files have been converted to bytecode.

### Corrective Action

If this test fails, consider the following when addressing the issue:

-   Verify that event logging is enabled.
-   Verify that all JavaScript files are syntactically valid.
-   Confirm that all previous versions of the app are uninstalled.
-   Exclude identified files from the app package.

### Optimized binding references

When using bindings, WinJS.Binding.optimizeBindingReferences should be set to true in order to optimize memory usage.

### Test Details

Verify the value of WinJS.Binding.optimizeBindingReferences.

### Corrective Action

Set WinJS.Binding.optimizeBindingReferences to **true** in the app JavaScript.

## App manifest resources test

### App resources validation

The app might not install if the strings or images declared in your app’s manifest are incorrect. If the app does install with these errors, your app’s logo or other images used by your app might not display correctly.

### Test Details

Inspects the resources defined in the app manifest to make sure they are present and valid.

### Corrective Action

Use the following table as guidance.

<table>
<tr><th>Error message</th><th>Comments</th></tr>
<tr><td>
<p>The image {image name} defines both Scale and TargetSize qualifiers; you can define only one qualifier at a time.</p>
</td><td>
<p>You can customize images for different resolutions.</p>
<p>In the actual message, {image name} contains the name of the image with the error.</p>
<p> Make sure that each image defines either Scale or TargetSize as the qualifier.</p>
</td></tr>
<tr><td>
<p>The image {image name} failed the size restrictions.</p>
</td><td>
<p>Ensure that all the app images adhere to the proper size restrictions.</p>
<p>In the actual message, {image name} contains the name of the image with the error.</p>
</td></tr>
<tr><td>
<p>The image {image name} is missing from the package.</p>
</td><td>
<p>A required image is missing.</p>
<p>In the actual message, {image name} contains the name of the image that is missing.</p>
</td></tr>
<tr><td>
<p>The image {image name} is not a valid image file.</p>
</td><td>
<p>Ensure that all the app images adhere to the proper file format type restrictions.</p>
<p>In the actual message, {image name} contains the name of the image that is not valid.</p>
</td></tr>
<tr><td>
<p>The image "BadgeLogo" has an ABGR value {value} at position (x, y) that is not valid. The pixel must be white (##FFFFFF) or transparent (00######)</p>
</td><td>
<p>The badge logo is an image that appears next to the badge notification to identify the app on the lock screen. This image must be monochromatic (it can contain only white and transparent pixels).</p>
<p>In the actual message, {value} contains the color value in the image that is not valid.</p>
</td></tr>
<tr><td>
<p>The image "BadgeLogo" has an ABGR value {value} at position (x, y) that is not valid for a high-contrast white image. The pixel must be (##2A2A2A) or darker, or transparent (00######).</p>
</td><td>
<p>The badge logo is an image that appears next to the badge notification to identify the app on the lock screen.   Because the badge logo  appears on a white background when in high-contrast white, it must be a dark version of the normal badge logo. In high-contrast white, the badge logo can only contain pixels that are darker than (##2A2A2A) or transparent.</p>
<p>In the actual message, {value} contains the color value in the image that is not valid.</p>
</td></tr>
<tr><td>
<p>The image must define at least one variant without a TargetSize qualifier. It must define a Scale qualifier or leave Scale and TargetSize unspecified, which defaults to Scale-100.</p>
</td><td>
<p>For more info, see <a href="/windows/uwp/layout/screen-sizes-and-breakpoints-for-responsive-design">Responsive design 101 for UWP apps</a> and <a href="/windows/uwp/app-settings/store-and-retrieve-app-data">Guidelines for app resources</a>.</p>
</td></tr>
<tr><td>
<p>The package is missing a "resources.pri" file.</p>
</td><td>
<p>If you have localizable content in your app manifest, make sure that your app's package includes a valid resources.pri file.</p>
</td></tr>
<tr><td>
<p>The "resources.pri" file must contain a resource map with a name that matches the package name  {package full name}</p>
</td><td>
<p>You can get this error if the manifest changed and  the name of the resource map in resources.pri no longer matches the package name in the manifest.</p>
<p>In the actual message, {package full name} contains the package name that resources.pri must contain.</p>
<p>To fix this, you need to rebuild resources.pri and the easiest way to do that is  by rebuilding the app's package.</p>
</td></tr>
<tr><td>
<p>The "resources.pri" file must not have AutoMerge enabled.</p>
</td><td>
<p>MakePRI.exe supports an option called <strong>AutoMerge</strong>. The default value of <strong>AutoMerge</strong> is <strong>off</strong>. When enabled, <strong>AutoMerge</strong> merges an app's  language pack resources into a single resources.pri at runtime. We don't recommend this for apps that you intend to distribute through  the Microsoft Store. The resources.pri of an app that is distributed through the  Microsoft Store must be in  the root of the app's package and contain all the language references that the app supports.</p>
</td></tr>
<tr><td>
<p>The string {string} failed the max length restriction of {number} characters.</p>
</td><td>
<p>Refer to the <a href="/windows/apps/publish/publish-your-app/app-package-requirements">App package requirements</a>.</p>
<p>In the actual message, {string} is replaced by the string with the error and {number} contains the maximum length.</p>
</td></tr>
<tr><td>
<p>The string {string} must not have leading/trailing whitespace.</p>
</td><td>
<p>The schema for the elements in the app manifest don't allow leading or trailing white space characters.</p>
<p>In the actual message, {string} is replaced by the string with the error.</p>
<p>Make sure that none of the localized values of the manifest fields in resources.pri have leading or trailing white space characters.</p>
</td></tr>
<tr><td>
<p>The string must be non-empty (greater than zero in length)</p>
</td><td>
<p>For more info, see <a href="/windows/apps/publish/publish-your-app/app-package-requirements">App package requirements</a>.</p>
</td></tr>
<tr><td>
<p>There is no default resource specified in the "resources.pri" file.</p>
</td><td>
<p>For more info, see <a href="/windows/uwp/app-settings/store-and-retrieve-app-data">Guidelines for app resources</a>.</p>
<p>In the default build configuration,  Visual Studio only includes scale-200 image resources in the app package when generating bundles, putting other resources in the resource package. Make sure  you either include scale-200 image resources or configure your project to include the resources you have.</p>
</td></tr>
<tr><td>
<p>There is no resource value specified in the "resources.pri" file.</p>
</td><td>
<p>Make sure that the app manifest has valid resources defined in resources.pri.</p>
</td></tr>
<tr><td>
<p>The image file {filename} must be smaller than 204800 bytes.\*\*</p>
</td><td>
<p>Reduce the size of the indicated images.</p>
</td></tr>
<tr><td>
<p>The {filename} file must not contain a reverse map section.\*\*</p>
</td><td>
<p>While the reverse map is generated during Visual Studio 'F5 debugging' when calling into makepri.exe, it can be removed by running makepri.exe without the /m parameter when generating a pri file.</p>
</td></tr>
<tr><td colspan="2">
<p>\*\* Indicates that a test was added in the Windows App Certification Kit 3.3 for Windows 8.1 and is only applicable when using the that version of the kit or later.</p>
</td></tr>
</table>



 

### Branding validation

UWP apps are expected to be complete and fully functional. Apps using the default images (from templates or SDK samples) present a poor user experience and cannot be easily identified in the store catalog.

### Test Details

The test will validate if the images used by the app are not default images either from SDK samples or from Visual Studio.

### Corrective actions

Replace default images with something more distinct and representative of your app.

## Debug configuration test

Test the app to make sure it is not a debug build.

### Background

To be certified for the Microsoft Store, apps must not be compiled for debug and they must not reference debug versions of an executable file. In addition, you must build your code as optimized for your app to pass this test.

### Test details

Test the app to make sure it is not a debug build and is not linked to any debug frameworks.

### Corrective actions

-   Build the app as a release build before you submit it to the Microsoft Store.
-   Make sure that you have the correct version of .NET framework installed.
-   Make sure the app isn't linking to debug versions of a framework and that it is building with a release version. If this app contains .NET components, make sure that you have installed the correct version of the .NET framework.

## File encoding test

### UTF-8 file encoding

### Background

HTML, CSS, and JavaScript files must be encoded in UTF-8 form with a corresponding byte-order mark (BOM) to benefit from bytecode caching and avoid certain runtime error conditions.

### Test details

Test the contents of app packages to make sure that they use the correct file encoding.

### Corrective Action

Open the affected file and select **Save As** from the **File** menu in Visual Studio. Select the drop-down control next to the **Save** button and select **Save with Encoding**. From the **Advanced** save options dialog, choose the Unicode (UTF-8 with signature) option and click **OK**.

## Direct3D feature level test

### Direct3D feature level support

Tests Microsoft Direct3D apps to ensure that they won't crash on devices with older graphics hardware.

### Background

Microsoft Store requires all applications using Direct3D to render properly or fail gracefully on feature level 9\-1 graphics cards.

Because users can change the graphics hardware in their device after the app is installed, if you choose a minimum feature level higher than 9\-1, your app must detect at launch whether or not the current hardware meets the minimum requirements. If the minimum requirements are not met, the app must display a message to the user detailing the Direct3D requirements. Also, if an app is downloaded on a device with which it is not compatible, it should detect that at launch and display a message to the customer detailing the requirements.

### Test Details

The test will validate if the apps render accurately on feature level 9\-1.

### Corrective Action

Ensure that your app renders correctly on Direct3D feature level 9\-1, even if you expect it to run at a higher feature level. See [Developing for different Direct3D feature levels](/previous-versions/windows/apps/hh994923(v=win.10)) for more info.

### Direct3D Trim after suspend

> **Note**  This test only applies to UWP apps developed for Windows 8.1 and later.

### Background

If the app does not call [**Trim**](/windows/desktop/api/dxgi1_3/nf-dxgi1_3-idxgidevice3-trim) on its Direct3D device, the app will not release memory allocated for its earlier 3D work. This increases the risk of apps being terminated due to system memory pressure.

### Test Details

Checks apps for compliance with d3d requirements and ensures that apps are calling a new [**Trim**](/windows/desktop/api/dxgi1_3/nf-dxgi1_3-idxgidevice3-trim) API upon their Suspend callback.

### Corrective Action

The app should call the [**Trim**](/windows/desktop/api/dxgi1_3/nf-dxgi1_3-idxgidevice3-trim) API on its [**IDXGIDevice3**](/windows/desktop/api/dxgi1_3/nn-dxgi1_3-idxgidevice3) interface anytime it is about to be suspended.

## App Capabilities test

### Special use capabilities

### Background

Special use capabilities are intended for very specific scenarios. Only company accounts are allowed to use these capabilities.

### Test Details

Validate if the app is declaring any of the below capabilities:

-   EnterpriseAuthentication
-   SharedUserCertificates
-   DocumentsLibrary

If any of these capabilities are declared, the test will display a warning to the user.

### Corrective Actions

Consider removing the special use capability if your app doesn't require it. Additionally, use of these capabilities are subject to additional on-boarding policy review.

## Windows Runtime metadata validation

### Background

Ensures that the components that ship in an app conform to the Windows Runtime type system.

### Test Details

Verifies that the **.winmd** files in the package conform to Windows Runtime rules.

### Corrective Actions

-   **ExclusiveTo attribute test:** Ensure that Windows Runtime classes don't implement interfaces that are marked as ExclusiveTo another class.
-   **Type location test:** Ensure that the metadata for all Windows Runtime types is located in the winmd file that has the longest namespace-matching name in the app package.
-   **Type name case-sensitivity test:** Ensure that all Windows Runtime types have unique, case-insensitive names within your app package. Also ensure that no UWP type name is also used as a namespace name within your app package.
-   **Type name correctness test:** Ensure there are no Windows Runtime types in the global namespace or in the Windows top-level namespace.
-   **General metadata correctness test:** Ensure that the compiler you are using to generate your types is up to date with the Windows Runtime specifications.
-   **Properties test:** ensure that all properties on a Windows Runtime class have a get method (set methods are optional). Ensure that the type of the get method return value matches the type of the set method input parameter, for all properties on Windows Runtime types.

## Package Sanity tests

### Platform appropriate files test

Apps that install mixed binaries may crash or not run correctly depending upon the user’s processor architecture.

### Background

This test validates the binaries in an app package for architecture conflicts. An app package should not include binaries that can't be used on the processor architecture specified in the manifest. Including unsupported binaries can lead to your app crashing or an unnecessary increase in the app package size.

### Test Details

Validates that each file's "bitness" in the PE header is appropriate when cross-referenced with the app package processor architecture declaration

### Corrective Action

Follow these guidelines to ensure that your app package only contains files supported by the architecture specified in the app manifest:

-   If the Target Processor Architecture for your app is Neutral processor Type, the app package cannot contain x86, x64, or Arm binary or image type files.

-   If the Target Processor Architecture for your app is x86 processor type, the app package must only contain x86 binary or image type files. If the package contains x64 or Arm binary or image types, it will fail the test.

-   If the Target Processor Architecture for your app is x64 processor type, the app package must contain x64 binary or image type files. Note that in this case the package can also include x86 files, but the primary app experience should utilize the x64 binary.

    However, if the package contains Arm binary or image type files, or only contains x86 binaries or image type files, it will fail the test.

-   If the Target Processor Architecture for your app is Arm processor type, the app package must only contain Arm binary or image type files. If the package contains x64 or x86 binary or image type files, it will fail the test.

### Supported Directory Structure test

Validates that applications are not creating subdirectories as part of installation that are longer than MAX\-PATH.

### Background

OS components (including Trident, WWAHost, etc.) are internally limited to MAX\-PATH for file system paths and will not work correctly for longer paths.

### Test Details

Verifies that no path within the app install directory exceeds MAX\-PATH.

### Corrective Action

Use a shorter directory structure, and or file name.

## Resource Usage test

### WinJS Background Task test

WinJS background task test ensures that JavaScript apps have the proper close statements so apps don’t consume battery.

### Background

Apps that have JavaScript background tasks need to call Close() as the last statement in their background task. Apps that do not do this could keep the system from returning to connected standby mode and result in draining the battery.

### Test Details

If the app does not have a background task file specified in the manifest, the test will pass. Otherwise the test will parse the JavaScript background task file that is specified in the app package, and look for a Close() statement. If found, the test will pass; otherwise the test will fail.

### Corrective Action

Update the background JavaScript code to call Close() correctly.


## Related topics

* [Windows Desktop Bridge app tests](windows-desktop-bridge-app-tests.md)
* [Microsoft Store Policies](/windows/apps/publish/store-policies-and-code-of-conduct)
 
