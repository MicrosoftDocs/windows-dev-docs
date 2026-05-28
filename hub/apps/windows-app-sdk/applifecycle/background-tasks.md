---
title: Using background tasks in Windows apps
description: Get an overview of using background tasks and learn how to create a new background task in an app with the BackgroundTaskBuilder in Windows App SDK.
ms.date: 07/14/2025
ms.topic: concept-article
keywords: windows 11, winui, background task, app lifecycle, windows app sdk
ms.localizationpriority: medium
# customer intent: As a Windows developer, I want to learn about using background tasks in Windows apps.
---

# Using background tasks in Windows apps

> [!IMPORTANT]
> Background tasks using the Windows App SDK `BackgroundTaskBuilder` require your app to be **packaged with MSIX**. For WPF or Windows Forms apps deployed without MSIX packaging, use [Task Scheduler](/windows/win32/taskschd/task-scheduler-start-page) or [.NET Worker Services](/dotnet/core/extensions/workers) instead.

This article provides an overview of using background tasks and describes how to create a new background task in a WinUI 3 or other MSIX-packaged app (including WPF and Windows Forms). For information about migrating your UWP apps with background tasks to WinUI, see the Windows App SDK [Background task migration strategy](../migrate-to-windows-app-sdk/guides/background-task-migration-strategy.md).

Background tasks are app components that run in the background without a user interface. They can perform actions such as downloading files, syncing data, sending notifications, or updating tiles. They can be triggered by various events, such as time, system changes, user actions, or push notifications. These tasks can get executed when corresponding trigger occurs even when the app is not in running state.

The implementation of background tasks is different for UWP and WinUI apps. For information about migrating your UWP apps with background tasks to WinUI, see the Windows App SDK [Background task migration strategy](../migrate-to-windows-app-sdk/guides/background-task-migration-strategy.md).

[Task Scheduler](/windows/win32/api/_taskschd/) helps desktop apps achieve the same functionality that is provided by [BackgroundTaskBuilder](/uwp/api/windows.applicationmodel.background.backgroundtaskbuilder) in UWP apps. More details on implementations using **TaskScheduler** are available [here](/windows/win32/api/taskschd/).

## Register a background task

Use the [BackgroundTaskBuilder](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.background.backgroundtaskbuilder) class included with the Windows App SDK to register a background task that uses full trust COM component.

