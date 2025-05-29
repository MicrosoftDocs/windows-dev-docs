---
description: Best practices for publishing your app
title: Best practices for publishing your app
ms.topic: article
ms.date: 05/27/2025
---

# Best practices

## Tips for choosing a great app name

Choosing the right name for your app is important. Pick a name that will capture your customers' interest and draw them in to learn more about your app. Here are some tips for choosing a great app name.

**Keep it short**. While your app's name can have up to 256 characters, the space used to display your app's name is limited. Long names may be truncated based on where in the store your app is being displayed and the user's display size and settings.

> [!TIP]
> Windows uses variable width fonts, so the number of visible characters in your title depends on which characters you use. For example, using Segoe UI, about 30 `i` characters will fit in the same space as 10 `w` characters. If you have multiple apps, be sure to test the visibility of each app's title, even if they are the same number of characters. Also be sure to test all localizations of your app's name. Keep in mind that East-Asian characters tend to be wider than Latin characters, so fewer characters will be displayed.

**Be original**. Make sure your app name is distinctive enough that it won't be easily confused with an existing app.

**Do not use names trademarked by others**. Make sure that you have the right to use the name that you reserve. If someone else has trademarked the name, they can report an infringement and you will not be able to keep using that name. If that happens after your app has been published, it will be removed from the Store until you've changed all instances of the name in your app, its content, and its store listing before you can submit your app for certification again.

**Avoid trailing differentiators**. Information that distinguishes different versions of your app should not be put at the end of your title. This information can be truncated by the UI, and users can miss it even if it is displayed.

If this is unavoidable, use different logos and app images to make it easier to differentiate one app from another.

**Do not include emojis in your name**. You will not be able to reserve a name that includes emojis or other unsupported characters.

### Choosing which device families to support

If you upload packages targeting one individual device family, we'll check the box to make those packages available to new customers on that type of device. For example, if a package targets Windows.Desktop, the **Windows 10/11 Desktop** box will be checked for that package (and you won't be able to check the boxes for other device families).

Packages targeting the Windows.Universal device family can run on any Windows 10 or Windows 11 device (including Xbox One). By default, we'll make those packages available to new customers on all device types _except_ for Xbox.

You can uncheck the box for any Windows 10 or Windows 11 device family if you don’t want to offer your submission to customers on that type of device. If a device family’s box is unchecked, new customers on that type of device won’t be able to acquire the app (though customers who already have the app can still use it, and will get any updates you submit).

If your app supports them, we recommend keeping all of the boxes checked, unless you have a specific reason to limit the types of Windows 10 or Windows 11 devices which can acquire your app. For instance, if you know that your app doesn't offer a good experience on [Surface Hub](https://developer.microsoft.com/windows/surfacehub) and/or [Microsoft HoloLens](https://developer.microsoft.com/mixed-reality), you can uncheck the **Windows 10 Team** and/or **Windows 10 Holographic** box. This prevents any new customers from acquiring the app on those devices. If you later decide you're ready to offer it to those customers, you can create a new submission with the boxes checked.

# Write a great app description for MSIX app

A great description can make your app stand out in the Microsoft Store and help encourage customers to download it. [The description you enter when submitting your app](add-and-edit-store-listing-info.md#description) is displayed in your app's Store listing. The first few lines may also be displayed in search results and algorithm lists in the Store.

Here are some tips for making your app's description the best it can be.

- **Grab attention in the first few sentences.** The beginning of your description is the most important, so make sure it grabs and holds attention. Start with the value prop: why should potential customers take the time and money to get your app? What is the benefit to choosing your app over another? In one or two sentences, using plain and clear language, explain your app's unique appeal and why someone would want it.
- **Make it easy to learn about your app.** After your initial hook, describe additional benefits, in-app purchase opportunities, and other details about your app that customers will want to know. Make sure you include any disclosures or information that you are required to provide under the law in the markets where you are distributing your app.
- **Use lists and short paragraphs.** Potential customers may just take a quick glance at your app's description. Breaking up the content by using short paragraphs and lists makes it easier to scan.

  > [!NOTE]
  >  Adding a list of [product features](create-app-store-listing.md) can also help to quickly show what your app does. This list appears directly below the app description.

- **Avoid dry language.** Write your description using engaging language. Be sure the wording clearly describes what your app does, but say it in a way that doesn't sound boring. For many apps, a casual and friendly tone works well.
- **Use a length that is just right.** A good description reads quickly, but also includes enough info to get the reader interested and explain what the app does. A complex app will need more sentences to describe it; a simple app may need only a few. In most cases the right length is somewhere over 200 words, but well under 3000.
- **Be clear about free trials and add-ons.** If you offer a free trial of your app, be sure to explain how that trial works, so that customers understand which features are limited. It's also a good idea to mention what types of add-ons are available, particularly if they have significant impacts on your app's functionality.
- **Use standard capitalization and punctuation.** Descriptions in all caps, or those that have unusual punctuation, can be hard to read.
- **Don't forget to check the spelling and grammar.** A description with lots of misspelled words or mangled sentences doesn't reflect well on the quality of your app. Be sure to review your description (or have someone else take a look) to check for errors.
- **Don't include links or info that belongs elsewhere.** URLs that you enter in the description field won't be clickable, so don't try to add links for things like your privacy policy or support website. Instead, add these in the designated areas of the **Properties** page of your submission.
- **Don't use HTML tags.** HTML or other code will not be rendered. Your description needs to be plain text only.
- **Get ideas by reviewing descriptions of similar apps in the Store.** Take a look at how other developers describe their apps. This also helps you figure out what you can emphasize that is different about your app.
