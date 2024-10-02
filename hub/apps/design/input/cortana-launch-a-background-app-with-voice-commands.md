---
title: Activate a background app in Cortana using voice commands | Cortana UWP design and development
description: Extend Cortana with features from your app (as a background task) using voice commands.

ms.assetid: e2c7eae3-6beb-4156-92a5-474bba53451e
ms.date: 09/24/2019
ms.topic: article

keywords: cortana
---

# Activate a background app in Cortana using voice commands  

>[!WARNING]
> This feature is no longer supported as of the Windows 10 May 2020 Update (version 2004, codename "20H1").

In addition to using voice commands within **Cortana** to access system features, you may also extend **Cortana** with features and functionality from your app (as a background task) using voice commands that specify an action or command to run. When an app handles a voice command in the background, it does not take focus. Instead, it returns all feedback and results through the **Cortana** canvas and the **Cortana** voice.  

> [!NOTE]
> **Important APIs**
>
> - [**Windows.ApplicationModel.VoiceCommands**](/uwp/api/Windows.ApplicationModel.VoiceCommands)
> - [**VCD elements and attributes v1.2**](/uwp/schemas/voicecommands/voice-command-elements-and-attributes-1-2)

Apps may be activated to the foreground (the app takes focus) or activated in the background (**Cortana** retains focus), depending on the complexity of the interaction. For example, voice commands that require additional context or user input (such as sending a message to a specific contact) are best handled in a foreground app, while basic commands (such as listing upcoming trips) may be handled in **Cortana** through a background app.  

If you want to activate an app to the foreground using voice commands, see [Activate a foreground app with voice commands through Cortana](cortana-launch-a-foreground-app-with-voice-commands.md).  

> [!NOTE]
> A voice command is a single utterance with a specific intent, defined in a Voice Command Definition (VCD) file, directed at an installed app via **Cortana**.
>
> A VCD file defines one or more voice commands, each with a unique intent.
>
> Voice command definitions can vary in complexity. They can support anything from a single, constrained utterance to a collection of more flexible, natural language utterances, all denoting the same intent.

