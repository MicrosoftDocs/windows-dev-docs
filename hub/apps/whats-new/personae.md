---
title: Choose tools and frameworks for your Windows app
description: Compare common Windows developer profiles and application types to choose suitable frameworks, technologies, and development tools.
keywords: what's new, Windows 11, Windows, developers, WinUI, sdk, tools
ms.date: 09/19/2026
author: GrantMeStrength
ms.author: jken
ms.reviewer: jken
ms.topic: overview
ms.localizationpriority: medium
---

# Choose Windows development tools and frameworks

Windows supports desktop apps, web experiences, games, cross-platform solutions, and other development workflows. Use the developer profiles and application types on this page to identify technologies that fit your goals.

## Explore developer profiles

These profiles represent common Windows development scenarios. Your project might combine technologies and tools from more than one profile.

### Emily: Desktop app developer

:::row:::
    :::column:::
        :::image type="content" source="./images/developers-collaborating-workspace.jpg" alt-text="Two developers collaborating at a desk with dual monitors that display code and project details.":::
    :::column-end:::
    :::column span="2":::
        Emily builds responsive Windows desktop apps that use native platform capabilities.

        **Builds:** Productivity tools, creative design apps, and utilities

        **Technologies:** [WinUI 3](../winui/winui3/index.md), [WPF](/dotnet/desktop/wpf/overview/), [Windows App SDK](../windows-app-sdk/index.md), [.NET MAUI](/dotnet/maui/), [.NET](/dotnet/), [Windows AI APIs](/windows/ai/), [Foundry Local](/windows/ai/foundry-local/get-started), and [Windows ML](/windows/ai/new-windows-ml/overview)

        **Tools:** Visual Studio and Visual Studio Code

        > [!TIP]
        > Support high DPI and accessibility, follow Fluent Design guidance, and choose a packaging option that fits your deployment requirements.
    :::column-end:::
:::row-end:::

### Carlos: Web app developer

