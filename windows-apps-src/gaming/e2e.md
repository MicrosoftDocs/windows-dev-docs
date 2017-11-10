---
author: joannaleecy
title: Windows 10 game development guide
description: An end-to-end guide to resources and information for developing Universal Windows Platform (UWP) games.
ms.assetid: 6061F498-96A8-44EF-9711-68AE5A1218C9
ms.author: joanlee
ms.date: 11/03/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, games, game development
localizationpriority: medium
---

# Windows 10 game development guide


Welcome to the Windows 10 game development guide!

This guide provides an end-to-end collection of the resources and information you'll need to develop a Universal Windows Platform (UWP) game. An English (US) version of this guide is available in [PDF](http://download.microsoft.com/download/9/C/9/9C9D344F-611F-412E-BB01-259E5C76B17F/Windev_Game_Dev_Guide_Oct_2017.pdf) format.

## Introduction to game development for the Universal Windows Platform (UWP)


When you create a Windows 10 game, you have the opportunity to reach millions of players worldwide across phone, PC, and Xbox One. With Xbox on Windows, Xbox Live, cross-device multiplayer, an amazing gaming community, and powerful new features like the Universal Windows Platform (UWP) and DirectX 12, Windows 10 games thrill players of all ages and genres. The new Universal Windows Platform (UWP) delivers compatibility for your game across Windows 10 devices with a common API for phone, PC, and Xbox One, along with tools and options to tailor your game to each device experience.

This guide provides an end-to-end collection of information and resources that will help you as you develop your game. The sections are organized according to the stages of game development, so you'll know where to look for information when you need it.

