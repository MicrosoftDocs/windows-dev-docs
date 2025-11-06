---
description: How to run the Microsoft Store Developer CLI (preview) commands for MSI/EXE apps.
title: Microsoft Store Developer CLI (preview) Commands (MSI/EXE)
ms.date: 11/06/2025
ms.topic: article
zone_pivot_groups: msstoredevcli-installer-packaging
---

# Commands (MSI/EXE)

## Installation

:::zone pivot="msstoredevcli-installer-windows"

### Step 1: Install .NET Windows Runtime

If you haven't done so already, install the latest version of the [.NET 8 Desktop Runtime](https://dotnet.microsoft.com/download/dotnet/8.0). This is a requirement to run the Microsoft Store Developer CLI.

The easiest way to install it is to use _winget_:

```console
winget install Microsoft.DotNet.DesktopRuntime.8
```

### Step 2: Install the Microsoft Store Developer CLI on Windows

You can download the Microsoft Store Developer CLI from the [Microsoft Store](https://www.microsoft.com/store/apps/9P53PC5S0PHJ). Alternatively, you can use _winget_:

```console
winget install "Microsoft Store Developer CLI"
```

:::zone-end
:::zone pivot="msstoredevcli-installer-macos"

### Step 1: Install .NET macOS Runtime

If you haven't done so already, install the latest version of the [.NET 8 Runtime](https://dotnet.microsoft.com/download/dotnet/8.0). This is a requirement to run the Microsoft Store Developer CLI.

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

If you haven't done so already, install the latest version of the [.NET 8 Runtime](https://dotnet.microsoft.com/download/dotnet/8.0). This is a requirement to run the Microsoft Store Developer CLI.

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

Set the Publisher Display Name property.

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

## Submission Command

| Sub-Command                                                | Description                                                 |
| -----------------------------------------------------------| ----------------------------------------------------------- |
| [status](#submission---status---usage)                     | Gets the status of a submission.                            |
| [get](#submission---get---usage)                           | Gets the metadata and package info of a specific submission.|
| [getListingAssets](#submission---getlistingassets---usage) | Gets the listing assets of a specific submission.           |
| [updateMetadata](#submission---updatemetadata---usage)     | Updates the metadata of a specific submission.              |
| [update](#submission---update---usage)                     | Updates the package info of a specific submission.          |
| [poll](#submission---poll---usage)                         | Polls the status of a submission.                           |
| [publish](#submission---publish---usage)                   | Publishes a specific submission.                            |

#### Submission - Status - Usage

```console
msstore submission status <productId>
```

#### Arguments

| Argument    | Description           |
| ----------- | --------------------- |
| `productId` | The Partner center ID.|

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
| `productId` | The Partner center ID.|

#### Options

| Option                | Description                                              |
| --------------------- | -------------------------------------------------------- |
| -l, --language        | Select which language you want to retrieve. [default: en]|
| -v, --verbose         | Print verbose output.                                    |
| -?, -h, --help        | Show help and usage information.                         |

#### Submission - GetListingAssets - Usage

```console
msstore submission getListingAssets <productId>
```

#### Arguments

| Argument    | Description           |
| ----------- | --------------------- |
| `productId` | The Partner center ID.|

#### Options

| Option                | Description                                              |
| --------------------- | -------------------------------------------------------- |
| -l, --language        | Select which language you want to retrieve. [default: en]|
| -v, --verbose         | Print verbose output.                                    |
| -?, -h, --help        | Show help and usage information.                         |

#### Submission - UpdateMetadata - Usage

```console
msstore submission updateMetadata <productId> <metadata>
```

#### Arguments

| Argument    | Description                               |
| ----------- | ----------------------------------------- |
| `productId` | The Partner center ID.                    |
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

| Argument    | Description                                        |
| ----------- | -------------------------------------------------- |
| `productId` | The Partner center ID.                             |
| `package`   | The updated JSON representation of the app package.|

#### Options

| Option                   | Description                                                            |
| ------------------------ | ---------------------------------------------------------------------- |
| -s, --skipInitialPolling | Skip the initial polling before executing the action. [default: False] |
| -v, --verbose            | Print verbose output.                                                  |
| -?, -h, --help           | Show help and usage information.                                       |

#### Submission - Poll - Usage

```console
msstore submission poll <productId>
```

#### Arguments

| Argument    | Description           |
| ----------- | --------------------- |
| `productId` | The Partner center ID.|

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
| `productId` | The Partner center ID.|

#### Options

| Option                | Description                      |
| --------------------- | -------------------------------- |
| -v, --verbose         | Print verbose output.            |
| -?, -h, --help        | Show help and usage information. |

## CI/CD Environments

The Microsoft Store Developer CLI (preview) supports running in CI/CD environments. This means that you can use the Microsoft Store Developer CLI (preview) in your CI/CD pipelines to, for example, automatically publish your applications to the Microsoft Store.

The firststep to achieve this it to install the Microsoft Store Developer CLI (preview) on your CI/CD environment. You can find instructions on how to do this [here](./commands-exe.md#installation).

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