:::row:::
    :::column:::
        :::image type="content" source="./images/web-app-developer-presentation.jpg" alt-text="A web app developer presenting an application interface.":::
    :::column-end:::
    :::column span="2":::
        Carlos builds responsive web applications that integrate with Windows services or run in hybrid environments.

        **Builds:** Progressive Web Apps (PWAs), internal dashboards, and consumer web apps

        **Technologies:** [WebView2](/microsoft-edge/webview2/), [React](https://react.dev), [Angular](https://angular.dev), [Blazor](/aspnet/core/blazor/), [ASP.NET Core](/aspnet/core/), and [TypeScript](https://www.typescriptlang.org)

        **Tools:** Windows Subsystem for Linux (WSL), Visual Studio Code, Node.js, GitHub, and GitHub Actions

        > [!TIP]
        > Design for different screen sizes, test accessibility and performance, and protect web traffic and identities.
    :::column-end:::
:::row-end:::

### Aisha: Game developer

:::row:::
    :::column:::
        :::image type="content" source="./images/game-developer-workstation.jpg" alt-text="A game developer working at a desktop computer.":::
    :::column-end:::
    :::column span="2":::
        Aisha builds games and simulations for Windows, with an emphasis on performance and rendering quality.

        **Builds:** AAA and independent games, simulations, virtual reality experiences, and educational apps

        **Technologies:** [DirectX 12](/windows/win32/direct3d12/directx-12-programming-guide), [Unity](https://unity.com), [Unreal Engine](https://www.unrealengine.com), and [Microsoft Game Development Kit (GDK)](/gaming/gdk/)

        **Tools:** C++, Visual Studio, Unity Editor, and PIX on Windows

        > [!TIP]
        > Profile CPU and GPU performance, and add advanced graphics features only when your target hardware supports them.
    :::column-end:::
:::row-end:::

### David: Enterprise and line-of-business developer

:::row:::
    :::column:::
        :::image type="content" source="./images/enterprise-developer-tablet.jpg" alt-text="An enterprise developer reviewing an application on a tablet.":::
    :::column-end:::
    :::column span="2":::
        David develops enterprise applications for internal business processes and workflows.

        **Builds:** Line-of-business apps, internal tools, and data-driven dashboards

        **Technologies:** [.NET](/dotnet/), [Windows Forms](/dotnet/desktop/winforms/overview), [WPF](/dotnet/desktop/wpf/overview/), [Entity Framework Core](/ef/core/), and [Blazor](/aspnet/core/blazor/)

        **Tools:** Visual Studio, SQL Server Management Studio, and Azure DevOps

        **Focus:** Adding Windows App SDK capabilities to existing WPF applications

        > [!TIP]
        > Account for security, compliance, maintainability, deployment, and support requirements when you choose an architecture.
    :::column-end:::
:::row-end:::

### Pradeep: Cross-platform app developer

:::row:::
    :::column:::
        :::image type="content" source="./images/cross-platform-dev-workstation.jpg" alt-text="A cross-platform app developer working at a desktop computer.":::
    :::column-end:::
    :::column span="2":::
        Pradeep creates applications that target Windows and other operating systems from a shared codebase.

        **Builds:** Mobile productivity apps, cross-platform utilities, and consumer apps

        **Technologies:** [.NET MAUI](/dotnet/maui/), [React Native for Windows](../../dev-environment/javascript/react-native-for-windows.md), and [Uno Platform](https://platform.uno)

        **Tools:** Visual Studio, Visual Studio Code, and Azure App Service

        > [!TIP]
        > Design for each platform's input methods and screen sizes, and test the app on every supported target.
    :::column-end:::
:::row-end:::

### Liam: Open-source and community developer

:::row:::
    :::column:::
        :::image type="content" source="./images/open-source-developer-working-laptop.jpg" alt-text="An open-source developer working on a laptop.":::
    :::column-end:::
    :::column span="2":::
        Liam contributes to open-source projects and builds tools for developer communities.

        **Builds:** Developer tools, community libraries, and command-line utilities

        **Technologies:** [Visual Studio Code extensions](https://marketplace.visualstudio.com/vscode), [GitHub Actions](https://github.com/features/actions), and [Node.js](https://nodejs.org)

        **Tools:** WSL, Windows Terminal, PowerShell, Visual Studio Code, GitHub, and Docker

        > [!TIP]
        > Document contribution workflows, gather community feedback, and automate repeatable build and release tasks.
    :::column-end:::
:::row-end:::


## Choose technologies by application type

Different application types have different goals, architectures, and platform requirements. The following examples highlight typical Windows application scenarios and suggest appropriate technologies for each.

| Application type | Typical use | Technologies to consider |
|------------------|-------------|--------------------------|
| Enterprise | Internal line-of-business systems, data-driven dashboards, and workflow tools | [WPF](/dotnet/desktop/wpf/overview), [Windows Forms](/dotnet/desktop/winforms/overview), or [.NET MAUI](/dotnet/maui/) for the UI; [.NET](/dotnet/) and [Entity Framework Core](/ef/core/) for application logic and data; [MSIX](/windows/msix/) for packaging; [Azure DevOps](/azure/devops/) for development workflows |
| Utility | Focused desktop tools that start quickly and follow current Windows design conventions | [WinUI 3](../winui/winui3/index.md) or [.NET MAUI](/dotnet/maui/) for the UI; [Windows App SDK](../windows-app-sdk/index.md) for Windows capabilities; [MSIX](/windows/msix/) for packaging; [Fluent Design](../design/index.md) for the user experience |
| AI-enabled | Apps that add text, image, speech, or language intelligence by using models on the device | [Windows AI APIs](/windows/ai/apis/get-started) for Windows-managed capabilities on supported devices; [Foundry Local](/windows/ai/foundry-local/get-started) for supported open-source models; [Windows ML](/windows/ai/new-windows-ml/overview) for custom ONNX models across CPU, GPU, and NPU |
| Games and immersive experiences | Games, simulations, and extended-reality experiences that require advanced graphics | [DirectX 12](/windows/win32/direct3d12/), [Microsoft GDK](/gaming/gdk/), [Unity](https://unity.com), [Unreal Engine](https://www.unrealengine.com), and [Visual Studio](/visualstudio/) |
| Headless or background | Services, agents, and command-line tools that run without a user interface | [.NET](/dotnet/) or [C++](/cpp/); [Windows Services](/windows/win32/services/) for long-running service processes; [PowerShell](/powershell/) and [Task Scheduler](/windows/win32/taskschd/task-scheduler-start-page) for automation |
| Web or hybrid | Cross-platform web applications that integrate with Windows features | [React](https://react.dev) or [Blazor](/aspnet/core/blazor/) for the front end; [ASP.NET Core](/aspnet/core/) or [Azure App Service](/azure/app-service/) for hosting; [WebView2](/microsoft-edge/webview2/) for hybrid Windows apps |
