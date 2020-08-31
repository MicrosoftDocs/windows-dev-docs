---
ms.assetid: bfabd3d5-dd56-4917-9572-f3ba0de4f8c0
title: Device Portal core API reference
description: Learn about the Windows Device Portal core REST APIs that you can use to access the data and control your device programmatically.
ms.custom: 19H1
ms.date: 04/19/2019
ms.topic: article
keywords: windows 10, uwp, device portal
ms.localizationpriority: medium
---

# Device Portal core API reference

All Device Portal functionality is built on REST APIs that developers can call directly to access resources and control their devices programmatically.

## App deployment

### Install an app

**Request**

You can install an app by using the following request format.

| Method      | Request URI |
| :------     | :----- |
| POST | /api/app/packagemanager/package |

**URI parameters**

You can specify the following additional parameters on the request URI:

| URI parameter | Description |
| :------          | :------ |
| package   | (**required**) The file name of the package to be installed. |

**Request headers**

- None

**Request body**

- The .appx or .appxbundle file, as well as any dependencies the app requires. 
- The certificate used to sign the app, if the device is IoT or Windows Desktop. Other platforms do not require the certificate. 

**Response**

**Status code**

This API has the following expected status codes.

|  HTTP status code      | Description | 
| :------     | :----- |
| 200 | Deploy request accepted and being processed |
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Windows Mobile
* Windows Desktop
* Xbox
* HoloLens
* IoT

<hr>

### Install a related set

**Request**

You can install a [related set](/windows/msix/) by using the following request format.

| Method      | Request URI |
| :------     | :------ |
| POST | /api/app/packagemanager/package |

**URI parameters**

You can specify the following additional parameters on the request URI:

| URI parameter | Description |
| :------          | :------ |
| package   | (**required**) The file names of the packages to be installed. |

**Request headers**

- None

**Request body** 
- Add ".opt" to the optional package file names when specifying them as a parameter, like so: "foo.appx.opt" or "bar.appxbundle.opt". 
- The .appx or .appxbundle file, as well as any dependencies the app requires. 
- The certificate used to sign the app, if the device is IoT or Windows Desktop. Other platforms do not require the certificate. 

**Response**

**Status code**

This API has the following expected status codes.

|  HTTP status code      | Description | 
| :------     | :----- |
| 200 | Deploy request accepted and being processed |
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Windows Mobile
* Windows Desktop
* Xbox
* HoloLens
* IoT

<hr>

### Register an app in a loose folder

**Request**

You can register an app in a loose folder by using the following request format.

| Method      | Request URI |
| :------     | :----- |
| POST | /api/app/packagemanager/networkapp |

**URI parameters**

- None

**Request headers**

- None

**Request body**

```json
{
    "mainpackage" :
    {
        "networkshare" : "\\some\share\path",
        "username" : "optional_username",
        "password" : "optional_password"
    }
}
```

**Response**

**Status code**

This API has the following expected status codes.

|  HTTP status code      | Description | 
| :------     | :----- |
| 200 | Deploy request accepted and being processed |
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Windows Desktop
* Xbox
* HoloLens
* IoT

<hr>

### Register a related set in loose file folders

**Request**

You can register a [related set](/windows/msix/) in loose folders by using the following request format.

| Method      | Request URI |
| :------     | :----- |
| POST | /api/app/packagemanager/networkapp |

**URI parameters**

- None

**Request headers**

- None

**Request body**

```json
{
    "mainpackage" :
    {
        "networkshare" : "\\some\share\path",
        "username" : "optional_username",
        "password" : "optional_password"
    },
    "optionalpackages" :
    [
        {
            "networkshare" : "\\some\share\path2",
            "username" : "optional_username2",
            "password" : "optional_password2"
        },
        ...
    ]
}
```

**Response**

**Status code**

This API has the following expected status codes.

|  HTTP status code      | Description | 
| :------     | :----- |
| 200 | Deploy request accepted and being processed |
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Windows Desktop
* Xbox
* HoloLens
* IoT

<hr>

### Get app installation status

**Request**

You can get the status of an app installation that is currently in progress by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| GET | /api/app/packagemanager/state |

**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

**Status code**

This API has the following expected status codes.

|  HTTP status code      | Description | 
| :------     | :----- |
| 200 | The result of the last deployment |
| 204 | The installation is running |
| 404 | No installation action was found |

**Available device families**

* Windows Mobile
* Windows Desktop
* Xbox
* HoloLens
* IoT

<hr>

### Uninstall an app

**Request**

You can uninstall an app by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| DELETE | /api/app/packagemanager/package |

**URI parameters**

| URI parameter | Description |
| :------          | :------ |
| package   | (**required**) The PackageFullName (from GET /api/app/packagemanager/packages) of the target app |

**Request headers**

- None

**Request body**

- None

**Response**

**Status code**

This API has the following expected status codes.

|  HTTP status code      | Description | 
| :------     | :----- |
|  200 | OK | 
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Windows Mobile
* Windows Desktop
* Xbox
* HoloLens
* IoT

<hr>

### Get installed apps

**Request**

You can get a list of apps installed on the system by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| GET | /api/app/packagemanager/packages |


**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

