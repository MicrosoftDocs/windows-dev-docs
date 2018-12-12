---
title: Windowed vs. full-screen mode
description: Direct3D applications can run in either of two modes windowed or full-screen.
ms.assetid: EE8B9F87-822B-4576-A446-CA603E786862
keywords:
- Windowed vs. full-screen mode
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# <span id="direct3dconcepts.windowed_vs__full-screen_mode"></span>Windowed vs. full-screen mode


Direct3D applications can run in either of two modes: windowed or full-screen. In *windowed mode*, the application shares the available desktop screen space with all running applications. In *full-screen mode*, the window that the application runs in covers the entire desktop, hiding all running applications (including your development environment). Games typically default to full-screen mode to fully immerse the user in the game by hiding all running applications.

Code differences between full-screen mode and windowed mode are very small.

Because an application running in full-screen mode takes over the screen, debugging the application requires either a separate monitor or the use of a remote debugger. One advantage of a windowed-mode application is that you can step through the code in a debugger without multiple monitors or a remote debugger.

## <span id="related-topics"></span>Related topics


[Devices](devices.md)

 

 




