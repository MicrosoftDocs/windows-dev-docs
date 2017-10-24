---
author: stevewhims
Description: This topic describes the schema of the MakePri.exe XML configuration file.
title: MakePri.exe configuration file
template: detail.hbs
ms.author: stwhi
ms.date: 10/18/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, resource, image, asset, MRT, qualifier
---
<link rel="stylesheet" href="https://az835927.vo.msecnd.net/sites/uwp/Resources/css/custom.css">

# MakePri.exe configuration file

This topic describes the schema of the [MakePri.exe](compile-resources-manually-with-makepri.md) XML configuration file; also known as a PRI config file. The MakePri.exe tool has a [createconfig command](makepri-exe-command-options.md#createconfig-command) that you can use to create a new, initialized PRI config file.

The PRI config file controls what resources are indexed, and how. The configuration XML must conform to the following schema.

```
<?xml version="1.0" encoding="utf-8"?>
<xs:schema attributeFormDefault="unqualified" elementFormDefault="qualified" xmlns:xs="http://www.w3.org/2001/XMLSchema">
  <xs:element name="resources">
    <xs:complexType>
      <xs:sequence>
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
    </xs:complexType>
  </xs:element>
</xs:schema>
```

- The **index** element defines discrete indexing passes that are done over the assets. Each indexing pass determines the format-specific indexers to use, and which resources to index.
- The **root** attribute is the path root of the physical file for the index pass. It can be relative or absolute. If relative, it is appended to the project root that you provide in the command line. If absolute, it is directly used as the index pass root. Back or forward slashes are acceptable. Trailing slashes are trimmed. The root of the index pass determines the folder to which all resources are considered relative.
- The **startIndexAt** attribute is the initial seed file or folder used in indexing. It is relative to the index pass root. An empty value assumes the index pass root folder.
- The **qualifiers** element sets the initial qualifiers for the first file or folder that other resources inherit. Each qualifier element must have a valid name and value (see [Tailor your resources for language, scale, high contrast, and other qualifiers](tailor-resources-lang-scale-contrast.md)).
- The **default** element specifies the context (language, scale, contrast, etc.) that should be used to resolve resources when the runtime context does not match any resource candidates. Because this context is specified at build time and does not change, resources are resolved to this context as qualifiers are created. The matched score is stored at build time. Every qualifier must have some value specified. See [ResourceContext](resource-management-system.md#resourcecontext) for details on how resources are chosen.

## Related topics

* [Compile resources manually with MakePri.exe](compile-resources-manually-with-makepri.md)
* [MakePri.exe command-line options&mdash;createconfig command](makepri-exe-command-options.md#createconfig-command)
* [Tailor your resources for language, scale, high contrast, and other qualifiers](tailor-resources-lang-scale-contrast.md)
* [Resource Management System&mdash;ResourceContext](resource-management-system.md#resourcecontext)