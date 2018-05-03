---
author: laurenhughes
title: Optional packages with executable code
description: Learn how to use Visual Studio to create an optional package with executable code. 
ms.author: lahugh
ms.date: 5/07/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, app installer, AppInstaller, sideload, related set, optional packages
ms.localizationpriority: medium
---

# Optional packages with executable code
 
Optional packages with executable code are useful for dividing a large or complex app, or for adding on to an app that's already been published. With Visual Studio 2017, version 15.7 and .NET Native 2.1, you can load executable code from both C++ and C# optional packages.

## Prerequisites
- Visual Studio 2017, version 15.7
- Windows 10, version 1709
- Windows 10, version 1709 SDK

To get the latest development tools, see [Downloads and tools for Windows 10](https://developer.microsoft.com/windows/downloads). 

> [!NOTE]
> To submit an app that uses optional packages and/or related sets to the Store, you will need permission. Optional packages and related sets can be used for Line of Business (LOB) or enterprise apps without Dev Center permission if they are not submitted to the Store. See [Windows developer support](https://developer.microsoft.com/windows/support) to get permission to submit an app that uses optional packages and related sets.

## C++ Optional packages with executable code

To load code from a C++ optional package, see the [OptionalPackageSample](https://github.com/AppInstaller/OptionalPackageSample) repository on GitHub. The [OptionalPackageDLL](https://github.com/AppInstaller/OptionalPackageSample/tree/master/OptionalPackageDLL) shows how to create a project with code that can be executed from the main package. The MyMainApp project demonstrates how to [load code](https://github.com/AppInstaller/OptionalPackageSample/blob/bf6b4915ff1f3b8abfdaacb1ad9e77184c49fe18/MyMainApp/MainPage.xaml.cpp#L182) from the OptionalPackageDLL.dll file.

## C# Optional packages with executable code

To get started building an optional code package in C#, follow the below steps to configure your solution:

1. Create a new UWP application with the minimum version set to the Windows 10 Fall Creators Update SDK (Build 16299) or higher.

2. Add a new **Optional Code Package (Universal Windows)** project to the solution. Ensure the **Minimum Version** and **Target Version** match that of your main app.

3. If you plan to submit your apps to the Store, right click on both projects and select **Store -> Associate App with the Store...**

4. Open the `Package.appxmanifest` file of the main app and find the `Identity Name` value. Make a note of this value for the next step.

5. Open the optional app package's `Package.appxmanifest` file and find the `uap3:MainAppPackageDependency Name` value. Update the `uap3:MainAppPackageDependency Name` value to match the `Identity Name` value of the main app package from the previous step. 

    Here's an example of the `Identity` from the main app's `Package.appxmanifest`.
    ```XML
    <Identity Name="12345.MainAppProject" Publisher="CN=PublisherName" Version="1.0.0.0" />
    ```

    The optional app package's `uap3:MainPackageDependency` needs to be updated to match the main app's `Identity`.
    ```XML
    <uap3:MainPackageDependency Name="12345.MainAppProjectTest" />
    ```

6. Add a `Bundle.mapping.txt` file to the main app. Follow the steps in this [Related sets](https://docs.microsoft.com/windows/uwp/packaging/optional-packages#related-sets) section to create a related set containing both apps. 

7. Build the optional package project and then navigate to the package Reference folder in the output from the build found at `..\[PathToOptionalPackageProject]\bin\[architecture]\[configuration]\Reference`. Note that you can choose any architecture in the path to the Reference folder since the `.winmd` file (step 8) is architecture independent.

8. Add a reference from the main app project to the `.winmd` file found in this folder. Every time you change the API surface area in the optional package project, this `.winmd` file **must** be updated. This reference provides the main app project with the necessary information to compile.

9. In the main app project, navigate to the project build properties and select **Compile with .NET Native tool chain**. Currently, only debugging in .NET Native is supported for optional code package creation in C#. Go to the project debug properties and select **Deploy optional packages**. This will ensure that both packages are in sync whenever you deploy the main app project.

Once you're finished with these steps, you can add code to the optional package project as if it were a managed WinRT Component project. To access the code in the main app project, call the public methods exposed in the optional package project.