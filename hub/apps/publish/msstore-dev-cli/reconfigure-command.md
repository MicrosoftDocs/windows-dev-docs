---
description: How to run the Microsoft Store Developer CLI (preview) reconfigure command.
title: Microsoft Store Developer CLI (preview) - Reconfigure Command
ms.date: 12/02/2022
ms.topic: article
ms.localizationpriority: medium
---

# Reconfigure Command

Re-configure the Microsoft Store Developer CLI. You can provide either a Client Secret or a Certificate. Certificates can be provided either through its Thumbprint or by providing a file path (with or without a password).

## Usage

```console
msstore reconfigure
```

## Options

| Option | Description |
|--------|-------------|
| -t, --tenantId | Specify the tenant Id that should be used. |
| -s, --sellerId | Specify the seller Id that should be used. |
| -c, --clientId | Specify the client Id that should be used. |
| -cs, --clientSecret | Specify the client Secret that should be used. |
| -ct, --certificateThumbprint | Specify the certificate Thumbprint that should be used. |
| -cfp, --certificateFilePath | Specify the certificate file path that should be used. |
| -cp, --certificatePassword | Specify the certificate password that should be used. |
| --reset | Only reset the credentials, without starting over. |