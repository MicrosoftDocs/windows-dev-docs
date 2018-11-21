---
title: Copying and accessing resource data
description: Usage flags indicate how the application intends to use the resource data, to place resources in the most performant area of memory possible. Resource data is copied across resources so that the CPU or GPU can access it without impacting performance.
ms.assetid: 6A09702D-0FF2-4EA6-A353-0F95A3EE34E2
keywords:
- Copying and accessing resource data
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Copying and accessing resource data


Usage flags indicate how the application intends to use the resource data, to place resources in the most performant area of memory possible. Resource data is copied across resources so that the CPU or GPU can access it without impacting performance.

It isn't necessary to think about resources as being created in either video memory or system memory, or to decide whether or not the runtime should manage the memory. With the architecture of the WDDM (Windows Display Driver Model), applications create Direct3D resources with different usage flags to indicate how the application intends to use the resource data. This driver model virtualizes the memory used by resources; it is the responsibility of the operating system/driver/memory manager to place resources in the most performant area of memory possible, given the expected usage.

The default case is for resources to be available to the GPU. There are times when the resource data needs to be available to the CPU. Copying resource data around so that the appropriate processor can access it without impacting performance requires some knowledge of how the API methods work.

## <span id="Copying"></span><span id="copying"></span><span id="COPYING"></span>Copying resource data


Resources are created in memory when Direct3D executes a Create call. They can be created in video memory, system memory, or any other kind of memory. Since WDDM driver model virtualizes this memory, applications no longer need to keep track of what kind of memory resources are created in.

Ideally, all resources would be located in video memory so that the GPU can have immediate access to them. However, it is sometimes necessary for the CPU to read the resource data or for the GPU to access resource data the CPU has written to. Direct3D handles these different scenarios by requesting the application specify a usage, and then offers several methods for copying resource data when necessary.

Depending on how the resource was created, it is not always possible to directly access the underlying data. This may mean that the resource data must be copied from the source resource to another resource that is accessible by the appropriate processor. In terms of Direct3D, default resources can be accessed directly by the GPU, dynamic and staging resources can be directly accessed by the CPU.

Once a resource has been created, its usage cannot be changed. Instead, copy the contents of one resource to another resource that was created with a different usage. You copy resource data from one resource to another, or copy data from memory to a resource.

There are two main kinds of resources: mappable and non-mappable. Resources created with dynamic or staging usages are mappable, while resources created with default or immutable usages are non-mappable.

Copying data among non-mappable resources is very fast because this is the most common case and has been optimized to perform well. Since these resources are not directly accessible by the CPU, they are optimized so that the GPU can manipulate them quickly.

Copying data among mappable resources is more problematic because the performance will depend on the usage the resource was created with. For example, the GPU can read a dynamic resource fairly quickly but cannot write to them, and the GPU cannot read or write to staging resources directly.

