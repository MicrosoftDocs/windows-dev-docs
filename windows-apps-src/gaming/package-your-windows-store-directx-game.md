---
title: Package your Universal Windows Platform (UWP) DirectX game
description: Larger Universal Windows Platform (UWP) games, especially those that support multiple languages with region-specific assets or feature optional high-definition assets, can easily balloon to large sizes.
ms.assetid: 68254203-c43c-684f-010a-9cfa13a32a77
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, games, directx, package
ms.localizationpriority: medium
---
#  Package your Universal Windows Platform (UWP) DirectX game

Larger Universal Windows Platform (UWP) games, especially those that support multiple languages with region-specific assets or feature optional high-definition assets, can easily balloon to large sizes. In this topic, learn how to use app packages and app bundles to customize your app so that your customers only receive the resources they actually need.

In addition to the app package model, Windows 10 supports app bundles which group together two types of packs:

-   App packs contain platform-specific executables and libraries. Typically, a UWP game can have up to three app packs: one each for the x86, x64, and ARM CPU architectures. All code and data specific to that hardware platform must be included in its app pack. An app pack should also contain all the core assets for the game to run with a baseline level of fidelity and performance.
-   Resource packs contain optional or expanded platform-agnostic data, such as game assets (textures, meshes, sound, text). A UWP game can have one or more resource packs, including resource packs for high-definition assets or textures, DirectX feature level 11+ resources, or language-specific assets and resources.

For more information about app bundles and app packs, read [Defining app resources](/previous-versions/windows/apps/hh965321(v=win.10)).

While you can place all content in your app packs, this is inefficient and redundant. Why have the same large texture file replicated three times for each platform, especially for ARM platforms that may not use it? A good goal is to try to minimize what your customer has to download, so they can start playing your game sooner, save space on their device, and avoid possible metered bandwidth costs.

To use this feature of the UWP app installer, it is important to consider the directory layout and file naming conventions for app and resource packaging early in game development, so your tools and source can output them correctly in a way that makes packaging simple. Follow the rules outlined in this doc when developing or configuring asset creation and managing tools and scripts, and when authoring code that loads or references resources.

## Why create resource packs?


When you create an app, particularly a game app that can be sold in many locales or a broad variety of UWP hardware platforms, you often need to include multiple versions of many files to support those locales or platforms. For example, if you are releasing your game in both the United States and Japan, you might need one set of voice files in English for the en-us locales, and another in Japanese for the jp-jp locale. Or, if you want to use an image in your game for ARM devices as well as x86 and x64 platforms, you must upload the same image asset 3 times, once for each CPU architecture.

Additionally, if your game has a lot of high definition resources that do not apply to platforms with lower DirectX feature levels, why include them in the baseline app pack and require your user to a download a large volume of components that the device can’t use? Separating these high-def resources into an optional resource pack means that customers with devices that support those high-def resources can obtain them at the cost of (possibly metered) bandwidth, while those who do not have higher-end devices can get their game quicker and at a lower network usage cost.

Content candidates for game resource packs include:

-   International locale specific assets (localized text, audio, or images)
-   High resolution assets for different device scaling factors (1.0x, 1.4x, and 1.8x)
-   High definition assets for higher DirectX feature levels (9, 10, and 11)

All of this is defined in the package.appxmanifest that is part of your UWP project, and in your directory structure of your final package. Because of the new Visual Studio UI, if you follow the process in this document, you should not need to edit it manually.

> **Important**   The loading and management of these resources are handled through the **Windows.ApplicationModel.Resources**\* APIs. If you use these app model resource APIs to load the correct file for a locale, scaling factor, or DirectX feature level, you do not need to load your assets using explicit file paths; rather, you provide the resource APIs with just the generalized file name of the asset you want, and let the resource management system obtain the correct variant of the resource for the user’s current platform and locale configuration (which you can specify directly as well with these same APIs).

 

Resources for resource packaging are specified in one of two basic ways:

-   Asset files have the same filename, and the resource pack specific versions are placed in specific named directories. These directory names are reserved by the system. For example, \\en-us, \\scale-140, \\dxfl-dx11.
-   Asset files are stored in folders with arbitrary names, but the files are named with a common label that is appended with strings reserved by the system to denote language or other qualifiers. Specifically, the qualifier strings are affixed to the generalized filename after an underscore (“\_”). For example, \\assets\\menu\_option1\_lang-en-us.png, \\assets\\menu\_option1\_scale-140.png, \\assets\\coolsign\_dxfl-dx11.dds. You may also combine these strings. For example, \\assets\\menu\_option1\_scale-140\_lang-en-us.png.
    > **Note**   When used in a filename rather than alone in a directory name, a language qualifier must take the form "lang-<tag>", for example, "lang-en-us" as described in [Tailor your resources for language, scale, and other qualifiers](../app-resources/tailor-resources-lang-scale-contrast.md).

     

