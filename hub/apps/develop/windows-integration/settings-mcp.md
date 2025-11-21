---
title: Windows Settings MCP server
description: Learn how to use the Settings MCP server to manage settings on a Windows device. 
ms.topic: article
ms.date: 11/21/2025
ms.localizationpriority: medium
---



#  Windows Settings MCP server

This article provides information about the Settings Model Context Protocol (MCP) server, which allows apps to manage the settings on a Windows device through MCP interactions. For more information about MCP servers, see [MCP on Windows](/windows/ai/mcp/overview).

## Settings MCP server tools

The Settings MCP server provides the following tools.

### initialize

The `initialize` tool sends a handshake request to the Settings MCP server to establish context. The tool returns metadata about the server, such as its name, version, and capabilities, and may include session-specific configuration that informs the caller what resources and tools are available.

The following is an example `initialize` request: 

```json
{ 
  "method": "initialize" 
} 
```
 

The following is an example `initialize` response: 

```json
{ 
  "capabilities": { 
    "logging": {}, 
    "tools": { 
      "listChanged": true 
    } 
  }, 
  "serverInfo": { 
    "name": "ModelContextProtocol.Core", 
    "version": "0.3.0.0" 
  } 
}
```

### tools/list

The `tools/list` tool queries the MCP server for all registered tools. The response includes a list of tools supported by the server and provides identifiers, descriptions, and metadata, such as input and output schemas, for each tool to inform the caller of th operations it can invoke.

The following is an example `tools/list` request:

```json
{ 
  "method": "tools/list", 
  "params": {} 
}
```

The following is an example `tools/list` response from the Settings MCP server:

```json
{
  "tools": [ 
    { 
      "name": "is_settings_change_applicable", 
      "description": "If considering a change to a well-known Windows Setting to a provided value this tool can be called first to determine if that change is both possible and relevant right now", 
      "inputSchema": { 
        "type": "object", 
        "properties": { 
          "SettingsChangeRequest": { 
            "description": "A concise natural language statement of intent to change a particular setting to a particular value. This statement should contain no extraneous information about the reason for the change. This statement should be self-sufficient without any additional information needed for updating the corresponding settings action to the correct value, for example enable/disable or increase/decrease or connect/disconnect, and other similar options.", 
            "type": "string" 
          } 
        }, 
        "required": [ 
          "SettingsChangeRequest" 
        ] 
      } 
    }, 
    { 
      "name": "make_settings_change", 
      "description": "Change a well-known Windows Setting to the provided value", 
      "inputSchema": { 
        "type": "object", 
        "properties": { 
          "SettingsChangeRequest": { 
            "description": "A concise natural language statement of intent to change a particular setting to a particular value. This statement should contain no extraneous information about the reason for the change. This statement should be self-sufficient without any additional information needed for updating the corresponding settings action to the correct value, for example enable/disable or increase/decrease or connect/disconnect, and other similar options.", 
            "type": "string" 
          } 
        }, 
        "required": [ 
          "SettingsChangeRequest" 
        ] 
      } 
    }, 
    { 
      "name": "undo_settings_change", 
      "description": "Undo the recent change to a well-known Windows Setting back to the original value", 
      "inputSchema": { 
        "type": "object", 
        "properties": { 
          "UndoId": { 
            "description": "The ID returned by the make_settings_change tool in the structured content.", 
            "type": "string", 
            "format": "uuid" 
          } 
        }, 
        "required": [ 
          "UndoId" 
        ] 
      } 
    }, 
    { 
      "name": "open_settings_page", 
      "description": "Open the Windows Settings app to the page containing a well-known Windows Setting", 
      "inputSchema": { 
        "type": "object", 
        "properties": { 
          "SettingsChangeRequest": { 
            "description": "A concise natural language statement of intent to change a particular setting to a particular value. This statement should contain no extraneous information about the reason for the change. This statement should be self-sufficient without any additional information needed for updating the corresponding settings action to the correct value, for example enable/disable or increase/decrease or connect/disconnect, and other similar options.", 
            "type": "string" 
          } 
        }, 
        "required": [ 
          "SettingsChangeRequest" 
        ] 
      } 
    } 
  ] 
}
```

