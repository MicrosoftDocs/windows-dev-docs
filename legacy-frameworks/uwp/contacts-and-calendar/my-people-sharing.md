---
title: My People sharing
description: Use My People sharing to let users pin contacts to their taskbar and stay in touch easily from anywhere in Windows.
ms.date: 01/04/2024
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# My People sharing

> [!IMPORTANT]
> My people is no longer supported in Windows 11 and Windows 10 versions with KB5034203 applied.

The My People feature allows users to pin contacts to their taskbar, enabling them to stay in touch easily from anywhere in Windows, no matter what application they are connected by. Now users can share content with their pinned contacts by dragging files from the File Explorer to their My People pin. They can also share to any contacts in the Windows contact store via the standard share charm. Keep reading to learn how to enable your application as a My People sharing target.

![My people sharing panel](images/my-people-sharing.png)

## Requirements

+ Windows 10 and Microsoft Visual Studio 2019. For installation details, see [Get set up with Visual Studio](/windows/apps/get-started/get-set-up).
+ Basic knowledge of C# or a similar object-oriented programming language. To get started with C#, see [Create a "Hello, world" app](../get-started/create-a-hello-world-app-xaml-universal.md).

## Overview

There are three steps you must take to enable your application as a My People sharing target:

1. [Declare support for the shareTarget activation contract in your application manifest.](#declaring-support-for-the-share-contract)
2. [Annotate the contacts that the users can share to using your app.](#annotating-contacts)
3. Support multiple instances of the application running at the same time.  Users must be able to interact with a full version of your application while also using it to share with others. They may use it in multiple share windows at once. To support this, your application needs to be able to run multiple views simultaneously. To learn how to do this, see the article ["show multiple views for an app"](/windows/apps/design/layout/show-multiple-views).

When you’ve done this, your application will appear as a share target in the My People share window, which can be launched in two ways:
1. A contact is chosen via the share charm.
2. File(s) are dragged and dropped on a contact pinned to the taskbar.

## Declaring support for the share contract

To declare support for your application as a share target, first open your application in Visual Studio. From the **Solution Explorer**, right click **Package.appxmanifest** and select **Open With**. From the menu, select **XML (Text) Editor** and click **OK**. Then, make the following changes to the manifest:


**Before**
```xml
<Applications>
	<Application Id="MyApp"
	  Executable="$targetnametoken$.exe"
	  EntryPoint="My.App">
	</Application>
</Applications>
```

**After**

```xml
<Applications>
	<Application Id="MyApp"
	  Executable="$targetnametoken$.exe"
	  EntryPoint="My.App">
		<Extensions>
			<uap:Extension Category="windows.shareTarget">
				<uap:ShareTarget Description="Share with MyApp">
					<uap:SupportedFileTypes>
						<uap:SupportsAnyFileType/>
					</uap:SupportedFileTypes>
					<uap:DataFormat>Text</uap:DataFormat>
					<uap:DataFormat>Bitmap</uap:DataFormat>
					<uap:DataFormat>Html</uap:DataFormat>
					<uap:DataFormat>StorageItems</uap:DataFormat>
					<uap:DataFormat>URI</uap:DataFormat>
				</uap:ShareTarget>
			</uap:Extension>
		 </Extensions>
	</Application>
</Applications>
```

This code adds support for all files and data formats, but you can choose to specify what files types and data formats are supported (see [ShareTarget class documentation](/uwp/schemas/appxpackage/appxmanifestschema/element-sharetarget) for more details).

## Annotating contacts

To allow the My People share window to show your application as a share target for your contacts, you need to write them to the Windows contact store. To learn how to write contacts, see the [Contact Card Integration sample](https://github.com/Microsoft/Windows-universal-samples/tree/6370138b150ca8a34ff86de376ab6408c5587f5d/Samples/ContactCardIntegration). 

For your application to appear as a My People share target when sharing to a contact, it must write an annotation to that contact. Annotations are pieces of data from your application that are associated with a contact. The annotation must contain the activatable class corresponding to your desired view in its **ProviderProperties** member, and declare support for the **Share** operation.

You can annotate contacts at any point while your app is running, but generally you should annotate contacts as soon as they are added to the Windows contact store.

```Csharp
if (ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 5))
{
	// Create a new contact annotation
	ContactAnnotation annotation = new ContactAnnotation();
	annotation.ContactId = myContact.Id;

	// Add appId and Share support to the annotation
	String appId = "MyApp_vqvv5s4y3scbg!App";
	annotation.ProviderProperties.Add("ContactShareAppID", appId);
	annotation.SupportedOperations = ContactAnnotationOperations::Share;

	// Save annotation to contact annotation list
	// Windows.ApplicationModel.Contacts.ContactAnnotationList 
	await contactAnnotationList.TrySaveAnnotationAsync(annotation);
}
```

The “appId” is the Package Family Name, followed by ‘!’ and the Activatable Class ID. To find your Package Family Name, open **Package.appxmanifest** using the default editor, and look in the “Packaging” tab. Here, “App” is the Activatable Class corresponding to the Share Target view.

## Running as a My People share target

Finally, to run the app, override the [OnShareTargetActivated](/uwp/api/Windows.UI.Xaml.Application#Windows_UI_Xaml_Application_OnShareTargetActivated_Windows_ApplicationModel_Activation_ShareTargetActivatedEventArgs_) method in your app’s main class to handle the share target activation. The [ShareTargetActivatedEventArgs.ShareOperation.Contacts](/uwp/api/windows.applicationmodel.datatransfer.sharetarget.shareoperation#Properties) property will contain the contact(s) that are being shared to, or will be empty if this is a standard share operation (not a My People share).

```Csharp
protected override void OnShareTargetActivated(ShareTargetActivatedEventArgs args)
{
	bool isPeopleShare = false;
	if (ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 5))
	{
		// Make sure the current OS version includes the My People feature before
		// accessing the ShareOperation.Contacts property
		isPeopleShare = (args.ShareOperation.Contacts.Count > 0);
	}

	if (isPeopleShare)
	{
  		// Show share UI for MyPeople contact(s)
	}
	else
	{
		// Show standard share UI for unpinned contacts
	}
}
```

## See also
+ [Adding My People support](my-people-support.md)
+ [ShareTarget Class](/uwp/schemas/appxpackage/appxmanifestschema/element-sharetarget)
+ [Contact Card Integration sample](https://github.com/Microsoft/Windows-universal-samples/tree/6370138b150ca8a34ff86de376ab6408c5587f5d/Samples/ContactCardIntegration)