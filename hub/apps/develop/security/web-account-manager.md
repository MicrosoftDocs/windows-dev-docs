---
title: Use Web Account Manager in a WinUI 3 desktop app
description: Learn how to use Web Account Manager (WAM) APIs with HWND interop to connect your WinUI 3 desktop app to Microsoft identity providers and call Microsoft Graph.
author: GrantMeStrength
ms.author: jken
ms.topic: how-to
ms.date: 07/15/2026
---

# Use Web Account Manager in a WinUI 3 desktop app

This article describes how to use [`AccountsSettingsPaneInterop`](/windows/win32/api/accountssettingspaneinterop/nn-accountssettingspaneinterop-iaccountssettingspaneinterop) to connect your WinUI 3 desktop app to identity providers like Microsoft or Microsoft Entra ID, using the Windows Web Account Manager (WAM) APIs. You learn how to request permission to use a user's account, obtain an access token, and use it to perform basic operations like retrieving profile data from Microsoft Graph.

> [!TIP]
> **For new projects, use [MSAL.NET with the WAM broker](/entra/msal/dotnet/acquiring-tokens/desktop-mobile/wam) as your primary authentication library.** MSAL handles token caching, refresh, scope negotiation, and WAM integration automatically via `.WithBroker()` and `.WithParentActivityOrWindow(hwnd)`. See the [WinUI 3 desktop authentication sample](https://github.com/Azure-Samples/ms-identity-docs-code-dotnet/tree/main/desktop-winui) for a complete working example. The patterns in this article are useful when you need the `AccountsSettingsPane` flyout UI, direct WAM access, or want to understand how the underlying broker works.

## Prerequisites

- A WinUI 3 desktop app project targeting .NET 6 or later (see [Create your first WinUI 3 project](/windows/apps/winui/winui3/create-your-first-winui3-app)).
- Windows 10 version 1809 (build 17763) or later.
- An app registration in the [Azure portal](https://portal.azure.com) with an **Application (client) ID**. Configure the supported account types to match the accounts you want to sign in:
  - **Personal Microsoft accounts** for consumer (MSA) sign-in.
  - **Accounts in this organizational directory** for Microsoft Entra sign-in.
- The **Microsoft Graph** delegated permission `User.Read` added to the app registration.

> [!IMPORTANT]
> Unlike UWP apps, WinUI 3 desktop apps don't require Store association to use Web Account Manager. You need an app registration with the appropriate identity provider instead.

## Expose the app window

Every interop call in this article needs the window handle (HWND) of your app's main window. The default WinUI 3 template keeps the window in a private field, so expose it as a static property first:

```csharp
// App.xaml.cs
public partial class App : Application
{
    public static Window MainWindow { get; private set; }

    protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
    {
        MainWindow = new MainWindow();
        MainWindow.Activate();
    }
}
```

## Set up the UI

Create a simple XAML UI with a sign-in button and text blocks to display user information:

```xml
<StackPanel HorizontalAlignment="Center" VerticalAlignment="Center">
    <Button x:Name="SignInButton" Content="Sign in" Click="SignInButton_Click" />
    <TextBlock x:Name="UserIdTextBlock"/>
    <TextBlock x:Name="UserNameTextBlock"/>
</StackPanel>
```

Add the following namespaces and a field for your **Application (client) ID** from the app registration in the code-behind file:

```csharp
using System;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web.Core;
using Windows.Security.Credentials;
using Windows.Storage;
using Windows.UI.ApplicationSettings;
using Windows.Data.Json;
using Windows.Web.Http;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using WinRT.Interop;

// Replace with your Application (client) ID from the Azure app registration.
private const string ClientId = "your_client_id_here";
```

## Show the AccountsSettingsPane

The system provides a built-in flyout UI for managing identity providers and web accounts, called the `AccountsSettingsPane`. In a WinUI 3 desktop app, you must use HWND interop to show this pane because there's no implicit `CoreWindow`.

```csharp
private async void SignInButton_Click(object sender, RoutedEventArgs e)
{
    // Get the window handle for the current window.
    IntPtr hwnd = WindowNative.GetWindowHandle(App.MainWindow);

    // Show the accounts settings pane using HWND interop.
    await AccountsSettingsPaneInterop.ShowAddAccountForWindowAsync(hwnd);
}
```

> [!IMPORTANT]
> In WinUI 3 desktop apps, you can't use `AccountsSettingsPane.Show()` or `AccountsSettingsPane.GetForCurrentView()` because there's no `CoreWindow`. Use [`AccountsSettingsPaneInterop`](/windows/win32/api/accountssettingspaneinterop/nn-accountssettingspaneinterop-iaccountssettingspaneinterop) with a window handle instead.

The pane is initially empty because the system provides only the UI shell. You populate it programmatically with your identity providers by handling the `AccountCommandsRequested` event, described in the next section. Register that handler *before* you show the pane — showing the pane is what raises the event.

## Register for AccountCommandsRequested

To add commands to the pane, register for the `AccountCommandsRequested` event. In a WinUI 3 desktop app, use `AccountsSettingsPaneInterop.GetForWindow` to get the pane instance for your window handle:

```csharp
public sealed partial class SignInPage : Page
{
    private const string ClientId = "your_client_id_here";
    private IntPtr _hwnd;

    public SignInPage()
    {
        this.InitializeComponent();
        this.Loaded += SignInPage_Loaded;
        this.Unloaded += SignInPage_Unloaded;
    }

    private void SignInPage_Loaded(object sender, RoutedEventArgs e)
    {
        _hwnd = WindowNative.GetWindowHandle(App.MainWindow);
        var pane = AccountsSettingsPaneInterop.GetForWindow(_hwnd);
        pane.AccountCommandsRequested += BuildPaneAsync;
    }

    private void SignInPage_Unloaded(object sender, RoutedEventArgs e)
    {
        var pane = AccountsSettingsPaneInterop.GetForWindow(_hwnd);
        pane.AccountCommandsRequested -= BuildPaneAsync;
    }

    private async void BuildPaneAsync(AccountsSettingsPane s,
        AccountsSettingsPaneCommandsRequestedEventArgs e)
    {
        // Implemented in the next section.
    }
}
```

Register and deregister the event handler in the `Loaded` and `Unloaded` events to prevent memory leaks. The customized pane is only in memory when there's a high chance the user will interact with it.

When the user selects **Sign in**, `SignInButton_Click` calls `ShowAddAccountForWindowAsync`, which raises `AccountCommandsRequested`, which runs `BuildPaneAsync` to populate the pane.

## Build the account settings pane

The `BuildPaneAsync` method is called whenever the `AccountsSettingsPane` is shown. Place the code to customize the displayed commands here.

Start by obtaining a deferral to delay showing the pane until you finish building it. Always complete the deferral in a `finally` block so that exceptions don't leave the pane in a bad state:

```csharp
private async void BuildPaneAsync(AccountsSettingsPane s,
    AccountsSettingsPaneCommandsRequestedEventArgs e)
{
    var deferral = e.GetDeferral();
    try
    {
        var provider = await WebAuthenticationCoreManager.FindAccountProviderAsync(
            "https://login.microsoft.com", "consumers");

        var command = new WebAccountProviderCommand(provider, GetTokenAsync);
        e.WebAccountProviderCommands.Add(command);
    }
    finally
    {
        deferral.Complete();
    }
}

private async void GetTokenAsync(WebAccountProviderCommand command)
{
    // Implemented in the next section.
}
```

> [!NOTE]
> Keep all of the code that adds providers, headers, and commands *between* `GetDeferral` and `Complete`. The snippets in the following sections each show a single addition — combine them inside this one deferral block.

If you're developing an enterprise app, use `"organizations"` instead of `"consumers"` to connect to a Microsoft Entra instance. The rest of this article works the same way for both authorities because both call Microsoft Graph.

### Request a token

Once the sign-in option displays in the `AccountsSettingsPane`, handle what happens when the user selects it. The `GetTokenAsync` method fires when the user chooses to sign in. Request the Microsoft Graph `User.Read` scope, and pass your app's client ID:

```csharp
private async void GetTokenAsync(WebAccountProviderCommand command)
{
    const string clientId = "your_client_id_here";
    IntPtr hwnd = WindowNative.GetWindowHandle(App.MainWindow);

    var request = new WebTokenRequest(command.WebAccountProvider, "User.Read", clientId);

    // Use the window-aware interop API so the system UI is parented to your window.
    WebTokenRequestResult result =
        await WebAuthenticationCoreManagerInterop.RequestTokenForWindowAsync(hwnd, request);

    if (result.ResponseStatus == WebTokenRequestStatus.Success)
    {
        string token = result.ResponseData[0].Token;
    }
}
```

The `"User.Read"` string passed to the *scope* parameter represents the type of information you request. This Microsoft Graph scope provides access to the signed-in user's basic profile, like name and email. The client ID identifies your app registration to the identity provider.

> [!TIP]
> If your app uses a sign-in hint (to populate the user field with a default email address) or another special property, list it in the [`WebTokenRequest.AppProperties`](/uwp/api/windows.security.authentication.web.core.webtokenrequest.appproperties) property. The system ignores this property when caching the web account, which prevents account mismatches in the cache.

## Use the token

The `RequestTokenForWindowAsync` method returns a `WebTokenRequestResult` object containing the results of your request. Always check `ResponseStatus` before reading the token.

Once you have a token, call your provider's API. The following example calls Microsoft Graph to get the user's basic profile information and displays it in the UI:

```csharp
private async void GetTokenAsync(WebAccountProviderCommand command)
{
    const string clientId = "your_client_id_here";
    IntPtr hwnd = WindowNative.GetWindowHandle(App.MainWindow);

    var request = new WebTokenRequest(command.WebAccountProvider, "User.Read", clientId);
    WebTokenRequestResult result =
        await WebAuthenticationCoreManagerInterop.RequestTokenForWindowAsync(hwnd, request);

    if (result.ResponseStatus == WebTokenRequestStatus.Success)
    {
        string token = result.ResponseData[0].Token;
        var restApi = new Uri("https://graph.microsoft.com/v1.0/me");

        using (var client = new System.Net.Http.HttpClient())
        {
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync(restApi);

            if (!response.IsSuccessStatusCode)
            {
                // Handle error — the token may be expired or the user may have
                // revoked consent. Log the status and prompt the user to sign in again.
                return;
            }

            string content = await response.Content.ReadAsStringAsync();
            var jsonObject = JsonObject.Parse(content);
            string id = jsonObject["id"].GetString();
            string name = jsonObject["displayName"].GetString();

            // Update the UI on the dispatcher thread.
            UserIdTextBlock.Text = "Id: " + id;
            UserNameTextBlock.Text = "Name: " + name;
        }
    }
}
```

> [!NOTE]
> This example handles only the `Success` case. In production, also handle other [`WebTokenRequestStatus`](/uwp/api/windows.security.authentication.web.core.webtokenrequeststatus) values — `UserInteractionRequired`, `AccountSwitch`, `AccountProviderNotAvailable`, `ProviderError`, and `UserCancel` — and account for unusual scenarios like a user revoking your app's permission or removing their account from Windows.

## Store the account for future use

Tokens have varying lifespans (some are valid for only a few hours). You don't need to re-show the `AccountsSettingsPane` each time a token expires. After a user authorizes your app once, store the user's account information for future use.

Use the [`WebAccount`](/uwp/api/windows.security.credentials.webaccount) class returned from the token request:

```csharp
private void StoreWebAccount(WebAccount account)
{
    // ApplicationData.Current.LocalSettings requires package identity (MSIX).
    // If your app is unpackaged, use an alternative such as a local JSON file
    // or the Windows Registry.
    ApplicationData.Current.LocalSettings.Values["CurrentUserProviderId"] =
        account.WebAccountProvider.Id;
    ApplicationData.Current.LocalSettings.Values["CurrentUserId"] = account.Id;
}
```

> [!IMPORTANT]
> `ApplicationData.Current.LocalSettings` requires package identity (an MSIX-packaged app). If your app is unpackaged, use an alternative storage mechanism such as a local JSON settings file or the Windows Registry.

Then, attempt to obtain a token silently in the background with the stored `WebAccount`. Use the same `User.Read` scope you used interactively:

```csharp
private async Task<string> GetTokenSilentlyAsync()
{
    const string clientId = "your_client_id_here";

    string providerId =
        ApplicationData.Current.LocalSettings.Values["CurrentUserProviderId"]?.ToString();
    string accountId =
        ApplicationData.Current.LocalSettings.Values["CurrentUserId"]?.ToString();

    if (providerId == null || accountId == null)
    {
        return null;
    }

    WebAccountProvider provider =
        await WebAuthenticationCoreManager.FindAccountProviderAsync(providerId);
    WebAccount account =
        await WebAuthenticationCoreManager.FindAccountAsync(provider, accountId);

    var request = new WebTokenRequest(provider, "User.Read", clientId);
    WebTokenRequestResult result =
        await WebAuthenticationCoreManager.GetTokenSilentlyAsync(request, account);

    if (result.ResponseStatus == WebTokenRequestStatus.Success)
    {
        return result.ResponseData[0].Token;
    }

    // UserInteractionRequired or other status — caller should show the pane.
    return null;
}
```

Call the silent token method before showing the `AccountsSettingsPane`. If the token is obtained in the background, there's no need to show the pane:

```csharp
private async void SignInButton_Click(object sender, RoutedEventArgs e)
{
    string silentToken = await GetTokenSilentlyAsync();

    if (silentToken != null)
    {
        // Token obtained silently — use it directly.
    }
    else
    {
        // Token could not be obtained silently — show the AccountsSettingsPane.
        IntPtr hwnd = WindowNative.GetWindowHandle(App.MainWindow);
        await AccountsSettingsPaneInterop.ShowAddAccountForWindowAsync(hwnd);
    }
}
```

Because obtaining a token silently is straightforward, use this process to refresh your token between sessions rather than caching an existing token that might expire.

> [!NOTE]
> The example above covers only basic success and failure cases. Your app should also account for unusual scenarios (like a user revoking your app's permission or removing their account from Windows) and handle them gracefully.

## Remove a stored account

If you persist a web account, give your users the ability to disassociate their account from your app. This lets them sign out, so their account information is no longer loaded automatically at launch. First, remove any saved account and provider information from storage. Then call [`SignOutAsync`](/uwp/api/windows.security.credentials.webaccount.signoutasync) to clear the cache and invalidate any existing tokens:

```csharp
private async Task SignOutAccountAsync(WebAccount account)
{
    ApplicationData.Current.LocalSettings.Values.Remove("CurrentUserProviderId");
    ApplicationData.Current.LocalSettings.Values.Remove("CurrentUserId");
    await account.SignOutAsync();
}
```

## Add third-party identity providers

If you need to authenticate with a third-party identity provider (such as Google, GitHub, or a custom OAuth service), use the [`OAuth2Manager`](/windows/apps/develop/security/oauth2) API instead of adding a custom `WebAccountProvider` to the `AccountsSettingsPane`. `OAuth2Manager` supports standard OAuth 2.0 authorization code flows with PKCE and uses the system browser for sign-in.

For details and a complete code sample, see [OAuth connections and APIs](oauth2.md).

> [!NOTE]
> The `AccountsSettingsPane` supports Microsoft and Microsoft Entra identity providers natively through Web Account Manager. For all other providers in desktop apps, `OAuth2Manager` is the recommended approach.

## Add a custom header

Customize the account settings pane using the `HeaderText` property:

```csharp
private async void BuildPaneAsync(AccountsSettingsPane s,
    AccountsSettingsPaneCommandsRequestedEventArgs e)
{
    var deferral = e.GetDeferral();
    try
    {
        e.HeaderText = "Sign in to access your account.";

        // Add provider commands here...
    }
    finally
    {
        deferral.Complete();
    }
}
```

Keep header text short and descriptive. If your sign-in process is complicated and you need to display more information, link the user to a separate page using a custom link.

## Add custom links

You can add custom commands to the `AccountsSettingsPane` that appear as links below your supported providers. Custom commands are useful for simple tasks related to user accounts, like displaying a privacy policy or launching a support page:

```csharp
private async void BuildPaneAsync(AccountsSettingsPane s,
    AccountsSettingsPaneCommandsRequestedEventArgs e)
{
    var deferral = e.GetDeferral();
    try
    {
        var settingsCmd = new SettingsCommand(
            "settings_privacy",
            "Privacy policy",
            async (x) => await Windows.System.Launcher.LaunchUriAsync(
                new Uri("https://privacy.microsoft.com/en-US/")));

        e.Commands.Add(settingsCmd);

        // Add provider commands here...
    }
    finally
    {
        deferral.Complete();
    }
}
```

Limit custom commands to intuitive, account-related scenarios.

## Related content

- [MSAL.NET with the WAM broker](/entra/msal/dotnet/acquiring-tokens/desktop-mobile/wam) — recommended authentication library for WinUI 3 desktop apps
- [WinUI 3 desktop authentication sample](https://github.com/Azure-Samples/ms-identity-docs-code-dotnet/tree/main/desktop-winui) — complete MSAL + WAM working example
- [OAuth connections and APIs](oauth2.md) — for third-party identity providers
- [AccountsSettingsPaneInterop API reference](/windows/win32/api/accountssettingspaneinterop/nn-accountssettingspaneinterop-iaccountssettingspaneinterop)
- [Credential locker](credential-locker.md)
- [Microsoft Graph documentation](https://developer.microsoft.com/graph)
