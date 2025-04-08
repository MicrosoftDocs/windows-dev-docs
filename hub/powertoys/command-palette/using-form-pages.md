---
title: Get user input with forms
description: Learn how to use Adaptive Cards forms in your Command Palette extension.
ms.date: 3/23/2025
ms.topic: how-to
no-loc: [PowerToys, Windows, Insider]
# Customer intent: As a Windows developer, I want to learn how to develop an extension for the Command Palette.
---

# Get user input with forms

**Previous**: [Display markdown content](using-markdown-content.md)

Now that we know how to present basic markdown content, let's try displaying something more elaborate by leveraging the power of [Adaptive Cards](https://adaptivecards.io/). This is useful for creating forms, or for displaying more complex content.

## Working with forms

Command Palette supports Adaptive Cards, which are a way to create rich, interactive content. This can be useful for creating forms, or for displaying more complex content.

You can create a card in the Command Palette with the **IFormContent** interface (see [FormContent](./microsoft-commandpalette-extensions-toolkit/formcontent.md) for the toolkit implementation). This allows you to provide the Adaptive Card JSON, and the Command Palette will render it for you. When the user submits the form, Command Palette will call the **SubmitForm** method on your form, with the JSON payload and inputs from the form.

Adaptive card payloads can be created using the [Adaptive Card Designer](https://adaptivecards.io/designer/). You can design your card there, and then copy the JSON payload into your extension. 

For a full example of using Forms and Content pages, head on over to [`SamplePagesExtension/Pages/SampleContentPage.cs`](https://github.com/microsoft/PowerToys/blob/main/src/modules/cmdpal/Exts/SamplePagesExtension/Pages/SampleContentPage.cs). Some brief things to note:

- Set the **TemplateJson** property of your form to the JSON payload of your Adaptive Card. (this is the "CARD PAYLOAD EDITOR" value in the Adaptive Card Designer)
- Set the **DataJson** property of your **FormContent** to the data you want to use to fill in your card template. (This is the "SAMPLE DATA EDITOR" value in the Adaptive Card Designer). This is optional, but can make authoring cards easier.
- Implement the **SubmitForm** method to handle the form submission. This method will be called when the user submits the form, and will be passed the JSON payload of the form.

```csharp
public override CommandResult SubmitForm(string payload)
{
    var formInput = JsonNode.Parse(payload)?.AsObject();
    if (formInput == null)
    {
        return CommandResult.GoHome();
    }

    // retrieve the value of the input field with the id "name"
    var name = formInput["name"]?.AsString();
        
    // do something with the data

    // and eventually
    return CommandResult.GoBack();
}
```

Of course, you can mix and match **IContent** in whatever way you'd like. For example, you could use a block markdown first for the body of a post, and have a **FormContent** next to reply to the post.

## Related content

- [PowerToys Command Palette utility](overview.md)
- [Extensibility overview](extensibility-overview.md)
- [Extension samples](samples.md)
