---
title: Introduction to Social Manager
author: KevinAsgari
description: Learn about the Xbox Live social manager API to keep track of online friends.
ms.assetid: d4c6d5aa-e18c-4d59-91f8-63077116eda3
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one
localizationpriority: medium
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

For a look at the social manager memory and performance characteristics take a look at [Social Manager Memory and Performance Overview](social-manager-memory-and-performance-overview.md)  

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

* Note: On Windows there can only be one local user

**Xbox Social User**: An *Xbox social user* is a full set of social data associated with a user from a group

**Xbox Social User Group**: A group is a collection of users that is used for things like populating UI. There are two types of groups

* **Filter Groups**: A filter group takes a local (calling) users *social graph* and returns a consistently fresh set of users based on specified filter parameters
* **User Groups**: A user group takes a list of users and returns a consistently fresh view of those users. These users can be outside of a users friends list.

In order to keep *social groups* to date **do_work()** must be called every frame

## API Overview
You will most frequently use the following key classes:

### social manager
This is a singleton class that can be used to get `xbox_social_user_groups` which are the views described above.

The `social_manager` will keep `xbox_social_user_groups` up to date, and can filter the `xbox_social_user_groups` by presence or relationship to the user.  For example, a `xbox_social_user_groups` containing all of the user's friends who are online and playing the current title could be created.  This would be kept up to date as friends start or stop playing the title.

### xbox social user group
A group of users that meet certain criteria, as described above. Xbox social user groups expose what type of a group they are, which users are being tracked or what the filter set is on them, and the local user which the group belongs to.

You can find a complete API description in the `social_manager.h` header, in the `social_manager` class  in the C++ API documentation, or the `SocialManager` class in the WinRT documentation.

## Usage

### Creating a social user group from filters

In this scenario, you want a list of users from a filter, such as all the people this user is following or tagged as favorite.

**Source example using the C++ API**

```cpp
    auto socialManager = social_manager::get_singleton_instance();

    socialManger->add_local_user(
      xboxLiveContext,
      social_manager_extra_detail_level::preferred_color_level | social_manager_extra_detail_level::title_history_level
      );

    auto socialUserGroup = socialManager->create_social_user_group_from_filters(
      xboxLiveContext,
      presence_filter::all,
      relationship_filter::following
      );

    while(true)
    {
      // some update loop in the game
      socialManager->do_work();
      // TODO: render the friends list using game UI, passing in socialUserGroup->users()
    }
```

**Events Returned**

`local_user_added` - Triggers when loading of users social graph is complete. Will indicate if any errors occurred during initialization

`social_user_group_loaded`- Triggers when social user group has been created

`users_added_to_social_graph`- Triggers when users are loaded in

**Additional details**

The above example shows how to initialize the social manager for a user, create a social user group for that user, and keep it up to date.

The filtering options can be seen in the `presence_filter` and `relationship_filter` enums

In the game loop, the `do_work` function updates all created views with the latest snapshot of the users in that group.

The users in the view can be obtained by calling the `xbox_social_user_group::get_users()` function which returns a list of `xbox_social_user`.  The `xbox_social_user` contains the social information such as gamertag, gamerpic uri, etc.

### Create and update a social user group from list

In this scenario, you want the social information of a list of users such as users in a multiplayer session.

**Source example using the C++ API**

```cpp
    auto socialManager = social_manager::get_singleton_instance();

    socialManger->add_local_user(
      xboxLiveContext->user(),
      social_manager_extra_detail_level::preferred_color_level | social_manager_extra_detail_level::title_history_level
      );

    auto socialUserGroup = socialManager->create_social_user_group_from_list(
      xboxLiveContext->user(),
      userList	// this is a std::vector<string_t> (list of xuids)
      );

    while(true)
    {
      // some update loop in the game
      socialManager->do_work();
      // TODO: render the friends list using game UI, passing in socialUserGroup->users()
    }
```

**Events Returned**

`local_user_added` - Triggers when loading of users social graph is complete. Will indicate if any errors occurred during initialization

`social_user_group_loaded`- Triggers when social user group has been created

`users_added_to_social_graph`- Triggers when users are loaded in

**Updating Social User Group From List**

You can also change the list of tracked users in the social user group by calling update_social_user_group()

**Source example using the C++ API**

```cpp
    socialManager->update_social_user_group(
	   socialUserGroup,
	   newUserList	// std::vector<string_t> (list of xuids)
	   );

     while(true)
     {
       // some update loop in the game
       socialManager->do_work();
       // TODO: render the friends list using game UI, passing in socialUserGroup->users()
     }
```

**Events Returned**

`social_user_group_updated`- Triggers when social user group updating is complete.

`users_added_to_social_graph`- Triggers when users are loaded in. If users added via list are already in graph, this event will not trigger

### Using social manager events

Social manager will also tell you what happened in the form of events.  You can use those events to update your UI or perform other logic.

**Source example using the C++ API**

```cpp
    auto socialManager = social_manager::get_singleton_instance();
    socialManger->add_local_user(
        xboxLiveContext->user(),
        social_manager_extra_detail_level::no_extra_detail
        );

    auto socialUserGroup = socialManager->create_social_user_group_from_filters(
        xboxLiveContext->user(),
        presence_filter::all,
        relationship_filter::following
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

**Events Returned**

`local_user_added` - Triggers when loading of users social graph is complete. Will indicate if any errors occurred during initialization

`social_user_group_loaded`- Triggers when social user group has been created

`users_added_to_social_graph`- Triggers when users are loaded in

**Additional details**

This example shows some of the additional control offered.  Rather than relying on the social user group filters to provide a fresh user list during the game loop, the social graph is initialized outside the game loop.  Then the title relies upon the `events` returned by the `socialManager->do_work()` function.  `events` is a list of `social_event`, and each `social_event` contains a change to the social graph that occurred during the last frame.  For example `profiles_changed`, `users_added`, etc.  More information can be found in the `social_event` API documentation.

### Cleanup
**Cleaning Up Social User Groups**

```cpp
    socialManger->destroy_social_user_group(
   	   socialUserGroup
	   );
```

Cleans up the social user group that was created. Caller should also remove anything references they have to any create social user group as it now contains stale data.

**Cleaning Up Local Users**

```cpp
    socialManger->remove_local_user(
   	   xboxLiveContext->user()
	   );
```

Remove local user removes the loaded users social graph, as well as any social user groups that were created using that user.

**Events Returned**

`local_user_removed` - Triggers when a local user has been removed successfully
