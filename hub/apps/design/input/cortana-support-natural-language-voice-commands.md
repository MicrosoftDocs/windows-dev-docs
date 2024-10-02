---
title: Support more natural voice commands in Cortana - Cortana UWP design and development
description: Extend Cortana with more flexible and natural voice commands that let a user say your app's name anywhere in the command.
label: Conceptual
ms.assetid: c2959c1b-c2f2-4a8d-8f3e-79585f69afcf
ms.date: 01/28/2021
ms.topic: article
keywords: cortana
---

# Support natural language voice commands in Cortana

>[!WARNING]
> This feature is no longer supported as of the Windows 10 May 2020 Update (version 2004, codename "20H1").

Extend **Cortana** with more flexible and natural voice commands that let a user say your app's name anywhere in the command.

> [!NOTE]
> **Important APIs**
>
> - [**Windows.ApplicationModel.VoiceCommands**](/uwp/api/Windows.ApplicationModel.VoiceCommands)
> - [**VCD elements and attributes v1.2**](/uwp/schemas/voicecommands/voice-command-elements-and-attributes-1-2)

Using voice commands to extend Cortana with functionality from your app requires the user to specify both the app and the command or function to execute. This is typically accomplished by announcing the app name at the beginning or the end of the voice command. For example, "Adventure Works, add a new trip to Las Vegas."

However, specifying the application name in this way can sound awkward, stilted, or not even make sense. In many cases, being able to say the app name elsewhere in the command is more comfortable and natural, and helps make the interaction much more intuitive and engaging for the user. Our previous example, "Adventure Works, add a new trip to Las Vegas." could be rephrased as "Add a new Adventure Works trip to Las Vegas." or "Using Adventure Works, add a new trip to Las Vegas."

You can set up your voice commands to support the app name as a:

- Prefix - before the command phrase
- Infix - within the command phrase
- Suffix - after the command phrase

> [!TIP]
> **Prerequisites**
>
> This topic builds on [Activate a background app in Cortana using voice commands](cortana-launch-a-background-app-with-voice-commands.md). We continue here to demonstrate features with a trip planning and management app named **Adventure Works**.
>
> If you're new to developing Universal Windows Platform (UWP) apps, have a look through these topics to get familiar with the technologies discussed here.
>
> - [Create your first app](/windows/uwp/get-started/your-first-app)
> - Learn about events with [Events and routed events overview](/windows/uwp/xaml-platform/events-and-routed-events-overview)
>
> **User experience guidelines**
>
> See [Cortana design guidelines](cortana-design-guidelines.md)  for info about how to integrate your app with **Cortana** and [Speech interactions](speech-interactions.md) for helpful tips on designing a useful and engaging speech-enabled app.

## Specify an **AppName** element in the VCD

The **AppName** element is used to specify a user-friendly name for an app in a voice command.

```XML
<AppName>Adventure Works</AppName>
```

## Specify where the app name can be spoken in the voice command

The **ListenFor** element has a **RequireAppName** attribute that specifies where the app name can appear in the voice command. This attribute supports four values.

1. **BeforePhrase**

   Default.

   Indicates that users must say your app name before the command phrase.

   Here, Cortana listens for "Adventure Works when is my trip to Las Vegas".

   ```xml
   <ListenFor RequireAppName="BeforePhrase"> show [my] trip to {destination} </ListenFor>
   ```

2. **AfterPhrase**

    Indicates that users must say your app name after the command phrase.

    A localized phrase list of prepositional conjunctions is provided by the system. This includes phrases such as, "using", "with "and "on".

    Here, Cortana listens for commands like "Show my next trip to Las Vegas on Adventure Works" and "Show my next trip to Las Vegas using Adventure Works".

    ```xml
    <ListenFor RequireAppName="AfterPhrase">show [my] next trip to {destination} </ListenFor>
    ```

3. **BeforeOrAfterPhrase**

    Indicates that users must say your app name either before or after the command phrase.

    For the suffix version, a localized phrase list of prepositional conjunctions is provided by the system. This includes phrases such as, "using", "with "and "on".

    Here, Cortana listens for commands like "Adventure Works, show my next trip to Las Vegas" or "Show my next trip to Last Vegas on Adventure works".

    ``` xml
    <ListenFor RequireAppName="BeforeOrAfterPhrase">show [my] next trip to {destination}</ListenFor>
    ```

4. **ExplicitlySpecified**

    Indicates that users must say your app name exactly where you specify in the command phrase. The user is not required to say the app name either before or after the phrase.

    You must explicitly reference your app name using the **{builtin:AppName}** tag.

    Here, Cortana listens for commands like "Adventure Works, show my next trip to Las Vegas" or "Show my next Adventure Works trip to Las Vegas".

    ```xml
    <ListenFor RequireAppName="ExplicitlySpecified">show [my] next {builtin:AppName} trip to {destination} </ListenFor>
    ```

## Special cases

When you declare a **ListenFor** element where **RequireAppName** is either "AfterPhrase" or "ExplicitlySpecified", you must ensure certain requirements are met:

