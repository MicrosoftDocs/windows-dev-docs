---
Description: Hyperlinks navigate the user to another part of the app, to another app, or launch a specific uniform resource identifier (URI) using a separate browser app.
title: Hyperlinks
ms.assetid: 74302FF0-65FC-4820-B59A-718A765EF7F0
label: Hyperlinks
template: detail.hbs
ms.date: 05/19/2017
ms.topic: article
keywords: windows 10, uwp
pm-contact: kisai
design-contact: kimsea
dev-contact: stpete
doc-status: Published
ms.localizationpriority: medium
---
# Hyperlinks

Hyperlinks navigate the user to another part of the app, to another app, or launch a specific uniform resource identifier (URI) using a separate browser app. There are two ways that you can add a hyperlink to a XAML app: the **Hyperlink** text element and **HyperlinkButton** control.

> **Platform APIs**: [Hyperlink text element](/uwp/api/Windows.UI.Xaml.Documents.Hyperlink), [HyperlinkButton control](/uwp/api/Windows.UI.Xaml.Controls.HyperlinkButton)

![A hyperlink button](images/controls/hyperlink-button.png)


## Is this the right control?

Use a hyperlink when you need text that responds when selected and navigates the user to more information about the text that was selected.

Choose the right type of hyperlink based on your needs:

-   Use an inline **Hyperlink** text element inside of a text control. A Hyperlink element flows with other text elements and you can use it in any InlineCollection. Use a text hyperlink if you want automatic text wrapping and don't necessarily need a large hit target. Hyperlink text can be small and difficult to target, especially for touch.
-   Use a **HyperlinkButton** for stand-alone hyperlinks. A HyperlinkButton is a specialized Button control that you can use anywhere that you would use a Button.
-   Use a **HyperlinkButton** with an [Image](/uwp/api/windows.ui.xaml.controls.image) as its content to make a clickable image.

## Examples

<table>
<th align="left">XAML Controls Gallery<th>
<tr>
<td><img src="images/xaml-controls-gallery-app-icon-sm.png" alt="XAML controls gallery"></img></td>
<td>
    <p>If you have the <strong style="font-weight: semi-bold">XAML Controls Gallery</strong> app installed, click here to <a href="xamlcontrolsgallery:/item/HyperlinkButton">open the app and see the HyperlinkButton in action</a>.</p>
    <ul>
    <li><a href="https://www.microsoft.com/store/productId/9MSVH128X2ZT">Get the XAML Controls Gallery app (Microsoft Store)</a></li>
    <li><a href="https://github.com/Microsoft/Xaml-Controls-Gallery">Get the source code (GitHub)</a></li>
    </ul>
</td>
</tr>
</table>

## Create a Hyperlink text element

This example shows how to use a Hyperlink text element inside of a [TextBlock](/uwp/api/windows.ui.xaml.controls.textblock).

```xml
<StackPanel Width="200">
    <TextBlock Text="Privacy" Style="{StaticResource SubheaderTextBlockStyle}"/>
    <TextBlock TextWrapping="WrapWholeWords">
        <Span xml:space="preserve"><Run>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Read the </Run><Hyperlink NavigateUri="http://www.contoso.com">Contoso Privacy Statement</Hyperlink><Run> in your browser.</Run> Donec pharetra, enim sit amet mattis tincidunt, felis nisi semper lectus, vel porta diam nisi in augue.</Span>
    </TextBlock>
</StackPanel>

```
The hyperlink appears inline and flows with the surrounding text:

![Example of a hyperlink as a text element](images/controls_hyperlink-element.png) 

> **Tip**&nbsp;&nbsp;When you use a Hyperlink in a text control with other text elements in XAML, place the content in a [Span](/uwp/api/windows.ui.xaml.documents.span) container and apply the `xml:space="preserve"` attribute to the Span to keep the white space between the Hyperlink and other elements.

## Create a HyperlinkButton

Here's how to use a HyperlinkButton, both with text and with an image.

