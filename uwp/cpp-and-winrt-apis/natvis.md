---
title: Visual Studio native debug visualization for C++/WinRT
description: The [C++/WinRT Visual Studio Extension (VSIX)](https://marketplace.visualstudio.com/items?itemName=CppWinRTTeam.cppwinrt101804264) gives you Visual Studio native debug visualization (natvis) of C++/WinRT projected types. This provides you an experience similar to C# debugging.
ms.date: 04/20/2021
ms.topic: article
keywords: windows 10, uwp, standard, c++, cpp, winrt, projection, Visual Studio, native debug visualization, debug visualization, visualization
ms.localizationpriority: medium
---

# Visual Studio native debug visualization (natvis) for C++/WinRT

The [C++/WinRT Visual Studio Extension (VSIX)](https://marketplace.visualstudio.com/items?itemName=CppWinRTTeam.cppwinrt101804264) gives you Visual Studio native debug visualization (natvis) of C++/WinRT projected types. This provides you an experience similar to C# debugging.

> [!NOTE]
> For more info about the C++/WinRT Visual Studio Extension (VSIX), see [Visual Studio support for C++/WinRT, and the VSIX](./intro-to-using-cpp-with-winrt.md#visual-studio-support-for-cwinrt-xaml-the-vsix-extension-and-the-nuget-package).

## Enabling natvis

Natvis is automatically on for a debug build because **WINRT_NATVIS** is defined when the **_DEBUG** symbol is defined.

Here's how to opt into it for a release build.

* Compile your code with the symbol **WINRT_NATVIS** defined. Doing so exports a **WINRT_abi_val** function, which provides the entry point for the debug visualizer to evaluate property values in the target process.
* Generate a full PDB. This is because the debug visualizer uses the Visual Studio C++ Expression Evaluator, which in turn requires symbolic definitions for displayed property types.
* A visualized type must report a runtime class or an interface defined in discoverable metadata. It does this via its implementation of [IInspectable::GetRuntimeClassName](/windows/win32/api/inspectable/nf-inspectable-iinspectable-getruntimeclassname).

Given the above, the debug visualizer works best with Windows system types for which metadata can be found in the `C:\Windows\System32\WinMetadata` folder. However, it can also support user-defined types and remote debugging, provided that you properly locate `.winmd` files.

## Using custom metadata

The debug visualizer looks for user-defined metadata (`.winmd` files) alongside the process `.exe`. It uses an algorithm similar to that of [RoGetMetaDataFile](/windows/win32/api/rometadataresolution/nf-rometadataresolution-rogetmetadatafile), probing for successive substrings of the fully-qualified typename. For example, if the type being visualized is **Contoso.Controls.Widget**, then the visualizer looks, in sequence, for:

* Contoso.Controls.Widget.winmd
* Contoso.Controls.winmd
* Contoso.winmd

## Remote debugging with custom metadata

When remote debugging, the process `.exe` isn't local, so the search for custom metadata (mentioned in the previous section) fails. In that case, the visualizer falls back to a local cache folder (`%TEMP%`) for a suitable `.winmd` file. If it finds one, then it records the size and date of the file, and then searches the remote debugging target for the same `.winmd` alongside the binary. If necessary, the remote file is downloaded, updating the local cache. This strategy ensures that the locally cached `.winmd` is always up to date, as well as providing a means for manually caching a .`winmd` if it can't be found remotely (for example, if F5 deployment didn't put it there).

For an example of the caching behavior, see the [Troubleshooting](#troubleshooting) section below.

## Troubleshooting

The debug visualizer uses the Visual Studio C++ Expression Evaluator to invoke the exported **WINRT_abi_val** function to obtain property values. Normally, the visualizer can catch unhandled exceptions, and degrade gracefully, displaying "\<Object uninitialized or information unavailable>" in Visual Studio **Watch** windows.

That's useful when the visualizer tries to evaluate a local variable outside of its lifetime scope (for example, before construction). In some contexts, such as unit tests, an unhandled exception filter is installed. This can cause the process to terminate when the C++ expression evaluator faults. To prevent faulting, the visualizer makes several [VirtualQuery](/windows/win32/api/memoryapi/nf-memoryapi-virtualquery) calls in **WINRT_abi_val**.

### Diagnostics

If a property isn't being displayed correctly, then turn on verbose natvis diagnostics in Visual Studio (**Tools** > **Options** > **Debugging** > **Output Window** > **Natvis diagnostic messages**), and then observe the **Output** window for natvis errors.

The following excerpt shows several attempts to probe for a `.winmd` file, followed by a download from the remote target to the local cache folder, and then a load of that `.winmd` file.

```console
Natvis C++/WinRT: Looking for C:\Users\...\AppData\Local\DevelopmentFiles\ffcddd4f-cfc0-44cb-b736-0b2d026def77VS.Debug_x64....\Consoso.Controls.Widget.winmd
Natvis C++/WinRT: Looking for C:\Users\...\AppData\Local\DevelopmentFiles\ffcddd4f-cfc0-44cb-b736-0b2d026def77VS.Debug_x64....\Consoso.Controls.winmd
Natvis C++/WinRT: Downloading C:\Users\...\AppData\Local\DevelopmentFiles\ffcddd4f-cfc0-44cb-b736-0b2d026def77VS.Debug_x64....\Consoso.Controls.winmd
Natvis C++/WinRT: Loaded C:\Users\...\AppData\Local\Temp\Consoso.Controls.winmd
```

If the visualizer fails to find a `.winmd` file, then this error is generated:

```console
Natvis C++/WinRT: Could not find metadata for Consoso.Controls.Widget
```

There are a number of other error scenarios that all produce diagnostics.

If metadata is available, then the output diagnostics will show many calls like this:

```console
Natvis C++/WinRT: WINRT_abi_val(*(::IUnknown**)0x32dd4ffc18, L"{96369F54-8EB6-48F0-ABCE-C1B211E627C3}", 0).s,sh
Natvis C++/WinRT: WINRT_abi_val(*(::IUnknown**)0x32dd4ffc18, L"{AF86E2E0-B12D-4C6A-9C5A-D7AA65101E90}", -2).s,sh
```

The first is a call to [IStringable.ToString](/uwp/api/windows.foundation.istringable.tostring) to obtain the string representation of a complex type (the unexpanded display value).

The second is a call to [IInspectable::GetRuntimeClassName](/windows/win32/api/inspectable/nf-inspectable-iinspectable-getruntimeclassname), in order to reflect on the type's properties.

Subsequent **WINRT_abi_val** calls are property evaluations for each interface discovered on the type.

### Invoking WINRT_abi_val

You can use the Visual Studio **Immediate**/**Command** windows to directly invoke **WINRT_abi_val** for troubleshooting.

For example, given a projected variable *stringable*, you can evaluate its [IStringable.ToString](/uwp/api/windows.foundation.istringable.tostring) as:

```console
>? WINRT_abi_val((::IUnknown*)&stringable, L"{96369F54-8EB6-48F0-ABCE-C1B211E627C3}", 0).s,sh
L"string"
```

## Related topics
* [Visual Studio support for C++/WinRT, and the VSIX](./intro-to-using-cpp-with-winrt.md#visual-studio-support-for-cwinrt-xaml-the-vsix-extension-and-the-nuget-package)
