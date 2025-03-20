---
title: Command Palette Extensibility
description: The Command Palette provides a full extension model, allowing you to create custom experiences for the palette. Learn how to create an extension and publish it.
ms.date: 2/28/2025
ms.topic: concept-article
no-loc: [PowerToys, Windows, Insider]
# Customer intent: As a Windows developer, I want to learn how to develop an extension for the Command Palette.
---

# Extensibility overview

The Command Palette provides a full extension model, allowing developers to create their own experiences for the palette. This document provides information about how to create an extension and publish it. It also includes a sample extension that demonstrates the extensibility model.

## Registering your extension

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
                           Description="Sample Extension for Run">
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

### Important notes

Some notable elements about the manifest example:

- The application must specify a `Extensions.comExtension.ComServer` to host their COM class. This allows for the OS to register that GUID as a COM class we can instantiate.
  - Make sure that this CLSID is unique, and matches the one in your application
- The application must specify a `Extensions.uap3Extension.AppExtension` with the Name set to `com.microsoft.commandpalette`. This is the unique identifier which DevPal can use to find it's extensions.
- In the `Properties` of your `AppExtension`, you must specify a `CmdPalProvider` element. This is where you specify the CLSID of the COM class that DevPal will instantiate to interact with your extension. Also, you specify which interfaces you support.

Currently, only `Commands` is supported. If we need to add more in the future, they will be added to the `SupportedInterfaces` element.

## Related content

- [PowerToys Command Palette utility](overview.md)
- [Extension samples](samples.md)
