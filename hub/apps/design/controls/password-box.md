---
description: A password box is a text input box that conceals the characters typed into it for the purpose of privacy.
title: Guidelines for password boxes
ms.assetid: 332B04D6-4FFE-42A4-8B3D-ABE8266C7C18
dev.assetid: 4BFDECC6-9BC5-4FF5-8C63-BB36F6DDF2EF
label: Password box
template: detail.hbs
ms.date: 02/26/2025
ms.topic: article
doc-status: Published
ms.localizationpriority: medium
---
# Password box

A password box is a text input box that conceals the characters typed into it for the purpose of privacy. A password box looks like a text box, except that it renders placeholder characters in place of the text that has been entered. You can configure the placeholder character.

![Password box focus state typing text](images/passwordbox-focus-typing.png)

By default, the password box provides a way for the user to view their password by holding down a reveal button. You can disable the reveal button, or provide an alternate mechanism to reveal the password, such as a check box.

## Is this the right control?

Use a **PasswordBox** control to collect a password or other private data, such as a Social Security number.

For more info about choosing the right text control, see the [Text controls](text-controls.md) article.

## Recommendations

- Use a label or placeholder text if the purpose of the password box isn't clear. A label is visible whether or not the text input box has a value. Placeholder text is displayed inside the text input box and disappears once a value has been entered.
- Give the password box an appropriate width for the range of values that can be entered. Word length varies between languages, so take localization into account if you want your app to be world-ready.
- Don't put another control right next to a password input box. The password box has a password reveal button for users to verify the passwords they have typed, and having another control right next to it might make users accidentally reveal their passwords when they try to interact with the other control. To prevent this from happening, put some spacing between the password in put box and the other control, or put the other control on the next line.
- Consider presenting two password boxes for account creation: one for the new password, and a second to confirm the new password.
- Only show a single password box for logins.
- When a password box is used to enter a PIN, consider providing an instant response as soon as the last number is entered instead of using a confirmation button.

## Examples

The password box has several states, including these notable ones.

A password box at rest can show hint text so that the user knows its purpose:

![Password box in rest state with hint text](images/passwordbox-rest-hinttext.png)

When the user types in a password box, the default behavior is to show bullets that hide the text being entered:

![Password box focus state typing text](images/passwordbox-focus-typing.png)

Pressing the "reveal" button on the right gives a peek at the password text being entered:

![Password box text revealed](images/passwordbox-text-reveal.png)

## Create a password box

> [!div class="checklist"]
>
> - **Important APIs:** [PasswordBox class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.passwordbox), [Password property](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.passwordbox.password), [PasswordChar property](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.passwordbox.passwordchar), [PasswordRevealMode property](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.passwordbox.passwordrevealmode), [PasswordChanged event](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.passwordbox.passwordchanged)

> [!div class="nextstepaction"]
> [Open the WinUI 3 Gallery app and see PasswordBox in action](winui3gallery:/item/PasswordBox)

[!INCLUDE [winui-3-gallery](../../../includes/winui-3-gallery.md)]

Use the [Password](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.passwordbox.password) property to get or set the contents of the PasswordBox. You can do this in the handler for the [PasswordChanged](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.passwordbox.passwordchanged) event to perform validation while the user enters the password. Or, you can use another event, like a button [Click](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.primitives.buttonbase.click), to perform validation after the user completes the text entry.

Here's the XAML for a password box control that demonstrates the default look of the PasswordBox. When the user enters a password, you check to see if it's the literal value, "Password". If it is, you display a message to the user.

```xaml
<StackPanel>  
  <PasswordBox x:Name="passwordBox" Width="200" MaxLength="16"
             PasswordChanged="passwordBox_PasswordChanged"/>

  <TextBlock x:Name="statusText" Margin="10" HorizontalAlignment="Center" />
</StackPanel>   
```

```csharp
private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
{
    if (passwordBox.Password == "Password")
    {
        statusText.Text = "'Password' is not allowed as a password.";
    }
    else
    {
        statusText.Text = string.Empty;
    }
}
```
Here's the result when this code runs and the user enters "Password".

![Password box with a validation message](images/passwordbox-revealed-validation.png)

### Password character

You can change the character used to mask the password by setting the [PasswordChar](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.passwordbox.passwordchar) property. Here, the default bullet is replaced with a pound sign.

```xaml
<PasswordBox x:Name="passwordBox" Width="300" PasswordChar="#"/>
```

The result looks like this.

![Password box with a custom character](images/passwordbox-custom-char.png)

### Headers and placeholder text

You can use the [Header](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.passwordbox.header) and [PlaceholderText](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.passwordbox.placeholdertext) properties to provide context for the PasswordBox. This is especially useful when you have multiple boxes, such as on a form to change a password.

