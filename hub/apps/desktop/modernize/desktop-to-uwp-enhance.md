---
description: Learn how to configure your WPF, Windows Forms, or Win32 desktop app to call Windows Runtime APIs and add modern Windows experiences.
title: Call Windows Runtime APIs in desktop apps
ms.date: 04/07/2026
ms.topic: how-to
keywords: windows 11, windows app sdk, winrt, desktop app
ms.localizationpriority: medium
---

# Call Windows Runtime APIs in desktop apps

This article describes how to configure your desktop app projects to call Windows Runtime (WinRT) APIs — the APIs that power modern Windows features such as notifications, file pickers, sharing, and more.

Some WinRT APIs are not supported in desktop apps. For more information, see [Windows Runtime APIs not supported in desktop apps](desktop-to-uwp-supported-api.md).

## Configure a .NET project

<a id="net-6-and-later-use-the-target-framework-moniker-option"></a>
### .NET 6 and later: Use the Target Framework Moniker option

Specify a Windows OS version-specific Target Framework Moniker (TFM) in your project file. This adds a reference to the appropriate [Windows SDK targeting package](https://www.nuget.org/packages/Microsoft.Windows.SDK.NET.Ref) at build time.

1. In Visual Studio, right-click your project in **Solution Explorer** and choose **Edit Project File**.

2. Replace the **TargetFramework** value with a Windows-specific TFM:

    | Target | TFM |
    |--------|-----|
    | Windows 11, version 24H2 | `net8.0-windows10.0.26100.0` |
    | Windows 11, version 22H2 | `net8.0-windows10.0.22621.0` |
    | Windows 11 (initial release) | `net8.0-windows10.0.22000.0` |
    | Windows 10, version 2004 | `net8.0-windows10.0.19041.0` |
    | Windows 10, version 1903 | `net8.0-windows10.0.18362.0` |
    | Windows 10, version 1809 | `net8.0-windows10.0.17763.0` |

    Example:

    ```xml
    <Project Sdk="Microsoft.NET.Sdk">
      <PropertyGroup>
        <OutputType>WinExe</OutputType>
        <TargetFramework>net8.0-windows10.0.19041.0</TargetFramework>
      </PropertyGroup>
    </Project>
    ```

3. Save and close the project file.

#### Supporting a minimum Windows version

To allow your app to run on a Windows version older than your TFM target, set **TargetPlatformMinVersion** explicitly:

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>WinExe</OutputType>
    <TargetFramework>net8.0-windows10.0.19041.0</TargetFramework>
    <TargetPlatformMinVersion>10.0.17763.0</TargetPlatformMinVersion>
  </PropertyGroup>
</Project>
```

When you target a range of OS versions, guard calls to APIs that aren't available on all versions using [ApiInformation](/uwp/api/windows.foundation.metadata.apiinformation) checks. For more information, see [Version adaptive apps](/windows/uwp/debug-test-perf/version-adaptive-apps).

#### WinRT APIs not supported in .NET 6 and later

In .NET 6 and later, several WinRT APIs in the **Windows.UI** namespace are not supported. Use the equivalent APIs in the **Microsoft.UI** namespace (provided by the Windows App SDK) instead:

| Unsupported | Use instead |
|---|---|
| `Windows.UI.Colors` | [`Microsoft.UI.Colors`](/windows/windows-app-sdk/api/winrt/microsoft.ui.colors) |
| `Windows.UI.ColorHelper` | [`Microsoft.UI.ColorHelper`](/windows/windows-app-sdk/api/winrt/microsoft.ui.colorhelper) |
| `Windows.UI.Text` (most types) | [`Microsoft.UI.Text`](/windows/windows-app-sdk/api/winrt/microsoft.ui.text) |
| `Windows.UI.Xaml` (all types) | [`Microsoft.UI.Xaml`](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml) |

### .NET Core 3.x or .NET Framework: Install the NuGet package

If your app targets .NET Core 3.x or .NET Framework, install the [`Microsoft.Windows.SDK.Contracts`](https://www.nuget.org/packages/Microsoft.Windows.SDK.Contracts) NuGet package:

1. In Visual Studio, right-click your project and choose **Manage NuGet Packages**.
2. Search for `Microsoft.Windows.SDK.Contracts`.
3. Select the package version matching your minimum Windows target:

    | Package version | Windows target |
    |---|---|
    | 10.0.19041.xxxx | Windows 10, version 2004 |
    | 10.0.18362.xxxx | Windows 10, version 1903 |
    | 10.0.17763.xxxx | Windows 10, version 1809 |
    | 10.0.17134.xxxx | Windows 10, version 1803 |

4. Click **Install**.

### Multi-targeting .NET 6+ and earlier .NET versions

Configure the project file to use the TFM approach for .NET 6+ and the NuGet package for earlier versions:

```xml
<Project Sdk="Microsoft.NET.Sdk.WindowsDesktop">
  <PropertyGroup>
    <OutputType>WinExe</OutputType>
    <TargetFrameworks>netcoreapp3.1;net8.0-windows10.0.19041.0</TargetFrameworks>
    <UseWPF>true</UseWPF>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Condition="'$(TargetFramework)' == 'netcoreapp3.1'"
                     Include="Microsoft.Windows.SDK.Contracts"
                     Version="10.0.19041.0" />
  </ItemGroup>
</Project>
```

## Configure a C++ (Win32) project

Use [C++/WinRT](/windows/uwp/cpp-and-winrt-apis/) to consume WinRT APIs from C++ desktop apps.

- **New projects**: Install the [C++/WinRT Visual Studio Extension (VSIX)](https://marketplace.visualstudio.com/items?itemName=CppWinRTTeam.cppwinrt101804264) and use one of the included project templates.
- **Existing projects**: Install the [Microsoft.Windows.CppWinRT](https://www.nuget.org/packages/Microsoft.Windows.CppWinRT/) NuGet package.

For more details, see [Visual Studio support for C++/WinRT](/windows/uwp/cpp-and-winrt-apis/intro-to-using-cpp-with-winrt#visual-studio-support-for-cwinrt-xaml-the-vsix-extension-and-the-nuget-package).

## Example: Send a toast notification

Once your project is configured, you can call WinRT APIs directly. The following example sends a toast notification from a WPF or Win32 app.

> [!NOTE]
> Toast notifications require app identity. Packaged apps have identity automatically. For unpackaged apps, see [Send local toast notifications from desktop C# apps](/windows/apps/develop/notifications/app-notifications/send-local-toast) for the additional registration steps required.


```csharp
using System.Security;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

void ShowToast(string title, string content, string image, string logo)
{
    string xmlString =
        $@"<toast><visual><binding template='ToastGeneric'>" +
        $"<text>{SecurityElement.Escape(title)}</text><text>{SecurityElement.Escape(content)}</text>" +
        $"<image src='{SecurityElement.Escape(image)}'/>" +
        $"<image src='{SecurityElement.Escape(logo)}' placement='appLogoOverride' hint-crop='circle'/>" +
        "</binding></visual></toast>";

    XmlDocument toastXml = new XmlDocument();
    toastXml.LoadXml(xmlString);
    ToastNotificationManager.CreateToastNotifier().Show(new ToastNotification(toastXml));
}
```

```cppwinrt
#include <sstream>
#include <string>
#include <string_view>
#include <winrt/Windows.Data.Xml.Dom.h>
#include <winrt/Windows.UI.Notifications.h>

using namespace winrt::Windows::UI::Notifications;
using namespace winrt::Windows::Data::Xml::Dom;

std::wstring XmlEscape(std::wstring_view input)
{
    std::wstring result;
    result.reserve(input.size());
    for (wchar_t ch : input) {
        switch (ch) {
            case L'&':  result += L"&amp;";  break;
            case L'<':  result += L"&lt;";   break;
            case L'>':  result += L"&gt;";   break;
            case L'\'': result += L"&apos;"; break;
            case L'"':  result += L"&quot;"; break;
            default:    result += ch;        break;
        }
    }
    return result;
}

void ShowToast(std::wstring title, std::wstring content, std::wstring image, std::wstring logo)
{
    std::wostringstream xml;
    xml << L"<toast><visual><binding template='ToastGeneric'>"
        << L"<text>" << XmlEscape(title) << L"</text><text>" << XmlEscape(content) << L"</text>"
        << L"<image src='" << XmlEscape(image) << L"'/>"
        << L"<image src='" << XmlEscape(logo) << L"' placement='appLogoOverride' hint-crop='circle'/>"
        << L"</binding></visual></toast>";

    XmlDocument toastXml;
    toastXml.LoadXml(xml.str().c_str());
    ToastNotificationManager::CreateToastNotifier().Show(ToastNotification(toastXml));
}
```

For more notification scenarios, see [Adaptive and interactive toast notifications](/windows/uwp/design/shell/tiles-and-notifications/adaptive-interactive-toasts).

## Conditional compilation

<a id="conditional-compilation"></a>

When [multi-targeting](#multi-targeting-net-6-and-earlier-net-versions) across .NET 6+ and earlier versions, use conditional compilation to write version-specific code in a single project:

```csharp
#if NET6_0_OR_GREATER
    // Code that uses .NET 6+ APIs or TFM-available WinRT APIs
#else
    // Fallback code for .NET Core 3.x / .NET Framework
#endif
```

## Related content

- [Windows Runtime APIs not supported in desktop apps](desktop-to-uwp-supported-api.md)
- [Integrate your app with Windows using packaging extensions](desktop-to-uwp-extensions.md)
- [Modernize your desktop apps](index.md)
