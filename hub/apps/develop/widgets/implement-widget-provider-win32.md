---
title: Implement a widget provider in a win32 app
description: This article walks you through the process of creating a widget provider that provides widget content and responds to widget actions. 
ms.topic: article
ms.date: 07/06/2022
ms.author: drewbat
author: drewbatgit
ms.localizationpriority: medium
---

# Implement a widget provider in a win32 app

> [!NOTE]
> **Some information relates to pre-released product, which may be substantially modified before it's commercially released. Microsoft makes no warranties, express or implied, with respect to the information provided here.**
> [!IMPORTANT]
> The self-contained feature described in this topic is available only in Windows App SDK 1.2 Preview 1.

## Create a new win32 app

* Provide specific guidance about project template type and any required modifications (e.g. bootsrapper APIs)
* Specify project name / class name

## Update your app manifest to register your app as a widget provider

tbd

## Create a class that implements IWidgetProvider

Widget providers must implement the [IWidgetProvider](tbd) interface, which provides methods that are called by the system in response to different widget events such as the initial widget creation and user actions within the widget. To access the **IWidgetProvider** class, add the following using directives / includes

```cpp
TBD
```

Specify that your class implements the [IWidgetProvider](tbd) interface.

```cpp
class MyWidgetProvider : public winrt::implements<MyWidgetProvider, IWidgetProvider>
{
...
}
```

## Implement the CreateWidget method

The [CreateWidget](tbd) method of the **IWidgetProvider** interface is called by the system when a widget associated your app has been added to the widget board by the user. The [WidgetContext](tbd) object passed into this method provides the OS-assigned identifier for the widget. Save this unique ID so that you can identify the associated widget from future invocations of your widget provider. Call [UpdateWidget](tbd) to provide the initial content for your widget. **UpdateWidget** takes a [WidgetUpdateRequestOptions]() object as an argument. First, we set the **WidgetId** property to specify the widget instance to be updated. Next we set the **Template** property to a JSON string that defines the widget UI. Finally we set the **Data** property that contains the data to be bound to the template. The helper methods **GetCardTemplate** and **GetCardData** will be defined later in this article.

TBD - GetCardData (and later, GetCustomState) is so simple, should we move it inline or do we want to show a more complex example?
TBD - Would it be beneficial to show some simple mechanism for storing the ID, so the tutorial code runs out of the box?

```cpp
 // This method notifies your app that a Widget has been created. As soon as a Widget has
    // been created you can provide content and should prepare to handle actions for that widget.
    void CreateWidget(Microsoft.Widgets.WidgetContext widgetContext)
    {
        // You can store the widgetId in your app for future calls to WigdetProviderManager.UpdateWidget
        MySample::StoreWidgetId(widgetContext.WidgetId());

        // Finally, the WidgetProviderManager.UpdateWidget method can be called to provide
        // some content for the newly created widget. (UpdateWidget can alternatively and
        // also be called later)
        WidgetUpdateRequestOptions options;
        options.WidgetId(widgetContext.WidgetId());
        options.Template(GetCardTemplate(widgetContext.WidgetHostContext().Language()));
        options.Data(GetCardData(0));
        WidgetProviderManager::UpdateWidget(options);
    }
```

## Implement the DeleteWidget method

The [DeleteWidget](tbd) method of the **IWidgetProvider** interface notifies your app that a widget has been deleted. After a widget has been deleted your provider will not receive any other action handling requests for that widget and any responses from the widget provider app that use the deleted ID will be ignored by the system.

TBD - Would it be beneficial to show some simple mechanism for deleting the ID?

```cpp
void DeleteWidget(hstring widgetId, hstring customState)
{
    // Delete the widgetId from the known widgetIds list
    MySample::DeleteWidgetId(widgetId);
}
```

## Implement the HandleAction method

The [HandleAction](tbd) method of the **IWidgetProvider** interface. This method is invoked by the system when the user raises an action in one of your app's widgets, such as clicking a button.

The first parameter to **HandleAction** is a **WidgetContext** object that you can use to retrieve the widget ID that identifies the widget associated with the user action. 

The *verb* and *data* parameters passed into **HandleAction** are defined as part of the JSON template you provide to define your widget. Both parameters are members of the **Action.Execute** action type that is defined by the Adaptive Cards schema. For more information, see [Universal Action Model](/adaptive-cards/authoring-cards/universal-action-model). The *verb* parameter to is an app-defined string that identifies the user action that triggered the method call, and the *data* parameter contains the data associated with the action. 

The last parameter to **HandleAction** is an arbitrary app-defined string (TBD - Is this string validated in any way?) representing the custom state of your widget. This string is maintained by the widget board between invocations of the widget provider. This example uses the custom state to store a simple JSON string that contains a single integer, *count*. When **HandleAction** is raised, we parse the custom state and increment the count. In the call to **UpdateWidget** we update the custom state value. We also update the widget data with the new count value, which will cause the widget UI to be updated with the new value.  

