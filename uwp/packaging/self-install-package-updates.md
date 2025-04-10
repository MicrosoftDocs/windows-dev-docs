---
ms.assetid: 414ACC73-2A72-465C-BD15-1B51CB2334F2
title: Download and install package updates from the Store
description: Learn how to mark packages as mandatory in Partner Center and write code in your app to download and install package updates.
ms.date: 04/04/2018
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Download and install package updates from the Store

Starting in Windows 10, version 1607, you can use methods of the [StoreContext](/uwp/api/windows.services.store.storecontext) class in the [Windows.Services.Store](/uwp/api/windows.services.store) namespace to programmatically check for package updates for the current app from the Microsoft Store, and download and install the updated packages. You can also query for packages that you have marked as mandatory in Partner Center and disable functionality in your app until the mandatory update is installed.

Additional [StoreContext](/uwp/api/windows.services.store.storecontext) methods introduced in Windows 10, version 1803 enable you to download and install package updates silently (without displaying a notification UI to the user), uninstall an [optional package](/windows/msix/package/optional-packages), and get info about packages in the download and install queue for your app.

These features help you automatically keep your user base up to date with the latest version of your app, optional packages, and related services in the Store.

## Download and install package updates with the user's permission

This code example demonstrates how to use the [GetAppAndOptionalStorePackageUpdatesAsync](/uwp/api/windows.services.store.storecontext.getappandoptionalstorepackageupdatesasync) method to discover all available package updates from the Store and then call the [RequestDownloadAndInstallStorePackageUpdatesAsync](/uwp/api/windows.services.store.storecontext.requestdownloadandinstallstorepackageupdatesasync) method to download and install the updates. When using this method to download and install updates, the OS displays a dialog that asks the user's permission before downloading the updates.

