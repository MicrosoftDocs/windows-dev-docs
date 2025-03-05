---
title: PowerToys DSC Configure module
description: Desired State Configuration module documentation for PowerToys
ms.date: 02/27/2025
ms.topic: article
no-loc: [PowerToys, Windows, DSC, Win]
---

# Desired State Configuration

Since version 0.80, the PowerToys installer has been released on GitHub with `Microsoft.PowerToys.Configure` [DSC resource](/powershell/dsc/overview) that allows you to configure PowerToys using a [Winget configuration file](/windows/package-manager/configuration/create).

## Installation

### Prerequisites

- PSDesiredStateConfiguration 2.0.7 or later: Refer to the [PowerShell DSC documentation](/powershell/dsc/overview) for installation instructions.
- PowerShell 7.2 or higher.
- WinGet [version v1.6.2631 or later](https://github.com/microsoft/winget-cli/releases).

### Download

Microsoft.PowerToys.Configure is [installed with PowerToys](install.md). Depending on the installer type, it's installed as follows:

- For the per-user install scope, the module is located in `%USERPROFILE%\Documents\PowerShell\Modules\Microsoft.PowerToys.Configure`.
- For the machine-wide install scope, it's found in `%ProgramFiles%\WindowsPowerShell\Modules\Microsoft.PowerToys.Configure`.

## Usage

You can invoke the resource directly using the following Powershell syntax:

```ps
Invoke-DscResource -Name PowerToysConfigure -Method Set -ModuleName Microsoft.PowerToys.Configure -Property @{ Awake = @{ Enabled = $false; Mode = "TIMED"; IntervalMinutes = "10" } }
```

However, creating a _configuration.dsc.yaml_ file that contains the required settings in a simpler format is more convenient. Here's an example:

```yaml
properties:
  resources:
    - resource: Microsoft.WinGet.DSC/WinGetPackage
      id: installPowerToys
      directives:
        description: Install PowerToys
        allowPrerelease: true
      settings:
        id: Microsoft.PowerToys
        source: winget

    - resource: Microsoft.PowerToys.Configure/PowerToysConfigure
      dependsOn:
        - installPowerToys
      directives:
        description: Configure PowerToys
      settings:
        ShortcutGuide:
          Enabled: false
          OverlayOpacity: 50
        FancyZones:
          Enabled: true
          FancyzonesEditorHotkey: "Shift+Ctrl+Alt+F"
        FileLocksmith:
          Enabled: false
  configurationVersion: 0.2.0
```

Use the following command to apply the configuration from the file:

```ps
winget configure .\configuration.dsc.yaml
```

This command installs the latest version of PowerToys and uses the PowerToysConfigure resource to apply settings for multiple PowerToys modules. More examples can be found [in the PowerToys repo](https://github.com/microsoft/PowerToys/tree/main/src/dsc/Microsoft.PowerToys.Configure/examples).

## Available Configuration Settings by Module

### AlwaysOnTop

| Name | Type | Description | Available |
| :--- | :--- | :--- | :--- |
| Enabled | Boolean | The enabled state for this utility. | ✅ Available |
| Hotkey | KeyboardKeys | Customize the shortcut to pin or unpin an app window. | ✅ Available |
| FrameEnabled | Boolean | Show a border around the pinned window. | ✅ Available |
| FrameThickness | Int | Border thickness in pixels. | ✅ Available |
| FrameColor | String | Specify a color in a `#FFFFFFFF` format. | ✅ Available |
| FrameOpacity | Int | Border opacity in percentage. | ✅ Available |
| FrameAccentColor | Boolean | Use a custom FrameColor value. | ✅ Available |
| SoundEnabled | Boolean | Play a sound when pinning a window. | ✅ Available |
| DoNotActivateOnGameMode | Boolean | Disable activation shortcut when Game Mode is on. | ✅ Available |
| ExcludedApps | String | '\r'-separated list of executable names to exclude from pinning on top. | ✅ Available |
| RoundCornersEnabled | Boolean | Enable round corners. | ✅ Available |

### Awake

| Name | Type | Description | Available |
| :--- | :--- | :--- | :--- |
| Enabled | Boolean | The enabled state for this utility. | ✅ Available |
| KeepDisplayOn | Boolean | This setting is only available when keeping the PC awake. | ✅ Available |
| Mode | AwakeMode | Possible values: PASSIVE, INDEFINITE, TIMED, EXPIRABLE.  | ✅ Available |
| IntervalHours | UInt32 | When using TIMED mode, specifies the number of hours. | ✅ Available |
| IntervalMinutes | UInt32 | When using TIMED mode, specifies the number of minutes. | ✅ Available |
| ExpirationDateTime | DateTimeOffset | When using EXPIRABLE mode, specifies the date and time in a format parsable with `DateTimeOffset.TryParse`. | ✅ Available |

### ColorPicker

| Name | Type | Description | Available |
| :--- | :--- | :--- | :--- |
| Enabled | Boolean | The enabled state for this utility. | ✅ Available |
| ActivationShortcut | HotkeySettings | Customize the shortcut to activate this module. | ✅ Available |
| CopiedColorRepresentation | String | The default color representation to be used. Example :"HEX". | ✅ Available |
| ActivationAction | ColorPickerActivationAction | Possible values: OpenEditor, OpenColorPickerAndThenEditor, OpenOnlyColorPicker. | ✅ Available |
| VisibleColorFormats | — | — | ❌ Not available |
| ShowColorName | Boolean | This will show the name of the color when picking a color. | ✅ Available |

> [!NOTE]
> Configuring custom color formats through DSC is not yet supported.

### CropAndLock

| Name | Type | Description | Available |
| :--- | :--- | :--- | :--- |
| Enabled | Boolean | The enabled state for this utility. | ✅ Available |
| ReparentHotkey | KeyboardKeys | Shortcut to crop an application's window into a cropped window. | ✅ Available |
| ThumbnailHotkey | KeyboardKeys | Shortcut to crop and create a thumbnail of another window. | ✅ Available |

### EnvironmentVariables

| Name | Type | Description | Available |
| :--- | :--- | :--- | :--- |
| Enabled | Boolean | The enabled state for this utility. | ✅ Available |
| LaunchAdministrator | Boolean | Needs to be launched as administrator in order to make changes to the system environment variables. | ✅ Available |

### FancyZones

| Name | Type | Description | Available |
| :--- | :--- | :--- | :--- |
| Enabled | Boolean | The enabled state for this utility. | ✅ Available |
| FancyzonesShiftDrag | Boolean | Hold Shift key to activate zones while dragging a window. | ✅ Available |
| FancyzonesMouseSwitch | Boolean | Use a non-primary mouse button to toggle zone activation. | ✅ Available |
| FancyzonesMouseMiddleClickSpanningMultipleZones | Boolean | Use middle-click mouse button to toggle multiple zones spanning. | ✅ Available |
| FancyzonesOverrideSnapHotkeys | Boolean | This overrides the Windows Snap shortcut (Win + arrow) to move windows between zones. | ✅ Available |
| FancyzonesMoveWindowsAcrossMonitors | Boolean | Move windows between zones across all monitors. | ✅ Available |
| FancyzonesMoveWindowsBasedOnPosition | Boolean | Move windows based on relative position or zone index. | ✅ Available |
| FancyzonesOverlappingZonesAlgorithm | Int | When multiple zones overlap algorithm index. | ✅ Available |
| FancyzonesDisplayOrWorkAreaChangeMoveWindows | Boolean | Keep windows in their zones when the screen resolution or work area changes. | ✅ Available |
| FancyzonesZoneSetChangeMoveWindows | Boolean | During zone layout changes, windows assigned to a zone will match new size/positions. | ✅ Available |
| FancyzonesAppLastZoneMoveWindows | Boolean | Move newly created windows to their last known zone. | ✅ Available |
| FancyzonesOpenWindowOnActiveMonitor | Boolean | Move newly created windows to the current active monitor (Experimental). | ✅ Available |
| FancyzonesRestoreSize | Boolean | Restore the original size of windows when unsnapping. | ✅ Available |
| FancyzonesQuickLayoutSwitch | Boolean | Enable quick layout switch. | ✅ Available |
| FancyzonesFlashZonesOnQuickSwitch | Boolean | Flash zones when switching layout. | ✅ Available |
| UseCursorposEditorStartupscreen | Boolean | Open editor on the display where the mouse point is. | ✅ Available |
| FancyzonesShowOnAllMonitors | Boolean | Show zones on all monitors while dragging a window. | ✅ Available |
| FancyzonesSpanZonesAcrossMonitors | Boolean | Allow zones to span across monitors. | ✅ Available |
| FancyzonesMakeDraggedWindowTransparent | Boolean | Make dragged window transparent. | ✅ Available |
| FancyzonesAllowChildWindowSnap | Boolean | Allow child windows snapping. | ✅ Available |
| FancyzonesDisableRoundCornersOnSnap | Boolean | Disable round corners when window is snapped. | ✅ Available |
| FancyzonesZoneHighlightColor | String | If not using FancyzonesSystemTheme, highlight color to use in `#FFFFFFFF` format. | ✅ Available |
| FancyzonesHighlightOpacity | Int | Zone opacity in percentage. | ✅ Available |
| FancyzonesEditorHotkey | KeyboardKeys | Customize the shortcut to activate this module. | ✅ Available |
| FancyzonesWindowSwitching | Boolean | Switch between windows in the current zone. | ✅ Available |
| FancyzonesNextTabHotkey | KeyboardKeys | Next window shortcut. | ✅ Available |
| FancyzonesPrevTabHotkey | KeyboardKeys | Previous window shortcut. | ✅ Available |
| FancyzonesExcludedApps | String | '\r'-separated list of executable names to exclude from snapping. | ✅ Available |
| FancyzonesBorderColor | String | If not using FancyzonesSystemTheme, border color to use in `#FFFFFFFF` format. | ✅ Available |
| FancyzonesInActiveColor | String | If not using FancyzonesSystemTheme, inactive color to use in `#FFFFFFFF` format. | ✅ Available |
| FancyzonesNumberColor | String | If not using FancyzonesSystemTheme, number color to use in `#FFFFFFFF` format. | ✅ Available |
| FancyzonesSystemTheme | Boolean | Use system theme for zone appearance. | ✅ Available |
| FancyzonesShowZoneNumber | Boolean | Show zone number. | ✅ Available |

> [!NOTE]
> Configuring layouts through DSC is not yet supported.

### FileLocksmith

| Name | Type | Description | Available |
| :--- | :--- | :--- | :--- |
| Enabled | Boolean | The enabled state for this utility. | ✅ Available |
| ExtendedContextMenuOnly | Boolean | Show File Locksmith in extended context menu only or in default context menu as well. | ✅ Available |

### FindMyMouse

| Name | Type | Description | Available |
| :--- | :--- | :--- | :--- |
| Enabled | Boolean | The enabled state for this utility. | ✅ Available |
| ActivationMethod | Int | Activation method index. | ✅ Available |
| ActivationShortcut | HotkeySettings | Custom activation shortcut when using Custom for ActivationMethod. | ✅ Available |
| DoNotActivateOnGameMode | Boolean | Disable activation shortcut when Game Mode is on. | ✅ Available |
| BackgroundColor | String | Background color in `#FFFFFFFF` format. | ✅ Available |
| SpotlightColor | String | Spotlight color in `#FFFFFFFF` format. | ✅ Available |
| OverlayOpacity | Int | Overlay opacity in percentage. | ✅ Available |
| SpotlightRadius | Int | Spotlight radius in px. | ✅ Available |
| AnimationDurationMs | Int | Animation duration in milliseconds. | ✅ Available |
| SpotlightInitialZoom | Int | Spotlight zoom factor at animation start. | ✅ Available |
| ExcludedApps | String | '\r'-separated list of executable names to prevent module activation. | ✅ Available |
| ShakingMinimumDistance | Int | When using shake mouse ActivationMethod, the minimum distance for mouse shaking activation, for adjusting sensitivity. | ✅ Available |
| ShakingIntervalMs | Int | When using shake mouse ActivationMethod, the span of time during which we track mouse movement to detect shaking, for adjusting sensitivity. | ✅ Available |
| ShakingFactor | Int | When using shake mouse ActivationMethod, Shake factor in percentage. | ✅ Available |

### Hosts

| Name | Type | Description | Available |
| :--- | :--- | :--- | :--- |
| Enabled | Boolean | The enabled state for this utility. | ✅ Available |
| LaunchAdministrator | Boolean | Needs to be opened as administrator in order to make changes to the system environment variables. | ✅ Available |
| ShowStartupWarning | Boolean | Show a warning at startup. | ✅ Available |
| LoopbackDuplicates | Boolean | Consider loopback addresses as duplicates. | ✅ Available |
| AdditionalLinesPosition | HostsAdditionalLinesPosition | Possible values: Top, Bottom. | ✅ Available |
| Encoding | HostsEncoding | Possible values: Utf8, Utf8Bom.  | ✅ Available |

### ImageResizer

| Name | Type | Description | Available |
| :--- | :--- | :--- | :--- |
| Enabled | Boolean | The enabled state for this utility. | ✅ Available |
| ImageresizerSelectedSizeIndex | Int | Default size preset index. | ✅ Available |
| ImageresizerShrinkOnly | Boolean | Make pictures smaller but not larger. | ✅ Available |
| ImageresizerReplace | Boolean | Overwrite files. | ✅ Available |
| ImageresizerIgnoreOrientation | Boolean | Ignore the orientation of pictures. | ✅ Available |
| ImageresizerJpegQualityLevel | Int | JPEG quality level in percentage. | ✅ Available |
| ImageresizerPngInterlaceOption | Int | PNG interlacing option index. | ✅ Available |
| ImageresizerTiffCompressOption | Int | Tiff compression index. | ✅ Available |
| ImageresizerFileName | String | This format is used as the filename for resized images.  | ✅ Available |
| ImageresizerSizes | — | — | ❌ Not available |
| ImageresizerKeepDateModified | Boolean | Remove metadata that doesn't affect rendering. | ✅ Available |
| ImageresizerFallbackEncoder | String | Fallback encoder to use. | ✅ Available |
| ImageresizerCustomSize | — | — | ❌ Not available |

> [!NOTE]
> Configuring custom sizes through DSC is not yet supported.

### KeyboardManager

| Name | Type | Description | Available |
| :--- | :--- | :--- | :--- |
| Enabled | Boolean | The enabled state for this utility. | ✅ Available |
| ActiveConfiguration | — | — | ❌ Not available |
| KeyboardConfigurations | — | — | ❌ Not available |

> [!NOTE]
> Configuring remappings through DSC is not yet supported.

### MeasureTool

Measure Tool is the internal name for Screen Ruler.

| Name | Type | Description | Available |
| :--- | :--- | :--- | :--- |
| Enabled | Boolean | The enabled state for this utility. | ✅ Available |
| ActivationShortcut | HotkeySettings | Customize the shortcut to bring up the command bar. | ✅ Available |
| ContinuousCapture | Boolean | Capture screen continuously during measuring. | ✅ Available |
| DrawFeetOnCross | Boolean | Adds feet to the end of cross lines. | ✅ Available |
| PerColorChannelEdgeDetection | Boolean | Enable a different edge detection algorithm. | ✅ Available |
| PixelTolerance | Int | Pixel Tolerance for edge detection. | ✅ Available |
| MeasureCrossColor | String | Line color in `#FFFFFFFF` format. | ✅ Available |
| DefaultMeasureStyle | Int | Default measure style index. | ✅ Available |

### MouseHighlighter

| Name | Type | Description | Available |
| :--- | :--- | :--- | :--- |
| Enabled | Boolean | The enabled state for this utility. | ✅ Available |
| ActivationShortcut | HotkeySettings | Customize the shortcut to turn on or off this mode. | ✅ Available |
| LeftButtonClickColor | String | Primary button highlight color in `#FFFFFFFF` format. | ✅ Available |
| RightButtonClickColor | String | Secondary button highlight color in `#FFFFFFFF` format. | ✅ Available |
| AlwaysColor | String | Always highlight color in `#FFFFFFFF` format. | ✅ Available |
| HighlightRadius | Int | Highlight radius in pixels. | ✅ Available |
| HighlightFadeDelayMs | Int | Fade delay in milliseconds. | ✅ Available |
| HighlightFadeDurationMs | Int | Fade duration in milliseconds. | ✅ Available |
| AutoActivate | Boolean | Automatically activate on utility startup. | ✅ Available |

### MouseJump

| Name | Type | Description | Available |
| :--- | :--- | :--- | :--- |
| Enabled | Boolean | The enabled state for this utility. | ✅ Available |
| ActivationShortcut | HotkeySettings | Customize the shortcut to turn on or off this mode. | ✅ Available |
| ThumbnailSize | MouseJumpThumbnailSize | Thumbnail size. | ✅ Available |

### MousePointerCrosshairs

| Name | Type | Description | Available |
| :--- | :--- | :--- | :--- |
| Enabled | Boolean | The enabled state for this utility. | ✅ Available |
| ActivationShortcut | HotkeySettings | Customize the shortcut to show/hide the crosshairs. | ✅ Available |
| CrosshairsColor | String | Crosshairs color in `#FFFFFFFF`. | ✅ Available |
| CrosshairsOpacity | Int | Crosshairs opacity in percentage. | ✅ Available |
| CrosshairsRadius | Int | Crosshairs center radius in pixels. | ✅ Available |
| CrosshairsThickness | Int | Crosshairs thickness in pixels. | ✅ Available |
| CrosshairsBorderColor | String | Crosshairs border color in `#FFFFFFFF` format. | ✅ Available |
| CrosshairsBorderSize | Int | Crosshairs border size in pixels. | ✅ Available |
| CrosshairsAutoHide | Boolean | Automatically hide crosshairs when the mouse pointer is hidden. | ✅ Available |
| CrosshairsIsFixedLengthEnabled | Boolean | Fix crosshairs length. | ✅ Available |
| CrosshairsFixedLength | Int | Crosshairs fixed length in pixels. | ✅ Available |
| AutoActivate | Boolean | Automatically activate on utility startup. | ✅ Available |

### MouseWithoutBorders

| Name | Type | Description | Available |
| :--- | :--- | :--- | :--- |
| Enabled | Boolean | The enabled state for this utility. | ✅ Available |
| ShowOriginalUI | Boolean | Show the original Mouse Without Borders UI. | ✅ Available |
| WrapMouse | Boolean | Move control back to the first machine when mouse moves past the last one. | ✅ Available |
| ShareClipboard | Boolean | If share clipboard stops working, Ctrl+Alt+Del then Esc may solve the problem. | ✅ Available |
| TransferFile | Boolean | If a file (<100MB) is copied, it will be transferred to the remote machine clipboard. | ✅ Available |
| HideMouseAtScreenEdge | Boolean | Hide mouse at the screen edge. | ✅ Available |
| DrawMouseCursor | Boolean | Mouse cursor may not be visible in Windows 10 and later versions of Windows when there is no physical mouse attached. | ✅ Available |
| ValidateRemoteMachineIP | Boolean | Reverse DNS lookup to validate machine IP Address. | ✅ Available |
| SameSubnetOnly | Boolean | Only connect to machines in the same intranet NNN.NNN.*.* (only works when both machines have IPv4 enabled). | ✅ Available |
| BlockScreenSaverOnOtherMachines | Boolean | Block screen saver on other machines. | ✅ Available |
| MoveMouseRelatively | Boolean | Use this option when remote machine's monitor settings are different, or remote machine has multiple monitors. | ✅ Available |
| BlockMouseAtScreenCorners | Boolean | Block mouse at screen corners to avoid accident machine-switch at screen corners. | ✅ Available |
| ShowClipboardAndNetworkStatusMessages | Boolean | Show clipboard and network status messages. | ✅ Available |
| EasyMouse | Int | Easy Mouse mode index. | ✅ Available |
| HotKeySwitchMachine | Int | Shortcut to switch between machines index. | ✅ Available |
| ToggleEasyMouseShortcut | HotkeySettings | Shortcut to toggle Easy Mouse. | ✅ Available |
| LockMachineShortcut | HotkeySettings | Shortcut to lock all machines. | ✅ Available |
| ReconnectShortcut | HotkeySettings | Shortcut to try reconnecting. | ✅ Available |
| Switch2AllPCShortcut | HotkeySettings | Shortcut to switch to multiple machine mode. | ✅ Available |
| Name2IP | String | IP address mapping. | ✅ Available |

### PastePlain

| Name | Type | Description | Available |
| :--- | :--- | :--- | :--- |
| Enabled | Boolean | The enabled state for this utility. | ✅ Available |
| ActivationShortcut | HotkeySettings | Customize the shortcut to activate this module. | ✅ Available |

### Peek

| Name | Type | Description | Available |
| :--- | :--- | :--- | :--- |
| Enabled | Boolean | The enabled state for this utility. | ✅ Available |
| ActivationShortcut | HotkeySettings | Customize the shortcut to activate this module. | ✅ Available |
| AlwaysRunNotElevated | Boolean | Always run not elevated, even when PowerToys is elevated. | ✅ Available |
| CloseAfterLosingFocus | Boolean | Automatically close the Peek window after it loses focus. | ✅ Available |

### PowerAccent

PowerAccent is the internal name for Quick Accent.

| Name | Type | Description | Available |
| :--- | :--- | :--- | :--- |
| Enabled | Boolean | The enabled state for this utility. | ✅ Available |
| ActivationKey | PowerAccentActivationKey | Possible values: LeftRightArrow, Space, Both. | ✅ Available |
| DoNotActivateOnGameMode | Boolean | Disable activation shortcut when Game Mode is on. | ✅ Available |
| ToolbarPosition | String | Toolbar position index. | ✅ Available |
| InputTime | Int | Input time delay in milliseconds. | ✅ Available |
| SelectedLang | String | A character set to use. | ✅ Available |
| ExcludedApps | String | '\r'-separated list of executable names to prevent module activation if they're in a foreground. | ✅ Available |
| ShowUnicodeDescription | Boolean | Show the Unicode code and name of the currently selected character. | ✅ Available |
| SortByUsageFrequency | Boolean | Sort characters by usage frequency. | ✅ Available |
| StartSelectionFromTheLeft | Boolean | Start selection from the left. | ✅ Available |

### PowerLauncher

PowerLaucher is the internal name for PowerToys Run.

| Name | Type | Description | Available |
| :--- | :--- | :--- | :--- |
| Enabled | Boolean | The enabled state for this utility. | ✅ Available |
| OpenPowerLauncher | HotkeySettings | Customize the shortcut to activate the module. | ✅ Available |
| IgnoreHotkeysInFullscreen | Boolean | Ignore shortcuts in fullscreen mode. | ✅ Available |
| ClearInputOnLaunch | Boolean | Clear the previous query on open. | ✅ Available |
| TabSelectsContextButtons | Boolean | Tab through context buttons. | ✅ Available |
| Theme | Theme | Possible values: System, Light, Dark, HighContrastOne, HighContrastTwo, HighContrastBlack, HighContrastWhite. | ✅ Available |
| TitleFontSize | Int32 | Text size in points. | ✅ Available |
| Position | StartupPosition | Possible values: Cursor, PrimaryMonitor, Focus. | ✅ Available |
| UseCentralizedKeyboardHook | Boolean | Use centralized keyboard hook. | ✅ Available |
| SearchQueryResultsWithDelay | Boolean | Input Smoothing. | ✅ Available |
| SearchInputDelay | Int32 | Immediate plugins delay in milliseconds. | ✅ Available |
| SearchInputDelayFast | Int32 | Background execution plugins delay in milliseconds. | ✅ Available |
| SearchClickedItemWeight | Int32 | Selected item weight. | ✅ Available |
| SearchQueryTuningEnabled | Boolean | Results order tuning. | ✅ Available |
| SearchWaitForSlowResults | Boolean | Wait for slower plugin results before selecting top item in results. | ✅ Available |
| MaximumNumberOfResults | Int | Number of results shown before having to scroll. | ✅ Available |
| UsePinyin | Boolean | Use Pinyin. | ✅ Available |
| GenerateThumbnailsFromFiles | Boolean | Thumbnail generation for files is turned on. | ✅ Available |
| Plugins | explained in the next subsection | Thumbnail generation for files is turned on. | ✅ Available |

#### PowerToys Run plugins

PowerToys Run plugins can be configured in the Plugins property. [A sample](https://github.com/microsoft/PowerToys/blob/main/src/dsc/Microsoft.PowerToys.Configure/examples/configureLauncherPlugins.dsc.yaml) can be found in the PowerToys repository.

These are the available properties to configure each plugin:

| Name | Type | Description |
| :--- | :--- | :--- |
| Name | String | Name of the plugin we want to configure |
| Disabled | Boolean | The plugin should be disabled |
| IsGlobal | Boolean | The results for this plugin are shown in the global results |
| ActionKeyword | String | Configure the action keyword of the plugin |
| WeightBoost | Int | The weight modifier to help in ordering the results for this plugin |

> [!NOTE]
> Configuring additional properties of plugins through DSC is not yet supported.

### PowerOcr

PowerOcr is the internal name for Text Extractor.

| Name | Type | Description | Available |
| :--- | :--- | :--- | :--- |
| Enabled | Boolean | The enabled state for this utility. | ✅ Available |
| ActivationShortcut | HotkeySettings | Customize the shortcut to activate this module. | ✅ Available |
| PreferredLanguage | String | Should match the full name of one of the languages installed in the system. Example: "English (United States)". | ✅ Available |

### PowerPreview

| Name | Type | Description | Available |
| :--- | :--- | :--- | :--- |
| EnableSvgPreview | Boolean | Scalable Vector Graphics Preview Handler Enabled state. | ✅ Available |
| SvgBackgroundColorMode | Int | Color mode index. | ✅ Available |
| SvgBackgroundSolidColor | String | When using Solid color SvgBackgroundColorMode, specifies the color in `#FFFFFFFF` format. | ✅ Available |
| SvgBackgroundCheckeredShade | Int | When using Checkered pattern SvgBackgroundColorMode, specifies the shade index. | ✅ Available |
| EnableSvgThumbnail | Boolean | Scalable Vector Graphics Thumbnail Generator Enabled state. | ✅ Available |
| EnableMdPreview | Boolean | Markdown Preview Handler Enabled state. | ✅ Available |
| EnableMonacoPreview | Boolean | Source code files Preview Handler Enabled state. | ✅ Available |
| EnableMonacoPreviewWordWrap | Boolean | Wrap text. | ✅ Available |
| MonacoPreviewTryFormat | Boolean | Try to format the source for preview. | ✅ Available |
| MonacoPreviewMaxFileSize | Int | Maximum file size to preview in KB. | ✅ Available |
| EnablePdfPreview | Boolean | Portable Document Format Preview Handler Enabled state. | ✅ Available |
| EnablePdfThumbnail | Boolean | Portable Document Format Thumbnail Generator Enabled state. | ✅ Available |
| EnableGcodePreview | Boolean | Geometric Code Preview Handler Enabled state. | ✅ Available |
| EnableGcodeThumbnail | Boolean | Geometric Code Thumbnail Generator Enabled state. | ✅ Available |
| EnableStlThumbnail | Boolean | Stereolithography Thumbnail Generator Enabled state. | ✅ Available |
| StlThumbnailColor | String | Thumbnail color in `#FFFFFFFF` format . | ✅ Available |
| EnableQoiPreview | Boolean | Quite OK Image Preview Handler Enabled state. | ✅ Available |
| EnableQoiThumbnail | Boolean | Quite OK Image Thumbnail Generator Enabled state. | ✅ Available |

### PowerRename

| Name | Type | Description | Available |
| :--- | :--- | :--- | :--- |
| Enabled | Boolean | The enabled state for this utility. | ✅ Available |
| MRUEnabled | Boolean | Enable auto-complete for the search & replace fields. | ✅ Available |
| MaxMRUSize | Int | Maximum number of recently used items to remember. | ✅ Available |
| ExtendedContextMenuOnly | Boolean | Show PowerRename in extended context menu only or in default context menu as well. | ✅ Available |
| UseBoostLib | Boolean | Use Boost Library. | ✅ Available |

### RegistryPreview

| Name | Type | Description | Available |
| :--- | :--- | :--- | :--- |
| Enabled | Boolean | The enabled state for this utility. | ✅ Available |
| DefaultRegApp | Boolean | Make Registry Preview default app for opening .reg files. | ✅ Available |

### ShortcutGuide

| Name | Type | Description | Available |
| :--- | :--- | :--- | :--- |
| Enabled | Boolean | The enabled state for this utility. | ✅ Available |
| OpenShortcutGuide | HotkeySettings | Customize the shortcut to activate this module. | ✅ Available |
| OverlayOpacity | Int | Background opacity in percentage. | ✅ Available |
| UseLegacyPressWinKeyBehavior | Boolean | If ShortcutGuide should be activated by pressing the Windows key. | ✅ Available |
| PressTimeForGlobalWindowsShortcuts | Int | Press duration before showing global Windows shortcuts in milliseconds. | ✅ Available |
| PressTimeForTaskbarIconShortcuts | Int | Press duration before showing taskbar icon shortcuts in milliseconds. | ✅ Available |
| Theme | String | Theme index. | ✅ Available |
| DisabledApps | String | Turns off Shortcut Guide when these applications have focus. | ✅ Available |

### VideoConference

| Name | Type | Description | Available |
| :--- | :--- | :--- | :--- |
| Enabled | Boolean | The enabled state for this utility. | ✅ Available |
| MuteCameraAndMicrophoneHotkey | KeyboardKeys | Shortcut for muting the camera and microphone. | ✅ Available |
| MuteMicrophoneHotkey | KeyboardKeys | Shortcut for muting the microphone. | ✅ Available |
| PushToTalkMicrophoneHotkey | KeyboardKeys | Shortcut for push to talk. | ✅ Available |
| PushToReverseEnabled | Boolean | If enabled, allows both push to talk and push to mute, depending on microphone state. | ✅ Available |
| MuteCameraHotkey | KeyboardKeys | Shortcut for muting the camera. | ✅ Available |
| SelectedCamera | String | Device name. | ✅ Available |
| SelectedMicrophone | String | Device name or [All]. | ✅ Available |
| ToolbarPosition | String | Toolbar position option: "Top center", "Bottom center", "Top right corner", "Top left corner", "Bottom right corner", "Bottom left corner". | ✅ Available |
| ToolbarMonitor | String | Toolbar monitor option: "Main monitor", "All monitors". | ✅ Available |
| CameraOverlayImagePath | String | Path to the image used for the camera overlay. | ✅ Available |
| ToolbarHide | String | When to hide the toolbar: "Never", "When both camera and microphone are unmuted", "When both camera and microphone are muted", "After timeout". | ✅ Available |
| StartupAction | String | Startup action: "Nothing", "Unmute", "Mute". | ✅ Available |

### GeneralSettings

| Name | Type | Description | Available |
| :--- | :--- | :--- | :--- |
| Startup | Boolean | PowerToys is automatically enabled at startup. | ✅ Available |
| EnableWarningsElevatedApps | Boolean | Show a warning for functionality issues when running alongside elevated applications. | ✅ Available |
| Theme | String | What theme to use for the Settings application: "system", "dark", "light". | ✅ Available |
| ShowNewUpdatesToastNotification | Boolean | Show a toast notification when a new PowerToys update is available. | ✅ Available |
| AutoDownloadUpdates | Boolean | If new updates of PowerToys should be automatically downloaded in the background. | ✅ Available |
| ShowWhatsNewAfterUpdates | Boolean | After updating PowerToys, open the "What's new" screen. | ✅ Available |
| EnableExperimentation | Boolean | Opt-in into experimental features. | ✅ Available |

## Contributing

Refer to the [relevant devdocs](https://github.com/microsoft/PowerToys/tree/main/doc/devdocs/settingsv2/dsc-configure.md) section in the developer documentation to start working on the DSC module.