Directory names can be combined for additional specificity in resource packaging. However, they cannot be redundant. For example, \\en-us\\menu\_option1\_lang-en-us.png is redundant.

You may specify any non-reserved subdirectory names you need underneath a resource directory, as long as the directory structure is identical in each resource directory. For example, \\dxfl-dx10\\assets\\textures\\coolsign.dds. When you load or reference an asset, the pathname must be generalized, removing any qualifiers for language, scale, or DirectX feature level, whether they are in folder nodes or in the file names. For example, to refer in code to an asset for which one of the variants is \\dxfl-dx10\\assets\\textures\\coolsign.dds, use \\assets\\textures\\coolsign.dds. Likewise, to refer to an asset with a variant \\images\\background\_scale-140.png, use \\images\\background.png.

Here are the following reserved directory names and filename underscore prefixes:

| Asset type                   | Resource pack directory name                                                                                                                  | Resource pack filename suffix                                                                                                    |
|------------------------------|-----------------------------------------------------------------------------------------------------------------------------------------------|----------------------------------------------------------------------------------------------------------------------------------|
| Localized assets             | All possible languages, or language and locale combinations, for Windows 10. (The qualifier prefix "lang-" is not required in a folder name.) | An "\_" followed by the language, locale, or language-locale specifier. For example, "\_en", "\_us", or "\_en-us", respectively. |
| Scaling factor assets        | scale-100, scale-140, scale-180. These are for the 1.0x, 1.4x, and 1.8x UI scaling factors, respectively.                                     | An "\_" followed by "scale-100", "scale-140", or "scale-180".                                                                    |
| DirectX feature level assets | dxfl-dx9, dxfl-dx10, and dxfl-dx11. These are for the DirectX 9, 10, and 11 feature levels, respectively.                                     | An "\_" followed by "dxfl-dx9", "dxfl-dx10", or "dxfl-dx11".                                                                     |

 

## Defining localized language resource packs


Locale-specific files are placed in project directories named for the language (for example, "en").

When configuring your app to support localized assets for multiple languages, you should:

-   Create an app subdirectory (or file version) for each language and locale you will support (for example, en-us, jp-jp, zh-cn, fr-fr, and so on).
-   During development, place copies of ALL assets (such as localized audio files, textures, and menu graphics) in the corresponding language locale subdirectory, even if they are not different across languages or locales. For the best user experience, ensure that the user is alerted if they have not obtained an available language resource pack for their locale if one is available (or if they have accidentally deleted it after download and installation).
-   Make sure each asset or string resource file (.resw) has the same name in each directory. For example, menu\_option1.png should have the same name in both the \\en-us and \\jp-jp directories even if the content of the file is for a different language. In this case, you'd see them as \\en-us\\menu\_option1.png and \\jp-jp\\menu\_option1.png.
    > **Note**   You can optionally append the locale to the file name and store them in the same directory; for example, \\assets\\menu\_option1\_lang-en-us.png, \\assets\\menu\_option1\_lang-jp-jp.png.

     

-   Use the APIs in [**Windows.ApplicationModel.Resources**](/uwp/api/Windows.ApplicationModel.Resources) and [**Windows.ApplicationModel.Resources.Core**](/uwp/api/Windows.ApplicationModel.Resources.Core) to specify and load the locale-specific resources for you app. Also, use asset references that do no include the specific locale, since these APIs determine the correct locale based on the user's settings and then retrieve the correct resource for the user.
-   In Microsoft Visual Studio 2015, select **PROJECT->Store->Create App Package...** and create the package.

## Defining scaling factor resource packs


Windows 10 provides three user interface scaling factors: 1.0x, 1.4x, and 1.8x. Scaling values for each display are set during installation based on a number of combined factors: the size of the screen, the resolution of the screen, and the assumed average distance of the user from the screen. The user can also adjust scale factors to improve readability. Your game should be both DPI-aware and scaling factor-aware for the best possible experience. Part of this awareness means creating versions of critical visual assets for each of the three scaling factors. This also includes pointer interaction and hit testing!

When configuring your app to support resource packs for different UWP app scaling factors, you should:

