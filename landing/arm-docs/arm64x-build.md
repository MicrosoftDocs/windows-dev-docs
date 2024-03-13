---
title: Build Arm64X Files
description: Build Arm64X files for situations where one file is loaded into both x64/Arm64EC and Arm64 processes.
ms.date: 08/08/2022
ms.topic: article
ms.service: windows
ms.subservice: arm
author: marswe
ms.author: marcs
---

# Build Arm64X binaries

You can build Arm64X binaries, also known as [Arm64X PE files](./arm64x-pe.md), to support loading a single binary into both x64/Arm64EC and Arm64 processes.  

## Building an Arm64X binary from a Visual Studio project

To enable building Arm64X binaries, the property pages of Arm64EC configuration has a new "Build Project as ARM64X" property, known as `BuildAsX` in the project file.

![Property page for an Arm64EC configuration showing the Build Project as ARM64X option](./images/arm64x-build.png)

When a user builds a project, Visual Studio would normally compile for Arm64EC and then link the outputs into an Arm64EC binary. When `BuildAsX` is set to `true`, Visual Studio will instead compile for both Arm64EC **and** Arm64. The Arm64EC link step is then used to link both together into a single Arm64X binary. The output directory for this Arm64X binary will be whatever the output directory is set to under the [Arm64EC configuration](./arm64ec-build.md).

For `BuildAsX` to work correctly, the user must have an existing Arm64 configuration, in addition to the Arm64EC configuration. The Arm64 and Arm64EC configurations must have the same C runtime and C++ standard library (e.g., both set [/MT](/cpp/c-runtime-library/crt-library-features)). To avoid build inefficiencies, such as building full Arm64 projects rather than just compilation, all direct and indirect references of the project should have `BuildAsX` set to true.

The build system assumes that the Arm64 and Arm64EC configurations have the same name. If the Arm64 and Arm64EC configurations have different names (such as `Debug|ARM64` and `MyDebug|ARM64EC`), you can manually edit the [vcxproj](/cpp/build/reference/vcxproj-file-structure) or `Directory.Build.props` file to add an `ARM64ConfigurationNameForX` property to the Arm64EC configuration that provides the name of the Arm64 configuration.

If the desired Arm64X binary is a combination of two separate projects, one as Arm64 and one as Arm64EC, you can manually edit the vxcproj of the Arm64EC project to add an `ARM64ProjectForX` property and specify the path to the Arm64 project. The two projects must be in the same solution.

## Building an Arm64X pure forwarder DLL

An **Arm64X pure forwarder DLL** is a small Arm64X DLL that forwards APIs to separate DLLs depending on their type:

- Arm64 APIs are forwarded to an Arm64 DLL, and
- x64 APIs are forwarded to an x64 or Arm64EC DLL.

An Arm64X pure forwarder enables the advantages of using an Arm64X binary even if there are challenges with building a merged Arm64X binary containing all of the Arm64EC and Arm64 code. Learn more about Arm64X pure forwarder DLLs in the [Arm64X PE files](./arm64x-pe.md) overview page.

You can build an Arm64X pure forwarder from the Arm64 developer command prompt following the steps below. The resulting Arm64X pure forwarder will route x64 calls to `foo_x64.DLL` and Arm64 calls to `foo_arm64.DLL`.

1. Create empty `OBJ` files that will later be used by the linker to create the pure forwarder. These are empty as the pure forwarder has no code in it. To do this, create an empty file. For the example below, we named the file **empty.cpp**. Empty `OBJ` files are then created using `cl`, with one for Arm64 (`empty_arm64.obj`) and one for Arm64EC (`empty_x64.obj`):

    ```cpp
    cl /c /Foempty_arm64.obj empty.cpp
    cl /c /arm64EC /Foempty_x64.obj empty.cpp
    ```
   > If the error message "cl : Command line warning D9002 : ignoring unknown option '-arm64EC'" appears, the incorrect compiler is being used. To resolve that please switch to [the ARM64 Developer Command Prompt](https://devblogs.microsoft.com/cppblog/arm64ec-support-in-visual-studio/#developer-command-prompt).

2. Create `DEF` files for both x64 and Arm64. These files enumerate all of the API exports of the DLL and points the loader to the name of the DLL that can fulfill those API calls.

    `foo_x64.def`:

    ```cpp
    EXPORTS
        MyAPI1  =  foo_x64.MyAPI1
        MyAPI2  =  foo_x64.MyAPI2
    ```

    `foo_arm64.def`:

    ```cpp
    EXPORTS
        MyAPI1  =  foo_arm64.MyAPI1
        MyAPI2  =  foo_arm64.MyAPI2
    ```

3. You can then use `link` to create `LIB` import files for both x64 and Arm64:

    ```cpp
    link /lib /machine:x64 /def:foo_x64.def /out:foo_x64.lib
    link /lib /machine:arm64 /def:foo_arm64.def /out:foo_arm64.lib
    ```

4. Link the empty `OBJ` and import `LIB` files using the flag `/MACHINE:ARM64X` to produce the Arm6X pure forwarder DLL:

    ```cpp
    link /dll /noentry /machine:arm64x /defArm64Native:foo_arm64.def /def:foo_x64.def empty_arm64.obj empty_x64.obj /out:foo.dll foo_arm64.lib foo_x64.lib
    ```

The resulting `foo.dll` can be loaded into either an Arm64 or an x64/Arm64EC process. When an Arm64 process loads `foo.dll`, the operating system will immediately load `foo_arm64.dll` in its place and any API calls will be handled by `foo_arm64.dll`.