```xml
<StackPanel>
    <TextBlock Text="About" Style="{StaticResource TitleTextBlockStyle}"/>
    <HyperlinkButton NavigateUri="http://www.contoso.com">
        <Image Source="Assets/ContosoLogo.png"/>
    </HyperlinkButton>
    <TextBlock Text="Version: 1.0.0001" Style="{StaticResource CaptionTextBlockStyle}"/>
    <HyperlinkButton Content="Contoso.com" NavigateUri="http://www.contoso.com"/>
    <HyperlinkButton Content="Acknowledgments" NavigateUri="http://www.contoso.com"/>
    <HyperlinkButton Content="Help" NavigateUri="http://www.contoso.com"/>
</StackPanel>

```

The hyperlink buttons with text content appear as marked-up text. The Contoso logo image is also a clickable hyperlink:

![Example of a hyperlink as a button control](images/controls_hyperlink-button-image.png)

This example shows how to create a HyperlinkButton in code.

```csharp
var helpLinkButton = new HyperlinkButton();
helpLinkButton.Content = "Help";
helpLinkButton.NavigateUri = new Uri("http://www.contoso.com");
```

## Handle navigation

For both kinds of hyperlinks, you handle navigation the same way; you can set the **NavigateUri** property, or handle the **Click** event.

**Navigate to a URI**

To use the hyperlink to navigate to a URI, set the NavigateUri property. When a user clicks or taps the hyperlink, the specified URI opens in the default browser. The default browser runs in a separate process from your app.

> [!NOTE]
> A URI is represented by the [Windows.Foundation.Uri](/uwp/api/windows.foundation.uri) class. When programming with .NET, this class is hidden and you should use the [System.Uri](/dotnet/api/system.uri) class. For more info, see the reference pages for these classes.

You don't have to use **http:** or **https:** schemes. You can use schemes such as **ms-appx:**, **ms-appdata:**, or **ms-resources:**, if there's resource content at these locations that's appropriate to load in a browser. However, the **file:** scheme is specifically blocked. For more info, see [URI schemes](/previous-versions/windows/apps/jj655406(v=win.10)).

When a user clicks the hyperlink, the value of the NavigateUri property is passed to a system handler for URI types and schemes. The system then launches the app that is registered for the scheme of the URI provided for NavigateUri.

If you don't want the hyperlink to load content in a default Web browser (and don't want a browser to appear), then don't set a value for NavigateUri. Instead, handle the Click event, and write code that does what you want.


**Handle the Click event**

Use the Click event for actions other than launching a URI in a browser, such as navigation within the app. For example, if you want to load a new app page rather than opening a browser, call a [Frame.Navigate](/uwp/api/windows.ui.xaml.controls.frame.navigate) method within your Click event handler to navigate to the new app page. If you want an external, absolute URI to load within a [WebView](/uwp/api/windows.ui.xaml.controls.webview) control that also exists in your app, call [WebView.Navigate](/uwp/api/windows.ui.xaml.controls.webview.navigate) as part of your Click handler logic.

You don't typically handle the Click event as well as specifying a NavigateUri value, as these represent two different ways of using the hyperlink element. If your intent is to open the URI in the default browser, and you have specified a value for NavigateUri, don't handle the Click event. Conversely, if you handle the Click event, don't specify a NavigateUri.

There's nothing you can do within the Click event handler to prevent the default browser from loading any valid target specified for NavigateUri; that action takes place automatically (asynchronously) when the hyperlink is activated and can't be canceled from within the Click event handler. 

## Hyperlink underlines
By default, hyperlinks are underlined. This underline is important because it helps meet accessibility requirements. Color-blind users use the underline to distinguish between hyperlinks and other text. If you disable underlines, you should consider adding some other type of formatting difference to distinguish hyperlinks from other text, such as FontWeight or FontStyle.

**Hyperlink text elements**

You can set the [UnderlineStyle](/uwp/api/windows.ui.xaml.documents.hyperlink.underlinestyle) property to disable the underline. If you do, consider using [FontWeight](/uwp/api/windows.ui.xaml.documents.textelement.fontweight) or [FontStyle](/uwp/api/windows.ui.xaml.documents.textelement.fontstyle) to differentiate your link text.

