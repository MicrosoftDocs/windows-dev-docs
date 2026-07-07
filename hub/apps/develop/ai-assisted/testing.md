---
title: "AI-assisted testing for Windows apps"
description: Use the winui-ui-testing skill and winapp ui commands to automate UI inspection and testing of your WinUI 3 app.
ms.topic: overview
ms.date: 07/05/2026
ms.author: jken
author: GrantMeStrength
---

# AI-assisted testing for Windows apps

The `winapp ui` commands and `winui-ui-testing` skill let your AI agent inspect, interact with, and validate your app's UI automatically — describing what it sees in natural language so you can iterate without writing test harness code first.

## UI inspection commands

The `winui-ui-testing` skill is included in the `winui@awesome-copilot` plugin. If you installed the plugin during [setup](quickstart.md#prerequisites), no additional install is required.

| Command | What it does |
|---------|-------------|
| `winapp ui inspect` | Dumps the accessibility tree of the running app |
| `winapp ui screenshot` | Captures a screenshot of the app window |
| `winapp ui click <selector>` | Clicks a button or selects an item |
| `winapp ui invoke <selector>` | Invokes the default action on a control |
| `winapp ui search <text>` | Searches the element tree by name or AutomationId |
| `winapp ui set-value <selector> <value>` | Sets the value of a TextBox or ComboBox |

## Ask your agent to test the app

1. Run the app:
   ```powershell
   dotnet run
   ```
2. Ask your agent a natural-language question, such as:
   > "Look at the running app and tell me if the Save button is reachable when the form is empty."
3. The agent uses `winapp ui inspect` and `winapp ui screenshot` to examine the app and respond.

This workflow lets you catch issues — missing keyboard focus, disabled controls, layout problems — before writing a single line of test code.

## Write automated tests

Once you've explored the app interactively, ask your agent to generate formal tests. Use this starter prompt:

```text
Write an xUnit UI test for my WinUI 3 app that:
1. Launches the app with dotnet run
2. Verifies the main window title is "My App"
3. Clicks the button with AutomationId "SaveButton"
4. Verifies a success message appears
Use the winapp ui commands for element interaction.
```

The generated tests use `winapp ui` commands as the interaction layer, so they run without a separate UI automation framework.

## Set AutomationIds in your XAML

For `winapp ui click` to target elements reliably, set `AutomationProperties.AutomationId` in your XAML:

```xml
<Button AutomationProperties.AutomationId="SaveButton"
        Content="Save" />
<TextBox AutomationProperties.AutomationId="TitleInput" />
```

Ask your agent: *"Add AutomationId attributes to all interactive controls in this XAML."*

## Run tests in CI

You can run `winapp ui` commands in a GitHub Actions workflow. The tests require a graphical session; use a Windows runner with `runs-on: windows-latest`.

```yaml
name: UI tests
on: [push, pull_request]
jobs:
  ui-test:
    runs-on: windows-latest
    steps:
      - uses: actions/checkout@v4
      - name: Setup .NET
        uses: actions/setup-dotnet@v4
        with:
          dotnet-version: '9.0.x'
      - name: Install winapp CLI
        run: winget install --id Microsoft.WinAppCLI --silent --accept-package-agreements --accept-source-agreements --disable-interactivity
      - name: Build
        run: dotnet build
      - name: Run UI tests
        run: dotnet test --filter Category=UITest
```

> [!NOTE]
> The `winapp ui` commands interact with the live accessibility tree of a running process. Tests must launch the app as part of the test setup (or in a `TestInitialize` step) and close it in teardown.

## Check accessibility automatically

The `winapp ui inspect` command outputs the full accessibility tree — element names, types, states, and AutomationIds. Ask your agent to review it for accessibility gaps:

```text
Run "winapp ui inspect" on the running app and identify any issues:
- Controls without accessible names
- Missing keyboard focus order
- Buttons or links that aren't reachable by Tab
Report findings as a prioritized list, most critical first.
```

This prompt works well as a first-pass accessibility review before running a formal audit tool. For a complete accessibility checklist, see [Accessibility overview for Windows apps](../accessibility.md).

## Related content

- [Quickstart: Build and publish a Windows app with AI](quickstart.md)
- [WinUI agent plugin](winui-agent-plugin.md)
- [Accessibility overview for Windows apps](../accessibility.md)