```xaml
<PasswordBox x:Name="passwordBox" Width="200" Header="Password" PlaceholderText="Enter your password"/>
```

![Password box in rest state with hint text](images/passwordbox-rest-hinttext.png)

### Maximum length

Specify the maximum number of characters that the user can enter by setting the [MaxLength](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.passwordbox.maxlength) property. There is no property to specify a minimum length, but you can check the password length, and perform any other validation, in your app code.

## Password reveal mode

The PasswordBox has a built-in button that the user can press to display the password text. Here's the result of the user's action. When the user releases it, the password is automatically hidden again.

![Password box text revealed](images/passwordbox-text-reveal.png)

### Peek mode

By default, the password reveal button (or "peek" button) is shown. The user must continuously press the button to view the password, so that a high level of security is maintained.

The value of the [PasswordRevealMode](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.passwordbox.passwordrevealmode) property is not the only factor that determines whether a password reveal button is visible to the user. Other factors include whether the control is displayed above a minimum width, whether the PasswordBox has focus, and whether the text entry field contains at least one character. The password reveal button is shown only when the PasswordBox receives focus for the first time and a character is entered. If the PasswordBox loses focus and then regains focus, the reveal button is not shown again unless the password is cleared and character entry starts over.

### Hidden and Visible modes

The other [PasswordRevealMode](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.passwordrevealmode) enumeration values, **Hidden** and **Visible**, hide the password reveal button and let you programmatically manage whether the password is obscured.

To always obscure the password, set PasswordRevealMode to Hidden. Unless you need the password to be always obscured, you can provide a custom UI to let the user toggle the PasswordRevealMode between Hidden and Visible. For example, you can use a check box to toggle whether the password is obscured, as shown in the following example. You can also use other controls, like [ToggleButton](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.primitives.togglebutton), to let the user switch modes.

This example shows how to use a [CheckBox](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.checkbox) to let a user switch the reveal mode of a PasswordBox.

```xaml
<StackPanel Width="200">
    <PasswordBox Name="passwordBox1"
                 PasswordRevealMode="Hidden"/>
    <CheckBox Name="revealModeCheckBox" Content="Show password"
              IsChecked="False"
              Checked="CheckBox_Changed" Unchecked="CheckBox_Changed"/>
</StackPanel>
```

```csharp
private void CheckBox_Changed(object sender, RoutedEventArgs e)
{
    if (revealModeCheckBox.IsChecked == true)
    {
        passwordBox1.PasswordRevealMode = PasswordRevealMode.Visible;
    }
    else
    {
        passwordBox1.PasswordRevealMode = PasswordRevealMode.Hidden;
    }
}
```

This PasswordBox looks like this.

![Password box with a custom reveal button](images/passwordbox-custom-reveal.png)

## Choose the right keyboard for your text control

To help users to enter data using the touch keyboard, or Soft Input Panel (SIP), you can set the input scope of the text control to match the kind of data the user is expected to enter. PasswordBox supports only the **Password** and **NumericPin** input scope values. Any other value is ignored.

For more info about how to use input scopes, see [Use input scope to change the touch keyboard](../input/use-input-scope-to-change-the-touch-keyboard.md).

## UWP and WinUI 2

[!INCLUDE [uwp-winui2-note](../../../includes/uwp-winui-2-note.md)]

APIs for this control exist in the [Windows.UI.Xaml.Controls](/uwp/api/Windows.UI.Xaml.Controls) namespace.

> [!div class="checklist"]
>
> - **UWP APIs:** [PasswordBox class](/uwp/api/windows.ui.xaml.controls.passwordbox), [Password property](/uwp/api/windows.ui.xaml.controls.passwordbox.password), [PasswordChar property](/uwp/api/windows.ui.xaml.controls.passwordbox.passwordchar), [PasswordRevealMode property](/uwp/api/windows.ui.xaml.controls.passwordbox.passwordrevealmode), [PasswordChanged event](/uwp/api/windows.ui.xaml.controls.passwordbox.passwordchanged)
> - [Open the WinUI 2 Gallery app and see PasswordBox in action](winui2gallery:/item/PasswordBox). [!INCLUDE [winui-2-gallery](../../../includes/winui-2-gallery.md)]

We recommend using the latest [WinUI 2](../../winui/winui2/index.md) to get the most current styles and templates for all controls. WinUI 2.2 or later includes a new template for this control that uses rounded corners. For more info, see [Corner radius](../style/rounded-corner.md).

## Related articles

[Text controls](text-controls.md)

- [Guidelines for spell checking](text-controls.md)
- [Adding search](/previous-versions/windows/apps/hh465231(v=win.10))
- [Guidelines for text input](text-controls.md)
- [TextBox class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.textbox)
- [class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.passwordbox)
- [String.Length property](/dotnet/api/system.string.length)