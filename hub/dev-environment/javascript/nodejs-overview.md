---
title: NodeJS on Windows
description: A guide to help you set up a NodeJS development environment on Windows.
author: mattwojo 
ms.author: mattwoj 
manager: jken
ms.topic: article
keywords: NodeJS, Node.js, windows 10, microsoft, learning nodejs, node on windows, node on windows for beginners, develop with node on windows, developer with nodejs on windows
ms.localizationpriority: medium
ms.date: 03/30/2021
---

# What is NodeJS?

Node.js is an open-source, cross-platform, server-side JavaScript runtime environment built on Chrome’s V8 JavaScript engine originally authored by Ryan Dahl and released in 2009.

## Does Node.js work on Windows?

Yes. Windows supports two different environments for developing apps with Node.js:

- [Install a Node.js development environment on Windows](./nodejs-on-windows.md)
- [Install a Node.js development environment on Windows Subsystem for Linux](./nodejs-on-wsl.md)

## What can you do with NodeJS?

Node.js is primarily used for building fast and scalable web applications. It uses an event-driven, non-blocking I/O model, making it lightweight and efficient. It's a great framework for data-intensive real-time applications that run across distributed devices. Here are a few examples of what you might create with Node.js.

- **Single-page apps (SPAs)**: These are web apps that work inside a browser and don't need to reload a page every time you use it to get new data. Some example SPAs include social networking apps, email or map apps, online text or drawing tools, etc.
- **Real-time apps (RTAs)**: These are web apps that enable users to receive information as soon as it's published by an author, rather than requiring that the user (or software) check a source periodically for updates. Some example RTAs include instant messaging apps or chat rooms, online multiplayer games that can be played in the browser, online collaboration docs, community storage, video conference apps, etc.
- **Data streaming apps**: These are apps (or services) that send data/content as it arrives (or is created) while keeping the connection open to continue downloading further data, content, or components as needed. Some examples include video- and audio-streaming apps.
- **REST APIs**: These are interfaces that provide data for someone else's web app to interact with. For example, a Calendar API service could provide dates and times for a concert venue that could be used by someone else's local events website.
- **Server-side rendered apps (SSRs)**: These web apps can run on both the client (in your browser / the front-end) and the server (the back-end) allowing pages that are dynamic to display (generate HTML for) whatever content is known and quickly grab content that is not known as it's available. These are often referred to as "isomorphic" or "universal" applications. SSRs utilize SPA methods in that they don't need to reload every time you use it. SSRs, however, offer a few benefits that may or may not be important to you, like making content on your site appear in Google search results and providing a preview image when links to your app are shared on social media like Twitter or Facebook. The potential drawback being that they require a Node.js server constantly running. In terms of examples, a social networking app that supports events that users will want to appear in search results and social media may benefit from SSR, while an email app may be fine as an SPA. You can also run server-rendered no-SPA apps, which may be something like a WordPress blog. As you can see, things can get complicated, you just need to decide what's important.
- **Command line tools**: These allow you to automate repetitive tasks and then distribute your tool across the vast Node.js ecosystem. An example of a command line tool is cURL, which stand for client URL and is used to download content from an internet URL. cURL is often used to install things like Node.js or, in our case, a Node.js version manager.
- **Hardware programming**: While not quite as popular as web apps, Node.js is growing in popularity for IoT uses, such as collecting data from sensors, beacons, transmitters, motors, or anything that generates large amounts of data. Node.js can enable data collection, analyzing that data, communicating back and forth between a device and server, and taking action based on the analysis. NPM contains more than 80 packages for Arduino controllers, raspberry pi, Intel IoT Edison, various sensors, and Bluetooth devices.

## Next steps

- [Install NodeJS on Windows](./nodejs-on-windows.md)
- [Install NodeJS on WSL](./nodejs-on-wsl.md)
- [Build JavaScript applications with Node.js](/training/paths/build-javascript-applications-nodejs/) learning path
