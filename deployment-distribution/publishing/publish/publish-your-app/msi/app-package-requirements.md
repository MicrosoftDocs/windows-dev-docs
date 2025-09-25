---
description: Prepare your MSI/EXE app's packages for submission to the Microsoft Store by following these guidelines. Be aware that the store enforces specific rules related to version numbers, which may vary across different OS versions. Additionally, you can refer to a table of supported languages and their corresponding language codes for app submission.
title: App package requirements for MSI/EXE app
ms.date: 10/30/2022
ms.topic: article
ms.localizationpriority: medium
---

# App package requirements for MSI/EXE app
## Requirements

Submit an HTTPS-enabled download URL (direct link) to the product’s installer binaries. Products submitted in this manner are subject to the following requirements:

- The installer binary may only be an .msi or .exe.

- The binary and all of its Portable Executable (PE) files must be digitally signed with a code signing certificate that chains up to a certificate issued by a Certificate Authority (CA) that is part of the [Microsoft Trusted Root Program](/security/trusted-root/participants-list).

- You must submit a versioned download URL in Partner Center. The binary associated with that URL must not change after submission.

- Whenever you have an updated binary to distribute, you must provide an updated versioned download URL in Partner Center associated with the updated binary. You are responsible for maintaining and updating the download URL.

- Initiating the install must not display an installation user interface (i.e., silent install is required), however a User Account Control (UAC) dialog is allowed.

- The installer is a standalone installer and is not a downloader stub/web installer that downloads bits when run.

## Package version numbering

You can manage the package version numbering through your installer. Package version numbering for Win32 is not supported through the Store.
