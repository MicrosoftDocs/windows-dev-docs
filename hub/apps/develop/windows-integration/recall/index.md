---
title: Recall overview
description: Learn how to use the AI-assisted Recall feature with the User Activity API in Windows.
ms.date: 11/17/2025
ms.topic: overview
no-loc: [Recall, Click to Do, Microsoft Foundry on Windows, Phi Silica]
---

# Recall overview

**Recall** utilizes [Microsoft Foundry on Windows](../overview.md) to help you find anything you've seen on your PC. Search using any clues you remember or use the timeline to scroll through your past activity, including apps, documents, and websites. Once you've found what you're looking for, you can quickly jump back to the content seen in the snapshot by selecting the relaunch button below the screenshot. The **UserActivity API** is what allows apps to provide deep links, so you can pick up where you left off.

[Learn more about Recall](https://support.microsoft.com/windows/retrace-your-steps-with-recall-aa03f8a0-a78b-4b3e-b0a1-2eb8ac48701c), including:

- System Requirements
- How to use Recall
- How to search with Recall
- How content interaction (with ["Screenray"](https://support.microsoft.com/windows/retrace-your-steps-with-recall-aa03f8a0-a78b-4b3e-b0a1-2eb8ac48701c#:~:text=Recall%20opens%20the%20snapshot%20and,cursor%20is%20blue%20and%20white.)) works
- How to pause or resume snapshots
- How to filter certain websites or apps from being saved by Recall
- How to manage Recall snapshots and disk space
- Keyboard shortcuts

Learn more about [Privacy and control over your Recall experience](https://support.microsoft.com/windows/privacy-and-control-over-your-recall-experience-d404f672-7647-41e5-886c-a3c59680af15), including:

- Controls on how to manage your Recall and snapshots preferences
- How to filter apps and websites from your snapshots
- How snapshot storage works (content stays local)
- Built-in security included with the secured-core PC, Microsoft Pluton security processor, and Windows Hello Enhanced Sign-in Security (ESS)

For IT Administrators, learn how to [Manage Recall](/windows/client-management/manage-recall) using Windows Client Management, including:

- System requirements
- Supported browsers
- How to configure policies for Recall
- Limitations
- User controlled settings for Recall
- Storage allocation
- Policy setting for control over whether Windows saves snapshots of the screen and analyzes user activity on the device: [DisableAIDataAnalysis](/windows/client-management/mdm/policy-csp-windowsai#disableaidataanalysis)

Recall updates:

- [GA release of Recall on Copilot+ PCs (April 25, 2025)](https://blogs.windows.com/windowsexperience/2025/04/25/copilot-pcs-are-the-most-performant-windows-pcs-ever-built-now-with-more-ai-features-that-empower-you-every-day/)
- [Previewing Recall with Click to Do on Copilot+ PCs with Windows Insiders in the Dev Channel (November 22, 2024)](https://blogs.windows.com/windows-insider/2024/11/22/previewing-recall-with-click-to-do-on-copilot-pcs-with-windows-insiders-in-the-dev-channel/)
- [Update on Recall security and privacy architecture (September 27, 2024)](https://blogs.windows.com/windowsexperience/2024/09/27/update-on-recall-security-and-privacy-architecture/)
- [Update on the Recall preview feature for Copilot+ PCs (June 7, 2024)](https://blogs.windows.com/windowsexperience/2024/06/07/update-on-the-recall-preview-feature-for-copilot-pcs/).

## Prerequisites

- A [Copilot+ PC](/windows/ai/npu-devices/) from Qualcomm, Intel, or AMD.
  - Arm64EC (Emulation Compatible) is not currently supported.
- The April 2025 Windows non-security preview update or later must be installed on your device.
  - Consumers with Copilot+ PCs can be among the first to experience these new features by going to: Settings > Windows Update and turning on "Get the latest updates as soon as they're available." Then select "Check for Updates" to download and install the April non-security preview release. In some cases, features may be provided via a separate update.

## Use Recall in your Windows app

For users who opt-in by [enabling "Recall & snapshots" in Settings > Privacy & security](https://support.microsoft.com/windows/privacy-and-control-over-your-recall-experience-d404f672-7647-41e5-886c-a3c59680af15#:~:text=You%20can%20turn%20on%20or,and%20selecting%20the%20pause%20option), Windows will regularly save snapshots of the customer's screen and store them locally. Using screen segmentation and image recognition, Windows provides the power to gain insight into what is visible on the screen. Users of your Windows applications will now be able to semantically search these saved snapshots and find content related to your app.

To improve the Recall experience for your app, you can [enable relaunching of content within your app](./recall-relaunch.md).

![Screenshot of the Recall interface showing a Redbarn Sale Analysis app sample.](../images/recall-redbarn.png)

## Temporarily suspend saving snapshots

Some apps might need to temporarily suspend saving of Recall snapshots.

To prevent Recall saving content, apps can follow existing guidance to prevent screen capture of their content.

For example, your app can use [SetWindowDisplayAffinity](/windows/win32/api/winuser/nf-winuser-setwindowdisplayaffinity) to set the display affinity to `WDA_MONITOR`. This ensures that the window content is only displayed on a monitor. Everywhere else, including Recall, the window appears with no content.

Web browser apps that support a concept of "InPrivate" mode should [see the guidance for web browsers](./recall-web-browsers.md).

## See also

- [Enable relaunching your content from Recall](./recall-relaunch.md)
- [Provide sensitivity labels to Recall with UserActivity ContentInfo](./recall-contentinfo-labels.md) - For enterprise DLP integration
- [Recall DLP Provider API](./dlp-provider-api.md) - For DLP vendor integration
- [Guidance for developers of web browsers](./recall-web-browsers.md)
- [Click to Do](../click-to-do.md)
- [Developing Responsible Generative AI Applications and Features on Windows](../rai.md)
