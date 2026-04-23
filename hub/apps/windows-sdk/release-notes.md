---
title: What's new in Windows SDK
description: Provides information about release notes for the Windows SDK.
ms.topic: release-notes
ms.date: 03/11/2025
keywords: windows win32, windows app development, Windows SDK, Windows Platform SDK, windows 11
ms.localizationpriority: medium
---

# What's new in the Windows SDK

In a new or existing Windows app, you can get the Windows SDK in several ways: install it from the installer or ISO, in the Visual Studio 2022 Installer, or by downloading the NuGet package.

You can update the SDK by manually installing the new build, updating in Visual Studio or update the Nuget package

For the the latest builds, see [Downloads for the Windows SDK](./downloads.md).

## Build 10.0.26100.7627

Released: **January, 2026** <br><br>

<details>
<summary>WinRT API additions and updates</summary>

> **Windows.Security.Credentials**:
> <br/>
> New interfaces:
>
> - `IKeyCredentialManagerCreateWithWindowStatics` - Provides methods for creating key credentials with an associated window handle
> - `IKeyCredentialWithWindow` - Extends key credential functionality with window association support
>
> New methods:
>
> - `KeyCredentialManager.RequestCreateForWindowAsync` - Creates a key credential associated with a specific window

</details>

<details>
<summary>Win32 API additions and updates</summary>

> **WinSQLite (winsqlite3.h / winsqlite3ext.h)**
> <br/>
> SQLite version update:
>
>
> New error codes:
>
> - `SQLITE_ERROR_RESERVESIZE`
> - `SQLITE_ERROR_KEY`
> - `SQLITE_ERROR_UNABLE`
>
> New IO error codes:
>
> - `SQLITE_IOERR_BADKEY`
> - `SQLITE_IOERR_CODEC`
>
> New capabilities:
>
> - `SQLITE_IOCAP_SUBPAGE_READ`
>
> New file control codes:
>
> - `SQLITE_FCNTL_NULL_IO`
> - `SQLITE_FCNTL_BLOCK_ON_CONNECT`
>
> New source control management constants:
>
> - `SQLITE_SCM_BRANCH`
> - `SQLITE_SCM_TAGS`
> - `SQLITE_SCM_DATETIME`
>     
> **winnt.h**
> <br/>
> New definitions:
>
> - `SECURITY_MANDATORY_MEDIUM_PLUS_CREDUI_RID` - New security mandatory integrity level RID for Credential UI contexts

</details>

---

## Build 10.0.26100.7463

Released: **December, 2025** <br><br>

<details>
<summary>WinRT API additions and updates</summary>

> **Windows.Management.Deployment**:
>    - Added the `PackageOperationPriority` enum and new priority fields exposed via `AddPackageOptions` and `StagePackageOptions`.

</details>

<details>
<summary>Win32 API additions and updates</summary>

> **Driver runtime reporting**
> <br/>
> Added new structs:
>
> - `_DRIVER_INFO_ENTRY`
> - `_DRIVER_RUNTIME_REPORT`
> - `_RUNTIME_REPORT_DIGEST_HEADER`
>     
> **DNS SVCB/HTTPS record parsing**
> <br/>
> Added new structs:
>
> - `DNS_SVCB_PARAM`
> - `DNS_SVCB_PARAM_IPV4`, `DNS_SVCB_PARAM_IPV6`
> - `DNS_SVCB_PARAM_ALPN`
> - `DNS_SVCB_PARAM_MANDATORY`
> - `DNS_SVCB_PARAM_UNKNOWN`
>     
> **Search and Query engine**
> <br/>
> Updated or added structs:
>
> - `CONTENTRESTRICTION`
> - `VECTORRESTRICTION`
> - `NODERESTRICTION`
> - `NOTRESTRICTION`
>     
> **Audio / Device activation**
> <br/>
> Added the `IMMDeviceActivator` interface for new device-level activation scenarios within the audio stack.
>     
> **Firmware table enumeration**
> <br/>
> Updated:
>
> - `EnumSystemFirmwareTables`
> - `GetSystemFirmwareTable`

