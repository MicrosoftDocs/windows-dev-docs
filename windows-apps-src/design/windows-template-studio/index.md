---
author: QuinnRadich
title: Windows Template Studio
description: Windows Template Studio allows you to easily create robust UWP UIs in Visual Studio
keywords: UWP, Visual Studio, template, Windows Template Studio, template studio
ms.author: quradic
ms.date: 4/4/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
ms.localizationpriority: medium
---

# Windows Template Studio

Windows Template Studio is a Visual Studio extension that allows you to easily create versatile and robust UIs in UWP projects. It's included within the standard UWP workload in Visual Studio, giving you easy access to its features.

## Using Windows Template Studio in your project

### Prerequisites

Windows Template Studio is included in the UWP workload of Visual Studio 2017 version 15.7, and therefore exists within your standard Windows 10 app development environment. If you're missing any of the below, you can get everything from the [Windows Dev Center Download page](https://developer.microsoft.com/windows/downloads).

* Visual Studio 2017 version 15.7 or higher (any edition). Windows Template Studio can be manually installed on versions as early as VS 2017.3, but we highly recommend using it with the latest Visual Studio version.
* Windows 10 Creators Update SDK (version 10.0.15063.0 or later). This is included by default in the UWP workload for Visual Studio 2017.
* .Net Framework 4.7. This is included by default with the Windows 10 Creators Update, but needs to be manually installed if you are on a previous version of Windows 10.
   
### Public Release instructions

No installation is needed on the latest versions of Visual Studio 2017, so long as you're using the UWP workload.

1. Select **File** → **New Project** → **C#** → **Windows Universal**, and you should see Windows Template Studio as an option.
2. To add pages and features to an existing Windows Template Studio project, select the project and choose **Windows Template Studio** → **New page** or **New feature** from the context menu. [More info on adding new items](adding-new-pages.md).
![Add new Page/Feature](images/addNewItem.PNG)

If you're using an older version of Visual Studio, you'll need to manually download Windows Template Studio. Inside Visual Studio, select **Tools** -> **Extensions & Updates**. Select the **Online** node, search for Windows Template Studio, and then select **Download.**

**Note:** Windows Template Studio features can only be added to projects first created by Windows Template Studio. You can't add these features retroactively to an existing project.

## Contributing to Windows Template Studio

Windows Template Studio is an open-source extention that welcomes external contributions. If you're interested in contributing, we encourage you to install a pre-release build, and to check out the project's Github repo for more information.

### Nightly / Pre-release instructions

Pre-release builds of Windows Template Studio allows you to use stable features which have not yet been officially released. Pre-release builds are not definitive and may be subject to change. 

Pre-release builds can be installed side-by-side with the release version of Windows Template Studio. The "nightly" feed provides a new build every day at 3AM GMT, which reflects the current state of the main branch of the [GitHub repo](https://github.com/Microsoft/WindowsTemplateStudio) The Stable feed provides builds which we believe to be free of any breaking changes. However, both the Stable and Nightly pre-release builds are not final, and should be installed at your own risk.

To use the pre-release builds, follow these steps:

1: Open Visual Studio 2017 and go to **Tools→ Extensions & Updates**, then select **Change your Extensions and Updates settings** *(bottom left of the 'Extensions and Updates' window)* and create an Additional Extension Gallery.

![Configure Additional Extension Gallery](images/configurefeed.PNG)

2: Return to **Tools→ Extensions & Updates**. Using the recently added online gallery *(added under the 'Online' node)*, download and install the Windows Template Studio extension. Note that you will likely need to restart Visual Studio for the install to complete. Note that the nightly and pre-release builds can be found here:

* **Nightly:** <https://www.myget.org/F/windows-template-studio-nightly/vsix/>
* **Pre-release (stable):** <https://www.myget.org/F/windows-template-studio-prerelease/vsix/>

![Install UWP Community Templates extension](images/onlinefeed.PNG)

3: Once installed, you will see a new Project Template, which allows you to access to the available templates. The stable pre-release version uses the VNext Template Repository.

![File New Project](images/fileNew.PNG)

### Contributing

You can start working with Windows Template Studio by cloning [our repo](https://github.com/Microsoft/WindowsTemplateStudio) and working locally with the code and the available templates. If you plan to contribute, please follow the [contribution guidelines](https://github.com/Microsoft/WindowsTemplateStudio/blob/dev/CONTRIBUTING.md) and refer to other contribution documentation within the GitHub repo.