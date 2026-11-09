---
title: Use Windows.UI.Palettes with locally generated metadata
description: Generate Windows.UI.Palettes metadata, register a palette extension, and create an item using the complete IDL and version header.
author: GrantMeStrength
ms.author: jken
ms.topic: how-to
ms.date: 11/05/2026
ms.localizationpriority: low
---

# Use Windows.UI.Palettes with locally generated metadata

The **Windows.UI.Palettes** Windows Runtime APIs let palette extensions supply content and structural items to the new Windows Search UX. An extension implements **IPaletteItemSourceExtension** to provide items and respond to queries. You create mutable **PaletteItem** objects with a **PaletteItemFactory** and return **PaletteItemView** objects to consumers.

> [!NOTE]
> Availability of palette extensions in Windows Search varies by region.

This article provides the complete IDL and its required version header so you can generate metadata and a C++/WinRT projection before a Windows SDK supplies the Palettes namespace. The declarations use **PalettesContract** version 2.0.

> [!IMPORTANT]
> Generating metadata or compiling a client does not install the Palettes runtime or establish that your target Windows version supports it. A minimum supported Windows release is not specified here. You must confirm that your target system provides the required runtime and host before running your app.

## Build-time requirements

You need a Windows SDK with MIDL/MIDLRT, standard Windows metadata, and the Windows Runtime base definitions. You also need the Visual Studio C++ build tools for preprocessing the IDL and compiling your client. The commands below use the C++/WinRT projection tool included in Windows SDK `10.0.22621.0`; that SDK's metadata does not need to contain Palettes.

Use only one source of Palettes metadata in a build. Do not combine this copied definition with another SDK or WinMD definition of the same namespace.

## Generate metadata and a C++/WinRT projection

