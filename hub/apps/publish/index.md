---
description: Learn how to publish your Windows apps and games to the Microsoft Store.
title: Publish Windows apps and games to Microsoft Store
ms.date: 10/30/2022
ms.topic: article
keywords: windows 10, uwp, publishing, publish, selling, sell, distribute, distributing, store, dashboard
ms.assetid: 631d1e2d-e4da-4740-ace0-4c0ad78653fe
ms.localizationpriority: medium
---

# Get started: Publish your first app in the Microsoft Store

:::image type="content" source="./images/store-image.png" lightbox="./images/store-image.png" alt-text="Microsoft Store welcome banner":::

## Introduction

Distributing apps via Microsoft Store is a good choice for developers of any app type and size. The Microsoft Store is a centralized hub for Windows users to discover and install a wide range of apps, providing developers access to a vast audience of over a billion users across Windows 10 and Windows 11. Microsoft Store also offers you various ways to make money from your apps, and lets you choose your own commerce platform and revenue sharing model. Microsoft Store also supports a wide range of app types and technologies, and allows you to bring your traditional desktop apps to the Store without changing your code or installer. 

For the full benefits of publishing your app into the Microsoft Store, please visit [benefits of distributing your apps via Microsoft Store.](https://go.microsoft.com/fwlink/?linkid=2278501)

## Prerequisite

A Windows app developer account in Partner Center is needed before you start the app submission process. There are 2 types of developer accounts available in Partner Center: **Individual and Company**. 

To understand how to open developer account, you can watch the following video:

### [Open individual developer account](#tab/individual)

Who should select an individual account:
- **Independent developers** whose distribution of apps through the Store is **not in relation to their business, trade, or profession**
- **Small scale creators** producing content for non-commercial purposes 
- Individuals creating digital content as a **hobbyist, amateur, school, or personal project**
</br>

The individual account costs approximately **$19 USD**. It is a one-time registration fee. (The fees varies depending on your [country or region](./partner-center/account-types-locations-and-fees.md#developer-account-and-app-submission-markets))

</br>

>[!VIDEO https://learn-video.azurefd.net/vod/player?id=663fa063-4205-4b88-a473-4a2782d49197]

For more details, refer [steps to create individual developer account](../publish/partner-center/open-a-developer-account.md).

### [Open company developer account](#tab/company)

Who should select a company account:
- **Independent developers and freelancers** whose distribution of apps through the Store is **in relation to their business, trade, or profession**
- **Businesses and Organizations** such as corporations, LLCs, partnerships, non-profits, or government organizations
- **Teams or Groups** within a company or organization
</br>

The company account costs approximately **$99 USD**. It is a one-time registration fee. (The fees varies depending on your [country or region](./partner-center/account-types-locations-and-fees.md#developer-account-and-app-submission-markets))

</br>

>[!VIDEO https://learn-video.azurefd.net/vod/player?id=2125b1c6-20d4-47da-ba76-d1ff17b89cc2]

For more details, refer [steps to create company developer account](/windows/apps/publish/partner-center/open-a-developer-account?tabs=company).

---

## Get started with app submission

### [MSIX or PWA](#tab/msix-pwa-getting-started)

**We recommend packaging your app (which is built with any app framework - UWP, Win32, PWA, WinApp SDK etc.), as [MSIX](/windows/msix/overview)**. By packaging your app as MSIX, you can take advantages of many features like a complimentary binary hosting (provided by Microsoft), complementary code signing (provided by Microsoft), Microsoft Store commerce platform, package flighting, advanced integration with Windows (to use features like share dialog, launch from Store etc), Windows 11 backup and restore etc.

For packaging your Win32 app as MSIX, follow these [steps](https://aka.ms/packagewin32asmsix).

**Note:** If you distribute your application as a web download (EXE /MSI) and you are planning to distribute it as a packaged application (MSIX) in the Store, you might want to prevent users from installing both versions or migrate users from the unpackaged web version to the Store version. Learn more about [transitioning users from web unpackaged to Store packaged app](../distribute-through-store/how-to-transition-users-from-your-web-unpackaged-app-to-store-packaged-app.md).

## Choice of commerce platform

Microsoft Store offers developers a flexible and transparent revenue sharing model (including in-app purchases, subscriptions, ads, and tips) that lets you choose your own commerce platform and keep 100% of the revenue for non-gaming apps, or use [Microsoft’s commerce platform](/windows/uwp/monetize/in-app-purchases-and-trials) and pay a competitive fee of 12% for games and 15% for apps. This means that you can maximize your profit and control your business model, while benefiting from the convenience and security of Microsoft’s commerce platform.

## App submission [MSIX/PWA]

To understand the submission process of MSIX apps, you can watch the following video.

</br>

>[!VIDEO https://learn-video.azurefd.net/vod/player?id=4f68f734-a116-4cae-a0a3-89a415087e61]

For more details, refer to the steps below.

#### Reserve your app's name [MSIX/PWA]

All apps on the Microsoft Store must have a unique name. To secure a name for your app, the first step is to reserve it, which you can do up to three months before publishing, even if development has not started.

Steps to Reserve Your App's Name:

1. Navigate to the [Partner Center apps and games page.](https://aka.ms/submitwindowsapp)

2. Click **New product**.

3. Click on **MSIX** or **PWA** app. If you want to submit an MSIX or PWA game, click on **Game**.

4. Enter the name you would like to use and click **Check availability**. If the name is available, you will see a green check mark. If the name is already in use, you will see a message indicating so.

5. Once you have selected an available name that you would like to reserve, click **Reserve product name.**

For more information, please visit [Reserve your app's name - Windows apps | Microsoft Learn.](./publish-your-app/msix/reserve-your-apps-name.md)

#### Create your app submission [MSIX/PWA]

1. After reserving an app name, you will be redirected to Application overview page. Click on **Start Submission**. A product submission in draft status will appear. This draft includes all the submission steps that need to be completed.

2. For submitting a PWA, [learn how to convert your website into a PWA.](./publish-your-app/pwa/turn-your-website-pwa.md)

3. Complete each tab of the submission draft:

   - **Pricing and Availability**: Provide the price of your app, where it will be available to customers and decide whether you'll offer a free trial by setting pricing and availability for your app. [Set app pricing and availability - Windows apps | Microsoft Learn](./publish-your-app/msix/price-and-availability.md)

   - **Properties**: Define your app’s category, secondary category, sub-category and provide system requirements for your app, by entering your app properties. [Enter app properties - Windows apps | Microsoft Learn](./publish-your-app/msix/enter-app-properties.md)

   - **Age Ratings**: For your app to receive the appropriate age and content ratings administered by the IARC rating system, answer age rating questionnaire and generate the age rating. [Age ratings for MSIX apps - Windows apps | Microsoft Learn](./publish-your-app/msix/age-ratings.md)

   - **Packages**: Upload the app packages for your app which will be downloaded by the Store user. [Upload app packages - Windows apps | Microsoft Learn](./publish-your-app/msix/upload-app-packages.md)

   - **Store Listings**: Enter the app description, screenshots, and logos that will be displayed on your app’s Product Details Page (PDP) in the Store, by creating a Store listing. [Create app store listings - Windows apps | Microsoft Learn](./publish-your-app/msix/create-app-store-listing.md)

   - **Submission options (optional)**: Enter notes for certification or decide when your app should publish to the Store using submission options. [Manage submission options - Windows apps | Microsoft Learn](./publish-your-app/msix/manage-submission-options.md)

4. After you complete a section, click on **Save**. Once all sections are complete and you are ready to submit, click on **Submit for certification**.

If your draft is missing any necessary information, it will be marked as **Incomplete.**

For more information, please visit [Create an app submission - Windows apps | Microsoft Learn.](./publish-your-app/msix/create-app-submission.md)

## App certification process [MSIX/PWA]

When you finish creating your app's submission and click Submit to the Store, the submission enters the certification step. This process can take up to three business days. During certification, we will perform security tests, technical compliance tests and also a content compliance check on you app submission. You will be notified if your submission fails any of these tests. 

After your submission passes certification, on an average, customers will be able to see the app’s listing within 15 minutes depending on their location. You will be notified when your submission is published to the Store, and the app's status in the dashboard change to 'In Microsoft Store'.

To understand the certification process of MSIX apps, you can watch the following video. 

</br>

>[!VIDEO https://learn-video.azurefd.net/vod/player?id=4f55d620-642f-4653-8fb4-1d2e49e3dff4]

For more information, please visit [The app certification process - Windows apps | Microsoft Learn.](./publish-your-app/msix/app-certification-process.md)

## Post-certification [MSIX/PWA]

#### Analyze performance for MSIX apps and games

View detailed analytics for your apps and games in Partner Center. Statistics and charts let you know how your apps are performing in the Store; from how many customers you have reached to how they are using your app and what they have to say about it. You can also find metrics on app install, app health, app usage, and more. You can view analytic reports right in Partner Center or download the reports you need to analyze your data offline. We also provide several ways for you to access your analytics data outside of Partner Center.

To understand how to analyze your MSIX app's performance, you can watch the following video. 

</br>

>[!VIDEO https://learn-video.azurefd.net/vod/player?id=fa8f1e24-e923-4ec1-b4b4-83956ef3ed51]

For more details, you can refer to the following reports.

#### Reports available for your apps

 There are different reports available for your apps, check the descriptions to find the metrics you are looking for.

| Report                                                                                   | Description                                                                                                                                                               |
| ---------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| [Acquisitions report](/partner-center/apps-and-msix-games-acquisitions-report?tabs=msix) | See how many people have seen and installed your app in Store. You can also review data for different acquisition channels, markets, and platform details in this report. |
| [Add-on acquisition report](/partner-center/add-on-acquisitions-report)                  | See how many add-ons you have sold, along with demographic and platform details.                                                                                          |
| [Usage report](/partner-center/usage-report)                                             | See how customers on Windows 10 or Windows 11 (including Xbox) are using your app, including data about custom events that you have defined.                              |
| [Health report](/partner-center/health-report)                                           | Get data related to the performance and quality of your app, including crashes and unresponsive events.                                                                   |
| [Reviews report](/partner-center/reviews-report)                                         | See the rating and reviews your customers have left for your app and provide responses to let customers know you are listening to their feedback.                         |
| [Insights report](/partner-center/insights-report)                                       | See meaningful insights about your app like significant (changes increases or decreases that we detected over the last 30 days in your acquisitions and health data).     |

## Appendix [MSIX/PWA]

### Useful links

- [Microsoft Store Developer CLI documentation](../publish/msstore-dev-cli/overview.md)
- [Store Policies and Code of Conduct](../publish/store-policies-and-code-of-conduct.md)

### Troubleshooting

- [Resolve submission errors](./publish-your-app/msix/resolve-submission-errors.md)
- [Avoid common certification failures](./publish-your-app/msix/resolve-submission-errors.md#avoid-common-certification-failures)

### Full app submission documentation

- If you are seeking more detailed documentation, click [here](./publish-your-app/msix/reserve-your-apps-name.md).

### Contact information for support

- If you need further assistance, a support ticket can be raised from [here.](https://aka.ms/windowsdevelopersupport)

### [MSI or EXE](#tab/msi-exe-getting-started)

Microsoft Store has allowed unpackaged applications since June 2021. To publish your application on the Store, you only need to share a link to your installer through the Partner Center and provide some additional information. Once your installer has been tested by our certification team and the submission is published, users will be able to locate your application in the Store and proceed with the installation.

For your installer to be accepted, it must adhere to the following recommendations:

- Must be a .msi or a .exe installer.
- Must be offline
- The binary hosted by the shared URL should remain unchanged.
- Your installer should only install the product intended by the user.

## App submission [MSI/EXE]

To understand the submission process of MSI/EXE (which is to be submitted in its original unpackaged form), you can watch the following video. 

</br>

>[!VIDEO https://learn-video.azurefd.net/vod/player?id=b643ac0c-d3a3-4bd0-b946-89489fc38a8a]

For more details, refer to the steps below.

#### Reserve your app's name [MSI/EXE]

All apps on the Microsoft Store must have a unique name. To secure a name for your app, the first step is to reserve it, which you can do up to three months before publishing, even if development has not started.

Steps to Reserve Your App's Name:

1. Navigate to the [Partner Center apps and games page](https://aka.ms/submitwindowsapp).

2. Click **New product.**

3. Click on **EXE** or **MSI** app.

4. Enter the name you would like to use and click **Check availability.** If the name is available, you will see a green check mark. If the name is already in use, you will see a message indicating so.

5. Once you have selected an available name that you would like to reserve, click **Reserve product name.**

For more information, please visit [Reserve your app's name - Windows apps | Microsoft Learn.](./publish-your-app/msi/reserve-your-apps-name.md)

#### Create your app submission [MSI/EXE]

Once you reserve your app name, you'll be automatically directed to the availability section of the submission process. This section acts as a draft for your store submission.

1. Complete each tab of the submission draft:

   - **Availability**: Provide the price of your app, where it will be available to customers and decide whether you'll offer a free trial by setting pricing and availability for your app. [Set app pricing and availability - Windows apps | Microsoft Learn.](./publish-your-app/msi/price-and-availability.md)

   - **Properties**: Define your app’s category, secondary category, sub-category and provide system requirements for your app, by entering your app properties. [Enter app properties - Windows apps | Microsoft Learn.](./publish-your-app/msi/enter-app-properties.md)

   - **Age Ratings**: For your app to receive the appropriate age and content ratings administered by the IARC rating system, answer age rating questionnaire and generate the age rating. [Age ratings for MSI and EXE apps - Windows apps | Microsoft Learn.](./publish-your-app/msi/age-ratings.md)

   - **Packages**: Upload the app packages for your app which will be downloaded by the Store user. [Upload app packages - Windows apps | Microsoft Learn.](./publish-your-app/msi/upload-app-packages.md)

   - **Store Listings**: Enter the app description, screenshots, and logos that will be displayed on your app’s Product Details Page (PDP) in the Store, by creating a Store listing. [Create app store listings - Windows apps | Microsoft Learn.](./publish-your-app/msi/create-app-store-listing.md)

2. After you complete a section, click on **Save**. Once all sections are complete and you are ready to submit, click on **Submit**.

For more information, please visit [Create an app submission - Windows apps | Microsoft Learn.](./publish-your-app/msi/create-app-submission.md)

## App certification process [MSI/EXE]

When you finish creating your app's submission and submit it to the Microsoft Store, the submission enters the certification step. This process can take up to three business days.During certification, we will perform security and content compliance tests on your app submission. We will also follow any instructions that you had mentioned in 'Notes for certification'. You will be notified if your submission fails any of these tests. 

After your submission passes certification, on an average, customers will be able to see the app’s listing within 15 minutes depending on their location. When your submission is published to the Store, you will be notified and the app's status in the dashboard will change to 'In Microsoft Store'.

To understand the certification process of MSI/EXE apps, you can watch the following video. 

</br>

>[!VIDEO https://learn-video.azurefd.net/vod/player?id=740d085c-02f4-4eed-b81f-af155007dc81]

For more information, please visit [The app certification process - Windows apps | Microsoft Learn.](./publish-your-app/msi/app-certification-process.md)

## Post-certification [MSI/EXE]

#### Analyze performance for MSI or EXE apps

In Partner Center, you have access to detailed analytics for your MSI or EXE application. Utilize statistics and charts to monitor the performance of your applications including insights into customer reach and feedback. You can also explore metrics related to app discoverability, health, usage, and other relevant data.

You can view analytic reports right in Partner Center or download the reports you need, to analyze your data offline. When viewing your analytic reports, you will see an arrow icon within each chart for which you can download data. Click the arrow to generate a downloadable .tsv file, which you can open in Microsoft Excel or another program that supports tab-separated values (TSV) files.

To understand how to analyze your MSI/EXE app's performance, you can watch the following video. 

</br>

>[!VIDEO https://learn-video.azurefd.net/vod/player?id=736b044d-dd75-452f-b24b-7b9a5dee0ccb]

For more details, you can refer to the following reports.

#### Reports available for your apps

There are different reports available for your apps, check the descriptions to find the metrics you are looking for.

| Report                                                                                   | Description                                                                                                                                                                    |
| ---------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| [Overview Report](../publish/analyze-msi-exe/analyze-app-performance.md#overview-report) | Shows information across a cross section of metrics such as Usage, Health and Ratings and reviews.                                                                             |
| [Installs Report](../publish/analyze-msi-exe/analyze-app-performance.md#installs-report) | Shows how many people have seen your app in Store and installed it. You can also review data for different acquisition channels, markets, and platform details in this report. |
| [Usage report](../publish/analyze-msi-exe/analyze-app-performance.md#usage-report)       | Shows details about how your customers are using your app over the selected period of time.                                                                                    |
| [Health report](../publish/analyze-msi-exe/analyze-app-performance.md#health-report)     | Shows data related to the performance and quality of your app, including crashes and unresponsive events.                                                                      |
| [Ratings & Reviews report](../publish/analyze-msi-exe/ratings-reviews-performance.md)    | See the rating and reviews your customers have left for your app and provide responses to let customers know you are listening to their feedback.                              |

## Appendix [MSI/EXE]

### Useful links

- [Microsoft Store Developer CLI documentation](../publish/msstore-dev-cli/overview.md)
- [Store Policies and Code of Conduct](../publish/store-policies-and-code-of-conduct.md)

### Full app submission documentation

- If you are seeking more detailed documentation, click [here](./publish-your-app/msi/reserve-your-apps-name.md).

### Contact information for support

- If you need further assistance, a support ticket can be raised from [here.](https://aka.ms/windowsdevelopersupport)