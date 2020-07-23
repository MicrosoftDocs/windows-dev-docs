---
Description: 
title: Private Audience setup
ms.assetid: 
ms.date: 7/23/2020
ms.topic: article
keywords: 
ms.localizationpriority: 
---

If you want your game/bundle/add-on to be visible only to selected people that you specify, choose **Enable Private audience**. With this option, the game/bundle/add-on will 
not be discoverable or available to anyone other than people in the group(s) you specify. This option is often used for [beta testing](https://docs.microsoft.com/en-us/windows/uwp/publish/beta-testing-and-targeted-distribution), as it lets you distribute your app to testers
without anyone else being able to get the app, or even see its Store listing (even if they were able to type in its Store listing URL).



When you choose **Enable Private audience**, you’ll need to specify at least one group of people who should get your app. You can choose from an existing [known user group](https://docs.microsoft.com/en-us/windows/uwp/publish/create-known-user-groups), or you can select Create a new group to define a new group. You’ll need to enter the email addresses associated with the Microsoft account of each person you’d like to include in the group. For more info, see [Create known user groups](https://docs.microsoft.com/en-us/windows/uwp/publish/create-known-user-groups).



After your game/bundle/add-on is published, the people in the group you specify will be able to view the listing and download it, as long as they are signed in with the Microsoft account associated with the email address that you entered and are running Windows 10, version 1607 or higher (including Xbox One). However, people who aren’t in your private audience won’t be able to view the app’s listing or download the app, regardless of what OS version they’re running. You can publish updated submissions to the private audience, which will be distributed to members of those audience in the same way as a regular app update (but still won’t be available to anyone who’s not in of your private audience, unless you change your audience selection).



At any time after using private audience, If you plan to make the app available to a public audience at a certain date and time, you can select the box labeled End Private Audience Restrictions on when creating your submission. Enter the date and time (in UTC/Local) when you’d like the product to become available to the public. Keep in mind the following:

* After the date and time that you select, all future submissions will use Public audience.
* Your game/bundle/add-on may go through an additional certification process, so be prepared to address any new certification issues that may arise.

Here are some important things to keep in mind when choosing to distribute your app to a private audience:

* People in your private audience will be able to get the app by using a specific link to your app’s Store listing that requires them to sign in with their Microsoft account in order to view it. This link is provided when you select **Private audience**. You can also find it on your [App identity](https://docs.microsoft.com/en-us/windows/uwp/publish/view-app-identity-details) page under **URL if your app is only visible to certain people (requires authentication)**. Be sure to give your testers this link, not the regular URL to your Store listing.
* Regardless of what you select in **Discoverability**, people in your private audience will need to use the link mentioned above to access the product. The listing will not be discoverable via search, even to people in that audience.
* Your game/bundle/add-on will be made available as per the Schedule you configure in the **Pricing and availability** page. 
* Other selections you make will apply to people in this audience. For example, if you choose a price other than **Free**, people in your private audience will have to pay that price in order to acquire the app.
* If you want to distribute different packages to different people in your private audience, after your initial submission you can use [package flights](https://docs.microsoft.com/en-us/windows/uwp/publish/package-flights) to distribute different package updates to subsets of your private audience. You can create additional known user groups to define who should get a specific package flight.
* While the Store will ensure that your game/bundle/add-on is only visible and available to people signed in with a Microsoft account that you’ve added to your private audience, we can’t prevent those people from sharing info or screenshots outside of your private audience. When confidentiality is critical, be sure that your private audience only includes people whom you trust not to share details about your app with others.
* If your game/bundle/add-on uses XBL, you must also set the Embargo date under Xbox Live Settings to protect your XBL social data
*	Make sure to let your testers know how to give you their feedback. You probably won’t want them to leave feedback in Feedback Hub, because any other customer could see that feedback. Consider including a link for them to send email or provide feedback in some other way.
* Any reviews written by people in your private audience will be available for you to view. However, these reviews won’t be published in your app’s Store listing, even after your submission is moved to **Public audience**. You can read reviews written by your private audience by viewing the [Reviews report](https://docs.microsoft.com/en-us/windows/uwp/publish/reviews-report), but you can't download this data or use the [Microsoft Store analytics API](https://docs.microsoft.com/en-us/windows/uwp/monetize/access-analytics-data-using-windows-store-services) to programmatically access these reviews.
*	When you move a game/bundle/add-on from **Private audience** to **Public audience**, the Release date shown on the Store listing will be the date it was first published to the public audience. Please note that this also applies if the product is not releasing until a future date; in this case please ensure you are setting the appropriate Display Release Date in Partner Center.


Recommended settings for common scenarios: 


**Scenario 1:** I do not know the exact date that my game/bundle/add-on is going to release but I would like to test my game under private audience
- Select the Enable Private Audience checkbox under the visibility section
- Ensure that the Schedule section has the following options selected

 png1
 
 **Scenario 2:** I know the date that my game/bundle/add-on is going to release but I would like to test my game under private audience and transition over to public audience
- Select the Enable Private Audience checkbox under the visibility section
- Select the End Private Audience Restrictions on and fill in the date you want your game to transition from private to public audience
- Ensure that the Schedule section has the following options selected


 

**Scenario 3:** My game/bundle has a pre-order, I would like to start the pre-order under private audience and then transition over to public audience
- 	Select the Enable Private Audience checkbox under the visibility section
-   Select the End Private Audience Restrictions on and fill in the date you want your game to transition from private to public audience

 
•	Fill out the dates in the Schedule section as per your product requirements
 
