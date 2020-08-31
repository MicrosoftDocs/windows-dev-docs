---
title: ms-tonepicker scheme
description: This topic describes the ms-tonepicker URI scheme and how to use it to display a tone picker to select a tone, save a tone, and get the friendly name for a tone.
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.assetid: 0c17e4fb-7241-4da9-b457-d6d3a7aefccb
ms.localizationpriority: medium
---
# Choose and save tones using the ms-tonepicker URI scheme

This topic describes how to use the **ms-tonepicker:** URI scheme. This URI scheme can be used to:
- Determine if the tone picker is available on the device.
- Display the tone picker to list available ringtones, system sounds, text tones, and alarm sounds; and get a tone token which represents the sound the user selected.
- Display the tone saver, which takes a sound file token as input and saves it to the device. Saved tones are then available via the tone picker. Users can also give the tone a friendly name.
- Convert a tone token to its friendly name.

## ms-tonepicker: URI scheme reference

This URI scheme does not pass arguments via the URI scheme string, but instead passes arguments via a [ValueSet](/uwp/api/windows.foundation.collections.valueset). All strings are case-sensitive.

The sections below indicate which arguments should be passed to accomplish the specified task.

## Task: Determine if the tone picker is available on the device
```cs
var status = await Launcher.QueryUriSupportAsync(new Uri("ms-tonepicker:"),     
                                     LaunchQuerySupportType.UriForResults,
                                     "Microsoft.Tonepicker_8wekyb3d8bbwe");

if (status != LaunchQuerySupportStatus.Available)
{
    // the tone picker is not available
}
```

## Task: Display the tone picker

The arguments you can pass to display the tone picker are as follows:

| Parameter | Type | Required | Possible values | Description |
|-----------|------|----------|-------|-------------|
| Action | string | yes | "PickRingtone" | Opens the tone picker. |
| CurrentToneFilePath | string | no | An existing tone token. | The tone to show as the current tone in the tone picker. If this value is not set, the first tone on the list is selected by default.<br>This is not, strictly speaking, a file path. You can get a suitable value for `CurrenttoneFilePath` from the `ToneToken` value returned from the tone picker.  |
| TypeFilter | string | no | "Ringtones", "Notifications", "Alarms", "None" | Selects which tones to add to the picker. If no filter is specified then all tones are displayed. |

The values that are returned in [LaunchUriResults.Result](/uwp/api/windows.system.launchuriresult.result):

| Return values | Type | Possible values | Description |
|--------------|------|-------|-------------|
| Result | Int32 | 0-success. <br>1-cancelled. <br>7-invalid parameters. <br>8 - no tones match the filter criteria. <br>255 - specified action is not implemented. | The result of the picker operation. |
| ToneToken | string | The selected tone's token. <br>The string is empty if the user selects **default** in the picker. | This token can be used in a toast notification payload, or can be assigned as a contact’s ringtone or text tone. The parameter is returned in the ValueSet only if **Result** is 0. |
| DisplayName | string | The specified tone’s friendly name. | A string that can be shown to the user to represent the selected tone. The parameter is returned in the ValueSet only if **Result** is 0. |


**Example: Open the tone picker so that the user can select a tone**

``` cs
LauncherOptions options = new LauncherOptions();
options.TargetApplicationPackageFamilyName = "Microsoft.Tonepicker_8wekyb3d8bbwe";

ValueSet inputData = new ValueSet() {
    { "Action", "PickRingtone" },
    { "TypeFilter", "Ringtones" } // Show only ringtones
};

LaunchUriResult result = await Launcher.LaunchUriForResultsAsync(new Uri("ms-tonepicker:"), options, inputData);

if (result.Status == LaunchUriStatus.Success)
{
     Int32 resultCode =  (Int32)result.Result["Result"];
     if (resultCode == 0)
     {
         string token = result.Result["ToneToken"] as string;
         string name = result.Result["DisplayName"] as string;
     }
     else
     {
           // handle failure
     }
}
```

## Task: Display the tone saver

The arguments you can pass to display the tone saver are as follows:

