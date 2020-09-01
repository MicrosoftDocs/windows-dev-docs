---
title: Get started using NodeJS on Windows for beginners
description: A guide to help beginners get started with Node.js development on Windows.
author: mattwojo 
ms.author: mattwoj 
manager: jken
ms.topic: article
keywords: NodeJS, Node.js, windows 10, microsoft, learning nodejs, node on windows, node on windows for beginners, develop with node on windows, developer with nodejs on windows
ms.localizationpriority: medium
ms.date: 09/19/2019
---

# Get started using Node.js on Windows for beginners

If you're brand new to using Node.js, this guide will help you to get started with some basics.

## Prerequisites

This guide assumes that you've already completed the steps to [set up your Node.js development environment on native Windows](./setup-on-windows.md), including:

- Install a Node.js version manager.
- Install Visual Studio Code.

Installing Node.js directly on Windows is the most straightforward way to get started performing basic Node.js operations with a minimal amount of set up.

Once you are ready to use Node.js to develop applications for production, which typically involves deploying to a Linux server, we recommend that you [set up your Node.js development environment with WSL2](./setup-on-wsl2.md). Though it's possible to deploy web apps on Windows servers, it is much more common to [use Linux servers to host your Node.js apps](https://azure.microsoft.com/develop/nodejs/).

## Types of Node.js applications

Node.js is a JavaScript runtime primarily used for creating web applications. Put another way, it's a server-side implementation of JavaScript used for writing the backend of an application. (Though many Node.js frameworks can also handle the frontend.) Here are a few examples of what you might create with Node.js.

- **Single-page apps (SPAs)**: These are web apps that work inside a browser and don't need to reload a page every time you use it to get new data. Some example SPAs include social networking apps, email or map apps, online text or drawing tools, etc.
- **Real-time apps (RTAs)**: These are web apps that enable users to receive information as soon as it's published by an author, rather than requiring that the user (or software) check a source periodically for updates. Some example RTAs include instant messaging apps or chat rooms, online multiplayer games that can be played in the browser, online collaboration docs, community storage, video conference apps, etc.
- **Data streaming apps**: These are apps (or services) that send data/content as it arrives (or is created) while keeping the connection open to continue downloading further data, content, or components as needed. Some examples include video- and audio-streaming apps.
- **REST APIs**: These are interfaces that provide data for someone else's web app to interact with. For example, a Calendar API service could provide dates and times for a concert venue that could be used by someone else's local events website.
- **Server-side rendered apps (SSRs)**: These web apps can run on both the client (in your browser / the front-end) and the server (the back-end) allowing pages that are dynamic to display (generate HTML for) whatever content is known and quickly grab content that is not known as it's available. These are often referred to as “isomorphic” or “universal” applications. SSRs utilize SPA methods in that they don't need to reload every time you use it. SSRs, however, offer a few benefits that may or may not be important to you, like making content on your site appear in Google search results and providing a preview image when links to your app are shared on social media like Twitter or Facebook. The potential drawback being that they require a Node.js server constantly running. In terms of examples, a social networking app that supports events that users will want to appear in search results and social media may benefit from SSR, while an email app may be fine as an SPA. You can also run server-rendered no-SPA apps, which my be something like a WordPress blog. As you can see, things can get complicated, you just need to decide what's important.
- **Command line tools**: These allow you to automate repetitive tasks and then distribute your tool across the vast Node.js ecosystem. An example of a command line tool is cURL, which stand for client URL and is used to download content from an internet URL. cURL is often used to install things like Node.js or, in our case, a Node.js version manager.
- **Hardware programming**: While not quite as popular as web apps, Node.js is growing in popularity for IoT uses, such as collecting data from sensors, beacons, transmitters, motors, or anything that generates large amounts of data. Node.js can enable data collection, analyzing that data, communicating back and forth between a device and server, and taking action based on the analysis. NPM contains more than 80 packages for Arduino controllers, raspberry pi, Intel IoT Edison, various sensors, and Bluetooth devices.

