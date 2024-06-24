---
title: Assign product level custom permissions to account users
description: Learn how to assign custom permissions at product level when adding users to your Partner Center account.
ms.date: 10/30/2022
ms.topic: article
ms.localizationpriority: high
---

# Assign product level custom permissions to account users

The permissions in this section can be granted to all products in the account, or can be customized to allow the permission only for one or more specific products.

Product-level permissions are grouped into four categories: **Analytics**, **Monetization**, **Publishing**, and **Xbox Live**. You can expand each of these categories to view the individual permissions in each category. You also have the option to enable **All permissions** for one or more specific products.

> [!Note]
> By default, an Owner or Manager has all custom permissions. Other standard roles such as Developer get assigned few custom permissions.

To grant a permission for every product in the account, make your selections for that permission (by toggling the box to indicate **Read only** or **Read/write**) in the row marked **All products**.

> [!TIP]
> Selections made for **All products** will apply to every product currently in the account, as well as any future products created in the account. To prevent permissions from applying to future products, select all of the products individually rather than choosing **All products**.

Below the **All products** row, you’ll see each product in the account listed on a separate row. To grant a permission for only a specific product, make your selections for that permission in the row for that product.

Each add-on is listed in a separate row underneath its parent product, along with an **All add-ons** row. Selections made for **All add-ons** will apply to all current add-ons for that product, as well as any future add-ons created for that product.

Note that some permissions cannot be set for add-ons. This is either because they don’t apply to add-ons (for example, the **Customer feedback** permission) or because the permission granted at the parent product level applies to all add-ons for that product (for example, **Promotional codes**). Note, however, that any permission that is available for add-ons must be set separately; add-ons do not inherit selections made for the parent product. For example, if you wish to allow a user to make pricing and availability selections for an add-on, you would need to enable the **Pricing and availability** permission for the add-on (or for **All add-ons**), whether or not you have granted the **Pricing and availability** permission for the parent product.

## Analytics

| **Permission name** | **Read only** | **Read/write** | **Read only (Add‑on)** | **Read‑write (Add‑on)** |
| ---------- | ---------- | ---------- |----------|----------|
| **Acquisitions** (including Near Real Time data) | Can view the <a href="..\acquisitions-report.md">Acquisitions</a> and <a href="..\acquisitions-report.md">Add-on acquisitions</a> reports for the product.   | N/A | N/A (settings for parent product include the **Add-on acquisitions** report) | N/A |
| **Usage**  | Can view the <a href="..\usage-report.md">Usage report</a> for the product. | N/A | N/A  | N/A |
| **Health** (including Near Real Time data) | Can view the <a href="..\health-report.md">Health report</a> for the product. |N/A | N/A | N/A |
| **Customer feedback** | Can view the <a href="..\reviews-report.md">Reviews</a> and <a href="..\feedback-report.md">Feedback</a> reports for the product. | N/A (to respond to feedback or reviews, the **Contact customer** permission must be granted) | N/A | N/A |
| **Xbox analytics** | Can view the <a href="..\xbox-analytics-report.md">Xbox analytics report</a> for the product. | N/A | N/A | N/A |

## Monetization

| **Permission name** | **Read only** | **Read/write** | **Read only (Add‑on)** | **Read‑write (Add‑on)** |
| ---------- | ---------- | ---------- |----------|----------|
| **Promotional codes** | Can view <a href="..\generate-promotional-codes.md">promotional code</a> orders and usage info for the product and its add-ons, and can view usage info. | Can view, manage, and create <a href="..\generate-promotional-codes.md">promotional code</a> orders for the product and its add-ons, and can view usage info. | N/A (settings for parent product apply to all add-ons) | N/A (settings for parent product apply to all add-ons) |
| **Targeted offers** | Can view <a href="..\use-targeted-offers-to-maximize-engagement-and-conversions.md">targeted offers</a> for the product. | Can view, manage and create <a href="..\use-targeted-offers-to-maximize-engagement-and-conversions.md">targeted offers</a> for the product. | N/A | N/A |
| **Contact customer** | Can view <a href="..\respond-to-customer-feedback.md">responses to customer feedback</a> and <a href="..\respond-to-customer-reviews.md">responses to customer reviews</a>, as long as the **Customer feedback** permission has been granted as well. Can also view <a href="..\send-push-notifications-to-your-apps-customers.md">targeted notifications</a> that have been created for the product. | Can <a href="..\respond-to-customer-feedback.md">respond to customer feedback</a> and <a href="..\respond-to-customer-reviews.md">respond to customer reviews</a>, as long as the **Customer feedback** permission has been granted as well. Can also <a href="..\send-push-notifications-to-your-apps-customers.md">create and send targeted notifications</a> for the product. | N/A | N/A |
| **Experimentation** | Can view <a href="\windows\uwp\monetize\run-app-experiments-with-a-b-testing">experiments (A/B testing)</a> and view experimentation data for the product. | Can create, manage, and view <a href="\windows\uwp\monetize\run-app-experiments-with-a-b-testing">experiments (A/B testing)</a> for the product, and view experimentation data. | N/A | N/A |
| **Store sale events** *| Can view sale event status for the product. | Can add the product to sale events and configure discounts. | Can view sale event status for the product. |Can add the product to sale events and configure discounts.  |

## Publishing

