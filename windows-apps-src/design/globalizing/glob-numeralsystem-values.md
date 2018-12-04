---
Description: This topic lists the values available to the NumeralSystem property of various classes in the Windows.Globalization namespace.
title: NumeralSystem values
template: detail.hbs
ms.date: 11/02/2017
ms.topic: article
keywords: windows 10, uwp, globalization, localization
ms.localizationpriority: medium
---
# NumeralSystem values

This topic lists the values available to the **NumeralSystem** property of various classes in the [**Windows.Globalization**](/uwp/api/windows.globalization?branch=live) namespace. These classes contain a **NumeralSystem** property.

- [**Windows.Globalization.Calendar**](/uwp/api/windows.globalization.calendar?branch=live)
- [**Windows.Globalization.NumberFormatting.CurrencyFormatter**](/uwp/api/windows.globalization.numberformatting.currencyformatter?branch=live)
- [**Windows.Globalization.NumberFormatting.DecimalFormatter**](/uwp/api/windows.globalization.numberformatting.decimalformatter?branch=live)
- [**Windows.Globalization.NumberFormatting.NumeralSystemTranslator**](/uwp/api/windows.globalization.numberformatting.numeralsystemtranslator?branch=live)
- [**Windows.Globalization.NumberFormatting.PercentFormatter**](/uwp/api/windows.globalization.numberformatting.percentformatter?branch=live)
- [**Windows.Globalization.NumberFormatting.PermilleFormatter**](/uwp/api/windows.globalization.numberformatting.permilleformatter?branch=live)
- [**Windows.Globalization.NumberFormatting.INumberFormatterOptions**](/uwp/api/windows.globalization.numberformatting.inumberformatteroptions?branch=live)

The **NumeralSystem** property indicates the numeral system used by the class. The property is a string such as "Latn" for the Latin numeral system (0123456789), or "Arab" for the Arabic-Indic numeral system (٠١٢٣٤٥٦٧٨٩).

These are the values for **NumeralSystem** that are supported in Windows.

