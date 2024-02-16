### Availability page

| Field name     | Required       | Notes |
|----------------|----------------|-------|
| **Markets**    | **Required**   | Default: All possible markets |
| **Pricing**    | **Required**   | One of: Free; Paid; Freemium; Subscription. |
| **Free Trial** | **Required**   | *Not* required if pricing is set to Free or Freemium. |

### Properties page, support info section

| Field name                      | Required     | Notes |
|---------------------------------|--------------|-------|
| **Category**                    | **Required** |       |
| **Subcategory**                 | Not required |       |
| **Secondary category**          | Not required |       |
| **Does this product access...** | **Required** |       |
| **Privacy policy URL**          | See notes    | Only required if you answered yes to the previous question |
| **Website**                     | Not required |       |
| **Contact details**             | Not required | Required for business/company accounts       |
| **Support contact info**        | Not required |       |

### Properties page, products declaration section

| Field name                                                     | Required     | Notes |
|----------------------------------------------------------------|--------------|-------|
| **This app depends on non-Microsoft drivers or NT services.**  | Not required |       |
| **This app has been tested to meet accessibility guidelines.** | Not required |       |
| **This product supports pen and ink input.**                   | Not required |       |
| **Notes for certification**                                    | Recommended  | Character limit: 2,000 |

### Properties page, system requirements section

| Field name                  | Required     | Notes |
|-----------------------------|--------------|-------|
| **Touch screen**            | Not required |       |
| **Keyboard**                | Not required |       |
| **Mouse**                   | Not required |       |
| **Camera**                  | Not required |       |
| **NFC HCE**                 | Not required |       |
| **NFC Proximity**           | Not required |       |
| **Bluetooth LE**            | Not required |       |
| **Telephony**               | Not required |       |
| **Microphone**              | Not required |       |
| **Memory**                  | Not required |       |
| **DirectX**                 | Not required |       |
| **Dedicated GPU Memory**    | Not required |       |
| **Processor**               | Not required |       |
| **Graphics**                | Not required |       |

### Packages page

| Field name                  | Required     | Notes |
|-----------------------------|--------------|-------|
| **Package URL**             | **Required** | At least one package URL is required |
| **Language**                | **Required** | At least one language is required |
| **Architecture**            | **Required** |       |
| **Installer parameters**    | **Required** | Support for silent install is required. Other parameters are optional |
| **App type**                | **Requited** | Specify between EXE and MSI |
| **Installer handling URL**  | **Required** | Required in case of EXE only |    


## Store listings page

Each language has a separate store listing page. One listing page is required. It is recommended to provide complete listing page information for each language your app supports.

| Field name                         | Required     | Notes                   |
|------------------------------------|--------------|-------------------------|
| **Description**                    | **Required** | Character limit: 10,000 |
| **What’s new in this version**     | Not required | Character limit: 1,500  |
| **App features**                   | Not required | Character limit: 200 per feature; Feature limit: 20. |
| **Screenshots**                    | **Required** | Required: 1; Recommended: 4+; Maximum: 10 |
| **Store logos**                    | Required | 1:1 Box art required, 2:3 Poster art recommended |
| **Short description**              | Not required | Character limit: 1,000  |
| **Additional system requirements** | Not required | Character limit: 200 characters per requirement; Requirements limit: 11 for each of minimum and recommended hardware. |
| **Keywords**                       | Not required | Character limit: 40 per term; Term limit: 7; Maximum of 21 unique words total among all terms. |
| **Copyright and trademark info**   | Not required | Character limit: 200    |
| **Applicable license terms**       | **Required** | Character limit: 10,000 |
| **Developed by**                   | Not required | Character limit: 255    |