We use a trip planning and management app named **Adventure Works** integrated into the **Cortana** UI, shown here, to demonstrate many of the concepts and features we discuss. For more info, see the [Cortana voice command sample](https://go.microsoft.com/fwlink/p/?LinkID=619899).

:::image type="content" source="images/cortana/cortana-overview.png" alt-text="Screenshot of Cortana launching foreground app":::

To view an **Adventure Works** trip without **Cortana**, a user would launch the app and navigate to the **Upcoming trips** page.  

Using voice commands through **Cortana** to launch your app in the background, the user may instead just say, `Adventure Works, when is my trip to Las Vegas?`. Your app handles the command and **Cortana** displays results along with your app icon and other app info, if provided.  

:::image type="content" source="images/cortana/cortana-backgroundapp-result.png" alt-text="Screenshot of Cortana with a basic query and result screen using the AdventureWorks app in the background":::

The following basic steps add voice-command functionality and extend **Cortana** with background functionality from your app using speech or keyboard input.

1. Create an app service (see [**Windows.ApplicationModel.AppService**](/uwp/api/Windows.ApplicationModel.AppService)) that **Cortana** invokes in the background.  
2. Create a VCD file. The VCD file is an XML document that defines all the spoken commands that the user may say to initiate actions or invoke commands when activating your app. See [**VCD elements and attributes v1.2**](/uwp/schemas/voicecommands/voice-command-elements-and-attributes-1-2).  
3. Register the command sets in the VCD file when the app is launched.  
4. Handle the background activation of the app service and the running of the voice command.  
5. Display and speak the appropriate feedback to the voice command within **Cortana**.  

> [!TIP]
> **Prerequisites**
>
> If you're new to developing Universal Windows Platform (UWP) apps, have a look through these topics to get familiar with the technologies discussed here.
>
> - [Create your first app](/windows/uwp/get-started/your-first-app)
> - Learn about events with [Events and routed events overview](/windows/uwp/xaml-platform/events-and-routed-events-overview)
>
> **User experience guidelines**
>
> See [Cortana design guidelines](cortana-design-guidelines.md)  for info about how to integrate your app with **Cortana** and [Speech interactions](speech-interactions.md) for helpful tips on designing a useful and engaging speech-enabled app.

## Create a New Solution with a Primary Project in Visual Studio  

1. Launch Microsoft Visual Studio 2015.  
    The Visual Studio 2015 Start page appears.  

2. On the **File** menu, select **New** > **Project**.  
    The **New Project** dialog appears. The left pane of the dialog lets you select the type of templates to display.  

3. In the left pane, expand **Installed > Templates > Visual C\# > Windows**, then pick the **Universal** template group. The center pane of the dialog displays a list of project templates for Universal Windows Platform (UWP) apps.  
4. In the center pane, select the **Blank App (Universal Windows)** template.  
    The **Blank App** template creates a minimal UWP app that compiles and runs. The **Blank App** template includes no user-interface controls or data. You add controls to the app using this page as a guide.  

5. In the **Name** text box, type your project name. Example: Use `AdventureWorks`.  
6. Click on the **OK** button to create the project.  
    Microsoft Visual Studio creates your project and displays it in the **Solution Explorer**.  

## Add Image Assets to Primary Project and Specify them in the App Manifest  

UWP apps should automatically select the most appropriate images. The selection is based upon specific settings and device capabilities (high contrast, effective pixels, locale, and so on). You must provide the images and ensure that you use the appropriate naming convention and folder organization within your app project for the different resource versions.  
If you do not provide the recommended resource versions, then the user experience may suffer in the following ways.

- Accessibility
- Localization  
- Image quality  
The resource versions are used to adapt the following changes in the user experience.  
- User preferences  
- Abilities  
- Device type  
- Location  

For more detail about image resources for high contrast and scale factors, visit the Guidelines for tile and icon assets page located at [msdn.microsoft.com/windows/uwp/controls-and-patterns/tiles-and-notifications-app-assets](/windows/uwp/app-resources/images-tailored-for-scale-theme-contrast).  

You must name resources using qualifiers. Resource qualifiers are folder and filename modifiers that identify the context in which a particular version of a resource should be used.  

The standard naming convention is `foldername/qualifiername-value[_qualifiername-value]/filename.qualifiername-value[_qualifiername-value].ext`.  
Example: `images/logo.scale-100_contrast-white.png`, which may refer to code using just the root folder and the filename: `images/logo.png`.  
For more information, visit the How to name resources using qualifiers page located at [msdn.microsoft.com/library/windows/apps/xaml/hh965324.aspx](/previous-versions/windows/apps/hh965324(v=win.10)).  

Microsoft recommends that you mark the default language on string resource files (such as `en-US\resources.resw`) and the default scale factor on images (such as `logo.scale-100.png`), even if you do not currently plan to provide localized or multiple resolution resources. However, at a minimum, Microsoft recommends that you provide assets for 100, 200, and 400 scale factors.  

>[!IMPORTANT]
> The app icon used in the title area of the **Cortana** canvas is the Square44x44Logo icon specified in the `Package.appxmanifest` file.  
> You may also specify an icon for each entry in the content area of the **Cortana** canvas. Valid image sizes for the results icons are:
>
> - 68w x 68h
> - 68w x 92h  
> - 280w x 140h  

The content tile is not validated until a [**VoiceCommandResponse**](/uwp/api/Windows.ApplicationModel.VoiceCommands.VoiceCommandResponse) object is passed to the [**VoiceCommandServiceConnection**](/uwp/api/Windows.ApplicationModel.VoiceCommands.VoiceCommandServiceConnection) class. If you pass a [**VoiceCommandResponse**](/uwp/api/Windows.ApplicationModel.VoiceCommands.VoiceCommandResponse) object to **Cortana** that includes a content tile with an image that does not adhere to these size ratios, then an exception may occur.  

Example: The **Adventure Works** app (`VoiceCommandService\\AdventureWorksVoiceCommandService.cs`) specifies a simple, grey square (`GreyTile.png`) on the [**VoiceCommandContentTile**](/uwp/api/Windows.ApplicationModel.VoiceCommands.VoiceCommandContentTile) class using the [**TitleWith68x68IconAndText**](/uwp/api/Windows.ApplicationModel.VoiceCommands.VoiceCommandContentTileType) tile template. The logo variants are located in `VoiceCommandService\\Images`, and are retrieved using the [**GetFileFromApplicationUriAsync**](/uwp/api/Windows.Storage.StorageFile) method.

```csharp
var destinationTile = new VoiceCommandContentTile();  

destinationTile.ContentTileType = VoiceCommandContentTileType.TitleWith68x68IconAndText;
destinationTile.Image = await StorageFile.GetFileFromApplicationUriAsync(
    new Uri("ms-appx:///AdventureWorks.VoiceCommands/Images/GreyTile.png")
);  
```

## Create an App Service Project

1. Right-click on your Solution name, select **New > Project**.  
2. Under **Installed > Templates > Visual C\# > Windows > Universal**, select **Windows Runtime Component**. The **Windows Runtime Component** is the component that implements the app service ([**Windows.ApplicationModel.AppService**](/uwp/api/Windows.ApplicationModel.AppService)).  
3. Type a name for the project and click on the **OK** button.  
    Example: `VoiceCommandService`.  
4. In **Solution Explorer**, select the `VoiceCommandService` project and rename the `Class1.cs` file generated by Visual Studio.
    Example: The **Adventure Works** uses `AdventureWorksVoiceCommandService.cs`.  
5. Click on the **Yes** button; when asked if you want to rename all occurrences of `Class1.cs`.  
6. In the `AdventureWorksVoiceCommandService.cs` file:
    1. Add the following using directive.  
        `using Windows.ApplicationModel.Background;`  
    2. When you create a new project, the project name is used as the default root namespace in all files. Rename the namespace to nest the app service code under the primary project.
        Example: `namespace AdventureWorks.VoiceCommands`.  
    3. Right-click on the app service project name in Solution Explorer and select **Properties**.  
    4. On the **Library** tab, update the **Default namespace** field with this same value.  
        Example: `AdventureWorks.VoiceCommands`).  
    5. Create a new class that implements the [**IBackgroundTask**](/uwp/api/Windows.ApplicationModel.Background.IBackgroundTask) interface. This class requires a [**Run**](/uwp/api/Windows.ApplicationModel.Background.IBackgroundTask) method, which is the entry point when Cortana recognizes the voice command.  

    Example: A basic background task class from the **Adventure Works** app.  

    >[!NOTE]
    > The background task class itself, as well as all classes in the background task project, must be sealed public classes.  

    ```csharp
    namespace AdventureWorks.VoiceCommands
    {
        ...
        
        /// <summary>
        /// The VoiceCommandService implements the entry point for all voice commands.
        /// The individual commands supported are described in the VCD xml file. 
        /// The service entry point is defined in the appxmanifest.
        /// </summary>
        public sealed class AdventureWorksVoiceCommandService : IBackgroundTask
        {
            ...

            /// <summary>
            /// The background task entrypoint. 
            /// 
            /// Background tasks must respond to activation by Cortana within 0.5 second, and must 
            /// report progress to Cortana every 5 seconds (unless Cortana is waiting for user
            /// input). There is no running time limit on the background task managed by Cortana,
            /// but developers should use plmdebug (https://msdn.microsoft.com/library/windows/hardware/jj680085%28v=vs.85%29.aspx)
            /// on the Cortana app package in order to prevent Cortana timing out the task during
            /// debugging.
            /// 
            /// The Cortana UI is dismissed if Cortana loses focus. 
            /// The background task is also dismissed even if being debugged. 
            /// Use of Remote Debugging is recommended in order to debug background task behaviors. 
            /// Open the project properties for the app package (not the background task project), 
            /// and enable Debug -> "Do not launch, but debug my code when it starts". 
            /// Alternatively, add a long initial progress screen, and attach to the background task process while it runs.
            /// </summary>
            /// <param name="taskInstance">Connection to the hosting background service process.</param>
            public void Run(IBackgroundTaskInstance taskInstance)
            {
              //
              // TODO: Insert code 
              //
              //
        }
      }
    }
    ```  

