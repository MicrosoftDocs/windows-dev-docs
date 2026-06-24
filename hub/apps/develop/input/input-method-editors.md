---
description: An Input Method Editor (IME) is a software component that enables a user to input text in a language that can't be represented easily on a standard QWERTY keyboard.
title: Input Method Editors (IME)
label: Input Method Editors (IME)
template: detail.hbs
keywords: ime, input method editor, input, interaction
ms.date: 07/17/2025
ms.topic: article
ms.localizationpriority: medium
---

# Input Method Editors (IME)

An Input Method Editor (IME) is a software component that supports text input in edit controls for characters in languages that can't be represented easily on a standard QWERTY keyboard, such as those of various East Asian languages.

Instead of each character appearing on a dedicated keyboard key, a combination of keystrokes are interpreted as a composite character by the IME. The IME generates the character that matches the set of key strokes (or provides a list of candidate characters to select from). The composite character is then inserted into the edit control.

> [!NOTE]
> IMEs can support both hardware keyboards and on-screen keyboards (OSK) such as the touch keyboard.

Your app doesn't need to interact directly with the IME. The IME is built into the system, just as the touch keyboard is. If your app has text input, and you intend to support text input in languages that require an IME, you should test the end-to-end customer experience for text entry. This lets you fix any issues, such as adjusting your UI so it isn't occluded by the touch keyboard or IME candidate window.

## Creating an IME

To enable a great input experience for all users, Microsoft produces IMEs that ship in-box for a variety of languages.

In addition to the in-box IMEs, you can build your own custom IMEs that users can install and use just like an in-box IME.

All IMEs run in the Windows system, which is hardened to stop malicious IMEs and to improve the security and user experience of all IMEs.

Custom IMEs can link to the default touch keyboard and use its layout so that end users can use their IME with the touch keyboard. However, you cannot provide your own independent touch keyboard and certain functions of in-box IMEs for touch keyboards are not available to custom IMEs.

## Requirements for IMEs

A third-party IME must meet these requirements:

- Must be digitally signed
- Must be [Text Services Framework (TSF)](/windows/win32/tsf/text-services-framework) aware, with appropriate IME flags set correctly
- Must follow the guidelines described in [Input Method Editor (IME) requirements](input-method-editor-requirements.md) and [Design and code Windows apps](../../design/index.md)

A third-party IME that doesn't meet these requirements is blocked from running.

Because Windows Defender removes malicious IMEs from the system, it's important to familiarize yourself with the IME coding requirements. For more info, see [Input Method Editor (IME) requirements](input-method-editor-requirements.md).

## Design guidelines for IMEs

Read the [Input Method Editor (IME) requirements](input-method-editor-requirements.md) for more details on best practices and design guidelines for IMEs. In general, all IME UIs need to:

- Follow the UX guidelines for Windows Runtime apps
- Avoid modal experiences and only show the IME window when needed
- include icons that are black and white only

## Related topics

- [Input Method Editor (IME) requirements](input-method-editor-requirements.md)
- [ITfFnGetPreferredTouchKeyboardLayout](/windows/win32/api/ctffunc/nn-ctffunc-itffngetpreferredtouchkeyboardlayout)
- [ITfCompartmentEventSink](/windows/win32/api/msctf/nn-msctf-itfcompartmenteventsink)
- [ITfThreadMgrEx::GetActiveFlags](/windows/win32/api/msctf/nf-msctf-itfthreadmgrex-getactiveflags)
- [ITfContextView::GetWnd](/windows/win32/api/msctf/nf-msctf-itfcontextview-getwnd)
- [TF_INPUTPROCESSORPROFILE](/windows/win32/api/msctf/ns-msctf-tf_inputprocessorprofile)
- [SendInput](/windows/win32/api/winuser/nf-winuser-sendinput)