| Parameter | Type | Required | Possible values | Description |
|-----------|------|----------|-------|-------------|
| Action | string | yes | "SaveRingtone" | Opens the picker to save a ringtone. |
| ToneFileSharingToken | string | yes | [SharedStorageAccessManager](/uwp/api/windows.applicationmodel.datatransfer.sharedstorageaccessmanager) file sharing token for the ringtone file to save. | Saves a specific sound file as a ringtone. The supported content types for the file are mpeg audio and x-ms-wma audio. |
| DisplayName | string | no | The specified tone’s friendly name. | Sets the display name to use when saving the specified ringtone. |

The values that are returned in [LaunchUriResults.Result](/uwp/api/windows.system.launchuriresult.result):

| Return values | Type | Possible values | Description |
|--------------|------|-------|-------------|
| Result | Int32 | 0-success.<br>1-cancelled by user.<br>2-Invalid file.<br>3-Invalid file content type.<br>4-file exceeds maximum ringtone size (1MB in Windows 10).<br>5-File exceeds 40 second length limit.<br>6-File is protected by digital rights management.<br>7-invalid  parameters. | The result of the picker operation. |

**Example: Save a local music file as a ringtone**

``` cs
LauncherOptions options = new LauncherOptions();
options.TargetApplicationPackageFamilyName = "Microsoft.Tonepicker_8wekyb3d8bbwe";

ValueSet inputData = new ValueSet() {
    { "Action", "SaveRingtone" },
    { "ToneFileSharingToken", SharedStorageAccessManager.AddFile(myLocalFile) }
};

LaunchUriResult result = await Launcher.LaunchUriForResultsAsync(new Uri("ms-tonepicker:"), options, inputData);

if (result.Status == LaunchUriStatus.Success)
{
     Int32 resultCode = (Int32)result.Result["Result"];

     if (resultCode == 0)
     {
         // no issues
     }
     else
     {
          switch (resultCode)
          {
             case 2:
              // The specified file was invalid
              break;
              case 3:
              // The specified file's content type is invalid
              break;
              case 4:
              // The specified file was too big
              break;
              case 5:
              // The specified file was too long
              break;
              case 6:
              // The file was protected by DRM
              break;
              case 7:
              // The specified parameter was incorrect
              break;
          }
      }
 }
```

## Task: Convert a tone token to its friendly name

The arguments you can pass to get the friendly name of a tone are as follows:

| Parameter | Type | Required | Possible values | Description |
|-----------|------|----------|-------|-------------|
| Action | string | yes | "GetToneName" | Indicates that you want to get the friendly name of a tone. |
| ToneToken | string | yes | The tone token | The tone token from which to obtain a display name. |

The values that are returned in [LaunchUriResults.Result](/uwp/api/windows.system.launchuriresult.result):

| Return value | Type | Possible values | Description |
|--------------|------|-------|-------------|
| Result | Int32 | 0-The picker operation succeeded.<br>7-Incorrect parameter (for example, no ToneToken provided).<br>9-Error reading the name for the specified token.<br>10-Unable to find specified tone token. | The result of the picker operation.
| DisplayName | string | The tone's friendly name. | Returns the selected tone's display name. This parameter is only returned in the ValueSet if **Result** is 0. |

**Example: Retrieve a tone token from Contact.RingToneToken and display its friendly name in the contact card.**

```cs
using (var connection = new AppServiceConnection())
{
    connection.AppServiceName = "ms-tonepicker-nameprovider";
    connection.PackageFamilyName = "Microsoft.Tonepicker_8wekyb3d8bbwe";
    AppServiceConnectionStatus connectionStatus = await connection.OpenAsync();
    if (connectionStatus == AppServiceConnectionStatus.Success)
    {
        var message = new ValueSet() {
            { "Action", "GetToneName" },
            { "ToneToken", token)
        };
        AppServiceResponse response = await connection.SendMessageAsync(message);
        if (response.Status == AppServiceResponseStatus.Success)
        {
            Int32 resultCode = (Int32)response.Message["Result"];
            if (resultCode == 0)
            {
                string name = response.Message["DisplayName"] as string;
            }
            else
            {
                // handle failure
            }
        }
        else
        {
            // handle failure
        }
    }
}
```