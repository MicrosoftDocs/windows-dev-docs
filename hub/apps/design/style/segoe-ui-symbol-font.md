---
description: This article lists and provides usage guidance for the glyphs that come with the Segoe MDL2 Assets font.
Search.Refinement.TopicID: 184
title: Segoe MDL2 Assets icons
ms.assetid: DFB215C2-8A61-4957-B662-3B1991AC9BE1
label: Segoe MDL2 Assets icons
template: detail.hbs
ms.date: 07/18/2024
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Segoe MDL2 Assets icons

This article provides developer guidelines for using the Segoe MDL2 Assets icons and lists the font glyphs along with their unicode values and descriptive names.

**Important APIs**:

* [**FontIcon class**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.fonticon)

## About Segoe MDL2 Assets

> [!IMPORTANT]
> With the release of Windows 10, the `Segoe MDL2 Assets` font replaced the Windows 8/8.1 `Segoe UI Symbol` icon font.
>
> With the release of Windows 11, the **`Segoe Fluent Icons`** font replaced `Segoe MDL2 Assets` as the recommended symbol icon font. `Segoe UI Symbol` and `Segoe MDL2 Assets` are still available, but we recommend updating your app to use the [Segoe Fluent Icons font](segoe-fluent-icons-font.md).

Most of the icons included in the `Segoe MDL2 Assets` font are mapped to the Private Use Area of Unicode (PUA). The PUA allows font developers to assign private Unicode values to glyphs that don’t map to existing code points. This is useful when creating a symbol font, but it creates an interoperability problem. If the font is not available, the glyphs won’t show up. Use these glyphs only when you can explicitly specify the `Segoe MDL2 Assets` font. If you are working with tiles, you can't use these glyphs because you can't specify the tile font and PUA glyphs are not available via font-fallback.

Unlike with `Segoe UI Symbol`, the icons in the `Segoe MDL2 Assets` font are not intended for use in-line with text. This means that some older "tricks" like the progressive disclosure arrows no longer apply. Likewise, since all of the new icons are sized and positioned the same, they do not have to be made with zero width; we have just made sure they work as a set. Ideally, you can overlay two icons that were designed as a set and they will fall into place. We may do this to allow colorization in the code. For example, U+EA3A and U+EA3B were created for the Start tile Badge status. Because these are already centered the circle fill can be colored for different states.

## Layering and mirroring

All glyphs in `Segoe MDL2 Assets` have the same fixed width with a consistent height and left origin point, so layering and colorization effects can be achieved by drawing glyphs directly on top of each other. This example show a black outline drawn on top of the zero-width red heart.

![using a zero-width glyph](images/segoe-ui-symbol-layering.png)

Many of the icons also have mirrored forms available for use in languages that use right-to-left text directionality such as Arabic, Dari, Persian, and Hebrew.

## Using the icons

To use a glyph from the `Segoe MDL2 Assets` font, then use a [**FontIcon**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.fonticon).

```xaml
<FontIcon FontFamily="Segoe MDL2 Assets" Glyph="&#xE700;"/>
```

## How do I get this font?

* On Windows: There's nothing you need to do, the font comes with Windows.
* On a Mac, you need to download and install the font: <a href="https://aka.ms/SegoeFonts">Get the Segoe UI and MDL2 icon fonts</a>

## Icon list

Please keep in mind that the `Segoe MDL2 Assets` font includes many more icons than we can show here. Many of the icons are intended for specialized purposes and are not typically used anywhere else.

> [!NOTE]
> Glyphs with prefixes ranging from **E0-** to **E5-** (e.g. E001, E5B1) are currently marked as legacy and we recommend that they not be used.

The following tables display all Segoe MDL2 Assets icons and their respective unicode values and descriptive names. Select a range from the following list to view glyphs according to the PUA range they belong to.