-   Create an app subdirectory (or file version) for each scaling factor you will support (scale-100, scale-140, and scale-180).
-   During development, place scale factor-appropriate copies of ALL assets in each scale factor resource directory, even if they are not different across scaling factors.
-   Make sure each asset has the same name in each directory. For example, menu\_option1.png should have the same name in both the \\scale-100 and \\scale-180 directories even if the content of the file is different. In this case, you'd see them as \\scale-100\\menu\_option1.png and \\scale-140\\menu\_option1.png.
    > **Note**   Again, you can optionally append the scaling factor suffix to the file name and store them in the same directory; for example, \\assets\\menu\_option1\_scale-100.png, \\assets\\menu\_option1\_scale-140.png.

     

-   Use the APIs in [**Windows.ApplicationModel.Resources.Core**](/uwp/api/Windows.ApplicationModel.Resources.Core) to load the assets. Asset references should be generalized (no suffix), leaving out the specific scale variation. The system will retrieve the appropriate scale asset for the display and the user's settings.
-   In Visual Studio 2015, select **PROJECT->Store->Create App Package...** and create the package.

## Defining DirectX feature level resource packs


DirectX feature levels correspond to GPU feature sets for prior and current versions of DirectX (specifically, Direct3D). This includes shader model specifications and functionality, shader language support, texture compression support, and overall graphics pipeline features.

Your baseline app pack should use the baseline texture compression formats: BC1, BC2, or BC3. These formats can be consumed by any UWP device, from low-end ARM platforms up to dedicated multi-GPU workstations and media computers.

Texture format support at DirectX feature level 10 or higher should be added in a resource pack to conserve local disk space and download bandwidth. This enables using the more advanced compression schemes for 11, like BC6H and BC7. (For more details, see [Texture block compression in Direct3D 11](/windows/desktop/direct3d11/texture-block-compression-in-direct3d-11).) These formats are more efficient for the high-resolution texture assets supported by modern GPUs, and using them improves the look, performance, and space requirements of your game on high-end platforms.

| DirectX feature level | Supported texture compression |
|-----------------------|-------------------------------|
| 9                     | BC1, BC2, BC3                 |
| 10                    | BC4, BC5                      |
| 11                    | BC6H, BC7                     |

 

Also, each DirectX feature levels supports different shader model versions. Compiled shader resources can be created on a per-feature level basis, and can be included in DirectX feature level resource packs. Additionally, some later version shader models can use assets, such as normal maps, that earlier shader model versions cannot. These shader model specific assets should be included in a DirectX feature level resource pack as well.

The resource mechanism is primarily focused on texture formats supported for assets, so it supports only the 3 overall feature levels. If you need to have separate shaders for sub-levels (dot versions) like DX9\_1 vs DX9\_3, your asset management and rendering code must handle them explicitly.

When configuring your app to support resource packs for different DirectX feature levels, you should:

-   Create an app subdirectory (or file version) for each DirectX feature level you will support (dxfl-dx9, dxfl-dx10, and dxfl-dx11).
-   During development, place feature level specific assets in each feature level resource directory. Unlike locales and scaling factors, you may have different rendering code branches for each feature level in your game, and if you have textures, compiled shaders, or other assets that are only used in one or a subset of all supported feature levels, put the corresponding assets only in the directories for the feature levels that use them. For assets that are loaded across all feature levels, make sure that each feature level resource directory has a version of it with the same name. For example, for a feature level independent texture named "coolsign.dds", place the BC3-compressed version in the \\dxfl-dx9 directory and the BC7-compressed version in the \\dxfl-dx11 directory.
-   Make sure each asset (if it is available to multiple feature levels) has the same name in each directory. For example, coolsign.dds should have the same name in both the \\dxfl-dx9 and \\dxfl-dx11 directories even if the content of the file is different. In this case, you'd see them as \\dxfl-dx9\\coolsign.dds and \\dxfl-dx11\\coolsign.dds.
    > **Note**   Again, you can optionally append the feature level suffix to the file name and store them in the same directory; for example, \\textures\\coolsign\_dxfl-dx9.dds, \\textures\\coolsign\_dxfl-dx11.dds.

     

