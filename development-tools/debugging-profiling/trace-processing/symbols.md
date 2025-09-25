---
title: Load Symbols - .NET TraceProcessing
description: In this tutorial, learn how load symbols when processing traces.
ms.date: 08/19/2024
ms.topic: tutorial
---

# Use symbols in .NET TraceProcessing

[TraceProcessor](/dotnet/api/microsoft.windows.eventtracing.traceprocessor) supports loading symbols and getting stacks from several data sources. The following console application looks at CPU samples and outputs the estimated duration that a specific function was running (based on the trace's statistical sampling of CPU usage).

```csharp
using Microsoft.Windows.EventTracing;
using Microsoft.Windows.EventTracing.Cpu;
using Microsoft.Windows.EventTracing.Symbols;
using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length != 3)
        {
            Console.Error.WriteLine("Usage: GetCpuSampleDuration.exe <trace.etl> <imageName> <functionName>");
            return;
        }

        string tracePath = args[0];
        string imageName = args[1];
        string functionName = args[2];
        Dictionary<string, Duration> matchDurationByCommandLine = new Dictionary<string, Duration>();

        using (ITraceProcessor trace = TraceProcessor.Create(tracePath))
        {
            IPendingResult<ISymbolDataSource> pendingSymbolData = trace.UseSymbols();
            IPendingResult<ICpuSampleDataSource> pendingCpuSamplingData = trace.UseCpuSamplingData();

            trace.Process();

            ISymbolDataSource symbolData = pendingSymbolData.Result;
            ICpuSampleDataSource cpuSamplingData = pendingCpuSamplingData.Result;

            symbolData.LoadSymbolsForConsoleAsync(SymCachePath.Automatic, SymbolPath.Automatic).GetAwaiter().GetResult();

            Console.WriteLine();
            IThreadStackPattern pattern = AnalyzerThreadStackPattern.Parse($"{imageName}!{functionName}");

            foreach (ICpuSample sample in cpuSamplingData.Samples)
            {
                if (sample.Stack != null && sample.Stack.Matches(pattern))
                {
                    string commandLine = sample.Process.CommandLine;

                    if (!matchDurationByCommandLine.ContainsKey(commandLine))
                    {
                        matchDurationByCommandLine.Add(commandLine, Duration.Zero);
                    }

                    matchDurationByCommandLine[commandLine] += sample.Weight;
                }
            }

            foreach (string commandLine in matchDurationByCommandLine.Keys)
            {
                Console.WriteLine($"{commandLine}: {matchDurationByCommandLine[commandLine]}");
            }
        }
    }
}
```

Running this program produces output similar to the following:

```shell
C:\GetCpuSampleDuration\bin\Debug\> GetCpuSampleDuration.exe C:\boot.etl user32.dll LoadImageInternal
0.0% (0 of 1165; 0 loaded)
<snip>
100.0% (1165 of 1165; 791 loaded)
wininit.exe: 15.99 ms
C:\Windows\Explorer.EXE: 5 ms
winlogon.exe: 20.15 ms
"C:\Users\AdminUAC\AppData\Local\Microsoft\OneDrive\OneDrive.exe" /background: 2.09 ms
```

(Output details will vary depending on the trace).

## Symbols format

Internally, TraceProcessor uses the [SymCache](/windows-hardware/test/wpt/loading-symbols#symcache-path) format, which is a cache of some of the data stored in a PDB. When loading symbols, TraceProcessor requires specifying a location to use for these SymCache files (a SymCache path) and supports optionally specifying a SymbolPath to access PDBs. When a SymbolPath is provided, TraceProcessor will create SymCache files out of PDB files as needed, and subsequent processing of the same data can use the SymCache files directly for better performance.

## Next steps

In this tutorial, you learned how to load symbols when processing traces.

The next step is to learn how to [use streaming](streaming.md) to access trace data without buffering everything in memory.