TBD - Do we want to break custom state out into a separate step? I.e. Maybe we have a primary scenario that's grabbing updated data from a service and using state is an additional feature.

```cpp
// This method is called when the user raises an action in your widget, for example,
// after clicking a button.
void HandleAction(Microsoft.Widgets.WidgetContext widgetContext, hstring verb, hstring actionData, hstring customState)
{
    // If your app is not storing a local state you can use the customState information
    // stored in the widget provider manager.
    int count{};
    if (customState.size() != 0)
    {
        // Using the customState data we can retrieve the count
        JsonObject parsedInfo = JsonObject::Parse(customState);
        count = (int)parsedInfo.GetNamedNumber(L"totalCount");
    }

    // Every Action.Execute method contains a verb property which can be used as
    // extra metadata. The list of verbs is specified by the provider in its card.
    if (verb == L"increment")
    {
        ++count;
    }

    // Call the WidgetProviderManager.UpdateWidget method to update the card data with the new
    // counter value. As we don't want to update the template then we don't set the Template property.
    WidgetUpdateRequestOptions options;
    options.WidgetId(widgetContext.WidgetId());
    options.Data(GetCardData(count));
    options.CustomState(GetCustomState(count));
    WidgetProviderManager::UpdateWidget(options);
}

```

## Implement the HandleHostContextChange method

The [HandleHostContextChange](tbd) method of the **IWidgetProvider** interface. The system calls this method when the host context of the widget has been changed. For example, when the host app language configuration changes. In this example, the helper method **GetCardTemplate** accepts the host language as parameter, so when the context changes, the template is regenerated and then the widget is updated with a call to **UpdateWidget**

```cpp
// This method is called once for every widget when the host context has been changed, for example,
// when the host app language configuration has changed.
void HandleHostContextChange(Microsoft.Widgets.WidgetContext widgetContext)
{
    // Generate an updated template for the card with the new language and
    // update it in the Widget Provider Manager.
    WidgetUpdateRequestOptions options;
    options.WidgetId(widgetContext.WidgetId());
    options.Template(GetCardTemplate(widgetContext.WidgetHostContext().Language()));
    WidgetProviderManager::UpdateWidget(options);
}
```

## Helper methods

This section describes the helper methods used the code examples above.

The **GetCardTemplate** method returns a JSON string the defines the visual appearance of the widget, including visual elements such as images and text, as well as *actions* which represent user inputs such as buttons. For more information about the Adaptive Cards JSON syntax used to represent widgets, see the [Getting Started](/adaptive-cards/authoring-cards/getting-started) section of the [Adaptive Cards](/adaptive-cards) documentation.

As shown in the examples above, the **WidgetContext** object provides access to the language preference for the widgets board. The language is passed into **GetCardTemplate** which constructs the UI text for the widget in the appropriate language. 

Within the *text* field of the widget *body* we use the syntax `${count}` to indicate a value that will be replaced with a value from the data JSON string generated in the **GetCardData** helper method. By separating the visual formatting of the widget from the data, the Adaptive Card pattern allows you to update the data for your widget without having to update the visual template every time.

Under *actions*, an JSON object with a type of "Action.Execute" is defined to represent a button. The title text of the button is set to the localized value and the verb is set to "increment". The verb value is arbitrary and app-defined and is used to determine which UI element triggered the **HandleAction** method, as shown in the example above. 

```cpp
winrt::hstring GetCardTemplate(hstring language)
{
    hstring textBlockLocalizedString = L"You have clicked the button ${count} times";
    hstring buttonLocalizedString = L"Increment";

    if (language == "es")
    {
        textBlockLocalizedString = L"Has presionado el boton ${count} veces";
        buttonLocalizedString = L"Incrementar";
    }

    return LR"(
    {
        "type": "AdaptiveCard",
        "body": [
            {
                "type": "TextBlock",
                "text": ")" + textBlockLocalizedString + LR"("
            }
        ],
        "actions": [
            {
                "type": "Action.Execute",
                "title": ")" + buttonLocalizedString + LR"(",
                "verb": "increment"
            }
        ],
        "$schema": "http://adaptivecards.io/schemas/adaptive-card.json",
        "version": "1.5"
    })";
}
```

The **GetCardData** returns a JSON string the defines the data for the widget that is bound to the JSON template returned by the **GetCardTemplate** method. In this example, the widget board will substitue the value of the *count* field for the `${count}` syntax is the widget body text.

```cpp
winrt::hstring GetCardData(const int count)
{
    return L"{ \"count\": " + to_hstring(count) + L" }";
}
```

The **GetCustomState** method returns a JSON string that represents the widget's custom state. In this example, it maintains the current value of the counter that is incremented when the user clicks the **Increment** button.

```cpp
winrt::hstring GetCustomState(const int count)
{
    return L"{ \"totalCount\": " + to_hstring(count) + L " }";
}
```

