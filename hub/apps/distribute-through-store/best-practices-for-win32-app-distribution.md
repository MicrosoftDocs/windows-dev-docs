---
description: This section will guide you on distribution options through Microsoft Store, suggested best practices and scenarios to be considered .
title: Best Practices for distributing your Win32 app through Microsoft Store and Commerce
ms.date: 12/05/2023
ms.topic: article
ms.localizationpriority: medium
---

# Best Practices for distributing your Win32 app through Microsoft Store and Commerce

This article guides you on a smooth onboarding process, various distribution options, recommended best practices, and scenarios to consider when distributing your app via the Store, to ensure a better customer experience. 

## Distribution options – Select the one that works best for you! 

You have two key options available for distributing your Win32 app through Microsoft Store. 

If you want to use Microsoft Hosting and Commerce services, you can distribute your app through Microsoft Store by packaging Win32 app as MSIX. Leverage the documentation on [creating MSIX package](https://go.microsoft.com/fwlink/?linkid=2255119) and package your app as MSIX. Follow these [steps](https://go.microsoft.com/fwlink/?linkid=2254524) for account creation and then follow these [steps](https://go.microsoft.com/fwlink/?linkid=2247663), to bring your app to the Store. Microsoft Store recommends this option as users will experience intuitive discovery, acquisition and install experience.

If you want to reuse your Hosting and Commerce services, you can distribute your Win32 app as-is through Microsoft Store by submitting the same directly.

Let’s delve into each of these options in detail below. 

### Option 1 - Package your Win32 app as MSIX 

Utilize Store commerce features and exclusive Store hosting by packaging your existing Win32 app as MSIX. Leverage the documentation on [creating MSIX package](https://go.microsoft.com/fwlink/?linkid=2255119) and migration practices to transition seamlessly. Once your app is packaged as MSIX, you can start the process for bringing your app to the Store. You should have a [Partner Center](https://partner.microsoft.com/dashboard) account to submit an app to the Store. If you don’t have an account, follow these [steps](https://go.microsoft.com/fwlink/?linkid=2254524) for account creation and then follow these [steps](https://go.microsoft.com/fwlink/?linkid=2247663) to effortlessly introduce your application to the Store.

To help users transition to your packaged app, you can enhance your package manifest with some [extensions](https://go.microsoft.com/fwlink/?linkid=2254522). These extensions can do the following:
1.	Redirect existing Start tiles and taskbar buttons to your packaged app.
2.	Register your packaged app for a set of file types.
3.	Set your packaged app as the default handler for certain types of files.
4.	For more information on how to use these extensions, see [Transition users to your app](https://go.microsoft.com/fwlink/?linkid=2254522).

You can also add some code to your packaged app that does the following:
1.  Move user data from your desktop app to the appropriate folders of your packaged app.
2.  Allow users to uninstall the desktop version of your app.

In the next section, we will discuss how to migrate user data.

#### Migrating user data from Web installed app to MSIX version

When you add code that migrates user data, you should run that code only once, when the application starts for the first time. You should also inform the user about the migration process, the benefits of doing it, and the impact on their existing data.

The following is an example of how you can implement this in a .NET-based packaged app.

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

#### Handling installation of MSIX packaged apps on Web installed devices

For users with an existing web-installed version, seeking to reinstall the MSIX-packaged app from the Microsoft Store on the same device:

**Prompt for reinstallation**

Before you uninstall the desktop version of your app, you should ask the user for their consent. Show them a dialog box that explains why you want to uninstall the desktop app and what will happen to their data. Users might choose to keep the desktop app on their device. In that case, you need to decide whether you want to prevent them from using the desktop app or allow them to use both apps simultaneously.

The following is an example of how you can implement this in a .NET-based packaged app.

You can see the full context of this snippet in the MainWindow.cs file of this sample [WPF picture viewer with transition/migration/uninstallation](https://go.microsoft.com/fwlink/?linkid=2254523).

Sample code for handling the scenario:

```csharp
// Replace the guid (7AD02FB8-B85E-44BC-8998-F4803BA5A0E3) with your product code.

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
**Coexistence**

Alternatively, allow both versions to coexist on the device. This case is suitable for supporting licensed apps with multiple versions.

#### Managing seamless transition for licensed users

You can offer Promotional codes as an easy way to give existing users free access to your app or add-on. You can generate single-use codes (and distribute one to each customer), or you can choose to generate a code that can be used multiple times by a specified number of customers. Provide clear instructions for users who purchased licenses for the non-Store version, ensuring a seamless transition to maintain access to features or content tied to their existing licenses.

[Partner Center](https://partner.microsoft.com/dashboard) lets you generate promotional codes for an app or add-on that you have published in the Microsoft Store. Each promotional code has a corresponding unique redeemable URL that a customer can click in order to redeem the code and install your app or add-on from the Microsoft Store. For more details, refer [Generate promotional codes](https://go.microsoft.com/fwlink/?linkid=2255120).

### Option 2 - Bring your unmodified Win32 App as-is

Bring your traditional desktop apps, commonly called “Win32” apps, packaged in .EXE or .MSI installers, and built using anything from .NET (WPF, Windows Forms, console) to C++, WinUI, MFC, Qt, Flutter, OpenGL, Pascal, Java, Electron, and so much more. Bringing your Win32 app to Microsoft Store adds another distribution channel for your traditional desktop app by making it discoverable to Windows customers from within Windows, all while keeping your existing build production workflows. 

You should have a [Partner Center](https://partner.microsoft.com/dashboard) account to submit an app to the Store. If you don’t have an account, follow these [steps](https://go.microsoft.com/fwlink/?linkid=2254524) for account creation and then follow these [steps](https://go.microsoft.com/fwlink/?linkid=2212501) to bring your unmodified Win32 app to the Store.

#### Experience for existing users

When existing users of your Win32 app explore the Microsoft Store for the same version, they will find the 'Installed' status on the PDP (Product detail page) of the app in the Store. This seamless integration ensures a straightforward experience, allowing users to maintain their familiarity and installed status with the app.

#### Experience for new users 

For new users who discover your Win32 app directly from Microsoft Store, the experience will be same as acquiring any other content from the Store. Regardless of how users initially acquired your Win32 app, you can provide a consistent and convenient in-app updates experience, so that your users will seamlessly receive the latest features, improvements, and enhancements directly through the app. Clearly communicate any changes during the update process to maintain user engagement and satisfaction

## Promoting the app on your website

You can add a Microsoft Store app badge on your app’s website to display, direct and track traffic from your assets to Microsoft Store listing. Read the [Microsoft Store marketing guidelines](https://go.microsoft.com/fwlink/?linkid=2255121) for apps and use this [badge generator](https://go.microsoft.com/fwlink/?linkid=2255504) to add a Microsoft Store badge on your website.
