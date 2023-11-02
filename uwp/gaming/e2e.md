---
title: Windows game development guide
description: An end-to-end guide to resources and information for developing Universal Windows Platform (UWP) games.
ms.assetid: 6061F498-96A8-44EF-9711-68AE5A1218C9
ms.date: 01/28/2022
ms.topic: article
keywords: windows 11, windows 10, windows, uwp, games, game development
ms.localizationpriority: medium
---

# Windows game development guide

Welcome to the Windows game development guide!

This guide provides an end-to-end collection of the resources and information you'll need to develop a Universal Windows Platform (UWP) game. An English (US) version of this guide is available in [PDF](https://download.microsoft.com/download/9/C/9/9C9D344F-611F-412E-BB01-259E5C76B17F/Windev_Game_Dev_Guide_Oct_2017.pdf) format.

## Introduction to game development for the Universal Windows Platform (UWP)

When you create a Windows game, you have the opportunity to reach millions of players worldwide across PC and Xbox One. With Xbox on Windows, Xbox Live, cross-device multiplayer, an amazing gaming community, and powerful new features like the Universal Windows Platform (UWP) and DirectX 12, Windows games thrill players of all ages and genres. The Universal Windows Platform (UWP) delivers compatibility for your game across Windows devices with a common API for PC and and Xbox One, along with tools and options to tailor your game to each device experience.

This guide provides an end-to-end collection of information and resources that will help you as you develop your game. The sections are organized according to the stages of game development, so you'll know where to look for information when you need it.

If you're new to developing games on Windows or Xbox, the [Getting started](getting-started.md) guide might be where you want to start off. The [Game development resources](#game-development-resources) section also provides a high-level survey of documentation, programs, and other resources that are helpful when creating a game. If you want to start by looking at some UWP code instead, see [Game samples](#game-samples).

## Game development resources

From documentation to developer programs, forums, blogs, and samples, there are many resources available to help you on your game development journey. Here's a roundup of resources to know about as you begin developing your Windows game.

> [!NOTE]
> Some features are managed through various programs. This guide covers a broad range of resources, so you might find that some resources are inaccessible depending on the program you're in, or your specific development role. Examples are links that resolve to `developer.xboxlive.com`, `forums.xboxlive.com`, `xdi.xboxlive.com`, or the Game Developer Network (GDN). For information about partnering with Microsoft, see [Developer programs](#developer-programs).

### Game development documentation

Throughout this guide, you'll find deep links to relevant documentation&mdash;organized by task, technology, and stage of game development. To give you a broad view of what's available, here are the main documentation portals for Windows game development.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Windows Dev Center main portal</td>
        <td><a href="https://developer.microsoft.com/windows">Windows Dev Center</a></td>
    </tr>
    <tr>
        <td>Developing Windows apps</td>
        <td><a href="https://developer.microsoft.com/windows/apps/develop">Develop Windows apps</a></td>
    </tr>
    <tr>
        <td>Universal Windows Platform app development</td>
        <td><a href="/windows/uwp/">Universal Windows Platform documentation</a></td>
    </tr>
    <tr>
        <td>How-to guides for UWP games</td>
        <td><a href="index.md">Games and DirectX</a> </td>
    </tr>
    <tr>
        <td>DirectX reference and overviews</td>
        <td><a href="/windows/win32/directx">DirectX Graphics and Gaming</a></td>
    </tr>
    <tr>
        <td>Azure for gaming</td>
        <td><a href="https://azure.microsoft.com/solutions/gaming/">Build and scale your games using Azure</a></td>
    </tr>
    <tr>
        <td>Azure PlayFab</td>
        <td><a href="https://playfab.com">Complete backend solution for live games</a></td>
    </tr>
    <tr>
        <td>UWP on Xbox One</td>
        <td><a href="/windows/uwp/xbox-apps/index">Building UWP apps on Xbox One</a></td>
    </tr>
    <tr>
        <td>UWP on HoloLens</td>
        <td><a href="https://developer.microsoft.com/windows/mixed-reality/development_overview">Building UWP apps on HoloLens</a></td>
    </tr>
    <tr>
        <td>Xbox Live documentation</td>
        <td><a href="/gaming/xbox-live/">Xbox Live developer guide</a></td>
    </tr>
    <tr>
        <td>Xbox One development documentation (XGD)</td>
        <td><a href="https://developer.microsoft.com/games/xbox/partner/development-home">Xbox One Development</a></td>
    </tr>
    <tr>
        <td>Xbox One development whitepapers (XGD)</td>
        <td><a href="https://developer.microsoft.com/games/xbox/partner/development-education-whitepapers">White Papers</a></td>
    </tr>
    <tr>
        <td>Mixer Interactive documentation</td>
        <td>Add interactivity to your game</td>
    </tr>
</table>

### Partner Center

