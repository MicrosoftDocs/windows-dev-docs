---
title: Set up your Microsoft Store developer account early
description: Start Microsoft Store developer registration and verification while you build your app, so account setup doesn't delay your first submission.
ms.date: 11/10/2026
ms.topic: concept-article
author: GrantMeStrength
ms.author: jken
ms.localizationpriority: medium
---

# Set up your Microsoft Store developer account early

You can't submit an app to the Microsoft Store until your developer account is created and verified. Many verification steps finish in minutes, but some go to manual review. Start registration when you start building your app, not when it's ready to ship.

This article explains what to decide before you register, what can slow you down, and what to do if verification stalls. For the step-by-step registration flow, see [Open a developer account](partner-center/open-a-developer-account.md).

## Choose the right account type

| Account type | Choose it when | Sign in with |
|---|---|---|
| Individual | You publish under your own name, and your Store distribution isn't related to your business, trade, or profession. | A personal Microsoft account |
| Company | You publish under the name of a registered business or organization, or your Store distribution is related to your business, trade, or profession. | A personal Microsoft account or a Microsoft Entra ID work account |

Choose carefully: you can't change an Individual account to a Company account. To publish as a company later, you need to create a new Company account.

> [!NOTE]
> To publish updates from the command line with the [Microsoft Store Developer CLI](msstore-dev-cli/overview.md), your Partner Center account needs an associated Microsoft Entra ID tenant, and you sign in to the CLI with Microsoft Entra ID credentials, not a personal Microsoft account. You can associate an existing tenant or create a new one in Partner Center. See the [CLI prerequisites](msstore-dev-cli/overview.md#prerequisites).

Both account types are free when you register through [storedeveloper.microsoft.com](https://storedeveloper.microsoft.com), which is the only supported entry point for the current onboarding flow.

## Gather what verification needs

- **Individual accounts:** A government-issued photo ID and a selfie, captured on a mobile device in good lighting with the original document.
- **Company accounts:** A D-U-N-S number (recommended, for faster automated verification) or official business documents, plus a work email address on your organization's domain. If your email domain doesn't match your organization, you might be asked for domain ownership documents.

Make sure the details you enter match your documents. Changing key details later, such as company name, address, or email domain, restarts verification.

## Know what to expect

- Many verification steps complete automatically within seconds to a minute.
- Steps that go to manual review typically take 2-5 business days. Partner Center emails you when the status changes or when you need to take action.
- For Company accounts, mandatory due diligence must pass before business and employment verification can continue.
- After your account is created, it can take up to 30 minutes for your verification status to appear across Partner Center.

While you wait, keep building. You can't submit until verification is complete, but you can finish your app, work through the [Store readiness checklist](store-readiness-checklist.md), and prepare your Store listing.

## If verification stalls or is rejected

1. Check the **Verification summary** page in Partner Center for required actions, and watch for email from Microsoft.
2. If additional documents are requested, upload current, official documents that match the details you entered. For Company accounts, business and employment verification each allow up to three appeals, so check your documents before you submit.
3. If you can't resolve the issue in Partner Center, [raise a support ticket](https://aka.ms/windowsdevelopersupport).

## For AI agents and automation

A person must complete account registration, identity and business verification, and acceptance of agreements. An AI agent can remind the developer to start registration early and help them prepare their app and listing while verification is in progress, but it must not attempt these steps on the developer's behalf.

## Next steps

- [Open a developer account](partner-center/open-a-developer-account.md)
- [Store readiness checklist](store-readiness-checklist.md)
- [Reserve your app's name](publish-your-app/msix/reserve-your-apps-name.md)
