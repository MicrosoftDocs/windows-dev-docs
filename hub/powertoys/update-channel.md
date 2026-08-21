---
title: Choose a PowerToys update channel
description: Learn how Stable and Insider update channels work in PowerToys, how to switch channels, and how Group Policy controls preview updates.
author: GrantMeStrength
ms.author: jken
ms.date: 08/11/2026
ms.topic: concept-article
no-loc: [PowerToys, Windows, Stable, Insider]
# Customer intent: As a PowerToys user, I want to choose between stable and nightly updates and understand the effects of switching channels.
---

# PowerToys update channels

PowerToys offers two update channels. Choose a channel based on whether you prioritize release stability or early access to changes.

| Channel | What you receive |
| :--- | :--- |
| **Stable (Recommended)** | Tested, generally available releases. Stable is the default channel. |
| **Insider** | Nightly prerelease builds with the latest features and fixes. These builds receive less validation than stable releases and might be unstable. |

Insider builds are published as GitHub prereleases. They can contain incomplete changes or regressions, so use the Stable channel on systems where reliability is important.

## Change the update channel

1. Open **PowerToys Settings**.
1. Select **General**, and then expand **Update channel** under **Version & updates**.
1. Select **Stable (Recommended)** or **Insider**.

PowerToys saves your selection and checks for updates using the selected channel.

## What happens when you switch

When you switch to Insider, update checks include stable releases and prerelease builds. PowerToys offers the newest release with a version later than the version you have installed.

When you switch to Stable, update checks include only stable releases. PowerToys doesn't downgrade or uninstall an Insider build. If your installed Insider version is newer than the latest stable release, you remain on that version until a later stable release becomes available.

Switching channels doesn't reset your PowerToys settings.

## Group Policy behavior

Administrators can manage access to the Insider channel with the **Disable preview build updates** policy under **Administrative Templates > Microsoft PowerToys > Installer and Updates**.

- If the policy is enabled, PowerToys turns off preview updates, locks the update channel control, and offers only stable updates. The policy ignores the saved user preference without overwriting it.
- If the policy is disabled or not configured, you can choose either update channel.
- After an administrator removes an enabled policy, PowerToys restores the previously saved channel preference.

For policy identifiers and deployment details, see [PowerToys Group Policy configuration](./grouppolicy.md#disable-preview-build-updates).
