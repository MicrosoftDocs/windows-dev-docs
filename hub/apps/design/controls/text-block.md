---
ms.assetid: DA562509-D893-425A-AAE6-B2AE9E9F8A19
description: Text block is the primary control for displaying read-only text in apps.
title: Text block
label: Text block
template: detail.hbs
ms.date: 02/04/2025
ms.topic: article
doc-status: Published
ms.localizationpriority: medium
no-loc: [TextBlock, Text, Inline, Inlines, Bold, Italic, Run, Span, LineBreak]
---

# Text block

Text block is the primary control for displaying read-only text in apps. You can use it to display single-line or multi-line text, inline hyperlinks, and text with formatting like bold, italic, or underlined.

## Is this the right control?

A text block is typically easier to use and provides better text rendering performance than a rich text block, so it's preferred for most app UI text. You can easily access and use text from a text block in your app by getting the value of the [Text](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.textblock.text) property. It also provides many of the same formatting options for customizing how your text is rendered.

Although you can put line breaks in the text, text block is designed to display a single paragraph and doesn't support text indentation. Use a **RichTextBlock** when you need support for multiple paragraphs, multi-column text or other complex text layouts, or inline UI elements like images.

For more info about choosing the right text control, see the [Text controls](text-controls.md) article.

## Create a text block

> [!div class="checklist"]
>
> - **Important APIs:** [TextBlock class](/windows/windows-app-sdk/api/winrt/microsoft.UI.Xaml.Controls.TextBlock), [Text property](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.textblock.text), [Inlines property](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.textblock.inlines)

> [!div class="nextstepaction"]
> [Open the WinUI 3 Gallery app and see the TextBlock in action](winui3gallery:/item/TextBlock)

[!INCLUDE [winui-3-gallery](../../../includes/winui-3-gallery.md)]

Here's how to define a simple TextBlock control and set its `Text` property to a string.

```xaml
<TextBlock Text="Hello, world!" />
```

```csharp
TextBlock textBlock1 = new TextBlock();
textBlock1.Text = "Hello, world!";
```

### Content model

There are two properties you can use to add content to a TextBlock: [Text](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.textblock.text) and [Inlines](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.textblock.inlines).

The most common way to display text is to set the `Text` property to a string value, as shown in the previous example.