</details>

--- 

## Build 10.0.26100.7175

Released: **November, 2025** <br><br>

<details>
<summary>Updated APIs</summary>

> Updates made to the following Win32 API headers (defines, structs, enums, interfaces and other changes):
>
> - AppxPackaging.h, AppxPackaging.idl (interfaces IAppxFactory4, IAppxBundleFactory3, IAppxBundleReader2)

</details>

<details>
<summary>New APIs</summary>

> WinRT namespaces updated (new or modified APIs/types):
>
> - Windows.ApplicationModel.DataTransfer
> - Windows.Management.Update
> - Windows.Security.Credentials
> - Windows.Storage.Provider
> - Windows.System.RemoteSystems
> - AppxManifestTypes.xsd schema updated

</details>

<details>
<summary>Known issues</summary>

> **BinSkim Warning 4146 Triggered by Windows SDK 10.0.26100.7175 in Visual Studio**
>
>  - We are aware of an issue where builds may fail with BinSkim rule BA2007 due to warning C4146 being explicitly disabled in a small number of Windows SDK libraries included in the Windows SDK version 10.0.26100.7175, which shipped with Visual Studio 17.14.22.
> - Developers may see build breaks or security‑tool validation failures when using this SDK version, depending on project configuration and toolchain settings.
> - A fix has been identified and is currently being prepared for a Visual Studio update. This will update the affected SDK content to restore expected behavior. Until the fix is available, you may use one of the following mitigations:
>   - Retarget your project to another supported Windows SDK version, or
>   - Suppress the specific BinSkim warning in your build configuration (not recommended long‑term)

</details>

--- 

## Build 10.0.26100.6901

Released: **October, 2025** <br><br>

<details>
<summary>Updated APIs</summary>

> Updates made to the following Win32 API headers, defines, structs, enums, and other changes:
>
> - networksetup.h
> - windows.system.power.thermal.h
> - windows.ui.input.preview.text.h

</details>

<details>
<summary>New APIs</summary>

> Added new APIs to the following WinRT namespaces:
>
> - Windows.AI.Actions
> - Windows.Management.Update
> - Windows.Media.Core

</details>

--- 

## Build 10.0.26100.6584

Released: **September, 2025**

Release to correspond with the Windows 11, version 25h2 public release. <br><br>

<details>
<summary>Updated APIs</summary>

> Updates made to the following Win32 API headers, defines, structs, enums, and other changes:
>
> - winnt.h
> - WtsApi32.h
> - wtsdefs.h
> - wtsprotocol.h
> - NetworkSetup.h
> - FoundationManifestSchema.xsd
> - AccessControlManifestSchema.xsd
> - AppDataManifestSchema.xsd
>
> Added new APIs to the following WinRT headers and idl:
>
> - windows.security.credentials.h
> - windows.system.power.thermal.h, windows.system.power.thermal.idl

</details>

<details>
<summary>New Experimental APIs</summary>

> Updated or added experimental APIs to the following:
>
> - windows.ai.actions.h
> - windows.ai.actions.hosting.h
> - windows.ai.agents.mcp.h
> - windows.ai.agents.h
> - windows.graphics.printing.printsupport.h
> - windows.graphics.printing.printticket.h, windows.graphics.printing.printticket.idl
> - windows.devices.printers.h
> - windows.applicationmodel.contacts.h

</details>

--- 

### Build 10.0.26100.4948

Released: **August, 2025**

<details>
<summary>Updated APIs</summary>

> Updates made to the following Win32 API headers, adding new defines, structs, enums, and other changes:
>
> - ModelContextProtocolHelpers.h
> - PrintSupportManifestSchema_v3.xsd

</details>

<details>
<summary>New APIs</summary>

> Added new APIs to the following WinRT headers and idl:
>
> - windows.graphics.printing.printsupport.h
> - windows.storage.provider.h
> - windows.devices.printers.h
> - windows.applicationmodel.activation.h
> - windows.ui.input.preview.text.h
>
> Removed experimental tag from:
>
> - windows.ui.input.preview.text (APIs previously behind ENABLE_WINRT_EXPERIMENTAL_TYPES are now stable)

