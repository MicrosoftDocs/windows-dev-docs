---
description: This section will guide you on how to transition users from your web unpackaged app to Store packaged app.
title: How to transition users from your web unpackaged app to Store packaged app.
ms.date: 02/15/2024
ms.topic: article
ms.localizationpriority: medium
---

# How to transition users from your web unpackaged app to Store packaged app

If you distribute your application both as a web download (EXE /MSI) and in the Store as a packaged application (MSIX), you might want to prevent users from installing both versions or migrate users from the unpackaged web version to the Store version. This guide will provide instructions on how to seamlessly transition users from the unpackaged version to the packaged one.

Two scenarios will be described below:

1. The user has already installed the web-based unpackaged version, and you wish to replace it with the Store's packaged version.
2. The user has already installed both versions, and you want to give priority to the Store packaged version and uninstall the web-based unpackaged version.
<br>

## Scenario 1: Automatically update the web unpackaged application to the Store packaged application
If you aim to automatically migrate your users from the web unpackaged application to the packaged Store version, we recommend to follow the following steps:

1. Enable your Store-packaged application to use existing taskbar and Start menu pins, ensuring users retain their shortcuts when the Store-packaged application replaces the web unpackaged application. 
    * See section: [Migrate existing pinned taskbar and Start Menu shortcuts](#how-to-migrate-existing-pinned-taskbar-and-start-menu-shortcuts)
2. Download and install silently the Store version from your unpackaged web version. 
    * See section: [How to install the Store packaged application from your web unpackaged application?](#how-to-install-the-store-packaged-application-from-your-web-unpackaged-application)
3. Indicate to users that the application will restart to apply an update
4. Once downloaded and installed, launch the Store packaged version and close the web unpackaged version. 
    * See section: [How to launch the Store application from your web unpackaged app?](#how-to-launch-the-store-application-from-your-web-unpackaged-app)
5. In the Store packaged application, migrate the data to the new app data folder. 
    * See section: [Migrate data](#how-to-migrate-data)
6. Finally, programmatically uninstall the unpackaged web version. 
    * See section: [How to uninstall your web unpackaged application from the packaged one?](#how-to-uninstall-your-web-unpackaged-application-from-the-packaged-one)
<br>

## Scenario 2: Uninstall the web-based unpackaged application if the user has installed both versions.

You can allow your users to use both versions of your application side-by-side, but you will have to manage conflicts between the application and will be responsible for syncing the data between the 2 versions.

If you prefer your users to only use 1 version and prioritize the Store version, here are some recommendations:

1. Enable your Store-packaged application to use existing taskbar and Start menu pins, ensuring users retain their shortcuts when the Store-packaged application replaces the web unpackaged application.
    * See section: [Migrate existing pinned taskbar and Start Menu shortcuts](#how-to-migrate-existing-pinned-taskbar-and-start-menu-shortcuts)
2. The Store application should detect if the unpackaged version is present and uninstall it at launch
    * See section: [How to detect if the Store packaged version is installed and launch it?](#how-to-detect-if-the-store-packaged-version-is-installed-and-launch-it)
3. When users are launching the unpackaged application, you should automatically launch the packaged version
    * See section: [How to launch the Store application from your web unpackaged app?](#how-to-launch-the-store-application-from-your-web-unpackaged-app)
4. Potentially migrate the data if you wish
    * See section: [Migrate data](#how-to-migrate-data)
5. Finally, programmatically uninstall the unpackaged web version.
    * See section: [How to uninstall your web unpackaged application from the packaged one?](#how-to-uninstall-your-web-unpackaged-application-from-the-packaged-one)
<br>

## Technical Recommendations
### How to install the Store packaged application from your web unpackaged application

To initiate the download and installation, you must know your application's Store ID. This 12-character ID can be obtained from the Partner Center, specifically under the "Product Identity" section, even if your application has not been submitted yet.

Subsequently, you can use the following code to silently download and install the Store application. This code will:

1. Assign an entitlement to the current Store user if present; otherwise, the entitlement will be associated with the device.
2. Initiate the download and installation of the product without generating any notification toasts.
3. You can monitor the download and installation progress using the event APIs.
```csharp
    private async Task<bool> DownloadStoreVersionAsync()
    {
        var productId = "<Product Id from Partner Center>";
        var applicationName = "<name of your application>";

        var appInstallManager = new AppInstallManager();
        var entitlement = await appInstallManager.GetFreeUserEntitlementAsync(productId, string.Empty, string.Empty);
        if (entitlement.Status is GetEntitlementStatus.NoStoreAccount)
        {
            entitlement = await appInstallManager.GetFreeDeviceEntitlementAsync(productId, string.Empty, string.Empty);
        }
        if (entitlement.Status is not GetEntitlementStatus.Succeeded)
        {
            return false;
        }

        var options = new AppInstallOptions()
        {
            LaunchAfterInstall = true,
            CompletedInstallToastNotificationMode = AppInstallationToastNotificationMode.NoToast
        };
        var items = await appInstallManager.StartProductInstallAsync(productId, string.Empty, applicationName, string.Empty, options);
        var firstItem = items.FirstOrDefault();
        if(firstItem is null)
        {
            return false;
        }
        firstItem.StatusChanged += StoreInstallation_StatusChanged;
        firstItem.Completed += StoreInstallation_Completed;
        return true;
    }

    private void StoreInstallation_Completed(AppInstallItem sender, object args)
    {
        // Launch the new Store version when ready and close this application
        // The Store version will then be responsible of migrating the data and uninstall the unpackaged version
    }

    private void StoreInstallation_StatusChanged(AppInstallItem sender, object args)
    {
        var status = sender.GetCurrentStatus();
        switch(status.InstallState)
        {
            case AppInstallState.Installing:
                {
                    // Show installing status
                }
                break;
            case AppInstallState.Downloading:
                {
                    // Show download progress using status.PercentComplete
                }
                break;
            ...
        }
```



### How to launch the Store application from your web unpackaged app
To launch a Store application, it is necessary to know its AMUID, which consists of the Package Family Name (found in the "Product Identity" section of the Partner Center) and the Application Id (from your appxmanifest), separated by an exclamation mark (!).

```csharp
        Process.Start(
            "explorer.exe",
            "shell:AppsFolder\\Microsoft.WindowsCalculator_8wekyb3d8bbwe!App"
        );
```



### How to detect if the Store packaged version is installed and launch it

You can determine whether your packaged version of the application is installed by using the [GetPackagesByPackageFamily](/windows/win32/api/appmodel/nf-appmodel-getpackagesbypackagefamily) win32 API and passing in the Package Family Name of your packaged app. If the count value is higher than zero, it indicates that the application is installed.


### How to uninstall your web unpackaged application from the packaged one

To retrieve the absolute path of your uninstaller, you can access the registry.

Your uninstaller information is located in the registry at:

```
HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\<your product code GUID\>
```

Retrieve the full command in the `UninstallString` value and execute it.
It is recommended to either perform the uninstallation silently or inform the user that you are migrating data and uninstalling the other application.


### How to migrate data
Your unpackaged application likely stores its local data in:

```
%localAppData%/<YourPublisherName\>/<YourAppName\>
```


Packaged applications have their reserved space for data storage, which is automatically deleted when the application is uninstalled. It is highly recommended (though not mandatory) to migrate the data to this space upon the first launch. You can retrieve the absolute path of this folder by calling [Windows.Storage.ApplicationData.Current.LocalFolder.Path](/uwp/api/windows.storage.applicationdata.localfolder?view=winrt-22621).


### How to migrate acquisitions and in-app purchases
#### In-app purchases
To guarantee an optimal user experience, it is essential that users can seamlessly access content they have purchased in the unpackaged version of your application. With this objective, the Microsoft Store has increased its flexibility for publishers by permitting the use of their own or third-party commerce platforms in addition to Microsoft's since June 2021.


We strongly encourage publishers to continue verifying in-app purchase entitlements as performed in the unpackaged version of their application in addition to integrate with the Microsoft Commerce platform to enable users to effortlessly purchase your content with just a few clicks on Windows.


#### Allow paid users of the unpackaged application to migrate to the packaged version
If users have purchased your product on your website, they should not have to pay again to download the packaged version from the Store.
To ensure a seamless transition, we recommend the following approaches:

1. Offer a free/demo version of your product, allowing users to unlock the full version through in-app purchases. For users who have already paid on your website, enable them to access the full version by signing in to verify their licenses or by entering their license key in the application's user interface.
2. Set your application as a paid offering but distribute coupon code to your existing users through your own channels. These codes will allow them to download the Store version at no additional cost. More information can be found in [Generate promotional codes](/windows/apps/publish/generate-promotional-codes).


### How to migrate existing pinned taskbar and Start Menu shortcuts
Your users may have pinned your desktop application to the taskbar or the Start menu. You can direct these shortcuts to your new packaged app by including the "windows.desktopAppMigration" extension in your application manifest.


#### Example

```csharp
xmlns:rescap3="http://schemas.microsoft.com/appx/manifest/foundation/windows10/restrictedcapabilities/3"
...
<Extensions>
<rescap3:Extension Category="windows.desktopAppMigration">
<rescap3:DesktopAppMigration>
<rescap3:DesktopApp AumId="[your_app_aumid]" />
<rescap3:DesktopApp ShortcutPath="%USERPROFILE%\Desktop\[my_app].lnk" />
<rescap3:DesktopApp ShortcutPath="%APPDATA%\Microsoft\Windows\Start Menu\Programs\[my_app].lnk" />
<rescap3:DesktopApp ShortcutPath="%PROGRAMDATA%\Microsoft\Windows\Start Menu\Programs\[my_app_folder]\[my_app].lnk"/>
</rescap3:DesktopAppMigration>
</rescap3:Extension>
</Extensions>
```

After installing your application, the pins in the taskbar or in the Start menu, as well as the tiles (for Windows 10) will launch automatically the Store application.

### How to migrate file extension & protocol associations
If your application supports file extension or protocol associations and users have selected your app as the default application for specific file extensions and protocols, you have the option to migrate these associations to your Store packaged application. This migration can be achieved by updating your app manifest with the following code snippet.

```code
xmlns:rescap3="http://schemas.microsoft.com/appx/manifest/foundation/windows10/restrictedcapabilities/3"
...
<Extensions>
<uap:Extension Category="windows.fileTypeAssociation">
<uap3:FileTypeAssociation Name=".foo">
<rescap3:MigrationProgIds>
<rescap3:MigrationProgId>Foo.Bar.1</rescap3:MigrationProgId>
</rescap3:MigrationProgIds>
…
</uap3:FileTypeAssociation>
</uap:Extension>
</Extensions>
```

Simply list the [programmatic identifiers](/windows/win32/shell/fa-progids) to which you want to migrate, and the system will automatically migrate them to your application after installation.
