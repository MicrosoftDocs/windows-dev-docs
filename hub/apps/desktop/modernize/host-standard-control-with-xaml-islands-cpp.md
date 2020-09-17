---
description: This article demonstrates how to host a standard UWP control in a C++ Win32 app by using the XAML Hosting API.
title: Host a standard UWP control in a C++ Win32 app using XAML Islands
ms.date: 03/23/2020
ms.topic: article
keywords: windows 10, uwp, cpp, win32, xaml islands, wrapped controls, standard controls
ms.author: mcleans
author: mcleanbyron
ms.localizationpriority: medium
ms.custom: 19H1
---

# Host a standard UWP control in a C++ Win32 app

This article demonstrates how to use the [UWP XAML hosting API](using-the-xaml-hosting-api.md) to host a standard UWP control (that is, a control provied by the Windows SDK) in a new C++ Win32 app. The code is based on the [simple XAML Island sample](https://github.com/microsoft/Xaml-Islands-Samples/tree/master/Standalone_Samples/CppWinRT_Basic_Win32App), and this section discusses some of the most important parts of the code. If you have an existing C++ Win32 app project, you can adapt these steps and code examples for your project.

> [!NOTE]
> The scenario demonstrated in this article doesn't support directly editing XAML markup for UWP controls hosted in your app. This scenario restricts you to modifying the appearance and behavior of hosted UWP controls via code. For instructions that enable you to directly edit XAML markup when hosting UWP controls, see [Host a custom UWP control in a C++ Win32 app](host-custom-control-with-xaml-islands-cpp.md).

## Create a desktop application project

1. In Visual Studio 2019 with the Windows 10, version 1903 SDK (version 10.0.18362) or a later release installed, create a new **Windows Desktop Application** project and name it **MyDesktopWin32App**. This project type is available under the **C++**, **Windows**, and **Desktop** project filters.

2. In **Solution Explorer**, right-click the solution node, click **Retarget solution**, select the **10.0.18362.0** or a later SDK release, and then click **OK**.

3. Install the [Microsoft.Windows.CppWinRT](https://www.nuget.org/packages/Microsoft.Windows.CppWinRT/) NuGet package to enable support for [C++/WinRT](/windows/uwp/cpp-and-winrt-apis) in your project:

    1. Right-click your project in **Solution Explorer** and choose **Manage NuGet Packages**.
    2. Select the **Browse** tab, search for the [Microsoft.Windows.CppWinRT](https://www.nuget.org/packages/Microsoft.Windows.CppWinRT/) package, and install the latest version of this package.

    > [!NOTE]
    > For new projects, you can alternatively install the [C++/WinRT Visual Studio Extension (VSIX)](https://marketplace.visualstudio.com/items?itemName=CppWinRTTeam.cppwinrt101804264) and use one of the C++/WinRT project templates included in that extension. For more details, see [this article](/windows/uwp/cpp-and-winrt-apis/intro-to-using-cpp-with-winrt#visual-studio-support-for-cwinrt-xaml-the-vsix-extension-and-the-nuget-package).

4. Install the [Microsoft.Toolkit.Win32.UI.SDK](https://www.nuget.org/packages/Microsoft.Toolkit.Win32.UI.SDK) NuGet package:

    1. In the **NuGet Package Manager** window, make sure that **Include prerelease** is selected.
    2. Select the **Browse** tab, search for the **Microsoft.Toolkit.Win32.UI.SDK** package, and install version v6.0.0 (or later) of this package. This package provides several build and run time assets that enable XAML Islands to work in your app.

5. Set the `maxVersionTested` value in your [application manifest](/windows/desktop/SbsCs/application-manifests) to specify that your application is compatible with Windows 10, version 1903 or later.

    1. If you don't already have an application manifest in your project, add a new XML file to your project and name it **app.manifest**.
    2. In your application manifest, include the **compatibility** element and the child elements shown in the following example. Replace the **Id** attribute of the **maxVersionTested** element with the version number of Windows 10 you are targeting (this must be Windows 10, version 1903 or a later release).

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

## Use the XAML hosting API to host a UWP control

The basic process of using the XAML hosting API to host a UWP control follows these general steps:

1. Initialize the UWP XAML framework for the current thread before your app creates any of the [Windows.UI.Xaml.UIElement](/uwp/api/windows.ui.xaml.uielement) objects that it will host. There are several ways to do this, depending on when you plan to create the [DesktopWindowXamlSource](/uwp/api/windows.ui.xaml.hosting.desktopwindowxamlsource) object that will host the controls.

    * If your application creates the **DesktopWindowXamlSource** object before it creates any of the **Windows.UI.Xaml.UIElement** objects that it will host, this framework will be initialized for you when you instantiate the **DesktopWindowXamlSource** object. In this scenario, you don't need to add any code of your own to initialize the framework.

    * However, if your application creates the **Windows.UI.Xaml.UIElement** objects before it creates the **DesktopWindowXamlSource** object that will host them, your application must call the static [WindowsXamlManager.InitializeForCurrentThread](/uwp/api/windows.ui.xaml.hosting.windowsxamlmanager.initializeforcurrentthread) method to explicitly initialize the UWP XAML framework before the **Windows.UI.Xaml.UIElement** objects are instantiated. Your application should typically call this method when the parent UI element that hosts the **DesktopWindowXamlSource** is instantiated.

    > [!NOTE]
    > This method returns a [WindowsXamlManager](/uwp/api/windows.ui.xaml.hosting.windowsxamlmanager) object that contains a reference to the UWP XAML framework. You can create as many **WindowsXamlManager** objects as you want on a given thread. However, because each object holds a reference to the UWP XAML framework, you should dispose the objects to ensure that XAML resources are eventually released.

2. Create a [DesktopWindowXamlSource](/uwp/api/windows.ui.xaml.hosting.desktopwindowxamlsource) object and attach it to a parent UI element in your application that is associated with a window handle.

    To do this, you'll need to follow these steps:

    1. Create a **DesktopWindowXamlSource** object and cast it to the **IDesktopWindowXamlSourceNative** or **IDesktopWindowXamlSourceNative2** COM interface.
        > [!NOTE]
        > These interfaces are declared in the **windows.ui.xaml.hosting.desktopwindowxamlsource.h** header file in the Windows SDK. By default, this file is in %programfiles(x86)%\Windows Kits\10\Include\\<build number\>\um.

    2. Call the **AttachToWindow** method of the **IDesktopWindowXamlSourceNative** or **IDesktopWindowXamlSourceNative2** interface, and pass in the window handle of the parent UI element in your application.

    3. Set the initial size of the internal child window contained in the **DesktopWindowXamlSource**. By default, this internal child window is set to a width and height of 0. If you don't set the size of the window, any UWP controls you add to the **DesktopWindowXamlSource** will not be visible. To access the internal child window in the **DesktopWindowXamlSource**, use the **WindowHandle** property of the **IDesktopWindowXamlSourceNative** or **IDesktopWindowXamlSourceNative2** interface.

3. Finally, assign the **Windows.UI.Xaml.UIElement** you want to host to the [Content](/uwp/api/windows.ui.xaml.hosting.desktopwindowxamlsource.content) property of your **DesktopWindowXamlSource** object.

The following steps and code examples demonstrate how to do implement the above process:

1. In the **Source Files** folder of the project, open the default **MyDesktopWin32App.cpp** file. Delete the entire contents of the file and add the following `include` and `using` statements. In addition to standard C++ and UWP headers and namespaces, these statements include several items specific to XAML Islands.

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

4. Copy the following code after the previous section. This code defines the [window procedure](/windows/win32/learnwin32/writing-the-window-procedure) for the window.

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

## Package the app

You can optionally package the app in an [MSIX package](/windows/msix) for deployment. MSIX is the modern app packaging technology for Windows, and it is based on a combination of MSI, .appx, App-V and ClickOnce installation technologies.

The following instructions show you how to package the all the components in the solution in an MSIX package by using the [Windows Application Packaging Project](/windows/msix/desktop/desktop-to-uwp-packaging-dot-net) in Visual Studio 2019. These steps are necessary only if you want to package the app in an MSIX package.

> [!NOTE]
> If you choose to not package your application in an [MSIX package](/windows/msix) for deployment, computers that run your app must have the [Visual C++ Runtime](https://support.microsoft.com/en-us/help/2977003/the-latest-supported-visual-c-downloads) installed.

1. Add a new [Windows Application Packaging Project](/windows/msix/desktop/desktop-to-uwp-packaging-dot-net) to your solution. As you create the project, select **Windows 10, version 1903 (10.0; Build 18362)** for both the **Target version** and **Minimum version**.

2. In the packaging project, right-click the **Applications** node and choose **Add reference**. In the list of projects, select the C++/Win32 desktop application project in your solution and click **OK**.

3. Build and run the packaging project. Confirm that the app runs and displays the UWP controls as expected.

## Next steps

The code examples in this article get you started with the basic scenario of hosting a standard UWP control in a C++ Win32 app. The following sections introduce additional scenarios that your application may need to support.

### Host a custom UWP control

For many scenarios, you may need to host a custom UWP XAML control that contains several individual controls that work together. The process for hosting a custom UWP control (either a control you define yourself or a control provided by a 3rd party) in a C++ Win32 app is more complex than hosting a standard control, and requires additional code.

For a complete walkthrough, see [Host a custom UWP control in a C++ Win32 app using the XAML Hosting API](host-custom-control-with-xaml-islands-cpp.md).

### Advanced scenarios

Many desktop applications that host XAML Islands will need to handle additional scenarios in order to provide a smooth user experience. For example, desktop applications may need to handle keyboard input in XAML Islands, focus navigation between XAML Islands and other UI elements, and layout changes.

For more information about handling these scenarios and pointers to related code samples, see [Advanced scenarios for XAML Islands in C++ Win32 apps](advanced-scenarios-xaml-islands-cpp.md).

## Related topics

* [Host UWP XAML controls in desktop apps (XAML Islands)](xaml-islands.md)
* [Using the UWP XAML hosting API in a C++ Win32 app](using-the-xaml-hosting-api.md)
* [Host a custom UWP control in a C++ Win32 app](host-custom-control-with-xaml-islands-cpp.md)
* [Advanced scenarios for XAML Islands in C++ Win32 apps](advanced-scenarios-xaml-islands-cpp.md)
* [XAML Islands code samples](https://github.com/microsoft/Xaml-Islands-Samples)