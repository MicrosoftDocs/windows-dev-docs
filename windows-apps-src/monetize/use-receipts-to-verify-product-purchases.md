---
ms.assetid: E322DFFE-8EEC-499D-87BC-EDA5CFC27551
description: Each Microsoft Store transaction that results in a successful product purchase can optionally return a transaction receipt.
title: Use receipts to verify product purchases
ms.date: 04/16/2018
ms.topic: article
keywords: windows 10, uwp, in-app purchases, IAPs, receipts, Windows.ApplicationModel.Store
ms.localizationpriority: medium
---
# Use receipts to verify product purchases

Each Microsoft Store transaction that results in a successful product purchase can optionally return a transaction receipt. This receipt provides information about the listed product and monetary cost to the customer.

Having access to this information supports scenarios where your app needs to verify that a user purchased your app, or has made add-on (also called in-app product or IAP) purchases from the Microsoft Store. For example, imagine a game that offers downloaded content. If the user who purchased the game content wants to play it on a different device, you need to verify that the user already owns the content. Here's how.

> [!IMPORTANT]
> This article shows how to use members of the [Windows.ApplicationModel.Store](/uwp/api/Windows.ApplicationModel.Store) namespace to get and validate a receipt for an in-app purchase. If you are using the [Windows.Services.Store](/uwp/api/Windows.Services.Store) namespace for in-app purchases (introduced in Windows 10, version 1607, and available to projects that target **Windows 10 Anniversary Edition (10.0; Build 14393)** or a later release in Visual Studio), this namespace does not provide an API for getting purchase receipts for in-app purchases. However, you can use a REST method in the Microsoft Store collection API to get data for a purchase transaction. For more information, see [Receipts for in-app purchases](in-app-purchases-and-trials.md#receipts).

## Requesting a receipt


The **Windows.ApplicationModel.Store** namespace supports several ways to get a receipt:

* When you make a purchase by using [CurrentApp.RequestAppPurchaseAsync](/uwp/api/windows.applicationmodel.store.currentapp.requestapppurchaseasync) or [CurrentApp.RequestProductPurchaseAsync](/uwp/api/windows.applicationmodel.store.currentapp.requestproductpurchaseasync) (or one of the other overloads of this method), the return value contains the receipt.
* You can call the [CurrentApp.GetAppReceiptAsync](/uwp/api/windows.applicationmodel.store.currentapp.getappreceiptasync) method to retrieve the current receipt info for your app and any add-ons in your app.

An app receipt looks something like this.

> [!NOTE]
> This example is formatted to help make the XML readable. Real app receipts do not include whitespace between elements.

> [!div class="tabbedCodeSnippets"]
```xml
<Receipt Version="1.0" ReceiptDate="2012-08-30T23:10:05Z" CertificateId="b809e47cd0110a4db043b3f73e83acd917fe1336" ReceiptDeviceId="4e362949-acc3-fe3a-e71b-89893eb4f528">
    <AppReceipt Id="8ffa256d-eca8-712a-7cf8-cbf5522df24b" AppId="55428GreenlakeApps.CurrentAppSimulatorEventTest_z7q3q7z11crfr" PurchaseDate="2012-06-04T23:07:24Z" LicenseType="Full" />
    <ProductReceipt Id="6bbf4366-6fb2-8be8-7947-92fd5f683530" ProductId="Product1" PurchaseDate="2012-08-30T23:08:52Z" ExpirationDate="2012-09-02T23:08:49Z" ProductType="Durable" AppId="55428GreenlakeApps.CurrentAppSimulatorEventTest_z7q3q7z11crfr" />
    <Signature xmlns="http://www.w3.org/2000/09/xmldsig#">
        <SignedInfo>
            <CanonicalizationMethod Algorithm="http://www.w3.org/2001/10/xml-exc-c14n#" />
            <SignatureMethod Algorithm="http://www.w3.org/2001/04/xmldsig-more#rsa-sha256" />
            <Reference URI="">
                <Transforms>
                    <Transform Algorithm="http://www.w3.org/2000/09/xmldsig#enveloped-signature" />
                </Transforms>
                <DigestMethod Algorithm="http://www.w3.org/2001/04/xmlenc#sha256" />
                <DigestValue>cdiU06eD8X/w1aGCHeaGCG9w/kWZ8I099rw4mmPpvdU=</DigestValue>
            </Reference>
        </SignedInfo>
        <SignatureValue>SjRIxS/2r2P6ZdgaR9bwUSa6ZItYYFpKLJZrnAa3zkMylbiWjh9oZGGng2p6/gtBHC2dSTZlLbqnysJjl7mQp/A3wKaIkzjyRXv3kxoVaSV0pkqiPt04cIfFTP0JZkE5QD/vYxiWjeyGp1dThEM2RV811sRWvmEs/hHhVxb32e8xCLtpALYx3a9lW51zRJJN0eNdPAvNoiCJlnogAoTToUQLHs72I1dECnSbeNPXiG7klpy5boKKMCZfnVXXkneWvVFtAA1h2sB7ll40LEHO4oYN6VzD+uKd76QOgGmsu9iGVyRvvmMtahvtL1/pxoxsTRedhKq6zrzCfT8qfh3C1w==</SignatureValue>
    </Signature>
</Receipt>
```

A product receipt looks like this.

> [!NOTE]
> This example is formatted to help make the XML readable. Real product receipts do not include whitespace between elements.

> [!div class="tabbedCodeSnippets"]
```xml
<Receipt Version="1.0" ReceiptDate="2012-08-30T23:08:52Z" CertificateId="b809e47cd0110a4db043b3f73e83acd917fe1336" ReceiptDeviceId="4e362949-acc3-fe3a-e71b-89893eb4f528">
    <ProductReceipt Id="6bbf4366-6fb2-8be8-7947-92fd5f683530" ProductId="Product1" PurchaseDate="2012-08-30T23:08:52Z" ExpirationDate="2012-09-02T23:08:49Z" ProductType="Durable" AppId="55428GreenlakeApps.CurrentAppSimulatorEventTest_z7q3q7z11crfr" />
    <Signature xmlns="http://www.w3.org/2000/09/xmldsig#">
        <SignedInfo>
            <CanonicalizationMethod Algorithm="http://www.w3.org/2001/10/xml-exc-c14n#" />
            <SignatureMethod Algorithm="http://www.w3.org/2001/04/xmldsig-more#rsa-sha256" />
            <Reference URI="">
                <Transforms>
                    <Transform Algorithm="http://www.w3.org/2000/09/xmldsig#enveloped-signature" />
                </Transforms>
                <DigestMethod Algorithm="http://www.w3.org/2001/04/xmlenc#sha256" />
                <DigestValue>Uvi8jkTYd3HtpMmAMpOm94fLeqmcQ2KCrV1XmSuY1xI=</DigestValue>
            </Reference>
        </SignedInfo>
        <SignatureValue>TT5fDET1X9nBk9/yKEJAjVASKjall3gw8u9N5Uizx4/Le9RtJtv+E9XSMjrOXK/TDicidIPLBjTbcZylYZdGPkMvAIc3/1mdLMZYJc+EXG9IsE9L74LmJ0OqGH5WjGK/UexAXxVBWDtBbDI2JLOaBevYsyy+4hLOcTXDSUA4tXwPa2Bi+BRoUTdYE2mFW7ytOJNEs3jTiHrCK6JRvTyU9lGkNDMNx9loIr+mRks+BSf70KxPtE9XCpCvXyWa/Q1JaIyZI7llCH45Dn4SKFn6L/JBw8G8xSTrZ3sBYBKOnUDbSCfc8ucQX97EyivSPURvTyImmjpsXDm2LBaEgAMADg==</SignatureValue>
    </Signature>
</Receipt>
```

You can use either of these receipt examples to test your validation code. For more information about the contents of the receipt, see the [element and attribute descriptions](#receipt-descriptions).

## Validating a receipt

To validate a receipt's authenticity, you need your back-end system (a web service or something similar) to check the receipt's signature using the public certificate. To get this certificate, use the URL ```https://lic.apps.microsoft.com/licensing/certificateserver/?cid=CertificateId%60%60%60, where ```CertificateId``` is the **CertificateId** value in the receipt.

Here's an example of that validation process. This code runs in a .NET Framework console application that includes a reference to the **System.Security** assembly.

> [!div class="tabbedCodeSnippets"]
:::code language="csharp" source="~/../snippets-windows/windows-uwp/monetize/ReceiptVerificationSample/cs/Program.cs" id="ReceiptVerificationSample":::

<span id="receipt-descriptions" />

## Element and attribute descriptions for a receipt

This section describes the elements and attributes in a receipt.

### Receipt element

The root element of this file is the **Receipt** element, which contains information about app and in-app purchases. This element contains the following child elements.

|  Element  |  Required  |  Quantity  |  Description   |
|-------------|------------|--------|--------|
|  [AppReceipt](#appreceipt)  |    No        |  0 or 1  |  Contains purchase information for the current app.            |
|  [ProductReceipt](#productreceipt)  |     No       |  0 or more    |   Contains information about an in-app purchase for the current app.     |
|  Signature  |      Yes      |  1   |   This element is a standard [XML-DSIG construct](https://www.w3.org/TR/xmldsig-core/). It contains a **SignatureValue** element, which contains the signature you can use to validate the receipt, and a **SignedInfo** element.      |

**Receipt** has the following attributes.

|  Attribute  |  Description   |
|-------------|-------------------|
|  **Version**  |    The version number of the receipt.            |
|  **CertificateId**  |     The certificate thumbprint used to sign the receipt.          |
|  **ReceiptDate**  |    Date the receipt was signed and downloaded.           |  
|  **ReceiptDeviceId**  |   Identifies the device used to request this receipt.         |  |

<span id="appreceipt" />

### AppReceipt element

This element contains purchase information for the current app.

**AppReceipt** has the following attributes.

|  Attribute  |  Description   |
|-------------|-------------------|
|  **Id**  |    Identifies the purchase.           |
|  **AppId**  |     The Package Family Name value that the OS uses for the app.           |
|  **LicenseType**  |    **Full**, if the user purchased the full version of the app. **Trial**, if the user downloaded a trial version of the app.           |  
|  **PurchaseDate**  |    Date when the app was acquired.          |  |

<span id="productreceipt" />

### ProductReceipt element

This element contains information about an in-app purchase for the current app.

**ProductReceipt** has the following attributes.

|  Attribute  |  Description   |
|-------------|-------------------|
|  **Id**  |    Identifies the purchase.           |
|  **AppId**  |     Identifies the app through which the user made the purchase.           |
|  **ProductId**  |     Identifies the product purchased.           |
|  **ProductType**  |    Determines the product type. Currently only supports a value of **Durable**.          |  
|  **PurchaseDate**  |    Date when the purchase occurred.          |  |

 

 
