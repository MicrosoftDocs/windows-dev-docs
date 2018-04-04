---
author: QuinnRadich
title: Configuring the Settings page
description: The settings page in Windows Template Studio can be configured to add additional settings. The steps that must be taken depend on the design pattern you're using.
keywords: template, Windows Template Studio, template studio, settings
ms.author: quradic
ms.date: 4/4/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
ms.localizationpriority: medium
---

# Configuring the Settings page in Windows Template Studio

By default the settings page contains a single boolean setting to track whether the app should be displayed with the Light or Dark theme.

You can add additional settings by following the instructions appropriate to the design pattern you are using. Each of the options below covers all the code changes necessary, and illustrates some of the practical differences between three of the patterns.

## Using the code-behind design pattern

### Add another boolean setting

Add the following below the `StackPanel` containing the `RadioButton`s in **SettingsView.xaml**

```xml
<CheckBox IsChecked="{x:Bind IsAutoErrorReportingEnabled, Mode=OneWay}"
          x:Uid="Settings_EnableAutoErrorReporting"
          Checked="CheckBoxChecked"
          Unchecked="CheckBoxUnchecked"
          Margin="0,8,0,0" />
```

Add an entry to **Strings/en-us/Resources.resw**

Name: **Settings_EnableAutoErrorReporting.Content**

Value: **Automatically report errors**

When run it will now look like this:

![Item added to the default settings configuration](images/Settings_added_checkbox.png)

But if you try and run it now you will get build errors as the code behind file hasn't been updated to add the new property and event handlers.

### Update the code-behind file

If using the Blank or NavigationView project types.

In **SettingsPage.xaml.cs**, change the `OnNavigatedTo` method to be like this

```csharp
protected override async void OnNavigatedTo(NavigationEventArgs e)
{
    Initialize();
    IsAutoErrorReportingEnabled = await Windows.Storage.ApplicationData.Current.LocalSettings.ReadAsync<bool>(nameof(IsAutoErrorReportingEnabled));
}
```

If using the Pivot&Tabs project type.

In **SettingsPage.xaml.cs**, change the `OnLoaded` and `Initialize` methods to be like this

```csharp
private async void OnLoaded(object sender, RoutedEventArgs e)
{
    await InitializeAsync();
}

private async Task InitializeAsync()
{
    VersionDescription = GetVersionDescription();
    IsAutoErrorReportingEnabled = await Windows.Storage.ApplicationData.Current.LocalSettings.ReadAsync<bool>(nameof(IsAutoErrorReportingEnabled));
}
```

For all project types, also add the following to **SettingsPage.xaml.cs**.

```csharp
using {YourAppName}.Helpers;


private bool _isAutoErrorReportingEnabled;
public bool? IsAutoErrorReportingEnabled
{
    get { return _isAutoErrorReportingEnabled; }
    set { Set(ref _isAutoErrorReportingEnabled, (bool)value); }
}

private async void CheckBoxChecked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
{
        await Windows.Storage.ApplicationData.Current.LocalSettings.SaveAsync(nameof(IsAutoErrorReportingEnabled), true);
}

private async void CheckBoxUnchecked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
{
    await Windows.Storage.ApplicationData.Current.LocalSettings.SaveAsync(nameof(IsAutoErrorReportingEnabled), false);
}
```

### Accessing the setting from elsewhere in the app

If you want to access the property elsewhere in the app, the easiest way to do this is to read the setting directly. The code below reads the value into a variable called `isEnabled` which you can then query as needed.

```csharp
var isEnabled = await Helpers.SettingsStorageExtensions.ReadAsync<bool>(Windows.Storage.ApplicationData.Current.LocalSettings, "IsAutoErrorReportingEnabled");
```

## Using the MVVM Basic pattern

### Add another boolean setting

Let's add a boolean setting to control whether errors should be automatically reported.
Adding a setting requires you to:

* Update the View so it's possible to see and change the setting
* Update the ViewModel to add logic related to changing the setting.

### Update the View

Add the following below the `StackPanel` containing the `RadioButton`s in **SettingsView.xaml**

```xml
<CheckBox IsChecked="{x:Bind ViewModel.IsAutoErrorReportingEnabled, Mode=TwoWay}"
          x:Uid="Settings_EnableAutoErrorReporting"
          Margin="0,8,0,0" />
```

Add an entry to **Strings/en-us/Resources.resw**

Name: **Settings_EnableAutoErrorReporting.Content**

Value: **Automatically report errors**

When run it will now look like this:

![Item added to the default settings configuration](images/Settings_added_checkbox.png)

But if you try and run it now you will get build errors as the ViewModel hasn't been updated to add the new property.

### Update the ViewModel

The `IsLightThemeEnabled` property uses a static `ThemeSelectorService`. It is not necessary to create equivalent services for every setting but is appropriate for the theme preference as this is needed when the app launches.

