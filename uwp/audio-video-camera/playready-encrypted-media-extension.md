---
ms.assetid: 79C284CA-C53A-4C24-807E-6D4CE1A29BFA
description: This section describes how to modify your PlayReady web app to support the changes made from the previous Windows 8.1 version to the Windows 10 version.
title: PlayReady Encrypted Media Extension
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# PlayReady Encrypted Media Extension



This section describes how to modify your PlayReady web app to support the changes made from the previous Windows 8.1 version to the Windows 10 version.

Using PlayReady media elements in Internet Explorer enables developers to create web apps capable of providing PlayReady content to the user while enforcing the access rules defined by the content provider. This section describes how to add PlayReady media elements to your existing web apps by using only HTML5 and JavaScript.

## What's new in PlayReady Encrypted Media Extension

This section provides a list of changes made to the PlayReady Encrypted Media Extension (EME) to enable PlayReady content protection on Windows 10.

The following list describes the new features and changes made to PlayReady Encrypted Media Extension for Windows 10:

-   Added hardware digital rights management (DRM).

    Hardware-based content protection support enables secure playback of high definition (HD) and ultra-high definition (UHD) content on multiple device platforms. Key material (including private keys, content keys, and any other key material used to derive or unlock said keys), and decrypted compressed and uncompressed video samples are protected by leveraging hardware security.

-   Provides proactive acquisition of non-persistent licenses.
-   Provides acquisition of multiple licenses in one message.

    You can either use a PlayReady object with multiple key identifiers (KeyIDs) as in Windows 8.1, or use [content decryption model data (CDMData)](/previous-versions/windows/apps/dn457361(v=ieb.10)) with multiple KeyIDs.

    > [!NOTE]
    > In Windows 10, multiple key identifiers are supported under &lt;KeyID&gt; in CDMData.

-   Added real time expiration support, or limited duration license (LDL).

    Provides the ability to set real-time expiration on licenses.

-   Added HDCP Type 1 (version 2.2) policy support.
-   Miracast is now implicit as an output.
-   Added secure stop.

    Secure stop provides the means for a PlayReady device to confidently assert to a media streaming service that media playback has stopped for any given piece of content.

-   Added audio and video license separation.

    Separate tracks prevent video from being decoded as audio; enabling more robust content protection. Emerging standards are requiring separate keys for audio and visual tracks.

-   Added MaxResDecode.

    This feature was added to limit playback of content to a maximum resolution even when in possession of a more capable key (but not a license). It supports cases where multiple stream sizes are encoded with a single key.

## Encrypted Media Extension support in PlayReady

This section describes the version of the W3C Encrypted Media Extension supported by PlayReady.

