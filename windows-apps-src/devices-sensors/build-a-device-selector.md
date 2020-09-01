---
ms.assetid: D06AA3F5-CED6-446E-94E8-713D98B13CAA
title: Build a device selector
description: Building a device selector will enable you to limit the devices you are searching through when enumerating devices.
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Build a device selector



**Important APIs**

- [**Windows.Devices.Enumeration**](/uwp/api/Windows.Devices.Enumeration)

Building a device selector will enable you to limit the devices you are searching through when enumerating devices. This will enable you to only get relevant results and will also improve the performance of the system. In most scenarios you get a device selector from a device stack. For example, you might use [**GetDeviceSelector**](/uwp/api/windows.devices.usb.usbdevice.getdeviceselector) for devices discovered over USB. These device selectors return an Advanced Query Syntax (AQS) string. If you are not familiar with the AQS format, you can read more at [Using Advanced Query Syntax Programmatically](/windows/desktop/search/-search-3x-advancedquerysyntax).

## Building the filter string

There are some cases where you need to enumerate devices and a provided device selector is not available for your scenario. A device selector is an AQS filter string that contains the following information. Before creating a filter string, you need to know some key pieces of information about the devices you want to enumerate.

-   The [**DeviceInformationKind**](/uwp/api/Windows.Devices.Enumeration.DeviceInformationKind) of the devices you are interested in. For more information about how **DeviceInformationKind** impacts enumerating devices, see [Enumerate devices](enumerate-devices.md).
-   How to build an AQS filter string, which is explained in this topic.
-   The properties you are interested in. The available properties will depend upon the [**DeviceInformationKind**](/uwp/api/Windows.Devices.Enumeration.DeviceInformationKind). See [Device information properties](device-information-properties.md) for more information.
-   The protocols you are querying over. This is only needed if you are searching for devices over a wireless or wired network. For more information about doing this, see [Enumerate devices over a network](enumerate-devices-over-a-network.md).

When using the [**Windows.Devices.Enumeration**](/uwp/api/Windows.Devices.Enumeration) APIs, you frequently combine the device selector with the device kind that you are interested in. The available list of device kinds is defined by the [**DeviceInformationKind**](/uwp/api/Windows.Devices.Enumeration.DeviceInformationKind) enumeration. This combination of factors helps you to limit the devices that are available to the ones that you are interested in. If you do not specify the **DeviceInformationKind**, or the method you are using does not provide a **DeviceInformationKind** parameter, the default kind is **DeviceInterface**.

The [**Windows.Devices.Enumeration**](/uwp/api/Windows.Devices.Enumeration) APIs use canonical AQS syntax, but not all of the operators are supported. For a list of properties that are available when you are constructing your filter string, see [Device information properties](device-information-properties.md).

**Caution**  Custom properties that are defined using the `{GUID} PID` format cannot be used when constructing your AQS filter string. This is because the property type is derived from the well-known property name.

 

The following table lists the AQS operators and what types of parameters they support.

| Operator                       | Supported types                                                             |
|--------------------------------|-----------------------------------------------------------------------------|
| **COP\_EQUAL**                 | String, boolean, GUID, UInt16, UInt32                                       |
| **COP\_NOTEQUAL**              | String, boolean, GUID, UInt16, UInt32                                       |
| **COP\_LESSTHAN**              | UInt16, UInt32                                                              |
| **COP\_GREATERTHAN**           | UInt16, UInt32                                                              |
| **COP\_LESSTHANOREQUAL**       | UInt16, UInt32                                                              |
| **COP\_GREATERTHANOREQUAL**    | UInt16, UInt32                                                              |
| **COP\_VALUE\_CONTAINS**       | String, string array, boolean array, GUID array, UInt16 array, UInt32 array |
| **COP\_VALUE\_NOTCONTAINS**    | String, string array, boolean array, GUID array, UInt16 array, UInt32 array |
| **COP\_VALUE\_STARTSWITH**     | String                                                                      |
| **COP\_VALUE\_ENDSWITH**       | String                                                                      |
| **COP\_DOSWILDCARDS**          | Not supported                                                               |
| **COP\_WORD\_EQUAL**           | Not supported                                                               |
| **COP\_WORD\_STARTSWITH**      | Not supported                                                               |
| **COP\_APPLICATION\_SPECIFIC** | Not supported                                                               |