Because we may want to access settings in various parts of the app it's important that the same settings values are used in all locations. The simplest way to do this is to have a single instance of the `SettingsViewModel` and use it for all access to settings values.

The generated code in includes a Singleton helper class to provide access to a single instance of the view model which we can use everywhere.

With this knowledge we can now add the new property for accessing our stored setting. We also need to add a new, awaitable initializer for the property too.
Add the following to **SettingsViewModel.cs**

```csharp
using {YourAppName}.Helpers;
using System.Threading.Tasks;


private bool? _isAutomaticErrorReportingEnabled;

public bool? IsAutoErrorReportingEnabled
{
    get => _isAutomaticErrorReportingEnabled ?? false;

    set
    {
        if (value != _isAutomaticErrorReportingEnabled)
        {
            Task.Run(async () => await Windows.Storage.ApplicationData.Current.LocalSettings.SaveAsync(nameof(IsAutoErrorReportingEnabled), value ?? false));
        }

        Set(ref _isAutomaticErrorReportingEnabled, value);
    }
}

private bool _hasInstanceBeenInitialized = false;

public async Task EnsureInstanceInitializedAsync()
{
    if (!_hasInstanceBeenInitialized)
    {
        IsAutoErrorReportingEnabled =
            await Windows.Storage.ApplicationData.Current.LocalSettings.ReadAsync<bool>(nameof(IsAutoErrorReportingEnabled));

        Initialize();

        _hasInstanceBeenInitialized = true;
    }
}
```

Then change the `SwitchThemeCommand` to match this:

```csharp
public ICommand SwitchThemeCommand
{
    get
    {
        if (_switchThemeCommand == null)
        {
            _switchThemeCommand = new RelayCommand<ElementTheme>(
                async (param) =>
                {
                    if (_hasInstanceBeenInitialized)
                    {
                        await ThemeSelectorService.SetThemeAsync(param);
                    }
                });
        }

        return _switchThemeCommand;
    }
}
```

We must now update our uses of the ViewModel.

#### If your app is using the Blank or NavigationView structure

 In **SettingsPage.xaml.cs** change the property declaration from this:

```csharp
public SettingsViewModel ViewModel { get; } = new SettingsViewModel();
```

to this:

```csharp
public SettingsViewModel ViewModel { get; } = Singleton<SettingsViewModel>.Instance;
```

so it uses the single instance.

You may also need to add the following using statement to the top of the file.

```csharp
using {YourAppName}.Helpers;
```

Then change the `OnNavigatedTo()` method so that instead of calling the old Initialize method, like this:

```csharp
protected override void OnNavigatedTo(NavigationEventArgs e)
{
    ViewModel.Initialize();
}
```

It now awaits the call to the new Initializer like this:

```csharp
protected override async void OnNavigatedTo(NavigationEventArgs e)
{
    await ViewModel.EnsureInstanceInitializedAsync();
}
```

#### If your app is using the 'Pivot and Tabs' structure

In **SettingsPage.xaml.cs** change the constructor so that it handles the `OnLoaded` event and add the following event handler, like this:

```csharp
public SettingsPage()
{
    InitializeComponent();
    ViewModel.Initialize();

    this.Loaded += SettingsPage_Loaded;
}

private async void SettingsPage_Loaded(object sender, RoutedEventArgs e)
{
    this.Loaded -= SettingsPage_Loaded;
    await ViewModel.EnsureInstanceInitializedAsync();
}
```

Everything is now complete. You can run the app and it will remember the value between invocations of the app.

### Accessing the setting from elsewhere in the app

If you want to access the property elsewhere in the app, ensure you have called `await Singleton<SettingsViewModel>.Instance.EnsureInstanceInitializedAsync();`. Then you can get or set the property with `Singleton<SettingsViewModel>.Instance.IsAutoErrorReportingEnabled`.
For example:

```csharp
try
{
    ...
}
catch (Exception exc)
{
    await Singleton<SettingsViewModel>.Instance.EnsureInstanceInitializedAsync();
    if (Singleton<SettingsViewModel>.Instance.IsAutoErrorReportingEnabled)
    {
        // Send the error details to the server
    }
}
```

If you only use the value in one or two places you could call `EnsureInstanceInitializedAsync()` each time before you access `Singleton<SettingsViewModel>.Instance`. But, as `EnsureInstanceInitializedAsync()` only needs to be called once before it is used, if you have lots of settings or need to access them in many places you could call it once as part of the `InitializeAsync()` method in **ActivationService.cs**.

## Using the MVVM Light design pattern

### Add another boolean setting

Let's add a boolean setting to control whether errors should be automatically reported.
Adding a setting requires you to:

* Update the View so it's possible to see and change the setting
* Update the ViewModel to add logic related to changing the setting.

### Update the View

Add the following below the `StackPanel` containing the `RadioButton`s in **SettingsView.xaml**