7. Declare your background task as an **AppService** in the app manifest.  
    1. In **Solution Explorer**, right-click on the `Package.appxmanifest` file and select **View Code**.  
    2. Find the [`Application`](/uwp/schemas/appxpackage/uapmanifestschema/element-application) element.  
    3. Add an [`Extensions`](/uwp/schemas/appxpackage/uapmanifestschema/element-1-extensions) element to the [`Application`](/uwp/schemas/appxpackage/uapmanifestschema/element-application) element.  
    4. Add a [`uap:Extension`](/uwp/schemas/appxpackage/uapmanifestschema/element-1-extension) element to the [`Extensions`](/uwp/schemas/appxpackage/uapmanifestschema/element-1-extensions) element.  
    5. Add a `Category` attribute to the `uap:Extension` element and set the value of the `Category` attribute to `windows.appService`.  
    6. Add an `EntryPoint` attribute to the `uap: Extension` element and set the value of the `EntryPoint` attribute to the name of the class that implements [`IBackgroundTask`](/uwp/api/Windows.ApplicationModel.Background.IBackgroundTask).  
        Example: `AdventureWorks.VoiceCommands.AdventureWorksVoiceCommandService`.  
    7. Add a [`uap:AppService`](/uwp/schemas/appxpackage/uapmanifestschema/element-uap-appservice) element to the `uap:Extension` element.  
    8. Add a `Name` attribute to the [`uap:AppService`](/uwp/schemas/appxpackage/uapmanifestschema/element-uap-appservice) element and set the value of the `Name` attribute to a name for the app service, in this case `AdventureWorksVoiceCommandService`.  
    9. Add a second [`uap:Extension`](/uwp/schemas/appxpackage/uapmanifestschema/element-1-extension) element to the [`Extensions`](/uwp/schemas/appxpackage/uapmanifestschema/element-1-extensions) element.  
    10. Add a `Category` attribute to this [`uap:Extension`](/uwp/schemas/appxpackage/uapmanifestschema/element-1-extension) element and set the value of the `Category` attribute to `windows.personalAssistantLaunch`.  

    Example: A manifest from the Adventure Works app.

    ```xml
    <Package>
        <Applications>
            <Application>
            
                <Extensions>
                    <uap:Extension Category="windows.appService" EntryPoint="CortanaBack1.VoiceCommands.AdventureWorksVoiceCommandService">
                        <uap:AppService Name="AdventureWorksVoiceCommandService"/>
                    </uap:Extension>
                    <uap:Extension Category="windows.personalAssistantLaunch"/>
                </Extensions>
                
            <Application>
        <Applications>
    </Package>
    ```  

8. Add this app service project as a reference in the primary project.  
    1. Right-click on the **References**.  
    2. Select **Add Reference...**.  
    3. In the **Reference Manager** dialog, expand **Projects** and select the app service project.  
    4. Click on the **OK** button.  

## Create a VCD File

1. In Visual Studio, right-click on your primary project name, select **Add > New Item**. Add an **XML File**.  
2. Type a name for the [**VCD**](/uwp/schemas/voicecommands/voice-command-elements-and-attributes-1-2) file.  
    Example: `AdventureWorksCommands.xml`.
