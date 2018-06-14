---
author: laurenhughes
title: Developing with asset packages and package folding
description: Learn how to efficiently organize your app with asset packages and package folding.
ms.author: lahugh
ms.date: 04/30/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, packaging, package layout, asset package
ms.localizationpriority: medium
---

# Developing with asset packages and package folding 

> [!IMPORTANT]
> If you intend to submit your app to the Store, you need to contact [Windows developer support](https://developer.microsoft.com/windows/support) and get approval to use asset packages and package folding.

Asset packages can decrease the overall packaging size and publishing time for your apps to the Store. You can learn more about asset packages and how it can speed up your development iterations at [Introduction to asset packages](asset-packages.md).

If you are thinking about using asset packages for your app or already know that you want to use it, then you are probably wondering about how asset packages will change your development process. In short, app development for you stays the same - this is possible because of package folding for asset packages.

## File access after splitting your app

To understand how package folding doesn’t impact your development process, let’s step back first to understand what happens when you split your app into multiple packages (with either asset packages or resource packages). 

At a high level, when you split some of your app’s files into other packages (that are not architecture packages), you will not be able to access those files directly relative to where your code runs. This is because these packages are all installed into different directories from where your architecture package is installed. For example, if you’re making a game and your game is localized into French and German and you built for both x86 and x64 machines, then you should have these .appx package files within the app bundle of your game:

- 	MyGame_1.0_x86.appx
- 	MyGame_1.0_x64.appx
- 	MyGame_1.0_language-fr.appx
- 	MyGame_1.0_language-de.appx

When your game is installed to a user’s machine, each .appx file will have its own folder in the **WindowsApps** directory. So for a French user running 64-bit Windows, your game will look like this:

```example
C:\Program Files\WindowsApps\
|-- MyGame_1.0_x64
|   `-- …
|-- MyGame_1.0_language-fr
|   `-- …
`-- …(other apps)
```

Note that the .appx package file that are not applicable to the user will not be installed (the x86 and German packages). 

For this user, your game’s main executable will be within the **MyGame_1.0_x64** folder and will run from there, and normally, it will only have access to the files within this folder. In order to access the files in the **MyGame_1.0_language-fr** folder, you would have to use either the MRT APIs or the PackageManager APIs. The MRT APIs can automatically select the most appropriate file from the languages installed, you can find out more about MRT APIs at [Windows.ApplicationModel.Resources.Core](https://docs.microsoft.com/uwp/api/windows.applicationmodel.resources.core). Alternatively, you can find the installed location of the French language package using the [PackageManager Class](https://docs.microsoft.com/uwp/api/Windows.Management.Deployment.PackageManager). You should never assume the installed location of the packages of your app since this can change and can vary between users. 

## Asset package folding

So how can you access the files in your asset packages? Well, you can continue to use the file access APIs you are using to access any other file in your architecture package. This is because asset package files will be folded into your architecture package when it is installed through the package folding process. Furthermore, since asset package files should originally be files within your architecture packages, this means that you would not have to change API usage when you move from loose files deployment to packaged deployment in your development process. 

To understand more about how package folding works, let’s start with an example. If you have a game project with the following file structure:

```example
MyGame
|-- Audios
|   |-- Level1
|   |   `-- ...
|   `-- Level2
|       `-- ...
|-- Videos
|   |-- Level1
|   |   `-- ...
|   `-- Level2
|       `-- ...
|-- Engine
|   `-- ...
|-- XboxLive
|   `-- ...
`-- Game.exe
```

If you want to split your game into 3 packages: an x64 architecture package, an asset package for audios, and an asset package for videos, your game will be divided into these packages:

```example
MyGame_1.0_x64.appx
|-- Engine
|   `-- ...
|-- XboxLive
|   `-- ...
`-- Game.exe
MyGame_1.0_Audios.appx
`-- Audios
    |-- Level1
    |   `-- ...
    `-- Level2
        `-- ...
MyGame_1.0_Videos.appx
`-- Videos
    |-- Level1
    |   `-- ...
    `-- Level2
        `-- ...
```

When you install your game, the x64 package will be deployed first. Then the two asset packages will still be deployed to their own folders, just like **MyGame_1.0_language-fr** from our previous example. However, because of package folding, the asset package files will also be hard linked to appear in the **MyGame_1.0_x64** folder (so even though the files appear in two locations, they do not take up twice the disk space). The location in which the asset package files will appear in is exactly the location that they are at relative to the root of the package. So here’s what the final layout of the deployed game will look like:

```example 
C:\Program Files\WindowsApps\
|-- MyGame_1.0_x64
|   |-- Audios
|   |   |-- Level1
|   |   |   `-- ...
|   |   `-- Level2
|   |       `-- ...
|   |-- Videos
|   |   |-- Level1
|   |   |   `-- ...
|   |   `-- Level2
|   |       `-- ...
|   |-- Engine
|   |   `-- ...
|   |-- XboxLive
|   |   `-- ...
|   `-- Game.exe
|-- MyGame_1.0_Audios
|   `-- Audios
|       |-- Level1
|       |   `-- ...
|       `-- Level2
|           `-- ...
|-- MyGame_1.0_Videos
|   `-- Videos
|       |-- Level1
|       |   `-- ...
|       `-- Level2
|           `-- ...
`-- …(other apps)
```

When using package folding for asset packages, you can still access the files you’ve split into asset packages the same way (notice that the architecture folder has the exact same structure as the original project folder), and you can add asset packages or move files between asset packages without impacting your code. 

Now for a more complicated package folding example. Let’s say that you want to split your files based on level instead, and if you want to keep the same structure as the original project folder, your packages should look like this:

```example
MyGame_1.0_x64.appx
|-- Engine
|   `-- ...
|-- XboxLive
|   `-- ...
`-- Game.exe
MyGame_Level1.appx
|-- Audios
|   `-- Level1
|       `-- ...
`-- Videos
    `-- Level1
        `-- ...

MyGame_Level2.appx
|-- Audios
|   `-- Level2
|       `-- ...
`-- Videos
    `-- Level2
        `-- ...
```
This will allow the **Level1** folders and files in the **MyGame_Level1** package and **Level2** folders and files in the **MyGame_Level2** package to be merged into the **Audios** and **Videos** folders during package folding. So as a general rule, the relative path designated for packaged files in the mapping file or [packaging layout](packaging-layout.md) for MakeAppx.exe is the path you should use to access them after package folding. 

Lastly, if there are two files in different asset packages that have the same relative paths, this will cause a collision during package folding. If a collision occurs, the deployment of your app will result in an error and fail. Also, because package folding takes advantage of hard links, if you do use asset packages, your app will not be able to be deployed to non-NTFS drives. If you know your app will likely be moved to removable drives by your users, then you should not use asset packages. 


