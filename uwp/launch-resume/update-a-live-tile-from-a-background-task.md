---
title: Update a live tile from a background task
description: Use a background task to update your app's live tile with fresh content.
Search.SourceType: Video
ms.assetid: 9237A5BD-F9DE-4B8C-B689-601201BA8B9A
ms.date: 08/08/2024
ms.topic: article
keywords: windows 10, uwp, background task
ms.localizationpriority: medium
---
# Update a live tile from a background task

[!INCLUDE [notes](includes/live-tiles-note.md)]

Use a background task to update your app's live tile with fresh content.

**Important APIs**

- [**IBackgroundTask**](/uwp/api/Windows.ApplicationModel.Background.IBackgroundTask)
- [**BackgroundTaskBuilder**](/uwp/api/Windows.ApplicationModel.Background.BackgroundTaskBuilder)

## Create the background task project  

To enable a live tile for your app, add a new Windows Runtime component project to your solution. This is a separate assembly that the OS loads and runs in the background when a user installs your app.

1.  In Solution Explorer, right-click the solution, click **Add**, and then click **New Project**.
2.  In the **Add New Project** dialog, select the **Windows Runtime Component** template in the **Installed &gt; Other Languages &gt; Visual C# &gt; Windows Universal** section.
3.  Name the project BackgroundTasks and click or tap **OK**. Microsoft Visual Studio adds the new project to the solution.
4.  In the main project, add a reference to the BackgroundTasks project.

## Implement the background task

Implement the [**IBackgroundTask**](/uwp/api/Windows.ApplicationModel.Background.IBackgroundTask) interface to create a class that updates your app's live tile. Your background work goes in the Run method. In this case, the task gets a syndication feed for the MSDN blogs. To prevent the task from closing prematurely while asynchronous code is still running, get a deferral.

1.  In Solution Explorer, rename the automatically generated file, Class1.cs, to BlogFeedBackgroundTask.cs.
2.  In BlogFeedBackgroundTask.cs, replace the automatically generated code with the stub code for the **BlogFeedBackgroundTask** class.
3.  In the Run method implementation, add code for the **GetMSDNBlogFeed** and **UpdateTile** methods.

```cs
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Added during quickstart
using Windows.ApplicationModel.Background;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using Windows.Web.Syndication;

namespace BackgroundTasks
{
    public sealed class BlogFeedBackgroundTask  : IBackgroundTask
    {
        public async void Run( IBackgroundTaskInstance taskInstance )
        {
            // Get a deferral, to prevent the task from closing prematurely
            // while asynchronous code is still running.
            BackgroundTaskDeferral deferral = taskInstance.GetDeferral();

            // Download the feed.
            var feed = await GetMSDNBlogFeed();

            // Update the live tile with the feed items.
            UpdateTile( feed );

            // Inform the system that the task is finished.
            deferral.Complete();
        }

        private static async Task<SyndicationFeed> GetMSDNBlogFeed()
        {
            SyndicationFeed feed = null;

            try
            {
                // Create a syndication client that downloads the feed.  
                SyndicationClient client = new SyndicationClient();
                client.BypassCacheOnRetrieve = true;
                client.SetRequestHeader( customHeaderName, customHeaderValue );

                // Download the feed.
                feed = await client.RetrieveFeedAsync( new Uri( feedUrl ) );
            }
            catch( Exception ex )
            {
                Debug.WriteLine( ex.ToString() );
            }

            return feed;
        }

        private static void UpdateTile( SyndicationFeed feed )
        {
            // Create a tile update manager for the specified syndication feed.
            var updater = TileUpdateManager.CreateTileUpdaterForApplication();
            updater.EnableNotificationQueue( true );
            updater.Clear();

            // Keep track of the number feed items that get tile notifications.
            int itemCount = 0;

            // Create a tile notification for each feed item.
            foreach( var item in feed.Items )
            {
                XmlDocument tileXml = TileUpdateManager.GetTemplateContent( TileTemplateType.TileWide310x150Text03 );

                var title = item.Title;
                string titleText = title.Text == null ? String.Empty : title.Text;
                tileXml.GetElementsByTagName( textElementName )[0].InnerText = titleText;

                // Create a new tile notification.
                updater.Update( new TileNotification( tileXml ) );

                // Don't create more than 5 notifications.
                if( itemCount++ > 5 ) break;
            }
        }

        // Although most HTTP servers do not require User-Agent header, others will reject the request or return
        // a different response if this header is missing. Use SetRequestHeader() to add custom headers.
        static string customHeaderName = "User-Agent";
        static string customHeaderValue = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";

        static string textElementName = "text";
        static string feedUrl = @"http://blogs.msdn.com/b/MainFeed.aspx?Type=BlogsOnly";
    }
}
```