3. Click on the **Add** button.  
4. In **Solution Explorer**, select the [**VCD**](/uwp/schemas/voicecommands/voice-command-elements-and-attributes-1-2) file.  
5. In the **Properties** window, set **Build action** to **Content**, and then set **Copy to output directory** to **Copy if newer**.  

## Edit the VCD File  

1. Add a `VoiceCommands` element with an `xmlns` attribute pointing to `https://schemas.microsoft.com/voicecommands/1.2`.  
2. For each language supported by your app, create a [`CommandSet`](/previous-versions/windows/dn722331(v=win.10)) element that includes the voice commands supported by your app.  
    You are able to declare multiple [`CommandSet`](/previous-versions/windows/dn722331(v=win.10)) elements, each with a different [`xml:lang`](/previous-versions/windows/dn722331(v=win.10)) attribute so your app to be used in different markets. For example, an app for the United States might have a [`CommandSet`](/previous-versions/windows/dn722331(v=win.10)) for English and a [`CommandSet`](/previous-versions/windows/dn722331(v=win.10)) for Spanish.  

    >[!IMPORTANT]
    > To activate an app and initiate an action using a voice command, the app must register a VCD file that includes a [`CommandSet`](/previous-versions/windows/dn722331(v=win.10)) element with a language that matches the speech language indicated in the device of the user. The speech language is located in **Settings > System > Speech > Speech Language**.  

3. Add a `Command` element for each command you want to support.  
    Each `Command` declared in a [**VCD**](/uwp/schemas/voicecommands/voice-command-elements-and-attributes-1-2) file must include this information:  
    - A `Name` attribute that your application uses to identify the voice command at runtime.  
    - An `Example` element that includes a phrase describing how a user invokes the command. **Cortana** shows the example when the user says `What can I say?`, `Help`, or taps **See more**.  
    - A `ListenFor` element that includes the words or phrases that your app recognizes as a command. Each `ListenFor` element may contain references to one or more `PhraseList` elements that contain specific words relevant to the command.  

       >[!NOTE]
       > `ListenFor` elements must not be programmatically modified. However, `PhraseList` elements associated with `ListenFor` elements may be programmatically modified. Applications should modify the content of the `PhraseList` element at runtime based on the data set generated as the user uses the app.
       >
       > For more information, see [Dynamically modify Cortana VCD phrase lists](cortana-dynamically-modify-voice-command-definition-vcd-phrase-lists.md).  

    - A `Feedback` element that includes the text for **Cortana** to display and speak as the application is launched.  

A `Navigate` element indicates that the voice command activates the app to the foreground. In this example, the ```showTripToDestination``` command is a foreground task.  

A `VoiceCommandService` element indicates that the voice command activates the app in the background. The value of the `Target` attribute of this element should match the value of the `Name` attribute of the [`uap:AppService`](/uwp/schemas/appxpackage/uapmanifestschema/element-uap-appservice) element in the package.appxmanifest file. In this example, the `whenIsTripToDestination` and `cancelTripToDestination` commands are background tasks that specify the name of the app service as `AdventureWorksVoiceCommandService`.  

For more detail, see the [**VCD elements and attributes v1.2**](/uwp/schemas/voicecommands/voice-command-elements-and-attributes-1-2) reference.  

Example: A portion of the [**VCD**](/uwp/schemas/voicecommands/voice-command-elements-and-attributes-1-2) file that defines the `en-us` voice commands for the **Adventure Works** app.  

```xml
<?xml version="1.0" encoding="utf-8" ?>
<VoiceCommands xmlns="https://schemas.microsoft.com/voicecommands/1.2">
<CommandSet xml:lang="en-us" Name="AdventureWorksCommandSet_en-us">
    <AppName> Adventure Works </AppName>
    <Example> Show trip to London </Example>
    
    <Command Name="showTripToDestination">
        <Example> Show trip to London </Example>
        <ListenFor RequireAppName="BeforeOrAfterPhrase"> show [my] trip to {destination} </ListenFor>
        <ListenFor RequireAppName="ExplicitlySpecified"> show [my] {builtin:AppName} trip to {destination} </ListenFor>
        <Feedback> Showing trip to {destination} </Feedback>
        <Navigate />
    </Command>
      
    <Command Name="whenIsTripToDestination">
        <Example> When is my trip to Las Vegas?</Example>
        <ListenFor RequireAppName="BeforeOrAfterPhrase"> when is [my] trip to {destination}</ListenFor>
        <ListenFor RequireAppName="ExplicitlySpecified"> when is [my] {builtin:AppName} trip to {destination} </ListenFor>
        <Feedback> Looking for trip to {destination}</Feedback>
        <VoiceCommandService Target="AdventureWorksVoiceCommandService"/>
    </Command>
    
    <Command Name="cancelTripToDestination">
        <Example> Cancel my trip to Las Vegas </Example>
        <ListenFor RequireAppName="BeforeOrAfterPhrase"> cancel [my] trip to {destination}</ListenFor>
        <ListenFor RequireAppName="ExplicitlySpecified"> cancel [my] {builtin:AppName} trip to {destination} </ListenFor>
        <Feedback> Cancelling trip to {destination}</Feedback>
        <VoiceCommandService Target="AdventureWorksVoiceCommandService"/>
    </Command>

    <PhraseList Label="destination">
        <Item>London</Item>
        <Item>Las Vegas</Item>
        <Item>Melbourne</Item>
        <Item>Yosemite National Park</Item>
    </PhraseList>
</CommandSet>
```  

