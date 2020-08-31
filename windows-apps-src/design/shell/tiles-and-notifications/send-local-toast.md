---
Description: Learn how to send a local toast notification and handle the user clicking the toast.
title: Send a local toast notification
ms.assetid: E9AB7156-A29E-4ED7-B286-DA4A6E683638
label: Send a local toast notification
template: detail.hbs
ms.date: 05/19/2017
ms.topic: article
keywords: windows 10, uwp, send toast notifications, notifications, send notifications, toast notifications, how to, quickstart, getting started, code sample, walkthrough
ms.localizationpriority: medium
---
# Send a local toast notification


A toast notification is a message that an app can construct and deliver to the user while they are not currently inside your app. This Quickstart walks you through the steps to create, deliver, and display a Windows 10 toast notification with the new adaptive templates and interactive actions. These actions are demonstrated through a local notification, which is the simplest notification to implement.

> [!IMPORTANT]
> Desktop applications (including packaged [MSIX](/windows/msix/desktop/source-code-overview) apps, apps that use [sparse packages](/windows/apps/desktop/modernize/grant-identity-to-nonpackaged-apps) to obtain package identity, and classic non-packaged Win32 apps) have different steps for sending notifications and handling activation. Please see the [Desktop apps](toast-desktop-apps.md) documentation to learn how to implement toasts.

We will go through the following things:

### Sending a toast

* Constructing the visual part (text and image) of the notification
* Adding actions to the notification
* Setting an expiration time on the toast
* Setting tag/group so you can replace/remove the toast at a later time
* Sending your toast using the local APIs

### Handling activation

* Handling activation when the body or buttons are clicked
* Handling foreground activation
* Handling background activation

> **Important APIs**: [ToastNotification Class](/uwp/api/Windows.UI.Notifications.ToastNotification), [ToastNotificationActivatedEventArgs Class](/uwp/api/Windows.ApplicationModel.Activation.ToastNotificationActivatedEventArgs)


## Prerequisites

To fully understand this topic, the following will be helpful...

* A working knowledge of toast notification terms and concepts. For more information, see [Toast and action center overview](/archive/blogs/tiles_and_toasts/toast-notification-and-action-center-overview-for-windows-10).
* A familiarity with Windows 10 toast notification content. For more information, see [toast content documentation](adaptive-interactive-toasts.md).
* A Windows 10 UWP app project

> [!NOTE]
> Unlike Windows 8/8.1, you no longer need to declare in your app's manifest that your app is capable of showing toast notifications. All apps are capable of sending and displaying toast notifications.

> [!NOTE]
> **Windows 8/8.1 apps**: Please use the [archived documentation](/previous-versions/windows/apps/hh868254(v=win.10)).


## Install NuGet packages

We recommend installing the two following NuGet packages to your project. Our code sample will use these packages. At the end of the article we'll provide the "Vanilla" code snippets that don't use any NuGet packages.

