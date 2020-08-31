---
Description: The following article describes all of the properties and elements within toast content.
title: Toast content schema
ms.assetid: 7CBC3BD5-D9C3-4781-8BD0-1F28039E1FA8
label: Toast content schema
template: detail.hbs
ms.date: 05/19/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Toast content schema

 

The following describes all of the properties and elements within toast content.

If you would rather use raw XML instead of the [Notifications library](https://www.nuget.org/packages/Microsoft.Toolkit.Uwp.Notifications/), please see [the XML schema](toast-xml-schema.md).

[ToastContent](#toastcontent)
* [ToastVisual](#toastvisual)
  * [ToastBindingGeneric](#toastbindinggeneric)
    * [IToastBindingGenericChild](#itoastbindinggenericchild)
    * [ToastGenericAppLogo](#toastgenericapplogo)
    * [ToastGenericHeroImage](#toastgenericheroimage)
    * [ToastGenericAttributionText](#toastgenericattributiontext)
* [IToastActions](#itoastactions)
* [ToastAudio](#toastaudio)
* [ToastHeader](#toastheader)


## ToastContent
ToastContent is the top level object that describes a notification's content, including visuals, actions, and audio.

| Property | Type | Required | Description |
|---|---|---|---|
| **Launch**| string | false | A string that is passed to the application when it is activated by the Toast. The format and contents of this string are defined by the app for its own use. When the user taps or clicks the Toast to launch its associated app, the launch string provides the context to the app that allows it to show the user a view relevant to the Toast content, rather than launching in its default way. |
| **Visual** | [ToastVisual](#toastvisual) | true | Describes the visual portion of the toast notification. |
| **Actions** | [IToastActions](#itoastactions) | false | Optionally create custom actions with buttons and inputs. |
| **Audio** | [ToastAudio](#toastaudio) | false | Describes the audio portion of the toast notification. |
| **ActivationType** | [ToastActivationType](#toastactivationtype) | false | Specifies what activation type will be used when the user clicks the body of this Toast. |
| **ActivationOptions** | [ToastActivationOptions](#toastactivationoptions) | false | New in Creators Update: Additional options relating to activation of the toast notification. |
| **Scenario** | [ToastScenario](#toastscenario) | false | Declares the scenario your toast is used for, like an alarm or reminder. |
| **DisplayTimestamp** | DateTimeOffset? | false | New in Creators Update: Override the default timestamp with a custom timestamp representing when your notification content was actually delivered, rather than the time the notification was received by the Windows platform. |
| **Header** | [ToastHeader](#toastheader) | false | New in Creators Update: Add a custom header to your notification to group multiple notifications together within Action Center. |


### ToastScenario
Specifies what scenario the toast represents.

| Value | Meaning |
|---|---|
| **Default** | The normal toast behavior. |
| **Reminder** | A reminder notification. This will be displayed pre-expanded and stay on the user's screen till dismissed. |
| **Alarm** | An alarm notification. This will be displayed pre-expanded and stay on the user's screen till dismissed. Audio will loop by default and will use alarm audio. |
| **IncomingCall** | An incoming call notification. This will be displayed pre-expanded in a special call format and stay on the user's screen till dismissed. Audio will loop by default and will use ringtone audio. |


## ToastVisual
The visual portion of toasts contains the bindings, which contains text, images, adaptive content, and more.

| Property | Type | Required | Description |
|---|---|---|---|
| **BindingGeneric** | [ToastBindingGeneric](#toastbindinggeneric) | true | The generic toast binding, which can be rendered on all devices. This binding is required and cannot be null. |
| **BaseUri** | Uri | false | A default base URL that is combined with relative URLs in image source attributes. |
| **AddImageQuery** | bool? | false | Set to "true" to allow Windows to append a query string to the image URL supplied in the toast notification. Use this attribute if your server hosts images and can handle query strings, either by retrieving an image variant based on the query strings or by ignoring the query string and returning the image as specified without the query string. This query string specifies scale, contrast setting, and language; for instance, a value of "www.website.com/images/hello.png" given in the notification becomes "www.website.com/images/hello.png?ms-scale=100&ms-contrast=standard&ms-lang=en-us" |
| **Language**| string | false | The target locale of the visual payload when using localized resources, specified as BCP-47 language tags such as "en-US" or "fr-FR". This locale is overridden by any locale specified in binding or text. If not provided, the system locale will be used instead. |


## ToastBindingGeneric
The generic binding is the default binding for toasts, and is where you specify the text, images, adaptive content, and more.

| Property | Type | Required | Description |
|---|---|---|---|
| **Children** | IList<[IToastBindingGenericChild](#itoastbindinggenericchild)> | false | The contents of the body of the Toast, which can include text, images, and groups (added in Anniversary Update). Text elements must come before any other elements, and only 3 text elements are supported. If a text element is placed after any other element, it will either be pulled to the top or dropped. And finally, certain text properties like HintStyle aren't supported on the root children text elements, and only work inside an AdaptiveSubgroup. If you use AdaptiveGroup on devices without the Anniversary Update, the group content will simply be dropped. |
| **AppLogoOverride** | [ToastGenericAppLogo](#toastgenericapplogo) | false | An optional logo to override the app logo. |
| **HeroImage** | [ToastGenericHeroImage](#toastgenericheroimage) | false | An optional featured "hero" image that is displayed on the toast and within Action Center. |
| **Attribution** | [ToastGenericAttributionText](#toastgenericattributiontext) | false | Optional attribution text which will be displayed at the bottom of the toast notification. |
| **BaseUri** | Uri | false | A default base URL that is combined with relative URLs in image source attributes. |
| **AddImageQuery** | bool? | false | Set to "true" to allow Windows to append a query string to the image URL supplied in the toast notification. Use this attribute if your server hosts images and can handle query strings, either by retrieving an image variant based on the query strings or by ignoring the query string and returning the image as specified without the query string. This query string specifies scale, contrast setting, and language; for instance, a value of "www.website.com/images/hello.png" given in the notification becomes "www.website.com/images/hello.png?ms-scale=100&ms-contrast=standard&ms-lang=en-us" |
| **Language**| string | false | The target locale of the visual payload when using localized resources, specified as BCP-47 language tags such as "en-US" or "fr-FR". This locale is overridden by any locale specified in binding or text. If not provided, the system locale will be used instead. |


## IToastBindingGenericChild
Marker interface for toast child elements that include text, images, groups, and more.

| Implementations |
| --- |
| [AdaptiveText](#adaptivetext) |
| [AdaptiveImage](#adaptiveimage) |
| [AdaptiveGroup](#adaptivegroup) |
| [AdaptiveProgressBar](#adaptiveprogressbar) |


## AdaptiveText
An adaptive text element. If placed in the top level ToastBindingGeneric.Children, only HintMaxLines will be applied. But if this is placed as a child of a group/subgroup, full text styling is supported.

| Property | Type | Required |Description |
|---|---|---|---|
| **Text** | string or [BindableString](#bindablestring) | false | The text to display. Data binding support added in Creators Update, but only works for top-level text elements. |
| **HintStyle** | [AdaptiveTextStyle](#adaptivetextstyle) | false | The style controls the text's font size, weight, and opacity. Only works for text elements inside a group/subgroup. |
| **HintWrap** | bool? | false | Set this to true to enable text wrapping. Top-level text elements ignore this property and always wrap (you can use HintMaxLines = 1 to disable wrapping for top-level text elements). Text elements inside groups/subgroups default to false for wrapping. |
| **HintMaxLines** | int? | false | The maximum number of lines the text element is allowed to display. |
| **HintMinLines** | int? | false | The minimum number of lines the text element must display. Only works for text elements inside a group/subgroup. |
| **HintAlign** | [AdaptiveTextAlign](#adaptivetextalign) | false | The horizontal alignment of the text. Only works for text elements inside a group/subgroup. |
| **Language** | string | false | The target locale of the XML payload, specified as a BCP-47 language tags such as "en-US" or "fr-FR". The locale specified here overrides any other specified locale, such as that in binding or visual. If this value is a literal string, this attribute defaults to the user's UI language. If this value is a string reference, this attribute defaults to the locale chosen by Windows Runtime in resolving the string. |


### BindableString
A binding value for strings.

| Property | Type | Required | Description |
|---|---|---|---|
| **BindingName** | string | true | Gets or sets the name that maps to your binding data value. |


### AdaptiveTextStyle
Text style controls font size, weight, and opacity. Subtle opacity is 60% opaque.

| Value | Meaning |
|---|---|
| **Default** | Default value. Style is determined by the renderer. |
| **Caption** | Smaller than paragraph font size. |
| **CaptionSubtle** | Same as Caption but with subtle opacity. |
| **Body** | Paragraph font size. |
| **BodySubtle** | Same as Body but with subtle opacity. |
| **Base** | Paragraph font size, bold weight. Essentially the bold version of Body. |
| **BaseSubtle** | Same as Base but with subtle opacity. |
| **Subtitle** | H4 font size. |
| **SubtitleSubtle** | Same as Subtitle but with subtle opacity. |
| **Title** | H3 font size. |
| **TitleSubtle** | Same as Title but with subtle opacity. |
| **TitleNumeral** | Same as Title but with top/bottom padding removed. |
| **Subheader** | H2 font size. |
| **SubheaderSubtle** | Same as Subheader but with subtle opacity. |
| **SubheaderNumeral** | Same as Subheader but with top/bottom padding removed. |
| **Header** | H1 font size. |
| **HeaderSubtle** | Same as Header but with subtle opacity. |
| **HeaderNumeral** | Same as Header but with top/bottom padding removed. |


### AdaptiveTextAlign
Controls the horizontal alignmen of text.

| Value | Meaning |
|---|---|
| **Default** | Default value. Alignment is automatically determined by the renderer. |
| **Auto** | Alignment determined by the current language and culture. |
| **Left** | Horizontally align the text to the left. |
| **Center** | Horizontally align the text in the center. |
| **Right** | Horizontally align the text to the right. |


## AdaptiveImage
An inline image.

| Property | Type | Required |Description |
|---|---|---|---|
| **Source** | string | true | The URL to the image. ms-appx, ms-appdata, and http are supported. As of the Fall Creators Update, web images can be up to 3 MB on normal connections and 1 MB on metered connections. On devices not yet running the Fall Creators Update, web images must be no larger than 200 KB. |
| **HintCrop** | [AdaptiveImageCrop](#adaptiveimagecrop) | false | New in Anniversary Update: Control the desired cropping of the image. |
| **HintRemoveMargin** | bool? | false | By default, images inside groups/subgroups have an 8px margin around them. You can remove this margin by setting this property to true. |
| **HintAlign** | [AdaptiveImageAlign](#adaptiveimagealign) | false | The horizontal alignment of the image. Only works for images inside a group/subgroup. |
| **AlternateText** | string | false | Alternate text describing the image, used for accessibility purposes. |
| **AddImageQuery** | bool? | false | Set to "true" to allow Windows to append a query string to the image URL supplied in the toast notification. Use this attribute if your server hosts images and can handle query strings, either by retrieving an image variant based on the query strings or by ignoring the query string and returning the image as specified without the query string. This query string specifies scale, contrast setting, and language; for instance, a value of "www.website.com/images/hello.png" given in the notification becomes "www.website.com/images/hello.png?ms-scale=100&ms-contrast=standard&ms-lang=en-us" |


### AdaptiveImageCrop
Specifies the desired cropping of the image.

| Value | Meaning |
|---|---|
| **Default** | Default value. Cropping behavior determined by renderer. |
| **None** | Image is not cropped. |
| **Circle** | Image is cropped to a circle shape. |


### AdaptiveImageAlign
Specifies the horizontal alignment for an image.

| Value | Meaning |
|---|---|
| **Default** | Default value. Alignment behavior determined by renderer. |
| **Stretch** | Image stretches to fill available width (and potentially available height too, depending on where the image is placed). |
| **Left** | Align the image to the left, displaying the image at its native resolution. |
| **Center** | Align the image in the center horizontally, displayign the image at its native resolution. |
| **Right** | Align the image to the right, displaying the image at its native resolution. |


## AdaptiveGroup
New in Anniversary Update: Groups semantically identify that the content in the group must either be displayed as a whole, or not displayed if it cannot fit. Groups also allow creating multiple columns.

| Property | Type | Required |Description |
|---|---|---|---|
| **Children** | IList<[AdaptiveSubgroup](#adaptivesubgroup)> | false | Subgroups are displayed as vertical columns. You must use subgroups to provide any content inside an AdaptiveGroup. |


## AdaptiveSubgroup
New in Anniversary Update: Subgroups are vertical columns that can contain text and images.

| Property | Type | Required |Description |
|---|---|---|---|
| **Children** | IList<[IAdaptiveSubgroupChild](#iadaptivesubgroupchild)> | false | [AdaptiveText](#adaptivetext) and [AdaptiveImage](#adaptiveimage) are valid children of subgroups. |
| **HintWeight** | int? | false | Control the width of this subgroup column by specifying the weight, relative to the other subgroups. |
| **HintTextStacking** | [AdaptiveSubgroupTextStacking](#adaptivesubgrouptextstacking) | false | Control the vertical alignment of this subgroup's content. |


### IAdaptiveSubgroupChild
Marker interface for subgroup children.

| Implementations |
| --- |
| [AdaptiveText](#adaptivetext) |
| [AdaptiveImage](#adaptiveimage) |


### AdaptiveSubgroupTextStacking
TextStacking specifies the vertical alignment of content.

| Value | Meaning |
|---|---|
| **Default** | Default value. Renderer automatically selects the default vertical alignment. |
| **Top** | Vertical align to the top. |
| **Center** | Vertical align to the center. |
| **Bottom** | Vertical align to the bottom. |


## AdaptiveProgressBar
New in Creators Update: A progress bar. Only supported on toasts on Desktop, build 15063 or newer.

| Property | Type | Required | Description |
|---|---|---|---|
| **Title** | string or [BindableString](#bindablestring) | false | Gets or sets an optional title string. Supports data binding. |
| **Value** | double or [AdaptiveProgressBarValue](#adaptiveprogressbarvalue) or [BindableProgressBarValue](#bindableprogressbarvalue) | false | Gets or sets the value of the progress bar. Supports data binding. Defaults to 0. |
| **ValueStringOverride** | string or [BindableString](#bindablestring) | false | Gets or sets an optional string to be displayed instead of the default percentage string. If this isn't provided, something like "70%" will be displayed. |
| **Status** | string or [BindableString](#bindablestring) | true | Gets or sets a status string (required), which is displayed underneath the progress bar on the left. This string should reflect the status of the operation, like "Downloading..." or "Installing..." |


### AdaptiveProgressBarValue
A class that represents the progress bar's value.

| Property | Type | Required | Description |
|---|---|---|---|
| **Value** | double | false | Gets or sets the value (0.0 - 1.0) representing the percent complete. |
| **IsIndeterminate** | bool | false | Gets or sets a value indicating whether the progress bar is indeterminate. If this is true, **Value** will be ignored. |


### BindableProgressBarValue
A bindable progress bar value.

| Property | Type | Required | Description |
|---|---|---|---|
| **BindingName** | string | true | Gets or sets the name that maps to your binding data value. |


## ToastGenericAppLogo
A logo to be displayed instead of the app logo.

| Property | Type | Required |Description |
|---|---|---|---|
| **Source** | string | true | The URL to the image. ms-appx, ms-appdata, and http are supported. Http images must be 200 KB or less in size. |
| **HintCrop** | [ToastGenericAppLogoCrop](#toastgenericapplogocrop) | false | Specify how you would like the image to be cropped. |
| **AlternateText** | string | false | Alternate text describing the image, used for accessibility purposes. |
| **AddImageQuery** | bool? | false | Set to "true" to allow Windows to append a query string to the image URL supplied in the toast notification. Use this attribute if your server hosts images and can handle query strings, either by retrieving an image variant based on the query strings or by ignoring the query string and returning the image as specified without the query string. This query string specifies scale, contrast setting, and language; for instance, a value of "www.website.com/images/hello.png" given in the notification becomes "www.website.com/images/hello.png?ms-scale=100&ms-contrast=standard&ms-lang=en-us" |


### ToastGenericAppLogoCrop
Controls the cropping of the app logo image.

| Value | Meaning |
|---|---|
| **Default** | Cropping uses the default behavior of the renderer. |
| **None** | Image is not cropped, displayed square. |
| **Circle** | Image is cropped to a circle. |


## ToastGenericHeroImage
A featured "hero" image that is displayed on the toast and within Action Center.

| Property | Type | Required |Description |
|---|---|---|---|
| **Source** | string | true | The URL to the image. ms-appx, ms-appdata, and http are supported. Http images must be 200 KB or less in size. |
| **AlternateText** | string | false | Alternate text describing the image, used for accessibility purposes. |
| **AddImageQuery** | bool? | false | Set to "true" to allow Windows to append a query string to the image URL supplied in the toast notification. Use this attribute if your server hosts images and can handle query strings, either by retrieving an image variant based on the query strings or by ignoring the query string and returning the image as specified without the query string. This query string specifies scale, contrast setting, and language; for instance, a value of "www.website.com/images/hello.png" given in the notification becomes "www.website.com/images/hello.png?ms-scale=100&ms-contrast=standard&ms-lang=en-us" |


## ToastGenericAttributionText
Attribution text displayed at the bottom of the toast notification.

| Property | Type | Required | Description |
|---|---|---|---|
| **Text** | string | true | The text to display. |
| **Language** | string | false | The target locale of the visual payload when using localized resources, specified as BCP-47 language tags such as "en-US" or "fr-FR". If not provided, the system locale will be used instead. |


## IToastActions
Marker interface for toast actions/inputs.

| Implementations |
| --- |
| [ToastActionsCustom](#toastactionscustom) |
| [ToastActionsSnoozeAndDismiss](#toastactionssnoozeanddismiss) |


## ToastActionsCustom
*Implements [IToastActions](#itoastactions)*

Create your own custom actions and inputs, using controls like buttons, text boxes, and selection inputs.

| Property | Type | Required | Description |
|---|---|---|---|
| **Inputs** | IList<[IToastInput](#itoastinput)> | false | Inputs like text boxes and selection inputs. Only up to 5 inputs are allowed. |
| **Buttons** | IList<[IToastButton](#itoastbutton)> | false | Buttons are displayed after all the inputs (or adjacent to an input if the button is used as a quick reply button). Only up to 5 buttons are allowed (or fewer if you also have context menu items). |
| **ContextMenuItems** | IList<[ToastContextMenuItem](#toastcontextmenuitem)> | false | New in Anniversary Update: Custom context menu items, providing additional actions if the user right clicks the notification. You can only have up to 5 buttons and context menu items *combined*. |


## IToastInput
Marker interface for toast inputs.

| Implementations |
| --- |
| [ToastTextBox](#toasttextbox) |
| [ToastSelectionBox](#toastselectionbox) |


## ToastTextBox
*Implements [IToastInput](#itoastinput)*

A text box control that the user can type text into.

| Property | Type | Required | Description |
|---|---|---|---|
| **Id** | string | true | The Id is required, and is used to map the user-inputted text into a key-value pair of id/value which your app later consumes. |
| **Title** | string | false | Title text to display above the text box. |
| **PlaceholderContent** | string | false | Placeholder text to be displayed on the text box when the user hasn't typed any text yet. |
| **DefaultInput** | string | false | The initial text to place in the text box. Leave this null for a blank text box. |


## ToastSelectionBox
*Implements [IToastInput](#itoastinput)*

A selection box control, which lets users pick from a dropdown list of options.

| Property | Type | Required | Description |
|---|---|---|---|
| **Id** | string | true | The Id is required. If the user selected this item, this Id will be passed back to your app's code, representing which selection they chose. |
| **Content** | string | true | Content is required, and is a string that is displayed on the selection item. |


### ToastSelectionBoxItem
A selection box item (an item that the user can select from the drop down list).

| Property | Type | Required | Description |
|---|---|---|---|
| **Id** | string | true | The Id is required, and is used to map the user-inputted text into a key-value pair of id/value which your app later consumes. |
| **Title** | string | false | Title text to display above the selection box. |
| **DefaultSelectionBoxItemId** | string | false | This controls which item is selected by default, and refers to the Id property of the [ToastSelectionBoxItem](#toastselectionboxitem). If you do not provide this, the default selection will be empty (user sees nothing). |
| **Items** | IList<[ToastSelectionBoxItem](#toastselectionboxitem)> | false | The selection items that the user can pick from in this SelectionBox. Only 5 items can be added. |


## IToastButton
Marker interface for toast buttons.

| Implementations |
| --- |
| [ToastButton](#toastbutton) |
| [ToastButtonSnooze](#toastbuttonsnooze) |
| [ToastButtonDismiss](#toastbuttondismiss) |


## ToastButton
*Implements [IToastButton](#itoastbutton)*

A button that the user can click.

| Property | Type | Required | Description |
|---|---|---|---|
| **Content** | string | true | Required. The text to display on the button. |
| **Arguments** | string | true | Required. App-defined string of arguments that the app will later receive if the user clicks this button. |
| **ActivationType** | [ToastActivationType](#toastactivationtype) | false | Controls what type of activation this button will use when clicked. Defaults to Foreground. |
| **ActivationOptions** | [ToastActivationOptions](#toastactivationoptions) | false | New in Creators Update: Gets or sets additional options relating to activation of the toast button. |


### ToastActivationType
Decides the type of activation that will be used when the user interacts with a specific action.

| Value | Meaning |
|---|---|
| **Foreground** | Default value. Your foreground app is launched. |
| **Background** | Your corresponding background task (assuming you set everything up) is triggered, and you can execute code in the background (like sending the user's quick reply message) without interrupting the user. |
| **Protocol** | Launch a different app using protocol activation. |


### ToastActivationOptions
New in Creators Update: Additional options relating to activation.

| Property | Type | Required | Description |
|---|---|---|---|
| **AfterActivationBehavior** | [ToastAfterActivationBehavior](#toastafteractivationbehavior) | false | New in Fall Creators Update: Gets or sets the behavior that the toast should use when the user invokes this action. This only works on Desktop, for [ToastButton](#toastbutton) and [ToastContextMenuItem](#toastcontextmenuitem). |
| **ProtocolActivationTargetApplicationPfn** | string | false | If you are using *ToastActivationType.Protocol*, you can optionally specify the target PFN, so that regardless of whether multiple apps are registered to handle the same protocol uri, your desired app will always be launched. |


### ToastAfterActivationBehavior
Specifies the behavior that the toast should use when the user takes action on the toast.

| Value | Meaning |
|---|---|
| **Default** | Default behavior. The toast will be dismissed when the user takes action on the toast. |
| **PendingUpdate** | After the user clicks a button on your toast, the notification will remain present, in a "pending update" visual state. You should immediately update your toast from a background task so that the user does not see this "pending update" visual state for too long. |


## ToastButtonSnooze
*Implements [IToastButton](#itoastbutton)*

A system-handled snooze button that automatically handles snoozing of the notification.

| Property | Type | Required | Description |
|---|---|---|---|
| **CustomContent** | string | false | Optional custom text displayed on the button that overrides the default localized "Snooze" text. |


## ToastButtonDismiss
*Implements [IToastButton](#itoastbutton)*

A system-handled dismiss button that dismisses the notification when clicked.

| Property | Type | Required | Description |
|---|---|---|---|
| **CustomContent** | string | false | Optional custom text displayed on the button that overrides the default localized "Dismiss" text. |


## ToastActionsSnoozeAndDismiss
*Implements [IToastActions](#itoastactions)

Automatically constructs a selection box for snooze intervals, and snooze/dismiss buttons, all automatically localized, and snoozing logic is automatically handled by the system.

| Property | Type | Required | Description |
|---|---|---|---|
| **ContextMenuItems** | IList<[ToastContextMenuItem](#toastcontextmenuitem)> | false | New in Anniversary Update: Custom context menu items, providing additional actions if the user right clicks the notification. You can only have up to 5 items. |


## ToastContextMenuItem
A context menu item entry.

| Property | Type | Required | Description |
|---|---|---|---|
| **Content** | string | true | Required. The text to display. |
| **Arguments** | string | true | Required. App-defined string of arguments that the app can later retrieve once it is activated when the user clicks the menu item. |
| **ActivationType** | [ToastActivationType](#toastactivationtype) | false | Controls what type of activation this menu item will use when clicked. Defaults to Foreground. |
| **ActivationOptions** | [ToastActivationOptions](#toastactivationoptions) | false | New in Creators Update: Additional options relating to activation of the toast context menu item. |


## ToastAudio
Specify audio to be played when the Toast notification is received.

| Property | Type | Required | Description |
|---|---|---|---|
| **Src** | uri | false | The media file to play in place of the default sound. Only ms-appx and ms-appdata are supported. |
| **Loop** | boolean | false | Set to true if the sound should repeat as long as the Toast is shown; false to play only once (default). |
| **Silent** | boolean | false | True to mute the sound; false to allow the toast notification sound to play (default). |


## ToastHeader
New in Creators Update: A custom header that groups multiple notifications together within Action Center.

| Property | Type | Required | Description |
|---|---|---|---|
| **Id** | string | true | A developer-created identifier that uniquely identifies this header. If two notifications have the same header id, they will be displayed underneath the same header in Action Center. |
| **Title** | string | true | A title for the header. |
| **Arguments**| string | true | Gets or sets a developer-defined string of arguments that is returned to the app when the user clicks this header. Cannot be null. |
| **ActivationType** | [ToastActivationType](#toastactivationtype) | false | Gets or sets the type of activation this header will use when clicked. Defaults to Foreground. Note that only Foreground and Protocol are supported. |
| **ActivationOptions** | [ToastActivationOptions](#toastactivationoptions) | false | Gets or sets additional options relating to activation of the toast header. |


## Related topics

* [Quickstart: Send a local toast and handle activation](/archive/blogs/tiles_and_toasts/quickstart-sending-a-local-toast-notification-and-handling-activations-from-it-windows-10)
* [Notifications library on GitHub](https://github.com/windows-toolkit/WindowsCommunityToolkit/tree/dev/Notifications)