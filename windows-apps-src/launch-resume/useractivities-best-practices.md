---
title: User Activities best practices
description: This guide outlines the recommended practices for creating and updating User Activities.
keywords: user activity, user activities, timeline, cortana pick up where you left off, cortana pick up where i left off, project rome
ms.date: 08/23/2018
ms.topic: article


ms.localizationpriority: medium
---
# User Activities best practices

This guide outlines the recommended practices for creating and updating User Activities. For an overview of the User Activities feature on Windows, see [Continue user activity, even across devices](./useractivities.md). Or, see the [User Activities section](/windows/project-rome/user-activities/) of Project Rome for the implementations of Activities on other development platforms.

## When to create or update User Activities

Because every app is different, it's up to each developer to determine the best way to map actions within the app to User Activities. Your User Activities will be showcased in Cortana and Timeline, which are focused on increasing users' productivity and efficiency by helping them get back to content they visited in the past.

**General guidelines**

* **Record a single activity for a group of related user actions.** This is especially relevant for music playlists or TV Shows: a single Activity can be updated at regular intervals to reflect the user's progress. In this case, you will have a single User Activity with multiple History Items representing periods of engagement across multiple days or weeks. The same applies to document-based activities on which the user makes gradual progress within your app.
* **Store user data in the cloud.** If you want to support cross-device Activities, you'll need to make sure the content required to re-engage this Activity is stored to a cloud location. Device-specific Activities will appear on Timeline on the device where the activity was created but may not appear on other devices.
* **Do not create Activities for actions that users will not need to resume.** If your application is used to complete simple, one-time operations that do not persist status, you probably do not need to create a User Activity.
* **Do not create Activities for actions completed by other users.** If an external account sends the user a message or @-mentions them within your app, you should not create an Activity for this. This type of action is better served by Action Center Notifications.
  * Collaboration scenarios are an exception: If multiple users are working on the same activity together (such as a Word document), there will be cases in which another user has made changes after your user. In this case, you may want to update the existing Activity to reflect changes that were made to the document. This would involve updating the existing User Activity content data without creating a new History Item.

**Guidelines for specific types of apps**

While every app is different, most apps will fall into one of the following interaction patterns.
* **Document-based apps** — Create one Activity per document, with one or more History Items reflecting periods of use. It is important to update your Activity as changes are made to the document.
* **Games** — Create one Activity for each game save or world. If your game supports only a single sequence of levels, you can re-publish the same Activity over time, although you may wish to update the content data to show the latest progress or achievements.
* **Utility apps** — If there is nothing within your app that users would need to leave and resume, you do not need to use User Activities. A good example is a simple app like Calculator.
* **Line-of-business apps** — Many apps exist for managing simple tasks or workflows. Create one activity for each separate workflow accessed through your app (for example, expense reports would each be a separate Activity, so that the user could then click an Activity to see if a particular report was approved).
* **Media playback apps** — Create one Activity per logical grouping of content (such as a playlist, program, or standalone content). The underlying question for app developers is whether a each piece of content (TV episode, song) counts as standalone content or part of a collection. As a general rule, if the user opts to play a collection or sequential content, the collection as a whole is the activity. If they opt to play a single piece of content, then that one piece of content is the activity. See more specific guidelines below.
  * **Music: Album/Artist/Genre** — If the user selects an Album, Artist, or Genre and hits **play**, that collection is the activity; do not write a separate Activity for each song. For short collections like a single album or collections being played back in a random order, you may not need to update the Activity to reflect the user's current position. For long sequential playback such as an album or playlist, recording your position within the album might make sense.
  * **Music: smart playlists** — Applications which play music in a random order should record a single Activity for that playlist. If the user plays the playlist a second time, you would create additional history records for the same Activity. Recording the user's current position in the playlist is not necessary because the ordering is random.
  * **TV series** — If your app is configured to play the next episode after the current one is complete, you should write a single Activity for the TV series. As you play the various episodes across multiple viewing sessions, you'll update your Activity to reflect the current position in the series, and multiple history records will be created.
  * **Movie** — A movie is a single piece of content and should have its own history record. If the user stops watching the movie part-way through, it is desirable to record their position. When they wish to resume it in the future, the Activity could resume the movie where they left off, or even ask the user if they wish to resume or start at the beginning.

