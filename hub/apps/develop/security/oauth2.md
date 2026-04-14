---
title: Implement OAuth 2.0 in Windows Apps
description: Learn how to implement OAuth 2.0 authentication in Windows apps using Windows App SDK's OAuth2Manager API. Secure user authentication with step-by-step examples. Get started now.
ms.date: 10/28/2025
ms.topic: concept-article
keywords: windows, winui, winrt, dotnet, security
#customer intent: As a Windows app developer, I want to learn how to implement OAuth 2.0 in my app so that I can securely authenticate users and access protected resources.
---

# Implement OAuth 2.0 in Windows apps

The [OAuth2Manager](/windows/windows-app-sdk/api/winrt/microsoft.security.authentication.oauth.oauth2manager) in Windows App SDK enables desktop applications such as WinUI 3 to seamlessly perform OAuth 2.0 authorization on Windows. The **OAuth2Manager** API doesn't provide APIs for the implicit request and resource owner password credential because of the security concerns that entails. Use the authorization code grant type with Proof Key for Code Exchange (PKCE). For more information, see the [PKCE RFC](https://tools.ietf.org/html/rfc7636).

> [!NOTE]
> **OAuth2Manager** is designed for general OAuth 2.0 flows with any identity provider (GitHub, Google, custom, etc.) and always uses the system browser for the authorization step. If you specifically want to sign in with **Microsoft accounts or Microsoft Entra ID (work/school) accounts** with **silent SSO** — using the account already signed in to Windows, with no browser prompt — use [MSAL.NET with the Web Account Manager (WAM) broker](/entra/msal/dotnet/acquiring-tokens/desktop-mobile/wam) instead. Web Account Manager also provides Windows Hello integration and conditional access support that OAuth2Manager does not.

## OAuth2Manager API in Windows App SDK

The **OAuth2Manager** API for Windows App SDK provides a streamlined solution that meets the expectations of developers. It offers seamless OAuth 2.0 capabilities with full feature parity across all Windows platforms supported by Windows App SDK. The new API eliminates the need for cumbersome workarounds and simplifies the process of incorporating OAuth 2.0 functionality into desktop apps.

The **OAuth2Manager** is different from the **WebAuthenticationBroker** in WinRT. It follows OAuth 2.0 best practices more closely - for example, by using the user's default browser. The best practices for the API come from the IETF (Internet Engineering Task Force) OAuth 2.0 Authorization Framework [RFC 6749](https://tools.ietf.org/html/rfc6749), PKCE [RFC 7636](https://tools.ietf.org/html/rfc7636), and OAuth 2.0 for Native Apps [RFC 8252](https://tools.ietf.org/html/rfc8252).

## OAuth 2.0 code examples

