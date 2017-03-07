---
author: awkoren
Description: In addition to the normal APIs available to all UWP apps, there are some extensions and APIs available only to converted desktop apps. This article describes these extensions and how to use them.
Search.Product: eADQiWindows 10XVcnh
title: Desktop to UWP Bridge App Extensions
ms.author: alkoren
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.assetid: 0a8cedac-172a-4efd-8b6b-67fd3667df34
---

# Desktop to UWP Bridge: App extensions

You can enhance your converted Desktop application with a wide range of Universal Windows Platform (UWP) APIs. However, in addition to the normal APIs available to all UWP apps, there are some extensions and APIs available only to converted desktop apps. These features focus on scenarios such as launching a process when the user logs on and File Explorer integration, and are designed to smooth the transition between the original desktop app and the converted app package.

This article describes these extensions and how to use them. Most require manual modification of your converted app's manifest file, which contains declarations about the extensions your app makes use of. To edit the manifest, right-click the **Package.appxmanifest** file in your Visual Studio solution and select *View Code*. 

## Manifest XML namespaces 

App extensions for the Desktop Bridge require several different XML namespaces. These namespaces should be defined in the `<Package>` element at the root of your manifest file, like this: 

```xml
<Package
  ...
  xmlns:uap2="http://schemas.microsoft.com/appx/manifest/uap/windows10/2"
  xmlns:uap3="http://schemas.microsoft.com/appx/manifest/uap/windows10/3"
  xmlns:desktop="http://schemas.microsoft.com/appx/manifest/desktop/windows10"
  ...
  IgnorableNamespaces="uap uap2 uap3 mp rescap desktop">
```

