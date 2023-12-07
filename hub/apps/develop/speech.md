---
title: Speech, voice, and conversation in Windows 11 and Windows 10
description: This page provides the information for you to get started developing speech-enabled Windows apps.
ms.topic: article
ms.date: 11/28/2023
keywords: Speech in Windows 10, speech, voice, conversation, win32 speech apps, UWP speech apps, WPF speech apps, WinForms speech apps
ms.author: kbridge
author: Karl-Bridge-Microsoft
---

# Speech, voice, and conversation in Windows 11 and Windows 10

![Speech hero image](images/hero-speech-composite-small.png)

Speech can be an effective, natural, and enjoyable way for people to interact with your Windows applications, complementing, or even replacing, traditional interaction experiences based on mouse, keyboard, touch, controller, or gestures.

Speech-based features such as speech recognition, dictation, speech synthesis (also known as text-to-speech or TTS), and conversational voice assistants (such as Cortana or Alexa) can provide accessible and inclusive user experiences that enable people to use your applications when other input devices might not suffice.

This page provides information on how the various Windows development frameworks provide speech recognition, speech synthesis, and conversation support for developers building Windows applications.

## Platform-specific documentation

:::row:::
   :::column:::
      ![Universal Windows Platform (UWP)](images/platform-uwp.png)

      **Universal Windows Platform (UWP)**

      Build speech-enabled apps on the modern platform for Windows 10 (and later) applications and games, on any Windows device (including PCs, phones, Xbox, HoloLens, and more), and publish them to the Microsoft Store.

      [Speech interactions](/windows/uwp/design/input/speech-interactions)

      [Speech recognition](/windows/uwp/design/input/speech-recognition)

      [Continuous dictation](/windows/uwp/design/input/enable-continuous-dictation)

      [Speech synthesis](/uwp/api/windows.media.speechsynthesis)

      [Conversational agents](/uwp/api/windows.applicationmodel.conversationalagent)

      [Cortana voice commands](/cortana/voice-commands/vcd)<br>
      (not supported in Windows 10 May 2020 Update and newer)
   :::column-end:::
   :::column:::
      ![Win32 platform apps](images/platform-win32.png)

      **Win32 platform**

      Develop speech-enabled applications for Windows desktop and Windows Server using the tools, information, and sample engines and applications provided here.

      [Microsoft Speech Platform - Software Development Kit (SDK) (Version 11)](https://www.microsoft.com/download/details.aspx?id=27226)
      
      [Microsoft Speech SDK, version 5.1](https://www.microsoft.com/download/details.aspx?id=10121)
   :::column-end:::
:::row-end:::
:::row:::
   :::column:::
      ![.NET](images/platform-dotnet.png)

      **.NET Framework**

      Develop accessible apps and tools on the established platform for managed Windows applications with a XAML UI model and the .NET Framework.

      [System.Speech Programming Guide for .NET Framework](/previous-versions/office/developer/speech-technologies/hh361625(v=office.14))
   :::column-end:::
   :::column:::
      ![Azure speech services](images/platform-azure-speech.png)

      **Azure speech services**

      Integrate speech processing into apps and services.

      [Speech to text](https://azure.microsoft.com/services/cognitive-services/speech-to-text/)

      [Text to speech](https://azure.microsoft.com/services/cognitive-services/text-to-speech/)
      
      [Speech translation](https://azure.microsoft.com/services/cognitive-services/speech-translation/)

      [Speaker Recognition](https://azure.microsoft.com/services/cognitive-services/speaker-recognition/)

      [Voice-first virtual assistants](/azure/cognitive-services/speech-service/voice-first-virtual-assistants)
   :::column-end:::
:::row-end:::
:::row:::
   :::column span="2":::
      **Legacy features**

      Legacy, deprecated, and/or unsupported versions of Microsoft speech and conversation technology.
   :::column-end:::
:::row-end:::
:::row:::
   :::column:::
      [Cortana Skills Kit](/cortana/skills/)

      As part of our goal to transform the modern productivity experiences by embedding Cortana deeply into [Microsoft 365](/microsoft-365/admin/misc/cortana-integration), we are retiring the Cortana Skills Kit developer platform and all skills built on this platform.
   :::column-end:::
   :::column:::

      [Microsoft Agent](/windows/win32/lwef/microsoft-agent)

      [Microsoft Speech API (SAPI) 5.3](/previous-versions/windows/desktop/ms723627(v=vs.85))

      [Microsoft Speech API (SAPI) 5.4](/previous-versions/windows/desktop/ee125663(v=vs.85))

      [The Bing Speech Recognition Control](/previous-versions/bing/speech/dn434583(v=msdn.10))
   :::column-end:::
:::row-end:::

## Samples

Download and run full Windows samples that demonstrate various accessibility features and functionality.

:::row:::
   :::column:::
      [Code sample browser](/samples/browse/?term=speech)

      The new samples browser (replaces the MSDN Code Gallery).
   :::column-end:::
   :::column:::
      [Windows classic samples on GitHub](https://github.com/microsoft/Windows-classic-samples/search?q=speech&unscoped_q=speech)

      These samples demonstrate the functionality and programming model for Windows and Windows Server. 
   :::column-end:::
:::row-end:::
:::row:::
   :::column:::
      [Universal Windows Platform (UWP) samples on GitHub](https://github.com/microsoft/Windows-universal-samples/search?q=speech&unscoped_q=speech)

      These samples demonstrate the API usage patterns for the Universal Windows Platform (UWP) in the Windows Software Development Kit (SDK) for Windows 10 and later.
   :::column-end:::
   :::column:::
      [WinUI 2 Gallery](https://github.com/Microsoft/WinUI-Gallery)

      This app demonstrates the various Xaml controls supported in the Fluent Design System.
   :::column-end:::
:::row-end:::


## Other resources

:::row:::
   :::column:::
      **Blogs and news**

      The latest from the world of Microsoft speech.
   :::column-end:::
   :::column:::
      **Community and support**

      Where Windows developers and users meet and learn together.
   :::column-end:::
:::row-end:::
:::row:::
   :::column:::
      [In the news](https://news.microsoft.com/?s=speech)

      [Speech blogs](https://blogs.windows.com/windowsdeveloper/?s=speech)
   :::column-end:::
   :::column:::
      [Windows community - Speech](https://answers.microsoft.com/en-us/search/search?SearchTerm=windows%20speech)

      [Windows Speech Developer's Forum](https://social.msdn.microsoft.com/Forums/home?filter=alltypes&sort=firstpostdesc&searchTerm=speech)

      [Stack Overflow](https://stackoverflow.com/questions/tagged/windows+speech)

   :::column-end:::
:::row-end:::
