---
description: A package resource index (PRI) file is the binary index of an app's resources. This topic describes what a PRI file contains, how it's structured logically, and how to inspect one.
title: Package resource index (PRI) file format
template: detail.hbs
ms.date: 08/31/2026
ms.topic: article
keywords: windows 10, uwp, resource, image, asset, MRT, qualifier, PRI, resources.pri
ms.localizationpriority: medium
---
# Package resource index (PRI) file format

A package resource index (PRI) file is the binary index of an app's resources that the [Resource Management System](resource-management-system.md) reads at run-time in order to load the resource candidate that best matches the current context (language, scale, contrast, and so on).

This topic describes what a PRI file contains and how you can inspect one. For the tooling that produces PRI files, see [Compile resources manually with MakePri.exe](compile-resources-manually-with-makepri.md) and [Package resource indexing (PRI) APIs and custom build systems](pri-apis-custom-build-systems.md).

> [!IMPORTANT]
> The binary layout of a PRI file (its headers, sections, and byte-level encoding) isn't documented, and it isn't a stable contract; it can change between versions of Windows and of the Windows SDK. Don't write code that parses or produces the bytes of a PRI file directly. Use the [PRI APIs](/windows/desktop/menurc/pri-indexing-reference), [MakePri.exe](compile-resources-manually-with-makepri.md), or the run-time resource APIs described in this topic instead. Everything documented here is the *logical* structure that those supported tools and APIs expose.

## Where PRI files come from, and where they live

- A PRI file is generated at build time. Visual Studio and MSBuild generate one for you; you can also generate one yourself with [MakePri.exe](makepri-exe-command-options.md) or with the [PRI APIs](pri-apis-custom-build-systems.md).
- A package typically contains a single PRI file named `resources.pri` at the root of the package. That file is loaded automatically when the resource APIs are first used.
- If your app is split into resource packages (for example, one per language, or per scale), then each resource package contains its own PRI file, containing the additional candidates for that package. See [MakePri.exe configuration file](makepri-exe-configuration.md) for how that split is configured.
- A PRI file contains only data. It isn't a portable executable (PE) file, and it isn't loaded as a module. This is a deliberate difference from the Win32 app model, where resources are contained within DLLs.

## What a PRI file contains

Logically, a PRI file is a tree of named resources, where each named resource owns one or more candidate values. The MakePri.exe `dump` command projects that tree into XML, so a dump is a convenient way to read the structure. Here's a dump of a small PRI file.

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
			<NamedResource name="LocalizedString1" uri="ms-resource://OurUWPApp/resources/LocalizedString1">
				<Candidate qualifiers="Language-en-US" isDefault="true" type="String">
					<Value>LocalizedString1-en-US</Value>
				</Candidate>
				<Candidate type="String">
					<Value>LocalizedString1-neutral</Value>
				</Candidate>
			</NamedResource>
		</ResourceMapSubtree>
	</ResourceMap>
