---
title: Rich Presence policies and limitations
author: KevinAsgari
description: Learn about policies and limitations of the Xbox Live Rich Presence system.
ms.assetid: 0ad21a75-0524-45a8-8d8a-0dec0f7d6d6f
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, rich presence, policies
ms.localizationpriority: low
---

# Rich Presence policies and limitations

When you implement Rich Presence for your title, you must adhere to the following policies and limits.

-   Each title must have at least 1 string-set, but there is no upper limit on how many strings-sets you can have.
-   You must define a default string as well as culture neutral strings for each enumeration and for each Rich Presence string.
-   You can use numerical or string statistics to fill in the parameters in your strings. You can't use DateTime statistics.
-   If you're using statistics in your Rich Presence strings, then those statistics (including any enumerations for statistics) must be available in the same SCID & Sandbox.
-   You have 1 line of 44 characters total (including the values of the parameters). This is similar to Xbox 360 Rich Presence limits. We will be working with the client to see if the length of the string can grow. There will be an announcement if the string can be longer.
    -   Unicode characters are required and must be able to work with UTF-8 encoding for display.
-   Your friendly names must follow these rules:
    -   The allowed characters are 'A' through 'Z', 'a' through 'z', underscore ('\_'), and '0' through '9'.

        There is no character limit.

-   No string verification is done on your strings; you must do any string verification, such as spellcheck and verifying that the string has been localized properly.
    -   If we feel a string-set is inappropriate (such as abusive or offensive language), Microsoft prevents titles from using Rich Presence until strings have been updated to our satisfaction.
-   If your title isn't integrating with the data platform, there are no options for using statistics as parameters in your strings.
    -   All strings must be completely predefined in that case (no tokens are allowed).
-   Enumeration names must be unique across all enumerations and must be unique to statistic names.
-   If a line exceeds the number of characters that can be shown, and there is a line break, the line is automatically truncated.