## Try using Node.js in VS Code

1. Open your command line (Command prompt, PowerShell, or whatever you prefer) and create a new directory: `mkdir HelloNode`, then enter the directory: `cd HelloNode`

2. Create a JavaScript file named "app.js" with a variable named "msg" inside: `echo var msg > app.js`

3. Open the directory and your app.js file in VS Code: : `code .`

4. Add a simple string variable ("Hello World"), then send the contents of the string to your console by entering this in your "app.js" file:

    ```js
    var msg = 'Hello World';
    console.log(msg);
    ```

5. To run your "app.js" file with Node.js. Open your terminal right inside VS Code by selecting **View** > **Terminal** (or select Ctrl+`, using the backtick character). If you need to change the default terminal, select the dropdown menu and choose **Select Default Shell**.

6. In the terminal, enter: `node app.js`. You should see the output: "Hello World".

> [!NOTE]
> Notice that when you type `console` in your 'app.js' file, VS Code displays supported options related to the [`console`](https://developer.mozilla.org/docs/Web/API/Console) object for you to choose from using IntelliSense. Try experimenting with Intellisense using other [JavaScript objects](https://developer.mozilla.org/docs/Web/JavaScript/Reference/Global_Objects).

> [!TIP]
> Try the new [Windows terminal](https://github.com/microsoft/terminal/blob/master/doc/user-docs/index.md) if you plan to use multiple command lines (Ubuntu, PowerShell, Windows Command Prompt, etc) or if you want to [customize your terminal](https://github.com/microsoft/terminal/blob/master/doc/user-docs/UsingJsonSettings.md), including text, background colors, key bindings, multiple window panes, etc.

## Set up a basic web app framework by using Express

Express is a minimal, flexible, and streamlined Node.js framework that makes it easier to develop a web app that can handle multiple types of requests, like GET, PUT, POST, and DELETE. Express comes with an application generator that will automatically create a file architecture for your app.

To create a project with Express.js:

1. Open your command line (Command Prompt, Powershell, or whatever you prefer).
2. Create a new project folder: `mkdir ExpressProjects` and enter that directory: `cd ExpressProjects`
3. Use Express to create a HelloWorld project template: `npx express-generator HelloWorld --view=pug`

>[!NOTE]
> We are using the `npx` command here to execute the Express.js Node package without actually installing it (or by temporarily installing it depending on how you want to think of it). If you try to use the `express` command or check the version of Express installed using: `express --version`, you will receive a response that Express cannot be found. If you want to globally install Express to use over and over again, use: `npm install -g express-generator`. You can view a list of the packages that have been installed by npm using `npm list`. They'll be listed by depth (the number of nested directories deep). Packages that you installed will be at depth 0. That package's dependencies will be at depth 1, further dependencies at depth 2, and so on. To learn more, see [Difference between npx and npm?](https://stackoverflow.com/questions/50605219/difference-between-npx-and-npm) on Stackoverflow.

4. Examine the files and folders that Express included by opening the project in VS Code, with: `code .`

   The files that Express generates will create a web app that uses an architecture that can appear a little overwhelming at first. You'll see in your VS Code **Explorer** window (Ctrl+Shift+E to view) that the following files and folders have been generated:

   - `bin`. Contains the executable file that starts your app. It fires up a server (on port 3000 if no alternative is supplied) and sets up basic error handling. 
   - `public`. Contains all the publicly accessed files, including JavaScript files, CSS stylesheets, font files, images, and any other assets that people need when they connect to your website.
   - `routes`. Contains all the route handlers for the application. Two files, `index.js` and `users.js`, are automatically generated in this folder to serve as examples of how to separate out your application’s route configuration.
   - `views`. Contains the files used by your template engine. Express is configured to look here for a matching view when the render method is called. The default template engine is Jade, but Jade has been deprecated in favor of Pug, so we used the `--view` flag to change the view (template) engine. You can see the `--view` flag options, and others, by using `express --help`.
   - `app.js`. The starting point of your app. It loads everything and begins serving user requests. It's basically the glue that holds all the parts together.
   - `package.json`. Contains the project description, scripts manager, and app manifest. Its main purpose is to track your app's dependencies and their respective versions.

5. You now need to install the dependencies that Express uses in order to build and run your HelloWorld Express app (the packages used for tasks like running the server, as defined in the `package.json` file). Inside VS Code, open your terminal by selecting **View** > **Terminal** (or select Ctrl+`, using the backtick character), be sure that you're still in the 'HelloWorld' project directory. Install the Express package dependencies with:

