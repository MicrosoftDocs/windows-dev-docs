---
title: .NET mappings of Windows Runtime types
description: The following table lists the mappings that .NET makes between Universal Windows Platform (UWP) types and .NET types.
ms.assetid: 5317D771-808D-4B97-8063-63492B23292F
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# .NET mappings of Windows Runtime types

The following table lists the mappings that .NET makes between Universal Windows Platform (UWP) types and .NET types. In a Universal Windows app written with managed code, Visual Studio IntelliSense shows the .NET type instead of the UWP type. For example, if a Windows Runtime method takes a parameter of type IVector&lt;string&gt;, then IntelliSense shows a parameter of type IList&lt;string&gt;. Similarly, in a Windows Runtime component written with managed code, you use the .NET type in member signatures. When the [Windows Runtime Metadata Export Tool (Winmdexp.exe)](/dotnet/framework/tools/winmdexp-exe-windows-runtime-metadata-export-tool) generates your Windows Runtime Component, the .NET type is translated into the corresponding UWP type.

Most of the types that have the same namespace name and type name in both the UWP and .NET are structures (or types associated with structures, such as enumerations). In UWP, structures have no members other than fields, and require helper types, which .NET hides. The .NET versions of these structures have properties and methods that provide the functionality of the hidden helper types.

## UWP types that map to .NET types with the same name and namespace

### In .NET assembly System.ObjectModel.dll

| Namespace | Type |
|-|-|
| Windows.UI.Xaml.Input | ICommand |

### In .NET assembly System.Runtime.WindowsRuntime.dll

| Namespace | Type |
|-|-|
| Windows.Foundation | Point |
| Windows.Foundation | Rect |
| Windows.Foundation | Size |
| Windows.UI | Color |

### In .NET assembly System.Runtime.WindowsRuntime.UI.Xaml.dll

| Namespace | Type |
|-|-|
| Windows.UI.Xaml | CornerRadius |
| Windows.UI.Xaml | Duration |
| Windows.UI.Xaml | DurationType |
| Windows.UI.Xaml | GridLength |
| Windows.UI.Xaml | GridUnitType |
| Windows.UI.Xaml | Thickness |
| Windows.UI.Xaml.Controls.Primitives | GeneratorPosition |
| Windows.UI.Xaml.Media | Matrix |
| Windows.UI.Xaml.Media.Animation | KeyTime |
| Windows.UI.Xaml.Media.Animation | RepeatBehavior |
| Windows.UI.Xaml.Media.Animation | RepeatBehaviorType |
| Windows.UI.Xaml.Media.Media3D | Matrix3D |

## UWP types that map to .NET types with a different name and/or namespace

### In .NET assembly System.ObjectModel.dll

| UWP type/namespace | .NET type/namespace |
|-|-|
| INotifyCollectionChanged (Windows.UI.Xaml.Interop) | INotifyCollectionChanged (System.Collections.Specialized) | 
| NotifyCollectionChangedEventHandler (Windows.UI.Xaml.Interop) | NotifyCollectionChangedEventHandler (System.Collections.Specialized) | 
| NotifyCollectionChangedEventArgs (Windows.UI.Xaml.Interop) | NotifyCollectionChangedEventArgs (System.Collections.Specialized) | 
| NotifyCollectionChangedAction (Windows.UI.Xaml.Interop) | NotifyCollectionChangedAction (System.Collections.Specialized) | 
| INotifyPropertyChanged (Windows.UI.Xaml.Data) | INotifyPropertyChanged (System.ComponentModel) | 
| PropertyChangedEventHandler (Windows.UI.Xaml.Data) | PropertyChangedEventHandler (System.ComponentModel) | 
| PropertyChangedEventArgs (Windows.UI.Xaml.Data) | PropertyChangedEventArgs (System.ComponentModel) | 

### In .NET assembly System.Runtime.dll

| UWP type/namespace | .NET type/namespace |
|-|-|
| AttributeUsageAttribute (Windows.Foundation.Metadata) | AttributeUsageAttribute (System) |
| AttributeTargets (Windows.Foundation.Metadata) | AttributeTargets (System) |
| DateTime (Windows.Foundation) | DateTimeOffset (System) |
| EventHandler&lt;T&gt; (Windows.Foundation) | EventHandler&lt;T&gt; (System) |
| HResult (Windows.Foundation) | Exception (System) |
| IReference&lt;T&gt; (Windows.Foundation) | Nullable&lt;T&gt; (System) |
| TimeSpan (Windows.Foundation) | TimeSpan (System) |
| Uri (Windows.Foundation) | Uri (System) |
| IClosable (Windows.Foundation) | IDisposable (System) |
| IIterable&lt;T&gt; (Windows.Foundation.Collections) | IEnumerable&lt;T&gt; (System.Collections.Generic) |
| IVector&lt;T&gt; (Windows.Foundation.Collections) | IList&lt;T&gt; (System.Collections.Generic) |
| IVectorView&lt;T&gt; (Windows.Foundation.Collections) | IReadOnlyList&lt;T&gt; (System.Collections.Generic) |
| IMap&lt;K,V&gt; (Windows.Foundation.Collections) | IDictionary&lt;TKey,TValue&gt; (System.Collections.Generic) |
| IMapView&lt;K,V&gt; (Windows.Foundation.Collections) | IReadOnlyDictionary&lt;TKey,TValue&gt; (System.Collections.Generic) |
| IKeyValuePair&lt;K,V&gt; (Windows.Foundation.Collections) | KeyValuePair&lt;TKey,TValue&gt; (System.Collections.Generic) |
| IBindableIterable (Windows.UI.Xaml.Interop) | IEnumerable (System.Collections) |
| IBindableVector (Windows.UI.Xaml.Interop) | IList (System.Collections) |
| TypeName (Windows.UI.Xaml.Interop) | Type (System) |

### In .NET assembly System.Runtime.InteropServices.WindowsRuntime.dll

| UWP type/namespace | .NET type/namespace |
|-|-|
| EventRegistrationToken (Windows.Foundation) | EventRegistrationToken (System.Runtime.InteropServices.WindowsRuntime) |

## Related topics

* [Windows Runtime components with C# and Visual Basic](creating-windows-runtime-components-in-csharp-and-visual-basic.md)