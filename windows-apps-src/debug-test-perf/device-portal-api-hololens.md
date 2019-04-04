---
ms.assetid: 41ac0142-4d86-4bb3-b580-36d0d6956091
title: Device Portal API reference for HoloLens
description: Learn about the Windows Device Portal for HoloLens REST API's that you can use to access the data and control your device programmatically.
ms.date: 03/22/2018
ms.topic: article
keywords: windows 10, uwp, device portal
ms.localizationpriority: medium
---
# Device Portal API reference for HoloLens

Everything in the Windows Device Portal is built on top of REST API's that you can use to access the data and control your device programmatically.

## Holographic OS

### Get HTTPS requirements for the Device Portal

**Request**

You can get the HTTPS requirements for the Device Portal by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| GET | /api/holographic/os/webmanagement/settings/https |


**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

- None

**Status code**

- Standard status codes.

### Get the stored interpupillary distance (IPD)

**Request**

You can get the stored IPD value by using the following request format. The value is returned in millimeters.
 
| Method      | Request URI |
| :------     | :----- |
| GET | /api/holographic/os/settings/ipd |


**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

- None

**Status code**

- Standard status codes.

### Get a list of HoloLens specific ETW providers

**Request**

You can get a list of HoloLens specific ETW providers that are not registered with the system by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| GET | /api/holographic/os/etw/customproviders |


**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

- None

**Status code**

- Standard status codes.


### Return the state for all active services

**Request**

You can get the state of all services that are currently running by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| GET | /api/holographic/os/services |


**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

- None

**Status code**

- Standard status codes.


### Set the HTTPS requirement for the Device Portal.

**Request**

You can set the HTTPS requirements for the Device Portal by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| POST | /api/holographic/management/settings/https |


**URI parameters**

You can specify the following additional parameters on the request URI:

| URI parameter | Description |
| :---          | :--- |
| required   | (**required**) Determines whether or not HTTPS is required for the Device Portal. Possible values are **yes**, **no**, and **default**. |

**Request headers**

- None

**Request body**

- None

**Response**

- None

**Status code**

- Standard status codes.


### Set the interpupillary distance (IPD)

**Request**

You can set the stored IPD by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| POST | /api/holographic/os/settings/ipd |


**URI parameters**

You can specify the following additional parameters on the request URI:

| URI parameter | Description |
| :---          | :--- |
| ipd   | (**required**) The new IPD value to be stored. This value should be in millimeters. |

**Request headers**

- None

**Request body**

- None

**Response**

- None

**Status code**

- Standard status codes.


## Holographic perception

### Accept websocket upgrades and run a mirage client that sends updates

**Request**

You can accept websocket upgrades and run a mirage client that sends updates at 30 fps by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| GET/WebSocket | /api/holographic/perception/client |


**URI parameters**

You can specify the following additional parameters on the request URI:

| URI parameter | Description |
| :---          | :--- |
| clientmode   | (**required**) Determines the tracking mode. A value of **active** forces visual tracking mode when it can't be established passively. |

**Request headers**

- None

**Request body**

- None

**Response**

- None

**Status code**

- Standard status codes.


## Holographic thermal

### Get the thermal stage of the device

**Request**

You can get the thermal stage of the device by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| GET | /api/holographic/ |

**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

The possible values are indicated by the following table.

| Value | Description |
| --- | --- |
| 1 | Normal |
| 2 | Warm |
| 3 | Critical |

**Status code**

- Standard status codes.

## HSimulation control
### Create a control stream or post data to a created stream

**Request**

You can create a control stream or post data to a created stream by using the following request format. The posted data is expected to be of type **application/octet-stream**.
 
| Method      | Request URI |
| :------     | :----- |
| POST | /api/holographic/simulation/control/stream |


**URI parameters**

You can specify the following additional parameters on the request URI:

| URI parameter | Description |
| :---          | :--- |
| priority   | (**required if creating a control stream**) Indicates the priority of the stream. |
| streamid   | (**required if posting to a created stream**) The identifier for the stream to post to. |

**Request headers**

- None

**Request body**

- None

**Response**

- None

**Status code**

- Standard status codes.

### Delete a control stream

**Request**

