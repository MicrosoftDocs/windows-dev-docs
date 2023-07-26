---
title: Access trace data - .NET TraceProcessing
description: In this tutorial, learn how to access trace data using TraceProcessor.
author: maiak
ms.author: maiak
ms.date: 02/23/2020
ms.topic: tutorial
---

# Access trace data

.NET TraceProcessing is available from [NuGet](https://www.nuget.org/packages/Microsoft.Windows.EventTracing.Processing.All) with the following package ID:

Microsoft.Windows.EventTracing.Processing.All

This package allows you to access data in a trace file. If you do not already have a trace file, you can use [Windows Performance Recorder](/windows-hardware/test/wpt/start-a-recording) to create one.

The following example console app shows how to access the command lines of all processes contained in the trace:

```csharp
using Microsoft.Windows.EventTracing;
using Microsoft.Windows.EventTracing.Processes;
using System;

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
            IPendingResult<IProcessDataSource> pendingProcessData = trace.UseProcesses();

            trace.Process();

            IProcessDataSource processData = pendingProcessData.Result;

            foreach (IProcess process in processData.Processes)
            {
                Console.WriteLine(process.CommandLine);
            }
        }
    }
}
```

## Using TraceProcessor

To process a trace, call [TraceProcessor.Create](/dotnet/api/microsoft.windows.eventtracing.traceprocessor.create). The core interface is [ITraceProcessor](/dotnet/api/microsoft.windows.eventtracing.itraceprocessor), and using this interface involves the following pattern:

1. First, tell the processor what data you want to use from a trace
2. Second, process the trace; and
3. Finally, access the results.

Telling the processor what kinds of data you want up front means you do not need to spend time processing large volumes of all possible kinds of trace data. Instead, [TraceProcessor](/dotnet/api/microsoft.windows.eventtracing.traceprocessor) just does the work needed to provide the specific kinds of data you request.

## Recommended project settings

There are a couple of project settings we recommend using with TraceProcessor:

1. We recommend running exes as 64-bit.

    The Visual Studio default for a new C# .NET Framework console application is Any CPU with Prefer 32-bit checked. The default for .NET may already have the recommended setting.

    Trace processing can be memory-intensive, especially with larger traces, and we recommend changing Platform target to x64 (or unchecking Prefer 32-bit) in exes that use TraceProcessor. To change these settings, see the Build tab under Properties for the project. To change these settings for all configurations, ensure that the Configuration dropdown is set to All Configurations, rather than the default of the current configuration only.

2. We suggest using NuGet with the newer-style PackageReference mode rather than the older packages.config mode.

    To change the default for new projects, see Tools, NuGet Package Manager, Package Manager Settings, Package Management, Default package management format.

## Built-in data sources

An .etl file can capture many kinds of data in a trace. Note that which data is in an .etl file depends on what providers were enabled when the trace was captured. The following list shows the kinds of trace data available from TraceProcessor:

| Code                                      | Description                                                                                                                | Related WPA Items                                                    |
|-------------------------------------------|----------------------------------------------------------------------------------------------------------------------------|----------------------------------------------------------------------|
| trace.UseClassicEvents()                  | Provides classic ETW events from a trace, which do not include schema information.                                         | Generic Events table (when Event Type is Classic or WPP)             |
| trace.UseConnectedStandbyData()           | Provides data from a trace about the system entering and exiting connected standby.                                        | CS Summary table                                                     |
| trace.UseCpuIdleStates()                  | Provides data from a trace about CPU C-states.                                                                             | CPU Idle States table (when Type is Actual)                          |
| trace.UseCpuSamplingData()                | Provides data from a trace about CPU usage based on periodic sampling of the instruction pointer.                          | CPU Usage (Sampled) table                                            |
| trace.UseCpuSchedulingData()              | Provides data from a trace about CPU thread scheduling, including context switches and ready thread events.                | CPU Usage (Precise) table                                            |
| trace.UseDevicePowerData()                | Provides data from a trace about device D-states.                                                                          | Device DState table                                                  |
| trace.UseDirectXData()                    | Provides data from a trace about DirectX activity.                                                                         | GPU Utilization table                                                |
| traceUseDiskIOData()                      | Provides data from a trace about Disk I/O activity.                                                                        | Disk Usage table                                                     |
| trace.UseEnergyEstimationData()           | Provides data from a trace about estimated per-process energy usage from Energy Estimation Engine.                         | Energy Estimation Engine Summary (by Process) table                  |
| trace.UseEnergyMeterData()                | Provides data from a trace about measured energy usage from Energy Meter Interface (EMI).                                  | Energy Estimation Engine (by Emi) table                              |
| trace.UseFileIOData()                     | Provides data from a trace about File I/O activity.                                                                        | File I/O table                                                       |
| trace.UseGenericEvents()                  | Provides manifested and TraceLogging events from a trace.                                                                  | Generic Events table (when Event Type is Manifested or TraceLogging) |
| trace.UseHandles()                        | Provides partial data from a trace about active kernel handles.                                                            | Handles table                                                        |
| trace.UseHardFaults()                     | Provides data from a trace about hard page faults.                                                                         | Hard Faults table                                                    |
| trace.UseHeapSnapshots()                  | Provides data from a trace about process heap usage.                                                                       | Heap Snapshot table                                                  |
| trace.UseHypercalls()                     | Provides data about Hyper-V hypercalls that ocurred during a trace.                                                        |                                                                      |
| trace.UseImageSections()                  | Provides data from a trace about the sections of an image.                                                                 | Section Name column of the CPU Usage (Sampled) table                 |
| trace.UseInterruptHandlingData()          | Provides data from a trace about Interrupt Service Routine (ISR) and Deferred Procedure Call (DPC) activity.               | DPC/ISR table                                                        |
| trace.UseMarks()                          | Provides the marks (labeled timestamps) from a trace.                                                                      | Marks table                                                          |
| trace.UseMemoryUtilizationData()          | Provides data from a trace about total system memory utilization.                                                          | Memory Utilization table                                             |
| trace.UseMetadata()                       | Provides trace metadata available without further processing.                                                              | System Configuration, Traces and General                             |
| trace.UsePlatformIdleStates()             | Provides data from a trace about the target and actual platform idle states of a system.                                   | Platform Idle State table                                            |
| trace.UsePoolAllocations()                | Provides data from a trace about kernel pool memory usage.                                                                 | Pool Summary table                                                   |
| trace.UsePowerConfigurationData()         | Provides data from a trace about system power configuration.                                                               | System Configuration, Power Settings                                 |
| trace.UsePowerDependencyCoordinatorData() | Provides data from a trace about active power dependency coordinator phases.                                               | Notification Phase Summary table                                     |
| trace.UseProcesses()                      | Provides data about processes active during a trace as well as their images and PDBs.                                      | Processes table; Images table; Symbols Hub                           |
| trace.UseProcessorCounters()              | Provides data from a trace about processor performance counter values from Processor Counter Monitor (PCM).                |                                                                      |
| trace.UseProcessorFrequencyData()         | Provides data from a trace about the frequency at which processors ran.                                                    | Processor Frequency table (when Type is Actual)                      |
| trace.UseProcessorProfileData()           | Provides data from a trace about the active processor power profile.                                                       | Processor Profiles table                                             |
| trace.UseProcessorParkingData()           | Provides data from a trace about which processors were parked or unparked.                                                 | Processor Parking State table                                        |
| trace.UseProcessorParkingLimits()         | Provides data from a trace about the maximum allowed number of unparked processors.                                        | Core Parking Cap State table                                         |
| trace.UseProcessorQualityOfServiceData()  | Provides data from a trace about the quality of service level for each processor.                                          | Processor Qos Class table                                            |
| trace.UseProcessorThrottlingData()        | Provides data from a trace about processor maximum frequency throttling.                                                   | Processor Constraints table                                          |
| trace.UseReadyBootData()                  | Provides data from a trace about boot prefetching activity from Ready Boot.                                                | Ready Boot Events table                                              |
| trace.UseReferenceSetData()               | Provides data from a trace about pages of virtual memory used by each process.                                             | Reference Set table                                                  |
| trace.UseRegionsOfInterest()              | Provides named regions of interest intervals from a trace as specified in an xml configuration file.                       | Regions of Interest table                                            |
| trace.UseRegistryData()                   | Provides data about registry activity during a trace.                                                                      | Registry table                                                       |
| trace.UseResidentSetData()                | Provides data from a trace about the pages of virtual memory for each process that were resident in physical memory.       | Resident Set table                                                   |
| trace.UseRundownData()                    | Provides data from a trace about intervals during which trace rundown data collection occurred.                            | Shaded regions in the graph timeline                                 |
| trace.UseScheduledTasks()                 | Provides data about scheduled tasks that ran during a trace.                                                               | Scheduled Tasks table                                                |
| trace.UseServices()                       | Provides data about services that were active or had their state captured during a trace.                                  | Services table; System Configuration, Services                       |
| trace.UseStacks()                         | Provides data about stacks recorded during a trace.                                                                        |                                                                      |
| trace.UseStackEvents()                    | Provides data about events associated with stacks recorded during a trace.                                                 | Stacks table                                                         |
| trace.UseStackTags()                      | Provides a mapper that groups stacks from a trace into stack tags as specified in an XML configuration file.               | Columns such as Stack Tag and Stack (Frame Tags)                     |
| trace.UseSymbols()                        | Provides the ability to load symbols for a trace.                                                                          | Configure Symbol Paths; Load Symbols                                 |
| trace.UseSyscalls()                       | Provides data about syscalls that occurred during a trace.                                                                 | Syscalls table                                                       |
| trace.UseSystemMetadata()                 | Provides general, system-wide metadata from a trace.                                                                       | System Configuration                                                 |
| trace.UseSystemPowerSourceData()          | Provides data from a trace about the active system power source (AC vs DC).                                                | System Power Source table                                            |
| trace.UseSystemSleepData()                | Provides data from a trace about overall system power state.                                                               | Power Transition table                                               |
| trace.UseTargetCpuIdleStates()            | Provides data from a trace about target CPU C-states.                                                                      | CPU Idle States table (when Type is Target)                          |
| trace.UseTargetProcessorFrequencyData()   | Provides data from a trace about target processor frequencies.                                                             | Processor Frequency table (when Type is Target)                      |
| trace.UseThreads()                        | Provides data about threads active during a trace.                                                                         | Thread Lifetimes table                                               |
| trace.UseTraceStatistics()                | Provides statistics about the events in a trace.                                                                           | System Configuration, Trace Statistics                               |
| trace.UseUtcData()                        | Provides data from a trace about Microsoft telemetry activity using Universal Telemetry Client (UTC).                      | Utc table                                                            |
| trace.UseWindowInFocus()                  | Provides data from a trace about changes to the active UI window in focus.                                                 | Window in Focus table                                                |
| trace.UseWindowsTracePreprocessorEvents() | Provides Windows software trace preprocessor (WPP) events from a trace.                                                    | WPP Trace table; Generic Events table (when Event Type is WPP)       |
| trace.UseWinINetData()                    | Provides data from a trace about internet activity via Windows Internet (WinINet).                                         | Download Details table                                               |
| trace.UseWorkingSetData()                 | Provides data from a trace about pages of virtual memory that were in the working set for each process or kernel category. | Virtual Memory Snapshots table                                       |

See also the extension methods on [ITraceSource](/dotnet/api/microsoft.windows.eventtracing.itracesource) for all available trace data, or examine the method available from "trace." shown by IntelliSense.

## Next Steps

In this overview, you learned how to access trace data using TraceProcessor and the built-in data sources that it can access.

The next step is to learn how to [extend](extensibility.md) TraceProcessor to access custom trace data.
