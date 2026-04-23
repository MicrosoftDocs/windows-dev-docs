---
title: Use streaming - .NET TraceProcessing
description: In this tutorial, learn how to use streaming to access trace data right away and using less memory.
ms.date: 08/19/2024
ms.topic: tutorial
---

# Use streaming with TraceProcessor

By default, TraceProcessor accesses data by loading it into memory as the trace is processed. This buffering approach is easy to use, but it can be expensive in terms of memory usage.

TraceProcessor also provides trace.UseStreaming(), which supports accessing multiple types of trace data in a streaming manner (processing data as it is read from the trace file, rather than buffering that data in memory). For example, a syscalls trace can be quite large, and buffering the entire list of syscalls in a trace can be quite expensive.

## Accessing buffered data

The following code shows accessing syscall data in the normal, buffered manner via trace.UseSyscalls():

```csharp
using Microsoft.Windows.EventTracing;
using Microsoft.Windows.EventTracing.Processes;
using Microsoft.Windows.EventTracing.Syscalls;
using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.Error.WriteLine("Usage: <trace.etl>");
            return;
        }

        using (ITraceProcessor trace = TraceProcessor.Create(args[0]))
        {
            IPendingResult<ISyscallDataSource> pendingSyscallData = trace.UseSyscalls();

            trace.Process();

            ISyscallDataSource syscallData = pendingSyscallData.Result;

            Dictionary<IProcess, int> syscallsPerCommandLine = new Dictionary<IProcess, int>();

            foreach (ISyscall syscall in syscallData.Syscalls)
            {
                IProcess process = syscall.Thread?.Process;

                if (process == null)
                {
                    continue;
                }

                if (!syscallsPerCommandLine.ContainsKey(process))
                {
                    syscallsPerCommandLine.Add(process, 0);
                }

                ++syscallsPerCommandLine[process];
            }

            Console.WriteLine("Process Command Line: Syscalls Count");

            foreach (IProcess process in syscallsPerCommandLine.Keys)
            {
                Console.WriteLine($"{process.CommandLine}: {syscallsPerCommandLine[process]}");
            }
        }
    }
}
```

## Accessing streaming data

With a large syscalls trace, attempting to buffer the syscall data in memory can be quite expensive, or it may not even be possible. The following code shows how to access the same syscall data in a streaming manner, replacing trace.UseSyscalls() with trace.UseStreaming().UseSyscalls():

```csharp
using Microsoft.Windows.EventTracing;
using Microsoft.Windows.EventTracing.Processes;
using Microsoft.Windows.EventTracing.Syscalls;
using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.Error.WriteLine("Usage: <trace.etl>");
            return;
        }

        using (ITraceProcessor trace = TraceProcessor.Create(args[0]))
        {
            IPendingResult<IThreadDataSource> pendingThreadData = trace.UseThreads();

            Dictionary<IProcess, int> syscallsPerCommandLine = new Dictionary<IProcess, int>();

            trace.UseStreaming().UseSyscalls(ConsumerSchedule.SecondPass, context =>
            {
                Syscall syscall = context.Data;
                IProcess process = syscall.GetThread(pendingThreadData.Result)?.Process;

                if (process == null)
                {
                    return;
                }

                if (!syscallsPerCommandLine.ContainsKey(process))
                {
                    syscallsPerCommandLine.Add(process, 0);
                }

                ++syscallsPerCommandLine[process];
            });

            trace.Process();

            Console.WriteLine("Process Command Line: Syscalls Count");

            foreach (IProcess process in syscallsPerCommandLine.Keys)
            {
                Console.WriteLine($"{process.CommandLine}: {syscallsPerCommandLine[process]}");
            }
        }
    }
}
```

## How streaming works

By default, all streaming data is provided during the first pass through the trace, and buffered data from other sources is not available. The example above shows how to combine streaming with buffering – thread data is buffered before syscall data is streamed. As a result, the trace must be read twice – once to get buffered thread data, and a second time to access streaming syscall data with the buffered thread data now available. In order to combine streaming and buffering in this way, the example passes ConsumerSchedule.SecondPass to trace.UseStreaming().UseSyscalls(), which causes syscall processing to happen in a second pass through the trace. By running in a second pass, the syscall callback can access the pending result from trace.UseThreads() when it processes each syscall. Without this optional argument, syscall streaming would have run in the first pass through the trace (there would be only one pass), and the pending result from trace.UseThreads() would not be available yet. In that case, the callback would still have access to the ThreadId from the syscall, but it would not have access to the process for the thread (because thread to process linking data is provided via other events which may not have been processed yet).

Some key differences in usage between buffering and streaming:

