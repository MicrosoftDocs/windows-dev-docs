---
title: Test App Signatures with Smart App Control
description: Learn how to enable Smart App Control and test your app's signature before distribution. Configure audit policies, check event logs, and verify code signing to ensure compatibility.
ms.topic: how-to
ms.date: 10/28/2025
# Customer intent: As a Windows developer, I want to learn how to test my app's signature with Smart App Control before distributing it to users.
---

# Test your app's signature with Smart App Control

Before distributing your signed app to users, test your app's signature against Smart App Control. Because Smart App Control evaluates binaries as it loads them, be sure to test all code paths and features of your app. This testing includes all of your app's install and uninstall binaries, all of your app's features, and all integrations with other apps that might load your binaries (for example, [Office add-ins](/office/dev/add-ins/overview/office-add-ins)). You can test Smart App Control by using audit policies, which create log entries without actually blocking your app from executing, or by testing directly against Smart App Control's enforcement mode.

## Configure Smart App Control for testing

You can configure Smart App Control in Windows Settings or by manually editing the Windows Registry.

### Configure Smart App Control by using Windows Settings

Go to **Settings** > **Privacy & Security** > **Windows Security** > **App and Browser Control** > **Smart App Control settings**.

> [!NOTE]
> Configuring Smart App Control to **Off** or **On** (enforcement) is a one-way operation. You can't change modes by using Windows Settings unless the current setting is **Evaluation**. For testing purposes, you can force Smart App Control into another setting [by using the registry](#configure-smart-app-control-by-using-the-registry).