* [Microsoft.Toolkit.Uwp.Notifications](https://www.nuget.org/packages/Microsoft.Toolkit.Uwp.Notifications/): Generate toast payloads via objects instead of raw XML.
* [QueryString.NET](https://www.nuget.org/packages/QueryString.NET/): Generate and parse query strings with C#


## Add namespace declarations

`Windows.UI.Notifications` includes the toast APIs.

```csharp
using Windows.UI.Notifications;
using Microsoft.Toolkit.Uwp.Notifications; // Notifications library
using Microsoft.QueryStringDotNET; // QueryString.NET
```


## Send a toast

In Windows 10, your toast notification content is described using an adaptive language that allows great flexibility with how your notification looks. See the [toast content documentation](adaptive-interactive-toasts.md) for more information.

### Constructing the visual part of the content

Let's start by constructing the visual part of the content, which includes the text and images you want the user to see.

Thanks to the Notifications library, generating the XML content is straightforward. If you don't install the Notifications library from NuGet, you have to construct the XML manually, which leaves room for errors.

> [!NOTE]
> Images can be used from the app's package, the app's local storage, or from the web. As of the Fall Creators Update, web images can be up to 3 MB on normal connections and 1 MB on metered connections. On devices not yet running the Fall Creators Update, web images must be no larger than 200 KB.

```csharp
// In a real app, these would be initialized with actual data
string title = "Andrew sent you a picture";
string content = "Check this out, Happy Canyon in Utah!";
string image = "https://picsum.photos/360/202?image=883";
string logo = "ms-appdata:///local/Andrew.jpg";
 
// Construct the visuals of the toast
ToastVisual visual = new ToastVisual()
{
    BindingGeneric = new ToastBindingGeneric()
    {
        Children =
        {
            new AdaptiveText()
            {
                Text = title
            },
 
            new AdaptiveText()
            {
                Text = content
            },
 
            new AdaptiveImage()
            {
                Source = image
            }
        },
 
        AppLogoOverride = new ToastGenericAppLogo()
        {
            Source = logo,
            HintCrop = ToastGenericAppLogoCrop.Circle
        }
    }
};
```


### Constructing actions part of the content

Now let's add actions to the content.

In the below example, we included an input element that allows the user to input text, which is returned to the app when the user clicks one of the buttons or the toast itself.

We then added two buttons, each with its own activation type, content, and arguments.
* **ActivationType** is used to specify how your app wants to be activated when this action is performed by the user. You can choose to launch your app in the foreground, launch a background task, or protocol launch another app. Whether your app chooses foreground or background, you will always receive the user input and the arguments you specified, so your app can perform the correct action, like sending the message or opening a conversation.

```csharp
// In a real app, these would be initialized with actual data
int conversationId = 384928;
 
// Construct the actions for the toast (inputs and buttons)
ToastActionsCustom actions = new ToastActionsCustom()
{
    Inputs =
    {
        new ToastTextBox("tbReply")
        {
            PlaceholderContent = "Type a response"
        }
    },
 
    Buttons =
    {
        new ToastButton("Reply", new QueryString()
        {
            { "action", "reply" },
            { "conversationId", conversationId.ToString() }
 
        }.ToString())
        {
            ActivationType = ToastActivationType.Background,
            ImageUri = "Assets/Reply.png",
 
            // Reference the text box's ID in order to
            // place this button next to the text box
            TextBoxId = "tbReply"
        },
 
        new ToastButton("Like", new QueryString()
        {
            { "action", "like" },
            { "conversationId", conversationId.ToString() }
 
        }.ToString())
        {
            ActivationType = ToastActivationType.Background
        },
 
        new ToastButton("View", new QueryString()
        {
            { "action", "viewImage" },
            { "imageUrl", image }
 
        }.ToString())
    }
};
```


### Combining the above to construct the full content

The construction of the content is now complete, and we can use it to instantiate your [**ToastNotification**](/uwp/api/Windows.UI.Notifications.ToastNotification) object.

**Note**: you can also provide an activation type inside the root element, to specify what type of activation needs to happen when the user taps on the body of the toast notification. Normally, tapping the body of the toast should launch your app in the foreground to create a consistent user experience, but you can use other activation types to fit your specific scenario where it makes most sense to the user.

You should always set the **Launch** property, so when user taps the body of the toast and your app is launched, your app knows what content it should display.

```csharp
// Now we can construct the final toast content
ToastContent toastContent = new ToastContent()
{
    Visual = visual,
    Actions = actions,
 
    // Arguments when the user taps body of toast
    Launch = new QueryString()
    {
        { "action", "viewConversation" },
        { "conversationId", conversationId.ToString() }
 
    }.ToString()
};
 
// And create the toast notification
var toast = new ToastNotification(toastContent.GetXml());
```


## Set an expiration time

In Windows 10, all toast notifications go in Action Center after they are dismissed or ignored by the user, so users can look at your notification after the popup is gone.

However, if the message in your notification is only relevant for a period of time, you should set an expiration time on the toast notification so the users do not see stale information from your app. For example, if a promotion is only valid for 12 hours, set the expiration time to 12 hours. In the code below, we set the expiration time to be 2 days.

> [!NOTE]
> The default and maximum expiration time for local toast notifications is 3 days.

```csharp
toast.ExpirationTime = DateTime.Now.AddDays(2);
```


## Provide a primary key for your toast

If you want to programmatically remove or replace the notification you send, you need to use the Tag property (and optionally the Group property) to provide a primary key for your notification. Then, you can use this primary key in the future to remove or replace the notification.

To see more details on replacing/removing already delivered toast notifications, please see [Quickstart: Managing toast notifications in action center (XAML)](/previous-versions/windows/apps/dn631260(v=win.10)).

Tag and Group combined act as a composite primary key. Group is the more generic identifier, where you can assign groups like "wallPosts", "messages", "friendRequests", etc. And then Tag should uniquely identify the notification itself from within the group. By using a generic group, you can then remove all notifications from that group by using the [RemoveGroup API](/uwp/api/Windows.UI.Notifications.ToastNotificationHistory#Windows_UI_Notifications_ToastNotificationHistory_RemoveGroup_System_String_).

```csharp
toast.Tag = "18365";
toast.Group = "wallPosts";
```


## Send the notification

Once you have initialized your toast, simply create a [ToastNotifier](/uwp/api/windows.ui.notifications.toastnotifier) and call Show(), passing in your toast notification.

```csharp
ToastNotificationManager.CreateToastNotifier().Show(toast);
```


## Clear your notifications

UWP apps are responsible for removing and clearing their own notifications. When your app is launched, we do NOT automatically clear your notifications.

Windows will only automatically remove a notification if the user explicitly clicks the notification.

Here's an example of what a messaging app should do…

1. User receives multiple toasts about new messages in a conversation
2. User taps one of those toasts to open the conversation
3. The app opens the conversation and then clears all toasts for that conversation (by using [RemoveGroup](/uwp/api/Windows.UI.Notifications.ToastNotificationHistory#Windows_UI_Notifications_ToastNotificationHistory_RemoveGroup_System_String_) on the app-supplied group for that conversation)
4. User's Action Center now properly reflects the notification state, since there are no stale notifications for that conversation left in Action Center.

To learn about clearing all notifications or removing specific notifications, see [Quickstart: Managing toast notifications in action center (XAML)](/previous-versions/windows/apps/dn631260(v=win.10)).


## Activation handling

In Windows 10, when the user clicks on your toast, you can have the toast activate your app in two different ways...

* Foreground activation
* Background activation

> [!NOTE]
> If you are using the legacy toast templates from Windows 8.1, **OnLaunched** will be called instead of **OnActivated**. The following documentation only applies to modern Windows 10 notifications utilizing the Notifications library (or the ToastGeneric template if using raw XML).


### Handling foreground activation

In Windows 10, when a user clicks a modern toast (or a button on the toast), **OnActivated** is invoked instead of **OnLaunched**, with a new activation kind – **ToastNotification**. Thus, the developer is able to easily distinguish a toast activation and perform tasks accordingly.

In the example you see below, you can retrieve the arguments string you initially provided in the toast content. You can also retrieve the input the user provided in your text boxes and selection boxes.

> [!IMPORTANT]
> You must initialize your frame and activate your window just like your **OnLaunched** code. **OnLaunched is NOT called if the user clicks on your toast**, even if your app was closed and is launching for the first time. We often recommend combining **OnLaunched** and **OnActivated** into your own `OnLaunchedOrActivated` method since the same initialization needs to occur in both.

```csharp
protected override void OnActivated(IActivatedEventArgs e)
{
    // Get the root frame
    Frame rootFrame = Window.Current.Content as Frame;
 
    // TODO: Initialize root frame just like in OnLaunched
 
    // Handle toast activation
    if (e is ToastNotificationActivatedEventArgs)
    {
        var toastActivationArgs = e as ToastNotificationActivatedEventArgs;
                 
        // Parse the query string (using QueryString.NET)
        QueryString args = QueryString.Parse(toastActivationArgs.Argument);
 
        // See what action is being requested 
        switch (args["action"])
        {
            // Open the image
            case "viewImage":
 
                // The URL retrieved from the toast args
                string imageUrl = args["imageUrl"];
 
                // If we're already viewing that image, do nothing
                if (rootFrame.Content is ImagePage && (rootFrame.Content as ImagePage).ImageUrl.Equals(imageUrl))
                    break;
 
                // Otherwise navigate to view it
                rootFrame.Navigate(typeof(ImagePage), imageUrl);
                break;
                             
 
            // Open the conversation
            case "viewConversation":
 
                // The conversation ID retrieved from the toast args
                int conversationId = int.Parse(args["conversationId"]);
 
                // If we're already viewing that conversation, do nothing
                if (rootFrame.Content is ConversationPage && (rootFrame.Content as ConversationPage).ConversationId == conversationId)
                    break;
 
                // Otherwise navigate to view it
                rootFrame.Navigate(typeof(ConversationPage), conversationId);
                break;
        }
 
        // If we're loading the app for the first time, place the main page on
        // the back stack so that user can go back after they've been
        // navigated to the specific page
        if (rootFrame.BackStack.Count == 0)
            rootFrame.BackStack.Add(new PageStackEntry(typeof(MainPage), null, null));
    }
 
    // TODO: Handle other types of activation
 
    // Ensure the current window is active
    Window.Current.Activate();
}
```


## Handling background activation

When you specify background activation on your toast (or on a button inside the toast), your background task will be executed instead of activating your foreground app.

For more information on background tasks, please see [Support your app with background tasks](../../../launch-resume/support-your-app-with-background-tasks.md).

If you are targeting build 14393 or higher, you can use in-process background tasks, which greatly simplify things. Note that in-process background tasks will fail to run on older versions of Windows. We'll use an in-process background task in this code sample.

```csharp
const string taskName = "ToastBackgroundTask";

// If background task is already registered, do nothing
if (BackgroundTaskRegistration.AllTasks.Any(i => i.Value.Name.Equals(taskName)))
    return;

// Otherwise request access
BackgroundAccessStatus status = await BackgroundExecutionManager.RequestAccessAsync();

// Create the background task
BackgroundTaskBuilder builder = new BackgroundTaskBuilder()
{
    Name = taskName
};

// Assign the toast action trigger
builder.SetTrigger(new ToastNotificationActionTrigger());

// And register the task
BackgroundTaskRegistration registration = builder.Register();
```


Then in your App.xaml.cs, override the OnBackgroundActivated method you can retrieve the pre-defined arguments and user input, similar to the foreground activation.

```csharp
protected override async void OnBackgroundActivated(BackgroundActivatedEventArgs args)
{
    var deferral = args.TaskInstance.GetDeferral();
 
    switch (args.TaskInstance.Task.Name)
    {
        case "ToastBackgroundTask":
            var details = args.TaskInstance.TriggerDetails as ToastNotificationActionTriggerDetail;
            if (details != null)
            {
                string arguments = details.Argument;
                var userInput = details.UserInput;

                // Perform tasks
            }
            break;
    }
 
    deferral.Complete();
}
```



## Plain "Vanilla" code snippets

If you're not using the Notifications library from NuGet, you can manually construct your XML as seen below to create a [ToastNotification](/uwp/api/Windows.UI.Notifications.ToastNotification).

```csharp
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;

// In a real app, these would be initialized with actual data
string title = "Andrew sent you a picture";
string content = "Check this out, Happy Canyon in Utah!";
string image = "http://blogs.msdn.com/cfs-filesystemfile.ashx/__key/communityserver-blogs-components-weblogfiles/00-00-01-71-81-permanent/2727.happycanyon1_5B00_1_5D00_.jpg";
string logo = "ms-appdata:///local/Andrew.jpg";
 
// TODO: all values need to be XML escaped
 
// Construct the visuals of the toast
string toastVisual =
$@"<visual>
  <binding template='ToastGeneric'>
    <text>{title}</text>
    <text>{content}</text>
    <image src='{image}'/>
    <image src='{logo}' placement='appLogoOverride' hint-crop='circle'/>
  </binding>
</visual>";

// In a real app, these would be initialized with actual data
int conversationId = 384928;
 
// Generate the arguments we'll be passing in the toast
string argsReply = $"action=reply&conversationId={conversationId}";
string argsLike = $"action=like&conversationId={conversationId}";
string argsView = $"action=viewImage&imageUrl={Uri.EscapeDataString(image)}";
 
// TODO: all args need to be XML escaped
 
string toastActions =
$@"<actions>
 
  <input
      type='text'
      id='tbReply'
      placeHolderContent='Type a response'/>
 
  <action
      content='Reply'
      arguments='{argsReply}'
      activationType='background'
      imageUri='Assets/Reply.png'
      hint-inputId='tbReply'/>
 
  <action
      content='Like'
      arguments='{argsLike}'
      activationType='background'/>
 
  <action
      content='View'
      arguments='{argsView}'/>
 
</actions>";

// Now we can construct the final toast content
string argsLaunch = $"action=viewConversation&conversationId={conversationId}";
 
// TODO: all args need to be XML escaped
 
string toastXmlString =
$@"<toast launch='{argsLaunch}'>
    {toastVisual}
    {toastActions}
</toast>";
 
// Parse to XML
XmlDocument toastXml = new XmlDocument();
toastXml.LoadXml(toastXmlString);
 
// Generate toast
var toast = new ToastNotification(toastXml);
```


## Resources

* [Full code sample on GitHub](https://github.com/WindowsNotifications/quickstart-sending-local-toast-win10)
* [Toast content documentation](adaptive-interactive-toasts.md)
* [ToastNotification Class](/uwp/api/Windows.UI.Notifications.ToastNotification)
* [ToastNotificationActivatedEventArgs Class](/uwp/api/Windows.ApplicationModel.Activation.ToastNotificationActivatedEventArgs)