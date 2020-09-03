---
ms.assetid: DA562509-D893-425A-AAE6-B2AE9E9F8A19
description: Text block is the primary control for displaying read-only text in apps.
title: Text block
label: Text block
template: detail.hbs
ms.date: 05/19/2017
ms.topic: article
keywords: windows 10, uwp
pm-contact: miguelrb
design-contact: ksulliv
doc-status: Published
ms.localizationpriority: medium
---

# Text block

Text block is the primary control for displaying read-only text in apps. You can use it to display single-line or multi-line text, inline hyperlinks, and text with formatting like bold, italic, or underlined.
 
 > **Platform APIs**: [TextBlock class](/uwp/api/Windows.UI.Xaml.Controls.TextBlock), [Text property](/uwp/api/windows.ui.xaml.controls.textblock.text), [Inlines property](/uwp/api/windows.ui.xaml.controls.textblock.inlines)

## Is this the right control?

A text block is typically easier to use and provides better text rendering performance than a rich text block, so it's preferred for most app UI text. You can easily access and use text from a text block in your app by getting the value of the [Text](/uwp/api/windows.ui.xaml.controls.textblock.text) property. It also provides many of the same formatting options for customizing how your text is rendered.

Although you can put line breaks in the text, text block is designed to display a single paragraph and doesn't support text indentation. Use a **RichTextBlock** when you need support for multiple paragraphs, multi-column text or other complex text layouts, or inline UI elements like images.

For more info about choosing the right text control, see the [Text controls](text-controls.md) article.

## Examples

<table>
<th align="left">XAML Controls Gallery<th>
<tr>
<td><img src="images/xaml-controls-gallery-app-icon-sm.png" alt="XAML controls gallery"></img></td>
<td>
    <p>If you have the <strong style="font-weight: semi-bold">XAML Controls Gallery</strong> app installed, click here to <a href="xamlcontrolsgallery:/item/TextBlock">open the app and see the TextBlock in action</a>.</p>
    <ul>
    <li><a href="https://www.microsoft.com/store/productId/9MSVH128X2ZT">Get the XAML Controls Gallery app (Microsoft Store)</a></li>
    <li><a href="https://github.com/Microsoft/Xaml-Controls-Gallery">Get the source code (GitHub)</a></li>
    </ul>
</td>
</tr>
</table>

## Create a text block

Here's how to define a simple TextBlock control and set its Text property to a string.

```xaml
<TextBlock Text="Hello, world!" />
```

```csharp
TextBlock textBlock1 = new TextBlock();
textBlock1.Text = "Hello, world!";
```

### Content model

There are two properties you can use to add content to a TextBlock: [Text](/uwp/api/windows.ui.xaml.controls.textblock.text) and [Inlines](/uwp/api/windows.ui.xaml.controls.textblock.inlines).

The most common way to display text is to set the Text property to a string value, as shown in the previous example.

You can also add content by placing inline flow content elements in the TextBox.Inlines property, like this.
```xaml
<TextBlock>Text can be <Bold>bold</Bold>, <Underline>underlined</Underline>, 
    <Italic>italic</Italic>, or a <Bold><Italic>combination</Italic></Bold>.</TextBlock>
```