PlayReady for Web Apps is currently bound to the [W3C Encrypted Media Extension (EME) draft of May 10, 2013](https://www.w3.org/TR/2013/WD-encrypted-media-20130510/). This support will be changed to the updated EME specification in future versions of Windows.

## Use hardware DRM

This section describes how your web app can use PlayReady hardware DRM, and how to disable hardware DRM if the protected content does not support it.

To use PlayReady hardware DRM, your JavaScript web app should use the **isTypeSupported** EME method with a key system identifier of `com.microsoft.playready.hardware` to query for PlayReady hardware DRM support from the browser.

Occasionally, some content is not supported in hardware DRM. Cocktail content is never supported in hardware DRM; if you want to play cocktail content, you must opt out of hardware DRM. Some hardware DRM will support HEVC and some will not; if you want to play HEVC content and hardware DRM doesn’t support it, you will want to opt out as well.

> [!NOTE]
> To determine whether HEVC content is supported, after instantiating `com.microsoft.playready`, use the [**PlayReadyStatics.CheckSupportedHardware**](/uwp/api/windows.media.protection.playready.playreadystatics.checksupportedhardware) method.

## Add secure stop to your web app

This section describes how to add secure stop to your web app.

Secure stop provides the means for a PlayReady device to confidently assert to a media streaming service that media playback has stopped for any given piece of content. This capability ensures your media streaming services provide accurate enforcement and reporting of usage limitations on different devices for a given account.

There are two primary scenarios for sending a secure stop challenge:

-   When the media presentation stops because end of content was reached or when the user stopped the media presentation somewhere in the middle.
-   When the previous session ends unexpectedly (for example, due to a system or app crash). The app will need to query, either at startup or shutdown, for any outstanding secure stop sessions and send challenge(s) separate from any other media playback.

The following procedures describe how to set up secure stop for various scenarios.

To set up secure stop for a normal end of a presentation:

1.  Register the **onEnded** event before playback starts.
2.  The **onEnded** event handler needs to call `removeAttribute("src")` from the video/audio element object to set the source to **NULL** which will trigger the media foundation to tear down the topology, destroy the decryptor(s), and set the stop state.
3.  You can start the secure stop CDM session inside the handler to send the secure stop challenge to the server to notify the playback has stopped at this time, but it can be done later as well.

To set up secure stop if the user navigates away from the page or closes down the tab or browser:

-   No app action is required to record the stop state; it will be recorded for you.

To set up secure stop for custom page controls or user actions (such as custom navigation buttons or starting a new presentation before the current presentation completed):

-   When custom user action occurs, the app needs to set the source to **NULL** which will trigger the media foundation to tear down the topology, destroy the decryptor(s), and set the stop state.

The following example demonstrates how to use secure stop in your web app:

```JavaScript
// JavaScript source code

var g_prkey = null;
var g_keySession = null;
var g_fUseSpecificSecureStopSessionID = false;
var g_encodedMeteringCert = 'Base64 encoded of your metering cert (aka publisher cert)';

// Note: g_encodedLASessionId is the CDM session ID of the proactive or reactive license acquisition 
//       that we want to initiate the secure stop process.
var g_encodedLASessionId = null;

function main()
{
    ...

    g_prkey = new MSMediaKeys("com.microsoft.playready");

    ...

    // add 'onended' event handler to the video element
    // Assume 'myvideo' is the ID of the video element
    var videoElement = document.getElementById("myvideo");
    videoElement.onended = function (e) { 

        //
        // Calling removeAttribute("src") will set the source to null
        // which will trigger the MF to tear down the topology, destroy the
        // decryptor(s) and set the stop state.  This is required in order
        // to set the stop state.
        //
        videoElement.removeAttribute("src");
        videoElement.load();

        onEndOfStream();
    };
}

function onEndOfStream()
{
    ...

    createSecureStopCDMSession();

    ...    
}

function createSecureStopCDMSession()
{
    try{    
        var targetMediaCodec = "video/mp4";
        var customData = "my custom data";

        var encodedSessionId = g_encodedLASessionId;
        if( !g_fUseSpecificSecureStopSessionID )
        {
            // Use "*" (wildcard) as the session ID to include all secure stop sessions
            // TODO: base64 encode "*" and place encoded result to encodedSessionId
        }

        var int8ArrayCDMdata = formatSecureStopCDMData( encodedSessionId, customData,  g_encodedMeteringCert );
        var emptyArrayofInitData = new Uint8Array();

        g_keySession = g_prkey.createSession(targetMediaCodec, emptyArrayofInitData, int8ArrayCDMdata);

        addPlayreadyKeyEventHandler();

    } catch( e )
    {
        // TODO: Handle exception
    }
}

function addPlayreadyKeyEventHandler()
{
    // add 'keymessage' eventhandler   
    g_keySession.addEventListener('mskeymessage', function (event) {

        // TODO: Get the keyMessage from event.message.buffer which contains the secure stop challenge
        //       The keyMessage format for the secure stop is similar to LA as below:
        //
        //            <PlayReadyKeyMessage type="SecureStop" >
        //              <SecureStop version="1.0" >
        //                <Challenge encoding="base64encoded">
        //                    secure stop challenge
        //                </Challenge>
        //                <HttpHeaders>
        //                    <HttpHeader>
        //                      <name>Content-Type</name>
        //                         <value>"content type data"</value>
        //                    </HttpHeader>
        //                    <HttpHeader>
        //                         <name>SOAPAction</name>
        //                         <value>soap action</value>
        //                     </HttpHeader>
        //                    ....
        //                </HttpHeaders>
        //              </SecureStop>
        //            </PlayReadyKeyMessage>
                
        // TODO: send the secure stop challenge to a server that handles the secure stop challenge

        // TODO: Receive and response and call event.target.Update() to process the response
    });
    
    // add 'keyerror' eventhandler
    g_keySession.addEventListener('mskeyerror', function (event) {
        var session = event.target;
        
        ...

        session.close();
    });
    
    // add 'keyadded' eventhandler
    g_keySession.addEventListener('mskeyadded', function (event) {
        
        var session = event.target;

        ...

        session.close();             
    });
}

/**
* desc@ formatSecureStopCDMData
*   generate playready CDMData
*   CDMData is in xml format:
*   <PlayReadyCDMData type="SecureStop">
*     <SecureStop version="1.0">
*       <SessionID>B64 encoded session ID</SessionID>
*       <CustomData>B64 encoded custom data</CustomData>
*       <ServerCert>B64 encoded server cert</ServerCert>
*     </SecureCert>
* </PlayReadyCDMData>        
*/
function formatSecureStopCDMData(encodedSessionId, customData, encodedPublisherCert) 
{
    var encodedCustomData = null;

    // TODO: base64 encode the custom data and place the encoded result to encodedCustomData

    var CDMDataStr = "<PlayReadyCDMData type=\"SecureStop\">" +
                     "<SecureStop version=\"1.0\" >" +
                     "<SessionID>" + encodedSessionId + "</SessionID>" +
                     "<CustomData>" + encodedCustomData + "</CustomData>" +
                     "<ServerCert>" + encodedPublisherCert + "</ServerCert>" +
                     "</SecureStop></PlayReadyCDMData>";
    
    var int8ArrayCDMdata = null

    // TODO: Convert CDMDataStr to Uint8 byte array and palce the converted result to int8ArrayCDMdata

    return int8ArrayCDMdata;
}
```

> [!NOTE]
> The secure stop data’s `<SessionID>B64 encoded session ID</SessionID>` in the sample above can be an asterisk (\*), which is a wild card for all the secure stop sessions recorded. That is, the **SessionID** tag can be a specific session, or a wild card (\*) to select all the secure stop sessions.

## Programming considerations for Encrypted Media Extension

This section lists the programming considerations that you should take into account when creating your PlayReady-enabled web app for Windows 10.

The **MSMediaKeys** and **MSMediaKeySession** objects created by your app must be kept alive until your app closes. One way of ensuring these objects stay alive is to assign them as global variables (the variables would become out of scope and subject to garbage collection if declared as a local variable inside of a function). For example, the following sample assigns the variables *g\_msMediaKeys* and *g\_mediaKeySession* as global variables, which are then assigned to the **MSMediaKeys** and **MSMediaKeySession** objects in the function.

``` syntax
var g_msMediaKeys;
var g_mediaKeySession;

function foo() {
  ...
  g_msMediaKeys = new MSMediaKeys("com.microsoft.playready");
  ...
  g_mediaKeySession = g_msMediaKeys.createSession("video/mp4", intiData, null);
  g_mediaKeySession.addEventListener(this.KEYMESSAGE_EVENT, function (e) 
  {
    ...
    downloadPlayReadyKey(url, keyMessage, function (data) 
    {
      g_mediaKeySession.update(data);
    });
  });
  g_mediaKeySession.addEventListener(this.KEYADDED_EVENT, function () 
  {
    ...
    g_mediaKeySession.close();
    g_mediaKeySession = null;
  });
}
```

For more information, see the [sample applications](https://github.com/microsoft/Windows-universal-samples/tree/master/Samples/PlayReady).

## See also
- [PlayReady DRM](playready-client-sdk.md)