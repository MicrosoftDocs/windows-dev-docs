---
author: jnHs
Description: Set custom permissions for account users.
title: Set custom permissions for account users
ms.assetid: 99f3aa18-98b4-4919-bd7b-d78356b0bf78
ms.author: wdg-dev-content
ms.date: 06/19/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---

# Set custom permissions for account users

When you add users to your account, you can give them a [standard role](manage-account-users.md#roles-and-permissions), or you can choose to customize their permissions to provide the appropriate level of access to the user. Some of these permissions apply to the entire account, and some can either be granted to all products or limited to specific products. 

To use custom permissions rather than standard roles, click **Customize permissions** in the **Roles** section when adding or editing the user account. 

> [!NOTE] 
> The same permissions can be applied regardless of whether you are adding a user, a group, or an Azure AD application.

To enable a permission for the user, toggle the box to the appropriate setting. 

![Guide to access settings](images/permission_key.png)

- **No access**: The user will not have the indicated permission.
- **Read only**: The user will have access to view features related to the indicated area, but not to make changes.
- **Read/write**: The user will have access to make changes associated with the area, as well as viewing it.
- **Mixed**: You can’t select this option directly, but the **Mixed** indicator will show if you have allowed a combination of access for that permission. For example, if you grant **Read only** access to **Pricing and availability** for **All products**, but then grant **Read/write** access to **Pricing and availability** for one specific product, the **Pricing and availability** indicator for **All products** will show as Mixed. The same applies if some products have **No access** for a permission, but others have **Read/write** and/or **Read only** access.

For some permissions, such as those related to viewing analytic data, only **Read only** access can be granted. Note that in the current implementation, some permissions have no distinction between **Read only** and **Read/write** access. Please review the details for each permission to understand the specific capabilities granted by **Read only** and **Read/write** access.

The specific details about each permission are described in the tables below.

## Account-level permissions

The permissions in this section cannot be limited to specific products. Granting access to these permissions allows the user to have that permission for the entire account.

<table>
    <colgroup>
    <col width="20%" />
    <col width="40%" />
    <col width="40%" />
    </colgroup>
    <thead>
    <tr class="header">
    <th align="left">Permission name</th>
    <th align="left">Read only</th>
    <th align="left">Read/write</th>
    </tr>
    </thead>
    <tbody>
<tr><td align="left">    **Account settings**                    </td><td align="left">  Can view all pages in the **Account settings** section, including [contact info](managing-your-profile.md).       </td><td align="left">  Can view all pages in the **Account settings** section. Can make changes to [contact info](managing-your-profile.md) and other pages, but can’t make changes to the payout account or tax profile (unless that permission is granted separately).            </td></tr>
<tr><td align="left">    **Account users**                       </td><td align="left">  Can view users that have been added to the account in the **Manage users** section.          </td><td align="left">  Can add users to the account and make changes to existing users in the **Manage users** section.             </td></tr>
<tr><td align="left">    **Account-level ad performance report** </td><td align="left">  Can view the account-level [Advertising performance report](advertising-performance-report.md). (Can’t view advertising performance reports for individual products unless that permission is granted separately.)       </td><td align="left">  N/A   </td></tr>
<tr><td align="left">    **Ad campaigns**                        </td><td align="left">  Can view [ad campaigns](create-an-ad-campaign-for-your-app.md) created in the account.      </td><td align="left">  Can create, manage, and view [ad campaigns](create-an-ad-campaign-for-your-app.md) created in the account.          </td></tr>
<tr><td align="left">    **Ad mediation**                        </td><td align="left">  Can view [ad mediation configurations](https://msdn.microsoft.com/library/windows/apps/xaml/mt149935.aspx) for all products in the account.    </td><td align="left">  Can view and change [ad mediation configurations](https://msdn.microsoft.com/library/windows/apps/xaml/mt149935.aspx) for all products in the account.        </td></tr>
<tr><td align="left">    **Ad mediation reports**                </td><td align="left">  Can view the [Ad mediation report](ad-mediation-report.md) for all products in the account.    </td><td align="left">  N/A    </td></tr>
<tr><td align="left">    **Ad performance reports**              </td><td align="left">  Can view [Advertising performance reports](advertising-performance-report.md) for all products in the account. (Can’t view the account-level [Advertising performance report](advertising-performance-report.md) unless that permission is granted separately.)       </td><td align="left">  Can view [Advertising performance reports](advertising-performance-report.md) for all products in the account. (Can’t view the account-level [Advertising performance report](advertising-performance-report.md) unless that permission is granted separately.)         </td></tr>
<tr><td align="left">    **Ad units**                            </td><td align="left">  Can view the [ad units](monetize-with-ads.md) that have been created for the account.    </td><td align="left">  Can create, manage, and view [ad units](monetize-with-ads.md) for the account.             </td></tr>
<tr><td align="left">    **Affiliate ads**                       </td><td align="left">  Can view [affiliate ad](about-affiliate-ads.md) usage in all products in the account.    </td><td align="left">  Can manage and view [affiliate ad](about-affiliate-ads.md) usage for all products in the account.                </td></tr>
<tr><td align="left">    **Affiliates performance reports**      </td><td align="left">  Can view the [Affiliates performance report](affiliates-performance-report.md) for all products in the account.   </td><td align="left">  N/A   </td></tr>
<tr><td align="left">    **App install ads reports**             </td><td align="left">  Can view the [App install ads report](app-install-ads-reports.md) for all products in the account.           </td><td align="left">  N/A   </td></tr>
<tr><td align="left">    **Community ads**                       </td><td align="left">  Can view free [community ad](about-community-ads.md) usage for all products in the account.          </td><td align="left">  Can create, manage, and view free [community ad](about-community-ads.md) usage for all products in the account.               </td></tr>
<tr><td align="left">    **Contact info**                        </td><td align="left">  Can view [contact info](managing-your-profile.md) in the Account settings section.        </td><td align="left">  Can edit and view [contact info](managing-your-profile.md) in the Account settings section.            </td></tr>
<tr><td align="left">    **COPPA compliance**                    </td><td align="left">  Can view [COPPA compliance](monetize-with-ads.md#coppa-compliance) selections (indicating whether products are targeted at children under the age of 13) for all products in the account.                                            </td><td align="left">  Can edit and view [COPPA compliance](monetize-with-ads.md#coppa-compliance)  selections (indicating whether products are targeted at children under the age of 13) for all products in the account.         </td></tr>
<tr><td align="left">    **Customer groups**                     </td><td align="left">  Can view [customer groups](create-customer-groups.md) (segments and flight groups) in the **Customers** section.      </td><td align="left">  Can create, edit, and view [customer groups](create-customer-groups.md) (segments and flight groups) in the **Customers** section.       </td></tr>
<tr><td align="left">    **New apps**                            </td><td align="left">  Can view the new app creation page, but can’t actually create new apps in the account.    </td><td align="left">  Can [create new apps](create-your-app-by-reserving-a-name.md) in the account by reserving new app names, and can create submissions and submit apps to the Store.     </td></tr>
<tr><td align="left">    **New bundles**&nbsp;*                       </td><td align="left">  Can view the new bundle creation page, but can’t actually create new bundles in the account.     </td><td align="left">  Can create new bundles of products.          </td></tr>
<tr><td align="left">    **Partner services**&nbsp;*                  </td><td align="left">  Can view certificates for installing to services to retrieve XTokens.     </td><td align="left">  Can manage and view certificates for installing to services to retrieve XTokens.       </td></tr>
<tr><td align="left">    **Payout account**                      </td><td align="left">  Can view [payout account info](setting-up-your-payout-account-and-tax-forms.md#payout-account) in **Account settings**.     </td><td align="left">  Can edit and view [payout account info](setting-up-your-payout-account-and-tax-forms.md#payout-account) in **Account settings**.       </td></tr>
<tr><td align="left">    **Payout summary**                      </td><td align="left">  Can view the [Payout summary](payout-summary.md) to access and download payout reporting info.       </td><td align="left">  Can view the [Payout summary](payout-summary.md) to access and download payout reporting info.   </td></tr>
<tr><td align="left">    **Relying parties**&nbsp;*                   </td><td align="left">  Can view relying parties to retrieve XTokens.    </td><td align="left">  Can manage and view relying parties to retrieve XTokens.     </td></tr>
<tr><td align="left">    **Sandboxes**&nbsp;*                         </td><td align="left">  Can access the **Sandboxes** page and view sandboxes in the account and any applicable configurations for those sandboxes. Can’t view the products and submissions for each sandbox unless the appropriate product-level permissions are granted. </td><td align="left">  Can access the **Sandboxes** page and view and manage the sandboxes in the account, including creating and deleting sandboxes and managing their configurations. Can’t view the products and submissions for each sandbox unless the appropriate product-level permissions are granted.    </td></tr>
<tr><td align="left">    **Tax profile**                         </td><td align="left">  Can view [tax profile info and forms](setting-up-your-payout-account-and-tax-forms.md#tax-forms) in **Account settings**.     </td><td align="left">  Can fill out tax forms and update [tax profile info](setting-up-your-payout-account-and-tax-forms.md#tax-forms) in **Account settings**.     </td></tr>
<tr><td align="left">    **Test accounts**&nbsp;*                     </td><td align="left">  Can view accounts for testing Xbox Live configuration.      </td><td align="left">  Can create, manage, and view accounts for testing Xbox Live configuration.      </td></tr>
<tr><td align="left">    **Xbox devices**                        </td><td align="left">  Can view the Xbox development consoles enabled for the account in the **Account settings** section.       </td><td align="left">  Can add, remove, and view the Xbox development consoles enabled for the account in the **Account settings** section.     </td></tr>
    </tbody>
    </table>

\* Permissions marked with an asterisk (*) grant access to features which are not available to all accounts. If your account has not been enabled for these features, your selections for these permissions will not have any effect.   

## Product-level permissions

The permissions in this section can be granted to all products in the account, or can be customized to allow the permission only for one or more specific products. These permissions are grouped into four categories: **Analytics**, **Monetization**, **Publishing**, and **Xbox Live**. You can expand each of these categories to view the individual permissions in each category. 

To grant a permission for all products in the account, make your selections for that permission (by toggling the box to indicate **Read only**, **Read/write**, or **No access**) in the row marked **All products**. 
 
> [!TIP]
> Selections made for **All products** will apply to every product currently in the account, as well as any future products created in the account. To prevent permissions from applying to future products, select all of the products individually rather than choosing **All products**.

Below the **All products** row, you’ll see each product in the account listed on a separate row. To grant a permission for only a specific product, make your selections for that permission in the row for that product.

Each add-on is listed in a separate row underneath its parent product, along with an **All add-ons** row. Selections made for **All add-ons** will apply to all current add-ons for that product, as well as any future add-ons created for that product.

Note that some permissions cannot be set for add-ons. This is either because they don’t apply to add-ons (for example, the **Customer feedback** permission) or because the permission granted at the parent product level applies to all add-ons for that product (for example, **Promotional codes**). Note, however, that any permission that is available for add-ons must be set separately; add-ons do not inherit selections made for the parent product. For example, if you wish to allow a user to make pricing and availability selections for an add-on, you would need to enable the **Pricing and availability** permission for the add-on (or for **All add-ons**), whether or not you have granted the **Pricing and availability** permission for the parent product. 

### Analytics

<table>
    <thead>
    <tr class="header">
    <th align="left">Permission&nbsp;name</th>
    <th align="left">Read&nbsp;only</th>
    <th align="left">Read/write</th>
    <th align="left">Read&nbsp;only&nbsp;(Add&#8209;on) </th>
    <th align="left">Read&#8209;write&nbsp;(Add&#8209;on)</th>
    </tr>
    </thead>
    <tbody>
    <tr><td align="left">    **Acquisitions**     </td><td>    Can view the [Acquisitions](acquisitions-report.md) and [Add-on acquisitions](add-on-acquisitions-report.md) reports for the product.        </td><td>    N/A    </td><td>    N/A (settings for parent product include Add-on acquisition reports)        </td><td>    N/A                         </td></tr>
    <tr><td align="left">    **Usage** </td><td>    Can view the [Usage report](usage-report.md) for the product.     </td><td>    N/A       </td><td>    N/A     </td><td>    N/A         </td></tr>
    <tr><td align="left">    **Health** </td><td>    Can view the [Health report](health-report.md) for the product.    </td><td>    N/A     </td><td>    N/A     </td><td>    N/A         </td></tr>
    <tr><td align="left">    **Customer feedback**    </td><td>    Can view the [Ratings](ratings-report.md), [Reviews](reviews-report.md), and [Feedback](feedback-report.md) reports for the product.       </td><td>    N/A (to respond to feedback or reviews, the **Contact customer** permission must be granted)   </td><td>    N/A     </td><td>    N/A         </td></tr>
    <tr><td align="left">    **Xbox analytics** </td><td>    Can view the Xbox analytics report for the product. (Note: This report is not yet available.)    </td><td>    N/A   </td><td>    N/A       </td><td>    N/A          </td></tr>
    <tr><td align="left">    **Real time**   </td><td>    Can view the Real time report for the product.       </td><td>    N/A   </td><td>    N/A     </td><td>    N/A                 </td></tr>
    </tbody>
    </table>

### Monetization

<table>
    <thead>
    <tr class="header">
    <th align="left">Permission&nbsp;name</th>
    <th align="left">Read&nbsp;only</th>
    <th align="left">Read/write</th>
    <th align="left">Read&nbsp;only&nbsp;(Add&#8209;on) </th>
    <th align="left">Read&#8209;write&nbsp;(Add&#8209;on)</th>
    </tr>
    </thead>
    <tbody>
    <tr><td align="left">    **Contact customer**  </td><td>    Can view [responses to customer feedback](respond-to-customer-feedback.md) and [responses to customer reviews](respond-to-customer-reviews.md), as long as the **Customer feedback** permission has been granted as well. Can also view [targeted notifications](send-push-notifications-to-your-apps-customers.md) that have been created for the product.    </td><td>    Can [respond to customer feedback](respond-to-customer-feedback.md), [respond to customer reviews](respond-to-customer-reviews.md), as long as the **Customer feedback** permission has been granted as well. Can also [create and send targeted notifications](send-push-notifications-to-your-apps-customers.md) for the product.                   </td><td>    N/A         </td><td>    N/A                          </td></tr>
    <tr><td align="left">    **Experimentation**</td><td>    Can view [experiments (A/B testing)](../monetize/run-app-experiments-with-a-b-testing.md) and view experimentation data for the product.   </td><td>    Can create, manage, and view [experiments (A/B testing)](../monetize/run-app-experiments-with-a-b-testing.md) for the product, and view experimentation data.     </td><td>    N/A  </td><td>    N/A                 </td></tr>
    <tr><td align="left">    **Promotional codes**     </td><td>    Can view [promotional code](generate-promotional-codes.md) orders and usage info for the product and its add-ons, and can view usage info.         </td><td>    Can view, manage, and create [promotional code](generate-promotional-codes.md) orders for the product and its add-ons, and can view usage info.          </td><td>    N/A (settings for parent product apply to all add-ons)     </td><td>    N/A (settings for parent product apply to all add-ons)     </td></tr>
    </tbody>
    </table>

### Publishing 

<table>
    <thead>
    <tr class="header">
    <th align="left">Permission&nbsp;name</th>
    <th align="left">Read&nbsp;only</th>
    <th align="left">Read/write</th>
    <th align="left">Read&nbsp;only&nbsp;(Add&#8209;on) </th>
    <th align="left">Read&#8209;write&nbsp;(Add&#8209;on)</th>
    </tr>
    </thead>
    <tbody>
    <tr><td align="left">    **Pricing and availability**  </td><td>    Can view the [Pricing and availability](set-app-pricing-and-availability.md) page of product submissions.     </td><td>    Can view and edit the [Pricing and availability](set-app-pricing-and-availability.md) page of product submissions. </td><td>    Can view the [Pricing and availability](set-add-on-pricing-and-availability.md) page of add-on submissions.   </td><td>    Can view and edit the [Pricing and availability](set-add-on-pricing-and-availability.md) page of add-on submissions.          </td></tr>
    <tr><td align="left">    **Properties**   </td><td>    Can view the [Properties](enter-app-properties.md) page of product submissions.      </td><td>    Can view and edit the [Properties](enter-app-properties.md) page of product submissions.       </td><td>    Can view the [Properties](enter-add-on-properties.md) page of add-on submissions.     </td><td>    Can view and edit the [Properties](enter-add-on-properties.md) page of add-on submissions.               </td></tr>
    <tr><td align="left">    **Age ratings**    </td><td>    Can view the [Age ratings](age-ratings.md) page of product submissions.       </td><td>    Can view and edit the [Age ratings](age-ratings.md) page of product submissions.    </td><td>    * Can view the Age ratings page of add-on submissions.          </td><td>    * Can view and edit the Age ratings page of add-on submissions.       </td></tr>
    <tr><td align="left">    **Packages**        </td><td>    Can view the [Packages](upload-app-packages.md) page for product submissions.  </td><td>    Can view and edit the [Packages](upload-app-packages.md) page for product submissions, including uploading packages.     </td><td>    * Can view device family targeting and packages (if applicable) for add-on submissions.   </td><td>    * Can view and edit device family targeting for add-on submissions, including uploading packages if applicable.             </td></tr>
    <tr><td align="left">    **Store listings**  </td><td>    Can view the [Store listing page(s)](create-app-store-listings.md) for product submissions.  </td><td>    Can view and edit the [Store listing page(s)](create-app-store-listings.md) for product submissions, and can add new Store listings for different languages.     </td><td>    Can view the [Store listing page(s)](create-add-on-store-listings.md) for add-on submissions.            </td><td>    Can view and edit the [Store listing page(s)](create-add-on-store-listings.md) for add-on submissions, and can add Store listings for different languages.                 </td></tr>
    <tr><td align="left">    **Store submission**     </td><td>    No access is granted if this permission is set to read-only.           </td><td>    Can submit the product to the Store and view certification reports. Includes both new and updated submissions. </td><td>No access is granted if this permission is set to read-only.     </td><td>    Can submit the add-on to the Store and view certification reports. Includes both new and updated submissions.</td></tr>
    <tr><td align="left">    **New submission creation**       </td><td>    No access is granted if this permission is set to read-only.        </td><td>    Can create new [submissions](app-submissions.md) for the product.  </td><td>    No access is granted if this permission is set to read-only.   </td><td>    Can create new [submissions](add-on-submissions.md) for the add-on.        </td></tr>
    <tr><td align="left">    **New add-ons**    </td><td>    No access is granted if this permission is set to read-only. </td><td>    Can [create new add-ons](set-your-add-on-product-id.md) for the product. </td><td>    N/A    </td><td>    N/A        </td></tr>
    <tr><td align="left">    **Name reservations**   </td><td>    Can view the [Manage app names](manage-app-names.md) page for the product.</td><td>    Can view and edit the [Manage app names](manage-app-names.md) page for the product, including reserving additional names and deleting reserved names. </td><td>   * Can view reserved names for the add-on.    </td><td>   * Can view and edit reserved names for the add-on.          </td></tr>
    </tbody>
    </table>

### Xbox Live \*

<table>
    <thead>
    <tr class="header">
    <th align="left">Permission&nbsp;name</th>
    <th align="left">Read&nbsp;only</th>
    <th align="left">Read/write</th>
    <th align="left">Read&nbsp;only&nbsp;(Add&#8209;on) </th>
    <th align="left">Read&#8209;write&nbsp;(Add&#8209;on)</th>
    </tr>
    </thead>
    <tbody>
    <tr><td align="left">    **Xbox service configuration**&nbsp;\*    </td><td>    Can view settings related to achievements, multiplayer, leaderboards and other Xbox Live configuration for the product.  </td><td>    Can view and edit settings related to achievements, multiplayer, leaderboards and other Xbox Live configuration for the product.  </td><td>    N/A     </td><td>    N/A                      </td></tr>
    <tr><td align="left">    **App channels**&nbsp;\*</td><td>    N/A  </td><td>    Can publish promotional video channels to the Xbox console for viewing through OneGuide.  </td><td>  N/A </td><td> N/A </td></tr>
</tbody>
</table>

\* Permissions marked with an asterisk (*) grant access to features which are not available to all accounts. If your account has not been enabled for these features, your selections for these permissions will not have any effect.  