## Set up the package manifest


To set up the package manifest, open it and add a new background task declaration. Set the entry point for the task to the class name, including its namespace.

1.  In Solution Explorer, open Package.appxmanifest.
2.  Click or tap the **Declarations** tab.
3.  Under **Available Declarations**, select **BackgroundTasks** and click **Add**. Visual Studio adds **BackgroundTasks** under **Supported Declarations**.
4.  Under **Supported task types**, ensure that **Timer** is checked.
5.  Under **App settings**, set the entry point to **BackgroundTasks.BlogFeedBackgroundTask**.
6.  Click or tap the **Application UI** tab.
7.  Set **Lock screen notifications** to **Badge and Tile Text**.
8.  Set a path to a 24x24 pixel icon in the **Badge logo** field.
    **Important**  This icon must use monochrome and transparent pixels only.
9.  In the **Small logo** field, set a path to a 30x30 pixel icon.
10. In the **Wide logo** field, set a path to a 310x150 pixel icon.

## Register the background task


Create a [**BackgroundTaskBuilder**](/uwp/api/Windows.ApplicationModel.Background.BackgroundTaskBuilder) to register your task.

> **Note**  Starting in Windows 8.1, background task registration parameters are validated at the time of registration. An error is returned if any of the registration parameters are invalid. Your app must be able to handle scenarios where background task registration fails - for example, use a conditional statement to check for registration errors and then retry failed registration using different parameter values.
 

In your app's main page, add the **RegisterBackgroundTask** method and call it in the **OnNavigatedTo** event handler.

```cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Syndication;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/p/?LinkID=234238

namespace ContosoApp
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

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo( NavigationEventArgs e )
        {
            this.RegisterBackgroundTask();
        }


        private async void RegisterBackgroundTask()
        {
            var backgroundAccessStatus = await BackgroundExecutionManager.RequestAccessAsync();
            if( backgroundAccessStatus == BackgroundAccessStatus.AllowedSubjectToSystemPolicy ||
                backgroundAccessStatus == BackgroundAccessStatus.AlwaysAllowed )
            {
                foreach( var task in BackgroundTaskRegistration.AllTasks )
                {
                    if( task.Value.Name == taskName )
                    {
                        task.Value.Unregister( true );
                    }
                }

                BackgroundTaskBuilder taskBuilder = new BackgroundTaskBuilder();
                taskBuilder.Name = taskName;
                taskBuilder.TaskEntryPoint = taskEntryPoint;
                taskBuilder.SetTrigger( new TimeTrigger( 15, false ) );
                var registration = taskBuilder.Register();
            }
        }

        private const string taskName = "BlogFeedBackgroundTask";
        private const string taskEntryPoint = "BackgroundTasks.BlogFeedBackgroundTask";
    }
}
```

## Debug the background task


To debug the background task, set a breakpoint in the task’s Run method. In the **Debug Location** toolbar, select your background task. This causes the system to call the Run method immediately.

1.  Set a breakpoint in the task’s Run method.
2.  Press F5 or tap **Debug &gt; Start Debugging** to deploy and run the app.
3.  After the app launches, switch back to Visual Studio.
4.  Ensure that the **Debug Location** toolbar is visible. It's on the **View &gt; Toolbars** menu.
5.  On the **Debug Location** toolbar, click the **Suspend** dropdown and select **BlogFeedBackgroundTask**.
6.  Visual Studio suspends execution at the breakpoint.
7.  Press F5 or tap **Debug &gt; Continue** to continue running the app.
8.  Press Shift+F5 or tap **Debug &gt; Stop Debugging** to stop debugging.
9.  Return to the app's tile on the Start screen. After a few seconds, tile notifications appear on your app's tile.

## Related topics


* [**BackgroundTaskBuilder**](/uwp/api/Windows.ApplicationModel.Background.BackgroundTaskBuilder)
* [**TileUpdateManager**](/uwp/api/Windows.UI.Notifications.TileUpdateManager)
* [**TileNotification**](/uwp/api/Windows.UI.Notifications.TileNotification)
* [Support your app with background tasks](support-your-app-with-background-tasks.md)
 