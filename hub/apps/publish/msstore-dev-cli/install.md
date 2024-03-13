---
description: How to install the Microsoft Store Command Line Interface.
title: Microsoft Store Developer CLI (preview) Installation
ms.date: 11/30/2022
ms.topic: article
ms.localizationpriority: medium
zone_pivot_groups: msstoredevcli-installer-packaging
---

# Installation

:::zone pivot="msstoredevcli-installer-windows"

## Step 1: Install .NET Windows Runtime

If you haven't done so already, install the latest version of the [.NET 8 Desktop Runtime](https://dotnet.microsoft.com/download/dotnet/8.0). This is a requirement to run the Microsoft Store Developer CLI.

The easiest way to install it is to use *winget*:

```console
winget install Microsoft.DotNet.DesktopRuntime.8
```

## Step 2: Install the Microsoft Store Developer CLI

You can download the Microsoft Store Developer CLI from the [Microsoft Store](https://www.microsoft.com/store/apps/9P53PC5S0PHJ). Alternatively, you can use *winget*:

```console
winget install "Microsoft Store Developer CLI"
```

:::zone-end
:::zone pivot="msstoredevcli-installer-macos"

## Step 1: Install .NET macOS Runtime

If you haven't done so already, install the latest version of the [.NET 8 Runtime](https://dotnet.microsoft.com/download/dotnet/8.0). This is a requirement to run the Microsoft Store Developer CLI.

## Step 2: Install the Microsoft Store Developer CLI

You can download the macOS *.tar.gz* for your specific architecture (x64 or Arm64) from the [Microsoft Store Developer CLI releases page](https://aka.ms/msstoredevcli/releases). Once downloaded, extract the archive and put it in your PATH, however you want to do that, for example:

```console
mkdir MSStoreCLI
curl https://github.com/microsoft/msstore-cli/releases/latest/download/MSStoreCLI-osx-x64.tar.gz -o MSStoreCLI-osx-x64.tar.gz
tar -xvf MSStoreCLI-osx-x64.tar.gz -C ./MSStoreCLI
sudo cp -R MSStoreCLI/. /usr/local/bin
```

Alternatively, you can use *brew*:

```console
brew install microsoft/msstore-cli/msstore-cli
```

:::zone-end
:::zone pivot="msstoredevcli-installer-linux"

## Step 1: Install .NET Linux Runtime

If you haven't done so already, install the latest version of the [.NET 8 Runtime](https://dotnet.microsoft.com/download/dotnet/8.0). This is a requirement to run the Microsoft Store Developer CLI.

## Step 2: Install the Microsoft Store Developer CLI

You can download the Linux *.tar.gz* for your specific architecture (x64 or Arm64) from the [Microsoft Store Developer CLI releases page](https://aka.ms/msstoredevcli/releases). Once downloaded, extract the archive and put it in your PATH, however you want to do that, for example:

```console
mkdir MSStoreCLI
wget https://github.com/microsoft/msstore-cli/releases/latest/download/MSStoreCLI-linux-x64.tar.gz
tar -xvf MSStoreCLI-linux-x64.tar.gz -C ./MSStoreCLI
sudo cp -R MSStoreCLI/. /usr/local/bin
```

Alternatively, you can use *brew*:

```console
brew install microsoft/msstore-cli/msstore-cli
```

:::zone-end