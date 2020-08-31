---
title: Intro to certificates
description: This article discusses the use of certificates in Universal Windows Platform (UWP) apps.
ms.assetid: 4EA2A9DF-BA6B-45FC-AC46-2C8FC085F90D
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, security
ms.localizationpriority: medium
---
# Intro to certificates




This article discusses the use of certificates in Universal Windows Platform (UWP) apps. Digital certificates are used in public key cryptography to bind a public key to a person, computer, or organization. The bound identities are most often used to authenticate one entity to another. For example, certificates are often used to authenticate a web server to a user and a user to a web server. You can create certificate requests and install or import issued certificates. You can also enroll a certificate in a certificate hierarchy.

### Shared certificate stores

UWP apps use the new isolationist application model introduced in Windows 8. In this model, apps run in low-level operating system construct, called an app container, that prohibits the app from accessing resources or files outside of itself unless explicitly permitted to do so. The following sections describe the implications this has on public key infrastructure (PKI).

### Certificate storage per app container

Certificates that are intended for use in a specific app container are stored in per user, per app container locations. An app running in an app container has write access to only its own certificate storage. If the application adds certificates to any of its stores, these certificates cannot be read by other apps. If an app is uninstalled, any certificates specific to it are also removed. An app also has read access to local machine certificate stores other than the MY and REQUEST store.

### Cache

Each app container has an isolated cache in which it can store issuer certificates needed for validation, certificate revocation lists (CRL), and online certificate status protocol (OCSP) responses.

### Shared certificates and keys

When a smart card is inserted into a reader, the certificates and keys contained on the card are propagated to the user MY store where they can be shared by any full-trust application the user is running. By default, however, app containers do not have access to the per user MY store.

To address this issue and enable groups of principals to access groups of resources, the app container isolation model supports the capabilities concept. A capability allows an app container process to access a specific resource. The sharedUserCertificates capability grants an app container read access to the certificates and keys contained in the user MY store and the Smart Card Trusted Roots store. The capability does not grant read access to the user REQUEST store.

You specify the sharedUserCertificates capability in the manifest as shown in the following example.

```xml
<Capabilities>
    <Capability Name="sharedUserCertificates" />
</Capabilities>
```

## Certificate fields


The X.509 public key certificate standard has been revised over time. Each successive version of the data structure has retained the fields that existed in the previous versions and added more, as shown in the following illustration.

![x.509 certificate versions 1, 2, and 3](images/x509certificateversions.png)

Some of these fields and extensions can be specified directly when you use the [**CertificateRequestProperties**](/uwp/api/Windows.Security.Cryptography.Certificates.CertificateRequestProperties) class to create a certificate request. Most cannot. These fields can be filled by the issuing authority or they can be left blank. For more information about the fields, see the following sections:

### Version 1 fields

| Field | Description |
|-------|-------------|
| Version | Specifies the version number of the encoded certificate. Currently, the possible values of this field are 0, 1, or 2. |
| Serial Number | Contains a positive, unique integer assigned by the certification authority (CA) to the certificate. |
| Signature Algorithm | Contains an object identifier (OID) that specifies the algorithm used by the CA to sign the certificate. For example, 1.2.840.113549.1.1.5 specifies a SHA-1 hashing algorithm combined with the RSA encryption algorithm from RSA Laboratories. |
| Issuer | Contains the X.500 distinguished name (DN) of the CA that created and signed the certificate. |
| Validity | Specifies the time interval during which the certificate is valid. Dates through the end of 2049 use the Coordinated Universal Time (Greenwich Mean Time) format (yymmddhhmmssz). Dates beginning with January 1st, 2050 use the generalized time format (yyyymmddhhmmssz). |
| Subject | Contains an X.500 distinguished name of the entity associated with the public key contained in the certificate. |
| Public Key | Contains the public key and associated algorithm information. |

### Version 2 fields

An X.509 version 2 certificate contains the basic fields defined in version 1 and adds the following fields.

| Field | Description |
|-------|-------------|
| Issuer Unique Identifier | Contains a unique value that can be used to make the X.500 name of the CA unambiguous when reused by different entities over time. |
| Subject Unique Identifier | Contains a unique value that can be used to make the X.500 name of the certificate subject unambiguous when reused by different entities over time. |

### Version 3 extensions

An X.509 version 3 certificate contains the fields defined in version 1 and version 2 and adds certificate extensions.

| Field  | Description |
|--------|-------------|
| Authority Key Identifier | Identifies the certification authority (CA) public key that corresponds to the CA private key used to sign the certificate. |
| Basic Constraints | Specifies whether the entity can be used as a CA and, if so, the number of subordinate CAs that can exist beneath it in the certificate chain. |
| Certificate Policies | Specifies the policies under which the certificate has been issued and the purposes for which it can be used. |
| CRL Distribution Points | Contains the URI of the base certificate revocation list (CRL). |
| Enhanced Key Usage | Specifies the manner in which the public key contained in the certificate can be used. |
| Issuer Alternative Name | Specifies one or more alternative name forms for the issuer of the certificate request. |
| Key Usage | Specifies restrictions on the operations that can be performed by the public key contained in the certificate.|
| Name Constraints  | Specifies the namespace within which all subject names in a certificate hierarchy must be located. The extension is used only in a CA certificate. |
| Policy Constraints | Constrains path validation by prohibiting policy mapping or by requiring that each certificate in the hierarchy contain an acceptable policy identifier. The extension is used only in a CA certificate. |
| Policy Mappings | Specifies the policies in a subordinate CA that correspond to policies in the issuing CA. |
| Private Key Usage Period | Specifies a different validity period for the private key than for the certificate with which the private key is associated. |
| Subject Alternative Name | Specifies one or more alternative name forms for the subject of the certificate request. Example alternative forms include email addresses, DNS names, IP addresses, and URIs. |
| Subject Directory Attributes | Conveys identification attributes such as the nationality of the certificate subject. The extension value is a sequence of OID-value pairs. |
| Subject Key Identifier | Differentiates between multiple public keys held by the certificate subject. The extension value is typically a SHA-1 hash of the key. |