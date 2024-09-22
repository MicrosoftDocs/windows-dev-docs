---
title: Build and deploy an unpackaged app that uses the Preview and Experimental versions of the Windows App SDK 
description: This article provides a walkthrough for building and deploying an unpackaged app that uses the Preview and Experimental versions of Windows App SDK.
ms.topic: article
ms.date: 05/24/2021
keywords: windows win32, windows app development, Windows App SDK 
ms.localizationpriority: medium
---

# Tutorial: Build and deploy an unpackaged app using Preview and Experimental channels of the Windows App SDK 

**Using the Windows App SDK Stable version**: You can auto-initialize the Windows App SDK through the `WindowsPackageType` project property when you [Create your first WinUI 3 project](../winui/winui3/create-your-first-winui3-app.md). You can also follow a tutorial ([Tutorial: Use the bootstrapper API in an app packaged with external location or unpackaged that uses the Windows App SDK](tutorial-unpackaged-deployment.md)) in which you configure a packaged with external location or unpackaged app to load the Windows App SDK runtime, and call Windows App SDK APIs.

This article provides a step-by-step tutorial for configuring a packaged with external location or unpackaged app so that it can load the Windows App SDK runtime and call Windows App SDK APIs. This tutorial demonstrates this scenario using a basic Console app project, but the steps apply to any unpackaged desktop app that uses the Windows App SDK.

Before completing this tutorial, we recommend that you review [Runtime architecture](deployment-architecture.md) to learn more about the Framework package dependency your app takes when it uses Reunion, and the additional components required to work in an unpackaged app.

