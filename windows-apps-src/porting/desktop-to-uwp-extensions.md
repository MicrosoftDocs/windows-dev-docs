---
author: normesta
Description: You can use extensions to integrate your packaged desktop app with Windows 10 in predefined ways.
Search.Product: eADQiWindows 10XVcnh
title: Integrate your app with Windows 10 (Desktop Bridge)
ms.author: normesta
ms.date: 04/18/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.assetid: 0a8cedac-172a-4efd-8b6b-67fd3667df34
ms.localizationpriority: medium
---

# Integrate your app with Windows 10 (Desktop Bridge)

Use extensions to integrate your app with Windows 10 in predefined ways.

For example, use an extension to create a firewall exception, make your app the default app for a file type, or point start tiles to the packaged version of your app. To use an extension, just add some XML to your app's package manifest file. No code is required.

This topic describes these extensions and the tasks that you can perform by using them.

## Transition users to your app

Help users transition to your packaged app.

* [Point existing Start tiles and taskbar buttons to your packaged app](#point)
* [Make your packaged app open files instead of your desktop app](#make)
* [Associate your packaged app with a set of file types](#associate)
* [Add options to the context menus of files that have a certain file type](#add)
* [Open certain types of files directly by using a URL](#open)

<a id="point" />

### Point existing Start tiles and taskbar buttons to your packaged app

Your users might have pinned your desktop application to the taskbar or the Start menu. You can point those shortcuts to your new packaged app.

#### XML namespace

http://schemas.microsoft.com/appx/manifest/foundation/windows10/restrictedcapabilities/3

#### Elements and attributes of this extension

```XML
<Extension Category="windows.desktopAppMigration">
    <DesktopAppMigration>
        <DesktopApp AumId="[your_app_aumid]" />
        <DesktopApp ShortcutPath="[path]" />
    </DesktopAppMigration>
</Extension>

```

Find the complete schema reference [here](https://docs.microsoft.com/uwp/schemas/appxpackage/uapmanifestschema/element-rescap3-desktopappmigration).

|Name | Description |
|-------|-------------|
|Category |Always ``windows.desktopAppMigration``.
|AumID |The Application User Model ID of your packaged app. |
|ShortcutPath |The path to .lnk files that start the desktop version of your app. |

#### Example

```XML
<Package
  xmlns:rescap3="http://schemas.microsoft.com/appx/manifest/foundation/windows10/restrictedcapabilities/3"
  IgnorableNamespaces="rescap3">
  <Applications>
    <Application>
      <Extensions>
        <rescap3:Extension Category="windows.desktopAppMigration">
          <rescap3:DesktopAppMigration>
            <rescap3:DesktopApp AumId="[your_app_aumid]" />
            <rescap3:DesktopApp ShortcutPath="%USERPROFILE%\Desktop\[my_app].lnk" />
            <rescap3:DesktopApp ShortcutPath="%APPDATA%\Microsoft\Windows\Start Menu\Programs\[my_app].lnk" />
            <rescap3:DesktopApp ShortcutPath="%PROGRAMDATA%\Microsoft\Windows\Start Menu\Programs\[my_app_folder]\[my_app].lnk"/>
         </rescap3:DesktopAppMigration>
        </rescap3:Extension>
      </Extensions>
    </Application>
  </Applications>
</Package>
```
#### Related sample

[WPF picture viewer with transition/migration/uninstallation](https://github.com/Microsoft/DesktopBridgeToUWP-Samples/tree/master/Samples/DesktopAppTransition)

<a id="make" />

### Make your packaged app open files instead of your desktop app

You can make sure that users open your new packaged app by default for specific types of files instead of opening the desktop version of your app.

To do that, you'll specify the [programmatic identifier (ProgID)](https://msdn.microsoft.com/library/windows/desktop/cc144152.aspx) of each application from which you want to inherit file associations.

#### XML namespaces

* http://schemas.microsoft.com/appx/manifest/uap/windows10/3
* http://schemas.microsoft.com/appx/manifest/foundation/windows10/restrictedcapabilities/3

#### Elements and attributes of this extension

```XML
<Extension Category="windows.fileTypeAssociation">
<FileTypeAssociation Name="[AppID]">
         <MigrationProgIds>
            <MigrationProgId>"[ProgID]"</MigrationProgId>
        </MigrationProgIds>
    </FileTypeAssociation>
</Extension>
```

Find the complete schema reference [here](https://docs.microsoft.com/uwp/schemas/appxpackage/uapmanifestschema/element-uap-filetypeassociation).

|Name |Description |
|-------|-------------|
|Category |Always ``windows.fileTypeAssociation``.
|Name |A unique Id for your app. This Id is used internally to generate a hashed [programmatic identifier (ProgID)](https://msdn.microsoft.com/library/windows/desktop/cc144152.aspx) associated with your file type association. You can use this Id to manage changes in future versions of your app. |
|MigrationProgId |The [programmatic identifier (ProgID)](https://msdn.microsoft.com/library/windows/desktop/cc144152.aspx) that describes the application, component, and version of the desktop app from which you want to inherit file associations.|

#### Example

```XML
<Package
  xmlns:uap3="http://schemas.microsoft.com/appx/manifest/uap/windows10/3"
  xmlns:rescap3="http://schemas.microsoft.com/appx/manifest/foundation/windows10/restrictedcapabilities/3"
  IgnorableNamespaces="uap3, rescap3">
  <Applications>
    <Application>
      <Extensions>
        <uap:Extension Category="windows.fileTypeAssociation">
          <uap3:FileTypeAssociation Name="Contoso">
            <rescap3:MigrationProgIds>
              <rescap3:MigrationProgId>Foo.Bar.1</rescap3:MigrationProgId>
              <rescap3:MigrationProgId>Foo.Bar.2</rescap3:MigrationProgId>
            </rescap3:MigrationProgIds>
          </uap3:FileTypeAssociation>
        </uap:Extension>
      </Extensions>
    </Application>
  </Applications>
</Package>
```
#### Related sample

[WPF picture viewer with transition/migration/uninstallation](https://github.com/Microsoft/DesktopBridgeToUWP-Samples/tree/master/Samples/DesktopAppTransition)

<a id="associate" />

### Associate your packaged app with a set of file types

You can associated your packaged app with file type extensions. If a user right-clicks a file and then selects the **Open with** option, your app appears in the list of suggestions.

#### XML namespace

* http://schemas.microsoft.com/appx/manifest/uap/windows10
* http://schemas.microsoft.com/appx/manifest/uap/windows10/3

#### Elements and attributes of this extension

```XML
<Extension Category="windows.fileTypeAssociation">
    <FileTypeAssociation Name="[AppID]">
        <SupportedFileTypes>
            <FileType>"[file extension]"</FileType>
        </SupportedFileTypes>
    </FileTypeAssociation>
</Extension>
```

Find the complete schema reference [here](https://docs.microsoft.com/uwp/schemas/appxpackage/uapmanifestschema/element-uap-filetypeassociation).

|Name |Description |
|-------|-------------|
|Category |Always ``windows.fileTypeAssociation``.
|Name |A unique Id for your app. This Id is used internally to generate a hashed [programmatic identifier (ProgID)](https://msdn.microsoft.com/library/windows/desktop/cc144152.aspx) associated with your file type association. You can use this Id to manage changes in future versions of your app.   |
|FileType |The file extension supported by your app. |

#### Example

```XML
<Package
  xmlns:uap="http://schemas.microsoft.com/appx/manifest/uap/windows10"
  xmlns:uap3="http://schemas.microsoft.com/appx/manifest/uap/windows10/3"
  IgnorableNamespaces="uap, uap3">
  <Applications>
    <Application>
      <Extensions>
        <uap:Extension Category="windows.fileTypeAssociation">
          <uap3:FileTypeAssociation Name="Contoso">
            <uap:SupportedFileTypes>
      	      <uap:FileType>.txt</uap:FileType>
      	      <uap:FileType>.avi</uap:FileType>
            </uap:SupportedFileTypes>
          </uap3:FileTypeAssociation>
        </uap:Extension>
      </Extensions>
    </Application>
  </Applications>
</Package>
```

#### Related sample

[WPF picture viewer with transition/migration/uninstallation](https://github.com/Microsoft/DesktopBridgeToUWP-Samples/tree/master/Samples/DesktopAppTransition)

<a id="add" />

### Add options to the context menus of files that have a certain file type

In most cases, users double-click files to open them. If users, right click a file, various options appear.

You can add options to that menu. These options give users other ways to interact with your file such as print, edit, or preview the file.

#### XML namespaces

* http://schemas.microsoft.com/appx/manifest/uap/windows10
* http://schemas.microsoft.com/appx/manifest/uap/windows10/2
* http://schemas.microsoft.com/appx/manifest/uap/windows10/3


#### Elements and attributes of this extension

```XML
<Extension Category="windows.fileTypeAssociation">
    <FileTypeAssociation Name="[AppID]">
        <SupportedVerbs>
	          <Verb Id="[ID]" Extended="[Extended]" Parameters="[parameters]">"[verb label]"</Verb>
        </SupportedVerbs>
    </FileTypeAssociation>
</Extension>
```

Find the complete schema reference [here](https://docs.microsoft.com/uwp/schemas/appxpackage/uapmanifestschema/element-uap-filetypeassociation).

|Name |Description |
|-------|-------------|
|Category | Always ``windows.fileTypeAssociation``.
|Name |A unique Id for your app. |
|Verb |The name that appears in the File Explorer context menu. This string is localizable that uses ```ms-resource```.|
|Id |The unique Id of the verb. If your app is a UWP app, this is passed to your app as part of its activation event args so it can handle the user’s selection appropriately. If your app is a full-trust packaged app, it receives parameters instead (see the next bullet). |
|Parameters |The list of argument parameters and values associated with the verb. If your app is a full-trust packaged app, these parameters are passed to the app as event args when the app is activated. You can customize the behavior of your app based on different activation verbs. If a variable can contain a file path, wrap the parameter value in quotes. That will avoid any issues that happen in cases where the path includes spaces. If your app is a UWP app, you can’t pass parameters. The app receives the Id instead (see the previous bullet).|
|Extended |Specifies that the verb appears only if the user shows the context menu by holding the **Shift** key before right-clicking the file. This attribute is optional and defaults to a value of **False** (e.g., always show the verb) if not listed. You specify this behavior individually for each verb (except for "Open," which is always **False**).|

#### Example

```XML
<Package
  xmlns:uap="http://schemas.microsoft.com/appx/manifest/uap/windows10"
  xmlns:uap2="http://schemas.microsoft.com/appx/manifest/uap/windows10/2"
  xmlns:uap3="http://schemas.microsoft.com/appx/manifest/uap/windows10/3"

  IgnorableNamespaces="uap, uap2, uap3">
  <Applications>
    <Application>
      <Extensions>
        <uap:Extension Category="windows.fileTypeAssociation">
          <uap3:FileTypeAssociation Name="Contoso">
            <uap2:SupportedVerbs>
      	      <uap3:Verb Id="Edit" Parameters="/e &quot;%1&quot;">Edit</uap3:Verb>
      	      <uap3:Verb Id="Print" Extended="true" Parameters="/p &quot;%1&quot;">Print</uap3:Verb>
            </uap2:SupportedVerbs>
          </uap3:FileTypeAssociation>
        </uap:Extension>
      </Extensions>
    </Application>
  </Applications>
</Package>
```
#### Related sample

[WPF picture viewer with transition/migration/uninstallation](https://github.com/Microsoft/DesktopBridgeToUWP-Samples/tree/master/Samples/DesktopAppTransition)

<a id="open" />

### Open certain types of files directly by using a URL

You can make sure that users open your new packaged app by default for specific types of files instead of opening the desktop version of your app.

#### XML namespaces

* http://schemas.microsoft.com/appx/manifest/uap/windows10
* http://schemas.microsoft.com/appx/manifest/uap/windows10/3"

#### Elements and attributes of this extension

```XML
<Extension Category="windows.fileTypeAssociation">
    <FileTypeAssociation Name="[AppID]" UseUrl="true" Parameters="%1">
        <SupportedFileTypes>
            <FileType>"[FileExtension]"</FileType>
        </SupportedFileTypes> 
    </FileTypeAssociation>
</Extension>
```

Find the complete schema reference [here](https://docs.microsoft.com/uwp/schemas/appxpackage/uapmanifestschema/element-uap-filetypeassociation).

|Name |Description |
|-------|-------------|
|Category |Always ``windows.fileTypeAssociation``.
|Name |A unique Id for your app. |
|UseUrl |Indicates whether to open files directly from a URL target. If you do not set this value, attempts by your app to open a file by using a URL cause the system to first download the file locally. |
|Parameters |optional parameters. |
|FileType |The relevant file extensions. |

#### Example

```XML
<Package
  xmlns:uap="http://schemas.microsoft.com/appx/manifest/uap/windows10"
  xmlns:uap3="http://schemas.microsoft.com/appx/manifest/uap/windows10/3"
  IgnorableNamespaces="uap, uap3">
  <Applications>
      <Application>
        <Extensions>
          <uap:Extension Category="windows.fileTypeAssociation">
            <uap3:FileTypeAssociation Name="documenttypes" UseUrl="true" Parameters="%1">
              <uap:SupportedFileTypes>
                <uap:FileType>.txt</uap:FileType>
                <uap:FileType>.doc</uap:FileType>
              </uap:SupportedFileTypes> 
            </uap3:FileTypeAssociation>
          </uap:Extension>
        </Extensions>
      </Application>
    </Applications>
</Package>
```

## Perform setup tasks

* [Create firewall exception for your app](#rules)
* [Place your DLL files into any folder of the package](#load-paths)

<a id="rules" />

### Create firewall exception for your app

If your app requires communication through a port, you can add your app to the list of firewall exceptions.

#### XML namespace

http://schemas.microsoft.com/appx/manifest/desktop/windows10/2

#### Elements and attributes of this extension

```XML
<Extension Category="windows.firewallRules">  
  <FirewallRules Executable="[executable file name]">  
    <Rule
      Direction="[Direction]"
      IPProtocol="[Protocol]"
      LocalPortMin="[LocalPortMin]"
      LocalPortMax="LocalPortMax"
      RemotePortMin="RemotePortMin"
      RemotePortMax="RemotePortMax"
      Profile="[Profile]"/>  
  </FirewallRules>  
</Extension>
```
Find the complete schema reference [here](https://docs.microsoft.com/uwp/schemas/appxpackage/uapmanifestschema/element-desktop2-firewallrules).

|Name |Description |
|-------|-------------|
|Category |Always ``windows.firewallRules``|
|Executable |The name of the executable file that you want to add to the list of firewall exceptions |
|Direction |Indicates whether the rule is an inbound or outbound rule |
|IPProtocol |The communication protocol |
|LocalPortMin |The lower port number in a range of local port numbers. |
|LocalPortMax |The highest port number of a range of local port numbers. |
|RemotePortMax |The lower port number in a range of remote port numbers. |
|RemotePortMax |The highest port number of a range of remote port numbers. |
|Profile |The network type |



#### Example

```XML
<Package
  xmlns:desktop2="http://schemas.microsoft.com/appx/manifest/desktop/windows10/2"
  IgnorableNamespaces="desktop2">
  <Extensions>
    <desktop2:Extension Category="windows.firewallRules">  
      <desktop2:FirewallRules Executable="Contoso.exe">  
          <desktop2:Rule Direction="in" IPProtocol="TCP" Profile="all"/>  
          <desktop2:Rule Direction="in" IPProtocol="UDP" LocalPortMin="1337" LocalPortMax="1338" Profile="domain"/>  
          <desktop2:Rule Direction="in" IPProtocol="UDP" LocalPortMin="1337" LocalPortMax="1338" Profile="public"/>  
          <desktop2:Rule Direction="out" IPProtocol="UDP" LocalPortMin="1339" LocalPortMax="1340" RemotePortMin="15"
                         RemotePortMax="19" Profile="domainAndPrivate"/>  
          <desktop2:Rule Direction="out" IPProtocol="GRE" Profile="private"/>  
      </desktop2:FirewallRules>  
  </desktop2:Extension>
</Extensions>
</Package>
```

<a id="load-paths" />

### Place your DLL files into any folder of the package

Use an extension to identify those folders. That way, the system can find and load the files that you place in them. Think of this extension as a replacement of the _%PATH%_ environment variable.

If you don't use this extension, the system searches the package dependency graph of the process, the package root folder, and then the system directory (_%SystemRoot%\system32_) in that order. To learn more, see [Search order of Windows apps](https://msdn.microsoft.com/library/windows/desktop/ms682586.aspx#_search_order_for_windows_store_apps).

Each package can contain only one of these extensions. That means that you can add one of them to your main package, and then add one to each of your [optional packages, and related sets](https://docs.microsoft.com/windows/uwp/packaging/optional-packages).

#### XML namespace

http://schemas.microsoft.com/appx/manifest/uap/windows10/6

#### Elements and attributes of this extension
Declare this extension at the package-level of your app manifest.

```XML
<Extension Category="windows.loaderSearchPathOverride">
  <LoaderSearchPathOverride>
    <LoaderSearchPathEntry FolderPath="[path]"/>
  </LoaderSearchPathOverride>
</Extension>

```

|Name | Description |
|-------|-------------|
|Category |Always ``windows.loaderSearchPathOverride``.
|FolderPath | The path of the folder that contains your dll files. Specify a path that is relative to the root folder of the package. You can specify up to five paths in one extension. If you want the system to search for files in the root folder of the package, use an empty string for one of these paths. Don't included duplicate paths and make sure that your paths don't contain leading and trailing slashes or backslashes. <br><br> The system won't search subfolders, so make sure to explicitly list each folder that contains DLL files that you want the system to load.|

#### Example

```XML
<Package
  xmlns:uap3="http://schemas.microsoft.com/appx/manifest/uap/windows10/6"
  IgnorableNamespaces="uap6">
  ...
    <Extensions>
      <uap6:Extension Category="windows.loaderSearchPathOverride">
        <uap6:LoaderSearchPathOverride>
          <uap6:LoaderSearchPathEntry FolderPath=""/>
          <uap6:LoaderSearchPathEntry FolderPath="folder1/subfolder1"/>
          <uap6:LoaderSearchPathEntry FolderPath="folder2/subfolder2"/>
        </uap6:LoaderSearchPathOverride>
      </uap6:Extension>
    </Extensions>
...
</Package>
```

## Integrate with File Explorer

Help users organize your files and interact with them in familiar ways.

* [Define how your app behaves when users select and open multiple files at the same time](#define)
* [Show file contents in a thumbnail image within File Explorer](#show)
* [Show file contents in a Preview pane of File Explorer](#preview)
* [Enable users to group files by using the Kind column in File Explorer](#enable)
* [Make file properties available to search, index, property dialogs, and the details pane](#make-file-properties)
* [Make files from your cloud service appear in File Explorer](#cloud-files)

<a id="define" />

### Define how your app behaves when users select and open multiple files at the same time

Specify how your app behaves when a user opens multiple files simultaneously.

#### XML namespaces

* http://schemas.microsoft.com/appx/manifest/uap/windows10
* http://schemas.microsoft.com/appx/manifest/uap/windows10/2
* http://schemas.microsoft.com/appx/manifest/uap/windows10/3

#### Elements and attributes of this extension

```XML
<Extension Category="windows.fileTypeAssociation">
    <FileTypeAssociation Name="[AppID]" MultiSelectModel="[SelectionModel]">
        <SupportedVerbs>
            <Verb Id="Edit" MultiSelectModel="[SelectionModel]">Edit</Verb>
        </SupportedVerbs>
	      <SupportedFileTypes>
		        <FileType>"[FileExtension]"</FileType>
        </SupportedFileTypes>
</Extension>
```
Find the complete schema reference [here](https://docs.microsoft.com/uwp/schemas/appxpackage/uapmanifestschema/element-uap-filetypeassociation).

|Name |Description |
|-------|-------------|
|Category |Always ``windows.fileTypeAssociation``.
|Name |A unique Id for your app. |
|MultiSelectModel |See below |
|FileType |The relevant file extensions. |

**MultSelectModel**

packaged desktop apps have the same three options as regular desktop apps.

 * ``Player``: Your app is activated one time. All of the selected files are passed to your app as argument parameters.
 * ``Single``: Your app is activated one time for the first selected file. Other files are ignored.
 * ``Document``: A new, separate instance of your app is activated for each selected file.

 You can set different preferences for different file types and actions. For example, you may wish to open *Documents* in *Document* mode and *Images* in *Player* mode.

#### Example

```XML
<Package
  xmlns:uap="http://schemas.microsoft.com/appx/manifest/uap/windows10"
  xmlns:uap2="http://schemas.microsoft.com/appx/manifest/uap/windows10/2"
  xmlns:uap3="http://schemas.microsoft.com/appx/manifest/uap/windows10/3"
  IgnorableNamespaces="uap, uap2, uap3">
  <Applications>
    <Application>
      <Extensions>
        <uap:Extension Category="windows.fileTypeAssociation">
          <uap3:FileTypeAssociation Name="myapp" MultiSelectModel="Document">
            <uap2:SupportedVerbs>
              <uap3:Verb Id="Edit" MultiSelectModel="Player">Edit</uap3:Verb>
              <uap3:Verb Id="Preview" MultiSelectModel="Document">Preview</uap3:Verb>
            </uap2:SupportedVerbs>
      	    <uap:SupportedFileTypes>
      		    <uap:FileType>.txt</uap:FileType>
            </uap:SupportedFileTypes>
        </uap:Extension>
      </Extensions>
    </Application>
  </Applications>
</Package>
```

If the user opens 15 or fewer files, the default choice for the **MultiSelectModel** attribute is *Player*. Otherwise, the default is *Document*. UWP apps are always started as *Player*.

<a id="show" />

### Show file contents in a thumbnail image within File Explorer

Enable users to view a thumbnail image of the file's contents when the icon of the file appears in the medium, large, or extra large size.

#### XML namespace

* http://schemas.microsoft.com/appx/manifest/uap/windows10
* http://schemas.microsoft.com/appx/manifest/uap/windows10/2
* http://schemas.microsoft.com/appx/manifest/uap/windows10/3
* http://schemas.microsoft.com/appx/manifest/desktop/windows10/2

#### Elements and attributes of this extension

```XML
<Extension Category="windows.fileTypeAssociation">
    <FileTypeAssociation Name="[AppID]">
        <SupportedFileTypes>
            <FileType>"[FileExtension]"</FileType>
        </SupportedFileTypes>
        <ThumbnailHandler
            Clsid  ="[Clsid  ]" />
    </FileTypeAssociation>
</Extension>
```

Find the complete schema reference [here](https://docs.microsoft.com/uwp/schemas/appxpackage/uapmanifestschema/element-uap-filetypeassociation).

|Name |Description |
|-------|-------------|
|Category |Always ``windows.fileTypeAssociation``.
|Name |A unique Id for your app. |
|FileType |The relevant file extensions. |
|Clsid   |The class ID of your app. |

#### Example

```XML
<Package
  xmlns:uap="http://schemas.microsoft.com/appx/manifest/uap/windows10"
  xmlns:uap2="http://schemas.microsoft.com/appx/manifest/uap/windows10/2"
  xmlns:uap3="http://schemas.microsoft.com/appx/manifest/uap/windows10/3"
  xmlns:desktop2="http://schemas.microsoft.com/appx/manifest/desktop/windows10/2"
  IgnorableNamespaces="uap, uap2, uap3, desktop2">
  <Applications>
    <Application>
      <Extensions>
        <uap:Extension Category="windows.fileTypeAssociation">
          <uap3:FileTypeAssociation Name="Contoso">
            <uap2:SupportedFileTypes>
              <uap:FileType>.bar</uap:FileType>
            </uap2:SupportedFileTypes>
            <desktop2:ThumbnailHandler
              Clsid  ="20000000-0000-0000-0000-000000000001"  />
            </uap3:FileTypeAssociation>
         </uap::Extension>
      </Extensions>
    </Application>
  </Applications>
</Package>
```

<a id="preview" />

### Show file contents in the Preview pane of File Explorer

Enable users to preview a file's contents in the Preview pane of File Explorer.

#### XML namespace

* http://schemas.microsoft.com/appx/manifest/uap/windows10
* http://schemas.microsoft.com/appx/manifest/uap/windows10/2
* http://schemas.microsoft.com/appx/manifest/uap/windows10/3
* http://schemas.microsoft.com/appx/manifest/desktop/windows10/2

#### Elements and attributes of this extension

```XML
<Extension Category="windows.fileTypeAssociation">
    <FileTypeAssociation Name="[AppID]">
        <SupportedFileTypes>
            <FileType>"[FileExtension]"</FileType>
        </SupportedFileTypes>
        <DesktopPreviewHandler Clsid  ="[Clsid  ]" />
    </FileTypeAssociation>
</Extension>
```

Find the complete schema reference [here](https://docs.microsoft.com/uwp/schemas/appxpackage/uapmanifestschema/element-uap-filetypeassociation).

|Name |Description |
|-------|-------------|
|Category |Always ``windows.fileTypeAssociation``.
|Name |A unique Id for your app. |
|FileType |The relevant file extensions. |
|Clsid   |The class ID of your app. |

#### Example

```XML
<Package
  xmlns:uap="http://schemas.microsoft.com/appx/manifest/uap/windows10"
  xmlns:uap2="http://schemas.microsoft.com/appx/manifest/uap/windows10/2"
  xmlns:uap3="http://schemas.microsoft.com/appx/manifest/uap/windows10/3"
  xmlns:desktop2="http://schemas.microsoft.com/appx/manifest/desktop/windows10/2"
  IgnorableNamespaces="uap, uap2, uap3, desktop2">
  <Applications>
    <Application>
      <Extensions>
        <uap:Extension Category="windows.fileTypeAssociation">
          <uap3:FileTypeAssociation Name="Contoso">
            <uap2SupportedFileTypes>
              <uap:FileType>.bar</uap:FileType>
                </uap2SupportedFileTypes>
              <desktop2:DesktopPreviewHandler Clsid ="20000000-0000-0000-0000-000000000001" />
           </uap3:FileTypeAssociation>
        </uap:Extension>
      </Extensions>
    </Application>
  </Applications>
</Package>
```

<a id="enable" />

### Enable users to group files by using the Kind column in File Explorer

You can associate one or more predefined values for your file types with the **Kind** field.

In File Explorer, users can group those files by using that field. System components also use this field for various purposes such as indexing.

For more information about the **Kind** field and the values that you can use for this field, see [Using Kind Names](https://msdn.microsoft.com/library/windows/desktop/cc144136.aspx).

#### XML namespaces

* http://schemas.microsoft.com/appx/manifest/uap/windows10
* http://schemas.microsoft.com/appx/manifest/foundation/windows10/restrictedcapabilities/3

#### Elements and attributes of this extension

```XML
<Extension Category="windows.fileTypeAssociation">
    <FileTypeAssociation Name="[AppID]">
        <SupportedFileTypes>
            <FileType>"[FileExtension]"</FileType>
        </SupportedFileTypes>
        <KindMap>
            <Kind value="[KindValue]">
        </KindMap>
    </FileTypeAssociation>
</Extension>
```
Find the complete schema reference [here](https://docs.microsoft.com/uwp/schemas/appxpackage/uapmanifestschema/element-uap-filetypeassociation).

|Name |Description |
|-------|-------------|
|Category |Always ``windows.fileTypeAssociation``.
|Name |A unique Id for your app. |
|FileType |The relevant file extensions. |
|value |A valid [Kind value](https://msdn.microsoft.com/en-us/library/windows/desktop/cc144136.aspx#kind_hierarchy) |

#### Example

```XML
<Package
  xmlns:uap="http://schemas.microsoft.com/appx/manifest/uap/windows10"
  xmlns:rescap="http://schemas.microsoft.com/appx/manifest/foundation/windows10/restrictedcapabilities/3"
  IgnorableNamespaces="uap, rescap">
  <Applications>
    <Application>
      <Extensions>
        <uap:Extension Category="windows.fileTypeAssociation">
           <uap:FileTypeAssociation Name="Contoso">
             <uap:SupportedFileTypes>
               <uap:FileType>.m4a</uap:FileType>
               <uap:FileType>.mta</uap:FileType>
             </uap:SupportedFileTypes>
             <rescap:KindMap>
               <rescap:Kind value="Item">
               <rescap:Kind value="Communications">
               <rescap:Kind value="Task">
             </rescap:KindMap>
          </uap:FileTypeAssociation>
      </uap:Extension>
      </Extensions>
    </Application>
  </Applications>
</Package>
```
<a id="make-file-properties" />

### Make file properties available to search, index, property dialogs, and the details pane

#### XML namespace

* http://schemas.microsoft.com/appx/manifest/uap/windows10
* http://schemas.microsoft.com/appx/manifest/uap/windows10/3
* http://schemas.microsoft.com/appx/manifest/desktop/windows10/2

#### Elements and attributes of this extension

```XML
<uap:Extension Category="windows.fileTypeAssociation">
    <uap:FileTypeAssociation Name="[AppID]">
        <SupportedFileTypes>
            <FileType>.bar</FileType>
        </SupportedFileTypes>
        <DesktopPropertyHandler Clsid ="[Clsid]"/>
    </uap:FileTypeAssociation>
</uap:Extension>
```
Find the complete schema reference [here](https://docs.microsoft.com/uwp/schemas/appxpackage/uapmanifestschema/element-uap-filetypeassociation).

|Name |Description |
|-------|-------------|
|Category |Always ``windows.fileTypeAssociation``.
|Name |A unique Id for your app. |
|FileType |The relevant file extensions. |
|Clsid  |The class ID of your app. |

#### Example

```XML
<Package
  xmlns:uap="http://schemas.microsoft.com/appx/manifest/uap/windows10"
  xmlns:uap3="http://schemas.microsoft.com/appx/manifest/uap/windows10/3"
  xmlns:desktop2="http://schemas.microsoft.com/appx/manifest/desktop/windows10/2"
  IgnorableNamespaces="uap, uap3, desktop2">
  <Applications>
    <Application>
      <Extensions>
        <uap:Extension Category="windows.fileTypeAssociation">
          <uap3:FileTypeAssociation Name="Contoso">
            <uap:SupportedFileTypes>
              <uap:FileType>.bar</uap:FileType>
            </uap:SupportedFileTypes>
            <desktop2:DesktopPropertyHandler Clsid ="20000000-0000-0000-0000-000000000001"/>
          </uap3:FileTypeAssociation>
        </uap:Extension>
      </Extensions>
    </Application>
  </Applications>
</Package>
```

<a id="cloud-files" />

### Make files from your cloud service appear in File Explorer

Register the handlers that you implement in your application. You can also add context menu options that appear when you users right-click your cloud-based files in File Explorer.

#### XML namespace

* http://schemas.microsoft.com/appx/manifest/desktop/windows10

#### Elements and attributes of this extension

```XML
<Extension Category="windows.cloudfiles" >
    <CloudFiles IconResource="[Icon]">
        <CustomStateHandler Clsid ="[Clsid]"/>
        <ThumbnailProviderHandler Clsid ="[Clsid]"/>
        <ExtendedPropertyhandler Clsid ="[Clsid]"/>
        <CloudFilesContextMenus>
            <Verb Id ="Command3" Clsid= "[GUID]">[Verb Label]</Verb>
        </CloudFilesContextMenus>
    </CloudFiles>
</Extension>

```

|Name |Description |
|-------|-------------|
|Category |Always ``windows.cloudfiles``.
|iconResource |The icon that represents your cloud file provider service. This icon appears in the Navigation pane of File Explorer.  Users choose this icon to show files from your cloud service. |
|CustomStateHandler Clsid |The class ID of the app that implements the CustomStateHandler. The system uses this Class ID to request custom states and columns for cloud files. |
|ThumbnailProviderHandler Clsid |The class ID of the app that implements the ThumbnailProviderHandler. The system uses this Class ID to request thumbnail images for cloud files. |
|ExtendedPropertyHandler Clsid |The class ID of the app that implements the ExtendedPropertyHandler.  The system uses this Class ID to request extended properties for a cloud file. |
|Verb |The name that appears in the File Explorer context menu for files provided by your cloud service. |
|Id |The unique ID of the verb. |

#### Example

```XML
<Package
    xmlns:desktop="http://schemas.microsoft.com/appx/manifest/desktop/windows10"
    IgnorableNamespaces="desktop">
  <Applications>
    <Application>
      <Extensions>
        <Extension Category="windows.cloudfiles" >
            <CloudFiles IconResource="images\Wide310x150Logo.png">
                <CustomStateHandler Clsid ="20000000-0000-0000-0000-000000000001"/>
                <ThumbnailProviderHandler Clsid ="20000000-0000-0000-0000-000000000001"/>
                <ExtendedPropertyhandler Clsid ="20000000-0000-0000-0000-000000000001"/>
                <desktop:CloudFilesContextMenus>
                    <desktop:Verb Id ="keep" Clsid=     
                       "20000000-0000-0000-0000-000000000001">
                       Always keep on this device</desktop:Verb>
                </desktop:CloudFilesContextMenus>
            </CloudFiles>
          </Extension>
      </Extensions>
    </Application>
  </Applications>
</Package>
```

<a id="start" />

## Start your app in different ways

* [Start your app by using a protocol](#protocol)
* [Start your app by using an alias](#alias)
* [Start an executable file when users log into Windows](#executable)
* [Enable users to start your app when they connect a device to their PC](#autoplay)
* [Restart automatically after receiving an update from the Microsoft Store](#updates)

<a id="protocol" />

### Start your app by using a protocol

Protocol associations can enable other programs and system components to interoperate with your packaged app. When your packaged app is started by using a protocol, you can specify specific parameters to pass to its activation event arguments so it can behave accordingly. Parameters are supported only for packaged, full-trust apps. UWP apps can't use parameters.  

#### XML namespace

http://schemas.microsoft.com/appx/manifest/uap/windows10/3


#### Elements and attributes of this extension

```XML
<Extension
    Category="windows.protocol">
	<Protocol
      Name="[Protocol name]"
      Parameters="[Parameters]" />
</Extension>
```

Find the complete schema reference [here](https://docs.microsoft.com/uwp/schemas/appxpackage/uapmanifestschema/element-uap-protocol).

|Name |Description |
|-------|-------------|
|Category |Always ``windows.protocol``.
|Name |The name of the protocol. |
|Parameters |The list of parameters and values to pass to your app as event arguments when the app is activated. If a variable can contain a file path, wrap the parameter value in quotes. That will avoid any issues that happen in cases where the path includes spaces. |

### Example

```XML
<Package
  xmlns:uap3="http://schemas.microsoft.com/appx/manifest/uap/windows10/3"
  IgnorableNamespaces="uap3">
  <Applications>
    <Application>
      <Extensions>
        <uap3:Extension
          Category="windows.protocol">
      	<uap3:Protocol
          Name="myapp-cmd"
          Parameters="/p &quot;%1&quot;" />
        </uap3:Extension>
      </Extensions>
    </Application>
  </Applications>
</Package>
```
<a id="alias" />

### Start your app by using an alias

Users and other processes can use an alias to start your app without having to specify the full path to your app. You can specify that alias name.

#### XML namespaces

* http://schemas.microsoft.com/appx/manifest/uap/windows10/3
* http://schemas.microsoft.com/appx/manifest/desktop/windows10


#### Elements and attributes of this extension

```XML
<Extension
    Category="windows.appExecutionAlias"
    Executable="[ExecutableName]"
    EntryPoint="Windows.FullTrustApplication">
    <AppExecutionAlias>
		    <desktop:ExecutionAlias Alias="[AliasName]" />
	  </AppExecutionAlias>
</Extension>
```

|Name |Description |
|-------|-------------|
|Category |Always ``windows.appExecutionAlias``.
|Executable |The relative path to the executable to start when the alias is invoked. |
|Alias |The short name for your app. It must always end with the ".exe" extension. You can only specify a single app execution alias for each application in the package. If multiple apps register for the same alias, the system will invoke the last one that was registered, so make sure to choose a unique alias other apps are unlikely to override.
|

#### Example

```XML
<Package
  xmlns:uap3="http://schemas.microsoft.com/appx/manifest/uap/windows10/3"
  xmlns:desktop="http://schemas.microsoft.com/appx/manifest/desktop/windows10"
  IgnorableNamespaces="uap3, desktop">
  ...
  <uap3:Extension
        Category="windows.appExecutionAlias"
        Executable="exes\launcher.exe"
        EntryPoint="Windows.FullTrustApplication">
	    <uap3:AppExecutionAlias>
		    <desktop:ExecutionAlias Alias="Contoso.exe" />
	    </uap3:AppExecutionAlias>
  </uap3:Extension>
...
</Package>
```

Find the complete schema reference [here](https://docs.microsoft.com/uwp/schemas/appxpackage/uapmanifestschema/element-uap-filetypeassociation).

<a id="executable" />

### Start an executable file when users log into Windows

Startup tasks allow your app to run an executable automatically whenever a user logs on.

> [!NOTE]
> The user has to start your app at least one time to register this startup task.

Your app can declare multiple startup tasks. Each task starts independently. All startup tasks will appear in Task Manager under the **Startup** tab with the name that you specify in your app's manifest and your app's icon. Task Manager will automatically analyze the startup impact of your tasks.

Users can manually disable your app's startup task by using Task Manager. If a user disables a task, you can't programmatically re-enable it.

#### XML namespace

http://schemas.microsoft.com/appx/manifest/desktop/windows10

#### Elements and attributes of this extension

```XML
<Extension
    Category="windows.startupTask"
    Executable="[ExecutableName]"
    EntryPoint="Windows.FullTrustApplication">
	<StartupTask
      TaskId="[TaskID]"
      Enabled="true"
      DisplayName="[DisplayName]" />
</Extension>
```

|Name |Description |
|-------|-------------|
|Category |Always ``windows.startupTask``.|
|Executable |The relative path to the executable file to start. |
|TaskId |A unique identifier for your task. Using this identifier, your app can call the APIs in the [Windows.ApplicationModel.StartupTask](https://docs.microsoft.com/uwp/api/Windows.ApplicationModel.StartupTask) class to programmatically enable or disable a startup task. |
|Enabled |Indicates whether the task first starts enabled or disabled. Enabled tasks will run the next time the user logs on (unless the user disables it). |
|DisplayName |The name of the task that appears in Task Manager. You can localize this string by using ```ms-resource```. |

#### Example

```XML
<Package
  xmlns:desktop="http://schemas.microsoft.com/appx/manifest/desktop/windows10"
  IgnorableNamespaces="desktop">
  <Applications>
    <Application>
      <Extensions>
        <desktop:Extension
          Category="windows.startupTask"
          Executable="bin\MyStartupTask.exe"
          EntryPoint="Windows.FullTrustApplication">
     	  <desktop:StartupTask
          TaskId="MyStartupTask"
          Enabled="true"
          DisplayName="My App Service" />
        </desktop:Extension>
      </Extensions>
    </Application>
  </Applications>
 </Package>
```
<a id="autoplay" />

### Enable users to start your app when they connect a device to their PC

AutoPlay can present your app as an option when a user connects a device to their PC.

#### XML namespace

http://schemas.microsoft.com/appx/manifest/desktop/windows10/3


#### Elements and attributes of this extension

```XML
<Extension Category="windows.autoPlayHandler">
  <AutoPlayHandler>
    <InvokeAction ActionDisplayName="[action string]" ProviderDisplayName="[name of your app/service]">
      <Content ContentEvent="[Content event]" Verb="[any string]" DropTargetHandler="[Clsid]" />
      <Content ContentEvent="[Content event]" Verb="[any string]" Parameters="[Initialization parameter]"/>
      <Device DeviceEvent="[Device event]" HWEventHandler="[Clsid]" InitCmdLine="[Initialization parameter]"/>
    </InvokeAction>
  </AutoPlayHandler>
```

|Name |Description |
|-------|-------------|
|Category |Always ``windows.autoPlayHandler``.
|ActionDisplayName |A string that represents the action that users can take with a device that they connect to a PC (For example: "Import files", or "Play video"). |
|ProviderDisplayName | A string that represents your app or service (For example: "Contoso video player"). |
|ContentEvent |The name of a content event that causes users to be prompted with your ``ActionDisplayName`` and ``ProviderDisplayName``. A content event is raised when a volume device such as a camera memory card, thumb drive, or DVD is inserted into the PC. You can find the full list of those events [here](https://docs.microsoft.com/windows/uwp/launch-resume/auto-launching-with-autoplay#autoplay-event-reference).  |
|Verb |The Verb setting identifies a value that is passed to your app for the selected option. You can specify multiple launch actions for an AutoPlay event and use the Verb setting to determine which option a user has selected for your app. You can tell which option the user selected by checking the verb property of the startup event arguments passed to your app. You can use any value for the Verb setting except, open, which is reserved. |
|DropTargetHandler |The class ID of the app that implements the [IDropTarget](https://docs.microsoft.com/dotnet/api/microsoft.visualstudio.ole.interop.idroptarget?view=visualstudiosdk-2017) interface. Files from the removable media are passed to the [Drop](https://docs.microsoft.com/dotnet/api/microsoft.visualstudio.ole.interop.idroptarget.drop?view=visualstudiosdk-2017#Microsoft_VisualStudio_OLE_Interop_IDropTarget_Drop_Microsoft_VisualStudio_OLE_Interop_IDataObject_System_UInt32_Microsoft_VisualStudio_OLE_Interop_POINTL_System_UInt32__) method of your [IDropTarget](https://docs.microsoft.com/dotnet/api/microsoft.visualstudio.ole.interop.idroptarget?view=visualstudiosdk-2017) implementation.  |
|Parameters |You don't have to implement the [IDropTarget](https://docs.microsoft.com/dotnet/api/microsoft.visualstudio.ole.interop.idroptarget?view=visualstudiosdk-2017) interface for all content events. For any of the content events, you could provide command line parameters instead of implementing the [IDropTarget](https://docs.microsoft.com/dotnet/api/microsoft.visualstudio.ole.interop.idroptarget?view=visualstudiosdk-2017) interface. For those events, AutoPlay will start your app by using those command line parameters. You can parse those parameters in your app's initialization code to determine if it was started by AutoPlay and then provide your custom implementation. |
|DeviceEvent |The name of a device event that causes users to be prompted with your ``ActionDisplayName`` and ``ProviderDisplayName``. A device event is raised when a device is connected to the PC. Device events begin with the string ``WPD`` and you can find them listed [here](https://docs.microsoft.com/windows/uwp/launch-resume/auto-launching-with-autoplay#autoplay-event-reference). |
|HWEventHandler |The Class ID of the app that implements the [IHWEventHandler](https://msdn.microsoft.com/library/windows/desktop/bb775492.aspx) interface. |
|InitCmdLine |The string parameter that you want to pass into the [Initialize](https://msdn.microsoft.com/en-us/library/windows/desktop/bb775495.aspx) method of the [IHWEventHandler](https://msdn.microsoft.com/library/windows/desktop/bb775492.aspx) interface. |

### Example

```XML
<Package
  xmlns:desktop3="http://schemas.microsoft.com/appx/manifest/desktop/windows10/3"
  IgnorableNamespaces="desktop3">
  <Applications>
    <Application>
      <Extensions>
        <desktop3:Extension Category="windows.autoPlayHandler">
          <desktop3:AutoPlayHandler>
            <desktop3:InvokeAction ActionDisplayName="Import my files" ProviderDisplayName="ms-resource:AutoPlayDisplayName">
              <desktop3:Content ContentEvent="ShowPicturesOnArrival" Verb="show" DropTargetHandler="CD041BAE-0DEA-4472-9B7B-C98043D26EA8"/>
              <desktop3:Content ContentEvent="PlayVideoFilesOnArrival" Verb="play" Parameters="%1" />
              <desktop3:Device DeviceEvent="WPD\ImageSource" HWEventHandler="CD041BAE-0DEA-4472-9B7B-C98043D26EA8" InitCmdLine="/autoplay"/>
            </desktop3:InvokeAction>
          </desktop3:AutoPlayHandler>
      </Extensions>
    </Application>
  </Applications>
</Package>
```
<a id="updates" />

### Restart automatically after receiving an update from the Microsoft Store

If your app is open when users install an update to it, the app closes.

If you want that app to restart after the update completes, call the  [RegisterApplicationRestart](https://msdn.microsoft.com/library/windows/desktop/aa373347.aspx) function in every process that you want to restart.

Each active window in your app receives a [WM_QUERYENDSESSION](https://msdn.microsoft.com/library/windows/desktop/aa376890.aspx) message. At this point, your app can call the [RegisterApplicationRestart](https://msdn.microsoft.com/library/windows/desktop/aa373347.aspx) function again to update the command line if necessary.

When each active window in your app receives the [WM_ENDSESSION](https://msdn.microsoft.com/library/windows/desktop/aa376889.aspx) message, your app should save data and shut down.

>[!NOTE]
Your active windows also receive the [WM_CLOSE](https://msdn.microsoft.com/library/windows/desktop/ms632617.aspx) message in case the app doesn't handle the [WM_ENDSESSION](https://msdn.microsoft.com/library/windows/desktop/aa376889.aspx) message.

At this point, your app has 30 seconds to close it's own processes or the platform terminates them forcefully.

After the update is complete, your app restarts.

## Work with other applications

Integrate with other apps, start other processes or share information.

* [Make your app appear as the print target in applications that support printing](#printing)
* [Share fonts with other Windows applications](#fonts)
* [Start a Win32 process from a Universal Windows Platform (UWP) app](#win32-process)

<a id="printing" />

### Make your app appear as the print target in applications that support printing

When users want to print data from another app such as Notepad, you can make your app appear as a print target in the app's list of available print targets.

You'll have to modify your app so that it receives print data in XML Paper Specification (XPS) format.

#### XML namespaces

http://schemas.microsoft.com/appx/manifest/desktop/windows10/2

#### Elements and attributes of this extension

```XML
<Extension Category="windows.appPrinter">
    <AppPrinter
        DisplayName="[DisplayName]"
        Parameters="[Parameters]" />
</Extension>
```

Find the complete schema reference [here](https://docs.microsoft.com/uwp/schemas/appxpackage/uapmanifestschema/element-desktop2-appprinter).

|Name |Description |
|-------|-------------|
|Category |Always ``windows.appPrinter``.
|DisplayName |The name that you want to appear in the list of print targets for an app. |
|Parameters |Any parameters that your app requires to properly handle the request. |

#### Example

```XML
<Package
  xmlns:desktop2="http://schemas.microsoft.com/appx/manifest/desktop/windows10/2"
  IgnorableNamespaces="desktop2">
  <Applications>
  <Application>
    <Extensions>
      <desktop2:Extension Category="windows.appPrinter">
        <desktop2:AppPrinter
          DisplayName="Send to Contoso"
          Parameters="/insertdoc %1" />
      </desktop2:Extension>
    </Extensions>
  </Application>
</Applications>
</Package>
```
Find a sample that uses this extension [Here](https://github.com/Microsoft/DesktopBridgeToUWP-Samples/tree/master/Samples/PrintToPDF)

<a id="fonts" />

### Share fonts with other Windows applications

Share your custom fonts with other Windows applications.

#### XML namespaces

http://schemas.microsoft.com/appx/manifest/desktop/windows10/2

#### Elements and attributes of this extension

```XML
<Extension Category="windows.sharedFonts">
    <SharedFonts>
      <Font File="[FontFile]" />
    </SharedFonts>
  </Extension>
```

Find the complete schema reference [here](https://review.docs.microsoft.com/uwp/schemas/appxpackage/uapmanifestschema/element-uap4-sharedfonts).


|Name |Description |
|-------|-------------|
|Category |Always ``windows.sharedFonts``.
|File |The file that contains the fonts that you want to share. |

#### Example

```XML
<Package
  xmlns:uap4="http://schemas.microsoft.com/appx/manifest/uap/windows10/4"
  IgnorableNamespaces="uap4">
  <Applications>
    <Application>
      <Extensions>
        <uap4:Extension Category="windows.sharedFonts">
          <uap4:SharedFonts>
            <uap4:Font File="Fonts\JustRealize.ttf" />
            <uap4:Font File="Fonts\JustRealizeBold.ttf" />
          </uap4:SharedFonts>
        </uap4:Extension>
      </Extensions>
    </Application>
  </Applications>
</Package>
```
<a id="win32-process" />

### Start a Win32 process from a Universal Windows Platform (UWP) app

Start a Win32 process that runs in full-trust.

#### XML namespaces

http://schemas.microsoft.com/appx/manifest/desktop/windows10

#### Elements and attributes of this extension

```XML
<Extension Category="windows.fullTrustProcess" Executable="[executable file]">
  <FullTrustProcess>
    <ParameterGroup GroupId="[GroupID]" Parameters="[Parameters]"/>
  </FullTrustProcess>
</Extension>
```

|Name |Description |
|-------|-------------|
|Category |Always ``windows.fullTrustProcess``.
|GroupID |A string that identifies a set of parameters that you want to pass to the executable. |
|Parameters |Parameters that you want to pass to the executable. |

#### Example

```XML
<Package xmlns="http://schemas.microsoft.com/appx/manifest/foundation/windows10"
         xmlns:uap="http://schemas.microsoft.com/appx/manifest/uap/windows10"
         xmlns:rescap=
"http://schemas.microsoft.com/appx/manifest/foundation/windows10/restrictedcapabilities"
         xmlns:desktop="http://schemas.microsoft.com/appx/manifest/desktop/windows10">
  ...
  <Capabilities>
      <rescap:Capability Name="runFullTrust"/>
  </Capabilities>
  <Applications>
    <Application>
      <Extensions>
          <desktop:Extension Category="windows.fullTrustProcess" Executable="fulltrustprocess.exe">
              <desktop:FullTrustProcess>
                  <desktop:ParameterGroup GroupId="SyncGroup" Parameters="/Sync"/>
                  <desktop:ParameterGroup GroupId="OtherGroup" Parameters="/Other"/>
              </desktop:FullTrustProcess>
           </desktop:Extension>
      </Extensions>
    </Application>
  </Applications>
</Package>
```
This extension might be useful if you want to create a Universal Windows Platform User interface that runs on all devices, but you want components of your Win32 app to continue running in full-trust.

Just create a desktop bridge package for your Win32 app. Then, add this extension to the package file of your UWP app. This extensions indicates that you want to start an executable file in the desktop bridge package.  If you want to communicate between your UWP app and your Win32 app, you can set up one or more [app services](../launch-resume/app-services.md) to do that. You can read more about this scenario [here](https://blogs.msdn.microsoft.com/appconsult/2016/12/19/desktop-bridge-the-migrate-phase-invoking-a-win32-process-from-a-uwp-app/).

## Next steps

**Find answers to your questions**

Have questions? Ask us on Stack Overflow. Our team monitors these [tags](http://stackoverflow.com/questions/tagged/project-centennial+or+desktop-bridge). You can also ask us [here](https://social.msdn.microsoft.com/Forums/en-US/home?filter=alltypes&sort=relevancedesc&searchTerm=%5BDesktop%20Converter%5D).

**Give feedback or make feature suggestions**

See [UserVoice](https://wpdev.uservoice.com/forums/110705-universal-windows-platform/category/161895-desktop-bridge-centennial).
