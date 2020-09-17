---
title: Create a Universal Windows Platform console app
description: This topic describes how to write a UWP app that runs in a console window.
keywords: console uwp
ms.date: 08/02/2018
ms.topic: article


ms.localizationpriority: medium
---
# Create a Universal Windows Platform console app

This topic describes how to create a [C++/WinRT](../cpp-and-winrt-apis/intro-to-using-cpp-with-winrt.md) or C++/CX Universal Windows Platform (UWP) console app.

Starting with Windows 10, version 1803, you can write C++/WinRT or C++/CX UWP console apps that run in a console window, such as a DOS or PowerShell console window. Console apps use the console window for input and output, and can use [Universal C Runtime](/cpp/c-runtime-library/reference/crt-alphabetical-function-reference) functions such as **printf** and **getchar**. UWP console apps can be published to the Microsoft Store. They have an entry in the app list, and a primary tile that can be pinned to the Start menu. UWP console apps can be launched from the Start menu, though you will typically launch them from the command-line.

To see one in action, here's a video about Creating a UWP Console App.

> [!VIDEO https://www.youtube.com/embed/bwvfrguY20s]

## Use a UWP Console app template 

To create a UWP console app, first install the **Console App (Universal) Project Templates**, available from the [Visual Studio Marketplace](https://marketplace.visualstudio.com/items?itemName=AndrewWhitechapelMSFT.ConsoleAppUniversal). The installed templates are then available under **New Project** > **Installed** > **Other Languages** > **Visual C++** > **Windows Universal** as **Console App C++/WinRT (Universal Windows)** and **Console App C++/CX (Universal Windows)**.

## Add your code to main()

The templates add **Program.cpp**, which contains the `main()` function. This is where execution begins in a UWP console app. Access the command-line arguments with the `__argc` and `__argv` parameters. The UWP console app exits when control returns from `main()`.

The following example of **Program.cpp** is added by the **Console App C++/WinRT** template:

```cppwinrt
#include "pch.h"

using namespace winrt;

// This example code shows how you could implement the required main function
// for a Console UWP Application. You can replace all the code inside main
// with your own custom code.

int __cdecl main()
{
    // You can get parsed command-line arguments from the CRT globals.
    wprintf(L"Parsed command-line arguments:\n");
    for (int i = 0; i < __argc; i++)
    {
        wprintf(L"__argv[%d] = %S\n", i, __argv[i]);
    }

    // Keep the console window alive in case you want to see console output when running from within Visual Studio
	  wprintf(L"Press 'Enter' to continue: ");
    getchar();
}
```

## UWP Console app behavior

A UWP Console app can access the file-system from the directory it is run from, and below. This is possible because the template adds the [AppExecutionAlias](/uwp/schemas/appxpackage/uapmanifestschema/element-uap5-appexecutionalias) extension to your app's Package.appxmanifest file. This extension also enables the user to type the alias from a console window to launch the app. The app does not need to be in the system path to launch.

You can additionally give broad access to the file system to your UWP console app by adding the restricted capability `broadFileSystemAccess` as described in [File access permissions](../files/file-access-permissions.md). This capability works with APIs in the [**Windows.Storage**](/uwp/api/Windows.Storage) namespace.

More than one instance of a UWP Console app can run at a time because the template adds the [SupportsMultipleInstances](multi-instance-uwp.md) capability to your app's Package.appxmanifest file.

The template also adds the `Subsystem="console"` capability to the Package.appxmanifest file, which denotes that this UWP app is a console app. Note the `desktop4` and iot2 `namespace` prefixes. UWP Console apps are only supported on desktop and Internet of Things (IoT) projects.

```xml
<Package
  ...
  xmlns:desktop4="http://schemas.microsoft.com/appx/manifest/desktop/windows10/4" 
  xmlns:iot2="http://schemas.microsoft.com/appx/manifest/iot/windows10/2" 
  IgnorableNamespaces="uap mp uap5 desktop4 iot2">
  ...
  <Applications>
    <Application Id="App"
	  ...
      desktop4:Subsystem="console" 
      desktop4:SupportsMultipleInstances="true" 
      iot2:Subsystem="console" 
      iot2:SupportsMultipleInstances="true"  >
      ...
      <Extensions>
          <uap5:Extension 
            Category="windows.appExecutionAlias" 
            Executable="YourApp.exe" 
            EntryPoint="YourApp.App">
            <uap5:AppExecutionAlias desktop4:Subsystem="console">
              <uap5:ExecutionAlias Alias="YourApp.exe" />
            </uap5:AppExecutionAlias>
          </uap5:Extension>
      </Extensions>
    </Application>
  </Applications>
    ...
</Package>
```

## Additional considerations for UWP console apps

- Only C++/WinRT and C++/CX UWP apps may be console apps.
- UWP Console apps must target the Desktop, or IoT project type.
- UWP console apps may not create a window. They cannot use MessageBox(), or Location(), or any other API that may create a window for any reason, such as user consent prompts.
- UWP console apps may not consume background tasks nor serve as a background task.
- With the exception of [Command-Line activation](https://blogs.windows.com/buildingapps/2017/07/05/command-line-activation-universal-windows-apps/#5YJUzjBoXCL4MhAe.97), UWP console apps do not support activation contracts, including file association, protocol association, etc.
- Although UWP console apps support multi-instancing, they do not support [Multi-instancing redirection](multi-instance-uwp.md)
- For a list of Win32 APIs that are available to UWP apps, see [Win32 and COM APIs for UWP apps](/uwp/win32-and-com/win32-and-com-for-uwp-apps)