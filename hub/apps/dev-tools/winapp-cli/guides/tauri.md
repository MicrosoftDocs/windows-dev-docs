---
title: Using winapp CLI with Tauri
description: Learn how to use the winapp CLI with Tauri cross-platform applications to add package identity, access Windows APIs, and package as MSIX.
ms.date: 02/20/2026
ms.topic: how-to
---

# Using winapp CLI with Tauri

This guide demonstrates how to use the winapp CLI with a Tauri application to debug with package identity and package your application as an MSIX.

Package identity is a core concept in the Windows app model. It allows your application to access specific Windows APIs (like Notifications, Security, AI APIs, etc.), have a clean install/uninstall experience, and more.

## Prerequisites

1. **Windows 11**
2. **Node.js** - `winget install OpenJS.NodeJS --source winget`
3. **winapp CLI** - `winget install microsoft.winappcli --source winget`

## 1. Create a new Tauri app

```powershell
npm create tauri-app@latest
```

Follow the prompts (Project name: `tauri-app`, Frontend language: `JavaScript`, Package manager: `npm`).

Navigate to your project directory and install dependencies:

```powershell
cd tauri-app
npm install
```

Run the app:

```powershell
npm run tauri dev
```

## 2. Update code to check identity

### Backend changes (Rust)

Add the `windows` dependency. Open `src-tauri/Cargo.toml`:

```toml
[target.'cfg(windows)'.dependencies]
windows = { version = "0.58", features = ["ApplicationModel"] }
```

Open `src-tauri/src/lib.rs` and add the command:

```rust
#[tauri::command]
fn get_package_family_name() -> String {
    #[cfg(target_os = "windows")]
    {
        use windows::ApplicationModel::Package;
        match Package::Current() {
            Ok(package) => {
                match package.Id() {
                    Ok(id) => match id.FamilyName() {
                        Ok(name) => name.to_string(),
                        Err(_) => "Error retrieving Family Name".to_string(),
                    },
                    Err(_) => "Error retrieving Package ID".to_string(),
                }
            }
            Err(_) => "No package identity".to_string(),
        }
    }
    #[cfg(not(target_os = "windows"))]
    {
        "Not running on Windows".to_string()
    }
}
```

Register the command in the `run` function:

```rust
pub fn run() {
    tauri::Builder::default()
        .plugin(tauri_plugin_opener::init())
        .invoke_handler(tauri::generate_handler![greet, get_package_family_name])
        .run(tauri::generate_context!())
        .expect("error while running tauri application");
}
```

### Frontend changes (JavaScript)

In `src/index.html`, add a paragraph to display the result:

```html
<p id="pfn-msg"></p>
```

In `src/main.js`, invoke the command:

```javascript
const { invoke } = window.__TAURI__.core;

async function checkPackageIdentity() {
  const pfn = await invoke("get_package_family_name");
  const pfnMsgEl = document.querySelector("#pfn-msg");

  if (pfn !== "No package identity" && !pfn.startsWith("Error")) {
    pfnMsgEl.textContent = `Package family name: ${pfn}`;
  } else {
    pfnMsgEl.textContent = `Not running with package identity`;
  }
}

window.addEventListener("DOMContentLoaded", () => {
  checkPackageIdentity();
});
```

Run the app to confirm it shows "Not running with package identity":

```powershell
npm run tauri dev
```

## 3. Initialize project with winapp CLI

```powershell
winapp init
```

When prompted:

- **Package name**: Press Enter to accept the default (tauri-app)
- **Publisher name**: Press Enter to accept the default or enter your name
- **Version**: Press Enter to accept 1.0.0.0
- **Entry point**: Press Enter to accept the default (tauri-app.exe)
- **Setup SDKs**: Select "Do not setup SDKs"

## 4. Debug with identity

Add a script to `package.json`:

```json
"scripts": {
    "tauri:dev:withidentity": "cargo build --manifest-path src-tauri/Cargo.toml && winapp create-debug-identity src-tauri/target/debug/tauri-app.exe && .\\src-tauri\\target\\debug\\tauri-app.exe"
}
```

Run it:

```powershell
npm run tauri:dev:withidentity
```

You should see the app display a Package Family Name.

## 5. Package with MSIX

1. **Build for release**:

    ```powershell
    npm run tauri build
    ```

2. **Prepare package directory**:

    ```powershell
    mkdir dist
    copy .\src-tauri\target\release\tauri-app.exe .\dist\
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

6. **Install** by double-clicking the generated `.msix` file. Launch from the start menu.

## Related topics

- [winapp CLI reference](../usage.md)
- [winapp CLI overview](../index.md)
