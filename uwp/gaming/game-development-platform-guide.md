---
title: Game technologies for Universal Windows Platform (UWP) apps
description: In this guide, you'll learn about the technologies available for developing Universal Windows Platform (UWP) games.
ms.assetid: bc4d4648-0d6e-efbb-7608-80bd09decd6e
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, games, technology, directx
ms.localizationpriority: medium
---

# Game technologies for UWP apps

In this guide, you'll learn about the technologies available for developing Universal Windows Platform (UWP) games.

##  Benefits of Windows 10 for game development

With the introduction of UWP in Windows 10, your Windows 10 titles will be able to span all of the Microsoft platforms. With free migration from previous versions of Windows, there is a steadily increasing number of Windows 10 clients. The combination of these two things means that your Windows 10 titles will be able to reach a huge number of customers through the Microsoft Store.

In addition, Windows 10 offers many new features that are particularly beneficial to games:

-   Reduced memory paging and reduced overall memory system size
-   Improved graphics memory management actively allocates and protects more memory for the foreground game

## UWP games with C++ and DirectX

Real-time games requiring high performance should make use of the DirectX APIs. DirectX is a collection of native APIs for creating games and multimedia applications that require high performance, such as 3D games.

## Development environment

To create games for UWP, you'll need to set up your development environment by installing Visual Studio 2015 or later. We recommend that you install the latest version of Visual Studio, giving you access to the latest development and security updates. Visual Studio allows you to create UWP apps and provides tools for game development:

-   Visual Studio tools for DX game programming - Visual Studio provides tools for creating, editing, previewing, and exporting image, model, and shader resources. There are also tools that you can use to convert resources at build time and debug DirectX graphics code. For more information, see [Use Visual Studio tools for game programming](set-up-visual-studio-for-game-development.md).
-   Visual Studio graphics diagnostics features - Graphics diagnostic tools are now available from within Windows as an optional feature. The diagnostic tools allow you to do graphics debugging, graphics frame analysis, and monitor GPU usage in real time. For more information, see [Use the DirectX runtime and Visual Studio graphics diagnostic features](use-the-directx-runtime-and-visual-studio-graphics-diagnostic-features.md).

For more information, see Prepare your Universal Windows Platform and [DirectX programming](directx-programming.md).

## Getting started with DirectX game project templates

After setting up your development environment, you can use one of the DirectX related project templates to create your UWP DirectX game. Visual Studio 2015 has three templates available for creating new UWP DirectX projects, **DirectX 11 App (Universal Windows)**, **DirectX 12 App (Universal Windows)**, and **DirectX 11 and XAML App (Universal Windows)**. For more information, see [Create a Universal Windows Platform and DirectX game project from a template](user-interface.md).

## Windows 10 APIs

Windows 10 provides an extensive collection of APIs that are useful for game development. There are APIs for almost all aspects of games including, 3D Graphics, 2D Graphics, Audio, Input, Text Resources, User Interface, and networking.

There are many APIs related to game development, but not all games need to use all of the APIs. For example, some games will only use 3D graphics and only make use of Direct3D, some games may only use 2D graphics and only make use of Direct2D, and still other games may make use of both. The following diagram shows the game development related APIs grouped by functionality type.

![game platform technologies](images/gameplatformtechnologies.png)

