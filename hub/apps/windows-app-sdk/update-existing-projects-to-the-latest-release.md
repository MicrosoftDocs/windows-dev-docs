--- 
title: Update existing projects to a different release of the Windows App SDK
description: This article provides instructions for updating a project created with an earlier preview or release version of the Windows App SDK or WinUI 3 to the latest version.
ms.topic: article
ms.date: 03/03/2023
keywords: windows win32, desktop development, Windows App SDK, Project Reunion
ms.localizationpriority: medium
---

# Update existing projects to a different release of the Windows App SDK

If you created a project with an earlier version of the Windows App SDK (previously called Project Reunion) or WinUI 3, then you can update the project to use a more recent release. To learn more about what's currently available in each release channel, see [Windows App SDK release channels](release-channels.md).

> [!NOTE]
> These instructions might have issues due to the uniqueness of each app's individual scenario. Please carefully follow them, and if you find an issue then please file a bug against the [microsoft-ui-xaml](https://github.com/microsoft/microsoft-ui-xaml/issues/new/choose) GitHub repo.

## Update between versions released after 1.0

If your project isn't referencing the version of the Windows App SDK NuGet package that you need, then you can use the **NuGet Package Manager** in Visual Studio to update your project's NuGet package references. For example, if you create a new project by using a stable release of the Windows App SDK VSIX, then your project will reference a stable release of the Windows App SDK. But you can easily reconfigure that project to reference, say, an experimental release of the Windows App SDK. Or reconfigure it to reference the latest stable release.

For steps, see the instructions in [Use the Windows App SDK in an existing project](/windows/apps/windows-app-sdk/use-windows-app-sdk-in-existing-project).

## Update from 0.8 to 1.0

If you created a project using version 0.8 (for example, version 0.8.4), then you can follow these instructions to update your project to the 1.0 release.

**Prerequisite:** Download and install the latest release of the Windows App SDK. For more information, see [Install tools for the Windows App SDK](set-up-your-development-environment.md).

### Instructions

1. In the `.wapproj` file, if your **TargetPlatformMinVersion** is older than `10.0.17763.0`, then change it to `10.0.17763.0`.

2. In Visual Studio, go to **Tools** > **Nuget Package Manager** > **Package Manager Console**. This process consists of uninstalling existing Project Reunion package references from  `.csproj`/`.vcxproj` and `.wapproj` files, and then installing the `WindowsAppSDK` package references to those files.

3. Enter the following commands to uninstall existing `ProjectReunion` packages from your `.csproj`/`.vcxproj`

    ```Console
    uninstall-package Microsoft.ProjectReunion -ProjectName {yourProject} 
    uninstall-package Microsoft.ProjectReunion.Foundation -ProjectName {yourProject}
    uninstall-package Microsoft.ProjectReunion.WinUI -ProjectName {yourProject}
    ```
    
4. Then, run the following to uninstall existing `ProjectReunion` packages from your `.wapproj`:

    ```Console
    uninstall-package Microsoft.ProjectReunion 
    uninstall-package Microsoft.ProjectReunion.WinUI
    ```
5. Now run the commands to install the stable `WindowsAppSDK` package.

6. To add the `WindowsAppSDK` package reference to your `.csproj`/`.vcxproj`:
    
    ```Console
    install-package Microsoft.WindowsAppSDK -ProjectName {yourProject} -Version 1.0.0
    ```

7. To add the `WindowsAppSDK` package reference to your `.wapproj`:
    
    ```Console
    install-package Microsoft.WindowsAppSDK -Version 1.0.0 
    ```   
    
## Update from 0.8 or 0.8 Preview to 1.0 Experimental or Preview 3

> [!IMPORTANT]
> Version 1.0 Preview 1 and Preview 2 contain a critical bug. If you've already installed one of these previews, see [Important issue impacting 1.0 Preview 1 and Preview 2](release-notes-archive/preview-channel-1.0.md#important-issue-impacting-10-preview-1-and-preview-2). We recommend using version [Version 1.0 Preview 3 (1.0.0-preview3)](release-notes-archive/preview-channel-1.0.md#version-10-preview-3-100-preview3) instead.

If you created a project using version 0.8 Preview or any version of 0.8 (for example, version 0.8.1), you can follow these instructions to update your project to the 1.0 Preview 3 or Experimental release.

Before starting, make sure you have all the Windows App SDK prerequisites installed, including the latest VSIX and NuGet package. For more details, see [Install tools for the Windows App SDK](set-up-your-development-environment.md).

