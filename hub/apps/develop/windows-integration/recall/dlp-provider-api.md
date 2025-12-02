---
title: Recall DLP Provider API
description: Learn how to build a Data Loss Prevention (DLP) provider that integrates with Windows Recall to control content capture based on organizational policies.
ms.date: 11/18/2025
ms.topic: reference
no-loc: [Recall, DLP, EnterpriseContextProvider]
---

# Recall DLP Provider API

Recall (preview) enables users to search locally saved and analyzed snapshots of their screen by using natural language. Recall integrates with Data Loss Prevention (DLP) providers to prevent the storage of sensitive content based on organizational policies. This article describes the public API that enables Recall to work with any DLP tool.

## System architecture

The following diagram shows how Windows Recall interacts with your DLP provider:

```text
┌─────────────────────────────────────────────────────────────┐
│ Windows Recall                                              │
│ - Captures screenshots and app content                      │
│ - Queries DLP provider before capturing                     │
└─────────────────────┬───────────────────────────────────────┘
                      │ Query: Should we capture this window?
                      │ Context: Process, Window, File, Labels
                      ▼
┌─────────────────────────────────────────────────────────────┐
│ AIContext.exe Process                                       │
│                                                             │
│ ┌─────────────────────────────────────────────────────────┐ │
│ │ Your DLP Provider DLL (loaded in-process)               │ │
│ │ - Evaluates organizational policies                     │ │
│ │ - Returns capture restrictions                          │ │
│ │ - Provides sensitivity label information                │ │
│ └─────────────────────────────────────────────────────────┘ │
└─────────────────────┬───────────────────────────────────────┘
                      │ Response: Allow/Block/Warn/Audit
                      │ Labels: Sensitivity information
                      ▼
┌─────────────────────────────────────────────────────────────┐
│ Windows Recall                                              │
│ - Enforces returned restrictions                            │
│ - Displays sensitivity labels to user                       │
│ - Logs audit events as required                             │
└─────────────────────────────────────────────────────────────┘
```

## Public API

### Core Structures

#### RestrictionEnforcement Enumeration

Defines the enforcement level for a specific restriction.

```cpp
enum RestrictionEnforcement
{
    RestrictionEnforcement_Allow = 0,
    RestrictionEnforcement_AuditAndAllow = 1,
    RestrictionEnforcement_Warn = 2,
    RestrictionEnforcement_Block = 3,
};
```

**Values:**

- **RestrictionEnforcement_Allow** (0): The operation is allowed without restrictions.
- **RestrictionEnforcement_AuditAndAllow** (1): The operation is allowed, but should be logged for audit purposes.
- **RestrictionEnforcement_Warn** (2): The operation prompts the user with a warning before proceeding.
- **RestrictionEnforcement_Block** (3): The operation is prevented entirely.

#### Restrictions Structure

Specifies the enforcement level for various operations.

```cpp
struct Restrictions
{
    RestrictionEnforcement CopyToClipboard;
    RestrictionEnforcement CaptureInRecall;
};
```

**Members:**

- **CopyToClipboard**: Enforcement level for copying content to the clipboard.
- **CaptureInRecall**: Enforcement level for capturing content in Recall snapshots.

#### SensitivityLabelDescription Structure

Provides information about a sensitivity label for display to the user.

```cpp
struct SensitivityLabelDescription
{
    LPCWSTR Name;
    LPCWSTR Color;
    LPCWSTR TooltipText;
    uint32_t Sensitivity;
};
```

**Members:**

- **Name**: Display name of the sensitivity label (for example, "Confidential").
- **Color**: Hex color code for visual representation (for example, "#FF0000").
- **TooltipText**: Descriptive text shown when the user hovers over the label.
- **Sensitivity**: Numeric sensitivity level (higher values indicate greater sensitivity).

#### EnterpriseContextQuery Structure

Contains information about a capture request and the response from the DLP provider.

```cpp
struct EnterpriseContextQuery
{
    uint32_t ProcessId;
    uint64_t WindowHandle;
    LPCWSTR FileName;
    LPCWSTR SensitivityLabelId;
    LPCWSTR OrganizationId;
    SensitivityLabelDescription SensitivityLabelDescription;
    Restrictions Restrictions;
};
```