> [!NOTE]
> These methods support required and [optional packages](/windows/msix/package/optional-packages) for your app. Optional packages are useful for downloadable content (DLC) add-ons, dividing your large app for size constraints, or for shipping additional content separate from your core app. To get permission to submit an app that uses optional packages (including DLC add-ons) to the Store, see [Windows developer support](https://developer.microsoft.com/windows/support).

This code example assumes:

* The code runs in the context of a [Page](/uwp/api/windows.ui.xaml.controls.page).
* The **Page** contains a [ProgressBar](/uwp/api/windows.ui.xaml.controls.progressbar) named ```downloadProgressBar``` to provide status for the download operation.
* The code file has a **using** statement for the **Windows.Services.Store**, **Windows.Threading.Tasks**, and **Windows.UI.Popups** namespaces.
* The app is a single-user app that runs only in the context of the user that launched the app. For a [multi-user app](../xbox-apps/multi-user-applications.md), use the [GetForUser](/uwp/api/windows.services.store.storecontext.User) method to get a **StoreContext** object instead of the [GetDefault](/uwp/api/windows.services.store.storecontext.GetDefault) method.

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

> [!NOTE]
> To only download (but not install) the available package updates, use the [RequestDownloadStorePackageUpdatesAsync](/uwp/api/windows.services.store.storecontext.requestdownloadstorepackageupdatesasync) method.

### Display download and install progress info

When you call the [RequestDownloadStorePackageUpdatesAsync](/uwp/api/windows.services.store.storecontext.requestdownloadstorepackageupdatesasync) or [RequestDownloadAndInstallStorePackageUpdatesAsync](/uwp/api/windows.services.store.storecontext.requestdownloadandinstallstorepackageupdatesasync) method, you can assign a [Progress](/uwp/api/windows.foundation.iasyncoperationwithprogress-2.progress) handler that is called one time for each step in the download (or download and install) process for each package in this request. The handler receives a [StorePackageUpdateStatus](/uwp/api/windows.services.store.storepackageupdatestatus) object that provides info about the update package that raised the progress notification. The previous example uses the **PackageDownloadProgress** field of the **StorePackageUpdateStatus** object to display the progress of the download and install process.

Be aware that when you call **RequestDownloadAndInstallStorePackageUpdatesAsync** to download and install package updates in a single operation, the **PackageDownloadProgress** field increases from 0.0 to 0.8 during the download process for a package, and then it increases from 0.8 to 1.0 during the install. Therefore, if you map the percentage shown in your custom progress UI directly to the value of the **PackageDownloadProgress** field, your UI will show 80% when the package is finished downloading and the OS displays the installation dialog. If you want your custom progress UI to display 100% when the package is downloaded and ready to be installed, you can modify your code to assign 100% to your progress UI when the **PackageDownloadProgress** field reaches 0.8.

## Download and install package updates silently

Starting in Windows 10, version 1803, you can use the [TrySilentDownloadStorePackageUpdatesAsync](/uwp/api/windows.services.store.storecontext.trysilentdownloadstorepackageupdatesasync) and [TrySilentDownloadAndInstallStorePackageUpdatesAsync](/uwp/api/windows.services.store.storecontext.trysilentdownloadandinstallstorepackageupdatesasync) methods to download and install package updates silently, without displaying a notification UI to the user. This operation will succeed only if the user has enabled the **Update apps automatically** setting in the Store and the user is not on a metered network. Before calling these methods, you can first check the [CanSilentlyDownloadStorePackageUpdates](/uwp/api/windows.services.store.storecontext.cansilentlydownloadstorepackageupdates) property to determine whether these conditions are currently met.

This code example demonstrates how to use the [GetAppAndOptionalStorePackageUpdatesAsync](/uwp/api/windows.services.store.storecontext.getappandoptionalstorepackageupdatesasync) method to discover all available package updates and then call the [TrySilentDownloadStorePackageUpdatesAsync](/uwp/api/windows.services.store.storecontext.trysilentdownloadstorepackageupdatesasync) and [TrySilentDownloadAndInstallStorePackageUpdatesAsync](/uwp/api/windows.services.store.storecontext.trysilentdownloadandinstallstorepackageupdatesasync) methods to download and install the updates silently.

This code example assumes:
* The code file has a **using** statement for the **Windows.Services.Store** and  **System.Threading.Tasks** namespaces.
* The app is a single-user app that runs only in the context of the user that launched the app. For a [multi-user app](../xbox-apps/multi-user-applications.md), use the [GetForUser](/uwp/api/windows.services.store.storecontext.User) method to get a **StoreContext** object instead of the [GetDefault](/uwp/api/windows.services.store.storecontext.GetDefault) method.

> [!NOTE]
> The **IsNowAGoodTimeToRestartApp**, **RetryDownloadAndInstallLater**, and **RetryInstallLater** methods called by the code in this example are placeholder methods that are intended to be implemented as needed according to your own app's design.

```csharp
private StoreContext context = null;

public async Task DownloadAndInstallAllUpdatesInBackgroundAsync()
{
    if (context == null)
    {
        context = StoreContext.GetDefault();
    }

    // Get the updates that are available.
    IReadOnlyList<StorePackageUpdate> storePackageUpdates =
        await context.GetAppAndOptionalStorePackageUpdatesAsync();

    if (storePackageUpdates.Count > 0)
    {

        if (!context.CanSilentlyDownloadStorePackageUpdates)
        {
            return;
        }

        // Start the silent downloads and wait for the downloads to complete.
        StorePackageUpdateResult downloadResult =
            await context.TrySilentDownloadStorePackageUpdatesAsync(storePackageUpdates);

        switch (downloadResult.OverallState)
        {
            case StorePackageUpdateState.Completed:
                // The download has completed successfully. At this point, confirm whether your app
                // can restart now and then install the updates (for example, you might only install
                // packages silently if your app has been idle for a certain period of time). The
                // IsNowAGoodTimeToRestartApp method is not implemented in this example, you should
                // implement it as needed for your own app.
                if (IsNowAGoodTimeToRestartApp())
                {
                    await InstallUpdate(storePackageUpdates);
                }
                else
                {
                    // Retry/reschedule the installation later. The RetryInstallLater method is not  
                    // implemented in this example, you should implement it as needed for your own app.
                    RetryInstallLater();
                    return;
                }
                break;
            // If the user cancelled the download or you can't perform the download for some other
            // reason (for example, Wi-Fi might have been turned off and the device is now on
            // a metered network) try again later. The RetryDownloadAndInstallLater method is not  
            // implemented in this example, you should implement it as needed for your own app.
            case StorePackageUpdateState.Canceled:
            case StorePackageUpdateState.ErrorLowBattery:
            case StorePackageUpdateState.ErrorWiFiRecommended:
            case StorePackageUpdateState.ErrorWiFiRequired:
            case StorePackageUpdateState.OtherError:
                RetryDownloadAndInstallLater();
                return;
            default:
                break;
        }
    }
}

private async Task InstallUpdate(IReadOnlyList<StorePackageUpdate> storePackageUpdates)
{
    // Start the silent installation of the packages. Because the packages have already
    // been downloaded in the previous method, the following line of code just installs
    // the downloaded packages.
    StorePackageUpdateResult downloadResult =
        await context.TrySilentDownloadAndInstallStorePackageUpdatesAsync(storePackageUpdates);

    switch (downloadResult.OverallState)
    {
        // If the user cancelled the installation or you can't perform the installation  
        // for some other reason, try again later. The RetryInstallLater method is not  
        // implemented in this example, you should implement it as needed for your own app.
        case StorePackageUpdateState.Canceled:
        case StorePackageUpdateState.ErrorLowBattery:
        case StorePackageUpdateState.OtherError:
            RetryInstallLater();
            return;
        default:
            break;
    }
}
```

## Mandatory package updates

When you create a package submission in Partner Center for an app that targets Windows 10, version 1607 or later, you can [mark the package as mandatory](/windows/apps/publish/publish-your-app/upload-app-packages?pivots=store-installer-msix#mandatory-update) and the date and time on which it becomes mandatory. When this property is set and your app discovers that the package update is available, your app can determine whether the update package is mandatory and alter its behavior until the update is installed (for example, your app can disable features).

> [!NOTE]
> The mandatory status of a package update is not enforced by Microsoft, and the OS does not provide a UI to indicate to users that a mandatory app update must be installed. Developers are intended to use the mandatory setting to enforce mandatory app updates in their own code.  

To mark a package submission as mandatory:

1. Sign in to [Partner Center](https://partner.microsoft.com/dashboard) and navigate to the overview page for your app.
2. Click the name of the submission that contains the package update you want to make mandatory.
3. Navigate to the **Packages** page for the submission. Near the bottom of this page, select **Make this update mandatory** and then choose the day and time on which the package update becomes mandatory. This option applies to all UWP packages in the submission.

For more information, see [Upload app packages](/windows/apps/publish/publish-your-app/upload-app-packages?pivots=store-installer-msix).

> [!NOTE]
> If you create a [package flight](/windows/apps/publish/package-flights), you can mark the packages as mandatory using a similar UI on the **Packages** page for the flight. In this case, the mandatory package update applies only to the customers who are part of the flight group.

### Code example for mandatory packages

The following code example demonstrates how to determine whether any update packages are mandatory. Typically, you should downgrade your app experience gracefully for the user if a mandatory package update does not successfully download or install.

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
    // operation and only installs the updates; no download progress notifications are provided.
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

## Uninstall optional packages

Starting in Windows 10, version 1803, you can use the [RequestUninstallStorePackageAsync](/uwp/api/windows.services.store.storecontext.requestuninstallstorepackageasync) or [RequestUninstallStorePackageByStoreIdAsync](/uwp/api/windows.services.store.storecontext.requestuninstallstorepackagebystoreidasync) methods to uninstall an [optional package](/windows/msix/package/optional-packages) (including a DLC package) for the current app. For example, if you have an app with content that is installed via optional packages, you might want to provide a UI that enables users to uninstall the optional packages to free up disk space.

The following code example demonstrates how to call [RequestUninstallStorePackageAsync](/uwp/api/windows.services.store.storecontext.requestuninstallstorepackageasync). This example assumes:
* The code file has a **using** statement for the **Windows.Services.Store** and  **System.Threading.Tasks** namespaces.
* The app is a single-user app that runs only in the context of the user that launched the app. For a [multi-user app](../xbox-apps/multi-user-applications.md), use the [GetForUser](/uwp/api/windows.services.store.storecontext.User) method to get a **StoreContext** object instead of the [GetDefault](/uwp/api/windows.services.store.storecontext.GetDefault) method.

```csharp
public async Task UninstallPackage(Windows.ApplicationModel.Package package)
{
    if (context == null)
    {
        context = StoreContext.GetDefault();
    }

    var storeContext = StoreContext.GetDefault();
    IAsyncOperation<StoreUninstallStorePackageResult> uninstallOperation =
        storeContext.RequestUninstallStorePackageAsync(package);

    // At this point, you can update your app UI to show that the package
    // is installing.

    uninstallOperation.Completed += (asyncInfo, status) =>
    {
        StoreUninstallStorePackageResult result = uninstallOperation.GetResults();
        switch (result.Status)
        {
            case StoreUninstallStorePackageStatus.Succeeded:
                {
                    // Update your app UI to show the package as uninstalled.
                    break;
                }
            default:
                {
                    // Update your app UI to show that the package uninstall failed.
                    break;
                }
        }
    };
}
```

## Get download queue info

Starting in Windows 10, version 1803, you can use the [GetAssociatedStoreQueueItemsAsync](/uwp/api/windows.services.store.storecontext.getassociatedstorequeueitemsasync) and [GetStoreQueueItemsAsync](/uwp/api/windows.services.store.storecontext.getstorequeueitemsasync) methods to get info about the packages that are in the current download and installation queue from the Store. These methods are useful if your app or game supports large optional packages (including DLCs) that can take hours or days to download and install, and you want to gracefully handle the case where a customer closes your app or game before the download and installation process is complete. When the customer starts your app or game again, your code can use these methods to get info about the state of the packages that are still in the download and installation queue so you can display the status of each package to the customer.

The following code example demonstrates how to call [GetAssociatedStoreQueueItemsAsync](/uwp/api/windows.services.store.storecontext.getassociatedstorequeueitemsasync) to get the list of in-progress package updates for the current app and retrieve status info for each package. This example assumes:
* The code file has a **using** statement for the **Windows.Services.Store** and  **System.Threading.Tasks** namespaces.
* The app is a single-user app that runs only in the context of the user that launched the app. For a [multi-user app](../xbox-apps/multi-user-applications.md), use the [GetForUser](/uwp/api/windows.services.store.storecontext.User) method to get a **StoreContext** object instead of the [GetDefault](/uwp/api/windows.services.store.storecontext.GetDefault) method.

> [!NOTE]
> The **MarkUpdateInProgressInUI**, **RemoveItemFromUI**, **MarkInstallCompleteInUI**, **MarkInstallErrorInUI**, and **MarkInstallPausedInUI** methods called by the code in this example are placeholder methods that are intended to be implemented as needed according to your own app's design.

```csharp
private StoreContext context = null;

private async Task GetQueuedInstallItemsAndBuildInitialStoreUI()
{
    if (context == null)
    {
        context = StoreContext.GetDefault();
    }

    // Get the Store packages in the install queue.
    IReadOnlyList<StoreQueueItem> storeUpdateItems = await context.GetAssociatedStoreQueueItemsAsync();

    foreach (StoreQueueItem storeItem in storeUpdateItems)
    {
        // In this example we only care about package updates.
        if (storeItem.InstallKind != StoreQueueItemKind.Update)
            continue;

        StoreQueueItemStatus currentStatus = storeItem.GetCurrentStatus();
        StoreQueueItemState installState = currentStatus.PackageInstallState;
        StoreQueueItemExtendedState extendedInstallState =
            currentStatus.PackageInstallExtendedState;

        // Handle the StatusChanged event to display current status to the customer.
        storeItem.StatusChanged += StoreItem_StatusChanged;

        switch (installState)
        {
            // Download and install are still in progress, so update the status for this  
            // item and provide the extended state info. The following methods are not
            // implemented in this example; you should implement them as needed for your
            // app's UI.
            case StoreQueueItemState.Active:
                MarkUpdateInProgressInUI(storeItem, extendedInstallState);
                break;
            case StoreQueueItemState.Canceled:
                RemoveItemFromUI(storeItem);
                break;
            case StoreQueueItemState.Completed:
                MarkInstallCompleteInUI(storeItem);
                break;
            case StoreQueueItemState.Error:
                MarkInstallErrorInUI(storeItem);
                break;
            case StoreQueueItemState.Paused:
                MarkInstallPausedInUI(storeItem, installState, extendedInstallState);
                break;
        }
    }
}

private void StoreItem_StatusChanged(StoreQueueItem sender, object args)
{
    StoreQueueItemStatus currentStatus = sender.GetCurrentStatus();
    StoreQueueItemState installState = currentStatus.PackageInstallState;
    StoreQueueItemExtendedState extendedInstallState = currentStatus.PackageInstallExtendedState;

    switch (installState)
    {
        // Download and install are still in progress, so update the status for this  
        // item and provide the extended state info. The following methods are not
        // implemented in this example; you should implement them as needed for your
        // app's UI.
        case StoreQueueItemState.Active:
            MarkUpdateInProgressInUI(sender, extendedInstallState);
            break;
        case StoreQueueItemState.Canceled:
            RemoveItemFromUI(sender);
            break;
        case StoreQueueItemState.Completed:
            MarkInstallCompleteInUI(sender);
            break;
        case StoreQueueItemState.Error:
            MarkInstallErrorInUI(sender);
            break;
        case StoreQueueItemState.Paused:
            MarkInstallPausedInUI(sender, installState, extendedInstallState);
            break;
    }
}
```

## Related topics

* [Optional packages and related set authoring](/windows/msix/package/optional-packages)
