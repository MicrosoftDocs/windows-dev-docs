---
title: What's new in Windows SDK
description: Provides information about release notes for the Windows SDK.
ms.topic: release-notes
ms.date: 06/01/2026
keywords: windows win32, windows app development, Windows SDK, Windows Platform SDK, windows 11
ms.localizationpriority: medium
---

# What's new in the Windows SDK

In a new or existing Windows app, you can get the Windows SDK in several ways: install it from the installer or ISO, in the Visual Studio 2022 Installer, or by downloading the NuGet package.
You can update the SDK by manually installing the new build, updating in Visual Studio or update the Nuget package

For the the latest builds, see [Downloads for the Windows SDK](./downloads.md).

## 28000 versions

## Build 10.0.28000.2114

Released: **May, 2026** <br><br>

<details>
<summary>WinRT API additions and updates</summary>

> **Windows.Devices.Printers**:
> <br/>
> Graduated from experimental to stable:
>
> - `IppAttributeConverter.ConvertPrintTicketToIppAttributesForPrinter(String, WorkflowPrintTicket, String)` — Print ticket to IPP attribute conversion
>
> **Windows.Graphics.Printing.Workflow**:
> <br/>
> Graduated from experimental to stable:
>
> - `PrintWorkflowPrinterJob.IsPassthroughJobWithAttributes` — Property indicating whether a print job is a passthrough job (renamed from `IsPassthroughJob`)

</details>

<details>
<summary>Win32 API additions and updates</summary>

> **Direct3D Kernel Mode (d3dkmthk.h)**
> <br/>
> New:
>
> - `_D3DKMT_QUERYFEATUREINTERFACE` — Query feature interface structure
>
> **Status Codes (ntstatus.h)**
> <br/>
> New:
>
> - `STATUS_SMB_ALTERNATIVE_PORT_CONFLICT` — SMB alternative port conflict status code
>
> **Power (poclass.h)**
> <br/>
> New:
>
> - `BATTERY_TEST_EXEMPT` — Battery test exemption flag
>
> **Error Codes (winerror.h)**
> <br/>
> New:
>
> - `FVE_E_METHOD_MISMATCH` — Full Volume Encryption method mismatch error
> - `ERROR_SMB_ALTERNATIVE_PORT_CONFLICT` — SMB alternative port conflict error
>
> **TPM / Key Storage (ncrypt.h)**
> <br/>
> New:
>
> - `NCRYPT_PCP_SDDIDK_OPERATION` — Platform Crypto Provider SDDI DK operation
> - `NCRYPT_PCP_AIKSTORE_PROPERTY` — AIK store property
> - `NCRYPT_PCP_EKSTORE_PROPERTY` — EK store property
> - `NCRYPT_PCP_SDDIDK_CONTEXT_PROPERTY` — SDDI DK context property
>
> **Content Index (NTQuery.h)**
> <br/>
> New:
>
> - `CI_VERSION_QUERY_METADATA` — Content index version query metadata
>
> **Security Packages (NTSecPKG.h)**
> <br/>
> New:
>
> - `SECPKG_CALL_AGENT_LOGON` — Security package agent logon call flag
>
> **Shell (shellapi.h)**
> <br/>
> New:
>
> - `ABC_OVERLAYDESKTOPICONS` — Overlay desktop icons flag
>
> **Windows Filtering Platform (fwpmtypes.h / fwpvi.h)**
> <br/>
> Removed:
>
> - `FWPM_LAYER_STATISTICS0_`, `FWPM_STATISTICS0_` — Filtering platform statistics structures
> - `FWPM_LAYER_STATISTICS`, `FWPM_STATISTICS` — Filtering platform statistics defines
>
> **Terminal Services Virtual Channels (tsvirtualchannels.h)**
> <br/>
> Removed:
>
> - `IWTSRemoteAppWindowInfoService` — Remote app window info service interface and related definitions

</details>

<details>
<summary>COM API updates</summary>

> **Windows Filtering Platform (fwpmtypes.idl)**
> <br/>
> Removed:
>
> - `FWPM_LAYER_STATISTICS0_`, `FWPM_STATISTICS0_` — Filtering platform statistics structures
>
> **Terminal Services Virtual Channels (tsvirtualchannels.idl)**
> <br/>
> Removed:
>
> - `IWTSRemoteAppWindowInfoService` — Remote app window info service interface
> - `GetLocalHwnd` — Get local window handle function

