---
title: How to disable mouse mode
description: Learn how to turn off the default mouse mode in HTML and XAML/C# Universal Windows Platform (UWP) applications.
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.assetid: e57ee4e6-7807-4943-a933-c2b4dc80fc01
ms.localizationpriority: medium
---
# How to disable mouse mode
Mouse mode is on by default for all applications, which means that all applications that have not opted out will receive a mouse pointer (similar to the one in the Edge browser on the console). We strongly recommend that you turn this off and optimize for directional controller navigation.   
   
## HTML   
To turn on directional controller navigation in a JavaScript Universal Windows Platform (UWP) app, use the [TVHelpers directional navigation](https://github.com/Microsoft/TVHelpers/wiki/Using-DirectionalNavigation) JavaScript library. Include the directional navigation JavaScript file in your app package, and add a reference to it in all of the HTML pages that require directional controller navigation:

```code
<script src="directionalnavigation-1.0.0.0.js"></script>
```
For more details, see the [directional navigation wiki](https://github.com/Microsoft/TVHelpers/wiki/Using-DirectionalNavigation).

If you instead want to turn off mouse mode and use the DOM or WinRT gamepad APIs directly, run the following for every page that requires it: 
   
```code
navigator.gamepadInputEmulation = "gamepad";
```   

   This property defaults to `mouse`, which enables mouse mode. Setting it to `keyboard` turns off mouse mode, and instead gamepad input generates DOM keyboard events. Setting it to `gamepad` turns off mouse mode and does not generate DOM keyboard events, and allows you to just use the DOM or WinRT gamepad APIs.

## XAML    
To turn off mouse mode, add the following to the constructor for your app:   
   
```code
public App() {
        this.InitializeComponent();
        this.RequiresPointerMode = Windows.UI.Xaml.ApplicationRequiresPointerMode.WhenRequested;
        this.Suspending += OnSuspending;
}
```

## C++/DirectX   
If you are writing a C++/DirectX app, there's nothing to do. Mouse mode only applies to HTML and XAML applications.

## See also
- [Best practices for Xbox](tailoring-for-xbox.md)
- [UWP on Xbox One](index.md)

