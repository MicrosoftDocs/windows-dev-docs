---
ms.assetid: BF877F23-1238-4586-9C16-246F3F25AE35
description: This article describes how to add adaptive streaming of multimedia content with Microsoft PlayReady content protection to a Universal Windows Platform (UWP) app.
title: Adaptive Streaming with PlayReady
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Adaptive streaming with PlayReady


This article describes how to add adaptive streaming of multimedia content with Microsoft PlayReady content protection to a Universal Windows Platform (UWP) app. 

This feature currently supports playback of Dynamic streaming over HTTP (DASH) content.

HLS (Apple's HTTP Live Streaming) is not supported with PlayReady.

Smooth streaming is also currently not supported natively; however, PlayReady is extensible and by using additional code or libraries, PlayReady-protected Smooth streaming can be supported, leveraging software or even hardware DRM (digital rights management).

This article only deals with the aspects of adaptive streaming specific to PlayReady. For information about implementing adaptive streaming in general, see [Adaptive streaming](adaptive-streaming.md).

This article uses code from the [Adaptive streaming sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/AdaptiveStreaming) in Microsoft's **Windows-universal-samples** repository on GitHub. Scenario 4 deals with using adaptive streaming with PlayReady. You can download the repo in a ZIP file by navigating to the root level of the repository and selecting the **Download ZIP** button.

You will need the following **using** statements:

```csharp
using LicenseRequest;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Windows.Foundation.Collections;
using Windows.Media.Protection;
using Windows.Media.Protection.PlayReady;
using Windows.Media.Streaming.Adaptive;
using Windows.UI.Xaml.Controls;
```

The **LicenseRequest** namespace is from **CommonLicenseRequest.cs**, a PlayReady file provided by Microsoft to licensees.

You will need to declare a few global variables:

```csharp
private AdaptiveMediaSource ams = null;
private MediaProtectionManager protectionManager = null;
private string playReadyLicenseUrl = "";
private string playReadyChallengeCustomData = "";
```

You will also want to declare the following constant:

```csharp
private const uint MSPR_E_CONTENT_ENABLING_ACTION_REQUIRED = 0x8004B895;
```

## Setting up the MediaProtectionManager

To add PlayReady content protection to your UWP app, you will need to set up a [MediaProtectionManager](/uwp/api/Windows.Media.Protection.MediaProtectionManager) object. You do this when initializing your [**AdaptiveMediaSource**](/uwp/api/Windows.Media.Streaming.Adaptive.AdaptiveMediaSource) object.

The following code sets up a [MediaProtectionManager](/uwp/api/Windows.Media.Protection.MediaProtectionManager):

```csharp
private void SetUpProtectionManager(ref MediaElement mediaElement)
{
    protectionManager = new MediaProtectionManager();

    protectionManager.ComponentLoadFailed += 
        new ComponentLoadFailedEventHandler(ProtectionManager_ComponentLoadFailed);

    protectionManager.ServiceRequested += 
        new ServiceRequestedEventHandler(ProtectionManager_ServiceRequested);

    PropertySet cpSystems = new PropertySet();

    cpSystems.Add(
        "{F4637010-03C3-42CD-B932-B48ADF3A6A54}", 
        "Windows.Media.Protection.PlayReady.PlayReadyWinRTTrustedInput");

    protectionManager.Properties.Add("Windows.Media.Protection.MediaProtectionSystemIdMapping", cpSystems);

    protectionManager.Properties.Add(
        "Windows.Media.Protection.MediaProtectionSystemId", 
        "{F4637010-03C3-42CD-B932-B48ADF3A6A54}");

    protectionManager.Properties.Add(
        "Windows.Media.Protection.MediaProtectionContainerGuid", 
        "{9A04F079-9840-4286-AB92-E65BE0885F95}");

    mediaElement.ProtectionManager = protectionManager;
}
```

This code can simply be copied to your app, since it is mandatory for setting up content protection.

The [ComponentLoadFailed](/uwp/api/windows.media.protection.mediaprotectionmanager.componentloadfailed) event is fired when the load of binary data fails. We need to add an event handler to handle this, signaling that the load did not complete:

```csharp
private void ProtectionManager_ComponentLoadFailed(
    MediaProtectionManager sender, 
    ComponentLoadFailedEventArgs e)
{
    e.Completion.Complete(false);
}
```

Similarly, we need to add an event handler for the [ServiceRequested](/uwp/api/windows.media.protection.mediaprotectionmanager.servicerequested) event, which fires when a service is requested. This code checks what kind of request it is, and responds appropriately:

```csharp
private async void ProtectionManager_ServiceRequested(
    MediaProtectionManager sender, 
    ServiceRequestedEventArgs e)
{
    if (e.Request is PlayReadyIndividualizationServiceRequest)
    {
        PlayReadyIndividualizationServiceRequest IndivRequest = 
            e.Request as PlayReadyIndividualizationServiceRequest;

        bool bResultIndiv = await ReactiveIndivRequest(IndivRequest, e.Completion);
    }
    else if (e.Request is PlayReadyLicenseAcquisitionServiceRequest)
    {
        PlayReadyLicenseAcquisitionServiceRequest licenseRequest = 
            e.Request as PlayReadyLicenseAcquisitionServiceRequest;

        LicenseAcquisitionRequest(
            licenseRequest, 
            e.Completion, 
            playReadyLicenseUrl, 
            playReadyChallengeCustomData);
    }
}
```

## Individualization service requests

The following code reactively makes a PlayReady individualization service request. We pass in the request as a parameter to the function. We surround the call in a try/catch block, and if there are no exceptions, we say the request completed successfully:

```csharp
async Task<bool> ReactiveIndivRequest(
    PlayReadyIndividualizationServiceRequest IndivRequest, 
    MediaProtectionServiceCompletion CompletionNotifier)
{
    bool bResult = false;
    Exception exception = null;

    try
    {
        await IndivRequest.BeginServiceRequest();
    }
    catch (Exception ex)
    {
        exception = ex;
    }
    finally
    {
        if (exception == null)
        {
            bResult = true;
        }
        else
        {
            COMException comException = exception as COMException;
            if (comException != null && comException.HResult == MSPR_E_CONTENT_ENABLING_ACTION_REQUIRED)
            {
                IndivRequest.NextServiceRequest();
            }
        }
    }

    if (CompletionNotifier != null) CompletionNotifier.Complete(bResult);
    return bResult;
}
```

Alternatively, we may want to proactively make an individualization service request, in which case we call the following function in place of the code calling `ReactiveIndivRequest` in `ProtectionManager_ServiceRequested`:

```csharp
async void ProActiveIndivRequest()
{
    PlayReadyIndividualizationServiceRequest indivRequest = new PlayReadyIndividualizationServiceRequest();
    bool bResultIndiv = await ReactiveIndivRequest(indivRequest, null);
}
```

## License acquisition service requests

If instead the request was a [PlayReadyLicenseAcquisitionServiceRequest](/uwp/api/Windows.Media.Protection.PlayReady.PlayReadyLicenseAcquisitionServiceRequest), we call the following function to request and acquire the PlayReady license. We tell the **MediaProtectionServiceCompletion** object that we passed in whether the request was successful or not, and we complete the request:

```csharp
async void LicenseAcquisitionRequest(
    PlayReadyLicenseAcquisitionServiceRequest licenseRequest, 
    MediaProtectionServiceCompletion CompletionNotifier, 
    string Url, 
    string ChallengeCustomData)
{
    bool bResult = false;
    string ExceptionMessage = string.Empty;

    try
    {
        if (!string.IsNullOrEmpty(Url))
        {
            if (!string.IsNullOrEmpty(ChallengeCustomData))
            {
                System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
                byte[] b = encoding.GetBytes(ChallengeCustomData);
                licenseRequest.ChallengeCustomData = Convert.ToBase64String(b, 0, b.Length);
            }

            PlayReadySoapMessage soapMessage = licenseRequest.GenerateManualEnablingChallenge();

            byte[] messageBytes = soapMessage.GetMessageBody();
            HttpContent httpContent = new ByteArrayContent(messageBytes);

            IPropertySet propertySetHeaders = soapMessage.MessageHeaders;

            foreach (string strHeaderName in propertySetHeaders.Keys)
            {
                string strHeaderValue = propertySetHeaders[strHeaderName].ToString();

                if (strHeaderName.Equals("Content-Type", StringComparison.OrdinalIgnoreCase))
                {
                    httpContent.Headers.ContentType = MediaTypeHeaderValue.Parse(strHeaderValue);
                }
                else
                {
                    httpContent.Headers.Add(strHeaderName.ToString(), strHeaderValue);
                }
            }

            CommonLicenseRequest licenseAcquision = new CommonLicenseRequest();

            HttpContent responseHttpContent = 
                await licenseAcquision.AcquireLicense(new Uri(Url), httpContent);

            if (responseHttpContent != null)
            {
                Exception exResult = licenseRequest.ProcessManualEnablingResponse(
                                         await responseHttpContent.ReadAsByteArrayAsync());

                if (exResult != null)
                {
                    throw exResult;
                }
                bResult = true;
            }
            else
            {
                ExceptionMessage = licenseAcquision.GetLastErrorMessage();
            }
        }
        else
        {
            await licenseRequest.BeginServiceRequest();
            bResult = true;
        }
    }
    catch (Exception e)
    {
        ExceptionMessage = e.Message;
    }

    CompletionNotifier.Complete(bResult);
}
```

## Initializing the AdaptiveMediaSource

Finally, you will need a function to initialize the [AdaptiveMediaSource](/uwp/api/Windows.Media.Streaming.Adaptive.AdaptiveMediaSource), created from a given [Uri](/dotnet/api/system.uri) and [MediaElement](/uwp/api/Windows.UI.Xaml.Controls.MediaElement). The **Uri** should be the link to the media file (HLS or DASH); the **MediaElement** should be defined in your XAML.

```csharp
async private void InitializeAdaptiveMediaSource(System.Uri uri, MediaElement m)
{
    AdaptiveMediaSourceCreationResult result = await AdaptiveMediaSource.CreateFromUriAsync(uri);
    if (result.Status == AdaptiveMediaSourceCreationStatus.Success)
    {
        ams = result.MediaSource;
        SetUpProtectionManager(ref m);
        m.SetMediaStreamSource(ams);
    }
    else
    {
        // Error handling
    }
}
```

You can call this function in whichever event handles the start of adaptive streaming; for example, in a button click event.

## See also
- [PlayReady DRM](playready-client-sdk.md)