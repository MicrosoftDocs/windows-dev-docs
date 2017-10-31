---
author: msatranjr
ms.assetid: 5B3A6326-15EE-4618-AA8C-F1C7FB5232FB
title: Bluetooth RFCOMM
description: This article provides an overview of Bluetooth RFCOMM in Universal Windows Platform (UWP) apps, along with example code on how to send or receive a file.
ms.author: misatran
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
localizationpriority: medium
---
# Bluetooth RFCOMM


**Important APIs**

-   [**Windows.Devices.Bluetooth**](https://msdn.microsoft.com/library/windows/apps/Dn263413)
-   [**Windows.Devices.Bluetooth.Rfcomm**](https://msdn.microsoft.com/library/windows/apps/Dn263529)

This article provides an overview of Bluetooth RFCOMM in Universal Windows Platform (UWP) apps, along with example code on how to send or receive a file..

## Overview

The APIs in the [**Windows.Devices.Bluetooth.Rfcomm**](https://msdn.microsoft.com/library/windows/apps/Dn263529) namespace build on existing patterns for Windows.Devices, including [**enumeration**](https://msdn.microsoft.com/library/windows/apps/BR225459) and [**instantiation**](https://msdn.microsoft.com/library/windows/apps/BR225654). Data reading and writing is designed to take advantage of [**established data stream patterns**](https://msdn.microsoft.com/library/windows/apps/BR208119) and objects in [**Windows.Storage.Streams**](https://msdn.microsoft.com/library/windows/apps/BR241791). Service Discovery Protocol (SDP) attributes have a value and an expected type. However, some common devices have faulty implementations of SDP attributes where the value is not of the expected type. Additionally, many usages of RFCOMM do not require additional SDP attributes at all. For these reasons, this API offers access to the unparsed SDP data, from which developers can obtain the information they need.

The RFCOMM APIs use the concept of service identifiers. Although a service identifier is simply a 128-bit GUID, it is also commonly specified as either a 16- or 32-bit integer. The RFCOMM API offers a wrapper for service identifiers that allows them be specified and consumed as 128-bit GUIDs as well as 32-bit integers but does not offer 16-bit integers. This is not an issue for the API because languages will automatically upsize to a 32-bit integer and the identifier can still be correctly generated.

Apps can perform multi-step device operations in a background task so that they can run to completion even if the app is moved to the background and suspended. This allows for reliable device servicing such as changes to persistent settings or firmware, and content synchronization, without requiring the user to sit and watch a progress bar. Use the [**DeviceServicingTrigger**](https://msdn.microsoft.com/library/windows/apps/Dn297315) for device servicing and the [**DeviceUseTrigger**](https://msdn.microsoft.com/library/windows/apps/Dn297337) for content synchronization. Note that these background tasks constrain the amount of time the app can run in the background, and are not intended to allow indefinite operation or infinite synchronization.

For a complete code sample that details RFCOMM operation, see the [**Bluetooth Rfcomm Chat Sample**](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/BluetoothRfcommChat) on Github.  
## Send a file as a client

When sending a file, the most basic scenario is to connect to a paired device based on a desired service. This involves the following steps:

-   Use the **RfcommDeviceService.GetDeviceSelector\*** functions to help generate an AQS query that can be used to enumerated paired device instances of the desired service.
-   Pick an enumerated device, create an [**RfcommDeviceService**](https://msdn.microsoft.com/library/windows/apps/Dn263463), and read the SDP attributes as needed (using [**established data helpers**](https://msdn.microsoft.com/library/windows/apps/BR208119) to parse the attribute's data).
-   Create a socket and use the [**RfcommDeviceService.ConnectionHostName**](https://msdn.microsoft.com/library/windows/apps/windows.devices.bluetooth.rfcomm.rfcommdeviceservice.connectionhostname.aspx) and [**RfcommDeviceService.ConnectionServiceName**](https://msdn.microsoft.com/library/windows/apps/windows.devices.bluetooth.rfcomm.rfcommdeviceservice.connectionservicename.aspx) properties to [**StreamSocket.ConnectAsync**](https://msdn.microsoft.com/library/windows/apps/Hh701504) to the remote device service with the appropriate parameters.
-   Follow established data stream patterns to read chunks of data from the file and send it on the socket's [**StreamSocket.OutputStream**](https://msdn.microsoft.com/library/windows/apps/BR226920) to the device.

```csharp
Windows.Devices.Bluetooth.RfcommDeviceService _service;
Windows.Networking.Sockets.StreamSocket _socket;

async void Initialize()
{
    // Enumerate devices with the object push service
    var services =
        await Windows.Devices.Enumeration.DeviceInformation.FindAllAsync(
            RfcommDeviceService.GetDeviceSelector(
                RfcommServiceId.ObexObjectPush));

    if (services.Count > 0)
    {
        // Initialize the target Bluetooth BR device
        var service = await RfcommDeviceService.FromIdAsync(services[0].Id);

        // Check that the service meets this App's minimum requirement
        if (SupportsProtection(service) && IsCompatibleVersion(service))
        {
            _service = service;

            // Create a socket and connect to the target
            _socket = new StreamSocket();
            await _socket.ConnectAsync(
                _service.ConnectionHostName,
                _service.ConnectionServiceName,
                SocketProtectionLevel
                    .BluetoothEncryptionAllowNullAuthentication);

            // The socket is connected. At this point the App can wait for
            // the user to take some action, e.g. click a button to send a
            // file to the device, which could invoke the Picker and then
            // send the picked file. The transfer itself would use the
            // Sockets API and not the Rfcomm API, and so is omitted here for
            // brevity.
        }
    }
}

// This App requires a connection that is encrypted but does not care about
// whether its authenticated.
bool SupportsProtection(RfcommDeviceService service)
{
    switch (service.ProtectionLevel)
    {
    case SocketProtectionLevel.PlainSocket:
        if ((service.MaximumProtectionLevel == SocketProtectionLevel
                .BluetoothEncryptionWithAuthentication)
            || (service.MaximumProtectionLevel == SocketProtectionLevel
                .BluetoothEncryptionAllowNullAuthentication)
        {
            // The connection can be upgraded when opening the socket so the
            // App may offer UI here to notify the user that Windows may
            // prompt for a PIN exchange.
            return true;
        }
        else
        {
            // The connection cannot be upgraded so an App may offer UI here
            // to explain why a connection won't be made.
            return false;
        }
    case SocketProtectionLevel.BluetoothEncryptionWithAuthentication:
        return true;
    case SocketProtectionLevel.BluetoothEncryptionAllowNullAuthentication:
        return true;
    }
    return false;
}

// This App relies on CRC32 checking available in version 2.0 of the service.
const uint SERVICE_VERSION_ATTRIBUTE_ID = 0x0300;
const byte SERVICE_VERSION_ATTRIBUTE_TYPE = 0x0A;   // UINT32
const uint MINIMUM_SERVICE_VERSION = 200;
bool IsCompatibleVersion(RfcommDeviceService service)
{
    var attributes = await service.GetSdpRawAttributesAsync(
        BluetothCacheMode.Uncached);
    var attribute = attributes[SERVICE_VERSION_ATTRIBUTE_ID];
    var reader = DataReader.FromBuffer(attribute);

    // The first byte contains the attribute' s type
    byte attributeType = reader.ReadByte();
    if (attributeType == SERVICE_VERSION_ATTRIBUTE_TYPE)
    {
        // The remainder is the data
        uint version = reader.Uint32();
        return version >= MINIMUM_SERVICE_VERSION;
    }
}
```

```cpp
Windows::Devices::Bluetooth::RfcommDeviceService^ _service;
Windows::Networking::Sockets::StreamSocket^ _socket;

void Initialize()
{
    // Enumerate devices with the object push service
    create_task(
        Windows::Devices::Enumeration::DeviceInformation::FindAllAsync(
            RfcommDeviceService::GetDeviceSelector(
                RfcommServiceId::ObexObjectPush)))
    .then([](DeviceInformationCollection^ services)
    {
        if (services->Size > 0)
        {
            // Initialize the target Bluetooth BR device
            create_task(RfcommDeviceService::FromIdAsync(services[0]->Id))
            .then([](RfcommDeviceService^ service)
            {
                // Check that the service meets this App's minimum
                // requirement
                if (SupportsProtection(service)
                    && IsCompatibleVersion(service))
                {
                    _service = service;

                    // Create a socket and connect to the target
                    _socket = ref new StreamSocket();
                    create_task(_socket->ConnectAsync(
                        _service->ConnectionHostName,
                        _service->ConnectionServiceName,
                        SocketProtectionLevel
                            ::BluetoothEncryptionAllowNullAuthentication)
                    .then([](void)
                    {
                        // The socket is connected. At this point the App can
                        // wait for the user to take some action, e.g. click
                        // a button to send a file to the device, which could
                        // invoke the Picker and then send the picked file.
                        // The transfer itself would use the Sockets API and
                        // not the Rfcomm API, and so is omitted here for
                        //brevity.
                    });
                }
            });
        }
    });
}

// This App requires a connection that is encrypted but does not care about
// whether its authenticated.
bool SupportsProtection(RfcommDeviceService^ service)
{
    switch (service->ProtectionLevel)
    {
    case SocketProtectionLevel->PlainSocket:
        if ((service->MaximumProtectionLevel == SocketProtectionLevel
                ::BluetoothEncryptionWithAuthentication)
            || (service->MaximumProtectionLevel == SocketProtectionLevel
                ::BluetoothEncryptionAllowNullAuthentication)
        {
            // The connection can be upgraded when opening the socket so the
            // App may offer UI here to notify the user that Windows may
            // prompt for a PIN exchange.
            return true;
        }
        else
        {
            // The connection cannot be upgraded so an App may offer UI here
            // to explain why a connection won't be made.
            return false;
        }
    case SocketProtectionLevel::BluetoothEncryptionWithAuthentication:
        return true;
    case SocketProtectionLevel::BluetoothEncryptionAllowNullAuthentication:
        return true;
    }
    return false;
}

// This App relies on CRC32 checking available in version 2.0 of the service.
const uint SERVICE_VERSION_ATTRIBUTE_ID = 0x0300;
const byte SERVICE_VERSION_ATTRIBUTE_TYPE = 0x0A;   // UINT32
const uint MINIMUM_SERVICE_VERSION = 200;
bool IsCompatibleVersion(RfcommDeviceService^ service)
{
    auto attributes = await service->GetSdpRawAttributesAsync(
        BluetoothCacheMode::Uncached);
    auto attribute = attributes[SERVICE_VERSION_ATTRIBUTE_ID];
    auto reader = DataReader.FromBuffer(attribute);

    // The first byte contains the attribute' s type
    byte attributeType = reader->ReadByte();
    if (attributeType == SERVICE_VERSION_ATTRIBUTE_TYPE)
    {
        // The remainder is the data
        uint version = reader->Uint32();
        return version >= MINIMUM_SERVICE_VERSION;
    }
}
```

## Receive File as a Server

Another common RFCOMM App scenario is to host a service on the PC and expose it for other devices.

-   Create a [**RfcommServiceProvider**](https://msdn.microsoft.com/library/windows/apps/Dn263511) to advertise the desired service.
-   Set the SDP attributes as needed (using [**established data helpers**](https://msdn.microsoft.com/library/windows/apps/BR208119) to generate the attribute’s Data) and starts advertising the SDP records for other devices to retrieve.
-   To connect to a client device, create a socket listener to start listening for incoming connection requests.
-   When a connection is received, store the connected socket for later processing.
-   Follow established data stream patterns to read chunks of data from the socket's InputStream and save it to a file.

In order to persist an RFCOMM service in the background, use the [**RfcommConnectionTrigger**](https://msdn.microsoft.com/library/windows/apps/windows.applicationmodel.background.rfcommconnectiontrigger.aspx). The background task is triggered on connection to the service. The developer receives a handle to the socket in the background task. The background task is long running and persists for as long as the socket is in use.    

```csharp
Windows.Devices.Bluetooth.RfcommServiceProvider _provider;

async void Initialize()
{
    // Initialize the provider for the hosted RFCOMM service
    _provider = await Windows.Devices.Bluetooth.
        RfcommServiceProvider.CreateAsync(RfcommServiceId.ObexObjectPush);

    // Create a listener for this service and start listening
    StreamSocketListener listener = new StreamSocketListener();
    listener.ConnectionReceived += OnConnectionReceived;
    await listener.BindServiceNameAsync(
        _provider.ServiceId.AsString(),
        SocketProtectionLevel
            .BluetoothEncryptionAllowNullAuthentication);

    // Set the SDP attributes and start advertising
    InitializeServiceSdpAttributes(_provider);
    _provider.StartAdvertising();
}

const uint SERVICE_VERSION_ATTRIBUTE_ID = 0x0300;
const byte SERVICE_VERSION_ATTRIBUTE_TYPE = 0x0A;   // UINT32
const uint SERVICE_VERSION = 200;
void InitializeServiceSdpAttributes(RfcommServiceProvider provider)
{
    auto writer = new Windows.Storage.Streams.DataWriter();

    // First write the attribute type
    writer.WriteByte(SERVICE_VERSION_ATTRIBUTE_TYPE)
    // Then write the data
    writer.WriteUint32(SERVICE_VERSION);

    auto data = writer.DetachBuffer();
    provider.SdpRawAttributes.Add(SERVICE_VERSION_ATTRIBUTE_ID, data);
}

void OnConnectionReceived(
    StreamSocketListener listener,
    StreamSocketListenerConnectionReceivedEventArgs args)
{
    // Stop advertising/listening so that we're only serving one client
    _provider.StopAdvertising();
    await listener.Close();
    _socket = args.Socket;

    // The client socket is connected. At this point the App can wait for
    // the user to take some action, e.g. click a button to receive a file
    // from the device, which could invoke the Picker and then save the
    // received file to the picked location. The transfer itself would use
    // the Sockets API and not the Rfcomm API, and so is omitted here for
    // brevity.
}
```

```cpp
Windows::Devices::Bluetooth::RfcommServiceProvider^ _provider;

void Initialize()
{
    // Initialize the provider for the hosted RFCOMM service
    create_task(Windows::Devices::Bluetooth.
        RfcommServiceProvider::CreateAsync(
            RfcommServiceId::ObexObjectPush))
    .then([](RfcommServiceProvider^ provider) -> task<void> {
        _provider = provider;

        // Create a listener for this service and start listening
        auto listener = ref new StreamSocketListener();
        listener->ConnectionReceived += ref new TypedEventHandler<
                StreamSocketListener^,
                StreamSocketListenerConnectionReceivedEventArgs^>
           (&OnConnectionReceived);
        return create_task(listener->BindServiceNameAsync(
            _provider->ServiceId->AsString(),
            SocketProtectionLevel
                ::BluetoothEncryptionAllowNullAuthentication));
    }).then([listener](void) {
        // Set the SDP attributes and start advertising
        InitializeServiceSdpAttributes(_provider);
        _provider->StartAdvertising();
    });
}

const uint SERVICE_VERSION_ATTRIBUTE_ID = 0x0300;
const byte SERVICE_VERSION_ATTRIBUTE_TYPE = 0x0A;   // UINT32
const uint SERVICE_VERSION = 200;
void InitializeServiceSdpAttributes(RfcommServiceProvider^ provider)
{
    auto writer = ref new Windows::Storage::Streams::DataWriter();

    // First write the attribute type
    writer->WriteByte(SERVICE_VERSION_ATTRIBUTE_TYPE)
    // Then write the data
    writer->WriteUint32(SERVICE_VERSION);

    auto data = writer->DetachBuffer();
    provider->SdpRawAttributes->Add(SERVICE_VERSION_ATTRIBUTE_ID, data);
}

void OnConnectionReceived(
    StreamSocketListener^ listener,
    StreamSocketListenerConnectionReceivedEventArgs^ args)
{
    // Stop advertising/listening so that we're only serving one client
    _provider->StopAdvertising();
    create_task(listener->Close())
    .then([args](void) {
        _socket = args->Socket;

        // The client socket is connected. At this point the App can wait for
        // the user to take some action, e.g. click a button to receive a
        // file from the device, which could invoke the Picker and then save
        // the received file to the picked location. The transfer itself
        // would use the Sockets API and not the Rfcomm API, and so is
        // omitted here for brevity.
    });
}
```