Applications that wish to copy data from a resource with default usage to a resource with staging usage (to allow the CPU to read the data -- that is, the GPU readback problem) must do so with care. See [Accessing resource data](#accessing), below.

## <span id="Accessing"></span><span id="accessing"></span><span id="ACCESSING"></span>Accessing resource data


Accessing a resource requires mapping the resource; mapping essentially means the application is trying to give the CPU access to memory. The process of mapping a resource so that the CPU can access the underlying memory can cause some performance bottlenecks and for this reason, care must be taken as to how and when to perform this task.

Performance can grind to a halt if the application tries to map a resource at the wrong time. If the application tries to access the results of an operation before that operation is finished, a pipeline stall will occur.

Performing a map operation at the wrong time could potentially cause a severe drop in performance by forcing the GPU and the CPU to synchronize with each other. This synchronization will occur if the application wants to access a resource before the GPU is finished copying it into a resource the CPU can map.

### <span id="Performance_Considerations"></span><span id="performance_considerations"></span><span id="PERFORMANCE_CONSIDERATIONS"></span>Performance considerations

It is best to think of a PC as a machine running as a parallel architecture with two main types of processors: one or more CPU's and one or more GPU's. As in any parallel architecture, the best performance is achieved when each processor is scheduled with enough tasks to prevent it from going idle and when the work of one processor is not waiting on the work of another.

The worst-case scenario for GPU/CPU parallelism is the need to force one processor to wait for the results of work done by another. Direct3D removes this cost by making the copy methods asynchronous; the copy has not necessarily executed by the time the method returns.

The benefit of this is that the application does not pay the performance cost of actually copying the data until the CPU accesses the data, which is when Map is called. If the Map method is called after the data has actually been copied, no performance loss occurs. On the other hand, if the Map method is called before the data has been copied, then a pipeline stall will occur.

Asynchronous calls in Direct3D (which are the vast majority of methods, and especially rendering calls) are stored in what is called a *command buffer*. This buffer is internal to the graphics driver and is used to batch calls to the underlying hardware so that the costly switch from user mode to kernel mode in Microsoft Windows occurs as rarely as possible.

The command buffer is flushed, thus causing a user/kernel mode switch, in one of four situations, which are as follows.

1.  Present is called.
2.  Flush is called.
3.  The command buffer is full; its size is dynamic and is controlled by the Operating System and the graphics driver.
4.  The CPU requires access to the results of a command waiting to execute in the command buffer.

Of the four situations above, number four is the most critical to performance. If the application issues a call to copy a resource, or subresource, this call is queued in the command buffer.

If the application then tries to map the staging resource that was the target of the copy call before the command buffer has been flushed, a pipeline stall will occur, because not only does the Copy method call need to execute, but all other buffered commands in the command buffer must execute as well. This will cause the GPU and CPU to synchronize because the CPU will be waiting to access the staging resource while the GPU is emptying the command buffer and finally filling the resource the CPU needs. Once the GPU finishes the copy, the CPU will begin accessing the staging resource, but during this time, the GPU will be sitting idle.

Doing this frequently at runtime will severely degrade performance. For that reason, mapping of resources created with default usage should be done with care. The application needs to wait long enough for the command buffer to be emptied and thus have all of those commands finish executing before it tries to map the corresponding staging resource.

How long should the application wait? At least two frames because this will enable parallelism between the CPU(s) and the GPU to be maximally leveraged. The way the GPU works is that while the application is processing frame N by submitting calls to the command buffer, the GPU is busy executing the calls from the previous frame, N-1.

So if an application wants to map a resource that originates in video memory and copies a resource at frame N, this call will actually begin to execute at frame N+1, when the application is submitting calls for the next frame. The copy should be finished when the application is processing frame N+2.

<table>
<colgroup>
<col width="50%" />
<col width="50%" />
</colgroup>
<thead>
<tr class="header">
<th align="left">Frame</th>
<th align="left">GPU/CPU Status</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td align="left">N</td>
<td align="left"><ul>
<li>CPU issues render calls for current frame.</li>
</ul></td>
</tr>
<tr class="even">
<td align="left">N+1</td>
<td align="left"><ul>
<li>GPU executing calls sent from CPU during frame N.</li>
<li>CPU issues render calls for current frame.</li>
</ul></td>
</tr>
<tr class="odd">
<td align="left">N+2</td>
<td align="left"><ul>
<li>GPU finished executing calls sent from CPU during frame N. Results ready.</li>
<li>GPU executing calls sent from CPU during frame N+1.</li>
<li>CPU issues render calls for current frame.</li>
</ul></td>
</tr>
<tr class="even">
<td align="left">N+3</td>
<td align="left"><ul>
<li>GPU finished executing calls sent from CPU during frame N+1. Results ready.</li>
<li>GPU executing calls sent from CPU during frame N+2.</li>
<li>CPU issues render calls for current frame.</li>
</ul></td>
</tr>
<tr class="odd">
<td align="left">N+4</td>
<td align="left">...</td>
</tr>
</tbody>
</table>

 

## <span id="related-topics"></span>Related topics


[Resources](resources.md)

 

 




