---
title: Publish your packaged desktop application to the Microsoft Store or sideload it onto one or more devices.
description: Learn how to use Desktop Bridge to distribute a packaged desktop application to the Microsoft Store or sideload it onto one or more devices.
ms.date: 05/18/2018
ms.topic: article
keywords: windows 10, uwp
ms.assetid: edff3787-cecb-4054-9a2d-1fbefa79efc4
ms.author: mcleans
author: mcleanbyron
ms.localizationpriority: medium
---

# Distribute your packaged desktop app

If you decide to [package your desktop app in an MSIX package](/windows/msix/desktop/desktop-to-uwp-root), you can publish your packaged application to the Microsoft Store or sideload it onto one or more devices.

> [!NOTE]
> Do you have a plan for how you might transition users to your packaged application? Before you distribute your app, see the [Transition users to your packaged app](#transition-users) section of this guide to get some ideas.

## Distribute your application by publishing it to the Microsoft Store

The [Microsoft Store](https://www.microsoft.com/store/apps) is a convenient way for customers to get your app.

Publish your application to the Microsoft Store to reach the broadest audience. Also, organizational customers can acquire your application to distribute internally to their organizations through the [Microsoft Store for Business](https://businessstore.microsoft.com/store).

If you plan to publish to the Microsoft Store, you'll be asked a few extra questions as part of the submission process. That's because your package manifest declares a restricted capability named **runFullTrust**, and we need to approve your application's use of that capability. You can read more about this requirement here: [Restricted capabilities](/windows/uwp/packaging/app-capability-declarations#restricted-capabilities).

You don't have to sign your application before you submit it to the Store.

>[!IMPORTANT]
> If you plan to publish your application to the Microsoft Store, make sure that your application operates correctly on devices that run Windows 10 S. This is a Store requirement. See [Test your Windows app for Windows 10  S](/windows/msix/desktop/desktop-to-uwp-test-windows-s).

<a id="side-load"></a>

## Distribute your application without placing it onto the Microsoft Store

If you'd rather distribute your application without using the Store, you can manually distribute apps to one or more devices.

This might make sense if you want greater control over the distribution experience or you don't want to get involved with the Microsoft Store certification process.

To distribute your application to other devices without placing it in the Store, you have to obtain a certificate, sign your application by using that certificate, and then sideload your application onto those devices.

You can [create a certificate](/windows/msix/package/create-certificate-package-signing) or obtain one from a popular vendor such as [Verisign](https://www.verisign.com/).

If you plan to distribute your application onto devices that run Windows 10 S, your application has to be signed by the Microsoft Store so you'll have to go through the Store submission process before you can distribute your application onto those devices.

If you create a certificate, you have to install it into the **Trusted Root** or **Trusted People** certificate store on each device that runs your app. If you get a certificate from a popular vendor, you won't have to install anything onto other systems besides your app.  

> [!IMPORTANT]
> Make sure that the publisher name on your certificate matches the publisher name of your app.

To sign your application by using a certificate, see [Sign an application package using SignTool](/windows/msix/package/sign-app-package-using-signtool).

To sideload your application onto other devices, see [Sideload LOB apps in Windows 10](/windows/application-management/sideload-apps-in-windows-10).

<a id="transition-users"></a>

## Transition users to your packaged app

Before you distribute your app, consider adding a few extensions to your package manifest to help users get into the habit of using your packaged app. Here's a few things you can do.

* Point existing Start tiles and taskbar buttons to your packaged app.
* Associate your packaged application with a set of file types.
* Make your packaged application open certain types of files by default.

For the complete list of extensions and the guidance for how to use them, see [Transition users to your app](desktop-to-uwp-extensions.md#transition-users-to-your-app).

Also, consider adding code to your packaged application that accomplishes these tasks:

* Migrates user data associated with your desktop application to the appropriate folder locations of your packaged app.
* Gives users the option to uninstall the desktop version of your app.

Let's talk about each one of these tasks. We'll start with user data migration.

### Migrate user data

If you're going to add code that migrates user data, it's best to run that code only when the application is first started. Before you migrate the users data, display a dialog box to the user that explains what is happening, why it is recommended, and what's going to happen to their existing data.

Here's an example of how you could do this in a .NET-based packaged app.

```csharp
private void MigrateUserData()
{
    String sourceDir = Environment.GetFolderPath
        (Environment.SpecialFolder.ApplicationData) + "\\AppName";

    if (sourceDir != null)
    {
        DialogResult migrateResult = MessageBox.Show
            ("Would you like to migrate your data from the previous version of this app?",
             "Data Migration", MessageBoxButtons.YesNo);

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

It is better not to uninstall the users desktop application without first asking them for permission. Display a dialog box that asks the user for that permission. Users might decide not to uninstall the desktop version of your app. If that happens, you'll have to decide whether you want to block usage of the desktop application or support the side-by-side use of both apps.

Here's an example of how you could do this in a .NET-based packaged app.

To view the complete context of this snippet, see the **MainWindow.cs** file of this sample [WPF picture viewer with transition/migration/uninstallation](https://github.com/Microsoft/DesktopBridgeToUWP-Samples/tree/master/Samples/DesktopAppTransition).

```csharp
private void RemoveDesktopApp()
{
    //Typically, you can find your uninstall string at this location.
    String uninstallString = (String)Microsoft.Win32.Registry.GetValue
        (@"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion" +
         @"\Uninstall\{7AD02FB8-B85E-44BC-8998-F4803BA5A0E3}\", "UninstallString", null);

    //Detect if the previous version of the Desktop application is installed.
    if (uninstallString != null)
    {
        DialogResult uninstallResult = MessageBox.Show
            ("To have the best experience, consider uninstalling the "
              + " previous version of this app. Would you like to do that now?",
              "Uninstall the previous version", MessageBoxButtons.YesNo);

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
                //Uninstallation was unsuccessful - You can choose to block the application here.
            }
        }
    }

}
```

## Next steps

Have questions? Ask us on Stack Overflow. Our team monitors these [tags](https://stackoverflow.com/questions/tagged/project-centennial+or+desktop-bridge). You can also ask us [here](https://social.msdn.microsoft.com/Forums/en-US/home?filter=alltypes&sort=relevancedesc&searchTerm=%5BDesktop%20Converter%5D).

If you encounter issues publishing your application to the Store, this [blog post](/archive/blogs/appconsult/preparing-a-desktop-bridge-application-for-the-store-submission) contains some useful tips.