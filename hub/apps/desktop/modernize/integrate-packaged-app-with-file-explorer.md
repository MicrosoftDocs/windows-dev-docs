---
description: Learn how packaged and sparse-manifested desktop apps add commands to the Windows 11 File Explorer context menu.
title: Add a File Explorer context menu command to a packaged desktop app
ms.date: 07/16/2026
author: GrantMeStrength
ms.author: jken
ms.topic: how-to
keywords: windows 11, msix, sparse package, file explorer, context menu, iexplorercommand
ms.localizationpriority: medium
---

# Add a File Explorer context menu command to a packaged desktop app

Windows 11 apps extend the modern File Explorer context menu by implementing [IExplorerCommand](/windows/win32/api/shobjidl_core/nn-shobjidl_core-iexplorercommand) and registering the command with app identity. This applies to packaged desktop apps and to unpackaged Win32 apps that use a [sparse package](/windows/msix/packaging-tool/sparse-packages).

A file type association can make an app available in **Open with** or add an editing verb for a file type that the app handles. It does not provide the general-purpose context menu extension described in this article. To add a command for arbitrary files, folders, or folder backgrounds, use `IExplorerCommand`.

For the Windows 11 context-menu design and guidance on when to add a command, see [Extending the Context Menu and Share Dialog in Windows 11](https://blogs.windows.com/windowsdeveloper/2021/07/19/extending-the-context-menu-and-share-dialog-in-windows-11/).

## How context menu extensions work

A context menu extension has these parts:

1. A native DLL implements one or more COM classes that expose `IExplorerCommand`.
2. The package manifest registers each COM class as a `windows.comServer` extension.
3. The package manifest registers the command as a `windows.fileExplorerContextMenus` extension and associates a shell item type with the COM class's CLSID.

File Explorer activates the COM class when it builds the context menu. Your `IExplorerCommand` implementation supplies the title, icon, state, and action. Commands from the same app can be grouped into an app-attributed flyout; implement `EnumSubCommands` to supply child commands.

> [!IMPORTANT]
> File Explorer loads shell extension code as part of the shell experience. Keep `GetTitle`, `GetIcon`, `GetState`, and other menu-construction methods fast. Do not perform expensive work on the File Explorer UI path. Run longer operations after `Invoke` is called.

## Implement IExplorerCommand

Implement `IExplorerCommand` in a native COM DLL. C++ is the usual choice because the command runs in the Shell integration path. The following abbreviated implementation provides a command title, enables the command, and receives the selected items when the user invokes it.

```cpp
class EditCommand final : public IExplorerCommand
{
public:
    IFACEMETHODIMP GetTitle(IShellItemArray*, PWSTR* title)
    {
        return SHStrDup(L"Edit with Contoso", title);
    }

    IFACEMETHODIMP GetState(
        IShellItemArray*, BOOL, EXPCMDSTATE* state)
    {
        *state = ECS_ENABLED;
        return S_OK;
    }

    IFACEMETHODIMP Invoke(IShellItemArray* items, IBindCtx*)
    {
        // Obtain selected items from items and start the app or operation.
        return S_OK;
    }

    IFACEMETHODIMP GetIcon(IShellItemArray*, PWSTR* icon)
    {
        return SHStrDup(L"ContosoCommand.dll,-101", icon);
    }

    IFACEMETHODIMP GetToolTip(IShellItemArray*, PWSTR* tooltip)
    {
        *tooltip = nullptr;
        return E_NOTIMPL;
    }

    IFACEMETHODIMP GetCanonicalName(GUID* canonicalName)
    {
        *canonicalName = GUID_NULL;
        return S_OK;
    }

    IFACEMETHODIMP GetFlags(EXPCMDFLAGS* flags)
    {
        *flags = ECF_DEFAULT;
        return S_OK;
    }

    IFACEMETHODIMP EnumSubCommands(IEnumExplorerCommand** commands)
    {
        *commands = nullptr;
        return E_NOTIMPL;
    }
};
```

Use the `IShellItemArray` passed to `Invoke` to enumerate the current selection. `GetState` can return states such as `ECS_HIDDEN`, `ECS_DISABLED`, or `ECS_ENABLED` to control whether the command appears and can be selected. For the complete contract, see [IExplorerCommand](/windows/win32/api/shobjidl_core/nn-shobjidl_core-iexplorercommand).

Assign the COM class a CLSID and ensure that the same CLSID is used in the package manifest. For example:

```cpp
class __declspec(uuid("01234567-89AB-CDEF-0123-456789ABCDEF"))
    EditCommand;
```

## Register the command in the package manifest

Declare the `com`, `desktop4`, and `desktop5` namespaces on the root `Package` element and add them to `IgnorableNamespaces`.

```xml
<Package
  xmlns="http://schemas.microsoft.com/appx/manifest/foundation/windows10"
  xmlns:com="http://schemas.microsoft.com/appx/manifest/com/windows10"
  xmlns:desktop4="http://schemas.microsoft.com/appx/manifest/desktop/windows10/4"
  xmlns:desktop5="http://schemas.microsoft.com/appx/manifest/desktop/windows10/5"
  IgnorableNamespaces="com desktop4 desktop5">
  <!-- ... -->
</Package>
```

Under the application `Extensions` element, first register the DLL as a COM server. `com:Class/@Id` is the command's CLSID, and `Path` is the DLL's package-relative path.

```xml
<com:Extension Category="windows.comServer">
  <com:ComServer>
    <com:SurrogateServer DisplayName="Contoso commands">
      <com:Class
        Id="01234567-89AB-CDEF-0123-456789ABCDEF"
        Path="ContosoCommand.dll"
        ThreadingModel="STA" />
    </com:SurrogateServer>
  </com:ComServer>
</com:Extension>
```

Then associate the CLSID with the context in which the command should appear.

```xml
<desktop4:Extension Category="windows.fileExplorerContextMenus">
  <desktop4:FileExplorerContextMenus>
    <desktop5:ItemType Type="*">
      <desktop5:Verb
        Id="EditWithContoso"
        Clsid="01234567-89AB-CDEF-0123-456789ABCDEF" />
    </desktop5:ItemType>
  </desktop4:FileExplorerContextMenus>
</desktop4:Extension>
```

`desktop5:ItemType/@Type` identifies the target shell items. Use `*` for files, `Directory` for selected folders, or `Directory\Background` for the background of a folder. You can register multiple `ItemType` entries for the same CLSID when a command supports multiple contexts.

The `desktop5:Verb/@Id` identifies the manifest registration. `desktop5:Verb/@Clsid` must match the `com:Class/@Id` value and the CLSID on the COM class.

For the complete manifest schema, see [desktop4:FileExplorerContextMenus](/uwp/schemas/appxpackage/uapmanifestschema/element-desktop4-fileexplorercontextmenus) and [com:Class](/uwp/schemas/appxpackage/uapmanifestschema/element-com-surrogateserver-class).

## Package the DLL

Include the command DLL and any resources it requires in the package at the path specified by `com:Class/@Path`. If you use a Windows Application Packaging Project, add the DLL to the project and configure it to copy to the package output. Configure a build dependency or post-build copy so the package contains the current DLL.

The DLL architecture must match the File Explorer architecture that loads it. Build and package the appropriate architecture for the target device.

## Use a sparse package for an unpackaged app

An unpackaged Win32 app can use the same `IExplorerCommand` and manifest registrations by installing a sparse package that gives the app package identity. A sparse package does not contain the app binaries; it references the externally installed app instead.

In addition to the COM and context-menu extensions shown previously, a sparse package manifest typically declares `uap10:AllowExternalContent` and configures the application as a Win32 app. The following example shows the relevant sparse-package declarations.

```xml
<Package
  xmlns="http://schemas.microsoft.com/appx/manifest/foundation/windows10"
  xmlns:uap10="http://schemas.microsoft.com/appx/manifest/uap/windows10/10"
  xmlns:rescap="http://schemas.microsoft.com/appx/manifest/foundation/windows10/restrictedcapabilities"
  IgnorableNamespaces="uap10 rescap">

  <Properties>
    <uap10:AllowExternalContent>true</uap10:AllowExternalContent>
  </Properties>

  <Applications>
    <Application
      Id="ContosoApp"
      Executable="ContosoApp.exe"
      uap10:TrustLevel="mediumIL"
      uap10:RuntimeBehavior="win32App">
      <!-- VisualElements and the COM/context-menu Extensions go here. -->
    </Application>
  </Applications>

  <Capabilities>
    <rescap:Capability Name="runFullTrust" />
    <rescap:Capability Name="unvirtualizedResources" />
  </Capabilities>
</Package>
```

See [Sparse packages](/windows/msix/packaging-tool/sparse-packages) for packaging and registration requirements for unpackaged apps.

## Test the extension

Install or register the package, then open File Explorer and right-click an item matching one of the registered item types. If the command does not appear after installing or updating a package, restart File Explorer or sign out and back in so the shell reloads the extension registration.

Use **Show more options** only to inspect legacy context-menu extensions. A command registered through `windows.fileExplorerContextMenus` and implemented with `IExplorerCommand` appears in the Windows 11 context menu.

## File type associations

Register a [file type association](/windows/uwp/launch-resume/handle-file-activation) when the app opens or edits that file type. It can make the app available through **Open with** and can add an edit verb for associated file types. Use the context-menu mechanism in this article for commands that apply to generic files, folders, or backgrounds, or for commands that perform operations without opening the app's normal file-open experience.