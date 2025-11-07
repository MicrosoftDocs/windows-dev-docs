---
description: Steps to use GitHub Actions to publish app updates to Microsoft Store.
title: Publish app updates to Microsoft Store with GitHub Actions 
ms.topic: how-to
ms.date: 11/07/2025
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

:::image type="content" source="../images/github-actions-repo-secret.png" lightbox="../images/github-actions-repo-secret.png" alt-text="A screenshot showing how to add secrets to your repository.":::

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
:::image type="content" source="./images/github-actions-base-metadata-exe.png" lightbox="./images/github-actions-base-metadata-exe.png" alt-text="A screenshot showing command line to obtain nase metadata for exe apps.":::

### Step 3

* [Add the GitHub Action Workflow](https://docs.github.com/en/actions/tutorials/create-an-example-workflow) to invoke the Microsoft GitHub action (microsoft-store-apppublisher) for publishing app metadata and package updates to store. 
* Under .github/workflows/, create a YAML file (e.g., AppMetadataUpdate.yml or AppPackageUpdate.yml). 
* Here’s an example workflow snippet: 

:::image type="content" source="./images/github-actions-workflow.png" lightbox="./images/github-actions-workflow.png" alt-text="A screenshot showing metadata update YAML file.":::

### Step 4

* For metadata updates: Execute the workflow by providing the path to updated metadata which will get published to the Microsoft Store. 
* For package updates: Execute the workflows by pointing to the location of your newly published package (e.g., PackageName.msix for MSIX apps or PackageName.json for EXE apps)

### Step 5

* Run the workflow by going to the Actions tab in your GitHub repository, selecting the relevant workflow, and clicking Run workflow. 

:::image type="content" source="./images/github-actions-run-workflow.png" lightbox="./images/github-actions-run-workflow.png" alt-text="A screenshot showing the process of running a workflow.":::

* The workflow will do the following in the background:  
  * Invoke the GitHub Action (microsoft-store-apppublisher) 
  * Authenticate your Microsoft Store Partner Center account using the secrets you configured (Tenant ID, Client ID, Client Secret, Seller ID). 
  * Use the Microsoft Store Developer CLI (msstore) to publish the updated metadata or package to the Microsoft Store. 

### Step 6

Once the workflow is executed successfully, check the Microsoft Store to confirm your changes are live after certification.
