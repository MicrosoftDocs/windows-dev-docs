---
title: Test your Windows app before you ship
description: A testing path for Windows apps, from running on a clean device to accessibility checks, the Windows App Certification Kit, UI automation, and AI-assisted testing.
ms.date: 11/10/2026
ms.topic: how-to
author: GrantMeStrength
ms.author: jken
ms.localizationpriority: medium
---

# Test your Windows app before you ship

Testing before you ship catches the problems that most often cause failed Store submissions and support requests. Work through these steps in order. Each step ends with a check that you, or an AI agent, can confirm.

## 1. Run the app on a clean device

Install your release package on a clean test device or virtual machine that hasn't had your development tools or builds installed. Install only the prerequisites documented for users, plus the public development certificate if needed for sideload testing. A new user account on your development machine doesn't remove machine-wide dependencies or certificate trust.

**Verify:** The package installs, the app launches, and your core scenarios work.

## 2. Check accessibility

Test your core scenarios with only the keyboard, and with a screen reader such as Narrator. Use [Accessibility Insights for Windows](https://accessibilityinsights.io/docs/windows/overview/) to find missing names and other common issues. See [Accessibility testing](../design/accessibility/accessibility-testing.md).

**Verify:** You can complete every core scenario without a mouse, and Narrator announces each control.

## 3. Run the Windows App Certification Kit

The [Windows App Certification Kit](/windows/uwp/debug-test-perf/windows-app-certification-kit) runs many of the same tests used in Store certification.

**Verify:** The kit reports no failures, or you understand and accept each one.

## 4. Automate UI tests

Automate the scenarios you test every release, so you can rerun them for each update. For unit tests, UI automation with Appium and WinAppDriver, and running tests in CI, see [Test apps built with the Windows App SDK and WinUI 3](testing/index.md).

**Verify:** Your UI tests pass in your build pipeline.

## 5. Test with an AI assistant

An AI assistant can help you write tests, run them, and review the results. See [AI-assisted testing for Windows apps](ai-assisted/testing.md). Review the tests an agent writes, and don't treat agent results as a replacement for testing with real users and assistive technology.

**Verify:** You've reviewed each test the agent wrote, and its results match what you see when you test the app yourself.

## Next steps

- [Microsoft Store readiness checklist](../publish/store-readiness-checklist.md)
- [Fix Microsoft Store submission and certification failures](../publish/fix-submission-failures.md)
