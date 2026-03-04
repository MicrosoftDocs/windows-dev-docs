---
title: Packaging Electron apps as MSIX
description: Learn how to create MSIX packages for distributing your Electron app with Windows APIs using the winapp CLI.
ms.date: 02/20/2026
ms.topic: how-to
---

# Packaging your Electron app for distribution

This guide shows you how to create an MSIX package for distributing your Electron app with Windows APIs.

## Prerequisites

- Completed the [development environment setup](electron-setup.md)
- Verified your app runs correctly with `npm start`

## Prepare for packaging

Before packaging, configure your build tool to exclude temporary files from the final build:

- `.winapp/` folder
- `winapp.yaml`
- Certificate files (`.pfx`)
- Debug symbols (`.pdb`)
- C# build artifacts (`obj/`, `bin/` folders)
- MSIX packages (`*.msix`)

Verify that your `appxmanifest.xml` `Executable` attribute points to the correct `.exe` file.

## Option 1: Using winapp CLI directly (recommended)

This approach gives you more control and works with any Electron packager.

### Build your Electron app

```bash
npx electron-forge package
```

This creates a production version in the `./out/` folder.

### Create the MSIX package

```bash
npx winapp pack .\out\<your-app-folder> --output .\out --cert .\devcert.pfx --manifest .\appxmanifest.xml
```

Replace `<your-app-folder>` with the actual folder name created by Electron Forge (for example, `my-windows-app-win32-x64`).

> [!TIP]
> Add these commands to your `package.json` scripts for convenience:
>
> ```json
> {
>   "scripts": {
>     "package-msix": "npx electron-forge package && npx winapp pack ./out/my-windows-app-win32-x64 --output ./out --cert ./devcert.pfx --manifest appxmanifest.xml"
>   }
> }
> ```

## Option 2: Using Electron Forge MSIX Maker

If you're already using Electron Forge, you can integrate MSIX packaging directly.

### Install the MSIX Maker

```bash
npm install --save-dev @electron-forge/maker-msix
```

### Configure forge.config.js

```javascript
module.exports = {
  makers: [
    {
      name: '@electron-forge/maker-msix',
      config: {
        appManifest: '.\\appxmanifest.xml',
        windowsSignOptions: {
          certificateFile: '.\\devcert.pfx',
          certificatePassword: 'password'
        }
      }
    }
  ],
};
```

### Update appxmanifest.xml

Update the `Executable` path to point to the `app` folder:

```xml
<Applications>
  <Application Id="myApp"
    Executable="app\my-app.exe"
    EntryPoint="Windows.FullTrustApplication">
  </Application>
</Applications>
```

### Create the MSIX package

```bash
npm run make
```

The MSIX package will be created in `./out/make/msix/`.

## Install and test the MSIX

Install the development certificate (one-time setup, run as administrator):

```bash
npx winapp cert install .\devcert.pfx
```

Install the MSIX package:

```bash
Add-AppxPackage .\my-windows-app.msix
```

Your app will appear in the Start Menu.

## Distribution options

### Microsoft Store

Submit your app for the widest distribution and automatic updates. Learn more: [Publish your app to the Microsoft Store](/windows/apps/publish/).

### Direct download

Host the MSIX package on your website. Sign it with a trusted certificate authority (CA) certificate.

### Enterprise distribution

Distribute via Company Portal (Intune), direct download, or sideloading. Learn more: [Distribute apps outside the Store](/windows/msix/desktop/managing-your-msix-deployment-overview).

### App Installer

Create an `.appinstaller` file for automatic updates. Learn more: [App Installer file overview](/windows/msix/app-installer/app-installer-file-overview).

## Related topics

- [Setting up Electron for Windows API development](electron-setup.md)
- [winapp CLI reference](../usage.md)
- [MSIX packaging documentation](/windows/msix/)