-   Declare the supported DirectX feature levels when configuring your graphics resources.
    ```cpp
    D3D_FEATURE_LEVEL featureLevels[] = 
    {
      D3D_FEATURE_LEVEL_11_1,
      D3D_FEATURE_LEVEL_11_0,
      D3D_FEATURE_LEVEL_10_1,
      D3D_FEATURE_LEVEL_10_0,
      D3D_FEATURE_LEVEL_9_3,
      D3D_FEATURE_LEVEL_9_1
    };
    ```

    ```cpp
    ComPtr<ID3D11Device> device;
    ComPtr<ID3D11DeviceContext> context;
    D3D11CreateDevice(
        nullptr,                    // Use the default adapter.
        D3D_DRIVER_TYPE_HARDWARE,
        0,                      // Use 0 unless it is a software device.
        creationFlags,          // defined above
        featureLevels,          // What the app will support.
        ARRAYSIZE(featureLevels),
        D3D11_SDK_VERSION,      // This should always be D3D11_SDK_VERSION.
        &device,                    // created device
        &m_featureLevel,            // The feature level of the device.
        &context                    // The corresponding immediate context.
    );
    ```

-   Use the APIs in [**Windows.ApplicationModel.Resources.Core**](/uwp/api/Windows.ApplicationModel.Resources.Core) to load the resources. Asset references should be generalized (no suffix), leaving out the feature level. However, unlike language and scale, the system does not automatically determine which feature level is optimal for a given display; that is left to you to determine based on code logic. Once you make that determination, use the APIs to inform the OS of the preferred feature level. The system will then be able to retrieve the correct asset based on that preference. Here is a code sample that shows how to inform your app of the current DirectX feature level for the platform:
    
    ```cpp
    // Set the current UI thread's MRT ResourceContext's DXFeatureLevel with the right DXFL. 

    Platform::String^ dxFeatureLevel;
        switch (m_featureLevel)
        {
        case D3D_FEATURE_LEVEL_9_1:
        case D3D_FEATURE_LEVEL_9_2:
        case D3D_FEATURE_LEVEL_9_3:
            dxFeatureLevel = L"DX9";
            break;
        case D3D_FEATURE_LEVEL_10_0:
        case D3D_FEATURE_LEVEL_10_1:
            dxFeatureLevel = L"DX10";
            break;
        default:
            dxFeatureLevel = L"DX11";
        }

        ResourceContext::SetGlobalQualifierValue(L"DXFeatureLevel", dxFeatureLevel);
    ```

    > **Note**  In your code, load the texture directly by name (or path below the feature level directory). Do not include either the feature level directory name or the suffix. For example, load "textures\\coolsign.dds", not "dxfl-dx11\\textures\\coolsign.dds" or "textures\\coolsign\_dxfl-dx11.dds".

     

-   Now, use the [**ResourceManager**](/uwp/api/Windows.ApplicationModel.Resources.Core.ResourceManager) to locate the file that matches current DirectX feature level. The **ResourceManager** returns a [**ResourceMap**](/uwp/api/Windows.ApplicationModel.Resources.Core.ResourceMap), which you query with [**ResourceMap::GetValue**](/uwp/api/windows.applicationmodel.resources.core.resourcemap.getvalue) (or [**ResourceMap::TryGetValue**](/uwp/api/windows.applicationmodel.resources.core.resourcemap.trygetvalue)) and a supplied [**ResourceContext**](/uwp/api/Windows.ApplicationModel.Resources.Core.ResourceContext). This returns a [**ResourceCandidate**](/uwp/api/Windows.ApplicationModel.Resources.Core.ResourceCandidate) that most closely matches the DirectX feature level that was specified by calling [**SetGlobalQualifierValue**](/uwp/api/windows.applicationmodel.resources.core.resourcecontext.setglobalqualifiervalue).
    
    ```cpp
    // An explicit ResourceContext is needed to match the DirectX feature level for the display on which the current view is presented.

    auto resourceContext = ResourceContext::GetForCurrentView();
    auto mainResourceMap = ResourceManager::Current->MainResourceMap;

    // For this code example, loader is a custom ref class used to load resources.
    // You can use the BasicLoader class from any of the 8.1 DirectX samples similarly.


    auto possibleResource = mainResourceMap->GetValue(
        L"Files/BumpPixelShader.cso",
        resourceContext
    );
    Platform::String^ resourceName = possibleResource->ValueAsString;
    ```

-   In Visual Studio 2015, select **PROJECT->Store->Create App Package...** and create the package.
-   Make sure that you enable app bundles in the package.appxmanifest manifest settings.

## Related topics


* [Defining app resources](/previous-versions/windows/apps/hh965321(v=win.10))
* [Packaging apps](../packaging/index.md)
* [App packager (MakeAppx.exe)](/windows/desktop/appxpkg/make-appx-package--makeappx-exe-)

 

 