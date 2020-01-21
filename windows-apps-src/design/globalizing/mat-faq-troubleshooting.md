---
Description: This topic provides answers to frequently-asked questions and issues related to the Multilingual App Toolkit (MAT) 4.0.
title: Multilingual App Toolkit FAQ & troubleshooting
template: detail.hbs
ms.date: 11/13/2017
ms.topic: article
keywords: windows 10, uwp, globalization, localizability, localization
ms.localizationpriority: medium
---
# Multilingual App Toolkit 4.0 FAQ & troubleshooting

This topic provides answers to frequently-asked questions and issues related to the Multilingual App Toolkit (MAT) 4.0.

Also see [Use the Multilingual App Toolkit 4.0](use-mat.md).

**Note** The toolkit supports both .resw (XAML) and .resjson (JavaScript) files. But in this topic we'll refer only to .resw files. A .resw file is known as a Resources File. It contains strings, either in the default language or translated into another language. The folder that contains a .resw file is typically named for the value of a language tag.

## Do I need .resw files in multiple languages?

No. One of the key benefits of the Toolkit is that .resw files in multiple languages are not required. The Toolkit manages and synchronizes your app's resources by using .xlf files. This removes the challenges associated with keeping content synchronized across multiple .resw files.

Projects that contain matching .resw and .xlf files cause the translations from the .xlf file to be ignored. When this happens, a warning is displayed during the build to let you know that the .xlf translations are not included in the final app. A .resw file and .xlf file match when they have a target language with the same language code. An example of a matching pair would be `Strings\de-DE\Resources.resw` and an `<project-name>.de-DE.xlf` file (containing `target-language="de-DE"`).

## Can I have .resw files in multiple languages?

You can, but we don't recommend it. If you want to include .resw files in multiple languages in your project and use the Toolkit, make sure you don't have matching .resw and .xlf files.

## I don't see an option in the Tools menu to enable the Multilingual App Toolkit

Try these steps.

- Make sure you select the project node, not the solution node, before opening the **Tools** menu.
- Confirm that the toolkit extension is installed by using the Visual Studio Extension Manager.
- Confirm that your project is a UWP project.

## When I build my project, I don't see a message saying that a Multilingual App Toolkit build has started

Confirm that you've enabled the MAT for your project. On the **Tools** menu, select **Multilingual App Toolkit** > **Enable selection**. If your project was enabled with a previous version, then disable and re-enable the MAT by using the **Tools** menu. This updates the project to work with the new version of the Toolkit.

Ensure that the "Build Task for all Visual Studio editions" component was installed. This build component is installed with the extension, but it can be manually deselected during the installation. This component is required to update the .xlf files and add the translation into the PRI file. When this component is installed and working correctly, you will see these build messages.

```dosbatch
1> Multilingual App Toolkit build started.
1> Multilingual App Toolkit build completed successfully.
```

## The toolkit is reporting that it didn't locate any XLIFF language files during the build

```dosbatch
No XLIFF language files were found. The app will not contain any localized resources.
```

This message is displayed when the toolkit doesn't find any files in the project with an extension of .xlf. The toolkit generates and keeps these files in the `MultilingualResources` folder by default. They can be moved; but it's best to leave them in that folder because that allows the Multilingual Editor to locate the related metadata files.

## My .xlf file is not included in the list of files processed by the toolkit during build

If you manually exclude an .xlf file from the project, and then re-include it, the file type element may not be set correctly. In Visual Studio, select the file and check the Properties window. The file's Build Action should be set to XliffResource and Copy to Output Directory should be set to Do not copy. This is how the reference should look in your project file.

```xml
<XliffResource Include="MultilingualResources\<project-name>.fr-FR.xlf" />
```

## I've added .xlf-based languages. Where are my strings?

Your default language Resources File (.resw) is your canonical "schema" of the strings that your app uses. Translated Resources Files can contain all, or a subset of, these strings.

When you build your project, your Resources Files and your .xlf files are synchronized.

- The .xlf files are updated to reflect any added or removed string, or added or removed Resources Files.
- The Resources Files are updated to reflected any translated strings in the .xlf files.

This is explained in detail in [Use the Multilingual App Toolkit 4.0](use-mat.md).

## When I build my project, the .xlf files remain empty

Before you can use the MAT effectively, your app needs to be localizable. This is explained in detail in [Use the Multilingual App Toolkit 4.0](use-mat.md).

## What is Microsoft Translator?

