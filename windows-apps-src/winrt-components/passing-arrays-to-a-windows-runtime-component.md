---
title: Passing arrays to a Windows Runtime Component
description: In the Windows Universal Platform (UWP), parameters are either for input or for output, never both. This means that the contents of an array that is passed to a method, as well as the array itself, are either for input or for output.
ms.assetid: 8DE695AC-CEF2-438C-8F94-FB783EE18EB9
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Passing arrays to a Windows Runtime Component




In the Windows Universal Platform (UWP), parameters are either for input or for output, never both. This means that the contents of an array that is passed to a method, as well as the array itself, are either for input or for output. If the contents of the array are for input, the method reads from the array but doesn't write to it. If the contents of the array are for output, the method writes to the array but doesn't read from it. This presents a problem for array parameters, because arrays in .NET are reference types, and the contents of an array are mutable even when the array reference is passed by value (**ByVal** in Visual Basic). The [Windows Runtime Metadata Export Tool (Winmdexp.exe)](/dotnet/framework/tools/winmdexp-exe-windows-runtime-metadata-export-tool) requires you to specify the intended usage of the array if it is not clear from context, by applying the ReadOnlyArrayAttribute attribute or the WriteOnlyArrayAttribute attribute to the parameter. Array usage is determined as follows:

-   For the return value or for an out parameter (a **ByRef** parameter with the [OutAttribute](/dotnet/api/system.runtime.interopservices.outattribute) attribute in Visual Basic) the array is always for output only. Do not apply the ReadOnlyArrayAttribute attribute. The WriteOnlyArrayAttribute attribute is allowed on output parameters, but it's redundant.

    > **Caution**  The Visual Basic compiler does not enforce output-only rules. You should never read from an output parameter; it may contain **Nothing**. Always assign a new array.
 
-   Parameters that have the **ref** modifier (**ByRef** in Visual Basic) are not allowed. Winmdexp.exe generates an error.
-   For a parameter that is passed by value, you must specify whether the array contents are for input or output by applying either the [ReadOnlyArrayAttribute](/dotnet/api/system.runtime.interopservices.windowsruntime.readonlyarrayattribute) attribute or the [WriteOnlyArrayAttribute](/dotnet/api/system.runtime.interopservices.windowsruntime.writeonlyarrayattribute) attribute. Specifying both attributes is an error.

If a method must accept an array for input, modify the array contents, and return the array to the caller, use a read-only parameter for the input and a write-only parameter (or the return value) for the output. The following code shows one way to implement this pattern:

> [!div class="tabbedCodeSnippets"]
> ```csharp
> public int[] ChangeArray([ReadOnlyArray()] int[] input)
> {
>     int[] output = input.Clone();
>     // Manipulate the copy.
>     //   ...
>     return output;
> }
> ```
> ```vb
> Public Function ChangeArray(<ReadOnlyArray> input() As Integer) As Integer()
>     Dim output() As Integer = input.Clone()
>     ' Manipulate the copy.
>     '   ...
>     Return output
> End Function
> ```

We recommend that you make a copy of the input array immediately, and manipulate the copy. This helps ensure that the method behaves the same whether or not your component is called by .NET code.

## Using components from managed and unmanaged code


Parameters that have the ReadOnlyArrayAttribute attribute or the WriteOnlyArrayAttribute attribute behave differently depending on whether the caller is written in native code or managed code. If the caller is native code (JavaScript or Visual C++ component extensions), the array contents are treated as follows:

-   ReadOnlyArrayAttribute: The array is copied when the call crosses the application binary interface (ABI) boundary. Elements are converted if necessary. Therefore, any accidental changes the method makes to an input-only array are not visible to the caller.
-   WriteOnlyArrayAttribute: The called method can't make any assumptions about the contents of the original array. For example, the array the method receives might not be initialized, or might contain default values. The method is expected to set the values of all the elements in the array.

If the caller is managed code, the original array is available to the called method, as it would be in any method call in .NET. Array contents are mutable in .NET code, so any changes the method makes to the array are visible to the caller. This is important to remember because it affects unit tests written for a Windows Runtime Component. If the tests are written in managed code, the contents of an array will appear to be mutable during testing.

## Related topics

* [ReadOnlyArrayAttribute](/dotnet/api/system.runtime.interopservices.windowsruntime.readonlyarrayattribute)
* [WriteOnlyArrayAttribute](/dotnet/api/system.runtime.interopservices.windowsruntime.writeonlyarrayattribute)
* [Windows Runtime components with C# and Visual Basic](creating-windows-runtime-components-in-csharp-and-visual-basic.md)