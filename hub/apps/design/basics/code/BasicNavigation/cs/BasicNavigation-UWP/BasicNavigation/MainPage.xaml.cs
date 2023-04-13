using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

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
