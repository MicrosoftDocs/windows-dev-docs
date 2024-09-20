---
description: Describes how to use app instancing features with the app lifecycle API in WinUI with C# and the Windows App SDK.
title: How to create a single-instanced WinUI app with C#
ms.topic: how-to
ms.date: 09/20/2024
keywords: AppLifecycle, Windows, ApplicationModel, instancing, single instance, multi instance, winui, windows app sdk, c#
#customer intent: As a Windows developer, I want to learn how to create a single-instanced WinUI 3 app so that I can ensure only one instance of my app is running at a time.
---

# Create a single-instanced WinUI app with C#

This how-to demonstrates how to create a single-instanced WinUI 3 app with C# and the Windows App SDK. Single-instanced apps only allow one instance of the app running at a time. WinUI apps are multi-instanced by default. They allow you to launch multiple instances of the same app simultaneously. That's referred to a multiple instances. However, you may want to implement single-instancing based on the use case of your app. Attempting to launch a second instance of a single-instanced app will only result in the first instance’s main window being activated instead. This tutorial demonstrates how to implement single-instancing in a WinUI app.

In this article, you will learn how to:

> [!div class="checklist"]
> - Turn off XAML's generated `Program` code
> - Define customized `Main` method for redirection
> - Test single-instancing after app deployment

## Pre-requisites

This tutorial uses Visual Studio and builds on the WinUI blank app template. If you're new to WinUI development, you can get set up by following the instructions in [Get started with WinUI](../../get-started/start-here.md). There you'll install Visual Studio, configure it for developing apps with WinUI while ensuring you have the latest version of WinUI and the Windows App SDK, and create a Hello World project.

When you've done that, come back here to learn how to turn your "Hello World" project into a single-instanced app.

