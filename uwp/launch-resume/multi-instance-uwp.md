---
title: Create a multi-instance Universal Windows App
description: This topic describes how to write UWP apps that support multi-instancing.
keywords: multi-instance uwp
ms.date: 02/29/2024
ms.topic: article


ms.localizationpriority: medium
---
# Create a multi-instance Universal Windows App

This topic describes how to create multi-instance Universal Windows Platform (UWP) apps.

From Windows 10, version 1803 (10.0; Build 17134) onward, your UWP app can opt in to support multiple instances. If an instance of an multi-instance UWP app is running, and a subsequent activation request comes through, the platform will not activate the existing instance. Instead, it will create a new instance, running in a separate process.

> [!IMPORTANT]
> Multi-instancing is supported for JavaScript applications, but multi-instancing redirection is not. Since multi-instancing redirection is not supported for JavaScript applications, the [**AppInstance**](/uwp/api/windows.applicationmodel.appinstance) class is not useful for such applications.

## Opt in to multi-instance behavior

If you are creating a new multi-instance application, you can install the [Multi-Instance App Project Templates.VSIX](https://marketplace.visualstudio.com/items?itemName=AndrewWhitechapelMSFT.MultiInstanceApps), available from the [Visual Studio Marketplace](https://marketplace.visualstudio.com/). Once you install the templates, they will be available in the **New Project** dialog under **Visual C# > Windows Universal** (or **Other Languages > Visual C++ > Windows Universal**).

> [!Note]
> The Multi-Instance App Project template is no longer available. The VSIX template was a convenience, so you will need to modify the existing project instead, as described below.  Be certain to add the DISABLE_XAML_GENERATED_MAIN constant to the project build symbols, as this prevents the build from generating a default Main(). This allows used of a specially written app-specific version of Main().

Two templates are installed: **Multi-Instance UWP app**, which provides the template for creating a multi-instance app, and **Multi-Instance Redirection UWP app**, which provides additional logic that you can build on to either launch a new instance or selectively activate an instance that has already been launched. For example, perhaps you only want one instance at a time editing the same document, so you bring the instance that has that file open to the foreground rather than launching a new instance.

Both templates add `SupportsMultipleInstances` to the `package.appxmanifest` file. Note the namespace prefix `desktop4` and `iot2`: only projects that target the desktop, or Internet of Things (IoT) projects, support multi-instancing.

```xml
<Package
  ...
  xmlns:desktop4="http://schemas.microsoft.com/appx/manifest/desktop/windows10/4"
  xmlns:iot2="http://schemas.microsoft.com/appx/manifest/iot/windows10/2"  
  IgnorableNamespaces="uap mp desktop4 iot2">
  ...
  <Applications>
    <Application Id="App"
      ...
      desktop4:SupportsMultipleInstances="true"
      iot2:SupportsMultipleInstances="true">
      ...
    </Application>
  </Applications>
   ...
</Package>
```

## Multi-instance activation redirection

 Multi-instancing support for UWP apps goes beyond simply making it possible to launch multiple instances of the app. It allows for customization in cases where you want to select whether a new instance of your app is launched or an instance that is already running is activated. For example, if the app is launched to edit a file that is already being edited in another instance, you may want to redirect the activation to that instance instead of opening up another instance that  that is already editing the file.

To see it in action, watch this video about Creating multi-instance UWP apps.

> [!VIDEO https://www.youtube.com/embed/0oZ2vrc1-fc]

The **Multi-Instance Redirection UWP app** template adds `SupportsMultipleInstances` to the package.appxmanifest file as shown above, and also adds a **Program.cs** (or **Program.cpp**, if you are using the C++ version of the template) to your project that contains a `Main()` function. The logic for redirecting activation goes in the `Main` function. The template for **Program.cs** is shown below.

The [**AppInstance.RecommendedInstance**](/uwp/api/windows.applicationmodel.appinstance.recommendedinstance) property represents the shell-provided preferred instance for this activation request, if there is one (or `null` if there isn't one). If the shell provides a preference, then you can redirect activation to that instance, or you can ignore it if you choose.

``` csharp
public static class Program
{
    // This example code shows how you could implement the required Main method to
    // support multi-instance redirection. The minimum requirement is to call
    // Application.Start with a new App object. Beyond that, you may delete the
    // rest of the example code and replace it with your custom code if you wish.

    static void Main(string[] args)
    {
        // First, we'll get our activation event args, which are typically richer
        // than the incoming command-line args. We can use these in our app-defined
        // logic for generating the key for this instance.
        IActivatedEventArgs activatedArgs = AppInstance.GetActivatedEventArgs();

        // If the Windows shell indicates a recommended instance, then
        // the app can choose to redirect this activation to that instance instead.
        if (AppInstance.RecommendedInstance != null)
        {
            AppInstance.RecommendedInstance.RedirectActivationTo();
        }
        else
        {
            // Define a key for this instance, based on some app-specific logic.
            // If the key is always unique, then the app will never redirect.
            // If the key is always non-unique, then the app will always redirect
            // to the first instance. In practice, the app should produce a key
            // that is sometimes unique and sometimes not, depending on its own needs.
            string key = Guid.NewGuid().ToString(); // always unique.
                                                    //string key = "Some-App-Defined-Key"; // never unique.
            var instance = AppInstance.FindOrRegisterInstanceForKey(key);
            if (instance.IsCurrentInstance)
            {
                // If we successfully registered this instance, we can now just
                // go ahead and do normal XAML initialization.
                global::Windows.UI.Xaml.Application.Start((p) => new App());
            }
            else
            {
                // Some other instance has registered for this key, so we'll 
                // redirect this activation to that instance instead.
                instance.RedirectActivationTo();
            }
        }
    }
}
```

`Main()` is the first thing that runs. It runs before [**OnLaunched**](/uwp/api/windows.ui.xaml.application#Windows_UI_Xaml_Application_OnLaunched_Windows_ApplicationModel_Activation_LaunchActivatedEventArgs_) and [**OnActivated**](/uwp/api/windows.ui.xaml.application#Windows_UI_Xaml_Application_OnActivated_Windows_ApplicationModel_Activation_IActivatedEventArgs_). This allows you to determine whether to activate this, or another instance, before any other initialization code in your app runs.

The code above determines whether an existing, or new, instance of your application is activated. A key is used to determine whether there is an existing instance that you want to activate. For example, if your app can be launched to [Handle file activation](./handle-file-activation.md), you might use the file name as a key. Then you can check whether an instance of your app is already registered with that key and activate it instead of opening a new instance. This is the idea behind the code: `var instance = AppInstance.FindOrRegisterInstanceForKey(key);`

If an instance registered with the key is found, then that instance is activated. If the key is not found, then the current instance (the instance that is currently running `Main`) creates its application object  and starts running.

## Background tasks and multi-instancing

- Out-of-proc background tasks support multi-instancing. Typically, each new trigger results in a new instance of the background task (although technically speaking multiple background tasks may run in same host process). Nevertheless, a different instance of the background task is created.
- In-proc background tasks do not support multi-instancing.
- Background audio tasks do not support multi-instancing.
- When an app registers a background task, it usually first checks to see if the task is already registered and then either deletes and re-registers it, or does nothing in order to keep the existing registration. This is still the typical behavior with multi-instance apps. However, a multi-instancing app may choose to register a different background task name on a per-instance basis. This will result in multiple registrations for the same trigger, and multiple background task instances will be activated when the trigger fires.
- App-services launch a separate instance of the app-service background task for every connection. This remains unchanged for multi-instance apps, that is each instance of a multi-instance app will get its own instance of the  app-service background task.

## Additional considerations

- Multi-instancing is supported by UWP apps that target desktop and Internet of Things (IoT) projects.
- To avoid race-conditions and contention issues, multi-instance apps need to take steps to partition/synchronize access to settings, app-local storage, and any other resource (such as user files, a data store, and so on) that can be shared among multiple instances. Standard synchronization mechanisms such as mutexes, semaphores, events, and so on, are available.
- If the app has `SupportsMultipleInstances` in its Package.appxmanifest file, then its extensions do not need to declare `SupportsMultipleInstances`.
- If you add `SupportsMultipleInstances` to any other extension, apart from background tasks or app-services, and the app that hosts the extension doesn't also declare `SupportsMultipleInstances` in its Package.appxmanifest file, then a schema error is generated.
- Apps can use the [**ResourceGroup**](./declare-background-tasks-in-the-application-manifest.md) declaration in their manifest to group multiple background tasks into the same host. This conflicts with multi-instancing, where each activation goes into a separate host. Therefore an app cannot declare both `SupportsMultipleInstances` and `ResourceGroup` in their manifest.

## Sample

See [Multi-Instance sample](https://github.com/Microsoft/AppModelSamples/tree/master/Samples/BananaEdit) for an example of multi-instance activation redirection.

## See also

[AppInstance.FindOrRegisterInstanceForKey](/uwp/api/windows.applicationmodel.appinstance#Windows_ApplicationModel_AppInstance_FindOrRegisterInstanceForKey_System_String_)
[AppInstance.GetActivatedEventArgs](/uwp/api/windows.applicationmodel.appinstance#Windows_ApplicationModel_AppInstance_GetActivatedEventArgs)
[AppInstance.RedirectActivationTo](/uwp/api/windows.applicationmodel.appinstance#Windows_ApplicationModel_AppInstance_RedirectActivationTo)
[Handle app activation](./activate-an-app.md)
