---
title: Distributing a managed Windows Runtime Component
description: Learn ways to plan a distributable Windows Runtime Component and distribute it by using file copy or extension SDK.
ms.assetid: 80262992-89FC-42FC-8298-5AABF58F8212
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Distributing a managed Windows Runtime Component

You can distribute your Windows Runtime Component by file copy. However, if your component consists of many files, installation can be tedious for your users. Also, errors in placing files or failure to set references might cause problems for them. You can package a complex component as a Visual Studio extension SDK, to make it easy to install and use. Users only need to set one reference for the entire package. They can easily locate and install your component by using the **Extensions and Updates** dialog box, as described in [Finding and Using Visual Studio Extensions](/visualstudio/ide/finding-and-using-visual-studio-extensions?view=vs-2015).

## Planning a distributable Windows Runtime Component

Choose unique names for binary files, such as your .winmd files. We recommend the following format to ensure uniqueness:

``` syntax
company.product.purpose.extension
For example: Microsoft.Cpp.Build.dll
```

Your binary files will be installed in app packages, possibly with binary files from other developers. See "Extension SDKs" in [How to: Create a Software Development Kit](/visualstudio/extensibility/creating-a-software-development-kit?view=vs-2015).

To decide how to distribute your component, consider how complex it is. An extension SDK or similar package manager is recommended when:

-   Your component consists of multiple files.
-   You provide versions of your component for multiple platforms (x86 and ARM, for example).
-   You provide both debug and release versions of your component.
-   Your component has files and assemblies that are used only at design time.

An extension SDK is particularly useful if more than one of the above are true.

> **Note**  For complex components, the NuGet package management system offers an open source alternative to extension SDKs. Like extension SDKs, NuGet enables you to create packages that simplify the installation of complex components. For a comparison of NuGet packages and Visual Studio extension SDKs, see [Adding References Using NuGet Versus an Extension SDK](/visualstudio/ide/adding-references-using-nuget-versus-an-extension-sdk?view=vs-2015).

## Distribution by file copy

If your component consists of a single .winmd file, or a .winmd file and a resource index (.pri) file, you can simply make the .winmd file available for users to copy. Users can put the file wherever they want to in a project, use the **Add Existing Item** dialog box to add the .winmd file to the project, and then use the Reference Manager dialog box to create a reference. If you include a .pri file or an .xml file, instruct users to place those files with the .winmd file.

> **Note**  Visual Studio always produces a .pri file when you build your Windows Runtime Component, even if your project doesn't include any resources. If you have a test app for your component, you can determine whether the .pri file is used by examining the contents of the app package in the bin\\debug\\AppX folder. If the .pri file from your component doesn't appear there, you don't need to distribute it. Alternatively, you can use the [MakePRI.exe](/previous-versions/windows/apps/jj552945(v=win.10)) tool to dump the resource file from your Windows Runtime Component project. For example, in the Visual Studio Command Prompt window, type: makepri dump /if MyComponent.pri /of MyComponent.pri.xml You can read more about .pri files in [Resource Management System (Windows)](/previous-versions/windows/apps/jj552947(v=win.10)).

## Distribution by extension SDK

A complex component usually includes Windows resources, but see the note about detecting empty .pri files in the previous section.

**To create an extension SDK**

1.  Make sure you have the Visual Studio SDK installed. You can download the Visual Studio SDK from the [Visual Studio Downloads](https://visualstudio.microsoft.com/downloads/download-visual-studio-vs) page.
2.  Create a new project using the VSIX Project template. You can find the template under Visual C# or Visual Basic, in the Extensibility category. This template is installed as part of the Visual Studio SDK. ([Walkthrough: Creating an SDK using C# or Visual Basic](/visualstudio/extensibility/walkthrough-creating-an-sdk-using-csharp-or-visual-basic?view=vs-2015) or [Walkthrough: Creating an SDK using C++](/visualstudio/extensibility/walkthrough-creating-an-sdk-using-cpp?view=vs-2015), demonstrates the use of this template in a very simple scenario. )
3.  Determine the folder structure for your SDK. The folder structure begins at the root level of your VSIX project, with the **References**, **Redist**, and **DesignTime** folders.

    -   **References** is the location for binary files that your users can program against. The extension SDK creates references to these files in your users' Visual Studio projects.
    -   **Redist** is the location for other files that must be distributed with your binary files, in apps that are created by using your component.
    -   **DesignTime** is the location for files that are used only when developers are creating apps that use your component.

    In each of these folders, you can create configuration folders. The permitted names are debug, retail, and CommonConfiguration. The CommonConfiguration folder is for files that are the same whether they're used by retail or debug builds. If you're only distributing retail builds of your component, you can put everything in CommonConfiguration and omit the other two folders.

    In each configuration folder, you can provide architecture folders for platform-specific files. If you use the same files for all platforms, you can supply a single folder named neutral. You can find details of the folder structure, including other architecture folder names, in [How to: Create a Software Development Kit](/visualstudio/extensibility/creating-a-software-development-kit?view=vs-2015). (That article discusses both platform SDKs and extension SDKs. You may find it useful to collapse the section about platform SDKs, to avoid confusion. )

4.  Create an SDK manifest file. The manifest specifies name and version information, the architectures your SDK supports, .NET versions, and other information about the way Visual Studio uses your SDK. You can find details and an example in [How to: Create a Software Development Kit](/visualstudio/extensibility/creating-a-software-development-kit?view=vs-2015).
5.  Build and distribute the extension SDK. For in-depth information, including localizing and signing the VSIX package, see [VSIX Deployment](/visualstudio/misc/how-to-manually-package-an-extension-vsix-deployment?view=vs-2015).

## Related topics

* [Creating a Software Development Kit](/visualstudio/extensibility/creating-a-software-development-kit?view=vs-2015)
* [NuGet package management system](https://github.com/NuGet/Home)
* [Resource Management System (Windows)](/previous-versions/windows/apps/jj552947(v=win.10))
* [Finding and Using Visual Studio Extensions](/visualstudio/ide/finding-and-using-visual-studio-extensions?view=vs-2015)
* [MakePRI.exe command options](/previous-versions/windows/apps/jj552945(v=win.10))