## Install the VCD Commands  

Your app must run once to install the VCD.  

>[!NOTE]
> Voice command data is not preserved across app installations. To ensure the voice command data for your app remains intact, consider initializing your VCD file each time your app is launched or activated, or maintain a setting that indicates if the VCD is currently installed.  

In the `app.xaml.cs` file:  

1. Add the following using directive:

   ```csharp
   using Windows.Storage;
   ```

2. Mark the `OnLaunched` method with the async modifier.  

   ```csharp
   protected async override void OnLaunched(LaunchActivatedEventArgs e)
   ```  

3. Call the [`InstallCommandDefinitionsFromStorageFileAsync`](/uwp/api/Windows.ApplicationModel.VoiceCommands.VoiceCommandDefinitionManager) method in the [`OnLaunched`](/uwp/api/Windows.UI.Xaml.Application) handler to register the voice commands that should be recognized.  
    Example: The Adventure Works app defines a [`StorageFile`](/uwp/api/Windows.Storage.StorageFile) object.  
    Example: Call the [`GetFileAsync`](/uwp/api/Windows.Storage.StorageFolder) method to initialize the [`StorageFile`](/uwp/api/Windows.Storage.StorageFile) object with the `AdventureWorksCommands.xml` file.  
    The [`StorageFile`](/uwp/api/Windows.Storage.StorageFile) object is then passed to [`InstallCommandDefinitionsFromStorageFileAsync`](/uwp/api/Windows.ApplicationModel.VoiceCommands.VoiceCommandDefinitionManager) method.  

   ```csharp
   try {
      // Install the main VCD. 
      StorageFile vcdStorageFile = await Package.Current.InstalledLocation.GetFileAsync(
            @"AdventureWorksCommands.xml"
      );
       
      await Windows.ApplicationModel.VoiceCommands.VoiceCommandDefinitionManager.InstallCommandDefinitionsFromStorageFileAsync(vcdStorageFile);
     
      // Update phrase list.
      ViewModel.ViewModelLocator locator = App.Current.Resources["ViewModelLocator"] as ViewModel.ViewModelLocator;
      if(locator != null) {
            await locator.TripViewModel.UpdateDestinationPhraseList();
        }
    }
    catch (Exception ex) {
        System.Diagnostics.Debug.WriteLine("Installing Voice Commands Failed: " + ex.ToString());
    }
    ```  

## Handle Activation  

Specify how your app responds to subsequent voice command activations.

>[!NOTE]
> You must launch your app at least once after the voice command sets have been installed.  

1. Confirm that your app was activated by a voice command.  

    Override the [`Application.OnActivated`](/uwp/api/Windows.UI.Xaml.Application) event and check whether [**IActivatedEventArgs**](/uwp/api/Windows.ApplicationModel.Activation.IActivatedEventArgs).[**Kind**](/uwp/api/Windows.ApplicationModel.Activation.IActivatedEventArgs) is [**VoiceCommand**](/uwp/api/Windows.ApplicationModel.Activation.ActivationKind).  

2. Determine the name of the command and what was spoken.  

    Get a reference to a [`VoiceCommandActivatedEventArgs`](/uwp/api/Windows.ApplicationModel.Activation.VoiceCommandActivatedEventArgs) object from the [**IActivatedEventArgs**](/uwp/api/Windows.ApplicationModel.Activation.IActivatedEventArgs) and query the [`Result`](/uwp/api/Windows.ApplicationModel.Activation.VoiceCommandActivatedEventArgs) property for a [`SpeechRecognitionResult`](/uwp/api/Windows.Media.SpeechRecognition.SpeechRecognitionResult) object.  

    To determine what the user said, check the value of [**Text**](/uwp/api/Windows.Media.SpeechRecognition.SpeechRecognitionResult) or the semantic properties of the recognized phrase in the [`SpeechRecognitionSemanticInterpretation`](/uwp/api/Windows.Media.SpeechRecognition.SpeechRecognitionSemanticInterpretation) dictionary.  

