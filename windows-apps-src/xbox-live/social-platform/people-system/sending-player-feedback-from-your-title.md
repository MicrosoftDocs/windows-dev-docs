---
title: Sending Player Feedback From Your Title
author: KevinAsgari
description: Learn how your title can help promote positive player experiences by sending player feedback to the Xbox Live reputation service.
ms.assetid: 49f8eb44-1e31-4248-b645-9123df6f8689
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, reputation, player feedback
ms.localizationpriority: low
---

# Sending player feedback from your title
The majority of Xbox Live members are awesome, but there are a small percentage of "Bad Apples" who hurt other people's game experiences. We identify these small percentages of users through user and title submitted feedback. We help protect the rest of the population by ensuring that these "Bad Apples" have a limited multiplayer experience where they can't interfere with good players' games. Xbox relies heavily on users to report other users to keep the system accurate, but titles in Xbox One can directly participate and dramatically help improve the accuracy of the user reputation population.

## Steps to Submit Feedback from Title or Title Service
1. Add feedback moments to title or title service
2. Determine the correct feedback type
3. Call Reputation Feedback APIs with Feedback

### Adding Feedback Moments to Title or Title Service
All gamers have had bad experiences with teammates who sabotage their own side, players who just stand around instead of actively playing, or cheaters who ruin their games. Xbox Live allows users to report these problematic players directly, but user feedback isn't perfect. Titles can easily determine simple things like players idling in game and quitting early and can sometimes even determine when someone is cheating. Your title can submit feedback in a wide variety of feedback moments, which will help improve the experience of all good players.

### Determining the Correct Feedback Type
The Reputation system has many feedback types that are intended to capture the various ways that a user may warrant feedback. They are fully listed in Table 1 below. Most of them are negative, but it is possible to improve a user's reputation with positive feedback as well.

Xbox system UI provides a way for players to submit feedback on other users in-game. That user-to-user feedback does not carry much weight, since users are prone to griefing one another when they lose. Titles can supplement that system UI, providing UI for users to directly submit feedback on another, but instead, we prefer that titles submit feedback on behalf of the title itself by using Partner feedback. Partner feedback is highly trusted.

## Example Partner Feedback Usage Scenarios

### User quitting a game in the middle of a match
A player is losing a game and uses the game's menu to quit the game, abandoning her teammates. When a title detects this behavior they can report the behavior using **FairPlayQuitter.**

### Idling after match found in game
A player gets matched with other players to play, but consistently stands idle in the game instead of helping the team. The title can report the player behavior using **FairplayIdler.**

### Killing Teammates in Game
A player in a game is constantly killing teammates for fun. When a title detects that a user is consistently team-killing it can report the player using **FairPlayKillsTeammates.**

### Title Has Community Kick/Vote Feature
A player has been voted by players in the round to be removed from the session for bad behavior. If the title removes that player from the session it can report the user with **FairPlayKicked.**

### Helpful Player Community Vote
After a few rounds of a game, the title offers an option to pick a person that has helped the most. When a title sees this action it can report the behavior using **PositiveHelpfulPlayer.**

### High Quality UGC (user generated content)
A title has a scene in which a player can pick the best design for a vehicle. When a title sees this action it can report the behavior using **PositiveHighQualityUGC.**

### Skilled Player
After a few rounds of a game, the title offers an option to pick a person that is the MVP who was the best player. When a title sees that a player consistenly earns MVP status it can report the behavior using **PositiveSkilledPlayer.**

### User Reports Questionable UGC in Title
A title has a scene in which a player can view vehicle designs. A player notices an offensive design and wants to report it. When a title sees this action it can report the offender using **UserContentInappropriateUGC.**

### Title Wants to Request an XBL Ban Review of a Player in Their Title
A title's community manager has noticed a low-reputation player that is consistently causing trouble in their game. A title can request an XBL Policy and Enforcement team review using **FairPlayUserBanRequest.**

## Complete Behavior Feedback Options
The table below lists the feedback types you can use to submit user feedback on behalf of your title. The Reputation service is flexible and can easily add new feedback types if you believe these do not meet your title's needs. Please tell your account manager if you'd like to see a new feedback type added.

Table 1: The various Partner feedback types the Reputation service supports.

