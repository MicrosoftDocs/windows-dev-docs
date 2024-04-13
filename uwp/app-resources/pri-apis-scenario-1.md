---
description: In this scenario, we'll make a new app to represent our custom build system. We'll create a resource indexer and add strings and other kinds of resources to it. Then we'll generate and dump a PRI file.
title: Scenario 1 Generate a PRI file from string resources and asset files
template: detail.hbs
ms.date: 05/07/2018
ms.topic: article
keywords: windows 10, uwp, resource, image, asset, MRT, qualifier
ms.localizationpriority: medium
---
# Scenario 1: Generate a PRI file from string resources and asset files
In this scenario, we'll use the [package resource indexing (PRI) APIs](/windows/desktop/menurc/pri-indexing-reference) to make a new app to represent our custom build system. The purpose of this custom build system, remember, is to create PRI files for a target UWP app. So, as part of this walkthrough, we'll create some sample resource files (containing strings, and other kinds of resources) to represent that target UWP app's resources.

## New project
Begin by creating a new project in Microsoft Visual Studio. Create a **Visual C++ Windows Console Application** project, and name it *CBSConsoleApp* (for "custom build system console app").

Choose *x64* from the **Solution Platforms** drop-down.

## Headers, static library, and dll
The PRI APIs are declared in the MrmResourceIndexer.h header file (which is installed to `%ProgramFiles(x86)%\Windows Kits\10\Include\<WindowsTargetPlatformVersion>\um\`). Open the file `CBSConsoleApp.cpp` and include the header along with some other headers that you'll need.

```cppwinrt
#include <string>
#include <windows.h>
#include <MrmResourceIndexer.h>
```

The APIs are implemented in MrmSupport.dll, which you access by linking to the static library MrmSupport.lib. Open your project's **Properties**, click **Linker** > **Input**, edit **AdditionalDependencies** and add `MrmSupport.lib`.

Build the Solution, and then copy `MrmSupport.dll` from `C:\Program Files (x86)\Windows Kits\10\bin\<WindowsTargetPlatformVersion>\x64\` to your build output folder (probably `C:\Users\%USERNAME%\source\repos\CBSConsoleApp\x64\Debug\`).

Add the following helper function to `CBSConsoleApp.cpp`, since we'll be needing it.

```cppwinrt
inline void ThrowIfFailed(HRESULT hr)
{
	if (FAILED(hr))
	{
		// Set a breakpoint on this line to catch Win32 API errors.
		throw new std::exception();
	}
}
```

In the `main()` function, add calls to initialize and uninitialize COM.

```cppwinrt
int main()
{
	::ThrowIfFailed(::CoInitializeEx(nullptr, COINIT_MULTITHREADED));
	
	// More code will go here.
	
	::CoUninitialize();
}
```

## Resource files belonging to the target UWP app
Now we'll need some sample resource files (containing strings, and other kinds of resources) to represent the target UWP app's resources. These, of course, can be located anywhere on your file system. But for this walkthrough it'll be convenient to put them in the project folder of CBSConsoleApp so that everything is in one place. You only need to add these resource files to the file system; don't add them to the CBSConsoleApp project.

Inside the same folder that contains `CBSConsoleApp.vcxproj`, add a new subfolder named `UWPAppProjectRootFolder`. Inside that new subfolder, create these sample resource files.

### \UWPAppProjectRootFolder\sample-image.png
This file can contain any PNG image.

### \UWPAppProjectRootFolder\resources.resw
```xml
<?xml version="1.0"?>
<root>
	<data name="LocalizedString1">
		<value>LocalizedString1-neutral</value>
	</data>
	<data name="LocalizedString2">
		<value>LocalizedString2-neutral</value>
	</data>
	<data name="NeutralOnlyString">
		<value>NeutralOnlyString-neutral</value>
	</data>
</root>
```

### \UWPAppProjectRootFolder\de-DE\resources.resw
```xml
<?xml version="1.0"?>
<root>
	<data name="LocalizedString2">
		<value>LocalizedString2-de-DE</value>
	</data>
</root>
```

### \UWPAppProjectRootFolder\en-US\resources.resw
```xml
<?xml version="1.0"?>
<root>
	<data name="LocalizedString1">
		<value>LocalizedString1-en-US</value>
	</data>
	<data name="EnOnlyString">
		<value>EnOnlyString-en-US</value>
	</data>
</root>
```

## Index the resources, and create a PRI file
In the `main()` function, before the call to initialize COM, declare some strings that we'll need, and also create the output folder in which we'll be generating our PRI file.

```cppwinrt
std::wstring projectRootFolderUWPApp{ L"UWPAppProjectRootFolder" };
std::wstring generatedPRIsFolder{ projectRootFolderUWPApp + L"\\Generated PRIs" };
std::wstring filePathPRI{ generatedPRIsFolder + L"\\resources.pri" };
std::wstring filePathPRIDumpBasic{ generatedPRIsFolder + L"\\resources-pri-dump-basic.xml" };

::CreateDirectory(generatedPRIsFolder.c_str(), nullptr);
```

Immediately after the call to Initialize COM, declare a resource indexer handle and then call [**MrmCreateResourceIndexer**](/windows/desktop/menurc/mrmcreateresourceindexer) to create a resource indexer.

```cppwinrt
MrmResourceIndexerHandle indexer;
::ThrowIfFailed(::MrmCreateResourceIndexer(
	L"OurUWPApp",
	projectRootFolderUWPApp.c_str(),
	MrmPlatformVersion::MrmPlatformVersion_Windows10_0_0_0,
	L"language-en_scale-100_contrast-standard",
	&indexer));
```

Here's an explanation of the arguments being passed to **MrmCreateResourceIndexer**.

- The package family name of our target UWP app, which will be used as the resource map name when we later generate a PRI file from this resource indexer.
- The project root of our target UWP app. In other words, the path to our resource files. We specify this so that we can then specify paths relative to that root in subsequent API calls to the same resource indexer.
- The version of Windows that we want to target.
- A list of default resource qualifiers.
- A pointer to our resource indexer handle so that the function can set it.

The next step is to add our resources to the resource indexer that we just created. `resources.resw` is a Resources File (.resw) that contains the neutral strings for our target UWP app. Scroll up (in this topic) if you want to see its contents. `de-DE\resources.resw` contains our German strings, and `en-US\resources.resw` our English strings. To add the string resources inside a Resources File to a resource indexer, you call [**MrmIndexResourceContainerAutoQualifiers**](/windows/desktop/menurc/mrmindexresourcecontainerautoqualifiers). Thirdly, we call the [**MrmIndexFile**](/windows/desktop/menurc/mrmindexfile) function to a file containing a neutral image resource to the resource indexer.

```cppwinrt
::ThrowIfFailed(::MrmIndexResourceContainerAutoQualifiers(indexer, L"resources.resw"));
::ThrowIfFailed(::MrmIndexResourceContainerAutoQualifiers(indexer, L"de-DE\\resources.resw"));
::ThrowIfFailed(::MrmIndexResourceContainerAutoQualifiers(indexer, L"en-US\\resources.resw"));
::ThrowIfFailed(::MrmIndexFile(indexer, L"ms-resource:///Files/sample-image.png", L"sample-image.png", L""));
```

In the call to **MrmIndexFile**, the value L"ms-resource:///Files/sample-image.png" is the resource uri. The first path segment is "Files", and that's what will be used as the resource map subtree name when we later generate a PRI file from this resource indexer.

Having briefed the resource indexer about our resource files, it's time to have it generate us a PRI file on disk by calling the [**MrmCreateResourceFile**](/windows/desktop/menurc/mrmcreateresourcefile) function.

```cppwinrt
::ThrowIfFailed(::MrmCreateResourceFile(indexer, MrmPackagingModeStandaloneFile, MrmPackagingOptionsNone, generatedPRIsFolder.c_str()));
```

At this point, a PRI file named `resources.pri` has been created inside a folder named `Generated PRIs`. Now that we're done with the resource indexer, we call [**MrmDestroyIndexerAndMessages**](/windows/desktop/menurc/mrmdestroyindexerandmessages) to destroy its handle and release any machine resources that it allocated.

```cppwinrt
::ThrowIfFailed(::MrmDestroyIndexerAndMessages(indexer));
```

Since a PRI file is binary, it's going to be easier to view what we've just generated if we dump the binary PRI file to its XML equivalent. A call to [**MrmDumpPriFile**](/windows/desktop/menurc/mrmdumpprifile) does just that.

```cppwinrt
::ThrowIfFailed(::MrmDumpPriFile(filePathPRI.c_str(), nullptr, MrmDumpType::MrmDumpType_Basic, filePathPRIDumpBasic.c_str()));
```

Here's an explanation of the arguments being passed to **MrmDumpPriFile**.

- The path to the PRI file to dump. We're not using the resource indexer in this call (we just destroyed it), so we need to specify a full file path.
- No schema file. We'll discuss what a schema is later in the topic.
- Just the basic info.
- The path of an XML file to create.

This is what the PRI file, dumped to XML here, contains.

```xml
<?xml version="1.0" encoding="UTF-8" standalone="yes"?>
<PriInfo>
	<ResourceMap name="OurUWPApp" version="1.0" primary="true">
		<Qualifiers>
			<Language>en-US,de-DE</Language>
		</Qualifiers>
		<ResourceMapSubtree name="Files">
			<NamedResource name="sample-image.png" uri="ms-resource://OurUWPApp/Files/sample-image.png">
				<Candidate type="Path">
					<Value>sample-image.png</Value>
				</Candidate>
			</NamedResource>
		</ResourceMapSubtree>
		<ResourceMapSubtree name="resources">
			<NamedResource name="EnOnlyString" uri="ms-resource://OurUWPApp/resources/EnOnlyString">
				<Candidate qualifiers="Language-en-US" isDefault="true" type="String">
					<Value>EnOnlyString-en-US</Value>
				</Candidate>
			</NamedResource>
			<NamedResource name="LocalizedString1" uri="ms-resource://OurUWPApp/resources/LocalizedString1">
				<Candidate qualifiers="Language-en-US" isDefault="true" type="String">
					<Value>LocalizedString1-en-US</Value>
				</Candidate>
				<Candidate type="String">
					<Value>LocalizedString1-neutral</Value>
				</Candidate>
			</NamedResource>
			<NamedResource name="LocalizedString2" uri="ms-resource://OurUWPApp/resources/LocalizedString2">
				<Candidate qualifiers="Language-de-DE" type="String">
					<Value>LocalizedString2-de-DE</Value>
				</Candidate>
				<Candidate type="String">
					<Value>LocalizedString2-neutral</Value>
				</Candidate>
			</NamedResource>
			<NamedResource name="NeutralOnlyString" uri="ms-resource://OurUWPApp/resources/NeutralOnlyString">
				<Candidate type="String">
					<Value>NeutralOnlyString-neutral</Value>
				</Candidate>
			</NamedResource>
		</ResourceMapSubtree>
	</ResourceMap>
</PriInfo>
```

The info begins with a resource map, which is named with the package family name of our target UWP app. Enclosed by the resource map are two resource map subtrees: one for the file resources that we indexed, and another for our string resources. Notice how the package family name has been inserted into all of the resource URIs.

The first string resource is *EnOnlyString* from `en-US\resources.resw`, and it has just one candidate (which matches the *language-en-US* qualifier). Next comes *LocalizedString1* from both `resources.resw` and `en-US\resources.resw`. Consequently, it has two candidates: one matching *language-en-US*, and a fallback neutral candidate that matches any context. Similarly, *LocalizedString2* has two candidates: *language-de-DE*, and neutral. And, finally, *NeutralOnlyString* only exists in neutral form. I gave it that name to make it clear that it's not meant to be localized.

## Summary
In this scenario, we showed how to use the [package resource indexing (PRI) APIs](/windows/desktop/menurc/pri-indexing-reference) to create a resource indexer. We added string resources and asset files to the resource indexer. Then, we used the resource indexer to generate a binary PRI file. And finally we dumped the binary PRI file in the form of XML so that we could confirm that it contains the info we expected.

## Important APIs
* [Package resource indexing (PRI) reference](/windows/desktop/menurc/pri-indexing-reference)

## Related topics
* [Package resource indexing (PRI) APIs and custom build systems](pri-apis-custom-build-systems.md)
