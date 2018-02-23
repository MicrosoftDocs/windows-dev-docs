---
title: Multiplayer session templates
author: KevinAsgari
description: Learn about Xbox Live multiplayer session templates.
ms.assetid: 178c9863-0fce-4e6a-9147-a928110b53a2
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, multiplayer, session template
ms.localizationpriority: low
---

# Multiplayer session templates

This topic gives a brief overview of multiplayer session templates and provides several examples of templates that you can copy and modify for your multiplayer sessions.

A multiplayer session template is a blueprint that is used to create a multiplayer session. All sessions must be created based on a predefined template. A template defines constants that will be the same for any session that is created from the template. Once a game creates a session from a template, the game can add and modify additional data to the session, but cannot modify the constants that were defined in the template.

 For more information, see [Session Overview](../multiplayer-appendix/mpsd-session-details.md).

The list of session templates that apply to a service configuration identifier (SCID), as well as the contents of specific session templates, can be retrieved from Multiplayer Session Directory (MPSD).


## About session templates

A session template uses the same format as an HTTP PUT request to create or modify a session. The difference is that the template is limited to constants (no members, servers, or properties). The constants can be any session constants, including a custom section and the full range of system constants.

### Session template versions

The session templates defined in this topic are constructed using template contract version 107. When using them to create a new template, make sure that 107 is entered as the contract version.

If you use XSAPI and watch the resulting requests in the debugger, you might notice that the requests use template contract version 105. MPSD effectively "upgrades" these requests to version 107 at run time.

> **Note:** It is permissible to use a different contract version in the request from what is used in the session template.

If necessary, you can change a session template from version 104/105 to version 107. For instructions, see migrating instructions in [Common Issues When Adapting Your Titles for 2015 Multiplayer](../multiplayer-appendix/common-issues-when-adapting-multiplayer.md).


## Session template default values

Each session created from a session template starts as a copy of the template. Values that the template does not include can be provided at session creation. Default values are provided in some cases when no other value is set. For example, the default set of timeouts for contract version 107 is:

```json
    {
      "constants": {
        "system": {
          "reservedRemovalTimeout": 30000,
          "inactiveRemovalTimeout": 0,
          "readyRemovalTimeout": 180000,
          "sessionEmptyTimeout": 0
        }
      }
    }
```
You can force a value to remain unset by specifying null. This overrides any default setting and prevents the value from being set at session creation. For example, to remove the session empty timeout, allowing sessions to continue indefinitely, even without any members, add the following to the session template:
```json
    {
      "constants": {
        "system": {
          "sessionEmptyTimeout": null
        }
      }
    }
```
> **Important:** Constants that are set through a template cannot be changed through writes to MPSD. To change the values, you must create and submit a new template with the required changes.


## Example session templates
This section shows several examples of session templates for different purposes and networking topologies. Please examine each template before choosing the one most appropriate for your client. You can then copy and paste the template into your service configuration, potentially after making required changes.

### Standard lobby session
You can use the following template as a starter template to create a lobby session for your game:

* Change the `maxMembersCount` value to the maximum number of players that you want to support in your lobby session.  
* If your title does not support players from different platforms (such as an Xbox One and a PC) playing together, you can remove the `crossPlay` element.  
* You can change the other values as well, but the following values are good values to start with if you're not sure what you need.


```json
{
   "constants": {
        "system": {
            "version": 1,
            "maxMembersCount": 8,
            "visibility": "open",
            "capabilities": {
                "connectivity": true,
                "connectionRequiredForActiveMembers": true,
                "crossPlay": true,
                "userAuthorizationStyle": true
            },
        },
        "custom": {}
    }
}
```

### Standard game session without matchmaking
You can use the following template as a starter template to create a game session for your game, if your game does not include anonymous matchmaking and does not require more than 100 members.

Note that the only new values specified from the standard lobby session template are the following:
* `constants.system.inviteProtocol : "game"`
* `constants.system.capabilities.gameplay : true`

```json
{
   "constants": {
        "system": {
            "version": 1,
            "maxMembersCount": 8,
            "visibility": "open",
            "inviteProtocol": "game",
            "capabilities": {
                "connectivity": true,
                "connectionRequiredForActiveMembers": true,
                "gameplay" : true,
                "crossPlay": true,
                "userAuthorizationStyle": true
            }
        },
        "custom": {}
    }
}
```

### Add matchmaking to a game session template, while letting the multiplayer service handle quality of service checks.

You can add the following `memberInitialization` json element to your gameplay template in order to add support for matchmaking.

