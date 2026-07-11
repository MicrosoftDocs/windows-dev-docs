---
title: Hosted apps in Windows App SDK
description: Learn how to use the hosted app model to create lightweight apps that inherit their executable and runtime from a host app.
author: GrantMeStrength
ms.author: jken
ms.topic: overview
ms.date: 07/08/2026
---

# Hosted apps in Windows App SDK

The hosted app model lets you create lightweight apps that inherit their executable and entry point from a host app. The hosted app is a separate package that contains content (scripts, assets, configuration) but relies on the host's runtime to execute.

## How hosted apps work

A **host app** declares a `HostRuntime` extension in its manifest, making its executable available to other packages. A **hosted app** declares a dependency on that host and specifies which content to run. When the user launches the hosted app, Windows starts the host's executable and passes the hosted app's content as context.

This model is useful for:

- **Script engines** — A host provides the runtime (for example, Python, Node.js), and hosted apps are individual scripts or applications.
- **Game engines** — A host provides the game engine, and hosted apps are individual games.
- **App frameworks** — A host provides a framework runtime, and hosted apps are content that runs on it.

> [!IMPORTANT]
> Both host and hosted apps require **MSIX package identity**. The hosted app is a signed MSIX package, but its executable comes from the host.

## Declare a host runtime

In the host app's `Package.appxmanifest`, add a `HostRuntime` extension:

```xml
<Package xmlns:uap10="http://schemas.microsoft.com/appx/manifest/uap/windows10/10" ...>
  <Applications>
    <Application Id="App" Executable="MyHost.exe" EntryPoint="...">
      <Extensions>
        <uap10:Extension Category="windows.hostRuntime">
          <uap10:HostRuntime Id="MyHostRuntime" />
        </uap10:Extension>
      </Extensions>
    </Application>
  </Applications>
</Package>
```

The `Id` value is what hosted apps reference to declare their dependency.

## Declare a hosted app

In the hosted app's manifest, declare the host runtime dependency and specify a `TrustLevel` and `RuntimeBehavior`:

```xml
<Package xmlns:uap10="http://schemas.microsoft.com/appx/manifest/uap/windows10/10" ...>
  <Dependencies>
    <uap10:HostRuntimeDependency Name="HostAppPackageFamilyName"
                                  Publisher="CN=HostPublisher"
                                  MinVersion="1.0.0.0" />
  </Dependencies>
  <Applications>
    <Application Id="App"
                 uap10:HostId="MyHostRuntime"
                 uap10:Parameters="&quot;script.py&quot;">
    </Application>
  </Applications>
</Package>
```

Key attributes:

| Attribute | Description |
|-----------|-------------|
| `HostRuntimeDependency.Name` | Package family name of the host app |
| `HostId` | Matches the `HostRuntime.Id` in the host app |
| `Parameters` | Command-line arguments passed to the host executable |

## Create a hosted app package

To create a hosted app package:

1. Create a standard MSIX project or use `MakeAppx.exe`.
2. Include only your content files (scripts, assets, configuration).
3. Do not include an executable — it comes from the host.
4. Declare the `HostRuntimeDependency` in the manifest.
5. Sign the package with a trusted certificate for production. During development, you can register the package unsigned (see below).

## Unsigned package registration (development)

During development, you can register an unsigned hosted app package:

```powershell
Add-AppxPackage -Path "HostedApp.msix" -AllowUnsigned
```

> [!NOTE]
> Unsigned package registration is for development only. For production, sign the package with a trusted certificate.

## Example: Python script host

A Python runtime app can serve as a host:

1. **Host app** — Packages `python.exe` and declares `HostRuntime Id="PythonHost"`.
2. **Hosted app** — Contains a Python script (`app.py`) and declares `HostId="PythonHost"` with `Parameters="app.py"`.
3. When the user launches the hosted app, Windows runs the host's `python.exe app.py` with the hosted app's package directory as the working context.

## Related content

- [Extensibility overview](extensibility-overview.md)
- [App extensions](app-extensions.md)
- [MSIX documentation](/windows/msix/)
