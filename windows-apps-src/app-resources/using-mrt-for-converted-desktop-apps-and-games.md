---
author: ptorr-msft
title: Using MRT for Converted Desktop Apps and Games
description: By packaging your .NET or Win32 app or game as an AppX package, you can leverage the Resource Management System to load app resources tailored to the run-time context. This in-depth topic describes the techniques.
ms.author: ptorr
ms.date: 10/25/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, mrt, pri. resources, games, centennial, desktop app converter, mui, satellite assembly
localizationpriority: medium
---

# Use the Windows 10 Resource Management System in a legacy app or game

## Overview

.NET and Win32 apps and games are often localized into different languages to expand their total addressable market. For more info about the value proposition of localizing your app, see [Globalization and localization](../design/globalizing/globalizing-portal.md). By packaging your .NET or Win32 app or game as an AppX package, you can leverage the Resource Management System to load app resources tailored to the run-time context. This in-depth topic describes the techniques.

There are many ways to localize a traditional Win32 application, 
but Windows 8 introduced a [new resource-management system](https://msdn.microsoft.com/en-us/library/windows/apps/jj552947.aspx) that works across programming languages, across application types, and provides functionality over and above simple localization. This system will be referred to as "MRT" in this topic. Historically, that stood for "Modern Resource Technology" but the term "Modern" has been discontinued. The resource manager might also be known as MRM (Modern Resource Manager) or PRI (Package Resource Index).

Combined with AppX-based deployment (for example, from the Microsoft Store), MRT can automatically deliver the most-applicable resources for a given user / device which minimizes the download and install size of your application. This size
reduction can be significant for applications with a large amount of localized content, perhaps on the order of several *gigabytes* for AAA games. Additional benefits of MRT include 
localized listings in the Windows Shell and the Microsoft Store, automatic fallback logic when a user's preferred language doesn't match your available resources.

This document describes the high-level architecture of MRT and provides a porting guide to help move legacy Win32 applications to MRT with minimal code changes. Once the move to MRT 
is made, additional benefits (such as the ability to segment resources by scale factor or system theme) become available to the developer. Note that MRT-based localization works for 
both UWP applications and Win32 applications processed by the Desktop Bridge (aka "Centennial").

In many situations, you can continue to use your existing localization formats and source code whilst integrating with MRT for resolving resources at runtime and minimizing download 
sizes - it's not an all-or-nothing approach. The following table summarizes the work and estimated cost/benefit of each stage. This table doesn't include non-localization tasks, such as providing high-resolution or high-contrast application icons. For more info about providing multiple 
assets for tiles, icons, etc., See [Tailor your resources for language, scale, high contrast, and other qualifiers](tailor-resources-lang-scale-contrast.md).

<table>
<tr>
<th>Work</th>
<th>Benefit</th>
<th>Estimated cost</th>
</tr>
<tr>
<td>Localize AppX manifest</td>
<td>Bare minimum work required to have your localized content appear in the Windows Shell and in the Microsoft Store</td>
<td>Small</td>
</tr>
<tr>
<td>Use MRT to identify and locate resources</td>
<td>Pre-requisite to minimizing download and install sizes; automatic language fallback</td>
<td>Medium</td>
</tr>
<tr>
<td>Build resource packs</td>
<td>Final step to minimize download and install sizes</td>
<td>Small</td>
</tr>
<tr>
<td>Migrate to MRT resource formats and APIs</td>
<td>Significantly smaller file sizes (depending on existing resource technology)</td>
<td>Large</td>
</tr>
</table>

## Introduction

Most non-trivial applications contain user-interface elements known as *resources* that are decoupled from the application's code (contrasted with *hard-coded values* that are 
authored in the source code itself). There are several reasons to prefer resources over hard-coded values - ease of editing by non-developers, for example - but one of the key reasons
is to enable the application to pick different representations of the same logical resource at runtime. For example, the text to display on a button (or the image to display in an 
icon) might differ depending on the language(s) the user understands, the characteristics of the display device, or whether the user has any assistive technologies enabled.

Thus the primary purpose of any resource-management technology is to translate, at runtime, a request for a logical or symbolic *resource name* (such as `SAVE_BUTTON_LABEL`) into the 
best possible actual *value* (eg, "Save") from a set of possible *candidates* (eg, "Save", "Speichern", or "저장"). MRT provides such a function, and enables applications to 
identify resource candidates using a wide variety of attributes, called *qualifiers*, such as the user's language, the display scale-factor, the user's selected theme, and other 
environmental factors. MRT even supports custom qualifiers for applications that need it (for example, an application could provide different graphic assets for users that had logged 
in with an account vs. guest users, without explicitly adding this check into every part of their application). MRT works with both string resources and file-based resources, where 
file-based resources are implemented as references to the external data (the files themselves). 

### Example

Here's a simple example of an application that has text labels on two buttons (`openButton` and `saveButton`) and a PNG file used for a logo (`logoImage`). The text labels are 
localized into English and German, and the logo is optimized for normal desktop displays (100% scale factor) and high-resolution phones (300% scale factor). Note that this diagram 
presents a high-level, conceptual view of the model; it does not map exactly to implementation.

<p><img src="images\conceptual-resource-model.png"/></p>

In the graphic, the application code references the three logical resource names. At runtime, the `GetResource` pseudo-function uses MRT to look those resource names up in the resource
table (known as PRI file) and find the most appropriate candidate based on the ambient conditions (the user's language and the display's scale-factor). In the case of the labels, the 
strings are used directly. In the case of the logo image, the strings are interpreted as filenames and the files are read off disk. 

