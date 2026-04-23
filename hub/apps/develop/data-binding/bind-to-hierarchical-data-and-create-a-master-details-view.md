---
ms.assetid: 6c563dd4-3dd0-4175-a1ab-7a1103fc9559
title: Bind hierarchical data and create a master/details view with WinUI
description: Create a multi-level master/details view of hierarchical data in WinUI by binding items controls to CollectionViewSource instances. Learn how to implement this structure.
ms.date: 11/11/2025
ms.topic: how-to
keywords: windows 10, windows 11, winui, windows app sdk, windows ui, xBind
ms.localizationpriority: medium
# customer intent: As a Windows developer, I want to learn how to bind hierarchical data and create a master/details view with WinUI.
---

# Bind hierarchical data and create a master/details view with WinUI

Learn how to create a multilevel master/details view of hierarchical data in WinUI by binding items controls to [CollectionViewSource](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.data.collectionviewsource) instances. This article explains how to use the [{x:Bind} markup extension](/windows/apps/develop/platform/xaml/x-bind-markup-extension) for better performance and the [{Binding} markup extension](/windows/apps/develop/platform/xaml/binding-markup-extension) when flexibility is needed.

One common structure for WinUI apps is to navigate to different details pages when a user makes a selection in a master list. This structure is useful when you want to provide a rich visual representation of each item at every level in a hierarchy. Another option is to display multiple levels of data on a single page. This structure is useful when you want to display a few simple lists that let the user quickly drill down to an item of interest. This article describes how to implement this interaction. The [CollectionViewSource](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.data.collectionviewsource) instances keep track of the current selection at each hierarchical level.

You create a view of a sports team hierarchy that's organized into lists for leagues, divisions, and teams, and includes a team details view. When you select an item from any list, the subsequent views update automatically.

:::image type="content" source="images/xaml-masterdetails.png" alt-text="Screenshot of a master/details view of a sports hierarchy. The view includes leagues, divisions, and teams.":::

> [!TIP]
> Also see the [Master/detail UWP sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/XamlMasterDetail).

## Prerequisites

This article assumes that you know how to create a basic WinUI app. For instructions on creating your first WinUI app, see [Create a WinUI app](/windows/apps/tutorials/winui-notes/intro).

## Create the project

Create a new **Blank App, Packaged (WinUI 3 in Desktop)** project. Name it "MasterDetailsBinding".

## Create the data model

Add a new class to your project, name it **ViewModel.cs**, and add this code to it. This class is your binding source class.

```cs
using System.Collections.Generic;
using System.Linq;

namespace MasterDetailsBinding
{
    public class Team
    {
        public string Name { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
    }

    public class Division
    {
        public string Name { get; set; }
        public IEnumerable<Team> Teams { get; set; }
    }

    public class League
    {
        public string Name { get; set; }
        public IEnumerable<Division> Divisions { get; set; }
    }

    public class LeagueList : List<League>
    {
        public LeagueList()
        {
            AddRange(GetLeague().ToList());
        }

        public IEnumerable<League> GetLeague()
        {
            return from x in Enumerable.Range(1, 2)
                   select new League
                   {
                       Name = "League " + x,
                       Divisions = GetDivisions(x).ToList()
                   };
        }

        public IEnumerable<Division> GetDivisions(int x)
        {
            return from y in Enumerable.Range(1, 3)
                   select new Division
                   {
                       Name = string.Format("Division {0}-{1}", x, y),
                       Teams = GetTeams(x, y).ToList()
                   };
        }

        public IEnumerable<Team> GetTeams(int x, int y)
        {
            return from z in Enumerable.Range(1, 4)
                   select new Team
                   {
                       Name = string.Format("Team {0}-{1}-{2}", x, y, z),
                       Wins = 25 - (x * y * z),
                       Losses = x * y * z
                   };
        }
    }
}
```

## Create the view

Next, expose the binding source class from the class that represents your page of markup. Add a property of type `LeagueList` to **MainWindow**.

