---
description: Use a RichTextBlock with RichTextBlockOverflow elements to create advanced text layouts.
title: RichTextBlock
ms.assetid: E4BE4B1B-418E-4075-88F1-22C09DDF8E45
label: Rich text block
template: detail.hbs
ms.date: 02/26/2025
ms.topic: article
doc-status: Published
ms.localizationpriority: medium
---
# Rich text block

Rich text blocks provide several features for advanced text layout that you can use when you need support for paragraphs, inline UI elements, or complex text layouts.

## Is this the right control?

Use a **RichTextBlock** when you need support for multiple paragraphs, multi-column or other complex text layouts, or inline UI elements like images.

Use a **TextBlock** to display most read-only text in your app. You can use it to display single-line or multi-line text, inline hyperlinks, and text with formatting like bold, italic, or underlined. TextBlock provides a simpler content model, so it's typically easier to use, and it can provide better text rendering performance than RichTextBlock. It's preferred for most app UI text. Although you can put line breaks in the text, TextBlock is designed to display a single paragraph and doesn't support text indentation.

For more info about choosing the right text control, see the [Text controls](text-controls.md) article.

## Recommendations

See Typography and Guidelines for fonts.

## Create a rich text block

> [!div class="checklist"]
>
> - **Important APIs:** [RichTextBlock class](/windows/windows-app-sdk/api/winrt/microsoft.UI.Xaml.Controls.RichTextBlock), [RichTextBlockOverflow class](/windows/windows-app-sdk/api/winrt/microsoft.UI.Xaml.Controls.RichTextBlockOverflow), [Paragraph class](/windows/windows-app-sdk/api/winrt/microsoft.UI.Xaml.Documents.Paragraph), [Typography class](/windows/windows-app-sdk/api/winrt/microsoft.UI.Xaml.Documents.Typography)

> [!div class="nextstepaction"]
> [Open the WinUI 3 Gallery app and see the RichTextBlock in action](winui3gallery:/item/RichTextBlock)

[!INCLUDE [winui-3-gallery](../../../includes/winui-3-gallery.md)]

The content property of RichTextBlock is the [Blocks](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.richtextblock.blocks) property, which supports paragraph based text via the [Paragraph](/windows/windows-app-sdk/api/winrt/microsoft.UI.Xaml.Documents.Paragraph) element. It doesn't have a **Text** property that you can use to easily access the control's text content in your app. However, RichTextBlock provides several unique features that TextBlock doesn't provide. 

RichTextBlock supports:
- Multiple paragraphs. Set the indentation for paragraphs by setting the [TextIndent](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.richtextblock.textindent) property.
- Inline UI elements. Use an [InlineUIContainer](/windows/windows-app-sdk/api/winrt/microsoft.UI.Xaml.Documents.InlineUIContainer) to display UI elements, such as images, inline with your text.
- Overflow containers. Use [RichTextBlockOverflow](/windows/windows-app-sdk/api/winrt/microsoft.UI.Xaml.Controls.RichTextBlockOverflow) elements to create multi-column text layouts.

### Paragraphs

You use [Paragraph](/windows/windows-app-sdk/api/winrt/microsoft.UI.Xaml.Documents.Paragraph) elements to define the blocks of text to display within a RichTextBlock control. Every RichTextBlock should include at least one Paragraph. 

You can set the indent amount for all paragraphs in a RichTextBlock by setting the [RichTextBlock.TextIndent](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.richtextblock.textindent) property. You can override this setting for specific paragraphs in a RichTextBlock by setting the [Paragraph.TextIndent](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.documents.paragraph.textindent) property to a different value.

```xaml
<RichTextBlock TextIndent="12">
  <Paragraph TextIndent="24">First paragraph.</Paragraph>
  <Paragraph>Second paragraph.</Paragraph>
  <Paragraph>Third paragraph. <Bold>With an inline.</Bold></Paragraph>
</RichTextBlock>
```

### Inline UI elements

The [InlineUIContainer](/windows/windows-app-sdk/api/winrt/microsoft.UI.Xaml.Documents.InlineUIContainer) class lets you embed any UIElement inline with your text. A common scenario is to place an Image inline with your text, but you can also use interactive elements, like a Button or CheckBox.

If you want to embed more than one element inline in the same position, consider using a panel as the single InlineUIContainer child, and then place the multiple elements within that panel.

This example shows how to use an InlineUIContainer to insert an image into a RichTextBlock. 

```xaml
<RichTextBlock>
    <Paragraph>
        <Italic>This is an inline image.</Italic>
        <InlineUIContainer>
            <Image Source="Assets/Square44x44Logo.png" Height="30" Width="30"/>
        </InlineUIContainer>
        Mauris auctor tincidunt auctor.
    </Paragraph>
</RichTextBlock>
```

