---
title: Web authentication broker
description: This article explains how to connect your Universal Windows Platform (UWP) app to an online identity provider that uses authentication protocols like OpenID or OAuth.
ms.assetid: 05F06961-1768-44A7-B185-BCDB74488F85
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, security
ms.localizationpriority: medium
---
# Web authentication broker




This article explains how to connect your Universal Windows Platform (UWP) app to an online identity provider that uses authentication protocols like OpenID or OAuth. The [**AuthenticateAsync**](/uwp/api/windows.security.authentication.web.webauthenticationbroker.authenticateasync) method sends a request to the online identity provider and gets back an access token that describes the provider resources to which the app has access.

>[!NOTE]
>For a complete, working code sample, clone the [WebAuthenticationBroker repo on GitHub](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/WebAuthenticationBroker).

 

## Register your app with your online provider


You must register your app with the online identity provider to which you want to connect. You can find out how to register your app from the identity provider. After registering, the online provider typically gives you an Id or secret key for your app.

## Build the authentication request URI


The request URI consists of the address where you send the authentication request to your online provider appended with other required information, such as an app ID or secret, a redirect URI where the user is sent after completing authentication, and the expected response type. You can find out from your provider what parameters are required.

The request URI is sent as the *requestUri* parameter of the [**AuthenticateAsync**](/uwp/api/windows.security.authentication.web.webauthenticationbroker.authenticateasync) method. It must be a secure address (it must start with `https://`)

The following example shows how to build the request URI.

```cs
string startURL = "https://<providerendpoint>?client_id=<clientid>&scope=<scopes>&response_type=token";
string endURL = "http://<appendpoint>";

System.Uri startURI = new System.Uri(startURL);
System.Uri endURI = new System.Uri(endURL);
```

## Connect to the online provider


You call the [**AuthenticateAsync**](/uwp/api/windows.security.authentication.web.webauthenticationbroker.authenticateasync) method to connect to the online identity provider and get an access token. The method takes the URI constructed in the previous step as the *requestUri* parameter, and a URI to which you want the user to be redirected as the *callbackUri* parameter.

```cs
string result;

try
{
    var webAuthenticationResult = 
        await Windows.Security.Authentication.Web.WebAuthenticationBroker.AuthenticateAsync( 
        Windows.Security.Authentication.Web.WebAuthenticationOptions.None, 
        startURI, 
        endURI);

    switch (webAuthenticationResult.ResponseStatus)
    {
        case Windows.Security.Authentication.Web.WebAuthenticationStatus.Success:
            // Successful authentication. 
            result = webAuthenticationResult.ResponseData.ToString(); 
            break;
        case Windows.Security.Authentication.Web.WebAuthenticationStatus.ErrorHttp:
            // HTTP error. 
            result = webAuthenticationResult.ResponseErrorDetail.ToString(); 
            break;
        default:
            // Other error.
            result = webAuthenticationResult.ResponseData.ToString(); 
            break;
    } 
}
catch (Exception ex)
{
    // Authentication failed. Handle parameter, SSL/TLS, and Network Unavailable errors here. 
    result = ex.Message;
}
```

