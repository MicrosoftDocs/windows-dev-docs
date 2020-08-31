---
title: Brokered Windows Runtime components for a side-loaded UWP app
description: This paper discusses an enterprise-targeted feature supported by Windows 10, which allows touch-friendly .NET apps to use the existing code responsible for key business-critical operations.
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.assetid: 81b3930c-6af9-406d-9d1e-8ee6a13ec38a
ms.localizationpriority: medium
---
# Brokered Windows Runtime components for a side-loaded UWP app

This article discusses an enterprise-targeted feature supported by
Windows 10, which allows touch-friendly .NET apps to use the existing
code responsible for key business-critical operations.

## Introduction

>**Note**  The sample code that accompanies this paper may be downloaded for [Visual Studio 2015 & 2017](https://github.com/Microsoft/Brokered-WinRT-Components). The Microsoft Visual Studio template to build Brokered Windows Runtime
Components can be downloaded here: [Visual Studio 2015 template targeting Universal Windows Apps for Windows
10](https://marketplace.visualstudio.com/items?itemName=vs-publisher-713547.VS2015TemplateBrokeredComponents)

Windows includes a new feature called *Brokered Windows Runtime
Components for side-loaded applications*. We use the term IPC
(inter-process communication) to describe the ability to run existing
desktop software assets in one process (desktop component) while
interacting with this code in a UWP App. This is a familiar model to
enterprise developers as database applications and applications
utilizing NT Services in Windows share a similar multi-process
architecture.

Side-loading of the app is a critical component of this feature.
Enterprise-specific applications have no place in the general consumer
Microsoft Store and corporations have very specific requirements around
security, privacy, distribution, setup, and servicing. As such, the
side-loading model is both a requirement of those who would use this
feature and a critical implementation detail.

Data-centric applications are a key target for this application
architecture. It is envisioned that existing business rules ensconced,
for example, in SQL Server, will be a common part of the desktop
component. This is certainly not the only type of functionality that can
be proffered by the desktop component, but a large part of the demand
for this feature is related to existing data and business logic.

Lastly, given the overwhelming penetration of the .NET runtime and the
C\# language in enterprise development, this feature was developed with
an emphasis on using .NET for both the UWP app and the desktop
component sides. While there are other languages and runtimes possible
for the UWP app, the accompanying sample only illustrates C\#,
and is restricted to the .NET runtime exclusively.

## Application components

>**Note**  This feature is exclusively for the use of .NET. Both client app and the
desktop component must be authored using .NET.

**Application model**

This feature is built around the general application architecture known
as MVVM (Model View View-Model). As such, it is assumed that the "model"
is housed entirely in the desktop component. Therefore, it should be
immediately obvious that the desktop component will be "headless" (i.e.
contains no UI). The view will be entirely contained in the side-loaded
enterprise application. While there is no requirement that this
application be built with the "view-model" construct, we anticipate that
usage of this pattern will be common.

**Desktop component**

The desktop component in this feature is a new application type being
introduced as part of this feature. This desktop component can only be
written in C\# and must target .NET 4.6 or greater for Windows 10. The
project type is a hybrid between the CLR targeting UWP as the
inter-process communication format comprises UWP types and classes,
while the desktop component is allowed to call all parts of the .NET
runtime class library. The impact on the Visual Studio project will be
described in detail later. This hybrid configuration allows marshaling
UWP types between the application built on the desktop components while
allowing desktop CLR code to be called inside the desktop component
implementation.

**Contract**

The contract between the side-loaded application and the desktop
component is described in terms of the UWP type system. This involves
declaring one or more C\# classes that can represent a UWP. See MSDN
topic [Creating Windows Runtime components in C\# and Visual
Basic](/previous-versions/windows/apps/br230301(v=vs.140)) for
specific requirement of creating Windows Runtime Class using C\#.

>**Note**  Enums are not supported in the Windows Runtime components contract
between desktop component and side-loaded application at this time.

**Side-loaded application**

The side-loaded application is a normal UWP app in every respect except
one: it is side-loaded instead of installed via the Microsoft Store. Most
of the installation mechanisms are identical: the manifest and
application packaging are similar (one addition to the manifest is
described in detail later). Once side-loading is enabled, a simple
PowerShell script can install the necessary certificates and the
application itself. It is the normal best practice that the side-loaded
application passes the WACK certification test that is included in the
Project / Store menu in Visual Studio

>**Note** Side-loading can be turned on in Settings-&gt; Update & security -&gt;
For developers.

One important point to note is that the App Broker mechanism shipped as
part of Windows 10 is 32-bit only. The desktop component must be 32-bit.
Side-loaded applications can be 64-bit (provided there is both a 64-bit
and 32-bit proxies registered), but this will be atypical. Building the
side-loaded application in C\# using the normal "neutral" configuration
and the "prefer 32-bit" default naturally creates 32-bit side-loaded
applications.

**Server instancing and AppDomains**

Each sided-loaded application receives its own instance of an App Broker
server (so-called "multi-instancing"). The server code runs inside of a
single AppDomain. This allows for having multiple version of libraries
run in separate instances. For example, application A needs V1.1 of a
component and application B needs V2. These are cleanly separated by
having V1.1 and V2 components in separate server directories and
pointing the application to whichever server supports the correct
version desired.

Server code implementation can be shared amongst multiple App Broker
server instance by pointing multiple applications to the same server
directory. There will still be multiple instances of the App Broker
server but they will be running identical code. All implementation
components used in a single application should be present in the same
path.

## Defining the contract

The first step in creating an application using this feature is to
create the contract between the side-loaded application and the desktop
component. This must be done exclusively using Windows Runtime types.
Fortunately, these are easy to declare using C\# classes. There are
important performance considerations, however, when defining these
conversations, which is covered in a later section.

The sequence to define the contract is introduced as following:

**Step 1:** Create a new class library in Visual Studio. Make sure to create the project using the **Class Library** template, and not the **Windows Runtime Component** template.

An implementation obviously follows, but this section is only covering
the definition of the inter-process contract. The accompanying sample
includes the following class (EnterpriseServer.cs), the beginning shape
of which looks like:

```csharp
namespace Fabrikam
{
    public sealed class EnterpriseServer
    {

        public ILis<String> TestMethod(String input)
        {
            throw new NotImplementedException();
        }
        
        public IAsyncOperation<int> FindElementAsync(int input)
        {
            throw new NotImplementedException();
        }
        
        public string[] RetrieveData()
        {
            throw new NotImplementedException();
        }
        
        public event EventHandler<string> PeriodicEvent;
    }
}
```

This defines a class "EnterpriseServer" that can be instantiated from the
side-loaded application. This class provides the functionality promised in the
RuntimeClass. The RuntimeClass can be used to generate the reference
winmd that will be included in the side-loaded application.

**Step 2:** Edit the project file manually to change the output type of project to **Windows Runtime Component**.

To do this in Visual Studio, right click on the newly created project
and select “Unload Project”, then right click again and select “Edit
EnterpriseServer.csproj” to open the project file, an XML file, for
editing.

In the opened file, search for the \<OutputType\> tag and change its
value to “winmdobj”.

**Step 3:** Create a build rule that creates a "reference" Windows metadata file (.winmd file). i.e. has no implementation.

**Step 4:** Create a build rule that creates an "implementation" Windows metadata file, i.e. has the same metadata information, but also includes the implementation.

This will is done by the following scripts. Add the scripts to the
Post-build event command line, in project **Properties** > **Build Events**.

> **Note** the script is different based on the version of Windows you
are targeting (Windows 10) and the version of Visual Studio in use.

**Visual Studio 2015**
```cmd
    call "$(DevEnvDir)..\..\vc\vcvarsall.bat" x86 10.0.14393.0

    md "$(TargetDir)"\impl    md "$(TargetDir)"\reference

    erase "$(TargetDir)\impl\*.winmd"
    erase "$(TargetDir)\impl\*.pdb"
    rem erase "$(TargetDir)\reference\*.winmd"

    xcopy /y "$(TargetPath)" "$(TargetDir)impl"
    xcopy /y "$(TargetDir)*.pdb" "$(TargetDir)impl"

    winmdidl /nosystemdeclares /metadata_dir:C:\Windows\System32\Winmetadata "$(TargetPath)"

    midl /metadata_dir "%WindowsSdkDir%UnionMetadata" /iid "$(SolutionDir)BrokeredProxyStub\$(TargetName)_i.c" /env win32 /x86 /h   "$(SolutionDir)BrokeredProxyStub\$(TargetName).h" /winmd "$(TargetName).winmd" /W1 /char signed /nologo /winrt /dlldata "$(SolutionDir)BrokeredProxyStub\dlldata.c" /proxy "$(SolutionDir)BrokeredProxyStub\$(TargetName)_p.c"  "$(TargetName).idl"
    mdmerge -n 1 -i "$(ProjectDir)bin\$(ConfigurationName)" -o "$(TargetDir)reference" -metadata_dir "%WindowsSdkDir%UnionMetadata" -partial

    rem erase "$(TargetPath)"

```


**Visual Studio 2017**
```cmd
    call "$(DevEnvDir)..\..\vc\auxiliary\build\vcvarsall.bat" x86 10.0.16299.0

    md "$(TargetDir)"\impl
    md "$(TargetDir)"\reference

    erase "$(TargetDir)\impl\*.winmd"
    erase "$(TargetDir)\impl\*.pdb"
    rem erase "$(TargetDir)\reference\*.winmd"

    xcopy /y "$(TargetPath)" "$(TargetDir)impl"
    xcopy /y "$(TargetDir)*.pdb" "$(TargetDir)impl"

    winmdidl /nosystemdeclares /metadata_dir:C:\Windows\System32\Winmetadata "$(TargetPath)"

    midl /metadata_dir "%WindowsSdkDir%UnionMetadata" /iid "$(SolutionDir)BrokeredProxyStub\$(TargetName)_i.c" /env win32 /x86 /h "$(SolutionDir)BrokeredProxyStub\$(TargetName).h" /winmd "$(TargetName).winmd" /W1 /char signed /nologo /winrt /dlldata "$(SolutionDir)BrokeredProxyStub\dlldata.c" /proxy "$(SolutionDir)BrokeredProxyStub\$(TargetName)_p.c"  "$(TargetName).idl"
    mdmerge -n 1 -i "$(ProjectDir)bin\$(ConfigurationName)" -o "$(TargetDir)reference" -metadata_dir "%WindowsSdkDir%UnionMetadata" -partial

    rem erase "$(TargetPath)"
```

Once the reference **winmd** is created (in folder “reference” under the
project’s Target folder), it is hand carried (copied) to each consuming
side-loaded application project and referenced. This will be described
further in the next section. The project structure embodied in the build
rules above ensure that the implementation and the
reference **winmd** are in clearly segregated directories in the build
hierarchy to avoid confusion.

## Side-loaded applications in detail
As stated previously, the side-loaded application is built like any
other UWP app, but there is one additional detail: declaring the
availability of the RuntimeClass (es) in the side-loaded application's
manifest. This allows the application to simply write new to access the
functionality in the desktop component. A new manifest entry in the <Extension> section describes the RuntimeClass implemented in the
desktop component and information on where it is located. These
declaration content in application’s manifest is the same for apps
targeting Windows 10. For example:

```XML
<Extension Category="windows.activatableClass.inProcessServer">
    <InProcessServer>
        <Path>clrhost.dll</Path>
        <ActivatableClass ActivatableClassId="Fabrikam.EnterpriseServer" ThreadingModel="both">
            <ActivatableClassAttribute Name="DesktopApplicationPath" Type="string" Value="c:\test" />
        </ActivatableClass>
    </InProcessServer>
</Extension>
```

The category is inProcessServer because there are several entries in the
outOfProcessServer category that are not applicable to this application
configuration. Note that the <Path> component must always contain
clrhost.dll (however this is **not** enforced and specifying a different
value will fail in undefined ways).

The <ActivatableClass> section is the same as a true in-process
RuntimeClass preferred by a Windows Runtime component in the app's
package. <ActivatableClassAttribute> is a new element, and the
attributes Name="DesktopApplicationPath" and Type="string" are mandatory
and invariant. The Value attribute points to the location where the
desktop component's implementation winmd resides (more detail on this in
the next section). Each RuntimeClass preferred by the desktop component
should have its own <ActivatableClass> element tree. The
ActivatableClassId must match the fully namespace-qualified name of the
RuntimeClass.

As mentioned in the section "Defining the contract", a project reference
must be made to the desktop component's reference winmd. The Visual
Studio project system normally creates a two level directory structure
with the same name. In the sample it is
EnterpriseIPCApplication\\EnterpriseIPCApplication. The reference
**winmd** is manually copied to this second level directory and then the
Project References dialog is used (click the **Browse..** button) to
locate and reference this **winmd**. After this, the top level namespace
of the desktop component (for example, Fabrikam) should appear as a top level
node in the References part of the project.

>**Note** It is very important to use the **reference winmd** in the side-loaded
application. If you accidentally carry over the **implementation
winmd** to the side-loaded app directory and reference it, you will
likely receive an error related to "cannot find IStringable". This is
one sure sign that the wrong **winmd** has been referenced. The
post-build rules in the IPC server app (detailed in the next section)
carefully segregate these two **winmd** into separate directories.

Environment variables (especially %ProgramFiles%) can be used in <ActivatableClassAttribute Value="path"> .As noted earlier, the App Broker only supports 32-bit so %ProgramFiles% will resolve to
C:\\Program Files (x86) if the application is run on a 64-bit OS.

## Desktop IPC server detail

The previous two sections describe declaration of the class and the
mechanics of transporting the reference **winmd** to the side-loaded
application project. The bulk of the remaining work in the desktop
component involves implementation. Since the whole point of the desktop
component is to be able to call desktop code (usually to re-utilize
existing code assets), the project must be configured in a special way.
Normally, a Visual Studio project using .NET uses one of two "profiles".
One is for desktop (".NetFramework") and one is targeting the UWP app
portion of the CLR (".NetCore"). A desktop component in this feature is
a hybrid between these two. As a result, the references section is very
carefully constructed to blend these two profiles.

A normal UWP app project contains no explicit project references because
the entirety of the Windows Runtime API surface is implicitly included.
Normally only other inter-project references are made. However, a
desktop component project has a very special set of references. It
starts life as a "Classic Desktop\\Class Library" project and therefore
is a desktop project. So explicit references to the Windows Runtime API
(via references to **winmd** files) must be made. Add proper references
as shown below.

```XML
<ItemGroup>
    <!-- These reference are added by VS automatically when you create a Class Library project-->
    <Reference Include="System" />
    <Reference Include="System.Core" />
    <Reference Include="System.Xml.Linq" />
    <Reference Include="System.Data.DataSetExtensions" />
    <Reference Include="Microsoft.CSharp" />
    <Reference Include="System.Data" />
    <Reference Include="System.Net.Http" />
<Reference Include="System.Xml" />
    <!-- These reference should be added manually by editing .csproj file-->

    <Reference Include="System.Runtime.WindowsRuntime, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089, processorArchitecture=MSIL">
      <HintPath>$(MSBuildProgramFiles32)\Microsoft SDKs\NETCoreSDK\System.Runtime.WindowsRuntime\4.0.10\lib\netcore50\System.Runtime.WindowsRuntime.dll</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include="Windows">
      <HintPath>$(MSBuildProgramFiles32)\Windows Kits\10\UnionMetadata\Facade\Windows.WinMD</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include="Windows.Foundation.FoundationContract">
      <HintPath>$(MSBuildProgramFiles32)\Windows Kits\10\References\Windows.Foundation.FoundationContract\1.0.0.0\Windows.Foundation.FoundationContract.winmd</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include="Windows.Foundation.UniversalApiContract">
      <HintPath>$(MSBuildProgramFiles32)\Windows Kits\10\References\Windows.Foundation.UniversalApiContract\1.0.0.0\Windows.Foundation.UniversalApiContract.winmd</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include="Windows.Networking.Connectivity.WwanContract">
      <HintPath>$(MSBuildProgramFiles32)\Windows Kits\10\References\Windows.Networking.Connectivity.WwanContract\1.0.0.0\Windows.Networking.Connectivity.WwanContract.winmd</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include="Windows.ApplicationModel.Activation.ActivatedEventsContract">
      <HintPath>$(MsBuildProgramFiles32)\Windows Kits\10\References\Windows.ApplicationModel.Activation.ActivatedEventsContract\1.0.0.0\Windows.ApplicationModel.Activation.ActivatedEventsContract.winmd</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include="Windows.ApplicationModel.Activation.ActivationCameraSettingsContract">
      <HintPath>$(MsBuildProgramFiles32)\Windows Kits\10\References\Windows.ApplicationModel.Activation.ActivationCameraSettingsContract\1.0.0.0\Windows.ApplicationModel.Activation.ActivationCameraSettingsContract.winmd</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include="Windows.ApplicationModel.Activation.ContactActivatedEventsContract">
      <HintPath>$(MsBuildProgramFiles32)\Windows Kits\10\References\Windows.ApplicationModel.Activation.ContactActivatedEventsContract\1.0.0.0\Windows.ApplicationModel.Activation.ContactActivatedEventsContract.winmd</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include="Windows.ApplicationModel.Activation.WebUISearchActivatedEventsContract">
      <HintPath>$(MsBuildProgramFiles32)\Windows Kits\10\References\Windows.ApplicationModel.Activation.WebUISearchActivatedEventsContract\1.0.0.0\Windows.ApplicationModel.Activation.WebUISearchActivatedEventsContract.winmd</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include="Windows.ApplicationModel.Background.BackgroundAlarmApplicationContract">
      <HintPath>$(MsBuildProgramFiles32)\Windows Kits\10\References\Windows.ApplicationModel.Background.BackgroundAlarmApplicationContract\1.0.0.0\Windows.ApplicationModel.Background.BackgroundAlarmApplicationContract.winmd</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include="Windows.ApplicationModel.Calls.LockScreenCallContract">
      <HintPath>$(MsBuildProgramFiles32)\Windows Kits\10\References\Windows.ApplicationModel.Calls.LockScreenCallContract\1.0.0.0\Windows.ApplicationModel.Calls.LockScreenCallContract.winmd</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include="Windows.ApplicationModel.Resources.Management.ResourceIndexerContract">
      <HintPath>$(MsBuildProgramFiles32)\Windows Kits\10\References\Windows.ApplicationModel.Resources.Management.ResourceIndexerContract\1.0.0.0\Windows.ApplicationModel.Resources.Management.ResourceIndexerContract.winmd</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include="Windows.ApplicationModel.Search.Core.SearchCoreContract">
      <HintPath>$(MsBuildProgramFiles32)\Windows Kits\10\References\Windows.ApplicationModel.Search.Core.SearchCoreContract\1.0.0.0\Windows.ApplicationModel.Search.Core.SearchCoreContract.winmd</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include="Windows.ApplicationModel.Search.SearchContract">
      <HintPath>$(MsBuildProgramFiles32)\Windows Kits\10\References\Windows.ApplicationModel.Search.SearchContract\1.0.0.0\Windows.ApplicationModel.Search.SearchContract.winmd</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include="Windows.ApplicationModel.Wallet.WalletContract">
      <HintPath>$(MsBuildProgramFiles32)\Windows Kits\10\References\Windows.ApplicationModel.Wallet.WalletContract\1.0.0.0\Windows.ApplicationModel.Wallet.WalletContract.winmd</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include="Windows.Devices.Custom.CustomDeviceContract">
      <HintPath>$(MsBuildProgramFiles32)\Windows Kits\10\References\Windows.Devices.Custom.CustomDeviceContract\1.0.0.0\Windows.Devices.Custom.CustomDeviceContract.winmd</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include="Windows.Devices.Portable.PortableDeviceContract">
      <HintPath>$(MsBuildProgramFiles32)\Windows Kits\10\References\Windows.Devices.Portable.PortableDeviceContract\1.0.0.0\Windows.Devices.Portable.PortableDeviceContract.winmd</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include="Windows.Devices.Printers.Extensions.ExtensionsContract">
      <HintPath>$(MsBuildProgramFiles32)\Windows Kits\10\References\Windows.Devices.Printers.Extensions.ExtensionsContract\1.0.0.0\Windows.Devices.Printers.Extensions.ExtensionsContract.winmd</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include="Windows.Devices.Printers.PrintersContract">
      <HintPath>$(MsBuildProgramFiles32)\Windows Kits\10\References\Windows.Devices.Printers.PrintersContract\1.0.0.0\Windows.Devices.Printers.PrintersContract.winmd</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include="Windows.Devices.Scanners.ScannerDeviceContract">
      <HintPath>$(MsBuildProgramFiles32)\Windows Kits\10\References\Windows.Devices.Scanners.ScannerDeviceContract\1.0.0.0\Windows.Devices.Scanners.ScannerDeviceContract.winmd</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include="Windows.Devices.Sms.LegacySmsApiContract">
      <HintPath>$(MsBuildProgramFiles32)\Windows Kits\10\References\Windows.Devices.Sms.LegacySmsApiContract\1.0.0.0\Windows.Devices.Sms.LegacySmsApiContract.winmd</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include="Windows.Gaming.Preview.GamesEnumerationContract">
      <HintPath>$(MsBuildProgramFiles32)\Windows Kits\10\References\Windows.Gaming.Preview.GamesEnumerationContract\1.0.0.0\Windows.Gaming.Preview.GamesEnumerationContract.winmd</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include="Windows.Globalization.GlobalizationJapanesePhoneticAnalyzerContract">
      <HintPath>$(MsBuildProgramFiles32)\Windows Kits\10\References\Windows.Globalization.GlobalizationJapanesePhoneticAnalyzerContract\1.0.0.0\Windows.Globalization.GlobalizationJapanesePhoneticAnalyzerContract.winmd</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include="Windows.Graphics.Printing3D.Printing3DContract">
      <HintPath>$(MsBuildProgramFiles32)\Windows Kits\10\References\Windows.Graphics.Printing3D.Printing3DContract\1.0.0.0\Windows.Graphics.Printing3D.Printing3DContract.winmd</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include="Windows.Management.Deployment.Preview.DeploymentPreviewContract">
      <HintPath>$(MsBuildProgramFiles32)\Windows Kits\10\References\Windows.Management.Deployment.Preview.DeploymentPreviewContract\1.0.0.0\Windows.Management.Deployment.Preview.DeploymentPreviewContract.winmd</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include="Windows.Management.Workplace.WorkplaceSettingsContract">
      <HintPath>$(MsBuildProgramFiles32)\Windows Kits\10\References\Windows.Management.Workplace.WorkplaceSettingsContract\1.0.0.0\Windows.Management.Workplace.WorkplaceSettingsContract.winmd</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include="Windows.Media.Capture.AppCaptureContract">
      <HintPath>$(MsBuildProgramFiles32)\Windows Kits\10\References\Windows.Media.Capture.AppCaptureContract\1.0.0.0\Windows.Media.Capture.AppCaptureContract.winmd</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include="Windows.Media.Capture.CameraCaptureUIContract">
      <HintPath>$(MsBuildProgramFiles32)\Windows Kits\10\References\Windows.Media.Capture.CameraCaptureUIContract\1.0.0.0\Windows.Media.Capture.CameraCaptureUIContract.winmd</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include="Windows.Media.Devices.CallControlContract">
      <HintPath>$(MsBuildProgramFiles32)\Windows Kits\10\References\Windows.Media.Devices.CallControlContract\1.0.0.0\Windows.Media.Devices.CallControlContract.winmd</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include="Windows.Media.MediaControlContract">
      <HintPath>$(MsBuildProgramFiles32)\Windows Kits\10\References\Windows.Media.MediaControlContract\1.0.0.0\Windows.Media.MediaControlContract.winmd</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include="Windows.Media.Playlists.PlaylistsContract">
      <HintPath>$(MsBuildProgramFiles32)\Windows Kits\10\References\Windows.Media.Playlists.PlaylistsContract\1.0.0.0\Windows.Media.Playlists.PlaylistsContract.winmd</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include="Windows.Media.Protection.ProtectionRenewalContract">
      <HintPath>$(MsBuildProgramFiles32)\Windows Kits\10\References\Windows.Media.Protection.ProtectionRenewalContract\1.0.0.0\Windows.Media.Protection.ProtectionRenewalContract.winmd</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include="Windows.Networking.NetworkOperators.LegacyNetworkOperatorsContract">
      <HintPath>$(MsBuildProgramFiles32)\Windows Kits\10\References\Windows.Networking.NetworkOperators.LegacyNetworkOperatorsContract\1.0.0.0\Windows.Networking.NetworkOperators.LegacyNetworkOperatorsContract.winmd</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include="Windows.Networking.Sockets.ControlChannelTriggerContract">
      <HintPath>$(MsBuildProgramFiles32)\Windows Kits\10\References\Windows.Networking.Sockets.ControlChannelTriggerContract\1.0.0.0\Windows.Networking.Sockets.ControlChannelTriggerContract.winmd</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include="Windows.Security.EnterpriseData.EnterpriseDataContract">
      <HintPath>$(MsBuildProgramFiles32)\Windows Kits\10\References\Windows.Security.EnterpriseData.EnterpriseDataContract\1.0.0.0\Windows.Security.EnterpriseData.EnterpriseDataContract.winmd</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include="Windows.Security.ExchangeActiveSyncProvisioning.EasContract">
      <HintPath>$(MsBuildProgramFiles32)\Windows Kits\10\References\Windows.Security.ExchangeActiveSyncProvisioning.EasContract\1.0.0.0\Windows.Security.ExchangeActiveSyncProvisioning.EasContract.winmd</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include="Windows.Services.Maps.GuidanceContract">
      <HintPath>$(MsBuildProgramFiles32)\Windows Kits\10\References\Windows.Services.Maps.GuidanceContract\1.0.0.0\Windows.Services.Maps.GuidanceContract.winmd</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include="Windows.Services.Maps.LocalSearchContract">
      <HintPath>$(MsBuildProgramFiles32)\Windows Kits\10\References\Windows.Services.Maps.LocalSearchContract\1.0.0.0\Windows.Services.Maps.LocalSearchContract.winmd</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include="Windows.System.Profile.SystemManufacturers.SystemManufacturersContract">
      <HintPath>$(MsBuildProgramFiles32)\Windows Kits\10\References\Windows.System.Profile.SystemManufacturers.SystemManufacturersContract\1.0.0.0\Windows.System.Profile.SystemManufacturers.SystemManufacturersContract.winmd</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include="Windows.System.Profile.ProfileHardwareTokenContract">
      <HintPath>$(MsBuildProgramFiles32)\Windows Kits\10\References\Windows.System.Profile.ProfileHardwareTokenContract\1.0.0.0\Windows.System.Profile.ProfileHardwareTokenContract.winmd</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include="Windows.System.Profile.ProfileRetailInfoContract">
      <HintPath>$(MsBuildProgramFiles32)\Windows Kits\10\References\Windows.System.Profile.ProfileRetailInfoContract\1.0.0.0\Windows.System.Profile.ProfileRetailInfoContract.winmd</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include="Windows.System.UserProfile.UserProfileContract">
      <HintPath>$(MsBuildProgramFiles32)\Windows Kits\10\References\Windows.System.UserProfile.UserProfileContract\1.0.0.0\Windows.System.UserProfile.UserProfileContract.winmd</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include="Windows.System.UserProfile.UserProfileLockScreenContract">
      <HintPath>$(MsBuildProgramFiles32)\Windows Kits\10\References\Windows.System.UserProfile.UserProfileLockScreenContract\1.0.0.0\Windows.System.UserProfile.UserProfileLockScreenContract.winmd</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include="Windows.UI.ApplicationSettings.ApplicationsSettingsContract">
      <HintPath>$(MsBuildProgramFiles32)\Windows Kits\10\References\Windows.UI.ApplicationSettings.ApplicationsSettingsContract\1.0.0.0\Windows.UI.ApplicationSettings.ApplicationsSettingsContract.winmd</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include="Windows.UI.Core.AnimationMetrics.AnimationMetricsContract">
      <HintPath>$(MsBuildProgramFiles32)\Windows Kits\10\References\Windows.UI.Core.AnimationMetrics.AnimationMetricsContract\1.0.0.0\Windows.UI.Core.AnimationMetrics.AnimationMetricsContract.winmd</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include="Windows.UI.Core.CoreWindowDialogsContract">
      <HintPath>$(MsBuildProgramFiles32)\Windows Kits\10\References\Windows.UI.Core.CoreWindowDialogsContract\1.0.0.0\Windows.UI.Core.CoreWindowDialogsContract.winmd</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include="Windows.UI.Xaml.Hosting.HostingContract">
      <HintPath>$(MsBuildProgramFiles32)\Windows Kits\10\References\Windows.UI.Xaml.Hosting.HostingContract\1.0.0.0\Windows.UI.Xaml.Hosting.HostingContract.winmd</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include="Windows.Web.Http.Diagnostics.HttpDiagnosticsContract">
      <HintPath>$(MsBuildProgramFiles32)\Windows Kits\10\References\Windows.Web.Http.Diagnostics.HttpDiagnosticsContract\1.0.0.0\Windows.Web.Http.Diagnostics.HttpDiagnosticsContract.winmd</HintPath>
      <Private>False</Private>
    </Reference>
</ItemGroup>
```

The references above are a careful mix of eferences that are critical to
the proper operation of this hybrid server. The protocol is to open the
.csproj file (as described in how to edit the project OutputType) and
add these references as necessary.

Once the references are properly configured, the next task is to
implement the server's functionality. See the topic [Best practices
for interoperability with Windows Runtime components (UWP apps
using C\#/VB/C++ and
XAML)](/previous-versions/windows/apps/hh750311(v=win.10)).
The task is to create a Windows Runtime component dll that is able to
call desktop code as part of its implementation. The accompanying sample
includes the major patterns used in Windows Runtime:

