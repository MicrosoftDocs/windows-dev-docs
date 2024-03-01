---
title: Install React on Windows Subsystem for Linux
description: Install React on Windows Subsystem for Linux (WSL) and start developing web apps with React components and the create-react-app toolchain.
author: mattwojo
ms.author: mattwoj 
manager: jken
ms.topic: article
keywords: react, install react on wsl, install react on linux on windows, react and windows, react development with windows, react with windows 10, react on windows, react with wsl
ms.date: 03/30/2021
---

# Install React on Windows Subsystem for Linux

This guide will walk through installing React on a Linux distribution (ie. Ubuntu) running on the Windows Subsystem for Linux (WSL) using the [create-react-app](https://github.com/facebook/create-react-app) toolchain.

We recommend following these instructions if you are creating a single-page app (SPA) that you would like to use Bash commands or tools with and/or plan to deploy to a Linux server or use Docker containers. If you are brand new to React and just interested in learning, you may want to consider [installing with create-react-app directly on Windows](./react-on-windows.md).

For more general information about React, deciding between React (web apps), React Native (mobile apps), and React Native for Windows (desktop apps), see the [React overview](./react-overview.md).

## Prerequisites

- Install the latest version of Windows 10 (Version 1903+, Build 18362+) or Windows 11
- [Install Windows Subsystem for Linux (WSL)](/windows/wsl/install-win10), including a Linux distribution (like Ubuntu) and make sure it is running in WSL 2 mode. You can check this by opening PowerShell and entering: `wsl -l -v`
- [Install Node.js on WSL 2](./nodejs-on-wsl.md): These instructions use Node Version Manager (nvm) for installation, you will need a recent version of NodeJS to run create-react-app, as well as a recent version of Node Package Manager (npm). For exact version requirements, see the [Create React App website](https://reactjs.org/docs/create-a-new-react-app.html#create-react-app).

> [!IMPORTANT]
> Installing a Linux distribution with WSL will create a directory for storing files: `\\wsl\Ubuntu-20.04` (substitute Ubuntu-20.04 with whatever Linux distribution you're using). To open this directory in Windows File Explorer, open your WSL command line, select your home directory using `cd ~`, then enter the command `explorer.exe .` Be careful not to install NodeJS or store files that you will be working with on the mounted C drive (`/mnt/c/Users/yourname$`). Doing so will significantly slow down your install and build times.

## Install React

To install the full React toolchain on WSL, we recommend using create-react-app:

1. Open a WSL command line (ie. Ubuntu).

2. Create a new project folder: `mkdir ReactProjects` and enter that directory: `cd ReactProjects`.

3. Install React using [npx](https://www.npmjs.com/package/npx):

    ```bash
    npx create-react-app my-app
    ```

    >[!NOTE]
    > [npx](https://www.npmjs.com/package/npx) is the package runner used by npm to execute packages in place of a global install. It basically creates a temporary install of React so that with each new project you are using the most recent version of React (not whatever version was current when you performed the global install above). Using the NPX package runner to execute a package can also help reduce the pollution of installing lots of packages on your machine.

4. This will first ask for your permission to temporarily install create-react-app and it's associated packages. Once completed, change directories into your new app ("my-app" or whatever you've chosen to call it): `cd my-app`.

5. Start your new React app:

    ```Bash
    npm start
    ```

    This command will start up the Node.js server and launch a new browser window displaying your app. You can use **Ctrl + c** to stop running the React app in your command line.

    > [!NOTE]
    > Create React App includes a frontend build pipeline using [Babel](https://babeljs.io/) and [webpack](https://webpack.js.org/), but doesn't handle backend logic or databases. If you are seeking to build a server-rendered website with React that uses a Node.js backend, we recommend [installing Next.js](./nextjs-on-wsl.md), rather than this create-react-app installation, which is intended more for single-page apps. You also may want to consider [installing Gatsby](./gatsby-on-wsl.md) if you want to build a static content-oriented website.

6. When you're ready to deploy your web app to production, running `npm run build` will create a build of your app in the "build" folder. You can learn more in the [Create React App User Guide](https://create-react-app.dev/docs/deployment).

## Add React to an existing web app

Since React is a JavaScript library that is, in its most basic form, just a collection of text files, you can create React apps without installing any tools or libraries on your computer. You may only want to add a few "sprinkles of interactivity" to a web page and not need build tooling. You can add a React component by just adding a plain `<script>` tag on an HTML page. Follow the ["Add React in One Minute"](https://reactjs.org/docs/add-react-to-a-website.html) steps in the React docs.

## Additional resources

- [React docs](https://reactjs.org/)
- [Create React App docs](https://create-react-app.dev/docs/getting-started)
- [Install Next.js](./nextjs-on-wsl.md)
- [Install Gatsby](./gatsby-on-wsl.md)
- [Install React Native for Windows](https://microsoft.github.io/react-native-windows/docs/getting-started)
- [Install NodeJS on Windows](./nodejs-on-windows.md)
- [Install NodeJS on WSL](./nodejs-on-wsl.md)
- Try the tutorial: [Using React in Visual Studio Code](https://code.visualstudio.com/docs/nodejs/reactjs-tutorial)
- Try the [React learning path](/training/paths/react/)