> [!NOTE]
> This how-to is based on the [Making the app single-instanced (Part 3)](https://blogs.windows.com/windowsdeveloper/2022/01/28/making-the-app-single-instanced-part-3/) blog post from a Windows blog series on WinUI 3. The code for those articles is available on [GitHub](https://github.com/jingwei-a-zhang/WinAppSDK-DrumPad).

## Disable auto-generated Program code

We need to check for redirection as early as possible, before creating any windows. To do this, we must define the symbol “DISABLE_XAML_GENERATED_MAIN” in the project file. Follow these steps to disable the auto-generated Program code:

1. Right-click on the project name in Solution Explorer and select **Edit Project File**.
1. Define the **DISABLE_XAML_GENERATED_MAIN** symbol for each configuration and platform. Add the following XML to the project file:

   ```xml
   <propertygroup condition="'$(Configuration)|$(Platform)'=='Debug|x64'">
     <defineconstants>DISABLE_XAML_GENERATED_MAIN</defineconstants>
   </propertygroup>
   <propertygroup condition="'$(Configuration)|$(Platform)'=='Debug|x86'">
     <defineconstants>DISABLE_XAML_GENERATED_MAIN</defineconstants>
   </propertygroup>
   <propertygroup condition="'$(Configuration)|$(Platform)'=='Release|x86'">
     <defineconstants>DISABLE_XAML_GENERATED_MAIN</defineconstants>
   </propertygroup>
   <propertygroup condition="'$(Configuration)|$(Platform)'=='Release|x64'">
     <defineconstants>DISABLE_XAML_GENERATED_MAIN</defineconstants>
   </propertygroup>
   <propertygroup condition="'$(Configuration)|$(Platform)'=='Debug|arm64'">
     <defineconstants>DISABLE_XAML_GENERATED_MAIN</defineconstants>
   </propertygroup>
   <propertygroup condition="'$(Configuration)|$(Platform)'=='Release|arm64'">
     <defineconstants>DISABLE_XAML_GENERATED_MAIN</defineconstants>
   </propertygroup>
   ```

Adding the **DISABLE_XAML_GENERATED_MAIN** symbol will disable the auto-generated Program code for your project.

## Define a Program class with a Main method

A customized Program.cs file must be created instead of running the default Main method. The code added to the **Program** class enables the app to check for redirection, which isn't the default behavior of WinUI apps.

1. Navigate to Solution Explorer, right-click on the project name, and select **Add | Class**.
1. Name the new class `Program.cs` and select **Add**.
1. Add the following namespaces to the Program class, replacing any existing namespaces:

   ```csharp
   using System;
   using System.Diagnostics;
   using System.Runtime.InteropServices;
   using System.Threading;
   using System.Threading.Tasks;
   using Microsoft.UI.Dispatching;
   using Microsoft.UI.Xaml;
   using Microsoft.Windows.AppLifecycle;
   ```

1. Replace the empty **Program** class with the following:

   ```csharp
   public class Program
   {
       [STAThread]
       static int Main(string[] args)
       {
           WinRT.ComWrappersSupport.InitializeComWrappers();
           bool isRedirect = DecideRedirection();

           if (!isRedirect)
           {
               Application.Start((p) =>
               {
                   var context = new DispatcherQueueSynchronizationContext(
                       DispatcherQueue.GetForCurrentThread());
                   SynchronizationContext.SetSynchronizationContext(context);
                   _ = new App();
               });
           }

           return 0;
       }
   }
   ```

   The **Main** method determines whether the app should redirect to the first instance or start a new instance after calling **DecideRedirection**, which we will define next.

1. Define the **DecideRedirection** method below the **Main** method:

   ```csharp
   private static bool DecideRedirection()
   {
       bool isRedirect = false;
       AppActivationArguments args = AppInstance.GetCurrent().GetActivatedEventArgs();
       ExtendedActivationKind kind = args.Kind;
       AppInstance keyInstance = AppInstance.FindOrRegisterForKey("MySingleInstanceApp");

       if (keyInstance.IsCurrent)
       {
           keyInstance.Activated += OnActivated;
       }
       else
       {
           isRedirect = true;
           RedirectActivationTo(args, keyInstance);
       }

       return isRedirect;
   }
   ```

   **DecideRedirection** determines if the app has been registered by registering a unique key that represents your app instance. Based on the result of key registration, it can determine if there's a current instance of the app running. After making the determination, the method knows whether to redirect or allow the app to continue launching the new instance. The **RedirectActivationTo** method is called if redirection is necessary.

1. Next, let's create the RedirectActivationTo method below the DecideRedirection method, along with the required DllImport statements. Add the following code to the Program class:

   ```csharp
   [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
   private static extern IntPtr CreateEvent(
       IntPtr lpEventAttributes, bool bManualReset,
       bool bInitialState, string lpName);

   [DllImport("kernel32.dll")]
   private static extern bool SetEvent(IntPtr hEvent);

   [DllImport("ole32.dll")]
   private static extern uint CoWaitForMultipleObjects(
       uint dwFlags, uint dwMilliseconds, ulong nHandles,
       IntPtr[] pHandles, out uint dwIndex);

   [DllImport("user32.dll")]
   static extern bool SetForegroundWindow(IntPtr hWnd);

   private static IntPtr redirectEventHandle = IntPtr.Zero;

   // Do the redirection on another thread, and use a non-blocking
   // wait method to wait for the redirection to complete.
   public static void RedirectActivationTo(AppActivationArguments args,
                                           AppInstance keyInstance)
   {
       redirectEventHandle = CreateEvent(IntPtr.Zero, true, false, null);
       Task.Run(() =>
       {
           keyInstance.RedirectActivationToAsync(args).AsTask().Wait();
           SetEvent(redirectEventHandle);
       });

       uint CWMO_DEFAULT = 0;
       uint INFINITE = 0xFFFFFFFF;
       _ = CoWaitForMultipleObjects(
          CWMO_DEFAULT, INFINITE, 1,
          [redirectEventHandle], out uint handleIndex);

       // Bring the window to the foreground
       Process process = Process.GetProcessById((int)keyInstance.ProcessId);
       SetForegroundWindow(process.MainWindowHandle);
   }
   ```

   The **RedirectActivationTo** method is responsible for redirecting the activation to the first instance of the app. It creates an event handle, starts a new thread to redirect the activation, and waits for the redirection to complete. After the redirection is complete, the method brings the window to the foreground.

1. Finally, define the helper method **OnActivated** below the **DecideRedirection** method:

   ```csharp
   private static void OnActivated(object sender, AppActivationArguments args)
   {
       ExtendedActivationKind kind = args.Kind;
   }
   ```

## Test single-instancing via app deployment

Until this point, we've been testing the app by debugging within Visual Studio. However, we can only have one debugger running at once. This limitation prevents us from knowing whether the app is single-instanced because we can’t debug the same project twice at the same time. For an accurate test, we'll deploy the application to our local Windows client. After deploying, we can launch the app from the desktop like you would with any app installed on Windows.

1. Navigate to Solution Explorer, right-click on the project name, and select **Deploy**.
1. Open the Start menu and click into the search field.
1. Type your app's name in the search field.
1. Click the app icon from the search result to launch your app.

   > [!NOTE]
   > If you experience app crashes in release mode, there are some known issues with trimmed apps in the Windows App SDK. You can disable trimming in the project by setting the **PublishTrimmed** property to **false** for all build configurations in your project's `.pubxml` files. For more information, see [this issue](https://github.com/microsoft/microsoft-ui-xaml/issues/9914#issuecomment-2303010651) on GitHub.

1. Repeat steps 2 to 4 to launch the same app again and see if another instance opens. If the app is single-instanced, the first instance will be activated instead of a new instance opening.

   > [!TIP]
   > You can optionally add some logging code to the **OnActivated** method to verify that the existing instance has been activated.

## Summary

All the code covered here is on [GitHub](https://github.com/jingwei-a-zhang/WinAppSDK-DrumPad), with branches for the different steps in the original [Windows blog series](https://blogs.windows.com/windowsdeveloper/2022/01/28/making-the-app-single-instanced-part-3/). See the [single-instancing](https://github.com/jingwei-a-zhang/WinAppSDK-DrumPad/tree/single-instancing) branch for code specific to this how-to. The `main` branch is the most comprehensive. The other branches are intended to show you how the app architecture evolved.

## Related content

[App instancing with the app lifecycle API](applifecycle-instancing.md)

[Making the app single-instanced (Part 3)](https://blogs.windows.com/windowsdeveloper/2022/01/28/making-the-app-single-instanced-part-3/)

[WinAppSDK-DrumPad sample on GitHub](https://github.com/jingwei-a-zhang/WinAppSDK-DrumPad)
