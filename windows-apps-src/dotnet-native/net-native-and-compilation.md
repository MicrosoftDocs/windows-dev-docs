---
description: Learn more about: ".NET Native and Compilation"
title: ".NET Native and Compilation"
ms.date: 03/30/2017
ms.topic: conceptual
ms.assetid: e38ae4f3-3e3d-42c3-a4b8-db1aa9d84f85
---

# .NET Native and compilation

Windows desktop applications that target .NET Framework are written in a particular programming language and compiled into intermediate language (IL). At run time, a just-in-time (JIT) compiler is responsible for compiling the IL into native code for the local machine just before a method is executed for the first time. In contrast, the .NET Native tool chain converts source code to native code at compile time. This article compares .NET Native with other compilation technologies available for .NET Framework apps, and also provides a practical overview of how .NET Native produces native code that can help you understand why exceptions that occur in code compiled with .NET Native do not occur in JIT-compiled code.

## Generating native binaries

An application that targets .NET Framework and that is not compiled by using the .NET Native tool chain consists of the application assembly, which includes the following:

- [Metadata](/dotnet/standard/metadata-and-self-describing-components) that describes the assembly, its dependencies, the types it contains, and their members. The metadata is used for reflection and late-bound access, and in some cases by compiler and build tools as well.

- Implementation code. This consists of intermediate language (IL) opcodes. At runtime, the just-in-time (JIT) compiler translates it into native code for the target platform.

In addition to the main application assembly, an app requires that the following be present:

- Any additional class libraries or third-party assemblies that are required by your app. These assemblies similarly include metadata that describes the assembly, its types, and their members, as well as the IL that implements all type members.

- The .NET Framework Class Library. This is a collection of assemblies that is installed on the local system with the .NET Framework installation. The assemblies included in the .NET Framework Class Library include a complete set of metadata and implementation code.

- The common language runtime. This is a collection of dynamic link libraries that perform such services as assembly loading, memory management and garbage collection, exception handling, just-in-time compilation, remoting, and interop. Like the class library, the runtime is installed on the local system as part of the .NET Framework installation.

Note that the entire common language runtime, as well as metadata and IL for all types in application-specific assemblies, third-party assemblies, and system assemblies must be present for the app to execute successfully.

## Just-in-time compilation

The input for the .NET Native tool chain is the UWP app built by the C# or Visual Basic compiler. In other words, the .NET Native tool chain begins execution when the language compiler has finished compilation of a UWP app.

> [!TIP]
> Because the input to .NET Native is the IL and metadata written to managed assemblies, you can still perform custom code generation or other custom operations by using pre-build or post-build events or by modifying the MSBuild project file.
>
> However, categories of tools that modify IL and thereby prevent the .NET tool chain from analyzing an app's IL are not supported. Obfuscators are the most notable tools of this type.

In the course of converting an app from IL to native code, the .NET Native tool chain performs operations like the following:

- For certain code paths, it replaces code that relies on reflection and metadata with static native code. For example, if a value type does not override the <xref:System.ValueType.Equals%2A?displayProperty=nameWithType> method, the default test for equality uses reflection to retrieve <xref:System.Reflection.FieldInfo> objects that represent the value type's fields, then compares the field values of two instances. When compiling to native code, the .NET Native tool chain replaces the reflection code and metadata with a static comparison of the field values.

- Where possible, it attempts to eliminate all metadata.

- It includes in the final app assemblies only the implementation code that is actually invoked by the app. This particularly affects code in third-party libraries and in the .NET Framework Class Library. As a result, an application no longer depends on either third-party libraries or the full .NET Framework Class Library; instead, code in third-party and .NET Framework class libraries is now local to the app.

- It replaces the full CLR with a refactored runtime that primarily contains the garbage collector. The refactored runtime is found in a library named mrt100_app.dll that is local to the app and is only a few hundred kilobytes in size. This is possible because static linking eliminates the need for many of the services performed by the common language runtime.

  > [!NOTE]
  > .NET Native uses the same garbage collector as the standard common language runtime. In the .NET Native garbage collector, background garbage collection is enabled by default. For more information about garbage collection, see [Fundamentals of Garbage Collection](/dotnet/standard/garbage-collection/fundamentals).

> [!IMPORTANT]
> .NET Native compiles an entire application to a native application. It does not allow you to compile a single assembly that contains a class library to native code so that it can be called independently from managed code.

The resulting app that is produced by the .NET Native tool chain is written to a directory named ilc.out in the Debug or Release directory of your project directory. It consists of the following files:

- *\<appName>*.exe, a stub executable that simply transfers control to a special `Main` export in *\<appName>*.dll.

- *\<appName>*.dll, a Windows dynamic link library that contains all your application code, as well as code from the .NET Framework Class Library and any third-party libraries that you have a dependency on.  It also contains support code, such as the code necessary to interoperate with Windows and to serialize objects in your app.

- mrt100_app.dll, a refactored runtime that provides runtime services such as garbage collection.

All dependencies are captured by the app's APPX manifest.  In addition to the application exe, dll, and mrt100_app.dll, which are bundled directly in the appx package, this includes two more files:

- msvcr140_app.dll, the C run-time (CRT) library used by mrt100_app.dll. It is included by a framework reference in the package.

- mrt100.dll. This library includes functions that can improve the performance of mrt100_app.dll, although its absence does not prevent mrt100_app.dll from functioning. It is loaded from the system32 directory on the local machine, if it is present.

Because the .NET Native tool chain links implementation code into your app only if it knows that your app actually invokes that code, either the metadata or the implementation code required in the following scenarios may not be included with your app:

- Reflection.

- Dynamic or late-bound invocation.

- Serialization and deserialization.

- COM interop.

If the necessary metadata or implementation code is absent at runtime, the .NET Native runtime throws an exception. You can prevent these exceptions, and ensure that the .NET Native tool chain includes the required metadata and implementation code, by using a [runtime directives file](runtime-directives-rd-xml-configuration-file-reference.md), an XML file that designates the program elements whose metadata or implementation code must be available at runtime and assigns a runtime policy to them. The following is the default runtime directives file that's added to a UWP project that's compiled by the .NET Native tool chain:

```xml
<Directives xmlns="http://schemas.microsoft.com/netfx/2013/01/metadata">
  <Application>
    <Assembly Name="*Application*" Dynamic="Required All" />
  </Application>
</Directives>
```

This enables all the types, as well as all their members, in all the assemblies in your app package for reflection and dynamic invocation. However, it does not enable reflection or dynamic activation of types in .NET Framework Class Library assemblies. In many cases, this is adequate.

## See also

- [Metadata and self-describing components](/dotnet/standard/metadata-and-self-describing-components)
- [Reflection and .NET Native](reflection-and-net-native.md)
- [.NET Native general troubleshooting](net-native-general-troubleshooting.md)
