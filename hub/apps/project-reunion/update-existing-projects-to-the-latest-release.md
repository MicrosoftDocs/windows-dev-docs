---
description: This article provides instructions for updating a project created with an earlier preview or release version of Project Reunion or WinUI 3 to the latest version.
title: Update existing projects to the latest release of Project Reunion
ms.topic: article
ms.date: 04/07/2021
keywords: windows win32, desktop development, project reunion
ms.author: mcleans
author: mcleanbyron
ms.localizationpriority: medium
---

# Update existing projects to the latest release of Project Reunion

If you created a project with an earlier preview or release version of Project Reunion or WinUI 3, you can update the project to use the latest stable release (version 0.5.5).

> [!NOTE]
> These instructions may have issues due to the uniqueness of each app's individual scenario. Please carefully follow them and if you find issues, [file a bug on our GitHub repo](https://github.com/microsoft/microsoft-ui-xaml/issues/new/choose).

## Update from Project Reunion 0.5.0

If you created a project using Project Reunion version 0.5.0, you can follow these instructions to update your project to Project Reunion version 0.5.5 (the latest stable release). This version includes several important bug fixes.

> [!NOTE]
> If you created a project using the Project Reunion 0.5 VSIX, you may be able to automatically update your project through the Visual Studio Extension Manager, without going through the manual steps below. In Visual Studio 2019, click on **Extensions** -> **Manage Extensions** and select **Updates** from the left menu bar. Select "Project Reunion" from the list and click **Update**. 

