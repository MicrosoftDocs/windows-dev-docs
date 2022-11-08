using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Services.Store;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace InAppPurchasesAndLicenses_UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ImplementTrialPage : Page
    {
        public ImplementTrialPage()
        {
            this.InitializeComponent();
        }

        //<ImplementTrial>
        private StoreContext context = null;
        private StoreAppLicense appLicense = null;

        // Call this while your app is initializing.
        private async void InitializeLicense()
        {
            if (context == null)
            {
                context = StoreContext.GetDefault();
                // If your app is a desktop app that uses the Desktop Bridge, you
                // may need additional code to configure the StoreContext object.
                // For more info, see https://aka.ms/storecontext-for-desktop.
            }

            workingProgressRing.IsActive = true;
            appLicense = await context.GetAppLicenseAsync();
            workingProgressRing.IsActive = false;

            // Register for the licenced changed event.
            context.OfflineLicensesChanged += context_OfflineLicensesChanged;
        }

        private async void context_OfflineLicensesChanged(StoreContext sender, object args)
        {
            // Reload the license.
            workingProgressRing.IsActive = true;
            appLicense = await context.GetAppLicenseAsync();
            workingProgressRing.IsActive = false;

            if (appLicense.IsActive)
            {
                if (appLicense.IsTrial)
                {
                    textBlock.Text = $"This is the trial version. Expiration date: {appLicense.ExpirationDate}";

                    // Show the features that are available during trial only.
                }
                else
                {
                    // Show the features that are available only with a full license.
                }
            }
        }
        //</ImplementTrial>

        private void implementTrialButton_Click(object sender, RoutedEventArgs e)
        {
            InitializeLicense();
        }

        private void mainPageButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
