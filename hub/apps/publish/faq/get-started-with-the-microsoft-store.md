---
description: FAQ for new developers starting with Microsoft Store.
title: Get started with Microsoft Store - Frequently Asked Questions
ms.date: 06/18/2025
ms.topic: article
ms.localizationpriority: medium
---

# Get started with Microsoft Store - Frequently Asked Questions
<details>
<summary><strong>What is the Microsoft Partner Center and why do I need a developer account to publish my app?</strong></summary>

Microsoft Partner Center is the online portal where you manage the submission, certification, and maintenance of your apps in the Microsoft Store. You must register for a developer account in Partner Center to publish apps to the Store – it’s a prerequisite before starting the app submission process.

Having a developer account gives you access to the Windows Apps & Games dashboard in Partner Center, where you will reserve your app name, upload packages, and track your app’s performance. Partner Center offers two types of developer accounts (Individual or Company) to accommodate different needs.

</details>

---

<details>
<summary><strong>What are the main benefits of distributing my app through the Microsoft Store?</strong></summary>

The Microsoft Store provides access to a vast user base of over a billion Windows customers across more than 240 markets and 110 languages. Beyond reach, the Store also offers various promotion and marketing tools such as:

- **Curated collections** and **editorial features** that highlight apps in Store spotlight areas  
- Built-in **search discoverability** for relevant categories
- Deep integration with Windows (Windows search, share dialog, launch from Store etc.)

Additionally, when you publish through the Microsoft Store, your app benefits from a rich ecosystem of services and infrastructure managed by Microsoft, including:

- **App distribution** — Hosted and paid by Microsoft, making deployment and availability seamless.
- **In-app purchase and promotion services** — Integrated Microsoft Store services like promo codes and in-app purchases.
- **Billing and download support** — Microsoft provides direct customer support for transactional and technical issues.
- **Marketing resources** — Access to product page promotion, campaigns, and Store placement tools.
- **Software updates** — Updates are delivered automatically through Windows, ensuring users always have the latest version.
- **Restore support** — Apps are automatically restored when users switch devices.
- **Global payment processing** — Microsoft manages worldwide transactions and payouts.
- **App analytics** — Insightful data provided through Partner Center for usage, health, reviews, and more.
- **Code signing** — Apps are signed by Microsoft to improve trust and security.
- **Age ratings** — Simplified and compliant rating management via the International Age Ratings Coalition (IARC).
- **User feedback and review responses** — Developers can respond directly to customer reviews in the Store.
- **Beta testing** — Hosted pre-release testing tools for gathering early feedback before launch.

All these benefits reduce friction in development, simplify distribution logistics, improve user confidence, and help your app succeed in a competitive marketplace.
</details>

---

<details>
<summary><strong>How does publishing to the Store improve my app’s trust and security?</strong></summary>

All apps in the Microsoft Store undergo a thorough certification process, including:

- **Security scans** to check for malware or vulnerabilities
- **Technical validation** for API usage and stability
- **Content policy enforcement** to ensure user-safe experiences

Apps are digitally signed, encrypted, and run in a sandboxed environment to protect users. In addition, Microsoft Store handles **automatic updates**, so users always receive the latest secure version of your app without needing to manually reinstall.

</details>

---

<details>
<summary><strong>What revenue and commerce options are available if I publish to the Store?</strong></summary>

Developers publishing non-gaming apps can **use their own commerce platform and keep 100% of the revenue**. Developers can also use **Microsoft’s commerce platform and pay a competitive fee of 12% for games and 15% for apps**. Supported monetization models include:

- **In-app purchases**
- **Subscriptions**
- **Advertising**

This flexibility makes the Microsoft Store suitable for a wide range of app business models.

</details>

---

<details>
<summary><strong>What analytics and insights can I access after publishing my app?</strong></summary>

Once your app is live, Partner Center provides robust analytics covering:

- **Acquisitions and installs**
- **User engagement and usage**
- **App health and crash data**
- **Ratings and reviews**
- **Add-on acquisitions**

You can filter by market, date, device type, and more. Data can be exported as CSV or TSV files for offline use, or accessed via APIs for integration into custom dashboards.

</details>

---

<details>
<summary><strong>How does the Store support enterprise distribution and management?</strong></summary>

Developers can distribute apps privately to specific organizations using:

- **Microsoft Intune** (for managed deployments)
- **Line-of-business (LOB)** licensing (for exclusive use inside an enterprise)

Apps can be distributed via **online or offline licenses**, providing flexibility for enterprise IT management.

</details>

---

<details>
<summary><strong>What are the different types of apps that can be distributed on Store?</strong></summary>

Apps be packaged as **MSIX** or **MSI** formats can be distributed through the Microsoft Store. Both types provide secure, reliable, and efficient installation experiences, simplified updates, and clean uninstalls. They supports both modern and classic Windows apps.

