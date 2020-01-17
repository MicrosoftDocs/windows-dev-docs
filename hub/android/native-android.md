---
title: Native Android development on Windows
description: Get started developing Android native apps on Windows.
author: mattwojo 
ms.author: mattwoj 
manager: jken
ms.topic: article
keywords: android on windows
ms.localizationpriority: medium
ms.date: 02/19/2020
---

# Native Android development on Windows

The following is a guide to the various workflows available to you for doing Android development using the Windows operating system.

The most straight forward way to create a native Android app is using Android Studio with either Java or Kotlin. The Android Studio SDK tools compile your code, data, and resource files into an APK, an Android package, which is an archive file with an .apk suffix. One APK file contains all the contents of an Android app and is the file that Android-powered devices use to install the app.

Learn more about Android [Application Fundamentals](https://developer.android.com/guide/components/fundamentals).

## Install Android Studio

Android Studio is the official integrated development environment for Google's Android operating system, built on JetBrain's ItelliJ IDEA software. Download the latest version of Android Studio at https://developer.android.com/studio.

- If you downloaded an .exe file (recommended), double-click to launch it.
- If you downloaded a .zip file, unpack the ZIP, copy the android-studio folder into your Program Files folder, and then open the android-studio > bin folder and launch studio64.exe (for 64-bit machines) or studio.exe (for 32-bit machines).

Follow the setup wizard in Android Studio and install any SDK packages that it recommends. As new tools and other APIs become available, Android Studio tells you with a pop-up, or you can check for updates by selecting **Help** > **Check for Update**.

## Create a new project

Select **File** > **New** > **New Project**.

In the **Choose your project** window, you will be able to choose between these templates:

    - **Basic Activity**: Creates a simple app with an app bar, a floating action button and two layout files: one for the activity and one to separate out text content.

    - **Empty Activity**: Creates an empty activity and a single layout file with sample text content.

    - **Bottom Navigation Activity**: Creates a standard bottom navigation bar for an activity. See see the [Bottom Navigation Component](https://material.io/guidelines/components/bottom-navigation.html) in Google's material design guidelines.

    - Learn more about [selecting an activity template](https://developer.android.com/studio/projects/templates#SelectTemplate) in the Android Studio docs.

Templates are commonly used to add new activities to new and existing app modules. For example, to create a login screen for your app’s users, add an activity with the [Login Activity template](https://developer.android.com/studio/projects/templates#LoginActivity).

> [!NOTE]
> The Android operating system is based on components and uses the terms **activity** and **intent** to define interactions. An **activity** represents a single, focused thing that a user can do. They provide a window for building the user interface using classes based on the View class. Learn more about [Activities in the Android docs](https://developer.android.com/reference/android/app/Activity). Activites also have a lifecycle in the Android operating system, defined by a set of six callbacks: `onCreate()`, `onStart()`, `onResume()`, `onPause()`, `onStop()`, and `onDestroy()`. Learn more about the [Activity Lifecycle in the Android docs](https://developer.android.com/guide/components/activities/activity-lifecycle). The activity components interact with one another using **intent** objects. Intent either defines the activity to start or describes the type of action to perform (and the system selects the appropriate activity for you, which can even be from a different application). Learn more about [Intents in the Android docs](https://developer.android.com/reference/android/content/Intent.html).

### Java or Kotlin

### Minimum API Level

You will need to decide the minimum API level for your application. This determines which version of Android your application will support. Lower API levels are older and therefore generally support more devices, but higher API levels are newer and therefor provide more features.

![Android Studio Minimum API selection screen](../images/android-minimum-api-selection.png)

Select the **Help me choose** link to open a comparison chart showing the device support distribution and key features associated with the platform version release.

![Android Studio Minimum API comparison screen](../images/android-minimum-api-selection-2.png)

### Androidx artifacts

You may notice a checkbox to **Use androidx.* artifacts** in your project creation options. AndroidX is the new version of the Android support library and provides backwards-compatibility across Android releases.

> [!NOTE]
> AndroidX provides a consistent namespace starting with the string androidx for all available packages. The former Support Library packages have been mapped into corresponding androidx.* packages. For a full mapping of all the old classes and build artifacts to the new ones, see [Migrating to AndroidX](https://developer.android.com/jetpack/androidx/migrate).



most templates depend on the [Android Support Library](https://developer.android.com/tools/support-library/features.html) to include user interface principles based on [material design](https://developer.android.com/design/material/index.html).

## Accessibility

## Emulating an Android device

## Storing and accessing data

SQLite, MySQL, etc?
 
Where to find the configuration file...

Both configuration files are stored in the configuration folder for Android Studio. The name of the folder depends on your Studio version. For example, Android Studio 3.3 has the folder name AndroidStudio3.3. The location of this folder depends on your operating system:

Windows: %USERPROFILE%\.CONFIGURATION_FOLDER

You can also use the following environment variables to point to specific override files elsewhere:

STUDIO_VM_OPTIONS: set the name and location of the .vmoptions file
STUDIO_PROPERTIES: set the name and location of the .properties file
STUDIO_JDK: set the JDK with which to run Studio

## Android NDK with C and C++

The Android Native Development Kit (NDK) allows you to use C and C++ code with Android. It provides platform libraries you can use to manage native activities and access physical device components, such as sensors and touch input.

The NDK **may not be appropriate for most novice Android programmers**. Unless you have a specific purpose for using the NDK, we recommend sticking with Java code, Android Studio, and the associated framework APIs to develop your app. However, the NDK can be useful for specific cases:

- Squeeze extra performance out of a device to achieve low latency or run computationally intensive applications, such as games or physics simulations.
- Reuse your own or other developers' C or C++ libraries.

Using Android Studio 2.2 and higher, the NDK can be used to compile C and C++ code into a native library. It can then be packaged into an APK using Gradle, the IDE's integrated build system. Your Java code can then call functions in your native library through the Java Native Interface (JNI) framework. Learn more in the [Getting Started with the NDK](https://developer.android.com/ndk/guides) section of the Android Developer Guide.
