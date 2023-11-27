---
ms.date: 5/22/2023
ms.topic: include
ms.prod: windows
---

You can manually run each of the package validation tests on your local machine. This can help diagnose validation failures, or can be done in advance to ensure your app will pass once you're ready to submit your app.

## Silent install

1. Download and locate the app installer onto your PC.
    > [!NOTE]
    > If you already have the app installer on your PC, you can use that installer instead of downloading a duplicate copy. Please ensure that the existing installer is **identical** to the version you are testing.
1. Open a command prompt and navigate to the installer location.
1. Run your installer, making sure top provide the silent installation parameter, if required.
    > [!NOTE]
    > For MSI apps, use `/qn` as the silent install parameter.
1. Your app should be installed without any user interaction.

    > [!NOTE]
    > UAC (User Account Control) prompts are allowed.

## Entry in add or remove programs

1. Repeat the steps for verifying silent install from the above section.
2. Open Control Panel -> Programs -> Programs and Features.
3. Verify the app name, publisher name and app version added by your app.
4. The entry for your product should not show a blank or unrelated Name or Publisher.

## Bundleware

1. Repeat the steps for verifying silent install from the above section.
1. Open Control Panel -> Programs -> Programs and Features.
1. Your app should only add a single entry to the programs list. If your app has added multiple entries, it means that your app is installing bundleware.
