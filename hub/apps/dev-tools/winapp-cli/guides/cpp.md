---
title: Using winapp CLI with C++ and CMake
description: Learn how to use the winapp CLI with a C++ application to add package identity, access Windows APIs, and package as MSIX.
ms.date: 02/20/2026
ms.topic: how-to
---

# Using winapp CLI with C++ and CMake

This guide demonstrates how to use the winapp CLI with a C++ application to debug with package identity and package your application as an MSIX.

Package identity is a core concept in the Windows app model. It allows your application to access specific Windows APIs (like Notifications, Security, AI APIs, etc.), have a clean install/uninstall experience, and more.

A standard executable (like one created with `cmake --build`) does not have package identity. This guide shows how to add it for debugging and then package it for distribution.

## Prerequisites

1. **Build Tools**: Use a compiler toolchain supported by CMake. This example uses Visual Studio. You can install the community edition with:

    ```powershell
    winget install --id Microsoft.VisualStudio.Community --source winget --override "--add Microsoft.VisualStudio.Workload.NativeDesktop --includeRecommended --passive --wait"
    ```

    Reboot after installation.

2. **CMake**: Install CMake:

    ```powershell
    winget install Kitware.CMake --source winget
    ```

3. **winapp CLI**: Install the `winapp` CLI via winget:

    ```powershell
    winget install Microsoft.winappcli --source winget
    ```

## 1. Create a new C++ app

Start by creating a simple C++ application. Create a new directory for your project:

```powershell
mkdir cpp-app
cd cpp-app
```

Create a `main.cpp` file with a basic "Hello, world!" program:

```cpp
#include <iostream>

int main() {
    std::cout << "Hello, world!" << std::endl;
    return 0;
}
```

Create a `CMakeLists.txt` file to configure the build:

```cmake
cmake_minimum_required(VERSION 3.20)
project(cpp-app)

set(CMAKE_CXX_STANDARD 20)
set(CMAKE_CXX_STANDARD_REQUIRED ON)

add_executable(cpp-app main.cpp)
```

Build and run it to make sure everything is working:

```powershell
cmake -B build
cmake --build build --config Debug
.\build\Debug\cpp-app.exe
```

## 2. Update code to check identity

Update the app to check if it's running with package identity using the Windows Runtime C++ API.

First, update your `CMakeLists.txt` to link against the Windows App Model library:

```cmake
cmake_minimum_required(VERSION 3.20)
project(cpp-app)

set(CMAKE_CXX_STANDARD 20)
set(CMAKE_CXX_STANDARD_REQUIRED ON)

add_executable(cpp-app main.cpp)

# Link Windows Runtime libraries
target_link_libraries(cpp-app PRIVATE WindowsApp.lib OneCoreUap.lib)
```

Next, replace the contents of `main.cpp`:

```cpp
#include <iostream>
#include <windows.h>
#include <appmodel.h>

int main() {
    UINT32 length = 0;
    LONG result = GetCurrentPackageFamilyName(&length, nullptr);

    if (result == ERROR_INSUFFICIENT_BUFFER) {
        std::wstring familyName;
        familyName.resize(length);

        result = GetCurrentPackageFamilyName(&length, familyName.data());

        if (result == ERROR_SUCCESS) {
            std::wcout << L"Package Family Name: " << familyName.c_str() << std::endl;
        } else {
            std::wcout << L"Error retrieving Package Family Name" << std::endl;
        }
    } else {
        std::cout << "Not packaged" << std::endl;
    }

    return 0;
}
```

## 3. Run without identity

Rebuild and run the app:

```powershell
cmake --build build --config Debug
.\build\Debug\cpp-app.exe
```

You should see "Not packaged". This confirms that the standard executable is running without any package identity.

## 4. Initialize project with winapp CLI

The `winapp init` command sets up everything you need: app manifest, assets, and optionally Windows App SDK headers for C++ development.

```powershell
winapp init
```

When prompted:

- **Package name**: Press Enter to accept the default (cpp-app)
- **Publisher name**: Press Enter to accept the default or enter your name
- **Version**: Press Enter to accept 1.0.0.0
- **Entry point**: Press Enter to accept the default (cpp-app.exe)
- **Setup SDKs**: Select "Stable SDKs" to download Windows App SDK and generate headers

