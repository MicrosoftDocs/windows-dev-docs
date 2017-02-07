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
using Windows.ApplicationModel.Store;

namespace InAppPurchasesAndLicenses
{
    public class Example1
    {
        private LicenseInformation licenseInformation;

        //<InitializeLicenseTest>
        void InitializeApp()
        {
            // Some app initialization code...

            // Initialize the license info for use in the app that is uploaded to the Store.
            // Uncomment the following line in the release version of your app.
            //   licenseInformation = CurrentApp.LicenseInformation;

            // Initialize the license info for testing.
            // Comment the following line in the release version of your app.
            licenseInformation = CurrentAppSimulator.LicenseInformation;

            // Other app initialization code...
        }
        //</InitializeLicenseTest>
    }

    public class Example2
    {
        private LicenseInformation licenseInformation;

        //<InitializeLicenseTestWithEvent>
        void InitializeApp()
        {
            // Some app initialization code...

            // Initialize the license info for use in the app that is uploaded to the Store.
            // Uncomment the following line in the release version of your app.
            //   licenseInformation = CurrentApp.LicenseInformation;

            // Initialize the license info for testing.
            // Comment the following line in the release version of your app.
            licenseInformation = CurrentAppSimulator.LicenseInformation;

            // Register for the license state change event.
            licenseInformation.LicenseChanged += LicenseInformation_LicenseChanged;

            // Other app initialization code...
        }

        void LicenseInformation_LicenseChanged()
        {
            // This method is defined later.
            ReloadLicense(); 
        }
        //</InitializeLicenseTestWithEvent>

        //<ReloadLicense>
        void ReloadLicense()
        {
            if (licenseInformation.IsActive)
            {
                if (licenseInformation.IsTrial)
                {
                    // Show the features that are available during trial only.
                }
                else
                {
                    // Show the features that are available only with a full license.
                }
            }
            else
            {
                // A license is inactive only when there' s an error.
            }
        }
        //</ReloadLicense>

        //<DisplayTrialVersionExpirationTime>
        void DisplayTrialVersionExpirationTime()
        {
            if (licenseInformation.IsActive)
            {
                if (licenseInformation.IsTrial)
                {
                    var longDateFormat = new Windows.Globalization.DateTimeFormatting.DateTimeFormatter("longdate");

                    // Display the expiration date using the DateTimeFormatter.
                    // For example, longDateFormat.Format(licenseInformation.ExpirationDate)

                    var daysRemaining = (licenseInformation.ExpirationDate - DateTime.Now).Days;

                    // Let the user know the number of days remaining before the feature expires
                }
                else
                {
                    // ...
                }
            }
            else
            {
                // ...
            }
        }
        //</DisplayTrialVersionExpirationTime>
    }

    public class Example3
    {
        private LicenseInformation licenseInformation;

        //<InitializeLicenseRetailWithEvent>
        void InitializeApp()
        {
            // Some app initialization code...

            // Initialize the license info for use in the app that is uploaded to the Store.
            // Uncomment the following line in the release version of your app.
            licenseInformation = CurrentApp.LicenseInformation;

            // Initialize the license info for testing.
            // Comment the following line in the release version of your app.
            // licenseInformation = CurrentAppSimulator.LicenseInformation;

            // Register for the license state change event.
            licenseInformation.LicenseChanged += LicenseInformation_LicenseChanged;

            // Other app initialization code...
        }
        //</InitializeLicenseRetailWithEvent>

        void LicenseInformation_LicenseChanged()
        {

        }
    }
}