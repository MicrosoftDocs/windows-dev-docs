---
title: Leverage Developer Tools - FAQ
description: Learn how to monitor your app’s analytics, performance, and automate submissions using the Microsoft Store Developer CLI.
ms.date: 08/21/2025
ms.topic: faq
---

# Leverage Developer Tools

This section answers frequently asked questions about using analytics in Partner Center and leveraging the Microsoft Store Developer CLI for publishing and automation.

---

<details>
<summary><strong>What developer tools can I leverage to grow my app on the Store?</strong></summary>

Partner Center offers several tools to help your app succeed:

- For MSIX apps:
  - **Package flights:** Test app updates with a select group before wider release.
  - **Product page experiments (A/B testing):** Optimize your Store listing visuals to improve conversion rates.
  - **Promotional codes:** Distribute free or discounted copies of your app or add-ons for promotions and marketing.
  - **Microsoft Store Developer CLI:** Automate app submissions, updates, and metadata management directly from your development workflow or CI/CD pipelines.

- For MSIX and MSI apps:
  - **Microsoft Store Web Installer:** Simplify user acquisition by enabling direct web-based installation without manual Store interaction.

These tools collectively help enhance your app’s quality, visibility, and user acquisition strategy.

</details>

---

<details>
<summary><strong>What is the Microsoft Store Developer CLI and how can it help me?</strong></summary>

The **Microsoft Store Developer CLI** is a cross-platform command-line tool that allows developers to automate many Partner Center tasks, such as:

- Listing and retrieving app information
- Uploading new packages
- Updating Store metadata
- Submitting and publishing app updates

This tool is particularly useful for **CI/CD pipelines**, where new builds can be automatically submitted and published. Authentication is done using Entra ID credentials linked to your Partner Center account.

It offers a flexible alternative to the web UI and supports scripting workflows across Windows, macOS, and Linux. To use it, developers must first configure API access with appropriate permissions. With this tool, teams can significantly streamline and scale their release operations.

</details>

---

<details>
<summary><strong>Can I automate Store submissions with the CLI?</strong></summary>

Yes, the CLI supports automation of app submissions. Integrated into build pipelines, it enables automated packaging, submission, and publishing of new app releases without manual intervention. This significantly reduces time and error rates, ideal for teams managing frequent updates or numerous applications.

</details>

---

<details>
<summary><strong>What are product page experiments?</strong></summary>

**Product page experiments** are A/B tests that allow developers to test variations of their app’s Store listing elements, like icons and screenshots, to determine which perform best. The Microsoft Store splits incoming user traffic between the original page and the new variant. Metrics like impressions, views, installs, and conversion rates are then measured to identify the most effective visuals, helping you optimize your Store listing to boost app engagement and downloads.

</details>

---

<details>
<summary><strong>How do I set up product page experiments?</strong></summary>

To set up a product page experiment:

1. **Start the experiment:** Sign in to Partner Center, go to your app’s Overview, and create a new product page experiment.
2. **Choose elements to test:** Provide alternative app logos or screenshots for your experiment.
3. **Submit for approval:** Once approved, the experiment runs (typically up to 90 days), dividing traffic evenly between your original and variant pages.
4. **Analyze results:** Use Partner Center analytics to compare impressions, page views, install counts, and conversion rates.
5. **Apply successful changes:** Adopt the most successful visual assets from the experiment to permanently improve your app’s Store listing.

</details>

---

<details>
<summary><strong>What is package flighting?</strong></summary>

**Package flighting** allows developers to distribute app updates to a specific group of users before releasing the update to everyone. It’s ideal for beta testing or validating updates with early adopters. Flighted packages are delivered to a predefined user group, while all other users continue receiving the current publicly available version. This helps ensure stability and quality before a broad rollout.

So a **package flight** lets you distribute app updates to a limited group of testers without affecting your broader audience. To create a package flight:

- Go to your app’s **Overview** in Partner Center.
- In the **Package flights** section, click **New package flight**.
- Name your flight and create or choose an existing tester group by adding their Microsoft account emails.
- Add or upload app packages specific to the flight.
- Submit the flight for certification.

Once certified, only your selected testers receive the update, while other users continue with the publicly available version.

</details>

---

<details>
<summary><strong>How can I use promo codes to promote my app and what types are available?</strong></summary>

Promo codes enable you to offer free access to your app or add-ons to selected customers, influencers, reviewers, or beta testers. Generated in Partner Center, promo codes come with unique redeemable URLs. Distributing these codes helps drive awareness, generate user reviews, incentivize engagement, and reward loyal customers.

Microsoft Store offers two types of promo codes:

- **Single-use codes:** One-time codes redeemable by a single user each.
- **Multi-use codes:** A single code redeemable by multiple users, up to a set limit.

These codes help developers tailor promotional campaigns to specific marketing goals or audiences.

</details>

---

<details>
<summary><strong>What is the Microsoft Store Web Installer and how can implement it?</strong></summary>