```cs
namespace MasterDetailsBinding
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new LeagueList();
        }
        public LeagueList ViewModel { get; set; }
    }
}
```

Finally, replace the contents of the **MainWindow.xaml** file with the following markup. This markup declares three [**CollectionViewSource**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.data.collectionviewsource) instances and binds them together in a chain. The subsequent controls can then bind to the appropriate `CollectionViewSource`, depending on its level in the hierarchy.

``` xaml
<Window
    x:Class="MasterDetailsBinding.MainWindow"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:local="using:MasterDetailsBinding"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    mc:Ignorable="d">

    <Grid>
        <Grid.Resources>
            <CollectionViewSource x:Name="Leagues"
                Source="{x:Bind ViewModel}"/>
            <CollectionViewSource x:Name="Divisions"
                Source="{Binding Divisions, Source={StaticResource Leagues}}"/>
            <CollectionViewSource x:Name="Teams"
                Source="{Binding Teams, Source={StaticResource Divisions}}"/>
    
            <Style TargetType="TextBlock">
                <Setter Property="FontSize" Value="15"/>
                <Setter Property="FontWeight" Value="Bold"/>
            </Style>
            <Style TargetType="ListBox">
                <Setter Property="FontSize" Value="15"/>
            </Style>
            <Style TargetType="ContentControl">
                <Setter Property="FontSize" Value="15"/>
            </Style>
        </Grid.Resources>

        <StackPanel Orientation="Horizontal">

            <!-- All Leagues view -->
            <StackPanel Margin="5">
                <TextBlock Text="All Leagues"/>
                <ListBox ItemsSource="{Binding Source={StaticResource Leagues}}" 
                         DisplayMemberPath="Name"/>
            </StackPanel>

            <!-- League/Divisions view -->
            <StackPanel Margin="5">
                <TextBlock Text="{Binding Name, Source={StaticResource Leagues}}"/>
                <ListBox ItemsSource="{Binding Source={StaticResource Divisions}}" 
                         DisplayMemberPath="Name"/>
            </StackPanel>

            <!-- Division/Teams view -->
            <StackPanel Margin="5">
                <TextBlock Text="{Binding Name, Source={StaticResource Divisions}}"/>
                <ListBox ItemsSource="{Binding Source={StaticResource Teams}}" 
                         DisplayMemberPath="Name"/>
            </StackPanel>

            <!-- Team view -->
            <ContentControl Content="{Binding Source={StaticResource Teams}}">
                <ContentControl.ContentTemplate>
                    <DataTemplate>
                        <StackPanel Margin="5">
                            <TextBlock Text="{Binding Name}" 
                                       FontSize="15" FontWeight="Bold"/>
                            <StackPanel Orientation="Horizontal" Margin="10,10">
                                <TextBlock Text="Wins:" Margin="0,0,5,0"/>
                                <TextBlock Text="{Binding Wins}"/>
                            </StackPanel>
                            <StackPanel Orientation="Horizontal" Margin="10,0">
                                <TextBlock Text="Losses:" Margin="0,0,5,0"/>
                                <TextBlock Text="{Binding Losses}"/>
                            </StackPanel>
                        </StackPanel>
                    </DataTemplate>
                </ContentControl.ContentTemplate>
            </ContentControl>
        </StackPanel>
    </Grid>
</Window>
```

When you bind directly to the [**CollectionViewSource**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.data.collectionviewsource), you imply that you want to bind to the current item in bindings where the path can't be found on the collection itself. You don't need to specify the `CurrentItem` property as the path for the binding, although you can add it if there's any ambiguity. For example, the [**ContentControl**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.contentcontrol) representing the team view has its [**Content**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.contentcontrol.content) property bound to the `Teams` `CollectionViewSource`. However, the controls in the [**DataTemplate**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.datatemplate) bind to properties of the `Team` class because the `CollectionViewSource` automatically supplies the currently selected team from the teams list when necessary.

## See also

- [Data binding overview](data-binding-overview.md)
- [Data binding in depth](data-binding-in-depth.md)
