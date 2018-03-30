---
title: Introduction to Social Manager
author: aablackm
description: Learn about the Xbox Live Social Manager API to keep track of online friends.
ms.assetid: d4c6d5aa-e18c-4d59-91f8-63077116eda3
ms.author: aablackm
ms.date: 03/26/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
ms.localizationpriority: low
---
# Introduction to Social Manager

## Description

Xbox Live provides a rich social graph that titles can use for various scenarios.

Using the social APIs in the Xbox Live API (XSAPI) to obtain and maintain information about a social graph is complex, and keeping this information up to date can be complicated.  Not doing this correctly can result in performance issues, stale data, or being throttled due to calling the Xbox Live social services more frequently than necessary.

The Social Manager solves this problem by:

* Creating a simple API to call
* Creating up to date information using the real time activity service behind the scenes
* Developers can call the Social Manager API synchronously without any extra strain on the service

The Social Manager masks the complexity of dealing with multiple RTA subscriptions, and refreshing data for users and allows developers to easily get the up to date graph they want to create interesting scenarios.

For a look at the Social Manager memory and performance characteristics take a look at [Social Manager Memory and Performance Overview](social-manager-memory-and-performance-overview.md)

## Features

The Social Manager provides the following features:

* Simplified Social API
* Up to date social graph
* Control over the verbosity of information displayed
* Reduce number of calls to Xbox Live services
  * This directly correlates to overall latency reduction in data acquisition
* Thread safe
* Efficiently keeping data up to date

## Core Concepts

**Social Graph**: A *social graph* is created for a local user on the device. This creates a structure that keeps information about all of a users friends up to date.

> [!NOTE]
> On Windows there can only be one local user

**Xbox Social User**: An *Xbox social user* is a full set of social data associated with a user from a group

**Xbox Social User Group**: A group is a collection of users that is used for things like populating UI. There are two types of groups

* **Filter Groups**: A filter group takes a local (calling) user's *social graph* and returns a consistently fresh set of users based on specified filter parameters
* **User Groups**: A user group takes a list of users and returns a consistently fresh view of those users. These users can be outside of a user's friends list.

In order to keep a *social user group* up to date the function `social_manager::do_work()` must be called every frame.

## API Overview

You will most frequently use the following key classes:

### Social Manager