The response includes a list of installed packages with associated details. The template for this response is as follows.
```json
{"InstalledPackages": [
    {
        "Name": string,
        "PackageFamilyName": string,
        "PackageFullName": string,
        "PackageOrigin": int, (https://msdn.microsoft.com/library/windows/desktop/dn313167(v=vs.85).aspx)
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

|  HTTP status code      | Description | 
| :------     | :----- |
|  200 | OK | 
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Windows Mobile
* Windows Desktop
* Xbox
* HoloLens
* IoT

<hr>

## Bluetooth

<hr>

### Get the Bluetooth radios on the machine

**Request**

You can get a list of the Bluetooth radios that are installed on the machine by using the following request format. This can be upgraded to a WebSocket connection as well, with the same JSON data.
 
| Method        | Request URI |
| :------          | :------ |
| GET           | /api/bt/getradios |
| GET/WebSocket | /api/bt/getradios |


**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

The response includes a JSON array of Bluetooth radios attached to the device.
```json
{"BluetoothRadios" : [
    {
        "BluetoothAddress" : int64,
        "DisplayName" : string,
        "HasUnknownUsbDevice" : boolean,
        "HasProblem" : boolean,
        "ID" : string,
        "ProblemCode" : int,
        "State" : string
    },...
]}
```
**Status code**

This API has the following expected status codes.

| HTTP status code | Description |
| :------             | :------ |
| 200              | OK |
| 4XX              | Error codes |
| 5XX              | Error codes |

**Available device families**

* Windows Desktop
* HoloLens
* IoT

<hr>

### Turn the Bluetooth radio on or off

**Request**

Sets a specific Bluetooth radio to On or Off.
 
| Method | Request URI |
| :------   | :------ |
| POST   | /api/bt/setradio |

**URI parameters**

You can specify the following additional parameters on the request URI:

| URI parameter | Description |
| :------          | :------ |
| ID            | (**required**) The device ID for the Bluetooth radio and must be base 64 encoded. |
| State         | (**required**) This can be `"On"` or `"Off"`. |

**Request headers**

- None

**Request body**

- None

**Response**

**Status code**

This API has the following expected status codes.

| HTTP status code | Description |
| :------             | :------ |
| 200              | OK |
| 4XX              | Error codes |
| 5XX              | Error codes |

**Available device families**

* Windows Desktop
* HoloLens
* IoT

---
### Get a list of paired Bluetooth devices

**Request**

You can get a list of the currently paired Bluetooth devices by using the following request format. This can be upgraded to a WebSocket connection with the same JSON data. During the lifetime of the WebSocket connection, the device list can change. A complete list of devices will be sent over the WebSocket connection each time there is an update.

| Method        | Request URI       |
| :---          | :---              |
| GET           | /api/bt/getpaired |
| GET/WebSocket | /api/bt/getpaired |

**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

The response includes a JSON array of Bluetooth devices that are currently paired.
```json
{"PairedDevices": [
    {
        "Name" : string,
        "ID" : string,
        "AudioConnectionStatus" : string
    },...
]}
```
The *AudioConnectionStatus* field will be present if the device can be used for audio on this system. (Policies and optional components may affect this.) *AudioConnectionStatus* will be either "Connected" or "Disconnected".

---
### Get a list of available Bluetooth devices

**Request**

You can get a list of the Bluetooth devices available for pairing by using the following request format. This can be upgraded to a WebSocket connection with the same JSON data. During the lifetime of the WebSocket connection, the device list can change. A complete list of devices will be sent over the WebSocket connection each time there is an update.

| Method        | Request URI          |
| :---          | :---                 |
| GET           | /api/bt/getavailable |
| GET/WebSocket | /api/bt/getavailable |

**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

The response includes a JSON array of Bluetooth devices that are currently available for pairing.
```json
{"AvailableDevices": [
    {
        "Name" : string,
        "ID" : string
    },...
]}
```

---
### Connect a Bluetooth device

**Request**

Will connect to the device if the device can be used for audio on this system. (Policies and optional components may affect this.)

| Method       | Request URI           |
| :---         | :---                  |
| POST         | /api/bt/connectdevice |

**URI parameters**

| URI parameter | Description |
| :---          | :--- |
| ID            | (**required**) The Association Endpoint ID for the Bluetooth device and must be Base64-encoded. |

**Request headers**

- None

**Request body**

- None

**Response**

**Status code**

This API has the following expected status codes.

| HTTP status code | Description |
| :---             | :--- |
| 200              | OK |
| 4XX              | Error codes |
| 5XX              | Error codes |

**Available device families**

* Windows Desktop
* HoloLens
* IoT


---
### Disconnect a Bluetooth device

**Request**

Will disconnect the device if the device can be used for audio on this system. (Policies and optional components may affect this.)

| Method       | Request URI              |
| :---         | :---                     |
| POST         | /api/bt/disconnectdevice |

**URI parameters**

| URI parameter | Description |
| :---          | :--- |
| ID            | (**required**) The Association Endpoint ID for the Bluetooth device and must be Base64-encoded. |

**Request headers**

- None

**Request body**

- None

**Response**

**Status code**

This API has the following expected status codes.

| HTTP status code | Description |
| :---             | :--- |
| 200              | OK |
| 4XX              | Error codes |
| 5XX              | Error codes |

**Available device families**

* Windows Desktop
* HoloLens
* IoT

---
## Device manager
<hr>

### Get the installed devices on the machine

**Request**

You can get a list of devices that are installed on the machine by using the following request format.

| Method      | Request URI |
| :------     | :----- |
| GET | /api/devicemanager/devices |

**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

The response includes a JSON array of devices attached to the device.
```json
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

|  HTTP status code      | Description | 
| :------     | :----- |
|  200 | OK | 
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Windows Mobile
* Windows Desktop
* IoT

<hr>

### Get data on connected USB Devices/Hubs

**Request**

You can get a list of USB descriptors for connected USB devices and Hubs by using the following request format.

| Method      | Request URI |
| :------     | :----- |
| GET | /ext/devices/usbdevices |


**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

The response is JSON that includes DeviceID for the USB Device along with the USB Descriptors and port information for hubs.
```json
{
    "DeviceList": [
        {
        "ID": string,
        "ParentID": string, // Will equal an "ID" within the list, or be blank
        "Description": string, // optional
        "Manufacturer": string, // optional
        "ProblemCode": int, // optional
        "StatusCode": int // optional
        },
        ...
    ]
}
```

**Sample return data**
```json
{
    "DeviceList": [{
        "ID": "System",
        "ParentID": ""
    }, {
        "Class": "USB",
        "Description": "Texas Instruments USB 3.0 xHCI Host Controller",
        "ID": "PCI\\VEN_104C&DEV_8241&SUBSYS_1589103C&REV_02\\4&37085792&0&00E7",
        "Manufacturer": "Texas Instruments",
        "ParentID": "System",
        "ProblemCode": 0,
        "StatusCode": 25174026
    }, {
        "Class": "USB",
        "Description": "USB Composite Device",
        "DeviceDriverKey": "{36fc9e60-c465-11cf-8056-444553540000}\\0016",
        "ID": "USB\\VID_045E&PID_00DB\\8&2994096B&0&1",
        "Manufacturer": "(Standard USB Host Controller)",
        "ParentID": "USB\\VID_0557&PID_8021\\7&2E9A8711&0&4",
        "ProblemCode": 0,
        "StatusCode": 25182218
    }]
}
```

**Status code**

This API has the following expected status codes.

|  HTTP status code      | Description | 
| :------     | :----- |
|  200 | OK | 
| 5XX | Error codes |

**Available device families**

* Windows Desktop
* IoT

<hr>

## Dump collection

<hr>

### Get the list of all crash dumps for apps

**Request**

You can get the list of all the available crash dumps for all sideloaded apps by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| GET | /api/debug/dump/usermode/dumps |


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

|  HTTP status code      | Description | 
| :------     | :----- |
|  200 | OK | 
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Window Mobile (in Windows Insider Program)
* Windows Desktop
* HoloLens
* IoT

<hr>

### Get the crash dump collection settings for an app

**Request**

You can get the crash dump collection settings for a sideloaded app by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| GET | /api/debug/dump/usermode/crashcontrol |


**URI parameters**

You can specify the following additional parameters on the request URI:

| URI parameter | Description |
| :------          | :------ |
| packageFullname   | (**required**) The full name of the package for the sideloaded app. |

**Request headers**

- None

**Request body**

- None

**Response**

The response has the following format.
```json
{"CrashDumpEnabled": bool}
```

**Status code**

This API has the following expected status codes.

|  HTTP status code      | Description | 
| :------     | :----- |
|  200 | OK | 
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Window Mobile (in Windows Insider Program)
* Windows Desktop
* HoloLens
* IoT

