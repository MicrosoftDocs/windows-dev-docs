---
title: Cloud Data Store Settings Reader Tool (readsettingdata.exe)
description: Packaged Windows App SDK apps can leverage WinRT APIs for reading and writing app settings, file and folder pickers, and special sand-boxed locations such as the Video/Music library.
ms.date: 02/27/2024
ms.topic: article
keywords: windows 10, windows 11, settings
ms.localizationpriority: medium
---

# Cloud Data Store Settings Reader tool (readsettingdata.exe)

This article describes the Cloud Data Store Settings Reader Tool, readsettingdata.exe, that can be used to fetch data stored within the Windows Cloud Data Store component on the local device. 

## Usage 

### Single instance items 

`readsettingdata.exe get -type:<type name> [-account:<secondary account id>]`

### Multi-instance items 

`readsettingdata.exe enum -type:<type name> [-collection:<collection name>] [-account:<secondary account id>]`

### Command line parameter descriptions 

| Parameter | Description |
|-----------|-------------|
| `<type name>` | The name of a Cloud Data Store type whose data is to be retrieved (e.g., "windows.data.platform.diagnostics.diagnosticdata") |
| `<collection name>` | The optional name of a collection for a Cloud Data Store multi-instance type. This must be specified if the multi-instance type has a named collection and must not be specified if either the collection has no name or the type is single-instance. Cloud Data Store does has no support for enumerating either the data or names of all collections of a type. |
| `<secondary account id>` | The optional id (in the form of user@domain) of a secondary account associated with the current user whose data is to be fetched.  This must be a secondary account associated with the currently logged in Windows user; it does not provide access to data for other Windows users that might be sharing the device.|

## Errors 

If the data does not exist or an error occurs, the output will report a pair of square brackets with nothing between; example: 

```powershell
[ 
] 
```

## Examples

### Single-instance type

Command line:

```powershell
C:\> readsettingdata.exe get /type:windows.data.platform.diagnostics.diagnosticdata
```

Output:

```powershell
/type: windows.data.platform.diagnostics.diagnosticdata 

[ 
    {"Data":{"diagnosticMessage":"test message data with current user"}} 
] 
```

### Single-instance type, alternate account

Command line:

```powershell
C:\> readsettingdata.exe get /type:windows.data.platform.diagnostics.diagnosticdata -account:otheraccount@contoso.com 
```

Output:

```powershell
/type: windows.data.platform.diagnostics.diagnosticdata 

[ 
    {"Data":{"diagnosticMessage":"test message data for otheraccount@contoso.com associated with current user"}} 
] 
```

### Multi-instance type with collection name 

Command line:

```powershell
C:\> readsettingdata.exe enum /type:windows.data.samplemetadata.sampledataitem /collection:samples 
```

Output:

```powershell
/type: windows.data.samplemetadata.sampledataitem 
/collection: samples 

[    {"Data":{"identifier":{"Data":{"Data1":0,"Data2":0,"Data3":0,"Data4":0}},"itemName":"name","someArbitraryPriority":0,"scale":{"Data":{"x":1,"y":1,"z":1}},"rotationAxis":{"Data":{"x":2,"y":2,"z":2}},"translation":{"Data":{"x":3,"y":3,"z":3}},"rotationAngle":4,"isVisible":true,"projection":0}},    {"Data":{"identifier":{"Data":{"Data1":1,"Data2":1,"Data3":1,"Data4":1}},"itemName":"name2","someArbitraryPriority":0,"scale":{"Data":{"x":1.1,"y":1.1,"z":1.1}},"rotationAxis":{"Data":{"x":2.1,"y":2.1,"z":2.1}},"translation":{"Data":{"x":3.1,"y":3.1,"z":3.1}},"rotationAngle":4.1,"isVisible":true,"projection":1}} 
] 
```


