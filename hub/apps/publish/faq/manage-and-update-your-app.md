---
title: Manage and Update Your App - FAQ
description: Learn how to manage, update, and improve your Microsoft Store app after publishing. Topics include app submissions, rollouts, flights, metadata changes, and managing in-app purchases.
ms.date: 06/18/2025
ms.topic: faq
---

# Manage and Update Your App

This section provides guidance for developers looking to update or manage their published Microsoft Store apps, including controlled rollouts, flights, listing changes, and in-app product updates.

---

<details>
<summary><strong>How do I release an update or new version of my app on the Microsoft Store?</strong></summary>

To release an update, create a new submission in Partner Center for your app:

1. Go to your app's Overview page and select **Update** (or **Create a new submission**).
2. Upload new app packages (e.g., MSIX, APPX), or modify listing info like pricing or descriptions.
3. Submit the update for certification.

Once approved, the new version replaces the previous one in the Store. Existing users will receive the update via the Store automatically.

</details>

---

<details>
<summary><strong>Can I gradually roll out an app update to a percentage of my users?</strong></summary>

Yes. Partner Center supports **gradual rollout** for updates only for MSIX apps:

- During the submission process, enable **Roll out update gradually** and set an initial percentage (e.g., 5%).
- After publishing, increase the rollout percentage or halt it from the Overview page.
- Halting stops further updates but doesn’t revert the app for users who already received it.

This helps catch issues early and ensures a more stable update for all users.

</details>

---

<details>
<summary><strong>What are package flights and how can I test updates with a limited group of users?</strong></summary>

**Package flights** allow you to distribute test versions of your app (MSIX apps only) to specific groups:

- Set up a flight group (e.g., internal testers).
- Upload a new package for that group, separate from the public version.
- Only designated users receive the update; others continue with the public release.

Flights go through certification but allow testing new features or bug fixes before wide release.

</details>

---

<details>
<summary><strong>How can I change my app’s Store listing details or price after it’s published?</strong></summary>

To change listing details:

1. Create a new submission via the **Update** button in Partner Center.
2. Modify description, screenshots, category, pricing, etc.
3. No need to upload a new package if you're only editing metadata.

These changes still go through certification before being published to ensure Store policy compliance.

</details>

---

<details>
<summary><strong>My app has in-app products (add-ons). How do I manage or update those after publishing?</strong></summary>

To manage add-ons:

- Go to the **Add-ons** section in Partner Center.
- Select the add-on, click **Update**, and start a new submission to change its listing, price, or content.

Add-on updates are certified like app updates. You can also track performance using the **Add-on acquisitions** report. Always keep your in-app product listings accurate and policy-compliant.

</details>

<br>

> [!TIP]
> For detailed information about **How to manage and update your app**, please see the [Manage and update your app](../publish-your-app/msix/publish-update-to-your-app-on-store.md) section.
