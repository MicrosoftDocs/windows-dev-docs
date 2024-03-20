---
description: Filter the items in your collection through user input.
title: Filtering collections
label: Filtering collections
template: detail.hbs
ms.date: 3/20/2024
ms.topic: article
keywords: windows 10, uwp
pm-contact: anawish
---

# Filtering collections and lists through user input
If your collection displays many items or is heavily tied to user interaction, filtering is a useful feature to implement. Filtering using the method described in this article can be implemented to most collection controls, including [ListView](/windows/windows-app-sdk/api/winrt/microsoft.UI.Xaml.Controls.ListView), [GridView](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.gridview), and [ItemsRepeater](/uwp/api/microsoft.ui.xaml.controls.itemsrepeater?view=winui-2.2&preserve-view=true). Many types of user input can be used to filter a collection - such as checkboxes, radio buttons, and sliders - but this article will be focusing on taking text-based user input and using it to update a ListView in real time, according to the user's search. 

> [!NOTE]
> This article will focus on filtering with a ListView. Please be aware that the filtering method can also be applied to other collections controls such as GridView, ItemsRepeater, or TreeView.

## Setting up the UI and XAML for filtering
To implement filtering, your app should have a ListView should appear alongside a TextBox or other control that allows for user input. The text that the user types into the TextBox will be used as the filter, i.e. only results containing their text input/search query will appear. As the user types into the TextBox, the ListView will constantly update with filtered results - specifically, everytime the text in the TextBox changes, even if by one letter, the ListView will go through its items and filter with that term.

The code below shows a UI with a simple ListView and its DataTemplate, along with an accompanying TextBox. In this example, the ListView displays a collection of Person objects. Person is a class defined in the code-behind (not shown in code sample below), and each Person object has the following properties: FirstName, LastName, and Company.

Using the TextBox, users can type a search/filtering term to filter the list of Person objects by last name. Note that the TextBox is bound to a specific name (`FilterByLName`) and has its own TextChanged event (`FilteredLV_LNameChanged`). The bound name allows us to access the TextBox's content/text in the code-behind, and the TextChanged event will fire whenever the user types in the TextBox, allowing us to perform a filtering operation upon recieving user input. 

For filtering to work, the ListView must have a data source that can be manipulated in the code-behind, such as an `ObservableCollection<>`. In this case, the ListView's ItemsSource property is assigned to an `ObservableCollection<Person>` in the code-behind. 

```xaml
<Grid>
    <Grid.ColumnDefinitions>
        <ColumnDefinition Width="1*"></ColumnDefinition>
        <ColumnDefinition Width="1*"></ColumnDefinition>
    </Grid.ColumnDefinitions>
    <Grid.RowDefinitions>
            <RowDefinition Height="400"></RowDefinition>
            <RowDefinition Height="400"></RowDefinition>
    </Grid.RowDefinitions>

    <ListView x:Name="FilteredListView"
                Grid.Column="0"
                Margin="0,0,20,0">

        <ListView.ItemTemplate>
            <DataTemplate x:DataType="local:Person">
                <StackPanel>
                    <TextBlock Style="{ThemeResource BaseTextBlockStyle}" Margin="0,5,0,5">
                        <Run Text="{x:Bind FirstName}"></Run>
                        <Run Text="{x:Bind LastName}"></Run>
                    </TextBlock>
                    <TextBlock Style="{ThemeResource BodyTextBlockStyle}" Margin="0,5,0,5" Text="{x:Bind Company}"/>
                </StackPanel>
            </DataTemplate>
        </ListView.ItemTemplate>

    </ListView>

    <TextBox x:Name="FilterByLName" Grid.Column="1" Header="Last Name" Width="200"
             HorizontalAlignment="Left" VerticalAlignment="Top" Margin="0,0,0,20"
             TextChanged="FilteredLV_LNameChanged"/>
</Grid>
```
## Filtering the data
[Linq](/dotnet/csharp/programming-guide/concepts/linq/introduction-to-linq-queries) queries allow you to group, order, and select certain items in a collection. For filtering a list, we will be constructing a Linq query that only selects terms that match the user-inputted search query/filtering term, entered in the `FilterByLName` TextBox. The query result can be assigned to an [IEnumerable\<T>](/dotnet/api/system.collections.generic.ienumerable-1) collection object. Once we have this collection, we can use it to compare with the original list, removing items that don't match and adding back items that do match (in case of a backspace).

