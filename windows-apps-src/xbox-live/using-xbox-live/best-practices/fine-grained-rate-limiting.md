---
title: Xbox Live fine grained rate limiting
author: KevinAsgari
description: Learn how Xbox Live fine grained rate limiting works, and how to prevent your title from being rate limited.
ms.assetid: ceca4784-9fe3-47c2-94c3-eb582ddf47d6
ms.author: kevinasg
ms.date: 04/04/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: xbox live, xbox, games, uwp, windows 10, xbox one, throttling, rate limiting
ms.localizationpriority: low
---

# Xbox Live fine grained rate limiting

## Introduction

This article provides an overview of Xbox Live fine grained rate limiting. In addition to summarizing what rate limiting is, this paper also intends to help you determine if you are being limited, and if you are, what tools and resources are at your disposal.

## Terminology Guide

| Term         | Definition                                                  |
|--------------|-----------------------------------------------------------------------------
| FGRL         | Fine Grained Rate Limiting                                                  
| XSAPI        | Xbox Services Application Program Interface                                 
| CU           | Content Update                                                              
| Burst        | Represents a volume of requests received in a short period of time          
| Sustain      | Represents a high volume of calls received constantly over a period of time
| User + Title | Represents the pairing of a user and title as one entity                    
| XLTA         | Xbox Live Trace Analyzer tool, used for determining if your title is being rate limited

## Xbox Live Fine Grained Rate Limiting

**Fine Grained Rate Limiting** was commissioned to promote **fair usage** of shared Xbox Live resources amongst different titles. This solution is like most traditional limiting systems which have a service keeping a count of the number of requests an entity has made in a given period of time.

Entities that reach the services specified limit are then moved to a rejecting state where all incoming requests from the entity will be turned away. Entities are only able to exit this state when the given period of time expires causing the entities associated count to reset.

Fine Grained Rate Limiting uses the same core mechanics mentioned above however instead of tracking one entity FGRL tracks the combination of **user and title** and compares the associated count to **two** different **limits** as oppose to one. FGRL dual limits are enforced on each service meaning the request count for GameClips will not affect the request count for Presence. The following sections will go into more detail about the user and title pairing, dual limiting, and the HTTP 429 limiting response object.

## Fair Usage

Xbox Live believes that each user should have the same high quality experience no matter what game/app he/she is playing. FGRL is designed to solve the following scenario:

Developer A has just released a title that follows all the Xbox live best practices ensuring optimal use of services while Developer B has also just released a title however this one has an unknown bug. This bug causes the title and each user to spam presence which results in the service going under heavy load. The service slows and eventually halts breaking the experience for developer A’s users even though it was developer B’s bug that caused the issue. If FGRL was implemented the service would have been able to stop receiving requests from the miss behaving title allowing it to serve Developer A’s title its fair slice of the resource pie.

## Title and User granularity

Title and User were chosen as the key to ensure fair usage of Xbox Live resources. Tracking just the user would create a scenario in which the user experience would be at the mercy of each titles integration. For example, most titles use the people service already so for the sake of this example, let’s say Fine Grained Rate Limiting was set up on the people service allowing no more than 100 requests in 5 minutes. If a user were to play a game that made 100 requests in 1 minute the limit would be exceeded and the user would not be able to make any more requests to the people service; imagine that in the same period the user then goes back to the home screen and clicks his friends list: Since the user would already have exceeded the limit that friends list call would fail until the 5 minutes interval has passed, even though the home screen was not responsible for putting the user in the limited state.

Alternatively limiting based only on the title would produce an equally unfair result. Setting a limit per title would ignore the popularity of the titles and the requests would just be first come first serve until a limit is reached.

The pairing of User and Title ensures that no title uses more resources than what is appropriate given the number of active users while also giving each user a consistent slice of the resource pie.

![](../../images/FGRL.png)

The diagram above shows a high level view of how the request is handled. First the request is generated and then received by the desired service. Upon receiving the request, the system checks to see how many times the user and title together have accessed the service. If the request is under the limit, then it will be processed as normal. If the request is found to be at or above the limit the services will drop it and instead return a 429 response. The response will indicate how long until the period rolls over and the user and title requests can be handled.

## Burst and Sustain Limits

Traditionally, rate limiting consists of one limit per endpoint which is tracked over a given period of time. This period represents the amount of time that an entities request count is tracked. At the end of the period the entities count is reset to 0 to begin tracking again.

