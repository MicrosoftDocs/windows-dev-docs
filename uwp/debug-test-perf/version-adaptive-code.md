---
title: Version adaptive code
description: Use the ApiInformation class to take advantage of new APIs while maintaining compatibility with previous versions
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.assetid: 3293e91e-6888-4cc3-bad3-61e5a7a7ab4e
ms.localizationpriority: medium
---

# Version adaptive code

You can think about writing adaptive code similarly to how you think about [creating an adaptive UI](/windows/apps/design/layout/layouts-with-xaml). You might design your base UI to run on the smallest screen, and then move or add elements when you detect that your app is running on a larger screen. With adaptive code, you write your base code to run on the lowest OS version, and you can add hand-selected features when you detect that your app is running on a higher version where the new feature is available.

For important background info about ApiInformation, API contracts, and configuring Visual Studio, see [Version adaptive apps](version-adaptive-apps.md).

### Runtime API checks

You use the [Windows.Foundation.Metadata.ApiInformation](/uwp/api/windows.foundation.metadata.apiinformation) class in a condition in your code to test for the presence of the API you want to call. This condition is evaluated wherever your app runs, but it evaluates to **true** only on devices where the API is present and therefore available to call. This lets you write version adaptive code in order to create apps that use APIs that are available only on certain OS versions.

