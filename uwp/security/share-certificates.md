---
title: Share certificates between apps
description: Universal Windows Platform (UWP) apps that require secure authentication beyond a user Id and password combination can use certificates for authentication.
ms.assetid: 159BA284-9FD4-441A-BB45-A00E36A386F9
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, security
ms.localizationpriority: medium
---
# Share certificates between apps




Universal Windows Platform (UWP) apps that require secure authentication beyond a user Id and password combination can use certificates for authentication. Certificate authentication provides a high level of trust when authenticating a user. In some cases, a group of services will want to authenticate a user for multiple apps. This article shows how you can authenticate multiple apps using the same certificate, and how you can provide convenient code for a user to import a certificate that was provided to access secured web services.

Apps can authenticate to a web service using a certificate, and multiple apps can use a single certificate from the certificate store to authenticate the same user. If a certificate does not exist in the store, you can add code to your app to import a certificate from a PFX file.

## Enable Microsoft Internet Information Services (IIS) and client certificate mapping


This article uses Microsoft Internet Information Services (IIS) for example purposes. IIS is not enabled by default. You can enable IIS by using the Control Panel.

1.  Open the Control Panel and select **Programs**.
2.  Select **Turn Windows features on or off**.
3.  Expand **Internet Information Services** and then expand **World Wide Web Services**. Expand **Application Development Features** and select **ASP.NET 3.5** and **ASP.NET 4.5**. Making these selections will automatically enable **Internet Information Services**.
4.  Click **OK** to apply the changes.

## Create and publish a secured web service


1.  Run Microsoft Visual Studio as administrator and select **New Project** from the start page. Administrator access is required to publish a web service to an IIS server. In the New Project dialog, change the framework to **.NET Framework 3.5**. Select **Visual C#** -&gt; **Web** -&gt; **Visual Studio** -&gt; **ASP.NET Web Service Application**. Name the application "FirstContosoBank". Click **OK** to create the project.
2.  In the **Service1.asmx.cs** file, replace the default **HelloWorld** web method with the following "Login" method.
    ```cs
            [WebMethod]
            public string Login()
            {
                // Verify certificate with CA
                var cert = new System.Security.Cryptography.X509Certificates.X509Certificate2(
                    this.Context.Request.ClientCertificate.Certificate);
                bool test = cert.Verify();
                return test.ToString();
            }
    ```

3.  Save the **Service1.asmx.cs** file.
4.  In the **Solution Explorer**, right-click the "FirstContosoBank" app and select **Publish**.
5.  In the **Publish Web** dialog, create a new profile and name it "ContosoProfile". Click **Next.**
6.  On the next page, enter the server name for your IIS server, and specify a site name of "Default Web Site/FirstContosoBank". Click **Publish** to publish your web service.

## Configure your web service to use client certificate authentication


1.  Run the **Internet Information Services (IIS) Manager**.
2.  Expand the sites for your IIS server. Under the **Default Web Site**, select the new "FirstContosoBank" web service. In the **Actions** section, select **Advanced Settings...**.
3.  Set the **Application Pool** to **.NET v2.0** and click **OK**.
4.  In the **Internet Information Services (IIS) Manager**, select your IIS server and then double-click **Server Certificates**. In the **Actions** section, select **Create Self-Signed Certificate...**. Enter "ContosoBank" as the friendly name for the certificate and click **OK**. This will create a new certificate for use by the IIS server in the form of "&lt;server-name&gt;.&lt;domain-name&gt;".
5.  In the **Internet Information Services (IIS) Manager**, select the default web site. In the **Actions** section, select **Binding** and then click **Add...**. Select "https" as the type, set the port to "443", and enter the full host name for your IIS server ("&lt;server-name&gt;.&lt;domain-name&gt;"). Set the SSL certificate to "ContosoBank". Click **OK**. Click **Close** in the **Site Bindings** window.
6.  In the **Internet Information Services (IIS) Manager**, select the "FirstContosoBank" web service. Double-click **SSL Settings**. Check **Require SSL**. Under **Client certificates**, select **Require**. In the **Actions** section, click **Apply**.
7.  You can verify that the web service is configured correctly by opening your web browser and entering the following web address: "https://&lt;server-name&gt;.&lt;domain-name&gt;/FirstContosoBank/Service1.asmx". For example, "https://myserver.example.com/FirstContosoBank/Service1.asmx". If your web service is configured correctly, you will be prompted to select a client certificate in order to access the web service.

You can repeat the previous steps to create multiple web services that can be accessed using the same client certificate.

## Create a UWP app that uses certificate authentication


Now that you have one or more secured web services, your apps can use certificates to authenticate to those web services. When you make a request to an authenticated web service using the [**HttpClient**](/uwp/api/Windows.Web.Http.HttpClient) object, the initial request will not contain a client certificate. The authenticated web service will respond with a request for client authentication. When this occurs, the Windows client will automatically query the certificate store for available client certificates. Your user can select from these certificates to authenticate to the web service. Some certificates are password protected, so you will need to provide the user with a way to input the password for a certificate.

If there are no client certificates available, then the user will need to add a certificate to the certificate store. You can include code in your app that enables a user to select a PFX file that contains a client certificate and then import that certificate into the client certificate store.

**Tip**  You can use makecert.exe to create a PFX file to use with this quickstart. For information on using makecert.exe, see [MakeCert.](/windows/desktop/SecCrypto/makecert)

 

