---
description: Learn about evolving inclusive design with Windows apps for Windows.  Design and build inclusive software with accessibility in mind.
ms.assetid: A6393A57-53F2-4F06-89AF-0D806FD76DB0
title: Designing inclusive software in Windows
label: Designing inclusive software
template: detail.hbs
ms.date: 03/17/2026
ms.topic: how-to
keywords: windows 11, winui, winappsdk, windows app sdk
ms.localizationpriority: medium
---

# Designing inclusive software for Windows

This topic describes how inclusive design treats accessibility as a core engineering and product-quality concern from the beginning of the lifecycle, not as a final compliance step, and informs how products look, feel, function, and behave. It outlines how to design and build Windows apps that are fully usable across a wide range of abilities, environments, and preferences.

"We define disability as a mismatch between the needs of the individual and the service, product or environment offered. Anyone can experience a disability. It is a common human trait to be excluded."  \- from the [Inclusive](https://www.microsoft.com/design/inclusive/) video  

Inclusive design improves products for everyone. Features originally intended for a specific scenario frequently become mainstream because they reduce friction for many users. Sidewalk curb cuts, television remote controls, and easy-fastening shoes are familiar examples: each began as an accommodation for a subset of users and became broadly beneficial. The same pattern applies in software when accessibility is built into core interaction and visual systems.

## Inclusive design principles

The following four principles guide Microsoft's inclusive design approach:

**Think universal**: We focus on what unifies people — human motivations, relationships, and abilities. This drives us to consider the broader social impact of our work. The result is an experience that has a diversity of ways for all people to participate.

**Make it personal**: Next, we challenge ourselves to create emotional connections. Human-to-human interactions can inspire better human-to-technology interaction. A person's unique circumstances can improve a design for everyone. The result is an experience that feels like it was created for one person.

**Keep it simple**: We start with simplicity as the ultimate unifier. When we reduce clutter people know what to do next. They're inspired to move forward into spaces that are clean, light, and open. The result is an experience that's honest and timeless.

**Create delight**: Delightful experiences evoke wonder and discovery. Sometimes it's magical. Sometimes it's a detail that's just right. We design these moments to feel like a welcomed change in tempo. The result is an experience that has momentum and flow.

## Inclusive design users

There are two broad groups of assistive technology (AT) users:

1. People who rely on AT due to disability, age-related changes, or temporary conditions (for example, limited mobility from an injury).  
2. People who use AT by preference because it is more efficient, comfortable, or convenient for a given context.

Research has long shown that awareness and adoption of AT are significant, yet still below potential for many users who could benefit from it.  

A 2003-2004 study commissioned by Microsoft and conducted by Forrester Research found that over half &mdash; 57 percent &mdash; of U.S. computer users ages 18 to 64 could benefit from assistive technology. Many did not identify as disabled, but reported task-related challenges. Forrester also reported that roughly one in four experienced visual difficulty, one in four experienced wrist or hand pain, and one in five experienced hearing difficulty.  

Beyond permanent disability, functional needs change over time and by situation. Capability is dynamic, not fixed. Designing for that variability produces more resilient and broadly usable software.  

## Practical inclusive design steps

This section outlines practical steps to apply for inclusive design during planning and implementation.  

### Describe the target audience

Define your target audience in functional terms, not only demographic terms. Consider language, hearing, vision, cognition, literacy, dexterity, and mobility characteristics, and assess whether core tasks remain equally achievable across these profiles.  

### Talk to actual humans with specific needs

Engage directly with people who have relevant functional needs. Include this research early and continuously. For example, Microsoft observed that deaf Xbox users were disabling app notifications. Discussions with users revealed that notification placement obscured closed captions. Moving the notification slightly higher resolved the issue. Telemetry exposed the behavior, but user conversations exposed the cause.  

### Choose a development framework wisely

Framework choice is an accessibility architecture decision. When evaluating frameworks (for example, Win32, web, or WinUI-based stacks), assess built-in accessibility support, default control behavior, and the expected cost of custom controls. Your framework choice determines how much accessibility behavior you inherit versus how much you must implement and maintain yourself.  

Use standard Windows controls whenever possible. They typically include established accessibility semantics, keyboard behavior, and assistive technology interoperability.

### Design a logical hierarchy for your controls

After framework selection, design a logical hierarchy for your controls. This includes layout structure and keyboard order. For assistive technology (AT) such as screen readers, visual arrangement alone is not enough; your UI must also expose a coherent programmatic structure. A logical hierarchy helps ensure that structure is understandable and navigable. It is mainly used to:  

1. Provide programmatic context for the logical reading order of UI elements.  
2. Identify clear boundaries between custom controls and standard controls.  
3. Define how parts of the UI interact as a system.  

A logical hierarchy is also an effective way to discover and address usability issues. If a simple experience requires a deeply nested or overly wide structure, the design likely needs simplification. If modeling a basic dialog takes pages of hierarchy diagrams, reconsider the information architecture before implementation. For more information, download the [Engineering Software for Accessibility](https://www.microsoft.com/download/details.aspx?id=19262) eBook.  

### Design appropriate visual UI settings

When designing visual UI behavior, ensure that your product supports high contrast, uses system font and smoothing settings, scales correctly across dots-per-inch (DPI) configurations, meets contrast targets, and avoids color-only differentiation.  

#### High contrast setting

High Contrast mode increases visual distinction between text, controls, and backgrounds. For many users, this reduces eyestrain and improves readability. Validate that controls (including links and status indicators) are implemented with system resources rather than hard-coded colors so content remains perceivable and actionable in high contrast themes.  

#### System font settings

To maintain readability and avoid rendering artifacts, honor default system fonts and text rendering behavior, including anti-aliasing and smoothing. Custom fonts can introduce legibility and compatibility issues when users customize the UI presentation or use assistive technology.  

#### High DPI resolutions

Scalable UI is critical for many users with low vision. Interfaces that do not adapt correctly at high DPI can cause clipping, overlap, and hidden interaction targets, resulting in inaccessible workflows.  

#### Color contrast ratio

[Section 508](https://www.section508.gov/) in the United States, along with similar regulations elsewhere, require sufficient contrast between text and background. In this guidance, target at least 5:1 for standard text and 3:1 for large text (18-point, or 14-point bold).  

#### Color combinations

About 7 percent of males (and less than 1 percent of females) have some form of color vision deficiency. Because some combinations are difficult to distinguish, never rely on color alone to convey state or meaning. For decorative visuals (such as icons or backgrounds), choose combinations that preserve visual differentiation for common color vision profiles. Building with these constraints from the start significantly improves inclusiveness.  

## Summary &mdash; seven steps for inclusive design

In summary, apply these seven steps to keep inclusive design actionable throughout development.

1. Establish inclusive design as a product requirement, and align design decisions to real user outcomes.  
2. Prefer framework-provided controls where possible to maximize built-in accessibility support and reduce custom maintenance cost.  
3. Design a logical hierarchy that defines standard controls, custom controls, and keyboard focus behavior in the UI.  
4. Build support for core system behaviors (such as keyboard navigation, high contrast, and high DPI) into the product architecture.  
5. Implement and validate against authoritative guidance, including the [Microsoft accessibility developer hub](https://developer.microsoft.com/windows/accessible-apps) and framework specifications.  
6. Test with users who have functional needs to confirm that implemented patterns are effective in real scenarios.  
7. Ship with clear accessibility documentation so future contributors can sustain and evolve the design intent.  

## Related topics

* [Inclusive design](https://www.microsoft.com/design/inclusive/)
* [Engineering Software for Accessibility](https://www.microsoft.com/download/details.aspx?id=19262)
* [Microsoft accessibility developer hub](https://developer.microsoft.com/windows/accessible-apps)
* [Develop accessible Windows apps](/windows/apps/develop/accessibility)
* [Accessibility overview](accessibility-overview.md)