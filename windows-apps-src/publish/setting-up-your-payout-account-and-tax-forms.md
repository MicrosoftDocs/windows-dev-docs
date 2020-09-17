---
Description: In order to receive money from app sales in the Microsoft Store, you need to set up your payout account and fill out the necessary tax forms.
title: Set up your payout account and tax forms
ms.assetid: 690A2EBC-11B1-4547-B422-54F15A6C26A7
ms.date: 1/17/2020
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Set up your payout account and tax forms

> [!NOTE]
> If you're looking for support regarding payouts, including configuring payout accounts, missing payouts, putting payouts on hold, or anything else, contact support [here](https://partner.microsoft.com/support).

In order to receive money from app sales in the Microsoft Store, you need to set up your payout account and fill out the necessary tax forms in [Partner Center](https://partner.microsoft.com/dashboard).

If you only plan to list free apps (and don't plan to offer in-app purchases or use Microsoft Advertising), you don't need to set up a payout account or fill out any tax forms. If you change your mind later and decide you do want to sell apps (or add-ons), you can set up your payout account and fill out tax forms at that time. You won't be able to submit any paid apps or add-ons until your payout account and tax profile have been completed.

> [!NOTE]
> In [certain markets](account-types-locations-and-fees.md#developer-account-and-app-submission-markets), developers can only submit free apps. If your account is registered in one of these markets, you will not have the option to set up a payout account.

After you have [set up your developer account](opening-a-developer-account.md), there are two things you need to do before you can sell apps (or add-ons) in the Microsoft Store:

- [Fill out your tax forms](#tax-forms)
- [Set up your payout account](#payout-account)

> [!NOTE]
> For details about how and when you will be paid for the money your apps make, see [Getting paid](getting-paid-apps.md).

## Tax forms

### Filling out your tax forms

First, you'll need to create a tax profile and assign it to the programs you participate in. You can create your *tax profile* for the Microsoft Store by completing the following steps:

- Specify your country of residence and citizenship.
- Fill out the appropriate tax forms.

You can complete and submit your tax forms electronically in Partner Center; in most cases, you don't need to print and mail any forms.

> [!IMPORTANT]
> Different countries and regions have different tax requirements. The exact amount that you must pay in taxes depends on the countries and regions where you sell your apps. See the [App Developer Agreement](/legal/windows/agreements/app-developer-agreement) to find out for which countries Microsoft remits sales and use tax on your behalf. In other countries, depending on where you are registered, you may need to remit sales and use tax for your app sales directly to the local taxing authority. In addition, the app sales proceeds you receive may be taxable as income. We strongly encourage you to contact the relevant authority for your country or region that can best help you determine the right tax info for your Microsoft Store developer activities.

1. In [Partner Center](https://partner.microsoft.com/dashboard), select the **Account settings** icon in the top right corner, then select **Developer settings**.
2. In the left navigation menu, select **Payout and tax**, then select **Payout and tax assignments**.

    ![Payout and tax profile assignment](images/payout-tax-profile-assignment.png)

3. Select the program and seller id combination for which you want to configure tax information.

    ![Payout select seller id](images/payout-select-seller-id.png)

4. If you would like to use an existing tax profile, select it from the dropdown. Otherwise, select **Create new profile** and press **Submit**. You will be taken to the tax profiles page.
5. Click the **Edit** button to edit your tax information.
6. Select the appropriate radio button, and select your country if prompted. This step determines the Microsoft business entity that will be used to make payouts on your account.

    ![Payout select tax country](images/payout-select-tax-country.png)

7. Depending on your selections in step 6, you will be prompted to provide tax information required for your country.

> [!NOTE]
> Regardless of your country of residence or citizenship, you must fill out United States tax forms to sell any apps or add-ons through the Microsoft Store. Developers who satisfy certain United States residency requirements must fill out an IRS W-9 form. Other developers outside the United States must fill out an IRS W-8 form. You can fill out these forms online as you complete your tax profile.

### Withholding rates

The info you submit in your tax forms determines the appropriate tax withholding rate. The withholding rate applies only to sales that you make into the United States; sales made into non-US locations are not subject to withholding. The withholding rates vary, but for most developers registering outside the United States, the default rate is 30%. You have the option of reducing this rate if your country has agreed to an income tax treaty with the United States.

### Tax treaty benefits

If you are outside the United States, you may be able to take advantage of tax treaty benefits. These benefits vary from country to country, and may allow you to reduce the amount of taxes that the Microsoft Store withholds. You can claim tax treaty benefits by completing Part II of the W-8BEN form. We recommend that you communicate with the appropriate resources in your country or region to determine whether these benefits apply to you.

> [!NOTE]
> A United States Individual Taxpayer Identification Number (or ITIN) is not required to receive payments from Microsoft or to claim tax treaty benefits.

## Payout account

A payout account is the bank account to which we send the proceeds from your sales. You can view all payment accounts that you have entered on the Profile page.

> [!NOTE]
> In some markets, PayPal can be used for your payout account. See [Payment thresholds, methods, and timeframes](payment-thresholds-methods-and-timeframes.md) to find out if PayPal is supported for a specific market, and read the [PayPal info](#paypal-info) below for more details.

### Create a payment profile

1. In [Partner Center](https://partner.microsoft.com/dashboard), select the **Settings** gear icon in the top right corner, then select **Developer settings**.
2. Underneath the *Payout and tax* heading, select **Payout and tax profile assignment**.

    > [!NOTE]
    > Because this is sensitive info, you may be prompted to sign in again.

3. Select the payment method you would like to configure.

    ![Payout account type selection](images/payout-account-type-selection.png)

4. Select an existing payment profile, or click **Create a new payment profile** to create a new profile for the chosen payment method.

> [!NOTE]
> If, for some reason, your account is not ready to receive funds from Microsoft, you may check the **Hold my payment** checkbox. You will continue to earn proceeds from your sales, but payments will not be distributed until you disable **Hold my payment.**

### Create a bank-based payment profile

If you elected to use a bank account to receive payouts, you'll complete the following process to configure your bank account.

1. On the *Bank Profile* page, provide the required information about your bank.
2. Provide your bank account details.

    > [!NOTE]
    > The fields you use to provide your account info accept only alphanumeric characters.

    ![Payout bank info](images/payout-bank-info.png)

3. Provide beneficiary details.
4. Back on the *Profile assignment* page, select the currency you would like us to use when we issue your payouts.

    > [!WARNING]
    > Make sure your bank accepts the payout currency you select.

5. You will need to select a payment profile for each program you participate in, though you can use the same profile for multiple programs.

    ![Payout use bank profile](images/payout-use-bank-profile.png)

6. Click submit to save your changes.

> [!NOTE]
> Microsoft may take up to 48 hours to validate the information in your profile. When this process is complete *verification status* will show **Complete**

To ensure your payout is successful, please also keep in mind the following:

- The **Account holder name** entered for your payout account in Partner Center must be the exact same name associated with your bank account. For example, if your bank account name contains a middle name, add a middle name to your **Account holder name**.
- Payouts are transferred directly from Microsoft to your bank account in USD currency.
- Bank information entered in Partner Center in Latin characters is translated to Cyrillic characters.

### Editing existing payment profiles

You can edit existing payment profiles if you need to make changes or correct any incorrect information.

1. In [Partner Center](https://partner.microsoft.com/dashboard), select the **Settings** gear icon in the top right corner, then select **Developer settings**.
2. Underneath the *Payout and tax* heading, select **Payout and tax profiles**.
3. Your payment profiles will be listed along with their status. Find the profile you wish to edit and click **Edit** at the far right

> [!IMPORTANT]
> Changing your payout account can delay your payments by up to one payment cycle. This delay occurs because we need to verify the account change, just as we did when you first set up the payout account. You'll still get paid for the full amount after your account has been verified; any payments due for the current payment cycle will be added to the next one. See [Getting paid](getting-paid-apps.md) for more info.

### PayPal info

In select countries and regions, you can create a payment account by entering your PayPal info. However, before choosing PayPal as a payment account option:

- Check [Payment thresholds, methods, and timeframes](payment-thresholds-methods-and-timeframes.md) to confirm whether PayPal is a supported payment method in your country or region.
- Review the following FAQs. Depending on your situation, PayPal may not be the best payment account option for you, and a bank account may be preferred.

Common questions about using PayPal as a payment method:

- **What PayPal settings do I need to have in order to receive payments?** You must ensure that your PayPal account does not block eCheck payments. This setting is managed in PayPal’s Payment Receiving Preferences page. See [PayPal’s account setup page](https://developer.paypal.com/webapps/developer/docs/classic/admin/setup-account/) for more info.
- **Is my country/region supported?** See [Payment thresholds, methods, and timeframes](payment-thresholds-methods-and-timeframes.md) to find out where PayPal is a supported payment method.
- **Does my PayPal account have to be registered in the same country/region as my Partner Center account?** No. When you set up a PayPal account, you can accept the default configuration. You shouldn’t have any issues with other countries/regions and currencies unless you have blocked payment in some currencies. This setting is managed in PayPal’s Payment Receiving Preferences page.
- **Do I have to accept PayPal payments manually?** No. PayPal accounts are set by default to require users to accept payments manually, which means if you don’t accept the payment within 30 days, it is returned. You can change this setting by turning off “Ask Me” in PayPal’s More Settings page.
- **What currencies does PayPal support?** Please see [PayPal's support page](https://developer.paypal.com/docs/classic/api/currency-codes/#paypal) for the current list

### Specific requirements for certain countries/regions

In some countries and regions, additional requirements for payout accounts must be followed. If you are a resident of Pakistan, Russia, or Ukraine, please note the following requirements.

#### Pakistan

Form-R is a Pakistan banking regulatory requirement. It is used to indicate the purpose and reason for receipt of funds from abroad. Therefore, anytime that you are eligible for a monthly payout from Microsoft, you will need to submit a Form-R to your bank before the payout can be released to your account. Contact your local bank branch for instructions on how to obtain a copy of Form-R.

You will need to submit a Form-R to your bank each month that you are eligible for a payout. For example, if you expect to receive a payout every month of the year, you will need to submit a Form-R 12 times (once each month).

Once the payout has been submitted to your bank, you have 30 days to submit a Form-R. If it is not submitted within 30 days, the funds will be returned to Microsoft.

#### Russia

If you’re a developer who lives in Russia, you may need to provide documentation to your bank before your bank will deposit funds into your account. When you’re eligible to be paid, we will provide you with the following documentation in an email message:

1. Acceptance Certificate (AC) – contains the amount of payout being transferred to your account.
2. App Developer Agreement (ADA) – a signed copy of the developer agreement that needs to be counter signed.

To ensure your payout is successful, please also keep in mind the following:

- The **Account holder name** entered for your payout account in Partner Center must be the exact same name associated with your bank account. For example, if your bank account name contains a middle name, add a middle name to your **Account holder name**.
- Payouts are transferred directly from Microsoft to your bank account in Ruble (RUB) currency.
- Bank information entered in Partner Center in Latin characters is translated to Cyrillic characters.
- Payouts must be made to a bank account and not to a bank card.

#### Ukraine

If you’re a developer who lives in Ukraine, you may need to provide documentation to your bank before your bank will deposit funds into your account. When you’re eligible to be paid, we will provide you with the following documentation in an email message:

1. Acceptance Certificate (AC) – contains the amount of payout being transferred to your account.
2. App Developer Agreement (ADA) – a signed copy of the developer agreement that needs to be counter signed.
3. Amendment Agreement (AA) – this document can be used by your bank to help identify your payout funds.

Microsoft provides all three documents when your first payout is attempted. For any subsequent payouts, you will only receive the AC document. Please retain the ADA and AA documents in case you need them to receive future payouts from your bank.

### Create a PayPal payment profile

If you elected to use a bank account to receive payouts, you'll complete the following process to configure your bank account.

1. On the *PayPal* page, provide the required information about your PayPal account.
2. Provide your paypal account details.

    > [!NOTE]
    > The fields you use to provide your account info accept only alphanumeric characters.

    ![Payout paypal info](images/payout-paypal-info.png)

3. Provide beneficiary details.
4. Back on the *Profile assignment* page, select the currency you would like us to use when we issue your payouts.
5. You will need to select a payment profile for each program you participate in, though you can use the same profile for multiple programs.
6. Click submit to save your changes.