</details>

---

## Build 10.0.28000.1839

Released: **April, 2026** <br><br>

<details>
<summary>WinRT API additions and updates</summary>

> **Windows.Devices.Haptics** (UniversalApiContract 19.0):
> <br/>
> New properties on `KnownSimpleHapticsControllerWaveforms`:
>
> - `Collide` — Waveform ID for collision haptic feedback
> - `Align` — Waveform ID for alignment haptic feedback
> - `Step` — Waveform ID for step haptic feedback
> - `Grow` — Waveform ID for growth haptic feedback
>
> **Windows.Devices.Printers**:
> <br/>
> Graduated from experimental to stable:
>
> - `VirtualPrinterInstallationStatus` enum — Installation status values including `InstallationSucceeded`
> - `VirtualPrinterPreferredInputFormat` enum — Preferred input format values including `OpenXps`
> - `IVirtualPrinterInstallationParameters` interface
> - `IVirtualPrinterInstallationResult` interface
> - `IVirtualPrinterManagerStatics` interface
> - `IVirtualPrinterSupportedFormat` interface
> - `IVirtualPrinterSupportedFormatFactory` interface
> - `VirtualPrinterInstallationParameters` runtime class
> - `VirtualPrinterInstallationResult` runtime class
> - `VirtualPrinterSupportedFormat` runtime class
>
> **Windows.Media.ClosedCaptioning** (UniversalApiContract 15.0):
> <br/>
> New types:
>
> - `ClosedCaptionTheme` runtime class — Represents a closed caption theme with customization support
> - `IClosedCaptionTheme` interface — Properties: `Id`, `DisplayName`, `FontColor`, `ComputedFontColor`, `FontOpacity`, `FontSize`, `FontStyle`, `FontEffect`, `BackgroundColor`, `ComputedBackgroundColor`, `BackgroundOpacity`, `RegionColor`, `ComputedRegionColor`, `RegionOpacity`
> - `IClosedCaptionThemeStatics` interface — Methods: `GetAvailableThemes`, `GetSelectedTheme`, `TrySetSelectedTheme`; Events: `ThemesChanged`, `SelectedThemeChanged`

</details>

<details>
<summary>WinRT Experimental API additions</summary>

> **Windows.Storage.Search** (UniversalApiContract 19.0):
> <br/>
> New interface:
>
> - `IQueryOptionsAdditionalSearchSources` — Adds `IncludeCloudProviders` and `IncludeLocalSemanticIndex` properties to `QueryOptions`

</details>

<details>
<summary>Win32 API additions and updates</summary>

