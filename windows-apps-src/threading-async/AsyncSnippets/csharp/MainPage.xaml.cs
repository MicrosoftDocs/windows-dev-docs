using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.Web.Syndication;
using Windows.Storage.Pickers;
using Windows.Storage;
using Windows.Storage.Streams;
using System.Threading;
using Windows.Storage.Search;
using Windows.Storage.BulkAccess;
using Windows.Storage.FileProperties;
using Windows.UI.Core;

namespace AsyncApp
{
    partial class MainPage
    {
        const string NEWLINE = "\r\n";
        public MainPage()
        {
            InitializeComponent();
        }

        #region Download RSS

        //<SnippetDownloadRSS>
        // Put the keyword async on the declaration of the event handler.
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {

            Windows.Web.Syndication.SyndicationClient client = new SyndicationClient();

            Uri feedUri
                = new Uri("http://windowsteamblog.com/windows/b/windowsexperience/atom.aspx");

            try
            {
                SyndicationFeed feed = await client.RetrieveFeedAsync(feedUri);

                // The rest of this method executes after await RetrieveFeedAsync completes.
                rssOutput.Text = feed.Title.Text + Environment.NewLine;

                foreach (SyndicationItem item in feed.Items)
                {
                    rssOutput.Text += item.Title.Text + ", " +
                                     item.PublishedDate.ToString() + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                // Log Error.
                rssOutput.Text =
                    "I'm sorry, but I couldn't load the page," +
                    " possibly due to network problems." +
                    "Here's the error message I received: "
                    + ex.ToString();
            }
        }
        //</SnippetDownloadRSS>
        #endregion

    }

}
    