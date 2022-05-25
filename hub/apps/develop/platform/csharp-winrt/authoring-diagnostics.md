---
description: C#/WinRT diagnostic error messages for authored components
title: Diagnose C#/WinRT component errors
ms.date: 01/27/2021
ms.topic: article
ms.localizationpriority: medium
---

# Diagnose C#/WinRT component errors

This article provides additional information about restrictions on Windows Runtime Components written with C#/WinRT. It expands on the information that is provided in error messages from C#/WinRT when an author builds their component. For existing UWP .NET Native managed components, the metadata for C# WinRT components is generated using [Winmdexp.exe](/dotnet/framework/tools/winmdexp-exe-windows-runtime-metadata-export-tool), a .NET tool. Now that Windows Runtime support is decoupled from .NET, C#/WinRT provides the tooling to generate a .winmd file from your component. The Windows Runtime has more constraints on code than a C# class library, and the C#/WinRT Diagnostic Scanner alerts you of these before generating a .winmd file.

This article covers the errors reported in your build from C#/WinRT. This article serves as an updated version of the information on restrictions for [existing UWP .NET Native managed components](/windows/uwp/winrt-components/creating-windows-runtime-components-in-csharp-and-visual-basic) which use the Winmdexp.exe tool.

Search for the error message text (omitting specific values for placeholders) or the message number. If you don’t find the information you need here, you can help us improve the documentation by using the feedback button at the end of this article. In your feedback, please include the error message. Alternatively, you can file a bug at the [C#/WinRT repo](https://github.com/microsoft/CsWinRT).

This article organizes error messages by scenario.

## Implementing interfaces that aren't valid Windows Runtime interfaces

C#/WinRT components cannot implement certain Windows Runtime interfaces, such as the Windows Runtime interfaces that represent asynchronous actions or operations (**IAsyncAction**, **IAsyncActionWithProgress\<TProgress\>**, **IAsyncOperation\<TResult\>**, or **IAsyncOperationWithProgress\<TResult,TProgress\>**). Instead, use the **AsyncInfo** class for generating async operations in Windows Runtime components. Note: these are not the only invalid interfaces, for example a class cannot implement **System.Exception**.

<table>
<colgroup>
<col />
<col />
</colgroup>
<thead>
<tr class="header">
<th><p>Error number</p></th>
<th><p>Message text</p></th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td><p>CsWinRT1008</p></td>
<td><p>Windows Runtime component type {0} cannot implement interface {1}, as the interface is not a valid Windows Runtime interface</p></td>
</tr>
</tbody>
</table>

## Attribute related errors

In the Windows Runtime, overloaded methods can have the same number of parameters only if one overload is specified as the default overload. Use the attribute **Windows.Foundation.Metadata.DefaultOverload** (CsWinRT1015, 1016).

When arrays are used as inputs or outputs in either functions or properties, they must be either read-only or write-only (CsWinRT 1025). The attributes **System.Runtime.InteropServices.WindowsRuntime.ReadOnlyArray** and **System.Runtime.InteropServices.WindowsRuntime.WriteOnlyArray** are provided for you to use. The provided attributes are only for use on parameters of the array type (CsWinRT1026), and only one should be applied per parameter (CsWinRT1023).

You do not need to apply any attribute to an array parameter marked **out**, as it is assumed to be write-only. There is an error message if you decorate it as read-only in this case (CsWinRT1024).

The attributes **System.Runtime.InteropServices.InAttribute** and **System.Runtime.InteropServices.OutAttribute** should not be used on parameters of any type (CsWinRT1021,1022).

<table>
<colgroup>
<col />
<col />
</colgroup>
<thead>
<tr class="header">
<th><p>Error number</p></th>
<th><p>Message text</p></th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td><p>CsWinRT1015</p></td>
<td><p>In class {2}: Multiple {0}-parameter overloads of '{1}' are decorated with Windows.Foundation.Metadata.DefaultOverloadAttribute. The attribute may only be applied to one overload of the method.</p></td>
</tr>
<tr class="even">
<td><p>CsWinRT1016</p></td>
<td><p>In class {2}: The {0}-parameter overloads of {1} must have exactly one method specified as the default overload by decorating it with the attribute  Windows.Foundation.Metadata.DefaultOverloadAttribute.</p></td>
</tr>
<tr class="odd">
<td><p>CsWinRT1021</p></td>
<td><p>Method '{0}' has parameter '{1}' which is an array, and which has either a System.Runtime.InteropServices.InAttribute or a System.Runtime.InteropServices.OutAttribute. In the Windows Runtime, array parameters must have either ReadOnlyArray or WriteOnlyArray. Please remove these attributes or replace them with the appropriate Windows Runtime attribute if necessary.</p></td>
</tr>
<tr class="even">
<td><p>CsWinRT1022</p></td>
<td><p>Method '{0}' has parameter '{1}' with a System.Runtime.InteropServices.InAttribute or System.Runtime.InteropServices.OutAttribute.Windows Runtime does not support marking parameters with System.Runtime.InteropServices.InAttribute or System.Runtime.InteropServices.OutAttribute. Please consider removing System.Runtime.InteropServices.InAttribute and replace System.Runtime.InteropServices.OutAttribute with 'out' modifier instead.</p></td>
</tr>
<tr class="odd">
<td><p>CsWinRT1023</p></td>
<td><p>Method '{0}' has parameter '{1}' which is an array, and which has both ReadOnlyArray and WriteOnlyArray. In the Windows Runtime, the contents array parameters must be either readable or writable. Please remove one of the attributes from '{1}'.</p></td>
</tr>
<tr class="even">
<td><p>CsWinRT1024</p></td>
<td><p>Method '{0}' has an output parameter '{1}' which is an array, but which has ReadOnlyArray attribute. In the Windows Runtime, the contents of output arrays are writable.  Please remove the attribute from '{1}'.</p></td>
</tr>
<tr class="even">
<td><p>CsWinRT1025</p></td>
<td><p>Method '{0}' has parameter '{1}' which is an array. In the Windows Runtime, the contents of array parameters must be either readable or writable. Please apply either ReadOnlyArray or WriteOnlyArray to '{1}'.</p></td>
</tr>
<tr class="even">
<td><p>CsWinRT1026</p></td>
<td><p>Method '{0}' has parameter '{1}' which is not an array, and which has either a ReadOnlyArray attribute or a WriteOnlyArray attribute. Windows Runtime does not support marking non-array parameters with ReadOnlyArray or WriteOnlyArray."</p></td>
</tr>
</tbody>
</table>

## Namespace errors and invalid names for the output file

In the Windows Runtime, all the public types in a Windows metadata (.winmd) file must be in a namespace that shares the .winmd file name, or in sub-namespaces of the file name. For example, if your Visual Studio project is named A.B (that is, your Windows Runtime component is A.B.winmd), it can contain public classes A.B.Class1 and A.B.C.Class2, but not A.Class3 or D.Class4.

> [!NOTE]
> These restrictions apply only to public types, not to private types used in your implementation.

In the case of A.Class3, you can either move Class3 to another namespace or change the name of the Windows Runtime component to A.winmd. In the previous example, code that calls A.B.winmd will be unable to locate A.Class3.

In the case of D.Class4, no file name can contain both D.Class4 and classes in the A.B namespace, so changing the name of the Windows Runtime component is not an option. You can either move D.Class4 to another namespace, or put it in another Windows Runtime component.

The file system can't distinguish between uppercase and lowercase, so namespaces that differ by case are not allowed (CsWinRT1002).

<table>
<colgroup>
<col />
<col />
</colgroup>
<thead>
<tr class="header">
<th><p>Error number</p></th>
<th><p>Message text</p></th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td><p>CsWinRT1001</p></td>
<td><p>A public type has a namespace ('{1}') that shares no common prefix with other namespaces ('{0}'). All types within a Windows Metadata file must exist in a sub namespace of the namespace that is implied by the file name.</p></td>
</tr>
<tr class="even">
<td><p>CsWinRT1002</p></td>
<td><p>Multiple namespaces found with the name '{0}'; namespace names cannot differ only by case in the Windows Runtime.</p></td>
</tr>
</tbody>
</table>

## Exporting types that aren't valid Windows Runtime types

The public interface of your component must expose only Windows Runtime types. However, .NET provides mappings for a number of commonly used types that are slightly different in .NET and the Windows Runtime. This enables the .NET developer to work with familiar types instead of learning new ones. You can use these mapped .NET Framework types in the public interface of your component. For more information, see [Declaring types in Windows Runtime Components](/windows/uwp/winrt-components/creating-windows-runtime-components-in-csharp-and-visual-basic#declaring-types-in-windows-runtime-components), [Passing Windows Runtime types to managed code](/windows/uwp/winrt-components/creating-windows-runtime-components-in-csharp-and-visual-basic#passing-windows-runtime-types-to-managed-code), and [.NET mappings of Windows Runtime types](/windows/uwp/winrt-components/net-framework-mappings-of-windows-runtime-types).

Many of these mappings are interfaces. For example, IList\<T\> maps to the Windows Runtime interface IVector\<T\>. If you use List\<string\> instead of IList\<string\> as a parameter type, C#/WinRT provides a list of alternatives that includes all the mapped interfaces implemented by List\<T\>. If you use nested generic types, such as List\<Dictionary\<int, string\>\>, C#/WinRT offers choices for each level of nesting. These lists can become quite long.

In general, the best choice is the interface that is closest to the type. For example, for Dictionary\<int, string\>, the best choice is most likely IDictionary\<int, string\>.

<table>
<colgroup>
<col />
<col />
</colgroup>
<thead>
<tr class="header">
<th><p>Error number</p></th>
<th><p>Message text</p></th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td><p>CsWinRT1006</p></td>
<td><p>The member '{0}' has the type '{1}' in its signature. The type '{1}' is not a valid Windows Runtime type. Yet, the type (or its generic parameters) implement interfaces that are valid Windows Runtime types. Consider changing the type '{1} in the member signature to one of the following types from System.Collections.Generic: {2}.</p></td>
</tr>
</tbody>
</table>

In the Windows Runtime, arrays in member signatures must be one-dimensional with a lower bound of 0 (zero). Nested array types such as `myArray[][]` (CsWinRT1017) and `myArray[,]` (CsWinRT1018) are not allowed.

> [!NOTE]
> This restriction does not apply to arrays you use internally in your implementation.

<table>
<colgroup>
<col />
<col />
</colgroup>
<thead>
<tr class="header">
<th><p>Error number</p></th>
<th><p>Message text</p></th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td><p>CsWinRT1017</p></td>
<td><p>Method {0} has a nested array of type {1} in its signature. Arrays in Windows Runtime method signature cannot be nested.</p></td>
</tr>
<tr class="even">
<td><p>CsWinRT1018</p></td>
<td><p>Method '{0}' has a multi-dimensional array of type '{1}' in its signature. Arrays in Windows Runtime method signatures must be one dimensional.</p></td>
</tr>
</tbody>
</table>

## Structures that contain fields of disallowed types

In the Windows Runtime, a structure can contain only fields, and only structures can contain fields. Those fields must be public. Valid field types include enumerations, structures, and primitive types.

<table>
<colgroup>
<col />
<col />
</colgroup>
<thead>
<tr class="header">
<th><p>Error number</p></th>
<th><p>Message text</p></th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td><p>CsWinRT1007</p></td>
<td><p>Structure {0} contains no public fields. Windows Runtime structures must contain at least one public field.</p></td>
</tr>
<tr class="even">
<td><p>CsWinRT1011</p></td>
<td><p>Structure {0} has non-public field. All fields must be public for Windows Runtime structures.</p></td>
</tr>
<tr class="odd">
<td><p>CsWinRT1012</p></td>
<td><p>Structure {0} has const field. Constants can only appear on Windows Runtime enumerations.</p></td>
</tr>
<tr class="even">
<td><p>CsWinRT1013</p></td>
<td><p>Structure {0} has field of type {1}; {1} is not a valid Windows Runtime field type. Each field in a Windows Runtime structure can only be UInt8, Int16, UInt16, Int32, UInt32, Int64, UInt64, Single, Double, Boolean, String, Enum, or itself a structure.</p></td>
</tr>
</tbody>
</table>

## Parameter name conflicts with generated code

In the Windows Runtime, return values are considered to be output parameters, and the names of parameters must be unique. By default, C#/WinRT gives the return value the name `__retval`. If your method has a parameter named `__retval`, you will get error CsWinRT1010. To correct this, give your parameter a name other than `__retvalue`.

<table>
<colgroup>
<col />
<col />
</colgroup>
<thead>
<tr class="header">
<th><p>Error number</p></th>
<th><p>Message text</p></th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td><p>CsWinRT1010</p></td>
<td><p>The parameter name {1} in method {0} is the same as the return value parameter name used in the generated C#/WinRT interop; use a different parameter name.</p></td>
</tr>
</tbody>
</table>

## Miscellaneous

Other restrictions in a C#/WinRT authored component include:

- You cannot expose overloaded operators on public types.
- Classes and interfaces cannot be generic.
- Classes must be sealed.
- Parameters cannot be passed by reference, e.g. using the **ref** keyword.
- Properties must have a public get method.
- There must be at least one public type (class or interface) in your component's namespace.

<table>
<colgroup>
<col />
<col />
</colgroup>
<thead>
<tr class="header">
<th><p>Error number</p></th>
<th><p>Message text</p></th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td><p>CsWinRT1014</p></td>
<td><p>'{0}' is an operator overload. Managed types cannot expose operator overloads in the Windows Runtime.</p></td>
</tr>
<tr class="even">
<td><p>CsWinRT1004</p></td>
<td><p>Type {0} is generic. Windows Runtime types cannot be generic.</p></td>
</tr>
<tr class="odd">
<td><p>CsWinRT1005</p></td>
<td><p>Exporting unsealed types is not supported in CsWinRT. Please mark type {0} as sealed.</p></td>
</tr>
<tr class="even">
<td><p>CsWinRT1020</p></td>
<td><p>Method '{0}' has parameter '{1}' marked `ref`. Reference parameters are not allowed in Windows Runtime.</p></td>
</tr>
<tr class="odd">
<td><p>CsWinRT1000</p></td>
<td><p>Property '{0}' does not have a public getter method. Windows Runtime does not support setter-only properties.</p></td>
</tr>
<tr class="even">
<td><p>CsWinRT1003</p></td>
<td><p>Windows Runtime components must have at least one public type</p></td>
</tr>
</tbody>
</table>

In the Windows Runtime, a class can have only one constructor with a given number of parameters. For example, you can't have one constructor that has a single parameter of type **string** and another constructor that has a single parameter of type **int**. The only workaround is to use a different number of parameters for each constructor.

<table>
<colgroup>
<col />
<col />
</colgroup>
<thead>
<tr class="header">
<th><p>Error number</p></th>
<th><p>Message text</p></th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td><p>CsWinRT1009</p></td>
<td><p>Classes cannot have multiple constructors of the same arity in the Windows Runtime, class {0} has multiple {1}-arity constructors.</p></td>
</tr>
</tbody>
</table>


