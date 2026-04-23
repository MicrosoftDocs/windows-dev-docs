---
title: Windows Settings connector
description: Learn how to use the Windows Settings connector to manage settings on a Windows device. 
ms.topic: article
ms.date: 11/21/2025
ms.localizationpriority: medium
---



#  Windows Settings connector

This article provides information about the Windows Settings connector, which allows apps to manage the settings on a Copilot+ PC device through MCP interactions. For more information about MCP servers, see [MCP on Windows](/windows/ai/mcp/overview).

This connector is designed for user‑consented and reversible interactions. It simplifies natural‑language settings changes (for example, “turn on Bluetooth”, “increase text size”), ensures changes are applicable to the current device state before executing, and provides a rollback path when supported.

> [!NOTE]
> This feature requires a Copilot+ PC device.

> [!NOTE]
> In the current release, this feature only supports English and French languages. Other languages are unsupported.

## Settings MCP server tools

The Settings MCP server provides the following tools.


| Tool                      | Purpose                                                                 | Input schema (JSON)                                                                                                                                         | Output (structured)                                                      | Notes                                                    |
|---------------------------|-------------------------------------------------------------------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------|-------------------------------------------------------------------------|----------------------------------------------------------|
| is_settings_change_applicable | Queries whether a natural language settings change is applicable in the current device context. | `{ "type":"object", "properties": { "SettingsChangeRequest": { "type":"string" } }, "required":["SettingsChangeRequest"] }` | ActionDescription (string), IsRollbackSupported (bool), IsApplicable (bool). Standard CallToolResult wrapping. | Always call this tool before calling the other settings tools to avoid invalid operations.           |
| make_settings_change      | Executes a settings change. | `{ "type":"object", "properties": { "SettingsChangeRequest": { "type":"string" } }, "required":["SettingsChangeRequest"] }` | ActionDescription (string), IsRollbackSupported (bool), UndoId (string) when applicable; wrapped in `CallToolResult`. | An `UndoId` that can be passed into `undo_settings_change` is returned when `IsRollbackSupprted` is true.  |
| undo_settings_change      | Reverts a prior change that was made using `make_settings_change`.  | `{ "type":"object", "properties": { "UndoId": { "type":"string", "format":"uuid" } }, "required":["UndoId"] }`         | ActionDescription (string), IsRollbackSupported (always false for undo). |  The `UndoId` value is returned in the response to a call to `make_settings_change` . Undo operations are one-way and can't be rolled back.                      |
| open_settings_page        | Open Windows Settings to the page containing the targeted setting. | `{ "type":"object", "properties": { "SettingsChangeRequest": { "type":"string" } }, "required":["SettingsChangeRequest"] }` | No structured content; returns success text and `isError` flag.            | This tool is a utility for guided manual adjustment and is not used for automated changes. |


## Important calling conventions for the Windows Settings connector

A caller may or may not be able to modify the value of a particular Windows setting through the Settings MCP server, depending on the current device state. In order to ensure that a setting can be modified successfully, callers should always call `is_settings_change_applicable` before calling `make_settings_change`.

Undo operations performed with a call to `undo_settings_change` can't be reverted.

## How it works
	
The Windows Settings connector uses a lightweight language model called Settings Mu, which is fine-tuned using Windows Settings data to help users quickly find and adjust settings. The model runs locally on the device, analyzing a user's query to match with relevant settings already available in Settings.

The Settings Mu model has undergone fairness evaluations, and comprehensive Responsible AI, security, and privacy assessments. These steps ensure the technology is effective, equitable, and aligned with Microsoft's Responsible AI principles.

 
## Microsoft’s commitment to responsible AI and Privacy

Microsoft has been working to advance AI responsibly since 2017, when we first defined our AI principles and later operationalized our approach through our Responsible AI Standard. Privacy and security are core principles as we develop and deploy AI systems. We work to help our customers use our AI products responsibly, sharing our learnings, and building trust-based partnerships. For more information about our responsible AI efforts, the principles that guide us, and the tools and capabilities developed to ensure responsible AI technology, see [Responsible AI](/windows/ai/rai).





