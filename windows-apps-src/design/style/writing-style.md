---
author: QuinnRadich
title: Writing style
description: Using the right voice and tone is key to making your app's text seem a natural part of its design.
keywords: UWP, Windows 10, text, writing, voice, tone, design, UI, UX
ms.author: quradic
ms.date: 1/22/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
ms.localizationpriority: medium
---

# Writing style

The way you phrase an error message, the way you write help documentation, and even the text you choose for a button have a big impact on the usability of your app. 

Writing style can make a big difference between an awful user experience...

![Windows 7 Blue Screen of Death](images/writing/bluescreen_old.png)

...And a better one.

![Windows 10 Blue Screen of Death](images/writing/bluescreen_new.png)


## Writing with a natural voice and tone

Research shows that users respond best to a writing style that's friendly, helpful, and concise. 

## Be friendly

Above all else, you don't want to scare off the user. Be informal, be casual, and don't use terms they won't understand. Even when things break, don't blame the user for any problems. Your app should take responsibility instead, and offer welcoming guidance that puts the user's actions first.

![Before: An error occured, and your image could not be uploaded. If subsequent attempts also fail, the app may need to be re-initialized. After: We couldn't upload your image because there was an error. Please try again - if it still doesn't work, try restarting the app.](images/writing/friendly_example.png)

## Be helpful

Always be focused on explaining what's going on and providing the relevant information that the user needs, without overloading them with unnecessary info.