<hr>

### Delete a crash dump for a sideloaded app

**Request**

You can delete a sideloaded app's crash dump by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| DELETE | /api/debug/dump/usermode/crashdump |


**URI parameters**

You can specify the following additional parameters on the request URI:

| URI parameter | Description |
| :---          | :--- |
| packageFullname   | (**required**) The full name of the package for the sideloaded app. |
| fileName   | (**required**) The name of the dump file that should be deleted. |

**Request headers**

- None

**Request body**

- None

**Response**

**Status code**

This API has the following expected status codes.

|  HTTP status code      | Description | 
| :------     | :----- |
|  200 | OK | 
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Window Mobile (in Windows Insider Program)
* Windows Desktop
* HoloLens
* IoT

<hr>

### Disable crash dumps for a sideloaded app

**Request**

You can disable crash dumps for a sideloaded app by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| DELETE | /api/debug/dump/usermode/crashcontrol |


**URI parameters**

You can specify the following additional parameters on the request URI:

| URI parameter | Description |
| :---          | :--- |
| packageFullname   | (**required**) The full name of the package for the sideloaded app. |

**Request headers**

- None

**Request body**

- None

**Response**

**Status code**

This API has the following expected status codes.

|  HTTP status code      | Description | 
| :------     | :----- |
|  200 | OK | 
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Window Mobile (in Windows Insider Program)
* Windows Desktop
* HoloLens
* IoT

<hr>

### Download the crash dump for a sideloaded app

**Request**

You can download a sideloaded app's crash dump by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| GET | /api/debug/dump/usermode/crashdump |


**URI parameters**

You can specify the following additional parameters on the request URI:

| URI parameter | Description |
| :------          | :------ |
| packageFullname   | (**required**) The full name of the package for the sideloaded app. |
| fileName   | (**required**) The name of the dump file that you want to download. |

**Request headers**

- None

**Request body**

- None

**Response**

The response includes a dump file. You can use WinDbg or Visual Studio to examine the dump file.

**Status code**

This API has the following expected status codes.

| HTTP status code      | Description |
| :------     | :----- |
|  200 | OK | 
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Window Mobile (in Windows Insider Program)
* Windows Desktop
* HoloLens
* IoT

<hr>

### Enable crash dumps for a sideloaded app

**Request**

You can enable crash dumps for a sideloaded app by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| POST | /api/debug/dump/usermode/crashcontrol |


**URI parameters**

You can specify the following additional parameters on the request URI:

| URI parameter | Description |
| :---          | :--- |
| packageFullname   | (**required**) The full name of the package for the sideloaded app. |

**Request headers**

- None

**Request body**

- None

**Response**

**Status code**

This API has the following expected status codes.

|  HTTP status code      | Description | 
| :------     | :----- |
|  200 | OK | 

**Available device families**

* Window Mobile (in Windows Insider Program)
* Windows Desktop
* HoloLens
* IoT

<hr>

### Get the list of bugcheck files

**Request**

You can get the list of bugcheck minidump files by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| GET | /api/debug/dump/kernel/dumplist |


**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

The response includes a list of dump file names and the sizes of these files. This list will be in the following format. 
```json
{"DumpFiles": [
    {
        "FileName": string,
        "FileSize": int
    },...
]}
```

**Status code**

This API has the following expected status codes.

|  HTTP status code      | Description | 
| :------     | :----- |
|  200 | OK | 

**Available device families**

* Windows Desktop
* IoT

<hr>

### Download a bugcheck dump file

**Request**

You can download a bugcheck dump file by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| GET | /api/debug/dump/kernel/dump |


**URI parameters**

You can specify the following additional parameters on the request URI:

| URI parameter | Description |
| :------          | :------ |
| filename   | (**required**) The file name of the dump file. You can find this by using the API to get the dump list. |


**Request headers**

- None

**Request body**

- None

**Response**

The response includes the dump file. You can inspect this file using WinDbg.

**Status code**

This API has the following expected status codes.

|  HTTP status code      | Description | 
| :------     | :----- |
|  200 | OK | 
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Windows Desktop
* IoT

<hr>

### Get the bugcheck crash control settings

**Request**

You can get the bugcheck crash control settings by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| GET | /api/debug/dump/kernel/crashcontrol |


**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

