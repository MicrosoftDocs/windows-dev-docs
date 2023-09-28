---
title: PowerToys Group Policy
description: Group policy objects 
ms.date: 08/03/2023
ms.topic: article
no-loc: [PowerToys, Windows, Group Policy, Win]
---

# Group Policy Objects

Since version 0.64, PowerToys is released on GitHub with [GroupPolicyObject files](/previous-versions/windows/desktop/policy/group-policy-objects). You can check these releases on <https://github.com/microsoft/PowerToys/releases>.

## How to install

A Group Policy Object is a virtual collection of policy settings with a unique name, such as a GUID, and represent policy settings in the file system and in the Active Directory. To install the PowerToys group policy follow these steps:

### Add the administrative template to an individual computer

1. Copy the _PowerToys.admx_ file to your Policy Definition template folder. (Example: `C:\Windows\PolicyDefinitions`)
2. Copy the _PowerToys.adml_ file to the matching language folder in your Policy Definition folder. (Example: `C:\Windows\PolicyDefinitions\en-US`)

### Add the administrative template to Active Directory

1. On a domain controller or workstation with [RSAT](/troubleshoot/windows-server/system-management-components/remote-server-administration-tools), go to the **PolicyDefinition** folder (also known as the _Central Store_) on any domain controller for your domain. For older versions of Windows Server, you might need to create the **PolicyDefinition** folder. For more information, see [How to create and manage the Central Store for Group Policy Administrative Templates in Windows](https://support.microsoft.com/help/3087759/how-to-create-and-manage-the-central-store-for-group-policy-administra).
2. Copy the _PowerToys.admx_ file to the PolicyDefinition folder. (Example: `%systemroot%\sysvol\domain\policies\PolicyDefinitions`)
3. Copy the _PowerToys.adml_ file to the matching language folder in the PolicyDefinition folder. Create the folder if it doesn't already exist. (Example: `%systemroot%\sysvol\domain\policies\PolicyDefinitions\EN-US`)
4. If your domain has more than one domain controller, the new [ADMX files](/troubleshoot/windows-client/group-policy/create-and-manage-central-store) will be replicated to them at the next domain replication interval.

### Scope

You will find the policies under "Administrative Templates/Microsoft PowerToys" in both the Computer Configuration and User Configuration folders. If both settings are configured, the setting in Computer Configuration takes precedence over the setting in User Configuration.

## Policies

Policy can force a PowerToys utility to be enabled or disabled for users covered by the policy.

### Configure enabled state

For each utility shipped with PowerToys, there's a "Configure enabled state" policy, which forces an **enabled** state for the utility.

- If you enable this setting, the utility will be always enabled and the user won't be able to disable it.
- If you disable this setting, the utility will be always disabled and the user won't be able to enable it.
- If you don't configure this setting, users are able to disable or enable the utility at their own discretion.
