---
description: Through the Windows.ApplicationModel.Contacts namespace, you have several options for selecting contacts.
title: Select contacts
ms.assetid: 35FEDEE6-2B0E-4391-84BA-5E9191D4E442
keywords: contacts, selecting select single contact select multiple contacts contacts, select multiple select specific contact data contact, selecting specific data contact, selecting specific fields
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Select contacts



Through the [**Windows.ApplicationModel.Contacts**](/uwp/api/Windows.ApplicationModel.Contacts) namespace, you have several options for selecting contacts. Here, we'll show you how to select a single contact or multiple contacts, and we'll show you how to configure the contact picker to retrieve only the contact information that your app needs.

## Set up the contact picker

Create an instance of [**Windows.ApplicationModel.Contacts.ContactPicker**](/uwp/api/Windows.ApplicationModel.Contacts.ContactPicker) and assign it to a variable.

```cs
var contactPicker = new Windows.ApplicationModel.Contacts.ContactPicker();
```

## Set the selection mode (optional)

By default, the contact picker retrieves all of the available data for the contacts that the user selects. The [**SelectionMode**](/uwp/api/windows.applicationmodel.contacts.contactpicker.selectionmode) property lets you configure the contact picker to retrieve only the data fields that your app needs. This is a more efficient way to use the contact picker if you only need a subset of the available contact data.

First, set the [**SelectionMode**](/uwp/api/windows.applicationmodel.contacts.contactpicker.selectionmode) property to **Fields**:

```cs
contactPicker.SelectionMode = Windows.ApplicationModel.Contacts.ContactSelectionMode.Fields;
```

Then, use the [**DesiredFieldsWithContactFieldType**](/uwp/api/windows.applicationmodel.contacts.contactpicker.desiredfieldswithcontactfieldtype) property to specify the fields that you want the contact picker to retrieve. This example configures the contact picker to retrieve email addresses:

``` cs
contactPicker.DesiredFieldsWithContactFieldType.Add(Windows.ApplicationModel.Contacts.ContactFieldType.Email);
```

## Launch the picker

```cs
Contact contact = await contactPicker.PickContactAsync();
```

Use [**PickContactsAsync**](/uwp/api/windows.applicationmodel.contacts.contactpicker.pickcontactsasync) if you want the user to select one or more contacts.

```cs
public IList<Contact> contacts;
contacts = await contactPicker.PickContactsAsync();
```

## Process the contacts

When the picker returns, check whether the user has selected any contacts. If so, process the contact information.

This example shows how to processes a single contact. Here we retrieve the contact's name and copy it into a [**TextBlock**](/uwp/api/Windows.UI.Xaml.Controls.TextBlock) control called *OutputName*.

```cs
if (contact != null)
{
    OutputName.Text = contact.DisplayName;
}
else
{
    rootPage.NotifyUser("No contact was selected.", NotifyType.ErrorMessage);
}
```

This example shows how to process multiple contacts.

```cs
if (contacts != null && contacts.Count > 0)
{
    foreach (Contact contact in contacts)
    {
        // Do something with the contact information.
    }
}
```

## Complete example (single contact)

This example uses the contact picker to retrieve a single contact's name along with an email address, location or phone number.