Here, we look at specific examples for targeting new features in the Windows Insider Preview. For a general overview of using **ApiInformation**, see [Programming with extension SDKs](/uwp/extension-sdks/device-families-overview) and [Dynamically detecting features with API contracts](version-adaptive-apps.md#api-contracts).

> [!TIP]
> Numerous runtime API checks can affect the performance of your app. We show the checks inline in these examples. In production code, you should perform the check once and cache the result, then used the cached result throughout your app. 

### Unsupported scenarios

In most cases, you can keep your app's Minimum Version set to SDK version 10240 and use runtime checks to enable any new APIs when your app runs on later a version. However, there are some cases where you must increase your app's Minimum Version in order to use new features.

You must increase your app's Minimum Version if you use:

- a new API that requires a capability that isn't available in an earlier version. You must increase the minimum supported version to one that includes that capability. For more info, see [App capability declarations](../packaging/app-capability-declarations.md).
- any new resource keys added to generic.xaml and not available in a previous version. The version of generic.xaml used at runtime is determined by the OS version the device is running on. You can't use runtime API checks to determine the presence of XAML resources. So, you must only use resource keys that are available in the minimum version that your app supports or a [**XAMLParseException**](/dotnet/api/windows.ui.xaml.markup.xamlparseexception) will cause your app to crash at runtime.

### Adaptive code options

There are two ways to create adaptive code. In most cases, you write your app markup to run on the Minimum version, then use your app code to tap into newer OS features when present. However, if you need to update a property in a visual state, and there is only a property or enumeration value change between OS versions, you can create an extensible state trigger that’s activated based on the presence of an API.

Here, we compare these options.

**App code**

When to use:
- Recommended for all adaptive code scenarios except for specific cases defined below for extensible triggers.

Benefits:
- Avoids developer overhead/complexity of tying API differences into markup.

Drawbacks:
- No Designer support.

**State Triggers**

When to use:
- Use when there is only a property or enum change between OS versions that doesn’t require logic changes, and is connected to a visual state.

Benefits:
- Lets you create specific visual states that are triggered based on the presence of an API.
- Some designer support available.

Drawbacks:
- Use of custom triggers is restricted to visual states, which doesn’t lend itself to complicated adaptive layouts.
- Must use Setters to specify value changes, so only simple changes are possible.
- Custom state triggers are fairly verbose to set up and use.

## Adaptive code examples

In this section, we show several examples of adaptive code that use APIs that are new in Windows 10, version 1607 (Windows Insider Preview).

### Example 1: New enum value

Windows 10, version 1607 adds a new value to the [InputScopeNameValue](/uwp/api/windows.ui.xaml.input.inputscopenamevalue) enumeration: **ChatWithoutEmoji**. This new input scope has the same input behavior as the **Chat** input scope (spellchecking, auto-complete, auto-capitalization), but it maps to a touch keyboard without an emoji button. This is useful if you create your own emoji picker and want to disable the built-in emoji button in the touch keyboard. 

This example shows how to check if the **ChatWithoutEmoji** enum value is present and sets the [InputScope](/uwp/api/windows.ui.xaml.controls.textbox.inputscope) property of a **TextBox** if it is. If it’s not present on the system the app is run on, the **InputScope** is set to **Chat** instead. The code shown could be placed in a Page constructor or Page.Loaded event handler.

> [!TIP]
> When you check an API, use static strings instead of relying on .NET language features, otherwise your app might try to access a type that isn’t defined and crash at runtime.

**C#**
```csharp
// Create a TextBox control for sending messages 
// and initialize an InputScope object.
TextBox messageBox = new TextBox();
messageBox.AcceptsReturn = true;
messageBox.TextWrapping = TextWrapping.Wrap;
InputScope scope = new InputScope();
InputScopeName scopeName = new InputScopeName();

// Check that the ChatWithEmoji value is present.
// (It's present starting with Windows 10, version 1607,
//  the Target version for the app. This check returns false on earlier versions.)         
if (ApiInformation.IsEnumNamedValuePresent("Windows.UI.Xaml.Input.InputScopeNameValue", "ChatWithoutEmoji"))
{
    // Set new ChatWithoutEmoji InputScope if present.
    scopeName.NameValue = InputScopeNameValue.ChatWithoutEmoji;
}
else
{
    // Fall back to Chat InputScope.
    scopeName.NameValue = InputScopeNameValue.Chat;
}

// Set InputScope on messaging TextBox.
scope.Names.Add(scopeName);
messageBox.InputScope = scope;

// For this example, set the TextBox text to show the selected InputScope.
messageBox.Text = messageBox.InputScope.Names[0].NameValue.ToString();

// Add the TextBox to the XAML visual tree (rootGrid is defined in XAML).
rootGrid.Children.Add(messageBox);
```

In the previous example, the TextBox is created and all properties are set in code. However, if you have existing XAML, and just need to change the InputScope property on systems where the new value is supported, you can do that without changing your XAML, as shown here. You set the default value to **Chat** in XAML, but you override it in code if the **ChatWithoutEmoji** value is present.

**XAML**
```xaml
<TextBox x:Name="messageBox"
         AcceptsReturn="True" TextWrapping="Wrap"
         InputScope="Chat"
         Loaded="messageBox_Loaded"/>
```

**C#**
```csharp
private void messageBox_Loaded(object sender, RoutedEventArgs e)
{
    if (ApiInformation.IsEnumNamedValuePresent("Windows.UI.Xaml.Input.InputScopeNameValue", "ChatWithoutEmoji"))
    {
        // Check that the ChatWithEmoji value is present.
        // (It's present starting with Windows 10, version 1607,
        //  the Target version for the app. This code is skipped on earlier versions.)
        InputScope scope = new InputScope();
        InputScopeName scopeName = new InputScopeName();
        scopeName.NameValue = InputScopeNameValue.ChatWithoutEmoji;
        // Set InputScope on messaging TextBox.
        scope.Names.Add(scopeName);
        messageBox.InputScope = scope;
    }

    // For this example, set the TextBox text to show the selected InputScope.
    // This is outside of the API check, so it will happen on all OS versions.
    messageBox.Text = messageBox.InputScope.Names[0].NameValue.ToString();
}
```

Now that we have a concrete example, let’s see how the Target and Minimum version settings apply to it.

In these examples, you can use the Chat enum value in XAML, or in code without a check, because it’s present in the minimum supported OS version. 

If you use the ChatWithoutEmoji value in XAML, or in code without a check, it will compile without error because it's present in the Target OS version. It will also run without error on a system with the Target OS version. However, when the app runs on a system with an OS using the Minimum version, it will crash at runtime because the ChatWithoutEmoji enum value is not present. Therefore, you must use this value only in code, and wrap it in a runtime API check so it’s called only if it’s supported on the current system.

### Example 2: New control

A new version of Windows typically brings new controls to the UWP API surface that bring new functionality to the platform. To leverage the presence of a new control, use the  [ApiInformation.IsTypePresent](/uwp/api/windows.foundation.metadata.apiinformation.istypepresent) method.

Windows 10, version 1607 introduces a new media control called [**MediaPlayerElement**](/uwp/api/windows.ui.xaml.controls.mediaplayerelement). This control builds on the [MediaPlayer](/uwp/api/windows.media.playback.mediaplayer) class, so it brings features like the ability to easily tie into background audio, and it makes use of architectural improvements in the media stack.

However, if the app runs on a device that’s running a version of Windows 10 older than version 1607, you must use the [**MediaElement**](/uwp/api/windows.ui.xaml.controls.mediaelement) control instead of the new **MediaPlayerElement** control. You can use the [**ApiInformation.IsTypePresent**](/uwp/api/windows.foundation.metadata.apiinformation.istypepresent) method to check for the presence of the MediaPlayerElement control at runtime, and load whichever control is suitable for the system where the app is running.

This example shows how to create an app that uses either the new MediaPlayerElement or the old MediaElement depending on whether MediaPlayerElement type is present. 
In this code, you use the [UserControl](/uwp/api/windows.ui.xaml.controls.usercontrol) class to componentize the controls and their related UI and code so that you can switch them in and out based on the OS version. As an alternative, you can use a custom control, which provides more functionality and custom behavior than what’s needed for this simple example.
 
**MediaPlayerUserControl** 

The `MediaPlayerUserControl` encapsulates a **MediaPlayerElement** and several buttons that are used to skip through the media frame by frame. The UserControl lets you treat these controls as a single entity and makes it easier to switch with a MediaElement on older systems. This user control should be used only on systems where MediaPlayerElement is present, so you don’t use ApiInformation checks in the code inside this user control.

> [!NOTE]
> To keep this example simple and focused, the frame step buttons are placed outside of the media player. For a better user experience, you should customize the MediaTransportControls to include your custom buttons. See [Custom transport controls](/windows/apps/design/controls/custom-transport-controls) for more info. 

**XAML**
```xaml
<UserControl
    x:Class="MediaApp.MediaPlayerUserControl"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:local="using:MediaApp"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    mc:Ignorable="d"
    d:DesignHeight="300"
    d:DesignWidth="400">

    <Grid x:Name="MPE_grid>
        <Grid.RowDefinitions>
            <RowDefinition/>
            <RowDefinition Height="Auto"/>
        </Grid.RowDefinitions>
        <StackPanel Orientation="Horizontal" 
                    HorizontalAlignment="Center" Grid.Row="1">
            <RepeatButton Click="StepBack_Click" Content="Step Back"/>
            <RepeatButton Click="StepForward_Click" Content="Step Forward"/>
        </StackPanel>
    </Grid>
</UserControl>
```

**C#**
```csharp
using System;
using Windows.Media.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MediaApp
{
    public sealed partial class MediaPlayerUserControl : UserControl
    {
        public MediaPlayerUserControl()
        {
            this.InitializeComponent();
            
            // The markup code compiler runs against the Minimum OS version so MediaPlayerElement must be created in app code
            MPE = new MediaPlayerElement();
            Uri videoSource = new Uri("ms-appx:///Assets/UWPDesign.mp4");
	        MPE.Source = MediaSource.CreateFromUri(videoSource);
	        MPE.AreTransportControlsEnabled = true;
            MPE.MediaPlayer.AutoPlay = true;

            // Add MediaPlayerElement to the Grid
            MPE_grid.Children.Add(MPE);

        }

        private void StepForward_Click(object sender, RoutedEventArgs e)
        {
            // Step forward one frame, only available using MediaPlayerElement.
            MPE.MediaPlayer.StepForwardOneFrame();
        }

        private void StepBack_Click(object sender, RoutedEventArgs e)
        {
            // Step forward one frame, only available using MediaPlayerElement.
            MPE.MediaPlayer.StepForwardOneFrame();
        }
    }
}
```

**MediaElementUserControl**
 
The `MediaElementUserControl` encapsulates a **MediaElement** control.

**XAML**
```xaml
<UserControl
    x:Class="MediaApp.MediaElementUserControl"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:local="using:MediaApp"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    mc:Ignorable="d"
    d:DesignHeight="300"
    d:DesignWidth="400">

    <Grid>
        <MediaElement AreTransportControlsEnabled="True" 
                      Source="Assets/UWPDesign.mp4"/>
    </Grid>
</UserControl>
```

> [!NOTE]
> The code page for `MediaElementUserControl` contains only generated code, so it's not shown.

**Initialize a control based on IsTypePresent**

At runtime, you call **ApiInformation.IsTypePresent** to check for MediaPlayerElement. If it's present, you load `MediaPlayerUserControl`, if it's not, you load `MediaElementUserControl`.

**C#**
```csharp
public MainPage()
{
    this.InitializeComponent();

    UserControl mediaControl;

    // Check for presence of type MediaPlayerElement.
    if (ApiInformation.IsTypePresent("Windows.UI.Xaml.Controls.MediaPlayerElement"))
    {
        mediaControl = new MediaPlayerUserControl();
    }
    else
    {
        mediaControl = new MediaElementUserControl();
    }

    // Add mediaControl to XAML visual tree (rootGrid is defined in XAML).
    rootGrid.Children.Add(mediaControl);
}
```

> [!IMPORTANT]
> Remember that this check only sets the `mediaControl` object to either `MediaPlayerUserControl` or `MediaElementUserControl`. You need to perform these conditional checks anywhere else in your code that you need to determine whether to use MediaPlayerElement or MediaElement APIs. You should perform the check once and cache the result, then used the cached result throughout your app.

## State trigger examples

Extensible state triggers let you use markup and code together to trigger visual state changes based on a condition that you check in code; in this case, the presence of a specific API. We don’t recommend state triggers for common adaptive code scenarios because of the overhead involved, and the restriction to only visual states. 

You should use state triggers for adaptive code only when you have small UI changes between different OS versions that won’t impact the remaining UI, such as a property or enum value change on a control.

### Example 1: New property

The first step in setting up an extensible state trigger is subclassing the [StateTriggerBase](/uwp/api/windows.ui.xaml.statetriggerbase) class to create a custom trigger that will be active based on the presence of an API. This example shows a trigger that activates if the property presence matches the `_isPresent` variable set in XAML.

**C#**
```csharp
class IsPropertyPresentTrigger : StateTriggerBase
{
    public string TypeName { get; set; }
    public string PropertyName { get; set; }

    private Boolean _isPresent;
    private bool? _isPropertyPresent = null;

    public Boolean IsPresent
    {
        get { return _isPresent; }
        set
        {
            _isPresent = value;
            if (_isPropertyPresent == null)
            {
                // Call into ApiInformation method to determine if property is present.
                _isPropertyPresent =
                ApiInformation.IsPropertyPresent(TypeName, PropertyName);
            }

            // If the property presence matches _isPresent then the trigger will be activated;
            SetActive(_isPresent == _isPropertyPresent);
        }
    }
}
```

The next step is setting up the visual state trigger in XAML so that two different visual states result based on the presence of the API. 

Windows 10, version 1607 introduces a new property on the [FrameworkElement](/uwp/api/windows.ui.xaml.frameworkelement) class called [AllowFocusOnInteraction](/uwp/api/windows.ui.xaml.frameworkelement.allowfocusoninteraction) that determines whether a control takes focus when  a user interacts with it. This is useful if you want to keep focus on a text box for data entry (and keep the touch keyboard showing) while the user clicks a button.

The trigger in this example checks if the property is present. If the property is present it sets the **AllowFocusOnInteraction** property on a Button to **false**; if the property isn’t present, the Button retains its original state. The TextBox is included to make it easier to see the effect of this property when you run the code.

**XAML**
```xaml
<Grid Background="{ThemeResource ApplicationPageBackgroundThemeBrush}">
    <StackPanel>
        <TextBox Width="300" Height="36"/>
        <!-- Button to set the new property on. -->
        <Button x:Name="testButton" Content="Test" Margin="12"/>
    </StackPanel>

    <VisualStateManager.VisualStateGroups>
        <VisualStateGroup x:Name="propertyPresentStateGroup">
            <VisualState>
                <VisualState.StateTriggers>
                    <!--Trigger will activate if the AllowFocusOnInteraction property is present-->
                    <local:IsPropertyPresentTrigger 
                        TypeName="Windows.UI.Xaml.FrameworkElement" 
                        PropertyName="AllowFocusOnInteraction" IsPresent="True"/>
                </VisualState.StateTriggers>
                <VisualState.Setters>
                    <Setter Target="testButton.AllowFocusOnInteraction" 
                            Value="False"/>
                </VisualState.Setters>
            </VisualState>
        </VisualStateGroup>
    </VisualStateManager.VisualStateGroups>
</Grid>
```

### Example 2: New enum value

This example shows how to set different enumeration values based on whether a value is present. It uses a custom state trigger to achieve the same result as the previous chat example. In this example, you use the new ChatWithoutEmoji input scope if the device is running Windows 10, version 1607, otherwise the **Chat** input scope is used. The visual states that use this trigger are set up in an *if-else* style where the input scope is chosen based on the presence of the new enum value.

**C#**
```csharp
class IsEnumPresentTrigger : StateTriggerBase
{
    public string EnumTypeName { get; set; }
    public string EnumValueName { get; set; }

    private Boolean _isPresent;
    private bool? _isEnumValuePresent = null;

    public Boolean IsPresent
    {
        get { return _isPresent; }
        set
        {
            _isPresent = value;

            if (_isEnumValuePresent == null)
            {
                // Call into ApiInformation method to determine if value is present.
                _isEnumValuePresent =
                ApiInformation.IsEnumNamedValuePresent(EnumTypeName, EnumValueName);
            }

            // If the property presence matches _isPresent then the trigger will be activated;
            SetActive(_isPresent == _isEnumValuePresent);
        }
    }
}
```

**XAML**
```xaml
<Grid Background="{ThemeResource ApplicationPageBackgroundThemeBrush}">

    <TextBox x:Name="messageBox"
     AcceptsReturn="True" TextWrapping="Wrap"/>


    <VisualStateManager.VisualStateGroups>
        <VisualStateGroup x:Name="EnumPresentStates">
            <!--if-->
            <VisualState x:Name="isPresent">
                <VisualState.StateTriggers>
                    <local:IsEnumPresentTrigger 
                        EnumTypeName="Windows.UI.Xaml.Input.InputScopeNameValue" 
                        EnumValueName="ChatWithoutEmoji" IsPresent="True"/>
                </VisualState.StateTriggers>
                <VisualState.Setters>
                    <Setter Target="messageBox.InputScope" Value="ChatWithoutEmoji"/>
                </VisualState.Setters>
            </VisualState>
            <!--else-->
            <VisualState x:Name="isNotPresent">
                <VisualState.StateTriggers>
                    <local:IsEnumPresentTrigger 
                        EnumTypeName="Windows.UI.Xaml.Input.InputScopeNameValue" 
                        EnumValueName="ChatWithoutEmoji" IsPresent="False"/>
                </VisualState.StateTriggers>
                <VisualState.Setters>
                    <Setter Target="messageBox.InputScope" Value="Chat"/>
                </VisualState.Setters>
            </VisualState>
        </VisualStateGroup>
    </VisualStateManager.VisualStateGroups>
</Grid>
```

## Related articles

- [Programming with extension SDKs](/uwp/extension-sdks/device-families-overview)
- [Dynamically detecting features with API contracts](version-adaptive-apps.md#api-contracts)
