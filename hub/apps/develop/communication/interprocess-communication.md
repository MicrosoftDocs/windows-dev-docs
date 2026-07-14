---
title: Interprocess communication (IPC)
description: Learn about ways to perform interprocess communication between Windows App SDK desktop apps and other Win32 applications.
author: GrantMeStrength
ms.author: jken
ms.topic: article
ms.date: 07/12/2026
---

# Interprocess communication (IPC)

This topic explains various ways to perform interprocess communication (IPC) between Windows App SDK desktop apps and other Win32 applications. Because Windows App SDK desktop apps run as full-trust Win32 processes, they have direct access to all OS-level IPC mechanisms. Some mechanisms have additional requirements when apps are packaged with MSIX, as noted in the sections below.

## App services

App services enable applications to expose services that accept and return property bags of primitives ([**ValueSet**](/uwp/api/Windows.Foundation.Collections.ValueSet)) in the background. Rich objects can be passed if they're serialized.

App services can run either out of process as a background task, or in process within the foreground application.

> [!NOTE]
> App services require a packaged app with package identity. They're available to Windows App SDK apps that use MSIX packaging.

App services are best used for sharing small amounts of data where near real-time latency isn't required.

## COM

[COM](/windows/win32/com/component-object-model--com--portal) is a distributed object-oriented system for creating binary software components that can interact and communicate. As a developer, you use COM to create reusable software components and automation layers for an application. COM components can be in process or out of process, and they can communicate via a [client and server](/windows/win32/com/com-clients-and-servers) model. Out-of-process COM servers have long been used as a means for [inter-object communication](/windows/win32/com/inter-object-communication).

