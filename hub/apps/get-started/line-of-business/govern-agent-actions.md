---
title: Govern agent-initiated actions in WinUI line-of-business apps
description: Plan identity, least-privilege authorization, human approval, and audit for actions that agents perform through a WinUI 3 business app.
ms.topic: concept-article
ms.date: 10/28/2026
author: GrantMeStrength
ms.author: jken
ms.localizationpriority: medium
---

# Govern agent-initiated actions in WinUI line-of-business apps

Before you enable agent access to a line-of-business (LOB) app, decide **who is acting**, **what that actor is allowed to do**, and **who approves consequential changes**. Record those decisions as policies that your services can enforce, rather than instructions that you expect an agent to follow.

This article focuses on governance of agent-initiated business actions. For MCP tool design and Windows registration, see [Make a WinUI line-of-business app usable by AI agents](agent-ready-lob-apps.md). For broader deployment and configuration planning, see [Plan enterprise manageability for WinUI LOB apps](enterprise-manageability.md).

## Identify who is acting

Distinguish the user, the agent host or workload, and the code that runs the action. These aren't necessarily three separately authenticated principals.

| Party | Example | What you need to establish |
|---|---|---|
| User | The signed-in employee | Whose data and permissions apply |
| Agent host or workload | An interactive assistant or a scheduled automation | Which authenticated application or workload initiated the request |
| App or tool | Your app's MCP server | Which integration executed the business action |

Use validated identity claims and trusted integration metadata for authorization and audit attribution. A tool argument that names an agent or user doesn't prove that identity. If you can't verify which agent generated a request, don't present an agent-supplied label as authenticated evidence.

Choose an identity pattern for each integration:

- **Delegated user access.** Use this as the default for interactive LOB workflows. The service authorizes both the user and the calling application for the requested action. Plan an approved delegated authentication flow; don't assume that a contained MCP server can use the WinUI app's MSAL token cache or Web Account Manager session. See [Sign in and call a protected enterprise API from a WinUI app](sign-in-call-api.md).
- **Workload access without a user.** Treat scheduled automation as a separate principal with narrowly assigned application permissions and an accountable owner. Use a [managed identity](/entra/identity/managed-identities-azure-resources/overview) when the hosting environment supports one, or an organization-approved workload authentication mechanism. A local Windows process doesn't acquire an Azure managed identity merely by being an agent. Don't embed a shared service account password or a long-lived API key in the desktop app.

> [!IMPORTANT]
> When an integration acts for a user, restrict the action to the intersection of the user's permissions, the calling application's allowed operations, and enterprise agent policy. Don't fall back to a more privileged workload identity when delegated authorization fails.

## Authorize every call on the server

The desktop client isn't a security boundary. Enforce authorization in your back-end service for every call, whether it comes from the UI or an agent.

- Validate the access token for your API, including its issuer, audience, tenant, and applicable permissions, using supported identity middleware.
- Define scopes or roles per business action, for example `orders.read` and `orders.submit`, instead of one broad permission for the whole app.
- Establish the request's integration channel from trusted authentication and service context, not a caller-controlled `isAgent` flag.
- Restrict agent integrations separately from interactive UI operations. For example, a user might submit orders in the UI while the agent integration can only create drafts.
- Recheck authorization and policy when committing a prepared action, not only when creating the draft.

Plan how authorization changes take effect when a user leaves, loses a role, or an administrator disables the integration. Previously issued access tokens can remain valid until expiry or another supported enforcement mechanism rejects them. Don't promise immediate revocation from short token lifetimes alone. Define and test the service-side checks required by your organization's revocation policy; see [Access tokens in the Microsoft identity platform](/entra/identity-platform/access-tokens).

## Classify actions by risk

Assign each action a risk tier and apply a consistent set of controls. The following table is a starting point for your organization's policy, not a Windows or MCP enforcement feature.

| Tier | Examples | Controls to require |
|---|---|---|
| Read | Get order status or look up a permitted customer record | Authorization, data minimization, audit entry |
| Reversible change | Create a draft or update a field with version history | Authorization, audit, visible activity history, supported reversal |
| Consequential change | Submit for approval, send to a customer, or change an amount | Authorization, audit, explicit human approval before commit |
| Restricted | Delete records, change permissions, or make financial or legal commitments | Don't expose through the agent integration; keep an authorized human workflow |

Classify read operations by the sensitivity and volume of data they expose, not just whether they change state. For example, exporting a customer directory might require stronger controls than reading one order's status.

Publish the tier assignments in your design documentation so reviewers and administrators can see what the integration permits. Limit workload-only automation to explicitly approved operations; don't interpret the absence of an interactive user as permission to skip an approval requirement.

## Design human approval

For consequential changes, a person must approve the action before it takes effect. Make the decision specific and enforceable:

