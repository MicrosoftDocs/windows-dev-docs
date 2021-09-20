---
title: Xbox Live Test User Management API reference  
description: Learn how to get or update the list of users on the console by using the Xbox Device Portal REST API.
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.assetid: 70876ab6-8222-4940-b4fb-65b581a77d6a
---

# Xbox Live User Management

## Request

You can get the list of users on the console, or update the list--adding, removing, signing in, signing out, or modifying existing users.

| Method        | Request URI     | 
| ------------- |-----------------|
| GET           | /ext/user |
| PUT           | /ext/user |


**URI parameters**

* None

**Request headers**

* None

**Request body**

Calls to PUT should include a JSON array with the following structure:

* Users
  * AutoSignIn (optional) : bool disabling or enabling automatic signin for the account specified by EmailAddress or UserId.
  * EmailAddress (optional - must be provided if UserId is not provided unless signing in a sponsored user) : Email address specifying the user to modify/add/delete.
  * Password (optional - must be provided if the user isn't currently on the console) : Password used for adding a new user to the console.
  * SignedIn (optional) : bool specifying whether the provided account should be signed in or out.
  * UserId (optional - must be provided if EmailAddress is not provided unless signing in a sponsored user) : UserId specifying the user to modify/add/delete.
  * SponsoredUser (optional) : bool specifying whether to add a sponsored user.
  * Delete (optional) : bool specifying to delete this user from the console

## Response

**Response body**

Calls to GET will return a JSON array with the following properties:

* Users
  * AutoSignIn (optional)
  * EmailAddress (optional)
  * Gamertag
  * SignedIn
  * UserId
  * XboxUserId
  * SponsoredUser (optional)
  
**Status code**

This API has the following expected status codes.

| HTTP status code   | Description     | 
| ------------------ |-----------------|
| 200                | Call to GET was successful and JSON array of users returned in the response body |
| 204                | Call to PUT was successful and the users on the console have been updated |
| 4XX                | Various errors for invalid request data or format |
| 5XX                | Error codes for unexpected failures |
