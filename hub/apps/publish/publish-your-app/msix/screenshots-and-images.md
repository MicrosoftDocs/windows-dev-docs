---
description: You can select the screenshots, logos, and other art assets (such as trailers and promotional images) to include in your MSIX app's Store listing.
title: App screenshots, images, and trailers for MSIX app
ms.date: 10/30/2022
ms.topic: article
ms.localizationpriority: medium
---

# App screenshots, images, and trailers for MSIX app

Well-designed images are one of the main ways for you to represent your app to potential customers in the Store.

You can provide [screenshots](#screenshots), [logos](#store-logos), [trailers](#trailers), and other art assets to include in your app's Store listing. Some of these are required, and some are optional (although some of the optional images are important to include for the best Store display).

During the [app submission process](./create-app-submission.md), you provide these art assets in the [Store listings](./create-app-store-listing.md) step. Note that the images which are used in the Store, and the way that they appear, may vary depending on the customer's operating system and other factors.

The Store may also use your app's icon and other images that you include in your app's package. Run the [Windows App Certification Kit](/windows/uwp/debug-test-perf/windows-app-certification-kit) to determine if you're missing any required images before you submit your app. For guidance and recommendations about these images, see [App icons and logos](../../../design/style/iconography/overview.md).

### Screenshots

Screenshots are images of your app that are displayed to your customers in your app's Store listing.

:::image type="content" source="images/msix-screenshots-logos.png" lightbox="images/msix-screenshots-logos.png" alt-text="A screenshot showing a section where you can add screenshots and logo for a MSIX/PWA app.":::

You have the option to provide screenshots for the different device families that your app supports so that the appropriate screenshots will appear when a customer views your app's Store listing on that type of device.

Only one screenshot (for any device family) is required for your submission, though you can provide several; up to 10 desktop screenshots and up to 8 screenshots for the other device families. We suggest providing at least four screenshots for each device family that your app supports so that people can see how the app will look on their device type. (Do not include screenshots for device families that your app does not support.) Note that **Desktop** screenshots will also be shown to customers on Surface Hub devices.

> [!NOTE]
> Microsoft Visual Studio provides a [tool to help you capture screenshots](/visualstudio/debugger/run-windows-store-apps-in-the-simulator#BKMK_Capture_a_screenshot_of_your_app_for_submission_to_the_Microsoft_Store).
> Each screenshot must be a .png file in either landscape or portrait orientation, and the file size can't be larger than 50 MB.

The size requirements vary depending on the device family:

- **Desktop** 1366 x 768 pixels or larger. Supports 4K images (3840 x 2160). (Will also be shown to customers on Surface Hub devices.)

- **Xbox** 3480 x 2160 pixels or smaller and 1920 x 1080 pixels or larger. Supports 4K images (3840 x 2160).

- **Holographic** 1268 x 720 pixels or larger. Supports 4K images (3840 x 2160).

For the best display, keep the following guidelines in mind when creating your screenshots:

- Keep critical visuals and text in the top 3/4 of the image. Text overlays may appear on the bottom 1/4.
- Don’t add additional logos, icons, or marketing messages to your screenshots.
- Don’t use extremely light or dark colors or highly-contrasting stripes that may interfere with readability of text overlays.

You can also provide a short caption that describes each screenshot in 200 characters or less.

> [!TIP]
> Screenshots are displayed in your listing in order. After you upload your screenshots, you can drag and drop them to reorder them.
> Note that if you create Store listings for [multiple languages](./app-package-requirements.md#supported-languages), you'll have a **Store listing** page for each one. You'll need to upload images for each language separately (even if you are using the same images), and provide captions to use for each language. (If you have Store listings in a lot of languages, you may find it easier to update them by [exporting your listing data and working offline](./import-and-export-store-listings.md).)

### Store logos

You can upload Store logos to create a more customized display in the Store. We recommend that you provide these images so that your Store listing appears optimally on all of the devices and OS versions that your app supports. Note that if your app is available to customers on Xbox, some of these images are required.

You can provide these images as .png files (no greater than 50 MB), each of which should follow the guidelines below.

#### 2:3 Poster art (720 x 1080 or 1440 x 2160 pixels)

For games, this is used as the main logo image for customers on Windows 10, Windows 11, and Xbox devices, so we **strongly recommend** providing this image to ensure proper display. This does not apply to apps. For information related to app icons, please refer to the section below on [1:1 App tile icon (300 x 300 pixels)](#11-app-tile-icon-300-x-300-pixels).

Your listing may not look good if you don't include it, and it won't be consistent with other listings that customers see while browsing the Store. This image may also be used in search results or in editorially curated collections.

This image can include your app’s name. If you have added the app name on the image, it should meet accessible readability requirements (4.51 contrast ratio). Note that text overlays may appear on the bottom quarter of this image, so make sure you don't include text or key imagery there.

> [!NOTE]
> If your app is available to customers on Xbox, this image is **required** and must include the product's title. The title must appear in the top three-quarters of the image, since text overlays may appear on the bottom quarter of the image.

#### 1:1 box art (1080 x 1080 or 2160 x 2160 pixels)

For games, this image may appear in various Store pages for Windows 10, Windows 11, and Xbox devices, and if you don't provide the **2:3 Poster art** image it will be used as your main logo. This does not apply to apps. For information related to app icons, please refer to the section below on [1:1 App tile icon (300 x 300 pixels)](#11-app-tile-icon-300-x-300-pixels).

This image can also include your app’s name. Text overlays may appear on the bottom quarter of this image, so don't include text or key imagery there.

> [!NOTE]
> If your app is available to customers on Xbox, this image is **required** and must include the product's title. The title must appear in the top three-quarters of the image, since text overlays may appear on the bottom quarter of the image.

#### 1:1 App tile icon (300 x 300 pixels)

This image will appear on various Store pages for Windows 10, Windows 11 and Xbox devices. We **strongly recommend** that you provide this image for your app. If you don’t provide this image, the Store will use the image from your app package.

> [!NOTE]
> If you choose to upload a 1:1 app tile icon (300 x 300 pixels), which is recommended, the Store will prioritize this image over the one included in your app package.

### Trailers and additional assets

This section lets you provide artwork to help display your product more effectively in the Store. We recommend providing these images to create a more inviting Store listing.

> [!TIP]
> The [16:9 Super hero art](#windows-10-and-xbox-image-169-super-hero-art) image is especially recommended for games if you plan to include [video trailers](#trailers) in your Store listing; if you don't include it, your trailers won't appear at the top of your listing.

#### Trailers

Trailers are short videos that give customers a way to see your product in action, so they can get a better understanding of what it’s like. They are shown at the top of your app's Store listing (as long as you include a [16:9 Super hero art](#windows-10-and-xbox-image-169-super-hero-art) image).

:::image type="content" source="images/msix-trailers.png" lightbox="images/msix-trailers.png" alt-text="A screenshot showing a section where you can add trailer for a MSIX/PWA app.":::

Trailers are encoded with [Smooth Streaming](https://www.iis.net/downloads/microsoft/smooth-streaming), which adapts the quality of a video stream delivered to clients in real time based on their available bandwidth and CPU resources.

> [!NOTE]
> Trailers are only shown to customers on Windows 10, version 1607 or later (which includes Xbox).

#### Upload trailers

You can add up to 15 trailers to your Store listing. Be sure they meet all of the [requirements](#trailer-requirements) listed below.

For each trailer you provide, you must upload a video file (.mp4 or .mov), a thumbnail image, and a title.

> [!IMPORTANT]
> When using trailers for games, you must also provide a [16:9 Super hero art](#windows-10-and-xbox-image-169-super-hero-art) image section in order for your trailers to appear at the top of your Store listing. This image will appear after your trailers have finished playing.

Follow these recommendations to make your trailers effective:

- Trailers should be of good quality and minimal length (60 seconds or less and less than 2 GB recommended).
- Use a different thumbnail for each trailer so that customers know they are unique.
- Because some Store layouts may slightly crop the top and bottom of your trailer, make sure key info appears in the center of the screen.
- Frame rate and resolution should match the source material. For example, content shot at 720p60 should be encoded and uploaded at 720p60.

You must also follow the requirements listed below.

**To add trailers to your listing:**

1. Upload your trailer **video file** in the indicated box. A drop-down box is also shown in case you want to reuse a trailer you have alread uploaded (perhaps for a Store listing in a different language).
2. After you have uploaded the trailer, you'll need to upload a **thumbnail image** to go along with it. This must be a .png file that is 1920 x 1080 pixels, and is typically a still image taken from the trailer.
3. Click the pencil icon to add a **title** for your trailer (255 characters or fewer).
4. If you want to add more trailers to the listing, click **Add trailer** and repeat the steps listed above.

> [!TIP]
> If you have created Store listings in multiple languages, you can select **Choose from existing trailers** to reuse the trailers you've already uploaded. You don't have to upload them individually for each language.
> To remove a trailer from a listing, click the **X** next to its file name. You can choose whether to remove it from only the current Store listing in which you are working, or to remove it from all of your product's Store listings (in every language).

#### Trailer requirements

When providing your trailers, be sure to follow these requirements:

- The video format must be MOV or MP4.
- The file size of the trailer shouldn't exceed 2 GB.
- The video resolution must be 1920 x 1080 pixels.
- The thumbnail must be a PNG file with a resolution of 1920 x 1080 pixels.
- The title can’t exceed 255 characters.
- Do not include age ratings in your trailers.

> [!WARNING]
> The exception to the requirement to include age ratings in your trailers applies **only** to trailers in the **Microsoft Store** that are shown **on the product page**. Any trailer posted outside of Partner Center, that is not intended for display exclusively on the Microsoft Store's product page **must** display embedded rating information, where required, in accordance with the appropriate rating authority’s guidelines.  
> Like the other fields on the Store listing page, trailers must pass certification before you can publish them to the Microsoft Store. Be sure your trailers comply with the [Microsoft Store Policies](../../store-policies.md).

There are additional requirements depending on the type of file.

##### MOV

:::row:::
:::column:::
**Video**<br> - 1080p ProRes (HQ where appropriate) - Native framerate; 29.97 FPS preferred
:::column-end:::
:::column:::
**Audio**<br> - Stereo required - Recommended Audio Level: -16 LKFS/LUFS
:::column-end:::
:::row-end:::

##### MP4

:::row:::
:::column:::
**Video**<br>
**Codec**: [H.264](/windows/desktop/DirectShow/h-264-video-types) (AVC1) - Progressive scan (no interlacing) - High Profile - 2 consecutive B frames - Closed GOP. GOP of half the frame rate - CABAC - 50 Mbps - Color Space: 4.2.0
:::column-end:::
:::column:::
**Audio**<br>
**Codec**: AAC-LC<br> - **Channels**: Stereo or surround sound<br> - **Sample rate**: 48 KHz<br> - **Audio Bitrate**: 384 Kbps for Stereo, 512 Kbps for surround sound
:::column-end:::
:::row-end:::

> [!WARNING]
> Customers may not hear audio for MP4 files encoded with codecs other than AVC1.
> For H.264 Mezzanine files, we recommend the following:
>
> - Container: MP4
> - No Edit Lists (or you might lose AV sync)
> - moov atom at the front of the file (Fast Start)

#### Windows 10 and Xbox image (16:9 Super hero art)

In the **Windows 10 or Windows 11 and Xbox image** section, the **16:9 Super hero art (1920 x 1080 or 3840 x 2160 pixels)** image is used in various layouts in the Microsoft Store on all Windows 10, Windows 11, and Xbox devices. We recommend providing this image, regardless of which OS versions or device types your app targets.

This image is _required_ for games for proper display if your listing includes [video trailers](#trailers). For customers on Windows 10, version 1607 or later (which includes Xbox), it is used as the main image on the top of your Store listing (or appears after any trailers finish playing). It may also be used to feature your app in promotional layouts throughout the Store. Note that this image must not include the product's title or other text.

Here are some tips to keep in mind when designing this image:

- The image must be a .png that is either 1920 x 1080 pixels or 3840 x 2160 pixels.
- Select a dynamic image that relates to the app to drive recognition and differentiation. Avoid stock photography or generic visuals.
- Don't include text in the image.
- Avoid placing key visual elements in the bottom third of the image (since in some layouts we may apply a gradient over that portion).
- Place the most important details in the center of the image (since in some layouts we may crop the image).
- Minimize empty space.
- Avoid showing your app's UI, and do not use any device-specific imagery.
- Avoid political and national themes, flags, or religious symbols.
- Don't include images of insensitive gestures, nudity, gambling, currency, drugs, tobacco, or alcohol.
- Don't use weapons pointing at the viewer or excessive violence and gore.

While providing this image allows us to consider your app for featured promotional opportunities, it does not guarantee that your app will be featured. See [Making your app easy to promote](../../make-your-app-easier-to-promote.md) for more information.

#### Xbox images

These images are required for proper display if you publish your app to Xbox.

There are 3 different sizes that you can upload:

- **Branded key art, 584 x 800 pixels**: Must include the product’s title. A Branding Bar is required on this image. Keep the title and all key imagery in the top three-quarters of the image, as an overlay may appear over the bottom quarter.
- **Titled hero art, 1920 x 1080 pixels**: Must include the product’s title. Keep the title and all key imagery in the top three-quarters of the image, as an overlay may appear over the bottom quarter.
- **Featured Promotional Square art, 1080 x 1080 pixels**: Must _not_ include the product’s title.

> [!NOTE]
> For the best display on Xbox, you must also provide a **9:16 (720 x 1080 or 1440 x 2160 pixels)** image in the [Store logos](#store-logos) section.

#### Holographic image

The **2:1 (2400 x 1200)** image is only used if your app supports the Holographic device family. If it does, we recommend providing this image.


