---
title: Architecture patterns for WinUI 3 desktop apps
description: Learn how to structure WinUI 3 apps with dependency injection, configuration management, and enterprise patterns.
author: GrantMeStrength
ms.author: jken
ms.topic: how-to
ms.date: 07/21/2026
---

# Architecture patterns for WinUI 3 desktop apps

This article shows how to apply proven architecture patterns to WinUI 3 desktop apps built with the Windows App SDK. You'll learn how to set up dependency injection, manage configuration, and structure code for enterprise line-of-business (LOB) scenarios.

## Prerequisites

- Windows App SDK 1.5 or later
- .NET 8 or later
- Visual Studio 2022 version 17.10 or later with the **.NET desktop development** and **Windows application development** workloads

## Dependency injection

WinUI 3 desktop apps don't include a built-in dependency injection (DI) container like ASP.NET Core does, but you can add one using the same `Microsoft.Extensions.DependencyInjection` NuGet package. DI makes your code testable, loosely coupled, and easier to maintain.

### Set up a DI container

Install the NuGet package:

```console
dotnet add package Microsoft.Extensions.DependencyInjection
dotnet add package Microsoft.Extensions.Hosting
```

Configure the host and services in your `App.xaml.cs`:

```csharp
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Xaml;

public partial class App : Application
{
    public IHost Host { get; }

    public static T GetService<T>() where T : class
    {
        if ((App.Current as App)!.Host.Services.GetService(typeof(T)) is not T service)
        {
            throw new ArgumentException(
                $"{typeof(T)} needs to be registered in ConfigureServices.");
        }
        return service;
    }

    public App()
    {
        InitializeComponent();

        Host = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
            .UseContentRoot(AppContext.BaseDirectory)
            .ConfigureServices((context, services) =>
            {
                // Services
                services.AddSingleton<INavigationService, NavigationService>();
                services.AddSingleton<IDataService, DataService>();
                services.AddTransient<IDialogService, DialogService>();

                // ViewModels
                services.AddTransient<MainViewModel>();
                services.AddTransient<SettingsViewModel>();

                // Views
                services.AddTransient<MainPage>();
                services.AddTransient<SettingsPage>();
            })
            .Build();
    }
}
```

> [!NOTE]
> If you forget to register a service, the `GetService<T>()` helper above throws an `ArgumentException` at runtime with the missing type name. Run the app and navigate to each page during development to verify all registrations are correct.

### Inject dependencies into ViewModels

With the container configured, your ViewModels receive dependencies through constructor injection:

```csharp
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

public partial class MainViewModel : ObservableObject
{
    private readonly IDataService _dataService;
    private readonly INavigationService _navigationService;

    public MainViewModel(IDataService dataService, INavigationService navigationService)
    {
        _dataService = dataService;
        _navigationService = navigationService;
    }

    [ObservableProperty]
    private string _statusMessage = string.Empty;

    [RelayCommand]
    private async Task LoadDataAsync()
    {
        StatusMessage = "Loading...";
        var items = await _dataService.GetItemsAsync();
        StatusMessage = $"Loaded {items.Count} items";
    }
}
```

### Service lifetimes

Choose the appropriate lifetime when registering services:

| Lifetime | Method | Use for |
|----------|--------|---------|
| Singleton | `AddSingleton<T>()` | Navigation, app-wide state, caches |
| Scoped | `AddScoped<T>()` | Per-window or per-dialog contexts |
| Transient | `AddTransient<T>()` | ViewModels, stateless services |

> [!TIP]
> Register ViewModels as **Transient** so each navigation creates a fresh instance. Register services that hold app-wide state as **Singleton**.

## Configuration management

Use `Microsoft.Extensions.Configuration` to manage app settings in desktop apps, the same pattern used in ASP.NET Core.

### Add configuration support

Install the required packages:

```console
dotnet add package Microsoft.Extensions.Configuration
dotnet add package Microsoft.Extensions.Configuration.Json
dotnet add package Microsoft.Extensions.Options
```

Create an `appsettings.json` file in your project root. In Solution Explorer, right-click the file, select **Properties**, and set **Copy to Output Directory** to **Copy if newer**. Alternatively, add `<CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>` to the file's entry in your `.csproj`.

