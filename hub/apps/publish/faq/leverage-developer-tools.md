---
title: Leverage Developer Tools - FAQ
description: Learn how to monitor your app’s analytics, performance, and automate submissions using the Microsoft Store Developer CLI.
ms.date: 06/18/2025
ms.topic: faq
---

# Leverage Developer Tools

This section answers frequently asked questions about using analytics in Partner Center and leveraging the Microsoft Store Developer CLI for publishing and automation.

---

<details>
<summary><strong>What analytics and insights does Partner Center provide for my app after it’s published?</strong></summary>

Once your app is live, **Partner Center** provides a powerful analytics dashboard with detailed reports and data visualizations, covering:

- **Acquisitions (Installs):** Track how many users are downloading your app, where they're coming from (search, direct links, etc.), and which devices or OS versions they use.
- **Usage:** See how users interact with your app, including session length, frequency of use, and custom telemetry events.
- **Health (Quality):** Monitor app stability through crash reports and error tracking, with detailed stack traces and failure types.
- **Ratings & Reviews:** View and respond to user feedback in the Store, including sorting and filtering by market or date.

All reports can be viewed online or exported as Excel/CSV files for further analysis. Developers can also access this data through APIs or CLI tools for integration with custom dashboards. These insights help identify areas for improvement, measure update impact, and better understand your audience.

</details>

---

<details>
<summary><strong>What is the Microsoft Store Developer CLI and how can it help me?</strong></summary>

The **Microsoft Store Developer CLI** is a cross-platform command-line tool that allows developers to automate many Partner Center tasks, such as:

- Listing and retrieving app information
- Uploading new packages
- Updating Store metadata
- Submitting and publishing app updates

This tool is particularly useful for **CI/CD pipelines**, where new builds can be automatically submitted and published. Authentication is done using Azure Active Directory credentials linked to your Partner Center account.

Although still in preview, the CLI offers a flexible alternative to the web UI and supports scripting workflows across Windows, macOS, and Linux. To use it, developers must first configure API access with appropriate permissions. With this tool, teams can significantly streamline and scale their release operations.

</details>
