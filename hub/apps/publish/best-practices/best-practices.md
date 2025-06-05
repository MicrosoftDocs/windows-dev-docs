---
description: Best practices for publishing your app
title: Best practices for publishing your app
ms.topic: article
ms.date: 05/27/2025
---

# Best practices

## Tips for choosing a great app name

Choosing the right name for your app is important. Pick a name that will capture your customers' interest and draw them in to learn more about your app. Here are some tips for choosing a great app name.

**Keep it short**. While your app's name can have up to 256 characters, the space used to display your app's name is limited. Long names may be truncated based on where in the store your app is being displayed and the user's display size and settings.

> [!TIP]
> Windows uses variable width fonts, so the number of visible characters in your title depends on which characters you use. For example, using Segoe UI, about 30 `i` characters will fit in the same space as 10 `w` characters. If you have multiple apps, be sure to test the visibility of each app's title, even if they are the same number of characters. Also be sure to test all localizations of your app's name. Keep in mind that East-Asian characters tend to be wider than Latin characters, so fewer characters will be displayed.

**Be original**. Make sure your app name is distinctive enough that it won't be easily confused with an existing app.

**Do not use names trademarked by others**. Make sure that you have the right to use the name that you reserve. If someone else has trademarked the name, they can report an infringement and you will not be able to keep using that name. If that happens after your app has been published, it will be removed from the Store until you've changed all instances of the name in your app, its content, and its store listing before you can submit your app for certification again.

**Avoid trailing differentiators**. Information that distinguishes different versions of your app should not be put at the end of your title. This information can be truncated by the UI, and users can miss it even if it is displayed.

If this is unavoidable, use different logos and app images to make it easier to differentiate one app from another.

**Do not include emojis in your name**. You will not be able to reserve a name that includes emojis or other unsupported characters.

### Choosing which device families to support

If you upload packages targeting one individual device family, we'll check the box to make those packages available to new customers on that type of device. For example, if a package targets Windows.Desktop, the **Windows 10/11 Desktop** box will be checked for that package (and you won't be able to check the boxes for other device families).

Packages targeting the Windows.Universal device family can run on any Windows 10 or Windows 11 device (including Xbox One). By default, we'll make those packages available to new customers on all device types _except_ for Xbox.

You can uncheck the box for any Windows 10 or Windows 11 device family if you don’t want to offer your submission to customers on that type of device. If a device family’s box is unchecked, new customers on that type of device won’t be able to acquire the app (though customers who already have the app can still use it, and will get any updates you submit).

If your app supports them, we recommend keeping all of the boxes checked, unless you have a specific reason to limit the types of Windows 10 or Windows 11 devices which can acquire your app. For instance, if you know that your app doesn't offer a good experience on [Surface Hub](https://developer.microsoft.com/windows/surfacehub) and/or [Microsoft HoloLens](https://developer.microsoft.com/mixed-reality), you can uncheck the **Windows 10 Team** and/or **Windows 10 Holographic** box. This prevents any new customers from acquiring the app on those devices. If you later decide you're ready to offer it to those customers, you can create a new submission with the boxes checked.

## Write a great app description for MSIX app

A great description can make your app stand out in the Microsoft Store and help encourage customers to download it. [The description you enter when submitting your app](/hub/apps/publish/publish-your-app/msix/add-and-edit-store-listing-info.md#description) is displayed in your app's Store listing. The first few lines may also be displayed in search results and algorithm lists in the Store.

Here are some tips for making your app's description the best it can be.

- **Grab attention in the first few sentences.** The beginning of your description is the most important, so make sure it grabs and holds attention. Start with the value prop: why should potential customers take the time and money to get your app? What is the benefit to choosing your app over another? In one or two sentences, using plain and clear language, explain your app's unique appeal and why someone would want it.
- **Make it easy to learn about your app.** After your initial hook, describe additional benefits, in-app purchase opportunities, and other details about your app that customers will want to know. Make sure you include any disclosures or information that you are required to provide under the law in the markets where you are distributing your app.
- **Use lists and short paragraphs.** Potential customers may just take a quick glance at your app's description. Breaking up the content by using short paragraphs and lists makes it easier to scan.

  > [!NOTE]
  >  Adding a list of [product features](/hub/apps/publish/publish-your-app/msix/create-app-store-listing.md) can also help to quickly show what your app does. This list appears directly below the app description.

- **Avoid dry language.** Write your description using engaging language. Be sure the wording clearly describes what your app does, but say it in a way that doesn't sound boring. For many apps, a casual and friendly tone works well.
- **Use a length that is just right.** A good description reads quickly, but also includes enough info to get the reader interested and explain what the app does. A complex app will need more sentences to describe it; a simple app may need only a few. In most cases the right length is somewhere over 200 words, but well under 3000.
- **Be clear about free trials and add-ons.** If you offer a free trial of your app, be sure to explain how that trial works, so that customers understand which features are limited. It's also a good idea to mention what types of add-ons are available, particularly if they have significant impacts on your app's functionality.
- **Use standard capitalization and punctuation.** Descriptions in all caps, or those that have unusual punctuation, can be hard to read.
- **Don't forget to check the spelling and grammar.** A description with lots of misspelled words or mangled sentences doesn't reflect well on the quality of your app. Be sure to review your description (or have someone else take a look) to check for errors.
- **Don't include links or info that belongs elsewhere.** URLs that you enter in the description field won't be clickable, so don't try to add links for things like your privacy policy or support website. Instead, add these in the designated areas of the **Properties** page of your submission.
- **Don't use HTML tags.** HTML or other code will not be rendered. Your description needs to be plain text only.
- **Get ideas by reviewing descriptions of similar apps in the Store.** Take a look at how other developers describe their apps. This also helps you figure out what you can emphasize that is different about your app.

## Global Time Zones

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
| Kyrgyzstan                                   | (UTC+06:00) Astana                                         |
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
