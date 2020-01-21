---
title: Get started with Node.js web frameworks on Windows
description: A guide to help you get started with Node.js web frameworks on Windows.
author: mattwojo 
ms.author: mattwoj 
manager: jken
ms.topic: article
keywords: NodeJS, Node.js, windows 10, microsoft, learning nodejs, node on windows, node on wsl, node on linux on windows, install node on windows, nodejs with vs code, develop with node on windows, develop with nodejs on windows, install node on WSL, NodeJS on Windows Subsystem for Linux
ms.localizationpriority: medium
ms.date: 09/19/2019
---

# Get started with Node.js web frameworks on Windows

A step-by-step guide to help you get started using Node.js web frameworks on Windows, including Next.js, Nuxt.js, and Gatsby.

## Prerequisites

This guide assumes that you've already completed the steps to [set up your Node.js development environment with WSL 2](./setup-on-wsl2.md), including:

- Install Windows 10 Insider Preview build 18932 or later.
- Enable the WSL 2 feature on Windows.
- Install a Linux distribution (Ubuntu 18.04 for our examples). You can check this with: `wsl lsb_release -a`
- Ensure your Ubuntu 18.04 distribution is running in WSL 2 mode. (WSL can run distributions in both v1 or v2 mode.) You can check this by opening PowerShell and entering: `wsl -l -v`
- Using PowerShell, set Ubuntu 18.04 as your default distribution, with: `wsl -s ubuntu 18.04`

## Get started with Next.js

Next.js is a framework for creating server-rendered JavaScript apps based on React.js, Node.js, Webpack and Babel.js. It is basically a project boilerplate for React, crafted with attention to best practices, that allows you to create "universal" web apps in a simple, consistent way, with hardly any configuration. These "universal" server-rendered web apps are also sometimes called “isomorphic”, meaning that code is shared between the client and server.

To create a Next.js project, which includes installing next, react, and react-dom:

1. Open your WSL terminal (ie. Ubuntu 18.04).

2. Create a new project folder: `mkdir NextProjects` and enter that directory: `cd NextProjects`.

