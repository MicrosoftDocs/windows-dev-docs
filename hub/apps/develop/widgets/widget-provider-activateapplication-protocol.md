---
title: Widget provider ActivateApplication protocol
description: Learn the command line activation parameters passed to a Windows widget provider app when it is activated by the Widgets Board.
ms.topic: article
ms.date: 10/23/2026
ms.localizationpriority: medium
author: GrantMeStrength
ms.author: jken
---

# Widget provider ActivateApplication protocol

This article describes the format of the command line activation parameters for widgets providers that use the **ActivateApplication** activation type.

> [!IMPORTANT]
> Widget providers specify an activation method in the Widget provider manifest file as described in the [Widget provider package manifest XML format](widget-provider-manifest.md). It is recommended that widget providers use the **CreateInstance** activation type and respond to widget host requests using the **IWidgetProvider** interface methods instead of using the **ActivateApplication** activation method. The information in this article is provided for completeness and is not recommended for use by most widget provider implementations.

## Arguments string in ActivateApplication and base64url encoding

When the widget provider is activated the command line will have `--widget-call=` prefixed before the [base64url](https://datatracker.ietf.org/doc/html/rfc4648#section-5) encoding in the command line arguments.

`--widget-call=[base64url]`

For example, the base64url part in

`--widget-call=ewogICAgIldpZGdldENhbGwiOiJDcmVhdGVXaWRnZXQiLAogICAgIldpZGdldENvbnRleHQiOnsKICAgICAgICAiSWQiOiI5ODU4MjEwOS1jNmJmLTQzNzItODlkNi04OWY1N2ViNzU0ZjYiLAogICAgICAgICJEZWZpbml0aW9uSWQiOiJQV0FfQ291bnRpbmdfV2lkZ2V0IiwKICAgICAgICAiU2l6ZSI6IkxhcmdlIgogICAgfQp9`

is decoded into

```json
{
    "WidgetCall":"CreateWidget",
    "WidgetContext":{
        "Id":"98582109-c6bf-4372-89d6-89f57eb754f6",
        "DefinitionId":"PWA_Counting_Widget",
        "Size":"Large"
    }
}
```

## Command line format details

The JSON command line represents calls corresponding to the [IWidgetProvider](/windows/windows-app-sdk/api/winrt/microsoft.windows.widgets.providers.iwidgetprovider) methods. The following pseudocode summarizes those methods; it isn't a compilable interface declaration.

```c++
    interface IWidgetProvider
    {
        void CreateWidget(WidgetContext widgetContext);
        void DeleteWidget(String widgetId, String customState);
        void OnActionInvoked(WidgetActionInvokedArgs actionInvokedArgs);
        void OnWidgetContextChanged(WidgetContextChangedArgs contextChangedArgs);
        void Activate(WidgetContext widgetContext);
        void Deactivate(String widgetId);
    }
```

Each activation represents one method call. The JSON object has a `WidgetCall` value with the method name and properties containing the call's data. The examples below show the transport property names, including `Args` for callback argument objects.

For a `WidgetContext`, the JSON contains `Id`, `DefinitionId`, and `Size`. `Id` identifies a widget instance, and `DefinitionId` identifies its manifest definition. The JSON represents `Size` as a string such as `"Large"`; the actual [WidgetContext.Size](/windows/windows-app-sdk/api/winrt/microsoft.windows.widgets.providers.widgetcontext.size) API property is a **WidgetSize** enumeration, not a string.

> [!Note]
>  Widget providers should ignore unexpected parameter values to handle the case where additional parameters are added in the future.

The widget provider `CreateWidget` API call is marshalled to Json object:

```json
{
    "WidgetCall":"CreateWidget",
    "WidgetContext":{
        "Id":"98582109-c6bf-4372-89d6-89f57eb754f6",
        "DefinitionId":"PWA_Counting_Widget",
        "Size":"Large"
    }
}
```

Use the JSON transport format shown here when handling **ActivateApplication** arguments, rather than treating the JSON as a WinRT interface declaration.


### Marshalled objects examples



#### Create Widget Json object

```json
{
    "WidgetCall":"CreateWidget",
    "WidgetContext":{
        "Id":"98582109-c6bf-4372-89d6-89f57eb754f6",
        "DefinitionId":"PWA_Counting_Widget",
        "Size":"Large"
    }
}
```

#### Delete Widget Json object

```json
{
  "WidgetId": "1AC74363-177B-4CD2-995F-3B25AEEA3FF4",
  "WidgetCall": "DeleteWidget",
  "CustomState": "usedata"
}
```

#### OnActionInvoked Json Object

```json
{
    "WidgetCall": "OnActionInvoked",
    "Args":{
        "Verb": "Verb String",
        "Data": "Data Details",
        "CustomState": "usedata",
        "WidgetContext": {
            "Id":"98582109-c6bf-4372-89d6-89f57eb754f6",
            "DefinitionId":"PWA_Counting_Widget",
            "Size":"Large"
        }
    }
}
```

#### Activate Json Object
```json
{
    "WidgetCall": "Activate",
    "WidgetContext": {
        "Id":"98582109-c6bf-4372-89d6-89f57eb754f6",
        "DefinitionId":"PWA_Counting_Widget",
        "Size":"Large"
    }
}
```

#### Deactivate Json Object
```json
{
    "WidgetCall": "Deactivate",
    "WidgetId":"98582109-c6bf-4372-89d6-89f57eb754f6"
}
```

#### OnWidgetContextChanged JSON Object
For example, when `WidgetSize` changes to `Medium`, a `WidgetContextChanged` event is triggered.

```json
{
  "WidgetCall": "OnWidgetContextChanged",
  "Args":{
    "WidgetContext":{
            "Id":"98582109-c6bf-4372-89d6-89f57eb754f6",
            "DefinitionId":"PWA_Counting_Widget",
            "Size":"Medium"
        }
  }
}
```