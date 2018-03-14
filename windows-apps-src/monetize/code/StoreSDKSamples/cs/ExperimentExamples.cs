using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using System.Threading.Tasks;
using Windows.UI;
using Microsoft.Services.Store.Engagement;

namespace StoreSDKSamples
{

    class ExperimentExamples
    {
        private Button button = new Button();

        //<ExperimentCodeSample>
        // <Snippet1>
        private StoreServicesExperimentVariation variation;
        private StoreServicesCustomEventLogger logger;
        // </Snippet1>

        // Assign this variable to the project ID for your experiment from Dev Center.
        // The project ID shown below is for example purposes only.
        // <Snippet2>
        private string projectId = "F48AC670-4472-4387-AB7D-D65B095153FB";
        // </Snippet2>

        private async Task InitializeExperiment()
        {
            // Get the current cached variation assignment for the experiment.
            // <Snippet3>
            var result = await StoreServicesExperimentVariation.GetCachedVariationAsync(projectId);
            variation = result.ExperimentVariation;
            // </Snippet3>

            // Refresh the cached variation assignment if necessary.
            // <Snippet4>
            if (result.ErrorCode != StoreServicesEngagementErrorCode.None || result.ExperimentVariation.IsStale)
            {
                result = await StoreServicesExperimentVariation.GetRefreshedVariationAsync(projectId);

                if (result.ErrorCode == StoreServicesEngagementErrorCode.None)
                {
                    variation = result.ExperimentVariation;
                }
            }
            // </Snippet4>

            // Get the remote variable named "buttonText" and assign the value
            // to the button.
            // <Snippet5>
            var buttonText = variation.GetString("buttonText", "Grey Button");
            // </Snippet5>
            // <Snippet6>
            await button.Dispatcher.RunAsync(
                Windows.UI.Core.CoreDispatcherPriority.Normal,
                () =>
                {
                    button.Content = buttonText;
                });
            // </Snippet6>

            // Log the view event named "userViewedButton" to Dev Center.
            // <Snippet7>
            if (logger == null)
            {
                logger = StoreServicesCustomEventLogger.GetDefault();
            }

            logger.LogForVariation(variation, "userViewedButton");
            // </Snippet7>
        }
        //</ExperimentCodeSample>

        // <Snippet8>
        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (logger == null)
            {
                logger = StoreServicesCustomEventLogger.GetDefault();
            }

            logger.LogForVariation(variation, "userClickedButton");
        }
        // </Snippet8>
    }
}
