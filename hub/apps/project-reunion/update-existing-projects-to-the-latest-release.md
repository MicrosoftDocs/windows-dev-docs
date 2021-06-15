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

If you created a project with an earlier preview or release version of Project Reunion or WinUI 3, you can update the project to use the latest stable release (version 0.5.7), the latest preview release (version 0.8 preview), or the latest release candidate (version 0.8 release candidate).

> [!NOTE]
> These instructions may have issues due to the uniqueness of each app's individual scenario. Please carefully follow them and if you find issues, [file a bug on our GitHub repo](https://github.com/microsoft/microsoft-ui-xaml/issues/new/choose).

## Update from Project Reunion 0.5.0 or above

If you created a project using Project Reunion version 0.5.0 or above (including the 0.8 preview), you can follow these instructions to update your project to a newer version.

> [!NOTE]
> If you created a project using the Project Reunion 0.5 VSIX and want to update to a newer stable version, you may be able to automatically update your project through the Visual Studio Extension Manager, without going through the manual steps below. In Visual Studio 2019, click on **Extensions** -> **Manage Extensions** and select **Updates** from the left menu bar. Select "Project Reunion" from the list and click **Update**. 

> [!IMPORTANT]
> The following example shows how to update a project to use Project Reunion version 0.5.7. If you'd like to update your app to version 0.8 preview, replace the "0.5.7" version number with "0.8.0-preview". If you'd like to update to version 0.8 release candidate, replace the version number with "0.8.0-rc".

Before starting, make sure you have all the Project Reunion 0.5 prerequisites installed, including the latest Project Reunion VSIX and NuGet package. For more details, see the [installation instructions](set-up-your-development-environment.md).

First, do the following:

- In the .wapproj file, if your **TargetPlatformMinVersion** is older than 10.0.17763.0, change it to 10.0.17763.0.

Next, make these changes to your project:

1. To receive all of the fixes from the latest stable or preview release of Project Reunion in C# .NET 5 projects, you'll need to update your project file to explicitly set the .NET SDK to the latest version. For more information, see [.NET SDK references](release-channels.md#net-sdk-references).

2. In Visual Studio, go to **Tools** -> **Nuget Package Manager** -> **Package Manager Console**.

3. Enter the following commands:

    ```Console
    uninstall-package Microsoft.ProjectReunion -ProjectName {yourProject}
    uninstall-package Microsoft.ProjectReunion.Foundation -ProjectName {yourProject}
    uninstall-package Microsoft.ProjectReunion.WinUI -ProjectName {yourProject}
    install-package Microsoft.ProjectReunion -Version 0.5.7 -ProjectName {yourProjectName}
    ```

