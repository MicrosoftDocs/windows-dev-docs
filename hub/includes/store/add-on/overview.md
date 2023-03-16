Follow these steps to publish your app add-on to the Microsoft Store:

| Topic                                                                                                        | Description          |
|--------------------------------------------------------------------------------------------------------------|----------------------|
| [Create an app submission for your add-on](../../../apps/publish/publish-your-app/create-app-submission.md)  | Add-on submissions contain all of the information needed to distribute your add-on in the Microsoft Store.  |
| [Set your add-on's price and availability](../../../apps/publish/publish-your-app/price-and-availability.md) | Specify how, when, and where your add-on will be available to customers, your add-on's pricing model, and whether you'll offer a free trial. |
| [Specify your add-on's properties](../../../apps/publish/publish-your-app/enter-app-properties.md)           | Add-on properties describe important details about your app including requirements, capabilities, and your contact information. |
| [Create your add-on's store listing](../../../apps/publish/publish-your-app/create-app-store-listing.md)     | Create your add-on's page in the Microsoft Store. |

## Checklist for submitting an add-on

Here's a list of the info that you provide when creating your add-on submission. The items that you are required to provide are noted below. Some of these are optional, or have default values already provided that you can change as desired.

### Create a new add-on page

| Field name                                                     | Notes    |
|----------------------------------------------------------------|----------|
| **Product type** | Required |  
| **Product ID**   | Required |

### Properties page

| Field name                | Notes                           |
|---------------------------|---------------------------------|
| **Product lifetime**      | Required if the product type is **Durable**. Not applicable to other product types. |
| **Quantity**              | Required if the product type is **Store-managed consumable**. Not applicable to other product types. |
| **Subscription period**   | Required if the product type is **Subscription**. Not applicable to other product types. |  
| **Free trial**            | Required if the product type is **Subscription**. Not applicable to other product types. |
| **Content type**          | Required                        |
| **Keywords**              | Optional (up to 10 keywords, 30 character limit each) |
| **Custom developer data** | Optional (3000 character limit) |

### Pricing and availability page

| Field name        | Notes                         |
|-------------------|-------------------------------|
| **Markets**      | Default: All possible markets |
| **Visibility**   | Default: Available for purchase. May be displayed in your app's listing |
| **Schedule**     | Default: Release as soon as possible |
| **Pricing**      | Required                      |
| **Sale pricing** | Optional                      |

### Store listings

One Store listing required. We recommend providing Store listings for every language your app supports.

| Field name      | Notes                           |
|-----------------|---------------------------------|
| **Title**       | Required (100 character limit)  |
| **Description** | Optional (200 character limit)  |
| **Icon**        | Optional (.png, 300x300 pixels) |

When you've finished entering this info, click **Submit to the Store**. In most cases, the certification process takes about an hour. After that, your add-on will be published to the Store and ready for customers to purchase.

> [!NOTE]
> The add-on must also be implemented in your app's code. For more info, see [In-app purchases and trials](/windows/uwp/monetize/in-app-purchases-and-trials).

## Updating an add-on after publication

You can make changes to a published add-on at any time. Add-on changes are submitted and published independently of your app, so you generally don't need to update the entire app in order to make changes to an add-on such as updating its price or description.

To submit updates, go to the add-on's page in Partner Center and click **Update**. This will create a new submission for the add-on, using the info from your previous submission as a starting point. Make the changes you'd like, and then click **Submit to the Store**.

If you'd like to remove an add-on you've previously offered, you can do this by creating a new submission and changing the [Distribution and visibility](../../../apps/publish/publish-your-app/price-and-availability.md) option to **Hidden in the Store** with the **Stop acquisition** option. Be sure to update your app's code as needed to also remove references to the add-on (especially if your previously-published app supports Windows 8.1 earlier; this visibility setting won't apply to those customers).

> [!IMPORTANT]
> If your previously-published app is available to customers on Windows 8.x, you will need to create and publish a new app submission in order to make the add-on updates visible to those customers. Similarly, if you add new add-ons to an app targeting Windows 8.x after the app has been published, you'll need to update your app's code to reference those add-ons, then resubmit the app. Otherwise, the new add-ons won't be visible to customers on Windows 8.x.
