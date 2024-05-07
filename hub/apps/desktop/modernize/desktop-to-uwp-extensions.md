---
description: You can use extensions to integrate your packaged desktop app with Windows 10 and later releases in predefined ways.
title: Integrate your desktop app with Windows using packaging extensions
ms.date: 09/11/2020
ms.topic: article
keywords: windows 10, uwp
ms.assetid: 0a8cedac-172a-4efd-8b6b-67fd3667df34
ms.localizationpriority: medium
---

# Integrate your desktop app with Windows using packaging extensions

If your desktop app is packaged (has package identity at runtime), then you can use extensions to integrate your app with Windows by using predefined [extensions in the package manifest](/uwp/schemas/appxpackage/uapmanifestschema/extensions). Also see [Features that require package identity](./modernize-packaged-apps.md).

For example, use an extension to create a firewall exception; make your app the default application for a file type; or point Start tiles to your app. To use an extension, just add some XML to your app's package manifest file. No code is required.

This topic describes those extensions and the tasks that you can perform by using them.

> [!NOTE]
> The features described in this topic require that your app is packaged (has package identity at runtime). That includes packaged apps (see [Create a new project for a packaged WinUI 3 desktop app](../../winui/winui3/create-your-first-winui3-app.md#packaged-create-a-new-project-for-a-packaged-c-or-c-winui-3-desktop-app)) and packaged apps with external location (see [Grant package identity by packaging with external location](./grant-identity-to-nonpackaged-apps.md)). Also see [Features that require package identity](./modernize-packaged-apps.md).

## Transition users to your app

Help users transition to your packaged app.

* [Redirect your existing desktop app to your packaged app](#redirect)
* [Point existing Start tiles and taskbar buttons to your packaged app](#point)
* [Make your packaged app open files instead of your desktop app](#make)
* [Associate your packaged app with a set of file types](#associate)
* [Add options to the context menus of files that have a certain file type](#add)
* [Open certain types of files directly by using a URL](#open)

<a id="redirect"></a>

### Redirect your existing desktop app to your packaged app

When users start your existing unpackaged desktop app, you can configure your packaged app to be opened instead. 

> [!NOTE]
> This feature is supported in Windows Insider Preview Build 21313 and later versions.

To enable this behavior:

1. Add registry entries to redirect your unpackaged desktop app executable to your packaged app.
2. Register your packaged app to be launched when your unpackaged desktop app executable is launched.

#### Add registry entries to redirect your unpackaged desktop app executable

1. In the registry, create a subkey with the name of your desktop app executable file under the **HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options** key.
2. Under this subkey, add the following values:
    * **AppExecutionAliasRedirect** (DWORD): If set to 1, the system will check for an [AppExecutionAlias](/uwp/schemas/appxpackage/uapmanifestschema/element-uap3-appexecutionalias) package extension with the same name as the executable. If the **AppExecutionAlias** extension is enabled, the packaged app will be activated using that value.
    * **AppExecutionAliasRedirectPackages** (REG_SZ): The system will redirect only to the listed packages. Packages are listed by their package family name, separated by semicolons. If the special value * is used, the system will redirect to an **AppExecutionAlias** from any package.

For example:

```Ini
HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\contosoapp.exe 
    AppExecutionAliasRedirect = 1
    AppExecutionAliasRedirectPackages = "Microsoft.WindowsNotepad_8weky8webbe" 
```

#### Register your packaged app to be launched

In your package manifest, add an [AppExecutionAlias](/uwp/schemas/appxpackage/uapmanifestschema/element-uap3-appexecutionalias) extension that registers the name of your unpackaged desktop app executable. For example:

```XML
<Package
  xmlns:uap3="http://schemas.microsoft.com/appx/manifest/uap/windows10/3"
  IgnorableNamespaces="uap3">
  <Applications>
    <Application>
      <Extensions>
        <uap3:Extension Category="windows.appExecutionAlias" EntryPoint="Windows.FullTrustApplication">
          <uap3:AppExecutionAlias>
            <desktop:ExecutionAlias Alias="contosoapp.exe" />
          </uap3:AppExecutionAlias>
        </uap3:Extension>
      </Extensions>
    </Application>
  </Applications>
</Package>
```

#### Disable the redirection

Users can turn off the redirection and launch your unpackaged app executable via these options:

* They can uninstall the packaged version of your app.
* The user can disable the **AppExecutionAlias** entry for your packaged app in the **App execution aliases** page in **Settings**.

#### XML namespaces

* `http://schemas.microsoft.com/appx/manifest/uap/windows10/3`
* `http://schemas.microsoft.com/appx/manifest/desktop/windows10`

#### Elements and attributes of this extension

```XML
<uap3:Extension
    Category="windows.appExecutionAlias"
    EntryPoint="Windows.FullTrustApplication">
    <uap3:AppExecutionAlias>
        <desktop:ExecutionAlias Alias="[AliasName]" />
    </uap3:AppExecutionAlias>
</uap3:Extension>
```

|Name |Description |
|-------|-------------|
|Category |Always ``windows.appExecutionAlias``. |
|Executable |The relative path to the executable to start when the alias is invoked. |
|Alias |The short name for your app. It must always end with the ".exe" extension. |

<a id="point"></a>

### Point existing Start tiles and taskbar buttons to your packaged app

Your users might have pinned your desktop application to the taskbar or the Start menu. You can point those shortcuts to your new packaged app.

#### XML namespace

`http://schemas.microsoft.com/appx/manifest/foundation/windows10/restrictedcapabilities/3`

#### Elements and attributes of this extension

```XML
<Extension Category="windows.desktopAppMigration">
    <DesktopAppMigration>
        <DesktopApp AumId="[your_app_aumid]" />
        <DesktopApp ShortcutPath="[path]" />
    </DesktopAppMigration>
</Extension>
```

Find the complete schema reference [here](/uwp/schemas/appxpackage/uapmanifestschema/element-rescap3-desktopappmigration).

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

<a id="make"></a>

### Make your packaged application open files instead of your desktop app

You can make sure that users open your new packaged application by default for specific types of files instead of opening the desktop version of your app.

To do that, you'll specify the [programmatic identifier (ProgID)](/windows/desktop/shell/fa-progids) of each application from which you want to inherit file associations.

#### XML namespaces

* `http://schemas.microsoft.com/appx/manifest/uap/windows10/3`
* `http://schemas.microsoft.com/appx/manifest/foundation/windows10/restrictedcapabilities/3`

#### Elements and attributes of this extension

```XML
<Extension Category="windows.fileTypeAssociation">
    <FileTypeAssociation Name="[Name]">
         <MigrationProgIds>
            <MigrationProgId>"[ProgID]"</MigrationProgId>
        </MigrationProgIds>
    </FileTypeAssociation>
</Extension>
```

Find the complete schema reference [here](/uwp/schemas/appxpackage/uapmanifestschema/element-uap-filetypeassociation).

|Name |Description |
|-------|-------------|
|Category |Always ``windows.fileTypeAssociation``.
|Name |The name of the file type association. You can use this name to organize and group file types. The name must be all lower case characters with no spaces. |
|MigrationProgId |The [programmatic identifier (ProgID)](/windows/desktop/shell/fa-progids) that describes the application, component, and version of the desktop application from which you want to inherit file associations.|

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
          <uap3:FileTypeAssociation Name="myfiletypes">
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

<a id="associate"></a>

### Associate your packaged application with a set of file types

You can associate your packaged application with file type extensions. If a user right-clicks a file in File Explorer and then selects the **Open with** option, your application appears in the list of suggestions. For more information about using this extension, see [Integrate a packaged desktop app with File Explorer](integrate-packaged-app-with-file-explorer.md).

#### XML namespaces

* `http://schemas.microsoft.com/appx/manifest/uap/windows10`
* `http://schemas.microsoft.com/appx/manifest/uap/windows10/3`

#### Elements and attributes of this extension

```XML
<Extension Category="windows.fileTypeAssociation">
    <FileTypeAssociation Name="[Name]">
        <SupportedFileTypes>
            <FileType>"[file extension]"</FileType>
        </SupportedFileTypes>
    </FileTypeAssociation>
</Extension>
```

Find the complete schema reference [here](/uwp/schemas/appxpackage/uapmanifestschema/element-uap-filetypeassociation).

|Name |Description |
|-------|-------------|
|Category |Always ``windows.fileTypeAssociation``.
|Name | The name of the file type association. You can use this name to organize and group file types. The name must be all lower case characters with no spaces.   |
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
          <uap3:FileTypeAssociation Name="mediafiles">
            <uap:SupportedFileTypes>
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

<a id="add"></a>

### Add options to the context menus of files that have a certain file type

This extension enables you to add options to the context menu that displays when users right-click a file in File Explorer.These options give users other ways to interact with your file such as print, edit, or preview the file. For more information about using this extension, see [Integrate a packaged desktop app with File Explorer](integrate-packaged-app-with-file-explorer.md).

#### XML namespaces

* `http://schemas.microsoft.com/appx/manifest/uap/windows10`
* `http://schemas.microsoft.com/appx/manifest/uap/windows10/2`
* `http://schemas.microsoft.com/appx/manifest/uap/windows10/3`

#### Elements and attributes of this extension

```XML
<Extension Category="windows.fileTypeAssociation">
    <FileTypeAssociation Name="[Name]">
        <SupportedVerbs>
           <Verb Id="[ID]" Extended="[Extended]" Parameters="[parameters]">"[verb label]"</Verb>
        </SupportedVerbs>
    </FileTypeAssociation>
</Extension>
```

Find the complete schema reference [here](/uwp/schemas/appxpackage/uapmanifestschema/element-uap-filetypeassociation).

|Name |Description |
|-------|-------------|
|Category | Always ``windows.fileTypeAssociation``.
|Name |The name of the file type association. You can use this name to organize and group file types. The name must be all lower case characters with no spaces. |
|Verb |The name that appears in the File Explorer context menu. This string is localizable that uses ```ms-resource```.|
|Id |The unique Id of the verb. If your application is a UWP app, this is passed to your app as part of its activation event args so it can handle the user’s selection appropriately. If your application is a full-trust packaged app, it receives parameters instead (see the next bullet). |
|Parameters |The list of argument parameters and values associated with the verb. If your application is a full-trust packaged app, these parameters are passed to the application as event args when the application is activated. You can customize the behavior of your application based on different activation verbs. If a variable can contain a file path, wrap the parameter value in quotes. That will avoid any issues that happen in cases where the path includes spaces. If your application is a UWP app, you can’t pass parameters. The app receives the Id instead (see the previous bullet).|
|Extended |Specifies that the verb appears only if the user shows the context menu by holding the **Shift** key before right-clicking the file. This attribute is optional and defaults to a value of **False** (for example, always show the verb) if not listed. You specify this behavior individually for each verb (except for "Open," which is always **False**).|

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
          <uap3:FileTypeAssociation Name="myfiletypes">
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

<a id="open"></a>

### Open certain types of files directly by using a URL

You can make sure that users open your new packaged application by default for specific types of files instead of opening the desktop version of your app.

#### XML namespaces

* `http://schemas.microsoft.com/appx/manifest/uap/windows10`
* `http://schemas.microsoft.com/appx/manifest/uap/windows10/3`

#### Elements and attributes of this extension

```XML
<Extension Category="windows.fileTypeAssociation">
    <FileTypeAssociation Name="[Name]" UseUrl="true" Parameters="%1">
        <SupportedFileTypes>
            <FileType>"[FileExtension]"</FileType>
        </SupportedFileTypes>
    </FileTypeAssociation>
</Extension>
```

Find the complete schema reference [here](/uwp/schemas/appxpackage/uapmanifestschema/element-uap-filetypeassociation).

|Name |Description |
|-------|-------------|
|Category |Always ``windows.fileTypeAssociation``.
|Name |The name of the file type association. You can use this name to organize and group file types. The name must be all lower case characters with no spaces. |
|UseUrl |Indicates whether to open files directly from a URL target. If you do not set this value, attempts by your application to open a file by using a URL cause the system to first download the file locally. |
|Parameters | Optional parameters. |
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
            <uap3:FileTypeAssociation Name="myfiletypes" UseUrl="true" Parameters="%1">
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

<a id="rules"></a>

### Create firewall exception for your app

If your application requires communication through a port, you can add your application to the list of firewall exceptions.

> [!NOTE]
> To use the "windows.firewallRules" extension category (see below), your package needs the **Full Trust Permission Level** restricted capability. See [Restricted capability list](/windows/uwp/packaging/app-capability-declarations#restricted-capability-list).

#### XML namespace

`http://schemas.microsoft.com/appx/manifest/desktop/windows10/2`

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

Find the complete schema reference [here](/uwp/schemas/appxpackage/uapmanifestschema/element-desktop2-firewallrules).

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

<a id="load-paths"></a>

### Place your DLL files into any folder of the package

Use the [uap6:LoaderSearchPathOverride](/uwp/schemas/appxpackage/uapmanifestschema/element-uap6-loadersearchpathoverride) extension to declare up to five folder paths in the app package, relative to the app package root path, to be used in the loader search path for the app's processes.

The [DLL search order](/windows/win32/dlls/dynamic-link-library-search-order) for Windows apps includes packages in the package dependency graph if the packages have execution rights. By default, this includes main, optional and framework packages, although this can be overwritten by the [uap6:AllowExecution](/uwp/schemas/appxpackage/uapmanifestschema/element-uap6-allowexecution) element in the package manifest.

A package that is included in the DLL search order will, by default, include its *effective path*. For more information about effective paths, see the [EffectivePath](/uwp/api/windows.applicationmodel.package.effectivepath) property (WinRT) and the [PackagePathType](/windows/win32/api/appmodel/ne-appmodel-packagepathtype) enumeration (Win32).

If a package specifies [uap6:LoaderSearchPathOverride](/uwp/schemas/appxpackage/uapmanifestschema/element-uap6-loadersearchpathoverride), then this information is used instead of the package's effective path.

Each package can contain only one [uap6:LoaderSearchPathOverride](/uwp/schemas/appxpackage/uapmanifestschema/element-uap6-loadersearchpathoverride) extension. That means that you can add one of them to your main package, and then add one to each of your [optional packages, and related sets](/windows/msix/package/optional-packages).

#### XML namespace

`http://schemas.microsoft.com/appx/manifest/uap/windows10/6`

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
|FolderPath | The path of the folder that contains your DLL files. Specify a path that is relative to the root folder of the package. You can specify up to five paths in one extension. If you want the system to search for files in the root folder of the package, use an empty string for one of these paths. Don't include duplicate paths, and make sure that your paths don't contain leading and trailing slashes or backslashes. <br><br> The system won't search subfolders, so make sure to explicitly list each folder that contains DLL files that you want the system to load.|

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

* [Define how your application behaves when users select and open multiple files at the same time](#define)
* [Show file contents in a thumbnail image within File Explorer](#show)
* [Show file contents in a Preview pane of File Explorer](#preview)
* [Enable users to group files by using the Kind column in File Explorer](#enable)
* [Make file properties available to search, index, property dialogs, and the details pane](#make-file-properties)
* [Specify a context menu handler for a file type](#context-menu)
* [Make files from your cloud service appear in File Explorer](#cloud-files)

<a id="define"></a>

### Define how your application behaves when users select and open multiple files at the same time

Specify how your application behaves when a user opens multiple files simultaneously.

#### XML namespaces

* `http://schemas.microsoft.com/appx/manifest/uap/windows10`
* `http://schemas.microsoft.com/appx/manifest/uap/windows10/2`
* `http://schemas.microsoft.com/appx/manifest/uap/windows10/3`

#### Elements and attributes of this extension

```XML
<Extension Category="windows.fileTypeAssociation">
    <FileTypeAssociation Name="[Name]" MultiSelectModel="[SelectionModel]">
        <SupportedVerbs>
            <Verb Id="Edit" MultiSelectModel="[SelectionModel]">Edit</Verb>
        </SupportedVerbs>
        <SupportedFileTypes>
            <FileType>"[FileExtension]"</FileType>
        </SupportedFileTypes>
</Extension>
```

Find the complete schema reference [here](/uwp/schemas/appxpackage/uapmanifestschema/element-uap-filetypeassociation).

|Name |Description |
|-------|-------------|
|Category |Always ``windows.fileTypeAssociation``.
|Name |The name of the file type association. You can use this name to organize and group file types. The name must be all lower case characters with no spaces. |
|MultiSelectModel |See below |
|FileType |The relevant file extensions. |

**MultiSelectModel**

packaged desktop apps have the same three options as regular desktop apps.

* ``Player``: Your application is activated one time. All of the selected files are passed to your application as argument parameters.
* ``Single``: Your application is activated one time for the first selected file. Other files are ignored.
* ``Document``: A new, separate instance of your application is activated for each selected file.

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
          <uap3:FileTypeAssociation Name="myfiletypes" MultiSelectModel="Document">
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

<a id="show"></a>

### Show file contents in a thumbnail image within File Explorer

Enable users to view a thumbnail image of the file's contents when the icon of the file appears in the medium, large, or extra large size.

#### XML namespace

* `http://schemas.microsoft.com/appx/manifest/uap/windows10`
* `http://schemas.microsoft.com/appx/manifest/uap/windows10/2`
* `http://schemas.microsoft.com/appx/manifest/uap/windows10/3`
* `http://schemas.microsoft.com/appx/manifest/desktop/windows10/2`

#### Elements and attributes of this extension

```XML
<Extension Category="windows.fileTypeAssociation">
    <FileTypeAssociation Name="[Name]">
        <SupportedFileTypes>
            <FileType>"[FileExtension]"</FileType>
        </SupportedFileTypes>
        <ThumbnailHandler
            Clsid  ="[Clsid  ]" />
    </FileTypeAssociation>
</Extension>
```

Find the complete schema reference [here](/uwp/schemas/appxpackage/uapmanifestschema/element-uap-filetypeassociation).

|Name |Description |
|-------|-------------|
|Category |Always ``windows.fileTypeAssociation``.
|Name |The name of the file type association. You can use this name to organize and group file types. The name must be all lower case characters with no spaces. |
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
          <uap3:FileTypeAssociation Name="myfiletypes">
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

<a id="preview"></a>

### Show file contents in the Preview pane of File Explorer

Enable users to preview a file's contents in the Preview pane of File Explorer.

#### XML namespace

* `http://schemas.microsoft.com/appx/manifest/uap/windows10`
* `http://schemas.microsoft.com/appx/manifest/uap/windows10/2`
* `http://schemas.microsoft.com/appx/manifest/uap/windows10/3`
* `http://schemas.microsoft.com/appx/manifest/desktop/windows10/2`

#### Elements and attributes of this extension

```XML
<Extension Category="windows.fileTypeAssociation">
    <FileTypeAssociation Name="[Name]">
        <SupportedFileTypes>
            <FileType>"[FileExtension]"</FileType>
        </SupportedFileTypes>
        <DesktopPreviewHandler Clsid  ="[Clsid  ]" />
    </FileTypeAssociation>
</Extension>
```

Find the complete schema reference [here](/uwp/schemas/appxpackage/uapmanifestschema/element-uap-filetypeassociation).

|Name |Description |
|-------|-------------|
|Category |Always ``windows.fileTypeAssociation``.
|Name |The name of the file type association. You can use this name to organize and group file types. The name must be all lower case characters with no spaces. |
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
          <uap3:FileTypeAssociation Name="myfiletypes">
            <uap:SupportedFileTypes>
              <uap:FileType>.bar</uap:FileType>
                </uap:SupportedFileTypes>
              <desktop2:DesktopPreviewHandler Clsid ="20000000-0000-0000-0000-000000000001" />
           </uap3:FileTypeAssociation>
        </uap:Extension>
      </Extensions>
    </Application>
  </Applications>
</Package>
```

<a id="enable"></a>

### Enable users to group files by using the Kind column in File Explorer

You can associate one or more predefined values for your file types with the **Kind** field.

In File Explorer, users can group those files by using that field. System components also use this field for various purposes such as indexing.

For more information about the **Kind** field and the values that you can use for this field, see [Using Kind Names](/windows/desktop/properties/building-property-handlers-user-friendly-kind-names).

#### XML namespaces

* `http://schemas.microsoft.com/appx/manifest/uap/windows10`
* `http://schemas.microsoft.com/appx/manifest/foundation/windows10/restrictedcapabilities/3`

#### Elements and attributes of this extension

```XML
<Extension Category="windows.fileTypeAssociation">
    <FileTypeAssociation Name="[Name]">
        <SupportedFileTypes>
            <FileType>"[FileExtension]"</FileType>
        </SupportedFileTypes>
        <KindMap>
            <Kind value="[KindValue]">
        </KindMap>
    </FileTypeAssociation>
</Extension>
```

Find the complete schema reference [here](/uwp/schemas/appxpackage/uapmanifestschema/element-uap-filetypeassociation).

|Name |Description |
|-------|-------------|
|Category |Always ``windows.fileTypeAssociation``.
|Name |The name of the file type association. You can use this name to organize and group file types. The name must be all lower case characters with no spaces. |
|FileType |The relevant file extensions. |
|value |A valid [Kind value](/windows/desktop/properties/building-property-handlers-user-friendly-kind-names) |

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
           <uap:FileTypeAssociation Name="mediafiles">
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

<a id="make-file-properties"></a>

### Make file properties available to search, index, property dialogs, and the details pane

#### XML namespace

* `http://schemas.microsoft.com/appx/manifest/uap/windows10`
* `http://schemas.microsoft.com/appx/manifest/uap/windows10/3`
* `http://schemas.microsoft.com/appx/manifest/desktop/windows10/2`

#### Elements and attributes of this extension

```XML
<uap:Extension Category="windows.fileTypeAssociation">
    <uap:FileTypeAssociation Name="[Name]">
        <SupportedFileTypes>
            <FileType>.bar</FileType>
        </SupportedFileTypes>
        <DesktopPropertyHandler Clsid ="[Clsid]"/>
    </uap:FileTypeAssociation>
</uap:Extension>
```

Find the complete schema reference [here](/uwp/schemas/appxpackage/uapmanifestschema/element-uap-filetypeassociation).

|Name |Description |
|-------|-------------|
|Category |Always ``windows.fileTypeAssociation``.
|Name |The name of the file type association. You can use this name to organize and group file types. The name must be all lower case characters with no spaces. |
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
          <uap3:FileTypeAssociation Name="myfiletypes">
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

<a id="context-menu"></a>

### Specify a context menu handler for a file type

If your desktop application defines a [context menu handler](/windows/desktop/shell/context-menu-handlers), use this extension to register the menu handler.

#### XML namespaces

* `http://schemas.microsoft.com/appx/manifest/foundation/windows10`
* `http://schemas.microsoft.com/appx/manifest/desktop/windows10/4`

#### Elements and attributes of this extension

```XML
<Extensions>
    <com:Extension Category="windows.comServer">
        <com:ComServer>
            <com:SurrogateServer AppId="[AppID]" DisplayName="[DisplayName]">
                <com:Class Id="[Clsid]" Path="[Path]" ThreadingModel="[Model]"/>
            </com:SurrogateServer>
        </com:ComServer>
    </com:Extension>
    <desktop4:Extension Category="windows.fileExplorerContextMenus">
        <desktop4:FileExplorerContextMenus>
            <desktop4:ItemType Type="[Type]">
                <desktop4:Verb Id="[ID]" Clsid="[Clsid]" />
            </desktop4:ItemType>
        </desktop4:FileExplorerContextMenus>
    </desktop4:Extension>
</Extensions>
```

Find the complete schema reference here: [com:ComServer](/uwp/schemas/appxpackage/uapmanifestschema/element-com-comserver) and [desktop4:FileExplorerContextMenus](/uwp/schemas/appxpackage/uapmanifestschema/element-desktop4-fileexplorercontextmenus).

#### Instructions

To register your context menu handler, follow these instructions.

1. In your desktop application, implement a [context menu handler](/windows/desktop/shell/context-menu-handlers) by implementing the [IExplorerCommand](/windows/desktop/api/shobjidl_core/nn-shobjidl_core-iexplorercommand) or [IExplorerCommandState](/windows/desktop/api/shobjidl_core/nn-shobjidl_core-iexplorercommandstate) interface. For a sample, see the [ExplorerCommandVerb](https://github.com/microsoft/Windows-classic-samples/tree/master/Samples/Win7Samples/winui/shell/appshellintegration/ExplorerCommandVerb) code sample. Make sure that you define a class GUID for each of your implementation objects. For example, the following code defines a class ID for an implementation of [IExplorerCommand](/windows/desktop/api/shobjidl_core/nn-shobjidl_core-iexplorercommand).

    ```cpp
    class __declspec(uuid("d0c8bceb-28eb-49ae-bc68-454ae84d6264")) CExplorerCommandVerb;
    ```

2. In your package manifest, specify a [com:ComServer](/uwp/schemas/appxpackage/uapmanifestschema/element-com-comserver) application extension that registers a COM surrogate server with the class ID of your context menu handler implementation.

    ```xml
    <com:Extension Category="windows.comServer">
        <com:ComServer>
            <com:SurrogateServer AppId="d0c8bceb-28eb-49ae-bc68-454ae84d6264" DisplayName="ContosoHandler">
                <com:Class Id="d0c8bceb-28eb-49ae-bc68-454ae84d6264" Path="ExplorerCommandVerb.dll" ThreadingModel="STA"/>
            </com:SurrogateServer>
        </com:ComServer>
    </com:Extension>
    ```

2. In your package manifest, specify a [desktop4:FileExplorerContextMenus](/uwp/schemas/appxpackage/uapmanifestschema/element-desktop4-fileexplorercontextmenus) application extension that registers your context menu handler implementation.

    ```xml
    <desktop4:Extension Category="windows.fileExplorerContextMenus">
        <desktop4:FileExplorerContextMenus>
            <desktop4:ItemType Type=".rar">
                <desktop4:Verb Id="Command1" Clsid="d0c8bceb-28eb-49ae-bc68-454ae84d6264" />
            </desktop4:ItemType>
        </desktop4:FileExplorerContextMenus>
    </desktop4:Extension>
    ```

#### Example

```XML
<Package
  xmlns="http://schemas.microsoft.com/appx/manifest/foundation/windows10"
  xmlns:desktop4="http://schemas.microsoft.com/appx/manifest/desktop/windows10/4"
  xmlns:com="http://schemas.microsoft.com/appx/manifest/com/windows10"
  IgnorableNamespaces="desktop4">
  <Applications>
    <Application>
      <Extensions>
        <com:Extension Category="windows.comServer">
          <com:ComServer>
            <com:SurrogateServer AppId="d0c8bceb-28eb-49ae-bc68-454ae84d6264" DisplayName="ContosoHandler">
              <com:Class Id="d0c8bceb-28eb-49ae-bc68-454ae84d6264" Path="ExplorerCommandVerb.dll" ThreadingModel="STA"/>
            </com:SurrogateServer>
          </com:ComServer>
        </com:Extension>
        <desktop4:Extension Category="windows.fileExplorerContextMenus">
          <desktop4:FileExplorerContextMenus>
            <desktop4:ItemType Type=".contoso">
              <desktop4:Verb Id="Command1" Clsid="d0c8bceb-28eb-49ae-bc68-454ae84d6264" />
            </desktop4:ItemType>
          </desktop4:FileExplorerContextMenus>
        </desktop4:Extension>
      </Extensions>
    </Application>
  </Applications>
</Package>
```

<a id="cloud-files"></a>

### Make files from your cloud service appear in File Explorer

Register the handlers that you implement in your application. You can also add context menu options that appear when you users right-click your cloud-based files in File Explorer.

#### XML namespace

* `http://schemas.microsoft.com/appx/manifest/desktop/windows10`

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
|CustomStateHandler Clsid |The class ID of the application that implements the CustomStateHandler. The system uses this Class ID to request custom states and columns for cloud files. |
|ThumbnailProviderHandler Clsid |The class ID of the application that implements the ThumbnailProviderHandler. The system uses this Class ID to request thumbnail images for cloud files. |
|ExtendedPropertyHandler Clsid |The class ID of the application that implements the ExtendedPropertyHandler.  The system uses this Class ID to request extended properties for a cloud file. |
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

<a id="start"></a>

## Start your application in different ways

* [Start your application by using a protocol](#protocol)
* [Start your application by using an alias](#alias)
* [Start an executable file when users log into Windows](#executable)
* [Enable users to start your application when they connect a device to their PC](#autoplay)
* [Restart automatically after receiving an update from the Microsoft Store](#updates)

<a id="protocol"></a>

### Start your application by using a protocol

Protocol associations can enable other programs and system components to interoperate with your packaged app. When your packaged application is started by using a protocol, you can specify specific parameters to pass to its activation event arguments so it can behave accordingly. Parameters are supported only for packaged, full-trust apps. UWP apps can't use parameters.

#### XML namespace

`http://schemas.microsoft.com/appx/manifest/uap/windows10/3`

#### Elements and attributes of this extension

```XML
<Extension
    Category="windows.protocol">
  <Protocol
      Name="[Protocol name]"
      Parameters="[Parameters]" />
</Extension>
```

Find the complete schema reference [here](/uwp/schemas/appxpackage/uapmanifestschema/element-uap-protocol).

|Name |Description |
|-------|-------------|
|Category |Always ``windows.protocol``.
|Name |The name of the protocol. |
|Parameters |The list of parameters and values to pass to your application as event arguments when the application is activated. If a variable can contain a file path, wrap the parameter value in quotes. That will avoid any issues that happen in cases where the path includes spaces. |

### Example

```XML
<Package
  xmlns:uap3="http://schemas.microsoft.com/appx/manifest/uap/windows10/3"
  xmlns:desktop="http://schemas.microsoft.com/appx/manifest/desktop/windows10"
  IgnorableNamespaces="uap3, desktop">
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

<a id="alias"></a>

### Start your application by using an alias

Users and other processes can use an alias to start your application without having to specify the full path to your app. You can specify that alias name.

#### XML namespaces

* `http://schemas.microsoft.com/appx/manifest/uap/windows10/3`
* `http://schemas.microsoft.com/appx/manifest/desktop/windows10`

#### Elements and attributes of this extension

```XML
<uap3:Extension
    Category="windows.appExecutionAlias"
    Executable="[ExecutableName]"
    EntryPoint="Windows.FullTrustApplication">
    <uap3:AppExecutionAlias>
        <desktop:ExecutionAlias Alias="[AliasName]" />
    </uap3:AppExecutionAlias>
</uap3:Extension>
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
  IgnorableNamespaces="uap3">
  <Applications>
    <Application>
      <Extensions>
         <uap3:Extension
                Category="windows.appExecutionAlias"
                Executable="exes\launcher.exe"
                EntryPoint="Windows.FullTrustApplication">
            <uap3:AppExecutionAlias>
                <desktop:ExecutionAlias Alias="Contoso.exe" />
            </uap3:AppExecutionAlias>
        </uap3:Extension>
      </Extensions>
    </Application>
  </Applications>
</Package>
```

Find the complete schema reference [here](/uwp/schemas/appxpackage/uapmanifestschema/element-uap-filetypeassociation).

<a id="executable"></a>

### Start an executable file when users log into Windows

Startup tasks allow your application to run an executable automatically whenever a user logs on.

> [!NOTE]
> The user has to start your application at least one time to register this startup task.

Your application can declare multiple startup tasks. Each task starts independently. All startup tasks will appear in Task Manager under the **Startup** tab with the name that you specify in your app's manifest and your app's icon. Task Manager will automatically analyze the startup impact of your tasks.

Users can manually disable your app's startup task by using Task Manager. If a user disables a task, you can't programmatically re-enable it.

#### XML namespace

`http://schemas.microsoft.com/appx/manifest/desktop/windows10`

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
|TaskId |A unique identifier for your task. Using this identifier, your application can call the APIs in the [Windows.ApplicationModel.StartupTask](/uwp/api/Windows.ApplicationModel.StartupTask) class to programmatically enable or disable a startup task. |
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

<a id="autoplay"></a>

### Enable users to start your application when they connect a device to their PC

AutoPlay can present your application as an option when a user connects a device to their PC.

#### XML namespace

`http://schemas.microsoft.com/appx/manifest/desktop/windows10/3`

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
|ProviderDisplayName | A string that represents your application or service (For example: "Contoso video player"). |
|ContentEvent |The name of a content event that causes users to be prompted with your ``ActionDisplayName`` and ``ProviderDisplayName``. A content event is raised when a volume device such as a camera memory card, thumb drive, or DVD is inserted into the PC. You can find the full list of those events [here](/windows/uwp/launch-resume/auto-launching-with-autoplay#autoplay-event-reference).  |
|Verb |The Verb setting identifies a value that is passed to your application for the selected option. You can specify multiple launch actions for an AutoPlay event and use the Verb setting to determine which option a user has selected for your app. You can tell which option the user selected by checking the verb property of the startup event arguments passed to your app. You can use any value for the Verb setting except, open, which is reserved. |
|DropTargetHandler |The class ID of the application that implements the [IDropTarget](/dotnet/api/microsoft.visualstudio.ole.interop.idroptarget) interface. Files from the removable media are passed to the [Drop](/dotnet/api/microsoft.visualstudio.ole.interop.idroptarget.drop#Microsoft_VisualStudio_OLE_Interop_IDropTarget_Drop_Microsoft_VisualStudio_OLE_Interop_IDataObject_System_UInt32_Microsoft_VisualStudio_OLE_Interop_POINTL_System_UInt32__) method of your [IDropTarget](/dotnet/api/microsoft.visualstudio.ole.interop.idroptarget) implementation.  |
|Parameters |You don't have to implement the [IDropTarget](/dotnet/api/microsoft.visualstudio.ole.interop.idroptarget) interface for all content events. For any of the content events, you could provide command line parameters instead of implementing the [IDropTarget](/dotnet/api/microsoft.visualstudio.ole.interop.idroptarget) interface. For those events, AutoPlay will start your application by using those command line parameters. You can parse those parameters in your app's initialization code to determine if it was started by AutoPlay and then provide your custom implementation. |
|DeviceEvent |The name of a device event that causes users to be prompted with your ``ActionDisplayName`` and ``ProviderDisplayName``. A device event is raised when a device is connected to the PC. Device events begin with the string ``WPD`` and you can find them listed [here](/windows/uwp/launch-resume/auto-launching-with-autoplay#autoplay-event-reference). |
|HWEventHandler |The Class ID of the application that implements the [IHWEventHandler](/windows/desktop/api/shobjidl/nn-shobjidl-ihweventhandler) interface. |
|InitCmdLine |The string parameter that you want to pass into the [Initialize](/windows/desktop/api/shobjidl/nf-shobjidl-ihweventhandler-initialize) method of the [IHWEventHandler](/windows/desktop/api/shobjidl/nn-shobjidl-ihweventhandler) interface. |

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

<a id="updates"></a>

### Restart automatically after receiving an update from the Microsoft Store

If your application is open when users install an update to it, the application closes.

If you want that application to restart after the update completes, call the  [RegisterApplicationRestart](/windows/desktop/api/winbase/nf-winbase-registerapplicationrestart) function in every process that you want to restart.

Each active window in your application receives a [WM_QUERYENDSESSION](/windows/desktop/Shutdown/wm-queryendsession) message. At this point, your application can call the [RegisterApplicationRestart](/windows/desktop/api/winbase/nf-winbase-registerapplicationrestart) function again to update the command line if necessary.

When each active window in your application receives the [WM_ENDSESSION](/windows/desktop/Shutdown/wm-endsession) message, your application should save data and shut down.

>[!NOTE]
> Your active windows also receive the [WM_CLOSE](/windows/desktop/winmsg/wm-close) message in case the application doesn't handle the [WM_ENDSESSION](/windows/desktop/Shutdown/wm-endsession) message.

At this point, your application has 30 seconds to close it's own processes or the platform terminates them forcefully.

After the update is complete, your application restarts.

## Work with other applications

Integrate with other apps, start other processes or share information.

* [Make your application appear as the print target in applications that support printing](#printing)
* [Share fonts with other Windows applications](#fonts)
* [Start a Win32 process from a Universal Windows Platform (UWP) app](#win32-process)

<a id="printing"></a>

### Make your application appear as the print target in applications that support printing

When users want to print data from another application such as Notepad, you can make your application appear as a print target in the app's list of available print targets.

You'll have to modify your application so that it receives print data in XML Paper Specification (XPS) format.

#### XML namespaces

`http://schemas.microsoft.com/appx/manifest/desktop/windows10/2`

#### Elements and attributes of this extension

```XML
<Extension Category="windows.appPrinter">
    <AppPrinter
        DisplayName="[DisplayName]"
        Parameters="[Parameters]" />
</Extension>
```

Find the complete schema reference [here](/uwp/schemas/appxpackage/uapmanifestschema/element-desktop2-appprinter).

|Name |Description |
|-------|-------------|
|Category |Always ``windows.appPrinter``.
|DisplayName |The name that you want to appear in the list of print targets for an app. |
|Parameters |Any parameters that your application requires to properly handle the request. |

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

<a id="fonts"></a>

### Share fonts with other Windows applications

Share your custom fonts with other Windows applications.

> [!NOTE]
> Before you can submit an app that uses this extension to the Store, you must first obtain approval from the Store team. To obtain approval, go to [https://aka.ms/storesupport](https://aka.ms/storesupport), click **Contact us**, and choose options relevant to submitting apps to the dashboard. This approval process helps to ensure that there are no conflicts between fonts installed by your app and fonts that are installed with the OS. If you do not obtain approval, you will receive an error similar to the following when you submit your app: "Package acceptance validation error: You can't use extension windows.sharedFonts with this account. Contact our support team if you'd like to request permissions to use this extension."

#### XML namespaces

`http://schemas.microsoft.com/appx/manifest/uap/windows10/4`

#### Elements and attributes of this extension

```XML
<Extension Category="windows.sharedFonts">
    <SharedFonts>
      <Font File="[FontFile]" />
    </SharedFonts>
  </Extension>
```

Find the complete schema reference [here](/uwp/schemas/appxpackage/uapmanifestschema/element-uap4-sharedfonts).

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

<a id="win32-process"></a>

### Start a Win32 process from a Universal Windows Platform (UWP) app

Start a Win32 process that runs in full-trust.

#### XML namespaces

`http://schemas.microsoft.com/appx/manifest/desktop/windows10`

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

This extension might be useful if you want to create a Universal Windows Platform User interface that runs on all devices, but you want components of your Win32 application to continue running in full-trust.

Just create a Windows app package for your Win32 app. Then, add this extension to the package file of your UWP app. This extensions indicates that you want to start an executable file in the Windows app package.  If you want to communicate between your UWP app and your Win32 app, you can set up one or more [app services](/windows/uwp/launch-resume/app-services) to do that. You can read more about this scenario [here](/archive/blogs/appconsult/desktop-bridge-the-migrate-phase-invoking-a-win32-process-from-a-uwp-app).

## Next steps

Have questions? Ask us on Stack Overflow. Our team monitors these [tags](https://stackoverflow.com/questions/tagged/project-centennial+or+desktop-bridge). You can also ask us [here](https://social.msdn.microsoft.com/Forums/en-US/home?filter=alltypes&sort=relevancedesc&searchTerm=%5BDesktop%20Converter%5D).
