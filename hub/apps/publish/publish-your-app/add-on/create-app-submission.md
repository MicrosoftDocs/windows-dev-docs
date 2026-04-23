---
title: Create an app submission for your add-on
description: Create an app submission to tell the Microsoft Store about your app add-on
ms.topic: article
ms.date: 10/30/2022
---

# Create add-ons

An add-on must be associated with an app that you've created in [Partner Center](https://partner.microsoft.com/dashboard) (even if you haven't submitted it yet). You can find the button to **Create a new add-on** on your app's **Overview** page or on its **add-ons** page.

Follow these steps to publish your app add-on to the Microsoft Store:

| Topic                                                                   | Description                                                                                                                                  |
| ----------------------------------------------------------------------- | -------------------------------------------------------------------------------------------------------------------------------------------- |
| [Create a new add-on page](#create-a-new-add-on-page)  | Add-on submissions contain all of the information needed to distribute your add-on in the Microsoft Store.                                   |
| [Set your add-on's price and availability](./price-and-availability.md) | Specify how, when, and where your add-on will be available to customers, your add-on's pricing model, and whether you'll offer a free trial. |
| [Set your add-on's age ratings](#age-ratings)           | Age ratings only apply for Durable product type.              |
| [Specify your add-on's properties](./enter-app-properties.md)           | Add-on properties describe important details about your app including requirements, capabilities, and your contact information.              |
| [Create your add-on's store listing](./create-app-store-listing.md)     | Create your add-on's page in the Microsoft Store.                                                                                            |
| [Submission options](./manage-submission-options.md)     | Set the submission options for your add-on.                                                                                            |

### Create a new add-on page

After you select **Create a new add-on**, you'll be prompted to specify a product type and assign a product ID for your add-on.

| Field name                        | Notes    |    
| --------------------------------- | -------- |
| [**Product type**](#product-type) | Required |
| [**Product ID**](#product-id)     | Required |

#### Product type

First, you'll need to indicate which type of add-on you are offering. This selection refers to how the customer can use your add-on.

> [!TIP]
> For detailed information about them, see the [Product type](./what-are-add-ons.md#product-type) section.

> [!NOTE]
> You won't be able to change the product type after you save this page to create the add-on. If you choose the wrong product type, you can always delete your in-progress add-on submission and start over by creating a new add-on.

#### Product ID

Regardless of the product type you choose, you will need to enter a unique product ID for your add-on. This name will be used to identify your add-on in Partner Center, and you can use this identifier to [refer to the add-on in your code](/windows/uwp/monetize/in-app-purchases-and-trials#how-to-use-product-ids-for-add-ons-in-your-code).

Here are a few things to keep in mind when choosing a product ID:

- A product ID must be unique within the parent product.
- You can’t change or delete an add-on’s product ID after it's been published.
- A product ID can't be more than 100 characters in length.
- A product ID cannot include any of the following characters: **&lt; &gt; \* % & : \\ ? + ,**
- Customers won't see the product ID. (Later, you can enter a [title and description](./create-app-store-listing.md) to be displayed to customers.)
- If your previously published app supports Windows Phone 8.1 or earlier, you must only use alphanumeric characters, periods, or underscores in your product ID. If you use any other types of characters, the add-on will not be available for purchase to customers running Windows Phone 8.1 or earlier.

### Pricing and availability page

| Field name       | Notes                                                                   |
| ---------------- | ----------------------------------------------------------------------- |
| **Markets**      | Default: All possible markets                                           |
| **Visibility**   | Default: Available for purchase. May be displayed in your app's listing |
| **Schedule**     | Default: Release as soon as possible                                    |
| **Pricing**      | Required                                                                |
| **Sale pricing** | Optional                                                                |

> [!TIP]
> For detailed information about the **Pricing and availability** fields, see the [Set app pricing and availability for add-on](./price-and-availability.md) section.

### Properties page

| Field name                | Notes                                                                                                |
| ------------------------- | ---------------------------------------------------------------------------------------------------- |
| **Product lifetime**      | Required if the product type is **Durable**. Not applicable to other product types.                  |
| **Quantity**              | Required if the product type is **Store-managed consumable**. Not applicable to other product types. |
| **Subscription period**   | Required if the product type is **Subscription**. Not applicable to other product types.             |
| **Free trial**            | Required if the product type is **Subscription**. Not applicable to other product types.             |
| **Content type**          | Required                                                                                             |
| **Keywords**              | Optional (up to 10 keywords, 30 character limit each)                                                |
| **Custom developer data** | Optional (3000 character limit)                                                                      |

> [!TIP]
> For detailed information about the **Properties** fields, see the [Enter add-on properties](./enter-app-properties.md) section.

### Age ratings

To publish a product in the Microsoft Store, you must provide accurate answers to the age ratings questions and receive age ratings.

:::image type="content" source="../msix/images/add-on-age-ratings.png" lightbox="../msix/images/add-on-age-ratings.png" alt-text="A screenshot showing questions for receiving age ratings.":::

> [!NOTE]
> You only need to provide age ratings information if your add-on has a **Durable** product type.

### Store listings

At least one Store listing is required. We recommend that you provide Store listings for every language your app supports.

| Field name      | Notes                           |
| --------------- | ------------------------------- |
| **Product name**| Required (100 character limit)  |
| **Description** | Required (200 character limit)  |
| **Icon**        | Optional (.png, 300x300 pixels) |

When you've finished entering this info, click **Submit to the Store**. In most cases, the certification process takes about an hour. After that, your add-on will be published to the Store and ready for customers to purchase.

> [!TIP]
> For detailed information about the **Store listings** fields, see the [Create add-on store listings](./create-app-store-listing.md) section.

> [!NOTE]
> The add-on must also be implemented in your app's code. For more info, see [In-app purchases and trials](/windows/uwp/monetize/in-app-purchases-and-trials).
