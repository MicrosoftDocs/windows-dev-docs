---
title: Improve performance speed by updating Defender settings
description: Learn how to improve performance speed and build times by updating Windows Defender settings to exclude checking specified file types.
author: mattwojo 
ms.author: mattwoj 
manager: jken
ms.topic: article
keywords: android on windows
ms.date: 02/19/2020
---

# Improve performance speed and build time by updating Windows Defender settings

To do.

On Windows 10, the [Windows Defender Antivirus]() is your default anti-malware engine to protect your device and data from viruses, toolkits, ransomware, and other security threats.

Windows Defender's real-time protection dramatically slows file system access.

The issue that is Windows Defender's Real-time protection is blocking an application from being able to read a file until it has read the file. It should, of course, be scanning in parallel - not synchronously delaying file access.

Fortunately, Windows Defender has the capability to skip synchronous checking of files based on a file extension - called Exclusions:
What you can do is under Windows Defender, click Add an exclusion. Under File Types, click Exclude a file extension. Individually add the extensions:

pas - Delphi Source File
dfm - Delphi Form
dpr - Delphi Project
dproj - Delphi Project File
dcu - DCU file
cs - C# source file
txt - Text File
pst - Outlook mailbox 
https://answers.microsoft.com/en-us/windows/forum/windows_10-performance/windows-defender-real-time-protection-service/fda3f73e-cc0a-4946-9b9d-3c05057ef90c

This way you get all the security of real-time protection, with the blazing speed of unimpeded native file access performance.

Questions: 1. Are settings different for Android Studio (native) vs Xamarin or React (crossplat)? 2. Should this topic be in this page or it's own page or an FAQ?

Windows security software, Windows Defender,
https://twitter.com/Aaronontheweb/status/1227654898843516928?s=20

How to exclude files and folders from Windows Defender Antivirus scans
https://www.windowscentral.com/how-exclude-files-and-folders-windows-defender-antivirus-scans