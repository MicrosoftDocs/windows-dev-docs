---
title: Create a PWA or Hybrid web app for Android
description: Get started developing Android apps using the PWA approach on Windows.
ms.topic: article
keywords: android on windows, pwa, android, cordova, ionic, phonegap, hybrid web app
ms.date: 04/28/2020
---

# Get started developing a PWA or Hybrid web app for Android

This guide will help you to get started creating a hybrid web app or Progressive Web App (PWA) on Windows using a single HTML/CSS/JavaScript codebase that can be used on the web and across device platforms (Android, iOS, Windows).

By using the right frameworks and components, web-based applications can work on an Android device in a way that looks to users very similar to a native app.

## Features of a PWA or Hybrid web app

There are two main types of web apps that can be installed on Android devices. The main difference being whether your application code is embedded in an app package (hybrid) or hosted on a web server (pwa).

- **Hybrid web apps**: Code (HTML, JS, CSS) is packaged in an APK and can be distributed via the Google Play Store. The viewing engine is isolated from the users' internet browser, no session or cache sharing.

- **Progressive Web Apps (PWAs)**: Code (HTML, JS, CSS) lives on the web and doesn't need to be packaged as an APK. Resources are downloaded and updated as needed using a Service Worker. The Chrome browser will render and display your app, but will look native and not include the normal browser address bar, etc. You can share storage, cache, and sessions with the browser. This is basically like installing a shortcut to the Chrome browser in a special mode. PWAs can also be listed in the Google Play Store using Trusted Web Activity.

PWAs and hybrid web apps are very similar to a native Android app in that they:

- Can be installed via the App Store (Google Play Store and/or Microsoft Store)
- Have access to native device features like camera, GPS, Bluetooth, notifications, and list of contacts
- Work Offline (no internet connection)

PWAs also have a few unique features:

