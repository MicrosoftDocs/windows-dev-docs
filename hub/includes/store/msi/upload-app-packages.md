The Packages page of the app submission process is where you provide the packages (MSI/EXE) and associated information for the app that you're submitting. When a customer downloads your app, the Store will automatically provide each customer with the package that works best for their device.

:::image type="content" source="images/msiexe-packages-overview.png" lightbox="images/msiexe-packages-overview.png" alt-text="A screenshot of the overview of Packages section in Partner Center.":::

You must complete the Packages page for at least one package. To add package, click Add package from Packages page.

:::image type="content" source="images/msiexe-package-details-overview.png" lightbox="images/msiexe-package-details-overview.png" alt-text="A screenshot of the Packages section showing the overview of package details.":::

## Add and edit Package info

To edit Package info, select the Package from the Packages page. You must edit each package separately.

**Package URL**<br>*Required*

You must enter at least one versioned secure URL pointing to app package (MSI/EXE) hosted on your CDN. An example of versioned secure URL is https://www.contoso.com/downloads/1.1/setup.exe. When customer installs your app from the Store, the Store downloads the package from this URL. You need to follow good CDN practices and ensure that this URL is performant, reliable, and available based on your market selection.

:::image type="content" source="images/msiexe-package-url.png" lightbox="images/msiexe-package-url.png" alt-text="A screenshot of the Packages section where you can provide your package URL details.":::

If you need to update the package URL, you may use the Update submission option in Partner Center to specify a new package URL.

The binary on the package URL must not change after it is submitted to ensure only certified binaries are installed by users. The Store will retain copies of your most recent app packages to distribute in case the app installer hosted by you on a separate hosting service, such as a content delivery network (CDN), is swapped with new app installer packages without submission through Partner Center or API. The Store will also download the new app packages and initiate the process of certification. If the updates pass certification tests, the Store makes them available for end users. If the updates fail certification tests, the Stores notifies you to submit the updates through Partner Center or API.

You must submit a standalone/offline installer and not a downloader that downloads binaries when invoked. This is required to certify the binaries that get installed are the same ones that passed the certification process.

**Architecture**<br>*Required*

You must select the architecture of the code contained in the package from one of the following values:

- x86
- x64
- neutral
- arm
- arm64

:::image type="content" source="images/msiexe-set-architecture.png" lightbox="images/msiexe-set-architecture.png" alt-text="A screenshot of the Packages section where you can provide your app architecture details.":::

If you have packages compiled in more than 1 architecture, you should add them to the submission.

**Languages**<br>*Required*

:::image type="content" source="images/msiexe-package-select-languages.png" lightbox="images/msiexe-package-select-languages.png" alt-text="A screenshot of the Packages section where you can provide the languages your app supports.":::

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

**App type**<br>*Required*

Select your app type – (EXE/MSI). If you choose EXE, you are required to provide Installer parameters and details for Installer handling.

:::image type="content" source="images/msiexe-packages-set-app-type.png" lightbox="images/msiexe-packages-set-app-type.png" alt-text="A screenshot of the Packages section where you can provide the type (msi/exe) of your app.":::

:::image type="content" source="images/msiexe-packages-exe-overview.png" lightbox="images/msiexe-packages-exe-overview.png" alt-text="A screenshot of the Packages section showing the the additional fields required for exe type of apps.":::

**Installer parameters**<br>*Required*

The Store will need to run your installer in silent mode. To support this, you need to provide the required switches, such as /s, specific to installer for your EXE app. This is not required if your installer runs in silent mode by default, without any switches.

:::image type="content" source="images/msiexe-set-installer-parameters.png" lightbox="images/msiexe-set-installer-parameters.png" alt-text="A screenshot of the Packages section where you can provide the installer parameters for your app.":::

For MSI apps, the Store uses the default silent switch ‘/qn’ to run your installer in silent mode.

**Installer handling for your EXE app**<br>*Required*

:::image type="content" source="images/msiexe-installer-handling.png" lightbox="images/msiexe-installer-handling.png" alt-text="A screenshot of the section of the Partner Center package details page where you can specify which return codes correspond to which installer outcomes.":::

EXE apps usually have installers that return custom codes during installation. The Store supports suitable customer facing messages and actions for the custom return codes provided by you.

The following are the standard install scenarios supported by the Store:

| Scenario                             | Description |
|--------------------------------------|-------------|
| Installation cancelled by user       | The install operation was cancelled by the user. |
| Application already exists           | The application already exists on the device. |
| Installation already in progress     | Another installation is already in progress. User needs to complete the installation before proceeding with this install. |
| Disk space is full                   | The disk space is full. |
| Reboot required                      | A restart is required to complete the install. |
| Network failure                      | Provide custom return code values for various network related failures. |
| Package rejected during installation | Package rejected during installation due to a security policy enabled on the device. |
| Installation successful              | Installation has been successful. |

You can add more than 1 return code for each of the above scenarios depending on your installer behavior.

For scenarios beyond the above list of standard scenarios, customers are directed to your installer return code documentation. For miscellaneous install failure scenarios, you can add your custom return codes along with return code specific documentation URL that Store can point customers to.

We highly recommend this information to be provided for EXE apps so the Store can provide tailored experience to customers. This will also help the Store to treat and report your app installs for EXE apps.

After adding the package, click on Save draft. You will be back on Manage Packages page. In the List of packages, you will see your package has been added. After verifying that your package has been added in the List of packages, click on Save All. You will see a message that your package is uploading and after a successful upload, you will get a message as Saved Successfully.