```xml
<CheckBox IsChecked="{x:Bind ViewModel.IsAutoErrorReportingEnabled, Mode=TwoWay}"
          x:Uid="Settings_EnableAutoErrorReporting"
          Margin="0,8,0,0" />
```

Add an entry to **Strings/en-us/Resources.resw**

Name: **Settings_EnableAutoErrorReporting.Content**

Value: **Automatically report errors**

When run it will now look like this:

![Item added to the default settings configuration](images/Settings_added_checkbox.png)

But if you try and run it now you will get build errors as the ViewModel hasn't been updated to add the new property.

### Update the ViewModel

The `IsLightThemeEnabled` property uses a static `ThemeSelectorService`. It is not necessary to create equivalent services for every setting but is appropriate for the theme preference as this is needed when the app launches.

Because we may want to access settings in various parts of the app it's important that the same settings values are used in all locations. The simplest way to do this is to have a single instance of the `SettingsViewModel` and use it for all access to settings values.

The generated code in includes a Singleton helper class to provide access to a single instance of the view model which we can use everywhere.

With this knowledge we can now add the new property for accessing our stored setting. We also need to add a new, awaitable initializer for the property too.
Add the following to **SettingsViewModel.cs**

```csharp
using {YourAppName}.Helpers;
using System.Threading.Tasks;


private bool? _isAutomaticErrorReportingEnabled;

public bool? IsAutoErrorReportingEnabled
{
    get => _isAutomaticErrorReportingEnabled ?? false;

    set
    {
        if (value != _isAutomaticErrorReportingEnabled)
        {
            Task.Run(async () => await Windows.Storage.ApplicationData.Current.LocalSettings.SaveAsync(nameof(IsAutoErrorReportingEnabled), value ?? false));
        }

        Set(ref _isAutomaticErrorReportingEnabled, value);
    }
}

private bool _hasInstanceBeenInitialized = false;

public async Task EnsureInstanceInitializedAsync()
{
    if (!_hasInstanceBeenInitialized)
    {
        IsAutoErrorReportingEnabled =
            await Windows.Storage.ApplicationData.Current.LocalSettings.ReadAsync<bool>(nameof(IsAutoErrorReportingEnabled));

        Initialize();

        _hasInstanceBeenInitialized = true;
    }
}
```

Then change the `SwitchThemeCommand` property to match this:

```csharp
public ICommand SwitchThemeCommand
{
    get
    {
        if (_switchThemeCommand == null)
        {
            _switchThemeCommand = new RelayCommand<ElementTheme>(
                async (param) =>
                {
                    if (_hasInstanceBeenInitialized)
                    {
                        await ThemeSelectorService.SetThemeAsync(param);
                    }
                });
        }

        return _switchThemeCommand;
    }
}
```

We must now update our uses of the ViewModel.

#### If your app is using the Blank or NavigationView structure

In **SettingsPage.xaml.cs** change the `OnNavigatedTo()` method so that instead of calling the old Initialize method, like this:

```csharp
protected override void OnNavigatedTo(NavigationEventArgs e)
{
    ViewModel.Initialize();
}
```

It now awaits the call to the new Initializer like this:

```csharp
protected override async void OnNavigatedTo(NavigationEventArgs e)
{
    await ViewModel.EnsureInstanceInitializedAsync();
}
```

#### If your app is using the 'Pivot and Tabs' structure

In **SettingsPage.xaml.cs** change the constructor so that it handles the `OnLoaded` event and add the following event handler, like this:

```csharp
public SettingsPage()
{
    InitializeComponent();
    ViewModel.Initialize();

    this.Loaded += SettingsPage_Loaded;
}

private async void SettingsPage_Loaded(object sender, RoutedEventArgs e)
{
    this.Loaded -= SettingsPage_Loaded;
    await ViewModel.EnsureInstanceInitializedAsync();
}
```

Everything is now complete. You can run the app and it will remember the value between invocations of the app.

### Accessing the setting from elsewhere in the app

If you want to access the property elsewhere in the app, ensure you have called `await ServiceLocator.Current.GetInstance<SettingsViewModel>().EnsureInstanceInitializedAsync();`. Then you can get or set the property with `ServiceLocator.Current.GetInstance<SettingsViewModel>().IsAutoErrorReportingEnabled`.
For example:

```csharp
try
{
    ...
}
catch (Exception exc)
{
    await ServiceLocator.Current.GetInstance<SettingsViewModel>().EnsureInstanceInitializedAsync();
    if (ServiceLocator.Current.GetInstance<SettingsViewModel>().IsAutoErrorReportingEnabled)
    {
        // Send the error details to the server
    }
}
```

If you only use the value in one or two places you could call `EnsureInstanceInitializedAsync()` each time before you access the SettingsViewModel instance. But, as `EnsureInstanceInitializedAsync()` only needs to be called once before it is used, if you have lots of settings or need to access them in many places you could call it once as part of the `InitializeAsync()` method in **ActivationService.cs**.