**Fairplay Feedback Types**               | **Description**
----------------------------------------- | -------------------------------------------------------------------------------------------------------------------------
FairplayKillsTeammates                    | Report a player who is intentionally killing their own teammates
FairplayCheater                           | Report a player who is certainly cheating
FairplayTampering                         | Report a player who has certainly meddled with the game disk or has otherwise tampered with the game software or hardware
FairplayUserBanRequest                    | Report a player who you think has earned a suspension
FairplayConsoleBanRequest                 | Report a console that you think should be banned from connecting to Xbox Live
FairplayUnsporting                        | Report a player who is certainly engaging in unsportsmanlike conduct
FairplayIdler                             | Report a player who enters multiplayer matches but does not actively play
FairplayLeaderboardCheater                | Report a player who has certainly cheated to appear high on a leaderboard
**Communications Feedback Types**         |
CommsInappropriateVideo                   | Report a player who is being inappropriate in video chat
**User Generated Content Feedback Types** |
UserContentInappropriateUGC               | Report an inappropriate piece of content that a player creates in your title
UserContentReviewRequest                  | Report a piece of content proactively so that the XBLPET team will review it
UserContentReviewRequestBroadcast         | Report a broadcast proactively so that the XBLPET team will review it
UserContentReviewRequestGameDVR           | Report a GameDVR clip proactively so that the XBLPET team will review it
UserContentReviewRequestScreenshot        | Report a screenshot proactively so that the XBLPET team will review it
**Positive Feedback**                     |
PositiveSkilledPlayer                     | If users can vote to determine an MVP, report a skilled player when certain that the player deserves positive feedback
PositiveHelpfulPlayer                     | If a game provides UI for a player to report that another one was helpful, report the helpful player
PositiveHighQualityUGC                    | If a game provides UI for a player to compliment another user’s content, report the content positively

## Feedback API Calls
Titles can use two strategies to call the Reputation service. The preferred method is to report users in aggregate from a partner service by using a service token for authentication. Titles can also report users directly from the client. The client API has anti-fraud technology built in which requires multiple reports on a user to be considered valid. Both APIs are batch and can report multiple users simultaneously.

The title can use the following Xbox Live APIs to submit player reputation feedback:

Language | API
-------- | --------------------------------------------------------------------------------------------
WinRT    | Microsoft::Xbox::Services::Social::ReputationService.SubmitBatchReputationFeedbackAsync(...)
C++      | xbox::services::social::reputation_service.submit_batch_reputation_feedback(...)

Alternatively, the title can use the following direct REST methods:

API          | URL                                                      | Auth Requirements
------------ | -------------------------------------------------------- | ---------------------------------------
Service POST | https://reputation.xboxlive.com/users/batchfeedback      | S-token with partner and sandbox claims
Client POST  | https://reputation.xboxlive.com/users/batchtitlefeedback | Xtoken with title and sandbox claims

## Feedback Object
The Feedback object has the following specification for the latest version, 101. Both APIs expect a batch of the following objects.

Member       | Type   | Description
------------ | ------ | -----------------------------------------------------------------------------------------------------------------------------------------------------------------
sessionRef   | object | An object describing the MPSD session this feedback relates to, or null.
feedbackType | string | The type of feedback. Possible values are defined in the ReputationFeedbackType Enumeration
textReason   | string | User-supplied text that the sender added to explain the reason the feedback was submitted. This is very valuable for our policy enforcement team.
evidenceId   | string | The ID of a resource that can be used as evidence of the feedback being submitted, for example, a video file recorded during game play, or an Activity Feed item.
titleID      | String | The title ID of the played title; only required if not present in token.
targetXuid   | String | The XUID of the player you are reporting.

Example:

```json
POST https://reputation.xboxlive.com/users/batchtitlefeedback
{
    "items" :
    [
        {
            "targetXuid": "33445566778899",
            "titleId" : null,
            "sessionRef": {
  "scid": "372D829B-FA8E-471F-B696-07B61F09EC20",
  "templateName": "CaptureFlag5",
  "name": "Title56932",
           },
            "feedbackType": "FairPlayKillsTeammates",
            "textReason": "Title detected this player killing team members 19 times",
            "evidenceId": null
        }
    ]
}
```

## Feedback Q&A

### Q: Can I send a hint to the system to help with humans that might be looking at the player report?
A: Yes, and it is very helpful! Use the "textReason" parameter to help the Enforcer who will ultimately look at the feedback submitted. For example, for an idler, you can add a text reason that says "We received no user input from this player after the first five seconds of the game". This text reason can be very valuable for the XBLPET enforcement agents, so ensure that the text reason is helpful and descriptive.

### Q: Should I worry about how often I send in feedback on a user?
A: Titles should call the Reputation service when they are confident that a user has earned feedback. The system has several safety catches to prevent titles and users from being able to over-impact users.

### Q: Can I adjust the weight of the feedback being sent?
A: No, the Reputation service determines the weight of the feedback. We are always adjusting the weights to make the system better.

### Q: Can I undo feedback I’ve sent on a user?
A: No, feedback is final. If you believe your title has a bug and is sending erroneous feedback, let us know and we'll blacklist your title until you fix the bug.

### Q: Can I see the feedback sent for my title from users?
A: Currently that info is not available self-serve. You can ask your account manager and we do have data per title we can share. Soon we'd like to make this directly available to you, and also show mix of users with low reputation using your title.

### Q: When I send in console or user ban review request how do I know what happened?
A: Currently the information for the review is sent to XBL Policy and Enforcement team, but currently you do not get updated on the ban review.

### Q: Is there a reputation score per title?
A: No. Currently there are sub scores for reputation for fairplay, communications, and user-generated content, but not per title. We may add this feature in the future if there is enough demand, so let your account manager know if you'd like to request that feature.
