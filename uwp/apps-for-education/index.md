---
title: Develop apps for education.
description: This section describes the Universal Windows Apps resources that are available to write Education apps for the Windows 10 and Windows 11 platforms.
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, education
ms.assetid: 2431f253-efe3-4895-b131-34653b61f13c
ms.localizationpriority: medium
---

# Develop Universal Windows apps for education

![take-a-test app screenshot](images/take-a-test-screen-small.png)

The following resources will help you write a Universal Windows app for education.

## Accessibility

Education apps need to be accessible. See [Developing apps for accessibility](https://developer.microsoft.com/windows/accessible-apps) for more information.

## Secure assessments

Assessment/testing apps will often need to produce a *locked down* environment in order to prevent students from using other computers or Internet resources during a test. This functionality is available through the [Take a Test API](take-a-test-api.md). See the [Take a Test](/education/windows/take-tests-in-windows-10) web app in the Windows IT Center for an example of a testing environment with locked down online access for high-stakes testing.

## User input

User input is a critical part of education apps; UI controls must be responsive and intuitive so as not to break the focus of their users. For a general overview of the input options available in a Universal Windows app, see the [Input primer](/windows/apps/design/input/input-primer) and the topics below it in the Design & UI section. Additionally, the following sample apps showcase basic UI handling in the Universal Windows Platform.

- [Basic input sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/BasicInput) shows how to handle input in Universal Windows Apps.
- [User interaction mode sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/UserInteractionMode) shows how to detect and respond to the user interaction mode.
- [Focus visuals sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/XamlFocusVisuals) shows how to take advantage of the new system drawn focus visuals or create your own custom focus visuals if the system drawn ones do not fit your needs.

The Windows Ink platform can make education apps shine by fitting them with an input mode that students are accustomed to. See [Pen interactions and Windows Ink](/windows/apps/design/input/pen-and-stylus-interactions) and the topics below it for a comprehensive guide to implementing Windows Ink in your app. The following sample apps provide working examples of this API.

- [Ink Analysis sample](https://github.com/microsoft/Windows-universal-samples/tree/main/Samples/InkAnalysis) demonstrates how to use ink functionality (such as capturing, manipulating, and interpreting ink strokes) in Universal Windows apps using JavaScript.
- [Simple ink sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/SimpleInk) demonstrates how to use ink functionality (such as capturing ink from user input and performing handwriting recognition on ink strokes) in Universal Windows apps using C#.
- [Complex ink sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/ComplexInk) demonstrates how to use advanced InkPresenter functionality to interleave ink with other objects, select ink, copy/paste, and handle events. It is built upon the Universal Windows Platform in C++ and can run on Desktop and Mobile Windows 10 and Windows 11 SKUs.

## Microsoft Store

Education apps are often released under special circumstances to a specific organization. See [Distribute line-of-business apps to enterprises](/windows/apps/publish/distribute-lob-apps-to-enterprises) for information on this.

## Related Topics

- [Windows 10/11 for Education](/education/windows/index) on the Windows IT Center
