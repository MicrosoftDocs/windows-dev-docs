---
description: This article provides instructions for updating a project created with an earlier preview or release version of the Windows App SDK or WinUI 3 to the latest version.
title: Update existing projects to the latest release of the Windows App SDK
ms.topic: article
ms.date: 04/07/2021
keywords: windows win32, desktop development, Windows App SDK, Project Reunion
ms.author: mcleans
author: mcleanbyron
ms.localizationpriority: medium
---

# Update existing projects to the latest release of the Windows App SDK

If you created a project with an earlier version of the Windows App SDK (previously called Project Reunion) or WinUI 3, you can update the project to use a more recent release. To learn more about what's currently available in each release channel, see [Windows App SDK release channels](release-channels.md).
. 

> [!NOTE]
> These instructions may have issues due to the uniqueness of each app's individual scenario. Please carefully follow them and if you find issues, [file a bug on our GitHub repo](https://github.com/microsoft/microsoft-ui-xaml/issues/new/choose).

## Update from 0.8 Preview to 0.8 Stable or between stable 0.8 versions

If you created a project using version 0.8 Preview, you can follow these instructions to update your project to a stable version of 0.8. These instructions also apply if you've created a project with an older stable version of 0.8 (for example, 0.8.0) and want to update your project to a newer stable version (for example, 0.8.1).

> [!NOTE]
> You may be able to automatically update your project through the Visual Studio Extension Manager, without going through the manual steps below. In Visual Studio 2019, click on **Extensions** -> **Manage Extensions** and select **Updates** from the left menu bar. Select "Project Reunion" from the list and click **Update**.

Before starting, make sure you have all the Windows App SDK prerequisites installed, including the latest VSIX and NuGet package. For more details, see the [installation instructions](set-up-your-development-environment.md).

First, do the following:

- In the .wapproj file, if your **TargetPlatformMinVersion** is older than 10.0.17763.0, change it to 10.0.17763.0.

Next, make these changes to your project:

1. In Visual Studio, go to **Tools** -> **Nuget Package Manager** -> **Package Manager Console**.

2. Enter the following commands:

    ```Console
    uninstall-package Microsoft.ProjectReunion -ProjectName {yourProject}
    uninstall-package Microsoft.ProjectReunion.Foundation -ProjectName {yourProject}
    uninstall-package Microsoft.ProjectReunion.WinUI -ProjectName {yourProject}
    install-package Microsoft.ProjectReunion -Version 0.8.1 -ProjectName {yourProjectName}
    ```