First, do the following:

- In the .wapproj file, if your **TargetPlatformMinVersion** is older than 10.0.17763.0, change it to 10.0.17763.0.

Next, make these changes to your project:

1. In Visual Studio, go to **Tools** > **Nuget Package Manager** > **Package Manager Console**.

2. Enter the following commands for 1.0 Preview 3:

    ```Console
    uninstall-package Microsoft.ProjectReunion -ProjectName {yourProject}
    uninstall-package Microsoft.ProjectReunion.Foundation -ProjectName {yourProject}
    uninstall-package Microsoft.ProjectReunion.WinUI -ProjectName {yourProject}
    install-package Microsoft.WindowsAppSDK -Version 1.0.0-preview3 -ProjectName {yourProjectName}
    ```

    Or the following commands for 1.0 Experimental:

    ```Console
    uninstall-package Microsoft.ProjectReunion -ProjectName {yourProject}
    uninstall-package Microsoft.ProjectReunion.Foundation -ProjectName {yourProject}
    uninstall-package Microsoft.ProjectReunion.WinUI -ProjectName {yourProject}
    install-package Microsoft.WindowsAppSDK -Version 1.0.0-experimental1 -ProjectName {yourProjectName}
    ```

3. Make the following changes in your Application (package).wapproj:
  
    1. Remove this item group (if you're updating from a different version than 0.8.0, you will see that corresponding version number referenced in this item group):

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

    2. Add this item group to replace it with 1.0 Preview 3:

        ```xml
        <ItemGroup>
            <PackageReference Include="Microsoft.WindowsAppSDK" Version="[1.0.0-preview3]">
            <IncludeAssets>build</IncludeAssets>
            </PackageReference>
        </ItemGroup>
        ```

       Or this item group to replace it with 1.0 Experimental:

        ```xml
        <ItemGroup>
            <PackageReference Include="Microsoft.WindowsAppSDK" Version="[1.0.0-experimental1]">
            <IncludeAssets>build</IncludeAssets>
            </PackageReference>
            <PackageReference Include="Microsoft.WindowsAppSDK.WinUI" Version="[1.0.0-experimental1]">
            <IncludeAssets>build</IncludeAssets>
            </PackageReference>
        </ItemGroup>
        ```

4. Make the following changes to your project (.csproj or .vcproj) file:

    1. Remove this item group (if you're updating from a different version than 0.8.0, you will see that corresponding version number referenced in this item group):
        ```xml
        <ItemGroup>
            <PackageReference Include="Microsoft.ProjectReunion" Version="0.8.0" />
            <PackageReference Include="Microsoft.ProjectReunion.Foundation" Version="0.8.0" />
            <PackageReference Include="Microsoft.ProjectReunion.WinUI" Version="0.8.0" />
            <Manifest Include="$(ApplicationManifest)" />
        </ItemGroup>
        ```

    2. Add this item group to replace it with 1.0 Preview 3:
        ```xml
        <ItemGroup>
            <PackageReference Include="Microsoft.WindowsAppSDK" Version="1.0.0-preview3" />
            <Manifest Include="$(ApplicationManifest)" />
        </ItemGroup>
        ```

        Or this item group to replace it with 1.0 Experimental:
        ```xml
        <ItemGroup>
            <PackageReference Include="Microsoft.WindowsAppSDK" Version="1.0.0-experimental1" />
            <PackageReference Include="Microsoft.WindowsAppSDK.Foundation" Version="1.0.0-experimental1" />
            <PackageReference Include="Microsoft.WindowsAppSDK.WinUI" Version="1.0.0-experimental1" />
            <Manifest Include="$(ApplicationManifest)" />
        </ItemGroup>
        ```
5. If your solution fails to build, clean the build output, restart Visual Studio, and try re-running the app. 

## Update from 0.8 Preview to 0.8 or between stable 0.8 versions

If you created a project using version 0.8 Preview, you can follow these instructions to update your project to a stable version of 0.8. These instructions also apply if you've created a project with an older stable version of 0.8 (for example, 0.8.0) and want to update your project to a newer stable version (for example, 0.8.2).

> [!NOTE]
> You may be able to automatically update your project through the Visual Studio Extension Manager, without going through the manual steps below. In Visual Studio 2019, click on **Extensions** > **Manage Extensions** and select **Updates** from the left menu bar. Select "Project Reunion" from the list and click **Update**.

Before starting, make sure you have all the Windows App SDK prerequisites installed, including the latest VSIX and NuGet package. For more details, see [Install tools for the Windows App SDK](set-up-your-development-environment.md).

First, do the following:

- In the .wapproj file, if your **TargetPlatformMinVersion** is older than 10.0.17763.0, change it to 10.0.17763.0.

Next, make these changes to your project:

1. In Visual Studio, go to **Tools** > **Nuget Package Manager** > **Package Manager Console**.

2. Enter the following commands:

    ```Console
    uninstall-package Microsoft.ProjectReunion -ProjectName {yourProject}
    uninstall-package Microsoft.ProjectReunion.Foundation -ProjectName {yourProject}
    uninstall-package Microsoft.ProjectReunion.WinUI -ProjectName {yourProject}
    install-package Microsoft.ProjectReunion -Version 0.8.2 -ProjectName {yourProjectName}
    ```

3. Make the following changes in your Application (package).wapproj:
  
    1. Remove this item group (if you're updating from a different version than 0.8 Preview, you will see that corresponding version number referenced in this item group):

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
            <PackageReference Include="Microsoft.ProjectReunion" Version="[0.8.2]">
            <IncludeAssets>build</IncludeAssets>
            </PackageReference>
            <PackageReference Include="Microsoft.ProjectReunion.WinUI" Version="[0.8.2]">
            <IncludeAssets>build</IncludeAssets>
            </PackageReference>
        </ItemGroup>
        ```

4. Make the following changes to your project (.csproj or .vcproj) file:

    1. Remove this item group (if you're updating from a different version than 0.8 Preview, you will see that corresponding version number referenced in this item group):
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
            <PackageReference Include="Microsoft.ProjectReunion" Version="0.8.2" />
            <PackageReference Include="Microsoft.ProjectReunion.Foundation" Version="0.8.2" />
            <PackageReference Include="Microsoft.ProjectReunion.WinUI" Version="0.8.2" />
            <Manifest Include="$(ApplicationManifest)" />
        </ItemGroup>
        ```
5. If your solution fails to build, clean the build output, restart Visual Studio, and try re-running the app. 

## Update from 0.5 to 0.8

If you created a project using version 0.5 stable, you can follow these instructions to update your project to version 0.8 stable.

> [!NOTE]
> You may be able to automatically update your project through the Visual Studio Extension Manager, without going through the manual steps below. In Visual Studio 2019, click on **Extensions** > **Manage Extensions** and select **Updates** from the left menu bar. Select "Project Reunion" from the list and click **Update**.

Before starting, make sure you have all the Windows App SDK prerequisites installed, including the latest VSIX and NuGet package. For more details, see [Install tools for the Windows App SDK](set-up-your-development-environment.md).

First, do the following:

- In the .wapproj file, if your **TargetPlatformMinVersion** is older than 10.0.17763.0, change it to 10.0.17763.0.

Next, make these changes to your project:

1. In Visual Studio, go to **Tools** > **Nuget Package Manager** > **Package Manager Console**.

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
> You may be able to automatically update your project through the Visual Studio Extension Manager, without going through the manual steps below. In Visual Studio 2019, click on **Extensions** > **Manage Extensions** and select **Updates** from the left menu bar. Select "Project Reunion" from the list and click **Update**.

Before starting, make sure you have all the Windows App SDK prerequisites installed, including the latest VSIX and NuGet package. For more details, see [Install tools for the Windows App SDK](set-up-your-development-environment.md).

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

1. In Visual Studio, go to **Tools** > **Nuget Package Manager** > **Package Manager Console**.

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

## Update from 0.5 Preview to 0.5

If you created a project using version 0.5 preview, you can follow these instructions to update your project to stable version 0.5.7.

Before starting, make sure you have all the Windows App SDK prerequisites installed, including the latest VSIX and NuGet package. For more details, see [Install tools for the Windows App SDK](set-up-your-development-environment.md).

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

1. In Visual Studio, go to **Tools** > **Nuget Package Manager** > **Package Manager Console**.
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

## Update from WinUI 3 Preview 4 to 0.5

If you created a desktop app using WinUI 3 Preview 4, you can follow these instructions to update your project to Project Reunion 0.5.

Before starting, make sure you have all the Windows App SDK prerequisites installed, including the latest VSIX and NuGet package. For more details, see [Install tools for the Windows App SDK](set-up-your-development-environment.md).

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

1. In Visual Studio, go to **Tools** > **Nuget Package Manager** > **Package Manager Console**.
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
