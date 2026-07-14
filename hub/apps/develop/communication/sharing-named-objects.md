---
title: Sharing named objects
description: Learn how to share named objects between packaged desktop apps and unpackaged Win32 applications for interprocess communication.
author: GrantMeStrength
ms.author: jken
ms.topic: concept-article
ms.date: 07/12/2026
---

# Sharing named objects

This topic explains how to share named objects between packaged desktop apps (including Windows App SDK apps packaged with MSIX) and unpackaged Win32 applications.

## Named objects in packaged applications

[Named objects](/windows/win32/sync/object-names) provide an easy way for processes to share object handles. After a process has created a named object, other processes can use the name to call the appropriate function to open a handle to the object. Named objects are commonly used for [thread synchronization](/windows/win32/sync/interprocess-synchronization) and [interprocess communication](./interprocess-communication.md).

Applications that run in an AppContainer (such as UWP apps) have an isolated named-object namespace by default — they can only access named objects they've created. Full-trust packaged desktop apps (including Windows App SDK apps) do not have this isolation and create named objects in the global namespace. See [Windows App SDK considerations](#windows-app-sdk-considerations) for details.

To share named objects with AppContainer apps, permissions must be set when objects are created, and names must be qualified when objects are opened. The techniques in this article are also useful when you want to restrict access between any processes, regardless of packaging.

## Creating named objects

Named objects are created with a corresponding `Create` API:

* [CreateEventEx](/windows/win32/api/synchapi/nf-synchapi-createeventexw)
* [CreateFileMapping](/windows/win32/api/memoryapi/nf-memoryapi-createfilemappingw)
* [CreateMutexEx](/windows/win32/api/synchapi/nf-synchapi-createmutexexw)
* [CreateSemaphoreEx](/windows/win32/api/synchapi/nf-synchapi-createsemaphoreexw)
* [CreateWaitableTimerEx](/windows/win32/api/synchapi/nf-synchapi-createwaitabletimerexw)

All of these APIs share an `LPSECURITY_ATTRIBUTES` parameter which enables the caller to specify [access control lists (ACLs)](/previous-versions/windows/desktop/legacy/aa379560(v=vs.85)) to control what processes can access the object. In order to share named objects with packaged applications, permission must be granted within the ACLs when the named objects are created.

Security identifiers (SIDs) represent identities within ACLs. Every packaged application has its own SID based on its package family name. You can generate the SID for a packaged application by passing its package family name to [DeriveAppContainerSidFromAppContainerName](/windows/win32/api/userenv/nf-userenv-deriveappcontainersidfromappcontainername).

> [!NOTE]
> The package family name can be found via the package manifest editor in Visual Studio during development time, via [Partner Center](/windows/apps/publish/view-app-identity-details) for applications published through the Microsoft Store, or via the [Get-AppxPackage](/powershell/module/appx/get-appxpackage?windowsserver2019-ps&preserve-view=true) PowerShell command for applications that are already installed.

