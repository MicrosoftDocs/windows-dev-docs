---
title: "AI-assisted WinUI tutorial - Build the interface"
description: Generate and inspect an accessible, adaptive WinUI 3 interface that uses platform controls, data binding, and responsive visual states.
author: GrantMeStrength
ms.author: jken
ms.date: 08/31/2026
ms.topic: tutorial
---

# Build the accessible and adaptive interface

Now ask the assistant to create the page. Name the WinUI controls you want so it doesn't substitute a control from another UI framework.

## Ask for the XAML

Use this prompt:

```text
Create MainPage.xaml for TaskTally.

- Use a TextBox with a visible Header and an Add task Button.
- Use ListView for the task collection. Don't wrap it in ScrollViewer.
- In each row, use a CheckBox, task title, and delete Button.
- Use InfoBar for save and error status.
- Use x:Bind and specify Mode for every binding that changes.
- For TextBox.Text, use Mode=TwoWay and
  UpdateSourceTrigger=PropertyChanged.
- Add accessible names and tooltips for icon-only commands.
- Add an empty state.
- At widths below 640 effective pixels, move the Add task button
  below the TextBox and stretch it.
- Use theme resources instead of hard-coded colors.
- Build and fix all XAML compiler and analyzer warnings.
```

Inspect these details in the generated XAML.

### Data entry

```xaml
<TextBox
    x:Name="NewTaskTextBox"
    Header="New task"
    PlaceholderText="For example, review the generated XAML"
    Text="{x:Bind ViewModel.NewTaskTitle, Mode=TwoWay,
                  UpdateSourceTrigger=PropertyChanged}" />
<Button
    x:Name="AddTaskButton"
    Grid.Column="1"
    VerticalAlignment="Bottom"
    AutomationProperties.Name="Add task"
    Command="{x:Bind ViewModel.AddTaskCommand}"
    Content="Add task" />
```

The visible `Header` means the input doesn't rely on placeholder text as its only label. `UpdateSourceTrigger=PropertyChanged` updates the view model as the user types, so the command can enable immediately.

### Task collection

Define a typed data template:

```xaml
<DataTemplate x:Key="TaskTemplate" x:DataType="models:TaskItem">
    <Grid Padding="8" ColumnSpacing="12">
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="Auto" />
            <ColumnDefinition Width="*" />
            <ColumnDefinition Width="Auto" />
        </Grid.ColumnDefinitions>

        <CheckBox
            VerticalAlignment="Center"
            AutomationProperties.Name="{x:Bind Title}"
            Click="TaskCheckBox_Click"
            IsChecked="{x:Bind IsComplete, Mode=TwoWay}" />
        <TextBlock
            Grid.Column="1"
            VerticalAlignment="Center"
            Text="{x:Bind Title}" />
        <Button
            Grid.Column="2"
            VerticalAlignment="Center"
            AutomationProperties.Name="{x:Bind local:MainPage.DeleteTaskName(Title),
                                                Mode=OneWay}"
            Click="DeleteButton_Click"
            DataContext="{x:Bind}"
            ToolTipService.ToolTip="Delete task">
            <FontIcon Glyph="&#xE74D;" />
        </Button>
    </Grid>
</DataTemplate>
```

Use a static function to include the task title in the delete button's automation name, so a screen reader can distinguish between rows:

```csharp
public static string DeleteTaskName(string title) => $"Delete {title}";
```

Then bind the collection:

```xaml
<ListView
    AutomationProperties.Name="Tasks"
    ItemTemplate="{StaticResource TaskTemplate}"
    ItemsSource="{x:Bind ViewModel.Tasks, Mode=OneWay}"
    SelectionMode="None" />
```

`ListView` provides scrolling and UI virtualization. Wrapping it in a `ScrollViewer` interferes with both.

Learn more: [ListView and GridView](../../develop/ui/controls/listview-and-gridview.md) and [Data binding with XAML](../../develop/data-binding/data-binding-in-depth.md).

