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
    <tr><td align="left">    <b>Acquisitions</b> (including Near Real Time data) </td><td>    Can view the <a href="..\acquisitions-report.md">Acquisitions</a> and <a href="..\acquisitions-report.md">Add-on acquisitions</a> reports for the product.        </td><td>    N/A    </td><td>    N/A (settings for parent product include the **Add-on acquisitions** report)        </td><td>    N/A                         </td></tr>
    <tr><td align="left">    <b>Usage</b> </td><td>    Can view the <a href="..\usage-report.md">Usage report</a> for the product.     </td><td>    N/A       </td><td>    N/A     </td><td>    N/A         </td></tr>
    <tr><td align="left">    <b>Health</b> (including Near Real Time data) </td><td>    Can view the <a href="..\health-report.md">Health report</a> for the product.    </td><td>    N/A     </td><td>    N/A     </td><td>    N/A         </td></tr>
    <tr><td align="left">    <b>Customer feedback</b>    </td><td>    Can view the <a href="..\reviews-report.md">Reviews</a> and <a href="..\feedback-report.md">Feedback</a> reports for the product.       </td><td>    N/A (to respond to feedback or reviews, the <b>Contact customer</b> permission must be granted)   </td><td>    N/A     </td><td>    N/A         </td></tr>
    <tr><td align="left">    <b>Xbox analytics</b> </td><td>    Can view the <a href="..\xbox-analytics-report.md">Xbox analytics report</a> for the product.    </td><td>    N/A   </td><td>    N/A       </td><td>    N/A          </td></tr>
    </tbody>
    </table>

## Monetization

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
    <tr><td align="left">    <b>Promotional codes</b>     </td><td>    Can view <a href="..\generate-promotional-codes.md">promotional code</a> orders and usage info for the product and its add-ons, and can view usage info.         </td><td>    Can view, manage, and create <a href="..\generate-promotional-codes.md">promotional code</a> orders for the product and its add-ons, and can view usage info.          </td><td>    N/A (settings for parent product apply to all add-ons)     </td><td>    N/A (settings for parent product apply to all add-ons)     </td></tr>
    <tr><td align="left">    <b>Targeted offers</b>     </td><td>    Can view <a href="..\use-targeted-offers-to-maximize-engagement-and-conversions.md">targeted offers</a> for the product.         </td><td>    Can view, manage and create <a href="..\use-targeted-offers-to-maximize-engagement-and-conversions.md">targeted offers</a> for the product.          </td><td>    N/A     </td><td>    N/A      </td></tr>
    <tr><td align="left">    <b>Contact customer</b>  </td><td>    Can view <a href="..\respond-to-customer-feedback.md">responses to customer feedback</a> and <a href="..\respond-to-customer-reviews.md">responses to customer reviews</a>, as long as the <b>Customer feedback</b> permission has been granted as well. Can also view <a href="..\send-push-notifications-to-your-apps-customers.md">targeted notifications</a> that have been created for the product.    </td><td>    Can <a href="..\respond-to-customer-feedback.md">respond to customer feedback</a> and <a href="..\respond-to-customer-reviews.md">respond to customer reviews</a>, as long as the <b>Customer feedback</b> permission has been granted as well. Can also <a href="..\send-push-notifications-to-your-apps-customers.md">create and send targeted notifications</a> for the product.                   </td><td>    N/A         </td><td>    N/A                          </td></tr>
    <tr><td align="left">    <b>Experimentation</b></td><td>    Can view <a href="\windows\uwp\monetize\run-app-experiments-with-a-b-testing">experiments (A/B testing)</a> and view experimentation data for the product.   </td><td>    Can create, manage, and view <a href="\windows\uwp\monetize\run-app-experiments-with-a-b-testing">experiments (A/B testing)</a> for the product, and view experimentation data.     </td><td>    N/A  </td><td>    N/A                 </td></tr>
    <tr><td align="left">    <b>Store sale events</b>&nbsp;*</td><td>    Can view sale event status for the product.   </td><td>    Can add the product to sale events and configure discounts.      </td><td>    Can view sale event status for the product.   </td><td>    Can add the product to sale events and configure discounts.      </td></tr>
    </tbody>
    </table>

