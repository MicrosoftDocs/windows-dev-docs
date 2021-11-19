---
description: The app properties page for your MSI or EXE app lets you define your app's category and indicate hardware preferences or other declarations.
title: Specify your MSI or EXE app's properties
ms.assetid: C423B766-28D0-4D67-BF44-CFDCE5C4B202
ms.date: 10/31/2018
ms.topic: article
keywords: windows 10, windows 11, windows, windows store, store, msi, exe, unpackaged, unpackaged app, desktop app, traditional desktop app, game settings, display mode, system requirements, hardware requirements, minimum hardware, recommended hardware, privacy policy, support contact info, app website, support info
ms.localizationpriority: medium
---

# Specify your MSI or EXE app's properties

> [!NOTE]
> MSI and EXE support in the Microsoft Store is currently in a limited public preview phase. As the size of the preview expands, we'll be adding new participants from the wait list. To join the wait list, click [here](https://aka.ms/storepreviewwaitlist).

App properties describe important details about your app including requirements, capabilities, and your contact information.

**Category and Subcategory**<br>*Category is required*

Categories and subcategories help users discover your app, and they help users understand what your app does.

Choose the category that best describes your app. If that category includes subcategories, select the one that best describes your app. If none of the available subcategories seem to fit, you can leave subcategory blank, or choose a subcategory you think customers who would want your app would be most likely to browse.

To change the category or subcategory of an app that's already in the Store, create a new submission and select the new category or subcategory.

| Category                   | Subcategory                         |
|----------------------------|-------------------------------------|
| Books + reference          | E-reader<br>Fiction<br>Nonfiction<br>Reference |
| Business                   | Accounting + finance<br>Collaboration<br>CRM<br>Data + analytics<br>File management<br>Inventory + logistics<br>Legal + HR<br>Project management<br>Remote desktop<br>Sales + marketing<br>Time + expenses |
| Developer tools            | Database<br>Design tools<br>Development kits<br>Networking<br>Reference + training<br>Servers<br>Utilities<br>Web hosting |
| Education                  | Books + reference<br>Early learning<br>Instructional tools<br>Language<br>Study aids |
| Entertainment              | (None)                              |
| Food + dining              | (None)                              |
| Government + politics      | (None)                              |
| Health + fitness           | (None)                              |
| Kids + family              | Books + reference<br>Entertainment<br>Hobbies + toys<br>Sports + activities<br>Travel  |
| Lifestyle                  | Automotive<br>DIY<br>Home + garden<br>Relationships<br>Special interest<br>Style + fashion |
| Medical                    | (None)                              |
| Multimedia design          | Illustration + graphic design<br>Music production<br>Photo + video production |
| Music                      | (None)                              |
| Navigation + maps          | (None)                              |
| News + weather             | News<br>Weather                     | 
| Personal finance           | Banking + investments<br>Budgeting + taxes  |
| Personalization            | Ringtones + sounds<br>Themes<br>Wallpaper + lock screens |
| Photo + video              | (None)                              |
| Productivity               | (None)                              |
| Security                   | PC protection<br>Personal security  |
| Shopping                   | (None)                              |
| Social                     | (None)                              |
| Sports                     | (None)                              |
| Travel                     | City guides<br>Hotels               |
| Utilities + tools          | Backup + manage<br>File managers    |

## Support info section

This section lets you provide info to help customers understand more about your app and how to get support.

**Does this product access, collect or transmit personal information (data that could be used to identity a person)?**<br>*Required*

You are responsible for ensuring your app complies with applicable privacy laws and regulations, and for providing a valid privacy policy URL here if required.

