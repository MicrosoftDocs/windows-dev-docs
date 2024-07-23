---
description: Select the base price for an app add-on and schedule price changes. You can also customize these options for specific markets.
title: Set and schedule app pricing for add-on
ms.date: 10/30/2022
ms.topic: article
ms.localizationpriority: medium
---

# Set and schedule app pricing for add-on

## Configure app pricing

The **Pricing** section of the [Pricing and availability](./price-and-availability.md) page lets you select the base price for an app. You can also [schedule price changes](#schedule-price-changes) to indicate the date and time at which your app’s price should change. Additionally, you have the option to [override the base price for specific markets](#override-base-price-for-specific-markets), either by selecting a new price tier or by entering a free-form price in the market's local currency. Please be aware that Microsoft does not alter the product pricing you set without your approval. You’re in charge of making sure the prices match the current market situations, including currency exchange rates.

:::image type="content" source="../msix/images/msix-set-app-pricing.png" lightbox="../msix/images/msix-set-app-pricing.png" alt-text="A screenshot of the Pricing and availability section showing how to set app pricing.":::

> [!NOTE]
> Although this topic refers to apps, price selection for add-on submissions uses the same process. Note that for [subscription add-ons](/windows/uwp/monetize/enable-subscription-add-ons-for-your-app), the base price that you select can't ever be increased (whether by changing the base price or by scheduling a price change), although it may be decreased.

### Base price

When you select your app's **Base price**, that price will be used in every market where your app is sold, unless you override the base price in any market(s).

You can set the **Base price** to **Free**, or you can choose an available price tier, which sets the price in all the countries where you choose to distribute your app. Price tiers start at 0.99 USD, with additional tiers available at increasing increments (1.09 USD, 1.19 USD, and so on). The increments generally increase as the price gets higher.

> [!NOTE]
> These price tiers also apply to add-ons.
> Each price tier has a corresponding value in each of the more than 60 currencies offered by the Store. We use these values to help you sell your apps at a comparable price point worldwide. You can select your base price in any currency, and we’ll automatically use the corresponding value for different markets. Note that at times we may adjust the corresponding value in a certain market to account for changes in currency conversion rates. You can click on Review price per market button to view the prices for each market.

In the **Pricing** section, click **view conversion table** to see the corresponding prices in all currencies. This also displays an ID number associated with each price tier, which you’ll need if you're using the [Microsoft Store submission API](/windows/uwp/monetize/manage-app-submissions#price-tiers) to enter prices. You can click **Download** to download a copy of the price tier table as a .csv file.

Keep in mind that the price tier you select may include sales or value-added tax that your customers must pay. To learn more about your app’s tax implications in selected markets, see [Tax details for paid apps](/partner-center/tax-details-marketplace). You should also review the [price considerations for specific markets](./market-selection.md#price-considerations-for-specific-markets).

> [!NOTE]
> If you choose the **Stop acquisition** option under **Make this product available but not discoverable in the Store** in the [Visibility](./visibility-options.md#discoverability) section), you won't be able to set pricing for your submission (since no one will able to acquire the app unless they use a promotional code to get the app for free).

### Schedule price changes

You can optionally schedule one or more price changes if you want the base price of your app to change at a specific date and time.

> [!IMPORTANT]
> Price changes are only shown to customers on Windows 10 or Windows 11 devices (including Xbox).
> Click **Schedule a price change** to see the price change options. Choose the price tier you’d like to use (or enter a free-form price for single-market base price overrides), then select the date, time, and time zone.

You can click **Schedule a price change** again to schedule as many subsequent changes as you’d like.

> [!NOTE]
> Scheduled price changes work differently from [Sale pricing](../../put-apps-and-add-ons-on-sale.md). When you put an app on sale, the price shows with a strikethrough in the Store, and customers will be able to purchase the app at the sale price during the time period that you have selected. After the sale period is up, the sale price will no longer apply and the app will be available at its base price (or a different price that you have specified for that market, if applicable).
>
> With a scheduled price change, you can adjust the price to be either higher or lower. The change will take place on the date you specify, but it won’t be displayed as a sale in the Store, or have any special formatting applied; the app will just have a new price.

### Override base price for specific markets

By default, the options you select above will apply to all markets in which your app is offered. You can optionally change the price for one or more markets, either by choosing a different price tier or entering a free-form price in the market’s local currency. This way, you can maintain your regional pricing strategy and respond more effectively to the changes in the currency exchange rates in each market.

You can override the base price for one market at a time, or for a group of markets together. Once you’ve done so, you can override the base price for an additional market, (or an additional market group) by selecting **Select markets for base price override** again and repeating the process described below. To remove the override pricing you’ve specified for a market (or market group), click **Remove**.

#### Override the base price for a single market

To change the price for one market only, select it and click **Create**. You’ll then see the same **Base price** and **Schedule a price change** options as described above, but the selections you make will be specific to that market. Because you are overriding the base price for one market only, the price tiers will be shown in that market’s local currency. You can click **view conversion table** to see the corresponding prices in all currencies.

Overriding the base price for a single market also gives you the option to enter a free-form price of your choosing in the market’s local currency. You can enter any price you like (within a minimum and maximum range), even if it does not correspond to one of the standard price tiers. This price will be used only for customers on Windows 10 or Windows 11 (including Xbox) in the selected market.

> [!IMPORTANT]
> If you enter a free-form price, that price will not be adjusted (even if conversion rates change) unless you submit an update with a new price.

#### Override the base price for a market group

To override the base price for multiple markets, you’ll create a _market group_. To do so, select the markets you wish to include, then optionally enter a name for the group. (This name is for your reference only and won’t be visible to any customers.) When you’re finished, click **Create**. You’ll then see the same **Base price** and **Schedule a price change** options as described above, but the selections you make will be specific to that market group. Note that free-form prices can’t be used with market groups; you’ll need to select an available price tier.

To change the markets included in a market group, click the name of the market group and add or remove any markets you’d like, then click **OK** to save your changes.

> [!NOTE]
> A market can’t belong to multiple market groups within the **Pricing** section.

## Configure precise release scheduling

The **Schedule** section on the [Pricing and availability](./price-and-availability.md) page lets you set the precise date and time that your app should become available in the Store, giving you greater flexibility and the ability to customize dates for different markets.

:::image type="content" source="../msix/images/msix-precise-release-scheduling.png" lightbox="../msix/images/msix-precise-release-scheduling.png" alt-text="A screenshot of the Pricing and availability section showing precise release scheduling options.":::

> [!NOTE]
> Although this topic refers to apps, release scheduling for add-on submissions uses the same process.
> You can additionally opt to set a date when the product should no longer be available in the Store. Note that this means that the product can no longer be found in the Store via searching or browsing, but any customer with a direct link can see the product's Store listing. They can only download it if they already own the product or if they have a [promotional code](../../generate-promotional-codes.md) and are using a Windows 10 or Windows 11 device.

By default (unless you have selected one of the **Make this app available but not discoverable in the Store** options in the [Visibility](./visibility-options.md#discoverability) section), your app will be available to customers as soon as it passes certification and complete the publishing process. To choose other dates, select **Show options** to expand this section.

Note that you won't be able to configure dates in the **Schedule** section if you have selected one of the **Make this app available but not discoverable in the Store** options in the [Visibility](./visibility-options.md#discoverability) section, because your app won't be released to customers, so there is no release date to configure.

> [!IMPORTANT]
> The dates you specify in the Schedule section only apply to customers on Windows 10 and Windows 11.
>
> If your previously-published app supports earlier OS versions, any **Stop acquisition** date you select will not apply to those customers; they will still be able to acquire the app (unless you submit an update with a new selection in the [Visibility](./visibility-options.md#discoverability) section, or if you select **Make app unavailable** from the **App overview** page).

### Base schedule

Selections you make for the Base schedule will apply to all markets in which your app is available, unless you later add dates for specific markets (or market groups) by selecting [Customize for specific markets](#customize-the-schedule-for-specific-markets).

You’ll see two options here: **Release** and **Stop acquisition**.

### Release

In the **Release** drop-down, you can set when you want your app to be available in the Store. This means that the app is discoverable in the Store via searching or browsing, and that customers can view its Store listing and acquire the app.

> [!NOTE]
> After your app has been published and has become available in the Store, you will no longer be able to select a **Release** date (since the app will already have been released).
> Here are the options you can configure for a product’s **Release** schedule:

- **as soon as possible**: The product will release as soon as it is certified and published. This is the default option.
- **at**: The product will release on the date and time that you select. You additionally have two options:
  - **UTC**: The time you select will be Universal Coordinated Time (UTC) time, so that the app releases at the same time everywhere.
  - **Local**: The time you select will be the used in each time zone associated with a market. (Note that for markets that include more than one time zone, only one time zone in that market will be used. For the United States, the Eastern time zone is used. A comprehensive list of time zones is shown further down this page.)

### Stop acquisition

In the **Stop acquisition** dropdown, you can set a date and time when you want to stop allowing new customers to acquire it from the Store or discover its listing. This can be useful if you want to precisely control when an app will no longer be offered to new customers, such as when you are coordinating availability between more than one of your apps.

By default, **Stop acquisition** is set to never. To change this, select **at** in the drop-down and specify a date and time, as described above. At the date and time you select, customers will no longer be able to acquire the app.

It's important to understand that this option has the same impact as selecting **Make this app discoverable but not available** in the [Visibility](./visibility-options.md#discoverability) section and choosing **Stop acquisition: Any customer with a direct link can see the product’s Store listing, but they can only download it if they owned the product before, or have a promotional code and are using a Windows 10 or Windows 11 device.** To completely stop offering an app to new customers, click **Make app unavailable** from the App overview page. For more info, see [Removing an app from the Store](../msix/app-package-management.md#removing-an-app-from-the-store).

> [!TIP]
> If you select a date to **Stop acquisition**, and later decide you'd like to make the app available again, you can create a new submission and change **Stop acquisition** back to **Never**. The app will become available again after your updated submission is published.

### Customize the schedule for specific markets

By default, the options you select above will apply to all markets in which your app is offered. To customize the price for specific markets, click **Customize for specific markets**. The **Market selection** pop-up window will appear, listing all of the markets in which you’ve chosen to make your app available. If you excluded any markets in the [Markets](./market-selection.md) section, those markets will not be shown.

To add a schedule for one market, select it and click **Create**. You’ll then see the same **Release** and **Stop acquisition** options described above, but the selections you make will only apply to that market.

To add a schedule that will apply to multiple markets, you’ll create a _market group_. To do so, select the markets you wish to include, then enter a name for the group. (This name is for your reference only and won’t be visible to any customers.) For example, if you want to create a market group for North America, you can select **Canada**, **Mexico**, and **United States**, and name it **North America** or another name that you choose. When you’re finished creating your market group, click **Create**. You’ll then see the same **Release** and **Stop acquisition** options described above, but the selections you make will only apply to that market group.

To add a custom schedule for an additional market, or an additional market group, just click **Customize for specific markets** again and repeat these steps. To change the markets included in a market group, select its name. To remove the custom schedule for a market group (or individual market), click **Remove**.

> [!NOTE]
> A market can’t belong to more than one of the market groups you use in the **Schedule** section.

### Global Time Zones

Below is a table that shows what specific time zones are used in each market, so when your submission uses local time (e.g. release at 9am local), you can find out what time will it be released in each market, in particular helpful with markets that have more than one time zone, like Canada.

| Market                                       | Time Zone                                                     |
| -------------------------------------------- | ------------------------------------------------------------- |
| Afghanistan                                  | (UTC+04:30) Kabul                                             |
| Albania                                      | (UTC+01:00) Sarajevo, Skopje, Warsaw, Zagreb                  |
| Algeria                                      | (UTC+01:00) Sarajevo, Skopje, Warsaw, Zagreb                  |
| American Samoa                               | (UTC+13:00) Samoa                                             |
| Andorra                                      | (UTC+01:00) Sarajevo, Skopje, Warsaw, Zagreb                  |
| Angola                                       | (UTC+01:00) West Central Africa                               |
| Anguilla                                     | (UTC-04:00) Atlantic Time (Canada)                            |
| Antarctica                                   | (UTC+12:00) Auckland, Wellington                              |
| Antigua and Barbuda                          | (UTC-04:00) Atlantic Time (Canada)                            |
| Argentina                                    | (UTC-03:00) City of Buenos Aires                              |
| Armenia                                      | (UTC+04:00) Abu Dhabi, Muscat                                 |
| Aruba                                        | (UTC-04:00) Atlantic Time (Canada)                            |
| Australia                                    | (UTC+10:00) Canberra, Melbourne, Sydney                       |
| Austria                                      | (UTC+01:00) Amsterdam, Berlin, Bern, Rome, Stockholm, Vienna  |
| Azerbaijan                                   | (UTC+04:00) Baku                                              |
| Bahamas, The                                 | (UTC-05:00) Eastern Time (US & Canada)                        |
| Bahrain                                      | (UTC+04:00) Abu Dhabi, Muscat                                 |
| Bangladesh                                   | (UTC+06:00) Dhaka                                             |
| Barbados                                     | (UTC-04:00) Atlantic Time (Canada)                            |
| Belarus                                      | (UTC+03:00) Minsk                                             |
| Belgium                                      | (UTC+01:00) Brussels, Copenhagen, Madrid, Paris               |
| Belize                                       | (UTC-06:00) Central Time (US & Canada)                        |
| Benin                                        | (UTC+01:00) West Central Africa                               |
| Bermuda                                      | (UTC-04:00) Atlantic Time (Canada)                            |
| Bhutan                                       | (UTC+06:00) Dhaka                                             |
| Bolivarian Republic of Venezuela             | (UTC-04:00) Caracas                                           |
| Bolivia                                      | (UTC-04:00) Georgetown, La Paz, Manaus, San Juan              |
| Bonaire, Saint Eustatius and Saba            | (UTC-04:00) Atlantic Time (Canada)                            |
| Bosnia and Herzegovina                       | (UTC+01:00) Sarajevo, Skopje, Warsaw, Zagreb                  |
| Botswana                                     | (UTC+01:00) West Central Africa                               |
| Bouvet Island                                | (UTC+00:00) Monrovia, Reykjavik                               |
| Brazil                                       | (UTC-03:00) Brasilia                                          |
| British Indian Ocean Territory               | (UTC+06:00) Dhaka                                             |
| British Virgin Islands                       | (UTC-04:00) Atlantic Time (Canada)                            |
| Brunei                                       | (UTC+08:00) Irkutsk                                           |
| Bulgaria                                     | (UTC+02:00) Chisinau                                          |
| Burkina Faso                                 | (UTC+00:00) Monrovia, Reykjavik                               |
| Burundi                                      | (UTC+02:00) Harare, Pretoria                                  |
| CÃ´te d'Ivoire                               | (UTC+00:00) Monrovia, Reykjavik                               |
| Cambodia                                     | (UTC+07:00) Bangkok, Hanoi, Jakarta                           |
| Cameroon                                     | (UTC+01:00) West Central Africa                               |
| Canada                                       | (UTC-05:00) Eastern Time (US & Canada)                        |
| Cabo Verde                                   | (UTC-01:00) Cabo Verde Is.                                    |
| Cayman Islands                               | (UTC-05:00) Eastern Time (US & Canada)                        |
| Central African Republic                     | (UTC+01:00) West Central Africa                               |
| Chad                                         | (UTC+01:00) West Central Africa                               |
| Chile                                        | (UTC-04:00) Santiago                                          |
| China                                        | (UTC+08:00) Beijing, Chongqing, Hong Kong, Urumqi             |
| Christmas Island                             | (UTC+07:00) Krasnoyarsk                                       |
| Cocos (Keeling) Islands                      | (UTC+06:30) Yangon (Rangoon)                                  |
| Colombia                                     | (UTC-05:00) Bogota, Lima, Quito, Rio Branco                   |
| Comoros                                      | (UTC+03:00) Nairobi                                           |
| Congo                                        | (UTC+01:00) West Central Africa                               |
| Congo (DRC)                                  | (UTC+01:00) West Central Africa                               |
| Cook Islands                                 | (UTC-10:00) Hawaii                                            |
| Costa Rica                                   | (UTC-06:00) Central Time (US & Canada)                        |
| Croatia                                      | (UTC+01:00) Sarajevo, Skopje, Warsaw, Zagreb                  |
| CuraÃ§ao                                     | (UTC-04:00) Cuiaba                                            |
| Cyprus                                       | (UTC+02:00) Chisinau                                          |
| Czechia                                      | (UTC+01:00) Belgrade, Bratislava, Budapest, Ljubljana, Prague |
| Denmark                                      | (UTC+01:00) Brussels, Copenhagen, Madrid, Paris               |
| Djibouti                                     | (UTC+03:00) Nairobi                                           |
| Dominica                                     | (UTC-04:00) Atlantic Time (Canada)                            |
| Dominican Republic                           | (UTC-04:00) Atlantic Time (Canada)                            |
| Ecuador                                      | (UTC-05:00) Bogota, Lima, Quito, Rio Branco                   |
| Egypt                                        | (UTC+02:00) Chisinau                                          |
| El Salvador                                  | (UTC-06:00) Central Time (US & Canada)                        |
| Equatorial Guinea                            | (UTC+01:00) West Central Africa                               |
| Eritrea                                      | (UTC+03:00) Nairobi                                           |
| Estonia                                      | (UTC+02:00) Chisinau                                          |
| Ethiopia                                     | (UTC+03:00) Nairobi                                           |
| Falkland Islands                             | (UTC-04:00) Santiago                                          |
| Faroe Islands                                | (UTC+00:00) Dublin, Edinburgh, Lisbon, London                 |
| Fiji                                         | (UTC+12:00) Fiji                                              |
| Finland                                      | (UTC+02:00) Helsinki, Kyiv, Riga, Sofia, Tallinn, Vilnius     |
| France                                       | (UTC+01:00) Brussels, Copenhagen, Madrid, Paris               |
| French Guiana                                | (UTC-03:00) Cayenne, Fortaleza                                |
| French Polynesia                             | (UTC-10:00) Hawaii                                            |
| French Southern and Antarctic Lands          | (UTC+05:00) Ashgabat, Tashkent                                |
| Gabon                                        | (UTC+01:00) West Central Africa                               |
| Gambia, The                                  | (UTC+00:00) Monrovia, Reykjavik                               |
| Georgia                                      | (UTC-05:00) Eastern Time (US & Canada)                        |
| Germany                                      | (UTC+01:00) Amsterdam, Berlin, Bern, Rome, Stockholm, Vienna  |
| Ghana                                        | (UTC+00:00) Monrovia, Reykjavik                               |
| Gibraltar                                    | (UTC+01:00) Sarajevo, Skopje, Warsaw, Zagreb                  |
| Greece                                       | (UTC+02:00) Athens, Bucharest                                 |
| Greenland                                    | (UTC+00:00) Monrovia, Reykjavik                               |
| Grenada                                      | (UTC-04:00) Atlantic Time (Canada)                            |
| Guadeloupe                                   | (UTC-04:00) Atlantic Time (Canada)                            |
| Guam                                         | (UTC+10:00) Guam, Port Moresby                                |
| Guatemala                                    | (UTC-06:00) Central Time (US & Canada)                        |
| Guernsey                                     | (UTC+00:00) Monrovia, Reykjavik                               |
| Guinea                                       | (UTC+00:00) Monrovia, Reykjavik                               |
| Guinea-Bissau                                | (UTC+00:00) Monrovia, Reykjavik                               |
| Guyana                                       | (UTC-04:00) Atlantic Time (Canada)                            |
| Haiti                                        | (UTC-05:00) Eastern Time (US & Canada)                        |
| Heard Island and McDonald Islands            | (UTC-05:00) Bogota, Lima, Quito, Rio Branco                   |
| Holy See (Vatican City)                      | (UTC+01:00) Sarajevo, Skopje, Warsaw, Zagreb                  |
| Honduras                                     | (UTC-06:00) Central Time (US & Canada)                        |
| Hong Kong SAR                                | (UTC+08:00) Beijing, Chongqing, Hong Kong, Urumqi             |
| Hungary                                      | (UTC+01:00) Belgrade, Bratislava, Budapest, Ljubljana, Prague |
| Iceland                                      | (UTC+00:00) Monrovia, Reykjavik                               |
| India                                        | (UTC+05:30) Chennai, Kolkata, Mumbai, New Delhi               |
| Indonesia                                    | (UTC+07:00) Bangkok, Hanoi, Jakarta                           |
| Iraq                                         | (UTC+04:00) Abu Dhabi, Muscat                                 |
| Ireland                                      | (UTC+00:00) Dublin, Edinburgh, Lisbon, London                 |
| Israel                                       | (UTC+02:00) Jerusalem                                         |
| Italy                                        | (UTC+01:00) Amsterdam, Berlin, Bern, Rome, Stockholm, Vienna  |
| Jamaica                                      | (UTC-05:00) Eastern Time (US & Canada)                        |
| Japan                                        | (UTC+09:00) Osaka, Sapporo, Tokyo                             |
| Jersey                                       | (UTC+00:00) Monrovia, Reykjavik                               |
| Jordan                                       | (UTC+02:00) Chisinau                                          |
| Kazakhstan                                   | (UTC+05:00) Ashgabat, Tashkent                                |
| Kenya                                        | (UTC+03:00) Nairobi                                           |
| Kiribati                                     | (UTC+14:00) Kiritimati Island                                 |
| Korea                                        | (UTC+09:00) Seoul                                             |
| Kuwait                                       | (UTC+04:00) Abu Dhabi, Muscat                                 |
| Kyrgyzstan                                   | (UTC+06:00) Nur-Sultan                                        |
| Laos                                         | (UTC+07:00) Bangkok, Hanoi, Jakarta                           |
| Latvia                                       | (UTC+02:00) Chisinau                                          |
| Lesotho                                      | (UTC+02:00) Harare, Pretoria                                  |
| Liberia                                      | (UTC+00:00) Monrovia, Reykjavik                               |
| Libya                                        | (UTC+02:00) Chisinau                                          |
| Liechtenstein                                | (UTC+01:00) Sarajevo, Skopje, Warsaw, Zagreb                  |
| Lithuania                                    | (UTC+02:00) Chisinau                                          |
| Luxembourg                                   | (UTC+01:00) Sarajevo, Skopje, Warsaw, Zagreb                  |
| Macao SAR                                    | (UTC+08:00) Beijing, Chongqing, Hong Kong, Urumqi             |
| Madagascar                                   | (UTC+03:00) Nairobi                                           |
| Malawi                                       | (UTC+02:00) Harare, Pretoria                                  |
| Malaysia                                     | (UTC+08:00) Kuala Lumpur, Singapore                           |
| Maldives                                     | (UTC+05:00) Ashgabat, Tashkent                                |
| Mali                                         | (UTC+00:00) Monrovia, Reykjavik                               |
| Malta                                        | (UTC+01:00) Sarajevo, Skopje, Warsaw, Zagreb                  |
| Man, Isle of                                 | (UTC+00:00) Dublin, Edinburgh, Lisbon, London                 |
| Marshall Islands                             | (UTC+12:00) Petropavlovsk-Kamchatsky - Old                    |
| Martinique                                   | (UTC-04:00) Atlantic Time (Canada)                            |
| Mauritania                                   | (UTC+00:00) Monrovia, Reykjavik                               |
| Mauritius                                    | (UTC+04:00) Port Louis                                        |
| Mayotte                                      | (UTC+03:00) Nairobi                                           |
| Mexico                                       | (UTC-06:00) Guadalajara, Mexico City, Monterrey               |
| Micronesia                                   | (UTC+10:00) Guam, Port Moresby                                |
| Moldova                                      | (UTC+02:00) Chisinau                                          |
| Monaco                                       | (UTC+01:00) Sarajevo, Skopje, Warsaw, Zagreb                  |
| Mongolia                                     | (UTC+07:00) Krasnoyarsk                                       |
| Montenegro                                   | (UTC+01:00) Sarajevo, Skopje, Warsaw, Zagreb                  |
| Montserrat                                   | (UTC-04:00) Atlantic Time (Canada)                            |
| Morocco                                      | (UTC+01:00) Casablanca                                        |
| Mozambique                                   | (UTC+02:00) Harare, Pretoria                                  |
| Myanmar                                      | (UTC+06:30) Yangon (Rangoon)                                  |
| Namibia                                      | (UTC+01:00) Amsterdam, Berlin, Bern, Rome, Stockholm, Vienna  |
| Nauru                                        | (UTC+12:00) Petropavlovsk-Kamchatsky - Old                    |
| Nepal                                        | (UTC+05:45) Kathmandu                                         |
| Netherlands                                  | (UTC+01:00) Amsterdam, Berlin, Bern, Rome, Stockholm, Vienna  |
| New Caledonia                                | (UTC+11:00) Solomon Is., New Caledonia                        |
| New Zealand                                  | (UTC+12:00) Auckland, Wellington                              |
| Nicaragua                                    | (UTC-06:00) Central Time (US & Canada)                        |
| Niger                                        | (UTC+01:00) West Central Africa                               |
| Nigeria                                      | (UTC+01:00) West Central Africa                               |
| Niue                                         | (UTC+13:00) Samoa                                             |
| Norfolk Island                               | (UTC+11:00) Solomon Is., New Caledonia                        |
| North Macedonia                              | (UTC+01:00) Sarajevo, Skopje, Warsaw, Zagreb                  |
| Northern Mariana Islands                     | (UTC+10:00) Guam, Port Moresby                                |
| Norway                                       | (UTC+01:00) Amsterdam, Berlin, Bern, Rome, Stockholm, Vienna  |
| Oman                                         | (UTC+04:00) Abu Dhabi, Muscat                                 |
| Pakistan                                     | (UTC+05:00) Islamabad, Karachi                                |
| Palau                                        | (UTC+09:00) Osaka, Sapporo, Tokyo                             |
| Palestinian Authority                        | (UTC+02:00) Chisinau                                          |
| Panama                                       | (UTC-05:00) Eastern Time (US & Canada)                        |
| Papua New Guinea                             | (UTC+10:00) Vladivostok                                       |
| Paraguay                                     | (UTC-04:00) Asuncion                                          |
| Peru                                         | (UTC-05:00) Bogota, Lima, Quito, Rio Branco                   |
| Philippines                                  | (UTC+08:00) Kuala Lumpur, Singapore                           |
| Pitcairn Islands                             | (UTC-08:00) Pacific Time (US & Canada)                        |
| Poland                                       | (UTC+01:00) Belgrade, Bratislava, Budapest, Ljubljana, Prague |
| Portugal                                     | (UTC+00:00) Dublin, Edinburgh, Lisbon, London                 |
| Qatar                                        | (UTC+04:00) Abu Dhabi, Muscat                                 |
| Reunion                                      | (UTC+04:00) Port Louis                                        |
| Romania                                      | (UTC+02:00) Chisinau                                          |
| ROW                                          | (UTC-07:00) Mountain Time (US & Canada)                       |
| Russia                                       | (UTC+03:00) Moscow, St. Petersburg                            |
| Rwanda                                       | (UTC+02:00) Harare, Pretoria                                  |
| SÃ£o TomÃ© and PrÃ­ncipe                     | (UTC+00:00) Monrovia, Reykjavik                               |
| Saint BarthÃ©lemy                            | (UTC+04:00) Yerevan                                           |
| Saint Helena, Ascension and Tristan da Cunha | (UTC+00:00) Dublin, Edinburgh, Lisbon, London                 |
| Saint Kitts and Nevis                        | (UTC-04:00) Atlantic Time (Canada)                            |
| Saint Lucia                                  | (UTC-04:00) Atlantic Time (Canada)                            |
| Saint Martin (French Part)                   | (UTC-04:00) Atlantic Time (Canada)                            |
| Saint Pierre and Miquelon                    | (UTC-02:00) Mid-Atlantic - Old                                |
| Saint Vincent and the Grenadines             | (UTC-04:00) Atlantic Time (Canada)                            |
| Samoa                                        | (UTC+13:00) Samoa                                             |
| San Marino                                   | (UTC+01:00) Sarajevo, Skopje, Warsaw, Zagreb                  |
| Saudi Arabia                                 | (UTC+03:00) Kuwait, Riyadh                                    |
| Senegal                                      | (UTC+00:00) Monrovia, Reykjavik                               |
| Serbia                                       | (UTC+01:00) Sarajevo, Skopje, Warsaw, Zagreb                  |
| Seychelles                                   | (UTC+04:00) Abu Dhabi, Muscat                                 |
| Sierra Leone                                 | (UTC+00:00) Monrovia, Reykjavik                               |
| Singapore                                    | (UTC+08:00) Kuala Lumpur, Singapore                           |
| Sint Maarten (Dutch Part)                    | (UTC-04:00) Atlantic Time (Canada)                            |
| Slovakia                                     | (UTC+01:00) Belgrade, Bratislava, Budapest, Ljubljana, Prague |
| Slovenia                                     | (UTC+01:00) Sarajevo, Skopje, Warsaw, Zagreb                  |
| Solomon Islands                              | (UTC+11:00) Solomon Is., New Caledonia                        |
| Somalia                                      | (UTC+03:00) Nairobi                                           |
| South Africa                                 | (UTC+02:00) Harare, Pretoria                                  |
| South Georgia and the South Sandwich Islands | (UTC-02:00) Mid-Atlantic - Old                                |
| Spain                                        | (UTC+01:00) Brussels, Copenhagen, Madrid, Paris               |
| Sri Lanka                                    | (UTC+05:30) Chennai, Kolkata, Mumbai, New Delhi               |
| Suriname                                     | (UTC-03:00) Cayenne, Fortaleza                                |
| Svalbard and Jan Mayen                       | (UTC+01:00) Sarajevo, Skopje, Warsaw, Zagreb                  |
| Swaziland                                    | (UTC+02:00) Harare, Pretoria                                  |
| Sweden                                       | (UTC+01:00) Amsterdam, Berlin, Bern, Rome, Stockholm, Vienna  |
| Switzerland                                  | (UTC+01:00) Amsterdam, Berlin, Bern, Rome, Stockholm, Vienna  |
| Taiwan                                       | (UTC+08:00) Taipei                                            |
| Tajikistan                                   | (UTC+05:00) Ashgabat, Tashkent                                |
| Tanzania                                     | (UTC+03:00) Nairobi                                           |
| Thailand                                     | (UTC+07:00) Bangkok, Hanoi, Jakarta                           |
| Timor-Leste                                  | (UTC+09:00) Seoul                                             |
| Togo                                         | (UTC+00:00) Monrovia, Reykjavik                               |
| Tokelau                                      | (UTC+13:00) Nuku'alofa                                        |
| Tonga                                        | (UTC+13:00) Nuku'alofa                                        |
| Trinidad and Tobago                          | (UTC-04:00) Atlantic Time (Canada)                            |
| Tunisia                                      | (UTC+01:00) Sarajevo, Skopje, Warsaw, Zagreb                  |
| Türkiye                                      | (UTC+03:00) Istanbul                                          |
| Turkmenistan                                 | (UTC+05:00) Ashgabat, Tashkent                                |
| Turks and Caicos Islands                     | (UTC-05:00) Eastern Time (US & Canada)                        |
| Tuvalu                                       | (UTC+12:00) Petropavlovsk-Kamchatsky - Old                    |
| U.S. Minor Outlying Islands                  | (UTC+13:00) Samoa                                             |
| U.S. Virgin Islands                          | (UTC-04:00) Atlantic Time (Canada)                            |
| Uganda                                       | (UTC+03:00) Nairobi                                           |
| Ukraine                                      | (UTC+02:00) Chisinau                                          |
| United Arab Emirates                         | (UTC+04:00) Abu Dhabi, Muscat                                 |
| United Kingdom                               | (UTC+00:00) Dublin, Edinburgh, Lisbon, London                 |
| United States                                | (UTC-05:00) Eastern Time (US & Canada)                        |
| Uruguay                                      | (UTC-03:00) Brasilia                                          |
| Uzbekistan                                   | (UTC+05:00) Ashgabat, Tashkent                                |
| Vanuatu                                      | (UTC+11:00) Solomon Is., New Caledonia                        |
| Vietnam                                      | (UTC+07:00) Bangkok, Hanoi, Jakarta                           |
| Wallis and Futuna                            | (UTC+12:00) Petropavlovsk-Kamchatsky - Old                    |
| Yemen                                        | (UTC+04:00) Abu Dhabi, Muscat                                 |
| Zambia                                       | (UTC+02:00) Harare, Pretoria                                  |
| Zimbabwe                                     | (UTC+02:00) Harare, Pretoria                                  |