Elements derived from the Inline class, such as Bold, Italic, Run, Span, and LineBreak, enable different formatting for different parts of the text. For more info, see the [Formatting text](#formatting-text) section. The inline Hyperlink element lets you add a hyperlink to your text. However, using Inlines also disables fast path text rendering, which is discussed in the next section.


## Performance considerations

Whenever possible, XAML uses a more efficient code path to layout text. This fast path both decreases overall memory use and greatly reduces the CPU time to do text measuring and arranging. This fast path applies only to TextBlock, so it should be preferred when possible over RichTextBlock.

Certain conditions require TextBlock to fall back to a more feature-rich and CPU intensive code path for text rendering. To keep text rendering on the fast path, be sure to follow these guidelines when setting the properties listed here.
- [Text](/uwp/api/windows.ui.xaml.controls.textblock.text): The most important condition is that the fast path is used only when you set text by explicitly setting the Text property, either in XAML or in code (as shown in the previous examples). Setting the text via TextBlock's Inlines collection (such as `<TextBlock>Inline text</TextBlock>`) will disable the fast path, due to the potential complexity of multiple formats.
- [CharacterSpacing](/uwp/api/windows.ui.xaml.controls.textblock.characterspacing): Only the default value of 0 is fast path.
- [TextTrimming](/uwp/api/windows.ui.xaml.controls.textblock.texttrimming): Only the **None**, **CharacterEllipsis**, and **WordEllipsis** values are fast path. The **Clip** value disables the fast path.

> **Note**&nbsp;&nbsp;Prior to Windows 10, version 1607, additional properties also affect the fast path. If your app is run on an earlier version of Windows, these conditions will also cause your text to render on the slow path. For more info about versions, see [Version adaptive code](/windows/uwp/debug-test-perf/version-adaptive-code).
- [Typography](/uwp/api/Windows.UI.Xaml.Documents.Typography): Only the default values for the various Typography properties are fast path.
- [LineStackingStrategy](/uwp/api/windows.ui.xaml.controls.textblock.linestackingstrategy): If [LineHeight](/uwp/api/windows.ui.xaml.controls.textblock.lineheight) is not 0, the **BaselineToBaseline** and **MaxHeight** values disable the fast path.
- [IsTextSelectionEnabled](/uwp/api/windows.ui.xaml.controls.textblock.istextselectionenabled): Only **false** is fast path. Setting this property to **true** disables the fast path.

You can set the [DebugSettings.IsTextPerformanceVisualizationEnabled](/uwp/api/windows.ui.xaml.debugsettings.istextperformancevisualizationenabled) property to **true** during debugging to determine whether text is using fast path rendering. When this property is set to true, the text that is on the fast path displays in a bright green color.

>**Tip**&nbsp;&nbsp;This feature is explained in depth in this session from Build 2015- [XAML Performance: Techniques for Maximizing Universal Windows App Experiences Built with XAML](https://channel9.msdn.com/Events/Build/2015/3-698).



You typically set debug settings in the [OnLaunched](/uwp/api/windows.ui.xaml.application.onlaunched) method override in the code-behind page for App.xaml, like this.
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

When you run this XAML in debug mode with IsTextPerformanceVisualizationEnabled set to true, the result looks like this.

![Text rendered in debug mode](images/text-block-rendering-performance.png)

>**Caution**&nbsp;&nbsp;The color of text that is not on the fast path is not changed. If you have text in your app with its color specified as bright green, it is still displayed in bright green when it's on the slower rendering path. Be careful to not confuse text that is set to green in the app with text that is on the fast path and green because of the debug settings.

## Formatting text

Although the Text property stores plain text, you can apply various formatting options to the TextBlock control to customize how the text is rendered in your app. You can set standard control properties like FontFamily, FontSize, FontStyle, Foreground, and CharacterSpacing to change the look of the text. You can also use inline text elements and Typography attached properties to format your text. These options affect only how the TextBlock displays the text locally, so if you copy and paste the text into a rich text control, for example, no formatting is applied.

>**Note**&nbsp;&nbsp;Remember, as noted in the previous section, inline text elements and non-default typography values are not rendered on the fast path.


### Inline elements

The [Windows.UI.Xaml.Documents](/uwp/api/Windows.UI.Xaml.Documents) namespace provides a variety of inline text elements that you can use to format your text, such as Bold, Italic, Run, Span, and LineBreak.

You can display a series of strings in a TextBlock, where each string has different formatting. You can do this by using a Run element to display each string with its formatting and by separating each Run element with a LineBreak element.

Here's how to define several differently formatted text strings in a TextBlock by using Run objects separated with a LineBreak.
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

The attached properties of the [Typography](/uwp/api/Windows.UI.Xaml.Documents.Typography) class provide access to a set of Microsoft OpenType typography properties. You can set these attached properties either on the TextBlock, or on individual inline text elements. These examples show both.
```xaml
<TextBlock Text="Hello, world!"
           Typography.Capitals="SmallCaps"
           Typography.StylisticSet4="True"/>
```

```csharp
TextBlock textBlock1 = new TextBlock();
textBlock1.Text = "Hello, world!";
Windows.UI.Xaml.Documents.Typography.SetCapitals(textBlock1, FontCapitals.SmallCaps);
Windows.UI.Xaml.Documents.Typography.SetStylisticSet4(textBlock1, true);
```

```xaml
<TextBlock>12 x <Run Typography.Fraction="Slashed">1/3</Run> = 4.</TextBlock>
```

## Get the sample code

- [XAML Controls Gallery sample](https://github.com/Microsoft/Xaml-Controls-Gallery) - See all the XAML controls in an interactive format.

## Related articles

- [Text controls](text-controls.md)
- [Guidelines for spell checking](text-controls.md)
- [Adding search](search.md)
- [Guidelines for text input](text-controls.md)
- [TextBox class](/uwp/api/Windows.UI.Xaml.Controls.TextBox)
- [Windows.UI.Xaml.Controls PasswordBox class](/uwp/api/Windows.UI.Xaml.Controls.PasswordBox)
- [String.Length property](/dotnet/api/system.string.length)