---
title: Handle URI activation
description: Learn how to register an app to become the default handler for a Uniform Resource Identifier (URI) scheme name.
ms.assetid: 92D06F3E-C8F3-42E0-A476-7E94FD14B2BE
ms.date: 07/05/2018
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Handle URI activation

**Important APIs**

-   [**Windows.ApplicationModel.Activation.ProtocolActivatedEventArgs**](/uwp/api/Windows.ApplicationModel.Activation.ProtocolActivatedEventArgs)
-   [**Windows.UI.Xaml.Application.OnActivated**](/uwp/api/windows.ui.xaml.application.onactivated)

Learn how to register an app to become the default handler for a Uniform Resource Identifier (URI) scheme name. Both Windows desktop apps and Universal Windows Platform (UWP) apps can register to be a default handler for a URI scheme name. If the user chooses your app as the default handler for a URI scheme name, your app will be activated every time that type of URI is launched.

We recommend that you only register for a URI scheme name if you expect to handle all URI launches for that type of URI scheme. If you do choose to register for a URI scheme name, you must provide the end user with the functionality that is expected when your app is activated for that URI scheme. For example, an app that registers for the mailto: URI scheme name should open to a new e-mail message so that the user can compose a new e-mail. For more info on URI associations, see [Guidelines and checklist for file types and URIs](../files/index.md).

These steps show how to register for a custom URI scheme name, `alsdk://`, and how to activate your app when the user launches a `alsdk://` URI.

> [!NOTE]
> In UWP apps, certain URIs and file extensions are reserved for use by built-in apps and the operating system. Attempts to register your app with a reserved URI or file extension will be ignored. See [Reserved URI scheme names and file types](reserved-uri-scheme-names.md) for an alphabetic list of Uri schemes that you can't register for your UWP apps because they are either reserved or forbidden.

## Step 1: Specify the extension point in the package manifest

The app receives activation events only for the URI scheme names listed in the package manifest. Here's how you indicate that your app handles the `alsdk` URI scheme name.

1. In the **Solution Explorer**, double-click package.appxmanifest to open the manifest designer. Select the **Declarations** tab and in the **Available Declarations** drop-down, select **Protocol** and then click **Add**.

    Here is a brief description of each of the fields that you may fill in the manifest designer for the Protocol (see [**AppX Package Manifest**](/uwp/schemas/appxpackage/uapmanifestschema/element-uap-extension) for details):

| Field | Description |
|-------|-------------|
| **Logo** | Specify the logo that is used to identify the URI scheme name in the [Set Default Programs](/windows/desktop/shell/default-programs) on the **Control Panel**. If no Logo is specified, the small logo for the app is used. |
| **Display Name** | Specify the display name to identify the URI scheme name in the [Set Default Programs](/windows/desktop/shell/default-programs) on the **Control Panel**. |
| **Name** | Choose a name for the Uri scheme. |
|  | **Note**  The Name must be in all lower case letters. |
|  | **Reserved and forbidden file types** See [Reserved URI scheme names and file types](reserved-uri-scheme-names.md) for an alphabetic list of Uri schemes that you can't register for your UWP apps because they are either reserved or forbidden. |
| **Executable** | Specifies the default launch executable for the protocol. If not specified, the app's executable is used. If specified, the string must be between 1 and 256 characters in length, must end with ".exe", and cannot contain these characters: &gt;, &lt;, :, ", &#124;, ?, or \*. If specified, the **Entry point** is also used. If the **Entry point** isn't specified, the entry point defined for the app is used. |
| **Entry point** | Specifies the task that handles the protocol extension. This is normally the fully namespace-qualified name of a Windows Runtime type. If not specified, the entry point for the app is used. |
| **Start page** | The web page that handles the extensibility point. |
| **Resource group** | A tag that you can use to group extension activations together for resource management purposes. |
| **Desired View** (Windows-only) | Specify the **Desired View** field to indicate the amount of space the app's window needs when it is launched for the URI scheme name. The possible values for **Desired View** are **Default**, **UseLess**, **UseHalf**, **UseMore**, or **UseMinimum**.<br/>**Note**  Windows takes into account multiple different factors when determining the target app's final window size, for example, the preference of the source app, the number of apps on screen, the screen orientation, and so on. Setting **Desired View** doesn't guarantee a specific windowing behavior for the target app.<br/>**Mobile device family: Desired View** isn't supported on the mobile device family. |

2. Enter `images\Icon.png` as the **Logo**.
3. Enter `SDK Sample URI Scheme` as the **Display name**
4. Enter `alsdk` as the **Name**.
5. Press Ctrl+S to save the change to package.appxmanifest.

    This adds an [**Extension**](/uwp/schemas/appxpackage/appxmanifestschema/element-1-extension) element like this one to the package manifest. The **windows.protocol** category indicates that the app handles the `alsdk` URI scheme name.

```xml
    <Applications>
        <Application Id= ... >
            <Extensions>
                <uap:Extension Category="windows.protocol">
                  <uap:Protocol Name="alsdk">
                    <uap:Logo>images\icon.png</uap:Logo>
                    <uap:DisplayName>SDK Sample URI Scheme</uap:DisplayName>
                  </uap:Protocol>
                </uap:Extension>
          </Extensions>
          ...
        </Application>
   <Applications>
```

## Step 2: Add the proper icons