## User Activity design

User Activities consist of three components: an activation URI, visual data, and content metadata.
* The activation URI is a URI that can be passed to an application or experience in order to resume the application with a specific context. Typically, these links take the form of protocol handler for a scheme (for example, "my-app://page2?action=edit"). It is up to the developer to determine how URI parameters will be handled by their app. See [Handle URI activation](./handle-uri-activation.md) for more information.
* The visual data, consisting of a set of required and optional properties (for example: title, description, or Adaptive Card elements), allow users to visually identify an Activity. See below for guidelines on creating Adaptive Card visuals for your Activity.
* The content metadata is JSON data that can be used to group and retrieve activities under a specific context. Typically, this takes the form of http://schema.org data. See below for guidelines on filling out this data.

### Adaptive Card design guidelines

When Activities appear in Timeline, they are displayed using the [Adaptive Card framework](/adaptive-cards/). If the developer does not provide an Adaptive Card for each Activity, Timeline will automatically create a simple card based on the app name/icon, the required Title field, and the optional Description field. 

App developers are encouraged to provide custom cards using the simple Adaptive Card JSON schema. See the [Adaptive Cards documentation](/adaptive-cards/authoring-cards/getting-started) for technical instructions on how to construct Adaptive Card objects. Refer to the guidelines below for designing Adaptive Cards in User Activities.
* Use images
  * Use a unique image for each Activity, if possible. Your application name and icon will automatically be displayed next to your Activity's card; additional images will help users locate the Activity they are looking for.
  * Images should not include text that the user is expected to read. This text won't be available to users with accessibility needs and cannot be searched.
  * If the image doesn't contain text and can be cropped to about a 2:1 ratio, you should use it as a background image. This results in a bold activity card which will stand out in Timeline. The image will be darkened slightly to ensure the text remains visible on the card, and you are encouraged to only use the Activity Name in this case, as smaller text can become hard to read.
  * If the image cannot be cropped to 2:1, you should put it within the Activity Card.  
    * If the aspect ratio is Square or Portrait, anchor the image on the right side of the card with no margins.
    * If the aspect ratio is Landscape, anchor the image to the upper-right corner of the card.
* Each activity is required to provide an Activity Name, which should always be shown.
  * This name should be displayed in the upper-left corner of the card using the large bold text option. It is important that the name is easily recognizable, as this is the only part users will see when the activity is shown in Cortana scenarios. Showing the same name in Timeline makes it easier for users to browse a large number of Activities.
* Use the same visual style for all of the Activities from your app, so that users can easily locate your app's activities in the Timeline.
  * For example, Activities should all use the same background color.
* Use supplemental text information sparingly. 
  * Avoid filling the card with text, and only use supplemental information that aids users in finding the right activity or reflects state information (such as the current progress in a particular task).

### Content metadata guidelines

User Activities can also contain content metadata, which Windows and Cortana use to categorize Activities and generate inferences. Activities can then be grouped around a particular topic, such as a location (if the user is researching vacations), object (if the user is researching something) or action (if the user is shopping for a particular product across different apps and websites). It's a good idea to represent both the nouns and the verbs involved in an activity. 

In the following example, the content metadata JSON, following the standards of [Schema.org](https://schema.org/), represents the scenario: "John played Angry Birds with Steve."

```json
// John played angry birds with Steve.
{
  "@context": "http://schema.org",
  "@type": "PlayAction",
  "agent": {
    "@type": "Person",
    "name": "John"
  },
  "object": {
    "@type": "MobileApplication",
    "name": "Angry Birds."
  },
  "participant": {
    "@type": "Person",
    "name": "Steve"
  }
}
```

## Key APIs

* [UserActivities namespace](/uwp/api/windows.applicationmodel.useractivities)

## Related topics

* [User Activities (Project Rome docs)](/windows/project-rome/user-activities/)
* [Adaptive cards](/adaptive-cards/)
* [Adaptive cards visualizer, samples](https://adaptivecards.io/)
* [Handle URI activation](./handle-uri-activation.md)
* [Engaging with your customers on any platform using the Microsoft Graph, Activity Feed, and Adaptive Cards](https://channel9.msdn.com/Events/Connect/2017/B111)
* [Microsoft Graph](https://developer.microsoft.com/graph)