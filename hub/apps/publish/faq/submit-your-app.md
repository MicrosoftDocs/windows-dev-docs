---
description: Learn how to submit your app to the Microsoft Store, including reserving names, uploading packages, and using different installer formats.
title: Submit your app to Microsoft Store
ms.date: 04/13/2026
ms.topic: article
ms.localizationpriority: medium
---

# Submit your app to Microsoft Store

## How do I reserve an app name, and why is it important?

Every app in the Microsoft Store must have a unique name. Reserving an app name ensures that the name you want to use for your app is locked down for you, so no one else can claim it while you finish developing your app. You can reserve a name even before your app is ready – up to three months in advance of publishing.

To reserve a name:
- Go to your Apps & games section in Partner Center and select “New product.”
- Choose the type of app (e.g., MSIX/PWA or EXE/MSI).
- Enter the app title and click **Check availability**.
- If available, click **Reserve product name** to hold the name for 3 months.

This is important because it guarantees your branding and prevents naming conflicts.

## What does the app submission process involve in Partner Center?

After reserving a name, start a new submission. The process involves:

- **Pricing and Availability**: Choose free/paid, markets, trials.
- **Properties**: Set category and capabilities.
- **Age Ratings**: Complete content questionnaire for regional ratings.
- **Packages**: Upload your MSIX or EXE/MSI packages.
- **Store Listings**: Add descriptions, features, images, logos.
- **Submission Options**: (Optional) Add notes for certification or schedule publish date.

Fill out all required fields, then click **Submit for certification**. Partner Center will validate inputs and flag any missing items before submission. Your app status will show as "in Certification" while being reviewed.

## Do I need to package my app as an MSIX, or can I submit a traditional EXE/MSI installer?

The Microsoft Store supports multiple packaging formats. MSIX is the recommended format, but traditional EXE/MSI installers are also accepted.

**Supported MSIX and related package formats**:
- .msix
- .msixbundle
- .msixupload
- .appx
- .appxbundle
- .appxupload

`.xap` is a legacy package type associated with previously published apps and isn't used for new submissions.

**MSIX benefits include**:
- Free Microsoft code signing and CDN hosting.
- Easier updates and better integration with Windows features.
- Support for advanced capabilities like flighting and commerce.

Using the MSIX package format ensures a more reliable, secure, and streamlined installation and update experience for users.

**EXE/MSI submission**:
- Allowed since June 2021.
- You must provide a package URL for the installer as part of the submission; the installer must be hosted on your own infrastructure or CDN.
- Requirements:
  - Must be .exe or .msi only.
  - Offline installer – no downloads during setup.
  - Installer must not change after submission or bundle unrelated software.

Both app types can be submitted in Store depending on developer's needs.

## What metadata fields are required when submitting an app to the Store?

Required metadata typically includes:

- App name
- App description
- Category
- At least one screenshot per device family
- A Store logo
- A privacy policy URL (especially if your app collects personal data)

Optional but recommended fields include:

- Feature list
- Promotional images and trailers
- Additional languages and localized metadata
- Search terms for discoverability
- Website or support contact information

Providing rich, complete metadata helps Microsoft validate your app more efficiently and improves the user’s understanding of what your app offers.

## What are the recommended best practices for writing the app description and listing features?

A well-written Store listing can help customers quickly understand your app's value and encourage downloads.

When writing your app description:
* Start with a clear and compelling value proposition in the first few sentences. Explain what makes your app unique and why customers should choose it.
* Use clear, concise, and engaging language that is easy to understand.
* Highlight the key benefits and functionality of your app.
* Use short paragraphs and bullet points to improve readability and make the content easier to scan.
* Include a feature list to quickly communicate what your app does.
* Avoid unnecessary technical jargon unless your audience expects it.
* Use keywords naturally to improve discoverability in Store search results.
* Clearly explain any free trials, subscriptions, in-app purchases, or add-ons that affect functionality.
* Use proper spelling, grammar, punctuation, and standard capitalization.
* Do not include HTML, code snippets, or URLs in the description field. Instead, provide support, privacy policy, and website links in their designated submission fields.
* Localize descriptions for all supported languages to provide a better experience for global customers.