### is_settings_change_applicable

The `is_settings_change_applicable` tool queries the Settings MCP server to see whether changing a particular setting is possible or relevant in the current environment, considering the local device state. This tool supports the scenario where a user requests to change a well-known setting. The operation may or may not be allowed depending on the device state. This tool should be called before calling the `make_settings_change` tool.

The following is an example `is_settings_change_applicable` request:

```json
{ 
  "method": "tools/call", 
  "params": { 
    "name": "is_settings_change_applicable", 
    "arguments": { 
      "SettingsChangeRequest": "change theme to light mode" 
    }, 
    "_meta": { 
      "progressToken": 0 
    }
  } 
} 
```

The following is an example `is_settings_change_applicable` response:

```json
{ 
  "content": [ 
    {
      "type": "text", 
      "text": "Settings change is applicable" 
    } 
  ], 
  "structuredContent": { 
    "ActionDescription": "Change current theme to Light", 
    "IsRollbackSupported": true, 
    "IsApplicable": true 
  }, 
  "isError": false 
}
```

### make_settings_change

The `make_settings_change` tool changes a well-known Windows setting to a specified value. The `UndoId` value in the response can be passed into `undo_settings_change` to revert the associated change.

The following is an example `make_settings_change` request:

```json
{ 
  "method": "tools/call", 
  "params": { 
    "name": "make_settings_change", 
    "arguments": { 
      "SettingsChangeRequest": "change theme to light mode" 
    }, 
    "_meta": { 
      "progressToken": 1 
    } 
  } 
} 
```

The following is an example `make_settings_change` response. 

```json
{ 
  "content": [ 
    { 
      "type": "text", 
      "text": "Settings change has been offered to the user" 
    } 
  ], 
  "structuredContent": { 
    "ActionDescription": "Changed current theme to Light", 
    "IsRollbackSupported": true, 
    "UndoId": "445b7e72-6085-4bb9-a285-d4af2b5ebd05" 
  }, 
  "isError": false 
} 
```

### undo_settings_change

The `undo_settings_change` operation reverts a recent change to a well-known Windows setting. The `UndoId` value is provided in the response to a call to `make_settings_change`.

The following is an example `undo_settings_change` request:

```json
{ 
  "method": "tools/call", 
  "params": { 
    "name": "undo_settings_change", 
    "arguments": { 
      "UndoId": "445b7e72-6085-4bb9-a285-d4af2b5ebd05" 
    }, 
    "_meta": { 
      "progressToken": 2 
    } 
  } 
} 
```

The following is an example `undo_settings_change` response:

```json
{ 
  "content": [ 
    {
      "type": "text", 
      "text": "Settings change has been undone" 
    } 
  ], 
  "structuredContent": { 
    "ActionDescription": "Changed current theme to Light", 
    "IsRollbackSupported": false 
  }, 
  "isError": false 
} 
```

### open_settings_page

The `open_settings_page` tool opens the Windows Settings app to the page corresponding a well-known Windows setting.

The following is an example `### open_settings_page
` request:

```json
{ 
  "method": "tools/call", 
  "params": { 
    "name": "open_settings_page", 
    "arguments": { 
      "SettingsChangeRequest": "change theme to light mode" 
    }, 
    "_meta": { 
      "progressToken": 3 
    } 
  } 
}
```

The following is an example `open_settings_page` response:

```json
{ 
  "content": [ 
    { 
      "type": "text", 
      "text": "Settings page has been opened" 
    } 
  ], 
  "isError": false 
}
```

## Important calling conventions for the Windows Settings MCP server

A caller may or may not be able to modify the value of a particular Windows setting through the Settings MCP server, depending on the current device state. In order to ensure that a setting can be modified successfully, callers should always call `is_settings_change_applicable` before calling `make_settings_change`.

Undo operations performed with a call to `undo_settings_change` can't be reverted.


 