If the user speaks a language other than English or
German, or has a display scale-factor other than 100% or 300%, MRT picks the "closest" matching candidate based on a set of fallback rules (see [the **Resource Management System** 
topic on MSDN](https://msdn.microsoft.com/en-us/library/windows/apps/jj552947.aspx) for more background). 

Note that MRT supports resources that are tailored to more than one qualifier - for example, if the logo image contained embedded text that also needed to be localized, the logo would 
have four candidates: EN/Scale-100, DE/Scale-100, EN/Scale-300 and DE/Scale-300.

### Sections in this document

The following sections outline the high-level tasks required to integrate MRT with your application.

**Phase 0: Build an application package**

This section outlines how to get your existing Desktop application building as an application package. No MRT features are used at this stage.

**Phase 1: Localize the application manifest**

This section outlines how to localize your application's manifest (so that it appears correctly in the Windows Shell) whilst still using your legacy resource format and API to package 
and locate resources. 

**Phase 2: Use MRT to identify and locate resources**

This section outlines how to modify your application code (and possibly resource layout) to locate resources using MRT, whilst still using your existing resource formats and APIs to 
load and consume the resources. 

**Phase 3: Build resource packs**

This section outlines the final changes needed to separate your resources into separate *resource packs*, minimizing the download (and install) size of your app.

### Not covered in this document

After completing Phases 0-3 above, you will have an application "bundle" that can be submitted to the Microsoft Store and that will minimize the download & install size for users by omitting 
the resources they don't need (eg, languages they don't speak). Further improvements in application size and functionality can be made by taking one final step. 

**Phase 4: Migrate to MRT resource formats and APIs**

This phase is beyond the scope of this document; it entails moving your resources (particularly strings) from legacy formats such as MUI DLLs or .NET resource assemblies into PRI files. 
This can lead to further space savings for download & install sizes. It also allows use of other MRT features such as minimizing the download and install of image files by based on scale 
factor, accessibility settings, and so on.

- - -

## Phase 0: Build an application package

Before you make any changes to your application's resources, you must first replace your current packaging and installation technology with the standard UWP packaging and deployment 
technology. There are three ways to do this:

0. If you have a large Desktop application with a complex installer or you utilize lots of OS extensibility points, you can use the Desktop App Converter tool to generate the UWP file layout
and manifest information from your existing app installer (eg, an MSI)
0. If you have a smaller Desktop application with relatively few files or a simple installer and no extensibility hooks, you can create the file layout and manifest information manually
0. If you're rebuilding from source and want to update your app to be a "pure" UWP application, you can create a new project in Visual Studio and rely on the IDE to do much of the work for 
you

If you want to use the [Desktop App Converter](https://aka.ms/converter), please refer to [the **Desktop to UWP Bridge: Desktop App Converter** topic on MSDN](https://aka.ms/converterdocs) 
for more information on the conversion process. A complete set of Desktop Converter samples can be found on [the **Desktop Bridge to UWP samples** GitHub repo](https://github.com/Microsoft/DesktopBridgeToUWP-Samples).

If you want to manually create the package, you will need to create a directory structure that includes all your application's files (executables and content, but not source code) and 
an `AppXManifest.xml` file. An example can be found in [the **Hello, World** GitHub sample](https://github.com/Microsoft/DesktopBridgeToUWP-Samples/blob/master/Samples/HelloWorldSample/CentennialPackage/AppxManifest.xml), 
but a basic `AppXManifest.xml` file that runs the Desktop executable named `ContosoDemo.exe` 
is as follows, where the <span style="background-color: yellow">highlighted text</span> would be replaced by your own values:

<blockquote>
<pre>
&lt;?xml version="1.0" encoding="utf-8" ?&gt;
&lt;Package xmlns="http://schemas.microsoft.com/appx/manifest/foundation/windows10"
         xmlns:mp="http://schemas.microsoft.com/appx/2014/phone/manifest"
         xmlns:uap="http://schemas.microsoft.com/appx/manifest/uap/windows10"
         xmlns:rescap="http://schemas.microsoft.com/appx/manifest/foundation/windows10/restrictedcapabilities"
         IgnorableNamespaces="uap mp rescap"&gt;
    &lt;Identity Name="<span style="background-color: yellow">Contoso.Demo</span>"
              Publisher="<span style="background-color: yellow">CN=Contoso.Demo</span>"
              Version="<span style="background-color: yellow">1.0.0.0</span>" /&gt;
    &lt;Properties&gt;
    &lt;DisplayName&gt;<span style="background-color: yellow">Contoso App</span>&lt;/DisplayName&gt;
    &lt;PublisherDisplayName&gt;<span style="background-color: yellow">Contoso, Inc</span>&lt;/PublisherDisplayName&gt;
    &lt;Logo&gt;Assets\StoreLogo.png&lt;/Logo&gt;
  &lt;/Properties&gt;
    &lt;Dependencies&gt;
    &lt;TargetDeviceFamily Name="Windows.Desktop" MinVersion="10.0.14393.0" 
                        MaxVersionTested="10.0.14393.0" /&gt;
  &lt;/Dependencies&gt;
    &lt;Resources&gt;
    &lt;Resource Language="<span style="background-color: yellow">en-US</span>" /&gt;
  &lt;/Resources&gt;
    &lt;Applications&gt;
    &lt;Application Id="<span style="background-color: yellow">ContosoDemo</span>" Executable="<span style="background-color: yellow">ContosoDemo.exe</span>" 
                 EntryPoint="Windows.FullTrustApplication"&gt;
    &lt;uap:VisualElements DisplayName="<span style="background-color: yellow">Contoso Demo</span>" BackgroundColor="#777777" 
                        Square150x150Logo="Assets\Square150x150Logo.png" 
                        Square44x44Logo="Assets\Square44x44Logo.png" 
        Description="<span style="background-color: yellow">Contoso Demo</span>"&gt;
      &lt;/uap:VisualElements&gt;
    &lt;/Application&gt;
  &lt;/Applications&gt;
    &lt;Capabilities&gt;
    &lt;rescap:Capability Name="runFullTrust" /&gt;
  &lt;/Capabilities&gt;
&lt;/Package&gt;
</pre>
</blockquote>

For more information about the `AppXManifest.xml` file and package layout, see [the **App package manifest** topic on MSDN](https://msdn.microsoft.com/en-us/library/windows/apps/br211474.aspx).

Finally, if you're using Visual Studio to create a new project and migrate your existing code across, see [the MSDN documentation for building a new UWP project](https://msdn.microsoft.com/en-us/windows/uwp/get-started/create-a-hello-world-app-xaml-universal). 
You can include your existing code into the new project, but you will likely have to make significant code changes (particularly in the user interface) in order to run as a "pure" UWP. 
These changes are outside the scope of this document.

***

## Phase 1: Localize the application manifest

### Step 1.1: Update strings & assets in the AppXManifest

In Phase 0 you created a basic `AppXManifest.xml` file for your application (based on values provided to the converter, extracted from the MSI, or manually entered into the manifest) 
but it will not contain localized information, nor will it support additional features like high-resolution Start tile assets, etc. 

To ensure your application's name and description are correctly localized, you must define some resources in a set of resource files, and update the AppX Manifest to reference them.

**Creating a default resource file**

The first step is to create a default resource file in your default language (eg, US English). You can do this either manually with a text editor, or via the Resource Designer in Visual 
Studio.

If you want to create the resources manually:

0. Create an XML file named `resources.resw` and place it in a `Strings\en-us` subfolder of your project. 
 * Use the appropriate BCP-47 code if your default language is not US English 
0. In the XML file, add the following content, where the <span style="background-color: yellow">highlighted text</span> is replaced with the appropriate text for your app, in your default
language.

**Note** There are restrictions on the lengths of some of these strings. For more info, see [VisualElements](/uwp/schemas/appxpackage/appxmanifestschema/element-visualelements?branch=live).

<blockquote>
<pre>
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;root&gt;
  &lt;data name="ApplicationDescription"&gt;
    &lt;value&gt;<span style="background-color: yellow">Contoso Demo app with localized resources (English)</span>&lt;/value&gt;
  &lt;/data&gt;
  &lt;data name="ApplicationDisplayName"&gt;
    &lt;value&gt;<span style="background-color: yellow">Contoso Demo Sample (English)</span>&lt;/value&gt;
  &lt;/data&gt;
  &lt;data name="PackageDisplayName"&gt;
    &lt;value&gt;<span style="background-color: yellow">Contoso Demo Package (English)</span>&lt;/value&gt;
  &lt;/data&gt;
  &lt;data name="PublisherDisplayName"&gt;
    &lt;value&gt;<span style="background-color: yellow">Contoso Samples, USA</span>&lt;/value&gt;
  &lt;/data&gt;
  &lt;data name="TileShortName"&gt;
    &lt;value&gt;<span style="background-color: yellow">Contoso (EN)</span>&lt;/value&gt;
  &lt;/data&gt;
&lt;/root&gt;
</pre>
</blockquote>

If you want to use the designer in Visual Studio:

0. Create the `Strings\en-us` folder (or other language as appropriate) in your project and add a **New Item** to the root folder of your project, using the default name of `resources.resw`
 * Be sure to choose **Resources File (.resw)** and not **Resource Dictionary** - a Resource Dictionary is a file used by XAML applications
0. Using the designer, enter the following strings (use the same `Names` but replace the `Values` with the appropriate text for your application):

<img src="images\editing-resources-resw.png"/>

Note: if you start with the Visual Studio designer, you can always edit the XML directly by pressing `F7`. But if you start with a minimal XML file, *the designer will not recognize the 
file* because it's missing a lot of additional metadata; you can fix this by copying the boilerplate XSD information from a designer-generated file into your hand-edited XML file. 

**Update the manifest to reference the resources**

Once you have the values defined in the `.resw` file, the next step is to update the manifest to reference the resource strings. Again, you can edit an XML file directly, or rely on the 
Visual Studio Manifest Designer.

If you are editing XML directly, open the `AppxManifest.xml` file and make the following changes to the <span style="background-color: lightgreen">highlighted values</span> - use this 
*exact* text, not text specific to your application. There is no requirement to use these exact resource names&mdash;you can choose your own&mdash;but whatever you choose must exactly match whatever is in the `.resw` file. These names should match the `Names` you created in the `.resw` file, prefixed with the `ms-resource:` scheme and the 
`Resources/` namespace. 

*Note: many elements of the manifest have been omitted from this snippet - do not delete anything!*

<blockquote>
<pre>
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;Package&gt;
  &lt;Properties&gt;
    &lt;DisplayName&gt;<span style="background-color: lightgreen">ms-resource:Resources/PackageDisplayName</span>&lt;/DisplayName&gt;
    &lt;PublisherDisplayName&gt;<span style="background-color: lightgreen">ms-resource:Resources/PublisherDisplayName</span>&lt;/PublisherDisplayName&gt;
  &lt;/Properties&gt;
  &lt;Applications&gt;
    &lt;Application&gt;
      &lt;uap:VisualElements DisplayName="<span style="background-color: lightgreen">ms-resource:Resources/ApplicationDisplayName</span>"
        Description="<span style="background-color: lightgreen">ms-resource:Resources/ApplicationDescription</span>"&gt;
        &lt;uap:DefaultTile ShortName="<span style="background-color: lightgreen">ms-resource:Resources/TileShortName</span>"&gt;
          &lt;uap:ShowNameOnTiles&gt;
            &lt;uap:ShowOn Tile="square150x150Logo" /&gt;
          &lt;/uap:ShowNameOnTiles&gt;
        &lt;/uap:DefaultTile&gt;
      &lt;/uap:VisualElements&gt;
    &lt;/Application&gt;
  &lt;/Applications&gt;
&lt;/Package&gt;
</pre>
</blockquote>

If you are using the Visual Studio manifest designer, open the `Package.appxmanifest` file and change the <span style="background-color: lightgreen">highlighted values</span> values in 
the `Application` tab and the `Packaging` tab:

<img src="images\editing-application-info.png"/>
<img src="images\editing-packaging-info.png"/>

### Step 1.2: Build PRI file, make an AppX package, and verify it's working

You should now be able to build the `.pri` file and deploy the application to verify that the correct information (in your default language) is appearing in the Start Menu. 

If you're building in Visual Studio, simply press `Ctrl+Shift+B` to build the project and then right-click on the project and choose `Deploy` from the context menu. 

If you're building manually, follow these steps to create a configuration file for `MakePRI` tool and to generate the `.pri` file itself (more information can be found in 
[the **Manual app packaging** topic on MSDN](https://docs.microsoft.com/en-us/windows/uwp/packaging/manual-packaging-root)):

0. Open a developer command prompt from the `Visual Studio 2015` folder in the Start menu
0. Switch to the project root directory (the one that contains the `AppxManifest.xml` file and the `Strings` folder)
0. Type the following command, replacing "contoso_demo.xml" with a name suitable for your project, and "en-US" with the default language of your app (or keep it en-US if applicable). 
Note the xml file is created in the parent directory (**not** in the project directory) since it's not part of the application (you can choose any other directory you want, but be sure to substitute that in future commands).

```CMD
    makepri createconfig /cf ..\contoso_demo.xml /dq en-US /pv 10.0 /o
```

0. You can type `makepri createconfig /?` to see what each parameter does, but in summary:
 * `/cf` sets the Configuration Filename (the output of this command)
 * `/dq` sets the Default Qualifiers, in this case the language `en-US`
 * `/pv` sets the Platform Version, in this case Windows 10
 * `/o` sets it to Overwrite the output file if it exists
0. Now you have a configuration file, run `MakePRI` again to actually search the disk for resources and package them into a PRI file. Replace "contoso_demop.xml" with the XML filename 
you used in the previous step, and be sure to specify the parent directory for both input and output: 

    `makepri new /pr . /cf ..\contoso_demo.xml /of ..\resources.pri /mf AppX /o`
0. You can type `makepri new /?` to see what each parameter does, but in a nutshell:
 * `/pr` sets the Project Root (in this case, the current directory)
 * `/cf` sets the Configuration Filename, created in the previous step
 * `/of` sets the Output File 
 * `/mf` creates a Mapping File (so we can exclude files in the package in a later step)
 * `/o` sets it to Overwrite the output file if it exists
0. Now you have a `.pri` file with the default language resources (eg, en-US). To verify that it worked correctly, you can run the following command:

    `makepri dump /if ..\resources.pri /of ..\resources /o`
0. You can type `makepri dump /?` to see what each parameter does, but in a nutshell:
 * `/if` sets the Input Filename 
 * `/of` sets the Output Filename (`.xml` will be appended automatically)
 * `/o` sets it to Overwrite the output file if it exists
0. Finally, you can open `..\resources.xml` in a text editor and verify it lists your `<NamedResource>` values (like `ApplicationDescription` and `PublisherDisplayName`) along 
with `<Candidate>` values for your chosen default language (there will be other content in the beginning of the file; ignore that for now).

If you like, you can open the mapping file `..\resources.map.txt` to verify it contains the files needed for your project (including the PRI file, which is not part of the project's 
directory). Importantly, the mapping file will *not* include a reference to your `resources.resw` file because the contents of that file have already been embedded in the PRI file. 
It will, however, contain other resources like the filenames of your images.

**Building and signing the package**

Now the PRI file is built, you can build and sign the package:

0. To create the app package, run the following command replacing `contoso_demo.appx` with the name of the AppX file you want to create and making sure to choose a different directory 
for the `.AppX` file (this sample uses the parent directory; it can be anywhere but should **not** be the project directory):

    `makeappx pack /m AppXManifest.xml /f ..\resources.map.txt /p ..\contoso_demo.appx /o`
0. You can type `makeappx pack /?` to see what each parameter does, but in a nutshell:
 * `/m` sets the Manifest file to use
 * `/f` sets the mapping File to use (created in the previous step) 
 * `/p` sets the output Package name
 * `/o` sets it to Overwrite the output file if it exists
0. Once the package is created, it must be signed. The easiest way to get a signing certificate is by creating an empty Universal Windows project in Visual Studio and copying the `.pfx` 
file it creates, but you can create one manually using the `MakeCert` and `Pvk2Pfx` utilities as described in [the **How to create an app package signing certificate** topic on MSDN]
(https://msdn.microsoft.com/en-us/library/windows/desktop/jj835832(v=vs.85).aspx). 
 * **Important:** If you manually create a signing certificate, make sure you place the files in a different directory than your source project or your package source, otherwise it 
might get included as part of the package, including the private key!
0. To sign the package, use the following command. Note that the `Publisher` specified in the `Identity` element of the `AppxManifest.xml` must match the `Subject` of the certificate 
(this is **not** the `<PublisherDisplayName>` element, which is the localized display name to show to users). As usual, replace the `contoso_demo...` filenames with the names 
appropriate for your project, and (**very important**) make sure the `.pfx` file is not in the current directory (otherwise it would have been created as part of your package, 
including the private signing key!):

    `signtool sign /fd SHA256 /a /f ..\contoso_demo_key.pfx ..\contoso_demo.appx`
0. You can type `signtool sign /?` to see what each parameter does, but in a nutshell:
 * `/fd` sets the File Digest algorithm (SHA256 is the default for AppX)
 * `/a` will Automatically select the best certificate
 * `/f` specifies the input File that contains the signing certificate

Finally, you can now double-click on the `.appx` file to install it, or if you prefer the command-line you can open a PowerShell prompt, change to the directory containing the package, 
and type the following (replacing `contoso_demo.appx` with your package name):

```CMD
    add-appxpackage contoso_demo.appx
```

If you receive errors about the certificate not being trusted, make sure it is added to the machine store (**not** the user store). To add the certificate to the machine store, you can 
either use the command-line or Windows Explorer.

To use the command-line:

0. Run a Visual Studio 2015 command prompt as an Administrator.
0. Switch to the directory that contains the `.cer` file (remember to ensure this is outside of your source or project directories!)
0. Type the following command, replacing `contoso_demo.cer` with your filename:

    `certutil -addstore TrustedPeople contoso_demo.cer`
0. You can run `certutil -addstore /?` to see what each parameter does, but in a nutshell:
 * `-addstore` adds a certificate to a certificate store
 * `TrustedPeople` indicates the store into which the certificate is placed

To use Windows Explorer:

0. Navigate to the folder that contains the `.pfx` file
0. Double-click on the `.pfx` file and the **Certicicate Import Wizard** should appear
0. Choose `Local Machine` and click `Next`
0. Accept the User Account Control admin elevation prompt, if it appears, and click `Next`
0. Enter the password for the private key, if there is one, and click `Next`
0. Select `Place all certificates in the following store`
0. Click `Browse`, and choose the `Trusted People` folder (**not** "Trusted Publishers")
0. Click `Next` and then `Finish`

After adding the certificate to the `Trusted People` store, try installing the package again.

You should now see your app appear in the Start Menu's "All Apps" list, with the correct information from the `.resw` / `.pri` file. If you see a blank string or the string 
`ms-resource:...` then something has gone wrong - double check your edits and make sure they're correct. If you right-click on your app in the Start Menu, you can Pin it as a tile and 
verify the correct information is displayed there also.

### Step 1.3: Add more supported languages

Once the changes have been made to the AppX manifest and the initial `resources.resw` file has been created, adding additional languages is easy.

**Create additional localized resources**

First, create the additional localized resource values. 

Within the `Strings` folder, create additional folders for each language you support using the appropriate BCP-47 code (for example, `Strings\de-DE`). Within each of these folders, 
create a `resources.resw` file (using either an XML editor or the Visual Studio designer) that includes the translated resource values. It is assumed you already have the localized 
strings available somewhere, and you just need to copy them into the `.resw` file; this document does not cover the translation step itself. 

For example, the `Strings\de-DE\resources.resw` file might look like this, with the <span style="background-color: yellow">highlighted text</span> changed from `en-US`:

<blockquote>
<pre>
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;root&gt;
  &lt;data name="ApplicationDescription"&gt;
    &lt;value&gt;<span style="background-color: yellow">Contoso Demo app with localized resources (German)</span>&lt;/value&gt;
  &lt;/data&gt;
  &lt;data name="ApplicationDisplayName"&gt;
    &lt;value&gt;<span style="background-color: yellow">Contoso Demo Sample (German)</span>&lt;/value&gt;
  &lt;/data&gt;
  &lt;data name="PackageDisplayName"&gt;
    &lt;value&gt;<span style="background-color: yellow">Contoso Demo Package (German)</span>&lt;/value&gt;
  &lt;/data&gt;
  &lt;data name="PublisherDisplayName"&gt;
    &lt;value&gt;<span style="background-color: yellow">Contoso Samples, DE</span>&lt;/value&gt;
  &lt;/data&gt;
  &lt;data name="TileShortName"&gt;
    &lt;value&gt;<span style="background-color: yellow">Contoso (DE)</span>&lt;/value&gt;
  &lt;/data&gt;
&lt;/root&gt;
</pre>
</blockquote>

The following steps assume you added resources for both `de-DE` and `fr-FR`, but the same pattern can be followed for any language.

**Update AppX manifest to list supported languages**

The AppX manifest must be updated to list the languages supported by the app. The Desktop App Converter adds the default language, but the others must be added explicitly. If you're 
editing the `AppxManifest.xml` file directly, update the `Resources` node as follows, adding as many elements as you need, and substituting the 
<span style="background-color: yellow">appropriate languages you support</span> and making sure the first entry in the list is the default (fallback) language. 
In this example, the default is English (US) with additional support for both German (Germany) and French (France):

<blockquote>
<pre>
&lt;Resources&gt;
  &lt;Resource Language="<span style="background-color: yellow">EN-US</span>" /&gt;
  &lt;Resource Language="<span style="background-color: yellow">DE-DE</span>" /&gt;
  &lt;Resource Language="<span style="background-color: yellow">FR-FR</span>" /&gt;
&lt;/Resources&gt;
</pre>
</blockquote>

If you are using Visual Studio, you shouldn't need to do anything; if you look at `Package.appxmanifest` you should see the special <span style="background-color: yellow">x-generate</span> value, which causes the build process 
to insert the languages it finds in your project (based on the folders named with BCP-47 codes). Note that this is not a valid value for a real Appx Manifest; it only works for Visual 
Studio projects:

<blockquote>
<pre>
&lt;Resources&gt;
  &lt;Resource Language="<span style="background-color: yellow">x-generate</span>" /&gt;
&lt;/Resources&gt;
</pre>
</blockquote>

**Re-build with the localized values**

Now you can build and deploy your application, again, and if you change your language preference in Windows you should see the newly-localized values appear in the Start menu (instructions 
for how to change your language are below).

For Visual Studio, again you can just use `Ctrl+Shift+B` to build, and right-click the project to `Deploy`.

If you're manually building the project, follow the same steps as above but add the additional languages, separated by underscores, to the default qualifiers list (`/dq`) when creating 
the configuration file. For example, to support the English, German, and French resources added in the previous step:

```CMD
    makepri createconfig /cf ..\contoso_demo.xml /dq en-US_de-DE_fr-FR /pv 10.0 /o
```

This will create a PRI file that contains all the specified languagesthat you can easily use for testing. If the total size of your resources is small, or you only support a small
number of languages, this might be acceptable for your shipping app; it's only if you want the benefits of minimizing install / download size for your resources that you need
to do the additional work of building separate language packs.

**Test with the localized values**

To test the new localized changes, you simply add a new preferred UI language to Windows. There is no need to download language packs, reboot the system, or have your entire Windows 
UI appear in a foreign language. 

0. Run the `Settings` app (`Windows + I`)
0. Go to `Time & language`
0. Go to `Region & language`
0. Click `Add a language`
0. Type (or select) the language you want (eg `Deutsch` or `German`)
 * If there are sub-languages, choose the one you want (eg, `Deutsch / Deutschland`)
0. Select the new language in the language list
0. Click `Set as default`

Now open the Start menu and search for your application, and you should see the localized values for the selected language (other apps might also appear localized). If you don't 
see the localized name right away, wait a few minutes until the Start Menu's cache is refreshed. To return to your native language, just make it the default language in the language 
list. 

### Step 1.4: Localizing more parts of the AppX manifest (optional)

Other sections of the AppX Manifest can be localized. For example, if your application handles file-extensions then it should have a `windows.fileTypeAssociation` extension in the 
manifest, using the <span style="background-color: lightgreen">green highlighted text</span> exactly as shown (since it will refer to resources), and replacing the
<span style="background-color: yellow">yellow highlighted text</span> with information specific to your application:

<blockquote>
<pre>
&lt;Extensions&gt;
  &lt;uap:Extension Category="windows.fileTypeAssociation"&gt;
    &lt;uap:FileTypeAssociation Name="default"&gt;
      &lt;uap:DisplayName&gt;<span style="background-color: lightgreen">ms-resource:Resources/FileTypeDisplayName</span>&lt;/uap:DisplayName&gt;
      &lt;uap:Logo&gt;<span style="background-color: yellow">Assets\StoreLogo.png</span>&lt;/uap:Logo&gt;
      &lt;uap:InfoTip&gt;<span style="background-color: lightgreen">ms-resource:Resources/FileTypeInfoTip</span>&lt;/uap:InfoTip&gt;
      &lt;uap:SupportedFileTypes&gt;
        &lt;uap:FileType ContentType="<span style="background-color: yellow">application/x-contoso</span>"&gt;<span style="background-color: yellow">.contoso</span>&lt;/uap:FileType&gt;
      &lt;/uap:SupportedFileTypes&gt;
    &lt;/uap:FileTypeAssociation&gt;
  &lt;/uap:Extension&gt;
&lt;/Extensions&gt;
</pre>
</blockquote>

You can also add this information using the Visual Studio Manifest Designer, using the `Declarations` tab, taking note of the
<span style="background-color: lightgreen">highlighted values</span>:

<p><img src="images\editing-declarations-info.png"/></p>

Now add the corresponding resource names to each of your `.resw` files, replacing the <span style="background-color: yellow">highlighted text</span> with the appropriate 
text for your app (remember to do this for *each supported language!*):

<blockquote>
<pre>
... existing content...

&lt;data name="FileTypeDisplayName"&gt;
  &lt;value&gt;<span style="background-color: yellow">Contoso Demo File</span>&lt;/value&gt;
&lt;/data&gt;
&lt;data name="FileTypeInfoTip"&gt;
  &lt;value&gt;<span style="background-color: yellow">Files used by Contoso Demo App</span>&lt;/value&gt;
&lt;/data&gt;
</pre>
</blockquote>

This will then show up in parts of the Windows shell, such as File Explorer:

<p><img src="images\file-type-tool-tip.png"/></p>

Build and test the package as before, exercising any new scenarios that should show the new UI strings.

- - -

## Phase 2: Use MRT to identify and locate resources

The previous section showed how to use MRT to localize your app's manifest file so that the Windows Shell can correctly display the app's name and other metadata. No code changes 
were required for this; it simply required the use of `.resw` files and some additional tools. This section will show how to use MRT to locate resources in your existing resource 
formats and using your existing resource-handling code with minimal changes.

### Assumptions about existing file layout & application code

Because there are many ways to localize Win32 Desktop apps, this paper will make some simplifying assumptions about the existing application's structure that you will need to map 
to your specific environment. You might need to make some changes to your existing codebase or resource layout to conform to the requirements of MRT, and those are largely out of 
scope for this document.

**Resource file layout**

This whitepaper assumes your localized resources all have the same filenames (eg, `contoso_demo.exe.mui` or `contoso_strings.dll` or `contoso.strings.xml`) but that they are placed 
in different folders with BCP-47 names (`en-US`, `de-DE`, etc.). It doesn't matter how many resource files you have, what their names are, what their file-formats / associated APIs 
are, etc. The only thing that matters is that every *logical* resource has the same filename (but placed in a different *physical* directory). 

As a counter-example, if your application uses a flat file-structure with a single `Resources` directory containing the files `english_strings.dll` and `french_strings.dll`, it 
would not map well to MRT. A better structure would be a `Resources` directory with subdirectories and files `en\strings.dll` and `fr\strings.dll`. It's also possible to use the same base filename but with embedded qualifiers, such as `strings.lang-en.dll` and `strings.lang-fr.dll`, but using directories with the language codes is conceptually simpler so it's what we'll focus on.

**Note** It is still possible to use MRT and the benefits of AppX packaging even if you can't follow this filenaming convention; it just requires more work.

For example, the application might have a set of custom UI commands (used for button labels etc.) in a simple text file named <span style="background-color: yellow">ui.txt</span>, 
laid out under a <span style="background-color: yellow">UICommands</span> folder:

<blockquote>
<pre>
+ ProjectRoot
|--+ Strings
|  |--+ en-US
|  |  \--- resources.resw
|  \--+ de-DE
|     \--- resources.resw
|--+ <span style="background-color: yellow">UICommands</span>
|  |--+ en-US
|  |  \--- <span style="background-color: yellow">ui.txt</span>
|  \--+ de-DE
|     \--- <span style="background-color: yellow">ui.txt</span>
|--- AppxManifest.xml
|--- ...rest of project...
</pre>
</blockquote>

**Resource loading code**

This whitepaper assumes that at some point in your code you want to locate the file that contains a localized resource, load it, and then use it. The APIs used to load the resources, 
the APIs used to extract the resources, etc. are not important. In pseudocode, there are basically three steps:

```
set userLanguage = GetUsersPreferredLanguage()
set resourceFile = FindResourceFileForLanguage(MY_RESOURCE_NAME, userLanguage)
set resource = LoadResource(resourceFile) 
    
// now use 'resource' however you want
```

MRT only requires changing the first two steps in this process - how you determine the best candidate resources and how you locate them. It doesn't require you to change how you load 
or use the resources (although it provides facilities for doing that if you want to take advantage of them).
 
For example, the application might use the Win32 API `GetUserPreferredUILanguages`, the CRT function `sprintf`, and the Win32 API `CreateFile` to replace the three pseudocode functions 
above, then manually parse the text file looking for `name=value` pairs. (The details are not important; this is merely to illustrate that MRT has no impact on the techniques used to 
handle resources once they have been located).

### Step 2.1: Code changes to use MRT to locate files

Switching your code to use MRT for locating resources is not difficult. It requires using a handful of WinRT types and a few lines of code. The main types that you will use are as follows:

* [ResourceContext](https://docs.microsoft.com/en-us/uwp/api/Windows.ApplicationModel.Resources.Core.ResourceContext), which encapsulates the currently active set of qualifier values 
(language, scale factor, etc.)
* [ResourceManager](https://docs.microsoft.com/en-us/uwp/api/windows.applicationmodel.resources.core.resourcemanager) (the WinRT version, not the .NET version), which enables access to 
all the resources from the PRI file
* [ResourceMap](https://docs.microsoft.com/en-us/uwp/api/windows.applicationmodel.resources.core.resourcemap), which represents a specific subset of the resources in the PRI file (in 
this example, the file-based resources vs. the string resources)
* [NamedResource](https://docs.microsoft.com/en-us/uwp/api/Windows.ApplicationModel.Resources.Core.NamedResource), which represents a logical resource and all its possible candidates
* [ResourceCandidate](https://docs.microsoft.com/en-us/uwp/api/windows.applicationmodel.resources.core.resourcecandidate), which represents a single concrete candidate resource 

In pseudo-code, the way you would resolve a given resource file name (like `UICommands\ui.txt` in the sample above) is as follows:

```
// Get the ResourceContext that applies to this app
set resourceContext = ResourceContext.GetForViewIndependentUse()
    
// Get the current ResourceManager (there's one per app)
set resourceManager = ResourceManager.Current
    
// Get the "Files" ResourceMap from the ResourceManager
set fileResources = resourceManager.MainResourceMap.GetSubtree("Files")
    
// Find the NamedResource with the logical filename we're looking for,
// by indexing into the ResourceMap
set desiredResource = fileResources["UICommands\ui.txt"]
    
// Get the ResourceCandidate that best matches our ResourceContext
set bestCandidate = desiredResource.Resolve(resourceContext)
   
// Get the string value (the filename) from the ResourceCandidate
set absoluteFileName = bestCandidate.ValueAsString
```

Note in particular that the code does **not** request a specific language folder - like `UICommands\en-US\ui.txt` - even though that is how the files exist on-disk. Instead, it
asks for the *logical* filename `UICommands\ui.txt` and relies on MRT to find the appropriate on-disk file in one of the language directories.

From here, the sample app could continue to use `CreateFile` to load the `absoluteFileName` and parse the `name=value` pairs just as before; none of that logic needs to change in the 
app. If you are writing in C# or C++/CX, the actual code is not much more complicated than this (and in fact many of the intermediate variables can be elided) - see the section on **Loading .NET 
resources**, below. C++/WRL-based applications will be more complex due to the low-level COM-based APIs used to activate and call the WinRT APIs, but the fundamental steps you take are 
the same - see the section on **Loading Win32 MUI resources**, below.

**Loading .NET resources**

Because .NET has a built-in mechanism for locating and loading resources (known as "Satellite Assemblies"), there is no explicit code to replace as in the synthetic example above - in 
.NET you just need your resource DLLs in the appropriate directories and they are automatically located for you. When an app is packaged as an AppX using resource packs, the directory 
structure is somewhat different - rather than having the resource directories be subdirectories of the main application directory, they are peers of it (or not present at all if the 
user doesn't have the language listed in their preferences). 

For example, imagine a .NET application with the following layout, where all the files exist under the `MainApp` folder:

<blockquote>
<pre>
+ MainApp
|--+ en-us
|  \--- MainApp.resources.dll
|--+ de-de
|  \--- MainApp.resources.dll
|--+ fr-fr
|  \--- MainApp.resources.dll
\--- MainApp.exe
</pre>
</blockquote>

After conversion to AppX, the layout will look something like this, assuming `en-US` was the default language and the user has both German and French listed in their language list:

<blockquote>
<pre>
+ WindowsAppsRoot
|--+ MainApp_neutral
|  |--+ en-us
|  |  \--- <span style="background-color: yellow">MainApp.resources.dll</span>
|  \--- MainApp.exe
|--+ MainApp_neutral_resources.language_de
|  \--+ de-de
|     \--- <span style="background-color: yellow">MainApp.resources.dll</span>
\--+ MainApp_neutral_resources.language_fr
   \--+ fr-fr
      \--- <span style="background-color: yellow">MainApp.resources.dll</span>
</pre>
</blockquote>

Because the localized resources no longer exist in sub-directories underneath the main executable's install location, the built-in .NET resource resolution fails. Luckily, .NET has 
a well-defined mechanism for handling failed assembly load attempts - the `AssemblyResolve` event. A .NET app using MRT must register for this event and provide the missing assembly 
for the .NET resource subsystem. 

A concise example of how to use the WinRT APIs to locate satellite assemblies used by .NET is as follows; the code as-presented is intentionally compressed to show a minimal implementation, 
although you can see it maps closely to the pseudo-code above, with the passed-in `ResolveEventArgs` providing the name of the assembly we need to locate. A runnable version of this code 
(with detailed comments and error-handling) can be found in the file `PriResourceRsolver.cs` in [the **.NET Assembly Resolver** sample on GitHub](https://aka.ms/fvgqt4).

```C#
static class PriResourceResolver
{
  internal static Assembly ResolveResourceDll(object sender, ResolveEventArgs args)
  {
    var fullAssemblyName = new AssemblyName(args.Name);
    var fileName = string.Format(@"{0}.dll", fullAssemblyName.Name);

    var resourceContext = ResourceContext.GetForViewIndependentUse();
    resourceContext.Languages = new[] { fullAssemblyName.CultureName };

    var resource = ResourceManager.Current.MainResourceMap.GetSubtree("Files")[fileName];

    // Note use of 'UnsafeLoadFrom' - this is required for apps installed with AppX, but
    // in general is discouraged. The full sample provides a safer wrapper of this method
    return Assembly.UnsafeLoadFrom(resource.Resolve(resourceContext).ValueAsString);
  }
}
```

Given the class above, you would add the following somewhere early-on in your application's startup code (before any localized resources would need to load):

```C#
void EnableMrtResourceLookup()
{
  AppDomain.CurrentDomain.AssemblyResolve += PriResourceResolver.ResolveResourceDll;
}
```

The .NET runtime will raise the `AssemblyResolve` event whenever it can't find the resource DLLs, at which point the provided event handler will locate the desired file via MRT and return the assembly.

**Note** If your app already has an `AssemblyResolve` handler for other purposes, you will need to integrate the resource-resolving code with your existing code.

**Loading Win32 MUI resources**

Loading Win32 MUI resources is essentially the same as loading .NET Satellite Assemblies, but using either C++/CX or C++/WRL code instead. Using C++/CX allows for much simpler code 
that closely matches the C# code above, but it uses C++ language extensions, compiler switches, and additional runtime overheard you might wish to avoid. If that is the case, using 
C++/WRL provides a much lower-impact solution at the cost of more verbose code. Nevertheless, if you are familiar with ATL programming (or COM in general) then WRL should feel familiar. 

The following sample function shows how to use C++/WRL to load a specific resource DLL and return an `HINSTANCE` that can be used to load further resources using the usual Win32 
resource APIs. Note that unlike the C# sample that explicitly initializes the `ResourceContext` with the language requested by the .NET runtime, this code relies on the user's current 
language.

```CPP
#include <roapi.h>
#include <wrl\client.h>
#include <wrl\wrappers\corewrappers.h>
#include <Windows.ApplicationModel.resources.core.h>
#include <Windows.Foundation.h>
   
#define IF_FAIL_RETURN(hr) if (FAILED((hr))) return hr;
    
HRESULT GetMrtResourceHandle(LPCWSTR resourceFilePath,  HINSTANCE* resourceHandle)
{
  using namespace Microsoft::WRL;
  using namespace Microsoft::WRL::Wrappers;
  using namespace ABI::Windows::ApplicationModel::Resources::Core;
  using namespace ABI::Windows::Foundation;
    
  *resourceHandle = nullptr;
  HRESULT hr{ S_OK };
  RoInitializeWrapper roInit{ RO_INIT_SINGLETHREADED };
  IF_FAIL_RETURN(roInit);
    
  // Get Windows.ApplicationModel.Resources.Core.ResourceManager statics
  ComPtr<IResourceManagerStatics> resourceManagerStatics;
  IF_FAIL_RETURN(GetActivationFactory(
    HStringReference(
    RuntimeClass_Windows_ApplicationModel_Resources_Core_ResourceManager).Get(),
    &resourceManagerStatics));
    
  // Get .Current property
  ComPtr<IResourceManager> resourceManager;
  IF_FAIL_RETURN(resourceManagerStatics->get_Current(&resourceManager));
    
  // get .MainResourceMap property
  ComPtr<IResourceMap> resourceMap;
  IF_FAIL_RETURN(resourceManager->get_MainResourceMap(&resourceMap));
    
  // Call .GetValue with supplied filename
  ComPtr<IResourceCandidate> resourceCandidate;
  IF_FAIL_RETURN(resourceMap->GetValue(HStringReference(resourceFilePath).Get(),
    &resourceCandidate));
    
  // Get .ValueAsString property
  HString resolvedResourceFilePath;
  IF_FAIL_RETURN(resourceCandidate->get_ValueAsString(
    resolvedResourceFilePath.GetAddressOf()));
    
  // Finally, load the DLL and return the hInst.
  *resourceHandle = LoadLibraryEx(resolvedResourceFilePath.GetRawBuffer(nullptr),
    nullptr, LOAD_LIBRARY_AS_DATAFILE | LOAD_LIBRARY_AS_IMAGE_RESOURCE);
    
  return S_OK;
}
```

## Phase 3: Building resource packs

Now that you have a "fat pack" that contains all resources, there are two paths towards building separate main package and resource packages in order to minimize download and install 
sizes:

0. Take an existing fat pack and run it through [the Bundle Generator tool](https://aka.ms/bundlegen) to automatically create resource packs. This is the preferred approach if you 
have a build system that already produces a fat pack and you want to post-process it to generate the resource packs.
0. Directly produce the individual resource packages and build them into a bundle. This is the preferred approach if you have more control over your build system and can build the 
packages directly.

### Step 3.1: Creating the bundle

**Using the Bundle Generator tool**

In order to use the Bundle Generator tool, the PRI config file created for the package needs to be manually updated to remove the `<packaging>` section.

If you're using Visual Studio, refer to [the **Ensure that resources are installed...** topic on MSDN](https://msdn.microsoft.com/en-us/library/dn482043.aspx) for information on 
how to build all languages into the main package by creating the files `priconfig.packaging.xml` and `priconfig.default.xml`.

If you're manually editing files, follow these steps: 

0. Create the config file the same way as before, substituting the correct path, file name and languages:

    `makepri createconfig /cf ..\contoso_demo.xml /dq en-US_de-DE_es-MX /pv 10.0 /o`
0. Manually open the created `.xml` file and delete the entire `&lt;packaging&rt;` section (but keep everything else intact):

<blockquote>
<pre>
&lt;?xml version="1.0" encoding="UTF-8" standalone="yes" ?&gt; 
&lt;resources targetOsVersion="10.0.0" majorVersion="1"&gt;
  &lt;!-- Packaging section has been deleted... --&gt;
  &lt;index root="\" startIndexAt="\"&gt;
    &lt;default&gt;
    ...
    ...
</pre>
</blockquote>

0. Build the `.pri` file and the `.appx` package as before, using the updated configuration file and the appropriate directory and file names (see above for more information on 
these commands):

```CMD
makepri new /pr . /cf ..\contoso_demo.xml /of ..\resources.pri /mf AppX /o
makeappx pack /m AppXManifest.xml /f ..\resources.map.txt /p ..\contoso_demo.appx /o
```

0. Once the package has been created, use the following command to create the bundle, using the appropriate directory and file names:

    `BundleGenerator.exe -Package ..\contoso_demo.appx -Destination ..\bundle -BundleName contoso_demo`

Now you can move to the final step, which is Signing (see below).

**Manually creating resource packages**

Manually creating resource packages requires running a slightly different set of commands to build separate `.pri` and `.appx` files - these are all similar to the commands used 
above to create fat packages, so minimal explanation is given. Note: All the commands assume that the current directory is the directory containing the `AppXManifest.xml` file, 
but all files are placed into the parent directory (you can use a different directory, if necessary, but you shouldn't pollute the project directory with any of these files). As 
always, replace the "Contoso" filenames with your own file names.

0. Use the following command to create a config file that names **only** the default language as the default qualifier - in this case, `en-US`:

    `makepri createconfig /cf ..\contoso_demo.xml /dq en-US /pv 10.0 /o`
0. Create a default `.pri` and `.map.txt` file for the main package, plus an additional set of files for each language found in your project, with the following command:

    `makepri new /pr . /cf ..\contoso_demo.xml /of ..\resources.pri /mf AppX /o`
0. Use the following command to create the main package (which contains the executable code and default language resources). As always, change the name as you see fit, although 
you should put the package in a separate directory to make creating the bundle easier later (this example uses the `..\bundle` directory):

    `makeappx pack /m .\AppXManifest.xml /f ..\resources.map.txt /p ..\bundle\contoso_demo.main.appx /o`
0. After the main package has been created, use the following command once for each additional language (ie, repeat this command for each language map file generated in the previous 
step). Again, the output should be in a separate directory (the same one as the main package). Note the language is specified **both** in the `/f` option and the `/p` option, and the 
use of the new `/r` argument (which indicates a Resource Package is desired):

    `makeappx pack /r /m .\AppXManifest.xml /f ..\resources.language-de.map.txt /p ..\bundle\contoso_demo.de.appx /o`
0. Combine all the packages from the bundle directory into a single `.appxbundle` file. The new `/d` option specifies the directory to use for all the files in the bundle (this is why 
the `.appx` files are put into a separate directory in the previous step):

    `makeappx bundle /d ..\bundle /p ..\contoso_demo.appxbundle /o`

The final step to building the package is Signing.

### Step 3.2: Signing the bundle

Once you have created the `.appxbundle` file (either through the Bundle Generator tool or manually) you will have a single file that contains the main package plus all the resource 
packages. The final step is to sign the file so that Windows will install it:

    signtool sign /fd SHA256 /a /f ..\contoso_demo_key.pfx ..\contoso_demo.appxbundle

This will produce a signed `.appxbundle` file that contains the main package plus all the language-specific resource packages. It can be double-clicked just like a package file to 
install the app plus any appropriate language(s) based on the user's Windows language preferences.

## Related topics

* [Tailor your resources for language, scale, high contrast, and other qualifiers](tailor-resources-lang-scale-contrast.md)