```json
{
  "AppSettings": {
    "ApiBaseUrl": "https://api.contoso.com/v2",
    "MaxRetryCount": 3,
    "EnableTelemetry": true
  },
  "Logging": {
    "LogLevel": "Information"
  }
}
```

Bind the configuration in your DI setup:

```csharp
.ConfigureServices((context, services) =>
{
    // Bind settings to a strongly-typed class
    services.Configure<AppSettings>(
        context.Configuration.GetSection("AppSettings"));

    // Inject IOptions<AppSettings> into services
    services.AddSingleton<IApiClient, ApiClient>();
})
```

### Use configuration in services

```csharp
using Microsoft.Extensions.Options;

public class ApiClient : IApiClient
{
    private readonly AppSettings _settings;
    private readonly HttpClient _httpClient;

    public ApiClient(IOptions<AppSettings> options)
    {
        _settings = options.Value;
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(_settings.ApiBaseUrl)
        };
    }
}
```

### User settings persistence

For per-user settings that survive app updates, use `Windows.Storage.ApplicationData` (packaged apps) or a local JSON file (unpackaged apps):

```csharp
public class UserSettingsService : IUserSettingsService
{
    private readonly string _settingsPath;

    public UserSettingsService()
    {
        var localAppData = Environment.GetFolderPath(
            Environment.SpecialFolder.LocalApplicationData);
        _settingsPath = Path.Combine(localAppData, "Contoso", "MyApp", "settings.json");
    }

    public async Task SaveAsync<T>(string key, T value)
    {
        var settings = await LoadAllAsync();
        settings[key] = JsonSerializer.Serialize(value);
        Directory.CreateDirectory(Path.GetDirectoryName(_settingsPath)!);
        await File.WriteAllTextAsync(
            _settingsPath, JsonSerializer.Serialize(settings));
    }
}
```

> [!NOTE]
> Packaged apps (MSIX) can use `ApplicationData.Current.LocalSettings` for simple key-value pairs. Unpackaged apps must manage their own storage location.

## Feature flags

Implement feature flags to enable gradual rollout and A/B testing without redeployment.

### Local feature flags with configuration

```csharp
public interface IFeatureFlagService
{
    bool IsEnabled(string featureName);
}

public class FeatureFlagService : IFeatureFlagService
{
    private readonly Dictionary<string, bool> _flags;

    public FeatureFlagService(IConfiguration configuration)
    {
        _flags = configuration.GetSection("FeatureFlags")
            .Get<Dictionary<string, bool>>() ?? new();
    }

    public bool IsEnabled(string featureName) =>
        _flags.TryGetValue(featureName, out var enabled) && enabled;
}
```

### Azure App Configuration integration

For cloud-managed feature flags, use Azure App Configuration:

```console
dotnet add package Microsoft.Extensions.Configuration.AzureAppConfiguration
dotnet add package Microsoft.FeatureManagement
```

```csharp
using Azure.Identity;

Host = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
    .ConfigureAppConfiguration((context, config) =>
    {
        config.AddAzureAppConfiguration(options =>
        {
            options.Connect(
                    new Uri("https://<your-store>.azconfig.io"),
                    new DefaultAzureCredential())
                .UseFeatureFlags(flagOptions =>
                {
                    flagOptions.CacheExpirationInterval = TimeSpan.FromMinutes(5);
                });
        });
    })
    .ConfigureServices((context, services) =>
    {
        services.AddFeatureManagement(context.Configuration);
    })
    .Build();
```

> [!NOTE]
> For local development, you can use a connection string instead of `DefaultAzureCredential`. Store the connection string in an environment variable or Windows Credential Manager — never in source control:
>
> ```csharp
> options.Connect(Environment.GetEnvironmentVariable("APP_CONFIG_CONNECTION_STRING"))
> ```

> [!TIP]
> For Store-based gradual rollout at the package level, see [Gradual package rollout](/windows/apps/publish/gradual-package-rollout).

## Enterprise and LOB patterns

Line-of-business apps have additional requirements around identity, data protection, and device management.

### Identity and conditional access

Use MSAL (Microsoft Authentication Library) for enterprise authentication:

```csharp
services.AddSingleton<IAuthService>(sp =>
{
    var app = PublicClientApplicationBuilder
        .Create("your-client-id")
        .WithAuthority(AzureCloudInstance.AzurePublic, "your-tenant-id")
        .WithRedirectUri("http://localhost")
        .Build();
    return new AuthService(app);
});
```

