---
title: Using winapp CLI with Rust
description: Learn how to use the winapp CLI with Rust applications to add package identity, access Windows APIs, and package as MSIX.
ms.date: 02/20/2026
ms.topic: how-to
---

# Using winapp CLI with Rust

This guide demonstrates how to use the winapp CLI with a Rust application to debug with package identity and package your application as an MSIX.

Package identity is a core concept in the Windows app model. It allows your application to access specific Windows APIs (like Notifications, Security, AI APIs, etc.), have a clean install/uninstall experience, and more.

## Prerequisites

1. **Rust Toolchain**: Install Rust using [rustup](https://rustup.rs/) or winget:

    ```powershell
    winget install Rustlang.Rustup --source winget
    ```

2. **winapp CLI**: Install the `winapp` tool via winget:

    ```powershell
    winget install microsoft.winappcli --source winget
    ```

## 1. Create a new Rust app

```powershell
cargo new rust-app
cd rust-app
```

Run it to make sure everything is working:

```powershell
cargo run
```

## 2. Update code to check identity

Add the `windows` dependency to your `Cargo.toml`:

```powershell
cargo add windows --features ApplicationModel
```

Replace the contents of `src/main.rs`:

```rust
use windows::ApplicationModel::Package;

fn main() {
    match Package::Current() {
        Ok(package) => {
            match package.Id() {
                Ok(id) => match id.FamilyName() {
                    Ok(name) => println!("Package Family Name: {}", name),
                    Err(e) => println!("Error getting family name: {}", e),
                },
                Err(e) => println!("Error getting package ID: {}", e),
            }
        }
        Err(_) => println!("Not packaged"),
    }
}
```

## 3. Run without identity

```powershell
cargo run
```

You should see "Not packaged".

## 4. Initialize project with winapp CLI

```powershell
winapp init
```

When prompted:

- **Package name**: Press Enter to accept the default (rust-app)
- **Publisher name**: Press Enter to accept the default or enter your name
- **Version**: Press Enter to accept 1.0.0.0
- **Entry point**: Press Enter to accept the default (rust-app.exe)
- **Setup SDKs**: Select "Do not setup SDKs"

This creates `appxmanifest.xml` and `Assets` folder for your app identity.

## 5. Debug with identity

1. **Build the executable**:

    ```powershell
    cargo build
    ```

2. **Apply debug identity**:

    ```powershell
    winapp create-debug-identity .\target\debug\rust-app.exe
    ```

3. **Run the executable** (do not use `cargo run` as it might rebuild):

    ```powershell
    .\target\debug\rust-app.exe
    ```

You should see:

```
Package Family Name: rust-app_12345abcde
```

## 6. Package with MSIX

1. **Build for release**:

    ```powershell
    cargo build --release
    ```

2. **Prepare package directory**:

    ```powershell
    mkdir dist
    copy .\target\release\rust-app.exe .\dist\
    ```

3. **Generate a development certificate**:

    ```powershell
    winapp cert generate --if-exists skip
    ```

4. **Package and sign**:

    ```powershell
    winapp pack .\dist --cert .\devcert.pfx
    ```

5. **Install the certificate** (run as administrator):

    ```powershell
    winapp cert install .\devcert.pfx
    ```

6. **Install and run**:

    ```powershell
    Add-AppxPackage .\rust-app.msix
    rust-app
    ```

> [!TIP]
> - Sign your MSIX with a code signing certificate from a Certificate Authority for production distribution.
> - The Microsoft Store signs the MSIX for you, no need to sign before submission.
> - You may need separate MSIX packages for each architecture you support (x64, Arm64).

## Related topics

- [winapp CLI reference](../usage.md)
- [winapp CLI overview](../index.md)
- [Getting started with Rust on Windows](/windows/dev-environment/rust/)