1.  Open Visual Studio and create a new project from the start page. Name the new project "FirstContosoBankApp". Click **OK** to create the new project.
2.  In the MainPage.xaml file, add the following XAML to the default **Grid** element. This XAML includes a button to browse for a PFX file to import, a text box to enter a password for a password-protected PFX file, a button to import a selected PFX file, a button to log in to the secured web service, and a text block to display the status of the current action.
    ```xml
    <Button x:Name="Import" Content="Import Certificate (PFX file)" HorizontalAlignment="Left" Margin="352,305,0,0" VerticalAlignment="Top" Height="77" Width="260" Click="Import_Click" FontSize="16"/>
    <Button x:Name="Login" Content="Login" HorizontalAlignment="Left" Margin="611,305,0,0" VerticalAlignment="Top" Height="75" Width="240" Click="Login_Click" FontSize="16"/>
    <TextBlock x:Name="Result" HorizontalAlignment="Left" Margin="355,398,0,0" TextWrapping="Wrap" VerticalAlignment="Top" Height="153" Width="560"/>
    <PasswordBox x:Name="PfxPassword" HorizontalAlignment="Left" Margin="483,271,0,0" VerticalAlignment="Top" Width="229"/>
    <TextBlock HorizontalAlignment="Left" Margin="355,271,0,0" TextWrapping="Wrap" Text="PFX password" VerticalAlignment="Top" FontSize="18" Height="32" Width="123"/>
    <Button x:Name="Browse" Content="Browse for PFX file" HorizontalAlignment="Left" Margin="352,189,0,0" VerticalAlignment="Top" Click="Browse_Click" Width="499" Height="68" FontSize="16"/>
    <TextBlock HorizontalAlignment="Left" Margin="717,271,0,0" TextWrapping="Wrap" Text="(Optional)" VerticalAlignment="Top" Height="32" Width="83" FontSize="16"/>
    ```
    
3.  Save the MainPage.xaml file.
4.  In the MainPage.xaml.cs file, add the following using statements.
    ```cs
    using Windows.Web.Http;
    using System.Text;
    using Windows.Security.Cryptography.Certificates;
    using Windows.Storage.Pickers;
    using Windows.Storage;
    using Windows.Storage.Streams;
    ```

5.  In the MainPage.xaml.cs file, add the following variables to the **MainPage** class. They specify the address for the secured "Login" method of your "FirstContosoBank" web service, and a global variable that holds a PFX certificate to import into the certificate store. Update the &lt;server-name&gt; to the fully-qualified server name for your Microsoft Internet Information Server (IIS) server.
    ```cs
    private Uri requestUri = new Uri("https://<server-name>/FirstContosoBank/Service1.asmx?op=Login");
    private string pfxCert = null;
    ```

6.  In the MainPage.xaml.cs file, add the following click handler for the login button and method to access the secured web service.
    ```cs
    private void Login_Click(object sender, RoutedEventArgs e)
    {
        MakeHttpsCall();
    }

    private async void MakeHttpsCall()
    {

        StringBuilder result = new StringBuilder("Login ");
        HttpResponseMessage response;
        try
        {
            Windows.Web.Http.HttpClient httpClient = new Windows.Web.Http.HttpClient();
            response = await httpClient.GetAsync(requestUri);
            if (response.StatusCode == HttpStatusCode.Ok)
            {
                result.Append("successful");
            }
            else
            {
                result = result.Append("failed with ");
                result = result.Append(response.StatusCode);
            }
        }
        catch (Exception ex)
        {
            result = result.Append("failed with ");
            result = result.Append(ex.Message);
        }

        Result.Text = result.ToString();
    }
    ```

7.  In the MainPage.xaml.cs file, add the following click handlers for the button to browse for a PFX file and the button to import a selected PFX file into the certificate store.
    ```cs
    private async void Import_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            Result.Text = "Importing selected certificate into user certificate store....";
            await CertificateEnrollmentManager.UserCertificateEnrollmentManager.ImportPfxDataAsync(
                pfxCert,
                PfxPassword.Password,
                ExportOption.Exportable,
                KeyProtectionLevel.NoConsent,
                InstallOptions.DeleteExpired,
                "Import Pfx");

            Result.Text = "Certificate import succeeded";
        }
        catch (Exception ex)
        {
            Result.Text = "Certificate import failed with " + ex.Message;
        }
    }

    private async void Browse_Click(object sender, RoutedEventArgs e)
    {

        StringBuilder result = new StringBuilder("Pfx file selection ");
        FileOpenPicker pfxFilePicker = new FileOpenPicker();
        pfxFilePicker.FileTypeFilter.Add(".pfx");
        pfxFilePicker.CommitButtonText = "Open";
        try
        {
            StorageFile pfxFile = await pfxFilePicker.PickSingleFileAsync();
            if (pfxFile != null)
            {
                IBuffer buffer = await FileIO.ReadBufferAsync(pfxFile);
                using (DataReader dataReader = DataReader.FromBuffer(buffer))
                {
                    byte[] bytes = new byte[buffer.Length];
                    dataReader.ReadBytes(bytes);
                    pfxCert = System.Convert.ToBase64String(bytes);
                    PfxPassword.Password = string.Empty;
                    result.Append("succeeded");
                }
            }
            else
            {
                result.Append("failed");
            }
        }
        catch (Exception ex)
        {
            result.Append("failed with ");
            result.Append(ex.Message); ;
        }

        Result.Text = result.ToString();
    }
    ```

8.  Run your app and log in to your secured web service as well as import a PFX file into the local certificate store.

You can use these steps to create multiple apps that use the same user certificate to access the same or different secured web services.