[This sample](/windows/win32/api/securityappcontainer/nf-securityappcontainer-getappcontainernamedobjectpath#examples) demonstrates the basic pattern needed to ACL a named object. To share named objects with packaged applications, build an [EXPLICIT_ACCESS](/windows/win32/api/accctrl/ns-accctrl-explicit_access_w) structure for each application:

* `grfAccessMode = GRANT_ACCESS`
* `grfAccessPermissions =` appropriate permissions based on the object and intended usage
    * [Generic Access Rights](/windows/win32/secauthz/generic-access-rights)
    * [Synchronization Object Security and Access Rights](/windows/win32/sync/synchronization-object-security-and-access-rights)
    * [File Mapping Security and Access Rights](/windows/win32/memory/file-mapping-security-and-access-rights)
* `grfInheritance = NO_INHERITANCE`
* `Trustee.TrusteeForm = TRUSTEE_IS_SID`
* `Trustee.TrusteeType = TRUSTEE_IS_USER`
* `Trustee.ptstrName =` the SID acquired from [DeriveAppContainerSidFromAppContainerName](/windows/win32/api/userenv/nf-userenv-deriveappcontainersidfromappcontainername)

By populating the `LPSECURITY_ATTRIBUTES` parameter in `Create` calls with `EXPLICIT_ACCESS` rules for packaged applications, you can grant access to those applications to open the named object.

> [!NOTE]
> Win32 applications can access named objects created by AppContainer apps by qualifying the object names when [opening them](#opening-named-objects), provided the object's DACL permits the access. The default DACL for most named objects grants access to the creator and to administrators. If you've set a restrictive DACL, Win32 callers also need an explicit access entry.

### Example: Creating a named event with ACL (C++)

The following example shows how to create a named event and grant access to a packaged application:

```cpp
#include <windows.h>
#include <sddl.h>
#include <aclapi.h>
#include <userenv.h>

HANDLE CreateSharedEvent(PCWSTR eventName, PCWSTR packageFamilyName)
{
    // Derive the SID for the target packaged application
    PSID appContainerSid = nullptr;
    HRESULT hr = DeriveAppContainerSidFromAppContainerName(
        packageFamilyName, &appContainerSid);
    if (FAILED(hr)) return nullptr;

    // Build an EXPLICIT_ACCESS entry granting synchronization and signal rights
    EXPLICIT_ACCESS_W explicitAccess = {};
    explicitAccess.grfAccessPermissions = SYNCHRONIZE | EVENT_MODIFY_STATE;
    explicitAccess.grfAccessMode = GRANT_ACCESS;
    explicitAccess.grfInheritance = NO_INHERITANCE;
    explicitAccess.Trustee.TrusteeForm = TRUSTEE_IS_SID;
    explicitAccess.Trustee.TrusteeType = TRUSTEE_IS_USER;
    explicitAccess.Trustee.ptstrName = reinterpret_cast<LPWSTR>(appContainerSid);

    // Create the ACL
    PACL acl = nullptr;
    DWORD result = SetEntriesInAclW(1, &explicitAccess, nullptr, &acl);
    if (result != ERROR_SUCCESS)
    {
        FreeSid(appContainerSid);
        return nullptr;
    }

    // Create the security descriptor
    SECURITY_DESCRIPTOR sd = {};
    if (!InitializeSecurityDescriptor(&sd, SECURITY_DESCRIPTOR_REVISION) ||
        !SetSecurityDescriptorDacl(&sd, TRUE, acl, FALSE))
    {
        LocalFree(acl);
        FreeSid(appContainerSid);
        return nullptr;
    }

    SECURITY_ATTRIBUTES sa = {};
    sa.nLength = sizeof(sa);
    sa.lpSecurityDescriptor = &sd;
    sa.bInheritHandle = FALSE;

    // Create the named event with only the rights the creator needs
    HANDLE hEvent = CreateEventExW(&sa, eventName, 0,
        SYNCHRONIZE | EVENT_MODIFY_STATE);

    // Cleanup
    LocalFree(acl);
    FreeSid(appContainerSid);

    return hEvent;
}
```

## Opening named objects

Named objects are opened by passing a name to a corresponding `Open` API:

* [OpenEvent](/windows/win32/api/synchapi/nf-synchapi-openeventw)
* [OpenFileMapping](/windows/win32/api/memoryapi/nf-memoryapi-openfilemappingw)
* [OpenMutex](/windows/win32/api/synchapi/nf-synchapi-openmutexw)
* [OpenSemaphore](/windows/win32/api/synchapi/nf-synchapi-opensemaphorew)
* [OpenWaitableTimer](/windows/win32/api/synchapi/nf-synchapi-openwaitabletimerw)

Named objects created by a packaged application are created within the namespace of the application, otherwise known as the named object path. When opening named objects created by a packaged application, the object names must be prefixed with the creating application's named object path.

[GetAppContainerNamedObjectPath](/windows/win32/api/securityappcontainer/nf-securityappcontainer-getappcontainernamedobjectpath) returns the named object path for a packaged application based on its SID. You can generate the SID for a packaged application by passing its package family name to [DeriveAppContainerSidFromAppContainerName](/windows/win32/api/userenv/nf-userenv-deriveappcontainersidfromappcontainername).

> [!NOTE]
> The package family name can be found via the package manifest editor in Visual Studio during development time, via [Partner Center](/windows/apps/publish/view-app-identity-details) for applications published through the Microsoft Store, or via the [Get-AppxPackage](/powershell/module/appx/get-appxpackage?windowsserver2019-ps&preserve-view=true) PowerShell command for applications that are already installed.

When opening named objects created by a packaged application, use the format `<PATH>\<NAME>`:

* Replace `<PATH>` with the creating application's named object path.
* Replace `<NAME>` with the object name.

> [!NOTE]
> Prefixing object names with `<PATH>` is only required if a packaged application created the object. Named objects created by Win32 applications do not need to be qualified, though access must still be granted when the objects are [created](#creating-named-objects).

## Windows App SDK considerations

Windows App SDK desktop apps packaged with MSIX run as **full-trust processes** with package identity — they do *not* run in an AppContainer. This means their named objects are created in the global namespace by default, just like traditional Win32 applications.

However, you still need the ACL and path-qualification procedures described above in these scenarios:

* **Communicating with a UWP app** — UWP apps run in an AppContainer and have isolated named-object namespaces. Use `GetAppContainerNamedObjectPath` and grant access via ACLs to reach a UWP app's named objects.
* **Restricting access** — Even between full-trust apps, you may want to use explicit ACLs to limit which processes can open your named objects.
* **Unpackaged Windows App SDK apps** — Named objects are in the global namespace with no special handling required, identical to traditional Win32 applications.

## Remarks

Named objects in packaged applications are isolated by default to preserve security and ensure support for application lifecycle events like suspension and termination. Sharing named objects across applications introduces tight binding and versioning constraints and requires each application to be resilient to the lifecycle of others. For these reasons, it is recommended to only share named objects between applications from the same publisher.

## Related content

* [Interprocess communication](./interprocess-communication.md)
* [Object names (Win32)](/windows/win32/sync/object-names)
* [Windows App SDK overview](../../windows-app-sdk/index.md)
