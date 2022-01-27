---
description:  An agile object is one that can be accessed from any thread. C#/WinRT provides support for agile references if you need to marshal a non-agile object across apartments in a safe way.
title: Agile objects with C#/WinRT
ms.date: 11/17/2020
ms.topic: article
ms.localizationpriority: medium
---

# Agile objects in C#/WinRT

Most Windows Runtime classes are *agile*, meaning they can be accessed from any threads across different apartments. C#/WinRT types that you author are agile by default, and it is not possible to opt out of this behavior for those types.

However, projected C#/WinRT types (which includes Windows Runtime types that are provided by the Windows SDK and the WinUI library) may or may not be agile. For example, many types that represent UI objects are not agile. When you consume non-agile types, you need to take into consideration their threading model and marshaling behavior. C#/WinRT provides support for agile references if you need to marshal a non-agile object across apartments in a safe way.

> [!NOTE]
> The Windows Runtime is based on COM. In COM terms, an agile class is registered with `ThreadingModel` = *Both*. For more info about COM threading models, and apartments, see [Understanding and Using COM Threading Models](/previous-versions/ms809971(v=msdn.10)).

## Check for agile support

To check whether a Windows Runtime object is agile, use the following code to determine whether the object supports the [IAgileObject](/windows/desktop/api/objidl/nn-objidl-iagileobject) interface.

```csharp
var queryAgileObject = testObject.As<IAgileObject>();

if (queryAgileObject != null) {
    // testObject is agile.
}
```

## Create an agile reference

To create an agile reference for a non-agile object, you can use the `AsAgile` extension method. `AsAgile` is a generic extension method that can be applied to any projected C#/WinRT type. If the type is not a projected type, an exception is thrown. Here is an example using a [PopupMenu](/uwp/api/Windows.UI.Popups.PopupMenu) object, which is a non-agile type from the Windows SDK.

```csharp
var nonAgileObj = new Windows.UI.Popups.PopupMenu();
AgileReference<Windows.UI.Popups.PopupMenu> agileReference = nonAgileObj.AsAgile();
```

You can now pass `agileReference` to a thread in a different apartment, and use it there.

```csharp
await Task.Run(() => {
        Windows.UI.Popups.PopupMenu nonAgileObjAgain = agileReference.Get()
    });
```