>[!WARNING]
>In addition to [**AuthenticateAsync**](/uwp/api/windows.security.authentication.web.webauthenticationbroker.authenticateasync), the [**Windows.Security.Authentication.Web**](/uwp/api/Windows.Security.Authentication.Web) namespace contains an [**AuthenticateAndContinue**](/uwp/api/Windows.Security.Authentication.Web.WebAuthenticationBroker#methods) method. Do not call this method. It is designed for apps targeting Windows Phone 8.1 only and is deprecated starting with Windows 10.

## Connecting with single sign-on (SSO).


By default, Web authentication broker does not allow cookies to persist. Because of this, even if the app user indicates that they want to stay logged in (for example, by selecting a check box in the provider's login dialog), they will have to login each time they want to access resources for that provider. To login with SSO, your online identity provider must have enabled SSO for Web authentication broker, and your app must call the overload of [**AuthenticateAsync**](/uwp/api/windows.security.authentication.web.webauthenticationbroker.authenticateasync) that does not take a *callbackUri* parameter. This will allow persisted cookies to be stored by the web authentication broker, so that future authentication calls by the same app will not require repeated sign-in by the user (the user is effectively "logged in" until the access token expires).

To support SSO, the online provider must allow you to register a redirect URI in the form `ms-app://<appSID>`, where `<appSID>` is the SID for your app. You can find your app's SID from the app developer page for your app, or by calling the [**GetCurrentApplicationCallbackUri**](/uwp/api/windows.security.authentication.web.webauthenticationbroker.getcurrentapplicationcallbackuri) method.

```cs
string result;

try
{
    var webAuthenticationResult = 
        await Windows.Security.Authentication.Web.WebAuthenticationBroker.AuthenticateAsync( 
        Windows.Security.Authentication.Web.WebAuthenticationOptions.None, 
        startURI);

    switch (webAuthenticationResult.ResponseStatus)
    {
        case Windows.Security.Authentication.Web.WebAuthenticationStatus.Success:
            // Successful authentication. 
            result = webAuthenticationResult.ResponseData.ToString(); 
            break;
        case Windows.Security.Authentication.Web.WebAuthenticationStatus.ErrorHttp:
            // HTTP error. 
            result = webAuthenticationResult.ResponseErrorDetail.ToString(); 
            break;
        default:
            // Other error.
            result = webAuthenticationResult.ResponseData.ToString(); 
            break;
    } 
}
catch (Exception ex)
{
    // Authentication failed. Handle parameter, SSL/TLS, and Network Unavailable errors here. 
    result = ex.Message;
}
```

## Debugging


There are several ways to troubleshoot the web authentication broker APIs, including reviewing operational logs and reviewing web requests and responses using Fiddler.

### Operational logs

Often you can determine what is not working by using the operational logs. There is a dedicated event log channel Microsoft-Windows-WebAuth\\Operational that allows website developers to understand how their web pages are being processed by the Web authentication broker. To enable it, launch eventvwr.exe and enable Operational log under the Application and Services\\Microsoft\\Windows\\WebAuth. Also, the Web authentication broker appends a unique string to the user agent string to identify itself on the web server. The string is "MSAuthHost/1.0". Note that the version number may change in the future, so you should not to depend on that version number in your code. An example of the full user agent string, followed by full debugging steps, is as follows.

`User-Agent: Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; Win64; x64; Trident/6.0; MSAuthHost/1.0)`

1.  Enable operational logs.
2.  Run Contoso social application. ![event viewer displaying the webauth operational logs](images/wab-event-viewer-1.png)
3.  The generated logs entries can be used to understand the behavior of Web authentication broker in greater detail. In this case, these can include:
    -   Navigation Start: Logs when the AuthHost is started and contains information about the start and termination URLs.
    -   ![illustrates the details of navigation start](images/wab-event-viewer-2.png)
    -   Navigation Complete: Logs the completion of loading a web page.
    -   Meta Tag: Logs when a meta-tag is encountered including the details.
    -   Navigation Terminate: Navigation terminated by the user.
    -   Navigation Error: AuthHost encounters a navigation error at a URL including HttpStatusCode.
    -   Navigation End: Terminating URL is encountered.

### Fiddler

The Fiddler web debugger can be used with apps. For more information, see [Fiddler documentation](https://docs.telerik.com/fiddler/configure-fiddler/tasks/configurefiddler)

1.  Since the AuthHost runs in its own app container, to give it the private network capability you must set a registry key: Windows Registry Editor Version 5.00

    **HKEY\_LOCAL\_MACHINE**\\**SOFTWARE**\\**Microsoft**\\**Windows NT**\\**CurrentVersion**\\**Image File Execution Options**\\**authhost.exe**\\**EnablePrivateNetwork** = 00000001

    If you do not have this registry key, you can create it in a Command Prompt with administrator privileges.

    ```cmd 
    REG ADD "HKLM\Software\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\authhost.exe" /v EnablePrivateNetwork /t REG_DWORD /d 1 /f
    ```

2.  Add a rule for the AuthHost as this is what is generating the outbound traffic.
    ```syntax
    CheckNetIsolation.exe LoopbackExempt -a -n=microsoft.windows.authhost.a.p_8wekyb3d8bbwe
    CheckNetIsolation.exe LoopbackExempt -a -n=microsoft.windows.authhost.sso.p_8wekyb3d8bbwe
    CheckNetIsolation.exe LoopbackExempt -a -n=microsoft.windows.authhost.sso.c_8wekyb3d8bbwe
    D:\Windows\System32>CheckNetIsolation.exe LoopbackExempt -s
    List Loopback Exempted AppContainers
    [1] -----------------------------------------------------------------
        Name: microsoft.windows.authhost.sso.c_8wekyb3d8bbwe
        SID:  S-1-15-2-1973105767-3975693666-32999980-3747492175-1074076486-3102532000-500629349
    [2] -----------------------------------------------------------------
        Name: microsoft.windows.authhost.sso.p_8wekyb3d8bbwe
        SID:  S-1-15-2-166260-4150837609-3669066492-3071230600-3743290616-3683681078-2492089544
    [3] -----------------------------------------------------------------
        Name: microsoft.windows.authhost.a.p_8wekyb3d8bbwe
        SID:  S-1-15-2-3506084497-1208594716-3384433646-2514033508-1838198150-1980605558-3480344935
    ```

3.  Add a firewall rule for incoming traffic to Fiddler.
