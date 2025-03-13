---
title: Microsoft Copilot hardware key PressAndHoldAndRelease
description: Learn how to register to be activated and receive notifications when the Microsoft Copilot hardware key or Windows + C is pressed. 
ms.topic: article
ms.date: 10/25/2024
ms.localizationpriority: medium
---



# Microsoft Copilot hardware key PressAndHoldAndRelease

This article describes how apps can register to be activated and receive notifications when the Microsoft Copilot hardware key or Windows + C is pressed. This feature enables the following user scenario.

1. The user presses the Microsoft Copilot Hardware key or Windows + C and holds it for the system-defined 700 ms time window.
1. The system launches the Copilot Key provider app, letting the app know that the user is pressing and holding the key.
1. The app begins recording audio and shows a window indicating to the user that audio is being recorded.
1. The user speaks and then releases the key.
1. The app is notified that the key has been released, prompting it to process the recorded speech and taking additional actions based on the user's words.

This feature extends the features of a basic Microsoft Copilot hardware key provider, which simply registers to be launched when the hardware key is pressed. For more information, see [Microsoft Copilot hardware key providers](microsoft-copliot-key-provider.md).

## Register for URI activation

The system launches Microsoft Copilot hardware key providers that implement PressAndHoldAndRelease using URI activation. Register a launch protocol by adding the [uap:Protocol](/uwp/schemas/appxpackage/uapmanifestschema/element-uap-protocol) element to your app manifest. For more information about how to register as the default handler for a URI scheme, see [Handle URI activation](/windows/apps/develop/launch/handle-uri-activation).

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

An app must be packaged in order to register as a Microsoft Copilot hardware key provider. For information on app packaging, see [An overview of Package Identity in Windows app](/windows/apps/desktop/modernize/package-identity-overview). Microsoft Copilot hardware key providers declare their registration information within the [uap3:AppExtension](/uwp/schemas/appxpackage/uapmanifestschema/element-uap3-appextension-manual). The **Name** attribute of the extension must be set to "com.microsoft.windows.copilotkeyprovider". To support the PressAndHoldAndRelease feature, apps must provide some additional entries to their **uap3:AppExtension** declaration.

Inside of the **uap3:AppExtension** element, add a [uap3:Properties](/uwp/schemas/appxpackage/uapmanifestschema/element-uap3-properties-manual) element with child elements **PressAndHoldStart** and **PressAndHoldStart**. The contents of these elements should be the URI of the protocol scheme registered in the manifest in the previous step. The query string arguments specify whether the URI is being launched because the user pressed and held the hot key or because the user released the hot key. The app uses these query string values during app activation to determine the correct action to take.

The following example shows a Copilot hot key provider registration with support for PressAndHoldAndRelease.

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
        <PressAndHoldStart>myapp-copilothotkey:?state=Down</PressAndHoldStart> 
        <PressAndHoldStop>myapp-copilothotkey:?state=Up</PressAndHoldStop> 
      </uap3:Properties> 
    </ uap3:AppExtension> 
  </uap3:Extension> 
  ...
```

## Handle URI activation

```csharp
// App.xaml.cs

private void OnActivated()
{

    AppActivationArguments args = AppInstance.GetCurrent().GetActivatedEventArgs();
    if (args.Kind == ExtendedActivationKind.Launch)
    {
        // launched by other means (possibly by copilot button press) 
        // implement primary UI 

    }
    else if (args.Kind == ExtendedActivationKind.Protocol)
    {
        var protocolArgs = (Windows.ApplicationModel.Activation.ProtocolActivatedEventArgs)args.Data;
        WwwFormUrlDecoder decoderEntries = new WwwFormUrlDecoder(protocolArgs.Uri.Query);
        string state = Uri.UnescapeDataString(decoderEntries.GetFirstValueByName("state"));

        if (state == "Down")
        {
            // Implement press and hold begin activation logic: 
            //  Start microphone capture 
            //  Show no activate a window indicating audio capture is happening 
        }
        else if (state == "Up")
        {
            // Implemented press and hold finished logic 
            //  Stop microphone capture 
            //  Indicate audio capture has stopped 
            //  Send the audio to the LLM 
        }

    }
}
```

## Handle fast path invocation

```xml
<!-- Package.appxmanifest -->

<uap3:Extension Category="windows.appExtension">
  <uap3:AppExtension Name="com.microsoft.windows.copilotkeyprovider"
    Id="MyAppId"
    DisplayName="App display name"
    Description="App description"
    PublicFolder="Public">
    <uap3:Properties>
      <SingleTap FastPathValue="0"/>
      <PressAndHoldStart FastPathValue="1">myapp-copilothotkey://?state=Down</PressAndHoldStart>
      <PressAndHoldStop FastPathValue="2">myapp-copilothotkey://?state=Up</PressAndHoldStop>
    </uap3:Properties>
  </uap3:AppExtension>
</uap3:Extension>
```

```csharp
// App.xaml.cs

protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
{   
    ...
    var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(m_window);

    IPropertyStore propertyStore;
    int hr = SHGetPropertyStoreForWindow(hwnd, ref IID_IPropertyStore, out propertyStore);
    PropVariant propVar = new PropVariant();
    propVar.vt = (ushort)VarEnum.VT_UI4;
    propVar.ulVal = MainWindow.CopilotHotKeyFastpathMessage;
    propertyStore.SetValue(PKEY_Shell_CopilotKeyProviderFastPathMessage, ref propVar);
    propertyStore.Commit();
    ...
}
```

```csharp
// MainWindow.xaml.cs

public MainWindow()
{
    ...
    var hWndMain = WinRT.Interop.WindowNative.GetWindowHandle(this);
    Microsoft.UI.Windowing.AppWindow appWindow = AppWindow;
    
    SubClassDelegate = new SUBCLASSPROC(WindowSubClass);
    bool bRet = SetWindowSubclass((int)appWindow.Id.Value, SubClassDelegate, 0, 0);
    ...
}
```

```csharp
private int WindowSubClass(IntPtr hWnd, uint uMsg, IntPtr wParam,
           IntPtr lParam, IntPtr uIdSubclass, uint dwRefData)
{
    switch (uMsg)
    {
        case CopilotHotKeyFastpathMessage:
        {
            switch (lParam)
            {
                case 0:
                    // Copilot key press, show our window 
                    PInvoke.ShowWindow((HWND)hWnd, SHOW_WINDOW_CMD.SW_SHOW);
                    break;
                case 1:
                    // Copilot key press and hold start, show our window without activation 
                    // and start listening 
                    PInvoke.ShowWindow((HWND)hWnd, SHOW_WINDOW_CMD.SW_SHOWNA);
                    // Start microphone recording 
                    break;
                case 2:
                    // Copilot key press and hold stop, stop listening and send query 
                    break;
            }
        }
        return 0;
    }

    return PInvoke.DefSubclassProc((HWND)hWnd, uMsg, wParam, lParam);

}
```

