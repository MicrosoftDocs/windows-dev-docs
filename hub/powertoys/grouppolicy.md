---
title: PowerToys Group Policy
description: Group policy documentation for PowerToys
ms.date: 07/06/2024
ms.topic: article
no-loc: [PowerToys, Windows, Group Policy, Win]
---

# Group Policies

Since version 0.64, PowerToys is released on GitHub with Administrative Templates that allows you to configure PowerToys using Group Policies.

## How to install

### Download

You can find the latest administrative templates (ADMX files) in the assets section of our newest PowerToys release on [GitHub](https://github.com/microsoft/PowerToys/releases). The file is named `GroupPolicyObjectsFiles-<Version>.zip`.

### Add the administrative template to an individual computer

1. Copy the _PowerToys.admx_ file to your Policy Definition template folder. (Example: _C:\Windows\PolicyDefinitions_)
2. Copy the _PowerToys.adml_ file to the matching language folder in your Policy Definition folder. (Example: _C:\Windows\PolicyDefinitions\en-US_)

### Add the administrative template to Active Directory

1. On a domain controller or workstation with RSAT, go to the **PolicyDefinition** folder (also known as the _Central Store_) on any domain controller for your domain. For older versions of Windows Server, you might need to create the **PolicyDefinition** folder. For more information, see [How to create and manage the Central Store for Group Policy Administrative Templates in Windows](https://support.microsoft.com/help/3087759/how-to-create-and-manage-the-central-store-for-group-policy-administra).
2. Copy the _PowerToys.admx_ file to the PolicyDefinition folder. (Example: _%systemroot%\sysvol\domain\policies\PolicyDefinitions_)
3. Copy the _PowerToys.adml_ file to the matching language folder in the PolicyDefinition folder. Create the folder if it doesn't already exist. (Example: _%systemroot%\sysvol\domain\policies\PolicyDefinitions\EN-US_)
4. If your domain has more than one domain controller, the new ADMX files will be replicated to them at the next domain replication interval.

### Import the administrative template in Intune

You can find all instructions on how to import the administrative templates in Intune on [this page](/mem/intune/configuration/administrative-templates-import-custom#add-the-admx-and-adml-files).

### Scope

You will find the policies under "Administrative Templates/Microsoft PowerToys" in both the Computer Configuration and User Configuration folders. If both settings are configured, the setting in Computer Configuration takes precedence over the setting in User Configuration.

## Policies

<!--Note for every dev who updates this file:
The syntax of OMA-URI is the following: ./Device/Vendor/MSFT/Policy/Config/PowerToys~Policy~PowerToys[[~<category1>]~<categoryN>]/<ADMX-DisplayName-ID>
-->

### Configure global utility enabled state

Supported on PowerToys 0.75.0 or later.

This policy configures the enabled state for all PowerToys utilities.

- If you enable this setting, all utilities will be always enabled and the user won't be able to disable it.
- If you disable this setting, all utilities will be always disabled and the user won't be able to enable it.
- If you don't configure this setting, users are able to enable or disable the utilities.

The individual enabled state policies for the utilities will override this policy.

#### Group Policy (ADMX) information

- GP unique name: ConfigureAllUtilityGlobalEnabledState
- GP name: Configure global utility enabled state
- GP path: Administrative Templates/Microsoft PowerToys
- GP scope: Computer and user
- ADMX file name: _PowerToys.admx_

#### Registry information

- Path: Software\Policies\PowerToys
- Name: ConfigureGlobalUtilityEnabledState
- Type: DWORD
- Example value: `0x00000000`

#### Intune information

- OMA-URI: `./Device/Vendor/MSFT/Policy/Config/PowerToys~Policy~PowerToys/ConfigureAllUtilityGlobalEnabledState`
- Example value: `<disabled/>`

### Configure enabled state for individual utilities

Supported on PowerToys 0.64.0 or later, depending on the utility.

For each utility shipped with PowerToys, there's a "Configure enabled state" policy, which forces an enabled state for the utility.

- If you enable this setting, the utility will be always enabled and the user won't be able to disable them.
- If you disable this setting, the utility will be always disabled and the user won't be able to enable them.
- If you don't configure this setting, users are able to enable or disable the utility.

These policies have a higher priority than, and will override, the policy "Configure global utility enabled state".

> [!NOTE]
> PDF file preview: There have been reports of incompatibility between the PDF Preview Handler and Outlook.

#### Table of utility Policies

| Utility | ADMX GP name |ADMX GP unique name /</br>Registry value name /</br>Intune PolicyID |
| :--- | :--- | :--- |
|Advanced Paste|Advanced Paste: Configure enabled state|ConfigureEnabledUtilityAdvancedPaste|
|Always On Top|Always On Top: Configure enabled state|ConfigureEnabledUtilityAlwaysOnTop|
|Awake|Awake: Configure enabled state|ConfigureEnabledUtilityAwake|
|Color Picker|Color Picker: Configure enabled state|ConfigureEnabledUtilityColorPicker|
|Command Not Found|Command Not Found: Configure enabled state|ConfigureEnabledUtilityCmdNotFound|
|Crop And Lock|Crop And Lock: Configure enabled state|ConfigureEnabledUtilityCropAndLock|
|Environment Variables|Environment Variables: Configure enabled state|ConfigureEnabledUtilityEnvironmentVariables|
|FancyZones|FancyZones: Configure enabled state|ConfigureEnabledUtilityFancyZones|
|File Locksmith|File Locksmith: Configure enabled state|ConfigureEnabledUtilityFileLocksmith|
|Gcode file preview|Gcode file preview: Configure enabled state|ConfigureEnabledUtilityFileExplorerGcodePreview|
|Markdown file preview|Markdown file preview: Configure enabled state|ConfigureEnabledUtilityFileExplorerMarkdownPreview|
|PDF file preview|PDF file preview: Configure enabled state|ConfigureEnabledUtilityFileExplorerPDFPreview|
|QOI file preview|QOI file preview: Configure enabled state|ConfigureEnabledUtilityFileExplorerQOIPreview|
|Source code file preview|Source code file preview: Configure enabled state|ConfigureEnabledUtilityFileExplorerMonacoPreview|
|SVG file preview|SVG file preview: Configure enabled state|ConfigureEnabledUtilityFileExplorerSVGPreview|
|Gcode file thumbnail|Gcode file thumbnail: Configure enabled state|ConfigureEnabledUtilityFileExplorerGcodeThumbnails|
|PDF file thumbnail|PDF file thumbnail: Configure enabled state|ConfigureEnabledUtilityFileExplorerPDFThumbnails|
|QOI file thumbnail|QOI file thumbnail: Configure enabled state|ConfigureEnabledUtilityFileExplorerQOIThumbnails|
|STL file thumbnail|STL file thumbnail: Configure enabled state|ConfigureEnabledUtilityFileExplorerSTLThumbnails|
|SVG file thumbnail|SVG file thumbnail: Configure enabled state|ConfigureEnabledUtilityFileExplorerSVGThumbnails|
|Hosts file editor|Hosts file editor: Configure enabled state|ConfigureEnabledUtilityHostsFileEditor|
|Image Resizer|Image Resizer: Configure enabled state|ConfigureEnabledUtilityImageResizer|
|Keyboard Manager|Keyboard Manager: Configure enabled state|ConfigureEnabledUtilityKeyboardManager|
|Find My Mouse|Find My Mouse: Configure enabled state|ConfigureEnabledUtilityFindMyMouse|
|Mouse Highlighter|Mouse Highlighter: Configure enabled state|ConfigureEnabledUtilityMouseHighlighter|
|Mouse Jump|Mouse Jump: Configure enabled state|ConfigureEnabledUtilityMouseJump|
|Mouse Pointer Crosshairs|Mouse Pointer Crosshairs: Configure enabled state|ConfigureEnabledUtilityMousePointerCrosshairs|
|Mouse Without Borders|Mouse Without Borders: Configure enabled state|ConfigureEnabledUtilityMouseWithoutBorders|
|New+|New+: Configure enabled state|ConfigureEnabledUtilityNewPlus|
|Peek|Peek: Configure enabled state|ConfigureEnabledUtilityPeek|
|Power Rename|Power Rename: Configure enabled state|ConfigureEnabledUtilityPowerRename|
|PowerToys Run|PowerToys Run: Configure enabled state|ConfigureEnabledUtilityPowerLauncher|
|Quick Accent|Quick Accent: Configure enabled state|ConfigureEnabledUtilityQuickAccent|
|Registry Preview|Registry Preview: Configure enabled state|ConfigureEnabledUtilityRegistryPreview|
|Screen Ruler|Screen Ruler: Configure enabled state|ConfigureEnabledUtilityScreenRuler|
|Shortcut Guide|Shortcut Guide: Configure enabled state|ConfigureEnabledUtilityShortcutGuide|
|Text Extractor|Text Extractor: Configure enabled state|ConfigureEnabledUtilityTextExtractor|
|Video Conference Mute|Video Conference Mute: Configure enabled state|ConfigureEnabledUtilityVideoConferenceMute|
|Workspaces|Workspaces: Configure enabled state|ConfigureEnabledUtilityWorkspaces|

#### Group Policy (ADMX) information

- GP unique name: See the table above.
- GP name: See the table above.
- GP path: Administrative Templates/Microsoft PowerToys
- GP scope: Computer and user
- ADMX file name: _PowerToys.admx_

#### Registry information

- Path: Software\Policies\PowerToys
- Name: See the table above.
- Type: DWORD
- Example value: `0x00000000`

#### Intune information

- OMA-URI: `./Device/Vendor/MSFT/Policy/Config/PowerToys~Policy~PowerToys/<PolicyID>`

> [!Note]
> Please see the table above for the _PolicyID_ value.

- Example value: `<disabled/>`

### General settings

#### Allow experimentation

Supported on PowerToys 0.68.0 or later.

This policy configures whether PowerToys experimentation is allowed. With experimentation allowed the user sees the new features being experimented if it gets selected as part of the test group. Experimentation will only happen on Windows Insider builds.

- If this setting is enabled or not configured, the user can control experimentation in the PowerToys settings menu.
- If this setting is disabled, experimentation is not allowed.

##### Group Policy (ADMX) information

- GP unique name: AllowExperimentation
- GP name: Allow experimentation
- GP path: Administrative Templates/Microsoft PowerToys/General settings
- GP scope: Computer and user
- ADMX file name: _PowerToys.admx_

##### Registry information

- Path: Software\Policies\PowerToys
- Name: AllowExperimentation
- Type: DWORD
- Example value: `0x00000000`

##### Intune information

- OMA-URI: `./Device/Vendor/MSFT/Policy/Config/PowerToys~Policy~PowerToys~GeneralSettings/AllowExperimentation`
- Example value: `<disabled/>`

### Installer and Updates

#### Disable per-user installation

Supported on PowerToys 0.68.0 or later.

This policy configures whether PowerToys per-user installation is allowed or not.

- If enabled, per-user installation is not allowed.
- If disabled or not configured, per-user installation is allowed.

> [!NOTE]
> You can set this policy only as Computer policy.

##### Group Policy (ADMX) information

- GP unique name: DisablePerUserInstallation
- GP name: Disable per-user installation
- GP path: Administrative Templates/Microsoft PowerToys/Installer and Updates
- GP scope: Computer only
- ADMX file name: _PowerToys.admx_

##### Registry information

- Path: HKLM\Software\Policies\PowerToys
- Name: DisablePerUserInstallation
- Type: DWORD
- Example value: `0x00000001`

##### Intune information

- OMA-URI: `./Device/Vendor/MSFT/Policy/Config/PowerToys~Policy~PowerToys~InstallerUpdates/DisablePerUserInstallation`
- Example value: `<enabled/>`

#### Disable automatic downloads

Supported on PowerToys 0.68.0 or later.

This policy configures whether the automatic download and installation of available updates is disabled or not. Updates are never downloaded on metered connections.

- If enabled, automatic download and installation is disabled.
- If disabled or not configured, the user can control this in the settings.

##### Group Policy (ADMX) information

- GP unique name: DisableAutomaticUpdateDownload
- GP name: Disable automatic downloads
- GP path: Administrative Templates/Microsoft PowerToys/Installer and Updates
- GP scope: Computer and user
- ADMX file name: _PowerToys.admx_

##### Registry information

- Path: Software\Policies\PowerToys
- Name: DisableAutomaticUpdateDownload
- Type: DWORD
- Example value: `0x00000001`

##### Intune information

- OMA-URI: `./Device/Vendor/MSFT/Policy/Config/PowerToys~Policy~PowerToys~InstallerUpdates/DisableAutomaticUpdateDownload`
- Example value: `<enabled/>`

#### Suspend Action Center notification for new updates

Supported on PowerToys 0.68.0 or later.

This policy configures whether the action center notification for new updates is suspended for 2 minor releases. (Example: if the installed version is v0.60.0, then the next notification is shown for the v0.63.* release.)

- If enabled, the notification is suspended.
- If disabled or not configured, the notification is shown.

> [!NOTE]
> The notification about new major versions is always displayed.

##### Group Policy (ADMX) information

- GP unique name: SuspendNewUpdateToast
- GP name: Suspend Action Center notification for new updates
- GP path: Administrative Templates/Microsoft PowerToys/Installer and Updates
- GP scope: Computer and user
- ADMX file name: _PowerToys.admx_

##### Registry information

- Path: Software\Policies\PowerToys
- Name: SuspendNewUpdateToast
- Type: DWORD
- Example value: `0x00000001`

##### Intune information

- OMA-URI: `./Device/Vendor/MSFT/Policy/Config/PowerToys~Policy~PowerToys~InstallerUpdates/SuspendNewUpdateToast`
- Example value: `<enabled/>`

#### Disable Action Center notification for new updates

Supported on PowerToys 0.78.0 or later.

This policy configures whether the action center notification for new updates is shown or not.

- If enabled, the notification is disabled.
- If disabled or not configured, the user can control if the notification is shown or not.

##### Group Policy (ADMX) information

- GP unique name: DisableNewUpdateToast
- GP name: Disable Action Center notification for new updates
- GP path: Administrative Templates/Microsoft PowerToys/Installer and Updates
- GP scope: Computer and user
- ADMX file name: _PowerToys.admx_

##### Registry information

- Path: Software\Policies\PowerToys
- Name: DisableNewUpdateToast
- Type: DWORD
- Example value: `0x00000001`

##### Intune information

- OMA-URI: `./Device/Vendor/MSFT/Policy/Config/PowerToys~Policy~PowerToys~InstallerUpdates/DisableNewUpdateToast`
- Example value: `<enabled/>`

#### Do not show the release notes after updates

Supported on PowerToys 0.78.0 or later.

This policy allows you to configure if the window with the release notes is shown after updates.

- If enabled, the window with the release notes is not shown automatically.
- If disabled or not configured, the user can control this in the settings of PowerToys.

##### Group Policy (ADMX) information

- GP unique name: DoNotShowWhatsNewAfterUpdates
- GP name: Disable Action Center notification for new updates
- GP path: Administrative Templates/Microsoft PowerToys/Installer and Updates
- GP scope: Computer and user
- ADMX file name: _PowerToys.admx_

##### Registry information

- Path: Software\Policies\PowerToys
- Name: DoNotShowWhatsNewAfterUpdates
- Type: DWORD
- Example value: `0x00000001`

##### Intune information

- OMA-URI: `./Device/Vendor/MSFT/Policy/Config/PowerToys~Policy~PowerToys~InstallerUpdates/DoNotShowWhatsNewAfterUpdates`
- Example value: `<enabled/>`

### Advanced Paste

#### Allow using online AI models

Supported on PowerToys 0.81.1 or later.

This policy allows you to disable Advanced Paste online AI models.

If you enable or don't configure this policy, the user takes control over the enabled state of the Enable paste with AI Advanced Paste setting.

If you disable this policy, the user won't be able to enable Enable paste with AI Advanced Paste setting and use Advanced Paste AI prompt nor set up the Open AI key in PowerToys Settings.

> [!NOTE]
> Changes require a restart of Advanced Paste.

##### Group Policy (ADMX) information

- GP unique name: AllowPowerToysAdvancedPasteOnlineAIModels
- GP name: Allow using online AI models
- GP path: Administrative Templates/Microsoft PowerToys/Advanced Paste
- GP scope: Computer and user
- ADMX file name: _PowerToys.admx_

##### Registry information

- Path: Software\Policies\PowerToys
- Name: AllowPowerToysAdvancedPasteOnlineAIModels
- Type: DWORD
- Example value: `0x00000000`

##### Intune information

- OMA-URI: `./Device/Vendor/MSFT/Policy/Config/PowerToys~Policy~PowerToys~AdvancedPaste/AllowPowerToysAdvancedPasteOnlineAIModels`
- Example value: `<disabled/>`

### Mouse Without Borders

#### Clipboard sharing enabled

Supported on PowerToys 0.83.0 or later.

This policy configures if the user can share the clipboard between machines.

If you enable or don't configure this policy, the user takes control over the clipboard sharing setting.

If you disable this policy, the user won't be able to enable the clipboard sharing setting.

##### Group Policy (ADMX) information

- GP unique name: MwbClipboardSharingEnabled
- GP name: Clipboard sharing enabled
- GP path: Administrative Templates/Microsoft PowerToys/MouseWithoutBorders
- GP scope: Computer and user
- ADMX file name: _PowerToys.admx_

##### Registry information

- Path: Software\Policies\PowerToys
- Name: MwbClipboardSharingEnabled
- Type: DWORD
- Example value: `0x00000000`

##### Intune information

- OMA-URI: `./Device/Vendor/MSFT/Policy/Config/PowerToys~Policy~PowerToys~MouseWithoutBorders/MwbClipboardSharingEnabled`
- Example value: `<disabled/>`

#### Connect only in same subnet

Supported on PowerToys 0.83.0 or later.

This policy configures if connections are only allowed in the same subnet.

If you enable this policy, the setting is enabled and only connections in the same subnet are allowed.

If you disable this policy, the setting is disabled and all connections are allowed.

If you don't configure this policy, the user takes control over the setting and can enable or disable it.

##### Group Policy (ADMX) information

- GP unique name: MwbSameSubnetOnly
- GP name: Connect only in same subnet
- GP path: Administrative Templates/Microsoft PowerToys/MouseWithoutBorders
- GP scope: Computer and user
- ADMX file name: _PowerToys.admx_

##### Registry information

- Path: Software\Policies\PowerToys
- Name: MwbSameSubnetOnly
- Type: DWORD
- Example value: `0x00000001`

##### Intune information

- OMA-URI: `./Device/Vendor/MSFT/Policy/Config/PowerToys~Policy~PowerToys~MouseWithoutBorders/MwbSameSubnetOnly`
- Example value: `<enabled/>`

#### Disable user defined IP Address mapping rules

Supported on PowerToys 0.83.0 or later.

This policy configures if the user can define IP Address mapping rules.

If you enable this policy, the setting is disabled and the user can't define rules or use existing ones.

If you disable or don't configure this policy, the user takes control over the setting.

Note: Enabling this policy does not prevent policy defined mapping rules from working.

##### Group Policy (ADMX) information

- GP unique name: MwbDisableUserDefinedIpMappingRules
- GP name: Disable user defined IP Address mapping rules
- GP path: Administrative Templates/Microsoft PowerToys/MouseWithoutBorders
- GP scope: Computer and user
- ADMX file name: _PowerToys.admx_

##### Registry information

- Path: Software\Policies\PowerToys
- Name: MwbDisableUserDefinedIpMappingRules
- Type: DWORD
- Example value: `0x00000001`

##### Intune information

- OMA-URI: `./Device/Vendor/MSFT/Policy/Config/PowerToys~Policy~PowerToys~MouseWithoutBorders/MwbDisableUserDefinedIpMappingRules`
- Example value: `<enabled/>`

#### Disallow blocking screensaver on other machines

Supported on PowerToys 0.83.0 or later.

This policy configures if the user is allowed to disable the screensaver on the remote machines.

If you enable this policy, the user won't be able to enable the "block screensaver" screensaver setting and the screensaver is not blocked.

If you disable or don't configure this policy, the user takes control over the setting and can block the screensaver.

##### Group Policy (ADMX) information

- GP unique name: MwbDisallowBlockingScreensaver
- GP name: Disallow blocking screensaver on other machines
- GP path: Administrative Templates/Microsoft PowerToys/MouseWithoutBorders
- GP scope: Computer and user
- ADMX file name: _PowerToys.admx_

##### Registry information

- Path: Software\Policies\PowerToys
- Name: MwbDisallowBlockingScreensaver
- Type: DWORD
- Example value: `0x00000001`

##### Intune information

- OMA-URI: `./Device/Vendor/MSFT/Policy/Config/PowerToys~Policy~PowerToys~MouseWithoutBorders/MwbDisallowBlockingScreensaver`
- Example value: `<enabled/>`

#### File transfer enabled

Supported on PowerToys 0.83.0 or later.

This policy configures if the user can transfer files between machines.

If you enable or don't configure this policy, the user takes control over the file sharing setting.

If you disable this policy, the user won't be able to enable the file sharing Settings.

Note: The file sharing feature depends on the clipboard sharing feature. Disabling clipboard sharing automatically disables file sharing too.

##### Group Policy (ADMX) information

- GP unique name: MwbFileTransferEnabled
- GP name: File transfer enabled
- GP path: Administrative Templates/Microsoft PowerToys/MouseWithoutBorders
- GP scope: Computer and user
- ADMX file name: _PowerToys.admx_

##### Registry information

- Path: Software\Policies\PowerToys
- Name: MwbFileTransferEnabled
- Type: DWORD
- Example value: `0x00000000`

##### Intune information

- OMA-URI: `./Device/Vendor/MSFT/Policy/Config/PowerToys~Policy~PowerToys~MouseWithoutBorders/MwbFileTransferEnabled`
- Example value: `<disabled/>`

#### Original user interface is available

Supported on PowerToys 0.83.0 or later.

This policy configures if the user can use the old Mouse Without Borders user interface.

If you enable or don't configure this policy, the user takes control over the setting and can enable or disable the old user interface.

If you disable this policy, the user won't be able to enable the old user interface.

##### Group Policy (ADMX) information

- GP unique name: MwbUseOriginalUserInterface
- GP name: Original user interface is available
- GP path: Administrative Templates/Microsoft PowerToys/MouseWithoutBorders
- GP scope: Computer and user
- ADMX file name: _PowerToys.admx_

##### Registry information

- Path: Software\Policies\PowerToys
- Name: MwbUseOriginalUserInterface
- Type: DWORD
- Example value: `0x00000000`

##### Intune information

- OMA-URI: `./Device/Vendor/MSFT/Policy/Config/PowerToys~Policy~PowerToys~MouseWithoutBorders/MwbUseOriginalUserInterface`
- Example value: `<disabled/>`

#### Validate remote machine IP Address

Supported on PowerToys 0.83.0 or later.

This policy configures if reverse DNS lookup is used to validate the remote machine IP Address.

If you enable this policy, the setting is enabled and the IP Address is validated.

If you disable this policy, the setting is disabled and the IP Address is not validated.

If you don't configure this policy, the user takes control over the setting and can enable or disable it.

##### Group Policy (ADMX) information

- GP unique name: MwbValidateRemoteIp
- GP name: Validate remote machine IP Address
- GP path: Administrative Templates/Microsoft PowerToys/MouseWithoutBorders
- GP scope: Computer and user
- ADMX file name: _PowerToys.admx_

##### Registry information

- Path: Software\Policies\PowerToys
- Name: MwbValidateRemoteIp
- Type: DWORD
- Example value: `0x00000001`

##### Intune information

- OMA-URI: `./Device/Vendor/MSFT/Policy/Config/PowerToys~Policy~PowerToys~MouseWithoutBorders/MwbValidateRemoteIp`
- Example value: `<enabled/>`

#### Predefined IP Address mapping rules

Supported on PowerToys 0.83.0 or later.

This policy allows you to define IP Address mapping rules.

If you enable this policy, you can define IP Address mapping rules that the user can't change or disable.
Please enter one mapping per line in the format: "hostname IP"

If you disable or don't configure this policy, no predefined rules are applied.

##### Group Policy (ADMX) information

- GP unique name: MwbPolicyDefinedIpMappingRules
- GP name: Predefined IP Address mapping rules
- GP path: Administrative Templates/Microsoft PowerToys/MouseWithoutBorders
- GP scope: Computer and user
- ADMX file name: _PowerToys.admx_

##### Registry information

- Path: Software\Policies\PowerToys
- Name: MwbPolicyDefinedIpMappingRules
- Type: MULTI_SZ
- Example value:

    ```
    Host1 192.0.2.1
    Host2 192.0.2.2
    Host3 192.0.2.3
    ```

##### Intune information

- OMA-URI: `./Device/Vendor/MSFT/Policy/Config/PowerToys~Policy~PowerToys~MouseWithoutBorders/MwbPolicyDefinedIpMappingRules`
- Example value:

    ```
    <enabled/>
    <data id="MwbPolicyDefinedIpMappingsList" value="Host1 192.0.2.1&#xF000Host2 192.0.2.2&#xF000Host3 192.0.2.3"/>
    ```
    > [!NOTE]
    > Syntax for the :::no-loc text="value"::: property from the :::no-loc text="data"::: element:
    > `<Hostname> <IP Address>&#xF000;<Hostname 2> <IP Address 2>&#xF000;<Hostname 3> <IP Address 3>`

### New+

#### Hide template filename extension

Supported on PowerToys 0.85.0 or later.

This policy configures if the template filenames are shown with extension or not.

If you enable this policy, the setting is enabled and the extension is hidden.

If you disable this policy, the setting is disabled and the extension is shown.

If you don't configure this policy, the user takes control over the setting and can enable or disable it.

##### Group Policy (ADMX) information

- GP unique name: NewPlusHideTemplateFilenameExtension
- GP name: Hide template filename extension
- GP path: Administrative Templates/Microsoft PowerToys/New+
- GP scope: Computer and user
- ADMX file name: _PowerToys.admx_

##### Registry information

- Path: Software\Policies\PowerToys
- Name: NewPlusHideTemplateFilenameExtension
- Type: DWORD
- Example value: `0x00000000`

##### Intune information

- OMA-URI: `./Device/Vendor/MSFT/Policy/Config/PowerToys~Policy~PowerToys~NewPlus/NewPlusHideTemplateFilenameExtension`
- Example value: `<disabled/>`

### PowerToys Run

#### Configure enabled state for all plugins

Supported on PowerToys 0.75.0 or later.

This policy configures the enabled state for all PowerToys Run plugins. All plugins will have the same state.

- If you enable this setting, the plugins will be always enabled and the user won't be able to disable it.
- If you disable this setting, the plugins will be always disabled and the user won't be able to enable it.
- If you don't configure this setting, users are able to enable or disable the plugins.

You can override this policy for individual plugins using the policy "Configure enabled state for individual plugins".

> [!NOTE]
> Changes require a restart of PowerToys Run.

##### Group Policy (ADMX) information

- GP unique name: PowerToysRunAllPluginsEnabledState
- GP name: Configure enabled state for all plugins
- GP path: Administrative Templates/Microsoft PowerToys/PowerToys Run
- GP scope: Computer and user
- ADMX file name: _PowerToys.admx_

##### Registry information

- Path: Software\Policies\PowerToys
- Name: PowerToysRunAllPluginsEnabledState
- Type: DWORD
- Example value: `0x00000000`

##### Intune information

- OMA-URI: `./Device/Vendor/MSFT/Policy/Config/PowerToys~Policy~PowerToys~PowerToysRun/PowerToysRunAllPluginsEnabledState`
- Example value: `<disabled/>`

#### Configure enabled state for individual plugins

Supported on PowerToys 0.75.0 or later.

With this policy you can configure an individual enabled state for each PowerToys Run plugin that you add to the list.

If you enable this setting, you can define the list of plugins and their enabled states:

- The value name (first column) is the plugin ID. You will find it in the `plugin.json` file which is located in the plugin folder.
- The value (second column) is a numeric value: 0 for disabled, 1 for enabled and 2 for user takes control.
- Example to disable the Program plugin: `791FC278BA414111B8D1886DFE447410 | 0`

If you disable or don't configure this policy, either the user or the policy "Configure enabled state for all plugins" takes control over the enabled state of the plugins.

You can set the enabled state for all plugins not controlled by this policy using the policy "Configure enabled state for all plugins".

> [!NOTE]
> Changes require a restart of PowerToys Run.

##### Group Policy (ADMX) information

- GP unique name: PowerToysRunIndividualPluginEnabledState
- GP name: Configure enabled state for individual plugins
- GP path: Administrative Templates/Microsoft PowerToys/PowerToys Run
- GP scope: Computer and user
- ADMX file name: _PowerToys.admx_

##### Registry information

- Path: Software\Policies\PowerToys\PowerLauncherIndividualPluginEnabledList
- Name: The plugin ID from the `plugin.json` file.
- Type: STRING
- Example values:

    ```
    Software\Policies\PowerToys\0778F0C264114FEC8A3DF59447CF0A74 = 2 (=> User can enable/disable the OneNote plugin.)
    Software\Policies\PowerToys\791FC278BA414111B8D1886DFE447410 = 0 (=> Program plugin force disabled.)
    Software\Policies\PowerToys\CEA0FDFC6D3B4085823D60DC76F28855 = 1 (=> Calculator plugin force enabled.)
    ```

##### Intune information

- OMA-URI: `./Device/Vendor/MSFT/Policy/Config/PowerToys~Policy~PowerToys~PowerToysRun/PowerToysRunIndividualPluginEnabledState`
- Example value:

    ```
    <enabled/>
    <data id="PowerToysRunIndividualPluginEnabledList" value="0778F0C264114FEC8A3DF59447CF0A74&#xF000;2&#xF000;791FC278BA414111B8D1886DFE447410&#xF000;0&#xF000;CEA0FDFC6D3B4085823D60DC76F28855&#xF000;1"/>
    ```
    > [!NOTE]
    > Syntax for the :::no-loc text="value"::: property from the :::no-loc text="data"::: element:
    > `<PluginID>&#xF000;<Number>&#xF000;<PluginID>&#xF000;<Number>`