If your manifest file is emitting compiler errors, verify you are including all the required namespaces and that child XML elements are prefixed with the correct namespace. You can also consult a complete, working manifest file in the [Desktop Bridge Samples repository on GitHub](https://github.com/Microsoft/DesktopBridgeToUWP-Samples). 

## Startup tasks

Startup tasks allow your app to run an executable automatically whenever a user logs on. 

To declare a startup task, add the following to your app's manifest: 

```XML
<desktop:Extension Category="windows.startupTask" Executable="bin\MyStartupTask.exe" EntryPoint="Windows.FullTrustApplication">
	<desktop:StartupTask TaskId="MyStartupTask" Enabled="true" DisplayName="My App Service" />
</desktop:Extension>
```
- *Extension Category* should always have the value "windows.startupTask ".
- *Extension Executable* is the relative path to the .exe to start.
- *Extension EntryPoint* should always have the value "Windows.FullTrustApplication".
- *StartupTask TaskId* is a unique identifier for your task. Using this identifier, your app can call the APIs in the [**Windows.ApplicationModel.StartupTask**](https://docs.microsoft.com/uwp/api/Windows.ApplicationModel.StartupTask) class to programmatically enable or disable a startup task.
- *StartupTask Enabled* indicates whether the task first starts enabled or disabled. Enabled tasks will run the next time the user logs on (unless the user disables it). 
- *StartupTask DisplayName* is the name of the task that appears in Task Manager. This string is localizable using ```ms-resource```. 

Apps can declare multiple startup tasks; each will fire and run independently. All startup tasks will appear in Task Manager under the **Startup** tab with the name specified in your app's manifest and your app's icon. Task Manager will automatically analyze the startup impact of your tasks. Users can opt to manually disable your app's startup task using Task Manager; if a user disables a task, you cannot programmatically re-enable it.

## App execution alias

An app execution alias allows you to specify a keyword name for your app. Users or other processes can use this keyword to easily launch your app as though it were in the PATH variable - from Run or a command prompt, for instance - without providing the full path. For example, if you declare the alias "Foo," a user can type "Foo Bar.txt" from cmd.exe and your app will be activated with the path to "Bar.txt" as part of the activation event args.

To specify an app execution alias, add the following to your app's manifest: 

```XML 
<uap3:Extension Category="windows.appExecutionAlias" Executable="exes\launcher.exe" EntryPoint="Windows.FullTrustApplication">
	<uap3:AppExecutionAlias>
		<desktop:ExecutionAlias Alias="Foo.exe" />
	</uap3:AppExecutionAlias>
</uap3:Extension>
```

- *Extension Category* should always have the value "windows.appExecutionAlias".
- *Extension Executable* is the relative path to the executable to launch when the alias is invoked.
- *Extension EntryPont* should always have the value "Windows.FullTrustApplication".
- *ExecutionAlias Alias* is the short name for your app. It must always end with the ".exe" extension. 

You can only specify a single app execution alias for each application in the package. If multiple apps register for the same alias, the system will invoke the last one that was registered, so make sure to choose a unique alias other apps are unlikely to override.

## Protocol associations 

Protocol associations enable interop scenarios between your converted app and other programs or system components. When your converted app is launched using a protocol, you can specify specific parameters to pass to its activation event args so it can behave accordingly. Note that parameters are only supported for converted, full-trust apps; UWP apps cannot use parameters.  

To declare a protocol association, add the following to your app's manifest:

```XML
<uap3:Extension Category="windows.protocol">
	<uap3:Protocol Name="myapp-cmd" Parameters="/p &quot;%1&quot;" />
</uap3:Extension>
```

- *Extension Category* should always have the value "windows.protocol". 
- *Protocol Name* is the name of the protocol. 
- *Protocol Parameters* is the list of parameters and values to pass to your app as event args when it is activated. Note that if a variable can contain a file path, you should wrap the value in quotes so it will not break if passed a path that includes spaces.

## Files and File Explorer integration

Converted apps have a variety of options for registering to handle certain file types and integrating into File Explorer. This allows users to easily access your app as part of their normal workflow.

To get started, first add the following to your app's manifest: 

```XML
<uap3:Extension Category="windows.fileTypeAssociation">
	<uap3:FileTypeAssociation Name="myapp">
		... additional elements here ...
	</uap3:FileTypeAssociation>
</uap3:Extension>
```

- *Extension Category* should always have the value "windows.fileTypeAssociation". 
- *FileTypeAssociation Name* is a unique Id. This Id is used internally to generate a hashed ProgID associated with your file type association. You can use this Id to manage changes in future versions of your app. For example, if you want to change the icon for a file extension, you can move it a new FileTypeAssociation with a different name.  

Next, add additional child elements to this entry based on the specific features you need. The available options are described below.

### Supported file types

Your app can specify it supports opening specific types of files. If a user right-clicks a file and selects "Open With," your app will appear in the list of suggestions.

Example:

```XML
<uap:SupportedFileTypes>
	<uap:FileType>.txt</uap:FileType>
	<uap:FileType>.avi</uap:FileType>
</uap:SupportedFileTypes>
```

- *FileType* is the extension your app supports.

### File context menu verbs 

Users normally open files by simply double-clicking them. However, when a user right-clicks a file, the context menu presents them with various options (known as "Verbs") that provide additional detail on how they wish to interact with the file, such as "Open", "Edit", "Preview," or "Print." 

Specifying a supported file type automatically adds the “Open” verb. However, apps can also add additional custom verbs to the File Explorer context menu. These allow the app to launch a certain way based on the user's selection when opening a file.

Example: 

```XML
<uap2:SupportedVerbs>
	<uap3:Verb Id="Edit" Parameters="/e &quot;%1&quot;">Edit</uap3:Verb>
	<uap3:Verb Id="Print" Extended="true" Parameters="/p &quot;%1&quot;">Print</uap3:Verb>
</uap2:SupportedVerbs>
```

- *Verb Id* is a unique Id of the verb. If your app is a UWP app, this is passed to your app as part of its activation event args so it can handle the user’s selection appropriately. If your app is a full-trust converted app, it receives parameters instead (see the next bullet). 
- *Verb Parameters* is the list of argument parameters and values associated with the verb. If your app is a full-trust converted app, these are passed to it as event args when it’s activated so you can customize its behavior for different activation verbs. If a variable can contain a file path, you should wrap the value in quotes so it will not break if passed a path that includes spaces. Note that if your app is a UWP app, you can’t pass parameters – it receives the Id instead (see the previous bullet). 
- *Verb Extended* specifies that the verb should only appear if the user holds the **Shift** key before right-clicking the file to show the context menu. This attribute is optional and defaults to *False* (e.g., always show the verb) if not listed. You specify this behavior individually for each verb (except for "Open," which is always *False*). 
- *Verb* is the name to display in the File Explorer context menu. This string is localizable using ```ms-resource```.

### Shell context menu verbs

Adding items to the shell's folder context menu is not currently supported. 

### Multiple Selection Model

Multiple selections allow you to specify how your app handles a user opening multiple files with it simultaneously (for example, by selecting 10 files in File Explorer and tapping "Open").

Converted desktop apps have the same three options as regular desktop apps. 
- *Player*: Your app is activated once with all of the selected files passed as argument parameters.
- *Single*: Your app is activated once for the first selected file. Other files are ignored. 
- *Document*: A new, separate instance of your app is activated for each selected file.

You can set different preferences for different file types and actions. For example, you may wish to open *Documents* in *Document* mode and *Images* in *Player* mode.

To set your app's behavior, add the *MultiSelectModel* attribute to elements in your manifest that are related to file types and file launching. 

Setting a model for a supported file type: 

```XML
<uap3:FileTypeAssociation Name="myapp" MultiSelectModel="Document">
	<uap:SupportedFileTypes>
		<uap:FileType>.txt</uap:FileType>
</uap:SupportedFileTypes>
```

Setting a model for verbs:

```XML
<uap3:Verb Id="Edit" MultiSelectModel="Player">Edit</uap3:Verb>
<uap3:Verb Id="Preview" MultiSelectModel="Document">Preview</uap3:Verb>
```

If your app doesn't specify a choice for multi-selection, the default is *Player* if the user is opening 15 or fewer files. Otherwise, if your app is a converted app, the default is *Document*. UWP apps are always launched as *Player*. 

### Complete example

The following is a complete example that integrates many of the file and File Explorer related elements described above: 

```XML
<uap3:Extension Category="windows.fileTypeAssociation">
    <uap3:FileTypeAssociation Name="myapp" MultiSelectModel="Document">
        <uap:SupportedFileTypes>
    		<uap:FileType>.txt</uap:FileType>
    		<uap:FileType>.foo</uap:FileType>
	</uap:SupportedFileTypes>
	<uap2:SupportedVerbs>
    		<uap3:Verb Id="Edit" Parameters="/e &quot;%1&quot;">Edit</uap3:Verb>
    		<uap3:Verb Id="Print" Parameters="/p &quot;%1&quot;">Print</uap3:Verb>
	</uap2:SupportedVerbs>
	<uap:Logo>Assets\MyExtensionLogo.png</uap:Logo>
    </uap3:FileTypeAssociation>
</uap3:Extension>
```

## See also

- [App package manifest](https://msdn.microsoft.com/library/windows/apps/br211474.aspx)