- **Show the effect, not the call.** Display what changes in business terms, such as "Submit expense report ER-2291 for $1,240 to Contoso Travel," not just a tool name and JSON.
- **Show verified provenance.** Identify the requesting user and integration, and distinguish verified identity from descriptive agent labels.
- **Confirm in a trusted surface.** Prefer your app's authenticated review workflow. A host confirmation prompt is sufficient only if your service can verify an approval bound to that operation and authorized approver. Don't accept an agent-supplied `confirmed` parameter.
- **Scope approval narrowly.** Bind it to the target record, exact values or draft version, approver, and expiry. If the values change, require approval again.
- **Fail closed.** Cancellation, expiry, missing approval, or an authorization failure must leave the consequential action uncommitted.

### Prepare, then commit

Separate preparation from the authorized business operation:

1. The agent calls a tool such as `create_expense_draft`. The service checks authorization, saves a draft with a version, and returns its identifier and summary.
2. The user reviews the current draft in the authenticated app or another verifiable approval surface. The service records approval for that version.
3. Before commit, the service checks the approver's current permissions, enterprise policy, and draft version. It consumes the approval for that operation and prevents duplicate commits if the client retries.
4. The service records who prepared, approved, and committed the action, with a shared correlation ID.

Don't let an agent edit an approved draft and then reuse the earlier approval. Use concurrency checks and an idempotency strategy appropriate to your service so a timeout or retry doesn't submit the same business action twice.

## Audit agent activity

Build on your existing [diagnostics and audit logging](diagnostics-audit-logging.md) rather than creating an unrelated logging pipeline.

| Record | Agent-specific information |
|---|---|
| Actor | Authenticated user where applicable, calling application or workload, integration identifier, and delegated or workload-only access |
| Action | Tool name, risk tier, target identifier where permitted, and outcome |
| Approval | Whether approval was required, authorized approver, approved draft version, timestamp, and commit outcome |
| Correlation | A service-generated or validated identifier propagated between the app, MCP server, and back-end service |

Record denied and policy-blocked calls as well as successful actions. Protect the audit destination, restrict access, and apply organization-approved retention. Don't log tokens, sensitive prompts, or full payloads unless that data handling is explicitly approved.

In the app, give users an **Agent activity** view with relevant recent actions and supported reversal options. Reversing an action is another authorized business operation; preserve the original audit record and record the reversal separately.

## Give IT control

Use managed settings to let administrators govern agent access without releasing new app code:

- Turn the integration on or off.
- Limit permitted risk tiers, for example read-only.
- Allow or block specific operations.
- Require approval for specified actions.
- Control diagnostic verbosity and audit retention within compliance requirements.

Make the back-end service's policy authoritative. The MCP server can also reject disallowed requests early, but a modified client must not bypass service policy. Don't rely on a registry value or UI setting on a user-controlled device as the only enforcement mechanism.

Define behavior when policy can't be loaded or is stale, and choose a fail-closed behavior for consequential actions. Keep mandatory audit events enabled even when diagnostic verbosity is reduced. See [Make a WinUI LOB app policy-aware](policy-aware-settings.md) for client-side configuration and locked-settings UI.

For Windows registrations that require static tool metadata, enforce restrictions when processing calls rather than dynamically changing the registered tool list. Disabling an operation must prevent execution even if the agent has already discovered or cached that tool.

## Defend against manipulated instructions

An agent might read malicious instructions in email, documents, web pages, or tool results. Design the service so that an inappropriate tool choice still can't bypass business rules:

- Validate parameters, target ownership, ranges, and allowed values.
- Apply rate and volume limits appropriate to the action and authenticated principal.
- Return only authorized records and the fields needed for the task.
- Keep authorization and approval decisions outside model-generated instructions.
- Test repeated calls, stale approvals, modified drafts, and attempts to bypass disabled operations.

Windows containment limits access to local resources for applicable package-identity registrations. It doesn't authorize enterprise API calls, stop an allowed business operation from being misused, or contain a remote server. Treat it as an additional boundary, not a replacement for these controls. See [Securely containing MCP servers on Windows](/windows/ai/mcp/servers/mcp-containment).

## Governance review checklist

- [ ] Each integration has an identified owner and a documented delegated or workload identity pattern.
- [ ] The service verifies the caller, user where applicable, tenant, and permissions.
- [ ] Every action has an assigned risk tier and enforceable policy.
- [ ] Consequential operations require approval bound to the exact action and draft version.
- [ ] Commit checks prevent stale approvals and duplicate submissions.
- [ ] Audit records distinguish preparation, approval, commit, denial, and reversal.
- [ ] IT can disable or restrict operations at the service boundary.
- [ ] Revocation, policy outages, and previously issued tokens have defined behavior.
- [ ] Users can review activity and reverse changes where supported.

## Related content

- [Make a WinUI line-of-business app usable by AI agents](agent-ready-lob-apps.md)
- [Plan enterprise manageability for WinUI LOB apps](enterprise-manageability.md)
- [Add diagnostics and audit logging to a WinUI LOB app](diagnostics-audit-logging.md)
- [Make a WinUI LOB app policy-aware](policy-aware-settings.md)
- [Sign in and call a protected enterprise API from a WinUI app](sign-in-call-api.md)
- [MCP servers on Windows overview](/windows/ai/mcp/servers/mcp-server-overview)