The **Microsoft Store Web Installer** is a small executable (.exe) installer that allows users to install apps directly from the web without opening the Microsoft Store app. When users click an install link on a web page, the installer checks system eligibility and seamlessly downloads and installs the app from the Store backend. This simplifies the installation process for free apps distributed via websites.

Implement the Web Installer by embedding a Microsoft Store badge with direct installation on your website:

- Use the [Microsoft Store badge generator](https://apps.microsoft.com/badge).
- Choose the "Direct" launch mode to ensure users trigger the Web Installer directly from your website.

Users clicking your badge will then automatically download and launch your app installer without needing to open the Store app.

</details>

---

<details>
<summary><strong>What tools does Microsoft offer to engage customers?</strong></summary>

Microsoft provides several tools to engage customers via Partner Center, including:

- **Create customer groups** that include a subset of your app's customers for promotion, testing, and other purposes
- **Targeted offers** for personalized promotions
- **Respond to reviews** mechanisms for customer relationship management

These tools allow developers to enhance user engagement, retention, and satisfaction.

</details>

---

<details>
<summary><strong>Can I respond to customer reviews?</strong></summary>

Yes. Developers can respond directly to customer reviews through Partner Center. Public responses appear alongside reviews on the Store page. Engaging professionally and constructively with reviews can greatly improve customer relations and foster a positive community around your app.

</details>

---

<details>
<summary><strong>What can I do through the Microsoft Store submission API?</strong></summary>

The Microsoft Store submission API enables developers to automate app management tasks, including:

- Creating and submitting new app updates.
- Uploading and managing app packages.
- Updating metadata and Store listings.

You can integrate this API into your continuous integration or build workflows, automating routine tasks to streamline your publishing process.

</details>

---

<details>
<summary><strong>How can I make my app easier to promote in the Microsoft Store?</strong></summary>

To increase your app’s visibility and chances of being featured in the Microsoft Store, follow these guidelines:

- **Include great screenshots and images:**  
  High-quality visuals greatly enhance your app’s appeal. Provide attractive and representative screenshots, particularly focusing on the first image. Include sets of screenshots tailored for each device type your app supports. Provide all required images in the **Store logos** and **Additional art assets** sections of your Store listing. Key recommended formats are:
  - **2:3 Poster art:** 720 x 1080 or 1440 x 2160 pixels
  - **16:9 Super hero art:** 1920 x 1080 or 3840 x 2160 pixels  
  This helps the Store feature your app prominently in different layouts.

- **Build one unified version of your app:**  
  Instead of separate free and paid versions, create a single app listing with either a free trial or in-app purchases to unlock additional functionality. This unified approach appeals to all potential customers and simplifies promotion and maintenance.

- **List your app in all relevant markets and languages:**  
  Maximizing your app’s reach by submitting it to every suitable market and providing localized Store listings will broaden your audience. Ensure your app adheres to local guidelines and customs for each market.

- **Enable content filters for 16+ and 18+ apps:**  
  Apps featuring content appropriate only for users aged 16+ or 18+ must implement content filtering to be eligible for Store promotion. These filters should be enabled by default, password-protected, and accessible directly within the app (not through an external site). This ensures your app remains compliant with Store guidelines, making it promotable to a wider audience.

Following these best practices increases your app’s attractiveness not just for Microsoft Store promotions but also for external reviewers, influencers, and social media channels.

</details>

---

<details>
<summary><strong>How do I link to my app in the Microsoft Store?</strong></summary>

You can make your app easily discoverable by providing direct links to your app's listing on the Microsoft Store. Here’s how to get and use these links:

- **Getting the direct URL to your Store listing:**  
  Navigate to your app's **Product Identity** page under the **Product management** section in [Partner Center](https://partner.microsoft.com/). You'll find a URL structured as:  `https://apps.microsoft.com/store/detail/<your app's Store ID>`


Customers clicking this link are taken to the web-based Store listing, where they can download and install your app via the Microsoft Store.

- **Using the Microsoft Store badge:**  
You can create a branded Microsoft Store badge that links directly to your app. To generate your custom badge:
1. Go to the [Microsoft Store badge creator](https://apps.microsoft.com/badge) page.
2. Provide your app’s 12-character Store ID (found in your Partner Center under the Product Identity section).

The badge is a [web component](https://developer.mozilla.org/en-US/docs/Web/API/Web_components) that automatically detects the user’s language and theme. It won’t inherit CSS styles from your page, but you can customize its size using a [CSS part selector](https://developer.mozilla.org/en-US/docs/Web/CSS/::part):

```css
/* Adjust the badge size to match other buttons on your page. */
ms-store-badge::part(img) {
    max-width: 200px;
}
```

This badge clearly indicates your app is available on the Microsoft Store and can help increase customer trust and downloads.

- **Direct Store App link using URI scheme:**  
If you want to directly open the Microsoft Store app (without launching a browser first), use the following URI scheme:
`ms-windows-store://pdp/?ProductId=<your app's Store ID>`


This approach is particularly useful when you know your users are already on a Windows device, or when directing users from within a WinUI app.

</details>

<br>

> [!TIP]
> For detailed information about **Leverage developer tools**, please see the [Leverage developer tools](../store-submission-api.md) section.
