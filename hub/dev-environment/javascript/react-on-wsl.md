---
title: Install React on Windows Subsystem for Linux
description: Install React on Windows Subsystem for Linux (WSL) and start developing web apps with React components and the create-react-app toolchain.
ms.topic: article
keywords: react, install react on wsl, install react on linux on windows, react and windows, react development with windows, react with windows 10, react on windows, react with wsl
ms.date: 03/30/2021
---

# Install React on Windows Subsystem for Linux

This guide will walk through installing React on a Linux distribution (ie. Ubuntu) running on the Windows Subsystem for Linux (WSL) using the [vite](https://vitejs.dev/) frontend tooling.

We recommend following these instructions if you are creating a single-page app (SPA) that you would like to use Bash commands or tools with and/or plan to deploy to a Linux server or use Docker containers. If you are brand new to React and just interested in learning, you may want to consider [installing with vite directly on Windows](./react-on-windows.md).

For more general information about React, deciding between React (web apps), React Native (mobile apps), and React Native for Windows (desktop apps), see the [React overview](./react-overview.md).

## Prerequisites

- Install the latest version of Windows 10 (Version 1903+, Build 18362+) or Windows 11
- [Install Windows Subsystem for Linux (WSL)](/windows/wsl/install-win10), including a Linux distribution (like Ubuntu) and make sure it is running in WSL 2 mode. You can check this by opening PowerShell and entering: `wsl -l -v`
- [Install Node.js on WSL 2](./nodejs-on-wsl.md): These instructions use Node Version Manager (nvm) for installation, you will need a recent version of NodeJS to run vite, as well as a recent version of Node Package Manager (npm).

> [!IMPORTANT]
> Installing a Linux distribution with WSL will create a directory for storing files: `\\wsl\Ubuntu-20.04` (substitute Ubuntu-20.04 with whatever Linux distribution you're using). To open this directory in Windows File Explorer, open your WSL command line, select your home directory using `cd ~`, then enter the command `explorer.exe .` Be careful not to install NodeJS or store files that you will be working with on the mounted C drive (`/mnt/c/Users/yourname$`). Doing so will significantly slow down your install and build times.

## Install React

To install the full React toolchain on WSL, we recommend using vite.

1. Open a WSL command line (ie. Ubuntu).

2. Create a new project folder: `mkdir ReactProjects` and enter that directory: `cd ReactProjects`.

3. Install React using vite :

    ```bash
    npm create vite@latest my-react-app -- --template react
    ```

4. Once installed, change directories into your new app ("my-react-app" or whatever you've chosen to call it): `cd my-react-app`, install the dependencies: `npm install` and then start your local development server: `npm run dev`

    This command will start up the Node.js server and launch a new browser window displaying your app. You can use **Ctrl + c** to stop running the React app in your command line.

> [!NOTE]
> Vite includes a frontend build pipeline using [Babel](https://babeljs.io/), [esbuild](https://esbuild.github.io/) and [Rollup](https://rollupjs.org/), but doesn't handle backend logic or databases. If you are seeking to build a server-rendered website with React that uses a Node.js backend, we recommend [installing Next.js](./nextjs-on-wsl.md), rather than this Vite installation, which is intended more for single-page apps(SPAs). You also may want to consider [installing Gatsby](./gatsby-on-wsl.md) if you want to build a static content-oriented website.

6. When you're ready to deploy your web app to production, running `npm run build` to  create a build of your app in the "dist" folder. You can learn more in the [Deploying a Static Site](https://vitejs.dev/guide/static-deploy.html).

## Additional resources

- [React docs](https://react.dev)
- [Vite](https://vitejs.dev/)
- [Install Next.js](./nextjs-on-wsl.md)
- [Install Gatsby](./gatsby-on-wsl.md)
- [Install React Native for Windows](https://microsoft.github.io/react-native-windows/docs/getting-started)
- [Install NodeJS on Windows](./nodejs-on-windows.md)
- [Install NodeJS on WSL](./nodejs-on-wsl.md)
- Try the tutorial: [Using React in Visual Studio Code](https://code.visualstudio.com/docs/nodejs/reactjs-tutorial)
- Try the [React learning path](/training/paths/react/)