A full WinUI sample app is available on [GitHub](https://github.com/microsoft/WindowsAppSDK-Samples/tree/release/experimental/Samples/OAuth2Manager). The following sections provide code snippets for the most common OAuth 2.0 flows using the **OAuth2Manager** API.

### Authorization code request

The following example demonstrates how to perform an authorization code request using the **OAuth2Manager** in Windows App SDK:

# [C++](#tab/cpp)

```cpp
// Get the WindowId for the application window
Microsoft::UI::WindowId parentWindowId = this->AppWindow().Id();

AuthRequestParams authRequestParams = AuthRequestParams::CreateForAuthorizationCodeRequest(L"my_client_id",
   Uri(L"my-app:/oauth-callback/"));
authRequestParams.Scope(L"user:email user:birthday");

AuthRequestResult authRequestResult = co_await OAuth2Manager::RequestAuthWithParamsAsync(parentWindowId, 
   Uri(L"https://my.server.com/oauth/authorize"), authRequestParams);
if (AuthResponse authResponse = authRequestResult.Response())
{
   //To obtain the authorization code
   //authResponse.Code();

   //To obtain the access token
   DoTokenExchange(authResponse);
}
else
{
   AuthFailure authFailure = authRequestResult.Failure();
   NotifyFailure(authFailure.Error(), authFailure.ErrorDescription());
}
```

# [C#](#tab/csharp)

```csharp
// Get the WindowId for the application window
Microsoft.UI.WindowId parentWindowId = this.AppWindow.Id;

AuthRequestParams authRequestParams = AuthRequestParams.CreateForAuthorizationCodeRequest("my_client_id",
   new Uri("my-app:/oauth-callback/"));
authRequestParams.Scope = "user:email user:birthday";

AuthRequestResult authRequestResult = await OAuth2Manager.RequestAuthWithParamsAsync(parentWindowId, 
   new Uri("https://my.server.com/oauth/authorize"), authRequestParams);

if (AuthResponse authResponse == authRequestResult.Response)
{
   //To obtain the authorization code
   //authResponse.Code;

   //To obtain the access token
   DoTokenExchange(authResponse);
}
else
{
   AuthFailure authFailure = authRequestResult.Failure;
   NotifyFailure(authFailure.Error, authFailure.ErrorDescription);
}
```

---

### Exchange authorization code for access token

The following example shows how to exchange an authorization code for an access token by using the **OAuth2Manager**.

For **public clients** (like native desktop apps) that use PKCE, don't include a client secret. The PKCE code verifier provides the security instead:

# [C++](#tab/cpp)

```cpp
AuthResponse authResponse = authRequestResult.Response();
TokenRequestParams tokenRequestParams = TokenRequestParams::CreateForAuthorizationCodeRequest(authResponse);

// For public clients using PKCE, do not include ClientAuthentication
TokenRequestResult tokenRequestResult = co_await OAuth2Manager::RequestTokenAsync(
    Uri(L"https://my.server.com/oauth/token"), tokenRequestParams);
if (TokenResponse tokenResponse = tokenRequestResult.Response())
{
    String accessToken = tokenResponse.AccessToken();
    String tokenType = tokenResponse.TokenType();

    // RefreshToken string null/empty when not present
    if (String refreshToken = tokenResponse.RefreshToken(); !refreshToken.empty())
    {
        // ExpiresIn is zero when not present
        DateTime expires = winrt::clock::now();
        if (String expiresIn = tokenResponse.ExpiresIn(); std::stoi(expiresIn) != 0)
        {
            expires += std::chrono::seconds(static_cast<int64_t>(std::stoi(expiresIn)));
        }
        else
        {
            // Assume a duration of one hour
            expires += std::chrono::hours(1);
        }

        //Schedule a refresh of the access token
        myAppState.ScheduleRefreshAt(expires, refreshToken);
    }

    // Use the access token for resources
    DoRequestWithToken(accessToken, tokenType);
}
else
{
    TokenFailure tokenFailure = tokenRequestResult.Failure();
    NotifyFailure(tokenFailure.Error(), tokenFailure.ErrorDescription());
}
```

# [C#](#tab/csharp)

```csharp
AuthResponse authResponse = authRequestResult.Response;
TokenRequestParams tokenRequestParams = TokenRequestParams.CreateForAuthorizationCodeRequest(authResponse);

// For public clients using PKCE, do not include ClientAuthentication
TokenRequestResult tokenRequestResult = await OAuth2Manager.RequestTokenAsync(
    new Uri("https://my.server.com/oauth/token"), tokenRequestParams);

if (TokenResponse tokenResponse == tokenRequestResult.Response)
{
    string accessToken = tokenResponse.AccessToken;
    string tokenType = tokenResponse.TokenType;

    // RefreshToken string null/empty when not present
    if (!string.IsNullOrEmpty(tokenResponse.RefreshToken))
    {
        // ExpiresIn is zero when not present
        DateTime expires = DateTime.Now;
        if (tokenResponse.ExpiresIn != 0)
        {
            expires += TimeSpan.FromSeconds(tokenResponse.ExpiresIn);
        }
        else
        {
            // Assume a duration of one hour
            expires += TimeSpan.FromHours(1);
        }

        //Schedule a refresh of the access token
        myAppState.ScheduleRefreshAt(expires, tokenResponse.RefreshToken);
    }

    // Use the access token for resources
    DoRequestWithToken(accessToken, tokenType);
}
else
{
    TokenFailure tokenFailure = tokenRequestResult.Failure;
    NotifyFailure(tokenFailure.Error, tokenFailure.ErrorDescription);
}
```

---

For **confidential clients** (like web apps or services) that have a client secret, include the `ClientAuthentication` parameter:

# [C++](#tab/cpp)

```cpp
AuthResponse authResponse = authRequestResult.Response();
TokenRequestParams tokenRequestParams = TokenRequestParams::CreateForAuthorizationCodeRequest(authResponse);
ClientAuthentication clientAuth = ClientAuthentication::CreateForBasicAuthorization(L"my_client_id",
    L"my_client_secret");

TokenRequestResult tokenRequestResult = co_await OAuth2Manager::RequestTokenAsync(
    Uri(L"https://my.server.com/oauth/token"), tokenRequestParams, clientAuth);
// Handle the response as shown in the previous example
```

# [C#](#tab/csharp)

```csharp
AuthResponse authResponse = authRequestResult.Response;
TokenRequestParams tokenRequestParams = TokenRequestParams.CreateForAuthorizationCodeRequest(authResponse);
ClientAuthentication clientAuth = ClientAuthentication.CreateForBasicAuthorization("my_client_id",
    "my_client_secret");

TokenRequestResult tokenRequestResult = await OAuth2Manager.RequestTokenAsync(
    new Uri("https://my.server.com/oauth/token"), tokenRequestParams, clientAuth);
// Handle the response as shown in the previous example
```

---

### Refresh an access token

The following example shows how to refresh an access token by using the **OAuth2Manager**'s [RefreshTokenAsync](/windows/windows-app-sdk/api/winrt/microsoft.security.authentication.oauth.oauth2manager.requesttokenasync) method.

For **public clients** that use PKCE, omit the `ClientAuthentication` parameter:

# [C++](#tab/cpp)

```cpp
TokenRequestParams tokenRequestParams = TokenRequestParams::CreateForRefreshToken(refreshToken);

// For public clients using PKCE, do not include ClientAuthentication
TokenRequestResult tokenRequestResult = co_await OAuth2Manager::RequestTokenAsync(
    Uri(L"https://my.server.com/oauth/token"), tokenRequestParams);
if (TokenResponse tokenResponse = tokenRequestResult.Response())
{
    UpdateToken(tokenResponse.AccessToken(), tokenResponse.TokenType(), tokenResponse.ExpiresIn());

    //Store new refresh token if present
    if (String refreshToken = tokenResponse.RefreshToken(); !refreshToken.empty())
    {
        // ExpiresIn is zero when not present
        DateTime expires = winrt::clock::now();
        if (String expiresInStr = tokenResponse.ExpiresIn(); !expiresInStr.empty())
        {
            int expiresIn = std::stoi(expiresInStr);
            if (expiresIn != 0)
            {
                expires += std::chrono::seconds(static_cast<int64_t>(expiresIn));
            }
        }
        else
        {
            // Assume a duration of one hour
            expires += std::chrono::hours(1);
        }

        //Schedule a refresh of the access token
        myAppState.ScheduleRefreshAt(expires, refreshToken);
    }
}
else
{
    TokenFailure tokenFailure = tokenRequestResult.Failure();
    NotifyFailure(tokenFailure.Error(), tokenFailure.ErrorDescription());
}
```

# [C#](#tab/csharp)

```csharp
TokenRequestParams tokenRequestParams = TokenRequestParams.CreateForRefreshToken(refreshToken);

// For public clients using PKCE, do not include ClientAuthentication
TokenRequestResult tokenRequestResult = await OAuth2Manager.RequestTokenAsync(
    new Uri("https://my.server.com/oauth/token"), tokenRequestParams);
if (TokenResponse tokenResponse == tokenRequestResult.Response)
{
    UpdateToken(tokenResponse.AccessToken, tokenResponse.TokenType, tokenResponse.ExpiresIn);

    //Store new refresh token if present
    if (!string.IsNullOrEmpty(tokenResponse.RefreshToken))
    {
        // ExpiresIn is zero when not present
        DateTime expires = DateTime.Now;
        if (tokenResponse.ExpiresIn != 0)
        {
            expires += TimeSpan.FromSeconds(tokenResponse.ExpiresIn);
        }
        else
        {
            // Assume a duration of one hour
            expires += TimeSpan.FromHours(1);
        }

        //Schedule a refresh of the access token
        myAppState.ScheduleRefreshAt(expires, tokenResponse.RefreshToken);
    }
}
else
{
    TokenFailure tokenFailure = tokenRequestResult.Failure;
    NotifyFailure(tokenFailure.Error, tokenFailure.ErrorDescription);
}
```

---

For **confidential clients** that have a client secret, include the `ClientAuthentication` parameter:

# [C++](#tab/cpp)

```cpp
TokenRequestParams tokenRequestParams = TokenRequestParams::CreateForRefreshToken(refreshToken);
ClientAuthentication clientAuth = ClientAuthentication::CreateForBasicAuthorization(L"my_client_id",
    L"my_client_secret");
TokenRequestResult tokenRequestResult = co_await OAuth2Manager::RequestTokenAsync(
    Uri(L"https://my.server.com/oauth/token"), tokenRequestParams, clientAuth);
// Handle the response as shown in the previous example
```

# [C#](#tab/csharp)

```csharp
TokenRequestParams tokenRequestParams = TokenRequestParams.CreateForRefreshToken(refreshToken);
ClientAuthentication clientAuth = ClientAuthentication.CreateForBasicAuthorization("my_client_id",
    "my_client_secret");
TokenRequestResult tokenRequestResult = await OAuth2Manager.RequestTokenAsync(
    new Uri("https://my.server.com/oauth/token"), tokenRequestParams, clientAuth);
// Handle the response as shown in the previous example
```

---

### Complete an authorization request

To complete an authorization request from a protocol activation, your app should handle the [AppInstance.Activated](/windows/windows-app-sdk/api/winrt/microsoft.windows.applifecycle.appinstance.activated) event. This event is required when your app has custom redirect logic. A full example is available on [GitHub](https://github.com/microsoft/WindowsAppSDK-Samples/tree/release/experimental/Samples/OAuth2Manager).

Use the following code:

# [C++](#tab/cpp)

```cpp
void App::OnActivated(const IActivatedEventArgs& args)
{
    if (args.Kind() == ActivationKind::Protocol)
    {
        auto protocolArgs = args.as<ProtocolActivatedEventArgs>();
        if (OAuth2Manager::CompleteAuthRequest(protocolArgs.Uri()))
        {
            TerminateCurrentProcess();
        }

        DisplayUnhandledMessageToUser();
    }
}
```

# [C#](#tab/csharp)

```csharp
protected override void OnActivated(IActivatedEventArgs args)
{
    if (args.Kind == ActivationKind.Protocol)
    {
        ProtocolActivatedEventArgs protocolArgs = args as ProtocolActivatedEventArgs;
        if (OAuth2Manager.CompleteAuthRequest(protocolArgs.Uri))
        {
            TerminateCurrentProcess();
        }

        DisplayUnhandledMessageToUser();
    }
}
```

---

## Related content

- [WebAuthenticationBroker](/uwp/api/windows.security.authentication.web.webauthenticationbroker)
- [OAuth2Manager](/windows/windows-app-sdk/api/winrt/microsoft.security.authentication.oauth.oauth2manager)
- [PKCE RFC 7636](https://tools.ietf.org/html/rfc7636)