-   3D Graphics - Windows 10 supports two 3D graphics API sets, Direct3D 11, and [Direct3D 12](/windows/win32/direct3d12/directx-12-programming-guide). Both of these APIs provide the capability to create 3D and 2D graphics. Direct3D 11 and Direct3D 12 are not used together, but either can be used with any of the APIs in the 2D Graphics and UI group. For more information about using the graphics APIs in your game, see [Basic 3D graphics for DirectX games](an-introduction-to-3d-graphics-with-directx.md).

    <table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <thead>
    <tr class="header">
    <th align="left">API</th>
    <th align="left">Description</th>
    </tr>
    </thead>
    <tbody>
    <tr>
    <td align="left">Direct3D 12</td>
    <td align="left"><p>Direct3D 12 introduces the next version of Direct3D, the 3D graphics API at the heart of DirectX. This version of Direct3D is designed to be faster and more efficient than previous versions of Direct3D. The tradeoff for Direct3D 12's increased speed is that it is lower level and requires you to manage your graphics resources yourself and have more extensive graphics programming experience to realize the increased speed.</p>
    <p><strong>When to use</strong></p>
    <p>Use Direct3D 12 when you need to maximize your game's performance and your game is CPU bound.</p>
    <p><strong>For more information</strong></p>
    <p>See the <a href="/windows/win32/direct3d12/directx-12-programming-guide">Direct3d 12</a> documentation.</p></td>
    </tr>
    <tr>
    <td align="left">Direct3D 11</td>
    <td align="left"><p>Direct3D 11 is the previous version of Direct3D and allows you to create 3D graphics using a higher level of hardware abstraction than D3D 12.</p>
    <p><strong>When to use</strong></p>
    <p>Use Direct3D 11 if you have existing Direct3D 11 code, your game is not CPU bound, or you want the benefit of having resources managed for you.</p>
    <p><strong>For more information</strong></p>
    <p>See the <a href="/windows/win32/direct3d11/atoc-dx-graphics-direct3d-11">Direct3D 11</a> documentation.</p></td>
    </tr>
    </tbody>
    </table>

     

-   2D Graphics and UI - APIs concerning 2D graphics such as text and user interfaces. All of the 2D graphics and UI APIs are optional.

    <table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <thead>
    <tr class="header">
    <th align="left">API</th>
    <th align="left">Description</th>
    </tr>
    </thead>
    <tbody>
    <tr>
    <td align="left">Direct2D</td>
    <td align="left"><p>Direct2D is a hardware-accelerated, immediate-mode, 2-D graphics API that provides high performance and high-quality rendering for 2-D geometry, bitmaps, and text. The Direct2D API is built on Direct3D and is designed to interoperate well with GDI, GDI+, and Direct3D.</p>
    <p><strong>When to use</strong></p>
    <p>Direct2D can be used instead of Direct3D to provide graphics for pure 2D games such as a side-scroller or board game, or can be used with Direct3D to simplify creation of 2D graphics in a 3D game, such as a user interface or heads-up-display.</p>
    <p><strong>For more information</strong></p>
    <p>See the <a href="/windows/win32/Direct2D/direct2d-portal">Direct2D</a> documentation.</p></td>
    </tr>
    <tr>
    <td align="left">DirectWrite</td>
    <td align="left"><p>DirectWrite provides extra capabilities for working with text and can be used with Direct3D or Direct2D to provide text output for user interfaces or other areas where text is required. DirectWrite supports measuring, drawing, and hit-testing of multi-format text. DirectWrite handles text in all supported languages for global and localized applications. DirectWrite also provides a low-level glyph rendering API for developers who want to perform their own layout and Unicode-to-glyph processing.</p>
    <p><strong>When to use</strong></p>
    <p></p>
    <p><strong>For more information</strong></p>
    <p>See the <a href="/windows/win32/DirectWrite/direct-write-portal">DirectWrite</a> documentation.</p></td>
    </tr>
    <tr>
    <td align="left">DirectComposition</td>
    <td align="left"><p>DirectComposition is a Windows component that enables high-performance bitmap composition with transforms, effects, and animations. Application developers can use the DirectComposition API to create visually engaging user interfaces that feature rich and fluid animated transitions from one visual to another.</p>
    <p><strong>When to use</strong></p>
    <p>DirectComposition is designed to simplify the process of composing visuals and creating animated transitions. If your game requires complex user interfaces, you can use DirectComposition to simplify the creation and management of the UI.</p>
    <p><strong>For more information</strong></p>
    <p>See the <a href="/windows/win32/directcomp/directcomposition-portal">DirectComposition</a> documentation.</p></td>
    </tr>
    </tbody>
    </table>

     

