---
description: This topic describes the format-specific indexers used by the MakePri.exe tool to generate its index of resources.
title: MakePri.exe format-specific indexers
template: detail.hbs
ms.date: 10/18/2017
ms.topic: article
keywords: windows 10, uwp, resource, image, asset, MRT, qualifier
ms.localizationpriority: medium
---
# MakePri.exe format-specific indexers

This topic describes the format-specific indexers used by the [MakePri.exe](compile-resources-manually-with-makepri.md) tool to generate its index of resources.

> [!NOTE]
> MakePri.exe is installed when you check the **Windows SDK for UWP Managed Apps** option while installing the Windows Software Development Kit. It is installed to the path `%WindowsSdkDir%bin\<WindowsTargetPlatformVersion>\x64\makepri.exe` (as well as in folders named for the other architectures). For example, `C:\Program Files (x86)\Windows Kits\10\bin\10.0.17713.0\x64\makepri.exe`.

MakePri.exe is typically used with the `new`, `versioned`, or `resourcepack` commands. See [MakePri.exe command-line options](makepri-exe-command-options.md). In those cases it indexes source files to generate an index of resources. MakePri.exe uses various individual indexers to read different source resource files or containers for resources. The simplest indexer is the folder indexer, which indexes the contents of a folder, such as `.jpg` or `.png` images.

You identify format-specific indexers by specifying `<indexer-config>` elements within an `<index>` element of the [MakePri.exe configuration file](makepri-exe-configuration.md). The `type` attribute identifies the format-specific indexer that is used.

Resource containers encountered during indexing usually get their contents indexed rather than being added to the index themselves. For example, `.resjson` files that the folder indexer finds may be further indexed by a `.resjson` indexer, in which case the `.resjson` file itself does not appear in the index. **Note** an `<indexer-config>` element for the indexer associated with that container must be included in the configuration file for that to happen.

Typically, qualifiers found on a containing entity&mdash;such as a folder or a `.resw` file&mdash;are applied to all resources within it, such as the files within the folder, or the strings within the `.resw` file.

## Folder

The folder indexer is identified by a `type` attribute of FOLDER. It indexes the contents of a folder, and determines resource qualifiers from the folder and filenames. Its configuration element conforms to the following schema.

```xml
<xs:schema attributeFormDefault="unqualified" elementFormDefault="qualified" xmlns:xs="http://www.w3.org/2001/XMLSchema">
    <xs:simpleType name="ExclusionTypeList">
        <xs:restriction base="xs:string">
            <xs:enumeration value="path"/>
            <xs:enumeration value="extension"/>
            <xs:enumeration value="name"/>
            <xs:enumeration value="tree"/>
        </xs:restriction>
    </xs:simpleType>
    <xs:complexType name="FolderExclusionType">
        <xs:attribute name="type" type="ExclusionTypeList" use="required"/>
        <xs:attribute name="value" type="xs:string" use="required"/>
        <xs:attribute name="doNotTraverse" type="xs:boolean" use="required"/>
        <xs:attribute name="doNotIndex" type="xs:boolean" use="required"/>
    </xs:complexType>
    <xs:simpleType name="IndexerConfigFolderType">
        <xs:restriction base="xs:string">
            <xs:pattern value="((f|F)(o|O)(l|L)(d|D)(e|E)(r|R))"/>
        </xs:restriction>
    </xs:simpleType>
    <xs:element name="indexer-config">
        <xs:complexType>
            <xs:sequence>
                <xs:element name="exclude" type="FolderExclusionType" minOccurs="0" maxOccurs="unbounded"/>
            </xs:sequence>
            <xs:attribute name="type" type="IndexerConfigFolderType" use="required"/>
            <xs:attribute name="foldernameAsQualifier" type="xs:boolean" use="required"/>
            <xs:attribute name="filenameAsQualifier" type="xs:boolean" use="required"/>
            <xs:attribute name="qualifierDelimiter" type="xs:string" use="required"/>
        </xs:complexType>
    </xs:element>
</xs:schema>
```

The `qualifierDelimiter` attribute specifies the character after which qualifiers are specified in a filename, ignoring the extension. The default is ".".

## PRI

The PRI indexer is identified by a `type` attribute of PRI. It indexes the contents of a PRI file. You typically use it when indexing the resource contained within another assembly, DLL, SDK, or class library into the app's PRI.

All resource names, qualifiers and values contained in the PRI file are directly maintained in the new PRI file. The top level resource map, however is not maintained in the final PRI. Resource Maps are merged.

```xml
<xs:schema id="prifile" xmlns:xs="http://www.w3.org/2001/XMLSchema" elementFormDefault="qualified">
    <xs:simpleType name="IndexerConfigPriType">
        <xs:restriction base="xs:string">
            <xs:pattern value="((p|P)(r|R)(i|I))"/>
        </xs:restriction>
    </xs:simpleType>
    <xs:element name="indexer-config">
        <xs:complexType>
            <xs:attribute name="type" type="IndexerConfigPriType" use="required"/>
        </xs:complexType>
    </xs:element>
</xs:schema>
```

