---
Description: Learn how to create effective and user-focused notifications that make your users productive and happy.
title: Toast UX Guidance
label: Toast UX Guidance
template: detail.hbs
ms.date: 05/18/2018
ms.topic: article
keywords: windows 10, uwp, notification, collection, group, ux, ux guidance, guidance, action, toast, action center, noninterruptive, effective notifications, nonintrusive notifications, actionable, manage, organize
ms.localizationpriority: medium
---
# Toast Notification UX Guidance
Notifications are a necessary part of modern life; they help users be more productive and engaged with apps and websites, as well as stay current with any updates. However, notifications can quickly turn from useful to overbearing and intrusive if they are not designed in a user-centric way. Your notifications are one right-click away from being turned off, and it's unlikely once they are turned off, they will be turned on again.  So make sure your notifications are respectful of the user's screen space and time, so you can keep this engagement channel open.

> **Important APIs**: [Windows Community Toolkit Notifications nuget package](https://www.nuget.org/packages/Microsoft.Toolkit.Uwp.Notifications/)

We've analyzed our Windows telemetry, as well as other first and third party case studies, to come up with four rules around what makes a great notification story.  We are confident these rules are universally applicable, regardless of the platform, and will help your notifications have a positive impact on your users.

## 1. Actionable notifications
Actionable notifications allow your users to be productive without opening your app.  While it is great to have app launches, this isn't the only measure of success, and enabling users to take accomplish small tasks without having to go into your app can be a very powerful tool in delighting your users.

![Actionable notification with input text box and buttons to set reminders and respond to the notification](images/actionable-notification-example01.png)

Above is an example of a notification that leverages actions. The feeling of finishing tasks is a universally positive feeling, and you can bring that feeling to your app or website by sending notifications that have actionable content in them. Actionable notifications can also help increase productivity, both in enterprise and consumer scenarios, by decreasing the time to action users go through to accomplish these smaller tasks. We recommend including actions that your users regularly take, or things that you are trying to train your users to do.  Some examples include:
* Liking, favoriting, flagging, or starring content
* Approving or denying expense reports, time off, permissions, etc.
* Inline replying to messages, emails, group chats, comments, etc.
* Completing orders using [pending update](toast-pending-update.md)
* Setting alerts or reminders for another time, as well as potentially booking time on a calendar

Actionable notifications are a very powerful tool to help your users feel productive, accomplish tasks, and have a great and efficient experience with your app or website.  There are lots of opportunities here! If you want help brainstorming ideas, feel free to reach out to the windows notifications team.

## 2. Timing and urgency
Contrary to how we often think about notifications, real time is not necessarily best! We urge developers to think about the user and if the notification they are sending is urgent information or not. Users can easily be overloaded with too much information and get frustrated if they are being interrupted while they are trying to focus. Windows provides a few options for how to consider the intrusiveness of the notifications you are sending:

**Raw notifications:** Using [raw notifications](raw-notification-overview.md) can be beneficial for many reasons, especially when it comes ot minimizing interruptions to the user.  Sending raw notifications will wake your app up in the background, so you can assess whether the notification makes sense to deliver immediately in your app's context. If it is something you feel should be shown to the user right away, you are able to pop a [local toast](send-local-toast.md) from there.  If it is something the user does not need to see right now, you are able to create a [scheduled toast](/archive/blogs/tiles_and_toasts/quickstart-sending-an-alarm-in-windows-10) that will fire at a later time.


**Ghost toast:** you can also fire a notification that will skip popping up in the bottom right corner of the screen, and instead send the notification directly to action center. This is accomplished by setting the [SupressPopup property](/uwp/api/windows.ui.notifications.toastnotification.suppresspopup) to True. Although there might be some skepticism around not popping notifications outside of action center, we see a 2-3 times higher engagement for toasts that live in action center over popped toast.  Users are more responsive when they are ready to receive notifications and can control when they are interrupted, which is why content in action center can be so much more effective for noninvasively notifying users.

## 3. Clear out the Clutter
Notifications can persist in Action Center for a fairly long time (default three days).  It is imperative that you make sure the content that lives here is up to date and relevant every time the user opens action center. You are wasting the user's screen space, and taking up slots that could be used for something more up-to-date.  Let's say the user installs your email management app, and receives ten emails and ten notifications along with those emails.  Depending on your desired experience, you might consider clearing those notifications if the user has read the corresponding email, or opened up the app as a way to remove old clutter from action center.

We have a series of [ToastNotificationHistory](/uwp/api/windows.ui.notifications.toastnotificationhistory) APIs that enable you to see what content is in action center, as well as manage these notifications. Be respectful of the user's screen space and take care that you are only showing relevant and current content to users.

## 4. Keeping Organized
As mentioned previously, the content in Action center does persist for three days.  To help your users pick out the information they are looking for quickly, organize the notifications in action center using [headers](./toast-headers.md) or [collections](/uwp/api/windows.ui.notifications.toastcollection). You can see an example of a header below.

![Toast examples with Headers labeled 'Camping!!'](images/toast-headers-action-center.png)

Group these notifications in a way so that relevant content stays together (i.e. think separating out different sports leagues in a sports app, or sorting messages by group chat). Collections are a more obvious way to group notifications, whereas headers are more subtle, but both allow users to triage and pick out notifications more quickly.



These four points above are guidance that we have found effective through our own analysis of telemetry, and through first and third party experiments. Keep in mind, however, that these guidelines are just that: guidelines.  We are confident these rules will help increase engagement and productivity of your notifications, but nothing can substitute user-centric thinking, and learning from your own data.  

## Related topics

* [Toast content](adaptive-interactive-toasts.md)
* [Raw notifications](raw-notification-overview.md)
* [Pending update](toast-pending-update.md)
* [Notifications library on GitHub (part of the Windows Community Toolkit)](https://github.com/windows-toolkit/WindowsCommunityToolkit/tree/master/Microsoft.Toolkit.Uwp.Notifications)