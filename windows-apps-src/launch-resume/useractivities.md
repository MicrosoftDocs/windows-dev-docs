---
title: Continue user activity, even across devices
description: This topic describes how to help users resume what they were doing in your app, even across multiple devices.
keywords: user activity, user activities, timeline, cortana pick up where you left off, cortana pick up where i left off, project rome
ms.date: 04/27/2018
ms.topic: article


ms.localizationpriority: medium
---
# Continue user activity, even across devices

This topic describes how to help users resume what they were doing in your app on their PC, and across devices.

## User Activities and Timeline

Our time each day is spread across multiple devices. We might use our phone while on the bus, a PC during the day, then a phone or tablet in the evening. Starting in Windows 10 Build 1803 or higher, creating a [User Activity](/uwp/api/windows.applicationmodel.useractivities.useractivity) makes that activity appear in Windows Timeline and in Cortana's Pick up where I left off feature. Timeline is a rich task view that takes advantage of User Activities to show a chronological view of what you’ve been working on. It can also include what you were working on across devices.

![Windows timeline image](images/timeline.png)

Likewise, linking your phone to your Windows PC allows you to continue what you were doing previously on your iOS or Android device.

Think of a **UserActivity** as something specific the user was working on within your app. For example, if you are using a RSS reader, a **UserActivity** could be the feed that you are reading. If you are playing a game, the **UserActivity** could be the level that you are playing. If you are listening to a music app, the **UserActivity** could be the playlist that you are listening to. If you are working on a document, the **UserActivity** could be where you left off working on it, and so on.  In short, a **UserActivity** represents a destination within your app so that enables the user to resume what they were doing.

When you engage with a **UserActivity** by calling [UserActivity.CreateSession](/uwp/api/windows.applicationmodel.useractivities.useractivity.createsession), the system creates a history record indicating the start and end time for that **UserActivity**. As you re-engage with that **UserActivity** over time, multiple history records are recorded for it.

## Add User Activities to your app

A [UserActivity](/uwp/api/windows.applicationmodel.useractivities.useractivity) is the unit of user engagement in Windows. It has three parts: a URI used to activate the app the activity belongs to, visuals, and metadata that describes the activity.

