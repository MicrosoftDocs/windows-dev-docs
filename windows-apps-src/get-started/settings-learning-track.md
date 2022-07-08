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

We’ll look at code to save and load app settings.

## What do you need to know

Use app settings to store configuration data such as user preferences and app state.

The following data types can be used with settings: integers, doubles, floats, chars, strings, Points, DateTimes, and more. You can also store instances of the [ApplicationDataCompositeValue](/uwp/api/Windows.Storage.ApplicationDataCompositeValue) class which is useful when there are multiple settings that should be treated as a unit. For example, a font name and point size for displaying text in the reading pane of your app should be saved/restored as a single unit. This prevents one setting from getting out of sync with the other due to concurrent access, such as between the main program and a background task.

Here are the main APIs you need to know about to save or load app settings:

- [Windows.Storage.ApplicationData.Current.LocalSettings](/uwp/api/Windows.Storage.ApplicationData#Windows_Storage_ApplicationData_LocalSettings) gets the application settings container from the local app data store. Settings stored here are kept on the device.
- [Windows.Storage.ApplicationData.Current.RoamingSettings](/uwp/api/windows.storage.applicationdata.roamingsettings#Windows_Storage_ApplicationData_RoamingSettings) gets the application settings container from the roaming app data store. Settings stored here no longer roam (as of Windows 11), but the settings store is still available. The recommended replacement for RoamingSettings is [Azure App Service](/azure/app-service/). Azure App Service is widely supported, well documented, reliable, and supports cross-platform/cross-ecosystem scenarios such as iOS, Android and web.
- [Windows.Storage.ApplicationDataContainer](/uwp/api/windows.storage.applicationdatacontainer) is a container that represents app settings as key/value pairs. Use this class to create and retrieve setting values.
- [Windows.Storage.ApplicationDataCompositeValue](/uwp/api/Windows.Storage.ApplicationDataCompositeValue) represents multiple app settings that should be serialized as a unit. This is useful when one setting shouldn't be updated independently of another.

## Save app settings

For this introduction, we will focus on two simple scenarios: saving and loading a simple app setting, and saving and loading a composite font/font size setting.

 ```csharp
ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

// Save a setting locally on the device
localSettings.Values["test setting"] = "a device specific setting";

// Save a composite setting locally on the device
Windows.Storage.ApplicationDataCompositeValue composite = new Windows.Storage.ApplicationDataCompositeValue();
composite["Font"] = "Calibri";
composite["FontSize"] = 11;
localSettings.Values["FontInfo"] = composite;
 ```

Save a setting by first getting an **ApplicationDataContainer** for the local settings data store with `Windows.Storage.ApplicationData.Current.LocalSettings`. Key/value dictionary pairs that you assign to this instance are saved in the local device setting data store.

In the code snippet above, an **ApplicationDataCompositeValue** stores multiple key/value pairs. Composite values are useful when you have multiple settings that shouldn't get out of sync with each other. When you save a **ApplicationDataCompositeValue**, the values are saved and loaded as a unit, or atomically. This way settings that are related won't get out of sync.

## Load app settings

```csharp
ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

// load a setting that is local to the device
String localValue = localSettings.Values["test setting"] as string;

// load a composite setting
Windows.Storage.ApplicationDataCompositeValue composite = (ApplicationDataCompositeValue)localSettings.Values["FontInfo"];
if (composite != null)
{
    String fontName = composite["Font"] as string;
    int fontSize = (int)composite["FontSize"];
}
```

Load a setting by first getting an **ApplicationDataContainer** instance for the local settings data store with `Windows.Storage.ApplicationData.Current.LocalSettings`. Then use it to retrieve key/value pairs. If the data is not present, you'll get a null object. In C#, this means that the `localValue` will be `null` if the setting does not exist.

Load a composite setting by following a similar pattern. Access key/value pairs from the local settings data store. If the data is not present, you'll get a null **ApplicationDataContainer**. That's why there is a `if (composite != null)` check in the example code above.

## Useful APIs and docs

Here is a quick summary of APIs, and other useful documentation, to help get you started saving and loading app settings.

### Useful APIs

| API | Description |
|------|---------------|
| [ApplicationData.LocalSettings](/uwp/api/windows.storage.applicationdata.localsettings) | Gets the application settings container from the local app data store. |
| [ApplicationData.RoamingSettings](/uwp/api/windows.storage.applicationdata.roamingsettings) | Gets the application settings container from the roaming app data store. |
| [ApplicationDataContainer](/uwp/api/windows.storage.applicationdatacontainer) | A container for app settings that supports creating, deleting, enumerating, and traversing the container hierarchy. |
| [Windows.UI.ApplicationSettings Namespace](/uwp/api/windows.ui.applicationsettings) | Provides classes that you'll use to define the app settings that appear in the settings pane of the Windows shell. |

### Useful docs

| Topic | Description |
|-------|----------------|
| [Guidelines for app settings](/windows/apps/design/app-settings/guidelines-for-app-settings) | Describes best practices for creating and displaying app settings. |
| [Store and retrieve settings and other app data](/windows/apps/design/app-settings/store-and-retrieve-app-data#create-and-read-a-local-file) | Walk-through for saving and retrieving settings. |

## Useful code samples

| Code sample | Description |
|-----------------|---------------|
| [Application data sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/ApplicationData) | Scenarios 2-4 focus on settings |