## Overflow containers

You can use a RichTextBlock with [RichTextBlockOverflow](/windows/windows-app-sdk/api/winrt/microsoft.UI.Xaml.Controls.RichTextBlockOverflow) elements to create multi-column or other advanced page layouts. The content for a RichTextBlockOverflow element always comes from a RichTextBlock element. You link RichTextBlockOverflow elements by setting them as the OverflowContentTarget of a RichTextBlock or another RichTextBlockOverflow.

Here's a simple example that creates a two column layout. See the Examples section for a more complex example.

```xaml
<Grid>
    <Grid.ColumnDefinitions>
        <ColumnDefinition/>
        <ColumnDefinition/>
    </Grid.ColumnDefinitions>
    <RichTextBlock Grid.Column="0" 
                   OverflowContentTarget="{Binding ElementName=overflowContainer}" >
        <Paragraph>
            Proin ac metus at quam luctus ultricies.
        </Paragraph>
    </RichTextBlock>
    <RichTextBlockOverflow x:Name="overflowContainer" Grid.Column="1"/>
</Grid>
```

## Formatting text

Although the RichTextBlock stores plain text, you can apply various formatting options to customize how the text is rendered in your app. You can set standard control properties like FontFamily, FontSize, FontStyle, Foreground, and CharacterSpacing to change the look of the text. You can also use inline text elements and Typography attached properties to format your text. These options affect only how the RichTextBlock displays the text locally, so if you copy and paste the text into a rich text control, for example, no formatting is applied.

### Inline elements

The [Microsoft.UI.Xaml.Documents](/windows/windows-app-sdk/api/winrt/microsoft.UI.Xaml.Documents) namespace provides a variety of inline text elements that you can use to format your text, such as Bold, Italic, Run, Span, and LineBreak. A typical way to apply formatting to sections of text is to place the text in a Run or Span element, and then set properties on that element.

Here's a Paragraph with the first phrase shown in bold, blue, 16pt text.

```xaml
<Paragraph>
    <Bold><Span Foreground="DarkSlateBlue" FontSize="16">Lorem ipsum dolor sit amet</Span></Bold>
    , consectetur adipiscing elit.
</Paragraph>
```

### Typography

The attached properties of the [Typography](/windows/windows-app-sdk/api/winrt/microsoft.UI.Xaml.Documents.Typography) class provide access to a set of Microsoft OpenType typography properties. You can set these attached properties either on the RichTextBlock, or on individual inline text elements, as shown here.

```xaml
<RichTextBlock Typography.StylisticSet4="True">
    <Paragraph>
        <Span Typography.Capitals="SmallCaps">Lorem ipsum dolor sit amet</Span>
        , consectetur adipiscing elit.
    </Paragraph>
</RichTextBlock>
```

## UWP and WinUI 2

[!INCLUDE [uwp-winui2-note](../../../includes/uwp-winui-2-note.md)]

APIs for this control exist in the [Windows.UI.Xaml.Controls](/uwp/api/Windows.UI.Xaml.Controls) namespace.

> [!div class="checklist"]
>
> - **UWP APIs:** [RichTextBlock class](/uwp/api/Windows.UI.Xaml.Controls.RichTextBlock), [RichTextBlockOverflow class](/uwp/api/Windows.UI.Xaml.Controls.RichTextBlockOverflow), [Paragraph class](/uwp/api/Windows.UI.Xaml.Documents.Paragraph), [Typography class](/uwp/api/Windows.UI.Xaml.Documents.Typography)
> - [Open the WinUI 2 Gallery app and see the RichTextBox in action](winui2gallery:/item/RichTextBox). [!INCLUDE [winui-2-gallery](../../../includes/winui-2-gallery.md)]

We recommend using the latest [WinUI 2](../../winui/winui2/index.md) to get the most current styles and templates for all controls. WinUI 2.2 or later includes a new template for this control that uses rounded corners. For more info, see [Corner radius](../style/rounded-corner.md).

## Related articles

[Text controls](text-controls.md)

**For designers**
- [Guidelines for spell checking](text-controls.md)
- [Adding search](/previous-versions/windows/apps/hh465231(v=win.10))
- [Guidelines for text input](text-controls.md)

**For developers (XAML)**
- [TextBox class](/windows/windows-app-sdk/api/winrt/microsoft.UI.Xaml.Controls.TextBox)
- [PasswordBox class](/windows/windows-app-sdk/api/winrt/microsoft.UI.Xaml.Controls.PasswordBox)

**For developers (other)**
- [String.Length property](/dotnet/api/system.string.length)