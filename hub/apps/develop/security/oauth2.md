---
title: Implement OAuth 2.0 functionality in Windows apps
description: Learn how to implement OAuth 2.0 functionality in Windows apps using the Windows App SDK's OAuth2Manager.
ms.date: 03/19/2025
ms.topic: concept-article
keywords: windows, winui, winrt, dotnet, security
#customer intent: As a Windows app developer, I want to learn how to implement OAuth 2.0 functionality in my app so that I can securely authenticate users and access protected resources.
---

# Implement OAuth 2.0 functionality in Windows apps

The new [OAuth2Manager](/windows/windows-app-sdk/api/winrt/microsoft.security.authentication.oauth.oauth2manager) in Windows App SDK enables desktop applications such as WinUI 3 to seamlessly perform OAuth 2.0 authorization on Windows. **OAuth2Manager** API intentionally doesn't provide APIs for the implicit request and resource owner password credential because of the security concerns that entails. It's recommended to use the authorization code grant type using Proof Key for Code Exchange (PKCE). For more information, see the [PKCE RFC](https://tools.ietf.org/html/rfc7636).

## OAuth background

The current WinRT [WebAuthenticationBroker](/uwp/api/windows.security.authentication.web.webauthenticationbroker), primarily designed for UWP apps, presents several challenges when used in desktop apps. Key issues include the dependency on [ApplicationView](/uwp/api/windows.ui.viewmanagement.applicationview), which isn't compatible with desktop app frameworks. As a result, developers are forced to resort to workarounds involving interop interfaces and additional code to implement OAuth 2.0 functionality into WinUI 3 and other desktop apps.

## OAuth2Manager API in Windows App SDK

The OAuth2Manager API for Windows App SDK aims to provide a streamlined solution that meets the expectations of developers. It offers seamless OAuth 2.0 capabilities with full feature parity across all Windows platforms supported by Windows App SDK. The new API eliminates the need for cumbersome workarounds and simplifies the process of incorporating OAuth 2.0 functionality into desktop apps.

The OAuth2Manager is different than the existing WinRT [WebAuthenticationBroker](/uwp/api/windows.security.authentication.web.webauthenticationbroker). It follows OAuth best practices more closely - e.g. using the user's default browser. The best practices for the API are taken from the IETF (Internet Engineering Task Force) OAuth 2.0 Authorization Framework [RFC 6749](https://tools.ietf.org/html/rfc6749), PKCE [RFC 7636](https://tools.ietf.org/html/rfc7636), and OAuth 2.0 for Native Apps [RFC 8252](https://tools.ietf.org/html/rfc8252).

## Perform OAuth 2.0 examples

### Authorization code request

The following example demonstrates how to perform an authorization code request using the OAuth2Manager API in Windows App SDK:

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

The following example demonstrates how to exchange an authorization code for an access token using the OAuth2Manager API in Windows App SDK:

# [C++](#tab/cpp)

```cpp
AuthResponse authResponse = authRequestResult.Response();
TokenRequestParams tokenRequestParams = TokenRequestParams::CreateForAuthorizationCodeRequest(authResponse);
ClientAuthentication clientAuth = ClientAuthentication::CreateForBasicAuthorization(L"my_client_id",
    L"my_client_secret");

TokenRequestResult tokenRequestResult = co_await OAuth2Manager::RequestTokenAsync(
    Uri(L"https://my.server.com/oauth/token"), tokenRequestParams, clientAuth);
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
ClientAuthentication clientAuth = ClientAuthentication.CreateForBasicAuthorization("my_client_id",
    "my_client_secret");

TokenRequestResult tokenRequestResult = await OAuth2Manager.RequestTokenAsync(
    new Uri("https://my.server.com/oauth/token"), tokenRequestParams, clientAuth);

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

### Refresh an access token

The following example shows how to refresh an access token using the OAuth2Manager API in Windows App SDK:

# [C++](#tab/cpp)

```cpp
TokenRequestParams tokenRequestParams = TokenRequestParams::CreateForRefreshToken(refreshToken);
ClientAuthentication clientAuth = ClientAuthentication::CreateForBasicAuthorization(L"my_client_id",
    L"my_client_secret");
TokenRequestResult tokenRequestResult = co_await OAuth2Manager::RequestTokenAsync(
    Uri(L"https://my.server.com/oauth/token"), tokenRequestParams, clientAuth));
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
ClientAuthentication clientAuth = ClientAuthentication.CreateForBasicAuthorization("my_client_id",
    "my_client_secret");
TokenRequestResult tokenRequestResult = await OAuth2Manager.RequestTokenAsync(
    new Uri("https://my.server.com/oauth/token"), tokenRequestParams, clientAuth);
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

### Complete an authorization request

Finally, to complete an authorization request from a protocol activation, your app should handle the [AppInstance.Activated](/windows/windows-app-sdk/api/winrt/microsoft.windows.applifecycle.appinstance.activated) event. This is required when having custom redirect logic. A full example is available on [GitHub](https://github.com/microsoft/WindowsAppSDK-Samples/tree/release/experimental/Samples/OAuth2Manager).

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
