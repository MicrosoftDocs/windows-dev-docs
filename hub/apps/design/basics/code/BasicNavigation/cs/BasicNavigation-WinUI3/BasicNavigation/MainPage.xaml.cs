// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BasicNavigation
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            //Frame.Navigate(typeof(Page2));

            //Frame.Navigate(typeof(Page2), name.Text);

            Frame.Navigate(typeof(Page2),
               name.Text,
               new SlideNavigationTransitionInfo()
                   { Effect = SlideNavigationTransitionEffect.FromRight });
        }
    }
}