The response includes the crash control settings. For more information about CrashControl, see the [CrashControl](https://technet.microsoft.com/library/cc951703.aspx) article. The template for the response is as follows.
```json
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

|  HTTP status code      | Description | 
| :------     | :----- |
|  200 | OK | 
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Windows Desktop
* IoT

<hr>

### Get a live kernel dump

**Request**

You can get a live kernel dump by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| GET | /api/debug/dump/livekernel |


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

|  HTTP status code      | Description | 
| :------     | :----- |
|  200 | OK | 
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Windows Desktop
* IoT

<hr>

### Get a dump from a live user process

**Request**

You can get the dump for live user process by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| GET | /api/debug/dump/usermode/live |


**URI parameters**

You can specify the following additional parameters on the request URI:

| URI parameter | Description |
| :------          | :------ |
| pid   | (**required**) The unique process id for the process you are interested in. |

**Request headers**

- None

**Request body**

- None

**Response**

The response includes the process dump. You can inspect this file using WinDbg or Visual Studio.

**Status code**

This API has the following expected status codes.

|  HTTP status code      | Description | 
| :------     | :----- |
|  200 | OK | 
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Windows Desktop
* IoT

<hr>

### Set the bugcheck crash control settings

**Request**

You can set the settings for collecting bugcheck data by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| POST | /api/debug/dump/kernel/crashcontrol |


**URI parameters**

You can specify the following additional parameters on the request URI:

| URI parameter | Description |
| :---          | :--- |
| autoreboot   | (**optional**) True or false. This indicates whether the system restarts automatically after it fails or locks. |
| dumptype   | (**optional**) The dump type. For the supported values, see the [CrashDumpType Enumeration](/previous-versions/azure/reference/dn802457(v=azure.100)).|
| maxdumpcount   | (**optional**) The maximum number of dumps to save. |
| overwrite   | (**optional**) True of false. This indicates whether or not to overwrite old dumps when the dump counter limit specified by *maxdumpcount* has been reached. |

**Request headers**

- None

**Request body**

- None

**Response**

**Status code**

This API has the following expected status codes.

|  HTTP status code      | Description | 
| :------     | :----- |
|  200 | OK | 
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Windows Desktop
* IoT

<hr>

## ETW

<hr>

### Create a realtime ETW session over a websocket

**Request**

You can create a realtime ETW session by using the following request format. This will be managed over a websocket.  ETW events are batched on the server and sent to the client once per second. 
 
| Method      | Request URI |
| :------     | :----- |
| GET/WebSocket | /api/etw/session/realtime |


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

|  HTTP status code      | Description | 
| :------     | :----- |
|  200 | OK | 
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Windows Mobile
* Windows Desktop
* HoloLens
* IoT

### ETW WebSocket commands
These commands are sent from the client to the server.

| Command | Description |
| :----- | :----- |
| provider *{guid}* enable *{level}* | Enable the provider marked by *{guid}* (without brackets) at the specified level. *{level}* is an **int** from 1 (least detail) to 5 (verbose). |
| provider *{guid}* disable | Disable the provider marked by *{guid}* (without brackets). |

This responses is sent from the server to the client. This is sent as text and you get the following format by parsing the JSON.
```json
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
```json
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

<hr>

### Enumerate the registered ETW providers

**Request**

You can enumerate through the registered providers by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| GET | /api/etw/providers |


**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

The response includes the list of ETW providers. The list will include the friendly name and GUID for each provider in the following format.
```json
{"Providers": [
    {
        "GUID": string, (GUID)
        "Name": string
    },...
]}
```

**Status code**

This API has the following expected status codes.

|  HTTP status code      | Description | 
| :------     | :----- |
|  200 | OK | 

**Available device families**

* Windows Mobile
* Windows Desktop
* HoloLens
* IoT

<hr>

### Enumerate the custom ETW providers exposed by the platform.

**Request**

You can enumerate through the registered providers by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| GET | /api/etw/customproviders |


**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

200 OK. The response includes the list of ETW providers. The list will include the friendly name and GUID for each provider.

```json
{"Providers": [
    {
        "GUID": string, (GUID)
        "Name": string
    },...
]}
```

**Status code**

- Standard status codes.

**Available device families**

* Windows Mobile
* Windows Desktop
* HoloLens
* IoT

<hr>

## Location

<hr>

### Get location override mode

**Request**

You can get the device's location stack override status by using the following request format. Developer mode must be on for this call to succeed.
 
| Method      | Request URI |
| :------     | :----- |
| GET | /ext/location/override |


**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

The response includes the override state of the device in the following format. 

```json
{"Override" : bool}
```

**Status code**

This API has the following expected status codes.

| HTTP status code      | Description |
| :------     | :----- |
|  200 | OK | 
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Windows Mobile
* Windows Desktop
* Xbox
* HoloLens
* IoT

### Set location override mode

**Request**

You can set the device's location stack override status by using the following request format. When enabled, the location stack allows position injection. Developer mode must be on for this call to succeed.

| Method      | Request URI |
| :------     | :----- |
| PUT | /ext/location/override |


**URI parameters**

- None

**Request headers**

- None

**Request body**

```json
{"Override" : bool}
```

**Response**

The response includes the override state that the device has been set to in the following format. 

```json
{"Override" : bool}
```

**Status code**

This API has the following expected status codes.

| HTTP status code      | Description |
| :------     | :----- |
| 200 | OK |
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Windows Mobile
* Windows Desktop
* Xbox
* HoloLens
* IoT

### Get the injected position

**Request**

You can get the device's injected (spoofed) location by using the following request format. An injected location must be set, or an error will be thrown.
 
| Method      | Request URI |
| :------     | :----- |
| GET | /ext/location/position |


**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

The response includes the current injected latitude and longitude values in the following format. 

```json
{
    "Latitude" : double,
    "Longitude" : double
}
```

**Status code**

This API has the following expected status codes.

|  HTTP status code      | Description | 
| :------     | :----- |
| 200 | OK |
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Windows Mobile
* Windows Desktop
* Xbox
* HoloLens
* IoT

### Set the injected position

**Request**

You can set the device's injected (spoofed) location by using the following request format. Location override mode must first be enabled on the device, and the set location must be a valid location or an error will be thrown.

| Method      | Request URI |
| :------     | :----- |
| PUT | /ext/location/override |


**URI parameters**

- None

**Request headers**

- None

**Request body**

```json
{
    "Latitude" : double,
    "Longitude" : double
}
```

**Response**

The response includes the location that has been set in the following format. 

```json
{
    "Latitude" : double,
    "Longitude" : double
}
```

**Status code**

This API has the following expected status codes.

| HTTP status code      | Description |
| :------     | :----- |
| 200 | OK |
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Windows Mobile
* Windows Desktop
* Xbox
* HoloLens
* IoT

<hr>

## OS information

<hr>

### Get the machine name

**Request**

You can get the name of a machine by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| GET | /api/os/machinename |


**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

The response includes the computer name in the following format. 

```json
{"ComputerName": string}
```

**Status code**

This API has the following expected status codes.

| HTTP status code      | Description |
| :------     | :----- |
| 200 | OK |
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Windows Mobile
* Windows Desktop
* Xbox
* HoloLens
* IoT

<hr>

### Get the operating system information

**Request**

You can get the OS information for a machine by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| GET | /api/os/info |


**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

The response includes the OS information in the following format.

```json
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

| HTTP status code      | Description |
| :------     | :----- |
| 200 | OK |
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Windows Mobile
* Windows Desktop
* Xbox
* HoloLens
* IoT

<hr>

### Get the device family 

**Request**

You can get the device family (Xbox, phone, desktop, etc) using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| GET | /api/os/devicefamily |


**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

The response includes the device family (SKU - Desktop, Xbox, etc).

```json
{
   "DeviceType" : string
}
```

DeviceType will look like "Windows.Xbox", "Windows.Desktop", etc. 

**Status code**

This API has the following expected status codes.

| HTTP status code      | Description |
| :------     | :----- |
| 200 | OK |
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Windows Mobile
* Windows Desktop
* Xbox
* HoloLens
* IoT

<hr>

### Set the machine name

**Request**

You can set the name of a machine by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| POST | /api/os/machinename |


**URI parameters**

You can specify the following additional parameters on the request URI:

| URI parameter | Description |
| :------          | :------ |
| name | (**required**) The new name for the machine. This should be base64 encoded. |

**Request headers**

- None

**Request body**

- None

**Response**

**Status code**

This API has the following expected status codes.

| HTTP status code      | Description |
| :------     | :----- |
| 200 | OK |

**Available device families**

* Windows Mobile
* Windows Desktop
* Xbox
* HoloLens
* IoT

<hr>

## User information

<hr>

### Get the active user

**Request**

You can get the name of the active user on the device by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| GET | /api/users/activeuser |


**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

The response includes user information in the following format. 

On success: 
```json
{
    "UserDisplayName" : string, 
    "UserSID" : string
}
```
On failure:
```json
{
    "Code" : int, 
    "CodeText" : string, 
    "Reason" : string, 
    "Success" : bool
}
```

**Status code**

This API has the following expected status codes.

| HTTP status code      | Description |
| :------     | :----- |
| 200 | OK |
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Windows Desktop
* HoloLens
* IoT

<hr>

## Performance data

<hr>

### Get the list of running processes

**Request**

You can get the list of currently running processes by using the following request format.  this can be upgraded to a WebSocket connection as well, with the same JSON data being pushed to the client once per second. 
 
| Method      | Request URI |
| :------     | :----- |
| GET | /api/resourcemanager/processes |
| GET/WebSocket | /api/resourcemanager/processes |


**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

The response includes a list of processes with details for each process. The information is in JSON format and has the following template.
```json
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

| HTTP status code      | Description |
| :------     | :----- |
| 200 | OK |
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Windows Mobile
* Windows Desktop
* HoloLens
* IoT

<hr>

### Get the system performance statistics

**Request**

You can get the system performance statistics by using the following request format. This includes information such as read and write cycles and how much memory has been used.
 
| Method      | Request URI |
| :------     | :----- |
| GET | /api/resourcemanager/systemperf |
| GET/WebSocket | /api/resourcemanager/systemperf |

This can also be upgraded to a WebSocket connection.  It provides the same JSON data below once every second. 

**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

The response includes the performance statistics for the system such as CPU and GPU usage, memory access, and network access. This information is in JSON format and has the following template.
```json
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

| HTTP status code      | Description |
| :------     | :----- |
| 200 | OK |
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Windows Mobile
* Windows Desktop
* Xbox
* HoloLens
* IoT

<hr>

## Power

<hr>

### Get the current battery state

**Request**

You can get the current state of the battery by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| GET | /api/power/battery |


**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

The current battery state information is returned using the following format.
```json
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

| HTTP status code      | Description |
| :------     | :----- |
| 200 | OK |
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Windows Mobile
* Windows Desktop
* HoloLens
* IoT

<hr>

### Get the active power scheme

**Request**

You can get the active power scheme by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| GET | /api/power/activecfg |


**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

The active power scheme has the following format.
```json
{"ActivePowerScheme": string (guid of scheme)}
```

**Status code**

This API has the following expected status codes.

| HTTP status code      | Description |
| :------     | :----- |
| 200 | OK |
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Windows Desktop
* IoT

<hr>

### Get the sub-value for a power scheme

**Request**

You can get the sub-value for a power scheme by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| GET | /api/power/cfg/*<power scheme path>* |

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

| HTTP status code      | Description |
| :------     | :----- |
| 200 | OK |
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Windows Desktop
* IoT

<hr>

### Get the power state of the system

**Request**

You can check the power state of the system by using the following request format. This will let you check to see if it is in a low power state.
 
| Method      | Request URI |
| :------     | :----- |
| GET | /api/power/state |


**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

The power state information has the following template.
```json
{"LowPowerState" : false, "LowPowerStateAvailable" : true }
```

**Status code**

This API has the following expected status codes.

| HTTP status code      | Description |
| :------     | :----- |
| 200 | OK |
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Windows Desktop
* HoloLens
* IoT

<hr>

### Set the active power scheme

**Request**

You can set the active power scheme by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| POST | /api/power/activecfg |


**URI parameters**

You can specify the following additional parameters on the request URI:

| URI parameter | Description |
| :---          | :--- |
| scheme | (**required**) The GUID of the scheme you want to set as the active power scheme for the system. |

**Request headers**

- None

**Request body**

- None

**Response**

**Status code**

This API has the following expected status codes.

| HTTP status code      | Description |
| :------     | :----- |
| 200 | OK |
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Windows Desktop
* IoT

<hr>

### Set the sub-value for a power scheme

**Request**

You can set the sub-value for a power scheme by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| POST | /api/power/cfg/*<power scheme path>* |


**URI parameters**

You can specify the following additional parameters on the request URI:

| URI parameter | Description |
| :------          | :------ |
| valueAC | (**required**) The value to use for A/C power. |
| valueDC | (**required**) The value to use for battery power. |

**Request headers**

- None

**Request body**

- None

**Response**

**Status code**

This API has the following expected status codes.

| HTTP status code      | Description |
| :------     | :----- |
| 200 | OK |

**Available device families**

* Windows Desktop
* IoT

<hr>

### Get a sleep study report

**Request**

| Method      | Request URI |
| :------     | :----- |
| GET | /api/power/sleepstudy/report |

You can get a sleep study report by using the following request format.

**URI parameters**
| URI parameter | Description |
| :------          | :------ |
| FileName | (**required**) The full name for the file you want to download. This value should be hex64 encoded. |

**Request headers**

- None

**Request body**

- None

**Response**

The response is a file containing the sleep study. 

**Status code**

This API has the following expected status codes.

| HTTP status code      | Description |
| :------     | :----- |
| 200 | OK |
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Windows Desktop
* IoT

<hr>

### Enumerate the available sleep study reports

**Request**

You can enumerate the available sleep study reports by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| GET | /api/power/sleepstudy/reports |


**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

The list of available reports has the following template.

```json
{"Reports": [
    {
        "FileName": string
    },...
]}
```

**Status code**

This API has the following expected status codes.

| HTTP status code      | Description |
| :------     | :----- |
| 200 | OK |
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Windows Desktop
* IoT

<hr>

### Get the sleep study transform

**Request**

You can get the sleep study transform by using the following request format. This transform is an XSLT that converts the sleep study report into an XML format that can be read by a person.
 
| Method      | Request URI |
| :------     | :----- |
| GET | /api/power/sleepstudy/transform |


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

| HTTP status code      | Description |
| :------     | :----- |
| 200 | OK |
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Windows Desktop
* IoT

<hr>

## Remote control

<hr>

### Restart the target computer

**Request**

You can restart the target computer by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| POST | /api/control/restart |


**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

**Status code**

This API has the following expected status codes.

| HTTP status code      | Description |
| :------     | :----- |
| 200 | OK |

**Available device families**

* Windows Mobile
* Windows Desktop
* Xbox
* HoloLens
* IoT

<hr>

### Shut down the target computer

**Request**

You can shut down the target computer by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| POST | /api/control/shutdown |


**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

**Status code**

This API has the following expected status codes.

| HTTP status code      | Description |
| :------     | :----- |
| 200 | OK |
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Windows Mobile
* Windows Desktop
* Xbox
* HoloLens
* IoT

<hr>

## Task manager

<hr>

### Start a modern app

**Request**

You can start a modern app by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| POST | /api/taskmanager/app |


**URI parameters**

You can specify the following additional parameters on the request URI:

| URI parameter | Description |
| :---          | :--- |
| appid   | (**required**) The PRAID for the app you want to start. This value should be hex64 encoded. |
| package   | (**required**) The full name for the app package you want to start. This value should be hex64 encoded. |

**Request headers**

- None

**Request body**

- None

**Response**

**Status code**

This API has the following expected status codes.

| HTTP status code      | Description |
| :------     | :----- |
| 200 | OK |
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Windows Mobile
* Windows Desktop
* Xbox
* HoloLens
* IoT

<hr>

### Stop a modern app

**Request**

You can stop a modern app by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| DELETE | /api/taskmanager/app |


**URI parameters**

You can specify the following additional parameters on the request URI:

| URI parameter | Description |
| :---          | :--- |
| package   | (**required**) The full name of the app packages that you want to stop. This value should be hex64 encoded. |
| forcestop   | (**optional**) A value of **yes** indicates that the system should force all processes to stop. |

**Request headers**

- None

**Request body**

- None

**Response**

**Status code**

This API has the following expected status codes.

| HTTP status code      | Description |
| :------     | :----- |
| 200 | OK |
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Windows Mobile
* Windows Desktop
* Xbox
* HoloLens
* IoT

<hr>

### Kill process by PID

**Request**

You can kill a process by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| DELETE | /api/taskmanager/process |


**URI parameters**

You can specify the following additional parameters on the request URI:

| URI parameter | Description |
| :------          | :------ |
| pid   | (**required**) The unique process id for the process to stop. |

**Request headers**

- None

**Request body**

- None

**Response**

**Status code**

This API has the following expected status codes.

| HTTP status code      | Description |
| :------     | :----- |
| 200 | OK |
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Windows Desktop
* HoloLens
* IoT

<hr>

## Networking

<hr>

### Get the current IP configuration

**Request**

You can get the current IP configuration by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| GET | /api/networking/ipconfig |


**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

The response includes the IP configuration in the following template.

```json
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

| HTTP status code      | Description |
| :------     | :----- |
| 200 | OK |
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Windows Mobile
* Windows Desktop
* Xbox
* HoloLens
* IoT

<hr>

### Set a static IP address (IPV4 configuration)

**Request**

Sets the IPV4 configuration with static IP and DNS. If a static IP is not specified, then it enables DHCP. If a static IP is specified, then DNS must be specified also.
 
| Method      | Request URI |
| :------     | :----- |
| PUT | /api/networking/ipv4config |


**URI parameters**

| URI parameter | Description |
| :---          | :--- |
| AdapterName | (**required**) The network interface GUID. |
| IPAddress | The static IP address to set. |
| SubnetMask | (**required** if *IPAddress* is not null) The static subnet mask. |
| DefaultGateway | (**required** if *IPAddress* is not null) The static default gateway. |
| PrimaryDNS | (**required** if *IPAddress* is not null) The static primary DNS to set. |
| SecondayDNS | (**required** if *PrimaryDNS* is not null) The static secondary DNS to set. |

For clarity, to set an interface to DHCP, serialize just the `AdapterName` on the wire:

```json
{
    "AdapterName":"{82F86C1B-2BAE-41E3-B08D-786CA44FEED7}"
}
```

**Request headers**

- None

**Request body**

- None

**Response**

**Status code**

This API has the following expected status codes.

| HTTP status code      | Description |
| :------     | :----- |
| 200 | OK |
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Windows Mobile
* Windows Desktop
* Xbox
* HoloLens
* IoT

<hr>

### Enumerate wireless network interfaces

**Request**

You can enumerate the available wireless network interfaces by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| GET | /api/wifi/interfaces |


**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

A list of the available wireless interfaces with details in the following format.

```json 
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

| HTTP status code      | Description |
| :------     | :----- |
| 200 | OK |
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Windows Mobile
* Windows Desktop
* Xbox
* HoloLens
* IoT

<hr>

### Enumerate wireless networks

**Request**

You can enumerate the list of wireless networks on the specified interface by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| GET | /api/wifi/networks |


**URI parameters**

You can specify the following additional parameters on the request URI:

| URI parameter | Description |
| :------          | :------ |
| interface   | (**required**) The GUID for the network interface to use to search for wireless networks, without brackets. |

**Request headers**

- None

**Request body**

- None

**Response**

The list of wireless networks found on the provided *interface*. This includes details for the networks in the following format.

```json
{"AvailableNetworks": [
    {
        "AlreadyConnected": bool,
        "AuthenticationAlgorithm": string, (WPA2, etc)
        "Channel": int,
        "CipherAlgorithm": string, (for example, AES)
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

| HTTP status code      | Description |
| :------     | :----- |
| 200 | OK |
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Windows Mobile
* Windows Desktop
* Xbox
* HoloLens
* IoT

<hr>

### Connect and disconnect to a Wi-Fi network.

**Request**

You can connect or disconnect to a Wi-Fi network by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| POST | /api/wifi/network |


**URI parameters**

You can specify the following additional parameters on the request URI:

| URI parameter | Description |
| :------          | :------ |
| interface   | (**required**) The GUID for the network interface you use to connect to the network. |
| op   | (**required**) Indicates the action to take. Possible values are connect or disconnect.|
| ssid   | (**required if *op* == connect**) The SSID to connect to. |
| key   | (**required if *op* == connect and network requires authentication**) The shared key. |
| createprofile | (**required**) Create a profile for the network on the device.  This will cause the device to auto-connect to the network in the future. This can be **yes** or **no**. |

**Request headers**

- None

**Request body**

- None

**Response**

**Status code**

This API has the following expected status codes.

| HTTP status code      | Description |
| :------     | :----- |
| 200 | OK |

**Available device families**

* Windows Mobile
* Windows Desktop
* Xbox
* HoloLens
* IoT

<hr>

### Delete a Wi-Fi profile

**Request**

You can delete a profile associated with a network on a specific interface by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| DELETE | /api/wifi/profile |


**URI parameters**

You can specify the following additional parameters on the request URI:

| URI parameter | Description |
| :------          | :------ |
| interface   | (**required**) The GUID for the network interface associated with the profile to delete. |
| profile   | (**required**) The name of the profile to delete. |

**Request headers**

- None

**Request body**

- None

**Response**

**Status code**

This API has the following expected status codes.

| HTTP status code      | Description |
| :------     | :----- |
| 200 | OK |

**Available device families**

* Windows Mobile
* Windows Desktop
* Xbox
* HoloLens
* IoT

<hr>

## Windows Error Reporting (WER)

<hr>

### Download a Windows error reporting (WER) file

**Request**

You can download a WER-related file by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| GET | /api/wer/report/file |


**URI parameters**

You can specify the following additional parameters on the request URI:

| URI parameter | Description |
| :------          | :------ |
| user   | (**required**) The user name associated with the report. |
| type   | (**required**) The type of report. This can be either **queried** or **archived**. |
| name   | (**required**) The name of the report. This should be base64 encoded. |
| file   | (**required**) The name of the file to download from the report. This should be base64 encoded. |

**Request headers**

- None

**Request body**

- None

**Response**

- Response contains the requested file. 

**Status code**

This API has the following expected status codes.

| HTTP status code      | Description |
| :------     | :----- |
| 200 | OK |
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Windows Desktop
* HoloLens
* IoT

<hr>

### Enumerate files in a Windows error reporting (WER) report

**Request**

You can enumerate the files in a WER report by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| GET | /api/wer/report/files |


**URI parameters**

You can specify the following additional parameters on the request URI:

| URI parameter | Description |
| :------          | :------ |
| user   | (**required**) The user associated with the report. |
| type   | (**required**) The type of report. This can be either **queried** or **archived**. |
| name   | (**required**) The name of the report. This should be base64 encoded. |

**Request headers**

- None

**Request body**

```json
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

| HTTP status code      | Description |
| :------     | :----- |
| 200 | OK |
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Windows Desktop
* HoloLens
* IoT

<hr>

### List the Windows error reporting (WER) reports

**Request**

You can get the WER reports by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| GET | /api/wer/reports |


**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

The WER reports in the following format.

```json
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

| HTTP status code      | Description |
| :------     | :----- |
| 200 | OK |
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Windows Desktop
* HoloLens
* IoT

<hr>

## Windows Performance Recorder (WPR) 

<hr>

### Start tracing with a custom profile

**Request**

You can upload a WPR profile and start tracing using that profile by using the following request format.  Only one trace can run at a time. The profile will not remain on the device. 
 
| Method      | Request URI |
| :------     | :----- |
| POST | /api/wpr/customtrace |


**URI parameters**

- None

**Request headers**

- None

**Request body**

- A multi-part conforming http body that contains the custom WPR profile.

**Response**

The WPR session status in the following format.

```json
{
    "SessionType": string, (Running or Idle) 
    "State": string (normal or boot)
}
```

**Status code**

This API has the following expected status codes.

| HTTP status code      | Description |
| :------     | :----- |
| 200 | OK |
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Windows Mobile
* Windows Desktop
* HoloLens
* IoT

<hr>

### Start a boot performance tracing session

**Request**

You can start a boot WPR tracing session by using the following request format. This is also known as a performance tracing session.
 
| Method      | Request URI |
| :------     | :----- |
| POST | /api/wpr/boottrace |


**URI parameters**

You can specify the following additional parameters on the request URI:

| URI parameter | Description |
| :------          | :------ |
| profile   | (**required**) This parameter is required on start. The name of the profile that should start a performance tracing session. The possible profiles are stored in perfprofiles/profiles.json. |

**Request headers**

- None

**Request body**

- None

**Response**

On start, this API returns the WPR session status in the following format.

```json
{
    "SessionType": string, (Running or Idle) 
    "State": string (boot)
}
```

**Status code**

This API has the following expected status codes.

| HTTP status code      | Description |
| :------     | :----- |
| 200 | OK |
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Windows Mobile
* Windows Desktop
* HoloLens
* IoT

<hr>

### Stop a boot performance tracing session

**Request**

You can stop a boot WPR tracing session by using the following request format. This is also known as a performance tracing session.
 
| Method      | Request URI |
| :------     | :----- |
| GET | /api/wpr/boottrace |


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

| HTTP status code      | Description |
| :------     | :----- |
| 200 | OK |
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Windows Mobile
* Windows Desktop
* HoloLens
* IoT

<hr>

### Start a performance tracing session

**Request**

You can start a WPR tracing session by using the following request format. This is also known as a performance tracing session.  Only one trace can run at a time. 
 
| Method      | Request URI |
| :------     | :----- |
| POST | /api/wpr/trace |


**URI parameters**

You can specify the following additional parameters on the request URI:

| URI parameter | Description |
| :------          | :------ |
| profile   | (**required**) The name of the profile that should start a performance tracing session. The possible profiles are stored in perfprofiles/profiles.json. |

**Request headers**

- None

**Request body**

- None

**Response**

On start, this API returns the WPR session status in the following format.

```json
{
    "SessionType": string, (Running or Idle) 
    "State": string (normal)
}
```

**Status code**

This API has the following expected status codes.

| HTTP status code      | Description |
| :------     | :----- |
| 200 | OK |
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Windows Mobile
* Windows Desktop
* HoloLens
* IoT

<hr>

### Stop a performance tracing session

**Request**

You can stop a WPR tracing session by using the following request format. This is also known as a performance tracing session.
 
| Method      | Request URI |
| :------     | :----- |
| GET | /api/wpr/trace |


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

| HTTP status code      | Description |
| :------     | :----- |
| 200 | OK |
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Windows Mobile
* Windows Desktop
* HoloLens
* IoT

<hr>

### Retrieve the status of a tracing session

**Request**

You can retrieve the status of the current WPR session by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| GET | /api/wpr/status |


**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

The status of the WPR tracing session in the following format.

```json
{
    "SessionType": string, (Running or Idle) 
    "State": string (normal or boot)
}
```

**Status code**

This API has the following expected status codes.

| HTTP status code      | Description |
| :------     | :----- |
| 200 | OK |
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Windows Mobile
* Windows Desktop
* HoloLens
* IoT

<hr>

### List completed tracing sessions (ETLs)

**Request**

You can get a listing of ETL traces on the device using the following request format. 

| Method      | Request URI |
| :------     | :----- |
| GET | /api/wpr/tracefiles |


**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

The listing of completed tracing sessions is provided in the following format.

```json
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

| HTTP status code      | Description |
| :------     | :----- |
| 200 | OK |
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Windows Mobile
* Windows Desktop
* HoloLens
* IoT

<hr>

### Download a tracing session (ETL)

**Request**

You can download a tracefile (boot trace or user-mode trace) using the following request format. 

| Method      | Request URI |
| :------     | :----- |
| GET | /api/wpr/tracefile |


**URI parameters**

You can specify the following additional parameter on the request URI:

| URI parameter | Description |
| :------          | :------ |
| filename   | (**required**) The name of the ETL trace to download.  These can be found in /api/wpr/tracefiles |

**Request headers**

- None

**Request body**

- None

**Response**

- Returns the trace ETL file.

**Status code**

This API has the following expected status codes.

| HTTP status code      | Description |
| :------     | :----- |
| 200 | OK |
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Windows Mobile
* Windows Desktop
* HoloLens
* IoT

<hr>

### Delete a tracing session (ETL)

**Request**

You can delete a tracefile (boot trace or user-mode trace) using the following request format. 

| Method      | Request URI |
| :------     | :----- |
| DELETE | /api/wpr/tracefile |


**URI parameters**

You can specify the following additional parameter on the request URI:

| URI parameter | Description |
| :------          | :------ |
| filename   | (**required**) The name of the ETL trace to delete.  These can be found in /api/wpr/tracefiles |

**Request headers**

- None

**Request body**

- None

**Response**

- Returns the trace ETL file.

**Status code**

This API has the following expected status codes.

| HTTP status code      | Description |
| :------     | :----- |
| 200 | OK |
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Windows Mobile
* Windows Desktop
* HoloLens
* IoT

<hr>

## DNS-SD Tags 

<hr>

### View Tags

**Request**

View the currently applied tags for the device.  These are advertised via DNS-SD TXT records in the T key.  
 
| Method      | Request URI |
| :------     | :----- |
| GET | /api/dns-sd/tags |


**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**
The currently applied tags in the following format. 
```json
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

| HTTP status code      | Description |
| :------     | :----- |
| 200 | OK |
| 5XX | Server Error |


**Available device families**

* Windows Mobile
* Windows Desktop
* Xbox
* HoloLens
* IoT

<hr>

### Delete Tags

**Request**

Delete all tags currently advertised by DNS-SD.   
 
| Method      | Request URI |
| :------     | :----- |
| DELETE | /api/dns-sd/tags |


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

| HTTP status code      | Description |
| :------     | :----- |
| 200 | OK |
| 5XX | Server Error |


**Available device families**

* Windows Mobile
* Windows Desktop
* Xbox
* HoloLens
* IoT

<hr>

### Delete Tag

**Request**

Delete a tag currently advertised by DNS-SD.   
 
| Method      | Request URI |
| :------     | :----- |
| DELETE | /api/dns-sd/tag |


**URI parameters**

| URI parameter | Description |
| :------     | :----- |
| tagValue | (**required**) The tag to be removed. |

**Request headers**

- None

**Request body**

- None

**Response**
 - None

**Status code**

This API has the following expected status codes.

| HTTP status code      | Description |
| :------     | :----- |
| 200 | OK |


**Available device families**

* Windows Mobile
* Windows Desktop
* Xbox
* HoloLens
* IoT
 
<hr>

### Add a Tag

**Request**

Add a tag to the DNS-SD advertisement.   
 
| Method      | Request URI |
| :------     | :----- |
| POST | /api/dns-sd/tag |


**URI parameters**

| URI parameter | Description |
| :------     | :----- |
| tagValue | (**required**) The tag to be added. |

**Request headers**

- None

**Request body**

- None

**Response**
 - None

**Status code**

This API has the following expected status codes.

| HTTP status code      | Description |
| :------     | :----- |
| 200 | OK |
| 401 | Tag space Overflow.  Results when the proposed tag is too long for the resulting DNS-SD service record. |


**Available device families**

* Windows Mobile
* Windows Desktop
* Xbox
* HoloLens
* IoT

## App File Explorer

<hr>

### Get known folders

**Request**

Obtain a list of accessible top-level folders.

| Method      | Request URI |
| :------     | :----- |
| GET | /api/filesystem/apps/knownfolders |


**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**
The available folders in the following format. 
```json
 {"KnownFolders": [
    "folder0",
    "folder1",...
]}
```
**Status code**

This API has the following expected status codes.

| HTTP status code      | Description |
| :------     | :----- |
| 200 | Deploy request accepted and being processed |
| 4XX | Error codes |
| 5XX | Error codes |


**Available device families**

* Windows Mobile
* Windows Desktop
* HoloLens
* Xbox
* IoT

<hr>

### Get files

**Request**

Obtain a list of files in a folder.

| Method      | Request URI |
| :------     | :----- |
| GET | /api/filesystem/apps/files |


**URI parameters**

| URI parameter | Description |
| :------     | :----- |
| knownfolderid | (**required**) The top-level directory where you want the list of files. Use **LocalAppData** for access to sideloaded apps. |
| packagefullname | (**required if *knownfolderid* == LocalAppData**) The package full name of the app you are interested in. |
| path | (**optional**) The sub-directory within the folder or package specified above. |

**Request headers**

- None

**Request body**

- None

**Response**
The available folders in the following format. 
```json
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

| HTTP status code      | Description |
| :------     | :----- |
| 200 | OK |
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Windows Mobile
* Windows Desktop
* HoloLens
* Xbox
* IoT

<hr>

### Download a file

**Request**

Obtain a file from a known folder or appLocalData.

| Method      | Request URI |
| :------     | :----- |
| GET | /api/filesystem/apps/file |

**URI parameters**

| URI parameter | Description |
| :------     | :----- |
| knownfolderid | (**required**) The top-level directory where you want to download files. Use **LocalAppData** for access to sideloaded apps. |
| filename | (**required**) The name of the file being downloaded. |
| packagefullname | (**required if *knownfolderid* == LocalAppData**) The package full name you are interested in. |
| path | (**optional**) The sub-directory within the folder or package specified above. |

**Request headers**

- None

**Request body**

- The file requested, if present

**Response**

**Status code**

This API has the following expected status codes.

| HTTP status code      | Description |
| :------     | :----- |
| 200 | The requested file |
| 404 | File not found |
| 5XX | Error codes |

**Available device families**

* Windows Mobile
* Windows Desktop
* HoloLens
* Xbox
* IoT

<hr>

### Rename a file

**Request**

Rename a file in a folder.

| Method      | Request URI |
| :------     | :----- |
| POST | /api/filesystem/apps/rename |


**URI parameters**

| URI parameter | Description |
| :------     | :----- |
| knownfolderid | (**required**) The top-level directory where the file is located. Use **LocalAppData** for access to sideloaded apps. |
| filename | (**required**) The original name of the file being renamed. |
| newfilename | (**required**) The new name of the file.|
| packagefullname | (**required if *knownfolderid* == LocalAppData**) The package full name of the app you are interested in. |
| path | (**optional**) The sub-directory within the folder or package specified above. |

**Request headers**

- None

**Request body**

- None

**Response**

- None

**Status code**

This API has the following expected status codes.

| HTTP status code      | Description |
| :------     | :----- |
| 200 | OK |. The file is renamed
| 404 | File not found |
| 5XX | Error codes |

**Available device families**

* Windows Mobile
* Windows Desktop
* HoloLens
* Xbox
* IoT

<hr>

### Delete a file

**Request**

Delete a file in a folder.

| Method      | Request URI |
| :------     | :----- |
| DELETE | /api/filesystem/apps/file |

**URI parameters**

| URI parameter | Description |
| :------     | :----- |
| knownfolderid | (**required**) The top-level directory where you want to delete files. Use **LocalAppData** for access to sideloaded apps. |
| filename | (**required**) The name of the file being deleted. |
| packagefullname | (**required if *knownfolderid* == LocalAppData**) The package full name of the app you are interested in. |
| path | (**optional**) The sub-directory within the folder or package specified above. |

**Request headers**

- None

**Request body**

- None

**Response**

- None 

**Status code**

This API has the following expected status codes.

| HTTP status code      | Description |
| :------     | :----- |
| 200 | OK |. The file is deleted |
| 404 | File not found |
| 5XX | Error codes |

**Available device families**

* Windows Mobile
* Windows Desktop
* HoloLens
* Xbox
* IoT

<hr>

### Upload a file

**Request**

Upload a file to a folder.  This will overwrite an existing file with the same name, but will not create new folders. 

| Method      | Request URI |
| :------     | :----- |
| POST | /api/filesystem/apps/file |

**URI parameters**

| URI parameter | Description |
| :------     | :----- |
| knownfolderid | (**required**) The top-level directory where you want to upload files. Use **LocalAppData** for access to sideloaded apps. |
| packagefullname | (**required if *knownfolderid* == LocalAppData**) The package full name of the app you are interested in. |
| path | (**optional**) The sub-directory within the folder or package specified above. |

**Request headers**

- None

**Request body**

- None

**Response**

**Status code**

This API has the following expected status codes.

| HTTP status code      | Description |
| :------     | :----- |
| 200 | OK |. The file is uploaded |
| 4XX | Error codes |
| 5XX | Error codes |

**Available device families**

* Windows Mobile
* Windows Desktop
* HoloLens
* Xbox
* IoT