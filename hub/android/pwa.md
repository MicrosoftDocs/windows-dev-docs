---
title: PWA approach to Android development on Windows
description: Get started developing Android apps using the PWA approach on Windows.
author: mattwojo
ms.author: mattwoj 
manager: jken
ms.topic: article
keywords: android on windows
ms.date: 02/19/2020
---

# Get started developing a PWA for Android

Progressive Web Apps (PWAs) are web-based applications that can be installed and used on an Android device like a native app, accessing the camera, GPS, Bluetooth, notifications, and contact list. This guide will get you started using Windows to create a PWA that will work on Android devices and can be distributed via the web or the Google Play Store.

## Components of a PWA

Service Worker
Trusted Web Activity

## Choosing a framework

You don't need a framework to create a PWAs 
You don't need a platform to create a hybrid app, but they are helpful because they've already taken care of creating a bridge between native APIs and JavaScript APIs.

Progressive Web Apps simply run in the browser so they can be built with basic HTML, CSS and JavaScript.

### Ionic with Apache Cordova

[Apache Cordova](https://cordova.apache.org/) is an open-source framework that enables developers to use a single HTML, CSS, and JavaScript codebase to create an application that will work across device platforms (Android, iOS, Windows).

Cordova renders what would normally be a web application inside a native WebView. WebView is an application component, like a button or tab bar, used to display web content within a native application. WebView is like a web browser without the usual interface elements, such as a URL field or status bar. These types of apps are also referred to as "Hybrid apps." They aren't truly native apps, because the layout rendering happens via web views rather than the native platform UI framework, nor are they purely web-based, because they aren't just web apps but have access to native device APIs and can be packaged for distribution via an app store.

## PhoneGap with Apache Cordova

## Install

## Create a new project

## Deploy to a device emulator

Notes:
Typically, web-based applications are executed within a sandbox, meaning that they do not have direct access to various hardware and software features on the device. A good example of this is the contact database on your mobile device. This database of names, phone numbers, emails, and other bits of information is not accessible to a web app.