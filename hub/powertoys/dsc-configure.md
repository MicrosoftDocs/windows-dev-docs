---
title: PowerToys DSC Configure module
description: DSC configure module documentation for PowerToys
ms.date: 03/22/2024
ms.topic: article
no-loc: [PowerToys, Windows, DSC, Win]
---

# DSC configuration

Since version 0.80, PowerToys installer is released on GitHub with `Microsoft.PowerToys.Configure` [DSC resource](https://learn.microsoft.com/en-us/powershell/dsc/overview?view=dsc-2.0) that allows you to configure PowerToys using a [Winget configuration file](../package-manager/configuration/create).

## Installation

### Prerequisites
- `PSDesiredStateConfiguration` 2.0.7 or higher: Refer to the [PowerShell DSC documentation](https://learn.microsoft.com/en-us/powershell/dsc/overview?view=dsc-2.0) for installation instructions.
- PowerShell 7.2 or higher.
- WinGet [version v1.6.2631 or later](https://github.com/microsoft/winget-cli/releases).

### Download

`Microsoft.PowerToys.Configure` is [installed alongside PowerToys](./install). Depending on the installer type, it is installed as follows:
- For the per-user install scope, the module is located in `%USERPROFILE%\Documents\PowerShell\Modules\Microsoft.PowerToys.Configure`.
- For the machine-wide install scope, it is found in `%ProgramFiles%\WindowsPowerShell\Modules\Microsoft.PowerToys.Configure`.

## Usage

You can invoke the resource directly using the following syntax:

```ps
Invoke-DscResource -Name PowerToysConfigure -Method Set -ModuleName Microsoft.PowerToys.Configure -Property @{ Awake = @{ Enabled = $false; Mode = "TIMED"; IntervalMinutes = "10" } }
```

However, creating a configuration.dsc.yaml file that contains the required settings in a simpler format is more convenient. Here's an example:

```yaml
properties:
  resources:
    - resource: Microsoft.WinGet.DSC/WinGetPackage
      directives:
        description: Install PowerToys
        allowPrerelease: true
      settings:
        id: PowerToys (Preview)
        source: winget

    - resource: PowerToysConfigure
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

The configuration file can be executed as follows:

```ps
winget configure .\configuration.dsc.yaml
```

This command installs the latest version of PowerToys and then uses the PowerToysConfigure resource to apply various settings for multiple PowerToys modules. More examples can be found [here](https://github.com/microsoft/PowerToys/tree/main/src/dsc/Microsoft.PowerToys.Configure/examples).

## Available Configuration Settings by Module

### AlwaysOnTop

| Name | Type | Description | Available |
| :--- | :--- | :--- | :--- |
| Enabled | Bool | The enabled state for this utility | ✅ |
| Hotkey | KeyboardKeys | Customize the shortcut to pin or unpin an app window | ✅ |
| FrameEnabled | Bool | Show a border around the pinned window | ✅ |
| FrameThickness | Int | Border thickness in pixels | ✅ |
| FrameColor | String | Specify a color in a #FFFFFFFF format or  | ✅ |
| FrameOpacity | Int | Border opacity in percentage | ✅ |
| FrameAccentColor | Bool | Whether to use a custom FrameColor value | ✅ |
| SoundEnabled | Bool | Play a sound when pinning a window | ✅ |
| DoNotActivateOnGameMode | Bool | Disable activation shortcut when Game Mode is on | ✅ |
| ExcludedApps | String | '\r'-separated list of executable names to exclude from pinning on top. | ✅ |
| RoundCornersEnabled | Bool | Enable round corners | ✅ |

### Awake

| Name | Type | Description | Available |
| :--- | :--- | :--- | :--- |
| Enabled | Bool | The enabled state for this utility | ✅ |
| KeepDisplayOn | Boolean | This setting is only available when keeping the PC awake | ✅ |
| Mode | AwakeMode | Possible values: PASSIVE INDEFINITE TIMED EXPIRABLE  | ✅ |
| IntervalHours | UInt32 | When using TIMED Mode, specifies the amount of hours | ✅ |
| IntervalMinutes | UInt32 | When using TIMED Mode, specifies the amount of minutes | ✅ |
| ExpirationDateTime | DateTimeOffset | When using EXPIRABLE Mode, specifies the date time in a format parsable with `DateTimeOffset.TryParse` | ✅ |

### ColorPicker

| Name | Type | Description | Available |
| :--- | :--- | :--- | :--- |
| Enabled | Bool | The enabled state for this utility | ✅ |
| ActivationShortcut | HotkeySettings | Customize the shortcut to activate this module | ✅ |
| CopiedColorRepresentation | String | The default color representation to be used. Example :"HEX" | ✅ |
| ActivationAction | ColorPickerActivationAction | Possible values: OpenEditor OpenColorPickerAndThenEditor OpenOnlyColorPicker  | ✅ |
| VisibleColorFormats | — | — | ❌ |
| ShowColorName | Boolean | This will show the name of the color when picking a color | ✅ |

### CropAndLock

| Name | Type | Description | Available |
| :--- | :--- | :--- | :--- |
| Enabled | Bool | The enabled state for this utility | ✅ |
| ReparentHotkey | KeyboardKeys | Shortcut to crop an application's window into a cropped window. This is experimental and can cause issues with some applications, since the cropped window will contain the original application window. | ✅ |
| ThumbnailHotkey | KeyboardKeys | Shortcut to crop and create a thumbnail of another window. The application isn't controllable through the thumbnail but it'll have less compatibility issues. | ✅ |

### EnvironmentVariables

| Name | Type | Description | Available |
| :--- | :--- | :--- | :--- |
| Enabled | Bool | The enabled state for this utility | ✅ |
| LaunchAdministrator | Boolean | Needs to be launched as administrator in order to make changes to the system environment variables | ✅ |

### FancyZones

| Name | Type | Description | Available |
| :--- | :--- | :--- | :--- |
| Enabled | Bool | The enabled state for this utility | ✅ |
| FancyzonesShiftDrag | Bool | Hold Shift key to activate zones while dragging a window | ✅ |
| FancyzonesMouseSwitch | Bool | Use a non-primary mouse button to toggle zone activation | ✅ |
| FancyzonesMouseMiddleClickSpanningMultipleZones | Bool | Use middle-click mouse button to toggle multiple zones spanning | ✅ |
| FancyzonesOverrideSnapHotkeys | Bool | This overrides the Windows Snap shortcut (Win + arrow) to move windows between zones | ✅ |
| FancyzonesMoveWindowsAcrossMonitors | Bool | Move windows between zones across all monitors | ✅ |
| FancyzonesMoveWindowsBasedOnPosition | Bool | Move windows based on relative position or zone index | ✅ |
| FancyzonesOverlappingZonesAlgorithm | Int | When multiple zones overlap algorithm index. | ✅ |
| FancyzonesDisplayOrWorkAreaChangeMoveWindows | Bool | Keep windows in their zones when the screen resolution or work area changes | ✅ |
| FancyzonesZoneSetChangeMoveWindows | Bool | During zone layout changes, windows assigned to a zone will match new size/positions | ✅ |
| FancyzonesAppLastZoneMoveWindows | Bool | Move newly created windows to their last known zone | ✅ |
| FancyzonesOpenWindowOnActiveMonitor | Bool | Move newly created windows to the curreynt active monitor (Experimental) | ✅ |
| FancyzonesRestoreSize | Bool | Restore the original size of windows when unsnapping | ✅ |
| FancyzonesQuickLayoutSwitch | Bool | Enable quick layout switch | ✅ |
| FancyzonesFlashZonesOnQuickSwitch | Bool | Flash zones when switching layout | ✅ |
| UseCursorposEditorStartupscreen | Bool | Whether to launch editor on the display where the mouse point is | ✅ |
| FancyzonesShowOnAllMonitors | Bool | Show zones on all monitors while dragging a window | ✅ |
| FancyzonesSpanZonesAcrossMonitors | Bool | Allow zones to span across monitors | ✅ |
| FancyzonesMakeDraggedWindowTransparent | Bool | Make dragged window transparent | ✅ |
| FancyzonesAllowChildWindowSnap | Bool | Allow child windows snapping | ✅ |
| FancyzonesDisableRoundCornersOnSnap | Bool | Disable round corners when window is snapped | ✅ |
| FancyzonesZoneHighlightColor | String | If not using FancyzonesSystemTheme, highlight color to use in #FFFFFFFF format | ✅ |
| FancyzonesHighlightOpacity | Int | Zone opacity in percentage | ✅ |
| FancyzonesEditorHotkey | KeyboardKeys | Customize the shortcut to activate this module | ✅ |
| FancyzonesWindowSwitching | Bool | Switch between windows in the current zone | ✅ |
| FancyzonesNextTabHotkey | KeyboardKeys | Next window shortcut | ✅ |
| FancyzonesPrevTabHotkey | KeyboardKeys | Previous window shortcut | ✅ |
| FancyzonesExcludedApps | String | '\r'-separated list of executable names to exclude from snapping | ✅ |
| FancyzonesBorderColor | String | If not using FancyzonesSystemTheme, border color to use in #FFFFFFFF format | ✅ |
| FancyzonesInActiveColor | String | If not using FancyzonesSystemTheme, inactive color to use in #FFFFFFFF format | ✅ |
| FancyzonesNumberColor | String | If not using FancyzonesSystemTheme, number color to use in #FFFFFFFF format | ✅ |
| FancyzonesSystemTheme | Bool | Whether to use system theme for zone appearance | ✅ |
| FancyzonesShowZoneNumber | Bool | Whether to show zone number | ✅ |

### FileLocksmith

| Name | Type | Description | Available |
| :--- | :--- | :--- | :--- |
| Enabled | Bool | The enabled state for this utility | ✅ |
| ExtendedContextMenuOnly | Bool | Show File Locksmith in extended context menu only or in default context menu as well | ✅ |

### FindMyMouse

| Name | Type | Description | Available |
| :--- | :--- | :--- | :--- |
| Enabled | Bool | The enabled state for this utility | ✅ |
| ActivationMethod | Int | Activation method index | ✅ |
| ActivationShortcut | HotkeySettings | Custom activation shortcut when using Custom for ActivationMethod | ✅ |
| DoNotActivateOnGameMode | Bool | Disable activation shortcut when Game Mode is on | ✅ |
| BackgroundColor | String | Background color in #FFFFFFFF format | ✅ |
| SpotlightColor | String | Spotlight color in #FFFFFFFF format | ✅ |
| OverlayOpacity | Int | Overlay opacity in percentage | ✅ |
| SpotlightRadius | Int | Spotlight radius in px | ✅ |
| AnimationDurationMs | Int | Animation duration in milliseconds | ✅ |
| SpotlightInitialZoom | Int | Spotlight zoom factor at animation start | ✅ |
| ExcludedApps | String | '\r'-separated list of executable names to prevent module activation | ✅ |
| ShakingMinimumDistance | Int | When using shake mouse ActivationMethod, the minimum distance for mouse shaking activation, for adjusting sensitivity | ✅ |
| ShakingIntervalMs | Int | When using shake mouse ActivationMethod, the span of time during which we track mouse movement to detect shaking, for adjusting sensitivity | ✅ |
| ShakingFactor | Int | When using shake mouse ActivationMethod, Shake factor in percentage | ✅ |

### Hosts

| Name | Type | Description | Available |
| :--- | :--- | :--- | :--- |
| Enabled | Bool | The enabled state for this utility | ✅ |
| ShowStartupWarning | Boolean | Show a warning at startup | ✅ |
| LoopbackDuplicates | Boolean | Consider loopback addresses as duplicates | ✅ |
| AdditionalLinesPosition | HostsAdditionalLinesPosition | Possible values: Top Bottom  | ✅ |
| Encoding | HostsEncoding | Possible values: Utf8 Utf8Bom  | ✅ |

### ImageResizer

| Name | Type | Description | Available |
| :--- | :--- | :--- | :--- |
| Enabled | Bool | The enabled state for this utility | ✅ |
| ImageresizerSelectedSizeIndex | Int | Default size preset index | ✅ |
| ImageresizerShrinkOnly | Bool | Make pictures smaller but not larger | ✅ |
| ImageresizerReplace | Bool | Overwrite files | ✅ |
| ImageresizerIgnoreOrientation | Bool | Ignore the orientation of pictures | ✅ |
| ImageresizerJpegQualityLevel | Int | JPEG quality level in percentage | ✅ |
| ImageresizerPngInterlaceOption | Int | PNG interlacing option index | ✅ |
| ImageresizerTiffCompressOption | Int | Tiff compression index | ✅ |
| ImageresizerFileName | String | This format is used as the filename for resized images  | ✅ |
| ImageresizerSizes | — | — | ❌ |
| ImageresizerKeepDateModified | Bool | Remove metadata that doesn't affect rendering | ✅ |
| ImageresizerFallbackEncoder | String | Fallback encoder to use | ✅ |
| ImageresizerCustomSize | — | — | ❌ |

### KeyboardManager

| Name | Type | Description | Available |
| :--- | :--- | :--- | :--- |
| Enabled | Bool | The enabled state for this utility | ✅ |
| ActiveConfiguration | — | — | ❌ |
| KeyboardConfigurations | — | — | ❌ |

### MeasureTool

| Name | Type | Description | Available |
| :--- | :--- | :--- | :--- |
| Enabled | Bool | The enabled state for this utility | ✅ |
| ActivationShortcut | HotkeySettings | Customize the shortcut to bring up the command bar | ✅ |
| ContinuousCapture | Boolean | Capture screen continuously during measuring | ✅ |
| DrawFeetOnCross | Boolean | Adds feet to the end of cross lines | ✅ |
| PerColorChannelEdgeDetection | Boolean | Enable a different edge detection algorithm | ✅ |
| PixelTolerance | Int | Pixel Tolerance for edge detection | ✅ |
| MeasureCrossColor | String | Line color in #FFFFFFFF format | ✅ |
| DefaultMeasureStyle | Int | Default measure style index | ✅ |

### MouseHighlighter

| Name | Type | Description | Available |
| :--- | :--- | :--- | :--- |
| Enabled | Bool | The enabled state for this utility | ✅ |
| ActivationShortcut | HotkeySettings | Customize the shortcut to turn on or off this mode | ✅ |
| LeftButtonClickColor | String | Primary button highlight color in #FFFFFFFF format | ✅ |
| RightButtonClickColor | String | Secondary button highlight color in #FFFFFFFF format | ✅ |
| AlwaysColor | String | Always highlight color in #FFFFFFFF format | ✅ |
| HighlightRadius | Int | Highlight radius in pixels | ✅ |
| HighlightFadeDelayMs | Int | Fade delay in milliseconds | ✅ |
| HighlightFadeDurationMs | Int | Fade duration in milliseconds | ✅ |
| AutoActivate | Bool | Automatically activate on utility startup | ✅ |

### MouseJump

| Name | Type | Description | Available |
| :--- | :--- | :--- | :--- |
| Enabled | Bool | The enabled state for this utility | ✅ |
| ActivationShortcut | HotkeySettings | Customize the shortcut to turn on or off this mode | ✅ |
| ThumbnailSize | MouseJumpThumbnailSize | Thumbnail size | ✅ |

### MousePointerCrosshairs

| Name | Type | Description | Available |
| :--- | :--- | :--- | :--- |
| Enabled | Bool | The enabled state for this utility | ✅ |
| ActivationShortcut | HotkeySettings | Customize the shortcut to show/hide the crosshairs | ✅ |
| CrosshairsColor | String | Crosshairs color in #FFFFFFFF | ✅ |
| CrosshairsOpacity | Int | Crosshairs opacity in percentage | ✅ |
| CrosshairsRadius | Int | Crosshairs center radius in pixels | ✅ |
| CrosshairsThickness | Int | Crosshairs thickness in pixels | ✅ |
| CrosshairsBorderColor | String | Crosshairs border color in #FFFFFFFF format | ✅ |
| CrosshairsBorderSize | Int | Crosshairs border size in pixels | ✅ |
| CrosshairsAutoHide | Bool | Automatically hide crosshairs when the mouse pointer is hidden | ✅ |
| CrosshairsIsFixedLengthEnabled | Bool | Fix crosshairs length | ✅ |
| CrosshairsFixedLength | Int | Crosshairs fixed length in pixels | ✅ |
| AutoActivate | Bool | Automatically activate on utility startup | ✅ |

### MouseWithoutBorders

| Name | Type | Description | Available |
| :--- | :--- | :--- | :--- |
| Enabled | Bool | The enabled state for this utility | ✅ |
| ShowOriginalUI | Boolean | show the original Mouse Without Borders UI | ✅ |
| WrapMouse | Boolean | Move control back to the first machine when mouse moves past the last one | ✅ |
| ShareClipboard | Boolean | If share clipboard stops working, Ctrl+Alt+Del then Esc may solve the problem. | ✅ |
| TransferFile | Boolean | If a file (<100MB) is copied, it will be transferred to the remote machine clipboard | ✅ |
| HideMouseAtScreenEdge | Boolean | Hide mouse at the screen edge | ✅ |
| DrawMouseCursor | Boolean | Mouse cursor may not be visible in Windows 10 and later versions of Windows when there is no physical mouse attached | ✅ |
| ValidateRemoteMachineIP | Boolean | Reverse DNS lookup to validate machine IP Address | ✅ |
| SameSubnetOnly | Boolean | Only connect to machines in the same intranet NNN.NNN.*.* (only works when both machines have IPv4 enabled) | ✅ |
| BlockScreenSaverOnOtherMachines | Boolean | Block screen saver on other machines | ✅ |
| MoveMouseRelatively | Boolean | Use this option when remote machine's monitor settings are different, or remote machine has multiple monitors | ✅ |
| BlockMouseAtScreenCorners | Boolean | Block mouse at screen corners to avoid accident machine-switch at screen corners | ✅ |
| ShowClipboardAndNetworkStatusMessages | Boolean | Show clipboard and network status messages | ✅ |
| EasyMouse | Int | Easy Mouse mode index | ✅ |
| HotKeySwitchMachine | Int | Shortcut to switch between machines index | ✅ |
| ToggleEasyMouseShortcut | HotkeySettings | Shortcut to toggle Easy Mouse | ✅ |
| LockMachineShortcut | HotkeySettings | Shortcut to lock all machines | ✅ |
| ReconnectShortcut | HotkeySettings | Shortcut to try reconnecting | ✅ |
| Switch2AllPCShortcut | HotkeySettings | Shortcut to switch to multiple machine mode | ✅ |
| Name2IP | String | IP address mapping | ✅ |

### PastePlain

| Name | Type | Description | Available |
| :--- | :--- | :--- | :--- |
| Enabled | Bool | The enabled state for this utility | ✅ |
| ActivationShortcut | HotkeySettings | Customize the shortcut to activate this module | ✅ |

### Peek

| Name | Type | Description | Available |
| :--- | :--- | :--- | :--- |
| Enabled | Bool | The enabled state for this utility | ✅ |
| ActivationShortcut | HotkeySettings | Customize the shortcut to activate this module | ✅ |
| AlwaysRunNotElevated | Bool | Always run not elevated, even when PowerToys is elevated | ✅ |
| CloseAfterLosingFocus | Bool | Automatically close the Peek window after it loses focus | ✅ |

### PowerAccent

| Name | Type | Description | Available |
| :--- | :--- | :--- | :--- |
| Enabled | Bool | The enabled state for this utility | ✅ |
| ActivationKey | PowerAccentActivationKey | Possible values: LeftRightArrow Space Both  | ✅ |
| DoNotActivateOnGameMode | Boolean | Disable activation shortcut when Game Mode is on | ✅ |
| ToolbarPosition | String | Toolbar position index | ✅ |
| InputTime | Int | Input time delay in milliseconds | ✅ |
| SelectedLang | String | A character set to use | ✅ |
| ExcludedApps | String | '\r'-separated list of executable names to to prevent module activation if they're in a foreground | ✅ |
| ShowUnicodeDescription | Boolean | Show the Unicode code and name of the currently selected character | ✅ |
| SortByUsageFrequency | Boolean | Sort characters by usage frequency | ✅ |
| StartSelectionFromTheLeft | Boolean | Start selection from the left | ✅ |

### PowerLauncher

| Name | Type | Description | Available |
| :--- | :--- | :--- | :--- |
| Enabled | Bool | The enabled state for this utility | ✅ |
| OpenPowerLauncher | HotkeySettings | Customize the shortcut to activate the module | ✅ |
| IgnoreHotkeysInFullscreen | Boolean | Ignore shortcuts in fullscreen mode | ✅ |
| ClearInputOnLaunch | Boolean | Clear the previous query on launch | ✅ |
| TabSelectsContextButtons | Boolean | Tab through context buttons | ✅ |
| Theme | Theme | Possible values: System Light Dark HighContrastOne HighContrastTwo HighContrastBlack HighContrastWhite  | ✅ |
| TitleFontSize | Int32 | Text size in points | ✅ |
| Position | StartupPosition | Possible values: Cursor PrimaryMonitor Focus  | ✅ |
| UseCentralizedKeyboardHook | Boolean | Use centralized keyboard hook | ✅ |
| SearchQueryResultsWithDelay | Boolean | Input Smoothing | ✅ |
| SearchInputDelay | Int32 | Immediate plugins delay in milliseconds | ✅ |
| SearchInputDelayFast | Int32 | Background execution plugins delay in milliseconds | ✅ |
| SearchClickedItemWeight | Int32 | Selected item weight | ✅ |
| SearchQueryTuningEnabled | Boolean | Results order tuning | ✅ |
| SearchWaitForSlowResults | Boolean | Wait on slower plugin results before selecting top item in results | ✅ |
| UsePinyin | Boolean | Use Pinyin | ✅ |
| GenerateThumbnailsFromFiles | Boolean | Whether thumbnail generation for files is turned on | ✅ |

### PowerOcr

| Name | Type | Description | Available |
| :--- | :--- | :--- | :--- |
| Enabled | Bool | The enabled state for this utility | ✅ |
| ActivationShortcut | HotkeySettings | Customize the shortcut to activate this module | ✅ |
| PreferredLanguage | String | Should match the full name of one of the languages installed in the system. Example: "English (United States)" | ✅ |

### PowerPreview

| Name | Type | Description | Available |
| :--- | :--- | :--- | :--- |
| EnableSvgPreview | Boolean | Scalable Vector Graphics Preview Handler Enabled state | ✅ |
| SvgBackgroundColorMode | Int | Color mode index | ✅ |
| SvgBackgroundSolidColor | String | When using Solid color SvgBackgroundColorMode, specifies the color in #FFFFFFFF format | ✅ |
| SvgBackgroundCheckeredShade | Int | | When using Checkered pattern SvgBackgroundColorMode, specifies the shade index | ✅ |
| EnableSvgThumbnail | Boolean | Scalable Vector Graphics Thumbnail Generator Enabled state | ✅ |
| EnableMdPreview | Boolean | Markdown Preview Handler Enabled state | ✅ |
| EnableMonacoPreview | Boolean | Source code files Preview Handler Enabled state | ✅ |
| EnableMonacoPreviewWordWrap | Boolean | Wrap text | ✅ |
| MonacoPreviewTryFormat | Boolean | Try to format the source for preview | ✅ |
| MonacoPreviewMaxFileSize | Int | Maximum file size to preview in KB | ✅ |
| EnablePdfPreview | Boolean | Portable Document Format Preview Handler Enabled state | ✅ |
| EnablePdfThumbnail | Boolean | Portable Document Format Thumbnail Generator Enabled state | ✅ |
| EnableGcodePreview | Boolean | Geometric Code Preview Handler Enabled state | ✅ |
| EnableGcodeThumbnail | Boolean | Geometric Code Thumbnail Generator Enabled state | ✅ |
| EnableStlThumbnail | Boolean | Stereolithography Thumbnail Generator Enabled state | ✅ |
| StlThumbnailColor | String | Thumbnail color in #FFFFFFFF format  | ✅ |
| EnableQoiPreview | Boolean | Quite OK Image Preview Handler Enabled state | ✅ |
| EnableQoiThumbnail | Boolean | Quite OK Image Thumbnail Generator Enabled state | ✅ |

### PowerRename

| Name | Type | Description | Available |
| :--- | :--- | :--- | :--- |
| Enabled | Bool | The enabled state for this utility | ✅ |
| MRUEnabled | Bool | Enable auto-complete for the search & replace fields | ✅ |
| MaxMRUSize | Int | Maximum number of recently used items to remember | ✅ |
| ExtendedContextMenuOnly | Bool | Show PowerRename in extended context menu only or in default context menu as well | ✅ |
| UseBoostLib | Bool | Use Boost Library | ✅ |

### RegistryPreview

| Name | Type | Description | Available |
| :--- | :--- | :--- | :--- |
| Enabled | Bool | The enabled state for this utility | ✅ |
| DefaultRegApp | Boolean | Make Registry Preview default app for opening .reg files | ✅ |

### ShortcutGuide

| Name | Type | Description | Available |
| :--- | :--- | :--- | :--- |
| Enabled | Bool | The enabled state for this utility | ✅ |
| OpenShortcutGuide | HotkeySettings | Customize the shortcut to activate this module | ✅ |
| OverlayOpacity | Int | Background opacity in percentage | ✅ |
| UseLegacyPressWinKeyBehavior | Bool | If ShortcutGuide should be activated by pressing the Windows key | ✅ |
| PressTimeForGlobalWindowsShortcuts | Int | Press duration before showing global Windows shortcuts in milliseconds | ✅ |
| PressTimeForTaskbarIconShortcuts | Int | Press duration before showing taskbar icon shortcuts in milliseconds | ✅ |
| Theme | String | Theme index | ✅ |
| DisabledApps | String | Turns off Shortcut Guide when these applications have focus | ✅ |

### VideoConference

| Name | Type | Description | Available |
| :--- | :--- | :--- | :--- |
| Enabled | Bool | The enabled state for this utility | ✅ |
| MuteCameraAndMicrophoneHotkey | KeyboardKeys | Shortcut for muting the camera and microphone | ✅ |
| MuteMicrophoneHotkey | KeyboardKeys | Shortcut for muting the microphone | ✅ |
| PushToTalkMicrophoneHotkey | KeyboardKeys | Shortcut for push to talk | ✅ |
| PushToReverseEnabled | Bool | If enabled, allows both push to talk and push to mute, depending on microphone state | ✅ |
| MuteCameraHotkey | KeyboardKeys | Shortcut for muting the camera | ✅ |
| SelectedCamera | String | Device name | ✅ |
| SelectedMicrophone | String | Device name or [All] | ✅ |
| ToolbarPosition | String | Toolbar position option: "Top center", "Bottom center", "Top right corner", "Top left corner", "Bottom right corner", "Bottom left corner" | ✅ |
| ToolbarMonitor | String | Toolbar monitor option: "Main monitor", "All monitors" | ✅ |
| CameraOverlayImagePath | String | Path to the image used for the camera overlay | ✅ |
| ToolbarHide | String | When to hide the toolbar: "Never", "When both camera and microphone are unmuted", "When both camera and microphone are muted", "After timeout" | ✅ |
| StartupAction | String | Startup action: "Nothing", "Unmute", "Mute" | ✅ |

### GeneralSettings

| Name | Type | Description | Available |
| :--- | :--- | :--- | :--- |
| Startup | Boolean | Whether PowerToys is automatically enabled at startup | ✅ |
| EnableWarningsElevatedApps | Boolean | Show a warning for functionality issues when running alongside elevated applications | ✅ |
| Theme | String | What theme to use for the Settings application: "system", "dark", "light" | ✅ |
| ShowNewUpdatesToastNotification | Boolean | Show a toast notification when a new PowerToys update is available | ✅ |
| AutoDownloadUpdates | Boolean | If new updates of PowerToys should be automatically downloaded in the background | ✅ |
| ShowWhatsNewAfterUpdates | Boolean | After updating PowerToys, open the "What's new" screen | ✅ |
| EnableExperimentation | Boolean | Whether to opt-in into experimental features | ✅ |

## Contributing

Refer to the [relevant devdocs](https://github.com/microsoft/PowerToys/tree/main/doc/devdocs/settingsv2/dsc-configure.md) section in the developer documentation to start working on the DSC module.
