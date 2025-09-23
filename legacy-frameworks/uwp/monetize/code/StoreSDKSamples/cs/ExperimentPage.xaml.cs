//<SampleExperiment>
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Core;

// Namespace for A/B testing.
using Microsoft.Services.Store.Engagement;

namespace SampleExperiment
{
    public sealed partial class MainPage : Page
    {
        private StoreServicesExperimentVariation variation;
        private StoreServicesCustomEventLogger logger;

        // Assign this variable to the project ID for your experiment from Dev Center.
        private string projectId = "";

        public MainPage()
        {
            this.InitializeComponent();

            // Because this call is not awaited, execution of the current method
            // continues before the call is completed.
#pragma warning disable CS4014
            InitializeExperiment();
#pragma warning restore CS4014
        }

        private async Task InitializeExperiment()
        {
            // Get the current cached variation assignment for the experiment.
            var result = await StoreServicesExperimentVariation.GetCachedVariationAsync(projectId);
            variation = result.ExperimentVariation;

            // Check whether the cached variation assignment needs to be refreshed.
            // If so, then refresh it.
            if (result.ErrorCode != StoreServicesEngagementErrorCode.None || result.ExperimentVariation.IsStale)
            {
                result = await StoreServicesExperimentVariation.GetRefreshedVariationAsync(projectId);

                // If the call succeeds, use the new result. Otherwise, use the cached value.
                if (result.ErrorCode == StoreServicesEngagementErrorCode.None)
                {
                    variation = result.ExperimentVariation;
                }
            }

            // Get remote variables named "buttonText", "r", "g", and "b" from the variation
            // assignment. If no variation assignment is available, the variables default
            // to "Grey button" for the button text and grey RGB value for the button color.
            var buttonText = variation.GetString("buttonText", "Grey Button");
            var r = (byte)variation.GetInt32("r", 128);
            var g = (byte)variation.GetInt32("g", 128);
            var b = (byte)variation.GetInt32("b", 128);

            // Assign button text and color.
            await button.Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal,
                () =>
                {
                    button.Background = new SolidColorBrush(Color.FromArgb(255, r, g, b));
                    button.Content = buttonText;
                    button.Visibility = Visibility.Visible;
                });

            // Log the view event named "userViewedButton" to Dev Center.
            if (logger == null)
            {
                logger = StoreServicesCustomEventLogger.GetDefault();
            }

            logger.LogForVariation(variation, "userViewedButton");
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            // Log the conversion event named "userClickedButton" to Dev Center.
            if (logger == null)
            {
                logger = StoreServicesCustomEventLogger.GetDefault();
            }

            logger.LogForVariation(variation, "userClickedButton");
        }
    }
}
//</SampleExperiment>