You can also add content by placing inline flow content elements in the `Inlines` property, like this. (`Inlines` is the default content property of a TextBlock, so you don't need to explicitly add it in XAML.)

```xaml
<TextBlock>Text can be <Bold>bold</Bold>, <Underline>underlined</Underline>, 
    <Italic>italic</Italic>, or a <Bold><Italic>combination</Italic></Bold>.</TextBlock>
```

Elements derived from the [Inline](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.documents.inline) class, such as [Bold](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.documents.bold), [Italic](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.documents.italic), [Run](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.documents.run), [Span](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.documents.span), and [LineBreak](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.documents.linebreak), enable different formatting for different parts of the text. For more info, see the [Formatting text](#formatting-text) section. The inline [Hyperlink](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.documents.hyperlink) element lets you add a hyperlink to your text. However, using `Inlines` also disables fast path text rendering, which is discussed in the next section.

## Performance considerations

Whenever possible, XAML uses a more efficient code path to layout text. This fast path both decreases overall memory use and greatly reduces the CPU time to do text measuring and arranging. This fast path applies only to TextBlock, so it should be preferred when possible over [RichTextBlock](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.richtextblock).

Certain conditions require TextBlock to fall back to a more feature-rich and CPU intensive code path for text rendering. To keep text rendering on the fast path, be sure to follow these guidelines when setting the properties listed here.

- [Text](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.textblock.text): The most important condition is that the fast path is used only when you set text by explicitly setting the `Text` property, either in XAML or in code (as shown in the previous examples). Setting the text via TextBlock's `Inlines` collection (such as `<TextBlock>Inline text</TextBlock>`) will disable the fast path, due to the potential complexity of multiple formats.
- [CharacterSpacing](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.textblock.characterspacing): Only the default value of 0 is fast path.
- [TextTrimming](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.textblock.texttrimming): Only the `None`, `CharacterEllipsis`, and `WordEllipsis` values are fast path. The `Clip` value disables the fast path.

> [!NOTE]
> **UWP Only:**
> Prior to Windows 10, version 1607, additional properties also affect the fast path. If your app is run on an earlier version of Windows, these conditions will cause your text to render on the slow path. For more info about versions, see [Version adaptive code](/windows/uwp/debug-test-perf/version-adaptive-code).
>
> - [Typography](/uwp/api/Windows.UI.Xaml.Documents.Typography): Only the default values for the various `Typography` properties are fast path.
> - [LineStackingStrategy](/uwp/api/windows.ui.xaml.controls.textblock.linestackingstrategy): If [LineHeight](/uwp/api/windows.ui.xaml.controls.textblock.lineheight) is not 0, the `BaselineToBaseline` and `MaxHeight` values disable the fast path.
> - [IsTextSelectionEnabled](/uwp/api/windows.ui.xaml.controls.textblock.istextselectionenabled): Only `false` is fast path. Setting this property to `true` disables the fast path.

You can set the [DebugSettings.IsTextPerformanceVisualizationEnabled](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.debugsettings.istextperformancevisualizationenabled) property to `true` during debugging to determine whether text is using fast path rendering. When this property is set to `true`, the text that is on the fast path displays in a bright green color.

You typically set debug settings in the [OnLaunched](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.application.onlaunched) method override in the code-behind page for `App.xaml`, like this.

```csharp
protected override void OnLaunched(LaunchActivatedEventArgs e)
{
#if DEBUG
    if (System.Diagnostics.Debugger.IsAttached)
    {
        this.DebugSettings.IsTextPerformanceVisualizationEnabled = true;
    }
#endif

// ...

}
```

In this example, the first TextBlock is rendered using the fast path, while the second is not.

```xaml
<StackPanel>
    <TextBlock Text="This text is on the fast path."/>
    <TextBlock>This text is NOT on the fast path.</TextBlock>
<StackPanel/>
```

When you run this XAML in debug mode with [IsTextPerformanceVisualizationEnabled](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.debugsettings.istextperformancevisualizationenabled) set to `true`, the result looks like this.

![Text rendered in debug mode](images/text-block-rendering-performance.png)

> [!CAUTION]
> The color of text that is not on the fast path is not changed. If you have text in your app with its color specified as bright green, it is still displayed in bright green when it's on the slower rendering path. Be careful to not confuse text that is set to green in the app with text that is on the fast path and green because of the debug settings.

## Formatting text

Although the [Text](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.textblock.text) property stores plain text, you can apply various formatting options to the TextBlock control to customize how the text is rendered in your app. You can set standard control properties like [FontFamily](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.textblock.fontfamily), [FontSize](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.textblock.fontsize), [FontStyle](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.textblock.fontstyle), [Foreground](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.textblock.foreground), and [CharacterSpacing](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.textblock.characterspacing) to change the look of the text. You can also use inline text elements and [Typography](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.documents.typography) attached properties to format your text. These options affect only how the TextBlock displays the text locally, so if you copy and paste the text into a rich text control, for example, no formatting is applied.

> [!NOTE]
> Remember, as noted in the previous section, inline text elements and non-default typography values are not rendered on the fast path.

### Inline elements

The [Microsoft.UI.Xaml.Documents](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.documents) namespace provides a variety of inline text elements that you can use to format your text, such as [Bold](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.documents.bold), [Italic](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.documents.italic), [Run](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.documents.run), [Span](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.documents.span), and [LineBreak](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.documents.linebreak).

You can display a series of strings in a TextBlock, where each string has different formatting. You can do this by using a `Run` element to display each string with its formatting and by separating each `Run` element with a `LineBreak` element.

Here's how to define several differently formatted text strings in a TextBlock by using `Run` objects separated with a `LineBreak`.

```xaml
<TextBlock FontFamily="Segoe UI" Width="400" Text="Sample text formatting runs">
    <LineBreak/>
    <Run Foreground="Gray" FontFamily="Segoe UI Light" FontSize="24">
        Segoe UI Light 24
    </Run>
    <LineBreak/>
    <Run Foreground="Teal" FontFamily="Georgia" FontSize="18" FontStyle="Italic">
        Georgia Italic 18
    </Run>
    <LineBreak/>
    <Run Foreground="Black" FontFamily="Arial" FontSize="14" FontWeight="Bold">
        Arial Bold 14
    </Run>
</TextBlock>
```

Here's the result.

![Text formatted with run elements](images/text-block-run-examples.png)

### Typography

The attached properties of the [Typography](/windows/windows-app-sdk/api/winrt/microsoft.UI.Xaml.Documents.Typography) class provide access to a set of Microsoft OpenType typography properties. You can set these attached properties either on the TextBlock, or on individual inline text elements. These examples show both.

```xaml
<TextBlock Text="Hello, world!"
           Typography.Capitals="SmallCaps"
           Typography.StylisticSet4="True"/>
```

```csharp
TextBlock textBlock1 = new TextBlock();
textBlock1.Text = "Hello, world!";
Typography.SetCapitals(textBlock1, FontCapitals.SmallCaps);
Typography.SetStylisticSet4(textBlock1, true);
```

```xaml
<TextBlock>12 x <Run Typography.Fraction="Slashed">1/3</Run> = 4.</TextBlock>
```

## UWP and WinUI 2

[!INCLUDE [uwp-winui2-note](../../../includes/uwp-winui-2-note.md)]

APIs for this control exist in the [Windows.UI.Xaml.Controls](/uwp/api/Windows.UI.Xaml.Controls) namespace.

> [!div class="checklist"]
>
> - **UWP APIs:** [TextBlock class](/uwp/api/Windows.UI.Xaml.Controls.TextBlock), [Text property](/uwp/api/windows.ui.xaml.controls.textblock.text), [Inlines property](/uwp/api/windows.ui.xaml.controls.textblock.inlines)
> - [Open the WinUI 2 Gallery app and see the TextBlock in action](winui2gallery:/item/TextBlock). [!INCLUDE [winui-2-gallery](../../../includes/winui-2-gallery.md)]

We recommend using the latest [WinUI 2](../../winui/winui2/index.md) to get the most current styles, templates, and features for all controls.

## Related articles

- [Text controls](text-controls.md)
- [TextBox class](/windows/windows-app-sdk/api/winrt/microsoft.UI.Xaml.Controls.TextBox)
- [PasswordBox class](/windows/windows-app-sdk/api/winrt/microsoft.UI.Xaml.Controls.PasswordBox)
- [String.Length property](/dotnet/api/system.string.length)