* C++ API class name: social_manager
* WinRT(C#) API class name: [SocialManager](https://docs.microsoft.com/en-us/dotnet/api/microsoft.xbox.services.social.manager.socialmanager?view=xboxlive-dotnet-2017.11.20171204.01)

This is a singleton class that can be used to get **Xbox social user groups** which are the views described above.

The Social Manager will keep xbox social user groups up to date, and can filter user groups by presence or relationship to the user.  For example, an xbox social user group containing all of the user's friends who are online and playing the current title could be created.  This would be kept up to date as friends start or stop playing the title.

### Xbox social user group

* C++ API class name: xbox_social_user_group
* WinRT(C#) API class name: [XboxSocialUserGroup](https://docs.microsoft.com/en-us/dotnet/api/microsoft.xbox.services.social.manager.xboxsocialusergroup?view=xboxlive-dotnet-2017.11.20171204.01)

A group of users that meet certain criteria, as described above. Xbox social user groups expose what type of a group they are, which users are being tracked or what the filter set is on them, and the local user which the group belongs to.

You can find a complete description of the Social Manager APIs in the [Xbox Live API reference](https://aka.ms/xboxliveuwpdocs).
You can also find the WinRT APIs in the [Microsoft.Xbox.Services.Social.Manager.Namespace documentation](https://docs.microsoft.com/en-us/dotnet/api/microsoft.xbox.services.social.manager?view=xboxlive-dotnet-2017.11.20171204.01)

## Usage

### Creating a social user group from filters

In this scenario, you want a list of users from a filter, such as all the people this user is following or tagged as favorite.

#### Source example using the C++ API

```cpp
//#include "Social.h"

auto socialManager = social_manager::get_singleton_instance();

socialManger->add_local_user(
    xboxLiveContext->user(),
    social_manager_extra_detail_level::preferred_color_level | social_manager_extra_detail_level::title_history_level
    );

auto socialUserGroup = socialManager->create_social_user_group_from_filters(
    xboxLiveContext->user(),
    presence_filter::all,
    relationship_filter::friends
    );

while(true)
{
    // some update loop in the game
    socialManager->do_work();
    // TODO: render the friends list using game UI, passing in socialUserGroup->users()
}
```

#### Source example using the C# API

```csharp
// using Microsoft.Xbox.Services;
// using Microsoft.Xbox.Services.System;
// using Microsoft.Xbox.Services.Social.Manager;

socialManager = SocialManager.SingletonInstance;

socialManager.AddLocalUser(
     xboxLiveContext.User,
     SocialManagerExtraDetailLevel.PreferredColorLevel | SocialManagerExtraDetailLevel.TitleHistoryLevel
     );

socialUserGroup = socialManager.CreateSocialUserGroupFromFilters(
     xboxLiveContext.User,
     PresenceFilter.All,
     RelationshipFilter.Friends
     );

while(true)
{
     // some update loop in the game
     socialManager.DoWork();
     // // TODO: render the friends list using game UI, passing in socialUserGroup.Users
}

```

#### Events Returned

`local_user_added`(C++) | `LocalUserAdded`(C#) - Triggers when loading of users social graph is complete. Will indicate if any errors occurred during initialization

`social_user_group_loaded`(C++) | `SocialUserGroupLoaded`(C#) - Triggers when social user group has been created

`users_added_to_social_graph`(C++) | `UsersAddedToSocialGraph`(C#) - Triggers when users are loaded in

#### Additional details

The above example shows how to initialize the Social Manager for a user, create a social user group for that user, and keep it up to date.

The filtering options can be seen in the `presence_filter` and `relationship_filter` enums

In the game loop, the `do_work` function updates all created views with the latest snapshot of the users in that group.

The users in the view can be obtained by calling the `xbox_social_user_group::get_users()` function which returns a list of `xbox_social_user` objects.  The `xbox_social_user` contains the social information such as gamertag, gamerpic uri, etc.

### Create and update a social user group from list

In this scenario, you want the social information of a list of users such as users in a multiplayer session.

#### Source example using the C++ API

```cpp
//#include "Social.h"

auto socialManager = social_manager::get_singleton_instance();

socialManger->add_local_user(
    xboxLiveContext->user(),
    social_manager_extra_detail_level::preferred_color_level | social_manager_extra_detail_level::title_history_level
    );

auto socialUserGroup = socialManager->create_social_user_group_from_list(
    xboxLiveContext->user(),
    userList  // this is a std::vector<string_t> (list of xuids)
    );

while(true)
{
    // some update loop in the game
    socialManager->do_work();
    // TODO: render the friends list using game UI, passing in socialUserGroup->users()
}
```

#### Source example using the C# API

```csharp
// using Microsoft.Xbox.Services;
// using Microsoft.Xbox.Services.System;
// using Microsoft.Xbox.Services.Social.Manager;

socialManager = SocialManager.SingletonInstance;

socialManager.AddLocalUser(
     xboxLiveContext.User,
     SocialManagerExtraDetailLevel.PreferredColorLevel | SocialManagerExtraDetailLevel.TitleHistoryLevel
     );

socialUserGroup = socialManager.CreateSocialUserGroupFromList(
     xboxLiveContext.User,
     userList //this is a IReadOnlyList<string> (list of xbox user ids a.k.a. xuids)
    );

while(true)
{
     // some update loop in the game
     socialManager.DoWork();
     // // TODO: render the friends list using game UI, passing in socialUserGroup.Users
}
```

#### Events Returned

`local_user_added`(C++) | `LocalUserAdded`(C#) - Triggers when loading of users social graph is complete. Will indicate if any errors occurred during initialization

`social_user_group_loaded`(C++) | `SocialUserGroupLoaded`(C#)- Triggers when social user group has been created

`users_added_to_social_graph`(C++) | `UsersAddedToSocialGraph`(C#)- Triggers when users are loaded in

### Updating Social User Group From List

You can also change the list of tracked users in the social user group by calling update_social_user_group()

#### Source example using the C++ API

```cpp
//#include "Social.h"

socialManager->update_social_user_group(
    xboxSocialUserGroup,
    newUserList    // std::vector<string_t> (list of xuids)
    );

    while(true)
    {
    // some update loop in the game
    socialManager->do_work();
    // TODO: render the friends list using game UI, passing in socialUserGroup->users()
    }
```

#### Source example using the C# API

```csharp
// using Microsoft.Xbox.Services.Social.Manager;

socialManager.UpdateSocialUserGroup(
     xboxSocialUserGroup,
     newUserList //IReadOnlyList<string> (list of xbox user ids a.k.a. xuids)
     );

while(true)
{
     // some update loop in the game
     socialManager.DoWork();
     // // TODO: render the friends list using game UI, passing in socialUserGroup.Users
}
```

#### Events Returned

`social_user_group_updated`(C++) | `SocialUserGroupUpdated`(C#) - Triggers when social user group updating is complete.

`users_added_to_social_graph` | `UsersAddedToSocialGraph`(C#) - Triggers when users are loaded in. If users added via list are already in graph, this event will not trigger

### Using Social Manager events

Social Manager will also tell you what happened in the form of events.  You can use those events to update your UI or perform other logic.

#### Source example using the C++ API

```cpp
//#include "Social.h"

auto socialManager = social_manager::get_singleton_instance();
socialManger->add_local_user(
    xboxLiveContext->user(),
    social_manager_extra_detail_level::no_extra_detail
    );

auto socialUserGroup = socialManager->create_social_user_group_from_filters(
    xboxLiveContext->user(),
    presence_filter::all,
    relationship_filter::friends
    );

socialManager->do_work();
// TODO: initialize the game UI containing the friends list using game UI, socialUserGroup->users()

while(true)
{
    // some update loop in the game
    auto events = socialManager->do_work();
    for(auto evt : events)
    {
        auto affectedUsersFromGraph = socialUserGroup->get_users_from_xbox_user_ids(evt.users_affected());
        // TODO: render the changes to the friends list using game UI, passing in affectedUsersFromGraph
    }
}
```

##### Source example using the C# API

```csharp
// using Microsoft.Xbox.Services;
// using Microsoft.Xbox.Services.System;
// using Microsoft.Xbox.Services.Social.Manager;

socialManager = SocialManager.SingletonInstance;

socialManager.AddLocalUser(
     xboxLiveContext.User,
     SocialManagerExtraDetailLevel.PreferredColorLevel | SocialManagerExtraDetailLevel.TitleHistoryLevel
     );

socialUserGroup = socialManager.CreateSocialUserGroupFromFilters(
     xboxLiveContext.User,
     PresenceFilter.All,
     RelationshipFilter.Friends
     );

socialManager.DoWork();
// TODO: initialize the game UI containing the friends list using game UI, socialUserGroup->users()

while(true)
{
    // some update loop in the game
    IReadOnlyList<SocialEvent> Events = socialManager.DoWork();
    IReadOnlyList<XboxSocialUser> affectedUsersFromGraph;
    foreach(SocialEvent managerEvent in Events)
    {
        affectedUsersFromGraph = socialUserGroup.GetUsersFromXboxUserIds(managerEvent.UsersAffected);
    }
}

```

#### Events Returned

`local_user_added`(C++) | `LocalUserAdded`(C#) - Triggers when loading of users social graph is complete. Will indicate if any errors occurred during initialization

`social_user_group_loaded`(C++) | `SocialUserGroupLoaded`(C#)- Triggers when social user group has been created

`users_added_to_social_graph`(C++) | `UsersAddedToSocialGraph`(C#)- Triggers when users are loaded in

#### Additional details

This example shows some of the additional control offered.  Rather than relying on the social user group filters to provide a fresh user list during the game loop, the social graph is initialized outside the game loop.  Then the title relies upon the `events` returned by the `socialManager->do_work()` function.  `events` is a list of `social_event`, and each `social_event` contains a change to the social graph that occurred during the last frame.  For example `profiles_changed`, `users_added`, etc.  More information can be found in the `social_event` API documentation.

### Cleanup

#### Cleaning Up Social User Groups

```cpp
//#include "Social.h"

socialManger->destroy_social_user_group(
    socialUserGroup
    );
```

```csharp
// using Microsoft.Xbox.Services.Social.Manager;

socialManager.DestroySocialUserGroup(
     socialUserGroup
     );
```

Cleans up the social user group that was created. Caller should also remove any references they have to any created social user group as it now contains stale data.

#### Cleaning Up Local Users

```cpp
//#include "Social.h"

socialManger->remove_local_user(
    xboxLiveContext->user()
    );
```

```csharp
// using Microsoft.Xbox.Services.Social.Manager;

socialManager.RemoveLocalUser(
     xboxLiveContext.User
     );
```

Remove local user removes the loaded users social graph, as well as any social user groups that were created using that user.

#### Events Returned

`local_user_removed`(C++) | `LocalUserRemoved`(C#) - Triggers when a local user has been removed successfully