---
description: Distribute different versions of your app to portions of your Microsoft Store audience at the same time.
title: Parallel package flighting (Preview)
ms.date: 09/11/2026
ms.topic: article
ms.localizationpriority: medium
---

# Parallel package flighting (Preview)

Parallel package flighting lets you distribute different versions of your app to portions of your Microsoft Store audience at the same time. Create device-based audience groups and associate each with a package flight. You don't need to collect testers' Microsoft account email addresses.

Use parallel package flighting to compare package-level changes, test an architecture or framework migration, or investigate a regression before expanding an update. Your app keeps the same Store listing; only the packages delivered to eligible devices differ.

> [!NOTE]
> This feature is in preview. Developers who see the parallel package flighting preview banner in Partner Center can use it. No separate preview access request is required. 

## What's new

| Capability                           | What you can do                                                        |
|-----------------------------------|---------------------------------------------------------------------------|
| **Device-based audiences**           | Target a percentage of the device population instead of maintaining a list of known users. |
| **Parallel package versions**         | Associate different device groups with different flights and run multiple package rollouts concurrently. |
| **Consistent cohort assignment**         | Reuse an audience identifier and range to keep targeting the same device group. |
| **Gradual rollout within a group**      | Start with a portion of a device rollout group, expand the rollout, or halt it before publishing a fix. |

