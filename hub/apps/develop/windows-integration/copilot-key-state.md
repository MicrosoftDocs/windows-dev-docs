---
title: Handle Microsoft Copilot hardware key state changes
description: Learn how to register to be activated and receive notifications when the Microsoft Copilot hardware key or Windows key + C is pressed. 
ms.topic: how-to
ms.date: 10/25/2024
ms.localizationpriority: medium
---



# Handle Microsoft Copilot hardware key state changes

This article describes how apps can register to be activated and receive notifications when the Microsoft Copilot hardware key or Windows key + C is pressed, pressed and held, and released. This feature enables apps to perform different actions depending on which key state change is detected. For example, an app may perform normal activation when the key is single-pressed, but take a screenshot when the key is pressed and held. Or, an app may begin recording audio and show a status indicator that audio is being recorded when the key is pressed and held, and then stop recording audio when the key is released. The key must be pressed and held for at least 300 ms to move into the held state.

This feature extends the features of a basic Microsoft Copilot hardware key provider, which simply registers to be launched when the hardware key is pressed. For more information, see [Microsoft Copilot hardware key providers](microsoft-copilot-key-provider.md).

The rest of this article will walk through creating a simple C# WinUI 3 app that responds to activation initiated by a single press or a press and hold and release of the Microsoft Copilot button.

## Create a new project

In Visual Studio, create a new project. For this example, in the **Create a new project** dialog, set the language filter to C# and the project type to WinUI and then select the "WinUI Blank App (Packaged)".

## Add a property to track the Microsoft Copilot key pressed state

In this example, we will create a property called **State** that we will use to display the current activation state in the UI. In `MainWindow.xaml.cs`, inside the definition of **MainWindow** add the following code to create a string property that we can bind to in our XAML file.

```csharp
// MainWindow.xaml.cs
public event PropertyChangedEventHandler? PropertyChanged;

private void OnPropertyChanged([CallerMemberName] string propertyName = "State")
{
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}

public void SetState(string state)
{
    State = state;
}

private string _state;
public string State
{
    get => _state;
    set
    {
        if (_state != value)
        {
            _state = value;
            OnPropertyChanged();
        }
    }
}
```

Add a **TextBlock** control to the UI to show the current activation state of the app. Replace the default **StackPanel** element in MainWindow.xaml with the following code.

```xaml
<!-- MainWindow.xaml -->
<StackPanel Orientation="Horizontal" HorizontalAlignment="Center" VerticalAlignment="Center">
    <TextBlock Name="KeyStateText" Text="{x:Bind State, Mode=OneWay}" />
</StackPanel>
```

Finally, update the **MainWindow** constructor to take an argument that will set the **State** property when the window is created.

```csharp
// MainWindow.xaml.cs
public MainWindow(string state)
{
    this.InitializeComponent();

    SetState(state);
}
```


## Register for URI activation

The system launches Microsoft Copilot hardware key providers using URI activation. Register a launch protocol by adding the [uap:Protocol](/uwp/schemas/appxpackage/uapmanifestschema/element-uap-protocol) element to your app manifest. For more information about how to register as the default handler for a URI scheme, see [Handle URI activation](/windows/apps/develop/launch/handle-uri-activation).

The following example shows the **uap:Extension** registering the URI scheme "myapp-copilothotkey".

```xml
<!-- Package.appxmanifest -->
...
  xmlns:uap3="http://schemas.microsoft.com/appx/manifest/uap/windows10/3"
...

<Extensions> 
  ...
  <uap:Extension Category="windows.protocol">
    <uap:Protocol Name="myapp-copilothotkey"> <!-- app-defined protocol name -->
      <uap:DisplayName>SDK Sample URI Scheme</uap:DisplayName>
    </uap:Protocol>
  </uap:Extension>
  ...
```
 
## Microsoft Copilot hardware key app extension

An app must be packaged in order to register as a Microsoft Copilot hardware key provider. For information on app packaging, see [An overview of Package Identity in Windows app](/windows/apps/desktop/modernize/package-identity-overview). Microsoft Copilot hardware key providers declare their registration information within the [uap3:AppExtension](/uwp/schemas/appxpackage/uapmanifestschema/element-uap3-appextension-manual). The **Name** attribute of the extension must be set to "com.microsoft.windows.copilotkeyprovider". To support the key state changes, apps must provide some additional entries to their **uap3:AppExtension** declaration.

Inside of the **uap3:AppExtension** element, add a [uap3:Properties](/uwp/schemas/appxpackage/uapmanifestschema/element-uap3-properties-manual) element with child elements **PressAndHoldStart** and **PressAndHoldStop**. The contents of these elements should be the URI of the protocol scheme registered in the manifest in the previous step. The query string arguments specify whether the URI is being launched because the user pressed and held the hot key or because the user released the hot key. The app uses these query string values during app activation to determine the correct action to take. Specifying the **SingleTap** element is optional but can be useful to determine if the app was launched from the Copilot hardware key.

