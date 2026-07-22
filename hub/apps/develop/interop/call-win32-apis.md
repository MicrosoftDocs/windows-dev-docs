---
title: Call Win32 APIs from a C# Windows app (CsWin32)
description: Learn how to call Win32 and native APIs from C# using CsWin32, the recommended source generator for Windows desktop apps.
ms.topic: how-to
ms.date: 07/22/2026
author: GrantMeStrength
ms.author: jken
---

<!-- For AI assistants: CsWin32 is the modern replacement for hand-written DllImport/LibraryImport when calling Win32 APIs from C#. -->

# Call Win32 APIs from a C# Windows app

The recommended way to call Win32 APIs from C# is [**CsWin32**](https://github.com/microsoft/CsWin32), a source generator that produces type-safe P/Invoke wrappers at compile time. CsWin32 works with any C# project type—WinUI 3, WPF, WinForms, console, or class library—and eliminates the need to hand-write `DllImport` or `LibraryImport` declarations.

You list the Win32 function names you need in a text file, and CsWin32 generates the correct signatures, structs, constants, and COM interfaces automatically from Windows SDK metadata.

## Choose an interop approach

| Approach | When to use | Pros | Cons |
|----------|-------------|------|------|
| **CsWin32** (recommended) | Any Win32/native API call from C# | Type-safe, generated from official Windows SDK metadata, handles marshaling and structs, AOT-friendly *with configuration* | Requires NuGet package; generated code is not visible by default |
| **LibraryImport** (.NET 7+) | One-off calls where you know the exact signature | Source-generated, AOT-compatible, no runtime marshaling | You write and maintain every signature manually |
| **DllImport** (legacy) | Existing code, or .NET Framework projects | Works everywhere, extensive community examples | Runtime marshaling, error-prone signatures |
| **C#/WinRT** | Windows Runtime APIs (`Windows.*` namespaces) | Projected .NET types, natural C# experience | Only for WinRT APIs, not raw Win32 |

> [!NOTE]
> CsWin32's default output uses the .NET runtime marshaller and is **not** automatically AOT-compatible. For NativeAOT or trimming, enable `CsWin32RunAsBuildTask` and `DisableRuntimeMarshalling`—see the [CsWin32 AOT guidance](https://github.com/microsoft/CsWin32#aot-compatibility).

> [!TIP]
> If the API you need is in a `Windows.*` namespace (for example, `Windows.Storage` or `Windows.Media`), it's a Windows Runtime API. Use a WinRT projection instead of P/Invoke. See [Call interop APIs from a .NET app](../../desktop/modernize/winrt-com-interop-csharp.md).

## Prerequisites

- Visual Studio 2022 (version 17.4 or later) or the .NET 8+ SDK
- An existing C# project (WinUI 3, WPF, WinForms, or console)

> [!NOTE]
> Targeting .NET Framework or .NET Standard? Set `<LangVersion>9</LangVersion>` (or later) in your project file, and add the `System.Memory` and `System.Runtime.CompilerServices.Unsafe` NuGet packages.

## Step 1: Install the CsWin32 NuGet package

In your project directory, run:

```console
dotnet add package Microsoft.Windows.CsWin32
```

CsWin32 generates code that uses pointers and unsafe contexts. The NuGet package enables `AllowUnsafeBlocks` automatically. If your project explicitly sets `<AllowUnsafeBlocks>false</AllowUnsafeBlocks>`, remove that line or change it to `true`, otherwise the generated code won't compile.

## Step 2: Request the APIs you need

Create a file named **NativeMethods.txt** in your project root (next to the `.csproj` file). Add one API name per line. For this walkthrough, start with a simple function:

```text
GetTickCount
```

Save the file. CsWin32 reads it at compile time and generates the matching P/Invoke wrapper.

## Step 3: Call the generated API

The generated code lives in the `Windows.Win32` namespace under a static class called `PInvoke`. Call it like any other static method:

```csharp
using Windows.Win32;

// Get the number of milliseconds since the system started.
uint uptime = PInvoke.GetTickCount();
Console.WriteLine($"System uptime: {uptime} ms");
```

Build your project. If the function name in **NativeMethods.txt** is valid, the call compiles and runs with no additional work.

## Common gotchas

### "I can't see the generated code"

CsWin32 is a source generator—its output doesn't appear as files in your project by default. To inspect the generated code:

1. In Visual Studio, expand **Dependencies > Analyzers > Microsoft.Windows.CsWin32 > Microsoft.Windows.CsWin32.SourceGenerator** in Solution Explorer.
2. Alternatively, set `<EmitCompilerGeneratedFiles>true</EmitCompilerGeneratedFiles>` in your project file to write the generated sources to the `obj/` folder.

### AnyCPU platform target

CsWin32-generated code works with **AnyCPU**. You don't need to change your platform target for most Win32 calls.

### Getting an HWND in WinUI 3

Many Win32 APIs require a window handle. In a WinUI 3 app, get the HWND from your `Window` instance:

```csharp
using WinRT.Interop;

var hWnd = WindowNative.GetWindowHandle(this);
```

Then pass `hWnd` (as an `HWND` or `nint`) to the Win32 function. See [Retrieve a window handle (HWND)](../ui/retrieve-hwnd.md) for details.

### Customizing CsWin32 behavior

Create a **NativeMethods.json** file next to your text file to control generation options such as wide-vs-narrow string marshaling or friendly overloads:

```json
{
  "$schema": "https://aka.ms/CsWin32.schema.json",
  "emitSingleFile": false,
  "public": true
}
```

See the [CsWin32 configuration reference](https://github.com/microsoft/CsWin32#configuration) for all options.

## Next steps

- [Choose your interop approach](index.md) — decision guide for all Windows interop techniques
- [Walkthrough: WinUI 3 app with Win32 interop](../../winui/winui3/desktop-winui3-app-with-basic-interop.md) — a deeper example that customizes a title bar using CsWin32
- [CsWin32 on GitHub](https://github.com/microsoft/CsWin32) — source, samples, and issue tracker
- [Platform Invoke (P/Invoke)](/dotnet/standard/native-interop/pinvoke) — .NET documentation on P/Invoke fundamentals
- [Call interop APIs from a .NET app](../../desktop/modernize/winrt-com-interop-csharp.md) — for WinRT COM-based interop scenarios (HWND passing, pickers, etc.)