> **Bluetooth Hands-Free Profile (bthdef.h)**
> <br/>
> Added comprehensive Bluetooth Hands-Free Profile (HFP) feature constants:
>
> - `HFP_AG_SDP_SUPPORTED_FEATURE_*` — Audio Gateway SDP supported feature flags for three-way calling, echo cancellation, voice recognition, in-band ring tone, voice tag, wide-band speech, and more
> - `HFP_AG_BRSF_SUPPORTED_FEATURE_*` — Audio Gateway BRSF supported feature flags for three-way calling, echo cancellation, voice recognition, codec negotiation, and more
> - `HFP_HF_SDP_SUPPORTED_FEATURE_*` — Hands-Free SDP supported feature flags for echo cancellation, three-way calling, CLI presentation, voice recognition, and more
> - `HFP_HF_BRSF_SUPPORTED_FEATURE_*` — Hands-Free BRSF supported feature flags for echo cancellation, three-way calling, voice recognition, codec negotiation, and more
> - `HFP_NETWORK_NO_ABILITY_TO_REJECT`, `HFP_NETWORK_ABILITY_TO_REJECT` — Network call rejection capability flags
>
> **HID Usages (hidusage.h)**
> <br/>
> New haptics usage values:
>
> - `HID_USAGE_HAPTICS_WAVEFORM_COLLIDE`, `HID_USAGE_HAPTICS_WAVEFORM_ALIGN`, `HID_USAGE_HAPTICS_WAVEFORM_STEP`, `HID_USAGE_HAPTICS_WAVEFORM_GROW`
>
> **NVMe (nvme.h)**
> <br/>
> Fixed typo:
>
> - `NVME_LOG_PAGE_BOOT_PARTITON` renamed to `NVME_LOG_PAGE_BOOT_PARTITION`
>
> **Security / SSPI (sspi.h)**
> <br/>
> New GUID:
>
> - `SEC_WINNT_AUTH_DATA_TYPE_PLACEHOLDER` — Placeholder authentication data type
>
> **Security / LSA (ntlsa.h)**
> <br/>
> New agent-based authentication APIs:
>
> - `LsaCreateAgentAccount` — Creates an agent account
> - `LsaRetrieveAgentLogonCredential` — Retrieves agent logon credentials
> - `LsaEnumerateAgentAccounts` — Enumerates agent accounts
> - `LsaDeleteAgentAccount` — Deletes an agent account
> - `LsaGetAgentOwner` — Gets the agent owner
> - `LSA_AGENT_LOGON_CREDENTIAL` — Agent logon credential struct
> - `LSA_AGENT_ACCOUNT_INFO` — Agent account information struct
> - `LSA_AGENT_ACCOUNT_LIST` — List of agent accounts struct
>
> **Security / Authentication (NTSecPKG.h)**
> <br/>
> New definitions:
>
> - `KSecAllocateContextBuffer` — Function for allocating security context buffers
> - Added `extern "C"` guards for C++ compatibility
>
> **Content Indexing (NTQuery.h)**
> <br/>
> New define:
>
> - `CI_VERSION_CORRID` — Content index correlation ID version constant
>
> **Text Services (TextStor.h)**
> <br/>
> New defines:
>
> - `TS_SD_DISABLEWRITINGSUGGESTIONS` — Flag to disable writing suggestions
> - `TS_SS_MULTILINE` — Flag for multiline text store support
>
> **WRL Async (wrl/async.h)**
> <br/>
> Updated:
>
> - Async completion handling reworked for thread safety using `_InterlockedCompareExchange` and reference counting (`cCompleteDelegateRefCount_`)

</details>

<details>
<summary>COM API updates</summary>

> **Edition Upgrade Helper (EditionUpgradeHelper.idl)**
> <br/>
> Updated method:
>
> - `IClipServiceNotificationHelper::ShowToast` — Parameter list simplified from 5 BSTR parameters to `void`
>
> **Text Services Framework (TextStor.idl)**
> <br/>
> New constants:
>
> - `TS_SD_DISABLEWRITINGSUGGESTIONS` — Flag to disable writing suggestions
> - `TS_SS_MULTILINE` — Flag for multiline text store support

</details>

---

## Build 10.0.28000.1721

Released: **March, 2026** <br><br>

This is a major version bump to the **28000** SDK series.

<details>
<summary>WinRT API additions and updates</summary>

> **Windows.Devices.Haptics** (UniversalApiContract 19.0):
> <br/>
> New types:
>
> - `HapticDeviceType` enum — Defines haptic device types: `None`, `Generic`, `Pen`, `Touchpad`, `Mouse`
> - `HapticsControllerOverrideToken` struct — Token for managing haptics controller overrides
> - `IInputHapticsManager` interface — Provides per-thread haptics management with methods for sending waveforms, controlling duration/play count, stopping feedback, and overriding haptics controllers
> - `IInputHapticsManagerStatics` interface — Static methods: `IsSupported`, `IsHapticDevicePresent`, `GetForCurrentThread`, `TryGetForThread`
> - `InputHapticsManager` runtime class
>
> **Windows.ApplicationModel.Contacts.Provider** (UniversalApiContract 19.0):
> <br/>
> New types:
>
> - `IContactProvider` interface — Provides `GetContactFromRemoteIdAsync` method and `ContactListId` property for contact provider scenarios

</details>

<details>
<summary>WinRT Experimental API additions (UniversalApiContract 20.0)</summary>

