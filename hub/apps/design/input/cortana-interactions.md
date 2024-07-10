---
description: Extend the basic functionality of **Cortana** with voice commands that launch and execute a single action in a Windows application.
title: Cortana interactions in Windows apps
ms.assetid: 4C11A7CF-DA26-4CA1-A9B9-FE52670101F5
label: Cortana
template: detail.hbs
keywords: Cortana, Cortana canvas, Cortana design, user interface, voice commands, VCD
ms.date: 01/27/2021
ms.topic: article
ms.localizationpriority: medium
---

# Cortana interactions in Windows apps

>[!WARNING]
> This feature is no longer supported as of the Windows 10 May 2020 Update (version 2004, codename "20H1").
>
> See [Cortana in Microsoft 365](/microsoft-365/admin/misc/cortana-integration) for how Cortana is transforming modern productivity experiences.

Extend the basic functionality of **Cortana** with voice commands that launch and execute a single action in a Windows application.

The target app can be launched in the foreground (the app takes focus and **Cortana** is dismissed) or activated in the background (**Cortana** retains focus but provides results from the app), depending on the complexity of the interaction. Generally, voice commands that require additional context or user input are best handled in a foreground app, while basic commands can be handled in **Cortana** through a background app.

By integrating the basic functionality of your app, and providing a central entry point for the user to accomplish most of the tasks without opening your app directly, **Cortana** becomes a liaison between your app and the user. Providing this shortcut to app functionality and reducing the need to switch apps, can save the user significant time and effort.

> [!NOTE]
> A voice command is a single utterance with a specific intent, defined in a Voice Command Definition (VCD) file, directed at an installed app via **Cortana**.
>
> A VCD file defines one or more voice commands, each with a unique intent.
>
> Voice command definitions can vary in complexity. They can support anything from a single, constrained utterance to a collection of more flexible, natural language utterances, all denoting the same intent.

## Other speech and conversation components

### Speech, voice, and conversation in Windows 10

See [Speech, voice, and conversation in Windows 10](../../develop/speech.md) for information on how the various Windows development frameworks provide speech recognition, speech synthesis, and conversation support for developers building Windows applications.

## Related articles

- [VCD elements and attributes v1.2](/uwp/schemas/voicecommands/voice-command-elements-and-attributes-1-2)
- [Cortana design guidelines](cortana-design-guidelines.md)
- [Cortana voice command sample](https://go.microsoft.com/fwlink/p/?LinkID=619899)