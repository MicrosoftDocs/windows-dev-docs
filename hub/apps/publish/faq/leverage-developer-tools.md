---
title: Leverage Developer Tools - FAQ
description: Learn how to monitor your app’s analytics, performance, and automate submissions using the Microsoft Store Developer CLI.
ms.date: 06/18/2025
ms.topic: faq
---

# Leverage Developer Tools

This section answers frequently asked questions about using analytics in Partner Center and leveraging the Microsoft Store Developer CLI for publishing and automation.

<details>
<summary><strong>What analytics and insights does Partner Center provide for my app after it’s published?</strong></summary>

Once your app is live, **Partner Center** provides a powerful analytics dashboard with detailed reports and data visualizations, covering:

- **Acquisitions (Installs):** Track how many users are downloading your app, where they're coming from (search, direct links, etc.), and which devices or OS versions they use.
- **Usage:** See how users interact with your app, including session length, frequency of use, and custom telemetry events.
- **Health (Quality):** Monitor app stability through crash reports and error tracking, with detailed stack traces and failure types.
- **Ratings & Reviews:** View and respond to user feedback in the Store, including sorting and filtering by market or date.

All reports can be viewed online or exported as Excel/CSV files for further analysis. Developers can also access this data through APIs or CLI tools for integration with custom dashboards. These insights help identify areas for improvement, measure update impact, and better understand your audience.

</details>

<details>
<summary><strong>What is the Microsoft Store Developer CLI and how can it help me?</strong></summary>

The **Microsoft Store Developer CLI** is a cross-platform command-line tool that allows developers to automate many Partner Center tasks, such as:

- Listing and retrieving app information
- Uploading new packages
- Updating Store metadata
- Submitting and publishing app updates

This tool is particularly useful for **CI/CD pipelines**, where new builds can be automatically submitted and published. Authentication is done using Entra ID credentials linked to your Partner Center account.

Although still in preview, the CLI offers a flexible alternative to the web UI and supports scripting workflows across Windows, macOS, and Linux. To use it, developers must first configure API access with appropriate permissions. With this tool, teams can significantly streamline and scale their release operations.

</details>

<details>
<summary><strong>What are product page experiments?</strong></summary>

Product page experiments allow you to perform A/B tests on your app’s visuals, like icons or screenshots, to see which performs best with users.

</details>

<details>
<summary><strong>How do I set up product page experiments?</strong></summary>

Set them up via Partner Center, specify visual asset variations, and monitor their performance to select the most effective visuals.

</details>


<details>
<summary><strong>What is package flighting?</strong></summary>

Package flighting lets you distribute app updates to a selected group of users for testing, without impacting the general audience.

</details>

<details>
<summary><strong>How do I create a package flight?</strong></summary>

Upload a package in Partner Center, select specific users for testing, and promote successful builds to all users when ready.

</details>

<details>
<summary><strong>How can I use promo codes to promote my app?</strong></summary>

Generate promo codes in Partner Center to distribute free access to your app or add-ons for promotions, reviews, or testing.

</details>

<details>
<summary><strong>What types of promo codes are available?</strong></summary>

Single-use codes (one redemption per user) and multi-use codes (multiple redemptions up to a defined limit).

</details>

<details>
<summary><strong>What is the Microsoft Store Web Installer?</strong></summary>

A streamlined installer allowing users to download and install your app directly from the web without opening the Store app.

</details>

<details>
<summary><strong>How can I implement the Web Installer?</strong></summary>

Generate a Microsoft Store badge with Web Installer integration from the Store badge generator and embed it on your site.

</details>

<details>
<summary><strong>What is the Microsoft Store CLI?</strong></summary>

A command-line interface tool for managing Store submissions, updates, and metadata directly from your terminal or scripts.

</details>

<details>
<summary><strong>Can I automate Store submissions with CLI?</strong></summary>

Yes, the Store CLI integrates with CI/CD pipelines, enabling automated submissions and app management.

</details>

<details>
<summary><strong>What is WNS?</strong></summary>

WNS enables you to send push notifications (toasts, tiles, badges) to your Windows apps via Microsoft's notification infrastructure.

</details>

<details>
<summary><strong>How do I send notifications using WNS?</strong></summary>

Acquire a channel URI in your app, authenticate with WNS via your server, and send notifications directly to user devices.

</details>

<details>
<summary><strong>How should I package my app for distribution?</strong></summary>

Use MSIX for modern packaging, automatic updates, and full Store support, or traditional EXE/MSI for existing installers.

</details>

<details>
<summary><strong>What are the advantages of using MSIX?</strong></summary>

Automatic updates, clean install/uninstall experiences, Store-managed hosting, and built-in signing.

</details>

<details>
<summary><strong>What tools does Microsoft offer to engage customers?</strong></summary>

Customer segmentation, targeted push notifications via Azure Notification Hubs, promotional campaigns, and responding to user reviews.

</details>

<details>
<summary><strong>Can I respond to customer reviews?</strong></summary>

Yes, Partner Center lets you directly respond to user reviews publicly or via email, improving user engagement.

</details>