| NumeralSystem value | Description | Unicode code points |
| -------------------- | ----------- | ------------------- |
| Arab | Arabic-Indic | U+0660, U+0661, U+0662, U+0663, U+0664, U+0665, U+0666, U+0667, U+0668, U+0669 |
| ArabExt | Extended Arabic-Indic | U+06F0, U+06F1, U+06F2, U+06F3, U+06F4, U+06F5, U+06F6, U+06F7, U+06F8, U+06F9 |
| Bali | Balinese | U+1B50, U+1B51, U+1B52, U+1B53, U+1B54, U+1B55, U+1B56, U+1B57, U+1B58, U+1B59 |
| Beng | Bengali | U+09E6, U+09E7, U+09E8, U+09E9, U+09EA, U+09EB, U+09EC, U+09ED, U+09EE, U+09EF |
| Cham | Cham | U+AA50, U+AA51, U+AA52, U+AA53, U+AA54, U+AA55, U+AA56, U+AA57, U+AA58, U+AA59 |
| Deva | Devanagari | U+0966, U+0967, U+0968, U+0969, U+096A, U+096B, U+096C, U+096D, U+096E, U+096F |
| FullWide | Full width | U+FF10, U+FF11, U+FF12, U+FF13, U+FF14, U+FF15, U+FF16, U+FF17, U+FF18, U+FF19 |
| Gujr | Gujarati | U+0AE6, U+0AE7, U+0AE8, U+0AE9, U+0AEA, U+0AEB, U+0AEC, U+0AED, U+0AEE, U+0AEF |
| Guru | Gurmukhi | U+0A66, U+0A67, U+0A68, U+0A69, U+0A6A, U+0A6B, U+0A6C, U+0A6D, U+0A6E, U+0A6F |
| Java | Javanese | U+A9D0, U+A9D1, U+A9D2, U+A9D3, U+A9D4, U+A9D5, U+A9D6, U+A9D7, U+A9D8, U+A9D9 |
| Kali | Kayah Li | U+A900, U+A901, U+A902, U+A903, U+A904, U+A905, U+A906, U+A907, U+A908, U+A909 |
| Khmr | Khmer | U+17E0, U+17E1, U+17E2, U+17E3, U+17E4, U+17E5, U+17E6, U+17E7, U+17E8, U+17E9 |
| Knda | Kannada | U+0CE6, U+0CE7, U+0CE8, U+0CE9, U+0CEA, U+0CEB, U+0CEC, U+0CED, U+0CEE, U+0CEF |
| Lana | Tai Tham Hora | U+1A80, U+1A81, U+1A82, U+1A83, U+1A84, U+1A85, U+1A86, U+1A87, U+1A88, U+1A89 |
| LanaTham | Tai Tham Tham | U+1A90, U+1A91, U+1A92, U+1A93, U+1A94, U+1A95, U+1A96, U+1A97, U+1A98, U+1A99 |
| Laoo | Lao | U+0ED0, U+0ED1, U+0ED2, U+0ED3, U+0ED4, U+0ED5, U+0ED6, U+0ED7, U+0ED8, U+0ED9 |
| Latn | Latin | U+0030, U+0031, U+0032, U+0033, U+0034, U+0035, U+0036, U+0037, U+0038, U+0039 |
| Lepc | Lepcha | U+1C40, U+1C41, U+1C42, U+1C43, U+1C44, U+1C45, U+1C46, U+1C47, U+1C48, U+1C49 |
| Limb | Limbu | U+1946, U+1947, U+1948, U+1949, U+194A, U+194B, U+194C, U+194D, U+194E, U+194F |
| Mlym | Malayalam | U+0D66, U+0D67, U+0D68, U+0D69, U+0D6A, U+0D6B, U+0D6C, U+0D6D, U+0D6E, U+0D6F |
| Mong | Mongolian | U+1810, U+1811, U+1812, U+1813, U+1814, U+1815, U+1816, U+1817, U+1818, U+1819 |
| Mtei | Meetei Mayek | U+ABF0, U+ABF1, U+ABF2, U+ABF3, U+ABF4, U+ABF5, U+ABF6, U+ABF7, U+ABF8, U+ABF9 |
| Mymr | Myanmar | U+1040, U+1041, U+1042, U+1043, U+1044, U+1045, U+1046, U+1047, U+1048, U+1049 |
| MymrShan | Myanmar Shan | U+1090, U+1091, U+1092, U+1093, U+1094, U+1095, U+1096, U+1097, U+1098, U+1099 |
| Nkoo | Nko | U+07C0, U+07C1, U+07C2, U+07C3, U+07C4, U+07C5, U+07C6, U+07C7, U+07C8, U+07C9 |
| Olck | Ol Chiki | U+1C50, U+1C51, U+1C52, U+1C53, U+1C54, U+1C55, U+1C56, U+1C57, U+1C58, U+1C59 |
| Orya | Odia | U+0B66, U+0B67, U+0B68, U+0B69, U+0B6A, U+0B6B, U+0B6C, U+0B6D, U+0B6E, U+0B6F |
| Saur | Saurashtra | U+A8D0, U+A8D1, U+A8D2, U+A8D3, U+A8D4, U+A8D5, U+A8D6, U+A8D7, U+A8D8, U+A8D9 |
| Sund | Sundanese | U+1BB0, U+1BB1, U+1BB2, U+1BB3, U+1BB4, U+1BB5, U+1BB6, U+1BB7, U+1BB8, U+1BB9 |
| Talu | New Tai Lue | U+19D0, U+19D1, U+19D2, U+19D3, U+19D4, U+19D5, U+19D6, U+19D7, U+19D8, U+19D9 |
| TamlDec | Tamil | U+0BE6, U+0BE7, U+0BE8, U+0BE9, U+0BEA, U+0BEB, U+0BEC, U+0BED, U+0BEE, U+0BEF |
| Telu | Telugu | U+0C66, U+0C67, U+0C68, U+0C69, U+0C6A, U+0C6B, U+0C6C, U+0C6D, U+0C6E, U+0C6F |
| Thai | Thai | U+0E50, U+0E51, U+0E52, U+0E53, U+0E54, U+0E55, U+0E56, U+0E57, U+0E58, U+0E59 |
| Tibt | Tibetan | U+0F20, U+0F21, U+0F22, U+0F23, U+0F24, U+0F25, U+0F26, U+0F27, U+0F28, U+0F29 |
| Vaii | Vai | U+A620, U+A621, U+A622, U+A623, U+A624, U+A625, U+A626, U+A627, U+A628, U+A629 |