![Before: Unable to connect to network. After: Your device is connected to a network, but the network isn't connected to the internet. Try restarting your router or moem. If you're using a public hotspot, make sure you're logged in.](images/writing/helpful_example.png)

## Be clear and concise

Most of the time, text isn't the focus of an app. It's there to guide users, to teach them what's going on and what they should do next. Don't lose sight of this when writing the text in your app. Use language that will be familiar to your audience. Usually this means choosing everyday and conversational words, but apps for specialized uses will have their own standards. Your users should never have to wonder what your text means, so favor simplicity if you're ever unsure of which tone to use.

![Before: Something went wrong and we weren't able to save your file. The location might not be accessible to this device, or there might not be enough space. After: We couldn't save your file to that location. Please try again and with a different location.](images/writing/concise_example.png)

## Writing tips

There are many ways you can implement those general principles in the limited space your app has. Below are some strategies that we find useful to put those princples into action:

### Lead with what's important

Users need to be able to read and understand your text at a glance. Don't pad your words with unnecessary introductions. Give your key points the most visibility, and always present the core of an idea before you add onto it.

**Good:** Select "filters" to add effects to your image.

**Bad:** If you want to add visual effects or alterations to your image, select "filters."

### Emphasize action

Apps are defined by actions. Users take action as they use the app, and the app takes action as it responds to the users. Make sure your text uses the *active voice* throughout your app. Users and functions should be described as doing things, instead of having things done to them.

**Good:** Restart the app to see your changes.

**Bad:** The changes will be applied when the app is restarted.

### Short and sweet

Users scan text, and will often skip over larger blocks of words entirely. Don't sacrifice necessary information and presentation, but don't use more words than you have to. Sometimes, this will mean relying on many shorter sentences or fragments. Other times, this will mean being extra choosy about the words and structure of longer sentences.

**Good:** Sorry, we couldn't upload the picture. If this happens again, try restarting the app. But don't worry — your picture will be waiting when you come back.

**Bad:** An error occured, and we weren't able to upload the picture. Please try again, and if you encounter this problem again, you may need to restart the app. But don't worry - we've saved your work locally, and it'll be waiting for you when you come back.

## Style conventions

If you're don't consider yourself to be a writer, it can be intimidating to try to implement these principles and recommendations. But don't worry — using simple and straightforward language is a great way to provide a good user experience. And if you're still unsure how to structure your words, here's some helpful guidelines.

### Addressing the user

Speak directly to the user.

* Always address the user as "you."

* Use "we" to refer to your own perspective. It's welcoming and helps the user feel like part of the experience.

* Don't use "I" or "me" to refer to the app's perspective, even if you're the only one creating it.

> We couldn't save your file to that location.

### Abbreviations

Abbreviations can be useful when you need to refer to products, places, or technical concepts multiple times throughout your app. They can save space and feel more natural, so long as the user understands them.

* Don't assume that users are already familiar with any abbreviations, even if you think they're common.

* Always define what a new abbreviation means the first time the user will see it.

* Don't use abbreviations that are too similar to one another.

> The Universal Windows Platform (UWP) design guidance is a resource to help you design and build beautiful, polished apps. With the design features that are included in every UWP app, you can build user interfaces (UI) that scale across a range of devices.

### Contractions

People are used to contractions, and expect to see them. Avoiding them can make your app seem too formal or even stilted.

* Use contractions when they're a natural fit for the text.

* Don't use unnatural contractions just to save space, or when they would make your words sound less conversational.

> When you're happy with your image, press "save" to add it to your gallery. From there, you'll be able to share it with friends.

### Periods

Ending text with a period implies that that text is a full sentence. Use a period for larger blocks of text, and avoid them for text that's shorter than a complete sentence.

* Use periods to end full sentences in tooltips, error messages, and dialogs.

* Don't end text for buttons, radio buttons, labels, or checkboxes with a period.

> **You're not connected**
> * Check that your network cables are plugged in.
> * Make sure you're not in airplane mode.
> * See if your wireless switch is turned on.
> * Restart your router.

### Capitalization

While capital letters are important, they're easy to overuse.

* Capitalize proper nouns.

* Capitalize the start of every string of text in your app: the start of every sentence, label, and title.

> **Which part is giving you trouble?**
> * I forgot my password
> * It won't accept my password
> * Someone else might be using my account

## Error messages

When something goes wrong in your app, users pay attention. Because users might be confused or frustrated when they encounter an error message, they're an area where good voice and tone can have a particularly significant impact.

More than anything else, it's important that your error message doesn't blame the user. But it's also important not to overwhelm them with information that they don't understand. Most of the time a user who encounters an error just wants to get back to what they were doing as quickly and as easily as they can. Therefore, any error message you write should:

* **Be friendly** by taking responsibility for what happened, and by avoiding unfamiliar terms and technical jargon.

* **Be helpful** by telling the user what went wrong to the best of your ability, by telling them what will happen next, and by providing a realistic solution they can accomplish.

* **Be clear and concise** by eliminating extraneous information.

![Example of a good error message](images/writing/connection_error.png)

## Buttons

Text on buttons needs to be concise enough that users can read it all at a glance and clear enough that the button's function is immediately obvious. The longest the text on a button should ever be is a couple short words, and many should be shorter than that.

When writing the text for buttons, remember that every button represents an action. Be sure to use the *active voice* in button text, to use words that represent actions rather than reactions.

![Example of a good button](images/writing/install_button.png)

## Dialogs

Many of the same advice for writing error messages also applies when creating the text for any dialogs in your app. While dialogs are expected by the user, they still interrupt the normal flow of the app, and need to be helpful and concise so the user can get back to what they were doing.

But most important is the "call and response" between the title of a dialog and its buttons. Make sure that your buttons are clear answers to the question posed by the title, and that their format is consistent across your app.

![Example of a good dialog](images/writing/password_dialog.png)

## Spoken experiences

The same general principles and recommendations apply when writing text for spoken experiences, such as Cortana. In those features, the principles of good writing are even more important, because you are unable to provide users with other visual design elements to supplement the spoken words.

* **Be friendly** by engaging the user with a conversational tone. More than in any other area, it's vital that a spoken experience sound warm and approachable, and be something that users aren't afraid to talk to.

* **Be helpful** by providing alternative suggestions when the user asks the impossible. Much like in an error message, if something went wrong and your app isn't able to fulfill the request, it should give the user a realistic alternative that they can try asking, instead.

* **Be clear and concise** by keeping your language simple. Spoken experiences aren't suitable for long sentences or complicated words.

## Accessibility and localization

Your app can reach a wider audience if it's written with accessibility and localization in mind. This is something that can't only be accomplished through text, though straightforward and friendly language is a great start. For more information, see our [accessibility overview](https://docs.microsoft.com/windows/uwp/design/accessibility/accessibility-overview) and [localization guidelines](https://docs.microsoft.com/windows/uwp/design/globalizing/globalizing-portal).

* **Be friendly and helpful** by taking different experiences into account. Avoid phrases that might not make sense to an international audience, and don't use words that make assumptions about what the user can and can't do.

* **Be clear and concise** by avoiding unusual and specialized words when they aren't necessary. The more straightforward your text is, the easier it is to localize.


## Techniques for non-writers

You don't need to be a trained or experienced writer to provide your users with a good experience. Pick words that sound comfortable to you — they'll feel comfortable to the user, too. But sometimes, that's not as easy as it sounds. If you get stuck, these techniques can help you out. 

* Imagine that you're talking to a friend about your app. How would you explain the app to them? How would you talk about its features or give them instructions? Better yet, explain the app to an actual person who hasn't used it yet. 

* Imagine how you would describe a completely different app. For instance, if you're making a game, think of what you'd say or write to describe a financial or a news app. The contrast in the language and stucture you use can give you more insight into the right words for what you're actually writing about.

* Take a look at similar apps for inspiration. 

Finding the right words is a problem that many people struggle with, so don't feel bad if it's not easy to settle on something that feels natural.
