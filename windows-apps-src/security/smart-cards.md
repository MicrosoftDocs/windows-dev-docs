---
title: Smart cards
description: This topic explains how Universal Windows Platform (UWP) apps can use smart cards to connect users to secure network services, including how to access physical smart card readers, create virtual smart cards, communicate with smart cards, authenticate users, reset user PINs, and remove or disconnect smart cards.
ms.assetid: 86524267-50A0-4567-AE17-35C4B6D24745
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, security
ms.localizationpriority: medium
---
# Smart cards




This topic explains how Universal Windows Platform (UWP) apps can use smart cards to connect users to secure network services, including how to access physical smart card readers, create virtual smart cards, communicate with smart cards, authenticate users, reset user PINs, and remove or disconnect smart cards. 

## Configure the app manifest


Before your app can authenticate users using smart cards or virtual smart cards, you must set the **Shared User Certificates** capability in the project Package.appxmanifest file.

## Access connected card readers and smart cards


You can query for readers and attached smart cards by passing the device ID (specified in [**DeviceInformation**](/uwp/api/Windows.Devices.Enumeration.DeviceInformation)) to the [**SmartCardReader.FromIdAsync**](/uwp/api/windows.devices.smartcards.smartcardreader.fromidasync) method. To access the smart cards currently attached to the returned reader device, call [**SmartCardReader.FindAllCardsAsync**](/uwp/api/windows.devices.smartcards.smartcardreader.findallcardsasync).

```cs
string selector = SmartCardReader.GetDeviceSelector();
DeviceInformationCollection devices =
    await DeviceInformation.FindAllAsync(selector);

foreach (DeviceInformation device in devices)
{
    SmartCardReader reader =
        await SmartCardReader.FromIdAsync(device.Id);

    // For each reader, we want to find all the cards associated
    // with it.  Then we will create a SmartCardListItem for
    // each (reader, card) pair.
    IReadOnlyList<SmartCard> cards =
        await reader.FindAllCardsAsync();
}
```

You should also enable your app to observe for [**CardAdded**](/uwp/api/windows.devices.smartcards.smartcardreader.cardadded) events by implementing a method to handle app behavior on card insertion.

```cs
private void reader_CardAdded(SmartCardReader sender, CardAddedEventArgs args)
{
  // A card has been inserted into the sender SmartCardReader.
}
```

You can then pass each returned [**SmartCard**](/uwp/api/Windows.Devices.SmartCards.SmartCard) object to [**SmartCardProvisioning**](/uwp/api/Windows.Devices.SmartCards.SmartCardProvisioning) to access the methods that allow your app to access and customize its configuration.

## Create a virtual smart card


To create a virtual smart card using [**SmartCardProvisioning**](/uwp/api/Windows.Devices.SmartCards.SmartCardProvisioning), your app will first need to provide a friendly name, an admin key, and a [**SmartCardPinPolicy**](/uwp/api/Windows.Devices.SmartCards.SmartCardPinPolicy). The friendly name is generally something provided to the app, but your app will still need to provide an admin key and generate an instance of the current **SmartCardPinPolicy** before passing all three values to [**RequestVirtualSmartCardCreationAsync**](/uwp/api/windows.devices.smartcards.smartcardprovisioning.requestvirtualsmartcardcreationasync).

1.  Create a new instance of a [**SmartCardPinPolicy**](/uwp/api/Windows.Devices.SmartCards.SmartCardPinPolicy)
2.  Generate the admin key value by calling [**CryptographicBuffer.GenerateRandom**](/uwp/api/windows.security.cryptography.cryptographicbuffer.generaterandom) on the admin key value provided by the service or management tool.
3.  Pass these values along with the *FriendlyNameText* string to [**RequestVirtualSmartCardCreationAsync**](/uwp/api/windows.devices.smartcards.smartcardprovisioning.requestvirtualsmartcardcreationasync).

```cs
SmartCardPinPolicy pinPolicy = new SmartCardPinPolicy();
pinPolicy.MinLength = 6;

IBuffer adminkey = CryptographicBuffer.GenerateRandom(24);

SmartCardProvisioning provisioning = await
     SmartCardProvisioning.RequestVirtualSmartCardCreationAsync(
          "Card friendly name",
          adminkey,
          pinPolicy);
```

Once [**RequestVirtualSmartCardCreationAsync**](/uwp/api/windows.devices.smartcards.smartcardprovisioning.requestvirtualsmartcardcreationasync) has returned the associated [**SmartCardProvisioning**](/uwp/api/Windows.Devices.SmartCards.SmartCardProvisioning) object, the virtual smart card is provisioned and ready for use.

>[!NOTE]
>In order to create a virtual smart card using a UWP app, the user running the app must be a member of the administrators group. If the user is not a member of the administrators group, virtual smart card creation will fail.

## Handle authentication challenges


To authenticate with smart cards or virtual smart cards, your app must provide the behavior to complete challenges between the admin key data stored on the card, and the admin key data maintained by the authentication server or management tool.

The following code shows how to support smart card authentication for services or modification of physical or virtual card details. If the data generated using the admin key on the card ("challenge") is the same as the admin key data provided by the server or management tool ("adminkey"), authentication is successful.

