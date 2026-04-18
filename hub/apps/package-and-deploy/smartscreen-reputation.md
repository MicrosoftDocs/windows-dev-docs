---
title: SmartScreen reputation for Windows app developers
description: Understand how Windows Defender SmartScreen reputation works, what to expect when you publish a new app, and how to minimize warnings for your users.
ms.topic: concept-article
ms.date: 04/17/2026
ms.localizationpriority: medium
---

# SmartScreen reputation for Windows app developers

Windows Defender SmartScreen checks the reputation of downloaded files before allowing them to run. Understanding how reputation works helps you set the right expectations for your users and choose the right signing strategy.

> [!TIP]
> **The simplest way to avoid SmartScreen warnings is to publish through the Microsoft Store.** Store-distributed apps carry Microsoft's certificate and are never subject to SmartScreen download warnings. Everything in this article applies to apps distributed outside the Store.

## How SmartScreen reputation works

SmartScreen evaluates two things when a user downloads a file:

1. **Publisher reputation** — Is the signing certificate from a known, trusted publisher?
2. **File hash reputation** — Has this specific file been downloaded by enough users without being reported as malicious?

Both signals are required for a clean (no-warning) download experience. A new signed binary from a trusted publisher will still receive a SmartScreen prompt until its hash accumulates sufficient download history.

SmartScreen reputation is **per file hash** — every new build of your app starts with zero reputation. Reputation does not transfer from previous versions.

## What changed in 2024: EV certificates no longer bypass SmartScreen

Historically, Extended Validation (EV) code signing certificates granted **immediate SmartScreen reputation** — an EV-signed binary would show no warning even on first download. This behavior was removed in 2024 when Microsoft updated the Trusted Root Program requirements.

**Current behavior (as of 2024):**

| Certificate type | First-download SmartScreen behavior |
|---|---|
| No signature | ❌ Strong block — "Windows protected your PC"; additional user confirmation may be required before the app can run. Enterprise policy can prevent this confirmation entirely. |
| Self-signed | ❌ Strong block — cert not trusted by default; same behavior as unsigned |
| OV certificate (Organization Validated) | ⚠️ Warning — app flagged as unrecognized until reputation accumulates; publisher name is displayed as verified |
| EV certificate (Extended Validation) | ⚠️ Warning — same as OV for new files (no longer instant bypass) |
| Azure Artifact Signing (formerly Trusted Signing) certificate | ⚠️ Warning for new files; reputation accumulates normally |
| Microsoft Store | ✅ No warning — covered by Microsoft's certificate |

EV certificates still provide value (they require more identity validation, which may matter for enterprise procurement), but they no longer provide instant SmartScreen bypass. Paying a premium for EV solely to avoid SmartScreen warnings is no longer justified.

## Certificate options and their SmartScreen implications

### Microsoft Store (recommended)

Apps published through the Microsoft Store are re-signed by Microsoft and carry full reputation. Users will never see a SmartScreen warning for a Store-installed app.

### Azure Artifact Signing (formerly Trusted Signing)

[Azure Artifact Signing (formerly Trusted Signing)](/azure/trusted-signing/) is Microsoft's recommended code signing service for non-Store distribution:

- **Cost:** Approximately $10/month — significantly lower than traditional CA certificates
- **No hardware token required** — integrates directly with CI/CD pipelines (GitHub Actions, Azure DevOps)
- **Identity validation required** — Microsoft validates your organization identity before issuing certificates
- **SmartScreen behavior:** Same as OV certs — reputation accumulates over time based on download volume

### OV and EV certificates from traditional CAs

Traditional code signing certificates from Certificate Authorities (DigiCert, Sectigo, etc.) are also accepted. OV certificates typically cost $150–300/year; EV certificates $400+/year. Both now have equivalent SmartScreen behavior for new files.

If you already have an OV or EV certificate, it remains valid and functional. If you're purchasing a new certificate, Azure Artifact Signing (formerly Trusted Signing) is typically the better choice for Windows app distribution.

## What to expect when you publish a new app

1. **First downloads:** Users may see a SmartScreen prompt indicating the app is unrecognized. For signed apps, the publisher name is displayed — the warning is about low file reputation, not an unknown publisher. Users should proceed only after verifying the source.
2. **As downloads accumulate:** SmartScreen reputation builds up automatically. The prompt will stop appearing once the file hash has sufficient download history. Based on developer reports, this typically takes **several weeks and hundreds of clean installs** — there is no exact threshold Microsoft publishes.
3. **New version:** Each new build starts fresh — reputation does not carry over from the previous version's hash.

There is no way to manually submit a file for SmartScreen reputation review for consumer endpoints. Reputation builds organically through download volume.

> [!TIP]
> For enterprise environments, IT administrators can submit files for review via the [Microsoft Security Intelligence portal](https://www.microsoft.com/en-us/wdsi/filesubmission). This can accelerate trust for internal or managed deployments, but does not affect consumer SmartScreen behavior.

> [!NOTE]
> Enterprise environments managed by Microsoft Defender for Endpoint or Windows Defender Application Control (WDAC) may have different SmartScreen behavior depending on policy configuration. IT administrators can allowlist specific publisher certificates or file hashes to bypass SmartScreen checks for managed devices.

## Minimizing SmartScreen warnings in practice

- **Publish to the Microsoft Store** where feasible — this is the most reliable way to avoid warnings entirely
- **Sign every release** — unsigned files show a stronger SmartScreen warning than signed files, and enterprises may block unsigned binaries entirely
- **Use a consistent signing identity** — changing your signing certificate affects the publisher trust signal; note that each new build's hash also starts with no file reputation regardless of certificate continuity
- **Use Azure Artifact Signing (formerly Trusted Signing)** for non-Store distribution — it's cost-effective and integrates with automated build pipelines
- **Communicate with early adopters** — for new apps, let beta users know they may see a SmartScreen prompt on first download, and that they should only proceed after verifying the publisher and confirming they trust the download source

## Related content

- [Choose a distribution path for your Windows app](choose-distribution-path.md)
- [Current status of Windows app distribution features](distribution-feature-status.md)
- [Sign an app package using SignTool](/windows/msix/package/sign-app-package-using-signtool)
- [Azure Artifact Signing (formerly Trusted Signing) documentation](/azure/trusted-signing/)
- [Microsoft Trusted Root Program requirements](/security/trusted-root/program-requirements)
