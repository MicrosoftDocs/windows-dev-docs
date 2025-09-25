---
description: This article lists and provides usage guidance for the glyphs that come with the Segoe Fluent Icons font.
title: Segoe Fluent Icons font
label: Segoe Fluent Icons font
ms.date: 09/02/2025
ms.topic: article
keywords: windows 10, windows 11
ms.localizationpriority: medium
---

# Segoe Fluent Icons font

This article provides developer guidelines for using the Segoe Fluent Icons font and lists each icon along with its Unicode value and descriptive name.

**Important APIs**:

* [**FontIcon class**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.fonticon)

## About Segoe Fluent Icons

> [!TIP]
> With the release of Windows 11, the **`Segoe Fluent Icons`** font replaced `Segoe MDL2 Assets` as the recommended symbol icon font. `Segoe MDL2 Assets` is still available, but we recommend updating your app to use the `Segoe Fluent Icons` font.

Most of the icons included in the `Segoe Fluent Icons` font are mapped to the Private Use Area of Unicode (PUA). The PUA range is a non-standardized range of Unicode that allows font developers to define their own characters. This is useful when creating a symbol font, but it creates an interoperability problem when `Segoe Fluent Icons` is not available.

Icons in the `Segoe Fluent Icons` font are not intended for use in-line with text. This means that some older "tricks" like the progressive disclosure arrows no longer apply. Likewise, since all of the new icons are sized and positioned the same, they do not have to be made with zero width; we have made sure they work as a set.

## Layering and mirroring

All glyphs in `Segoe Fluent Icons` have the same fixed width with a consistent height and left origin point, so layering and colorization effects can be achieved by drawing glyphs directly on top of each other. This example show a black outline drawn on top of the zero-width red heart.

:::image type="content" border="false" source="images/segoe-ui-symbol-layering.png" alt-text="Screenshot of using a zero-width glyph.":::

```xaml
<Grid>
    <FontIcon FontFamily="Segoe Fluent Icons" Glyph="&#xEB52;"
              Foreground="#C72335" />
    <FontIcon FontFamily="Segoe Fluent Icons" Glyph="&#xEB51;" />
</Grid>
```

Many of the icons also have mirrored forms available for use in languages that use right-to-left text directionality such as Arabic, Dari, Persian, and Hebrew.

## Using the icons

If you are developing an app in XAML, you can use specified glyphs from `Segoe Fluent Icons` with a [SymbolIcon](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.symbolicon) and the [Symbol enumeration](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.symbol).

```xaml
<SymbolIcon Symbol="GlobalNavigationButton"/>
```

If you would like to use a glyph from the `Segoe Fluent Icons` font that is not included in the Symbol enum, set it as the [Glyph](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.fonticon.glyph) property of a [**FontIcon**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.fonticon) control.

```xaml
<FontIcon FontFamily="Segoe Fluent Icons" Glyph="&#xE700;"/>
```

You can also use the static resource `SymbolThemeFontFamily` to access `Segoe Fluent Icons`, instead of specifying the font by name:

```xaml
<FontIcon FontFamily="{StaticResource SymbolThemeFontFamily}" Glyph="&#xE700;"/>
```

> [!NOTE]
> For optimal appearance, use these specific sizes: 16, 20, 24, 32, 40, 48, and 64. Deviating from these font sizes could lead to less crisp or blurry outcomes.

## How do I get this font?

