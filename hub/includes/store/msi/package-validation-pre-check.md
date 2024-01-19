---
ms.date: 5/22/2023
ms.topic: include
ms.service: windows
---

You can validate your app packages against [Microsoft Store policy](/windows/apps/publish/store-policies) section **10.2 Security** before you submit it for review. Manual validation helps reduce validation delay by fixing validation failures before you submit your app for review.

To run the validation checks, follow these steps:

1. Navigate to the Manage Packages page.
1. Click the **Run** next to the app package you'd like to validate in the packages table. This starts the validation checks.
1. Once the tests have completed, you can click on **View status** to see the results of the validation checks.
1. You can also see the validation results on the Package Validation page.
1. If the validation report shows any issues, fix them and run the validation again.

> [!NOTE]
> To run the validation again on the same package, you need to update either the package binary or the silent install parameter.