When you create your SmartMatch hopper, use this template as the target session template for your hopper.

```json
{
   "constants": {
        "system": {
            "memberInitialization": {
               "joinTimeout": 20000,
               "measurementTimeout": 15000,
               "membersNeededToStart": 2
            }
        }
    }
}
```

### Add matchmaking to a game session template, where quality of service checks are handled by a title managed data center.



```json
{
   "constants": {
        "system": {
            "peerToHostRequirements": {  
                "latencyMaximum": 250,
                "bandwidthDownMinimum": 256,
                "bandwidthUpMinimum": 256,
                "hostSelectionMetric": "latency"
            },
            "memberInitialization": {
               "joinTimeout": 15000,
               "measurementTimeout": 15000,
               "membersNeededToStart": 2
            }
        },
        "custom": {}
    }
}
```

### Basic session template for client-server game session

You can use this template for a title that does not perform peer-to-peer communication and does not use Xbox Live Compute, but instead has clients connect to a third-party hosted server.
```json
    {
      "constants": {
        "system": {
          "version": 1,
          "maxMembersCount": 12,
          "visibility": "open",
          "inviteProtocol": "game",
          "capabilities": {
            "connectionRequiredForActiveMembers": true,
            "gameplay" : true,
          },
        },
        "custom": {}
      }
    }
```

### Lobby/SmartMatch ticket session template for peer-based networking

Use this template for creating a lobby session or a SmartMatch ticket session that is only to be used to send a group of players into matchmaking. The template is not to be used to configure a game session. It is intended for use by clients using a peer-to-peer or peer-to-host network topology.
```json
    {
      "constants": {
        "system": {
          "version": 1,
          "maxMembersCount": 10,
          "visibility": "open",
          "capabilities": {
            "connectionRequiredForActiveMembers": true,
          },
          "memberInitialization": {
            "membersNeededToStart": 1
          },
        },
        "custom": {}
      }
    }
```

### Quality of Service (QoS) templates

If your client is using matchmaking and evaluating QoS, you must add some constants to the session template to inform MPSD to coordinate with the client to manage users joining the session. This coordination validates the quality of the connection state prior to informing users that the game is ready to start. In the case of client-server games, the coordination validates connection quality before a group of players enters matchmaking.

#### Peer-to-host game session template with QoS

The following example presents a peer-to-host game session template with QoS.
```json
    {
      "constants": {
        "system": {
          "version": 1,
          "maxMembersCount": 12,
          "visibility": "open",
          "inviteProtocol": "game",
          "capabilities": {
            "connectivity": true,
            "connectionRequiredForActiveMembers": true,
            "gameplay" : true
          },
          "memberInitialization": {
            "membersNeededToStart": 2
          },
          "peerToHostRequirements": {
            "latencyMaximum": 350,
            "bandwidthDownMinimum": 1000,
            "bandwidthUpMinimum": 1000,
            "hostSelectionMetric": "latency"
          }
        },
        "custom": { }
      }
    }
```

#### Peer-to-peer game session template with QoS

The following is an example of a peer-to-peer game session template with QoS.
```json
    {
    "constants": {
      "system": {
        "version": 1,
        "maxMembersCount": 12,
        "visibility": "open",
        "inviteProtocol": "game",
        "capabilities": {
          "connectivity": true,
          "connectionRequiredForActiveMembers": true,
          "gameplay" : true
        },
        "memberInitialization": {
          "membersNeededToStart": 2
        },
        "peerToPeerRequirements": {
          "latencyMaximum": 250,
          "bandwidthMinimum": 10000
        }
      },
      "custom": { }
     }
    }
```

#### Client-server (Xbox Live Compute) lobby/matchmaking session template with QoS

Use this template to create a lobby session or a matchmaking session using QoS. This template is not intended to be used to configure a game session.
```json
    {
      "constants": {
        "system": {
          "version": 1,
          "maxMembersCount": 12,
          "visibility": "open",
          "memberInitialization": {
            "membersNeededToStart": 1
          }
        },
        "custom": {}
      }
    }
```

#### Session template for crossplay between Xbox One and Windows 10

Use this template to enable crossplay multiplayer between Xbox One and Windows 10. The userAuthorizationStyle capability enables access to Windows 10. The optional crossPlay capability signifies that your title supports interactions such as invites and join-in-progress between platforms.
```json
    {
      "constants": {
        "system": {
          "capabilities": {
            "crossPlay": true,
            "userAuthorizationStyle": true
          },
        },
        "custom": {}
      }
    }
```
