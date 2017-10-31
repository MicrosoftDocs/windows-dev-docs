---
author: JnHs
Description: Learn how to create known user groups to use for package flighting and more.
title: Create known user groups
ms.author: wdg-dev-content
ms.date: 08/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, segment, segments, targeted group, customers
localizationpriority: high
---

# Create known user groups

Known user groups let you add specific people to a group, using the email address associated with their Microsoft account. These known user groups are most often used with [package flights](package-flights.md) to distribute specific packages to a selected group of people. They can also be used to send [targeted notifications](send-push-notifications-to-your-apps-customers.md) or [targeted offers](use-targeted-offers-to-maximize-engagement-and-conversions.md) to a group of specific customers as part of your engagement campaigns.

In order to be counted as a member of the group, each person must be authenticated with the Store using the Microsoft account associated with the email address you provide. For package flights, they must be using [a Windows 10 device that supports package flighting](package-flights.md) to download the app.


## To create a known user group

1.	In the Windows Dev Center dashboard, expand **Engage** in the left navigation menu and then select **Customer groups**. 
2.	In the **My customer groups** section, select **Create new group**.
3.	On the next page, select the **Known user group** radio button.
4.	In the **Group name** box, enter a name for your known user group.
5.	Enter the email addresses of the people you'd like to add to the group. You must include at least one email address, with a maximum of 10,000. You can enter email addresses directly into the field (separated by spaces, commas, semicolons, or line breaks), or you can click the **Import .csv** link to create the flight group from a list of email addresses in a .csv file.
6. Select **Save**.

The group will now be available for you to use.

You can also create a known user group by selecting **Create a flight group** from the [package flight](package-flights.md) creation page. Note that you'll need to re-enter any info you've already provided in the package flight creation page if you do this.

> [!IMPORTANT]
> When using known user groups with package flighting, be sure that you have obtained any necessary consent from people that you add to your group, and that they understand that they will be getting packages that are different from your non-flighted submission. 

## To edit a known user group

You cannot remove a known user group from your dashboard (or change its name) after it's been created, but you can edit its membership at any time.

To review and edit your known user groups, expand the **Engage** menu in the left navigation menu and select **Customer groups**. Under **My customer groups**, select the name of the group you want to edit. You can also edit a known user group from the package flight creation page by selecting **View and manage existing groups** when creating a new flight, or by selecting the group's name from a package flight's overview page. 

After you've selected the group you want to edit, you can add or remove email addresses directly in the field.

For larger changes, select **Export .csv** to save your group membership info to a .csv file. Make your changes in this file, then click **Import .csv** to use the new version to update the group membership.

Note that it may take up to 30 minutes for membership changes to be implemented. If you add people to a known user group after you've published a package flight for that group, the packages will be delivered to the new people automatically; you don't have to create and publish a new submission for that package flight. 






