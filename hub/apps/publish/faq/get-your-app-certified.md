---
title: Get your app certified - FAQ
description: This file contains detailed frequently asked questions for the "Get your app certified" section of the Microsoft Store documentation, specifically aimed at helping new developers understand the certification process in Partner Center. It is part of the broader FAQ content initiative to support app developers onboarding through Microsoft Learn.
ms.date: 06/18/2025
ms.topic: faq
---

# Get your app certified

This FAQ is designed to help new developers understand what happens after submitting an app to the Microsoft Store, including how certification works, how long it takes, common reasons for failure, and how to prepare your app to pass certification successfully. This content was created as part of a structured effort to improve the "Submit and manage your apps" documentation on Microsoft Learn.

---

<details>
<summary><strong>What is the app certification process, and how long does it take?</strong></summary>

After you submit your app to the Store, it enters the certification stage. Microsoft performs a series of checks on your app before it can be published. This typically takes up to three business days, though it can be quicker.

The process includes:
- **Security testing:** Your app is scanned for malware and checked for security vulnerabilities.
- **Technical compliance testing:** Ensures your app doesn’t crash or use prohibited APIs. The Store installs and runs your app to verify it behaves as expected.
- **Content compliance check:** Microsoft reviews your app’s content and Store listing to ensure they comply with Store policies, including age rating and appropriate descriptions.

Once approved, your app typically becomes visible in the Store within about 15 minutes. You'll receive a notification, and your app’s status in the dashboard will show as “In Microsoft Store.”

</details>

---

<details>
<summary><strong>What should I do if my app fails certification or is removed from the Store?</strong></summary>

If your app fails certification, Microsoft will provide a **certification report** explaining why it failed—whether due to a technical issue or policy violation.

To address the failure:

1. Review the Microsoft Store Policies to understand the issue.
2. Fix the problem (e.g., remove prohibited content, correct metadata).
3. Submit an updated version for certification.

If your app was removed from the Store after being published, it may be due to a severe issue (e.g., security vulnerabilities or policy violations). In such cases:

- Check Partner Center and your email for communication from Microsoft.
- Address the stated issue.
- Submit a corrected update for review.

For clarification or help, contact Microsoft at `reportapp@microsoft.com` with your app ID or respond to the certification report email directly.

</details>

---

<details>
<summary><strong>How can I improve my app’s chances of passing certification on the first try?</strong></summary>

Here are Microsoft’s best practices to help your app pass certification smoothly:

- **Submit only when ready:** Ensure your app is complete and free of placeholders or broken features.
- **Use Windows App Certification Kit (WACK):** Run this tool locally to pre-test for issues Microsoft will check.
- **Test across environments:** Validate your app on different devices, OS versions, and conditions (including offline).
- **Handle offline scenarios:** Don’t let your app crash without internet—display proper error messages.
- **Provide test info:** Share credentials or instructions in the “Notes for certification” if your app has locked features or requires sign-in.
- **Include a privacy policy:** If your app accesses personal data or services, include a privacy policy URL and display it in the app.
- **Write clear Store listings:** Make sure descriptions truthfully represent the app to avoid rejections for being misleading.
- **Answer age rating questionnaire carefully:** Be truthful to avoid generating incorrect ratings.
- **Accessibility claims:** Only mark your app as accessible if it genuinely meets accessibility standards.

Finally, **always check the latest Microsoft Store Policies** to ensure compliance with technical and content guidelines.

</details>

<br>

> [!TIP]
> For detailed information about **How to get your app certified**, please see the [Get your app certified](../publish-your-app/msix/app-certification-process.md) section.
