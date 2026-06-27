---
description: This article lists and provides usage guidance for the glyphs that come with the Segoe MDL2 Assets font.
Search.Refinement.TopicID: 184
title: Segoe MDL2 Assets icons
ms.assetid: DFB215C2-8A61-4957-B662-3B1991AC9BE1
label: Segoe MDL2 Assets icons
template: detail.hbs
ms.date: 09/02/2025
ms.topic: article
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
> With the release of Windows 11, the `Segoe Fluent Icons` font replaced `Segoe MDL2 Assets` as the recommended symbol icon font. `Segoe UI Symbol` and `Segoe MDL2 Assets` are still available, but we recommend updating your app to use the [Segoe Fluent Icons font](../style/segoe-fluent-icons-font.md).

Most of the icons included in the `Segoe MDL2 Assets` font are mapped to the Private Use Area of Unicode (PUA). The PUA allows font developers to assign private Unicode values to glyphs that don't map to existing code points. This is useful when creating a symbol font, but it creates an interoperability problem. If the font is not available, the glyphs won't show up. Use these glyphs only when you can explicitly specify the `Segoe MDL2 Assets` font. If you are working with tiles, you can't use these glyphs because you can't specify the tile font and PUA glyphs are not available via font-fallback.

Unlike with `Segoe UI Symbol`, the icons in the `Segoe MDL2 Assets` font are not intended for use in-line with text. This means that some older "tricks" like the progressive disclosure arrows no longer apply. Likewise, since all of the new icons are sized and positioned the same, they do not have to be made with zero width; we have just made sure they work as a set. Ideally, you can overlay two icons that were designed as a set and they will fall into place. We may do this to allow colorization in the code. For example, U+EA3A and U+EA3B were created for the Start tile Badge status. Because these are already centered the circle fill can be colored for different states.

## Layering and mirroring

All glyphs in `Segoe MDL2 Assets` have the same fixed width with a consistent height and left origin point, so layering and colorization effects can be achieved by drawing glyphs directly on top of each other. This example show a black outline drawn on top of the zero-width red heart.

:::image type="content" border="false" source="images/segoe-ui-symbol-layering.png" alt-text="Screenshot of using a zero-width glyph.":::

Many of the icons also have mirrored forms available for use in languages that use right-to-left text directionality such as Arabic, Dari, Persian, and Hebrew.

## Using the icons

To use a glyph from the `Segoe MDL2 Assets` font, use a [**FontIcon**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.fonticon).

```xaml
<FontIcon FontFamily="Segoe MDL2 Assets" Glyph="&#xE700;"/>
```

## How do I get this font?