## PriInfo

The PriInfo indexer is identified by a `type` attribute of PRIINFO. It indexes the contents of a detailed dump file. You produce a detailed dump file by running `makepri dump` with the `/dt detailed` option. The configuration element for the indexer conforms to the following schema.

```xml
<xs:schema id="priinfo" xmlns:xs="http://www.w3.org/2001/XMLSchema" elementFormDefault="qualified">
  <xs:simpleType name="IndexerConfigPriInfoType">
    <xs:restriction base="xs:string">
      <xs:pattern value="((p|P)(r|R)(i|I)(i|I)(n|N)(f|F)(o|O))"/>
    </xs:restriction>
  </xs:simpleType>
  <xs:element name="indexer-config">
    <xs:complexType>
      <xs:attribute name="type" type="IndexerConfigPriInfoType" use="required"/>
      <xs:attribute name="emitStrings" type="xs:boolean" use="optional"/>
      <xs:attribute name="emitPaths" type="xs:boolean" use="optional"/>
    </xs:complexType>
  </xs:element>
</xs:schema>
```

This configuration element allows for optional attributes to configure the behavior of the PriInfo indexer. The default value of `emitStrings` and `emitPaths` is `true`. If `emitStrings` is `true` then resource candidates with the `type` attribute set to "String" are be included in the index, otherwise they are excluded. If 'emitPaths' is `true` then resource candidates with the `type` attribute set to "Path" are included in the index, otherwise they are excluded.

Here is an example configuration that includes String resource types but skips Path resource types.

```xml
<indexer-config type="priinfo" emitStrings="true" emitPaths="false" />
```

To be indexed, a dump file must end with the extension `.pri.xml`, and must conform to the following schema.

```xml
<xs:schema xmlns:xs="http://www.w3.org/2001/XMLSchema" elementFormDefault="qualified" >
  <xs:simpleType name="candidateType">
    <xs:restriction base="xs:string">
      <xs:pattern value="Path|String"/>
    </xs:restriction>
  </xs:simpleType>
  <xs:complexType name="scopeType">
    <xs:sequence>
      <xs:element name="ResourceMapSubtree" type="scopeType" minOccurs="0" maxOccurs="unbounded"/>
      <xs:element name="NamedResource" minOccurs="0" maxOccurs="unbounded">
        <xs:complexType>
          <xs:sequence>
            <xs:element name="Decision" minOccurs="0" maxOccurs="unbounded">
              <xs:complexType>
                <xs:sequence>
                  <xs:any processContents="skip" minOccurs="0" maxOccurs="unbounded"/>
                </xs:sequence>
                <xs:anyAttribute processContents="skip" />
              </xs:complexType>
            </xs:element>
            <xs:element name="Candidate" minOccurs="0" maxOccurs="unbounded">
              <xs:complexType>
                <xs:sequence>
                  <xs:element name="QualifierSet" minOccurs="0" maxOccurs="unbounded">
                    <xs:complexType>
                      <xs:sequence>
                        <xs:element name="Qualifier" minOccurs="0" maxOccurs="unbounded">
                          <xs:complexType>
                            <xs:attribute name="name" type="xs:string" use="required" />
                            <xs:attribute name="value" type="xs:string" use="required" />
                            <xs:attribute name="priority" type="xs:integer" use="required" />
                            <xs:attribute name="scoreAsDefault" type="xs:decimal" use="required" />
                            <xs:attribute name="index" type="xs:integer" use="required" />
                          </xs:complexType>
                        </xs:element>
                      </xs:sequence>
                      <xs:anyAttribute processContents="skip" />
                    </xs:complexType>
                  </xs:element>
                  <xs:element name="Value" type="xs:string"  minOccurs="0" maxOccurs="unbounded"/>
                </xs:sequence>
                <xs:attribute name="type" type="candidateType" use="required" />
              </xs:complexType>
            </xs:element>
          </xs:sequence>
          <xs:attribute name="name" use="required" type="xs:string" />
          <xs:anyAttribute processContents="skip" />
        </xs:complexType>
      </xs:element>
    </xs:sequence>
    <xs:attribute name="name" use="required" type="xs:string" />
    <xs:anyAttribute processContents="skip" />
  </xs:complexType>
  <xs:element name="PriInfo">
    <xs:complexType>
      <xs:sequence>
        <xs:element name="PriHeader" >
          <xs:complexType>
            <xs:sequence>
              <xs:any minOccurs ="0" maxOccurs="unbounded" processContents="skip" />
            </xs:sequence>
            <xs:anyAttribute processContents="skip" />
          </xs:complexType>
        </xs:element>
        <xs:element name="QualifierInfo">
          <xs:complexType>
            <xs:sequence>
              <xs:any minOccurs="0" maxOccurs="unbounded" processContents="skip" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
        <xs:element name="ResourceMap">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="VersionInfo">
                <xs:complexType>
                  <xs:anyAttribute processContents="skip" />
                </xs:complexType>
              </xs:element>
              <xs:element minOccurs="0" maxOccurs="unbounded" name="ResourceMapSubtree" type="scopeType" />
            </xs:sequence>
            <xs:attribute name="name" type="xs:string" use="required" />
            <xs:anyAttribute processContents="skip" />
          </xs:complexType>
        </xs:element>
      </xs:sequence>
    </xs:complexType>
  </xs:element>
</xs:schema>
```

