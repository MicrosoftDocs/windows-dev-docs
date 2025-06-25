---
description: Learn how to submit your app to the Microsoft Store, including reserving names, uploading packages, and using different installer formats.
title: Submit your app to Microsoft Store
ms.date: 06/18/2025
ms.topic: article
ms.localizationpriority: medium
---

# Submit your app to Microsoft Store

<details>
<summary><strong>How do I reserve an app name, and why is it important?</strong></summary>

Every app in the Microsoft Store must have a unique name. Reserving an app name ensures that the name you want to use for your app is locked down for you, so no one else can claim it while you finish developing your app. You can reserve a name even before your app is ready – up to three months in advance of publishing.

To reserve a name:
- Go to your Apps & games section in Partner Center and select “New product.”
- Choose the type of app (e.g., MSIX/PWA or EXE/MSI).
- Enter the app title and click **Check availability**.
- If available, click **Reserve product name** to hold the name for 3 months.

This is important because it guarantees your branding and prevents naming conflicts.
</details>

<details>
<summary><strong>What does the app submission process involve in Partner Center?</strong></summary>

After reserving a name, start a new submission. The process involves:

- **Pricing and Availability**: Choose free/paid, markets, trials.
- **Properties**: Set category and capabilities.
- **Age Ratings**: Complete content questionnaire for regional ratings.
- **Packages**: Upload your MSIX or EXE/MSI packages.
- **Store Listings**: Add descriptions, features, images, logos.
- **Submission Options**: (Optional) Add notes for certification or schedule publish date.

Fill out all required fields, then click **Submit for certification**. Partner Center will validate inputs and flag any missing items before submission. Your app status will show as "in Certification" while being reviewed.
</details>

<details>
<summary><strong>Do I need to package my app as an MSIX, or can I submit a traditional EXE/MSI installer?</strong></summary>

MSIX is recommended but not required.

**MSIX Benefits**:
- Free Microsoft code signing and CDN hosting.
- Easier updates, better integration with Windows features.
- Enables advanced capabilities like flighting and commerce.

**EXE/MSI Submission**:
- Allowed since June 2021.
- You must provide a URL or upload the installer in submission.
- Requirements:
  - Must be .exe or .msi only.
  - Offline installer – no downloads during setup.
  - Installer must not change after submission or bundle unrelated software.

If using EXE/MSI, you won’t get MSIX-specific benefits (auto updates, Store integration), but it's acceptable if MSIX packaging isn’t feasible.
</details>

<details>
<summary><strong>What metadata fields are required when submitting an app to the Store?</strong></summary>

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

</details>

<details>
<summary><strong>What are the recommended best practices for writing the app description and listing features?</strong></summary>

A good app description should:

- Be **clear, concise, and engaging**
- Highlight the **main value proposition** of the app
- Use **bullet points** to list features for easy readability
- Avoid technical jargon unless it's a developer tool
- Use **keywords naturally** to help with search discoverability

Also, localize your descriptions for all languages you support to better connect with global users.

</details>

<details>
<summary><strong>How many screenshots should developers include in their Store listing?</strong></summary>

While only **one screenshot per device family** is required, Microsoft recommends including **5–8 high-quality screenshots** for each supported device type (PC, tablet, Xbox, etc.). 

These screenshots should:

- Showcase the app’s **key features and UI**
- Highlight different user scenarios or workflows
- Be localized when possible to match the listing language

Good visuals can significantly impact the user’s first impression and increase conversions.

</details>

<details>
<summary><strong>What are the requirements and recommendations for Store logos and trailers?</strong></summary>

A **Store logo** is mandatory for submission and is used throughout the Store’s interface (search results, listing, recommendations).

Additionally:

- Upload **logos at multiple resolutions** for different displays if possible
- Include a **trailer video** to demonstrate the app’s functionality and value — this is optional but highly recommended, as it increases engagement and conversion rates
- Keep trailers short (30–90 seconds), visually compelling, and captioned if possible for accessibility

</details>

<details>
<summary><strong>How should developers choose and use search terms for their app?</strong></summary>

You may define **up to 7 search terms**, each up to **30 characters**. These are used by the Microsoft Store’s internal search engine to improve discoverability.

Tips for effective search terms:

- Focus on words users would actually search for
- Reflect the app’s **core features or use cases**
- Avoid brand names (unless they’re your own) and generic terms like “free” or “best”
- Don’t use misleading or unrelated keywords — this may violate Store policy

Search terms are not shown to users but play a crucial role in helping your app surface in search results.

</details>