You can delete a control stream by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| DELETE | /api/holographic/simulation/control/stream |


**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

- None

**Status code**

- Standard status codes.

### Get a control stream

**Request**

You can open a web socket connection for a control stream by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| GET/WebSocket | /api/holographic/simulation/control/stream |


**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

- None

**Status code**

- Standard status codes.

### Get the simulation mode

**Request**

You can get the simluation mode by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| GET | /api/holographic/simulation/control/mode |


**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

- None

**Status code**

- Standard status codes.

### Set the simulation mode

**Request**

You can set the simulation mode by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| POST | /api/holographic/simluation/control/mode |


**URI parameters**

You can specify the following additional parameters on the request URI:

| URI parameter | Description |
| :---          | :--- |
| mode   | (**required**) Indicates the simulation mode. Possible values include **default**, **simulation**, **remote**, and **legacy**. |

**Request headers**

- None

**Request body**

- None

**Response**

- None

**Status code**

- Standard status codes.

## HSimulation playback

### Delete a recording

**Request**

You can delete a recording by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| DELETE | /api/holographic/simulation/playback/file |


**URI parameters**

You can specify the following additional parameters on the request URI:

| URI parameter | Description |
| :---          | :--- |
| recording   | (**required**) The name of the recording to delete. |

**Request headers**

- None

**Request body**

- None

**Response**

- None

**Status code**

- Standard status codes.

### Get all recordings

**Request**

You can get all the available recordings by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| GET | /api/holographic/simulation/playback/files |


**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

- None

**Status code**

- Standard status codes.

### Get the types of data in a loaded recording

**Request**

You can get the types of data in a loaded recording by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| GET | /api/holographic/simulation/playback/session/types |


**URI parameters**

You can specify the following additional parameters on the request URI:

| URI parameter | Description |
| :---          | :--- |
| recording   | (**required**) The name of the recording you are interested in. |

**Request headers**

- None

**Request body**

- None

**Response**

- None

**Status code**

- Standard status codes.

### Get all the loaded recordings

**Request**

You can get all the loaded recordings by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| GET | /api/holographic/simulation/playback/session/files |


**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

- None

**Status code**

- Standard status codes.

### Get the current playback state of a recording 

**Request**

You can get the current playback state of a recording by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| GET | /api/holographic/simulation/playback/session |


**URI parameters**

You can specify the following additional parameters on the request URI:

| URI parameter | Description |
| :---          | :--- |
| recording   | (**required**) The name of the recording that you are interested in. |

**Request headers**

- None

**Request body**

- None

**Response**

- None

**Status code**

- Standard status codes.

### Load a recording

**Request**

You can load a recording by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| POST | /api/holographic/simulation/playback/session/file |


**URI parameters**

You can specify the following additional parameters on the request URI:

| URI parameter | Description |
| :---          | :--- |
| recording   | (**required**) The name of the recording to load. |

**Request headers**

- None

**Request body**

- None

**Response**

- None

**Status code**

- Standard status codes.

### Pause a recording

**Request**

You can pause a recording by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| POST | /api/holographic/simulation/playback/session/pause |


**URI parameters**

You can specify the following additional parameters on the request URI:

| URI parameter | Description |
| :---          | :--- |
| recording   | (**required**) The name of the recording to pause. |

**Request headers**

- None

**Request body**

- None

**Response**

- None

**Status code**

- Standard status codes.

### Play a recording

**Request**

You can play a recording by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| POST | /api/holographic/simulation/playback/session/play |


**URI parameters**

You can specify the following additional parameters on the request URI:

| URI parameter | Description |
| :---          | :--- |
| recording   | (**required**) The name of the recording to play. |

**Request headers**

- None

**Request body**

- None

**Response**

- None

**Status code**

- Standard status codes.

### Stop a recording

**Request**

You can stop a recording by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| POST | /api/holographic/simulation/playback/session/stop |


**URI parameters**

You can specify the following additional parameters on the request URI:

| URI parameter | Description |
| :---          | :--- |
| recording   | (**required**) The name of the recording to stop. |

**Request headers**

- None

**Request body**

- None

**Response**

- None

**Status code**

- Standard status codes.

### Unload a recording

**Request**