[Registering as a developer in Partner Center](https://developer.microsoft.com/microsoft-store/register/) is the first step towards publishing your Windows game. A developer account lets you reserve your game's name, and submit free or paid games to the Microsoft Store for all Windows devices. Use your developer account to manage your game and in-game products, get detailed analytics, and enable services that create great experiences for your players around the world.

Microsoft also offers several developer programs to help you develop and publish Windows games. We recommend seeing whether any are right for you before registering for a Partner Center account. For more info, go to [Developer programs](#developer-programs)

### Developer programs

Microsoft offers several developer programs to help you develop and publish Windows games. Consider joining a developer program if you want to develop games for Xbox One, and integrate Xbox Live features in your game. To publish a game in the Microsoft Store, you'll also need to create a developer account in [Partner Center](https://partner.microsoft.com/dashboard) .

#### Xbox Live Creators Program

The Xbox Live Creators Program allows anyone to integrate Xbox Live into their title, and publish to Xbox One and Windows. There's a simplified certification process, and no concept approval is required outside of the standard [Microsoft Store policies](/legal/windows/agreements/store-policies).

You can deploy, design, and publish your game in the Creators Program without a dedicated dev kit, using only retail hardware. To get started, download the [Dev Mode Activation app](../xbox-apps/devkit-activation.md) on your Xbox One.

If you want access to even more Xbox Live capabilities, dedicated marketing and development support, and the chance to be featured in the main Xbox One store, then apply to the [ID@Xbox](https://www.xbox.com/Developers/id) program.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Xbox Live Creators Program</td>
        <td><a href="https://developer.microsoft.com/games/xbox/xboxlive/creator">Learn more about the Xbox Live Creators Program</a></td>
    </tr>
</table>

#### ID@Xbox

The ID@Xbox program helps qualified game developers self-publish on Windows and Xbox One. If you want to develop for Xbox One, or add Xbox Live features like Gamerscore, achievements, and leaderboards to your Windows game, then sign up with `ID@Xbox`. Become an `ID@Xbox` developer to get the tools and support you need to unleash your creativity and maximize your success. We recommend that you apply to `ID@Xbox` before registering for a developer account in Partner Center.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>ID@Xbox developer program</td>
        <td><a href="https://www.xbox.com/Developers/id">Independent Developer Program for Xbox One</a></td>
    </tr>
    <tr>
        <td>ID@Xbox consumer site</td>
        <td><a href="https://www.idatxbox.com/">ID@Xbox</a></td>
    </tr>
</table>

### Game samples

There are many Windows game and app samples available to help you understand Windows gaming features, and get a quick start on game development. Samples are developed and published regularly, so don't forget to occasionally check back on sample portals to see what's new. You can also [watch](https://help.github.com/en/articles/watching-and-unwatching-repositories) GitHub repos to be notified of changes and additions.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Universal Windows Platform sample apps</td>
        <td><a href="https://github.com/Microsoft/Windows-universal-samples">Windows-universal-samples</a></td>
    </tr>
    <tr>
        <td>Direct3D 12 graphics samples</td>
        <td><a href="https://github.com/Microsoft/DirectX-Graphics-Samples">DirectX-Graphics-Samples</a></td>
    </tr>
    <tr>
        <td>Direct3D 11 graphics samples</td>
        <td><a href="https://github.com/walbourn/directx-sdk-samples">directx-sdk-samples</a></td>
    </tr>
    <tr>
        <td>Direct3D 11 first-person game sample</td>
        <td><a href="tutorial--create-your-first-uwp-directx-game.md">Create a simple UWP game with DirectX</a></td>
    </tr>
    <tr>
        <td>Direct2D custom image effects sample</td>
        <td><a href="https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/D2DCustomEffects">D2DCustomEffects</a></td>
    </tr>
    <tr>
        <td>Direct2D gradient mesh sample</td>
        <td><a href="https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/D2DGradientMesh">D2DGradientMesh</a></td>
    </tr>
    <tr>
        <td>Direct2D photo adjustment sample</td>
        <td><a href="https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/D2DPhotoAdjustment">D2DPhotoAdjustment</a></td>
    </tr>
    <tr>
        <td>Xbox Advanced Technology Group public samples</td>
        <td><a href="https://github.com/Microsoft/Xbox-ATG-Samples">Xbox-ATG-Samples</a></td>
    </tr>
    <tr>
        <td>Xbox Live samples</td>
        <td><a href="https://github.com/Microsoft/xbox-live-samples">xbox-live-samples</a></td>
    </tr>
    <tr>
        <td>Xbox One game samples (XGD)</td>
        <td><a href="https://developer.microsoft.com/games/xbox/partner/development-education-samples">Samples</a></td>
    </tr>
    <tr>
        <td>Windows game samples (MSDN Code Gallery)</td>
        <td><a href="/samples/browse/?term=games">Microsoft Store game samples</a></td>
    </tr>
    <tr>
        <td>JavaScript 2D game sample</td>
        <td><a href="/windows/uwp/get-started/">Create a UWP game in JavaScript</a></td>
    </tr>
    <tr>
        <td>JavaScript 3D game sample</td>
        <td><a href="/windows/uwp/get-started/">Creating a 3D JavaScript game using three.js</a></td>
    </tr>
    <tr>
        <td>MonoGame 2D UWP game sample</td>
        <td><a href="/windows/uwp/get-started/">Create a UWP game in MonoGame 2D</a></td>
    </tr>
</table>

### Developer forums

Developer forums are a great place to ask and answer game development questions, and connect with the game development community. Forums can also be fantastic resources for finding existing answers to difficult issues that developers have faced and solved in the past.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Publishing apps and games developer forums</td>
        <td><a href="https://social.msdn.microsoft.com/Forums/en/home?forum=windowsstore%2Cwpsubmit%2Caiaads%2Caiasdk%2Caiamgr">Publishing and ads-in-apps</a></td>
    </tr>
    <tr>
        <td>UWP apps developer forum</td>
        <td><a href="/answers/topics/uwp.html">Developing Universal Windows Platform apps</a></td>
    </tr>
    <tr>
        <td>Desktop applications developer forums</td>
        <td><a href="https://social.msdn.microsoft.com/Forums/en/home?forum=windowsgeneraldevelopmentissues">Windows desktop applications forums</a></td>
    </tr>
    <tr>
        <td>DirectX Microsoft Store games (archived forum posts)</td>
        <td><a href="https://social.msdn.microsoft.com/Forums/en/home?forum=wingameswithdirectx">Building Microsoft Store games with DirectX (archived)</a></td>
    </tr>
    <tr>
        <td>Windows 10 managed partner developer forums</td>
        <td><a href="https://forums.xboxlive.com/users/login.html">XBOX Developer Forums: Windows 10</a></td>
    </tr>
    <tr>
        <td>Xbox Live forum</td>
        <td><a href="https://social.msdn.microsoft.com/Forums/en/home?forum=xboxlivedev">Xbox Live development forum</a></td>
    </tr>
    <tr>
        <td>PlayFab forums</td>
        <td><a href="https://community.playfab.com/index.html">PlayFab forums</a></td>
    </tr>
</table>

### Developer blogs

Developer blogs are another great resource for the latest information about game development. You'll find posts about new features, implementation details, best practices, architecture background, and more.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Building apps for Windows (blog)</td>
        <td><a href="https://blogs.windows.com/buildingapps/">Building Apps for Windows</a></td>
    </tr>
    <tr>
        <td>Windows 10 (blog posts)</td>
        <td><a href="https://blogs.windows.com/blog/tag/windows-10/">Posts in Windows 10</a></td>
    </tr>
    <tr>
        <td>Visual Studio engineering team blog</td>
        <td><a href="https://devblogs.microsoft.com/visualstudio/">The Visual Studio Blog</a></td>
    </tr>
    <tr>
        <td>Visual Studio developer tools blogs</td>
        <td><a href="https://devblogs.microsoft.com/visualstudio/">Developer Tools Blogs</a></td>
    </tr>
    <tr>
        <td>Somasegar's developer tools blog</td>
        <td><a href="https://devblogs.microsoft.com/somasegar/">Somasegar's blog</a></td>
    </tr>
    <tr>
        <td>DirectX developer blog</td>
        <td><a href="https://devblogs.microsoft.com/directx/">DirectX Developer blog</a></td>
    </tr>
    <tr>
        <td>DirectX 12 introduction (blog post)</td>
        <td><a href="https://devblogs.microsoft.com/directx/directx-12/">DirectX 12</a></td>
    </tr>
    <tr>
        <td>Visual C++ tools team blog</td>
        <td><a href="https://devblogs.microsoft.com/cppblog/">Visual C++ team blog</a></td>
    </tr>
    <tr>
        <td>PIX team blog</td>
        <td><a href="https://devblogs.microsoft.com/pix/">Performance tuning and debugging for DirectX 12 games on Windows and Xbox</a></td>
    </tr>
    <tr>
        <td>Universal Windows App Deployment team blog</td>
        <td><a href="/windows/msix/">Build and deploy UWP apps team blog</a></td>
    </tr>
</table>

## Concept and planning

In the concept and planning stage, you decide what your game is going to be like and the technologies and tools you'll use to bring it to life.

### Overview of game development technologies

When you start developing a game for the UWP you have multiple options available for graphics, input, audio, networking, utilities, and libraries.

If you've already decided on all the technologies you'll be using in your game, then you're set. If not, the [Game technologies for UWP apps](game-development-platform-guide.md) guide is an excellent overview of many of the technologies available, and is highly recommended reading to help you understand the options and how they fit together.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Survey of UWP game technologies</td>
        <td><a href="game-development-platform-guide.md">Game technologies for UWP apps</a></td>
    </tr>
</table>

### Game planning

These are some high-level concept and planning topics to consider when planning for your game.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Make your game accessible</td>
        <td><a href="/windows/uwp/gaming/accessibility-for-games">Accessibility for games</a></td>
    </tr>
    <tr>
        <td>Build games using cloud</td>
        <td><a href="/windows/uwp/gaming/cloud-for-games">Cloud for games</a></td>
    </tr>
    <tr>
        <td>Monetize your game</td>
        <td><a href="/windows/uwp/gaming/monetization-for-games">Monetization for games</a></td>
    </tr>
</table>

### Choosing your graphics technology and programming language

There are several programming languages and graphics technologies available for use in Windows games. The path you take depends on the type of game you're developing, the experience and preferences of your development studio, and specific feature requirements of your game. Will you use C#, C++, or JavaScript? DirectX, XAML, or HTML5?

#### DirectX

Microsoft DirectX is the choice to make for the highest-performance 2D and 3D graphics and multimedia.

DirectX 12 is faster and more efficient than any previous version. Direct3D 12 enables richer scenes, more objects, more complex effects, and full utilization of modern GPU hardware on Windows PCs and Xbox One.

If you want to use the familiar graphics pipeline of Direct3D 11, you'll still benefit from the new rendering and optimization features added to Direct3D 11.3. And, if you're a tried-and-true desktop Windows API developer with roots in Win32, then you'll still have that option for your Windows game.

The extensive features and deep platform integration of DirectX provide the power and performance needed by the most demanding games.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>DirectX for UWP development</td>
        <td><a href="directx-programming.md">DirectX programming</a></td>
    </tr>
    <tr>
        <td>Tutorial: How to create a UWP DirectX game</td>
        <td><a href="tutorial--create-your-first-uwp-directx-game.md">Create a simple UWP game with DirectX</a></td>
    </tr>
    <tr>
        <td>DirectX overviews and reference</td>
        <td><a href="/windows/win32/directx">DirectX Graphics and Gaming</a></td>
    </tr>
    <tr>
        <td>Direct3D 12 programming guide and reference</td>
        <td><a href="/windows/win32/direct3d12/direct3d-12-graphics">Direct3D 12 Graphics</a></td>
    </tr>
    <tr>
        <td>Graphics and DirectX 12 development videos (YouTube channel)</td>
        <td><a href="https://www.youtube.com/channel/UCiaX2B8XiXR70jaN7NK-FpA">Microsoft DirectX 12 and Graphics Education</a></td>
    </tr>
</table>

#### XAML

XAML is an easy-to-use declarative UI language with convenient features like animations, storyboards, data binding, scalable vector-based graphics, dynamic resizing, and scene graphs. XAML works great for game UI, menus, sprites, and 2D graphics. To make UI layout easy, XAML is compatible with design and development tools like Expression Blend and Microsoft Visual Studio. XAML is commonly used with C#, but C++ is also a good choice if that's your preferred language or if your game has high CPU demands.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>XAML platform overview</td>
        <td><a href="/windows/uwp/xaml-platform/index">XAML platform</a></td>
    </tr>
    <tr>
        <td>XAML UI and controls</td>
        <td><a href="/windows/uwp/design/basics/">Controls, layouts, and text</a></td>
    </tr>
</table>

#### HTML 5

HyperText Markup Language (HTML) is a common UI markup language used for web pages, apps, and rich clients. Windows games can use HTML5 as a full-featured presentation layer with the familiar features of HTML, access to the Universal Windows Platform, and support for modern web features like AppCache, Web Workers, canvas, drag-and-drop, asynchronous programming, and SVG. Behind the scenes, HTML rendering takes advantage of the power of DirectX hardware acceleration, so you can still get the performance benefits of DirectX without writing any extra code. HTML5 is a good choice if you're proficient with web development, porting a web game, or you want to use language and graphics layers that can be easier to approach than the other choices. HTML5 is used with JavaScript, but can also call into components created with C# or C++/CX.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>HTML5 and Document Object Model information</td>
        <td><a href="https://developer.mozilla.org/docs/Web">HTML and DOM reference</a></td>
    </tr>
    <tr>
        <td>The HTML5 W3C Recommendation</td>
        <td><a href="https://www.w3.org/TR/html5/">HTML5</a></td>
    </tr>
</table>

#### Combining presentation technologies

The Microsoft DirectX Graphics Infrastructure (DXGI) provides interoperability and compatibility across multiple graphics technologies. For high-performance graphics, you can combine XAML and DirectX; using XAML for menus and other simple UI, and DirectX for rendering complex 2D and 3D scenes. DXGI also provides compatibility between Direct2D, Direct3D, DirectWrite, DirectCompute, and the Microsoft Media Foundation.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>DirectX Graphics Infrastructure programming guide and reference</td>
        <td><a href="/windows/win32/direct3ddxgi/dx-graphics-dxgi">DXGI</a></td>
    </tr>
    <tr>
        <td>Combining DirectX and XAML</td>
        <td><a href="directx-and-xaml-interop.md">DirectX and XAML interop</a></td>
    </tr>
</table>

#### C++

[C++/WinRT](../cpp-and-winrt-apis/index.md) is a high-performance, low overhead language that provides the powerful combination of speed, compatibility, and platform access. C++/WinRT makes it easy to use all of the great gaming features in Windows, including DirectX and Xbox Live. You can also reuse existing C++ code and libraries. C++/WinRT creates fast, native code that doesn't incur the overhead of garbage collection, so your game can have great performance and low power consumption, which leads to longer battery life. Use C++/WinRT with DirectX or XAML, or create a game that uses a combination of both.

#### C#

C# (pronounced "C sharp") is a modern, innovative language that is simple, powerful, type-safe, and object-oriented. C# enables rapid development while retaining the familiarity and expressiveness of C-style languages. Though easy to use, C# has numerous advanced language features such as polymorphism, delegates, lambdas, closures, iterator methods, covariance, and Language-Integrated Query (LINQ) expressions. C# is an excellent choice if you are targeting XAML, want to get a quick start developing your game, or have previous C# experience. C# is used primarily with XAML, so if you want to use DirectX, then choose C++ instead, or write part of your game as a C++ component that interacts with DirectX. Or, consider [Win2D](https://github.com/Microsoft/Win2D): an immediate mode Direct2D graphics libary for C# and C++.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>C# programming guide and reference</td>
        <td><a href="/dotnet/articles/csharp/csharp">C# language reference</a></td>
    </tr>
</table>

#### Use Windows Runtime components to combine languages

With the Universal Windows Platform, it's easy to combine components written in different languages. Create Windows Runtime components in C++, C#, or Visual Basic, and then call into them from JavaScript, C#, C++, or Visual Basic. This is a great way to program portions of your game in the language of your choice. Components also let you consume external libraries that are only available in a particular language, as well as use legacy code you've already written.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>How to create Windows Runtime components</td>
        <td><a href="/windows/uwp/winrt-components/create-a-windows-runtime-component-in-cppwinrt">Windows Runtime components with C++/WinRT</a></td>
    </tr>
</table>

### Which version of DirectX should your game use?

If you're choosing DirectX for your game, then you'll need to decide which version to use: Microsoft Direct3D 12 or Microsoft Direct3D 11.

DirectX 12 is faster and more efficient than any previous version. Direct3D 12 enables richer scenes, more objects, more complex effects, and full utilization of modern GPU hardware on Windows PCs and Xbox One. Since Direct3D 12 works at a very low level, it's able to give an expert graphics development team or an experienced DirectX 11 development team all the control they need to maximize graphics optimization.

Direct3D 11.3 is a low level graphics API that uses the familiar Direct3D programming model and handles for you more of the complexity involved in GPU rendering. It is also supported in Windows and Xbox One. If you have an existing engine written in Direct3D 11, and you're not quite ready to make the jump to Direct3D 12, you can use Direct3D 11 on 12 to achieve some performance improvements. Versions 11.3+ contain the new rendering and optimization features enabled also in Direct3D 12.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Choosing Direct3D 12 or Direct3D 11</td>
        <td><a href="/windows/win32/direct3d12/what-is-directx-12-">What is Direct3D 12?</a></td>
    </tr>
    <tr>
        <td>Overview of Direct3D 11</td>
        <td><a href="/windows/win32/direct3d11/atoc-dx-graphics-direct3d-11">Direct3D 11 Graphics</a></td>
    </tr>
    <tr>
        <td>Overview of Direct3D 11 on 12</td>
        <td><a href="/windows/win32/direct3d12/direct3d-11-on-12">Direct3D 11 on 12</a></td>
    </tr>
</table>

### Bridges, game engines, and middleware

Depending on the needs of your game, using bridges, game engines, or middleware can save development and testing time and resources. Here are some overview and resources for bridges, game engines, and middleware.

#### Azure PlayFab

Now part of the Microsoft family, Azure PlayFab is a complete back-end platform for live games and a powerful way for independent studios to get started. Boost revenue, engagement, and retention—while cutting costs—with game services, real-time analytics, and LiveOps.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>PlayFab</td>
        <td><a href="https://playfab.com/">Overview of tools and services</a></td>
    </tr>
    <tr>
        <td>Getting started</td>
        <td><a href="/gaming/playfab/what-is-playfab">What is PlayFab?</a></td>
    </tr>
    <tr>
        <td>Video tutorial series</td>
        <td><a href="https://www.youtube.com/watch?v=fGNpiqVi5xU&list=PLHCfyL7JpoPbLpA_oh_T5PKrfzPgCpPT5">Series of demo videos about PlayFab's core systems</a></td>
    </tr>
    <tr>
        <td>Recipes</td>
        <td><a href="/gaming/playfab/resources/playfab-recipes">Recipes</a></td>
    </tr>
    <tr>
        <td>GitHub repo</td>
        <td><a href="https://github.com/PlayFab">Get scripts and SDKs for various platforms including Android, iOS, Windows, Unity, and Unreal.</a></td>
    </tr>
    <tr>
        <td>API documentation</td>
        <td><a href="/gaming/playfab/api-references/">REST API overview</a></td>
    </tr>
    <tr>
        <td>Forums</td>
        <td><a href="https://community.playfab.com/index.html">PlayFab forums</a></td>
    </tr>
</table>

#### Unity

Unity offers a platform for creating beautiful and engaging 2D, 3D, VR, and AR games and apps. It enables you to realize your creative vision fast, and delivers your content to virtually any media or device.

Beginning with Unity 5.4, Unity supports Direct3D 12 development.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>The Unity game engine</td>
        <td><a href="https://unity.com/">Unity - Game Engine</a></td>
    </tr>
    <tr>
        <td>Get Unity</td>
        <td><a href="https://store.unity.com/">Get Unity</a></td>
    </tr>
    <tr>
        <td>Unity documentation for Windows</td>
        <td><a href="https://docs.unity3d.com/Manual/Windows.html">Unity Manual / Windows</a></td>
    </tr>
    <tr>
        <td>Add LiveOps using PlayFab</td>
        <td><a href="/gaming/playfab/sdks/unity3d/quickstart">Quickstart: PlayFab Client library for C# in Unity</a></td>
    </tr>
    <tr>
        <td>How to add interactivity to your game using Mixer Interactive</td>
        <td><a href="https://github.com/mixer/interactive-unity-plugin/wiki/Getting-started">Getting started guide</a></td>
    </tr>
    <tr>
        <td>Mixer SDK for Unity</td>
        <td><a href="https://www.assetstore.unity3d.com/en/#!/content/88585">Mixer Unity plugin</a></td>
    </tr>
    <tr>
        <td>Mixer SDK for Unity reference documentation</td>
        <td>API reference for Mixer Unity plugin</td>
    </tr>
    <tr>
        <td>Troubleshooting missing assembly references related to .NET APIs</td>
        <td><a href="/windows/uwp/gaming/missing-dot-net-apis-in-unity-and-uwp">Missing .NET APIs in Unity and UWP</a></td>
    </tr>
    <tr>
        <td>Publish your Unity game as a Universal Windows Platform app (video)</td>
        <td>How to publish your Unity game as a UWP app</td>
    </tr>
    <tr>
        <td>Use Unity to make Windows games and apps (video)</td>
        <td>Making Windows games and apps with Unity</td>
    </tr>
    <tr>
        <td>Unity game development using Visual Studio (video series)</td>
        <td><a href="https://www.youtube.com/playlist?list=PLReL099Y5nRfseAg0k1SJOlpqdcsDs8Em">Using Unity with Visual Studio 2015</a></td>
    </tr>
</table>

#### Havok

Havok's modular suite of tools and technologies help game creators reach new levels of interactivity and immersion. Havok enables highly realistic physics, interactive simulations, and stunning cinematics. Version 2015.1 or later officially supports UWP in Visual Studio 2015 on x86, 64-bit, and Arm.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Havok website</td>
        <td><a href="https://www.havok.com/">Havok</a></td>
    </tr>
</table>

#### MonoGame

MonoGame is an open source, cross-platform game development framework originally based on Microsoft's XNA Framework 4.0. Monogame currently supports Windows and Xbox, as well as Linux, macOS, iOS, Android, and several other platforms.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>MonoGame</td>
        <td><a href="https://www.monogame.net">MonoGame website</a></td>
    </tr>
    <tr>
        <td>MonoGame Documentation</td>
        <td><a href="https://www.monogame.net/documentation/">MonoGame Documentation (latest)</a></td>
    </tr>
    <tr>
        <td>Monogame Downloads</td>
        <td><a href="https://www.monogame.net/downloads/">Download releases, development builds, and source code</a> from the MonoGame website, or <a href="https://www.nuget.org/profiles/MonoGame">get the latest release via NuGet</a>.
    </tr>
</table>

#### Cocos2d

Cocos2d-x is a cross-platform open source game development engine and tools suite that supports building UWP games. Beginning with version 3, 3D features are being added as well.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Cocos2d-x</td>
        <td><a href="https://www.cocos2d-x.org/">What is Cocos2d-x?</a></td>
    </tr>
    <tr>
        <td>Cocos2d-x programmer's guide</td>
        <td><a href="https://www.cocos2d-x.org/programmersguide/">Cocos2d-x Programmers Guide</a></td>
    </tr>
    <tr>
        <td>Cocos2d-x on Windows 10 (blog post)</td>
        <td><a href="https://blogs.windows.com/buildingapps/2015/06/15/running-cocos2d-x-on-windows-10/">Running Cocos2d-x on Windows 10</a></td>
    </tr>
    <tr>
        <td>Add LiveOps using PlayFab</td>
        <td><a href="/gaming/playfab/sdks/cocos2d-x/quickstart">Cocos2D-x quickstart</a></td>
    </tr>
</table>

#### Unreal Engine

Unreal Engine is a complete suite of game development tools for all types of games and developers. For the most demanding console and PC games, Unreal Engine is used by game developers worldwide.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Unreal Engine overview</td>
        <td><a href="https://www.unrealengine.com/en-US/unreal">Unreal Engine</a></td>
    </tr>
</table>

#### BabylonJS

BabylonJS is a complete JavaScript framework for building 3D games with HTML5, WebGL, WebVR, and Web Audio.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>BabylonJS</td>
        <td><a href="https://www.babylonjs.com/">BabylonJS</a></td>
    </tr>
    <tr>
        <td>Building a cross-platform WebGL game with BabylonJS</td>
        <td><a href="https://www.smashingmagazine.com/2016/07/babylon-js-building-sponza-a-cross-platform-webgl-game/">Use BabylonJS to develop a cross-platform game</a></td>
    </tr>
</table>

### Porting your game

If you have an existing game, there are many resources and guides available to help you quickly bring your game to the UWP.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Porting a Windows 8 app to a Universal Windows Platform app</td>
        <td><a href="/windows/uwp/porting/w8x-to-uwp-root">Move from Windows Runtime 8.x to UWP</a></td>
    </tr>
    <tr>
        <td>Porting a Windows 8 app to a Universal Windows Platform app (video)</td>
        <td>Porting 8.1 Apps to Windows 10</td>
    </tr>
    <tr>
        <td>Porting an iOS app to a Universal Windows Platform app</td>
        <td><a href="/windows/uwp/porting/ios-to-uwp-root">Move from iOS to UWP</a></td>
    </tr>
    <tr>
        <td>Porting a Silverlight app to a Universal Windows Platform app</td>
        <td><a href="/windows/uwp/porting/wpsl-to-uwp-root">Move from Windows Phone Silverlight to UWP</a></td>
    </tr>
    <tr>
        <td>Porting from XAML or Silverlight to a Universal Windows Platform app (video)</td>
        <td>Porting an App from XAML or Silverlight to Windows 10</td>
    </tr>
    <tr>
        <td>Porting an Xbox game to a Universal Windows Platform app</td>
        <td><a href="/windows/uwp/xbox-apps/">UWP on Xbox One</a></td>
    </tr>
    <tr>
        <td>Porting from DirectX 9 to DirectX 11</td>
        <td><a href="porting-your-directx-9-game-to-windows-store.md">Port from DirectX 9 to Universal Windows Platform (UWP)</a></td>
    </tr>
    <tr>
        <td>Porting from Direct3D 11 to Direct3D 12</td>
        <td><a href="/windows/win32/direct3d12/porting-from-direct3d-11-to-direct3d-12">Porting from Direct3D 11 to Direct3D 12</a></td>
    </tr>
    <tr>
        <td>Porting from OpenGL ES to Direct3D 11</td>
        <td><a href="port-from-opengl-es-2-0-to-directx-11-1.md">Port from OpenGL ES 2.0 to Direct3D 11</a></td>
    </tr>
    <tr>
        <td>OpenGL ES to Direct3D 11 using ANGLE</td>
        <td><a href="https://github.com/microsoft/angle/wiki">ANGLE</a></td>
    </tr>
    <tr>
        <td>Classic Windows API equivalents in the UWP</td>
        <td><a href="/uwp/win32-and-com/win32-and-com-for-uwp-apps">Alternatives to Windows APIs in Universal Windows Platform (UWP) apps</a></td>
    </tr>
</table>

## Prototype and design

Now that you've decided the type of game you want to create, and the tools and graphics technology you'll use to build it, you're ready to get started with the design and prototype. At its core, your game is a Universal Windows Platform app, so that's where you'll begin.

### Introduction to the Universal Windows Platform (UWP)

Windows is the home of the Universal Windows Platform (UWP), which provides a common API platform across Windows devices. UWP evolves and expands the Windows Runtime (WinRT) model, and hones it into a cohesive, unified core. Games that target the UWP can call WinRT APIs that are common to all devices. Because the UWP provides guaranteed API layers, you can choose to create a single app package that will install across Windows devices. And if you want to, your game can still call APIs (including some classic Windows APIs from Win32 and .NET) that are specific to the devices your game runs on.

The following are excellent guides that discuss the Universal Windows Platform apps in detail, and are recommended reading to help you understand the platform.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Introduction to Universal Windows Platform apps</td>
        <td><a href="/windows/uwp/get-started/whats-a-uwp">What's a Universal Windows Platform app?</a></td>
    </tr>
</table>

### Getting started with UWP development

Getting set up and ready to develop a Universal Windows Platform app is quick and easy. The following guides take you through the process step-by-step.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Getting started with UWP development</td>
        <td><a href="https://developer.microsoft.com/windows/apps/getstarted">Get started with Windows apps</a></td>
    </tr>
    <tr>
        <td>Getting set up for UWP development</td>
        <td><a href="/windows/uwp/get-started/get-set-up">Get set up</a></td>
    </tr>
</table>

If you're an absolute beginner to UWP programming, and are considering using XAML in your game (see [Choosing your graphics technology and programming language](#choosing-your-graphics-technology-and-programming-language)), the Windows 10 development for absolute beginners video series is a good place to start.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Announcing the Windows 10 absolute beginners series using XAML (blog post)</td>
        <td><a href="https://blogs.windows.com/buildingapps/2015/09/30/windows-10-development-for-absolute-beginners/">Windows 10 development for absolute beginners</a></td>
    </tr>
</table>

### UWP development concepts

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Overview of Universal Windows Platform app development</td>
        <td><a href="https://developer.microsoft.com/windows/apps/develop">Develop Windows apps</a></td>
    </tr>
    <tr>
        <td>Overview of network programming in the UWP</td>
        <td><a href="/windows/uwp/networking/index">Networking and web services</a></td>
    </tr>
    <tr>
        <td>Using Windows.Web.HTTP and Windows.Networking.Sockets in games</td>
        <td><a href="work-with-networking-in-your-directx-game.md">Networking for games</a></td>
    </tr>
    <tr>
        <td>Asynchronous programming concepts in the UWP</td>
        <td><a href="/windows/uwp/threading-async/asynchronous-programming-universal-windows-platform-apps">Asynchronous programming</a></td>
    </tr>
</table>

### Windows Desktop APIs and UWP

These are some links to help you interoperate between desktop and UWP code.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Use existing C++ code for UWP game development</td>
        <td><a href="/cpp/porting/how-to-use-existing-cpp-code-in-a-universal-windows-platform-app">How to: Use existing C++ code in a UWP app</a></td>
    </tr>
    <tr>
        <td>Windows Runtime APIs for Win32 and COM APIs</td>
        <td><a href="/uwp/win32-and-com/win32-and-com-for-uwp-apps">Win32 and COM APIs for UWP apps</a></td>
    </tr>
    <tr>
        <td>Unsupported CRT functions in UWP</td>
        <td><a href="/cpp/cppcx/crt-functions-not-supported-in-universal-windows-platform-apps">CRT functions not supported in Universal Windows Platform apps</a></td>
    </tr>
    <tr>
        <td>Alternatives for Windows APIs</td>
        <td><a href="/uwp/win32-and-com/alternatives-to-windows-apis-uwp">Alternatives to Windows APIs in Universal Windows Platform (UWP) apps</a></td>
    </tr>
</table>

### Process lifetime management

Process lifetime management, or app lifecyle, describes the various activation states that a Universal Windows Platform app can transition through. Your game can be activated, suspended, resumed, or terminated, and can transition through those states in a variety of ways.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Handling app lifecyle transitions</td>
        <td><a href="/windows/uwp/launch-resume/app-lifecycle">App lifecycle</a></td>
    </tr>
    <tr>
        <td>Using Microsoft Visual Studio to trigger app transitions</td>
        <td><a href="/visualstudio/debugger/how-to-trigger-suspend-resume-and-background-events-for-windows-store-apps-in-visual-studio">How to trigger suspend, resume, and background events for UWP apps in Visual Studio</a></td>
    </tr>
</table>

### Designing game UX

The genesis of a great game is inspired design.

Games share some common user interface elements and design principles with apps; but games often have a unique look, feel, and design goal for their user experience. Games succeed when thoughtful design is applied to both aspects&mdash;when should your game use tested UX, and when should it diverge and innovate? The presentation technology that you choose for your game&mdash;DirectX, XAML, HTML5, or some combination of the three&mdash;will influence implementation details, but the design principles you apply are largely independent of that choice.

Separately from UX design, gameplay design such as level design, pacing, world design, and other aspects is an art form of its own&mdash;one that's up to you and your team, and not covered in this development guide.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>UWP design basics and guidelines</td>
        <td><a href="https://developer.microsoft.com/windows/apps/design">Designing UWP apps</a></td>
    </tr>
    <tr>
        <td>Designing for app lifecycle states</td>
        <td><a href="/windows/uwp/launch-resume/index">UX guidelines for launch, suspend, and resume</a></td>
    </tr>
    <tr>
        <td>Design your UWP app for Xbox One and television screens</td>
        <td><a href="/windows/uwp/design/devices/designing-for-tv">Designing for Xbox and TV</a></td>
    </tr>
</table>

#### Color guideline and palette

Following a consistent color guideline in your game improves aesthetics, aids navigation, and is a powerful tool to inform the player of menu and HUD functionality. Consistent coloring of game elements like warnings, damage, XP, and achievements can lead to cleaner UI, and reduce the need for explicit labels.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Color guide</td>
        <td><a href="/windows/apps/design/signature-experiences/color">Color in Windows 11</a></td>
    </tr>
</table>

#### Typography

The appropriate use of typography enhances many aspects of your game, including UI layout, navigation, readability, atmosphere, brand, and player immersion.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Typography guide</td>
        <td><a href="https://cmsresources.windowsphone.com/devcenter/common/resources/content/101_BestPractices_Typography.pdf">Best Practices: Typography</a></td>
    </tr>
</table>

#### UI map

A UI map is a layout of game navigation and menus expressed as a flowchart. The UI map helps all involved stakeholders understand the game's interface and navigation paths, and can expose potential roadblocks and dead ends early in the development cycle.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>UI map guide</td>
        <td><a href="https://cmsresources.windowsphone.com/devcenter/common/resources/content/101_BestPractices_UI_Map.pdf">Best Practices: UI Map</a></td>
    </tr>
</table>

### Game audio

Here are guides and references for implementing audio in games using XAudio2, XAPO, and Windows Sonic. XAudio2 is a low-level audio API that provides signal processing and mixing foundation for developing high-performance audio engines. The XAPO API allows the creation of cross-platform audio processing objects (XAPO) for use in XAudio2 on both Windows and Xbox. Windows Sonic audio support allows you to add Dolby Atmos for Home Theater, Dolby Atmos for Headphones, and Windows HRTF support to your game or streaming media application.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>XAudio2 APIs</td>
        <td><a href="/windows/win32/xaudio2/xaudio2-apis-portal">Programming guide and API reference for XAudio2</a></td>
    </tr>
    <tr>
        <td>Create cross-platform audio processing objects</td>
        <td><a href="/windows/win32/xaudio2/xapo-overview">XAPO Overview</a></td>
    </tr>
    <tr>
        <td>Intro to audio concepts</td>
        <td><a href="working-with-audio-in-your-directx-game.md">Audio for games</a></td>
    </tr>
    <tr>
        <td>Windows Sonic overview</td>
        <td><a href="/windows/win32/CoreAudio/spatial-sound">Spatial sound</a></td>
    </tr>
    <tr>
        <td>Windows Sonic spatial sound samples</td>
        <td><a href="https://github.com/Microsoft/Xbox-ATG-Samples/tree/master/UWPSamples/Audio">Xbox Advanced Technology Group audio samples</a></td>
    </tr>
</table>

#### Direct3D 12

Learn what's new and different in Direct3D 12 (compared to Direct3D 11), and how to start programming using Direct3D 12.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Direct3D 12 programming guide and reference</td>
        <td><a href="/windows/win32/direct3d12/direct3d-12-graphics">Direct3D 12 graphics</a></td>
    </tr>
    <tr>
        <td>Set up programming environment</td>
        <td><a href="/windows/win32/direct3d12/directx-12-programming-environment-set-up">Direct3D 12 programming environment setup</a></td>
    </tr>
    <tr>
        <td>How to create a basic component</td>
        <td><a href="/windows/win32/direct3d12/creating-a-basic-direct3d-12-component">Creating a basic Direct3D 12 component</a></td>
    </tr>
    <tr>
        <td>Changes in Direct3D 12</td>
        <td><a href="/windows/win32/direct3d12/important-changes-from-directx-11-to-directx-12">Important changes migrating from Direct3D 11 to Direct3D 12</a></td>
    </tr>
    <tr>
        <td>How to port from Direct3D 11 to Direct3D 12</td>
        <td><a href="/windows/win32/direct3d12/porting-from-direct3d-11-to-direct3d-12">Porting from Direct3D 11 to Direct3D 12</a></td>
    </tr>
    <tr>
        <td>Resource binding concepts (covering descriptor, descriptor table, descriptor heap, and root signature) </td>
        <td><a href="/windows/win32/direct3d12/resource-binding">Resource binding in Direct3D 12</a></td>
    </tr>
    <tr>
        <td>Managing memory</td>
        <td><a href="/windows/win32/direct3d12/memory-management">Memory management in Direct3D 12</a></td>
    </tr>
</table>

### DirectX development

Guides and references for DirectX game development.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>DirectX for UWP development</td>
        <td><a href="directx-programming.md">DirectX programming</a></td>
    </tr>
    <tr>
        <td>Tutorial: How to create a UWP DirectX game</td>
        <td><a href="tutorial--create-your-first-uwp-directx-game.md">Create a simple UWP game with DirectX</a></td>
    </tr>
    <tr>
        <td>DirectX interaction with the UWP app model</td>
        <td><a href="about-the-uwp-user-interface-and-directx.md">The app object and DirectX</a></td>
    </tr>
    <tr>
        <td>DirectX overviews and reference</td>
        <td><a href="/windows/win32/directx">DirectX Graphics and Gaming</a></td>
    </tr>
    <tr>
        <td>DirectX 12 fundamentals (video)</td>
        <td>Better Power, Better Performance: Your Game on DirectX 12</td>
    </tr>
</table>

#### DirectX Tool Kit and libraries

The DirectX Tool Kit, DirectX texture processing library, DirectXMesh geometry processing library, UVAtlas library, and DirectXMath library provide texture, mesh, sprite, and other utility functionality and helper classes for DirectX development. These libraries can help save you development time and effort.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Get DirectX Tool Kit for DirectX 12</td>
        <td><a href="https://github.com/Microsoft/DirectXTK12">DirectXTK 12</a></td>
    </tr>
    <tr>
        <td>Get DirectX Tool Kit for DirectX 11</td>
        <td><a href="https://github.com/Microsoft/DirectXTK">DirectXTK</a></td>
    </tr>
    <tr>
        <td>Get DirectX texture processing library</td>
        <td><a href="https://github.com/Microsoft/DirectXTex">DirectXTex</a></td>
    </tr>
    <tr>
        <td>Get DirectXMesh geometry processing library</td>
        <td><a href="https://github.com/Microsoft/DirectXMesh">DirectXMesh</a></td>
    </tr>
    <tr>
        <td>Get UVAtlas for creating and packing isochart texture atlas</td>
        <td><a href="https://github.com/Microsoft/UVAtlas">UVAtlas</a></td>
    </tr>
    <tr>
        <td>Get the DirectXMath library</td>
        <td><a href="https://github.com/Microsoft/DirectXMath">DirectXMath</a></td>
    </tr>
    <tr>
        <td>Direct3D 12 support in the DirectXTK (GitHub issue)</td>
        <td><a href="https://github.com/Microsoft/DirectXTK/issues/2">Support for DirectX 12</a></td>
    </tr>
</table>

#### DirectX resources from partners

Here's some additional DirectX documentation created by external partners.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Nvidia: DX12 Do's and Don'ts (blog post) </td>
        <td><a href="https://developer.nvidia.com/dx12-dos-and-donts-updated">DX12 Do's and Don'ts, Updated!</a></td>
    </tr>
    <tr>
        <td>Intel: Parallel Processing with DirectX 3D 12</td>
        <td><a href="https://www.intel.com/content/www/us/en/developer/articles/technical/parallel-processing-with-directx-3d-12.html?wapkw=Efficient%20rendering%20with%20DirectX%2012">Parallel Processing with DirectX 3D 12</a></td>
    </tr>
    <tr>
        <td>Intel: How to implement an explicit multi-adapter application using DirectX 12</td>
        <td><a href="https://www.intel.com/content/www/us/en/developer/articles/technical/multi-adapter-support-in-directx-12.html">Multi-Adapter Support in DirectX 12</a></td>
    </tr>
    <tr>
        <td>Intel: Collaborative white paper by Intel, Suzhou Snail, and Microsoft</td>
        <td><a href="https://software.intel.com/articles/tutorial-migrating-your-apps-to-directx-12-part-1">Tutorial: Migrating Your Apps to DirectX 12 – Part 1</a></td>
    </tr>
</table>

## Production

At this stage, your studio is fully engaged and moving into the production cycle, with work distributed throughout your team. You're polishing, refactoring, and extending the prototype to craft it into a full game.

### Notifications and live tiles

A tile is your game's representation on the Start Menu. Tiles and notifications can drive player interest even when they aren't currently playing your game.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Developing tiles and badges</td>
        <td><a href="/windows/uwp/controls-and-patterns/tiles-badges-notifications">Tiles, badges, and notifications</a></td>
    </tr>
    <tr>
        <td>Sample illustrating live tiles and notifications</td>
        <td><a href="https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/Notifications">Notifications sample</a></td>
    </tr>
    <tr>
        <td>Adaptive tile templates (blog post)</td>
        <td><a href="/archive/blogs/tiles_and_toasts/adaptive-tile-templates-schema-and-documentation">Adaptive Tile Templates - Schema and Documentation</a></td>
    </tr>
    <tr>
        <td>Designing tiles and badges</td>
        <td><a href="/windows/uwp/controls-and-patterns/tiles-and-notifications-creating-tiles">Guidelines for tiles and badges</a></td>
    </tr>
    <tr>
        <td>Windows 10 app for interactively developing live tile templates</td>
        <td><a href="https://www.microsoft.com/store/apps/9nblggh5xsl1">Notifications Visualizer</a></td>
    </tr>
    <tr>
        <td>UWP Tile Generator extension for Visual Studio</td>
        <td><a href="https://marketplace.visualstudio.com/items?itemName=shenchauhan.UWPTileGenerator">Tool for creating all required tiles using single image</a></td>
    </tr>
    <tr>
        <td>UWP Tile Generator extension for Visual Studio (blog post)</td>
        <td><a href="https://blogs.windows.com/buildingapps/2016/02/15/uwp-tile-generator-extension-for-visual-studio/">Tips on using the UWP Tile Generator tool</a></td>
    </tr>
</table>

### Enable in-app product (add-on) purchases

An add-on (in-app product) is a supplementary item that players can purchase in-game. Add-ons can be game levels, items, or anything else that your players might enjoy. Used appropriately, add-ons can provide revenue while improving the game experience. You define and publish your game's add-ons through Partner Center, and enable in-app purchases in your game's code.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Durable add-ons</td>
        <td><a href="/windows/uwp/monetize/enable-in-app-product-purchases">Enable in-app product purchases</a></td>
    </tr>
    <tr>
        <td>Consumable add-ons</td>
        <td><a href="/windows/uwp/monetize/enable-consumable-in-app-product-purchases">Enable consumable in-app product purchases</a></td>
    </tr>
    <tr>
        <td>Add-on details and submission</td>
        <td><a href="/windows/apps/publish/publish-your-app/create-app-submission?pivots=store-installer-add-on">Add-on submissions</a></td>
    </tr>
    <tr>
        <td>Monitor add-on sales and demographics for your game</td>
        <td><a href="/windows/apps/publish/add-on-acquisitions-report">Add-on acquisitions report</a></td>
    </tr>
</table>

### Debugging, performance optimization, and monitoring

To optimize performance, you can take advantage of Game Mode in Windows to provide your gamers with the best possible gaming experience by fully utilizing the capacity of their current hardware.

The Windows Performance Toolkit (WPT) consists of performance monitoring tools that produce in-depth performance profiles of Windows operating systems and applications. This is especially useful for monitoring memory usage, and improving game performance. The Windows Performance Toolkit is included in the Windows SDK and Windows ADK. This toolkit consists of two independent tools: Windows Performance Recorder (WPR) and Windows Performance Analyzer (WPA). ProcDump, which is part of [Windows Sysinternals](/sysinternals/), is a command-line utility that monitors CPU spikes and generates dump files during game crashes.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Performance test your code</td>
        <td><a href="https://azure.microsoft.com/services/devops/test-plans/">Azure Test Plans</a></td>
    </tr>
    <tr>
        <td>Get Xbox console type using Gaming Device Information</td>
        <td><a href="/previous-versions/windows/desktop/gamingdvcinfo/gaming-device-information-portal">Gaming Device Information</a></td>
    </tr>
    <tr>
        <td>Improve performance by getting exclusive or priority access to hardware resources using Game Mode APIs</td>
        <td><a href="/previous-versions/windows/desktop/gamemode/game-mode-portal">Game Mode</a></td>
    </tr>
    <tr>
        <td>Get the Windows Performance Toolkit (WPT)</td>
        <td><a href="/windows-hardware/test/wpt/">Windows Performance Toolkit</a></td>
    </tr>
    <tr>
        <td>Get Windows Performance Toolkit (WPT) from Windows ADK</td>
        <td><a href="/windows-hardware/get-started/adk-install">Windows ADK</a></td>
    </tr>
    <tr>
        <td>Get ProcDump</td>
        <td><a href="/sysinternals/downloads/procdump">ProcDump</a></td>
    </tr>
</table>

### Advanced DirectX techniques and concepts

Some portions of DirectX development can be nuanced and complex. When you get to the point in production where you need to dig down into the details of your DirectX engine, or debug difficult performance problems, the resources and information in this section can help.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Performance tuning and debugging tool for DirectX 12 on Windows</td>
        <td><a href="https://devblogs.microsoft.com/pix/introducing-pix-on-windows-beta/">Introducing PIX on Windows</a></td>
    </tr>
    <tr>
        <td>Direct3D 12 programming guide</td>
        <td><a href="/windows/win32/direct3d12/direct3d-12-graphics">Direct3D 12 graphics</a></td>
    </tr>
    <tr>
        <td>Combining DirectX and XAML</td>
        <td><a href="directx-and-xaml-interop.md">DirectX and XAML interop</a></td>
    </tr>
</table>

### High dynamic range (HDR) content development

Build game content that uses the full color capabilities of HDR.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Learn how to render HDR content and detect whether the current display supports it</td>
        <td><a href="https://github.com/Microsoft/DirectX-Graphics-Samples/tree/master/Samples/UWP/D3D12HDR">Direct3D 12 HDR sample</a></td>
    </tr>
    <tr>
        <td>Create and configure an advanced color using DirectX</td>
        <td><a href="https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/D2DAdvancedColorImages">Direct2D advanced color image rendering sample</a></td>
    </tr>
</table>

### Globalization and localization

Develop World-ready games for the Windows platform, and learn about the international features built into Microsoft's top products.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Preparing your game for the global market</td>
        <td><a href="/windows/uwp/globalizing/globalizing-portal">Globalization and localization</a></td>
    </tr>
</table>

## Submitting and publishing your game

The following guides and information help make the publishing and submission process go as smoothly as possible.

### Publishing

You'll use [Partner Center](https://partner.microsoft.com/dashboard) to publish and manage your game packages.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Partner Center app publishing</td>
        <td><a href="https://developer.microsoft.com/store/publish-apps">Publish Windows apps</a></td>
    </tr>
    <tr>
        <td>Use Azure Active Directory (AAD) to add users to your Partner Center account</td>
        <td><a href="/windows/apps/publish/create-customer-groups">Create customer groups</a></td>
    </tr>
    <tr>
        <td>Rating your game (blog post)</td>
        <td><a href="https://blogs.windows.com/buildingapps/2016/01/06/now-available-single-age-rating-system-to-simplify-app-submissions/">Now Available: Single age rating system to simplify app submissions</a></td>
    </tr>
</table>

#### Packaging and uploading

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Divide and group content to enable streaming install</td>
        <td><a href="/windows/msix/package/streaming-install">UWP App Streaming install</a></td>
    </tr>
    <tr>
        <td>Create optional packages like DLC game content</td>
        <td><a href="/windows/msix/package/optional-packages">Optional packages and related set authoring</a></td>
    </tr>
    <tr>
        <td>Package your UWP game</td>
        <td><a href="../packaging/index.md">Packaging apps</a></td>
    </tr>
    <tr>
        <td>Package your UWP DirectX game</td>
        <td><a href="package-your-windows-store-directx-game.md">Package your UWP DirectX game</a></td>
    </tr>
    <tr>
        <td>Packaging your game as a 3rd party developer (blog post)</td>
        <td><a href="https://blogs.windows.com/buildingapps/2015/12/15/building-an-app-for-a-3rd-party-how-to-package-their-store-app/">Create uploadable packages without publisher's store account access</a></td>
    </tr>
    <tr>
        <td>Creating app packages and app package bundles using MakeAppx</td>
        <td><a href="/windows/msix/package/create-app-package-with-makeappx-tool">Create packages using app packager tool MakeAppx.exe</a></td>
    </tr>
    <tr>
        <td>Signing your files digitally using SignTool</td>
        <td><a href="/windows/win32/SecCrypto/signtool">Sign files and verify signatures in files using SignTool</a></td>
    </tr>
    <tr>
        <td>Uploading and versioning your game</td>
        <td><a href="/windows/apps/publish/publish-your-app/upload-app-packages">Upload app packages</a></td>
    </tr>
</table>

### Policies and certification

Don't let certification issues delay your game's release. Here are policies and common certification issues to be aware of.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Microsoft Store App Developer Agreement</td>
        <td><a href="/legal/windows/agreements/app-developer-agreement">App Developer Agreement</a></td>
    </tr>
    <tr>
        <td>Policies for publishing apps in the Microsoft Store</td>
        <td><a href="/legal/windows/agreements/store-policies">Microsoft Store Policies</a></td>
    </tr>
    <tr>
        <td>How to avoid some common app certification issues</td>
        <td><a href="/windows/apps/publish/publish-your-app/avoid-common-certification-failures">Avoid common certification failures</a></td>
    </tr>
</table>

### Store manifest (StoreManifest.xml)

The store manifest (`StoreManifest.xml`) is an optional configuration file that you can include in your app package. The store manifest provides additional features that are not part of the `AppxManifest.xml` file. For example, you can use the store manifest to block installation of your game if a target device doesn't have the specified minimum DirectX feature level or the specified minimum system memory.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Store manifest schema</td>
        <td><a href="/uwp/schemas/storemanifest/storemanifestschema2015/schema-root">StoreManifest schema (Windows 10)</a></td>
    </tr>
</table>

## Game lifecycle management

After you've finished development and shipped your game, it's not *game over*. You might be done with development on version 1, but your game's journey in the marketplace has only just begun. You'll want to monitor usage and error reporting, respond to user feedback, and publish updates to your game.

### Partner Center analytics and promotion

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Partner Center analytics</td>
        <td><a href="/windows/apps/publish/analytics">Analyze app performance</a></td>
    </tr>
    <tr>
        <td>Learn how your customers are engaging with the Xbox features in your game</td>
        <td><a href="/windows/apps/publish/xbox-analytics-report">Xbox analytics report</a></td>
    </tr>
    <tr>
        <td>Responding to customer reviews</td>
        <td><a href="/windows/apps/publish/respond-to-customer-reviews">Respond to customer reviews</a></td>
    </tr>
    <tr>
        <td>Ways to promote your game</td>
        <td><a href="/windows/apps/publish/attract-customers-and-promote-your-apps">Attract customers and promote your apps</a></td>
    </tr>
</table>

### Visual Studio Application Insights

Visual Studio Application Insights provides performance, telemetry, and usage analytics for your published game. Application Insights helps you to detect and solve issues after your game is released, continuously monitor and improve usage, and understand how players are continuing to interact with your game. Application Insights works by adding an SDK into your app, which sends telemetry to the [Azure portal](https://portal.azure.com/).

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Application performance and usage analytics</td>
        <td><a href="/azure/azure-monitor/app/app-insights-overview">Application Insights overview</a></td>
    </tr>
    <tr>
        <td>Azure Monitor</td>
        <td><a href="/azure/azure-monitor/overview">Azure Monitor overview</a></td>
    </tr>
</table>

### Third party solutions for analytics and promotion

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Understand player behavior using GameAnalytics</td>
        <td><a href="https://gameanalytics.com/">GameAnalytics</a></td>
    </tr>
    <tr>
        <td>Connect your UWP game to Google Analytics</td>
        <td><a href="https://github.com/dotnet/windows-sdk-for-google-analytics">Get Windows SDK for Google Analytics</a></td>
    </tr>
    <tr>
        <td>Use Facebook App Installs Ads to promote your game to Facebook users</td>
        <td><a href="https://github.com/Microsoft/winsdkfb">Get Windows SDK for Facebook</a></td>
    </tr>
    <tr>
        <td>Use Vungle to add video ads into your games</td>
        <td><a href="https://publisher.vungle.com/sdk/">Get Windows SDK for Vungle</a></td>
    </tr>
</table>

### Creating and managing content updates

To update your published game, you submit a new app package with a higher version number. After the package makes its way through submission and certification, it will automatically be available to customers as an update.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Updating and versioning your game</td>
        <td><a href="/windows/apps/publish/publish-your-app/package-version-numbering">Package version numbering</a></td>
    </tr>
    <tr>
        <td>Game package management guidance</td>
        <td><a href="/windows/apps/publish/publish-your-app/app-package-management">Guidance for app package management</a></td>
    </tr>
</table>

## Adding Xbox Live to your game

Xbox Live is a premier gaming network that connects millions of gamers across the world. Developers gain access to Xbox Live features that can organically grow their game's audience, including Xbox Live presence, Leaderboards, Cloud Saves, Game Hubs, Clubs, Party Chat, Game DVR, and more.

> [!NOTE]
> If you would like to develop Xbox Live enabled titles, there are several options are available to you. For info about the various programs, see [Choosing an Xbox Live developer program](/gaming/gdk/_content/gc/live/get-started/live-getstarted-nav).

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Xbox Live overview</td>
        <td><a href="/gaming/xbox-live/">Xbox Live documentation</a></td>
    </tr>
</table>

### For developers in the Xbox Live Creators Program

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Overview</td>
        <td><a href="/gaming/xbox-live/get-started/join-dev-program/live-join-creators-program">Joining the Creators Program</a></td>
    </tr>
</table>

### For managed partners and developers in the ID@Xbox program

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Overview</td>
        <td><a href="/gaming/xbox-live/get-started/setup-partner-center/legacy/live-get-started-xbl-partner">Getting started with Xbox Live, for Managed Partners</a></td>
    </tr>
    <tr>
        <td>Samples</td>
        <td><a href="/gaming/xbox/samples">Xbox game development samples</a></td>
    </tr>
</table>

## Additional resources

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Videos from major conferences such as GDC and //build</td>
        <td><a href="/windows/uwp/gaming/game-development-videos">Game development videos</a></td>
    </tr>
</table>