-   Audio - APIs concerning playing audio and applying audio effects. For information about using the audio APIs in your game, see [Audio for games](working-with-audio-in-your-directx-game.md).

    <table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <thead>
    <tr class="header">
    <th align="left">API</th>
    <th align="left">Description</th>
    </tr>
    </thead>
    <tbody>
    <tr>
    <td align="left">XAudio2</td>
    <td align="left"><p>XAudio2 is a low-level audio API that provides a foundation for signal processing and mixing. XAudio is designed to be very responsive for game audio engines while maintaining the ability to create custom audio effects and complex chains of audio effects and filters.</p>
    <p><strong>When to use</strong></p>
    <p>Use XAudio2 when your game needs to play sounds with minimal overhead and delay.</p>
    <p><strong>For more information</strong></p>
    <p>See the <a href="/windows/win32/xaudio2/xaudio2-apis-portal">XAudio2</a> documentation.</p></td>
    </tr>
    <tr>
    <td align="left">Audio graphs</td>
    <td align="left"><p>For functionality that you can implement with XAudio2, you have the alternative of using the Windows Runtime audio graph APIs instead. To help you decide between the two alternatives, see <a href="/windows/uwp/audio-video-camera/audio-graphs#choosing-windows-runtime-audiograph-or-xaudio2">Choosing Windows Runtime AudioGraph or XAudio2</a>.</p>
    <p><strong>When to use</strong></p>
    <p>Use audio graphs when your game needs to play sounds with minimal overhead and delay, but with a significantly easier-to-use API than XAudio2, and with the option of C# support.</p>
    <p><strong>For more information</strong></p>
    <p>See the <a href="/windows/uwp/audio-video-camera/audio-graphs">Audio graphs</a> documentation.</p></td>
    </tr>
    <tr>
    <td align="left">Media Foundation</td>
    <td align="left"><p>Microsoft Media Foundation is designed for the playback of media files and streams, both audio and video, but can also be used in games when higher level functionality than XAudio2 is required and some additional overhead is acceptable.</p>
    <p><strong>When to use</strong></p>
    <p>Media foundation is particularly useful for cinematic scenes or non-interactive components of your game. Media foundation is also useful for decoding audio files for playback using XAudio2.</p>
    <p><strong>For more information</strong></p>
    <p>See the <a href="/windows/win32/medfound/microsoft-media-foundation-sdk">Microsoft Media Foundation</a> overview.</p></td>
    </tr>
    </tbody>
    </table>

     

-   Input - APIs concerning input from the keyboard, mouse, gamepad, and other user input sources.

    <table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <thead>
    <tr class="header">
    <th align="left">API</th>
    <th align="left">Description</th>
    </tr>
    </thead>
    <tbody>
    <tr>
    <td align="left">XInput</td>
    <td align="left"><p>The XInput Game Controller API enables applications to receive input from game controllers.</p>
    <p><strong>When to use</strong></p>
    <p>If your game needs to support gamepad input and you have existing XInput code, you can continue to make use of XInput. XInput has been replaced by Windows.Gaming.Input for UWP, and if you're writing new input code, you should use Windows.Gaming.Input instead of XInput.</p>
    <p><strong>For more information</strong></p>
    <p>See the <a href="/windows/win32/xinput/xinput-game-controller-apis-portal">XInput</a> documentation.</p></td>
    </tr>
    <tr>
    <td align="left">Windows.Gaming.Input</td>
    <td align="left"><p>The Windows.Gaming.Input API replaces XInput and provides the same functionality with the following advantages over Xinput:</p>
    <ul>
    <li>Lower resource usage</li>
    <li>Lower API call latency for retrieving input</li>
    <li>The ability to work with more than 4 gamepads at once</li>
    <li>The ability to access additional gamepad features, such as the trigger vibration motors</li>
    <li>The ability to be notified when controllers connect/disconnect via event instead of polling</li>
    <li>The ability to attribute input to a specific user (Windows.System.User)</li>
    </ul>
    <p><strong>When to use</strong></p>
    <p>If your game needs to support gamepad input and is not using existing XInput code or you need one of the benefits listed above, you should make use of Windows.Gaming.Input.</p>
    <p><strong>For more information</strong></p>
    <p>See the <a href="/uwp/api/Windows.Gaming.Input">Windows.Gaming.Input</a> documentation.</p></td>
    </tr>
    <tr>
    <td align="left">Windows.UI.Core.CoreWindow</td>
    <td align="left"><p>The Windows.UI.Core.CoreWindow class provides events for tracking pointer presses and movement, and key down and key up events.</p>
    <p><strong>When to use</strong></p>
    <p>Use Windows.UI.Core.CoreWindows events when you need to track the mouse or key presses in your game.</p>
    <p><strong>For more information</strong></p>
    <p>See <a href="/windows/uwp/gaming/tutorial--adding-move-look-controls-to-your-directx-game">Move-look controls for games</a> for more information about using the mouse or keyboard in your game.</p></td>
    </tr>
    </tbody>
    </table>

     

