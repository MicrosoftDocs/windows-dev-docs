---
description: Learn more about compiling UWP apps with .NET Native.
title: Compiling Apps with .NET Native
ms.date: 04/29/2021
ms.topic: conceptual
helpviewer_keywords:
  - ".NET and native code"
  - "compilation with .NET Native"
  - ".NET Native"
ms.assetid: 47cd5648-9469-4b1d-804c-43cc04384045
---
# Compile apps with .NET Native

.NET Native is a precompilation technology for building and deploying UWP apps. .NET Native is included with Visual Studio 2015 and later versions. It automatically compiles the release version of UWP apps that are written in managed code (C# or Visual Basic) to native code.

Typically, .NET apps are compiled to intermediate language (IL). At run time, the just-in-time (JIT) compiler translates the IL to native code. In contrast, .NET Native compiles UWP apps directly to native code. For developers, this means:

- Your apps feature the performance of native code. Usually, performance will be superior to code that is first compiled to IL and then compiled to native code by the JIT compiler.

- You can continue to program in C# or Visual Basic.

- You can continue to take advantage of the resources provided by .NET Framework, including its class library, automatic memory management and garbage collection, and exception handling.

For users of your apps, .NET Native offers these advantages:

- Faster execution times for the majority of apps and scenarios.

- Faster startup times for the majority of apps and scenarios.

- Low deployment and update costs.

- Optimized app memory usage.

But .NET Native involves more than a compilation to native code. It transforms the way that .NET Framework apps are built and executed. In particular:

- During precompilation, required portions of .NET Framework are statically linked into your app. This allows the app to run with app-local libraries of .NET Framework, and the compiler to perform global analysis to deliver performance wins. As a result, apps launch consistently faster even after .NET Framework updates.

- The .NET Native runtime is optimized for static precompilation and in the vast majority of cases offers superior performance. At the same time, it retains the core reflection features that developers find so productive.

- .NET Native uses the same back end as the C++ compiler, which is optimized for static precompilation scenarios.

.NET Native is able to bring the performance benefits of C++ to managed code developers because it uses the same or similar tools as C++ under the hood, as shown in this table.

| Component | .NET Native                      | C++                         |
|-----------|----------------------------------|-----------------------------|
| Libraries | .NET Framework + Windows Runtime | Win32 + Windows Runtime     |
| Compiler  | UTC optimizing compiler          | UTC optimizing compiler     |
| Deployed  | Ready-to-run binaries            | Ready-to-run binaries (ASM) |
| Runtime   | MRT.dll (Minimal CLR Runtime)    | CRT.dll (C Runtime)         |

For UWP apps, you upload .NET Native Code Compilation binaries in app packages (.msix or .appx files) to the Microsoft Store.
