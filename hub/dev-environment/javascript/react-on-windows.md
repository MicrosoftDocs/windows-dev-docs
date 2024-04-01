---
title: Install React on Windows
description: Install a React development environment on Windows.
author: mattwojo 
ms.author: mattwoj 
manager: jken
ms.topic: article
ms.date: 03/01/2024
---

# Install React directly on Windows


This guide will walk through installing React on a Linux distribution (ie. Ubuntu) running on the Windows Subsystem for Linux (WSL) using the [vite](https://vitejs.dev/) frontend tooling.

We recommend following these instructions if you are new to React and just interested in learning. If you are creating a single-page app (SPA) that you would like to use Bash commands or tools with and/or plan to deploy to a Linux server, we recommend that you [install with vite on Windows Subsystem for Linux (WSL)](./react-on-wsl.md).

For more general information about React, deciding between React (web apps), React Native (mobile apps), and React Native for Windows (desktop apps), see the [React overview](./react-overview.md).

## Create your React app

To install Create React App:

1. Open a terminal(Windows Command Prompt or PowerShell).
2. Create a new project folder: `mkdir ReactProjects` and enter that directory: `cd ReactProjects`.
3. Install React using vite :

    ```powershell
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