-   Method calls

-   Windows Runtime Events sources by the desktop component

-   Windows Runtime Async operations

-   Returning arrays of basic types

**Install**

To install the app, copy the implementation **winmd** to the correct
directory specified in the associated side-loaded application's
manifest: <ActivatableClassAttribute>'s Value="path". Also copy
any associated support files and the proxy/stub dll (this latter detail
is covered below). Failing to copy the implementation **winmd** to the
server directory location will cause all of the side-loaded
application's calls to new on the RuntimeClass will throw a "class not
registered" error. Failure to install the proxy/stub (or failure to
register) will cause all calls to fail with no return values. This
latter error is frequently **not** associated with visible exceptions.
If exceptions are observed due to this configuration error, they may
refer to "invalid cast".

**Server implementation considerations**

The desktop Windows Runtime server can be thought of as "worker" or
"task" based. Every call into the server operates on a non-UI thread and
all code must be multi-thread aware and safe. Which part of the
side-loaded application is calling the server's functionality is also
important. It is critical to always avoid calling long-running code from
any UI thread in the side-loaded application. There are two main ways to
accomplish this:

1.  If calling server functionality from a UI thread, always use an
    async pattern in the server's public surface area
    and implementation.

2.  Call the server's functionality from a background thread in the
    side-loaded application.

