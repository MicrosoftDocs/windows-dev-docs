---
title: Multiplayer roles
author: KevinAsgari
description: Learn about using roles to define player roles in Xbox Live multiplayer.
ms.author: kevinasg
ms.date: 06/29/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, multiplayer, roles
ms.localizationpriority: low
---

# Roles

For some game sessions, you may want to specify that certain members have certain gameplay roles, such as support, medic, assault, etc. You may also wat to reserve game slots for players that will fill a specific gameplay role. By using the Xbox Live roles feature, the service can track which players are assigned which gameplay roles, and enforce a maximum number of players that can select a specific gameplay role.

The most common use of roles is to determine game specific roles for that game session. For example, you can have a game mode that requires between 1 and 2 support classes, at least 1 tank/heavy class, and no more than 5 assault classes.

In another possible scenario, you may want to specify that a game session can have exactly 4 game players, up to 8 spectators, and 1 announcer.

You can also use roles to reserve slots for friends while filling the remaining slots through other means, such as session browse.

## Role types

A role type represents a group of role definitions. Every role must be defined as part of a role type. Role types are defined in the multiplayer session document.

A member can only be assigned one role from a given role type. For example, if a "class" role type includes healers, tanks, and damage, then a member can only be assigned to one of those roles.

You can define multiple role types, and a member can be assigned one role from each role type. In the previous scenario, a member may have chosen the healer role, but may also be assigned a squad leader role, if the squad leader role is defined in a separate role type.

> **Important:** The Xbox Live SDK currently only supports a single role type and a single role per member.

## Role type properties

When you define a role type, you must specify the following information for role types:

* The name of the role type. The name must be lowercase and alpha-numeric, and no more than 100 characters long.
* If the roles defined in the role type are owner managed or not.
* If the properties of the roles defined in the role type can be changed during the life of the session.
* The role definitions that are include in the role type.

If a role type is owner managed, that means that only members that are owners of the session can assign roles of that type to members. If the role type is not owner managed, then members can assign roles to themselves.

You can only specify that a role type is owner managed on sessions that have the "hasOwners" capability set.

> The Xbox Live SDK does not currently support owners assigning roles to other members.

## Role properties

When you define a role, you must specify the following information for each role:

* The name of the role. The name must be lowercase and alpha-numeric, and no more than 100 characters long.
* The maximum number of members that are allowed to fill the role. Must be greater than zero.
* The target number of members that should fill the role. The target must be greater than zero, and less than or equal to the maximum number of members allowed to fill the role.

When a session member is assigned a role, that information is recorded in the member roles in the multiplayer session document.

The service enforces the maximum number of members that can be assigned to a role, but does not enforce the target number.

## Create roles

Roles and role types are typically defined in the [session template](service-configuration/session-templates.md). The service supports role and role type definition during session creation, however the Xbox Live SDK does not.

### Define role types and roles in a session template

You can define role types and roles when you create a session template during the Xbox Live configuration.

The role type and role information is specified as a base level "roleTypes" element in the session template, in the following format:

```json
"roleTypes": {
  "myroletype1": { // must be lowercase alpha-numeric.
    "ownerManaged": true, // Can only be true on sessions with the "hasOwners" capability set. If true, only the owner of the session can assign this role to members.
    "mutableRoleSettings": ["max", "target"], // Which role settings for roles in this role type can be modified throughout the life of the session. Exclude role settings to lock them.
    "roles": {
      "role1": { // must be lowercase alpha-numeric.
        "max": 3, // Max number of members assigned to this role at a time, enforced by MPSD.
        "target": 2 // Target number of members to assign this role to. Like max, but not enforced (can be exceeded).
      },
      "role2": {
        ...
      }
  },
  "myroletype2": {
    ...
  }
},
```

## Retrieve role information for a multiplayer session

You can get the information about the role types, roles, and how many members are assigned to each role from either a multiplayer session, or from a multiplayer search handle.

In the Xbox Live SDK, information about the role types and roles is stored inside a map structure. The C++ APIs use the `unordered_map` class and the WinRT APIs use the `IMapView` class.

### Get the role information from a search handle

In the `multiplayer_search_handle_details` object returned from a search request, you can get the role type information by indexing the `role_types` map with the name of the role type you're interested in.

This returns a `multiplayer_role_type` object. You can get the roles by indexing the `roles` map, which returns a `multiplayer_role_info` object.

The `multiplayer_role_info` object contains information about the role, including `max_members_count`, `member_xbox_user_ids`, `members_count`, and `target_count`.

### Get the role information from a search handle

The flow for getting role information from a session is similar to the flow for getting information from a search handle, but some different classes are used.

In the `multiplayer_session` object, you can get the role type information by referencing the `session_role_types` object, which is a `multiplayer_session_role_types` class. In this object, you can index the `role_types` map with the name of the role type you're interested in.

This returns a `multiplayer_role_type` object. You can get the roles by indexing the `roles` map, which returns a `multiplayer_role_info` object.

The `multiplayer_role_info` object contains information about the role, including `max_members_count`, `member_xbox_user_ids`, `members_count`, and `target_count`.

## Change mutable role settings

If a role type indicates that some role settings can be changed (mutable), you can use the `multiplayer_role_type.set_roles()` method to modify the mutable settings. Only members that are marked as session owners can change role settings.

## Assign a role to a member

Currently, only a member can assign their own role in the Xbox Live SDK. In the `multiplayer_session` object, you can call the `set_current_user_role_info(role_type, role_name)` method to specify the role type and role for the current member.

If the role is already full when you try to write the session to the service, MPSD will reject the write.
