---
description: Author a Windows Runtime Component with C#/WinRT and consume it from a native application 
title: Create a C#/WinRT component and consume it from C++/WinRT
ms.date: 01/28/2021
ms.topic: article
ms.localizationpriority: medium
---

# Walkthrough: Create a C#/WinRT component and consume it from C++/WinRT

> [!NOTE]
> The C#/WinRT authoring support described in this article is currently in preview as of C#/WinRT version 1.1.1. As of this release, it is intended to be used only for early feedback and evaluation.

C#/WinRT enables .NET 5 developers to author their own Windows Runtime components in C# using a class library project. Authored components can be consumed in native desktop applications with a package reference or with a **.winmd** file reference.

This walkthrough demonstrates how you can use C#/WinRT to create your own Windows Runtime types, package them as Windows Runtime component, and consume the component from a C++/WinRT console application.

While authoring your runtime component, follow the guidelines and type restrictions outlined in [this article](../winrt-components/creating-windows-runtime-components-in-csharp-and-visual-basic.md) Internally, the Windows Runtime types in your component can use any .NET functionality that's allowed in a UWP app. For more info, see [.NET for UWP apps](/dotnet/api/index?view=dotnet-uwp-10.0&preserve-view=true). Externally, the members of your type can expose only Windows Runtime types for their parameters and return values.

