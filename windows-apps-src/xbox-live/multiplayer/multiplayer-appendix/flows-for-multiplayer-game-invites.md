---
title: Updated Flows For Multiplayer Game Invites
author: KevinAsgari
description: Provides updated flows for implementing Xbox Live multiplayer game invites.
ms.assetid: 1569588e-3bbc-47d3-8b7d-cc9774071adc
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, multiplayer 2015
ms.localizationpriority: low
---

# Updated Flows For Multiplayer Game Invites

As a result of Xbox One beta feedback, the user experience flow for multiplayer game invites has been changed as of Xbox One Recovery Update 24, released on November 6, 2013. This is a change to the **user-experience (UX) only** and will not affect any behavior or functionality from the perspective of a game title. Title developers will not need to make any code changes.

## Summary of changes

-   The initial invitation toast has changed from “join my party” to “&lt;game title&gt; Let’s Play” and now has a button that allows users to launch the game and jump right into gameplay.

-   The Party app is not snapped by default when the user chooses the new “&lt;game title&gt; Let’s Play” option. This change is also made so that the user can jump right into gameplay.

-   A new toast has been added on the sender’s side that says “Adding \[*number*\] friends to the game”. This makes it clear that invites were sent out when a game session is associated with a user’s party.

The detailed user experience flows are described in the following examples. Each table shows an example flow for two users, David and Laura. These flows are shown in each column and occur in parallel. The <b style="background-color: #FFFF00">highlighted text</b> shows the adjustments that have been made from the prior UX flows.

## Inviting users from within a game

<table>
  <tr>
    <td style="border-bottom:solid 1px #fff">
    David is in the multiplayer lobby for a game he is playing.<p><br>David chooses <b>Invite a Friend</b>.<p><br>David selects Laura.<p><br>Toast pops up indicating that an invite was sent. | Laura is playing a different game.
    </td>
    <td style="border-bottom:solid 1px #fff">
    Laura is playing a different game.
    </td>
  </tr>
  <tr>
    <td></td>
    <td>
    Toast pops up indicating an invitation from David, and <b style="background-color: #FFFF00">displays the game name and icon</b>. (The Party app does not auto-snap.)  
    <p><br>
    In the Notification Center, <b style="background-color: #FFFF00">Laura can choose Launch and accept invite</b>, <b>Accept invite</b>, or <b style="background-color: #FFFF00">Decline Invite</b>.
    </td>
  </tr>
  <tr>
    <td colspan="2" style="text-align:center"><b style="background-color: #FFFF00">Case 1: Laura selects Launch and accept invite</b> (This is a new option)</td>
  </tr>
  <tr>
    <td>
    Toast pops up indicating that Laura has joined David’s party.             
    <p><br>
    David starts the game from multiplayer lobby.                              
    <p><br>
    <b style="background-color: #FFFF00">Toast pops up indicating that a game invite was sent to Laura.</b>
    </td>
    <td>
    The game launches and the Party app does not snap.
    </td>
  </tr>
  <tr>
    <td colspan="2" style="text-align:center"><b>Case 2: Laura selects Accept invite</b></td>
  </tr>
  <tr>
    <td style="border-bottom:solid 1px #fff"></td>
    <td style="border-bottom:solid 1px #fff">Laura joins the party.</td>
  </tr>
  <tr>
    <td style="border-bottom:solid 1px #fff"><b style="background-color: #FFFF00">Toast pops up indicating that a game invite was sent to Laura.</b></td>
    <td style="border-bottom:solid 1px #fff"></td>
  </tr>
  <tr>
    <td></td>
    <td>Toast pops up indicating that a game was found for the party.
    <p><br>
    In the Notification Center, Laura can choose:
    <ul>
    <li>   <b>Accept game invite:</b> Game launches.
    <li>   <b>Decline game invite:</b> No game launches. Laura is still in the party and will receive subsequent game invites.         
    <li>   <b style="background-color: #FFFF00">Leave party: No game launches. Laura is removed from the party.</b>
    </ul>
    </td>
  </tr>
</table>

## In-game invite flow: In a party, and switching titles

<table>
  <tr>
    <td>
    Playing a game together.
    <p><br>
     David switches to a different game and goes to the multiplayer menu.
     <p><br>
     The Xbox System UI asks David if he would like to switch his Party to the new game, to which he can answer <b>Yes</b> or <b>No</b>.
    </td>
    <td style="text-align:top">
    Playing a game together.
    </td>
  </tr>
  <tr>
    <td colspan=2 style="text-align:center">
      <b>Case 1: Yes</b>
    </td>
  </tr>
  <tr>
    <td style="border-bottom:solid 1px #fff">
    Party comes to the new title.
    <p><br>
    From the multiplayer lobby, David starts the game.
    <p><br>
    <b style="background-color: #FFFF00">Toast pops up indicating that a game invite has been sent to Laura.
    </td>
    <td style="border-bottom:solid 1px #fff">
    </td>
  </tr>
  <tr>
    <td></td>
    <td>Toast pops up indicating that a game was found for the party.
    <p><br>
     From the Notification Center, Laura can choose:
     <ul>
     <li><b>Accept game invite</b>: New game launches
     <li><b>Decline game invite:</b> No game launches, but Laura is still in the party and will receive subsequent game invites.
     <li><b style="background-color: #FFFF00"><b>Leave party:</b> No game launches and Laura is removed from the party.</b>
     </ul>
     </td>
  </tr>
  <tr>
    <td colspan=2 style="text-align:center">
      <b>Case 2: No</b>
    </td>
  </tr>
  <tr>
    <td>
      Party does not come to the new title.
      David plays on multiplayer mode without his party members.
      David is still in the party.
    </td>
    <td>
    </td>
  </tr>
</table>

## Invite flow from Home

<table>
  <tr>
    <td>
    David is browsing <b>Home</b>, and in his <b>Friends</b> list, he sees that Laura is online.
    <p><br>
    David chooses to invite Laura to his party. Toast pops up indicating that the invite is sent. The Party app is snapped for David.
    </td>
    <td>
    Laura is playing a game.
    </td>
  </tr>
  <tr>
    <td></td>
    <td>
    Toast pops up indicating that David has invited Laura to his party.
    <p><br>
    In the Notification Center, Laura has the option to <b>Accept the invite</b>.
    <p><br>
    When Laura accepts, the Party app snaps.                                                                         </td>
  </tr>
  <tr>
    <td>
    Toast pops up indicating that Laura has joined the party.
    <p><br>
    David and Laura discuss what game they want to play. David launches the game and enters the multiplayer game mode.
    <p><br>
    Game will either give an option to invite friends, or auto-pull the party members.
    <p><br>
    <b style="background-color: #FFFF00">Toast pops up indicating that a game invite has been sent.</b>
    </td>
    <td>
    Toast pops up indicating that a game has been found for the party.
    <p><br>
    In the Notification Center, Laura can:
    <ul>
    <li>   <b>Accept game invite:</b> Game launches
    <li>   <b>Decline game invite:</b> No game launches, Laura is still in the party and will receive subsequent invites.
    <li>   <b style="background-color: #FFFF00">Leave party: No game launches, Laura is removed from the party.</b>
    </ul>  
    </td>
  </tr>
</table>


## More about the Game Invite Sent toast

The **Game Invite Sent** toast will only appear the first time a game session is established with remote party members. Subsequent requests sent to remote party members will not generate this toast. This prevents the user from being spammed with repeated **Game Invite Sent** toasts if the title makes multiple calls to **PullReservedPlayersAsync**.

**Note** The best practice is to add all desired friends as Reserved, and then call **PullReservedPlayersAsync** only once.
