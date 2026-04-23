---
description: Product declarations help make sure your MSI/EXE app is displayed appropriately in the Microsoft Store and offered to the right set of customers.
title: Product declarations for MSI/EXE app
ms.date: 10/30/2022
ms.topic: article
ms.localizationpriority: medium
---

# Product declarations for MSI/EXE app

You can check boxes in this section to indicate if any of the declarations apply to your app. This may affect the way your app is displayed, whether it is offered to certain customers, or how customers can use it.

:::image type="content" source="images/msiexe-product-declarations.png" lightbox="images/msiexe-product-declarations.png" alt-text="A screenshot of the Properties section where you can provide product declarations for your app.":::

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
> **This product supports pen and ink input.**

If your app supports pen and ink input, you can check this box which makes your app discoverable to customers who are specifically looking for pen and ink input supported apps in the Store.

## Notes for certification

**Notes for certification**<br>Character limit: 2000<br>_Recommended_

As you submit your app, you have the option to use the Notes for certification page to provide additional info to the certification testers. This info can help ensure that your app is tested correctly. Including these notes is particularly important for products that use dependencies on non-Microsoft drivers or NT services and/or that require logging in to an account. If we cannot fully test your submission, it may fail certification.

:::image type="content" source="images/msiexe-notes-for-certification.png" lightbox="images/msiexe-notes-for-certification.png" alt-text="A screenshot of the Properties section where you can provide notes for certification for your app.":::

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
