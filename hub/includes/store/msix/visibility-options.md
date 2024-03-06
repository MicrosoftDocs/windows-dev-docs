The **Visibility** section of the [Pricing and availability page](../../../apps/publish/publish-your-app/price-and-availability.md) allows you to set restrictions on how your app can be discovered and acquired. This gives you the option to specify whether people can find your app in the Store or see its Store listing at all.

There are two separate sections within the Visibility section: **Audience** and **Discoverability**.

:::image type="content" source="images/msix-visibility-options.png" lightbox="images/msix-visibility-options.png" alt-text="A screenshot of the Pricing and availability section showing visibility options available for an app.":::

## Audience

The Audience section lets you specify whether you want to restrict the visibility of your submission to a specific audience that you define.

### Public audience

By default, your app’s Store listing will be visible to a **Public audience**. This is appropriate for most submissions, unless you want to limit who can see your app’s listing to specific people. You can also use the options in the [Discoverability](#discoverability) section to restrict discoverability if you’d like.

> [!IMPORTANT]
> If you submit a product with this option set to **Public audience**, you can't choose **Private audience** in a later submission.

### Private audience

If you want your app’s listing to be visible only to selected people that you specify, choose **Private audience**. With this option, the app will not be discoverable or available to anyone other than people in the group(s) you specify. This option is often used for [beta testing](../../../apps/publish/beta-testing-and-targeted-distribution.md), as it lets you distribute your app to testers without anyone else being able to get the app, or even see its Store listing (even if they were able to type in its Store listing URL).

When you choose **Private audience**, you’ll need to specify at least one group of people who should get your app. You can choose from an existing [known user group](../../../apps/publish/create-known-user-groups.md), or you can select **Create a new group** to define a new group. You’ll need to enter the email addresses associated with the Microsoft account of each person you’d like to include in the group. For more info, see [Create known user groups](../../../apps/publish/create-known-user-groups.md).

After your submission is published, the people in the group you specify will be able to view the app’s listing and download the app, as long as they are signed in with the Microsoft account associated with the email address that you entered and are running Windows 10, version 1607 or later (including Xbox One). However, people who aren’t in your private audience won’t be able to view the app’s listing or download the app, regardless of what OS version they’re running. You can publish updated submissions to the private audience, which will be distributed to members of those audience in the same way as a regular app update (but still won’t be available to anyone who’s not in of your private audience, unless you change your audience selection).

If you plan to make the app available to a public audience at a certain date and time, you can select the box labeled **Make this product public on** when creating your submission. Enter the date and time (in UTC) when you’d like the product to become available to the public. Keep in mind the following:

- The date and time that you select will apply to all markets. If you want to customize the release schedule for different markets, don’t use this box. Instead, create a new submission that changes your setting to **Public audience**, then use the [Schedule](../../../apps/publish/publish-your-app/release-schedule.md) options to specify your release timing.
- Entering a date for **Make this product public on** does not apply to the Microsoft Store for Business and/or Microsoft Store for Education. To allow us to offer your app to these customers through organizational licensing, you’ll need to create a new submission with **Public audience** selected (and [organizational licensing](../../../apps/publish/organizational-licensing.md) enabled).
- After the date and time that you select, all future submissions will use **Public audience**.

If you don’t specify a date and time to make your app available to a public audience, you can always do so later by creating a new submission and changing your audience setting from **Private audience** to **Public audience**. When you do so, keep in mind that your app may go through an additional certification process, so be prepared to address any new certification issues that may arise.

Here are some important things to keep in mind when choosing to distribute your app to a private audience:

- People in your private audience will be able to get the app by using a specific link to your app’s Store listing that requires them to sign in with their Microsoft account in order to view it. This link is provided when you select **Private audience**. You can also find it on your [App identity](../../../apps/publish/view-app-identity-details.md) page under **URL if your app is only visible to certain people (requires authentication)**. Be sure to give your testers this link, not the regular URL to your Store listing.
- Unless you choose an option in **Discoverability** that prevents it, people in your private audience will be able to find your app by searching within the Microsoft Store app. However, the web listing will not be discoverable via search, even to people in that audience.
- You won’t be able to [configure release dates in the Schedule section](../../../apps/publish/publish-your-app/release-schedule.md) of the **Pricing and availability page**, since your app won’t be released to customers outside of your private audience.
- Other selections you make will apply to people in this audience. For example, if you choose a price other than **Free**, people in your private audience will have to pay that price in order to acquire the app.
- If you want to distribute different packages to different people in your private audience, after your initial submission you can use [package flights](../../../apps/publish/package-flights.md) to distribute different package updates to subsets of your private audience. You can create additional known user groups to define who should get a specific package flight.
- You can edit the membership of the known user group(s) in your private audience. However, keep in mind that if you remove someone who was in the group and previously downloaded your app, they will still be able to use the app, but they won’t get any updates that you provide (unless you choose **Public audience** at a later date).
- Your app won't be available through the Microsoft Store for Business and/or Microsoft Store for Education, regardless of your organizational licensing settings, even to people in your private audience.
- While the Store will ensure that your app is only visible and available to people signed in with a Microsoft account that you’ve added to your private audience, we can’t prevent those people from sharing info or screenshots outside of your private audience. When confidentiality is critical, be sure that your private audience only includes people whom you trust not to share details about your app with others.
- Make sure to let your testers know how to give you their feedback. You probably won’t want them to leave feedback in Feedback Hub, because any other customer could see that feedback. Consider including a link for them to send email or provide feedback in some other way.
- Any reviews written by people in your private audience will be available for you to view. However, these reviews won’t be published in your app’s Store listing, even after your submission is moved to **Public audience**. You can read reviews written by your private audience by viewing the [Reviews report](../../../apps/publish/reviews-report.md), but you can't download this data or use the [Microsoft Store analytics API](/windows/uwp/monetize/access-analytics-data-using-windows-store-services) to programmatically access these reviews.
- When you move an app from **Private audience** to **Public audience**, the **Release date** shown on the Store listing will be the date it was first published to the public audience.

## Discoverability

The selections in the **Discoverability** section indicate how customers can discover and acquire your app.

> [!IMPORTANT]
> If you select **Private audience**, your **Discoverability** selections only apply to people in your private audience. Customers who are not in the groups you specified won’t be able to get the app or even see its listing.

### Make this product available and discoverable in the Store

This is the default option. Leave this option selected if you want your app to be listed in the Store for customers to find via the app's direct link and/or by other methods, including searching, browsing, and inclusion in curated lists.

### Make this product available but not discoverable in the Store

When you select this option, your app can’t be found in the Store by customers searching or browsing; the only way to get to your app’s listing is by a direct link.

> [!TIP]
> If you don’t want the listing to be viewable to the public, even with a direct link, choose **Private audience** in the **Audience** section, as described above.
You must also choose one of the following options to specify how your app can be acquired:

>[!IMPORTANT]
> Each of these options limits the OS versions on which customers can acquire your app. Please read the descriptions carefully to make sure you are aware which OS versions are supported.

- **Direct link only: Any customer with a direct link to the product’s listing can download it, except on Windows 8.x.** Any customer who gets to your app's listing via a direct link can download it on devices running Windows 10, Windows 11.
- **Stop acquisition: Any customer with a direct link can see the product’s Store listing, but they can only download it if they owned the product before, or have a promotional code and are using a Windows 11 or Windows 10 device.** Even if a customer has a direct link, they can't download the app unless they have a [promotional code](../../../apps/publish/generate-promotional-codes.md) and are using a Windows 11 or Windows 10 device. If a customer has a promotional code, they can use it to get your app for free (on Windows 11 and Windows 10 only), even though you aren't offering it to any other customers. Aside from using a promotional code, there is no way for anyone to get your app.

> [!TIP]
> If you want to stop offering an app to any new customers, you can select **Make app unavailable** from its overview page. After you confirm that you want to make the app unavailable, within a few hours it will no longer be visible in the Store, and no new customers will be able to get it (unless they have a [promotional code](../../../apps/publish/generate-promotional-codes.md) and are on a Windows 10 or Windows 11 device). This action will override the **Visibility** selections in your submission. To make the app available to new customers again (per your **Visibility** selections), you can click **Make app available** from the overview page at any time. For more info, see [Removing an app from the Store](../../../apps/publish/publish-your-app/app-package-management.md#removing-an-app-from-the-store).
