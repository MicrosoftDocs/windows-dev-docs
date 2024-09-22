---
title: Install Vue.js directly on Windows
description: A guide to help you get started using the Vue.js web frameworks directly on Windows.
ms.topic: article
ms.date: 03/30/2021
---

# Install Vue.js directly on Windows

A guide to help you set up a Vue.js development environment on Windows. Learn more on the [Vue.js overview](./vue-overview.md) page.

Vue can be installed directly on Windows or on the Windows Subsystem for Linux (WSL). We generally recommend that you [install Vue on WSL](./vue-on-wsl.md) if you are planning to interact with a NodeJS backend, want parity with a Linux production server, or plan to follow along with a tutorial that utilizes Bash commands. You may also want to consider [Vite](https://vitejs.dev/guide/why.html) as an alternative to Vue.js.

## Prerequisites

- [Install Node.js on Windows](./nodejs-on-windows.md): This includes a version manager, package manager, and Visual Studio Code. The Node Package Manager (npm) is used to install Vue.js.

## Install Vue.js

To install Vue.js:

1. Open a command line (ie. Windows Command Prompt or PowerShell).

2. Create a new project folder: `mkdir VueProjects` and enter that directory: `cd VueProjects`.

3. Install Vue.js using Node Package Manager (npm):

```powershell
npm install vue
```

Check the version number you have installed by using the command: `vue --version`.

> [!NOTE]
> To install Vue.js using a CDN, rather than NPM, see the [Vue.js install docs](https://vuejs.org/v2/guide/installation.html#CDN). See the Vue docs for an [Explanation of different Vue builds](https://vuejs.org/v2/guide/installation.html#Explanation-of-Different-Builds).

## Install Vue CLI

Vue CLI is a toolkit for working with Vue in your terminal / command line. It enables you to quickly scaffold a new project (vue create), prototype new ideas (vue serve), or manage projects using a graphical user interface (vue ui). Vue CLI is a globally installed npm package that handles some of the build complexities (like using Babel or Webpack) for you. *If you are not building a new single-page app, you may not need or want Vue CLI.*

To install Vue CLI, use npm. You must use the `-g` flag to globally install in order to upgrade (`vue upgrade --next`):

```PowerShell
npm install -g @vue/cli
```

To learn more about additional plugins that can be added (such as linting or Apollo for integrating GraphQL), visit [Vue CLI plugins](https://cli.vuejs.org/guide/#cli-plugins) in the Vue CLI docs.

## Additional resources

- [Vue docs](https://vuejs.org/)
- [Vue.js overview](./vue-overview.md)
- [Install Vue.js on WSL](./vue-on-wsl.md)
- [Install Nuxt.js](./nuxtjs-on-wsl.md)
- [Take your first steps with Vue.js](/training/paths/vue-first-steps/) learning path
- Try a [Vue tutorial with VS Code](https://code.visualstudio.com/docs/nodejs/vuejs-tutorial)
