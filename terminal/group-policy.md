---
title: Group Policy for Windows Terminal
description: Learn how to set Group Policy for Windows Terminal.
ms.date: 04/25/2025
ms.topic: how-to 
---

# Group Policy for Windows Terminal

Since Windows Terminal Preview 1.22, Windows Terminal is released on GitHub with Administrative Templates that help you to configure Windows Terminal using Group Policy.

Group Policy is a feature in Microsoft Windows that allows administrators in a business environment to manage and configure operating system settings, applications, and user environments centrally, including access available via Windows Terminal. Learn more in [Group Policy overview for Windows Server](/windows-server/identity/ad-ds/manage/group-policy/group-policy-overview).

## How to set up Windows Terminal Group Policy

### Download

You can find the latest administrative templates (ADMX and ADML files) in the assets section of our [newest Windows Terminal release on GitHub](https://github.com/microsoft/terminal/releases/latest). The file is named `GroupPolicyTemplates-<Version>.zip`

### Add the administrative template to an individual computer

1. Unzip the `GroupPolicyTemplates-<Version>.zip` file to your **Policy Definition** template folder (`C:\Windows\PolicyDefinitions`).

> [!NOTE]
> The Group Policy is available in English (United States region), or `en-US`, as the fallback language. If no language-location specific context is added, the `en-US` default will be used.

### Add the administrative template to Active Directory

1. On a domain controller (a server that responds to security authentication requests within a Windows Server domain) or a [workstation with RSAT](/windows-server/remote/remote-server-administration-tools), go to the **PolicyDefinition** folder (also known as the Central Store) on any domain controller for your domain. For older versions of Windows Server, you might need to create the PolicyDefinition folder. For more information, see [How to create and manage the Central Store for Group Policy Administrative Templates in Windows](/troubleshoot/windows-client/group-policy/create-and-manage-central-store)
2. Copy the `WindowsTerminal.admx` file to the **PolicyDefinition** folder.
(`%systemroot%\sysvol\domain\policies\PolicyDefinitions`)
3. Copy the `WindowsTerminal.adml` file to the matching language folder in your language folder in your Policy Definition folder. Create the folder if it does not already exist.
(`%systemroot%\sysvol\domain\policies\PolicyDefinitions\EN-US`)
4. If your domain has more than one domain controller, the new ADMX files will be replicated to them at the next domain replication interval.

### Import the administrative template in Intune

You can find all instructions on how to import the administrative template in Intune on [Import custom ADMX and ADML administrative templates into Microsoft Intune](/mem/intune/configuration/administrative-templates-import-custom#add-the-admx-and-adml-files).

> [!Important]
> You will need to import `Windows.admx` since the Windows Terminal ADMX files contains references to that file.

## Policies

### Disabled Source Profiles

Supported on Windows Terminal 1.21 or later, this policy disables source profiles from being generated. Source names can be arbitrary strings. Potential candidates can be found as the "source" property on profile definitions in Windows Terminal's `settings.json` file.

Common sources are:

- Windows.Terminal.Azure
- Windows.Terminal.PowershellCore
- Windows.Terminal.Wsl

For instance, setting this policy to `Windows.Terminal.Wsl` will disable the built-in WSL integration of Windows Terminal. Existing profiles will disappear from Windows Terminal after adding their source to this policy.

#### Group Policy (ADMX) information

- GP unique name: DisabledProfileSources
- GP name: Disabled Profile Sources
- GP path: Administrative Templates/Windows Components/Windows Terminal/
- GP scope: Computer and user
- ADMX file name: WindowsTerminal.admx

#### Registry information

- Path: Software\Policies\Microsoft\Windows Terminal
- Name: DisabledProfileSources
- Type: MULTI_SZ
- Example value: `Windows.Terminal.Azure`

### Enabled Language Models/AI Providers

Supported on Windows Terminal Canary 1.23 or later, this policy allows the listed Language Models / AI providers to be used with Terminal Chat.

Common providers are:

- OpenAI
- AzureOpenAI
- GitHubCopilot

For instance, setting this policy to `OpenAI` will allow the use of OpenAI in Terminal Chat.

**Disabling Terminal Chat**

Enabling this policy but leaving the list empty disallows all providers and disables the Terminal Chat feature. 

#### Group Policy (ADMX) information

- GP unique name: EnabledLMProviders
- GP name: Enabled Language Models/AI Providers
- GP path: Administrative Templates/Windows Components/Windows Terminal/
- GP scope: Computer and user
- ADMX file name: WindowsTerminal.admx

#### Registry information

- Path: Software\Policies\Microsoft\Windows Terminal
- Name: EnabledLMProviders
- Type: MULTI_SZ
- Example value: `AzureOpenAI`

### Default terminal application

Supported on Windows 11 22H2 or Windows 10 22H2 (Build 19045.3031, KB5026435) with Windows Terminal 1.17 or later, this policy allows you to select the default terminal application used in Windows.

The possible selections are:
- Automatic selection (Windows Terminal, if available)
- Windows Console Host (legacy)
- Windows Terminal
- Windows Terminal Preview (if available)

> [!Note]
> If you select Windows Terminal Preview and it is not installed, the system will fallback to the legacy Windows Console Host. (Please note that the settings interfaces showing "Let windows decide" in this case as configuration.)

> [!Important]
> This policy is implemented as and acts like a preference. This means it's value won't be deleted when you change it from configured to not configured.

_For more details on how to configure the default terminal application please see [Command Prompt and Windows Powershell on Microsoft Support](https://support.microsoft.com/windows/command-prompt-and-windows-powershell-for-windows-11-6453ce98-da91-476f-8651-5c14d5777c20)._

#### Group Policy (ADMX) information

- GP unique name: DefaultTerminalApplication
- GP name: Default terminal application
- GP path: Administrative Templates/Windows Components/Windows Terminal/
- GP scope: User only
- ADMX file name: WindowsTerminal.admx

#### Registry information

- Path: `Console\%%Startup`
- Name: DelegationTerminal, DelegationConsole
- Type: REG_SZ
- Example value: `{00000000-0000-0000-0000-000000000000}`

**Values for "automatic"**
- DelegationTerminal: `{00000000-0000-0000-0000-000000000000}`
- DelegationConsole: `{00000000-0000-0000-0000-000000000000}`

**Values for "Windows Console Host"**
- DelegationTerminal: `{B23D10C0-E52E-411E-9D5B-C09FDF709C7D}`
- DelegationConsole: `{B23D10C0-E52E-411E-9D5B-C09FDF709C7D}`

**Values for "Windows Terminal"**
- DelegationTerminal: `{E12CFF52-A866-4C77-9A90-F570A7AA2C6B}`
- DelegationConsole: `{2EACA947-7F5F-4CFA-BA87-8F7FBEEFBE69}`

**Values for "Windows Terminal Preview"**
- DelegationTerminal: `{86633F1F-6454-40EC-89CE-DA4EBA977EE2}`
- DelegationConsole: `{06EC847C-C0A5-46B8-92CB-7C92F6E35CD5}`