You can unload a recording by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| DELETE | /api/holographic/simulation/playback/session/file |


**URI parameters**

You can specify the following additional parameters on the request URI:

| URI parameter | Description |
| :---          | :--- |
| recording   | (**required**) The name of the recording to unload. |

**Request headers**

- None

**Request body**

- None

**Response**

- None

**Status code**

- Standard status codes.

### Upload a recording

**Request**

You can upload a recording by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| POST | /api/holographic/simulation/playback/file |


**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

- None

**Status code**

- Standard status codes.

## HSimulation recording

### Get the recording state

**Request**

You can get the current recording state by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| GET | /api/holographic/simulation/recording/status |


**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

- None

**Status code**

- Standard status codes.

### Start a recording

**Request**

You can start a recording by using the following request format. There can only be one active recording at a time. 
 
| Method      | Request URI |
| :------     | :----- |
| POST | /api/holographic/simulation/recording/start |


**URI parameters**

You can specify the following additional parameters on the request URI:

| URI parameter | Description |
| :---          | :--- |
| head   | (**see below**) Set this value to 1 to indicate the system should record head data. |
| hands   | (**see below**) Set this value to 1 to indicate the system should record hands data. |
| spatialMapping   | (**see below**) Set this value to 1 to indicate the system should record spatial mapping data. |
| environment   | (**see below**) Set this value to 1 to indicate the system should record environment data. |
| name   | (**required**) The name of the recording. |
| singleSpatialMappingFrame   | (**optional**) Set this value to 1 to indicate that only a single sptial mapping frame should be recorded. |

For these parameters, exactly one of the following parameters must be set to 1: *head*, *hands*, *spatialMapping*, or *environment*.

**Request headers**

- None

**Request body**

- None

**Response**

- None

**Status code**

- Standard status codes.

### Stop the current recording

**Request**

You can stop the current recording by using the following request format. The recording will be returned as a file.
 
| Method      | Request URI |
| :------     | :----- |
| POST | /api/holographic/simulation/recording/stop |


**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

- None

**Status code**

- Standard status codes.

## Mixed reality capture

### Delete a mixed reality capture (MRC) recording from the device

**Request**

You can delete an MRC recording by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
DELETE | /api/holographic/mrc/file |


**URI parameters**

You can specify the following additional parameters on the request URI:

| URI parameter | Description |
| :---          | :--- |
| filename   | (**required**) The name of the video file to delete. The name should be hex64 encoded. |

**Request headers**

- None

**Request body**

- None

**Response**

- None

**Status code**

- Standard status codes.

### Download a mixed reality capture (MRC) file

**Request**

You can download an MRC file from the device by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| GET | /api/holographic/mrc/file |


**URI parameters**

You can specify the following additional parameters on the request URI:

| URI parameter | Description |
| :---          | :--- |
| filename   | (**required**) The name of the video file you want to get. The name should be hex64 encoded. |
| op   | (**optional**) Set this value to **stream** if you want to download a stream. |

**Request headers**

- None

**Request body**

- None

**Response**

- None

**Status code**

- Standard status codes.

### Get the mixed reality capture (MRC) settings

**Request**

You can get the MRC settings by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| GET | /api/holographic/mrc/settings |


**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

- None

**Status code**

- Standard status codes.

### Get the status of the mixed reality capture (MRC) recording

**Request**

You can get the MRC recording status by using the following request format. The possible values include **running** and **stopped**.
 
| Method      | Request URI |
| :------     | :----- |
| GET | /api/holographic/mrc/status |


**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

- None

**Status code**

- Standard status codes.

### Get the list of mixed reality capture (MRC) files

**Request**

You can get the MRC files stored on the device by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| GET | /api/holographic/mrc/files |


**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

- None

**Status code**

- Standard status codes.

### Set the mixed reality capture (MRC) settings

**Request**

You can set the MRC settings by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| POST | /api/holographic/mrc/settings |


**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

- None

**Status code**

- Standard status codes.

### Starts a mixed reality capture (MRC) recording

**Request**

You can start an MRC recording by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| POST | /api/holographic/mrc/video/control/start |


**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

- None

**Status code**

- Standard status codes.

### Stop the current mixed reality capture (MRC) recording

