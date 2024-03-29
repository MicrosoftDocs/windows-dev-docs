---
description: Filter the items in your collection through user input.
title: Filtering collections
label: Filtering collections
template: detail.hbs
ms.date: 3/29/2024
ms.topic: article
keywords: windows 10, uwp
pm-contact: anawish
---

# Filtering collections and lists through user input

If your collection displays many items or is heavily tied to user interaction, filtering is a useful feature to implement. Filtering using the method described in this article can be implemented with most collection controls, including [ListView](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.listview), [GridView](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.gridview), and [ItemsView](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemsview). Many types of user input can be used to filter a collection - such as checkboxes, radio buttons, and sliders - but this article demonstrates taking text-based user input and using it to update a ListView in real time, according to the user's search.

## Setting up the UI for filtering

To implement text filtering, your app will need a [ListView](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.listview) and a [TextBox](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.textbox) or other control that allows user input. The text that the user types into the TextBox is used as the filter; that is, only results that contain the user's text input will appear in the ListView. As the user types into the TextBox, the ListView constantly updates with filtered results.

> [!NOTE]
> This article demonstrates filtering with a ListView. However, the filtering that is demonstrated can also be applied to other collection controls such as GridView, ItemsView, or TreeView.

The following XAML shows a UI with a simple ListView along with an accompanying TextBox. In this example, the ListView displays a collection of `Contact` objects. `Contact` is a class defined in the code-behind, and each `Contact` object has the following properties: `FirstName`, `LastName`, and `Company`.

The user can type a filtering term into the TextBox to filter the list of `Contact` objects by last name. The TextBox has it's `x:Name` attribute set (`FilterByLastName`) so you can access the TextBox's [Text](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.textbox.text) property in the code-behind. You also handle it's [TextChanged](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.textbox.textchanged) event (`OnFilterChanged`). The TextChanged event occurs whenever the user types in the TextBox, letting you perform a filtering operation upon receiving user input.

For filtering to work, the ListView must have a data source that can be manipulated in the code-behind, such as an [ObservableCollection\<T>](/dotnet/api/system.collections.objectmodel.observablecollection-1). In this case, the ListView's [ItemsSource](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemscontrol.itemssource) property is assigned to an `ObservableCollection<Contact>` in the code-behind.