1. The [ActivationUri](/uwp/api/windows.applicationmodel.useractivities.useractivity.activationuri#Windows_ApplicationModel_UserActivities_UserActivity_ActivationUri) is used to resume the application with a specific context. Typically, this link takes the form of protocol handler for a scheme (for example, “my-app://page2?action=edit”) or of an AppUriHandler (for example, http://constoso.com/page2?action=edit).
2. [VisualElements](/uwp/api/windows.applicationmodel.useractivities.useractivity.visualelements) exposes a class that allows the user to visually identify an activity with a title, description, or Adaptive Card elements.
3. Finally, [Content](/uwp/api/windows.applicationmodel.useractivities.useractivityvisualelements.content#Windows_ApplicationModel_UserActivities_UserActivityVisualElements_Content) is where you can store metadata for the activity that can be used to group and retrieve activities under a specific context. Often, this takes the form of [https://schema.org](https://schema.org) data.

To add a **UserActivity** to your app:

1. Generate **UserActivity** objects when your user’s context changes within the app (such as page navigation, new game level, etc.)
2. Populate **UserActivity** objects with the minimum set of required fields: [ActivityId](/uwp/api/windows.applicationmodel.useractivities.useractivity.activityid#Windows_ApplicationModel_UserActivities_UserActivity_ActivityId), [ActivationUri](/uwp/api/windows.applicationmodel.useractivities.useractivity.activationuri), and [UserActivity.VisualElements.DisplayText](/uwp/api/windows.applicationmodel.useractivities.useractivityvisualelements.displaytext#Windows_ApplicationModel_UserActivities_UserActivityVisualElements_DisplayText).
3. Add a custom scheme handler to your app so it can be re-activated by a **UserActivity**.

A **UserActivity** can be integrated into an app with just a few lines of code. For example, imagine this code in MainPage.xaml.cs, inside the MainPage class (note: assumes `using Windows.ApplicationModel.UserActivities;`):

```csharp
UserActivitySession _currentActivity;
private async Task GenerateActivityAsync()
{
    // Get the default UserActivityChannel and query it for our UserActivity. If the activity doesn't exist, one is created.
    UserActivityChannel channel = UserActivityChannel.GetDefault();
    UserActivity userActivity = await channel.GetOrCreateUserActivityAsync("MainPage");
 
    // Populate required properties
    userActivity.VisualElements.DisplayText = "Hello Activities";
    userActivity.ActivationUri = new Uri("my-app://page2?action=edit");
     
    //Save
    await userActivity.SaveAsync(); //save the new metadata
 
    // Dispose of any current UserActivitySession, and create a new one.
    _currentActivity?.Dispose();
    _currentActivity = userActivity.CreateSession();
}
```

The first line in the `GenerateActivityAsync()` method above gets a user’s [UserActivityChannel](/uwp/api/windows.applicationmodel.useractivities.useractivitychannel). This is the feed that this app’s activities will be published to. The next line queries the channel of an activity called `MainPage`.

* Your app should name activities in such a way that same ID is generated each time the user is in a particular location in the app. For example, if your app is page-based, use an identifier for the page; if its document based, use the name of the doc (or a hash of the name).
* If there is an existing activity in the feed with the same ID, that activity will be returned from the channel with `UserActivity.State` set to [Published](/uwp/api/windows.applicationmodel.useractivities.useractivitystate)). If there is no activity with that name, and new activity is returned with `UserActivity.State` set to **New**.
* Activities are scoped to your app. You do not need to worry about your activity ID colliding with IDs in other apps.

After getting or creating the **UserActivity**, specify the other two required fields:  `UserActivity.VisualElements.DisplayText`and `UserActivity.ActivationUri`.

Next, save the **UserActivity** metadata by calling [SaveAsync](/uwp/api/windows.applicationmodel.useractivities.useractivity.saveasync), and finally [CreateSession](/uwp/api/windows.applicationmodel.useractivities.useractivity.createsession), which returns a [UserActivitySession](/uwp/api/windows.applicationmodel.useractivities.useractivitysession). The **UserActivitySession** is the object that we can use to manage when the user is actually engaged with the **UserActivity**. For example, we should call `Dispose()` on the **UserActivitySession** when the user leaves the page. In the example above, we also call `Dispose()` on `_currentActivity` before calling `CreateSession()`. This is because we made `_currentActivity` a member field of our page, and we want to stop any existing activity before we start the new one (note: the `?` is the [null-conditional operator](/dotnet/csharp/language-reference/operators/member-access-operators#null-conditional-operators--and-) which tests for null before performing the member access).

Since, in this case, the `ActivationUri` is a custom scheme, we also need to register the protocol in the application manifest. This is done in the Package.appmanifest XML file, or by using the designer.

To make the change with the designer, double-click the Package.appmanifest file in your project to launch the designer, select the **Declarations** tab and add a **Protocol** definition. The only property that needs to be filled out, for now, is **Name**. It should match the URI we specified above, `my-app`.

Now we need to write some code to tell the app what to do when it’s been activated by a protocol. We’ll override the `OnActivated` method in App.xaml.cs to pass the URI on to the main page, like so:

```csharp
protected override void OnActivated(IActivatedEventArgs e)
{
    if (e.Kind == ActivationKind.Protocol)
    {
        var uriArgs = e as ProtocolActivatedEventArgs;
        if (uriArgs != null)
        {
            if (uriArgs.Uri.Host == "page2")
            {
                // Navigate to the 2nd page of the  app
            }
        }
    }
    Window.Current.Activate();
}
```

What this code does is detect whether the app was activated via a protocol. If it was, then it looks to see what the app should do to resume the task it is being activated for. Being a simple app, the only activity this app resumes is putting you on the secondary page when the app comes up.

## Use Adaptive Cards to improve the Timeline experience

User Activities appear in Cortana and Timeline. When activities appear in Timeline, we display them using the [Adaptive Card](https://adaptivecards.io/) framework. If you do not provide an adaptive card for each activity, Timeline will automatically create a simple activity card based on your application name and icon, the title field and optional description field. Below is an example Adaptive Card payload and the card it produces.

![An adaptive card](images/adaptivecard.png)]

Example adaptive card payload JSON string:

```json
{ 
  "$schema": "http://adaptivecards.io/schemas/adaptive-card.json", 
  "type": "AdaptiveCard", 
  "version": "1.0",
  "backgroundImage": "https://winblogs.azureedge.net/win/2017/11/eb5d872c743f8f54b957ff3f5ef3066b.jpg", 
  "body": [ 
    { 
      "type": "Container", 
      "items": [ 
        { 
          "type": "TextBlock", 
          "text": "Windows Blog", 
          "weight": "bolder", 
          "size": "large", 
          "wrap": true, 
          "maxLines": 3 
        }, 
        { 
          "type": "TextBlock", 
          "text": "Training Haiti’s radiologists: St. Louis doctor takes her teaching global", 
          "size": "default", 
          "wrap": true, 
          "maxLines": 3 
        } 
      ] 
    } 
  ]
}
```

Add the Adaptive Cards payload as a JSON string to the **UserActivity** like this:

```csharp
activity.VisualElements.Content = 
Windows.UI.Shell.AdaptiveCardBuilder.CreateAdaptiveCardFromJson(jsonCardText); // where jsonCardText is a JSON string that represents the card
```

## Cross-platform and Service-to-service integration

If your app runs cross-platform (for example on Android and iOS), or maintains user state in the cloud, you can publish UserActivities via [Microsoft Graph](https://developer.microsoft.com/graph).
Once your application or service is authenticated with a Microsoft Account, it just takes two simple REST calls to generate [Activity](/graph/api/resources/projectrome-activity) and [History](/graph/api/resources/projectrome-historyitem) objects, using the same data as described above.

## Summary

You can use the [UserActivity](/uwp/api/windows.applicationmodel.useractivities) API to make your app appear in Timeline and Cortana.
* Learn more about the [**UserActivity** API](/uwp/api/windows.applicationmodel.useractivities)
* Check out the [sample code](https://github.com/Microsoft/project-rome).
* See [more sophisticated Adaptive Cards](https://adaptivecards.io/).
* Publish a **UserActivity** from iOS, Android or your web service via [Microsoft Graph](https://developer.microsoft.com/graph).
* Learn more about [Project Rome on GitHub](https://github.com/Microsoft/project-rome).

## Key APIs

* [UserActivities namespace](/uwp/api/windows.applicationmodel.useractivities)

## Related topics

* [User Activities (Project Rome docs)](/windows/project-rome/user-activities/)
* [Adaptive cards](/adaptive-cards/)
* [Adaptive cards visualizer, samples](https://adaptivecards.io/)
* [Handle URI activation](./handle-uri-activation.md)
* [Engaging with your customers on any platform using the Microsoft Graph, Activity Feed, and Adaptive Cards](https://channel9.msdn.com/Events/Connect/2017/B111)
* [Microsoft Graph](https://developer.microsoft.com/graph)