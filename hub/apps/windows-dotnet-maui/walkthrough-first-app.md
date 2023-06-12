---
title: Build your first .NET MAUI app for Windows
description: Get hands-on with .NET MAUI by building your first cross-platform app on Windows.
ms.topic: article
ms.date: 08/15/2022
keywords: windows win32, desktop development, Windows App SDK, .net maui
ms.localizationpriority: medium
---

# Build your first .NET MAUI app for Windows

Get hands-on with .NET MAUI by building your first cross-platform app on Windows.

## Introduction

In this tutorial, you'll learn how to create and run your first .NET MAUI app for Windows in Visual Studio 2022 (17.3 or later). We will add some [MVVM Toolkit](/dotnet/communitytoolkit/mvvm/) features from the [.NET Community Toolkit](/dotnet/communitytoolkit/introduction) to improve the design the default project.

## Setting up the environment

If you haven't already set up your environment for .NET MAUI development, please follow the steps to [Get started with .NET MAUI on Windows](index.md#get-started-with-net-maui-on-windows).

## Creating the .NET MAUI project

1. Launch Visual Studio, and in the start window click **Create a new project** to create a new project:

![Create a new project.](images/hello-maui-new-project.png)

2. In the **Create a new project** window, select **MAUI** in the All project types drop-down, select the **.NET MAUI App** template, and click the **Next** button:

![.NET MAUI App template.](images/hello-maui-app-template.png)

3. In the **Configure your new project** window, give your project a name, choose a location for it, and click the **Next** button:

![Name the new project.](images/hello-maui-name-project.png)

4. In the **Additional information** window, click the **Create** button:

![Create new project.](images/hello-maui-addl-info-create.png)

5. Wait for the project to be created, and for its dependencies to be restored:

![Project is created.](images/hello-maui-project-created.png)

6. In the Visual Studio toolbar, press the Windows Machine button to build and run the app.

7. In the running app, press the **Click me** button several times and observe that the count of the number of button clicks is incremented:

![Run a MAUI app for the first time.](images/hello-maui-first-run-app.png)

You just ran your first .NET MAUI app on Windows. In the next section, you'll learn how to add data binding and messaging features from the **MVVM Toolkit** to your app.

## Troubleshooting

If your app fails to compile, review [Troubleshooting known issues](/dotnet/maui/troubleshooting), which may have a solution to your problem.

## Adding the MVVM Toolkit

Now that you have your first .NET MAUI app running on Windows, let's add some MVVM Toolkit features to the project to improve the app's design.

1. Right-click the project in **Solution Explorer** and select **Manage NuGet Packages...** from the context menu.

2. In the **NuGet Package Manager** window, select the **Browse** tab and search for **CommunityToolkit.MVVM**:

![CommunityToolkit.MVVM package.](images/hello-maui-mvvm-pkg.png)

3. Add the latest stable version of the **CommunityToolkit.MVVM** package (version 8.0.0 or later) to the project by clicking **Install**.

4. Close the **NuGet Package Manager** window after the new package has finished installing.

5. Right-click the project again and select **Add | Class** from the context menu.

6. In the **Add New Item** window that appears, name the class `MainViewModel` and click **Add**:

![Add MainViewModel class.](images/hello-maui-add-vm.png)

7. The `MainViewModel` class will be the data binding target for the `MainPage`. Update it to inherit from `ObservableObject` in the `CommunityToolkit.Mvvm.ComponentModel` namespace This will also require updating the class to be `public` and `partial`.

8. The `MainViewModel` class will contain the following code. The `CountChangedMessage` record defines a message that is sent each time the Click me button is clicked, notifying the view of the change. The [ObservableProperty](/dotnet/communitytoolkit/mvvm/generators/observableproperty) and [RelayCommand](/dotnet/communitytoolkit/mvvm/generators/relaycommand) attributes added to the `message` and `IncrementCounter` members are source generators provided by the MVVM Toolkit to create the MVVM boilerplate code for `INotifyPropertyChanged` and `IRelayCommand` implementations. The `IncrementCounter` method's implementation contains the logic from `OnCounterClicked` in MainPage.xaml.cs, with a change to send a message with the new counter message. We will be removing that code-behind code later.

``` csharp
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace MauiOnWindows
{
    public sealed record CountChangedMessage(string Text);

    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private string message = "Click me";

        private int count;

        [RelayCommand]
        private void IncrementCounter()
        {
            count++;

            if (count == 1)
                message = $"Clicked {count} time";
            else
                message = $"Clicked {count} times";

            WeakReferenceMessenger.Default.Send(new CountChangedMessage(message));
        }
    }
}
```

> [!NOTE]
> You will need to update the namespace in the previous code to match the namespace in your project.

9. Open the **MainPage.xaml.cs** file for editing and remove the `OnCounterClicked` method and the `count` field.

10. Add the following code to the `MainPage` constructor after the call to `InitializeComponenent()`. This code will receive the message sent by `IncrementCounter()` in the `MainViewModel` and will update the `CounterBtn.Text` property with the new message and announce the new text with the `SemanticScreenReader`:

``` csharp
WeakReferenceMessenger.Default.Register<CountChangedMessage>(this, (r, m) =>
{
    CounterBtn.Text = m.Text;
    SemanticScreenReader.Announce(m.Text);
});
```

11. You will also need to add a `using` statement to the class:

``` csharp
using CommunityToolkit.Mvvm.Messaging;
```

12. In `MainPage.xaml`, add a namespace declaration to the `ContentPage` so the `MainViewModel` class can be found:

``` xaml
xmlns:local="clr-namespace:MauiOnWindows"
```

13. Add `MainViewModel` as the `BindingContext` for the `ContentPage`:

``` xaml
<ContentPage.BindingContext>
    <local:MainViewModel/>
</ContentPage.BindingContext>
```

14. Update the `Button` on `MainPage` to use a `Command` instead of handling the `Clicked` event. The command will bind to the `IncrementCounterCommand` public property that is generated by the MVVM Toolkit's source generators:

``` xaml
<Button
    x:Name="CounterBtn"
    Text="Click me"
    SemanticProperties.Hint="Counts the number of times you click"
    Command="{Binding Path=IncrementCounterCommand}"
    HorizontalOptions="Center" />
```

15. Run the project again and observe that the counter is still incremented when you click the button:

![Click me button clicked three times.](images/hello-maui-mvvm-clicked-3-times.png)

16. While the project is running, try updating the "Hello, World!" message in the first Label to read "Hello, Windows!" in **MainPage.xaml**. Save the file and notice that [XAML Hot Reload](/dotnet/maui/xaml/hot-reload) updates the UI while the app is still running:

![Hello World updated to Hello Windows with XAML Hot Reload.](images/hello-maui-xaml-hot-reload-edited.png)

## Next steps

Learn to build an app that displays [Microsoft Graph](/graph/) data for a user by leveraging the [Graph SDK](/graph/sdks/sdks-overview) in a [.NET MAUI for Windows tutorial](./tutorial-graph-api.md).

## Related topics

[Resources for learning .NET MAUI](/dotnet/maui/get-started/resources)

[Using the Microsoft Graph API with .NET MAUI on Windows](./tutorial-graph-api.md)
