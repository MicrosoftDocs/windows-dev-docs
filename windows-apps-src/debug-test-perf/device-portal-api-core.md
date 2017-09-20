---
author: muhsinking
ms.assetid: bfabd3d5-dd56-4917-9572-f3ba0de4f8c0
title: Device Portal core API reference
description: Learn about the Windows Device Portal core REST APIs that you can use to access the data and control your device programmatically.
ms.author: mukin
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---

# Device Portal core API reference

Everything in the Windows Device Portal is built on top of REST APIs that you can use to access the data and control your device programmatically.

## App deployment

---
### Install an app

**Request**

You can install an app by using the following request format.

Method      | Request URI
:------     | :-----
POST | /api/app/packagemanager/package
<br />
**URI parameters**

You can specify the following additional parameters on the request URI:

URI parameter | Description
:---          | :---
package   | (**required**) The file name of the package to be installed.
<br />
**Request headers**

- None

**Request body**

- The .appx or .appxbundle file, as well as any dependencies the app requires. 
- The certificate used to sign the app, if the device is IoT or Windows Desktop. Other platforms do not require the certificate. 

**Response**

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | Deploy request accepted and being processed
4XX | Error codes
5XX | Error codes
<br />
**Available device families**

* Windows Mobile
* Windows Desktop
* Xbox
* HoloLens
* IoT

---
### Get app installation status

**Request**

You can get the status of an app installation that is currently in progress by using the following request format.
 
Method      | Request URI
:------     | :-----
GET | /api/app/packagemanager/state
<br />
**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | The result of the last deployment
204 | The installation is running
404 | No installation action was found
<br />
**Available device families**

* Windows Mobile
* Windows Desktop
* Xbox
* HoloLens
* IoT

---
### Uninstall an app

**Request**

You can uninstall an app by using the following request format.
 
Method      | Request URI
:------     | :-----
DELETE | /api/app/packagemanager/package
<br />

**URI parameters**

URI parameter | Description
:---          | :---
package   | (**required**) The PackageFullName (from GET /api/app/packagemanager/packages) of the target app

**Request headers**

- None

**Request body**

- None

**Response**

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
4XX | Error codes
5XX | Error codes
<br />
**Available device families**

* Windows Mobile
* Windows Desktop
* Xbox
* HoloLens
* IoT

---
### Get installed apps

**Request**

You can get a list of apps installed on the system by using the following request format.
 
Method      | Request URI
:------     | :-----
GET | /api/app/packagemanager/packages
<br />

**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

The response includes a list of installed packages with associated details. The template for this response is as follows.
```
{"InstalledPackages": [
    {
        "Name": string,
        "PackageFamilyName": string,
        "PackageFullName": string,
        "PackageOrigin": int, (https://msdn.microsoft.com/en-us/library/windows/desktop/dn313167(v=vs.85).aspx)
        "PackageRelativeId": string,
        "Publisher": string,
        "Version": {
            "Build": int,
            "Major": int,
            "Minor": int,
            "Revision": int
     },
     "RegisteredUsers": [
     {
        "UserDisplayName": string,
        "UserSID": string
     },...
     ]
    },...
]}
```
**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
4XX | Error codes
5XX | Error codes
<br />
**Available device families**

* Windows Mobile
* Windows Desktop
* Xbox
* HoloLens
* IoT

---
## Device manager
---
### Get the installed devices on the machine

**Request**

You can get a list of devices that are installed on the machine by using the following request format.
 
Method      | Request URI
:------     | :-----
GET | /api/devicemanager/devices
<br />

**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

The response includes a JSON array of devices attached to the device.
``` 
{"DeviceList": [
    {
        "Class": string,
        "Description": string,
        "ID": string,
        "Manufacturer": string,
        "ParentID": string,
        "ProblemCode": int,
        "StatusCode": int
    },...
]}
```

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
4XX | Error codes
5XX | Error codes
<br />
**Available device families**

* Windows Mobile
* Windows Desktop
* IoT

---
## Dump collection
---
### Get the list of all crash dumps for apps

**Request**

You can get the list of all the available crash dumps for all sideloaded apps by using the following request format.
 
Method      | Request URI
:------     | :-----
GET | /api/debug/dump/usermode/dumps
<br />

**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

The response includes a list of crash dumps for each sideloaded application.

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
4XX | Error codes
5XX | Error codes
<br />
**Available device families**

* Window Mobile (in Windows Insider Program)
* Windows Desktop
* HoloLens
* IoT

---
### Get the crash dump collection settings for an app

**Request**

You can get the crash dump collection settings for a sideloaded app by using the following request format.
 
Method      | Request URI
:------     | :-----
GET | /api/debug/dump/usermode/crashcontrol
<br />

**URI parameters**

You can specify the following additional parameters on the request URI:

URI parameter | Description
:---          | :---
packageFullname   | (**required**) The full name of the package for the sideloaded app.
<br />
**Request headers**

- None

**Request body**

- None

**Response**

The response has the following format.
```
{"CrashDumpEnabled": bool}
```

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
4XX | Error codes
5XX | Error codes
<br />
**Available device families**

* Window Mobile (in Windows Insider Program)
* Windows Desktop
* HoloLens
* IoT

---
### Delete a crash dump for a sideloaded app

**Request**

You can delete a sideloaded app's crash dump by using the following request format.
 
Method      | Request URI
:------     | :-----
DELETE | /api/debug/dump/usermode/crashdump
<br />

**URI parameters**

You can specify the following additional parameters on the request URI:

URI parameter | Description
:---          | :---
packageFullname   | (**required**) The full name of the package for the sideloaded app.
fileName   | (**required**) The name of the dump file that should be deleted.
<br />
**Request headers**

- None

**Request body**

- None

**Response**

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
4XX | Error codes
5XX | Error codes
<br />
**Available device families**

* Window Mobile (in Windows Insider Program)
* Windows Desktop
* HoloLens
* IoT

---
### Disable crash dumps for a sideloaded app

**Request**

You can disable crash dumps for a sideloaded app by using the following request format.
 
Method      | Request URI
:------     | :-----
DELETE | /api/debug/dump/usermode/crashcontrol

<br />
**URI parameters**

You can specify the following additional parameters on the request URI:

