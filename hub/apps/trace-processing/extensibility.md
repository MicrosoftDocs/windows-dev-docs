---
title: Extensibility - .NET TraceProcessing
description: In this tutorial, learn how to extend .NET TraceProcessing.
ms.date: 08/19/2024
ms.topic: overview
---

# Extend TraceProcessor

Many kinds of trace data have built-in support in [TraceProcessor](/dotnet/api/microsoft.windows.eventtracing.traceprocessor), but if you have your other providers that you would like to analyze (including your own custom providers), that data is also available from the trace live while processing occurs.

> [!NOTE]
> This part of the API is in preview and under active development. It may change in future releases.

For example, here is a simple way to get the list of providers IDs in a trace.

```csharp
// Open a trace with TraceProcessor.Create() and call Run...

static void Run(ITraceProcessor trace)
{
    HashSet<Guid> providerIds = new HashSet<Guid>();
    trace.Use((e) => providerIds.Add(e.ProviderId));
    trace.Process();

    foreach (Guid providerId in providerIds)
    {
        Console.WriteLine(providerId);
    }
}
```

The following example shows a simplified custom data source.

```csharp
// Open a trace with TraceProcessor.Create() and call Run...

static void Run(ITraceProcessor trace)
{
    CustomDataSource customDataSource = new CustomDataSource();
    trace.Use(customDataSource);

    trace.Process();

    Console.WriteLine(customDataSource.Count);
}

class CustomDataSource : IFilteredEventConsumer
{
    public IReadOnlyList<Guid> ProviderIds { get; } = new Guid[] { new Guid("your provider ID") };

    public int Count { get; private set; }

    public void Process(EventContext eventContext)
    {
        ++Count;
    }
}
```

## Next steps

In this tutorial, you learned how to extend TraceProcessor.

The next step is to learn how to [load symbols](symbols.md) for traces.