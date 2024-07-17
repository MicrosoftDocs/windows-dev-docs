---
title: Microsoft Store Policy version 7.6
description: Archived version of Microsoft Store Policy version 7.6
ms.date: 11/04/2021
ms.topic: article
keywords: store policies agreement archive
ms.localizationpriority: high
---

# Microsoft Store Policies (7.6)

**Document version: 7.6**

**Effective dates: Nov 20, 2017 - Feb 13, 2018**

> [!WARNING]
> There is a newer version of this document. See [Microsoft Store Policies](../store-policies.md) for details.

Thank you for your interest in developing products for the Microsoft Store<sup>1</sup>. We’re committed to a diverse catalog of apps for customers worldwide. Apps on the Store must meet our certification standards, offer customers a truly useful and engaging experience, and provide a good fit for the Store.

A few principles to get you started:

- Offer unique and distinct value within your app. Provide a compelling reason to download your app from the Store.
- Don’t mislead our joint customers about what your app can do, who is offering it, etc.
- Don’t attempt to cheat customers, the system or the ecosystem. There is no place in our Store for any kind of fraud, be it ratings and review manipulation, credit card fraud or other fraudulent activity.

Adhering to these policies should help you make choices that enhance your app’s appeal and audience.

Your apps are crucial to the experience of hundreds of millions of customers. We can’t wait to see what you create and are thrilled to help deliver your apps to the world.