> [!NOTE]
> In order for the ListView to animate in the most intuitive way when adding and subtracting items, it's important to remove and add items to the ListView's ItemsSource collection itself, rather than create a new collection of filtered objects and assign that to the ListView's ItemsSource property.

To start, we'll need to initialize our original data source in a separate collection, such as a `List<T>` or `ObservableCollection<T>`. In this example, we have an `List<Person>` called `People`, that holds all of the Person objects shown in the ListView (population/initialization of this List is not shown in the code snippet below). We'll also need a list to hold the filtered data, which will constantly change every time a filter is applied. This will be an `ObservableCollection<Person>` called `PeopleFiltered`, and at initialization will have the same contents as `People`.
 
The code below performs the filtering operation through the following steps, shown in the code below:
 - Set the ListView's ItemsSource property to `PeopledFiltered`. 
 - Define the TextChanged event, `FilteredLV_LNameChanged()`, for the `FilterByLName` TextBox. Inside this function, filter the data.
 - To filter the data, access the user-inputted search query/filtering term through `FilterByLName.Text`. Use a Linq query to select the items in `People` whose last name contains the term `FilterByLName.Text`, and add those matching items into a collection called `TempFiltered`.
 - Compare the current `PeopleFiltered` collection with the newly filtered items in `TempFiltered`, removing and adding items from `PeopleFiltered` where necessary.
 - As items are removed and added from `PeopleFiltered`, the ListView will update and animate accordingly.

 ```csharp
using System.Linq;

IList<Person> People;
ObservableCollection<Person> PeopleFiltered;

public MainPage()
{
    // Define People collection to hold all Person objects. 
    // Populate collection - i.e. add Person objects (not shown)
    People = new List<Person>();

    // Create PeopleFiltered collection and copy data from original People collection
    PeopleFiltered = new ObservableCollection<Person>(People);

    // Set the ListView's ItemsSource property to the PeopleFiltered collection
    FilteredListView.ItemsSource = PeopleFiltered;

    // ... 
}

private void FilteredLV_LNameChanged(object sender, TextChangedEventArgs e)
{
    /* Perform a Linq query to find all Person objects (from the original People collection)
    that fit the criteria of the filter, save them in a new List called TempFiltered. */
    List<Person> TempFiltered;
    
    /* Make sure all text is case-insensitive when comparing, and make sure 
    the filtered items are in a List object */
    TempFiltered = People.Where(contact => contact.LastName.Contains(FilterByLName.Text, StringComparison.InvariantCultureIgnoreCase)).ToList();
    
    /* Go through TempFiltered and compare it with the current PeopleFiltered collection,
    adding and subtracting items as necessary: */

    // First, remove any Person objects in PeopleFiltered that are not in TempFiltered
    for (int i = PeopleFiltered.Count - 1; i >= 0; i--)
    {
        var item = PeopleFiltered[i];
        if (!TempFiltered.Contains(item))
        {
            PeopleFiltered.Remove(item);
        }
    }

    /* Next, add back any Person objects that are included in TempFiltered and may 
    not currently be in PeopleFiltered (in case of a backspace) */

    foreach (var item in TempFiltered)
    {
        if (!PeopleFiltered.Contains(item))
        {
            PeopleFiltered.Add(item);
        }
    }
}
 ```

Now, as the user types in their filtering terms in the `FilterByLName` TextBox, the ListView will immediately update to only show the people whose last name contains the filtering term.

## Next steps

### Get the sample code
> - [Open the WinUI 2 Gallery app and see the ListView](winui2gallery:/item/ListView) in action. [!INCLUDE [winui-2-gallery](../../../includes/winui-2-gallery.md)]
- Get the [WinUI 2 Gallery app (Microsoft Store)](https://www.microsoft.com/store/productId/9MSVH128X2ZT).

### Related articles
- [Lists](lists.md)
- [List view and grid view](listview-and-gridview.md)
- [Collection commanding](collection-commanding.md)
