---
title: Save and load settings in a UWP app
description: Learn how to save and load app settings in Universal Windows Platform apps.
ms.date: 05/07/2018
ms.topic: article
keywords: get started, uwp, windows 10, learning track, settings, save settings, load settings
ms.localizationpriority: medium
ms.custom: RS5
---
# Save and load settings in a UWP app

This topic covers what you need to know to get started loading, and saving, settings in a Universal Windows Platform (UWP) app. The main APIs are introduced, and links are provided to help you learn more.

Use settings to remember the user-customizable aspects of your app. For example, a news reader could use app settings to save which news sources to display and what font to use for reading articles.

We’ll look at code to save and load app settings, including local and roaming settings.

## What do you need to know

Use app settings to store configuration data such as user preferences and app state.  Settings that are specific to the device are stored locally. Settings that apply on whichever device your app is installed on are stored in the roaming data store. Settings are roamed between devices on which the user is signed in with the same Microsoft Account and have the same version of the app installed.

The following data types can be used with settings: integers, doubles, floats, chars, strings, Points, DateTimes, and more. You can also store instances of the [ApplicationDataCompositeValue](/uwp/api/Windows.Storage.ApplicationDataCompositeValue) class which is useful when there are multiple settings that should be treated as a unit. For example, a font name and point size for displaying text in the reading pane of your app should be saved/restored as a single unit. This prevents one setting from getting out of sync with the other due to delays in roaming one setting before the other.

Here are the main APIs you need to know about to save or load app settings:

- [Windows.Storage.ApplicationData.Current.LocalSettings](/uwp/api/Windows.Storage.ApplicationData#Windows_Storage_ApplicationData_LocalSettings) gets the application settings container from the local app data store. Store settings here that are not appropriate to roam between devices because they represent state specific to this device, or are too big.
- [Windows.Storage.ApplicationData.Current.RoamingSettings](/uwp/api/windows.storage.applicationdata.roamingsettings#Windows_Storage_ApplicationData_RoamingSettings) gets the application settings container from the roaming app data store. This data roams between devices.
- [Windows.Storage.ApplicationDataContainer](/uwp/api/windows.storage.applicationdatacontainer) is a container that represents app settings as key/value pairs. Use this class to create and retrieve setting values.
- [Windows.Storage.ApplicationDataCompositeValue](/uwp/api/Windows.Storage.ApplicationDataCompositeValue) represents multiple app settings that should be serialized as a unit. This is useful when one setting shouldn't be updated independently of another.

## Save app settings

For this introduction, we will focus on two simple scenarios: saving and loading an app setting locally, and roaming a composite font/font size setting between devices.

 ```csharp
// Save a setting locally on the device
ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
localSettings.Values["test setting"] = "a device specific setting";

// Save a composite setting that will be roamed between devices
ApplicationDataContainer roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
Windows.Storage.ApplicationDataCompositeValue composite = new Windows.Storage.ApplicationDataCompositeValue();
composite["Font"] = "Calibri";
composite["FontSize"] = 11;
roamingSettings.Values["RoamingFontInfo"] = composite;
 ```

Save a setting to the local device, by first getting an **ApplicationDataContainer** for the local settings data store with `Windows.Storage.ApplicationData.Current.LocalSettings`. Key/value dictionary pairs that you assign to this instance are saved in the local device setting data store.

Save a roaming setting using a similar pattern. First get an **ApplicationDataContainer** for the roaming settings data store with `Windows.Storage.ApplicationData.Current.RoamingSettings`. Then assign key/value pairs to this instance.  Those key/value pairs will automatically roam between devices.

In the code snippet above, a  **ApplicationDataCompositeValue** stores multiple key/value pairs. Composite values are useful when you have multiple settings that shouldn't get out of sync with each other. When you save a **ApplicationDataCompositeValue**, the values are saved and loaded as a unit, or atomically. This way settings that are related won't get out of sync because they are roamed as a unit instead of individually.

## Load app settings

```csharp
// load a setting that is local to the device
ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
String localValue = localSettings.Values["test setting"] as string;

// load a composite setting that roams between devices
ApplicationDataContainer roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
Windows.Storage.ApplicationDataCompositeValue composite = (ApplicationDataCompositeValue)roamingSettings.Values["RoamingFontInfo"];
if (composite != null)
{
    String fontName = composite["Font"] as string;
    int fontSize = (int)composite["FontSize"];
}
```

Load a setting from the local device, by first getting an **ApplicationDataContainer** instance for the local settings data store with `Windows.Storage.ApplicationData.Current.LocalSettings`. Then use it to retrieve key/value pairs.

Load a roaming setting by following a similar pattern. First get an **ApplicationDataContainer** instance from the roaming settings data store with `Windows.Storage.ApplicationData.Current.RoamingSettings`. Access key/value pairs from that instance. If the data hasn't roamed yet to the device that you are accessing the settings from, you'll get a null **ApplicationDataContainer**. That's why there is a `if (composite != null)` check in the example code above.

## Useful APIs and docs

Here is a quick summary of APIs, and other useful documentation, to help get you started saving and loading app settings.

### Useful APIs

| API | Description |
|------|---------------|
| [ApplicationData.LocalSettings](/uwp/api/windows.storage.applicationdata.temporaryfolder) | Gets the application settings container from the local app data store. |
| [ApplicationData.RoamingSettings](/uwp/api/windows.storage.applicationdata.roamingsettings) | Gets the application settings container from the roaming app data store. |
| [ApplicationDataContainer](/uwp/api/windows.storage.applicationdatacontainer) | A container for app settings that supports creating, deleting, enumerating, and traversing the container hierarchy. |
| [Windows.UI.ApplicationSettings Namespace](/uwp/api/windows.ui.applicationsettings) | Provides classes that you'll use to define the app settings that appear in the settings pane of the Windows shell. |

### Useful docs

| Topic | Description |
|-------|----------------|
| [Guidelines for app settings](../design/app-settings/guidelines-for-app-settings.md) | Describes best practices for creating and displaying app settings. |
| [Store and retrieve settings and other app data](../design/app-settings/store-and-retrieve-app-data.md#create-and-read-a-local-file) | Walk-through for saving and retrieving settings, including roaming settings. |

## Useful code samples

| Code sample | Description |
|-----------------|---------------|
| [Application data sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/ApplicationData) | Scenarios 2-4 focus on settings |