### Status and empty state

Use `InfoBar` for a nonblocking status message:

```xaml
<InfoBar
    IsClosable="True"
    IsOpen="{x:Bind ViewModel.IsStatusOpen, Mode=TwoWay}"
    Message="{x:Bind ViewModel.StatusMessage, Mode=OneWay}"
    Severity="{x:Bind local:MainPage.StatusSeverity(ViewModel.HasError),
                      Mode=OneWay}" />
```

Use a static function to convert the empty-state Boolean to `Visibility` without adding a converter class:

```csharp
public static Visibility BoolToVisibility(bool value) =>
    value ? Visibility.Visible : Visibility.Collapsed;

public static InfoBarSeverity StatusSeverity(bool hasError) =>
    hasError ? InfoBarSeverity.Error : InfoBarSeverity.Success;
```

```xaml
<TextBlock
    HorizontalAlignment="Center"
    VerticalAlignment="Center"
    Text="Add a task to get started."
    Visibility="{x:Bind local:MainPage.BoolToVisibility(ViewModel.HasNoTasks),
                        Mode=OneWay}" />
```

Run the app before adding data and confirm that the empty state is clear and the **Add task** button is disabled:

:::image type="content" source="media/task-tally-empty.png" alt-text="Task Tally in its empty state with a new task text box, a disabled Add task button, and guidance to add a first task.":::

### Adaptive layout

Use visual states to move the button below the input:

```xaml
<VisualStateManager.VisualStateGroups>
    <VisualStateGroup>
        <VisualState x:Name="Narrow">
            <VisualState.StateTriggers>
                <AdaptiveTrigger MinWindowWidth="0" />
            </VisualState.StateTriggers>
            <VisualState.Setters>
                <Setter Target="AddTaskButton.(Grid.Row)" Value="1" />
                <Setter Target="AddTaskButton.(Grid.Column)" Value="0" />
                <Setter Target="AddTaskButton.HorizontalAlignment" Value="Stretch" />
            </VisualState.Setters>
        </VisualState>
        <VisualState x:Name="Standard">
            <VisualState.StateTriggers>
                <AdaptiveTrigger MinWindowWidth="640" />
            </VisualState.StateTriggers>
        </VisualState>
    </VisualStateGroup>
</VisualStateManager.VisualStateGroups>
```

Resize the running app across the breakpoint. A responsive claim in a prompt isn't evidence that the layout works.

Learn more: [Responsive layouts with XAML](../../design/layout/responsive-design.md).

## Connect view-specific events

The page loads persisted data once and delegates task changes to the view model:

```csharp
public sealed partial class MainPage : Page
{
    public MainPageViewModel ViewModel { get; } = new();

    public MainPage()
    {
        InitializeComponent();
        Loaded += MainPage_Loaded;
    }

    private async void MainPage_Loaded(object sender, RoutedEventArgs e)
    {
        Loaded -= MainPage_Loaded;
        await ViewModel.InitializeAsync();
        NewTaskTextBox.Focus(FocusState.Programmatic);
    }

    private async void TaskCheckBox_Click(object sender, RoutedEventArgs e)
    {
        await ViewModel.TaskCompletionChangedAsync();
    }

    private async void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button { DataContext: TaskItem task })
        {
            await ViewModel.DeleteTaskCommand.ExecuteAsync(task);
        }
    }
}
```

Event handlers with an `async void` return type are appropriate here because XAML events require `void`. Other asynchronous methods in the app return `Task`.

## Build and run

Build the app, launch it through its package, and test every acceptance criterion that is now implemented:

1. Add a task with the mouse.
1. Add a task using only the keyboard.
1. Complete and delete a task.
1. Close and restart the app to verify persistence.
1. Resize the window below and above 640 effective pixels.
1. Switch between light, dark, and a contrast theme.

> [!div class="nextstepaction"]
> [Catch an AI mistake and verify the finished app](verify.md)
