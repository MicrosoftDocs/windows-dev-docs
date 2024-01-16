---
title: Interprocess communication (IPC)
description: This topic explains various ways to perform interprocess communication (IPC) between Universal Windows Platform (UWP) applications and Win32 applications.
ms.date: 03/23/2020
ms.topic: article
keywords: windows 10, uwp
---
# Interprocess communication (IPC)

This topic explains various ways to perform interprocess communication (IPC) between Universal Windows Platform (UWP) applications and Win32 applications.

## App services

App services enable applications to expose services that accept and return property bags of primitives ([**ValueSet**](/uwp/api/Windows.Foundation.Collections.ValueSet)) in the background. Rich objects can be passed if they're [serialized](https://stackoverflow.com/questions/46367985/how-to-make-a-class-that-can-be-added-to-the-windows-foundation-collections-valu).

App services can run either [out of process](../launch-resume/how-to-create-and-consume-an-app-service.md) as a background task, or [in process](../launch-resume/convert-app-service-in-process.md) within the foreground application.

App services are best used for sharing small amounts of data where near real-time latency isn't required.

## COM

[COM](/windows/win32/com/component-object-model--com--portal) is a distributed object-oriented system for creating binary software components that can interact and communicate. As a developer, you use COM to create reusable software components and automation layers for an application. COM components can be in process or out of process, and they can communicate via a [client and server](/windows/win32/com/com-clients-and-servers) model. Out-of-process COM servers have long been used as a means for [inter-object communication](/windows/win32/com/inter-object-communication).