**Windows Runtime async in the server**

Given the cross-process nature of the application model, calls to the
server have more overhead than code that runs exclusively in-process. It
is normally safe to call a simple property that returns an in-memory
value because it will execute quickly enough that blocking the UI thread
is not a concern. However, any call that involves I/O of any sort (this
includes all file handling and database retrievals) can potentially
block the calling UI thread and cause the application to be terminated
due to unresponsiveness. In addition, property calls on objects are
discouraged in this application architecture for performance reasons.
This is covered in more depth in the following section.

A properly implemented server will normally implement calls made
directly from UI threads via the Windows Runtime async pattern. This can
be implemented by following this pattern. First, the declaration (again,
from the accompanying sample):

```csharp
public IAsyncOperation<int> FindElementAsync(int input)
```

This declares a Windows Runtime async operation that returns an integer.
The implementation of the async operation normally takes the form:

```csharp
return Task<int>.Run( () =>
{
    int retval = ...
    // execute some potentially long-running code here 
}).AsAsyncOperation<int>();

```

>**Note** It is common to await some other potentially long running
operations while writing the implementation. If so,
the **Task.Run** code needs to be declared:

```csharp
return Task<int>.Run(async () =>
{
    int retval = ...
    // execute some potentially long-running code here 
    await ... // some other WinRT async operation or Task
}).AsAsyncOperation<int>();
```

