Learn how your app's packages are made available to your customers, and how to manage specific package scenarios.

## OS versions and package distribution

Different operating systems can run different types of packages. If more than one of your packages can run on a customer's device, the Microsoft Store will provide the best available match.

Generally speaking, later OS versions can run packages that target previous OS versions for the same device family. Windows 10 devices can run all previous supported OS versions (per device family). Windows 10 desktop devices can run apps that were built for Windows 8.1 or Windows 8. However, customers on Windows 10 or Windows 11 will only get those packages if the app doesn't include UWP packages targeting the applicable device family.

> [!IMPORTANT]
> You can no longer upload new XAP packages built using the Windows Phone 8.x SDK(s). Apps that are already in Store with XAP packages will continue to work on Windows 10 Mobile devices. For more info, see this [blog post](https://blogs.windows.com/windowsdeveloper/2018/08/20/important-dates-regarding-apps-with-windows-phone-8-x-and-earlier-and-windows-8-8-1-packages-submitted-to-microsoft-store).

## Removing an app from the Store

At times, you may want to stop offering an app to customers, effectively "unpublishing" it. To do so, use the toggle button in **Store presence** card from the **App overview** page. After you confirm that you want to make the app unavailable, within a few hours it will no longer be visible in the Store, and no new customers will be able to get it (unless they have a [promotional code](../../../apps/publish/generate-promotional-codes.md) and are using a Windows 10 or Windows 11 device).

:::image type="content" source="images/new-overview-make-product-unavailable.png" lightbox="images/new-overview-make-product-unavailable.png" alt-text="A screenshot of the new msix overview page showing how to make product unavailable in Store":::

> [!IMPORTANT]
> This option will override any [visibility](../../../apps/publish/publish-your-app/visibility-options.md#discoverability) settings that you have selected in your submissions.

This option has the same effect as if you created a submission and chose **Make this product available but not discoverable in the Store** with the **Stop acquisition** option. However, it does not require you to create a new submission.

Note that any customers who already have the app will still be able to use it and can download it again (and could even get updates if you submit new packages later).

After making the app unavailable, you'll still see it in Partner Center. If you decide to offer the app to customers again, you can click **Make product available** from the banner on the **App overview** page or you can use the toggle button in **Store presence** card on **App overview** page. After you confirm, the app will be available to new customers (unless restricted by the settings in your last submission) within a few hours.

:::image type="content" source="images/new-overview-make-product-available.png" lightbox="images/new-overview-make-product-available.png" alt-text="A screenshot of the new msix overview page showing how to make product available in Store":::

> [!NOTE]
> If you want to keep your app available, but don't want to continuing offering it to new customers on a particular OS version, you can create a new submission and remove all packages for the OS version on which you want to prevent new acquisitions.

## Removing packages for a previously-supported device family

If you remove all packages for a certain device family (see [Programming with extension SDKs](/uwp/extension-sdks/device-families-overview)) that your app previously supported, you'll be prompted to confirm that this is your intention before you can save your changes on the **Packages** page.

When you publish a submission that removes all of the packages that could run on a device family that your app previously supported, new customers will not be able to acquire the app on that device family. You can always publish another update later to provide packages for that device family again.

Be aware that even if you remove all of the packages that support a certain device family, any existing customers who have already installed the app on that type of device can still use it, and they will get any updates you provide later.

## Adding packages for Windows 10 to an existing app

If you have an app in the Store that only included packages for Windows 8.x and you want to update your app for Windows 10 and 11, create a new submission and add your UWP .msixupload or .appxupload package(s) during the [Packages](../../../apps/publish/publish-your-app/upload-app-packages.md) step. After your app goes through the certification process, the UWP package will also be available for new acquisitions by customers on Windows 10 and 11.

> [!NOTE]
> Once a customer on Windows 10 or 11 gets your UWP package, you can't roll that customer back to using a package for any previous OS version.

Note that the version number of your Windows 10 and 11 packages must be higher than those for any Windows 8, Windows 8.1 packages you have used. For more info, see [Package version numbering](../../../apps/publish/publish-your-app/package-version-numbering.md).

For more info about packaging UWP apps for the Store, see [Packaging apps](/windows/uwp/packaging/).
