// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BasicNavigation
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Page2 : Page
    {
        public Page2()
        {
            this.InitializeComponent();
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            //Frame.Navigate(typeof(MainPage));

            Frame.Navigate(typeof(MainPage),
               null,
               new SlideNavigationTransitionInfo()
                   { Effect = SlideNavigationTransitionEffect.FromLeft });
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is string && !string.IsNullOrWhiteSpace((string)e.Parameter))
            {
                greeting.Text = $"Hello, {e.Parameter.ToString()}";
            }
            else
            {
                greeting.Text = "Hello!";
            }
            base.OnNavigatedTo(e);
        }
    }
}