Clients of this async method can await this operation like any other
Windows Runtime aysnc operation.

**Call server functionality from an application background thread**

Since it is typical that both client and server will be written by the
same organization, a programming practice can be adopted that all calls
to the server will be made by a background thread in the side-loaded
application. A direct call that collects one or more batches of data
from the server can be made from a background thread. When the result(s)
are completely retrieved, the batch of data that is in-memory in the
application process can usually be directly retrieved from the UI
thread. C\# objects are naturally agile between background threads and
UI threads so are especially useful for this kind of calling pattern.

## Creating and deploying the Windows Runtime proxy

Since the IPC approach involves marshaling Windows Runtime interfaces
between two processes, a globally registered Windows Runtime proxy and
stub must be used.

**Creating the proxy in Visual Studio**

The process for creating and registering proxies and stubs for use
inside a regular UWP app package are described in the
topic [Raising Events in Windows Runtime
Components](/previous-versions/windows/apps/dn169426(v=vs.140)).
The steps described in this article are more complicated than the
process described below because it involves registering the proxy/stub
inside the application package (as opposed to registering it globally).

**Step 1:** Using the solution for the desktop component project, create a
    Proxy/Stub project in Visual Studio:

**Solution > Add > Project > Visual C++ > Win32 Console Select DLL
option.**

