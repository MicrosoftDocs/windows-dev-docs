---
description: How to run the Microsoft Store Developer CLI (preview) commands for MSI/EXE apps.
title: Microsoft Store Developer CLI (preview) Commands (MSI/EXE)
ms.date: 11/08/2024
ms.topic: article
zone_pivot_groups: msstoredevcli-installer-packaging
---

# Commands

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

### Info Command - Usage

```console
msstore info
```

### Info Command - Options

| Option        | Description           |
| ------------- | --------------------- |
| -v, --verbose | Print verbose output. |

## Reconfigure Command

Re-configure the Microsoft Store Developer CLI. You can provide either a Client Secret or a Certificate. Certificates can be provided either through its Thumbprint or by providing a file path (with or without a password).

### Reconfigure - Usage

```console
msstore reconfigure
```

### Reconfigure - Options

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

## Settings Command

Change settings of the Microsoft Store Developer CLI.

### Settings - Usage

```console
msstore settings
```

| Sub-Command | Description                                                               |
| ----------- | ------------------------------------------------------------------------- |
| setpdn      | Set the Publisher Display Name property that is used by the init command. |

### Settings - Options

| Option                | Description                                       |
| --------------------- | ------------------------------------------------- |
| -t, --enableTelemetry | Enable (empty/true) or Disable (false) telemetry. |
| -v, --verbose         | Print verbose output.                             |

### Settings - SetPDN Command Usage

```console
msstore settings setpdn <publisherDisplayName>
```

#### Arguments

| Argument               | Description                                                    |
| ---------------------- | -------------------------------------------------------------- |
| `publisherDisplayName` | The Publisher Display Name property that will be set globally. |

#### Help

```console
msstore settings setpdn --help
```

## Apps Command

