---
title: Create a Windows widget with GitHub Copilot
description: Use a reusable GitHub Copilot prompt to select a supported Windows widget architecture, generate the provider and package registration, and validate the result in the Widgets Board.
ms.topic: how-to
ms.date: 10/27/2026
ms.localizationpriority: medium
author: GrantMeStrength
ms.author: jken
---

# Create a Windows widget with GitHub Copilot

You can use GitHub Copilot agent mode or GitHub Copilot CLI to create a widget in an existing app repository. The reusable, copy-and-paste prompt in this article tells the agent to select a supported widget architecture, generate the required source and package registration, build the app, and validate the widget in the Windows Widgets Board. The prompt is included in this article, not provided as a separately installable asset.

AI-generated code still requires review. Verify the selected APIs against the [Microsoft.Windows.Widgets.Providers API reference](/windows/windows-app-sdk/api/winrt/microsoft.windows.widgets.providers), review every manifest capability and package dependency, and don't include secrets or production credentials in your prompt.

## Before you use the prompt

Decide as many of these requirements as possible:

- The widget's purpose, display name, and data source.
- Whether the content should use an Adaptive Card or HTML from a URL.
- Whether the app is a packaged C# desktop app, a packaged C++/WinRT desktop app, or a PWA.
- The supported sizes: small, medium, and large.
- User actions, URL navigation, and optional customization settings.
- Refresh behavior, offline or signed-out fallback content, and persistent per-widget state.
- For a desktop provider, package identity and the architectures you intend to ship.

If you don't specify a choice, the prompt directs Copilot to ask for information that changes the architecture and to use documented defaults for smaller implementation details.

## Reusable widget-generation prompt

Replace the bracketed widget request, then paste the complete prompt into GitHub Copilot agent mode or an interactive GitHub Copilot CLI session.

> [!IMPORTANT]
> The [Windows App Development CLI](../../dev-tools/winapp-cli/index.md), used by the prompt's optional CLI deployment path, is currently in **public preview**. Features and commands may change before the final release.