```xml
<!-- Package.appxmanifest -->

<Extensions> 
  ...
  <uap3:Extension Category="windows.appExtension"> 
    <uap3:AppExtension Name="com.microsoft.windows.copilotkeyprovider"  
      Id="MyAppId" 
      DisplayName="App display name" 
      Description="App description" 
      PublicFolder="Public"> 
      <uap3:Properties> 
        <SingleTap>myapp-copilothotkey://?state=Tap</SingleTap>
        <PressAndHoldStart>myapp-copilothotkey://?state=Down</PressAndHoldStart> 
        <PressAndHoldStop>myapp-copilothotkey://?state=Up</PressAndHoldStop>
      </uap3:Properties> 
    </uap3:AppExtension>
  </uap3:Extension> 
  ...
```

## Handle URI activation

To detect whether the app was activated via URI activation, call [AppInstance.GetActivatedEventArgs](/windows/windows-app-sdk/api/winrt/microsoft.windows.applifecycle.appinstance.getactivatedeventargs) and check to see if the value of the [AppActivationArguments.Kind](/windows/windows-app-sdk/api/winrt/microsoft.windows.applifecycle.appactivationarguments.kind) property is [Protocol](/windows/windows-app-sdk/api/winrt/microsoft.windows.applifecycle.extendedactivationkind). If the app was launched via protocol activation, check to see if the URI scheme is the same as the protocol name you specified in your app manifest. If all of these tests pass, then you know that your app was activated by the user pressing the Copilot hardware key. At this point you can parse the URI query string and get the *state* parameter, which will have the values you specified in the **PressAndHoldStart** and **PressAndHoldStop** elements in the app manifest.

```csharp
// App.xaml.cs

protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
{
    var eventargs = AppInstance.GetCurrent().GetActivatedEventArgs();
    string state = "";
    if ((eventargs != null) && (eventargs.Kind == ExtendedActivationKind.Protocol))
    {
        var protocolArgs = (Windows.ApplicationModel.Activation.ProtocolActivatedEventArgs)eventargs.Data;
        WwwFormUrlDecoder decoderEntries = new WwwFormUrlDecoder(protocolArgs.Uri.Query);
        state = Uri.UnescapeDataString(decoderEntries.GetFirstValueByName("state"));
    }
    state = (state == "") ? "Launched" : state;

    m_window = new MainWindow(state);
    m_window.Activate();
}
```

