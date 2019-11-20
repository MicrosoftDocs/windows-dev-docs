---
title: Export restrictions on cryptography
description: Use this info to determine if your app uses cryptography in a way that might prevent it from being listed in the Microsoft Store.
ms.assetid: 204C7D1D-6F08-4AEE-A333-434D715E7617
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, security
ms.localizationpriority: medium
---
# Export restrictions on cryptography



Use this info to determine if your app uses cryptography in a way that might prevent it from being listed in the Microsoft Store.

The Bureau of Industry and Security in the United States Department of Commerce regulates the export of technology that uses certain types of encryption. All apps listed in the Microsoft Store must comply with these laws and regulations because the app files can be stored in the United States. Even apps that are uploaded by app developers from other countries for distribution outside of the United States must comply with these regulations. Consequently, when submitting an app to the Microsoft Store, all app developers must make sure that their apps don't contain any technology that is restricted by these regulations.

> **Note**  The information provided here provides some guidance, but it is your responsibility as the app developer who is publishing apps in the Microsoft Store to make sure that your app complies with all applicable laws and regulations.

 

For more info about the U.S. Department of Commerce and the Bureau of Industry and Security, see [About the Bureau of Industry and Security](https://www.bis.doc.gov/about/index.htm).

For info about the Export Administration Regulations (EAR) that govern the export of technology that includes encryption, see [EAR Controls for Items That Use Encryption](https://www.bis.doc.gov/index.php/policy-guidance/encryption).

## Governed uses

First, determine if your app uses a type of cryptography that is governed by the Export Administration Regulations. The question includes the examples shown in the list here; but remember that this list doesn't include every possible application of cryptography.

> **Important**  Consider not only the code you wrote for your app, but also all the software libraries, utilities and operating system components that your app includes or links to.

-   Any use of a digital signature, such as authentication or integrity checking
-   Encryption of any data or files that your app uses or accesses
-   Key management, certificate management, or anything that interacts with a public key infrastructure
-   Using a secure communication channel such as NTLM, Kerberos, Secure Sockets Layer (SSL), or Transport Layer Security (TLS)
-   Encrypting passwords or other forms of information security
-   Copy protection or digital rights management (DRM)
-   Antivirus protection

For the complete and current list of cryptographic applications, see [EAR Controls for Items That Use Encryption](https://www.bis.doc.gov/index.php/policy-guidance/encryption).

## Non-restricted uses

Note that some of the applications of cryptography are not restricted. Here are the unrestricted tasks:

-   Password encryption
-   Copy protection
-   Authentication
-   Digital rights management
-   Using digital signatures

For the complete and current list of cryptographic applications, see [EAR Controls for Items That Use Encryption](https://www.bis.doc.gov/index.php/policy-guidance/encryption).

If your app calls, supports, contains, or uses cryptography or encryption for any task that is not in this list, it needs an Export Commodity Classification Number (ECCN).

If you don't have an ECCN, see [ECCN Questions and Answers](https://www.bis.doc.gov/licensing/do_i_needaneccn.html).
