---
title: Quickstart Process a trace - .NET TraceProcessing
description: In this quickstart, learn how to access data in an ETW trace.
author: maiak
ms.author: maiak
ms.date: 02/20/2020
ms.topic: quickstart
---

# Quickstart: Process your first trace

Try out TraceProcessor to access data in an Event Tracing for Windows (ETW) trace. TraceProcessor allows you to access ETW trace data as .NET objects.

In this quick start you learn how to:

1. Install the TraceProcessing NuGet package.
2. Create a TraceProcessor.
3. Use TraceProcessor to access process command lines contained in the trace.

## Prerequisites

Visual Studio 2019

## Install the TraceProcessing NuGet package

.NET TraceProcessing is available from [NuGet](https://www.nuget.org/packages/Microsoft.Windows.EventTracing.Processing.All) with the following package ID:

Microsoft.Windows.EventTracing.Processing.All

You can use this package in a console app to list the process command lines contained in an ETW trace (.etl file).

1. Create a new .NET Console App. In Visual Studio, select File, New, Project..., and choose the Console App template for C#.

    Enter a Project name, for example, TraceProcessorQuickstart, and choose Create.

2. In Solution Explorer, right-click on Dependencies and choose Manage NuGet Packages... and switch to the Browse tab.

3. In the Search box, enter Microsoft.Windows.EventTracing.Processing.All and search.

    Select Install on the NuGet package with that name, and close the NuGet window.

## Create a TraceProcessor

1. Change Program.cs to the following contents:

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
                // TODO: call trace.Use...

                trace.Process();

                Console.WriteLine("TODO: Access data from the trace");
            }
        }
    }
    ```

2. Provide a trace name to use when running the project.

    In Solution Explorer, right-click on the project and choose Properties. Switch to the Debug tab and enter the path to a trace (.etl file) in Application arguments.

    If you do not already have a trace file, you can use [Windows Performance Recorder](/windows-hardware/test/wpt/start-a-recording) to create one.

3. Run the application.

    Choose Debug, Start Without Debugging to run your code.

## Use TraceProcessor to access process command lines contained in the trace

1. Change Program.cs to the following contents:

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

2. Run the application again.

    This time, you should see a list command lines from all processes that were executing while the trace was being recorded.

## Next Steps

In this quickstart, you created a console application, installed TraceProcessor, and used it to access process command lines from an ETW trace. Now you have an application that accesses trace data.

Process information is just one of many kinds of data stored in an ETW trace that your application can access.

The next step is to [look closer at TraceProcessor](tutorial.md) and the other data sources it can access.