In this section, you must indicate whether your app accesses, collects, or transmits any [personal information](/legal/windows/agreements/store-policies#105-personal-information). If you answer Yes, a privacy policy URL is required. Otherwise, it is optional (though if we determine that your app requires a privacy policy, and you have not provided one, your submission may fail certification).

To help you determine if your app requires a privacy policy, review the [App Developer Agreement](/legal/windows/agreements/app-developer-agreement) and the [Microsoft Store Policies](/legal/windows/agreements/store-policies#105-personal-information).

> [!NOTE]
> Microsoft does not provide a default privacy policy for your app, and your app is not covered by any Microsoft privacy policy.

**Privacy policy URL**<br>*Required only if you answered yes to the previous question*

*See above*

**Website**<br>*Recommended*

Enter the URL of the web page for your app. This URL must point to a page on your own website, not your app's web listing in the Store. This field is optional but recommended.

**Support contact info**<br>*Recommended*

Enter the URL of the web page where your customers can go for support with your app, or an email address that customers can contact for support. We recommend including this info for all submissions, so that your customers know how to get support if they need it. Note that Microsoft does not provide your customers with support for your app.

## Product declarations section

You can check boxes in this section to indicate if any of the declarations apply to your app. This may affect the way your app is displayed, whether it is offered to certain customers, or how customers can use it.

**This app depends on non-Microsoft drivers or NT services.**

If your app depends on non-Microsoft drivers or NT services, you can check this box and provide info in Notes for Certification section.  

**This app has been tested to meet accessibility guidelines.**

Checking this box makes your app discoverable to customers who are specifically looking for accessible apps in the Store.

You should only check this box if you have done all the following items:

- Set all the relevant accessibility info for UI elements, such as accessible names.
- Implemented keyboard navigation and operations, tab order, keyboard activation, arrow key navigation, and shortcuts.
- Ensured an accessible visual experience by using a 4.5:1 text contrast ratio, and not relying on color alone to convey info to the user.
- Used accessibility testing tools, such as Inspect or AccChecker, to verify your app and resolve all high-priority errors detected by those tools.
- Verified the app’s key scenarios from end to end using tools such as Narrator, Magnifier, On Screen Keyboard, High Contrast, and High DPI.

When you declare your app as accessible, you agree that your app is accessible to all customers, including those with disabilities. For example, this means you have tested the app with high-contrast mode and with a screen reader. You've also verified that the user interface functions correctly with a keyboard, the Magnifier, and other accessibility tools.

> [!IMPORTANT]
> Do not list your app as accessible unless you have specifically engineered and tested it for that purpose. If your app is declared as accessible, but it does not support accessibility, you'll probably receive negative feedback from the community.

**This product supports pen and ink input.**

If your app supports pen and ink input, you can check this box which makes your app discoverable to customers who are specifically looking for pen and ink input supported apps in the Store.  

**Notes for certification**<br>Character limit: 2000<br>*Recommended*

As you submit your app, you have the option to use the Notes for certification page to provide additional info to the certification testers. This info can help ensure that your app is tested correctly. Including these notes is particularly important for products that use dependencies on non-Microsoft drivers or NT services and/or that require logging in to an account. If we cannot fully test your submission, it may fail certification.

Make sure to include the following (if applicable for your app):

- **Dependency on non-Microsoft drivers or NT services**: If you indicated a dependency in the previous question, describe it here. For each dependency, tell us why your app needs to declare the dependency and how it is used. Be sure to provide as much detail as possible to help us understand why your product needs to declare the dependency.

  During the certification process, our testers will review the info you have provided to determine whether your submission is approved to use the dependency. Note that this may add some additional time for your submission to complete the certification process. If we approve your use of the dependency, your app will continue through the rest of the certification process. You generally will not have to repeat the dependency approval process when you submit updates to your app (unless you declare additional dependencies).

  If we do not approve your use of the dependency, your submission will fail certification, and we will provide feedback in the certification report. You then have the option to create a new submission and provide URLs to packages which do not declare the dependency, or, if applicable, address any issues related to your use of the dependency and request approval in a new submission.

- **Usernames and passwords for test accounts**: If your app requires users to log in to a service or social media account, provide the username and password for a test account. The certification testers will use this account when reviewing your app.

- **Steps to access hidden or locked features**: Briefly describe how testers can access any features, modes, or content that might not be obvious. Apps that appear to be incomplete may fail certification.

- **Steps to verify background audio usage**: If your app allows audio to run in the background, testers may need instructions on how to access this feature so they can confirm it functions appropriately.

- **Expected differences in behavior based on region or other customer settings**: For example, if customers in different regions will see different content, make sure to call this out so that testers understand the differences and review appropriately.

- **Info about what's changed in an app update**: For updates to previously published apps, you may want to let the testers know what has changed, especially if your packages are the same and you're just making changes to your app listing (such as adding more screenshots, changing your app's category, or editing the description).

- **The date you're entering the notes**: Seeing the date helps testers evaluate whether there were any temporary issues that may no longer apply.

- **Anything else you think testers will need to understand about your submission.** When considering what to write, remember:
  - **A real person will read these notes**. Testers will appreciate a polite, clear, and helpful instructions.
  - **Be succinct and keep instructions simple**. If you really need to explain something in detail, you can include the URL to a page with more info. However, keep in mind that customers of your app will not see these notes. If you feel that you need to provide complicated instructions for testing your app, consider whether your app could be simplified so that customers (and testers) will know how to use it.
  - **Services and external components must be online and available**. If your app needs to connect to a service to function, make sure that the service will be online and available. Include any information about the service that testers will need, such as login info. If your app cannot connect to a service it needs during testing, it may fail certification.

## System requirements section

**System Requrements** (Touch screen, Keyboard, Mouse, Camera, NFC HCE, NFC Proximity, Bluetooth LE, Telephony, Microphone, Memory, DirectX, Video Memory, Processor, Graphics)

In this section, you have the option to indicate if certain hardware features are required or recommended to run and interact with your app properly.

Selected requirements will be displayed in your product's Store listing as required hardware. The Store may also display a warning to customers who are viewing your app's listing on a device that does not have the required hardware. Users who do not have the required hardware will be able to download your app, but they will not be able to rate or review your app on those devices.

Recommended hardware will be displayed in your product's Store listing as recommended hardware.

> [!TIP]
> You'll have a chance to specify additional system requirements, such as 3D printers or USB devices, later in the submission process.
