---
Description: Lists the Microsoft UI Automation control patterns, the classes that clients use to access them, and the interfaces providers use to implement them.
ms.assetid: 2091883C-5D0C-44ED-936A-709022926A42
title: Control patterns and interfaces
label: Control patterns and interfaces
template: detail.hbs
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Control patterns and interfaces  



Lists the Microsoft UI Automation control patterns, the classes that clients use to access them, and the interfaces providers use to implement them.

The table in this topic describes the Microsoft UI Automation control patterns. The table also lists the classes used by UI Automation clients to access the control patterns and the interfaces used by UI Automation providers to implement them. The **Control pattern** column shows the pattern name from the UI Automation client perspective, as a constant value listed in [**Control Pattern Availability Property Identifiers**](/windows/desktop/WinAuto/uiauto-control-pattern-availability-propids). From the UI Automation provider perspective, each of these patterns is a [**PatternInterface**](/uwp/api/Windows.UI.Xaml.Automation.Peers.PatternInterface) constant name. The **Class provider interface** column shows the name of the interface that providers implement to provide this pattern for a custom XAML control.

For more info about how to implement custom automation peers that expose control patterns and implement the interfaces, see [Custom automation peers](custom-automation-peers.md).

When you implement a control pattern, you should also consult the UI Automation provider documentation that explains some of the expectations that clients will have of a control pattern regardless of which UI framework is used to implement it. Some of the info listed in the general UI Automation provider documentation will influence how you implement your peers and correctly support that pattern. See [Implementing UI Automation Control Patterns](/windows/desktop/WinAuto/uiauto-implementinguiautocontrolpatterns), and view the page that documents the pattern you intend to implement.

