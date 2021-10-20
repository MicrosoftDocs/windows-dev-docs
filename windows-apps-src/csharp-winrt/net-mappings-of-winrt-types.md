---
title: .NET mappings of WinRT types in C#/WinRT
description: The following table lists the mappings that C#/WinRT makes between Windows Runtime types and .NET types.
ms.date: 04/19/2020
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# .NET mappings of WinRT types in C#/WinRT

This article lists the mappings that C#/WinRT makes between Windows Runtime (WinRT) types and .NET types in desktop apps that target .NET 5 (or later). In these apps, Visual Studio IntelliSense shows the .NET type instead of the Windows Runtime type. For example, if a Windows Runtime method takes a parameter of type **IVector&lt;string&gt;**, then IntelliSense shows a parameter of type **IList&lt;string&gt;**. Similarly, in a WinRT component authored using C#/WinRT, you use the .NET type in member signatures. When you use C#/WinRT to generate a Windows Runtime component, the .NET type is translated into the corresponding WinRT type.

The C#/WinRT custom type mappings are categorized by types in the Windows SDK or in [WinUI 3](/windows/apps/winui) (WinUI 3 is part of [Project Reunion](/windows/apps/project-reunion)). The WinRT types for Windows SDK mappings live under the **Windows.\*** namespaces, and the WinRT types for WinUI 3 mappings live under the **Microsoft.UI.Xaml.\*** namespaces. There are two reasons for custom type mappings that C#/WinRT makes for WinRT types:

- **WinRT types that map to .NET types with a different name and/or namespace.** These custom mappings are for mapping WinRT types to existing .NET equivalent types. There are also cases where the mapping is to a different type (e.g., a value type maps to a class type).

- **WinRT types that map to .NET types with the same name and namespace.** These custom mappings are generally for performance or enhancement reasons, and are implemented directly in C#. Most of the types that have the same namespace name and type name in WinRT and .NET are structures (or types associated with structures, such as enumerations). In WinRT, structures have no members other than fields, and require helper types, which .NET hides. The .NET versions of these structures have properties and methods that provide the functionality of the hidden helper types (for example, **Windows.UI.Color**).

> [!NOTE]
> For a list of mappings between WinRT and .NET types in the context of UWP apps, see [.NET mappings of WinRT types in UWP](../winrt-components/net-framework-mappings-of-windows-runtime-types.md).

## Mappings for WinRT types in the Windows SDK

### Types with a different name and/or namespace

| WinRT type/namespace | .NET type/namespace |
|-|-|
| DateTime (Windows.Foundation) | DateTimeOffset (System) |
| EventHandler&lt;T&gt; (Windows.Foundation) | EventHandler&lt;T&gt; (System) |
| EventRegistrationToken (Windows.Foundation) | EventRegistrationToken (WinRT) |
| HResult (Windows.Foundation) | Exception (System) |
| IClosable (Windows.Foundation) | IDisposable (System) |
| IReference&lt;T&gt; (Windows.Foundation) | Nullable&lt;T&gt; (System) |
| TimeSpan (Windows.Foundation) | TimeSpan (System) |
| Uri (Windows.Foundation) | Uri (System) |
| IIterable&lt;T&gt; (Windows.Foundation.Collections) | IEnumerable&lt;T&gt; (System.Collections.Generic) |
| IIterator&lt;T&gt; (Windows.Foundation.Collections) | IEnumerator&lt;T&gt; (System.Collections.Generic) |
| IMap&lt;K,V&gt; (Windows.Foundation.Collections) | IDictionary&lt;TKey,TValue&gt; (System.Collections.Generic) |
| IMapView&lt;K,V&gt; (Windows.Foundation.Collections) | IReadOnlyDictionary&lt;TKey,TValue&gt; (System.Collections.Generic) |
| IKeyValuePair&lt;K,V&gt; (Windows.Foundation.Collections) | KeyValuePair&lt;TKey,TValue&gt; (System.Collections.Generic) |
| IVector&lt;T&gt; (Windows.Foundation.Collections) | IList&lt;T&gt; (System.Collections.Generic) |
| IVectorView&lt;T&gt; (Windows.Foundation.Collections) | IReadOnlyList&lt;T&gt; (System.Collections.Generic) |
| AttributeTargets (Windows.Foundation.Metadata) | AttributeTargets (System) |
| AttributeUsageAttribute (Windows.Foundation.Metadata) | AttributeUsageAttribute (System) |
| Matrix3x2 (Windows.Foundation.Numerics) | Matrix3x2 (System.Numerics) |
| Matrix4x4 (Windows.Foundation.Numerics) | Matrix4x4 (System.Numerics) |
| Plane (Windows.Foundation.Numerics) | Plane (System.Numerics) |
| Quaternion (Windows.Foundation.Numerics) | Quaternion (System.Numerics) |
| Vector2 (Windows.Foundation.Numerics) | Vector2 (System.Numerics) |
| Vector3 (Windows.Foundation.Numerics) | Vector3 (System.Numerics) |
| Vector4 (Windows.Foundation.Numerics) | Vector4 (System.Numerics) |
| IBindableIterable (Windows.UI.Xaml.Interop) | IEnumerable (System.Collections) |
| IBindableVector (Windows.UI.Xaml.Interop) | IList (System.Collections) |
| TypeName (Windows.UI.Xaml.Interop) | Type (System) |

