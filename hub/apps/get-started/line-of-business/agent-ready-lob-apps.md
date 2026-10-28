---
title: Make a WinUI line-of-business app usable by AI agents
description: Expose business actions in a WinUI 3 line-of-business app as MCP tools, with user authorization, enterprise controls, and audit logging.
ms.topic: how-to
ms.date: 10/28/2026
author: GrantMeStrength
ms.author: jken
ms.localizationpriority: medium
---

# Make a WinUI line-of-business app usable by AI agents

Line-of-business (LOB) apps hold the business actions that people perform every day: look up a customer, submit an expense, approve an order, or check stock. You can make selected actions available to an AI agent so that users can combine them with tasks from other apps.

This article explains how to expose actions in a WinUI 3 LOB app through the Model Context Protocol (MCP), with controls that IT administrators can manage and audit. It focuses on business-workflow decisions and links to the MCP on Windows documentation for implementation and registration details. Adding AI features inside your app is a separate scenario; see [Add AI capabilities to a line-of-business WinUI app](ai-for-lob-apps.md).

> [!NOTE]
> MCP on Windows and the Windows on-device agent registry (ODR) include pre-release features that may change before they're commercially released. Check the [MCP server registration requirements](/windows/ai/mcp/servers/mcp-windows-identity#prerequisites) for supported Windows builds, signing, and tooling. Your agent host must support your chosen MCP connection and discovery method; Windows registration doesn't automatically connect every agent product to your app.

## Decide whether your app should be agent-ready

Exposing actions to agents is worth considering when:

- Users repeat the same multi-step task in your app, often copying data from email, documents, or other apps.
- The action is well defined, with clear inputs, outputs, and permissions.
- Your organization uses an MCP-capable agent host and wants it to work with internal systems.

It's usually not the right first step when the action has no clear owner, the underlying service has no authorization model, or a mistake can't be detected or reversed.

## Choose what an agent can do

Expose **business actions**, not UI controls. Start from the business services that your app already uses, rather than pages or controls.

| Expose as a tool | Keep in the app UI |
|---|---|
| `find_customer(name)` - read-only lookup | Bulk delete or data purge |
| `get_order_status(order_id)` | Changing security or policy settings |
| `create_expense_draft(...)` - creates a draft, not a submission | Actions that have no undo or audit trail |
| `submit_for_approval(draft_id)` - with user confirmation | Anything that needs a signature or legal attestation |

Use these guidelines:

- **Start read-only.** Ship lookup tools first, then add actions that change data.
- **Prefer drafts over commits.** Let an agent prepare work that a person reviews in your app.
- **Keep tools small.** One tool per business action is easier for an agent to choose correctly than one tool with many modes.
- **Reuse your service layer.** The app and its MCP server should use the same validation and authorization rules. Don't rely on checks that exist only in the UI.

## Write tool descriptions that agents can use

Agents choose and call tools based on the name, description, and input schema you declare. Vague or missing descriptions can cause wrong tool choices and repeated retries.

For each tool:

- Use a verb-noun name in the business vocabulary your users already know, for example `get_invoice`, not `query2`.
- Write the description as one or two plain sentences: what it does, when to use it, and what it doesn't do.
- Define input types and constraints, with descriptions and examples. Use enumerations for fixed values such as status codes.
- Return structured results with stable field names, plus a short human-readable summary.
- Return actionable errors, for example "Order SO-004521 not found. Use find_order to search by customer.", not a stack trace. Don't reveal records that the caller isn't authorized to see.

The following excerpt illustrates a tool definition, not a complete MCPB manifest:

```json
{
  "name": "get_order_status",
  "description": "Returns the current fulfillment status of a single sales order. Use find_order first if you only know the customer name. Read-only.",
  "inputSchema": {
    "type": "object",
    "properties": {
      "order_id": {
        "type": "string",
        "description": "Sales order number, for example SO-004521."
      }
    },
    "required": ["order_id"]
  }
}
```

In this example, `find_order` represents a separate lookup tool that you also implement and declare.

> [!IMPORTANT]
> For default-mode Windows registration, the server's runtime `initialize` and `tools/list` responses must match the static responses declared in the MCP bundle (MCPB) manifest. Don't dynamically change tool names, descriptions, or input or output schemas. Update the manifest with the implementation when you service the app. For details, see [Register an MCP server with an MCP bundle](/windows/ai/mcp/servers/mcp-mcpb).

## Build the MCP server

