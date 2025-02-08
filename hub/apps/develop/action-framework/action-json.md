---
title: Action JSON schema for Action Framework providers
description: Describes the format of the action definition JSON file format for Action Framework.
ms.topic: article
ms.date: 02/04/2025
ms.localizationpriority: medium
---



# Action JSON schema for Action Framework providers

Describes the format of the action definition JSON file format for Action Framework.

## Example action definition JSON file

```json
"version": 1, 
  "actions": [ 
    { 
      "id": "Contoso.SampleGreeting", 
      "description": "A simple greeting action.", 
      "icon": "ms-resource//...", 
      "inputs": [ 
        { 
          "name": "UserFriendlyName", 
          "kind": "Text" 
        }, 
        { 
          "name": "PetName", 
          "kind": "Text", 
          "required": false 
        } 
      ], 
      "inputCombinations": [ 
        { 
          "inputs": ["UserFriendlyName"], 
          "description": "Greet ${UserFriendlyName.Text}" 
        }, 
        { 
          "inputs": ["UserFriendlyName", "PetName"], 
          "description": "Greet ${UserFriendlyName.Text} and their pet ${PetName.Text}" 
        } 
      ], 
      "contentAgeRating": "child",  
      "invocation": 
      {
        { 
          "type": "Uri", 
          "uri": "contoso://greetUser? userName=${UserFriendlyName.Text}&petName=${PetName.Text}", 
        }, 
        "where": [ 
          "${UserFriendlyName.Text.Length > 3}" 
        ] 
      } 
    }, 
    { 
      "id": "Contoso.SampleGetText", 
      "description": "Summarize a file", 
      "icon": "ms-resource://...", 
      "inputs": [ 
        { 
          "name": "FileToSummarize", 
          "kind": "File" 
        } 
      ], 
      "inputCombinations": [ 
        { 
          "inputs": ["FileToSummarize"], 
          "description": "Summarize ${FileToSummarize.Path}" 
        }, 
      ], 
      "outputs": [ 
        { 
          "name": "Summary", 
          "kind": "Text" 
        } 
      ],
      "contentAgeRating": "child", 
      "invocation": { 
        "type": "COM", 
        "clsid": "{...}" 
      } 
    } 
  ] 
} 
```

## Action definition JSON properties

The tables below describe the properties of the action definition JSON file.

### Document root

| Property | Type | Description | Required |
|----------|------|-------------|----------|
| version | string | Schema version. When new functionality added, the version increments by one. | Yes. |
| actions | Action[] | Defines the actions provided by the app. | Yes. |

### Action

| Property | Type | Description | Required |
|----------|------|-------------|----------|
| id | string | Action identifier. Must be unique per app package. This value is not localizable. | Yes |
| description | string | User-facing description for this action. This value is localizable. | Yes |
| icon | string | Localizable icon for the action. This value is an *ms-resource* string for an icon deployed with the app. | No |
| inputs | Inputs[] | List of entities that this action accepts as input. | Yes |
| inputCombinations | InputCombination[] | Provides descriptions for different combinations of inputs. | Yes |
| outputs | Output[] | If specified, must be an empty string in the current release. | No |
| invocation | Invocation | Provides information about how the action is invoked. | Yes |
| contentAgeRating | string | A field name from the [UserAgeConsentGroup](/uwp/api/windows.system.userageconsentgroup)that specifies the appropriate age rating for the action. The allowed values are "Child", "Minor", "Adult". If no value is specified, the default behavior allows access to all ages. | No |

### Output

| Property | Type | Description | Required |
|----------|------|-------------|----------|
| name | string | The variable name of the entity. This value is not localizable. | Yes |
| kind | string | A field name from the **ActionEntityKind** specifying the entity type. This value is not localizable. The allowed values are "None", "Document", "File", "Photo", "Text". | Yes |

### InputCombination

| Property | Type | Description | Required |
|----------|------|-------------|----------|
| inputs | string[] | A list of Input names for an action invocation. The list may be empty. | Yes |
| description | string | Description for the action invocation. This value is localizable. | No |
| where | string[] | One or more conditional statements determining the conditions under which the action applies. | No |

### Invocation

| Property | Type | Description | Required |
|----------|------|-------------|----------|
| type | string | The instantiation type for the action. The allowed values are "uri" and "com" | Yes |
| uri | string | The absolute URI for launching the action. Entity usage can be included within the string. | Yes, for URI instantiated actions. |
| clsid | string | The class ID for the COM class that implements **IActionProvider**. | Yes, for COM actions |
| inputData | A list of name/value pairs specifying additional data for URI actions. | No. Only valid for URI actions. |


## Related articles


