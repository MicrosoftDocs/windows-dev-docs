---
description: This walkthrough shows how to use C#/WinRT to generate a .NET 5 projection for a C++/WinRT component. 
title: Walkthrough to Generate a .NET 5 projection from a C++/WinRT component and distribute the NuGet
ms.date: 11/12/2020
ms.topic: article
keywords: windows 10, c#, winrt, cswinrt, projection
ms.localizationpriority: medium
---

# Walkthrough: Generate a .NET 5 projection from a C++/WinRT component and distribute the NuGet

This walkthrough shows how to use [C#/WinRT](index.md) to generate a .NET 5 projection for a C++/WinRT component, create the associated NuGet package, and reference the NuGet package from a .NET 5 C# console application.

You can download the full sample for this walkthrough from GitHub [here](https://github.com/microsoft/CsWinRT/tree/master/src/Samples/Net5ProjectionSample).

## Prerequisites

This walkthrough and the corresponding sample requires the following tools and components:

- [Visual Studio 16.8](https://visualstudio.microsoft.com/downloads/) (or later) with the Universal Windows Platform development workload installed. In **Installation Details** > **Universal Windows Platform development**, check the **C++ (v14x) Universal Windows Platform tools** option.
- [.NET 5.0 SDK](https://dotnet.microsoft.com/download/dotnet/5.0).
- [C++/WinRT VSIX extension](https://marketplace.visualstudio.com/items?itemName=CppWinRTTeam.cppwinrt101804264) for C++/WinRT project templates.

## Create a simple C++/WinRT Runtime component

To follow this walkthrough, you must first have a C++/WinRT component for which to create a .NET 5 projection. This walkthrough uses the **SimpleMathComponent** project in the related sample from GitHub [here](https://github.com/microsoft/CsWinRT/tree/master/src/Samples/Net5ProjectionSample/SimpleMathComponent). This is a **Windows Runtime Component (C++/WinRT)** project that was created by using the [C++/WinRT VSIX extension](../cpp-and-winrt-apis/intro-to-using-cpp-with-winrt.md#visual-studio-support-for-cwinrt-xaml-the-vsix-extension-and-the-nuget-package). After you copy the project to your development computer, open the solution in Visual Studio 2019 Preview.

The code in this project provides the functionality for basic math operations shown in the header file below. 

```cpp
// SimpleMath.h
...
namespace winrt::SimpleMathComponent::implementation
{
    struct SimpleMath: SimpleMathT<SimpleMath>
    {
        SimpleMath() = default;
        double add(double firstNumber, double secondNumber);
        double subtract(double firstNumber, double secondNumber);
        double multiply(double firstNumber, double secondNumber);
        double divide(double firstNumber, double secondNumber);
    };
}
```

For more detailed steps about creating a C++/WinRT component and generating a .winmd file, see [Windows Runtime components with C++/WinRT](../winrt-components/create-a-windows-runtime-component-in-cppwinrt.md).

> [!NOTE]
> If you are implementing [IInspectable::GetRuntimeClassName](/windows/win32/api/inspectable/nf-inspectable-iinspectable-getruntimeclassname) in your component, it **must** return a valid WinRT class name. Because C#/WinRT uses the class name string for interop, an incorrect runtime class name will raise an **InvalidCastException**.

## Add a projection project to the component solution

If you have cloned the sample from the repo, first delete the **SimpleMathProjection** project to follow the walkthrough step by step.

1. Add a new **Class Library (.NET Core)** project to your solution.

    1. In **Solution Explorer**, right click your solution node and click **Add** -> **New Project**.
    2. In the **Add New Project dialog box**, search for the **Class Library (.NET Core)** project template. Select the template and click **Next**.
    3. Name the new project **SimpleMathProjection** and click **Create**.

2. Delete the empty **Class1.cs** file from the project.

3. Install the [C#/WinRT NuGet package](https://www.nuget.org/packages/Microsoft.Windows.CsWinRT).

    1. In **Solution Explorer**, right click your **SimpleMathProjection** project and select **Manage NuGet Packages**. 
    2. Search for the **Microsoft.Windows.CsWinRT** NuGet package and install the latest version.

4. Add a project reference to the **SimpleMathComponent** project. In **Solution Explorer**, right click the **Dependencies** node under the **SimpleMathProjection** project, select **Add Project Reference**, and select the **SimpleMathComponent** project.

After these steps, your **Solution Explorer** should look similar to this.

![Solution Explorer showing projection project dependencies](images/projection-dependencies.png)

## Edit the project file to execute C#/WinRT

Before you can invoke **cswinrt.exe** and generate the projection assembly, you must edit the project file for the projection project.

1. In **Solution Explorer**, double-click the **SimpleMathProjection** node to open the project file in the editor.

2. Update the `TargetFramework` element to reference the Windows SDK. This adds assembly depedencies that are necessary for the interop and projection support. Our sample targets the latest Windows 10 release as of this walkthrough, **net5.0-windows10.0.19041.0** (also known as SDK version 2004).

    ```xml
    <PropertyGroup>
      <TargetFramework>net5.0-windows10.0.19041.0</TargetFramework>
      <Platforms>x64</Platforms>
    </PropertyGroup>
    ```

3. Add a new `PropertyGroup` element that sets several **cswinrt** properties.

    ```xml
    <PropertyGroup>
      <CsWinRTIncludes>SimpleMathComponent</CsWinRTIncludes>
      <CsWinRTGeneratedFilesDir>$(OutDir)</CsWinRTGeneratedFilesDir>
    </PropertyGroup>
    ```

    Here are some details about the settings in this example:

    - The `CsWinRTIncludes` property specifies which namespaces to project.
    - The `CsWinRTGeneratedFilesDir` property sets the output directory where files from the projection are generated, which we set in the following section on building out of source.

4. The latest C#/WinRT version as of this walkthrough may require specifying Windows Metadata. This can be supplied with either:

    - A NuGet package reference, such as to [Microsoft.Windows.SDK.Contracts]( https://www.nuget.org/packages/Microsoft.Windows.SDK.Contracts/):

      ```xml
      <ItemGroup>
        <PackageReference Include="Microsoft.Windows.SDK.Contracts" Version="10.0.19041.1" />
      </ItemGroup>
      ```

    - Another option is to add the following `CsWinRTWindowsMetadata` property to the `PropertyGroup` from step 3:

      ```xml
      <CsWinRTWindowsMetadata>10.0.19041.0</CsWinRTWindowsMetadata>
      ```

5. Save and close the **SimpleMathProjection.csproj** file.

## Build projects out of source

In the [related sample](https://github.com/microsoft/CsWinRT/tree/master/src/Samples/Net5ProjectionSample), the build is configured with the **Directory.build.props** file. The generated files from building both the **SimpleMathComponent** and **SimpleMathProjection** projects appear in the *_build* folder at the solution level. To configure your projects to build out of source, copy the **Directory.build.props** file below to the directory containing your solution file.

```xml
<Project>
  <PropertyGroup>
    <BuildOutDir>$([MSBuild]::NormalizeDirectory('$(SolutionDir)_build', '$(Platform)', '$(Configuration)'))</BuildOutDir>
    <OutDir>$([MSBuild]::NormalizeDirectory('$(BuildOutDir)', '$(MSBuildProjectName)', 'bin'))</OutDir>
    <IntDir>$([MSBuild]::NormalizeDirectory('$(BuildOutDir)', '$(MSBuildProjectName)', 'obj'))</IntDir>
  </PropertyGroup>
</Project>
```

Although this step is not required to generate a projection, it provides simplicity by generating build files from both projects in the same directory and making build cleanup easier. Note that if you do not build out of source, both **SimpleMathComponent.winmd** and the interop assembly **SimpleMathComponent.dll** will be generated in different directories in their respective project folders. These files are both referenced in **SimpleMathProjection.nuspec** below, so the paths would have to be changed accordingly.

## Create a NuGet package from the projection

To distribute and use the interop assembly, you can automatically create a NuGet package when building the solution by adding some additional project properties. For .NET 5.0 targets, the package should include the interop assembly, implementation assembly, and a dependency on the C#/WinRT NuGet package for the required C#/WinRT runtime assembly, **WinRT.Runtime.dll**.

1. Add a NuGet spec (.nuspec) file to the **SimpleMathProjection** project.

    1. In **Solution Explorer**, right-click the **SimpleMathProjection** node, choose **Add** -> **New Folder**, and name the folder **nuget**. 
    2. Right-click the **nuget** folder, choose **Add** -> **New Item**, choose the XML file, and name it **SimpleMathProjection.nuspec**. 

2. Add the following to **SimpleMathProjection.csproj** to automatically generate the package. These properties specify the `NuspecFile` and the directory to generate the NuGet package.

    ```xml
    <PropertyGroup>
      <GeneratedNugetDir>.\nuget\</GeneratedNugetDir>
      <NuspecFile>$(GeneratedNugetDir)SimpleMathProjection.nuspec</NuspecFile>
      <OutputPath>$(GeneratedNugetDir)</OutputPath>
      <GeneratePackageOnBuild>true</GeneratePackageOnBuild>
    </PropertyGroup>

3. Open the **SimpleMathProjection.nuspec** file to edit the package creation properties. Below is an example NuGet spec for distributing the interop assembly from the C++/WinRT component. Note that for .NET 5.0 targets, under the `dependencies` node there is a dependency on CsWinRT, and under the `files` node **SimpleMathProjection.dll** is specified instead of **SimpleMathComponent.winmd** for the target `lib\net5.0\SimpleMathProjection.dll`. This behavior is new in .NET 5.0 and enabled by C#/WinRT. The implementation assembly, **SimpleMathComponent.dll**, must also be deployed for .NET 5.0 targets. 

    ```xml
    <?xml version="1.0" encoding="utf-8"?>
    <package xmlns="http://schemas.microsoft.com/packaging/2012/06/nuspec.xsd">
      <metadata>
        <id>SimpleMathComponent</id>
        <version>0.1.0-prerelease</version>
        <authors>Contoso Math Inc.</authors>
        <description>A simple component with basic math operations</description>
        <dependencies>
          <group targetFramework=".NETCoreApp3.0" />
          <group targetFramework="UAP10.0" />
          <group targetFramework=".NETFramework4.6" />
          <group targetFramework="net5.0">
            <dependency id="Microsoft.Windows.CsWinRT" version="1.0.1" exclude="Build,Analyzers" />
          </group>
        </dependencies>
      </metadata>
      <files>
        <!--Support netcore3, uap, net46+, net5, c++ -->
        <file src="..\..\_build\x64\Debug\SimpleMathComponent\bin\SimpleMathComponent\SimpleMathComponent.winmd" target="lib\netcoreapp3.0\SimpleMathComponent.winmd" />
        <file src="..\..\_build\x64\Debug\SimpleMathComponent\bin\SimpleMathComponent\SimpleMathComponent.winmd" target="lib\uap10.0\SimpleMathComponent.winmd" />
        <file src="..\..\_build\x64\Debug\SimpleMathComponent\bin\SimpleMathComponent\SimpleMathComponent.winmd" target="lib\net46\SimpleMathComponent.winmd" />
        <file src="..\..\_build\x64\Debug\SimpleMathProjection\bin\SimpleMathProjection.dll" target="lib\net5.0\SimpleMathProjection.dll" />
        <file src="..\..\_build\x64\Debug\SimpleMathComponent\bin\SimpleMathComponent\SimpleMathComponent.dll" target="runtimes\win10-x64\native\SimpleMathComponent.dll" />
      </files>
    </package>
    ```

## Build the solution to generate the projection and NuGet package

At this point you can now build the solution: right click on your solution node and select **Build Solution**. This will first build the component project and then the projection project. The interop **.cs** files and assembly will be generated in the output directory, in addition to the metadata files from the component project. You will also be able to see the the generated NuGet package **SimpleMathComponent0.1.0-prerelease.nupkg** in the **nuget** folder.

![Solution Explorer showing projection generation](images/projection-generated-files.png)

## Reference the NuGet package in a C# .NET 5.0 console application

To consume the projected **SimpleMathComponent**, you can simply add a reference to the newly created NuGet package in your application. The following steps demonstrate how to do this by creating a simple Console app in a separate solution.

1. Create a new solution with a **Console App (.NET Core)** project.

    1. In Visual Studio, select **File** -> **New** -> **Project**.
    2. In the **Add New Project dialog box**, search for the **Console App (.NET Core)** project template. Select the template and click **Next**.
    3. Name the new project **SampleConsoleApp** and click **Create**. Creating this project in a new solution allows you to restore the **SimpleMathComponent** NuGet package separately.

2. In **Solution Explorer**, double-click the **SampleConsoleApp** node to open the **SampleConsoleApp.csproj** project file, and update the target framework moniker and platform configuration as shown in the following example.

    ```xml
    <PropertyGroup>
      <TargetFramework>net5.0-windows10.0.19041.0</TargetFramework>
      <Platforms>x64</Platforms>
    </PropertyGroup>
    ```

3. Add the **SimpleMathComponent** NuGet package to the **SampleConsoleApp** project. You will also need the [Microsoft.VCRTForwarders.140](https://www.nuget.org/packages/Microsoft.VCRTForwarders.140/) NuGet package, which is required in apps that are not packaged in an MSIX package. To restore the **SimpleMathComponent** NuGet when building the project, you can use the `RestoreSources` property with the path to the **nuget** folder in your component solution.

    ```xml
    <PropertyGroup>
      <RestoreSources>
        https://api.nuget.org/v3/index.json;
        ../../CppWinRTProjectionSample/SimpleMathProjection/nuget
      </RestoreSources>
    </PropertyGroup>

    <ItemGroup>
      <PackageReference Include="Microsoft.VCRTForwarders.140" Version="1.0.6" />
      <PackageReference Include="SimpleMathComponent" Version="0.1.0-prerelease" />
    </ItemGroup>
    ```

    Note that for this walkthrough, the NuGet restore path for the **SimpleMathComponent** assumes that both solution files are in the same directory. Alternatively, you can [add a local NuGet package feed](/nuget/consume-packages/install-use-packages-visual-studio#package-sources) to your solution.

4. Edit the **Program.cs** file to use the functionality provided by **SimpleMathComponent**.

    ```csharp
    static void Main(string[] args)
    {
        var x = new SimpleMathComponent.SimpleMath();
        Console.WriteLine("Adding 5.5 + 6.5 ...");
        Console.WriteLine(x.add(5.5, 6.5).ToString());
    }
    ```

5. Build and run the console app. You should see the output below.

    ![Console NET5 output](images/console-output.png)

## Resources

- [Full code sample for this walkthrough](https://github.com/microsoft/CsWinRT/tree/master/src/Samples/Net5ProjectionSample)