3. Make the following changes in your Application (package).wapproj:
  
    1. Remove this item group (if you're updating from a different version than 0.8 Preview, you will see a that corresponding version number referenced in this item group):

        ```xml
        <ItemGroup>
            <PackageReference Include="Microsoft.ProjectReunion" Version="[0.8.0-preview]">
            <IncludeAssets>build</IncludeAssets>
            </PackageReference>
            <PackageReference Include="Microsoft.ProjectReunion.WinUI" Version="[0.8.0-preview]">
            <IncludeAssets>build</IncludeAssets>
            </PackageReference>
        </ItemGroup>
        ```

    2. Add this item group to replace it:

        ```xml
        <ItemGroup>
            <PackageReference Include="Microsoft.ProjectReunion" Version="[0.8.1]">
            <IncludeAssets>build</IncludeAssets>
            </PackageReference>
            <PackageReference Include="Microsoft.ProjectReunion.WinUI" Version="[0.8.1]">
            <IncludeAssets>build</IncludeAssets>
            </PackageReference>
        </ItemGroup>
        ```

4. Make the following changes to your project (.csproj or .vcproj) file:

    1. Remove this item group (if you're updating from a different version than 0.8 Preview, you will see a that corresponding version number referenced in this item group):
        ```xml
        <ItemGroup>
            <PackageReference Include="Microsoft.ProjectReunion" Version="0.8.0-preview" />
            <PackageReference Include="Microsoft.ProjectReunion.Foundation" Version="0.8.0-preview" />
            <PackageReference Include="Microsoft.ProjectReunion.WinUI" Version="0.8.0-preview" />
            <Manifest Include="$(ApplicationManifest)" />
        </ItemGroup>
        ```
    2. Add this item group to replace it:
        ```xml
        <ItemGroup>
            <PackageReference Include="Microsoft.ProjectReunion" Version="0.8.1" />
            <PackageReference Include="Microsoft.ProjectReunion.Foundation" Version="0.8.1" />
            <PackageReference Include="Microsoft.ProjectReunion.WinUI" Version="0.8.1" />
            <Manifest Include="$(ApplicationManifest)" />
        </ItemGroup>
        ```
5. If your solution fails to build, clean the build output, restart Visual Studio, and try re-running the app. 

## Update from 0.5 Stable to 0.8 Stable

If you created a project using version 0.5 stable, you can follow these instructions to update your project to version 0.8 stable.

> [!NOTE]
> You may be able to automatically update your project through the Visual Studio Extension Manager, without going through the manual steps below. In Visual Studio 2019, click on **Extensions** -> **Manage Extensions** and select **Updates** from the left menu bar. Select "Project Reunion" from the list and click **Update**.

Before starting, make sure you have all the Windows App SDK prerequisites installed, including the latest VSIX and NuGet package. For more details, see the [installation instructions](set-up-your-development-environment.md).

First, do the following:

- In the .wapproj file, if your **TargetPlatformMinVersion** is older than 10.0.17763.0, change it to 10.0.17763.0.

Next, make these changes to your project:

1. In Visual Studio, go to **Tools** -> **Nuget Package Manager** -> **Package Manager Console**.

2. Enter the following commands:

    ```Console
    uninstall-package Microsoft.ProjectReunion -ProjectName {yourProject}
    uninstall-package Microsoft.ProjectReunion.Foundation -ProjectName {yourProject}
    uninstall-package Microsoft.ProjectReunion.WinUI -ProjectName {yourProject}
    install-package Microsoft.ProjectReunion -Version 0.8.0 -ProjectName {yourProjectName}
    ```

3. Add the following line to your project (.csproj or .vcproj) file, inside the first `<PropertyGroup>`:

    ```xml
    <UseWinUI>true</UseWinUI>
    ```

4. Make the following changes in your Application (package).wapproj:
  
    1. Add this section:

        ```xml
        <ItemGroup>
            <PackageReference Include="Microsoft.ProjectReunion" Version="[0.8.0]">
            <IncludeAssets>build</IncludeAssets>
            </PackageReference>
            <PackageReference Include="Microsoft.ProjectReunion.WinUI" Version="[0.8.0]">
            <IncludeAssets>build</IncludeAssets>
            </PackageReference>
        </ItemGroup>
        ```

    2. Locate the following line:

        ```xml
        <AssetTargetFallback>net5.0-windows$(TargetPlatformVersion);$(AssetTargetFallback)</AssetTargetFallback>
        ```

        Move this line and place it on a new line directly beneath the `<TargetPlatformVersion>` tag.

    3. Remove this item group (if you're updating from an earlier version than 0.5.7, you will see an earlier version number referenced in this item group):

        ```xml
        <ItemGroup>
            <PackageReference Include="Microsoft.ProjectReunion" Version="[0.5.7]">
            <IncludeAssets>build</IncludeAssets>
            </PackageReference>
            <PackageReference Include="Microsoft.ProjectReunion.WinUI" Version="[0.5.7]">
            <IncludeAssets>build</IncludeAssets>
            </PackageReference>
        </ItemGroup>
        ```
5. If your solution fails to build, clean the build output, restart Visual Studio, and try re-running the app. 

## Update from 0.5 Preview to 0.8 Preview

If you created a project using version 0.5 preview, you can follow these instructions to update your project to use version 0.8 preview.

> [!NOTE]
> You may be able to automatically update your project through the Visual Studio Extension Manager, without going through the manual steps below. In Visual Studio 2019, click on **Extensions** -> **Manage Extensions** and select **Updates** from the left menu bar. Select "Project Reunion" from the list and click **Update**.

Before starting, make sure you have all the Windows App SDK prerequisites installed, including the latest VSIX and NuGet package. For more details, see the [installation instructions](set-up-your-development-environment.md).

First, do the following:

- In the .wapproj file, if your **TargetPlatformMinVersion** is older than 10.0.17763.0, change it to 10.0.17763.0.
- The default project templates for both C++ and C# apps included the following lines. The `Application.Suspending` event is no longer called for desktop apps, so be sure to remove these lines (and any other uses of this event) if they are still present in your code:

    ```csharp
    this.Suspending += OnSuspending;
    ```

    ```cpp
    Suspending({ this, &App::OnSuspending });
    ```

Next, make these changes to your project:

1. In Visual Studio, go to **Tools** -> **Nuget Package Manager** -> **Package Manager Console**.

2. Enter the following commands:

    ```Console
    uninstall-package Microsoft.ProjectReunion -ProjectName {yourProject}
    uninstall-package Microsoft.ProjectReunion.Foundation -ProjectName {yourProject}
    uninstall-package Microsoft.ProjectReunion.WinUI -ProjectName {yourProject}
    install-package Microsoft.ProjectReunion -Version 0.8.0-preview -ProjectName {yourProjectName}
    ```

3. Make the following changes in your Application (package).wapproj:
  
    1. Add this section:

        ```xml
        <ItemGroup>
            <PackageReference Include="Microsoft.ProjectReunion" Version="[0.8.0-preview]">
            <IncludeAssets>build</IncludeAssets>
            </PackageReference>
            <PackageReference Include="Microsoft.ProjectReunion.WinUI" Version="[0.8.0-preview]">
            <IncludeAssets>build</IncludeAssets>
            </PackageReference>
        </ItemGroup>
        ```

    2. Add the following line to a new line directly beneath the `<TargetPlatformVersion>` tag.

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
            <PackageReference Include="Microsoft.ProjectReunion" Version="[0.5.0]" GeneratePathProperty="true">
              <ExcludeAssets>all</ExcludeAssets>
            </PackageReference>
            <PackageReference Include="Microsoft.ProjectReunion.WinUI" Version="[0.5.0]" GeneratePathProperty="true">
              <ExcludeAssets>all</ExcludeAssets>
            </PackageReference>
        </ItemGroup>
        ```
4. Make the following changes to your project (.csproj or .vcproj) file:

    1. Remove this item group:
        ```xml
        <ItemGroup>
            <PackageReference Include="Microsoft.ProjectReunion" Version="0.5.0-prerelease" />
            <PackageReference Include="Microsoft.ProjectReunion.Foundation" Version="0.5.0-prerelease" />
            <PackageReference Include="Microsoft.ProjectReunion.WinUI" Version="0.5.0-prerelease" />
            <Manifest Include="$(ApplicationManifest)" />
        </ItemGroup>
        ```

    2. Add this item group:
        ```xml
        <ItemGroup>
            <PackageReference Include="Microsoft.ProjectReunion" Version="0.8.0-preview" />
            <PackageReference Include="Microsoft.ProjectReunion.Foundation" Version="0.8.0-preview" />
            <PackageReference Include="Microsoft.ProjectReunion.WinUI" Version="0.8.0-preview" />
            <Manifest Include="$(ApplicationManifest)" />
        </ItemGroup>
        ```

5. If your solution fails to build, clean the build output, restart Visual Studio, and try re-running the app. 

## Update from 0.5 Preview to  0.5 Stable

If you created a project using version 0.5 preview, you can follow these instructions to update your project to stable version 0.5.7.

Before starting, make sure you have all the Windows App SDK prerequisites installed, including the latest VSIX and NuGet package. For more details, see the [installation instructions](set-up-your-development-environment.md).

First, do the following:

- **[Desktop apps only]** In the .wapproj file, if your **TargetPlatformMinVersion** is older than 10.0.17763.0, change it to 10.0.17763.0.
- The default project templates for both C++ and C# apps included the following lines. The `Application.Suspending` event is no longer called for desktop apps, so be sure to remove these lines (and any other uses of this event) if they are still present in your code:

    ```csharp
    this.Suspending += OnSuspending;
    ```

    ```cpp
    Suspending({ this, &App::OnSuspending });
    ```

Next, make these changes to your project:

1. In Visual Studio, go to **Tools** -> **Nuget Package Manager** -> **Package Manager Console**.
2. Enter the following commands:

    ```Console
    uninstall-package Microsoft.ProjectReunion -ProjectName {yourProject}
    uninstall-package Microsoft.ProjectReunion.Foundation -ProjectName {yourProject}
    uninstall-package Microsoft.ProjectReunion.WinUI -ProjectName {yourProject}
    install-package Microsoft.ProjectReunion -Version 0.5.7 -ProjectName {yourProjectName}
    ```

3. If you have a UWP app, your update process should be complete at this stage. If you have a desktop app, make the following changes in your Application (package).wapproj:
  
    1. Add this section:

        ```xml
        <ItemGroup>
            <PackageReference Include="Microsoft.ProjectReunion" Version="[0.5.7]">
                <IncludeAssets>build</IncludeAssets>
            </PackageReference>
        </ItemGroup>
        ```

    2. Locate the `<TargetPlatformVersion>` tag, and add the following on a new line directly beneath that tag

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

## Update from WinUI 3 Preview 4 to 0.5 Stable

If you created a desktop app using WinUI 3 Preview 4, you can follow these instructions to update your project to Project Reunion 0.5 Stable. 

Before starting, make sure you have all the Windows App SDK prerequisites installed, including the latest VSIX and NuGet package. For more details, see the [installation instructions](set-up-your-development-environment.md).

First, do the following:

- In the .wapproj file, if your TargetPlatformMinVersion is older than 10.0.17763.0, change it to 10.0.17763.0.
- The default project templates for both C++ and C# apps included the following lines. The `Application.Suspending` event is no longer called for desktop apps, so be sure to remove these lines (and any other uses of this event) if they are still present in your code:

    ```csharp
    this.Suspending += OnSuspending;
    ```

    ```cpp
    Suspending({ this, &App::OnSuspending });
    ```

Next, make these changes to your project:

1. In Visual Studio, go to **Tools** -> **Nuget Package Manager** -> **Package Manager Console**.
2. Enter the following commands:

    ```Console
    uninstall-package Microsoft.WinUI -ProjectName {yourProject}
    install-package Microsoft.ProjectReunion -Version 0.5.7 -ProjectName {yourProjectName}
    ```

3. Make the following changes in your Application (package).wapproj:

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

4. Delete the existing `Microsoft.WinUI.AppX.targets` file under the {YourProject}(package)/build/ folder of your project.