### Types with the same name and namespace

| Type | Namespace |
|-|-|
| IPropertyValue | Windows.Foundation |
| IReferenceArray&lt;T&gt; | Windows.Foundation |
| Point | Windows.Foundation |
| Rect | Windows.Foundation |
| Size | Windows.Foundation |
| Color | Windows.UI |
| TypeKind | Windows.UI.Xaml.Interop |

## Mappings for WinRT types in WinUI

### Types with a different name and/or namespace

| WinRT type/namespace | .NET type/namespace |
|-|-|
| INotifyCollectionChanged (Microsoft.UI.Xaml.Data) | INotifyCollectionChanged (System.Collections.Specialized) | 
| NotifyCollectionChangedEventHandler (Microsoft.UI.Xaml.Data) | NotifyCollectionChangedEventHandler (System.Collections.Specialized) | 
| NotifyCollectionChangedEventArgs (Microsoft.UI.Xaml.Data) | NotifyCollectionChangedEventArgs (System.Collections.Specialized) | 
| NotifyCollectionChangedAction (Microsoft.UI.Xaml.Data) | NotifyCollectionChangedAction (System.Collections.Specialized) | 
| DataErrorsChangedEventArgs (Microsoft.UI.Xaml.Data) | DataErrorsChangedEventArgs (System.ComponentModel) | 
| INotifyDataErrorInfo (Microsoft.UI.Xaml.Data) | INotifyDataErrorInfo (System.ComponentModel) | 
| INotifyPropertyChanged (Microsoft.UI.Xaml.Data) | INotifyPropertyChanged (System.ComponentModel) | 
| PropertyChangedEventHandler (Microsoft.UI.Xaml.Data) | PropertyChangedEventHandler (System.ComponentModel) | 
| PropertyChangedEventArgs (Microsoft.UI.Xaml.Data) | PropertyChangedEventArgs (System.ComponentModel) | 
| ICommand (Microsoft.UI.Xaml.Input) | ICommand (System.Windows.Input) |
| IXamlServiceProvider (Microsoft.UI.Xaml) | IServiceProvider (System) | 

### Types with the same name and namespace

| Type | Namespace |
|-|-|
| CornerRadius | Microsoft.UI.Xaml | 
| Duration | Microsoft.UI.Xaml | 
| DurationType | Microsoft.UI.Xaml | 
| GridLength | Microsoft.UI.Xaml | 
| GridUnitType | Microsoft.UI.Xaml | 
| Thickness | Microsoft.UI.Xaml | 
| GeneratorPosition | Microsoft.UI.Xaml.Controls.Primitives | 
| Matrix | Microsoft.UI.Xaml.Media | 
| KeyTime | Microsoft.UI.Xaml.Media.Animation | 
| RepeatBehavior | Microsoft.UI.Xaml.Media.Animation | 
| RepeatBehaviorType | Microsoft.UI.Xaml.Media.Animation | 
| Matrix3D |(Microsoft.UI.Xaml.Media.Media3D | 