Microsoft Translator is a cloud-based service that provides machine-based translation. Machine translation is ideal for gaining access to translation when human translation is not reasonable to obtain. You can learn more at [Microsoft Translator](https://www.microsofttranslator.com/).

The Toolkit uses the Microsoft Translator service to provide translation suggestions to you. You can see which languages are supported by Microsoft Translator when the Microsoft Translator icon is present in the Translation Languages dialog.

You can quickly translate your app using Microsoft Translator from within the Multilingual Editor by selecting a string and clicking **Translate**.

## What is pseudo language, and what are pseudo resource trackers?

Pseudo language is an artificial modification of the software product intended to simulate real language localization. You can find more details about pseudo language and pseudo resource trackers in [Use the Multilingual App Toolkit 4.0](use-mat.md).

## How do I set my language preference to pseudo language, so that I can test my pseudo-loc'd strings?

This is explained in [Use the Multilingual App Toolkit 4.0](use-mat.md).

## What kind of localizability issues can I find using pseudo language?

This is explained in [Use the Multilingual App Toolkit 4.0](use-mat.md).

## I'm not seeing any translations when I launch my app, or my app is only partially translated

Open the .xlf file in the Multilingual Editor to see whether the translations are present. When strings in the default language .resw file are changed explicitly, any corresponding translations are removed from .xlf files. This is to ensure that a translation matches its source string. Translate the string(s) in the .xlf file(s), and rebuild to update the non-default language .resw file(s).

If your strings are translated in the .xlf file(s), but they're not appearing in your app, then rebuild your project to update the non-default language .resw file(s). Visual Studio optimizes the Build command to build only files that have changed since the last Build.

Check your language preference order. Ensure that the language you want to test is listed at the top of your language preference list in **Settings**.

## The toolkit is reporting error  0x80004004 in the build output

```dosbatch
Merge of Loc PRI file failed calling makepri.exe: "0x80004004"
```

This message can be displayed when the region format conflicts with the toolkit build operation. The workaround is to change your language in **Settings** to en-US while building.


## The toolkit is reporting error  0x80004005 in the build output

```dosbatch
Merge of Loc PRI file failed calling makepri.exe: "0x80004005"
```

This message can be displayed when the .xlf file contains an unsupported target language. For example, "zh-cht" is incorrect (change it to "zh-hant"), and "zh-chs" is incorrect (change it to "zh-hans").

## Is there a way to find out more information about the errors I'm seeing?

Yes, you can turn on verbose logging in Visual Studio. Click **Tools** > **Options** > **Projects and Solutions** > **Build and Run**. Change **MSBuild project build output verbosity** from Minimal to Normal or higher.

Running MSBuild from the command line can also produce additional messages.

```dosbatch
msbuild /t:rebuild <project-name>
```

## Import translation failed

The import process performs basic validation before importing. This ensures that the target culture information in the files being imported matches that in the existing .xlf files. Open the .xlf files in the Multilingual Editor and make sure that the culture information matches.

## What if my translator doesn't have Windows 10, and/or Visual Studio and/or the Multilingual App Toolkit installed?

When you select **Output: Mail recipient** in the Export string resources dialog, the email includes a link to download and install the Multilingual App Toolkit (MAT) 4.0. Your translator can still install the MAT 4.0 standalone Multilingual Editor tool even without Windows 10 nor Visual Studio.

For more details, see [Use the Multilingual App Toolkit 4.0](use-mat.md).

## What happened to the `MarkupRules.xml` and `ResourcesLocks.xml` files?

The Multilingual App Toolkit 4.0 doesn't use proprietary resource locking files. Instead, the XLIFF 1.2 tag `<mrk>` is added directly to the .xlf file to identify strings that are not modified during Machine Translation. This enables the XLIFF file to be self-contained, and allows for per-file based resource locking.

These extra support files are no longer needed and you can safely delete them if you have them.

## What happened to the .tpx file?

The .tpx file provided an easy way to include the `MarkupRules.xml` and `ResourcesLocks.xml` files when the .xlf file was sent out for translation. This functionality is no longer required.

If you have translations in a .tpx file that you need to retrieve, simply rename the .tpx file extension to .zip. This will allow you to open and extract the contents with File Explorer or any .zip compatible tool.

## I think I've done everything right, but it still isn't working

Try these steps.

1. Add translations using one of the methods already described.
2. Dump the .pri file (see [MakePri.exe command-line options](../../app-resources/makepri-exe-command-options.md)) to see whether your translations are in the .pri file. Translations will appear with language code and translated value like this.
   ```xml
   <Candidate qualifiers="Language-QPS-PLOC" type="String">
       <Value>[!!_Ŝéãřćĥ_!!]</Value>
   </Candidate>
   ```
3. Build from a Command Prompt; the resulting error may have more details than what is reported in the build output.

## My app failed certification to the Microsoft Store

Before you start the Microsoft Store Certification process, you must exclude the `<project-name>.qps-ploc.xlf` file from your project. Pseudo language is used to detect potential localizability issues or bugs, but it is not a valid Microsoft Store language. If it is not removed then your app will fail during the Microsoft Store Certification process.

## Related topics

* [Use the Multilingual App Toolkit 4.0](use-mat.md)
* [Microsoft Translator](https://www.microsofttranslator.com/)
* [MakePri.exe command-line options](../../app-resources/makepri-exe-command-options.md)
