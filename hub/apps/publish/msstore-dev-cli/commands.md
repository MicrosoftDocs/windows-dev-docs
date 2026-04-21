---
description: How to run the Microsoft Store Developer CLI (preview) commands for MSIX apps.
title: Microsoft Store Developer CLI (preview) Commands (MSIX)
ms.date: 11/08/2024
ms.topic: article
zone_pivot_groups: msstoredevcli-installer-packaging
---

# Commands (MSIX)

## Installation

:::zone pivot="msstoredevcli-installer-windows"

### Step 1: Install .NET Windows Runtime

If you haven't done so already, install the latest version of the [.NET 9 Desktop Runtime](https://dotnet.microsoft.com/download/dotnet/9.0). This is a requirement to run the Microsoft Store Developer CLI.

The easiest way to install it is to use _winget_:

```console
winget install Microsoft.DotNet.DesktopRuntime.9
```

### Step 2: Install the Microsoft Store Developer CLI on Windows

You can download the Microsoft Store Developer CLI from the [Microsoft Store](https://www.microsoft.com/store/apps/9P53PC5S0PHJ). Alternatively, you can use _winget_:

```console
winget install "Microsoft Store Developer CLI"
```

:::zone-end
:::zone pivot="msstoredevcli-installer-macos"

### Step 1: Install .NET macOS Runtime

If you haven't done so already, install the latest version of the [.NET 9 Runtime](https://dotnet.microsoft.com/download/dotnet/9.0). This is a requirement to run the Microsoft Store Developer CLI.

### Step 2: Install the Microsoft Store Developer CLI on macOS

You can download the macOS _.tar.gz_ for your specific architecture (x64 or Arm64) from the [Microsoft Store Developer CLI releases page](https://aka.ms/msstoredevcli/releases). Once downloaded, extract the archive and put it in your PATH, however you want to do that, for example:

```console
mkdir MSStoreCLI
curl https://github.com/microsoft/msstore-cli/releases/latest/download/MSStoreCLI-osx-x64.tar.gz -o MSStoreCLI-osx-x64.tar.gz
tar -xvf MSStoreCLI-osx-x64.tar.gz -C ./MSStoreCLI
sudo cp -R MSStoreCLI/. /usr/local/bin
```

Alternatively, you can use _brew_:

```console
brew install microsoft/msstore-cli/msstore-cli
```

:::zone-end
:::zone pivot="msstoredevcli-installer-linux"

### Step 1: Install .NET Linux Runtime

If you haven't done so already, install the latest version of the [.NET 9 Runtime](https://dotnet.microsoft.com/download/dotnet/9.0). This is a requirement to run the Microsoft Store Developer CLI.

### Step 2: Install the Microsoft Store Developer CLI on Linux

You can download the Linux _.tar.gz_ for your specific architecture (x64 or Arm64) from the [Microsoft Store Developer CLI releases page](https://aka.ms/msstoredevcli/releases). Once downloaded, extract the archive and put it in your PATH, however you want to do that, for example:

```console
mkdir MSStoreCLI
wget https://github.com/microsoft/msstore-cli/releases/latest/download/MSStoreCLI-linux-x64.tar.gz
tar -xvf MSStoreCLI-linux-x64.tar.gz -C ./MSStoreCLI
sudo cp -R MSStoreCLI/. /usr/local/bin
```

Alternatively, you can use _brew_:

```console
brew install microsoft/msstore-cli/msstore-cli
```

:::zone-end

## Info Command

Print existing configuration.

#### Usage

```console
msstore info
```

#### Options

| Option         | Description                      |
| -------------  | -------------------------------- |
| -v, --verbose  | Print verbose output.            |
| -?, -h, --help | Show help and usage information. |

## Reconfigure Command

Re-configure the Microsoft Store Developer CLI. You can provide either a Client Secret or a Certificate. Certificates can be provided either through its Thumbprint or by providing a file path (with or without a password).

#### Usage

```console
msstore reconfigure
```

#### Options

| Option                       | Description                                             |
| ---------------------------- | ------------------------------------------------------- |
| -t, --tenantId               | Specify the tenant Id that should be used.              |
| -s, --sellerId               | Specify the seller Id that should be used.              |
| -c, --clientId               | Specify the client Id that should be used.              |
| -cs, --clientSecret          | Specify the client Secret that should be used.          |
| -ct, --certificateThumbprint | Specify the certificate Thumbprint that should be used. |
| -cfp, --certificateFilePath  | Specify the certificate file path that should be used.  |
| -cp, --certificatePassword   | Specify the certificate password that should be used.   |
| --reset                      | Only reset the credentials, without starting over.      |
| -v, --verbose                | Print verbose output.                                   |
| -?, -h, --help               | Show help and usage information.                        |

## Settings Command

Change settings of the Microsoft Store Developer CLI.

#### Usage

```console
msstore settings
```

#### Options

| Option                | Description                                       |
| --------------------- | ------------------------------------------------- |
| -t, --enableTelemetry | Enable (empty/true) or Disable (false) telemetry. |
| -v, --verbose         | Print verbose output.                             |
| -?, -h, --help        | Show help and usage information.                  |

### SetPDN Sub-Command

Set the Publisher Display Name property that is used by the init command.

#### Usage

```console
msstore settings setpdn <publisherDisplayName>
```

#### Arguments

| Argument               | Description                                                    |
| ---------------------- | -------------------------------------------------------------- |
| `publisherDisplayName` | The Publisher Display Name property that will be set globally. |

#### Options

| Option                | Description                     |
| --------------------- | ------------------------------- |
| -?, -h, --help        | Show help and usage information.|

## Apps Command

| Sub-Command                  | Description                                 |
| -----------------------------| --------------------------------------------|
| [list](#apps---list---usage) | Lists all the applications in your account. |
| [get](#apps---get---usage)   | Gets the details of a specific application. |

#### Apps - List - Usage

```console
msstore apps list
```

#### Options

| Option                | Description                      |
| --------------------- | -------------------------------- |
| -v, --verbose         | Print verbose output.            |
| -?, -h, --help        | Show help and usage information. |

#### Apps - Get - Usage

```console
msstore apps get <productId>
```

#### Arguments

| Argument    | Description           |
| ----------- | --------------------- |
| `productId` | The Store product ID. |

#### Options

| Option                | Description                      |
| --------------------- | -------------------------------- |
| -v, --verbose         | Print verbose output.            |
| -?, -h, --help        | Show help and usage information. |

## Submission Command

| Sub-Command                                                | Description                                                 |
| -----------------------------------------------------------| ----------------------------------------------------------- |
| [status](#submission---status---usage)                     | Gets the status of a submission.                            |
| [get](#submission---get---usage)                           | Gets the metadata and package info of a specific submission.|
| [getListingAssets](#submission---getlistingassets---usage) | Gets the listing assets of a specific submission.           |
| [updateMetadata](#submission---updatemetadata---usage)     | Updates the metadata of a specific submission.              |
| [update](#submission---update---usage)                     | Updates the package of a specific submission.               |
| [poll](#submission---poll---usage)                         | Polls the status of a submission.                           |
| [publish](#submission---publish---usage)                   | Publishes a specific submission.                            |
| [delete](#submission---delete---usage)                     | Deletes a specific submission.                              |

#### Submission - Status - Usage

```console
msstore submission status <productId>
```

#### Arguments

| Argument    | Description           |
| ----------- | --------------------- |
| `productId` | The Store product ID. |

#### Options

| Option                | Description                      |
| --------------------- | -------------------------------- |
| -v, --verbose         | Print verbose output.            |
| -?, -h, --help        | Show help and usage information. |

#### Submission - Get - Usage

```console
msstore submission get <productId>
```

#### Arguments

| Argument    | Description           |
| ----------- | --------------------- |
| `productId` | The Store product ID. |

#### Options

| Option                | Description                      |
| --------------------- | -------------------------------- |
| -v, --verbose         | Print verbose output.            |
| -?, -h, --help        | Show help and usage information. |

#### Submission - GetListingAssets - Usage

```console
msstore submission getListingAssets <productId>
```

#### Arguments

| Argument    | Description     |
| ----------- | --------------- |
| `productId` | The Store product ID. |

#### Options

| Option                | Description                      |
| --------------------- | -------------------------------- |
| -v, --verbose         | Print verbose output.            |
| -?, -h, --help        | Show help and usage information. |

#### Submission - UpdateMetadata - Usage

```console
msstore submission updateMetadata <productId> <metadata>
```

#### Arguments

| Argument    | Description                               |
| ----------- | ----------------------------------------- |
| `productId` | The Store product ID.                     |
| `metadata`  | The updated JSON metadata representation. |

#### Options

| Option                   | Description                                                            |
| ------------------------ | ---------------------------------------------------------------------- |
| -s, --skipInitialPolling | Skip the initial polling before executing the action. [default: False] |
| -v, --verbose            | Print verbose output.                                                  |
| -?, -h, --help           | Show help and usage information.                                       |

#### Submission - Update - Usage

```console
msstore submission update <productId> <package>
```

#### Arguments

| Argument    | Description                                         |
| ----------- | --------------------------------------------------- |
| `productId` | The Store product ID.                               |
| `package`   | The updated JSON representation of the app package. |

#### Options

| Option                   | Description                                                            |
| ------------------------ | ---------------------------------------------------------------------- |
| -s, --skipInitialPolling | Skip the initial polling before executing the action. [default: False] |
| -v, --verbose            | Print verbose output.                                                  |
| -?, -h, --help           | Show help and usage information.                                       |

> [!TIP]
> Use `submission get` to retrieve the current package JSON before calling `submission update`. This ensures you're working with the correct structure and current values. For example, in PowerShell:
>
> ```powershell
> # Step 1: Retrieve the current submission package JSON
> msstore submission get <productId> | Out-File -Encoding utf8 package.json
>
> # Step 2: Edit package.json to reflect your changes
>
> # Step 3: Pass the updated JSON to submission update
> $updatedPackage = Get-Content -Raw package.json
> msstore submission update <productId> $updatedPackage
> ```
>
> For more context, see [Publish app updates to Microsoft Store with GitHub Actions](./github-actions.md).

#### Submission - Poll - Usage

```console
msstore submission poll <productId>
```

#### Arguments

| Argument    | Description           |
| ----------- | --------------------- |
| `productId` | The Store product ID. |

#### Options

| Option                | Description                      |
| --------------------- | -------------------------------- |
| -v, --verbose         | Print verbose output.            |
| -?, -h, --help        | Show help and usage information. |

#### Submission - Publish - Usage

```console
msstore submission publish <productId>
```

#### Arguments

| Argument    | Description           |
| ----------- | --------------------- |
| `productId` | The Store product ID. |

#### Options

| Option                | Description                      |
| --------------------- | -------------------------------- |
| -v, --verbose         | Print verbose output.            |
| -?, -h, --help        | Show help and usage information. |

#### Submission - Delete - Usage

```console
msstore submission delete <productId>
```

#### Arguments

| Argument    | Description     |
| ----------- | --------------- |
| `productId` | The Store product ID. |

#### Options

| Option                | Description                                      |
| --------------------- | ------------------------------------------------ |
| --no-confirm          | Do not prompt for confirmation. [default: False] |
| -v, --verbose         | Print verbose output.                            |
| -?, -h, --help        | Show help and usage information.                 |

## Flights Commands

| Sub-Command                                 | Description                                                  |
| ------------------------------------------- | ------------------------------------------------------------ |
| [list](#flights---list-command-usage)       | Retrieves all the Flights for the specified Application.     |
| [get](#flights---get-command-usage)         | Retrieves a flight for the specified Application and flight. |
| [delete](#flights---delete-command-usage)   | Deletes a flight for the specified Application and flight.   |
| [create](#flights---create-command-usage)   | Creates a flight for the specified Application and flight.   |
| [submission](#flights---submission-command) | Execute flight submissions related tasks.                    |

### Flights - List Command Usage

```console
msstore flights list <productId>
```

#### Flights - List Command Arguments

| Argument    | Description     |
| ----------- | --------------- |
| `productId` | The product ID. |

#### Flights - List Command Help

```console
msstore flights list --help
```
### Flights - Get Command Usage

```console
msstore flights get <productId> <flightId>
```

#### Flights - Get Command Arguments

| Argument    | Description     |
| ----------- | --------------- |
| `productId` | The product ID. |
| `flightId`  | The flight ID.  |

#### Flights - Get Command Help

```console
msstore flights get --help
```

### Flights - Delete Command Usage

```console
msstore flights delete <productId> <flightId>
```

#### Flights - Delete Command Arguments

| Argument    | Description     |
| ----------- | --------------- |
| `productId` | The product ID. |
| `flightId`  | The flight ID.  |

#### Flights - Delete Command Help

```console
msstore flights delete --help
```

### Flights - Create Command Usage

```console
msstore flights create <productId> <friendlyName> --group-ids <group-ids>
```

#### Flights - Create Command Arguments

| Argument       | Description                      |
| -------------- | -------------------------------- |
| `productId`    | The product ID.                  |
| `friendlyName` | The friendly name of the flight. |

#### Flights - Create Command Options

| Option | Description |
|--------|-------------|
| -g, --group-ids | The group IDs to associate with the flight. |
  -r, --rank-higher-than | The flight ID to rank higher than. |

#### Flights - Create Command Help

```console
msstore flights create --help
```

## Flights - Submission Command

| Sub-Command                                            | Description                                                                                            |
| ------------------------------------------------------ | ------------------------------------------------------------------------------------------------------ |
| [get](#flights---submission-get-command-usage)         | Retrieves the existing package flight submission, either the existing draft or the last published one. |
| [delete](#flights---submission-delete-command-usage)   | Deletes the pending package flight submission from the store.                                          |
| [update](#flights---submission-update-command-usage)   | Updates the existing flight draft with the provided JSON.                                              |
| [publish](#flights---submission-publish-command-usage) | Starts the flight submission process for the existing Draft.                                           |
| [poll](#flights---submission-poll-command-usage)       | Polls until the existing flight submission is PUBLISHED or FAILED.                                     |
| [status](#flights---submission-status-command-usage)   | Retrieves the current status of the store flight submission.                                           |
| [rollout](#flights---submission---rollout-command)       | Execute flight rollout related operations.                                                             |

### Flights - Submission Get Command Usage

```console
msstore flights submission get <productId> <flightId>
```

#### Flights - Submission Get Command Arguments

| Argument    | Description     |
| ----------- | --------------- |
| `productId` | The product ID. |
| `flightId`  | The flight ID.  |

#### Flights - Submission Get Command Help

```console
msstore flights submission get --help
```

### Flights - Submission Delete Command Usage

```console
msstore flights submission delete <productId> <flightId>
```

#### Flights - Submission Delete Command Arguments

| Argument    | Description     |
| ----------- | --------------- |
| `productId` | The product ID. |
| `flightId`  | The flight ID.  |

#### Flights - Submission Delete Command Help

```console
msstore flights submission delete --help
```

### Flights - Submission Update Command Usage

```console
msstore flights submission update <productId> <flightId> <product>
```

#### Flights - Submission Update Command Arguments

| Argument    | Description                              |
| ----------- | ---------------------------------------- |
| `productId` | The product ID.                          |
| `flightId`  | The flight ID.                           |
| `product`   | The updated JSON product representation. |

#### Flights - Submission Update Command Options

| Option                   | Description                                                            |
| ------------------------ | ---------------------------------------------------------------------- |
| -s, --skipInitialPolling | Skip the initial polling before executing the action. [default: False] |

#### Flights - Submission Update Command Help

```console
msstore flights submission update --help
```

### Flights - Submission Publish Command Usage

```console
msstore flights submission publish <productId> <flightId>
```

#### Flights - Submission Publish Command Arguments

| Argument    | Description     |
| ----------- | --------------- |
| `productId` | The product ID. |
| `flightId`  | The flight ID.  |

#### Flights - Submission Publish Command Help

```console
msstore flights submission publish --help
```

### Flights - Submission Poll Command Usage

```console
msstore flights submission poll <productId> <flightId>
```

#### Flights - Submission Poll Command Arguments

| Argument    | Description     |
| ----------- | --------------- |
| `productId` | The product ID. |
| `flightId`  | The flight ID.  |

#### Flights - Submission Poll Command Help

```console
msstore flights submission poll --help
```

### Flights - Submission Status Command Usage

```console
msstore flights submission status <productId> <flightId>
```

#### Flights - Submission Status Command Arguments

| Argument    | Description     |
| ----------- |---------------- |
| `productId` | The product ID. |
| `flightId`  | The flight ID.  |

#### Flights - Submission Status Command Help

```console
msstore flights submission status --help
```

## Flights - Submission - Rollout Command

| Sub-Command                                                        | Description                                           |
| ------------------------------------------------------------------ | ----------------------------------------------------- |
| [get](#flights---submission---rollout-get-command-usage)           | Retrieves the flight rollout status of a submission.  |
| [update](#flights---submission---rollout-update-command-usage)     | Update the flight rollout percentage of a submission. |
| [halt](#flights---submission---rollout-halt-command-usage)         | Halts the flight rollout of a submission.             |
| [finalize](#flights---submission---rollout-finalize-command-usage) | Finalizes the flight rollout of a submission.         |

### Flights - Submission - Rollout Get Command Usage

```console
msstore flights submission rollout get <productId> <flightId>
```

#### Flights - Submission - Rollout Get Command Arguments

| Argument    | Description     |
| ----------- | -------------   |
| `productId` | The product ID. |
| `flightId`  | The flight ID.  |

#### Flights - Submission - Rollout Get Command Options

| Option             | Description        |
| ------------------ | ------------------ |
| -s, --submissionId | The submission ID. |

#### Flights - Submission - Rollout Get Command Help

```console
msstore flights submission rollout get --help
```

### Flights - Submission - Rollout Update Command Usage

```console
msstore flights submission rollout update <productId> <flightId> <percentage>
```

#### Flights - Submission - Rollout Update Command Arguments

| Argument     | Description                                                       |
| ------------ | ----------------------------------------------------------------- |
| `productId`  | The product ID.                                                   |
| `flightId`   | The flight ID.                                                    |
| `percentage` | The percentage of users that will receive the submission rollout. |

#### Flights - Submission - Rollout Update Command Options

| Option             | Description        |
| ------------------ | ------------------ |
| -s, --submissionId | The submission ID. |

#### Flights - Submission - Rollout Update Command Help

```console
msstore flights submission rollout update --help
```

### Flights - Submission - Rollout Halt Command Usage

```console
msstore flights submission rollout halt <productId> <flightId>
```

#### Flights - Submission - Rollout Halt Command Arguments

| Argument    | Description     |
| ----------- | --------------- |
| `productId` | The product ID. |
| `flightId`  | The flight ID.  |

#### Flights - Submission - Rollout Halt Command Options

| Option             | Description        |
| ------------------ | ------------------ |
| -s, --submissionId | The submission ID. |

#### Flights - Submission - Rollout Halt Command Help

```console
msstore flights submission rollout halt --help
```

### Flights - Submission - Rollout Finalize Command Usage

```console
msstore flights submission rollout finalize <productId> <flightId>
```

#### Flights - Submission - Rollout Finalize Command Arguments

| Argument    | Description     |
| ----------- | --------------- |
| `productId` | The product ID. |
| `flightId`  | The flight ID.  |

#### Flights - Submission - Rollout Finalize Command Options

| Option             | Description        |
| ------------------ | ------------------ |
| -s, --submissionId | The submission ID. |

#### Flights - Submission - Rollout Finalize Command Help

```console
msstore flights submission rollout finalize --help
```

## Init Command

The `init` command helps you setup your application to publish to the Microsoft Store. It currently supports the following application types:

- WinUI 
- .NET MAUI
- Flutter
- Electron
- React Native for Desktop
- PWA
- UWP

### Usage Examples

#### Windows App SDK/WinUI

```console
msstore init "C:\path\to\winui3_app"
```

#### UWP

```console
msstore init "C:\path\to\uwp_app"
```

#### .NET MAUI

```console
msstore init "C:\path\to\maui_app"
```

#### Flutter

```console
msstore init "C:\path\to\flutter_app"
```

#### Electron

```console
msstore init "C:\path\to\electron_app"
```

#### React Native for Desktop

```console
msstore init "C:\path\to\react_native_app"
```

> [!Note]
> For Electron, as well as React Native for Desktop projects, both `Npm` and `Yarn` are supported. The presence of the `Yarn` lock file (`yarn.lock`) will be used to determine which package manager to use, so make sure that you check in your lock file into your source control system.

#### PWA

```console
msstore init https://contoso.com --output .
```

#### Arguments

| Argument    | Description                                                                              |
| ----------- | ---------------------------------------------------------------------------------------- |
| `pathOrUrl` | The root directory path where the project file is, or a public URL that points to a PWA. |

#### Options

| Option                     | Description                                                                                                                                                                                                                    |
| -------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| -n, --publisherDisplayName | The Publisher Display Name used to configure the application. If provided, avoids an extra APIs call.                                                                                                                          |
| --package                  | If supported by the app type, automatically packs the project.                                                                                                                                                                 |
| --publish                  | If supported by the app type, automatically publishes the project. Implies '--package true'                                                                                                                                    |
| -f, --flightId | Specifies the Flight Id where the package will be published. |
| -prp, --packageRolloutPercentage | Specifies the rollout percentage of the package. The value must be between 0 and 100. |
| -a, --arch                 | The architecture(s) to build for. If not provided, the default architecture for the current OS, and project type, will be used. Allowed values: "x86", "x64", "arm64". Only used it used in conjunction with '--package true'. |
| -o, --output               | The output directory where the packaged app will be stored. If not provided, the default directory for each different type of app will be used.                                                                                |
| -ver, --version            | The version used when building the app. If not provided, the version from the project file will be used.                                                                                                                       |

## Package Command

Helps you package your Microsoft Store Application as an MSIX.

### Usage Examples

#### Windows App SDK/WinUI

```console
msstore package "C:\path\to\winui3_app"
```

#### WinUI

```console
msstore package "C:\path\to\uwp_app"
```

#### .NET MAUI

```console
msstore package "C:\path\to\maui_app"
```

#### Flutter

```console
msstore package "C:\path\to\flutter_app"
```

#### Electron

```console
msstore package "C:\path\to\electron_app"
```

#### React Native for Desktop

```console
msstore package "C:\path\to\react_native_app"
```

#### PWA

```console
msstore package "C:\path\to\pwa_app"
```

#### Arguments

| Option      | Description                                                                              |
| ----------- | ---------------------------------------------------------------------------------------- |
| `pathOrUrl` | The root directory path where the project file is, or a public URL that points to a PWA. |

#### Options

| Option          | Description                                                                                                                                                            |
| --------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| -o, --output    | The output directory where the packaged app will be stored. If not provided, the default directory for each different type of app will be used.                        |
| -a, --arch      | The architecture(s) to build for. If not provided, the default architecture for the current OS, and project type, will be used. Allowed values: "x86", "x64", "arm64". |
| -ver, --version | The version used when building the app. If not provided, the version from the project file will be used.                                                               |

## Publish Command

Publishes your Application to the Microsoft Store.

### Usage Examples

#### Windows App SDK/WinUI

```console
msstore publish "C:\path\to\winui3_app"
```

#### WinUI

```console
msstore publish "C:\path\to\uwp_app"
```

#### .NET MAUI

```console
msstore publish "C:\path\to\maui_app"
```

#### Flutter

```console
msstore publish "C:\path\to\flutter_app"
```

#### Electron

```console
msstore publish "C:\path\to\electron_app"
```

#### React Native for Desktop

```console
msstore publish "C:\path\to\react_native_app"
```

#### PWA

```console
msstore publish "C:\path\to\pwa_app"
```

#### Arguments

| Option      | Description                                                                              |
| ----------- | ---------------------------------------------------------------------------------------- |
| `pathOrUrl` | The root directory path where the project file is, or a public URL that points to a PWA. |

#### Options

| Option               | Description                                                                                                                                                                                  |
| -------------------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| -i, --inputFile | The path to the '.msix' or '.msixupload' file to be used for the publishing command. If not provided, the cli will try to find the best candidate based on the 'pathOrUrl' argument.              |
| -id, --appId         | Specifies the Application Id. Only needed if the project has not been initialized before with the 'init' command.                                                                            |
| -nc, --noCommit | Disables committing the submission, keeping it in draft state. |
| -f, --flightId | Specifies the Flight Id where the package will be published. |
| -prp, --packageRolloutPercentage | Specifies the rollout percentage of the package. The value must be between 0 and 100. |

## Flights Command

| Sub-Command                                                                                                     | Description          |
|-----------------------------------------------------------------------------------------------------------|----------------------|
| [list](#flights---list---usage) | Retrieves all the Flights for the specified Application. |
| [get](#flights---get---usage) | Retrieves a flight for the specified Application and flight. |
| [delete](#flights---delete---usage) | Deletes a flight for the specified Application and flight. |
| [create](#flights---create---usage) | Creates a flight for the specified Application and flight. |
| [submission](#flights---submission) | Execute flight submissions related tasks. |

#### Flights - List - Usage

```console
msstore flights list <productId>
```

#### Arguments

| Argument    | Description |
|-------------|-------------|
| `productId` | The product ID. |

#### Options

| Option                | Description                      |
| --------------------- | -------------------------------- |
| -v, --verbose         | Print verbose output.            |
| -?, -h, --help        | Show help and usage information. |

#### Flights - Get - Usage

```console
msstore flights get <productId> <flightId>
```

#### Arguments

| Argument    | Description |
|-------------|-------------|
| `productId` | The product ID. |
| `flightId` | The flight ID. |

#### Options

| Option                | Description                      |
| --------------------- | -------------------------------- |
| -v, --verbose         | Print verbose output.            |
| -?, -h, --help        | Show help and usage information. |

#### Flights - Delete - Usage

```console
msstore flights delete <productId> <flightId>
```

#### Arguments

| Argument    | Description |
|-------------|-------------|
| `productId` | The product ID. |
| `flightId` | The flight ID. |

#### Options

| Option                | Description                      |
| --------------------- | -------------------------------- |
| -v, --verbose         | Print verbose output.            |
| -?, -h, --help        | Show help and usage information. |

#### Flights - Create - Usage

```console
msstore flights create <productId> <friendlyName> --group-ids <group-ids>
```

#### Arguments

| Argument    | Description |
|-------------|-------------|
| `productId` | The product ID. |
| `friendlyName` | The friendly name of the flight. |

#### Options

| Option | Description |
|--------|-------------|
| -g, --group-ids | The group IDs to associate with the flight. |
| -r, --rank-higher-than | The flight ID to rank higher than. |
| -v, --verbose         | Print verbose output.            |
| -?, -h, --help        | Show help and usage information. |

### Flights - Submission

| Sub-Command                                                                                                     | Description          |
|-----------------------------------------------------------------------------------------------------------|----------------------|
| [get](#flights---submission---get---usage) | Retrieves the existing package flight submission, either the existing draft or the last published one. |
| [delete](#flights---submission---delete---usage) | Deletes the pending package flight submission from the store. |
| [update](#flights---submission---update---usage) | Updates the existing flight draft with the provided JSON. |
| [publish](#flights---submission---publish---usage) | Starts the flight submission process for the existing Draft. |
| [poll](#flights---submission---poll---usage) | Polls until the existing flight submission is PUBLISHED or FAILED. |
| [status](#flights---submission---status---usage) | Retrieves the current status of the store flight submission. |
| [rollout](#flights---submission---rollout) | Execute flight rollout related operations. |

#### Flights - Submission - Get - Usage

```console
msstore flights submission get <productId> <flightId>
```

#### Arguments

| Argument    | Description |
|-------------|-------------|
| `productId` | The product ID. |
| `flightId` | The flight ID. |

#### Options

| Option                | Description                      |
| --------------------- | -------------------------------- |
| -v, --verbose         | Print verbose output.            |
| -?, -h, --help        | Show help and usage information. |

#### Flights - Submission - Delete - Usage

```console
msstore flights submission delete <productId> <flightId>
```

#### Arguments

| Argument    | Description |
|-------------|-------------|
| `productId` | The product ID. |
| `flightId` | The flight ID. |

#### Options

| Option                | Description                      |
| --------------------- | -------------------------------- |
| --no-confirm          | Do not prompt for confirmation.  |
| -v, --verbose         | Print verbose output.            |
| -?, -h, --help        | Show help and usage information. |

#### Flights - Submission - Update - Usage

```console
msstore flights submission update <productId> <flightId> <product>
```

#### Arguments

| Argument    | Description |
|-------------|-------------|
| `productId` | The product ID. |
| `flightId` | The flight ID. |
| `product`   | The updated JSON product representation. |

#### Options

| Option | Description |
|--------|-------------|
| -s, --skipInitialPolling | Skip the initial polling before executing the action. [default: False] |
| -v, --verbose         | Print verbose output.            |
| -?, -h, --help        | Show help and usage information. |

#### Flights - Submission - Publish - Usage

```console
msstore flights publish <productId> <flightId>
```

#### Arguments

| Argument    | Description |
|-------------|-------------|
| `productId` | The product ID. |
| `flightId` | The flight ID. |

#### Options

| Option                | Description                      |
| --------------------- | -------------------------------- |
| -v, --verbose         | Print verbose output.            |
| -?, -h, --help        | Show help and usage information. |

#### Flights - Submission - Poll - Usage

```console
msstore flights poll <productId> <flightId>
```

#### Arguments

| Argument    | Description |
|-------------|-------------|
| `productId` | The product ID. |
| `flightId` | The flight ID. |

#### Options

| Option                | Description                      |
| --------------------- | -------------------------------- |
| -v, --verbose         | Print verbose output.            |
| -?, -h, --help        | Show help and usage information. |

#### Flights - Submission - Status - Usage

```console
msstore flights status <productId> <flightId>
```

#### Arguments

| Argument    | Description |
|-------------|-------------|
| `productId` | The product ID. |
| `flightId` | The flight ID. |

#### Options

| Option                | Description                      |
| --------------------- | -------------------------------- |
| -v, --verbose         | Print verbose output.            |
| -?, -h, --help        | Show help and usage information. |

### Flights - Submission - Rollout

| Sub-Command                                                                                                     | Description          |
|-----------------------------------------------------------------------------------------------------------|----------------------|
| [get](#flights---submission---rollout---get---usage) | Retrieves the flight rollout status of a submission. |
| [update](#flights---submission---rollout---update---usage) | Update the flight rollout percentage of a submission. |
| [halt](#flights---submission---rollout---halt---usage) | Halts the flight rollout of a submission. |
| [finalize](#flights---submission---rollout---finalize---usage) | Finalizes the flight rollout of a submission. |

#### Flights - Submission - Rollout - Get - Usage

```console
msstore flights submission rollout get <productId> <flightId>
```

#### Arguments

| Argument    | Description |
|-------------|-------------|
| `productId` | The product ID. |
| `flightId` | The flight ID. |

#### Options

| Option | Description |
|--------|-------------|
| -s, --submissionId | The submission ID. |
| -v, --verbose         | Print verbose output.            |
| -?, -h, --help        | Show help and usage information. |

#### Flights - Submission - Rollout - Update - Usage

```console
msstore flights submission rollout update <productId> <flightId> <percentage>
```

#### Arguments

| Argument    | Description |
|-------------|-------------|
| `productId` | The product ID. |
| `flightId` | The flight ID. |
| `percentage` | The percentage of users that will receive the submission rollout. |

#### Options

| Option | Description |
|--------|-------------|
| -s, --submissionId | The submission ID. |
| -v, --verbose         | Print verbose output.            |
| -?, -h, --help        | Show help and usage information. |

#### Flights - Submission - Rollout - Halt - Usage

```console
msstore flights submission rollout halt <productId> <flightId>
```

#### Arguments

| Argument    | Description |
|-------------|-------------|
| `productId` | The product ID. |
| `flightId` | The flight ID. |

#### Options

| Option | Description |
|--------|-------------|
| -s, --submissionId | The submission ID. |
| -v, --verbose         | Print verbose output.            |
| -?, -h, --help        | Show help and usage information. |

#### Flights - Submission - Rollout - Finalize - Usage

```console
msstore flights submission rollout finalize <productId> <flightId>
```

#### Arguments

| Argument    | Description |
|-------------|-------------|
| `productId` | The product ID. |
| `flightId` | The flight ID. |

#### Options

| Option | Description |
|--------|-------------|
| -s, --submissionId | The submission ID. |
| -v, --verbose         | Print verbose output.            |
| -?, -h, --help        | Show help and usage information. |

## CI/CD Environments

The Microsoft Store Developer CLI (preview) supports running in CI/CD environments. This means that you can use the Microsoft Store Developer CLI (preview) in your CI/CD pipelines to, for example, automatically publish your applications to the Microsoft Store.

The firststep to achieve this it to install the Microsoft Store Developer CLI (preview) on your CI/CD environment. You can find instructions on how to do this [here](./commands.md#installation).

After installing the Microsoft Store Developer CLI (preview), you have to configure your environment to be able to run commands. You can do this by running the `msstore reconfigure` command with the specific parameters that identify your partner center account (_TenantId_, _SellerId_, _ClientId_). You also need to provide either a _ClientSecret_ or a _Certificate_.

It is very important to hide these credentials, as they will be visible in the logs of your CI/CD pipeline. You can do this by using **secrets**. Each CI/CD pipeline system have different names for these secrets. For example, Azure DevOps call them [_Secret Variables_](/azure/devops/pipelines/process/set-secret-variables), but GitHub Action calls them [Encrypted Secrets](https://docs.github.com/actions/security-guides/encrypted-secrets). Create one **secret** for each of the parameters (_TenantId_, _SellerId_, _ClientId_, and _ClientSecret_ or a _Certificate_), and then use the `reconfigure` command to setup your environment.

For example:

### Azure DevOps

```yaml
- task: UseMSStoreCLI@0
  displayName: Setup Microsoft Store Developer CLI
- script: msstore reconfigure --tenantId $(PARTNER_CENTER_TENANT_ID) --sellerId $(PARTNER_CENTER_SELLER_ID) --clientId $(PARTNER_CENTER_CLIENT_ID) --clientSecret $(PARTNER_CENTER_CLIENT_SECRET)
  displayName: Configure Microsoft Store Developer CLI
```

### GitHub Actions

```yaml
- name: Setup Microsoft Store Developer CLI
  uses: microsoft/microsoft-store-apppublisher@v1.1
- name: Configure Microsoft Store Developer CLI
  run: msstore reconfigure --tenantId ${{ secrets.PARTNER_CENTER_TENANT_ID }} --sellerId ${{ secrets.PARTNER_CENTER_SELLER_ID }} --clientId ${{ secrets.PARTNER_CENTER_CLIENT_ID }} --clientSecret ${{ secrets.PARTNER_CENTER_CLIENT_SECRET }}
```

Once this command is executed, the Microsoft Store Developer CLI (preview) will be configured to use the credentials provided. You can now use the Microsoft Store Developer CLI (preview) in your CI/CD pipeline.
