---
description: This article describes best practices for creating and displaying app settings.
title: Guidelines for app settings
ms.assetid: 2D765E90-3FA0-42F5-A5CB-BEDC14C3F60A
label: Guidelines
template: detail.hbs
ms.date: 09/24/2020
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---

# Guidelines for app settings

App settings are the user-customizable portions of your Windows app accessed through an app settings page. For example, a news reader app might let the user specify which news sources to display or how many columns to display on the screen, while a weather app could let the user choose between Celsius and Fahrenheit. This article provides recommendations and best practices for creating and displaying app settings.

## When to provide a settings page

Here are examples of app options that belong in an app settings page:

- Configuration options that affect the behavior of the app and don't require frequent readjustment, like choosing between Celsius or Fahrenheit as default units for temperature in a weather app, changing account settings for a mail app, settings for notifications, or accessibility options.
- Options that depend on the user's preferences, like music, sound effects, or color themes.
- App information that isn't accessed very often, such as privacy policy, help, app version, or copyright info.

Commands that are part of the typical app workflow (for example, changing the brush size in an art app) shouldn't be in a settings page. To learn more about command placement, see [Command design basics](../basics/commanding-basics.md).

## General recommendations

- Keep settings pages simple and make use of binary (on/off) controls. A [toggle switch](../controls/toggles.md) is usually the best control for a binary setting.
- For settings that let users choose one item from a set of up to 5 mutually exclusive, related options, use [radio buttons](../controls/radio-button.md).
- Create an entry point for all app settings in your app setting's page.
- Keep your settings simple. Define smart defaults and keep the number of settings to a minimum.
- When a user changes a setting, the app should immediately reflect the change.
- Don't include commands that are part of the common app workflow.

## Entry point

The way that users get to your app settings page should be based on your app's layout.

**Navigation pane**

For a nav pane layout, app settings should be the last item in the list of navigational choices and be pinned to the bottom:

![app settings entry point for nav pane](images/appsettings-nav-settings.png)

**Command bar**

If you're using a [command bar](../controls/command-bar.md) or tool bar, place the settings entry point as one of the last items in the "More" overflow menu. If greater discoverability for the settings entry point is important for your app, place the entry point directly on the command bar and not within the overflow.

![app settings entry point for command bar](../controls/images/appbar_rs2_overflow_icons.png)

**Hub**

If you're using a hub layout, the entry point for app settings should be placed inside the "More" overflow menu of a command bar.

**Tabs/pivots**

For a tabs or pivots layout, we don't recommend placing the app settings entry point as one of the top items within the navigation. Instead, the entry point for app settings should be placed inside the "More" overflow menu of a command bar.

**List-details**

Instead of burying the app settings entry point deeply within a list-details pane, make it the last pinned item on the top level of the list pane.

## Layout


The app settings window should open full-screen and fill the whole window. If your app settings menu has up to four top-level groups, those groups should cascade down one column.

![layout for app settings page on desktop](images/appsettings-layout-navpane-desktop.png)


## "Color mode" settings


If your app allows users to choose the app's color mode, present these options using [radio buttons](../controls/radio-button.md) or a [combo box](../controls/combo-box.md) with the header "Choose an app mode". The options should read
- Light
- Dark
- Windows default

We also recommend adding a hyperlink to the Colors page of the Windows Settings app where users can access and modify the current default app mode. Use the string "Windows color settings" for the hyperlink text and `ms-settings:colors` for the URI.

!["Choose a mode" section](images/appsettings_mode.png)

<!--
<div class="microsoft-internal-note">
Detailed redlines showing preferred text strings for the "Choose a mode" section are available on [UNI](https://uni/DesignDepot.FrontEnd/#/ProductNav/2543/0/dv/?t=Windows%7CControls%7CColorMode&f=RS2).
</div>
-->

## About section and Feedback button


We recommend placing  "About this App" section in your app either as a dedicated page or in its own section. If you want a "Send Feedback" button, place that toward the bottom of the "About this App" page.

Under a "Legal" subheader, place any "Terms of Use" and "Privacy Statement" (should be [hyperlink buttons](../controls/hyperlinks.md) with wrapping text) as well as additional legal information, such as copyright.

!["about this app" section with "give feedback" button](images/appsettings-about.png)


## Recommended page content


Once you have a list of items that you want to include in your app settings page, consider these guidelines:

- Group similar or related settings under one settings label.
- Try to keep the total number of settings to a maximum of four or five.
- Display the same settings regardless of the app context. If some settings aren't relevant in a certain context, disable those in the app settings flyout.
- Use descriptive, one-word labels for settings. For example, name the setting "Accounts" instead of "Account settings" for account-related settings. If you only want one option for your settings and the settings don't lend themselves to a descriptive label, use "Options" or "Defaults."
- If a setting directly links to the web instead of to a flyout, let the user know this with a visual clue, such as "Help (online)" or "Web forums" styled as a [hyperlink](../controls/hyperlinks.md). Consider grouping multiple links to the web into a flyout with a single setting. For example, an "About" setting could open a flyout with links to your terms of use, privacy policy, and app support.
- Combine less-used settings into a single entry so that more common settings can each have their own entry. Put content or links that only contain information in an "About" setting.
- Don't duplicate the functionality in the "Permissions" pane. Windows provides this pane by default and you can't modify it.

- Add settings content to Settings flyouts
- Present content from top to bottom in a single column, scrollable if necessary. Limit scrolling to a maximum of twice the screen height.
- Use the following controls for app settings:

    - [Toggle switches](../controls/toggles.md): To let users set values on or off.
    - [Radio buttons](../controls/radio-button.md): To let users choose one item from a set of up to 5 mutually exclusive, related options.
    - [Text input box](../controls/text-block.md): To let users enter text. Use the type of text input box that corresponds to the type of text you're getting from the user, such as an email or password.
    - [Hyperlinks](../controls/hyperlinks.md): To take the user to another page within the app or to an external website. When a user clicks a hyperlink, the Settings flyout will be dismissed.
    - [Buttons](../controls/buttons.md): To let users initiate an immediate action without dismissing the current Settings flyout.
- Add a descriptive message if one of the controls is disabled. Place this message above the disabled control.
- Animate content and controls as a single block after the Settings flyout and header have animated. Animate content using the [**enterPage**](/previous-versions/windows/apps/br212672(v=win.10)) or [**EntranceThemeTransition**](/uwp/api/Windows.UI.Xaml.Media.Animation.EntranceThemeTransition) animations with a 100px left offset.
- Use section headers, paragraphs, and labels to aid organize and clarify content, if necessary.
- If you need to repeat settings, use an additional level of UI or an expand/collapse model, but avoid hierarchies deeper than two levels. For example, a weather app that provides per-city settings could list the cities and let the user tap on the city to either open a new flyout or expand to show the settings options.
- If loading controls or web content takes time, use an indeterminate progress control to indicate to users that info is loading. For more info, see [Guidelines for progress controls](../controls/progress-controls.md).
- Don't use buttons for navigation or to commit changes. Use hyperlinks to navigate to other pages, and instead of using a button to commit changes, automatically save changes to app settings when a user dismisses the Settings flyout.



## Related articles

* [Command design basics](../basics/commanding-basics.md)
* [Guidelines for progress controls](../controls/progress-controls.md)
* [Store and retrieve app data](./store-and-retrieve-app-data.md)
* [**EntranceThemeTransition**](/uwp/api/Windows.UI.Xaml.Media.Animation.EntranceThemeTransition)