The following example shows how to register a background task using C++. In the Windows App SDK github sample, you can see this registration code in [MainWindow.Xaml.cpp](https://github.com/microsoft/WindowsAppSDK-Samples/blob/main/Samples/BackgroundTask/InProc%20BackgroundTask/cpp-winui/BackgroundTaskBuilder/MainWindow.xaml.cpp#L82)

```cpp
auto access = co_await BackgroundExecutionManager::RequestAccessAsync();

// Unregister all existing background task registrations
auto allRegistrations = BackgroundTaskRegistration::AllTasks();
for (const auto& taskPair : allRegistrations)
{
    IBackgroundTaskRegistration task = taskPair.Value();
    task.Unregister(true);
}

//Using the Windows App SDK API for BackgroundTaskBuilder
winrt::Microsoft::Windows::ApplicationModel::Background::BackgroundTaskBuilder builder;
builder.Name(L"TimeZoneChangeTask");
SystemTrigger trigger = SystemTrigger(SystemTriggerType::TimeZoneChange, false);
auto backgroundTrigger = trigger.as<IBackgroundTrigger>();
builder.SetTrigger(backgroundTrigger);
builder.AddCondition(SystemCondition(SystemConditionType::InternetAvailable));
builder.SetTaskEntryPointClsid(__uuidof(winrt::BackgroundTaskInProcCPP::BackgroundTask));

try
{
    builder.Register();
}
catch (...)
{
    // Indicate an error was encountered.
}
```

The following example shows how to register a background task using C#. In the Windows App SDK github sample, you can see this registration code in [MainWindow.xaml.cs](https://github.com/microsoft/WindowsAppSDK-Samples/blob/main/Samples/BackgroundTask/InProc%20BackgroundTask/cs-winui/BackgroundTaskBuilder/MainWindow.xaml.cs#L79).

```csharp
await BackgroundExecutionManager.RequestAccessAsync();

// Unregister all existing background task registrations
var allRegistrations = BackgroundTaskRegistration.AllTasks;
foreach (var taskPair in allRegistrations)
{
    IBackgroundTaskRegistration task = taskPair.Value;
    task.Unregister(true);
}

//Using the Windows App SDK API for BackgroundTaskBuilder
var builder = new Microsoft.Windows.ApplicationModel.Background.BackgroundTaskBuilder();
builder.Name = "TimeZoneChangeTask";
var trigger = new SystemTrigger(SystemTriggerType.TimeZoneChange, false);
var backgroundTrigger = trigger as IBackgroundTrigger;
builder.SetTrigger(backgroundTrigger);
builder.AddCondition(new SystemCondition(SystemConditionType.InternetAvailable));
builder.SetTaskEntryPointClsid(typeof(BackgroundTask).GUID);
builder.Register();
```

Note that the call to [SetEntryPointClsid](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.background.backgroundtaskbuilder) method takes as an argument the GUID for an app-defined class that implements [IBackgroundTask](/uwp/api/windows.applicationmodel.background.ibackgroundtask). This interfaced is discussed in the section [Implement IBackgroundTask](#implement-ibackgroundtask) later in this article.

### Best practices for background task registration

Use the following best practices when registering background tasks.

* Call [BackgroundExecutionManager.RequestAccessAsync](/uwp/api/windows.applicationmodel.background.backgroundexecutionmanager.requestaccessasync) before registering background tasks.

* Don't register a background task multiple times. Either verify that a background task isn't already registered before registering or, as in the Windows App SDK sample, unregister all background tasks and then reregister the tasks. Use the [BackgroundTaskRegistration](/uwp/api/windows.applicationmodel.background.backgroundtaskregistration) class to query for existing background tasks.

* Use the [BackgroundTaskBuilder.Name](/windows/windows-app-sdk/api/winrt/microsoft.windows.applicationmodel.background.backgroundtaskbuilder.name) property to specify a meaningful name for the background task to simplify debugging and maintenance.



## Implement IBackgroundTask

**IBackgroundTask** is an interface that exposes one method, [Run](/uwp/api/windows.applicationmodel.background.ibackgroundtask.run), which is executed when the background task is invoked. Apps that use background tasks must include a class that implements **IBackgroundTask**.

The following example shows how to implement **IBackgroundTask** using C++. In the Windows App SDK github sample, you can see this registration code in [BackgroundTask.cpp](https://github.com/microsoft/WindowsAppSDK-Samples/blob/main/Samples/BackgroundTask/InProc%20BackgroundTask/cpp-winui/BackgroundTaskBuilder/BackgroundTask.cpp#L19).

```cpp
 void BackgroundTask::Run(_In_ IBackgroundTaskInstance taskInstance)
{
    // Get deferral to indicate not to kill the background task process as soon as the Run method returns
    m_deferral = taskInstance.GetDeferral();
    m_progress = 0;
    taskInstance.Canceled({ this, &BackgroundTask::OnCanceled });

    // Calling a method on the Window to inform that the background task is executed
    winrt::Microsoft::UI::Xaml::Window window = winrt::BackgroundTaskBuilder::implementation::App::Window();
    m_mainWindow = window.as<winrt::BackgroundTaskBuilder::IMainWindow>();

    Windows::Foundation::TimeSpan period{ std::chrono::seconds{2} };
    m_periodicTimer = Windows::System::Threading::ThreadPoolTimer::CreatePeriodicTimer([this, lifetime = get_strong()](Windows::System::Threading::ThreadPoolTimer timer)
        {
            if (!m_cancelRequested && m_progress < 100)
            {
                m_progress += 10;
            }
            else
            {
                m_periodicTimer.Cancel();

                // Indicate that the background task has completed.
                m_deferral.Complete();
                if (m_cancelRequested) m_progress = -1;
            }
            m_mainWindow.BackgroundTaskExecuted(m_progress);
        }, period);
}

void BackgroundTask::OnCanceled(_In_ IBackgroundTaskInstance /* taskInstance */, _In_ BackgroundTaskCancellationReason /* cancelReason */)
{
    m_cancelRequested = true;
}
```

The following example shows how to implement **IBackgroundTask** using C#. In the Windows App SDK github sample, you can see this implementation code in [BackgroundTask.cs](https://github.com/microsoft/WindowsAppSDK-Samples/blob/main/Samples/BackgroundTask/InProc%20BackgroundTask/cs-winui/BackgroundTaskBuilder/BackgroundTask.cs#L21).

```csharp
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.None)]
[Guid("00001111-aaaa-2222-bbbb-3333cccc4444")]
[ComSourceInterfaces(typeof(IBackgroundTask))]
public class BackgroundTask : IBackgroundTask
{
    /// <summary>
    /// This method is the main entry point for the background task. The system will believe this background task
    /// is complete when this method returns.
    /// </summary>
    [MTAThread]
    public void Run(IBackgroundTaskInstance taskInstance)
    {
        // Get deferral to indicate not to kill the background task process as soon as the Run method returns
        _deferral = taskInstance.GetDeferral();
        // Wire the cancellation handler.
        taskInstance.Canceled += this.OnCanceled;

        // Set the progress to indicate this task has started
        taskInstance.Progress = 0;

        _periodicTimer = ThreadPoolTimer.CreatePeriodicTimer(new TimerElapsedHandler(PeriodicTimerCallback), TimeSpan.FromSeconds(1));
    }

    // Simulate the background task activity.
    private void PeriodicTimerCallback(ThreadPoolTimer timer)
    {
        if ((_cancelRequested == false) && (_progress < 100))
        {
            _progress += 10;
        }
        else
        {
            if (_cancelRequested) _progress = -1;
            if (_periodicTimer != null) _periodicTimer.Cancel();

            // Indicate that the background task has completed.
            if (_deferral != null) _deferral.Complete();
        }

        BackgroundTaskBuilder.MainWindow.taskStatus(_progress);
    }

    /// <summary>
    /// This method is signaled when the system requests the background task be canceled. This method will signal
    /// to the Run method to clean up and return.
    /// </summary>
    [MTAThread]
    public void OnCanceled(IBackgroundTaskInstance taskInstance, BackgroundTaskCancellationReason cancellationReason)
    {
        // Handle cancellation operations and flag the task to end
        _cancelRequested = true;
    }
```

### Best practices for implementing IBackgroundTask

Use the following best practices when implementing **IBackgroundTask**.

* If the background task will perform asynchronous operations, get a **deferral** object by calling [GetDeferral](/uwp/api/windows.applicationmodel.background.ibackgroundtaskinstance.getdeferral) on the **ITaskInstance** object passed into **Run**. This prevents the background task host, `backgroundtaskhost.exe`, from terminating prematurely before the operations are complete. Release the deferral once all of the asynchronous tasks have completed.
* Keep tasks as lightweight as possible. Long-running tasks may be terminated by the system and are not recommended.
* Use logging to capture execution details for troubleshooting.
* For more best practices for implementing background tasks, see [Guidelines for background tasks](/windows/uwp/launch-resume/guidelines-for-background-tasks).
 

## Declare the background task app extension in the App Manifest

You must declare an app extension in your app's `Package.appxmanifest` file in order to register your background task with the system when your app is installed and to provide information that the system needs in order to launch your background task.

You must add an extension with the category **windows.backgroundTasks** to your app manifest in order for the background task to be registered successfully when the app is installed. C# apps must specify the **EntryPoint** attribute value "Microsoft.Windows.ApplicationModel.Background.UniversalBGTask.Task". For C++ apps, this gets added automatically by setting `WindowsAppSDKBackgroundTask` to **true** in the project file.

You must also declare a [com:Extension](/uwp/schemas/appxpackage/uapmanifestschema/element-com-extension) with the category value of "windows.comServer". You must specify the **LaunchAndActivationPermission** attribute in the [com:ExeServer](/uwp/schemas/appxpackage/uapmanifestschema/element-com-exeserver) element to explicitly grant the `backgroundtaskhost.exe` process permission to invoke the COM class.  For information about the format of this string, see [Security Descriptor String Format](/windows/win32/secauthz/security-descriptor-string-format).

Make sure that the class ID you specify in the [com:Class](/uwp/schemas/appxpackage/uapmanifestschema/element-com-exeserver-class) element matches the class ID for your implementation of **IBackgroundTask**.

The following example shows the syntax of the application extension declarations in the app manifest file to enable the system to discover and launch a background task. To see the full app manifest file for the background task on github, see [Package.appxmanifest](https://github.com/microsoft/WindowsAppSDK-Samples/blob/8393a02f0308696246607e157326e87fb04af4d8/Samples/BackgroundTask/InProc%20BackgroundTask/cpp-winui/BackgroundTaskBuilder/Package.appxmanifest#L54).

```xml
<Extensions>
    <Extension Category="windows.backgroundTasks" EntryPoint="Microsoft.Windows.ApplicationModel.Background.UniversalBGTask.Task">
        <BackgroundTasks>
            <Task Type="general"/>
        </BackgroundTasks>
    </Extension>
    <com:Extension Category="windows.comServer">
        <com:ComServer>
            <com:ExeServer Executable="BackgroundTaskBuilder.exe" DisplayName="BackgroundTask"
                                LaunchAndActivationPermission="O:PSG:BUD:(A;;11;;;IU)(A;;11;;;S-1-15-2-1)S:(ML;;NX;;;LW)">
                <com:Class Id="00001111-aaaa-2222-bbbb-3333cccc4444" DisplayName="BackgroundTask" />
            </com:ExeServer>
        </com:ComServer>
    </com:Extension>
</Extensions>
```

## Register the COM server for the background task

Registering a COM server ensures that the system knows how to instantiate your background task class when [CoCreateInstance](/windows/win32/api/combaseapi/nf-combaseapi-cocreateinstance) is called by `backgroundtaskhost.exe`. You must register the COM class factory for your background task by calling [CoRegisterClassObject](/windows/win32/api/combaseapi/nf-combaseapi-coregisterclassobject) or COM activation will fail. 


### COM server registration in C++

The following example shows a C++ helper function, **RegisterBackgroundTaskFactory** that registers the class factory for the class that implements **IBackgroundTask**. In this example this class is called **BackgroundTask**. In the destructor for the helper class, [CoRevokeClassObject](/windows/win32/api/combaseapi/nf-combaseapi-corevokeclassobject) is called to revoke the class factory registration.

You can see this helper class in the sample repo in the file [RegisterForCOM.cpp](https://github.com/microsoft/WindowsAppSDK-Samples/blob/main/Samples/BackgroundTask/InProc%20BackgroundTask/cpp-winui/BackgroundTaskBuilder/RegisterForCOM.cpp).

```cpp
hresult RegisterForCom::RegisterBackgroundTaskFactory()
{
    hresult hr;
    try
    {
        com_ptr<IClassFactory> taskFactory = make<BackgroundTaskFactory>();

        check_hresult(CoRegisterClassObject(__uuidof(BackgroundTask),
            taskFactory.detach(),
            CLSCTX_LOCAL_SERVER,
            REGCLS_MULTIPLEUSE,
            &ComRegistrationToken));

        OutputDebugString(L"COM Registration done");
        hr = S_OK;
    }
    CATCH_RETURN();
}

 RegisterForCom::~RegisterForCom()
{
    if (ComRegistrationToken != 0)
    {
        CoRevokeClassObject(ComRegistrationToken);
    }
}
```

For in-proc server registration in C++, call the helper class to register the class factory from within the [Application.OnLaunched](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.application.onlaunched) method. See the call to the helper method in the sample repo in [App.xaml.cpp](https://github.com/microsoft/WindowsAppSDK-Samples/blob/main/Samples/BackgroundTask/InProc%20BackgroundTask/cpp-winui/BackgroundTaskBuilder/App.xaml.cpp).

```cpp
void App::OnLaunched([[maybe_unused]] LaunchActivatedEventArgs const& e)
{
    window = make<MainWindow>();
    window.Activate();
    // Start COM server for the COM calls to complete
    comRegister.RegisterBackgroundTaskFactory();
}
```

For out-of-proc background tasks, COM server registration must be done during the startup process. You can see the call to the helper class in [App.xaml.cpp](https://github.com/microsoft/WindowsAppSDK-Samples/blob/main/Samples/BackgroundTask/OutOfProc%20BackgroundTask/cpp-winui/BackgroundTaskBuilder/App.xaml.cpp).

```cpp
int WINAPI wWinMain(_In_ HINSTANCE, _In_opt_ HINSTANCE, _In_ LPWSTR lpCmdLine, _In_ int)
{
    if (std::wcsncmp(lpCmdLine, RegisterForCom::RegisterForComToken, sizeof(RegisterForCom::RegisterForComToken)) == 0)
    {
        winrt::init_apartment(winrt::apartment_type::multi_threaded);
        RegisterForCom comRegister;
        // Start COM server and wait for the COM calls to complete
        comRegister.RegisterAndWait(__uuidof(BackgroundTask));
        OutputDebugString(L"COM Server Shutting Down");
    }
    else
    {
        // put your fancy code somewhere here
        ::winrt::Microsoft::UI::Xaml::Application::Start(
            [](auto&&)
            {
                ::winrt::make<::winrt::BackgroundTaskBuilder::implementation::App>();
            });
    }

    return 0;
}

```

### COM server registration in C#

The following example shows a C# helper function, **CreateInstance** that registers the class factory for the class that implements **IBackgroundTask**. In this example this class is called **BackgroundTask**. The helper class uses the [LibraryImportAttribute](/dotnet/api/system.runtime.interopservices.libraryimportattribute) to access the native COM registration methods from C#. For more information, see [Source generation for platform invokes](/dotnet/standard/native-interop/pinvoke-source-generation). You can see the implementation of the helper class in the sample repo in [ComServer.cs](https://github.com/microsoft/WindowsAppSDK-Samples/blob/main/Samples/BackgroundTask/InProc%20BackgroundTask/cs-winui/BackgroundTaskBuilder/ComServer.cs).

```cs
static partial class ComServer
{
    [LibraryImport("ole32.dll")]
    public static partial int CoRegisterClassObject(
        ref Guid classId,
        [MarshalAs(UnmanagedType.Interface)] IClassFactory objectAsUnknown,
        uint executionContext,
        uint flags,
        out uint registrationToken);

    [LibraryImport("ole32.dll")]
    public static partial int CoRevokeClassObject(uint registrationToken);

    public const uint CLSCTX_LOCAL_SERVER = 4;
    public const uint REGCLS_MULTIPLEUSE = 1;

    public const uint S_OK = 0x00000000;
    public const uint CLASS_E_NOAGGREGATION = 0x80040110;
    public const uint E_NOINTERFACE = 0x80004002;

    public const string IID_IUnknown = "00000000-0000-0000-C000-000000000046";
    public const string IID_IClassFactory = "00000001-0000-0000-C000-000000000046";

    [GeneratedComInterface]
    [Guid(IID_IClassFactory)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public partial interface IClassFactory
    {
        [PreserveSig]
        uint CreateInstance(IntPtr objectAsUnknown, in Guid interfaceId, out IntPtr objectPointer);

        [PreserveSig]
        uint LockServer([MarshalAs(UnmanagedType.Bool)] bool Lock);
    }

    [GeneratedComClass]
    internal sealed partial class BackgroundTaskFactory : IClassFactory
    {
        public uint CreateInstance(IntPtr objectAsUnknown, in Guid interfaceId, out IntPtr objectPointer)
        {
            if (objectAsUnknown != IntPtr.Zero)
            {
                objectPointer = IntPtr.Zero;
                return CLASS_E_NOAGGREGATION;
            }

            if ((interfaceId != typeof(BackgroundTask).GUID) && (interfaceId != new Guid(IID_IUnknown)))
            {
                objectPointer = IntPtr.Zero;
                return E_NOINTERFACE;
            }

            objectPointer = MarshalInterface<IBackgroundTask>.FromManaged(new BackgroundTask());
            return S_OK;
        }

        public uint LockServer(bool lockServer) => S_OK;
    }
}
```

For in-proc background tasks in C#, the COM registration is performed during app startup, in the constructor for the [Application](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.application) object. You can see the call to the helper method in the sample repo in [App.xaml.cs](https://github.com/microsoft/WindowsAppSDK-Samples/blob/main/Samples/BackgroundTask/InProc%20BackgroundTask/cs-winui/BackgroundTaskBuilder/App.xaml.cs).

```csharp
public App()
{
    this.InitializeComponent();
    Guid taskGuid = typeof(BackgroundTask).GUID;
    ComServer.CoRegisterClassObject(ref taskGuid,
                                    new ComServer.BackgroundTaskFactory(),
                                    ComServer.CLSCTX_LOCAL_SERVER,
                                    ComServer.REGCLS_MULTIPLEUSE,
                                    out _RegistrationToken);
}

~App()
{
    ComServer.CoRevokeClassObject(_RegistrationToken);
}
```

For out-of-proc tasks in C#, you must perform COM registration in app startup. To do this, you must disable the default XAML-generated **Main** entry point by updating your app's project file.

In the default project template, the **Main** method entry point is autogenerated by the compiler. This example will disable the autogeneration of Main so that the necessary activation code can be run at startup.

1. In **Solution Explorer**, right-click the project icon and select Edit Project File.
1. In the **PropertyGroup** element, add the following child element to disable the auto-generated main function.

```xml
<DefineConstants>$(DefineConstants);DISABLE_XAML_GENERATED_MAIN</DefineConstants>
```

You can see the call to the helper class to register the COM class in the sample repo in [Program.cs](https://github.com/microsoft/WindowsAppSDK-Samples/blob/main/Samples/BackgroundTask/OutOfProc%20BackgroundTask/cs-winui/BackgroundTaskBuilder/Program.cs)

```csharp
public class Program
{
    static private uint _RegistrationToken;
    static private ManualResetEvent _exitEvent = new ManualResetEvent(false);

    static void Main(string[] args)
    {
        if (args.Contains("-RegisterForBGTaskServer"))
        {
            Guid taskGuid = typeof(BackgroundTask).GUID;
            ComServer.CoRegisterClassObject(ref taskGuid,
                                            new ComServer.BackgroundTaskFactory(),
                                            ComServer.CLSCTX_LOCAL_SERVER,
                                            ComServer.REGCLS_MULTIPLEUSE,
                                            out _RegistrationToken);

            // Wait for the exit event to be signaled before exiting the program
            _exitEvent.WaitOne();
        }
        else
        {
            App.Start(p => new App());
        }
    }

    public static void SignalExit()
    {
        _exitEvent.Set();
    }
}
```

## Related content

- [Guidelines for background tasks in UWP apps](/windows/uwp/launch-resume/guidelines-for-background-tasks)
- [Background task migration strategy](../migrate-to-windows-app-sdk/guides/background-task-migration-strategy.md)
