---
title: Get started with Docker for remote development with containers
description: Guide to get started with Docker Desktop on Windows or WSL.
author: mattwojo 
ms.author: mattwoj 
manager: jken
ms.topic: article
ms.technology: windows-nodejs
keywords: Microsoft, Windows, Docker, WSL, Remote development, Containers, Docker Desktop, Windows vs WSL
ms.date: 09/24/2020
---

# Overview of Docker remote development on Windows



## Introduction to Docker on Windows

:::row:::
    :::column:::
       [![Docker Docs Icon](../../images/docker-docs-icon.png)](https://docs.docker.com/docker-for-windows/install/)<br>
        **[Install Docker Desktop for Windows](https://docs.docker.com/docker-for-windows/install/)**<br>
        Download Docker Desktop for Windows 10 and learn how to install, including system requirements, what's included in the installer, how to uninstall, differences between stable and edge versions, and  switching between Windows and Linux containers in these Docker for Windows docs.
    :::column-end:::
    :::column:::
       [![Docker running screenshot](../../images/docker-running-screenshot.png)](https://docs.docker.com/get-started/)<br>
        **[Get started with Docker](https://docs.docker.com/get-started/)**<br>
        Docker orientation and setup docs with step-by-step instructions on how to get started, including a video walk-through.
    :::column-end:::
    :::column:::
       [![Microsoft Learn Docker course screenshot](../../images/docker-learn-course.png)](/learn/modules/intro-to-docker-containers/)<br>
        **[MS Learn course: Introduction to Docker containers](/learn/modules/intro-to-docker-containers/)**<br>
        Microsoft Learn offers a free intro course on Docker containers, in addition to course covering how to [Build a containerized web application with Docker](/learn/modules/intro-to-containers/) and a [variety of courses on using Docker with Azure services](/learn/browse/?terms=docker).
    :::column-end:::
    :::column:::
       [![Docker Desktop WSL2 menu screenshot](../../images/docker-wsl2.png)](/windows/wsl/tutorials/wsl-containers)<br>
        **[Get started with Docker remote containers on WSL 2](/windows/wsl/tutorials/wsl-containers)**<br>
        Learn how to set up Docker Desktop for Windows to use with a Linux command line (Ubuntu, Debian, SUSE, etc) using WSL 2 (Windows Subsystem for Linux, version 2).
    :::column-end:::
:::row-end:::

## VS Code and Docker

:::row:::
    :::column:::
       [![VS Code remote container graphic](../../images/vscode-remote-containers.png)](https://code.visualstudio.com/docs/remote/create-dev-container)<br>
        **[Create a Docker container with VS Code](https://code.visualstudio.com/docs/remote/containers-tutorial)**<br>
        Learn how to create a full-featured development environment inside of a Docker container with Visual Studio Code and the [Remote - Containers extension](https://marketplace.visualstudio.com/items?itemName=ms-vscode-remote.remote-containers) in this tutorial, or find a deeper dive in [this article](https://code.visualstudio.com/docs/remote/create-dev-container) or tutorials on how to set up a [NodeJS container](https://code.visualstudio.com/docs/containers/quickstart-node), a [Python container](https://code.visualstudio.com/docs/containers/quickstart-python), or a [ASP.NET Core container](https://code.visualstudio.com/docs/containers/quickstart-aspnet-core).
    :::column-end:::
    :::column:::
       [![VSCode attach Docker screenshot](../../images/vscode-attach-docker.png)](https://code.visualstudio.com/docs/remote/attach-container)<br>
        **[Attach VS Code to a Docker container](https://code.visualstudio.com/docs/remote/attach-container)**<br>
        Learn how to attach Visual Studio Code to a Docker container that is already running or to a [container in a Kubernetes cluster](https://code.visualstudio.com/docs/remote/attach-container#_attach-to-a-container-in-a-kubernetes-cluster).
    :::column-end:::
    :::column:::
       [![VSCode container menu screenshot](../../images/vscode-advanced-docker.png)](https://code.visualstudio.com/docs/remote/containers-advanced)<br>
        **[Advanced Container Configuration](https://code.visualstudio.com/docs/remote/containers-advanced)**<br>
        Learn about advanced setup scenarios for using Docker containers with Visual Studio Code or read this article on how to [Inspect Containers](https://code.visualstudio.com/blogs/2019/10/31/inspecting-containers) for debugging with VS Code.
    :::column-end:::
    :::column:::
       [![VSCode Docker Desktop with WSL screenshot](../../images/vscode-docker-wsl.png)](https://code.visualstudio.com/blogs/2020/07/01/containers-wsl)<br>
        **[Using Remote Containers in WSL 2](https://code.visualstudio.com/blogs/2020/07/01/containers-wsl)**<br>
        Read about using Docker containers with WSL 2 (Windows Subsystem for Linux, version 2) and how to set everything up with VS Code. You can also read about [how it works](https://code.visualstudio.com/blogs/2020/03/02/docker-in-wsl2#_how-it-works).
    :::column-end:::
:::row-end:::

## .NET Core and Docker

:::row:::
    :::column:::
       [![.NET microservice guide cover](../../images/dotnet-microservice-guide.png)](/dotnet/architecture/microservices/)<br>
        **[.NET Guide: Microservice apps and containers](/dotnet/architecture/microservices/)**<br>
        An introductory guide to developing microservices-based apps and managing them using containers. Discusses architectural design and implementation approaches using .NET Core and Docker containers, including [Choosing between .NET Core and .NET Framework for Docker containers](/dotnet/architecture/microservices/net-core-net-framework-containers/).
    :::column-end:::
    :::column:::
       [![Docker Infographic](../../images/dotnet-docker-infographic.png)](/dotnet/architecture/microservices/container-docker-introduction/docker-defined)<br>
        **[What is Docker?](/dotnet/architecture/microservices/container-docker-introduction/docker-defined)**<br>
        Basic explanation of Docker containers, including [Comparing Docker containers with Virtual machines](/dotnet/architecture/microservices/container-docker-introduction/docker-defined#comparing-docker-containers-with-virtual-machines).
    :::column-end:::
    :::column:::
       [![Docker Taxonomy infographic](../../images/taxonomy-of-docker-terms-and-concepts.png)](/dotnet/architecture/microservices/container-docker-introduction/docker-containers-images-registries)<br>
        **[Docker containers, images, and registries](/dotnet/architecture/microservices/container-docker-introduction/docker-containers-images-registries)**<br>
        A basic taxonomy of Docker terms and concepts explaining the difference between containers, images, and registries.
    :::column-end:::
    :::column:::
       [![Inner-loop dev workflow with Docker infographic](../../images/dotnet-docker-workflow.png)](/dotnet/architecture/microservices/docker-application-development-process/docker-app-development-workflow)<br>
        **[Development workflow for Docker apps](/dotnet/architecture/microservices/docker-application-development-process/docker-app-development-workflow)**<br>
        Describes the inner-loop development workflow for Docker container-based applications.
    :::column-end:::
:::row-end:::

![Basic Docker taxonomy infographic for containers, images, and registries](./images/taxonomy-of-docker-terms-and-concepts.png)

[Tutorial: Containerize a .NET Core app](https://docs.microsoft.com/dotnet/core/docker/build-container?tabs=windows)

[Docker support in Visual Studio](https://docs.microsoft.com/visualstudio/containers/overview#docker-support-in-visual-studio-1)

[Docker docs: Overview](https://docs.docker.com/engine/docker-overview/)
[Docker docs: Dockerfile commands](https://docs.docker.com/engine/reference/builder/)

[Review the Azure services that support containers](https://azure.microsoft.com/overview/containers/)