</PriInfo>
```

The pieces of that structure are as follows.

| Element | Description |
|-|-|
| Resource map | A named collection of resources; the root of the tree. When a PRI file is loaded from a package, the resource map name is verified to match the package identity name. A PRI file has one primary resource map, and it also records a version. At run-time, a resource map is projected as a [ResourceMap](/uwp/api/windows.applicationmodel.resources.core.resourcemap) object. |
| Qualifier set | The set of qualifier values (language, scale, contrast, and so on) that the candidates in the file are qualified by. See [Tailor your resources for language, scale, high contrast, and other qualifiers](tailor-resources-lang-scale-contrast.md). |
| Resource map subtree | A node in the tree, which contains further subtrees and named resources. Subtrees typically correspond to the resource file that the resources came from (for example, `resources` for `resources.resw`). Indexed file paths all live under a reserved `Files` subtree, so `\Images\logo.png` is indexed as `Files/images/logo.png`. |
| Named resource | A single logical resource, such as the string identifier `LocalizedString1` or the file `sample-image.png`, together with all of its variants. Each named resource has a `ms-resource` URI (see [URI schemes](uri-schemes.md)). At run-time, it's projected as a [NamedResource](/uwp/api/windows.applicationmodel.resources.core.namedresource) object. |
| Candidate | One concrete value of a named resource, together with the qualifiers that it matches. A candidate with no qualifiers is a neutral candidate, which matches any context; a candidate marked as default is the one that's used for the default qualifier values of the app. At run-time, a candidate is projected as a [ResourceCandidate](/uwp/api/windows.applicationmodel.resources.core.resourcecandidate) object. |

A candidate value is one of three kinds: a string that's stored in the PRI file itself; a path that refers to a file elsewhere in the package (the file itself isn't stored in the PRI file); or embedded data, which is arbitrary bytes stored in the PRI file. Those three kinds correspond to the `emitStrings`, `emitPaths`, and `emitEmbeddedData` attributes of the `priinfo` indexer (see [MakePri.exe format-specific indexers](makepri-exe-format-specific-indexers.md)).

A PRI file can also contain these optional parts.

- **A schema.** The schema is the structure of the tree (subtrees and named resource names) without the candidate values. It's what makes it possible to version a PRI file, or to build a resource package for an existing PRI file, without rebuilding everything. You can write the schema to a separate file with the MakePri.exe `/SchemaFile(sf)` option, and you can pass a schema file, instead of a PRI file, as the input to the `versioned` and `resourcepack` commands. A resource pack that was built with the *omitSchemaFromResourcePacks* configuration switch doesn't carry its own copy of the schema, and reading it requires the main package's PRI file as an external schema.
- **A reverse map.** A debugging-only section that maps candidates back to the source files that they were indexed from. It's generated by the MakePri.exe `/ReverseMap(rm)` option, and it isn't present by default.

For how the candidates in a PRI file are ranked and chosen at run-time, see [How the Resource Management System matches and chooses resources](how-rms-matches-and-chooses-resources.md).

## Inspect a PRI file

To read the contents of an existing PRI file, dump it to XML.

```console
makepri dump /if C:\MyApp\resources.pri /of C:\resources.pri.xml /dt Detailed
```

The `/DumpType(dt)` option controls how much detail the XML contains.

| Dump type | Description |
|-|-|
| `Basic` | The default. The resource maps, named resources, and their candidate values, as shown earlier in this topic. |
| `Detailed` | The same information, with additional detail about the file's internals. |
| `Schema` | The structure of the tree only, without candidate values. This is the same content that the `/SchemaFile(sf)` option writes. |
| `Summary` | A high-level overview of the file, such as its resource map name, version, and resource counts, rather than the individual resources. |

Use `/OutputOptions(oo)` for finer control over what's emitted, and `/ExternalSchema(es)` when you're dumping a schema-free resource pack. For the full set of options, see [MakePri.exe command-line options](makepri-exe-command-options.md#dump-command).

You can also dump a PRI file programmatically by calling the [MrmDumpPriFile](/windows/desktop/menurc/pri-indexing-reference) function; for a walkthrough, see [Scenario 1: Generate a PRI file from string resources and asset files](pri-apis-scenario-1.md).

To read a PRI file's contents at run-time instead, use the resource APIs.

- For a UWP app, use [ResourceManager](/uwp/api/windows.applicationmodel.resources.core.resourcemanager) and the other types in the [Windows.ApplicationModel.Resources.Core](/uwp/api/windows.applicationmodel.resources.core) namespace, which let you enumerate resource maps, named resources, and candidates. See [Resource Management System](resource-management-system.md).
- For an app that uses the Windows App SDK, use MRT Core. See [Manage resources with MRT Core](/windows/apps/windows-app-sdk/mrtcore/mrtcore-overview).

## Related topics

* [Resource Management System](resource-management-system.md)
* [How the Resource Management System matches and chooses resources](how-rms-matches-and-chooses-resources.md)
* [Compile resources manually with MakePri.exe](compile-resources-manually-with-makepri.md)
* [MakePri.exe command-line options](makepri-exe-command-options.md)
* [Package resource indexing (PRI) APIs and custom build systems](pri-apis-custom-build-systems.md)
* [Package resource indexing (PRI) reference](/windows/desktop/menurc/pri-indexing-reference)
* [URI schemes](uri-schemes.md)
