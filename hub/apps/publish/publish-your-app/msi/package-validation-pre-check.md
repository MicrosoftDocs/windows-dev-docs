---
description: Manually run package validation checks before your MSI/EXE app has been submitted for review
title: Package validation for MSI/EXE app
ms.date: 10/30/2022
ms.topic: article
ms.localizationpriority: medium
---

# Package validation for MSI/EXE app

You can validate your app packages against [Microsoft Store policy](/windows/apps/publish/store-policies) section **10.2 Security** before you submit it for review. Manual validation helps reduce validation delay by fixing validation failures before you submit your app for review.

To run the validation checks, follow these steps:

1. Navigate to the Manage Packages page.
2. Click the **Run** next to the app package you'd like to validate in the packages table. This starts the validation checks.

:::image type="content" source="images/msiexe-package-validation-on-packages-page.png" lightbox="images/msiexe-package-validation-on-packages-page.png" alt-text="A screenshot of the Packages section showing how to run package validation.":::

3. Once the tests have completed, you can click on **View status** to see the results of the validation checks.

:::image type="content" source="images/msiexe-package-validation-in-progress.png" lightbox="images/msiexe-package-validation-in-progress.png" alt-text="A screenshot of the package validation section showing the checks in progress.":::

4. You can also see the validation results on the Package Validation page.

:::image type="content" source="images/msiexe-package-validation-completed.png" lightbox="images/msiexe-package-validation-completed.png" alt-text="A screenshot of the package validation section showing the checks are completed.":::

5. If the validation report shows any issues, fix them and run the validation again.

> [!NOTE]
> To run the validation again on the same package, you need to update either the package binary or the silent install parameter.
