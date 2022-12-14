---
title: Test your app's signature with Smart App Control
description: Enable Smart App Control and verify your app is signed correctly
ms.topic: article
ms.date: 09/20/2022
---

# Test your app's signature with Smart App Control

Before distributing your signed app to users, you should test your app's signature against Smart App Control. Because Smart App Control evaluates binaries as they're loaded, be sure to test all code paths and features of your app. This includes testing all of your app's install and uninstall binaries, all of your app's features, and all integrations with other apps that might load your binaries (for example, [Office add-ins](/office/dev/add-ins/overview/office-add-ins)). You can test Smart App Control using audit policies, which will create log entries without actually blocking your app from executing, or test directly against Smart App Control's enforcement mode.

## Configure Smart App Control for Testing

You can configure Smart App Control in the Windows Settings app, or by manually editing the Windows Registry.

### Configure Smart App Control using Windows Settings

Go to **Settings** > **Privacy & Security** > **Windows Security** > **App and Browser Control** > **Smart App Control settings**.

> [!NOTE]
> Configuring Smart App Control to **Off** or **On** (enforcement) is a one-way operation. This means you cannot change modes using Windows Settings unless the current setting is **Evaluation**. For testing purposes, you can force Smart App Control into another setting [using the registry](#configure-smart-app-control-using-the-registry).

If Smart App Control is in Evaluation mode, Smart App Control will evaluate your app's signature, but will not block your app if its signature is invalid. In this mode, you can use [Audit Policies](#configure-smart-app-controls-audit-policy) to view Smart App Control's output, including errors encountered while checking your app's signature.

Select **On** to put Smart App Control in enforcement mode. In this mode, Smart App Control will prevent your app from running if its signature is invalid.

### Configure Smart App Control using the Registry

> [!IMPORTANT]
> Smart App Control can be manually configured via the Registry **for testing purposes only.** Editing Smart App Control settings in this way could compromise the protection it provides.

Configuring Smart App Control using the Windows Registry allows you to force any desired enforcement mode, even if you cannot select that mode using [Windows Settings](#configure-smart-app-control-using-windows-settings). To configure Smart App Control:

1. Open a command prompt with administrator privileges and execute the following commands:

    ```powershell
    manage-bde -protectors c: -disable -rebootcount 2
    "C:\Program Files\Windows Defender\MpCmdRun.exe" -RemoveDefinitions -DynamicSignatures
    ```

    > [!NOTE]
    > You may have to update the second command if your system drive is not C:.

2. Reboot into the boot menu by launching Settings and selecting **Recovery** > **Recovery Options** > **Advanced Startup** > **Restart now**.

3. From the advanced boot menu, select **Troubleshoot** > **Advanced** > **Command Prompt**. A recovery command prompt will open.

    > [!NOTE]
    > The recovery command prompt opens the recovery drive X: by default. This does not indicate your system drive has changed. Your system drive is still associated with its usual drive letter (usually C:).

4. Execute the following commands:

    > [!NOTE]
    > In the following commands, replace {VALUE} with the value of the mode you want to set.
    >
    > | Value | Mode             |
    > |-------|------------------|
    > | 0     | Off              |
    > | 1     | On (Enforcement) |
    > | 2     | Evaluation       |

    ```ps
    reg load HKLM\sac c:\windows\system32\config\system
    reg add hklm\sac\controlset001\control\ci\policy /v VerifiedAndReputablePolicyState /t REG_DWORD /d {VALUE} /f 
    reg add hklm\sac\controlset001\control\ci\protected /v VerifiedAndReputablePolicyStateMinValueSeen /t REG_DWORD /d {VALUE} /f
    reg unload hklm\sac

    reg load HKLM\sac2 C:\windows\system32\config\SOFTWARE
    reg add "hklm\sac2\Microsoft\Windows Defender" /v SacLearningModeSwitch /t REG_DWORD /d 0
    reg unload hklm\sac2
    ```

5. Restart the computer.

## Verify Smart App Control's current mode

You can verify Smart App Control's current mode by opening a command prompt and executing the following command:

`citool.exe -lp`

Smart App Control is in evaluation mode if the value of Friendly Name is `VerifiedAndReputableDesktopEvaluation` and the value of Is Currently Enforced is `true`.

Smart App Control is in enforcement mode if the value of Friendly Name is `VerifiedAndReputableDesktop` and the value of Is Currently Enforced is `true`.

## Configure Smart App Control's audit policy

The default Windows Defender Application Control (WDAC) policy used by Smart App Control in evaluation mode does not log audit events in the CodeIntegrity Operational log. This is to reduce the size of the log on typical consumer devices shipping with Smart App Control in evaluation mode.

For the purposes of evaluating applications against Smart App Control, a developer or system administrator may want to enable audit logs in evaluation mode to see what files would be blocked if the system were in enforcement mode.  

> [!NOTE]
> Audit policies only apply when Smart App Control is running in Evaluation mode. In Enforcement mode, Smart App Control will log events by default.

A zip file containing two sample policies below can be downloaded [here](https://aka.ms/sacauditpolicies).

> [!NOTE]
> You can also create your own policies. See [Windows Defender Application Control (WDAC) example base policies](/windows/security/threat-protection/windows-defender-application-control/example-wdac-base-policies) and [Create WDAC policy for lightly managed devices](/windows/security/threat-protection/windows-defender-application-control/create-wdac-policy-for-lightly-managed-devices#create-a-custom-base-policy-using-an-example-wdac-base-policy) for more information.

### Smart App Control audit policy (SmartAppControlAudit.bin)

This is the standard Smart App Control policy, with audit logs enabled in evaluation mode. All binaries and scripts allowed by signature and cloud reputation will pass the policy, just as they would if enforcement mode was enabled. Applications and binaries that would be blocked would log an audit event.

> [!NOTE]
> This policy only works with Smart App Control in evaluation mode. It is still possible for the Smart App Control evaluation model to turn evaluation mode off with this policy is applied, so we recommend testing with one of the other methods below.

When this policy is applied, the output for `citool.exe -lp` will show `VerifiedAndReputableDesktopEvaluationAudit` as the policy name.

#### Apply the Smart App Control audit policy

First, [ensure SAC is in evaluation mode](#configure-smart-app-control-for-testing).

Take ownership of the evaluation mode policy file `C:\WINDOWS\System32\CodeIntegrity\CiPolicies\Active\{1283AC0F-FFF1-49AE-ADA1-8A933130CAD6}.cip` using [takeown.exe](/windows-server/administration/windows-commands/takeown). If you are unable to use takeown, then you can manually take ownership using the following steps:  

> [!IMPORTANT]
> We strongly recommend using takeown, if possible.

1. Right click the file in explorer and select "Properties."
1. Go to the Security tab, and choose Advanced at the bottom.
1. Click "Change" in the dialog .
1. In the popup dialog, enter your user information (e.g. `<PC name>\<username>`) and click OK.
1. Click OK in the Advanced Security Settings dialog and confirm .
1. Reopen the file properties Security tab and click "Edit ."
1. Under Administrators, choose all the checkboxes and click OK, and confirm again in the popup dialog .

Now that you have ownership of the policy file, rename it to `{1283AC0F-FFF1-49AE-ADA1-8A933130CAD6}.cip.old`.  Rename the audit policy file you want to apply to `{1283AC0F-FFF1-49AE-ADA1-8A933130CAD6}.cip`, and copy it to the policy directory.

Run `citool.exe -r` from an admin command prompt to refresh the policy .

### Smart App Control audit policy without ISG (SmartAppControlAuditNoISG.bin)

This is the recommended policy for testing your own apps as a developer.

This policy checks binaries and scripts against Smart App Control in evaluation mode, without checking the Intelligent Security Graph, meaning that only apps that are properly signed by a trusted certificate will be allowed without audit events. Because reputation may not be available for newly published binaries, and can change over time, ensuring that all your binaries are correctly signed is the best way to make sure users don’t encounter issues using your app. This is also the requirement when publishing through the Windows Store, where a signature from a cert obtained from a trusted Certificate Authority is required.

This policy can be applied even when Smart App Control is set to Off. When this policy is applied, the output for `citool.exe -lp` will show `VerifiedAndReputableDesktopEvaluationAuditNoISG` as the policy name.

#### Apply the Smart App Control Audit policy without ISG  

This policy is for testing applications in evaluation mode against the signing requirement of Smart App Control exclusively, and will not allow any app binaries based on cloud intelligence from the Intelligent Security Graph.

Ensure that Smart App Control is in evaluation mode or off  

Run `mountvol S: /S` from an admin command prompt  

copy SmartAppControlAuditNoISG.bin to `S:\efi\microsoft\boot\cipolicies\active\{5283AC0F-FFF1-49AE-ADA1-8A933130CAD6}.cip `.

Run `citool.exe -r` from admin command line to refresh the policy  

### Checking Event Logs  

Smart App Control logs any executable that was (or would have been) blocked into the Code Integrity Event Logs.  You can find those logs by opening the Event Viewer, and then browsing to **Application and Services Logs** > **Microsoft** > **Windows** > **CodeIntegrity** > **Operational**.  

Smart App Control logs evaluation mode events with event ID 3076, and enforcement mode events with event ID 3077. For more information about Smart App Control and Microsoft Defender event logging, please see [Review event logs and error codes to troubleshoot issues with Microsoft Defender Antivirus](/microsoft-365/security/defender-endpoint/troubleshoot-microsoft-defender-antivirus).