> [!NOTE]
> There are some Windows Runtime types that are [mapped to .NET types](../winrt-components/net-framework-mappings-of-windows-runtime-types.md#uwp-types-that-map-to-net-types-with-a-different-name-andor-namespace). These .NET types can be used in the public interface of your Windows Runtime component, and will appear to users of the component as the corresponding Windows Runtime types.

## Prerequisites

This walkthrough requires the following tools and components:

- Visual Studio 2019
- .NET 5.0 SDK
- [C++/WinRT VSIX](https://marketplace.visualstudio.com/items?itemName=CppWinRTTeam.cppwinrt101804264) for C++/WinRT project templates

## Create a simple Windows Runtime Component using C#/WinRT

Begin by creating a new project in Visual Studio 2019. Select the **Class Library (.NET Core)** project template and name it **AuthoringDemo**. You will need to make the following additions and modifications to the project:

1. Install the latest version of the [C#/WinRT NuGet package](https://www.nuget.org/packages/Microsoft.Windows.CsWinRT/).

    a. In Solution Explorer, right click on the project node and select **Manage NuGet Packages**.

    b. Search for the **Microsoft.Windows.CsWinRT** NuGet package and install the latest version. This walkthrough uses C#/WinRT version 1.1.1.

2. Update the `TargetFramework` in the **AuthoringDemo.csproj** file and add the following elements to the `PropertyGroup`:

    ```xml
    <PropertyGroup>
        <TargetFramework>net5.0-windows10.0.19041.0</TargetFramework>
        <Platforms>x64</Platforms>
        <AssemblyVersion>1.0.0.0</AssemblyVersion>
    </PropertyGroup>
    ```

    For the `TargetFramework` element, you can use one of the following [target framework monikers](/windows/apps/desktop/modernize/desktop-to-uwp-enhance#net-5-use-the-target-framework-moniker-option).
    - **net5.0-windows10.0.17763.0**
    - **net5.0-windows10.0.18362.0**
    - **net5.0-windows10.0.19041.0**

    You also need to specify an `AssemblyVersion` for your Windows Runtime component.

3. Add a new `PropertyGroup` element that sets several **CsWinRT** properties.

    ```xml
    <PropertyGroup>   
        <CsWinRTComponent>true</CsWinRTComponent>
        <CsWinRTWindowsMetadata>10.0.19041.0</CsWinRTWindowsMetadata>
        <CsWinRTEnableLogging>true</CsWinRTEnableLogging>
        <GeneratedFilesDir Condition="'$(GeneratedFilesDir)'==''">$([MSBuild]::NormalizeDirectory('$(MSBuildProjectDirectory)', '$(IntermediateOutputPath)', 'Generated Files'))</GeneratedFilesDir>
    </PropertyGroup>
      ```

      Here are some details about the properties in this example. For a full list of CsWinRT project properties, refer to the [CsWinRT NuGet documentation.](https://github.com/microsoft/CsWinRT/blob/master/nuget/readme.md)

    - The `CsWinRTComponent` property specifies that your project is a Windows Runtime component, so that a WinMD file is generated for the component.
    - The `CsWinRTWindowsMetadata` property provides a source for Windows Metadata. This is required as of version 1.1.1.
    - The `CsWinRTEnableLogging` property generates a **log.txt** file with detailed output when building the runtime component.
    - The `GeneratedFilesDir` property is required for generating the **.winmd** file in the right output directory. This is required as of version 1.1.1.

4. You can author your runtime classes using library **(.cs)** class files. Right click on the **Class1.cs** file and rename it to **Example.cs**. Add the following code to this file, which adds a public property and method to the runtime class. Remember to mark any classes you want to expose in the runtime component **public**.

    ```csharp
    namespace AuthoringDemo
    {
        public sealed class Example
        {
            public int SampleProperty { get; set; }

            public static string SayHello()
            {
                return "Hello from your C# WinRT component";
            }
        }
    }
    ```

5. You can now build the project to generate the metadata file for your component. Right click on the project in **Solution Explorer** and click **Build**. You will see the generated **AuthoringDemo.winmd** file in the in the **Solution Explorer** under **Generated Files** folder, and also in your build output folder.

## Generate a NuGet package for the component

To distribute the runtime component as a NuGet package, you will need to make the following modifications to the **AuthoringDemo** project. If you choose not to generate a NuGet package for your component, native applications can also consume the component using a direct reference to the generated **.winmd** file, shown in the following section.

1. Add a targets file so that native applications can reference the generated NuGet package and consume your component. In the **Solution Explorer**, right click on the **AuthoringDemo** project and select **Add -> New Item**. Search for the **XML file** template, and name the file **AuthoringDemo.targets**.

    > [!NOTE]
    > The targets file **must** be named using your component name, with the format *YourComponentName.targets*.

    ```xml
    <?xml version="1.0" encoding="utf-8"?>
    <Project xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
        <Import Project="$(MSBuildThisDirectory)AuthoringDemo.CsWinRT.targets" />
    </Project> 
    ```

   The imported **AuthoringDemo.CsWinRT.targets** file will be added to the NuGet package, which configures the package with C#/WinRT hosting assemblies to enable consumption from native applications.  

2. Add the following elements to the **AuthoringDemo.csproj** project file.

    ```xml
    <PropertyGroup>
        <GeneratePackageOnBuild>true</GeneratePackageOnBuild>
    </PropertyGroup>

    <ItemGroup>
        <Content Include="AuthoringDemo.targets" PackagePath="build;buildTransitive"/>
    </ItemGroup>
    ```

    These properties will generate a NuGet package for your component and include the CsWinRT targets in the package for consumption from native applications.

3. Build the **AuthoringDemo** project again. You should now see in the build output that the NuGet package “AuthoringDemo.1.0.0.nupkg” has been successfully created.

## Consume the component in C++/WinRT

C#/WinRT authored Windows Runtime components can be consumed from native applications with a few modifications. The following steps demonstrate how to call the authored component above in a native console application.

1. Add a new **C++/WinRT Console Application** project to your solution. Note that this project can also be part of a different solution if you choose so.

    a. In **Solution Explorer**, right click your solution node and click **Add** -> **New Project**.

    b. In the **Add New Project dialog box**, search for the **C++/WinRT Console Application** project template. Select the template and click **Next**.

    c. Name the new project **CppConsoleApp** and click **Create**.

2. Add a reference to the AuthoringDemo component. You can either add a package reference to the generated NuGet package from the previous section, or a direct reference to **AuthoringDemo.winmd**.

    - **Option 1 (Package reference)**: Right click on the **CppConsoleApp** project, and select **Manage NuGet packages**. You may need to configure your package sources to add a reference to the AuthoringDemo NuGet package. To do this, click on the Settings gear in NuGet Package Manager, and add a package source to the appropriate path.

        ![NuGet settings](images/nuget-sources-settings.png)

        After configuring your package sources, search for the **AuthoringDemo** package and click **Install**.

        ![Install NuGet package](images/install-authoring-nuget.png)

    - **Option 2  (Direct reference)**: Right click on the **CppConsoleApp** project, and click **Add -> Reference**. Select the **Browse** tab, and find and select the **AuthoringDemo.winmd** file from the **AuthoringDemo** project build output.

3. To assist with hosting the component, you will need to add a runtimeconfig.json file and a manifest file. For more details on managed component hosting, refer to [these hosting docs](https://github.com/microsoft/CsWinRT/blob/master/docs/hosting.md).

    a. To add the runtimeconfig.json file, right click on the project and choose **Add -> New Item**. Search for the **Text File** template and name it **WinRT.Host.runtimeconfig.json**. Paste the following contents:

    ```json
    {
        "runtimeOptions": {
            "tfm": "net5.0",
            "rollForward": "LatestMinor",
            "framework": {
                "name": "Microsoft.NETCore.App",
                "version": "5.0.0"
            }
        }
    }
    ```

    Note for the `tfm` entry, a custom self-contained .NET 5 installation can be referenced using the DOTNET_ROOT environment variable.

    b. To add the manifest file, again right click on the project and choose **Add -> New Item**. Search for the **Text File** template and name it **CppConsoleApp.exe.manifest**. Paste the following contents:

    ```xml
    <?xml version="1.0" encoding="utf-8"?>
    <assembly manifestVersion="1.0" xmlns="urn:schemas-microsoft-com:asm.v1">
        <assemblyIdentity version="1.0.0.0" name="CppConsoleApp"/>
        <file name="WinRT.Host.dll">
            <activatableClass
                name="AuthoringDemo.Example"
                threadingModel="both"
                xmlns="urn:schemas-microsoft-com:winrt.v1" />
        </file>
    </assembly>
    ```

    The manifest file is required for non-packaged applications. In this file, specify your runtime classes using activatable class registrations entries as shown above.

4. Edit the native project file to include the runtimeconfig and manifest files in deploying the project. Right click on the project and click **Unload Project**. After it has unloaded, right click on the project again and select **Edit Project File**. Find the entries for **WinRT.Host.runtimeconfig.json** and **CppConsoleApp.exe.manifest**, and add the `DeploymentContent` property as shown below.

    ```xml
    <ItemGroup>
        <None Include="WinRT.Host.runtimeconfig.json">
            <DeploymentContent>true</DeploymentContent>
        </None>

        <Manifest Include="CppConsoleApp.exe.manifest">
            <DeploymentContent>true</DeploymentContent>
        </Manifest>
    </ItemGroup> 
    ```

5. Open **pch.h** under the project's Header Files, and add the following line of code to include your component.

    ```cpp
    #include <winrt/AuthoringDemo.h>
    ```

6. Open **main.cpp** under the project's Source Files, and replace it with the following contents.

    ```cpp
    #include "pch.h"
    #include "iostream"

    using namespace winrt;
    using namespace Windows::Foundation;

    int main()
    {
        init_apartment();

        AuthoringDemo::Example ex;
        ex.SampleProperty(42);
        std::wcout << ex.SampleProperty() << std::endl;
        std::wcout << ex.SayHello().c_str() << std::endl;
    }
    ```

7. Build and run the **CppConsoleApp** project. You should now see the output below.

    ![C++/WinRT Console output](images/consume-component-output.png)

## Related topics

- [Authoring components](https://github.com/microsoft/CsWinRT/blob/master/docs/authoring.md)
- [Managed component hosting](https://github.com/microsoft/CsWinRT/blob/master/docs/hosting.md)