This approach works for most API’s however it was not resilient enough for games and apps that call Xbox live. The solution above assumes that people are calling in a consistent steady predictable manor. In the Xbox Live case, depending on the service and requesting title/app, the calling patterns are drastically different.

Choosing just one limit in this case would require compromising on both ends of the call pattern spectrum. The Xbox Live solution uses two periods and limits. The smaller period is called the Burst period while the bigger longer one is known as the Sustain period. The burst time period for FGRL is always 15 seconds whereas the sustain is always 300 seconds (5 minutes) So during a 5 minute sustain period there are 20 burst periods. Both burst and sustain limits are tracking at the same time and as such count requests at the same time. Both the burst and the sustain limit are set on the service meaning each service has its own burst and sustain count. To help you understand how these two limits work together the table below shows a user playing a title which is making a number of requests of a service that has implemented FGRL. In this case the burst limit is 30 requests in 15 seconds and the sustain limit is 100 requests over 5 minutes.

| Time Period (seconds)  | Requests per  burst period  | Requests per sustain period  | \# of throttled Requests within the 15 sec interval | Which Limit? (burst, sustain, or both)  |
|-------------|--------------|----------------|-----------------------------------------------------|---------------------------|
| 0-15        | 35           | 35             | 5                                                   | Burst                     |
| 15-30       | 28           | 63             | 0                                                   | N/A                       |
| 30-45       | 21           | 84             | 0                                                   | N/A                       |
| 45-60       | 36           | 120            | 20                                                  | Both                      |
| 60-75       | 24           | 144            | 24                                                  | Sustain                   |
| …           | …            | …              |                                                     | …                         |
| 285-300     | 4            | 148            |                                                     | Sustain                   |

The table shows that in the first 15 seconds the user trips the burst limit by making 35 requests. Those 5 extra requests are dropped and 5 429 Reponses are sent. It is worth noting that those 5 request though throttled still count towards the sustain limit. Once either limit is tripped no requests are let through as shown when both limits trip at the 45 second mark and again when only 4 requests are made at the 285 second mark.

## HTTP 429 Response object

When the associated user and title count is at or above either the burst or sustain limit the service will not handle the request and will instead return a HTTP 429 response. The HTTP 429 code stands for “too many requests” will be accompanied by a header containing a “retry after X seconds” value. FGRL 429’s contains a retry after header that specifies the amount of time the calling entities should wait before trying again. Developers that use XSAPI will not have to worry as XSAPI honors and handles the Retry-After header. The actual response will contain the following fields:

| Field Name      | Value Type | Example                | Definition                       |
|-----------------|------------|------------------------|----------------------------------|
| Version         | Integer    | `"version":1`          |                                  |
| currentRequests | Integer    | `"currentRequests":13` | Total number of requests sent    |
| maxRequests     | Integer    | `"maxRequests":10`     | Total number of requests allowed |
| periodInSeconds | Integer    | `“periodInSeconds”:15` | Time window                      |
| Type            | String     | `“type”:”burst”`       | Throttle limit type              |

## Implemented limits

The following services have implemented FGRL limits, with enforcement of these limits in place since **May 2016**. To reiterate, these limits will be the same across all sandboxes and titles. **Any title that was published via XDP or Dev Center and shipped prior to May 2016 will be considered Legacy and therefore exempted.**

| **Name** | **Burst Limit** (15 seconds per user per title) | **Sustain Limit** (300 seconds per user per title) | **Certification Limit** (10x Sustained, 300 seconds per user per title) |
|----------------------------|---------------------------|----------------------------|----------------------------|
| Stats Read                 | 100                       | 300                        | 3000                       |
| Profile                    | 10                        | 30                         | 300                        |
| MPSD                       | 30                        | 300                        | 3000                       |
| Presence                   | Read 10, Write 3          | Read 100, Write 30         | Read 1000, Write 300       |
| Social                     | 10                        | 30                         | 300                        |
| Leaderboards               | 30                        | 100                        | 1000                       |
| Achievements               | 100                       | 300                        | 3000                       |
| Smart Match                | 10                        | 100                        | 1000                       |
| User Posts                 | 100                       | 300                        | 3000                       |
| Stats Write                | 100                       | 300                        | 3000                       |
| Privacy                    | 10                        | 30                         | 300                        |
| Clubs                      | 10                        | 30                         | 300                        |

The table above represents the current list of services that were selected for FGRL. This list is not final as new services and existing services can be added. When a service is going to be added the table will be updated and an announcement will be made. The limits represented in the table should also not be viewed as finalized. As services change and evolve so too will the limits however you will be notified and the necessary legacy exemptions will be made.

