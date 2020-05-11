---
Description: The Multilingual App Toolkit (MAT) 4.0 integrates with Microsoft Visual Studio 2019 to provide Windows apps with translation support, translation file management, and editor tools.
title: Use the Multilingual App Toolkit
template: detail.hbs
ms.date: 01/23/2018
ms.topic: article
keywords: windows 10, uwp, globalization, localizability, localization
ms.localizationpriority: medium
---

# Use the Multilingual App Toolkit 4.0

The Multilingual App Toolkit (MAT) 4.0 integrates with Microsoft Visual Studio 2019 to provide Windows apps with translation support, translation file management, and editor tools. Here are some of the value propositions of the toolkit.

- Helps you manage resource changes and translation status during development.
- Provides a UI for choosing languages based on configured translation providers.
- Supports the localization industry-standard XLIFF file format.
- Provides a pseudo-language engine to help identify translation issues during development.
- Connects with the Microsoft Language Portal to easily access translated strings and terminology.
- Connects with the Microsoft Translator for quick translation suggestions.

## How to use the toolkit

### Step 1. Design your app for globalization and localization

Before you can use the MAT effectively, your app needs to be localizable. Specifically, your project should contain one or more Resources Files (.resw) containing your app's strings in the default language. For details, see [Localize strings in your UI and app package manifest](../../app-resources/localize-strings-ui-manifest.md). Once you've done that, the toolkit makes adding additional languages quick and easy.

For the value proposition of globalization and localization&mdash;as well as definitions of the terms **globalization**, **localizability**, and **localization**&mdash;see [Globalization and localization](globalizing-portal.md).

Also see [Guidelines for globalization](guidelines-and-checklist-for-globalizing-your-app.md) and [Make your app localizable](prepare-your-app-for-localization.md).

### Step 2. Download and install the Multilingual App Toolkit 4.0

There are two parts to the Multilingual App Toolkit 4.0 (MAT 4.0), each with its own installer.