**Members:**

- **ProcessId**: Process ID of the application to be captured.
- **WindowHandle**: Handle to the window being captured.
- **FileName**: Full path to the file open in the application (if applicable).
- **SensitivityLabelId**: Identifier of any existing sensitivity label.
- **OrganizationId**: Organization identifier from the current user context.
- **SensitivityLabelDescription**: Sensitivity label information to be displayed (populated by provider).
- **Restrictions**: Capture restrictions to be enforced (populated by provider).

> [!NOTE]
> Applications can provide sensitivity label information through the `UserActivity.ContentInfo` API. For details on how applications should format and supply this information, see [Provide sensitivity labels to Recall with UserActivity ContentInfo](recall-contentinfo-labels.md).

### Required DLL exports

Your DLP provider DLL must export these functions with the exact names shown:

#### EnterpriseContextProvider_QueryEnterpriseContext

Recall calls this function to evaluate capture requests.

```cpp
HRESULT STDMETHODCALLTYPE EnterpriseContextProvider_QueryEnterpriseContext(
    _In_ ULONG totalQuerySizeBytes,
    _Inout_updates_all_(totalQuerySizeBytes / sizeof(EnterpriseContextQuery)) EnterpriseContextQuery* queryBuffer);
```

**Parameters:**

- **totalQuerySizeBytes**: Total size of the query buffer in bytes.
- **queryBuffer**: Pointer to an array of `EnterpriseContextQuery` structures. Your provider should update the `Restrictions` and `SensitivityLabelDescription` fields based on organizational policies.

**Return Value:**

- Returns `S_OK` on success, or an appropriate `HRESULT` error code on failure.

**Remarks:**

Windows might send multiple queries at the same time for efficiency. Your implementation should process all queries in the buffer and update the appropriate fields before returning.

#### EnterpriseContextProvider_FlushEnterpriseContext

Recall calls this function periodically to allow your provider to free cached strings or resources.

```cpp
VOID STDMETHODCALLTYPE EnterpriseContextProvider_FlushEnterpriseContext();
```

**Remarks:**

Recall calls this function after it examines or copies data from a previous query response. Use this function to deallocate any resources, clear caches, or perform cleanup operations.

## Provider registration

### Registry setup (Provider)

Your DLP provider installation creates a registry entry that contains the path to your DLL:

```registry
HKEY_LOCAL_MACHINE\SOFTWARE\YourCompany\DLP
    InstallPath    REG_SZ    C:\Program Files\YourCompany\DLP
```

**Security considerations:**

Harden the registry key to prevent unauthorized modification. Set appropriate ACLs to restrict write access to administrators only.

### Group Policy Configuration (Administrator)

Administrators configure your provider through the **Set Data Loss Prevention Provider** Group Policy:

- **Policy Name**: `SetDataLossPreventionProvider`
- **Policy Location**: Computer Configuration > Administrative Templates > Windows Components > Windows AI
- **Policy Value Format**: `key:<REGISTRY_PATH>; value:<VALUE_NAME>; binary:<DLL_NAME>`

**Important:** The `value` field refers to the registry value name under the registry key specified by `key`.

**Example Configuration:**

If you creat a registry entry by using:

```cmd
reg add HKLM\Software\YourCompany\DLP -v InstallPath -t REG_SZ -d "C:\Program Files\YourCompany\DLP"
```

And your DLL is named `YourCompanyDLP.dll`, the Group Policy value is:

```text
key:HKLM\software\YourCompany\DLP; value:InstallPath; binary:YourCompanyDLP.dll
```

**Optional Version Checking:**

You can specify a minimum required version for your DLP provider:

```text
key:HKLM\software\YourCompany\DLP; value:InstallPath; binary:YourCompanyDLP.dll; minversion:1.2.0.0
```

If you specify a `minversion`, Recall loads your binary only if its version is equal to or greater than the specified version.

## Query Processing Flow

### Typical interaction sequence

1. **Windows Recall** prepares to capture content from an application window.

2. **Recall creates queries** that include:
   - Process ID and window handle of the target application
   - File path (if the application has an open document)
   - Any existing sensitivity label information
   - Organization ID from the current user context