3. Take the appropriate action in your app, such as navigating to the desired page.  

    >[!NOTE]
    > If you need to refer to your VCD, visit the [Edit the VCD File](#edit-the-vcd-file) section.

    After receiving the speech-recognition result for the voice command, you get the command name from the first value in the [`RulePath`](/uwp/api/Windows.Media.SpeechRecognition.SpeechRecognitionResult) array. Since the VCD file defines more than one possible voice command, you must verify that the value matches the command names in the VCD and take the appropriate action.  

    The most common action for an application is to navigate to a page with content relevant to the context of the voice command.  
    Example: Open the **TripPage** page and pass in the value of the voice command, how the command was input, and the recognized destination phrase (if applicable). Alternatively, the app may send a navigation parameter to the [**SpeechRecognitionResult**](/uwp/api/Windows.Media.SpeechRecognition.SpeechRecognitionResult) when navigating to the **TripPage** page.  

    You are able to find out whether the voice command that launched your app was actually spoken, or whether it was typed in as text, from the [`SpeechRecognitionSemanticInterpretation.Properties`](/uwp/api/Windows.Media.SpeechRecognition.SpeechRecognitionSemanticInterpretation) dictionary using the **commandMode** key. The value of that key will be either `voice` or `text`. If the value of the key is `voice`, consider using speech synthesis ([**Windows.Media.SpeechSynthesis**](/uwp/api/Windows.Media.SpeechSynthesis)) in your app to provide the user with spoken feedback.  

    Use the [**SpeechRecognitionSemanticInterpretation.Properties**](/uwp/api/Windows.Media.SpeechRecognition.SpeechRecognitionSemanticInterpretation) to find out the content spoken in the `PhraseList` or `PhraseTopic` constraints of a `ListenFor` element. The dictionary key is the value of the `Label` attribute of the `PhraseList` or `PhraseTopic` element.
    Example: The following code for How to access the value of **{destination}** phrase.  

    ```csharp
    /// <summary>
    /// Entry point for an application activated by some means other than normal launching. 
    /// This includes voice commands, URI, share target from another app, and so on. 
    /// 
    /// NOTE:
    /// A previous version of the VCD file might remain in place 
    /// if you modify it and update the app through the store. 
    /// Activations might include commands from older versions of your VCD. 
    /// Try to handle these commands gracefully.
    /// </summary>
    /// <param name="args">Details about the activation method.</param>
    protected override void OnActivated(IActivatedEventArgs args) {
        base.OnActivated(args);
        
        Type navigationToPageType;
        ViewModel.TripVoiceCommand? navigationCommand = null;
        
        // Voice command activation.
        if (args.Kind == ActivationKind.VoiceCommand) {
            // Event args may represent many different activation types. 
            // Cast the args so that you only get useful parameters out.
            var commandArgs = args as VoiceCommandActivatedEventArgs;
            
            Windows.Media.SpeechRecognition.SpeechRecognitionResult speechRecognitionResult = commandArgs.Result;
            
            // Get the name of the voice command and the text spoken.
            // See VoiceCommands.xml for supported voice commands.
            string voiceCommandName = speechRecognitionResult.RulePath[0];
            string textSpoken = speechRecognitionResult.Text;
            
            // commandMode indicates whether the command was entered using speech or text.
            // Apps should respect text mode by providing silent (text) feedback.
            string commandMode = this.SemanticInterpretation("commandMode", speechRecognitionResult);
            
            switch (voiceCommandName) {
                case "showTripToDestination":
                    // Access the value of {destination} in the voice command.
                    string destination = this.SemanticInterpretation("destination", speechRecognitionResult);
                    
                    // Create a navigation command object to pass to the page.
                    navigationCommand = new ViewModel.TripVoiceCommand(
                        voiceCommandName,
                        commandMode,
                        textSpoken,
                        destination
                    );
              
                    // Set the page to navigate to for this voice command.
                    navigationToPageType = typeof(View.TripDetails);
                    break;
                default:
                    // If not able to determine what page to launch, then go to the default entry point.
                    navigationToPageType = typeof(View.TripListView);
                    break;
            }
        }
        // Protocol activation occurs when a card is selected within Cortana (using a background task).
        else if (args.Kind == ActivationKind.Protocol) {
            // Extract the launch context. In this case, use the destination from the phrase set (passed
            // along in the background task inside Cortana), which makes no attempt to be unique. A unique id or 
            // identifier is ideal for more complex scenarios. The destination page is left to check if the 
            // destination trip still exists, and navigate back to the trip list if it does not.
            var commandArgs = args as ProtocolActivatedEventArgs;
            Windows.Foundation.WwwFormUrlDecoder decoder = new Windows.Foundation.WwwFormUrlDecoder(commandArgs.Uri.Query);
            var destination = decoder.GetFirstValueByName("LaunchContext");
            
            navigationCommand = new ViewModel.TripVoiceCommand(
                "protocolLaunch",
                "text",
                "destination",
                destination
            );
            
            navigationToPageType = typeof(View.TripDetails);
        }
        else {
            // If launched using any other mechanism, fall back to the main page view.
            // Otherwise, the app will freeze at a splash screen.
            navigationToPageType = typeof(View.TripListView);
        }
        
        // Repeat the same basic initialization as OnLaunched() above, taking into account whether
        // or not the app is already active.
        Frame rootFrame = Window.Current.Content as Frame;
        
        // Do not repeat app initialization when the Window already has content,
        // just ensure that the window is active.
        if (rootFrame == null) {
            // Create a frame to act as the navigation context and navigate to the first page.
            rootFrame = new Frame();
            App.NavigationService = new NavigationService(rootFrame);
            
            rootFrame.NavigationFailed += OnNavigationFailed;
            
            // Place the frame in the current window.
            Window.Current.Content = rootFrame;
        }
        
        // Since the expectation is to always show a details page, navigate even if 
        // a content frame is in place (unlike OnLaunched).
        // Navigate to either the main trip list page, or if a valid voice command
        // was provided, to the details page for that trip.
        rootFrame.Navigate(navigationToPageType, navigationCommand);
        
        // Ensure the current window is active
        Window.Current.Activate();
    }

    /// <summary>
    /// Returns the semantic interpretation of a speech result. 
    /// Returns null if there is no interpretation for that key.
    /// </summary>
    /// <param name="interpretationKey">The interpretation key.</param>
    /// <param name="speechRecognitionResult">The speech recognition result to get the semantic interpretation from.</param>
    /// <returns></returns>
    private string SemanticInterpretation(string interpretationKey, SpeechRecognitionResult speechRecognitionResult) {
        return speechRecognitionResult.SemanticInterpretation.Properties[interpretationKey].FirstOrDefault();
    }
    ```  

## Handle the Voice Command in the App Service  

Process the voice command in the app service.  

1. Add the following using directives to your voice command service file.  
    Example: `AdventureWorksVoiceCommandService.cs`.  

    ```csharp
        using Windows.ApplicationModel.VoiceCommands;
        using Windows.ApplicationModel.Resources.Core;
        using Windows.ApplicationModel.AppService;
    ```  

2. Take a service deferral so your app service is not terminated while handling the voice command.  
3. Confirm that your background task is running as an app service activated by a voice command.  
    1. Cast the [**IBackgroundTaskInstance.TriggerDetails**](/uwp/api/Windows.ApplicationModel.Background.IBackgroundTaskInstance) to [**Windows.ApplicationModel.AppService.AppServiceTriggerDetails**](/uwp/api/Windows.ApplicationModel.AppService.AppServiceTriggerDetails).  
    2. Check that [**IBackgroundTaskInstance.TriggerDetails.Name**](/uwp/api/Windows.ApplicationModel.Background.IBackgroundTaskRegistration) is the name of the app service in the `Package.appxmanifest` file.  
4. Use [**IBackgroundTaskInstance.TriggerDetails**](/uwp/api/Windows.ApplicationModel.Background.IBackgroundTaskInstance) to create a [**VoiceCommandServiceConnection**](/uwp/api/Windows.ApplicationModel.VoiceCommands.VoiceCommandServiceConnection) to **Cortana** to retrieve the voice command.
5. Register an event handler for [**VoiceCommandServiceConnection**](/uwp/api/Windows.ApplicationModel.VoiceCommands.VoiceCommandServiceConnection).  [**VoiceCommandCompleted**](/uwp/api/Windows.ApplicationModel.VoiceCommands.VoiceCommandServiceConnection) to receive notification when the app service is closed due to a user cancellation.  
6. Register an event handler for the [**IBackgroundTaskInstance.Canceled**](/uwp/api/Windows.ApplicationModel.Background.IBackgroundTaskInstance) to receive notification when the app service is closed due to an unexpected failure.  
7. Determine the name of the command and what was spoken.  
    1. Use the [**VoiceCommand**](/uwp/api/Windows.ApplicationModel.VoiceCommands.VoiceCommand).[**CommandName**](/uwp/api/Windows.ApplicationModel.VoiceCommands.VoiceCommand) property to determine the name of the voice command.  
    2. To determine what the user said, check the value of [**Text**](/uwp/api/Windows.Media.SpeechRecognition.SpeechRecognitionResult) or the semantic properties of the recognized phrase in the [`SpeechRecognitionSemanticInterpretation`](/uwp/api/Windows.Media.SpeechRecognition.SpeechRecognitionSemanticInterpretation) dictionary.  
8. Take the appropriate action in your app service.  
9. Display and speak the feedback to the voice command using **Cortana**.  
    1. Determine the strings that you want **Cortana** to display and speak to the user in response to the voice command and create a [`VoiceCommandResponse`](/uwp/api/Windows.ApplicationModel.VoiceCommands.VoiceCommandResponse) object. For guidance on how to select the feedback strings that **Cortana** shows and speaks, see [Cortana design guidelines](cortana-design-guidelines.md).  
    2. Use the [**VoiceCommandServiceConnection**](/uwp/api/Windows.ApplicationModel.VoiceCommands.VoiceCommandServiceConnection) instance to report progress or completion to **Cortana** by calling [**ReportProgressAsync**](/uwp/api/Windows.ApplicationModel.VoiceCommands.VoiceCommandServiceConnection) or [**ReportSuccessAsync**](/uwp/api/Windows.ApplicationModel.VoiceCommands.VoiceCommandServiceConnection) with the `VoiceCommandServiceConnection` object.  

    >[!NOTE]
    > If you need to refer to your VCD, visit the [Edit the VCD File](#edit-the-vcd-file) section.  

    ```csharp
    public sealed class VoiceCommandService : IBackgroundTask {
        private BackgroundTaskDeferral serviceDeferral;
        VoiceCommandServiceConnection voiceServiceConnection;
        
        public async void Run(IBackgroundTaskInstance taskInstance) {
            //Take a service deferral so the service isn&#39;t terminated.
            this.serviceDeferral = taskInstance.GetDeferral();
            
            taskInstance.Canceled += OnTaskCanceled;
            
            var triggerDetails = taskInstance.TriggerDetails as AppServiceTriggerDetails;
    
            if (triggerDetails != null &amp;&amp; 
                triggerDetails.Name == "AdventureWorksVoiceServiceEndpoint") {
                try {
                    voiceServiceConnection = 
                    VoiceCommandServiceConnection.FromAppServiceTriggerDetails(
                        triggerDetails);
                    voiceServiceConnection.VoiceCommandCompleted += 
                    VoiceCommandCompleted;
                    
                    VoiceCommand voiceCommand = await 
                    voiceServiceConnection.GetVoiceCommandAsync();
                    
                    switch (voiceCommand.CommandName) {
                        case "whenIsTripToDestination":
                            {
                                var destination = 
                                voiceCommand.Properties["destination"][0];
                                SendCompletionMessageForDestination(destination);
                                break;
                            }
                            
                            // As a last resort, launch the app in the foreground.
                        default:
                            LaunchAppInForeground();
                            break;
                    }
                }
                finally {
                    if (this.serviceDeferral != null) {
                        // Complete the service deferral.
                        this.serviceDeferral.Complete();
                    }
                }
            }
        }
        
        private void VoiceCommandCompleted(VoiceCommandServiceConnection sender,
            VoiceCommandCompletedEventArgs args) {
            if (this.serviceDeferral != null) {
                // Insert your code here.
                // Complete the service deferral.
                this.serviceDeferral.Complete();
            }
        }
        
        private async void SendCompletionMessageForDestination(
            string destination) {
            // Take action and determine when the next trip to destination
            // Insert code here.
            
            // Replace the hardcoded strings used here with strings 
            // appropriate for your application.
            
            // First, create the VoiceCommandUserMessage with the strings 
            // that Cortana will show and speak.
            var userMessage = new VoiceCommandUserMessage();
            userMessage.DisplayMessage = "Here's your trip.";
            userMessage.SpokenMessage = "Your trip to Vegas is on August 3rd.";
            
            // Optionally, present visual information about the answer.
            // For this example, create a VoiceCommandContentTile with an 
            // icon and a string.
            var destinationsContentTiles = new List<VoiceCommandContentTile>();
            
            var destinationTile = new VoiceCommandContentTile();
            destinationTile.ContentTileType = 
                VoiceCommandContentTileType.TitleWith68x68IconAndText;
            // The user taps on the visual content to launch the app. 
            // Pass in a launch argument to enable the app to deep link to a 
            // page relevant to the item displayed on the content tile.
            destinationTile.AppLaunchArgument = 
                string.Format("destination={0}", "Las Vegas");
            destinationTile.Title = "Las Vegas";
            destinationTile.TextLine1 = "August 3rd 2015";
            destinationsContentTiles.Add(destinationTile);
            
            // Create the VoiceCommandResponse from the userMessage and list    
            // of content tiles.
            var response = VoiceCommandResponse.CreateResponse(
                userMessage, destinationsContentTiles);
            
            // Cortana displays a "Go to app_name" link that the user 
            // taps to launch the app. 
            // Pass in a launch to enable the app to deep link to a page 
            // relevant to the voice command.
            response.AppLaunchArgument = string.Format(
                "destination={0}", "Las Vegas");
            
            // Ask Cortana to display the user message and content tile and 
            // also speak the user message.
            await voiceServiceConnection.ReportSuccessAsync(response);
        }
        
        private async void LaunchAppInForeground() {
            var userMessage = new VoiceCommandUserMessage();
            userMessage.SpokenMessage = "Launching Adventure Works";
            
            var response = VoiceCommandResponse.CreateResponse(userMessage);
            
            // When launching the app in the foreground, pass an app 
            // specific launch parameter to indicate what page to show.
            response.AppLaunchArgument = "showAllTrips=true";
            
            await voiceServiceConnection.RequestAppLaunchAsync(response);
        }
    }
    ```  

Once activated, the app service has 0.5 second to call [**ReportSuccessAsync**](/uwp/api/Windows.ApplicationModel.VoiceCommands.VoiceCommandServiceConnection). **Cortana** shows and says a feedback string.  

>[!NOTE]
> You are able to declare a **Feedback** string in the VCD file. The string does not affect the UI text displayed on the Cortana canvas, it only affects the text spoken by **Cortana**.  

If the app takes longer than 0.5 second to make the call, **Cortana** inserts a hand-off screen, as shown here. **Cortana** displays the hand-off screen until the application calls **ReportSuccessAsync**, or for up to 5 seconds. If the app service does not call **ReportSuccessAsync**, or any of the [`VoiceCommandServiceConnection`](/uwp/api/Windows.ApplicationModel.VoiceCommands.VoiceCommandServiceConnection) methods that provide **Cortana** with information, the user receives an error message and the app service is canceled.  

:::image type="content" source="images/cortana/cortana-backgroundapp-progress-result.png" alt-text="Screenshot of Cortana and a basic query with progress and result screens using the AdventureWorks app in the background":::

## Related articles

- [Cortana interactions in Windows apps](cortana-interactions.md)
- [Activate a foreground app with voice commands through Cortana](cortana-launch-a-foreground-app-with-voice-commands.md)
- [VCD elements and attributes v1.2](/uwp/schemas/voicecommands/voice-command-elements-and-attributes-1-2)
- [Cortana design guidelines](cortana-design-guidelines.md)
- [Cortana voice command sample](https://go.microsoft.com/fwlink/p/?LinkID=619899)