Existing [known user groups](https://learn.microsoft.com/windows/apps/publish/create-customer-groups#known-user-groups) remain available when you want to test with specific people.

## Before you begin

You need:
* Access to your app in Partner Center and the parallel package flighting preview banner.
* Packages prepared for package flighting, including compatible packages for the device families, architectures and OS versions your app supports.
* A plan for your groups, flight priorities, package versions, and the rollout percentage for each flight.

## Understand device rollout groups

A device rollout group is defined by three fields:

| Field                           | Description                                                        |
|-----------------------------------|---------------------------------------------------------------------------|
| Audience identifier	| A value used to determine device assignment. Reuse the same identifier across groups when you want their ranges to partition the same device population. |
| Range start (%)	| The beginning of the group's range within that population. |
| Range end (%) |	The exclusive upper boundary of the percentage of the population to be included in the group. |

For example, create these two groups using the same audience identifier:

| Group name |	Range start (%)	| Range end (%)	| Audience range |
|-----------------------------------|---------------------------------------------------------------------------|
| Device rollout group A |	0	| 10	| 0% up to, but not including, 10% |
| Device rollout group B	| 10	| 20	| 10% up to, but not including, 20% |

These ranges don't overlap. Devices outside both groups aren't eligible for either group's flight unless another selected group makes them eligible.
Windows determines membership automatically from the device and audience identifier. A range isn't a list of customers or the order in which they installed your app. Group sizes represent a proportion of the device population, not an exact device count.

Keep the audience identifier unchanged to preserve assignment. Changing the identifier changes which devices can fall into a range. If you reuse an identifier across apps, the same device maps to the same position in the audience range; it still needs to be eligible for each app.

> [!IMPORTANT]
> Non-overlapping ranges are mutually exclusive only when they use the same audience identifier. Groups with different identifiers can overlap even when their ranges look different.

## Step-by-step flow

### 1. Open the preview in Partner Center

:::image type="content" source="images/parallel-package-flight-0-1.png" lightbox="images/parallel-package-flight-0-1.png" alt-text="Parallel package flighting preview banner in Partner Center.":::

Sign in to Partner Center and open **Apps and games** dashboard. Confirm that the parallel package flighting preview banner is available for your account.

### 2. Create your device rollout group

:::image type="content" source="images/parallel-package-flight-1.png" lightbox="images/parallel-package-flight-1.png" alt-text="Parallel package flighting navigation.":::

1.	In **Apps and games**, go to **Engage** > **Customer groups**.

:::image type="content" source="images/parallel-package-flight-1-1.png" lightbox="images/parallel-package-flight-1-1.png" alt-text="Parallel package flighting create new group.":::

2.	Select **Create new** group.

:::image type="content" source="images/parallel-package-flight-2.png" lightbox="images/parallel-package-flight-2.png" alt-text="Parallel package flighting group details.":::

3.	Enter a descriptive **Group name**, such as Contoso Device Rollout Group.
4.	Select **Device rollout group**.
5.	Enter your **Audience identifier**.
6.	Use the slider to select the **Range start (%)**, and **Range end (%)**.
7.	Select **Save**.
8.	Repeat for each additional group. For separate portions of the same audience, reuse the audience identifier and choose non-overlapping ranges.

For the example above, give Device rollout group A a start of 0 and end of 10. Give Device rollout group B a start of 10 and end of 20, using the same audience identifier.

### 3. Create a package flight for each group

:::image type="content" source="images/parallel-package-flight-3-1.png" lightbox="images/parallel-package-flight-3-1.png" alt-text="Parallel package flighting create package flight.":::

9.	Open your app's overview page.
10.	In **Manage package flights**, select **Create new package flight**.

:::image type="content" source="images/parallel-package-flight-4.png" lightbox="images/parallel-package-flight-4.png" alt-text="Parallel package flighting details.":::

11.	Enter a name for the flight, such as Contoso-Flight.
12.	Select the device-rollout  group “Contoso Device Rollout Group” created earlier to associate with it.
13.	If other flights exist, review the flight's **Rank**.
14.	Select **Create flight**.

You can associate more than one group with a flight, including a combination of device rollout groups and known user groups. Membership is a union: matching any selected group can make a user or device eligible. Selecting two groups does not require membership in both.

If a device or user is eligible for multiple flights, the highest-ranked eligible flight takes precedence. 

### 4. Add packages and choose the rollout percentage

:::image type="content" source="images/parallel-package-flight-5.png" lightbox="images/parallel-package-flight-5.png" alt-text="Parallel package flighting upload packages.":::

15.	Open **Packages** for the flight.
16.	Upload the packages for that flight, or select packages from a previously published submission.
17.	If you want to start with only part of the cohort, select **Roll out update gradually after this submission is published** and enter the initial percentage.
18.	Select **Save**.

:::image type="content" source="images/parallel-package-flight-6.png" lightbox="images/parallel-package-flight-6.png" alt-text="Parallel package flighting submit packages.":::

19.	Review **Flight options** if you need to change when the flight is published.

The rollout percentage applies **within the flight's audience**, not to your entire app audience. For example, a 10% rollout within a group covering 10% of the app's eligible device population reaches approximately 1% of eligible devices.

Without gradual rollout, the packages are made available to the flight's eligible audience when published.

> [!NOTE]
> Eligibility does not guarantee installation. Actual delivery depends on factors such as device activity, package compatibility, installed versions, and update checks.

### 5. Publish and monitor the flights

:::image type="content" source="images/parallel-package-flight-7.png" lightbox="images/parallel-package-flight-7.png" alt-text="Parallel package flighting review.":::

Review the flight, then select **Submit to the Store**. 

:::image type="content" source="images/parallel-package-flight-8.png" lightbox="images/parallel-package-flight-8.png" alt-text="Parallel package flighting certification.":::

After the submission passes certification and is published, eligible devices can receive the packages assigned to that flight.

:::image type="content" source="images/parallel-package-flight-9.png" lightbox="images/parallel-package-flight-9.png" alt-text="Parallel package flighting analytics.":::

Return to the app overview to monitor and manage each flight independently. Use your existing package-version analytics and app telemetry to assess stability and compare the package versions. You can view reports for flighted package versions in Partner Center under Insights, on the Summary, Reviews, and Usage pages.

## Manage an active rollout

### Expand or finalize a rollout

On the app overview, increase the rollout percentage for the relevant flight and select **Update %**. When you're ready to release the packages to the entire flight audience, select **Finalize package rollout**.

### Halt a rollout and publish a fix

:::image type="content" source="images/parallel-package-flight-9-1.png" lightbox="images/parallel-package-flight-9-1.png" alt-text="Parallel package flighting halt rollout.":::

1.	Select **Halt package rollout** for the affected flight to stop further distribution of the current rollout package.

:::image type="content" source="images/parallel-package-flight-10.png" lightbox="images/parallel-package-flight-10.png" alt-text="Parallel package flighting halt rollout confirmation.":::

2.	Select **OK** to confirm.
3.	Create an update for the **same flight** using the same steps as before.
4.	Upload a newer package version that contains your fix.
5.	Keep the same device rollout group definition and set the rollout percentage for the new submission.
6.	Submit the updated flight.

Keeping the group definition and rollout percentage unchanged lets you target the same rollout subset again. Don't change the audience identifier if your intention is to return to the same group.

> [!WARNING]
> Halting a rollout does not uninstall or downgrade packages already installed. Devices that received the halted version keep it until an applicable newer version is available.

### Change a group

In **Customer groups**, open the group and edit its audience identifier or range. Group names can't be changed.

Changes affect all flights linked to that group. Expanding a range makes additional devices eligible. Reducing a range stops excluded devices from receiving further packages through that group, but it does not remove a package they already received. Another group or flight can still make those devices eligible.

### Release a flight package to all customers

Create an update to your app's **non-flighted submission**. On **Packages**, use the option to copy packages from a package flight, select the packages you want to release, and complete the regular submission process.

The normal validation and certification requirements for a non-flighted submission apply.

For the existing flight update and deletion procedures, see [Update or modify your package flight](https://learn.microsoft.com/windows/apps/publish/package-flights#update-or-modify-your-package-flight) and [Delete a package flight](https://learn.microsoft.com/windows/apps/publish/package-flights#delete-a-package-flight).

## Important behavior and limitations

* Targeting is device-based.
* The Store listing is shared. A flight changes packages, not the app's listing.
* Device rollout groups are for package flights. They aren't used to define a private audience for an app's initial release.
* Ranks don't enable downgrades. Flight priority determines which eligible flight takes precedence.
* Device rollout groups aren't customer lists. Partner Center doesn't expose which individual devices belong to a device cohort.

## Frequently asked questions

1. **Do I need a list of testers' Microsoft accounts?**

    No. Device rollout groups let the Store determine eligibility automatically. Use known user groups when you want to target specific people such as internal testers.

2. **How is this different from gradual package rollout?**

    Gradual package rollout controls how widely one update is distributed. Parallel package flighting lets you distribute different packages to different device groups at the same time. You can also use gradual rollout within each flight.

3. **Can I run two flights without overlapping audiences?**

    Yes. Use the same audience identifier with non-overlapping ranges, associate each group with its own flight, and make sure other selected groups don't introduce overlap. Different audience identifiers do not guarantee separate audiences.

4. **Can I go back to an older package if an experiment fails?**

    Not by halting the rollout or shrinking a group. Those actions don't downgrade installed packages. Publish the required fixes in a package with a newer version number.

5. **Does finalizing a flight make its packages the regular Store version?**

    No. To release those packages to all customers, include them in a non-flighted submission and complete the regular publishing process.

## Need help?

For help with Partner Center or app publishing, contact [Windows developer support](https://aka.ms/windowsdevelopersupport).
