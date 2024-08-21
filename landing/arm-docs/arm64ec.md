---
title: Arm64EC for Windows 11 apps on Arm
description: Learn how Arm64EC empowers you to build and incrementally update apps that benefit from native performance on Arm devices, without interrupting your current x64 functionality.
ms.date: 08/08/2022
ms.topic: overview
ms.service: windows
ms.subservice: arm
---

# Arm64EC - Build and port apps for native performance on Arm

Arm64EC ("Emulation Compatible") enables you to build new native apps or incrementally transition existing x64 apps to take advantage of the native speed and performance possible with Arm-powered devices, including better power consumption, battery life, and accelerated AI & ML workloads.

Arm64EC is a new application binary interface (ABI) for apps running on Arm devices with Windows 11. It is a Windows 11 feature that requires the use of the Windows 11 SDK and is not available on Windows 10 on Arm.

## Interoperability

Code built as Arm64EC is interoperable with x64 code running under emulation within the same process.  The Arm64EC code in the process runs with native performance, while any x64 code runs using emulation that comes built-in with Windows 11. Even if your app relies on existing dependencies or plugins that don't yet support Arm, you can start to rebuild parts of your app as Arm64EC to gain the benefits of native performance.

Arm64EC guarantees interoperability with x64 by following x64 software conventions including calling convention, stack usage, data structure layout, and preprocessor definitions. However, Arm64EC code is not compatible with code built as Arm64, which uses a different set of software conventions.

The Windows 11 on Arm operating system itself relies heavily on Arm64EC's interoperability to enable running x64 applications.  Most operating system code loaded by an x64 app running on Windows 11 on Arm will have been compiled as Arm64EC, enabling native performance for that code without the application knowing. 

An x64 or Arm64EC process can load and call into both x64 and Arm64EC binaries, whereas an Arm64 process can only load Arm64 binaries.  Both architectures can load [Arm64X binaries](./arm64x-pe.md) as those contain code for both x64 and Arm64.

|Process architecture |x64 binary |Arm64EC binary |Arm64 binary |
|---|---|---|---|
|**x64/Arm64EC** |✔ |✔ |❌ |
|**Arm64** |❌ |❌ |✔ |

✔ = Supported,
❌ = Not supported

Similarly, at build time, Arm64EC binaries can link in both x64 and Arm64EC libs, while Arm64 binaries can only link in Arm64 libs. 

|PE architecture |x64 lib |Arm64EC lib |Arm64 lib |
|---|---|---|---|
|**Arm64EC** |✔ |✔ |❌ |
|**Arm64** |❌ |❌ |✔ |

✔ = Supported,
❌ = Not supported

For more detail about how the Arm64EC ABI enables interoperability, see [Understanding Arm64EC ABI and assembly code](./arm64ec-abi.md).

## Use Arm64EC to make an existing app faster on Windows 11 on Arm

Arm64EC enables to you to **incrementally** transition the code in your existing app from emulated to native. At each step along the way, your application continues to run well without the need to be recompiled all at once.

![Example graph showing incremental update effects on Arm performance using Arm64EC](./images/arm64ec-incremental-update.png)

The image above shows a simplified example of a fully-emulated x64 workload taking some amount of time that is then incrementally improved using Arm64EC:

1. Starting as a fully emulated x64 workload
2. After recompiling the most CPU-intensive parts as Arm64EC
3. After continuing to recompile more x64 modules over time
4. Ending result of a fully native Arm64EC app

By recompiling the modules that take the most time or are the most CPU-intensive from x64 to Arm64EC, the resulting workload receives the most improvement for the least amount of effort each step of the way.

## App dependencies

When using Arm64EC to rebuild an application, you will want to use Arm64EC versions of dependencies but you can also rely on x64 versions of dependencies. Arm64 versions of dependencies won't be usable.

Any x64 code, including code from dependencies, in an Arm64EC process will run under emulation in your app. Prioritizing the most CPU-intensive dependencies to transition from x64 to Arm64EC will have the greatest impact toward improving your app's performance.

## Identifying Arm64EC binaries and apps

Apps running on Windows 11 on Arm will interact with Arm64EC binaries as though they are x64 binaries. The app does not need to know to what extent the code in the binary has been recompiled as Arm64EC.  

For developers interested in identifying these binaries, you can see them in a developer command prompt using `link /dump /headers`.

```powershell
File Type: EXECUTABLE IMAGE
FILE HEADER VALUES
    8664 machine (x64) (ARM64X)
```

The combination of (x64) and (ARM64X) indicates that some portion of the binary has been recompiled as Arm64EC, even though the binary still appears to be x64. A binary with a machine header that contains (ARM64) and (ARM64X) is an [Arm64X PE file](./arm64x-pe.md) that can be loaded into both x64 and Arm64 apps.

Windows **Task Manager** can also be used to identify if an app has been compiled as Arm64EC. In the **Details** tab of Task manager, the **Architecture** column will show **ARM64 (x64 compatible)** for applications whose main executable has been partially or completely compiled as Arm64EC.

![Screenshot of Task Manager showing ARM64 (x64 compatible) in Architecture details.](./images/arm64ec-task-manager.png)

## Next steps

See [Get started with Arm64EC](./arm64ec-build.md) to learn how to build or update Win32 apps using Arm64EC.