> **Tip**  You can specify **NULL** for **COP\_EQUAL** or **COP\_NOTEQUAL**. This translates to a property with no value, or that the value does not exist. In AQS, you specify **NULL** by using empty brackets \[\].

> **Important**  When using the **COP\_VALUE\_CONTAINS** and **COP\_VALUE\_NOTCONTAINS** operators, they behave differently with strings and string arrays. In the case of a string, the system will perform a case-insensitive search to see if the device contains the indicated string as a substring. In the case of a string array, substrings are not searched. With the string array, the array is searched to see if it contains the entire specified string. It is not possible to search a string array to see if the elements in the array contain a substring.

If you cannot create a single AQS filter string that will scope your results appropriately, you can filter your results after you receive them. However, if you choose to do this, we recommend limiting the results from your initial AQS filter string as much as possible when you provide it to the [**Windows.Devices.Enumeration**](/uwp/api/Windows.Devices.Enumeration) APIs. This will help improve the performance of your application.

## AQS string examples

The following examples demonstrate how the AQS syntax can be used to limit the devices you want to enumerate. All of these filter strings are paired up with a [**DeviceInformationKind**](/uwp/api/Windows.Devices.Enumeration.DeviceInformationKind) to create a complete filter. If no kind is specified, remember that the default kind is **DeviceInterface**.

When this filter is paired with a [**DeviceInformationKind**](/uwp/api/Windows.Devices.Enumeration.DeviceInformationKind) of **DeviceInterface**, it enumerates all objects that contain the Audio Capture interface class and that are currently enabled. **=** translates to **COP\_EQUALS**.

``` syntax
System.Devices.InterfaceClassGuid:="{2eef81be-33fa-4800-9670-1cd474972c3f}" AND
System.Devices.InterfaceEnabled:=System.StructuredQueryType.Boolean#True
```

When this filter is paired with a [**DeviceInformationKind**](/uwp/api/Windows.Devices.Enumeration.DeviceInformationKind) of **Device**, it enumerates all objects that have at least one hardware id of GenCdRom. **~~** translates to **COP\_VALUE\_CONTAINS**.

``` syntax
System.Devices.HardwareIds:~~"GenCdRom"
```

When this filter is paired with a [**DeviceInformationKind**](/uwp/api/Windows.Devices.Enumeration.DeviceInformationKind) of **DeviceContainer**, it enumerates all objects that have a model name containing the substring Microsoft. **~~** translates to **COP\_VALUE\_CONTAINS**.

``` syntax
System.Devices.ModelName:~~"Microsoft"
```

When this filter is paired with a [**DeviceInformationKind**](/uwp/api/Windows.Devices.Enumeration.DeviceInformationKind) of **DeviceInterface**, it enumerates all objects that have a name starting with the substring Microsoft. **~&lt;** translates to **COP\_STARTSWITH**.

``` syntax
System.ItemNameDisplay:~<"Microsoft"
```

When this filter is paired with a [**DeviceInformationKind**](/uwp/api/Windows.Devices.Enumeration.DeviceInformationKind) of **Device**, it enumerates all objects that have a **System.Devices.IpAddress** property set. **&lt;&gt;\[\]** translates to **COP\_NOTEQUALS** combined with a **NULL** value.

``` syntax
System.Devices.IpAddress:<>[]
```

When this filter is paired with a [**DeviceInformationKind**](/uwp/api/Windows.Devices.Enumeration.DeviceInformationKind) of **Device**, it enumerates all objects that do not have a **System.Devices.IpAddress** property set. **=\[\]** translates to **COP\_EQUALS** combined with a **NULL** value.

``` syntax
System.Devices.IpAddress:=[]
```

 

 