```bash
npm install
```

6. At this point you have the framework set up for a multiple-page web app that has access to a large variety of APIs and HTTP utility methods and middleware, making it easier to create a robust API. Start the Express app on a virtual server by entering:

```bash
npx cross-env DEBUG=HelloWorld:* npm start
```

> [!TIP]
> The `DEBUG=myapp:*` part of the command above means you are telling Node.js that you want to turn on logging for debugging purposes. Remember to replace 'myapp' with your app name. You can find your app name in the `package.json` file under the "name" property. Using `npx cross-env` sets the `DEBUG` environment variable in any terminal, but you can also set it with your terminal specific way. The `npm start` command is telling npm to run the scripts in your `package.json` file.

7. You can now view the running app by opening a web browser and going to: **localhost:3000**

   ![Screenshot of Express app running in a browser](../images/express-app.png)

8. Now that your HelloWorld Express app is running locally in your browser, try making a change by opening the 'views' folder in your project directory and selecting the 'index.pug' file. Once open, change `h1= title` to `h1= "Hello World!"` and selecting **Save** (Ctrl+S). View your change by refreshing the **localhost:3000** URL on your web browser.

9. To stop running your Express app, in your terminal, enter: **Ctrl+C**

## Try using a Node.js module

Node.js has tools to help you develop server-side web apps, some built in and many more available via npm. These modules can help with many tasks:

|Tool               |Used for                                                                                                  |
|:----------------- |:---------------------------------------------------------------------------------------------------------|
|gm, sharp          |Image manipulation, including editing, resizing, compression, and so on, directly in your JavaScript code |
|PDFKit             |PDF generation                                                                                            |
|validator.js       |String validation                                                                                         |
|imagemin, UglifyJS2|Minification                                                                                              |
|spritesmith        |Sprite sheet generation                                                                                   |
|winston            |Logging                                                                                                  |
|commander.js       |Creating command-line applications                                                                       |

Let's use the built-in OS module to get some information about your computer's operating system:

1) In your command line, open the Node.js CLI. You'll see the `>` prompt letting you know you're using Node.js after entering: `node`

2) To identify the operating system you are currently using (which should return a response letting you know that you're on Windows), enter: `os.platform()`

3) To check your CPU's architecture, enter: `os.arch()`

4) To view the the CPUs available on your system, enter: `os.cpus()`

5) Leave the Node.js CLI by entering `.exit` or by selecting Ctrl+C twice.

   > [!TIP]
   > You can use the Node.js OS module to do things like check the platform and return a platform-specific variable: Win32/.bat for Windows development, darwin/.sh for Mac/unix, Linux, SunOS, and so on (for example, `var isWin = process.platform === "win32";`).

## Next steps

In this guide, you learned a few basic things about what you can do with Node.js, tried using the Node.js command line in VS Code, created a simple web app with Express.js and ran it locally in your web browser, and then tried using a few of the built-in Node.js modules. To learn more about how to install and use some popular Node.js web frameworks, continue to the next guide which covers Next.js (a server-rendered web framework based on React), Nuxt.js (a server-rendered web framework based on Vue), and Gatsby (a statically-rendered web framework based on React). You can also skip to learning about how to work with MongoDB or PostgreSQL databases or Docker containers.

- [Get started with Node.js web frameworks on Windows](./web-frameworks.md)
- [Get started connecting Node.js apps to a database](/windows/wsl/tutorials/wsl-database)
- [Get started using Docker containers with Node.js](./containers.md)