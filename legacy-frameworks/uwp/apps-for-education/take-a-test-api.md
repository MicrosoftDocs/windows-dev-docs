---
description: The JavaScript API for the Microsoft Take a Test app allows you to do secure assessments. Take a Test provides a secure browser that prevents students from using other computer or internet resources during a test.
title: Take a Test JavaScript API.
ms.assetid: 9bff6318-504c-4d0e-ba80-1a5ea45743da
ms.date: 11/15/2023
ms.topic: article
keywords: windows 10, uwp, education
ms.localizationpriority: medium
---

# Take a Test JavaScript API

[Take a Test](/education/windows/take-tests-in-windows-10) is a browser-based UWP app that renders locked-down online assessments for high-stakes testing, allowing educators to focus on the assessment content rather than how to provide a secure testing environment. To achieve this, it uses a JavaScript API that any web application can utilize. The Take-a-test API supports the [SBAC browser API standard](https://www.smarterapp.org/documents/SecureBrowserRequirementsSpecifications_0-3.pdf) for high stakes common core testing.

See the [Take a Test app technical reference](/education/windows/take-a-test-app-technical?f=255&MSPPError=-2147217396) for more information about the app itself. For troubleshooting help, see [Troubleshoot Microsoft Take a Test with the event viewer](troubleshooting.md).

## Reference documentation
The Take a Test APIs exist in the following namespaces. Note that all of the APIs depend on a global `SecureBrowser` object.

| Namespace | Description |
|-----------|-------------|
|[security namespace](#security-namespace)|Contains APIs that enable you to lock down the device for testing and enforce a testing environment. |

### Security namespace

The security namespace allows you to lock down the device, check the list of user and system processes, obtain MAC and IP addresses, and clear cached web resources.

| Method | Description   |
|--------|---------------|
|[lockDown](#lockDown) | Locks down the device for testing. |
|[isEnvironmentSecure](#isEnvironmentSecure) | Determines whether the lockdown context is still applied to the device. |
|[getDeviceInfo](#getDeviceInfo) | Gets details about the platform on which the testing application is running. |
|[examineProcessList](#examineProcessList)|Gets the list of running user and system processes.|
|[close](#close) | Closes the browser and unlocks the device. |
|[getPermissiveMode](#getPermissiveMode)|Checks if permissive mode is on or off.|
|[setPermissiveMode](#setPermissiveMode)|Toggles permissive mode on or off.|
|[emptyClipBoard](#emptyClipBoard)|Clears the system clipboard.|
|[getMACAddress](#getMACAddress)|Gets the list of MAC addresses for the device.|
|[getStartTime](#getStartTime) | Gets the time that the testing app was started. |
|[getCapability](#getCapability) | Queries whether a capability is enabled or disabled. |
|[setCapability](#setCapability)|Enables or disables the specified capability.| 
|[isRemoteSession](#isRemoteSession) | Checks if the current session is logged in remotely. |
|[isVMSession](#isVMSession) | Checks if the current session is running in a virtual machine. |

---

<span id="lockDown"></span>

### lockDown
Locks down the device. Also used to unlock the device. The testing web application will invoke this call prior to allowing students to start testing. The implementer is required to take any actions necessary to secure the testing environment. The steps taken to secure the environment are device specific and for example, include aspects such as disabling screen captures, disabling voice chat when in secure mode, clearing the system clipboard, entering into a kiosk mode, disabling Spaces in OSX 10.7+ devices, etc. The testing application will enable lockdown before an assessment commences and will disable the lockdown when the student has completed the assessment and is out of the secure test.

**Syntax**  
`void SecureBrowser.security.lockDown(Boolean enable, Function onSuccess, Function onError);`

**Parameters**  
* `enable` - **true** to run the Take-a-Test app above the lock screen and apply policies discussed in this [document](/education/windows/take-a-test-app-technical?f=255&MSPPError=-2147217396). **false** stops running Take-a-Test above the lock screen and closes it unless the app is not locked down; in which case there is no effect.  
* `onSuccess` - [optional] The function to call after the lockdown has been successfully enabled or disabled. It must be of the form `Function(Boolean currentlockdownstate)`.  
* `onError` - [optional] The function to call if the lockdown operation failed. It must be of the form `Function(Boolean currentlockdownstate)`.  

**Requirements**  
Windows 10, version 1709 or later

---

<span id="isEnvironmentSecure"></span>

### isEnvironmentSecure
Determines whether the lockdown context is still applied to the device. The testing web application will invoke this prior to allowing students to start testing and periodically when inside the test.

**Syntax**  
`void SecureBrowser.security.isEnvironmentSecure(Function callback);`

**Parameters**  
* `callback` - The function to call when this function completes. It must be of the form `Function(String state)` where `state` is a JSON string containing two fields. The first is the `secure` field, which will show `true` only if all necessary locks have been enabled (or features disabled) to enable a secure testing environment, and none of these have been compromised since the app entered the lockdown mode. The other field, `messageKey`, includes other details or information that is vendor-specific. The intent here is to allow vendors to put additional information that augments the boolean `secure` flag:

```JSON
{
    'secure' : "true/false",
    'messageKey' : "some message"
}
```

**Requirements**  
Windows 10, version 1709 or newer

---

<span id="getDeviceInfo"></span>

### getDeviceInfo
Gets details about the platform on which the testing application is running. This is used to augment any information that was discernible from the user agent.

**Syntax**  
`void SecureBrowser.security.getDeviceInfo(Function callback);`

**Parameters**  
* `callback` - The function to call when this function completes. It must be of the form `Function(String infoObj)` where `infoObj` is a JSON string containing several fields. The following fields must be supported:
    * `os` represents the OS type (for example: Windows, macOS, Linux, iOS, Android, etc.)
    * `name` represents the OS release name, if any (for example: Sierra, Ubuntu).
    * `version` represents the OS version (for example: 10.1, 10 Pro, etc.)
    * `brand` represents the secure browser branding (for example: OAKS, CA, SmarterApp, etc.)
    * `model` represents the device model for mobile devices only; null/unused for desktop browsers.

**Requirements**  
Windows 10, version 1709 or newer

---

<span id="examineProcessList"></span>

### examineProcessList
Gets the list of all processes running on the client machine owned by the user. The testing application will invoke this to examine the list and compare it with a list of processes that have been deemed deny-listed during testing cycle. This call should be invoked both at the start of an assessment and periodically while the student is taking the assessment. If a deny-listed process is detected, the assessment should be stopped to preserve test integrity.

**Syntax**  
`void SecureBrowser.security.examineProcessList(String[] denylistedProcessList, Function callback);`

**Parameters**  
* `denylistedProcessList` - The list of processes that the testing application has deny-listed.  
`callback` - The function to invoke once the active processes have been found. It must be in the form: `Function(String foundDenylistedProcesses)` where `foundDenylistedProcesses` is in the form: `"['process1.exe','process2.exe','processEtc.exe']"`. It will be empty if no deny-listed processes were found. If it is null, this indicates that an error occurred in the original function call.

**Remarks**
The list does not include system processes.

**Requirements**  
Windows 10, version 1709 or newer

---

<span id="close"></span>

### close
Closes the browser and unlocks the device. The testing application should invoke this when the user elects to exit the browser.

**Syntax**  
`void SecureBrowser.security.close(restart);`

**Parameters**  
* `restart` - This parameter is ignored but must be provided.

**Remarks**
In Windows 10, version 1607, the device must be locked down initially. In later versions, this method closes the browser regardless of whether the device is locked down.

**Requirements**  
Windows 10, version 1709 or newer

---

<span id="getPermissiveMode"></span>

### getPermissiveMode
The testing web application should invoke this to determine if permissive mode is on or off. In permissive mode, a browser is expected to relax some of its stringent security hooks to allow assistive technology to work with the secure browser. For example, browsers that aggressively prevent other application UIs from presenting on top of them might want to relax this when in permissive mode. 

**Syntax**  
`void SecureBrowser.security.getPermissiveMode(Function callback)`

**Parameters**  
* `callback` - The function to invoke when this call completes. It must be in the form: `Function(Boolean permissiveMode)` where `permissiveMode` indicates whether the browser is currently in permissive mode. If it is undefined or null, an error occurred in the get operation.

**Requirements**  
Windows 10, version 1709 or newer

---

<span id="setPermissiveMode"></span>

### setPermissiveMode
The testing web application should invoke this to toggle permissive mode on or off. In permissive mode, a browser is expected to relax some of its stringent security hooks to allow assistive technology to work with the secure browser. For example, browsers that aggressively prevent other application UIs from presenting on top of them might want to relax this when in permissive mode. 

**Syntax**  
`void SecureBrowser.security.setPermissiveMode(Boolean enable, Function callback)`

**Parameters**  
* `enable` - The Boolean value indicating the intended permissive mode status.  
* `callback` - The function to invoke when this call completes. It must be in the form: `Function(Boolean permissiveMode)` where `permissiveMode` indicates whether the browser is currently in permissive mode. If it is undefined or null, an error occurred in the set operation.

**Requirements**  
Windows 10, version 1709 or newer

---

<span id="emptyClipBoard"></span>

### emptyClipBoard
Clears the system clipboard. The testing application should invoke this to force clear any data that may be stored in the system clipboard. The **[lockDown](#lockDown)** function also performs this operation.

**Syntax**  
`void SecureBrowser.security.emptyClipBoard();`

**Requirements**  
Windows 10, version 1709 or newer

---

<span id="getMACAddress"></span>

### getMACAddress
Gets the list of MAC addresses for the device. The testing application should invoke this to assist in diagnostics. 

**Syntax**  
`void SecureBrowser.security.getMACAddress(Function callback);`

**Parameters**  
* `callback` - The function to invoke when this call completes. It must be in the form: `Function(String addressArray)` where `addressArray` is in the form: `"['00:11:22:33:44:55','etc']"`.

**Remarks**  
It is difficult to rely on source IP addresses to distinguish between end user machines within the testing servers because firewalls/NATs/Proxies are commonly in use at the schools. The MAC addresses allow the app to distinguish end client machines behind a common firewall for diagnostics purposes.

**Requirements**  
Windows 10, version 1709 or newer

---

<span id="getStartTime"></span>

### getStartTime
Gets the time that the testing app was started.

**Syntax**  
`DateTime SecureBrowser.security.getStartTime();`

**Return**  
A DateTime object indicating the time the testing app was started.

**Requirements**  
Windows 10, version 1709 or newer

---

<span id="getCapability"></span>

### getCapability
Queries whether a capability is enabled or disabled. 

**Syntax**  
`Object SecureBrowser.security.getCapability(String feature)`

**Parameters**  
`feature` - The string to determine which capability to query. Valid capability strings are "screenMonitoring", "printing", and "textSuggestions" (case insensitive).

**Return Value**  
This function returns either a JavaScript Object or literal with the form: `{<feature>:true|false}`. **true** if the queried capability is enabled, **false** if the capability is not enabled or the capability string is invalid.

**Requirements**
Windows 10, version 1703 or newer

---

<span id="setCapability"></span>

### setCapability
Enables or disables a specific capability on the browser.

**Syntax**  
`void SecureBrowser.security.setCapability(String feature, String value, Function onSuccess, Function onError)`

**Parameters**  
* `feature` - The string to determine which capability to set. Valid capability strings are `"screenMonitoring"`, `"printing"`, and `"textSuggestions"` (case insensitive).  
* `value` - The intended setting for the feature. It must be either `"true"` or `"false"`.  
* `onSuccess` - [optional] The function to call after the set operation has been completed successfully. It must be of the form `Function(String jsonValue)` where *jsonValue* is in the form: `{<feature>:true|false|undefined}`.  
* `onError` - [optional] The function to call if the set operation failed. It must be of the form `Function(String jsonValue)` where *jsonValue* is in the form: `{<feature>:true|false|undefined}`.

**Remarks**  
If the targeted feature is unknown to the browser, this function will pass a value of `undefined` to the callback function.

**Requirements**
Windows 10, version 1703 or newer

---

<span id="isRemoteSession"></span>

### isRemoteSession
Checks if the current session is logged in remotely.

**Syntax**  
`Boolean SecureBrowser.security.isRemoteSession();`

**Return value**  
**true** if the current session is remote, otherwise **false**.

**Requirements**  
Windows 10, version 1709 or later

---

<span id="isVMSession"></span>

### isVMSession
Checks if the current session is running within a virtual machine.

**Syntax**  
`Boolean SecureBrowser.security.isVMSession();`

**Return value**  
**true** if the current session is running in a virtual machine, otherwise **false**.

**Remarks**  
This API check can only detect VM sessions that are running in certain hypervisors that implement the appropriate APIs

**Requirements**  
Windows 10, version 1709 or later

---
