---
Description: The following article describes all of the properties and elements within the toast content XML payload.
title: Toast content XML schema
ms.assetid: AF49EFAC-447E-44C3-93C3-CCBEDCF07D22
label: Toast content XML schema
template: detail.hbs
ms.date: 05/19/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Toast content XML schema

 

The following describes all of the properties and elements within the toast content XML payload.

In the following XML schemas, a "?" suffix means that an attribute is optional.

## &lt;visual&gt; and &lt;audio&gt;

```
<toast launch? duration? activationType? scenario? >
  <visual lang? baseUri? addImageQuery? >
    <binding template? lang? baseUri? addImageQuery? >
      <text lang? hint-maxLines? >content</text>
      <image src placement? alt? addImageQuery? hint-crop? />
      <group>
        <subgroup hint-weight? hint-textStacking? >
          <text />
          <image />
        </subgroup>
      </group>
    </binding>
  </visual>
  <audio src? loop? silent? />
</toast>
```

**Attributes in &lt;toast&gt;**

launch?

-   launch? = string
-   This is an optional attribute.
-   A string that is passed to the application when it is activated by the toast.
-   Depending on the value of activationType, this value can be received by the app in the foreground, inside the background task, or by another app that's protocol launched from the original app.
-   The format and contents of this string are defined by the app for its own use.
-   When the user taps or clicks the toast to launch its associated app, the launch string provides the context to the app that allows it to show the user a view relevant to the toast content, rather than launching in its default way.
-   If the activation is happened because user clicked on an action, instead of the body of the toast, the developer retrieves back the "arguments" pre-defined in that &lt;action&gt; tag, instead of "launch" pre-defined in the &lt;toast&gt; tag.

duration?

-   duration? = "short|long"
-   This is an optional attribute. Default value is "short".
-   This is only here for specific scenarios and appCompat. You don't need this for the alarm scenario anymore.
-   We don't recommend using this property.

activationType?

-   activationType? = "foreground | background | protocol | system"
-   This is an optional attribute.
-   The default value is "foreground".

scenario?

-   scenario? = "default | alarm | reminder | incomingCall"
-   This is an optional attribute, default value is "default".
-   You do not need this unless your scenario is to pop an alarm, reminder, or incoming call.
-   Do not use this just for keeping your notification persistent on screen.

**Attributes in &lt;visual&gt;**

lang?

-   See [this element schema article](/uwp/schemas/tiles/toastschema/element-visual) for details on this optional attribute.

baseUri?

-   See [this element schema article](/uwp/schemas/tiles/toastschema/element-visual) for details on this optional attribute.

addImageQuery?

-   See [this element schema article](/uwp/schemas/tiles/toastschema/element-visual) for details on this optional attribute.

**Attributes in &lt;binding&gt;**

template?

-   \[Important\] template? = "ToastGeneric"
-   If you are using any of the new adaptive and interactive notification features, please make sure you start using "ToastGeneric" template instead of the legacy template.
-   Using the legacy templates with the new actions might work now, but that is not the intended use case, and we cannot guarantee that will continue working.

lang?

-   See [this element schema article](/uwp/schemas/tiles/toastschema/element-visual) for details on this optional attribute.

baseUri?

-   See [this element schema article](/uwp/schemas/tiles/toastschema/element-visual) for details on this optional attribute.

addImageQuery?

-   See [this element schema article](/uwp/schemas/tiles/toastschema/element-visual) for details on this optional attribute.

**Attributes in &lt;text&gt;**

lang?

-   See [this element schema article](/uwp/schemas/tiles/toastschema/element-visual) for details on this optional attribute.

**Attributes in &lt;image&gt;**

src

-   See [this element schema article](/uwp/schemas/tiles/toastschema/element-image) for details on this required attribute.

placement?

-   placement? = "inline" | "appLogoOverride"
-   This attribute is optional.
-   This specifies where this image will be displayed.
-   "inline" means inside the toast body, below the text; "appLogoOverride" means replace the application icon (that shows up on the top left corner of the toast).
-   You can have up to one image for each placement value.

alt?

-   See [this element schema article](/uwp/schemas/tiles/toastschema/element-image) for details on this optional attribute.

addImageQuery?

-   See [this element schema article](/uwp/schemas/tiles/toastschema/element-image) for details on this optional attribute.

hint-crop?

-   hint-crop? = "none" | "circle"
-   This attribute is optional.
-   "none" is the default value which means no cropping.
-   "circle" crops the image to a circular shape. Use this for profile images of a contact, images of a person, and so on.

**Attributes in &lt;audio&gt;**

src?

-   See [this element schema article](/uwp/schemas/tiles/toastschema/element-audio) for details on this optional attribute.

loop?

-   See [this element schema article](/uwp/schemas/tiles/toastschema/element-audio) for details on this optional attribute.

