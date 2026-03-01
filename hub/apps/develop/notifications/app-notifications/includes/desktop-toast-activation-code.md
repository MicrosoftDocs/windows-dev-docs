```csharp
// Listen to notification activation
ToastNotificationManagerCompat.OnActivated += toastArgs =>
{
    // Obtain the arguments from the notification
    ToastArguments args = ToastArguments.Parse(toastArgs.Argument);

    // Obtain any user input (text boxes, menu selections) from the notification
    ValueSet userInput = toastArgs.UserInput;

    // Need to dispatch to UI thread if performing UI operations
    Application.Current.Dispatcher.Invoke(delegate
    {
        // TODO: Show the corresponding content
        MessageBox.Show("Toast activated. Args: " + toastArgs.Argument);
    });
};
```