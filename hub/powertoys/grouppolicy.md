---
title: PowerToys Group Policy
description: Group policy documentation for PowerToys
ms.date: 11/12/2023
ms.topic: article
no-loc: [PowerToys, Windows, Group Policy, Win]
---

# Group Policies

Since version 0.64, PowerToys is released on GitHub with Administrative Templates that allows you to configure PowerToys using Group Policies.

## Installing PowerToys using Group Policy

### Download

You can find the latest administrative templates (ADMX files) in the assets section of our newest PowerToys release on <https://github.com/microsoft/PowerToys/releases>. The file is named `GroupPolicyObjectsFiles-<Version>.zip`.

### Adding the administrative template to an individual computer

1. Copy the "PowerToys.admx" file to your Policy Definition template folder. (Example: C:\Windows\PolicyDefinitions)
2. Copy the "PowerToys.adml" file to the matching language folder in your Policy Definition folder. (Example: C:\Windows\PolicyDefinitions\en-US)

### Adding the administrative template to Active Directory

1. On a domain controller or workstation with RSAT, go to the **PolicyDefinition** folder (also known as the *Central Store*) on any domain controller for your domain. For older versions of Windows Server, you might need to create the **PolicyDefinition** folder. For more information, see [How to create and manage the Central Store for Group Policy Administrative Templates in Windows](https://support.microsoft.com/help/3087759/how-to-create-and-manage-the-central-store-for-group-policy-administra).
2. Copy the "PowerToys.admx" file to the PolicyDefinition folder. (Example: %systemroot%\sysvol\domain\policies\PolicyDefinitions)
3. Copy the "PowerToys.adml" file to the matching language folder in the PolicyDefinition folder. Create the folder if it doesn't already exist. (Example: %systemroot%\sysvol\domain\policies\PolicyDefinitions\EN-US)
4. If your domain has more than one domain controller, the new ADMX files will be replicated to them at the next domain replication interval.

### Importing the administrative template in Intune

You can find all instructions on how to import the administrative templates in Intune [here](/mem/intune/configuration/administrative-templates-import-custom#add-the-admx-and-adml-files).

### Scope

You will find the policies under "Administrative Templates/Microsoft PowerToys" in both the Computer Configuration and User Configuration folders. If both settings are configured, the setting in Computer Configuration takes precedence over the setting in User Configuration.

## Policies

<!--Note for every dev who updates this file:
The syntax of OMA-URI is the following: ./Device/Vendor/MSFT/Policy/Config/PowerToys~Policy~PowerToys[[~<category1>]~<categoryN>]/<ADMX-DisplayName-ID>
-->

### Configuring global utility enabled state

> Supported on PowerToys 0.75.0 or later.

This policy configures the enabled state for all PowerToys utilities.

- If you enable this setting, all utilities will be always enabled and the user won't be able to disable it.
- If you disable this setting, all utilities will be always disabled and the user won't be able to enable it.
- If you don't configure this setting, users are able to enable or disable the utilities.

The individual enabled state policies for the utilities will override this policy.

#### Group Policy (ADMX) information

- GP unique name: ConfigureGlobalUtilityEnabledState
- GP name: Configure global utility enabled state
- GP path: Administrative Templates/Microsoft PowerToys
- GP scope: Computer and user
- ADMX file name: PowerToys.admx

#### Registry information

- Path: Software\Policies\PowerToys
- Name: ConfigureGlobalUtilityEnabledState
- Type: DWORD
- Example value: `0x00000000`

#### Intune information

- OMA-URI: `./Device/Vendor/MSFT/Policy/Config/PowerToys~Policy~PowerToys/ConfigureGlobalUtilityEnabledState`
- Example value: `<disabled/>`

### Configuring enabled state for individual utilities

> Supported on PowerToys 0.64.0 or later depending on the utility.

For each utility shipped with PowerToys, there's a "Configure enabled state" policy, which forces an enabled state for the utility.

- If you enable this setting, the utility will be always enabled and the user won't be able to disable it.
- If you disable this setting, the utility will be always disabled and the user won't be able to enable it.
- If you don't configure this setting, users are able to enable or disable the utility.

These policies have a higher priority than the policy "Configure global utility enabled state" and override it.

> [!NOTE]
> PDF file preview: There have been reports of incompatibility between the PDF Preview Handler and Outlook.

#### Table of utility Policies

| Utility | ADMX GP name |ADMX GP unique name /</br>Registry value name /</br>Intune PolicyID |
| :--- | :--- | :--- |
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
|Paste as Plain Text|Paste as Plain Text: Configure enabled state|ConfigureEnabledUtilityPastePlain|
|Peek|Peek: Configure enabled state|ConfigureEnabledUtilityPeek|
|Power Rename|Power Rename: Configure enabled state|ConfigureEnabledUtilityPowerRename|
|PowerToys Run|PowerToys Run: Configure enabled state|ConfigureEnabledUtilityPowerLauncher|
|Quick Accent|Quick Accent: Configure enabled state|ConfigureEnabledUtilityQuickAccent|
|Registry Preview|Registry Preview: Configure enabled state|ConfigureEnabledUtilityRegistryPreview|
|Screen Ruler|Screen Ruler: Configure enabled state|ConfigureEnabledUtilityScreenRuler|
|Shortcut Guide|Shortcut Guide: Configure enabled state|ConfigureEnabledUtilityShortcutGuide|
|Text Extractor|Text Extractor: Configure enabled state|ConfigureEnabledUtilityTextExtractor|
|Video Conference Mute|Video Conference Mute: Configure enabled state|ConfigureEnabledUtilityVideoConferenceMute|

#### Group Policy (ADMX) information

- GP unique name: See the table above.
- GP name: See the table above.
- GP path: Administrative Templates/Microsoft PowerToys
- GP scope: Computer and user
- ADMX file name: PowerToys.admx

#### Registry information

- Path: Software\Policies\PowerToys
- Name: See the table above.
- Type: DWORD
- Example value: `0x00000000`

#### Intune information

- OMA-URI: `./Device/Vendor/MSFT/Policy/Config/PowerToys~Policy~PowerToys/<PolicyID>`

    > [!Note]
    > Please see the table above for the *PolicyID* value.

- Example value: `<disabled/>`

### Allowing experimentation

> Supported on PowerToys 0.68.0 or later.

This policy configures whether PowerToys experimentation is allowed. With experimentation allowed the user sees the new features being experimented if it gets selected as part of the test group. (Experimentation will only happen on Windows Insider builds.)

- If this setting is enabled or not configured, the user can control experimentation in the PowerToys settings menu.
- If this setting is disabled, experimentation is not allowed.

#### Group Policy (ADMX) information

- GP unique name: AllowExperimentation
- GP name: Allow experimentation
- GP path: Administrative Templates/Microsoft PowerToys
- GP scope: Computer and user
- ADMX file name: PowerToys.admx

#### Registry information

- Path: Software\Policies\PowerToys
- Name: AllowExperimentation
- Type: DWORD
- Example value: `0x00000000`

#### Intune information

- OMA-URI: `./Device/Vendor/MSFT/Policy/Config/PowerToys~Policy~PowerToys/AllowExperimentation`
- Example value: `<disabled/>`

### Installer and Updates

#### Disabling per-user installation

> Supported on PowerToys 0.68.0 or later.

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
- ADMX file name: PowerToys.admx

##### Registry information

- Path: HKLM\Software\Policies\PowerToys
- Name: DisablePerUserInstallation
- Type: DWORD
- Example value: `0x00000001`

##### Intune information

- OMA-URI: `./Device/Vendor/MSFT/Policy/Config/PowerToys~Policy~PowerToys~InstallerUpdates/DisablePerUserInstallation`
- Example value: `<enabled/>`

#### Disabling automatic downloads

> Supported on PowerToys 0.68.0 or later.

This policy configures whether automatic downloads of available updates are disabled or not. (On metered connections updates are never downloaded.)

- If enabled, automatic downloads are disabled.
- If disabled or not configured, the user is in control of automatic downloads setting.

##### Group Policy (ADMX) information

- GP unique name: DisableAutomaticUpdateDownload
- GP name: Disable automatic downloads
- GP path: Administrative Templates/Microsoft PowerToys/Installer and Updates
- GP scope: Computer and user
- ADMX file name: PowerToys.admx

##### Registry information

- Path: Software\Policies\PowerToys
- Name: DisableAutomaticUpdateDownload
- Type: DWORD
- Example value: `0x00000001`

##### Intune information

- OMA-URI: `./Device/Vendor/MSFT/Policy/Config/PowerToys~Policy~PowerToys~InstallerUpdates/DisableAutomaticUpdateDownload`
- Example value: `<enabled/>`

#### Suspend Action Center notification for new updates

> Supported on PowerToys 0.68.0 or later.

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
- ADMX file name: PowerToys.admx

##### Registry information

- Path: Software\Policies\PowerToys
- Name: SuspendNewUpdateToast
- Type: DWORD
- Example value: `0x00000001`

##### Intune information

- OMA-URI: `./Device/Vendor/MSFT/Policy/Config/PowerToys~Policy~PowerToys~InstallerUpdates/SuspendNewUpdateToast`
- Example value: `<enabled/>`

<!-- This policy is implemented for later usage (PT v1.0 and later) and therefore inactive. (To make it working please update `src/runner/UpdateUtils.cpp`)
#### Disable automatic update checks

> Supported on PowerToys 0.68.0 or later.

This policy allows you to disable automatic update checks running in the background. (The manual check in PT Settings is not affected by this policy.)

- If enabled, the automatic update checks are disabled.
- If disabled or not configured, the automatic update checks are enabled.

##### Group Policy (ADMX) information

- GP unique name: DisablePeriodicUpdateCheck
- GP name: Disable automatic update checks
- GP path: Administrative Templates/Microsoft PowerToys/Installer and Updates
- GP scope: Computer and user
- ADMX file name: PowerToys.admx

##### Registry information

- Path: Software\Policies\PowerToys
- Name: DisablePeriodicUpdateCheck
- Type: DWORD
- Example value: `0x00000000`

##### Intune information

- OMA-URI: `./Device/Vendor/MSFT/Policy/Config/PowerToys~Policy~PowerToys~InstallerUpdates/DisablePeriodicUpdateCheck`
- Example value: `<enabled/>`
-->

### PowerToys Run

#### Configure enabled state for all plugins

> Supported on PowerToys 0.75.0 or later.

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
- ADMX file name: PowerToys.admx

##### Registry information

- Path: Software\Policies\PowerToys
- Name: PowerToysRunAllPluginsEnabledState
- Type: DWORD
- Example value: `0x00000000`

##### Intune information

- OMA-URI: `./Device/Vendor/MSFT/Policy/Config/PowerToys~Policy~PowerToys~PowerToysRun/PowerToysRunAllPluginsEnabledState`
- Example value: `<disabled/>`

#### Configuring enabled state for individual plugins

> Supported on PowerToys 0.75.0 or later.

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
- ADMX file name: PowerToys.admx

##### Registry information

- Path: Software\Policies\PowerToys\PowerLauncherIndividualPluginEnabledList
- Name: The plugin ID from the `plugin.json` file.
- Type: STRING
- Example value:

    ```
    Software\Policies\PowerToys\0778F0C264114FEC8A3DF59447CF0A74 = 2 (=> User can enable/disable the OneNote plugin.)
    Software\Policies\PowerToys\791FC278BA414111B8D1886DFE447410 = 0 (=> Program plugin force disabled.)
    Software\Policies\PowerToys\CEA0FDFC6D3B4085823D60DC76F28855 = 1 (=> Calculator plugin force enabled.)
    ```

##### Intune information

- OMA-URI: `./Device/Vendor/MSFT/Policy/Config/PowerToys~Policy~PowerToys~PowerToysRun/PowerToysRunIndividualPluginEnabledState`
- Example value:

    > [!NOTE]
    > Syntax for the :::no-loc text="value"::: property from the :::no-loc text="data"::: element:
    > `<PluginID>&#xF000;<Number>&#xF000;<PluginID>&#xF000;<Number>`

    ```
    <enabled/>
    <data id="PowerToysRunIndividualPluginEnabledList" value="0778F0C264114FEC8A3DF59447CF0A74&#xF000;2&#xF000;791FC278BA414111B8D1886DFE447410&#xF000;0&#xF000;CEA0FDFC6D3B4085823D60DC76F28855&#xF000;1"/>
    ```
