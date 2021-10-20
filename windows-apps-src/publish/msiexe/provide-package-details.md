---
description: The package details page for your MSI or EXE app is where you provide the location of your app's package, and specify CPU architecture, instller parameters, and supported languages.
title: Provide package details for your MSI or EXE app
ms.assetid: 0DD073F6-0E93-49BF-85D9-35D362AE3686
ms.date: 06/24/2021
ms.topic: article
keywords: windows 10, windows 11, windows, windows store, store, msi, exe, unpackaged, unpackaged app, desktop app, traditional desktop app, game settings, display mode, system requirements, hardware requirements, minimum hardware, recommended hardware, privacy policy, support contact info, app website, support info
ms.localizationpriority: medium
---

# Provide package details for your MSI or EXE app

> [!NOTE]
> MSI and EXE support in the Microsoft Store is currently in a limited public preview phase. As the size of the preview expands, we'll be adding new participants from the wait list. To join the wait list, click [here](https://aka.ms/storepreviewwaitlist).

The Packages page of the app submission process is where you provide the packages (MSI/EXE) and associated information for the app that you're submitting. When a customer downloads your app, the Store will automatically provide each customer with the package that works best for their device.

**Package URL**<br>*Required*

You must enter at least one versioned secure URL pointing to app package (MSI/EXE) hosted on your CDN. An example of versioned secure URL is https://www.contoso.com/downloads/1.1/setup.exe. You need to follow good CDN practices and ensure that this URL is performant, reliable, and available based on your market selection.

The binary on the package URL must not change after it is submitted to ensure only certified binaries are installed by users. If you need to update the package URL, you may use the Update submission option in Partner Center to specify a new package URL.

You must submit a standalone/offline installer and not a downloader that downloads binaries when invoked. This is required to certify the binaries that get installed are the same ones that passed the certification process.

**Languages**<br>*Required*

You can submit apps to the Microsoft Store in over 100 languages. Your app must support at least one of the following languages.

> [!NOTE]
> Language codes not listed here are not supported by the store.

