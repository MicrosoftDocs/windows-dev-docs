To edit a Store listing, select the language name from the app overview page. You must edit each language separately, unless you choose to export your Store listings and work offline, then import all of the listing data at once. For more about how that works, see [Import and export Store listings](../../../apps/publish/publish-your-app/import-and-export-store-listings.md).

:::image type="content" source="images/msix-listing-overview.png" lightbox="images/msix-listing-overview.png" alt-text="A screenshot showing the overview of the listing page for MSIX/PWA app.":::

The available fields are described below.

## Product name

This drop-down box lets you specify which name should be used in the Store listing (if you have reserved more than one name for the app).

If you have uploaded packages in the same language as the Store listing you're working on, the name used in those packages will be selected. If you need to [rename the app](../../../apps/publish/partner-center/manage-app-name-reservations.md#rename-an-app-that-has-already-been-published) after it's already been published, you can select a different reserved name here when you create a new submission, after you've uploaded packages that use the new name.

If you haven't uploaded packages for the language you're working on, and you've reserved more than one name, you'll need to select one of your reserved app names, since there isn't an associated package in that language from which to pull the name.

> [!NOTE]
> The **Product name** you select only applies to the Store listing in the language you're working in. It does not impact the name displayed when a customer installs the app; that name comes from the manifest of the package that gets installed. To avoid confusion, we recommend that each language's package(s) and Store listing use the same name.

## Description

The description field is where you can tell customers what your app does. This field is required, and will accept up to 10,000 characters of plain text.

For some tips on making your description stand out, see [Write a great app description](../../../apps/publish/publish-your-app/write-great-app-description.md).

## What's new in this version

If this is the first time you're submitting your app, leave this field blank. For an update to an existing app, this is where you can let customers know what's changed in the latest release. This field has a 1500 character limit. (Previously, this field was called **Release notes**).

## Product features

These are short summaries of your app's key features. They are displayed to the customer as a bulleted list in the **Features** section of your app's Store listing, in addition to the **Description**. Keep these brief, with just a few words (and no more than 200 characters) per feature. You may include up to 20 features.

> [!NOTE]
> These features will appear bulleted in your Store listing, so don't add your own bullets.

## Screenshots

One screenshot is required in order to submit your app. We recommend providing at least four screenshots for each device type that your app supports so that people can see how the app will look on their device type.

For more info, see [App screenshots and images](../../../apps/publish/publish-your-app/screenshots-and-images.md#screenshots).

## Store logos

Store logos are optional images that you can upload to enhance the way your app is displayed to customers. You can also optionally specify that only images you upload here should be used in your app’s Store listing for customers on Windows 10 or Windows 11 (including Xbox), rather than allowing the Store to use logo images from your app’s packages.

> [!IMPORTANT]
> If your app supports Xbox, you must provide certain images here in order for the listing to appear properly in the Store.

For more info, see [Store logos](../../../apps/publish/publish-your-app/screenshots-and-images.md#store-logos).

## Trailers and additional assets

You can submit additional assets for your product, including video trailers and promotional images. These are all optional, but we recommend that you consider uploading as many of them as possible. These images can help give customers a better idea of what your product is and make a more enticing listing.

For more info, see [Trailers and additional assets](../../../apps/publish/publish-your-app/screenshots-and-images.md#trailers-and-additional-assets).

## Supplemental fields

The fields in this section are all optional. Review the info below to determine if providing this info makes sense for your submission. In particular, the **Short description** is recommended for most submissions. The other fields may help provide an optimal experience for your product in different scenarios.

### Short title

A shorter version of your product’s name. If provided, this shorter name may appear in various places on Xbox One (during installation, in Achievements, etc.) in place of the full title of your product.

This field has a 50 character limit.

### Sort title

If your product could be alphabetized or spelled in different ways, you can enter another version here. This allows customers to find your product more quickly if they type that version in while searching.

This field has a 255 character limit.

### Voice title

An alternate name for your product that, if provided, may be used in the audio experience on Xbox One when using Kinect or a headset.

This field has a 255 character limit.

### Short description

A shorter, catchy description that may be used in the top of your product’s Store listing. If not provided, the first paragraph (up to 500 characters) of your longer [description](#description) will be used instead. Because your description also appears below this text, we recommend providing a short description with different text so that your Store listing isn’t repetitive.

For games, the short description may also appear in the Information section of the Game Hub on Xbox One.

For best results, keep your short description under 270 characters. The field has a 1000 character limit, but in some views, only the first 270 characters will be shown (with a link available to view the rest of the short description).

### Additional system requirements

If needed, you can describe the hardware configurations that your app requires to work properly (beyond the info you provided in the **System requirements** section in [App properties](../../../apps/publish/publish-your-app/enter-app-properties.md#system-requirements). This is especially important if your app requires hardware that might not be available on every computer. For instance, if your app will only work properly with external USB hardware such as a 3D printer or microcontroller, we suggest entering those here. The info you enter will be shown to customers viewing your app's Store listing on Windows 10, version 1607 or later (including Xbox), along with the requirements you indicated on the product's properties page.

You can enter up to 11 items for both **Minimum hardware** and **Recommended hardware**. These are displayed to the customer as a bulleted list in your Store listing. Keep these brief, with just a few words (and no more than 200 characters) per item.

> [!NOTE]
> Your additional system requirements will appear bulleted in your Store listing, so don't add your own bullets.
