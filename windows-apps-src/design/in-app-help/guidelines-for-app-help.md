---
Description: These guidelines describe how to design effective Help content for your app.
title: Guidelines for app help
label: Guidelines for app help
template: detail.hbs
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.assetid: c3e73f9b-4839-4804-b379-c95b0ca4fbe8
ms.localizationpriority: medium
---

# Guidelines for app help

Applications can be complex, and providing effective help for your users can greatly improve their experience. Not all applications need to provide help for their users, and what sort of help should be provided can vary greatly, depending on the application.

If you decide to provide help, follow these guidelines when creating it. Help that isn't helpful can be worse than no help at all.

## Intuitive Design

As useful as help content can be, your app cannot rely on it to provide a good experience for the user. If the user is unable to immediately discover and use the critical functions of your app, the user will not use your app. No amount or quality help will change that first impression.

An intuitive and user-friendly design is the first step to writing useful help. Not only does it keep the user engaged for long enough for them to use more advanced features, but it also provides them with knowledge of an app's core functions, which they can build upon as they continue to use the app and learn.

## General instructions

A user will not look for help content unless they already have a problem, so help needs to provide a quick and effective answer to that problem. If help is not immediately useful, or if help is too complicated, then users are more likely to ignore it.

All help, no matter what kind, should follow these principles:

-   **Easy to understand:** Help that confuses the user is worse than no help at all.

-   **Straightforward:** Users looking for help want clear answers presented directly to them.

-   **Relevant:** Users do not want to have to search for their specific issue. They want the most relevant help presented straight to them (this is called "Contextual Help"), or they want an easily navigated interface.

-   **Direct:** When a user looks for help, they want to see help. If your app includes pages for reporting bugs, giving feedback, viewing term of service, or similar functions, it is fine if your help links to those pages. But they should be included as an afterthought on the main help page, and not as items of equal or greater importance.

-   **Consistent:** No matter the type, help is still a part of your app, and should be treated as any other part of the UI. The same design principles of usability, accessibility, and style which are used throughout the rest of your app should also be present in the help you offer.

## Types of help

There are three primary categories of help content, each with varying strengths and suitable for different purposes. Use any combination of them in your app, depending on your needs.

#### Instructional UI

Normally, users should be able to use all the core functions of your app without instruction. But sometimes, your app will depend on use of a specific gesture, or there may be secondary features of your app which are not immediately obvious. In this case, instructional UI should be used to educate users with instructions on how to perform specific tasks.

[See guidelines for instructional UI](instructional-ui.md)

#### In-app help

The standard method of presenting help is to display it within the application at the user's request. There are several ways in which this can be implemented, such as in help pages or informative descriptions. This method is ideal for general-purpose help, that directly answers a user's questions without complexity.

[See guidelines for in-app help](in-app-help.md)

#### External help

For detailed tutorials, advanced functions, or libraries of help topics too large to fit within your application, links to external web pages are ideal. These links should be used sparingly if possible, as they remove the user from the application experience.

[See guidelines for external help](external-help.md)


