---
title: Troubleshoot Microsoft Store app color and appearance issues
description: Understand why your app may appear differently in the Microsoft Store app versus the web version, and how to resolve common visual appearance issues.
ms.assetid: F8E5F6C8-6D1A-4F5A-8C4F-5C5B2F2F2F2F
ms.date: 12/19/2024
ms.topic: troubleshooting
keywords: windows 10, windows 11, uwp, microsoft store, background color, visual elements, app appearance, gradient
ms.localizationpriority: medium
---

# Troubleshoot Microsoft Store app color and appearance issues

If your app appears differently in the Microsoft Store app compared to the web version of the Microsoft Store, this article explains the common causes and provides solutions for visual appearance issues.

## Common issue: App background shows as gray instead of custom color

### Problem description
Your app's background color appears correctly in the web version of the Microsoft Store (showing your custom color from the manifest), but displays as a light gray color (#464646) in the Microsoft Store app on Windows.

**Example scenario**: Your app manifest specifies `BackgroundColor="#0ab68e"` (a teal/green color), which displays correctly on the Microsoft Store website, but appears as gray in the Microsoft Store app.

### Why this happens
The Microsoft Store app and web version may render app visuals differently based on several factors:

1. **Default color fallback**: When the Microsoft Store app cannot display your custom background color, it falls back to the default light gray color (#464646).

2. **Context differences**: The Store app and web version use different rendering contexts and may prioritize different visual elements from your app package.

3. **Platform variations**: The Microsoft Store app on different Windows versions may handle color rendering differently than the web experience.

## Troubleshooting steps

### Step 1: Verify your app manifest configuration

Ensure your `Package.appxmanifest` file correctly specifies the background color in the `VisualElements` section:

```xml
<uap:VisualElements
  DisplayName="Your App Name"
  Description="Your App Description"
  BackgroundColor="#0ab68e"
  Square150x150Logo="Assets\Square150x150Logo.png"
  Square44x44Logo="Assets\Square44x44Logo.png">
  <uap:DefaultTile 
    Wide310x150Logo="Assets\Wide310x150Logo.png"  
    Square71x71Logo="Assets\SmallTile.png" 
    Square310x310Logo="Assets\LargeTile.png" 
    ShortName="Your App">
    <uap:ShowNameOnTiles>
      <uap:ShowOn Tile="square150x150Logo"/>
      <uap:ShowOn Tile="square310x310Logo"/>
      <uap:ShowOn Tile="wide310x150Logo"/>
    </uap:ShowNameOnTiles>
  </uap:DefaultTile>
  <uap:SplashScreen Image="Assets\SplashScreen.png" BackgroundColor="#0ab68e"/>
</uap:VisualElements>
```

**Key points:**
- The `BackgroundColor` attribute should use a valid hex color value (e.g., "#0ab68e")
- Ensure the same color is used consistently in both `VisualElements` and `SplashScreen` elements
- The color value should include the "#" symbol

### Step 2: Check color format and validity

**Valid color formats:**
- Hex values: `#0ab68e`, `#FF0000`, `#464646`
- Named colors: `red`, `blue`, `green`, `transparent`

**Invalid formats that may cause fallback to gray:**
- RGB values: `rgb(10, 182, 142)` - not supported
- HSL values: `hsl(165, 89%, 38%)` - not supported
- Color values without "#": `0ab68e` - should be `#0ab68e`

### Step 3: Verify asset consistency

Ensure all your visual assets are consistent and properly formatted:

1. **Logo assets**: Verify that all logo files referenced in the manifest exist and are valid image files
2. **Image formats**: Use PNG format for logos and tiles
3. **Transparency**: If using transparent backgrounds in your assets, ensure they work well with your specified background color

### Step 4: Test across platforms

The appearance may vary across different contexts:

- **Microsoft Store app** (Windows 10/11)
- **Microsoft Store web version** (browsers)
- **Xbox Store**
- **Mobile Store** (if applicable)

### Step 5: Update and republish your app

After making changes to your manifest:

1. Increment your app version number
2. Create a new app package
3. Submit the update through Partner Center
4. Wait for certification and publication
5. Test the appearance in the Microsoft Store app after publication

## Additional considerations

### Theme compatibility
Your chosen background color should work well with both light and dark themes. Consider how your color appears with:
- Light theme system UI
- Dark theme system UI
- High contrast modes

### Accessibility
Ensure your background color choice meets accessibility guidelines:
- Sufficient contrast with text overlays
- Compatibility with high contrast themes
- Visibility for users with color vision deficiencies

### Store listing images
While the background color affects tile appearance, consider also optimizing your [Store listing images](../../hub/apps/publish/publish-your-app/msix/screenshots-and-images.md) to ensure consistent brand representation.

## Known limitations

### Platform-specific behavior
- Some Windows versions may have different color rendering behavior
- The Microsoft Store app may prioritize performance over custom color rendering in certain scenarios
- Network connectivity issues might affect how custom visuals are loaded

### Workarounds for persistent issues

If the gray background persists after following the troubleshooting steps:

1. **Use named colors**: Try using named color values instead of hex values (e.g., `"green"` instead of `"#008000"`)

2. **Simplify color values**: Use simpler hex values like `#00FF00` instead of complex values

3. **Test with transparency**: Set the background color to `"transparent"` to see if this resolves the issue

4. **Contact support**: If the issue persists across multiple app updates, consider reaching out to Microsoft Store support

## Related topics

- [Add a splash screen](add-a-splash-screen.md)
- [Create a customized splash screen](create-a-customized-splash-screen.md)
- [App screenshots, images, and trailers](../../hub/apps/publish/publish-your-app/msix/screenshots-and-images.md)
- [VisualElements element reference](/uwp/schemas/appxpackage/appxmanifestschema/element-visualelements)
- [SplashScreen element reference](/uwp/schemas/appxpackage/appxmanifestschema/element-splashscreen)