---
title: Vue on Windows
description: A guide to help you set up a Vue development environment on Windows 10.
ms.topic: article
keywords: Vue, Vue.js, vue on windows 10, vue on windows, vue overview, what is vue, vue with windows, set up vue on windows, vue dev environment, set up a vue development environment 
ms.localizationpriority: medium
ms.date: 03/30/2021
---

# What is Vue.js?

Vue is an open-source, front end JavaScript framework for building user interfaces and single-page applications on the web. Created by Evan You, released in 2014 and maintained by Evan and his core team, Vue focuses on declarative rendering and component composition offering a core library for the view layer only.

If you want to build a server-rendered Vue web app with advanced features such as routing, state management and build tooling, take a look at [Nuxt.js](./nuxtjs-on-wsl.md).

## What makes Vue unique?

Vue uses a model-view-viewmodel architecture. Evan You previously worked on AngularJS at Google and extracted parts of Angular to offer a more lightweight framework. Vue is in may ways similar to React, Angular, Ember, Knockout, etc. See the Vue documentation for a more [in-depth comparison](https://vuejs.org/v2/guide/comparison.html) to these other JavaScript frameworks.

## What can you do with Vue?

- Build a single-page app (SPA)
- Use just a component of Vue to [add a simple to-do list to your app](https://vuejs.org/v2/guide/single-file-components.html#Getting-Started) or find [more complex examples](https://vuejsexamples.com/)
- Build a server-rendered website with a Node.js backend, with help from [Nuxt.js](./nuxtjs-on-wsl.md)

## Vue tools

Vue.js is focused only on the view layer, so may require additional tools to create a more complex app. You may want to consider using:

- Package manager: Two popular package managers for Vue are [npm](https://www.npmjs.com/) (which is included with NodeJS) and [yarn](https://yarnpkg.com/). Both support a broad library of well-maintained packages that can be installed.
- [Vue CLI](https://cli.vuejs.org): a standard toolkit for rapid Vue.js development with out-of-the-box support for Babel, PostCSS, TypeScript, ESLint, etc.
- [Nuxt.js](./nuxtjs-on-wsl.md): A framework to make server-side rendered Vue.js apps possible. Server-side rendering can improve SEO and make user interfaces more responsive.
- [Vue extension pack for VS Code](https://marketplace.visualstudio.com/items?itemName=mubaidr.vuejs-extension-pack): Adds syntax highlighting, code formatting, and code snippets to your .vue files.
- [Vuetify](https://vuetifyjs.com/): A Vue UI library offering Material Design Framework components.
- [Vuesion](https://github.com/vuesion/vuesion): A Vue boilerplate for production-ready Progressive Web Apps (PWAs).
- [Storybook](https://storybook.js.org/): A development and testing environment for Vue user interface components.
- [Vue Router](https://router.vuejs.org/): Supports mapping application URLs to Vue components.
- [Vue Design System](https://vueds.com/): An open source tool for building Design Systems with Vue.js.
- [VueX](https://vuex.vuejs.org/): State management system for Vue apps.

## Additional resources

- [Vue docs](https://vuejs.org/)
- [Vue.js overview](./vue-overview.md)
- [Install Vue.js on WSL](./vue-on-wsl.md)
- [Install Vue.js on Windows](./vue-on-windows.md)
- [Install Nuxt.js](./nuxtjs-on-wsl.md)
- [Take your first steps with Vue.js](/training/paths/vue-first-steps/) learning path
- Try a [Vue tutorial with VS Code](https://code.visualstudio.com/docs/nodejs/vuejs-tutorial)
