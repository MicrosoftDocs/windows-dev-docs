---
title: Index buffers
description: Index buffers are memory buffers that contain index data, which are integer offsets into vertex buffers, used to render primitives.
ms.assetid: 14D3DEC5-CF74-488B-BE41-16BF5E3201BE
keywords:
- Index buffers
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Index buffers


*Index buffers* are memory buffers that contain index data, which are integer offsets into vertex buffers, used to render primitives.

Index buffers are memory buffers that contain index data. Index data, or indices, are integer offsets into vertex buffers and are used to render primitives.

A vertex buffer contains vertices; therefore, you can draw a vertex buffer either with or without indexed primitives. However, because an index buffer contains indices, you cannot use an index buffer without a corresponding vertex buffer.

## <span id="Index_Buffer_Description"></span><span id="index_buffer_description"></span><span id="INDEX_BUFFER_DESCRIPTION"></span>Index Buffer Description


An index buffer is described in terms of its capabilities, such as where it exists in memory, whether it supports reading and writing, and the type and number of indices it can contain.

Index buffer descriptions tell your application how an existing buffer was created. You provide an empty description structure for the system to fill with the capabilities of a previously created index buffer.

## <span id="Index_Processing_Requirements"></span><span id="index_processing_requirements"></span><span id="INDEX_PROCESSING_REQUIREMENTS"></span>Index Processing Requirements


The performance of index processing operations depends heavily on where the index buffer exists in memory and what type of rendering device is being used. Applications control the memory allocation for index buffers when they are created.

The application can directly write indices to an index buffer allocated in driver-optimal memory. This technique prevents a redundant copy operation later. This technique does not work well if your application reads data back from an index buffer, because read operations done by the host from driver-optimal memory can be very slow. Therefore, if your application needs to read during processing or writes data to the buffer erratically, a system-memory index buffer is a better choice.

## <span id="related-topics"></span>Related topics


[Vertex and index buffers](vertex-and-index-buffers.md)

 

 