* On Windows: There's nothing you need to do, the font comes with Windows.
* On a Mac, you need to download and install the font: [Get the Segoe UI and MDL2 icon fonts](https://aka.ms/SegoeFonts)

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

| Glyph | Unicode point | Description |
|-------|---------------|-------------|
| :::image type="content" border="false" source="images/segoe-mdl/E700.png" alt-text="Screenshot of GlobalNavigationButton."::: | E700 | :::no-loc text="GlobalNavigationButton"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E701.png" alt-text="Screenshot of Wifi."::: | E701 | :::no-loc text="Wifi"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E702.png" alt-text="Screenshot of Bluetooth."::: | E702 | :::no-loc text="Bluetooth"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E703.png" alt-text="Screenshot of Connect."::: | E703 | :::no-loc text="Connect"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E704.png" alt-text="Screenshot of InternetSharing."::: | E704 | :::no-loc text="InternetSharing"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E705.png" alt-text="Screenshot of VPN."::: | E705 | :::no-loc text="VPN"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E706.png" alt-text="Screenshot of Brightness."::: | E706 | :::no-loc text="Brightness"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E707.png" alt-text="Screenshot of MapPin."::: | E707 | :::no-loc text="MapPin"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E708.png" alt-text="Screenshot of QuietHours."::: | E708 | :::no-loc text="QuietHours"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E709.png" alt-text="Screenshot of Airplane."::: | E709 | :::no-loc text="Airplane"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E70A.png" alt-text="Screenshot of Tablet."::: | E70A | :::no-loc text="Tablet"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E70B.png" alt-text="Screenshot of QuickNote."::: | E70B | :::no-loc text="QuickNote"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E70C.png" alt-text="Screenshot of RememberedDevice."::: | E70C | :::no-loc text="RememberedDevice"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E70D.png" alt-text="Screenshot of ChevronDown."::: | E70D | :::no-loc text="ChevronDown"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E70E.png" alt-text="Screenshot of ChevronUp."::: | E70E | :::no-loc text="ChevronUp"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E70F.png" alt-text="Screenshot of Edit."::: | E70F | :::no-loc text="Edit"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E710.png" alt-text="Screenshot of Add."::: | E710 | :::no-loc text="Add"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E711.png" alt-text="Screenshot of Cancel."::: | E711 | :::no-loc text="Cancel"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E712.png" alt-text="Screenshot of More."::: | E712 | :::no-loc text="More"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E713.png" alt-text="Screenshot of Setting."::: | E713 | :::no-loc text="Setting"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E714.png" alt-text="Screenshot of Video."::: | E714 | :::no-loc text="Video"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E715.png" alt-text="Screenshot of Mail."::: | E715 | :::no-loc text="Mail"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E716.png" alt-text="Screenshot of People."::: | E716 | :::no-loc text="People"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E717.png" alt-text="Screenshot of Phone."::: | E717 | :::no-loc text="Phone"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E718.png" alt-text="Screenshot of Pin."::: | E718 | :::no-loc text="Pin"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E719.png" alt-text="Screenshot of Shop."::: | E719 | :::no-loc text="Shop"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E71A.png" alt-text="Screenshot of Stop."::: | E71A | :::no-loc text="Stop"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E71B.png" alt-text="Screenshot of Link."::: | E71B | :::no-loc text="Link"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E71C.png" alt-text="Screenshot of Filter."::: | E71C | :::no-loc text="Filter"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E71D.png" alt-text="Screenshot of AllApps."::: | E71D | :::no-loc text="AllApps"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E71E.png" alt-text="Screenshot of Zoom."::: | E71E | :::no-loc text="Zoom"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E71F.png" alt-text="Screenshot of ZoomOut."::: | E71F | :::no-loc text="ZoomOut"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E720.png" alt-text="Screenshot of Microphone."::: | E720 | :::no-loc text="Microphone"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E721.png" alt-text="Screenshot of Search."::: | E721 | :::no-loc text="Search"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E722.png" alt-text="Screenshot of Camera."::: | E722 | :::no-loc text="Camera"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E723.png" alt-text="Screenshot of Attach."::: | E723 | :::no-loc text="Attach"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E724.png" alt-text="Screenshot of Send."::: | E724 | :::no-loc text="Send"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E725.png" alt-text="Screenshot of SendFill."::: | E725 | :::no-loc text="SendFill"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E726.png" alt-text="Screenshot of WalkSolid."::: | E726 | :::no-loc text="WalkSolid"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E727.png" alt-text="Screenshot of InPrivate."::: | E727 | :::no-loc text="InPrivate"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E728.png" alt-text="Screenshot of FavoriteList."::: | E728 | :::no-loc text="FavoriteList"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E729.png" alt-text="Screenshot of PageSolid."::: | E729 | :::no-loc text="PageSolid"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E72A.png" alt-text="Screenshot of Forward."::: | E72A | :::no-loc text="Forward"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E72B.png" alt-text="Screenshot of Back."::: | E72B | :::no-loc text="Back"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E72C.png" alt-text="Screenshot of Refresh."::: | E72C | :::no-loc text="Refresh"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E72D.png" alt-text="Screenshot of Share."::: | E72D | :::no-loc text="Share"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E72E.png" alt-text="Screenshot of Lock."::: | E72E | :::no-loc text="Lock"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E730.png" alt-text="Screenshot of ReportHacked."::: | E730 | :::no-loc text="ReportHacked"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E731.png" alt-text="Screenshot of EMI."::: | E731 | :::no-loc text="EMI"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E734.png" alt-text="Screenshot of FavoriteStar."::: | E734 | :::no-loc text="FavoriteStar"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E735.png" alt-text="Screenshot of FavoriteStarFill."::: | E735 | :::no-loc text="FavoriteStarFill"::: |
 | :::image type="content" border="false" source="images/segoe-mdl/E736.png" alt-text="Screenshot of ReadingMode."::: | E736 | :::no-loc text="ReadingMode"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E737.png" alt-text="Screenshot of Favicon."::: | E737 | :::no-loc text="Favicon"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E738.png" alt-text="Screenshot of Remove."::: | E738 | :::no-loc text="Remove"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E739.png" alt-text="Screenshot of Checkbox."::: | E739 | :::no-loc text="Checkbox"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E73A.png" alt-text="Screenshot of CheckboxComposite."::: | E73A | :::no-loc text="CheckboxComposite"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E73B.png" alt-text="Screenshot of CheckboxFill."::: | E73B | :::no-loc text="CheckboxFill"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E73C.png" alt-text="Screenshot of CheckboxIndeterminate."::: | E73C | :::no-loc text="CheckboxIndeterminate"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E73D.png" alt-text="Screenshot of CheckboxCompositeReversed."::: | E73D | :::no-loc text="CheckboxCompositeReversed"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E73E.png" alt-text="Screenshot of CheckMark."::: | E73E | :::no-loc text="CheckMark"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E73F.png" alt-text="Screenshot of BackToWindow."::: | E73F | :::no-loc text="BackToWindow"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E740.png" alt-text="Screenshot of FullScreen."::: | E740 | :::no-loc text="FullScreen"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E741.png" alt-text="Screenshot of ResizeTouchLarger."::: | E741 | :::no-loc text="ResizeTouchLarger"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E742.png" alt-text="Screenshot of ResizeTouchSmaller."::: | E742 | :::no-loc text="ResizeTouchSmaller"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E743.png" alt-text="Screenshot of ResizeMouseSmall."::: | E743 | :::no-loc text="ResizeMouseSmall"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E744.png" alt-text="Screenshot of ResizeMouseMedium."::: | E744 | :::no-loc text="ResizeMouseMedium"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E745.png" alt-text="Screenshot of ResizeMouseWide."::: | E745 | :::no-loc text="ResizeMouseWide"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E746.png" alt-text="Screenshot of ResizeMouseTall."::: | E746 | :::no-loc text="ResizeMouseTall"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E747.png" alt-text="Screenshot of ResizeMouseLarge."::: | E747 | :::no-loc text="ResizeMouseLarge"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E748.png" alt-text="Screenshot of SwitchUser."::: | E748 | :::no-loc text="SwitchUser"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E749.png" alt-text="Screenshot of Print."::: | E749 | :::no-loc text="Print"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E74A.png" alt-text="Screenshot of Up."::: | E74A | :::no-loc text="Up"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E74B.png" alt-text="Screenshot of Down."::: | E74B | :::no-loc text="Down"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E74C.png" alt-text="Screenshot of OEM."::: | E74C | :::no-loc text="OEM"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E74D.png" alt-text="Screenshot of Delete."::: | E74D | :::no-loc text="Delete"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E74E.png" alt-text="Screenshot of Save."::: | E74E | :::no-loc text="Save"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E74F.png" alt-text="Screenshot of Mute."::: | E74F | :::no-loc text="Mute"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E750.png" alt-text="Screenshot of BackSpaceQWERTY."::: | E750 | :::no-loc text="BackSpaceQWERTY"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E751.png" alt-text="Screenshot of ReturnKey."::: | E751 | :::no-loc text="ReturnKey"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E752.png" alt-text="Screenshot of UpArrowShiftKey."::: | E752 | :::no-loc text="UpArrowShiftKey"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E753.png" alt-text="Screenshot of Cloud."::: | E753 | :::no-loc text="Cloud"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E754.png" alt-text="Screenshot of Flashlight."::: | E754 | :::no-loc text="Flashlight"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E755.png" alt-text="Screenshot of RotationLock."::: | E755 | :::no-loc text="RotationLock"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E756.png" alt-text="Screenshot of CommandPrompt."::: | E756 | :::no-loc text="CommandPrompt"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E759.png" alt-text="Screenshot of SIPMove."::: | E759 | :::no-loc text="SIPMove"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E75A.png" alt-text="Screenshot of SIPUndock."::: | E75A | :::no-loc text="SIPUndock"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E75B.png" alt-text="Screenshot of SIPRedock."::: | E75B | :::no-loc text="SIPRedock"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E75C.png" alt-text="Screenshot of EraseTool."::: | E75C | :::no-loc text="EraseTool"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E75D.png" alt-text="Screenshot of UnderscoreSpace."::: | E75D | :::no-loc text="UnderscoreSpace"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E75E.png" alt-text="Screenshot of GripperTool."::: | E75E | :::no-loc text="GripperTool"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E75F.png" alt-text="Screenshot of Dialpad."::: | E75F | :::no-loc text="Dialpad"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E760.png" alt-text="Screenshot of PageLeft."::: | E760 | :::no-loc text="PageLeft"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E761.png" alt-text="Screenshot of PageRight."::: | E761 | :::no-loc text="PageRight"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E762.png" alt-text="Screenshot of MultiSelect."::: | E762 | :::no-loc text="MultiSelect"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E763.png" alt-text="Screenshot of KeyboardLeftHanded."::: | E763 | :::no-loc text="KeyboardLeftHanded"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E764.png" alt-text="Screenshot of KeyboardRightHanded."::: | E764 | :::no-loc text="KeyboardRightHanded"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E765.png" alt-text="Screenshot of KeyboardClassic."::: | E765 | :::no-loc text="KeyboardClassic"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E766.png" alt-text="Screenshot of KeyboardSplit."::: | E766 | :::no-loc text="KeyboardSplit"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E767.png" alt-text="Screenshot of Volume."::: | E767 | :::no-loc text="Volume"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E768.png" alt-text="Screenshot of Play."::: | E768 | :::no-loc text="Play"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E769.png" alt-text="Screenshot of Pause."::: | E769 | :::no-loc text="Pause"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E76B.png" alt-text="Screenshot of ChevronLeft."::: | E76B | :::no-loc text="ChevronLeft"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E76C.png" alt-text="Screenshot of ChevronRight."::: | E76C | :::no-loc text="ChevronRight"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E76D.png" alt-text="Screenshot of InkingTool."::: | E76D | :::no-loc text="InkingTool"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E76E.png" alt-text="Screenshot of Emoji2."::: | E76E | :::no-loc text="Emoji2"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E76F.png" alt-text="Screenshot of GripperBarHorizontal."::: | E76F | :::no-loc text="GripperBarHorizontal"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E770.png" alt-text="Screenshot of System."::: | E770 | :::no-loc text="System"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E771.png" alt-text="Screenshot of Personalize."::: | E771 | :::no-loc text="Personalize"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E772.png" alt-text="Screenshot of Devices."::: | E772 | :::no-loc text="Devices"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E773.png" alt-text="Screenshot of SearchAndApps."::: | E773 | :::no-loc text="SearchAndApps"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E774.png" alt-text="Screenshot of Globe."::: | E774 | :::no-loc text="Globe"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E775.png" alt-text="Screenshot of TimeLanguage."::: | E775 | :::no-loc text="TimeLanguage"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E776.png" alt-text="Screenshot of EaseOfAccess."::: | E776 | :::no-loc text="EaseOfAccess"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E777.png" alt-text="Screenshot of UpdateRestore."::: | E777 | :::no-loc text="UpdateRestore"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E778.png" alt-text="Screenshot of HangUp."::: | E778 | :::no-loc text="HangUp"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E779.png" alt-text="Screenshot of ContactInfo."::: | E779 | :::no-loc text="ContactInfo"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E77A.png" alt-text="Screenshot of Unpin."::: | E77A | :::no-loc text="Unpin"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E77B.png" alt-text="Screenshot of Contact."::: | E77B | :::no-loc text="Contact"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E77C.png" alt-text="Screenshot of Memo."::: | E77C | :::no-loc text="Memo"::: |
 | :::image type="content" border="false" source="images/segoe-mdl/E77E.png" alt-text="Screenshot of IncomingCall."::: | E77E | :::no-loc text="IncomingCall"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E77F.png" alt-text="Screenshot of Paste."::: | E77F | :::no-loc text="Paste"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E780.png" alt-text="Screenshot of PhoneBook."::: | E780 | :::no-loc text="PhoneBook"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E781.png" alt-text="Screenshot of LEDLight."::: | E781 | :::no-loc text="LEDLight"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E783.png" alt-text="Screenshot of Error."::: | E783 | :::no-loc text="Error"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E784.png" alt-text="Screenshot of GripperBarVertical."::: | E784 | :::no-loc text="GripperBarVertical"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E785.png" alt-text="Screenshot of Unlock."::: | E785 | :::no-loc text="Unlock"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E786.png" alt-text="Screenshot of Slideshow."::: | E786 | :::no-loc text="Slideshow"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E787.png" alt-text="Screenshot of Calendar."::: | E787 | :::no-loc text="Calendar"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E788.png" alt-text="Screenshot of GripperResize."::: | E788 | :::no-loc text="GripperResize"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E789.png" alt-text="Screenshot of Megaphone."::: | E789 | :::no-loc text="Megaphone"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E78A.png" alt-text="Screenshot of Trim."::: | E78A | :::no-loc text="Trim"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E78B.png" alt-text="Screenshot of NewWindow."::: | E78B | :::no-loc text="NewWindow"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E78C.png" alt-text="Screenshot of SaveLocal."::: | E78C | :::no-loc text="SaveLocal"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E790.png" alt-text="Screenshot of Color."::: | E790 | :::no-loc text="Color"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E791.png" alt-text="Screenshot of DataSense."::: | E791 | :::no-loc text="DataSense"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E792.png" alt-text="Screenshot of SaveAs."::: | E792 | :::no-loc text="SaveAs"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E793.png" alt-text="Screenshot of Light."::: | E793 | :::no-loc text="Light"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E799.png" alt-text="Screenshot of AspectRatio."::: | E799 | :::no-loc text="AspectRatio"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E7A5.png" alt-text="Screenshot of DataSenseBar."::: | E7A5 | :::no-loc text="DataSenseBar"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E7A6.png" alt-text="Screenshot of Redo."::: | E7A6 | :::no-loc text="Redo"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E7A7.png" alt-text="Screenshot of Undo."::: | E7A7 | :::no-loc text="Undo"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E7A8.png" alt-text="Screenshot of Crop."::: | E7A8 | :::no-loc text="Crop"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E7AC.png" alt-text="Screenshot of OpenWith."::: | E7AC | :::no-loc text="OpenWith"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E7AD.png" alt-text="Screenshot of Rotate."::: | E7AD | :::no-loc text="Rotate"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E7B3.png" alt-text="Screenshot of RedEye."::: | E7B3 | :::no-loc text="RedEye"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E7B5.png" alt-text="Screenshot of SetlockScreen."::: | E7B5 | :::no-loc text="SetlockScreen"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E7B7.png" alt-text="Screenshot of MapPin2."::: | E7B7 | :::no-loc text="MapPin2"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E7B8.png" alt-text="Screenshot of Package."::: | E7B8 | :::no-loc text="Package"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E7BA.png" alt-text="Screenshot of Warning."::: | E7BA | :::no-loc text="Warning"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E7BC.png" alt-text="Screenshot of ReadingList."::: | E7BC | :::no-loc text="ReadingList"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E7BE.png" alt-text="Screenshot of Education."::: | E7BE | :::no-loc text="Education"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E7BF.png" alt-text="Screenshot of ShoppingCart."::: | E7BF | :::no-loc text="ShoppingCart"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E7C0.png" alt-text="Screenshot of Train."::: | E7C0 | :::no-loc text="Train"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E7C1.png" alt-text="Screenshot of Flag."::: | E7C1 | :::no-loc text="Flag"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E7C3.png" alt-text="Screenshot of Page."::: | E7C3 | :::no-loc text="Page"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E7C4.png" alt-text="Screenshot of TaskView."::: | E7C4 | :::no-loc text="TaskView"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E7C5.png" alt-text="Screenshot of BrowsePhotos."::: | E7C5 | :::no-loc text="BrowsePhotos"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E7C6.png" alt-text="Screenshot of HalfStarLeft."::: | E7C6 | :::no-loc text="HalfStarLeft"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E7C7.png" alt-text="Screenshot of HalfStarRight."::: | E7C7 | :::no-loc text="HalfStarRight"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E7C8.png" alt-text="Screenshot of Record."::: | E7C8 | :::no-loc text="Record"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E7C9.png" alt-text="Screenshot of TouchPointer."::: | E7C9 | :::no-loc text="TouchPointer"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E7DE.png" alt-text="Screenshot of LangJPN."::: | E7DE | :::no-loc text="LangJPN"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E7E3.png" alt-text="Screenshot of Ferry."::: | E7E3 | :::no-loc text="Ferry"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E7E6.png" alt-text="Screenshot of Highlight."::: | E7E6 | :::no-loc text="Highlight"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E7E7.png" alt-text="Screenshot of ActionCenterNotification."::: | E7E7 | :::no-loc text="ActionCenterNotification"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E7E8.png" alt-text="Screenshot of PowerButton."::: | E7E8 | :::no-loc text="PowerButton"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E7EA.png" alt-text="Screenshot of ResizeTouchNarrower."::: | E7EA | :::no-loc text="ResizeTouchNarrower"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E7EB.png" alt-text="Screenshot of ResizeTouchShorter."::: | E7EB | :::no-loc text="ResizeTouchShorter"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E7EC.png" alt-text="Screenshot of DrivingMode."::: | E7EC | :::no-loc text="DrivingMode"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E7ED.png" alt-text="Screenshot of RingerSilent."::: | E7ED | :::no-loc text="RingerSilent"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E7EE.png" alt-text="Screenshot of OtherUser."::: | E7EE | :::no-loc text="OtherUser"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E7EF.png" alt-text="Screenshot of Admin."::: | E7EF | :::no-loc text="Admin"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E7F0.png" alt-text="Screenshot of CC."::: | E7F0 | :::no-loc text="CC"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E7F1.png" alt-text="Screenshot of SDCard."::: | E7F1 | :::no-loc text="SDCard"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E7F2.png" alt-text="Screenshot of CallForwarding."::: | E7F2 | :::no-loc text="CallForwarding"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E7F3.png" alt-text="Screenshot of SettingsDisplaySound."::: | E7F3 | :::no-loc text="SettingsDisplaySound"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E7F4.png" alt-text="Screenshot of TVMonitor."::: | E7F4 | :::no-loc text="TVMonitor"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E7F5.png" alt-text="Screenshot of Speakers."::: | E7F5 | :::no-loc text="Speakers"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E7F6.png" alt-text="Screenshot of Headphone."::: | E7F6 | :::no-loc text="Headphone"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E7F7.png" alt-text="Screenshot of DeviceLaptopPic."::: | E7F7 | :::no-loc text="DeviceLaptopPic"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E7F8.png" alt-text="Screenshot of DeviceLaptopNoPic."::: | E7F8 | :::no-loc text="DeviceLaptopNoPic"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E7F9.png" alt-text="Screenshot of DeviceMonitorRightPic."::: | E7F9 | :::no-loc text="DeviceMonitorRightPic"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E7FA.png" alt-text="Screenshot of DeviceMonitorLeftPic."::: | E7FA | :::no-loc text="DeviceMonitorLeftPic"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E7FB.png" alt-text="Screenshot of DeviceMonitorNoPic."::: | E7FB | :::no-loc text="DeviceMonitorNoPic"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E7FC.png" alt-text="Screenshot of Game."::: | E7FC | :::no-loc text="Game"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E7FD.png" alt-text="Screenshot of HorizontalTabKey."::: | E7FD | :::no-loc text="HorizontalTabKey"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E802.png" alt-text="Screenshot of StreetsideSplitMinimize."::: | E802 | :::no-loc text="StreetsideSplitMinimize"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E803.png" alt-text="Screenshot of StreetsideSplitExpand."::: | E803 | :::no-loc text="StreetsideSplitExpand"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E804.png" alt-text="Screenshot of Car."::: | E804 | :::no-loc text="Car"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E805.png" alt-text="Screenshot of Walk."::: | E805 | :::no-loc text="Walk"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E806.png" alt-text="Screenshot of Bus."::: | E806 | :::no-loc text="Bus"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E809.png" alt-text="Screenshot of TiltUp."::: | E809 | :::no-loc text="TiltUp"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E80A.png" alt-text="Screenshot of TiltDown."::: | E80A | :::no-loc text="TiltDown"::: |
 | :::image type="content" border="false" source="images/segoe-mdl/E80B.png" alt-text="Screenshot of CallControl."::: | E80B | :::no-loc text="CallControl"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E80C.png" alt-text="Screenshot of RotateMapRight."::: | E80C | :::no-loc text="RotateMapRight"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E80D.png" alt-text="Screenshot of RotateMapLeft."::: | E80D | :::no-loc text="RotateMapLeft"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E80F.png" alt-text="Screenshot of Home."::: | E80F | :::no-loc text="Home"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E811.png" alt-text="Screenshot of ParkingLocation."::: | E811 | :::no-loc text="ParkingLocation"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E812.png" alt-text="Screenshot of MapCompassTop."::: | E812 | :::no-loc text="MapCompassTop"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E813.png" alt-text="Screenshot of MapCompassBottom."::: | E813 | :::no-loc text="MapCompassBottom"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E814.png" alt-text="Screenshot of IncidentTriangle."::: | E814 | :::no-loc text="IncidentTriangle"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E815.png" alt-text="Screenshot of Touch."::: | E815 | :::no-loc text="Touch"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E816.png" alt-text="Screenshot of MapDirections."::: | E816 | :::no-loc text="MapDirections"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E819.png" alt-text="Screenshot of StartPoint."::: | E819 | :::no-loc text="StartPoint"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E81A.png" alt-text="Screenshot of StopPoint."::: | E81A | :::no-loc text="StopPoint"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E81B.png" alt-text="Screenshot of EndPoint."::: | E81B | :::no-loc text="EndPoint"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E81C.png" alt-text="Screenshot of History."::: | E81C | :::no-loc text="History"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E81D.png" alt-text="Screenshot of Location."::: | E81D | :::no-loc text="Location"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E81E.png" alt-text="Screenshot of MapLayers."::: | E81E | :::no-loc text="MapLayers"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E81F.png" alt-text="Screenshot of Accident."::: | E81F | :::no-loc text="Accident"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E821.png" alt-text="Screenshot of Work."::: | E821 | :::no-loc text="Work"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E822.png" alt-text="Screenshot of Construction."::: | E822 | :::no-loc text="Construction"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E823.png" alt-text="Screenshot of Recent."::: | E823 | :::no-loc text="Recent"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E825.png" alt-text="Screenshot of Bank."::: | E825 | :::no-loc text="Bank"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E826.png" alt-text="Screenshot of DownloadMap."::: | E826 | :::no-loc text="DownloadMap"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E829.png" alt-text="Screenshot of InkingToolFill2."::: | E829 | :::no-loc text="InkingToolFill2"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E82A.png" alt-text="Screenshot of HighlightFill2."::: | E82A | :::no-loc text="HighlightFill2"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E82B.png" alt-text="Screenshot of EraseToolFill."::: | E82B | :::no-loc text="EraseToolFill"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E82C.png" alt-text="Screenshot of EraseToolFill2."::: | E82C | :::no-loc text="EraseToolFill2"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E82D.png" alt-text="Screenshot of Dictionary."::: | E82D | :::no-loc text="Dictionary"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E82E.png" alt-text="Screenshot of DictionaryAdd."::: | E82E | :::no-loc text="DictionaryAdd"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E82F.png" alt-text="Screenshot of ToolTip."::: | E82F | :::no-loc text="ToolTip"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E830.png" alt-text="Screenshot of ChromeBack."::: | E830 | :::no-loc text="ChromeBack"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E835.png" alt-text="Screenshot of ProvisioningPackage."::: | E835 | :::no-loc text="ProvisioningPackage"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E836.png" alt-text="Screenshot of AddRemoteDevice."::: | E836 | :::no-loc text="AddRemoteDevice"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E838.png" alt-text="Screenshot of FolderOpen."::: | E838 | :::no-loc text="FolderOpen"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E839.png" alt-text="Screenshot of Ethernet."::: | E839 | :::no-loc text="Ethernet"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E83A.png" alt-text="Screenshot of  ShareBroadband."::: | E83A | :::no-loc text=" ShareBroadband"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E83B.png" alt-text="Screenshot of DirectAccess."::: | E83B | :::no-loc text="DirectAccess"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E83C.png" alt-text="Screenshot of  DialUp."::: | E83C | :::no-loc text=" DialUp"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E83D.png" alt-text="Screenshot of DefenderApp ."::: | E83D | :::no-loc text="DefenderApp "::: |
| :::image type="content" border="false" source="images/segoe-mdl/E83E.png" alt-text="Screenshot of BatteryCharging9."::: | E83E | :::no-loc text="BatteryCharging9"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E83F.png" alt-text="Screenshot of Battery10."::: | E83F | :::no-loc text="Battery10"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E840.png" alt-text="Screenshot of Pinned."::: | E840 | :::no-loc text="Pinned"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E841.png" alt-text="Screenshot of PinFill."::: | E841 | :::no-loc text="PinFill"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E842.png" alt-text="Screenshot of PinnedFill."::: | E842 | :::no-loc text="PinnedFill"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E843.png" alt-text="Screenshot of PeriodKey."::: | E843 | :::no-loc text="PeriodKey"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E844.png" alt-text="Screenshot of PuncKey."::: | E844 | :::no-loc text="PuncKey"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E845.png" alt-text="Screenshot of RevToggleKey."::: | E845 | :::no-loc text="RevToggleKey"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E846.png" alt-text="Screenshot of RightArrowKeyTime1."::: | E846 | :::no-loc text="RightArrowKeyTime1"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E847.png" alt-text="Screenshot of RightArrowKeyTime2."::: | E847 | :::no-loc text="RightArrowKeyTime2"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E848.png" alt-text="Screenshot of LeftQuote."::: | E848 | :::no-loc text="LeftQuote"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E849.png" alt-text="Screenshot of RightQuote."::: | E849 | :::no-loc text="RightQuote"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E84A.png" alt-text="Screenshot of DownShiftKey."::: | E84A | :::no-loc text="DownShiftKey"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E84B.png" alt-text="Screenshot of UpShiftKey."::: | E84B | :::no-loc text="UpShiftKey"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E84C.png" alt-text="Screenshot of PuncKey0."::: | E84C | :::no-loc text="PuncKey0"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E84D.png" alt-text="Screenshot of PuncKeyLeftBottom."::: | E84D | :::no-loc text="PuncKeyLeftBottom"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E84E.png" alt-text="Screenshot of RightArrowKeyTime3."::: | E84E | :::no-loc text="RightArrowKeyTime3"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E84F.png" alt-text="Screenshot of RightArrowKeyTime4."::: | E84F | :::no-loc text="RightArrowKeyTime4"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E850.png" alt-text="Screenshot of Battery0."::: | E850 | :::no-loc text="Battery0"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E851.png" alt-text="Screenshot of Battery1."::: | E851 | :::no-loc text="Battery1"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E852.png" alt-text="Screenshot of Battery2."::: | E852 | :::no-loc text="Battery2"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E853.png" alt-text="Screenshot of Battery3."::: | E853 | :::no-loc text="Battery3"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E854.png" alt-text="Screenshot of Battery4."::: | E854 | :::no-loc text="Battery4"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E855.png" alt-text="Screenshot of Battery5."::: | E855 | :::no-loc text="Battery5"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E856.png" alt-text="Screenshot of Battery6."::: | E856 | :::no-loc text="Battery6"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E857.png" alt-text="Screenshot of Battery7."::: | E857 | :::no-loc text="Battery7"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E858.png" alt-text="Screenshot of Battery8."::: | E858 | :::no-loc text="Battery8"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E859.png" alt-text="Screenshot of Battery9."::: | E859 | :::no-loc text="Battery9"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E85A.png" alt-text="Screenshot of BatteryCharging0."::: | E85A | :::no-loc text="BatteryCharging0"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E85B.png" alt-text="Screenshot of BatteryCharging1."::: | E85B | :::no-loc text="BatteryCharging1"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E85C.png" alt-text="Screenshot of BatteryCharging2."::: | E85C | :::no-loc text="BatteryCharging2"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E85D.png" alt-text="Screenshot of BatteryCharging3."::: | E85D | :::no-loc text="BatteryCharging3"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E85E.png" alt-text="Screenshot of BatteryCharging4."::: | E85E | :::no-loc text="BatteryCharging4"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E85F.png" alt-text="Screenshot of BatteryCharging5."::: | E85F | :::no-loc text="BatteryCharging5"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E860.png" alt-text="Screenshot of BatteryCharging6."::: | E860 | :::no-loc text="BatteryCharging6"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E861.png" alt-text="Screenshot of BatteryCharging7."::: | E861 | :::no-loc text="BatteryCharging7"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E862.png" alt-text="Screenshot of BatteryCharging8."::: | E862 | :::no-loc text="BatteryCharging8"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E863.png" alt-text="Screenshot of BatterySaver0."::: | E863 | :::no-loc text="BatterySaver0"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E864.png" alt-text="Screenshot of BatterySaver1."::: | E864 | :::no-loc text="BatterySaver1"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E865.png" alt-text="Screenshot of BatterySaver2."::: | E865 | :::no-loc text="BatterySaver2"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E866.png" alt-text="Screenshot of BatterySaver3."::: | E866 | :::no-loc text="BatterySaver3"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E867.png" alt-text="Screenshot of BatterySaver4."::: | E867 | :::no-loc text="BatterySaver4"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E868.png" alt-text="Screenshot of BatterySaver5."::: | E868 | :::no-loc text="BatterySaver5"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E869.png" alt-text="Screenshot of BatterySaver6."::: | E869 | :::no-loc text="BatterySaver6"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E86A.png" alt-text="Screenshot of BatterySaver7."::: | E86A | :::no-loc text="BatterySaver7"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E86B.png" alt-text="Screenshot of BatterySaver8."::: | E86B | :::no-loc text="BatterySaver8"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E86C.png" alt-text="Screenshot of SignalBars1."::: | E86C | :::no-loc text="SignalBars1"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E86D.png" alt-text="Screenshot of SignalBars2."::: | E86D | :::no-loc text="SignalBars2"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E86E.png" alt-text="Screenshot of SignalBars3."::: | E86E | :::no-loc text="SignalBars3"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E86F.png" alt-text="Screenshot of SignalBars4."::: | E86F | :::no-loc text="SignalBars4"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E870.png" alt-text="Screenshot of SignalBars5."::: | E870 | :::no-loc text="SignalBars5"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E871.png" alt-text="Screenshot of SignalNotConnected."::: | E871 | :::no-loc text="SignalNotConnected"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E872.png" alt-text="Screenshot of Wifi1."::: | E872 | :::no-loc text="Wifi1"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E873.png" alt-text="Screenshot of Wifi2."::: | E873 | :::no-loc text="Wifi2"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E874.png" alt-text="Screenshot of Wifi3."::: | E874 | :::no-loc text="Wifi3"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E875.png" alt-text="Screenshot of MobSIMLock."::: | E875 | :::no-loc text="MobSIMLock"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E876.png" alt-text="Screenshot of MobSIMMissing."::: | E876 | :::no-loc text="MobSIMMissing"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E877.png" alt-text="Screenshot of Vibrate."::: | E877 | :::no-loc text="Vibrate"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E878.png" alt-text="Screenshot of RoamingInternational."::: | E878 | :::no-loc text="RoamingInternational"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E879.png" alt-text="Screenshot of RoamingDomestic."::: | E879 | :::no-loc text="RoamingDomestic"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E87A.png" alt-text="Screenshot of CallForwardInternational."::: | E87A | :::no-loc text="CallForwardInternational"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E87B.png" alt-text="Screenshot of CallForwardRoaming."::: | E87B | :::no-loc text="CallForwardRoaming"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E87C.png" alt-text="Screenshot of JpnRomanji."::: | E87C | :::no-loc text="JpnRomanji"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E87D.png" alt-text="Screenshot of JpnRomanjiLock."::: | E87D | :::no-loc text="JpnRomanjiLock"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E87E.png" alt-text="Screenshot of JpnRomanjiShift."::: | E87E | :::no-loc text="JpnRomanjiShift"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E87F.png" alt-text="Screenshot of JpnRomanjiShiftLock."::: | E87F | :::no-loc text="JpnRomanjiShiftLock"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E880.png" alt-text="Screenshot of StatusDataTransfer."::: | E880 | :::no-loc text="StatusDataTransfer"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E881.png" alt-text="Screenshot of StatusDataTransferVPN."::: | E881 | :::no-loc text="StatusDataTransferVPN"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E882.png" alt-text="Screenshot of StatusDualSIM2."::: | E882 | :::no-loc text="StatusDualSIM2"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E883.png" alt-text="Screenshot of StatusDualSIM2VPN."::: | E883 | :::no-loc text="StatusDualSIM2VPN"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E884.png" alt-text="Screenshot of StatusDualSIM1."::: | E884 | :::no-loc text="StatusDualSIM1"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E885.png" alt-text="Screenshot of StatusDualSIM1VPN."::: | E885 | :::no-loc text="StatusDualSIM1VPN"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E886.png" alt-text="Screenshot of StatusSGLTE."::: | E886 | :::no-loc text="StatusSGLTE"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E887.png" alt-text="Screenshot of StatusSGLTECell."::: | E887 | :::no-loc text="StatusSGLTECell"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E888.png" alt-text="Screenshot of StatusSGLTEDataVPN."::: | E888 | :::no-loc text="StatusSGLTEDataVPN"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E889.png" alt-text="Screenshot of StatusVPN."::: | E889 | :::no-loc text="StatusVPN"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E88A.png" alt-text="Screenshot of WifiHotspot."::: | E88A | :::no-loc text="WifiHotspot"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E88B.png" alt-text="Screenshot of LanguageKor."::: | E88B | :::no-loc text="LanguageKor"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E88C.png" alt-text="Screenshot of LanguageCht."::: | E88C | :::no-loc text="LanguageCht"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E88D.png" alt-text="Screenshot of LanguageChs."::: | E88D | :::no-loc text="LanguageChs"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E88E.png" alt-text="Screenshot of USB."::: | E88E | :::no-loc text="USB"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E88F.png" alt-text="Screenshot of InkingToolFill."::: | E88F | :::no-loc text="InkingToolFill"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E890.png" alt-text="Screenshot of View."::: | E890 | :::no-loc text="View"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E891.png" alt-text="Screenshot of HighlightFill."::: | E891 | :::no-loc text="HighlightFill"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E892.png" alt-text="Screenshot of Previous."::: | E892 | :::no-loc text="Previous"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E893.png" alt-text="Screenshot of Next."::: | E893 | :::no-loc text="Next"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E894.png" alt-text="Screenshot of Clear."::: | E894 | :::no-loc text="Clear"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E895.png" alt-text="Screenshot of Sync."::: | E895 | :::no-loc text="Sync"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E896.png" alt-text="Screenshot of Download."::: | E896 | :::no-loc text="Download"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E897.png" alt-text="Screenshot of Help."::: | E897 | :::no-loc text="Help"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E898.png" alt-text="Screenshot of Upload."::: | E898 | :::no-loc text="Upload"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E899.png" alt-text="Screenshot of Emoji."::: | E899 | :::no-loc text="Emoji"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E89A.png" alt-text="Screenshot of TwoPage."::: | E89A | :::no-loc text="TwoPage"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E89B.png" alt-text="Screenshot of LeaveChat."::: | E89B | :::no-loc text="LeaveChat"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E89C.png" alt-text="Screenshot of MailForward."::: | E89C | :::no-loc text="MailForward"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E89E.png" alt-text="Screenshot of RotateCamera."::: | E89E | :::no-loc text="RotateCamera"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E89F.png" alt-text="Screenshot of ClosePane."::: | E89F | :::no-loc text="ClosePane"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8A0.png" alt-text="Screenshot of OpenPane."::: | E8A0 | :::no-loc text="OpenPane"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8A1.png" alt-text="Screenshot of PreviewLink."::: | E8A1 | :::no-loc text="PreviewLink"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8A2.png" alt-text="Screenshot of AttachCamera."::: | E8A2 | :::no-loc text="AttachCamera"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8A3.png" alt-text="Screenshot of ZoomIn."::: | E8A3 | :::no-loc text="ZoomIn"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8A4.png" alt-text="Screenshot of Bookmarks."::: | E8A4 | :::no-loc text="Bookmarks"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8A5.png" alt-text="Screenshot of Document."::: | E8A5 | :::no-loc text="Document"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8A6.png" alt-text="Screenshot of ProtectedDocument."::: | E8A6 | :::no-loc text="ProtectedDocument"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8A7.png" alt-text="Screenshot of OpenInNewWindow."::: | E8A7 | :::no-loc text="OpenInNewWindow"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8A8.png" alt-text="Screenshot of MailFill."::: | E8A8 | :::no-loc text="MailFill"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8A9.png" alt-text="Screenshot of ViewAll."::: | E8A9 | :::no-loc text="ViewAll"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8AA.png" alt-text="Screenshot of VideoChat."::: | E8AA | :::no-loc text="VideoChat"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8AB.png" alt-text="Screenshot of Switch."::: | E8AB | :::no-loc text="Switch"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8AC.png" alt-text="Screenshot of Rename."::: | E8AC | :::no-loc text="Rename"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8AD.png" alt-text="Screenshot of Go."::: | E8AD | :::no-loc text="Go"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8AE.png" alt-text="Screenshot of SurfaceHub."::: | E8AE | :::no-loc text="SurfaceHub"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8AF.png" alt-text="Screenshot of Remote."::: | E8AF | :::no-loc text="Remote"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8B0.png" alt-text="Screenshot of Click."::: | E8B0 | :::no-loc text="Click"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8B1.png" alt-text="Screenshot of Shuffle."::: | E8B1 | :::no-loc text="Shuffle"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8B2.png" alt-text="Screenshot of Movies."::: | E8B2 | :::no-loc text="Movies"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8B3.png" alt-text="Screenshot of SelectAll."::: | E8B3 | :::no-loc text="SelectAll"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8B4.png" alt-text="Screenshot of Orientation."::: | E8B4 | :::no-loc text="Orientation"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8B5.png" alt-text="Screenshot of Import."::: | E8B5 | :::no-loc text="Import"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8B6.png" alt-text="Screenshot of ImportAll."::: | E8B6 | :::no-loc text="ImportAll"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8B7.png" alt-text="Screenshot of Folder."::: | E8B7 | :::no-loc text="Folder"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8B8.png" alt-text="Screenshot of Webcam."::: | E8B8 | :::no-loc text="Webcam"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8B9.png" alt-text="Screenshot of Picture."::: | E8B9 | :::no-loc text="Picture"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8BA.png" alt-text="Screenshot of Caption."::: | E8BA | :::no-loc text="Caption"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8BB.png" alt-text="Screenshot of ChromeClose."::: | E8BB | :::no-loc text="ChromeClose"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8BC.png" alt-text="Screenshot of ShowResults."::: | E8BC | :::no-loc text="ShowResults"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8BD.png" alt-text="Screenshot of Message."::: | E8BD | :::no-loc text="Message"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8BE.png" alt-text="Screenshot of Leaf."::: | E8BE | :::no-loc text="Leaf"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8BF.png" alt-text="Screenshot of CalendarDay."::: | E8BF | :::no-loc text="CalendarDay"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8C0.png" alt-text="Screenshot of CalendarWeek."::: | E8C0 | :::no-loc text="CalendarWeek"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8C1.png" alt-text="Screenshot of Characters."::: | E8C1 | :::no-loc text="Characters"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8C2.png" alt-text="Screenshot of MailReplyAll."::: | E8C2 | :::no-loc text="MailReplyAll"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8C3.png" alt-text="Screenshot of Read."::: | E8C3 | :::no-loc text="Read"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8C4.png" alt-text="Screenshot of ShowBcc."::: | E8C4 | :::no-loc text="ShowBcc"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8C5.png" alt-text="Screenshot of HideBcc."::: | E8C5 | :::no-loc text="HideBcc"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8C6.png" alt-text="Screenshot of Cut."::: | E8C6 | :::no-loc text="Cut"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8C7.png" alt-text="Screenshot of PaymentCard."::: | E8C7 | :::no-loc text="PaymentCard"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8C8.png" alt-text="Screenshot of Copy."::: | E8C8 | :::no-loc text="Copy"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8C9.png" alt-text="Screenshot of Important."::: | E8C9 | :::no-loc text="Important"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8CA.png" alt-text="Screenshot of MailReply."::: | E8CA | :::no-loc text="MailReply"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8CB.png" alt-text="Screenshot of Sort."::: | E8CB | :::no-loc text="Sort"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8CC.png" alt-text="Screenshot of MobileTablet."::: | E8CC | :::no-loc text="MobileTablet"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8CD.png" alt-text="Screenshot of DisconnectDrive."::: | E8CD | :::no-loc text="DisconnectDrive"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8CE.png" alt-text="Screenshot of MapDrive."::: | E8CE | :::no-loc text="MapDrive"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8CF.png" alt-text="Screenshot of ContactPresence."::: | E8CF | :::no-loc text="ContactPresence"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8D0.png" alt-text="Screenshot of Priority."::: | E8D0 | :::no-loc text="Priority"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8D1.png" alt-text="Screenshot of GotoToday."::: | E8D1 | :::no-loc text="GotoToday"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8D2.png" alt-text="Screenshot of Font."::: | E8D2 | :::no-loc text="Font"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8D3.png" alt-text="Screenshot of FontColor."::: | E8D3 | :::no-loc text="FontColor"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8D4.png" alt-text="Screenshot of Contact2."::: | E8D4 | :::no-loc text="Contact2"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8D5.png" alt-text="Screenshot of FolderFill."::: | E8D5 | :::no-loc text="FolderFill"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8D6.png" alt-text="Screenshot of Audio."::: | E8D6 | :::no-loc text="Audio"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8D7.png" alt-text="Screenshot of Permissions."::: | E8D7 | :::no-loc text="Permissions"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8D8.png" alt-text="Screenshot of DisableUpdates."::: | E8D8 | :::no-loc text="DisableUpdates"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8D9.png" alt-text="Screenshot of Unfavorite."::: | E8D9 | :::no-loc text="Unfavorite"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8DA.png" alt-text="Screenshot of OpenLocal."::: | E8DA | :::no-loc text="OpenLocal"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8DB.png" alt-text="Screenshot of Italic."::: | E8DB | :::no-loc text="Italic"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8DC.png" alt-text="Screenshot of Underline."::: | E8DC | :::no-loc text="Underline"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8DD.png" alt-text="Screenshot of Bold."::: | E8DD | :::no-loc text="Bold"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8DE.png" alt-text="Screenshot of MoveToFolder."::: | E8DE | :::no-loc text="MoveToFolder"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8DF.png" alt-text="Screenshot of LikeDislike."::: | E8DF | :::no-loc text="LikeDislike"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8E0.png" alt-text="Screenshot of Dislike."::: | E8E0 | :::no-loc text="Dislike"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8E1.png" alt-text="Screenshot of Like."::: | E8E1 | :::no-loc text="Like"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8E2.png" alt-text="Screenshot of AlignRight."::: | E8E2 | :::no-loc text="AlignRight"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8E3.png" alt-text="Screenshot of AlignCenter."::: | E8E3 | :::no-loc text="AlignCenter"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8E4.png" alt-text="Screenshot of AlignLeft."::: | E8E4 | :::no-loc text="AlignLeft"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8E5.png" alt-text="Screenshot of OpenFile."::: | E8E5 | :::no-loc text="OpenFile"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8E6.png" alt-text="Screenshot of ClearSelection."::: | E8E6 | :::no-loc text="ClearSelection"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8E7.png" alt-text="Screenshot of FontDecrease."::: | E8E7 | :::no-loc text="FontDecrease"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8E8.png" alt-text="Screenshot of FontIncrease."::: | E8E8 | :::no-loc text="FontIncrease"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8E9.png" alt-text="Screenshot of FontSize."::: | E8E9 | :::no-loc text="FontSize"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8EA.png" alt-text="Screenshot of CellPhone."::: | E8EA | :::no-loc text="CellPhone"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8EB.png" alt-text="Screenshot of Reshare."::: | E8EB | :::no-loc text="Reshare"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8EC.png" alt-text="Screenshot of Tag."::: | E8EC | :::no-loc text="Tag"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8ED.png" alt-text="Screenshot of RepeatOne."::: | E8ED | :::no-loc text="RepeatOne"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8EE.png" alt-text="Screenshot of RepeatAll."::: | E8EE | :::no-loc text="RepeatAll"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8EF.png" alt-text="Screenshot of Calculator."::: | E8EF | :::no-loc text="Calculator"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8F0.png" alt-text="Screenshot of Directions."::: | E8F0 | :::no-loc text="Directions"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8F1.png" alt-text="Screenshot of Library."::: | E8F1 | :::no-loc text="Library"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8F2.png" alt-text="Screenshot of ChatBubbles."::: | E8F2 | :::no-loc text="ChatBubbles"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8F3.png" alt-text="Screenshot of PostUpdate."::: | E8F3 | :::no-loc text="PostUpdate"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8F4.png" alt-text="Screenshot of NewFolder."::: | E8F4 | :::no-loc text="NewFolder"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8F5.png" alt-text="Screenshot of CalendarReply."::: | E8F5 | :::no-loc text="CalendarReply"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8F6.png" alt-text="Screenshot of UnsyncFolder."::: | E8F6 | :::no-loc text="UnsyncFolder"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8F7.png" alt-text="Screenshot of SyncFolder."::: | E8F7 | :::no-loc text="SyncFolder"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8F8.png" alt-text="Screenshot of BlockContact."::: | E8F8 | :::no-loc text="BlockContact"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8F9.png" alt-text="Screenshot of SwitchApps."::: | E8F9 | :::no-loc text="SwitchApps"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8FA.png" alt-text="Screenshot of AddFriend."::: | E8FA | :::no-loc text="AddFriend"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8FB.png" alt-text="Screenshot of Accept."::: | E8FB | :::no-loc text="Accept"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8FC.png" alt-text="Screenshot of GoToStart."::: | E8FC | :::no-loc text="GoToStart"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8FD.png" alt-text="Screenshot of BulletedList."::: | E8FD | :::no-loc text="BulletedList"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8FE.png" alt-text="Screenshot of Scan."::: | E8FE | :::no-loc text="Scan"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E8FF.png" alt-text="Screenshot of Preview."::: | E8FF | :::no-loc text="Preview"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E902.png" alt-text="Screenshot of Group."::: | E902 | :::no-loc text="Group"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E904.png" alt-text="Screenshot of ZeroBars."::: | E904 | :::no-loc text="ZeroBars"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E905.png" alt-text="Screenshot of OneBar."::: | E905 | :::no-loc text="OneBar"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E906.png" alt-text="Screenshot of TwoBars."::: | E906 | :::no-loc text="TwoBars"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E907.png" alt-text="Screenshot of ThreeBars."::: | E907 | :::no-loc text="ThreeBars"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E908.png" alt-text="Screenshot of FourBars."::: | E908 | :::no-loc text="FourBars"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E909.png" alt-text="Screenshot of World."::: | E909 | :::no-loc text="World"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E90A.png" alt-text="Screenshot of Comment."::: | E90A | :::no-loc text="Comment"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E90B.png" alt-text="Screenshot of MusicInfo."::: | E90B | :::no-loc text="MusicInfo"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E90C.png" alt-text="Screenshot of DockLeft."::: | E90C | :::no-loc text="DockLeft"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E90D.png" alt-text="Screenshot of DockRight."::: | E90D | :::no-loc text="DockRight"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E90E.png" alt-text="Screenshot of DockBottom."::: | E90E | :::no-loc text="DockBottom"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E90F.png" alt-text="Screenshot of Repair."::: | E90F | :::no-loc text="Repair"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E910.png" alt-text="Screenshot of Accounts."::: | E910 | :::no-loc text="Accounts"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E911.png" alt-text="Screenshot of DullSound."::: | E911 | :::no-loc text="DullSound"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E912.png" alt-text="Screenshot of Manage."::: | E912 | :::no-loc text="Manage"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E913.png" alt-text="Screenshot of Street."::: | E913 | :::no-loc text="Street"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E914.png" alt-text="Screenshot of Printer3D."::: | E914 | :::no-loc text="Printer3D"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E915.png" alt-text="Screenshot of RadioBullet."::: | E915 | :::no-loc text="RadioBullet"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E916.png" alt-text="Screenshot of Stopwatch."::: | E916 | :::no-loc text="Stopwatch"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E91B.png" alt-text="Screenshot of Photo."::: | E91B | :::no-loc text="Photo"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E91C.png" alt-text="Screenshot of ActionCenter."::: | E91C | :::no-loc text="ActionCenter"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E91F.png" alt-text="Screenshot of FullCircleMask."::: | E91F | :::no-loc text="FullCircleMask"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E921.png" alt-text="Screenshot of ChromeMinimize."::: | E921 | :::no-loc text="ChromeMinimize"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E922.png" alt-text="Screenshot of ChromeMaximize."::: | E922 | :::no-loc text="ChromeMaximize"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E923.png" alt-text="Screenshot of ChromeRestore."::: | E923 | :::no-loc text="ChromeRestore"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E924.png" alt-text="Screenshot of Annotation."::: | E924 | :::no-loc text="Annotation"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E925.png" alt-text="Screenshot of BackSpaceQWERTYSm."::: | E925 | :::no-loc text="BackSpaceQWERTYSm"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E926.png" alt-text="Screenshot of BackSpaceQWERTYMd."::: | E926 | :::no-loc text="BackSpaceQWERTYMd"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E927.png" alt-text="Screenshot of Swipe."::: | E927 | :::no-loc text="Swipe"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E928.png" alt-text="Screenshot of Fingerprint."::: | E928 | :::no-loc text="Fingerprint"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E929.png" alt-text="Screenshot of Handwriting."::: | E929 | :::no-loc text="Handwriting"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E92C.png" alt-text="Screenshot of ChromeBackToWindow."::: | E92C | :::no-loc text="ChromeBackToWindow"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E92D.png" alt-text="Screenshot of ChromeFullScreen."::: | E92D | :::no-loc text="ChromeFullScreen"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E92E.png" alt-text="Screenshot of KeyboardStandard."::: | E92E | :::no-loc text="KeyboardStandard"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E92F.png" alt-text="Screenshot of KeyboardDismiss."::: | E92F | :::no-loc text="KeyboardDismiss"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E930.png" alt-text="Screenshot of Completed."::: | E930 | :::no-loc text="Completed"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E931.png" alt-text="Screenshot of ChromeAnnotate."::: | E931 | :::no-loc text="ChromeAnnotate"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E932.png" alt-text="Screenshot of Label."::: | E932 | :::no-loc text="Label"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E933.png" alt-text="Screenshot of IBeam."::: | E933 | :::no-loc text="IBeam"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E934.png" alt-text="Screenshot of IBeamOutline."::: | E934 | :::no-loc text="IBeamOutline"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E935.png" alt-text="Screenshot of FlickDown."::: | E935 | :::no-loc text="FlickDown"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E936.png" alt-text="Screenshot of FlickUp."::: | E936 | :::no-loc text="FlickUp"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E937.png" alt-text="Screenshot of FlickLeft."::: | E937 | :::no-loc text="FlickLeft"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E938.png" alt-text="Screenshot of FlickRight."::: | E938 | :::no-loc text="FlickRight"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E939.png" alt-text="Screenshot of FeedbackApp."::: | E939 | :::no-loc text="FeedbackApp"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E93C.png" alt-text="Screenshot of MusicAlbum."::: | E93C | :::no-loc text="MusicAlbum"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E93E.png" alt-text="Screenshot of Streaming."::: | E93E | :::no-loc text="Streaming"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E943.png" alt-text="Screenshot of Code."::: | E943 | :::no-loc text="Code"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E944.png" alt-text="Screenshot of ReturnToWindow."::: | E944 | :::no-loc text="ReturnToWindow"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E945.png" alt-text="Screenshot of LightningBolt."::: | E945 | :::no-loc text="LightningBolt"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E946.png" alt-text="Screenshot of Info."::: | E946 | :::no-loc text="Info"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E947.png" alt-text="Screenshot of CalculatorMultiply."::: | E947 | :::no-loc text="CalculatorMultiply"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E948.png" alt-text="Screenshot of CalculatorAddition."::: | E948 | :::no-loc text="CalculatorAddition"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E949.png" alt-text="Screenshot of CalculatorSubtract."::: | E949 | :::no-loc text="CalculatorSubtract"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E94A.png" alt-text="Screenshot of CalculatorDivide."::: | E94A | :::no-loc text="CalculatorDivide"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E94B.png" alt-text="Screenshot of CalculatorSquareroot."::: | E94B | :::no-loc text="CalculatorSquareroot"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E94C.png" alt-text="Screenshot of CalculatorPercentage."::: | E94C | :::no-loc text="CalculatorPercentage"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E94D.png" alt-text="Screenshot of CalculatorNegate."::: | E94D | :::no-loc text="CalculatorNegate"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E94E.png" alt-text="Screenshot of CalculatorEqualTo."::: | E94E | :::no-loc text="CalculatorEqualTo"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E94F.png" alt-text="Screenshot of CalculatorBackspace."::: | E94F | :::no-loc text="CalculatorBackspace"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E950.png" alt-text="Screenshot of Component."::: | E950 | :::no-loc text="Component"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E951.png" alt-text="Screenshot of DMC."::: | E951 | :::no-loc text="DMC"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E952.png" alt-text="Screenshot of Dock."::: | E952 | :::no-loc text="Dock"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E953.png" alt-text="Screenshot of MultimediaDMS."::: | E953 | :::no-loc text="MultimediaDMS"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E954.png" alt-text="Screenshot of MultimediaDVR."::: | E954 | :::no-loc text="MultimediaDVR"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E955.png" alt-text="Screenshot of MultimediaPMP."::: | E955 | :::no-loc text="MultimediaPMP"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E956.png" alt-text="Screenshot of PrintfaxPrinterFile."::: | E956 | :::no-loc text="PrintfaxPrinterFile"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E957.png" alt-text="Screenshot of Sensor."::: | E957 | :::no-loc text="Sensor"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E958.png" alt-text="Screenshot of StorageOptical."::: | E958 | :::no-loc text="StorageOptical"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E95A.png" alt-text="Screenshot of Communications."::: | E95A | :::no-loc text="Communications"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E95B.png" alt-text="Screenshot of Headset."::: | E95B | :::no-loc text="Headset"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E95D.png" alt-text="Screenshot of Projector."::: | E95D | :::no-loc text="Projector"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E95E.png" alt-text="Screenshot of Health."::: | E95E | :::no-loc text="Health"::: |
 | :::image type="content" border="false" source="images/segoe-mdl/E95F.png" alt-text="Screenshot of Wire."::: | E95F | :::no-loc text="Wire"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E960.png" alt-text="Screenshot of Webcam2."::: | E960 | :::no-loc text="Webcam2"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E961.png" alt-text="Screenshot of Input."::: | E961 | :::no-loc text="Input"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E962.png" alt-text="Screenshot of Mouse."::: | E962 | :::no-loc text="Mouse"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E963.png" alt-text="Screenshot of Smartcard."::: | E963 | :::no-loc text="Smartcard"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E964.png" alt-text="Screenshot of SmartcardVirtual."::: | E964 | :::no-loc text="SmartcardVirtual"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E965.png" alt-text="Screenshot of MediaStorageTower."::: | E965 | :::no-loc text="MediaStorageTower"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E966.png" alt-text="Screenshot of ReturnKeySm."::: | E966 | :::no-loc text="ReturnKeySm"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E967.png" alt-text="Screenshot of GameConsole."::: | E967 | :::no-loc text="GameConsole"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E968.png" alt-text="Screenshot of Network."::: | E968 | :::no-loc text="Network"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E969.png" alt-text="Screenshot of StorageNetworkWireless."::: | E969 | :::no-loc text="StorageNetworkWireless"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E96A.png" alt-text="Screenshot of StorageTape."::: | E96A | :::no-loc text="StorageTape"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E96D.png" alt-text="Screenshot of ChevronUpSmall."::: | E96D | :::no-loc text="ChevronUpSmall"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E96E.png" alt-text="Screenshot of ChevronDownSmall."::: | E96E | :::no-loc text="ChevronDownSmall"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E96F.png" alt-text="Screenshot of ChevronLeftSmall."::: | E96F | :::no-loc text="ChevronLeftSmall"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E970.png" alt-text="Screenshot of ChevronRightSmall."::: | E970 | :::no-loc text="ChevronRightSmall"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E971.png" alt-text="Screenshot of ChevronUpMed."::: | E971 | :::no-loc text="ChevronUpMed"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E972.png" alt-text="Screenshot of ChevronDownMed."::: | E972 | :::no-loc text="ChevronDownMed"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E973.png" alt-text="Screenshot of ChevronLeftMed."::: | E973 | :::no-loc text="ChevronLeftMed"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E974.png" alt-text="Screenshot of ChevronRightMed."::: | E974 | :::no-loc text="ChevronRightMed"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E975.png" alt-text="Screenshot of Devices2."::: | E975 | :::no-loc text="Devices2"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E976.png" alt-text="Screenshot of ExpandTile."::: | E976 | :::no-loc text="ExpandTile"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E977.png" alt-text="Screenshot of PC1."::: | E977 | :::no-loc text="PC1"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E978.png" alt-text="Screenshot of PresenceChicklet."::: | E978 | :::no-loc text="PresenceChicklet"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E979.png" alt-text="Screenshot of PresenceChickletVideo."::: | E979 | :::no-loc text="PresenceChickletVideo"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E97A.png" alt-text="Screenshot of Reply."::: | E97A | :::no-loc text="Reply"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E97B.png" alt-text="Screenshot of SetTile."::: | E97B | :::no-loc text="SetTile"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E97C.png" alt-text="Screenshot of Type."::: | E97C | :::no-loc text="Type"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E97D.png" alt-text="Screenshot of Korean."::: | E97D | :::no-loc text="Korean"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E97E.png" alt-text="Screenshot of HalfAlpha."::: | E97E | :::no-loc text="HalfAlpha"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E97F.png" alt-text="Screenshot of FullAlpha."::: | E97F | :::no-loc text="FullAlpha"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E980.png" alt-text="Screenshot of Key12On."::: | E980 | :::no-loc text="Key12On"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E981.png" alt-text="Screenshot of ChineseChangjie."::: | E981 | :::no-loc text="ChineseChangjie"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E982.png" alt-text="Screenshot of QWERTYOn."::: | E982 | :::no-loc text="QWERTYOn"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E983.png" alt-text="Screenshot of QWERTYOff."::: | E983 | :::no-loc text="QWERTYOff"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E984.png" alt-text="Screenshot of ChineseQuick."::: | E984 | :::no-loc text="ChineseQuick"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E985.png" alt-text="Screenshot of Japanese."::: | E985 | :::no-loc text="Japanese"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E986.png" alt-text="Screenshot of FullHiragana."::: | E986 | :::no-loc text="FullHiragana"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E987.png" alt-text="Screenshot of FullKatakana."::: | E987 | :::no-loc text="FullKatakana"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E988.png" alt-text="Screenshot of HalfKatakana."::: | E988 | :::no-loc text="HalfKatakana"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E989.png" alt-text="Screenshot of ChineseBoPoMoFo."::: | E989 | :::no-loc text="ChineseBoPoMoFo"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E98A.png" alt-text="Screenshot of ChinesePinyin."::: | E98A | :::no-loc text="ChinesePinyin"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E98F.png" alt-text="Screenshot of ConstructionCone."::: | E98F | :::no-loc text="ConstructionCone"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E990.png" alt-text="Screenshot of XboxOneConsole."::: | E990 | :::no-loc text="XboxOneConsole"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E992.png" alt-text="Screenshot of Volume0."::: | E992 | :::no-loc text="Volume0"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E993.png" alt-text="Screenshot of Volume1."::: | E993 | :::no-loc text="Volume1"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E994.png" alt-text="Screenshot of Volume2."::: | E994 | :::no-loc text="Volume2"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E995.png" alt-text="Screenshot of Volume3."::: | E995 | :::no-loc text="Volume3"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E996.png" alt-text="Screenshot of BatteryUnknown."::: | E996 | :::no-loc text="BatteryUnknown"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E998.png" alt-text="Screenshot of WifiAttentionOverlay."::: | E998 | :::no-loc text="WifiAttentionOverlay"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E99A.png" alt-text="Screenshot of Robot."::: | E99A | :::no-loc text="Robot"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E9A1.png" alt-text="Screenshot of TapAndSend."::: | E9A1 | :::no-loc text="TapAndSend"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E9A6.png" alt-text="Screenshot of FitPage."::: | E9A6 | :::no-loc text="FitPage"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E9A8.png" alt-text="Screenshot of PasswordKeyShow."::: | E9A8 | :::no-loc text="PasswordKeyShow"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E9A9.png" alt-text="Screenshot of PasswordKeyHide."::: | E9A9 | :::no-loc text="PasswordKeyHide"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E9AA.png" alt-text="Screenshot of BidiLtr."::: | E9AA | :::no-loc text="BidiLtr"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E9AB.png" alt-text="Screenshot of BidiRtl."::: | E9AB | :::no-loc text="BidiRtl"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E9AC.png" alt-text="Screenshot of ForwardSm."::: | E9AC | :::no-loc text="ForwardSm"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E9AD.png" alt-text="Screenshot of CommaKey."::: | E9AD | :::no-loc text="CommaKey"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E9AE.png" alt-text="Screenshot of DashKey."::: | E9AE | :::no-loc text="DashKey"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E9AF.png" alt-text="Screenshot of DullSoundKey."::: | E9AF | :::no-loc text="DullSoundKey"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E9B0.png" alt-text="Screenshot of HalfDullSound."::: | E9B0 | :::no-loc text="HalfDullSound"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E9B1.png" alt-text="Screenshot of RightDoubleQuote."::: | E9B1 | :::no-loc text="RightDoubleQuote"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E9B2.png" alt-text="Screenshot of LeftDoubleQuote."::: | E9B2 | :::no-loc text="LeftDoubleQuote"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E9B3.png" alt-text="Screenshot of PuncKeyRightBottom."::: | E9B3 | :::no-loc text="PuncKeyRightBottom"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E9B4.png" alt-text="Screenshot of PuncKey1."::: | E9B4 | :::no-loc text="PuncKey1"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E9B5.png" alt-text="Screenshot of PuncKey2."::: | E9B5 | :::no-loc text="PuncKey2"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E9B6.png" alt-text="Screenshot of PuncKey3."::: | E9B6 | :::no-loc text="PuncKey3"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E9B7.png" alt-text="Screenshot of PuncKey4."::: | E9B7 | :::no-loc text="PuncKey4"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E9B8.png" alt-text="Screenshot of PuncKey5."::: | E9B8 | :::no-loc text="PuncKey5"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E9B9.png" alt-text="Screenshot of PuncKey6."::: | E9B9 | :::no-loc text="PuncKey6"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E9BA.png" alt-text="Screenshot of PuncKey9."::: | E9BA | :::no-loc text="PuncKey9"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E9BB.png" alt-text="Screenshot of PuncKey7."::: | E9BB | :::no-loc text="PuncKey7"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E9BC.png" alt-text="Screenshot of PuncKey8."::: | E9BC | :::no-loc text="PuncKey8"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E9CA.png" alt-text="Screenshot of Frigid."::: | E9CA | :::no-loc text="Frigid"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E9CE.png" alt-text="Screenshot of Unknown."::: | E9CE | :::no-loc text="Unknown"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E9D2.png" alt-text="Screenshot of AreaChart."::: | E9D2 | :::no-loc text="AreaChart"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E9D5.png" alt-text="Screenshot of CheckList."::: | E9D5 | :::no-loc text="CheckList"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E9D9.png" alt-text="Screenshot of Diagnostic."::: | E9D9 | :::no-loc text="Diagnostic"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E9E9.png" alt-text="Screenshot of Equalizer."::: | E9E9 | :::no-loc text="Equalizer"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E9F3.png" alt-text="Screenshot of Process."::: | E9F3 | :::no-loc text="Process"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E9F5.png" alt-text="Screenshot of Processing."::: | E9F5 | :::no-loc text="Processing"::: |
| :::image type="content" border="false" source="images/segoe-mdl/E9F9.png" alt-text="Screenshot of ReportDocument."::: | E9F9 | :::no-loc text="ReportDocument"::: |

### PUA EA00-EC00

The following table of glyphs displays unicode points prefixed from EA-  to EC-.

[Back to top](#icon-list)

| Glyph | Unicode point | Description |
|-------|---------------|-------------|
| :::image type="content" border="false" source="images/segoe-mdl/EA0C.png" alt-text="Screenshot of VideoSolid."::: | EA0C | :::no-loc text="VideoSolid"::: |
 | :::image type="content" border="false" source="images/segoe-mdl/EA0D.png" alt-text="Screenshot of MixedMediaBadge."::: | EA0D | :::no-loc text="MixedMediaBadge"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA14.png" alt-text="Screenshot of DisconnectDisplay."::: | EA14 | :::no-loc text="DisconnectDisplay"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA18.png" alt-text="Screenshot of Shield."::: | EA18 | :::no-loc text="Shield"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA1F.png" alt-text="Screenshot of Info2."::: | EA1F | :::no-loc text="Info2"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA21.png" alt-text="Screenshot of ActionCenterAsterisk."::: | EA21 | :::no-loc text="ActionCenterAsterisk"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA24.png" alt-text="Screenshot of Beta."::: | EA24 | :::no-loc text="Beta"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA35.png" alt-text="Screenshot of SaveCopy."::: | EA35 | :::no-loc text="SaveCopy"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA37.png" alt-text="Screenshot of List."::: | EA37 | :::no-loc text="List"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA38.png" alt-text="Screenshot of Asterisk."::: | EA38 | :::no-loc text="Asterisk"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA39.png" alt-text="Screenshot of ErrorBadge."::: | EA39 | :::no-loc text="ErrorBadge"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA3A.png" alt-text="Screenshot of CircleRing."::: | EA3A | :::no-loc text="CircleRing"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA3B.png" alt-text="Screenshot of CircleFill."::: | EA3B | :::no-loc text="CircleFill"::: |
 | :::image type="content" border="false" source="images/segoe-mdl/EA3C.png" alt-text="Screenshot of MergeCall."::: | EA3C | :::no-loc text="MergeCall"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA3D.png" alt-text="Screenshot of PrivateCall."::: | EA3D | :::no-loc text="PrivateCall"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA3F.png" alt-text="Screenshot of Record2."::: | EA3F | :::no-loc text="Record2"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA40.png" alt-text="Screenshot of AllAppsMirrored."::: | EA40 | :::no-loc text="AllAppsMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA41.png" alt-text="Screenshot of BookmarksMirrored."::: | EA41 | :::no-loc text="BookmarksMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA42.png" alt-text="Screenshot of BulletedListMirrored."::: | EA42 | :::no-loc text="BulletedListMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA43.png" alt-text="Screenshot of CallForwardInternationalMirrored."::: | EA43 | :::no-loc text="CallForwardInternationalMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA44.png" alt-text="Screenshot of CallForwardRoamingMirrored."::: | EA44 | :::no-loc text="CallForwardRoamingMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA47.png" alt-text="Screenshot of ChromeBackMirrored."::: | EA47 | :::no-loc text="ChromeBackMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA48.png" alt-text="Screenshot of ClearSelectionMirrored."::: | EA48 | :::no-loc text="ClearSelectionMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA49.png" alt-text="Screenshot of ClosePaneMirrored."::: | EA49 | :::no-loc text="ClosePaneMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA4A.png" alt-text="Screenshot of ContactInfoMirrored."::: | EA4A | :::no-loc text="ContactInfoMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA4B.png" alt-text="Screenshot of DockRightMirrored."::: | EA4B | :::no-loc text="DockRightMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA4C.png" alt-text="Screenshot of DockLeftMirrored."::: | EA4C | :::no-loc text="DockLeftMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA4E.png" alt-text="Screenshot of ExpandTileMirrored."::: | EA4E | :::no-loc text="ExpandTileMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA4F.png" alt-text="Screenshot of GoMirrored."::: | EA4F | :::no-loc text="GoMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA50.png" alt-text="Screenshot of GripperResizeMirrored."::: | EA50 | :::no-loc text="GripperResizeMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA51.png" alt-text="Screenshot of HelpMirrored."::: | EA51 | :::no-loc text="HelpMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA52.png" alt-text="Screenshot of ImportMirrored."::: | EA52 | :::no-loc text="ImportMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA53.png" alt-text="Screenshot of ImportAllMirrored."::: | EA53 | :::no-loc text="ImportAllMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA54.png" alt-text="Screenshot of LeaveChatMirrored."::: | EA54 | :::no-loc text="LeaveChatMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA55.png" alt-text="Screenshot of ListMirrored."::: | EA55 | :::no-loc text="ListMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA56.png" alt-text="Screenshot of MailForwardMirrored."::: | EA56 | :::no-loc text="MailForwardMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA57.png" alt-text="Screenshot of MailReplyMirrored."::: | EA57 | :::no-loc text="MailReplyMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA58.png" alt-text="Screenshot of MailReplyAllMirrored."::: | EA58 | :::no-loc text="MailReplyAllMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA5B.png" alt-text="Screenshot of OpenPaneMirrored."::: | EA5B | :::no-loc text="OpenPaneMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA5C.png" alt-text="Screenshot of OpenWithMirrored."::: | EA5C | :::no-loc text="OpenWithMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA5E.png" alt-text="Screenshot of ParkingLocationMirrored."::: | EA5E | :::no-loc text="ParkingLocationMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA5F.png" alt-text="Screenshot of ResizeMouseMediumMirrored."::: | EA5F | :::no-loc text="ResizeMouseMediumMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA60.png" alt-text="Screenshot of ResizeMouseSmallMirrored."::: | EA60 | :::no-loc text="ResizeMouseSmallMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA61.png" alt-text="Screenshot of ResizeMouseTallMirrored."::: | EA61 | :::no-loc text="ResizeMouseTallMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA62.png" alt-text="Screenshot of ResizeTouchNarrowerMirrored."::: | EA62 | :::no-loc text="ResizeTouchNarrowerMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA63.png" alt-text="Screenshot of SendMirrored."::: | EA63 | :::no-loc text="SendMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA64.png" alt-text="Screenshot of SendFillMirrored."::: | EA64 | :::no-loc text="SendFillMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA65.png" alt-text="Screenshot of ShowResultsMirrored."::: | EA65 | :::no-loc text="ShowResultsMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA69.png" alt-text="Screenshot of Media."::: | EA69 | :::no-loc text="Media"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA6A.png" alt-text="Screenshot of SyncError."::: | EA6A | :::no-loc text="SyncError"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA6C.png" alt-text="Screenshot of Devices3."::: | EA6C | :::no-loc text="Devices3"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA79.png" alt-text="Screenshot of SlowMotionOn."::: | EA79 | :::no-loc text="SlowMotionOn"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA80.png" alt-text="Screenshot of Lightbulb."::: | EA80 | :::no-loc text="Lightbulb"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA81.png" alt-text="Screenshot of StatusCircle."::: | EA81 | :::no-loc text="StatusCircle"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA82.png" alt-text="Screenshot of StatusTriangle."::: | EA82 | :::no-loc text="StatusTriangle"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA83.png" alt-text="Screenshot of StatusError."::: | EA83 | :::no-loc text="StatusError"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA84.png" alt-text="Screenshot of StatusWarning."::: | EA84 | :::no-loc text="StatusWarning"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA86.png" alt-text="Screenshot of Puzzle."::: | EA86 | :::no-loc text="Puzzle"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA89.png" alt-text="Screenshot of CalendarSolid."::: | EA89 | :::no-loc text="CalendarSolid"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA8A.png" alt-text="Screenshot of HomeSolid."::: | EA8A | :::no-loc text="HomeSolid"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA8B.png" alt-text="Screenshot of ParkingLocationSolid."::: | EA8B | :::no-loc text="ParkingLocationSolid"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA8C.png" alt-text="Screenshot of ContactSolid."::: | EA8C | :::no-loc text="ContactSolid"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA8D.png" alt-text="Screenshot of ConstructionSolid."::: | EA8D | :::no-loc text="ConstructionSolid"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA8E.png" alt-text="Screenshot of AccidentSolid."::: | EA8E | :::no-loc text="AccidentSolid"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA8F.png" alt-text="Screenshot of Ringer."::: | EA8F | :::no-loc text="Ringer"::: |
 | :::image type="content" border="false" source="images/segoe-mdl/EA90.png" alt-text="Screenshot of PDF."::: | EA90 | :::no-loc text="PDF"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA91.png" alt-text="Screenshot of ThoughtBubble."::: | EA91 | :::no-loc text="ThoughtBubble"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA92.png" alt-text="Screenshot of HeartBroken."::: | EA92 | :::no-loc text="HeartBroken"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA93.png" alt-text="Screenshot of BatteryCharging10."::: | EA93 | :::no-loc text="BatteryCharging10"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA94.png" alt-text="Screenshot of BatterySaver9."::: | EA94 | :::no-loc text="BatterySaver9"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA95.png" alt-text="Screenshot of BatterySaver10."::: | EA95 | :::no-loc text="BatterySaver10"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA97.png" alt-text="Screenshot of CallForwardingMirrored."::: | EA97 | :::no-loc text="CallForwardingMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA98.png" alt-text="Screenshot of MultiSelectMirrored."::: | EA98 | :::no-loc text="MultiSelectMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EA99.png" alt-text="Screenshot of Broom."::: | EA99 | :::no-loc text="Broom"::: |
 | :::image type="content" border="false" source="images/segoe-mdl/EAC2.png" alt-text="Screenshot of ForwardCall."::: | EAC2 | :::no-loc text="ForwardCall"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EADF.png" alt-text="Screenshot of Trackers."::: | EADF | :::no-loc text="Trackers"::: |
 | :::image type="content" border="false" source="images/segoe-mdl/EAFC.png" alt-text="Screenshot of Market."::: | EAFC | :::no-loc text="Market"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EB05.png" alt-text="Screenshot of PieSingle."::: | EB05 | :::no-loc text="PieSingle"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EB0F.png" alt-text="Screenshot of StockDown."::: | EB0F | :::no-loc text="StockDown"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EB11.png" alt-text="Screenshot of StockUp."::: | EB11 | :::no-loc text="StockUp"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EB3C.png" alt-text="Screenshot of Design."::: | EB3C | :::no-loc text="Design"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EB41.png" alt-text="Screenshot of Website."::: | EB41 | :::no-loc text="Website"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EB42.png" alt-text="Screenshot of Drop."::: | EB42 | :::no-loc text="Drop"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EB44.png" alt-text="Screenshot of Radar."::: | EB44 | :::no-loc text="Radar"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EB47.png" alt-text="Screenshot of BusSolid."::: | EB47 | :::no-loc text="BusSolid"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EB48.png" alt-text="Screenshot of FerrySolid."::: | EB48 | :::no-loc text="FerrySolid"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EB49.png" alt-text="Screenshot of StartPointSolid."::: | EB49 | :::no-loc text="StartPointSolid"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EB4A.png" alt-text="Screenshot of StopPointSolid."::: | EB4A | :::no-loc text="StopPointSolid"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EB4B.png" alt-text="Screenshot of EndPointSolid."::: | EB4B | :::no-loc text="EndPointSolid"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EB4C.png" alt-text="Screenshot of AirplaneSolid."::: | EB4C | :::no-loc text="AirplaneSolid"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EB4D.png" alt-text="Screenshot of TrainSolid."::: | EB4D | :::no-loc text="TrainSolid"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EB4E.png" alt-text="Screenshot of WorkSolid."::: | EB4E | :::no-loc text="WorkSolid"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EB4F.png" alt-text="Screenshot of ReminderFill."::: | EB4F | :::no-loc text="ReminderFill"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EB50.png" alt-text="Screenshot of Reminder."::: | EB50 | :::no-loc text="Reminder"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EB51.png" alt-text="Screenshot of Heart."::: | EB51 | :::no-loc text="Heart"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EB52.png" alt-text="Screenshot of HeartFill."::: | EB52 | :::no-loc text="HeartFill"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EB55.png" alt-text="Screenshot of EthernetError."::: | EB55 | :::no-loc text="EthernetError"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EB56.png" alt-text="Screenshot of EthernetWarning."::: | EB56 | :::no-loc text="EthernetWarning"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EB57.png" alt-text="Screenshot of StatusConnecting1."::: | EB57 | :::no-loc text="StatusConnecting1"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EB58.png" alt-text="Screenshot of StatusConnecting2."::: | EB58 | :::no-loc text="StatusConnecting2"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EB59.png" alt-text="Screenshot of StatusUnsecure."::: | EB59 | :::no-loc text="StatusUnsecure"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EB5A.png" alt-text="Screenshot of WifiError0."::: | EB5A | :::no-loc text="WifiError0"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EB5B.png" alt-text="Screenshot of WifiError1."::: | EB5B | :::no-loc text="WifiError1"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EB5C.png" alt-text="Screenshot of WifiError2."::: | EB5C | :::no-loc text="WifiError2"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EB5D.png" alt-text="Screenshot of WifiError3."::: | EB5D | :::no-loc text="WifiError3"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EB5E.png" alt-text="Screenshot of WifiError4."::: | EB5E | :::no-loc text="WifiError4"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EB5F.png" alt-text="Screenshot of WifiWarning0."::: | EB5F | :::no-loc text="WifiWarning0"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EB60.png" alt-text="Screenshot of WifiWarning1."::: | EB60 | :::no-loc text="WifiWarning1"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EB61.png" alt-text="Screenshot of WifiWarning2."::: | EB61 | :::no-loc text="WifiWarning2"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EB62.png" alt-text="Screenshot of WifiWarning3."::: | EB62 | :::no-loc text="WifiWarning3"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EB63.png" alt-text="Screenshot of WifiWarning4."::: | EB63 | :::no-loc text="WifiWarning4"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EB66.png" alt-text="Screenshot of Devices4."::: | EB66 | :::no-loc text="Devices4"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EB67.png" alt-text="Screenshot of NUIIris."::: | EB67 | :::no-loc text="NUIIris"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EB68.png" alt-text="Screenshot of NUIFace."::: | EB68 | :::no-loc text="NUIFace"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EB7E.png" alt-text="Screenshot of EditMirrored."::: | EB7E | :::no-loc text="EditMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EB82.png" alt-text="Screenshot of NUIFPStartSlideHand ."::: | EB82 | :::no-loc text="NUIFPStartSlideHand "::: |
| :::image type="content" border="false" source="images/segoe-mdl/EB83.png" alt-text="Screenshot of NUIFPStartSlideAction ."::: | EB83 | :::no-loc text="NUIFPStartSlideAction "::: |
| :::image type="content" border="false" source="images/segoe-mdl/EB84.png" alt-text="Screenshot of NUIFPContinueSlideHand ."::: | EB84 | :::no-loc text="NUIFPContinueSlideHand "::: |
| :::image type="content" border="false" source="images/segoe-mdl/EB85.png" alt-text="Screenshot of NUIFPContinueSlideAction."::: | EB85 | :::no-loc text="NUIFPContinueSlideAction"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EB86.png" alt-text="Screenshot of NUIFPRollRightHand ."::: | EB86 | :::no-loc text="NUIFPRollRightHand "::: |
| :::image type="content" border="false" source="images/segoe-mdl/EB87.png" alt-text="Screenshot of NUIFPRollRightHandAction."::: | EB87 | :::no-loc text="NUIFPRollRightHandAction"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EB88.png" alt-text="Screenshot of NUIFPRollLeftHand ."::: | EB88 | :::no-loc text="NUIFPRollLeftHand "::: |
| :::image type="content" border="false" source="images/segoe-mdl/EB89.png" alt-text="Screenshot of NUIFPRollLeftAction."::: | EB89 | :::no-loc text="NUIFPRollLeftAction"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EB8A.png" alt-text="Screenshot of NUIFPPressHand ."::: | EB8A | :::no-loc text="NUIFPPressHand "::: |
| :::image type="content" border="false" source="images/segoe-mdl/EB8B.png" alt-text="Screenshot of NUIFPPressAction."::: | EB8B | :::no-loc text="NUIFPPressAction"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EB8C.png" alt-text="Screenshot of NUIFPPressRepeatHand ."::: | EB8C | :::no-loc text="NUIFPPressRepeatHand "::: |
| :::image type="content" border="false" source="images/segoe-mdl/EB8D.png" alt-text="Screenshot of NUIFPPressRepeatAction."::: | EB8D | :::no-loc text="NUIFPPressRepeatAction"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EB90.png" alt-text="Screenshot of StatusErrorFull."::: | EB90 | :::no-loc text="StatusErrorFull"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EB91.png" alt-text="Screenshot of TaskViewExpanded."::: | EB91 | :::no-loc text="TaskViewExpanded"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EB95.png" alt-text="Screenshot of Certificate."::: | EB95 | :::no-loc text="Certificate"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EB96.png" alt-text="Screenshot of BackSpaceQWERTYLg."::: | EB96 | :::no-loc text="BackSpaceQWERTYLg"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EB97.png" alt-text="Screenshot of ReturnKeyLg."::: | EB97 | :::no-loc text="ReturnKeyLg"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EB9D.png" alt-text="Screenshot of FastForward."::: | EB9D | :::no-loc text="FastForward"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EB9E.png" alt-text="Screenshot of Rewind."::: | EB9E | :::no-loc text="Rewind"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EB9F.png" alt-text="Screenshot of Photo2."::: | EB9F | :::no-loc text="Photo2"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EBA0.png" alt-text="Screenshot of  MobBattery0."::: | EBA0 | :::no-loc text=" MobBattery0"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EBA1.png" alt-text="Screenshot of  MobBattery1."::: | EBA1 | :::no-loc text=" MobBattery1"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EBA2.png" alt-text="Screenshot of  MobBattery2."::: | EBA2 | :::no-loc text=" MobBattery2"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EBA3.png" alt-text="Screenshot of  MobBattery3."::: | EBA3 | :::no-loc text=" MobBattery3"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EBA4.png" alt-text="Screenshot of  MobBattery4."::: | EBA4 | :::no-loc text=" MobBattery4"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EBA5.png" alt-text="Screenshot of  MobBattery5."::: | EBA5 | :::no-loc text=" MobBattery5"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EBA6.png" alt-text="Screenshot of  MobBattery6."::: | EBA6 | :::no-loc text=" MobBattery6"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EBA7.png" alt-text="Screenshot of  MobBattery7."::: | EBA7 | :::no-loc text=" MobBattery7"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EBA8.png" alt-text="Screenshot of  MobBattery8."::: | EBA8 | :::no-loc text=" MobBattery8"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EBA9.png" alt-text="Screenshot of  MobBattery9."::: | EBA9 | :::no-loc text=" MobBattery9"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EBAA.png" alt-text="Screenshot of MobBattery10."::: | EBAA | :::no-loc text="MobBattery10"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EBAB.png" alt-text="Screenshot of  MobBatteryCharging0."::: | EBAB | :::no-loc text=" MobBatteryCharging0"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EBAC.png" alt-text="Screenshot of  MobBatteryCharging1."::: | EBAC | :::no-loc text=" MobBatteryCharging1"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EBAD.png" alt-text="Screenshot of  MobBatteryCharging2."::: | EBAD | :::no-loc text=" MobBatteryCharging2"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EBAE.png" alt-text="Screenshot of  MobBatteryCharging3."::: | EBAE | :::no-loc text=" MobBatteryCharging3"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EBAF.png" alt-text="Screenshot of  MobBatteryCharging4."::: | EBAF | :::no-loc text=" MobBatteryCharging4"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EBB0.png" alt-text="Screenshot of  MobBatteryCharging5."::: | EBB0 | :::no-loc text=" MobBatteryCharging5"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EBB1.png" alt-text="Screenshot of  MobBatteryCharging6."::: | EBB1 | :::no-loc text=" MobBatteryCharging6"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EBB2.png" alt-text="Screenshot of  MobBatteryCharging7."::: | EBB2 | :::no-loc text=" MobBatteryCharging7"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EBB3.png" alt-text="Screenshot of  MobBatteryCharging8."::: | EBB3 | :::no-loc text=" MobBatteryCharging8"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EBB4.png" alt-text="Screenshot of  MobBatteryCharging9."::: | EBB4 | :::no-loc text=" MobBatteryCharging9"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EBB5.png" alt-text="Screenshot of  MobBatteryCharging10."::: | EBB5 | :::no-loc text=" MobBatteryCharging10"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EBB6.png" alt-text="Screenshot of  MobBatterySaver0."::: | EBB6 | :::no-loc text=" MobBatterySaver0"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EBB7.png" alt-text="Screenshot of  MobBatterySaver1."::: | EBB7 | :::no-loc text=" MobBatterySaver1"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EBB8.png" alt-text="Screenshot of  MobBatterySaver2."::: | EBB8 | :::no-loc text=" MobBatterySaver2"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EBB9.png" alt-text="Screenshot of  MobBatterySaver3."::: | EBB9 | :::no-loc text=" MobBatterySaver3"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EBBA.png" alt-text="Screenshot of  MobBatterySaver4."::: | EBBA | :::no-loc text=" MobBatterySaver4"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EBBB.png" alt-text="Screenshot of  MobBatterySaver5."::: | EBBB | :::no-loc text=" MobBatterySaver5"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EBBC.png" alt-text="Screenshot of  MobBatterySaver6."::: | EBBC | :::no-loc text=" MobBatterySaver6"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EBBD.png" alt-text="Screenshot of  MobBatterySaver7."::: | EBBD | :::no-loc text=" MobBatterySaver7"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EBBE.png" alt-text="Screenshot of  MobBatterySaver8."::: | EBBE | :::no-loc text=" MobBatterySaver8"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EBBF.png" alt-text="Screenshot of  MobBatterySaver9."::: | EBBF | :::no-loc text=" MobBatterySaver9"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EBC0.png" alt-text="Screenshot of  MobBatterySaver10."::: | EBC0 | :::no-loc text=" MobBatterySaver10"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EBC3.png" alt-text="Screenshot of DictionaryCloud."::: | EBC3 | :::no-loc text="DictionaryCloud"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EBC4.png" alt-text="Screenshot of ResetDrive."::: | EBC4 | :::no-loc text="ResetDrive"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EBC5.png" alt-text="Screenshot of VolumeBars."::: | EBC5 | :::no-loc text="VolumeBars"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EBC6.png" alt-text="Screenshot of Project."::: | EBC6 | :::no-loc text="Project"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EBD2.png" alt-text="Screenshot of AdjustHologram."::: | EBD2 | :::no-loc text="AdjustHologram"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EBD4.png" alt-text="Screenshot of EBD4 WifiCallBars."::: | EBD4 | :::no-loc text="WifiCallBars"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EBD5.png" alt-text="Screenshot of EBD5 WifiCall0."::: | EBD5 | :::no-loc text="WifiCall0"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EBD6.png" alt-text="Screenshot of EBD6 WifiCall1."::: | EBD6 | :::no-loc text="WifiCall1"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EBD7.png" alt-text="Screenshot of EBD7 WifiCall2."::: | EBD7 | :::no-loc text="WifiCall2"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EBD8.png" alt-text="Screenshot of EBD8 WifiCall3."::: | EBD8 | :::no-loc text="WifiCall3"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EBD9.png" alt-text="Screenshot of EBD9 WifiCall4."::: | EBD9 | :::no-loc text="WifiCall4"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EBDA.png" alt-text="Screenshot of Family."::: | EBDA | :::no-loc text="Family"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EBDB.png" alt-text="Screenshot of LockFeedback."::: | EBDB | :::no-loc text="LockFeedback"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EBDE.png" alt-text="Screenshot of DeviceDiscovery."::: | EBDE | :::no-loc text="DeviceDiscovery"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EBE6.png" alt-text="Screenshot of WindDirection."::: | EBE6 | :::no-loc text="WindDirection"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EBE7.png" alt-text="Screenshot of RightArrowKeyTime0."::: | EBE7 | :::no-loc text="RightArrowKeyTime0"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EBE8.png" alt-text="Screenshot of Bug."::: | EBE8 | :::no-loc text="Bug"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EBFC.png" alt-text="Screenshot of TabletMode."::: | EBFC | :::no-loc text="TabletMode"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EBFD.png" alt-text="Screenshot of StatusCircleLeft."::: | EBFD | :::no-loc text="StatusCircleLeft"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EBFE.png" alt-text="Screenshot of StatusTriangleLeft."::: | EBFE | :::no-loc text="StatusTriangleLeft"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EBFF.png" alt-text="Screenshot of StatusErrorLeft."::: | EBFF | :::no-loc text="StatusErrorLeft"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC00.png" alt-text="Screenshot of StatusWarningLeft."::: | EC00 | :::no-loc text="StatusWarningLeft"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC02.png" alt-text="Screenshot of MobBatteryUnknown."::: | EC02 | :::no-loc text="MobBatteryUnknown"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC05.png" alt-text="Screenshot of NetworkTower."::: | EC05 | :::no-loc text="NetworkTower"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC06.png" alt-text="Screenshot of CityNext."::: | EC06 | :::no-loc text="CityNext"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC07.png" alt-text="Screenshot of CityNext2."::: | EC07 | :::no-loc text="CityNext2"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC08.png" alt-text="Screenshot of Courthouse."::: | EC08 | :::no-loc text="Courthouse"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC09.png" alt-text="Screenshot of Groceries."::: | EC09 | :::no-loc text="Groceries"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC0A.png" alt-text="Screenshot of Sustainable."::: | EC0A | :::no-loc text="Sustainable"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC0B.png" alt-text="Screenshot of BuildingEnergy."::: | EC0B | :::no-loc text="BuildingEnergy"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC11.png" alt-text="Screenshot of ToggleFilled."::: | EC11 | :::no-loc text="ToggleFilled"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC12.png" alt-text="Screenshot of ToggleBorder."::: | EC12 | :::no-loc text="ToggleBorder"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC13.png" alt-text="Screenshot of SliderThumb."::: | EC13 | :::no-loc text="SliderThumb"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC14.png" alt-text="Screenshot of ToggleThumb."::: | EC14 | :::no-loc text="ToggleThumb"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC15.png" alt-text="Screenshot of MiracastLogoSmall."::: | EC15 | :::no-loc text="MiracastLogoSmall"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC16.png" alt-text="Screenshot of MiracastLogoLarge."::: | EC16 | :::no-loc text="MiracastLogoLarge"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC19.png" alt-text="Screenshot of PLAP."::: | EC19 | :::no-loc text="PLAP"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC1B.png" alt-text="Screenshot of Badge."::: | EC1B | :::no-loc text="Badge"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC1E.png" alt-text="Screenshot of SignalRoaming."::: | EC1E | :::no-loc text="SignalRoaming"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC20.png" alt-text="Screenshot of MobileLocked."::: | EC20 | :::no-loc text="MobileLocked"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC24.png" alt-text="Screenshot of InsiderHubApp."::: | EC24 | :::no-loc text="InsiderHubApp"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC25.png" alt-text="Screenshot of PersonalFolder."::: | EC25 | :::no-loc text="PersonalFolder"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC26.png" alt-text="Screenshot of HomeGroup."::: | EC26 | :::no-loc text="HomeGroup"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC27.png" alt-text="Screenshot of MyNetwork."::: | EC27 | :::no-loc text="MyNetwork"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC31.png" alt-text="Screenshot of KeyboardFull."::: | EC31 | :::no-loc text="KeyboardFull"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC32.png" alt-text="Screenshot of Cafe."::: | EC32 | :::no-loc text="Cafe"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC37.png" alt-text="Screenshot of MobSignal1."::: | EC37 | :::no-loc text="MobSignal1"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC38.png" alt-text="Screenshot of MobSignal2."::: | EC38 | :::no-loc text="MobSignal2"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC39.png" alt-text="Screenshot of MobSignal3."::: | EC39 | :::no-loc text="MobSignal3"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC3A.png" alt-text="Screenshot of MobSignal4."::: | EC3A | :::no-loc text="MobSignal4"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC3B.png" alt-text="Screenshot of MobSignal5."::: | EC3B | :::no-loc text="MobSignal5"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC3C.png" alt-text="Screenshot of MobWifi1."::: | EC3C | :::no-loc text="MobWifi1"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC3D.png" alt-text="Screenshot of MobWifi2."::: | EC3D | :::no-loc text="MobWifi2"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC3E.png" alt-text="Screenshot of MobWifi3."::: | EC3E | :::no-loc text="MobWifi3"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC3F.png" alt-text="Screenshot of MobWifi4."::: | EC3F | :::no-loc text="MobWifi4"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC40.png" alt-text="Screenshot of MobAirplane."::: | EC40 | :::no-loc text="MobAirplane"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC41.png" alt-text="Screenshot of MobBluetooth."::: | EC41 | :::no-loc text="MobBluetooth"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC42.png" alt-text="Screenshot of MobActionCenter."::: | EC42 | :::no-loc text="MobActionCenter"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC43.png" alt-text="Screenshot of MobLocation."::: | EC43 | :::no-loc text="MobLocation"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC44.png" alt-text="Screenshot of MobWifiHotspot."::: | EC44 | :::no-loc text="MobWifiHotspot"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC45.png" alt-text="Screenshot of LanguageJpn."::: | EC45 | :::no-loc text="LanguageJpn"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC46.png" alt-text="Screenshot of MobQuietHours."::: | EC46 | :::no-loc text="MobQuietHours"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC47.png" alt-text="Screenshot of MobDrivingMode."::: | EC47 | :::no-loc text="MobDrivingMode"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC48.png" alt-text="Screenshot of SpeedOff."::: | EC48 | :::no-loc text="SpeedOff"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC49.png" alt-text="Screenshot of SpeedMedium."::: | EC49 | :::no-loc text="SpeedMedium"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC4A.png" alt-text="Screenshot of SpeedHigh."::: | EC4A | :::no-loc text="SpeedHigh"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC4E.png" alt-text="Screenshot of ThisPC."::: | EC4E | :::no-loc text="ThisPC"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC4F.png" alt-text="Screenshot of MusicNote."::: | EC4F | :::no-loc text="MusicNote"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC50.png" alt-text="Screenshot of FileExplorer."::: | EC50 | :::no-loc text="FileExplorer"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC51.png" alt-text="Screenshot of FileExplorerApp."::: | EC51 | :::no-loc text="FileExplorerApp"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC52.png" alt-text="Screenshot of LeftArrowKeyTime0."::: | EC52 | :::no-loc text="LeftArrowKeyTime0"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC54.png" alt-text="Screenshot of MicOff."::: | EC54 | :::no-loc text="MicOff"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC55.png" alt-text="Screenshot of MicSleep."::: | EC55 | :::no-loc text="MicSleep"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC56.png" alt-text="Screenshot of MicError."::: | EC56 | :::no-loc text="MicError"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC57.png" alt-text="Screenshot of PlaybackRate1x."::: | EC57 | :::no-loc text="PlaybackRate1x"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC58.png" alt-text="Screenshot of PlaybackRateOther."::: | EC58 | :::no-loc text="PlaybackRateOther"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC59.png" alt-text="Screenshot of CashDrawer."::: | EC59 | :::no-loc text="CashDrawer"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC5A.png" alt-text="Screenshot of BarcodeScanner."::: | EC5A | :::no-loc text="BarcodeScanner"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC5B.png" alt-text="Screenshot of ReceiptPrinter."::: | EC5B | :::no-loc text="ReceiptPrinter"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC5C.png" alt-text="Screenshot of MagStripeReader."::: | EC5C | :::no-loc text="MagStripeReader"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC61.png" alt-text="Screenshot of CompletedSolid."::: | EC61 | :::no-loc text="CompletedSolid"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC64.png" alt-text="Screenshot of CompanionApp."::: | EC64 | :::no-loc text="CompanionApp"::: |
 | :::image type="content" border="false" source="images/segoe-mdl/EC6C.png" alt-text="Screenshot of Favicon2."::: | EC6C | :::no-loc text="Favicon2"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC6D.png" alt-text="Screenshot of SwipeRevealArt."::: | EC6D | :::no-loc text="SwipeRevealArt"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC71.png" alt-text="Screenshot of MicOn."::: | EC71 | :::no-loc text="MicOn"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC72.png" alt-text="Screenshot of MicClipping."::: | EC72 | :::no-loc text="MicClipping"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC74.png" alt-text="Screenshot of TabletSelected."::: | EC74 | :::no-loc text="TabletSelected"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC75.png" alt-text="Screenshot of MobileSelected."::: | EC75 | :::no-loc text="MobileSelected"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC76.png" alt-text="Screenshot of LaptopSelected."::: | EC76 | :::no-loc text="LaptopSelected"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC77.png" alt-text="Screenshot of TVMonitorSelected."::: | EC77 | :::no-loc text="TVMonitorSelected"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC7A.png" alt-text="Screenshot of DeveloperTools."::: | EC7A | :::no-loc text="DeveloperTools"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC7E.png" alt-text="Screenshot of MobCallForwarding."::: | EC7E | :::no-loc text="MobCallForwarding"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC7F.png" alt-text="Screenshot of MobCallForwardingMirrored."::: | EC7F | :::no-loc text="MobCallForwardingMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC80.png" alt-text="Screenshot of BodyCam."::: | EC80 | :::no-loc text="BodyCam"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC81.png" alt-text="Screenshot of PoliceCar."::: | EC81 | :::no-loc text="PoliceCar"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC87.png" alt-text="Screenshot of Draw."::: | EC87 | :::no-loc text="Draw"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC88.png" alt-text="Screenshot of DrawSolid."::: | EC88 | :::no-loc text="DrawSolid"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC8A.png" alt-text="Screenshot of LowerBrightness."::: | EC8A | :::no-loc text="LowerBrightness"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC8F.png" alt-text="Screenshot of ScrollUpDown."::: | EC8F | :::no-loc text="ScrollUpDown"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EC92.png" alt-text="Screenshot of DateTime."::: | EC92 | :::no-loc text="DateTime"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ECA5.png" alt-text="Screenshot of Tiles."::: | ECA5 | :::no-loc text="Tiles"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ECA7.png" alt-text="Screenshot of PartyLeader."::: | ECA7 | :::no-loc text="PartyLeader"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ECAA.png" alt-text="Screenshot of AppIconDefault."::: | ECAA | :::no-loc text="AppIconDefault"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ECAD.png" alt-text="Screenshot of Calories."::: | ECAD | :::no-loc text="Calories"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ECB9.png" alt-text="Screenshot of BandBattery0."::: | ECB9 | :::no-loc text="BandBattery0"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ECBA.png" alt-text="Screenshot of BandBattery1."::: | ECBA | :::no-loc text="BandBattery1"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ECBB.png" alt-text="Screenshot of BandBattery2."::: | ECBB | :::no-loc text="BandBattery2"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ECBC.png" alt-text="Screenshot of BandBattery3."::: | ECBC | :::no-loc text="BandBattery3"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ECBD.png" alt-text="Screenshot of BandBattery4."::: | ECBD | :::no-loc text="BandBattery4"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ECBE.png" alt-text="Screenshot of BandBattery5."::: | ECBE | :::no-loc text="BandBattery5"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ECBF.png" alt-text="Screenshot of BandBattery6."::: | ECBF | :::no-loc text="BandBattery6"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ECC4.png" alt-text="Screenshot of AddSurfaceHub."::: | ECC4 | :::no-loc text="AddSurfaceHub"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ECC5.png" alt-text="Screenshot of DevUpdate."::: | ECC5 | :::no-loc text="DevUpdate"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ECC6.png" alt-text="Screenshot of Unit."::: | ECC6 | :::no-loc text="Unit"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ECC8.png" alt-text="Screenshot of AddTo."::: | ECC8 | :::no-loc text="AddTo"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ECC9.png" alt-text="Screenshot of RemoveFrom."::: | ECC9 | :::no-loc text="RemoveFrom"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ECCA.png" alt-text="Screenshot of RadioBtnOff."::: | ECCA | :::no-loc text="RadioBtnOff"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ECCB.png" alt-text="Screenshot of RadioBtnOn."::: | ECCB | :::no-loc text="RadioBtnOn"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ECCC.png" alt-text="Screenshot of RadioBullet2."::: | ECCC | :::no-loc text="RadioBullet2"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ECCD.png" alt-text="Screenshot of ExploreContent."::: | ECCD | :::no-loc text="ExploreContent"::: |
 | :::image type="content" border="false" source="images/segoe-mdl/ECE4.png" alt-text="Screenshot of Blocked2."::: | ECE4 | :::no-loc text="Blocked2"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ECE7.png" alt-text="Screenshot of ScrollMode."::: | ECE7 | :::no-loc text="ScrollMode"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ECE8.png" alt-text="Screenshot of ZoomMode."::: | ECE8 | :::no-loc text="ZoomMode"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ECE9.png" alt-text="Screenshot of PanMode."::: | ECE9 | :::no-loc text="PanMode"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ECF0.png" alt-text="Screenshot of WiredUSB  ."::: | ECF0 | :::no-loc text="WiredUSB  "::: |
| :::image type="content" border="false" source="images/segoe-mdl/ECF1.png" alt-text="Screenshot of WirelessUSB."::: | ECF1 | :::no-loc text="WirelessUSB"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ECF3.png" alt-text="Screenshot of USBSafeConnect."::: | ECF3 | :::no-loc text="USBSafeConnect"::: |

### PUA ED00-EF00

The following table of glyphs displays unicode points prefixed from ED-  to EF-.

[Back to top](#icon-list)

| Glyph | Unicode point | Description |
|-------|---------------|-------------|
| :::image type="content" border="false" source="images/segoe-mdl/ED0C.png" alt-text="Screenshot of ActionCenterNotificationMirrored."::: | ED0C | :::no-loc text="ActionCenterNotificationMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ED0D.png" alt-text="Screenshot of ActionCenterMirrored."::: | ED0D | :::no-loc text="ActionCenterMirrored"::: |
 | :::image type="content" border="false" source="images/segoe-mdl/ED0E.png" alt-text="Screenshot of SubscriptionAdd."::: | ED0E | :::no-loc text="SubscriptionAdd"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ED10.png" alt-text="Screenshot of ResetDevice."::: | ED10 | :::no-loc text="ResetDevice"::: |
 | :::image type="content" border="false" source="images/segoe-mdl/ED11.png" alt-text="Screenshot of SubscriptionAddMirrored."::: | ED11 | :::no-loc text="SubscriptionAddMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ED14.png" alt-text="Screenshot of QRCode."::: | ED14 | :::no-loc text="QRCode"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ED15.png" alt-text="Screenshot of Feedback."::: | ED15 | :::no-loc text="Feedback"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ED1E.png" alt-text="Screenshot of Subtitles."::: | ED1E | :::no-loc text="Subtitles"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ED1F.png" alt-text="Screenshot of SubtitlesAudio."::: | ED1F | :::no-loc text="SubtitlesAudio"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ED25.png" alt-text="Screenshot of OpenFolderHorizontal."::: | ED25 | :::no-loc text="OpenFolderHorizontal"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ED28.png" alt-text="Screenshot of CalendarMirrored."::: | ED28 | :::no-loc text="CalendarMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ED2A.png" alt-text="Screenshot of MobeSIM."::: | ED2A | :::no-loc text="MobeSIM"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ED2B.png" alt-text="Screenshot of MobeSIMNoProfile."::: | ED2B | :::no-loc text="MobeSIMNoProfile"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ED2C.png" alt-text="Screenshot of MobeSIMLocked."::: | ED2C | :::no-loc text="MobeSIMLocked"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ED2D.png" alt-text="Screenshot of MobeSIMBusy."::: | ED2D | :::no-loc text="MobeSIMBusy"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ED2E.png" alt-text="Screenshot of SignalError."::: | ED2E | :::no-loc text="SignalError"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ED2F.png" alt-text="Screenshot of StreamingEnterprise."::: | ED2F | :::no-loc text="StreamingEnterprise"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ED30.png" alt-text="Screenshot of Headphone0."::: | ED30 | :::no-loc text="Headphone0"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ED31.png" alt-text="Screenshot of Headphone1."::: | ED31 | :::no-loc text="Headphone1"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ED32.png" alt-text="Screenshot of Headphone2."::: | ED32 | :::no-loc text="Headphone2"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ED33.png" alt-text="Screenshot of Headphone3."::: | ED33 | :::no-loc text="Headphone3"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ED35.png" alt-text="Screenshot of Apps."::: | ED35 | :::no-loc text="Apps"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ED39.png" alt-text="Screenshot of KeyboardBrightness."::: | ED39 | :::no-loc text="KeyboardBrightness"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ED3A.png" alt-text="Screenshot of KeyboardLowerBrightness."::: | ED3A | :::no-loc text="KeyboardLowerBrightness"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ED3C.png" alt-text="Screenshot of SkipBack10."::: | ED3C | :::no-loc text="SkipBack10"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ED3D.png" alt-text="Screenshot of SkipForward30 ."::: | ED3D | :::no-loc text="SkipForward30 "::: |
| :::image type="content" border="false" source="images/segoe-mdl/ED41.png" alt-text="Screenshot of TreeFolderFolder."::: | ED41 | :::no-loc text="TreeFolderFolder"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ED42.png" alt-text="Screenshot of TreeFolderFolderFill."::: | ED42 | :::no-loc text="TreeFolderFolderFill"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ED43.png" alt-text="Screenshot of TreeFolderFolderOpen."::: | ED43 | :::no-loc text="TreeFolderFolderOpen"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ED44.png" alt-text="Screenshot of TreeFolderFolderOpenFill."::: | ED44 | :::no-loc text="TreeFolderFolderOpenFill"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ED47.png" alt-text="Screenshot of MultimediaDMP."::: | ED47 | :::no-loc text="MultimediaDMP"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ED4C.png" alt-text="Screenshot of KeyboardOneHanded."::: | ED4C | :::no-loc text="KeyboardOneHanded"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ED4D.png" alt-text="Screenshot of Narrator."::: | ED4D | :::no-loc text="Narrator"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ED53.png" alt-text="Screenshot of EmojiTabPeople."::: | ED53 | :::no-loc text="EmojiTabPeople"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ED54.png" alt-text="Screenshot of EmojiTabSmilesAnimals."::: | ED54 | :::no-loc text="EmojiTabSmilesAnimals"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ED55.png" alt-text="Screenshot of EmojiTabCelebrationObjects."::: | ED55 | :::no-loc text="EmojiTabCelebrationObjects"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ED56.png" alt-text="Screenshot of EmojiTabFoodPlants."::: | ED56 | :::no-loc text="EmojiTabFoodPlants"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ED57.png" alt-text="Screenshot of EmojiTabTransitPlaces."::: | ED57 | :::no-loc text="EmojiTabTransitPlaces"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ED58.png" alt-text="Screenshot of EmojiTabSymbols."::: | ED58 | :::no-loc text="EmojiTabSymbols"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ED59.png" alt-text="Screenshot of EmojiTabTextSmiles."::: | ED59 | :::no-loc text="EmojiTabTextSmiles"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ED5A.png" alt-text="Screenshot of EmojiTabFavorites."::: | ED5A | :::no-loc text="EmojiTabFavorites"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ED5B.png" alt-text="Screenshot of EmojiSwatch."::: | ED5B | :::no-loc text="EmojiSwatch"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ED5C.png" alt-text="Screenshot of ConnectApp."::: | ED5C | :::no-loc text="ConnectApp"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ED5D.png" alt-text="Screenshot of CompanionDeviceFramework."::: | ED5D | :::no-loc text="CompanionDeviceFramework"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ED5E.png" alt-text="Screenshot of Ruler."::: | ED5E | :::no-loc text="Ruler"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ED5F.png" alt-text="Screenshot of FingerInking."::: | ED5F | :::no-loc text="FingerInking"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ED60.png" alt-text="Screenshot of StrokeErase."::: | ED60 | :::no-loc text="StrokeErase"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ED61.png" alt-text="Screenshot of PointErase."::: | ED61 | :::no-loc text="PointErase"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ED62.png" alt-text="Screenshot of ClearAllInk."::: | ED62 | :::no-loc text="ClearAllInk"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ED63.png" alt-text="Screenshot of Pencil."::: | ED63 | :::no-loc text="Pencil"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ED64.png" alt-text="Screenshot of Marker."::: | ED64 | :::no-loc text="Marker"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ED65.png" alt-text="Screenshot of InkingCaret."::: | ED65 | :::no-loc text="InkingCaret"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ED66.png" alt-text="Screenshot of InkingColorOutline."::: | ED66 | :::no-loc text="InkingColorOutline"::: |
| :::image type="content" border="false" source="images/segoe-mdl/ED67.png" alt-text="Screenshot of InkingColorFill."::: | ED67 | :::no-loc text="InkingColorFill"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EDA2.png" alt-text="Screenshot of HardDrive."::: | EDA2 | :::no-loc text="HardDrive"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EDA3.png" alt-text="Screenshot of NetworkAdapter."::: | EDA3 | :::no-loc text="NetworkAdapter"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EDA4.png" alt-text="Screenshot of Touchscreen."::: | EDA4 | :::no-loc text="Touchscreen"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EDA5.png" alt-text="Screenshot of NetworkPrinter."::: | EDA5 | :::no-loc text="NetworkPrinter"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EDA6.png" alt-text="Screenshot of CloudPrinter."::: | EDA6 | :::no-loc text="CloudPrinter"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EDA7.png" alt-text="Screenshot of KeyboardShortcut."::: | EDA7 | :::no-loc text="KeyboardShortcut"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EDA8.png" alt-text="Screenshot of BrushSize."::: | EDA8 | :::no-loc text="BrushSize"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EDA9.png" alt-text="Screenshot of NarratorForward."::: | EDA9 | :::no-loc text="NarratorForward"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EDAA.png" alt-text="Screenshot of NarratorForwardMirrored."::: | EDAA | :::no-loc text="NarratorForwardMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EDAB.png" alt-text="Screenshot of SyncBadge12."::: | EDAB | :::no-loc text="SyncBadge12"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EDAC.png" alt-text="Screenshot of RingerBadge12."::: | EDAC | :::no-loc text="RingerBadge12"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EDAD.png" alt-text="Screenshot of AsteriskBadge12."::: | EDAD | :::no-loc text="AsteriskBadge12"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EDAE.png" alt-text="Screenshot of ErrorBadge12."::: | EDAE | :::no-loc text="ErrorBadge12"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EDAF.png" alt-text="Screenshot of CircleRingBadge12."::: | EDAF | :::no-loc text="CircleRingBadge12"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EDB0.png" alt-text="Screenshot of CircleFillBadge12."::: | EDB0 | :::no-loc text="CircleFillBadge12"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EDB1.png" alt-text="Screenshot of ImportantBadge12."::: | EDB1 | :::no-loc text="ImportantBadge12"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EDB3.png" alt-text="Screenshot of MailBadge12."::: | EDB3 | :::no-loc text="MailBadge12"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EDB4.png" alt-text="Screenshot of PauseBadge12."::: | EDB4 | :::no-loc text="PauseBadge12"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EDB5.png" alt-text="Screenshot of PlayBadge12."::: | EDB5 | :::no-loc text="PlayBadge12"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EDC6.png" alt-text="Screenshot of PenWorkspace."::: | EDC6 | :::no-loc text="PenWorkspace"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EDD6.png" alt-text="Screenshot of CaretRight8."::: | EDD6 | :::no-loc text="CaretRight8"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EDD9.png" alt-text="Screenshot of CaretLeftSolid8."::: | EDD9 | :::no-loc text="CaretLeftSolid8"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EDDA.png" alt-text="Screenshot of CaretRightSolid8."::: | EDDA | :::no-loc text="CaretRightSolid8"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EDDB.png" alt-text="Screenshot of CaretUpSolid8."::: | EDDB | :::no-loc text="CaretUpSolid8"::: |
 | :::image type="content" border="false" source="images/segoe-mdl/EDDC.png" alt-text="Screenshot of CaretDownSolid8."::: | EDDC | :::no-loc text="CaretDownSolid8"::: |
 | :::image type="content" border="false" source="images/segoe-mdl/EDE0.png" alt-text="Screenshot of Strikethrough."::: | EDE0 | :::no-loc text="Strikethrough"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EDE1.png" alt-text="Screenshot of Export."::: | EDE1 | :::no-loc text="Export"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EDE2.png" alt-text="Screenshot of ExportMirrored."::: | EDE2 | :::no-loc text="ExportMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EDE3.png" alt-text="Screenshot of ButtonMenu."::: | EDE3 | :::no-loc text="ButtonMenu"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EDE4.png" alt-text="Screenshot of CloudSearch."::: | EDE4 | :::no-loc text="CloudSearch"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EDE5.png" alt-text="Screenshot of PinyinIMELogo."::: | EDE5 | :::no-loc text="PinyinIMELogo"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EDFB.png" alt-text="Screenshot of CalligraphyPen."::: | EDFB | :::no-loc text="CalligraphyPen"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EE35.png" alt-text="Screenshot of ReplyMirrored."::: | EE35 | :::no-loc text="ReplyMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EE3F.png" alt-text="Screenshot of LockscreenDesktop."::: | EE3F | :::no-loc text="LockscreenDesktop"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EE40.png" alt-text="Screenshot of TaskViewSettings."::: | EE40 | :::no-loc text="TaskViewSettings"::: |
 | :::image type="content" border="false" source="images/segoe-mdl/EE47.png" alt-text="Screenshot of MiniExpand2Mirrored."::: | EE47 | :::no-loc text="MiniExpand2Mirrored"::: |
 | :::image type="content" border="false" source="images/segoe-mdl/EE49.png" alt-text="Screenshot of MiniContract2Mirrored."::: | EE49 | :::no-loc text="MiniContract2Mirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EE4A.png" alt-text="Screenshot of Play36."::: | EE4A | :::no-loc text="Play36"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EE56.png" alt-text="Screenshot of PenPalette."::: | EE56 | :::no-loc text="PenPalette"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EE57.png" alt-text="Screenshot of GuestUser."::: | EE57 | :::no-loc text="GuestUser"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EE63.png" alt-text="Screenshot of SettingsBattery."::: | EE63 | :::no-loc text="SettingsBattery"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EE64.png" alt-text="Screenshot of TaskbarPhone."::: | EE64 | :::no-loc text="TaskbarPhone"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EE65.png" alt-text="Screenshot of LockScreenGlance."::: | EE65 | :::no-loc text="LockScreenGlance"::: |
 | :::image type="content" border="false" source="images/segoe-mdl/EE6F.png" alt-text="Screenshot of GenericScan."::: | EE6F | :::no-loc text="GenericScan"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EE71.png" alt-text="Screenshot of ImageExport ."::: | EE71 | :::no-loc text="ImageExport "::: |
| :::image type="content" border="false" source="images/segoe-mdl/EE77.png" alt-text="Screenshot of WifiEthernet."::: | EE77 | :::no-loc text="WifiEthernet"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EE79.png" alt-text="Screenshot of ActionCenterQuiet."::: | EE79 | :::no-loc text="ActionCenterQuiet"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EE7A.png" alt-text="Screenshot of ActionCenterQuietNotification."::: | EE7A | :::no-loc text="ActionCenterQuietNotification"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EE92.png" alt-text="Screenshot of TrackersMirrored."::: | EE92 | :::no-loc text="TrackersMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EE93.png" alt-text="Screenshot of DateTimeMirrored."::: | EE93 | :::no-loc text="DateTimeMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EE94.png" alt-text="Screenshot of Wheel."::: | EE94 | :::no-loc text="Wheel"::: |
 | :::image type="content" border="false" source="images/segoe-mdl/EEA3.png" alt-text="Screenshot of VirtualMachineGroup."::: | EEA3 | :::no-loc text="VirtualMachineGroup"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EECA.png" alt-text="Screenshot of ButtonView2."::: | EECA | :::no-loc text="ButtonView2"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EF15.png" alt-text="Screenshot of PenWorkspaceMirrored."::: | EF15 | :::no-loc text="PenWorkspaceMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EF16.png" alt-text="Screenshot of PenPaletteMirrored."::: | EF16 | :::no-loc text="PenPaletteMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EF17.png" alt-text="Screenshot of StrokeEraseMirrored."::: | EF17 | :::no-loc text="StrokeEraseMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EF18.png" alt-text="Screenshot of PointEraseMirrored."::: | EF18 | :::no-loc text="PointEraseMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EF19.png" alt-text="Screenshot of ClearAllInkMirrored."::: | EF19 | :::no-loc text="ClearAllInkMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EF1F.png" alt-text="Screenshot of BackgroundToggle."::: | EF1F | :::no-loc text="BackgroundToggle"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EF20.png" alt-text="Screenshot of Marquee."::: | EF20 | :::no-loc text="Marquee"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EF2C.png" alt-text="Screenshot of ChromeCloseContrast."::: | EF2C | :::no-loc text="ChromeCloseContrast"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EF2D.png" alt-text="Screenshot of ChromeMinimizeContrast."::: | EF2D | :::no-loc text="ChromeMinimizeContrast"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EF2E.png" alt-text="Screenshot of ChromeMaximizeContrast."::: | EF2E | :::no-loc text="ChromeMaximizeContrast"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EF2F.png" alt-text="Screenshot of ChromeRestoreContrast."::: | EF2F | :::no-loc text="ChromeRestoreContrast"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EF31.png" alt-text="Screenshot of TrafficLight."::: | EF31 | :::no-loc text="TrafficLight"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EF3B.png" alt-text="Screenshot of Replay."::: | EF3B | :::no-loc text="Replay"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EF3C.png" alt-text="Screenshot of Eyedropper."::: | EF3C | :::no-loc text="Eyedropper"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EF3D.png" alt-text="Screenshot of LineDisplay."::: | EF3D | :::no-loc text="LineDisplay"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EF3E.png" alt-text="Screenshot of PINPad."::: | EF3E | :::no-loc text="PINPad"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EF3F.png" alt-text="Screenshot of SignatureCapture."::: | EF3F | :::no-loc text="SignatureCapture"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EF40.png" alt-text="Screenshot of ChipCardCreditCardReader."::: | EF40 | :::no-loc text="ChipCardCreditCardReader"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EF58.png" alt-text="Screenshot of PlayerSettings."::: | EF58 | :::no-loc text="PlayerSettings"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EF6B.png" alt-text="Screenshot of LandscapeOrientation."::: | EF6B | :::no-loc text="LandscapeOrientation"::: |
 | :::image type="content" border="false" source="images/segoe-mdl/EF90.png" alt-text="Screenshot of Flow."::: | EF90 | :::no-loc text="Flow"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EFA5.png" alt-text="Screenshot of Touchpad."::: | EFA5 | :::no-loc text="Touchpad"::: |
| :::image type="content" border="false" source="images/segoe-mdl/EFA9.png" alt-text="Screenshot of Speech."::: | EFA9 | :::no-loc text="Speech"::: |

### PUA F000-F200

The following table of glyphs displays unicode points prefixed from F0-  to F2-.

[Back to top](#icon-list)

| Glyph | Unicode point | Description |
|-------|---------------|-------------|
| :::image type="content" border="false" source="images/segoe-mdl/F000.png" alt-text="Screenshot of KnowledgeArticle."::: | F000 | :::no-loc text="KnowledgeArticle"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F003.png" alt-text="Screenshot of Relationship."::: | F003 | :::no-loc text="Relationship"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F080.png" alt-text="Screenshot of DefaultAPN."::: | F080 | :::no-loc text="DefaultAPN"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F081.png" alt-text="Screenshot of UserAPN ."::: | F081 | :::no-loc text="UserAPN "::: |
| :::image type="content" border="false" source="images/segoe-mdl/F085.png" alt-text="Screenshot of DoublePinyin."::: | F085 | :::no-loc text="DoublePinyin"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F08C.png" alt-text="Screenshot of BlueLight."::: | F08C | :::no-loc text="BlueLight"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F093.png" alt-text="Screenshot of ButtonA."::: | F093 | :::no-loc text="ButtonA"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F094.png" alt-text="Screenshot of ButtonB."::: | F094 | :::no-loc text="ButtonB"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F095.png" alt-text="Screenshot of ButtonY."::: | F095 | :::no-loc text="ButtonY"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F096.png" alt-text="Screenshot of ButtonX."::: | F096 | :::no-loc text="ButtonX"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F0AD.png" alt-text="Screenshot of ArrowUp8."::: | F0AD | :::no-loc text="ArrowUp8"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F0AE.png" alt-text="Screenshot of ArrowDown8."::: | F0AE | :::no-loc text="ArrowDown8"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F0AF.png" alt-text="Screenshot of ArrowRight8."::: | F0AF | :::no-loc text="ArrowRight8"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F0B0.png" alt-text="Screenshot of ArrowLeft8."::: | F0B0 | :::no-loc text="ArrowLeft8"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F0B2.png" alt-text="Screenshot of QuarentinedItems."::: | F0B2 | :::no-loc text="QuarentinedItems"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F0B3.png" alt-text="Screenshot of QuarentinedItemsMirrored."::: | F0B3 | :::no-loc text="QuarentinedItemsMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F0B4.png" alt-text="Screenshot of Protractor."::: | F0B4 | :::no-loc text="Protractor"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F0B5.png" alt-text="Screenshot of ChecklistMirrored."::: | F0B5 | :::no-loc text="ChecklistMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F0B6.png" alt-text="Screenshot of StatusCircle7."::: | F0B6 | :::no-loc text="StatusCircle7"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F0B7.png" alt-text="Screenshot of StatusCheckmark7."::: | F0B7 | :::no-loc text="StatusCheckmark7"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F0B8.png" alt-text="Screenshot of StatusErrorCircle7."::: | F0B8 | :::no-loc text="StatusErrorCircle7"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F0B9.png" alt-text="Screenshot of Connected."::: | F0B9 | :::no-loc text="Connected"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F0C6.png" alt-text="Screenshot of PencilFill."::: | F0C6 | :::no-loc text="PencilFill"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F0C7.png" alt-text="Screenshot of CalligraphyFill."::: | F0C7 | :::no-loc text="CalligraphyFill"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F0CA.png" alt-text="Screenshot of QuarterStarLeft."::: | F0CA | :::no-loc text="QuarterStarLeft"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F0CB.png" alt-text="Screenshot of QuarterStarRight."::: | F0CB | :::no-loc text="QuarterStarRight"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F0CC.png" alt-text="Screenshot of ThreeQuarterStarLeft."::: | F0CC | :::no-loc text="ThreeQuarterStarLeft"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F0CD.png" alt-text="Screenshot of ThreeQuarterStarRight."::: | F0CD | :::no-loc text="ThreeQuarterStarRight"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F0CE.png" alt-text="Screenshot of QuietHoursBadge12."::: | F0CE | :::no-loc text="QuietHoursBadge12"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F0D2.png" alt-text="Screenshot of BackMirrored."::: | F0D2 | :::no-loc text="BackMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F0D3.png" alt-text="Screenshot of ForwardMirrored."::: | F0D3 | :::no-loc text="ForwardMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F0D5.png" alt-text="Screenshot of ChromeBackContrast."::: | F0D5 | :::no-loc text="ChromeBackContrast"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F0D6.png" alt-text="Screenshot of ChromeBackContrastMirrored."::: | F0D6 | :::no-loc text="ChromeBackContrastMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F0D7.png" alt-text="Screenshot of ChromeBackToWindowContrast."::: | F0D7 | :::no-loc text="ChromeBackToWindowContrast"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F0D8.png" alt-text="Screenshot of ChromeFullScreenContrast."::: | F0D8 | :::no-loc text="ChromeFullScreenContrast"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F0E2.png" alt-text="Screenshot of GridView."::: | F0E2 | :::no-loc text="GridView"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F0E3.png" alt-text="Screenshot of ClipboardList."::: | F0E3 | :::no-loc text="ClipboardList"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F0E4.png" alt-text="Screenshot of ClipboardListMirrored."::: | F0E4 | :::no-loc text="ClipboardListMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F0E5.png" alt-text="Screenshot of OutlineQuarterStarLeft."::: | F0E5 | :::no-loc text="OutlineQuarterStarLeft"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F0E6.png" alt-text="Screenshot of OutlineQuarterStarRight."::: | F0E6 | :::no-loc text="OutlineQuarterStarRight"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F0E7.png" alt-text="Screenshot of OutlineHalfStarLeft."::: | F0E7 | :::no-loc text="OutlineHalfStarLeft"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F0E8.png" alt-text="Screenshot of OutlineHalfStarRight."::: | F0E8 | :::no-loc text="OutlineHalfStarRight"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F0E9.png" alt-text="Screenshot of OutlineThreeQuarterStarLeft."::: | F0E9 | :::no-loc text="OutlineThreeQuarterStarLeft"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F0EA.png" alt-text="Screenshot of OutlineThreeQuarterStarRight."::: | F0EA | :::no-loc text="OutlineThreeQuarterStarRight"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F0EB.png" alt-text="Screenshot of SpatialVolume0."::: | F0EB | :::no-loc text="SpatialVolume0"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F0EC.png" alt-text="Screenshot of SpatialVolume1."::: | F0EC | :::no-loc text="SpatialVolume1"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F0ED.png" alt-text="Screenshot of SpatialVolume2."::: | F0ED | :::no-loc text="SpatialVolume2"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F0EE.png" alt-text="Screenshot of SpatialVolume3."::: | F0EE | :::no-loc text="SpatialVolume3"::: |
 | :::image type="content" border="false" source="images/segoe-mdl/F0EF.png" alt-text="Screenshot of ApplicationGuard."::: | F0EF | :::no-loc text="ApplicationGuard"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F0F7.png" alt-text="Screenshot of OutlineStarLeftHalf."::: | F0F7 | :::no-loc text="OutlineStarLeftHalf"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F0F8.png" alt-text="Screenshot of OutlineStarRightHalf."::: | F0F8 | :::no-loc text="OutlineStarRightHalf"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F0F9.png" alt-text="Screenshot of ChromeAnnotateContrast."::: | F0F9 | :::no-loc text="ChromeAnnotateContrast"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F0FB.png" alt-text="Screenshot of DefenderBadge12."::: | F0FB | :::no-loc text="DefenderBadge12"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F103.png" alt-text="Screenshot of DetachablePC."::: | F103 | :::no-loc text="DetachablePC"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F108.png" alt-text="Screenshot of LeftStick."::: | F108 | :::no-loc text="LeftStick"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F109.png" alt-text="Screenshot of RightStick."::: | F109 | :::no-loc text="RightStick"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F10A.png" alt-text="Screenshot of TriggerLeft."::: | F10A | :::no-loc text="TriggerLeft"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F10B.png" alt-text="Screenshot of TriggerRight."::: | F10B | :::no-loc text="TriggerRight"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F10C.png" alt-text="Screenshot of BumperLeft."::: | F10C | :::no-loc text="BumperLeft"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F10D.png" alt-text="Screenshot of BumperRight."::: | F10D | :::no-loc text="BumperRight"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F10E.png" alt-text="Screenshot of Dpad."::: | F10E | :::no-loc text="Dpad"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F110.png" alt-text="Screenshot of EnglishPunctuation."::: | F110 | :::no-loc text="EnglishPunctuation"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F111.png" alt-text="Screenshot of ChinesePunctuation."::: | F111 | :::no-loc text="ChinesePunctuation"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F119.png" alt-text="Screenshot of HMD."::: | F119 | :::no-loc text="HMD"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F11B.png" alt-text="Screenshot of CtrlSpatialRight."::: | F11B | :::no-loc text="CtrlSpatialRight"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F126.png" alt-text="Screenshot of PaginationDotOutline10."::: | F126 | :::no-loc text="PaginationDotOutline10"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F127.png" alt-text="Screenshot of PaginationDotSolid10."::: | F127 | :::no-loc text="PaginationDotSolid10"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F128.png" alt-text="Screenshot of StrokeErase2."::: | F128 | :::no-loc text="StrokeErase2"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F129.png" alt-text="Screenshot of SmallErase."::: | F129 | :::no-loc text="SmallErase"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F12A.png" alt-text="Screenshot of LargeErase."::: | F12A | :::no-loc text="LargeErase"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F12B.png" alt-text="Screenshot of FolderHorizontal."::: | F12B | :::no-loc text="FolderHorizontal"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F12E.png" alt-text="Screenshot of MicrophoneListening."::: | F12E | :::no-loc text="MicrophoneListening"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F12F.png" alt-text="Screenshot of StatusExclamationCircle7 ."::: | F12F | :::no-loc text="StatusExclamationCircle7 "::: |
| :::image type="content" border="false" source="images/segoe-mdl/F131.png" alt-text="Screenshot of Video360."::: | F131 | :::no-loc text="Video360"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F133.png" alt-text="Screenshot of GiftboxOpen."::: | F133 | :::no-loc text="GiftboxOpen"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F136.png" alt-text="Screenshot of StatusCircleOuter."::: | F136 | :::no-loc text="StatusCircleOuter"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F137.png" alt-text="Screenshot of StatusCircleInner."::: | F137 | :::no-loc text="StatusCircleInner"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F138.png" alt-text="Screenshot of StatusCircleRing."::: | F138 | :::no-loc text="StatusCircleRing"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F139.png" alt-text="Screenshot of StatusTriangleOuter."::: | F139 | :::no-loc text="StatusTriangleOuter"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F13A.png" alt-text="Screenshot of StatusTriangleInner."::: | F13A | :::no-loc text="StatusTriangleInner"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F13B.png" alt-text="Screenshot of StatusTriangleExclamation."::: | F13B | :::no-loc text="StatusTriangleExclamation"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F13C.png" alt-text="Screenshot of StatusCircleExclamation."::: | F13C | :::no-loc text="StatusCircleExclamation"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F13D.png" alt-text="Screenshot of StatusCircleErrorX."::: | F13D | :::no-loc text="StatusCircleErrorX"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F13E.png" alt-text="Screenshot of StatusCircleCheckmark."::: | F13E | :::no-loc text="StatusCircleCheckmark"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F13F.png" alt-text="Screenshot of StatusCircleInfo."::: | F13F | :::no-loc text="StatusCircleInfo"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F140.png" alt-text="Screenshot of StatusCircleBlock."::: | F140 | :::no-loc text="StatusCircleBlock"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F141.png" alt-text="Screenshot of StatusCircleBlock2."::: | F141 | :::no-loc text="StatusCircleBlock2"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F142.png" alt-text="Screenshot of StatusCircleQuestionMark."::: | F142 | :::no-loc text="StatusCircleQuestionMark"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F143.png" alt-text="Screenshot of StatusCircleSync."::: | F143 | :::no-loc text="StatusCircleSync"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F146.png" alt-text="Screenshot of Dial1."::: | F146 | :::no-loc text="Dial1"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F147.png" alt-text="Screenshot of Dial2."::: | F147 | :::no-loc text="Dial2"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F148.png" alt-text="Screenshot of Dial3."::: | F148 | :::no-loc text="Dial3"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F149.png" alt-text="Screenshot of Dial4."::: | F149 | :::no-loc text="Dial4"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F14A.png" alt-text="Screenshot of Dial5."::: | F14A | :::no-loc text="Dial5"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F14B.png" alt-text="Screenshot of Dial6."::: | F14B | :::no-loc text="Dial6"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F14C.png" alt-text="Screenshot of Dial7."::: | F14C | :::no-loc text="Dial7"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F14D.png" alt-text="Screenshot of Dial8."::: | F14D | :::no-loc text="Dial8"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F14E.png" alt-text="Screenshot of Dial9."::: | F14E | :::no-loc text="Dial9"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F14F.png" alt-text="Screenshot of Dial10."::: | F14F | :::no-loc text="Dial10"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F150.png" alt-text="Screenshot of Dial11."::: | F150 | :::no-loc text="Dial11"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F151.png" alt-text="Screenshot of Dial12."::: | F151 | :::no-loc text="Dial12"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F152.png" alt-text="Screenshot of Dial13."::: | F152 | :::no-loc text="Dial13"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F153.png" alt-text="Screenshot of Dial14."::: | F153 | :::no-loc text="Dial14"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F154.png" alt-text="Screenshot of Dial15."::: | F154 | :::no-loc text="Dial15"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F155.png" alt-text="Screenshot of Dial16."::: | F155 | :::no-loc text="Dial16"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F156.png" alt-text="Screenshot of DialShape1."::: | F156 | :::no-loc text="DialShape1"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F157.png" alt-text="Screenshot of DialShape2."::: | F157 | :::no-loc text="DialShape2"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F158.png" alt-text="Screenshot of DialShape3."::: | F158 | :::no-loc text="DialShape3"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F159.png" alt-text="Screenshot of DialShape4."::: | F159 | :::no-loc text="DialShape4"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F161.png" alt-text="Screenshot of TollSolid."::: | F161 | :::no-loc text="TollSolid"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F163.png" alt-text="Screenshot of TrafficCongestionSolid."::: | F163 | :::no-loc text="TrafficCongestionSolid"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F164.png" alt-text="Screenshot of ExploreContentSingle."::: | F164 | :::no-loc text="ExploreContentSingle"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F165.png" alt-text="Screenshot of CollapseContent."::: | F165 | :::no-loc text="CollapseContent"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F166.png" alt-text="Screenshot of CollapseContentSingle."::: | F166 | :::no-loc text="CollapseContentSingle"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F167.png" alt-text="Screenshot of InfoSolid."::: | F167 | :::no-loc text="InfoSolid"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F168.png" alt-text="Screenshot of GroupList."::: | F168 | :::no-loc text="GroupList"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F169.png" alt-text="Screenshot of CaretBottomRightSolidCenter8."::: | F169 | :::no-loc text="CaretBottomRightSolidCenter8"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F16A.png" alt-text="Screenshot of ProgressRingDots."::: | F16A | :::no-loc text="ProgressRingDots"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F16B.png" alt-text="Screenshot of Checkbox14."::: | F16B | :::no-loc text="Checkbox14"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F16C.png" alt-text="Screenshot of CheckboxComposite14."::: | F16C | :::no-loc text="CheckboxComposite14"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F16D.png" alt-text="Screenshot of CheckboxIndeterminateCombo14."::: | F16D | :::no-loc text="CheckboxIndeterminateCombo14"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F16E.png" alt-text="Screenshot of CheckboxIndeterminateCombo."::: | F16E | :::no-loc text="CheckboxIndeterminateCombo"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F175.png" alt-text="Screenshot of StatusPause7."::: | F175 | :::no-loc text="StatusPause7"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F17F.png" alt-text="Screenshot of CharacterAppearance."::: | F17F | :::no-loc text="CharacterAppearance"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F180.png" alt-text="Screenshot of Lexicon ."::: | F180 | :::no-loc text="Lexicon "::: |
| :::image type="content" border="false" source="images/segoe-mdl/F182.png" alt-text="Screenshot of ScreenTime."::: | F182 | :::no-loc text="ScreenTime"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F191.png" alt-text="Screenshot of HeadlessDevice."::: | F191 | :::no-loc text="HeadlessDevice"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F193.png" alt-text="Screenshot of NetworkSharing."::: | F193 | :::no-loc text="NetworkSharing"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F19D.png" alt-text="Screenshot of EyeGaze."::: | F19D | :::no-loc text="EyeGaze"::: |
 | :::image type="content" border="false" source="images/segoe-mdl/F19E.png" alt-text="Screenshot of ToggleLeft."::: | F19E | :::no-loc text="ToggleLeft"::: |
 | :::image type="content" border="false" source="images/segoe-mdl/F19F.png" alt-text="Screenshot of ToggleRight."::: | F19F | :::no-loc text="ToggleRight"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F1AD.png" alt-text="Screenshot of WindowsInsider."::: | F1AD | :::no-loc text="WindowsInsider"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F1CB.png" alt-text="Screenshot of ChromeSwitch."::: | F1CB | :::no-loc text="ChromeSwitch"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F1CC.png" alt-text="Screenshot of ChromeSwitchContast."::: | F1CC | :::no-loc text="ChromeSwitchContast"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F1D8.png" alt-text="Screenshot of StatusCheckmark."::: | F1D8 | :::no-loc text="StatusCheckmark"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F1D9.png" alt-text="Screenshot of StatusCheckmarkLeft."::: | F1D9 | :::no-loc text="StatusCheckmarkLeft"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F20C.png" alt-text="Screenshot of KeyboardLeftAligned."::: | F20C | :::no-loc text="KeyboardLeftAligned"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F20D.png" alt-text="Screenshot of KeyboardRightAligned."::: | F20D | :::no-loc text="KeyboardRightAligned"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F210.png" alt-text="Screenshot of KeyboardSettings."::: | F210 | :::no-loc text="KeyboardSettings"::: |
 | :::image type="content" border="false" source="images/segoe-mdl/F211.png" alt-text="Screenshot of NetworkPhysical."::: | F211 | :::no-loc text="NetworkPhysical"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F22C.png" alt-text="Screenshot of IOT."::: | F22C | :::no-loc text="IOT"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F22E.png" alt-text="Screenshot of UnknownMirrored."::: | F22E | :::no-loc text="UnknownMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F246.png" alt-text="Screenshot of ViewDashboard."::: | F246 | :::no-loc text="ViewDashboard"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F259.png" alt-text="Screenshot of ExploitProtectionSettings."::: | F259 | :::no-loc text="ExploitProtectionSettings"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F260.png" alt-text="Screenshot of KeyboardNarrow."::: | F260 | :::no-loc text="KeyboardNarrow"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F261.png" alt-text="Screenshot of Keyboard12Key."::: | F261 | :::no-loc text="Keyboard12Key"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F26B.png" alt-text="Screenshot of KeyboardDock."::: | F26B | :::no-loc text="KeyboardDock"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F26C.png" alt-text="Screenshot of KeyboardUndock."::: | F26C | :::no-loc text="KeyboardUndock"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F26D.png" alt-text="Screenshot of KeyboardLeftDock."::: | F26D | :::no-loc text="KeyboardLeftDock"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F26E.png" alt-text="Screenshot of KeyboardRightDock."::: | F26E | :::no-loc text="KeyboardRightDock"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F270.png" alt-text="Screenshot of Ear."::: | F270 | :::no-loc text="Ear"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F271.png" alt-text="Screenshot of PointerHand."::: | F271 | :::no-loc text="PointerHand"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F272.png" alt-text="Screenshot of Bullseye."::: | F272 | :::no-loc text="Bullseye"::: |
 | :::image type="content" border="false" source="images/segoe-mdl/F2B7.png" alt-text="Screenshot of LocaleLanguage."::: | F2B7 | :::no-loc text="LocaleLanguage"::: |

### PUA F300-F500

The following table of glyphs displays unicode points prefixed from F3-  to F5-.

[Back to top](#icon-list)

| Glyph | Unicode point | Description |
|-------|---------------|-------------|
| :::image type="content" border="false" source="images/segoe-mdl/F32A.png" alt-text="Screenshot of PassiveAuthentication."::: | F32A | :::no-loc text="PassiveAuthentication"::: |
 | :::image type="content" border="false" source="images/segoe-mdl/F354.png" alt-text="Screenshot of ColorSolid."::: | F354 | :::no-loc text="ColorSolid"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F384.png" alt-text="Screenshot of NetworkOffline."::: | F384 | :::no-loc text="NetworkOffline"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F385.png" alt-text="Screenshot of NetworkConnected."::: | F385 | :::no-loc text="NetworkConnected"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F386.png" alt-text="Screenshot of NetworkConnectedCheckmark."::: | F386 | :::no-loc text="NetworkConnectedCheckmark"::: |
 | :::image type="content" border="false" source="images/segoe-mdl/F3B1.png" alt-text="Screenshot of SignOut."::: | F3B1 | :::no-loc text="SignOut"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F3CC.png" alt-text="Screenshot of StatusInfo."::: | F3CC | :::no-loc text="StatusInfo"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F3CD.png" alt-text="Screenshot of StatusInfoLeft."::: | F3CD | :::no-loc text="StatusInfoLeft"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F3E2.png" alt-text="Screenshot of NearbySharing."::: | F3E2 | :::no-loc text="NearbySharing"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F3E7.png" alt-text="Screenshot of CtrlSpatialLeft."::: | F3E7 | :::no-loc text="CtrlSpatialLeft"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F404.png" alt-text="Screenshot of InteractiveDashboard."::: | F404 | :::no-loc text="InteractiveDashboard"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F406.png" alt-text="Screenshot of ClippingTool."::: | F406 | :::no-loc text="ClippingTool"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F407.png" alt-text="Screenshot of RectangularClipping ."::: | F407 | :::no-loc text="RectangularClipping "::: |
| :::image type="content" border="false" source="images/segoe-mdl/F408.png" alt-text="Screenshot of FreeFormClipping."::: | F408 | :::no-loc text="FreeFormClipping"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F413.png" alt-text="Screenshot of CopyTo."::: | F413 | :::no-loc text="CopyTo"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F439.png" alt-text="Screenshot of DynamicLock."::: | F439 | :::no-loc text="DynamicLock"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F45E.png" alt-text="Screenshot of PenTips."::: | F45E | :::no-loc text="PenTips"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F45F.png" alt-text="Screenshot of PenTipsMirrored."::: | F45F | :::no-loc text="PenTipsMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F460.png" alt-text="Screenshot of HWPJoin."::: | F460 | :::no-loc text="HWPJoin"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F461.png" alt-text="Screenshot of HWPInsert."::: | F461 | :::no-loc text="HWPInsert"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F462.png" alt-text="Screenshot of HWPStrikeThrough."::: | F462 | :::no-loc text="HWPStrikeThrough"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F463.png" alt-text="Screenshot of HWPScratchOut."::: | F463 | :::no-loc text="HWPScratchOut"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F464.png" alt-text="Screenshot of HWPSplit."::: | F464 | :::no-loc text="HWPSplit"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F465.png" alt-text="Screenshot of HWPNewLine."::: | F465 | :::no-loc text="HWPNewLine"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F466.png" alt-text="Screenshot of HWPOverwrite."::: | F466 | :::no-loc text="HWPOverwrite"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F473.png" alt-text="Screenshot of MobWifiWarning1."::: | F473 | :::no-loc text="MobWifiWarning1"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F474.png" alt-text="Screenshot of MobWifiWarning2."::: | F474 | :::no-loc text="MobWifiWarning2"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F475.png" alt-text="Screenshot of MobWifiWarning3."::: | F475 | :::no-loc text="MobWifiWarning3"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F476.png" alt-text="Screenshot of MobWifiWarning4."::: | F476 | :::no-loc text="MobWifiWarning4"::: |
 | :::image type="content" border="false" source="images/segoe-mdl/F49A.png" alt-text="Screenshot of Globe2."::: | F49A | :::no-loc text="Globe2"::: |
 | :::image type="content" border="false" source="images/segoe-mdl/F4A5.png" alt-text="Screenshot of SpecialEffectSize."::: | F4A5 | :::no-loc text="SpecialEffectSize"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F4A9.png" alt-text="Screenshot of GIF."::: | F4A9 | :::no-loc text="GIF"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F4AA.png" alt-text="Screenshot of Sticker2."::: | F4AA | :::no-loc text="Sticker2"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F4BE.png" alt-text="Screenshot of SurfaceHubSelected."::: | F4BE | :::no-loc text="SurfaceHubSelected"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F4BF.png" alt-text="Screenshot of HoloLensSelected."::: | F4BF | :::no-loc text="HoloLensSelected"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F4C0.png" alt-text="Screenshot of Earbud."::: | F4C0 | :::no-loc text="Earbud"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F4C3.png" alt-text="Screenshot of MixVolumes."::: | F4C3 | :::no-loc text="MixVolumes"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F540.png" alt-text="Screenshot of Safe."::: | F540 | :::no-loc text="Safe"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F552.png" alt-text="Screenshot of LaptopSecure."::: | F552 | :::no-loc text="LaptopSecure"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F56D.png" alt-text="Screenshot of PrintDefault."::: | F56D | :::no-loc text="PrintDefault"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F56E.png" alt-text="Screenshot of PageMirrored."::: | F56E | :::no-loc text="PageMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F56F.png" alt-text="Screenshot of LandscapeOrientationMirrored."::: | F56F | :::no-loc text="LandscapeOrientationMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F570.png" alt-text="Screenshot of ColorOff."::: | F570 | :::no-loc text="ColorOff"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F571.png" alt-text="Screenshot of PrintAllPages."::: | F571 | :::no-loc text="PrintAllPages"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F572.png" alt-text="Screenshot of PrintCustomRange."::: | F572 | :::no-loc text="PrintCustomRange"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F573.png" alt-text="Screenshot of PageMarginPortraitNarrow."::: | F573 | :::no-loc text="PageMarginPortraitNarrow"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F574.png" alt-text="Screenshot of PageMarginPortraitNormal."::: | F574 | :::no-loc text="PageMarginPortraitNormal"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F575.png" alt-text="Screenshot of PageMarginPortraitModerate."::: | F575 | :::no-loc text="PageMarginPortraitModerate"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F576.png" alt-text="Screenshot of PageMarginPortraitWide."::: | F576 | :::no-loc text="PageMarginPortraitWide"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F577.png" alt-text="Screenshot of PageMarginLandscapeNarrow."::: | F577 | :::no-loc text="PageMarginLandscapeNarrow"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F578.png" alt-text="Screenshot of PageMarginLandscapeNormal."::: | F578 | :::no-loc text="PageMarginLandscapeNormal"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F579.png" alt-text="Screenshot of PageMarginLandscapeModerate."::: | F579 | :::no-loc text="PageMarginLandscapeModerate"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F57A.png" alt-text="Screenshot of PageMarginLandscapeWide."::: | F57A | :::no-loc text="PageMarginLandscapeWide"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F57B.png" alt-text="Screenshot of CollateLandscape."::: | F57B | :::no-loc text="CollateLandscape"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F57C.png" alt-text="Screenshot of CollatePortrait."::: | F57C | :::no-loc text="CollatePortrait"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F57D.png" alt-text="Screenshot of CollatePortraitSeparated."::: | F57D | :::no-loc text="CollatePortraitSeparated"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F57E.png" alt-text="Screenshot of DuplexLandscapeOneSided."::: | F57E | :::no-loc text="DuplexLandscapeOneSided"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F57F.png" alt-text="Screenshot of DuplexLandscapeOneSidedMirrored."::: | F57F | :::no-loc text="DuplexLandscapeOneSidedMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F580.png" alt-text="Screenshot of DuplexLandscapeTwoSidedLongEdge."::: | F580 | :::no-loc text="DuplexLandscapeTwoSidedLongEdge"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F581.png" alt-text="Screenshot of DuplexLandscapeTwoSidedLongEdgeMirrored."::: | F581 | :::no-loc text="DuplexLandscapeTwoSidedLongEdgeMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F582.png" alt-text="Screenshot of DuplexLandscapeTwoSidedShortEdge."::: | F582 | :::no-loc text="DuplexLandscapeTwoSidedShortEdge"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F583.png" alt-text="Screenshot of DuplexLandscapeTwoSidedShortEdgeMirrored."::: | F583 | :::no-loc text="DuplexLandscapeTwoSidedShortEdgeMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F584.png" alt-text="Screenshot of DuplexPortraitOneSided."::: | F584 | :::no-loc text="DuplexPortraitOneSided"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F585.png" alt-text="Screenshot of DuplexPortraitOneSidedMirrored."::: | F585 | :::no-loc text="DuplexPortraitOneSidedMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F586.png" alt-text="Screenshot of DuplexPortraitTwoSidedLongEdge."::: | F586 | :::no-loc text="DuplexPortraitTwoSidedLongEdge"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F587.png" alt-text="Screenshot of DuplexPortraitTwoSidedLongEdgeMirrored."::: | F587 | :::no-loc text="DuplexPortraitTwoSidedLongEdgeMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F588.png" alt-text="Screenshot of DuplexPortraitTwoSidedShortEdge."::: | F588 | :::no-loc text="DuplexPortraitTwoSidedShortEdge"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F589.png" alt-text="Screenshot of DuplexPortraitTwoSidedShortEdgeMirrored."::: | F589 | :::no-loc text="DuplexPortraitTwoSidedShortEdgeMirrored"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F58A.png" alt-text="Screenshot of PPSOneLandscape."::: | F58A | :::no-loc text="PPSOneLandscape"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F58B.png" alt-text="Screenshot of PPSTwoLandscape."::: | F58B | :::no-loc text="PPSTwoLandscape"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F58C.png" alt-text="Screenshot of PPSTwoPortrait."::: | F58C | :::no-loc text="PPSTwoPortrait"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F58D.png" alt-text="Screenshot of PPSFourLandscape."::: | F58D | :::no-loc text="PPSFourLandscape"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F58E.png" alt-text="Screenshot of PPSFourPortrait."::: | F58E | :::no-loc text="PPSFourPortrait"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F58F.png" alt-text="Screenshot of HolePunchOff."::: | F58F | :::no-loc text="HolePunchOff"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F590.png" alt-text="Screenshot of HolePunchPortraitLeft."::: | F590 | :::no-loc text="HolePunchPortraitLeft"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F591.png" alt-text="Screenshot of HolePunchPortraitRight."::: | F591 | :::no-loc text="HolePunchPortraitRight"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F592.png" alt-text="Screenshot of HolePunchPortraitTop."::: | F592 | :::no-loc text="HolePunchPortraitTop"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F593.png" alt-text="Screenshot of HolePunchPortraitBottom."::: | F593 | :::no-loc text="HolePunchPortraitBottom"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F594.png" alt-text="Screenshot of HolePunchLandscapeLeft."::: | F594 | :::no-loc text="HolePunchLandscapeLeft"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F595.png" alt-text="Screenshot of HolePunchLandscapeRight."::: | F595 | :::no-loc text="HolePunchLandscapeRight"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F596.png" alt-text="Screenshot of HolePunchLandscapeTop."::: | F596 | :::no-loc text="HolePunchLandscapeTop"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F597.png" alt-text="Screenshot of HolePunchLandscapeBottom."::: | F597 | :::no-loc text="HolePunchLandscapeBottom"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F598.png" alt-text="Screenshot of StaplingOff."::: | F598 | :::no-loc text="StaplingOff"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F599.png" alt-text="Screenshot of StaplingPortraitTopLeft."::: | F599 | :::no-loc text="StaplingPortraitTopLeft"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F59A.png" alt-text="Screenshot of StaplingPortraitTopRight."::: | F59A | :::no-loc text="StaplingPortraitTopRight"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F59B.png" alt-text="Screenshot of StaplingPortraitBottomRight."::: | F59B | :::no-loc text="StaplingPortraitBottomRight"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F59C.png" alt-text="Screenshot of StaplingPortraitTwoLeft."::: | F59C | :::no-loc text="StaplingPortraitTwoLeft"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F59D.png" alt-text="Screenshot of StaplingPortraitTwoRight."::: | F59D | :::no-loc text="StaplingPortraitTwoRight"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F59E.png" alt-text="Screenshot of StaplingPortraitTwoTop."::: | F59E | :::no-loc text="StaplingPortraitTwoTop"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F59F.png" alt-text="Screenshot of StaplingPortraitTwoBottom."::: | F59F | :::no-loc text="StaplingPortraitTwoBottom"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F5A0.png" alt-text="Screenshot of StaplingPortraitBookBinding."::: | F5A0 | :::no-loc text="StaplingPortraitBookBinding"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F5A1.png" alt-text="Screenshot of StaplingLandscapeTopLeft."::: | F5A1 | :::no-loc text="StaplingLandscapeTopLeft"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F5A2.png" alt-text="Screenshot of StaplingLandscapeTopRight."::: | F5A2 | :::no-loc text="StaplingLandscapeTopRight"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F5A3.png" alt-text="Screenshot of StaplingLandscapeBottomLeft."::: | F5A3 | :::no-loc text="StaplingLandscapeBottomLeft"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F5A4.png" alt-text="Screenshot of StaplingLandscapeBottomRight."::: | F5A4 | :::no-loc text="StaplingLandscapeBottomRight"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F5A5.png" alt-text="Screenshot of StaplingLandscapeTwoLeft."::: | F5A5 | :::no-loc text="StaplingLandscapeTwoLeft"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F5A6.png" alt-text="Screenshot of StaplingLandscapeTwoRight."::: | F5A6 | :::no-loc text="StaplingLandscapeTwoRight"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F5A7.png" alt-text="Screenshot of StaplingLandscapeTwoTop."::: | F5A7 | :::no-loc text="StaplingLandscapeTwoTop"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F5A8.png" alt-text="Screenshot of StaplingLandscapeTwoBottom."::: | F5A8 | :::no-loc text="StaplingLandscapeTwoBottom"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F5A9.png" alt-text="Screenshot of StaplingLandscapeBookBinding."::: | F5A9 | :::no-loc text="StaplingLandscapeBookBinding"::: |
 | :::image type="content" border="false" source="images/segoe-mdl/F5AA.png" alt-text="Screenshot of StatusDataTransferRoaming."::: | F5AA | :::no-loc text="StatusDataTransferRoaming"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F5AB.png" alt-text="Screenshot of MobSIMError."::: | F5AB | :::no-loc text="MobSIMError"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F5AC.png" alt-text="Screenshot of CollateLandscapeSeparated."::: | F5AC | :::no-loc text="CollateLandscapeSeparated"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F5AD.png" alt-text="Screenshot of PPSOnePortrait."::: | F5AD | :::no-loc text="PPSOnePortrait"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F5AE.png" alt-text="Screenshot of StaplingPortraitBottomLeft."::: | F5AE | :::no-loc text="StaplingPortraitBottomLeft"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F5B0.png" alt-text="Screenshot of PlaySolid."::: | F5B0 | :::no-loc text="PlaySolid"::: |
 | :::image type="content" border="false" source="images/segoe-mdl/F5E7.png" alt-text="Screenshot of RepeatOff."::: | F5E7 | :::no-loc text="RepeatOff"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F5ED.png" alt-text="Screenshot of Set."::: | F5ED | :::no-loc text="Set"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F5EE.png" alt-text="Screenshot of SetSolid."::: | F5EE | :::no-loc text="SetSolid"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F5EF.png" alt-text="Screenshot of FuzzyReading."::: | F5EF | :::no-loc text="FuzzyReading"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F5F2.png" alt-text="Screenshot of VerticalBattery0."::: | F5F2 | :::no-loc text="VerticalBattery0"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F5F3.png" alt-text="Screenshot of VerticalBattery1."::: | F5F3 | :::no-loc text="VerticalBattery1"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F5F4.png" alt-text="Screenshot of VerticalBattery2."::: | F5F4 | :::no-loc text="VerticalBattery2"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F5F5.png" alt-text="Screenshot of VerticalBattery3."::: | F5F5 | :::no-loc text="VerticalBattery3"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F5F6.png" alt-text="Screenshot of VerticalBattery4."::: | F5F6 | :::no-loc text="VerticalBattery4"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F5F7.png" alt-text="Screenshot of VerticalBattery5."::: | F5F7 | :::no-loc text="VerticalBattery5"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F5F8.png" alt-text="Screenshot of VerticalBattery6."::: | F5F8 | :::no-loc text="VerticalBattery6"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F5F9.png" alt-text="Screenshot of VerticalBattery7."::: | F5F9 | :::no-loc text="VerticalBattery7"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F5FA.png" alt-text="Screenshot of VerticalBattery8."::: | F5FA | :::no-loc text="VerticalBattery8"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F5FB.png" alt-text="Screenshot of VerticalBattery9."::: | F5FB | :::no-loc text="VerticalBattery9"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F5FC.png" alt-text="Screenshot of VerticalBattery10."::: | F5FC | :::no-loc text="VerticalBattery10"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F5FD.png" alt-text="Screenshot of VerticalBatteryCharging0."::: | F5FD | :::no-loc text="VerticalBatteryCharging0"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F5FE.png" alt-text="Screenshot of VerticalBatteryCharging1."::: | F5FE | :::no-loc text="VerticalBatteryCharging1"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F5FF.png" alt-text="Screenshot of VerticalBatteryCharging2."::: | F5FF | :::no-loc text="VerticalBatteryCharging2"::: |

### PUA F600-F800

The following table of glyphs displays unicode points prefixed from F6-  to F8-.

[Back to top](#icon-list)

| Glyph | Unicode point | Description |
|-------|---------------|-------------|
| :::image type="content" border="false" source="images/segoe-mdl/F600.png" alt-text="Screenshot of VerticalBatteryCharging3."::: | F600 | :::no-loc text="VerticalBatteryCharging3"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F601.png" alt-text="Screenshot of VerticalBatteryCharging4."::: | F601 | :::no-loc text="VerticalBatteryCharging4"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F602.png" alt-text="Screenshot of VerticalBatteryCharging5."::: | F602 | :::no-loc text="VerticalBatteryCharging5"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F603.png" alt-text="Screenshot of VerticalBatteryCharging6."::: | F603 | :::no-loc text="VerticalBatteryCharging6"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F604.png" alt-text="Screenshot of VerticalBatteryCharging7."::: | F604 | :::no-loc text="VerticalBatteryCharging7"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F605.png" alt-text="Screenshot of VerticalBatteryCharging8."::: | F605 | :::no-loc text="VerticalBatteryCharging8"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F606.png" alt-text="Screenshot of VerticalBatteryCharging9."::: | F606 | :::no-loc text="VerticalBatteryCharging9"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F607.png" alt-text="Screenshot of VerticalBatteryCharging10."::: | F607 | :::no-loc text="VerticalBatteryCharging10"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F608.png" alt-text="Screenshot of VerticalBatteryUnknown."::: | F608 | :::no-loc text="VerticalBatteryUnknown"::: |
 | :::image type="content" border="false" source="images/segoe-mdl/F614.png" alt-text="Screenshot of DoublePortrait."::: | F614 | :::no-loc text="DoublePortrait"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F615.png" alt-text="Screenshot of DoubleLandscape."::: | F615 | :::no-loc text="DoubleLandscape"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F616.png" alt-text="Screenshot of SinglePortrait."::: | F616 | :::no-loc text="SinglePortrait"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F617.png" alt-text="Screenshot of SingleLandscape."::: | F617 | :::no-loc text="SingleLandscape"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F618.png" alt-text="Screenshot of SIMError."::: | F618 | :::no-loc text="SIMError"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F619.png" alt-text="Screenshot of SIMMissing."::: | F619 | :::no-loc text="SIMMissing"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F61A.png" alt-text="Screenshot of SIMLock."::: | F61A | :::no-loc text="SIMLock"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F61B.png" alt-text="Screenshot of eSIM."::: | F61B | :::no-loc text="eSIM"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F61C.png" alt-text="Screenshot of eSIMNoProfile."::: | F61C | :::no-loc text="eSIMNoProfile"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F61D.png" alt-text="Screenshot of eSIMLocked."::: | F61D | :::no-loc text="eSIMLocked"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F61E.png" alt-text="Screenshot of eSIMBusy."::: | F61E | :::no-loc text="eSIMBusy"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F61F.png" alt-text="Screenshot of NoiseCancelation."::: | F61F | :::no-loc text="NoiseCancelation"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F620.png" alt-text="Screenshot of NoiseCancelationOff."::: | F620 | :::no-loc text="NoiseCancelationOff"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F623.png" alt-text="Screenshot of MusicSharing."::: | F623 | :::no-loc text="MusicSharing"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F624.png" alt-text="Screenshot of MusicSharingOff."::: | F624 | :::no-loc text="MusicSharingOff"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F63C.png" alt-text="Screenshot of CircleShapeSolid."::: | F63C | :::no-loc text="CircleShapeSolid"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F657.png" alt-text="Screenshot of F657 WifiCallBars."::: | F657 | :::no-loc text="WifiCallBars"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F658.png" alt-text="Screenshot of F658 WifiCall0."::: | F658 | :::no-loc text="WifiCall0"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F659.png" alt-text="Screenshot of F659 WifiCall1."::: | F659 | :::no-loc text="WifiCall1"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F65A.png" alt-text="Screenshot of F65A WifiCall2."::: | F65A | :::no-loc text="WifiCall2"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F65B.png" alt-text="Screenshot of F65B WifiCall3."::: | F65B | :::no-loc text="WifiCall3"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F65C.png" alt-text="Screenshot of F65C WifiCall4."::: | F65C | :::no-loc text="WifiCall4"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F69E.png" alt-text="Screenshot of CHTLanguageBar."::: | F69E | :::no-loc text="CHTLanguageBar"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F6A9.png" alt-text="Screenshot of ComposeMode."::: | F6A9 | :::no-loc text="ComposeMode"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F6B8.png" alt-text="Screenshot of ExpressiveInputEntry."::: | F6B8 | :::no-loc text="ExpressiveInputEntry"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F6BA.png" alt-text="Screenshot of EmojiTabMoreSymbols."::: | F6BA | :::no-loc text="EmojiTabMoreSymbols"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F6FA.png" alt-text="Screenshot of WebSearch."::: | F6FA | :::no-loc text="WebSearch"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F712.png" alt-text="Screenshot of Kiosk."::: | F712 | :::no-loc text="Kiosk"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F714.png" alt-text="Screenshot of RTTLogo."::: | F714 | :::no-loc text="RTTLogo"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F715.png" alt-text="Screenshot of VoiceCall."::: | F715 | :::no-loc text="VoiceCall"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F716.png" alt-text="Screenshot of GoToMessage."::: | F716 | :::no-loc text="GoToMessage"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F71A.png" alt-text="Screenshot of ReturnToCall."::: | F71A | :::no-loc text="ReturnToCall"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F71C.png" alt-text="Screenshot of StartPresenting."::: | F71C | :::no-loc text="StartPresenting"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F71D.png" alt-text="Screenshot of StopPresenting."::: | F71D | :::no-loc text="StopPresenting"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F71E.png" alt-text="Screenshot of ProductivityMode."::: | F71E | :::no-loc text="ProductivityMode"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F738.png" alt-text="Screenshot of SetHistoryStatus."::: | F738 | :::no-loc text="SetHistoryStatus"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F739.png" alt-text="Screenshot of SetHistoryStatus2."::: | F739 | :::no-loc text="SetHistoryStatus2"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F73D.png" alt-text="Screenshot of Keyboardsettings20."::: | F73D | :::no-loc text="Keyboardsettings20"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F73E.png" alt-text="Screenshot of OneHandedRight20."::: | F73E | :::no-loc text="OneHandedRight20"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F73F.png" alt-text="Screenshot of OneHandedLeft20."::: | F73F | :::no-loc text="OneHandedLeft20"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F740.png" alt-text="Screenshot of Split20."::: | F740 | :::no-loc text="Split20"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F741.png" alt-text="Screenshot of Full20."::: | F741 | :::no-loc text="Full20"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F742.png" alt-text="Screenshot of Handwriting20."::: | F742 | :::no-loc text="Handwriting20"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F743.png" alt-text="Screenshot of CheveronLeft20."::: | F743 | :::no-loc text="CheveronLeft20"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F744.png" alt-text="Screenshot of CheveronLeft32."::: | F744 | :::no-loc text="CheveronLeft32"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F745.png" alt-text="Screenshot of CheveronRight20."::: | F745 | :::no-loc text="CheveronRight20"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F746.png" alt-text="Screenshot of CheveronRight32."::: | F746 | :::no-loc text="CheveronRight32"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F781.png" alt-text="Screenshot of MicOff2."::: | F781 | :::no-loc text="MicOff2"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F785.png" alt-text="Screenshot of DeliveryOptimization."::: | F785 | :::no-loc text="DeliveryOptimization"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F78A.png" alt-text="Screenshot of CancelMedium."::: | F78A | :::no-loc text="CancelMedium"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F78B.png" alt-text="Screenshot of SearchMedium."::: | F78B | :::no-loc text="SearchMedium"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F78C.png" alt-text="Screenshot of AcceptMedium."::: | F78C | :::no-loc text="AcceptMedium"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F78D.png" alt-text="Screenshot of RevealPasswordMedium."::: | F78D | :::no-loc text="RevealPasswordMedium"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F7AD.png" alt-text="Screenshot of DeleteWord."::: | F7AD | :::no-loc text="DeleteWord"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F7AE.png" alt-text="Screenshot of DeleteWordFill."::: | F7AE | :::no-loc text="DeleteWordFill"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F7AF.png" alt-text="Screenshot of DeleteLines."::: | F7AF | :::no-loc text="DeleteLines"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F7B0.png" alt-text="Screenshot of DeleteLinesFill."::: | F7B0 | :::no-loc text="DeleteLinesFill"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F7B1.png" alt-text="Screenshot of InstertWords."::: | F7B1 | :::no-loc text="InstertWords"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F7B2.png" alt-text="Screenshot of InstertWordsFill."::: | F7B2 | :::no-loc text="InstertWordsFill"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F7B3.png" alt-text="Screenshot of JoinWords."::: | F7B3 | :::no-loc text="JoinWords"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F7B4.png" alt-text="Screenshot of JoinWordsFill."::: | F7B4 | :::no-loc text="JoinWordsFill"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F7B5.png" alt-text="Screenshot of OverwriteWords."::: | F7B5 | :::no-loc text="OverwriteWords"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F7B6.png" alt-text="Screenshot of OverwriteWordsFill."::: | F7B6 | :::no-loc text="OverwriteWordsFill"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F7B7.png" alt-text="Screenshot of AddNewLine."::: | F7B7 | :::no-loc text="AddNewLine"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F7B8.png" alt-text="Screenshot of AddNewLineFill."::: | F7B8 | :::no-loc text="AddNewLineFill"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F7B9.png" alt-text="Screenshot of OverwriteWordsKorean."::: | F7B9 | :::no-loc text="OverwriteWordsKorean"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F7BA.png" alt-text="Screenshot of OverwriteWordsFillKorean."::: | F7BA | :::no-loc text="OverwriteWordsFillKorean"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F7BB.png" alt-text="Screenshot of EducationIcon."::: | F7BB | :::no-loc text="EducationIcon"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F7ED.png" alt-text="Screenshot of WindowSnipping."::: | F7ED | :::no-loc text="WindowSnipping"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F7EE.png" alt-text="Screenshot of VideoCapture."::: | F7EE | :::no-loc text="VideoCapture"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F809.png" alt-text="Screenshot of StatusSecured."::: | F809 | :::no-loc text="StatusSecured"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F83B.png" alt-text="Screenshot of NarratorApp."::: | F83B | :::no-loc text="NarratorApp"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F83D.png" alt-text="Screenshot of PowerButtonUpdate."::: | F83D | :::no-loc text="PowerButtonUpdate"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F83E.png" alt-text="Screenshot of RestartUpdate."::: | F83E | :::no-loc text="RestartUpdate"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F83F.png" alt-text="Screenshot of UpdateStatusDot."::: | F83F | :::no-loc text="UpdateStatusDot"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F847.png" alt-text="Screenshot of Eject."::: | F847 | :::no-loc text="Eject"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F87B.png" alt-text="Screenshot of Spelling."::: | F87B | :::no-loc text="Spelling"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F87C.png" alt-text="Screenshot of SpellingKorean."::: | F87C | :::no-loc text="SpellingKorean"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F87D.png" alt-text="Screenshot of SpellingSerbian."::: | F87D | :::no-loc text="SpellingSerbian"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F87E.png" alt-text="Screenshot of SpellingChinese."::: | F87E | :::no-loc text="SpellingChinese"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F89A.png" alt-text="Screenshot of FolderSelect."::: | F89A | :::no-loc text="FolderSelect"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F8A5.png" alt-text="Screenshot of SmartScreen."::: | F8A5 | :::no-loc text="SmartScreen"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F8A6.png" alt-text="Screenshot of ExploitProtection."::: | F8A6 | :::no-loc text="ExploitProtection"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F8AA.png" alt-text="Screenshot of AddBold."::: | F8AA | :::no-loc text="AddBold"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F8AB.png" alt-text="Screenshot of SubtractBold."::: | F8AB | :::no-loc text="SubtractBold"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F8AC.png" alt-text="Screenshot of BackSolidBold."::: | F8AC | :::no-loc text="BackSolidBold"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F8AD.png" alt-text="Screenshot of ForwardSolidBold."::: | F8AD | :::no-loc text="ForwardSolidBold"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F8AE.png" alt-text="Screenshot of PauseBold."::: | F8AE | :::no-loc text="PauseBold"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F8AF.png" alt-text="Screenshot of ClickSolid."::: | F8AF | :::no-loc text="ClickSolid"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F8B0.png" alt-text="Screenshot of SettingsSolid."::: | F8B0 | :::no-loc text="SettingsSolid"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F8B1.png" alt-text="Screenshot of MicrophoneSolidBold."::: | F8B1 | :::no-loc text="MicrophoneSolidBold"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F8B2.png" alt-text="Screenshot of SpeechSolidBold."::: | F8B2 | :::no-loc text="SpeechSolidBold"::: |
| :::image type="content" border="false" source="images/segoe-mdl/F8B3.png" alt-text="Screenshot of ClickedOutLoudSolidBold."::: | F8B3 | :::no-loc text="ClickedOutLoudSolidBold"::: |

## Related articles

* [Guidelines for icons](../style/icons.md)
* [Symbol enumeration](/uwp/api/Windows.UI.Xaml.Controls.Symbol)
* [FontIcon class](/uwp/api/windows.ui.xaml.controls.fonticon)
