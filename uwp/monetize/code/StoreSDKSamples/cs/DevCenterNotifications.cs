using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
//<EngagementNamespace>
using Microsoft.Services.Store.Engagement;
//</EngagementNamespace>
using Windows.UI.Notifications;

namespace StoreSDKSamples
{
    class DevCenterNotifications
    {
        private async void Example1()
        {
            //<RegisterNotificationChannelAsync1>
            StoreServicesEngagementManager engagementManager = StoreServicesEngagementManager.GetDefault();
            await engagementManager.RegisterNotificationChannelAsync();
            //</RegisterNotificationChannelAsync1>
        }

        private async void Example2()
        {
            //<RegisterNotificationChannelAsync2>
            StoreServicesNotificationChannelParameters parameters =
                new StoreServicesNotificationChannelParameters();
            parameters.CustomNotificationChannelUri = "Assign your channel URI here";

            StoreServicesEngagementManager engagementManager = StoreServicesEngagementManager.GetDefault();
            await engagementManager.RegisterNotificationChannelAsync(parameters);
            //</RegisterNotificationChannelAsync2>
        }

        private async void Example3()
        {
            //<UnregisterNotificationChannelAsync>
            StoreServicesEngagementManager engagementManager = StoreServicesEngagementManager.GetDefault();
            await engagementManager.UnregisterNotificationChannelAsync();
            //</UnregisterNotificationChannelAsync>
        }
    }

    class MyBackgroundTask : IBackgroundTask
    {
        //<Run>
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            var details = taskInstance.TriggerDetails as ToastNotificationActionTriggerDetail;

            if (details != null)
            {
                StoreServicesEngagementManager engagementManager = StoreServicesEngagementManager.GetDefault();
                string originalArgs = engagementManager.ParseArgumentsAndTrackAppLaunch(details.Argument);

                // Use the originalArgs variable to access the original arguments
                // that were passed to the app.
            }
        }
        //</Run>
    }
}