> [!IMPORTANT]
> Note that, by default, WinUI apps are multi-instanced, which means that a new instance will be launched whenever the Microsoft Copilot hot key is pressed or released. This may be the desired behavior for many providers, but if you would prefer, you can update you app to use a single instance. For more information, see [Create a single-instanced WinUI app with C#](/windows/apps/windows-app-sdk/applifecycle/applifecycle-single-instance).

## Handle fast path invocation

In addition to URI activation, apps can register to support fast path invocation in which a running app receives messages about Copilot hardware app through window messages. For a currently-running app, this invocation method is faster than URI activation and will provide a better user experience, since the app can begin listening for speech more quickly after the key is pressed and held.

### Update the app manifest file to support fast path invocation

To add support for fast path invocation, update the "com.microsoft.windows.copilotkeyprovider" extension to add the *MessageWParam* attribute to the **SingleTap**, **PressAndHoldStart**, and **PressAndHoldStop** elements. Each *MessageWParam* value must be a unique 32-bit integer, but the values used are chosen by the app. This example uses values of 0, 1, and 2, respectively. These values will be used later in the example when they are passed in the *wParam* parameter of a Windows message to determine the current pressed state of the Windows Copilot hardware key. 

```xml
<!-- Package.appxmanifest -->

<uap3:Extension Category="windows.appExtension">
  <uap3:AppExtension Name="com.microsoft.windows.copilotkeyprovider"
    Id="MyAppId"
    DisplayName="App display name"
    Description="App description"
    PublicFolder="Public">
    <uap3:Properties>
      <SingleTap MessageWParam="0">myapp-copilothotkey://?state=Tap</SingleTap>
      <PressAndHoldStart MessageWParam="1">myapp-copilothotkey://?state=Down</PressAndHoldStart>
      <PressAndHoldStop MessageWParam="2">myapp-copilothotkey://?state=Up</PressAndHoldStop>
    </uap3:Properties>
  </uap3:AppExtension>
</uap3:Extension>
```

### Access win32 APIs for window registration

Fast path activation is enabled by setting a property on the [IPropertyStore](/windows/win32/api/propsys/nn-propsys-ipropertystore) associated with one of the app's windows. To do this requires access to some native Win32 APIs. This walkthrough will use the CsWin32 library, which automates the generation of C# bindings and is available as a NuGet package.

In Visual Studio, in **Solution Explorer**, right-click on your project file and select **Manage NuGet packages...**. On the **Browse** tab of the NuGet package manager, search for "cswin32" and select the "Microsoft.Windows.CsWin32" package and click **Install*.

After the package is installed, add a new text file in your project directory and name it "NativeMethods.txt". The CsWin32 tool will look in this file for a list of the Win32 APIs that it will generate bindings for. Put the following API names in "NativeMethods.txt".

`SUBCLASSPROC`

`SHGetPropertyStoreForWindow`

`IPropertyStore`

`SetWindowSubclass`

`DefSubclassProc`

### Register the window for Microsoft Copilot fastpath invocation

Next we will update the **MainWindow** class to register the window to receive fastpath invocations from the Copilot hardware key. 

First, call [GetWindowHandle](/windows/windows-app-sdk/api/win32/microsoft.ui.xaml.window/nf-microsoft-ui-xaml-window-iwindownative-get_windowhandle) to get an [HWND](/windows/win32/winprog/windows-data-types) handle to the **MainWindow**. Call [SHGetPropertyStoreForWindow](/windows/win32/api/shellapi/nf-shellapi-shgetpropertystoreforwindow) to get the **IPropertyStore** for the window. Create a new [PROPERTYKEY](/windows/win32/api/wtypes/ns-wtypes-propertykey) and set the *fmtid* member to the GUID for Windows Copilot fastpath activation. Set the value of the property to an app-defined value, that will be passed back to the app from the system when the hardware key state changes. The app-defined value is the windows message ID which must be in the WM_APP range. For more information, see [WM_APP](/windows/win32/winmsg/wm-app). Call [SetValue](/windows/win32/api/propsys/nf-propsys-ipropertystore-setvalue) and then call [Commit](/windows/win32/api/propsys/nf-propsys-ipropertystore-commit) to commit the change to the property store. 

Finally, create a [SUBCLASSPROC](/windows/win32/api/commctrl/nc-commctrl-subclassproc) callback that will be called when the hardware key state changes. **WindowSubClass** is the callback implementation that will be shown in the next step. Call [SetWindowSubclass](/windows/win32/api/commctrl/nf-commctrl-setwindowsubclass) to register the callback.

```csharp
private HWND hWndMain;
private Windows.Win32.UI.Shell.SUBCLASSPROC SubClassDelegate;
public const int WM_COPILOT = 0x8000 + 0x0001;

public MainWindow(string state)
{
    this.InitializeComponent();

    hWndMain = (HWND)WinRT.Interop.WindowNative.GetWindowHandle(this);

    var propertyStoreGUID = new Guid("886D8EEB-8CF2-4446-8D02-CDBA1DBDCF99");
    var hr = PInvoke.SHGetPropertyStoreForWindow(hWndMain, in propertyStoreGUID, out var propertyStore);
    var key = new PROPERTYKEY();
    var copilotFastpathGUID = new Guid("38652BCA-4329-4E74-86F9-39CF29345EEA");
    key.fmtid = copilotFastpathGUID;
    key.pid = 0x00000002;
    var value = new PROPVARIANT();
    value.Anonymous.Anonymous.vt = VARENUM.VT_UINT;
    value.Anonymous.decVal = WM_COPILOT;
    ((IPropertyStore)propertyStore).SetValue(in key, in value);
    ((IPropertyStore)propertyStore).Commit();

    SubClassDelegate = new Windows.Win32.UI.Shell.SUBCLASSPROC(WindowSubClass);
    bool bRet = PInvoke.SetWindowSubclass(hWndMain, SubClassDelegate, 0, 0);

    SetState(state);
}
```

### Implement the window subclass callback

The last step in this example is implementing the window subclass callback that will be called whenever the app is running and the state of the Windows Copilot hardware key changes. In this example, we check that the window message is the **WM_COPILOT** value that we specified when setting the property store value in the previous step. Then we check the value of the *wParam* argument to see which of the values we specified with the **MessageWParam** attributes in the app manifest has been passed in. **SetState** is called to update the UI with the current state.

```csharp
private LRESULT WindowSubClass(HWND hWnd, uint uMsg, WPARAM wParam, LPARAM lParam, nuint uIdSubclass, nuint dwRefData)
{
    switch (uMsg)
    {
        case WM_COPILOT:
        {
            switch (wParam.Value)
            {
                case 0:
                    SetState("SingleTap");
                    break;
                case 1:
                    SetState("PressAndHold START");
                    break;
                case 2:
                    SetState("PressAndHold END");
                    break;
            }
        }
        break;

    }
    return PInvoke.DefSubclassProc((HWND)hWnd, uMsg, wParam, lParam);

}
```

## Sign your Windows Copilot hardware key provider

Provider apps must be signed in order to be enabled as a target of the Microsoft Copilot hardware key. For information on packaging and signing your app, see [Package a desktop or UWP app in Visual Studio](/windows/msix/package/packaging-uwp-apps).
