---
author: normesta
Description: Distribute your UWP app converted with the Desktop to UWP Bridge
Search.Product: eADQiWindows 10XVcnh
title: Desktop to UWP Bridge Distribute
ms.author: normesta
ms.date: 03/09/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.assetid: edff3787-cecb-4054-9a2d-1fbefa79efc4
---

# Distribute a Windows Desktop Bridge app

There are three main ways to deploy your converted app: the Windows Store, sideloading, and loose file registration.  

## Windows Store

The Windows Store is the most convenient way for customers to get your app. To get started, fill out the form at [Bring your existing apps and games to the Windows Store with the Desktop Bridge](https://developer.microsoft.com/windows/projects/campaigns/desktop-bridge). Microsoft will contact you to start the onboarding process.

Note that you need to be the developer and/or publisher of the app or game to bring it to the Windows Store. As such, make sure your name and e-mail address match with the website you submit as the URL below, so we can validate you are the developer and/or publisher.

## Sideloading

Sideloading provides an easy means for deploying to across multiple machines. It is especially useful in enterprise / line of business LOB) scenarios where you want finer control over the distribution experience and don't want to get involved with Store certificate.

Before you deploy your via sideload app, you'll need to sign it with a certificate. For information on creating a certificate, see [Sign your Windows app package](https://msdn.microsoft.com/windows/uwp/porting/desktop-to-uwp-run-desktop-app-converter#deploy-your-converted-appx).

Here's how you import a certificate that you created previously. You can import the cert directly with CERTUTIL, or you can install it from an Windows app package that you've signed, like the customer will.

To install cert via CERTUTIL, run the following command from an administrator command prompt:

```cmd
Certutil -addStore TrustedPeople <testcert.cer>
```

To import the cert from the Windows app package like a customer would:

1.	In File Explorer, right click an Windows app package that you've signed with a test cert and choose **Properties** from the context menu.
2.	Click or tap the **Digital Signatures** tab.
3.	Click or tap on the certificate and choose **Details**.
4.	Click or tap **View Certificate**.
5.	Click or tap **Install Certificate**.
6.	In the **Store Location** group, select **Local Machine**.
7.	Click or tap **Next** and **OK** to confirm the UAC dialog.
8.	In the next screen of the Certificate Import Wizard, change the selected option to **Place all certificates in the following store**.
9.	Click or tap **Browse**. In the Select Certificate Store window, scroll down and select **Trusted People** and click or tap **OK**.
10.	Click or tap **Next**. A new screen appears. Click or tap **Finish**.
11.	A confirmation dialog should appear. If so, click **OK**. If a different dialog indicates that there is a problem with the certificate, you may need to do some certificate troubleshooting.

Note: For Windows to trust the certificate, the certificate must be located in either the **Certificates (Local Computer) > Trusted Root Certification Authorities > Certificates** node or the **Certificates (Local Computer) > Trusted People > Certificates** node. Only certificates in these two locations can validate the certificate trust in the context of the local machine. Otherwise, an error message that resembles the following string appears:

```CMD
"Add-AppxPackage : Deployment failed with HRESULT: 0x800B0109, A certificate chain processed,
but terminated in a rootcertificate which is not trusted by the trust provider.
(Exception from HRESULT: 0x800B0109) error 0x800B0109: The root certificate of the signature
in the app package must be trusted."
```

Now that the cert has been trusted, there are 2 ways you can install the package – through the powershell or just double-click on the Windows app package file to install it.  To install via powershell, run the following cmdlet:

```powershell
Add-AppxPackage <MyApp>.appx
```

### Loose file registration

Loose file registration is useful for debugging purposes where the files are laid out on disk in a location you can easily access and update, and does not require signing or a cert.  

To deploy your app during development, run the following PowerShell cmdlet:

```Add-AppxPackage –Register AppxManifest.xml```

To update your app's .exe or .dll files, simply replace the existing files in your package with the new ones, increase the version number in AppxManifest.xml, and then run the above command again.

Note the following:

* Any drive that you install your converted app on to must be formatted to NTFS format.
* A converted app always runs as the interactive user.