For the steps below, we assume the server component is
called **MyWinRTComponent**.

**Step 3:** Delete all the CPP/H files from the project.

**Step 4:** The previous section "Defining the Contract" contains a Post-Build command that runs **winmdidl.exe**, **midl.exe**, **mdmerge.exe**, and so on. One of the outputs from the midl step of this post-build command generates four important outputs:

a) Dlldata.c

b) A header file (for example, MyWinRTComponent.h)

c) A \*\_i.c file (for example, MyWinRTComponent\_i.c)

d) A \*\_p.c file (for example, MyWinRTComponent\_p.c)

**Step 5:** Add these four generated files to the "MyWinRTProxy" project.

**Step 6:** Add a def file to "MyWinRTProxy" project **(Project > Add New Item > Code > Module-Definition File**) and update the contents to be:

LIBRARY MyWinRTComponent.Proxies.dll

EXPORTS

DllCanUnloadNow PRIVATE

DllGetClassObject PRIVATE

DllRegisterServer PRIVATE

DllUnregisterServer PRIVATE

**Step 7:** Open properties for the "MyWinRTProxy" project:

**Comfiguration Properties > General > Target Name :**

MyWinRTComponent.Proxies

**C/C++ > Preprocessor Definitions > Add**

"WIN32;\_WINDOWS;REGISTER\_PROXY\_DLL"

