The **Pricing** section of the [Pricing and availability](../../../apps/publish/publish-your-app/price-and-availability.md) page lets you select the base price for an app. You can also [schedule price changes](#schedule-price-changes) to indicate the date and time at which your app’s price should change. Additionally, you have the option to [override the base price for specific markets](#override-base-price-for-specific-markets), either by selecting a new price tier or by entering a free-form price in the market's local currency. Please be aware that Microsoft does not alter the product pricing you set without your approval. You’re in charge of making sure the prices match the current market situations, including currency exchange rates. 

:::image type="content" source="images/msix-set-app-pricing.png" lightbox="images/msix-set-app-pricing.png" alt-text="A screenshot of the Pricing and availability section showing how to set app pricing.":::

> [!NOTE]
> Although this topic refers to apps, price selection for add-on submissions uses the same process. Note that for [subscription add-ons](/windows/uwp/monetize/enable-subscription-add-ons-for-your-app), the base price that you select can't ever be increased (whether by changing the base price or by scheduling a price change), although it may be decreased.

## Base price

When you select your app's **Base price**, that price will be used in every market where your app is sold, unless you override the base price in any market(s).

You can set the **Base price** to **Free**, or you can choose an available price tier, which sets the price in all the countries where you choose to distribute your app. Price tiers start at 0.99 USD, with additional tiers available at increasing increments (1.09 USD, 1.19 USD, and so on). The increments generally increase as the price gets higher.

> [!NOTE]
> These price tiers also apply to add-ons.
Each  price tier has a corresponding value in each of the more than 60 currencies offered by the Store. We use these values to help you sell your apps at a comparable price point worldwide. You can select your base price in any currency, and we’ll automatically use the corresponding value for different markets. Note that at times we may adjust the corresponding value in a certain market to account for changes in currency conversion rates. You can click on Review price per market button to view the prices for each market. 

In the **Pricing** section, click **view conversion table** to see the corresponding prices in all currencies. This also displays an ID number associated with each price tier, which you’ll need if you're using the [Microsoft Store submission API](/windows/uwp/monetize/manage-app-submissions#price-tiers) to enter prices. You can click **Download** to download a copy of the price tier table as a .csv file.

Keep in mind that the price tier you select may include sales or value-added tax that your customers must pay. To learn more about your app’s tax implications in selected markets, see [Tax details for paid apps](/partner-center/tax-details-marketplace). You should also review the [price considerations for specific markets](../../../apps/publish/publish-your-app/market-selection.md#price-considerations-for-specific-markets).

> [!NOTE]
> If you choose the **Stop acquisition** option under **Make this product available but not discoverable in the Store** in the [Visibility](../../../apps/publish/publish-your-app/visibility-options.md#discoverability) section), you won't be able to set pricing for your submission (since no one will able to acquire the app unless they use a promotional code to get the app for free).

## Schedule price changes

You can optionally schedule one or more price changes if you want the base price of your app to change at a specific date and time.

> [!IMPORTANT]
> Price changes are only shown to customers on Windows 10 or Windows 11 devices (including Xbox). If your previously-published app supports earlier OS versions, the price changes will not apply to those customers. For customers on Windows 8, the app will always be offered at its **Base price** (and not any market-specific price), even if you schedule additional price changes. For customers on Windows 8.1, the app will always be offered at the first price tier for the customer's market.
Click **Schedule a price change** to see the price change options. Choose the price tier you’d like to use (or enter a free-form price for single-market base price overrides), then select the date, time, and time zone.

You can click **Schedule a price change** again to schedule as many subsequent changes as you’d like.

> [!NOTE]
> Scheduled price changes work differently from [Sale pricing](../../../apps/publish/put-apps-and-add-ons-on-sale.md). When you put an app on sale, the price shows with a strikethrough in the Store, and customers will be able to purchase the app at the sale price during the time period that you have selected. After the sale period is up, the sale price will no longer apply and the app will be available at its base price (or a different price that you have specified for that market, if applicable).
>
> With a scheduled price change, you can adjust the price to be either higher or lower. The change will take place on the date you specify, but it won’t be displayed as a sale in the Store, or have any special formatting applied; the app will just have a new price.

## Override base price for specific markets

By default, the options you select above will apply to all markets in which your app is offered. You can optionally change the price for one or more markets, either by choosing a different price tier or entering a free-form price in the market’s local currency. This way, you can maintain your regional pricing strategy and respond more effectively to the changes in the currency exchange rates in each market. 

> [!IMPORTANT]
> If your previously-published app supports Windows 8, those customers will always see the app at its **Base price**, even if you select a different price for their market.

You can override the base price for one market at a time, or for a group of markets together. Once you’ve done so, you can override the base price for an additional market, (or an additional market group) by selecting **Select markets for base price override** again and repeating the process described below. To remove the override pricing you’ve specified for a market (or market group), click **Remove**.

### Override the base price for a single market

To change the price for one market only, select it and click **Create**. You’ll then see the same **Base price** and **Schedule a price change** options as described above, but the selections you make will be specific to that market. Because you are overriding the base  price for one market only, the price tiers will be shown in that market’s local currency. You can click **view conversion table** to see the corresponding prices in all currencies.

Overriding the base price for a single market also gives you the option to enter a free-form price of your choosing in the market’s local currency. You can enter any price you like (within a minimum and maximum range), even if it does not correspond to one of the standard price tiers. This price will be used only for customers on Windows 10 or Windows 11 (including Xbox) in the selected market.

> [!IMPORTANT]
> If you enter a free-form price, that price will not be adjusted (even if conversion rates change) unless you submit an update with a new price.

### Override the base price for a market group

To override the base price for multiple markets, you’ll create a *market group*. To do so, select the markets you wish to include, then optionally enter a name for the group. (This name is for your reference only and won’t be visible to any customers.) When you’re finished, click **Create**. You’ll then see the same **Base price** and **Schedule a price change** options as described above, but the selections you make will be specific to that market group. Note that free-form prices can’t be used with market groups; you’ll need to select an available price tier.

To change the markets included in a market group, click the name of the market group and add or remove any markets you’d like, then click **OK** to save your changes.

> [!NOTE]
> A market can’t belong to multiple market groups within the **Pricing** section.
