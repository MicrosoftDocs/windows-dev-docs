# Number box

Represents a control that can be used to display and edit numbers. This supports validation, increment stepping, and computing inline calculations of basic equations, such as multiplication, division, addition, and subtraction.


**Important APIs:** [NumberBox class](https://docs.microsoft.com/uwp/api/microsoft.ui.xaml.controls.NumberBox)

## Is this the right control? 

You can use a NumberBox control to capture and display mathematic input. If you need an editable text box that accepts more than numbers, use the [TextBox](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.TextBox) control. If you need an editable text box that accepts passwords or other sensitive input, see [PasswordBox](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.passwordbox). If you need a text box to enter search terms, see [AutoSuggestBox](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.autosuggestbox). If you need to enter or edit formatted text, see [RichEditBox](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.richeditbox).

## Examples

### Create a simple NumberBox

Here's the XAML for a basic NumberBox that demonstrates the default look. Use [x:Bind](/windows/uwp/xaml-platform/x-bind-markup-extension#property-path) to ensure the data displayed to the user remains in sync with the data stored in your app. 


```XAML
<NumberBox Value="{x:Bind Path=ViewModel.NumberBoxValue, Mode=TwoWay}" />
```
![An in-focus input field showing 0.](images/numberbox-basic.PNG)

### Labeling NumberBox

Use `Header` or `PlaceholderText` if the purpose of the NumberBox isn't clear. `Header` is visible whether or not the NumberBox has a value. 

```XAML
<NumberBox Header="Enter expression:"
    Value="{x:Bind Path=ViewModel.NumberBoxValue, Mode=TwoWay}" />
```

![A header reading "Enter expression:" above a NumberBox.](images/numberbox-header.PNG)

`PlaceholderText` is displayed inside the NumberBox and disappears once a value has been entered.

```XAML
<NumberBox PlaceholderText="A + B"
    Value="{x:Bind Path=ViewModel.NumberBoxValue, Mode=TwoWay}" />
```

![A NumberBox containing placeholder text reading "A + B".](images/numberbox-placeholder-text.PNG)

### Enable calculation support

Setting the `AcceptsExpression` property to true enables NumberBox to evaluate basic inline expressions such as multiplication, division, addition, and subtraction using standard order of operations. Evaluation is triggered on loss of focus or when the user presses the "Enter" key. Once an expression is evaluated, the original form of the expression is not preserved.

XAML
```XAML
<NumberBox Value="{x:Bind Path=ViewModel.NumberBoxValue, Mode=TwoWay}"
    AcceptsExpression="True" />
```

### Add increment and decrement stepping

Use the `SpinButtonPlacementMode` property to enable buttons in the NumberBox control that can be clicked to increment or decrement the value in the NumberBox. These buttons will be disabled if a Maximum or Minimum value would be surpassed with another step. The amount of increment/decrement is specified with the `StepFrequency` property, which defaults to 1.

This defaults to `Hidden`, but NumberBox offers two visible placement modes: `Inline` and `Compact`. Set `SpinButtonPlacementMode` to `Inline` to enable the buttons to appear beside the control. 

XAML
```XAML
<NumberBox Value="{x:Bind Path=ViewModel.NumberBoxValue, Mode=TwoWay}"
    StepFrequency="2"
    SpinButtonPlacementMode="Inline" />
```

![A NumberBox with a down-arrow button and up-arrow button beside it.](images/numberbox-spinbutton-inline.PNG)

Set `SpinButtonPlacementMode` to `Compact` to enable the buttons to appear as a Flyout only when the NumberBox is in focus.  

XAML
```XAML
<NumberBox Value="{x:Bind Path=ViewModel.NumberBoxValue, Mode=TwoWay}"
    StepFrequency="2"
    SpinButtonPlacementMode="Compact" />
```

![A NumberBox with a small icon inside of it showing an arrow pointing up and an arrow pointing down.](images/numberbox-spinbutton-compact-non-visible.PNG)

![A NumberBox with a down-arrow button and up-arrow button floating off to the side at an elevated layer.](images/numberbox-spinbutton-compact-visible.PNG)

### Enabling input validation

Setting `ValidationMode` to `InvalidInputOverwritten` will enable NumberBox to overwrite invalid input that is not numerical nor legally formulaic with the last valid value when evaluation is triggered on loss of focus or a press of the "Enter" key.

XAML
```XAML
<NumberBox Header="Quantity"
    Value="{x:Bind Path=ViewModel.NumberBoxValue, Mode=TwoWay}"
    ValidationMode="InvalidInputOverwritten" />
```

Setting `ValidationMode` to `Disabled` allows custom input validation to be configured.  

With regard to decimal points and commas, the formatting used by a user will be replaced by the formatting configured for the NumberBox. An input validation error will not be triggered. 

### Formatting input 

[Number formatting](/uwp/api/windows.globalization.numberformatting) can be used to format the value of a Numberbox by configuring an instance of a formatting class and assigning it to the `NumberFormatter` property. Decimal, currency, percent, and significant figures are few of the number formatting classes available. 

Here is an example of using DecimalFormatter to format a NumberBox's value to have one integer digit and two fraction digits:  

XAML
```XAML
<NumberBox  x:Name="FormattedNumberBox"
    Value="{x:Bind Path=ViewModel.NumberBoxValue, Mode=TwoWay}" />
```

C#
```C#
private void SetNumberBoxNumberFormatter()
{
    DecimalFormatter formatter = new DecimalFormatter();
    formatter.IntegerDigits = 1;
    formatter.FractionDigits = 2;
    FormattedNumberBox.NumberFormatter = formatter;
}
```

![A NumberBox showign a value of 0.00.](images/numberbox-formatted.PNG)

With regard to decimal points and commas, the formatting used by a user will be replaced by the formatting configured for the NumberBox. An input validation error will not be triggered. 

## Remarks

### Input Scope

`Number` will be used for the [input scope](https://docs.microsoft.com/en-us/uwp/api/Windows.UI.Xaml.Input.InputScopeNameValue). This input scope is intended for working with digits 0-9. This may be overwritten but alternative InputScope types will not be explicitly supported. 

### Not a Number

When a NumberBox is cleared of input, `Value` will be set to `NaN` to indicate no numerical value is present. 

### Expression evaluation 

NumberBox uses infix notation to evaluate expressions. In order of precedence, the allowable operators are:

1. ^
2. */
3. +-

Note that parentheses can be used to override precedence rules. 

## Recommendations

* `Text` and `Value` make it easy to capture the value of a NumberBox as a String or as a Double without needing to convert the value bewteen types. When programmatically altering the value of a NumberBox, it is recommended to do so through the `Value` property. `Value` will overwrite `Text` in initial set up. After the initial set up, changes to one will be progrogated to the other, but consistently making programmatic changes through `Value` helps avoid any conceptual misunderstanding that NumberBox will accept non-numeric characters through `Text`.  
