---
author: Xansky
Description: Lists the Microsoft UI Automation control patterns, the classes that clients use to access them, and the interfaces providers use to implement them.
ms.assetid: 2091883C-5D0C-44ED-936A-709022926A42
title: Control patterns and interfaces
label: Control patterns and interfaces
template: detail.hbs
ms.author: mhopkins
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Control patterns and interfaces  



Lists the Microsoft UI Automation control patterns, the classes that clients use to access them, and the interfaces providers use to implement them.

The table in this topic describes the Microsoft UI Automation control patterns. The table also lists the classes used by UI Automation clients to access the control patterns and the interfaces used by UI Automation providers to implement them. The **Control pattern** column shows the pattern name from the UI Automation client perspective, as a constant value listed in [**Control Pattern Availability Property Identifiers**](https://msdn.microsoft.com/library/windows/desktop/Ee671199). From the UI Automation provider perspective, each of these patterns is a [**PatternInterface**](https://msdn.microsoft.com/library/windows/apps/BR242496) constant name. The **Class provider interface** column shows the name of the interface that providers implement to provide this pattern for a custom XAML control.

For more info about how to implement custom automation peers that expose control patterns and implement the interfaces, see [Custom automation peers](custom-automation-peers.md).

When you implement a control pattern, you should also consult the UI Automation provider documentation that explains some of the expectations that clients will have of a control pattern regardless of which UI framework is used to implement it. Some of the info listed in the general UI Automation provider documentation will influence how you implement your peers and correctly support that pattern. See [Implementing UI Automation Control Patterns](https://msdn.microsoft.com/library/windows/desktop/Ee671292), and view the page that documents the pattern you intend to implement.

| Control pattern | Class provider interface | Description |
|-----------------|--------------------------|-------------|
| **Annotation** | [**IAnnotationProvider**](https://msdn.microsoft.com/library/windows/apps/Hh738493) | Used to expose the properties of an annotation in a document. |
| **Dock** | [**IDockProvider**](https://msdn.microsoft.com/library/windows/apps/BR242565) | Used for controls that can be docked in a docking container. For example, toolbars or tool palettes. |
| **Drag** | [**IDragProvider**](https://msdn.microsoft.com/library/windows/apps/Hh750322) | Used to support draggable controls, or controls with draggable items. |
| **DropTarget** | [**IDropTargetProvider**](https://msdn.microsoft.com/library/windows/apps/Hh750327) | Used to support controls that can be the target of a drag-and-drop operation. |
| **ExpandCollapse** | [**IExpandCollapseProvider**](https://msdn.microsoft.com/library/windows/apps/BR242568) | Used to support controls that visually expand to display more content and collapse to hide content. |
| **Grid** | [**IGridProvider**](https://msdn.microsoft.com/library/windows/apps/BR242578) | Used for controls that support grid functionality such as sizing and moving to a specified cell. Note that Grid itself does not implement this pattern because it provides layout but is not a control. |
| **GridItem** | [**IGridItemProvider**](https://msdn.microsoft.com/library/windows/apps/BR242572) | Used for controls that have cells within grids. |
| **Invoke** | [**IInvokeProvider**](https://msdn.microsoft.com/library/windows/apps/BR242582) | Used for controls that can be invoked, such as a  [**Button**](https://msdn.microsoft.com/library/windows/apps/BR209265). |
| **ItemContainer** | [**IItemContainerProvider**](https://msdn.microsoft.com/library/windows/apps/BR242583) | Enables applications to find an element in a container, such as a virtualized list. |
| **MultipleView** | [**IMultipleViewProvider**](https://msdn.microsoft.com/library/windows/apps/BR242585) | Used for controls that can switch between multiple representations of the same set of information, data, or children. |
| **ObjectModel** | [**IObjectModelProvider**](https://msdn.microsoft.com/library/windows/apps/Dn251815) | Used to expose a pointer to the underlying object model of a document. |
| **RangeValue** | [**IRangeValueProvider**](https://msdn.microsoft.com/library/windows/apps/BR242590) | Used for controls that have a range of values that can be applied to the control. For example, a spinner control containing years might have a range of 1900 to the current year, while another spinner control presenting months would have a range of 1 to 12. |
| **Scroll** | [**IScrollProvider**](https://msdn.microsoft.com/library/windows/apps/BR242601) | Used for controls that can scroll. For example, a control that has scroll bars that are active when there is more information than can be displayed in the viewable area of the control. |
| **ScrollItem** | [**IScrollItemProvider**](https://msdn.microsoft.com/library/windows/apps/BR242599) | Used for controls that have individual items in a list that scrolls. For example, a list control that has individual items in the scroll list, such as a combo box control. |
| **Selection** | [**ISelectionProvider**](https://msdn.microsoft.com/library/windows/apps/BR242616) | Used for selection container controls. For example, [**ListBox**](https://msdn.microsoft.com/library/windows/apps/BR242868) and [**ComboBox**](https://msdn.microsoft.com/library/windows/apps/BR209348). |
| **SelectionItem** | [**ISelectionItemProvider**](https://msdn.microsoft.com/library/windows/apps/BR242610) | Used for individual items in selection container controls, such as list boxes and combo boxes. |
| **Spreadsheet** | [**ISpreadsheetProvider**](https://msdn.microsoft.com/library/windows/apps/Dn251821) | Used to expose the contents of a spreadsheet or other grid-based document. |
| **SpreadsheetItem** | [**ISpreadsheetItemProvider**](https://msdn.microsoft.com/library/windows/apps/Dn251817) | Used to expose the properties of a cell in a spreadsheet or other grid-based document. |
| **Styles** | [**IStylesProvider**](https://msdn.microsoft.com/library/windows/apps/Dn251823) | Used to describe a UI element that has a specific style, fill color, fill pattern, or shape. |
| **SynchronizedInput** | [**ISynchronizedInputProvider**](https://msdn.microsoft.com/library/windows/apps/Dn279198) | Enables UI Automation client apps to direct the mouse or keyboard input to a specific UI element. |
| **Table** | [**ITableProvider**](https://msdn.microsoft.com/library/windows/apps/BR242623) | Used for controls that have a grid as well as header information. For example, a tabular calendar control. |
| **TableItem** | [**ITableItemProvider**](https://msdn.microsoft.com/library/windows/apps/BR242620) | Used for items in a table. |
| **Text** | [**ITextProvider**](https://msdn.microsoft.com/library/windows/apps/BR242627) | Used for edit controls and documents that expose textual information. See also [**ITextRangeProvider**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.automation.provider.itextrangeprovider) and [**ITextProvider2**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.automation.provider.itextprovider2). |
| **TextChild** | [**ITextChildProvider**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.automation.provider.itextchildprovider) | Used to access an element’s nearest ancestor that supports the **Text** control pattern. |
| **TextEdit** | No managed class available | Provides access to a control that modifies text, for example a control that performs auto-correction or enables input composition through an Input Method Editor (IME). |
| **TextRange** | [**ITextRangeProvider**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.automation.provider.itextrangeprovider) | Provides access to a span of continuous text in a text container that implements [**ITextProvider**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.automation.provider.itextprovider). See also [**ITextRangeProvider2**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.automation.provider.itextrangeprovider2). |
| **Toggle** | [**IToggleProvider**](https://msdn.microsoft.com/library/windows/apps/BR242653) | Used for controls where the state can be toggled. For example, [**CheckBox**](https://msdn.microsoft.com/library/windows/apps/BR209316) and menu items that can be checked. |
| **Transform** | [**ITransformProvider**](https://msdn.microsoft.com/library/windows/apps/BR242656) | Used for controls that can be resized, moved, and rotated. Typical uses for the Transform control pattern are in designers, forms, graphical editors, and drawing applications. |
| **Value** | [**IValueProvider**](https://msdn.microsoft.com/library/windows/apps/BR242663) | Allows clients to get or set a value on controls that do not support a range of values. |
| **VirtualizedItem** | [**IVirtualizedItemProvider**](https://msdn.microsoft.com/library/windows/apps/BR242668) | Exposes items inside containers that are virtualized and need to be made fully accessible as UI Automation elements. |
| **Window** | [**IWindowProvider**](https://msdn.microsoft.com/library/windows/apps/BR242670) | Exposes information specific to windows, a fundamental concept to the Microsoft Windows operating system. Examples of controls that are windows are child windows and dialogs. |

> [!NOTE]
> You won't necessarily find implementations of all these patterns in existing XAML controls. Some of the patterns have interfaces solely to support parity with the general UI Automation framework definition of patterns, and to support automation peer scenarios that will require a purely custom implementation to support that pattern.

> [!NOTE]
> Windows Phone Store apps do not support all the UI Automation control patterns listed here. **Annotation**, **Dock**, **Drag**, **DropTarget**, **ObjectModel** are some of the unsupported patterns.

<span id="related_topics"/>

## Related topics  
* [Custom automation peers](custom-automation-peers.md)
* [Accessibility](accessibility.md) 