MakePri.exe supports dump types 'Basic', 'Detailed', 'Schema', and 'Summary'. To configure MakePri.exe to emit the dump type that the PriInfo indexer can read, include "/DumpType Detailed" when using the `dump` command.

Several elements of the `.pri.xml` file are skipped by MakePri.exe. These elements are either computed during indexing, or are specified in the MakePri.exe configuration file. Resource names, qualifiers, and values that are contained within the dump file are directly maintained in the new PRI file. The top-level resource map, however, is not maintained in the final PRI. Resource Maps are merged as part of the indexing.

This is an example of a valid String type resource from a dump file.

```xml
<NamedResource name="SampleString " index="96" uri="ms-resource://SampleApp/resources/SampleString ">
  <Decision index="2">
    <QualifierSet index="1">
      <Qualifier name="Language" value="EN-US" priority="900" scoreAsDefault="1.0" index="1"/>
    </QualifierSet>
  </Decision>
  <Candidate type="String">
    <QualifierSet index="1">
      <Qualifier name="Language" value="EN-US" priority="900" scoreAsDefault="1.0" index="1"/>
    </QualifierSet>
    <Value>A Sample String Value</Value>
  </Candidate>
</NamedResource>
```

This is an example of a valid Path type resource with two candidates from a dump file.

```xml
<NamedResource name="Sample.png" index="77" uri="ms-resource://SampleApp/Files/Images/Sample.png">
  <Decision index="2">
    <QualifierSet index="1">
      <Qualifier name="Scale" value="180" priority="500" scoreAsDefault="1.0" index="1"/>
    </QualifierSet>
    <QualifierSet index="2">
      <Qualifier name="Scale" value="140" priority="500" scoreAsDefault="0.7" index="2"/>
    </QualifierSet>
  </Decision>
  <Candidate type="Path">
    <QualifierSet index="1">
      <Qualifier name="Scale" value="180" priority="500" scoreAsDefault="1.0" index="1"/>
    </QualifierSet>
    <Value>Images\Sample.scale-180.png</Value>
  </Candidate>
  <Candidate type="Path">
    <QualifierSet index="2">
      <Qualifier name="Scale" value="140" priority="500" scoreAsDefault="1.0" index="1"/>
    </QualifierSet>
    <Value>Images\Sample.scale-140.png</Value>
  </Candidate>
</NamedResource>
```

## ResFiles

The ResFiles indexer is identified by a `type` attribute of RESFILES. It indexes the contents of a `.resfiles` file. Its configuration element conforms to the following schema.

```xml
<xs:schema id="resx" xmlns:xs="http://www.w3.org/2001/XMLSchema" elementFormDefault="qualified">
    <xs:simpleType name="IndexerConfigResFilesType">
        <xs:restriction base="xs:string">
            <xs:pattern value="((r|R)(e|E)(s|S)(f|F)(i|I)(l|L)(e|E)(s|S))"/>
        </xs:restriction>
    </xs:simpleType>
    <xs:element name="indexer-config">
        <xs:complexType>
            <xs:attribute name="type" type="IndexerConfigResFilesType" use="required"/>
            <xs:attribute name="qualifierDelimiter" type="xs:string" use="required"/>
        </xs:complexType>
    </xs:element>
</xs:schema>
```

A `.resfiles` file is a text file containing a flat list of file paths. A `.resfiles` file can contain "//" comments. Here's an example.


<pre>
Strings\component1\fr\elements.resjson
Images\logo.scale-100.png
Images\logo.scale-140.png
Images\logo.scale-180.png
</pre>


## ResJSON

The ResJSON indexer is identified by a `type` attribute of RESJSON. It indexes the contents of a `.resjson` file, which is a string resource file. Its configuration element conforms to the following schema.