| Sub-Command                        | Description                                 |
| ---------------------------------- | ------------------------------------------- |
| [list](#apps---list-command-usage) | Lists all the applications in your account. |
| [get](#apps---get-command-usage)   | Gets the details of a specific application. |

### Apps - List Command Usage

```console
msstore apps list
```

#### List Command - Help

```console
msstore apps list --help
```

### Apps - Get Command Usage

```console
msstore apps get <productId>
```

#### Apps - Get Command Arguments

| Argument    | Description     |
| ----------- | --------------- |
| `productId` | The product ID. |

#### Apps - Get Command Help

```console
msstore apps get --help
```

## Submission Command

| Sub-Command                                                      | Description                                       |
| ---------------------------------------------------------------- | ------------------------------------------------- |
| [status](#submission---status-command-usage)                     | Gets the status of a submission.                  |
| [get](#submission---get-command-usage)                           | Gets the details of a specific submission.        |
| [getListingAssets](#submission---getlistingassets-command-usage) | Gets the listing assets of a specific submission. |
| [updateMetadata](#submission---updatemetadata-command-usage)     | Updates the metadata of a specific submission.    |
| [update](#submission---update-command-usage)                     | Updates the details of a specific submission.     |
| [poll](#submission---poll-command-usage)                         | Polls the status of a submission.                 |
| [publish](#submission---publish-command-usage)                   | Publishes a specific submission.                  |
| [delete](#submission---delete-command-usage)                     | Deletes a specific submission.                    |

### Submission - Status Command Usage

```console
msstore submission status <productId>
```

#### Submission - Status Command Arguments

| Argument    | Description     |
| ----------- | --------------- |
| `productId` | The product ID. |

#### Submission - Status Command Help

```console
msstore submission status --help
```

### Submission - Get Command Usage

```console
msstore submission get <productId>
```

#### Submission - Get Command Arguments

| Argument    | Description     |
| ----------- | --------------- |
| `productId` | The product ID. |

#### Submission - Get Command Options

| Option         | Description                                                                            |
| -------------- | -------------------------------------------------------------------------------------- |
| -m, --module   | Select which module you want to retrieve ('availability', 'listings' or 'properties'). |
| -l, --language | Select which language you want to retrieve. [default: en]                              |

#### Submission - Get Command Help

```console
msstore submission get --help
```

### Submission - GetListingAssets Command Usage

Retrieves the existing draft listing assets from the store submission.

```console
msstore submission getListingAssets <productId>
```

#### Submission - GetListingAssets Command Arguments

| Argument    | Description     |
| ----------- | --------------- |
| `productId` | The product ID. |

#### Submission - GetListingAssets Command Options

| Option         | Description                                               |
| -------------- | --------------------------------------------------------- |
| -l, --language | Select which language you want to retrieve. [default: en] |

#### Submission - GetListingAssets Command Help

```console
msstore submission getListingAssets --help
```

### Submission - UpdateMetadata Command Usage

```console
msstore submission updateMetadata <productId> <metadata>
```

#### Submission - UpdateMetadata Command Arguments

| Argument    | Description                               |
| ----------- | ----------------------------------------- |
| `productId` | The product ID.                           |
| `metadata`  | The updated JSON metadata representation. |

#### Submission - UpdateMetadata Command Options

| Option                   | Description                                                            |
| ------------------------ | ---------------------------------------------------------------------- |
| -s, --skipInitialPolling | Skip the initial polling before executing the action. [default: False] |

#### Submission - UpdateMetadata Command Help

```console
msstore submission updateMetadata --help
```

### Submission - Update Command Usage

```console
msstore submission update <productId> <product>
```

#### Submission - Update Command Arguments

| Argument    | Description                              |
| ----------- | ---------------------------------------- |
| `productId` | The product ID.                          |
| `product`   | The updated JSON product representation. |

#### Submission - Update Command Options

| Option                   | Description                                                            |
| ------------------------ | ---------------------------------------------------------------------- |
| -s, --skipInitialPolling | Skip the initial polling before executing the action. [default: False] |

#### Submission - Update Command Help

```console
msstore submission update --help
```

### Submission - Poll Command Usage

```console
msstore submission poll <productId>
```

#### Submission - Poll Command Arguments

| Argument    | Description     |
| ----------- | --------------- |
| `productId` | The product ID. |

#### Submission - Poll Command Help

```console
msstore submission poll --help
```

### Submission - Publish Command Usage

```console
msstore submission publish <productId>
```

#### Submission - Publish Command Arguments

| Argument    | Description     |
| ----------- | --------------- |
| `productId` | The product ID. |

#### Submission - Publish Command Help

```console
msstore submission publish --help
```

### Submission - Delete Command Usage

Deletes the pending submission from the store.

#### Submission - Delete Command Arguments

| Argument    | Description     |
| ----------- | --------------- |
| `productId` | The product ID. |

#### Submission - Delete Command Options

| Option       | Description                                      |
| ------------ | ------------------------------------------------ |
| --no-confirm | Do not prompt for confirmation. [default: False] |

#### Submission - Delete Command Help

```console
msstore submission delete --help
```

## CI/CD Environments

The Microsoft Store Developer CLI (preview) supports running in CI/CD environments. This means that you can use the Microsoft Store Developer CLI (preview) in your CI/CD pipelines to, for example, automatically publish your applications to the Microsoft Store.

The firststep to achieve this it to install the Microsoft Store Developer CLI (preview) on your CI/CD environment. You can find instructions on how to do this [here](./commands-win32.md#installation).

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
  uses: microsoft/setup-msstore-cli@v1
- name: Configure Microsoft Store Developer CLI
  run: msstore reconfigure --tenantId ${{ secrets.PARTNER_CENTER_TENANT_ID }} --sellerId ${{ secrets.PARTNER_CENTER_SELLER_ID }} --clientId ${{ secrets.PARTNER_CENTER_CLIENT_ID }} --clientSecret ${{ secrets.PARTNER_CENTER_CLIENT_SECRET }}
```

Once this command is executed, the Microsoft Store Developer CLI (preview) will be configured to use the credentials provided. You can now use the Microsoft Store Developer CLI (preview) in your CI/CD pipeline.
