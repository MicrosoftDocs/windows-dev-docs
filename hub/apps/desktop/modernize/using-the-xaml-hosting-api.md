---
description: This article describes how to host UWP XAML UI in your desktop C++ Win32 app.
title: Using the UWP XAML hosting API in a C++ Win32 app
ms.date: 01/24/2020
ms.topic: article
keywords: windows 10, uwp, windows forms, wpf, win32, xaml islands
ms.author: mcleans
author: mcleanbyron
ms.localizationpriority: medium
ms.custom: 19H1
---

# Using the UWP XAML hosting API in a C++ Win32 app

Starting in Windows 10, version 1903, non-UWP desktop apps (including C++ Win32, WPF, and Windows Forms apps) can use the *UWP XAML hosting API* to host UWP controls in any UI element that is associated with a window handle (HWND). This API enables non-UWP desktop apps to use the latest Windows 10 UI features that are only available via UWP controls. For example, non-UWP desktop apps can use this API to host UWP controls that use the [Fluent Design System](/windows/uwp/design/fluent-design-system/index) and support [Windows Ink](/windows/uwp/design/input/pen-and-stylus-interactions).

The UWP XAML hosting API provides the foundation for a broader set of controls that we are providing to enable developers to bring Fluent UI to non-UWP desktop apps. This feature is called *XAML Islands*. For an overview of this feature, see [Host UWP XAML controls in desktop apps (XAML Islands)](xaml-islands.md).