silent?

-   See [this element schema article](/uwp/schemas/tiles/toastschema/element-audio) for details on this optional attribute.

## Schemas: &lt;action&gt;


In the following XML schemas, a "?" suffix means that an attribute is optional.

```
<toast>
  <visual>
  </visual>
  <audio />
  <actions>
    <input id type title? placeHolderContent? defaultInput? >
      <selection id content />
    </input>
    <action content arguments activationType? imageUri? hint-inputId />
  </actions>
</toast>
```

**Attributes in &lt;input&gt;**

id

-   id = string
-   This attribute is required.
-   The id attribute is required and is used by developers to retrieve user inputs once the app is activated (in the foreground or background).

type

-   type = "text | selection"
-   This attribute is required.
-   It is used to specify a text input or input from a list of pre-defined selections.
-   On mobile and desktop, this is to specify whether you want a textbox input or a listbox input.

title?

-   title? = string
-   The title attribute is optional and is for developers to specify a title for the input for shells to render when there is affordance.
-   For mobile and desktop, this title will be displayed above the input.

placeHolderContent?

-   placeHolderContent? = string
-   The placeHolderContent attribute is optional and is the grey-out hint text for text input type. This attribute is ignored when the input type is not "text".

defaultInput?

-   defaultInput? = string
-   The defaultInput attribute is optional and is used to provide a default input value.
-   If the input type is "text", this will be treated as a string input.
-   If the input type is "selection", this is expected to be the id of one of the available selections inside this input's elements.

**Attributes in &lt;selection&gt;**

id

-   This attribute is required. It's used to identify user selections. The id is returned to your app.

content

-   This attribute is required. It provides the string to display for this selection element.

**Attributes in &lt;action&gt;**

content

-   content = string
-   The content attribute is required. It provides the text string displayed on the button.

arguments

-   arguments = string
-   The arguments attribute it required. It describes the app-defined data that the app can later retrieve once it is activated from user taking this action.

activationType?

-   activationType? = "foreground | background | protocol | system"
-   The activationType attribute is optional and its default value is "foreground".
-   It describes the kind of activation this action will cause: foreground, background, or launching another app via protocol launch, or invoking a system action.

imageUri?

-   imageUri? = string
-   imageUri is optional and is used to provide an image icon for this action to display inside the button alone with the text content.

hint-inputId

-   hint-inputId = string
-   The hint-inpudId attribute is required. It's specifically used for the quick reply scenario.
-   The value needs to be the id of the input element desired to be associated with.
-   In mobile and desktop, this will put the button right next to the input box.

## Attributes for system-handled actions


The system can handle actions for snoozing and dismissing notifications if you don't want your app to handle the snoozing/rescheduling of notifications as a background task. System-handled actions can be combined (or individually specified), but we don't recommend implementing a snooze action without a dismiss action.

System commands combo: SnoozeAndDismiss

```
<toast>
  <visual>
  </visual>
  <actions hint-systemCommands="SnoozeAndDismiss" />
</toast>
```

Individual system-handled actions

```
<toast>
  <visual>
  </visual>
  <actions>
  <input id="snoozeTime" type="selection" defaultInput="10">
    <selection id="5" content="5 minutes" />
    <selection id="10" content="10 minutes" />
    <selection id="20" content="20 minutes" />
    <selection id="30" content="30 minutes" />
    <selection id="60" content="1 hour" />
  </input>
  <action activationType="system" arguments="snooze" hint-inputId="snoozeTime" content=""/>
  <action activationType="system" arguments="dismiss" content=""/>
  </actions>
</toast>
```

To construct individual snooze and dismiss actions, do the following:

-   Specify activationType = "system"
-   Specify arguments = "snooze" | "dismiss"
-   Specify content:
    -   If you want localized strings of "snooze" and "dismiss" to be displayed on the actions, specify content to be an empty string: &lt;action content = ""/&gt;
    -   If you want a customized string, just provide its value: &lt;action content="Remind me later" /&gt;
-   Specify input:
    -   If you don't want the user to select a snooze interval and instead just want your notification to snooze only once for a system-defined time interval (that is consistent across the OS), then don't construct any &lt;input&gt; at all.
    -   If you want to provide snooze interval selections:
        -   Specify hint-inputId in the snooze action
        -   Match the id of the input with the hint-inputId of the snooze action: &lt;input id="snoozeTime"&gt;&lt;/input&gt;&lt;action hint-inputId="snoozeTime"/&gt;
        -   Specify selection id to be a nonNegativeInteger which represents snooze interval in minutes: &lt;selection id="240" /&gt; means snoozing for 4 hours
        -   Make sure that the value of defaultInput in &lt;input&gt; matches with one of the ids of the &lt;selection&gt; children elements
        -   Provide up to (but no more than) 5 &lt;selection&gt; values