## Publishing 

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
    <tr><td align="left">    <b>Pricing and availability</b>  </td><td>    Can view the <a href="../publish-your-app/price-and-availability.md">Pricing and availability</a> page of products.     </td><td>    Can view and edit the <a href="../publish-your-app/price-and-availability.md">Pricing and availability</a> page of products. </td><td>    Can view the <a href="../publish-your-app/price-and-availability.md">Pricing and availability</a> page of add-ons.   </td><td>    Can view and edit the <a href="../publish-your-app/price-and-availability.md">Pricing and availability</a> page of add-ons.          </td></tr>
    <tr><td align="left">    <b>Properties</b>   </td><td>    Can view the <a href="../publish-your-app/enter-app-properties.md">Properties</a> page of products.      </td><td>    Can view and edit the <a href="../publish-your-app/enter-app-properties.md">Properties</a> page of products.       </td><td>    Can view the <a href="../publish-your-app/enter-app-properties.md">Properties</a> page of add-ons.     </td><td>    Can view and edit the <a href="../publish-your-app/enter-app-properties.md">Properties</a> page of add-ons.               </td></tr>
    <tr><td align="left">    <b>Age ratings</b>    </td><td>    Can view the <a href="../publish-your-app/age-ratings.md">Age ratings</a> page of products.       </td><td>    Can view and edit the <a href="../publish-your-app/age-ratings.md">Age ratings</a> page of products.    </td><td>    Can view the Age ratings page of add-ons.          </td><td>     Can view and edit the Age ratings page of add-ons.       </td></tr>
    <tr><td align="left">    <b>Packages</b>        </td><td>    Can view the <a href="../publish-your-app/upload-app-packages.md">Packages</a> page of products.  </td><td>    Can view and edit the <a href="../publish-your-app/upload-app-packages.md">Packages</a> page of products, including uploading packages.     </td><td>   Can view the <a href="../publish-your-app/upload-app-packages.md">Packages</a> page of addons (if applicable).   </td><td>     Can view and edit <a href="../publish-your-app/upload-app-packages.md">Packages</a> page of addons (if applicable).             </td></tr>
    <tr><td align="left">    <b>Store listings</b>  </td><td>    Can view the <a href="../publish-your-app/create-app-store-listing.md">Store listing page(s)</a> of products.  </td><td>    Can view and edit the <a href="../publish-your-app/create-app-store-listing.md">Store listing page(s)</a> of products, and can add new Store listings for different languages.     </td><td>    Can view the <a href="../publish-your-app/create-app-store-listing.md">Store listing page(s)</a> of add-ons.            </td><td>    Can view and edit the <a href="../publish-your-app/create-app-store-listing.md">Store listing page(s)</a> of add-ons, and can add Store listings for different languages.                 </td></tr>
    <tr><td align="left">    <b>Store submission</b>     </td><td>    No access is granted if this permission is set to read-only.           </td><td>    Can submit the product to the Store and view certification reports. Includes both new and updated submissions. </td><td>No access is granted if this permission is set to read-only.     </td><td>    Can submit the add-on to the Store and view certification reports. Includes both new and updated submissions.</td></tr>
    <tr><td align="left">    <b>New submission creation</b>       </td><td>    No access is granted if this permission is set to read-only.        </td><td>    Can create new <a href="../publish-your-app/create-app-submission.md">submissions</a> for the product.  </td><td>    No access is granted if this permission is set to read-only.   </td><td>    Can create new <a href="../publish-your-app/create-app-submission.md">submissions</a> for the add-on.        </td></tr>
    <tr><td align="left">    <b>New add-ons</b>    </td><td>    No access is granted if this permission is set to read-only. </td><td>    Can <a href="../publish-your-app/overview.md">create new add-ons</a> for the product. </td><td>    N/A    </td><td>    N/A        </td></tr>
    <tr><td align="left">    <b>Name reservations</b>   </td><td>    Can view the <a href="manage-app-name-reservations.md">Manage app names</a> page for the product.</td><td>    Can view and edit the <a href="manage-app-name-reservations.md">Manage app names</a> page for the product, including reserving additional names and deleting reserved names. </td><td>   Can view reserved names for the add-on.    </td><td>   Can view and edit reserved names for the add-on.          </td></tr>
    <tr><td align="left">    <b>Disc request</b>   </td><td>    Can view disc the request page. </td><td>    Can create disc requests. </td><td>   N/A    </td><td>   N/A          </td></tr>
    <tr><td align="left">    <b>Disc royalties </b>   </td><td>    Can view disc the royalties page.</td><td>    Can create disc royalties. </td><td>   N/A    </td><td>   N/A          </td></tr>
    </tbody>
    </table>

## Xbox Live \*

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
    <tr><td align="left">    <b>Proprietary access</b>&nbsp;*</td><td>Can view support inquires.</td><td>Can view and edit support inquires.</td><td>    N/A    </td><td>    N/A                      </td></tr>
</tbody>
</table>

\* Permissions marked with an asterisk (*) grant access to features which are not available to all accounts. If your account has not been enabled for these features, your selections for these permissions will not have any effect.