Apps that become the default for a URI scheme name have their icons displayed in various places throughout the system such as in the Default programs control panel. Include a 44x44 icon with your project for this purpose. Match the look of the app tile logo and use your app's background color rather than making the icon transparent. Have the logo extend to the edge without padding it. Test your icons on white backgrounds. See [App icons and logos](../design/style/app-icons-and-logos.md) for more details about icons.

## Step 3: Handle the activated event

The [**OnActivated**](/uwp/api/windows.ui.xaml.application.onactivated) event handler receives all activation events. The **Kind** property indicates the type of activation event. This example is set up to handle [**Protocol**](/uwp/api/Windows.ApplicationModel.Activation.ActivationKind) activation events.

```csharp
public partial class App
{
   protected override void OnActivated(IActivatedEventArgs args)
  {
      if (args.Kind == ActivationKind.Protocol)
      {
         ProtocolActivatedEventArgs eventArgs = args as ProtocolActivatedEventArgs;
         // TODO: Handle URI activation
         // The received URI is eventArgs.Uri.AbsoluteUri
      }
   }
}
```

```vb
Protected Overrides Sub OnActivated(ByVal args As Windows.ApplicationModel.Activation.IActivatedEventArgs)
   If args.Kind = ActivationKind.Protocol Then
      ProtocolActivatedEventArgs eventArgs = args As ProtocolActivatedEventArgs
      
      ' TODO: Handle URI activation
      ' The received URI is eventArgs.Uri.AbsoluteUri
 End If
End Sub
```

```cppwinrt
void App::OnActivated(Windows::ApplicationModel::Activation::IActivatedEventArgs const& args)
{
    if (args.Kind() == Windows::ApplicationModel::Activation::ActivationKind::Protocol)
    {
        auto protocolActivatedEventArgs{ args.as<Windows::ApplicationModel::Activation::ProtocolActivatedEventArgs>() };
        // TODO: Handle URI activation  
        auto receivedURI{ protocolActivatedEventArgs.Uri().RawUri() };
    }
}
```

```cpp
void App::OnActivated(Windows::ApplicationModel::Activation::IActivatedEventArgs^ args)
{
   if (args->Kind == Windows::ApplicationModel::Activation::ActivationKind::Protocol)
   {
      Windows::ApplicationModel::Activation::ProtocolActivatedEventArgs^ eventArgs =
          dynamic_cast<Windows::ApplicationModel::Activation::ProtocolActivatedEventArgs^>(args);
      
      // TODO: Handle URI activation  
      // The received URI is eventArgs->Uri->RawUri
   }
}
```

> [!NOTE]
> When launched via Protocol Contract, make sure that Back button takes the user back to the screen that launched the app and not to the app's previous content.

The following code programmatically launches the app via its URI:

```csharp
   // Launch the URI
   var uri = new Uri("alsdk:");
   var success = await Windows.System.Launcher.LaunchUriAsync(uri)
```

For more details about how to launch an app via a URI, see [Launch the default app for a URI](launch-default-app.md).

It is recommended that apps create a new XAML [**Frame**](/uwp/api/Windows.UI.Xaml.Controls.Frame) for each activation event that opens a new page. This way, the navigation backstack for the new XAML **Frame** will not contain any previous content that the app might have on the current window when suspended. Apps that decide to use a single XAML **Frame** for Launch and File Contracts should clear the pages on the **Frame** navigation journal before navigating to a new page.

When launched via Protocol activation, apps should consider including UI that allows the user to go back to the top page of the app.

## Remarks

Any app or website can use your URI scheme name, including malicious ones. So any data that you get in the URI could come from an untrusted source. We recommend that you never perform a permanent action based on the parameters that you receive in the URI. For example, URI parameters could be used to launch the app to a user's account page, but we recommend that you never use them to directly modify the user's account.

> [!NOTE]
> If you are creating a new URI scheme name for your app, be sure to follow the guidance in [RFC 4395](https://tools.ietf.org/html/rfc4395). This ensures that your name meets the standards for URI schemes.

> [!NOTE]
> When launched via Protocol Contract, make sure that Back button takes the user back to the screen that launched the app and not to the app's previous content.

We recommend that apps create a new XAML [**Frame**](/uwp/api/Windows.UI.Xaml.Controls.Frame) for each activation event that opens a new Uri target. This way, the navigation backstack for the new XAML **Frame** will not contain any previous content that the app might have on the current window when suspended.

If you decide that you want your apps to use a single XAML [**Frame**](/uwp/api/Windows.UI.Xaml.Controls.Frame) for Launch and Protocol Contracts, clear the pages on the **Frame** navigation journal before navigating to a new page. When launched via Protocol Contract, consider including UI into your apps that allows the user to go back to the top of the app.

## Related topics

### Complete sample app

- [Association launching sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/AssociationLaunching)

### Concepts

- [Default Programs](/windows/desktop/shell/default-programs)
- [File Type and URI Associations Model](/windows/desktop/w8cookbook/file-type-and-protocol-associations-model)

### Tasks

- [Launch the default app for a URI](launch-default-app.md)
- [Handle file activation](handle-file-activation.md)

### Guidelines

- [Guidelines for file types and URIs](../files/index.md)

### Reference

- [AppX Package Manifest](/uwp/schemas/appxpackage/uapmanifestschema/element-uap-extension)
- [Windows.ApplicationModel.Activation.ProtocolActivatedEventArgs](/uwp/api/Windows.ApplicationModel.Activation.ProtocolActivatedEventArgs)
- [Windows.UI.Xaml.Application.OnActivated](/uwp/api/windows.ui.xaml.application.onactivated)