As of **April 2018** titles will not pass certification if they exceed the sustain limit (limit at which rate limiting takes effect) by 10x.  For example, if the sustained limit at which FGRL takes effect is set to 300 calls in 300 seconds as specified in the table above, titles at or above 3000 calls in 300 seconds will fail certification. 

## Service Mapping and Title Effects of Rate Limiting

| **Name** | **Service Endpoint** | **Anticipiated Game Impact of FGRL**
| --- | :---: | --- 
| Stats Read | userstats.xboxlive.com | Achievements or Leaderboards entries not updated or retrieved.
| Profile | profile.xboxlive.com | Player’s data not updated or displayed correctly.
| MPSD | sessiondirectory.xboxlive.com | Joins/invites would not complete correctly, sessions not created or updated properly which can cause title failures.
| Presence | presence.xboxlive.com | Player’s in-game presence would not be accurate.
| Social | social.xboxlive.com | Impacts all friends writes (e.g. adding a friend, making someone favorite etc.) and may impact friend reads (e.g. fetch my friend list). Developers are encouraged to call the peoplehub for read rather than social.xboxlive.com.
| Leaderboards | leaderboards.xboxlive.com | In-game UX for leaderboards would not populate/update.
| Achievements | achievements.xboxlive.com | In-game UX for achievements unlocked would not be updated.
| Smart Match | momatch.xboxlive.com | Matches would not be successfully set up.
| User Posts | userposts.xboxlive.com | User posts would not appear.
| Stats Write | statswrite.xboxlive.com | Achievements or Leaderboards entries not updated.
| Privacy | privacy.xboxlive.com | Privacy failures may result in blocked access for all callers.
| Clubs | Clubhub.xboxlive.com | Player may not be able to see their in-game clubs.

**NOTE:** The latest API mapping is regularly updated and can be found under [Live Trace Analyzer API Mapping](https://github.com/Microsoft/xbox-live-trace-analyzer/blob/master/Source/XboxLiveTraceAnalyzer.APIMap.csv).

## FAQ

## How can I determine I am being throttled and what steps can I take?

Please refer to the [Xbox Live Best Practices](best-practices-for-calling-xbox-live.md) document as it will contain steps for improving your call pattern as well as an explanation of how the XSAPI assertion and XSAPI Social and Multiplayer managers can be used to notify you of and mitigate throttling issues.

Another option is to record a trace of the Xbox Live calls and then analyze that trace using the [Xbox Live Trace Analyzer tool](https://docs.microsoft.com/windows/uwp/xbox-live/tools/analyze-service-calls).  To record a trace, you can either use Fiddler to record a .SAZ file, or use the built-in trace logging of XSAPI. For more information, how to use turn on traces in XSAPI see the Xbox Live documentation page "Analyze calls to Xbox Live Services". Once you have a trace, the Xbox Live Trace Analyzer tool will warn upon detecting throttled calls.

You can find the best practices paper on GDNP and SDK and XDK docs 1602 and higher.

### Can limits change?

The intent is that the published limits will not change over time. However, if the need were to arise, it is possible that some of the limits could be made stricter; in that case, titles already released to RETAIL would be made exempt from the updated limits.

### Are more services going to get limits?

Yes, more services and new services can and will be creating limits. Though just like this first FGRL release you will be notified and the proper precautions will be taken.

### When will these changes take effect?

Rate limits have been enforced since **May 2016**.  As of **April 2018**, titles exceeding the specified sustained limits by 10x or more will not pass the Xbox Certification process.

### What if we can’t adhere to the limits?

Please see the [Xbox Live Best Practices](best-practices-for-calling-xbox-live.md) and ensure you are following these steps.  also consider using the [social manager](../../social-platform/intro-to-social-manager.md) if you are being rate limited with any of the social services.

If after following these steps, you are still unable to remain under the limits, please reach out to your Developer Account Manager.

**NOTE: Titles at or above 10x the specified sustain limit will not be allowed to pass cert after April 2018**.  For example, if the sustained limit at which FGRL takes effect is set to 300 calls in 300 seconds as specified in the table above, titles at or above 3000 calls in 300 seconds will fail certification.

### What about my existing Title?

Any titles in RETAIL before April 2018 are considered Legacy and are exempt.

### Content Updates?

For a legacy or exempted title, content updates will also be exempt, though we strongly encourage you to leverage the tools and assets to optimize the service integration aspects of your game.

### Can I get an exemption for my game until I can make a content update?

Please speak with your Developer Account Manager.
