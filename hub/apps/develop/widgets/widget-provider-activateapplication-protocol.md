---
title: Widget provider ActivateApplication protocol
description: Describes the format of the command line activation parameters for widgets 
ms.topic: article
ms.date: 09/27/2022
ms.localizationpriority: medium
---

# Widget provider ActivateApplication protocol

This article describes the format of the command line activation parameters for widgets providers that use the **ActivateApplication** activation type.

> [!IMPORTANT]
> Widget providers specify an activation method in the Widget provider manifest file as described in the [Widget provider package manifest XML format](widget-provider-manifest.md). It is recommended that widget providers use the **CreateInstance** activation type and respond to widget host requests using the **IWidgetProvider** interface methods instead of using the **ActivateApplication** activation method. The information in this article is provided for completeness and is not recommended for use by most widget provider implementations.

## Arguments string in ActivateApplication and base64url encoding

When the widget provider is activated the command line will have `--widget-call=` prefixed before the [base64url](https://datatracker.ietf.org/doc/html/rfc4648#section-5) encoding in the command line arguments.

`--widget-call=[base64url]`

For example, the base64url part in

`--widget-call=ew0KICAgICJXaWRnZXRDYWxsIjoiQ3JlYXRlV2lkZ2V0IiwNCiAgICAiV2lkZ2V0Q29udGV4dCI6ew0KICAgICAgICAiSWQiOiI5ODU4MjEwOS1jNmJmLTQzNzItODlkNi04OWY1N2ViNzU0ZjYiLA0KICAgICAgICAiRGVmaW5pdGlvbk5hbWUiOiJQV0FfQ291bnRpbmdfV2lkZ2V0IiwNCiAgICAgICAgIlNpemUiOiJMYXJnZSINCiAgICB9DQp9`

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

The Json command line encodes calls to the `IWidgetProvider` methods:

```c++
    interface IWidgetProvider
    {
        void CreateWidget(WidgetContext widgetContext);
        void DeleteWidget(String widgetId);
        void OnActionInvoked(WidgetCallInvokedArgs actionInvokedArgs);
        void OnWidgetContextChanged(WidgetContextChangedArgs contextChangedArgs);
    }
```

Each activation represents one method call. The Json object has a `WidgetCall` value with the method name,
and then a value for each parameter, named as the parameter name except capitalized. Each parameter is mapped as a pair<variable name, value> to the json object. For example, for the `WidgetContext widgetContext` parameter of the `CreateWidget` method,
the `WidgetContext` is defined like this:

```c++
    runtimeclass WidgetContext
    {
        String Id { get; };
        String DefinitionId{ get; };
        String Size { get; };
    };
```

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

The object will always include the `WidgetCall` value and all parameter values as specified by the `IWidgetProvider` method.


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

#### OnWidgetContextChanged Json Object
For example WidgetSize is changed to Medium. In SV2, WidgetSize is the only thing which trig the WidgetContextChanged.

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