**C/C++ > Precompiled Header : Select "Not Using Precompiled Header"**

**Linker > General > Ignore Import Library : Select "Yes"**

**Linker > Input > Additional Dependencies : Add
rpcrt4.lib;runtimeobject.lib**

**Linker > Windows Metadata > Generate Windows Metadata : Select "No"**

**Step 8:** Build the "MyWinRTProxy" project.

**Deploying the proxy**

The proxy must be globally registered. The simplest way to do this is to
have your install process call DllRegisterServer on the proxy dll. Note
that since the feature only supports servers built for x86 (i.e. no
64-bit support), the simplest configuration is to use a 32-bit server, a
32-bit proxy, and a 32-bit side-loaded application. The proxy normally
sits alongside the implementation **winmd** for the desktop component.

One additional configuration step must be performed. In order for the
side-loaded process to load and execute the proxy, the directory must be
marked "read / execute" for ALL_APPLICATION_PACKAGES. This is done via
the **icacls.exe** command line tool. This command should execute in the
directory where the implementation **winmd** and proxy/stub dll resides:

*icacls . /T /grant \*S-1-15-2-1:RX*

## Patterns and performance

It is very important that performance of the cross-process transport be
carefully monitored. A cross-process call is at least twice as expensive
than an in-process call. Creating "chatty" conversations cross-process
or performing repeated transfers of large objects like bitmap images,
can cause unexpected and undesirable application performance.

