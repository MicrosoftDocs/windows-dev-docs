---
title: Diagnosing Windows Runtime Component error conditions
description: This article provides additional information about restrictions on Windows Runtime components written with managed code.
ms.assetid: CD0D0E11-E68A-411D-B92E-E9DECFDC9599
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Diagnosing Windows Runtime Component error conditions

This article provides additional information about restrictions on Windows Runtime components written with managed code. It expands on the information that is provided in error messages from [Winmdexp.exe (Windows Runtime Metadata Export Tool)](/dotnet/framework/tools/winmdexp-exe-windows-runtime-metadata-export-tool), and complements the information on restrictions that is provided in [Windows Runtime components with C# and Visual Basic](creating-windows-runtime-components-in-csharp-and-visual-basic.md).

This article doesn’t cover all errors. The errors discussed here are grouped by general category, and each category includes a table of associated error messages. Search for message text (omitting specific values for placeholders) or for message number. If you don’t find the information you need here, please help us improve the documentation by using the feedback button at the end of this article. Include the error message. Alternatively, you can file a bug at the Microsoft Connect website.

## Error message for implementing async interface provides incorrect type

Managed Windows Runtime components cannot implement the Universal Windows Platform (UWP) interfaces that represent asynchronous actions or operations ([IAsyncAction](/windows/desktop/api/windows.foundation/nn-windows-foundation-iasyncaction), [IAsyncActionWithProgress&lt;TProgress&gt;](/previous-versions/br205784(v=vs.85)), [IAsyncOperation&lt;TResult&gt;](/uwp/api/Windows.Foundation.IAsyncOperation_TResult_), or [IAsyncOperationWithProgress&lt;TResult, TProgress&gt;](/uwp/api/Windows.Foundation.IAsyncOperationWithProgress_TResult_TProgress_)). Instead, .NET provides the [AsyncInfo](/dotnet/api/system.runtime.interopservices.windowsruntime) class for generating async operations in Windows Runtime components. The error message that Winmdexp.exe displays when you try to implement an async interface incorrectly refers to this class by its former name, AsyncInfoFactory. .NET no longer includes the AsyncInfoFactory class.

| Error number | Message Text|       
|--------------|-------------|
| WME1084      | Type '{0}' implements Windows Runtime async interface '{1}'. Windows Runtime types cannot implement async interfaces. Please use the System.Runtime.InteropServices.WindowsRuntime.AsyncInfoFactory class to generate async operations for export to Windows Runtime. |

> **Note** The error messages that refer to the Windows Runtime use an old terminology. This is now referred to as the Universal Windows Platform (UWP). For example, Windows Runtime types are now called UWP types.

## Missing references to mscorlib.dll or System.Runtime.dll

This issue occurs only when you use Winmdexp.exe from the command line. We recommend that you use the /reference option to include references to both mscorlib.dll and System.Runtime.dll from the .NET Framework core reference assemblies, which are located in "%ProgramFiles(x86)%\\Reference Assemblies\\Microsoft\\Framework\\.NETCore\\v4.5" ("%ProgramFiles%\\..." on a 32-bit computer).

| Error number | Message Text                                                                                                                                     |
|--------------|--------------------------------------------------------------------------------------------------------------------------------------------------|
| WME1009      | No reference was made to mscorlib.dll. A reference to this metadata file is required in order to export correctly.                               |
| WME1090      | Could not determine the core reference assembly. Please make sure mscorlib.dll and System.Runtime.dll is referenced using the /reference switch. |

## Operator overloading is not allowed

In a Windows Runtime Component written in managed code, you cannot expose overloaded operators on public types.

> **Note** In the error message, the operator is identified by its metadata name, such as op\_Addition, op\_Multiply, op\_ExclusiveOr, op\_Implicit (implicit conversion), and so on.

| Error number | Message Text                                                                                          |
|--------------|-------------------------------------------------------------------------------------------------------|
| WME1087      | '{0}' is an operator overload. Managed types cannot expose operator overloads in the Windows Runtime. |

## Constructors on a class have the same number of parameters

In the UWP, a class can have only one constructor with a given number of parameters; for example, you can't have one constructor that has a single parameter of type **String** and another that has a single parameter of type **int** (**Integer** in Visual Basic). The only workaround is to use a different number of parameters for each constructor.

| Error number | Message Text                                                                                                                                            |
|--------------|---------------------------------------------------------------------------------------------------------------------------------------------------------|
| WME1099      | Type '{0}' has multiple constructors with '{1}' argument(s). Windows Runtime types cannot have multiple constructors with the same number of arguments. |

## Must specify a default for overloads that have the same number of parameters

In the UWP, overloaded methods can have the same number of parameters only if one overload is specified as the default overload. See "Overloaded Methods" in [Windows Runtime components with C# and Visual Basic](creating-windows-runtime-components-in-csharp-and-visual-basic.md).

| Error number | Message Text                                                                                                                                                                      |
|--------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| WME1059      | Multiple {0}-parameter overloads of '{1}.{2}' are decorated with Windows.Foundation.Metadata.DefaultOverloadAttribute.                                                            |
| WME1085      | The {0}-parameter overloads of {1}.{2} must have exactly one method specified as the default overload by decorating it with Windows.Foundation.Metadata.DefaultOverloadAttribute. |

## Namespace errors and invalid names for the output file

In the Universal Windows Platform, all the public types in a Windows metadata (.winmd) file must be in a namespace that shares the .winmd file name, or in sub-namespaces of the file name. For example, if your Visual Studio project is named A.B (that is, your Windows Runtime Component is A.B.winmd), it can contain public classes A.B.Class1 and A.B.C.Class2, but not A.Class3 (WME0006) or D.Class4 (WME1044).

> **Note**  These restrictions apply only to public types, not to private types used in your implementation.

In the case of A.Class3, you can either move Class3 to another namespace or change the name of the Windows Runtime Component to A.winmd. Although WME0006 is a warning, you should treat it as an error. In the previous example, code that calls A.B.winmd will be unable to locate A.Class3.

In the case of D.Class4, no file name can contain both D.Class4 and classes in the A.B namespace, so changing the name of the Windows Runtime Component is not an option. You can either move D.Class4 to another namespace, or put it in another Windows Runtime Component.

The file system can't distinguish between uppercase and lowercase, so namespaces that differ by case are not allowed (WME1067).

Your component must contain at least one **public sealed** type (**Public NotInheritable** in Visual Basic). If not, you will get WME1042 or WME1043, depending on whether your component contains private types.

A type in a Windows Runtime Component cannot have a name that is the same as a namespace (WME1068).

> **Caution**  If you call Winmdexp.exe directly and don't use the /out option to specify a name for your Windows Runtime Component, Winmdexp.exe tries to generate a name that includes all the namespaces in the component. Renaming namespaces can change the name of your component.

 

| Error number | Message Text                                                                                                                                                                                                                                                                                                                                             |
|--------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| WME0006      | '{0}' is not a valid winmd file name for this assembly. All types within a Windows Metadata file must exist in a sub namespace of the namespace that is implied by the file name. Types that do not exist in such a sub namespace cannot be located at runtime. In this assembly, the smallest common namespace that could serve as a filename is '{1}'. |
| WME1042      | Input module must contain at least one public type that is located inside a namespace.                                                                                                                                                                                                                                                                   |
| WME1043      | Input module must contain at least one public type that is located inside a namespace. The only types found inside namespaces are private.                                                                                                                                                                                                               |
| WME1044      | A public type has a namespace ('{1}') that shares no common prefix with other namespaces ('{0}'). All types within a Windows Metadata file must exist in a sub namespace of the namespace that is implied by the file name.                                                                                                                              |
| WME1067      | Namespace names cannot differ only by case: '{0}', '{1}'.                                                                                                                                                                                                                                                                                                |
| WME1068      | Type '{0}' cannot have the same name as namespace '{1}'.                                                                                                                                                                                                                                                                                                 |

## Exporting types that aren't valid Universal Windows Platform types

The public interface of your component must expose only UWP types. However, .NET provides mappings for a number of commonly used types that are slightly different in .NET and the UWP. This enables the .NET developer to work with familiar types instead of learning new ones. You can use these mapped .NET types in the public interface of your component. See "Declaring types in Windows Runtime components" and "Passing Universal Windows Platform types to managed code" in [Windows Runtime components with C# and Visual Basic](creating-windows-runtime-components-in-csharp-and-visual-basic.md), and [.NET mappings of Windows Runtime types](net-framework-mappings-of-windows-runtime-types.md).

Many of these mappings are interfaces. For example, [IList&lt;T&gt;](/dotnet/api/system.collections.generic.ilist-1) maps to the UWP interface [IVector&lt;T&gt;](/uwp/api/Windows.Foundation.Collections.IVector_T_). If you use List&lt;string&gt; (`List(Of String)` in Visual Basic) instead of IList&lt;string&gt; as a parameter type, Winmdexp.exe provides a list of alternatives that includes all the mapped interfaces implemented by List&lt;T&gt;. If you use nested generic types, such as List&lt;Dictionary&lt;int, string&gt;&gt; (List(Of Dictionary(Of Integer, String)) in Visual Basic), Winmdexp.exe offers choices for each level of nesting. These lists can become quite long.

In general, the best choice is the interface that is closest to the type. For example, for Dictionary&lt;int, string&gt;, the best choice is most likely IDictionary&lt;int, string&gt;.

> **Important**  JavaScript uses the interface that appears first in the list of interfaces that a managed type implements. For example, if you return Dictionary&lt;int, string&gt; to JavaScript code, it appears as IDictionary&lt;int, string&gt; no matter which interface you specify as the return type. This means that if the first interface doesn't include a member that appears on later interfaces, that member isn't visible to JavaScript.

> **Caution**  Avoid using the non-generic [IList](/dotnet/api/system.collections.ilist) and [IEnumerable](/dotnet/api/system.collections.ienumerable) interfaces if your component will be used by JavaScript. These interfaces map to [IBindableVector](/uwp/api/windows.ui.xaml.interop.ibindablevector) and [IBindableIterator](/uwp/api/windows.ui.xaml.interop.ibindableiterator), respectively. They support binding for XAML controls, and are invisible to JavaScript. JavaScript issues the run-time error "The function 'X' has an invalid signature and cannot be called."

 

<table>
<colgroup>
<col width="50%" />
<col width="50%" />
</colgroup>
<thead>
<tr class="header">
<th align="left">Error number</th>
<th align="left">Message Text</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td align="left">WME1033</td>
<td align="left">Method '{0}' has parameter '{1}' of type '{2}'. '{2}' is not a valid Windows Runtime parameter type.</td>
</tr>
<tr class="even">
<td align="left">WME1038</td>
<td align="left">Method '{0}' has a parameter of type '{1}' in its signature. Although this type is not a valid Windows Runtime type, it implements interfaces that are valid Windows Runtime types. Consider changing the method signature to use one of the following types instead: '{2}'.</td>
</tr>
<tr class="odd">
<td align="left">WME1039</td>
<td align="left"><p>Method '{0}' has a parameter of type '{1}' in its signature. Although this generic type is not a valid Windows Runtime type, the type or its generic parameters implement interfaces that are valid Windows Runtime types. {2}</p>
> **Note**  For {2}, Winmdexp.exe appends a list of alternatives, such as "Consider changing the type 'System.Collections.Generic.List&lt;T&gt;' in the method signature to one of the following types instead: 'System.Collections.Generic.IList&lt;T&gt;, System.Collections.Generic.IReadOnlyList&lt;T&gt;, System.Collections.Generic.IEnumerable&lt;T&gt;'."
</td>
</tr>
<tr class="even">
<td align="left">WME1040</td>
<td align="left">Method '{0}' has a parameter of type '{1}' in its signature. Instead of using a managed Task type, use Windows.Foundation.IAsyncAction, Windows.Foundation.IAsyncOperation, or one of the other Windows Runtime async interfaces. The standard .NET await pattern also applies to these interfaces. Please see System.Runtime.InteropServices.WindowsRuntime.AsyncInfo for more information about converting managed task objects to Windows Runtime async interfaces.</td>
</tr>
</tbody>
</table>

 

## Structures that contain fields of disallowed types


In the UWP, a structure can contain only fields, and only structures can contain fields. Those fields must be public. Valid field types include enumerations, structures, and primitive types.

| Error number | Message Text                                                                                                                                                                                                                                                            |
|--------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| WME1060      | Structure '{0}' has field '{1}' of type '{2}'. '{2}' is not a valid Windows Runtime field type. Each field in a Windows Runtime structure can only be UInt8, Int16, UInt16, Int32, UInt32, Int64, UInt64, Single, Double, Boolean, String, Enum, or itself a structure. |

 

## Restrictions on arrays in member signatures


In the UWP, arrays in member signatures must be one-dimensional with a lower bound of 0 (zero). Nested array types such as `myArray[][]` (`myArray()()` in Visual Basic) are not allowed.

> **Note** This restriction does not apply to arrays you use internally in your implementation.

 

| Error number | Message Text                                                                                                                                                     |
|--------------|--------------------|
| WME1034      | Method '{0}' has an array of type '{1}' with non-zero lower bound in its signature. Arrays in Windows Runtime method signatures must have a lower bound of zero. |
| WME1035      | Method '{0}' has a multi-dimensional array of type '{1}' in its signature. Arrays in Windows Runtime method signatures must be one dimensional.                  |
| WME1036      | Method '{0}' has a nested array of type '{1}' in its signature. Arrays in Windows Runtime method signatures cannot be nested.                                    |

 

## Array parameters must specify whether array contents are readable or writable


In the UWP, parameters must be read-only or write-only. Parameters cannot be marked **ref** (**ByRef** without the [OutAttribute](/dotnet/api/system.runtime.interopservices.outattribute) attribute in Visual Basic). This applies to the contents of arrays, so array parameters must indicate whether the array contents are read-only or write-only. The direction is clear for **out** parameters (**ByRef** parameter with the OutAttribute attribute in Visual Basic), but array parameters that are passed by value (ByVal in Visual Basic) must be marked. See [Passing arrays to a Windows Runtime Component](passing-arrays-to-a-windows-runtime-component.md).

| Error number | Message Text         |
|--------------|----------------------|
| WME1101      | Method '{0}' has parameter '{1}' which is an array, and which has both {2} and {3}. In the Windows Runtime, the contents array parameters must be either readable or writable. Please remove one of the attributes from '{1}'.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     |
| WME1102      | Method '{0}' has an output parameter '{1}' which is an array, but which has {2}. In the Windows Runtime, the contents of output arrays are writable. Please remove the attribute from '{1}'.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       |
| WME1103      | Method '{0}' has parameter '{1}' which is an array, and which has either a System.Runtime.InteropServices.InAttribute or a System.Runtime.InteropServices.OutAttribute. In the Windows Runtime, array parameters must have either {2} or {3}. Please remove these attributes or replace them with the appropriate Windows Runtime attribute if necessary.                                                                                                                                                                                                                                                                                                                                                                                          |
| WME1104      | Method '{0}' has parameter '{1}' which is not an array, and which has either a {2} or a {3}. Windows Runtime does not support marking non-array parameters with {2} or {3}.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        |
| WME1105      | Method '{0}' has parameter '{1}' with a System.Runtime.InteropServices.InAttribute or System.Runtime.InteropServices.OutAttribute. Windows Runtime does not support marking parameters with System.Runtime.InteropServices.InAttribute or System.Runtime.InteropServices.OutAttribute. Please consider removing System.Runtime.InteropServices.InAttribute and replace System.Runtime.InteropServices.OutAttribute with 'out' modifier instead. Method '{0}' has parameter '{1}' with a System.Runtime.InteropServices.InAttribute or System.Runtime.InteropServices.OutAttribute. Windows Runtime only supports marking ByRef parameters with System.Runtime.InteropServices.OutAttribute, and does not support other usages of those attributes. |
| WME1106      | Method '{0}' has parameter '{1}' which is an array. In the Windows Runtime, the contents of array parameters must be either readable or writable. Please apply either {2} or {3} to '{1}'.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         |


## Member with a parameter named "value"


In the UWP, return values are considered to be output parameters, and the names of parameters must be unique. By default, Winmdexp.exe gives the return value the name "value". If your method has a parameter named "value", you will get error WME1092. There are two ways to correct this:

-   Give your parameter a name other than "value" (in property accessors, a name other than "returnValue").
-   Use the ReturnValueNameAttribute attribute to change the name of the return value, as shown here:

    > [!div class="tabbedCodeSnippets"]
    > ```cs
    > using System.Runtime.InteropServices;
    > using System.Runtime.InteropServices.WindowsRuntime;
    >
    > [return: ReturnValueName("average")]
    > public int GetAverage(out int lowValue, out int highValue)
    > ```
    > ```vb
    > Imports System.Runtime.InteropServices
    > Imports System.Runtime.InteropServices.WindowsRuntime
    >
    > Public Function GetAverage(<Out> ByRef lowValue As Integer, _
    > <Out> ByRef highValue As Integer) As <ReturnValueName("average")> String
    > ```

> **Note**  If you change the name of the return value, and the new name collides with the name of another parameter, you will get error WME1091.

JavaScript code can access the output parameters of a method by name, including the return value. For an example, see the [ReturnValueNameAttribute](/dotnet/api/system.runtime.interopservices.windowsruntime.returnvaluenameattribute) attribute.

| Error number | Message Text |
|--------------|--------------|
| WME1091 | The method '\{0}' has the return value named '\{1}' which is the same as a parameter name. Windows Runtime method parameters and return value must have unique names. |
| WME1092 | The method '\{0}' has a parameter named '\{1}' which is the same as the default return value name. Consider using another name for the parameter or use the System.Runtime.InteropServices.WindowsRuntime.ReturnValueNameAttribute to explicitly specify the name of the return value. |

**Note**  The default name is "returnValue" for property accessors and "value" for all other methods.

## Related topics

* [Windows Runtime components with C# and Visual Basic](creating-windows-runtime-components-in-csharp-and-visual-basic.md)
* [Winmdexp.exe (Windows Runtime Metadata Export Tool)](/dotnet/framework/tools/winmdexp-exe-windows-runtime-metadata-export-tool)