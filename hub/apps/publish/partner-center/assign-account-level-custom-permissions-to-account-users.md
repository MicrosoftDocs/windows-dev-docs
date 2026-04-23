---
title: Assign account level custom permissions to account users
description: Learn how to assign custom permissions at account level when adding users to your Partner Center account.
ms.date: 11/09/2022
ms.topic: article
ms.localizationpriority: high
---

# Assign account level custom permissions to account users

The permissions in this section cannot be limited to specific products. Granting access to one of these permissions allows the user to have that permission for the entire account.

| **Permission name** | **Read only** | **Read/write** |
| ---------- | ---------- | ---------- |
| **Account settings** | Can view all pages in the **Account settings** section, including <a href="/partner-center/partner-center-account-setup">contact info</a> | Can view all pages in the **Account settings** section. Can make changes to <a href="/partner-center/partner-center-account-setup">contact info</a> and other pages, but can’t make changes to the payout account or tax profile (unless that permission is granted separately).|
| **Account users** | Can view users that have been added to the account in the **User management** section.  | Can add users to the account and make changes to existing users in the **User management** section.  |
| **Account-level ad performance report** | Can view the account-level <a href="../advertising-performance-report.md">Advertising performance report</a>. | N/A |
| **Ad campaigns** | Can view <a href="/windows/uwp/monetize/">ad campaigns</a> created in the account. | Can create, manage, and view <a href="/windows/uwp/monetize/">ad campaigns</a> created in the account. |
| **Contact info** | Can view <a href="/partner-center/partner-center-account-setup">contact info</a> in the Account settings section. | Can edit and view <a href="/partner-center/partner-center-account-setup">contact info</a> in the Account settings section. |
| **Customer groups** | Can view <a href="../create-customer-groups.md">customer groups</a> (segments and known user groups). | Can create, edit, and view <a href="../create-customer-groups.md">customer groups</a> (segments and known user groups). |
| **Forums Private Space** | Can access the Private Space associated with this seller account on Xbox Developer Forums | N/A |
| **Manage product groups** *| Can view the new product group creation page, but can’t actually create new product groups. | Can create and edit product groups.  |
| **New apps** | Can view the new app creation page, but can’t actually create new apps in the account. | Can <a href="../publish-your-app/msix/reserve-your-apps-name.md">create new apps</a> in the account by reserving new app names, and can create submissions and submit apps to the Store. |
| **New bundles** *| Can view the new bundle creation page, but can’t actually create new bundles in the account. | Can create new bundles of products.  |
| **Order Xbox Dev Kits** *| N/A | Can order Xbox Dev Kits on the <a href="https://gamedevstore.partners.extranet.microsoft.com/">Entertainment Developer Store</a> |
| **Partner services** *| Can view certificates for installing to services to retrieve XTokens. | Can manage and view certificates for installing to services to retrieve XTokens. |
| **Payout account** | Can view <a href="/partner-center/set-up-your-payout-account#payout-account">payout account info</a> in **Account settings**. | Can edit and view <a href="/partner-center/set-up-your-payout-account#payout-account">payout account info</a> in **Account settings**. |
| **Payout summary** | Can view the <a href="/partner-center/payout-statement">Payout summary</a> to access and download payout reporting info. | Can view the <a href="/partner-center/payout-statement">Payout summary</a> to access and download payout reporting info. |
| **Relying parties** *| Can view relying parties to retrieve XTokens. | Can manage and view relying parties to retrieve XTokens. |
| **Request disc** *| Can view game disc requests. | Can build and view game disc requests |
| **Sandboxes** *| Can access the **Sandboxes** page and view sandboxes in the account and any applicable configurations for those sandboxes. Can’t view the products and submissions for each sandbox unless the appropriate product-level permissions are granted. | Can access the **Sandboxes** page and view and manage the sandboxes in the account, including creating and deleting sandboxes and managing their configurations. Can’t view the products and submissions for each sandbox unless the appropriate product-level permissions are granted. |
| **Tax profile** | Can view <a href="/partner-center/set-up-your-payout-account#tax-forms">tax profile info and forms</a> in **Account settings**. | Can fill out tax forms and update <a href="/partner-center/set-up-your-payout-account#tax-forms">tax profile info</a> in **Account settings**. |
| **Test accounts** *| Can view accounts for testing Xbox Live configuration. | Can create, manage, and view accounts for testing Xbox Live configuration. |
| **Ticketing Administrator** | N/A | Can view and edit all tickets created under this Partner Center account in the Game Creator Ticketing Portal. |
| **Xbox devices** | Can view the Xbox development consoles enabled for the account in the **Account settings** section. | Can add, remove, and view the Xbox development consoles enabled for the account in the **Account settings** section. |

\* Permissions marked with an asterisk (*) grant access to features which are not available to all accounts. If your account has not been enabled for these features, your selections for these permissions will not have any effect.

> [!NOTE]
> **Ad mediation reports**, **Ad performance reports**, **Affiliates performance reports** and **App install ads reports** were deprecated from the account level custom permissions.