> **Windows.Devices.Printers**:
> <br/>
> New types:
>
> - `IppAttributeGroupKind` enum — Defines IPP attribute group kinds: `Printer`, `Job`, `Operation`
> - `IIppAttributeConverterStatics` interface — Provides `ConvertPrintTicketToIppAttributesForPrinter`, `ConvertBufferToIppAttributes`, `ConvertIppAttributesToBuffer`
> - `IppAttributeConverter` runtime class
> - `IPdlPassthroughProvider2` interface — Adds `IsPassthroughWithJobAttributesSupported` property and `StartPrintJobWithIppJobAttributes` method
>
> **Windows.Graphics.Printing.PrintSupport**:
> <br/>
> New types:
>
> - `IPrintSupportPrintDeviceCapabilitiesChangedEventArgs5` interface — Adds `SetPdlPassthroughWithJobAttributesSupported` method
>
> **Windows.Graphics.Printing.Workflow**:
> <br/>
> New types:
>
> - `IPrintWorkflowPrinterJob3` interface — Adds `IsPassthroughJob` property, `GetPassthroughJobAttributes`, and `GetPassthroughJobOperationAttributes` methods
>
> **Windows.UI.Shell.Tasks**:
> <br/>
> New types:
>
> - `IAppTaskInfo2` interface — Adds `Id` and `HiddenByUser` properties and `UpdateDeepLink` method
> - `AppTaskContract` version bumped from 1.0 to 2.0

</details>

<details>
<summary>Win32 API additions and updates</summary>

> **Video Encoding — D3D12 Reconstructed Picture Output (codecapi.h / mfapi.h)**
> <br/>
> New enum and properties for D3D12-based video encoder reconstructed picture output:
>
> - `eAVEncVideoD3D12ReconstructedPictureOutputMode` enum — Values: `None`, `Copy`, `Shared`
> - `CODECAPI_AVEncVideoD3D12ReconstructedPictureOutputMode` codec API GUID
> - `MFSampleExtension_VideoEncodeD3D12ReconstructedPicture` Media Foundation sample extension GUID
>
> **HTTP Server API (http.h)**
> <br/>
> New server property and struct:
>
> - `HttpServerRequestInfoProperty` (=19) — New server property to enable optional request info fields
> - `HTTP_REQUEST_INFO_PROPERTY_INFO` struct with `HTTP_REQUEST_INFO_FLAG_INITIAL_TTL` flag
> - `HttpFeatureTlsHandshakePerformanceCounters` (=17) — New HTTP feature for TLS handshake performance counters
>
> **Graphics / Display Driver (d3dukmdt.h)**
> <br/>
> New driver feature:
>
> - `DXGK_DRIVER_FEATURE_PANEL_BUFFER_CONTROL` (=46) and corresponding `DXGK_FEATURE_PANEL_BUFFER_CONTROL` feature ID
>
> **Event Tracing (evntprov.h / evntcons.h)**
> <br/>
> New definitions:
>
> - `EVENT_DATA_DESCRIPTOR_TYPE_RESERVED1` (=4) — Reserved event data descriptor type
> - `EventProviderSetReserved2` — New value in `EVENT_INFO_CLASS` enum
> - `EVENT_HEADER_FLAG_RESERVED1` (0x0400) — New event header flag
>
> **Storage Provider Properties (propkey.h)**
> <br/>
> New property:
>
> - `PKEY_StorageProviderUserAccountKind` — Identifies the account kind (Unknown, Consumer, Business) for the authenticated storage provider user
>
> **User Input (WinUser.h)**
> <br/>
> New function:
>
> - `ConvertPrimaryPointerToMouseDrag` — Converts primary pointer input to a mouse drag operation
>
> Updated struct:
>
> - `TOUCHPAD_PARAMETERS_V2` — Improved C/C++ layout compatibility
>
> **WebAuthn Plugin API (webauthnplugin.h)**
> <br/>
> Graduated from experimental to stable:
>
> - `WEBAUTHN_PLUGIN_ADD_AUTHENTICATOR_OPTIONS_2` (previously `EXPERIMENTAL_WEBAUTHN_PLUGIN_ADD_AUTHENTICATOR_OPTIONS_2`)
> - `WEBAUTHN_PLUGIN_UPDATE_AUTHENTICATOR_DETAILS_2` (previously `EXPERIMENTAL_WEBAUTHN_PLUGIN_UPDATE_AUTHENTICATOR_DETAILS_2`)
> - `WEBAUTHN_PLUGIN_USER_VERIFICATION_REQUEST_2` (previously `EXPERIMENTAL_WEBAUTHN_PLUGIN_USER_VERIFICATION_REQUEST_2`)
> - `WebAuthNPluginAddAuthenticator2`, `WebAuthNPluginUpdateAuthenticatorDetails2`, `WebAuthNPluginPerformUserVerification2` functions
>
> **Rust Bindgen Compatibility (ntdef.h / winnt.h)**
> <br/>
> Updated:
>
> - `DECLSPEC_NOINITALL` macro now excludes Rust bindgen passes via `!defined(RUST_BINDGEN)`

