---
title: Microsoft Copilot hardware key providers
description: Learn how to register as an app that can be selected as the launch app for the Microsoft Copilot hardware key. 
ms.topic: article
ms.date: 10/25/2024
ms.localizationpriority: medium
---



# Microsoft Copilot hardware key providers

Starting with Windows Build 22621, apps can register to be included in the picker UI that allows users to select the app that is launched when the Microsoft Copilot hardware key or the Windows key + C is pressed.

> [!NOTE]
> It is recommended that apps that register to be a Microsoft Copilot hardware key provider be implemented as single-window apps.

## Microsoft Copilot hardware key app extension

An app must be packaged in order to register as a Microsoft Copilot hardware key provider. For information on app packaging, see [An overview of Package Identity in Windows app](/windows/apps/desktop/modernize/package-identity-overview). The app package manifest file, `Package.appxmanifest`, supports many different extensions and features for Windows apps. The app package manifest format is defined by a set of schemas that are documented in the [Package manifest schema reference](/uwp/schemas/appxpackage/uapmanifestschema/schema-root).  Microsoft Copilot hardware key providers declare their registration information within the [uap3:AppExtension](/uwp/schemas/appxpackage/uapmanifestschema/element-uap3-appextension-manual). The **Name** attribute of the extension must be set to "com.microsoft.windows.copilotkeyprovider".


```xml
<Package
...

  xmlns:uap3="http://schemas.microsoft.com/appx/manifest/uap/windows10/3"
...>
    <Applications>
        <Application...>
            ...
            <Extensions>
                <uap3:Extension Category="windows.appExtension">
                    <uap3:AppExtension Name="com.microsoft.windows.copilotkeyprovider" 
                        Id="MyAppId"
                        DisplayName="App display name"
                        Description="App description"
                        PublicFolder="Public" />
                </uap3:Extension>
            </Extensions>
          ...
    </Application>
    </Applications>
    ...
</Package>
```

The following table *uap3:AppExtension* describes the attributes of the **uap3:AppExtension** element.

| Attribute | Description | Required |
|-----------|-------------|----------|
| Id | The app-defined identifier for the app. | Yes |
| DisplayName | The app name displayed in the Windows Copilot hardware button picker UI.  | Yes |
| Description | The app description displayed in the Windows Copilot hardware button picker UI. | Yes |
| PublicFolder|  The folder that the instance declares as the location where a host can have read access to files through a broker. | Yes | 


## Sign your Windows Copilot hardware key provider

Provider apps must be signed in order to be enabled as a target of the Microsoft Copilot hardware key. For information on packaging and signing your app, see [Package a desktop or UWP app in Visual Studio](/windows/msix/package/packaging-uwp-apps).

## Query for the current Copilot hardware key provider

Check if your app is the user's target for the Copilot hardware key & Windows key + C keyboard shortcut by querying the value of the following registry keys. For more information, see [Retrieving Data from the Registry](/windows/win32/sysinfo/retrieving-data-from-the-registry).

| Registry key | Description | Value |
|--------------|-------------|-------|
| HKEY_CURRENT_USER\Software\Microsoft\Windows\Shell\BrandedKey\BrandedKeyChoiceType | Identifies if the user has selected Search or an app as the target of the Copilot key. The value "AppEnforcedByPolicy" indicates that an app target of the Copilot key has been set by IT administrator policy. For more information, see [Policy CSP - WindowsAI](/windows/client-management/mdm/policy-csp-windowsai#setcopilothardwarekey). | "Search", "App", "AppEnforcedByPolicy" |
| HKEY_CURRENT_USER\Software\Microsoft\Windows\Shell\BrandedKey\AppAumid | Identifies the Application User Model Id (AUMID, also known as AppId) of the Copilot hardware key provider that was last configured, even if the key is currently configured to Search. | An AUMID. |

To provide a good user experience, apps should be respectful of the user's selection for the Copilot hardware key provider app and should not display persistent or noisy requests for the user to change their selection.
