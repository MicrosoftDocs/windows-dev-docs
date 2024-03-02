---
title: Cloud Data Store Settings Reader Tool (readCloudDataSettings.exe)
description: This article describes the Cloud Data Store Settings Reader Tool, readCloudDataSettings.exe, that can be used to fetch data stored within the Windows Cloud Data Store component on the local device.
ms.date: 02/27/2024
ms.topic: article
keywords: windows 10, windows 11, settings
ms.localizationpriority: medium
---

# Cloud Data Store Settings Reader tool (readCloudDataSettings.exe)

This article describes the Cloud Data Store Settings Reader Tool, readCloudDataSettings.exe, that can be used to fetch data stored within the Windows Cloud Data Store component on the local device. 

## Usage 

### Single instance items 

`readCloudDataSettings.exe get -type:<type name> [-account:<secondary account id>]`

### Multi-instance items 

`readCloudDataSettings.exe enum -type:<type name> [-collection:<collection name>] [-account:<secondary account id>]`

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
C:\> readCloudDataSettings.exe get /type:windows.data.platform.diagnostics.diagnosticdata
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
C:\> readCloudDataSettings.exe get /type:windows.data.platform.diagnostics.diagnosticdata -account:otheraccount@contoso.com 
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
C:\> readCloudDataSettings.exe enum /type:windows.data.samplemetadata.sampledataitem /collection:samples 
```

Output:

```powershell
/type: windows.data.samplemetadata.sampledataitem 
/collection: samples 

[    {"Data":{"identifier":{"Data":{"Data1":0,"Data2":0,"Data3":0,"Data4":0}},"itemName":"name","someArbitraryPriority":0,"scale":{"Data":{"x":1,"y":1,"z":1}},"rotationAxis":{"Data":{"x":2,"y":2,"z":2}},"translation":{"Data":{"x":3,"y":3,"z":3}},"rotationAngle":4,"isVisible":true,"projection":0}},    {"Data":{"identifier":{"Data":{"Data1":1,"Data2":1,"Data3":1,"Data4":1}},"itemName":"name2","someArbitraryPriority":0,"scale":{"Data":{"x":1.1,"y":1.1,"z":1.1}},"rotationAxis":{"Data":{"x":2.1,"y":2.1,"z":2.1}},"translation":{"Data":{"x":3.1,"y":3.1,"z":3.1}},"rotationAngle":4.1,"isVisible":true,"projection":1}} 
] 
```

## Note on serialization

For interoperability settings data structures are serialized to JSON when exported from Windows. In some cases, this can result in unintuitive results. See the examples below. 

### Basic data types
Basic data types (integer, floating point, string, etc) have no special representation beyond normal JSON encoding.

### Structures
All structures (including top level structures) are wrapped in a JSON element called "Data". This includes nested structures (including when structures are used in vectors, maps, and nullables)

#### Example

This Bond schema:

```csharp
struct MyInnerDataType
{
    0: int32 data;
}

struct MyDataType
{
    0: MyInnerDataType innerData;
    1: int64 id;
}
```

Becomes the following JSON:

```json
{
    "Data":
    {
        "innerData":
        {
            "Data":
            {
                "data": 1
            }
        },
        "id": 2
    }
}
```

### Vectors / Lists

Vectors and lists in Bond are translated into standard JSON arrays (with the caveat from above that a list of structures has each element wrapped in a JSON element named "Data").

#### Example

This Bond schema:

```csharp
struct MyInnerDataType
{
    0: int32 data;
}

struct MyDataType
{
    0: vector<MyInnerDataType> dataList;
    1: vector<int32> idList;
}
```

Becomes the following JSON:

```json
{
    "Data":
    {
        "dataList":
        [
            {
                "Data":
                {
                    "data": 1
                }
            },
            {
                "Data":
                {
                    "data": 2
                }
            },
        ],
        "idList": [ 1, 2 ]
    }
}
```

### Blob

Blobs are translated into arrays of integers

#### Example

This Bond schema:

```C#
Struct mySetting
{
0: blob settingData;
}
```

Becomes the following JSON:

```json
{
    "Data":
    {
        "settingData":[-103,84,-51,60,-88,-121,16,75,-94,21,96,-120,-120,-35,59,85,4,0,0,0,0,1,0,0,36,0,0,0,73,0,110,0,116,0,101,0,114,0,110,0,101,0,116,0,32,0,69,0,120,0,112,0,108,0,111,0,114,0,101,0,114,0,0,0,1,0,0,0,2,0,0,0,7,0,0,0,7,0,0,0,58,0,0,0,104,0,116,0,116,0,112,0,115,0,58,0,47,0,47,0,103,0,108,0,111,0,98,0,97,0,108,0,46,0,115,0,116,0,115,0,46,0,109,0,115,0,102,0,116,0,46,0,110,0,101,0,116,0,47,0,0,0,2,0,0,0,2,0,0,0,7,0,0,0,7,0,0,0,34,0,0,0,109,0,105,0,99,0,114,0,105,0,100,0,101,0,114,0,64,0,103,0,109,0,101,0,46,0,103,0,98,0,108,0,0,0,3,0,0,0,0,0,0,0,7,0,0,0,7,0,0,0,34,0,0,0,110,0,97,0,109,0,105,0,56,0,48,0,114,0,117,0,108,0,101,0,115,0,58,0,68,0,103,0,109,0,101,0,0,0,1,0,0,0,100,0,0,0,0,0,0,0,8,0,0,0,8,0,0,0,16,0,0,0,-43,-74,60,78,86,37,-40,76,-92,-115,-57,85,-57,55,-53,-90,5,0,0,0,0,0,0,0,8,0,0,0,0,0,0,0,1,0,0,0,7,0,0,0,0,0,0,0,2,0,0,0,7,0,0,0,0,0,0,0,3,0,0,0,7,0,0,0,0,0,0,0,4,0,0,0,6,0,0,0,-58,-104,104,-79,72,-95,103,73,-111,113,100,-41,85,-38,-123,32]
    }
}
```
