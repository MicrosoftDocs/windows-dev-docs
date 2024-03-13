Each package you provide must have a version number (provided as a value in the **Version** attribute of the [Package/Identity](/uwp/schemas/appxpackage/uapmanifestschema/element-identity) element in the app manifest). The Microsoft Store enforces certain rules related to version numbers, which work somewhat differently in different OS versions.

> [!NOTE]
> This topic refers to "packages", but unless noted, the same rules apply to version numbers for both .msix/.appx and .msixbundle/.appxbundle files.

## Version numbering for Windows 10 and 11 packages

> [!IMPORTANT]
> For Windows 10 or Windows 11 (UWP) packages, the last (fourth) section of the version number is reserved for Store use and must be left as 0 when you build your package (although the Store may change the value in this section). The other sections must be set to an integer between 0 and 65535 (except for the first section, which cannot be 0).

When choosing a UWP package from your published submission, the Microsoft Store will always use the highest-versioned package that is applicable to the customer’s Windows 10 or Windows 11 device. This gives you greater flexibility and puts you in control over which packages will be provided to customers on specific device types. Importantly, you can submit these packages in any order; you are not limited to providing higher-versioned packages with each subsequent submission.

You can provide multiple UWP packages with the same version number. However, packages that share a version number cannot also have the same architecture, because the full identity that the Store uses for each of your packages must be unique. For more info, see [**Identity**](/uwp/schemas/appxpackage/uapmanifestschema/element-identity).

When you provide multiple UWP packages that use the same version number, the architecture (in the order x64, x86, Arm, neutral) will be used to decide which one is of higher rank (when the Store determines which package to provide to a customer's device). When ranking app bundles that use the same version number, the highest architecture rank within the bundle is considered: an app bundle that contains an x64 package will have a higher rank than one that only contains an x86 package.

This gives you a lot of flexibility to evolve your app over time. You can upload and submit new packages that use lower version numbers to add support for Windows 10 or Windows 11 devices that you did not previously support, you can add higher-versioned packages that have stricter dependencies to take advantage of hardware or OS features, or you can add higher-versioned packages that serve as updates to some or all of your existing customer base.

The following example illustrates how version numbering can be managed to deliver the intended packages to your customers over multiple submissions.

### Example: Moving to a single package over multiple submissions

Windows 10 enables you to write a single codebase that runs everywhere. This makes starting a new cross-platform project much easier. However, for a number of reasons, you might not want to merge existing codebases to create a single project right away.

You can use the package versioning rules to gradually move your customers to a single package for the Universal device family, while shipping a number of interim updates for specific device families (including ones that take advantage of Windows 10 APIs). The example below illustrates how the same rules are consistently applied over a series of submissions for the same app.

| Submission | Contents                                                  | Customer experience                                                                                                                                                                             |
|------------|-----------------------------------------------------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| 1          | -   Package version: 1.1.10.0<br> -   Device family: Windows.Desktop, minVersion 10.0.10240.0    | -   Devices on Windows 10 and 11 Desktop build 10.0.10240.0 and above will get 1.1.10.0<br> -   Other device families will not be able to purchase and install the app |
| 2          | -   Package version: 1.1.10.0<br> -   Device family: Windows.Desktop, minVersion 10.0.10240.0<br><br> -   Package version: 1.0.0.0<br> -   Device family: Windows.Universal, minVersion 10.0.10240.0    | -   Devices on Windows 10 and 11 Desktop build 10.0.10240.0 and above will get 1.1.10.0<br>  -   Other (non-desktop) device families when they are introduced will get 1.0.0.0<br> -   Desktop devices that already have the app installed will not see any update (because they already have the best available version 1.1.10.0 and are higher than 1.0.0.0) |
| 3          | -   Package version: 1.1.10.0<br> -   Device family: Windows.Desktop, minVersion 10.0.10240.0<br><br> -   Package version: 1.1.5.0<br> -   Device family: Windows.Universal, minVersion 10.0.10250.0<br><br> -   Package version: 1.0.0.0<br> -   Device family: Windows.Universal, minVersion 10.0.10240.0    | -   Devices on Windows 10 and 11 Desktop build 10.0.10240.0 and above will get 1.1.10.0<br> -   Other (non-desktop) device families when introduced with build 10.0.10250.0 and above will get 1.1.5.0<br> -   Other (non-desktop) device familes when introduced with build >=10.0.10240.0 and < 10.010250.0 will get 1.1.0.0<br> -   Desktop devices that already have the app installed will not see any update (because they already have the best available version 1.1.10.0 which is higher than both 1.1.5.0 and 1.0.0.0)|
| 4          | -   Package version: 2.0.0.0<br> -   Device family: Windows.Universal, minVersion 10.0.10240.0   | -   All customers on all device families on Windows 10 and 11 build v10.0.10240.0 and above will get package 2.0.0.0 |

> [!NOTE]
> In all cases, customer devices will receive the package that has the highest possible version number that they qualify for. For example, in the third submission above, all desktop devices will get v1.1.10.0, even if they have OS version 10.0.10250.0 or later and thus could also accept v1.1.5.0. Since 1.1.10.0 is the highest version number available to them, that is the package they will get.

### Using version numbering to roll back to a previously-shipped package for new acquisitions

If you keep copies of your packages, you'll have the option to roll back your app’s package in the Store to an earlier Windows 10 package if you should discover problems with a release. This is a temporary way to limit the disruption to your customers while you take time to fix the issue.

To do this, create a new [submission](../../../apps/publish/publish-your-app/create-app-submission.md). Remove the problematic package and upload the old package that you want to provide in the Store. Customers who have already received the package you are rolling back will still have the problematic package (since your older package will have an earlier version number). But this will stop anyone else from acquiring the problematic package, while allowing the app to still be available in the Store.

To fix the issue for the customers who have already received the problematic package, you can submit a new Windows 10 package that has a higher version number than the bad package as soon as you can. After that submission goes through the certification process, all customers will be updated to the new package, since it will have a higher version number.