Packaged applications with the [runFullTrust](../packaging/app-capability-declarations.md#restricted-capabilities) capability can register out-of-process COM servers for IPC via the [package manifest](/uwp/schemas/appxpackage/uapmanifestschema/element-com-extension). This is known as [Packaged COM](https://blogs.windows.com/windowsdeveloper/2017/04/13/com-server-ole-document-support-desktop-bridge/).

## Filesystem

### BroadFileSystemAccess

Packaged applications can perform IPC using the broad filesystem by declaring the [broadFileSystemAccess](../files/file-access-permissions.md#accessing-additional-locations) restricted capability. This capability grants [Windows.Storage](/uwp/api/Windows.Storage) APIs and [xxxFromApp](/previous-versions/windows/desktop/legacy/mt846585(v=vs.85)) Win32 APIs access to the broad filesystem.

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

### SharedAccessStorageManager

[SharedAccessStorageManager](/uwp/api/Windows.ApplicationModel.DataTransfer.SharedStorageAccessManager) is used in conjunction with App services, protocol activations (for example, LaunchUriForResultsAsync), etc., to share StorageFiles via tokens.

## FullTrustProcessLauncher

With the [runFullTrust](../packaging/app-capability-declarations.md#restricted-capabilities) capability, packaged applications can [launch full trust processes](/uwp/api/Windows.ApplicationModel.FullTrustProcessLauncher) within the same package.

For scenarios where package restrictions are a burden, or IPC options are lacking, an application could use a full trust process as a proxy to interface with the system, and then IPC with the full trust process itself via App services or some other well supported IPC mechanism.

## LaunchUriForResultsAsync

[LaunchUriForResultsAsync](../launch-resume/how-to-launch-an-app-for-results.md) is used for simple ([ValueSet](/uwp/api/Windows.Foundation.Collections.ValueSet)) data exchange with other packaged applications that implement the [ProtocolForResults](../launch-resume/how-to-launch-an-app-for-results.md#step-2-override-applicationonactivated-in-the-app-that-youll-launch-for-results) activation contract. Unlike App services, which typically run in the background, the target application is launched in the foreground.

Files can be shared by passing [SharedStorageAccessManager](/uwp/api/Windows.ApplicationModel.DataTransfer.SharedStorageAccessManager) tokens to the application via the ValueSet.

## Loopback

Loopback is the process of communicating with a network server listening on localhost (the loopback address).

To maintain security and network isolation, loopback connections for IPC are blocked by default for packaged applications. You can enable loopback connections among trusted packaged application using [capabilities](/previous-versions/windows/apps/hh770532(v=win.10)) and [manifest properties](/uwp/schemas/appxpackage/uapmanifestschema/element-uap4-loopbackaccessrules).

* All packaged applications participating in loopback connections will need to declare the `privateNetworkClientServer` capability in their [package manifests](/uwp/schemas/appxpackage/uapmanifestschema/element-capability).
* Two packaged applications can communicate via loopback by declaring [LoopbackAccessRules](/uwp/schemas/appxpackage/uapmanifestschema/element-uap4-loopbackaccessrules) within their package manifests.
    * Each application must list the other in its [LoopbackAccessRules](/uwp/schemas/appxpackage/uapmanifestschema/element-uap4-loopbackaccessrules). The client declares an "out" Rule for the server, and the server declares "in" Rules for its supported clients.

> [!NOTE]
> The package family name required to identify an application in these Rules can be found via the package manifest editor in Visual Studio during development time, via [Partner Center](/windows/apps/publish/view-app-identity-details) for applications published through the Microsoft Store, or via the [Get-AppxPackage](/powershell/module/appx/get-appxpackage?windowsserver2019-ps&preserve-view=true) PowerShell command for applications that are already installed.

Unpackaged applications and services don't have package identity, so they can't be declared in [LoopbackAccessRules](/uwp/schemas/appxpackage/uapmanifestschema/element-uap4-loopbackaccessrules). You can configure a packaged application to connect via loopback with unpackaged applications and services via [CheckNetIsolation.exe](/previous-versions/windows/apps/hh780593(v=win.10)), however this is only possible for sideload or debugging scenarios where you have local access to the machine, and you have administrator privileges.

* All packaged applications participating in loopback connections need to declare the `privateNetworkClientServer` capability in their [package manifests](/uwp/schemas/appxpackage/uapmanifestschema/element-capability).
* If a packaged application is connecting to an unpackaged application or service, run `CheckNetIsolation.exe LoopbackExempt -a -n=<PACKAGEFAMILYNAME>` to add a loopback exemption for the packaged application.
* If an unpackaged application or service is connecting to a packaged application, run `CheckNetIsolation.exe LoopbackExempt -is -n=<PACKAGEFAMILYNAME>` to enable the packaged application to receive inbound loopback connections.
    * [CheckNetIsolation.exe](/previous-versions/windows/apps/hh780593(v=win.10)) must be running continuously while the packaged application is listening for connections.
    * The `-is` flag was introduced in Windows 10, version 1607 (10.0; Build 14393).

> [!NOTE]
> The package family name required for the `-n` flag of [CheckNetIsolation.exe](/previous-versions/windows/apps/hh780593(v=win.10)) can be found via the package manifest editor in Visual Studio during development time, via [Partner Center](/windows/apps/publish/view-app-identity-details) for applications published through the Microsoft Store, or via the [Get-AppxPackage](/powershell/module/appx/get-appxpackage?windowsserver2019-ps&preserve-view=true) PowerShell command for applications that are already installed.

[CheckNetIsolation.exe](/previous-versions/windows/apps/hh780593(v=win.10)) is also useful for [debugging network isolation issues](/previous-versions/windows/apps/hh780593(v=win.10)#debug-network-isolation-issues).

## Pipes

[Pipes](/windows/win32/ipc/pipes) enable simple communication between a pipe server and one or more pipe clients.

[Anonymous pipes](/windows/win32/ipc/anonymous-pipes) and [named pipes](/windows/win32/ipc/named-pipes) are supported with the following constraints:

* By default, named pipes in packaged applications are supported only between processes within the same package, unless a process is full trust.
* Named pipes can be shared across packages following the guidelines for [sharing named objects](./sharing-named-objects.md).
* Named pipes must use the syntax `\\.\pipe\LOCAL\` for the pipe name.

## Registry

[Registry](/windows/win32/sysinfo/registry-functions) usage for IPC is generally discouraged, but it is supported for existing code. Packaged applications can access only registry keys that they have permission to access.

Commonly, packaged desktop apps (see [Building an MSIX package from your code](/windows/msix/desktop/source-code-overview)) leverage [registry virtualization](/windows/msix/desktop/desktop-to-uwp-behind-the-scenes#registry) such that global registry writes are contained to a private hive within the MSIX package. This enables source code compatibility while minimizing global registry impact, and can be used for IPC between processes in the same package. If you must use the registry, this model is preferred versus manipulating the global registry.

## RPC

[RPC](/windows/win32/rpc/rpc-start-page) can be used to connect a packaged application to a Win32 RPC endpoint, provided that the packaged application has the correct capabilities to match the ACLs on the RPC endpoint.

Custom capabilities enable OEMs and IHVs to [define arbitrary capabilities](/windows-hardware/drivers/devapps/hardware-support-app--hsa--steps-for-driver-developers#reserving-a-custom-capability), [ACL their RPC endpoints with them](/windows-hardware/drivers/devapps/hardware-support-app--hsa--steps-for-driver-developers#allowing-access-to-an-rpc-endpoint-to-a-uwp-app-using-the-custom-capability), and then [grant those capabilities to authorized client applications](/windows-hardware/drivers/devapps/hardware-support-app--hsa--steps-for-driver-developers#preparing-the-signed-custom-capability-descriptor-sccd-file). For a full sample application, see the [CustomCapability](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/CustomCapability) sample.

RPC endpoints can also be ACLed to specific packaged applications to limit access to the endpoint to just those applications without requiring the management overhead of custom capabilities. You can use the [DeriveAppContainerSidFromAppContainerName](/windows/win32/api/userenv/nf-userenv-deriveappcontainersidfromappcontainername) API to derive a SID from a package family name, and then ACL the RPC endpoint with the SID as shown in the [CustomCapability](https://github.com/Microsoft/Windows-universal-samples/blob/master/Samples/CustomCapability/Service/Server/RpcServer.cpp) sample.

## Shared Memory

[File mapping](/windows/win32/memory/sharing-files-and-memory) can be used to share a file or memory between two or more processes with the following constraints:

* By default, file mappings in packaged applications are supported only between processes within the same package, unless a process is full trust.
* File mappings can be shared across packages following the guidelines for [sharing named objects](./sharing-named-objects.md).

Shared memory is recommended for efficiently sharing and manipulating large amounts of data.
