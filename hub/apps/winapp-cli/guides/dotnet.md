---
title: Using winapp CLI with .NET
description: Learn how to use the winapp CLI with .NET applications (console, WPF, WinForms) to add package identity, access Windows APIs, and package as MSIX.
ms.date: 02/20/2026
ms.topic: how-to
---

# Using winapp CLI with .NET

> [!NOTE]
> This guide works for most .NET project types. The steps have been tested with both console and UI-based projects like WPF.

This guide demonstrates how to use the winapp CLI with a .NET application to debug with package identity and package your application as an MSIX.

Package identity is a core concept in the Windows app model. It allows your application to access specific Windows APIs (like Notifications, Security, AI APIs, etc.), have a clean install/uninstall experience, and more.

## Prerequisites

1. **.NET SDK**: Install the .NET SDK:

    ```powershell
    winget install Microsoft.DotNet.SDK.10 --source winget
    ```

2. **winapp CLI**: Install the `winapp` tool via winget:

    ```powershell
    winget install Microsoft.winappcli --source winget
    ```

## 1. Create a new .NET app

Start by creating a simple .NET console application:

```powershell
dotnet new console -n dotnet-app
cd dotnet-app
```

Run it to make sure everything is working:

```powershell
dotnet run
```

## 2. Update code to check identity

First, update your project file to target a specific Windows SDK version. Open `dotnet-app.csproj` and change the `TargetFramework`:

```xml
<TargetFramework>net10.0-windows10.0.26100.0</TargetFramework>
```

Now replace the contents of `Program.cs`:

```csharp
using Windows.ApplicationModel;

try
{
    var package = Package.Current;
    var familyName = package.Id.FamilyName;
    Console.WriteLine($"Package Family Name: {familyName}");
}
catch (InvalidOperationException)
{
    Console.WriteLine("Not packaged");
}
```

## 3. Run without identity

```powershell
dotnet run
```

You should see "Not packaged".

## 4. Initialize project with winapp CLI

The `winapp init` command automatically detects `.csproj` files and runs a .NET-specific setup:

```powershell
winapp init
```

When prompted:

- **Package name**: Press Enter to accept the default
- **Publisher name**: Press Enter to accept the default or enter your name
- **Description**: Press Enter to accept the default or enter a description
- **Version**: Press Enter to accept 1.0.0.0
- **Entry point**: Press Enter to accept the default (dotnet-app.exe)
- **Windows App SDK setup**: Select Stable, Preview, or Experimental

This command:

- Updates the `TargetFramework` in your `.csproj` to a supported Windows TFM (if needed)
- Adds `Microsoft.WindowsAppSDK` and `Microsoft.Windows.SDK.BuildTools` NuGet package references
- Creates `appxmanifest.xml` and `Assets` folder for your app identity

> [!NOTE]
> Unlike native/C++ projects, the .NET flow does **not** create a `winapp.yaml` file. NuGet packages are managed directly via your `.csproj`. Use `dotnet restore` to restore packages after cloning.

## 5. Debug with identity

1. **Build the executable**:

    ```powershell
    dotnet build -c Debug
    ```

2. **Apply debug identity**:

    ```powershell
    winapp create-debug-identity .\bin\Debug\net10.0-windows10.0.26100.0\dotnet-app.exe
    ```

3. **Run the executable** (do not use `dotnet run` as it might rebuild):

    ```powershell
    .\bin\Debug\net10.0-windows10.0.26100.0\dotnet-app.exe
    ```

You should see:

```
Package Family Name: dotnet-app_12345abcde
```

### Automating debug identity (optional)

Add this target to your `.csproj` file:

```xml
<Target Name="ApplyDebugIdentity" AfterTargets="Build" Condition="'$(Configuration)' == 'Debug'">
    <Exec Command="winapp create-debug-identity &quot;$(TargetDir)$(TargetName).exe&quot;"
          WorkingDirectory="$(ProjectDir)"
          IgnoreExitCode="false" />
</Target>
```

## 6. Using Windows App SDK

If you ran `winapp init`, `Microsoft.WindowsAppSDK` was already added as a NuGet package reference. Update `Program.cs` to use the Windows App Runtime API:

```csharp
using Windows.ApplicationModel;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            var package = Package.Current;
            var familyName = package.Id.FamilyName;
            Console.WriteLine($"Package Family Name: {familyName}");

            var runtimeVersion = Microsoft.Windows.ApplicationModel.WindowsAppRuntime.RuntimeInfo.AsString;
            Console.WriteLine($"Windows App Runtime Version: {runtimeVersion}");
        }
        catch (InvalidOperationException)
        {
            Console.WriteLine("Not packaged");
        }
    }
}
```

## 7. Package with MSIX

1. **Build for release**:

    ```powershell
    dotnet build -c Release
    ```

2. **Generate a development certificate**:

    ```powershell
    winapp cert generate --if-exists skip
    ```

3. **Package and sign**:

    ```powershell
    winapp pack .\bin\Release\net10.0-windows10.0.26100.0 --manifest .\appxmanifest.xml --cert .\devcert.pfx
    ```

4. **Install the certificate** (run as administrator):

    ```powershell
    winapp cert install .\devcert.pfx
    ```

5. **Install** by double-clicking the generated `.msix` file.

> [!TIP]
> - The Microsoft Store signs the MSIX for you, no need to sign before submission.
> - You may need separate MSIX packages for each architecture: `dotnet build -c Release -r win-x64` or `dotnet build -c Release -r win-arm64`.

### Automating MSIX packaging (optional)

Add this target to your `.csproj`:

```xml
<Target Name="PackageMsix" AfterTargets="Build" Condition="'$(Configuration)' == 'Release'">
    <Exec Command="winapp pack &quot;$(TargetDir.TrimEnd('\'))&quot; --cert &quot;$(ProjectDir)devcert.pfx&quot;"
          WorkingDirectory="$(ProjectDir)"
          IgnoreExitCode="false" />
</Target>
```

## Related topics

- [winapp CLI reference](../usage.md)
- [winapp CLI overview](../index.md)
- [Windows App SDK documentation](/windows/apps/windows-app-sdk/)
