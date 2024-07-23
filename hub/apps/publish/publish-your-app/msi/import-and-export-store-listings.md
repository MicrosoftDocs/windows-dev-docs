---
description: Import and export store listings for your MSI/EXE app so you can reuse them later
title: Import and export store listings for MSI/EXE app
ms.date: 10/30/2022
ms.topic: article
ms.localizationpriority: medium
---

# Import and export store listings for MSI/EXE app

Instead of entering info for your Store listing directly in Partner Center, you have the option to add or update info by exporting your listings in a .csv file, entering your info and assets, and then importing the updated file. You can use this method to create listings from scratch, or to update listings you’ve already created.

This option is especially useful if you want to create or update Store listings for your product in multiple languages, since you can copy/paste the same info into multiple fields and easily make any changes that should apply to specific languages.

## Export listing

On the Store listing page for an app, you can add languages and then click on Export listing to generate a .csv file with the language(s) you selected and the fields for the listing. Alternatively, you can first click on Export listing and then select the languages. Once the listing is exported successfully, download the listing. In the exported listing, you will see Field names, type and the languages you have selected for your listing.

Here’s an overview of what’s contained in each of the columns in the exported .csv file:

- The Field column contains a name that is associated with every part of a Store listing. These correspond to the same items you can provide when creating Store listings in Partner Center, although some of the names are slightly different. For items where you can enter more than one of the same type of items, you’ll see multiple rows, up to the maximum number that you can provide. For example, for Product features you will see ProductFeatures1, ProductFeatures2, etc., going up to ProductFeatures20 (since you can provide up to 20 app features). Please refer to the table below to find the exact mapping of the Store listing fields with the fields exported in .csv file.
- The Type column provides general guidance about what type of info to provide for that field, such as Text or Relative path.

| Field name in Store listing in Partner Center | Corresponding field name in exported .csv file        |
| --------------------------------------------- | ----------------------------------------------------- |
| Description                                   | Description                                           |
| Product name                                  | ProductName                                           |
| What’s new in this version?                   | WhatsNew                                              |
| Product features                              | ProductFeatures1 to ProductFeatures20                 |
| Screenshots                                   | Screenshots1 to Screenshots10                         |
| Store logos (1:1 box art)                     | StoreLogos1                                           |
| Store logos (2:3 poster art)                  | StoreLogos2                                           |
| Short description                             | ShortDescription                                      |
| Minimum hardware                              | RequirementsMinimum1 to RequirementsMinimum11         |
| Recommended hardware                          | RequirementsRecommended1 to RequirementsRecommended11 |
| Search terms                                  | SearchTerms1 to SearchTerms7                          |
| Copyright and trademark info                  | Copyright                                             |
| Applicable license terms                      | Applicable license terms                              |
| Developed by                                  | DevelopedBy                                           |

> [!Important]
> Do not change any of the field names or delete any of the fields in the exported .csv file. The name of the columns and items under field must remain unchanged for your imported file to be processed.

## Update listing info

Once you’ve exported your listings and saved your .csv file, you can edit your listing info directly in the .csv file. For each language added, there is a separate column with the name of the language as the heading. Do not change the name of the language exported in the .csv file. The changes you make in a column will be applied to the listing of that language.

Most of the Store listing fields are optional. The Description, one Screenshot, Store logo (1:1 box art) and Applicable license terms are required for each listing. For all other fields, you can leave the field empty if you don’t want to include it in your listing.

Many of the fields in your exported listings require text entry, such as the ones in the example above, Description and WhatsNew. For these types of fields, simply enter the appropriate text into the field for each language. Be sure to follow the length and other requirements for each field. For more info on these requirements, see [Create app Store listings](./create-app-store-listing.md).

Providing info for fields that correspond to assets, such as images, are a bit more complicated. Rather than Text, the Type for these assets is Relative path.

> [!Tip]
> If your assets (screenshot or logo) are in the same folder as the exported .csv file, the relative path will just be the name of the screenshot or logo (for example: logo.png). If you have created a subfolder for images in the main folder which has the .csv file, the relative path will start with the name of the subfolder. (For example, if the name of subfolder is “Images”, the relative path will be Images/logo.png)

## Import listing

Once you have entered all your changes into the .csv file (and included any assets you want to upload), you’ll need to save your file before uploading it.

When you’re ready to upload the updated .csv file and import your listing data, select Import listing on your app’s Store listing page. When you are importing the listing for the first time, you will have to upload the listing using a folder which will have the exported .csv file and all the assets. Make sure there is only one .csv file in your folder, along with any assets you’re uploading.

> [!Important]
> When importing the listing for the first time, you need to upload a folder. This folder should contain the exported .csv file with the listing information and all the assets (screenshots and logo) you mentioned in the .csv file.

If we detect any problems, you’ll see a note indicating that Import has failed. Download the report to check the errors. You’ll need to correct these issues in your .csv file (or replace any invalid assets) and then import your listings again.

Refer to the table below for the error messages, their meaning, and the recommendation to resolve them.

| Error message                                                                                                                                        | Meaning and recommendation                                                                                                                                                                                                                            |
| ---------------------------------------------------------------------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| "The .csv file is empty. Please ensure that the file has the required info."                                                                         | You might have uploaded a blank .csv file. From the Store listing page, export the latest .csv file.                                                                                                                                                  |
| “The field is either not present or invalid. Please use the correct template by exporting the listing.”                                              | You might have deleted an existing field or added a new field in the exported .csv file. Export the .csv file from the Store listing page. Do not change any of the fields in the .csv file.                                                          |
| “The language codes are either invalid or listing is not available for the languages. Please use the most recent template by exporting the listing.” | You might have edited the language code in the exported .csv file or you might have added a new language code which is not present in the Store listing page. From the Store listing page, select the required languages and then export the listing. |
| “All the selected languages are not present in the imported .csv file. Please use the most recent template by exporting the listing.”                | You might have removed a language code in the .csv file. Export the .csv file from the Store listing page. Do not change any of the language codes in the .csv file.                                                                                  |

You can continue to make updates to your listings either by importing another updated .csv file, or by making changes directly in Partner Center.