1. **{builtin:AppName}** must appear once and only once when **RequireAppName** is "ExplicitlySpecified".

    With this value, the system cannot infer where the app name can appear in the voice command. You must explicitly specify the location.

2. You cannot have a voice command begin with a **PhraseTopic** element, which is typically used for large vocabulary speech recognition. At least one word must precede it.

    This helps to minimize the chance that **Cortana** launches your app if a command contains your app name, or part of it, anywhere in the utterance.

    Here is an invalid declaration that could lead to **Cortana** launching the **Adventure Works** app if the user says something like "Show me reviews for Kinect Adventure works".
  
    ```xml
    <ListenFor RequireAppName="ExplicitlySpecified">{searchPhrase} {builtin:AppName}</ListenFor>
    ```

3. There must be at least two words in the **ListenFor** string besides your app name and references to **PhraseTopic** elements.

    Similar to case 2, you need to ensure your commands contain sufficient phonetic content to minimize the chances your app is launched unintentionally.

    This helps you set up your application for best possible success so your application does not get incorrectly launched when user says for example "Find Kinect Adventure works".

    Here are invalid declarations that could lead to **Cortana** launching the **Adventure Works** app if the user says something like "Hey adventure works" or "Find Kinect adventure works".

    ```xml
    <ListenFor RequireAppName="ExplicitlySpecified">Hey {builtin:AppName}</ListenFor>
    <ListenFor RequireAppName="ExplicitlySpecified">Find {searchPhrase} {builtin:AppName}</ListenFor>
    ```

## Remarks

Supporting more variation in how a voice command can be uttered by a user in **Cortana** also increases the general usability of your app.

Avoid having "Hey \[app name\]" as your **AppName**. Users are much more likely to say "Hey Cortana" to invoke Cortana through voice activation, and having "Hey \[app name\]" in the utterance does not sound natural. For example, "Hey Cortana, show my next trip to Las Vegas on Hey Adventure Works".

Consider adding infix/suffix variations to your existing voice commands. As we've shown here, it doesn't require a lot of effort to add an additional attribute to your existing **ListenFor** elements and support suffix variants. It feels a lot more natural to say "Hey Cortana, show my next trip to Las Vegas on Adventure works" than "Hey Cortana, Adventure Works, show my next trip to Las Vegas".

Consider using your app name as a prefix in cases where the voice command conflicts with existing **Cortana** functionality (calling, messaging, and so on). For example, "Adventure Works, message \[travel agent\] about Las Vegas trip".

## Complete example

Here is a VCD file that demonstrates various ways to provide more natural language voice commands.

> [!NOTE]
> It is valid to have multiple **ListenFor** elements, each with a different **RequireAppName** attribute value.

```XML
<?xml version="1.0" encoding="utf-8"?>
<VoiceCommands xmlns="https://schemas.microsoft.com/voicecommands/1.1">
  <CommandSet xml:lang="en-us" Name="commandSet_en-us">
    <AppName>Adventure Works</AppName>
    <Example> When is my trip to Las Vegas? </Example>
    <Command Name="whenIsTripToDestination">
      <Example> When is my trip to Las Vegas?</Example>
      <ListenFor RequireAppName="BeforePhrase">
        when is my] trip to {destination} </ListenFor>

      <!-- This ListenFor command will set up Cortana to accept commands like 
           "Show my next trip to Las Vegas on Adventure Works"; "Show my next 
           trip to Las Vegas using Adventure Works" -->
      <ListenFor RequireAppName="AfterPhrase">
        show [my] next trip to {destination} </ListenFor>

      <!-- This ListenFor command will set up Cortana to accept commands when 
           the user specifies your app name either before or after the command. 
           "Adventure Works, show my next trip to Las Vegas"; 
           "Show my next trip to Last Vegas on Adventure works" -->
      <ListenFor RequireAppName="BeforeOrAfterPhrase">
        show [my] next trip to {destination} </ListenFor>

      <!-- This ListenFor command will set up Cortana to accept commands 
           when the user specifies your app name inline. 
           "Show my next Adventure Works trip to Las Vegas" -->
      <ListenFor RequireAppName="ExplicitlySpecified">
        show [my] next {builtin:AppName} trip to {destination} </ListenFor>

      <Feedback> Looking for trip to {destination} </Feedback>
      <Navigate />
    </Command>
    <PhraseList Label="destination">
      <Item> Las Vegas </Item>
      <Item> Dallas </Item>
      <Item> New York </Item>
    </PhraseList>
  </CommandSet>
  <!-- Other CommandSets for other languages -->
</VoiceCommands>
```

## Related articles

- [Cortana interactions in Windows apps](cortana-interactions.md)
- [Cortana design guidelines](cortana-design-guidelines.md)
- [VCD elements and attributes v1.2](/uwp/schemas/voicecommands/voice-command-elements-and-attributes-1-2)
- [Cortana voice command sample](https://go.microsoft.com/fwlink/p/?LinkID=619899)