1. Save the [complete IDL](#copyable-idl) as *Windows.UI.Palettes.idl* and the [required version header](#required-version-header) as *Windows.UI.Palettes.Version.h* in the same directory. The IDL includes that header; copying only the IDL leaves its contract-version macro undefined.

   Preserve the namespace, declaration order, parameter types and direction, attributes, and version definitions. These affect the generated contract and ABI. Do not move the declarations into your application's namespace or remove attributes to make compilation succeed.

1. Open an **x64 Native Tools Command Prompt for Visual Studio** and change to the directory containing the two files. Run these commands:

   ```bat
   set "SDK_ROOT=%ProgramFiles(x86)%\Windows Kits\10"
   set "SDK_VERSION=10.0.22621.0"
   if not exist generated mkdir generated
   "%SDK_ROOT%\bin\%SDK_VERSION%\x64\midl.exe" ^
     /nologo /winrt /nomidl ^
     /metadata_dir "%SDK_ROOT%\UnionMetadata\%SDK_VERSION%" ^
     /reference "%SDK_ROOT%\UnionMetadata\%SDK_VERSION%\Windows.winmd" ^
     /I "%SDK_ROOT%\Include\%SDK_VERSION%\winrt" ^
     /winmd Windows.UI.Palettes.winmd ^
     /out generated Windows.UI.Palettes.idl
   "%SDK_ROOT%\bin\%SDK_VERSION%\x64\cppwinrt.exe" ^
     -input generated\Windows.UI.Palettes.winmd ^
     -reference "%SDK_ROOT%\UnionMetadata\%SDK_VERSION%\Windows.winmd" ^
     -output generated\projection
   ```

   The MIDL `/nomidl` option selects the metadata-only path required when supplying `/reference`. The SDK metadata provides standard types such as **Windows.Foundation.Uri**, collection interfaces, and **Windows.Graphics.Imaging.SoftwareBitmap**. The copied IDL and version header define Palettes.

1. Add *generated\projection* to your C++ project's include directories before other projections, and use the matching SDK C++/WinRT headers. Configure the example for C++20 (`/std:c++20`), include *winrt/Windows.UI.Palettes.h*, and use your application's normal Windows Runtime compiler and linker settings.

The generated WinMD is a build-time input, not an implementation DLL. It does not register or provide a runtime implementation. Do not generate component implementations for the system-provided Palettes classes or register them as classes owned by your application.

## Create a palette item

The following C++/WinRT helper creates a command that opens **Settings**. Pass the **PaletteItemFactory** that your extension receives in **IPaletteItemSourceExtension.Initialize**; the factory has no public constructor. Call the helper from an initialized Windows Runtime apartment in an extension running on a system with the Palettes runtime.

```cpp
#include <winrt/Windows.Foundation.h>
#include <winrt/Windows.UI.Palettes.h>

winrt::Windows::UI::Palettes::PaletteItemView CreateSettingsItem(
    winrt::Windows::UI::Palettes::PaletteItemFactory const& factory)
{
    using namespace winrt::Windows::UI::Palettes;

    PaletteKinds kinds({ KnownPaletteKinds::Command() });
    auto item = factory.Create(L"open-settings", kinds);
    item.Title(L"Open Settings");
    item.Icon(PaletteIconData::FromGlyph(L"\uE713"));
    item.Invoker(PaletteUriInvoker(winrt::Windows::Foundation::Uri(L"ms-settings:")));

    // Return the consumer view, not the mutable item.
    return item.GetView();
}
```

This helper creates an item; it does not register an extension or invoke the command.

## Runtime and provider requirements

Your target system must provide the **Windows.UI.Palettes** runtime, its activation and marshaling support, and a host for the operations you use. Hosts determine which system actions are available and how they present items and handle interaction.

An item-source extension implements **IPaletteItemSourceExtension**, provides an out-of-process COM server, and registers the **com.microsoft.palettes** app-extension contract. Generating the WinMD does not perform these steps.

## Register a palette extension

In *Package.appxmanifest*, merge the following entries into your application's **Extensions** element. Replace the executable name, display names, extension **Id**, and example GUID with your app's values. Use the same COM class identifier (CLSID) in **com:Class** and **ComObjects**; this is your server class's identifier, not the interface identifier.

```xml
<Extensions
    xmlns="http://schemas.microsoft.com/appx/manifest/foundation/windows10"
    xmlns:com="http://schemas.microsoft.com/appx/manifest/com/windows10"
    xmlns:uap3="http://schemas.microsoft.com/appx/manifest/uap/windows10/3">
  <com:Extension Category="windows.comServer">
    <com:ComServer>
      <com:ExeServer Executable="ContosoPalette.exe"
                     Arguments="-RegisterProcessAsComServer"
                     DisplayName="Contoso palettes">
        <com:Class Id="01234567-89ab-cdef-0123-456789abcdef" DisplayName="Contoso palettes" />
      </com:ExeServer>
    </com:ComServer>
  </com:Extension>
  <uap3:Extension Category="windows.appExtension">
    <uap3:AppExtension Name="com.microsoft.palettes"
                      Id="ContosoCommands"
                      DisplayName="Contoso commands"
                      PublicFolder="Public">
      <uap3:Properties>
        <MinimumPaletteApiVersion>2.0</MinimumPaletteApiVersion>
        <ComObjects>
          <Windows.UI.Palettes.IPaletteItemSourceExtension>01234567-89ab-cdef-0123-456789abcdef</Windows.UI.Palettes.IPaletteItemSourceExtension>
        </ComObjects>
        <Kinds>
          <KindSpec>ZeroState.*</KindSpec>
          <KindSpec>QueryResult.*</KindSpec>
        </Kinds>
        <ContainedKinds>
          <KindSpec>Command</KindSpec>
        </ContainedKinds>
      </uap3:Properties>
    </uap3:AppExtension>
  </uap3:Extension>
</Extensions>
```

This is a manifest fragment, not a complete package manifest. Set **PublicFolder** to a folder included in your package and deploy the updated package to register the declarations. **MinimumPaletteApiVersion** identifies the Palettes API version, not a Windows release.

The example declares a source that supplies Zero State and query-result items of kind **Command**. Adjust **Kinds** and **ContainedKinds** to match your source and the host's requirements.

Implement **IPaletteItemSourceExtension** on the registered COM class. The executable must recognize `-RegisterProcessAsComServer` and register a class factory for the same CLSID; change **Arguments** if your server uses a different startup convention.

Your server must accept headless COM activation and remain available while clients or in-flight work need it, independently of any application UI. The registration does not provide the server implementation or install the Palettes runtime.

## Copyable IDL

Save this block as *Windows.UI.Palettes.idl*. Nonessential source comments are omitted; the complete declarations, their order, and their attributes are preserved.

```idl
// Copyright (c) Microsoft Corporation. All rights reserved.

#include "Windows.UI.Palettes.Version.h"

namespace Windows.UI.Palettes
{
    [contractversion(PALETTES_VERSION_LATEST)] apicontract PalettesContract {}

    [contract(PalettesContract, 2.0)] runtimeclass PaletteItem;
    [contract(PalettesContract, 2.0)] runtimeclass PaletteItemView;
    [contract(PalettesContract, 2.0)] runtimeclass PaletteQuery;
    [contract(PalettesContract, 2.0)] runtimeclass PaletteKinds;
    [contract(PalettesContract, 2.0)] runtimeclass PaletteContent;

    [contract(PalettesContract, 2.0)]
    enum PaletteItemAnalyticsOutcome
    {

        None = 0,

        Dismissed = 1,

        Selected = 2,

        OtherSelected = 3,
    };

    [contract(PalettesContract, 2.0)]
    runtimeclass PaletteItemAnalytics
    {
        PaletteItemAnalytics(
            String itemId, String extensionId, Windows.Foundation.DateTime timestamp);
        PaletteItemAnalytics(
            String itemId,
            String extensionId,
            Windows.Foundation.DateTime timestamp,
            Windows.Foundation.Collections.IMapView<String, Object> context);

        String ItemId { get; };
        String ExtensionId { get; };
        Windows.Foundation.DateTime Timestamp { get; };
        Boolean IsVisible { get; };
        Windows.Foundation.Collections.IMapView<String, Object> Context { get; };

        PaletteItemAnalyticsOutcome Outcome;
    }

    [contract(PalettesContract, 2.0)]
    static runtimeclass VersionInfo
    {
        static UInt16 MAJOR { get; };
        static UInt16 MINOR { get; };
        static Boolean IsEligible(UInt16 appMajor, UInt16 appMinor);
    }

    [contract(PalettesContract, 2.0)]
    runtimeclass PalettePropertyChangedEventArgs
    {
        String PropertyName { get; };
    }

    [contract(PalettesContract, 2.0)]
    delegate Windows.Graphics.Imaging.SoftwareBitmap IconResourceDelegate(
        Windows.Foundation.Collections.IMap<String, String> qualifierValues);

    [contract(PalettesContract, 2.0)]
    runtimeclass PaletteIconData
    {
        static PaletteIconData FromResourceQualifier(IconResourceDelegate handler);
        static PaletteIconData FromBitmap(Windows.Graphics.Imaging.SoftwareBitmap bitmap);
        static PaletteIconData FromUri(Windows.Foundation.Uri iconUri);
        static PaletteIconData FromGlyph(String iconGlyph);
        static PaletteIconData FromGlyph(String iconGlyph, String fontFamily);
        static PaletteIconData FromStreamReference(
            Windows.Storage.Streams.IRandomAccessStreamReference streamReference);

        Windows.Foundation.Uri IconUri { get; };
        String IconGlyph { get; };
        String FontFamily { get; };

        Windows.Storage.Streams.IRandomAccessStreamReference ImageStreamReference { get; };
        Windows.Graphics.Imaging.SoftwareBitmap Bitmap { get; };

        Windows.Foundation.IAsyncOperation<Windows.Graphics.Imaging.SoftwareBitmap> GetBitmapAsync(
            Windows.Foundation.Collections.IMap<String, String> qualifierValues);
    }

    [contract(PalettesContract, 2.0)]
    runtimeclass PaletteTag
    {
        PaletteTag(PaletteIconData icon, String text, String tooltip);

        PaletteIconData Icon { get; };
        String Text { get; };
        String ToolTip { get; };
    }

    [contract(PalettesContract, 2.0)]
    unsealed runtimeclass PaletteInvoker
    {
        void Invoke(PaletteQuery query);
    }

    [contract(PalettesContract, 2.0)]
    enum SystemInvokerKind
    {
        PickFiles = 0,
        PickFolders = 1,
        PickAgent = 2,
        Navigate = 3,
        Query = 4,

        TakeScreenshot = 5,
    };

    [contract(PalettesContract, 2.0)]
    runtimeclass PaletteSystemInvoker : PaletteInvoker
    {
        PaletteSystemInvoker(SystemInvokerKind kind);
        PaletteSystemInvoker(
            SystemInvokerKind kind,
            Windows.Foundation.Collections.IMapView<String, Object> context);

        SystemInvokerKind Kind { get; };
        Windows.Foundation.Collections.IMapView<String, Object> Context { get; };
    }

    [contract(PalettesContract, 2.0)]
    runtimeclass PaletteUriInvoker : PaletteInvoker
    {
        PaletteUriInvoker(Windows.Foundation.Uri uri);

        PaletteUriInvoker(
            Windows.Foundation.Uri uri,
            Windows.Foundation.Collections.IMapView<String, Object> launcherOptions);

        Windows.Foundation.Uri Uri { get; };
    }

    [contract(PalettesContract, 2.0)]
    runtimeclass PaletteToggleInvoker : PaletteInvoker
    {
        PaletteToggleInvoker(PaletteInvoker invoker, Boolean isToggled);

        Boolean IsToggled { get; };
    }

    [contract(PalettesContract, 2.0)]
    delegate void PaletteItemInvokeDelegate(PaletteQuery query);

    [default_interface]
    [contract(PalettesContract, 2.0)]
    runtimeclass PaletteDelegateInvoker : PaletteInvoker
    {
        PaletteDelegateInvoker(PaletteItemInvokeDelegate handler);
    }

    [contract(PalettesContract, 2.0)]
    delegate Windows.Foundation.IAsyncOperation<PaletteContent>
        PaletteContentDelegate(String itemId);

    [contract(PalettesContract, 2.0)]
    runtimeclass PaletteItemFactory
    {
        String ExtensionId { get; };

        PaletteItem Create(String id);
        PaletteItem Create(String id, PaletteKinds kinds);
    }

    [contract(PalettesContract, 2.0)]
    runtimeclass PaletteItem
    {
        String Id { get; };
        String ExtensionId { get; };

        PaletteIconData Icon;
        PaletteIconData IconOverlay;
        String Title;
        Object[] Subtitle;
        PaletteTag[] Tags;

        Windows.Foundation.Collections.IMap<String, Object> Properties { get; };

        PaletteItemView GetView();

        PaletteStatus Status;

        PaletteKinds Kinds { get; };
        PaletteInvoker Invoker;
        ExecutePaletteQueryDelegate ExecuteQueryDelegate;
        PaletteContentDelegate ContentDelegate;

        event Windows.Foundation.TypedEventHandler<
            PaletteItem, PalettePropertyChangedEventArgs> PropertyChanged;
    }

    [contract(PalettesContract, 2.0)]
    enum KindsSelector
    {
        All = 0,
        Any = 1,
        None = 2,
        ExactlyOne = 3,
    };

    [contract(PalettesContract, 2.0)]
    runtimeclass PaletteKinds
    {
        PaletteKinds(String[] values);
        String[] Values { get; };
        String GetPrimaryKind();
        Boolean IsOfKind(String kindSpec);
        Boolean IsOfKinds(String[] kindSpecs, KindsSelector selector);
    };

    [contract(PalettesContract, 2.0)]
    runtimeclass PaletteItemView : Windows.Foundation.IStringable
    {
        String Id { get; };
        String ExtensionId { get; };
        PaletteIconData Icon { get; };
        PaletteIconData IconOverlay { get; };
        String Title { get; };
        Object[] Subtitle { get; };
        PaletteTag[] Tags { get; };

        Windows.Foundation.Collections.IMapView<String, Object> Properties { get; };

        Windows.Foundation.IAsyncOperationWithProgress<
            PaletteQueryResultsView, PaletteQueryProgress>
            ExecuteQueryAsync(PaletteQuery query);

        PaletteStatus Status { get; };

        Boolean IsContentAvailable { get; };
        Windows.Foundation.IAsyncOperation<PaletteContentView> GetContentAsync();

        PaletteKinds Kinds { get; };
        event Windows.Foundation.TypedEventHandler<PaletteItemView, Object> ContentChanged;

        PaletteInvoker TryGetInvoker();

        event Windows.Foundation.TypedEventHandler<
            PaletteItemView, PalettePropertyChangedEventArgs> PropertyChanged;
    }

    [contract(PalettesContract, 2.0)]
    enum PaletteContentKind
    {
        Unknown = 0,
        Template = 1,
    };

    [contract(PalettesContract, 2.0)]
    unsealed runtimeclass PaletteContent
    {
        PaletteContentKind Kind { get; };
        PaletteContentView GetView();
    }

    [contract(PalettesContract, 2.0)]
    unsealed runtimeclass PaletteContentView
    {
        PaletteContentKind Kind { get; };
    }

    [contract(PalettesContract, 2.0)]
    delegate void PaletteTemplateActionInvokeDelegate(
        PaletteTemplateContent content, String actionId, String data, String inputs);

    [contract(PalettesContract, 2.0)]
    runtimeclass PaletteTemplateContent : PaletteContent
    {
        PaletteTemplateContent(PaletteTemplateActionInvokeDelegate invokeAction);

        String TemplateJson;
        String DataJson;

        event Windows.Foundation.TypedEventHandler<
            PaletteTemplateContent, PalettePropertyChangedEventArgs> PropertyChanged;
    }

    [contract(PalettesContract, 2.0)]
    runtimeclass PaletteTemplateContentView : PaletteContentView
    {
        String TemplateJson { get; };
        String DataJson { get; };

        void InvokeAction(String actionId, String dataJson, String inputJson);

        event Windows.Foundation.TypedEventHandler<
            PaletteTemplateContentView, PalettePropertyChangedEventArgs> PropertyChanged;
    }

    [contract(PalettesContract, 2.0)]
    enum PaletteMatchType
    {
        Unknown = 0,
        MostRelevantPropertiesMatch = 1,
        FullTextMatch = 2,
        SemanticFileMatch = 3,
        StorageProviderMatch = 4,

        SemanticSettingsMatch = 5,
        RemoteFileMatch = 6,

    };

    [contract(PalettesContract, 2.0)]
    static runtimeclass KnownPaletteProperties
    {

        static String Path{ get; };

        static String CommandGlyph{ get; };

        static String UndoItemId{ get; };

        static String ParentItemId{ get; };

        static String KindSpecificId{ get; };

        static String Score{ get; };

        static String MatchType{ get; };

        static String LastAccessedDate{ get; };

        static String LastModifiedDate{ get; };

        static String CreatedDate{ get; };

        static String ExpectedKinds{ get; };

        static String AdditionalQueries{ get; };

        static String FileTypeFilter{ get; };

        static String SourceKinds{ get; };

        static String Query{ get; };

        static String ExcludeFromHistory{ get; };
    }

    [contract(PalettesContract, 2.0)]
    static runtimeclass KnownPaletteKinds
    {
        static String Container { get; };
        static String Command { get; };

        static String File { get; };

        static String Application { get; };
        static String Setting { get; };
        static String Picture { get; };
        static String Suggestion { get; };

        static String FilesSpec { get; };

        static String Directory { get; };

        static String ZeroStateSpec { get; };
        static String ZeroStateSystemComposer { get; };
        static String ZeroStateRunUX { get; };

        static String QueryResultSpec { get; };
        static String QueryResultSystemComposer { get; };
        static String QueryResultStartMenu { get; };

        static String WindowsAction { get; };

        static String WebSearchSpec { get; };
        static String WebSearchSystemComposer { get; };

        static String AppIdSpec { get; };

        static String Toggle { get; };

        static String Assistant { get; };

        static String AppStoreSpec { get; };

        static String Placeholder { get; };
    }

    [contract(PalettesContract, 2.0)]
    enum PaletteQueryMode
    {

        Search = 0,

        Children = 1,

        Commands = 2,
    };

    [contract(PalettesContract, 2.0)]
    runtimeclass PaletteQuery
    {
        PaletteQuery();

        String ToJson();

        static PaletteQuery FromJson(String json);

        Guid QueryId { get; };

        Object[] Arguments;
        String DisplayLanguageTag;
        String SystemLanguageTag;
        Int32 ItemCount;
        PaletteItemView[] Scopes;

        String[] RequestedProperties;
        Windows.Foundation.Collections.IMap<String, Object> Context { get; };

        PaletteQueryMode Mode;
    }

    [contract(PalettesContract, 2.0)]
    runtimeclass PaletteQueryProgress
    {
        PaletteQueryProgress(PaletteItemView[] items, PaletteProgressState progressState);

        PaletteItemView[] Items { get; };
        PaletteProgressStateView PaletteProgressState { get; };
    }

    [contract(PalettesContract, 2.0)]
    delegate Windows.Foundation.IAsyncOperationWithProgress<
        PaletteQueryResultsView, PaletteQueryProgress>
        ExecutePaletteQueryDelegate(PaletteItemFactory factory, PaletteQuery query);

    [contract(PalettesContract, 2.0)]
    runtimeclass PaletteQueryResults
    {
        PaletteQueryResults(PaletteQuery query);
        PaletteQueryResults(PaletteQuery query, ExecutePaletteQueryDelegate getNextDelegate);

        Guid QueryId { get; };

        PaletteItemView[] Items;
        Boolean HasMoreItems;

        PaletteQueryResultsView GetView();
    }

    [contract(PalettesContract, 2.0)]
    runtimeclass PaletteQueryResultsView
    {
        Guid QueryId { get; };

        PaletteItemView[] Items { get; };
        Boolean HasMoreItems { get; };

        Windows.Foundation.IAsyncOperationWithProgress<
            PaletteQueryResultsView, PaletteQueryProgress> GetMoreItemsAsync();
    }

    [contract(PalettesContract, 2.0)]
    enum PaletteSeverity
    {
        Informational = 0,
        Success,
        Warning,
        Error,
    };

    [contract(PalettesContract, 2.0)]
    runtimeclass PaletteProgressState
    {
        PaletteProgressState(Boolean isIndeterminate, Single progress);

        Boolean IsIndeterminate;
        Single Progress;

        PaletteProgressStateView GetView();

        event Windows.Foundation.TypedEventHandler<
            PaletteProgressState, PalettePropertyChangedEventArgs> PropertyChanged;
    }

    [contract(PalettesContract, 2.0)]
    runtimeclass PaletteProgressStateView
    {
        Boolean IsIndeterminate { get; };
        Single Progress { get; };

        event Windows.Foundation.TypedEventHandler<
            PaletteProgressStateView, PalettePropertyChangedEventArgs> PropertyChanged;
    }

    [contract(PalettesContract, 2.0)]
    runtimeclass PaletteStatus
    {
        PaletteStatus(String message, PaletteSeverity severity);
        PaletteStatus(String message, PaletteSeverity severity, PaletteProgressState progress);
        PaletteStatus(
            String message,
            PaletteSeverity severity,
            PaletteProgressState progress,
            PaletteItemView item);

        PaletteSeverity Severity { get; };
        String Message { get; };

        PaletteProgressState Progress { get; };
        PaletteItemView Item { get; };
    }

    [contract(PalettesContract, 2.0)]
    runtimeclass PaletteExtensionHost
    {
        Windows.Foundation.IAsyncAction ReportStatusAsync(PaletteStatus status);
        [method_name("ReportStatusAsyncWithMessage")]
        Windows.Foundation.IAsyncAction ReportStatusAsync(String message, PaletteSeverity severity);
        [method_name("ReportStatusAsyncWithProgress")]
        Windows.Foundation.IAsyncAction ReportStatusAsync(
            String message, PaletteSeverity severity, PaletteProgressState progress);
        [method_name("ReportStatusAsyncWithItem")]
        Windows.Foundation.IAsyncAction ReportStatusAsync(
            String message,
            PaletteSeverity severity,
            PaletteProgressState progress,
            PaletteItemView item);

        PaletteSystemInvoker GetSystemInvoker(SystemInvokerKind kind);
        [method_name("GetSystemInvokerWithContext")]
        PaletteSystemInvoker GetSystemInvoker(
            SystemInvokerKind kind,
            Windows.Foundation.Collections.IMapView<String, Object> context);

        event Windows.Foundation.TypedEventHandler<PaletteExtensionHost, Object> Dismissed;

        void RequestUnload();
    }

    [contract(PalettesContract, 2.0)]
    interface IPaletteItemSourceExtension
    {

        void Initialize(PaletteItemFactory factory, PaletteExtensionHost host);

        PaletteItemView GetItem(String id);
        PaletteItemView[] GetItems();

        Windows.Foundation.IAsyncOperationWithProgress<
            PaletteQueryResultsView, PaletteQueryProgress>
            ExecuteQueryAsync(PaletteQuery query);

        Windows.Foundation.IAsyncAction ReportItemAnalyticsAsync(PaletteItemAnalytics[] analytics);

        Windows.Foundation.IAsyncAction ReportConversationAnalyticsAsync(
            PaletteItemAnalytics[] itemAnalytics);

        event Windows.Foundation.TypedEventHandler<
            IPaletteItemSourceExtension, Object> ItemsChanged;
    }

    [contract(PalettesContract, 2.0)]
    runtimeclass PaletteExtensionDefinition
    {
        IPaletteItemSourceExtension CreateExtension(PaletteExtensionHost host);

        PaletteItemFactory ItemFactory { get; };

        String ExtensionId { get; };
        String PackageFamilyName { get; };
        String ApplicationId { get; };

        PaletteIconData Icon { get; };
        String DisplayName { get; };
        String Description { get; };

        String[] AcceptedKindSpecs { get; };

        String Publisher { get; };

        Windows.Foundation.Collections.IMap<String, Object> Properties { get; };

        PaletteKinds Kinds { get; };

        PaletteKinds ContainedKinds { get; };
    }

    [contract(PalettesContract, 2.0)]
    runtimeclass PaletteCatalog
    {
        [default_overload]
        static PaletteItemView[] Parse(String input);
        static PaletteItemView[] Parse(String input, PaletteExtensionHost host);

        [default_overload]
        [return_name("succeeded")] static Boolean TryParse(
            String input, out PaletteItemView[] items);
        [return_name("succeeded")] static Boolean TryParse(
            String input, PaletteExtensionHost host, out PaletteItemView[] items);

        static PaletteCatalog OpenDefault();

        [method_name("OpenWithKinds")]
        static PaletteCatalog Open(PaletteKinds kindSpecs);

        [method_name("OpenWithKindsAndConfig")]
        static PaletteCatalog Open(PaletteKinds kindSpecs, String configRegistryKeyPath);

        PaletteExtensionDefinition GetPalette(String extensionId);
        PaletteExtensionDefinition[] GetPalettes();

        static Boolean IsCallerIdentityAccessSystemFor(Object calledObject);
        static PaletteItemView TryGetItem(String extensionId, String itemId);

        event Windows.Foundation.TypedEventHandler<PaletteCatalog, Object> Changed;
    }

}
```

## Required version header

Save this block as *Windows.UI.Palettes.Version.h* beside the IDL. Its definitions supply the contract version used by the IDL.

```cpp
// Copyright (c) Microsoft Corporation. All rights reserved.

#define PALETTES_VERSION_MAJOR 2
#define PALETTES_VERSION_MINOR 0

#define PALETTES_VERSION_LATEST PALETTES_VERSION_MAJOR.PALETTES_VERSION_MINOR
```