- [Multilingual App Toolkit 4.0 Extension for Visual Studio 2017 and later](https://marketplace.visualstudio.com/items?itemName=MultilingualAppToolkit.MultilingualAppToolkit-18308). This contains the MAT 4.0 extension for Visual Studio 2019, in the form of a .vsix installer.
- [Multilingual App Toolkit 4.0 Editor](https://developer.microsoft.com/windows/develop/multilingual-app-toolkit). This contains the MAT 4.0 standalone Multilingual Editor tool, in the form of an .msi installer. It also includes the MAT 4.0 extension for Visual Studio 2015 and for Visual Studio 2013.

If you use Visual Studio 2017 or Visual Studio 2019, then download and run both installers, one after the other. If you use Visual Studio 2015 or Visual Studio 2013, then download and run the .msi installer.

### Step 3. Enable the Multilingual App Toolkit for your project

The MAT must be enabled for your project before you can begin to localize the app. Here's how to enable the toolkit.

- Open the project solution in Visual Studio.
- Select the desired project in Solution Explorer.
- On the **Tools** menu, select **Multilingual App Toolkit** > **Enable selection**. 

In the Output window (showing output from Multilingual App Toolkit), watch for the message `Project '<project-name>' was enabled. The project's source culture is '<language-tag>' <language-name>`. If this message appears, then the MAT is ready to use.

### Step 4. Add languages to your project

Follow these steps to add languages to your project.

1. Right-click the project node in Solution Explorer.
2. Click **Multilingual App Toolkit** > **Add translation languages...**.
3. In the Translation Languages dialog, select the language(s) you want to support, and click OK.

The toolkit does these things in response.

- For each language you added, a new folder is created named for the [BCP-47 language tag](https://tools.ietf.org/html/bcp47) of the language. Inside that folder, new Resources File(s) (.resw) are created to match the one(s) that contain the default language strings.
- If this is the first time you've added a language, a new folder named `MultilingualResources` is added to the project. Inside that folder, an .xlf file is added for each language. The .xlf files contain a translation unit for each string in each Resources File (.resw) in your project.
- The Output window confirms the addition of the language(s) that you added.

Whenever you add/remove a default language Resources File (.resw), or you add/remove a string inside a default language Resources File (.resw), rebuild the project to re-synchronize the .xlf files. This ensures that the .xlf files contain the union of the strings in the default language.

Installed Translation Providers&mdash;such as the [Microsoft Language Portal](https://www.microsoft.com/Language/) and [Microsoft Translator](https://www.microsofttranslator.com/)&mdash;can be used to translate your app's resources. When a provider supports a specific language, the provider's icon is displayed next to the language name in the Translation Languages dialog.

In the Translation Languages dialog, any existing .xlf-based languages that are discovered by the toolkit have their selection box pre-checked to indicate that the language is already included the project.

Once a language is added to the project, it cannot be removed by un-checking the box in the Translation Languages dialog. To remove a language, right-click on the language-specific .xlf file and select **Delete**. If you confirm, then this will also delete the corresponding Resources File (.resw).

### Step 5. Test your app using pseudo language

Pseudo language is an artificial modification of the software product intended to simulate real language localization, but remaining readable to native speakers. Pseudo translation replaces characters and expands the resource string length to detect potential localizability issues or bugs early in the project cycle and before localization starts in earnest.

Follow these steps to pseudo-localize and test your project.

1. Use the Translation Languages dialog to add Pseudo Language (Pseudo) [qps-ploc] to your project.
2. Right-click the `<project-name>.qps-ploc.xlf` file in Solution Explorer and click **Multilingual App Toolkit** > **Generate machine translations**.
3. In **Settings** > **Time & Language** > **Region & language** > **Languages**, click **Add a language**.
5. In the search box, type `qps-ploc`.
6. Click `English (qps-ploc)` to add it.
7. From the language list, select `English (qps-ploc)` and click **Set as default**.
8. Test your pseudo-localized app. For example, look for UI layout issues where not all of a string is displayed (the string is truncated), or strings that are not translated (but instead hard-coded).

In addition to character replacement and expansion, the pseudo engine provides a unique tracking identifier for each resource. This tracker is prepended to the start of every string and enclosed within brackets `[xxxxx]`. You can use these trackers during visual UI inspection testing. They can help track down specific resources in the product, especially if multiple resources have similar or duplicate text.

In this "Hello, World!" text example, the pseudo translation expands to take about 30 percent more screen space, and then applies the resource tracker.

`"Hello World" -> "Ĥèĺļõ Ŵòŗłđ" -> "[!!_Ĥèĺļõ Ŵòŗłđ_!!]" -> "[hJ8s1][!!_Ĥèĺļõ Ŵòŗłđ_!!]"`

### Step 6. Translate your app into selected languages

The Multilingual App Toolkit is integrated into the build process. During a build, updated strings are automatically added to each language .xlf file.
After you've tested your app by using Pseudo language, there are three options to translate your app into other languages for release.

#### Option 1. Translate the strings yourself

You can use the Multilingual Editor to translate strings individually. As already mentioned, this is included in [The .msi installer](https://developer.microsoft.com/windows/develop/multilingual-app-toolkit).

- Right-click the .xlf file that you want to translate.
- Click **Open With...** and select Multilingual Editor. You can optionally click **Set as Default**.
- For each string, **Source** shows the original string in the default language. In **Translation**, type the string translated into the appropriate language for the .xlf file that you're editing.
- When you're done, save and close the file.

Rebuild your project to cause the translated strings to be copied into the Resources File (.resw) that corresponds to the .xlf file you were just editing.

You can also launch the Multilingual Editor like this. Go to Start, show all apps, open the Multilingual App Toolkit folder, and click Multilingual Editor to launch it.

#### Option 2. Send the .xlf files to a third party for translation

To outsource the translation and editing work to localizers, select the desired .xlf files in Solution Explorer, right-click them, and click **Multilingual App Toolkit** > **Export translations...**.

Select **Output: Mail recipient** in the Export string resources dialog, and click OK, and your files will be zipped and attached to a new email. Select **Output: File folder location**, browser for a folder and click OK, optionally choose for the files to be zipped, click OK again, and your files will be (zipped and) saved at the location you chose, inside a new folder named for your project.

After your localizers complete the translation work and send you the translated .xlf files, you can import them into your project. Select the desired .xlf files in Solution Explorer, right-click them, and click **Multilingual App Toolkit** > **Import/recycle translations...**. Click **Add**, navigate to the .xlf or .zip files, and click **Import**.

**Note** The import process performs basic validation before importing. This ensures that the target culture information in the files being imported matches that in the existing .xlf files.

Rebuild your project to cause the translated strings to be copied into the Resources File(s) (.resw) that corresponds to the .xlf file(s) you just imported.

These third party providers offer localization services, and may be able to assist you.

- [Elanex](https://www.strakertranslations.com/)
- [Keywords Studios](https://www.keywordsstudios.com/)
- [Lionbridge](https://www.lionbridge.com)
- [Moravia](https://www.rws.com/what-we-do/rws-moravia/)
- [SDL](https://www.sdl.com/translate/get-started/instant-quote.html)
- [Welocalize](https://www.welocalize.com/)

> [!NOTE]
> The list above is provided for informational purposes only and is not an endorsement. Microsoft does not make any representation or warranty regarding these vendors or their services, and under no circumstances will Microsoft have any liability for your use of such vendors or services. Any questions, complaints, or claims regarding such vendors or their services must be directed to the appropriate vendor.

#### Option 3. Use the integrated translation services

Translation services are integrated into the Visual Studio IDE as well as into the Multilingual Editor. This provides easy access to translation services while developing your product as well as localizing your resources. For this service, you'll need an Azure account subscription, as described in [Microsoft Translator Moves to the Azure portal](https://multilingualapptoolkit.uservoice.com/knowledgebase/articles/1167898-microsoft-translator-moves-to-the-azure-portal).

To access the translation services inside Visual Studio, select and right-click one or more .xlf files in Solution Explorer and click **Generate machine translations**.

The Multilingual Editor provides the same translation support, as well as adding interactive translation suggestions, which enable you to select the translation that best fits your resource strings. After the translation suggestion is provided, you can fine-tune the string for your translation style.

Two providers are shipped with the Multilingual App Toolkit.

- The [Microsoft Language Portal](https://www.microsoft.com/Language/) provider enables translation-recycling and terminology-matching support based on translations of the user interface text for Microsoft products and services.
- The [Microsoft Translator](https://www.microsofttranslator.com/) provider enables on-demand machine translation services.

You and your translator(s) can manage the status of translations in the Multilingual Editor to review uncertain translations later. You can set the status of each string in the **Properties** tab. Status values are: **New**, **Needs review**, **Translated**, **Final**, and **Signed off**. The indicator at the left of the row shows the status. When all rows show green in the Multilingual Editor, then your translation work is done.

Rebuild your project to cause the translated strings to be copied into the Resources File(s) (.resw) that corresponds to the .xlf file(s) you just edited.

### Step 7. Upload your app to the Microsoft Store

Before you start the Microsoft Store Certification process, you must exclude the `<project-name>.qps-ploc.xlf` file from your project. Pseudo language is used to detect potential localizability issues or bugs, but it is not a valid Microsoft Store language. If it is not removed then your app will fail during the Microsoft Store Certification process.

## Related topics

* [Localize strings in your UI and app package manifest](../../app-resources/localize-strings-ui-manifest.md)
* [Globalization and localization](globalizing-portal.md)
* [Guidelines for globalization](guidelines-and-checklist-for-globalizing-your-app.md)
* [Make your app localizable](prepare-your-app-for-localization.md)
* [BCP-47 language tag](https://tools.ietf.org/html/bcp47)

## Downloads

* [Multilingual App Toolkit 4.0 .vsix installer](https://marketplace.visualstudio.com/items?itemName=MultilingualAppToolkit.MultilingualAppToolkit-18308)
* [Multilingual App Toolkit 4.0 .msi installer](https://developer.microsoft.com/windows/develop/multilingual-app-toolkit)

## Translation services

* [Microsoft Language Portal](https://www.microsoft.com/Language/)
* [Microsoft Translator](https://www.microsofttranslator.com/)
