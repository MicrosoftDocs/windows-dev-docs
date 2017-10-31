---
author: mcleanbyron
ms.assetid: 414ACC73-2A72-465C-BD15-1B51CB2334F2
title: Download and install package updates for your app
description: Learn how to mark packages as mandatory in the Dev Center dashboard and write code in your app to download and install package updates.
ms.author: mcleans
ms.date: 03/15/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
localizationpriority: high
---
# Download and install package updates for your app


Starting in Windows 10, version 1607, you can use an API in the [Windows.Services.Store](https://docs.microsoft.com/uwp/api/windows.services.store) namespace to programmatically check for package updates for the current app, and download and install the updated packages. You can also query for packages that have been [marked as mandatory on the Windows Dev Center dashboard](#mandatory-dashboard) and disable functionality in your app until the mandatory update is installed.

These features help you to automatically keep your user base up to date with the latest version of your app and related services.

## API overview

Apps that targets Windows 10, version 1607 or later can use the following methods of the [StoreContext](https://docs.microsoft.com/uwp/api/windows.services.store.storecontext) class to download and install package updates.

|  Method  |  Description  |
|----------|---------------|
| [GetAppAndOptionalStorePackageUpdatesAsync](https://docs.microsoft.com/uwp/api/windows.services.store.storecontext#Windows_Services_Store_StoreContext_GetAppAndOptionalStorePackageUpdatesAsync) | Call this method to get the list of package updates that are available. Note that there can be a delay of up to a day between the time when a package passes the certification process and when the **GetAppAndOptionalStorePackageUpdatesAsync** method recognizes that the package update is available to the app. |
| [RequestDownloadStorePackageUpdatesAsync](https://docs.microsoft.com/uwp/api/windows.services.store.storecontext#Windows_Services_Store_StoreContext_RequestDownloadStorePackageUpdatesAsync_Windows_Foundation_Collections_IIterable_Windows_Services_Store_StorePackageUpdate__) | Call this method to download (but not install) the available package updates. This OS displays a dialog that asks the user's permission to download the updates. |
| [RequestDownloadAndInstallStorePackageUpdatesAsync](https://docs.microsoft.com/uwp/api/windows.services.store.storecontext#Windows_Services_Store_StoreContext_RequestDownloadAndInstallStorePackageUpdatesAsync_Windows_Foundation_Collections_IIterable_Windows_Services_Store_StorePackageUpdate__) | Call this method to download and install the available package updates. The OS displays dialogs that ask the user's permission to download and install the updates. If you already downloaded the package updates by calling **RequestDownloadStorePackageUpdatesAsync**, this method skips the download process and only installs the updates.  |

<span/>

These methods use [StorePackageUpdate](https://docs.microsoft.com/uwp/api/windows.services.store.storepackageupdate) objects to represent available update packages. Use the following **StorePackageUpdate** properties to get information about an update package.

|  Property  |  Description  |
|----------|---------------|
| [Mandatory](https://docs.microsoft.com/uwp/api/windows.services.store.storepackageupdate#Windows_Services_Store_StorePackageUpdate_Mandatory) | Use this property to determine whether the package is marked as mandatory in the Dev Center dashboard. |
| [Package](https://docs.microsoft.com/uwp/api/windows.services.store.storepackageupdate#Windows_Services_Store_StorePackageUpdate_Package) | Use this property to access the underlying package-related data. |

<span/>

## Code examples

The following code examples demonstrate how to download and install package updates in your app. These example assume:
* The code runs in the context of a [Page](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.controls.page.aspx).
* The **Page** contains a [ProgressBar](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.controls.progressbar.aspx) named ```downloadProgressBar``` to provide status for the download operation.
* The code file has a **using** statement for the **Windows.Services.Store**, **Windows.Threading.Tasks**, and **Windows.UI.Popups** namespaces.
* The app is a single-user app that runs only in the context of the user that launched the app. For a [multi-user app](https://msdn.microsoft.com/windows/uwp/xbox-apps/multi-user-applications), use the [GetForUser](https://docs.microsoft.com/uwp/api/windows.services.store.storecontext#Windows_Services_Store_StoreContext_GetForUser_Windows_System_User_) method to get a **StoreContext** object instead of the [GetDefault](https://docs.microsoft.com/uwp/api/windows.services.store.storecontext#Windows_Services_Store_StoreContext_GetDefault) method.

<span/>

### Download and install all package updates

The following code example demonstrates how to download and install all available package updates.  

```csharp
private StoreContext context = null;

public async Task DownloadAndInstallAllUpdatesAsync()
{
    if (context == null)
    {
        context = StoreContext.GetDefault();
    }

    // Get the updates that are available.
    IReadOnlyList<StorePackageUpdate> updates =
        await context.GetAppAndOptionalStorePackageUpdatesAsync();

    if (updates.Count > 0)
    {
        // Alert the user that updates are available and ask for their consent
        // to start the updates.
        MessageDialog dialog = new MessageDialog(
            "Download and install updates now? This may cause the application to exit.", "Download and Install?");
        dialog.Commands.Add(new UICommand("Yes"));
        dialog.Commands.Add(new UICommand("No"));
        IUICommand command = await dialog.ShowAsync();

        if (command.Label.Equals("Yes", StringComparison.CurrentCultureIgnoreCase))
        {
            // Download and install the updates.
            IAsyncOperationWithProgress<StorePackageUpdateResult, StorePackageUpdateStatus> downloadOperation =
                context.RequestDownloadAndInstallStorePackageUpdatesAsync(updates);

            // The Progress async method is called one time for each step in the download
            // and installation process for each package in this request.
            downloadOperation.Progress = async (asyncInfo, progress) =>
            {
                await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                () =>
                {
                    downloadProgressBar.Value = progress.PackageDownloadProgress;
                });
            };

            StorePackageUpdateResult result = await downloadOperation.AsTask();
        }
    }
}
```

### Handle mandatory package updates

The following code example builds off the previous example, and demonstrates how to determine whether any update packages have been [marked as mandatory on the Windows Dev Center dashboard](#mandatory-dashboard). Typically, you should downgrade your app experience gracefully for the user if a mandatory package update does not successfully download or install.

```csharp
private StoreContext context = null;

// Downloads and installs package updates in separate steps.
public async Task DownloadAndInstallAllUpdatesAsync()
{
    if (context == null)
    {
        context = StoreContext.GetDefault();
    }  
    // Get the updates that are available.
    IReadOnlyList<StorePackageUpdate> updates =
        await context.GetAppAndOptionalStorePackageUpdatesAsync();

    if (updates.Count != 0)
    {
        // Download the packages.
        bool downloaded = await DownloadPackageUpdatesAsync(updates);

        if (downloaded)
        {
            // Install the packages.
            await InstallPackageUpdatesAsync(updates);
        }
    }
}

// Helper method for downloading package updates.
private async Task<bool> DownloadPackageUpdatesAsync(IEnumerable<StorePackageUpdate> updates)
{
    bool downloadedSuccessfully = false;

    IAsyncOperationWithProgress<StorePackageUpdateResult, StorePackageUpdateStatus> downloadOperation =
        this.context.RequestDownloadStorePackageUpdatesAsync(updates);

    // The Progress async method is called one time for each step in the download process for each
    // package in this request.
    downloadOperation.Progress = async (asyncInfo, progress) =>
    {
        await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
        () =>
        {
            downloadProgressBar.Value = progress.PackageDownloadProgress;
        });
    };

    StorePackageUpdateResult result = await downloadOperation.AsTask();

    switch (result.OverallState)
    {
        case StorePackageUpdateState.Completed:
            downloadedSuccessfully = true;
            break;
        default:
            // Get the failed updates.
            var failedUpdates = result.StorePackageUpdateStatuses.Where(
                status => status.PackageUpdateState != StorePackageUpdateState.Completed);

            // See if any failed updates were mandatory
            if (updates.Any(u => u.Mandatory && failedUpdates.Any(
                failed => failed.PackageFamilyName == u.Package.Id.FamilyName)))
            {
                // At least one of the updates is mandatory. Perform whatever actions you
                // want to take for your app: for example, notify the user and disable
                // features in your app.
                HandleMandatoryPackageError();
            }
            break;
    }

    return downloadedSuccessfully;
}

// Helper method for installing package updates.
private async Task InstallPackageUpdatesAsync(IEnumerable<StorePackageUpdate> updates)
{
    IAsyncOperationWithProgress<StorePackageUpdateResult, StorePackageUpdateStatus> installOperation =
        this.context.RequestDownloadAndInstallStorePackageUpdatesAsync(updates);

    // The package updates were already downloaded separately, so this method skips the download
    // operatation and only installs the updates; no download progress notifications are provided.
    StorePackageUpdateResult result = await installOperation.AsTask();

    switch (result.OverallState)
    {
        case StorePackageUpdateState.Completed:
            break;
        default:
            // Get the failed updates.
            var failedUpdates = result.StorePackageUpdateStatuses.Where(
                status => status.PackageUpdateState != StorePackageUpdateState.Completed);

            // See if any failed updates were mandatory
            if (updates.Any(u => u.Mandatory && failedUpdates.Any(failed => failed.PackageFamilyName == u.Package.Id.FamilyName)))
            {
                // At least one of the updates is mandatory, so tell the user.
                HandleMandatoryPackageError();
            }
            break;
    }
}

// Helper method for handling the scenario where a mandatory package update fails to
// download or install. Add code to this method to perform whatever actions you want
// to take, such as notifying the user and disabling features in your app.
private void HandleMandatoryPackageError()
{
}
```

### Display progress info for the download and install

When you call **RequestDownloadStorePackageUpdatesAsync** or **RequestDownloadAndInstallStorePackageUpdatesAsync**, you can assign a [Progress](https://docs.microsoft.com/uwp/api/windows.foundation.iasyncoperationwithprogress_tresult_tprogress_#Windows_Foundation_IAsyncOperationWithProgress_2_Progress) handler that is called one time for each step in the download (or download and install) process for each package in this request. The handler receives a [StorePackageUpdateStatus](https://docs.microsoft.com/uwp/api/windows.services.store.storepackageupdatestatus) object that provides info about the update package that raised the progress notification. The previous examples use the **PackageDownloadProgress** field of the **StorePackageUpdateStatus** object to display the progress of the download and install process.

Be aware that when you call **RequestDownloadAndInstallStorePackageUpdatesAsync** to download and install package updates in a single operation, the **PackageDownloadProgress** field increases from 0.0 to 0.8 during the download process for a package, and then it increases from 0.8 to 1.0 during the install. Therefore, if you map the percentage shown in your custom progress UI directly to the value of the **PackageDownloadProgress** field, your UI will show 80% when the package is finished downloading and the OS displays the installation dialog. If you want your custom progress UI to display 100% when the package is downloaded and ready to be installed, you can modify your code to assign 100% to your progress UI when the **PackageDownloadProgress** field reaches 0.8.

<span id="mandatory-dashboard" />
## Make a package submission mandatory in the Dev Center dashboard

When you create a package submission for an app that targets Windows 10, version 1607 or later, you can mark the package as mandatory and the date/time on which it becomes mandatory. When this property is set and your app discovers that the package update is available by using the API described earlier in this article, your app can determine whether the update package is mandatory and alter its behavior until the update is installed (for example, your app can disable features).

> [!NOTE]
> The mandatory status of a package update is not enforced by Microsoft, and the OS does not provide a UI to indicate to users that a mandatory app update must be installed. Developers are intended to use the mandatory setting to enforce mandatory app updates in their own code.  

To mark a package submission as mandatory:

1. Sign in to the [Dev Center dashboard](https://dev.windows.com/overview) and navigate to the overview page for your app.
2. Click the name of the submission that contains the package update you want to make mandatory.
3. Navigate to the **Packages** page for the submission. Near the bottom of this page, select **Make this update mandatory** and then choose the day and time on which the package update becomes mandatory. This option applies to all UWP packages in the submission.

For more information about configuring packages in the Dev Center dashboard, see [Upload app packages](../publish/upload-app-packages.md).

  > [!NOTE]
  > If you create a [package flight](../publish/package-flights.md), you can mark the packages as mandatory using a similar UI on the **Packages** page for the flight. In this case, the mandatory package update applies only to the customers who are part of the flight group.