-   Math - APIs concerning simplifying commonly used mathematical operations.

    <table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <thead>
    <tr class="header">
    <th align="left">API</th>
    <th align="left">Description</th>
    </tr>
    </thead>
    <tbody>
    <tr>
    <td align="left">DirectXMath</td>
    <td align="left"><p>The DirectXMath API provides SIMD-friendly C++ types and functions for common linear algebra and graphics math operations common to games.</p>
    <p><strong>When to use</strong></p>
    <p>Use of DirectXMath is optional and simplifies common mathematical operations.</p>
    <p><strong>For more information</strong></p>
    <p>See the <a href="/windows/win32/dxmath/directxmath-portal">DirectXMath</a> documentation.</p></td>
    </tr>
    </tbody>
    </table>

     

-   Networking - APIs concerning communicating with other computers and devices over either the Internet or private networks.

    <table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <thead>
    <tr class="header">
    <th align="left">API</th>
    <th align="left">Description</th>
    </tr>
    </thead>
    <tbody>
    <tr>
    <td align="left">Windows.Networking.Sockets</td>
    <td align="left"><p>The Windows.Networking.Sockets namespace provides TCP and UDP sockets that allow reliable or unreliable network communication.</p>
    <p><strong>When to use</strong></p>
    <p>Use Windows.Networking.Sockets if your game needs to communicate with other computers or devices over the network.</p>
    <p><strong>For more information</strong></p>
    <p>See <a href="/windows/uwp/gaming/work-with-networking-in-your-directx-game">Work with networking in your game</a>.</p></td>
    </tr>
    <tr>
    <td align="left">Windows.Web.HTTP</td>
    <td align="left"><p>The Windows.Web.HTTP namespace provides a reliable connection to HTTP servers that can be used to access a web site.</p>
    <p><strong>When to use</strong></p>
    <p>Use Windows.Web.HTTP when your game needs to access a web site to retrieve or store information.</p>
    <p><strong>For more information</strong></p>
    <p>See <a href="/windows/uwp/gaming/work-with-networking-in-your-directx-game">Work with networking in your game</a>.</p></td>
    </tr>
    </tbody>
    </table>

     