```xml
<xs:schema id="resjson" xmlns:xs="http://www.w3.org/2001/XMLSchema" elementFormDefault="qualified">
    <xs:simpleType name="IndexerConfigResJsonType">
        <xs:restriction base="xs:string">
            <xs:pattern value="((r|R)(e|E)(s|S)(j|J)(s|S)(o|O)(n|N))"/>
        </xs:restriction>
    </xs:simpleType>
    <xs:element name="indexer-config">
        <xs:complexType>
            <xs:attribute name="type" type="IndexerConfigResJsonType" use="required"/>
            <xs:attribute name="initialPath" type="xs:string" use="optional"/>
        </xs:complexType>
    </xs:element>
</xs:schema>
```

A `.resjson` file contains JSON text (see [The application/json Media Type for JavaScript Object Notation (JSON)](https://www.ietf.org/rfc/rfc4627.txt)). The file must contain a single JSON object with hierarchical properties. Each property must either be another JSON object or a string value.

JSON properties with names that begin with an underscore ("_") are not compiled into the final PRI file, but are maintained in the log file.

The file can also contain "//" comments which are ignored during parsing.

The `initialPath` attribute places all resources under this initial path by prepending it to the name of the resource. You would typically use this when indexing class library resources. The default is blank.

## ResW

The ResW indexer is identified by a `type` attribute of RESW. It indexes the contents of a `.resw` file, which is a string resource file. Its configuration element conforms to the following schema.

```xml
<xs:schema id="resw" xmlns:xs="http://www.w3.org/2001/XMLSchema" elementFormDefault="qualified">
    <xs:simpleType name="IndexerConfigResxType">
        <xs:restriction base="xs:string">
            <xs:pattern value="((r|R)(e|E)(s|S)(w|W))"/>
        </xs:restriction>
    </xs:simpleType>
    <xs:element name="indexer-config">
        <xs:complexType>
            <xs:attribute name="type" type="IndexerConfigResxType" use="required"/>
            <xs:attribute name="convertDotsToSlashes" type="xs:boolean" use="required"/>
            <xs:attribute name="initialPath" type="xs:string" use="optional"/>
        </xs:complexType>
    </xs:element>
</xs:schema>
```

A `.resw` file is an XML file that conforms to the following schema.

```xml
  <xsd:schema id="root" xmlns="" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
    <xsd:import namespace="http://www.w3.org/XML/1998/namespace" />
    <xsd:element name="root" msdata:IsDataSet="true">
      <xsd:complexType>
        <xsd:choice maxOccurs="unbounded">
          <xsd:element name="metadata">
            <xsd:complexType>
              <xsd:sequence>
                <xsd:element name="value" type="xsd:string" minOccurs="0" />
              </xsd:sequence>
              <xsd:attribute name="name" use="required" type="xsd:string" />
              <xsd:attribute name="type" type="xsd:string" />
              <xsd:attribute name="mimetype" type="xsd:string" />
              <xsd:attribute ref="xml:space" />
            </xsd:complexType>
          </xsd:element>
          <xsd:element name="assembly">
            <xsd:complexType>
              <xsd:attribute name="alias" type="xsd:string" />
              <xsd:attribute name="name" type="xsd:string" />
            </xsd:complexType>
          </xsd:element>
          <xsd:element name="data">
            <xsd:complexType>
              <xsd:sequence>
                <xsd:element name="value" type="xsd:string" minOccurs="0" msdata:Ordinal="1" />
                <xsd:element name="comment" type="xsd:string" minOccurs="0" msdata:Ordinal="2" />
              </xsd:sequence>
              <xsd:attribute name="name" type="xsd:string" use="required" msdata:Ordinal="1" />
              <xsd:attribute name="type" type="xsd:string" msdata:Ordinal="3" />
              <xsd:attribute name="mimetype" type="xsd:string" msdata:Ordinal="4" />
              <xsd:attribute ref="xml:space" />
            </xsd:complexType>
          </xsd:element>
          <xsd:element name="resheader">
            <xsd:complexType>
              <xsd:sequence>
                <xsd:element name="value" type="xsd:string" minOccurs="0" msdata:Ordinal="1" />
              </xsd:sequence>
              <xsd:attribute name="name" type="xsd:string" use="required" />
            </xsd:complexType>
          </xsd:element>
        </xsd:choice>
      </xsd:complexType>
    </xsd:element>
  </xsd:schema>
```

The `convertDotsToSlashes` attribute converts all dot (".") characters found in resource names (data element name attributes) to a forward slash "/", except when the dot characters are between a "[" and "]".

The `initialPath` attribute places all resources under this initial path by prepending it to the name of the resource. You typically use this when indexing class library resources. The default is blank.

## Related topics

* [Compile resources manually with MakePri.exe](compile-resources-manually-with-makepri.md)
* [MakePri.exe command-line options](makepri-exe-command-options.md)
* [MakePri.exe configuration file](makepri-exe-configuration.md)
* [The application/json Media Type for JavaScript Object Notation (JSON)](https://www.ietf.org/rfc/rfc4627.txt)