```cs
static class ChallengeResponseAlgorithm
{
    public static IBuffer CalculateResponse(IBuffer challenge, IBuffer adminkey)
    {
        if (challenge == null)
            throw new ArgumentNullException("challenge");
        if (adminkey == null)
            throw new ArgumentNullException("adminkey");

        SymmetricKeyAlgorithmProvider objAlg = SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithmNames.TripleDesCbc);
        var symmetricKey = objAlg.CreateSymmetricKey(adminkey);
        var buffEncrypted = CryptographicEngine.Encrypt(symmetricKey, challenge, null);
        return buffEncrypted;
    }
}
```

You will see this code referenced throughout the remainder of this topic was we review how to complete an authentication action, and how to apply changes to smart card and virtual smart card information.

## Verify smart card or virtual smart card authentication response


Now that we have the logic for authentication challenges defined, we can communicate with the reader to access the smart card, or alternatively, access a virtual smart card for authentication.

1.  To begin the challenge, call [**GetChallengeContextAsync**](/uwp/api/windows.devices.smartcards.smartcardprovisioning.getchallengecontextasync) from the [**SmartCardProvisioning**](/uwp/api/Windows.Devices.SmartCards.SmartCardProvisioning) object associated with the smart card. This will generate an instance of [**SmartCardChallengeContext**](/uwp/api/Windows.Devices.SmartCards.SmartCardChallengeContext), which contains the card's [**Challenge**](/uwp/api/windows.devices.smartcards.smartcardchallengecontext.challenge) value.

2.  Next, pass the card's challenge value and the admin key provided by the service or management tool to the **ChallengeResponseAlgorithm** that we defined in the previous example.

3.  [**VerifyResponseAsync**](/uwp/api/windows.devices.smartcards.smartcardchallengecontext.verifyresponseasync) will return **true** if authentication is successful.

```cs
bool verifyResult = false;
SmartCard card = await rootPage.GetSmartCard();
SmartCardProvisioning provisioning =
    await SmartCardProvisioning.FromSmartCardAsync(card);

using (SmartCardChallengeContext context =
       await provisioning.GetChallengeContextAsync())
{
    IBuffer response = ChallengeResponseAlgorithm.CalculateResponse(
        context.Challenge,
        rootPage.AdminKey);

    verifyResult = await context.VerifyResponseAsync(response);
}
```

## Change or reset a user PIN


To change the PIN associated with a smart card:

1.  Access the card and generate the associated [**SmartCardProvisioning**](/uwp/api/Windows.Devices.SmartCards.SmartCardProvisioning) object.
2.  Call [**RequestPinChangeAsync**](/uwp/api/windows.devices.smartcards.smartcardprovisioning.requestpinchangeasync) to display a UI to the user to complete this operation.
3.  If the PIN was successfully changed the call will return **true**.

```cs
SmartCardProvisioning provisioning =
    await SmartCardProvisioning.FromSmartCardAsync(card);

bool result = await provisioning.RequestPinChangeAsync();
```

To request a PIN reset:

1.  Call [**RequestPinResetAsync**](/uwp/api/windows.devices.smartcards.smartcardprovisioning.requestpinresetasync) to initiate the operation. This call includes a [**SmartCardPinResetHandler**](/uwp/api/windows.devices.smartcards.smartcardpinresethandler) method that represents the smart card and the pin reset request.
2.  [**SmartCardPinResetHandler**](/uwp/api/windows.devices.smartcards.smartcardpinresethandler) provides information that our **ChallengeResponseAlgorithm**, wrapped in a [**SmartCardPinResetDeferral**](/uwp/api/Windows.Devices.SmartCards.SmartCardPinResetDeferral) call, uses to compare the card's challenge value and the admin key provided by the service or management tool to authenticate the request.

3.  If the challenge is successful, the [**RequestPinResetAsync**](/uwp/api/windows.devices.smartcards.smartcardprovisioning.requestpinresetasync) call is completed; returning **true** if the PIN was successfully reset.

```cs
SmartCardProvisioning provisioning =
    await SmartCardProvisioning.FromSmartCardAsync(card);

bool result = await provisioning.RequestPinResetAsync(
    (pinResetSender, request) =>
    {
        SmartCardPinResetDeferral deferral =
            request.GetDeferral();

        try
        {
            IBuffer response =
                ChallengeResponseAlgorithm.CalculateResponse(
                    request.Challenge,
                    rootPage.AdminKey);
            request.SetResponse(response);
        }
        finally
        {
            deferral.Complete();
        }
    });
}
```

## Remove a smart card or virtual smart card


When a physical smart card is removed a [**CardRemoved**](/uwp/api/windows.devices.smartcards.smartcardreader.cardremoved) event will fire when the card is deleted.

Associate the firing of this event with the card reader with the method that defines your app's behavior on card or reader removal as an event handler. This behavior can be something as simply as providing notification to the user that the card was removed.

```cs
reader = card.Reader;
reader.CardRemoved += HandleCardRemoved;
```

The removal of a virtual smart card is handled programmatically by first retrieving the card and then calling [**RequestVirtualSmartCardDeletionAsync**](/uwp/api/windows.devices.smartcards.smartcardprovisioning.requestvirtualsmartcarddeletionasync) from the [**SmartCardProvisioning**](/uwp/api/Windows.Devices.SmartCards.SmartCardProvisioning) returned object.

```cs
bool result = await SmartCardProvisioning
    .RequestVirtualSmartCardDeletionAsync(card);
```