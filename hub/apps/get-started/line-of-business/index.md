---
title: Build line-of-business apps with WinUI - overview
description: A hub for developers building enterprise line-of-business apps with WinUI 3, covering data, forms, migration, design, and AI.
ms.topic: overview
ms.date: 09/02/2026
author: GrantMeStrength
ms.author: jken
---

# Build line-of-business apps with WinUI - overview

This hub is for developers building line-of-business (LOB) apps, such as internal tools, data-entry apps, reporting dashboards, and enterprise clients, with WinUI 3 and the Windows App SDK.

WinUI 3 provides Fluent Design controls and XAML data binding. The Windows App SDK delivers WinUI 3 and also provides APIs for app lifecycle, windowing, notifications, and other Windows capabilities. Packaging and deployment are separate architectural choices.

![Diagram showing WinUI 3 and Windows App SDK at the center with five line-of-business capabilities radiating out as peers.](images/lob-journey.png)

## Choose an approach

Use this table as a starting point. The appropriate choice depends on your app's data volume, deployment model, security requirements, and the Windows App SDK version that you target.

| Requirement | Starting point |
|---|---|
| Display a collection of records | Use `ListView` or `ItemsView` with a `DataTemplate`. |
| Display dense tabular data | Use a columnar list for simple read-only scenarios, or evaluate a supported third-party grid when you need grid features such as column resizing, grouping, or spreadsheet-style editing. |
| Validate form input | Choose a validation strategy that fits your architecture. WinUI 3 doesn't provide a complete built-in form-validation framework. |
| Authenticate users | Prefer an identity library and OS broker, such as MSAL with Web Account Manager (WAM), instead of implementing an OAuth flow directly. |
| Store structured data locally | Consider EF Core with SQLite. |
| Access enterprise data | Prefer an authenticated HTTPS service layer. Don't embed shared database credentials in a desktop client. |
| Keep the UI responsive | Use asynchronous APIs and avoid blocking the UI thread. Dispatch UI updates only when work completes on another thread. |
| Add AI features | Choose an on-device Windows AI API, ONNX Runtime, or a cloud AI service based on privacy, hardware, connectivity, and model requirements. |
| Package and deploy | Choose packaged with MSIX, packaged with external location, or unpackaged. Separately choose a framework-dependent or self-contained Windows App SDK deployment. |

## Build a new app

### Display and edit data

- [Display tabular data in a WinUI app](display-tabular-data.md)
- [Data binding overview](../../develop/data-binding/data-binding-overview.md)
- [Data binding in depth](../../develop/data-binding/data-binding-in-depth.md)
- [Data binding and MVVM](../../develop/data-binding/data-binding-and-mvvm.md)
- [List views and grid views](../../develop/ui/controls/listview-and-gridview.md)

### Build forms with validation

- [Build a data-entry form with validation](build-validated-form.md)
- [Controls overview](../../develop/ui/controls/index.md)

The `CommunityToolkit.Mvvm` package remains a Microsoft-maintained option for implementing MVVM. MVVM is not required for every WinUI app, and the toolkit's `ObservableValidator` is one possible validation implementation rather than a WinUI platform standard.

### Connect to data and services

- [Connect a WinUI app to a database](connect-to-a-database.md)
- [HTTP client](../../develop/networking/httpclient.md)
- [Web Account Manager](../../develop/security/web-account-manager.md)
- [OAuth 2.0 and OpenID Connect](../../develop/security/oauth2.md)

### Package and deploy

- [Deployment overview](../../package-and-deploy/deploy-overview.md)
- [Choose a distribution method](../../package-and-deploy/choose-distribution-path.md)

## Modernize an existing app

You don't need to rewrite a working WPF or Windows Forms app to adopt every Windows App SDK capability. You can add supported non-XAML APIs incrementally where they provide value. Full WinUI 3 UI requires a WinUI 3 project or an applicable interop approach.

- [Use the Windows App SDK in an existing project](../../windows-app-sdk/use-windows-app-sdk-in-existing-project.md)
- [Migration decision guide](../../windows-app-sdk/migrate-to-windows-app-sdk/migration-decision-guide.md)
- [Migration strategy overview](../../windows-app-sdk/migrate-to-windows-app-sdk/overall-migration-strategy.md)

### Migrate from WPF

- [WPF patterns and their WinUI 3 equivalents](../../windows-app-sdk/migrate-to-windows-app-sdk/wpf-patterns-winui3.md)

### Migrate from Windows Forms

- [Windows Forms patterns and their WinUI 3 equivalents](migrate-winforms-patterns.md)

### Migrate from UWP

- [UWP to Windows App SDK migration overview](../../windows-app-sdk/migrate-to-windows-app-sdk/migrate-to-windows-app-sdk-ovw.md)
- [WinUI 3 migration guide](../../windows-app-sdk/migrate-to-windows-app-sdk/guides/winui3.md)
- [API mapping table](../../windows-app-sdk/migrate-to-windows-app-sdk/api-mapping-table.md)

## Add AI to your app

Use [Add AI capabilities to a line-of-business WinUI app](ai-for-lob-apps.md) to compare on-device Windows AI, ONNX Runtime, and cloud AI approaches.

## Design for productivity

Use [Design for productivity in WinUI LOB apps](design-for-lob.md) for guidance about theming, materials, accessibility, responsive layouts, and navigation.

## Related content

- [Windows App SDK overview](../../windows-app-sdk/index.md)
- [WinUI 3 overview](../../winui/winui3/index.md)
- [Packaging and deployment overview](../../package-and-deploy/deploy-overview.md)
- [Security overview](../../develop/security/index.md)