Here is a non-exhaustive list of things to consider:

-   Synchronous method calls from application's UI thread to the server
    should always be avoided. Call the method from a background thread
    in the application and then use CoreWindowDispatcher to get the
    results onto the UI thread if necessary.

-   Calling async operations from an application UI thread is safe, but
    consider the performance problems discussed below.

-   Bulk transfer of results reduces cross-process chattiness. This is
    normally performed by using the Windows Runtime Array construct.

-   Returning *List<T>* where *T* is an object from an async
    operation or property fetch, will cause a lot of
    cross-process chattiness. For example, assume you return
    a*List&lt;People&gt;* objects. Each iteration pass will be a
    cross-process call. Each *People* object returned is represented by
    a proxy and each call to a method or property on that individual
    object will result in a cross-process call. So an
    "innocent" *List&lt;People&gt;* object where *Count* is large will
    cause a large number of slow calls. Better performance results from
    bulk transfer of structs of the content in an array. For example:

```csharp
struct PersonStruct
{
    String LastName;
    String FirstName;
    int Age;
   // etc.
}
```

Then return *PersonStruct\[\]* instead of *List&lt;PersonObject&gt;*.
This gets all the data across in one cross-process "hop"

As with all performance considerations, measurement and testing is
critical. Ideally telemetry should be inserted into the various
operations to determine how long they take. It is important to measure
across a range: for example, how long does it actually take to consume
all the *People* objects for a particular query in the side-loaded
application?