</details>

--- 

## Build 10.0.26100.4654

Released: **July, 2025** <br><br>

<details>
<summary>Updated APIs</summary>

> Updates made to the following Win32 API headers, adding new defines, structs, enums and other changes:
>
> - bugcodes.h
> - d3d12.h
> - d3d12.idl
> - DbgEng.h
> - fwpmu.h
> - hidusage.h
> - ksarm64.h
> - minidumpapiset.h
> - ModelContextProtocolHelpers.h
> - ntddstor.h
> - ntddvdeo.h
> - ntlsa.h
> - ntstatus.h
> - nvme.h
> - Raseapif.h
> - sherrors.h
> - srb.h
> - WaaSApiTypes.h
> - WaaSApiTypes.idl
> - webauthn.h
> - winbio_types.h
> - winerror.h
> - WinHvPlatformDefs.h
> - winioctl.h

</details>

<details>
<summary>New APIs</summary>

> Added new APIs to the following Win32 headers:
>
> - wincodec.h, wincodec.idl
>   - **IWICBitmapFrameChainReader**
>   - **IWICBitmapFrameChainWriter**
>   - **IWICDisplayAdaptationControl2**
> - wincodecsdk.h, wincodecsdk.idl
>   - **GUID_MetadataFormatGainMap**
>   - **CLSID_WICGainMapMetadataReader**
>   - **CLSID_WICGainMapMetadataWriter**
>
> Added new WinRT Preview namespaces:
>
> - **windows.ui.input.preview.text**

</details>

--- 

## Build 10.0.26100.4188

Released: **May, 2025** <br><br>

<details>
<summary>New APIs</summary>

> Added or updated new APIs to the following WinRT namespaces:
>
> - **Windows.AI.Actions**
> - **Windows.AI.ModelContextProtocol** (experimental)
> - **Windows.ApplicationModel.Background.Bluetooth**
> - **Windows.Devices.Bluetooth**
> - **Windows.UI.ViewManagement**
>
> Added new APIs to the following Win32 headers:
>
> - http.h
>   - **HttpQueryRequestProperty**
> - ntlsa.h
>   - **LsaSetLocalSystemAccess**
>   - **LsaQueryLocalSystemAccess**
>   - **LsaQueryLocalSystemAccessAll**
> - WinUser.h
>   - **ConvertToInterceptWindow**
>   - **IsInterceptWindow**
>   - **ApplyWindowAction**
>   - **RegisterCloakedNotification**
>   - **EnterMoveSizeLoop**

</details>

<details>
<summary>Updated APIs</summary>

> Updates made to the Win32 CRT headers:
>
> - corecrt_search.h
> - wchar.h
>
> Updates made to the following Win32 API headers, adding new defines, structs and enums:
>
> - CertSrv.h
> - codecapi.h
> - dwmapi.h
> - MDMRegistration.h
> - mfapi.h
> - ntddvdeo.h
> - NTSecAPI.h
> - NTSecPKG.h
> - ntstatus.h
> - overridecapabilities.h
> - Propkey.h
> - WindowsSearchErrors.h
> - Winldap.h
> - rpcndr.h
> - winerror.h
>
> Added new Win32 API header:
>
> - ModelContextProtocolHelpers.h 

</details>

<details>
<summary>New Experimental APIs</summary>

> Added experimental APIs to the following Win32 API headers:
>
> - webauthn.h
> - WinBio.h
> - winbio_types.h

</details>

--- 

## Build 10.0.26100.3916

Released: **April, 2025** <br><br>

<details>
<summary>New APIs</summary>

> Added new APIs to the **windows.ui.viewmanagement** WinRT namespace to support the user's preferred UserInteractionMode.
>
> Added support for semantic search to the **searchapi.h** Win 32 API header.
>
> Added new **GamingExperience** Win32 API header.
>
> Added new APIs to the following Win32 headers:
>
> - dcomp.h
> - http.h
> - ntsecpkg.h
> - winioctl.h