You can also review listings for similar apps in the Microsoft Store to understand how they communicate value and differentiate their offerings.

## How many screenshots should developers include in their Store listing?

While only **one screenshot per device family** is required, Microsoft recommends including **5–8 high-quality screenshots** for each supported device type (PC, tablet, Xbox, etc.). 

These screenshots should:

- Showcase the app’s **key features and UI**
- Highlight different user scenarios or workflows
- Be localized when possible to match the listing language

Good visuals can significantly impact the user’s first impression and increase conversions.

## What are the requirements and recommendations for Store logos and trailers?

A **Store logo** is mandatory for submission and is used throughout the Store’s interface (search results, listing, recommendations).

Additionally:

- Upload **logos at multiple resolutions** for different displays if possible
- Include a **trailer video** to demonstrate the app’s functionality and value — this is optional but highly recommended, as it increases engagement and conversion rates
- Keep trailers short (30–90 seconds), visually compelling, and captioned if possible for accessibility

## How should developers choose and use search terms for their app?

You may define **up to 7 search terms**, each up to **30 characters**. These are used by the Microsoft Store’s internal search engine to improve discoverability.

Tips for effective search terms:

- Focus on words users would actually search for
- Reflect the app’s **core features or use cases**
- Avoid brand names (unless they’re your own) and generic terms like “free” or “best”
- Don’t use misleading or unrelated keywords — this may violate Store policy

Search terms are not shown to users but play a crucial role in helping your app surface in search results.

## How can I manage who receives submission notifications in Partner Center?

After publishing an app, the **owner** of your developer account is always notified about the publishing status and required actions via email and through the [Action Center](/partner-center/action-center-overview) in Partner Center. 

To ensure delivery of these critical notifications, the owner must verify their email address via [My Preferences](https://partner.microsoft.com/dashboard/actioncenter/mypreferences) in Action Center.

You can also add other team members to receive the same submission notifications by assigning them either the **Developer** or **Manager** role. This is useful for keeping co-developers or managers informed of updates or required actions.

To **add or remove** members from the notification list:

1. On the **Submission options** page, find the field labeled “Submission notification audience.”
2. Click the **“Click here”** link to open the Notification audience overview page.
3. On the overview page, add or remove users as needed.

> [!NOTE]
> - The **account owner is always notified** and cannot be removed from the audience list.
> - The audience list is **product-specific** and applies to all submissions for that product. If you have multiple apps, you’ll need to configure the list separately for each one.
> - **Add-ons inherit** the parent product’s notification audience list and **cannot be managed separately.**

## How do I choose a great app name for the Microsoft Store?

Choosing a compelling name is crucial to attracting customers and making your app easily discoverable. Here are some best practices:

- **Keep it short**. Although your app's name can have up to 256 characters, display space is limited, and longer names may be truncated depending on the user's screen and settings. Shorter names are more memorable and easier to display clearly.
  
  > [!TIP]
  > Windows uses variable-width fonts. This means the number of visible characters depends on their width (e.g., 30 'i' characters fit in the same space as 10 'w' characters). Test your app name across different devices and languages to ensure it's always clearly visible.

- **Be original**. Choose a distinctive name that clearly differentiates your app from others. An original name reduces confusion and enhances your brand identity.

- **Do not use trademarked names**. Ensure you have the rights to use the app name. Using a trademarked name could lead to your app being removed from the Store, forcing you to rename and re-submit your app.

- **Avoid trailing differentiators**. Don't include differentiating information (such as version numbers or dates) at the end of your app's title. Such details may be cut off in some views, causing confusion. If necessary, differentiate your apps using distinct logos or images.

- **Avoid emojis and special characters**. The Microsoft Store does not allow emojis or other unsupported special characters in app names.

> [!TIP]
> For detailed information about **How to submit your app**, please see the [Submit your app](../publish-your-app/msix/reserve-your-apps-name.md) section.
