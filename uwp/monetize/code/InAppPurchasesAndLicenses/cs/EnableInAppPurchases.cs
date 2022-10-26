using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.ApplicationModel.Store;

namespace InAppPurchasesAndLicenses
{
    class EnableInAppPurchases
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

        void Example1()
        {
            licenseInformation = CurrentAppSimulator.LicenseInformation;

            //<CodeFeature>
            if (licenseInformation.ProductLicenses["featureName"].IsActive)
            {
                // the customer can access this feature
            }
            else
            {
                // the customer can' t access this feature
            }
            //</CodeFeature>
        }

        //<BuyFeature>
        async void BuyFeature()
        {
            if (!licenseInformation.ProductLicenses["featureName"].IsActive)
            {
                try
                {
                    // The customer doesn't own this feature, so
                    // show the purchase dialog.
                    await CurrentAppSimulator.RequestProductPurchaseAsync("featureName", false);

                    //Check the license state to determine if the in-app purchase was successful.
                }
                catch (Exception)
                {
                    // The in-app purchase was not completed because
                    // an error occurred.
                }
            }
            else
            {
                // The customer already owns this feature.
            }
        }
        //</BuyFeature>

    }
}
