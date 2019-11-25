---
Description: If your app doesn't have resources that match the particular settings of a customer device, then the app's default resources are used. This topic explains how to specify what those default resources are.
title: Specify the default resources that your app uses
template: detail.hbs
ms.date: 11/14/2017
ms.topic: article
keywords: windows 10, uwp, resource, image, asset, MRT, qualifier
ms.localizationpriority: medium
---
# Specify the default resources that your app uses

If your app doesn't have resources that match the particular settings of a customer device, then the app's default resources are used. This topic explains how to specify what those default resources are.

When a customer installs your app from the Microsoft Store, settings on the customer's device are matched against the app's available resources. This matching is done so that only the appropriate resources need to be downloaded and installed for that user. For example, the most appropriate strings and images for the user's language preferences, and the device's resolution and DPI settings, are used. For example, `200` is the default value for `scale`, but you can override that default if you wish.

Even for resources that don't go into their own resource packs (such as images tailored for high contrast settings), you can specify what default resources the app should use at run time if a resource that matches the user's settings can't be found. For example, `standard` is the default value for `contrast`, but you can override that default if you wish.

These defaults are specified in the form of default resource qualifier values. For an explanation of what resource qualifiers are, their use, and purpose, see [Tailor your resources for language, scale, high contrast, and other qualifiers](tailor-resources-lang-scale-contrast.md).

You can configure what these defaults are in one of two ways. You can either add a configuration file to your project, or you can edit your project file directly. Use whichever of these options you're most comfortable with, or whichever works best with your build system.

## Option 1. Use priconfig.default.xml to specify default qualifier values

1. In Visual Studio, add a new item to your project. Choose XML File, and name the file `priconfig.default.xml`.
2. In Solution Explorer, select `priconfig.default.xml` and check the Properties window. The file's Build Action should be set to None, and Copy to Output Directory should be set to Do not copy.
3. Replace the contents of the file with this XML.
   ```xml
   <default>
      <qualifier name="Language" value="LANGUAGE-TAG(S)" />
      <qualifier name="Contrast" value="standard" />
      <qualifier name="Scale" value="200" />
      <qualifier name="HomeRegion" value="001" />
      <qualifier name="TargetSize" value="256" />
      <qualifier name="LayoutDirection" value="LTR" />
      <qualifier name="DXFeatureLevel" value="DX9" />
      <qualifier name="Configuration" value="" />
      <qualifier name="AlternateForm" value="" />
   </default>
   ```
   
   **Note** The value `LANGUAGE-TAG(S)` needs to be synchronized with your app's default language. If that's a single [BCP-47 language tag](https://tools.ietf.org/html/bcp47), then your app's default language needs to be the same tag. If it's a comma-separated list of language tags, then your app's default language needs to be the first tag in the list. You set your app's default language in the **Default language** field on the **Application** tab in your app package manifest source file (`Package.appxmanifest`).

4. Each `<qualifier>` element tells Visual Studio what value to use as the default for each qualifier name. With the file contents you have so far, you haven't actually changed Visual Studio's behavior. In other words, Visual Studio *already behaved as if* this file were present with these contents, because these are the default defaults. So to override a default with your own default value, you'll have to change a value in the file. Here's an example of how the file might look if you'd edited the first three values.
   ```xml
   <default>
      <qualifier name="Language" value="de-DE" />
      <qualifier name="Contrast" value="black" />
      <qualifier name="Scale" value="400" />
      <qualifier name="HomeRegion" value="001" />
      <qualifier name="TargetSize" value="256" />
      <qualifier name="LayoutDirection" value="LTR" />
      <qualifier name="DXFeatureLevel" value="DX9" />
      <qualifier name="Configuration" value="" />
      <qualifier name="AlternateForm" value="" />
   </default>
   ```
5. Save and close the file and rebuild your project.

To confirm that your overridden defaults are being taken into account, look for the file `<ProjectFolder>\obj\<ReleaseConfiguration folder>\priconfig.xml` and confirm that its contents match your overrides. If they do, then you have successfully configured the qualifier values of the resources that your app will use by default. If a match for the user's settings is not found, then resources will be used whose folder or file names contain the default qualifer values that you've set here.

### How does this work?

Behind the scenes, Visual Studio launches a tool named `MakePri.exe` to generate a file known as a Package Resource Index (PRI), which describes all of your app's resources, including indicating which are the default resources. For details about this tool, see [Compile resources manually with MakePri.exe](compile-resources-manually-with-makepri.md). Visual Studio passes a configuration file to `MakePri.exe`. The contents of your `priconfig.default.xml` file are used as the `<default>` element of that configuration file, which is the part that specifies the set of qualifier values that are considered to be default. So, adding and editing `priconfig.default.xml` ultimately influences the contents of the Package Resource Index file that Visual Studio generates for your app and includes in its app package.

**Note** Any time you change the value of the `<qualifier name="Language" ... />` element, you need to synchronize that change with your app's default language. This is so that the language resources indexed in your app's PRI file match your app's manifest default language. The value in the `<qualifier name="Language" ... />` element overrides the value in the manifest with respect to the contents of `<ProjectFolder>\obj\<ReleaseConfiguration folder>\priconfig.xml`, but that file and your app's manifest should match.

### Using a different file name than `priconfig.default.xml`

If you name your file `priconfig.default.xml`, then Visual Studio will recognize it and use it automatically. If you give it a different name, then you'll need to let Visual Studio know. In your project file, between the opening and closing tags of the first `<PropertyGroup>` element, add this XML.

```xml
<AppxPriConfigXmlDefaultSnippetPath>FILE-PATH-AND-NAME</AppxPriConfigXmlDefaultSnippetPath>
```

Replace `FILE-PATH-AND-NAME` with the path to, and name of, your file.

## Option 2. Use your project file to specify default qualifier values

This is an alternative to Option 1. Once you understand how Option 1 works, you can choose to do Option 2 instead, if that suits your development and/or build workflow better.

In your project file, between the opening and closing tags of the first `<PropertyGroup>` element, add this XML.

```xml
<AppxDefaultResourceQualifiers>Language=LANGUAGE-TAG(S)|Contrast=standard|Scale=200|HomeRegion=001|TargetSize=256|LayoutDirection=LTR|DXFeatureLevel=DX9|Configuration=|AlternateForm=</AppxDefaultResourceQualifiers>
```

Here's an example of how that might look after you've edited the first three values.

```xml
<AppxDefaultResourceQualifiers>Language=de-DE|Contrast=black|Scale=400|HomeRegion=001|TargetSize=256|LayoutDirection=LTR|DXFeatureLevel=DX9|Configuration=|AlternateForm=</AppxDefaultResourceQualifiers>
```

Save and close, and rebuild your project.

**Note** Any time you change the `Language=` value, you need to synchronize that change with your app's default language in the manifest designer (by opening `Package.appxmanifest`).

## Related topics

* [Tailor your resources for language, scale, high contrast, and other qualifiers](tailor-resources-lang-scale-contrast.md)
* [BCP-47 language tag](https://tools.ietf.org/html/bcp47)
* [Compile resources manually with MakePri.exe](compile-resources-manually-with-makepri.md)