Build the server as a separate project in your solution that references the same business and data-access libraries as your WinUI app. For .NET apps, use the [MCP C# SDK](https://github.com/modelcontextprotocol/csharp-sdk). The [MCP on Windows samples](https://github.com/microsoft/mcp-on-windows-samples) include a packaged app that ships an MCP server alongside its UI.

Keep the server free of UI dependencies. A contained server runs in a separate agent session and can't directly access your app's windows or signed-in user's credentials. Design authentication and any handoff to your app explicitly, rather than assuming that a running WinUI app shares its session with the server.

If an action needs review, return a draft identifier and instructions for opening it in the app. Don't commit the action until your service can verify approval by an authorized user. Treat any agent-host confirmation UI as an additional safeguard, not a replacement for service-side authorization.

## Register the server with Windows

Choose the registration path that matches how you deploy your LOB app. Local binary servers with package identity can use the contained agent session. A remote server runs on its own infrastructure, not inside Windows containment.

| Your deployment model | Registration approach |
|---|---|
| Packaged with MSIX | Add the MCP app extension and MCPB manifest to your package. Windows registers the server when the package is installed. See [Register an MCP server from an app with package identity](/windows/ai/mcp/servers/mcp-windows-identity). |
| Packaged with external location | Use the package-identity registration path. See [Grant package identity by packaging with external location](../../desktop/modernize/grant-identity-to-nonpackaged-apps-overview.md). |
| Unpackaged (MSI or .exe) | An MCP bundle is an option, but directly installed bundles aren't available from ODR under its default protections. See [Register an MCP server with an MCP bundle](/windows/ai/mcp/servers/mcp-mcpb). |
| Remote service | Register the remote server manually. See [Manually register remote and local MCP servers](/windows/ai/mcp/servers/mcp-manual). Apply authentication and authorization on the remote service. |

For enterprise LOB apps that need contained local execution, prefer a supported package-identity registration path. Registration and removal then follow the app package. Don't make reducing agent-connector protections a production prerequisite.

For production releases, the [registration prerequisites](/windows/ai/mcp/servers/mcp-windows-identity#prerequisites) require a signing certificate that is part of the Microsoft Trusted Root Program. Many organizations sign internal LOB packages with a private certificate authority, so confirm your signing approach before you choose a contained, package-identity registration.

## Protect business data and identity

An agent acting on behalf of a user must never get more access than that user has. Before rollout, use [Govern agent-initiated actions in WinUI line-of-business apps](govern-agent-actions.md) to define identity patterns, action risk tiers, approval, and revocation behavior.

For the MCP implementation:

- Plan delegated authentication explicitly. Your app's [MSAL and Web Account Manager integration](sign-in-call-api.md) doesn't automatically make its tokens available to a contained server. Don't copy token caches across sessions.
- Treat tool input as untrusted, and call services that enforce authorization and approval for every operation. Tool descriptions and annotations alone don't enforce those rules.
- Declare only the supported resource capabilities your local server needs. File-access consent is granted to the agent host, not individually to each server used by that host. See [Securely containing MCP servers on Windows](/windows/ai/mcp/servers/mcp-containment).

## Make agent activity auditable and manageable

Use your existing logging and managed-configuration infrastructure for the integration:

- Propagate a validated correlation ID through tool calls and back-end requests. See [Add diagnostics and audit logging to a WinUI LOB app](diagnostics-audit-logging.md).
- Read applicable managed configuration and show locked settings in the app. See [Make a WinUI LOB app policy-aware](policy-aware-settings.md).
- Enforce restrictions when handling calls, without dynamically changing tool metadata required by Windows registration.

For the audit fields, authoritative service policy, and user activity history to design, see [Govern agent-initiated actions](govern-agent-actions.md).

## Test with real agents

1. Use [MCP Inspector](/windows/ai/mcp/servers/test-mcp-server) to call each tool directly and check its schema, results, and errors.
2. For Windows registration, confirm that the server appears in `odr.exe list`. Then test through your intended agent host under default protections; a direct Inspector call doesn't validate containment or host integration.
3. Ask the agent to complete realistic tasks in natural language and check that it chooses the right tool. Rewrite descriptions where it doesn't.
4. Test with restricted permissions, an expired sign-in, and unavailable services. Confirm that the agent can't exceed the authenticated user's permissions or bypass required approval.
5. Test misleading input, such as a document that tells the agent to delete records. Confirm that service-side policy still blocks unauthorized actions.
6. Disable agent access through managed policy, then test app updates and uninstallation. Confirm that calls are blocked and registration is updated or removed as appropriate to your deployment model.

## Agent-readiness checklist

- [ ] Each exposed action maps to one business task and reuses the app's business rules.
- [ ] Read-only tools ship first; consequential changes require verifiable user approval.
- [ ] Every tool has a clear name, description, input schema, and actionable errors.
- [ ] Runtime responses match the registration manifest where required.
- [ ] The service authenticates and authorizes the user without relying on the app's session or embedded secrets.
- [ ] Agent calls are logged with user, tool, result, and correlation ID.
- [ ] IT can disable or restrict agent access through an enforced managed setting.
- [ ] Registration, updates, and removal follow the chosen deployment model.
- [ ] Package signing meets the production prerequisites for the chosen registration path.
- [ ] The intended agent host works with the server under the organization's approved protections.

## Related content

- [Build line-of-business apps with WinUI - overview](index.md)
- [Govern agent-initiated actions in WinUI line-of-business apps](govern-agent-actions.md)
- [MCP servers on Windows overview](/windows/ai/mcp/servers/mcp-server-overview)
- [Plan enterprise manageability for WinUI LOB apps](enterprise-manageability.md)
- [Add AI capabilities to a line-of-business WinUI app](ai-for-lob-apps.md)
- [Sign in and call a protected enterprise API from a WinUI app](sign-in-call-api.md)
