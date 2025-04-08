---
title: Additional migration guidance
description: This topic contains additional migration guidance not categorized into a feature area in the [feature area guides](feature-area-guides-ovw.md).
ms.topic: article
ms.date: 09/16/2021
keywords: Windows, App, SDK, migrate, migrating, migration, port, porting
ms.localizationpriority: medium
---

# Additional migration guidance

This topic contains additional migration guidance not categorized into a feature area in the [feature area guides](guides/feature-area-guides-ovw.md).

## Conditional compilation

The info in this section might be useful if you plan to use the same source code file in both a UWP and a Windows App SDK project.

In C# source code in a Windows App SDK project, you can use preprocessor directives with the **WINDOWS_UWP** symbol to perform conditional compilation.

```csharp
#if !WINDOWS_UWP
    // Win32/Desktop code, including Windows App SDK code
#else
    // UWP code
#endif
```

In C++/WinRT source code in a Windows App SDK project, you can use preprocessor directives with **WINAPI_FAMILY_PC_APP** to do the same thing. Or you could use **WINAPI_FAMILY_DESKTOP_APP** instead. A comment in the `winapifamily.h` header file indicates that **WINAPI_FAMILY_APP** should be considered deprecated.

```cppwinrt
#if (WINAPI_FAMILY == WINAPI_FAMILY_DESKTOP_APP)
    // Win32/Desktop code, including Windows App SDK code
#else
    // UWP code
#endif
```

You can also use conditional compilation in XAML markup.

```xaml
<Application
    ...
    xmlns:nouwp="condition:!WINDOWS_UWP"
    mc:Ignorable="nouwp">
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <!--Not Needed for UWP-->
                <nouwp:XamlControlsResources xmlns="using:Microsoft.UI.Xaml.Controls" />
                 <!--Other merged dictionaries here--> 
            </ResourceDictionary.MergedDictionaries>
             <!--Other app resources here--> 
        </ResourceDictionary>
    </Application.Resources>
</Application>
```

Also see [Conditional compilation](../../desktop/modernize/desktop-to-uwp-enhance.md#conditional-compilation).

## Debugging

During the migration process, you might find your app in a state where your XAML markup has references to XAML resource keys, but you haven't yet defined those keys. Such a condition results in a run-time crash that might not be straightforward to debug. But in a debug build, messages about missing resource keys appear in Visual Studio via debug output in the **Output** pane. So run your app under the debugger, and watch out for such messages.

## Unregistering an event handler (C++/WinRT)

In a C++/WinRT project, you can manually revoke (unregister) an event handler such as **SizeChanged** (for more details, and code examples, see [Revoke a registered delegate](/windows/uwp/cpp-and-winrt-apis/handle-events#revoke-a-registered-delegate)). But an alternative to manually revoking&mdash;and one that you could consider if you're having problems with manually revoking&mdash;is to use a C++/WinRT auto event revoker. Again, more details and code examples in [Revoke a registered delegate](/windows/uwp/cpp-and-winrt-apis/handle-events#revoke-a-registered-delegate).

## See Also

- [Windows App SDK and supported Windows releases](../support.md)
