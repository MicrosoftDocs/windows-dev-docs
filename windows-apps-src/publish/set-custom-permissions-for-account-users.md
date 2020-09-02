---
title: Set roles or custom permissions for account users
description: Learn how to set standard roles or custom permissions when adding users to your Partner Center account.
ms.assetid: 99f3aa18-98b4-4919-bd7b-d78356b0bf78
ms.date: 10/31/2018
ms.topic: article
keywords: windows 10, uwp, user roles, user permission, custom roles, user access, customize permissions, standard roles
ms.localizationpriority: medium
---
# Set roles or custom permissions for account users

When you [add users to your Partner Center account](add-users-groups-and-azure-ad-applications.md), you'll need to specify what access they have within the account. You can do this by assigning them [standard roles](#roles) which applies to the entire account, or you can [customize their permissions](#custom) to provide the appropriate level of access. Some of the custom permissions apply to the entire account, and some can be limited to one or more specific products (or granted to all products, if you prefer).

> [!NOTE] 
> The same roles and permissions can be applied regardless of whether you are adding a user, a group, or an Azure AD application.

When determining what role or permissions to apply, keep in mind: 
-   Users (including groups and Azure AD applications) will be able to access the entire Partner Center account with the permissions associated with their assigned role(s), unless you [customize permissions](#custom) and assign [product-level permissions](#product-level-permissions) so that they can only work with specific apps and/or add-ons.
-   You can allow a user, group, or Azure AD application to have access to more than one role's functionality by selecting multiple roles, or by using custom permissions to grant the access you'd like.
-   A user with a certain role (or set of custom permissions) may also be part of a group that has a different role (or set of permissions). In that case, the user will have access to all of the functionality associated with both the group and the individual account.

> [!TIP]
> This topic is specific to the Windows apps developer program in [Partner Center](https://partner.microsoft.com/dashboard). For info about user roles in the Hardware Developer Program, see [Managing User Roles](/windows-hardware/drivers/dashboard/managing-user-roles). For info about user roles in the Windows Desktop Application Program, see [Windows Desktop Application Program](/windows/desktop/appxpkg/windows-desktop-application-program#add-and-manage-account-users).


<span id="roles" />

## Assign roles to account users

By default, a set of standard roles is presented for you to choose from when you add a user, group, or Azure AD application to your Partner Center account. Each role has a specific set of permissions in order to perform certain functions within the account. 

Unless you opt to define [custom permissions](#custom) by selecting **Customize permissions**, each user, group, or Azure AD application that you add to an account must be assigned at least one of the following standard roles. 

> [!NOTE]
> The **owner** of the account is the person who first created it with a Microsoft account (and not any user(s) added through Azure AD). This account owner is the only person with complete access to the account, including the ability to delete apps, create and edit all account users, and change all financial and account settings. 


| Role                 | Description              |
|----------------------|--------------------------|
| Manager              | Has complete access to the account, except for changing tax and payout settings. This includes managing users in Partner Center, but note that the ability to create and delete users in the Azure AD tenant is dependent on the account's permission in Azure AD. That is, if a user is assigned the Manager role, but does not have global administrator permissions in the organization's Azure AD, they will not be able to create new users or delete users from the directory (though they can change a user's Partner Center role). <p> Note that if the Partner Center account is associated with more than one Azure AD tenant, a Manager can’t see complete details for a user (including first name, last name, password recovery email, and whether they are an Azure AD global administrator) unless they are signed in to the same tenant as that user with an account that has global administrator permissions for that tenant. However, they can add and remove users in any tenant that is associated with the Partner Center account. |
| Developer            | Can upload packages and submit apps and add-ons, and can view the [Usage report](usage-report.md) for telemetry details. Can access [Cross-Device Experiences](https://developer.microsoft.com/windows/project-rome) functionality. Can’t view financial info or account settings.   |
| Business Contributor | Can view [Health](health-report.md) and [Usage](usage-report.md) reports. Can't create or submit products, change account settings, or view financial info.   |
| Finance Contributor  | Can view [payout reports](payout-summary.md), financial info, and acquisition reports. Can’t make any changes to apps, add-ons, or account settings.    |
| Marketer             | Can [respond to customer reviews](respond-to-customer-reviews.md) and view non-financial [analytic reports](analytics.md). Can’t make any changes to apps, add-ons, or account settings.      |

The table below shows some of the specific features available to each of these roles (and to the account owner).

|                                                       |    Account owner                 |    Manager                       |    Developer                     |    Business Contributor    |    Finance Contributor    |    Marketer                      |
|-------------------------------------------------------|----------------------------------|----------------------------------|----------------------------------|----------------------------|---------------------------|----------------------------------|
|    Acquisition report (including Near Real Time data) |    Can view                      |    Can view                      |    No access                     |    No access               |    Can view               |    No access                     |
|    Feedback report/responses                          |    Can view and send feedback    |    Can view and send feedback    |    Can view and send feedback    |    No access               |    No access              |    Can view and send feedback    |
|    Health report (including Near Real Time data)      |    Can view                      |    Can view                      |    Can view                      |    Can view                |    No access              |    No access                     |
|    Usage report                                       |    Can view                      |    Can view                      |    Can view                      |    Can view                |    No access              |    No access                     |
|    Payout account                                     |    Can update                    |    No access                     |    No access                     |    No access               |    Can update             |    No access                     |
|    Tax profile                                        |    Can update                    |    No access                     |    No access                     |    No access               |    Can update             |    No access                     |
|    Payout summary                                     |    Can view                      |    No access                     |    No access                     |    No access               |    Can view               |    No access                     |

If none of the standard roles are appropriate, or you wish to limit access to specific apps and/or add-ons, you can grant custom permissions to the user by selecting **Customize permissions**, as described below.


<span id="custom" />

## Assign custom permissions to account users

To assign custom permissions rather than standard roles, click **Customize permissions** in the **Roles** section when adding or editing the user account. 

To enable a permission for the user, toggle the box to the appropriate setting. 

![Guide to access settings](images/permission_key.png)

- **No access**: The user will not have the indicated permission.
- **Read only**: The user will have access to view features related to the indicated area, but will not be able to make changes. 
- **Read/write**: The user will have access to make changes associated with the area, as well as viewing it.
- **Mixed**: You can’t select this option directly, but the **Mixed** indicator will show if you have allowed a combination of access for that permission. For example, if you grant **Read only** access to **Pricing and availability** for **All products**, but then grant **Read/write** access to **Pricing and availability** for one specific product, the **Pricing and availability** indicator for **All products** will show as Mixed. The same applies if some products have **No access** for a permission, but others have **Read/write** and/or **Read only** access.

For some permissions, such as those related to viewing analytic data, only **Read only** access can be granted. Note that in the current implementation, some permissions have no distinction between **Read only** and **Read/write** access. Review the details for each permission to understand the specific capabilities granted by **Read only** and/or **Read/write** access.

The specific details about each permission are described in the tables below.

## Account-level permissions

The permissions in this section cannot be limited to specific products. Granting access to one of these permissions allows the user to have that permission for the entire account.

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
<tr><td align="left">    <b>Account settings</b>                    </td><td align="left">  Can view all pages in the <b>Account settings</b> section, including <a href="managing-your-profile.md">contact info</a>.       </td><td align="left">  Can view all pages in the <b>Account settings</b> section. Can make changes to <a href="/windows/uwp/publish/manage-account-settings-and-profile">contact info</a> and other pages, but can’t make changes to the payout account or tax profile (unless that permission is granted separately).            </td></tr>
<tr><td align="left">    <b>Account users</b>                       </td><td align="left">  Can view users that have been added to the account in the <b>Users</b> section.          </td><td align="left">  Can add users to the account and make changes to existing users in the <b>Users</b> section.             </td></tr>
<tr><td align="left">    <b>Account-level ad performance report</b> </td><td align="left">  Can view the account-level <a href="advertising-performance-report.md">Advertising performance report</a>.      </td><td align="left">  N/A   </td></tr>
<tr><td align="left">    <b>Ad campaigns</b>                        </td><td align="left">  Can view <a href="create-an-ad-campaign-for-your-app.md">ad campaigns</a> created in the account.      </td><td align="left">  Can create, manage, and view <a href="create-an-ad-campaign-for-your-app.md">ad campaigns</a> created in the account.          </td></tr>
<tr><td align="left">    <b>Ad mediation</b>                        </td><td align="left">  Can view ad mediation configurations for all products in the account.    </td><td align="left">  Can view and change ad mediation configurations for all products in the account.        </td></tr>
<tr><td align="left">    <b>Ad mediation reports</b>                </td><td align="left">  Can view the <a href="/windows/uwp/publish/advertising-performance-report">Ad mediation report</a> for all products in the account.    </td><td align="left">  N/A    </td></tr>
<tr><td align="left">    <b>Ad performance reports</b>              </td><td align="left">  Can view <a href="advertising-performance-report.md">Advertising performance reports</a> for all products in the account.       </td><td align="left">  N/A         </td></tr>
<tr><td align="left">    <b>Ad units</b>                            </td><td align="left">  Can view the <a href="in-app-ads.md">ad units</a> that have been created for the account.    </td><td align="left">  Can create, manage, and view <a href="in-app-ads.md">ad units</a> for the account.             </td></tr>
<tr><td align="left">    <b>Affiliate ads</b>                       </td><td align="left">  Can view <a href="/windows/uwp/publish/in-app-ads">affiliate ad</a> usage in all products in the account.    </td><td align="left">  Can manage and view <a href="/windows/uwp/publish/in-app-ads">affiliate ad</a> usage for all products in the account.                </td></tr>
<tr><td align="left">    <b>Affiliates performance reports</b>      </td><td align="left">  Can view the <a href="/windows/uwp/publish/advertising-performance-report">Affiliates performance report</a> for all products in the account.   </td><td align="left">  N/A   </td></tr>
<tr><td align="left">    <b>App install ads reports</b>             </td><td align="left">  Can view the <a href="promote-your-app-report.md">Ad campaign report</a>.           </td><td align="left">  N/A   </td></tr>
<tr><td align="left">    <b>Community ads</b>                       </td><td align="left">  Can view free <a href="about-community-ads.md">community ad</a> usage for all products in the account.          </td><td align="left">  Can create, manage, and view free <a href="about-community-ads.md">community ad</a> usage for all products in the account.               </td></tr>
<tr><td align="left">    <b>Contact info</b>                        </td><td align="left">  Can view <a href="/windows/uwp/publish/manage-account-settings-and-profile">contact info</a> in the Account settings section.        </td><td align="left">  Can edit and view <a href="/windows/uwp/publish/manage-account-settings-and-profile">contact info</a> in the Account settings section.            </td></tr>
<tr><td align="left">    <b>COPPA compliance</b>                    </td><td align="left">  Can view <a href="in-app-ads.md#coppa-compliance">COPPA compliance</a> selections (indicating whether products are targeted at children under the age of 13) for all products in the account.                                            </td><td align="left">  Can edit and view <a href="in-app-ads.md#coppa-compliance">COPPA compliance</a>  selections (indicating whether products are targeted at children under the age of 13) for all products in the account.         </td></tr>
<tr><td align="left">    <b>Customer groups</b>                     </td><td align="left">  Can view <a href="create-customer-groups.md">customer groups</a> (segments and known user groups).      </td><td align="left">  Can create, edit, and view <a href="create-customer-groups.md">customer groups</a> (segments and known user groups).       </td></tr>
<tr><td align="left">    <b>Manage product groups</b>&nbsp;*                            </td><td align="left">  Can view the new product group creation page, but can’t actually create new product groups.    </td><td align="left">  Can create and edit product groups.     </td></tr>
<tr><td align="left">    <b>New apps</b>                            </td><td align="left">  Can view the new app creation page, but can’t actually create new apps in the account.    </td><td align="left">  Can <a href="create-your-app-by-reserving-a-name.md">create new apps</a> in the account by reserving new app names, and can create submissions and submit apps to the Store.     </td></tr>
<tr><td align="left">    <b>New bundles</b>&nbsp;*                       </td><td align="left">  Can view the new bundle creation page, but can’t actually create new bundles in the account.     </td><td align="left">  Can create new bundles of products.          </td></tr>
<tr><td align="left">    <b>Partner services</b>&nbsp;*                  </td><td align="left">  Can view certificates for installing to services to retrieve XTokens.     </td><td align="left">  Can manage and view certificates for installing to services to retrieve XTokens.       </td></tr>
<tr><td align="left">    <b>Payout account</b>                      </td><td align="left">  Can view <a href="setting-up-your-payout-account-and-tax-forms.md#payout-account">payout account info</a> in <b>Account settings</b>.     </td><td align="left">  Can edit and view <a href="setting-up-your-payout-account-and-tax-forms.md#payout-account">payout account info</a> in <b>Account settings</b>.       </td></tr>
<tr><td align="left">    <b>Payout summary</b>                      </td><td align="left">  Can view the <a href="payout-summary.md">Payout summary</a> to access and download payout reporting info.       </td><td align="left">  Can view the <a href="payout-summary.md">Payout summary</a> to access and download payout reporting info.   </td></tr>
<tr><td align="left">    <b>Relying parties</b>&nbsp;*                   </td><td align="left">  Can view relying parties to retrieve XTokens.    </td><td align="left">  Can manage and view relying parties to retrieve XTokens.     </td></tr>
<tr><td align="left">    <b>Sandboxes</b>&nbsp;*                         </td><td align="left">  Can access the <b>Sandboxes</b> page and view sandboxes in the account and any applicable configurations for those sandboxes. Can’t view the products and submissions for each sandbox unless the appropriate product-level permissions are granted. </td><td align="left">  Can access the <b>Sandboxes</b> page and view and manage the sandboxes in the account, including creating and deleting sandboxes and managing their configurations. Can’t view the products and submissions for each sandbox unless the appropriate product-level permissions are granted.    </td></tr>
<tr><td align="left">    <b>Store sale events</b>&nbsp;*                            </td><td align="left">  N/A    </td><td align="left">  Can configure the option to automatically include products in Store sale events.     </td></tr>
<tr><td align="left">    <b>Tax profile</b>                         </td><td align="left">  Can view <a href="setting-up-your-payout-account-and-tax-forms.md#tax-forms">tax profile info and forms</a> in <b>Account settings</b>.     </td><td align="left">  Can fill out tax forms and update <a href="setting-up-your-payout-account-and-tax-forms.md#tax-forms">tax profile info</a> in <b>Account settings</b>.     </td></tr>
<tr><td align="left">    <b>Test accounts</b>&nbsp;*                     </td><td align="left">  Can view accounts for testing Xbox Live configuration.      </td><td align="left">  Can create, manage, and view accounts for testing Xbox Live configuration.      </td></tr>
<tr><td align="left">    <b>Xbox devices</b>                        </td><td align="left">  Can view the Xbox development consoles enabled for the account in the <b>Account settings</b> section.       </td><td align="left">  Can add, remove, and view the Xbox development consoles enabled for the account in the <b>Account settings</b> section.     </td></tr>
    </tbody>
    </table>

\* Permissions marked with an asterisk (*) grant access to features which are not available to all accounts. If your account has not been enabled for these features, your selections for these permissions will not have any effect.   


## Product-level permissions

The permissions in this section can be granted to all products in the account, or can be customized to allow the permission only for one or more specific products. 

Product-level permissions are grouped into four categories: **Analytics**, **Monetization**, **Publishing**, and **Xbox Live**. You can expand each of these categories to view the individual permissions in each category. You also have the option to enable **All permissions** for one or more specific products.

To grant a permission for every product in the account, make your selections for that permission (by toggling the box to indicate **Read only** or **Read/write**) in the row marked **All products**. 
 
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
    <tr><td align="left">    <b>Acquisitions</b> (including Near Real Time data) </td><td>    Can view the <a href="acquisitions-report.md">Acquisitions</a> and <a href="add-on-acquisitions-report.md">Add-on acquisitions</a> reports for the product.        </td><td>    N/A    </td><td>    N/A (settings for parent product include the **Add-on acquisitions** report)        </td><td>    N/A                         </td></tr>
    <tr><td align="left">    <b>Usage</b> </td><td>    Can view the <a href="usage-report.md">Usage report</a> for the product.     </td><td>    N/A       </td><td>    N/A     </td><td>    N/A         </td></tr>
    <tr><td align="left">    <b>Health</b> (including Near Real Time data) </td><td>    Can view the <a href="health-report.md">Health report</a> for the product.    </td><td>    N/A     </td><td>    N/A     </td><td>    N/A         </td></tr>
    <tr><td align="left">    <b>Customer feedback</b>    </td><td>    Can view the <a href="reviews-report.md">Reviews</a> and <a href="feedback-report.md">Feedback</a> reports for the product.       </td><td>    N/A (to respond to feedback or reviews, the <b>Contact customer</b> permission must be granted)   </td><td>    N/A     </td><td>    N/A         </td></tr>
    <tr><td align="left">    <b>Xbox analytics</b> </td><td>    Can view the <a href="xbox-analytics-report.md">Xbox analytics report</a> for the product.    </td><td>    N/A   </td><td>    N/A       </td><td>    N/A          </td></tr>
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
    <tr><td align="left">    <b>Promotional codes</b>     </td><td>    Can view <a href="generate-promotional-codes.md">promotional code</a> orders and usage info for the product and its add-ons, and can view usage info.         </td><td>    Can view, manage, and create <a href="generate-promotional-codes.md">promotional code</a> orders for the product and its add-ons, and can view usage info.          </td><td>    N/A (settings for parent product apply to all add-ons)     </td><td>    N/A (settings for parent product apply to all add-ons)     </td></tr>
    <tr><td align="left">    <b>Targeted offers</b>     </td><td>    Can view <a href="use-targeted-offers-to-maximize-engagement-and-conversions.md">targeted offers</a> for the product.         </td><td>    Can view, manage and create <a href="use-targeted-offers-to-maximize-engagement-and-conversions.md">targeted offers</a> for the product.          </td><td>    N/A     </td><td>    N/A      </td></tr>
    <tr><td align="left">    <b>Contact customer</b>  </td><td>    Can view <a href="respond-to-customer-feedback.md">responses to customer feedback</a> and <a href="respond-to-customer-reviews.md">responses to customer reviews</a>, as long as the <b>Customer feedback</b> permission has been granted as well. Can also view <a href="send-push-notifications-to-your-apps-customers.md">targeted notifications</a> that have been created for the product.    </td><td>    Can <a href="respond-to-customer-feedback.md">respond to customer feedback</a> and <a href="respond-to-customer-reviews.md">respond to customer reviews</a>, as long as the <b>Customer feedback</b> permission has been granted as well. Can also <a href="send-push-notifications-to-your-apps-customers.md">create and send targeted notifications</a> for the product.                   </td><td>    N/A         </td><td>    N/A                          </td></tr>
    <tr><td align="left">    <b>Experimentation</b></td><td>    Can view <a href="../monetize/run-app-experiments-with-a-b-testing.md">experiments (A/B testing)</a> and view experimentation data for the product.   </td><td>    Can create, manage, and view <a href="../monetize/run-app-experiments-with-a-b-testing.md">experiments (A/B testing)</a> for the product, and view experimentation data.     </td><td>    N/A  </td><td>    N/A                 </td></tr>
    <tr><td align="left">    <b>Store sale events</b>&nbsp;*</td><td>    Can view sale event status for the product.   </td><td>    Can add the product to sale events and configure discounts.      </td><td>    Can view sale event status for the product.   </td><td>    Can add the product to sale events and configure discounts.      </td></tr>
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
    <tr><td align="left">    <b>Product Setup</b>  </td><td>    Can view the product setup page of products.     </td><td>    Can view and edit the product setup page of products. </td><td>    Can view the product setup page of add-ons.   </td><td>    Can view and edit the product setup page add-ons.          </td></tr>
    <tr><td align="left">    <b>Pricing and availability</b>  </td><td>    Can view the <a href="set-app-pricing-and-availability.md">Pricing and availability</a> page of products.     </td><td>    Can view and edit the <a href="set-app-pricing-and-availability.md">Pricing and availability</a> page of products. </td><td>    Can view the <a href="set-add-on-pricing-and-availability.md">Pricing and availability</a> page of add-ons.   </td><td>    Can view and edit the <a href="set-add-on-pricing-and-availability.md">Pricing and availability</a> page of add-ons.          </td></tr>
    <tr><td align="left">    <b>Properties</b>   </td><td>    Can view the <a href="enter-app-properties.md">Properties</a> page of products.      </td><td>    Can view and edit the <a href="enter-app-properties.md">Properties</a> page of products.       </td><td>    Can view the <a href="enter-add-on-properties.md">Properties</a> page of add-ons.     </td><td>    Can view and edit the <a href="enter-add-on-properties.md">Properties</a> page of add-ons.               </td></tr>
    <tr><td align="left">    <b>Age ratings</b>    </td><td>    Can view the <a href="age-ratings.md">Age ratings</a> page of products.       </td><td>    Can view and edit the <a href="age-ratings.md">Age ratings</a> page of products.    </td><td>    Can view the Age ratings page of add-ons.          </td><td>     Can view and edit the Age ratings page of add-ons.       </td></tr>
    <tr><td align="left">    <b>Packages</b>        </td><td>    Can view the <a href="upload-app-packages.md">Packages</a> page of products.  </td><td>    Can view and edit the <a href="upload-app-packages.md">Packages</a> page of products, including uploading packages.     </td><td>   Can view the <a href="upload-app-packages.md">Packages</a> page of addons (if applicable).   </td><td>     Can view and edit <a href="upload-app-packages.md">Packages</a> page of addons (if applicable).             </td></tr>
    <tr><td align="left">    <b>Store listings</b>  </td><td>    Can view the <a href="create-app-store-listings.md">Store listing page(s)</a> of products.  </td><td>    Can view and edit the <a href="create-app-store-listings.md">Store listing page(s)</a> of products, and can add new Store listings for different languages.     </td><td>    Can view the <a href="create-add-on-store-listings.md">Store listing page(s)</a> of add-ons.            </td><td>    Can view and edit the <a href="create-add-on-store-listings.md">Store listing page(s)</a> of add-ons, and can add Store listings for different languages.                 </td></tr>
    <tr><td align="left">    <b>Store submission</b>     </td><td>    No access is granted if this permission is set to read-only.           </td><td>    Can submit the product to the Store and view certification reports. Includes both new and updated submissions. </td><td>No access is granted if this permission is set to read-only.     </td><td>    Can submit the add-on to the Store and view certification reports. Includes both new and updated submissions.</td></tr>
    <tr><td align="left">    <b>New submission creation</b>       </td><td>    No access is granted if this permission is set to read-only.        </td><td>    Can create new <a href="app-submissions.md">submissions</a> for the product.  </td><td>    No access is granted if this permission is set to read-only.   </td><td>    Can create new <a href="add-on-submissions.md">submissions</a> for the add-on.        </td></tr>
    <tr><td align="left">    <b>New add-ons</b>    </td><td>    No access is granted if this permission is set to read-only. </td><td>    Can <a href="set-your-add-on-product-id.md">create new add-ons</a> for the product. </td><td>    N/A    </td><td>    N/A        </td></tr>
    <tr><td align="left">    <b>Name reservations</b>   </td><td>    Can view the <a href="manage-app-names.md">Manage app names</a> page for the product.</td><td>    Can view and edit the <a href="manage-app-names.md">Manage app names</a> page for the product, including reserving additional names and deleting reserved names. </td><td>   Can view reserved names for the add-on.    </td><td>   Can view and edit reserved names for the add-on.          </td></tr>
    <tr><td align="left">    <b>Disc request</b>   </td><td>    Can view disc the request page. </td><td>    Can create disc requests. </td><td>   N/A    </td><td>   N/A          </td></tr>
    <tr><td align="left">    <b>Disc royalties </b>   </td><td>    Can view disc the royalties page.</td><td>    Can create disc royalties. </td><td>   N/A    </td><td>   N/A          </td></tr>
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
    <tr><td align="left">    <b>Relying Parties</b>&nbsp;*</td><td>    Can view the Relying parties page of an account.   </td><td>    Can view and edit the Relying parties page of an account.    </td><td>    N/A    </td><td>    N/A                      </td></tr>
    <tr><td align="left">    <b>Partner Services</b>&nbsp;*</td><td>    Can view the Web services page of an account.  </td><td>    Can view and edit the Web services page of an account.	    </td><td>    N/A    </td><td>    N/A                      </td></tr>
    <tr><td align="left">    <b>Xbox Test Accounts</b>&nbsp;*</td><td>    Can view the Xbox Test Accounts page of an account.  </td><td>    Can view and edit the Xbox Test Accounts page of an account.    </td><td>    N/A    </td><td>    N/A                      </td></tr>
    <tr><td align="left">    <b>Xbox Test Accounts per Sandbox</b>&nbsp;*</td><td>    Can view the Xbox Test Accounts page for only the specified sandboxes of an account.  </td><td>    Can view and edit the Xbox Test.   <tr><td align="left">    <b>Accounts page for only the specified sandboxes of an account    </td><td>    N/A    </td><td>    N/A                      </td></tr>
    <tr><td align="left">    <b>Xbox Devices</b>&nbsp;*</td><td>    Can view the Xbox one development consoles page of an account.  </td><td>    Can view and edit the Xbox one development consoles page of an account.    </td><td>    N/A    </td><td>    N/A                      </td></tr>
    <tr><td align="left">    <b>Xbox Devices per Sandbox</b>&nbsp;*</td><td>    Can view the Xbox one development consoles page for only the specified sandboxes of an account.  </td><td>    Can view and edit the Xbox one development consoles page for only the specified sandboxes of an account.    </td><td>    N/A    </td><td>    N/A                      </td></tr>
    <tr><td align="left">    <b>App Channels</b>&nbsp;*</td><td>    N/A  </td><td>    Can publish promotional video channels to the Xbox console for viewing through OneGuide.    </td><td>    N/A    </td><td>    N/A                      </td></tr>
    <tr><td align="left">    <b>Service Configuration</b>&nbsp;*</td><td>    Can view the Xbox Live Service configuration page of a product.  </td><td>    Can view and edit the Xbox Live Service configuration page of a product.    </td><td>    N/A    </td><td>    N/A                      </td></tr>
    <tr><td align="left">    <b>Tools Access</b>&nbsp;*</td><td>    Can run Xbox Live tools on a product to only view data.  </td><td>    Can run Xbox Live tools on a product to view and edit data.    </td><td>    N/A    </td><td>    N/A                      </td></tr>
</tbody>
</table>

\* Permissions marked with an asterisk (*) grant access to features which are not available to all accounts. If your account has not been enabled for these features, your selections for these permissions will not have any effect.