</details>

--- 

## Build 10.0.26100.3624

Released: **March, 2025** <br><br>

<details>
<summary>New APIs</summary>

> Added gamepad support to the CoreInputViewKind enumeration in the **windows.ui.viewmanagement.core** WinRT namespace.
>
> Added new APIs to the following Win32 headers:
>
> - fileapi.h
> - ntlsa.h
> - shobjidl_core.h
> - softintrin.h
> - webauthn.h
> - webservices.h
> - winenclaveapi.h
> - winnt.h

</details>

--- 

## Build 10.0.26100.3323

Released: **February, 2025** <br><br>

<details>
<summary>Updated APIs</summary>

> Renamed the PrivacyScreen WinRT APIs added to the **windows.devices.sensors** namespace in the previous build. These are now OnlookerDetection APIs.

</details>

--- 

## Build 10.0.26100.3037

Released: **January, 2025** <br><br>

<details>
<summary>New APIs</summary>

> Added new WinRT APIs in the **windows.devices.sensors** namespace:
>
> - PrivacyScreenOptions class.
> - HumanPresenceSettings.PrivacyScreenOptions method.
> - LightSensor.IsChromaticitySupported method.
> - New properies and structs were also added to support these methods.

</details>

<details>
<summary>New Experimental APIs</summary>

> Experimental APIs were changed within the following Win32 header (please note that Expertimental APIs should not be used in a production environment):
>
> - webauthn.h

</details>

--- 

## Build 10.0.26100.2454

Released: **January, 2025** <br><br>

<details>
<summary>Updated APIs</summary>

> Made major additions or changes to the following Win32 headers:
>
> - windows.applicationmodel.background.h: Added many bluetooth-related APIs.
> - windows.applicationmodel.calls.h: Added many VOIP call configuration APIs.
>
> Added or modified the following Win32 APIs:
>
> certsrv.h:
> - CRL_BUILD_PROPID
> - CRL_EXTRACT_KEY_INDEX
> - CRL_EXTRACT_PARTITION_INDEX
>
> clusapi.h:
> - NodeSriovInfo
>
> combaseapi.h:
> - STDMETHOD_CHPE_PATCHABLE
>
> d2d11.h:
> - d3d11.D3D11_FEATURE_DATA_D3D11_OPTIONS6
>
> filter.h:
> - IPixelFilter
> - IPixelFilter.GetImageInfo
> - IPixelFilter.GetPixelsForImage
> - IMAGE_INFO
> - IPixelFilterVtbl
>
> http.h:
> - _HTTP_REQUEST_TRANSPORT_IDLE_CONNECTION_TIMEOUT_INFO
>
> msclus.h:
> - NodeSriovInfo
>
> ntsecapi.h:
> - _KERB_CHANGEMACHINEPASSWORD_REQUEST
>
> winenclaveapi.h:
> - EnclaveEncryptDataForTrustlet
> - EnclaveUsesAttestedKeys
>
> winnt.h:
> - STDAPI_CHPE_PATCHABLE_
>
> winuser.h:
> - GetCurrentMonitorTopologyId
>
> Please follow best practices to ensure an API is available on a machine before it is called.

</details>

<details>
<summary>New Experimental APIs</summary>

> Many experimental APIs were added to the following Win32 header (Please note that experimental APIs should not be used in a production environment):
>
> - webauthn.h

</details>

--- 

## Build 10.0.26100.1742

Released: **September 24, 2024**

Release to correspond with the Windows 11, version 24H2 public release.

---

## Build 10.0.26100

Released: **May 5, 2024**

Initial release of the 10.0.26100 series, to correspond with the Windows 11, version 24H2 preview.

---

## Build 10.0.22621.3235

Released: **February 29, 2024**

Servicing update 10.0.22621.3235.

---

## Build 10.0.22621.2428

Released: **October 24, 2023**

Servicing update 10.0.22621.2428.

---

## Build 10.0.22621.1778

Released: **May, 2023**

Servicing update 10.0.22621.1778. <br><br>