If you have feedback on the policies, please let us know by commenting in [our forum](https://go.microsoft.com/fwlink/p/?LinkId=224196). We will consider every comment.

## Table of Contents

**App Policies:**

- [10.1 Distinct Function & Value; Accurate Representation](#101-distinct-function--value-accurate-representation)
- [10.2 Security](#102-security)
- [10.3 App is Testable](#103-app-is-testable)
- [10.4 Usability](#104-usability)
- [10.5 Personal Information](#105-personal-information)
- [10.6 Capabilities](#106-capabilities)
- [10.7 Localization](#107-localization)
- [10.8 Financial Transactions](#108-financial-transactions)
- [10.9 Notifications](#109-notifications)
- [10.10 Advertising Conduct and Content](#1010-advertising-conduct-and-content)
- [10.11 Mobile Voice Plans](#1011-mobile-voice-plans)
- [10.12 Edge Extensions](#1012-edge-extensions)
- [10.13 Gaming and Xbox](#1013-gaming-and-xbox)

**Content Policies:**

- [11.1 General Content Requirements](#111-general-content-requirements)
- [11.2 Content Including Names, Logos, Original and Third Party](#112-content-including-names-logos-original-and-third-party)
- [11.3 Harm to Others](#113-harm-to-others)
- [11.4 Defamatory, Libelous, Slanderous and Threatening](#114-defamatory-libelous-slanderous-and-threatening)
- [11.5 Offensive Content](#115-offensive-content)
- [11.6 Alcohol, Tobacco, Weapons and Drugs](#116-alcohol-tobacco-weapons-and-drugs)
- [11.7 Adult Content](#117-adult-content)
- [11.8 Illegal Activity](#118-illegal-activity)
- [11.9 Excessive Profanity and Inappropriate Content](#119-excessive-profanity-and-inappropriate-content)
- [11.10 Country/Region Specific Requirements](#1110-countryregion-specific-requirements)
- [11.11 Age Ratings](#1111-age-ratings)

## App Policies

### 10.1 Distinct Function & Value; Accurate Representation

Your app and its associated metadata must accurately and clearly reflect the source, functionality, and features of your app.

**10.1.1**

All aspects of your app should accurately describe the functions, features and any important limitations of your app, including required or supported input devices. Your app may not use a name or icon similar to that of other apps, and may not claim to be from a company, government body, or other entity if you do not have permission to make that representation.

**10.1.2**

Your app must be fully functional and must provide appropriate functionality for each targeted device family.

**10.1.3**

Keywords may not exceed seven unique terms and should be relevant to your app.

**10.1.4**

Your app must have distinct and informative metadata and must provide a valuable and quality user experience.

### 10.2 Security

Your app must not jeopardize or compromise user security, or the security or functionality of the device, system or related systems.

**10.2.1**

Apps that browse the web must use the appropriate HTML and JavaScript engines provided by the Windows Platform.

**10.2.2**

Your app must not attempt to change or extend the described functionality through any form of dynamic inclusion of code that is in violation of Store Policies. Your app should not, for example, download a remote script and subsequently execute that script in a manner that is not consistent with the described functionality.

**10.2.3**

Your app must not contain or enable malware as defined by the Microsoft criteria for[Unwanted and Malicious Software](https://go.microsoft.com/fwlink/?LinkId=821298).

**10.2.4**

Your app may contain fully integrated middleware (such as third-party cross-platform engines and third-party analytics services), but must not deliver or install non-integrated third-party owned or branded apps or modules unless they are fully contained in your app package.

Your app may depend on non-integrated software (such as another app or module) to deliver its primary functionality, subject to the following requirements:

- You disclose the dependency at the beginning of the description metadata
- The dependent software is available in the Store

**10.2.5**

All of your apps and in-app products that are available for purchase from the Store must be installed and updated only through the Store.

### 10.3 App is Testable

The app must be testable. If it is not possible to test your app for any reason, including, but not limited to, the items below, your app may fail this requirement.

**10.3.1**

If your app requires login credentials, provide us with a working demo account using the Notes to Tester field.

**10.3.2**

If your app requires access to a server, the server must be functional to verify that it's working correctly.

**10.3.3**

If your app allows a user to add a gift card balance, give us a gift card number that can be used in the testing.

### 10.4 Usability

Your app must meet Store standards for usability, including, but not limited to, those listed in the subsections below.

**10.4.1**

The app must run on devices that are compatible with the software, hardware and screen resolution requirements specified by the application. If an app is downloaded on a device with which it is not compatible, it should detect that at launch and display a message to the customer detailing the requirements.

**10.4.2**

Apps must continue to run and remain responsive to user input. Apps must shut down gracefully and not close unexpectedly. The app must handle exceptions raised by any of the managed or native system APIs and remain responsive to user input after the exception is handled.

**10.4.3**

The app must start up promptly and must stay responsive to user input.

**10.4.4**

Where applicable, pressing the back button should take the user to a previous page/dialog. If the user presses the back button on the first page of the app, then the app terminates (unless it is allowed to run in the background).

### 10.5 Personal Information

The following requirements apply to apps that access personal information. Personal information includes all information or data that identifies or could be used to identify a person, or that is associated with such information or data. Examples of personal information include: name and address, phone number, biometric identifiers, location, contacts, photos, audio & video recordings, documents, SMS, email, or other text communication, screenshots, and in some cases, combined browsing history.

**10.5.1**

If your app accesses, collects or transmits personal information, or if otherwise required by law, you must maintain a privacy policy. You must provide users with access to your privacy policy by entering the privacy policy URL in Dev Center when you submit your app. In addition, you may also include or link to your privacy policy in the app. The privacy policy can be hosted within or directly linked from the app. Your privacy policy must inform users of the personal information accessed, collected or transmitted by your app, how that information is used, stored and secured, and indicate the types of parties to whom it is disclosed. It must describe the controls that users have over the use and sharing of their information and how they may access their information, and it must comply with applicable laws and regulations. Your privacy policy must be kept up-to-date as you add new features and functionality to your app.

Additionally, apps that receive device location must provide settings that allow the user to enable and disable the app's access to and use of location from the Location Service API. For Windows Phone 8 and Windows Phone 8.1 apps, these settings must be provided in-app. For Windows Mobile 10 apps, these settings are provided automatically by Windows within the Settings App (on the **Settings** > **Privacy** > **Location** page).

**10.5.2**

You may publish the personal information of customers of your app to an outside service or third party through your app or its metadata only after obtaining opt-in consent from those customers. Opt-in consent means the customer gives their express permission in the app user interface for the requested activity, after you have:

- described to the customer how the information will be accessed, used or shared, indicating the types of parties to whom it is disclosed, and
- provided the customer a mechanism in the app user interface through which they can later rescind this permission and opt-out.

**10.5.3**

If you publish a person’s personal information to an outside service or third party through your app or its metadata, but the person whose information is being shared is not a customer of your app, you must obtain express written consent to publish that personal information, and you must permit the person whose information is shared to withdraw that consent at any time. If your app provides a customer with access to another person’s personal information, this requirement would also apply.

**10.5.4**

If your app collects, stores or transmits personal information, it must do so securely, by using modern cryptography methods.

**10.5.5**

Your app must not collect, store or transmit highly sensitive personal information, such as health or financial data, unless that information is related to the primary purpose of the app. Your app must not collect, store or transmit personal information unrelated to its primary purpose, without first obtaining express user consent.

### 10.6 Capabilities

The capabilities you declare must legitimately relate to the functions of your app, and the use of those declarations must comply with our app capability declarations. You must not circumvent operating system checks for capability usage.

For more information about app capability declarations, see [App capability declarations](/windows/uwp/packaging/app-capability-declarations).

### 10.7 Localization

You must localize your app for all languages that it supports. The text of your app’s description must be localized in each language that you declare. If your app is localized such that some features are not available in a localized version, you must clearly state or display the limits of localization in the app description. The experience provided by an app must be reasonably similar in all languages that it supports.

### 10.8 Financial Transactions

If your app includes in-app purchase, subscriptions, virtual currency, billing functionality or captures financial information, the following requirements apply:

**10.8.1**

You must use the Microsoft Store in-app purchase API to sell digital items or services that are consumed or used within your app. Your app may enable users to consume previously purchased digital content or services, but must not direct users to a purchase mechanism other than the Microsoft Store in-app purchase API.

In-app products sold in your app cannot be converted to any legally valid currency (e.g. USD, Euro, etc.) or any physical goods or services.

**10.8.2**

You must use the Microsoft payment request API or a secure third party purchase API for purchases of physical goods or services, and a secure third party purchase API for payments made in connection with real world gambling or charitable contributions. If your app is used to facilitate or collect charitable contributions or to conduct a promotional sweepstakes or contest, you must do so in compliance with applicable law. You must also state clearly that Microsoft is not the fundraiser or sponsor of the promotion.

The following requirements apply to your use of a secure third party purchase API:

- At the time of the transaction or when you collect any payment or financial information from the customer, your app must identify the commerce transaction provider, authenticate the user, and obtain user confirmation for the transaction.
- The app can offer the user the ability to save this authentication, but the user must have the ability to either require an authentication on every transaction or to turn off in-app transactions.
- If your app collects credit card information or uses a third-party payment processor that collects credit card information, the payment processing must meet the current PCI Data Security Standard (PCI DSS).

**10.8.3**

If your app collects financial account information, you must submit that app from a Business account type.

**10.8.4**

You must provide in-app purchase information about the types of in-app purchases offered and the range of prices. You may not mislead customers about the nature of your in-app promotions and offerings. In addition, your app must make it clear to users that they are initiating a purchase option in the app.

**10.8.5**

Your app may promote or distribute software only through the Microsoft Store.

**10.8.6**

You must use the Microsoft recurring billing API to bill for subscriptions of digital goods or services, and the following guidelines apply:

- You may add value to a subscription but may not remove value for users who have previously purchased it.
- If you discontinue an active subscription, you must continue to provide purchased digital goods or services until the subscription expires.

### 10.9 Notifications

Your app must respect system settings for notifications and remain functional when they are disabled. This includes the presentation of ads and notifications to the customer, which must also be consistent with the customer’s preferences, whether the notifications are provided by the Microsoft Push Notification Service (MPNS), Windows Push Notification Service (WNS) or any other service. If the customer disables notifications, either on an app-specific or system-wide basis, your app must remain functional.

If your app uses MPNS or WNS to transmit notifications, it must comply with the following requirements:

**10.9.1**

Because notifications provided through WNS or MPNS are considered app content, they are subject to all Store Policies.

**10.9.2**

You may not obscure or try to disguise the source of any notification initiated by your app.

**10.9.3**

You may not include in a notification any information a customer would reasonably consider to be confidential or sensitive.

**10.9.4**

Notifications sent from your app must relate to the app or to other apps you publish in the Store catalog, may link only to the app or the Store catalog listing of your other apps, and may not include promotional messages of any kind that are not related to your apps.

### 10.10 Advertising Conduct and Content

For all advertising related activities, the following requirements apply:

**10.10.1**

- The primary purpose of your app should not be to get users to click ads.
- Your app may not do anything that interferes with or diminishes the visibility, value, or quality of any ads it displays.
- Your app must respect advertising ID settings that the user has selected.

**10.10.2**

If you purchase or create promotional ad campaigns to promote your apps through the “Promote Your App” capability in Dev Center, all ad materials you provide to Microsoft, including any associated landing pages, must comply with Microsoft’s [Creative Specifications Policy](https://help.ads.microsoft.com/#apex/ads/en/n5095/0) and [Creative Acceptance Policy](https://help.ads.microsoft.com/#apex/ads/en/n5095/0).

**10.10.3**

Any advertising content your app displays must adhere to Microsoft’s [Creative Acceptance Policy](https://help.ads.microsoft.com/#apex/ads/en/n5095/0).

If your app displays ads, all content displayed must conform to the advertising requirements of the [App Developer Agreement](/legal/windows/agreements/app-developer-agreement), including the following requirements:

**10.10.4**

The primary content of your app may not be advertising, and advertising must be clearly distinguishable from other content in your app.

**10.10.5**

Your privacy statement or terms of use must let users know you will send personal information to the ad service provider and must tell users how they can opt-out of interest-based advertising.

**10.10.6**

If your app is directed at children under the age of 13 (as defined in the [Children’s Online Privacy Protection Act](https://go.microsoft.com/fwlink/p/?LinkID=623015)), you must notify Microsoft of this fact in Dev Center and ensure that all ad content displayed in your app is appropriate for children under the age of 13.

### 10.11 Mobile Voice Plans

Your app may not sell, link to, or otherwise promote mobile voice plans.

### 10.12 Edge Extensions

Edge Extensions are subject to these additional requirements:

- Your Extension must have a single purpose with narrowly scoped functionality that is clearly explained in the product description.
- Your Extension may collect personal information only as part of a prominently disclosed, user-facing feature.
- If your Extension collects web browsing activity, it must do so only if required by and only for use in a prominently disclosed, user-facing feature.
- The Extension must not programmatically alter, or appear to alter, browser functionality or settings including, but not limited to: the address bar search provider and suggestions, the start or home page, the new tab page, and adding or removing favorites and reading list items.

### 10.13 Gaming and Xbox

For apps that are primarily gaming experiences or target Xbox One, the following requirements apply:

**10.13.1**

Games that target Xbox One must use Xbox Live services through either the [Xbox Live Creators](https://go.microsoft.com/fwlink/?linkid=844722) or [ID@Xbox](https://go.microsoft.com/fwlink/?LinkId=821742) program.

**10.13.2**

Games that allow cross-player communication or synchronous network play on Xbox One devices must use Xbox Live and be approved through the [ID@Xbox](https://go.microsoft.com/fwlink/?LinkId=821742) program.

**10.13.3**

Games on Xbox One must not present an alternate friends list obtained outside Xbox Live.

**10.13.4**

Apps published to Xbox One must not:

- Include the sale of Xbox games, Xbox consoles or Xbox console accessories outside the Store.
- Request or store Microsoft Account usernames or passwords.

**10.13.5**

Games that use Xbox Live must:

- Automatically sign the user in to Xbox Live, or offer the user the option to sign in, before gameplay begins.
- Display the user's Xbox gamertag as their primary display and profile name.

**10.13.6**

Games that use Xbox Live and offer multiplayer gameplay, user generated content or user communication:

- Must not allow gameplay until the user signs in to Xbox Live.
- Must respect [parental and service controls](https://go.microsoft.com/fwlink/?linkid=860295).

**10.13.7**

Games must gracefully handle errors with or disconnection from the Xbox Live service. When attempting to retry a connection request following a failure, games must honor the retry policies set by Xbox Games. When they are unable to retrieve configuration information for or communicate with any non-Microsoft service, games must not direct users to Microsoft support.

**10.13.8**

Games must not store user information sourced from Xbox Live, such as profile data, preferences, or display names, beyond a locally stored cache used to support loss of network connectivity. Any such caches must be updated on the next available connection to the service.

**10.13.9**

Xbox Live games must comply with the following requirements for service usage:

- Do not link or federate the Xbox Live user account identifier or other user account data with other services or identity providers.
- Do not provide services or user data in a way that it could be included in a search engine or directory.
- Keep your secret key and access tokens private, except if you share them with an agent acting to operate your app and the agent signs a confidentiality agreement.
- Do not duplicate the Xbox Live Friends service.

**10.13.10**

Apps that emulate a game system are not allowed on any device family.

**10.13.11**

The following privacy requirements apply to Xbox Live user data:

- Services and user data are only for use in your game by you. Don't sell, license, or share any data obtained from us or our services. Your use of services and user data must comply with the then-current Microsoft Privacy Statement.
- Services and user data must be used appropriately in games. This data includes (without limitation) usage data, account identifiers and any other personally identifiable data, statistics, scores, ratings, rankings, connections with other users, and any other data relating to a user’s social activity.
- Don’t store any Xbox Live social graph data (e.g., friends lists), except for account identifiers for users who’ve linked their Xbox Live account with your game.
- Delete all account identifiers, when you remove your game from our service, or when a user unlinks their Xbox Live account from your game. Do not share services or user data (even if anonymous, aggregate, or derived data) to any ad network, data broker or other advertising or monetization-related service.

## Content Policies

The following policies apply to content and metadata (including publisher name, app name, app icon, app description, app screenshots, app trailers and trailer thumbnails, and any other app metadata) offered for distribution in the Store. Content means the app name, publisher name, app icon, app description, the images, sounds, videos and text contained in the app, the tiles, notifications, error messages or ads exposed through your app, and anything that’s delivered from a server or that the app connects to. Because apps and the Store are used around the world, these requirements will be interpreted and applied in the context of regional and cultural norms.

### 11.1 General Content Requirements

Metadata and other content you submit to accompany your app may contain only content that would merit a rating of PEGI 12, ESRB EVERYONE 10+, or lower.

### 11.2 Content Including Names, Logos, Original and Third Party

All content in your app and associated metadata must be either originally created by the application provider, appropriately licensed from the third-party rights holder, used as permitted by the rights holder, or used as otherwise permitted by law.

### 11.3 Harm to Others

**11.3.1**

Your app must not contain any content that facilitates or glamorizes the following real world activities: (a) extreme or gratuitous violence; (b) human rights violations; (c) the creation of illegal weapons; or (d) the use of weapons against a person, animal, or real or personal property.

**11.3.2**

Your app must not: (a) pose a safety risk to, nor result in discomfort, injury or any other harm to end users or to any other person or animal; or (b) pose a risk of or result in damage to real or personal property. You are solely responsible for all app safety testing, certificate acquisition, and implementation of any appropriate feature safeguards. You will not disable any platform safety or comfort features, and you must include all legally required and industry-standard warnings, notices, and disclaimers in your app.

### 11.4 Defamatory, Libelous, Slanderous and Threatening

Your app must not contain any content that is defamatory, libelous, slanderous, or threatening.

### 11.5 Offensive Content

Your app and associated metadata must not contain potentially sensitive or offensive content. Content may be considered sensitive or offensive in certain countries/regions because of local laws or cultural norms. In addition, your app and associated metadata must not contain content that advocates discrimination, hatred, or violence based on considerations of race, ethnicity, national origin, language, gender, age, disability, religion, sexual orientation, status as a veteran, or membership in any other social group.

### 11.6 Alcohol, Tobacco, Weapons and Drugs

Your app must not contain any content that facilitates or glamorizes excessive or irresponsible use of alcohol or tobacco products, drugs, or weapons.

### 11.7 Adult Content

Your app must not contain or display content that a reasonable person would consider pornographic or sexually explicit.

### 11.8 Illegal Activity

Your app must not contain content or functionality that encourages, facilitates or glamorizes illegal activity in the real world.

### 11.9 Excessive Profanity and Inappropriate Content

- Your app must not contain excessive or gratuitous profanity.
- Your app must not contain or display content that a reasonable person would consider to be obscene.

### 11.10 Country/Region Specific Requirements

Content that is offensive in any country/region to which your app is targeted is not allowed. Content may be considered offensive in certain countries/regions because of local laws or cultural norms. Examples of potentially offensive content in certain countries/regions include the following:

China

- Prohibited sexual content
- Disputed territory or region references
- Providing or enabling access to content or services that are illegal under applicable local law

### 11.11 Age Ratings

You must obtain an age rating for your app or game when you submit it in Dev Center. You are responsible for accurately completing the rating questionnaire to obtain the appropriate rating.

**11.11.3**

If your app provides content (such as user-generated, retail or other web-based content) that might be appropriate for a higher age rating than its assigned rating, you must enable users to opt in to receiving such content by using a content filter or by signing in with a pre-existing account.

---
<sup>1</sup>"Store" or "Microsoft Store" means a Microsoft owned or operated platform, however named, through which Apps may be offered to or acquired by Customers. Unless otherwise specified, Store includes the Microsoft Store, the Windows Store, the Xbox Store, Microsoft Store for Business, and Microsoft Store for Education.

### See also

- [Change history for Microsoft Store Policies](../store-policies-change-history.md)
- [Microsoft Store Policies and Code of Conduct](../store-policies-and-code-of-conduct.md)
- [App Developer Agreement](/legal/windows/agreements/app-developer-agreement)