</details>

<details>
<summary>COM API updates</summary>

> **Shell Object IDL (ShObjIdl_core.idl)**
> <br/>
> Updated:
>
> - `IAttachmentExecute2` — Method comments corrected: `Save2()` renamed to `SaveNoVirusCheck()`, `SaveWithUI2()` renamed to `SaveWithUINoVirusCheck()`

</details>

---

## 26100 versions

## Build 10.0.26100.8249

Released: **April, 2026** <br><br>

<details>
<summary>WinRT API additions and updates</summary>

> **Windows.Devices.Printers**:
> <br/>
> Updated methods:
>
> - `IIppAttributeConverterStatics.ConvertPrintTicketToIppAttributesForPrinter` — Now accepts an additional `targetPdlFormat` parameter
>
> Graduated from experimental to stable:
>
> - `IppAttributeConverter` runtime class
> - `IppAttributeGroupKind` enum
> - `IPdlPassthroughProvider2` interface
>
> **Windows.Graphics.Printing.PrintSupport**:
> <br/>
> Graduated from experimental to stable:
>
> - `IPrintSupportPrintDeviceCapabilitiesChangedEventArgs5` — Interface with `SetPdlPassthroughWithJobAttributesSupported` method
>
> New types:
>
> - `PrintSupportEnterpriseManagementUIEventArgs` — Implements `IActivatedEventArgs` and `IActivatedEventArgsWithUser` for enterprise management UI activation scenarios
>
> **Windows.Graphics.Printing.Workflow**:
> <br/>
> Graduated from experimental to stable:
>
> - `IPrintWorkflowPrinterJob3` — Interface with passthrough attribute support
>
> Updated properties:
>
> - `IsPassthroughJob` renamed to `IsPassthroughJobWithAttributes`

</details>

<details>
<summary>WinRT Experimental API additions</summary>

> **Windows.AI.Agents.Mcp**:
> <br/>
> New interface:
>
> - `IMcpMessageFilterExperimental2` — Adds `Initialize` method with client/server process identifiers and IDs, plus `OnMessage` for MCP message filtering
>
> **Windows.Devices.Haptics**:
> <br/>
> New properties on `KnownSimpleHapticsControllerWaveforms`:
>
> - `Collide` — Waveform ID for collision haptic feedback
> - `Align` — Waveform ID for alignment haptic feedback
> - `Step` — Waveform ID for step haptic feedback
> - `Grow` — Waveform ID for growth haptic feedback
>
> **Windows.Graphics.Capture**:
> <br/>
> New interfaces:
>
> - `IDirect3D11CaptureFrame3` — Adds `ConfigurationIteration` property to capture frames
> - `IGraphicsCaptureSession7` — Adds `ConfigurationIteration` property and window exclusion list management
> - `IDisplayGraphicsCaptureSession` — Display-specific graphics capture session
>
> New methods:
>
> - `SetWindowExclusionList` — Sets a list of windows to exclude from capture
> - `GetWindowExclusionList` — Gets the current window exclusion list
>
> **Windows.Media.ClosedCaptioning**:
> <br/>
> New types:
>
> - `ClosedCaptionTheme` — Represents a closed caption theme with customization support
>
> New methods:
>
> - `GetAvailableThemes` — Retrieves available closed caption themes
> - `GetSelectedTheme` — Gets the currently selected theme
> - `TrySetSelectedTheme` — Attempts to set the selected theme
>
> New events:
>
> - `SelectedThemeChanged` — Fires when the selected closed caption theme changes

</details>

<details>
<summary>Win32 API additions and updates</summary>

