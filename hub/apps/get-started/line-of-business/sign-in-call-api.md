---
title: Sign in and call a protected enterprise API from a WinUI app
description: Use MSAL.NET with Web Account Manager to sign in users from a WinUI line-of-business app and call a protected enterprise REST API.
ms.topic: how-to
ms.date: 10/13/2026
author: GrantMeStrength
ms.author: jken
---

# Sign in and call a protected enterprise API from a WinUI app

Many line-of-business (LOB) apps need to call an enterprise service that owns access to shared data. Use Microsoft Authentication Library (MSAL) with Web Account Manager (WAM) to sign in the user, acquire an access token, and call the service over HTTPS. Don't embed a database password, client secret, or privileged connection string in a desktop app.

This article shows the client-side pattern for a WinUI 3 app. It links to the Microsoft identity platform for registration and authority details, because those settings are owned by your tenant and service.

For a lower-level WinUI 3 article that uses the Web Account Manager APIs directly and calls Microsoft Graph, see [Use Web Account Manager in a WinUI 3 desktop app](../../develop/security/web-account-manager.md). For non-Microsoft identity providers or general OAuth 2.0 flows, see [Implement OAuth 2.0 in Windows apps](../../develop/security/oauth2.md).

## Architecture

Use this pattern for shared enterprise data:

1. The WinUI app signs in the user with MSAL.NET and the WAM broker.
1. Microsoft Entra ID issues an access token for the enterprise API.
1. The WinUI app sends the token to the API in the `Authorization` header.
1. The API validates the token and enforces authorization near the data.
1. The API returns only the data that the user is allowed to access.

Keep authorization, validation, auditing, and database access in the service. The desktop app can cache data for responsiveness or offline use, but the service remains the security boundary for shared enterprise data.

## Prerequisites

- A WinUI 3 app that targets a Windows-specific .NET target framework.
- An app registration for the desktop client.
- An app registration or exposed scope for the protected API.
- The API scope that the client app is allowed to request, such as `api://00000000-0000-0000-0000-000000000000/Orders.Read`.
- The NuGet packages required by MSAL.NET and broker support.

For identity setup, see [Using MSAL.NET with Web Account Manager (WAM)](/entra/msal/dotnet/acquiring-tokens/desktop-mobile/wam) and [Register an application with the Microsoft identity platform](/entra/identity-platform/quickstart-register-app).

## Add MSAL packages

Add MSAL.NET and broker support to the WinUI project.

```console
dotnet add package Microsoft.Identity.Client
dotnet add package Microsoft.Identity.Client.Broker
```

Use a current supported version of MSAL.NET. WAM broker support requires MSAL.NET 4.52.0 or later.

## Create an authentication service

Centralize token acquisition in a service class instead of scattering sign-in code across pages and view models.

```csharp
using Microsoft.Identity.Client;
using Microsoft.Identity.Client.Broker;

public sealed class AuthenticationService
{
    private static readonly string[] Scopes =
    {
        "api://00000000-0000-0000-0000-000000000000/Orders.Read"
    };

    private readonly IPublicClientApplication _client;

    public AuthenticationService(IntPtr windowHandle)
    {
        BrokerOptions brokerOptions =
            new(BrokerOptions.OperatingSystems.Windows)
            {
                Title = "Contoso Portfolio"
            };

        _client = PublicClientApplicationBuilder
            .Create("00000000-0000-0000-0000-000000000000")
            .WithAuthority("https://login.microsoftonline.com/contoso.onmicrosoft.com")
            .WithDefaultRedirectUri()
            .WithParentActivityOrWindow(windowHandle)
            .WithBroker(brokerOptions)
            .Build();
    }

    public async Task<AuthenticationResult> AcquireTokenAsync()
    {
        IAccount? account = (await _client.GetAccountsAsync()).FirstOrDefault();

        try
        {
            if (account is not null)
            {
                return await _client.AcquireTokenSilent(Scopes, account)
                    .ExecuteAsync();
            }

            return await _client.AcquireTokenSilent(
                    Scopes,
                    PublicClientApplication.OperatingSystemAccount)
                .ExecuteAsync();
        }
        catch (MsalUiRequiredException)
        {
            return await _client.AcquireTokenInteractive(Scopes)
                .ExecuteAsync();
        }
    }
}
```