> [!NOTE]
> If you have feedback about XAML Islands, create a new issue in the [Microsoft.Toolkit.Win32 repo](https://github.com/windows-toolkit/Microsoft.Toolkit.Win32/issues) and leave your comments there. If you prefer to submit your feedback privately, you can send it to XamlIslandsFeedback@microsoft.com. Your insights and scenarios are critically important to us.

## Is the UWP XAML hosting API the right choice for your desktop app?

The UWP XAML hosting API provides the low-level infrastructure for hosting UWP controls in desktop apps. Some types of desktop apps have the option of using alternative, more convenient APIs to accomplish this goal.

* If you have a C++ Win32 desktop app and you want to host UWP controls in your app, you must use the UWP XAML hosting API. There are no alternatives for these types of apps.

* For WPF and Windows Forms apps, we strongly recommend that you use the [XAML Island .NET controls](xaml-islands.md#wpf-and-windows-forms-applications) in the Windows Community Toolkit instead of using the UWP XAML hosting API directly. These controls use the UWP XAML hosting API internally and implement all of the behavior you would otherwise need to handle yourself if you used the UWP XAML hosting API directly, including keyboard navigation and layout changes.

Because we recommend that only C++ Win32 apps use the UWP XAML hosting API, this article primarily provides instructions and examples for C++ Win32 apps. However, you can use the UWP XAML hosting API in WPF and Windows Forms apps if you choose. This article points to relevant source code for the [host controls](xaml-islands.md#host-controls) for WPF and Windows Forms in the Windows Community Toolkit so you can see how the UWP XAML hosting API is used by those controls.

## Prerequisites

XAML Islands require Windows 10, version 1903 (or later) and the corresponding build of the Windows SDK. To use XAML Islands in your C++ Win32 app, you must set up your project as described in [this section](#configure-the-project).

## Architecture of the API

The UWP XAML hosting API includes these main Windows Runtime types and COM interfaces.

|  Type or interface | Description |
|--------------------|-------------|
| [WindowsXamlManager](https://docs.microsoft.com/uwp/api/windows.ui.xaml.hosting.windowsxamlmanager) | This class represents the UWP XAML framework. This class provides a single static [InitializeForCurrentThread](https://docs.microsoft.com/uwp/api/windows.ui.xaml.hosting.windowsxamlmanager.initializeforcurrentthread) method that initializes the UWP XAML framework on the current thread in the desktop app. |
| [DesktopWindowXamlSource](https://docs.microsoft.com/uwp/api/windows.ui.xaml.hosting.desktopwindowxamlsource) | This class represents an instance of UWP XAML content that you are hosting in your desktop app. The most important member of this class is the [Content](https://docs.microsoft.com/uwp/api/windows.ui.xaml.hosting.desktopwindowxamlsource.content) property. You assign this property to a [Windows.UI.Xaml.UIElement](https://docs.microsoft.com/uwp/api/windows.ui.xaml.uielement) that you want to host. This class also has other members for routing keyboard focus navigation into and out of the XAML Islands. |
| IDesktopWindowXamlSourceNative | This COM interface provides the **AttachToWindow** method, which you use to attach a XAML Island in your app to a parent UI element. Every **DesktopWindowXamlSource** object implements this interface. |
| IDesktopWindowXamlSourceNative2 | This COM interface provides the **PreTranslateMessage** method, which enables the UWP XAML framework to process certain Windows messages correctly. Every **DesktopWindowXamlSource** object implements this interface. |

The following diagram illustrates the hierarchy of objects in a XAML Island that is hosted in a desktop app.

* At the base level is the UI element in your app where you want to host the XAML Island. This UI element must have a window handle (HWND). Examples of UI elements in which you can host a XAML Island include a [window](https://docs.microsoft.com/windows/desktop/winmsg/about-windows) for C++ Win32 apps, a [System.Windows.Interop.HwndHost](https://docs.microsoft.com/dotnet/api/system.windows.interop.hwndhost) for WPF apps, and a [System.Windows.Forms.Control](https://docs.microsoft.com/dotnet/api/system.windows.forms.control) for Windows Forms apps.

* At the next level is a **DesktopWindowXamlSource** object. This object provides the infrastructure for hosting the XAML Island. Your code is responsible for creating this object and attaching it to the parent UI element.

* When you create a **DesktopWindowXamlSource**, this object automatically creates a native child window to host your UWP control. This native child window is mostly abstracted away from your code, but you can access its handle (HWND) if necessary.

* Finally, at the top level is the UWP control you want to host in your desktop app. This can be any UWP object that derives from [Windows.UI.Xaml.UIElement](https://docs.microsoft.com/uwp/api/windows.ui.xaml.uielement), including any UWP control provided by the Windows SDK as well as custom user controls.

![DesktopWindowXamlSource architecture](images/xaml-islands/xaml-hosting-api-rev2.png)

## Related samples

The way you use the UWP XAML hosting API in your code depends on your app type, the design of your app, and other factors. To help illustrate how to use this API in the context of a complete app, this article refers to code from the following samples.

### C++ Win32

The following samples demonstrate how to use the UWP XAML hosting API in a C++ Win32 app:

* [Simple XAML Island sample](https://github.com/microsoft/Xaml-Islands-Samples/tree/master/Standalone_Samples/CppWinRT_Basic_Win32App). This sample demonstrates a basic implementation of hosting a UWP control in an unpackaged C++ Win32 app (that is, an app that is not built into an MSIX package).

* [XAML Island with custom control sample](https://github.com/microsoft/Xaml-Islands-Samples/tree/master/Samples/Win32). This sample demonstrates a complete implementation of hosting a custom UWP control in a packaged C++ Win32 app, as well as handling other behavior such as keyboard input and focus navigation.

### WPF and Windows Forms

The [WindowsXamlHost](https://docs.microsoft.com/windows/communitytoolkit/controls/wpf-winforms/windowsxamlhost) control in the Windows Community Toolkit serves as a reference sample for using the UWP hosting API in WPF and Windows Forms apps. The source code is available at the following locations:

* For the WPF version of the control, [go here](https://github.com/windows-toolkit/Microsoft.Toolkit.Win32/tree/master/Microsoft.Toolkit.Wpf.UI.XamlHost). The WPF version derives from [System.Windows.Interop.HwndHost](https://docs.microsoft.com/dotnet/api/system.windows.interop.hwndhost).

* For the Windows Forms version of the control, [go here](https://github.com/windows-toolkit/Microsoft.Toolkit.Win32/tree/master/Microsoft.Toolkit.Forms.UI.XamlHost). The Windows Forms version derives from [System.Windows.Forms.Control](https://docs.microsoft.com/dotnet/api/system.windows.forms.control).

> [!NOTE]
> We strongly recommend that you use the [XAML Island .NET controls](xaml-islands.md#wpf-and-windows-forms-applications) in the Windows Community Toolkit instead of using the UWP XAML hosting API directly in WPF and Windows Forms apps. The WPF and Windows Forms sample links in this article are for illustrative purposes only.

## Host a standard UWP control

This section walks you through the process of using the UWP XAML hosting API to host a standard UWP control (that is, a control provided by the Windows SDK or WinUI library) in a new C++ Win32 app. The code is based on the [simple XAML Island sample](https://github.com/microsoft/Xaml-Islands-Samples/tree/master/Standalone_Samples/CppWinRT_Basic_Win32App), and this section discusses some of the most important parts of the code. If you have an existing C++ Win32 app project, you can adapt these steps and code examples for your project.

### Configure the project

1. In Visual Studio 2019 with the Windows 10, version 1903 SDK (version 10.0.18362) or a later release installed, create a new **Windows Desktop Application** project. This project is available under the **C++**, **Windows**, and **Desktop** project filters.

2. In **Solution Explorer**, right-click the solution node, click **Retarget solution**, select the **10.0.18362.0** or a later SDK release, and then click **OK**.

3. Install the [Microsoft.Windows.CppWinRT](https://www.nuget.org/packages/Microsoft.Windows.CppWinRT/) NuGet package to enable support for [C++/WinRT](/windows/uwp/cpp-and-winrt-apis) in your project:

    1. Right-click your project in **Solution Explorer** and choose **Manage NuGet Packages**.
    2. Select the **Browse** tab, search for the [Microsoft.Windows.CppWinRT](https://www.nuget.org/packages/Microsoft.Windows.CppWinRT/) package, and install the latest version of this package.

    > [!NOTE]
    > For new projects, you can alternatively install the [C++/WinRT Visual Studio Extension (VSIX)](https://marketplace.visualstudio.com/items?itemName=CppWinRTTeam.cppwinrt101804264) and use one of the C++/WinRT project templates included in that extension. For more details, see [this article](/windows/uwp/cpp-and-winrt-apis/intro-to-using-cpp-with-winrt#visual-studio-support-for-cwinrt-xaml-the-vsix-extension-and-the-nuget-package).

4. Install the [Microsoft.Toolkit.Win32.UI.SDK](https://www.nuget.org/packages/Microsoft.Toolkit.Win32.UI.SDK) NuGet package:

    1. In the **NuGet Package Manager** window, make sure that **Include prerelease** is selected.
    2. Select the **Browse** tab, search for the **Microsoft.Toolkit.Win32.UI.SDK** package, and install version v6.0.0 (or later) of this package. This package provides several build and run time assets that enable XAML Islands to work in your app.

5. Set the `maxVersionTested` value in your [assembly manifest](https://docs.microsoft.com/windows/desktop/SbsCs/application-manifests) to specify that your application is compatible with Windows 10, version 1903 or later.

    1. If you don't already have an assembly manifest in your project, add a new XML file to your project and name it **app.manifest**.
    2. In your assembly manifest, include the **compatibility** element and the child elements shown in the following example. Replace the **Id** attribute of the **maxVersionTested** element with the version number of Windows 10 you are targeting (this must be Windows 10, version 1903 or a later release).

        ```xml
        <?xml version="1.0" encoding="UTF-8"?>
        <assembly xmlns="urn:schemas-microsoft-com:asm.v1" manifestVersion="1.0">
            <compatibility xmlns="urn:schemas-microsoft-com:compatibility.v1">
                <application>
                    <!-- Windows 10 -->
                    <maxversiontested Id="10.0.18362.0"/>
                    <supportedOS Id="{8e0f7a12-bfb3-4fe8-b9a5-48fd50a15a9a}" />
                </application>
            </compatibility>
        </assembly>
        ```

### Use the XAML hosting API to host a UWP control

The basic process of using the XAML hosting API to host a UWP control follows these general steps:

1. Initialize the UWP XAML framework for the current thread before your app creates any of the [Windows.UI.Xaml.UIElement](https://docs.microsoft.com/uwp/api/windows.ui.xaml.uielement) objects that it will host. There are several ways to do this, depending on when you plan to create the [DesktopWindowXamlSource](https://docs.microsoft.com/uwp/api/windows.ui.xaml.hosting.desktopwindowxamlsource) object that will host the controls.

    * If your application creates the **DesktopWindowXamlSource** object before it creates any of the **Windows.UI.Xaml.UIElement** objects that it will host, this framework will be initialized for you when you instantiate the **DesktopWindowXamlSource** object. In this scenario, you don't need to add any code of your own to initialize the framework.

    * However, if your application creates the **Windows.UI.Xaml.UIElement** objects before it creates the **DesktopWindowXamlSource** object that will host them, your application must call the static [WindowsXamlManager.InitializeForCurrentThread](https://docs.microsoft.com/uwp/api/windows.ui.xaml.hosting.windowsxamlmanager.initializeforcurrentthread) method to explicitly initialize the UWP XAML framework before the **Windows.UI.Xaml.UIElement** objects are instantiated. Your application should typically call this method when the parent UI element that hosts the **DesktopWindowXamlSource** is instantiated.

    > [!NOTE]
    > This method returns a [WindowsXamlManager](https://docs.microsoft.com/uwp/api/windows.ui.xaml.hosting.windowsxamlmanager) object that contains a reference to the UWP XAML framework. You can create as many **WindowsXamlManager** objects as you want on a given thread. However, because each object holds a reference to the UWP XAML framework, you should dispose the objects to ensure that XAML resources are eventually released.

2. Create a [DesktopWindowXamlSource](https://docs.microsoft.com/uwp/api/windows.ui.xaml.hosting.desktopwindowxamlsource) object and attach it to a parent UI element in your application that is associated with a window handle.

    To do this, you'll need to follow these steps:

    1. Create a **DesktopWindowXamlSource** object and cast it to the **IDesktopWindowXamlSourceNative** or **IDesktopWindowXamlSourceNative2** COM interface.
        > [!NOTE]
        > These interfaces are declared in the **windows.ui.xaml.hosting.desktopwindowxamlsource.h** header file in the Windows SDK. By default, this file is in %programfiles(x86)%\Windows Kits\10\Include\\<build number\>\um.

    2. Call the **AttachToWindow** method of the **IDesktopWindowXamlSourceNative** or **IDesktopWindowXamlSourceNative2** interface, and pass in the window handle of the parent UI element in your application.

    3. Set the initial size of the internal child window contained in the **DesktopWindowXamlSource**. By default, this internal child window is set to a width and height of 0. If you don't set the size of the window, any UWP controls you add to the **DesktopWindowXamlSource** will not be visible. To access the internal child window in the **DesktopWindowXamlSource**, use the **WindowHandle** property of the **IDesktopWindowXamlSourceNative** or **IDesktopWindowXamlSourceNative2** interface.

3. Finally, assign the **Windows.UI.Xaml.UIElement** you want to host to the [Content](https://docs.microsoft.com/uwp/api/windows.ui.xaml.hosting.desktopwindowxamlsource.content) property of your **DesktopWindowXamlSource** object.

The following steps and code examples demonstrate how to do implement the above process:

1. In the **Source Files** folder of the project, open the default **WindowsProject.cpp** file. Delete the entire contents of the file and add the following `include` and `using` statements. In addition to standard C++ and UWP headers and namespaces, these statements include several items specific to XAML Islands.

    ```cppwinrt
    #include <windows.h>
    #include <stdlib.h>
    #include <string.h>

    #include <winrt/Windows.Foundation.Collections.h>
    #include <winrt/Windows.system.h>
    #include <winrt/windows.ui.xaml.hosting.h>
    #include <windows.ui.xaml.hosting.desktopwindowxamlsource.h>
    #include <winrt/windows.ui.xaml.controls.h>
    #include <winrt/Windows.ui.xaml.media.h>

    using namespace winrt;
    using namespace Windows::UI;
    using namespace Windows::UI::Composition;
    using namespace Windows::UI::Xaml::Hosting;
    using namespace Windows::Foundation::Numerics;
    ```

3. Copy the following code after the previous section. This code defines the **WinMain** function for the app. This function initializes a basic window and uses the XAML hosting API to host a simple UWP **TextBlock** control in the window.

    ```cppwinrt
    LRESULT CALLBACK WindowProc(HWND, UINT, WPARAM, LPARAM);

    HWND _hWnd;
    HWND _childhWnd;
    HINSTANCE _hInstance;

    int CALLBACK WinMain(_In_ HINSTANCE hInstance, _In_opt_ HINSTANCE hPrevInstance, _In_ LPSTR lpCmdLine, _In_ int nCmdShow)
    {
        _hInstance = hInstance;

        // The main window class name.
        const wchar_t szWindowClass[] = L"Win32DesktopApp";
        WNDCLASSEX windowClass = { };

        windowClass.cbSize = sizeof(WNDCLASSEX);
        windowClass.lpfnWndProc = WindowProc;
        windowClass.hInstance = hInstance;
        windowClass.lpszClassName = szWindowClass;
        windowClass.hbrBackground = (HBRUSH)(COLOR_WINDOW + 1);

        windowClass.hIconSm = LoadIcon(windowClass.hInstance, IDI_APPLICATION);

        if (RegisterClassEx(&windowClass) == NULL)
        {
            MessageBox(NULL, L"Windows registration failed!", L"Error", NULL);
            return 0;
        }

        _hWnd = CreateWindow(
            szWindowClass,
            L"Windows c++ Win32 Desktop App",
            WS_OVERLAPPEDWINDOW | WS_VISIBLE,
            CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT,
            NULL,
            NULL,
            hInstance,
            NULL
        );
        if (_hWnd == NULL)
        {
            MessageBox(NULL, L"Call to CreateWindow failed!", L"Error", NULL);
            return 0;
        }


        // Begin XAML Island section.

        // The call to winrt::init_apartment initializes COM; by default, in a multithreaded apartment.
        winrt::init_apartment(apartment_type::single_threaded);

        // Initialize the XAML framework's core window for the current thread.
        WindowsXamlManager winxamlmanager = WindowsXamlManager::InitializeForCurrentThread();

        // This DesktopWindowXamlSource is the object that enables a non-UWP desktop application 
        // to host UWP controls in any UI element that is associated with a window handle (HWND).
        DesktopWindowXamlSource desktopSource;

        // Get handle to the core window.
        auto interop = desktopSource.as<IDesktopWindowXamlSourceNative>();

        // Parent the DesktopWindowXamlSource object to the current window.
        check_hresult(interop->AttachToWindow(_hWnd));

        // This HWND will be the window handler for the XAML Island: A child window that contains XAML.  
        HWND hWndXamlIsland = nullptr;

        // Get the new child window's HWND. 
        interop->get_WindowHandle(&hWndXamlIsland);

        // Update the XAML Island window size because initially it is 0,0.
        SetWindowPos(hWndXamlIsland, 0, 200, 100, 800, 200, SWP_SHOWWINDOW);

        // Create the XAML content.
        Windows::UI::Xaml::Controls::StackPanel xamlContainer;
        xamlContainer.Background(Windows::UI::Xaml::Media::SolidColorBrush{ Windows::UI::Colors::LightGray() });

        Windows::UI::Xaml::Controls::TextBlock tb;
        tb.Text(L"Hello World from Xaml Islands!");
        tb.VerticalAlignment(Windows::UI::Xaml::VerticalAlignment::Center);
        tb.HorizontalAlignment(Windows::UI::Xaml::HorizontalAlignment::Center);
        tb.FontSize(48);

        xamlContainer.Children().Append(tb);
        xamlContainer.UpdateLayout();
        desktopSource.Content(xamlContainer);

        // End XAML Island section.

        ShowWindow(_hWnd, nCmdShow);
        UpdateWindow(_hWnd);

        //Message loop:
        MSG msg = { };
        while (GetMessage(&msg, NULL, 0, 0))
        {
            TranslateMessage(&msg);
            DispatchMessage(&msg);
        }

        return 0;
    }
    ```

4. Copy the following code after the previous section. This code defines the [window procedure](https://docs.microsoft.com/windows/win32/learnwin32/writing-the-window-procedure) for the window.

    ```cppwinrt
    LRESULT CALLBACK WindowProc(HWND hWnd, UINT messageCode, WPARAM wParam, LPARAM lParam)
    {
        PAINTSTRUCT ps;
        HDC hdc;
        wchar_t greeting[] = L"Hello World in Win32!";
        RECT rcClient;

        switch (messageCode)
        {
            case WM_PAINT:
                if (hWnd == _hWnd)
                {
                    hdc = BeginPaint(hWnd, &ps);
                    TextOut(hdc, 300, 5, greeting, wcslen(greeting));
                    EndPaint(hWnd, &ps);
                }
                break;
            case WM_DESTROY:
                PostQuitMessage(0);
                break;

            // Create main window
            case WM_CREATE:
                _childhWnd = CreateWindowEx(0, L"ChildWClass", NULL, WS_CHILD | WS_BORDER, 0, 0, 0, 0, hWnd, NULL, _hInstance, NULL);
                return 0;

            // Main window changed size
            case WM_SIZE:
                // Get the dimensions of the main window's client
                // area, and enumerate the child windows. Pass the
                // dimensions to the child windows during enumeration.
                GetClientRect(hWnd, &rcClient);
                MoveWindow(_childhWnd, 200, 200, 400, 500, TRUE);
                ShowWindow(_childhWnd, SW_SHOW);

                return 0;

                // Process other messages.

            default:
                return DefWindowProc(hWnd, messageCode, wParam, lParam);
                break;
        }

        return 0;
    }
    ```

5. Save the code file, and build and run the app. Confirm that you see the UWP **TextBlock** control in the app window.
    > [!NOTE]
    > You may see the several build warnings, including `warning C4002:  too many arguments for function-like macro invocation 'GetCurrentTime'` and `manifest authoring warning 81010002: Unrecognized Element "maxversiontested" in namespace "urn:schemas-microsoft-com:compatibility.v1"`. These warnings are known issues with the current tools and NuGet packages, and they can be ignored.

For complete examples that demonstrate these tasks, see the following code files:

* **C++ Win32:**
  * See the [HelloWindowsDesktop.cpp](https://github.com/microsoft/Xaml-Islands-Samples/blob/master/Standalone_Samples/CppWinRT_Basic_Win32App/Win32DesktopApp/HelloWindowsDesktop.cpp) file.
  * See the [XamlBridge.cpp](https://github.com/microsoft/Xaml-Islands-Samples/blob/master/Samples/Win32/SampleCppApp/XamlBridge.cpp) file.
* **WPF:** See the [WindowsXamlHostBase.cs](https://github.com/windows-toolkit/Microsoft.Toolkit.Win32/blob/master/Microsoft.Toolkit.Wpf.UI.XamlHost/WindowsXamlHostBase.cs) and  [WindowsXamlHost.cs](https://github.com/windows-toolkit/Microsoft.Toolkit.Win32/blob/master/Microsoft.Toolkit.Wpf.UI.XamlHost/WindowsXamlHost.cs) files in the Windows Community Toolkit.  

* **Windows Forms:** See the [WindowsXamlHostBase.cs](https://github.com/windows-toolkit/Microsoft.Toolkit.Win32/blob/master/Microsoft.Toolkit.Forms.UI.XamlHost/WindowsXamlHostBase.cs) and  [WindowsXamlHost.cs](https://github.com/windows-toolkit/Microsoft.Toolkit.Win32/blob/master/Microsoft.Toolkit.Forms.UI.XamlHost/WindowsXamlHost.cs) files in the Windows Community Toolkit.

## Host a custom UWP control

The process for hosting a custom UWP control (either a control you define yourself or a control provided by a 3rd party) in a C++ Win32 app begins with the same steps as [hosting a standard control](#host-a-standard-uwp-control). However, hosting a custom UWP control requires additional projects and steps.

To host a custom UWP control, you'll need the following projects and components:

* **The project and source code for your app**. Make sure you have [configured your project](#configure-the-project) for hosting XAML Islands.

* **The custom UWP control**. You'll need the source code for the custom UWP control you want to host so you can compile it with your app. Typically, the custom control is defined in a UWP class library project that you reference in the same solution as your C++ Win32 project.

* **A UWP app project that defines a XamlApplication object**. Your C++ Win32 project must have access to an instance of the [Microsoft.Toolkit.Win32.UI.XamlHost.XamlApplication](https://github.com/windows-toolkit/Microsoft.Toolkit.Win32/tree/master/Microsoft.Toolkit.Win32.UI.XamlApplication) class provided by the Windows Community Toolkit. This type acts as a root metadata provider for loading metadata for custom UWP XAML types in assemblies in the current directory of your application.

  The recommended way to do this is to add a **Blank App (Universal Windows)** project to the same solution as your C++ Win32 project, revise the default `App` class in this project to derive from `XamlApplication`, and then create an instance of this object in the entry point code for your C++ Win32 app.

  > [!NOTE]
  > Your solution can contain only one project that defines a `XamlApplication` object. All custom UWP controls in your app share the same `XamlApplication` object. The project that defines the `XamlApplication` object must include references to all other UWP libraries and projects that are used host UWP controls in the XAML Island.

To host a custom UWP control in a C++ Win32 app, follow these general steps.

1. In the solution that contains your C++ Win32 desktop app project, add a **Blank App (Universal Windows)** project and define a `XamlApplication` class in it by following the detailed instructions in [this section](host-custom-control-with-xaml-islands.md#define-a-xamlapplication-class-in-a-uwp-app-project) in the related WPF walkthrough. 

2. In the same solution, add the project that contains the source code for the custom UWP XAML control (this is typically a UWP class library project) and build the project.

3. In the UWP app project, add a reference to the UWP class library project.

4. In your C++ Win32 project:

    * Add a reference to the UWP app project and the UWP class library project in your solution.
    * In the `WinMain` function or some other entry point code, create an instance of the `XamlApplication` class that you defined in the UWP app project earlier. For example, see [this line of code](https://github.com/microsoft/Xaml-Islands-Samples/blob/master/Standalone_Samples/CppWinRT_Desktop_Win32App/DesktopWin32App/DesktopWin32App.cpp#L46) from the C++ Win32 sample in the [XAML Islands Samples](https://github.com/microsoft/Xaml-Islands-Samples).

5. Follow the process described in the [Use the XAML hosting API to host a UWP control](#use-the-xaml-hosting-api-to-host-a-uwp-control) section to host the custom control in a XAML Island in your app. Assign an instance of the custom control to host to the [Content](https://docs.microsoft.com/uwp/api/windows.ui.xaml.hosting.desktopwindowxamlsource.content) property of the **DesktopWindowXamlSource** object in your code.

For a complete example for a C++ Win32 application, see the [XAML Island C++ Win32 sample](https://github.com/microsoft/Xaml-Islands-Samples/tree/master/Standalone_Samples/CppWinRT_Desktop_Win32App).

## Handle keyboard, layout, and DPI

The following sections provide guidance and links to code examples about how to handle keyboard and layout scenarios.

* [Keyboard input](#keyboard-input)
* [Keyboard focus navigation](#keyboard-focus-navigation)
* [Handle layout changes](#handle-layout-changes)
* [Handle DPI changes](#handle-dpi-changes)

### Keyboard input

To properly handle keyboard input for each XAML Island, your application must pass all Windows messages to the UWP XAML framework so that certain messages can be processed correctly. To do this, in some place in your application that can access the message loop, cast the **DesktopWindowXamlSource** object for each XAML Island to an **IDesktopWindowXamlSourceNative2** COM interface. Then, call the **PreTranslateMessage** method of this interface and pass in the current message.

  * **C++ Win32:**: The app can call **PreTranslateMessage** directly in its main message loop. For an example, see the [XamlBridge.cpp](https://github.com/microsoft/Xaml-Islands-Samples/blob/master/Samples/Win32/SampleCppApp/XamlBridge.cpp#L16) file.

  * **WPF:** The app can call **PreTranslateMessage** from the event handler for the [ComponentDispatcher.ThreadFilterMessage](https://docs.microsoft.com/dotnet/api/system.windows.interop.componentdispatcher.threadfiltermessage) event. For an example, see the [WindowsXamlHostBase.Focus.cs](https://github.com/windows-toolkit/Microsoft.Toolkit.Win32/blob/master/Microsoft.Toolkit.Wpf.UI.XamlHost/WindowsXamlHostBase.Focus.cs#L177) file in the Windows Community Toolkit.

  * **Windows Forms:** The app can call **PreTranslateMessage** from an override for the [Control.PreprocessMessage](https://docs.microsoft.com/dotnet/api/system.windows.forms.control.preprocessmessage) method. For an example, see the [WindowsXamlHostBase.KeyboardFocus.cs](https://github.com/windows-toolkit/Microsoft.Toolkit.Win32/blob/master/Microsoft.Toolkit.Forms.UI.XamlHost/WindowsXamlHostBase.KeyboardFocus.cs#L100) file in the Windows Community Toolkit.

### Keyboard focus navigation

When the user navigates through the UI elements in your application using the keyboard (for example, by pressing **Tab** or direction/arrow key), you'll need to programmatically move focus into and out of the **DesktopWindowXamlSource** object. When the user's keyboard navigation reaches the **DesktopWindowXamlSource**, move focus into the first [Windows.UI.Xaml.UIElement](https://docs.microsoft.com/uwp/api/windows.ui.xaml.uielement) object in the navigation order for your UI, continue to move focus to the following **Windows.UI.Xaml.UIElement** objects as the user cycles through the elements, and then move focus back out of the **DesktopWindowXamlSource** and into the parent UI element.  

The UWP XAML hosting API provides several types and members to help you accomplish these tasks.

* When the keyboard navigation enters your **DesktopWindowXamlSource**, the [GotFocus](https://docs.microsoft.com/uwp/api/windows.ui.xaml.hosting.desktopwindowxamlsource.gotfocus) event is raised. Handle this event and programmatically move focus to the first hosted **Windows.UI.Xaml.UIElement** by using the [NavigateFocus](https://docs.microsoft.com/uwp/api/windows.ui.xaml.hosting.desktopwindowxamlsource.navigatefocus) method.

* When the user is on the last focusable element in your **DesktopWindowXamlSource** and presses the **Tab** key or an arrow key, the [TakeFocusRequested](https://docs.microsoft.com/uwp/api/windows.ui.xaml.hosting.desktopwindowxamlsource.takefocusrequested) event is raised. Handle this event and programmatically move focus to the next focusable element in the host application. For example, in a WPF application where the **DesktopWindowXamlSource** is hosted in a [System.Windows.Interop.HwndHost](https://docs.microsoft.com/dotnet/api/system.windows.interop.hwndhost), you can use the [MoveFocus](https://docs.microsoft.com/dotnet/api/system.windows.frameworkelement.movefocus) method to transfer focus to the next focusable element in the host application.

For examples that demonstrate how to do this in the context of a working sample application, see the following code files:

  * **C++/Win32**: See the [XamlBridge.cpp](https://github.com/microsoft/Xaml-Islands-Samples/blob/master/Samples/Win32/SampleCppApp/XamlBridge.cpp) file.

  * **WPF:** See the [WindowsXamlHostBase.Focus.cs](https://github.com/windows-toolkit/Microsoft.Toolkit.Win32/blob/master/Microsoft.Toolkit.Wpf.UI.XamlHost/WindowsXamlHostBase.Focus.cs) file in the Windows Community Toolkit.  

  * **Windows Forms:** See the [WindowsXamlHostBase.KeyboardFocus.cs](https://github.com/windows-toolkit/Microsoft.Toolkit.Win32/blob/master/Microsoft.Toolkit.Forms.UI.XamlHost/WindowsXamlHostBase.KeyboardFocus.cs) file in the Windows Community Toolkit.

### Handle layout changes

When the user changes the size of the parent UI element, you'll need to handle any necessary layout changes to make sure your UWP controls display as you expect. Here are some important scenarios to consider.

* In a C++ Win32 application, when your application handles the WM_SIZE message it can reposition the hosted XAML Island by using the [SetWindowPos](https://docs.microsoft.com/windows/desktop/api/winuser/nf-winuser-setwindowpos) function. For an example, see the [SampleApp.cpp](https://github.com/microsoft/Xaml-Islands-Samples/blob/master/Samples/Win32/SampleCppApp/SampleApp.cpp#L170) code file.

* When the parent UI element needs to get the size of the rectangular area needed to fit the **Windows.UI.Xaml.UIElement** that you are hosting on the **DesktopWindowXamlSource**, call the [Measure](https://docs.microsoft.com/uwp/api/windows.ui.xaml.uielement.measure) method of the **Windows.UI.Xaml.UIElement**. For example:

    * In a WPF application you might do this from the [MeasureOverride](https://docs.microsoft.com/dotnet/api/system.windows.frameworkelement.measureoverride) method of the [HwndHost](https://docs.microsoft.com/dotnet/api/system.windows.interop.hwndhost) that hosts the **DesktopWindowXamlSource**. For an example, see the [WindowsXamlHostBase.Layout.cs](https://github.com/windows-toolkit/Microsoft.Toolkit.Win32/blob/master/Microsoft.Toolkit.Wpf.UI.XamlHost/WindowsXamlHostBase.Layout.cs) file in the Windows Community Toolkit.

    * In a Windows Forms application you might do this from the [GetPreferredSize](https://docs.microsoft.com/dotnet/api/system.windows.forms.control.getpreferredsize) method of the [Control](https://docs.microsoft.com/dotnet/api/system.windows.forms.control) that hosts the **DesktopWindowXamlSource**. For an example, see the [WindowsXamlHostBase.Layout.cs](https://github.com/windows-toolkit/Microsoft.Toolkit.Win32/blob/master/Microsoft.Toolkit.Forms.UI.XamlHost/WindowsXamlHostBase.Layout.cs) file in the Windows Community Toolkit.

* When the size of the parent UI element changes, call the [Arrange](https://docs.microsoft.com/uwp/api/windows.ui.xaml.uielement.arrange) method of the root **Windows.UI.Xaml.UIElement** that you are hosting on the **DesktopWindowXamlSource**. For example:

    * In a WPF application you might do this from the [ArrangeOverride](https://docs.microsoft.com/dotnet/api/system.windows.frameworkelement.arrangeoverride) method of the [HwndHost](https://docs.microsoft.com/dotnet/api/system.windows.interop.hwndhost) object that hosts the **DesktopWindowXamlSource**. For an example, see the [WindowsXamlHost.Layout.cs](https://github.com/windows-toolkit/Microsoft.Toolkit.Win32/blob/master/Microsoft.Toolkit.Wpf.UI.XamlHost/WindowsXamlHostBase.Layout.cs) file in the Windows Community Toolkit.

    * In a Windows Forms application you might do this from the handler for the [SizeChanged](https://docs.microsoft.com/dotnet/api/system.windows.forms.control.sizechanged) event of the [Control](https://docs.microsoft.com/dotnet/api/system.windows.forms.control) that hosts the **DesktopWindowXamlSource**. For an example, see the [WindowsXamlHost.Layout.cs](https://github.com/windows-toolkit/Microsoft.Toolkit.Win32/blob/master/Microsoft.Toolkit.Forms.UI.XamlHost/WindowsXamlHostBase.Layout.cs) file in the Windows Community Toolkit.

### Handle DPI changes

The UWP XAML framework handles DPI changes for hosted UWP controls automatically (for example, when the user drags the window between monitors with different screen DPI). For the best experience, we recommend that your Windows Forms, WPF, or C++ Win32 application is configured to be per-monitor DPI aware.

To configure your application to be per-monitor DPI aware, add a [side-by-side assembly manifest](https://docs.microsoft.com/windows/desktop/SbsCs/application-manifests) to your project and set the **\<dpiAwareness\>** element to **PerMonitorV2**. For more information about this value, see the description for [DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2](https://docs.microsoft.com/windows/desktop/hidpi/dpi-awareness-context).

```xml
<?xml version="1.0" encoding="UTF-8" standalone="yes"?>
<assembly xmlns="urn:schemas-microsoft-com:asm.v1" manifestVersion="1.0">
    <application xmlns="urn:schemas-microsoft-com:asm.v3">
        <windowsSettings>
            <dpiAwareness xmlns="http://schemas.microsoft.com/SMI/2016/WindowsSettings">PerMonitorV2</dpiAwareness>
        </windowsSettings>
    </application>
</assembly>
```

## Package the app

You can optionally package the app in an [MSIX package](https://docs.microsoft.com/windows/msix) for deployment. MSIX is the modern app packaging technology for Windows, and it is based on a combination of MSI, .appx, App-V and ClickOnce installation technologies.

The following instructions show you how to package the all the components in the solution in an MSIX package by using the [Windows Application Packaging Project](https://docs.microsoft.com/windows/msix/desktop/desktop-to-uwp-packaging-dot-net) in Visual Studio 2019. These steps are necessary only if you want to package the app in an MSIX package.

1. Add a new [Windows Application Packaging Project](https://docs.microsoft.com/windows/msix/desktop/desktop-to-uwp-packaging-dot-net) to your solution. As you create the project, select **Windows 10, version 1903 (10.0; Build 18362)** for both the **Target version** and **Minimum version**.

2. In the packaging project, right-click the **Applications** node and choose **Add reference**. In the list of projects, select the C++/Win32 desktop application project in your solution and click **OK**.

3. Build and run the packaging project. Confirm that the app runs and displays the UWP controls as expected.

## Troubleshooting

### Error using UWP XAML hosting API in a UWP app

| Issue | Resolution |
|-------|------------|
| Your app receives a **COMException** with the following message: "Cannot activate DesktopWindowXamlSource. This type cannot be used in a UWP app." or "Cannot activate WindowsXamlManager. This type cannot be used in a UWP app." | This error indicates you are trying to use the UWP XAML hosting API (specifically, you are trying to instantiate the [DesktopWindowXamlSource](https://docs.microsoft.com/uwp/api/windows.ui.xaml.hosting.desktopwindowxamlsource) or [WindowsXamlManager](https://docs.microsoft.com/uwp/api/windows.ui.xaml.hosting.windowsxamlmanager) types) in a UWP app. The UWP XAML hosting API is only intended to be used in non-UWP desktop apps, such as WPF, Windows Forms, and C++ Win32 applications. |

### Error trying to use the WindowsXamlManager or DesktopWindowXamlSource types

| Issue | Resolution |
|-------|------------|
| Your app receives an exception with the following  message: "WindowsXamlManager and DesktopWindowXamlSource are supported for apps targeting Windows version 10.0.18226.0 and later. Please check either the application manifest or package manifest and ensure the MaxTestedVersion property is updated." | This error indicates that your application tried to use the **WindowsXamlManager** or **DesktopWindowXamlSource** types in the UWP XAML hosting API, but the OS can't determine whether the app was built to target Windows 10, version 1903 or later. The UWP XAML hosting API was first introduced as a preview in an earlier version of Windows 10, but it is only supported starting in Windows 10, version 1903.</p></p>To resolve this issue, either create an MSIX package for the app and run it from the package, or install the [Microsoft.Toolkit.Win32.UI.SDK](https://www.nuget.org/packages/Microsoft.Toolkit.Win32.UI.SDK) NuGet package in your project. For more information, see [this section](#configure-the-project). |

### Error attaching to a window on a different thread

| Issue | Resolution |
|-------|------------|
| Your app receives a **COMException** with the following message: "AttachToWindow method failed because the specified HWND was created on a different thread." | This error indicates that your application called the **IDesktopWindowXamlSourceNative::AttachToWindow** method and passed it the HWND of a window that was created on a different thread. You must pass this method the HWND of a window that was created on the same thread as the code from which you are calling the method. |

### Error attaching to a window on a different top-level window

| Issue | Resolution |
|-------|------------|
| Your app receives a **COMException** with the following  message: "AttachToWindow method failed because the specified HWND descends from a different top-level window than the HWND that was previously passed to AttachToWindow on the same thread." | This error indicates that your application called the **IDesktopWindowXamlSourceNative::AttachToWindow** method and passed it the HWND of a window that descends from a different top-level window than a window you specified in a previous call to this method on the same thread.</p></p>After your application calls **AttachToWindow** on a particular thread, all other [DesktopWindowXamlSource](https://docs.microsoft.com/uwp/api/windows.ui.xaml.hosting.desktopwindowxamlsource) objects on the same thread can only attach to windows that are descendants of the same top-level window that was passed in the first call to **AttachToWindow**. When all the **DesktopWindowXamlSource** objects are closed for a particular thread, the next **DesktopWindowXamlSource** is then free to attach to any window again.</p></p>To resolve this issue, either close all **DesktopWindowXamlSource** objects that are bound to other top-level windows on this thread, or create a new thread for this **DesktopWindowXamlSource**. |

## Related topics

* [Host UWP XAML controls in desktop apps (XAML Islands)](xaml-islands.md)
* [C++ Win32 XAML Islands sample](https://github.com/microsoft/Xaml-Islands-Samples/tree/master/Samples/Win32/SampleCppApp)