URI parameter | Description
:---          | :---
packageFullname   | (**required**) The full name of the package for the sideloaded app.
<br />
**Request headers**

- None

**Request body**

- None

**Response**

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
4XX | Error codes
5XX | Error codes
<br />
**Available device families**

* Window Mobile (in Windows Insider Program)
* Windows Desktop
* HoloLens
* IoT

---
### Download the crash dump for a sideloaded app

**Request**

You can download a sideloaded app's crash dump by using the following request format.
 
Method      | Request URI
:------     | :-----
GET | /api/debug/dump/usermode/crashdump
<br />

**URI parameters**

You can specify the following additional parameters on the request URI:

URI parameter | Description
:---          | :---
packageFullname   | (**required**) The full name of the package for the sideloaded app.
fileName   | (**required**) The name of the dump file that you want to download.
<br />
**Request headers**

- None

**Request body**

- None

**Response**

The response includes a dump file. You can use WinDbg or Visual Studio to examine the dump file.

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
4XX | Error codes
5XX | Error codes
<br />
**Available device families**

* Window Mobile (in Windows Insider Program)
* Windows Desktop
* HoloLens
* IoT

---
### Enable crash dumps for a sideloaded app

**Request**

You can enable crash dumps for a sideloaded app by using the following request format.
 
Method      | Request URI
:------     | :-----
POST | /api/debug/dump/usermode/crashcontrol
<br />

**URI parameters**

You can specify the following additional parameters on the request URI:

URI parameter | Description
:---          | :---
packageFullname   | (**required**) The full name of the package for the sideloaded app.
<br />
**Request headers**

- None

**Request body**

- None

**Response**

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
<br />
**Available device families**

* Window Mobile (in Windows Insider Program)
* Windows Desktop
* HoloLens
* IoT

---
### Get the list of bugcheck files

**Request**

You can get the list of bugcheck minidump files by using the following request format.
 
Method      | Request URI
:------     | :-----
GET | /api/debug/dump/kernel/dumplist
<br />

**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

The response includes a list of dump file names and the sizes of these files. This list will be in the following format. 
```
{"DumpFiles": [
    {
        "FileName": string,
        "FileSize": int
    },...
]}
```

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
<br />
**Available device families**

* Windows Desktop
* IoT

---
### Download a bugcheck dump file

**Request**

You can download a bugcheck dump file by using the following request format.
 
Method      | Request URI
:------     | :-----
GET | /api/debug/dump/kernel/dump
<br />

**URI parameters**

You can specify the following additional parameters on the request URI:

URI parameter | Description
:---          | :---
filename   | (**required**) The file name of the dump file. You can find this by using the API to get the dump list.
<br />
**Request headers**

- None

**Request body**

- None

**Response**

The response includes the dump file. You can inspect this file using WinDbg.

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
4XX | Error codes
5XX | Error codes
<br />
**Available device families**

* Windows Desktop
* IoT

---
### Get the bugcheck crash control settings

**Request**

You can get the bugcheck crash control settings by using the following request format.
 
Method      | Request URI
:------     | :-----
GET | /api/debug/dump/kernel/crashcontrol

<br />
**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