> [!NOTE]
>  The dynamic dependencies and bootstrapper APIs fail when called by an elevated process. As a result, Visual Studio should not be launched elevated. See [issue](https://github.com/microsoft/WindowsAppSDK/issues/567) for more details. 

## Prerequisites

1. [Install tools for the Windows App SDK](set-up-your-development-environment.md).
2. Ensure that all dependencies for the app are installed (see [Windows App SDK deployment guide for framework-dependent apps packaged with external location or unpackaged](deploy-unpackaged-apps.md#prerequisites)). The simplest solution is to run the Windows App SDK runtime installer. 

## Instructions

You can choose to follow this tutorial using a C++ project or a C# project.

### [C++](#tab/cpp)

Follow these instructions to configure a C++ project. Starting in [1.0 Preview 3](release-notes-archive/preview-channel-1.0.md#version-10-preview-3-100-preview3), you can also configure a C++ project that includes WinUI 3 unpackaged support.  

1. In Visual Studio, create a new C++ **Console App** project. Name the project **DynamicDependenciesTest**.
    ![Screenshot of creating a new C++ app in Visual Studio](images/tutorial-deploy-create-project.png)

    ![Screenshot of naming a new C++ app in Visual Studio](images/tutorial-deploy-name-project.png)

    After you create the project, you should have a 'Hello World' C++ console app.

2. Next, install the Windows App SDK NuGet package in your project.

    1. In **Solution Explorer**, right-click the **References** node and choose **Manage Nuget Packages**.
    2. In the **NuGet Package Manager** window, select the **Include prerelease** check box near the top of the window, select the **Browse** tab, and install one of the following packages:
        - To install 1.0 Preview 3 or 1.0 Experimental, search for **Microsoft.WindowsAppSDK**.
        - To install 0.8 Preview, search for **Microsoft.ProjectReunion**.

3. For unpackaged apps, you will need to call the Bootstrapper API to [Use the Windows App SDK runtime for apps packaged with external location or unpackaged](use-windows-app-sdk-run-time.md). This enables you to use Windows App SDK APIs at runtime.

    1. Add the following include files to the top of your **DynamicDependenciesTest.cpp** file. The [mddbootstrap.h](/windows/windows-app-sdk/api/win32/mddbootstrap) header is available via the Windows App SDK NuGet package.

        ```cpp
         #include <windows.h> 
         #include <MddBootstrap.h>   
        ```

    2. Next, add this code at the beginning of your `main` method to call the [MddBootstrapInitialize](/windows/windows-app-sdk/api/win32/mddbootstrap/nf-mddbootstrap-mddbootstrapinitialize) function to initialize the Bootstrapper and handle any errors. This code defines what version of the Windows App SDK the app is dependent upon when initializing the Bootstrapper.

        ```cpp

        // The following code is for 1.0 Preview 3. If using 1.0 Experimental,  
        // replace with versionTag{ L"experimental1" }. If using version 0.8 Preview,
        // replace with majorMinorVersion{ 0x00000008 } and versionTag{ L"preview" }. 
        const UINT32 majorMinorVersion{ 0x00010000 }; 
        PCWSTR versionTag{ L"preview3" }; 
        const PACKAGE_VERSION minVersion{};

        const HRESULT hr{ MddBootstrapInitialize(majorMinorVersion, versionTag, minVersion) }; 

        // Check the return code for errors. If there is an error, display the result.
        if (FAILED(hr)) 
        { 
            wprintf(L"Error 0x%X in MddBootstrapInitialize(0x%08X, %s, %hu.%hu.%hu.%hu)\n", 
                hr, majorMinorVersion, versionTag, minVersion.Major, minVersion.Minor, minVersion.Build, minVersion.Revision); 
            return hr; 
        } 
        ```

    3. Finally, add this code to display the string `Hello World!` and call the [MddBootstrapShutdown](/windows/windows-app-sdk/api/win32/mddbootstrap/nf-mddbootstrap-mddbootstrapshutdown) function to uninitialize the Bootstrapper.

        ```cpp
        std::cout << "Hello World!\n"; 
    
        // Release the DDLM and clean up.
        MddBootstrapShutdown(); 
        ```

    4. Your final code should look like this.

        ```cpp
        #include <iostream> 
        #include <windows.h> 
        #include <MddBootstrap.h>
        
        int main() 
        { 

            // Take a dependency on Windows App SDK 1.0 Preview 3. If using 1.0 Experimental,
            // replace with versionTag{ L"experimental1" }. If using version 0.8 Preview, 
            // replace with majorMinorVersion{ 0x00000008 } and  versionTag{ L"preview" }.
            const UINT32 majorMinorVersion{ 0x00010000 }; 
            PCWSTR versionTag{ L"preview3" }; 
            const PACKAGE_VERSION minVersion{};

            const HRESULT hr{ MddBootstrapInitialize(majorMinorVersion, versionTag, minVersion) }; 
        
            // Check the return code. If there is a failure, display the result.
            if (FAILED(hr)) 
            { 
                wprintf(L"Error 0x%X in MddBootstrapInitialize(0x%08X, %s, %hu.%hu.%hu.%hu)\n", 
                    hr, majorMinorVersion, versionTag, minVersion.Major, minVersion.Minor, minVersion.Build, minVersion.Revision); 
                return hr; 
            } 
        
            std::cout << "Hello World!\n"; 
        
            // Release the DDLM and clean up.
            MddBootstrapShutdown(); 
        } 
        ```

4. Press F5 to build and run your app.

### [C# with 1.0 Preview 3 and later](#tab/csharp-dotnet-preview3)

Follow these instructions to configure a C# project. Starting in [1.0 Preview 3](release-notes-archive/preview-channel-1.0.md#version-10-preview-3-100-preview3), you can also configure a C# project that includes WinUI 3 unpackaged support.  

1. In Visual Studio, create a new C# **Console Application** project. Name the project **DynamicDependenciesTest**.

2. Next, configure your project.

    1. In **Solution Explorer**, right-click your project and choose **Edit Project File**.
    2. Replace the value of the **TargetFramework** element with a [Target Framework Moniker](../desktop/modernize/desktop-to-uwp-enhance.md#net-6-and-later-use-the-target-framework-moniker-option). For example, use the following if your app targets Windows 10, version 2004.

        ```xml
        <TargetFramework>net6.0-windows10.0.19041.0</TargetFramework>
        ```

    3. Save and close the project file.

3. Change the platform for your solution to **x64**. The default value in a .NET project is **AnyCPU**, but WinUI 3 doesn't support that platform.

    1. Select **Build** > **Configuration Manager**.
    2. Select the drop-down under **Active solution platform** and click **New** to open the **New Solution Platform** dialog box.
    3. In the drop-down under **Type or select the new platform**, select **x64**.
    4. Click **OK** to close the **New Solution Platform** dialog box.
    5. In **Configuration Manager**, click **Close**.

4. Install the Windows App SDK NuGet package in your project.

    1. In **Solution Explorer**, right-click the **Dependencies** node and choose **Manage Nuget Packages**.
    2. In the **NuGet Package Manager** window, select the **Include prerelease** check box near the top of the window, select the **Browse** tab, and install the **Microsoft.WindowsAppSDK** package.

5. For unpackaged apps, you will need to call the Bootstrapper API to [Use the Windows App SDK runtime for apps packaged with external location or unpackaged](use-windows-app-sdk-run-time.md). This enables you to use Windows App SDK APIs at runtime.

    Open the **Program.cs** code file and replace the default code with the following code.

    ```csharp
    using System;
    using Microsoft.Windows.ApplicationModel.DynamicDependency;
    
    namespace DynamicDependenciesTest
    {
        class Program
        {
            static void Main(string[] args)
            {
                Bootstrap.Initialize(0x00010000, "preview3");
                Console.WriteLine("Hello World!");
    
                // Release the DDLM and clean up.
                Bootstrap.Shutdown();
            }
        }
    }
    ```

    The bootstrapper API is a native C/C++ API that enables you to use the Windows App SDK APIs in your app. In .NET apps that use the Windows App SDK 1.0 Preview 3 or a later release, you can use the [.NET wrapper](use-windows-app-sdk-run-time.md#net-wrapper-for-the-bootstrapper-api) for the bootstrapper API. This wrapper provides an easier way of calling the bootstrapper API in a .NET app than calling the native C/C++ functions directly. The previous code example calls the static `Initialize` and `Shutdown` methods of the `Bootstrap` class in the .NET wrapper for the bootstrapper API.

6. To demonstrate that the Windows App SDK runtime components were loaded properly, add some code that uses the [ResourceManager](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.resources.resourcemanager) class in the Windows App SDK to load a string resource.

    1. Add a new **Resources File (.resw)** to your project.

    2. With the resources file open in the editor, create a new string resource with the following properties.
        - Name: **Message**
        - Value: **Hello World!**

    3. Save the resources file.

    4. Open the **Program.cs** code file and add the following statement to the top of the file:

        ```csharp
        using Microsoft.Windows.ApplicationModel.Resources;
        ```

    5. Replace the `Console.WriteLine("Hello World!");` line with the following code.

        ```csharp
        // Create a resource manager using the resource index generated during build.
        var manager = new Microsoft.ApplicationModel.Resources.ResourceManager("DynamicDependenciesTest.pri");

        // Lookup a string in the RESW file using its name.
        Console.WriteLine(manager.MainResourceMap.GetValue("Resources/Message").ValueAsString);
        ```

7. Press F5 to build and run your app. You should see the string `Hello World!` successfully displayed.

### [C# with 1.0 Experimental and earlier](#tab/csharp-dotnet-experimental1)

Follow these instructions to configure a C# project that uses the [1.0 Experimental](release-notes-archive/experimental-channel-1.0.md#version-10-experimental-100-experimental1) or earlier release of the Windows App SDK.

1. In Visual Studio, create a new C# **Console Application** project. Name the project **DynamicDependenciesTest**.

2. Next, configure your project.

    1. In **Solution Explorer**, right-click your project and choose **Edit Project File**.
    2. Replace the value of the **TargetFramework** element with a [Target Framework Moniker](../desktop/modernize/desktop-to-uwp-enhance.md#net-6-and-later-use-the-target-framework-moniker-option). For example, use the following if your app targets Windows 10, version 2004.

        ```xml
        <TargetFramework>net6.0-windows10.0.19041.0</TargetFramework>
        ```

    3. Save and close the project file.

3. Change the platform for your solution to **x64**. The default value in a .NET project is **AnyCPU**, but WinUI 3 doesn't support that platform.

    1. Select **Build** > **Configuration Manager**.
    2. Select the drop-down under **Active solution platform** and click **New** to open the **New Solution Platform** dialog box.
    3. In the drop-down under **Type or select the new platform**, select **x64**.
    4. Click **OK** to close the **New Solution Platform** dialog box.
    5. In **Configuration Manager**, click **Close**.

4. Install the Windows App SDK NuGet package in your project.

    1. In **Solution Explorer**, right-click the **Dependencies** node and choose **Manage Nuget Packages**.
    2. In the **NuGet Package Manager** window, select the **Include prerelease** check box near the top of the window, select the **Browse** tab, and install one of the following packages:
        - To install 1.0 Experimental, search for **Microsoft.WindowsAppSDK**.
        - To install 0.8 Preview, search for **Microsoft.ProjectReunion**.

5. You are now ready to use the bootstrapper API (see [Use the Windows App SDK runtime for apps packaged with external location or unpackaged](use-windows-app-sdk-run-time.md)) to dynamically take a dependency on the Windows App SDK framework package. This enables you to use the Windows App SDK APIs in your app.

    1. Add a new code file named **MddBootstrap.cs** to your project and add the following code to it. The [MddBootstrapInitialize](/windows/windows-app-sdk/api/win32/mddbootstrap/nf-mddbootstrap-mddbootstrapinitialize) and [MddBootstrapShutdown](/windows/windows-app-sdk/api/win32/mddbootstrap/nf-mddbootstrap-mddbootstrapshutdown) functions shown in this code example are available via the Windows App SDK NuGet package.

        ```csharp
        using System.Runtime.InteropServices;
        
        namespace Microsoft.Windows.ApplicationModel
        {
            public struct PackageVersion
            {
                ushort Major;
                ushort Minor;
                ushort Build;
                ushort Revision;
        
                public PackageVersion(ushort major) :
                    this(major, 0, 0, 0)
                {
                }
                public PackageVersion(ushort major, ushort minor) :
                    this(major, minor, 0, 0)
                {
                }
                public PackageVersion(ushort major, ushort minor, ushort build) :
                    this(major, minor, build, 0)
                {
                }
                public PackageVersion(ushort major, ushort minor, ushort build, ushort revision)
                {
                    Major = major;
                    Minor = minor;
                    Build = build;
                    Revision = revision;
                }
        
                public PackageVersion(ulong version) :
                    this((ushort)(version >> 48), (ushort)(version >> 32), (ushort)(version >> 16), (ushort)version)
                {
                }
        
                public ulong ToVersion()
                {
                    return (((ulong)Major) << 48) | (((ulong)Minor) << 32) | (((ulong)Build) << 16) | ((ulong)Revision);
                }
            };
        
            public class MddBootstrap
            {
                public static int Initialize(uint majorMinorVersion)
                {
                    return Initialize(majorMinorVersion, null);
                }
        
                public static int Initialize(uint majorMinorVersion, string versionTag)
                {
                    var minVersion = new PackageVersion();
                    return Initialize(majorMinorVersion, versionTag, minVersion);
                }
        
                public static int Initialize(uint majorMinorVersion, string versionTag, PackageVersion minVersion)
                {
                    return MddBootstrapInitialize(majorMinorVersion, versionTag, minVersion);
                }
        
                // Import the bootstrapper library for Windows App SDK 1.0 Experimental.
                // If using version 0.8 Preview, replace with Microsoft.ProjectReunion.Bootstrap.dll.
                [DllImport("Microsoft.WindowsAppSDK.Bootstrap.dll", CharSet = CharSet.Unicode)]
                private static extern int MddBootstrapInitialize(uint majorMinorVersion, string versionTag, PackageVersion packageVersion);
        
                public static void Shutdown()
                {
                    MddBootstrapShutdown();
                }
        
                // Import the bootstrapper library for Windows App SDK 1.0 Experimental. 
                // If using version 0.8 Preview, replace with Microsoft.ProjectReunion.Bootstrap.dll.
                [DllImport("Microsoft.WindowsAppSDK.Bootstrap.dll")]
                private static extern void MddBootstrapShutdown();
            }
        }
        ```

    2. In the **Program.cs** code file, replace the default code with the following code.

        ```csharp
        using System;
        using Microsoft.Windows.ApplicationModel;
        
        namespace DynamicDependenciesTest
        {
            class Program
            {
                static void Main(string[] args)
                {
                
                    // Take a dependency on Windows App SDK 1.0 Experimental.
                    // If using version 0.8 Preview, replace with MddBootstrap.Initialize(8, "preview").
                    MddBootstrap.Initialize(0x00010000, "experimental1");
        
                    Console.WriteLine("Hello World!");
        
                    // Release the DDLM and clean up.
                    MddBootstrap.Shutdown();
                }
            }
        }
        ```

6. To demonstrate that the Windows App SDK runtime components were loaded properly, add some code that uses the [ResourceManager](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.resources.resourcemanager) class in the Windows App SDK to load a string resource.

    1. Add a new **Resources File (.resw)** to your project.

    2. With the resources file open in the editor, create a new string resource with the following properties.
        - Name: **Message**
        - Value: **Hello World!**

    3. Save the resources file.

    4. Open the **Program.cs** code file and add the following statement to the top of the file:

        ```csharp
        using Microsoft.Windows.ApplicationModel.Resources;
        ```

    5. Replace the `Console.WriteLine("Hello World!");` line with the following code.

        ```csharp
        // Create a resource manager using the resource index generated during build.
        var manager = new ResourceManager("DynamicDependenciesTest.pri");

        // Lookup a string in the RESW file using its name.
        Console.WriteLine(manager.MainResourceMap.GetValue("Resources/Message").ValueAsString);
        ```

7. Press F5 to build and run your app. You should see the string `Hello World!` successfully displayed.

---

## Related topics

- [Windows App SDK deployment guide for framework-dependent apps packaged with external location or unpackaged](deploy-unpackaged-apps.md)
- [Runtime architecture](deployment-architecture.md)
