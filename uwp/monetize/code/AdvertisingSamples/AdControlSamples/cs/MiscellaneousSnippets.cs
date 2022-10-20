// <Snippet1>
using Microsoft.Advertising.WinRT.UI;

namespace AdControlExample
{
    public sealed partial class MainPage : Page
    {
        AdControl myAdControl;
		
        public MainPage()
        {
            this.InitializeComponent();
			
            myAdControl = new AdControl()
            {
                ApplicationId = "{ApplicationID}",
                AdUnitId = "{AdUnitID}",
                Height = 90,
                Width = 728
            };
        }
    }
}
// </Snippet1>

// <Snippet2>
using Microsoft.Advertising.WinRT.UI;

namespace AdControlExample
{
    public partial class MainPage : Page
    {
        AdControl myAdControl;
		
        public MainPage()
        {
            this.InitializeComponent();
			
            myAdControl = new AdControl();
            myAdControl.ApplicationId = "{ApplicationID}";
            myAdControl.AdUnitId = "{AdUnitID}";
            myAdControl.Height = 90;
            myAdControl.Width = 728;
			
            myAdControl.ErrorOccurred += (s,e) =>
            {
                TextBlock1.Text = e.Error.Message;
            };
        }
    }
}
// </Snippet2>

// <Snippet3>
AdControl myAdControl;

public MainPage()
{
    InitializeComponent();

    myAdControl = new AdControl();
    myAdControl.ApplicationId = "{ApplicationID}";
    myAdControl.AdUnitId = "{AdUnitID}";
    myAdControl.Height = 90;
    myAdControl.Width = 728;
    myAdControl.IsAutoRefreshEnabled = false;

    ContentPanel.Children.Add(myAdControl);

    var timer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(60) };
    timer.Tick += (s, e) => myAdControl.Refresh();
    timer.Start();
}
// </Snippet3>