* On Windows 11: There's nothing you need to do, the font comes with Windows.
* On Windows 10: `Segoe Fluent Icons` is not included by default on Windows 10. You can download it from the [Design resources](../downloads/index.md#fonts) page.
* On a Mac or other device: You can download `Segoe Fluent Icons` and other fonts from the [Design resources](../downloads/index.md#fonts) page. You can download the font for use in design and development, but you may not ship it to another platform.

## Examples

> [!div class="nextstepaction"]
> [Open the WinUI 3 Gallery app and see Iconography principles in action](winui3gallery:/item/Iconography)

[!INCLUDE [winui-3-gallery](../../../includes/winui-3-gallery.md)]

## Icon list

Please keep in mind that the `Segoe Fluent Icons` font includes many more icons than we can show here. Many of the icons are intended for specialized purposes and are not typically used anywhere else.

> [!NOTE]
> Glyphs with prefixes ranging from **E0-** to **E5-** (e.g. E001, E5B1) are currently marked as legacy and are therefore deprecated.

The following tables display all `Segoe Fluent Icons` glyphs and their respective unicode values and descriptive names. Select a range from the following list to view glyphs according to the PUA range they belong to.

* [PUA E700-E900](#pua-e700-e900)
* [PUA EA00-EC00](#pua-ea00-ec00)
* [PUA ED00-EF00](#pua-ed00-ef00)
* [PUA F000-F200](#pua-f000-f200)
* [PUA F300-F500](#pua-f300-f500)
* [PUA F600-F800](#pua-f600-f800)

### PUA E700-E900

The following table of glyphs displays unicode points prefixed from E7- to E9-.

[Back to top](#icon-list)

| Glyph | Unicode point | Description |
|-------|---------------|-------------|
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e700.png" alt-text="Screenshot of GlobalNavButton."::: | e700 | :::no-loc text="GlobalNavButton"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e701.png" alt-text="Screenshot of Wifi."::: | e701 | :::no-loc text="Wifi"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e702.png" alt-text="Screenshot of Bluetooth."::: | e702 | :::no-loc text="Bluetooth"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e703.png" alt-text="Screenshot of Connect."::: | e703 | :::no-loc text="Connect"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e704.png" alt-text="Screenshot of InternetSharing."::: | e704 | :::no-loc text="InternetSharing"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e705.png" alt-text="Screenshot of VPN."::: | e705 | :::no-loc text="VPN"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e706.png" alt-text="Screenshot of Brightness."::: | e706 | :::no-loc text="Brightness"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e707.png" alt-text="Screenshot of MapPin."::: | e707 | :::no-loc text="MapPin"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e708.png" alt-text="Screenshot of QuietHours."::: | e708 | :::no-loc text="QuietHours"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e709.png" alt-text="Screenshot of Airplane."::: | e709 | :::no-loc text="Airplane"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e70a.png" alt-text="Screenshot of Tablet."::: | e70a | :::no-loc text="Tablet"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e70b.png" alt-text="Screenshot of QuickNote."::: | e70b | :::no-loc text="QuickNote"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e70c.png" alt-text="Screenshot of RememberedDevice."::: | e70c | :::no-loc text="RememberedDevice"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e70d.png" alt-text="Screenshot of ChevronDown."::: | e70d | :::no-loc text="ChevronDown"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e70e.png" alt-text="Screenshot of ChevronUp."::: | e70e | :::no-loc text="ChevronUp"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e70f.png" alt-text="Screenshot of Edit."::: | e70f | :::no-loc text="Edit"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e710.png" alt-text="Screenshot of Add."::: | e710 | :::no-loc text="Add"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e711.png" alt-text="Screenshot of Cancel."::: | e711 | :::no-loc text="Cancel"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e712.png" alt-text="Screenshot of More."::: | e712 | :::no-loc text="More"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e713.png" alt-text="Screenshot of Settings."::: | e713 | :::no-loc text="Settings"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e714.png" alt-text="Screenshot of Video."::: | e714 | :::no-loc text="Video"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e715.png" alt-text="Screenshot of Mail."::: | e715 | :::no-loc text="Mail"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e716.png" alt-text="Screenshot of People."::: | e716 | :::no-loc text="People"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e717.png" alt-text="Screenshot of Phone."::: | e717 | :::no-loc text="Phone"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e718.png" alt-text="Screenshot of Pin."::: | e718 | :::no-loc text="Pin"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e719.png" alt-text="Screenshot of Shop."::: | e719 | :::no-loc text="Shop"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e71a.png" alt-text="Screenshot of Stop."::: | e71a | :::no-loc text="Stop"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e71b.png" alt-text="Screenshot of Link."::: | e71b | :::no-loc text="Link"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e71c.png" alt-text="Screenshot of Filter."::: | e71c | :::no-loc text="Filter"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e71d.png" alt-text="Screenshot of AllApps."::: | e71d | :::no-loc text="AllApps"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e71e.png" alt-text="Screenshot of Zoom."::: | e71e | :::no-loc text="Zoom"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e71f.png" alt-text="Screenshot of ZoomOut."::: | e71f | :::no-loc text="ZoomOut"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e720.png" alt-text="Screenshot of Microphone."::: | e720 | :::no-loc text="Microphone"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e721.png" alt-text="Screenshot of Search."::: | e721 | :::no-loc text="Search"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e722.png" alt-text="Screenshot of Camera."::: | e722 | :::no-loc text="Camera"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e723.png" alt-text="Screenshot of Attach."::: | e723 | :::no-loc text="Attach"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e724.png" alt-text="Screenshot of Send."::: | e724 | :::no-loc text="Send"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e725.png" alt-text="Screenshot of SendFill."::: | e725 | :::no-loc text="SendFill"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e726.png" alt-text="Screenshot of WalkSolid."::: | e726 | :::no-loc text="WalkSolid"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e727.png" alt-text="Screenshot of InPrivate."::: | e727 | :::no-loc text="InPrivate"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e728.png" alt-text="Screenshot of FavoriteList."::: | e728 | :::no-loc text="FavoriteList"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e729.png" alt-text="Screenshot of PageSolid."::: | e729 | :::no-loc text="PageSolid"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e72a.png" alt-text="Screenshot of Forward."::: | e72a | :::no-loc text="Forward"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e72b.png" alt-text="Screenshot of Back."::: | e72b | :::no-loc text="Back"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e72c.png" alt-text="Screenshot of Refresh."::: | e72c | :::no-loc text="Refresh"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e72d.png" alt-text="Screenshot of Share."::: | e72d | :::no-loc text="Share"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e72e.png" alt-text="Screenshot of Lock."::: | e72e | :::no-loc text="Lock"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e72f.png" alt-text="Screenshot of BlockedSite."::: | e72f | :::no-loc text="BlockedSite"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e730.png" alt-text="Screenshot of ReportHacked."::: | e730 | :::no-loc text="ReportHacked"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e731.png" alt-text="Screenshot of EMI."::: | e731 | :::no-loc text="EMI"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e733.png" alt-text="Screenshot of Blocked."::: | e733 | :::no-loc text="Blocked"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e734.png" alt-text="Screenshot of FavoriteStar."::: | e734 | :::no-loc text="FavoriteStar"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e735.png" alt-text="Screenshot of FavoriteStarFill."::: | e735 | :::no-loc text="FavoriteStarFill"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e736.png" alt-text="Screenshot of ReadingMode."::: | e736 | :::no-loc text="ReadingMode"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e737.png" alt-text="Screenshot of Favicon."::: | e737 | :::no-loc text="Favicon"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e738.png" alt-text="Screenshot of Remove."::: | e738 | :::no-loc text="Remove"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e739.png" alt-text="Screenshot of Checkbox."::: | e739 | :::no-loc text="Checkbox"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e73a.png" alt-text="Screenshot of CheckboxComposite."::: | e73a | :::no-loc text="CheckboxComposite"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e73b.png" alt-text="Screenshot of CheckboxFill."::: | e73b | :::no-loc text="CheckboxFill"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e73c.png" alt-text="Screenshot of CheckboxIndeterminate."::: | e73c | :::no-loc text="CheckboxIndeterminate"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e73d.png" alt-text="Screenshot of CheckboxCompositeReversed."::: | e73d | :::no-loc text="CheckboxCompositeReversed"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e73e.png" alt-text="Screenshot of CheckMark."::: | e73e | :::no-loc text="CheckMark"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e73f.png" alt-text="Screenshot of BackToWindow."::: | e73f | :::no-loc text="BackToWindow"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e740.png" alt-text="Screenshot of FullScreen."::: | e740 | :::no-loc text="FullScreen"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e741.png" alt-text="Screenshot of ResizeTouchLarger."::: | e741 | :::no-loc text="ResizeTouchLarger"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e742.png" alt-text="Screenshot of ResizeTouchSmaller."::: | e742 | :::no-loc text="ResizeTouchSmaller"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e743.png" alt-text="Screenshot of ResizeMouseSmall."::: | e743 | :::no-loc text="ResizeMouseSmall"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e744.png" alt-text="Screenshot of ResizeMouseMedium."::: | e744 | :::no-loc text="ResizeMouseMedium"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e745.png" alt-text="Screenshot of ResizeMouseWide."::: | e745 | :::no-loc text="ResizeMouseWide"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e746.png" alt-text="Screenshot of ResizeMouseTall."::: | e746 | :::no-loc text="ResizeMouseTall"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e747.png" alt-text="Screenshot of ResizeMouseLarge."::: | e747 | :::no-loc text="ResizeMouseLarge"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e748.png" alt-text="Screenshot of SwitchUser."::: | e748 | :::no-loc text="SwitchUser"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e749.png" alt-text="Screenshot of Print."::: | e749 | :::no-loc text="Print"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e74a.png" alt-text="Screenshot of Up."::: | e74a | :::no-loc text="Up"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e74b.png" alt-text="Screenshot of Down."::: | e74b | :::no-loc text="Down"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e74c.png" alt-text="Screenshot of OEM."::: | e74c | :::no-loc text="OEM"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e74d.png" alt-text="Screenshot of Delete."::: | e74d | :::no-loc text="Delete"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e74e.png" alt-text="Screenshot of Save."::: | e74e | :::no-loc text="Save"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e74f.png" alt-text="Screenshot of Mute."::: | e74f | :::no-loc text="Mute"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e750.png" alt-text="Screenshot of BackSpaceQWERTY."::: | e750 | :::no-loc text="BackSpaceQWERTY"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e751.png" alt-text="Screenshot of ReturnKey."::: | e751 | :::no-loc text="ReturnKey"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e752.png" alt-text="Screenshot of UpArrowShiftKey."::: | e752 | :::no-loc text="UpArrowShiftKey"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e753.png" alt-text="Screenshot of Cloud."::: | e753 | :::no-loc text="Cloud"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e754.png" alt-text="Screenshot of Flashlight."::: | e754 | :::no-loc text="Flashlight"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e755.png" alt-text="Screenshot of RotationLock."::: | e755 | :::no-loc text="RotationLock"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e756.png" alt-text="Screenshot of CommandPrompt."::: | e756 | :::no-loc text="CommandPrompt"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e759.png" alt-text="Screenshot of SIPMove."::: | e759 | :::no-loc text="SIPMove"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e75a.png" alt-text="Screenshot of SIPUndock."::: | e75a | :::no-loc text="SIPUndock"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e75b.png" alt-text="Screenshot of SIPRedock."::: | e75b | :::no-loc text="SIPRedock"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e75c.png" alt-text="Screenshot of EraseTool."::: | e75c | :::no-loc text="EraseTool"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e75d.png" alt-text="Screenshot of UnderscoreSpace."::: | e75d | :::no-loc text="UnderscoreSpace"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e75e.png" alt-text="Screenshot of GripperTool."::: | e75e | :::no-loc text="GripperTool"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e75f.png" alt-text="Screenshot of Dialpad."::: | e75f | :::no-loc text="Dialpad"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e760.png" alt-text="Screenshot of PageLeft."::: | e760 | :::no-loc text="PageLeft"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e761.png" alt-text="Screenshot of PageRight."::: | e761 | :::no-loc text="PageRight"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e762.png" alt-text="Screenshot of MultiSelect."::: | e762 | :::no-loc text="MultiSelect"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e763.png" alt-text="Screenshot of KeyboardLeftHanded."::: | e763 | :::no-loc text="KeyboardLeftHanded"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e764.png" alt-text="Screenshot of KeyboardRightHanded."::: | e764 | :::no-loc text="KeyboardRightHanded"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e765.png" alt-text="Screenshot of KeyboardClassic."::: | e765 | :::no-loc text="KeyboardClassic"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e766.png" alt-text="Screenshot of KeyboardSplit."::: | e766 | :::no-loc text="KeyboardSplit"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e767.png" alt-text="Screenshot of Volume."::: | e767 | :::no-loc text="Volume"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e768.png" alt-text="Screenshot of Play."::: | e768 | :::no-loc text="Play"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e769.png" alt-text="Screenshot of Pause."::: | e769 | :::no-loc text="Pause"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e76b.png" alt-text="Screenshot of ChevronLeft."::: | e76b | :::no-loc text="ChevronLeft"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e76c.png" alt-text="Screenshot of ChevronRight."::: | e76c | :::no-loc text="ChevronRight"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e76d.png" alt-text="Screenshot of InkingTool."::: | e76d | :::no-loc text="InkingTool"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e76e.png" alt-text="Screenshot of Emoji2."::: | e76e | :::no-loc text="Emoji2"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e76f.png" alt-text="Screenshot of GripperBarHorizontal."::: | e76f | :::no-loc text="GripperBarHorizontal"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e770.png" alt-text="Screenshot of System."::: | e770 | :::no-loc text="System"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e771.png" alt-text="Screenshot of Personalize."::: | e771 | :::no-loc text="Personalize"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e772.png" alt-text="Screenshot of Devices."::: | e772 | :::no-loc text="Devices"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e773.png" alt-text="Screenshot of SearchAndApps."::: | e773 | :::no-loc text="SearchAndApps"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e774.png" alt-text="Screenshot of Globe."::: | e774 | :::no-loc text="Globe"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e775.png" alt-text="Screenshot of TimeLanguage."::: | e775 | :::no-loc text="TimeLanguage"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e776.png" alt-text="Screenshot of EaseOfAccess."::: | e776 | :::no-loc text="EaseOfAccess"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e777.png" alt-text="Screenshot of UpdateRestore."::: | e777 | :::no-loc text="UpdateRestore"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e778.png" alt-text="Screenshot of HangUp."::: | e778 | :::no-loc text="HangUp"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e779.png" alt-text="Screenshot of ContactInfo."::: | e779 | :::no-loc text="ContactInfo"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e77a.png" alt-text="Screenshot of Unpin."::: | e77a | :::no-loc text="Unpin"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e77b.png" alt-text="Screenshot of Contact."::: | e77b | :::no-loc text="Contact"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e77c.png" alt-text="Screenshot of Memo."::: | e77c | :::no-loc text="Memo"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e77e.png" alt-text="Screenshot of IncomingCall."::: | e77e | :::no-loc text="IncomingCall"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e77f.png" alt-text="Screenshot of Paste."::: | e77f | :::no-loc text="Paste"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e780.png" alt-text="Screenshot of PhoneBook."::: | e780 | :::no-loc text="PhoneBook"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e781.png" alt-text="Screenshot of LEDLight."::: | e781 | :::no-loc text="LEDLight"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e783.png" alt-text="Screenshot of Error."::: | e783 | :::no-loc text="Error"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e784.png" alt-text="Screenshot of GripperBarVertical."::: | e784 | :::no-loc text="GripperBarVertical"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e785.png" alt-text="Screenshot of Unlock."::: | e785 | :::no-loc text="Unlock"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e786.png" alt-text="Screenshot of Slideshow."::: | e786 | :::no-loc text="Slideshow"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e787.png" alt-text="Screenshot of Calendar."::: | e787 | :::no-loc text="Calendar"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e788.png" alt-text="Screenshot of GripperResize."::: | e788 | :::no-loc text="GripperResize"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e789.png" alt-text="Screenshot of Megaphone."::: | e789 | :::no-loc text="Megaphone"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e78a.png" alt-text="Screenshot of Trim."::: | e78a | :::no-loc text="Trim"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e78b.png" alt-text="Screenshot of NewWindow."::: | e78b | :::no-loc text="NewWindow"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e78c.png" alt-text="Screenshot of SaveLocal."::: | e78c | :::no-loc text="SaveLocal"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e790.png" alt-text="Screenshot of Color."::: | e790 | :::no-loc text="Color"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e791.png" alt-text="Screenshot of DataSense."::: | e791 | :::no-loc text="DataSense"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e792.png" alt-text="Screenshot of SaveAs."::: | e792 | :::no-loc text="SaveAs"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e793.png" alt-text="Screenshot of Light."::: | e793 | :::no-loc text="Light"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e794.png" alt-text="Screenshot of Effects."::: | e794 | :::no-loc text="Effects"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e799.png" alt-text="Screenshot of AspectRatio."::: | e799 | :::no-loc text="AspectRatio"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e7a1.png" alt-text="Screenshot of Contrast."::: | e7a1 | :::no-loc text="Contrast"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e7a5.png" alt-text="Screenshot of DataSenseBar."::: | e7a5 | :::no-loc text="DataSenseBar"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e7a6.png" alt-text="Screenshot of Redo."::: | e7a6 | :::no-loc text="Redo"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e7a7.png" alt-text="Screenshot of Undo."::: | e7a7 | :::no-loc text="Undo"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e7a8.png" alt-text="Screenshot of Crop."::: | e7a8 | :::no-loc text="Crop"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e7aa.png" alt-text="Screenshot of PhotoCollection."::: | e7aa | :::no-loc text="PhotoCollection"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e7ac.png" alt-text="Screenshot of OpenWith."::: | e7ac | :::no-loc text="OpenWith"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e7ad.png" alt-text="Screenshot of Rotate."::: | e7ad | :::no-loc text="Rotate"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e7b3.png" alt-text="Screenshot of RedEye."::: | e7b3 | :::no-loc text="RedEye"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e7b5.png" alt-text="Screenshot of SetlockScreen."::: | e7b5 | :::no-loc text="SetlockScreen"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e7b7.png" alt-text="Screenshot of MapPin2."::: | e7b7 | :::no-loc text="MapPin2"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e7b8.png" alt-text="Screenshot of Package."::: | e7b8 | :::no-loc text="Package"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e7ba.png" alt-text="Screenshot of Warning."::: | e7ba | :::no-loc text="Warning"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e7bc.png" alt-text="Screenshot of ReadingList."::: | e7bc | :::no-loc text="ReadingList"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e7be.png" alt-text="Screenshot of Education."::: | e7be | :::no-loc text="Education"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e7bf.png" alt-text="Screenshot of ShoppingCart."::: | e7bf | :::no-loc text="ShoppingCart"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e7c0.png" alt-text="Screenshot of Train."::: | e7c0 | :::no-loc text="Train"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e7c1.png" alt-text="Screenshot of Flag."::: | e7c1 | :::no-loc text="Flag"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e7c2.png" alt-text="Screenshot of Move."::: | e7c2 | :::no-loc text="Move"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e7c3.png" alt-text="Screenshot of Page."::: | e7c3 | :::no-loc text="Page"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e7c4.png" alt-text="Screenshot of TaskView."::: | e7c4 | :::no-loc text="TaskView"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e7c5.png" alt-text="Screenshot of BrowsePhotos."::: | e7c5 | :::no-loc text="BrowsePhotos"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e7c6.png" alt-text="Screenshot of HalfStarLeft."::: | e7c6 | :::no-loc text="HalfStarLeft"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e7c7.png" alt-text="Screenshot of HalfStarRight."::: | e7c7 | :::no-loc text="HalfStarRight"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e7c8.png" alt-text="Screenshot of Record."::: | e7c8 | :::no-loc text="Record"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e7c9.png" alt-text="Screenshot of TouchPointer."::: | e7c9 | :::no-loc text="TouchPointer"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e7de.png" alt-text="Screenshot of LangJPN."::: | e7de | :::no-loc text="LangJPN"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e7e3.png" alt-text="Screenshot of Ferry."::: | e7e3 | :::no-loc text="Ferry"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e7e6.png" alt-text="Screenshot of Highlight."::: | e7e6 | :::no-loc text="Highlight"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e7e7.png" alt-text="Screenshot of ActionCenterNotification."::: | e7e7 | :::no-loc text="ActionCenterNotification"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e7e8.png" alt-text="Screenshot of PowerButton."::: | e7e8 | :::no-loc text="PowerButton"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e7ea.png" alt-text="Screenshot of ResizeTouchNarrower."::: | e7ea | :::no-loc text="ResizeTouchNarrower"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e7eb.png" alt-text="Screenshot of ResizeTouchShorter."::: | e7eb | :::no-loc text="ResizeTouchShorter"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e7ec.png" alt-text="Screenshot of DrivingMode."::: | e7ec | :::no-loc text="DrivingMode"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e7ed.png" alt-text="Screenshot of RingerSilent."::: | e7ed | :::no-loc text="RingerSilent"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e7ee.png" alt-text="Screenshot of OtherUser."::: | e7ee | :::no-loc text="OtherUser"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e7ef.png" alt-text="Screenshot of Admin."::: | e7ef | :::no-loc text="Admin"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e7f0.png" alt-text="Screenshot of CC."::: | e7f0 | :::no-loc text="CC"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e7f1.png" alt-text="Screenshot of SDCard."::: | e7f1 | :::no-loc text="SDCard"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e7f2.png" alt-text="Screenshot of CallForwarding."::: | e7f2 | :::no-loc text="CallForwarding"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e7f3.png" alt-text="Screenshot of SettingsDisplaySound."::: | e7f3 | :::no-loc text="SettingsDisplaySound"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e7f4.png" alt-text="Screenshot of TVMonitor."::: | e7f4 | :::no-loc text="TVMonitor"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e7f5.png" alt-text="Screenshot of Speakers."::: | e7f5 | :::no-loc text="Speakers"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e7f6.png" alt-text="Screenshot of Headphone."::: | e7f6 | :::no-loc text="Headphone"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e7f7.png" alt-text="Screenshot of DeviceLaptopPic."::: | e7f7 | :::no-loc text="DeviceLaptopPic"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e7f8.png" alt-text="Screenshot of DeviceLaptopNoPic."::: | e7f8 | :::no-loc text="DeviceLaptopNoPic"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e7f9.png" alt-text="Screenshot of DeviceMonitorRightPic."::: | e7f9 | :::no-loc text="DeviceMonitorRightPic"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e7fa.png" alt-text="Screenshot of DeviceMonitorLeftPic."::: | e7fa | :::no-loc text="DeviceMonitorLeftPic"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e7fb.png" alt-text="Screenshot of DeviceMonitorNoPic."::: | e7fb | :::no-loc text="DeviceMonitorNoPic"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e7fc.png" alt-text="Screenshot of Game."::: | e7fc | :::no-loc text="Game"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e7fd.png" alt-text="Screenshot of HorizontalTabKey."::: | e7fd | :::no-loc text="HorizontalTabKey"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e802.png" alt-text="Screenshot of StreetsideSplitMinimize."::: | e802 | :::no-loc text="StreetsideSplitMinimize"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e803.png" alt-text="Screenshot of StreetsideSplitExpand."::: | e803 | :::no-loc text="StreetsideSplitExpand"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e804.png" alt-text="Screenshot of Car."::: | e804 | :::no-loc text="Car"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e805.png" alt-text="Screenshot of Walk."::: | e805 | :::no-loc text="Walk"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e806.png" alt-text="Screenshot of Bus."::: | e806 | :::no-loc text="Bus"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e809.png" alt-text="Screenshot of TiltUp."::: | e809 | :::no-loc text="TiltUp"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e80a.png" alt-text="Screenshot of TiltDown."::: | e80a | :::no-loc text="TiltDown"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e80b.png" alt-text="Screenshot of CallControl."::: | e80b | :::no-loc text="CallControl"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e80c.png" alt-text="Screenshot of RotateMapRight."::: | e80c | :::no-loc text="RotateMapRight"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e80d.png" alt-text="Screenshot of RotateMapLeft."::: | e80d | :::no-loc text="RotateMapLeft"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e80f.png" alt-text="Screenshot of Home."::: | e80f | :::no-loc text="Home"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e811.png" alt-text="Screenshot of ParkingLocation."::: | e811 | :::no-loc text="ParkingLocation"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e812.png" alt-text="Screenshot of MapCompassTop."::: | e812 | :::no-loc text="MapCompassTop"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e813.png" alt-text="Screenshot of MapCompassBottom."::: | e813 | :::no-loc text="MapCompassBottom"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e814.png" alt-text="Screenshot of IncidentTriangle."::: | e814 | :::no-loc text="IncidentTriangle"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e815.png" alt-text="Screenshot of Touch."::: | e815 | :::no-loc text="Touch"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e816.png" alt-text="Screenshot of MapDirections."::: | e816 | :::no-loc text="MapDirections"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e819.png" alt-text="Screenshot of StartPoint."::: | e819 | :::no-loc text="StartPoint"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e81a.png" alt-text="Screenshot of StopPoint."::: | e81a | :::no-loc text="StopPoint"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e81b.png" alt-text="Screenshot of EndPoint."::: | e81b | :::no-loc text="EndPoint"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e81c.png" alt-text="Screenshot of History."::: | e81c | :::no-loc text="History"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e81d.png" alt-text="Screenshot of Location."::: | e81d | :::no-loc text="Location"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e81e.png" alt-text="Screenshot of MapLayers."::: | e81e | :::no-loc text="MapLayers"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e81f.png" alt-text="Screenshot of Accident."::: | e81f | :::no-loc text="Accident"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e821.png" alt-text="Screenshot of Work."::: | e821 | :::no-loc text="Work"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e822.png" alt-text="Screenshot of Construction."::: | e822 | :::no-loc text="Construction"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e823.png" alt-text="Screenshot of Recent."::: | e823 | :::no-loc text="Recent"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e825.png" alt-text="Screenshot of Bank."::: | e825 | :::no-loc text="Bank"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e826.png" alt-text="Screenshot of DownloadMap."::: | e826 | :::no-loc text="DownloadMap"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e829.png" alt-text="Screenshot of InkingToolFill2."::: | e829 | :::no-loc text="InkingToolFill2"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e82a.png" alt-text="Screenshot of HighlightFill2."::: | e82a | :::no-loc text="HighlightFill2"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e82b.png" alt-text="Screenshot of EraseToolFill."::: | e82b | :::no-loc text="EraseToolFill"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e82c.png" alt-text="Screenshot of EraseToolFill2."::: | e82c | :::no-loc text="EraseToolFill2"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e82d.png" alt-text="Screenshot of Dictionary."::: | e82d | :::no-loc text="Dictionary"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e82e.png" alt-text="Screenshot of DictionaryAdd."::: | e82e | :::no-loc text="DictionaryAdd"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e82f.png" alt-text="Screenshot of ToolTip."::: | e82f | :::no-loc text="ToolTip"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e830.png" alt-text="Screenshot of ChromeBack."::: | e830 | :::no-loc text="ChromeBack"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e835.png" alt-text="Screenshot of ProvisioningPackage."::: | e835 | :::no-loc text="ProvisioningPackage"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e836.png" alt-text="Screenshot of AddRemoteDevice."::: | e836 | :::no-loc text="AddRemoteDevice"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e838.png" alt-text="Screenshot of FolderOpen."::: | e838 | :::no-loc text="FolderOpen"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e839.png" alt-text="Screenshot of Ethernet."::: | e839 | :::no-loc text="Ethernet"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e83a.png" alt-text="Screenshot of ShareBroadband."::: | e83a | :::no-loc text="ShareBroadband"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e83b.png" alt-text="Screenshot of DirectAccess."::: | e83b | :::no-loc text="DirectAccess"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e83c.png" alt-text="Screenshot of DialUp."::: | e83c | :::no-loc text="DialUp"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e83d.png" alt-text="Screenshot of DefenderApp."::: | e83d | :::no-loc text="DefenderApp"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e83e.png" alt-text="Screenshot of BatteryCharging9."::: | e83e | :::no-loc text="BatteryCharging9"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e83f.png" alt-text="Screenshot of Battery10."::: | e83f | :::no-loc text="Battery10"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e840.png" alt-text="Screenshot of Pinned."::: | e840 | :::no-loc text="Pinned"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e841.png" alt-text="Screenshot of PinFill."::: | e841 | :::no-loc text="PinFill"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e842.png" alt-text="Screenshot of PinnedFill."::: | e842 | :::no-loc text="PinnedFill"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e843.png" alt-text="Screenshot of PeriodKey."::: | e843 | :::no-loc text="PeriodKey"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e844.png" alt-text="Screenshot of PuncKey."::: | e844 | :::no-loc text="PuncKey"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e845.png" alt-text="Screenshot of RevToggleKey."::: | e845 | :::no-loc text="RevToggleKey"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e846.png" alt-text="Screenshot of RightArrowKeyTime1."::: | e846 | :::no-loc text="RightArrowKeyTime1"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e847.png" alt-text="Screenshot of RightArrowKeyTime2."::: | e847 | :::no-loc text="RightArrowKeyTime2"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e848.png" alt-text="Screenshot of LeftQuote."::: | e848 | :::no-loc text="LeftQuote"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e849.png" alt-text="Screenshot of RightQuote."::: | e849 | :::no-loc text="RightQuote"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e84a.png" alt-text="Screenshot of DownShiftKey."::: | e84a | :::no-loc text="DownShiftKey"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e84b.png" alt-text="Screenshot of UpShiftKey."::: | e84b | :::no-loc text="UpShiftKey"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e84c.png" alt-text="Screenshot of PuncKey0."::: | e84c | :::no-loc text="PuncKey0"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e84d.png" alt-text="Screenshot of PuncKeyLeftBottom."::: | e84d | :::no-loc text="PuncKeyLeftBottom"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e84e.png" alt-text="Screenshot of RightArrowKeyTime3."::: | e84e | :::no-loc text="RightArrowKeyTime3"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e84f.png" alt-text="Screenshot of RightArrowKeyTime4."::: | e84f | :::no-loc text="RightArrowKeyTime4"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e850.png" alt-text="Screenshot of Battery0."::: | e850 | :::no-loc text="Battery0"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e851.png" alt-text="Screenshot of Battery1."::: | e851 | :::no-loc text="Battery1"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e852.png" alt-text="Screenshot of Battery2."::: | e852 | :::no-loc text="Battery2"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e853.png" alt-text="Screenshot of Battery3."::: | e853 | :::no-loc text="Battery3"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e854.png" alt-text="Screenshot of Battery4."::: | e854 | :::no-loc text="Battery4"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e855.png" alt-text="Screenshot of Battery5."::: | e855 | :::no-loc text="Battery5"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e856.png" alt-text="Screenshot of Battery6."::: | e856 | :::no-loc text="Battery6"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e857.png" alt-text="Screenshot of Battery7."::: | e857 | :::no-loc text="Battery7"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e858.png" alt-text="Screenshot of Battery8."::: | e858 | :::no-loc text="Battery8"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e859.png" alt-text="Screenshot of Battery9."::: | e859 | :::no-loc text="Battery9"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e85a.png" alt-text="Screenshot of BatteryCharging0."::: | e85a | :::no-loc text="BatteryCharging0"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e85b.png" alt-text="Screenshot of BatteryCharging1."::: | e85b | :::no-loc text="BatteryCharging1"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e85c.png" alt-text="Screenshot of BatteryCharging2."::: | e85c | :::no-loc text="BatteryCharging2"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e85d.png" alt-text="Screenshot of BatteryCharging3."::: | e85d | :::no-loc text="BatteryCharging3"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e85e.png" alt-text="Screenshot of BatteryCharging4."::: | e85e | :::no-loc text="BatteryCharging4"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e85f.png" alt-text="Screenshot of BatteryCharging5."::: | e85f | :::no-loc text="BatteryCharging5"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e860.png" alt-text="Screenshot of BatteryCharging6."::: | e860 | :::no-loc text="BatteryCharging6"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e861.png" alt-text="Screenshot of BatteryCharging7."::: | e861 | :::no-loc text="BatteryCharging7"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e862.png" alt-text="Screenshot of BatteryCharging8."::: | e862 | :::no-loc text="BatteryCharging8"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e863.png" alt-text="Screenshot of BatterySaver0."::: | e863 | :::no-loc text="BatterySaver0"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e864.png" alt-text="Screenshot of BatterySaver1."::: | e864 | :::no-loc text="BatterySaver1"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e865.png" alt-text="Screenshot of BatterySaver2."::: | e865 | :::no-loc text="BatterySaver2"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e866.png" alt-text="Screenshot of BatterySaver3."::: | e866 | :::no-loc text="BatterySaver3"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e867.png" alt-text="Screenshot of BatterySaver4."::: | e867 | :::no-loc text="BatterySaver4"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e868.png" alt-text="Screenshot of BatterySaver5."::: | e868 | :::no-loc text="BatterySaver5"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e869.png" alt-text="Screenshot of BatterySaver6."::: | e869 | :::no-loc text="BatterySaver6"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e86a.png" alt-text="Screenshot of BatterySaver7."::: | e86a | :::no-loc text="BatterySaver7"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e86b.png" alt-text="Screenshot of BatterySaver8."::: | e86b | :::no-loc text="BatterySaver8"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e86c.png" alt-text="Screenshot of SignalBars1."::: | e86c | :::no-loc text="SignalBars1"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e86d.png" alt-text="Screenshot of SignalBars2."::: | e86d | :::no-loc text="SignalBars2"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e86e.png" alt-text="Screenshot of SignalBars3."::: | e86e | :::no-loc text="SignalBars3"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e86f.png" alt-text="Screenshot of SignalBars4."::: | e86f | :::no-loc text="SignalBars4"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e870.png" alt-text="Screenshot of SignalBars5."::: | e870 | :::no-loc text="SignalBars5"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e871.png" alt-text="Screenshot of SignalNotConnected."::: | e871 | :::no-loc text="SignalNotConnected"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e872.png" alt-text="Screenshot of Wifi1."::: | e872 | :::no-loc text="Wifi1"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e873.png" alt-text="Screenshot of Wifi2."::: | e873 | :::no-loc text="Wifi2"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e874.png" alt-text="Screenshot of Wifi3."::: | e874 | :::no-loc text="Wifi3"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e875.png" alt-text="Screenshot of MobSIMLock."::: | e875 | :::no-loc text="MobSIMLock"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e876.png" alt-text="Screenshot of MobSIMMissing."::: | e876 | :::no-loc text="MobSIMMissing"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e877.png" alt-text="Screenshot of Vibrate."::: | e877 | :::no-loc text="Vibrate"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e878.png" alt-text="Screenshot of RoamingInternational."::: | e878 | :::no-loc text="RoamingInternational"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e879.png" alt-text="Screenshot of RoamingDomestic."::: | e879 | :::no-loc text="RoamingDomestic"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e87a.png" alt-text="Screenshot of CallForwardInternational."::: | e87a | :::no-loc text="CallForwardInternational"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e87b.png" alt-text="Screenshot of CallForwardRoaming."::: | e87b | :::no-loc text="CallForwardRoaming"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e87c.png" alt-text="Screenshot of JpnRomanji."::: | e87c | :::no-loc text="JpnRomanji"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e87d.png" alt-text="Screenshot of JpnRomanjiLock."::: | e87d | :::no-loc text="JpnRomanjiLock"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e87e.png" alt-text="Screenshot of JpnRomanjiShift."::: | e87e | :::no-loc text="JpnRomanjiShift"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e87f.png" alt-text="Screenshot of JpnRomanjiShiftLock."::: | e87f | :::no-loc text="JpnRomanjiShiftLock"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e880.png" alt-text="Screenshot of StatusDataTransfer."::: | e880 | :::no-loc text="StatusDataTransfer"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e881.png" alt-text="Screenshot of StatusDataTransferVPN."::: | e881 | :::no-loc text="StatusDataTransferVPN"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e882.png" alt-text="Screenshot of StatusDualSIM2."::: | e882 | :::no-loc text="StatusDualSIM2"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e883.png" alt-text="Screenshot of StatusDualSIM2VPN."::: | e883 | :::no-loc text="StatusDualSIM2VPN"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e884.png" alt-text="Screenshot of StatusDualSIM1."::: | e884 | :::no-loc text="StatusDualSIM1"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e885.png" alt-text="Screenshot of StatusDualSIM1VPN."::: | e885 | :::no-loc text="StatusDualSIM1VPN"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e886.png" alt-text="Screenshot of StatusSGLTE."::: | e886 | :::no-loc text="StatusSGLTE"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e887.png" alt-text="Screenshot of StatusSGLTECell."::: | e887 | :::no-loc text="StatusSGLTECell"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e888.png" alt-text="Screenshot of StatusSGLTEDataVPN."::: | e888 | :::no-loc text="StatusSGLTEDataVPN"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e889.png" alt-text="Screenshot of StatusVPN."::: | e889 | :::no-loc text="StatusVPN"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e88a.png" alt-text="Screenshot of WifiHotspot."::: | e88a | :::no-loc text="WifiHotspot"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e88b.png" alt-text="Screenshot of LanguageKor."::: | e88b | :::no-loc text="LanguageKor"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e88c.png" alt-text="Screenshot of LanguageCht."::: | e88c | :::no-loc text="LanguageCht"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e88d.png" alt-text="Screenshot of LanguageChs."::: | e88d | :::no-loc text="LanguageChs"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e88e.png" alt-text="Screenshot of USB."::: | e88e | :::no-loc text="USB"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e88f.png" alt-text="Screenshot of InkingToolFill."::: | e88f | :::no-loc text="InkingToolFill"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e890.png" alt-text="Screenshot of View."::: | e890 | :::no-loc text="View"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e891.png" alt-text="Screenshot of HighlightFill."::: | e891 | :::no-loc text="HighlightFill"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e892.png" alt-text="Screenshot of Previous."::: | e892 | :::no-loc text="Previous"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e893.png" alt-text="Screenshot of Next."::: | e893 | :::no-loc text="Next"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e894.png" alt-text="Screenshot of Clear."::: | e894 | :::no-loc text="Clear"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e895.png" alt-text="Screenshot of Sync."::: | e895 | :::no-loc text="Sync"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e896.png" alt-text="Screenshot of Download."::: | e896 | :::no-loc text="Download"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e897.png" alt-text="Screenshot of Help."::: | e897 | :::no-loc text="Help"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e898.png" alt-text="Screenshot of Upload."::: | e898 | :::no-loc text="Upload"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e899.png" alt-text="Screenshot of Emoji."::: | e899 | :::no-loc text="Emoji"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e89a.png" alt-text="Screenshot of TwoPage."::: | e89a | :::no-loc text="TwoPage"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e89b.png" alt-text="Screenshot of LeaveChat."::: | e89b | :::no-loc text="LeaveChat"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e89c.png" alt-text="Screenshot of MailForward."::: | e89c | :::no-loc text="MailForward"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e89e.png" alt-text="Screenshot of RotateCamera."::: | e89e | :::no-loc text="RotateCamera"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e89f.png" alt-text="Screenshot of ClosePane."::: | e89f | :::no-loc text="ClosePane"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8a0.png" alt-text="Screenshot of OpenPane."::: | e8a0 | :::no-loc text="OpenPane"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8a1.png" alt-text="Screenshot of PreviewLink."::: | e8a1 | :::no-loc text="PreviewLink"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8a2.png" alt-text="Screenshot of AttachCamera."::: | e8a2 | :::no-loc text="AttachCamera"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8a3.png" alt-text="Screenshot of ZoomIn."::: | e8a3 | :::no-loc text="ZoomIn"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8a4.png" alt-text="Screenshot of Bookmarks."::: | e8a4 | :::no-loc text="Bookmarks"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8a5.png" alt-text="Screenshot of Document."::: | e8a5 | :::no-loc text="Document"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8a6.png" alt-text="Screenshot of ProtectedDocument."::: | e8a6 | :::no-loc text="ProtectedDocument"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8a7.png" alt-text="Screenshot of OpenInNewWindow."::: | e8a7 | :::no-loc text="OpenInNewWindow"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8a8.png" alt-text="Screenshot of MailFill."::: | e8a8 | :::no-loc text="MailFill"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8a9.png" alt-text="Screenshot of ViewAll."::: | e8a9 | :::no-loc text="ViewAll"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8aa.png" alt-text="Screenshot of VideoChat."::: | e8aa | :::no-loc text="VideoChat"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8ab.png" alt-text="Screenshot of Switch."::: | e8ab | :::no-loc text="Switch"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8ac.png" alt-text="Screenshot of Rename."::: | e8ac | :::no-loc text="Rename"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8ad.png" alt-text="Screenshot of Go."::: | e8ad | :::no-loc text="Go"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8ae.png" alt-text="Screenshot of SurfaceHub."::: | e8ae | :::no-loc text="SurfaceHub"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8af.png" alt-text="Screenshot of Remote."::: | e8af | :::no-loc text="Remote"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8b0.png" alt-text="Screenshot of Click."::: | e8b0 | :::no-loc text="Click"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8b1.png" alt-text="Screenshot of Shuffle."::: | e8b1 | :::no-loc text="Shuffle"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8b2.png" alt-text="Screenshot of Movies."::: | e8b2 | :::no-loc text="Movies"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8b3.png" alt-text="Screenshot of SelectAll."::: | e8b3 | :::no-loc text="SelectAll"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8b4.png" alt-text="Screenshot of Orientation."::: | e8b4 | :::no-loc text="Orientation"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8b5.png" alt-text="Screenshot of Import."::: | e8b5 | :::no-loc text="Import"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8b6.png" alt-text="Screenshot of ImportAll."::: | e8b6 | :::no-loc text="ImportAll"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8b7.png" alt-text="Screenshot of Folder."::: | e8b7 | :::no-loc text="Folder"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8b8.png" alt-text="Screenshot of Webcam."::: | e8b8 | :::no-loc text="Webcam"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8b9.png" alt-text="Screenshot of Picture."::: | e8b9 | :::no-loc text="Picture"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8ba.png" alt-text="Screenshot of Caption."::: | e8ba | :::no-loc text="Caption"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8bb.png" alt-text="Screenshot of ChromeClose."::: | e8bb | :::no-loc text="ChromeClose"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8bc.png" alt-text="Screenshot of ShowResults."::: | e8bc | :::no-loc text="ShowResults"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8bd.png" alt-text="Screenshot of Message."::: | e8bd | :::no-loc text="Message"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8be.png" alt-text="Screenshot of Leaf."::: | e8be | :::no-loc text="Leaf"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8bf.png" alt-text="Screenshot of CalendarDay."::: | e8bf | :::no-loc text="CalendarDay"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8c0.png" alt-text="Screenshot of CalendarWeek."::: | e8c0 | :::no-loc text="CalendarWeek"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8c1.png" alt-text="Screenshot of Characters."::: | e8c1 | :::no-loc text="Characters"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8c2.png" alt-text="Screenshot of MailReplyAll."::: | e8c2 | :::no-loc text="MailReplyAll"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8c3.png" alt-text="Screenshot of Read."::: | e8c3 | :::no-loc text="Read"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8c4.png" alt-text="Screenshot of ShowBcc."::: | e8c4 | :::no-loc text="ShowBcc"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8c5.png" alt-text="Screenshot of HideBcc."::: | e8c5 | :::no-loc text="HideBcc"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8c6.png" alt-text="Screenshot of Cut."::: | e8c6 | :::no-loc text="Cut"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8c7.png" alt-text="Screenshot of PaymentCard."::: | e8c7 | :::no-loc text="PaymentCard"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8c8.png" alt-text="Screenshot of Copy."::: | e8c8 | :::no-loc text="Copy"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8c9.png" alt-text="Screenshot of Important."::: | e8c9 | :::no-loc text="Important"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8ca.png" alt-text="Screenshot of MailReply."::: | e8ca | :::no-loc text="MailReply"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8cb.png" alt-text="Screenshot of Sort."::: | e8cb | :::no-loc text="Sort"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8cc.png" alt-text="Screenshot of MobileTablet."::: | e8cc | :::no-loc text="MobileTablet"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8cd.png" alt-text="Screenshot of DisconnectDrive."::: | e8cd | :::no-loc text="DisconnectDrive"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8ce.png" alt-text="Screenshot of MapDrive."::: | e8ce | :::no-loc text="MapDrive"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8cf.png" alt-text="Screenshot of ContactPresence."::: | e8cf | :::no-loc text="ContactPresence"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8d0.png" alt-text="Screenshot of Priority."::: | e8d0 | :::no-loc text="Priority"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8d1.png" alt-text="Screenshot of GotoToday."::: | e8d1 | :::no-loc text="GotoToday"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8d2.png" alt-text="Screenshot of Font."::: | e8d2 | :::no-loc text="Font"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8d3.png" alt-text="Screenshot of FontColor."::: | e8d3 | :::no-loc text="FontColor"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8d4.png" alt-text="Screenshot of Contact2."::: | e8d4 | :::no-loc text="Contact2"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8d5.png" alt-text="Screenshot of FolderFill."::: | e8d5 | :::no-loc text="FolderFill"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8d6.png" alt-text="Screenshot of Audio."::: | e8d6 | :::no-loc text="Audio"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8d7.png" alt-text="Screenshot of Permissions."::: | e8d7 | :::no-loc text="Permissions"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8d8.png" alt-text="Screenshot of DisableUpdates."::: | e8d8 | :::no-loc text="DisableUpdates"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8d9.png" alt-text="Screenshot of Unfavorite."::: | e8d9 | :::no-loc text="Unfavorite"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8da.png" alt-text="Screenshot of OpenLocal."::: | e8da | :::no-loc text="OpenLocal"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8db.png" alt-text="Screenshot of Italic."::: | e8db | :::no-loc text="Italic"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8dc.png" alt-text="Screenshot of Underline."::: | e8dc | :::no-loc text="Underline"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8dd.png" alt-text="Screenshot of Bold."::: | e8dd | :::no-loc text="Bold"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8de.png" alt-text="Screenshot of MoveToFolder."::: | e8de | :::no-loc text="MoveToFolder"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8df.png" alt-text="Screenshot of LikeDislike."::: | e8df | :::no-loc text="LikeDislike"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8e0.png" alt-text="Screenshot of Dislike."::: | e8e0 | :::no-loc text="Dislike"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8e1.png" alt-text="Screenshot of Like."::: | e8e1 | :::no-loc text="Like"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8e2.png" alt-text="Screenshot of AlignRight."::: | e8e2 | :::no-loc text="AlignRight"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8e3.png" alt-text="Screenshot of AlignCenter."::: | e8e3 | :::no-loc text="AlignCenter"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8e4.png" alt-text="Screenshot of AlignLeft."::: | e8e4 | :::no-loc text="AlignLeft"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8e5.png" alt-text="Screenshot of OpenFile."::: | e8e5 | :::no-loc text="OpenFile"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8e6.png" alt-text="Screenshot of ClearSelection."::: | e8e6 | :::no-loc text="ClearSelection"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8e7.png" alt-text="Screenshot of FontDecrease."::: | e8e7 | :::no-loc text="FontDecrease"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8e8.png" alt-text="Screenshot of FontIncrease."::: | e8e8 | :::no-loc text="FontIncrease"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8e9.png" alt-text="Screenshot of FontSize."::: | e8e9 | :::no-loc text="FontSize"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8ea.png" alt-text="Screenshot of CellPhone."::: | e8ea | :::no-loc text="CellPhone"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8eb.png" alt-text="Screenshot of Reshare."::: | e8eb | :::no-loc text="Reshare"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8ec.png" alt-text="Screenshot of Tag."::: | e8ec | :::no-loc text="Tag"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8ed.png" alt-text="Screenshot of RepeatOne."::: | e8ed | :::no-loc text="RepeatOne"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8ee.png" alt-text="Screenshot of RepeatAll."::: | e8ee | :::no-loc text="RepeatAll"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8ef.png" alt-text="Screenshot of Calculator."::: | e8ef | :::no-loc text="Calculator"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8f0.png" alt-text="Screenshot of Directions."::: | e8f0 | :::no-loc text="Directions"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8f1.png" alt-text="Screenshot of Library."::: | e8f1 | :::no-loc text="Library"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8f2.png" alt-text="Screenshot of ChatBubbles."::: | e8f2 | :::no-loc text="ChatBubbles"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8f3.png" alt-text="Screenshot of PostUpdate."::: | e8f3 | :::no-loc text="PostUpdate"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8f4.png" alt-text="Screenshot of NewFolder."::: | e8f4 | :::no-loc text="NewFolder"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8f5.png" alt-text="Screenshot of CalendarReply."::: | e8f5 | :::no-loc text="CalendarReply"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8f6.png" alt-text="Screenshot of UnsyncFolder."::: | e8f6 | :::no-loc text="UnsyncFolder"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8f7.png" alt-text="Screenshot of SyncFolder."::: | e8f7 | :::no-loc text="SyncFolder"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8f8.png" alt-text="Screenshot of BlockContact."::: | e8f8 | :::no-loc text="BlockContact"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8f9.png" alt-text="Screenshot of SwitchApps."::: | e8f9 | :::no-loc text="SwitchApps"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8fa.png" alt-text="Screenshot of AddFriend."::: | e8fa | :::no-loc text="AddFriend"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8fb.png" alt-text="Screenshot of Accept."::: | e8fb | :::no-loc text="Accept"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8fc.png" alt-text="Screenshot of GoToStart."::: | e8fc | :::no-loc text="GoToStart"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8fd.png" alt-text="Screenshot of BulletedList."::: | e8fd | :::no-loc text="BulletedList"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8fe.png" alt-text="Screenshot of Scan."::: | e8fe | :::no-loc text="Scan"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e8ff.png" alt-text="Screenshot of Preview."::: | e8ff | :::no-loc text="Preview"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e902.png" alt-text="Screenshot of Group."::: | e902 | :::no-loc text="Group"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e904.png" alt-text="Screenshot of ZeroBars."::: | e904 | :::no-loc text="ZeroBars"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e905.png" alt-text="Screenshot of OneBar."::: | e905 | :::no-loc text="OneBar"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e906.png" alt-text="Screenshot of TwoBars."::: | e906 | :::no-loc text="TwoBars"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e907.png" alt-text="Screenshot of ThreeBars."::: | e907 | :::no-loc text="ThreeBars"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e908.png" alt-text="Screenshot of FourBars."::: | e908 | :::no-loc text="FourBars"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e909.png" alt-text="Screenshot of World."::: | e909 | :::no-loc text="World"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e90a.png" alt-text="Screenshot of Comment."::: | e90a | :::no-loc text="Comment"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e90b.png" alt-text="Screenshot of MusicInfo."::: | e90b | :::no-loc text="MusicInfo"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e90c.png" alt-text="Screenshot of DockLeft."::: | e90c | :::no-loc text="DockLeft"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e90d.png" alt-text="Screenshot of DockRight."::: | e90d | :::no-loc text="DockRight"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e90e.png" alt-text="Screenshot of DockBottom."::: | e90e | :::no-loc text="DockBottom"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e90f.png" alt-text="Screenshot of Repair."::: | e90f | :::no-loc text="Repair"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e910.png" alt-text="Screenshot of Accounts."::: | e910 | :::no-loc text="Accounts"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e911.png" alt-text="Screenshot of DullSound."::: | e911 | :::no-loc text="DullSound"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e912.png" alt-text="Screenshot of Manage."::: | e912 | :::no-loc text="Manage"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e913.png" alt-text="Screenshot of Street."::: | e913 | :::no-loc text="Street"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e914.png" alt-text="Screenshot of Printer3D."::: | e914 | :::no-loc text="Printer3D"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e915.png" alt-text="Screenshot of RadioBullet."::: | e915 | :::no-loc text="RadioBullet"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e916.png" alt-text="Screenshot of Stopwatch."::: | e916 | :::no-loc text="Stopwatch"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e91b.png" alt-text="Screenshot of Photo."::: | e91b | :::no-loc text="Photo"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e91c.png" alt-text="Screenshot of ActionCenter."::: | e91c | :::no-loc text="ActionCenter"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e91f.png" alt-text="Screenshot of FullCircleMask."::: | e91f | :::no-loc text="FullCircleMask"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e921.png" alt-text="Screenshot of ChromeMinimize."::: | e921 | :::no-loc text="ChromeMinimize"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e922.png" alt-text="Screenshot of ChromeMaximize."::: | e922 | :::no-loc text="ChromeMaximize"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e923.png" alt-text="Screenshot of ChromeRestore."::: | e923 | :::no-loc text="ChromeRestore"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e924.png" alt-text="Screenshot of Annotation."::: | e924 | :::no-loc text="Annotation"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e925.png" alt-text="Screenshot of BackSpaceQWERTYSm."::: | e925 | :::no-loc text="BackSpaceQWERTYSm"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e926.png" alt-text="Screenshot of BackSpaceQWERTYMd."::: | e926 | :::no-loc text="BackSpaceQWERTYMd"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e927.png" alt-text="Screenshot of Swipe."::: | e927 | :::no-loc text="Swipe"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e928.png" alt-text="Screenshot of Fingerprint."::: | e928 | :::no-loc text="Fingerprint"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e929.png" alt-text="Screenshot of Handwriting."::: | e929 | :::no-loc text="Handwriting"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e92c.png" alt-text="Screenshot of ChromeBackToWindow."::: | e92c | :::no-loc text="ChromeBackToWindow"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e92d.png" alt-text="Screenshot of ChromeFullScreen."::: | e92d | :::no-loc text="ChromeFullScreen"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e92e.png" alt-text="Screenshot of KeyboardStandard."::: | e92e | :::no-loc text="KeyboardStandard"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e92f.png" alt-text="Screenshot of KeyboardDismiss."::: | e92f | :::no-loc text="KeyboardDismiss"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e930.png" alt-text="Screenshot of Completed."::: | e930 | :::no-loc text="Completed"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e931.png" alt-text="Screenshot of ChromeAnnotate."::: | e931 | :::no-loc text="ChromeAnnotate"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e932.png" alt-text="Screenshot of Label."::: | e932 | :::no-loc text="Label"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e933.png" alt-text="Screenshot of IBeam."::: | e933 | :::no-loc text="IBeam"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e934.png" alt-text="Screenshot of IBeamOutline."::: | e934 | :::no-loc text="IBeamOutline"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e935.png" alt-text="Screenshot of FlickDown."::: | e935 | :::no-loc text="FlickDown"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e936.png" alt-text="Screenshot of FlickUp."::: | e936 | :::no-loc text="FlickUp"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e937.png" alt-text="Screenshot of FlickLeft."::: | e937 | :::no-loc text="FlickLeft"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e938.png" alt-text="Screenshot of FlickRight."::: | e938 | :::no-loc text="FlickRight"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e939.png" alt-text="Screenshot of FeedbackApp."::: | e939 | :::no-loc text="FeedbackApp"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e93c.png" alt-text="Screenshot of MusicAlbum."::: | e93c | :::no-loc text="MusicAlbum"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e93e.png" alt-text="Screenshot of Streaming."::: | e93e | :::no-loc text="Streaming"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e943.png" alt-text="Screenshot of Code."::: | e943 | :::no-loc text="Code"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e944.png" alt-text="Screenshot of ReturnToWindow."::: | e944 | :::no-loc text="ReturnToWindow"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e945.png" alt-text="Screenshot of LightningBolt."::: | e945 | :::no-loc text="LightningBolt"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e946.png" alt-text="Screenshot of Info."::: | e946 | :::no-loc text="Info"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e947.png" alt-text="Screenshot of CalculatorMultiply."::: | e947 | :::no-loc text="CalculatorMultiply"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e948.png" alt-text="Screenshot of CalculatorAddition."::: | e948 | :::no-loc text="CalculatorAddition"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e949.png" alt-text="Screenshot of CalculatorSubtract."::: | e949 | :::no-loc text="CalculatorSubtract"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e94a.png" alt-text="Screenshot of CalculatorDivide."::: | e94a | :::no-loc text="CalculatorDivide"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e94b.png" alt-text="Screenshot of CalculatorSquareroot."::: | e94b | :::no-loc text="CalculatorSquareroot"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e94c.png" alt-text="Screenshot of CalculatorPercentage."::: | e94c | :::no-loc text="CalculatorPercentage"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e94d.png" alt-text="Screenshot of CalculatorNegate."::: | e94d | :::no-loc text="CalculatorNegate"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e94e.png" alt-text="Screenshot of CalculatorEqualTo."::: | e94e | :::no-loc text="CalculatorEqualTo"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e94f.png" alt-text="Screenshot of CalculatorBackspace."::: | e94f | :::no-loc text="CalculatorBackspace"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e950.png" alt-text="Screenshot of Component."::: | e950 | :::no-loc text="Component"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e951.png" alt-text="Screenshot of DMC."::: | e951 | :::no-loc text="DMC"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e952.png" alt-text="Screenshot of Dock."::: | e952 | :::no-loc text="Dock"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e953.png" alt-text="Screenshot of MultimediaDMS."::: | e953 | :::no-loc text="MultimediaDMS"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e954.png" alt-text="Screenshot of MultimediaDVR."::: | e954 | :::no-loc text="MultimediaDVR"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e955.png" alt-text="Screenshot of MultimediaPMP."::: | e955 | :::no-loc text="MultimediaPMP"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e956.png" alt-text="Screenshot of PrintfaxPrinterFile."::: | e956 | :::no-loc text="PrintfaxPrinterFile"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e957.png" alt-text="Screenshot of Sensor."::: | e957 | :::no-loc text="Sensor"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e958.png" alt-text="Screenshot of StorageOptical."::: | e958 | :::no-loc text="StorageOptical"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e95a.png" alt-text="Screenshot of Communications."::: | e95a | :::no-loc text="Communications"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e95b.png" alt-text="Screenshot of Headset."::: | e95b | :::no-loc text="Headset"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e95d.png" alt-text="Screenshot of Projector."::: | e95d | :::no-loc text="Projector"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e95e.png" alt-text="Screenshot of Health."::: | e95e | :::no-loc text="Health"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e95f.png" alt-text="Screenshot of Wire."::: | e95f | :::no-loc text="Wire"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e960.png" alt-text="Screenshot of Webcam2."::: | e960 | :::no-loc text="Webcam2"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e961.png" alt-text="Screenshot of Input."::: | e961 | :::no-loc text="Input"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e962.png" alt-text="Screenshot of Mouse."::: | e962 | :::no-loc text="Mouse"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e963.png" alt-text="Screenshot of Smartcard."::: | e963 | :::no-loc text="Smartcard"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e964.png" alt-text="Screenshot of SmartcardVirtual."::: | e964 | :::no-loc text="SmartcardVirtual"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e965.png" alt-text="Screenshot of MediaStorageTower."::: | e965 | :::no-loc text="MediaStorageTower"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e966.png" alt-text="Screenshot of ReturnKeySm."::: | e966 | :::no-loc text="ReturnKeySm"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e967.png" alt-text="Screenshot of GameConsole."::: | e967 | :::no-loc text="GameConsole"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e968.png" alt-text="Screenshot of Network."::: | e968 | :::no-loc text="Network"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e969.png" alt-text="Screenshot of StorageNetworkWireless."::: | e969 | :::no-loc text="StorageNetworkWireless"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e96a.png" alt-text="Screenshot of StorageTape."::: | e96a | :::no-loc text="StorageTape"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e96d.png" alt-text="Screenshot of ChevronUpSmall."::: | e96d | :::no-loc text="ChevronUpSmall"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e96e.png" alt-text="Screenshot of ChevronDownSmall."::: | e96e | :::no-loc text="ChevronDownSmall"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e96f.png" alt-text="Screenshot of ChevronLeftSmall."::: | e96f | :::no-loc text="ChevronLeftSmall"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e970.png" alt-text="Screenshot of ChevronRightSmall."::: | e970 | :::no-loc text="ChevronRightSmall"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e971.png" alt-text="Screenshot of ChevronUpMed."::: | e971 | :::no-loc text="ChevronUpMed"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e972.png" alt-text="Screenshot of ChevronDownMed."::: | e972 | :::no-loc text="ChevronDownMed"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e973.png" alt-text="Screenshot of ChevronLeftMed."::: | e973 | :::no-loc text="ChevronLeftMed"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e974.png" alt-text="Screenshot of ChevronRightMed."::: | e974 | :::no-loc text="ChevronRightMed"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e975.png" alt-text="Screenshot of Devices2."::: | e975 | :::no-loc text="Devices2"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e976.png" alt-text="Screenshot of ExpandTile."::: | e976 | :::no-loc text="ExpandTile"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e977.png" alt-text="Screenshot of PC1."::: | e977 | :::no-loc text="PC1"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e978.png" alt-text="Screenshot of PresenceChicklet."::: | e978 | :::no-loc text="PresenceChicklet"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e979.png" alt-text="Screenshot of PresenceChickletVideo."::: | e979 | :::no-loc text="PresenceChickletVideo"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e97a.png" alt-text="Screenshot of Reply."::: | e97a | :::no-loc text="Reply"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e97b.png" alt-text="Screenshot of SetTile."::: | e97b | :::no-loc text="SetTile"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e97c.png" alt-text="Screenshot of Type."::: | e97c | :::no-loc text="Type"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e97d.png" alt-text="Screenshot of Korean."::: | e97d | :::no-loc text="Korean"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e97e.png" alt-text="Screenshot of HalfAlpha."::: | e97e | :::no-loc text="HalfAlpha"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e97f.png" alt-text="Screenshot of FullAlpha."::: | e97f | :::no-loc text="FullAlpha"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e980.png" alt-text="Screenshot of Key12On."::: | e980 | :::no-loc text="Key12On"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e981.png" alt-text="Screenshot of ChineseChangjie."::: | e981 | :::no-loc text="ChineseChangjie"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e982.png" alt-text="Screenshot of QWERTYOn."::: | e982 | :::no-loc text="QWERTYOn"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e983.png" alt-text="Screenshot of QWERTYOff."::: | e983 | :::no-loc text="QWERTYOff"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e984.png" alt-text="Screenshot of ChineseQuick."::: | e984 | :::no-loc text="ChineseQuick"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e985.png" alt-text="Screenshot of Japanese."::: | e985 | :::no-loc text="Japanese"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e986.png" alt-text="Screenshot of FullHiragana."::: | e986 | :::no-loc text="FullHiragana"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e987.png" alt-text="Screenshot of FullKatakana."::: | e987 | :::no-loc text="FullKatakana"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e988.png" alt-text="Screenshot of HalfKatakana."::: | e988 | :::no-loc text="HalfKatakana"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e989.png" alt-text="Screenshot of ChineseBoPoMoFo."::: | e989 | :::no-loc text="ChineseBoPoMoFo"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e98a.png" alt-text="Screenshot of ChinesePinyin."::: | e98a | :::no-loc text="ChinesePinyin"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e98f.png" alt-text="Screenshot of ConstructionCone."::: | e98f | :::no-loc text="ConstructionCone"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e990.png" alt-text="Screenshot of XboxOneConsole."::: | e990 | :::no-loc text="XboxOneConsole"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e992.png" alt-text="Screenshot of Volume0."::: | e992 | :::no-loc text="Volume0"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e993.png" alt-text="Screenshot of Volume1."::: | e993 | :::no-loc text="Volume1"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e994.png" alt-text="Screenshot of Volume2."::: | e994 | :::no-loc text="Volume2"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e995.png" alt-text="Screenshot of Volume3."::: | e995 | :::no-loc text="Volume3"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e996.png" alt-text="Screenshot of BatteryUnknown."::: | e996 | :::no-loc text="BatteryUnknown"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e998.png" alt-text="Screenshot of WifiAttentionOverlay."::: | e998 | :::no-loc text="WifiAttentionOverlay"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e99a.png" alt-text="Screenshot of Robot."::: | e99a | :::no-loc text="Robot"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e9a1.png" alt-text="Screenshot of TapAndSend."::: | e9a1 | :::no-loc text="TapAndSend"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e9a4.png" alt-text="Screenshot of TextBulletListSquare."::: | e9a4 | :::no-loc text="TextBulletListSquare"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e9a6.png" alt-text="Screenshot of FitPage."::: | e9a6 | :::no-loc text="FitPage"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e9a8.png" alt-text="Screenshot of PasswordKeyShow."::: | e9a8 | :::no-loc text="PasswordKeyShow"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e9a9.png" alt-text="Screenshot of PasswordKeyHide."::: | e9a9 | :::no-loc text="PasswordKeyHide"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e9aa.png" alt-text="Screenshot of BidiLtr."::: | e9aa | :::no-loc text="BidiLtr"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e9ab.png" alt-text="Screenshot of BidiRtl."::: | e9ab | :::no-loc text="BidiRtl"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e9ac.png" alt-text="Screenshot of ForwardSm."::: | e9ac | :::no-loc text="ForwardSm"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e9ad.png" alt-text="Screenshot of CommaKey."::: | e9ad | :::no-loc text="CommaKey"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e9ae.png" alt-text="Screenshot of DashKey."::: | e9ae | :::no-loc text="DashKey"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e9af.png" alt-text="Screenshot of DullSoundKey."::: | e9af | :::no-loc text="DullSoundKey"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e9b0.png" alt-text="Screenshot of HalfDullSound."::: | e9b0 | :::no-loc text="HalfDullSound"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e9b1.png" alt-text="Screenshot of RightDoubleQuote."::: | e9b1 | :::no-loc text="RightDoubleQuote"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e9b2.png" alt-text="Screenshot of LeftDoubleQuote."::: | e9b2 | :::no-loc text="LeftDoubleQuote"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e9b3.png" alt-text="Screenshot of PuncKeyRightBottom."::: | e9b3 | :::no-loc text="PuncKeyRightBottom"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e9b4.png" alt-text="Screenshot of PuncKey1."::: | e9b4 | :::no-loc text="PuncKey1"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e9b5.png" alt-text="Screenshot of PuncKey2."::: | e9b5 | :::no-loc text="PuncKey2"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e9b6.png" alt-text="Screenshot of PuncKey3."::: | e9b6 | :::no-loc text="PuncKey3"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e9b7.png" alt-text="Screenshot of PuncKey4."::: | e9b7 | :::no-loc text="PuncKey4"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e9b8.png" alt-text="Screenshot of PuncKey5."::: | e9b8 | :::no-loc text="PuncKey5"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e9b9.png" alt-text="Screenshot of PuncKey6."::: | e9b9 | :::no-loc text="PuncKey6"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e9ba.png" alt-text="Screenshot of PuncKey9."::: | e9ba | :::no-loc text="PuncKey9"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e9bb.png" alt-text="Screenshot of PuncKey7."::: | e9bb | :::no-loc text="PuncKey7"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e9bc.png" alt-text="Screenshot of PuncKey8."::: | e9bc | :::no-loc text="PuncKey8"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e9ca.png" alt-text="Screenshot of Frigid."::: | e9ca | :::no-loc text="Frigid"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e9ce.png" alt-text="Screenshot of Unknown."::: | e9ce | :::no-loc text="Unknown"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e9d2.png" alt-text="Screenshot of AreaChart."::: | e9d2 | :::no-loc text="AreaChart"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e9d5.png" alt-text="Screenshot of CheckList."::: | e9d5 | :::no-loc text="CheckList"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e9d9.png" alt-text="Screenshot of Diagnostic."::: | e9d9 | :::no-loc text="Diagnostic"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e9e9.png" alt-text="Screenshot of Equalizer."::: | e9e9 | :::no-loc text="Equalizer"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e9f3.png" alt-text="Screenshot of Process."::: | e9f3 | :::no-loc text="Process"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e9f5.png" alt-text="Screenshot of Processing."::: | e9f5 | :::no-loc text="Processing"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/e9f9.png" alt-text="Screenshot of ReportDocument."::: | e9f9 | :::no-loc text="ReportDocument"::: |

### PUA EA00-EC00

The following table of glyphs displays unicode points prefixed from EA- to EC-.

[Back to top](#icon-list)

| Glyph | Unicode point | Description |
|-------|---------------|-------------|
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea0c.png" alt-text="Screenshot of VideoSolid."::: | ea0c | :::no-loc text="VideoSolid"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea0d.png" alt-text="Screenshot of MixedMediaBadge."::: | ea0d | :::no-loc text="MixedMediaBadge"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea14.png" alt-text="Screenshot of DisconnectDisplay."::: | ea14 | :::no-loc text="DisconnectDisplay"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea18.png" alt-text="Screenshot of Shield."::: | ea18 | :::no-loc text="Shield"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea1f.png" alt-text="Screenshot of Info2."::: | ea1f | :::no-loc text="Info2"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea21.png" alt-text="Screenshot of ActionCenterAsterisk."::: | ea21 | :::no-loc text="ActionCenterAsterisk"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea24.png" alt-text="Screenshot of Beta."::: | ea24 | :::no-loc text="Beta"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea35.png" alt-text="Screenshot of SaveCopy."::: | ea35 | :::no-loc text="SaveCopy"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea37.png" alt-text="Screenshot of List."::: | ea37 | :::no-loc text="List"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea38.png" alt-text="Screenshot of Asterisk."::: | ea38 | :::no-loc text="Asterisk"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea39.png" alt-text="Screenshot of ErrorBadge."::: | ea39 | :::no-loc text="ErrorBadge"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea3a.png" alt-text="Screenshot of CircleRing."::: | ea3a | :::no-loc text="CircleRing"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea3b.png" alt-text="Screenshot of CircleFill."::: | ea3b | :::no-loc text="CircleFill"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea3c.png" alt-text="Screenshot of MergeCall."::: | ea3c | :::no-loc text="MergeCall"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea3d.png" alt-text="Screenshot of PrivateCall."::: | ea3d | :::no-loc text="PrivateCall"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea3f.png" alt-text="Screenshot of Record2."::: | ea3f | :::no-loc text="Record2"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea40.png" alt-text="Screenshot of AllAppsMirrored."::: | ea40 | :::no-loc text="AllAppsMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea41.png" alt-text="Screenshot of BookmarksMirrored."::: | ea41 | :::no-loc text="BookmarksMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea42.png" alt-text="Screenshot of BulletedListMirrored."::: | ea42 | :::no-loc text="BulletedListMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea43.png" alt-text="Screenshot of CallForwardInternationalMirrored."::: | ea43 | :::no-loc text="CallForwardInternationalMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea44.png" alt-text="Screenshot of CallForwardRoamingMirrored."::: | ea44 | :::no-loc text="CallForwardRoamingMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea47.png" alt-text="Screenshot of ChromeBackMirrored."::: | ea47 | :::no-loc text="ChromeBackMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea48.png" alt-text="Screenshot of ClearSelectionMirrored."::: | ea48 | :::no-loc text="ClearSelectionMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea49.png" alt-text="Screenshot of ClosePaneMirrored."::: | ea49 | :::no-loc text="ClosePaneMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea4a.png" alt-text="Screenshot of ContactInfoMirrored."::: | ea4a | :::no-loc text="ContactInfoMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea4b.png" alt-text="Screenshot of DockRightMirrored."::: | ea4b | :::no-loc text="DockRightMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea4c.png" alt-text="Screenshot of DockLeftMirrored."::: | ea4c | :::no-loc text="DockLeftMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea4e.png" alt-text="Screenshot of ExpandTileMirrored."::: | ea4e | :::no-loc text="ExpandTileMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea4f.png" alt-text="Screenshot of GoMirrored."::: | ea4f | :::no-loc text="GoMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea50.png" alt-text="Screenshot of GripperResizeMirrored."::: | ea50 | :::no-loc text="GripperResizeMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea51.png" alt-text="Screenshot of HelpMirrored."::: | ea51 | :::no-loc text="HelpMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea52.png" alt-text="Screenshot of ImportMirrored."::: | ea52 | :::no-loc text="ImportMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea53.png" alt-text="Screenshot of ImportAllMirrored."::: | ea53 | :::no-loc text="ImportAllMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea54.png" alt-text="Screenshot of LeaveChatMirrored."::: | ea54 | :::no-loc text="LeaveChatMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea55.png" alt-text="Screenshot of ListMirrored."::: | ea55 | :::no-loc text="ListMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea56.png" alt-text="Screenshot of MailForwardMirrored."::: | ea56 | :::no-loc text="MailForwardMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea57.png" alt-text="Screenshot of MailReplyMirrored."::: | ea57 | :::no-loc text="MailReplyMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea58.png" alt-text="Screenshot of MailReplyAllMirrored."::: | ea58 | :::no-loc text="MailReplyAllMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea5b.png" alt-text="Screenshot of OpenPaneMirrored."::: | ea5b | :::no-loc text="OpenPaneMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea5c.png" alt-text="Screenshot of OpenWithMirrored."::: | ea5c | :::no-loc text="OpenWithMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea5e.png" alt-text="Screenshot of ParkingLocationMirrored."::: | ea5e | :::no-loc text="ParkingLocationMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea5f.png" alt-text="Screenshot of ResizeMouseMediumMirrored."::: | ea5f | :::no-loc text="ResizeMouseMediumMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea60.png" alt-text="Screenshot of ResizeMouseSmallMirrored."::: | ea60 | :::no-loc text="ResizeMouseSmallMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea61.png" alt-text="Screenshot of ResizeMouseTallMirrored."::: | ea61 | :::no-loc text="ResizeMouseTallMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea62.png" alt-text="Screenshot of ResizeTouchNarrowerMirrored."::: | ea62 | :::no-loc text="ResizeTouchNarrowerMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea63.png" alt-text="Screenshot of SendMirrored."::: | ea63 | :::no-loc text="SendMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea64.png" alt-text="Screenshot of SendFillMirrored."::: | ea64 | :::no-loc text="SendFillMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea65.png" alt-text="Screenshot of ShowResultsMirrored."::: | ea65 | :::no-loc text="ShowResultsMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea69.png" alt-text="Screenshot of Media."::: | ea69 | :::no-loc text="Media"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea6a.png" alt-text="Screenshot of SyncError."::: | ea6a | :::no-loc text="SyncError"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea6c.png" alt-text="Screenshot of Devices3."::: | ea6c | :::no-loc text="Devices3"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea79.png" alt-text="Screenshot of SlowMotionOn."::: | ea79 | :::no-loc text="SlowMotionOn"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea80.png" alt-text="Screenshot of Lightbulb."::: | ea80 | :::no-loc text="Lightbulb"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea81.png" alt-text="Screenshot of StatusCircle."::: | ea81 | :::no-loc text="StatusCircle"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea82.png" alt-text="Screenshot of StatusTriangle."::: | ea82 | :::no-loc text="StatusTriangle"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea83.png" alt-text="Screenshot of StatusError."::: | ea83 | :::no-loc text="StatusError"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea84.png" alt-text="Screenshot of StatusWarning."::: | ea84 | :::no-loc text="StatusWarning"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea85.png" alt-text="Screenshot of VolumeDisabled."::: | ea85 | :::no-loc text="VolumeDisabled"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea86.png" alt-text="Screenshot of Puzzle."::: | ea86 | :::no-loc text="Puzzle"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea89.png" alt-text="Screenshot of CalendarSolid."::: | ea89 | :::no-loc text="CalendarSolid"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea8a.png" alt-text="Screenshot of HomeSolid."::: | ea8a | :::no-loc text="HomeSolid"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea8b.png" alt-text="Screenshot of ParkingLocationSolid."::: | ea8b | :::no-loc text="ParkingLocationSolid"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea8c.png" alt-text="Screenshot of ContactSolid."::: | ea8c | :::no-loc text="ContactSolid"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea8d.png" alt-text="Screenshot of ConstructionSolid."::: | ea8d | :::no-loc text="ConstructionSolid"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea8e.png" alt-text="Screenshot of AccidentSolid."::: | ea8e | :::no-loc text="AccidentSolid"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea8f.png" alt-text="Screenshot of Ringer."::: | ea8f | :::no-loc text="Ringer"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea90.png" alt-text="Screenshot of PDF."::: | ea90 | :::no-loc text="PDF"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea91.png" alt-text="Screenshot of ThoughtBubble."::: | ea91 | :::no-loc text="ThoughtBubble"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea92.png" alt-text="Screenshot of HeartBroken."::: | ea92 | :::no-loc text="HeartBroken"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea93.png" alt-text="Screenshot of BatteryCharging10."::: | ea93 | :::no-loc text="BatteryCharging10"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea93.png" alt-text="Screenshot of BatteryCharging10."::: | ea93 | :::no-loc text="BatteryCharging10"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea94.png" alt-text="Screenshot of BatterySaver9."::: | ea94 | :::no-loc text="BatterySaver9"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea95.png" alt-text="Screenshot of BatterySaver10."::: | ea95 | :::no-loc text="BatterySaver10"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea97.png" alt-text="Screenshot of CallForwardingMirrored."::: | ea97 | :::no-loc text="CallForwardingMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea98.png" alt-text="Screenshot of MultiSelectMirrored."::: | ea98 | :::no-loc text="MultiSelectMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ea99.png" alt-text="Screenshot of Broom."::: | ea99 | :::no-loc text="Broom"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eac2.png" alt-text="Screenshot of ForwardCall."::: | eac2 | :::no-loc text="ForwardCall"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eac7.png" alt-text="Screenshot of DesktopLeafTwo."::: | eac7 | :::no-loc text="DesktopLeafTwo"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ead4.png" alt-text="Screenshot of Emojiplay."::: | ead4 | :::no-loc text="Emojiplay"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ead5.png" alt-text="Screenshot of EmojiBrush."::: | ead5 | :::no-loc text="EmojiBrush"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ead6.png" alt-text="Screenshot of EyeTracking."::: | ead6 | :::no-loc text="EyeTracking"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ead7.png" alt-text="Screenshot of EyeTrackingText."::: | ead7 | :::no-loc text="EyeTrackingText"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eadf.png" alt-text="Screenshot of Trackers."::: | eadf | :::no-loc text="Trackers"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eafc.png" alt-text="Screenshot of Market."::: | eafc | :::no-loc text="Market"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb05.png" alt-text="Screenshot of PieSingle."::: | eb05 | :::no-loc text="PieSingle"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb0f.png" alt-text="Screenshot of StockUp."::: | eb0f | :::no-loc text="StockUp"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb11.png" alt-text="Screenshot of StockDown."::: | eb11 | :::no-loc text="StockDown"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb19.png" alt-text="Screenshot of ClicktoDoOff."::: | eb19 | :::no-loc text="ClicktoDoOff"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb1d.png" alt-text="Screenshot of ClicktoDo."::: | eb1d | :::no-loc text="ClicktoDo"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb3b.png" alt-text="Screenshot of GenericApp."::: | eb3b | :::no-loc text="GenericApp"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb3c.png" alt-text="Screenshot of Design."::: | eb3c | :::no-loc text="Design"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb41.png" alt-text="Screenshot of Website."::: | eb41 | :::no-loc text="Website"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb42.png" alt-text="Screenshot of Drop."::: | eb42 | :::no-loc text="Drop"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb44.png" alt-text="Screenshot of Radar."::: | eb44 | :::no-loc text="Radar"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb47.png" alt-text="Screenshot of BusSolid."::: | eb47 | :::no-loc text="BusSolid"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb48.png" alt-text="Screenshot of FerrySolid."::: | eb48 | :::no-loc text="FerrySolid"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb49.png" alt-text="Screenshot of StartPointSolid."::: | eb49 | :::no-loc text="StartPointSolid"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb4a.png" alt-text="Screenshot of StopPointSolid."::: | eb4a | :::no-loc text="StopPointSolid"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb4b.png" alt-text="Screenshot of EndPointSolid."::: | eb4b | :::no-loc text="EndPointSolid"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb4c.png" alt-text="Screenshot of AirplaneSolid."::: | eb4c | :::no-loc text="AirplaneSolid"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb4d.png" alt-text="Screenshot of TrainSolid."::: | eb4d | :::no-loc text="TrainSolid"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb4e.png" alt-text="Screenshot of WorkSolid."::: | eb4e | :::no-loc text="WorkSolid"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb4f.png" alt-text="Screenshot of ReminderFill."::: | eb4f | :::no-loc text="ReminderFill"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb50.png" alt-text="Screenshot of Reminder."::: | eb50 | :::no-loc text="Reminder"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb51.png" alt-text="Screenshot of Heart."::: | eb51 | :::no-loc text="Heart"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb52.png" alt-text="Screenshot of HeartFill."::: | eb52 | :::no-loc text="HeartFill"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb55.png" alt-text="Screenshot of EthernetError."::: | eb55 | :::no-loc text="EthernetError"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb56.png" alt-text="Screenshot of EthernetWarning."::: | eb56 | :::no-loc text="EthernetWarning"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb57.png" alt-text="Screenshot of StatusConnecting1."::: | eb57 | :::no-loc text="StatusConnecting1"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb58.png" alt-text="Screenshot of StatusConnecting2."::: | eb58 | :::no-loc text="StatusConnecting2"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb59.png" alt-text="Screenshot of StatusUnsecure."::: | eb59 | :::no-loc text="StatusUnsecure"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb5a.png" alt-text="Screenshot of WifiError0."::: | eb5a | :::no-loc text="WifiError0"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb5b.png" alt-text="Screenshot of WifiError1."::: | eb5b | :::no-loc text="WifiError1"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb5c.png" alt-text="Screenshot of WifiError2."::: | eb5c | :::no-loc text="WifiError2"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb5d.png" alt-text="Screenshot of WifiError3."::: | eb5d | :::no-loc text="WifiError3"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb5e.png" alt-text="Screenshot of WifiError4."::: | eb5e | :::no-loc text="WifiError4"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb5f.png" alt-text="Screenshot of WifiWarning0."::: | eb5f | :::no-loc text="WifiWarning0"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb60.png" alt-text="Screenshot of WifiWarning1."::: | eb60 | :::no-loc text="WifiWarning1"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb61.png" alt-text="Screenshot of WifiWarning2."::: | eb61 | :::no-loc text="WifiWarning2"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb62.png" alt-text="Screenshot of WifiWarning3."::: | eb62 | :::no-loc text="WifiWarning3"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb63.png" alt-text="Screenshot of WifiWarning4."::: | eb63 | :::no-loc text="WifiWarning4"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb66.png" alt-text="Screenshot of Devices4."::: | eb66 | :::no-loc text="Devices4"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb67.png" alt-text="Screenshot of NUIIris."::: | eb67 | :::no-loc text="NUIIris"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb68.png" alt-text="Screenshot of NUIFace."::: | eb68 | :::no-loc text="NUIFace"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb77.png" alt-text="Screenshot of GatewayRouter."::: | eb77 | :::no-loc text="GatewayRouter"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb7e.png" alt-text="Screenshot of EditMirrored."::: | eb7e | :::no-loc text="EditMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb82.png" alt-text="Screenshot of NUIFPStartSlideHand."::: | eb82 | :::no-loc text="NUIFPStartSlideHand"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb83.png" alt-text="Screenshot of NUIFPStartSlideAction."::: | eb83 | :::no-loc text="NUIFPStartSlideAction"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb84.png" alt-text="Screenshot of NUIFPContinueSlideHand."::: | eb84 | :::no-loc text="NUIFPContinueSlideHand"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb85.png" alt-text="Screenshot of NUIFPContinueSlideAction."::: | eb85 | :::no-loc text="NUIFPContinueSlideAction"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb86.png" alt-text="Screenshot of NUIFPRollRightHand."::: | eb86 | :::no-loc text="NUIFPRollRightHand"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb87.png" alt-text="Screenshot of NUIFPRollRightHandAction."::: | eb87 | :::no-loc text="NUIFPRollRightHandAction"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb88.png" alt-text="Screenshot of NUIFPRollLeftHand."::: | eb88 | :::no-loc text="NUIFPRollLeftHand"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb89.png" alt-text="Screenshot of NUIFPRollLeftAction."::: | eb89 | :::no-loc text="NUIFPRollLeftAction"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb8a.png" alt-text="Screenshot of NUIFPPressHand."::: | eb8a | :::no-loc text="NUIFPPressHand"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb8b.png" alt-text="Screenshot of NUIFPPressAction."::: | eb8b | :::no-loc text="NUIFPPressAction"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb8c.png" alt-text="Screenshot of NUIFPPressRepeatHand."::: | eb8c | :::no-loc text="NUIFPPressRepeatHand"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb8d.png" alt-text="Screenshot of NUIFPPressRepeatAction."::: | eb8d | :::no-loc text="NUIFPPressRepeatAction"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb90.png" alt-text="Screenshot of StatusErrorFull."::: | eb90 | :::no-loc text="StatusErrorFull"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb91.png" alt-text="Screenshot of TaskViewExpanded."::: | eb91 | :::no-loc text="TaskViewExpanded"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb95.png" alt-text="Screenshot of Certificate."::: | eb95 | :::no-loc text="Certificate"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb96.png" alt-text="Screenshot of BackSpaceQWERTYLg."::: | eb96 | :::no-loc text="BackSpaceQWERTYLg"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb97.png" alt-text="Screenshot of ReturnKeyLg."::: | eb97 | :::no-loc text="ReturnKeyLg"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb9d.png" alt-text="Screenshot of FastForward."::: | eb9d | :::no-loc text="FastForward"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb9e.png" alt-text="Screenshot of Rewind."::: | eb9e | :::no-loc text="Rewind"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eb9f.png" alt-text="Screenshot of Photo2."::: | eb9f | :::no-loc text="Photo2"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eba0.png" alt-text="Screenshot of MobBattery0."::: | eba0 | :::no-loc text="MobBattery0"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eba1.png" alt-text="Screenshot of MobBattery1."::: | eba1 | :::no-loc text="MobBattery1"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eba2.png" alt-text="Screenshot of MobBattery2."::: | eba2 | :::no-loc text="MobBattery2"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eba3.png" alt-text="Screenshot of MobBattery3."::: | eba3 | :::no-loc text="MobBattery3"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eba4.png" alt-text="Screenshot of MobBattery4."::: | eba4 | :::no-loc text="MobBattery4"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eba5.png" alt-text="Screenshot of MobBattery5."::: | eba5 | :::no-loc text="MobBattery5"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eba6.png" alt-text="Screenshot of MobBattery6."::: | eba6 | :::no-loc text="MobBattery6"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eba7.png" alt-text="Screenshot of MobBattery7."::: | eba7 | :::no-loc text="MobBattery7"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eba8.png" alt-text="Screenshot of MobBattery8."::: | eba8 | :::no-loc text="MobBattery8"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eba9.png" alt-text="Screenshot of MobBattery9."::: | eba9 | :::no-loc text="MobBattery9"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ebaa.png" alt-text="Screenshot of MobBattery10."::: | ebaa | :::no-loc text="MobBattery10"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ebab.png" alt-text="Screenshot of MobBatteryCharging0."::: | ebab | :::no-loc text="MobBatteryCharging0"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ebac.png" alt-text="Screenshot of MobBatteryCharging1."::: | ebac | :::no-loc text="MobBatteryCharging1"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ebad.png" alt-text="Screenshot of MobBatteryCharging2."::: | ebad | :::no-loc text="MobBatteryCharging2"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ebae.png" alt-text="Screenshot of MobBatteryCharging3."::: | ebae | :::no-loc text="MobBatteryCharging3"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ebaf.png" alt-text="Screenshot of MobBatteryCharging4."::: | ebaf | :::no-loc text="MobBatteryCharging4"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ebb0.png" alt-text="Screenshot of MobBatteryCharging5."::: | ebb0 | :::no-loc text="MobBatteryCharging5"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ebb1.png" alt-text="Screenshot of MobBatteryCharging6."::: | ebb1 | :::no-loc text="MobBatteryCharging6"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ebb2.png" alt-text="Screenshot of MobBatteryCharging7."::: | ebb2 | :::no-loc text="MobBatteryCharging7"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ebb3.png" alt-text="Screenshot of MobBatteryCharging8."::: | ebb3 | :::no-loc text="MobBatteryCharging8"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ebb4.png" alt-text="Screenshot of MobBatteryCharging9."::: | ebb4 | :::no-loc text="MobBatteryCharging9"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ebb5.png" alt-text="Screenshot of MobBatteryCharging10."::: | ebb5 | :::no-loc text="MobBatteryCharging10"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ebb6.png" alt-text="Screenshot of MobBatterySaver0."::: | ebb6 | :::no-loc text="MobBatterySaver0"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ebb7.png" alt-text="Screenshot of MobBatterySaver1."::: | ebb7 | :::no-loc text="MobBatterySaver1"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ebb8.png" alt-text="Screenshot of MobBatterySaver2."::: | ebb8 | :::no-loc text="MobBatterySaver2"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ebb9.png" alt-text="Screenshot of MobBatterySaver3."::: | ebb9 | :::no-loc text="MobBatterySaver3"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ebba.png" alt-text="Screenshot of MobBatterySaver4."::: | ebba | :::no-loc text="MobBatterySaver4"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ebbb.png" alt-text="Screenshot of MobBatterySaver5."::: | ebbb | :::no-loc text="MobBatterySaver5"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ebbc.png" alt-text="Screenshot of MobBatterySaver6."::: | ebbc | :::no-loc text="MobBatterySaver6"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ebbd.png" alt-text="Screenshot of MobBatterySaver7."::: | ebbd | :::no-loc text="MobBatterySaver7"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ebbe.png" alt-text="Screenshot of MobBatterySaver8."::: | ebbe | :::no-loc text="MobBatterySaver8"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ebbf.png" alt-text="Screenshot of MobBatterySaver9."::: | ebbf | :::no-loc text="MobBatterySaver9"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ebc0.png" alt-text="Screenshot of MobBatterySaver10."::: | ebc0 | :::no-loc text="MobBatterySaver10"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ebc3.png" alt-text="Screenshot of DictionaryCloud."::: | ebc3 | :::no-loc text="DictionaryCloud"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ebc4.png" alt-text="Screenshot of ResetDrive."::: | ebc4 | :::no-loc text="ResetDrive"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ebc5.png" alt-text="Screenshot of VolumeBars."::: | ebc5 | :::no-loc text="VolumeBars"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ebc6.png" alt-text="Screenshot of Project."::: | ebc6 | :::no-loc text="Project"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ebd2.png" alt-text="Screenshot of AdjustHologram."::: | ebd2 | :::no-loc text="AdjustHologram"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ebd3.png" alt-text="Screenshot of CloudDownload."::: | ebd3 | :::no-loc text="CloudDownload"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ebd4.png" alt-text="Screenshot of MobWifiCallBars."::: | ebd4 | :::no-loc text="MobWifiCallBars"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ebd5.png" alt-text="Screenshot of MobWifiCall0."::: | ebd5 | :::no-loc text="MobWifiCall0"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ebd6.png" alt-text="Screenshot of MobWifiCall1."::: | ebd6 | :::no-loc text="MobWifiCall1"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ebd7.png" alt-text="Screenshot of MobWifiCall2."::: | ebd7 | :::no-loc text="MobWifiCall2"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ebd8.png" alt-text="Screenshot of MobWifiCall3."::: | ebd8 | :::no-loc text="MobWifiCall3"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ebd9.png" alt-text="Screenshot of MobWifiCall4."::: | ebd9 | :::no-loc text="MobWifiCall4"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ebda.png" alt-text="Screenshot of Family."::: | ebda | :::no-loc text="Family"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ebdb.png" alt-text="Screenshot of LockFeedback."::: | ebdb | :::no-loc text="LockFeedback"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ebde.png" alt-text="Screenshot of DeviceDiscovery."::: | ebde | :::no-loc text="DeviceDiscovery"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ebe6.png" alt-text="Screenshot of WindDirection."::: | ebe6 | :::no-loc text="WindDirection"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ebe7.png" alt-text="Screenshot of RightArrowKeyTime0."::: | ebe7 | :::no-loc text="RightArrowKeyTime0"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ebe8.png" alt-text="Screenshot of Bug."::: | ebe8 | :::no-loc text="Bug"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ebfc.png" alt-text="Screenshot of TabletMode."::: | ebfc | :::no-loc text="TabletMode"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ebfd.png" alt-text="Screenshot of StatusCircleLeft."::: | ebfd | :::no-loc text="StatusCircleLeft"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ebfe.png" alt-text="Screenshot of StatusTriangleLeft."::: | ebfe | :::no-loc text="StatusTriangleLeft"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ebff.png" alt-text="Screenshot of StatusErrorLeft."::: | ebff | :::no-loc text="StatusErrorLeft"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec00.png" alt-text="Screenshot of StatusWarningLeft."::: | ec00 | :::no-loc text="StatusWarningLeft"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec02.png" alt-text="Screenshot of MobBatteryUnknown."::: | ec02 | :::no-loc text="MobBatteryUnknown"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec05.png" alt-text="Screenshot of NetworkTower."::: | ec05 | :::no-loc text="NetworkTower"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec06.png" alt-text="Screenshot of CityNext."::: | ec06 | :::no-loc text="CityNext"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec07.png" alt-text="Screenshot of CityNext2."::: | ec07 | :::no-loc text="CityNext2"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec08.png" alt-text="Screenshot of Courthouse."::: | ec08 | :::no-loc text="Courthouse"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec09.png" alt-text="Screenshot of Groceries."::: | ec09 | :::no-loc text="Groceries"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec0a.png" alt-text="Screenshot of Sustainable."::: | ec0a | :::no-loc text="Sustainable"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec0b.png" alt-text="Screenshot of BuildingEnergy."::: | ec0b | :::no-loc text="BuildingEnergy"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec11.png" alt-text="Screenshot of ToggleFilled."::: | ec11 | :::no-loc text="ToggleFilled"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec12.png" alt-text="Screenshot of ToggleBorder."::: | ec12 | :::no-loc text="ToggleBorder"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec13.png" alt-text="Screenshot of SliderThumb."::: | ec13 | :::no-loc text="SliderThumb"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec14.png" alt-text="Screenshot of ToggleThumb."::: | ec14 | :::no-loc text="ToggleThumb"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec15.png" alt-text="Screenshot of MiracastLogoSmall."::: | ec15 | :::no-loc text="MiracastLogoSmall"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec16.png" alt-text="Screenshot of MiracastLogoLarge."::: | ec16 | :::no-loc text="MiracastLogoLarge"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec19.png" alt-text="Screenshot of PLAP."::: | ec19 | :::no-loc text="PLAP"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec1b.png" alt-text="Screenshot of Badge."::: | ec1b | :::no-loc text="Badge"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec1e.png" alt-text="Screenshot of SignalRoaming."::: | ec1e | :::no-loc text="SignalRoaming"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec20.png" alt-text="Screenshot of MobileLocked."::: | ec20 | :::no-loc text="MobileLocked"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec24.png" alt-text="Screenshot of InsiderHubApp."::: | ec24 | :::no-loc text="InsiderHubApp"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec25.png" alt-text="Screenshot of PersonalFolder."::: | ec25 | :::no-loc text="PersonalFolder"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec26.png" alt-text="Screenshot of HomeGroup."::: | ec26 | :::no-loc text="HomeGroup"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec27.png" alt-text="Screenshot of MyNetwork."::: | ec27 | :::no-loc text="MyNetwork"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec31.png" alt-text="Screenshot of KeyboardFull."::: | ec31 | :::no-loc text="KeyboardFull"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec32.png" alt-text="Screenshot of Cafe."::: | ec32 | :::no-loc text="Cafe"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec34.png" alt-text="Screenshot of FormatText."::: | ec34 | :::no-loc text="FormatText"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec37.png" alt-text="Screenshot of MobSignal1."::: | ec37 | :::no-loc text="MobSignal1"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec38.png" alt-text="Screenshot of MobSignal2."::: | ec38 | :::no-loc text="MobSignal2"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec39.png" alt-text="Screenshot of MobSignal3."::: | ec39 | :::no-loc text="MobSignal3"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec3a.png" alt-text="Screenshot of MobSignal4."::: | ec3a | :::no-loc text="MobSignal4"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec3b.png" alt-text="Screenshot of MobSignal5."::: | ec3b | :::no-loc text="MobSignal5"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec3c.png" alt-text="Screenshot of MobWifi1."::: | ec3c | :::no-loc text="MobWifi1"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec3d.png" alt-text="Screenshot of MobWifi2."::: | ec3d | :::no-loc text="MobWifi2"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec3e.png" alt-text="Screenshot of MobWifi3."::: | ec3e | :::no-loc text="MobWifi3"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec3f.png" alt-text="Screenshot of MobWifi4."::: | ec3f | :::no-loc text="MobWifi4"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec40.png" alt-text="Screenshot of MobAirplane."::: | ec40 | :::no-loc text="MobAirplane"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec41.png" alt-text="Screenshot of MobBluetooth."::: | ec41 | :::no-loc text="MobBluetooth"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec42.png" alt-text="Screenshot of MobActionCenter."::: | ec42 | :::no-loc text="MobActionCenter"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec43.png" alt-text="Screenshot of MobLocation."::: | ec43 | :::no-loc text="MobLocation"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec44.png" alt-text="Screenshot of MobWifiHotspot."::: | ec44 | :::no-loc text="MobWifiHotspot"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec45.png" alt-text="Screenshot of LanguageJpn."::: | ec45 | :::no-loc text="LanguageJpn"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec46.png" alt-text="Screenshot of MobQuietHours."::: | ec46 | :::no-loc text="MobQuietHours"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec47.png" alt-text="Screenshot of MobDrivingMode."::: | ec47 | :::no-loc text="MobDrivingMode"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec48.png" alt-text="Screenshot of SpeedOff."::: | ec48 | :::no-loc text="SpeedOff"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec49.png" alt-text="Screenshot of SpeedMedium."::: | ec49 | :::no-loc text="SpeedMedium"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec4a.png" alt-text="Screenshot of SpeedHigh."::: | ec4a | :::no-loc text="SpeedHigh"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec4e.png" alt-text="Screenshot of ThisPC."::: | ec4e | :::no-loc text="ThisPC"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec4f.png" alt-text="Screenshot of MusicNote."::: | ec4f | :::no-loc text="MusicNote"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec50.png" alt-text="Screenshot of FileExplorer."::: | ec50 | :::no-loc text="FileExplorer"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec51.png" alt-text="Screenshot of FileExplorerApp."::: | ec51 | :::no-loc text="FileExplorerApp"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec52.png" alt-text="Screenshot of LeftArrowKeyTime0."::: | ec52 | :::no-loc text="LeftArrowKeyTime0"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec54.png" alt-text="Screenshot of MicOff."::: | ec54 | :::no-loc text="MicOff"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec55.png" alt-text="Screenshot of MicSleep."::: | ec55 | :::no-loc text="MicSleep"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec56.png" alt-text="Screenshot of MicError."::: | ec56 | :::no-loc text="MicError"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec57.png" alt-text="Screenshot of PlaybackRate1x."::: | ec57 | :::no-loc text="PlaybackRate1x"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec58.png" alt-text="Screenshot of PlaybackRateOther."::: | ec58 | :::no-loc text="PlaybackRateOther"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec59.png" alt-text="Screenshot of CashDrawer."::: | ec59 | :::no-loc text="CashDrawer"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec5a.png" alt-text="Screenshot of BarcodeScanner."::: | ec5a | :::no-loc text="BarcodeScanner"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec5b.png" alt-text="Screenshot of ReceiptPrinter."::: | ec5b | :::no-loc text="ReceiptPrinter"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec5c.png" alt-text="Screenshot of MagStripeReader."::: | ec5c | :::no-loc text="MagStripeReader"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec61.png" alt-text="Screenshot of CompletedSolid."::: | ec61 | :::no-loc text="CompletedSolid"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec64.png" alt-text="Screenshot of CompanionApp."::: | ec64 | :::no-loc text="CompanionApp"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec6c.png" alt-text="Screenshot of Favicon2."::: | ec6c | :::no-loc text="Favicon2"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec6d.png" alt-text="Screenshot of SwipeRevealArt."::: | ec6d | :::no-loc text="SwipeRevealArt"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec71.png" alt-text="Screenshot of MicOn."::: | ec71 | :::no-loc text="MicOn"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec72.png" alt-text="Screenshot of MicClipping."::: | ec72 | :::no-loc text="MicClipping"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec74.png" alt-text="Screenshot of TabletSelected."::: | ec74 | :::no-loc text="TabletSelected"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec75.png" alt-text="Screenshot of MobileSelected."::: | ec75 | :::no-loc text="MobileSelected"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec76.png" alt-text="Screenshot of LaptopSelected."::: | ec76 | :::no-loc text="LaptopSelected"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec77.png" alt-text="Screenshot of TVMonitorSelected."::: | ec77 | :::no-loc text="TVMonitorSelected"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec7a.png" alt-text="Screenshot of DeveloperTools."::: | ec7a | :::no-loc text="DeveloperTools"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec7e.png" alt-text="Screenshot of MobCallForwarding."::: | ec7e | :::no-loc text="MobCallForwarding"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec7f.png" alt-text="Screenshot of MobCallForwardingMirrored."::: | ec7f | :::no-loc text="MobCallForwardingMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec80.png" alt-text="Screenshot of BodyCam."::: | ec80 | :::no-loc text="BodyCam"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec81.png" alt-text="Screenshot of PoliceCar."::: | ec81 | :::no-loc text="PoliceCar"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec83.png" alt-text="Screenshot of UpdateStatusDot2."::: | ec83 | :::no-loc text="UpdateStatusDot2"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec87.png" alt-text="Screenshot of Draw."::: | ec87 | :::no-loc text="Draw"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec88.png" alt-text="Screenshot of DrawSolid."::: | ec88 | :::no-loc text="DrawSolid"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec8a.png" alt-text="Screenshot of LowerBrightness."::: | ec8a | :::no-loc text="LowerBrightness"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec8f.png" alt-text="Screenshot of ScrollUpDown."::: | ec8f | :::no-loc text="ScrollUpDown"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec91.png" alt-text="Screenshot of Uninstall."::: | ec91 | :::no-loc text="Uninstall"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec92.png" alt-text="Screenshot of DateTime."::: | ec92 | :::no-loc text="DateTime"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec94.png" alt-text="Screenshot of HoloLens."::: | ec94 | :::no-loc text="HoloLens"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ec9c.png" alt-text="Screenshot of CloudNotSynced."::: | ec9c | :::no-loc text="CloudNotSynced"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eca5.png" alt-text="Screenshot of Tiles."::: | eca5 | :::no-loc text="Tiles"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eca7.png" alt-text="Screenshot of PartyLeader."::: | eca7 | :::no-loc text="PartyLeader"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ecaa.png" alt-text="Screenshot of AppIconDefault."::: | ecaa | :::no-loc text="AppIconDefault"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ecad.png" alt-text="Screenshot of Calories."::: | ecad | :::no-loc text="Calories"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ecaf.png" alt-text="Screenshot of POI."::: | ecaf | :::no-loc text="POI"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ecb9.png" alt-text="Screenshot of BandBattery0."::: | ecb9 | :::no-loc text="BandBattery0"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ecba.png" alt-text="Screenshot of BandBattery1."::: | ecba | :::no-loc text="BandBattery1"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ecbb.png" alt-text="Screenshot of BandBattery2."::: | ecbb | :::no-loc text="BandBattery2"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ecbc.png" alt-text="Screenshot of BandBattery3."::: | ecbc | :::no-loc text="BandBattery3"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ecbd.png" alt-text="Screenshot of BandBattery4."::: | ecbd | :::no-loc text="BandBattery4"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ecbe.png" alt-text="Screenshot of BandBattery5."::: | ecbe | :::no-loc text="BandBattery5"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ecbf.png" alt-text="Screenshot of BandBattery6."::: | ecbf | :::no-loc text="BandBattery6"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ecc4.png" alt-text="Screenshot of AddSurfaceHub."::: | ecc4 | :::no-loc text="AddSurfaceHub"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ecc5.png" alt-text="Screenshot of DevUpdate."::: | ecc5 | :::no-loc text="DevUpdate"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ecc6.png" alt-text="Screenshot of Unit."::: | ecc6 | :::no-loc text="Unit"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ecc8.png" alt-text="Screenshot of AddTo."::: | ecc8 | :::no-loc text="AddTo"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ecc9.png" alt-text="Screenshot of RemoveFrom."::: | ecc9 | :::no-loc text="RemoveFrom"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ecca.png" alt-text="Screenshot of RadioBtnOff."::: | ecca | :::no-loc text="RadioBtnOff"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eccb.png" alt-text="Screenshot of RadioBtnOn."::: | eccb | :::no-loc text="RadioBtnOn"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eccc.png" alt-text="Screenshot of RadioBullet2."::: | eccc | :::no-loc text="RadioBullet2"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eccd.png" alt-text="Screenshot of ExploreContent."::: | eccd | :::no-loc text="ExploreContent"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ece4.png" alt-text="Screenshot of Blocked2."::: | ece4 | :::no-loc text="Blocked2"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ece7.png" alt-text="Screenshot of ScrollMode."::: | ece7 | :::no-loc text="ScrollMode"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ece8.png" alt-text="Screenshot of ZoomMode."::: | ece8 | :::no-loc text="ZoomMode"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ece9.png" alt-text="Screenshot of PanMode."::: | ece9 | :::no-loc text="PanMode"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ecf0.png" alt-text="Screenshot of WiredUSB."::: | ecf0 | :::no-loc text="WiredUSB"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ecf1.png" alt-text="Screenshot of WirelessUSB."::: | ecf1 | :::no-loc text="WirelessUSB"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ecf3.png" alt-text="Screenshot of USBSafeConnect."::: | ecf3 | :::no-loc text="USBSafeConnect"::: |

### PUA ED00-EF00

The following table of glyphs displays unicode points prefixed from ED- to EF-.

[Back to top](#icon-list)

| Glyph | Unicode point | Description |
|-------|---------------|-------------|
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ed0c.png" alt-text="Screenshot of ActionCenterNotificationMirrored."::: | ed0c | :::no-loc text="ActionCenterNotificationMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ed0d.png" alt-text="Screenshot of ActionCenterMirrored."::: | ed0d | :::no-loc text="ActionCenterMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ed0e.png" alt-text="Screenshot of SubscriptionAdd."::: | ed0e | :::no-loc text="SubscriptionAdd"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ed10.png" alt-text="Screenshot of ResetDevice."::: | ed10 | :::no-loc text="ResetDevice"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ed11.png" alt-text="Screenshot of SubscriptionAddMirrored."::: | ed11 | :::no-loc text="SubscriptionAddMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ed14.png" alt-text="Screenshot of QRCode."::: | ed14 | :::no-loc text="QRCode"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ed15.png" alt-text="Screenshot of Feedback."::: | ed15 | :::no-loc text="Feedback"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ed1a.png" alt-text="Screenshot of Hide."::: | ed1a | :::no-loc text="Hide"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ed1e.png" alt-text="Screenshot of Subtitles."::: | ed1e | :::no-loc text="Subtitles"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ed1f.png" alt-text="Screenshot of SubtitlesAudio."::: | ed1f | :::no-loc text="SubtitlesAudio"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ed21.png" alt-text="Screenshot of RestartUpdate2."::: | ed21 | :::no-loc text="RestartUpdate2"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ed25.png" alt-text="Screenshot of OpenFolderHorizontal."::: | ed25 | :::no-loc text="OpenFolderHorizontal"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ed28.png" alt-text="Screenshot of CalendarMirrored."::: | ed28 | :::no-loc text="CalendarMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ed2a.png" alt-text="Screenshot of MobeSIM."::: | ed2a | :::no-loc text="MobeSIM"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ed2b.png" alt-text="Screenshot of MobeSIMNoProfile."::: | ed2b | :::no-loc text="MobeSIMNoProfile"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ed2c.png" alt-text="Screenshot of MobeSIMLocked."::: | ed2c | :::no-loc text="MobeSIMLocked"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ed2d.png" alt-text="Screenshot of MobeSIMBusy."::: | ed2d | :::no-loc text="MobeSIMBusy"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ed2e.png" alt-text="Screenshot of SignalError."::: | ed2e | :::no-loc text="SignalError"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ed2f.png" alt-text="Screenshot of StreamingEnterprise."::: | ed2f | :::no-loc text="StreamingEnterprise"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ed30.png" alt-text="Screenshot of Headphone0."::: | ed30 | :::no-loc text="Headphone0"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ed31.png" alt-text="Screenshot of Headphone1."::: | ed31 | :::no-loc text="Headphone1"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ed32.png" alt-text="Screenshot of Headphone2."::: | ed32 | :::no-loc text="Headphone2"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ed33.png" alt-text="Screenshot of Headphone3."::: | ed33 | :::no-loc text="Headphone3"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ed35.png" alt-text="Screenshot of Apps."::: | ed35 | :::no-loc text="Apps"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ed39.png" alt-text="Screenshot of KeyboardBrightness."::: | ed39 | :::no-loc text="KeyboardBrightness"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ed3a.png" alt-text="Screenshot of KeyboardLowerBrightness."::: | ed3a | :::no-loc text="KeyboardLowerBrightness"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ed3c.png" alt-text="Screenshot of SkipBack10."::: | ed3c | :::no-loc text="SkipBack10"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ed3d.png" alt-text="Screenshot of SkipForward30."::: | ed3d | :::no-loc text="SkipForward30"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ed41.png" alt-text="Screenshot of TreeFolderFolder."::: | ed41 | :::no-loc text="TreeFolderFolder"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ed42.png" alt-text="Screenshot of TreeFolderFolderFill."::: | ed42 | :::no-loc text="TreeFolderFolderFill"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ed43.png" alt-text="Screenshot of TreeFolderFolderOpen."::: | ed43 | :::no-loc text="TreeFolderFolderOpen"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ed44.png" alt-text="Screenshot of TreeFolderFolderOpenFill."::: | ed44 | :::no-loc text="TreeFolderFolderOpenFill"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ed47.png" alt-text="Screenshot of MultimediaDMP."::: | ed47 | :::no-loc text="MultimediaDMP"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ed4c.png" alt-text="Screenshot of KeyboardOneHanded."::: | ed4c | :::no-loc text="KeyboardOneHanded"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ed4d.png" alt-text="Screenshot of Narrator."::: | ed4d | :::no-loc text="Narrator"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ed53.png" alt-text="Screenshot of EmojiTabPeople."::: | ed53 | :::no-loc text="EmojiTabPeople"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ed54.png" alt-text="Screenshot of EmojiTabSmilesAnimals."::: | ed54 | :::no-loc text="EmojiTabSmilesAnimals"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ed55.png" alt-text="Screenshot of EmojiTabCelebrationObjects."::: | ed55 | :::no-loc text="EmojiTabCelebrationObjects"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ed56.png" alt-text="Screenshot of EmojiTabFoodPlants."::: | ed56 | :::no-loc text="EmojiTabFoodPlants"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ed57.png" alt-text="Screenshot of EmojiTabTransitPlaces."::: | ed57 | :::no-loc text="EmojiTabTransitPlaces"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ed58.png" alt-text="Screenshot of EmojiTabSymbols."::: | ed58 | :::no-loc text="EmojiTabSymbols"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ed59.png" alt-text="Screenshot of EmojiTabTextSmiles."::: | ed59 | :::no-loc text="EmojiTabTextSmiles"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ed5a.png" alt-text="Screenshot of EmojiTabFavorites."::: | ed5a | :::no-loc text="EmojiTabFavorites"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ed5b.png" alt-text="Screenshot of EmojiSwatch."::: | ed5b | :::no-loc text="EmojiSwatch"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ed5c.png" alt-text="Screenshot of ConnectApp."::: | ed5c | :::no-loc text="ConnectApp"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ed5d.png" alt-text="Screenshot of CompanionDeviceFramework."::: | ed5d | :::no-loc text="CompanionDeviceFramework"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ed5e.png" alt-text="Screenshot of Ruler."::: | ed5e | :::no-loc text="Ruler"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ed5f.png" alt-text="Screenshot of FingerInking."::: | ed5f | :::no-loc text="FingerInking"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ed60.png" alt-text="Screenshot of StrokeErase."::: | ed60 | :::no-loc text="StrokeErase"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ed61.png" alt-text="Screenshot of PointErase."::: | ed61 | :::no-loc text="PointErase"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ed62.png" alt-text="Screenshot of ClearAllInk."::: | ed62 | :::no-loc text="ClearAllInk"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ed63.png" alt-text="Screenshot of Pencil."::: | ed63 | :::no-loc text="Pencil"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ed64.png" alt-text="Screenshot of Marker."::: | ed64 | :::no-loc text="Marker"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ed65.png" alt-text="Screenshot of InkingCaret."::: | ed65 | :::no-loc text="InkingCaret"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ed66.png" alt-text="Screenshot of InkingColorOutline."::: | ed66 | :::no-loc text="InkingColorOutline"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ed67.png" alt-text="Screenshot of InkingColorFill."::: | ed67 | :::no-loc text="InkingColorFill"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eda2.png" alt-text="Screenshot of HardDrive."::: | eda2 | :::no-loc text="HardDrive"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eda3.png" alt-text="Screenshot of NetworkAdapter."::: | eda3 | :::no-loc text="NetworkAdapter"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eda4.png" alt-text="Screenshot of Touchscreen."::: | eda4 | :::no-loc text="Touchscreen"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eda5.png" alt-text="Screenshot of NetworkPrinter."::: | eda5 | :::no-loc text="NetworkPrinter"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eda6.png" alt-text="Screenshot of CloudPrinter."::: | eda6 | :::no-loc text="CloudPrinter"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eda7.png" alt-text="Screenshot of KeyboardShortcut."::: | eda7 | :::no-loc text="KeyboardShortcut"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eda8.png" alt-text="Screenshot of BrushSize."::: | eda8 | :::no-loc text="BrushSize"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eda9.png" alt-text="Screenshot of NarratorForward."::: | eda9 | :::no-loc text="NarratorForward"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/edaa.png" alt-text="Screenshot of NarratorForwardMirrored."::: | edaa | :::no-loc text="NarratorForwardMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/edab.png" alt-text="Screenshot of SyncBadge12."::: | edab | :::no-loc text="SyncBadge12"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/edac.png" alt-text="Screenshot of RingerBadge12."::: | edac | :::no-loc text="RingerBadge12"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/edad.png" alt-text="Screenshot of AsteriskBadge12."::: | edad | :::no-loc text="AsteriskBadge12"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/edae.png" alt-text="Screenshot of ErrorBadge12."::: | edae | :::no-loc text="ErrorBadge12"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/edaf.png" alt-text="Screenshot of CircleRingBadge12."::: | edaf | :::no-loc text="CircleRingBadge12"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/edb0.png" alt-text="Screenshot of CircleFillBadge12."::: | edb0 | :::no-loc text="CircleFillBadge12"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/edb1.png" alt-text="Screenshot of ImportantBadge12."::: | edb1 | :::no-loc text="ImportantBadge12"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/edb3.png" alt-text="Screenshot of MailBadge12."::: | edb3 | :::no-loc text="MailBadge12"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/edb4.png" alt-text="Screenshot of PauseBadge12."::: | edb4 | :::no-loc text="PauseBadge12"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/edb5.png" alt-text="Screenshot of PlayBadge12."::: | edb5 | :::no-loc text="PlayBadge12"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/edc6.png" alt-text="Screenshot of PenWorkspace."::: | edc6 | :::no-loc text="PenWorkspace"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/edd5.png" alt-text="Screenshot of CaretLeft8."::: | edd5 | :::no-loc text="CaretLeft8"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/edd6.png" alt-text="Screenshot of CaretRight8."::: | edd6 | :::no-loc text="CaretRight8"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/edd7.png" alt-text="Screenshot of CaretUp8."::: | edd7 | :::no-loc text="CaretUp8"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/edd8.png" alt-text="Screenshot of CaretDown8."::: | edd8 | :::no-loc text="CaretDown8"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/edd9.png" alt-text="Screenshot of CaretLeftSolid8."::: | edd9 | :::no-loc text="CaretLeftSolid8"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/edda.png" alt-text="Screenshot of CaretRightSolid8."::: | edda | :::no-loc text="CaretRightSolid8"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eddb.png" alt-text="Screenshot of CaretUpSolid8."::: | eddb | :::no-loc text="CaretUpSolid8"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eddc.png" alt-text="Screenshot of CaretDownSolid8."::: | eddc | :::no-loc text="CaretDownSolid8"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ede0.png" alt-text="Screenshot of Strikethrough."::: | ede0 | :::no-loc text="Strikethrough"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ede1.png" alt-text="Screenshot of Export."::: | ede1 | :::no-loc text="Export"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ede2.png" alt-text="Screenshot of ExportMirrored."::: | ede2 | :::no-loc text="ExportMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ede3.png" alt-text="Screenshot of ButtonMenu."::: | ede3 | :::no-loc text="ButtonMenu"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ede4.png" alt-text="Screenshot of CloudSearch."::: | ede4 | :::no-loc text="CloudSearch"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ede5.png" alt-text="Screenshot of PinyinIMELogo."::: | ede5 | :::no-loc text="PinyinIMELogo"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/edfb.png" alt-text="Screenshot of CalligraphyPen."::: | edfb | :::no-loc text="CalligraphyPen"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ee35.png" alt-text="Screenshot of ReplyMirrored."::: | ee35 | :::no-loc text="ReplyMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ee3f.png" alt-text="Screenshot of LockscreenDesktop."::: | ee3f | :::no-loc text="LockscreenDesktop"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ee40.png" alt-text="Screenshot of TaskViewSettings."::: | ee40 | :::no-loc text="TaskViewSettings"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ee41.png" alt-text="Screenshot of FullHiraganaPrivateMode."::: | ee41 | :::no-loc text="FullHiraganaPrivateMode"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ee42.png" alt-text="Screenshot of FullKatakanaPrivateMode."::: | ee42 | :::no-loc text="FullKatakanaPrivateMode"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ee43.png" alt-text="Screenshot of HalfAlphaPrivateMode."::: | ee43 | :::no-loc text="HalfAlphaPrivateMode"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ee44.png" alt-text="Screenshot of HalfKatakanaPrivateMode."::: | ee44 | :::no-loc text="HalfKatakanaPrivateMode"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ee45.png" alt-text="Screenshot of FullAlphaPrivateMode."::: | ee45 | :::no-loc text="FullAlphaPrivateMode"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ee47.png" alt-text="Screenshot of MiniExpand2Mirrored."::: | ee47 | :::no-loc text="MiniExpand2Mirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ee49.png" alt-text="Screenshot of MiniContract2Mirrored."::: | ee49 | :::no-loc text="MiniContract2Mirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ee4a.png" alt-text="Screenshot of Play36."::: | ee4a | :::no-loc text="Play36"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ee56.png" alt-text="Screenshot of PenPalette."::: | ee56 | :::no-loc text="PenPalette"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ee57.png" alt-text="Screenshot of GuestUser."::: | ee57 | :::no-loc text="GuestUser"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ee63.png" alt-text="Screenshot of SettingsBattery."::: | ee63 | :::no-loc text="SettingsBattery"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ee64.png" alt-text="Screenshot of TaskbarPhone."::: | ee64 | :::no-loc text="TaskbarPhone"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ee65.png" alt-text="Screenshot of LockScreenGlance."::: | ee65 | :::no-loc text="LockScreenGlance"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ee6f.png" alt-text="Screenshot of GenericScan."::: | ee6f | :::no-loc text="GenericScan"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ee71.png" alt-text="Screenshot of ImageExport."::: | ee71 | :::no-loc text="ImageExport"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ee77.png" alt-text="Screenshot of WifiEthernet."::: | ee77 | :::no-loc text="WifiEthernet"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ee79.png" alt-text="Screenshot of ActionCenterQuiet."::: | ee79 | :::no-loc text="ActionCenterQuiet"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ee7a.png" alt-text="Screenshot of ActionCenterQuietNotification."::: | ee7a | :::no-loc text="ActionCenterQuietNotification"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ee7e.png" alt-text="Screenshot of FIDOPasskey."::: | ee7e | :::no-loc text="FIDOPasskey"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ee92.png" alt-text="Screenshot of TrackersMirrored."::: | ee92 | :::no-loc text="TrackersMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ee93.png" alt-text="Screenshot of DateTimeMirrored."::: | ee93 | :::no-loc text="DateTimeMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ee94.png" alt-text="Screenshot of Wheel."::: | ee94 | :::no-loc text="Wheel"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ee95.png" alt-text="Screenshot of StopSolid."::: | ee95 | :::no-loc text="StopSolid"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eea0.png" alt-text="Screenshot of RAM."::: | eea0 | :::no-loc text="RAM"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eea1.png" alt-text="Screenshot of CPU."::: | eea1 | :::no-loc text="CPU"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eea3.png" alt-text="Screenshot of VirtualMachineGroup."::: | eea3 | :::no-loc text="VirtualMachineGroup"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/eeca.png" alt-text="Screenshot of ButtonView2."::: | eeca | :::no-loc text="ButtonView2"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ef15.png" alt-text="Screenshot of PenWorkspaceMirrored."::: | ef15 | :::no-loc text="PenWorkspaceMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ef16.png" alt-text="Screenshot of PenPaletteMirrored."::: | ef16 | :::no-loc text="PenPaletteMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ef17.png" alt-text="Screenshot of StrokeEraseMirrored."::: | ef17 | :::no-loc text="StrokeEraseMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ef18.png" alt-text="Screenshot of PointEraseMirrored."::: | ef18 | :::no-loc text="PointEraseMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ef19.png" alt-text="Screenshot of ClearAllInkMirrored."::: | ef19 | :::no-loc text="ClearAllInkMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ef1f.png" alt-text="Screenshot of BackgroundToggle."::: | ef1f | :::no-loc text="BackgroundToggle"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ef20.png" alt-text="Screenshot of Marquee."::: | ef20 | :::no-loc text="Marquee"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ef2c.png" alt-text="Screenshot of ChromeCloseContrast."::: | ef2c | :::no-loc text="ChromeCloseContrast"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ef2d.png" alt-text="Screenshot of ChromeMinimizeContrast."::: | ef2d | :::no-loc text="ChromeMinimizeContrast"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ef2e.png" alt-text="Screenshot of ChromeMaximizeContrast."::: | ef2e | :::no-loc text="ChromeMaximizeContrast"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ef2f.png" alt-text="Screenshot of ChromeRestoreContrast."::: | ef2f | :::no-loc text="ChromeRestoreContrast"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ef31.png" alt-text="Screenshot of TrafficLight."::: | ef31 | :::no-loc text="TrafficLight"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ef3b.png" alt-text="Screenshot of Replay."::: | ef3b | :::no-loc text="Replay"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ef3c.png" alt-text="Screenshot of Eyedropper."::: | ef3c | :::no-loc text="Eyedropper"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ef3d.png" alt-text="Screenshot of LineDisplay."::: | ef3d | :::no-loc text="LineDisplay"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ef3e.png" alt-text="Screenshot of PINPad."::: | ef3e | :::no-loc text="PINPad"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ef3f.png" alt-text="Screenshot of SignatureCapture."::: | ef3f | :::no-loc text="SignatureCapture"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ef40.png" alt-text="Screenshot of ChipCardCreditCardReader."::: | ef40 | :::no-loc text="ChipCardCreditCardReader"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ef42.png" alt-text="Screenshot of MarketDown."::: | ef42 | :::no-loc text="MarketDown"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ef58.png" alt-text="Screenshot of PlayerSettings."::: | ef58 | :::no-loc text="PlayerSettings"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ef60.png" alt-text="Screenshot of TextEdit."::: | ef60 | :::no-loc text="TextEdit"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ef6b.png" alt-text="Screenshot of LandscapeOrientation."::: | ef6b | :::no-loc text="LandscapeOrientation"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/ef90.png" alt-text="Screenshot of Flow."::: | ef90 | :::no-loc text="Flow"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/efa5.png" alt-text="Screenshot of Touchpad."::: | efa5 | :::no-loc text="Touchpad"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/efa9.png" alt-text="Screenshot of Speech."::: | efa9 | :::no-loc text="Speech"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/efda.png" alt-text="Screenshot of AppIconDefaultAdd."::: | efda | :::no-loc text="AppIconDefaultAdd"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/efff.png" alt-text="Screenshot of CRMScheduleReports."::: | efff | :::no-loc text="CRMScheduleReports"::: |

### PUA F000-F200

The following table of glyphs displays unicode points prefixed from F0- to F2-.

[Back to top](#icon-list)

| Glyph | Unicode point | Description |
|-------|---------------|-------------|
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f000.png" alt-text="Screenshot of KnowledgeArticle."::: | f000 | :::no-loc text="KnowledgeArticle"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f003.png" alt-text="Screenshot of Relationship."::: | f003 | :::no-loc text="Relationship"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f012.png" alt-text="Screenshot of ZipFolder."::: | f012 | :::no-loc text="ZipFolder"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f080.png" alt-text="Screenshot of DefaultAPN."::: | f080 | :::no-loc text="DefaultAPN"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f081.png" alt-text="Screenshot of UserAPN."::: | f081 | :::no-loc text="UserAPN"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f085.png" alt-text="Screenshot of DoublePinyin."::: | f085 | :::no-loc text="DoublePinyin"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f08c.png" alt-text="Screenshot of BlueLight."::: | f08c | :::no-loc text="BlueLight"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f08d.png" alt-text="Screenshot of CaretSolidLeft."::: | f08d | :::no-loc text="CaretSolidLeft"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f08e.png" alt-text="Screenshot of CaretSolidDown."::: | f08e | :::no-loc text="CaretSolidDown"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f08f.png" alt-text="Screenshot of CaretSolidRight."::: | f08f | :::no-loc text="CaretSolidRight"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f090.png" alt-text="Screenshot of CaretSolidUp."::: | f090 | :::no-loc text="CaretSolidUp"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f093.png" alt-text="Screenshot of ButtonA."::: | f093 | :::no-loc text="ButtonA"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f094.png" alt-text="Screenshot of ButtonB."::: | f094 | :::no-loc text="ButtonB"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f095.png" alt-text="Screenshot of ButtonY."::: | f095 | :::no-loc text="ButtonY"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f096.png" alt-text="Screenshot of ButtonX."::: | f096 | :::no-loc text="ButtonX"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f0ad.png" alt-text="Screenshot of ArrowUp8."::: | f0ad | :::no-loc text="ArrowUp8"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f0ae.png" alt-text="Screenshot of ArrowDown8."::: | f0ae | :::no-loc text="ArrowDown8"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f0af.png" alt-text="Screenshot of ArrowRight8."::: | f0af | :::no-loc text="ArrowRight8"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f0b0.png" alt-text="Screenshot of ArrowLeft8."::: | f0b0 | :::no-loc text="ArrowLeft8"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f0b2.png" alt-text="Screenshot of QuarentinedItems."::: | f0b2 | :::no-loc text="QuarentinedItems"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f0b3.png" alt-text="Screenshot of QuarentinedItemsMirrored."::: | f0b3 | :::no-loc text="QuarentinedItemsMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f0b4.png" alt-text="Screenshot of Protractor."::: | f0b4 | :::no-loc text="Protractor"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f0b5.png" alt-text="Screenshot of ChecklistMirrored."::: | f0b5 | :::no-loc text="ChecklistMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f0b6.png" alt-text="Screenshot of StatusCircle7."::: | f0b6 | :::no-loc text="StatusCircle7"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f0b7.png" alt-text="Screenshot of StatusCheckmark7."::: | f0b7 | :::no-loc text="StatusCheckmark7"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f0b8.png" alt-text="Screenshot of StatusErrorCircle7."::: | f0b8 | :::no-loc text="StatusErrorCircle7"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f0b9.png" alt-text="Screenshot of Connected."::: | f0b9 | :::no-loc text="Connected"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f0c6.png" alt-text="Screenshot of PencilFill."::: | f0c6 | :::no-loc text="PencilFill"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f0c7.png" alt-text="Screenshot of CalligraphyFill."::: | f0c7 | :::no-loc text="CalligraphyFill"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f0ca.png" alt-text="Screenshot of QuarterStarLeft."::: | f0ca | :::no-loc text="QuarterStarLeft"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f0cb.png" alt-text="Screenshot of QuarterStarRight."::: | f0cb | :::no-loc text="QuarterStarRight"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f0cc.png" alt-text="Screenshot of ThreeQuarterStarLeft."::: | f0cc | :::no-loc text="ThreeQuarterStarLeft"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f0cd.png" alt-text="Screenshot of ThreeQuarterStarRight."::: | f0cd | :::no-loc text="ThreeQuarterStarRight"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f0ce.png" alt-text="Screenshot of QuietHoursBadge12."::: | f0ce | :::no-loc text="QuietHoursBadge12"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f0d2.png" alt-text="Screenshot of BackMirrored."::: | f0d2 | :::no-loc text="BackMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f0d3.png" alt-text="Screenshot of ForwardMirrored."::: | f0d3 | :::no-loc text="ForwardMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f0d5.png" alt-text="Screenshot of ChromeBackContrast."::: | f0d5 | :::no-loc text="ChromeBackContrast"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f0d6.png" alt-text="Screenshot of ChromeBackContrastMirrored."::: | f0d6 | :::no-loc text="ChromeBackContrastMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f0d7.png" alt-text="Screenshot of ChromeBackToWindowContrast."::: | f0d7 | :::no-loc text="ChromeBackToWindowContrast"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f0d8.png" alt-text="Screenshot of ChromeFullScreenContrast."::: | f0d8 | :::no-loc text="ChromeFullScreenContrast"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f0e2.png" alt-text="Screenshot of GridView."::: | f0e2 | :::no-loc text="GridView"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f0e3.png" alt-text="Screenshot of ClipboardList."::: | f0e3 | :::no-loc text="ClipboardList"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f0e4.png" alt-text="Screenshot of ClipboardListMirrored."::: | f0e4 | :::no-loc text="ClipboardListMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f0e5.png" alt-text="Screenshot of OutlineQuarterStarLeft."::: | f0e5 | :::no-loc text="OutlineQuarterStarLeft"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f0e6.png" alt-text="Screenshot of OutlineQuarterStarRight."::: | f0e6 | :::no-loc text="OutlineQuarterStarRight"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f0e7.png" alt-text="Screenshot of OutlineHalfStarLeft."::: | f0e7 | :::no-loc text="OutlineHalfStarLeft"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f0e8.png" alt-text="Screenshot of OutlineHalfStarRight."::: | f0e8 | :::no-loc text="OutlineHalfStarRight"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f0e9.png" alt-text="Screenshot of OutlineThreeQuarterStarLeft."::: | f0e9 | :::no-loc text="OutlineThreeQuarterStarLeft"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f0ea.png" alt-text="Screenshot of OutlineThreeQuarterStarRight."::: | f0ea | :::no-loc text="OutlineThreeQuarterStarRight"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f0eb.png" alt-text="Screenshot of SpatialVolume0."::: | f0eb | :::no-loc text="SpatialVolume0"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f0ec.png" alt-text="Screenshot of SpatialVolume1."::: | f0ec | :::no-loc text="SpatialVolume1"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f0ed.png" alt-text="Screenshot of SpatialVolume2."::: | f0ed | :::no-loc text="SpatialVolume2"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f0ee.png" alt-text="Screenshot of SpatialVolume3."::: | f0ee | :::no-loc text="SpatialVolume3"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f0ef.png" alt-text="Screenshot of ApplicationGuard."::: | f0ef | :::no-loc text="ApplicationGuard"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f0f7.png" alt-text="Screenshot of OutlineStarLeftHalf."::: | f0f7 | :::no-loc text="OutlineStarLeftHalf"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f0f8.png" alt-text="Screenshot of OutlineStarRightHalf."::: | f0f8 | :::no-loc text="OutlineStarRightHalf"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f0f9.png" alt-text="Screenshot of ChromeAnnotateContrast."::: | f0f9 | :::no-loc text="ChromeAnnotateContrast"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f0fb.png" alt-text="Screenshot of DefenderBadge12."::: | f0fb | :::no-loc text="DefenderBadge12"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f103.png" alt-text="Screenshot of DetachablePC."::: | f103 | :::no-loc text="DetachablePC"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f108.png" alt-text="Screenshot of LeftStick."::: | f108 | :::no-loc text="LeftStick"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f109.png" alt-text="Screenshot of RightStick."::: | f109 | :::no-loc text="RightStick"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f10a.png" alt-text="Screenshot of TriggerLeft."::: | f10a | :::no-loc text="TriggerLeft"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f10b.png" alt-text="Screenshot of TriggerRight."::: | f10b | :::no-loc text="TriggerRight"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f10c.png" alt-text="Screenshot of BumperLeft."::: | f10c | :::no-loc text="BumperLeft"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f10d.png" alt-text="Screenshot of BumperRight."::: | f10d | :::no-loc text="BumperRight"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f10e.png" alt-text="Screenshot of Dpad."::: | f10e | :::no-loc text="Dpad"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f110.png" alt-text="Screenshot of EnglishPunctuation."::: | f110 | :::no-loc text="EnglishPunctuation"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f111.png" alt-text="Screenshot of ChinesePunctuation."::: | f111 | :::no-loc text="ChinesePunctuation"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f119.png" alt-text="Screenshot of HMD."::: | f119 | :::no-loc text="HMD"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f11b.png" alt-text="Screenshot of CtrlSpatialRight."::: | f11b | :::no-loc text="CtrlSpatialRight"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f126.png" alt-text="Screenshot of PaginationDotOutline10."::: | f126 | :::no-loc text="PaginationDotOutline10"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f127.png" alt-text="Screenshot of PaginationDotSolid10."::: | f127 | :::no-loc text="PaginationDotSolid10"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f128.png" alt-text="Screenshot of StrokeErase2."::: | f128 | :::no-loc text="StrokeErase2"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f129.png" alt-text="Screenshot of SmallErase."::: | f129 | :::no-loc text="SmallErase"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f12a.png" alt-text="Screenshot of LargeErase."::: | f12a | :::no-loc text="LargeErase"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f12b.png" alt-text="Screenshot of FolderHorizontal."::: | f12b | :::no-loc text="FolderHorizontal"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f12e.png" alt-text="Screenshot of MicrophoneListening."::: | f12e | :::no-loc text="MicrophoneListening"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f12f.png" alt-text="Screenshot of StatusExclamationCircle7."::: | f12f | :::no-loc text="StatusExclamationCircle7"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f131.png" alt-text="Screenshot of Video360."::: | f131 | :::no-loc text="Video360"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f133.png" alt-text="Screenshot of GiftboxOpen."::: | f133 | :::no-loc text="GiftboxOpen"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f136.png" alt-text="Screenshot of StatusCircleOuter."::: | f136 | :::no-loc text="StatusCircleOuter"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f137.png" alt-text="Screenshot of StatusCircleInner."::: | f137 | :::no-loc text="StatusCircleInner"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f138.png" alt-text="Screenshot of StatusCircleRing."::: | f138 | :::no-loc text="StatusCircleRing"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f139.png" alt-text="Screenshot of StatusTriangleOuter."::: | f139 | :::no-loc text="StatusTriangleOuter"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f13a.png" alt-text="Screenshot of StatusTriangleInner."::: | f13a | :::no-loc text="StatusTriangleInner"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f13b.png" alt-text="Screenshot of StatusTriangleExclamation."::: | f13b | :::no-loc text="StatusTriangleExclamation"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f13c.png" alt-text="Screenshot of StatusCircleExclamation."::: | f13c | :::no-loc text="StatusCircleExclamation"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f13d.png" alt-text="Screenshot of StatusCircleErrorX."::: | f13d | :::no-loc text="StatusCircleErrorX"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f13e.png" alt-text="Screenshot of StatusCircleCheckmark."::: | f13e | :::no-loc text="StatusCircleCheckmark"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f13f.png" alt-text="Screenshot of StatusCircleInfo."::: | f13f | :::no-loc text="StatusCircleInfo"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f140.png" alt-text="Screenshot of StatusCircleBlock."::: | f140 | :::no-loc text="StatusCircleBlock"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f141.png" alt-text="Screenshot of StatusCircleBlock2."::: | f141 | :::no-loc text="StatusCircleBlock2"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f142.png" alt-text="Screenshot of StatusCircleQuestionMark."::: | f142 | :::no-loc text="StatusCircleQuestionMark"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f143.png" alt-text="Screenshot of StatusCircleSync."::: | f143 | :::no-loc text="StatusCircleSync"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f146.png" alt-text="Screenshot of Dial1."::: | f146 | :::no-loc text="Dial1"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f147.png" alt-text="Screenshot of Dial2."::: | f147 | :::no-loc text="Dial2"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f148.png" alt-text="Screenshot of Dial3."::: | f148 | :::no-loc text="Dial3"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f149.png" alt-text="Screenshot of Dial4."::: | f149 | :::no-loc text="Dial4"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f14a.png" alt-text="Screenshot of Dial5."::: | f14a | :::no-loc text="Dial5"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f14b.png" alt-text="Screenshot of Dial6."::: | f14b | :::no-loc text="Dial6"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f14c.png" alt-text="Screenshot of Dial7."::: | f14c | :::no-loc text="Dial7"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f14d.png" alt-text="Screenshot of Dial8."::: | f14d | :::no-loc text="Dial8"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f14e.png" alt-text="Screenshot of Dial9."::: | f14e | :::no-loc text="Dial9"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f14f.png" alt-text="Screenshot of Dial10."::: | f14f | :::no-loc text="Dial10"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f150.png" alt-text="Screenshot of Dial11."::: | f150 | :::no-loc text="Dial11"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f151.png" alt-text="Screenshot of Dial12."::: | f151 | :::no-loc text="Dial12"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f152.png" alt-text="Screenshot of Dial13."::: | f152 | :::no-loc text="Dial13"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f153.png" alt-text="Screenshot of Dial14."::: | f153 | :::no-loc text="Dial14"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f154.png" alt-text="Screenshot of Dial15."::: | f154 | :::no-loc text="Dial15"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f155.png" alt-text="Screenshot of Dial16."::: | f155 | :::no-loc text="Dial16"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f156.png" alt-text="Screenshot of DialShape1."::: | f156 | :::no-loc text="DialShape1"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f157.png" alt-text="Screenshot of DialShape2."::: | f157 | :::no-loc text="DialShape2"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f158.png" alt-text="Screenshot of DialShape3."::: | f158 | :::no-loc text="DialShape3"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f159.png" alt-text="Screenshot of DialShape4."::: | f159 | :::no-loc text="DialShape4"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f15f.png" alt-text="Screenshot of ClosedCaptionsInternational."::: | f15f | :::no-loc text="ClosedCaptionsInternational"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f161.png" alt-text="Screenshot of TollSolid."::: | f161 | :::no-loc text="TollSolid"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f163.png" alt-text="Screenshot of TrafficCongestionSolid."::: | f163 | :::no-loc text="TrafficCongestionSolid"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f164.png" alt-text="Screenshot of ExploreContentSingle."::: | f164 | :::no-loc text="ExploreContentSingle"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f165.png" alt-text="Screenshot of CollapseContent."::: | f165 | :::no-loc text="CollapseContent"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f166.png" alt-text="Screenshot of CollapseContentSingle."::: | f166 | :::no-loc text="CollapseContentSingle"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f167.png" alt-text="Screenshot of InfoSolid."::: | f167 | :::no-loc text="InfoSolid"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f168.png" alt-text="Screenshot of GroupList."::: | f168 | :::no-loc text="GroupList"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f169.png" alt-text="Screenshot of CaretBottomRightSolidCenter8."::: | f169 | :::no-loc text="CaretBottomRightSolidCenter8"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f16a.png" alt-text="Screenshot of ProgressRingDots."::: | f16a | :::no-loc text="ProgressRingDots"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f16b.png" alt-text="Screenshot of Checkbox14."::: | f16b | :::no-loc text="Checkbox14"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f16c.png" alt-text="Screenshot of CheckboxComposite14."::: | f16c | :::no-loc text="CheckboxComposite14"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f16d.png" alt-text="Screenshot of CheckboxIndeterminateCombo14."::: | f16d | :::no-loc text="CheckboxIndeterminateCombo14"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f16e.png" alt-text="Screenshot of CheckboxIndeterminateCombo."::: | f16e | :::no-loc text="CheckboxIndeterminateCombo"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f175.png" alt-text="Screenshot of StatusPause7."::: | f175 | :::no-loc text="StatusPause7"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f17f.png" alt-text="Screenshot of CharacterAppearance."::: | f17f | :::no-loc text="CharacterAppearance"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f180.png" alt-text="Screenshot of Lexicon."::: | f180 | :::no-loc text="Lexicon"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f182.png" alt-text="Screenshot of ScreenTime."::: | f182 | :::no-loc text="ScreenTime"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f191.png" alt-text="Screenshot of HeadlessDevice."::: | f191 | :::no-loc text="HeadlessDevice"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f193.png" alt-text="Screenshot of NetworkSharing."::: | f193 | :::no-loc text="NetworkSharing"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f19d.png" alt-text="Screenshot of EyeGaze."::: | f19d | :::no-loc text="EyeGaze"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f19e.png" alt-text="Screenshot of ToggleLeft."::: | f19e | :::no-loc text="ToggleLeft"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f19f.png" alt-text="Screenshot of ToggleRight."::: | f19f | :::no-loc text="ToggleRight"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f1ad.png" alt-text="Screenshot of WindowsInsider."::: | f1ad | :::no-loc text="WindowsInsider"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f1cb.png" alt-text="Screenshot of ChromeSwitch."::: | f1cb | :::no-loc text="ChromeSwitch"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f1cc.png" alt-text="Screenshot of ChromeSwitchContast."::: | f1cc | :::no-loc text="ChromeSwitchContast"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f1d8.png" alt-text="Screenshot of StatusCheckmark."::: | f1d8 | :::no-loc text="StatusCheckmark"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f1d9.png" alt-text="Screenshot of StatusCheckmarkLeft."::: | f1d9 | :::no-loc text="StatusCheckmarkLeft"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f20c.png" alt-text="Screenshot of KeyboardLeftAligned."::: | f20c | :::no-loc text="KeyboardLeftAligned"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f20d.png" alt-text="Screenshot of KeyboardRightAligned."::: | f20d | :::no-loc text="KeyboardRightAligned"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f210.png" alt-text="Screenshot of KeyboardSettings."::: | f210 | :::no-loc text="KeyboardSettings"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f211.png" alt-text="Screenshot of NetworkPhysical."::: | f211 | :::no-loc text="NetworkPhysical"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f22c.png" alt-text="Screenshot of IOT."::: | f22c | :::no-loc text="IOT"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f22e.png" alt-text="Screenshot of UnknownMirrored."::: | f22e | :::no-loc text="UnknownMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f246.png" alt-text="Screenshot of ViewDashboard."::: | f246 | :::no-loc text="ViewDashboard"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f259.png" alt-text="Screenshot of ExploitProtectionSettings."::: | f259 | :::no-loc text="ExploitProtectionSettings"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f260.png" alt-text="Screenshot of KeyboardNarrow."::: | f260 | :::no-loc text="KeyboardNarrow"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f261.png" alt-text="Screenshot of Keyboard12Key."::: | f261 | :::no-loc text="Keyboard12Key"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f26b.png" alt-text="Screenshot of KeyboardDock."::: | f26b | :::no-loc text="KeyboardDock"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f26c.png" alt-text="Screenshot of KeyboardUndock."::: | f26c | :::no-loc text="KeyboardUndock"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f26d.png" alt-text="Screenshot of KeyboardLeftDock."::: | f26d | :::no-loc text="KeyboardLeftDock"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f26e.png" alt-text="Screenshot of KeyboardRightDock."::: | f26e | :::no-loc text="KeyboardRightDock"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f270.png" alt-text="Screenshot of Ear."::: | f270 | :::no-loc text="Ear"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f271.png" alt-text="Screenshot of PointerHand."::: | f271 | :::no-loc text="PointerHand"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f272.png" alt-text="Screenshot of Bullseye."::: | f272 | :::no-loc text="Bullseye"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f28b.png" alt-text="Screenshot of DocumentApproval."::: | f28b | :::no-loc text="DocumentApproval"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f2b7.png" alt-text="Screenshot of LocaleLanguage."::: | f2b7 | :::no-loc text="LocaleLanguage"::: |
  
### PUA F300-F500

The following table of glyphs displays unicode points prefixed from F3- to F5-.

[Back to top](#icon-list)

| Glyph | Unicode point | Description |
|-------|---------------|-------------|
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f112.png" alt-text="Screenshot of ReadOutLoud."::: | f112 | :::no-loc text="ReadOutLoud"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f117.png" alt-text="Screenshot of ProjectToDevice."::: | f117 | :::no-loc text="ProjectToDevice"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f120.png" alt-text="Screenshot of TaskManagerApp."::: | f120 | :::no-loc text="TaskManagerApp"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f191.png" alt-text="Screenshot of HeadlessDevice."::: | f191 | :::no-loc text="HeadlessDevice"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f196.png" alt-text="Screenshot of Beaker."::: | f196 | :::no-loc text="Beaker"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f1ad.png" alt-text="Screenshot of WindowsInsider."::: | f1ad | :::no-loc text="WindowsInsider"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f1b1.png" alt-text="Screenshot of PowerButtonUpdate2."::: | f1b1 | :::no-loc text="PowerButtonUpdate2"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f1e8.png" alt-text="Screenshot of LeafTwo."::: | f1e8 | :::no-loc text="LeafTwo"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f232.png" alt-text="Screenshot of GridViewSmall."::: | f232 | :::no-loc text="GridViewSmall"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f27c.png" alt-text="Screenshot of Earbudsingle."::: | f27c | :::no-loc text="Earbudsingle"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f27f.png" alt-text="Screenshot of HearingAid."::: | f27f | :::no-loc text="HearingAid"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f285.png" alt-text="Screenshot of MobSnooze."::: | f285 | :::no-loc text="MobSnooze"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f2a3.png" alt-text="Screenshot of MobNotificationBell."::: | f2a3 | :::no-loc text="MobNotificationBell"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f2a5.png" alt-text="Screenshot of MobNotificationBellFilled."::: | f2a5 | :::no-loc text="MobNotificationBellFilled"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f2a8.png" alt-text="Screenshot of MobSnoozeFilled."::: | f2a8 | :::no-loc text="MobSnoozeFilled"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f2c7.png" alt-text="Screenshot of BulletedList2."::: | f2c7 | :::no-loc text="BulletedList2"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f2c8.png" alt-text="Screenshot of BulletedList2Mirrored."::: | f2c8 | :::no-loc text="BulletedList2Mirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f2d9.png" alt-text="Screenshot of CirclePause."::: | f2d9 | :::no-loc text="CirclePause"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f32a.png" alt-text="Screenshot of PassiveAuthentication."::: | f32a | :::no-loc text="PassiveAuthentication"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f354.png" alt-text="Screenshot of ColorSolid."::: | f354 | :::no-loc text="ColorSolid"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f384.png" alt-text="Screenshot of NetworkOffline."::: | f384 | :::no-loc text="NetworkOffline"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f385.png" alt-text="Screenshot of NetworkConnected."::: | f385 | :::no-loc text="NetworkConnected"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f386.png" alt-text="Screenshot of NetworkConnectedCheckmark."::: | f386 | :::no-loc text="NetworkConnectedCheckmark"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f3b1.png" alt-text="Screenshot of SignOut."::: | f3b1 | :::no-loc text="SignOut"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f3cc.png" alt-text="Screenshot of StatusInfo."::: | f3cc | :::no-loc text="StatusInfo"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f3cd.png" alt-text="Screenshot of StatusInfoLeft."::: | f3cd | :::no-loc text="StatusInfoLeft"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f3e2.png" alt-text="Screenshot of NearbySharing."::: | f3e2 | :::no-loc text="NearbySharing"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f3e7.png" alt-text="Screenshot of CtrlSpatialLeft."::: | f3e7 | :::no-loc text="CtrlSpatialLeft"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f404.png" alt-text="Screenshot of InteractiveDashboard."::: | f404 | :::no-loc text="InteractiveDashboard"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f405.png" alt-text="Screenshot of DeclineCall."::: | f405 | :::no-loc text="DeclineCall"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f406.png" alt-text="Screenshot of ClippingTool."::: | f406 | :::no-loc text="ClippingTool"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f407.png" alt-text="Screenshot of RectangularClipping."::: | f407 | :::no-loc text="RectangularClipping"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f408.png" alt-text="Screenshot of FreeFormClipping."::: | f408 | :::no-loc text="FreeFormClipping"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f413.png" alt-text="Screenshot of CopyTo."::: | f413 | :::no-loc text="CopyTo"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f427.png" alt-text="Screenshot of IDBadge."::: | f427 | :::no-loc text="IDBadge"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f432.png" alt-text="Screenshot of BatterySaver."::: | f432 | :::no-loc text="BatterySaver"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f439.png" alt-text="Screenshot of DynamicLock."::: | f439 | :::no-loc text="DynamicLock"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f45e.png" alt-text="Screenshot of PenTips."::: | f45e | :::no-loc text="PenTips"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f45f.png" alt-text="Screenshot of PenTipsMirrored."::: | f45f | :::no-loc text="PenTipsMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f460.png" alt-text="Screenshot of HWPJoin."::: | f460 | :::no-loc text="HWPJoin"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f461.png" alt-text="Screenshot of HWPInsert."::: | f461 | :::no-loc text="HWPInsert"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f462.png" alt-text="Screenshot of HWPStrikeThrough."::: | f462 | :::no-loc text="HWPStrikeThrough"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f463.png" alt-text="Screenshot of HWPScratchOut."::: | f463 | :::no-loc text="HWPScratchOut"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f464.png" alt-text="Screenshot of HWPSplit."::: | f464 | :::no-loc text="HWPSplit"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f465.png" alt-text="Screenshot of HWPNewLine."::: | f465 | :::no-loc text="HWPNewLine"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f466.png" alt-text="Screenshot of HWPOverwrite."::: | f466 | :::no-loc text="HWPOverwrite"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f473.png" alt-text="Screenshot of MobWifiWarning1."::: | f473 | :::no-loc text="MobWifiWarning1"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f474.png" alt-text="Screenshot of MobWifiWarning2."::: | f474 | :::no-loc text="MobWifiWarning2"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f475.png" alt-text="Screenshot of MobWifiWarning3."::: | f475 | :::no-loc text="MobWifiWarning3"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f476.png" alt-text="Screenshot of MobWifiWarning4."::: | f476 | :::no-loc text="MobWifiWarning4"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f47f.png" alt-text="Screenshot of MicLocationCombo."::: | f47f | :::no-loc text="MicLocationCombo"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f49a.png" alt-text="Screenshot of Globe2."::: | f49a | :::no-loc text="Globe2"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f4a5.png" alt-text="Screenshot of SpecialEffectSize."::: | f4a5 | :::no-loc text="SpecialEffectSize"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f4a9.png" alt-text="Screenshot of GIF."::: | f4a9 | :::no-loc text="GIF"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f4aa.png" alt-text="Screenshot of Sticker2."::: | f4aa | :::no-loc text="Sticker2"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f4bd.png" alt-text="Screenshot of Snooze."::: | f4bd | :::no-loc text="Snooze"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f4be.png" alt-text="Screenshot of SurfaceHubSelected."::: | f4be | :::no-loc text="SurfaceHubSelected"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f4bf.png" alt-text="Screenshot of HoloLensSelected."::: | f4bf | :::no-loc text="HoloLensSelected"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f4c0.png" alt-text="Screenshot of Earbud."::: | f4c0 | :::no-loc text="Earbud"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f4c3.png" alt-text="Screenshot of MixVolumes."::: | f4c3 | :::no-loc text="MixVolumes"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f540.png" alt-text="Screenshot of Safe."::: | f540 | :::no-loc text="Safe"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f552.png" alt-text="Screenshot of LaptopSecure."::: | f552 | :::no-loc text="LaptopSecure"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f56d.png" alt-text="Screenshot of PrintDefault."::: | f56d | :::no-loc text="PrintDefault"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f56e.png" alt-text="Screenshot of PageMirrored."::: | f56e | :::no-loc text="PageMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f56f.png" alt-text="Screenshot of LandscapeOrientationMirrored."::: | f56f | :::no-loc text="LandscapeOrientationMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f570.png" alt-text="Screenshot of ColorOff."::: | f570 | :::no-loc text="ColorOff"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f571.png" alt-text="Screenshot of PrintAllPages."::: | f571 | :::no-loc text="PrintAllPages"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f572.png" alt-text="Screenshot of PrintCustomRange."::: | f572 | :::no-loc text="PrintCustomRange"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f573.png" alt-text="Screenshot of PageMarginPortraitNarrow."::: | f573 | :::no-loc text="PageMarginPortraitNarrow"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f574.png" alt-text="Screenshot of PageMarginPortraitNormal."::: | f574 | :::no-loc text="PageMarginPortraitNormal"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f575.png" alt-text="Screenshot of PageMarginPortraitModerate."::: | f575 | :::no-loc text="PageMarginPortraitModerate"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f576.png" alt-text="Screenshot of PageMarginPortraitWide."::: | f576 | :::no-loc text="PageMarginPortraitWide"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f577.png" alt-text="Screenshot of PageMarginLandscapeNarrow."::: | f577 | :::no-loc text="PageMarginLandscapeNarrow"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f578.png" alt-text="Screenshot of PageMarginLandscapeNormal."::: | f578 | :::no-loc text="PageMarginLandscapeNormal"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f579.png" alt-text="Screenshot of PageMarginLandscapeModerate."::: | f579 | :::no-loc text="PageMarginLandscapeModerate"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f57a.png" alt-text="Screenshot of PageMarginLandscapeWide."::: | f57a | :::no-loc text="PageMarginLandscapeWide"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f57b.png" alt-text="Screenshot of CollateLandscape."::: | f57b | :::no-loc text="CollateLandscape"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f57c.png" alt-text="Screenshot of CollatePortrait."::: | f57c | :::no-loc text="CollatePortrait"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f57d.png" alt-text="Screenshot of CollatePortraitSeparated."::: | f57d | :::no-loc text="CollatePortraitSeparated"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f57e.png" alt-text="Screenshot of DuplexLandscapeOneSided."::: | f57e | :::no-loc text="DuplexLandscapeOneSided"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f57f.png" alt-text="Screenshot of DuplexLandscapeOneSidedMirrored."::: | f57f | :::no-loc text="DuplexLandscapeOneSidedMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f580.png" alt-text="Screenshot of DuplexLandscapeTwoSidedLongEdge."::: | f580 | :::no-loc text="DuplexLandscapeTwoSidedLongEdge"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f581.png" alt-text="Screenshot of DuplexLandscapeTwoSidedLongEdgeMirrored."::: | f581 | :::no-loc text="DuplexLandscapeTwoSidedLongEdgeMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f582.png" alt-text="Screenshot of DuplexLandscapeTwoSidedShortEdge."::: | f582 | :::no-loc text="DuplexLandscapeTwoSidedShortEdge"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f583.png" alt-text="Screenshot of DuplexLandscapeTwoSidedShortEdgeMirrored."::: | f583 | :::no-loc text="DuplexLandscapeTwoSidedShortEdgeMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f584.png" alt-text="Screenshot of DuplexPortraitOneSided."::: | f584 | :::no-loc text="DuplexPortraitOneSided"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f585.png" alt-text="Screenshot of DuplexPortraitOneSidedMirrored."::: | f585 | :::no-loc text="DuplexPortraitOneSidedMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f586.png" alt-text="Screenshot of DuplexPortraitTwoSidedLongEdge."::: | f586 | :::no-loc text="DuplexPortraitTwoSidedLongEdge"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f587.png" alt-text="Screenshot of DuplexPortraitTwoSidedLongEdgeMirrored."::: | f587 | :::no-loc text="DuplexPortraitTwoSidedLongEdgeMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f588.png" alt-text="Screenshot of DuplexPortraitTwoSidedShortEdge."::: | f588 | :::no-loc text="DuplexPortraitTwoSidedShortEdge"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f589.png" alt-text="Screenshot of DuplexPortraitTwoSidedShortEdgeMirrored."::: | f589 | :::no-loc text="DuplexPortraitTwoSidedShortEdgeMirrored"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f58a.png" alt-text="Screenshot of PPSOneLandscape."::: | f58a | :::no-loc text="PPSOneLandscape"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f58b.png" alt-text="Screenshot of PPSTwoLandscape."::: | f58b | :::no-loc text="PPSTwoLandscape"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f58c.png" alt-text="Screenshot of PPSTwoPortrait."::: | f58c | :::no-loc text="PPSTwoPortrait"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f58d.png" alt-text="Screenshot of PPSFourLandscape."::: | f58d | :::no-loc text="PPSFourLandscape"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f58e.png" alt-text="Screenshot of PPSFourPortrait."::: | f58e | :::no-loc text="PPSFourPortrait"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f58f.png" alt-text="Screenshot of HolePunchOff."::: | f58f | :::no-loc text="HolePunchOff"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f590.png" alt-text="Screenshot of HolePunchPortraitLeft."::: | f590 | :::no-loc text="HolePunchPortraitLeft"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f591.png" alt-text="Screenshot of HolePunchPortraitRight."::: | f591 | :::no-loc text="HolePunchPortraitRight"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f592.png" alt-text="Screenshot of HolePunchPortraitTop."::: | f592 | :::no-loc text="HolePunchPortraitTop"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f593.png" alt-text="Screenshot of HolePunchPortraitBottom."::: | f593 | :::no-loc text="HolePunchPortraitBottom"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f594.png" alt-text="Screenshot of HolePunchLandscapeLeft."::: | f594 | :::no-loc text="HolePunchLandscapeLeft"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f595.png" alt-text="Screenshot of HolePunchLandscapeRight."::: | f595 | :::no-loc text="HolePunchLandscapeRight"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f596.png" alt-text="Screenshot of HolePunchLandscapeTop."::: | f596 | :::no-loc text="HolePunchLandscapeTop"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f597.png" alt-text="Screenshot of HolePunchLandscapeBottom."::: | f597 | :::no-loc text="HolePunchLandscapeBottom"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f598.png" alt-text="Screenshot of StaplingOff."::: | f598 | :::no-loc text="StaplingOff"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f599.png" alt-text="Screenshot of StaplingPortraitTopLeft."::: | f599 | :::no-loc text="StaplingPortraitTopLeft"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f59a.png" alt-text="Screenshot of StaplingPortraitTopRight."::: | f59a | :::no-loc text="StaplingPortraitTopRight"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f59b.png" alt-text="Screenshot of StaplingPortraitBottomRight."::: | f59b | :::no-loc text="StaplingPortraitBottomRight"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f59c.png" alt-text="Screenshot of StaplingPortraitTwoLeft."::: | f59c | :::no-loc text="StaplingPortraitTwoLeft"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f59d.png" alt-text="Screenshot of StaplingPortraitTwoRight."::: | f59d | :::no-loc text="StaplingPortraitTwoRight"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f59e.png" alt-text="Screenshot of StaplingPortraitTwoTop."::: | f59e | :::no-loc text="StaplingPortraitTwoTop"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f59f.png" alt-text="Screenshot of StaplingPortraitTwoBottom."::: | f59f | :::no-loc text="StaplingPortraitTwoBottom"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f5a0.png" alt-text="Screenshot of StaplingPortraitBookBinding."::: | f5a0 | :::no-loc text="StaplingPortraitBookBinding"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f5a1.png" alt-text="Screenshot of StaplingLandscapeTopLeft."::: | f5a1 | :::no-loc text="StaplingLandscapeTopLeft"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f5a2.png" alt-text="Screenshot of StaplingLandscapeTopRight."::: | f5a2 | :::no-loc text="StaplingLandscapeTopRight"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f5a3.png" alt-text="Screenshot of StaplingLandscapeBottomLeft."::: | f5a3 | :::no-loc text="StaplingLandscapeBottomLeft"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f5a4.png" alt-text="Screenshot of StaplingLandscapeBottomRight."::: | f5a4 | :::no-loc text="StaplingLandscapeBottomRight"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f5a5.png" alt-text="Screenshot of StaplingLandscapeTwoLeft."::: | f5a5 | :::no-loc text="StaplingLandscapeTwoLeft"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f5a6.png" alt-text="Screenshot of StaplingLandscapeTwoRight."::: | f5a6 | :::no-loc text="StaplingLandscapeTwoRight"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f5a7.png" alt-text="Screenshot of StaplingLandscapeTwoTop."::: | f5a7 | :::no-loc text="StaplingLandscapeTwoTop"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f5a8.png" alt-text="Screenshot of StaplingLandscapeTwoBottom."::: | f5a8 | :::no-loc text="StaplingLandscapeTwoBottom"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f5a9.png" alt-text="Screenshot of StaplingLandscapeBookBinding."::: | f5a9 | :::no-loc text="StaplingLandscapeBookBinding"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f5aa.png" alt-text="Screenshot of StatusDataTransferRoaming."::: | f5aa | :::no-loc text="StatusDataTransferRoaming"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f5ab.png" alt-text="Screenshot of MobSIMError."::: | f5ab | :::no-loc text="MobSIMError"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f5ac.png" alt-text="Screenshot of CollateLandscapeSeparated."::: | f5ac | :::no-loc text="CollateLandscapeSeparated"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f5ad.png" alt-text="Screenshot of PPSOnePortrait."::: | f5ad | :::no-loc text="PPSOnePortrait"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f5ae.png" alt-text="Screenshot of StaplingPortraitBottomLeft."::: | f5ae | :::no-loc text="StaplingPortraitBottomLeft"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f5b0.png" alt-text="Screenshot of PlaySolid."::: | f5b0 | :::no-loc text="PlaySolid"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f5e7.png" alt-text="Screenshot of RepeatOff."::: | f5e7 | :::no-loc text="RepeatOff"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f5ed.png" alt-text="Screenshot of Set."::: | f5ed | :::no-loc text="Set"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f5ee.png" alt-text="Screenshot of SetSolid."::: | f5ee | :::no-loc text="SetSolid"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f5ef.png" alt-text="Screenshot of FuzzyReading."::: | f5ef | :::no-loc text="FuzzyReading"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f5f2.png" alt-text="Screenshot of VerticalBattery0."::: | f5f2 | :::no-loc text="VerticalBattery0"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f5f3.png" alt-text="Screenshot of VerticalBattery1."::: | f5f3 | :::no-loc text="VerticalBattery1"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f5f4.png" alt-text="Screenshot of VerticalBattery2."::: | f5f4 | :::no-loc text="VerticalBattery2"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f5f5.png" alt-text="Screenshot of VerticalBattery3."::: | f5f5 | :::no-loc text="VerticalBattery3"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f5f6.png" alt-text="Screenshot of VerticalBattery4."::: | f5f6 | :::no-loc text="VerticalBattery4"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f5f7.png" alt-text="Screenshot of VerticalBattery5."::: | f5f7 | :::no-loc text="VerticalBattery5"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f5f8.png" alt-text="Screenshot of VerticalBattery6."::: | f5f8 | :::no-loc text="VerticalBattery6"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f5f9.png" alt-text="Screenshot of VerticalBattery7."::: | f5f9 | :::no-loc text="VerticalBattery7"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f5fa.png" alt-text="Screenshot of VerticalBattery8."::: | f5fa | :::no-loc text="VerticalBattery8"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f5fb.png" alt-text="Screenshot of VerticalBattery9."::: | f5fb | :::no-loc text="VerticalBattery9"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f5fc.png" alt-text="Screenshot of VerticalBattery10."::: | f5fc | :::no-loc text="VerticalBattery10"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f5fd.png" alt-text="Screenshot of VerticalBatteryCharging0."::: | f5fd | :::no-loc text="VerticalBatteryCharging0"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f5fe.png" alt-text="Screenshot of VerticalBatteryCharging1."::: | f5fe | :::no-loc text="VerticalBatteryCharging1"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f5ff.png" alt-text="Screenshot of VerticalBatteryCharging2."::: | f5ff | :::no-loc text="VerticalBatteryCharging2"::: |

### PUA F600-F800

The following table of glyphs displays unicode points prefixed from F6- to F8-.

[Back to top](#icon-list)

| Glyph | Unicode point | Description |
|-------|---------------|-------------|
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f600.png" alt-text="VerticalBatteryCharging3"::: | f600 | :::no-loc text="VerticalBatteryCharging3"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f601.png" alt-text="VerticalBatteryCharging4"::: | f601 | :::no-loc text="VerticalBatteryCharging4"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f602.png" alt-text="VerticalBatteryCharging5"::: | f602 | :::no-loc text="VerticalBatteryCharging5"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f603.png" alt-text="VerticalBatteryCharging6"::: | f603 | :::no-loc text="VerticalBatteryCharging6"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f604.png" alt-text="VerticalBatteryCharging7"::: | f604 | :::no-loc text="VerticalBatteryCharging7"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f605.png" alt-text="VerticalBatteryCharging8"::: | f605 | :::no-loc text="VerticalBatteryCharging8"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f606.png" alt-text="VerticalBatteryCharging9"::: | f606 | :::no-loc text="VerticalBatteryCharging9"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f607.png" alt-text="VerticalBatteryCharging10"::: | f607 | :::no-loc text="VerticalBatteryCharging10"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f608.png" alt-text="VerticalBatteryUnknown"::: | f608 | :::no-loc text="VerticalBatteryUnknown"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f618.png" alt-text="SIMError"::: | f618 | :::no-loc text="SIMError"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f619.png" alt-text="SIMMissing"::: | f619 | :::no-loc text="SIMMissing"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f61a.png" alt-text="SIMLock"::: | f61a | :::no-loc text="SIMLock"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f61b.png" alt-text="eSIM"::: | f61b | :::no-loc text="eSIM"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f61c.png" alt-text="eSIMNoProfile"::: | f61c | :::no-loc text="eSIMNoProfile"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f61d.png" alt-text="eSIMLocked"::: | f61d | :::no-loc text="eSIMLocked"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f61e.png" alt-text="eSIMBusy"::: | f61e | :::no-loc text="eSIMBusy"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f61f.png" alt-text="NoiseCancelation"::: | f61f | :::no-loc text="NoiseCancelation"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f620.png" alt-text="NoiseCancelationOff"::: | f620 | :::no-loc text="NoiseCancelationOff"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f623.png" alt-text="MusicSharing"::: | f623 | :::no-loc text="MusicSharing"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f624.png" alt-text="MusicSharingOff"::: | f624 | :::no-loc text="MusicSharingOff"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f63c.png" alt-text="CircleShapeSolid"::: | f63c | :::no-loc text="CircleShapeSolid"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f657.png" alt-text="WifiCallBars"::: | f657 | :::no-loc text="WifiCallBars"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f658.png" alt-text="WifiCall0"::: | f658 | :::no-loc text="WifiCall0"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f659.png" alt-text="WifiCall1"::: | f659 | :::no-loc text="WifiCall1"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f65a.png" alt-text="WifiCall2"::: | f65a | :::no-loc text="WifiCall2"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f65b.png" alt-text="WifiCall3"::: | f65b | :::no-loc text="WifiCall3"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f65c.png" alt-text="WifiCall4"::: | f65c | :::no-loc text="WifiCall4"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f67b.png" alt-text="Pen"::: | f67b | :::no-loc text="Pen"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f683.png" alt-text="TextSelect"::: | f683 | :::no-loc text="TextSelect"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f684.png" alt-text="TextNavigate"::: | f684 | :::no-loc text="TextNavigate"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f698.png" alt-text="PinyinIMELogo2"::: | f698 | :::no-loc text="PinyinIMELogo2"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f69b.png" alt-text="UserRemove"::: | f69b | :::no-loc text="UserRemove"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f69e.png" alt-text="CHTLanguageBar"::: | f69e | :::no-loc text="CHTLanguageBar"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f6a9.png" alt-text="ComposeMode"::: | f6a9 | :::no-loc text="ComposeMode"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f6b8.png" alt-text="ExpressiveInputEntry"::: | f6b8 | :::no-loc text="ExpressiveInputEntry"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f6ba.png" alt-text="EmojiTabMoreSymbols"::: | f6ba | :::no-loc text="EmojiTabMoreSymbols"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f6c4.png" alt-text="PhoneScreen"::: | f6c4 | :::no-loc text="PhoneScreen"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f6c5.png" alt-text="AlertUrgent"::: | f6c5 | :::no-loc text="AlertUrgent"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f6c6.png" alt-text="PhoneDesktop"::: | f6c6 | :::no-loc text="PhoneDesktop"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f6fa.png" alt-text="WebSearch"::: | f6fa | :::no-loc text="WebSearch"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f712.png" alt-text="Kiosk"::: | f712 | :::no-loc text="Kiosk"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f714.png" alt-text="RTTLogo"::: | f714 | :::no-loc text="RTTLogo"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f715.png" alt-text="VoiceCall"::: | f715 | :::no-loc text="VoiceCall"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f716.png" alt-text="GoToMessage"::: | f716 | :::no-loc text="GoToMessage"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f71a.png" alt-text="ReturnToCall"::: | f71a | :::no-loc text="ReturnToCall"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f71c.png" alt-text="StartPresenting"::: | f71c | :::no-loc text="StartPresenting"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f71d.png" alt-text="StopPresenting"::: | f71d | :::no-loc text="StopPresenting"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f71e.png" alt-text="ProductivityMode"::: | f71e | :::no-loc text="ProductivityMode"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f738.png" alt-text="SetHistoryStatus"::: | f738 | :::no-loc text="SetHistoryStatus"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f739.png" alt-text="SetHistoryStatus2"::: | f739 | :::no-loc text="SetHistoryStatus2"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f73d.png" alt-text="Keyboardsettings20"::: | f73d | :::no-loc text="Keyboardsettings20"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f73e.png" alt-text="OneHandedRight20"::: | f73e | :::no-loc text="OneHandedRight20"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f73f.png" alt-text="OneHandedLeft20"::: | f73f | :::no-loc text="OneHandedLeft20"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f740.png" alt-text="Split20"::: | f740 | :::no-loc text="Split20"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f741.png" alt-text="Full20"::: | f741 | :::no-loc text="Full20"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f742.png" alt-text="Handwriting20"::: | f742 | :::no-loc text="Handwriting20"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f743.png" alt-text="ChevronLeft20"::: | f743 | :::no-loc text="ChevronLeft20"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f744.png" alt-text="ChevronLeft32"::: | f744 | :::no-loc text="ChevronLeft32"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f745.png" alt-text="ChevronRight20"::: | f745 | :::no-loc text="ChevronRight20"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f746.png" alt-text="ChevronRight32"::: | f746 | :::no-loc text="ChevronRight32"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f763.png" alt-text="Event12"::: | f763 | :::no-loc text="Event12"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f781.png" alt-text="MicOff2"::: | f781 | :::no-loc text="MicOff2"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f785.png" alt-text="DeliveryOptimization"::: | f785 | :::no-loc text="DeliveryOptimization"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f78a.png" alt-text="CancelMedium"::: | f78a | :::no-loc text="CancelMedium"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f78b.png" alt-text="SearchMedium"::: | f78b | :::no-loc text="SearchMedium"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f78c.png" alt-text="AcceptMedium"::: | f78c | :::no-loc text="AcceptMedium"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f78d.png" alt-text="RevealPasswordMedium"::: | f78d | :::no-loc text="RevealPasswordMedium"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f7ad.png" alt-text="DeleteWord"::: | f7ad | :::no-loc text="DeleteWord"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f7ae.png" alt-text="DeleteWordFill"::: | f7ae | :::no-loc text="DeleteWordFill"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f7af.png" alt-text="DeleteLines"::: | f7af | :::no-loc text="DeleteLines"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f7b0.png" alt-text="DeleteLinesFill"::: | f7b0 | :::no-loc text="DeleteLinesFill"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f7b1.png" alt-text="InstertWords"::: | f7b1 | :::no-loc text="InstertWords"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f7b2.png" alt-text="InstertWordsFill"::: | f7b2 | :::no-loc text="InstertWordsFill"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f7b3.png" alt-text="JoinWords"::: | f7b3 | :::no-loc text="JoinWords"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f7b4.png" alt-text="JoinWordsFill"::: | f7b4 | :::no-loc text="JoinWordsFill"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f7b5.png" alt-text="OverwriteWords"::: | f7b5 | :::no-loc text="OverwriteWords"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f7b6.png" alt-text="OverwriteWordsFill"::: | f7b6 | :::no-loc text="OverwriteWordsFill"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f7b7.png" alt-text="AddNewLine"::: | f7b7 | :::no-loc text="AddNewLine"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f7b8.png" alt-text="AddNewLineFill"::: | f7b8 | :::no-loc text="AddNewLineFill"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f7b9.png" alt-text="OverwriteWordsKorean"::: | f7b9 | :::no-loc text="OverwriteWordsKorean"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f7ba.png" alt-text="OverwriteWordsFillKorean"::: | f7ba | :::no-loc text="OverwriteWordsFillKorean"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f7bb.png" alt-text="EducationIcon"::: | f7bb | :::no-loc text="EducationIcon"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f7ed.png" alt-text="WindowSnipping"::: | f7ed | :::no-loc text="WindowSnipping"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f7ee.png" alt-text="VideoCapture"::: | f7ee | :::no-loc text="VideoCapture"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f809.png" alt-text="StatusSecured"::: | f809 | :::no-loc text="StatusSecured"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f83b.png" alt-text="NarratorApp"::: | f83b | :::no-loc text="NarratorApp"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f83d.png" alt-text="PowerButtonUpdate"::: | f83d | :::no-loc text="PowerButtonUpdate"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f83e.png" alt-text="RestartUpdate"::: | f83e | :::no-loc text="RestartUpdate"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f83f.png" alt-text="UpdateStatusDot"::: | f83f | :::no-loc text="UpdateStatusDot"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f847.png" alt-text="Eject"::: | f847 | :::no-loc text="Eject"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f87b.png" alt-text="Spelling"::: | f87b | :::no-loc text="Spelling"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f87c.png" alt-text="SpellingKorean"::: | f87c | :::no-loc text="SpellingKorean"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f87d.png" alt-text="SpellingSerbian"::: | f87d | :::no-loc text="SpellingSerbian"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f87e.png" alt-text="SpellingChinese"::: | f87e | :::no-loc text="SpellingChinese"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f89a.png" alt-text="FolderSelect"::: | f89a | :::no-loc text="FolderSelect"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f8a5.png" alt-text="SmartScreen"::: | f8a5 | :::no-loc text="SmartScreen"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f8a6.png" alt-text="ExploitProtection"::: | f8a6 | :::no-loc text="ExploitProtection"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f8aa.png" alt-text="AddBold"::: | f8aa | :::no-loc text="AddBold"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f8ab.png" alt-text="SubtractBold"::: | f8ab | :::no-loc text="SubtractBold"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f8ac.png" alt-text="BackSolidBold"::: | f8ac | :::no-loc text="BackSolidBold"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f8ad.png" alt-text="ForwardSolidBold"::: | f8ad | :::no-loc text="ForwardSolidBold"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f8ae.png" alt-text="PauseBold"::: | f8ae | :::no-loc text="PauseBold"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f8af.png" alt-text="ClickSolid"::: | f8af | :::no-loc text="ClickSolid"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f8b0.png" alt-text="SettingsSolid"::: | f8b0 | :::no-loc text="SettingsSolid"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f8b1.png" alt-text="MicrophoneSolidBold"::: | f8b1 | :::no-loc text="MicrophoneSolidBold"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f8b2.png" alt-text="SpeechSolidBold"::: | f8b2 | :::no-loc text="SpeechSolidBold"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f8b3.png" alt-text="ClickedOutLoudSolidBold"::: | f8b3 | :::no-loc text="ClickedOutLoudSolidBold"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f8c0.png" alt-text="VPNOverlay"::: | f8c0 | :::no-loc text="VPNOverlay"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f8c1.png" alt-text="VPNRoamingOverly"::: | f8c1 | :::no-loc text="VPNRoamingOverly"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f8c2.png" alt-text="WifiVPN3"::: | f8c2 | :::no-loc text="WifiVPN3"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f8c3.png" alt-text="WifiVPN4"::: | f8c3 | :::no-loc text="WifiVPN4"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f8c4.png" alt-text="WifiVPN5"::: | f8c4 | :::no-loc text="WifiVPN5"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f8c5.png" alt-text="SignalBarsVPN2"::: | f8c5 | :::no-loc text="SignalBarsVPN2"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f8c6.png" alt-text="SignalBarsVPN3"::: | f8c6 | :::no-loc text="SignalBarsVPN3"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f8c7.png" alt-text="SignalBarsVPN4"::: | f8c7 | :::no-loc text="SignalBarsVPN4"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f8c8.png" alt-text="SignalBarsVPN5"::: | f8c8 | :::no-loc text="SignalBarsVPN5"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f8c9.png" alt-text="SignalBarsVPNRoaming3"::: | f8c9 | :::no-loc text="SignalBarsVPNRoaming3"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f8ca.png" alt-text="SignalBarsVPNRoaming4"::: | f8ca | :::no-loc text="SignalBarsVPNRoaming4"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f8cb.png" alt-text="SignalBarsVPNRoaming5"::: | f8cb | :::no-loc text="SignalBarsVPNRoaming5"::: |
| :::image type="content" border="false" source="images/glyphs/segoe-fluent-icons/f8cc.png" alt-text="EthernetVPN"::: | f8cc | :::no-loc text="EthernetVPN"::: |