1. Buffering returns an [IPendingResult&lt;T&gt;](/dotnet/api/microsoft.windows.eventtracing.ipendingresult-1), and the result it holds is available only before the trace has been processed. After the trace has been processed, the results can be enumerated using techniques such as foreach and LINQ.
2. Streaming returns void and instead takes a callback argument. It calls the callback once as each item becomes available. Because the data is not buffered, there is never a list of results to enumerate with foreach or LINQ – the streaming callback needs to buffer whatever part of the data it wants to save for use after processing has completed.
3. The code for processing buffered data appears after the call to trace.Process(), when the pending results are available.
4. The code for processing streaming data appears before the call to trace.Process(), as a callback to the trace.UseStreaming.Use...() method.
5. A streaming consumer can choose to process only part of the stream and cancel future callbacks by calling context.Cancel(). A buffering consumer always is provided a full, buffered list.

## Correlated streaming data

Sometimes trace data comes in a sequence of events – for example, syscalls are logged via separate enter and exit events, but the combined data from both events can be more helpful. The method trace.UseStreaming().UseSyscalls() correlates the data from both of these events and provides it as the pair becomes available. A few types of correlated data are available via trace.UseStreaming():

| Code                                        | Description                                                                                                                                     |
|---------------------------------------------|-------------------------------------------------------------------------------------------------------------------------------------------------|
| trace.UseStreaming().UseContextSwitchData() | Streams correlated context switch data (from compact and non-compact events, with more accurate SwitchInThreadIds than raw non-compact events). |
| trace.UseStreaming().UseScheduledTasks()    | Streams correlated scheduled task data.                                                                                                         |
| trace.UseStreaming().UseSyscalls()          | Streams correlated system call data.                                                                                                            |
| trace.UseStreaming().UseWindowInFocus()     | Streams correlated window-in-focus data.                                                                                                        |

## Standalone streaming events

Additionally, trace.UseStreaming() provides parsed events for a number of different standalone event types:

| Code                                               | Description                                     |
|----------------------------------------------------|-------------------------------------------------|
| trace.UseStreaming().UseLastBranchRecordEvents()   | Streams parsed last branch record (LBR) events. |
| trace.UseStreaming().UseReadyThreadEvents()        | Streams parsed ready thread events.             |
| trace.UseStreaming().UseThreadCreateEvents()       | Streams parsed thread create events.            |
| trace.UseStreaming().UseThreadExitEvents()         | Streams parsed thread exit events.              |
| trace.UseStreaming().UseThreadRundownStartEvents() | Streams parsed thread rundown start events.     |
| trace.UseStreaming().UseThreadRundownStopEvents()  | Streams parsed thread rundown stop events.      |
| trace.UseStreaming().UseThreadSetNameEvents()      | Streams parsed thread set name events.          |

## Underlying streaming events for correlated data

Finally, trace.UseStreaming() also provides the underlying events used to correlate data in the list above. These underlying events are:

| Code                                                        | Description                                                                                | Included in                                 |
|-------------------------------------------------------------|--------------------------------------------------------------------------------------------|---------------------------------------------|
| trace.UseStreaming().UseCompactContextSwitchEvents()        | Streams parsed compact context switch events.                                              | trace.UseStreaming().UseContextSwitchData() |
| trace.UseStreaming().UseContextSwitchEvents()               | Streams parsed context switch events. SwitchInThreadIds may not be accurate in some cases. | trace.UseStreaming().UseContextSwitchData() |
| trace.UseStreaming().UseFocusChangeEvents()                 | Streams parsed window focus change events.                                                 | trace.UseStreaming().UseWindowInFocus()     |
| trace.UseStreaming().UseScheduledTaskStartEvents()          | Streams parsed scheduled task start events.                                                | trace.UseStreaming().UseScheduledTasks()    |
| trace.UseStreaming().UseScheduledTaskStopEvents()           | Streams parsed scheduled task stop events.                                                 | trace.UseStreaming().UseScheduledTasks()    |
| trace.UseStreaming().UseScheduledTaskTriggerEvents()        | Streams parsed scheduled task trigger events.                                              | trace.UseStreaming().UseScheduledTasks()    |
| trace.UseStreaming().UseSessionLayerSetActiveWindowEvents() | Streams parsed session-layer set active window events.                                     | trace.UseStreaming().UseWindowInFocus()     |
| trace.UseStreaming().UseSyscallEnterEvents()                | Streams parsed syscall enter events.                                                       | trace.UseStreaming().UseSyscalls()          |
| trace.UseStreaming().UseSyscallExitEvents()                 | Streams parsed syscall exit events.                                                        | trace.UseStreaming().UseSyscalls()          |

## Next Steps

In this tutorial, you learned how to use streaming to access trace data right away and using less memory.

The next step is to look access the data you want from your traces. Look at the [samples](https://github.com/microsoft/eventtracing-processing-samples) for some ideas. Note that not all traces include all supported types of data.