-   Support Utilities - Libraries that build on the Windows 10 APIs.

    <table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <thead>
    <tr class="header">
    <th align="left">Library</th>
    <th align="left">Description</th>
    </tr>
    </thead>
    <tbody>
    <tr>
    <td align="left">DirectX Tool Kit</td>
    <td align="left"><p>The DirectX Tool Kit (DirectXTK) is a collection of helper classes for writing DirectX 11.x code in C++.</p>
    <p><strong>When to use</strong></p>
    <p>Use the DirectX Tool Kit if you're a C++ developer looking for a modern replacement to the legacy D3DX utility code or you're an XNA Game Studio developer transitioning to native C++.</p>
    <p><strong>For more information</strong></p>
    <p>See the DirectX Tool Kit project page, <a href="https://github.com/Microsoft/DirectXTK">https://github.com/Microsoft/DirectXTK</a>.</p></td>
    </tr>
    <tr>
    <td align="left">Win2D</td>
    <td align="left"><p>Win2D is an easy-to-use Windows Runtime API for immediate mode 2D graphics rendering.</p>
    <p><strong>When to use</strong></p>
    <p>Use Win2D if you're a C++ developer and want an easier to use WinRT wrapper for Direct2D and DirectWrite, or you're a C# developer wanting to use Direct2D and DirectWrite.</p>
    <p><strong>For more information</strong></p>
    <p>See the Win2D project page, <a href="https://github.com/Microsoft/Win2D">https://github.com/Microsoft/Win2D</a>.</p></td>
    </tr>
    </tbody>
    </table>

## Xbox Live Services

The [Xbox Developer Programs](https://developer.microsoft.com/en-US/games/publish/) allow any developer to integrate Xbox Live into their UWP game and publish to Xbox One and Windows 10. Integrate Xbox Live social experiences such as sign-in, presence, leaderboards, and more into your title, with minimal development time. Xbox Live social features are designed to organically grow your audience, spreading awareness to over 55 million active gamers.

If you want access to even more Xbox Live capabilities, dedicated marketing and development support, and the chance to be featured in the main Xbox One store, apply to the [ID@Xbox](https://www.xbox.com/developers/id) program. To see which features are available to the Xbox Live Creators Program and ID@Xbox program, see the [Feature table](/gaming/gdk/_content/gc/live/get-started/live-xbl-overview).

For more info, go to [Adding Xbox Live to your game](e2e.md#adding-xbox-live-to-your-game).

##  Alternatives to writing games with DirectX and UWP

### UWP games without DirectX

Simpler games with minimal performance requirements, such as card games or board games, can be written without DirectX and don't necessarily need to be written in C++. These sort of games can make use of any of the languages supported by UWP such as C#, Visual Basic, C++, and HTML/JavaScript. If performance and intensive graphics are not a requirement for your game, checkout [JavaScript and HTML5 touch game sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/master/Official%20Windows%20Platform%20Sample/JavaScript%20and%20HTML5%20touch%20game%20sample/%5BJavaScript%5D-JavaScript%20and%20HTML5%20touch%20game%20sample/JavaScript) as an example.

### Game engines

As an alternative to writing your own game engine using the Windows game development APIs, many high quality game engines that build on the Windows game development APIs are available for developing games on Windows platforms. When considering a game engine or library, you have multiple options:

-   Full game engine - A full game engine encapsulates most or all of the Windows 10 APIs you would use when writing a game engine from scratch, such as graphics, audio, input, and networking. Full game engines may also provide game logic functionality such as artificial intelligence and pathfinding.
-   Graphics engine - Graphics engines encapsulate the Windows 10 graphics APIs, manage graphics resources, and support a variety of model and world formats.
-   Audio engine - Audio engines encapsulate the Windows 10 audio APIs, manage audio resources, and provide advanced audio processing and effects.
-   Network engine - Network engines encapsulate Windows 10 networking APIs for adding peer-to-peer or server-based multiplayer support to your game, and may include advanced networking functionality to support large numbers of players.
-   Artificial intelligence and pathfinding engine - AI and pathfinding engines provide a framework for controlling the behavior of agents in your game.
-   Special purpose engines - A variety of additional engines exist for handling almost any game development related task you might run into, such as creating inventory systems and dialog trees.

## Submitting a game to the Microsoft Store

Once you’re ready to publish your game, you’ll need to create a developer account and submit your game to the Microsoft Store.

For information about submitting your game to the Microsoft Store, see [Submitting and publishing your game](e2e.md#submitting-and-publishing-your-game).
