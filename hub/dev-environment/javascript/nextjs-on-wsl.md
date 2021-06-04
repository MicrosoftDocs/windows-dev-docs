---
title: Install Next.js on Windows
description: A guide to help you get started using the Next.js web frameworks on Windows Subsystem for Linux.
author: mattwojo 
ms.author: mattwoj 
manager: jken
ms.topic: article
keywords: Next.js, NextJS, Node.js, windows 10, microsoft, learning nodejs, node on windows, node on wsl, node on linux on windows, install node on windows, nodejs with vs code, develop with node on windows, develop with nodejs on windows, install node on WSL, NodeJS on Windows Subsystem for Linux
ms.localizationpriority: medium
ms.date: 03/30/2021
---

# Get started with Next.js on Windows

A guide to help you install the Next.js web framework and get up and running on Windows 10.

Next.js is a framework for creating server-rendered JavaScript apps based on React.js, Node.js, Webpack and Babel.js. It is basically a project boilerplate for React, crafted with attention to best practices, that allows you to create "universal" web apps in a simple, consistent way, with hardly any configuration. These "universal" server-rendered web apps are also sometimes called “isomorphic”, meaning that code is shared between the client and server.

To learn more about React and other JavaScript frameworks based on React, see the [React overview](./react-overview.md) page.

## Prerequisites

This guide assumes that you've already completed the steps to set up your Node.js development environment, including:

- Install the latest version of Windows 10 (Version 1903+, Build 18362+)
- [Install Windows Subsystem for Linux (WSL)](/windows/wsl/install-win10), including a Linux distribution (like Ubuntu) and make sure it is running in WSL 2 mode. You can check this by opening PowerShell and entering: `wsl -l -v`
- [Install Node.js on WSL 2](./nodejs-on-wsl.md): This includes a version manager, package manager, Visual Studio Code, and the Remote Development extension.

We recommend using the Windows Subsystem for Linux when working with NodeJS apps for better performance speed, system call compatibility, and for parody when running Linux servers or Docker containers.

> [!IMPORTANT]
> Installing a Linux distribution with WSL will create a directory for storing files: `\\wsl\Ubuntu-20.04` (substitute Ubuntu-20.04 with whatever Linux distribution you're using). To open this directory in Windows File Explorer, open your WSL command line, select your home directory using `cd ~`, then enter the command `explorer.exe .` Be careful not to install NodeJS or store files that you will be working with on the mounted C drive (`/mnt/c/Users/yourname$`). Doing so will significantly slow down your install and build times.

## Install Next.js

To install Next.js, which includes installing next, react, and react-dom:

1. Open a WSL command line (ie. Ubuntu).

2. Create a new project folder: `mkdir NextProjects` and enter that directory: `cd NextProjects`.

3. Install Next.js and create a project (replacing 'my-next-app' with whatever you'd like to call your app): `npx create-next-app my-next-app`.

4. Once the package has been installed, change directories into your new app folder, `cd my-next-app`, then use `code .` to open your Next.js project in VS Code. This will allow you to look at the Next.js framework that has been created for your app. Notice that VS Code opened your app in a WSL-Remote environment (as indicated in the green tab on the bottom-left of your VS Code window). This means that while you are using VS Code for editing on the Windows OS, it is still running your app on the Linux OS.

    ![WSL-Remote Extension](../../images/wsl-remote-extension.png)

5. There are 3 commands you need to know once Next.js is installed:

    - `npm run dev` for running a development instance with hot-reloading, file watching and task re-running.
    - `npm run build` for compiling your project.
    - `npm start` for starting your app in production mode.

    Open the WSL terminal integrated in VS Code (**View > Terminal**). Make sure that the terminal path is pointed to your project directory (ie. `~/NextProjects/my-next-app$`). Then try running a development instance of your new Next.js app using: `npm run dev`

6. The local development server will start and once your project pages are done building, your terminal will display "compiled successfully - ready on [http://localhost:3000](http://localhost:3000)". Select this localhost link to open your new Next.js app in a web browser.

    ![Your Next.js app running in localhost:3000](../../images/next-app.png)

7. Open the `pages/index.js` file in your VS Code editor. Find the page title `<h1 className='title'>Welcome to Next.js!</h1>` and change it to `<h1 className='title'>This is my new Next.js app!</h1>`. With your web browser still open to localhost:3000, save your change and notice the hot-reloading feature automatically compile and update your change in the browser.

8. Let's see how Next.js handles errors. Remove the `</h1>` closing tag so that your title code now looks like this: `<h1 className='title'>This is my new Next.js app!`. Save this change and notice that a "Failed to compile" error will display in your browser, and in your terminal, letting your know that a closing tag for `<h1>` is expected. Replace the `</h1>` closing tag, save, and the page will reload.

You can use VS Code's debugger with your Next.js app by selecting the F5 key, or by going to **View > Debug** (Ctrl+Shift+D) and **View > Debug Console** (Ctrl+Shift+Y) in the menu bar. If you select the gear icon in the Debug window, a launch configuration (`launch.json`) file will be created for you to save debugging setup details. To learn more, see [VS Code Debugging](https://code.visualstudio.com/docs/nodejs/nodejs-debugging).

![VS Code debug window and launch.json config icon](../../images/vscode-debug-launch-configuration.png)

To learn more about Next.js, see the [Next.js docs](https://nextjs.org/docs).