| Language name         | Supported language codes |
|-----------------------|--------------------------|
| Afrikaans             | af, af-za                |
| Albanian              | sq, sq-al                |
| Amharic               | am, am-et                |
| Armenian              | hy, hy-am                |
| Assamese              | as, as-in                |
| Azerbaijani           | az-arab, az-arab-az, az-cyrl, az-cyrl-az, az-latn, az-latn-az |
| Basque (Basque)       | eu, eu-es                |
| Belarusian            | be, be-by                |
| Bangla                | bn, bn-bd, bn-in         |
| Bosnian               | bs, bs-cyrl, bs-cyrl-ba, bs-latn, bs-latn-ba |
| Bulgarian             | bg, bg-bg                |
| Catalan               | ca, ca-es, ca-es-valencia |
| Cherokee              | chr-cher, chr-cher-us, chr-latn |
| Chinese (Simplified)  | zh-Hans, zh-cn, zh-hans-cn, zh-sg, zh-hans-sg |
| Chinese (Traditional) | zh-Hant, zh-hk, zh-mo, zh-tw, zh-hant-hk, zh-hant-mo, zh-hant-tw, zh-mo, zh-tw, zh-hant-hk, zh-hant-mo, zh-hant-tw |
| Croatian              | hr, hr-hr, hr-ba         |
| Czech                 | cs, cs-cz                |
| Danish                | da, da-dk                |
| Dari                  | prs, prs-af, prs-arab    |
| Dutch                 | nl, nl-nl, nl-be         |
| English               | en, en-au, en-ca, en-gb, en-ie, en-in, en-nz, en-sg, en-us, en-za, en-bz, en-hk, en-id, en-jm, en-kz, en-mt, en-my, en-ph, en-pk, en-tt, en-vn, en-zw |
| Estonian              | et, et-ee                |
| Filipino              | fil, fil-latn, fil-ph    |
| Finnish               | fi, fi-fi                |
| French                | fr, fr-be , fr-ca , fr-ch , fr-fr , fr-lu, fr-cd, fr-ci, fr-cm, fr-ht, fr-ma, fr-mc, fr-ml, fr-re, frc-latn, frp-latn |
| Galician              | gl, gl-es                |
| Georgian              | ka, ka-ge                |
| German                | de, de-at, de-ch, de-de, de-lu, de-li |
| Greek                 | el, el-gr                |
| Gujarati              | gu, gu-in                |
| Hausa                 | ha, ha-latn, ha-latn-ng  |
| Hebrew                | he, he-il                |
| Hindi                 | hi, hi-in                |
| Hungarian             | hu, hu-hu                |
| Icelandic             | is, is-is                |
| Igbo                  | ig-latn, ig-ng           |
| Indonesian            | id, id-id                |
| Inuktitut (Latin)     | iu-cans, iu-latn, iu-latn-ca |
| Irish                 | ga, ga-ie                |
| isiXhosa              | xh, xh-za                |
| isiZulu               | zu, zu-za                |
| Italian               | it, it-it, it-ch         |
| Japanese              | ja , ja-jp               |
| Kannada               | kn, kn-in                |
| Kazakh                | kk, kk-kz                |
| Khmer                 | km, km-kh                |
| K'iche'               | quc-latn, qut-gt, qut-latn |
| Kinyarwanda           | rw, rw-rw                |
| KiSwahili             | sw, sw-ke                |
| Konkani               | kok, kok-in              |
| Korean                | ko, ko-kr                |
| Kurdish               | ku-arab, ku-arab-iq      |
| Kyrgyz                | ky-kg, ky-cyrl           |
| Lao                   | lo, lo-la                |
| Latvian               | lv, lv-lv                |
| Lithuanian            | lt, lt-lt                |
| Luxembourgish         | lb, lb-lu                |
| Macedonian            | mk, mk-mk                |
| Malay                 | ms, ms-bn, ms-my         |
| Malayalam             | ml, ml-in                |
| Maltese               | mt, mt-mt                |
| Maori                 | mi, mi-latn, mi-nz       |
| Marathi               | mr, mr-in                |
| Mongolian (Cyrillic)  | mn-cyrl, mn-mong, mn-mn, mn-phag |
| Nepali                | ne, ne-np                |
| Norwegian             | nb, nb-no, nn, nn-no, no, no-no |
| Odia                  | or, or-in                |
| Persian               | fa, fa-ir                |
| Polish                | pl, pl-pl                |
| Portuguese (Brazil)   | pt-br                    |
| Portuguese (Portugal) | pt, pt-pt                |
| Punjabi               | pa, pa-arab, pa-arab-pk, pa-deva, pa-in |
| Quechua               | quz, quz-bo, quz-ec, quz-pe |
| Romanian              | ro, ro-ro                |
| Russian               | ru , ru-ru               |
| Scottish Gaelic       | gd-gb, gd-latn           |
| Serbian (Latin)       | sr-Latn, sr-latn-cs, sr, sr-latn-ba, sr-latn-me, sr-latn-rs |
| Serbian (Cyrillic)    | sr-cyrl, sr-cyrl-ba, sr-cyrl-cs, sr-cyrl-me, sr-cyrl-rs |
| Sesotho sa Leboa      | nso, nso-za              |
| Setswana              | tn, tn-bw, tn-za         |
| Sindhi                | sd-arab, sd-arab-pk, sd-deva |
| Sinhala               | si, si-lk                |
| Slovak                | sk, sk-sk                |
| Slovenian             | sl, sl-si                |
| Spanish               | es, es-cl, es-co, es-es, es-mx, es-ar, es-bo, es-cr, es-do, es-ec, es-gt, es-hn, es-ni, es-pa, es-pe, es-pr, es-py, es-sv, es-us, es-uy, es-ve |
| Swedish               | sv, sv-se, sv-fi         |
| Tajik (Cyrillic)      | tg-arab, tg-cyrl, tg-cyrl-tj, tg-latn |
| Tamil                 | ta, ta-in                |
| Tatar                 | tt-arab, tt-cyrl, tt-latn, tt-ru |
| Telugu                | te, te-in                |
| Thai                  | th, th-th                |
| Tigrinya              | ti, ti-et                |
| Turkish               | tr, tr-tr                |
| Turkmen               | tk-cyrl, tk-latn, tk-tm, tk-latn-tr, tk-cyrl-tr |
| Ukrainian             | uk, uk-ua                |
| Urdu                  | ur, ur-pk                |
| Uyghur                | ug-arab, ug-cn, ug-cyrl, ug-latn |
| Uzbek (Latin)         | uz, uz-cyrl, uz-latn, uz-latn-uz |
| Vietnamese            | vi, vi-vn                |
| Welsh                 | cy, cy-gb                |
| Wolof                 | wo, wo-sn                |
| Yoruba                | yo-latn, yo-ng           |

**Architecture**<br>*Required*

You must select the architecture of the code contained in the package from one of the following values:

- x86
- x64
- neutral
- arm
- arm64

**Installer parameters**<br>*Required*

The Store will need to run your installer in silent mode. To support this, you need to provide the required switches, such as /s, specific to your installation package. This is not required if your installer supports silent mode but does not require switches.