| **Permission name** | **Read only** | **Read/write** | **Read only (Add‑on)** | **Read‑write (Add‑on)** |
| ---------- | ---------- | ---------- |----------|----------|
| **Product Setup** | Can view the product setup page of products. | Can view and edit the product setup page of products. | Can view the product setup page of add-ons. | Can view and edit the product setup page add-ons. |
| **Pricing and availability** | Can view the <a href="../publish-your-app/price-and-availability.md">Pricing and availability</a> page of products. | Can view and edit the <a href="../publish-your-app/price-and-availability.md">Pricing and availability</a> page of products. | Can view the <a href="../publish-your-app/price-and-availability.md">Pricing and availability</a> page of add-ons. | Can view and edit the <a href="../publish-your-app/price-and-availability.md">Pricing and availability</a> page of add-ons. |
| **Properties** | Can view the <a href="../publish-your-app/enter-app-properties.md">Properties</a> page of products. | Can view and edit the <a href="../publish-your-app/enter-app-properties.md">Properties</a> page of products. | Can view the <a href="../publish-your-app/enter-app-properties.md">Properties</a> page of add-ons. | Can view and edit the <a href="../publish-your-app/enter-app-properties.md">Properties</a> page of add-ons. |
| **Age ratings** | Can view the <a href="../publish-your-app/age-ratings.md">Age ratings</a> page of products. | Can view and edit the <a href="../publish-your-app/age-ratings.md">Age ratings</a> page of products. | Can view the Age ratings page of add-ons. | Can view and edit the Age ratings page of add-ons. |
| **Packages** | Can view the <a href="../publish-your-app/upload-app-packages.md">Packages</a> page of products. | Can view and edit the <a href="../publish-your-app/upload-app-packages.md">Packages</a> page of products, including uploading packages. | Can view the <a href="../publish-your-app/upload-app-packages.md">Packages</a> page of addons (if applicable). | Can view and edit <a href="../publish-your-app/upload-app-packages.md">Packages</a> page of addons (if applicable). |
| **Store listings** | Can view the <a href="../publish-your-app/create-app-store-listing.md">Store listing page(s)</a> of products.| Can view and edit the <a href="../publish-your-app/create-app-store-listing.md">Store listing page(s)</a> of products, and can add new Store listings for different languages.| Can view the <a href="../publish-your-app/create-app-store-listing.md">Store listing page(s)</a> of add-ons.| Can view and edit the <a href="../publish-your-app/create-app-store-listing.md">Store listing page(s)</a> of add-ons, and can add Store listings for different languages.|
| **Store submission** | No access is granted if this permission is set to read-only. | Can submit the product to the Store and view certification reports. Includes both new and updated submissions. | No access is granted if this permission is set to read-only. | Can submit the add-on to the Store and view certification reports. Includes both new and updated submissions. |
| **New submission creation** | No access is granted if this permission is set to read-only.| Can create new <a href="../publish-your-app/create-app-submission.md">submissions</a> for the product.| No access is granted if this permission is set to read-only. | Can create new <a href="../publish-your-app/create-app-submission.md">submissions</a> for the add-on. |
| **New add-ons** | No access is granted if this permission is set to read-only. | Can <a href="../publish-your-app/overview.md">create new add-ons</a> for the product. | N/A | N/A |
| **Name reservations** | Can view the <a href="manage-app-name-reservations.md">Manage app names</a> page for the product. | Can view and edit the <a href="manage-app-name-reservations.md">Manage app names</a> page for the product, including reserving additional names and deleting reserved names. | Can view reserved names for the add-on. | Can view and edit reserved names for the add-on. |
| **Disc request** | Can view disc the request page. | Can create disc requests. | N/A | N/A |
| **Disc royalties** | Can view disc the royalties page. | Can create disc royalties. | N/A | N/A |

## Xbox Live \*

| **Permission name** | **Read only** | **Read/write** | **Read only (Add‑on)** | **Read‑write (Add‑on)** |
| ---------- | ---------- | ---------- |----------|----------|
| **Relying Parties** \* | Can view the Relying parties page of an account. | Can view and edit the Relying parties page of an account. | N/A | N/A |
| **Partner Services** * | Can view the Web services page of an account. | Can view and edit the Web services page of an account. | N/A | N/A |
| **Xbox Test Accounts** * | Can view the Xbox Test Accounts page of an account. | Can view and edit the Xbox Test Accounts page of an account. | N/A | N/A |
| **Xbox Test Accounts per Sandbox** * | Can view the Xbox Test Accounts page for only the specified sandboxes of an account. | Can view and edit the Xbox Test. | - | - |
| **Accounts page for only the specified sandboxes of an account** | N/A | N/A | - | - |
| **Xbox Devices** * | Can view the Xbox one development consoles page of an account. | Can view and edit the Xbox one development consoles page of an account. | N/A | N/A |
| **Xbox Devices per Sandbox** * | Can view the Xbox one development consoles page for only the specified sandboxes of an account. | Can view and edit the Xbox one development consoles page for only the specified sandboxes of an account. | N/A | N/A |
| **App Channels** * | N/A | Can publish promotional video channels to the Xbox console for viewing through OneGuide. | N/A | N/A |
| **Service Configuration** * | Can view the Xbox Live Service configuration page of a product. | Can view and edit the Xbox Live Service configuration page of a product. | N/A | N/A |
| **Tools Access** * | Can run Xbox Live tools on a product to only view data. | Can run Xbox Live tools on a product to view and edit data. | N/A | N/A |
| **Proprietary access** * | Can view support inquires. | Can view and edit support inquires. | N/A | N/A |

\* Permissions marked with an asterisk (*) grant access to features which are not available to all accounts. If your account has not been enabled for these features, your selections for these permissions will not have any effect.