4. **If you're updating from 0.8 preview**, your app should now be updated. Otherwise, make the following changes in your Application (package).wapproj:

    1. Locate the following line:

        ```xml
        <AssetTargetFallback>net5.0-windows$(TargetPlatformVersion);$(AssetTargetFallback)</AssetTargetFallback>
        ```

        Move this line and place it on a new line directly beneath the `<TargetPlatformVersion>` tag.

    2. Remove this item group (if you're updating from a newer version than 0.5.0, you will see a later version number referenced in this item group):

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

If you created a project using a preview version of Project Reunion, you can follow these instructions to update your project to a newer release.

> [!IMPORTANT]
> The following example shows how to update a project to use Project Reunion version 0.5.7. If you'd like to update your app to version 0.8 preview, replace the "0.5.7" version number with "0.8.0-preview". If you'd like to update to version 0.8 release candidate, replace the version number with "0.8.0-rc". 

Before starting, make sure you have all the Project Reunion prerequisites installed, including the latest Project Reunion VSIX and NuGet package. For more details, see the [installation instructions](set-up-your-development-environment.md).

First, do the following:

- In the .wapproj file, if your **TargetPlatformMinVersion** is older than 10.0.17763.0, change it to 10.0.17763.0.
- If your app uses the `Application.Suspending` event, be sure to remove or change that line since `Application.Suspending` is no longer called for desktop apps. See the [API reference documentation](/windows/winui/api/microsoft.ui.xaml.application.suspending) for more info.
- The default project templates for both C++ and C# apps included the following lines. Be sure to remove these lines if they are still present in your code:

    ```csharp
    this.Suspending += OnSuspending;
    ```

    ```cpp
    Suspending({ this, &App::OnSuspending });
    ```

Next, make these changes to your project:

1. To receive all of the fixes from the latest stable or preview release of Project Reunion in C# .NET 5 projects, you'll need to update your project file to explicitly set the .NET SDK to the latest version. For more information, see [.NET SDK references](release-channels.md#net-sdk-references).

2. In Visual Studio, go to **Tools** -> **Nuget Package Manager** -> **Package Manager Console**.
3. Enter the following commands:

    ```Console
    uninstall-package Microsoft.ProjectReunion -ProjectName {yourProject}
    uninstall-package Microsoft.ProjectReunion.Foundation -ProjectName {yourProject}
    uninstall-package Microsoft.ProjectReunion.WinUI -ProjectName {yourProject}
    install-package Microsoft.ProjectReunion -Version 0.5.7 -ProjectName {yourProjectName}
    ```

4. If you have a UWP app, your update process should be complete at this stage. If you have a desktop app, make the following changes in your Application (package).wapproj:
  
    1. Add this section:

        ```xml
        <ItemGroup>
            <PackageReference Include="Microsoft.ProjectReunion" Version="[0.5.7]">
                <IncludeAssets>build</IncludeAssets>
            </PackageReference>
        </ItemGroup>
        ```

    2. Locate the `<TargetPlatformVersion>` tag, and add the following on a new line directly beneath that tag:
        ```xml
        <AssetTargetFallback>net5.0-windows$(TargetPlatformVersion);$(AssetTargetFallback)</AssetTargetFallback>
        ```
    3. Remove this line:

        ```xml
        <AppxTargetsLocation Condition="'$(AppxTargetsLocation)'==''">$(MSBuildThisFileDirectory)build\</AppxTargetsLocation>
        ```

        And these lines:

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

If you created a project using WinUI 3 Preview 4, you can follow these instructions to update your project to a newer version of Project.

> [!IMPORTANT]
> The following example shows how to update a project to use Project Reunion version 0.5.7. If you'd like to update your app to version 0.8 preview, replace the "0.5.7" version number with "0.8.0-preview". If you'd like to update to version 0.8 release candidate, replace the version number with "0.8.0-rc".

Before starting, make sure you have all the Project Reunion prerequisites installed, including the latest Project Reunion VSIX and NuGet package. For more details, see the [installation instructions](set-up-your-development-environment.md).

First, do the following:

- In the .wapproj file, if your TargetPlatformMinVersion is older than 10.0.17763.0, change it to 10.0.17763.0.
- If your app uses the `Application.Suspending` event, be sure to remove or change that line since `Application.Suspending` is no longer called for desktop apps. See the [API reference documentation](/windows/winui/api/microsoft.ui.xaml.application.suspending?view=winui-3.0-preview&preserve-view=true) for more info.
- The default project templates for both C++ and C# apps included the following lines. Be sure to remove these lines if they are still present in your code:

    ```csharp
    this.Suspending += OnSuspending;
    ```

    ```cpp
    Suspending({ this, &App::OnSuspending });
    ```

Next, make these changes to your project:

1. To receive all of the fixes from the latest stable or preview release of Project Reunion in C# .NET 5 projects, you'll need to update your project file to explicitly set the .NET SDK to the latest version. For more information, see [.NET SDK references](release-channels.md#net-sdk-references).

2. In Visual Studio, go to **Tools** -> **Nuget Package Manager** -> **Package Manager Console**.
3. Enter the following commands:

    ```Console
    uninstall-package Microsoft.WinUI -ProjectName {yourProject}
    install-package Microsoft.ProjectReunion -Version 0.5.7 -ProjectName {yourProjectName}
    ```

4. If you have a UWP app, your update process should be complete at this stage. If you have a desktop app, make the following changes in your Application (package).wapproj:

    1. Add this section:

        ```xml
        <ItemGroup>
          <PackageReference Include="Microsoft.ProjectReunion" Version="[0.5.7]">
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
