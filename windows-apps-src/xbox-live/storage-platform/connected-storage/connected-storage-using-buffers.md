---
title: Working with Connected Storage buffers
author: KevinAsgari
description: Learn about working with Connected Storage buffers.
ms.assetid: 1d9d1b52-4bfe-4cd9-8b80-50ca6c0e9ae1
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, connected storage
ms.localizationpriority: low
---

# Working with Connected Storage buffers

The Connected Storage API uses **Windows::Storage::Streams::Buffer** instances to pass data to and from an application. Because WinRT types cannot expose raw pointers, access to the data of a Buffer instance occurs through **DataReader** and **DataWriter** classes. However, **Buffer** also implements the COM interface **IBufferByteAccess**, which makes it possible to obtain a pointer directly to the buffer data.

### To get a pointer to a Buffer instance's data

1.  Use **reinterpret\_cast** to cast the Buffer instance as **IUnknown**.

```cpp
        IUnknown* unknown = reinterpret_cast<IUnknown*>(buffer);
```

2.  Query the **IUnknown** interface for the **IBufferByteAccess** COM interface.

```cpp
        Microsoft::WRL::ComPtr<IBufferByteAccess> bufferByteAccess;
        HRESULT hr = unknown->QueryInterface(_uuidof(IBufferByteAccess), &bufferByteAccess);
        if (FAILED(hr))
        return nullptr;
```

3.  Use **IBufferByteAccess::Buffer** to get a pointer directly to the buffer's data.

```cpp
        byte* bytes = nullptr;
        bufferByteAccess->Buffer(&bytes);
```

For example, the following code sample shows how to create a buffer that holds the current system time. Since buffers have a separate capacity and length value it is necessary to explicitly set both the capacity and length. By default, the length is 0.

```cpp
    inline byte* GetBufferData(Windows::Storage::Streams::IBuffer^ buffer)
    {
        using namespace Windows::Storage::Streams;
        IUnknown* unknown = reinterpret_cast<IUnknown*>(buffer);
        Microsoft::WRL::ComPtr<IBufferByteAccess> bufferByteAccess;
        HRESULT hr = unknown->QueryInterface(_uuidof(IBufferByteAccess), &bufferByteAccess);
        if (FAILED(hr))
        return nullptr;
        byte* bytes = nullptr;
        bufferByteAccess->Buffer(&bytes);
        return bytes;
    }

    IBuffer^ WrapRawBuffer( void* ptr, size_t size )
    {
        using namespace Windows::Storage::Streams;

        //uint32 size = sizeof(FILETIME);
        Buffer^ buffer = ref new Buffer(size);
        buffer->Length = size;

        memcpy(GetBufferData(buffer),ptr,size);


        return buffer;

    };
```
