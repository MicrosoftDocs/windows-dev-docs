---
title: Command Palette Extensibility
description: The Command Palette provides a full extension model, allowing you to create custom experiences for the palette. Learn how to create an extension and publish it.
ms.date: 2/28/2025
ms.topic: concept-article
no-loc: [PowerToys, Windows, Insider]
# Customer intent: As a Windows developer, I want to learn how to develop an extension for the Command Palette.
---

# Extensibility overview

The Command Palette provides a full extension model, allowing developers to create their own experiences for the palette. 

The fastest way to get started writing extensions is from the Command Palette itself. Just run the "Create a new extension" command, fill out the fields to populate the template project, and you should be ready to start.

For more detailed instructions, you can follow these pages:

* [Creating an extension](../command-palette/creating-an-extension.md)
* [Adding commands](../command-palette/adding-commands.md)
* [Update a list of commands](../command-palette/update-a-list-of-commands.md)
* [Add top-level commands to your extension](../command-palette/add-top-level-commands-to-your-extension.md)
* [Command results](../command-palette/command-results.md)
* [Display markdown content](../command-palette/using-markdown-content.md)
* [Get user input with forms](../command-palette/using-form-pages.md)
<!-- * [Handle the search text](../command-palette/dynamic-lists.md) -->
<!-- * [Advanced: Adding an extension to your package](../command-palette/adding-an-extension-to-your-package.md) -->


## Extension details

Command Palette defines a WinRT API ([Microsoft.CommandPalette.Extensions](./microsoft-commandpalette-extensions/microsoft-commandpalette-extensions.md)), which is how extensions can communicate with Command Palette. 

Command Palette will use the Package Catalog to find apps that list themselves as an `windows.appExtension` for `com.microsoft.commandpalette`.

### Registering your extension

Extensions can register themselves with the Command Palette using their `.appxmanifest`. As an example:

```xml
<Extensions>
    <com:Extension Category="windows.comServer">
        <com:ComServer>
            <com:ExeServer Executable="ExtensionName.exe" Arguments="-RegisterProcessAsComServer" DisplayName="Sample Extension">
                <com:Class Id="<Extension CLSID Here>" DisplayName="Sample Extension" />
            </com:ExeServer>
        </com:ComServer>
    </com:Extension>
    <uap3:Extension Category="windows.appExtension">
        <uap3:AppExtension Name="com.microsoft.commandpalette"
                           Id="YourApplicationUniqueId"
                           PublicFolder="Public"
                           DisplayName="Sample Extension"
                           Description="Sample Extension for Command Palette">
            <uap3:Properties>
                <CmdPalProvider>
                    <Activation>
                        <CreateInstance ClassId="<Extension CLSID Here>" />
                    </Activation>
                    <SupportedInterfaces>
                        <Commands />
                    </SupportedInterfaces>
                </CmdPalProvider>
            </uap3:Properties>
        </uap3:AppExtension>
    </uap3:Extension>
</Extensions>
```

In this manifest, we're using an out-of-process COM server to act as the communication layer between your app and Command Palette. **Don't worry about this**! The template project will take care of creating a COM server for you, starting it, and marshalling your objects to Command Palette. 

### Important notes

Some notable elements about the manifest example:

- The application must specify a `Extensions.uap3Extension.AppExtension` with the Name set to `com.microsoft.commandpalette`. This is the unique identifier which Command Palette uses to find it's extensions.
- The application must specify a `Extensions.comExtension.ComServer` to host their COM class. This allows for the OS to register that GUID as a COM class we can instantiate.
  - Make sure that this CLSID is unique, and matches the one in your application. If you change one, you need to change all three.
- In the `Properties` of your `AppExtension`, you must specify a `CmdPalProvider` element. This is where you specify the CLSID of the COM class that Command Palette will instantiate to interact with your extension. 
  - Currently, only `Commands` is supported.

## Related content

- [PowerToys Command Palette utility](overview.md)
- [Extension samples](samples.md)