3. **Your DLP Provider evaluates** each query against organizational policies:
   - Check if the application should be captured
   - Verify file-level restrictions
   - Evaluate sensitivity labels
   - Apply user/group-specific policies

4. **Your Provider returns** updated query structures with:
   - `Restrictions.CaptureInRecall`: Whether to allow, warn, audit, or block capture
   - `SensitivityLabelDescription`: Label name, color, and tooltip for display
   - Any other relevant restriction information

5. **Windows Recall enforces** the returned restrictions:
   - **Allow**: Captures normally
   - **AuditAndAllow**: Captures and logs the action
   - **Warn**: Prompts user before capturing
   - **Block**: Prevents capture entirely

### Example Query Scenario 1: Word Document with "Confidential" Label

**Input:**

- Process: `winword.exe`
- File: `SecretProject.docx`
- Label: `Confidential`

**Your Processing:**

Check document classification policy against organizational rules.

**Output:**

- `CaptureInRecall`: `RestrictionEnforcement_Block`
- `SensitivityLabelDescription.Name`: "Confidential - Do Not Capture"

### Example Query Scenario 2: Web Browser on Public Site

**Input:**

- Process: `msedge.exe`
- Window: `news.example.com`

**Your Processing:**

Check domain against approved list.

**Output:**

- `CaptureInRecall`: `RestrictionEnforcement_Allow`

### Example Query Scenario 3: Finance Application

**Input:**

- Process: `FinanceApp.exe`
- User: `finance_user`

**Your Processing:**

Check user group and application sensitivity.

**Output:**

- `CaptureInRecall`: `RestrictionEnforcement_AuditAndAllow`
- `SensitivityLabelDescription.Name`: "Finance Data - Audited"

## Implementation Guidelines

### Performance Considerations

- **Batch Processing**: Windows might send multiple queries at the same time to be more effecient. Optimize your code to handle batch processing.
- **Caching**: Cache policy decisions when appropriate to improve response times. Use the `FlushEnterpriseContext` function to manage cache lifecycle.
- **Asynchronous Operations**: Avoid blocking operations in the query function. Return quickly to prevent impacting user experience.

### Error Handling

- Return appropriate `HRESULT` codes for different error conditions.
- Use the `FlushEnterpriseContext` function to clean up resources.
- Handle cases where policy information is temporarily unavailable gracefully (default to safe behavior).

### Security requirements

- **In-Process execution**: Your DLL runs in-process within the AIContext.exe process with elevated privileges.
- **Secure coding practices**: Follow secure coding practices for memory management. Validate all input parameters thoroughly.
- **Digital signing**: Your DLL must be Authenticode-signed for deployment. Unsigned binaries aren't loaded.
- **Registry protection**: Harden the registry key specified in Group Policy to prevent unauthorized modifications.

### Loading process

Recall uses `LoadLibraryEx` to load your DLL from the path specified in the registry, then calls `GetProcAddress` to retrieve the addresses of the required exported functions. After calling `QueryEnterpriseContext`, Recall examines and copies data from the response, then calls `FlushEnterpriseContext` to allow your provider to free allocated resources.

## Get started

Follow these steps to create and deploy your DLP provider:

1. **Develop your DLL** by implementing the required exports:
   - `EnterpriseContextProvider_QueryEnterpriseContext`
   - `EnterpriseContextProvider_FlushEnterpriseContext`

2. **Test your implementation** with sample queries to ensure correct behavior across different scenarios.

3. **Sign your binary** with an Authenticode certificate.

4. **Create installation process** that:
   - Installs your DLL to a secure location
   - Sets up the registry entry with appropriate ACLs
   - Provides configuration tools for administrators

5. **Provide Group Policy instructions** for administrators to configure the `SetDataLossPreventionProvider` policy.

6. **Deploy and configure** in your enterprise environment.

## Related Links

- [Manage Recall for Windows clients](/windows/client-management/manage-recall)
- [Set Data Loss Prevention Provider policy](/windows/client-management/mdm/policy-csp-windowsai#setdatalosspreventionprovider)
- [Microsoft Purview Data Loss Prevention](/microsoft-365/compliance/dlp-learn-about-dlp)