3. Install Next.js and create a project (replacing 'my-next-app' with whatever you'd like to call your app): `npm create next-app my-next-app`.

4. Once the package has been installed, change directories into your new app folder, `cd my-next-app`, then use `code .` to open your Next.js project in VS Code. This will allow you to look at the Next.js framework that has been created for your app. Notice that VS Code opened your app in a WSL-Remote environment (as indicated in the green tab on the bottom-left of your VS Code window). This means that while you are using VS Code for editing on the Windows OS, it is still running your app on the Linux OS.

    ![WSL-Remote Extension](../images/wsl-remote-extension.png)

5. There are 3 commands you need to know once Next.js is installed:

    - `npm run dev` for running a development instance with hot-reloading, file watching and task re-running.
    - `npm run build` for compiling your project.
    - `npm start` for starting your app in production mode.

    Open the WSL terminal integrated in VS Code (**View > Terminal**). Make sure that the terminal path is pointed to your project directory (ie. `~/NextProjects/my-next-app$`). Then try running a development instance of your new Next.js app using: `npm run dev`

6. The local development server will start and once your project pages are done building, your terminal will display "compiled successfully - ready on [http://localhost:3000](http://localhost:3000)". Select this localhost link to open your new Next.js app in a web browser.

    ![Your Next.js app running in localhost:3000](../images/next-app.png)

7. Open the `pages/index.js` file in your VS Code editor. Find the page title `<h1 className='title'>Welcome to Next.js!</h1>` and change it to `<h1 className='title'>This is my new Next.js app!</h1>`. With your web browser still open to localhost:3000, save your change and notice the hot-reloading feature automatically compile and update your change in the browser.

8. Let's see how Next.js handles errors. Remove the `</h1>` closing tag so that your title code now looks like this: `<h1 className='title'>This is my new Next.js app!`. Save this change and notice that a "Failed to compile" error will display in your browser, and in your terminal, letting your know that a closing tag for `<h1>` is expected. Replace the `</h1>` closing tag, save, and the page will reload.

You can use VS Code's debugger with your Next.js app by selecting the F5 key, or by going to **View > Debug** (Ctrl+Shift+D) and **View > Debug Console** (Ctrl+Shift+Y) in the menu bar. If you select the gear icon in the Debug window, a launch configuration (`launch.json`) file will be created for you to save debugging setup details. To learn more, see [VS Code Debugging](https://code.visualstudio.com/docs/nodejs/nodejs-debugging).

![VS Code debug window and launch.json config icon](../images/vscode-debug-launch-configuration.png)

To learn more about Next.js, see the [Next.js docs](https://nextjs.org/docs).

## Get started with Nuxt.js

Nuxt.js is a framework for creating server-rendered JavaScript apps based on Vue.js, Node.js, Webpack and Babel.js. It was inspired by Next.js. It is basically a project boilerplate for Vue. Just like Next.js, it is crafted with attention to best practices and allows you to create "universal" web apps in a simple, consistent way, with hardly any configuration. These "universal" server-rendered web apps are also sometimes called “isomorphic”, meaning that code is shared between the client and server.

To create a Nuxt.js project, which will include answering a series of questions about what sort of integrated server-side framework, UI framework, testing framework, mode, modules, and linter you would like to install:

1. Open your WSL terminal (ie. Ubuntu 18.04).

2. Create a new project folder: `mkdir NuxtProjects` and enter that directory: `cd NuxtProjects`.

3. Install Nuxt.js and create a project (replacing 'my-nuxt-app' with whatever you'd like to call your app): `npm create nuxt-app my-nuxt-app`

4. The Nuxt.js installer will now ask you the following questions:
    - Project Name: my-nuxtjs-app
    - Project description: Description of my Nuxt.js app.
    - Author name: I use my GitHub alias.
    - Choose the package manager: Yarn or **Npm** - we use NPM for our examples.
    - Choose UI framework: None, Ant Design Vue, Bootstrap Vue, etc. Let's choose **Vuetify** for this example, but the Vue Community created a nice [summary comparing these UI frameworks](https://vue-community.org/guide/ecosystem/ui-libraries.html#summary-tldr) to help you choose the best fit for your project.
    - Choose custom server frameworks: None, AdonisJs, Express, Fastify, etc. Let's choose **None** for this example, but you can find a [2019-2020 server framework comparison](https://dev.to/santypk4/introducing-the-best-10-node-js-frameworks-for-2019-and-2020-mcm) on the Dev.to site.
    - Choose Nuxt.js modules (use spacebar to select modules or just enter if you don't want any): Axios (for simplifying HTTP requests) or [PWA support](https://pwa.nuxtjs.org/) (for adding a service-worker, manifest.json file, etc). Let's not add a module for this example.
    - Choose linting tools: **ESLint**, Prettier, Lint staged files. Let's choose **ESLint** (a tool for analyzing your code and warning you of potential errors).
    - Choose a test framework: **None**, Jest, AVA. Let's choose **None** as we won't cover testing in this quickstart.
    - Choose rendering mode: **Universal (SSR)** or Single Page App (SPA). Let's choose **Universal (SSR)** for our example, but the [Nuxt.js docs](https://nuxtjs.org/guide#server-rendered-universal-ssr-) point out some of the differences -- SSR requiring a Node.js server running to server-render your app and SPA for static hosting.
    - Choose development tools: **jsconfig.json** (recommended for VS Code so Intellisense code completion works)

5. Once your project is created, `cd my-nuxtjs-app` to enter your Nuxt.js project directory, then enter `code .` to open the project in the VS Code WSL-Remote environment.

    ![WSL-Remote Extension](../images/wsl-remote-extension.png)

6. There are 3 commands you need to know once Nuxt.js is installed:

    - `npm run dev` for running a development instance with hot-reloading, file watching and task re-running.
    - `npm run build` for compiling your project.
    - `npm start` for starting your app in production mode.

    Open the WSL terminal integrated in VS Code (**View > Terminal**). Make sure that the terminal path is pointed to your project directory (ie. `~/NuxtProjects/my-nuxt-app$`). Then try running a development instance of your new Nuxt.js app using: `npm run dev`

6. The local development server will start (displaying some kind of cool progress bars for the client and server compiles). Once your project is done building, your terminal will display "Compiled successfully" along with how much time it took to compile. Point your web browser to [http://localhost:3000](http://localhost:3000) to open your new Nuxt.js app.

    ![Your Nuxt.js app running in localhost:3000](../images/nuxt-app.png)

7. Open the `pages/index.vue` file in your VS Code editor. Find the page title `<v-card-title class="headline">Welcome to the Vuetify + Nuxt.js template</v-card-title>` and change it to `<v-card-title class="headline">This is my new Nuxt.js app!</v-card-title>`. With your web browser still open to localhost:3000, save your change and notice the hot-reloading feature automatically compile and update your change in the browser.

8. Let's see how Nuxt.js handles errors. Remove the `</v-card-title>` closing tag so that your title code now looks like this: `<v-card-title class="headline">This is my new Nuxt.js app!`. Save this change and notice that a compiling error will display in your browser, and in your terminal, letting your know that a closing tag for `<v-card-title>` is missing, along with the line numbers where the error can be found in your code. Replace the `</v-card-title>` closing tag, save, and the page will reload.

You can use VS Code's debugger with your Nuxt.js app by selecting the F5 key, or by going to **View > Debug** (Ctrl+Shift+D) and **View > Debug Console** (Ctrl+Shift+Y) in the menu bar. If you select the gear icon in the Debug window, a launch configuration (`launch.json`) file will be created for you to save debugging setup details. To learn more, see [VS Code Debugging](https://code.visualstudio.com/docs/nodejs/nodejs-debugging).

![VS Code debug window and launch.json config icon](../images/vscode-debug-launch-configuration.png)

To learn more about Nuxt.js, see the  [Nuxt.js guide](https://nuxtjs.org/guide).

## Get started with Gatsby.js

Gatsby.js is a static site generator framework based on React.js, as opposed to being server-rendered like Next.js and Nuxt.js. A static site generator generates static HTML on build time. It doesn’t require a server. Next.js and Nuxt.js generate HTML on runtime (each time a new request comes in). They do require a server to run. Gatsby also dictates how to handle data in your app (with GraphQL), whereas Next.js and Nuxt.js leave that decision up to you.

To create a Gatsby.js project:

1. Open your WSL terminal (ie. Ubuntu 18.04).
2. Create a new project folder: `mkdir GatsbyProjects` and enter that directory: `cd GatsbyProjects`
3. Use npm to install the Gatsby CLI: `npm install -g gatsby-cli`. Once installed, check the version with `gatsby --version`.
4. Create your Gatsby.js project: `gatsby new my-gatsby-app`
5. Once the package has been installed, change directories into your new app folder, `cd my-gatsby-app`, then use `code .` to open your Gatsby project in VS Code. This will allow you to look at the Gatsby.js framework that has been created for your app using VS Code's File Explorer. Notice that VS Code opened your app in a WSL-Remote environment (as indicated in the green tab on the bottom-left of your VS Code window). This means that while you are using VS Code for editing on the Windows OS, it is still running your app on the Linux OS.

    ![WSL-Remote Extension](../images/wsl-remote-extension.png)

6. There are 3 commands you need to know once Gatsby is installed:

    - `gatsby develop` for running a development instance with hot-reloading.
    - `gatsby build` for creating a production build.
    - `gatsby serve` for starting your app in production mode.

    Open the WSL terminal integrated in VS Code (**View > Terminal**). Make sure that the terminal path is pointed to your project directory (ie. `~/GatsbyProjects/my-gatsby-app$`). Then try running a development instance of your new app using: `gatsby develop`

7. Once your new Gatsby project finishes compiling, your terminal will display that "You can now view gatsby-starter-default in the browser. [http://localhost:8000/](http://localhost:8000/)." Select this localhost link to view your new project built in a web browser.

> [!NOTE]
> You'll notice that your terminal output also let's you know that you can "View GraphiQL, an in-browser IDE, to explore your site's data and schema: [http://localhost:8000/___graphql](http://localhost:8000/___graphql)." GraphQL consolidates your APIs into a self-documenting IDE (GraphiQL) built into Gatsby. In addition to exploring your site's data and schema, you can perform GraphQL operations such as queries, mutations, and subscriptions. For more info, see [Introducing GraphiQL](https://www.gatsbyjs.org/docs/running-queries-with-graphiql/).

8. Open the `src/pages/index.js` file in your VS Code editor. Find the page title `<h1 >Hi people</h1>` and change it to `<h1 >Hi (Your Name)!</h1>`. With your web browser still open to localhost:8000, save your change and notice the hot-reloading feature automatically compile and update your change in the browser.

    ![Your Gatsby.js app running in localhost:3000](../images/gatsby-app.png)

9. Let's see how Next.js handles errors. Remove the `</h1>` closing tag so that your title code now looks like this: `<h1>Hi (Your Name)!`. Save this change and notice that a "Failed to compile" error will display in your browser, and in your terminal, letting your know that a closing tag for `<h1>` is expected. Replace the `</h1>` closing tag, save, and the page will reload.

You can use VS Code's debugger with your Next.js app by selecting the F5 key, or by going to **View > Debug** (Ctrl+Shift+D) and **View > Debug Console** (Ctrl+Shift+Y) in the menu bar. If you select the gear icon in the Debug window, a launch configuration (`launch.json`) file will be created for you to save debugging setup details. To learn more, see [VS Code Debugging](https://code.visualstudio.com/docs/nodejs/nodejs-debugging).

![VS Code debug window and launch.json config icon](../images/vscode-debug-launch-configuration.png)

To learn more about Gatsby, see the  [Gatsby.js docs](https://www.gatsbyjs.org/docs/).