**HyperlinkButton** 

By default, the HyperlinkButton appears as underlined text when you set a string as the value for the [Content](/uwp/api/windows.ui.xaml.controls.contentcontrol.content) property.

The text does not appear underlined in the following cases:
- You set a [TextBlock](/uwp/api/windows.ui.xaml.controls.textblock) as the value for the Content property, and set the [Text](/uwp/api/windows.ui.xaml.controls.textblock.text) property on the TextBlock.
- You re-template the HyperlinkButton and change the name of the [ContentPresenter](/uwp/api/windows.ui.xaml.controls.contentpresenter) template part.

If you need a button that appears as non-underlined text, consider using a standard Button control and applying the built-in `TextBlockButtonStyle` system resource to its Style property.

## Notes for Hyperlink text element

This section applies only to the Hyperlink text element, not to the HyperlinkButton control.

**Input events**

Because a Hyperlink is not a [UIElement](/uwp/api/windows.ui.xaml.uielement), it does not have the set of UI element input events such as Tapped, PointerPressed, and so on. Instead, a Hyperlink has its own Click event, plus the implicit behavior of the system loading any URI specified as the NavigateUri. The system handles all input actions that should invoke the Hyperlink actions and raises the Click event in response.

**Content**

Hyperlink has restrictions on the content that can exist in its [Inlines](/uwp/api/windows.ui.xaml.documents.span.inlines) collection. Specifically, a Hyperlink only permits [Run](/uwp/api/windows.ui.xaml.documents.run) and other [Span](/uwp/api/windows.ui.xaml.documents.span) types that aren't another Hyperlink. [InlineUIContainer](/uwp/api/windows.ui.xaml.documents.inlineuicontainer) can't be in the Inlines collection of a Hyperlink. Attempting to add restricted content throws an invalid argument exception or XAML parse exception.

**Hyperlink and theme/style behavior**

Hyperlink doesn't inherit from [Control](/uwp/api/windows.ui.xaml.controls.control), so it doesn't have a [Style](/uwp/api/windows.ui.xaml.frameworkelement.style) property or a [Template](/uwp/api/windows.ui.xaml.controls.control.template). You can edit the properties that are inherited from [TextElement](/uwp/api/windows.ui.xaml.documents.textelement), such as Foreground or FontFamily, to change the appearance of a Hyperlink, but you can't use a common style or template to apply changes. Instead of using a template, consider using common resources for values of Hyperlink properties to provide consistency. Some properties of Hyperlink use defaults from a {ThemeResource} markup extension value provided by the system. This enables the Hyperlink appearance to switch in appropriate ways when the user changes the system theme at run-time.

The default color of the hyperlink is the accent color of the system. You can set the [Foreground](/uwp/api/windows.ui.xaml.documents.textelement.foreground) property to override this.

## Recommendations

-   Only use hyperlinks for navigation; don't use them for other actions.
-   Use the Body style from the type ramp for text-based hyperlinks. Read about [fonts and the Windows 10 type ramp](../style/typography.md).
-   Keep discrete hyperlinks far enough apart so that the user can differentiate between them and has an easy time selecting each one.
-   Add tooltips to hyperlinks that indicate to where the user will be directed. If the user will be directed to an external site, include the top-level domain name inside the tooltip, and style the text with a secondary font color.

## Get the sample code

- [XAML Controls Gallery sample](https://github.com/Microsoft/Xaml-Controls-Gallery) - See all the XAML controls in an interactive format.

## Related articles

- [Text controls](text-controls.md)
- [Guidelines for tooltips](tooltips.md)

**For developers (XAML)**
- [Windows.UI.Xaml.Documents.Hyperlink class](/uwp/api/Windows.UI.Xaml.Documents.Hyperlink)
- [Windows.UI.Xaml.Controls.HyperlinkButton class](/uwp/api/Windows.UI.Xaml.Controls.HyperlinkButton)