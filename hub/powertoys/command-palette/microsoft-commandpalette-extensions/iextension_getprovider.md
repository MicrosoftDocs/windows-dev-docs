---
title: IExtension.GetProvider(ProviderType) Method
description: The GetProvider method is used to retrieve a provider of a specified type.
ms.date: 2/7/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# IExtension.GetProvider(ProviderType) Method

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

The **GetProvider** method is used to retrieve a provider of a specified type. This method allows extensions to access specific providers that are registered with the Command Palette, enabling them to interact with various functionalities and services.

## Parameters

*providerType* [ProviderType](providertype.md)

The type of provider to be retrieved. This parameter is used to specify the type of provider that the extension wants to access.

## Returns

An **IInspectable** object that represents the provider of the specified type. This object can be used to interact with the provider and access its functionality.
