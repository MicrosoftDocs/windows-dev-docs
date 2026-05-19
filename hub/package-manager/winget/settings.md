---
title: settings command
description: Provides customizations for the Windows Package Manager.
ms.date: 03/24/2026
no-loc: [winget, settings, config]
ms.topic: article
---

# settings command (winget)

The **settings** command of [WinGet](./index.md) allows you to customize your Windows Package Manager client experience. You can change defaults and try out experimental features that are available in your client.

The **settings** command will launch your default JSON editor. Windows by default will launch Notepad as an option.  We recommend using a tool like [Visual Studio code](https://code.visualstudio.com/).  

>[!NOTE]
>You can easily install Visual Studio Code by typing `winget install Microsoft.VisualStudioCode`

## Aliases

The following aliases are available for this command:

- `config`

## Sub-commands

The following sub-commands are available.

| Sub-Command | Description |
|-------------|-------------|
| **export** | Exports settings. |
| **set** | Sets the value of an administrator setting. |
| **reset** | Resets an administrator setting to its default value. |

## Options
The following options are available:

| Argument      | Description |
|-------------|-------------|
| **--enable** | Enables the specified administrator setting. |
| **--disable** | Disables the specified administrator setting. |
| **-?,--help** | Shows help about the selected command. |
| **--wait** | Prompts the user to press any key before exiting. |
| **--logs,--open-logs** | Opens the default logs location. |
| **--verbose,--verbose-logs** | Enables verbose logging for winget. |
| **--nowarn,--ignore-warnings** | Suppresses warning outputs. |
| **--disable-interactivity** | Disables interactive prompts. |
| **--proxy** | Sets a proxy to use for this execution. |
| **--no-proxy** | Disables the use of proxy for this execution. |

## Use the winget settings command

Launch your default JSON editing tool: `winget settings`

When you launch the settings for the first time, there will be no settings specified. At the top of the JSON file, we provide a [WinGet CLI Settings](https://aka.ms/winget-settings) link, where you can discover the latest experimental features and settings.

The code snippet below shows an example of what your settings file might look like with visual output modifications and experimental features enabled.

```json
{
    "$schema": "https://aka.ms/winget-settings.schema.json",

    "visual": {
        "enableSixels": true,
        "progressBar": "rainbow"
    },
    "experimentalFeatures": {
        "experimentalARG": true,
        "experimentalCMD": true
    }
}
```

We have also defined a schema for the settings file. This allows you to use TAB to discover settings and syntax if your JSON editor supports JSON schemas.

## Updating settings

The following settings are available for the 1.28 release of the Windows Package Manager.

### source settings

The `source` settings involve configuration to the WinGet source.

```json
"source": {
    "autoUpdateIntervalInMinutes": 60
},
```

#### autoUpdateIntervalInMinutes

A positive integer represents the update interval in minutes. The check for updates only happens when a source is used. A zero will disable the check for updates to a source. Any other values are invalid.

- Disable: 0
- Default: 15

To manually update the source use `winget source update`.

### visual settings

The `visual` settings involve visual elements that are displayed by WinGet

```json
"visual": {
    "enableSixels": true,
    "progressBar": "rainbow"
},
```

#### progressBar

Color of the progress bar that WinGet displays when not specified by arguments. 

- accent (default)
- rainbow
- retro
- sixel
- disabled

#### anonymizeDisplayedPaths

Replaces some known folder paths with their respective environment variables.

#### enableSixels

Enables output of sixel images in certain contexts.

### logging settings

The `logging` settings control the level of detail in log files. `--verbose-logs` will override this setting and always creates a verbose log.


```json
"logging": {
    "level": "verbose"
}
```

#### level

The following logging levels are available. Defaults to `info` if the value is not set or is invalid. 

- verbose
- info
- warning
- error
- critical

#### channels

The `channels` setting restricts logging output to specific log channels. Special values `default` (the default set of channels) and `all` (all channels) are also accepted. Invalid values are ignored.

```json
"logging": {
    "channels": ["default"]
}
```

#### file

The `file` settings control automatic cleanup of log files in the default log directory. Cleanup runs at the start of each WinGet process and applies only to the default log location.

| Setting | Description | Default |
|---------|-------------|---------|
| `ageLimitInDays` | Maximum age in days of files in the log directory; older files are deleted. Set to `0` to disable. | 7 |
| `totalSizeLimitInMB` | Maximum total size in megabytes of all files in the log directory; the oldest files are deleted first. Set to `0` to disable. | 128 |
| `countLimit` | Maximum number of files in the log directory; the oldest files are deleted first. Set to `0` to disable. | 0 (disabled) |
| `individualSizeLimitInMB` | Maximum size in megabytes of a single log file. If a file would exceed this limit, logs wrap. Set to `0` to disable. | 16 |

```json
"logging": {
    "level": "verbose",
    "file": {
        "ageLimitInDays": 7,
        "totalSizeLimitInMB": 128,
        "countLimit": 0,
        "individualSizeLimitInMB": 16
    }
}
```

### preferences and requirements settings

Some of the settings are duplicated under `preferences` and `requirements`. 

- The `preferences` setting controls how the various available options are sorted when choosing the one to act on. For example, the default scope of package installs is for the current user, but if that is not an option then a machine level installer will be chosen.
- The `requirements` setting filters the options, potentially resulting in an empty list and a failure to install. In the previous example, a user scope requirement would result in no applicable installers and an error.

Any arguments passed on the command line will effectively override the matching `requirement` setting for the duration of that command.

#### scope

The `scope` behavior controls the choice between installing a package for the current user or for the entire machine. The matching parameter is `--scope`, and uses the same values (`user` or `machine`). See  [known issues relating to package installation scope](./troubleshooting.md#scope-for-specific-user-vs-machine-wide).

```json
"installBehavior": {
    "preferences": {
        "scope": "user"
    }
},
```

#### locale

The `locale` behavior controls the choice of installer based on installer locale. The matching parameter is `--locale`, and uses bcp47 language tag.

```json
"installBehavior": {
    "preferences": {
        "locale": [ "en-US", "fr-FR" ]
    }
},
```

#### architectures

The `architectures` behavior controls what architectures will be selected when installing a package. The matching parameter is `--architecture`. Only architectures compatible with your system can be selected.

```json
    "installBehavior": {
        "preferences": {
            "architectures": ["x64", "arm64"]
        }
    },
```

#### installerTypes

The `installerTypes` behavior affects what installer types will be selected when installing a package. It can also determine which type to install by default if a manifest has multiple types: The list is in priority order, with the first listed type being preferred over the others, and so on.  This is convenient for users who for instance prefer portable packages or MSIX/AppX installations. The matching parameter is `--installer-type`, which will override the settings.

Allowed values as of version 1.12.470 include: `appx`, `burn`, `exe`, `font`, `inno`, `msi`, `msix`, `msstore`, `nullsoft`, `portable`, `wix`, `zip`

By default, and with all other properties being equal, WinGet defaults to the installer type that is listed first in the manifest's installer YAML if the package has not been installed yet.  If it is already installed, the same installer type will be required to ensure a proper upgrade.

```json
    "installBehavior": {
        "preferences": {
            "installerTypes": ["msix", "msi"]
        }
    },
```

### installBehavior settings

The `installBehavior` settings control the default behavior of installing and upgrading (where applicable) packages.

#### disableInstallNotes

The `disableInstallNotes` setting determines whether installation notes are shown after a successful install. Defaults to `false` if value is not set or is invalid.

```json
    "installBehavior": {
        "disableInstallNotes": true
    },
```

#### portablePackageUserRoot setting

The `portablePackageUserRoot` setting defines the default root directory for installing packages under the `User` scope. This applies only to packages with the `portable` installer type. Defaults to `%LOCALAPPDATA%/Microsoft/WinGet/Packages/` if value is not set or is invalid.

This setting value must be an absolute path.

```json
    "installBehavior": {
        "portablePackageUserRoot": "C:/Users/FooBar/Packages"
    },
```

#### portablePackageMachineRoot setting

The `portablePackageMachineRoot` setting defines the default root directory for installing packages under the `Machine` scope. This applies only to packages with the `portable` installer type. Defaults to `%PROGRAMFILES%/WinGet/Packages/` if the value is not set or is invalid.

This setting value must be an absolute path.

```json
    "installBehavior": {
        "portablePackageMachineRoot": "C:/Program Files/Packages/Portable"
    },
```

#### defaultInstallRoot

The `defaultInstallRoot` setting specifies the default install location for packages that require an explicit install path, if the install location is not specified.

#### maxResumes

The `maxResumes` setting specifies the maximum number of resume attempts allowed for a single resume ID. This prevents continuous reboots if an installation requiring a reboot is not properly detected.

#### archiveExtractionMethod

The `archiveExtractionMethod` setting controls how the installer extracts archives. Supported values are `shellApi` and `tar`.

- `shellApi` uses the Windows Shell API to extract archives. 

- `tar` uses the tar command to extract archives.

### UninstallBehavior

The `uninstallBehavior` setting controls whether the default uninstall process removes all files and directories relevant to this package. Only applies to the portable `installerType`.

#### purgePortablePackage

The `purgePortablePackage` setting controls the default behavior for uninstalling a portable package. If set to `true`, uninstall will remove all files and directories relevant to the `portable` package. This setting only applies to packages with the `portable` installer type. Defaults to `false` if value is not set or is invalid.

```json
    "uninstallBehavior": {
        "purgePortablePackage": true
    },
```

### ConfigureBehavior

The `ConfigureBehavior` setting specifies the default root directory where PowerShell modules are installed to when applying a configuration.

### downloadBehavior

The `downloadBehavior` settings control the default directory where installers are downloaded to.

#### defaultDownloadDirectory

The `defaultDownloadDirectory` setting controls the default directory where packages are downloaded to. Defaults to `%USERPROFILE%/Downloads` if value is not set or is invalid.

This setting value must be an absolute path.

```json
    "downloadBehavior": {
        "defaultDownloadDirectory": "C:/Users/FooBar/Downloads"
    },
```

### Telemetry settings

The `telemetry` settings control whether WinGet writes ETW events that may be sent to Microsoft on a default installation of Windows.

See [details on telemetry](https://github.com/microsoft/winget-cli/blob/master/README.md#datatelemetry), and our [primary privacy statement](https://github.com/microsoft/winget-cli/blob/master/PRIVACY.md).

#### disable

```json
"telemetry": {
    "disable": true
},
```

If set to true, the `telemetry.disable` setting will prevent any event from being written by the program.

### Network settings

The `network` settings influence how WinGet uses the network to retrieve packages and metadata.

#### downloader

The `downloader` setting controls which code is used when downloading packages. The default is `do`, which may be managed by Group Policy.

`wininet` uses the [WinINet](/windows/win32/wininet/about-wininet) APIs, while `do` uses the [Delivery Optimization](https://support.microsoft.com/windows/delivery-optimization-in-windows-10-0656e53c-15f2-90de-a87a-a2172c94cf6d) service.

```json
"network": {
    "downloader": "wininet"
}
```

#### doProgressTimeoutInSeconds

The `doProgressTimeoutInSeconds` specifies the number of seconds to wait without progress before fallback.

### Interactivity

The `Interactivity` setting controls whether interactive prompts are shown by the Windows Package Manager client.

### Enabling experimental features

To discover which experimental features are available, go to [https://aka.ms/winget-settings](https://aka.ms/winget-settings) where you can see the experimental features available to you.

The `experimentalFeatures` settings involve the configuration of these "experimental" features. Individual features can be enabled under this node:

```json
"experimentalFeatures": {
    "directMSI": true,
    "resume": true
}
```

#### directMSI

This feature enables the Windows Package Manager to directly install MSI packages with the MSI APIs rather than through msiexec. Note that when silent installation is used this is already in effect, as MSI packages that require elevation will fail in that scenario without it.

```json
"experimentalFeatures": {
    "directMSI": true
}
```

#### resume

This feature enables support for some commands to resume after a reboot.

```json
"experimentalFeatures": {
    "resume": true
}
```

#### fonts

This feature enables support for fonts via `winget settings`. The `winget font list` command will list installed font families and the number of installed font faces.

```json
"experimentalFeatures": {
    "fonts": true
}
```

#### sourcePriority

This feature enables sources to have a priority value assigned. Sources with a higher priority will appear earlier in search results and will be selected for installing new packages when multiple sources have a matching package.

Note that search result ordering is dependent on several factors, and source priority is the lowest field currently (match quality and field are more important).

```json
"experimentalFeatures": {
    "sourcePriority": true
}
```