Replace the client ID, authority, and scope with values from your tenant and API. If your app supports multiple environments or tenants, make those values policy-aware and validate them before sign-in.

## Get the WinUI window handle

WAM needs a parent window handle so it can show account UI correctly. Pass your app's main window handle to the authentication service.

```csharp
using WinRT.Interop;

IntPtr hwnd = WindowNative.GetWindowHandle(App.MainWindow);
AuthenticationService authentication = new(hwnd);
```

For more information, see [Retrieve a window handle](../../develop/ui/retrieve-hwnd.md).

## Call the protected API

Use the access token in an `Authorization` header. Add a correlation ID so the client request can be matched to service logs.

```csharp
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.Identity.Client;

public sealed class OrdersClient
{
    private readonly HttpClient _httpClient;
    private readonly AuthenticationService _authentication;

    public OrdersClient(
        HttpClient httpClient,
        AuthenticationService authentication)
    {
        _httpClient = httpClient;
        _authentication = authentication;
    }

    public async Task<IReadOnlyList<OrderSummary>> GetOrdersAsync(
        CancellationToken cancellationToken)
    {
        AuthenticationResult result =
            await _authentication.AcquireTokenAsync();

        using HttpRequestMessage request = new(
            HttpMethod.Get,
            "https://api.contoso.com/orders");

        request.Headers.Authorization =
            new AuthenticationHeaderValue("Bearer", result.AccessToken);
        request.Headers.Add("x-correlation-id", Guid.NewGuid().ToString("N"));

        using HttpResponseMessage response =
            await _httpClient.SendAsync(request, cancellationToken);

        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            throw new InvalidOperationException(
                "The service rejected the access token. Sign in again or contact support.");
        }

        response.EnsureSuccessStatusCode();

        IReadOnlyList<OrderSummary>? orders =
            await response.Content.ReadFromJsonAsync<IReadOnlyList<OrderSummary>>(
                cancellationToken);

        return orders ?? [];
    }
}
```

Don't log the access token or the full response body. For diagnostic guidance, see [Add diagnostics and audit logging to a WinUI LOB app](diagnostics-audit-logging.md).

## Handle token and service failures

Plan for these cases:

- Silent sign-in isn't available, so the app must show interactive sign-in.
- Conditional Access requires user interaction.
- The user signs in successfully but isn't authorized for the API.
- The service returns `401`, `403`, `429`, or `5xx`.
- The device is offline.
- The user's access changes while cached data exists on the device.

Use cancellation tokens, timeouts, and bounded retries for service calls. Show errors that tell the user whether to retry, sign in again, contact support, or reconnect to the network.

## Cache data carefully

If the app caches enterprise data:

- Cache only the data needed for the workflow.
- Encrypt or avoid caching sensitive records.
- Include the signed-in account and tenant in the cache partition key.
- Clear cached data on sign-out, account switch, role change, or device retirement when required by policy.
- Define conflict behavior for offline changes.

For local data guidance, see [Connect a WinUI app to a database](connect-to-a-database.md).

## Production checklist

Before rollout, confirm that:

- The client app uses public-client authentication, not a client secret.
- The API validates tokens and authorizes each operation.
- The app requests only the scopes it needs.
- The app uses WAM where supported and falls back only according to your organization's policy.
- The app doesn't log tokens, credentials, or full payloads.
- Service calls include a correlation ID.
- Offline and access-revocation behavior is tested.
- The endpoint, tenant, and logging level can be managed by the organization when needed.

## Related content

- [Using MSAL.NET with Web Account Manager (WAM)](/entra/msal/dotnet/acquiring-tokens/desktop-mobile/wam)
- [Security and identity](../../develop/security/index.md)
- [Use Web Account Manager in a WinUI 3 desktop app](../../develop/security/web-account-manager.md)
- [Implement OAuth 2.0 in Windows apps](../../develop/security/oauth2.md)
- [HTTP client](../../develop/networking/httpclient.md)
- [Connect a WinUI app to a database](connect-to-a-database.md)
- [Make a WinUI LOB app policy-aware](policy-aware-settings.md)
