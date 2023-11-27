The **Schedule** section on the [Pricing and availability](../../../apps/publish/publish-your-app/price-and-availability.md) page lets you set the precise date and time that your app should become available in the Store, giving you greater flexibility and the ability to customize dates for different markets.

> [!NOTE]
> Although this topic refers to apps, release scheduling for add-on submissions uses the same process.
You can additionally opt to set a date when the product should no longer be available in the Store. Note that this means that the product can no longer be found in the Store via searching or browsing, but any customer with a direct link can see the product's Store listing. They can only download it if they already own the product or if they have a [promotional code](../../../apps/publish/generate-promotional-codes.md) and are using a Windows 10 or Windows 11 device.

By default (unless you have selected one of the **Make this app available but not discoverable in the Store** options in the [Visibility](../../../apps/publish/publish-your-app/visibility-options.md#discoverability) section), your app will be available to customers as soon as it passes certification and complete the publishing process. To choose other dates, select **Show options** to expand this section.

Note that you won't be able to configure dates in the **Schedule** section if you have selected one of the **Make this app available but not discoverable in the Store** options in the [Visibility](../../../apps/publish/publish-your-app/visibility-options.md#discoverability) section, because your app won't be released to customers, so there is no release date to configure.

> [!IMPORTANT]
> The dates you specify in the Schedule section only apply to customers on Windows 10 and Windows 11.
>
>If your previously-published app supports earlier OS versions, any **Stop acquisition** date you select will not apply to those customers; they will still be able to acquire the app (unless you submit an update with a new selection in the [Visibility](../../../apps/publish/publish-your-app/visibility-options.md#discoverability) section, or if you select **Make app unavailable** from the **App overview** page).

## Base schedule

Selections you make for the Base schedule will apply to all markets in which your app is available, unless you later add dates for specific markets (or market groups) by selecting [Customize for specific markets](#customize-the-schedule-for-specific-markets).

You’ll see two options here: **Release** and **Stop acquisition**.

## Release

In the **Release** drop-down, you can set when you want your app to be available in the Store. This means that the app is discoverable in the Store via searching or browsing, and that customers can view its Store listing and acquire the app.

>[!NOTE]
> After your app has been published and has become available in the Store, you will no longer be able to select a **Release** date (since the app will already have been released).
Here are the options you can configure for a product’s **Release** schedule:

- **as soon as possible**: The product will release as soon as it is certified and published. This is the default option.
- **at**: The product will release on the date and time that you select. You additionally have two options:
  - **UTC**: The time you select will be Universal Coordinated Time (UTC) time, so that the app releases at the same time everywhere.
  - **Local**: The time you select will be the used in each time zone associated with a market. (Note that for markets that include more than one time zone, only one time zone in that market will be used. For the United States, the Eastern time zone is used. A comprehensive list of time zones is shown further down this page.)
- **not scheduled**: The app will not be available in the Store. If you choose this option, you can make the app available in the Store later by creating a new submission and choosing one of the other options.

## Stop acquisition

In the **Stop acquisition** dropdown, you can set a date and time when you want to stop allowing new customers to acquire it from the Store or discover its listing. This can be useful if you want to precisely control when an app will no longer be offered to new customers, such as when you are coordinating availability between more than one of your apps.

By default, **Stop acquisition** is set to never. To change this, select **at** in the drop-down and specify a date and time, as described above. At the date and time you select, customers will no longer be able to acquire the app.

It's important to understand that this option has the same impact as selecting **Make this app discoverable but not available** in the [Visibility](../../../apps/publish/publish-your-app/visibility-options.md#discoverability) section and choosing **Stop acquisition: Any customer with a direct link can see the product’s Store listing, but they can only download it if they owned the product before, or have a promotional code and are using a Windows 10 or Windows 11 device.** To completely stop offering an app to new customers, click **Make app unavailable** from the App overview page. For more info, see [Removing an app from the Store](../../../apps/publish/publish-your-app/app-package-management.md#removing-an-app-from-the-store).

> [!TIP]
> If you select a date to **Stop acquisition**, and later decide you'd like to make the app available again, you can create a new submission and change **Stop acquisition** back to **Never**. The app will become available again after your updated submission is published.
## Customize the schedule for specific markets

By default, the options you select above will apply to all markets in which your app is offered. To customize the price for specific markets, click **Customize for specific markets**. The **Market selection** pop-up window will appear, listing all of the markets in which you’ve chosen to make your app available. If you excluded any markets in the [Markets](../../../apps/publish/publish-your-app/market-selection.md) section, those markets will not be shown.

To add a schedule for one market, select it and click **Save**. You’ll then see the same **Release** and **Stop acquisition** options described above, but the selections you make will only apply to that market.

To add a schedule that will apply to multiple markets, you’ll create a *market group*. To do so, select the markets you wish to include, then enter a name for the group. (This name is for your reference only and won’t be visible to any customers.) For example, if you want to create a market group for North America, you can select **Canada**, **Mexico**, and **United States**, and name it **North America** or another name that you choose. When you’re finished creating your market group, click **Save**. You’ll then see the same **Release** and **Stop acquisition** options described above, but the selections you make will only apply to that market group.

To add a custom schedule for an additional market, or an additional market group, just click **Customize for specific markets** again and repeat these steps. To change the markets included in a market group, select its name. To remove the custom schedule for a market group (or individual market), click **Remove**.

> [!NOTE]
> A market can’t belong to more than one of the market groups you use in the **Schedule** section.
## Global Time Zones

Below is a table that shows what specific time zones are used in each market, so when your submission uses local time (e.g. release at 9am local), you can find out what time will it be released in each market, in particular helpful with markets that have more than one time zone, like Canada.

| Market                                       | Time Zone                                                     |
|----------------------------------------------|---------------------------------------------------------------|
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
| SÃ£o TomÃ© and PrÃ­ncipe                      | (UTC+00:00) Monrovia, Reykjavik                               |
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