> [!TIP]
> This is a simplified version of the example in the ListView page of the [WinUI Gallery app](#get-the-sample-code). Use the WinUI Gallery app to run and view the full code, including the ListView's [DataTemplate](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.datatemplate) and the `Contact` class.

```xaml
<Grid>
    <StackPanel Width="300" Margin="24"
                HorizontalAlignment="Left">
        <TextBox x:Name="FilterByLastName"
                 Header="Filter by Last Name"
                 TextChanged="OnFilterChanged"/>
        <ListView x:Name="FilteredListView"
             ItemTemplate="{StaticResource ContactListViewTemplate}"/>
    </StackPanel>
</Grid>

```

## Filtering the data

[Linq](/dotnet/csharp/programming-guide/concepts/linq/introduction-to-linq-queries) queries let you group, order, and select certain items in a collection. To filter a list, you construct a Linq query that selects only items that match the user-entered filtering term, entered in the `FilterByLastName` TextBox. The query result can be assigned to an [IEnumerable\<T>](/dotnet/api/system.collections.generic.ienumerable-1) collection object. Once you have this collection, you can use it to compare with the original list, removing items that don't match and adding back items that do match (in case of a backspace).

> [!NOTE]
> In order for the ListView to animate in the most intuitive way when adding and removing items, it's important to add and remove items in the ListView's [ItemsSource](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemscontrol.itemssource) collection itself, rather than create a new collection of filtered objects and assign that to the ListView's ItemsSource property.

To start, you'll need to initialize your original data source in a separate collection, such as a [List\<T>](/dotnet/api/system.collections.generic.list-1). In this example, you have a `List<Contact>` called `allContacts` that holds all of the `Contact` objects that can potentially be shown in the ListView.

You'll also need a collection to hold the filtered data, which will constantly change every time a filter is applied. For this, you'll use an [ObservableCollection\<T>](/dotnet/api/system.collections.objectmodel.observablecollection-1) so that the ListView is notified to update whenever the collection changes. In this example, it's an `ObservableCollection<Person>` called `contactsFiltered`, and is the [ItemsSource](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemscontrol.itemssource) for the ListView. At initialization, it will have the same contents as `allContacts`.

The filtering operation is performed through these steps, shown in the following code:

- Set the ListView's [ItemsSource](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemscontrol.itemssource) property to `contactsFiltered`.
- Handle the [TextChanged](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.textbox.textchanged) event (`OnFilterChanged`) for the `FilterByLastName` TextBox. Inside this event handler function, filter the data.
- To filter the data, access the user-entered filtering term through the `FilterByLastName.Text` property. Use a [Linq](/dotnet/csharp/programming-guide/concepts/linq/introduction-to-linq-queries) query to select the items in `allContacts` where the last name contains the term in `FilterByLastName.Text`, and add those matching items into a collection called `filtered`.
- Compare the current `contactsFiltered` collection with the newly filtered items in `filtered`, removing and adding items in `contactsFiltered` where necessary to make it match `filtered`.
- As items are removed and added in `contactsFiltered`, the ListView updates and animates accordingly.

 ```csharp
using System.Linq;

public sealed partial class MainPage : Page
{
// Define Contact collection to hold all Contact objects.
IList<Contact> allContacts = new List<Contact>();
// Define an ObservableCollection<Contact> object to serve as the ListView's
// ItemsSource. This collection will get updated after the filters are used:
ObservableCollection<Contact> contactsFiltered;

public MainPage()
{
    this.InitializeComponent();

    // Populate allContacts collection.
    allContacts.Add(new Contact("Kendall", "Collins", "Adatum Corporation"));
    allContacts.Add(new Contact("Victoria", "Burke", "Bellows College"));
    allContacts.Add(new Contact("Preston", "Morales", "Margie's Travel"));
    allContacts.Add(new Contact("Miguel", "Reyes", "Tailspin Toys"));

    // Populate contactsFiltered with all Contact objects (in this case,
    // allContacts holds all of our Contact objects so we copy them into
    // contactsFiltered). Set this newly populated collection as the
    // ItemsSource for the ListView.
    contactsFiltered = new ObservableCollection<Contact>(allContacts);
    Filtereditemscontrol.itemssource = contactsFiltered;
}

// Whenever text changes in the filtering text box, this function is called:
private void OnFilterChanged(object sender, TextChangedEventArgs args)
{
    // This is a Linq query that selects only items that return true after
    // being passed through the Filter function, and adds all of those
    // selected items to filtered.
    var filtered = allContacts.Where(contact => Filter(contact));
    Remove_NonMatching(filtered);
    AddBack_Contacts(filtered);
}

// The following functions are called inside OnFilterChanged:

// When the text in any filter is changed, perform a check on each item in
// the original contact list to see if the item should be displayed. If the
// item passes the check, the function returns true and the item is added to
// the filtered list. Make sure all text is case-insensitive when comparing.
private bool Filter(Contact contact)
{
    return contact.LastName.Contains
        (FilterByLastName.Text, StringComparison.InvariantCultureIgnoreCase);
}

// These functions go through the current list being displayed
// (contactsFiltered), and remove any items not in the filtered collection
// (any items that don't belong), or add back any items from the original
// allContacts list that are now supposed to be displayed. (Adding/removing
// the items ensures the list view uses the desired add/remove animations.)

private void Remove_NonMatching(IEnumerable<Contact> filteredData)
{
    for (int i = contactsFiltered.Count - 1; i >= 0; i--)
    {
        var item = contactsFiltered[i];
        // If contact is not in the filtered argument list,
        // remove it from the ListView's source.
        if (!filteredData.Contains(item))
        {
            contactsFiltered.Remove(item);
        }
    }
}

private void AddBack_Contacts(IEnumerable<Contact> filteredData)
{
    foreach (var item in filteredData)
    {
        // If the item in the filtered list is not currently in
        // the ListView's source collection, add it back in.
        if (!contactsFiltered.Contains(item))
        {
            contactsFiltered.Add(item);
        }
    }
}
}
 ```

Now, as the user types in their filtering string in the `FilterByLastName` TextBox, the ListView immediately updates to show only the people whose last name contains the filtering string.

## Get the sample code

> [!div class="nextstepaction"]
> [Open the WinUI 3 Gallery app and see the ListView in action](winui3gallery:/item/ItemsView).

[!INCLUDE [winui-3-gallery](../../../includes/winui-3-gallery.md)]

> For UWP: [!INCLUDE [winui-2-gallery](../../../includes/winui-2-gallery.md)]


## Related articles

- [Lists](lists.md)
- [Items view](itemsview.md)
- [List view and grid view](listview-and-gridview.md)
- [Collection commanding](collection-commanding.md)
