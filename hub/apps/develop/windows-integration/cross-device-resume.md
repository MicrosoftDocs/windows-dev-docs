---
title: Use the Continuity SDK to implement Cross Device Resume (XDR) for Android and Windows Applications
description: Guidelines for first & third party developers to integrate with Windows XDR experiences using the Continuity SDK.
ms.date: 08/12/2025
ms.topic: how-to
# customer intent: As a Windows developer, I want to learn how to integrate my app with Windows XDR experiences so that I can provide a seamless experience for my users.
---

# Use the Continuity SDK to implement Cross Device Resume (XDR) for Android and Windows Applications

This article provides comprehensive guidelines for first-party and third-party developers on how to integrate features using the Continuity SDK in your applications. The Continuity SDK enables seamless cross-device experiences, allowing users to resume activities across different platforms, including Android and Windows.

By following this guidance, you can create a smooth and integrated user experience across multiple devices by leveraging the XDR using Continuity SDK.

> [!IMPORTANT]
> **Onboarding to Resume in Windows**
>
> Resume is a Limited Access Feature (LAF). To gain access to this API, you'll need to get approval from Microsoft to interoperate with the "Link to Windows" package on Android mobile devices. 
>
> To request access, email [wincrossdeviceapi@microsoft.com](mailto:wincrossdeviceapi@microsoft.com) with the information listed below:
>
> - A description of your user experience 
> - A screenshot of your application where a user natively accesses web or documents 
> - The **PackageId** of your application 
> - The Google Play store URL for your application 
>
> If the request is approved, you will receive instructions on how to unlock the feature. Approvals will be based on your communication, provided that your scenario meets the outlined [Scenario Requirements](/windows/cross-device/phonelink/#scenario-requirements).

## Prerequisites

For Android applications, ensure the following requirements are met before integrating the Continuity SDK:

- Minimum SDK Version: 24
- Kotlin Version: 1.9.x
- Link to Windows (LTW): 1.241101.XX

For Windows applications, ensure the following requirements are met:

- Minimum Windows Version: Windows 11
- Development Environment: Visual Studio 2019 or later

> [!NOTE]
> iOS applications are not supported for integration with the Continuity SDK at this time. 

## Configure your development environment

The following sections provide step-by-step instructions for setting up the development environment for both Android and Windows applications.

### Android setup

To set up the development environment for Android, follow these steps: 

1. To set up the bundle, download and use the .aar file via libraries provided in the following releases: [Windows Cross-Device SDK releases](https://github.com/microsoft/Windows-Cross-Device/releases).

1. Add the meta tags in the AndroidManifest.xml file of your Android application. The following snippet demonstrates how to add the required meta tags: 

   ```xml
   <meta-data 
       android:name="com.microsoft.crossdevice.resumeActivityProvider" 
       android:value="true" /> 
     
   <meta-data 
       android:name="com.microsoft.crossdevice.trigger.PartnerApp" 
       android:value="4" /> 
   ```

## API integration steps

After the manifest declarations, app developers can easily send their app context by following a simple code example.

The App must:

1. Initialize/DeInitialize the Continuity SDK:
   1. The app should determine the appropriate time to call the Initialize and DeInitialize functions.
   1. After calling the Initialize function, a callback that implements IAppContextEventHandler should be triggered.
1. Send/Delete **AppContext**:
   1. After initializing the SDK, if **onContextRequestReceived** is called, it indicates the connection is established. The app can then send (including create and update) **AppContext** to LTW or delete **AppContext** from LTW.
   1. If there is no connection between the phone and PC and the app sends **AppContext** to LTW, the app will receive **onContextResponseError** with the message “PC is not connected.”
   1. When the connection is re-established, **onContextRequestReceived** is called again. The app can then send the current AppContext to LTW. 
   1. After **onSyncServiceDisconnected** or deinitializing the SDK, the app should not send an **AppContext**.

Below is a code example. For all the required and optional fields in **AppContext**, please refer to the [AppContext description](#appcontext). 

The following Android code snippet demonstrates how to make API requests using the Continuity SDK:

```kotlin MainActivity.kt
import android.os.Bundle 
import android.util.Log 
import android.widget.Button 
import android.widget.TextView 
import android.widget.Toast 
import androidx.activity.enableEdgeToEdge 
import androidx.appcompat.app.AppCompatActivity 
import androidx.core.view.ViewCompat 
import androidx.core.view.WindowInsetsCompat 
import androidx.lifecycle.LiveData 
import androidx.lifecycle.MutableLiveData 
import androidx.lifecycle.Observer 
import com.microsoft.crossdevicesdk.continuity.AppContext 
import com.microsoft.crossdevicesdk.continuity.AppContextManager 
import com.microsoft.crossdevicesdk.continuity.ContextRequestInfo 
import com.microsoft.crossdevicesdk.continuity.IAppContextEventHandler 
import com.microsoft.crossdevicesdk.continuity.IAppContextResponse 
import com.microsoft.crossdevicesdk.continuity.LogUtils 
import com.microsoft.crossdevicesdk.continuity.ProtocolConstants 
import java.util.UUID 

  

class MainActivity : AppCompatActivity() { 

    //Make buttons member variables --- 
    private lateinit var buttonSend: Button 
    private lateinit var buttonDelete: Button 
    private lateinit var buttonUpdate: Button 

    private val appContextResponse = object : IAppContextResponse { 
        override fun onContextResponseSuccess(response: AppContext) { 
            Log.d("MainActivity", "onContextResponseSuccess") 
            runOnUiThread { 
                Toast.makeText( 
                    this@MainActivity, 
                    "Context response success: ${response.contextId}", 
                    Toast.LENGTH_SHORT 
                ).show() 
            } 
        } 

        override fun onContextResponseError(response: AppContext, throwable: Throwable) { 
            Log.d("MainActivity", "onContextResponseError: ${throwable.message}") 
            runOnUiThread { 
                Toast.makeText( 
                    this@MainActivity, 
                    "Context response error: ${throwable.message}", 
                    Toast.LENGTH_SHORT 
                ).show() 

                // Check if the error message contains the specific string 
                if (throwable.message?.contains("PC is not connected") == true) { 
                    //App should stop sending intent once this callback is received 
                }
            } 
        } 
    } 

    private lateinit var appContextEventHandler: IAppContextEventHandler 

    private val _currentAppContext = MutableLiveData<AppContext?>() 

    private val currentAppContext: LiveData<AppContext?> get() = _currentAppContext 

    override fun onCreate(savedInstanceState: Bundle?) { 
        super.onCreate(savedInstanceState) 
        enableEdgeToEdge() 
        setContentView(R.layout.activity_main) 
        ViewCompat.setOnApplyWindowInsetsListener(findViewById(R.id.main)) { v, insets -> 
            val systemBars = insets.getInsets(WindowInsetsCompat.Type.systemBars()) 
            v.setPadding(systemBars.left, systemBars.top, systemBars.right, systemBars.bottom) 
            insets 
        } 

        LogUtils.setDebugMode(true) 
        var ready = false 
        buttonSend = findViewById(R.id.buttonSend) 
        buttonDelete = findViewById(R.id.buttonDelete) 
        buttonUpdate = findViewById(R.id.buttonUpdate) 
        setButtonDisabled(buttonSend) 
        setButtonDisabled(buttonDelete) 
        setButtonDisabled(buttonUpdate) 

        buttonSend.setOnClickListener { 
            if (ready) { 
                sendResumeActivity() 
            } 
        } 

        buttonDelete.setOnClickListener { 
            if (ready) { 
                deleteResumeActivity() 
            } 
        } 

        buttonUpdate.setOnClickListener { 
            if (ready) { 
                updateResumeActivity() 
            }
        } 

        appContextEventHandler = object : IAppContextEventHandler { 

            override fun onContextRequestReceived(contextRequestInfo: ContextRequestInfo) { 
                LogUtils.d("MainActivity", "onContextRequestReceived") 
                ready = true 
                setButtonEnabled(buttonSend) 
                setButtonEnabled(buttonDelete) 
                setButtonEnabled(buttonUpdate) 

            } 

  

            override fun onInvalidContextRequestReceived(throwable: Throwable) { 
                Log.d("MainActivity", "onInvalidContextRequestReceived") 

            } 

  

            override fun onSyncServiceDisconnected() { 
                Log.d("MainActivity", "onSyncServiceDisconnected") 
                ready = false 
                setButtonDisabled(buttonSend) 
                setButtonDisabled(buttonDelete) 
            } 
        } 

        // Initialize the AppContextManager 
        AppContextManager.initialize(this.applicationContext, appContextEventHandler) 

        // Update currentAppContext text view. 
        val textView = findViewById<TextView>(R.id.appContext) 

        currentAppContext.observe(this, Observer { appContext -> 
            appContext?.let { 
                textView.text = 
                    "Current app context: ${it.contextId}\n App ID: ${it.appId}\n Created: ${it.createTime}\n Updated: ${it.lastUpdatedTime}\n Type: ${it.type}" 
                Log.d("MainActivity", "Current app context: ${it.contextId}") 
            } ?: run { 
                textView.text = "No current app context available" 
                Log.d("MainActivity", "No current app context available") 
            } 
        }) 
    } 


    // Send resume activity to LTW 
    private fun sendResumeActivity() { 
        val appContext = AppContext().apply { 
            this.contextId = generateContextId() 
            this.appId = applicationContext.packageName 
            this.createTime = System.currentTimeMillis() 
            this.lastUpdatedTime = System.currentTimeMillis() 
            this.type = ProtocolConstants.TYPE_RESUME_ACTIVITY 
        } 

        _currentAppContext.value = appContext 
        AppContextManager.sendAppContext(this.applicationContext, appContext, appContextResponse) 
    } 

    // Delete resume activity from LTW 
    private fun deleteResumeActivity() { 
        currentAppContext.value?.let { 
            AppContextManager.deleteAppContext( 
                this.applicationContext, 
                it.contextId, 
                appContextResponse 
            ) 
            _currentAppContext.value = null 
        } ?: run { 
            Toast.makeText(this, "No resume activity to delete", Toast.LENGTH_SHORT).show() 
            Log.d("MainActivity", "No resume activity to delete") 
        }
    } 

    private fun updateResumeActivity() { 
        currentAppContext.value?.let { 
            it.lastUpdatedTime = System.currentTimeMillis() 
            AppContextManager.sendAppContext(this.applicationContext, it, appContextResponse) 
            _currentAppContext.postValue(it) 
        } ?: run { 
            Toast.makeText(this, "No resume activity to update", Toast.LENGTH_SHORT).show() 
            Log.d("MainActivity", "No resume activity to update") 
        } 
    } 

    private fun setButtonDisabled(button: Button) { 
        button.isEnabled = false 
        button.alpha = 0.5f 
    } 

    private fun setButtonEnabled(button: Button) { 
        button.isEnabled = true 
        button.alpha = 1.0f 
    } 

    override fun onDestroy() { 
        super.onDestroy() 
        // Deinitialize the AppContextManager 
        AppContextManager.deInitialize(this.applicationContext) 
    } 

    override fun onStart() { 
        super.onStart() 
        // AppContextManager.initialize(this.applicationContext, appContextEventHandler) 
    } 


    override fun onStop() { 
        super.onStop() 
        // AppContextManager.deInitialize(this.applicationContext) 
    } 

    private fun generateContextId(): String { 
        return "${packageName}.${UUID.randomUUID()}" 
    } 

} 
```

## Integration validation steps

To validate the integration of the Continuity SDK in your application, follow these steps:

### Preparation 

The following steps are required to prepare for the integration validation:

1. Ensure private LTW is installed. 
1. Connect LTW to your PC:

   Refer to [How to manage your mobile device on your PC](https://support.microsoft.com/topic/phone-link-requirements-and-setup-cd2a1ee7-75a7-66a6-9d4e-bf22e735f9e3#bkmk_cdeh_learn_more) for instructions. 

   > [!NOTE]
   > If after scanning the QR code you aren't redirected to LTW, please open LTW first and scan the QR code within the app.

1. Verify that the partner app has integrated the Continuity SDK. 

### Validation 

Next, follow these steps to validate the integration:

1. Launch the app and initialize the SDK. Confirm that **onContextRequestReceived** is called. 
1. After **onContextRequestReceived** has been called, the app can send the **AppContext** to LTW. If **onContextResponseSuccess** is called after sending **AppContext**, the SDK integration is successful.
1. If the app sends **AppContext** while the PC is locked or disconnected, verify that **onContextResponseError** is called with “PC is not connected.”
1. When the connection is restored, ensure **onContextRequestReceived** is called again and app can then send the current AppContext to LTW.

The screenshot below shows the log entry when the PC is disconnected with the error message "PC is not connected" and the log entry after reconnection when **onContextRequestReceived** is called again.

:::image type="content" source="images/xdr-not-connected-logs.png" alt-text="A screenshot of Windows log entries showing the PC is not connected error message and the subsequent onContextRequestReceived log entry after reconnection.":::

## AppContext 

XDR defines **AppContext** as metadata through which XDR can understand which app to resume, along with the context with which the application must be resumed. Apps can use activities to enable users to get back to what they were doing in their app, across multiple devices. Activities created by any mobile app appear on users' Windows device(s) so long as those devices have been Cross Device Experience Host (CDEH) provisioned.   

Every application is different, and it's up to Windows to understand the target application for resume and up to specific applications on Windows to understand the context. XDR is proposing a generic schema which can cater to requirements for all first party as well as third party app resume scenarios.

### contextId 

- Required: Yes
- Description: This is a unique identifier used to distinguish one **AppContext** from another. It ensures that each **AppContext** is uniquely identifiable. 
- Usage: Make sure to generate a unique contextId for each **AppContext** to avoid conflicts. 

### type 

- Required: Yes 
- Description: This is a binary flag that indicates the type of **AppContext** being sent to Link to Windows (LTW). The value should be consistent with the requestedContextType. 
- Usage: Set this flag according to the type of context you are sending. For example, `ProtocolConstants.TYPE_RESUME_ACTIVITY`.

### createTime 

- Required: Yes 
- Description: This timestamp represents the creation time of the **AppContext**. 
- Usage: Record the exact time when the **AppContext** is created. 

### intentUri 

- Required: No, if **weblink** is provided 
- Description: This URI indicates which app can continue the **AppContext** handed over from the originating device. 
- Usage: Provide this if you want to specify a particular app to handle the context. 

### weblink 

- Required: No, if **intentUri** is provided
- Description: This URI is used to launch the web endpoint of the application if they choose not to use store apps. This parameter is used only when **intentUri** is not provided. If both are provided, **intentUri** will be used to resume the application on Windows. 
- Usage: Only to be use if application wants to resume on web endpoints and not the store applications. 

### appId 

- Required: Yes 
- Description: This is the package name of the application the context is for. 
- Usage: Set this to the package name of your application. 

### title 

- Required: Yes 
- Description: This is the title of the **AppContext**, such as a document name or web page title. 
- Usage: Provide a meaningful title that represents the **AppContext**. 

### preview 

- Required: No 
- Description: These are bytes of the preview image that can represent the **AppContext**. 
- Usage: Provide a preview image if available to give users a visual representation of the **AppContext**. 

### LifeTime 

- Required: No
- Description: This is the lifetime of the `AppContext` in milliseconds. It is only used for ongoing scenarios. If not set, the default value is 5 minutes. 
- Usage: Set this to define how long the `AppContext` should be valid. You can set a value up to a maximum of 5 minutes. Any greater value will automatically be shortened to 5 minutes.

## Intent URIs

URIs allow you to launch another app to perform a specific task, enabling helpful app-to-app scenarios. For more infomation about launching apps using URIs, see [Launch the default Windows app for a URI](/windows/apps/develop/launch/launch-default-app) and [Create Deep Links to App Content | Android Developers](https://developer.android.com/training/app-links/deep-linking).

## Handling API responses in Windows

This section describes how to handle the API responses in Windows applications. The Continuity SDK provides a way to handle the API responses for Win32, UWP, and Windows App SDK apps.

### Win32 app example

For Win32 apps to handle protocol URI launch, the following steps are required:

1. First, an entry needs to be made to the registry as follows: 

    ```
    [HKEY_CLASSES_ROOT\partnerapp] 
    @="URL:PartnerApp Protocol" 
    "URL Protocol"="" 
    
    [HKEY_CLASSES_ROOT\partnerapp\shell\open\command] 
    @="\"C:\\path\\to\\PartnerAppExecutable.exe\" \"%1\"" 
    ```

1. The launch must be handled in the main function of the Win32 app: 

    ```cpp
    #include <windows.h> 
    #include <shellapi.h> 
    #include <string> 
    #include <iostream> 
    
    int CALLBACK wWinMain(HINSTANCE, HINSTANCE, PWSTR lpCmdLine, int) 
    { 
        // Check if there's an argument passed via lpCmdLine 
        std::wstring cmdLine(lpCmdLine); 
        std::wstring arguments; 
    
        if (!cmdLine.empty()) 
        { 
            // Check if the command-line argument starts with "partnerapp://", indicating a URI launch 
            if (cmdLine.find(L"partnerapp://") == 0) 
            { 
                // This is a URI protocol launch 
                // Process the URI as needed 
                // Example: Extract action and parameters from the URI 
                arguments = cmdLine;  // or further parse as required 
            } 
            else 
            {
                // Launched by command line or activation APIs 
            } 
        } 
        else 
        { 
            // Handle cases where no arguments were passed 
        } 
    
        return 0; 
    } 
    ```

### UWP Apps 

For UWP apps, the protocol URI can be registered in the project's app manifest. The following steps demonstrate how to handle protocol activation in a UWP app.

1. First, the protocol URI is registered in the `Package.appxmanifest` file as follows:

    ```xml
    <Applications> 
            <Application Id= ... > 
                <Extensions> 
                    <uap:Extension Category="windows.protocol"> 
                      <uap:Protocol Name="alsdk"> 
                        <uap:Logo>images\icon.png</uap:Logo> 
                        <uap:DisplayName>SDK Sample URI Scheme</uap:DisplayName> 
                      </uap:Protocol> 
                    </uap:Extension> 
              </Extensions> 
              ... 
            </Application> 
       <Applications> 
    ```

1. Next, in the `App.xaml.cs` file, override the `OnActivated` method as follows: 

    ```csharp
    public partial class App 
    { 
       protected override void OnActivated(IActivatedEventArgs args) 
      { 
          if (args.Kind == ActivationKind.Protocol) 
          { 
             ProtocolActivatedEventArgs eventArgs = args as ProtocolActivatedEventArgs; 
             // TODO: Handle URI activation 
             // The received URI is eventArgs.Uri.AbsoluteUri 
          } 
       } 
    } 
    ```

For more information on handling URI launch in UWP apps, see step 3 in [Handle URI activation](/windows/apps/develop/launch/handle-uri-activation#step-3-handle-the-activated-event).

### WinUI 3 example

The following code snippet demonstrates how to handle protocol activation in a C++ WinUI app with Windows App SDK:

```cpp
void App::OnActivated(winrt::Windows::ApplicationModel::Activation::IActivatedEventArgs const& args) 
{ 
     if (args.Kind() == winrt::Windows::ApplicationModel::Activation::ActivationKind::Protocol) 
     { 
         auto protocolArgs = args.as<winrt::Windows::ApplicationModel::Activation::ProtocolActivatedEventArgs>(); 
         auto uri = protocolArgs.Uri(); 
         std::wstring uriString = uri.AbsoluteUri().c_str(); 
         //Process the URI as per argument scheme 
     } 
} 
```

## Weblink 

Using a weblink will launch the web endpoint of the application. App developers need to ensure that the weblink provided from their Android application is valid because XDR will use default browser of the system to redirect to the weblink provided. 

## Handling arguments obtained from Cross Device Resume 

It is the responsibility of each app to deserialize and decrypt the argument received and process the information accordingly to transfer the ongoing context from phone to PC. For example, if a call needs to be transferred, the app must be able to communicate that context from phone and the desktop app must understand that context appropriately and continue loading.

## Related content

- [Handle URI activation](/windows/apps/develop/launch/handle-uri-activation)
- [Integrate with Windows](index.md)
- [Cross Device People API](cross-device-people-api.md)