If Smart App Control is in Evaluation mode, it evaluates your app's signature but doesn't block your app if its signature is invalid. In this mode, you can use [Audit Policies](#configure-the-smart-app-control-audit-policy) to view Smart App Control's output, including errors encountered while checking your app's signature.

Select **On** to put Smart App Control in enforcement mode. In this mode, Smart App Control prevents your app from running if its signature is invalid.

### Configure Smart App Control by using the Registry

> [!IMPORTANT]
> Smart App Control can be manually configured via the Registry **for testing purposes only.** Editing Smart App Control settings in this way could compromise the protection it provides.

Configuring Smart App Control using the Windows Registry allows you to force any desired enforcement mode, even if you cannot select that mode using [Windows Settings](#configure-smart-app-control-by-using-windows-settings). To configure Smart App Control:

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

You can verify Smart App Control's current mode by opening a command prompt and running the following command:

`citool.exe -lp`

Smart App Control is in evaluation mode if the value of Friendly Name is `VerifiedAndReputableDesktopEvaluation` and the value of Is Currently Enforced is `true`.

Smart App Control is in enforcement mode if the value of Friendly Name is `VerifiedAndReputableDesktop` and the value of Is Currently Enforced is `true`.

## Configure the Smart App Control audit policy

The default Windows Defender Application Control (WDAC) policy used by Smart App Control in evaluation mode doesn't log audit events in the CodeIntegrity Operational log. This policy reduces the size of the log on typical consumer devices that ship with Smart App Control in evaluation mode.

For the purposes of evaluating applications against Smart App Control, a developer or system administrator might want to enable audit logs in evaluation mode to see what files would be blocked if the system were in enforcement mode.  

> [!NOTE]
> Audit policies only apply when Smart App Control runs in Evaluation mode. In Enforcement mode, Smart App Control logs events by default.

You can download a zip file containing two sample policies [here](https://aka.ms/sacauditpolicies).

> [!NOTE]
> You can also create your own policies. For more information, see [Windows Defender Application Control (WDAC) example base policies](/windows/security/threat-protection/windows-defender-application-control/example-wdac-base-policies) and [Create WDAC policy for lightly managed devices](/windows/security/threat-protection/windows-defender-application-control/create-wdac-policy-for-lightly-managed-devices#create-a-custom-base-policy-using-an-example-wdac-base-policy).

### Smart App Control audit policy (SmartAppControlAudit.bin)

This policy is the standard Smart App Control policy with audit logs enabled in evaluation mode. All binaries and scripts allowed by signature and cloud reputation pass the policy, just as they do if enforcement mode is enabled. Applications and binaries that the policy blocks log an audit event.

> [!NOTE]
> This policy only works with Smart App Control in evaluation mode. The Smart App Control evaluation model might turn evaluation mode off when you apply this policy, so test with one of the other methods in this article.

When you apply this policy, the output for `citool.exe -lp` shows `VerifiedAndReputableDesktopEvaluationAudit` as the policy name.

#### Apply the Smart App Control audit policy

First, [ensure SAC is in evaluation mode](#configure-smart-app-control-for-testing).

To apply this policy, take ownership of the evaluation mode policy file and replace it with the audit policy. From an administrator command prompt or PowerShell window, run the following commands:

```powershell
takeown /f "C:\WINDOWS\System32\CodeIntegrity\CiPolicies\Active\{1283AC0F-FFF1-49AE-ADA1-8A933130CAD6}.cip" /a
icacls "C:\WINDOWS\System32\CodeIntegrity\CiPolicies\Active\{1283AC0F-FFF1-49AE-ADA1-8A933130CAD6}.cip" /grant Administrators:F
```

> [!NOTE]
> The `takeown` command might report success but fail to actually take ownership of the file. If the subsequent steps fail, use the manual method in the next section to take ownership.

If the commands above fail, or if you prefer to manually take ownership, use the following steps:

1. Right-click the file in File Explorer and select **Properties**.
1. Go to the **Security** tab, and choose **Advanced** at the bottom.
1. Select **Change** in the dialog.
1. In the popup dialog, enter your user information (for example, `<PC name>\<username>`) and select **OK**.
1. Select **OK** in the **Advanced Security Settings** dialog and confirm.
1. Reopen the file properties **Security** tab and select **Edit**.
1. Under **Administrators**, choose all the checkboxes and select **OK**, and confirm again in the popup dialog.

Now that you have ownership of the policy file, rename it to `{1283AC0F-FFF1-49AE-ADA1-8A933130CAD6}.cip.old`.  Rename the audit policy file you want to apply to `{1283AC0F-FFF1-49AE-ADA1-8A933130CAD6}.cip`, and copy it to the policy directory.

Run `citool.exe -r` from an admin command prompt to refresh the policy .

### Smart App Control audit policy without ISG (SmartAppControlAuditNoISG.bin)

Use this policy to test your own apps as a developer.

This policy checks binaries and scripts against Smart App Control in evaluation mode, without checking the Intelligent Security Graph. It means that only apps that a trusted certificate properly signs are allowed without audit events. Because reputation might not be available for newly published binaries and can change over time, ensuring that you correctly sign all your binaries is the best way to make sure users don't encounter issues when using your app. This requirement also applies when publishing through the Windows Store, where a signature from a cert obtained from a trusted Certificate Authority is required.

You can apply this policy even when you set Smart App Control to Off. When you apply this policy, the output for `citool.exe -lp` shows `VerifiedAndReputableDesktopEvaluationAuditNoISG` as the policy name.

#### Apply the Smart App Control Audit policy without ISG  

Use this policy to test applications in evaluation mode against the signing requirement of Smart App Control exclusively. This policy doesn't allow any app binaries based on cloud intelligence from the Intelligent Security Graph.

1. Ensure that Smart App Control is in evaluation mode or off.  

1. Run `mountvol S: /S` from an admin command prompt.  

1. Copy SmartAppControlAuditNoISG.bin to `S:\efi\microsoft\boot\cipolicies\active\{5283AC0F-FFF1-49AE-ADA1-8A933130CAD6}.cip`.

1. Run `citool.exe -r` from admin command line to refresh the policy.  

### Checking event logs  

Smart App Control logs any executable that it blocks (or would block) into the Code Integrity Event Logs. You can find those logs by opening the Event Viewer, then browsing to **Application and Services Logs** > **Microsoft** > **Windows** > **CodeIntegrity** > **Operational**.  

Smart App Control logs evaluation mode events with event ID 3076, and enforcement mode events with event ID 3077. 

> [!IMPORTANT]
> The CodeIntegrity event log only records information about blocked or audited files. It doesn't log which software installations fail or why installations fail when Smart App Control is enabled. The event log shows configuration changes and which individual files were blocked, but it doesn't provide installation-level diagnostics. To troubleshoot installation failures, you must review the 3076 and 3077 events to identify which specific files within the installer were blocked.

For more information about Smart App Control and Microsoft Defender event logging, see [Review event logs and error codes to troubleshoot issues with Microsoft Defender Antivirus](/microsoft-365/security/defender-endpoint/troubleshoot-microsoft-defender-antivirus).

## Related content

[Windows Defender Application Control (WDAC) example base policies](/windows/security/threat-protection/windows-defender-application-control/example-wdac-base-policies)

[Review event logs and error codes to troubleshoot issues with Microsoft Defender Antivirus](/microsoft-365/security/defender-endpoint/troubleshoot-microsoft-defender-antivirus)

[Create WDAC policy for lightly managed devices](/windows/security/threat-protection/windows-defender-application-control/create-wdac-policy-for-lightly-managed-devices)