> [!IMPORTANT]
> The `http://localhost` redirect URI is suitable for development. For production desktop apps, use the [Windows broker (WAM)](/entra/msal/dotnet/acquiring-tokens/desktop-mobile/wam) instead, which provides SSO with the user's Windows account and stronger token protection.

Enterprise apps deployed through Intune can enforce conditional access policies that require:

- Device compliance (encryption, PIN, OS version)
- Multi-factor authentication
- Network location restrictions

### Offline data and caching

Desktop LOB apps frequently need to work offline. Implement a repository pattern with local caching:

```csharp
public class CachedRepository<T> : IRepository<T> where T : class, IEntity
{
    private readonly IApiClient _apiClient;
    private readonly ILocalDatabase _localDb;

    public async Task<IReadOnlyList<T>> GetAllAsync(bool forceRefresh = false)
    {
        if (!forceRefresh)
        {
            var cached = await _localDb.GetAllAsync<T>();
            if (cached.Any())
                return cached;
        }

        try
        {
            var items = await _apiClient.GetAsync<List<T>>();
            await _localDb.UpsertAllAsync(items);
            return items;
        }
        catch (HttpRequestException)
        {
            // Offline fallback
            return await _localDb.GetAllAsync<T>();
        }
    }
}
```

### Data protection

Use `Windows.Security.Cryptography.DataProtection` (packaged apps) or the .NET `DataProtectionProvider` for encrypting sensitive local data.

Install the required package:

```console
dotnet add package Microsoft.AspNetCore.DataProtection.Extensions
```

Then register data protection in your DI container:

```csharp
using Microsoft.AspNetCore.DataProtection;

services.AddDataProtection()
    .SetApplicationName("Contoso.LOBApp")
    .ProtectKeysWithDpapi();
```

## Layered architecture

Structure your WinUI 3 app in layers to keep dependencies flowing in one direction:

```
┌─────────────────────────────┐
│   Views (XAML + code-behind)│  ← UI layer, no business logic
├─────────────────────────────┤
│   ViewModels (MVVM Toolkit) │  ← Presentation logic, commands
├─────────────────────────────┤
│   Services / Use Cases      │  ← Business rules, orchestration
├─────────────────────────────┤
│   Repositories / Data       │  ← Data access, API clients, caching
└─────────────────────────────┘
```

Rules:

- Each layer depends only on the layer directly below it.
- ViewModels never reference UI types (`Page`, `Window`, `ContentDialog`).
- Services define interfaces; implementations live in the data layer.
- Register all cross-layer dependencies in the DI container.

## Backward compatibility and versioning

When you release new versions of your app, consider:

- **Data migration**: Version your local database schema. Use a migration runner at startup to upgrade from any previous schema to the current one.
- **Settings migration**: Store a schema version in your settings file. On load, apply transforms from old formats to new.
- **Side-by-side installs**: MSIX upgrades a package in place by default. To run multiple major versions side by side, assign each version a distinct package family name at design time.

```csharp
public class DatabaseMigrator
{
    public async Task MigrateAsync(SqliteConnection db)
    {
        var currentVersion = await GetSchemaVersionAsync(db);

        if (currentVersion < 2)
            await ApplyMigration_v2(db);
        if (currentVersion < 3)
            await ApplyMigration_v3(db);

        await SetSchemaVersionAsync(db, LatestVersion);
    }
}
```

## Verify your setup

Run the app and navigate to each page to verify services resolve correctly. If a service is not registered, you will see an `InvalidOperationException` at runtime with the missing type name. Also confirm that:

- Configuration values load from `appsettings.json` (check a bound property in the debugger).
- Feature flags evaluate as expected (toggle a flag and restart).
- Offline caching returns data when the network is unavailable.

## Related content

- [Data binding and MVVM](/windows/apps/develop/data-binding/data-binding-and-mvvm)
- [MVVM performance tips](/windows/apps/develop/performance/mvvm-performance-tips)
- [App extensibility overview](/windows/apps/develop/launch/extensibility-overview)
- [Gradual package rollout](/windows/apps/publish/gradual-package-rollout)
- [CommunityToolkit.Mvvm documentation](/dotnet/communitytoolkit/mvvm/)
- [Version adaptive code](/windows/apps/develop/testing/version-adaptive-code)