> **Event Tracing (evntprov.h / evntcons.h)**
> <br/>
> New definitions:
>
> - `EVENT_DATA_DESCRIPTOR_TYPE_RESERVED1` — Reserved event data descriptor type
> - `EventProviderSetReserved2` — New value in `EVENT_INFO_CLASS` enum
> - `EVENT_HEADER_FLAG_RESERVED1` — New event header flag
>
> **HID Usages (hidusage.h)**
> <br/>
> New haptics usage values:
>
> - `HID_USAGE_HAPTICS_WAVEFORM_COLLIDE` — Collision haptic waveform
> - `HID_USAGE_HAPTICS_WAVEFORM_ALIGN` — Alignment haptic waveform
> - `HID_USAGE_HAPTICS_WAVEFORM_STEP` — Step haptic waveform
> - `HID_USAGE_HAPTICS_WAVEFORM_GROW` — Growth haptic waveform
>
> **Error Codes (winerror.h)**
> <br/>
> New BitLocker error codes:
>
> - `FVE_E_MISSING_PROTECTORS` — BitLocker protectors are missing
> - `FVE_E_METHOD_MISMATCH` — BitLocker method mismatch
>
> **Security / Authentication (NTSecPKG.h)**
> <br/>
> New definitions:
>
> - `SECPKG_CALL_AGENT_LOGON` — Security package call flag for agent-based logon
> - `KSecAllocateContextBuffer` — Function for allocating security context buffers
> - Added `extern "C"` guards for C++ compatibility
>
> **Crypto / TPM (ncrypt.h)**
> <br/>
> New TPM property defines:
>
> - `NCRYPT_PCP_AIKSTORE_PROPERTY` — TPM AIK store property
> - `NCRYPT_PCP_EKSTORE_PROPERTY` — TPM EK store property
>
> **Content Indexing (NTQuery.h)**
> <br/>
> New define:
>
> - `CI_VERSION_QUERY_METADATA` — Content index version for query metadata / semantic reliability
>
> **Shell API (shellapi.h)**
> <br/>
> New define:
>
> - `ABC_OVERLAYDESKTOPICONS` — Overlay desktop icons flag for `ABM_NEW`
>
> **Rust Bindgen Compatibility (ntdef.h / winnt.h)**
> <br/>
> Updated:
>
> - `DECLSPEC_NOINITALL` macro now excludes Rust bindgen passes via `!defined(RUST_BINDGEN)`
>
> **WRL Async (wrl/async.h)**
> <br/>
> Updated:
>
> - Async completion handling reworked for thread safety using `_InterlockedCompareExchange` and reference counting (`cCompleteDelegateRefCount_`)

</details>

---

## Build 10.0.26100.8038

Released: **March, 2026** <br><br>

<details>
<summary>WinRT API additions and updates</summary>

> **Windows.ApplicationModel.Contacts.Provider**:
> <br/>
> New types:
>
> - `IContactProvider` - Interface with `GetContactFromRemoteIdAsync` method
>
> **Windows.Devices.Printers**:
> <br/>
> New types:
>
> - `IppAttributeGroupKind` - Enum for IPP attribute group kinds
> - `IIppAttributeConverterStatics` - Interface with `ConvertPrintTicketToIppAttributesForPrinter`, `ConvertBufferToIppAttributes`, `ConvertIppAttributesToBuffer`
> - `IppAttributeConverter` - Runtime class
> - `IPdlPassthroughProvider2` - Interface with `IsPassthroughWithJobAttributesSupported` property and `StartPrintJobWithIppJobAttributes` method
>
> **Windows.Graphics.Printing.PrintSupport**:
> <br/>
> New types:
>
> - `IPrintSupportPrintDeviceCapabilitiesChangedEventArgs5` - Interface with `SetPdlPassthroughWithJobAttributesSupported` method
>
> **Windows.Graphics.Printing.Workflow**:
> <br/>
> New types:
>
> - `IPrintWorkflowPrinterJob3` - Interface with `IsPassthroughJob` property, `GetPassthroughJobAttributes`, and `GetPassthroughJobOperationAttributes` methods
>
> **Windows.Storage.Search**:
> <br/>
> New types:
>
> - `IQueryOptionsAdditionalSearchSources` - Adds `IncludeCloudProviders` and `IncludeLocalSemanticIndex` properties to `QueryOptions`

</details>

<details>
<summary>Win32 API additions and updates</summary>