Packaged applications with the `runFullTrust` capability can register out-of-process COM servers for IPC via the [package manifest](/uwp/schemas/appxpackage/uapmanifestschema/element-com-extension). This is known as [Packaged COM](https://blogs.windows.com/windowsdeveloper/2017/04/13/com-server-ole-document-support-desktop-bridge/).

Windows App SDK desktop apps run as full-trust processes, so they can also register and use COM servers directly via the Windows Registry, just like traditional Win32 applications.

## Filesystem

### BroadFileSystemAccess

Packaged applications can perform IPC using the broad filesystem by declaring the `broadFileSystemAccess` restricted capability. This capability grants [Windows.Storage](/uwp/api/Windows.Storage) APIs and [Win32 *FromApp* APIs](/previous-versions/windows/desktop/legacy/mt846585(v=vs.85)) access to the broad filesystem.

By default, IPC via the filesystem for packaged applications is restricted to the other mechanisms described in this section.

### PublisherCacheFolder

The [PublisherCacheFolder](/uwp/api/windows.storage.applicationdata.getpublishercachefolder) enables packaged applications to declare folders in their manifest that can be shared with other packages by the same publisher.

The shared storage folder has the following requirements and restrictions:

* Data in the shared storage folder is not backed up or roamed.
* The user can clear the contents of the shared storage folder.
* You can't use the shared storage folder to share data among applications from different publishers.
* You can't use the shared storage folder to share data among different users.
* The shared storage folder doesn't have version management.

If you publish multiple applications and you're looking for a simple mechanism to share data between them, then the PublisherCacheFolder is a simple filesystem-based option.

## Pipes

[Pipes](/windows/win32/ipc/pipes) enable simple communication between a pipe server and one or more pipe clients.

[Anonymous pipes](/windows/win32/ipc/anonymous-pipes) and [named pipes](/windows/win32/ipc/named-pipes) are supported with the following constraints:

* By default, named pipes in packaged applications are supported only between processes within the same package, unless a process is full trust.
* Named pipes can be shared across packages following the guidelines for [sharing named objects](./sharing-named-objects.md).
* Named pipes in packaged apps should include `LOCAL\` in the pipe name (for example, `\\.\pipe\LOCAL\<pipename>`). The `LOCAL\` segment scopes the pipe to the caller's login session and is required for MSIX-packaged apps. When using .NET APIs like `NamedPipeServerStream`, pass only the `LOCAL\<pipename>` portion — the `\\.\pipe\` prefix is handled internally.

Windows App SDK desktop apps run as full-trust processes, so they can create and use named pipes without the same-package restriction. However, if you're communicating with another packaged app that doesn't have full trust, the constraints above still apply.

### Named pipe example (C#)

The following example demonstrates a complete named pipe server and client as two console applications. Because Windows App SDK desktop apps are full-trust processes, named pipe IPC works without any special capabilities or manifest entries.

To try this example, create two console app projects (targeting `net8.0-windows` with `ImplicitUsings` enabled) and run the server first, then the client in a separate terminal.

**Pipe server** — creates a named pipe and echoes messages back to the client:

```csharp
using System.Text;

string PipeName = @"LOCAL\WinAppSdkIpcDemo";

Console.WriteLine("Named Pipe Server");
Console.WriteLine($"  Process ID: {Environment.ProcessId}");
Console.WriteLine($"  User:       {Environment.UserName}");
Console.WriteLine($"  Pipe:       \\\\.\\pipe\\{PipeName}");
Console.WriteLine();

using var server = new System.IO.Pipes.NamedPipeServerStream(
    PipeName,
    System.IO.Pipes.PipeDirection.InOut,
    maxNumberOfServerInstances: 1,
    System.IO.Pipes.PipeTransmissionMode.Message,
    System.IO.Pipes.PipeOptions.Asynchronous);

Console.WriteLine("Waiting for client...");
await server.WaitForConnectionAsync();
Console.WriteLine("Client connected!");

byte[] buffer = new byte[4096];

while (server.IsConnected)
{
    try
    {
        int bytesRead = await server.ReadAsync(buffer);
        if (bytesRead == 0) break;

        string received = Encoding.UTF8.GetString(buffer, 0, bytesRead);
        Console.WriteLine($"  Received: \"{received}\"");

        if (received.Equals("QUIT", StringComparison.OrdinalIgnoreCase))
            break;

        // Echo the message back with the server's process ID
        string reply = $"Echo from PID {Environment.ProcessId}: {received}";
        await server.WriteAsync(Encoding.UTF8.GetBytes(reply));
        await server.FlushAsync();
    }
    catch (IOException)
    {
        break;
    }
}

Console.WriteLine("Done.");
```

**Pipe client** — connects to the server and sends user input:

```csharp
using System.Text;

string PipeName = @"LOCAL\WinAppSdkIpcDemo";

Console.WriteLine("Named Pipe Client");
Console.WriteLine($"  Process ID: {Environment.ProcessId}");
Console.WriteLine();

using var client = new System.IO.Pipes.NamedPipeClientStream(
    serverName: ".",
    pipeName: PipeName,
    System.IO.Pipes.PipeDirection.InOut,
    System.IO.Pipes.PipeOptions.Asynchronous);

Console.WriteLine("Connecting...");
await client.ConnectAsync(timeout: 5000);
client.ReadMode = System.IO.Pipes.PipeTransmissionMode.Message;
Console.WriteLine("Connected! Type messages (or QUIT to exit):");

byte[] buffer = new byte[4096];

while (true)
{
    Console.Write("> ");
    string? input = Console.ReadLine();
    if (string.IsNullOrEmpty(input)) continue;

    await client.WriteAsync(Encoding.UTF8.GetBytes(input));
    await client.FlushAsync();

    if (input.Equals("QUIT", StringComparison.OrdinalIgnoreCase))
        break;

    int bytesRead = await client.ReadAsync(buffer);
    string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
    Console.WriteLine($"  <- {response}");
}

Console.WriteLine("Done.");
```

When you run both applications, the client sends messages that the server echoes back, confirming cross-process communication between two full-trust desktop processes with no special configuration required.

## Registry

[Registry](/windows/win32/sysinfo/registry-functions) usage for IPC is generally discouraged, but it is supported for existing code. Packaged applications can access only registry keys that they have permission to access.

Packaged desktop apps (see [Building an MSIX package from your code](/windows/msix/desktop/source-code-overview)) leverage [registry virtualization](/windows/msix/desktop/desktop-to-uwp-behind-the-scenes#registry) such that global registry writes are contained to a private hive within the MSIX package. This enables source code compatibility while minimizing global registry impact, and can be used for IPC between processes in the same package. If you must use the registry, this model is preferred versus manipulating the global registry.

## RPC

[RPC](/windows/win32/rpc/rpc-start-page) can be used to connect a packaged application to a Win32 RPC endpoint, provided that the packaged application has the correct capabilities to match the ACLs on the RPC endpoint.

Custom capabilities enable OEMs and IHVs to [define arbitrary capabilities](/windows-hardware/drivers/devapps/hardware-support-app--hsa--steps-for-driver-developers#reserving-a-custom-capability), [ACL their RPC endpoints with them](/windows-hardware/drivers/devapps/hardware-support-app--hsa--steps-for-driver-developers#allowing-access-to-an-rpc-endpoint-to-a-uwp-app-using-the-custom-capability), and then [grant those capabilities to authorized client applications](/windows-hardware/drivers/devapps/hardware-support-app--hsa--steps-for-driver-developers#preparing-the-signed-custom-capability-descriptor-sccd-file). For a full sample application, see the [CustomCapability](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/CustomCapability) sample.

RPC endpoints can also be ACLed to specific packaged applications to limit access to the endpoint to just those applications without requiring the management overhead of custom capabilities. You can use the [DeriveAppContainerSidFromAppContainerName](/windows/win32/api/userenv/nf-userenv-deriveappcontainersidfromappcontainername) API to derive a SID from a package family name, and then ACL the RPC endpoint with the SID as shown in the [CustomCapability](https://github.com/Microsoft/Windows-universal-samples/blob/master/Samples/CustomCapability/Service/Server/RpcServer.cpp) sample.

## Shared memory

[File mapping](/windows/win32/memory/sharing-files-and-memory) can be used to share a file or memory between two or more processes with the following constraints:

* By default, file mappings in packaged applications are supported only between processes within the same package, unless a process is full trust.
* File mappings can be shared across packages following the guidelines for [sharing named objects](./sharing-named-objects.md).

Windows App SDK desktop apps run as full-trust processes, so they can create and use shared memory file mappings without restriction. When communicating with another packaged app that doesn't have full trust, use the ACL approach described in [sharing named objects](./sharing-named-objects.md).

Shared memory is recommended for efficiently sharing and manipulating large amounts of data.

## Loopback

Loopback is the process of communicating with a network server listening on localhost (the loopback address).

To maintain security and network isolation, loopback connections for IPC are blocked by default for packaged applications. You can enable loopback connections among trusted packaged applications using capabilities and manifest properties.

* All packaged applications participating in loopback connections need to declare the `privateNetworkClientServer` capability in their [package manifests](/uwp/schemas/appxpackage/uapmanifestschema/element-capability).
* Two packaged applications can communicate via loopback by declaring [LoopbackAccessRules](/uwp/schemas/appxpackage/uapmanifestschema/element-uap4-loopbackaccessrules) within their package manifests.
    * Each application must list the other in its LoopbackAccessRules. The client declares an "out" Rule for the server, and the server declares "in" Rules for its supported clients.

> [!NOTE]
> The package family name required to identify an application in these Rules can be found via the package manifest editor in Visual Studio during development time, via [Partner Center](/windows/apps/publish/view-app-identity-details) for applications published through the Microsoft Store, or via the [Get-AppxPackage](/powershell/module/appx/get-appxpackage?windowsserver2019-ps&preserve-view=true) PowerShell command for applications that are already installed.

Unpackaged applications and services don't have package identity, so they can't be declared in LoopbackAccessRules. You can configure a packaged application to connect via loopback with unpackaged applications and services via [CheckNetIsolation.exe](/previous-versions/windows/apps/hh780593(v=win.10)), however this is only possible for sideload or debugging scenarios where you have local access to the machine, and you have administrator privileges.

* If a packaged application is connecting to an unpackaged application or service, run `CheckNetIsolation.exe LoopbackExempt -a -n=<PACKAGEFAMILYNAME>` to add a loopback exemption for the packaged application.
* If an unpackaged application or service is connecting to a packaged application, run `CheckNetIsolation.exe LoopbackExempt -is -n=<PACKAGEFAMILYNAME>` to enable the packaged application to receive inbound loopback connections.
    * [CheckNetIsolation.exe](/previous-versions/windows/apps/hh780593(v=win.10)) must be running continuously while the packaged application is listening for connections.

> [!NOTE]
> The package family name required for the `-n` flag of [CheckNetIsolation.exe](/previous-versions/windows/apps/hh780593(v=win.10)) can be found via the package manifest editor in Visual Studio during development time, via [Partner Center](/windows/apps/publish/view-app-identity-details) for applications published through the Microsoft Store, or via the [Get-AppxPackage](/powershell/module/appx/get-appxpackage?windowsserver2019-ps&preserve-view=true) PowerShell command for applications that are already installed.

## Choosing an IPC mechanism

The following table summarizes the IPC mechanisms and their best use cases:

| Mechanism | Best for | Requirements |
|-----------|----------|--------------|
| App services | Small data exchange with property bags | Packaged app with package identity |
| COM | Reusable components, automation layers | None (Win32) or package manifest (Packaged COM) |
| Named pipes | Stream-based bidirectional communication | None for full-trust apps |
| Shared memory | Large data, high performance | None for full-trust apps |
| RPC | Distributed client/server programs | ACLs must permit access |
| Registry | Legacy code compatibility | Discouraged for new code |
| Loopback | Network-based protocols (TCP/UDP) | `privateNetworkClientServer` capability for packaged apps |

## Related content

* [Sharing named objects](./sharing-named-objects.md)
* [Windows App SDK overview](../../windows-app-sdk/index.md)
* [MSIX packaging overview](/windows/msix/overview)