```text
Create and validate a Windows widget in this repository.

Widget request:
[Describe the widget's purpose, content, data source, supported sizes, actions,
customization, refresh behavior, preferred language, and packaging constraints.]

Work as an implementation agent, not as an advisor. Inspect the existing repository,
create or update the required files, build the selected architecture, deploy a development
package, and test the widget in the Windows Widgets Board when the local environment supports it.

Never report a build, deployment, or Widgets Board check as passed unless you performed it
and observed the result. If you can't operate or observe the Widgets Board, list each
unperformed check under "Manual verification required" with exact steps and expected results.
Do not infer a pass from successful compilation, package registration, or Designer rendering.

Use these requirements:

1. Select exactly one supported implementation path:
   - Packaged C# desktop provider that implements Microsoft.Windows.Widgets.Providers.IWidgetProvider.
   - Packaged C++/WinRT desktop provider that implements IWidgetProvider.
   - PWA widget defined in the web app manifest and handled by its service worker.
   If the request doesn't contain enough information to select a path, ask before creating files.
   Don't mix the PWA service-worker model with the desktop COM provider model.
   Apply desktop-specific API, package, and lifecycle requirements below only to desktop providers.
   For a PWA, use the web app manifest, service-worker, testing, and publishing guidance at:
   https://learn.microsoft.com/microsoft-edge/progressive-web-apps/how-to/widgets

2. For a desktop provider:
   - Use a currently supported stable Windows App SDK release. Verify the package and API names
     against current Microsoft Learn documentation instead of inventing a version.
   - Require package identity. Register the out-of-process COM server and
     com.microsoft.windows.widgets app extension in Package.appxmanifest.
   - Generate one CLSID and use the same value in the provider class, COM factory registration,
     com:Class element, and WidgetProvider/CreateInstance element.
   - Implement CreateWidget, DeleteWidget, OnActionInvoked, OnWidgetContextChanged, Activate,
     and Deactivate.
   - Treat WidgetContext.DefinitionId as the manifest definition and WidgetContext.Id as the
     individual pinned instance. Store and restore state per instance.
   - On process startup, call WidgetManager.GetDefault().GetWidgetInfos() and restore pinned
     instances from CustomState. Make creation and recovery idempotent.
   - Synchronize shared state because provider callbacks can run concurrently.
   - Don't retain callback argument objects after a callback returns.
   - For C# only: follow the documented class-factory pattern. In IClassFactory.CreateInstance,
     obtain a temporary pointer with MarshalInspectable<IWidgetProvider>.FromManaged(new T()),
     query the requested interface with Marshal.QueryInterface, and release the temporary
     pointer in finally. Return HRESULTs at the COM boundary. Use a Windows application output
     type (WinExe) for the final experience so COM activation doesn't open a console window.
   - If the widget is customizable, set IsCustomizable="true" in the widget manifest, implement
     IWidgetProvider2.OnCustomizationRequested, and render a separate customization card. Read
     Adaptive Card input values from WidgetActionInvokedArgs.Data, validate them, and persist
     the resulting settings per widget. Confirm that the COM-callable wrapper exposes every
     provider interface that the class implements.
   - For C++/WinRT: use the walkthrough's non-caching winrt::no_module_lock class factory,
     serialize provider construction, query the requested interface, and translate exceptions
     to HRESULTs at the COM boundary.
   - Send logging to an app-controlled log or debugger output.
   - Target explicit architectures such as x64 and ARM64. Don't use AnyCPU.

3. Select the content model:
   - For an Adaptive Card widget, create a JSON template and a separate JSON data payload.
     Serialize values used as TextBlock text as strings, including numeric counts.
   - For a desktop HTML widget, use the documented web-widget metadata.webUrl payload, provide an
     Adaptive Card fallback, and add IWidgetResourceProvider or IWidgetProviderMessage only
     when the requested scenario needs those APIs.
     Web widgets are available only to users in the European Economic Area (EEA).
     Check regional and host requirements before selecting this path.
   - For a PWA, follow the PWA widget manifest and service-worker documentation instead of
     generating desktop IWidgetProvider code.

4. Implement interaction safely:
   - For a desktop provider, handle Action.Execute in OnActionInvoked. For a PWA, handle it
     through the service worker's widgetclick event.
   - Display Action.Execute only when $host.isUserContextAuthenticated is true.
   - Use Action.OpenUrl for navigation and for an interaction that must remain available when
     the widget user context isn't authenticated.
   - Validate every action verb and input before changing state.
   - Don't put secrets, access tokens, or credentials in the Adaptive Card payload or custom state.

5. Implement responsive and fallback content:
   - For a desktop provider, declare every supported size in Package.appxmanifest.
   - Use $host.widgetSize, $host.hostTheme, and other documented $host properties when needed.
   - Design the small layout separately; don't assume a medium layout will fit.
   - Don't rely on scrolling to reach the widget's primary information. Bound configurable
     collections, and verify that every configured item remains visible in the largest supported
     size. Prefer compact, item-specific links over broad summary links when users need to act
     on individual items.
   - Provide explicit loading, unavailable, offline, and signed-out states that are relevant
     to the requested widget.
   - For a desktop provider, set WidgetUpdateRequestOptions.IsPlaceholderContent when the
     payload isn't yet personalized or actionable.
   - Include accessible display names, descriptions, icon assets, screenshot assets, and
     screenshot alternative text for the widget picker.

6. Keep capabilities and dependencies minimal:
   - Add only the package capabilities required by the widget and its data source.
   - Use HTTPS for remote content.
   - Don't add broad file-system, account, network, or restricted capabilities without a
     requirement and an explanation.

7. Build and deploy:
   - Follow the repository's existing build and packaging structure.
   - For a PWA, follow the PWA widget documentation to test from a public endpoint and package
     with PWABuilder when targeting the Microsoft Store. Don't generate a desktop COM server.
     The Windows App SDK, COM registration, and CLI-first C# steps below are desktop-specific.
   - For a desktop provider, prefer the specific Windows App SDK runtime and widget component
     packages required by the provider over an umbrella dependency when producing a small
     framework-dependent package.
   - Reuse a stable development certificate for local upgrade testing. Don't overwrite a trusted
     certificate on every build. Never publish a private key, PFX, certificate password, token,
     or other secret in source control or release assets.
   - If a public installer trusts a self-signed development certificate, pin the expected public
     certificate thumbprint, verify that it is an unexpired non-CA certificate restricted to code
     signing, and confirm that it signed the MSIX before requesting trust. With explicit user
     consent, install only the public certificate in **Trusted People**. Never add an end-entity
     development certificate to **Trusted Root Certification Authorities**.
   - Increment the package version whenever installing changed MSIX contents. Windows rejects a
     changed package with the same identity and version.
   - The Windows App Development CLI is in public preview. Features and commands may change;
     check the installed version's help before using it.
   - For a CLI-first single-project MSIX C# provider, use EnableMsixTooling=true, keep
     Package.appxmanifest in the provider project, and register a development package with:
       winapp run <project>.csproj --configuration Release --arch x64 --no-launch
     This command registers the package without launching the app. It doesn't verify
     activation or rendering in the Widgets Board.
   - Don't try to replace a development-registered package with an MSIX that uses the same
     identity. Unregister the development package before testing the packaged deployment.
   - Fix build, manifest, registration, and architecture errors before continuing.
   - For a desktop provider, confirm that the registered package contains the windows.comServer
     extension and the com.microsoft.windows.widgets app extension.

8. Validate in the real Widgets Board:
   - Open the board, open the widget picker, and confirm the provider name, widget definition,
     icon, screenshot, and description.
   - Pin the widget and verify the initial template and data.
   - Resize it to every supported size and check for clipping, overlap, overflow, excess empty
     space, and incorrect conditional content.
   - Test every available action and verify the updated data and persisted per-widget settings.
     For desktop providers, verify CustomState.
   - If a desktop widget is customizable, open **Customize widget**, test invalid input, Save,
     and Cancel, and verify that saved settings refresh immediately and survive a host restart.
   - Open item-specific links and verify that each one targets the expected resource rather
     than a broad organization or product page.
   - For a desktop provider, restart the provider process and verify that pinned widget state
     is restored. For a PWA, verify restoration when the service worker activates again.
   - Unpin the widget and verify DeleteWidget for a desktop provider or the widgetuninstall
     service-worker event for a PWA.
   - Don't report interaction success from the Adaptive Cards Designer; it doesn't provide
     Windows $host values or invoke provider callbacks.
   - If the host reports an unauthenticated user context, verify the fallback and clearly
     report that Action.Execute couldn't be exercised in that session.

9. Finish with:
   - A concise list of files created or changed.
   - The selected widget architecture and why it matches the request.
   - Build and deployment results.
   - Observed Widgets Board test results for discovery, rendering, sizes, actions, restart
     persistence, and unpinning. Separate these from "Manual verification required", which
     must list every unperformed check with exact steps and expected results.
   - Any host, account, SDK, or OS limitation that prevented a requested test.

Don't stop after generating snippets. Deliver a buildable implementation and perform the
available validation. Don't invent Windows widget APIs, manifest elements, host properties,
or supported behavior.
```

## Review the generated widget

Before you publish the result, review these areas:

- The app uses the correct [widget provider architecture](widget-providers.md) and doesn't combine incompatible desktop and PWA patterns.
- Desktop provider registration follows the [widget provider package manifest format](widget-provider-manifest.md).
- The template uses only documented [Windows widget host properties](widgets-create-a-template.md#windows-widget-host-properties).
- The widget handles unauthenticated, unavailable, and placeholder states without presenting unsupported actions.
- Every supported size has been rendered and visually checked in the Widgets Board.
- The desktop provider restores custom state after restart and handles concurrent callbacks safely, or the PWA restores its widgets when the service worker activates.
- The final app uses only the capabilities and dependencies required by its scenario.

For broader guidance about reviewing AI-generated code, see [Security and responsible AI for Windows development](../ai-assisted/security-and-responsible-ai.md).
