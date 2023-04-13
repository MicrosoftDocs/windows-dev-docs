using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

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