<details>
<summary><strong>How can I manage who receives submission notifications in Partner Center?</strong></summary>

After publishing an app, the **owner** of your developer account is always notified about the publishing status and required actions via email and through the [Action Center](https://learn.microsoft.com/partner-center/action-center-overview) in Partner Center. 

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

</details>


## Best practices for submitting your app

### Tips for choosing a great app name

Choosing the right name for your app is important. Pick a name that will capture your customers' interest and draw them in to learn more about your app. Here are some tips for choosing a great app name.

**Keep it short**. While your app's name can have up to 256 characters, the space used to display your app's name is limited. Long names may be truncated based on where in the store your app is being displayed and the user's display size and settings.

> [!TIP]
> Windows uses variable width fonts, so the number of visible characters in your title depends on which characters you use. For example, using Segoe UI, about 30 `i` characters will fit in the same space as 10 `w` characters. If you have multiple apps, be sure to test the visibility of each app's title, even if they are the same number of characters. Also be sure to test all localizations of your app's name. Keep in mind that East-Asian characters tend to be wider than Latin characters, so fewer characters will be displayed.

**Be original**. Make sure your app name is distinctive enough that it won't be easily confused with an existing app.

**Do not use names trademarked by others**. Make sure that you have the right to use the name that you reserve. If someone else has trademarked the name, they can report an infringement and you will not be able to keep using that name. If that happens after your app has been published, it will be removed from the Store until you've changed all instances of the name in your app, its content, and its store listing before you can submit your app for certification again.

**Avoid trailing differentiators**. Information that distinguishes different versions of your app should not be put at the end of your title. This information can be truncated by the UI, and users can miss it even if it is displayed.

If this is unavoidable, use different logos and app images to make it easier to differentiate one app from another.

**Do not include emojis in your name**. You will not be able to reserve a name that includes emojis or other unsupported characters.

### Write a great app description

A great description can make your app stand out in the Microsoft Store and help encourage customers to download it. [The description you enter when submitting your app](/hub/apps/publish/publish-your-app/msix/add-and-edit-store-listing-info.md#description) is displayed in your app's Store listing. The first few lines may also be displayed in search results and algorithm lists in the Store.

Here are some tips for making your app's description the best it can be.

- **Grab attention in the first few sentences.** The beginning of your description is the most important, so make sure it grabs and holds attention. Start with the value prop: why should potential customers take the time and money to get your app? What is the benefit to choosing your app over another? In one or two sentences, using plain and clear language, explain your app's unique appeal and why someone would want it.
- **Make it easy to learn about your app.** After your initial hook, describe additional benefits, in-app purchase opportunities, and other details about your app that customers will want to know. Make sure you include any disclosures or information that you are required to provide under the law in the markets where you are distributing your app.
- **Use lists and short paragraphs.** Potential customers may just take a quick glance at your app's description. Breaking up the content by using short paragraphs and lists makes it easier to scan.

  > [!NOTE]
  >  Adding a list of [product features](/hub/apps/publish/publish-your-app/msix/create-app-store-listing.md) can also help to quickly show what your app does. This list appears directly below the app description.

- **Avoid dry language.** Write your description using engaging language. Be sure the wording clearly describes what your app does, but say it in a way that doesn't sound boring. For many apps, a casual and friendly tone works well.
- **Use a length that is just right.** A good description reads quickly, but also includes enough info to get the reader interested and explain what the app does. A complex app will need more sentences to describe it; a simple app may need only a few. In most cases the right length is somewhere over 200 words, but well under 3000.
- **Be clear about free trials and add-ons.** If you offer a free trial of your app, be sure to explain how that trial works, so that customers understand which features are limited. It's also a good idea to mention what types of add-ons are available, particularly if they have significant impacts on your app's functionality.
- **Use standard capitalization and punctuation.** Descriptions in all caps, or those that have unusual punctuation, can be hard to read.
- **Don't forget to check the spelling and grammar.** A description with lots of misspelled words or mangled sentences doesn't reflect well on the quality of your app. Be sure to review your description (or have someone else take a look) to check for errors.
- **Don't include links or info that belongs elsewhere.** URLs that you enter in the description field won't be clickable, so don't try to add links for things like your privacy policy or support website. Instead, add these in the designated areas of the **Properties** page of your submission.
- **Don't use HTML tags.** HTML or other code will not be rendered. Your description needs to be plain text only.
- **Get ideas by reviewing descriptions of similar apps in the Store.** Take a look at how other developers describe their apps. This also helps you figure out what you can emphasize that is different about your app.