**Request**

You can stop the current MRC recording by using the following request format.
 
| Method      | Request URI |
| :------     | :----- |
| POST | /api/holographic/mrc/video/control/stop |


**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

- None

**Status code**

- Standard status codes.

### Take a mixed reality capture (MRC) photo

**Request**

You can take an MRC photo by using the following request format. The photo is returned in JPEG format.
 
| Method      | Request URI |
| :------     | :----- |
| GET | /api/holographic/mrc/photo |


**URI parameters**

- None

**Request headers**

- None

**Request body**

- None

**Response**

- None

**Status code**

- Standard status codes.

## Mixed reality streaming

### Initiates a chunked download of a fragmented mp4

**Request**

You can initiate a chunked download of a fragmented mp4 by using the following request format. This API uses the default quality.
 
| Method      | Request URI |
| :------     | :----- |
| GET | /api/holographic/stream/live.mp4 |


**URI parameters**

You can specify the following additional parameters on the request URI:

| URI parameter | Description |
| :---          | :--- |
| pv   | (**optional**) Indiates whether to capture the PV camera. Should be **true** or **false**. |
| holo   | (**optional**) Indiates whether to capture holograms. Should be **true** or **false**. |
| mic   | (**optional**) Indiates whether to capture the microphone. Should be **true** or **false**. |
| loopback   | (**optional**) Indiates whether to capture the application audio. Should be **true** or **false**. |

**Request headers**

- None

**Request body**

- None

**Response**

- None

**Status code**

- Standard status codes.

### Initiates a chunked download of a fragmented mp4

**Request**

You can initiate a chunked download of a fragmented mp4 by using the following request format. This API uses the high quality.
 
| Method      | Request URI |
| :------     | :----- |
| GET | /api/holographic/stream/live_high.mp4 |


**URI parameters**

You can specify the following additional parameters on the request URI:

| URI parameter | Description |
| :---          | :--- |
| pv   | (**optional**) Indiates whether to capture the PV camera. Should be **true** or **false**. |
| holo   | (**optional**) Indiates whether to capture holograms. Should be **true** or **false**. |
| mic   | (**optional**) Indiates whether to capture the microphone. Should be **true** or **false**. |
| loopback   | (**optional**) Indiates whether to capture the application audio. Should be **true** or **false**. |

**Request headers**

- None

**Request body**

- None

**Response**

- None

**Status code**

- Standard status codes.

### Initiates a chunked download of a fragmented mp4

**Request**

You can initiate a chunked download of a fragmented mp4 by using the following request format. This API uses the low quality.
 
| Method      | Request URI |
| :------     | :----- |
| GET | /api/holographic/stream/live_low.mp4 |


**URI parameters**

You can specify the following additional parameters on the request URI:

| URI parameter | Description |
| :---          | :--- |
| pv   | (**optional**) Indiates whether to capture the PV camera. Should be **true** or **false**. |
| holo   | (**optional**) Indiates whether to capture holograms. Should be **true** or **false**. |
| mic   | (**optional**) Indiates whether to capture the microphone. Should be **true** or **false**. |
| loopback   | (**optional**) Indiates whether to capture the application audio. Should be **true** or **false**. |

**Request headers**

- None

**Request body**

- None

**Response**

- None

**Status code**

- Standard status codes.

### Initiates a chunked download of a fragmented mp4

**Request**

You can initiate a chunked download of a fragmented mp4 by using the following request format. This API uses the medium quality.
 
| Method      | Request URI |
| :------     | :----- |
| GET | /api/holographic/stream/live_med.mp4 |


**URI parameters**

You can specify the following additional parameters on the request URI:

| URI parameter | Description |
| :---          | :--- |
| pv   | (**optional**) Indiates whether to capture the PV camera. Should be **true** or **false**. |
| holo   | (**optional**) Indiates whether to capture holograms. Should be **true** or **false**. |
| mic   | (**optional**) Indiates whether to capture the microphone. Should be **true** or **false**. |
| loopback   | (**optional**) Indiates whether to capture the application audio. Should be **true** or **false**. |

**Request headers**

- None

**Request body**

- None

**Response**

- None

**Status code**

- Standard status codes.