> **Bluetooth Hands-Free Profile (bthdef.h)**
> <br/>
> Added new defines for HFP Audio Gateway and Hands-Free SDP and BRSF supported features:
>
> - `HFP_AG_SDP_SUPPORTED_FEATURE_*` - Audio Gateway SDP feature flags for three-way calling, echo cancellation, voice recognition, in-band ring tone, voice tag, wide-band speech, and more
> - `HFP_AG_BRSF_SUPPORTED_FEATURE_*` - Audio Gateway BRSF feature flags for three-way calling, echo cancellation, voice recognition, reject call, enhanced call status/control, codec negotiation, and more
> - `HFP_HF_SDP_SUPPORTED_FEATURE_*` - Hands-Free SDP feature flags for echo cancellation, three-way calling, CLI presentation, voice recognition, remote volume control, wide-band speech, and more
> - `HFP_HF_BRSF_SUPPORTED_FEATURE_*` - Hands-Free BRSF feature flags for echo cancellation, three-way calling, CLI presentation, voice recognition, remote volume control, codec negotiation, and more
> - `HFP_NETWORK_NO_ABILITY_TO_REJECT`, `HFP_NETWORK_ABILITY_TO_REJECT` - Network call rejection capability flags
>
> **Virtualization-Based Security (ntstatus.h / winerror.h)**
> <br/>
> New error codes:
>
> - `STATUS_VSM_FW_MEASUREMENTS_SEAL_FAILURE` - VSM firmware measurements seal failure status
> - `ERROR_VSM_FW_MEASUREMENTS_SEAL_FAILURE` - Corresponding Win32 error code
>
> **Video Encoding (codecapi.h)**
> <br/>
> New enum and property for D3D12 reconstructed picture output:
>
> - `eAVEncVideoD3D12ReconstructedPictureOutputMode` enum
> - `CODECAPI_AVEncVideoD3D12ReconstructedPictureOutputMode` codec API GUID
>
> **Direct3D 12 (d3d12.h)**
> <br/>
> Spelling corrections for tight alignment defines:
>
> - `D3D12_TIGHT_ALIGNMENT_MIN_COMMITTED_RESOURCE_ALIGNMENT` (replaces misspelled `ALIGNEMNT` variant)
> - `D3D12_TIGHT_ALIGNMENT_MIN_PLACED_RESOURCE_ALIGNMENT` (replaces misspelled `ALIGNEMNT` variant)
>
> **Local Security Authority (ntlsa.h)**
> <br/>
> Added new structs for agent-based logon:
>
> - `_LSA_AGENT_LOGON_CREDENTIAL` - Agent logon credential data
> - `_LSA_AGENT_ACCOUNT_INFO` - Agent account information
> - `_LSA_AGENT_ACCOUNT_LIST` - List of agent accounts
>
> **Content Indexing (NTQuery.h)**
> <br/>
> New define:
>
> - `CI_VERSION_CORRID` - Content index version correlation ID
>
> **Secure Channel (schannel.h)**
> <br/>
> Added new types for TLS extension copying:
>
> - `SCH_COPY_EXTS_DATA` struct - Data structure for copying TLS extensions
> - `SchCopyExtsOptions` enum - Options for TLS extension copy operations
>
> **Text Services Framework (TextStor.h)**
> <br/>
> New defines:
>
> - `TS_SD_DISABLEWRITINGSUGGESTIONS` - Flag to disable writing suggestions
> - `TS_SS_MULTILINE` - Flag for multiline text store support
>
> **Remote Desktop Virtual Channels (tsvirtualchannels.h)**
> <br/>
> Added new interfaces, structs, and enums for RDP window information:
>
> - `IWTSWindowChangedCallback` - Callback interface for window change notifications
> - `IWTSWindowInfoService` - Interface for querying window info, client process ID, session type, and subscribing to window changes
> - `WTSWindowInfo` struct - Window information data
> - `RdpSessionType` enum - RDP session type values
>
> **WebAuthn Plugin API (webauthnplugin.h)**
> <br/>
> Graduated from experimental to stable:
>
> - `WEBAUTHN_PLUGIN_ADD_AUTHENTICATOR_OPTIONS_2` (previously `EXPERIMENTAL_WEBAUTHN_PLUGIN_ADD_AUTHENTICATOR_OPTIONS_2`)
> - `WEBAUTHN_PLUGIN_UPDATE_AUTHENTICATOR_DETAILS_2` (previously `EXPERIMENTAL_WEBAUTHN_PLUGIN_UPDATE_AUTHENTICATOR_DETAILS_2`)
> - `WEBAUTHN_PLUGIN_USER_VERIFICATION_REQUEST_2` (previously `EXPERIMENTAL_WEBAUTHN_PLUGIN_USER_VERIFICATION_REQUEST_2`)

