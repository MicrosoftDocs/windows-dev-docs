# Using VAPID and Webpush in UWP 
## Introduction 

The introduction of the web push standard has allowed websites to start acting more like apps, sending notifications to users even when they aren’t on the page. This is a huge boon for web developers, but something that app developers have been doing for years. However, the web push spec includes some new features of interest for apps developers as well.  

The VAPID authentication protocol was added as a part of the web push spec to allow app servers to authenticate with a push server in a vendor agnostic way. It allows a website to not need to know what browser it is running on before sending a push notification since all vendors use the same protocol. This is a significant improvement over having to implement a different push protocol for each platform you support. Starting in the Fall Creators Update apps are going be able to use VAPID authentication to send push notifications to either UWP.  

This will save development time on the server for new apps and simplify cross platform support for existing apps. As well it means that enterprise apps or sideloaded apps can send notifications without registering in the Windows Store first. This will hopefully open up new ways to engage with your customers across all the platforms you support.  

## Alternate Channels 
These new VAPID channels are called alternate channels in UWP and provide similar functionality what you expect from a web push channel.  They can trigger an app background task to run, enable message encryption, and allow for multiple channels from a single app. For more information about the difference between the different channel types, please see Choosing the right channel (add link) 

## Basic Steps on the Client 

The basic process of setting up an alternate channel on the client is similar to setting up a primary or secondary channel. First the app must register for a channel with the WNS server. Then it must register to run as a background task. Finally, once a notification is sent, the background task will be triggered and it is up to your app to handle the event.  

### Get a channel 
To create an alternate channel, the app must provide 2 pieces of information: the public key for its server and the name of the channel it is creating. The details about the server keys are available in the web push spec but we recommend using a standard web push library on the server to generate the keys.  
The channel ID is important because an app can create multiple alternate channels. Each channel must be identified by a unique ID that will be included along with any notification payloads sent along that channel.  
```csharp
private async void AppCreateVAPIDChannelAsync(string appChannelId, IBuffer applicationServerKey) 
{ 
    // From the spec: applicationServer Key (p256dh):  
    //               An Elliptic curve Diffie–Hellman public key on the P-256 curve 
    //               (that is, the NIST secp256r1 elliptic curve).   
    //               The resulting key is an uncompressed point in ANSI X9.62 format             
    // ChannelId is an app provided value for it to identify the channel later.  
    // case of this app it is from the set { "Football", "News", "Baseball" } 
    PushNotificationChannel webChannel = await PushNotificationChannelManager.Current.CreateRawPushNotificationChannelWithAlternateKeyForApplicationAsync(applicationServerKey, appChannelId); 
 
    //Save the channel  
    AppUpdateChannelMapping(appChannelId, webChannel); 
             
    //Tell our web service that we have a new channel to push notifications to 
    AppPassChannelToSite(webChannel.Uri); 
} 
```
You can also see the sample that the app is sending the channel back up to it server as well as saving it locally. Saving the channel ID locally means that the app will be able to differentiate between the channels it created and also knows when to renew channels to prevent them from expiring.

Just like every other type of push notification channel, web channels can expire as well. We recommend that apps create a new channel every time it is launched. This is the easiest way to prevent channels from expiring without your app knowing.   

### Register for a background task 

Once the app has created an alternate channel it should register to receive the notifications either in the foreground or the background. In this case, it is going to register to use the one-process model to receive the notifications either in the foreground or the background.  

```csharp
var builder = new BackgroundTaskBuilder(); 
builder.Name = "Push Trigger"; 
builder.SetTrigger(new PushNotificationTrigger()); 
BackgroundTaskRegistration task = builder.Register(); 
```
### Receive the notifications 

Once the app has registered to receive the notifications, it needs to be able to process the incoming notifications. Since there could be many different alternate channels registered for a single app, it is important to check the channel ID before processing the notification.  

```csharp
protected override void OnBackgroundActivated(BackgroundActivatedEventArgs args) 
{ 
    base.OnBackgroundActivated(args); 
    var raw = args.TaskInstance.TriggerDetails as RawNotification; 
    switch (raw.ChannelId) 
    { 
        case "Football": 
            AppPostFootballScore(raw.Content); 
            break; 
        case "News": 
            AppProcessNewsItem(raw.Content); 
            break; 
        case "Baseball": 
            AppProcessBaseball(raw.Content); 
            break; 
 
        default: 
            //We don't have the channelID registered, should only happen in the case of a 
            //caching issue on the application server 
            break; 
    }                           
} 
```

Note the channel ID will not be set if the notification is coming from a primary channel.  

### Note on encryption 

You have the full flexibility to use whatever encryption scheme that you find more useful for your app. In some cases, it will be enough to rely on the standard in TLS between the server and any Windows device. In other cases though it might make sense to use the web push encryption scheme or another scheme of your design.  

If you wish to do this, the key is the use the raw.Headers property. It will contain all the encryption headers that were included in the POST request to the push server. From there your app can use the keys to decrypt the message.  

## Conclusions 

Using alternate channels is a great way to access push notifications if your app can’t use a primary channel or if you want to share code between your website and app. Setting up a channel is easy and familiar to anyone who has either used the web push standard or worked with Windows push notifications before.  

We’re looking forward to seeing how apps are going to use alternate channels, let us know if you have any questions in the comments below.  
