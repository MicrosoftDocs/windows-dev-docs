---
author: normesta
Description: Distribute a packaged desktop app (Desktop Bridge)
Search.Product: eADQiWindows 10XVcnh
title: Publish your packaged desktop app to a Windows store or sideload it onto one or more devices.
ms.author: normesta
ms.date: 05/25/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.assetid: edff3787-cecb-4054-9a2d-1fbefa79efc4
---

# Distribute a packaged desktop app (Desktop Bridge)

Publish your packaged desktop app to a Windows store or sideload it onto one or more devices.  

> [!NOTE]
> Do you have a plan for how you might transition users to your packaged app? Before you distribute your app, see the [Transition users to your desktop bridge app](#transition-users) section of this guide to get some ideas.

## Distribute your app by publishing it to the Windows Store

The [Windows Store](https://www.microsoft.com/store/apps) is a convenient way for customers to get your app.

<div style="float: left; padding: 10px">
    ![Store icon](images/desktop-to-uwp/store.png)
</div>
 Publish your app to that store to reach the broadest audience. Also, Also, organizational customers can acquire your app to distribute internally to their organizations through the [Windows Store for Business](https://www.microsoft.com/business-store).

If you plan to publish to the Windows Store, and you haven't reached out to us yet, please fill out [this form](https://developer.microsoft.com/windows/projects/campaigns/desktop-bridge), and Microsoft will contact you to start the onboarding process.

You don't have to sign your app before you submit it to the store.

>[!IMPORTANT]
> If you plan to publish your app to the Windows Store, make sure that your app operates correctly on devices that run Windows 10 S. This is a store requirement. See [Test your Windows app for Windows 10  S](desktop-to-uwp-test-windows-s.md).

## Distribute your app without placing it onto the Windows Store

If you'd rather distribute your app without using the store, you can manually distribute apps to one or more devices.

This might make sense if you want greater control over the distribution experience or you don't want to get involved with the Windows Store certification process.

To distribute your app to other devices without placing it onto the store, you have to obtain a certificate, sign your app by using that certificate, and then sideload your app onto those devices.

You can [create a certificate](../packaging/create-certificate-package-signing.md) or obtain one from a popular vendor such as [Verisign](https://www.verisign.com/).

If you plan to distribute your app onto devices that run Windows 10 S, your app has to be signed by the Windows Store so you'll have to go through the Store submission process before you can distribute your app onto those devices.

If you create a certificate, you have to install it into the **Trusted Root** or **Trusted People** certificate store on each device that runs your app. If you get a certificate from a popular vendor, you won't have to install anything onto other systems besides your app.  

> [!IMPORTANT]
> Make sure that the publisher name on your certificate matches the publisher name of your app.

To sign your app by using a certificate, see [Sign an app package using SignTool](../packaging/sign-app-package-using-signtool.md).

To sideload your app onto other devices, see [Sideload LOB apps in Windows 10](https://technet.microsoft.com/itpro/windows/deploy/sideload-apps-in-windows-10).

**Videos**

|Publish your app into the Windows Store |Distribute an enterprise app  |
|---|---|
|<iframe src="https://mva.microsoft.com/en-US/training-courses-embed/developers-guide-to-the-desktop-bridge-17373/Demo-Windows-Store-Publication-3cWyG5WhD_5506218965"      width="426" height="472" allowFullScreen frameBorder="0"></iframe>|<iframe src="https://mva.microsoft.com/en-US/training-courses-embed/developers-guide-to-the-desktop-bridge-17373/Video-Distribution-for-Enterprise-Apps-XJ5Hd5WhD_1106218965" width="426" height="472" allowFullScreen frameBorder="0"></iframe>|

<span id="transition-users" />
## Transition users to your desktop bridge app

Before you distribute your app, consider adding a few extensions to your package manifest to help users get into the habit of using your desktop bridge app. Here's a few things you can do.

* Point existing Start tiles and taskbar buttons to your desktop bridge app.
* Associate your packaged app with a set of file types.
* Make your desktop bridge app open certain types of files by default.

For the complete list of extensions and the guidance for how to use them, see [Transition users to your app](desktop-to-uwp-extensions.md#transition-users-to-your-app).

Also, consider adding code to your desktop bridge app that accomplishes these tasks:

* Migrates user data associated with your desktop app to the appropriate folder locations of your desktop bridge app.
* Gives users the option to uninstall the desktop version of your app.

Let's talk about each one of these tasks. We'll start with user data migration.

### Migrate user data

If you're going to add code that migrates user data, it's best to run that code only when the app is first started. Before you migrate the users data, display a dialog box to the user that explains what is happening, why it is recommended, and what's going to happen to their existing data.

Here's an example of how you could do this in a .NET-based desktop bridge app.

```csharp
private void MigrateUserData()
{
    String sourceDir = Environment.GetFolderPath
        (Environment.SpecialFolder.ApplicationData) + "\\AppName";

    if (sourceDir != null)
    {
        String migrateMessage =
            "Would you like to migrate your data from the previous version of this app?";

        DialogResult migrateResult = MessageBox.Show
            (migrateMessage, "Data Migration", MessageBoxButtons.YesNo);

        if (migrateResult.Equals(DialogResult.Yes))
        {
            String destinationDir =
                Windows.Storage.ApplicationData.Current.LocalFolder.Path + "\\AppName";

            Process process = new Process();
            process.StartInfo.FileName = "robocopy.exe";
            process.StartInfo.Arguments = "%LOCALAPPDATA%\\AppName " + destinationDir + " /move";
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            process.WaitForExit();

            if (process.ExitCode > 1)
            {
                //Migration was unsuccessful -- you can choose to block/retry/other action
            }
        }
    }
}
```

### Uninstall the desktop version of your app

It is better not to uninstall the users desktop app without first asking them for permission. Display a dialog box that asks the user for that permission. Users might decide not to uninstall the desktop version of your app. If that happens, you'll have to decide whether you want to block usage of the desktop app or support the side-by-side use of both apps.

Here's an example of how you could do this in a .NET-based desktop bridge app.

To view the complete context of this snippet, see the **MainWindow.cs** file of this sample [WPF picture viewer with transition/migration/uninstallation](https://github.com/Microsoft/DesktopBridgeToUWP-Samples/tree/master/Samples/DesktopAppTransition).

```csharp
private void RemoveDesktopApp()
{              
    //Typically, you can find your uninstall string at this location.
    String uninstallString = (String)Microsoft.Win32.Registry.GetValue
        (@"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion" +
         @"\Uninstall\{7AD02FB8-B85E-44BC-8998-F4803BA5A0E3}\", "UninstallString", null);

    //Detect if the previous version of the Desktop App is installed.
    if (uninstallString != null)
    {
        String uninstallMessage = "To have the best experience, consider uninstalling the "
            +" previous version of this app. Would you like to do that now?";

        DialogResult uninstallResult = MessageBox.Show
            (uninstallMessage, "Uninstall the previous version", MessageBoxButtons.YesNo);

        if (uninstallResult.Equals(DialogResult.Yes))
        {
                    string[] uninstallArgs = uninstallString.Split(' ');

            Process process = new Process();
            process.StartInfo.FileName = uninstallArgs[0];
            process.StartInfo.Arguments = uninstallArgs[1];
            process.StartInfo.CreateNoWindow = true;

            process.Start();
            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                //Uninstallation was unsuccessful - You can choose to block the app here.
            }
        }
    }

}
```

### Video

<iframe src="https://mva.microsoft.com/en-US/training-courses-embed/developers-guide-to-the-desktop-bridge-17373/Demo-Transition-Taskbar-Pins-Start-Tiles-File-Type-Associations-and-Protocol-Handlers-MD5mv5WhD_2406218965" width="636" height="480" allowFullScreen frameBorder="0"></iframe>

## Next steps

**Find answers to specific questions**

Our team monitors these [StackOverflow tags](http://stackoverflow.com/questions/tagged/project-centennial+or+desktop-bridge).

**Give feedback about this article**

Use the comments section below.
