using System;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web.Core;
using Windows.UI.ApplicationSettings;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Data.Json;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;
using Windows.System;
using Windows.Security.Credentials;
using Windows.Storage;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace token_broker
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

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            AccountsSettingsPane.Show();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            AccountsSettingsPane.GetForCurrentView().AccountCommandsRequested += BuildPaneAsync;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            AccountsSettingsPane.GetForCurrentView().AccountCommandsRequested -= BuildPaneAsync;
        }

        private async void BuildPaneAsync(AccountsSettingsPane s,
            AccountsSettingsPaneCommandsRequestedEventArgs e)
        {
            var deferral = e.GetDeferral();

            var msaProvider = await WebAuthenticationCoreManager.FindAccountProviderAsync(
                "https://login.microsoft.com", "consumers");

            var command = new WebAccountProviderCommand(msaProvider, GetMsaToken);



            e.WebAccountProviderCommands.Add(command);

            deferral.Complete();
        }

        private async void GetMsaToken(WebAccountProviderCommand command)
        {
            WebTokenRequest request = new WebTokenRequest(command.WebAccountProvider, "wl.basic");
            WebTokenRequestResult result = await WebAuthenticationCoreManager.RequestTokenAsync(request);

            if (result.ResponseStatus == WebTokenRequestStatus.Success)
            {
                WebAccount account = result.ResponseData[0].WebAccount;

                StoreWebAccount(account);



                //var restApi = new Uri(@"https://apis.live.net/v5.0/me?access_token=" + token);
                //var webAccount = await WebAuthenticationCoreManager.FindAccountAsync(command.WebAccountProvider, token);

                //using (var client = new HttpClient())
                //{
                //    var infoResult = await client.GetAsync(restApi);
                //    string content = await infoResult.Content.ReadAsStringAsync();

                //    var jsonObject = JsonObject.Parse(content);
                //    string id = jsonObject["id"].GetString();
                //    string username = jsonObject["name"].GetString();


                //    UserIdTextBlock.Text = id; 
                //    UserNameTextBlock.Text = username;

                //    var asdf = new WebAccount(command.WebAccountProvider, username, WebAccountState.Connected); 
            }
        }

        private async void StoreWebAccount(WebAccount account)
        {
            ApplicationData.Current.RoamingSettings.Values["CurrentUserProviderId"] = account.WebAccountProvider.Id;
            ApplicationData.Current.RoamingSettings.Values["CurrentUserId"] = account.Id;
        }

        private async Task SignOutAsync(WebAccount account)
        {
            ApplicationData.Current.RoamingSettings.Values["CurrentUserProviderId"] = null;
            ApplicationData.Current.RoamingSettings.Values["CurrentUserId"] = null;
        }

        private async Task GetTokenSilentlyAsync()
        {
            string providerId = ApplicationData.Current.RoamingSettings.Values["CurrentUserProviderId"]?.ToString();
            string accountId = ApplicationData.Current.RoamingSettings.Values["CurrentUserId"]?.ToString();

            if (null == providerId || null == accountId)
            {
                return;
            }

            WebAccountProvider provider = await WebAuthenticationCoreManager.FindAccountProviderAsync(providerId);
            WebAccount account = await WebAuthenticationCoreManager.FindAccountAsync(provider, accountId);
            
            WebTokenRequest request = new WebTokenRequest(provider, "wl.basic");

            WebTokenRequestResult result = await WebAuthenticationCoreManager.GetTokenSilentlyAsync(request, account);
            if (result.ResponseStatus == WebTokenRequestStatus.UserInteractionRequired)
            {
                // Unable to get a token silently - you'll need to show the UI
            }
            if (result.ResponseStatus == WebTokenRequestStatus.Success)
            {
                string token = result.ResponseData[0].Token;
            }
        }


        private async void SignOutButton_Click(object sender, RoutedEventArgs e)
        {
            var msaProvider = await WebAuthenticationCoreManager.FindAccountProviderAsync(
                "https://login.microsoft.com", "consumers");
            var request = new WebTokenRequest(msaProvider, "consumers");
            var result = await WebAuthenticationCoreManager.GetTokenSilentlyAsync(request);
            if (result.ResponseStatus == WebTokenRequestStatus.UserInteractionRequired)
            {
                // Unable to get a token silently - you'll need to show the UI
            }
            else if (result.ResponseStatus == WebTokenRequestStatus.Success)
            {
                // Success, use your token
            }

            string id = ""; // ID obtained from calling https://apis.live.net/v5.0/me?access_token=" + token

            var webAccount = await WebAuthenticationCoreManager.FindAccountAsync(msaProvider, id);

        }
    }
}
