---
Description: Lists the practices to avoid if you want to create an accessible Windows app.
ms.assetid: 024A9B70-9821-45BB-93F1-61C0B2ECF53E
title: Accessibility practices to avoid
label: Accessibility practices to avoid
template: detail.hbs
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Accessibility practices to avoid

If you want to create an accessible Windows app, see this list of practices to avoid: 

* **Avoid building custom UI elements if you can use the default Windows controls** or controls that have already implemented Microsoft UI Automation support. Standard Windows controls are accessible by default and usually require adding only a few accessibility attributes that are app-specific. In contrast, implementing the [**AutomationPeer**](/uwp/api/Windows.UI.Xaml.Automation.Peers.AutomationPeer) support for a true custom control is somewhat more involved (see [Custom automation peers](custom-automation-peers.md)).
* **Don't put static text or other non-interactive elements into the tab order** (for example, by setting the [**TabIndex**](/uwp/api/windows.ui.xaml.controls.control.tabindex) property for an element that is not interactive). If non-interactive elements are in the tab order, that is against keyboard accessibility guidelines because it decreases efficiency of keyboard navigation for users. Many assistive technologies use tab order and the ability to focus an element as part of their logic for how to present an app's interface to the assistive technology user. Text-only elements in the tab order can confuse users who expect only interactive elements in the tab order (buttons, check boxes, text input fields, combo boxes, lists, and so on).
* **Avoid using absolute positioning of UI elements** (such as in a [**Canvas**](/uwp/api/Windows.UI.Xaml.Controls.Canvas) element) because the presentation order often differs from the child element declaration order (which is the de facto logical order). Whenever possible, arrange UI elements in document or logical order to ensure that screen readers can read those elements in the correct order. If the visible order of UI elements can diverge from the document or logical order, use explicit tab index values (set [**TabIndex**](/uwp/api/windows.ui.xaml.controls.control.tabindex)) to define the correct reading order.
* **Don’t use color as the only way to convey information.** Users who are color blind cannot receive information that is conveyed only through color, such as in a color status indicator. Include other visual cues, preferably text, to ensure that information is accessible.
* **Don’t automatically refresh an entire app canvas** unless it is really necessary for app functionality. If you need to automatically refresh page content, update only certain areas of the page. Assistive technologies generally must assume that a refreshed app canvas is a totally new structure, even if the effective changes were minimal. The cost of this to the assistive technology user is that any document view or description of the refreshed app now must be recreated and presented to the user again.
  
  A deliberate page navigation that is initiated by the user is a legitimate case for refreshing the app's structure. But make sure that the UI item that initiates the navigation is correctly identified or named to give some indication that invoking it will result in a context change and page reload.

  > [!NOTE]
  > If you do refresh content within a region, consider setting the [**AccessibilityProperties.LiveSetting**](/uwp/api/windows.ui.xaml.automation.automationproperties.livesettingproperty) accessibility property on that element to one of the non-default settings **Polite** or **Assertive**. Some assistive technologies can map this setting to the Accessible Rich Internet Applications (ARIA) concept of live regions and can thus inform the user that a region of content has changed.

* **Don’t use UI elements that flash more than three times per second.** Flashing elements can cause some people to have seizures. It is best to avoid using UI elements that flash.
* **Don’t change user context or activate functionality automatically.** Context or activation changes should occur only when the user takes a direct action on a UI element that has focus. Changes in user context include changing focus, displaying new content, and navigating to a different page. Making context changes without involving the user can be disorienting for users who have disabilities. The exceptions to this requirement include displaying submenus, validating forms, displaying help text in another control, and changing context in response to an asynchronous event.

<span id="related_topics"/>

## Related topics  
* [Accessibility](accessibility.md)
* [Accessibility in the Store](accessibility-in-the-store.md)
* [Accessibility checklist](accessibility-checklist.md)