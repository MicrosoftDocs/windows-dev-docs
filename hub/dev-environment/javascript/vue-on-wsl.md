---
title: Install Vue.js on WSL
description: A guide to help you get started using the Vue.js web frameworks on Windows Subsystem for Linux.
author: mattwojo 
ms.author: mattwoj 
manager: jken
ms.topic: article
keywords: Vue, Vue.js, windows 10, install vue, install vue on windows, install vue with windows, install vue for windows, web app with vue, vue dev environment, install vue on windows subsystem for linux, install vue with wsl, install vue on wsl, install vue on ubuntu on windows
ms.localizationpriority: medium
ms.date: 08/18/2021
---

# Install Vue.js on Windows Subsystem for Linux

A guide to help you set up a Vue.js development environment on Windows by installing the Vue.js web framework on Windows Subsystem for Linux (WSL). Learn more on the [Vue.js overview](./vue-overview.md) page.

Vue can be installed [directly on Windows](./vue-on-windows.md) or on WSL. We generally recommend installing on WSL if you are planning to interact with a NodeJS backend, want parity with a Linux production server, or plan to follow along with a tutorial that utilizes Bash commands.

## Prerequisites

- [Install Windows Subsystem for Linux (WSL)](/windows/wsl/install-win10), including a Linux distribution (like Ubuntu) and make sure it is running in WSL 2 mode. You can check this by opening PowerShell and entering: `wsl -l -v`
- [Install Node.js on WSL 2](./nodejs-on-wsl.md): This includes a version manager, package manager, Visual Studio Code, and the Remote Development extension. The Node Package Manager (npm) is used to install Vue.js.

> [!IMPORTANT]
> Installing a Linux distribution with WSL will create a directory for storing files: `\\wsl\Ubuntu-20.04` (substitute Ubuntu-20.04 with whatever Linux distribution you're using). To open this directory in Windows File Explorer, open your WSL command line, select your home directory using `cd ~`, then enter the command `explorer.exe .` Be careful not to install or store files that you will be working with on the mounted C drive (`/mnt/c/Users/yourname$`). Doing so will significantly slow down your install and build times.

## Install Vue.js

To install Vue.js on WSL:

1. Open a WSL command line (ie. Ubuntu).

2. Create a new project folder: `mkdir VueProjects` and enter that directory: `cd VueProjects`.

3. Install Vue.js using Node Package Manager (npm):

```bash
npm install vue
```

Check the version number you have installed by using the command: `vue --version`.

> [!NOTE]
> To install Vue.js using a CDN, rather than NPM, see the [Vue.js install docs](https://vuejs.org/v2/guide/installation.html#CDN).

## Install Vue CLI

Vue CLI is a toolkit for working with Vue in your terminal / command line. It enables you to quickly scaffold a new project (vue create), prototype new ideas (vue serve), or manage projects using a graphical user interface (vue ui). Vue CLI is a globally installed npm package that handles some of the build complexities (like using Babel or Webpack) for you. *If you are not building a new single-page app, you may not need or want Vue CLI.*

To install Vue CLI, use npm. You must use the `-g` flag to globally install in order to upgrade (`vue upgrade --next`):

```bash
npm install -g @vue/cli
```

To learn more about additional plugins that can be added (such as linting or Apollo for integrating GraphQL), visit [Vue CLI plugins](https://cli.vuejs.org/guide/#cli-plugins) in the Vue CLI docs.

## Additional resources

- [Vue docs](https://vuejs.org/)
- [Vue.js overview](./vue-overview.md)
- [Install Vue.js on Windows](./vue-on-windows.md)
- [Install Nuxt.js](./nuxtjs-on-wsl.md)
- Microsoft Learn online course: [Take your first steps with Vue.js](/training/paths/vue-first-steps/)
- Try a [Vue tutorial with VS Code](https://code.visualstudio.com/docs/nodejs/vuejs-tutorial)