```cs
// ...
using Windows.ApplicationModel.Contacts;
// ...

private async void PickAContactButton-Click(object sender, RoutedEventArgs e)
{
    ContactPicker contactPicker = new ContactPicker();

    contactPicker.DesiredFieldsWithContactFieldType.Add(ContactFieldType.Email);
    contactPicker.DesiredFieldsWithContactFieldType.Add(ContactFieldType.Address);
    contactPicker.DesiredFieldsWithContactFieldType.Add(ContactFieldType.PhoneNumber);

    Contact contact = await contactPicker.PickContactAsync();

    if (contact != null)
    {
        OutputFields.Visibility = Visibility.Visible;
        OutputEmpty.Visibility = Visibility.Collapsed;

        OutputName.Text = contact.DisplayName;

        AppendContactFieldValues(OutputEmails, contact.Emails);
        AppendContactFieldValues(OutputPhoneNumbers, contact.Phones);
        AppendContactFieldValues(OutputAddresses, contact.Addresses);
    }
    else
    {
        OutputEmpty.Visibility = Visibility.Visible;
        OutputFields.Visibility = Visibility.Collapsed;
    }
}

private void AppendContactFieldValues<T>(TextBlock content, IList<T> fields)
{
    if (fields.Count > 0)
    {
        StringBuilder output = new StringBuilder();

        if (fields[0].GetType() == typeof(ContactEmail))
        {
            foreach (ContactEmail email in fields as IList<ContactEmail>)
            {
                output.AppendFormat("Email: {0} ({1})\n", email.Address, email.Kind);
            }
        }
        else if (fields[0].GetType() == typeof(ContactPhone))
        {
            foreach (ContactPhone phone in fields as IList<ContactPhone>)
            {
                output.AppendFormat("Phone: {0} ({1})\n", phone.Number, phone.Kind);
            }
        }
        else if (fields[0].GetType() == typeof(ContactAddress))
        {
            List<String> addressParts = null;
            string unstructuredAddress = "";

            foreach (ContactAddress address in fields as IList<ContactAddress>)
            {
                addressParts = (new List<string> { address.StreetAddress, address.Locality, address.Region, address.PostalCode });
                unstructuredAddress = string.Join(", ", addressParts.FindAll(s => !string.IsNullOrEmpty(s)));
                output.AppendFormat("Address: {0} ({1})\n", unstructuredAddress, address.Kind);
            }
        }

        content.Visibility = Visibility.Visible;
        content.Text = output.ToString();
    }
    else
    {
        content.Visibility = Visibility.Collapsed;
    }
}
```

## Complete example (multiple contacts)

This example uses the contact picker to retrieve multiple contacts and then adds the contacts to a [**ListView**](/uwp/api/Windows.UI.Xaml.Controls.ListView) control called `OutputContacts`.

```cs
MainPage rootPage = MainPage.Current;
public IList<Contact> contacts;

private async void PickContactsButton-Click(object sender, RoutedEventArgs e)
{
    var contactPicker = new Windows.ApplicationModel.Contacts.ContactPicker();
    contactPicker.CommitButtonText = "Select";
    contacts = await contactPicker.PickContactsAsync();

    // Clear the ListView.
    OutputContacts.Items.Clear();

    if (contacts != null && contacts.Count > 0)
    {
        OutputContacts.Visibility = Windows.UI.Xaml.Visibility.Visible;
        OutputEmpty.Visibility = Visibility.Collapsed;

        foreach (Contact contact in contacts)
        {
            // Add the contacts to the ListView.
            OutputContacts.Items.Add(new ContactItemAdapter(contact));
        }
    }
    else
    {
        OutputEmpty.Visibility = Visibility.Visible;
    }         
}
```

``` cs
public class ContactItemAdapter
{
    public string Name { get; private set; }
    public string SecondaryText { get; private set; }

    public ContactItemAdapter(Contact contact)
    {
        Name = contact.DisplayName;
        if (contact.Emails.Count > 0)
        {
            SecondaryText = contact.Emails[0].Address;
        }
        else if (contact.Phones.Count > 0)
        {
            SecondaryText = contact.Phones[0].Number;
        }
        else if (contact.Addresses.Count > 0)
        {
            List<string> addressParts = (new List<string> { contact.Addresses[0].StreetAddress,
              contact.Addresses[0].Locality, contact.Addresses[0].Region, contact.Addresses[0].PostalCode });
              string unstructuredAddress = string.Join(", ", addressParts.FindAll(s => !string.IsNullOrEmpty(s)));
            SecondaryText = unstructuredAddress;
        }
    }
}
```

## Summary and next steps

Now you have a basic understanding of how to use the contact picker to retrieve contact information. Download the [Universal Windows app samples](https://github.com/Microsoft/Windows-universal-samples) from GitHub to see more examples of how to use contacts and the contact picker.