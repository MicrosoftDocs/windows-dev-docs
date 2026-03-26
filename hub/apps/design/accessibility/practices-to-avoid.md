---
description: Lists the practices to avoid if you want to create an accessible Windows app.
ms.assetid: 024A9B70-9821-45BB-93F1-61C0B2ECF53E
title: Accessibility practices to avoid
label: Accessibility practices to avoid
template: detail.hbs
ms.date: 03/17/2026
ms.topic: article
keywords: windows 11, winui, winappsdk, windows app sdk
ms.localizationpriority: medium
---

# Accessibility practices to avoid

When you build accessible Windows experiences, avoid the following implementation patterns. These patterns commonly degrade keyboard efficiency, reduce screen-reader reliability, or create unnecessary cognitive load for users who depend on assistive technology.

* **Avoid creating custom UI controls when a standard control is functionally sufficient.** Prefer built-in Windows controls, or third-party controls that already expose robust Microsoft UI Automation behavior. In most cases, standard controls require only app-specific accessibility metadata. By contrast, truly custom controls require full peer and pattern design, including a correct [**AutomationPeer**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Automation.Peers.AutomationPeer) implementation and ongoing validation (see [Custom automation peers](custom-automation-peers.md)).
* **Do not place non-interactive content in the tab order.** For example, avoid setting [**TabIndex**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.tabindex) on static text or decorative elements. Keyboard users and assistive technologies both rely on focus traversal to discover actionable UI. When non-interactive elements receive focus, users must spend additional navigation steps with no actionable outcome, and some screen-reader workflows become harder to parse because focus no longer implies interactivity.
* **Avoid layout strategies that decouple visual order from logical order unless you explicitly manage navigation order.** Absolute positioning (for example, via [**Canvas**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Controls.Canvas)) can produce a visual sequence that differs from declaration order, which is often used as the baseline reading order for accessibility tooling. Prefer container layouts that preserve document flow. If visual and logical order must diverge, define focus navigation intentionally by setting [**TabIndex**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.tabindex) values to match the intended reading and interaction sequence.
* **Do not rely on color alone to communicate state or meaning.** Color-only encoding excludes users with color-vision deficiencies and can fail in high contrast or custom theme scenarios. Pair color with an additional signal, such as text, iconography, shape, or programmatic state exposed to assistive technology.
* **Avoid full-surface automatic refresh unless a complete context reset is required by the user task.** If content must update, scope the update to the smallest region possible. Many assistive technologies interpret a full canvas refresh as a new UI tree and must rebuild their virtual representation, which interrupts reading position and increases interaction cost.
  
  A user-initiated navigation is a legitimate case for structural refresh. In that case, ensure that the initiating command is clearly named so users can anticipate that activation changes context or reloads content.

  > [!NOTE]
  > If you refresh content within a region, consider setting the [**AutomationProperties.LiveSetting**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.automation.automationproperties.livesettingproperty) property on that element to a non-default value such as **Polite** or **Assertive**. Some assistive technologies map this setting to the Accessible Rich Internet Applications (ARIA) live-region model and can announce that content in that region changed.

* **Do not present flashing UI at frequencies greater than three flashes per second.** Rapid flashing can trigger photosensitive seizure responses and should be treated as a hard accessibility risk, not a stylistic preference.
* **Do not change user context or trigger actions without explicit user intent.** Context changes include moving focus, loading new regions, opening new UI surfaces, and navigating between pages. Automatic context shifts can be disorienting, especially for screen-reader and switch-device users who depend on predictable focus and announcement flow. Acceptable exceptions include expected submenu expansion, inline validation feedback, contextual help tied to active input, or system-driven asynchronous state changes that are clearly communicated.

## Related topics

* [Accessibility](accessibility.md)
* [Accessibility in the Store](accessibility-in-the-store.md)
* [Accessibility checklist](accessibility-checklist.md)