</details>

<details>
<summary>COM API updates</summary>

> **Remote Desktop Virtual Channels (tsvirtualchannels.idl)**
> <br/>
> Added new interfaces for RDP window information:
>
> - `IWTSWindowChangedCallback` - Callback interface with `WindowChanged` method
> - `IWTSWindowInfoService` - Interface with `GetWindowInfo`, `GetRdpClientProcessId`, `GetRdpSessionType`, `SubscribeWindowChanged`, `UnsubscribeWindowChanged`
> - `WTSWindowInfo` struct - Window information data
> - `RdpSessionType` enum - RDP session type values

</details>

---

## Build 10.0.26100.7705

Released: **February, 2026** <br><br>

<details>
<summary>WinRT API additions and updates</summary>

> **Windows.UI.Shell.Tasks**:
> <br/>
> New namespace for managing app tasks:
>
> - `AppTaskContract` - API contract for the task APIs
> - `AppTaskState` - Enumeration for task states
> - `IAppTaskContent`, `IAppTaskContentStatics` - Interfaces for task content
> - `IAppTaskInfo`, `IAppTaskInfo2`, `IAppTaskInfoStatics` - Interfaces for task information
> - `IAppTaskResultAsset`, `IAppTaskResultAssetFactory` - Interfaces for task result assets
> - `AppTaskContent` - Runtime class for task content
> - `AppTaskInfo` - Runtime class for task information
> - `AppTaskResultAsset` - Runtime class for task result assets
>
> **Windows.UI.Shell.CompanionWindows**:
> <br/>
> New namespace for companion window management:
>
> - `CompanionWindowsContract` - API contract for companion windows
> - `CompanionWindowRequestResultStatus` - Enumeration for request result status
> - `ICompanionWindowCoordinator`, `ICompanionWindowCoordinatorStatics` - Coordinator interfaces
> - `ICompanionWindowRequest`, `ICompanionWindowRequestResult` - Request interfaces
> - `CompanionWindowCoordinator` - Runtime class for coordinating companion windows
> - `CompanionWindowRequest`, `CompanionWindowRequestResult` - Runtime classes for requests
>
> **Windows.Devices.Haptics**:
> <br/>
> New types and enhancements for haptic feedback:
>
> - `HapticDeviceType` - New enum for haptic device types (UniversalApiContract 19.0)
> - `HapticsControllerOverrideToken` - New struct for controller override tokens
> - `IInputHapticsManager`, `IInputHapticsManagerStatics` - New interfaces for input haptics management
> - `InputHapticsManager` - New runtime class for managing input haptics
>

</details>

<details>
<summary>Win32 API additions and updates</summary>

> **Windows Hypervisor Emulation (WinHvEmulation.h)**
> <br/>
> Added new emulator management functions for AMD64:
>
> - `WHvEmulatorCreateEmulator` - Creates a new emulator instance with specified callbacks
> - `WHvEmulatorDestroyEmulator` - Destroys an emulator instance
> - Additional emulator management APIs for memory access, I/O port handling, and virtual processor register operations
>
> **WebAuthn Plugin (webauthnplugin.h)**
> <br/>
> Updated documentation:
>
> - Plugin RPID is now required (previously optional) for nested WebAuthN calls originating from a plugin
>
> **Windows Error Codes (winerror.h)**
> <br/>
> Updated error definitions and codes
>
> **Graphics Driver Model (d3dukmdt.h, d3dkmdt.h)**
> <br/>
> Updates to graphics driver display mode definitions and user mode types
>
> **Windows User Interface (WinUser.h)**
> <br/>
> Updates to user interface definitions
>
> **Cryptography (wincrypt.h)**
> <br/>
> Updates to cryptographic function definitions
>
> **Property Keys (propkey.h)**
> <br/>
> Updated property key definitions
>
> **HTTP API (http.h)**
> <br/>
> Updates to HTTP server API definitions

</details>

> The following issue is fixed in this SDK version: **BinSkim Warning 4146 Triggered by Windows SDK 10.0.26100.7175 in Visual Studio**

---

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

## 22000 versions and earlier

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