- Can be installed on the Android home screen directly from the web (without an App Store)
- Can additionally be installed via the Google Play Store [using a Trusted Web Activity](https://css-tricks.com/how-to-get-a-progressive-web-app-into-the-google-play-store/)
- Can be discovered via web search or shared via a URL link
- Rely on a [Service Worker](https://developers.google.com/web/fundamentals/primers/service-workers) to avoid the need to package native code

You don't need a framework to create a Hybrid app or PWA, but there are a few popular frameworks that will be covered in this guide, including PhoneGap (with Cordova) and Ionic (with Cordova or Capacitor using Angular or React).

## Apache Cordova

[Apache Cordova](https://cordova.apache.org/) is an open-source framework that can simplify the communication between your JavaScript code living in a native [WebView](https://developer.android.com/reference/android/webkit/WebView) and the native Android platform by using [plugins](https://cordova.apache.org/plugins/?platforms=cordova-android). These plugins expose JavaScript endpoints that can be called from your code and used to call native Android device APIs. Some example Cordova plugins include access to device services like battery status, file access, vibration / ring tones, etc. These features are not typically available to web apps or browsers.

There are two popular distributions of Cordova:

- PhoneGap: Support has been discontinued by Adobe.

- [Ionic](https://ionicframework.com/)

## Ionic

[Ionic](https://ionicframework.com/) is a framework that adjusts the user interface (UI) of your app to match the design language of each platform (Android, iOS, Windows). Ionic enables you to use either [Angular](https://ionicframework.com/docs/developer-resources/guides/first-app-v4/intro) or [React](https://ionicframework.com/react).

> [!NOTE]
> There is a new version of Ionic that uses an alternative to Cordova, called [Capacitor](https://capacitor.ionicframework.com/). This alternative uses containers to make your app [more web-friendly](https://ionicframework.com/blog/announcing-capacitor-1-0/).

### Get started with Ionic by installing required tools

To get started building a PWA or hybrid web app with Ionic, you should first install the following tools:

- Node.js for interacting with the Ionic ecosystem. [Download NodeJS for Windows](https://nodejs.org/en/) or follow the [NodeJS installation guide](../dev-environment/javascript/nodejs-on-wsl.md) using Windows Subsystem for Linux (WSL). You may want to consider using [Node Version Manager (nvm)](../dev-environment/javascript/nodejs-on-wsl.md#install-nvm-nodejs-and-npm) if you will be working with multiple projects and version of NodeJS.

- VS Code for writing your code. [Download VS Code for Windows](https://code.visualstudio.com/). You may also want to install the [WSL Remote Extension](https://marketplace.visualstudio.com/items?itemName=ms-vscode-remote.remote-wsl) if you prefer to build your app with a Linux command line.

- Windows Terminal for working with your preferred command-line interface (CLI). [Install Windows Terminal from Microsoft Store](https://www.microsoft.com/en-us/p/windows-terminal-preview/9n0dx20hk701?activetab=pivot:overviewtab).

- Git for version control. [Download Git](https://git-scm.com/downloads).

## Create a new project with Ionic Cordova and Angular

Install Ionic and Cordova by entering the following in your command line:

```bash
npm install -g @ionic/cli cordova
```

Create an Ionic Angular app using the "Tabs" app template by entering the command:

```bash
ionic start photo-gallery tabs
```

Change into the app folder:

```bash
cd photo-gallery
```

Run the app in your web browser:

```bash
ionic serve
```

For more information, see the [Ionic Cordova Angular docs](https://ionicframework.com/docs/developer-resources/guides/first-app-v4/intro). Visit the [Making your Angular app a PWA](https://ionicframework.com/docs/angular/pwa) section of the Ionic docs to learn how to move your app from being a hybrid to a PWA.

## Create a new project with Ionic Capacitor and Angular

Install Ionic and Cordova-Res by entering the following in your command line:

```bash
npm install -g @ionic/cli native-run cordova-res
```

Create an Ionic Angular app using the "Tabs" app template and adding Capacitor by entering the command:

```bash
ionic start photo-gallery tabs --type=angular --capacitor
```

Change into the app folder:

```bash
cd photo-gallery
```

Add components to make the app a PWA:

```bash
npm install @ionic/pwa-elements
```

Import @ionic/pwa-elements by add the following to your `src/main.ts` file:

```typescript
import { defineCustomElements } from '@ionic/pwa-elements/loader';

// Call the element loader after the platform has been bootstrapped
defineCustomElements(window);
```

Run the app in your web browser:

```bash
ionic serve
```

For more information, see the [Ionic Capacitor Angular docs](https://ionicframework.com/docs/angular/your-first-app). Visit the [Making your Angular app a PWA](https://ionicframework.com/docs/angular/pwa) section of the Ionic docs to learn how to move your app from being a hybrid to a PWA.  

## Create a new project with Ionic and React

Install the Ionic CLI by entering the following in your command line:

```bash
npm install -g @ionic/cli
```

Create a new project with React by entering the command:

```bash
ionic start myApp blank --type=react
```

Change into the app folder:

```bash
cd myApp
```

Run the app in your web browser:

```bash
ionic serve
```

For more information, see the [Ionic React docs](https://ionicframework.com/docs/react/quickstart). Visit the [Making your React app a PWA](https://ionicframework.com/docs/react/pwa) section of the Ionic docs to learn how to move your app from being a hybrid to a PWA.

## Test your Ionic app on a device or emulator

To test your Ionic app on an Android device, plug-in your device ([make sure it is first enabled for development](emulator.md#enable-your-device-for-development)), then in your command line enter:

```bash
ionic cordova run android
```

To test your Ionic app on an Android device emulator, you must:

1. [Install the required components -- Java Development Kit (JDK), Gradle, and the Android SDK](https://cordova.apache.org/docs/en/latest/guide/platforms/android/#installing-the-requirements).

2. Create an Android Virtual Device (AVD): See the [Android developer guide]](https://developer.android.com/studio/run/managing-avds.html).

3. Enter the command for Ionic to build and deploy your app to the emulator: `ionic cordova emulate [<platform>] [options]`. In this case, the command should be:

```bash
ionic cordova emulate android --list
```

See the [Cordova Emulator](https://ionicframework.com/docs/cli/commands/cordova-emulate) in the Ionic docs for more info.

## Additional resources

- [Develop Dual-screen apps for Android and get the Surface Duo device SDK](/dual-screen/android/)

- [Add Windows Defender exclusions to improve performance](defender-settings.md)

- [Enable Virtualization support to improve emulator performance](emulator.md#enable-virtualization-support)
