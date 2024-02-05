---
title: Set up continuous integration for your WinUI 3 app
description: How to automate WinUI 3 builds with continuous integration to produce sideload and/or Store packages.
ms.date: 12/13/2021
zone_pivot_groups: winui3-version-c#-only
ms.topic: article
keywords: ci, continuous integration, automated builds, github actions, pipelines, winui 3, winui3, windows app sdk
ms.localizationpriority: medium
---
# Set up continuous integration for your WinUI 3 app

You can use GitHub Actions to set up continuous integration builds for WinUI 3 projects. In this article, we'll look at different ways to do this. We'll also show you how to perform these tasks by using the command line so that you can integrate with any other build system.

## Prerequisites

* Start with a single-project MSIX WinUI 3 app or migrate your project to [use single-project MSIX](../windows-app-sdk/single-project-msix.md).
* [Sign up for GitHub](https://github.com/join) and [create a repository](https://github.com/new) if you haven't done so already.

::: zone pivot="winui3-packaged-csharp"

## Step 1: Set up your certificate

MSIX apps must be signed in order to be installed. If you already have a certificate, you can skip this step. You can easily create a test certificate by opening your app in Visual Studio, right clicking your WinUI 3 project, and selecting **Package and Publish** -> **Create App Packages**.

Then select **Next** to move to the **Select signing method** page, and click the **Create...** button to create a new certificate. Choose the publisher name and **leave the password field blank**, and create the certificate.

Then, close/cancel out of the dialogs and notice that a new **.pfx** file has been created in your project. This is the certificate you can sign your MSIX with!

## Step 2: Add your certificate to the Actions secrets

You should avoid submitting certificates to your repo if at all possible, and git ignores them by default. To manage the safe handling of sensitive files like certificates, GitHub supports [secrets](https://docs.github.com/actions/automating-your-workflow-with-github-actions/creating-and-using-encrypted-secrets).

To upload a certificate for your automated build:

1. **Encode your certificate as a Base 64 string**: Open PowerShell to the directory that contains your certificate, and execute the following command, replacing the pfx file name with your certificate's file name.

```powershell
$pfx_cert = Get-Content 'App1_TemporaryKey.pfx' -AsByteStream

[System.Convert]::ToBase64String($pfx_cert) | Out-File 'App1_TemporaryKey_Base64.txt'
```

2. In your GitHub repository, go to the **Settings** page and click **Secrets** on the left.
3. Click **New repository secret**, name it `BASE64_ENCODED_PFX`, and copy/paste the text from the text file in the PowerShell output into the secret value.

## Step 3: Set up your workflow

Next, in your repository, go to the Actions tab and create a new workflow. Chose the **set up a workflow yourself** option instead of one of the workflow templates.

Copy/paste the following into your workflow file, and then update...

1. **Solution_Name** to the name of your solution
2. **dotnet-version** to either 5.0 or 6.0 depending on your project

> [!NOTE]
> For the step uploading the artifact (the last step below), if the build output doesn't land in a folder that contains your solution, then replace `env.Solution_Name` with `github.workspace` (the GitHub actions Workspace folder).

```yml
# This workflow will build, sign, and package a WinUI 3 MSIX desktop application
# built on .NET.

name: WinUI 3 MSIX app

on:
  push:
    branches: [ main ]
  pull_request:
    branches: [ main ]

jobs:

  build:

    strategy:
      matrix:
        configuration: [Release]
        platform: [x64, x86]

    runs-on: windows-latest  # For a list of available runner types, refer to
                             # https://help.github.com/en/actions/reference/workflow-syntax-for-github-actions#jobsjob_idruns-on

    env:
      Solution_Name: your-solution-name                         # Replace with your solution name, i.e. App1.sln.

    steps:
    - name: Checkout
      uses: actions/checkout@v2
      with:
        fetch-depth: 0

    # Install the .NET Core workload
    - name: Install .NET Core
      uses: actions/setup-dotnet@v1
      with:
        dotnet-version: 6.0.x

    # Add  MSBuild to the PATH: https://github.com/microsoft/setup-msbuild
    - name: Setup MSBuild.exe
      uses: microsoft/setup-msbuild@v1.0.2

    # Restore the application to populate the obj folder with RuntimeIdentifiers
    - name: Restore the application
      run: msbuild $env:Solution_Name /t:Restore /p:Configuration=$env:Configuration
      env:
        Configuration: ${{ matrix.configuration }}

    # Decode the base 64 encoded pfx and save the Signing_Certificate
    - name: Decode the pfx
      run: |
        $pfx_cert_byte = [System.Convert]::FromBase64String("${{ secrets.BASE64_ENCODED_PFX }}")
        $certificatePath = "GitHubActionsWorkflow.pfx"
        [IO.File]::WriteAllBytes("$certificatePath", $pfx_cert_byte)

    # Create the app package by building and packaging the project
    - name: Create the app package
      run: msbuild $env:Solution_Name /p:Configuration=$env:Configuration /p:Platform=$env:Platform /p:UapAppxPackageBuildMode=$env:Appx_Package_Build_Mode /p:AppxBundle=$env:Appx_Bundle /p:PackageCertificateKeyFile=GitHubActionsWorkflow.pfx /p:AppxPackageDir="$env:Appx_Package_Dir" /p:GenerateAppxPackageOnBuild=true
      env:
        Appx_Bundle: Never
        Appx_Package_Build_Mode: SideloadOnly
        Appx_Package_Dir: Packages\
        Configuration: ${{ matrix.configuration }}
        Platform: ${{ matrix.platform }}

    # Remove the pfx
    - name: Remove the pfx
      run: Remove-Item -path GitHubActionsWorkflow.pfx

    # Upload the MSIX package: https://github.com/marketplace/actions/upload-a-build-artifact
    - name: Upload MSIX package
      uses: actions/upload-artifact@v2
      with:
        name: MSIX Package
        path: ${{ env.Solution_Name }}\\Packages

```

## Step 4: Commit the workflow and watch it run!

Commit the workflow file to your main branch, and then go to the Actions tab on your GitHub repository and watch your workflow run! It should successfully run and produce artifacts that contain your built MSIX app.

## Building from command line

If you want to build your solution by using the command line, or by using any other CI system, run MSBuild with these arguments. The `GenerateAppxPackageOnBuild` property causes the MSIX package to be generated.

```powershell
/p:AppxPackageDir="Packages"
/p:UapAppxPackageBuildMode=SideloadOnly
/p:AppxBundle=Never
/p:GenerateAppxPackageOnBuild=true
```

::: zone-end
::: zone pivot="winui3-unpackaged-csharp"

## Step 1: Set up your workflow

In your GitHub repository, go to the Actions tab and create a new workflow. Chose the **set up a workflow yourself** option instead of one of the workflow templates.

Copy/paste the following into your workflow file, and then update...

1. **Solution_Name** to the name of your solution
2. **dotnet-version** to either 5.0 or 6.0 depending on your project

> [!NOTE]
> For the step uploading the artifact (the last step below), if the build output doesn't land in a folder that contains your solution, then replace `env.Solution_Name` with `github.workspace` (the GitHub actions Workspace folder).

```yml
# This workflow will build and publish a WinUI 3 unpackaged desktop application
# built on .NET.

name: WinUI 3 unpackaged app

on:
  push:
    branches: [ main ]
  pull_request:
    branches: [ main ]

jobs:

  build:

    strategy:
      matrix:
        configuration: [Release]
        platform: [x64, x86]

    runs-on: windows-latest  # For a list of available runner types, refer to
                             # https://help.github.com/en/actions/reference/workflow-syntax-for-github-actions#jobsjob_idruns-on

    env:
      Solution_Name: your-solution-name                         # Replace with your solution name, i.e. App1.sln.

    steps:
    - name: Checkout
      uses: actions/checkout@v2
      with:
        fetch-depth: 0

    # Install the .NET Core workload
    - name: Install .NET Core
      uses: actions/setup-dotnet@v1
      with:
        dotnet-version: 6.0.x

    # Add  MSBuild to the PATH: https://github.com/microsoft/setup-msbuild
    - name: Setup MSBuild.exe
      uses: microsoft/setup-msbuild@v1.0.2

    # Restore the application to populate the obj folder with RuntimeIdentifiers
    - name: Restore the application
      run: msbuild $env:Solution_Name /t:Restore /p:Configuration=$env:Configuration
      env:
        Configuration: ${{ matrix.configuration }}

    # Create the app by building and publishing the project
    - name: Create the app
      run: msbuild $env:Solution_Name /t:Publish /p:Configuration=$env:Configuration /p:Platform=$env:Platform
      env:
        Configuration: ${{ matrix.configuration }}
        Platform: ${{ matrix.platform }}

    # Upload the app
    - name: Upload app
      uses: actions/upload-artifact@v2
      with:
        name: Upload app
        path: ${{ env.Solution_Name }}\\bin
```

## Step 2: Commit the workflow and watch it run!

Commit the workflow file to your main branch, and then go to the Actions tab on your GitHub repository and watch your workflow run! It should successfully run and produce artifacts that contain your built app.

## Building from command line

If you want to build your solution by using the command line, or by using any other CI system, run MSBuild with the `/t:Publish` argument.

::: zone-end
