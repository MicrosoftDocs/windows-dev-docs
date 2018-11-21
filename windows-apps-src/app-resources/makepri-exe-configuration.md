---
Description: This topic describes the schema of the MakePri.exe XML configuration file.
title: MakePri.exe configuration file
template: detail.hbs
ms.date: 10/18/2017
ms.topic: article
keywords: windows 10, uwp, resource, image, asset, MRT, qualifier
ms.localizationpriority: medium
---
# MakePri.exe configuration file

This topic describes the schema of the [MakePri.exe](compile-resources-manually-with-makepri.md) XML configuration file; also known as a PRI config file. The MakePri.exe tool has a [createconfig command](makepri-exe-command-options.md#createconfig-command) that you can use to create a new, initialized PRI config file.

> [!NOTE]
> MakePri.exe is installed when you check the **Windows SDK for UWP Managed Apps** option while installing the Windows Software Development Kit. It is installed to the path `%WindowsSdkDir%bin\<WindowsTargetPlatformVersion>\x64\makepri.exe` (as well as in folders named for the other architectures). For example, `C:\Program Files (x86)\Windows Kits\10\bin\10.0.17713.0\x64\makepri.exe`.

The PRI config file controls what resources are indexed, and how. The configuration XML must conform to the following schema.

```xml
<?xml version="1.0" encoding="utf-8"?>
<xs:schema attributeFormDefault="unqualified" elementFormDefault="qualified" xmlns:xs="http://www.w3.org/2001/XMLSchema">
  <xs:element name="resources">
    <xs:complexType>
      <xs:sequence>
        <xs:element name="packaging" maxOccurs="1" minOccurs="0">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="autoResourcePackage" maxOccurs="unbounded" minOccurs="0">
                <xs:complexType>
                  <xs:attribute name="qualifier" type="xs:string" use="required" />
                </xs:complexType>
              </xs:element>
              <xs:element name="resourcePackage" maxOccurs="unbounded" minOccurs="0">
                <xs:complexType>
                  <xs:sequence>
                    <xs:element name="qualifierSet" maxOccurs="unbounded" minOccurs="0">
                      <xs:complexType>
                        <xs:attribute name="definition" type="xs:string" use="required" />
                      </xs:complexType>
                    </xs:element>
                  </xs:sequence>
                  <xs:attribute name="name" type="xs:string" use="required" />
                </xs:complexType>
              </xs:element>
            </xs:sequence>
          </xs:complexType>
        </xs:element>
        <xs:element maxOccurs="unbounded" name="index">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="qualifiers" minOccurs="0" maxOccurs="unbounded">
                <xs:complexType>
                  <xs:sequence>
                    <xs:element minOccurs="1" maxOccurs="unbounded" name="qualifier">
                      <xs:complexType>
                        <xs:attribute name="name" type="xs:string" use="required" />
                        <xs:attribute name="value" type="xs:string" use="required" />
                      </xs:complexType>
                    </xs:element>
                  </xs:sequence>
                </xs:complexType>
              </xs:element>
              <xs:element name="default" minOccurs="0" maxOccurs="unbounded">
                <xs:complexType>
                  <xs:sequence>
                    <xs:element minOccurs="1" maxOccurs="unbounded" name="qualifier">
                      <xs:complexType>
                        <xs:attribute name="name" type="xs:string" use="required" />
                        <xs:attribute name="value" type="xs:string" use="required" />
                      </xs:complexType>
                    </xs:element>
                  </xs:sequence>
                </xs:complexType>
              </xs:element>
              <xs:element name="indexer-config" minOccurs="0" maxOccurs="unbounded">
                <xs:complexType>
                  <xs:sequence>
                    <xs:any minOccurs="0" maxOccurs="unbounded" processContents="skip"/>
                  </xs:sequence>
                  <xs:attribute name="type" type="xs:string" use="required" />
                  <xs:anyAttribute processContents="skip"/>
                </xs:complexType>
              </xs:element>
            </xs:sequence>
            <xs:attribute name="root" type="xs:string" use="required" />
            <xs:attribute name="startIndexAt" type="xs:string" use="required" />
          </xs:complexType>
        </xs:element>
      </xs:sequence>
      <xs:attribute name="isDeploymentMergeable" type="xs:boolean" use="optional" />
      <xs:attribute name="majorVersion" type="xs:positiveInteger" use="optional" />
      <xs:attribute name="targetOsVersion" type="xs:string" use="optional" />
    </xs:complexType>
  </xs:element>
</xs:schema>
```

- The `default` element specifies the context (language, scale, contrast, etc.) that should be used to resolve resources when the runtime context does not match any resource candidates. Because this context is specified at build time and does not change, resources are resolved to this context as qualifiers are created. The matched score is stored at build time. Every qualifier must have some value specified. See [ResourceContext](resource-management-system.md#resourcecontext) for details on how resources are chosen.
- The `index` element defines discrete indexing passes that are done over the assets. Each indexing pass determines the [format-specific indexers](makepri-exe-format-specific-indexers.md) to use, and which resources to index.
- The `qualifiers` element sets the initial qualifiers for the first file or folder that other resources inherit. Each qualifier element must have a valid name and value (see [Tailor your resources for language, scale, high contrast, and other qualifiers](tailor-resources-lang-scale-contrast.md)).
- The `root` attribute is the path root of the physical file for the index pass. It can be relative or absolute. If relative, it is appended to the project root that you provide in the command line. If absolute, it is directly used as the index pass root. Back or forward slashes are acceptable. Trailing slashes are trimmed. The root of the index pass determines the folder to which all resources are considered relative.
- The `startIndexAt` attribute is the initial seed file or folder used in indexing. It is relative to the index pass root. An empty value assumes the index pass root folder.

## Default PRI config file

MakePri.exe generates this new, initialized PRI config file when the [createconfig command](makepri-exe-command-options.md#createconfig-command) is issued.

```xml
<?xml version="1.0" encoding="UTF-8" standalone="yes"?>
<resources targetOsVersion="10.0.0" majorVersion="1">
  <packaging>
    <autoResourcePackage qualifier="Language"/>
    <autoResourcePackage qualifier="Scale"/>
    <autoResourcePackage qualifier="DXFeatureLevel"/>
  </packaging>
  <index root="\" startIndexAt="\">
    <default>
      <qualifier name="Language" value="en-US"/>
      <qualifier name="Contrast" value="standard"/>
      <qualifier name="Scale" value="100"/>
      <qualifier name="HomeRegion" value="001"/>
      <qualifier name="TargetSize" value="256"/>
      <qualifier name="LayoutDirection" value="LTR"/>
      <qualifier name="Theme" value="dark"/>
      <qualifier name="AlternateForm" value=""/>
      <qualifier name="DXFeatureLevel" value="DX9"/>
      <qualifier name="Configuration" value=""/>
      <qualifier name="DeviceFamily" value="Universal"/>
      <qualifier name="Custom" value=""/>
    </default>
    <indexer-config type="folder" foldernameAsQualifier="true" filenameAsQualifier="true" qualifierDelimiter="."/>
    <indexer-config type="resw" convertDotsToSlashes="true" initialPath=""/>
    <indexer-config type="resjson" initialPath=""/>
    <indexer-config type="PRI"/>
  </index>
  <!--<index startIndexAt="Start Index Here" root="Root Here">-->
  <!--        <indexer-config type="resfiles" qualifierDelimiter="."/>-->
  <!--        <indexer-config type="priinfo" emitStrings="true" emitPaths="true" emitEmbeddedData="true"/>-->
  <!--</index>-->
</resources>
```

## Packaging element

The `packaging` element defines PRI split information. The schema for the `packaging` element is defined for both automatic (support for `autoResourcePackage` along a specific dimension), and manual configuration.

This example shows how to use `autoResourcePackage` along a specific dimension.

```xml
	<packaging>
		<autoResourcePackage qualifier="Language"/>
		<autoResourcePackage qualifier="Scale"/>
		<autoResourcePackage qualifier="DXFeatureLevel"/>
	</packaging>
```

This example shows how to use manual `resourcePackage`.

```xml
  <packaging>
    <resourcePackage name="Germany">
      <qualifierSet definition="lang-de-de"/>
      <qualifierSet definition="lang-es-es"/>
    </resourcePackage>  
    <resourcePackage name="France">
      <qualifierSet definition="lang-fr-fr"/>
    </resourcePackage>  
    <resourcePackage name="HighRes1">
      <qualifierSet definition="scale-200"/>
    </resourcePackage>
    <resourcePackage name="HighRes2">
      <qualifierSet definition="scale-400"/>
    </resourcePackage>
  </packaging>
```

MakePri.exe doesn't explicitly block generation of resource PRI files along any specific dimension. Restrictions along a certain set of dimensions are defined and implemented externally by either MakeAppx.exe or other tools in the pipeline.

MakePri.exe parses the `packaging` element after all the `index` nodes to populate all the default qualifiers. MakePri.exe collects parsed info in these data structures.

```csharp
enum ResourcePackageMode
{
    None,
    AutoPackQualifier,
    ManualPack
}

ResourcePackageMode eResourcePackageMode;
list<string> RPQualifierList; // To store AutoResourcePackage Qualifiers
map<string, list<string>> RPNameToQSIMap; // To store ResourcePackage name to QualifierSet list mapping.
```

## resources@isDeploymentMergeable attribute

This attribute sets a flag in the PRI file that causes

- Deployment merge to identify that this PRI file can merge.
- GetFullyQualifiedReference to return an error in case this flag is set and the resource manager has been initialized with a file.

The default value of this attribute is `true`. MakePri.exe only sets the flag in PRI if you target Windows 10.

We recommend that you omit `isDeploymentMergeable` (or set it explicitly to `true`) for resource pack creation if you target Windows 10.

MakePri.exe adds the value of `isDeploymentMergeable` to the dump file if `makepri dump` is run with the `/dt detailed` option.

```xml
<?xml version="1.0" encoding="UTF-8" standalone="yes"?>
<PriInfo>
	<PriHeader>
		...
		<IsDeploymentMergeable>true</IsDeploymentMergeable>
		...
	</PriHeader>
  ...
</PriInfo>
```

## resources@majorVersion attribute

The default value for this attribute is 1. If you provide an explicit value, and you also use the deprecated `/VersionMajor(vma)` command-line option for the MakePri.exe tool, then the value in the config file takes precedence.

Here's an example.

```xml
<resources majorVersion="2">
  <packaging ... />
  <index root="\" startIndexAt="\">
    ...
  </index>
</resources>
```

## resources@targetOsVersion attribute

Indicates the target operating system version. The table below shows the values that are supported; the default value is 6.3.0.

| Value | Meaning |
| ----- | ------- |
| 10.0.0 | Windows 10 |
| 6.3.0 (default) | Windows 8.1 |
| 6.2.1 | Windows 8 |

Here's an example.

```xml
<resources targetOsVersion="10.0.0">
  <packaging ... />
  <index root="\" startIndexAt="\">
    ...
  </index>
</resources>
```

**Note** Windows is backward compatible with respect to PRI files; but not always forward compatible.

MakePri.exe adds the value of `targetOsVersion` to the dump file if `makepri dump` is run with the `/dt detailed` option.

```xml
<?xml version="1.0" encoding="UTF-8" standalone="yes"?>
<PriInfo>
	<PriHeader>
		...
		<TargetOS version="10.0.0"/>
		...
	</PriHeader>
  ...
</PriInfo>
```

## Validation error messages

Here are some example error conditions, and the corresponding error message.

| Condition | Severity | Message |
| --------- | -------- | ------- |
| A targetOsVersion other than one of the supported values is specified. | Error | Invalid Configuration: Invalid targetOsVersion specified. |
| A targetOsVersion of "6.2.1" is specified and a `packaging` element is present. | Error | Invalid Configuration: 'Packaging' node is not supported with this targetOsVersion. |
| More than one mode found in the configuration. For example, Manual and AutoResourcePackage specified. | Error | Invalid Configuration: 'packaging' node cannot have more than one mode of operation. |
| A default qualifier is listed under resource package. | Error | Invalid Configuration: <Qualifiername>=<QualifierValue> is a default qualifier and its candidates cannot be added to a resource package. |
| AutoResourcePackage qualifier contains multiple qualifiers. For example, language_scale. | Error | Invalid Configuration : AutoResourcePackage with multiple qualifiers is not supported. |
| ResourcePackage QualifierSet contains multiple qualifiers. For example, language-en-us_scale-100 | Error | Invalid Configuration : QualifierSet with multiple qualifiers is not supported. |
| Duplicate resourcepack name found. | Error | Invalid Configuration : Duplicate resource pack name <rpname>. |
| Same qualifier set defined in two resource packages. | Error | Invalid Configuration: Multiple instances of QualifierSet "<qualifier tags>" found. |
| No candidates are found for the QualifierSet listed for 'ResourcePackage' node. | Warning | Invalid Configuration: No candidates found for <Resource Package Name>. |
| No candidates found for qualifier listed under ‘AutoResourcePackage’ node. | Warning | Invalid Configuration: No candidates found for qualifier <qualifier name>. Resource Package not generated. |
| None of the modes are found. That is, empty 'packaging' node found. | Warning | Invalid Configuration: No packaging mode specified. |

## Related topics

* [Compile resources manually with MakePri.exe](compile-resources-manually-with-makepri.md)
* [MakePri.exe command-line options&mdash;createconfig command](makepri-exe-command-options.md#createconfig-command)
* [Tailor your resources for language, scale, high contrast, and other qualifiers](tailor-resources-lang-scale-contrast.md)
* [Resource Management System&mdash;ResourceContext](resource-management-system.md#resourcecontext)