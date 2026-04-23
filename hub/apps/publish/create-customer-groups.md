---
description: Create customer groups that you can use to target a subset of your app's customer base for promotions, testing, or other purposes.
title: Create customer groups
ms.date: 10/30/2022
ms.topic: article
keywords: windows 10, uwp, customer groups, segment, flight group, known user group
ms.assetid: ad1a5a02-71aa-47d9-8d03-2d85f4941442
ms.localizationpriority: medium
---
# Create customer groups

You can create *customer groups* that include a subset of your app's customers. These groups can be used to target customers for promotions, testing, and other purposes.

## Known user groups

Known user groups let you add specific people to a group, using the email address associated with their Microsoft account. These known user groups are most often used to distribute specific packages to a selected group of people with package flights, or for distribution of a submission to a private audience. They can also be used for engagement campaigns, such as sending targeted notifications or targeted offers to a group of specific customers.

1. In [Partner Center](https://partner.microsoft.com/dashboard), expand **Engage** in the left navigation menu and then select **Customer groups**. 
2. In the **My customer groups** section, select **Create new group**.
3. On the next page, enter a name for your group in the **Group name** box.
4. Ensure that the **Known user group** radio button is selected.
5. Enter the email addresses of the people you'd like to add to the group. You must include at least one email address, with a maximum of 10,000. You can enter email addresses directly into the field (separated by spaces, commas, semicolons, or line breaks), or you can click the **Import .csv** link to create the flight group from a list of email addresses in a .csv file.
6. Select **Save**.

The group will now be available for you to use.

You can also create a known user group by selecting **Create a flight group** from the [package flight](package-flights.md#package-flights) creation page. Note that you'll need to re-enter any info you've already provided in the package flight creation page if you do this.

> [!IMPORTANT]
> When using known user groups with package flighting, be sure that you have obtained any necessary consent from people that you add to your group, and that they understand that they will be getting packages that are different from your non-flighted submission. 

## To edit a known user group

You cannot remove a known user group from Partner Center (or change its name) after it's been created, but you can edit its membership at any time.

To review and edit your known user groups, expand the **Engage** menu in the left navigation menu and select **Customer groups**. Under **My customer groups**, select the name of the group you want to edit. You can also edit a known user group from the package flight creation page by selecting **View and manage existing groups** when creating a new flight, or by selecting the group's name from a package flight's overview page. 

After you've selected the group you want to edit, you can add or remove email addresses directly in the field.

For larger changes, select **Export .csv** to save your group membership info to a .csv file. Make your changes in this file, then click **Import .csv** to use the new version to update the group membership.

Note that it may take up to 30 minutes for membership changes to be implemented. You don't need to publish a new submission in order for new group members to be able to access your submission through package flights or private audience; they will have access as soon as the changes are implemented.