We recommend packaging your app (which is built with any app framework - UWP, Win32, PWA, WinApp SDK etc.), as **MSIX**. By packaging your app as MSIX, you can take advantages of many features like a complimentary binary hosting (provided by Microsoft), complementary code signing (provided by Microsoft), Microsoft Store commerce platform, package flighting, advanced integration with Windows (to use features like share dialog, launch from Store etc), Windows 11 backup and restore etc.

</details>

---

<details>
<summary><strong>What are the Microsoft Store Policies and why do they matter for my app?</strong></summary>

The **Microsoft Store Policies** are a set of rules every app must follow to be published in the Store. These include requirements for:

- Technical compliance (e.g., no use of banned APIs)
- Security (e.g., malware scanning)
- Content (e.g., no prohibited or misleading material)
- Legal (e.g., valid age ratings, correct use of in-app purchases)

Violations can result in failed submissions or removal from the Store. Microsoft publishes a complete version of the Store Policies (currently version 7.18) that all developers should review. Following these policies helps ensure your app is certified quickly and provides a safe, high-quality experience to users.

</details>

---

<details>
<summary><strong>How do I register for a Microsoft Store developer account? What are the main steps?</strong></summary>
To open a developer account, you will sign up through Partner Center and provide some information. The process is straightforward:

- **Sign in with a Microsoft account:** Go to the Partner Center registration page and sign in with your Microsoft account (or create one if needed). This Microsoft account will be used to log in to your developer dashboard.
- **Join the developer program:** During registration, select the Windows and Xbox program – this enrolls you as a Windows app developer.
- **Choose account type and country:** Specify your account type (Individual or Company) and your country/region. Note that the country/region cannot be changed later.
- **Provide publisher details:** Enter a Publisher Display Name – this is the name shown to customers in the Store. You’ll also provide contact info (address, email, etc.) for verification.
- **Accept terms and pay the fee:** Accept the Microsoft App Developer Agreement and pay the one-time registration fee (approximately $99 USD for companies, depending on your country. For individual companies, please see [Free developer registration for individual developers](../whats-new-individual-developer.md) section).
- **Verify your email and account:** You’ll receive a verification email after payment. Confirm your email to finalize the account creation. Once complete, you can begin the app submission process.
</details>

---

<details>
<summary><strong>How can I get help from Microsoft if I run into problems with Partner Center or my app?</strong></summary>

You can contact Microsoft Support through **Partner Center**:

1. Click the **Help (?) icon** at the top of Partner Center.
2. Select **Contact Support** from the Help panel.
3. Fill in the support ticket with relevant details (e.g., app name, issue type).

For general questions, you can also use Microsoft Q&A forums or check the Learn documentation. However, for urgent or account-related issues, always submit a support ticket through Partner Center. You can also reach out to reportapp@microsoft.com for Certification related queries.

</details>

---

<details>
<summary><strong>Does the Microsoft Store provide code signing for my app? Do I need my own code signing certificate?</strong></summary>

Yes, the Microsoft Store provides **automatic code signing** for **MSIX and AppX packages** submitted for Store distribution. You do not need to purchase or provide your own CA-trusted code signing certificate, .pfx file, .cer file, or use a USB token/hardware security module (HSM) to submit MSIX packages to the Microsoft Store.

Here's how it works:

- **For MSIX/AppX Store submissions:** When you submit your MSIX/AppX package to the Microsoft Store, the package does not need to be signed with a CA-trusted certificate. After your app passes certification, the Microsoft Store automatically re-signs your package with a Microsoft certificate during the publishing process, replacing any existing signature. This ensures customers can trust and install your app without security warnings.
  
- **What you need:** Only the MSIX/AppX package files (.msix, .msixupload, .msixbundle, .appx, .appxupload, or .appxbundle) are required for submission. No CA-trusted code signing certificate is needed.

- **For MSI or EXE installers:** The Store does **not** re-sign MSI or EXE installers. If you submit an MSI or EXE installer, you must Authenticode-sign it yourself with a valid code signing certificate before submission.

- **For non-Store distribution:** If you plan to distribute your MSIX package outside the Microsoft Store (for example, for enterprise deployment, sideloading, or direct downloads), you will need to sign the package yourself with a valid code signing certificate before distribution. For more information, see [Sign an app package using SignTool](/windows/win32/appxpkg/how-to-sign-a-package-using-signtool).

This automatic re-signing is one of the key benefits of publishing MSIX packages through the Microsoft Store, as it eliminates the need to purchase and manage CA-trusted code signing infrastructure for Store distribution.

</details>

<br>

> [!TIP]
> For detailed information about **How to get started with the microsoft store**, please see the [Get started with the microsoft store](../index.md) section.
