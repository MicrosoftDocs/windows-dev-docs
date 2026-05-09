---
title: SmartScreen reputation for Windows app developers
description: Understand how Windows Defender SmartScreen reputation works, what to expect when you publish a new app, and how to minimize warnings for your users.
ms.topic: concept-article
ms.date: 05/04/2026
ms.localizationpriority: medium
---

# SmartScreen reputation for Windows app developers

Microsoft Defender SmartScreen checks the reputation of downloaded files before allowing them to run. Understanding how reputation works can help avoid warnings when users download or run your files.

> [!TIP]
> **The simplest way to avoid SmartScreen warnings is to publish through the Microsoft Store.** Store-distributed apps are signed by a Microsoft certificate and are never subject to SmartScreen download warnings. The remainder of this article applies to apps distributed outside the Store.

## How SmartScreen reputation works

SmartScreen evaluates two signals when a user downloads and runs a file:

1. **Publisher reputation** — Is the file signed? Is the signing certificate from a known, trusted publisher?
2. **File hash reputation** — Has this specific file been downloaded by users without indications of malicious behavior?

A negative or unknown reputation for a file's hash or its publisher's certificate can cause warnings to show. Even when signed, a newly created binary could still show a SmartScreen warning until its hash or publisher certificate accumulates sufficient evidence of positive reputation.

When a file is not signed, SmartScreen reputation must build for each new version of your files, starting with zero reputation. Reputation cannot transfer from previous versions unless both were signed using the same publisher identity.

## Certificate options and their SmartScreen implications
To reduce the likelihood of interruption, you should sign all of your files with a valid certificate.

| Certificate type | First-download SmartScreen behavior |
|---|---|
| Microsoft Store | ✅ No warning — covered by Microsoft's certificate |
| Valid Certificate (OV/EV) | ⚠️ Warning — app flagged as unrecognized until reputation accumulates; verified publisher name is displayed |
| No signature | ⚠️ Warning — "Windows protected your PC"; User must choose "Run anyway" before the app can run. Enterprise policy can prevent  continuation entirely. |
| Self-signed Certificate | ⚠️ Warning — Same behavior as no signature |

> [!NOTE]
> EV certificates no longer bypass SmartScreen. Years ago, signing files with an Extended Validation (EV) code signing certificate would result in positive SmartScreen reputation by default, but this behavior no longer exists. EV certificates may matter for enterprise procurement, but they no longer impact SmartScreen behavior. Paying a premium for EV solely to avoid SmartScreen warnings is no longer justified.

### Microsoft Store (recommended)

Apps published through the Microsoft Store are re-signed by Microsoft and carry full reputation. Users will never see a SmartScreen warning for a Store-installed app.

### Artifact Signing (formerly Trusted Signing)

[Artifact Signing](/azure/trusted-signing/) (formerly Trusted Signing) is Microsoft's recommended code signing service for non-Store distribution:

- **Cost:** Approximately $10/month
- **No hardware token required** — integrates directly with CI/CD pipelines (GitHub Actions, Azure DevOps)
- **Identity validation required** — Microsoft validates your identity before issuing certificates
- **SmartScreen behavior** — reputation accumulates over time based on download volume and behavior

## What to expect when you publish a new app

1. **First downloads:** Users may see a SmartScreen prompt indicating the app is unrecognized. For signed apps, the publisher name is displayed. Users should proceed only after verifying the source.
2. **As downloads accumulate:** SmartScreen reputation builds up automatically. The prompt will stop appearing once the file hash has sufficient download history. There is no exact threshold, but it can take several weeks and hundreds of clean installs from a wide audience.
3. **New version:** Signing files using a trusted certificate can allow certificate reputation to build, potentially avoiding warnings on new files signed by the same trusted certificate. Unsigned files must build reputation anew with every update.

There is no need (or mechanism) to manually submit a file for SmartScreen reputation review for consumer endpoints. Reputation builds organically through download volume.

> [!NOTE]
> Enterprise environments may have different SmartScreen behavior depending on policy configuration; for example, the ability to bypass a SmartScreen warning may be disabled. Enterprises may distribute files from Trusted Intranet locations not subject to SmartScreen review. Enterprise IT administrators may optionally submit files for review via the [Microsoft Security Intelligence portal](https://www.microsoft.com/en-us/wdsi/filesubmission). This can accelerate trust for internal or managed deployments.

## Minimizing SmartScreen warnings in practice

- **Publish to the Microsoft Store** where feasible — this is the most reliable way to avoid warnings entirely
- **Sign every release** — unsigned files cannot inherit a positive reputation from the signing certificate
- **Do not modify signed files** - Avoid modifying files after signing as doing so can [break the signature](/windows/win32/secbp/understanding-pe-signatures#security-implications) depending on client configuration
- **Do not sign potentially unwanted applications** - Avoid signing any file which exhibits [malicious or potentially unwanted application](/unified-secops/criteria) behavior, or the certificate may develop **negative** reputation
- **Use a consistent signing identity** — changing your signing certificate affects the publisher trust signal
- **Communicate with early adopters** — for new apps, let beta users know they may see a SmartScreen prompt on first download, and that they should only proceed after verifying the publisher and confirming they trust the download source

> [!TIP]
> On Windows 11 devices, the Smart App Control feature may supersede SmartScreen Application Reputation. Smart App Control will block execution of unsigned files unless the file has a positive reputation. Smart App Control signature checks apply to all executable files, not just those downloaded from the Internet.

## Related content

- [Choose a distribution path for your Windows app](choose-distribution-path.md)
- [Current status of Windows app distribution features](distribution-feature-status.md)
- [Sign an app package using SignTool](/windows/msix/package/sign-app-package-using-signtool)
- [Artifact Signing (formerly Trusted Signing) documentation](/azure/trusted-signing/)
- [Microsoft Trusted Root Program requirements](/security/trusted-root/program-requirements)
