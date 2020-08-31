---

Description: Some kinds of apps (multilingual dictionaries, translation tools, etc.) need to override the default behavior of an app bundle, and build resources into the app package instead of having them in separate resource packages. This topic explains how to do that.
title: Build resources into your app package
template: detail.hbs
ms.date: 11/14/2017
ms.topic: article
keywords: windows 10, uwp, resource, image, asset, MRT, qualifier
ms.localizationpriority: medium
---

# Build resources into your app package, instead of into a resource pack

Some kinds of apps (multilingual dictionaries, translation tools, etc.) need to override the default behavior of an app bundle, and build resources into the app package instead of having them in separate resource packages (or resource packs). This topic explains how to do that.

By default when you build an [app bundle (.appxbundle)](/windows/msix/package/packaging-uwp-apps), only your default resources for language, scale, and DirectX feature level are built into the app package. Your translated resources&mdash;and your resources tailored for non-default scales and/or DirectX feature levels&mdash;are built into resource packages and they are only downloaded onto devices that need them. If a customer is buying your app from the Microsoft Store using a device with a language preference set to Spanish, then only your app plus the Spanish resource package are downloaded and installed. If that same user later changes their language preference to French in **Settings**, then your app's French resource package is downloaded and installed. Similar things happen with your resources qualified for scale and for DirectX feature level. For the majority of apps, this behavior constitutes a valuable efficiency, and it's exactly what you and the customer *want* to happen.

But if your app allows the user to change the language on the fly from within the app (instead of via **Settings**), then that default behavior is not appropriate. You actually want all of your language resources to be unconditionally downloaded and installed along with the app one time, and then remain on the device. You want to build all of those resources into your app package instead of into separate resource packages.

**Note** Including resources in an app package essentially increases the size of the app. That's why it's only worth doing if the nature of the app demands it. If not, then you don't need to do anything except build a regular app bundle as usual.

You can configure Visual Studio to build resources into your app package in one of two ways. You can either add a configuration file to your project, or you can edit your project file directly. Use whichever of these options you're most comfortable with, or whichever works best with your build system.

## Option 1. Use priconfig.packaging.xml to build resources into your app package

1. In Visual Studio, add a new item to your project. Choose XML File, and name the file `priconfig.packaging.xml`.
2. In Solution Explorer, select `priconfig.packaging.xml` and check the Properties window. The file's Build Action should be set to None, and Copy to Output Directory should be set to Do not copy.
3. Replace the contents of the file with this XML.
   ```xml
   <packaging>
      <autoResourcePackage qualifier="Language" />
      <autoResourcePackage qualifier="Scale" />
      <autoResourcePackage qualifier="DXFeatureLevel" />
   </packaging>
   ```
4. Each `<autoResourcePackage>` element tells Visual Studio to automatically split the resources for the given qualifier name out into separate resource packages. This is called *auto-splitting*. With the file contents you have so far, you haven't actually changed Visual Studio's behavior. In other words, Visual Studio *already behaved as if* this file were present with these contents, because these are the defaults. If you don't want Visual Studio to auto-split on a qualifier name then delete that `<autoResourcePackage>` element from the file. Here's how the file would look if you wanted all of your language resources to be built into the app package instead of being auto-split out into separate resource packages.
   ```xml
   <packaging>
      <autoResourcePackage qualifier="Scale" />
      <autoResourcePackage qualifier="DXFeatureLevel" />
   </packaging>
   ```
5. Save and close the file and rebuild your project.

To confirm that your auto-split choices are being taken into account, look for the file `<ProjectFolder>\obj\<ReleaseConfiguration folder>\split.priconfig.xml` and confirm that its contents match your choices. If they do, then you have successfully configured Visual Studio to build the resources of your choice into the app package.

There is one final step that you need to do. **But only if you deleted the `Language` qualifier name**. You need to specify the union of all of your app's supported language as your app's default for language. For details, see [Specify the default resources that your app uses](specify-default-resources-installed.md). This is what your `priconfig.default.xml` would contain if you were including resources for English, Spanish, and French in your app package.

```xml
   <default>
      <qualifier name="Language" value="en;es;fr" />
      ...
   </default>
```

### How does this work?

Behind the scenes, Visual Studio launches a tool named `MakePri.exe` to generate a file known as a Package Resource Index, which describes all of your app's resources, including indicating which resource qualifier names to auto-split on. For details about this tool, see [Compile resources manually with MakePri.exe](compile-resources-manually-with-makepri.md). Visual Studio passes a configuration file to `MakePri.exe`. The contents of your `priconfig.packaging.xml` file are used as the `<packaging>` element of that configuration file, which is the part that determines auto-splitting. So, adding and editing `priconfig.packaging.xml` ultimately influences the contents of the Package Resource Index file that Visual Studio generates for your app, as well as the contents of the packages in your app bundle.

### Using a different file name than `priconfig.packaging.xml`

If you name your file `priconfig.packaging.xml`, then Visual Studio will recognize it and use it automatically. If you give it a different name, then you'll need to let Visual Studio know. In your project file, between the opening and closing tags of the first `<PropertyGroup>` element, add this XML.

```xml
<AppxPriConfigXmlPackagingSnippetPath>FILE-PATH-AND-NAME</AppxPriConfigXmlPackagingSnippetPath>
```

Replace `FILE-PATH-AND-NAME` with the path to, and name of, your file.

## Option 2. Use your project file to build resources into your app package

This is an alternative to Option 1. Once you understand how Option 1 works, you can choose to do Option 2 instead, if that suits your development and/or build workflow better.

In your project file, between the opening and closing tags of the first `<PropertyGroup>` element, add this XML.

```xml
<AppxBundleAutoResourcePackageQualifiers>Language|Scale|DXFeatureLevel</AppxBundleAutoResourcePackageQualifiers>
```

Here's how that looks after you've delete the first qualifier name.

```xml
<AppxBundleAutoResourcePackageQualifiers>Scale|DXFeatureLevel</AppxBundleAutoResourcePackageQualifiers>
```

Save and close, and rebuild your project.

There is one final step that you need to do. **But only if you deleted the `Language` qualifier name**. You need to specify the union of all of your app's supported language as your app's default for language. For details, see [Specify the default resources that your app uses](specify-default-resources-installed.md). This is what your project file would contain if you were including resources for English, Spanish, and French in your app package.

```xml
<AppxDefaultResourceQualifiers>Language=en;es;fr</AppxDefaultResourceQualifiers>
```

## Related topics

* [Package a UWP app with Visual Studio](/windows/msix/package/packaging-uwp-apps)
* [Compile resources manually with MakePri.exe](compile-resources-manually-with-makepri.md)
* [Specify the default resources that your app uses](specify-default-resources-installed.md)