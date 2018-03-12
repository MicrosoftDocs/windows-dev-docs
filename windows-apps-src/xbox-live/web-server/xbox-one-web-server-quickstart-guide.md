---
title: Xbox One Web Server QuickStart Guide
author: KevinAsgari
description: Learn how to set up an Xbox One Web Server.
ms.assetid: 2f6831ab-2dea-4f21-bb32-39cb4de4799c
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, web server
ms.localizationpriority: low
---

# Xbox One Web Server QuickStart Guide

> [!NOTE] 
> This topic is an advanced topic, and requires access to permission restricted sites which are only available to managed partners and ID@Xbox members.

A step-by-step guide for setting up a simple web server for Xbox One development.

-   [Introduction](#introduction)
-   [Server setup](#server-setup)
-   [Building the SimpleAuthService sample website for deployment](#build-sample-website)
-   [Configuring IIS and running the sample website](#configure-sample-website)
-   [Verifying Relying Party and XSTS token setup](#verify-tokens)
-   [Enabling HTTPS for your web service](#enable-https)
-   [FAQs and troubleshooting](#faqs-and-troubleshooting)


## Introduction

Xbox One and the Xbox Live platform rely heavily on the use of RESTful HTTPS communication. And on Xbox One, developers can set up their game servers over HTTPS rather than an Xbox Live Server Platform (XLSP) connection, which was originally required on Xbox 360. This extends titles' ability to provide flexible game services that are quick and reliable.

In this QuickStart guide, you will learn how to set up a working HTTP/HTTPS web service for your Xbox One title by using the Simple Web Server Sample ("SimpleAuthService") available on Game Developer Network (GDN). The guide walks you through setting up a Windows server running the sample web service so you can start exploring the use of Xbox Secure Token Service (XSTS) tokens and HTTPS for communication between your Xbox One title and your servers. It also provides the steps to quickly and easily set up a new virtual machine for your server through Microsoft Azure.


## Server setup

The Relying Party SDK and samples provided on GDN for running an Xbox One web service will work with Windows Server 2008 R2 or later. The instructions that follow are tailored to setting up a server running Windows Server 2012 R2. The required setup should be essentially the same across versions, but where to access some of the server setup features might be slightly different. You can use a physical machine equipped with Windows Server software as your server, or you can set up a virtual machine with Azure.

-   [Azure virtual machine setup](#azure-virtual-machine-setup)
-   [Server roles and features setup](#server-roles-and-features-setup)
-   [Certificate installation](#certificate-installation)


### Azure virtual machine setup

If you have access to Azure, setting up your virtual server for development and testing is straightforward and quick. Simply follow these steps:

1. Log on with your account to the [Azure Portal](https://manage.windowsazure.com/).

1. Select **Virtual Machines** and click **+NEW**.

1. Select **QUICK CREATE**.

1. Enter a DNS name for your service. With ".cloudapp.net" appended to it, this will be the domain name that you use to make HTTP calls to the server.

1. Select Windows Server 2012 R2 and Size setting A1 (you can choose others, but A1 works fine for the sample).

1. Enter a username and password that you will use for a Remote Desktop Connection (RDC) into the virtual machine; also select which region you want the server to run in (preferably closer to your physical location).

1. To finish, click **CREATE A VIRTUAL MACHINE**.

1. Wait a few moments for provisioning to complete, and then click the machine's name to open the Azure configuration settings.

1. Select the **ENDPOINTS** tab.

1. Click **+ADD** and select **ADD A STAND-ALONE ENDPOINT**.

1. Select **HTTP (TCP port 80)** from the **NAME** drop-down list and click **OK** (or the checkmark button).

| Note                                                                                                                |
|----------------------------------------------------------------------------------------------------------------------------------|
| Do not check the Enable Direct Server Return option, because doing so could actually block incoming traffic to your web service. |

1. After you have added the HTTP endpoint, add another endpoint for HTTPS:
 1.  Select the **ENDPOINTS** tab.
 1.  Click **+ADD** and select **ADD A STAND-ALONE ENDPOINT**.
 1.  Select **HTTPS (TCP port 443)** from the **NAME** drop-down list and click **OK** (or the checkmark button).

1. Go to the **DASHBOARD** tab, make sure that the virtual machine is started, and select **CONNECT** to open the .rdp file and use RDC to connect to the machine.
1. Log in to the machine with the username and password you set up in Step 6.

| Note                                                                                                     |
|-----------------------------------------------------------------------------------------------------------------------|
| If you are on a domain-joined computer, you might need to use "\\UserNameYouChose" to log in to the machine directly. |


### Server roles and features setup

After you have your Azure virtual machine running or you have installed Windows Server on your physical machine, you need to turn the machine into a web server running Internet Information Services (IIS). To do this you need to add roles to the server:

1.  Open the Server Manager from the **Start** menu.
2.  Click **Manage** and select **Add Roles and Features**.
3.  Select **Role-Based** or **Feature-Based** installation from the options and click **Next**.
4.  Highlight your server from the Server Pool list and click **Next**.
5.  From the Server Roles list, select Application Server and Web Server (IIS) and then click Next.
6.  On the **Features** list, select **ASP.NET 4.5** under **.NET Framework 4.5 Features** and **HTTP Activation** under **WCF Services** in the same group.
7.  Click **Next** until you are at the Application Server Role Services screen.
8.  Check **Web Server (IIS) Support** and **Add Features** on the window that pops up.
9.  Click **Next** until you are at the **Web Server Role (IIS) Services** screen.
10. Leave all the default values checked and click **Next**.
11. Review the list of items that will be installed and then click **Install**.
12. Wait until the installation of all the software and services is complete.


### Certificate installation

You will need to install the following certificates to enable the Xbox One console to make web service calls to your server with Xbox Secure Token Service (XSTS) tokens:

-   [XSTS Signing certificate](https://developer.xboxlive.com/_layouts/xna/download.ashx?file=xsts.auth.xboxlive.com.cer.zip&folder=platform/RelyingParty)
    -   Download the certificate.
    -   Install the certificate to the "Local Computer/Personal" and "Local Computer/Trusted People" certificate stores.

| Important                                                                                                                                                                                                                                                                                                                                                   |
|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Directly pasting a certificate thumbprint into the web.config file from the MMC can add an extra invisible Unicode character to the thumbprint. For more information about this issue and how to fix it, see [Certificate thumbprint displayed in MMC certificate snap-in has extra invisible unicode character](http://support.microsoft.com/kb/2023835?wa=wsignin1.0). |

-   Your service's Relying Party certificate
    -   The Relying Party certificate's public key is uploaded through XDP. In XDP, navigate to your sandbox, click **Manage**, and then click **Web Services**. On the **Web Services** page, use an existing Web service or create a new one. Once you have created an endpoint for the Web service, you must add a new token definition and then upload the key. For more information about setting up Web services in XDP, see the [XDP Documentation](https://developer.xboxlive.com/en-us/xdp/documentation/xdphelp/Pages/AboutWebServiceManagement_SS_xdpdocs.aspx).
    -   Install the certificate to the "Local Computer/Personal" certificate store.

Additionally, if your web service will be talking directly to the Xbox Live service on behalf of the user (delegated authentication), you will need the following certificate:

### Business Partner certificate

You or your publisher create this certificate by following the steps outlined on the Business Partner Certificate tab of the Xbox One Service Config Workbook. The workbook also includes guidance regarding the information you'll have to provide to Microsoft about the certificate after you've created it.
-   Install the certificate to the "Local Computer/Personal" certificate store.

| Note                                                                                                                                                                                                                                |
|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| If you do not already have a Relying Party or Business Partner certificate, follow the instructions in the Service Config Workbook and contact your developer account manager to make arrangements for submitting the certificates to Microsoft. |

The easiest way to ensure that the certificates are installed in the right place and give the proper permissions to the private keys is through the Microsoft Management Console (mmc.exe):

1.  Open mmc.exe from the Start menu.
2.  From the **File** menu, select **Add/Remove Snap-in**.
3.  Select **Certificates** and click **Add**.
4.  Select **Computer Account** and click **Next**.
5.  Ensure that Local computer is selected and click **Finish**.
6.  Click **OK**.
7.  Expand the **Certificates (Local Computer)** list.
8.  Open the folder or certificate store where you want to import the certificate.
9.  Select **Action**-&gt;**All Tasks**-&gt;**Import**.
10. Follow the Certificate Import Wizard by specifying the .pfx or .cer file for the certificate.

| Note |
|------|
| .cer is the file type by default in the file picker. To install your Relying Party or Business Partner certificates, you might need to change the view to **All Files** (\*.\*). |

Also, your Relying Party and Business Partner certificates will have private keys that the IIS service needs to read in order to handle XSTS tokens. To give the IIS service access to the private keys, do the following from within mmc.exe:

1.  Right-click the name of the certificate.
2.  Select **All Tasks**-&gt;**Manage Private Keys**.
3.  Click **Add**.
4.  Add the \[Local Computer Name\]\\SERVICE account and click **OK**.
5.  Ensure that the SERVICE account has Read access to the private key.
6.  Click **OK**.


## Building the SimpleAuthService sample website for deployment <a name="build-sample-website">

After you have configured your server and verified that IIS is set up properly, you can set up the sample and compile it to run on your web server.

1.  Ensure that you have Visual Studio 2012 installed.
2.  Download the latest Xbox One Simple Web Server Sample ("SimpleAuthService") from the [XDK software downloads](https://developer.xboxlive.com/en-us/platform/development/downloads/Pages/home.aspx) page on GDN.
3.  Download the Xbox One Relying Party SDK from the software downloads page on GDN.
4.  Open the SimpleAuthService project in Visual Studio.
5.  Install the [IdentityModel for handling JSON Web Tokens (JWT)](https://www.nuget.org/packages/Microsoft.IdentityModel.Tokens.JWT).
6.  Right-click the SimpleAuthService project and select **Add Reference**.
7.  Select **Browse** and target the Microsoft.XboxLive.Auth.dll that was unpacked from the Relying Party SDK.
8.  Select **OK**.
9.  Open the Web.config file and find the audienceUris node.
10. Change <http://Your_Relying_Party.com/> to reflect your publisher or service's Relying Party name.
11. For testing to make sure the website is active, enable directory browsing temporarily in the Web.config file by changing the directoryBrowse setting to true. The default value for this setting is false:
12. Compile the solution to verify that it succeeds. If it does, you are now ready to publish the service.
13. Right-click the **SimpleAuthService** project and select **Publish**.
14. From the drop-down list, select &lt;New Profile...&gt;.
15. Name your publish profile **SimpleAuthService** and click **Next**.
16. On the Connection page, select **File System** from the drop-down list.
17. Set the target location to a new folder on your development computer and click **Next**.
18. Select **Debug** for the configuration so that you can debug the site later if needed, then click **Publish**.
19. Copy the resulting files in your output folder from Step 16 to the C:\\SampleService\\ folder on your server.


## Configuring IIS and running the sample website <a name="configure-sample-website">

Before the console will be able to talk to your web server, you'll have to configure your service in IIS. For simplicity, first start with running the SimpleAuthService sample. The following steps will help you set up an HTTP/HTTPS web service through IIS for that sample.

1.  Open the Internet Information Services (IIS) Manager.
2.  Expand your server's list and right-click **Application Pools**; then select **Add New Pool**.
3.  Name the pool "SampleServicePool" and set the pool to .NET Framework 4.0.
4.  Expand the **Sites** list and select **Default Web Site**.
5.  Under **Manage Website**, select **Stop**.
6.  Now right-click **Sites** and select **Add Website...**
7.  Name the site "SampleService," set the Application Pool to SampleServicePool, and set the physical path to C:\\SampleService\\.
8.  Leave the default binding to port 80, click **OK**, then click **Yes** on the binding warning.
9.  Run the following command from an Administrator Command Prompt:

           %windir%\Microsoft.Net\Framework64\v4.0.30319\aspnet_regiis.exe -ir

10. Under Sites, make sure that the default web site is stopped and restart your web service.

At this point your server should be set up for HTTP traffic. Before moving on to enable HTTPS—which is required for all web-based communication in any Xbox One retail environment—it is important for you to ensure that the system is working with only HTTP enabled on the site you configured in IIS. This will help you better troubleshoot issues that might arise when enabling HTTPS, described later in this document.

To verify that everything is working so far, try to access `http://localhost/` and confirm that you see the directory information for your deployed SampleService website. If your server is accessible through the public Internet at this point, you will want to turn off directory browsing by updating the directoryBrowse setting back to false in your Web.config file. This will prevent unauthorized access and discovery of your service:

      <directoryBrowse enabled=?false?>

Now that you know that IIS is set up, try calling one of the services from the server and verify that you get a .json response back:

      http://localhost/RESTService.svc/messageoftheday


## Verifying Relying Party and XSTS token setup <a name="verify-tokens">

Now that you are able to run the deployed service through HTTP on your server, the next step is to make a call from an Xbox One console with an XSTS token and verify that your certificates are installed properly.

Download the Web Services code sample for Xbox One from the [samples page on GDN](https://developer.xboxlive.com/en-us/platform/development/education/Pages/Samples.aspx) and extract it to your development computer with the Xbox One XDK installed.
Open the \\live\\WebServices\\WebServices110.sln file.
Open the WebServices.cpp file.
Update the MYSERVERNAME definition to be the domain of your test server (for instance, yourserver.cloudapp.net).
Open Package.Appxmanifest.
Update the TitleID and PrimaryServiceConfigID values to match those of your development title.
Make sure that your Xbox One development console (devkit) is in the sandbox where your Relying Party certificate is set up.
Sign in with one of your developer accounts on your devkit.
Open the NSAL.json file and update it to target your test server host and relying party (if you have not already set up your Network Security Authorization List (NSAL) settings for your Relying Party certificate).

For more information on configuring the NSAL.json file, review this post on the Entertainment Developer Forums: How to configure the NSAL.json file.

1.  Run the SetNSAL.bat command to copy the needed NSAL files to your devkit.

Compile and run the solution.
Press the **Y** button to make a call to the People Service and verify that the client sample is running properly.
Press the **A** and **X** buttons to make calls to your test web service.


## Enabling HTTPS for your web service <a name="enable-https">

-   [Export the self-signed certificate's public key](#export-key)
-   [Create an HTTPS binding for your web service in IIS](#create-https-binding)
-   [Install the self-signed certificate on your Xbox One console](#install-certificate)

After you have verified that the server is set up and working end to end with an Xbox One console over HTTP, update your server setup to accept HTTPS traffic. As previously noted, HTTPS is required for all web-based traffic from the console to any retail environment on Xbox One.

If you have set up your server through Azure, your server will already have a certificate ready. To verify this, do the following:

1.  Open Internet Information Services (IIS) Manager from the Start menu.
2.  Select your server name from the Connections page.
3.  Open **Server Certificates**.
4.  Verify that there is a certificate with your server's domain (for instance, server.cloudapp.net) in the list.
5.  Select the certificate and click **View...**
6.  Select the Certification Path and verify that it has your server's DNS name (for instance, server.cloudapp.net).

If you are not using Azure or you need to create a certificate for SSL communication, you can create a self-signed certificate by following the instructions in this article:

[How to: Create Temporary Certificates for Use During Development](http://msdn.microsoft.com/en-us/library/ms733813(v=vs.110).aspx)

Make sure when creating your certificate that you use the full DNS name of your server (such as server.contoso.com) for the certificate name. Otherwise, the certificate name and the server name will not match exactly, and the console will not trust the certificate. When the certificate is ready, install it in the machine's Personal certificate store as you did with the other certificates in the server setup.


### Export the self-signed certificate's public key <a name="export-key">

1.  Open mmc.exe from the Start menu.
2.  From the **File** menu, select **Add/Remove Snap-in**.
3.  Select **Certificates** and click **Add**.
4.  Select **Computer Account** and click **Next**.
5.  Ensure that Local computer is selected and click **Finish**.
6.  Click **OK**.
7.  Expand the Certificates (Local Computer)\\Personal\\Certificates list.
8.  Right-click the name of the self-signed SSL certificate that you created earlier (or that was already created on your machine).
9.  Select **All Tasks** and **Export...**
10. Click **Next** on the Certificate Export Wizard.
11. Select **No, do not export the private key** and click **Next**.
12. Select **Base-64 encoded X.509 (.CER)** and click **Next**.
13. Name the certificate file with a recognizable name for your server, click **Next**, and then click **Finish**.
14. Copy the resulting .cer file to your development computer.


### Create an HTTPS binding for your web service in IIS <a name="create-https-binding">

1.  Open your service website in IIS Manager.
2.  Click **Bindings**.
3.  Select **Add**.
4.  Select **https** from the drop-down list.
5.  Select your self-signed certificate from the SSL certificate drop-down list.
6.  Click **OK**.

You should now be able to try https://localhost, which will give you a warning in Internet Explorer that there is a problem with the website's certificate. This is only because the certificate is self-signed and does not chain up to a trusted certification authority (CA). Before you ship your title, you will have to set up your web service with a real certificate for your domain that is requested through a trusted CA.

If you try to go to https://localhost/RESTService.svc/messageoftheday you will get an error message. The reason is that you will also have to update the Web.config file for the service to support HTTPS requests. To do so, uncomment the following setting, save the Web.config file, and try the link again:

      <!-- Uncomment this block to enable HTTPS

      <endpoint behaviorConfiguration="webHttpBehavior" binding="webHttpBinding"
      bindingConfiguration="webHttpsBindingConfig" contract="SimpleAuthService.IRESTService" /> -->

Now that you are able to reach your service over HTTPS through an Internet browser, the last step is getting your devkit to make the service call over HTTPS. If your server is using an SSL certificate that chains up to a trusted CA, then you should be able to just call the HTTPS version of your web service without any additional steps. Alternatively, if you created a self-signed certificate for SSL communication as outlined above, you will need to get the certificate and install it on your console; otherwise the console will not trust the server it is trying to talk to over HTTPS. And because the console wipes all temporary certificates when it is shut down, you'll have to reinstall the certificate every time you restart the console.


### Install the self-signed certificate on your Xbox One console <a name="install-certificate">

1.  Open up an Xbox One XDK Command Prompt and navigate to where you saved your server's SSL certificate.
2.  Run the following commands (using a .bat file is an easy way to quickly reinstall the certificate):

        xbcp "%DurangoXDK%\bin\setproxy.exe" xd:\
        xbcp "%DurangoXDK%\bin\certmgr.exe" xd:\
        xbcp [YourSSLCert].cer xd:\
        xbrun /o d:\certmgr.exe -add -all d:\[YourSSLCert].cer -s -r localmachine root

The final step is to update your NSAL.json file to include the HTTPS definition for your service by simply copying the current HTTP definition and updating the protocol value to *HTTPS*.


## FAQs and troubleshooting

 **When the client-side sample tries to call our service it is getting errors from GetTokenAndSignatureAsync(). How can I resolve these errors?**   
Most of the NSAL or token-related errors from the console start with 0x87DD\*. [This Forum post on XboxLive.com](https://forums.xboxlive.com/AnswerPage.aspx?qid=10ef67c1-db3a-4f29-b4bf-3eacd77313cb&tgt=1) provides a list of the most common errors and how to resolve them.

 **When trying to access a web service URL I'm getting HTTP error 404.3. What is causing this and how do I fix it?**   
When initially trying to set up your server with the sample, a 404 error usually indicates that there is a missing component to .NET 3.5, 4.5, or ASP.NET. Make sure that the following Service roles (Features) are enabled in the Add Roles and Features Wizard:

-   HTTP Activation under .NET 3.5 (Windows Server 2008)
-   ASP.NET 4.5 under .NET 4.5
-   HTTP Activation under .NET 4.5 / WCF Services

If you are running on Windows Server 2008 R2, run the following command from an Administrator Command Prompt:

      %windir%\"Microsoft.NET\Framework\v3.0\Windows Communication Foundation\ servicemodelreg" –i

 **Windows Server 2008 R2 – Config Error: The configuration section "system.serviceModel" cannot be read because it is missing a section declaration.**   
Install .NET 3.5. See [This article on MSDN](http://blogs.msdn.com/b/sqlblog/archive/2010/01/08/how-to-install-net-framework-3-5-sp1-on-windows-server-2008-r2-environments.aspx) for more information.

 **Windows Server 2008 R2 – HTTP Error 500.21: Handler "svc-Integrated" has a bad module "ManagedPipelineHandler" in its module list.**   
From an Administrator Command Prompt, run:

    %windir%\Microsoft.NET\Framework\v4.0.21006\aspnet_regiis.exe –i

 **What is HTTP Error 500.19 and 0x80070021?**   
The handlers section on your server is currently locked. Running the following command from an Administrator Command Prompt should fix it:

    %windir%\system32\inetsrv\appcmd.exe unlock config "Default Web Site/exchange" -section:handlers -commitpath:apphost

 **I get HRESULT Error 0x800c0019 when trying to call my web service over HTTPS from the console. Why?**   
This means that the SSL certificate you used for HTTPS traffic is not trusted by the console or has been misconfigured on the server. First, make sure that the certificate you created and used for SSL on your server has the full domain name of your server in the Issued to: value when you double-click on the exported .cer file. Example: Issued to: Server.Contoso.com. Next, make sure that when you exported the .cer file you selected the Base-64 option. Finally, you can look in IIS to make sure that the HTTPS port (443) is using the certificate that you exported and not another one. Also make sure to reinstall the certificate on your devkit.
