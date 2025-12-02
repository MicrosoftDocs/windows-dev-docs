---
title: Click to Do overview
description: Learn how to use the AI-assisted Click to Do feature in Windows.
ms.author: mattwoj
author: mattwojo
ms.date: 08/05/2025
ms.topic: overview
no-loc: [Click to Do, Windows Copilot Runtime, Phi Silica]
---

# Click to Do overview

**Click to Do** is an AI-supported feature that utilizes the local Phi Silica model in Copilot+ PCs or [eligible Cloud PCs](https://learn.microsoft.com/en-us/windows-365/enterprise/ai-enabled-cloud-pcs) to connect actions to the content (text or images) on the screen.

Click to Do analyzes what's on screen and then you can choose the text or image you want to take action on. Once selected, Click to Do will suggest actions that you can take based on the content. For example, if you select a piece of text, it might suggest summarizing or rewriting it. If you select an image, it might suggest blurring the background, erasing objects in the image, or searching the web for related information.

Developers can launch Click to Do from their app using the URI scheme `ms-clicktodo://`. This allows the app users to interact with the Click to Do feature directly from their application, enhancing the user experience by providing quick access to AI-assisted actions.

Learn more about [Click to Do](https://support.microsoft.com/windows/click-to-do-in-recall-do-more-with-what-s-on-your-screen-967304a8-32d1-4812-a904-fad59b5e6abf).

## Launch Click to Do

To launch the Click to Do feature on a Copilot+ PC from your app, you use the following URI scheme: `ms-clicktodo://`

The `ms-clicktodo://` URI enables your app to programmatically launch Click to Do, placing an interactive overlay on top of the PC screen. This overlay suggests quick actions to appear for images or text. The analysis of the screen is always performed locally on the device. Content is only shared if the user chooses to complete an action. Content is not saved, nor is it automatically passed back to the app used to open the overlay. This URI does not accept any additional parameters.

The following code samples open Click to Do from the user's app:

### [C#](#tab/csharp)

```csharp
var clickToDoUri = new Windows.Foundation.Uri(L"ms-clicktodo://");  
 
Windows.System.Launcher.LaunchUriAsync(clickToDoUri)
```

### [C++/WinRT](#tab/cpp)

```cppwinrt
winrt::Windows::Foundation::Uri clickToDoUri(L"ms-clicktodo://");  
 
winrt::Windows::System::Launcher::LaunchUriAsync(clickToDoUri);
```

---

## Related content

- [Recall](./recall/index.md)
 
- [Developing Responsible Generative AI Applications and Features on Windows](./rai.md)