The response includes the crash control settings. For more information about CrashControl, see the [CrashControl](https://technet.microsoft.com/library/cc951703.aspx) article. The template for the response is as follows.
```
{
    "autoreboot": bool (0 or 1),
    "dumptype": int (0 to 4),
    "maxdumpcount": int,
    "overwrite": bool (0 or 1)
}
```

**Dump types**

0: Disabled

1: Complete memory dump (collects all in-use memory)

2: Kernel memory dump (ignores user mode memory)

3: Limited kernel minidump

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
4XX | Error codes
5XX | Error codes
<br />
**Available device families**

* Windows Desktop
* IoT

---
### Get a live kernel dump

**Request**

You can get a live kernel dump by using the following request format.
 
Method      | Request URI
:------     | :-----
GET | /api/debug/dump/livekernel
<br />

**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

The response includes the full kernel mode dump. You can inspect this file using WinDbg.

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
4XX | Error codes
5XX | Error codes
<br />
**Available device families**

* Windows Desktop
* IoT

---
### Get a dump from a live user process

**Request**

You can get the dump for live user process by using the following request format.
 
Method      | Request URI
:------     | :-----
GET | /api/debug/dump/usermode/live
<br />

**URI parameters**

You can specify the following additional parameters on the request URI:

URI parameter | Description
:---          | :---
pid   | (**required**) The unique process id for the process you are interested in.
<br />
**Request headers**

- None

**Request body**

- None

**Response**

The response includes the process dump. You can inspect this file using WinDbg or Visual Studio.

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
4XX | Error codes
5XX | Error codes
<br />
**Available device families**

* Windows Desktop
* IoT

---
### Set the bugcheck crash control settings

**Request**

You can set the settings for collecting bugcheck data by using the following request format.
 
Method      | Request URI
:------     | :-----
POST | /api/debug/dump/kernel/crashcontrol
<br />

**URI parameters**

You can specify the following additional parameters on the request URI:

URI parameter | Description
:---          | :---
autoreboot   | (**optional**) True or false. This indicates whether the system restarts automatically after it fails or locks.
dumptype   | (**optional**) The dump type. For the supported values, see the [CrashDumpType Enumeration](https://msdn.microsoft.com/library/azure/microsoft.azure.management.insights.models.crashdumptype.aspx).
maxdumpcount   | (**optional**) The maximum number of dumps to save.
overwrite   | (**optional**) True of false. This indicates whether or not to overwrite old dumps when the dump counter limit specified by *maxdumpcount* has been reached.
<br />
**Request headers**

- None

**Request body**

- None

**Response**

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
4XX | Error codes
5XX | Error codes
<br />
**Available device families**

* Windows Desktop
* IoT

---
## ETW
---
### Create a realtime ETW session over a websocket

**Request**

You can create a realtime ETW session by using the following request format. This will be managed over a websocket.  ETW events are batched on the server and sent to the client once per second. 
 
Method      | Request URI
:------     | :-----
GET/WebSocket | /api/etw/session/realtime
<br />

**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

The response includes the ETW events from the enabled providers.  See ETW WebSocket commands below. 

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
4XX | Error codes
5XX | Error codes
<br />
**Available device families**

* Windows Mobile
* Windows Desktop
* HoloLens
* IoT

### ETW WebSocket commands
These commands are sent from the client to the server.

Command | Description
:----- | :-----
provider *{guid}* enable *{level}* | Enable the provider marked by *{guid}* (without brackets) at the specified level. *{level}* is an **int** from 1 (least detail) to 5 (verbose).
provider *{guid}* disable | Disable the provider marked by *{guid}* (without brackets).

This responses is sent from the server to the client. This is sent as text and you get the following format by parsing the JSON.
```
{
    "Events":[
        {
            "Timestamp": int,
            "ProviderName": string,
            "ID": int, 
            "TaskName": string,
            "Keyword": int,
            "Level": int,
            payload objects...
        },...
    ],
    "Frequency": int
}
```

Payload objects are extra key-value pairs (string:string) that are provided in the original ETW event.

Example:
```
{
    "ID" : 42, 
    "Keyword" : 9223372036854775824, 
    "Level" : 4, 
    "Message" : "UDPv4: 412 bytes transmitted from 10.81.128.148:510 to 132.215.243.34:510. ",
    "PID" : "1218", 
    "ProviderName" : "Microsoft-Windows-Kernel-Network", 
    "TaskName" : "KERNEL_NETWORK_TASK_UDPIP", 
    "Timestamp" : 131039401761757686, 
    "connid" : "0", 
    "daddr" : "132.245.243.34", 
    "dport" : "500", 
    "saddr" : "10.82.128.118", 
    "seqnum" : "0", 
    "size" : "412", 
    "sport" : "500"
}
```

---
### Enumerate the registered ETW providers

**Request**

You can enumerate through the registered providers by using the following request format.
 
Method      | Request URI
:------     | :-----
GET | /api/etw/providers
<br />

**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

The response includes the list of ETW providers. The list will include the friendly name and GUID for each provider in the following format.
```
{"Providers": [
    {
        "GUID": string, (GUID)
        "Name": string
    },...
]}
```

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
<br />
**Available device families**

* Windows Mobile
* Windows Desktop
* HoloLens
* IoT

---
### Enumerate the custom ETW providers exposed by the platform.

**Request**

You can enumerate through the registered providers by using the following request format.
 
Method      | Request URI
:------     | :-----
GET | /api/etw/customproviders
<br />

**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

200 OK. The response includes the list of ETW providers. The list will include the friendly name and GUID for each provider.

```
{"Providers": [
    {
        "GUID": string, (GUID)
        "Name": string
    },...
]}
```

**Status code**

- Standard status codes.
<br />
**Available device families**

* Windows Mobile
* Windows Desktop
* HoloLens
* IoT

---
## Location
---

### Get location override mode

**Request**

You can get the device's location stack override status by using the following request format. Developer mode must be on for this call to succeed.
 
Method      | Request URI
:------     | :-----
GET | /ext/location/override
<br />

**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

The response includes the override state of the device in the following format. 

```
{"Override" : bool}
```

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
4XX | Error codes
5XX | Error codes
<br />
**Available device families**

* Windows Mobile
* Windows Desktop
* Xbox
* HoloLens
* IoT

### Set location override mode

**Request**

You can set the device's location stack override status by using the following request format. When enabled, the location stack allows position injection. Developer mode must be on for this call to succeed.

Method      | Request URI
:------     | :-----
PUT | /ext/location/override
<br />

**URI parameters**

- None

**Request headers**

- None

**Request body**

```
{"Override" : bool}
```

**Response**

The response includes the override state that the device has been set to in the following format. 

```
{"Override" : bool}
```

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
4XX | Error codes
5XX | Error codes
<br />
**Available device families**

* Windows Mobile
* Windows Desktop
* Xbox
* HoloLens
* IoT

### Get the injected position

**Request**

You can get the device's injected (spoofed) location by using the following request format. An injected location must be set, or an error will be thrown.
 
Method      | Request URI
:------     | :-----
GET | /ext/location/position
<br />

**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

The response includes the current injected latitude and longitude values in the following format. 

```
{
    "Latitude" : double,
    "Longitude : double
}
```

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
4XX | Error codes
5XX | Error codes
<br />
**Available device families**

* Windows Mobile
* Windows Desktop
* Xbox
* HoloLens
* IoT

### Set the injected position

**Request**

You can set the device's injected (spoofed) location by using the following request format. Location override mode must first be enabled on the device, and the set location must be a valid location or an error will be thrown.

Method      | Request URI
:------     | :-----
PUT | /ext/location/override
<br />

**URI parameters**

- None

**Request headers**

- None

**Request body**

```
{
    "Latitude" : double,
    "Longitude : double
}
```

**Response**

The response includes the location that has been set in the following format. 

```
{
    "Latitude" : double,
    "Longitude : double
}
```

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
4XX | Error codes
5XX | Error codes
<br />
**Available device families**

* Windows Mobile
* Windows Desktop
* Xbox
* HoloLens
* IoT

---
## OS information
---
### Get the machine name

**Request**

You can get the name of a machine by using the following request format.
 
Method      | Request URI
:------     | :-----
GET | /api/os/machinename
<br />

**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

The response includes the computer name in the following format. 

```
{"ComputerName": string}
```

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
4XX | Error codes
5XX | Error codes
<br />
**Available device families**

* Windows Mobile
* Windows Desktop
* Xbox
* HoloLens
* IoT

---
### Get the operating system information

**Request**

You can get the OS information for a machine by using the following request format.
 
Method      | Request URI
:------     | :-----
GET | /api/os/info
<br />

**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

The response includes the OS information in the following format.

```
{
    "ComputerName": string,
    "OsEdition": string,
    "OsEditionId": int,
    "OsVersion": string,
    "Platform": string
}
```

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
4XX | Error codes
5XX | Error codes
<br />
**Available device families**

* Windows Mobile
* Windows Desktop
* Xbox
* HoloLens
* IoT

---
### Get the device family 

**Request**

You can get the device family (Xbox, phone, desktop, etc) using the following request format.
 
Method      | Request URI
:------     | :-----
GET | /api/os/devicefamily
<br />

**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

The response includes the device family (SKU - Desktop, Xbox, etc).

```
{
   "DeviceType" : string
}
```

DeviceType will look like "Windows.Xbox", "Windows.Desktop", etc. 

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
4XX | Error codes
5XX | Error codes

**Available device families**

* Windows Mobile
* Windows Desktop
* Xbox
* HoloLens
* IoT

---
### Set the machine name

**Request**

You can set the name of a machine by using the following request format.
 
Method      | Request URI
:------     | :-----
POST | /api/os/machinename
<br />

**URI parameters**

You can specify the following additional parameters on the request URI:

URI parameter | Description
:---          | :---
name | (**required**) The new name for the machine.
<br />
**Request headers**

- None

**Request body**

- None

**Response**

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
<br />
**Available device families**

* Windows Mobile
* Windows Desktop
* Xbox
* HoloLens
* IoT

---
## User information
---
### Get the active user

**Request**

You can get the name of the active user on the device by using the following request format.
 
Method      | Request URI
:------     | :-----
GET | /api/users/activeuser
<br />

**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

The response includes user information in the following format. 

On success: 
```
{
    "UserDisplayName" : string, 
    "UserSID" : string
}
```
On failure:
```
{
    "Code" : int, 
    "CodeText" : string, 
    "Reason" : string, 
    "Success" : bool
}
```

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
4XX | Error codes
5XX | Error codes
<br />
**Available device families**

* Windows Desktop
* HoloLens
* IoT

---
## Performance data
---
### Get the list of running processes

**Request**

You can get the list of currently running processes by using the following request format.  this can be upgraded to a WebSocket connection as well, with the same JSON data being pushed to the client once per second. 
 
Method      | Request URI
:------     | :-----
GET | /api/resourcemanager/processes
GET/WebSocket | /api/resourcemanager/processes
<br />

**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

The response includes a list of processes with details for each process. The information is in JSON format and has the following template.
```
{"Processes": [
    {
        "CPUUsage": float,
        "ImageName": string,
        "PageFileUsage": long,
        "PrivateWorkingSet": long,
        "ProcessId": int,
        "SessionId": int,
        "UserName": string,
        "VirtualSize": long,
        "WorkingSetSize": long
    },...
]}
```

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
4XX | Error codes
5XX | Error codes
<br />
**Available device families**

* Windows Mobile
* Windows Desktop
* HoloLens
* IoT

---
### Get the system performance statistics

**Request**

You can get the system performance statistics by using the following request format. This includes information such as read and write cycles and how much memory has been used.
 
Method      | Request URI
:------     | :-----
GET | /api/resourcemanager/systemperf
GET/WebSocket | /api/resourcemanager/systemperf
<br />
This can also be upgraded to a WebSocket connection.  It provides the same JSON data below once every second. 

**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

The response includes the performance statistics for the system such as CPU and GPU usage, memory access, and network access. This information is in JSON format and has the following template.
```
{
    "AvailablePages": int,
    "CommitLimit": int,
    "CommittedPages": int,
    "CpuLoad": int,
    "IOOtherSpeed": int,
    "IOReadSpeed": int,
    "IOWriteSpeed": int,
    "NonPagedPoolPages": int,
    "PageSize": int,
    "PagedPoolPages": int,
    "TotalInstalledInKb": int,
    "TotalPages": int,
    "GPUData": 
    {
        "AvailableAdapters": [{ (One per detected adapter)
            "DedicatedMemory": int,
            "DedicatedMemoryUsed": int,
            "Description": string,
            "SystemMemory": int,
            "SystemMemoryUsed": int,
            "EnginesUtilization": [ float,... (One per detected engine)]
        },...
    ]},
    "NetworkingData": {
        "NetworkInBytes": int,
        "NetworkOutBytes": int
    }
}
```

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
4XX | Error codes
5XX | Error codes
<br />
**Available device families**

* Windows Mobile
* Windows Desktop
* Xbox
* HoloLens
* IoT

---
## Power
---
### Get the current battery state

**Request**

You can get the current state of the battery by using the following request format.
 
Method      | Request URI
:------     | :-----
GET | /api/power/battery
<br />

**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

The current battery state information is returned using the following format.
```
{
    "AcOnline": int (0 | 1),
    "BatteryPresent": int (0 | 1),
    "Charging": int (0 | 1),
    "DefaultAlert1": int,
    "DefaultAlert2": int,
    "EstimatedTime": int,
    "MaximumCapacity": int,
    "RemainingCapacity": int
}
```

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
4XX | Error codes
5XX | Error codes
<br />
**Available device families**

* Windows Mobile
* Windows Desktop
* HoloLens
* IoT

---
### Get the active power scheme

**Request**

You can get the active power scheme by using the following request format.
 
Method      | Request URI
:------     | :-----
GET | /api/power/activecfg
<br />

**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

The active power scheme has the following format.
```
{"ActivePowerScheme": string (guid of scheme)}
```

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
4XX | Error codes
5XX | Error codes
<br />
**Available device families**

* Windows Desktop
* IoT

---
### Get the sub-value for a power scheme

**Request**

You can get the sub-value for a power scheme by using the following request format.
 
Method      | Request URI
:------     | :-----
GET | /api/power/cfg/*<power scheme path>*
<br />
Options:
- SCHEME_CURRENT

**URI parameters**

- None

**Request headers**

- None

**Request body**

A full listing of power states available is on a per-application basis and the settings for flagging various power states like low and critical batterty. 

**Response**

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
4XX | Error codes
5XX | Error codes
<br />
**Available device families**

* Windows Desktop
* IoT

---
### Get the power state of the system

**Request**

You can check the power state of the system by using the following request format. This will let you check to see if it is in a low power state.
 
Method      | Request URI
:------     | :-----
GET | /api/power/state
<br />

**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

The power state information has the following template.
```
{"LowPowerStateAvailable": bool}
```

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
4XX | Error codes
5XX | Error codes
<br />
**Available device families**

* Windows Desktop
* HoloLens
* IoT

---
### Set the active power scheme

**Request**

You can set the active power scheme by using the following request format.
 
Method      | Request URI
:------     | :-----
POST | /api/power/activecfg
<br />

**URI parameters**

You can specify the following additional parameters on the request URI:

URI parameter | Description
:---          | :---
scheme | (**required**) The GUID of the scheme you want to set as the active power scheme for the system.
<br />
**Request headers**

- None

**Request body**

- None

**Response**

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
4XX | Error codes
5XX | Error codes
<br />
**Available device families**

* Windows Desktop
* IoT

---
### Set the sub-value for a power scheme

**Request**

You can set the sub-value for a power scheme by using the following request format.
 
Method      | Request URI
:------     | :-----
POST | /api/power/cfg/*<power scheme path>*
<br />

**URI parameters**

You can specify the following additional parameters on the request URI:

URI parameter | Description
:---          | :---
valueAC | (**required**) The value to use for A/C power.
valueDC | (**required**) The value to use for battery power.
<br />
**Request headers**

- None

**Request body**

- None

**Response**

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
<br />
**Available device families**

* Windows Desktop
* IoT

---
### Get a sleep study report

**Request**

Method      | Request URI
:------     | :-----
GET | /api/power/sleepstudy/report
<br />
You can get a sleep study report by using the following request format.

**URI parameters**
URI parameter | Description
:---          | :---
FileName | (**required**) The full name for the file you want to download. This value should be hex64 encoded.
<br />
**Request headers**

- None

**Request body**

- None

**Response**

The response is a file containing the sleep study. 

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
4XX | Error codes
5XX | Error codes
<br />
**Available device families**

* Windows Desktop
* IoT

---
### Enumerate the available sleep study reports

**Request**

You can enumerate the available sleep study reports by using the following request format.
 
Method      | Request URI
:------     | :-----
GET | /api/power/sleepstudy/reports
<br />

**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

The list of available reports has the following template.

```
{"Reports": [
    {
        "FileName": string
    },...
]}
```

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
4XX | Error codes
5XX | Error codes
<br />
**Available device families**

* Windows Desktop
* IoT

---
### Get the sleep study transform

**Request**

You can get the sleep study transform by using the following request format. This transform is an XSLT that converts the sleep study report into an XML format that can be read by a person.
 
Method      | Request URI
:------     | :-----
GET | /api/power/sleepstudy/transform
<br />

**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

The response contains the sleep study transform.

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
4XX | Error codes
5XX | Error codes
<br />
**Available device families**

* Windows Desktop
* IoT

---
## Remote control
---
### Restart the target computer

**Request**

You can restart the target computer by using the following request format.
 
Method      | Request URI
:------     | :-----
POST | /api/control/restart
<br />

**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
<br />
**Available device families**

* Windows Mobile
* Windows Desktop
* Xbox
* HoloLens
* IoT

---
### Shut down the target computer

**Request**

You can shut down the target computer by using the following request format.
 
Method      | Request URI
:------     | :-----
POST | /api/control/shutdown
<br />

**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
4XX | Error codes
5XX | Error codes
<br />
**Available device families**

* Windows Mobile
* Windows Desktop
* Xbox
* HoloLens
* IoT

---
## Task manager
---
### Start a modern app

**Request**

You can start a modern app by using the following request format.
 
Method      | Request URI
:------     | :-----
POST | /api/taskmanager/app
<br />

**URI parameters**

You can specify the following additional parameters on the request URI:

URI parameter | Description
:---          | :---
appid   | (**required**) The PRAID for the app you want to start. This value should be hex64 encoded.
package   | (**required**) The full name for the app package you want to start. This value should be hex64 encoded.
<br />
**Request headers**

- None

**Request body**

- None

**Response**

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
4XX | Error codes
5XX | Error codes
<br />
**Available device families**

* Windows Mobile
* Windows Desktop
* Xbox
* HoloLens
* IoT

---
### Stop a modern app

**Request**

You can stop a modern app by using the following request format.
 
Method      | Request URI
:------     | :-----
DELETE | /api/taskmanager/app
<br />

**URI parameters**

You can specify the following additional parameters on the request URI:

URI parameter | Description
:---          | :---
package   | (**required**) The full name of the app packages that you want to stop. This value should be hex64 encoded.
forcestop   | (**optional**) A value of **yes** indicates that the system should force all processes to stop.
<br />
**Request headers**

- None

**Request body**

- None

**Response**

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
4XX | Error codes
5XX | Error codes
<br />
**Available device families**

* Windows Mobile
* Windows Desktop
* Xbox
* HoloLens
* IoT

---
### Kill process by PID

**Request**

You can kill a process by using the following request format.
 
Method      | Request URI
:------     | :-----
DELETE | /api/taskmanager/process
<br />

**URI parameters**

You can specify the following additional parameters on the request URI:

URI parameter | Description
:---          | :---
pid   | (**required**) The unique process id for the process to stop.
<br />
**Request headers**

- None

**Request body**

- None

**Response**

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
4XX | Error codes
5XX | Error codes
<br />
**Available device families**

* Windows Desktop
* HoloLens
* IoT

---
## Networking
---
### Get the current IP configuration

**Request**

You can get the current IP configuration by using the following request format.
 
Method      | Request URI
:------     | :-----
GET | /api/networking/ipconfig
<br />

**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

The response includes the IP configuration in the following template.

```
{"Adapters": [
    {
        "Description": string,
        "HardwareAddress": string,
        "Index": int,
        "Name": string,
        "Type": string,
        "DHCP": {
            "LeaseExpires": int, (timestamp)
            "LeaseObtained": int, (timestamp)
            "Address": {
                "IpAddress": string,
                "Mask": string
            }
        },
        "WINS": {(WINS is optional)
            "Primary": {
                "IpAddress": string,
                "Mask": string
            },
            "Secondary": {
                "IpAddress": string,
                "Mask": string
            }
        },
        "Gateways": [{ (always 1+)
            "IpAddress": "10.82.128.1",
            "Mask": "255.255.255.255"
            },...
        ],
        "IpAddresses": [{ (always 1+)
            "IpAddress": "10.82.128.148",
            "Mask": "255.255.255.0"
            },...
        ]
    },...
]}
```

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
4XX | Error codes
5XX | Error codes
<br />
**Available device families**

* Windows Mobile
* Windows Desktop
* Xbox
* HoloLens
* IoT

--
### Enumerate wireless network interfaces

**Request**

You can enumerate the available wireless network interfaces by using the following request format.
 
Method      | Request URI
:------     | :-----
GET | /api/wifi/interfaces
<br />

**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

A list of the available wireless interfaces with details in the following format.

``` 
{"Interfaces": [{
    "Description": string,
    "GUID": string (guid with curly brackets),
    "Index": int,
    "ProfilesList": [
        {
            "GroupPolicyProfile": bool,
            "Name": string, (Network currently connected to)
            "PerUserProfile": bool
        },...
    ]
    }
]}
```

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
4XX | Error codes
5XX | Error codes
<br />
**Available device families**

* Windows Mobile
* Windows Desktop
* Xbox
* HoloLens
* IoT

---
### Enumerate wireless networks

**Request**

You can enumerate the list of wireless networks on the specified interface by using the following request format.
 
Method      | Request URI
:------     | :-----
GET | /api/wifi/networks
<br />

**URI parameters**

You can specify the following additional parameters on the request URI:

URI parameter | Description
:---          | :---
interface   | (**required**) The GUID for the network interface to use to search for wireless networks, without brackets. 
<br />
**Request headers**

- None

**Request body**

- None

**Response**

The list of wireless networks found on the provided *interface*. This includes details for the networks in the following format.

```
{"AvailableNetworks": [
    {
        "AlreadyConnected": bool,
        "AuthenticationAlgorithm": string, (WPA2, etc)
        "Channel": int,
        "CipherAlgorithm": string, (e.g. AES)
        "Connectable": int, (0 | 1)
        "InfrastructureType": string,
        "ProfileAvailable": bool,
        "ProfileName": string,
        "SSID": string,
        "SecurityEnabled": int, (0 | 1)
        "SignalQuality": int,
        "BSSID": [int,...],
        "PhysicalTypes": [string,...]
    },...
]}
```

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
4XX | Error codes
5XX | Error codes
<br />
**Available device families**

* Windows Mobile
* Windows Desktop
* Xbox
* HoloLens
* IoT

---
### Connect and disconnect to a Wi-Fi network.

**Request**

You can connect or disconnect to a Wi-Fi network by using the following request format.
 
Method      | Request URI
:------     | :-----
POST | /api/wifi/network
<br />

**URI parameters**

You can specify the following additional parameters on the request URI:

URI parameter | Description
:---          | :---
interface   | (**required**) The GUID for the network interface you use to connect to the network.
op   | (**required**) Indicates the action to take. Possible values are connect or disconnect.
ssid   | (**required if *op* == connect**) The SSID to connect to.
key   | (**required if *op* == connect and network requires authentication**) The shared key.
createprofile | (**required**) Create a profile for the network on the device.  This will cause the device to auto-connect to the network in the future. This can be **yes** or **no**. 

**Request headers**

- None

**Request body**

- None

**Response**

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
<br />
**Available device families**

* Windows Mobile
* Windows Desktop
* Xbox
* HoloLens
* IoT

---
### Delete a Wi-Fi profile

**Request**

You can delete a profile associated with a network on a specific interface by using the following request format.
 
Method      | Request URI
:------     | :-----
DELETE | /api/wifi/network
<br />

**URI parameters**

You can specify the following additional parameters on the request URI:

URI parameter | Description
:---          | :---
interface   | (**required**) The GUID for the network interface associated with the profile to delete.
profile   | (**required**) The name of the profile to delete.
<br />
**Request headers**

- None

**Request body**

- None

**Response**

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
<br />
**Available device families**

* Windows Mobile
* Windows Desktop
* Xbox
* HoloLens
* IoT

---
## Windows Error Reporting (WER)
---
### Download a Windows error reporting (WER) file

**Request**

You can download a WER-related file by using the following request format.
 
Method      | Request URI
:------     | :-----
GET | /api/wer/report/file
<br />

**URI parameters**

You can specify the following additional parameters on the request URI:

URI parameter | Description
:---          | :---
user   | (**required**) The user name associated with the report.
type   | (**required**) The type of report. This can be either **queried** or **archived**.
name   | (**required**) The name of the report. This should be base64 encoded. 
file   | (**required**) The name of the file to download from the report. This should be base64 encoded. 
<br />
**Request headers**

- None

**Request body**

- None

**Response**

- Response contains the requested file. 

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
4XX | Error codes
5XX | Error codes
<br />
**Available device families**

* Windows Desktop
* HoloLens
* IoT

---
### Enumerate files in a Windows error reporting (WER) report

**Request**

You can enumerate the files in a WER report by using the following request format.
 
Method      | Request URI
:------     | :-----
GET | /api/wer/report/files
<br />

**URI parameters**

You can specify the following additional parameters on the request URI:

URI parameter | Description
:---          | :---
user   | (**required**) The user associated with the report.
type   | (**required**) The type of report. This can be either **queried** or **archived**.
name   | (**required**) The name of the report. This should be base64 encoded. 
<br />
**Request headers**

- None

**Request body**

```
{"Files": [
    {
        "Name": string, (Filename, not base64 encoded)
        "Size": int (bytes)
    },...
]}
```

**Response**

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
4XX | Error codes
5XX | Error codes
<br />
**Available device families**

* Windows Desktop
* HoloLens
* IoT

---
### List the Windows error reporting (WER) reports

**Request**

You can get the WER reports by using the following request format.
 
Method      | Request URI
:------     | :-----
GET | /api/wer/reports
<br />

**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

The WER reports in the following format.

```
{"WerReports": [
    {
        "User": string,
        "Reports": [
            {
                "CreationTime": int,
                "Name": string, (not base64 encoded)
                "Type": string ("Queue" or "Archive")
            },
    },...
]}
```

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
4XX | Error codes
5XX | Error codes
<br />
**Available device families**

* Windows Desktop
* HoloLens
* IoT

---
## Windows Performance Recorder (WPR) 
---
### Start tracing with a custom profile

**Request**

You can upload a WPR profile and start tracing using that profile by using the following request format.  Only one trace can run at a time. The profile will not remain on the device. 
 
Method      | Request URI
:------     | :-----
POST | /api/wpr/customtrace
<br />

**URI parameters**

- None

**Request headers**

- None

**Request body**

- A multi-part conforming http body that contains the custom WPR profile.

**Response**

The WPR session status in the following format.

```
{
    "SessionType": string, (Running or Idle) 
    "State": string (normal or boot)
}
```

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
4XX | Error codes
5XX | Error codes
<br />
**Available device families**

* Windows Mobile
* Windows Desktop
* HoloLens
* IoT

---
### Start a boot performance tracing session

**Request**

You can start a boot WPR tracing session by using the following request format. This is also known as a performance tracing session.
 
Method      | Request URI
:------     | :-----
POST | /api/wpr/boottrace
<br />

**URI parameters**

You can specify the following additional parameters on the request URI:

URI parameter | Description
:---          | :---
profile   | (**required**) This parameter is required on start. The name of the profile that should start a performance tracing session. The possible profiles are stored in perfprofiles/profiles.json.
<br />
**Request headers**

- None

**Request body**

- None

**Response**

On start, this API returns the WPR session status in the following format.

```
{
    "SessionType": string, (Running or Idle) 
    "State": string (boot)
}
```

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
4XX | Error codes
5XX | Error codes
<br />
**Available device families**

* Windows Mobile
* Windows Desktop
* HoloLens
* IoT

---
### Stop a boot performance tracing session

**Request**

You can stop a boot WPR tracing session by using the following request format. This is also known as a performance tracing session.
 
Method      | Request URI
:------     | :-----
GET | /api/wpr/boottrace
<br />

**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

-  None.  **Note:** This is a long running operation.  It will return when the ETL is finished writing to disk.

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
4XX | Error codes
5XX | Error codes
<br />
**Available device families**

* Windows Mobile
* Windows Desktop
* HoloLens
* IoT

---
### Start a performance tracing session

**Request**

You can start a WPR tracing session by using the following request format. This is also known as a performance tracing session.  Only one trace can run at a time. 
 
Method      | Request URI
:------     | :-----
POST | /api/wpr/trace
<br />

**URI parameters**

You can specify the following additional parameters on the request URI:

URI parameter | Description
:---          | :---
profile   | (**required**) The name of the profile that should start a performance tracing session. The possible profiles are stored in perfprofiles/profiles.json.
<br />
**Request headers**

- None

**Request body**

- None

**Response**

On start, this API returns the WPR session status in the following format.

```
{
    "SessionType": string, (Running or Idle) 
    "State": string (normal)
}
```

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
4XX | Error codes
5XX | Error codes
<br />
**Available device families**

* Windows Mobile
* Windows Desktop
* HoloLens
* IoT

---
### Stop a performance tracing session

**Request**

You can stop a WPR tracing session by using the following request format. This is also known as a performance tracing session.
 
Method      | Request URI
:------     | :-----
GET | /api/wpr/trace
<br />

**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

- None.  **Note:** This is a long running operation.  It will return when the ETL is finished writing to disk.  

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
4XX | Error codes
5XX | Error codes
<br />
**Available device families**

* Windows Mobile
* Windows Desktop
* HoloLens
* IoT

---
### Retrieve the status of a tracing session

**Request**

You can retrieve the status of the current WPR session by using the following request format.
 
Method      | Request URI
:------     | :-----
GET | /api/wpr/status
<br />

**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

The status of the WPR tracing session in the following format.

```
{
    "SessionType": string, (Running or Idle) 
    "State": string (normal or boot)
}
```

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
4XX | Error codes
5XX | Error codes
<br />
**Available device families**

* Windows Mobile
* Windows Desktop
* HoloLens
* IoT

---
### List completed tracing sessions (ETLs)

**Request**

You can get a listing of ETL traces on the device using the following request format. 

Method      | Request URI
:------     | :-----
GET | /api/wpr/tracefiles
<br />

**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

The listing of completed tracing sessions is provided in the following format.

```
{"Items": [{
    "CurrentDir": string (filepath),
    "DateCreated": int (File CreationTime),
    "FileSize": int (bytes),
    "Id": string (filename),
    "Name": string (filename),
    "SubPath": string (filepath),
    "Type": int
}]}
```

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
4XX | Error codes
5XX | Error codes
<br />
**Available device families**

* Windows Mobile
* Windows Desktop
* HoloLens
* IoT

---
### Download a tracing session (ETL)

**Request**

You can download a tracefile (boot trace or user-mode trace) using the following request format. 

Method      | Request URI
:------     | :-----
GET | /api/wpr/tracefile
<br />

**URI parameters**

You can specify the following additional parameter on the request URI:

URI parameter | Description
:---          | :---
filename   | (**required**) The name of the ETL trace to download.  These can be found in /api/wpr/tracefiles

**Request headers**

- None

**Request body**

- None

**Response**

- Returns the trace ETL file.

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
4XX | Error codes
5XX | Error codes
<br />
**Available device families**

* Windows Mobile
* Windows Desktop
* HoloLens
* IoT

---
### Delete a tracing session (ETL)

**Request**

You can delete a tracefile (boot trace or user-mode trace) using the following request format. 

Method      | Request URI
:------     | :-----
DELETE | /api/wpr/tracefile
<br />

**URI parameters**

You can specify the following additional parameter on the request URI:

URI parameter | Description
:---          | :---
filename   | (**required**) The name of the ETL trace to delete.  These can be found in /api/wpr/tracefiles

**Request headers**

- None

**Request body**

- None

**Response**

- Returns the trace ETL file.

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
4XX | Error codes
5XX | Error codes
<br />
**Available device families**

* Windows Mobile
* Windows Desktop
* HoloLens
* IoT

---
## DNS-SD Tags 
---
### View Tags

**Request**

View the currently applied tags for the device.  These are advertised via DNS-SD TXT records in the T key.  
 
Method      | Request URI
:------     | :-----
GET | /api/dns-sd/tags
<br />

**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**
The currently applied tags in the following format. 
```
 {
    "tags": [
        "tag1", 
        "tag2", 
        ...
     ]
}
```

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
5XX | Server Error 

<br />
**Available device families**

* Windows Mobile
* Windows Desktop
* Xbox
* HoloLens
* IoT

---
### Delete Tags

**Request**

Delete all tags currently advertised by DNS-SD.   
 
Method      | Request URI
:------     | :-----
DELETE | /api/dns-sd/tags
<br />

**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**
 - None

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
5XX | Server Error 

<br />
**Available device families**

* Windows Mobile
* Windows Desktop
* Xbox
* HoloLens
* IoT

---
### Delete Tag

**Request**

Delete a tag currently advertised by DNS-SD.   
 
Method      | Request URI
:------     | :-----
DELETE | /api/dns-sd/tag
<br />

**URI parameters**

URI parameter | Description
:------     | :-----
tagValue | (**required**) The tag to be removed.

**Request headers**

- None

**Request body**

- None

**Response**
 - None

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK

<br />
**Available device families**

* Windows Mobile
* Windows Desktop
* Xbox
* HoloLens
* IoT
 
---
### Add a Tag

**Request**

Add a tag to the DNS-SD advertisement.   
 
Method      | Request URI
:------     | :-----
POST | /api/dns-sd/tag
<br />

**URI parameters**

URI parameter | Description
:------     | :-----
tagValue | (**required**) The tag to be added.

**Request headers**

- None

**Request body**

- None

**Response**
 - None

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
401 | Tag space Overflow.  Results when the proposed tag is too long for the resulting DNS-SD service record.  

<br />
**Available device families**

* Windows Mobile
* Windows Desktop
* Xbox
* HoloLens
* IoT

## App File Explorer

---
### Get known folders

**Request**

Obtain a list of accessible top-level folders.

Method      | Request URI
:------     | :-----
GET | /api/filesystem/apps/knownfolders
<br />

**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**
The available folders in the following format. 
```
 {"KnownFolders": [
    "folder0",
    "folder1",...
]}
```
**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | Deploy request accepted and being processed
4XX | Error codes
5XX | Error codes
<br />

**Available device families**

* Windows Mobile
* Windows Desktop
* HoloLens
* Xbox
* IoT

---
### Get files

**Request**

Obtain a list of files in a folder.

Method      | Request URI
:------     | :-----
GET | /api/filesystem/apps/files
<br />

**URI parameters**

URI parameter | Description
:------     | :-----
knownfolderid | (**required**) The top-level directory where you want the list of files. Use **LocalAppData** for access to sideloaded apps. 
packagefullname | (**required if *knownfolderid* == LocalAppData**) The package full name of the app you are interested in. 
path | (**optional**) The sub-directory within the folder or package specified above. 

**Request headers**

- None

**Request body**

- None

**Response**
The available folders in the following format. 
```
{"Items": [
    {
        "CurrentDir": string (folder under the requested known folder),
        "DateCreated": int,
        "FileSize": int (bytes),
        "Id": string,
        "Name": string,
        "SubPath": string (present if this item is a folder, this is the name of the folder),
        "Type": int
    },...
]}
```
**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK
4XX | Error codes
5XX | Error codes
<br />
**Available device families**

* Windows Mobile
* Windows Desktop
* HoloLens
* Xbox
* IoT

---
### Download a file

**Request**

Obtain a file from a known folder or appLocalData.

Method      | Request URI
:------     | :-----
GET | /api/filesystem/apps/file

**URI parameters**

URI parameter | Description
:------     | :-----
knownfolderid | (**required**) The top-level directory where you want to download files. Use **LocalAppData** for access to sideloaded apps. 
filename | (**required**) The name of the file being downloaded. 
packagefullname | (**required if *knownfolderid* == LocalAppData**) The package full name you are interested in. 
path | (**optional**) The sub-directory within the folder or package specified above.

**Request headers**

- None

**Request body**

- The file requested, if present

**Response**

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | The requested file
404 | File not found
5XX | Error codes
<br />
**Available device families**

* Windows Mobile
* Windows Desktop
* HoloLens
* Xbox
* IoT

---
### Rename a file

**Request**

Rename a file in a folder.

Method      | Request URI
:------     | :-----
POST | /api/filesystem/apps/rename

<br />
**URI parameters**

URI parameter | Description
:------     | :-----
knownfolderid | (**required**) The top-level directory where the file is located. Use **LocalAppData** for access to sideloaded apps. 
filename | (**required**) The original name of the file being renamed. 
newfilename | (**required**) The new name of the file.
packagefullname | (**required if *knownfolderid* == LocalAppData**) The package full name of the app you are interested in. 
path | (**optional**) The sub-directory within the folder or package specified above. 

**Request headers**

- None

**Request body**

- None

**Response**

- None

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK. The file is renamed
404 | File not found
5XX | Error codes
<br />
**Available device families**

* Windows Mobile
* Windows Desktop
* HoloLens
* Xbox
* IoT

---
### Delete a file

**Request**

Delete a file in a folder.

Method      | Request URI
:------     | :-----
DELETE | /api/filesystem/apps/file
<br />
**URI parameters**

URI parameter | Description
:------     | :-----
knownfolderid | (**required**) The top-level directory where you want to delete files. Use **LocalAppData** for access to sideloaded apps. 
filename | (**required**) The name of the file being deleted. 
packagefullname | (**required if *knownfolderid* == LocalAppData**) The package full name of the app you are interested in. 
path | (**optional**) The sub-directory within the folder or package specified above.

**Request headers**

- None

**Request body**

- None

**Response**

- None 

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK. The file is deleted
404 | File not found
5XX | Error codes
<br />
**Available device families**

* Windows Mobile
* Windows Desktop
* HoloLens
* Xbox
* IoT

---
### Upload a file

**Request**

Upload a file to a folder.  This will overwrite an existing file with the same name, but will not create new folders. 

Method      | Request URI
:------     | :-----
POST | /api/filesystem/apps/file
<br />
**URI parameters**

URI parameter | Description
:------     | :-----
knownfolderid | (**required**) The top-level directory where you want to upload files. Use **LocalAppData** for access to sideloaded apps.
packagefullname | (**required if *knownfolderid* == LocalAppData**) The package full name of the app you are interested in. 
path | (**optional**) The sub-directory within the folder or package specified above.

**Request headers**

- None

**Request body**

- None

**Response**

**Status code**

This API has the following expected status codes.

HTTP status code      | Description
:------     | :-----
200 | OK. The file is uploaded
4XX | Error codes
5XX | Error codes
<br />
**Available device families**

* Windows Mobile
* Windows Desktop
* HoloLens
* Xbox
* IoT