---
description: This article demonstrates how to host a standard WinRT XAML control in a C++ desktop (Win32) app by using the XAML Hosting API.
title: Host a standard WinRT XAML control in a C++ desktop (Win32) app using XAML Islands
ms.date: 10/02/2020
ms.topic: article
keywords: windows 10, uwp, cpp, win32, xaml islands, wrapped controls, standard controls
ms.author: kbridge
author: Karl-Bridge-Microsoft
ms.localizationpriority: medium
ms.custom: 19H1
---

# Host a standard WinRT XAML control in a C++ desktop (Win32) app

> [!IMPORTANT]
> This topic uses or mentions types from the [CommunityToolkit/Microsoft.Toolkit.Win32](https://github.com/CommunityToolkit/Microsoft.Toolkit.Win32) GitHub repo. For important info about XAML Islands support, please see the [XAML Islands Notice](https://github.com/CommunityToolkit/Microsoft.Toolkit.Win32#xaml-islands-notice) in that repo.

This article demonstrates how to use the [WinRT XAML hosting API](using-the-xaml-hosting-api.md) to host a standard WinRT XAML control (that is, a control provided by the Windows SDK) in a new C++ desktop app. The code is based on the [simple XAML Island sample](https://github.com/microsoft/Xaml-Islands-Samples/tree/master/Standalone_Samples/CppWinRT_Basic_Win32App), and this section discusses some of the most important parts of the code. If you have an existing C++ desktop app project, you can adapt these steps and code examples for your project.

> [!NOTE]
> The scenario demonstrated in this article doesn't support directly editing XAML markup for WinRT XAML controls hosted in your app. This scenario restricts you to modifying the appearance and behavior of hosted controls via code. For instructions that enable you to directly edit XAML markup when hosting WinRT XAML controls, see [Host a custom WinRT XAML control in a C++ desktop app](host-custom-control-with-xaml-islands-cpp.md).

## Create a desktop application project

1. In Visual Studio 2019 with the Windows 10, version 1903 SDK (build 10.0.18362) or a later release installed, create a new **Windows Desktop Application** project and name it **MyDesktopWin32App**. This project type is available under the **C++**, **Windows**, and **Desktop** project filters.

2. In **Solution Explorer**, right-click the solution node, click **Retarget solution**, select the **10.0.18362.0** or a later SDK release, and then click **OK**.

3. Install the [Microsoft.Windows.CppWinRT](https://www.nuget.org/packages/Microsoft.Windows.CppWinRT/) NuGet package to enable support for [C++/WinRT](/windows/uwp/cpp-and-winrt-apis) in your project:

    1. Right-click your project in **Solution Explorer** and choose **Manage NuGet Packages**.
    2. Select the **Browse** tab, search for the [Microsoft.Windows.CppWinRT](https://www.nuget.org/packages/Microsoft.Windows.CppWinRT/) package, and install the latest version of this package.

    > [!NOTE]
    > For new projects, you can alternatively install the [C++/WinRT Visual Studio Extension (VSIX)](https://marketplace.visualstudio.com/items?itemName=CppWinRTTeam.cppwinrt101804264) and use one of the C++/WinRT project templates included in that extension. For more details, see [Visual Studio support for C++/WinRT, and the VSIX](/windows/uwp/cpp-and-winrt-apis/intro-to-using-cpp-with-winrt#visual-studio-support-for-cwinrt-xaml-the-vsix-extension-and-the-nuget-package).

4. On the **Browse** tab of the **NuGet Package Manager** window, search for the [Microsoft.Toolkit.Win32.UI.SDK](https://www.nuget.org/packages/Microsoft.Toolkit.Win32.UI.SDK) NuGet package and install the latest stable version of this package. This package provides several build and run time assets that enable XAML Islands to work in your app.

5. Set the `maxversiontested` value in your [application manifest](/windows/desktop/SbsCs/application-manifests) to specify that your application is compatible with Windows 10, version 1903.

    1. If you don't already have an application manifest in your project, add a new XML file to your project and name it **app.manifest**.
    2. In your application manifest, include the **compatibility** element and the child elements shown in the following example. Replace the **Id** attribute of the **maxversiontested** element with the version number of Windows you are targeting (this must be 10.0.18362.0 or a later release). Note that setting a higher value means older versions of Windows won't run the app properly because every Windows release only knows of versions before it. If you want the app to run on Windows 10, version 1903 (build 10.0.18362), you should either leave the 10.0.18362.0 value as is, or add multiple **maxversiontested** elements for the different values the app supports.

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

6. Add a reference to the Windows Runtime metadata:
   1. In **Solution Explorer**, right-click on your project **References** node and select **Add Reference**.
   2. Click the **Browse** button at the bottom of the page and navigate to the UnionMetadata folder in your SDK install path. By default the SDK will be installed to `C:\Program Files (x86)\Windows Kits\10\UnionMetadata`. 
   3. Then, select the folder named after the Windows version you are targeting (e.g. 10.0.18362.0) and inside of that folder pick the `Windows.winmd` file.
   4. Click **OK** to close the **Add Reference** dialog.

## Use the XAML hosting API to host a WinRT XAML control

The basic process of using the XAML hosting API to host a WinRT XAML control follows these general steps:

1. Initialize the WinRT XAML framework for the current thread before your app creates any of the [Windows.UI.Xaml.UIElement](/uwp/api/windows.ui.xaml.uielement) objects that it will host. There are several ways to do this, depending on when you plan to create the [DesktopWindowXamlSource](/uwp/api/windows.ui.xaml.hosting.desktopwindowxamlsource) object that will host the controls.

    * If your application creates the **DesktopWindowXamlSource** object before it creates any of the **Windows.UI.Xaml.UIElement** objects that it will host, this framework will be initialized for you when you instantiate the **DesktopWindowXamlSource** object. In this scenario, you don't need to add any code of your own to initialize the framework.

    * However, if your application creates the **Windows.UI.Xaml.UIElement** objects before it creates the **DesktopWindowXamlSource** object that will host them, your application must call the static [WindowsXamlManager.InitializeForCurrentThread](/uwp/api/windows.ui.xaml.hosting.windowsxamlmanager.initializeforcurrentthread) method to explicitly initialize the WinRT XAML framework before the **Windows.UI.Xaml.UIElement** objects are instantiated. Your application should typically call this method when the parent UI element that hosts the **DesktopWindowXamlSource** is instantiated.

    > [!NOTE]
    > This method returns a [WindowsXamlManager](/uwp/api/windows.ui.xaml.hosting.windowsxamlmanager) object that contains a reference to the WinRT XAML framework. You can create as many **WindowsXamlManager** objects as you want on a given thread. However, because each object holds a reference to the WinRT XAML framework, you should dispose the objects to ensure that XAML resources are eventually released.

2. Create a [DesktopWindowXamlSource](/uwp/api/windows.ui.xaml.hosting.desktopwindowxamlsource) object and attach it to a parent UI element in your application that is associated with a window handle.

    To do this, you'll need to follow these steps:

    1. Create a **DesktopWindowXamlSource** object and cast it to the [IDesktopWindowXamlSourceNative](/windows/win32/api/windows.ui.xaml.hosting.desktopwindowxamlsource/nn-windows-ui-xaml-hosting-desktopwindowxamlsource-idesktopwindowxamlsourcenative) or [IDesktopWindowXamlSourceNative2](/windows/win32/api/windows.ui.xaml.hosting.desktopwindowxamlsource/nn-windows-ui-xaml-hosting-desktopwindowxamlsource-idesktopwindowxamlsourcenative2) COM interface.

    2. Call the [AttachToWindow](/windows/win32/api/windows.ui.xaml.hosting.desktopwindowxamlsource/nf-windows-ui-xaml-hosting-desktopwindowxamlsource-idesktopwindowxamlsourcenative-attachtowindow) method of the **IDesktopWindowXamlSourceNative** or **IDesktopWindowXamlSourceNative2** interface, and pass in the window handle of the parent UI element in your application.

        > [!IMPORTANT]
        > Make sure that your code calls the **AttachToWindow** method only once per [DesktopWindowXamlSource](/uwp/api/windows.ui.xaml.hosting.desktopwindowxamlsource) object. Calling this method more than once for a **DesktopWindowXamlSource** object could result in a memory leak.

    3. Set the initial size of the internal child window contained in the **DesktopWindowXamlSource**. By default, this internal child window is set to a width and height of 0. If you don't set the size of the window, any WinRT XAML controls you add to the **DesktopWindowXamlSource** will not be visible. To access the internal child window in the **DesktopWindowXamlSource**, use the **WindowHandle** property of the **IDesktopWindowXamlSourceNative** or **IDesktopWindowXamlSourceNative2** interface.

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
        // to host WinRT XAML controls in any UI element that is associated with a window handle (HWND).
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

For complete examples that demonstrate using the XAML hosting API to host a WinRT XAML control, see the following code files:

* **C++ desktop (Win32):** See the [XamlBridge.cpp](https://github.com/microsoft/Xaml-Islands-Samples/blob/master/Standalone_Samples/Contoso/App/XamlBridge.cpp) file in the [XAML Islands code samples repo](https://github.com/microsoft/Xaml-Islands-Samples).
* **WPF:** See the [WindowsXamlHostBase.cs](https://github.com/windows-toolkit/Microsoft.Toolkit.Win32/blob/master/Microsoft.Toolkit.Wpf.UI.XamlHost/WindowsXamlHostBase.cs) and  [WindowsXamlHost.cs](https://github.com/windows-toolkit/Microsoft.Toolkit.Win32/blob/master/Microsoft.Toolkit.Wpf.UI.XamlHost/WindowsXamlHost.cs) files in the Windows Community Toolkit.  
* **Windows Forms:** See the [WindowsXamlHostBase.cs](https://github.com/windows-toolkit/Microsoft.Toolkit.Win32/blob/master/Microsoft.Toolkit.Forms.UI.XamlHost/WindowsXamlHostBase.cs) and  [WindowsXamlHost.cs](https://github.com/windows-toolkit/Microsoft.Toolkit.Win32/blob/master/Microsoft.Toolkit.Forms.UI.XamlHost/WindowsXamlHost.cs) files in the Windows Community Toolkit.

## Package the app

You can optionally package the app in an [MSIX package](/windows/msix) for deployment. MSIX is the modern app packaging technology for Windows, and it is based on a combination of MSI, .appx, App-V and ClickOnce installation technologies.

The following instructions show you how to package the all the components in the solution in an MSIX package by using the [Windows Application Packaging Project](/windows/msix/desktop/desktop-to-uwp-packaging-dot-net) in Visual Studio 2019. These steps are necessary only if you want to package the app in an MSIX package.

> [!NOTE]
> If you choose to not package your application in an [MSIX package](/windows/msix) for deployment, then computers that run your app must have the [Visual C++ Runtime](https://support.microsoft.com/en-us/help/2977003/the-latest-supported-visual-c-downloads) installed.

1. Add a new [Windows Application Packaging Project](/windows/msix/desktop/desktop-to-uwp-packaging-dot-net) to your solution. As you create the project, select **Windows 10, version 1903 (10.0; Build 18362)** for both the **Target version** and **Minimum version**.

2. In the packaging project, right-click the **Applications** node and choose **Add reference**. In the list of projects, select the C++ desktop application project in your solution and click **OK**.

3. Build and run the packaging project. Confirm that the app runs and displays the WinRT XAML controls as expected.

4. For info about distributing/deploying the package, see [Manage your MSIX deployment](/windows/msix/desktop/managing-your-msix-deployment-overview). 

## Next steps

The code examples in this article get you started with the basic scenario of hosting a standard WinRT XAML control in a C++ desktop app. The following sections introduce additional scenarios that your application may need to support.

### Host a custom WinRT XAML control

For many scenarios, you may need to host a custom WinRT XAML control that contains several individual controls that work together. The process for hosting a custom control (either a control you define yourself or a control provided by a 3rd party) in a C++ desktop app is more complex than hosting a standard control, and requires additional code.

For a complete walkthrough, see [Host a custom WinRT XAML control in a C++ desktop app using the XAML Hosting API](host-custom-control-with-xaml-islands-cpp.md).

### Advanced scenarios

Many desktop applications that host XAML Islands will need to handle additional scenarios in order to provide a smooth user experience. For example, desktop applications may need to handle keyboard input in XAML Islands, focus navigation between XAML Islands and other UI elements, and layout changes.

For more information about handling these scenarios and pointers to related code samples, see [Advanced scenarios for XAML Islands in C++ desktop apps](advanced-scenarios-xaml-islands-cpp.md).

## Related topics

* [Host WinRT XAML controls in desktop apps (XAML Islands)](xaml-islands.md)
* [Using the WinRT XAML hosting API in a C++ desktop app](using-the-xaml-hosting-api.md)
* [Host a custom WinRT XAML control in a C++ desktop app](host-custom-control-with-xaml-islands-cpp.md)
* [Advanced scenarios for XAML Islands in C++ desktop apps](advanced-scenarios-xaml-islands-cpp.md)
* [XAML Islands code samples](https://github.com/microsoft/Xaml-Islands-Samples)