* [PUA E700-E900](#pua-e700-e900)
* [PUA EA00-EC00](#pua-ea00-ec00)
* [PUA ED00-EF00](#pua-ed00-ef00)
* [PUA F000-F200](#pua-f000-f200)
* [PUA F300-F500](#pua-f300-f500)
* [PUA F600-F800](#pua-f600-f800)

### PUA E700-E900

The following table of glyphs displays unicode points prefixed from E7-  to E9-.

[Back to top](#icon-list)

</br>
<table>
 <tr>
  <td>Glyph</td>
  <td>Unicode point</td>
  <td>Description</td>
 </tr>
 <tr><td><img src="images/segoe-mdl/E700.png" width="32" height="32" alt="GlobalNavigationButton" /></td>
  <td>E700</td>
  <td>GlobalNavigationButton</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E701.png" width="32" height="32" alt="Wifi" /></td>
  <td>E701</td>
  <td>Wifi</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E702.png" width="32" height="32" alt="Bluetooth" /></td>
  <td>E702</td>
  <td>Bluetooth</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E703.png" width="32" height="32" alt="Connect" /></td>
  <td>E703</td>
  <td>Connect</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E704.png" width="32" height="32" alt="InternetSharing" /></td>
  <td>E704</td>
  <td>InternetSharing</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E705.png" width="32" height="32" alt="VPN" /></td>
  <td>E705</td>
  <td>VPN</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E706.png" width="32" height="32" alt="Brightness" /></td>
  <td>E706</td>
  <td>Brightness</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E707.png" width="32" height="32" alt="MapPin" /></td>
  <td>E707</td>
  <td>MapPin</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E708.png" width="32" height="32" alt="QuietHours" /></td>
  <td>E708</td>
  <td>QuietHours</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E709.png" width="32" height="32" alt="Airplane" /></td>
  <td>E709</td>
  <td>Airplane</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E70A.png" width="32" height="32" alt="Tablet" /></td>
  <td>E70A</td>
  <td>Tablet</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E70B.png" width="32" height="32" alt="QuickNote" /></td>
  <td>E70B</td>
  <td>QuickNote</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E70C.png" width="32" height="32" alt="RememberedDevice" /></td>
  <td>E70C</td>
  <td>RememberedDevice</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E70D.png" width="32" height="32" alt="ChevronDown" /></td>
  <td>E70D</td>
  <td>ChevronDown</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E70E.png" width="32" height="32" alt="ChevronUp" /></td>
  <td>E70E</td>
  <td>ChevronUp</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E70F.png" width="32" height="32" alt="Edit" /></td>
  <td>E70F</td>
  <td>Edit</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E710.png" width="32" height="32" alt="Add" /></td>
  <td>E710</td>
  <td>Add</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E711.png" width="32" height="32" alt="Cancel" /></td>
  <td>E711</td>
  <td>Cancel</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E712.png" width="32" height="32" alt="More" /></td>
  <td>E712</td>
  <td>More</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E713.png" width="32" height="32" alt="Setting" /></td>
  <td>E713</td>
  <td>Setting</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E714.png" width="32" height="32" alt="Video" /></td>
  <td>E714</td>
  <td>Video</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E715.png" width="32" height="32" alt="Mail" /></td>
  <td>E715</td>
  <td>Mail</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E716.png" width="32" height="32" alt="People" /></td>
  <td>E716</td>
  <td>People</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E717.png" width="32" height="32" alt="Phone" /></td>
  <td>E717</td>
  <td>Phone</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E718.png" width="32" height="32" alt="Pin" /></td>
  <td>E718</td>
  <td>Pin</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E719.png" width="32" height="32" alt="Shop" /></td>
  <td>E719</td>
  <td>Shop</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E71A.png" width="32" height="32" alt="Stop" /></td>
  <td>E71A</td>
  <td>Stop</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E71B.png" width="32" height="32" alt="Link" /></td>
  <td>E71B</td>
  <td>Link</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E71C.png" width="32" height="32" alt="Filter" /></td>
  <td>E71C</td>
  <td>Filter</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E71D.png" width="32" height="32" alt="AllApps" /></td>
  <td>E71D</td>
  <td>AllApps</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E71E.png" width="32" height="32" alt="Zoom" /></td>
  <td>E71E</td>
  <td>Zoom</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E71F.png" width="32" height="32" alt="ZoomOut" /></td>
  <td>E71F</td>
  <td>ZoomOut</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E720.png" width="32" height="32" alt="Microphone" /></td>
  <td>E720</td>
  <td>Microphone</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E721.png" width="32" height="32" alt="Search" /></td>
  <td>E721</td>
  <td>Search</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E722.png" width="32" height="32" alt="Camera" /></td>
  <td>E722</td>
  <td>Camera</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E723.png" width="32" height="32" alt="Attach" /></td>
  <td>E723</td>
  <td>Attach</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E724.png" width="32" height="32" alt="Send" /></td>
  <td>E724</td>
  <td>Send</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E725.png" width="32" height="32" alt="SendFill" /></td>
  <td>E725</td>
  <td>SendFill</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E726.png" width="32" height="32" alt="WalkSolid" /></td>
  <td>E726</td>
  <td>WalkSolid</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E727.png" width="32" height="32" alt="InPrivate" /></td>
  <td>E727</td>
  <td>InPrivate</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E728.png" width="32" height="32" alt="FavoriteList" /></td>
  <td>E728</td>
  <td>FavoriteList</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E729.png" width="32" height="32" alt="PageSolid" /></td>
  <td>E729</td>
  <td>PageSolid</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E72A.png" width="32" height="32" alt="Forward" /></td>
  <td>E72A</td>
  <td>Forward</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E72B.png" width="32" height="32" alt="Back" /></td>
  <td>E72B</td>
  <td>Back</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E72C.png" width="32" height="32" alt="Refresh" /></td>
  <td>E72C</td>
  <td>Refresh</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E72D.png" width="32" height="32" alt="Share" /></td>
  <td>E72D</td>
  <td>Share</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E72E.png" width="32" height="32" alt="Lock" /></td>
  <td>E72E</td>
  <td>Lock</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E730.png" width="32" height="32" alt="ReportHacked" /></td>
  <td>E730</td>
  <td>ReportHacked</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E731.png" width="32" height="32" alt="EMI" /></td>
  <td>E731</td>
  <td>EMI</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E734.png" width="32" height="32" alt="FavoriteStar" /></td>
  <td>E734</td>
  <td>FavoriteStar</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E735.png" width="32" height="32" alt="FavoriteStarFill" /></td>
  <td>E735</td>
  <td>FavoriteStarFill</td>
 </tr>
 <tr><td><img src="images/segoe-mdl/E736.png" width="32" height="32" alt="ReadingMode" /></td>
  <td>E736</td>
  <td>ReadingMode</td>
</tr>
<tr><td><img src="images/segoe-mdl/E737.png" width="32" height="32" alt="Favicon" /></td>
  <td>E737</td>
  <td>Favicon</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E738.png" width="32" height="32" alt="Remove" /></td>
  <td>E738</td>
  <td>Remove</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E739.png" width="32" height="32" alt="Checkbox" /></td>
  <td>E739</td>
  <td>Checkbox</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E73A.png" width="32" height="32" alt="CheckboxComposite" /></td>
  <td>E73A</td>
  <td>CheckboxComposite</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E73B.png" width="32" height="32" alt="CheckboxFill" /></td>
  <td>E73B</td>
  <td>CheckboxFill</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E73C.png" width="32" height="32" alt="CheckboxIndeterminate" /></td>
  <td>E73C</td>
  <td>CheckboxIndeterminate</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E73D.png" width="32" height="32" alt="CheckboxCompositeReversed" /></td>
  <td>E73D</td>
  <td>CheckboxCompositeReversed</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E73E.png" width="32" height="32" alt="CheckMark" /></td>
  <td>E73E</td>
  <td>CheckMark</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E73F.png" width="32" height="32" alt="BackToWindow" /></td>
  <td>E73F</td>
  <td>BackToWindow</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E740.png" width="32" height="32" alt="FullScreen" /></td>
  <td>E740</td>
  <td>FullScreen</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E741.png" width="32" height="32" alt="ResizeTouchLarger" /></td>
  <td>E741</td>
  <td>ResizeTouchLarger</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E742.png" width="32" height="32" alt="ResizeTouchSmaller" /></td>
  <td>E742</td>
  <td>ResizeTouchSmaller</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E743.png" width="32" height="32" alt="ResizeMouseSmall" /></td>
  <td>E743</td>
  <td>ResizeMouseSmall</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E744.png" width="32" height="32" alt="ResizeMouseMedium" /></td>
  <td>E744</td>
  <td>ResizeMouseMedium</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E745.png" width="32" height="32" alt="ResizeMouseWide" /></td>
  <td>E745</td>
  <td>ResizeMouseWide</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E746.png" width="32" height="32" alt="ResizeMouseTall" /></td>
  <td>E746</td>
  <td>ResizeMouseTall</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E747.png" width="32" height="32" alt="ResizeMouseLarge" /></td>
  <td>E747</td>
  <td>ResizeMouseLarge</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E748.png" width="32" height="32" alt="SwitchUser" /></td>
  <td>E748</td>
  <td>SwitchUser</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E749.png" width="32" height="32" alt="Print" /></td>
  <td>E749</td>
  <td>Print</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E74A.png" width="32" height="32" alt="Up" /></td>
  <td>E74A</td>
  <td>Up</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E74B.png" width="32" height="32" alt="Down" /></td>
  <td>E74B</td>
  <td>Down</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E74C.png" width="32" height="32" alt="OEM" /></td>
  <td>E74C</td>
  <td>OEM</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E74D.png" width="32" height="32" alt="Delete" /></td>
  <td>E74D</td>
  <td>Delete</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E74E.png" width="32" height="32" alt="Save" /></td>
  <td>E74E</td>
  <td>Save</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E74F.png" width="32" height="32" alt="Mute" /></td>
  <td>E74F</td>
  <td>Mute</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E750.png" width="32" height="32" alt="BackSpaceQWERTY" /></td>
  <td>E750</td>
  <td>BackSpaceQWERTY</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E751.png" width="32" height="32" alt="ReturnKey" /></td>
  <td>E751</td>
  <td>ReturnKey</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E752.png" width="32" height="32" alt="UpArrowShiftKey" /></td>
  <td>E752</td>
  <td>UpArrowShiftKey</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E753.png" width="32" height="32" alt="Cloud" /></td>
  <td>E753</td>
  <td>Cloud</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E754.png" width="32" height="32" alt="Flashlight" /></td>
  <td>E754</td>
  <td>Flashlight</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E755.png" width="32" height="32" alt="RotationLock" /></td>
  <td>E755</td>
  <td>RotationLock</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E756.png" width="32" height="32" alt="CommandPrompt" /></td>
  <td>E756</td>
  <td>CommandPrompt</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E759.png" width="32" height="32" alt="SIPMove" /></td>
  <td>E759</td>
  <td>SIPMove</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E75A.png" width="32" height="32" alt="SIPUndock" /></td>
  <td>E75A</td>
  <td>SIPUndock</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E75B.png" width="32" height="32" alt="SIPRedock" /></td>
  <td>E75B</td>
  <td>SIPRedock</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E75C.png" width="32" height="32" alt="EraseTool" /></td>
  <td>E75C</td>
  <td>EraseTool</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E75D.png" width="32" height="32" alt="UnderscoreSpace" /></td>
  <td>E75D</td>
  <td>UnderscoreSpace</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E75E.png" width="32" height="32" alt="GripperTool" /></td>
  <td>E75E</td>
  <td>GripperTool</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E75F.png" width="32" height="32" alt="Dialpad" /></td>
  <td>E75F</td>
  <td>Dialpad</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E760.png" width="32" height="32" alt="PageLeft" /></td>
  <td>E760</td>
  <td>PageLeft</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E761.png" width="32" height="32" alt="PageRight" /></td>
  <td>E761</td>
  <td>PageRight</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E762.png" width="32" height="32" alt="MultiSelect" /></td>
  <td>E762</td>
  <td>MultiSelect</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E763.png" width="32" height="32" alt="KeyboardLeftHanded" /></td>
  <td>E763</td>
  <td>KeyboardLeftHanded</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E764.png" width="32" height="32" alt="KeyboardRightHanded" /></td>
  <td>E764</td>
  <td>KeyboardRightHanded</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E765.png" width="32" height="32" alt="KeyboardClassic" /></td>
  <td>E765</td>
  <td>KeyboardClassic</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E766.png" width="32" height="32" alt="KeyboardSplit" /></td>
  <td>E766</td>
  <td>KeyboardSplit</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E767.png" width="32" height="32" alt="Volume" /></td>
  <td>E767</td>
  <td>Volume</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E768.png" width="32" height="32" alt="Play" /></td>
  <td>E768</td>
  <td>Play</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E769.png" width="32" height="32" alt="Pause" /></td>
  <td>E769</td>
  <td>Pause</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E76B.png" width="32" height="32" alt="ChevronLeft" /></td>
  <td>E76B</td>
  <td>ChevronLeft</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E76C.png" width="32" height="32" alt="ChevronRight" /></td>
  <td>E76C</td>
  <td>ChevronRight</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E76D.png" width="32" height="32" alt="InkingTool" /></td>
  <td>E76D</td>
  <td>InkingTool</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E76E.png" width="32" height="32" alt="Emoji2" /></td>
  <td>E76E</td>
  <td>Emoji2</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E76F.png" width="32" height="32" alt="GripperBarHorizontal" /></td>
  <td>E76F</td>
  <td>GripperBarHorizontal</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E770.png" width="32" height="32" alt="System" /></td>
  <td>E770</td>
  <td>System</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E771.png" width="32" height="32" alt="Personalize" /></td>
  <td>E771</td>
  <td>Personalize</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E772.png" width="32" height="32" alt="Devices" /></td>
  <td>E772</td>
  <td>Devices</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E773.png" width="32" height="32" alt="SearchAndApps" /></td>
  <td>E773</td>
  <td>SearchAndApps</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E774.png" width="32" height="32" alt="Globe" /></td>
  <td>E774</td>
  <td>Globe</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E775.png" width="32" height="32" alt="TimeLanguage" /></td>
  <td>E775</td>
  <td>TimeLanguage</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E776.png" width="32" height="32" alt="EaseOfAccess" /></td>
  <td>E776</td>
  <td>EaseOfAccess</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E777.png" width="32" height="32" alt="UpdateRestore" /></td>
  <td>E777</td>
  <td>UpdateRestore</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E778.png" width="32" height="32" alt="HangUp" /></td>
  <td>E778</td>
  <td>HangUp</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E779.png" width="32" height="32" alt="ContactInfo" /></td>
  <td>E779</td>
  <td>ContactInfo</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E77A.png" width="32" height="32" alt="Unpin" /></td>
  <td>E77A</td>
  <td>Unpin</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E77B.png" width="32" height="32" alt="Contact" /></td>
  <td>E77B</td>
  <td>Contact</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E77C.png" width="32" height="32" alt="Memo" /></td>
  <td>E77C</td>
  <td>Memo</td>
 </tr>
 <tr><td><img src="images/segoe-mdl/E77E.png" width="32" height="32" alt="IncomingCall" /></td>
  <td>E77E</td>
  <td>IncomingCall</td>
</tr>
<tr><td><img src="images/segoe-mdl/E77F.png" width="32" height="32" alt="Paste" /></td>
  <td>E77F</td>
  <td>Paste</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E780.png" width="32" height="32" alt="PhoneBook" /></td>
  <td>E780</td>
  <td>PhoneBook</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E781.png" width="32" height="32" alt="LEDLight" /></td>
  <td>E781</td>
  <td>LEDLight</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E783.png" width="32" height="32" alt="Error" /></td>
  <td>E783</td>
  <td>Error</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E784.png" width="32" height="32" alt="GripperBarVertical" /></td>
  <td>E784</td>
  <td>GripperBarVertical</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E785.png" width="32" height="32" alt="Unlock" /></td>
  <td>E785</td>
  <td>Unlock</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E786.png" width="32" height="32" alt="Slideshow" /></td>
  <td>E786</td>
  <td>Slideshow</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E787.png" width="32" height="32" alt="Calendar" /></td>
  <td>E787</td>
  <td>Calendar</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E788.png" width="32" height="32" alt="GripperResize" /></td>
  <td>E788</td>
  <td>GripperResize</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E789.png" width="32" height="32" alt="Megaphone" /></td>
  <td>E789</td>
  <td>Megaphone</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E78A.png" width="32" height="32" alt="Trim" /></td>
  <td>E78A</td>
  <td>Trim</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E78B.png" width="32" height="32" alt="NewWindow" /></td>
  <td>E78B</td>
  <td>NewWindow</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E78C.png" width="32" height="32" alt="SaveLocal" /></td>
  <td>E78C</td>
  <td>SaveLocal</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E790.png" width="32" height="32" alt="Color" /></td>
  <td>E790</td>
  <td>Color</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E791.png" width="32" height="32" alt="DataSense" /></td>
  <td>E791</td>
  <td>DataSense</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E792.png" width="32" height="32" alt="SaveAs" /></td>
  <td>E792</td>
  <td>SaveAs</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E793.png" width="32" height="32" alt="Light" /></td>
  <td>E793</td>
  <td>Light</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E799.png" width="32" height="32" alt="AspectRatio" /></td>
  <td>E799</td>
  <td>AspectRatio</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E7A5.png" width="32" height="32" alt="DataSenseBar" /></td>
  <td>E7A5</td>
  <td>DataSenseBar</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E7A6.png" width="32" height="32" alt="Redo" /></td>
  <td>E7A6</td>
  <td>Redo</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E7A7.png" width="32" height="32" alt="Undo" /></td>
  <td>E7A7</td>
  <td>Undo</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E7A8.png" width="32" height="32" alt="Crop" /></td>
  <td>E7A8</td>
  <td>Crop</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E7AC.png" width="32" height="32" alt="OpenWith" /></td>
  <td>E7AC</td>
  <td>OpenWith</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E7AD.png" width="32" height="32" alt="Rotate" /></td>
  <td>E7AD</td>
  <td>Rotate</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E7B3.png" width="32" height="32" alt="RedEye" /></td>
  <td>E7B3</td>
  <td>RedEye</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E7B5.png" width="32" height="32" alt="SetlockScreen" /></td>
  <td>E7B5</td>
  <td>SetlockScreen</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E7B7.png" width="32" height="32" alt="MapPin2" /></td>
  <td>E7B7</td>
  <td>MapPin2</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E7B8.png" width="32" height="32" alt="Package" /></td>
  <td>E7B8</td>
  <td>Package</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E7BA.png" width="32" height="32" alt="Warning" /></td>
  <td>E7BA</td>
  <td>Warning</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E7BC.png" width="32" height="32" alt="ReadingList" /></td>
  <td>E7BC</td>
  <td>ReadingList</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E7BE.png" width="32" height="32" alt="Education" /></td>
  <td>E7BE</td>
  <td>Education</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E7BF.png" width="32" height="32" alt="ShoppingCart" /></td>
  <td>E7BF</td>
  <td>ShoppingCart</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E7C0.png" width="32" height="32" alt="Train" /></td>
  <td>E7C0</td>
  <td>Train</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E7C1.png" width="32" height="32" alt="Flag" /></td>
  <td>E7C1</td>
  <td>Flag</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E7C3.png" width="32" height="32" alt="Page" /></td>
  <td>E7C3</td>
  <td>Page</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E7C4.png" width="32" height="32" alt="TaskView" /></td>
  <td>E7C4</td>
  <td>TaskView</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E7C5.png" width="32" height="32" alt="BrowsePhotos" /></td>
  <td>E7C5</td>
  <td>BrowsePhotos</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E7C6.png" width="32" height="32" alt="HalfStarLeft" /></td>
  <td>E7C6</td>
  <td>HalfStarLeft</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E7C7.png" width="32" height="32" alt="HalfStarRight" /></td>
  <td>E7C7</td>
  <td>HalfStarRight</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E7C8.png" width="32" height="32" alt="Record" /></td>
  <td>E7C8</td>
  <td>Record</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E7C9.png" width="32" height="32" alt="TouchPointer" /></td>
  <td>E7C9</td>
  <td>TouchPointer</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E7DE.png" width="32" height="32" alt="LangJPN" /></td>
  <td>E7DE</td>
  <td>LangJPN</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E7E3.png" width="32" height="32" alt="Ferry" /></td>
  <td>E7E3</td>
  <td>Ferry</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E7E6.png" width="32" height="32" alt="Highlight" /></td>
  <td>E7E6</td>
  <td>Highlight</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E7E7.png" width="32" height="32" alt="ActionCenterNotification" /></td>
  <td>E7E7</td>
  <td>ActionCenterNotification</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E7E8.png" width="32" height="32" alt="PowerButton" /></td>
  <td>E7E8</td>
  <td>PowerButton</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E7EA.png" width="32" height="32" alt="ResizeTouchNarrower" /></td>
  <td>E7EA</td>
  <td>ResizeTouchNarrower</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E7EB.png" width="32" height="32" alt="ResizeTouchShorter" /></td>
  <td>E7EB</td>
  <td>ResizeTouchShorter</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E7EC.png" width="32" height="32" alt="DrivingMode" /></td>
  <td>E7EC</td>
  <td>DrivingMode</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E7ED.png" width="32" height="32" alt="RingerSilent" /></td>
  <td>E7ED</td>
  <td>RingerSilent</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E7EE.png" width="32" height="32" alt="OtherUser" /></td>
  <td>E7EE</td>
  <td>OtherUser</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E7EF.png" width="32" height="32" alt="Admin" /></td>
  <td>E7EF</td>
  <td>Admin</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E7F0.png" width="32" height="32" alt="CC" /></td>
  <td>E7F0</td>
  <td>CC</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E7F1.png" width="32" height="32" alt="SDCard" /></td>
  <td>E7F1</td>
  <td>SDCard</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E7F2.png" width="32" height="32" alt="CallForwarding" /></td>
  <td>E7F2</td>
  <td>CallForwarding</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E7F3.png" width="32" height="32" alt="SettingsDisplaySound" /></td>
  <td>E7F3</td>
  <td>SettingsDisplaySound</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E7F4.png" width="32" height="32" alt="TVMonitor" /></td>
  <td>E7F4</td>
  <td>TVMonitor</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E7F5.png" width="32" height="32" alt="Speakers" /></td>
  <td>E7F5</td>
  <td>Speakers</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E7F6.png" width="32" height="32" alt="Headphone" /></td>
  <td>E7F6</td>
  <td>Headphone</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E7F7.png" width="32" height="32" alt="DeviceLaptopPic" /></td>
  <td>E7F7</td>
  <td>DeviceLaptopPic</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E7F8.png" width="32" height="32" alt="DeviceLaptopNoPic" /></td>
  <td>E7F8</td>
  <td>DeviceLaptopNoPic</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E7F9.png" width="32" height="32" alt="DeviceMonitorRightPic" /></td>
  <td>E7F9</td>
  <td>DeviceMonitorRightPic</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E7FA.png" width="32" height="32" alt="DeviceMonitorLeftPic" /></td>
  <td>E7FA</td>
  <td>DeviceMonitorLeftPic</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E7FB.png" width="32" height="32" alt="DeviceMonitorNoPic" /></td>
  <td>E7FB</td>
  <td>DeviceMonitorNoPic</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E7FC.png" width="32" height="32" alt="Game" /></td>
  <td>E7FC</td>
  <td>Game</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E7FD.png" width="32" height="32" alt="HorizontalTabKey" /></td>
  <td>E7FD</td>
  <td>HorizontalTabKey</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E802.png" width="32" height="32" alt="StreetsideSplitMinimize" /></td>
  <td>E802</td>
  <td>StreetsideSplitMinimize</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E803.png" width="32" height="32" alt="StreetsideSplitExpand" /></td>
  <td>E803</td>
  <td>StreetsideSplitExpand</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E804.png" width="32" height="32" alt="Car" /></td>
  <td>E804</td>
  <td>Car</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E805.png" width="32" height="32" alt="Walk" /></td>
  <td>E805</td>
  <td>Walk</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E806.png" width="32" height="32" alt="Bus" /></td>
  <td>E806</td>
  <td>Bus</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E809.png" width="32" height="32" alt="TiltUp" /></td>
  <td>E809</td>
  <td>TiltUp</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E80A.png" width="32" height="32" alt="TiltDown" /></td>
  <td>E80A</td>
  <td>TiltDown</td>
 </tr>
 <tr><td><img src="images/segoe-mdl/E80B.png" width="32" height="32" alt="CallControl" /></td>
  <td>E80B</td>
  <td>CallControl</td>
</tr>
<tr><td><img src="images/segoe-mdl/E80C.png" width="32" height="32" alt="RotateMapRight" /></td>
  <td>E80C</td>
  <td>RotateMapRight</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E80D.png" width="32" height="32" alt="RotateMapLeft" /></td>
  <td>E80D</td>
  <td>RotateMapLeft</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E80F.png" width="32" height="32" alt="Home" /></td>
  <td>E80F</td>
  <td>Home</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E811.png" width="32" height="32" alt="ParkingLocation" /></td>
  <td>E811</td>
  <td>ParkingLocation</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E812.png" width="32" height="32" alt="MapCompassTop" /></td>
  <td>E812</td>
  <td>MapCompassTop</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E813.png" width="32" height="32" alt="MapCompassBottom" /></td>
  <td>E813</td>
  <td>MapCompassBottom</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E814.png" width="32" height="32" alt="IncidentTriangle" /></td>
  <td>E814</td>
  <td>IncidentTriangle</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E815.png" width="32" height="32" alt="Touch" /></td>
  <td>E815</td>
  <td>Touch</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E816.png" width="32" height="32" alt="MapDirections" /></td>
  <td>E816</td>
  <td>MapDirections</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E819.png" width="32" height="32" alt="StartPoint" /></td>
  <td>E819</td>
  <td>StartPoint</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E81A.png" width="32" height="32" alt="StopPoint" /></td>
  <td>E81A</td>
  <td>StopPoint</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E81B.png" width="32" height="32" alt="EndPoint" /></td>
  <td>E81B</td>
  <td>EndPoint</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E81C.png" width="32" height="32" alt="History" /></td>
  <td>E81C</td>
  <td>History</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E81D.png" width="32" height="32" alt="Location" /></td>
  <td>E81D</td>
  <td>Location</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E81E.png" width="32" height="32" alt="MapLayers" /></td>
  <td>E81E</td>
  <td>MapLayers</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E81F.png" width="32" height="32" alt="Accident" /></td>
  <td>E81F</td>
  <td>Accident</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E821.png" width="32" height="32" alt="Work" /></td>
  <td>E821</td>
  <td>Work</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E822.png" width="32" height="32" alt="Construction" /></td>
  <td>E822</td>
  <td>Construction</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E823.png" width="32" height="32" alt="Recent" /></td>
  <td>E823</td>
  <td>Recent</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E825.png" width="32" height="32" alt="Bank" /></td>
  <td>E825</td>
  <td>Bank</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E826.png" width="32" height="32" alt="DownloadMap" /></td>
  <td>E826</td>
  <td>DownloadMap</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E829.png" width="32" height="32" alt="InkingToolFill2" /></td>
  <td>E829</td>
  <td>InkingToolFill2</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E82A.png" width="32" height="32" alt="HighlightFill2" /></td>
  <td>E82A</td>
  <td>HighlightFill2</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E82B.png" width="32" height="32" alt="EraseToolFill" /></td>
  <td>E82B</td>
  <td>EraseToolFill</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E82C.png" width="32" height="32" alt="EraseToolFill2" /></td>
  <td>E82C</td>
  <td>EraseToolFill2</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E82D.png" width="32" height="32" alt="Dictionary" /></td>
  <td>E82D</td>
  <td>Dictionary</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E82E.png" width="32" height="32" alt="DictionaryAdd" /></td>
  <td>E82E</td>
  <td>DictionaryAdd</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E82F.png" width="32" height="32" alt="ToolTip" /></td>
  <td>E82F</td>
  <td>ToolTip</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E830.png" width="32" height="32" alt="ChromeBack" /></td>
  <td>E830</td>
  <td>ChromeBack</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E835.png" width="32" height="32" alt="ProvisioningPackage" /></td>
  <td>E835</td>
  <td>ProvisioningPackage</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E836.png" width="32" height="32" alt="AddRemoteDevice" /></td>
  <td>E836</td>
  <td>AddRemoteDevice</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E838.png" width="32" height="32" alt="FolderOpen" /></td>
  <td>E838</td>
  <td>FolderOpen</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E839.png" width="32" height="32" alt="Ethernet" /></td>
  <td>E839</td>
  <td>Ethernet</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E83A.png" width="32" height="32" alt=" ShareBroadband" /></td>
  <td>E83A</td>
  <td> ShareBroadband</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E83B.png" width="32" height="32" alt="DirectAccess" /></td>
  <td>E83B</td>
  <td>DirectAccess</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E83C.png" width="32" height="32" alt=" DialUp" /></td>
  <td>E83C</td>
  <td> DialUp</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E83D.png" width="32" height="32" alt="DefenderApp " /></td>
  <td>E83D</td>
  <td>DefenderApp </td>
 </tr>
<tr><td><img src="images/segoe-mdl/E83E.png" width="32" height="32" alt="BatteryCharging9" /></td>
  <td>E83E</td>
  <td>BatteryCharging9</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E83F.png" width="32" height="32" alt="Battery10" /></td>
  <td>E83F</td>
  <td>Battery10</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E840.png" width="32" height="32" alt="Pinned" /></td>
  <td>E840</td>
  <td>Pinned</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E841.png" width="32" height="32" alt="PinFill" /></td>
  <td>E841</td>
  <td>PinFill</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E842.png" width="32" height="32" alt="PinnedFill" /></td>
  <td>E842</td>
  <td>PinnedFill</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E843.png" width="32" height="32" alt="PeriodKey" /></td>
  <td>E843</td>
  <td>PeriodKey</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E844.png" width="32" height="32" alt="PuncKey" /></td>
  <td>E844</td>
  <td>PuncKey</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E845.png" width="32" height="32" alt="RevToggleKey" /></td>
  <td>E845</td>
  <td>RevToggleKey</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E846.png" width="32" height="32" alt="RightArrowKeyTime1" /></td>
  <td>E846</td>
  <td>RightArrowKeyTime1</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E847.png" width="32" height="32" alt="RightArrowKeyTime2" /></td>
  <td>E847</td>
  <td>RightArrowKeyTime2</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E848.png" width="32" height="32" alt="LeftQuote" /></td>
  <td>E848</td>
  <td>LeftQuote</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E849.png" width="32" height="32" alt="RightQuote" /></td>
  <td>E849</td>
  <td>RightQuote</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E84A.png" width="32" height="32" alt="DownShiftKey" /></td>
  <td>E84A</td>
  <td>DownShiftKey</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E84B.png" width="32" height="32" alt="UpShiftKey" /></td>
  <td>E84B</td>
  <td>UpShiftKey</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E84C.png" width="32" height="32" alt="PuncKey0" /></td>
  <td>E84C</td>
  <td>PuncKey0</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E84D.png" width="32" height="32" alt="PuncKeyLeftBottom" /></td>
  <td>E84D</td>
  <td>PuncKeyLeftBottom</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E84E.png" width="32" height="32" alt="RightArrowKeyTime3" /></td>
  <td>E84E</td>
  <td>RightArrowKeyTime3</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E84F.png" width="32" height="32" alt="RightArrowKeyTime4" /></td>
  <td>E84F</td>
  <td>RightArrowKeyTime4</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E850.png" width="32" height="32" alt="Battery0" /></td>
  <td>E850</td>
  <td>Battery0</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E851.png" width="32" height="32" alt="Battery1" /></td>
  <td>E851</td>
  <td>Battery1</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E852.png" width="32" height="32" alt="Battery2" /></td>
  <td>E852</td>
  <td>Battery2</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E853.png" width="32" height="32" alt="Battery3" /></td>
  <td>E853</td>
  <td>Battery3</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E854.png" width="32" height="32" alt="Battery4" /></td>
  <td>E854</td>
  <td>Battery4</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E855.png" width="32" height="32" alt="Battery5" /></td>
  <td>E855</td>
  <td>Battery5</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E856.png" width="32" height="32" alt="Battery6" /></td>
  <td>E856</td>
  <td>Battery6</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E857.png" width="32" height="32" alt="Battery7" /></td>
  <td>E857</td>
  <td>Battery7</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E858.png" width="32" height="32" alt="Battery8" /></td>
  <td>E858</td>
  <td>Battery8</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E859.png" width="32" height="32" alt="Battery9" /></td>
  <td>E859</td>
  <td>Battery9</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E85A.png" width="32" height="32" alt="BatteryCharging0" /></td>
  <td>E85A</td>
  <td>BatteryCharging0</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E85B.png" width="32" height="32" alt="BatteryCharging1" /></td>
  <td>E85B</td>
  <td>BatteryCharging1</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E85C.png" width="32" height="32" alt="BatteryCharging2" /></td>
  <td>E85C</td>
  <td>BatteryCharging2</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E85D.png" width="32" height="32" alt="BatteryCharging3" /></td>
  <td>E85D</td>
  <td>BatteryCharging3</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E85E.png" width="32" height="32" alt="BatteryCharging4" /></td>
  <td>E85E</td>
  <td>BatteryCharging4</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E85F.png" width="32" height="32" alt="BatteryCharging5" /></td>
  <td>E85F</td>
  <td>BatteryCharging5</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E860.png" width="32" height="32" alt="BatteryCharging6" /></td>
  <td>E860</td>
  <td>BatteryCharging6</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E861.png" width="32" height="32" alt="BatteryCharging7" /></td>
  <td>E861</td>
  <td>BatteryCharging7</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E862.png" width="32" height="32" alt="BatteryCharging8" /></td>
  <td>E862</td>
  <td>BatteryCharging8</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E863.png" width="32" height="32" alt="BatterySaver0" /></td>
  <td>E863</td>
  <td>BatterySaver0</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E864.png" width="32" height="32" alt="BatterySaver1" /></td>
  <td>E864</td>
  <td>BatterySaver1</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E865.png" width="32" height="32" alt="BatterySaver2" /></td>
  <td>E865</td>
  <td>BatterySaver2</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E866.png" width="32" height="32" alt="BatterySaver3" /></td>
  <td>E866</td>
  <td>BatterySaver3</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E867.png" width="32" height="32" alt="BatterySaver4" /></td>
  <td>E867</td>
  <td>BatterySaver4</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E868.png" width="32" height="32" alt="BatterySaver5" /></td>
  <td>E868</td>
  <td>BatterySaver5</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E869.png" width="32" height="32" alt="BatterySaver6" /></td>
  <td>E869</td>
  <td>BatterySaver6</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E86A.png" width="32" height="32" alt="BatterySaver7" /></td>
  <td>E86A</td>
  <td>BatterySaver7</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E86B.png" width="32" height="32" alt="BatterySaver8" /></td>
  <td>E86B</td>
  <td>BatterySaver8</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E86C.png" width="32" height="32" alt="SignalBars1" /></td>
  <td>E86C</td>
  <td>SignalBars1</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E86D.png" width="32" height="32" alt="SignalBars2" /></td>
  <td>E86D</td>
  <td>SignalBars2</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E86E.png" width="32" height="32" alt="SignalBars3" /></td>
  <td>E86E</td>
  <td>SignalBars3</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E86F.png" width="32" height="32" alt="SignalBars4" /></td>
  <td>E86F</td>
  <td>SignalBars4</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E870.png" width="32" height="32" alt="SignalBars5" /></td>
  <td>E870</td>
  <td>SignalBars5</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E871.png" width="32" height="32" alt="SignalNotConnected" /></td>
  <td>E871</td>
  <td>SignalNotConnected</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E872.png" width="32" height="32" alt="Wifi1" /></td>
  <td>E872</td>
  <td>Wifi1</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E873.png" width="32" height="32" alt="Wifi2" /></td>
  <td>E873</td>
  <td>Wifi2</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E874.png" width="32" height="32" alt="Wifi3" /></td>
  <td>E874</td>
  <td>Wifi3</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E875.png" width="32" height="32" alt="MobSIMLock" /></td>
  <td>E875</td>
  <td>MobSIMLock</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E876.png" width="32" height="32" alt="MobSIMMissing" /></td>
  <td>E876</td>
  <td>MobSIMMissing</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E877.png" width="32" height="32" alt="Vibrate" /></td>
  <td>E877</td>
  <td>Vibrate</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E878.png" width="32" height="32" alt="RoamingInternational" /></td>
  <td>E878</td>
  <td>RoamingInternational</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E879.png" width="32" height="32" alt="RoamingDomestic" /></td>
  <td>E879</td>
  <td>RoamingDomestic</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E87A.png" width="32" height="32" alt="CallForwardInternational" /></td>
  <td>E87A</td>
  <td>CallForwardInternational</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E87B.png" width="32" height="32" alt="CallForwardRoaming" /></td>
  <td>E87B</td>
  <td>CallForwardRoaming</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E87C.png" width="32" height="32" alt="JpnRomanji" /></td>
  <td>E87C</td>
  <td>JpnRomanji</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E87D.png" width="32" height="32" alt="JpnRomanjiLock" /></td>
  <td>E87D</td>
  <td>JpnRomanjiLock</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E87E.png" width="32" height="32" alt="JpnRomanjiShift" /></td>
  <td>E87E</td>
  <td>JpnRomanjiShift</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E87F.png" width="32" height="32" alt="JpnRomanjiShiftLock" /></td>
  <td>E87F</td>
  <td>JpnRomanjiShiftLock</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E880.png" width="32" height="32" alt="StatusDataTransfer" /></td>
  <td>E880</td>
  <td>StatusDataTransfer</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E881.png" width="32" height="32" alt="StatusDataTransferVPN" /></td>
  <td>E881</td>
  <td>StatusDataTransferVPN</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E882.png" width="32" height="32" alt="StatusDualSIM2" /></td>
  <td>E882</td>
  <td>StatusDualSIM2</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E883.png" width="32" height="32" alt="StatusDualSIM2VPN" /></td>
  <td>E883</td>
  <td>StatusDualSIM2VPN</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E884.png" width="32" height="32" alt="StatusDualSIM1" /></td>
  <td>E884</td>
  <td>StatusDualSIM1</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E885.png" width="32" height="32" alt="StatusDualSIM1VPN" /></td>
  <td>E885</td>
  <td>StatusDualSIM1VPN</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E886.png" width="32" height="32" alt="StatusSGLTE" /></td>
  <td>E886</td>
  <td>StatusSGLTE</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E887.png" width="32" height="32" alt="StatusSGLTECell" /></td>
  <td>E887</td>
  <td>StatusSGLTECell</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E888.png" width="32" height="32" alt="StatusSGLTEDataVPN" /></td>
  <td>E888</td>
  <td>StatusSGLTEDataVPN</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E889.png" width="32" height="32" alt="StatusVPN" /></td>
  <td>E889</td>
  <td>StatusVPN</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E88A.png" width="32" height="32" alt="WifiHotspot" /></td>
  <td>E88A</td>
  <td>WifiHotspot</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E88B.png" width="32" height="32" alt="LanguageKor" /></td>
  <td>E88B</td>
  <td>LanguageKor</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E88C.png" width="32" height="32" alt="LanguageCht" /></td>
  <td>E88C</td>
  <td>LanguageCht</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E88D.png" width="32" height="32" alt="LanguageChs" /></td>
  <td>E88D</td>
  <td>LanguageChs</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E88E.png" width="32" height="32" alt="USB" /></td>
  <td>E88E</td>
  <td>USB</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E88F.png" width="32" height="32" alt="InkingToolFill" /></td>
  <td>E88F</td>
  <td>InkingToolFill</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E890.png" width="32" height="32" alt="View" /></td>
  <td>E890</td>
  <td>View</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E891.png" width="32" height="32" alt="HighlightFill" /></td>
  <td>E891</td>
  <td>HighlightFill</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E892.png" width="32" height="32" alt="Previous" /></td>
  <td>E892</td>
  <td>Previous</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E893.png" width="32" height="32" alt="Next" /></td>
  <td>E893</td>
  <td>Next</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E894.png" width="32" height="32" alt="Clear" /></td>
  <td>E894</td>
  <td>Clear</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E895.png" width="32" height="32" alt="Sync" /></td>
  <td>E895</td>
  <td>Sync</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E896.png" width="32" height="32" alt="Download" /></td>
  <td>E896</td>
  <td>Download</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E897.png" width="32" height="32" alt="Help" /></td>
  <td>E897</td>
  <td>Help</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E898.png" width="32" height="32" alt="Upload" /></td>
  <td>E898</td>
  <td>Upload</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E899.png" width="32" height="32" alt="Emoji" /></td>
  <td>E899</td>
  <td>Emoji</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E89A.png" width="32" height="32" alt="TwoPage" /></td>
  <td>E89A</td>
  <td>TwoPage</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E89B.png" width="32" height="32" alt="LeaveChat" /></td>
  <td>E89B</td>
  <td>LeaveChat</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E89C.png" width="32" height="32" alt="MailForward" /></td>
  <td>E89C</td>
  <td>MailForward</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E89E.png" width="32" height="32" alt="RotateCamera" /></td>
  <td>E89E</td>
  <td>RotateCamera</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E89F.png" width="32" height="32" alt="ClosePane" /></td>
  <td>E89F</td>
  <td>ClosePane</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8A0.png" width="32" height="32" alt="OpenPane" /></td>
  <td>E8A0</td>
  <td>OpenPane</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8A1.png" width="32" height="32" alt="PreviewLink" /></td>
  <td>E8A1</td>
  <td>PreviewLink</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8A2.png" width="32" height="32" alt="AttachCamera" /></td>
  <td>E8A2</td>
  <td>AttachCamera</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8A3.png" width="32" height="32" alt="ZoomIn" /></td>
  <td>E8A3</td>
  <td>ZoomIn</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8A4.png" width="32" height="32" alt="Bookmarks" /></td>
  <td>E8A4</td>
  <td>Bookmarks</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8A5.png" width="32" height="32" alt="Document" /></td>
  <td>E8A5</td>
  <td>Document</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8A6.png" width="32" height="32" alt="ProtectedDocument" /></td>
  <td>E8A6</td>
  <td>ProtectedDocument</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8A7.png" width="32" height="32" alt="OpenInNewWindow" /></td>
  <td>E8A7</td>
  <td>OpenInNewWindow</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8A8.png" width="32" height="32" alt="MailFill" /></td>
  <td>E8A8</td>
  <td>MailFill</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8A9.png" width="32" height="32" alt="ViewAll" /></td>
  <td>E8A9</td>
  <td>ViewAll</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8AA.png" width="32" height="32" alt="VideoChat" /></td>
  <td>E8AA</td>
  <td>VideoChat</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8AB.png" width="32" height="32" alt="Switch" /></td>
  <td>E8AB</td>
  <td>Switch</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8AC.png" width="32" height="32" alt="Rename" /></td>
  <td>E8AC</td>
  <td>Rename</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8AD.png" width="32" height="32" alt="Go" /></td>
  <td>E8AD</td>
  <td>Go</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8AE.png" width="32" height="32" alt="SurfaceHub" /></td>
  <td>E8AE</td>
  <td>SurfaceHub</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8AF.png" width="32" height="32" alt="Remote" /></td>
  <td>E8AF</td>
  <td>Remote</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8B0.png" width="32" height="32" alt="Click" /></td>
  <td>E8B0</td>
  <td>Click</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8B1.png" width="32" height="32" alt="Shuffle" /></td>
  <td>E8B1</td>
  <td>Shuffle</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8B2.png" width="32" height="32" alt="Movies" /></td>
  <td>E8B2</td>
  <td>Movies</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8B3.png" width="32" height="32" alt="SelectAll" /></td>
  <td>E8B3</td>
  <td>SelectAll</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8B4.png" width="32" height="32" alt="Orientation" /></td>
  <td>E8B4</td>
  <td>Orientation</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8B5.png" width="32" height="32" alt="Import" /></td>
  <td>E8B5</td>
  <td>Import</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8B6.png" width="32" height="32" alt="ImportAll" /></td>
  <td>E8B6</td>
  <td>ImportAll</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8B7.png" width="32" height="32" alt="Folder" /></td>
  <td>E8B7</td>
  <td>Folder</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8B8.png" width="32" height="32" alt="Webcam" /></td>
  <td>E8B8</td>
  <td>Webcam</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8B9.png" width="32" height="32" alt="Picture" /></td>
  <td>E8B9</td>
  <td>Picture</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8BA.png" width="32" height="32" alt="Caption" /></td>
  <td>E8BA</td>
  <td>Caption</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8BB.png" width="32" height="32" alt="ChromeClose" /></td>
  <td>E8BB</td>
  <td>ChromeClose</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8BC.png" width="32" height="32" alt="ShowResults" /></td>
  <td>E8BC</td>
  <td>ShowResults</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8BD.png" width="32" height="32" alt="Message" /></td>
  <td>E8BD</td>
  <td>Message</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8BE.png" width="32" height="32" alt="Leaf" /></td>
  <td>E8BE</td>
  <td>Leaf</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8BF.png" width="32" height="32" alt="CalendarDay" /></td>
  <td>E8BF</td>
  <td>CalendarDay</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8C0.png" width="32" height="32" alt="CalendarWeek" /></td>
  <td>E8C0</td>
  <td>CalendarWeek</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8C1.png" width="32" height="32" alt="Characters" /></td>
  <td>E8C1</td>
  <td>Characters</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8C2.png" width="32" height="32" alt="MailReplyAll" /></td>
  <td>E8C2</td>
  <td>MailReplyAll</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8C3.png" width="32" height="32" alt="Read" /></td>
  <td>E8C3</td>
  <td>Read</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8C4.png" width="32" height="32" alt="ShowBcc" /></td>
  <td>E8C4</td>
  <td>ShowBcc</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8C5.png" width="32" height="32" alt="HideBcc" /></td>
  <td>E8C5</td>
  <td>HideBcc</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8C6.png" width="32" height="32" alt="Cut" /></td>
  <td>E8C6</td>
  <td>Cut</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8C7.png" width="32" height="32" alt="PaymentCard" /></td>
  <td>E8C7</td>
  <td>PaymentCard</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8C8.png" width="32" height="32" alt="Copy" /></td>
  <td>E8C8</td>
  <td>Copy</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8C9.png" width="32" height="32" alt="Important" /></td>
  <td>E8C9</td>
  <td>Important</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8CA.png" width="32" height="32" alt="MailReply" /></td>
  <td>E8CA</td>
  <td>MailReply</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8CB.png" width="32" height="32" alt="Sort" /></td>
  <td>E8CB</td>
  <td>Sort</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8CC.png" width="32" height="32" alt="MobileTablet" /></td>
  <td>E8CC</td>
  <td>MobileTablet</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8CD.png" width="32" height="32" alt="DisconnectDrive" /></td>
  <td>E8CD</td>
  <td>DisconnectDrive</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8CE.png" width="32" height="32" alt="MapDrive" /></td>
  <td>E8CE</td>
  <td>MapDrive</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8CF.png" width="32" height="32" alt="ContactPresence" /></td>
  <td>E8CF</td>
  <td>ContactPresence</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8D0.png" width="32" height="32" alt="Priority" /></td>
  <td>E8D0</td>
  <td>Priority</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8D1.png" width="32" height="32" alt="GotoToday" /></td>
  <td>E8D1</td>
  <td>GotoToday</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8D2.png" width="32" height="32" alt="Font" /></td>
  <td>E8D2</td>
  <td>Font</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8D3.png" width="32" height="32" alt="FontColor" /></td>
  <td>E8D3</td>
  <td>FontColor</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8D4.png" width="32" height="32" alt="Contact2" /></td>
  <td>E8D4</td>
  <td>Contact2</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8D5.png" width="32" height="32" alt="FolderFill" /></td>
  <td>E8D5</td>
  <td>FolderFill</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8D6.png" width="32" height="32" alt="Audio" /></td>
  <td>E8D6</td>
  <td>Audio</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8D7.png" width="32" height="32" alt="Permissions" /></td>
  <td>E8D7</td>
  <td>Permissions</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8D8.png" width="32" height="32" alt="DisableUpdates" /></td>
  <td>E8D8</td>
  <td>DisableUpdates</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8D9.png" width="32" height="32" alt="Unfavorite" /></td>
  <td>E8D9</td>
  <td>Unfavorite</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8DA.png" width="32" height="32" alt="OpenLocal" /></td>
  <td>E8DA</td>
  <td>OpenLocal</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8DB.png" width="32" height="32" alt="Italic" /></td>
  <td>E8DB</td>
  <td>Italic</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8DC.png" width="32" height="32" alt="Underline" /></td>
  <td>E8DC</td>
  <td>Underline</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8DD.png" width="32" height="32" alt="Bold" /></td>
  <td>E8DD</td>
  <td>Bold</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8DE.png" width="32" height="32" alt="MoveToFolder" /></td>
  <td>E8DE</td>
  <td>MoveToFolder</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8DF.png" width="32" height="32" alt="LikeDislike" /></td>
  <td>E8DF</td>
  <td>LikeDislike</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8E0.png" width="32" height="32" alt="Dislike" /></td>
  <td>E8E0</td>
  <td>Dislike</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8E1.png" width="32" height="32" alt="Like" /></td>
  <td>E8E1</td>
  <td>Like</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8E2.png" width="32" height="32" alt="AlignRight" /></td>
  <td>E8E2</td>
  <td>AlignRight</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8E3.png" width="32" height="32" alt="AlignCenter" /></td>
  <td>E8E3</td>
  <td>AlignCenter</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8E4.png" width="32" height="32" alt="AlignLeft" /></td>
  <td>E8E4</td>
  <td>AlignLeft</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8E5.png" width="32" height="32" alt="OpenFile" /></td>
  <td>E8E5</td>
  <td>OpenFile</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8E6.png" width="32" height="32" alt="ClearSelection" /></td>
  <td>E8E6</td>
  <td>ClearSelection</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8E7.png" width="32" height="32" alt="FontDecrease" /></td>
  <td>E8E7</td>
  <td>FontDecrease</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8E8.png" width="32" height="32" alt="FontIncrease" /></td>
  <td>E8E8</td>
  <td>FontIncrease</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8E9.png" width="32" height="32" alt="FontSize" /></td>
  <td>E8E9</td>
  <td>FontSize</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8EA.png" width="32" height="32" alt="CellPhone" /></td>
  <td>E8EA</td>
  <td>CellPhone</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8EB.png" width="32" height="32" alt="Reshare" /></td>
  <td>E8EB</td>
  <td>Reshare</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8EC.png" width="32" height="32" alt="Tag" /></td>
  <td>E8EC</td>
  <td>Tag</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8ED.png" width="32" height="32" alt="RepeatOne" /></td>
  <td>E8ED</td>
  <td>RepeatOne</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8EE.png" width="32" height="32" alt="RepeatAll" /></td>
  <td>E8EE</td>
  <td>RepeatAll</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8EF.png" width="32" height="32" alt="Calculator" /></td>
  <td>E8EF</td>
  <td>Calculator</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8F0.png" width="32" height="32" alt="Directions" /></td>
  <td>E8F0</td>
  <td>Directions</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8F1.png" width="32" height="32" alt="Library" /></td>
  <td>E8F1</td>
  <td>Library</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8F2.png" width="32" height="32" alt="ChatBubbles" /></td>
  <td>E8F2</td>
  <td>ChatBubbles</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8F3.png" width="32" height="32" alt="PostUpdate" /></td>
  <td>E8F3</td>
  <td>PostUpdate</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8F4.png" width="32" height="32" alt="NewFolder" /></td>
  <td>E8F4</td>
  <td>NewFolder</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8F5.png" width="32" height="32" alt="CalendarReply" /></td>
  <td>E8F5</td>
  <td>CalendarReply</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8F6.png" width="32" height="32" alt="UnsyncFolder" /></td>
  <td>E8F6</td>
  <td>UnsyncFolder</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8F7.png" width="32" height="32" alt="SyncFolder" /></td>
  <td>E8F7</td>
  <td>SyncFolder</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8F8.png" width="32" height="32" alt="BlockContact" /></td>
  <td>E8F8</td>
  <td>BlockContact</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8F9.png" width="32" height="32" alt="SwitchApps" /></td>
  <td>E8F9</td>
  <td>SwitchApps</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8FA.png" width="32" height="32" alt="AddFriend" /></td>
  <td>E8FA</td>
  <td>AddFriend</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8FB.png" width="32" height="32" alt="Accept" /></td>
  <td>E8FB</td>
  <td>Accept</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8FC.png" width="32" height="32" alt="GoToStart" /></td>
  <td>E8FC</td>
  <td>GoToStart</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8FD.png" width="32" height="32" alt="BulletedList" /></td>
  <td>E8FD</td>
  <td>BulletedList</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8FE.png" width="32" height="32" alt="Scan" /></td>
  <td>E8FE</td>
  <td>Scan</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E8FF.png" width="32" height="32" alt="Preview" /></td>
  <td>E8FF</td>
  <td>Preview</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E902.png" width="32" height="32" alt="Group" /></td>
  <td>E902</td>
  <td>Group</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E904.png" width="32" height="32" alt="ZeroBars" /></td>
  <td>E904</td>
  <td>ZeroBars</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E905.png" width="32" height="32" alt="OneBar" /></td>
  <td>E905</td>
  <td>OneBar</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E906.png" width="32" height="32" alt="TwoBars" /></td>
  <td>E906</td>
  <td>TwoBars</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E907.png" width="32" height="32" alt="ThreeBars" /></td>
  <td>E907</td>
  <td>ThreeBars</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E908.png" width="32" height="32" alt="FourBars" /></td>
  <td>E908</td>
  <td>FourBars</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E909.png" width="32" height="32" alt="World" /></td>
  <td>E909</td>
  <td>World</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E90A.png" width="32" height="32" alt="Comment" /></td>
  <td>E90A</td>
  <td>Comment</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E90B.png" width="32" height="32" alt="MusicInfo" /></td>
  <td>E90B</td>
  <td>MusicInfo</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E90C.png" width="32" height="32" alt="DockLeft" /></td>
  <td>E90C</td>
  <td>DockLeft</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E90D.png" width="32" height="32" alt="DockRight" /></td>
  <td>E90D</td>
  <td>DockRight</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E90E.png" width="32" height="32" alt="DockBottom" /></td>
  <td>E90E</td>
  <td>DockBottom</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E90F.png" width="32" height="32" alt="Repair" /></td>
  <td>E90F</td>
  <td>Repair</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E910.png" width="32" height="32" alt="Accounts" /></td>
  <td>E910</td>
  <td>Accounts</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E911.png" width="32" height="32" alt="DullSound" /></td>
  <td>E911</td>
  <td>DullSound</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E912.png" width="32" height="32" alt="Manage" /></td>
  <td>E912</td>
  <td>Manage</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E913.png" width="32" height="32" alt="Street" /></td>
  <td>E913</td>
  <td>Street</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E914.png" width="32" height="32" alt="Printer3D" /></td>
  <td>E914</td>
  <td>Printer3D</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E915.png" width="32" height="32" alt="RadioBullet" /></td>
  <td>E915</td>
  <td>RadioBullet</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E916.png" width="32" height="32" alt="Stopwatch" /></td>
  <td>E916</td>
  <td>Stopwatch</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E91B.png" width="32" height="32" alt="Photo" /></td>
  <td>E91B</td>
  <td>Photo</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E91C.png" width="32" height="32" alt="ActionCenter" /></td>
  <td>E91C</td>
  <td>ActionCenter</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E91F.png" width="32" height="32" alt="FullCircleMask" /></td>
  <td>E91F</td>
  <td>FullCircleMask</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E921.png" width="32" height="32" alt="ChromeMinimize" /></td>
  <td>E921</td>
  <td>ChromeMinimize</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E922.png" width="32" height="32" alt="ChromeMaximize" /></td>
  <td>E922</td>
  <td>ChromeMaximize</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E923.png" width="32" height="32" alt="ChromeRestore" /></td>
  <td>E923</td>
  <td>ChromeRestore</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E924.png" width="32" height="32" alt="Annotation" /></td>
  <td>E924</td>
  <td>Annotation</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E925.png" width="32" height="32" alt="BackSpaceQWERTYSm" /></td>
  <td>E925</td>
  <td>BackSpaceQWERTYSm</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E926.png" width="32" height="32" alt="BackSpaceQWERTYMd" /></td>
  <td>E926</td>
  <td>BackSpaceQWERTYMd</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E927.png" width="32" height="32" alt="Swipe" /></td>
  <td>E927</td>
  <td>Swipe</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E928.png" width="32" height="32" alt="Fingerprint" /></td>
  <td>E928</td>
  <td>Fingerprint</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E929.png" width="32" height="32" alt="Handwriting" /></td>
  <td>E929</td>
  <td>Handwriting</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E92C.png" width="32" height="32" alt="ChromeBackToWindow" /></td>
  <td>E92C</td>
  <td>ChromeBackToWindow</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E92D.png" width="32" height="32" alt="ChromeFullScreen" /></td>
  <td>E92D</td>
  <td>ChromeFullScreen</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E92E.png" width="32" height="32" alt="KeyboardStandard" /></td>
  <td>E92E</td>
  <td>KeyboardStandard</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E92F.png" width="32" height="32" alt="KeyboardDismiss" /></td>
  <td>E92F</td>
  <td>KeyboardDismiss</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E930.png" width="32" height="32" alt="Completed" /></td>
  <td>E930</td>
  <td>Completed</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E931.png" width="32" height="32" alt="ChromeAnnotate" /></td>
  <td>E931</td>
  <td>ChromeAnnotate</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E932.png" width="32" height="32" alt="Label" /></td>
  <td>E932</td>
  <td>Label</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E933.png" width="32" height="32" alt="IBeam" /></td>
  <td>E933</td>
  <td>IBeam</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E934.png" width="32" height="32" alt="IBeamOutline" /></td>
  <td>E934</td>
  <td>IBeamOutline</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E935.png" width="32" height="32" alt="FlickDown" /></td>
  <td>E935</td>
  <td>FlickDown</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E936.png" width="32" height="32" alt="FlickUp" /></td>
  <td>E936</td>
  <td>FlickUp</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E937.png" width="32" height="32" alt="FlickLeft" /></td>
  <td>E937</td>
  <td>FlickLeft</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E938.png" width="32" height="32" alt="FlickRight" /></td>
  <td>E938</td>
  <td>FlickRight</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E939.png" width="32" height="32" alt="FeedbackApp" /></td>
  <td>E939</td>
  <td>FeedbackApp</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E93C.png" width="32" height="32" alt="MusicAlbum" /></td>
  <td>E93C</td>
  <td>MusicAlbum</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E93E.png" width="32" height="32" alt="Streaming" /></td>
  <td>E93E</td>
  <td>Streaming</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E943.png" width="32" height="32" alt="Code" /></td>
  <td>E943</td>
  <td>Code</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E944.png" width="32" height="32" alt="ReturnToWindow" /></td>
  <td>E944</td>
  <td>ReturnToWindow</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E945.png" width="32" height="32" alt="LightningBolt" /></td>
  <td>E945</td>
  <td>LightningBolt</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E946.png" width="32" height="32" alt="Info" /></td>
  <td>E946</td>
  <td>Info</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E947.png" width="32" height="32" alt="CalculatorMultiply" /></td>
  <td>E947</td>
  <td>CalculatorMultiply</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E948.png" width="32" height="32" alt="CalculatorAddition" /></td>
  <td>E948</td>
  <td>CalculatorAddition</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E949.png" width="32" height="32" alt="CalculatorSubtract" /></td>
  <td>E949</td>
  <td>CalculatorSubtract</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E94A.png" width="32" height="32" alt="CalculatorDivide" /></td>
  <td>E94A</td>
  <td>CalculatorDivide</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E94B.png" width="32" height="32" alt="CalculatorSquareroot" /></td>
  <td>E94B</td>
  <td>CalculatorSquareroot</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E94C.png" width="32" height="32" alt="CalculatorPercentage" /></td>
  <td>E94C</td>
  <td>CalculatorPercentage</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E94D.png" width="32" height="32" alt="CalculatorNegate" /></td>
  <td>E94D</td>
  <td>CalculatorNegate</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E94E.png" width="32" height="32" alt="CalculatorEqualTo" /></td>
  <td>E94E</td>
  <td>CalculatorEqualTo</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E94F.png" width="32" height="32" alt="CalculatorBackspace" /></td>
  <td>E94F</td>
  <td>CalculatorBackspace</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E950.png" width="32" height="32" alt="Component" /></td>
  <td>E950</td>
  <td>Component</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E951.png" width="32" height="32" alt="DMC" /></td>
  <td>E951</td>
  <td>DMC</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E952.png" width="32" height="32" alt="Dock" /></td>
  <td>E952</td>
  <td>Dock</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E953.png" width="32" height="32" alt="MultimediaDMS" /></td>
  <td>E953</td>
  <td>MultimediaDMS</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E954.png" width="32" height="32" alt="MultimediaDVR" /></td>
  <td>E954</td>
  <td>MultimediaDVR</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E955.png" width="32" height="32" alt="MultimediaPMP" /></td>
  <td>E955</td>
  <td>MultimediaPMP</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E956.png" width="32" height="32" alt="PrintfaxPrinterFile" /></td>
  <td>E956</td>
  <td>PrintfaxPrinterFile</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E957.png" width="32" height="32" alt="Sensor" /></td>
  <td>E957</td>
  <td>Sensor</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E958.png" width="32" height="32" alt="StorageOptical" /></td>
  <td>E958</td>
  <td>StorageOptical</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E95A.png" width="32" height="32" alt="Communications" /></td>
  <td>E95A</td>
  <td>Communications</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E95B.png" width="32" height="32" alt="Headset" /></td>
  <td>E95B</td>
  <td>Headset</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E95D.png" width="32" height="32" alt="Projector" /></td>
  <td>E95D</td>
  <td>Projector</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E95E.png" width="32" height="32" alt="Health" /></td>
  <td>E95E</td>
  <td>Health</td>
 </tr>
 <tr><td><img src="images/segoe-mdl/E95F.png" width="32" height="32" alt="Wire" /></td>
  <td>E95F</td>
  <td>Wire</td>
</tr>
<tr><td><img src="images/segoe-mdl/E960.png" width="32" height="32" alt="Webcam2" /></td>
  <td>E960</td>
  <td>Webcam2</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E961.png" width="32" height="32" alt="Input" /></td>
  <td>E961</td>
  <td>Input</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E962.png" width="32" height="32" alt="Mouse" /></td>
  <td>E962</td>
  <td>Mouse</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E963.png" width="32" height="32" alt="Smartcard" /></td>
  <td>E963</td>
  <td>Smartcard</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E964.png" width="32" height="32" alt="SmartcardVirtual" /></td>
  <td>E964</td>
  <td>SmartcardVirtual</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E965.png" width="32" height="32" alt="MediaStorageTower" /></td>
  <td>E965</td>
  <td>MediaStorageTower</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E966.png" width="32" height="32" alt="ReturnKeySm" /></td>
  <td>E966</td>
  <td>ReturnKeySm</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E967.png" width="32" height="32" alt="GameConsole" /></td>
  <td>E967</td>
  <td>GameConsole</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E968.png" width="32" height="32" alt="Network" /></td>
  <td>E968</td>
  <td>Network</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E969.png" width="32" height="32" alt="StorageNetworkWireless" /></td>
  <td>E969</td>
  <td>StorageNetworkWireless</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E96A.png" width="32" height="32" alt="StorageTape" /></td>
  <td>E96A</td>
  <td>StorageTape</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E96D.png" width="32" height="32" alt="ChevronUpSmall" /></td>
  <td>E96D</td>
  <td>ChevronUpSmall</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E96E.png" width="32" height="32" alt="ChevronDownSmall" /></td>
  <td>E96E</td>
  <td>ChevronDownSmall</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E96F.png" width="32" height="32" alt="ChevronLeftSmall" /></td>
  <td>E96F</td>
  <td>ChevronLeftSmall</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E970.png" width="32" height="32" alt="ChevronRightSmall" /></td>
  <td>E970</td>
  <td>ChevronRightSmall</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E971.png" width="32" height="32" alt="ChevronUpMed" /></td>
  <td>E971</td>
  <td>ChevronUpMed</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E972.png" width="32" height="32" alt="ChevronDownMed" /></td>
  <td>E972</td>
  <td>ChevronDownMed</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E973.png" width="32" height="32" alt="ChevronLeftMed" /></td>
  <td>E973</td>
  <td>ChevronLeftMed</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E974.png" width="32" height="32" alt="ChevronRightMed" /></td>
  <td>E974</td>
  <td>ChevronRightMed</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E975.png" width="32" height="32" alt="Devices2" /></td>
  <td>E975</td>
  <td>Devices2</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E976.png" width="32" height="32" alt="ExpandTile" /></td>
  <td>E976</td>
  <td>ExpandTile</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E977.png" width="32" height="32" alt="PC1" /></td>
  <td>E977</td>
  <td>PC1</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E978.png" width="32" height="32" alt="PresenceChicklet" /></td>
  <td>E978</td>
  <td>PresenceChicklet</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E979.png" width="32" height="32" alt="PresenceChickletVideo" /></td>
  <td>E979</td>
  <td>PresenceChickletVideo</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E97A.png" width="32" height="32" alt="Reply" /></td>
  <td>E97A</td>
  <td>Reply</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E97B.png" width="32" height="32" alt="SetTile" /></td>
  <td>E97B</td>
  <td>SetTile</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E97C.png" width="32" height="32" alt="Type" /></td>
  <td>E97C</td>
  <td>Type</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E97D.png" width="32" height="32" alt="Korean" /></td>
  <td>E97D</td>
  <td>Korean</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E97E.png" width="32" height="32" alt="HalfAlpha" /></td>
  <td>E97E</td>
  <td>HalfAlpha</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E97F.png" width="32" height="32" alt="FullAlpha" /></td>
  <td>E97F</td>
  <td>FullAlpha</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E980.png" width="32" height="32" alt="Key12On" /></td>
  <td>E980</td>
  <td>Key12On</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E981.png" width="32" height="32" alt="ChineseChangjie" /></td>
  <td>E981</td>
  <td>ChineseChangjie</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E982.png" width="32" height="32" alt="QWERTYOn" /></td>
  <td>E982</td>
  <td>QWERTYOn</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E983.png" width="32" height="32" alt="QWERTYOff" /></td>
  <td>E983</td>
  <td>QWERTYOff</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E984.png" width="32" height="32" alt="ChineseQuick" /></td>
  <td>E984</td>
  <td>ChineseQuick</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E985.png" width="32" height="32" alt="Japanese" /></td>
  <td>E985</td>
  <td>Japanese</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E986.png" width="32" height="32" alt="FullHiragana" /></td>
  <td>E986</td>
  <td>FullHiragana</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E987.png" width="32" height="32" alt="FullKatakana" /></td>
  <td>E987</td>
  <td>FullKatakana</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E988.png" width="32" height="32" alt="HalfKatakana" /></td>
  <td>E988</td>
  <td>HalfKatakana</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E989.png" width="32" height="32" alt="ChineseBoPoMoFo" /></td>
  <td>E989</td>
  <td>ChineseBoPoMoFo</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E98A.png" width="32" height="32" alt="ChinesePinyin" /></td>
  <td>E98A</td>
  <td>ChinesePinyin</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E98F.png" width="32" height="32" alt="ConstructionCone" /></td>
  <td>E98F</td>
  <td>ConstructionCone</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E990.png" width="32" height="32" alt="XboxOneConsole" /></td>
  <td>E990</td>
  <td>XboxOneConsole</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E992.png" width="32" height="32" alt="Volume0" /></td>
  <td>E992</td>
  <td>Volume0</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E993.png" width="32" height="32" alt="Volume1" /></td>
  <td>E993</td>
  <td>Volume1</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E994.png" width="32" height="32" alt="Volume2" /></td>
  <td>E994</td>
  <td>Volume2</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E995.png" width="32" height="32" alt="Volume3" /></td>
  <td>E995</td>
  <td>Volume3</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E996.png" width="32" height="32" alt="BatteryUnknown" /></td>
  <td>E996</td>
  <td>BatteryUnknown</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E998.png" width="32" height="32" alt="WifiAttentionOverlay" /></td>
  <td>E998</td>
  <td>WifiAttentionOverlay</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E99A.png" width="32" height="32" alt="Robot" /></td>
  <td>E99A</td>
  <td>Robot</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E9A1.png" width="32" height="32" alt="TapAndSend" /></td>
  <td>E9A1</td>
  <td>TapAndSend</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E9A6.png" width="32" height="32" alt="FitPage" /></td>
  <td>E9A6</td>
  <td>FitPage</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E9A8.png" width="32" height="32" alt="PasswordKeyShow" /></td>
  <td>E9A8</td>
  <td>PasswordKeyShow</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E9A9.png" width="32" height="32" alt="PasswordKeyHide" /></td>
  <td>E9A9</td>
  <td>PasswordKeyHide</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E9AA.png" width="32" height="32" alt="BidiLtr" /></td>
  <td>E9AA</td>
  <td>BidiLtr</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E9AB.png" width="32" height="32" alt="BidiRtl" /></td>
  <td>E9AB</td>
  <td>BidiRtl</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E9AC.png" width="32" height="32" alt="ForwardSm" /></td>
  <td>E9AC</td>
  <td>ForwardSm</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E9AD.png" width="32" height="32" alt="CommaKey" /></td>
  <td>E9AD</td>
  <td>CommaKey</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E9AE.png" width="32" height="32" alt="DashKey" /></td>
  <td>E9AE</td>
  <td>DashKey</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E9AF.png" width="32" height="32" alt="DullSoundKey" /></td>
  <td>E9AF</td>
  <td>DullSoundKey</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E9B0.png" width="32" height="32" alt="HalfDullSound" /></td>
  <td>E9B0</td>
  <td>HalfDullSound</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E9B1.png" width="32" height="32" alt="RightDoubleQuote" /></td>
  <td>E9B1</td>
  <td>RightDoubleQuote</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E9B2.png" width="32" height="32" alt="LeftDoubleQuote" /></td>
  <td>E9B2</td>
  <td>LeftDoubleQuote</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E9B3.png" width="32" height="32" alt="PuncKeyRightBottom" /></td>
  <td>E9B3</td>
  <td>PuncKeyRightBottom</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E9B4.png" width="32" height="32" alt="PuncKey1" /></td>
  <td>E9B4</td>
  <td>PuncKey1</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E9B5.png" width="32" height="32" alt="PuncKey2" /></td>
  <td>E9B5</td>
  <td>PuncKey2</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E9B6.png" width="32" height="32" alt="PuncKey3" /></td>
  <td>E9B6</td>
  <td>PuncKey3</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E9B7.png" width="32" height="32" alt="PuncKey4" /></td>
  <td>E9B7</td>
  <td>PuncKey4</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E9B8.png" width="32" height="32" alt="PuncKey5" /></td>
  <td>E9B8</td>
  <td>PuncKey5</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E9B9.png" width="32" height="32" alt="PuncKey6" /></td>
  <td>E9B9</td>
  <td>PuncKey6</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E9BA.png" width="32" height="32" alt="PuncKey9" /></td>
  <td>E9BA</td>
  <td>PuncKey9</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E9BB.png" width="32" height="32" alt="PuncKey7" /></td>
  <td>E9BB</td>
  <td>PuncKey7</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E9BC.png" width="32" height="32" alt="PuncKey8" /></td>
  <td>E9BC</td>
  <td>PuncKey8</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E9CA.png" width="32" height="32" alt="Frigid" /></td>
  <td>E9CA</td>
  <td>Frigid</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E9CE.png" width="32" height="32" alt="Unknown" /></td>
  <td>E9CE</td>
  <td>Unknown</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E9D2.png" width="32" height="32" alt="AreaChart" /></td>
  <td>E9D2</td>
  <td>AreaChart</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E9D5.png" width="32" height="32" alt="CheckList" /></td>
  <td>E9D5</td>
  <td>CheckList</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E9D9.png" width="32" height="32" alt="Diagnostic" /></td>
  <td>E9D9</td>
  <td>Diagnostic</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E9E9.png" width="32" height="32" alt="Equalizer" /></td>
  <td>E9E9</td>
  <td>Equalizer</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E9F3.png" width="32" height="32" alt="Process" /></td>
  <td>E9F3</td>
  <td>Process</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E9F5.png" width="32" height="32" alt="Processing" /></td>
  <td>E9F5</td>
  <td>Processing</td>
 </tr>
<tr><td><img src="images/segoe-mdl/E9F9.png" width="32" height="32" alt="ReportDocument" /></td>
  <td>E9F9</td>
  <td>ReportDocument</td>
 </tr>
</table>

 ### PUA EA00-EC00

The following table of glyphs displays unicode points prefixed from EA-  to EC-.

[Back to top](#icon-list)

</br>
 <table>
 <tr>
  <td>Glyph</td>
  <td>Unicode point</td>
  <td>Description</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA0C.png" width="32" height="32" alt="VideoSolid" /></td>
  <td>EA0C</td>
  <td>VideoSolid</td>
 </tr>
 <tr><td><img src="images/segoe-mdl/EA0D.png" width="32" height="32" alt="MixedMediaBadge" /></td>
  <td>EA0D</td>
  <td>MixedMediaBadge</td>
</tr>
<tr><td><img src="images/segoe-mdl/EA14.png" width="32" height="32" alt="DisconnectDisplay" /></td>
  <td>EA14</td>
  <td>DisconnectDisplay</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA18.png" width="32" height="32" alt="Shield" /></td>
  <td>EA18</td>
  <td>Shield</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA1F.png" width="32" height="32" alt="Info2" /></td>
  <td>EA1F</td>
  <td>Info2</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA21.png" width="32" height="32" alt="ActionCenterAsterisk" /></td>
  <td>EA21</td>
  <td>ActionCenterAsterisk</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA24.png" width="32" height="32" alt="Beta" /></td>
  <td>EA24</td>
  <td>Beta</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA35.png" width="32" height="32" alt="SaveCopy" /></td>
  <td>EA35</td>
  <td>SaveCopy</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA37.png" width="32" height="32" alt="List" /></td>
  <td>EA37</td>
  <td>List</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA38.png" width="32" height="32" alt="Asterisk" /></td>
  <td>EA38</td>
  <td>Asterisk</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA39.png" width="32" height="32" alt="ErrorBadge" /></td>
  <td>EA39</td>
  <td>ErrorBadge</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA3A.png" width="32" height="32" alt="CircleRing" /></td>
  <td>EA3A</td>
  <td>CircleRing</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA3B.png" width="32" height="32" alt="CircleFill" /></td>
  <td>EA3B</td>
  <td>CircleFill</td>
 </tr>
 <tr><td><img src="images/segoe-mdl/EA3C.png" width="32" height="32" alt="MergeCall" /></td>
  <td>EA3C</td>
  <td>MergeCall</td>
</tr>
<tr><td><img src="images/segoe-mdl/EA3D.png" width="32" height="32" alt="PrivateCall" /></td>
  <td>EA3D</td>
  <td>PrivateCall</td>
</tr>
<tr><td><img src="images/segoe-mdl/EA3F.png" width="32" height="32" alt="Record2" /></td>
  <td>EA3F</td>
  <td>Record2</td>
</tr>
<tr><td><img src="images/segoe-mdl/EA40.png" width="32" height="32" alt="AllAppsMirrored" /></td>
  <td>EA40</td>
  <td>AllAppsMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA41.png" width="32" height="32" alt="BookmarksMirrored" /></td>
  <td>EA41</td>
  <td>BookmarksMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA42.png" width="32" height="32" alt="BulletedListMirrored" /></td>
  <td>EA42</td>
  <td>BulletedListMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA43.png" width="32" height="32" alt="CallForwardInternationalMirrored" /></td>
  <td>EA43</td>
  <td>CallForwardInternationalMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA44.png" width="32" height="32" alt="CallForwardRoamingMirrored" /></td>
  <td>EA44</td>
  <td>CallForwardRoamingMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA47.png" width="32" height="32" alt="ChromeBackMirrored" /></td>
  <td>EA47</td>
  <td>ChromeBackMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA48.png" width="32" height="32" alt="ClearSelectionMirrored" /></td>
  <td>EA48</td>
  <td>ClearSelectionMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA49.png" width="32" height="32" alt="ClosePaneMirrored" /></td>
  <td>EA49</td>
  <td>ClosePaneMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA4A.png" width="32" height="32" alt="ContactInfoMirrored" /></td>
  <td>EA4A</td>
  <td>ContactInfoMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA4B.png" width="32" height="32" alt="DockRightMirrored" /></td>
  <td>EA4B</td>
  <td>DockRightMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA4C.png" width="32" height="32" alt="DockLeftMirrored" /></td>
  <td>EA4C</td>
  <td>DockLeftMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA4E.png" width="32" height="32" alt="ExpandTileMirrored" /></td>
  <td>EA4E</td>
  <td>ExpandTileMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA4F.png" width="32" height="32" alt="GoMirrored" /></td>
  <td>EA4F</td>
  <td>GoMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA50.png" width="32" height="32" alt="GripperResizeMirrored" /></td>
  <td>EA50</td>
  <td>GripperResizeMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA51.png" width="32" height="32" alt="HelpMirrored" /></td>
  <td>EA51</td>
  <td>HelpMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA52.png" width="32" height="32" alt="ImportMirrored" /></td>
  <td>EA52</td>
  <td>ImportMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA53.png" width="32" height="32" alt="ImportAllMirrored" /></td>
  <td>EA53</td>
  <td>ImportAllMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA54.png" width="32" height="32" alt="LeaveChatMirrored" /></td>
  <td>EA54</td>
  <td>LeaveChatMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA55.png" width="32" height="32" alt="ListMirrored" /></td>
  <td>EA55</td>
  <td>ListMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA56.png" width="32" height="32" alt="MailForwardMirrored" /></td>
  <td>EA56</td>
  <td>MailForwardMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA57.png" width="32" height="32" alt="MailReplyMirrored" /></td>
  <td>EA57</td>
  <td>MailReplyMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA58.png" width="32" height="32" alt="MailReplyAllMirrored" /></td>
  <td>EA58</td>
  <td>MailReplyAllMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA5B.png" width="32" height="32" alt="OpenPaneMirrored" /></td>
  <td>EA5B</td>
  <td>OpenPaneMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA5C.png" width="32" height="32" alt="OpenWithMirrored" /></td>
  <td>EA5C</td>
  <td>OpenWithMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA5E.png" width="32" height="32" alt="ParkingLocationMirrored" /></td>
  <td>EA5E</td>
  <td>ParkingLocationMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA5F.png" width="32" height="32" alt="ResizeMouseMediumMirrored" /></td>
  <td>EA5F</td>
  <td>ResizeMouseMediumMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA60.png" width="32" height="32" alt="ResizeMouseSmallMirrored" /></td>
  <td>EA60</td>
  <td>ResizeMouseSmallMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA61.png" width="32" height="32" alt="ResizeMouseTallMirrored" /></td>
  <td>EA61</td>
  <td>ResizeMouseTallMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA62.png" width="32" height="32" alt="ResizeTouchNarrowerMirrored" /></td>
  <td>EA62</td>
  <td>ResizeTouchNarrowerMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA63.png" width="32" height="32" alt="SendMirrored" /></td>
  <td>EA63</td>
  <td>SendMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA64.png" width="32" height="32" alt="SendFillMirrored" /></td>
  <td>EA64</td>
  <td>SendFillMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA65.png" width="32" height="32" alt="ShowResultsMirrored" /></td>
  <td>EA65</td>
  <td>ShowResultsMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA69.png" width="32" height="32" alt="Media" /></td>
  <td>EA69</td>
  <td>Media</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA6A.png" width="32" height="32" alt="SyncError" /></td>
  <td>EA6A</td>
  <td>SyncError</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA6C.png" width="32" height="32" alt="Devices3" /></td>
  <td>EA6C</td>
  <td>Devices3</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA79.png" width="32" height="32" alt="SlowMotionOn" /></td>
  <td>EA79</td>
  <td>SlowMotionOn</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA80.png" width="32" height="32" alt="Lightbulb" /></td>
  <td>EA80</td>
  <td>Lightbulb</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA81.png" width="32" height="32" alt="StatusCircle" /></td>
  <td>EA81</td>
  <td>StatusCircle</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA82.png" width="32" height="32" alt="StatusTriangle" /></td>
  <td>EA82</td>
  <td>StatusTriangle</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA83.png" width="32" height="32" alt="StatusError" /></td>
  <td>EA83</td>
  <td>StatusError</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA84.png" width="32" height="32" alt="StatusWarning" /></td>
  <td>EA84</td>
  <td>StatusWarning</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA86.png" width="32" height="32" alt="Puzzle" /></td>
  <td>EA86</td>
  <td>Puzzle</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA89.png" width="32" height="32" alt="CalendarSolid" /></td>
  <td>EA89</td>
  <td>CalendarSolid</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA8A.png" width="32" height="32" alt="HomeSolid" /></td>
  <td>EA8A</td>
  <td>HomeSolid</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA8B.png" width="32" height="32" alt="ParkingLocationSolid" /></td>
  <td>EA8B</td>
  <td>ParkingLocationSolid</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA8C.png" width="32" height="32" alt="ContactSolid" /></td>
  <td>EA8C</td>
  <td>ContactSolid</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA8D.png" width="32" height="32" alt="ConstructionSolid" /></td>
  <td>EA8D</td>
  <td>ConstructionSolid</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA8E.png" width="32" height="32" alt="AccidentSolid" /></td>
  <td>EA8E</td>
  <td>AccidentSolid</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA8F.png" width="32" height="32" alt="Ringer" /></td>
  <td>EA8F</td>
  <td>Ringer</td>
 </tr>
 <tr><td><img src="images/segoe-mdl/EA90.png" width="32" height="32" alt="PDF" /></td>
  <td>EA90</td>
  <td>PDF</td>
</tr>
<tr><td><img src="images/segoe-mdl/EA91.png" width="32" height="32" alt="ThoughtBubble" /></td>
  <td>EA91</td>
  <td>ThoughtBubble</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA92.png" width="32" height="32" alt="HeartBroken" /></td>
  <td>EA92</td>
  <td>HeartBroken</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA93.png" width="32" height="32" alt="BatteryCharging10" /></td>
  <td>EA93</td>
  <td>BatteryCharging10</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA94.png" width="32" height="32" alt="BatterySaver9" /></td>
  <td>EA94</td>
  <td>BatterySaver9</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA95.png" width="32" height="32" alt="BatterySaver10" /></td>
  <td>EA95</td>
  <td>BatterySaver10</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA97.png" width="32" height="32" alt="CallForwardingMirrored" /></td>
  <td>EA97</td>
  <td>CallForwardingMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA98.png" width="32" height="32" alt="MultiSelectMirrored" /></td>
  <td>EA98</td>
  <td>MultiSelectMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EA99.png" width="32" height="32" alt="Broom" /></td>
  <td>EA99</td>
  <td>Broom</td>
 </tr>
 <tr><td><img src="images/segoe-mdl/EAC2.png" width="32" height="32" alt="ForwardCall" /></td>
  <td>EAC2</td>
  <td>ForwardCall</td>
</tr>
<tr><td><img src="images/segoe-mdl/EADF.png" width="32" height="32" alt="Trackers" /></td>
  <td>EADF</td>
  <td>Trackers</td>
 </tr>
 <tr><td><img src="images/segoe-mdl/EAFC.png" width="32" height="32" alt="Market" /></td>
  <td>EAFC</td>
  <td>Market</td>
</tr>
<tr><td><img src="images/segoe-mdl/EB05.png" width="32" height="32" alt="PieSingle" /></td>
  <td>EB05</td>
  <td>PieSingle</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EB0F.png" width="32" height="32" alt="StockDown" /></td>
  <td>EB0F</td>
  <td>StockDown</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EB11.png" width="32" height="32" alt="StockUp" /></td>
  <td>EB11</td>
  <td>StockUp</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EB3C.png" width="32" height="32" alt="Design" /></td>
  <td>EB3C</td>
  <td>Design</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EB41.png" width="32" height="32" alt="Website" /></td>
  <td>EB41</td>
  <td>Website</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EB42.png" width="32" height="32" alt="Drop" /></td>
  <td>EB42</td>
  <td>Drop</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EB44.png" width="32" height="32" alt="Radar" /></td>
  <td>EB44</td>
  <td>Radar</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EB47.png" width="32" height="32" alt="BusSolid" /></td>
  <td>EB47</td>
  <td>BusSolid</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EB48.png" width="32" height="32" alt="FerrySolid" /></td>
  <td>EB48</td>
  <td>FerrySolid</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EB49.png" width="32" height="32" alt="StartPointSolid" /></td>
  <td>EB49</td>
  <td>StartPointSolid</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EB4A.png" width="32" height="32" alt="StopPointSolid" /></td>
  <td>EB4A</td>
  <td>StopPointSolid</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EB4B.png" width="32" height="32" alt="EndPointSolid" /></td>
  <td>EB4B</td>
  <td>EndPointSolid</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EB4C.png" width="32" height="32" alt="AirplaneSolid" /></td>
  <td>EB4C</td>
  <td>AirplaneSolid</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EB4D.png" width="32" height="32" alt="TrainSolid" /></td>
  <td>EB4D</td>
  <td>TrainSolid</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EB4E.png" width="32" height="32" alt="WorkSolid" /></td>
  <td>EB4E</td>
  <td>WorkSolid</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EB4F.png" width="32" height="32" alt="ReminderFill" /></td>
  <td>EB4F</td>
  <td>ReminderFill</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EB50.png" width="32" height="32" alt="Reminder" /></td>
  <td>EB50</td>
  <td>Reminder</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EB51.png" width="32" height="32" alt="Heart" /></td>
  <td>EB51</td>
  <td>Heart</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EB52.png" width="32" height="32" alt="HeartFill" /></td>
  <td>EB52</td>
  <td>HeartFill</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EB55.png" width="32" height="32" alt="EthernetError" /></td>
  <td>EB55</td>
  <td>EthernetError</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EB56.png" width="32" height="32" alt="EthernetWarning" /></td>
  <td>EB56</td>
  <td>EthernetWarning</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EB57.png" width="32" height="32" alt="StatusConnecting1" /></td>
  <td>EB57</td>
  <td>StatusConnecting1</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EB58.png" width="32" height="32" alt="StatusConnecting2" /></td>
  <td>EB58</td>
  <td>StatusConnecting2</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EB59.png" width="32" height="32" alt="StatusUnsecure" /></td>
  <td>EB59</td>
  <td>StatusUnsecure</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EB5A.png" width="32" height="32" alt="WifiError0" /></td>
  <td>EB5A</td>
  <td>WifiError0</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EB5B.png" width="32" height="32" alt="WifiError1" /></td>
  <td>EB5B</td>
  <td>WifiError1</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EB5C.png" width="32" height="32" alt="WifiError2" /></td>
  <td>EB5C</td>
  <td>WifiError2</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EB5D.png" width="32" height="32" alt="WifiError3" /></td>
  <td>EB5D</td>
  <td>WifiError3</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EB5E.png" width="32" height="32" alt="WifiError4" /></td>
  <td>EB5E</td>
  <td>WifiError4</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EB5F.png" width="32" height="32" alt="WifiWarning0" /></td>
  <td>EB5F</td>
  <td>WifiWarning0</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EB60.png" width="32" height="32" alt="WifiWarning1" /></td>
  <td>EB60</td>
  <td>WifiWarning1</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EB61.png" width="32" height="32" alt="WifiWarning2" /></td>
  <td>EB61</td>
  <td>WifiWarning2</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EB62.png" width="32" height="32" alt="WifiWarning3" /></td>
  <td>EB62</td>
  <td>WifiWarning3</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EB63.png" width="32" height="32" alt="WifiWarning4" /></td>
  <td>EB63</td>
  <td>WifiWarning4</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EB66.png" width="32" height="32" alt="Devices4" /></td>
  <td>EB66</td>
  <td>Devices4</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EB67.png" width="32" height="32" alt="NUIIris" /></td>
  <td>EB67</td>
  <td>NUIIris</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EB68.png" width="32" height="32" alt="NUIFace" /></td>
  <td>EB68</td>
  <td>NUIFace</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EB7E.png" width="32" height="32" alt="EditMirrored" /></td>
  <td>EB7E</td>
  <td>EditMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EB82.png" width="32" height="32" alt="NUIFPStartSlideHand " /></td>
  <td>EB82</td>
  <td>NUIFPStartSlideHand </td>
 </tr>
<tr><td><img src="images/segoe-mdl/EB83.png" width="32" height="32" alt="NUIFPStartSlideAction " /></td>
  <td>EB83</td>
  <td>NUIFPStartSlideAction </td>
 </tr>
<tr><td><img src="images/segoe-mdl/EB84.png" width="32" height="32" alt="NUIFPContinueSlideHand " /></td>
  <td>EB84</td>
  <td>NUIFPContinueSlideHand </td>
 </tr>
<tr><td><img src="images/segoe-mdl/EB85.png" width="32" height="32" alt="NUIFPContinueSlideAction" /></td>
  <td>EB85</td>
  <td>NUIFPContinueSlideAction</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EB86.png" width="32" height="32" alt="NUIFPRollRightHand " /></td>
  <td>EB86</td>
  <td>NUIFPRollRightHand </td>
 </tr>
<tr><td><img src="images/segoe-mdl/EB87.png" width="32" height="32" alt="NUIFPRollRightHandAction" /></td>
  <td>EB87</td>
  <td>NUIFPRollRightHandAction</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EB88.png" width="32" height="32" alt="NUIFPRollLeftHand " /></td>
  <td>EB88</td>
  <td>NUIFPRollLeftHand </td>
 </tr>
<tr><td><img src="images/segoe-mdl/EB89.png" width="32" height="32" alt="NUIFPRollLeftAction" /></td>
  <td>EB89</td>
  <td>NUIFPRollLeftAction</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EB8A.png" width="32" height="32" alt="NUIFPPressHand " /></td>
  <td>EB8A</td>
  <td>NUIFPPressHand </td>
 </tr>
<tr><td><img src="images/segoe-mdl/EB8B.png" width="32" height="32" alt="NUIFPPressAction" /></td>
  <td>EB8B</td>
  <td>NUIFPPressAction</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EB8C.png" width="32" height="32" alt="NUIFPPressRepeatHand " /></td>
  <td>EB8C</td>
  <td>NUIFPPressRepeatHand </td>
 </tr>
<tr><td><img src="images/segoe-mdl/EB8D.png" width="32" height="32" alt="NUIFPPressRepeatAction" /></td>
  <td>EB8D</td>
  <td>NUIFPPressRepeatAction</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EB90.png" width="32" height="32" alt="StatusErrorFull" /></td>
  <td>EB90</td>
  <td>StatusErrorFull</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EB91.png" width="32" height="32" alt="TaskViewExpanded" /></td>
  <td>EB91</td>
  <td>TaskViewExpanded</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EB95.png" width="32" height="32" alt="Certificate" /></td>
  <td>EB95</td>
  <td>Certificate</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EB96.png" width="32" height="32" alt="BackSpaceQWERTYLg" /></td>
  <td>EB96</td>
  <td>BackSpaceQWERTYLg</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EB97.png" width="32" height="32" alt="ReturnKeyLg" /></td>
  <td>EB97</td>
  <td>ReturnKeyLg</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EB9D.png" width="32" height="32" alt="FastForward" /></td>
  <td>EB9D</td>
  <td>FastForward</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EB9E.png" width="32" height="32" alt="Rewind" /></td>
  <td>EB9E</td>
  <td>Rewind</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EB9F.png" width="32" height="32" alt="Photo2" /></td>
  <td>EB9F</td>
  <td>Photo2</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EBA0.png" width="32" height="32" alt=" MobBattery0" /></td>
  <td>EBA0</td>
  <td> MobBattery0</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EBA1.png" width="32" height="32" alt=" MobBattery1" /></td>
  <td>EBA1</td>
  <td> MobBattery1</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EBA2.png" width="32" height="32" alt=" MobBattery2" /></td>
  <td>EBA2</td>
  <td> MobBattery2</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EBA3.png" width="32" height="32" alt=" MobBattery3" /></td>
  <td>EBA3</td>
  <td> MobBattery3</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EBA4.png" width="32" height="32" alt=" MobBattery4" /></td>
  <td>EBA4</td>
  <td> MobBattery4</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EBA5.png" width="32" height="32" alt=" MobBattery5" /></td>
  <td>EBA5</td>
  <td> MobBattery5</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EBA6.png" width="32" height="32" alt=" MobBattery6" /></td>
  <td>EBA6</td>
  <td> MobBattery6</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EBA7.png" width="32" height="32" alt=" MobBattery7" /></td>
  <td>EBA7</td>
  <td> MobBattery7</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EBA8.png" width="32" height="32" alt=" MobBattery8" /></td>
  <td>EBA8</td>
  <td> MobBattery8</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EBA9.png" width="32" height="32" alt=" MobBattery9" /></td>
  <td>EBA9</td>
  <td> MobBattery9</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EBAA.png" width="32" height="32" alt="MobBattery10" /></td>
  <td>EBAA</td>
  <td>MobBattery10</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EBAB.png" width="32" height="32" alt=" MobBatteryCharging0" /></td>
  <td>EBAB</td>
  <td> MobBatteryCharging0</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EBAC.png" width="32" height="32" alt=" MobBatteryCharging1" /></td>
  <td>EBAC</td>
  <td> MobBatteryCharging1</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EBAD.png" width="32" height="32" alt=" MobBatteryCharging2" /></td>
  <td>EBAD</td>
  <td> MobBatteryCharging2</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EBAE.png" width="32" height="32" alt=" MobBatteryCharging3" /></td>
  <td>EBAE</td>
  <td> MobBatteryCharging3</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EBAF.png" width="32" height="32" alt=" MobBatteryCharging4" /></td>
  <td>EBAF</td>
  <td> MobBatteryCharging4</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EBB0.png" width="32" height="32" alt=" MobBatteryCharging5" /></td>
  <td>EBB0</td>
  <td> MobBatteryCharging5</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EBB1.png" width="32" height="32" alt=" MobBatteryCharging6" /></td>
  <td>EBB1</td>
  <td> MobBatteryCharging6</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EBB2.png" width="32" height="32" alt=" MobBatteryCharging7" /></td>
  <td>EBB2</td>
  <td> MobBatteryCharging7</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EBB3.png" width="32" height="32" alt=" MobBatteryCharging8" /></td>
  <td>EBB3</td>
  <td> MobBatteryCharging8</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EBB4.png" width="32" height="32" alt=" MobBatteryCharging9" /></td>
  <td>EBB4</td>
  <td> MobBatteryCharging9</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EBB5.png" width="32" height="32" alt=" MobBatteryCharging10" /></td>
  <td>EBB5</td>
  <td> MobBatteryCharging10</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EBB6.png" width="32" height="32" alt=" MobBatterySaver0" /></td>
  <td>EBB6</td>
  <td> MobBatterySaver0</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EBB7.png" width="32" height="32" alt=" MobBatterySaver1" /></td>
  <td>EBB7</td>
  <td> MobBatterySaver1</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EBB8.png" width="32" height="32" alt=" MobBatterySaver2" /></td>
  <td>EBB8</td>
  <td> MobBatterySaver2</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EBB9.png" width="32" height="32" alt=" MobBatterySaver3" /></td>
  <td>EBB9</td>
  <td> MobBatterySaver3</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EBBA.png" width="32" height="32" alt=" MobBatterySaver4" /></td>
  <td>EBBA</td>
  <td> MobBatterySaver4</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EBBB.png" width="32" height="32" alt=" MobBatterySaver5" /></td>
  <td>EBBB</td>
  <td> MobBatterySaver5</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EBBC.png" width="32" height="32" alt=" MobBatterySaver6" /></td>
  <td>EBBC</td>
  <td> MobBatterySaver6</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EBBD.png" width="32" height="32" alt=" MobBatterySaver7" /></td>
  <td>EBBD</td>
  <td> MobBatterySaver7</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EBBE.png" width="32" height="32" alt=" MobBatterySaver8" /></td>
  <td>EBBE</td>
  <td> MobBatterySaver8</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EBBF.png" width="32" height="32" alt=" MobBatterySaver9" /></td>
  <td>EBBF</td>
  <td> MobBatterySaver9</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EBC0.png" width="32" height="32" alt=" MobBatterySaver10" /></td>
  <td>EBC0</td>
  <td> MobBatterySaver10</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EBC3.png" width="32" height="32" alt="DictionaryCloud" /></td>
  <td>EBC3</td>
  <td>DictionaryCloud</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EBC4.png" width="32" height="32" alt="ResetDrive" /></td>
  <td>EBC4</td>
  <td>ResetDrive</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EBC5.png" width="32" height="32" alt="VolumeBars" /></td>
  <td>EBC5</td>
  <td>VolumeBars</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EBC6.png" width="32" height="32" alt="Project" /></td>
  <td>EBC6</td>
  <td>Project</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EBD2.png" width="32" height="32" alt="AdjustHologram" /></td>
  <td>EBD2</td>
  <td>AdjustHologram</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EBD4.png" width="32" height="32" alt="EBD4 WifiCallBars" /></td>
  <td>EBD4</td>
  <td>WifiCallBars</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EBD5.png" width="32" height="32" alt="EBD5 WifiCall0" /></td>
  <td>EBD5</td>
  <td>WifiCall0</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EBD6.png" width="32" height="32" alt="EBD6 WifiCall1" /></td>
  <td>EBD6</td>
  <td>WifiCall1</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EBD7.png" width="32" height="32" alt="EBD7 WifiCall2" /></td>
  <td>EBD7</td>
  <td>WifiCall2</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EBD8.png" width="32" height="32" alt="EBD8 WifiCall3" /></td>
  <td>EBD8</td>
  <td>WifiCall3</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EBD9.png" width="32" height="32" alt="EBD9 WifiCall4" /></td>
  <td>EBD9</td>
  <td>WifiCall4</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EBDA.png" width="32" height="32" alt="Family" /></td>
  <td>EBDA</td>
  <td>Family</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EBDB.png" width="32" height="32" alt="LockFeedback" /></td>
  <td>EBDB</td>
  <td>LockFeedback</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EBDE.png" width="32" height="32" alt="DeviceDiscovery" /></td>
  <td>EBDE</td>
  <td>DeviceDiscovery</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EBE6.png" width="32" height="32" alt="WindDirection" /></td>
  <td>EBE6</td>
  <td>WindDirection</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EBE7.png" width="32" height="32" alt="RightArrowKeyTime0" /></td>
  <td>EBE7</td>
  <td>RightArrowKeyTime0</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EBE8.png" width="32" height="32" alt="Bug" /></td>
  <td>EBE8</td>
  <td>Bug</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EBFC.png" width="32" height="32" alt="TabletMode" /></td>
  <td>EBFC</td>
  <td>TabletMode</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EBFD.png" width="32" height="32" alt="StatusCircleLeft" /></td>
  <td>EBFD</td>
  <td>StatusCircleLeft</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EBFE.png" width="32" height="32" alt="StatusTriangleLeft" /></td>
  <td>EBFE</td>
  <td>StatusTriangleLeft</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EBFF.png" width="32" height="32" alt="StatusErrorLeft" /></td>
  <td>EBFF</td>
  <td>StatusErrorLeft</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC00.png" width="32" height="32" alt="StatusWarningLeft" /></td>
  <td>EC00</td>
  <td>StatusWarningLeft</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC02.png" width="32" height="32" alt="MobBatteryUnknown" /></td>
  <td>EC02</td>
  <td>MobBatteryUnknown</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC05.png" width="32" height="32" alt="NetworkTower" /></td>
  <td>EC05</td>
  <td>NetworkTower</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC06.png" width="32" height="32" alt="CityNext" /></td>
  <td>EC06</td>
  <td>CityNext</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC07.png" width="32" height="32" alt="CityNext2" /></td>
  <td>EC07</td>
  <td>CityNext2</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC08.png" width="32" height="32" alt="Courthouse" /></td>
  <td>EC08</td>
  <td>Courthouse</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC09.png" width="32" height="32" alt="Groceries" /></td>
  <td>EC09</td>
  <td>Groceries</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC0A.png" width="32" height="32" alt="Sustainable" /></td>
  <td>EC0A</td>
  <td>Sustainable</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC0B.png" width="32" height="32" alt="BuildingEnergy" /></td>
  <td>EC0B</td>
  <td>BuildingEnergy</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC11.png" width="32" height="32" alt="ToggleFilled" /></td>
  <td>EC11</td>
  <td>ToggleFilled</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC12.png" width="32" height="32" alt="ToggleBorder" /></td>
  <td>EC12</td>
  <td>ToggleBorder</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC13.png" width="32" height="32" alt="SliderThumb" /></td>
  <td>EC13</td>
  <td>SliderThumb</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC14.png" width="32" height="32" alt="ToggleThumb" /></td>
  <td>EC14</td>
  <td>ToggleThumb</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC15.png" width="32" height="32" alt="MiracastLogoSmall" /></td>
  <td>EC15</td>
  <td>MiracastLogoSmall</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC16.png" width="32" height="32" alt="MiracastLogoLarge" /></td>
  <td>EC16</td>
  <td>MiracastLogoLarge</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC19.png" width="32" height="32" alt="PLAP" /></td>
  <td>EC19</td>
  <td>PLAP</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC1B.png" width="32" height="32" alt="Badge" /></td>
  <td>EC1B</td>
  <td>Badge</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC1E.png" width="32" height="32" alt="SignalRoaming" /></td>
  <td>EC1E</td>
  <td>SignalRoaming</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC20.png" width="32" height="32" alt="MobileLocked" /></td>
  <td>EC20</td>
  <td>MobileLocked</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC24.png" width="32" height="32" alt="InsiderHubApp" /></td>
  <td>EC24</td>
  <td>InsiderHubApp</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC25.png" width="32" height="32" alt="PersonalFolder" /></td>
  <td>EC25</td>
  <td>PersonalFolder</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC26.png" width="32" height="32" alt="HomeGroup" /></td>
  <td>EC26</td>
  <td>HomeGroup</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC27.png" width="32" height="32" alt="MyNetwork" /></td>
  <td>EC27</td>
  <td>MyNetwork</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC31.png" width="32" height="32" alt="KeyboardFull" /></td>
  <td>EC31</td>
  <td>KeyboardFull</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC32.png" width="32" height="32" alt="Cafe" /></td>
  <td>EC32</td>
  <td>Cafe</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC37.png" width="32" height="32" alt="MobSignal1" /></td>
  <td>EC37</td>
  <td>MobSignal1</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC38.png" width="32" height="32" alt="MobSignal2" /></td>
  <td>EC38</td>
  <td>MobSignal2</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC39.png" width="32" height="32" alt="MobSignal3" /></td>
  <td>EC39</td>
  <td>MobSignal3</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC3A.png" width="32" height="32" alt="MobSignal4" /></td>
  <td>EC3A</td>
  <td>MobSignal4</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC3B.png" width="32" height="32" alt="MobSignal5" /></td>
  <td>EC3B</td>
  <td>MobSignal5</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC3C.png" width="32" height="32" alt="MobWifi1" /></td>
  <td>EC3C</td>
  <td>MobWifi1</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC3D.png" width="32" height="32" alt="MobWifi2" /></td>
  <td>EC3D</td>
  <td>MobWifi2</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC3E.png" width="32" height="32" alt="MobWifi3" /></td>
  <td>EC3E</td>
  <td>MobWifi3</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC3F.png" width="32" height="32" alt="MobWifi4" /></td>
  <td>EC3F</td>
  <td>MobWifi4</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC40.png" width="32" height="32" alt="MobAirplane" /></td>
  <td>EC40</td>
  <td>MobAirplane</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC41.png" width="32" height="32" alt="MobBluetooth" /></td>
  <td>EC41</td>
  <td>MobBluetooth</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC42.png" width="32" height="32" alt="MobActionCenter" /></td>
  <td>EC42</td>
  <td>MobActionCenter</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC43.png" width="32" height="32" alt="MobLocation" /></td>
  <td>EC43</td>
  <td>MobLocation</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC44.png" width="32" height="32" alt="MobWifiHotspot" /></td>
  <td>EC44</td>
  <td>MobWifiHotspot</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC45.png" width="32" height="32" alt="LanguageJpn" /></td>
  <td>EC45</td>
  <td>LanguageJpn</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC46.png" width="32" height="32" alt="MobQuietHours" /></td>
  <td>EC46</td>
  <td>MobQuietHours</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC47.png" width="32" height="32" alt="MobDrivingMode" /></td>
  <td>EC47</td>
  <td>MobDrivingMode</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC48.png" width="32" height="32" alt="SpeedOff" /></td>
  <td>EC48</td>
  <td>SpeedOff</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC49.png" width="32" height="32" alt="SpeedMedium" /></td>
  <td>EC49</td>
  <td>SpeedMedium</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC4A.png" width="32" height="32" alt="SpeedHigh" /></td>
  <td>EC4A</td>
  <td>SpeedHigh</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC4E.png" width="32" height="32" alt="ThisPC" /></td>
  <td>EC4E</td>
  <td>ThisPC</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC4F.png" width="32" height="32" alt="MusicNote" /></td>
  <td>EC4F</td>
  <td>MusicNote</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC50.png" width="32" height="32" alt="FileExplorer" /></td>
  <td>EC50</td>
  <td>FileExplorer</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC51.png" width="32" height="32" alt="FileExplorerApp" /></td>
  <td>EC51</td>
  <td>FileExplorerApp</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC52.png" width="32" height="32" alt="LeftArrowKeyTime0" /></td>
  <td>EC52</td>
  <td>LeftArrowKeyTime0</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC54.png" width="32" height="32" alt="MicOff" /></td>
  <td>EC54</td>
  <td>MicOff</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC55.png" width="32" height="32" alt="MicSleep" /></td>
  <td>EC55</td>
  <td>MicSleep</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC56.png" width="32" height="32" alt="MicError" /></td>
  <td>EC56</td>
  <td>MicError</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC57.png" width="32" height="32" alt="PlaybackRate1x" /></td>
  <td>EC57</td>
  <td>PlaybackRate1x</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC58.png" width="32" height="32" alt="PlaybackRateOther" /></td>
  <td>EC58</td>
  <td>PlaybackRateOther</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC59.png" width="32" height="32" alt="CashDrawer" /></td>
  <td>EC59</td>
  <td>CashDrawer</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC5A.png" width="32" height="32" alt="BarcodeScanner" /></td>
  <td>EC5A</td>
  <td>BarcodeScanner</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC5B.png" width="32" height="32" alt="ReceiptPrinter" /></td>
  <td>EC5B</td>
  <td>ReceiptPrinter</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC5C.png" width="32" height="32" alt="MagStripeReader" /></td>
  <td>EC5C</td>
  <td>MagStripeReader</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC61.png" width="32" height="32" alt="CompletedSolid" /></td>
  <td>EC61</td>
  <td>CompletedSolid</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC64.png" width="32" height="32" alt="CompanionApp" /></td>
  <td>EC64</td>
  <td>CompanionApp</td>
 </tr>
 <tr><td><img src="images/segoe-mdl/EC6C.png" width="32" height="32" alt="Favicon2" /></td>
  <td>EC6C</td>
  <td>Favicon2</td>
</tr>
<tr><td><img src="images/segoe-mdl/EC6D.png" width="32" height="32" alt="SwipeRevealArt" /></td>
  <td>EC6D</td>
  <td>SwipeRevealArt</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC71.png" width="32" height="32" alt="MicOn" /></td>
  <td>EC71</td>
  <td>MicOn</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC72.png" width="32" height="32" alt="MicClipping" /></td>
  <td>EC72</td>
  <td>MicClipping</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC74.png" width="32" height="32" alt="TabletSelected" /></td>
  <td>EC74</td>
  <td>TabletSelected</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC75.png" width="32" height="32" alt="MobileSelected" /></td>
  <td>EC75</td>
  <td>MobileSelected</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC76.png" width="32" height="32" alt="LaptopSelected" /></td>
  <td>EC76</td>
  <td>LaptopSelected</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC77.png" width="32" height="32" alt="TVMonitorSelected" /></td>
  <td>EC77</td>
  <td>TVMonitorSelected</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC7A.png" width="32" height="32" alt="DeveloperTools" /></td>
  <td>EC7A</td>
  <td>DeveloperTools</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC7E.png" width="32" height="32" alt="MobCallForwarding" /></td>
  <td>EC7E</td>
  <td>MobCallForwarding</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC7F.png" width="32" height="32" alt="MobCallForwardingMirrored" /></td>
  <td>EC7F</td>
  <td>MobCallForwardingMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC80.png" width="32" height="32" alt="BodyCam" /></td>
  <td>EC80</td>
  <td>BodyCam</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC81.png" width="32" height="32" alt="PoliceCar" /></td>
  <td>EC81</td>
  <td>PoliceCar</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC87.png" width="32" height="32" alt="Draw" /></td>
  <td>EC87</td>
  <td>Draw</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC88.png" width="32" height="32" alt="DrawSolid" /></td>
  <td>EC88</td>
  <td>DrawSolid</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC8A.png" width="32" height="32" alt="LowerBrightness" /></td>
  <td>EC8A</td>
  <td>LowerBrightness</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC8F.png" width="32" height="32" alt="ScrollUpDown" /></td>
  <td>EC8F</td>
  <td>ScrollUpDown</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EC92.png" width="32" height="32" alt="DateTime" /></td>
  <td>EC92</td>
  <td>DateTime</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ECA5.png" width="32" height="32" alt="Tiles" /></td>
  <td>ECA5</td>
  <td>Tiles</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ECA7.png" width="32" height="32" alt="PartyLeader" /></td>
  <td>ECA7</td>
  <td>PartyLeader</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ECAA.png" width="32" height="32" alt="AppIconDefault" /></td>
  <td>ECAA</td>
  <td>AppIconDefault</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ECAD.png" width="32" height="32" alt="Calories" /></td>
  <td>ECAD</td>
  <td>Calories</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ECB9.png" width="32" height="32" alt="BandBattery0" /></td>
  <td>ECB9</td>
  <td>BandBattery0</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ECBA.png" width="32" height="32" alt="BandBattery1" /></td>
  <td>ECBA</td>
  <td>BandBattery1</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ECBB.png" width="32" height="32" alt="BandBattery2" /></td>
  <td>ECBB</td>
  <td>BandBattery2</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ECBC.png" width="32" height="32" alt="BandBattery3" /></td>
  <td>ECBC</td>
  <td>BandBattery3</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ECBD.png" width="32" height="32" alt="BandBattery4" /></td>
  <td>ECBD</td>
  <td>BandBattery4</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ECBE.png" width="32" height="32" alt="BandBattery5" /></td>
  <td>ECBE</td>
  <td>BandBattery5</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ECBF.png" width="32" height="32" alt="BandBattery6" /></td>
  <td>ECBF</td>
  <td>BandBattery6</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ECC4.png" width="32" height="32" alt="AddSurfaceHub" /></td>
  <td>ECC4</td>
  <td>AddSurfaceHub</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ECC5.png" width="32" height="32" alt="DevUpdate" /></td>
  <td>ECC5</td>
  <td>DevUpdate</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ECC6.png" width="32" height="32" alt="Unit" /></td>
  <td>ECC6</td>
  <td>Unit</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ECC8.png" width="32" height="32" alt="AddTo" /></td>
  <td>ECC8</td>
  <td>AddTo</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ECC9.png" width="32" height="32" alt="RemoveFrom" /></td>
  <td>ECC9</td>
  <td>RemoveFrom</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ECCA.png" width="32" height="32" alt="RadioBtnOff" /></td>
  <td>ECCA</td>
  <td>RadioBtnOff</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ECCB.png" width="32" height="32" alt="RadioBtnOn" /></td>
  <td>ECCB</td>
  <td>RadioBtnOn</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ECCC.png" width="32" height="32" alt="RadioBullet2" /></td>
  <td>ECCC</td>
  <td>RadioBullet2</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ECCD.png" width="32" height="32" alt="ExploreContent" /></td>
  <td>ECCD</td>
  <td>ExploreContent</td>
 </tr>
 <tr><td><img src="images/segoe-mdl/ECE4.png" width="32" height="32" alt="Blocked2" /></td>
  <td>ECE4</td>
  <td>Blocked2</td>
</tr>
<tr><td><img src="images/segoe-mdl/ECE7.png" width="32" height="32" alt="ScrollMode" /></td>
  <td>ECE7</td>
  <td>ScrollMode</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ECE8.png" width="32" height="32" alt="ZoomMode" /></td>
  <td>ECE8</td>
  <td>ZoomMode</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ECE9.png" width="32" height="32" alt="PanMode" /></td>
  <td>ECE9</td>
  <td>PanMode</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ECF0.png" width="32" height="32" alt="WiredUSB  " /></td>
  <td>ECF0</td>
  <td>WiredUSB  </td>
 </tr>
<tr><td><img src="images/segoe-mdl/ECF1.png" width="32" height="32" alt="WirelessUSB" /></td>
  <td>ECF1</td>
  <td>WirelessUSB</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ECF3.png" width="32" height="32" alt="USBSafeConnect" /></td>
  <td>ECF3</td>
  <td>USBSafeConnect</td>
 </tr>
</table>

 ### PUA ED00-EF00

The following table of glyphs displays unicode points prefixed from ED-  to EF-.

[Back to top](#icon-list)

</br>
<table>
 <tr>
  <td>Glyph</td>
  <td>Unicode point</td>
  <td>Description</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ED0C.png" width="32" height="32" alt="ActionCenterNotificationMirrored" /></td>
  <td>ED0C</td>
  <td>ActionCenterNotificationMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ED0D.png" width="32" height="32" alt="ActionCenterMirrored" /></td>
  <td>ED0D</td>
  <td>ActionCenterMirrored</td>
 </tr>
 <tr><td><img src="images/segoe-mdl/ED0E.png" width="32" height="32" alt="SubscriptionAdd" /></td>
  <td>ED0E</td>
  <td>SubscriptionAdd</td>
</tr>
<tr><td><img src="images/segoe-mdl/ED10.png" width="32" height="32" alt="ResetDevice" /></td>
  <td>ED10</td>
  <td>ResetDevice</td>
 </tr>
 <tr><td><img src="images/segoe-mdl/ED11.png" width="32" height="32" alt="SubscriptionAddMirrored" /></td>
  <td>ED11</td>
  <td>SubscriptionAddMirrored</td>
</tr>
<tr><td><img src="images/segoe-mdl/ED14.png" width="32" height="32" alt="QRCode" /></td>
  <td>ED14</td>
  <td>QRCode</td>
</tr>
<tr><td><img src="images/segoe-mdl/ED15.png" width="32" height="32" alt="Feedback" /></td>
  <td>ED15</td>
  <td>Feedback</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ED1E.png" width="32" height="32" alt="Subtitles" /></td>
  <td>ED1E</td>
  <td>Subtitles</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ED1F.png" width="32" height="32" alt="SubtitlesAudio" /></td>
  <td>ED1F</td>
  <td>SubtitlesAudio</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ED25.png" width="32" height="32" alt="OpenFolderHorizontal" /></td>
  <td>ED25</td>
  <td>OpenFolderHorizontal</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ED28.png" width="32" height="32" alt="CalendarMirrored" /></td>
  <td>ED28</td>
  <td>CalendarMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ED2A.png" width="32" height="32" alt="MobeSIM" /></td>
  <td>ED2A</td>
  <td>MobeSIM</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ED2B.png" width="32" height="32" alt="MobeSIMNoProfile" /></td>
  <td>ED2B</td>
  <td>MobeSIMNoProfile</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ED2C.png" width="32" height="32" alt="MobeSIMLocked" /></td>
  <td>ED2C</td>
  <td>MobeSIMLocked</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ED2D.png" width="32" height="32" alt="MobeSIMBusy" /></td>
  <td>ED2D</td>
  <td>MobeSIMBusy</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ED2E.png" width="32" height="32" alt="SignalError" /></td>
  <td>ED2E</td>
  <td>SignalError</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ED2F.png" width="32" height="32" alt="StreamingEnterprise" /></td>
  <td>ED2F</td>
  <td>StreamingEnterprise</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ED30.png" width="32" height="32" alt="Headphone0" /></td>
  <td>ED30</td>
  <td>Headphone0</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ED31.png" width="32" height="32" alt="Headphone1" /></td>
  <td>ED31</td>
  <td>Headphone1</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ED32.png" width="32" height="32" alt="Headphone2" /></td>
  <td>ED32</td>
  <td>Headphone2</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ED33.png" width="32" height="32" alt="Headphone3" /></td>
  <td>ED33</td>
  <td>Headphone3</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ED35.png" width="32" height="32" alt="Apps" /></td>
  <td>ED35</td>
  <td>Apps</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ED39.png" width="32" height="32" alt="KeyboardBrightness" /></td>
  <td>ED39</td>
  <td>KeyboardBrightness</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ED3A.png" width="32" height="32" alt="KeyboardLowerBrightness" /></td>
  <td>ED3A</td>
  <td>KeyboardLowerBrightness</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ED3C.png" width="32" height="32" alt="SkipBack10" /></td>
  <td>ED3C</td>
  <td>SkipBack10</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ED3D.png" width="32" height="32" alt="SkipForward30 " /></td>
  <td>ED3D</td>
  <td>SkipForward30 </td>
 </tr>
<tr><td><img src="images/segoe-mdl/ED41.png" width="32" height="32" alt="TreeFolderFolder" /></td>
  <td>ED41</td>
  <td>TreeFolderFolder</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ED42.png" width="32" height="32" alt="TreeFolderFolderFill" /></td>
  <td>ED42</td>
  <td>TreeFolderFolderFill</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ED43.png" width="32" height="32" alt="TreeFolderFolderOpen" /></td>
  <td>ED43</td>
  <td>TreeFolderFolderOpen</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ED44.png" width="32" height="32" alt="TreeFolderFolderOpenFill" /></td>
  <td>ED44</td>
  <td>TreeFolderFolderOpenFill</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ED47.png" width="32" height="32" alt="MultimediaDMP" /></td>
  <td>ED47</td>
  <td>MultimediaDMP</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ED4C.png" width="32" height="32" alt="KeyboardOneHanded" /></td>
  <td>ED4C</td>
  <td>KeyboardOneHanded</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ED4D.png" width="32" height="32" alt="Narrator" /></td>
  <td>ED4D</td>
  <td>Narrator</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ED53.png" width="32" height="32" alt="EmojiTabPeople" /></td>
  <td>ED53</td>
  <td>EmojiTabPeople</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ED54.png" width="32" height="32" alt="EmojiTabSmilesAnimals" /></td>
  <td>ED54</td>
  <td>EmojiTabSmilesAnimals</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ED55.png" width="32" height="32" alt="EmojiTabCelebrationObjects" /></td>
  <td>ED55</td>
  <td>EmojiTabCelebrationObjects</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ED56.png" width="32" height="32" alt="EmojiTabFoodPlants" /></td>
  <td>ED56</td>
  <td>EmojiTabFoodPlants</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ED57.png" width="32" height="32" alt="EmojiTabTransitPlaces" /></td>
  <td>ED57</td>
  <td>EmojiTabTransitPlaces</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ED58.png" width="32" height="32" alt="EmojiTabSymbols" /></td>
  <td>ED58</td>
  <td>EmojiTabSymbols</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ED59.png" width="32" height="32" alt="EmojiTabTextSmiles" /></td>
  <td>ED59</td>
  <td>EmojiTabTextSmiles</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ED5A.png" width="32" height="32" alt="EmojiTabFavorites" /></td>
  <td>ED5A</td>
  <td>EmojiTabFavorites</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ED5B.png" width="32" height="32" alt="EmojiSwatch" /></td>
  <td>ED5B</td>
  <td>EmojiSwatch</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ED5C.png" width="32" height="32" alt="ConnectApp" /></td>
  <td>ED5C</td>
  <td>ConnectApp</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ED5D.png" width="32" height="32" alt="CompanionDeviceFramework" /></td>
  <td>ED5D</td>
  <td>CompanionDeviceFramework</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ED5E.png" width="32" height="32" alt="Ruler" /></td>
  <td>ED5E</td>
  <td>Ruler</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ED5F.png" width="32" height="32" alt="FingerInking" /></td>
  <td>ED5F</td>
  <td>FingerInking</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ED60.png" width="32" height="32" alt="StrokeErase" /></td>
  <td>ED60</td>
  <td>StrokeErase</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ED61.png" width="32" height="32" alt="PointErase" /></td>
  <td>ED61</td>
  <td>PointErase</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ED62.png" width="32" height="32" alt="ClearAllInk" /></td>
  <td>ED62</td>
  <td>ClearAllInk</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ED63.png" width="32" height="32" alt="Pencil" /></td>
  <td>ED63</td>
  <td>Pencil</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ED64.png" width="32" height="32" alt="Marker" /></td>
  <td>ED64</td>
  <td>Marker</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ED65.png" width="32" height="32" alt="InkingCaret" /></td>
  <td>ED65</td>
  <td>InkingCaret</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ED66.png" width="32" height="32" alt="InkingColorOutline" /></td>
  <td>ED66</td>
  <td>InkingColorOutline</td>
 </tr>
<tr><td><img src="images/segoe-mdl/ED67.png" width="32" height="32" alt="InkingColorFill" /></td>
  <td>ED67</td>
  <td>InkingColorFill</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EDA2.png" width="32" height="32" alt="HardDrive" /></td>
  <td>EDA2</td>
  <td>HardDrive</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EDA3.png" width="32" height="32" alt="NetworkAdapter" /></td>
  <td>EDA3</td>
  <td>NetworkAdapter</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EDA4.png" width="32" height="32" alt="Touchscreen" /></td>
  <td>EDA4</td>
  <td>Touchscreen</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EDA5.png" width="32" height="32" alt="NetworkPrinter" /></td>
  <td>EDA5</td>
  <td>NetworkPrinter</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EDA6.png" width="32" height="32" alt="CloudPrinter" /></td>
  <td>EDA6</td>
  <td>CloudPrinter</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EDA7.png" width="32" height="32" alt="KeyboardShortcut" /></td>
  <td>EDA7</td>
  <td>KeyboardShortcut</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EDA8.png" width="32" height="32" alt="BrushSize" /></td>
  <td>EDA8</td>
  <td>BrushSize</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EDA9.png" width="32" height="32" alt="NarratorForward" /></td>
  <td>EDA9</td>
  <td>NarratorForward</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EDAA.png" width="32" height="32" alt="NarratorForwardMirrored" /></td>
  <td>EDAA</td>
  <td>NarratorForwardMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EDAB.png" width="32" height="32" alt="SyncBadge12" /></td>
  <td>EDAB</td>
  <td>SyncBadge12</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EDAC.png" width="32" height="32" alt="RingerBadge12" /></td>
  <td>EDAC</td>
  <td>RingerBadge12</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EDAD.png" width="32" height="32" alt="AsteriskBadge12" /></td>
  <td>EDAD</td>
  <td>AsteriskBadge12</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EDAE.png" width="32" height="32" alt="ErrorBadge12" /></td>
  <td>EDAE</td>
  <td>ErrorBadge12</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EDAF.png" width="32" height="32" alt="CircleRingBadge12" /></td>
  <td>EDAF</td>
  <td>CircleRingBadge12</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EDB0.png" width="32" height="32" alt="CircleFillBadge12" /></td>
  <td>EDB0</td>
  <td>CircleFillBadge12</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EDB1.png" width="32" height="32" alt="ImportantBadge12" /></td>
  <td>EDB1</td>
  <td>ImportantBadge12</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EDB3.png" width="32" height="32" alt="MailBadge12" /></td>
  <td>EDB3</td>
  <td>MailBadge12</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EDB4.png" width="32" height="32" alt="PauseBadge12" /></td>
  <td>EDB4</td>
  <td>PauseBadge12</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EDB5.png" width="32" height="32" alt="PlayBadge12" /></td>
  <td>EDB5</td>
  <td>PlayBadge12</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EDC6.png" width="32" height="32" alt="PenWorkspace" /></td>
  <td>EDC6</td>
  <td>PenWorkspace</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EDD6.png" width="32" height="32" alt="CaretRight8" /></td>
  <td>EDD6</td>
  <td>CaretRight8</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EDD9.png" width="32" height="32" alt="CaretLeftSolid8" /></td>
  <td>EDD9</td>
  <td>CaretLeftSolid8</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EDDA.png" width="32" height="32" alt="CaretRightSolid8" /></td>
  <td>EDDA</td>
  <td>CaretRightSolid8</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EDDB.png" width="32" height="32" alt="CaretUpSolid8" /></td>
  <td>EDDB</td>
  <td>CaretUpSolid8</td>
 </tr>
 <tr><td><img src="images/segoe-mdl/EDDC.png" width="32" height="32" alt="CaretDownSolid8" /></td>
  <td>EDDC</td>
  <td>CaretDownSolid8</td>
 </tr>
 <tr><td><img src="images/segoe-mdl/EDE0.png" width="32" height="32" alt="Strikethrough" /></td>
  <td>EDE0</td>
  <td>Strikethrough</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EDE1.png" width="32" height="32" alt="Export" /></td>
  <td>EDE1</td>
  <td>Export</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EDE2.png" width="32" height="32" alt="ExportMirrored" /></td>
  <td>EDE2</td>
  <td>ExportMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EDE3.png" width="32" height="32" alt="ButtonMenu" /></td>
  <td>EDE3</td>
  <td>ButtonMenu</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EDE4.png" width="32" height="32" alt="CloudSearch" /></td>
  <td>EDE4</td>
  <td>CloudSearch</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EDE5.png" width="32" height="32" alt="PinyinIMELogo" /></td>
  <td>EDE5</td>
  <td>PinyinIMELogo</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EDFB.png" width="32" height="32" alt="CalligraphyPen" /></td>
  <td>EDFB</td>
  <td>CalligraphyPen</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EE35.png" width="32" height="32" alt="ReplyMirrored" /></td>
  <td>EE35</td>
  <td>ReplyMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EE3F.png" width="32" height="32" alt="LockscreenDesktop" /></td>
  <td>EE3F</td>
  <td>LockscreenDesktop</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EE40.png" width="32" height="32" alt="TaskViewSettings" /></td>
  <td>EE40</td>
  <td>TaskViewSettings</td>
 </tr>
 <tr><td><img src="images/segoe-mdl/EE47.png" width="32" height="32" alt="MiniExpand2Mirrored" /></td>
  <td>EE47</td>
  <td>MiniExpand2Mirrored</td>
 </tr>
 <tr><td><img src="images/segoe-mdl/EE49.png" width="32" height="32" alt="MiniContract2Mirrored" /></td>
  <td>EE49</td>
  <td>MiniContract2Mirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EE4A.png" width="32" height="32" alt="Play36" /></td>
  <td>EE4A</td>
  <td>Play36</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EE56.png" width="32" height="32" alt="PenPalette" /></td>
  <td>EE56</td>
  <td>PenPalette</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EE57.png" width="32" height="32" alt="GuestUser" /></td>
  <td>EE57</td>
  <td>GuestUser</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EE63.png" width="32" height="32" alt="SettingsBattery" /></td>
  <td>EE63</td>
  <td>SettingsBattery</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EE64.png" width="32" height="32" alt="TaskbarPhone" /></td>
  <td>EE64</td>
  <td>TaskbarPhone</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EE65.png" width="32" height="32" alt="LockScreenGlance" /></td>
  <td>EE65</td>
  <td>LockScreenGlance</td>
 </tr>
 <tr><td><img src="images/segoe-mdl/EE6F.png" width="32" height="32" alt="GenericScan" /></td>
  <td>EE6F</td>
  <td>GenericScan</td>
</tr>
<tr><td><img src="images/segoe-mdl/EE71.png" width="32" height="32" alt="ImageExport " /></td>
  <td>EE71</td>
  <td>ImageExport </td>
 </tr>
<tr><td><img src="images/segoe-mdl/EE77.png" width="32" height="32" alt="WifiEthernet" /></td>
  <td>EE77</td>
  <td>WifiEthernet</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EE79.png" width="32" height="32" alt="ActionCenterQuiet" /></td>
  <td>EE79</td>
  <td>ActionCenterQuiet</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EE7A.png" width="32" height="32" alt="ActionCenterQuietNotification" /></td>
  <td>EE7A</td>
  <td>ActionCenterQuietNotification</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EE92.png" width="32" height="32" alt="TrackersMirrored" /></td>
  <td>EE92</td>
  <td>TrackersMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EE93.png" width="32" height="32" alt="DateTimeMirrored" /></td>
  <td>EE93</td>
  <td>DateTimeMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EE94.png" width="32" height="32" alt="Wheel" /></td>
  <td>EE94</td>
  <td>Wheel</td>
 </tr>
 <tr><td><img src="images/segoe-mdl/EEA3.png" width="32" height="32" alt="VirtualMachineGroup" /></td>
  <td>EEA3</td>
  <td>VirtualMachineGroup</td>
</tr>
<tr><td><img src="images/segoe-mdl/EECA.png" width="32" height="32" alt="ButtonView2" /></td>
  <td>EECA</td>
  <td>ButtonView2</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EF15.png" width="32" height="32" alt="PenWorkspaceMirrored" /></td>
  <td>EF15</td>
  <td>PenWorkspaceMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EF16.png" width="32" height="32" alt="PenPaletteMirrored" /></td>
  <td>EF16</td>
  <td>PenPaletteMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EF17.png" width="32" height="32" alt="StrokeEraseMirrored" /></td>
  <td>EF17</td>
  <td>StrokeEraseMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EF18.png" width="32" height="32" alt="PointEraseMirrored" /></td>
  <td>EF18</td>
  <td>PointEraseMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EF19.png" width="32" height="32" alt="ClearAllInkMirrored" /></td>
  <td>EF19</td>
  <td>ClearAllInkMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EF1F.png" width="32" height="32" alt="BackgroundToggle" /></td>
  <td>EF1F</td>
  <td>BackgroundToggle</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EF20.png" width="32" height="32" alt="Marquee" /></td>
  <td>EF20</td>
  <td>Marquee</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EF2C.png" width="32" height="32" alt="ChromeCloseContrast" /></td>
  <td>EF2C</td>
  <td>ChromeCloseContrast</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EF2D.png" width="32" height="32" alt="ChromeMinimizeContrast" /></td>
  <td>EF2D</td>
  <td>ChromeMinimizeContrast</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EF2E.png" width="32" height="32" alt="ChromeMaximizeContrast" /></td>
  <td>EF2E</td>
  <td>ChromeMaximizeContrast</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EF2F.png" width="32" height="32" alt="ChromeRestoreContrast" /></td>
  <td>EF2F</td>
  <td>ChromeRestoreContrast</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EF31.png" width="32" height="32" alt="TrafficLight" /></td>
  <td>EF31</td>
  <td>TrafficLight</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EF3B.png" width="32" height="32" alt="Replay" /></td>
  <td>EF3B</td>
  <td>Replay</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EF3C.png" width="32" height="32" alt="Eyedropper" /></td>
  <td>EF3C</td>
  <td>Eyedropper</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EF3D.png" width="32" height="32" alt="LineDisplay" /></td>
  <td>EF3D</td>
  <td>LineDisplay</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EF3E.png" width="32" height="32" alt="PINPad" /></td>
  <td>EF3E</td>
  <td>PINPad</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EF3F.png" width="32" height="32" alt="SignatureCapture" /></td>
  <td>EF3F</td>
  <td>SignatureCapture</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EF40.png" width="32" height="32" alt="ChipCardCreditCardReader" /></td>
  <td>EF40</td>
  <td>ChipCardCreditCardReader</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EF58.png" width="32" height="32" alt="PlayerSettings" /></td>
  <td>EF58</td>
  <td>PlayerSettings</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EF6B.png" width="32" height="32" alt="LandscapeOrientation" /></td>
  <td>EF6B</td>
  <td>LandscapeOrientation</td>
 </tr>
 <tr><td><img src="images/segoe-mdl/EF90.png" width="32" height="32" alt="Flow" /></td>
  <td>EF90</td>
  <td>Flow</td>
</tr>
<tr><td><img src="images/segoe-mdl/EFA5.png" width="32" height="32" alt="Touchpad" /></td>
  <td>EFA5</td>
  <td>Touchpad</td>
 </tr>
<tr><td><img src="images/segoe-mdl/EFA9.png" width="32" height="32" alt="Speech" /></td>
  <td>EFA9</td>
  <td>Speech</td>
 </tr>
</table>

 ### PUA F000-F200

The following table of glyphs displays unicode points prefixed from F0-  to F2-.

[Back to top](#icon-list)

</br>
<table>
 <tr>
  <td>Glyph</td>
  <td>Unicode point</td>
  <td>Description</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F000.png" width="32" height="32" alt="KnowledgeArticle" /></td>
  <td>F000</td>
  <td>KnowledgeArticle</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F003.png" width="32" height="32" alt="Relationship" /></td>
  <td>F003</td>
  <td>Relationship</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F080.png" width="32" height="32" alt="DefaultAPN" /></td>
  <td>F080</td>
  <td>DefaultAPN</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F081.png" width="32" height="32" alt="UserAPN " /></td>
  <td>F081</td>
  <td>UserAPN </td>
 </tr>
<tr><td><img src="images/segoe-mdl/F085.png" width="32" height="32" alt="DoublePinyin" /></td>
  <td>F085</td>
  <td>DoublePinyin</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F08C.png" width="32" height="32" alt="BlueLight" /></td>
  <td>F08C</td>
  <td>BlueLight</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F093.png" width="32" height="32" alt="ButtonA" /></td>
  <td>F093</td>
  <td>ButtonA</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F094.png" width="32" height="32" alt="ButtonB" /></td>
  <td>F094</td>
  <td>ButtonB</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F095.png" width="32" height="32" alt="ButtonY" /></td>
  <td>F095</td>
  <td>ButtonY</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F096.png" width="32" height="32" alt="ButtonX" /></td>
  <td>F096</td>
  <td>ButtonX</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F0AD.png" width="32" height="32" alt="ArrowUp8" /></td>
  <td>F0AD</td>
  <td>ArrowUp8</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F0AE.png" width="32" height="32" alt="ArrowDown8" /></td>
  <td>F0AE</td>
  <td>ArrowDown8</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F0AF.png" width="32" height="32" alt="ArrowRight8" /></td>
  <td>F0AF</td>
  <td>ArrowRight8</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F0B0.png" width="32" height="32" alt="ArrowLeft8" /></td>
  <td>F0B0</td>
  <td>ArrowLeft8</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F0B2.png" width="32" height="32" alt="QuarentinedItems" /></td>
  <td>F0B2</td>
  <td>QuarentinedItems</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F0B3.png" width="32" height="32" alt="QuarentinedItemsMirrored" /></td>
  <td>F0B3</td>
  <td>QuarentinedItemsMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F0B4.png" width="32" height="32" alt="Protractor" /></td>
  <td>F0B4</td>
  <td>Protractor</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F0B5.png" width="32" height="32" alt="ChecklistMirrored" /></td>
  <td>F0B5</td>
  <td>ChecklistMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F0B6.png" width="32" height="32" alt="StatusCircle7" /></td>
  <td>F0B6</td>
  <td>StatusCircle7</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F0B7.png" width="32" height="32" alt="StatusCheckmark7" /></td>
  <td>F0B7</td>
  <td>StatusCheckmark7</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F0B8.png" width="32" height="32" alt="StatusErrorCircle7" /></td>
  <td>F0B8</td>
  <td>StatusErrorCircle7</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F0B9.png" width="32" height="32" alt="Connected" /></td>
  <td>F0B9</td>
  <td>Connected</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F0C6.png" width="32" height="32" alt="PencilFill" /></td>
  <td>F0C6</td>
  <td>PencilFill</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F0C7.png" width="32" height="32" alt="CalligraphyFill" /></td>
  <td>F0C7</td>
  <td>CalligraphyFill</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F0CA.png" width="32" height="32" alt="QuarterStarLeft" /></td>
  <td>F0CA</td>
  <td>QuarterStarLeft</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F0CB.png" width="32" height="32" alt="QuarterStarRight" /></td>
  <td>F0CB</td>
  <td>QuarterStarRight</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F0CC.png" width="32" height="32" alt="ThreeQuarterStarLeft" /></td>
  <td>F0CC</td>
  <td>ThreeQuarterStarLeft</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F0CD.png" width="32" height="32" alt="ThreeQuarterStarRight" /></td>
  <td>F0CD</td>
  <td>ThreeQuarterStarRight</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F0CE.png" width="32" height="32" alt="QuietHoursBadge12" /></td>
  <td>F0CE</td>
  <td>QuietHoursBadge12</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F0D2.png" width="32" height="32" alt="BackMirrored" /></td>
  <td>F0D2</td>
  <td>BackMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F0D3.png" width="32" height="32" alt="ForwardMirrored" /></td>
  <td>F0D3</td>
  <td>ForwardMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F0D5.png" width="32" height="32" alt="ChromeBackContrast" /></td>
  <td>F0D5</td>
  <td>ChromeBackContrast</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F0D6.png" width="32" height="32" alt="ChromeBackContrastMirrored" /></td>
  <td>F0D6</td>
  <td>ChromeBackContrastMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F0D7.png" width="32" height="32" alt="ChromeBackToWindowContrast" /></td>
  <td>F0D7</td>
  <td>ChromeBackToWindowContrast</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F0D8.png" width="32" height="32" alt="ChromeFullScreenContrast" /></td>
  <td>F0D8</td>
  <td>ChromeFullScreenContrast</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F0E2.png" width="32" height="32" alt="GridView" /></td>
  <td>F0E2</td>
  <td>GridView</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F0E3.png" width="32" height="32" alt="ClipboardList" /></td>
  <td>F0E3</td>
  <td>ClipboardList</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F0E4.png" width="32" height="32" alt="ClipboardListMirrored" /></td>
  <td>F0E4</td>
  <td>ClipboardListMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F0E5.png" width="32" height="32" alt="OutlineQuarterStarLeft" /></td>
  <td>F0E5</td>
  <td>OutlineQuarterStarLeft</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F0E6.png" width="32" height="32" alt="OutlineQuarterStarRight" /></td>
  <td>F0E6</td>
  <td>OutlineQuarterStarRight</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F0E7.png" width="32" height="32" alt="OutlineHalfStarLeft" /></td>
  <td>F0E7</td>
  <td>OutlineHalfStarLeft</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F0E8.png" width="32" height="32" alt="OutlineHalfStarRight" /></td>
  <td>F0E8</td>
  <td>OutlineHalfStarRight</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F0E9.png" width="32" height="32" alt="OutlineThreeQuarterStarLeft" /></td>
  <td>F0E9</td>
  <td>OutlineThreeQuarterStarLeft</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F0EA.png" width="32" height="32" alt="OutlineThreeQuarterStarRight" /></td>
  <td>F0EA</td>
  <td>OutlineThreeQuarterStarRight</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F0EB.png" width="32" height="32" alt="SpatialVolume0" /></td>
  <td>F0EB</td>
  <td>SpatialVolume0</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F0EC.png" width="32" height="32" alt="SpatialVolume1" /></td>
  <td>F0EC</td>
  <td>SpatialVolume1</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F0ED.png" width="32" height="32" alt="SpatialVolume2" /></td>
  <td>F0ED</td>
  <td>SpatialVolume2</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F0EE.png" width="32" height="32" alt="SpatialVolume3" /></td>
  <td>F0EE</td>
  <td>SpatialVolume3</td>
 </tr>
 <tr><td><img src="images/segoe-mdl/F0EF.png" width="32" height="32" alt="ApplicationGuard" /></td>
  <td>F0EF</td>
  <td>ApplicationGuard</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F0F7.png" width="32" height="32" alt="OutlineStarLeftHalf" /></td>
  <td>F0F7</td>
  <td>OutlineStarLeftHalf</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F0F8.png" width="32" height="32" alt="OutlineStarRightHalf" /></td>
  <td>F0F8</td>
  <td>OutlineStarRightHalf</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F0F9.png" width="32" height="32" alt="ChromeAnnotateContrast" /></td>
  <td>F0F9</td>
  <td>ChromeAnnotateContrast</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F0FB.png" width="32" height="32" alt="DefenderBadge12" /></td>
  <td>F0FB</td>
  <td>DefenderBadge12</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F103.png" width="32" height="32" alt="DetachablePC" /></td>
  <td>F103</td>
  <td>DetachablePC</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F108.png" width="32" height="32" alt="LeftStick" /></td>
  <td>F108</td>
  <td>LeftStick</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F109.png" width="32" height="32" alt="RightStick" /></td>
  <td>F109</td>
  <td>RightStick</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F10A.png" width="32" height="32" alt="TriggerLeft" /></td>
  <td>F10A</td>
  <td>TriggerLeft</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F10B.png" width="32" height="32" alt="TriggerRight" /></td>
  <td>F10B</td>
  <td>TriggerRight</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F10C.png" width="32" height="32" alt="BumperLeft" /></td>
  <td>F10C</td>
  <td>BumperLeft</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F10D.png" width="32" height="32" alt="BumperRight" /></td>
  <td>F10D</td>
  <td>BumperRight</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F10E.png" width="32" height="32" alt="Dpad" /></td>
  <td>F10E</td>
  <td>Dpad</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F110.png" width="32" height="32" alt="EnglishPunctuation" /></td>
  <td>F110</td>
  <td>EnglishPunctuation</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F111.png" width="32" height="32" alt="ChinesePunctuation" /></td>
  <td>F111</td>
  <td>ChinesePunctuation</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F119.png" width="32" height="32" alt="HMD" /></td>
  <td>F119</td>
  <td>HMD</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F11B.png" width="32" height="32" alt="CtrlSpatialRight" /></td>
  <td>F11B</td>
  <td>CtrlSpatialRight</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F126.png" width="32" height="32" alt="PaginationDotOutline10" /></td>
  <td>F126</td>
  <td>PaginationDotOutline10</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F127.png" width="32" height="32" alt="PaginationDotSolid10" /></td>
  <td>F127</td>
  <td>PaginationDotSolid10</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F128.png" width="32" height="32" alt="StrokeErase2" /></td>
  <td>F128</td>
  <td>StrokeErase2</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F129.png" width="32" height="32" alt="SmallErase" /></td>
  <td>F129</td>
  <td>SmallErase</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F12A.png" width="32" height="32" alt="LargeErase" /></td>
  <td>F12A</td>
  <td>LargeErase</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F12B.png" width="32" height="32" alt="FolderHorizontal" /></td>
  <td>F12B</td>
  <td>FolderHorizontal</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F12E.png" width="32" height="32" alt="MicrophoneListening" /></td>
  <td>F12E</td>
  <td>MicrophoneListening</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F12F.png" width="32" height="32" alt="StatusExclamationCircle7 " /></td>
  <td>F12F</td>
  <td>StatusExclamationCircle7 </td>
 </tr>
<tr><td><img src="images/segoe-mdl/F131.png" width="32" height="32" alt="Video360" /></td>
  <td>F131</td>
  <td>Video360</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F133.png" width="32" height="32" alt="GiftboxOpen" /></td>
  <td>F133</td>
  <td>GiftboxOpen</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F136.png" width="32" height="32" alt="StatusCircleOuter" /></td>
  <td>F136</td>
  <td>StatusCircleOuter</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F137.png" width="32" height="32" alt="StatusCircleInner" /></td>
  <td>F137</td>
  <td>StatusCircleInner</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F138.png" width="32" height="32" alt="StatusCircleRing" /></td>
  <td>F138</td>
  <td>StatusCircleRing</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F139.png" width="32" height="32" alt="StatusTriangleOuter" /></td>
  <td>F139</td>
  <td>StatusTriangleOuter</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F13A.png" width="32" height="32" alt="StatusTriangleInner" /></td>
  <td>F13A</td>
  <td>StatusTriangleInner</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F13B.png" width="32" height="32" alt="StatusTriangleExclamation" /></td>
  <td>F13B</td>
  <td>StatusTriangleExclamation</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F13C.png" width="32" height="32" alt="StatusCircleExclamation" /></td>
  <td>F13C</td>
  <td>StatusCircleExclamation</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F13D.png" width="32" height="32" alt="StatusCircleErrorX" /></td>
  <td>F13D</td>
  <td>StatusCircleErrorX</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F13E.png" width="32" height="32" alt="StatusCircleCheckmark" /></td>
  <td>F13E</td>
  <td>StatusCircleCheckmark</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F13F.png" width="32" height="32" alt="StatusCircleInfo" /></td>
  <td>F13F</td>
  <td>StatusCircleInfo</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F140.png" width="32" height="32" alt="StatusCircleBlock" /></td>
  <td>F140</td>
  <td>StatusCircleBlock</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F141.png" width="32" height="32" alt="StatusCircleBlock2" /></td>
  <td>F141</td>
  <td>StatusCircleBlock2</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F142.png" width="32" height="32" alt="StatusCircleQuestionMark" /></td>
  <td>F142</td>
  <td>StatusCircleQuestionMark</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F143.png" width="32" height="32" alt="StatusCircleSync" /></td>
  <td>F143</td>
  <td>StatusCircleSync</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F146.png" width="32" height="32" alt="Dial1" /></td>
  <td>F146</td>
  <td>Dial1</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F147.png" width="32" height="32" alt="Dial2" /></td>
  <td>F147</td>
  <td>Dial2</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F148.png" width="32" height="32" alt="Dial3" /></td>
  <td>F148</td>
  <td>Dial3</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F149.png" width="32" height="32" alt="Dial4" /></td>
  <td>F149</td>
  <td>Dial4</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F14A.png" width="32" height="32" alt="Dial5" /></td>
  <td>F14A</td>
  <td>Dial5</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F14B.png" width="32" height="32" alt="Dial6" /></td>
  <td>F14B</td>
  <td>Dial6</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F14C.png" width="32" height="32" alt="Dial7" /></td>
  <td>F14C</td>
  <td>Dial7</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F14D.png" width="32" height="32" alt="Dial8" /></td>
  <td>F14D</td>
  <td>Dial8</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F14E.png" width="32" height="32" alt="Dial9" /></td>
  <td>F14E</td>
  <td>Dial9</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F14F.png" width="32" height="32" alt="Dial10" /></td>
  <td>F14F</td>
  <td>Dial10</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F150.png" width="32" height="32" alt="Dial11" /></td>
  <td>F150</td>
  <td>Dial11</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F151.png" width="32" height="32" alt="Dial12" /></td>
  <td>F151</td>
  <td>Dial12</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F152.png" width="32" height="32" alt="Dial13" /></td>
  <td>F152</td>
  <td>Dial13</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F153.png" width="32" height="32" alt="Dial14" /></td>
  <td>F153</td>
  <td>Dial14</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F154.png" width="32" height="32" alt="Dial15" /></td>
  <td>F154</td>
  <td>Dial15</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F155.png" width="32" height="32" alt="Dial16" /></td>
  <td>F155</td>
  <td>Dial16</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F156.png" width="32" height="32" alt="DialShape1" /></td>
  <td>F156</td>
  <td>DialShape1</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F157.png" width="32" height="32" alt="DialShape2" /></td>
  <td>F157</td>
  <td>DialShape2</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F158.png" width="32" height="32" alt="DialShape3" /></td>
  <td>F158</td>
  <td>DialShape3</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F159.png" width="32" height="32" alt="DialShape4" /></td>
  <td>F159</td>
  <td>DialShape4</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F161.png" width="32" height="32" alt="TollSolid" /></td>
  <td>F161</td>
  <td>TollSolid</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F163.png" width="32" height="32" alt="TrafficCongestionSolid" /></td>
  <td>F163</td>
  <td>TrafficCongestionSolid</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F164.png" width="32" height="32" alt="ExploreContentSingle" /></td>
  <td>F164</td>
  <td>ExploreContentSingle</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F165.png" width="32" height="32" alt="CollapseContent" /></td>
  <td>F165</td>
  <td>CollapseContent</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F166.png" width="32" height="32" alt="CollapseContentSingle" /></td>
  <td>F166</td>
  <td>CollapseContentSingle</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F167.png" width="32" height="32" alt="InfoSolid" /></td>
  <td>F167</td>
  <td>InfoSolid</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F168.png" width="32" height="32" alt="GroupList" /></td>
  <td>F168</td>
  <td>GroupList</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F169.png" width="32" height="32" alt="CaretBottomRightSolidCenter8" /></td>
  <td>F169</td>
  <td>CaretBottomRightSolidCenter8</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F16A.png" width="32" height="32" alt="ProgressRingDots" /></td>
  <td>F16A</td>
  <td>ProgressRingDots</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F16B.png" width="32" height="32" alt="Checkbox14" /></td>
  <td>F16B</td>
  <td>Checkbox14</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F16C.png" width="32" height="32" alt="CheckboxComposite14" /></td>
  <td>F16C</td>
  <td>CheckboxComposite14</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F16D.png" width="32" height="32" alt="CheckboxIndeterminateCombo14" /></td>
  <td>F16D</td>
  <td>CheckboxIndeterminateCombo14</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F16E.png" width="32" height="32" alt="CheckboxIndeterminateCombo" /></td>
  <td>F16E</td>
  <td>CheckboxIndeterminateCombo</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F175.png" width="32" height="32" alt="StatusPause7" /></td>
  <td>F175</td>
  <td>StatusPause7</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F17F.png" width="32" height="32" alt="CharacterAppearance" /></td>
  <td>F17F</td>
  <td>CharacterAppearance</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F180.png" width="32" height="32" alt="Lexicon " /></td>
  <td>F180</td>
  <td>Lexicon </td>
 </tr>
<tr><td><img src="images/segoe-mdl/F182.png" width="32" height="32" alt="ScreenTime" /></td>
  <td>F182</td>
  <td>ScreenTime</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F191.png" width="32" height="32" alt="HeadlessDevice" /></td>
  <td>F191</td>
  <td>HeadlessDevice</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F193.png" width="32" height="32" alt="NetworkSharing" /></td>
  <td>F193</td>
  <td>NetworkSharing</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F19D.png" width="32" height="32" alt="EyeGaze" /></td>
  <td>F19D</td>
  <td>EyeGaze</td>
 </tr>
 <tr><td><img src="images/segoe-mdl/F19E.png" width="32" height="32" alt="ToggleLeft" /></td>
  <td>F19E</td>
  <td>ToggleLeft</td>
 </tr>
 <tr><td><img src="images/segoe-mdl/F19F.png" width="32" height="32" alt="ToggleRight" /></td>
  <td>F19F</td>
  <td>ToggleRight</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F1AD.png" width="32" height="32" alt="WindowsInsider" /></td>
  <td>F1AD</td>
  <td>WindowsInsider</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F1CB.png" width="32" height="32" alt="ChromeSwitch" /></td>
  <td>F1CB</td>
  <td>ChromeSwitch</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F1CC.png" width="32" height="32" alt="ChromeSwitchContast" /></td>
  <td>F1CC</td>
  <td>ChromeSwitchContast</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F1D8.png" width="32" height="32" alt="StatusCheckmark" /></td>
  <td>F1D8</td>
  <td>StatusCheckmark</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F1D9.png" width="32" height="32" alt="StatusCheckmarkLeft" /></td>
  <td>F1D9</td>
  <td>StatusCheckmarkLeft</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F20C.png" width="32" height="32" alt="KeyboardLeftAligned" /></td>
  <td>F20C</td>
  <td>KeyboardLeftAligned</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F20D.png" width="32" height="32" alt="KeyboardRightAligned" /></td>
  <td>F20D</td>
  <td>KeyboardRightAligned</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F210.png" width="32" height="32" alt="KeyboardSettings" /></td>
  <td>F210</td>
  <td>KeyboardSettings</td>
 </tr>
 <tr><td><img src="images/segoe-mdl/F211.png" width="32" height="32" alt="NetworkPhysical" /></td>
  <td>F211</td>
  <td>NetworkPhysical</td>
</tr>
<tr><td><img src="images/segoe-mdl/F22C.png" width="32" height="32" alt="IOT" /></td>
  <td>F22C</td>
  <td>IOT</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F22E.png" width="32" height="32" alt="UnknownMirrored" /></td>
  <td>F22E</td>
  <td>UnknownMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F246.png" width="32" height="32" alt="ViewDashboard" /></td>
  <td>F246</td>
  <td>ViewDashboard</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F259.png" width="32" height="32" alt="ExploitProtectionSettings" /></td>
  <td>F259</td>
  <td>ExploitProtectionSettings</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F260.png" width="32" height="32" alt="KeyboardNarrow" /></td>
  <td>F260</td>
  <td>KeyboardNarrow</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F261.png" width="32" height="32" alt="Keyboard12Key" /></td>
  <td>F261</td>
  <td>Keyboard12Key</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F26B.png" width="32" height="32" alt="KeyboardDock" /></td>
  <td>F26B</td>
  <td>KeyboardDock</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F26C.png" width="32" height="32" alt="KeyboardUndock" /></td>
  <td>F26C</td>
  <td>KeyboardUndock</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F26D.png" width="32" height="32" alt="KeyboardLeftDock" /></td>
  <td>F26D</td>
  <td>KeyboardLeftDock</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F26E.png" width="32" height="32" alt="KeyboardRightDock" /></td>
  <td>F26E</td>
  <td>KeyboardRightDock</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F270.png" width="32" height="32" alt="Ear" /></td>
  <td>F270</td>
  <td>Ear</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F271.png" width="32" height="32" alt="PointerHand" /></td>
  <td>F271</td>
  <td>PointerHand</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F272.png" width="32" height="32" alt="Bullseye" /></td>
  <td>F272</td>
  <td>Bullseye</td>
 </tr>
 <tr><td><img src="images/segoe-mdl/F2B7.png" width="32" height="32" alt="LocaleLanguage" /></td>
  <td>F2B7</td>
  <td>LocaleLanguage</td>
</tr>
</table>

### PUA F300-F500

The following table of glyphs displays unicode points prefixed from F3-  to F5-.

[Back to top](#icon-list)

</br>
<table>
 <tr>
  <td>Glyph</td>
  <td>Unicode point</td>
  <td>Description</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F32A.png" width="32" height="32" alt="PassiveAuthentication" /></td>
  <td>F32A</td>
  <td>PassiveAuthentication</td>
 </tr>
 <tr><td><img src="images/segoe-mdl/F354.png" width="32" height="32" alt="ColorSolid" /></td>
  <td>F354</td>
  <td>ColorSolid</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F384.png" width="32" height="32" alt="NetworkOffline" /></td>
  <td>F384</td>
  <td>NetworkOffline</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F385.png" width="32" height="32" alt="NetworkConnected" /></td>
  <td>F385</td>
  <td>NetworkConnected</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F386.png" width="32" height="32" alt="NetworkConnectedCheckmark" /></td>
  <td>F386</td>
  <td>NetworkConnectedCheckmark</td>
 </tr>
 <tr><td><img src="images/segoe-mdl/F3B1.png" width="32" height="32" alt="SignOut" /></td>
  <td>F3B1</td>
  <td>SignOut</td>
</tr>
<tr><td><img src="images/segoe-mdl/F3CC.png" width="32" height="32" alt="StatusInfo" /></td>
  <td>F3CC</td>
  <td>StatusInfo</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F3CD.png" width="32" height="32" alt="StatusInfoLeft" /></td>
  <td>F3CD</td>
  <td>StatusInfoLeft</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F3E2.png" width="32" height="32" alt="NearbySharing" /></td>
  <td>F3E2</td>
  <td>NearbySharing</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F3E7.png" width="32" height="32" alt="CtrlSpatialLeft" /></td>
  <td>F3E7</td>
  <td>CtrlSpatialLeft</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F404.png" width="32" height="32" alt="InteractiveDashboard" /></td>
  <td>F404</td>
  <td>InteractiveDashboard</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F406.png" width="32" height="32" alt="ClippingTool" /></td>
  <td>F406</td>
  <td>ClippingTool</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F407.png" width="32" height="32" alt="RectangularClipping " /></td>
  <td>F407</td>
  <td>RectangularClipping </td>
 </tr>
<tr><td><img src="images/segoe-mdl/F408.png" width="32" height="32" alt="FreeFormClipping" /></td>
  <td>F408</td>
  <td>FreeFormClipping</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F413.png" width="32" height="32" alt="CopyTo" /></td>
  <td>F413</td>
  <td>CopyTo</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F439.png" width="32" height="32" alt="DynamicLock" /></td>
  <td>F439</td>
  <td>DynamicLock</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F45E.png" width="32" height="32" alt="PenTips" /></td>
  <td>F45E</td>
  <td>PenTips</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F45F.png" width="32" height="32" alt="PenTipsMirrored" /></td>
  <td>F45F</td>
  <td>PenTipsMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F460.png" width="32" height="32" alt="HWPJoin" /></td>
  <td>F460</td>
  <td>HWPJoin</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F461.png" width="32" height="32" alt="HWPInsert" /></td>
  <td>F461</td>
  <td>HWPInsert</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F462.png" width="32" height="32" alt="HWPStrikeThrough" /></td>
  <td>F462</td>
  <td>HWPStrikeThrough</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F463.png" width="32" height="32" alt="HWPScratchOut" /></td>
  <td>F463</td>
  <td>HWPScratchOut</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F464.png" width="32" height="32" alt="HWPSplit" /></td>
  <td>F464</td>
  <td>HWPSplit</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F465.png" width="32" height="32" alt="HWPNewLine" /></td>
  <td>F465</td>
  <td>HWPNewLine</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F466.png" width="32" height="32" alt="HWPOverwrite" /></td>
  <td>F466</td>
  <td>HWPOverwrite</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F473.png" width="32" height="32" alt="MobWifiWarning1" /></td>
  <td>F473</td>
  <td>MobWifiWarning1</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F474.png" width="32" height="32" alt="MobWifiWarning2" /></td>
  <td>F474</td>
  <td>MobWifiWarning2</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F475.png" width="32" height="32" alt="MobWifiWarning3" /></td>
  <td>F475</td>
  <td>MobWifiWarning3</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F476.png" width="32" height="32" alt="MobWifiWarning4" /></td>
  <td>F476</td>
  <td>MobWifiWarning4</td>
 </tr>
 <tr><td><img src="images/segoe-mdl/F49A.png" width="32" height="32" alt="Globe2" /></td>
  <td>F49A</td>
  <td>Globe2</td>
</tr>
 <tr><td><img src="images/segoe-mdl/F4A5.png" width="32" height="32" alt="SpecialEffectSize" /></td>
  <td>F4A5</td>
  <td>SpecialEffectSize</td>
</tr>
<tr><td><img src="images/segoe-mdl/F4A9.png" width="32" height="32" alt="GIF" /></td>
  <td>F4A9</td>
  <td>GIF</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F4AA.png" width="32" height="32" alt="Sticker2" /></td>
  <td>F4AA</td>
  <td>Sticker2</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F4BE.png" width="32" height="32" alt="SurfaceHubSelected" /></td>
  <td>F4BE</td>
  <td>SurfaceHubSelected</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F4BF.png" width="32" height="32" alt="HoloLensSelected" /></td>
  <td>F4BF</td>
  <td>HoloLensSelected</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F4C0.png" width="32" height="32" alt="Earbud" /></td>
  <td>F4C0</td>
  <td>Earbud</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F4C3.png" width="32" height="32" alt="MixVolumes" /></td>
  <td>F4C3</td>
  <td>MixVolumes</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F540.png" width="32" height="32" alt="Safe" /></td>
  <td>F540</td>
  <td>Safe</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F552.png" width="32" height="32" alt="LaptopSecure" /></td>
  <td>F552</td>
  <td>LaptopSecure</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F56D.png" width="32" height="32" alt="PrintDefault" /></td>
  <td>F56D</td>
  <td>PrintDefault</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F56E.png" width="32" height="32" alt="PageMirrored" /></td>
  <td>F56E</td>
  <td>PageMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F56F.png" width="32" height="32" alt="LandscapeOrientationMirrored" /></td>
  <td>F56F</td>
  <td>LandscapeOrientationMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F570.png" width="32" height="32" alt="ColorOff" /></td>
  <td>F570</td>
  <td>ColorOff</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F571.png" width="32" height="32" alt="PrintAllPages" /></td>
  <td>F571</td>
  <td>PrintAllPages</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F572.png" width="32" height="32" alt="PrintCustomRange" /></td>
  <td>F572</td>
  <td>PrintCustomRange</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F573.png" width="32" height="32" alt="PageMarginPortraitNarrow" /></td>
  <td>F573</td>
  <td>PageMarginPortraitNarrow</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F574.png" width="32" height="32" alt="PageMarginPortraitNormal" /></td>
  <td>F574</td>
  <td>PageMarginPortraitNormal</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F575.png" width="32" height="32" alt="PageMarginPortraitModerate" /></td>
  <td>F575</td>
  <td>PageMarginPortraitModerate</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F576.png" width="32" height="32" alt="PageMarginPortraitWide" /></td>
  <td>F576</td>
  <td>PageMarginPortraitWide</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F577.png" width="32" height="32" alt="PageMarginLandscapeNarrow" /></td>
  <td>F577</td>
  <td>PageMarginLandscapeNarrow</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F578.png" width="32" height="32" alt="PageMarginLandscapeNormal" /></td>
  <td>F578</td>
  <td>PageMarginLandscapeNormal</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F579.png" width="32" height="32" alt="PageMarginLandscapeModerate" /></td>
  <td>F579</td>
  <td>PageMarginLandscapeModerate</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F57A.png" width="32" height="32" alt="PageMarginLandscapeWide" /></td>
  <td>F57A</td>
  <td>PageMarginLandscapeWide</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F57B.png" width="32" height="32" alt="CollateLandscape" /></td>
  <td>F57B</td>
  <td>CollateLandscape</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F57C.png" width="32" height="32" alt="CollatePortrait" /></td>
  <td>F57C</td>
  <td>CollatePortrait</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F57D.png" width="32" height="32" alt="CollatePortraitSeparated" /></td>
  <td>F57D</td>
  <td>CollatePortraitSeparated</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F57E.png" width="32" height="32" alt="DuplexLandscapeOneSided" /></td>
  <td>F57E</td>
  <td>DuplexLandscapeOneSided</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F57F.png" width="32" height="32" alt="DuplexLandscapeOneSidedMirrored" /></td>
  <td>F57F</td>
  <td>DuplexLandscapeOneSidedMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F580.png" width="32" height="32" alt="DuplexLandscapeTwoSidedLongEdge" /></td>
  <td>F580</td>
  <td>DuplexLandscapeTwoSidedLongEdge</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F581.png" width="32" height="32" alt="DuplexLandscapeTwoSidedLongEdgeMirrored" /></td>
  <td>F581</td>
  <td>DuplexLandscapeTwoSidedLongEdgeMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F582.png" width="32" height="32" alt="DuplexLandscapeTwoSidedShortEdge" /></td>
  <td>F582</td>
  <td>DuplexLandscapeTwoSidedShortEdge</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F583.png" width="32" height="32" alt="DuplexLandscapeTwoSidedShortEdgeMirrored" /></td>
  <td>F583</td>
  <td>DuplexLandscapeTwoSidedShortEdgeMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F584.png" width="32" height="32" alt="DuplexPortraitOneSided" /></td>
  <td>F584</td>
  <td>DuplexPortraitOneSided</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F585.png" width="32" height="32" alt="DuplexPortraitOneSidedMirrored" /></td>
  <td>F585</td>
  <td>DuplexPortraitOneSidedMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F586.png" width="32" height="32" alt="DuplexPortraitTwoSidedLongEdge" /></td>
  <td>F586</td>
  <td>DuplexPortraitTwoSidedLongEdge</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F587.png" width="32" height="32" alt="DuplexPortraitTwoSidedLongEdgeMirrored" /></td>
  <td>F587</td>
  <td>DuplexPortraitTwoSidedLongEdgeMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F588.png" width="32" height="32" alt="DuplexPortraitTwoSidedShortEdge" /></td>
  <td>F588</td>
  <td>DuplexPortraitTwoSidedShortEdge</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F589.png" width="32" height="32" alt="DuplexPortraitTwoSidedShortEdgeMirrored" /></td>
  <td>F589</td>
  <td>DuplexPortraitTwoSidedShortEdgeMirrored</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F58A.png" width="32" height="32" alt="PPSOneLandscape" /></td>
  <td>F58A</td>
  <td>PPSOneLandscape</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F58B.png" width="32" height="32" alt="PPSTwoLandscape" /></td>
  <td>F58B</td>
  <td>PPSTwoLandscape</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F58C.png" width="32" height="32" alt="PPSTwoPortrait" /></td>
  <td>F58C</td>
  <td>PPSTwoPortrait</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F58D.png" width="32" height="32" alt="PPSFourLandscape" /></td>
  <td>F58D</td>
  <td>PPSFourLandscape</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F58E.png" width="32" height="32" alt="PPSFourPortrait" /></td>
  <td>F58E</td>
  <td>PPSFourPortrait</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F58F.png" width="32" height="32" alt="HolePunchOff" /></td>
  <td>F58F</td>
  <td>HolePunchOff</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F590.png" width="32" height="32" alt="HolePunchPortraitLeft" /></td>
  <td>F590</td>
  <td>HolePunchPortraitLeft</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F591.png" width="32" height="32" alt="HolePunchPortraitRight" /></td>
  <td>F591</td>
  <td>HolePunchPortraitRight</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F592.png" width="32" height="32" alt="HolePunchPortraitTop" /></td>
  <td>F592</td>
  <td>HolePunchPortraitTop</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F593.png" width="32" height="32" alt="HolePunchPortraitBottom" /></td>
  <td>F593</td>
  <td>HolePunchPortraitBottom</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F594.png" width="32" height="32" alt="HolePunchLandscapeLeft" /></td>
  <td>F594</td>
  <td>HolePunchLandscapeLeft</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F595.png" width="32" height="32" alt="HolePunchLandscapeRight" /></td>
  <td>F595</td>
  <td>HolePunchLandscapeRight</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F596.png" width="32" height="32" alt="HolePunchLandscapeTop" /></td>
  <td>F596</td>
  <td>HolePunchLandscapeTop</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F597.png" width="32" height="32" alt="HolePunchLandscapeBottom" /></td>
  <td>F597</td>
  <td>HolePunchLandscapeBottom</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F598.png" width="32" height="32" alt="StaplingOff" /></td>
  <td>F598</td>
  <td>StaplingOff</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F599.png" width="32" height="32" alt="StaplingPortraitTopLeft" /></td>
  <td>F599</td>
  <td>StaplingPortraitTopLeft</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F59A.png" width="32" height="32" alt="StaplingPortraitTopRight" /></td>
  <td>F59A</td>
  <td>StaplingPortraitTopRight</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F59B.png" width="32" height="32" alt="StaplingPortraitBottomRight" /></td>
  <td>F59B</td>
  <td>StaplingPortraitBottomRight</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F59C.png" width="32" height="32" alt="StaplingPortraitTwoLeft" /></td>
  <td>F59C</td>
  <td>StaplingPortraitTwoLeft</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F59D.png" width="32" height="32" alt="StaplingPortraitTwoRight" /></td>
  <td>F59D</td>
  <td>StaplingPortraitTwoRight</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F59E.png" width="32" height="32" alt="StaplingPortraitTwoTop" /></td>
  <td>F59E</td>
  <td>StaplingPortraitTwoTop</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F59F.png" width="32" height="32" alt="StaplingPortraitTwoBottom" /></td>
  <td>F59F</td>
  <td>StaplingPortraitTwoBottom</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F5A0.png" width="32" height="32" alt="StaplingPortraitBookBinding" /></td>
  <td>F5A0</td>
  <td>StaplingPortraitBookBinding</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F5A1.png" width="32" height="32" alt="StaplingLandscapeTopLeft" /></td>
  <td>F5A1</td>
  <td>StaplingLandscapeTopLeft</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F5A2.png" width="32" height="32" alt="StaplingLandscapeTopRight" /></td>
  <td>F5A2</td>
  <td>StaplingLandscapeTopRight</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F5A3.png" width="32" height="32" alt="StaplingLandscapeBottomLeft" /></td>
  <td>F5A3</td>
  <td>StaplingLandscapeBottomLeft</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F5A4.png" width="32" height="32" alt="StaplingLandscapeBottomRight" /></td>
  <td>F5A4</td>
  <td>StaplingLandscapeBottomRight</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F5A5.png" width="32" height="32" alt="StaplingLandscapeTwoLeft" /></td>
  <td>F5A5</td>
  <td>StaplingLandscapeTwoLeft</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F5A6.png" width="32" height="32" alt="StaplingLandscapeTwoRight" /></td>
  <td>F5A6</td>
  <td>StaplingLandscapeTwoRight</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F5A7.png" width="32" height="32" alt="StaplingLandscapeTwoTop" /></td>
  <td>F5A7</td>
  <td>StaplingLandscapeTwoTop</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F5A8.png" width="32" height="32" alt="StaplingLandscapeTwoBottom" /></td>
  <td>F5A8</td>
  <td>StaplingLandscapeTwoBottom</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F5A9.png" width="32" height="32" alt="StaplingLandscapeBookBinding" /></td>
  <td>F5A9</td>
  <td>StaplingLandscapeBookBinding</td>
 </tr>
 <tr><td><img src="images/segoe-mdl/F5AA.png" width="32" height="32" alt="StatusDataTransferRoaming" /></td>
  <td>F5AA</td>
  <td>StatusDataTransferRoaming</td>
</tr>
<tr><td><img src="images/segoe-mdl/F5AB.png" width="32" height="32" alt="MobSIMError" /></td>
  <td>F5AB</td>
  <td>MobSIMError</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F5AC.png" width="32" height="32" alt="CollateLandscapeSeparated" /></td>
  <td>F5AC</td>
  <td>CollateLandscapeSeparated</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F5AD.png" width="32" height="32" alt="PPSOnePortrait" /></td>
  <td>F5AD</td>
  <td>PPSOnePortrait</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F5AE.png" width="32" height="32" alt="StaplingPortraitBottomLeft" /></td>
  <td>F5AE</td>
  <td>StaplingPortraitBottomLeft</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F5B0.png" width="32" height="32" alt="PlaySolid" /></td>
  <td>F5B0</td>
  <td>PlaySolid</td>
 </tr>
 <tr><td><img src="images/segoe-mdl/F5E7.png" width="32" height="32" alt="RepeatOff" /></td>
  <td>F5E7</td>
  <td>RepeatOff</td>
</tr>
<tr><td><img src="images/segoe-mdl/F5ED.png" width="32" height="32" alt="Set" /></td>
  <td>F5ED</td>
  <td>Set</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F5EE.png" width="32" height="32" alt="SetSolid" /></td>
  <td>F5EE</td>
  <td>SetSolid</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F5EF.png" width="32" height="32" alt="FuzzyReading" /></td>
  <td>F5EF</td>
  <td>FuzzyReading</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F5F2.png" width="32" height="32" alt="VerticalBattery0" /></td>
  <td>F5F2</td>
  <td>VerticalBattery0</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F5F3.png" width="32" height="32" alt="VerticalBattery1" /></td>
  <td>F5F3</td>
  <td>VerticalBattery1</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F5F4.png" width="32" height="32" alt="VerticalBattery2" /></td>
  <td>F5F4</td>
  <td>VerticalBattery2</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F5F5.png" width="32" height="32" alt="VerticalBattery3" /></td>
  <td>F5F5</td>
  <td>VerticalBattery3</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F5F6.png" width="32" height="32" alt="VerticalBattery4" /></td>
  <td>F5F6</td>
  <td>VerticalBattery4</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F5F7.png" width="32" height="32" alt="VerticalBattery5" /></td>
  <td>F5F7</td>
  <td>VerticalBattery5</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F5F8.png" width="32" height="32" alt="VerticalBattery6" /></td>
  <td>F5F8</td>
  <td>VerticalBattery6</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F5F9.png" width="32" height="32" alt="VerticalBattery7" /></td>
  <td>F5F9</td>
  <td>VerticalBattery7</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F5FA.png" width="32" height="32" alt="VerticalBattery8" /></td>
  <td>F5FA</td>
  <td>VerticalBattery8</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F5FB.png" width="32" height="32" alt="VerticalBattery9" /></td>
  <td>F5FB</td>
  <td>VerticalBattery9</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F5FC.png" width="32" height="32" alt="VerticalBattery10" /></td>
  <td>F5FC</td>
  <td>VerticalBattery10</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F5FD.png" width="32" height="32" alt="VerticalBatteryCharging0" /></td>
  <td>F5FD</td>
  <td>VerticalBatteryCharging0</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F5FE.png" width="32" height="32" alt="VerticalBatteryCharging1" /></td>
  <td>F5FE</td>
  <td>VerticalBatteryCharging1</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F5FF.png" width="32" height="32" alt="VerticalBatteryCharging2" /></td>
  <td>F5FF</td>
  <td>VerticalBatteryCharging2</td>
 </tr>
</table>

 ### PUA F600-F800

The following table of glyphs displays unicode points prefixed from F6-  to F8-.

[Back to top](#icon-list)

</br>
<table>
 <tr>
  <td>Glyph</td>
  <td>Unicode point</td>
  <td>Description</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F600.png" width="32" height="32" alt="VerticalBatteryCharging3" /></td>
  <td>F600</td>
  <td>VerticalBatteryCharging3</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F601.png" width="32" height="32" alt="VerticalBatteryCharging4" /></td>
  <td>F601</td>
  <td>VerticalBatteryCharging4</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F602.png" width="32" height="32" alt="VerticalBatteryCharging5" /></td>
  <td>F602</td>
  <td>VerticalBatteryCharging5</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F603.png" width="32" height="32" alt="VerticalBatteryCharging6" /></td>
  <td>F603</td>
  <td>VerticalBatteryCharging6</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F604.png" width="32" height="32" alt="VerticalBatteryCharging7" /></td>
  <td>F604</td>
  <td>VerticalBatteryCharging7</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F605.png" width="32" height="32" alt="VerticalBatteryCharging8" /></td>
  <td>F605</td>
  <td>VerticalBatteryCharging8</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F606.png" width="32" height="32" alt="VerticalBatteryCharging9" /></td>
  <td>F606</td>
  <td>VerticalBatteryCharging9</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F607.png" width="32" height="32" alt="VerticalBatteryCharging10" /></td>
  <td>F607</td>
  <td>VerticalBatteryCharging10</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F608.png" width="32" height="32" alt="VerticalBatteryUnknown" /></td>
  <td>F608</td>
  <td>VerticalBatteryUnknown</td>
 </tr>
 <tr><td><img src="images/segoe-mdl/F614.png" width="32" height="32" alt="DoublePortrait" /></td>
  <td>F614</td>
  <td>DoublePortrait</td>
</tr>
<tr><td><img src="images/segoe-mdl/F615.png" width="32" height="32" alt="DoubleLandscape" /></td>
  <td>F615</td>
  <td>DoubleLandscape</td>
</tr>
<tr><td><img src="images/segoe-mdl/F616.png" width="32" height="32" alt="SinglePortrait" /></td>
  <td>F616</td>
  <td>SinglePortrait</td>
</tr>
<tr><td><img src="images/segoe-mdl/F617.png" width="32" height="32" alt="SingleLandscape" /></td>
  <td>F617</td>
  <td>SingleLandscape</td>
</tr>
<tr><td><img src="images/segoe-mdl/F618.png" width="32" height="32" alt="SIMError" /></td>
  <td>F618</td>
  <td>SIMError</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F619.png" width="32" height="32" alt="SIMMissing" /></td>
  <td>F619</td>
  <td>SIMMissing</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F61A.png" width="32" height="32" alt="SIMLock" /></td>
  <td>F61A</td>
  <td>SIMLock</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F61B.png" width="32" height="32" alt="eSIM" /></td>
  <td>F61B</td>
  <td>eSIM</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F61C.png" width="32" height="32" alt="eSIMNoProfile" /></td>
  <td>F61C</td>
  <td>eSIMNoProfile</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F61D.png" width="32" height="32" alt="eSIMLocked" /></td>
  <td>F61D</td>
  <td>eSIMLocked</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F61E.png" width="32" height="32" alt="eSIMBusy" /></td>
  <td>F61E</td>
  <td>eSIMBusy</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F61F.png" width="32" height="32" alt="NoiseCancelation" /></td>
  <td>F61F</td>
  <td>NoiseCancelation</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F620.png" width="32" height="32" alt="NoiseCancelationOff" /></td>
  <td>F620</td>
  <td>NoiseCancelationOff</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F623.png" width="32" height="32" alt="MusicSharing" /></td>
  <td>F623</td>
  <td>MusicSharing</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F624.png" width="32" height="32" alt="MusicSharingOff" /></td>
  <td>F624</td>
  <td>MusicSharingOff</td>
 </tr>
<tr><td><img src="images/segoe-mdl/F63C.png" width="32" height="32" alt="CircleShapeSolid" /></td>
  <td>F63C</td>
  <td>CircleShapeSolid</td>
</tr>
<tr><td><img src="images/segoe-mdl/F657.png" width="32" height="32" alt="F657 WifiCallBars" /></td>
  <td>F657</td>
  <td>WifiCallBars</td>
</tr>
<tr><td><img src="images/segoe-mdl/F658.png" width="32" height="32" alt="F658 WifiCall0" /></td>
  <td>F658</td>
  <td>WifiCall0</td>
</tr>
<tr><td><img src="images/segoe-mdl/F659.png" width="32" height="32" alt="F659 WifiCall1" /></td>
  <td>F659</td>
  <td>WifiCall1</td>
</tr>
<tr><td><img src="images/segoe-mdl/F65A.png" width="32" height="32" alt="F65A WifiCall2" /></td>
  <td>F65A</td>
  <td>WifiCall2</td>
</tr>
<tr><td><img src="images/segoe-mdl/F65B.png" width="32" height="32" alt="F65B WifiCall3" /></td>
  <td>F65B</td>
  <td>WifiCall3</td>
</tr>
<tr><td><img src="images/segoe-mdl/F65C.png" width="32" height="32" alt="F65C WifiCall4" /></td>
  <td>F65C</td>
  <td>WifiCall4</td>
</tr>
<tr><td><img src="images/segoe-mdl/F69E.png" width="32" height="32" alt="CHTLanguageBar" /></td>
  <td>F69E</td>
  <td>CHTLanguageBar</td>
</tr>
<tr><td><img src="images/segoe-mdl/F6A9.png" width="32" height="32" alt="ComposeMode" /></td>
  <td>F6A9</td>
  <td>ComposeMode</td>
</tr>
<tr><td><img src="images/segoe-mdl/F6B8.png" width="32" height="32" alt="ExpressiveInputEntry" /></td>
  <td>F6B8</td>
  <td>ExpressiveInputEntry</td>
</tr>
<tr><td><img src="images/segoe-mdl/F6BA.png" width="32" height="32" alt="EmojiTabMoreSymbols" /></td>
  <td>F6BA</td>
  <td>EmojiTabMoreSymbols</td>
</tr>
<tr><td><img src="images/segoe-mdl/F6FA.png" width="32" height="32" alt="WebSearch" /></td>
  <td>F6FA</td>
  <td>WebSearch</td>
</tr>
<tr><td><img src="images/segoe-mdl/F712.png" width="32" height="32" alt="Kiosk" /></td>
  <td>F712</td>
  <td>Kiosk</td>
</tr>
<tr><td><img src="images/segoe-mdl/F714.png" width="32" height="32" alt="RTTLogo" /></td>
  <td>F714</td>
  <td>RTTLogo</td>
</tr>
<tr><td><img src="images/segoe-mdl/F715.png" width="32" height="32" alt="VoiceCall" /></td>
  <td>F715</td>
  <td>VoiceCall</td>
</tr>
<tr><td><img src="images/segoe-mdl/F716.png" width="32" height="32" alt="GoToMessage" /></td>
  <td>F716</td>
  <td>GoToMessage</td>
</tr>
<tr><td><img src="images/segoe-mdl/F71A.png" width="32" height="32" alt="ReturnToCall" /></td>
  <td>F71A</td>
  <td>ReturnToCall</td>
</tr>
<tr><td><img src="images/segoe-mdl/F71C.png" width="32" height="32" alt="StartPresenting" /></td>
  <td>F71C</td>
  <td>StartPresenting</td>
</tr>
<tr><td><img src="images/segoe-mdl/F71D.png" width="32" height="32" alt="StopPresenting" /></td>
  <td>F71D</td>
  <td>StopPresenting</td>
</tr>
<tr><td><img src="images/segoe-mdl/F71E.png" width="32" height="32" alt="ProductivityMode" /></td>
  <td>F71E</td>
  <td>ProductivityMode</td>
</tr>
<tr><td><img src="images/segoe-mdl/F738.png" width="32" height="32" alt="SetHistoryStatus" /></td>
  <td>F738</td>
  <td>SetHistoryStatus</td>
</tr>
<tr><td><img src="images/segoe-mdl/F739.png" width="32" height="32" alt="SetHistoryStatus2" /></td>
  <td>F739</td>
  <td>SetHistoryStatus2</td>
</tr>
<tr><td><img src="images/segoe-mdl/F73D.png" width="32" height="32" alt="Keyboardsettings20" /></td>
  <td>F73D</td>
  <td>Keyboardsettings20</td>
</tr>
<tr><td><img src="images/segoe-mdl/F73E.png" width="32" height="32" alt="OneHandedRight20" /></td>
  <td>F73E</td>
  <td>OneHandedRight20</td>
</tr>
<tr><td><img src="images/segoe-mdl/F73F.png" width="32" height="32" alt="OneHandedLeft20" /></td>
  <td>F73F</td>
  <td>OneHandedLeft20</td>
</tr>
<tr><td><img src="images/segoe-mdl/F740.png" width="32" height="32" alt="Split20" /></td>
  <td>F740</td>
  <td>Split20</td>
</tr>
<tr><td><img src="images/segoe-mdl/F741.png" width="32" height="32" alt="Full20" /></td>
  <td>F741</td>
  <td>Full20</td>
</tr>
<tr><td><img src="images/segoe-mdl/F742.png" width="32" height="32" alt="Handwriting20" /></td>
  <td>F742</td>
  <td>Handwriting20</td>
</tr>
<tr><td><img src="images/segoe-mdl/F743.png" width="32" height="32" alt="CheveronLeft20" /></td>
  <td>F743</td>
  <td>CheveronLeft20</td>
</tr>
<tr><td><img src="images/segoe-mdl/F744.png" width="32" height="32" alt="CheveronLeft32" /></td>
  <td>F744</td>
  <td>CheveronLeft32</td>
</tr>
<tr><td><img src="images/segoe-mdl/F745.png" width="32" height="32" alt="CheveronRight20" /></td>
  <td>F745</td>
  <td>CheveronRight20</td>
</tr>
<tr><td><img src="images/segoe-mdl/F746.png" width="32" height="32" alt="CheveronRight32" /></td>
  <td>F746</td>
  <td>CheveronRight32</td>
</tr>
<tr><td><img src="images/segoe-mdl/F781.png" width="32" height="32" alt="MicOff2" /></td>
  <td>F781</td>
  <td>MicOff2</td>
</tr>
<tr><td><img src="images/segoe-mdl/F785.png" width="32" height="32" alt="DeliveryOptimization" /></td>
  <td>F785</td>
  <td>DeliveryOptimization</td>
</tr>
<tr><td><img src="images/segoe-mdl/F78A.png" width="32" height="32" alt="CancelMedium" /></td>
  <td>F78A</td>
  <td>CancelMedium</td>
</tr>
<tr><td><img src="images/segoe-mdl/F78B.png" width="32" height="32" alt="SearchMedium" /></td>
  <td>F78B</td>
  <td>SearchMedium</td>
</tr>
<tr><td><img src="images/segoe-mdl/F78C.png" width="32" height="32" alt="AcceptMedium" /></td>
  <td>F78C</td>
  <td>AcceptMedium</td>
</tr>
<tr><td><img src="images/segoe-mdl/F78D.png" width="32" height="32" alt="RevealPasswordMedium" /></td>
  <td>F78D</td>
  <td>RevealPasswordMedium</td>
</tr>
<tr><td><img src="images/segoe-mdl/F7AD.png" width="32" height="32" alt="DeleteWord" /></td>
  <td>F7AD</td>
  <td>DeleteWord</td>
</tr>
<tr><td><img src="images/segoe-mdl/F7AE.png" width="32" height="32" alt="DeleteWordFill" /></td>
  <td>F7AE</td>
  <td>DeleteWordFill</td>
</tr>
<tr><td><img src="images/segoe-mdl/F7AF.png" width="32" height="32" alt="DeleteLines" /></td>
  <td>F7AF</td>
  <td>DeleteLines</td>
</tr>
<tr><td><img src="images/segoe-mdl/F7B0.png" width="32" height="32" alt="DeleteLinesFill" /></td>
  <td>F7B0</td>
  <td>DeleteLinesFill</td>
</tr>
<tr><td><img src="images/segoe-mdl/F7B1.png" width="32" height="32" alt="InstertWords" /></td>
  <td>F7B1</td>
  <td>InstertWords</td>
</tr>
<tr><td><img src="images/segoe-mdl/F7B2.png" width="32" height="32" alt="InstertWordsFill" /></td>
  <td>F7B2</td>
  <td>InstertWordsFill</td>
</tr>
<tr><td><img src="images/segoe-mdl/F7B3.png" width="32" height="32" alt="JoinWords" /></td>
  <td>F7B3</td>
  <td>JoinWords</td>
</tr>
<tr><td><img src="images/segoe-mdl/F7B4.png" width="32" height="32" alt="JoinWordsFill" /></td>
  <td>F7B4</td>
  <td>JoinWordsFill</td>
</tr>
<tr><td><img src="images/segoe-mdl/F7B5.png" width="32" height="32" alt="OverwriteWords" /></td>
  <td>F7B5</td>
  <td>OverwriteWords</td>
</tr>
<tr><td><img src="images/segoe-mdl/F7B6.png" width="32" height="32" alt="OverwriteWordsFill" /></td>
  <td>F7B6</td>
  <td>OverwriteWordsFill</td>
</tr>
<tr><td><img src="images/segoe-mdl/F7B7.png" width="32" height="32" alt="AddNewLine" /></td>
  <td>F7B7</td>
  <td>AddNewLine</td>
</tr>
<tr><td><img src="images/segoe-mdl/F7B8.png" width="32" height="32" alt="AddNewLineFill" /></td>
  <td>F7B8</td>
  <td>AddNewLineFill</td>
</tr>
<tr><td><img src="images/segoe-mdl/F7B9.png" width="32" height="32" alt="OverwriteWordsKorean" /></td>
  <td>F7B9</td>
  <td>OverwriteWordsKorean</td>
</tr>
<tr><td><img src="images/segoe-mdl/F7BA.png" width="32" height="32" alt="OverwriteWordsFillKorean" /></td>
  <td>F7BA</td>
  <td>OverwriteWordsFillKorean</td>
</tr>
<tr><td><img src="images/segoe-mdl/F7BB.png" width="32" height="32" alt="EducationIcon" /></td>
  <td>F7BB</td>
  <td>EducationIcon</td>
</tr>
<tr><td><img src="images/segoe-mdl/F7ED.png" width="32" height="32" alt="WindowSnipping" /></td>
  <td>F7ED</td>
  <td>WindowSnipping</td>
</tr>
<tr><td><img src="images/segoe-mdl/F7EE.png" width="32" height="32" alt="VideoCapture" /></td>
  <td>F7EE</td>
  <td>VideoCapture</td>
</tr>
<tr><td><img src="images/segoe-mdl/F809.png" width="32" height="32" alt="StatusSecured" /></td>
  <td>F809</td>
  <td>StatusSecured</td>
</tr>
<tr><td><img src="images/segoe-mdl/F83B.png" width="32" height="32" alt="NarratorApp" /></td>
  <td>F83B</td>
  <td>NarratorApp</td>
</tr>
<tr><td><img src="images/segoe-mdl/F83D.png" width="32" height="32" alt="PowerButtonUpdate" /></td>
  <td>F83D</td>
  <td>PowerButtonUpdate</td>
</tr>
<tr><td><img src="images/segoe-mdl/F83E.png" width="32" height="32" alt="RestartUpdate" /></td>
  <td>F83E</td>
  <td>RestartUpdate</td>
</tr>
<tr><td><img src="images/segoe-mdl/F83F.png" width="32" height="32" alt="UpdateStatusDot" /></td>
  <td>F83F</td>
  <td>UpdateStatusDot</td>
</tr>
<tr><td><img src="images/segoe-mdl/F847.png" width="32" height="32" alt="Eject" /></td>
  <td>F847</td>
  <td>Eject</td>
</tr>
<tr><td><img src="images/segoe-mdl/F87B.png" width="32" height="32" alt="Spelling" /></td>
  <td>F87B</td>
  <td>Spelling</td>
</tr>
<tr><td><img src="images/segoe-mdl/F87C.png" width="32" height="32" alt="SpellingKorean" /></td>
  <td>F87C</td>
  <td>SpellingKorean</td>
</tr>
<tr><td><img src="images/segoe-mdl/F87D.png" width="32" height="32" alt="SpellingSerbian" /></td>
  <td>F87D</td>
  <td>SpellingSerbian</td>
</tr>
<tr><td><img src="images/segoe-mdl/F87E.png" width="32" height="32" alt="SpellingChinese" /></td>
  <td>F87E</td>
  <td>SpellingChinese</td>
</tr>
<tr><td><img src="images/segoe-mdl/F89A.png" width="32" height="32" alt="FolderSelect" /></td>
  <td>F89A</td>
  <td>FolderSelect</td>
</tr>
<tr><td><img src="images/segoe-mdl/F8A5.png" width="32" height="32" alt="SmartScreen" /></td>
  <td>F8A5</td>
  <td>SmartScreen</td>
</tr>
<tr><td><img src="images/segoe-mdl/F8A6.png" width="32" height="32" alt="ExploitProtection" /></td>
  <td>F8A6</td>
  <td>ExploitProtection</td>
</tr>
<tr><td><img src="images/segoe-mdl/F8AA.png" width="32" height="32" alt="AddBold" /></td>
  <td>F8AA</td>
  <td>AddBold</td>
</tr>
<tr><td><img src="images/segoe-mdl/F8AB.png" width="32" height="32" alt="SubtractBold" /></td>
  <td>F8AB</td>
  <td>SubtractBold</td>
</tr>
<tr><td><img src="images/segoe-mdl/F8AC.png" width="32" height="32" alt="BackSolidBold" /></td>
  <td>F8AC</td>
  <td>BackSolidBold</td>
</tr>
<tr><td><img src="images/segoe-mdl/F8AD.png" width="32" height="32" alt="ForwardSolidBold" /></td>
  <td>F8AD</td>
  <td>ForwardSolidBold</td>
</tr>
<tr><td><img src="images/segoe-mdl/F8AE.png" width="32" height="32" alt="PauseBold" /></td>
  <td>F8AE</td>
  <td>PauseBold</td>
</tr>
<tr><td><img src="images/segoe-mdl/F8AF.png" width="32" height="32" alt="ClickSolid" /></td>
  <td>F8AF</td>
  <td>ClickSolid</td>
</tr>
<tr><td><img src="images/segoe-mdl/F8B0.png" width="32" height="32" alt="SettingsSolid" /></td>
  <td>F8B0</td>
  <td>SettingsSolid</td>
</tr>
<tr><td><img src="images/segoe-mdl/F8B1.png" width="32" height="32" alt="MicrophoneSolidBold" /></td>
  <td>F8B1</td>
  <td>MicrophoneSolidBold</td>
</tr>
<tr><td><img src="images/segoe-mdl/F8B2.png" width="32" height="32" alt="SpeechSolidBold" /></td>
  <td>F8B2</td>
  <td>SpeechSolidBold</td>
</tr>
<tr><td><img src="images/segoe-mdl/F8B3.png" width="32" height="32" alt="ClickedOutLoudSolidBold" /></td>
  <td>F8B3</td>
  <td>ClickedOutLoudSolidBold</td>
</tr>
</table>



## Related articles

* [Guidelines for icons](../style/icons.md)
* [Symbol enumeration](/uwp/api/Windows.UI.Xaml.Controls.Symbol)
* [FontIcon class](/uwp/api/windows.ui.xaml.controls.fonticon)