Another technique is variable load testing. This can be done by putting
performance test hooks into the application that introduce variable
delay loads into the server processing. This can simulate various kinds
of load and the application's reaction to varying server performance.
The sample illustrates how to put time delays into code using proper
async techniques. The exact amount of delay to inject and the range of
randomization to put into that artificial load will vary by application
design and the anticipated environment in which the application will
run.

## Development process

When you make changes to the server, it is necessary to make sure any
previously running instances are no longer running. COM will eventually
scavenge the process, but the rundown timer takes longer than is
efficient for iterative development. Thus, killing a previously running
instance is a normal step during development. This requires that the
developer keep track of which dllhost instance is hosting the server.

The server process can be found and killed using Task Manager or other
third party apps. The command line tool **TaskList.exe** is also
included and has flexible syntax, for example:

  
 | **Command** | **Action** |
 | ------------| ---------- |
 | tasklist | Lists all the running processes in approximate order of creation time, with the most recently created processes near the bottom. |
 | tasklist /FI "IMAGENAME eq dllhost.exe" /M | Lists info on all the dllhost.exe instances. The /M switch lists the modules that they have loaded. |
 | tasklist /FI "PID eq 12564" /M | You can use this option to query the dllhost.exe if you know its PID. |

The module list for a broker server should list *clrhost.dll* in its
list of loaded modules.

## Resources

-   [Brokered WinRT Component Project Templates for Windows 10 and VS 2015](https://marketplace.visualstudio.com/items?itemName=vs-publisher-713547.VS2015TemplateBrokeredComponents)

-   [Delivering reliable and trustworthy Microsoft Store
    apps](https://blogs.msdn.com/b/b8/archive/2012/05/17/delivering-reliable-and-trustworthy-metro-style-apps.aspx)

-   [App contracts and extensions (Windows
    Store apps)](/previous-versions/windows/apps/hh464906(v=win.10))

-   [How to sideload apps on Windows 10](../get-started/enable-your-device-for-development.md)

-   [Deploying UWP apps to
    businesses](https://blogs.msdn.com/b/windowsstore/archive/2012/04/25/deploying-metro-style-apps-to-businesses.aspx)