To get started, the [Game development resources](#game-development-resources) section provides a high-level survey of documentation, programs, and other resources that are helpful when creating a game.

This guide will be updated as additional Windows 10 game development resources and material become available.  

## Game development resources

From documentation to developer programs, forums, blogs, and samples, there are many resources available to help you on your game development journey. Here's a roundup of resources to know about as you begin developing your Windows 10 game.

> [!Note]
> Some features are managed through various programs. This guide covers a broad range of resources, so you may find that some resources are inaccessible depending on the program you are in or your specific development role. Examples are links that resolve to developer.xboxlive.com, forums.xboxlive.com, xdi.xboxlive.com, or the Game Developer Network (GDN). For information about partnering with Microsoft, see [Developer Programs](#developer-programs).


### Game development documentation

Throughout this guide, you'll find deep links to relevant documentation—organized by task, technology, and stage of game development. To give you a broad view of what's available, here are the main documentation portals for Windows 10 game development.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Windows Dev Center main portal</td>
        <td>[Windows Dev Center](https://dev.windows.com)</td>
    </tr>
    <tr>
        <td>Developing Windows apps</td>
        <td>[Develop Windows apps](https://dev.windows.com/develop)</td>
    </tr>
    <tr>
        <td>Universal Windows Platform app development</td>
        <td>[How-to guides for Windows 10 apps](https://msdn.microsoft.com/library/windows/apps/mt244352)</td>
    </tr>
    <tr>
        <td>How-to guides for UWP games</td>
        <td>[Games and DirectX](index.md) </td>
    </tr>
    <tr>
        <td>DirectX reference and overviews</td>
        <td>[DirectX Graphics and Gaming](https://msdn.microsoft.com/library/windows/desktop/ee663274)</td>
    </tr>
    <tr>
        <td>Azure for gaming</td>
        <td>[Build and scale your games using Azure](https://azure.microsoft.com/solutions/gaming/)</td>
    </tr>
    <tr>
        <td>UWP on Xbox One</td>
        <td>[Building UWP apps on Xbox One](https://msdn.microsoft.com/windows/uwp/xbox-apps/index)</td>
    </tr>
    <tr>
        <td>UWP on HoloLens</td>
        <td>[Building UWP apps on HoloLens](https://developer.microsoft.com/windows/mixed-reality/development_overview)</td>
    </tr>
    <tr>
        <td>Xbox Live documentation</td>
        <td>[Xbox Live developer guide](../xbox-live/index.md)</td>
    </tr>
    <tr>
        <td>Xbox One developer documentation (GDN)</td>
        <td>[Xbox One XDK documentation](https://developer.xboxlive.com/en-us/platform/development/documentation/Pages/home.aspx)</td>
    </tr>
    <tr>
        <td>Xbox One developer whitepapers (GDN)</td>
        <td>[White Papers](https://developer.xboxlive.com/en-us/platform/development/education/Pages/WhitePapers.aspx)</td>
    </tr>
    <tr>
        <td>Mixer Interactive documentation</td>
        <td>[Add interactivity to your game](https://dev.mixer.com/reference/interactive/index.html)</td>
    </tr>        
</table>

### Windows Dev Center

Registering a developer account on the Windows Dev Center is the first step towards publishing your Windows game. A developer account lets you reserve your game's name and submit free or paid games to the Microsoft Store for all Windows devices. Use your developer account to manage your game and in-game products, get detailed analytics, and enable services that create great experiences for your players around the world. 

Microsoft also offers several developer programs to help you develop and publish Windows games. We recommend seeing if any are right for you before registering for a Dev Center account. For more info, go to [Developer programs](#developer-programs)

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Register a developer account</td>
        <td>[Ready to sign up?](https://msdn.microsoft.com/library/windows/apps/bg124287)</td>
    </tr> 
</table>

### Developer programs

Microsoft offers several developer programs to help you develop and publish Windows games. Consider joining a developer program if you want to develop games for Xbox One and integrate Xbox Live features in your game. To publish a game in the Microsoft Store, you'll also need to create a developer account on Windows Dev Center.

#### Xbox Live Creators Program

The Xbox Live Creators Program allows anyone to integrate Xbox Live into their title and publish to Xbox One and Windows 10. There is a simplified certification process and no concept approval is required outside of the standard [Microsoft Store Policies](https://msdn.microsoft.com/library/windows/apps/dn764944.aspx).

You can deploy, design, and publish your game in the Creators Program without a dedicated dev kit, using only retail hardware. To get started, download the [Dev Mode Activation app](https://docs.microsoft.com/windows/uwp/xbox-apps/devkit-activation) on your Xbox One.

If you want access to even more Xbox Live capabilities, dedicated marketing and development support, and the chance to be featured in the main Xbox One store, apply to the [ID@Xbox](http://www.xbox.com/Developers/id) program.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Xbox Live Creators Program</td>
        <td>[Learn more about the Xbox Live Creators Program](https://developer.microsoft.com/games/xbox/xboxlive/creator)</td>
    </tr>
</table>

#### ID@Xbox

The ID@Xbox program helps qualified game developers self-publish on Windows and Xbox One. If you want to develop for Xbox One, or add Xbox Live features like Gamerscore, achievements, and leaderboards to your Windows 10 game, sign up with ID@Xbox. Become an ID@Xbox developer to get the tools and support you need to unleash your creativity and maximize your success. We recommend that you apply to ID@Xbox first before registering for a developer account on Windows Dev Center.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>ID@Xbox developer program</td>
        <td>[Independent Developer Program for Xbox One](http://go.microsoft.com/fwlink/p/?LinkID=526271)</td>
    </tr>
    <tr>
        <td>ID@Xbox consumer site</td>
        <td>[ID@Xbox](http://www.idatxbox.com/)</td>
    </tr>
</table>

#### Xbox tools and middleware

The Xbox Tools and Middleware Program licenses Xbox development kits to professional developers of game tools and middleware. Developers accepted into the program can share and distribute their Xbox XDK technologies to other licensed Xbox developers.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Contact the tools and middleware program</td>
        <td><xboxtlsm@microsoft.com></td>
    </tr>
</table>


### Game samples

There are many Windows 10 game and app samples available to help you understand Windows 10 gaming features and get a quick start on game development. More samples are developed and published regularly, so don't forget to occasionally check back at sample portals to see what's new. You can also [watch](https://help.github.com/articles/watching-repositories/) GitHub repos to be notified of changes and additions.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Universal Windows Platform app samples</td>
        <td>[Windows-universal-samples](https://github.com/Microsoft/Windows-universal-samples)</td>
    </tr>
    <tr>
        <td>Direct3D 12 graphics samples</td>
        <td>[DirectX-Graphics-Samples](https://github.com/Microsoft/DirectX-Graphics-Samples)</td>
    </tr>
	<tr>
		<td>Direct3D 11 graphics samples</td>
		<td>[directx-sdk-samples](https://github.com/walbourn/directx-sdk-samples)</td>
	</tr>
    <tr>
        <td>Direct3D 11 first-person game sample</td>
        <td>[Create a simple UWP game with DirectX](tutorial--create-your-first-metro-style-directx-game.md)</td>
    </tr>
    <tr>
        <td>Direct2D custom image effects sample</td>
        <td>[D2DCustomEffects](http://go.microsoft.com/fwlink/p/?LinkId=620531)</td>
    </tr>
    <tr>
        <td>Direct2D gradient mesh sample</td>
        <td>[D2DGradientMesh](http://go.microsoft.com/fwlink/p/?LinkId=620532)</td>
    </tr>
    <tr>
        <td>Direct2D photo adjustment sample</td>
        <td>[D2DPhotoAdjustment](http://go.microsoft.com/fwlink/p/?LinkId=620533)</td>
    </tr>
    <tr>
		<td>Xbox Advanced Technology Group public samples</td>
		<td>[Xbox-ATG-Samples](https://github.com/Microsoft/Xbox-ATG-Samples)</td>
	</tr>
    <tr>
        <td>Xbox Live samples</td>
        <td>[xbox-live-samples](https://github.com/Microsoft/xbox-live-samples)</td>
    </tr>
    <tr>
        <td>Xbox One game samples (GDN)</td>
        <td>[Samples](https://developer.xboxlive.com/en-us/platform/development/education/Pages/Samples.aspx)</td>
    </tr>
    <tr>
        <td>Windows game samples (MSDN Code Gallery)</td>
        <td>[Microsoft Store game samples](https://code.msdn.microsoft.com/windowsapps/site/search?f%5B0%5D.Type=SearchText&f%5B0%5D.Value=game&f%5B1%5D.Type=Contributors&f%5B1%5D.Value=Microsoft&f%5B1%5D.Text=Microsoft)</td>
    </tr>
    <tr>
        <td>JavaScript and HTML5 game sample</td>
        <td>[JavaScript and HTML5 touch game sample](https://code.msdn.microsoft.com/windowsapps/JavaScript-and-HTML5-touch-d96f6031)</td>
    </tr>      
</table>


### Developer forums

Developer forums are a great place to ask and answer game development questions and connect with the game development community. Forums can also be fantastic resources for finding existing answers to difficult issues that developers have faced and solved in the past.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Windows apps developer forums</td>
        <td>[Windows store and apps forums](https://social.msdn.microsoft.com/Forums/en-us/home?category=windowsapps)</td>
    </tr>
    <tr>
        <td>UWP apps developer forum</td>
        <td>[Developing Universal Windows Platform apps](https://social.msdn.microsoft.com/Forums/en-us/home?forum=wpdevelop)</td>
    </tr>

    <tr>
        <td>Desktop applications developer forums</td>
        <td>[Windows desktop applications forums](https://social.msdn.microsoft.com/Forums/en-us/home?category=windowsdesktopdev)</td>
    </tr>
    <tr>
        <td>DirectX Microsoft Store games (archived forum posts)</td>
        <td>[Building Microsoft Store games with DirectX (archived)](https://social.msdn.microsoft.com/Forums/vstudio/home?forum=wingameswithdirectx)</td>
    </tr>
    <tr>
        <td>Windows 10 managed partner developer forums</td>
        <td>[XBOX Developer Forums: Windows 10](http://aka.ms/win10devforums)</td>
    </tr>
    <tr>
        <td>DirectX forums</td>
        <td>[DirectX 12 forum](http://forums.directxtech.com/index.php)</td>
    </tr>
    <tr>
        <td>Azure platform forums</td>
        <td>[Azure forum](https://social.msdn.microsoft.com/Forums/en-us/home?category=windowsazureplatform)</td>
    </tr>
    <tr>
        <td>Xbox Live forum</td>
        <td>[Xbox Live development forum](https://social.msdn.microsoft.com/Forums/en-US/home?forum=xboxlivedev)</td>
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
        <td>Building apps for Windows blog</td>
        <td>[Building Apps for Windows](http://blogs.windows.com/buildingapps/)</td>
    </tr>
    <tr>
        <td>Windows 10 (blog posts)</td>
        <td>[Posts in Windows 10](http://blogs.windows.com/blog/tag/windows-10/)</td>
    </tr>
    <tr>
        <td>Visual Studio engineering team blog</td>
        <td>[The Visual Studio Blog](http://blogs.msdn.com/b/visualstudio/)</td>
    </tr>
    <tr>
        <td>Visual Studio developer tools blogs</td>
        <td>[Developer Tools Blogs](http://blogs.msdn.com/b/developer-tools/)</td>
    </tr>
    <tr>
        <td>Somasegar's developer tools blog</td>
        <td>[Somasegar’s blog](http://blogs.msdn.com/b/somasegar/)</td>
    </tr>
    <tr>
        <td>DirectX developer blog</td>
        <td>[DirectX Developer blog](http://blogs.msdn.com/b/directx)</td>
    </tr>
    <tr>
        <td>DirectX 12 introduction (blog post)</td>
        <td>[DirectX 12](http://blogs.msdn.com/b/directx/archive/2014/03/20/directx-12.aspx)</td>
    </tr>
    <tr>
        <td>Visual C++ tools team blog</td>
        <td>[Visual C++ team blog](http://blogs.msdn.com/b/vcblog/)</td>
    </tr>
    <tr>
        <td>PIX team blog</td>
        <td>[Performance tuning and debugging for DirectX 12 games on Windows and Xbox](https://blogs.msdn.microsoft.com/pix/)</td>
    </tr>
    <tr>
        <td>Universal Windows App Deployment team blog</td>
        <td>[Build and deploy UWP apps team blog](https://blogs.msdn.microsoft.com/appinstaller/)</td>
    </tr>
</table>
 

## Concept and planning


In the concept and planning stage, you're deciding what your game is going to be like and the technologies and tools you'll use to bring it to life.

### Overview of game development technologies

When you start developing a game for the UWP you have multiple options available for graphics, input, audio, networking, utilities, and libraries.

If you've already decided on all the technologies you'll be using in your game, great! If not, the [Game technologies for UWP apps](game-development-platform-guide.md) guide is an excellent overview of many of the technologies available, and is highly recommended reading to help you understand the options and how they fit together.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Survey of UWP game technologies</td>
        <td>[Game technologies for UWP apps](game-development-platform-guide.md)</td>
    </tr>
</table>
 

These three GDC 2015 videos give a good overview of Windows 10 game development and the Windows 10 gaming experience.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Overview of Windows 10 game development (video)</td>
        <td>[Developing Games for Windows 10](http://channel9.msdn.com/Events/GDC/GDC-2015/Developing-Games-for-Windows-10)</td>
    </tr>
    <tr>
        <td>Windows 10 gaming experience (video)</td>
        <td>[Gaming Consumer Experience on Windows 10](http://channel9.msdn.com/Events/GDC/GDC-2015/Gaming-Consumer-Experience-on-Windows-10)</td>
    </tr>
    <tr>
        <td>Gaming across the Microsoft ecosystem (video)</td>
        <td>[The Future of Gaming Across the Microsoft Ecosystem](http://channel9.msdn.com/Events/GDC/GDC-2015/The-Future-of-Gaming-Across-the-Microsoft-Ecosystem)</td>
    </tr>
</table>

### Game planning

These are some high level concept and planning topics to consider when planning for your game.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Make your game accessible</td>
        <td>[Accessibility for games](https://msdn.microsoft.com/windows/uwp/gaming/accessibility-for-games)</td>
    </tr>
    <tr>
        <td>Build games using cloud</td>
        <td>[Cloud for games](https://msdn.microsoft.com/windows/uwp/gaming/cloud-for-games)</td>
    </tr>
    <tr>
        <td>Monetize your game</td>
        <td>[Monetization for games](https://msdn.microsoft.com/windows/uwp/gaming/monetization-for-games)</td>
    </tr>
</table>



### Choosing your graphics technology and programming language

There are several programming languages and graphics technologies available for use in Windows 10 games. The path you take depends on the type of game you’re developing, the experience and preferences of your development studio, and specific feature requirements of your game. Will you use C#, C++, or JavaScript? DirectX, XAML, or HTML5?

#### DirectX

Microsoft DirectX is the choice to make for the highest-performance 2D and 3D graphics and multimedia. 

Direct3D 12, new in Windows 10, brings the power of a console-like API and is faster and more efficient than ever before. Your game can fully utilize modern graphics hardware and feature more objects, richer scenes, and enhanced effects. Direct3D 12 delivers optimized graphics on Windows 10 PCs and Xbox One. If you want to use the familiar graphics pipeline of Direct3D 11, you’ll still benefit from the new rendering and optimization features added to Direct3D 11.3. And, if you’re a tried-and-true desktop Windows API developer with roots in Win32, you’ll still have that option in Windows 10.

The extensive features and deep platform integration of DirectX provide the power and performance needed by the most demanding games.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>DirectX for UWP development</td>
        <td>[DirectX programming](directx-programming.md)</td>
    </tr>
    <tr>
        <td>Tutorial: How to create a UWP DirectX game</td>
        <td>[Create a simple UWP game with DirectX](tutorial--create-your-first-metro-style-directx-game.md)</td>
    </tr>
    <tr>
        <td>DirectX overviews and reference</td>
        <td>[DirectX Graphics and Gaming](https://msdn.microsoft.com/library/windows/desktop/ee663274)</td>
    </tr>
    <tr>
        <td>Direct3D 12 programming guide and reference</td>
        <td>[Direct3D 12 Graphics](https://msdn.microsoft.com/library/windows/desktop/dn903821)</td>
    </tr>
    <tr>
        <td>Graphics and DirectX 12 development videos (YouTube channel)</td>
        <td>[Microsoft DirectX 12 and Graphics Education](https://www.youtube.com/channel/UCiaX2B8XiXR70jaN7NK-FpA)</td>
    </tr>
</table>
 

#### XAML

XAML is an easy-to-use declarative UI language with convenient features like animations, storyboards, data binding, scalable vector-based graphics, dynamic resizing, and scene graphs. XAML works great for game UI, menus, sprites, and 2D graphics. To make UI layout easy, XAML is compatible with design and development tools like Expression Blend and Microsoft Visual Studio. XAML is commonly used with C#, but C++ is also a good choice if that’s your preferred language or if your game has high CPU demands.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>XAML platform overview</td>
        <td>[XAML platform](https://msdn.microsoft.com/library/windows/apps/mt228259)</td>
    </tr>
    <tr>
        <td>XAML UI and controls</td>
        <td>[Controls, layouts, and text](https://msdn.microsoft.com/library/windows/apps/mt228348)</td>
    </tr>
</table>
 

#### HTML 5

HyperText Markup Language (HTML) is a common UI markup language used for web pages, apps, and rich clients. Windows games can use HTML5 as a full-featured presentation layer with the familiar features of HTML, access to the Universal Windows Platform, and support for modern web features like AppCache, Web Workers, canvas, drag-and-drop, asynchronous programming, and SVG. Behind the scenes, HTML rendering takes advantage of the power of DirectX hardware acceleration, so you can still get the performance benefits of DirectX without writing any extra code. HTML5 is a good choice if you are proficient with web development, porting a web game, or want to use language and graphics layers that can be easier to approach than the other choices. HTML5 is used with JavaScript, but can also call into components created with C# or C++/CX.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>HTML5 and Document Object Model information</td>
        <td>[HTML and DOM reference](https://msdn.microsoft.com/library/windows/apps/br212882.aspx)</td>
    </tr>
    <tr>
        <td>The HTML5 W3C Recommendation</td>
        <td>[HTML5](http://go.microsoft.com/fwlink/p/?linkid=221374)</td>
    </tr>
</table>
 

#### Combining presentation technologies

The Microsoft DirectX Graphics Infrastructure (DXGI) provides interop and compatibility across multiple graphics technologies. For high-performance graphics, you can combine XAML and DirectX, using XAML for menus and other simple UI, and DirectX for rendering complex 2D and 3D scenes. DXGI also provides compatibility between Direct2D, Direct3D, DirectWrite, DirectCompute, and the Microsoft Media Foundation.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>DirectX Graphics Infrastructure programming guide and reference</td>
        <td>[DXGI](https://msdn.microsoft.com/library/windows/desktop/hh404534)</td>
    </tr>
    <tr>
        <td>Combining DirectX and XAML</td>
        <td>[DirectX and XAML interop](directx-and-xaml-interop.md)</td>
    </tr>
</table>
 

#### C++

C++/CX is a high-performance, low overhead language that provides the powerful combination of speed, compatibility, and platform access. C++/CX makes it easy to use all of the great gaming features in Windows 10, including DirectX and Xbox Live. You can also reuse existing C++ code and libraries. C++/CX creates fast, native code that doesn’t incur the overhead of garbage collection, so your game can have great performance and low power consumption, which leads to longer battery life. Use C++/CX with DirectX or XAML, or create a game that uses a combination of both.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>C++/CX reference and overviews</td>
        <td>[Visual C++ Language Reference (C++/CX)](https://msdn.microsoft.com/library/windows/apps/hh699871.aspx)</td>
    </tr>
    <tr>
        <td>Visual C++ programming guide and reference</td>
        <td>[Visual C++ in Visual Studio 2015](https://msdn.microsoft.com/library/60k1461a.aspx)</td>
    </tr>
</table>
 

#### C#

C# (pronounced "C sharp") is a modern, innovative language that is simple, powerful, type-safe, and object-oriented. C# enables rapid development while retaining the familiarity and expressiveness of C-style languages. Though easy to use, C# has numerous advanced language features like polymorphism, delegates, lambdas, closures, iterator methods, covariance, and Language-Integrated Query (LINQ) expressions. C# is an excellent choice if you are targeting XAML, want to get a quick start developing your game, or have previous C# experience. C# is used primarily with XAML, so if you want to use DirectX, choose C++ instead, or write part of your game as a C++ component that interacts with DirectX. Or, consider [Win2D](https://github.com/Microsoft/Win2D), an immediate mode Direct2D graphics libary for C# and C++.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>C# programming guide and reference</td>
        <td>[C# language reference](https://msdn.microsoft.com/library/kx37x362.aspx)</td>
    </tr>
</table>
 

#### JavaScript

JavaScript is a dynamic scripting language widely used for modern web and rich client applications.

Windows JavaScript apps can access the powerful features of the Universal Windows Platform in an easy, intuitive way—as methods and properties of object-oriented JavaScript classes. JavaScript is a good choice for your game if you’re coming from a web development environment, are already familiar with JavaScript, or want to use HTML5, CSS, WinJS, or JavaScript libraries. If you’re targeting DirectX or XAML, choose C# or C++/CX instead.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>JavaScript and Windows Runtime reference</td>
        <td>[JavaScript reference](https://msdn.microsoft.com/library/windows/apps/jj613794)</td>
    </tr>
</table>


#### Use Windows Runtime Components to combine languages

With the Universal Windows Platform, it’s easy to combine components written in different languages. Create Windows Runtime Components in C++, C#, or Visual Basic, and then call into them from JavaScript, C#, C++, or Visual Basic. This is a great way to program portions of your game in the language of your choice. Components also let you consume external libraries that are only available in a particular language, as well as use legacy code you’ve already written.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>How to create Windows Runtime Components</td>
        <td>[Creating Windows Runtime Components](https://docs.microsoft.com/windows/uwp/winrt-components/creating-windows-runtime-components-in-cpp)</td>
    </tr>
</table>


### Which version of DirectX should your game use?

If you are choosing DirectX for your game, you'll need to decide which version to use: Microsoft Direct3D 12 or Microsoft Direct3D 11.

Direct3D 12, new in Windows 10, brings the power of a console-like API and is faster and more efficient than ever before. Your game can fully utilize modern graphics hardware and feature more objects, richer scenes, and enhanced effects. Direct3D 12 delivers optimized graphics on Windows 10 PCs and Xbox One. Since Direct3D 12 works at a very low level, it is able to give an expert graphics development team or an experienced DirectX 11 development team all the control they need to maximize graphics optimization.

Direct3D 11.3 is a low level graphics API that uses the familiar Direct3D programming model and handles for you more of the complexity involved in GPU rendering. It is also supported in Windows 10 and Xbox One. If you have an existing engine written in Direct3D 11, and you're not quite ready to make the jump to Direct3D 12, you can use Direct3D 11 on 12 to achieve some performance improvements. Versions 11.3+ contain the new rendering and optimization features enabled also in Direct3D 12.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Choosing Direct3D 12 or Direct3D 11</td>
        <td>[What is Direct3D 12?](https://msdn.microsoft.com/library/windows/desktop/dn899228)</td>
    </tr>
    <tr>
        <td>Overview of Direct3D 11</td>
        <td>[Direct3D 11 Graphics](https://msdn.microsoft.com/library/windows/desktop/ff476080)</td>
    </tr>
    <tr>
        <td>Overview of Direct3D 11 on 12</td>
        <td>[Direct3D 11 on 12](https://msdn.microsoft.com/library/windows/desktop/dn913195)</td>
    </tr>
</table>


### Bridges, game engines, and middleware

Depending on the needs of your game, using bridges, game engines, or middleware can save development and testing time and resources. Here are some overview and resources for bridges, game engines, and middleware to help you decide if any are right for you.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Game Development with Middleware (video)</td>
        <td>[Accelerating Microsoft Store Game Development with Middleware](https://channel9.msdn.com/Events/Build/2013/3-187)</td>
    </tr>
    <tr>
        <td>Introduction to game middleware (blog post)</td>
        <td>[Game Development Middleware - What is it? Do I need it?](http://blogs.msdn.com/b/wsdevsol/archive/2014/05/02/game-development-middleware-what-is-it-do-i-need-it.aspx)</td>
    </tr>
</table>
 

#### Universal Windows Platform Bridges

Universal Windows Platform Bridges are technologies that bring your existing app or game over to the UWP. Bridges are a great way to get a quick start on UWP game development.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>UWP bridges</td>
        <td>[Bring your code to Windows](https://dev.windows.com/bridges/)</td>
    </tr>
    <tr>
        <td>Windows Bridge for iOS</td>
        <td>[Bring your iOS apps to Windows](https://dev.windows.com/bridges/ios)</td>
    </tr>
    <tr>
        <td>Windows Bridge for desktop applications (.NET and Win32)</td>
        <td>[Convert your desktop application to a UWP app](https://developer.microsoft.com/windows/bridges/desktop)</td>
    </tr>
</table>
 

#### Unity

Unity offers a platform for creating beautiful and engaging 2D, 3D, VR, and AR games and apps. It enables you to realize your creative vision fast and delivers your content to virtually any media or device.

Beginning with Unity 5.4, Unity supports Direct3D 12 development.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>The Unity game engine</td>
        <td>[Unity - Game Engine](http://unity3d.com/)</td>
    </tr>
    <tr>
        <td>Get Unity</td>
        <td>[Get Unity](http://unity3d.com/get-unity)</td>
    </tr>
    <tr>
        <td>Universal Windows Platform app support in Unity 5.2 or later (blog post)</td>
        <td>[Windows 10 UWP apps in Unity 5.2](http://blogs.unity3d.com/2015/09/09/windows-10-universal-apps-in-unity-5-2/)</td>
    </tr>
    <tr>
        <td>Unity documentation for Windows</td>
        <td>[Unity Manual / Windows](http://docs.unity3d.com/Manual/Windows.html)</td>
    </tr>
    <tr>
        <td>How to add interactivity to your game using Mixer Interactive</td>
        <td>[Getting started guide](https://github.com/mixer/interactive-unity-plugin/wiki/Getting-started)</td>
    </tr>
    <tr>
        <td>Mixer SDK for Unity</td>
        <td>[Mixer Unity plugin](https://www.assetstore.unity3d.com/en/#!/content/88585)</td>
    </tr>
    <tr>
        <td>Mixer SDK for Unity reference documentation</td>
        <td>[API reference for Mixer Unity plugin](https://dev.mixer.com/reference/interactive/csharp/index.html)</td>
    </tr>
    <tr>
        <td>Publish your Unity game to Microsoft Store</td>
        <td>[Porting guide](https://unity3d.com/partners/microsoft/porting-guides)</td>
    </tr>
    <tr>
        <td>Troubleshooting missing assembly references related to .NET APIs</td>
        <td>[Missing .NET APIs in Unity and UWP](https://docs.microsoft.com/windows/uwp/gaming/missing-dot-net-apis-in-unity-and-uwp)</td>
    </tr>
    <tr>
        <td>Publish your Unity game as a Universal Windows Platform app (video)</td>
        <td>[How to publish your Unity game as a UWP app](https://channel9.msdn.com/Blogs/One-Dev-Minute/How-to-publish-your-Unity-game-as-a-UWP-app)</td>
    </tr>
    <tr>
        <td>Use Unity to make Windows games and apps (video)</td>
        <td>[Making Windows games and apps with Unity](https://channel9.msdn.com/Blogs/One-Dev-Minute/Making-games-and-apps-with-Unity)</td>
    </tr>
    <tr>
        <td>Unity game development using Visual Studio (video series)</td>
        <td>[Using Unity with Visual Studio 2015](http://go.microsoft.com/fwlink/?LinkId=722359)</td>
    </tr>
</table>
 

#### Havok

Havok’s modular suite of tools and technologies help game creators reach new levels of interactivity and immersion. Havok enables highly realistic physics, interactive simulations, and stunning cinematics. Version 2015.1 and higher officially supports UWP in Visual Studio 2015 on x86, 64-bit, and ARM.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Havok website</td>
        <td>[Havok](http://www.havok.com/)</td>
    </tr>
    <tr>
        <td>Havok tool suite</td>
        <td>[Havok Product Overview](http://www.havok.com/products/)</td>
    </tr>
    <tr>
        <td>Havok support forums</td>
        <td>[Havok](http://support.havok.com)</td>
    </tr>
</table>
 

#### MonoGame

MonoGame is an open source, cross-platform game development framework originally based on Microsoft's XNA Framework 4.0. Monogame currently supports Windows, Windows Phone, and Xbox, as well as Linux, macOS, iOS, Android, and several other platforms.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>MonoGame</td>
        <td>[MonoGame website](http://www.monogame.net)</td>
    </tr>
    <tr>
        <td>MonoGame Documentation</td>
        <td>[MonoGame Documentation (latest)](http://www.monogame.net/documentation/)</td>
    </tr>
    <tr>
        <td>Monogame Downloads</td>
        <td>[Download releases, development builds, and source code](http://www.monogame.net/downloads/) from the MonoGame website, or [get the latest release via NuGet](https://www.nuget.org/profiles/MonoGame).
    </tr>
</table>


#### Cocos2d

Cocos2d-X is a cross-platform open source game development engine and tools suite that supports building UWP games. Beginning with version 3, 3D features are being added as well.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Cocos2d-x</td>
        <td>[What is Cocos2d-X?](http://www.cocos2d-x.org/)</td>
    </tr>
    <tr>
        <td>Cocos2d-x programmer's guide</td>
        <td>[Cocos2d-x Programmers Guide v3.8](http://www.cocos2d-x.org/programmersguide/)</td>
    </tr>
    <tr>
        <td>Cocos2d-x on Windows 10 (blog post)</td>
        <td>[Running Cocos2d-x on Windows 10](https://blogs.windows.com/buildingapps/2015/06/15/running-cocos2d-x-on-windows-10/)</td>
    </tr>
</table>


#### Unreal Engine

Unreal Engine 4 is a complete suite of game development tools for all types of games and developers. For the most demanding console and PC games, Unreal Engine is used by game developers worldwide.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Unreal Engine overview</td>
        <td>[Unreal Engine 4](https://www.unrealengine.com/what-is-unreal-engine-4)</td>
    </tr>
</table>

#### BabylonJS

BabylonJS is a complete JavaScript framework for building 3D games with HTML5, WebGL, and Web Audio.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>BabylonJS</td>
        <td>[BabylonJS](http://www.babylonjs.com/)</td>
    </tr>
    <tr>
        <td>WebGL 3D with HTML5 and BabylonJS (video series)</td>
        <td>[Learning WebGL 3D and BabylonJS](https://channel9.msdn.com/Series/Introduction-to-WebGL-3D-with-HTML5-and-Babylonjs/01)</td>
    </tr>
    <tr>
        <td>Building a cross-platform WebGL game with BabylonJS</td>
        <td>[Use BabylonJS to develop a cross-platform game](https://www.smashingmagazine.com/2016/07/babylon-js-building-sponza-a-cross-platform-webgl-game/)</td>
    </tr>    
</table>

### Middleware and partners

There are many other middleware and engine partners that can provide solutions depending on your game development needs.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Windows Dev Center partners</td>
        <td>[Dev Center Partners](https://developer.microsoft.com/windows/app-middleware-partners)</td>
    </tr>
</table>


### Porting your game

If you have an existing game, there are many resources and guides available to help you quickly bring your game to the UWP. To jumpstart your porting efforts, you might also consider using a [Universal Windows Platform Bridge](#universal-windows-platform-bridges).

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Porting a Windows 8 app to a Universal Windows Platform app</td>
        <td>[Move from Windows Runtime 8.x to UWP](https://msdn.microsoft.com/library/windows/apps/mt238322)</td>
    </tr>
    <tr>
        <td>Porting a Windows 8 app to a Universal Windows Platform app (video)</td>
        <td>[Porting 8.1 Apps to Windows 10](https://channel9.msdn.com/Series/A-Developers-Guide-to-Windows-10/21)</td>
    </tr>
    <tr>
        <td>Porting an iOS app to a Universal Windows Platform app</td>
        <td>[Move from iOS to UWP](https://msdn.microsoft.com/library/windows/apps/mt238320)</td>
    </tr>
    <tr>
        <td>Porting a Silverlight app to a Universal Windows Platform app</td>
        <td>[Move from Windows Phone Silverlight to UWP](https://msdn.microsoft.com/library/windows/apps/mt238323)</td>
    </tr>
    <tr>
        <td>Porting from XAML or Silverlight to a Universal Windows Platform app (video)</td>
        <td>[Porting an App from XAML or Silverlight to Windows 10](https://channel9.msdn.com/Events/Build/2015/3-741)</td>
    </tr>
    <tr>
        <td>Porting an Xbox game to a Universal Windows Platform app</td>
        <td>[Porting from Xbox One to Windows 10 UWP](https://developer.xboxlive.com/en-us/platform/development/education/Documents/Porting%20from%20Xbox%20One%20to%20Windows%2010.aspx)</td>
    </tr>
    <tr>
        <td>Porting from DirectX 9 to DirectX 11</td>
        <td>[Port from DirectX 9 to Universal Windows Platform (UWP)](porting-your-directx-9-game-to-windows-store.md)</td>
    </tr>
    <tr>
        <td>Porting from Direct3D 11 to Direct3D 12</td>
        <td>[Porting from Direct3D 11 to Direct3D 12](https://msdn.microsoft.com/library/windows/desktop/mt431709)</td>
    </tr>
    <tr>
        <td>Porting from OpenGL ES to Direct3D 11</td>
        <td>[Port from OpenGL ES 2.0 to Direct3D 11](port-from-opengl-es-2-0-to-directx-11-1.md)</td>
    </tr>
    <tr>
        <td>OpenGL ES to Direct3D 11 using ANGLE</td>
        <td>[ANGLE](http://go.microsoft.com/fwlink/p/?linkid=618387)</td>
    </tr>
    <tr>
        <td>Classic Windows API equivalents in the UWP</td>
        <td>[Alternatives to Windows APIs in Universal Windows Platform (UWP) apps](https://msdn.microsoft.com/library/windows/apps/hh464945)</td>
    </tr>
</table>


## Prototype and design


Now that you've decided the type of game you want to create and the tools and graphics technology you'll use to build it, you're ready to get started with the design and prototype. At its core, your game is a Universal Windows Platform app, so that's where you'll begin.

### Introduction to the Universal Windows Platform (UWP)

Windows 10 introduces the Universal Windows Platform (UWP), which provides a common API platform across Windows 10 devices. UWP evolves and expands the Windows Runtime model and hones it into a cohesive, unified core. Games that target the UWP can call WinRT APIs that are common to all devices. Because the UWP provides guaranteed API layers, you can choose to create a single app package that will install across Windows 10 devices. And if you want to, your game can still call APIs (including some classic Windows APIs from Win32 and .NET) that are specific to the devices your game runs on.

The following are excellent guides that discuss the Universal Windows Platform apps in detail, and are recommended reading to help you understand the platform.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Introduction to Universal Windows Platform apps</td>
        <td>[What's a Universal Windows Platform app?](https://msdn.microsoft.com/library/windows/apps/dn726767)</td>
    </tr>
    <tr>
        <td>Overview of the UWP</td>
        <td>[Guide to UWP apps](https://msdn.microsoft.com/library/windows/apps/dn894631)</td>
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
        <td>[Get started with Windows apps](https://dev.windows.com/getstarted)</td>
    </tr>
    <tr>
        <td>Getting set up for UWP development</td>
        <td>[Get set up](https://msdn.microsoft.com/library/windows/apps/dn726766)</td>
    </tr>
</table>

If you're an "absolute beginner" to UWP programming, and are considering using XAML in your game (see [Choosing your graphics technology and programming language](#choosing-your-graphics-technology-and-programming-language)), the [Windows 10 development for absolute beginners](https://channel9.msdn.com/Series/Windows-10-development-for-absolute-beginners) video series is a good place to start.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Beginners guide to Windows 10 development with XAML (Video series)</td>
        <td>[Windows 10 development for absolute beginners](https://channel9.msdn.com/Series/Windows-10-development-for-absolute-beginners)</td>
    </tr>
    <tr>
        <td>Announcing the Windows 10 absolute beginners series using XAML (blog post)</td>
        <td>[Windows 10 development for absolute beginners](http://blogs.windows.com/buildingapps/2015/09/30/windows-10-development-for-absolute-beginners/)</td>
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
        <td>[Develop Windows apps](https://dev.windows.com/develop)</td>
    </tr>
    <tr>
        <td>Overview of network programming in the UWP</td>
        <td>[Networking and web services](https://msdn.microsoft.com/library/windows/apps/mt280378)</td>
    </tr>
    <tr>
        <td>Using Windows.Web.HTTP and Windows.Networking.Sockets in games</td>
        <td>[Networking for games](work-with-networking-in-your-directx-game.md)</td>
    </tr>
    <tr>
        <td>Asynchronous programming concepts in the UWP</td>
        <td>[Asynchronous programming](https://msdn.microsoft.com/library/windows/apps/mt187335)</td>
    </tr>
</table>

### Windows Desktop APIs to UWP

These are some links to help you move your Windows desktop game to UWP.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Use existing C++ code for UWP game development</td>
        <td>[How to: Use existing C++ code in a UWP app](https://docs.microsoft.com/cpp/porting/how-to-use-existing-cpp-code-in-a-universal-windows-platform-app)</td>
    </tr>
    <tr>
        <td>UWP APIs for Win32 and COM APIs</td>
        <td>[Win32 and COM APIs for UWP apps](https://docs.microsoft.com/uwp/win32-and-com/win32-and-com-for-uwp-apps)</td>
    </tr>
    <tr>
        <td>Unsupported CRT functions in UWP</td>
        <td>[CRT functions not supported in Universal Windows Platform apps](https://msdn.microsoft.com/library/windows/apps/jj606124.aspx)</td>
    </tr>
    <tr>
        <td>Alternatives for Windows APIs</td>
        <td>[Alternatives to Windows APIs in Universal Windows Platform (UWP) apps](https://msdn.microsoft.com/library/windows/apps/mt592894.aspx)</td>
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
        <td>[App lifecycle](https://msdn.microsoft.com/library/windows/apps/mt243287)</td>
    </tr>
    <tr>
        <td>Using Microsoft Visual Studio to trigger app transitions</td>
        <td>[How to trigger suspend, resume, and background events for UWP apps in Visual Studio](https://msdn.microsoft.com/library/hh974425.aspx)</td>
    </tr>
</table>
 

### Designing game UX

The genesis of a great game is inspired design.

Games share some common user interface elements and design principles with apps, but games often have a unique look, feel, and design goal for their user experience. Games succeed when thoughtful design is applied to both aspects—when should your game use tested UX, and when should it diverge and innovate? The presentation technology that you choose for your game—DirectX, XAML, HTML5, or some combination of the three—will influence implementation details, but the design principles you apply are largely independent of that choice.

Separately from UX design, gameplay design such as level design, pacing, world design, and other aspects is an art form of its own—one that's up to you and your team, and not covered in this development guide.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>UWP design basics and guidelines</td>
        <td>[Designing UWP apps](https://dev.windows.com/design)</td>
    </tr>
    <tr>
        <td>Designing for app lifecycle states</td>
        <td>[UX guidelines for launch, suspend, and resume](https://msdn.microsoft.com/library/windows/apps/dn611862)</td>
    </tr>
    <tr>
        <td>Design your UWP app for Xbox One and television screens</td>
        <td>[Designing for Xbox and TV](https://docs.microsoft.com/windows/uwp/input/designing-for-tv)</td>
    </tr>
    <tr>
        <td>Targeting multiple device form factors (video)</td>
        <td>[Designing Games for a Windows Core World](http://channel9.msdn.com/Events/GDC/GDC-2015/Designing-Games-for-a-Windows-Core-World)</td>
    </tr>   
</table>
 

#### Color guideline and palette

Following a consistent color guideline in your game improves aesthetics, aids navigation, and is a powerful tool to inform the player of menu and HUD functionality. Consistent coloring of game elements like warnings, damage, XP, and achievements can lead to cleaner UI and reduce the need for explicit labels.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Color guide</td>
        <td>[Best Practices: Color](https://assets.windowsphone.com/499cd2be-64ed-4b05-a4f5-cd0c9ad3f6a3/101_BestPractices_Color_InvariantCulture_Default.zip)</td>
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
        <td>[Best Practices: Typography](http://go.microsoft.com/fwlink/?LinkId=535007)</td>
    </tr>
</table>
 

#### UI map

A UI map is a layout of game navigation and menus expressed as a flowchart. The UI map helps all involved stakeholders understand the game’s interface and navigation paths, and can expose potential roadblocks and dead ends early in the development cycle.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>UI map guide</td>
        <td>[Best Practices: UI Map](http://go.microsoft.com/fwlink/?LinkId=535008)</td>
    </tr>
</table>

### Game audio

Guides and references for implementing audio in games using XAudio2, XAPO, and Windows Sonic. XAudio2 is a low-level audio API that provides signal processing and mixing foundation for developing high performance audio engines. XAPO API allows the creation of cross-platform audio processing objects (XAPO) for use in XAudio2 on both Windows and Xbox. Windows Sonic audio support allows you to add Dolby Atmos for Home Theater, Dolby Atmos for Headphones, and Windows HRTF support to your game or streaming media application.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>XAudio2 APIs</td>
        <td>[Programming guide and API reference for XAudio2](https://msdn.microsoft.com/library/windows/desktop/hh405049.aspx)</td>
    </tr>
    <tr>
        <td>Create cross-platform audio processing objects</td>
        <td>[XAPO Overview](https://msdn.microsoft.com/library/windows/desktop/ee415735.aspx)</td>
    </tr>
    <tr>
        <td>Intro to audio concepts</td>
        <td>[Audio for games](working-with-audio-in-your-directx-game.md)</td>
    </tr>
    <tr>
        <td>Windows Sonic overview</td>
        <td>[Spatial sound](https://msdn.microsoft.com/library/windows/desktop/mt807491.aspx)</td>
    </tr>
    <tr>
        <td>Windows Sonic spatial sound samples</td>
        <td>[Xbox Advanced Technology Group audio samples](https://github.com/Microsoft/Xbox-ATG-Samples/tree/master/UWPSamples/Audio)</td>
    </tr>
    <tr>
        <td>Learn how to integrate Windows Sonic into your games (video)</td>
        <td>[Introducing Spatial Audio Capabilities for Xbox and Windows](https://channel9.msdn.com/Events/GDC/GDC-2017/GDC2017-002)</td>
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
        <td>[DirectX programming](directx-programming.md)</td>
    </tr>
    <tr>
        <td>Tutorial: How to create a UWP DirectX game</td>
        <td>[Create a simple UWP game with DirectX](tutorial--create-your-first-metro-style-directx-game.md)</td>
    </tr>
    <tr>
        <td>DirectX interaction with the UWP app model</td>
        <td>[The app object and DirectX](about-the-metro-style-user-interface-and-directx.md)</td>
    </tr>
    <tr>
        <td>Graphics and DirectX 12 development videos (YouTube channel)</td>
        <td>[Microsoft DirectX 12 and Graphics Education](https://www.youtube.com/channel/UCiaX2B8XiXR70jaN7NK-FpA)</td>
    </tr>
    <tr>
        <td>DirectX overviews and reference</td>
        <td>[DirectX Graphics and Gaming](https://msdn.microsoft.com/library/windows/desktop/ee663274)</td>
    </tr>
    <tr>
        <td>Direct3D 12 programming guide and reference</td>
        <td>[Direct3D 12 Graphics](https://msdn.microsoft.com/library/windows/desktop/dn903821)</td>
    </tr>
    <tr>
        <td>DirectX 12 fundamentals (video)</td>
        <td>[Better Power, Better Performance: Your Game on DirectX 12](http://channel9.msdn.com/Events/GDC/GDC-2015/Better-Power-Better-Performance-Your-Game-on-DirectX12)</td>
    </tr>
</table>

#### Learning Direct3D 12

Learn what changed in Direct3D 12 and how to start programming using Direct3D 12. 

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Set up programming environment</td>
        <td>[Direct3D 12 programming environment setup](https://msdn.microsoft.com/library/windows/desktop/dn899120.aspx)</td>
    </tr>
    <tr>
        <td>How to create a basic component</td>
        <td>[Creating a basic Direct3D 12 component](https://msdn.microsoft.com/library/windows/desktop/dn859356.aspx)</td>
    </tr>
    <tr>
        <td>Changes in Direct3D 12</td>
        <td>[Important changes migrating from Direct3D 11 to Direct3D 12](https://msdn.microsoft.com/library/windows/desktop/dn899194.aspx)</td>
    </tr>
    <tr>
        <td>How to port from Direct3D 11 to Direct3D 12</td>
        <td>[Porting from Direct3D 11 to Direct3D 12](https://msdn.microsoft.com/library/windows/desktop/mt431709.aspx)</td>
    </tr>
    <tr>
        <td>Resource binding concepts (covering descriptor, descriptor table, descriptor heap, and root signature) </td>
        <td>[Resource binding in Direct3D 12](https://msdn.microsoft.com/library/windows/desktop/dn899206.aspx)</td>
    </tr>
    <tr>
        <td>Managing memory</td>
        <td>[Memory management in Direct3D 12](https://msdn.microsoft.com/library/windows/desktop/dn899198.aspx)</td>
    </tr>
</table>
 
#### DirectX Tool Kit and libraries

The DirectX Tool Kit, DirectX texture processing library, DirectXMesh geometry processing library, UVAtlas library, and DirectXMath library provide texture, mesh, sprite, and other utility functionality and helper classes for DirectX development. These libraries can help you save development time and effort.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Get DirectX Tool Kit for DirectX 11</td>
        <td>[DirectXTK](http://go.microsoft.com/fwlink/?LinkId=248929)</td>
    </tr>
    <tr>
        <td>Get DirectX Tool Kit for DirectX 12</td>
        <td>[DirectXTK 12](http://go.microsoft.com/fwlink/?LinkID=615561)</td>
    </tr>
    <tr>
        <td>Get DirectX texture processing library</td>
        <td>[DirectXTex](http://go.microsoft.com/fwlink/?LinkId=248926)</td>
    </tr>
    <tr>
        <td>Get DirectXMesh geometry processing library</td>
        <td>[DirectXMesh](http://go.microsoft.com/fwlink/?LinkID=324981)</td>
    </tr>
    <tr>
        <td>Get UVAtlas for creating and packing isochart texture atlas</td>
        <td>[UVAtlas](http://go.microsoft.com/fwlink/?LinkID=512686)</td>
    </tr>
    <tr>
        <td>Get the DirectXMath library</td>
        <td>[DirectXMath](http://go.microsoft.com/fwlink/?LinkID=615560)</td>
    </tr>
    <tr>
        <td>Direct3D 12 support in the DirectXTK (blog post)</td>
        <td>[Support for DirectX 12](https://github.com/Microsoft/DirectXTK/issues/2)</td>
    </tr>
</table>

#### DirectX resources from partners

These are some additional DirectX documentation created by external partners.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Nvidia: DX12 Do's and Don'ts (blog post) </td>
        <td>[DirectX 12 on Nvidia GPUs](https://developer.nvidia.com/dx12-dos-and-donts-updated)</td>
    </tr>
    <tr>
        <td>Intel: Efficient rendering with DirectX 12</td>
        <td>[DirectX 12 rendering on Intel Graphics](https://software.intel.com/sites/default/files/managed/4a/38/Efficient-Rendering-with-DirectX-12-on-Intel-Graphics.pdf)</td>
    </tr>
    <tr>
        <td>Intel: Multi adapter support in DirectX 12</td>
        <td>[How to implement an explicit multi-adapter application using DirectX 12](https://software.intel.com/articles/multi-adapter-support-in-directx-12)</td>
    </tr>
    <tr>
        <td>Intel: DirectX 12 tutorial</td>
        <td>[Collaborative white paper by Intel, Suzhou Snail and Microsoft](https://software.intel.com/articles/tutorial-migrating-your-apps-to-directx-12-part-1)</td>
    </tr>
</table>


## Production


Your studio is now fully engaged and moving into the production cycle, with work distributed throughout your team. You're polishing, refactoring, and extending the prototype to craft it into a full game.

### Notifications and live tiles

A tile is your game's representation on the Start Menu. Tiles and notifications can drive player interest even when they aren't currently playing your game.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Developing tiles and badges</td>
        <td>[Tiles, badges, and notifications](https://msdn.microsoft.com/library/windows/apps/mt185606)</td>
    </tr>
    <tr>
        <td>Sample illustrating live tiles and notifications</td>
        <td>[Notifications sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/Notifications)</td>
    </tr>
    <tr>
        <td>Adaptive tile templates (blog post)</td>
        <td>[Adaptive Tile Templates - Schema and Documentation](http://blogs.msdn.com/b/tiles_and_toasts/archive/2015/06/30/adaptive-tile-templates-schema-and-documentation.aspx)</td>
    </tr>
    <tr>
        <td>Designing tiles and badges</td>
        <td>[Guidelines for tiles and badges](https://msdn.microsoft.com/library/windows/apps/hh465403)</td>
    </tr>
    <tr>
        <td>Windows 10 app for interactively developing live tile templates</td>
        <td>[Notifications Visualizer](https://www.microsoft.com/store/apps/9nblggh5xsl1)</td>
    </tr>
    <tr>
        <td>UWP Tile Generator extension for Visual Studio</td>
        <td>[Tool for creating all required tiles using single image](https://visualstudiogallery.msdn.microsoft.com/09611e90-f3e8-44b7-9c83-18dba8275bb2)</td>
    </tr>
    <tr>
        <td>UWP Tile Generator extension for Visual Studio (blog post)</td>
        <td>[Tips on using the UWP Tile Generator tool](https://blogs.windows.com/buildingapps/2016/02/15/uwp-tile-generator-extension-for-visual-studio/)</td>
    </tr>
</table>
 

### Enable in-app product (IAP) purchases

An IAP (in-app product) is a supplementary item that players can purchase in-game. IAPs can be new add-ons, game levels, items, or anything else that your players might enjoy. Used appropriately, IAPs can provide revenue while improving the game experience. You define and publish your game's IAPs through the Windows Dev Center dashboard, and enable in-app purchases in your game's code.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Durable in-app products</td>
        <td>[Enable in-app product purchases](https://msdn.microsoft.com/library/windows/apps/mt219684)</td>
    </tr>
    <tr>
        <td>Consumable in-app products</td>
        <td>[Enable consumable in-app product purchases](https://msdn.microsoft.com/library/windows/apps/mt219683)</td>
    </tr>
    <tr>
        <td>In-app product details and submission</td>
        <td>[IAP submissions](https://msdn.microsoft.com/library/windows/apps/mt148551)</td>
    </tr>
    <tr>
        <td>Monitor IAP sales and demographics for your game</td>
        <td>[IAP acquisitions report](https://msdn.microsoft.com/library/windows/apps/mt148538)</td>
    </tr>
</table>
 
### Debugging, performance optimization, and monitoring

To optimize performance, take advantage of Game Mode in Windows 10 to provide your gamers with the best possible gaming experience by fully utilizing the capacity of their current hardware.

The Windows Performance Toolkit (WPT) consists of performance monitoring tools that produce in-depth performance profiles of Windows operating systems and applications. This is especially useful for monitoring memory usage and improving game performance. The Windows Performance Toolkit is included in the Windows 10 SDK and Windows ADK. This toolkit consists of two independent tools: Windows Performance Recorder (WPR) and Windows Performance Analyzer (WPA). ProcDump, which is part of [Windows Sysinternals](https://technet.microsoft.com/sysinternals/default), is a command-line utility that monitors CPU spikes and generates dump files during game crashes. 

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Performance test your code</td>
        <td>[Cloud based load testing](https://www.visualstudio.com/team-services/cloud-load-testing/)</td>
    </tr>
    <tr>
        <td>Get Xbox console type using Gaming Device Information</td>
        <td>[Gaming Device Information](https://msdn.microsoft.com/library/windows/desktop/mt825235)</td>
    </tr>
    <tr>
        <td>Improve performance by getting exclusive or priority access to hardware resources using Game Mode APIs</td>
        <td>[Game Mode](https://msdn.microsoft.com/library/windows/desktop/mt808808)</td>
    </tr>
    <tr>
        <td>Get Windows Performance Toolkit (WPT) from Windows 10 SDK</td>
        <td>[Windows 10 SDK](https://developer.microsoft.com/windows/downloads/windows-10-sdk)</td>
    </tr>
    <tr>
        <td>Get Windows Performance Toolkit (WPT) from Windows ADK</td>
        <td>[Windows ADK](https://msdn.microsoft.com/windows/hardware/dn913721.aspx)</td>
    </tr>
    <tr>
        <td>Troubleshoot unresponsible UI using Windows Performance Analyzer (video)</td>
        <td>[Critical path analysis with WPA](https://channel9.msdn.com/Shows/Defrag-Tools/Defrag-Tools-156-Critical-Path-Analysis-with-Windows-Performance-Analyzer)</td>
    </tr>
    <tr>
        <td>Diagnose memory usage and leaks using Windows Performance Recorder (video)</td>
        <td>[Memory footprint and leaks](https://channel9.msdn.com/Shows/Defrag-Tools/Defrag-Tools-154-Memory-Footprint-and-Leaks)</td>
    </tr>
    <tr>
        <td>Get ProcDump</td>
        <td>[ProcDump](https://technet.microsoft.com/sysinternals/dd996900)</td>
    </tr>
    <tr>
        <td>Learn to use ProcDump (video)</td>
        <td>[Configure ProcDump to create dump files](https://channel9.msdn.com/Shows/Defrag-Tools/Defrag-Tools-131-Windows-10-SDK)</td>
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
        <td>PIX on Windows</td>
        <td>[Performance tuning and debugging tool for DirectX 12 on Windows](https://blogs.msdn.microsoft.com/pix/2017/01/17/introducing-pix-on-windows-beta/)</td>
    </tr>
    <tr>
        <td>Debugging and validation tools for D3D12 development (video)</td>
        <td>[D3D12 Performance Tuning and Debugging with PIX and GPU Validation](https://channel9.msdn.com/Events/GDC/GDC-2017/GDC2017-003)</td>
    </tr>
    <tr>
        <td>Optimizing graphics and performance (video)</td>
        <td>[Advanced DirectX 12 Graphics and Performance](http://channel9.msdn.com/Events/GDC/GDC-2015/Advanced-DirectX12-Graphics-and-Performance)</td>
    </tr>
    <tr>
        <td>DirectX graphics debugging (video)</td>
        <td>[Solve the tough graphics problems with your game using DirectX Tools](http://channel9.msdn.com/Events/GDC/GDC-2015/Solve-the-Tough-Graphics-Problems-with-your-Game-Using-DirectX-Tools)</td>
    </tr>
    <tr>
        <td>Visual Studio 2015 tools for debugging DirectX 12 (video)</td>
        <td>[DirectX tools for Windows 10 in Visual Studio 2015](https://channel9.msdn.com/Series/ConnectOn-Demand/212)</td>
    </tr>
    <tr>
        <td>Direct3D 12 programming guide</td>
        <td>[Direct3D 12 Programming Guide](https://msdn.microsoft.com/library/windows/desktop/dn903821)</td>
    </tr>
    <tr>
        <td>Combining DirectX and XAML</td>
        <td>[DirectX and XAML interop](directx-and-xaml-interop.md)</td>
    </tr>
</table>

### Globalization and localization

Develop world-ready games for the Windows platform and learn about the international features built into Microsoft’s top products.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Preparing your game for the global market</td>
        <td>[Guidelines when developing for a global audience](https://msdn.microsoft.com/library/windows/apps/xaml/mt186453.aspx)</td>
    </tr>
    <tr>
        <td>Bridging languages, cultures, and technology</td>
        <td>[Online resource for language conventions and standard Microsoft terminology](http://www.microsoft.com/Language/Default.aspx)</td>
    </tr>
</table>

### Security

Create an environment where your gamers can play and compete fairly. A game enrolled in TruePlay runs in a protected process which mitigates a class of common attacks. The game monitoring system also helps to identify common cheating scenarios. 

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Tools to combat cheating within PC games</td>
        <td>[TruePlay](https://msdn.microsoft.com/library/windows/desktop/mt808781)</td>
    </tr>
</table>

## Submitting and publishing your game

The following guides and information help make the publishing and submission process as smooth as possible.

### Publishing

You'll use the new unified Windows Dev Center dashboard to publish and manage your game packages.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Windows Dev Center app publishing</td>
        <td>[Publish Windows apps](https://dev.windows.com/publish)</td>
    </tr>
    <tr>
        <td>Windows Dev Center advanced publishing (GDN)</td>
        <td>[Windows Dev Center Dashboard advanced publishing guide](https://developer.xboxlive.com/en-us/windows/documentation/Pages/home.aspx)</td>
    </tr>
    <tr>
        <td>Use Azure Active Directory (AAD) to add users to your Dev Center account</td>
        <td>[Manage account users](https://docs.microsoft.com/windows/uwp/publish/manage-account-users)</td>
    </tr>   
    <tr>
        <td>Rating your game (blog post)</td>
        <td>[Single workflow to assign age ratings using IARC system](https://blogs.windows.com/buildingapps/2016/01/06/now-available-single-age-rating-system-to-simplify-app-submissions/)</td>
    </tr>
</table>

#### Packaging and uploading

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Learn to use streaming install and optional packages (video)</td>
        <td>[Nextgen UWP app distribution: Building extensible, stream-able, componentized apps](https://channel9.msdn.com/Events/Build/2017/B8093)</td>
    </tr>
    <tr>
        <td>Divide and group content to enable streaming install</td>
        <td>[UWP App Streaming install](../packaging/streaming-install.md)</td>
    </tr>
    <tr>
        <td>Create optional packages like DLC game content</td>
        <td>[Optional packages and related set authoring](../packaging/optional-packages.md)</td>
    </tr>
    <tr>
        <td>Package your UWP game</td>
        <td>[Packaging apps](../packaging/index.md)</td>
    </tr>
    <tr>
        <td>Package your UWP DirectX game</td>
        <td>[Package your UWP DirectX game](package-your-windows-store-directx-game.md)</td>
    </tr>
    <tr>
        <td>Packaging your game as a 3rd party developer (blog post)</td>
        <td>[Create uploadable packages without publisher's store account access](https://blogs.windows.com/buildingapps/2015/12/15/building-an-app-for-a-3rd-party-how-to-package-their-store-app/)</td>
    </tr>
    <tr>
        <td>Creating app packages and app package bundles using MakeAppx</td>
        <td>[Create packages using app packager tool MakeAppx.exe](https://docs.microsoft.com/windows/uwp/packaging/create-app-package-with-makeappx-tool)</td>
    </tr>
    <tr>
        <td>Signing your files digitally using SignTool</td>
        <td>[Sign files and verify signatures in files using SignTool](https://msdn.microsoft.com/library/windows/desktop/aa387764)</td>
    </tr>    
    <tr>
        <td>Uploading and versioning your game</td>
        <td>[Upload app packages](https://msdn.microsoft.com/library/windows/apps/mt148542)</td>
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
        <td>[App Developer Agreement](https://msdn.microsoft.com/library/windows/apps/hh694058)</td>
    </tr>
    <tr>
        <td>Policies for publishing apps in the Microsoft Store</td>
        <td>[Microsoft Store Policies](https://msdn.microsoft.com/library/windows/apps/dn764944)</td>
    </tr>
    <tr>
        <td>How to avoid some common app certification issues</td>
        <td>[Avoid common certification failures](https://msdn.microsoft.com/library/windows/apps/jj657968)</td>
    </tr>
</table>
 

### Store manifest (StoreManifest.xml)

The store manifest (StoreManifest.xml) is an optional configuration file that can be included in your app package. The store manifest provides additional features that are not part of the AppxManifest.xml file. For example, you can use the store manifest to block installation of your game if a target device doesn't have the specified minimum DirectX feature level, or the specified minimum system memory.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Store manifest schema</td>
        <td>[StoreManifest schema (Windows 10)](https://msdn.microsoft.com/library/windows/apps/mt617335)</td>
    </tr>
</table>
 

## Game lifecycle management


After you've finished development and shipped your game, it's not "game over". You may be done with development on version one, but your game's journey in the marketplace has only just begun. You'll want to monitor usage and error reporting, respond to user feedback, and publish updates to your game.

### Windows Dev Center analytics and promotion

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Dev Center App</td>
        <td>[Dev Center Windows 10 app to view performance of your published apps](https://www.microsoft.com/store/apps/dev-center/9nblggh4r5ws)</td>
    </tr>  
    <tr>
        <td>Windows Dev Center analytics</td>
        <td>[Analytics](https://msdn.microsoft.com/library/windows/apps/mt148522)</td>
    </tr>
    <tr>
        <td>Responding to customer reviews</td>
        <td>[Respond to customer reviews](https://msdn.microsoft.com/library/windows/apps/mt148546)</td>
    </tr>
    <tr>
        <td>Ways to promote your game</td>
        <td>[Promote your apps](https://dev.windows.com/store-promotion)</td>
    </tr>
</table>
 

### Visual Studio Application Insights

Visual Studio Application Insights provides performance, telemetry, and usage analytics for your published game. Application Insights helps you detect and solve issues after your game is released, continuously monitor and improve usage, and understand how players are continuing to interact with your game. Application Insights works by adding an SDK into your app, which sends telemetry to the [Azure portal](http://portal.azure.com/).

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Application performance and usage analytics</td>
        <td>[Visual Studio Application Insights](https://azure.microsoft.com/documentation/articles/app-insights-get-started/)</td>
    </tr>
    <tr>
        <td>Enable Application Insights in Windows apps</td>
        <td>[Application Insights for Windows Phone and Store apps](https://azure.microsoft.com/documentation/articles/app-insights-windows-get-started/)</td>
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
        <td>[GameAnalytics](http://www.gameanalytics.com/)</td>
    </tr>
    <tr>
        <td>Connect your UWP game to Google Analytics</td>
        <td>[Get Windows SDK for Google Analytics](https://github.com/dotnet/windows-sdk-for-google-analytics)</td>
    </tr>
    <tr>
        <td>Learn how to use Windows SDK for Google Analytics (video)</td>
        <td>[Getting started with Windows SDK for Google Analytics](https://channel9.msdn.com/Events/Windows/Windows-Developer-Day-Creators-Update/Getting-started-with-the-Windows-SDK-for-Google-Analytics)</td>
    </tr>    
    <tr>
        <td>Use Facebook App Installs Ads to promote your game to Facebook users</td>
        <td>[Get Windows SDK for Facebook](https://github.com/Microsoft/winsdkfb)</td>
    </tr>
    <tr>
        <td>Learn how to use Facebook App Installs Ads (video)</td>
        <td>[Getting started with Windows SDK for Facebook](https://channel9.msdn.com/Events/Windows/Windows-Developer-Day-Creators-Update/Getting-started-with-Facebook-App-Install-Ads)</td>
    </tr>
    <tr>
        <td>Use Vungle to add video ads into your games</td>
        <td>[Get Windows SDK for Vungle](https://v.vungle.com/sdk)</td>
    </tr>
</table>
 

### Creating and managing content updates

To update your published game, submit a new app package with a higher version number. After the package makes its way through submission and certification, it will automatically be available to customers as an update.

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Updating and versioning your game</td>
        <td>[Package version numbering](https://msdn.microsoft.com/library/windows/apps/mt188602)</td>
    </tr>
    <tr>
        <td>Game package management guidance</td>
        <td>[Guidance for app package management](https://msdn.microsoft.com/library/windows/apps/mt188602)</td>
    </tr>
</table>


## Adding Xbox Live to your game

Xbox Live is a premier gaming network that connects millions of gamers across the world. Developers gain access to Xbox Live features that can organically grow their game’s audience, including Xbox Live presence, Leaderboards, Cloud Saves, Game Hubs, Clubs, Party Chat, Game DVR, and more.

> [!Note]
> If you would like to develop Xbox Live enabled titles, there are several options are available to you. For info about the various programs, see [Developer program overview](../xbox-live/developer-program-overview.md).

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Xbox Live overview</td>
        <td>[Xbox Live developer guide](../xbox-live/index.md)</td>
    </tr>
    <tr>
        <td>Understand which features are available depending on program</td>
        <td>[Developer program overview: Feature table](../xbox-live/developer-program-overview.md#feature-table)</td>
    </tr>
    <tr>
        <td>Links to useful resources for developing Xbox Live games</td>
        <td>[Xbox Live resources](../xbox-live/xbox-live-resources.md)</td>
    </tr>
    <tr>
        <td>Learn how to get info from Xbox Live services</td>
        <td>[Introduction to Xbox Live APIs](../xbox-live/introduction-to-xbox-live-apis.md)</td>
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
        <td>[Get started with the Xbox Live Creators Program](../xbox-live/get-started-with-creators/get-started-with-xbox-live-creators.md)</td>
    </tr>
    <tr>
        <td>Add Xbox Live to your game</td>
        <td>[Step by step guide to integrate Xbox Live Creators Program](../xbox-live/get-started-with-creators/creators-step-by-step-guide.md)</td>
    </tr>
    <tr>
        <td>Add Xbox Live to your UWP game created using Unity</td>
        <td>[Get started developing an Xbox Live Creators Program title with the Unity game engine](../xbox-live/get-started-with-creators/develop-creators-title-with-unity.md)</td>
    </tr>
    <tr>
        <td>Set up your development sandbox</td>
        <td>[Xbox Live sandboxes introduction](../xbox-live/get-started-with-creators/xbox-live-sandboxes-creators.md)</td>
    </tr>
    <tr>
        <td>Set up accounts for testing</td>
        <td>[Authorize Xbox Live accounts in your test environment](../xbox-live/get-started-with-creators/authorize-xbox-live-accounts.md)</td>
    </tr>
    <tr>
        <td>Samples for Xbox Live Creators Program</td>
        <td>[Code samples for Creators Program developers](https://github.com/Microsoft/xbox-live-samples/tree/master/Samples/CreatorsSDK)</td>
    </tr>
    <tr>
        <td>Learn how to integrate cross-platform Xbox Live experiences in UWP games (video)</td>
        <td>[Xbox Live Creators Program](https://channel9.msdn.com/Events/GDC/GDC-2017/GDC2017-005)</td>
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
        <td>[Get started with Xbox Live as a managed partner or an ID developer](../xbox-live/get-started-with-partner/get-started-with-xbox-live-partner.md)</td>
    </tr>
    <tr>
        <td>Add Xbox Live to your game</td>
        <td>[Step by step guide to integrate Xbox Live for managed partners and ID members](../xbox-live/get-started-with-partner/partners-step-by-step-guide.md)</td>
    </tr>
    <tr>
        <td>Add Xbox Live to your UWP game created using Unity</td>
        <td>[Add Xbox Live support to Unity for UWP with IL2CPP scripting backend for ID and managed partners](../xbox-live/get-started-with-partner/partner-unity-uwp-il2cpp.md)</td>
    </tr>
    <tr>
        <td>Set up your development sandbox</td>
        <td>[Advanced Xbox Live sandboxes](../xbox-live/get-started-with-partner/advanced-xbox-live-sandboxes.md)</td>
    </tr>
    <tr>
        <td>Requirements for games that use Xbox Live (GDN)</td>
        <td>[Xbox Requirements for Xbox Live on Windows 10](http://go.microsoft.com/fwlink/?LinkId=533217)</td>
    </tr>
    <tr>
        <td>Samples</td>
        <td>[Code samples for ID@Xbox developers](https://github.com/Microsoft/xbox-live-samples/tree/master/Samples/ID%40XboxSDK)</td>
    </tr>  
    <tr>
        <td>Overview of Xbox Live game development (video)</td>
        <td>[Developing with Xbox Live for Windows 10](http://channel9.msdn.com/Events/GDC/GDC-2015/Developing-with-Xbox-Live-for-Windows-10)</td>
    </tr>
    <tr>
        <td>Cross-platform matchmaking (video)</td>
        <td>[Xbox Live Multiplayer: Introducing services for cross-platform matchmaking and gameplay](http://channel9.msdn.com/Events/GDC/GDC-2015/Xbox-Live-Multiplayer-Introducing-services-for-cross-platform-matchmaking-and-gameplay)</td>
    </tr>
    <tr>
        <td>Cross-device gameplay in Fable Legends (video)</td>
        <td>[Fable Legends: Cross-device Gameplay with Xbox Live](http://channel9.msdn.com/Events/GDC/GDC-2015/Fable-Legends-Cross-device-Gameplay-with-Xbox-Live)</td>
    </tr>
    <tr>
        <td>Xbox Live stats and achievements (video)</td>
        <td>[Best Practices for Leveraging Cloud-Based User Stats and Achievements in Xbox Live](http://channel9.msdn.com/Events/GDC/GDC-2015/Best-Practices-for-Leveraging-Cloud-Based-User-Stats-and-Achievements-in-Xbox-Live)</td>
    </tr>
</table>


## Additional resources

<table>
    <colgroup>
    <col width="50%" />
    <col width="50%" />
    </colgroup>
    <tr>
        <td>Game development videos</td>
        <td>[Videos from major conferences like GDC and //build](https://docs.microsoft.com/windows/uwp/gaming/game-development-videos)</td>
    </tr>
    <tr>
        <td>Indie game development (video)</td>
        <td>[New Opportunities for Independent Developers](http://channel9.msdn.com/Events/GDC/GDC-2015/New-Opportunities-for-Independent-Developers)</td>
    </tr>
    <tr>
        <td>Considerations for multi-core mobile devices (video)</td>
        <td>[Sustained Gaming Performance in multi-core mobile devices](http://channel9.msdn.com/Events/GDC/GDC-2015/Sustained-gaming-performance-in-multi-core-mobile-devices)</td>
    </tr>
    <tr>
        <td>Developing Windows 10 desktop games (video)</td>
        <td>[PC Games for Windows 10](http://channel9.msdn.com/Events/GDC/GDC-2015/PC-Games-for-Windows-10)</td>
    </tr>
</table>



 

 

 
