---
title: Dynamically modify Cortana VCD phrase lists - Cortana UWP design and development
description: Access and update the list of supported phrases (PhraseList elements) in a Voice Command Definition (VCD) file at run time using the speech recognition result.
ms.assetid: b497145b-c7a0-454a-8329-6bc1228953bb
ms.date: 01/28/2021
ms.topic: article
keywords: cortana
---

# Dynamically modify Cortana VCD phrase lists

>[!WARNING]
> This feature is no longer supported as of the Windows 10 May 2020 Update (version 2004, codename "20H1").

Access and update the list of supported phrases (**PhraseList** elements) in a Voice Command Definition (VCD) file at run time using the speech recognition result.

> [!NOTE]
> A voice command is a single utterance with a specific intent, defined in a Voice Command Definition (VCD) file, directed at an installed app via **Cortana**.
>
> A VCD file defines one or more voice commands, each with a unique intent.
>
> Voice command definitions can vary in complexity. They can support anything from a single, constrained utterance to a collection of more flexible, natural language utterances, all denoting the same intent.

Dynamically modifying a phrase list at run time is useful if the voice command is specific to a task involving some kind of user-defined, or transient, app data.

> [!NOTE]
> **Important APIs**
>
> - [**Windows.ApplicationModel.VoiceCommands**](/uwp/api/Windows.ApplicationModel.VoiceCommands)
> - [**VCD elements and attributes v1.2**](/uwp/schemas/voicecommands/voice-command-elements-and-attributes-1-2)

As an example, let's say you have a travel app where users can enter destinations, and you want users to be able to start the app by saying the app name followed by "Show trip to &lt;destination&gt;". In the **ListenFor** element itself, you would specify something like: `<ListenFor> Show trip to {destination}  </ListenFor>`, where "destination" is the value of the **Label** attribute for the **PhraseList**.

Updating the phrase list at run time eliminates the need to create a separate **ListenFor** element for each possible destination. Instead, you can dynamically populate **PhraseList** with destinations specified by the user as they enter their itineraries.

For more info about **PhraseList** and other VCD elements, see the [**VCD elements and attributes v1.2**](/uwp/schemas/voicecommands/voice-command-elements-and-attributes-1-2) reference.

> [!TIP]
> **Prerequisites**
>
> If you're new to developing Universal Windows Platform (UWP) apps, have a look through these topics to get familiar with the technologies discussed here.
>
> - [Create your first app](/windows/uwp/get-started/your-first-app)
> - Learn about events with [Events and routed events overview](/windows/uwp/xaml-platform/events-and-routed-events-overview)
>
> **User experience guidelines**
>
> See [Cortana design guidelines](cortana-design-guidelines.md)  for info about how to integrate your app with **Cortana** and [Speech interactions](speech-interactions.md) for helpful tips on designing a useful and engaging speech-enabled app.

## Identify the command and update the phrase list

Here's an example VCD file that defines a **Command** "showTripToDestination" and a **PhraseList** that defines three options for destination in our **Adventure Works** travel app. As the user saves and deletes destinations in the app, the app updates the options in the **PhraseList**.

```XML
<?xml version="1.0" encoding="utf-8"?>
<VoiceCommands xmlns="https://schemas.microsoft.com/voicecommands/1.1">
  <CommandSet xml:lang="en-us" Name="AdventureWorksCommandSet_en-us">
    <AppName> Adventure Works, </AppName>
    <Example> Show trip to London </Example>

    <Command Name="showTripToDestination">
      <Example> show trip to London  </Example>
      <ListenFor> show trip to {destination} </ListenFor>
      <Feedback> Showing trip to {destination} </Feedback>
      <Navigate/>
    </Command>

    <PhraseList Label="destination">
      <Item> London </Item>
      <Item> Dallas </Item>
      <Item> New York </Item>
    </PhraseList>

  </CommandSet>

<!-- Other CommandSets for other languages -->

</VoiceCommands>
```

To update a **PhraseList** element in the VCD file, get the **CommandSet** element that contains the phrase list. Use the **Name** attribute of that **CommandSet** element (**Name** must be unique in the VCD file) as a key to access the [**VoiceCommandManager.InstalledCommandSets**](/uwp/api/Windows.Media.SpeechRecognition.VoiceCommandManager) property and get the [**VoiceCommandSet**](/uwp/api/Windows.Media.SpeechRecognition.VoiceCommandSet) reference.

After you've identified the command set, get a reference to the phrase list that you want to modify and call the [**SetPhraseListAsync**](/uwp/api/Windows.Media.SpeechRecognition.VoiceCommandSet) method; use the **Label** attribute of the **PhraseList** element and an array of strings as the new content of the phrase list.

> [!NOTE]
> If you modify a phrase list, the entire phrase list is replaced. If you want to insert new items into a phrase list, you must specify both the existing items and the new items in the call to [**SetPhraseListAsync**](/uwp/api/Windows.Media.SpeechRecognition.VoiceCommandSet).

In this example, we update the **PhraseList** shown in the previous example with an additional destination to Phoenix.

```CSharp
Windows.ApplicationModel.VoiceCommands.VoiceCommandDefinition.VoiceCommandSet commandSetEnUs;

if (Windows.ApplicationModel.VoiceCommands.VoiceCommandDefinitionManager.
      InstalledCommandSets.TryGetValue(
        "AdventureWorksCommandSet_en-us", out commandSetEnUs))
{
  await commandSetEnUs.SetPhraseListAsync(
    "destination", new string[] {"London", "Dallas", "New York", "Phoenix"});
}
```

## Remarks

Using a **PhraseList** to constrain the recognition is appropriate for a relatively small set or words. When the set of words is too large (hundreds of words, for example), or shouldn't be constrained at all, use the **PhraseTopic** element and a **Subject** element to refine the relevance of speech-recognition results to improve scalability.

In our example, we have a **PhraseTopic** with a **Scenario** of "Search", further refined by a **Subject** of "City\\State".

```XML
<?xml version="1.0" encoding="utf-8"?>
<VoiceCommands xmlns="https://schemas.microsoft.com/voicecommands/1.1">
  <CommandSet xml:lang="en-us" Name="AdventureWorksCommandSet_en-us">
    <AppName> Adventure Works, </AppName>
    <Example> Show trip to London </Example>

    <Command Name="showTripToDestination">
      <Example> show trip to London  </Example>
      <ListenFor> show trip to {destination} </ListenFor>
      <Feedback> Showing trip to {destination} </Feedback>
      <Navigate/>
    </Command>

    <PhraseList Label="destination">
      <Item> London </Item>
      <Item> Dallas </Item>
      <Item> New York </Item>
    </PhraseList>

    <PhraseTopic Label="destination" Scenario="Search">
      <Subject>City/State</Subject>
    </PhraseTopic>

  </CommandSet>
```

## Related articles

- [Cortana interactions in Windows apps](cortana-interactions.md)
- [VCD elements and attributes v1.2](/uwp/schemas/voicecommands/voice-command-elements-and-attributes-1-2)
- [Activate a foreground app with voice commands through Cortana](cortana-launch-a-foreground-app-with-voice-commands.md)
- [Activate a background app in Cortana using voice commands](cortana-launch-a-background-app-with-voice-commands.md)
- [Cortana design guidelines](cortana-design-guidelines.md)
- [Cortana voice command sample](https://go.microsoft.com/fwlink/p/?LinkID=619899)