<details>
<summary>Highlighted features</summary>

> - WindowTabManager APIs allows applications with tabbed interfaces to provide information on open tabs to the Windows shell.
> - Updates to HumanPresence APIs to improve ease-of-use and add new settings for sensors that support human presence capabilities.
> - RemoteDesktop APIs allows applications to switch between a remote and local desktop.

</details>

--- 

## Windows SDK for Windows 11, version 22H2

Servicing update 10.0.22621.755. Includes ARM64 support for the VS 17.4 release

---

## Windows 10 SDK, Version 2104

<details>
<summary>Updated APIs</summary>

> - Removed api-ms-win-net-isolation-l1-1-0.lib. Apps that were linking against api-ms-win-net-isolation-l1-1-0.lib can switch t OneCoreUAP.lib as a replacement.
> - Removed irprops.lib. Apps that were linking against irprops.lib can switch to bthprops.lib as a drop-in replacement.
> - Moved ENUM tagServerSelection from wuapicommon.h to wupai.h and removed the header. If you would like to use the ENUM tagServerSelection, you will need to include wuapi.h or wuapi.idl.
> - The Windows 10 WinRT API Pack lets you add the latest Windows Runtime APIs support to your .NET Framework 4.5+ and .NET Core 3.0+ libraries and apps. To access the Windows 10 WinRT API Pack, see the [Microsoft.Windows.SDK.Contracts nuget package](https://www.nuget.org/packages/Microsoft.Windows.SDK.Contracts).
> - The printf family of functions now [conforms with the IEEE 754 rounding rules](/cpp/c-runtime-library/reference/printf-printf-l-wprintf-wprintf-l#requirements) when printing exactly representable floating-point numbers and will honor the rounding mode requested via calls to [fesetround](/cpp/c-runtime-library/reference/fegetround-fesetround2). Legacy behavior is available when linking with [legacy_stdio_float_rounding.obj](/cpp/c-runtime-library/link-options).
> - Windows App Certification Kit. Several new APIs were added to the Supported APIs list in the App Certification Kit and Windows Store. If there are APIs in the supported list that appear greyed out or disabled in Visual Studio, you can make a small change to your source file, to access them. For more details, see this [known issue](https://social.msdn.microsoft.com/Forums/86ae092f-a9df-4d2d-8d09-8bf1e93c029c/known-issue-in-visual-studio-a-windows-api-is-greyed-out-and-i-cannot-access-it-even-though-it-is?forum=Win10SDKToolsIssues). [Find more updates to tests](/windows/uwp/debug-test-perf/windows-app-certification-kit).

</details>

<details>
<summary>Tool updates</summary>

> Message Compiler (mc.exe) updates:
>
> - Now detects the Unicode byte order mark (BOM) in .mc files. If the .mc file starts with a UTF-8 BOM, it will be read as a UTF-8 file. Otherwise, if it starts with a UTF-16LE BOM, it will be read as a UTF-16LE file. If the -u parameter was specified, it will be read as a UTF-16LE file. Otherwise, it will be read using the current code page (CP_ACP).
> - Now avoids one-definition-rule (ODR) problems in MC-generated C/C++ ETW helpers caused by conflicting configuration macros (e.g. when two .cpp files with conflicting definitions of MCGEN_EVENTWRITETRANSFER are linked into the same binary, the MC-generated ETW helpers will now respect the definition of MCGEN_EVENTWRITETRANSFER in each .cpp file instead of arbitrarily picking one or the other).
>
> Windows Trace Preprocessor (tracewpp.exe) updates:
>
> - Supports Unicode input (.ini, .tpl, and source code) files. Input files starting with a UTF-8 or UTF-16 byte order mark (BOM) will be read as Unicode. Input files that do not start with a BOM will be read using the current code page (CP_ACP). For backwards-compatibility, if the -UnicodeIgnore command-line parameter is specified, files starting with a UTF-16 BOM will be treated as empty.
> - Supports Unicode output (.tmh) files. By default, output files will be encoded using the current code page (CP_ACP). Use command-line parameters -cp:UTF-8 or -cp:UTF-16 to generate Unicode output files.
> - Behavior change: tracewpp now converts all input text to Unicode, performs processing in Unicode, and converts output text to the specified output encoding. Earlier versions of tracewpp avoided Unicode conversions and performed text processing assuming a single-byte character set. This may lead to behavior changes in cases where the input files do not conform to the current code page. In cases where this is a problem, consider converting the input files to UTF-8 (with BOM) and/or using the -cp:UTF-8 command-line parameter to avoid encoding ambiguity.
>
> TraceLoggingProvider.h updates:
>
> - Avoids one-definition-rule (ODR) problems caused by conflicting configuration macros (e.g. when two .cpp files with conflicting definitions of TLG_EVENT_WRITE_TRANSFER are linked into the same binary, the TraceLoggingProvider.h helpers will now respect the definition of TLG_EVENT_WRITE_TRANSFER in each .cpp file instead of arbitrarily picking one or the other).
> - In C++ code, the TraceLoggingWrite macro has been updated to enable better code sharing between similar events using variadic templates.
>
> Signing your apps. Device Guard signing is a Device Guard feature that is available in Microsoft Store for Business and Education, which allows enterprises to guarantee every app comes from a trusted source. See the [documentation about Device Guard Signing](/windows/msix/package/signing-package-device-guard-signing).

</details>

<details>
<summary>SDK updates</summary>

> - SDK headers have been updated to address errors when compiling using the standard-conformant C preprocessor in the MSVC compiler cl.exe (/Zc:preprocessor, introduced in VS 2019 v16.6).
> - Fixed: "GdiplusTypes.h does not compile with NOMINMAX". [See Visual Studio Feedback](https://developercommunity2.visualstudio.com/t/GdiplusTypesh-does-not-compile-with-NOM/727770).
> - When building with /std:c11 or /std:c17, you now get:
>   - C99 tgmath.h
>   - C11 static_assert in assert.h
>   - C11 stdalign.h
>   - C11 stdnoreturn.h

</details>

<details>
<summary>Known issues</summary>

> - Clang/LLVM for Windows v11 targeting ARM64 is not compatible with the latest winnt.h
>   - As a workaround, use the previous version of the Windows 10 SDK (build 19041), or clang/LLVM for Windows v10 when targeting ARM64 platforms
> - DirectXMath (including version 3.16 in this release) is not compatible with Clang/LLVM for Windows on ARM64.
>   - As a workaround, use the latest version of DirectXMath, available from NuGet, vcpkg, or GitHub. These versions include the required hot fixes (version 3.16b).
>     - [//www.nuget.org/packages/directxmath](https://www.nuget.org/packages/directxmath)
>     - [//github.com/microsoft/vcpkg/tree/master/ports/directxmath](https://github.com/microsoft/vcpkg/tree/master/ports/directxmath)
>     - [//github.com/microsoft/DirectXMath](https://github.com/microsoft/DirectXMath)
> - The case of some header files were changed, to normalize them for case-sensitive file systems:
>   - OAIdl.h, ObjIdl.h, ObjIdlbase.h, OCIdl.h, Ole2.h, OleAuto.h, and OleCtl.h were all made lower-case.
>   - For Clang/LLVM for Windows builds, to support both older version and the latest Windows 10 SDK without warnings, add `-Wno-nonportable-system-include-path` to the CLI, or the following #pragma in source:
>
>   `#ifdef __clang__`
>
>   `#pragma clang diagnostic ignored "-Wnonportable-system-include-path"`
>
>   `#endif`

</details>

--- 

## Windows 10 SDK, Version 2004 servicing update

Released: **December 16, 2020**

<details>
<summary>Bugfixes</summary>

> - Resolved unpredictable and hard to diagnose crashes when linking both umbrella libraries and native OS libraries (for example, onecoreuap.lib and kernel32.lib)
> - Resolved issue that prevented AppVerifier from working
> - Resolved issue that caused WACK to fail with "Task failed to enable HighVersionLie"

</details>