This command creates:

- `appxmanifest.xml` and `Assets` folder for your app identity
- A `.winapp` folder with Windows App SDK headers and libraries
- A `winapp.yaml` configuration file for pinning SDK versions

## 5. Debug with identity

To test features that require identity without fully packaging the app, use `winapp create-debug-identity`:

1. **Build the executable**:

    ```powershell
    cmake --build build --config Debug
    ```

2. **Apply debug identity**:

    ```powershell
    winapp create-debug-identity .\build\Debug\cpp-app.exe
    ```

3. **Run the executable**:

    ```powershell
    .\build\Debug\cpp-app.exe
    ```

You should now see output similar to:

```
Package Family Name: cpp-app_12345abcde
```

### Automating debug identity (optional)

Add a post-build command to your `CMakeLists.txt` to automatically apply debug identity:

```cmake
add_custom_command(TARGET cpp-app POST_BUILD
    COMMAND $<$<CONFIG:Debug>:winapp>
            $<$<CONFIG:Debug>:create-debug-identity>
            $<$<CONFIG:Debug>:$<TARGET_FILE:cpp-app>>
    WORKING_DIRECTORY ${CMAKE_CURRENT_SOURCE_DIR}
    COMMAND_EXPAND_LISTS
    COMMENT "Applying debug identity to executable..."
)
```

## 6. Using Windows App SDK (optional)

If you selected to setup the SDKs during `winapp init`, you have access to Windows App SDK headers in the `.winapp/include` folder.

Update your `CMakeLists.txt` to include the headers:

```cmake
# Add Windows App SDK include directory
target_include_directories(cpp-app PRIVATE ${CMAKE_CURRENT_SOURCE_DIR}/.winapp/include)
```

Update `main.cpp` to use the Windows App Runtime API:

```cpp
#include <iostream>
#include <windows.h>
#include <appmodel.h>
#include <winrt/Microsoft.Windows.ApplicationModel.WindowsAppRuntime.h>

int main() {
    winrt::init_apartment();

    UINT32 length = 0;
    LONG result = GetCurrentPackageFamilyName(&length, nullptr);

    if (result == ERROR_INSUFFICIENT_BUFFER) {
        std::wstring familyName;
        familyName.resize(length);

        result = GetCurrentPackageFamilyName(&length, familyName.data());

        if (result == ERROR_SUCCESS) {
            std::wcout << L"Package Family Name: " << familyName.c_str() << std::endl;

            auto runtimeVersion = winrt::Microsoft::Windows::ApplicationModel::WindowsAppRuntime::RuntimeInfo::AsString();
            std::wcout << L"Windows App Runtime Version: " << runtimeVersion.c_str() << std::endl;
        }
    } else {
        std::cout << "Not packaged" << std::endl;
    }

    return 0;
}
```

## 7. Restore headers when needed

The `.winapp` folder is automatically added to `.gitignore`. When others clone your project, they need to restore these files:

```powershell
winapp restore
winapp cert generate --if-exists skip
```

## 8. Package with MSIX

Once you're ready to distribute, package as MSIX:

1. **Build for release**:

    ```powershell
    cmake --build build --config Release
    ```

2. **Prepare package directory**:

    ```powershell
    mkdir dist
    copy .\build\Release\cpp-app.exe .\dist\
    ```

3. **Generate a development certificate**:

    ```powershell
    winapp cert generate --if-exists skip
    ```

4. **Package and sign**:

    ```powershell
    winapp pack .\dist --cert .\devcert.pfx
    ```

5. **Install the certificate** (run as administrator):

    ```powershell
    winapp cert install .\devcert.pfx
    ```

6. **Install and run**:

    ```powershell
    Add-AppxPackage .\cpp-app.msix
    cpp-app
    ```

> [!TIP]
> - Sign your MSIX with a code signing certificate from a Certificate Authority for production distribution.
> - The Microsoft Store signs the MSIX for you, no need to sign before submission.
> - You may need separate MSIX packages for each architecture you support (x64, Arm64).

## Related topics

- [winapp CLI reference](../usage.md)
- [winapp CLI overview](../index.md)
- [Windows App SDK documentation](/windows/apps/windows-app-sdk/)
