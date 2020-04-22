---
title: React Native for Android development on Windows
description: Get started developing Android apps using Xamarin Native on Windows.
author: mattwojo
ms.author: mattwoj 
manager: jken
ms.topic: article
keywords: android on windows
ms.date: 02/19/2020
---

# Get started developing for Android using React Native

This guide will help you to get started using React Native on Windows to create a cross-platform app that will work on Android devices.

## Overview

React Native is an [open-source](https://github.com/facebook/react-native) mobile application framework created by Facebook. It is used to develop applications for Android, iOS, Web and UWP (Windows) providing native UI controls and full access to the native platform. Working with React Native requires an understanding of JavaScript fundamentals.

## Get started with React Native by installing required tools

1. [Install Visual Studio Code](https://code.visualstudio.com) (or your code editor of choice).

2. [Install Android Studio for Windows](https://developer.android.com/studio). Android Studio installs the latest Android SDK by default. React Native requires Android 6.0 (Marshmallow) SDK or higher. We recommend using the latest SDK.

3. Create environment variable paths for the Java SDK and Android SDK:
    - In the Windows search menu, enter: "Edit the system environment variables", this will open the **System Properties** window.
    - Choose **Environment Variables...** and then choose **New...** under **User variables**.
    - Enter the Variable name and value (path). The default paths for the Java and Android SDKs are as follows. If you've chosen a specific location to install the Java and Android SDKs, be sure to update the variable paths accordingly.
        - JAVA_HOME: C:\Program Files\Android\Android Studio\jre\jre
        - ANDROID_HOME: C:\Users\username\AppData\Local\Android\Sdk

    ![Screenshot of adding environmental variable path](../images/add-environmental-variable-path.png)

4. [Install NodeJS for Windows](https://nodejs.org/en/) You may want to consider using [Node Version Manager (nvm) for Windows](https://github.com/coreybutler/nvm-windows#node-version-manager-nvm-for-windows) if you will be working with multiple projects and version of NodeJS. We recommend installing the latest LTS version for new projects.

> [!NOTE]
> You may also want to consider installing and using the [Windows Terminal](https://www.microsoft.com/en-us/p/windows-terminal-preview/9n0dx20hk701?activetab=pivot:overviewtab) for working with your preferred command-line interface (CLI), as well as, [Git for version control](https://git-scm.com/downloads). The [Java JDK](https://www.oracle.com/java/technologies/javase-downloads.html) comes packaged with Android Studio v2.2+, but if you need to update your JDK separately from Android Studio, use the [Windows x64 Installer](https://www.oracle.com/java/technologies/javase-jdk14-downloads.html).

## Create a new project with React Native

1. Use npm to install the [Expo CLI](https://docs.expo.io/versions/latest/) command line utility from the Windows Command Prompt, PowerShell, [Windows Terminal](https://www.microsoft.com/en-us/p/windows-terminal-preview/9n0dx20hk701?activetab=pivot:overviewtab), or the integrated terminal in VS Code (View > Integrated Terminal).

    ```powershell
    npm install -g expo-cli
    ```

2. Use [NPX](https://medium.com/@maybekatz/introducing-npx-an-npm-package-runner-55f7d4bd282b) with [Create-React-Native-App](https://github.com/expo/create-react-native-app) to create a React Native app that runs on iOS, Android, and web.

    ```powershell
    npx create-react-native-app my-new-app
    ```

    > [!NOTE]
    > We are using the `npx` command here to execute the React Native package without actually installing it (or by temporarily installing it depending on how you want to think of it). If you've previously installed create-react-native-app globally via `npm install -g create-react-native-app`, we recommend you uninstall the package using `npm uninstall -g create-react-native-app` to ensure that npx always uses the latest version. You can view a list of the packages that have been installed by using `npm list`. You are not required to have an Expo account to use `Create-react-native-app`. However, if prefer, you can create your React Native app using [`expo init my-new-app`](https://docs.expo.io/versions/v37.0.0/). [What's the difference?](https://stackoverflow.com/questions/51214823/differences-between-create-react-native-app-and-exp-init)

3. Open your new "my-new-app" directory:

    ```powershell
    cd my-new-app
    ```

4. To run your project, enter the following command. This will open a localhost window in your default internet browser displaying Node Metro Bundler. It will also display a QR code in both your command line and the Metro Bundler browser window.

     ```powershell
    expo start
    ```

5. To view your project running on an Android device, you will need to first [install the Expo Client app with the Google Play Store](https://play.google.com/store/apps/details?id=host.exp.exponent&hl=en_US) on your Android device. Once the Expo client app is installed, open it on your device and select **Scan QR Code**. Once the QR code is registered, you will be able to see the package build both on your device and in the Metro Bundler window running on localhost in your browser.

6. To view your project running on an Android emulator, follow the instructions here:
https://docs.expo.io/versions/latest/workflow/android-studio-emulator/

7. Open the `App.tsx` file in your React Native project to start working on your app. You should see your changes auto-updated in the app running via the Expo Client on your device or in your Android Emulator.

8. Try changing the landing page view text to say: "Hello World!".

    ```typescript
    export default function App() {
      return (
        <View style={styles.container}>
          <Text>Hello World!</Text>
        </View>
      );
    }
    ```

9. Try adding an image. First, you'll need to create a folder at the same level as the "android" and "ios" folders in your app, let's call it "images". Place an image in that folder, `your-image.png` for example. Use the format below to reference your image and style it with a height and width.

     ```typescript
    export default function App() {
      return (
        <View style={styles.container}>
          <Text>Hello World!</Text>
          <Image source={require('./images/your-image.png')} style = {{height: 200, width: 250, }} />
        </View>
      );
    }
    ```

You can also open up the projects in the ios and android directories with their respective IDEs. 

*Figure out the difference between expo init and create-react-native-app and which I should use here.*

Then open http://localhost:3000/ to see your app. For more information, see the [Create-React-Native-App docs](https://github.com/expo/create-react-native-app). **Notice that `create-react-native-app` is different than `create-react-app`.*

> [!TIP]
> If you want to add support for your React Native app so that it runs as a Windows 10 app, see the [Get started with React Native for Windows](https://microsoft.github.io/react-native-windows/docs/getting-started) docs.

npm run android is different and doesn't seem to work ...The Node Metro Bundler launches, says press "d" to open the developer menu.... but doesn't work.


## Use Expo to test your React Native app on an Android device

https://expo.io/learn
https://reactnative.dev/docs/environment-setup


https://codeburst.io/setting-up-development-environment-using-react-native-on-windows-dd240e69f776
https://shift.infinite.red/getting-started-with-react-native-development-on-windows-90d85a72ae65
https://medium.com/@hwanderlustdev/react-native-environment-for-windows-e25806df867c

EXPO vs React CLI: https://stackoverflow.com/questions/54862388/what-is-the-difference-between-expo-cli-and-react-native-cli

React Native get started docs: https://devdocs.io/react_native/