| Control pattern | Class provider interface | Description |
|-----------------|--------------------------|-------------|
| **Annotation** | [**IAnnotationProvider**](/uwp/api/Windows.UI.Xaml.Automation.Provider.IAnnotationProvider) | Used to expose the properties of an annotation in a document. |
| **Dock** | [**IDockProvider**](/uwp/api/Windows.UI.Xaml.Automation.Provider.IDockProvider) | Used for controls that can be docked in a docking container. For example, toolbars or tool palettes. |
| **Drag** | [**IDragProvider**](/uwp/api/Windows.UI.Xaml.Automation.Provider.IDragProvider) | Used to support draggable controls, or controls with draggable items. |
| **DropTarget** | [**IDropTargetProvider**](/uwp/api/Windows.UI.Xaml.Automation.Provider.IDropTargetProvider) | Used to support controls that can be the target of a drag-and-drop operation. |
| **ExpandCollapse** | [**IExpandCollapseProvider**](/uwp/api/Windows.UI.Xaml.Automation.Provider.IExpandCollapseProvider) | Used to support controls that visually expand to display more content and collapse to hide content. |
| **Grid** | [**IGridProvider**](/uwp/api/Windows.UI.Xaml.Automation.Provider.IGridProvider) | Used for controls that support grid functionality such as sizing and moving to a specified cell. Note that Grid itself does not implement this pattern because it provides layout but is not a control. |
| **GridItem** | [**IGridItemProvider**](/uwp/api/Windows.UI.Xaml.Automation.Provider.IGridItemProvider) | Used for controls that have cells within grids. |
| **Invoke** | [**IInvokeProvider**](/uwp/api/Windows.UI.Xaml.Automation.Provider.IInvokeProvider) | Used for controls that can be invoked, such as a  [**Button**](/uwp/api/Windows.UI.Xaml.Controls.Button). |
| **ItemContainer** | [**IItemContainerProvider**](/uwp/api/Windows.UI.Xaml.Automation.Provider.IItemContainerProvider) | Enables applications to find an element in a container, such as a virtualized list. |
| **MultipleView** | [**IMultipleViewProvider**](/uwp/api/Windows.UI.Xaml.Automation.Provider.IMultipleViewProvider) | Used for controls that can switch between multiple representations of the same set of information, data, or children. |
| **ObjectModel** | [**IObjectModelProvider**](/uwp/api/Windows.UI.Xaml.Automation.Provider.IObjectModelProvider) | Used to expose a pointer to the underlying object model of a document. |
| **RangeValue** | [**IRangeValueProvider**](/uwp/api/Windows.UI.Xaml.Automation.Provider.IRangeValueProvider) | Used for controls that have a range of values that can be applied to the control. For example, a spinner control containing years might have a range of 1900 to the current year, while another spinner control presenting months would have a range of 1 to 12. |
| **Scroll** | [**IScrollProvider**](/uwp/api/Windows.UI.Xaml.Automation.Provider.IScrollProvider) | Used for controls that can scroll. For example, a control that has scroll bars that are active when there is more information than can be displayed in the viewable area of the control. |
| **ScrollItem** | [**IScrollItemProvider**](/uwp/api/Windows.UI.Xaml.Automation.Provider.IScrollItemProvider) | Used for controls that have individual items in a list that scrolls. For example, a list control that has individual items in the scroll list, such as a combo box control. |
| **Selection** | [**ISelectionProvider**](/uwp/api/Windows.UI.Xaml.Automation.Provider.ISelectionProvider) | Used for selection container controls. For example, [**ListBox**](/uwp/api/Windows.UI.Xaml.Controls.ListBox) and [**ComboBox**](/uwp/api/Windows.UI.Xaml.Controls.ComboBox). |
| **SelectionItem** | [**ISelectionItemProvider**](/uwp/api/Windows.UI.Xaml.Automation.Provider.ISelectionItemProvider) | Used for individual items in selection container controls, such as list boxes and combo boxes. |
| **Spreadsheet** | [**ISpreadsheetProvider**](/uwp/api/Windows.UI.Xaml.Automation.Provider.ISpreadsheetProvider) | Used to expose the contents of a spreadsheet or other grid-based document. |
| **SpreadsheetItem** | [**ISpreadsheetItemProvider**](/uwp/api/Windows.UI.Xaml.Automation.Provider.ISpreadsheetItemProvider) | Used to expose the properties of a cell in a spreadsheet or other grid-based document. |
| **Styles** | [**IStylesProvider**](/uwp/api/Windows.UI.Xaml.Automation.Provider.IStylesProvider) | Used to describe a UI element that has a specific style, fill color, fill pattern, or shape. |
| **SynchronizedInput** | [**ISynchronizedInputProvider**](/uwp/api/Windows.UI.Xaml.Automation.Provider.ISynchronizedInputProvider) | Enables UI Automation client apps to direct the mouse or keyboard input to a specific UI element. |
| **Table** | [**ITableProvider**](/uwp/api/Windows.UI.Xaml.Automation.Provider.ITableProvider) | Used for controls that have a grid as well as header information. For example, a tabular calendar control. |
| **TableItem** | [**ITableItemProvider**](/uwp/api/Windows.UI.Xaml.Automation.Provider.ITableItemProvider) | Used for items in a table. |
| **Text** | [**ITextProvider**](/uwp/api/Windows.UI.Xaml.Automation.Provider.ITextProvider) | Used for edit controls and documents that expose textual information. See also [**ITextRangeProvider**](/uwp/api/windows.ui.xaml.automation.provider.itextrangeprovider) and [**ITextProvider2**](/uwp/api/windows.ui.xaml.automation.provider.itextprovider2). |
| **TextChild** | [**ITextChildProvider**](/uwp/api/windows.ui.xaml.automation.provider.itextchildprovider) | Used to access an element’s nearest ancestor that supports the **Text** control pattern. |
| **TextEdit** | No managed class available | Provides access to a control that modifies text, for example a control that performs auto-correction or enables input composition through an Input Method Editor (IME). |
| **TextRange** | [**ITextRangeProvider**](/uwp/api/windows.ui.xaml.automation.provider.itextrangeprovider) | Provides access to a span of continuous text in a text container that implements [**ITextProvider**](/uwp/api/windows.ui.xaml.automation.provider.itextprovider). See also [**ITextRangeProvider2**](/uwp/api/windows.ui.xaml.automation.provider.itextrangeprovider2). |
| **Toggle** | [**IToggleProvider**](/uwp/api/Windows.UI.Xaml.Automation.Provider.IToggleProvider) | Used for controls where the state can be toggled. For example, [**CheckBox**](/uwp/api/Windows.UI.Xaml.Controls.CheckBox) and menu items that can be checked. |
| **Transform** | [**ITransformProvider**](/uwp/api/Windows.UI.Xaml.Automation.Provider.ITransformProvider) | Used for controls that can be resized, moved, and rotated. Typical uses for the Transform control pattern are in designers, forms, graphical editors, and drawing applications. |
| **Value** | [**IValueProvider**](/uwp/api/Windows.UI.Xaml.Automation.Provider.IValueProvider) | Allows clients to get or set a value on controls that do not support a range of values. |
| **VirtualizedItem** | [**IVirtualizedItemProvider**](/uwp/api/Windows.UI.Xaml.Automation.Provider.IVirtualizedItemProvider) | Exposes items inside containers that are virtualized and need to be made fully accessible as UI Automation elements. |
| **Window** | [**IWindowProvider**](/uwp/api/Windows.UI.Xaml.Automation.Provider.IWindowProvider) | Exposes information specific to windows, a fundamental concept to the Microsoft Windows operating system. Examples of controls that are windows are child windows and dialogs. |

> [!NOTE]
> You won't necessarily find implementations of all these patterns in existing XAML controls. Some of the patterns have interfaces solely to support parity with the general UI Automation framework definition of patterns, and to support automation peer scenarios that will require a purely custom implementation to support that pattern.

> [!NOTE]
> Windows Phone Store apps do not support all the UI Automation control patterns listed here. **Annotation**, **Dock**, **Drag**, **DropTarget**, **ObjectModel** are some of the unsupported patterns.

<span id="related_topics"/>

## Related topics  
* [Custom automation peers](custom-automation-peers.md)
* [Accessibility](accessibility.md)