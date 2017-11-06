---
author: Jwmsft
Description: Use the tree view example code to create an expandable tree.
title: Tree view
label: Tree view
template: detail.hbs
localizationpriority: medium
pm-contact: predavid
design-contact: ksulliv
dev-contact: joyate
doc-status: Published
---
# TreeView
 

> [!IMPORTANT]
> This article describes functionality that hasn’t been released yet and may be substantially modified before it's commercially released. Microsoft makes no warranties, express or implied, with respect to the information provided here.

The new XAML TreeView control enables a hierarchical list with expanding and collapsing nodes that contain nested items. It can be used to illustrate a folder structure or nested relationships in your UI.

> **Important APIs**: [TreeView class](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.treeview), [TreeViewItem class](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.treeviewitem), [TreeViewList class](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.treeviewlist), [TreeViewNode class](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.treeviewnode)

The TreeView APIs support the following features:

- N-level nesting
- Expanding/collapsing of nodes
- Built-in accessibility

## Is this the right control?

- Use a TreeView when items have nested list items, and if it is important to illustrate the hierarchical relationship of items to their peers and nodes.

- Avoid using TreeView if highlighting the nested relationship of an item is not a priority. For most drill-in scenarios, a regular list view is appropriate

## TreeView UI structure

You can use icons to represent nodes in a TreeView. A combination of indentation and icons can be used to represent the nested relationship between
folder/parent nodes and non-folder/child nodes. Here is guidance on how to do so.

### Icons

Use icons to indicate that an item is a node, as well as what state the node is in (expanded or collapsed).

#### Chevron

For consistency, collapsed nodes should use a chevron pointing to the right, and expanded nodes should use a chevron pointing down.

![Usage of the Chevron icon in TreeView](images/treeview_chevron.png)

#### Folder

Use a folder icon only for literal representations of folders.

![Usage of the Folder icon in TreeView](images/treeview_folder.png)

#### Chevron and Folder

A combination of a chevron and a folder should be used only if non-node list items in the TreeView also have icons.

![Usage of the Chevron and Folder icons together in a TreeView](images/treeview_chevron_folder.png)

## Building a TreeView

TreeView has the following main classes.

- The [TreeViewNode](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.treeviewnode) class implements the hierarchical layout for the TreeView. It also holds the data that will be bound to it in the item's template.
- The [TreeView](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.treeview) class implements events for ItemClick and expand/collapse of folders.
- The [TreeViewItem](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.treeviewitem) class has the styles, brushes, and glyphs for a folder type TreeViewItem.

## Declare a TreeView in XAML

Here's an example of a TreeView declared in XAML.

```xaml
<TreeView x:Name="sampleTreeView"/>
```

## Set up the data in your TreeView

Here is the code that sets up the data for the TreeView.

```csharp
public MainPage()
{
    this.InitializeComponent();

    TreeViewNode workFolder = new TreeViewNode() {Data = "Work Documents" };
    workFolder.IsExpanded = true;
    workFolder.Add(new TreeViewNode() { Data = "Feature Schedule" });
    workFolder.Add(new TreeViewNode() { Data = "Overall Project Plan" });
    workFolder.Add(new TreeViewNode() { Data = "Feature Rsource Allocation" });

    TreeViewNode remodelFolder = new TreeViewNode() { Data = "Home Remodel" };
    remodelFolder.IsExpanded = true;
    remodelFolder.Add(new TreeViewNode() { Data = "Contractor Contact Info" });
    remodelFolder.Add(new TreeViewNode() { Data = "Paint Color Scheme" });
    remodelFolder.Add(new TreeViewNode() { Data = "Flooring woodgrain type" });
    remodelFolder.Add(new TreeViewNode() { Data = "Kitchen cabinet style" });

    TreeViewNode personalFolder = new TreeViewNode() { Data = "Personal Documents" };
    personalFolder.IsExpanded = true;

    personalFolder.Add(remodelFolder);
    workFolder.Add(personalFolder);
    sampleTreeView.RootNode = workFolder;
}
```

Once you’re done with the above steps, you will have a fully populated TreeView/Hierarchical layout with n-level nesting, support for expand/collapse of folders, and accessibility built in.

To provide the user the ability to add/remove items from the TreeView, we recommend you add a context menu to expose those options to the user.


## Related articles

- [TreeView class](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.treeview)
- [ListView class](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.controls.listview.aspx)
- [ListView and GridView](listview-and-gridview.md)
