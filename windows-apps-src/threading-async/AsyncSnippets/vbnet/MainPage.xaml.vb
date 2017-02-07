Imports Windows.Web.Syndication
' The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

''' <summary>
''' An empty page that can be used on its own or navigated to within a Frame.
''' </summary>
Public NotInheritable Class MainPage
    Inherits Page

    ''' <summary>
    ''' Invoked when this page is about to be displayed in a Frame.
    ''' </summary>
    ''' <param name="e">Event data that describes how this page was reached.  The Parameter
    ''' property is typically used to configure the page.</param>
    Protected Overrides Sub OnNavigatedTo(e As Navigation.NavigationEventArgs)

        Return
    End Sub

    '<SnippetDownloadRSS>
    ' Put the keyword Async on the declaration of the event handler.
    Private Async Sub Button_Click_1(sender As Object, e As RoutedEventArgs)

        Dim client As New Windows.Web.Syndication.SyndicationClient()

        Dim feedUri As New Uri("http://windowsteamblog.com/windows/b/windowsexperience/atom.aspx")

        Try

            Dim feed As SyndicationFeed = Await client.RetrieveFeedAsync(feedUri)

            '<SnippetCodeAfterAwait>
            ' The rest of this method executes after the await operation completes.
            rssOutput.Text = feed.Title.Text & vbCrLf

            For Each item In feed.Items

                rssOutput.Text += item.Title.Text & ", " &
                                  item.PublishedDate.ToString() & vbCrLf

            Next item
            '</SnippetCodeAfterAwait>


        Catch ex As Exception

            ' Log Error.
            rssOutput.Text = "I'm sorry, but I couldn't load the page," &
                             " possibly due to network problems." &
                             "Here's the error message I received: " &
                              ex.ToString()


        End Try

    End Sub
    '</SnippetDownloadRSS>


End Class
