---
title: Get started with Java on Windows
description: A guide to help you set up a Java development environment on Windows, including JDK installation, JAVA_HOME configuration, and recommended tools.
ms.topic: get-started
ms.date: 03/27/2026
---

# Get started with Java on Windows

This guide covers what's different about setting up Java on Windows compared to Mac or Linux, and gets you to a working JDK installation with your environment configured correctly.

## Choose a JDK

Several JDK distributions are available for Windows. Microsoft publishes its own build based on OpenJDK:

- **[Microsoft Build of OpenJDK](https://www.microsoft.com/openjdk)** — free, open source, supported by Microsoft, available for Windows x64 and ARM64
- **[Eclipse Temurin (Adoptium)](https://adoptium.net/)** — widely used open-source distribution
- **[Oracle JDK](https://www.oracle.com/java/technologies/downloads/)** — requires a license for commercial use

For most developers, Microsoft Build of OpenJDK or Temurin are good choices.

## Install with winget

Open a PowerShell or Command Prompt terminal and run:

```powershell
winget install Microsoft.OpenJDK.21
```

To install Temurin instead:

```powershell
winget install EclipseAdoptium.Temurin.21.JDK
```

Verify the installation:

```powershell
java -version
```

## Set JAVA_HOME

Unlike on macOS (where `/usr/libexec/java_home` handles this), on Windows you need to set `JAVA_HOME` manually. Many tools — Maven, Gradle, Android Studio — require it.

1. Open **Start**, search for **Environment Variables**, and select **Edit the system environment variables**.
2. Click **Environment Variables**.
3. Under **System variables**, click **New** and set:
   - Variable name: `JAVA_HOME`
   - Variable value: the path to your JDK, for example `C:\Program Files\Microsoft\jdk-21.0.x.x-hotspot`
4. Find the **Path** variable under System variables, click **Edit**, and add `%JAVA_HOME%\bin`.
5. Click OK to close all dialogs, then open a new terminal and verify:

```powershell
echo $env:JAVA_HOME
java -version
javac -version
```

> [!TIP]
> If you have multiple JDKs installed, the one listed first in `Path` takes precedence. Tools like [SDKMAN](https://sdkman.io/) (via WSL) or [jEnv](https://www.jenv.be/) can help manage multiple versions.

## WSL or native Windows?

For most Java development — web backends, Android, enterprise apps — native Windows works well. Use WSL if your build tooling or deployment target is Linux-specific, or if you're working in a team where everyone else is on Linux/Mac and you want environment parity.

## Recommended editors

- **[Visual Studio Code](https://code.visualstudio.com/)** with the [Extension Pack for Java](https://marketplace.visualstudio.com/items?itemName=vscjava.vscode-java-pack) — lightweight, works well for most projects
- **[IntelliJ IDEA](https://www.jetbrains.com/idea/)** — full-featured IDE, the Community edition is free

## Next steps

> [!div class="nextstepaction"]
> [Java on Azure and Windows documentation](/java/)

- [Java for Beginners (video series)](/shows/java-for-beginners/)
- [Learn Java (java.com)](https://dev.java/learn/)
- [Maven in 5 minutes](https://maven.apache.org/guides/getting-started/maven-in-five-minutes.html)
- [Gradle quickstart](https://docs.gradle.org/current/userguide/getting_started_eng.html)
- [Java in Visual Studio Code](https://code.visualstudio.com/docs/languages/java)
