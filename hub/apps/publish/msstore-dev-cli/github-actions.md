---
description: Steps to use GitHub Actions to publish app updates to Microsoft Store.
title: Publish app updates to Microsoft Store with GitHub Actions 
ms.topic: how-to
ms.date: 11/06/2025
---

# Publishing app updates to Microsoft Store with GitHub Actions 

GitHub Actions lets you automate the process of updating your Microsoft Store apps directly from your code repository. By integrating updates into your workflow, you save time, reduce manual errors, and ensure every change is securely tracked and delivered to the Store. 

## Pre-requisite

1. Register as a Windows app developer in [Partner Center](https://storedeveloper.microsoft.com/). 
2. Have a tenant associated with your Partner Center account. You can achieve that by either [associating an existing Microsoft Entra ID in Partner Center](https://learn.microsoft.com/windows/apps/publish/partner-center/associate-existing-azure-ad-tenant-with-partner-center-account) or by [creating a new Microsoft Entra ID in Partner Center](https://learn.microsoft.com/windows/apps/publish/partner-center/create-new-azure-ad-tenant). 
3. [Register](https://learn.microsoft.com/entra/identity-platform/quickstart-register-app) an application in Microsoft Entra ID 
4. Next, from the Microsoft Entra applications tab under User management page in the Account settings section of Partner Center, add the Microsoft Entra ID application that represents the app or service that you will use to access submissions for your Partner Center account. Make sure you assign this application the Manager role. 
5. The app you want to update must already be published and live in Microsoft Store. 
6. Required IDs and Secrets:  
      * Tenant ID (This is the unique identifier for your Microsoft Entra tenant. Go to https://entra.microsoft.com/. Navigate to Azure Active Directory > Overview. Copy the “Tenant ID” value.) 
      * Client ID (This is the Application ID of the app registration you created. In the Entra admin center, go to Azure Active Directory > App registrations. Select your registered app. Copy the “Application ID”.) for API access. In the Entra admin center, go to Azure Active Directory > App registrations. Select your registered app. Copy the “Application ID”.) 
      * Client Secret (This is a password-like value generated for your app registration, used for secure authentication. In the Entra admin center, go to Azure Active Directory > App registrations. Select your registered app, go to Certificates & secrets. Under “Client secrets”, create a new secret if you haven’t already. Copy the value immediately as it will not be shown again) 
      * Seller ID (This is your unique publisher/seller identifier in Microsoft Partner Center. Sign in to [Partner Center](https://partner.microsoft.com/). Go to Account settings > Developer settings or Identifiers. Look for “Publisher ID” or “Seller ID”.) 

     These will be used as secrets in your GitHub repository. 

## Setting up GitHub Actions to update apps on Microsoft Store

If your project already has a GitHub repository, you can use it directly for automating Microsoft Store app updates.

### Step 1

* In your GitHub repo, go to **Settings** > **Secrets and variables** > **Actions** > **New Repository Secret**. 

:::image type="content" source="publish/images/github-actions-repo-secret.png" lightbox="publish/images/github-actions-repo-secret.png" alt-text="A screenshot showing how to add secrets to your repository.":::

* Add the following secrets:
  * TENANT_ID
  * CLIENT_ID
  * CLIENT_SECRET
  * SELLER_ID 

### Step 2

Before publishing updates, you need the base metadata JSON from Partner Center for your app submission. This ensures you start with the correct structure for MSIX or Win32 apps. Create a GitHub Actions workflow under .github/workflows/ (e.g., GetBaseMetadata.yml) with the following snippets:

For MSIX
:::image type="content" source="images/github-actions-base-metadata-msix.png" lightbox="images/github-actions-base-metadata-msix.png" alt-text="A screenshot showing command line to obtain nase metadata for msix apps.":::

For EXE
:::image type="content" source="images/github-actions-base-metadata-exe.png" lightbox="images/github-actions-base-metadata-exe.png" alt-text="A screenshot showing command line to obtain nase metadata for exe apps.":::


1. Navigate to the [Partner Center apps and games page](https://aka.ms/submitwindowsapp).
2. Click **New product**.
3. Click on **MSIX or PWA app**. If you want to submit MSIX or PWA game, click on **Game**.

:::image type="content" source="images/msix-new-product.png" lightbox="images/msix-new-product.png" alt-text="A screenshot showing how to create a MSIX/PWA app.":::

4. Enter the name you'd like to use and click **Check availability**. If the name is available, you'll see a green check mark. If the name is already in use, you'll see a message indicating so.

:::image type="content" source="images/msix-app-name-reservation.png" lightbox="images/msix-app-name-reservation.png" alt-text="A screenshot showing how to reserve a name for MSIX/PWA app.":::

5. Once you've selected an available name that you'd like to reserve, click **Reserve product name**.

> [!NOTE]
> You might find that you cannot reserve a name, even though you do not see any apps listed by that name in the Microsoft Store. This is usually because another developer has reserved the name for their app but has not submitted it yet. If you are unable to reserve a name for which you hold the trademark or other legal right, or if you see another app in the Microsoft Store using that name, [contact Microsoft](https://www.microsoft.com/info/cpyrtInfrg.html).

> [!TIP]
> For guidance on selecting an effective app name, see [How do I choose a great app name for the Microsoft Store](../../faq/submit-your-app.md) in the FAQ section.