Before starting, make sure you have all the Project Reunion 0.5 prerequisites installed, including the latest Project Reunion VSIX and NuGet package. For more details, see the [installation instructions](get-started-with-project-reunion.md#set-up-your-development-environment).

First, do the following:
- In the .wapproj file, if your **TargetPlatformMinVersion** is older than 10.0.17763.0, change it to 10.0.17763.0.

Next, make these changes to your project:
1. To get all of the changes from the latest stable release, you'll need to explicitly set your .NET SDK to the latest version. To do this, add the following item group to your .csproj file, then save your project:

    ```xml
    <ItemGroup>            
        <FrameworkReference Update="Microsoft.Windows.SDK.NET.Ref" RuntimeFrameworkVersion="10.0.18362.16" />
        <FrameworkReference Update="Microsoft.Windows.SDK.NET.Ref" TargetingPackVersion="10.0.18362.16" />
    </ItemGroup>
    ```

    Note that once .NET 5.0.6 is available in May, these lines can be removed. 

3. In Visual Studio, go to **Tools** -> **Nuget Package Manager** -> **Package Manager Console**.

4. Enter the following commands:

    ```Console
    uninstall-package Microsoft.ProjectReunion -ProjectName {yourProject}
    uninstall-package Microsoft.ProjectReunion.Foundation -ProjectName {yourProject}
    uninstall-package Microsoft.ProjectReunion.WinUI -ProjectName {yourProject}
    install-package Microsoft.ProjectReunion -Version 0.5.5 -ProjectName {yourProjectName}
    ```

5. Make the following changes in your Application (package).wapproj:
  
    1. Add this section:

        ```xml
        <ItemGroup>
            <PackageReference Include="Microsoft.ProjectReunion" Version="[0.5.5]">
                <IncludeAssets>build</IncludeAssets>
            </PackageReference>
        </ItemGroup>
        ```

    2. Remove the following lines:

        ```xml
        <AppxTargetsLocation Condition="'$(AppxTargetsLocation)'==''">$(MSBuildThisFileDirectory)build\</AppxTargetsLocation>
        ```

        And:

        ```xml
        <Import Project="$(Microsoft_ProjectReunion_AppXReference_props)" />
        <Import Project="$(Microsoft_WinUI_AppX_targets)" />
        ```

        And this item group:

        ```xml
        <ItemGroup>
            <PackageReference Include="Microsoft.ProjectReunion" Version="[0.5.0]" GeneratePathProperty="true">
              <ExcludeAssets>all</ExcludeAssets>
            </PackageReference>
            <PackageReference Include="Microsoft.ProjectReunion.WinUI" Version="[0.5.0]" GeneratePathProperty="true">
              <ExcludeAssets>all</ExcludeAssets>
            </PackageReference>
        </ItemGroup>
        ```

## Update from Project Reunion 0.5 Preview

If you created a project using Project Reunion 0.5 Preview, you can follow these instructions to update your project to Project Reunion version 0.5.5 (the latest stable release).

Before starting, make sure you have all the Project Reunion 0.5 prerequisites installed, including the latest Project Reunion VSIX and NuGet package. For more details, see the [installation instructions](get-started-with-project-reunion.md#set-up-your-development-environment).

First, do the following:

- In the .wapproj file, if your **TargetPlatformMinVersion** is older than 10.0.17763.0, change it to 10.0.17763.0.
- If your app uses the `Application.Suspending` event, be sure to remove or change that line since `Application.Suspending` is no longer called for desktop apps. See the [API reference documentation](https://docs.microsoft.com/windows/winui/api/microsoft.ui.xaml.application.suspending) for more info.
- The default project templates for both C++ and C# apps included the following lines. Be sure to remove these lines if they are still present in your code:

    ```csharp
    this.Suspending += OnSuspending;
    ```

    ```cpp
    Suspending({ this, &App::OnSuspending });
    ```

Next, make these changes to your project:
1. To get all of the changes from the latest stable release, you'll need to explicitly set your .NET SDK to the latest version. To do this, add the following item group to your .csproj file, then save your project:

    ```xml
    <ItemGroup>            
        <FrameworkReference Update="Microsoft.Windows.SDK.NET.Ref" RuntimeFrameworkVersion="10.0.18362.16" />
        <FrameworkReference Update="Microsoft.Windows.SDK.NET.Ref" TargetingPackVersion="10.0.18362.16" />
    </ItemGroup>
    ```

    Note that once .NET 5.0.6 is available in May, these lines can be removed.

2. In Visual Studio, go to **Tools** -> **Nuget Package Manager** -> **Package Manager Console**.
3. Enter the following commands:

    ```Console
    uninstall-package Microsoft.ProjectReunion -ProjectName {yourProject}
    uninstall-package Microsoft.ProjectReunion.Foundation -ProjectName {yourProject}
    uninstall-package Microsoft.ProjectReunion.WinUI -ProjectName {yourProject}
    install-package Microsoft.ProjectReunion -Version 0.5.5 -ProjectName {yourProjectName}
    ```

4. Make the following changes in your Application (package).wapproj:
  
    1. Add this section:

        ```xml
        <ItemGroup>
            <PackageReference Include="Microsoft.ProjectReunion" Version="[0.5.5]">
                <IncludeAssets>build</IncludeAssets>
            </PackageReference>
        </ItemGroup>
        ```

    2. Remove the following lines:

        ```xml
        <AppxTargetsLocation Condition="'$(AppxTargetsLocation)'==''">$(MSBuildThisFileDirectory)build\</AppxTargetsLocation>
        ```

        And

        ```xml
        <Import Project="$(Microsoft_ProjectReunion_AppXReference_props)" />
        <Import Project="$(Microsoft_WinUI_AppX_targets)" />
        ```

        And this item group:

        ```xml
        <ItemGroup>
            <PackageReference Include="Microsoft.ProjectReunion" Version="[0.5.0-prerelease]" GeneratePathProperty="true">
              <ExcludeAssets>all</ExcludeAssets>
            </PackageReference>
            <PackageReference Include="Microsoft.ProjectReunion.WinUI" Version="[0.5.0-prerelease]" GeneratePathProperty="true">
              <ExcludeAssets>all</ExcludeAssets>
            </PackageReference>
        </ItemGroup>
        ```

## Update from WinUI 3 Preview 4

If you created a project using WinUI 3 Preview 4, you can follow these instructions to update your project to Project Reunion version 0.5.5 (the latest stable release).

Before starting, make sure you have all the Project Reunion 0.5 prerequisites installed, including the latest Project Reunion VSIX and NuGet package. For more details, see the [installation instructions](get-started-with-project-reunion.md#set-up-your-development-environment).

First, do the following:

- In the .wapproj file, if your TargetPlatformMinVersion is older than 10.0.17763.0, change it to 10.0.17763.0.
- If your app uses the `Application.Suspending` event, be sure to remove or change that line since `Application.Suspending` is no longer called for desktop apps. See the [API reference documentation](https://docs.microsoft.com/windows/winui/api/microsoft.ui.xaml.application.suspending?view=winui-3.0-preview&preserve-view=true) for more info.
- The default project templates for both C++ and C# apps included the following lines. Be sure to remove these lines if they are still present in your code:

    ```csharp
    this.Suspending += OnSuspending;
    ```

    ```cpp
    Suspending({ this, &App::OnSuspending });
    ```

Next, make these changes to your project:
1. To get all of the changes from the latest stable release, you'll need to explicitly set your .NET SDK to the latest version. To do this, add the following item group to your .csproj file, then save your project:

    ```xml
    <ItemGroup>            
        <FrameworkReference Update="Microsoft.Windows.SDK.NET.Ref" RuntimeFrameworkVersion="10.0.18362.16" />
        <FrameworkReference Update="Microsoft.Windows.SDK.NET.Ref" TargetingPackVersion="10.0.18362.16" />
    </ItemGroup>
    ```

    Note that once .NET 5.0.6 is available in May, these lines can be removed.
2. In Visual Studio, go to **Tools** -> **Nuget Package Manager** -> **Package Manager Console**.
3. Enter the following commands:

    ```Console
    uninstall-package Microsoft.WinUI -ProjectName {yourProject}
    install-package Microsoft.ProjectReunion -Version 0.5.2 -ProjectName {yourProjectName}
    ```

4. Make the following changes in your Application (package).wapproj:

    1. Add this section:

        ```xml
        <ItemGroup>
          <PackageReference Include="Microsoft.ProjectReunion" Version="[0.5.2]">
            <IncludeAssets>build</IncludeAssets>
          </PackageReference>
        </ItemGroup>
        ```

    2. Remove the following lines:

        ```xml
        <AppxTargetsLocation Condition="'$(AppxTargetsLocation)'==''">$(MSBuildThisFileDirectory)build\</AppxTargetsLocation>
        ```

        ```xml
        <Import Project="$(AppxTargetsLocation)Microsoft.WinUI.AppX.targets" />
        ```

5. Delete the existing `Microsoft.WinUI.AppX.targets` file under the {YourProject}(package)/build/ folder of your project.
