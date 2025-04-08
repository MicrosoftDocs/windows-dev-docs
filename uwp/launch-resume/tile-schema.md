---
description: The following article describes all of the properties and elements within tile content.
title: Tile content schema
ms.assetid: 7CBC3BD5-D9C3-4781-8BD0-1F28039E1FA8
label: Tile content schema
template: detail.hbs
ms.date: 08/08/2024
ms.topic: article
keywords: windows 10, uwp, tile, tile notification, tile content, schema, tile payload
ms.localizationpriority: medium
---
# Tile content schema

[!INCLUDE [notes](includes/live-tiles-note.md)]

The following describes all of the properties and elements within tile content.

If you would rather use raw XML instead of the [Notifications library](https://www.nuget.org/packages/Microsoft.Toolkit.Uwp.Notifications/), please see [the XML schema](adaptive-tiles-schema.md).

* [TileContent](#tilecontent)
  * [TileVisual](#tilevisual)
    * [TileBinding](#tilebinding)
      * [TileBindingContentAdaptive](#tilebindingcontentadaptive)
      * [TileBindingContentIconic](#tilebindingcontenticonic)
      * [TileBindingContentContact](#tilebindingcontentcontact)
      * [TileBindingContentPeople](#tilebindingcontentpeople)
      * [TileBindingContentPhotos](#tilebindingcontentphotos)

## TileContent

TileContent is the top level object that describes a tile notification's content, including visuals.

| Property | Type | Required | Description |
|---|---|---|---|
| **Visual** | [ToastVisual](#tilevisual) | true | Describes the visual portion of the tile notification. |

## TileVisual

The visual portion of tiles contains the visual specifications for all tile sizes, and more visual-related properties.

| Property | Type | Required | Description |
|---|---|---|---|
| **TileSmall** | [TileBinding](#tilebinding) | false | Provide an optional small binding to specify content for the small tile size. |
| **TileMedium** | [TileBinding](#tilebinding) | false | Provide an optional medium binding to specify content for the medium tile size. |
| **TileWide** | [TileBinding](#tilebinding) | false | Provide an optional wide binding to specify content for the wide tile size. |
| **TileLarge** | [TileBinding](#tilebinding) | false | Provide an optional large binding to specify content for the large tile size. |
| **Branding** | TileBranding | false | The form that the tile should use to display the app's brand. By default, inherits branding from the default tile. |
| **DisplayName** | string | false | An optional string to override the tile's display name while showing this notification. |
| **Arguments** | string | false | New in Anniversary Update: App-defined data that is passed back to your app via the TileActivatedInfo property on LaunchActivatedEventArgs when the user launches your app from the Live Tile. This allows you to know which tile notifications your user saw when they tapped your Live Tile. On devices without the Anniversary Update, this will simply be ignored. |
| **LockDetailedStatus1** | string | false | If you specify this, you must also provide a TileWide binding. This is the first line of text that will be displayed on the lock screen if the user has selected your tile as their detailed status app. |
| **LockDetailedStatus2** | string | false | If you specify this, you must also provide a TileWide binding. This is the second line of text that will be displayed on the lock screen if the user has selected your tile as their detailed status app. |
| **LockDetailedStatus3** | string | false | If you specify this, you must also provide a TileWide binding. This is the third line of text that will be displayed on the lock screen if the user has selected your tile as their detailed status app. |
| **BaseUri** | Uri | false | A default base URL that is combined with relative URLs in image source attributes. |
| **AddImageQuery** | bool? | false | Set to "true" to allow Windows to append a query string to the image URL supplied in the toast notification. Use this attribute if your server hosts images and can handle query strings, either by retrieving an image variant based on the query strings or by ignoring the query string and returning the image as specified without the query string. This query string specifies scale, contrast setting, and language; for instance, a value of "www.website.com/images/hello.png" given in the notification becomes "www.website.com/images/hello.png?ms-scale=100&ms-contrast=standard&ms-lang=en-us" |
| **Language**| string | false | The target locale of the visual payload when using localized resources, specified as BCP-47 language tags such as "en-US" or "fr-FR". This locale is overridden by any locale specified in binding or text. If not provided, the system locale will be used instead. |

## TileBinding

The binding object contains the visual content for a specific tile size.

| Property | Type | Required | Description |
|---|---|---|---|
| **Content** | [ITileBindingContent](#itilebindingcontent) | false | The visual content to display on the tile. One of [TileBindingContentAdaptive](#tilebindingcontentadaptive), [TileBindingContentIconic](#tilebindingcontenticonic), [TileBindingContentContact](#tilebindingcontentcontact), [TileBindingContentPeople](#tilebindingcontentpeople), or [TileBindingContentPhotos](#tilebindingcontentphotos). |
| **Branding** | TileBranding | false | The form that the tile should use to display the app's brand. By default, inherits branding from the default tile. |
| **DisplayName** | string | false | An optional string to override the tile's display name for this tile size. |
| **Arguments** | string | false | New in Anniversary Update: App-defined data that is passed back to your app via the TileActivatedInfo property on LaunchActivatedEventArgs when the user launches your app from the Live Tile. This allows you to know which tile notifications your user saw when they tapped your Live Tile. On devices without the Anniversary Update, this will simply be ignored. |
| **BaseUri** | Uri | false | A default base URL that is combined with relative URLs in image source attributes. |
| **AddImageQuery** | bool? | false | Set to "true" to allow Windows to append a query string to the image URL supplied in the toast notification. Use this attribute if your server hosts images and can handle query strings, either by retrieving an image variant based on the query strings or by ignoring the query string and returning the image as specified without the query string. This query string specifies scale, contrast setting, and language; for instance, a value of "www.website.com/images/hello.png" given in the notification becomes "www.website.com/images/hello.png?ms-scale=100&ms-contrast=standard&ms-lang=en-us" |
| **Language**| string | false | The target locale of the visual payload when using localized resources, specified as BCP-47 language tags such as "en-US" or "fr-FR". This locale is overridden by any locale specified in binding or text. If not provided, the system locale will be used instead. |

## ITileBindingContent

Marker interface for tile binding content. These let you choose what you want to specify your tile visuals in - Adaptive, or one of the special templates.

| Implementations |
| --- |
| [TileBindingContentAdaptive](#tilebindingcontentadaptive) |
| [TileBindingContentIconic](#tilebindingcontenticonic) |
| [TileBindingContentContact](#tilebindingcontentcontact) |
| [TileBindingContentPeople](#tilebindingcontentpeople) |
| [TileBindingContentPhotos](#tilebindingcontentphotos) |

## TileBindingContentAdaptive

Supported on all sizes. This is the recommended way of specifying your tile content. Adaptive Tile templates new in Windows 10, and you can create a wide variety of custom tiles through adaptive.

| Property | Type | Required | Description |
|---|---|---|---|
| **Children** | IList\<ITileBindingContentAdaptiveChild> | false | The inline visual elements. [AdaptiveText](#adaptivetext), [AdaptiveImage](#adaptiveimage), and [AdaptiveGroup](#adaptivegroup) objects can be added. The children are displayed in a vertical StackPanel fashion. |
| **BackgroundImage** | [TileBackgroundImage](#tilebackgroundimage) | false | An optional background image that gets displayed behind all the Tile content, full bleed. |
| **PeekImage** | [TilePeekImage](#tilepeekimage) | false | An optional peek image that animates in from the top of the Tile. |
| **TextStacking** | [TileTextStacking](#tiletextstacking) | false | Controls the text stacking (vertical alignment) of the children content as a whole. |

## AdaptiveText

An adaptive text element.

| Property | Type | Required |Description |
|---|---|---|---|
| **Text** | string | false | The text to display. |
| **HintStyle** | [AdaptiveTextStyle](#adaptivetextstyle) | false | The style controls the text's font size, weight, and opacity. |
| **HintWrap** | bool? | false | Set this to true to enable text wrapping. Default to false. |
| **HintMaxLines** | int? | false | The maximum number of lines the text element is allowed to display. |
| **HintMinLines** | int? | false | The minimum number of lines the text element must display. |
| **HintAlign** | [AdaptiveTextAlign](#adaptivetextalign) | false | The horizontal alignment of the text. |
| **Language** | string | false | The target locale of the XML payload, specified as a BCP-47 language tags such as "en-US" or "fr-FR". The locale specified here overrides any other specified locale, such as that in binding or visual. If this value is a literal string, this attribute defaults to the user's UI language. If this value is a string reference, this attribute defaults to the locale chosen by Windows Runtime in resolving the string. |

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

Controls the horizontal alignment of text.

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
| **HintCrop** | [AdaptiveImageCrop](#adaptiveimagecrop) | false | Control the desired cropping of the image. |
| **HintRemoveMargin** | bool? | false | By default, images inside groups/subgroups have an 8px margin around them. You can remove this margin by setting this property to true. |
| **HintAlign** | [AdaptiveImageAlign](#adaptiveimagealign) | false | The horizontal alignment of the image. |
| **AlternateText** | string | false | Alternate text describing the image, used for accessibility purposes. |
| **AddImageQuery** | bool? | false | Set to "true" to allow Windows to append a query string to the image URL supplied in the tile notification. Use this attribute if your server hosts images and can handle query strings, either by retrieving an image variant based on the query strings or by ignoring the query string and returning the image as specified without the query string. This query string specifies scale, contrast setting, and language; for instance, a value of "www.website.com/images/hello.png" given in the notification becomes "www.website.com/images/hello.png?ms-scale=100&ms-contrast=standard&ms-lang=en-us" |

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
| **Center** | Align the image in the center horizontally, displaying the image at its native resolution. |
| **Right** | Align the image to the right, displaying the image at its native resolution. |

## AdaptiveGroup

Groups semantically identify that the content in the group must either be displayed as a whole, or not displayed if it cannot fit. Groups also allow creating multiple columns.

| Property | Type | Required |Description |
|---|---|---|---|
| **Children** | IList<[AdaptiveSubgroup](#adaptivesubgroup)> | false | Subgroups are displayed as vertical columns. You must use subgroups to provide any content inside an AdaptiveGroup. |

## AdaptiveSubgroup

Subgroups are vertical columns that can contain text and images.

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

## TileBackgroundImage

A background image displayed full-bleed on the tile.

| Property | Type | Required |Description |
|---|---|---|---|
| **Source** | string | true | The URL to the image. ms-appx, ms-appdata, and http(s) are supported. Http images must be 200 KB or less in size. |
| **HintOverlay** | int? | false | A black overlay on the background image. This value controls the opacity of the black overlay, with 0 being no overlay and 100 being completely black. Defaults to 20. |
| **HintCrop** | [TileBackgroundImageCrop](#tilebackgroundimagecrop) | false | New in 1511: Specify how you would like the image to be cropped. In versions before 1511, this will be ignored and background image will be displayed without any cropping. |
| **AlternateText** | string | false | Alternate text describing the image, used for accessibility purposes. |
| **AddImageQuery** | bool? | false | Set to "true" to allow Windows to append a query string to the image URL supplied in the tile notification. Use this attribute if your server hosts images and can handle query strings, either by retrieving an image variant based on the query strings or by ignoring the query string and returning the image as specified without the query string. This query string specifies scale, contrast setting, and language; for instance, a value of "www.website.com/images/hello.png" given in the notification becomes "www.website.com/images/hello.png?ms-scale=100&ms-contrast=standard&ms-lang=en-us" |

### TileBackgroundImageCrop

Controls the cropping of the background image.

| Value | Meaning |
|---|---|
| **Default** | Cropping uses the default behavior of the renderer. |
| **None** | Image is not cropped, displayed square. |
| **Circle** | Image is cropped to a circle. |

## TilePeekImage

A peek image that animates in from the top of the tile.

| Property | Type | Required |Description |
|---|---|---|---|
| **Source** | string | true | The URL to the image. ms-appx, ms-appdata, and http(s) are supported. Http images must be 200 KB or less in size. |
| **HintOverlay** | int? | false | New in 1511: A black overlay on the peek image. This value controls the opacity of the black overlay, with 0 being no overlay and 100 being completely black. Defaults to 20. In previous versions, this value will be ignored and peek image will be displayed with 0 overlay. |
| **HintCrop** | [TilePeekImageCrop](#tilepeekimagecrop) | false | New in 1511: Specify how you would like the image to be cropped. In versions before 1511, this will be ignored and peek image will be displayed without any cropping. |
| **AlternateText** | string | false | Alternate text describing the image, used for accessibility purposes. |
| **AddImageQuery** | bool? | false | Set to "true" to allow Windows to append a query string to the image URL supplied in the tile notification. Use this attribute if your server hosts images and can handle query strings, either by retrieving an image variant based on the query strings or by ignoring the query string and returning the image as specified without the query string. This query string specifies scale, contrast setting, and language; for instance, a value of "www.website.com/images/hello.png" given in the notification becomes "www.website.com/images/hello.png?ms-scale=100&ms-contrast=standard&ms-lang=en-us" |

### TilePeekImageCrop

Controls the cropping of the peek image.

| Value | Meaning |
|---|---|
| **Default** | Cropping uses the default behavior of the renderer. |
| **None** | Image is not cropped, displayed square. |
| **Circle** | Image is cropped to a circle. |

### TileTextStacking

Text stacking specifies the vertical alignment of the content.

| Value | Meaning |
|---|---|
| **Default** | Default value. Renderer automatically selects the default vertical alignment. |
| **Top** | Vertical align to the top. |
| **Center** | Vertical align to the center. |
| **Bottom** | Vertical align to the bottom. |

## TileBindingContentIconic

Supported on Small and Medium. Enables an iconic tile template, where you can have an icon and badge display next to each other on the tile, in true classic Windows Phone style. The number next to the icon is achieved through a separate badge notification.

| Property | Type | Required |Description |
|---|---|---|---|
| **Icon** | [TileBasicImage](#tilebasicimage) | true | At minimum, to support both Desktop and Mobile, Small and Medium tiles, provide a square aspect ratio image with a resolution of 200x200, PNG format, with transparency and no color other than white. For more info see: [Special Tile Templates](special-tile-templates-catalog.md). |

## TileBindingContentContact

Mobile-only. Supported on Small, Medium, and Wide.

| Property | Type | Required |Description |
|---|---|---|---|
| **Image** | [TileBasicImage](#tilebasicimage) | true | The image to display. |
| **Text** | [TileBasicText](#tilebasictext) | false | A line of text that is displayed. Not displayed on small tile. |

## TileBindingContentPeople

New in 1511: Supported on Medium, Wide, and Large (Desktop and Mobile). Previously this was Mobile-only and only Medium and Wide.

| Property | Type | Required |Description |
|---|---|---|---|
| **Images** | IList<[TileBasicImage](#tilebasicimage)> | true | Images that will roll around as circles. |

## TileBindingContentPhotos

Animates through a slideshow of photos. Supported on all sizes.

| Property | Type | Required |Description |
|---|---|---|---|
| **Images** | IList<[TileBasicImage](#tilebasicimage)> | true | Up to 12 images can be provided (Mobile will only display up to 9), which will be used for the slideshow. Adding more than 12 will throw an exception. |

### TileBasicImage

An image used on various special templates.

| Property | Type | Required |Description |
|---|---|---|---|
| **Source** | string | true | The URL to the image. ms-appx, ms-appdata, and http(s) are supported. Http images must be 200 KB or less in size. |
| **AlternateText** | string | false | Alternate text describing the image, used for accessibility purposes. |
| **AddImageQuery** | bool? | false | Set to "true" to allow Windows to append a query string to the image URL supplied in the tile notification. Use this attribute if your server hosts images and can handle query strings, either by retrieving an image variant based on the query strings or by ignoring the query string and returning the image as specified without the query string. This query string specifies scale, contrast setting, and language; for instance, a value of "www.website.com/images/hello.png" given in the notification becomes "www.website.com/images/hello.png?ms-scale=100&ms-contrast=standard&ms-lang=en-us" |

### TileBasicText

A basic text element used on various special templates.

| Property | Type | Required |Description |
|---|---|---|---|
| **Text** | string | false | The text to display. |
| **Language** | string | false | The target locale of the XML payload, specified as a BCP-47 language tags such as "en-US" or "fr-FR". The locale specified here overrides any other specified locale, such as that in binding or visual. If this value is a literal string, this attribute defaults to the user's UI language. If this value is a string reference, this attribute defaults to the locale chosen by Windows Runtime in resolving the string. |

## Related topics

* [Send a local tile notification](sending-a-local-tile-notification.md)
* [Notifications library on GitHub](https://github.com/windows-toolkit/WindowsCommunityToolkit/)
