---
description: Learn what XAML is and how to use it to build WinUI app UIs — namespaces, markup extensions, events, resources, and more.
title: XAML overview
ms.assetid: 48041B37-F1A8-44A4-BB8E-1D4DE30E7823
ms.date: 07/03/2026
ms.topic: concept-article
keywords: windows 10, uwp
ms.localizationpriority: medium
dev_langs:
  - csharp
  - cppwinrt
---
# XAML overview

XAML (Extensible Application Markup Language) is the declarative markup language used to define UI in WinUI apps. You describe your UI structure and properties in `.xaml` files, and handle logic and events in a paired code-behind file.

## What is XAML?

XAML is an XML-based language for creating objects and setting their properties. You declare UI elements—buttons, text boxes, layouts—in markup, and use a code-behind file to respond to events and manipulate those elements at runtime.

Because XAML is a text-based interchange format, designers and developers can work independently: designers author the visual structure in XAML, developers wire up logic in code-behind, and both can iterate without blocking each other.

## Basic XAML syntax

All valid XAML is also valid XML, but XAML adds its own concepts on top. For example, *property element syntax* lets you set a property as a nested XML element rather than an attribute string — valid XML, but with extra meaning in XAML.

For full syntax details, see [XAML syntax guide](xaml-syntax-guide.md).

## XAML and Visual Studio

Visual Studio's XAML editor provides IntelliSense, auto-completion, and a visual design surface. Errors appear inline as squiggles before you compile. You don't need to memorize every syntax rule — let the IDE guide you.

## XAML namespaces

Each XAML file declares namespaces in its root element. A typical WinUI page starts with two:

```xml
<Page
    x:Class="Application1.BlankPage"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
>
```

- **Default namespace** (`xmlns=`): Contains all built-in WinUI controls, text, graphics, data binding, and styling types. Most of your XAML uses this namespace with no prefix.
- **XAML-language namespace** (`xmlns:x=`): Mapped to the `x:` prefix, provides core language features.

### The `x:` namespace

| Attribute | Purpose |
| --- | --- |
| [x:Key](x-key-attribute.md) | Unique key for a resource in a `ResourceDictionary` |
| [x:Class](x-class-attribute.md) | Links the XAML file to its code-behind class |
| [x:Name](x-name-attribute.md) | Assigns a runtime name so code-behind can reference the element |
| [x:Uid](x-uid-directive.md) | Marks elements for localized resource substitution |
| [XAML intrinsic types](xaml-intrinsic-data-types.md) | Expresses simple values (like `x:Boolean`) as object elements |

### Mapping custom types

To use your own types or third-party library types in XAML, declare a namespace prefix that maps to the code namespace:

```xml
xmlns:myTypes="using:myCompany.myTypes"
```

Then reference those types with the prefix:

```xml
<myTypes:CustomButton/>
```

For details, including C++/CX considerations, see [XAML namespaces and namespace mapping](xaml-namespaces-and-namespace-mapping.md).

### Designer and compatibility namespaces

You'll often see `d:` (designer) and `mc:` (markup compatibility) prefixes in tool-generated XAML. These support design-time tooling and can be safely ignored at runtime. For details, see [Other XAML namespaces](xaml-namespaces-and-namespace-mapping.md#other-XAML-namespaces).

## Markup extensions

Markup extensions let you set a property to something that can't be expressed as a plain string — like a reference to a shared resource or a data binding. They use curly brace syntax:

```xml
<Button Style="{StaticResource SearchButtonStyle}"/>
```

Without `{StaticResource}`, you'd need verbose property element syntax to assign a `Style`. Markup extensions keep XAML concise.

Windows Runtime XAML supports these markup extensions:

| Extension | Purpose |
| --- | --- |
| [{x:Bind}](x-bind-markup-extension.md) | Compile-time data binding (best performance) |
| [{Binding}](binding-markup-extension.md) | Runtime data binding |
| [{StaticResource}](staticresource-markup-extension.md) | Reference a `ResourceDictionary` entry by key |
| [{ThemeResource}](themeresource-markup-extension.md) | Like `{StaticResource}`, but updates when the app theme changes |
| [{TemplateBinding}](templatebinding-markup-extension.md) | Bind to a property of a control template's parent |
| [{RelativeSource}](relativesource-markup-extension.md) | Bind relative to the templated parent |
| [{CustomResource}](customresource-markup-extension.md) | Advanced custom resource lookup |
| [{x:Null}](x-null-markup-extension.md) | Explicitly set a nullable value to `null` |

Markup extensions can be nested; the innermost is evaluated first.

## Events

Attach event handlers in XAML by specifying the event as an attribute and the handler method name as its value:

```xml
<Button Click="showUpdatesButton_Click">Show updates</Button>
```

The compiler expects a matching method in code-behind with the correct delegate signature:

```csharp
private void showUpdatesButton_Click(object sender, RoutedEventArgs e)
{
    // your code
}
```

```cppwinrt
void showUpdatesButton_Click(Windows::Foundation::IInspectable const&, Microsoft::UI::Xaml::RoutedEventArgs const&);
```

> [!NOTE]
> In C++/CX, each XAML page has two code-behind files: a header (`.xaml.h`) and an implementation (`.xaml.cpp`).

For more, see [Events and routed events overview](events-and-routed-events-overview.md).

## Resource dictionaries

A `ResourceDictionary` stores reusable objects — brushes, styles, templates — identified by `x:Key`. Resources can live in a page's resources section or in a separate `.xaml` file. For a full explanation, see [ResourceDictionary and XAML resource references](xaml-resource-dictionary.md).

## XAML and XML

XAML is a superset of XML with a few things to know:

- **Case-sensitive**: Element and attribute names must match exactly.
- **`xml:lang`**: Valid in XAML, but maps to framework-level runtime behavior, not parse behavior.
- **Encoding**: Only UTF-8 and UTF-16 are supported.

## XAML namescopes

Names assigned with `x:Name` must be unique within their *namescope*. Namescopes are particularly relevant inside control templates, where names are scoped to the template rather than the page. For details, see [XAML namescopes](xaml-namescopes.md).

## Performance tips

- Define frequently-used brushes as `SolidColorBrush` resources instead of repeating color literals.
- Put resources shared across pages in `Application.Resources`; keep page-specific resources local to that page.
- Remove unused resources (keys with no `{StaticResource}` references) before shipping.
- Avoid overriding control templates — the defaults are already load-performance optimized.
- Use `Border` and layout containers rather than drawing the same pixel multiple times (overdraw).
- Use the default item templates for `ListView` and `GridView`; their `Presenter` logic is tuned for large lists.

For more guidance, see [Optimize your XAML markup](/windows/uwp/debug-test-perf/optimize-xaml-loading).

## Debug XAML

You can't set breakpoints in `.xaml` files, but Visual Studio provides other help:

- **Design time**: Squiggles and error markers appear inline as you type.
- **Runtime**: Parse failures throw a `XamlParseException` (or a similar structured exception in C++/CX) with XAML file line-number context.

For runtime error handling, see [Exception handling for WinUI apps in C# or Visual Basic](/previous-versions/windows/apps/dn532194(v=win.10)). To start a debugging session, see [Start a debug session](/visualstudio/debugger/start-a-debugging-session-for-a-store-app-in-visual-studio-vb-csharp-cpp-and-xaml).
