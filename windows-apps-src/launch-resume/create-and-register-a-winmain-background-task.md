---
title: Create and register a winmain background task
description: Create a winmain background task that can run in your main process or out-of-process when your winmain app is not running.
ms.assetid: 8CBD4986-6E65-4374-BC7C-C38908E417E1
ms.date: 03/27/2020
ms.topic: article
keywords: windows 10, desktop bridge, sparse signed packaged, winmain, background task
ms.localizationpriority: medium
dev_langs:
- csharp
- cppwinrt
---

# Create and register a winmain background task

> [!TIP]
> The BackgroundTaskBuilder.SetTaskEntryPointClsid method is available starting in Windows 10, version 2004.

**Important APIs**

-   [**IBackgroundTask**](https://docs.microsoft.com/uwp/api/Windows.ApplicationModel.Background.IBackgroundTask)
-   [**BackgroundTaskBuilder**](https://docs.microsoft.com/uwp/api/Windows.ApplicationModel.Background.BackgroundTaskBuilder)

Create a background task class and register it to run in your full trust packaged winmain app in response to triggers. You can use background tasks to provide functionality when your app is suspended or not running. This topic demonstrates how to create and register a background task that can run in your foreground app process or another process.

## Create the Background Task class

> [!TIP]
> This scenario is not applicable to UWP applications. UWP applications will encounter errors trying to implement this scenario.

You can run code in the background by writing classes that implement the [**IBackgroundTask**](https://docs.microsoft.com/uwp/api/Windows.ApplicationModel.Background.IBackgroundTask) interface. This code runs when a specific event is triggered by using, for example, [**SystemTrigger**](https://docs.microsoft.com/uwp/api/Windows.ApplicationModel.Background.SystemTriggerType) or [**TimeTrigger**](https://docs.microsoft.com/en-us/uwp/api/Windows.ApplicationModel.Background.TimeTrigger).

The following steps show you how to write a new class that implements the [**IBackgroundTask**](https://docs.microsoft.com/uwp/api/Windows.ApplicationModel.Background.IBackgroundTask) interface and add it to your main process.

1.  [**See these instructions**](https://docs.microsoft.com/en-us/windows/apps/desktop/modernize/desktop-to-uwp-enhance) to reference WinRT APIs in your packaged WinMain application solution. This is required to use the IBackgroundTask and related APIs.
2.  In that new class, implement the [**IBackgroundTask**](/uwp/api/Windows.ApplicationModel.Background.IBackgroundTask) interface. The [**IBackgroundTask.Run**](/uwp/api/windows.applicationmodel.background.ibackgroundtask.run) method is a required entry point that will be called when the specified event is triggered; this method is required in every background task.

> [!NOTE]
> The background task class itself&mdash;and all other classes in the background task project&mdash;need to be **public**.

The following sample code shows a basic a background task class that counts primes and writes it to a file until it is requested to be canceled.

The C++/WinRT example implements the background task class as a [**COM coclass**](https://docs.microsoft.com/en-us/windows/uwp/cpp-and-winrt-apis/author-coclasses#implement-the-coclass-and-class-factory).


<details>
<summary>Background task code sample</summary>
<p>

```csharp

using System.Text; // UnicodeEncoding
using System.Threading; // Interlocked
using System.Collections.Generic; // Queue
using System.Runtime.InteropServices; // Guid
using Windows.ApplicationModel.Background; // IBackgroundTask
namespace PackagedWinMainBackgroundTaskSample
{
    // the clsid to register this task with BackgroundTaskBuilder. Generate a random GUID before implementing.
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [Guid("14C5882B-35D3-41BE-86B2-5106269B97E6")]
    [ComSourceInterfaces(typeof(IBackgroundTask))]
    public class SampleTask : IBackgroundTask
    {
        private volatile int cleanupTask; // flag used to indicate to Run method that it should exit
        private Queue<int> numbersQueue; // the data structure holding the set of primes in memory

        private const int maxPrimeNumber = 100000; // the number up to which task will attempt to calculate primes
        private const int queueDepthToWrite = 10; // how frequently this task should flush its queue of primes
        private const string numbersQueueFile = "numbersQueue.log"; // the file to write to relative to AppData

        public SampleTask()
        {
            cleanupTask = 0;
            numbersQueue = new Queue<int>(queueDepthToWrite);
        }

        /// <summary>
        /// This method writes all the numbers in the current queue to the specified file.
        /// </summary>
        private void FlushNumbersToFile(Queue<int> queueToWrite)
        {
            string logPath = Path.Combine(ApplicationData.Current.LocalFolder.Path,
                                        System.Diagnostics.Process.GetCurrentProcess().ProcessName);

            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }

            logPath = Path.Combine(logPath, numbersQueueFile);

            const string delimiter = ", ";
            UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
            // convert the queue to a list of comma separated values.
            string stringToWrite = String.Join(delimiter, queueToWrite);
            // Add the comma at the end.
            stringToWrite += delimiter;

            File.AppendAllText(logPath, stringToWrite);
        }

        /// <summary>
        /// This method determines if the specified number is a prime number.
        /// </summary>
        private bool IsPrimeNumber(int dividend)
        {
            bool isPrime = true;
            for (int divisor = dividend - 1; divisor > 1; divisor -= 1)
            {
                if ((dividend % divisor) == 0)
                {
                    isPrime = false;
                    break;
                }
            }

            return isPrime;
        }

        /// <summary>
        /// Given the current number, this method calculates the next prime number (excluding the specified number).
        /// </summary>
        private int GetNextPrime(int previousNumber)
        {
            int currentNumber = previousNumber + 1;
            while (!IsPrimeNumber(currentNumber))
            {
                currentNumber += 1;
            }
    
            return currentNumber;
        }

        /// <summary>
        /// This method is the main entry point for the background task. The system will believe this background task
        /// is complete when this method returns.
        /// </summary>
        [MTAThread]
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            // Start with the first applicable number.
            int currentNumber = 1;

            taskDeferral = taskInstance.GetDeferral();

            // Wire the cancellation handler.
            taskInstance.Canceled += this.OnCanceled;

            // Set the progress to indicate this task has started
            taskInstance.Progress = 10;

            // Calculate primes until a cancellation has been requested or until
            // the maximum number is reached.
            while ((cleanupTask == 0) && (currentNumber < maxPrimeNumber)) {
                // Compute the next prime number and add it to our queue.
                currentNumber = GetNextPrime(currentNumber);
                numbersQueue.Enqueue(currentNumber);
                // Once the queue is filled to its max size, flush the numbers to the file.
                if (numbersQueue.Count >= queueDepthToWrite)
                {
                    FlushNumbersToFile(numbersQueue);
                    numbersQueue.Clear();
                }
            }

            // Flush any remaining numbers to the file as part of cleanup.
            FlushNumbersToFile(numbersQueue);

            if (taskDeferral != null)
            {
                taskDeferral.Complete();
            }
        }

        /// <summary>
        /// This method is signaled when the system requests the background task be canceled. This method will signal
        /// to the Run method to clean up and return.
        /// </summary>
        [MTAThread]
        public void OnCanceled(IBackgroundTaskInstance taskInstance, BackgroundTaskCancellationReason cancellationReason)
        {
            cleanupTask = 1;
        }
    }
}

```

```cppwinrt

#include <unknwn.h>
#include <winrt/Windows.Foundation.h>
#include <winrt/Windows.Foundation.Collections.h>
#include <winrt/Windows.ApplicationModel.Background.h>

using namespace winrt;
using namespace winrt::Windows::Foundation;
using namespace winrt::Windows::Foundation::Collections;
using namespace winrt::Windows::ApplicationModel::Background;

namespace Win32BackgroundExecutionApiSample {

    handle _taskFactoryCompletionEvent{ CreateEvent(nullptr, false, false, nullptr) };

    // Note insert unique UUID.
    struct __declspec(uuid("14C5882B-35D3-41BE-86B2-5106269B97E6"))
    SampleTask : implements<SampleTask, IBackgroundTask>
    {
        const unsigned int MaximumPotentialPrime = 1000000000;
        volatile bool IsCanceled = false;
        BackgroundTaskDeferral TaskDeferral = nullptr;

        void __stdcall Run (_In_ IBackgroundTaskInstance taskInstance)
        {
            taskInstance.Canceled({ this, &SampleTask::OnCanceled });

            TaskDeferral = taskInstance.GetDeferral();

            unsigned int currentPrimeNumber = 1;
            while (!IsCanceled && (currentPrimeNumber < MaximumPotentialPrime))
            {
                currentPrimeNumber = GetNextPrime(currentPrimeNumber);
            }

            check_bool(SetEvent(_taskFactoryCompletionEvent.get()));
            TaskDeferral.Complete();
        }

        void __stdcall OnCanceled (_In_ IBackgroundTaskInstance, _In_ BackgroundTaskCancellationReason)
        {
            IsCanceled = true;
        }
    }; // class SampleTask

    struct TaskFactory : implements<TaskFactory, IClassFactory>
    {
        HRESULT __stdcall CreateInstance (_In_opt_ IUnknown* aggregateInterface, _In_ REFIID interfaceId, _Outptr_ VOID** object) noexcept final
        {
            if (aggregateInterface != NULL) {
                return CLASS_E_NOAGGREGATION;
            }

            return make<SampleTask>().as(interfaceId, object);
        }

        HRESULT __stdcall LockServer (BOOL) noexcept final
        {
            return S_OK;
        }
    };
}

```

</p>
</details> 


## Add the support code to instantiate the COM class 

In order for the background task to be activated into a full trust winmain application, the background task class must have support code such that COM understands how to start the app process if it is not running, and then understanding which instance of the process is currently the server for handling new activations for that background task.

1.  COM needs to understand how to launch the app process if it is not running already. The app process that hosts the background task code needs to be declared in the package manifest. The following sample code shows how the **SampleTask** is hosted inside **SampleBackgroundApp.exe**. When the background task is launched when no process is running, **SampleBackgroundApp.exe** will be launched with process arguments **"-StartSampleTaskServer"**.

```xml

<Extensions>
  <com:Extension Category="windows.comServer">
    <com:ComServer>
      <com:ExeServer Executable="SampleBackgroundApp\SampleBackgroundApp.exe" DisplayName="SampleBackgroundApp" Arguments="-StartSampleTaskServer">
        <com:Class Id="14C5882B-35D3-41BE-86B2-5106269B97E6" DisplayName="Sample Task" />
      </com:ExeServer>
    </com:ComServer>
  </com:Extension>
</Extensions>

```

2.  Once your process is started with the right arguments, it should tell COM that it is the current COM server for new instances of SampleTask. The following sample code shows how the application process should register itself with COM. Note these samples indicate how the process would declare itself as the COM server for SampleTask for at least one instance to complete before exiting. This is optional, and handling a background task may start your main process functions.

```csharp

class SampleTaskServer
{
    SampleTaskServer()
    {
        comRegistrationToken = 0;
        waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
    }

    ~SampleTaskServer()
    {
        Stop();
    }

    public void Start()
    {
        RegistrationServices registrationServices = new RegistrationServices();
        comRegistrationToken = registrationServices.RegisterTypeForComClients(typeof(SampleTask), RegistrationClassContext.LocalServer, RegistrationConnectionType.MultipleUse);

        // Either have the background task signal this handle when it completes, or never signal this handle to keep this
        // process as the COM server until it is closed. 
        waitHandle.WaitOne();
    }

    public void Stop()
    {
        if (comRegistrationToken != 0)
        {
            registrationServices.UnregisterTypeForComClients(registrationCookie);
        }

        waitHandle.Set();
    }

    private int comRegistrationToken;
    private EventWaitHandle waitHandle;
};

SampleTaskServer sampleTaskServer = new SampleTaskServer();
sampleTaskServer.Start();

```

```cpp

class SampleTaskServer
{
public:
    SampleTaskServer()
    {
        waitHandle = EventWaitHandle(false, EventResetMode::AutoResetEvent);
        comRegistrationToken = 0;
    }

    ~SampleTaskServer()
    {
        Stop();
    }

    void Start()
    {
        try
        {
            // Verify the global handle is correctly constructed.
            check_bool(bool { _taskFactoryCompletionEvent });

            com_ptr<IClassFactory> taskFactory = make<TaskFactory>();

            winrt::check_hresult(CoRegisterClassObject(__uuidof(SampleTask),
                                                       taskFactory.get(),
                                                       CLSCTX_LOCAL_SERVER,
                                                       REGCLS_MULTIPLEUSE,
                                                       &comRegistrationToken));

            // This process now handles all new instances of SampleTask until it is closed.
            Sleep(INFINITE);

        }
        catch (...)
        {
            // Indicate an error has been encountered.
        }
    }

    void Stop()
    {
        if (comRegistrationToken != 0)
        {
            CoRevokeClassObject(comRegistrationToken);
        }

        waitHandle.Set();
    }

private:
    DWORD comRegistrationToken;
    EventWaitHandle waitHandle;
};

SampleTaskServer sampleTaskServer;
sampleTaskServer.Start();

```


## Register the background task to run

1.  Find out whether the background task is already registered by iterating through the [**BackgroundTaskRegistration.AllTasks**](https://docs.microsoft.com/uwp/api/windows.applicationmodel.background.backgroundtaskregistration.alltasks) property. *This step is important*; if your app doesn't check for existing background task registrations, it could easily register the task multiple times, causing issues with performance and maxing out the task's available CPU time before work can complete. An application is free to use the same entry point to handle all background tasks and use other properties like the [**Name**](https://docs.microsoft.com/en-us/uwp/api/windows.applicationmodel.background.backgroundtaskregistration.name#Windows_ApplicationModel_Background_BackgroundTaskRegistration_Name) or [**TaskId**](https://docs.microsoft.com/en-us/uwp/api/windows.applicationmodel.background.backgroundtaskregistration.taskid#Windows_ApplicationModel_Background_BackgroundTaskRegistration_TaskId) assigned to a [**BackgroundTaskRegistration**](https://docs.microsoft.com/uwp/api/windows.applicationmodel.background.backgroundtaskregistration) to decide what work should be done.

The following example iterates on the **AllTasks** property and sets a flag variable to true if the task is already registered.

```csharp

var taskRegistered = false;
var sampleTaskName = "SampleTask";

foreach (var task in BackgroundTaskRegistration.AllTasks)
{
    if (task.Value.Name == sampleTaskName)
    {
        taskRegistered = true;
        break;
    }
}

// The code in the next step goes here.

```

```cppwinrt

bool taskRegistered = false;
std::wstring sampleTaskName = L"SampleTask";
auto allTasks{ BackgroundTaskRegistration::AllTasks() };

for (auto const& task : allTasks)
{
    if (task.Value().Name() == sampleTaskName)
    {
        taskRegistered = true;
        break;
    }
}

// The code in the next step goes here.

```

1.  If the background task is not already registered, use [**BackgroundTaskBuilder**](https://docs.microsoft.com/uwp/api/Windows.ApplicationModel.Background.BackgroundTaskBuilder) to create an instance of your background task. The task entry point should be the name of your background task class prefixed by the namespace.

The background task trigger controls when the background task will run. For a list of possible triggers, see the [**Windows.ApplicationModel.Background**](https://docs.microsoft.com/en-us/uwp/api/windows.applicationmodel.background) Namespace.

> [!NOTE]
> Only a subset of triggers are supported for packaged winmain background tasks.

For example, this code creates a new background task and sets it to run it on a 15-minute recurring [**TimeTrigger**]():

```csharp

if (!taskRegistered)
{
    var builder = new BackgroundTaskBuilder();

    builder.Name = sampleTaskName;
    builder.SetTaskEntryPointClsid(typeof(SampleTask).GUID);
    builder.SetTrigger(new TimeTrigger(15, false));
}

// The code in the next step goes here.

```

```cppwinrt

if (!taskRegistered)
{
    BackgroundTaskBuilder builder;
    
    builder.Name(sampleTaskName);
    builder.SetTaskEntryPointClsid(__uuidof(SampleTask));
    builder.SetTrigger(TimeTrigger(15, false));
}

// The code in the next step goes here.

```

1.  You can add a condition to control when your task will run after the trigger event occurs (optional). For example, if you don't want the task to run until internet is available, use the condition **InternetAvailable**. For a list of possible conditions, see [**SystemConditionType**](https://docs.microsoft.com/uwp/api/Windows.ApplicationModel.Background.SystemConditionType).

The following sample code assigns a condition requiring the user to be present:

```csharp
builder.AddCondition(new SystemCondition(SystemConditionType.InternetAvailable));
// The code in the next step goes here.
```

```cppwinrt
builder.AddCondition(SystemCondition{ SystemConditionType::InternetAvailable });
// The code in the next step goes here.
```

4.  Register the background task by calling the Register method on the [**BackgroundTaskBuilder**](https://docs.microsoft.com/uwp/api/Windows.ApplicationModel.Background.BackgroundTaskBuilder) object. Store the [**BackgroundTaskRegistration**](https://docs.microsoft.com/uwp/api/Windows.ApplicationModel.Background.BackgroundTaskRegistration) result so it can be used in the next step. Note that the register function may return errors in the form of exceptions. Be sure to call Register in a try-catch.

The following code registers the background task and stores the result:

```csharp

try
{
    BackgroundTaskRegistration task = builder.Register();
}
catch (...)
{
    // Indicate an error was encountered.
}

```

```cppwinrt

try
{
    BackgroundTaskRegistration task = builder.Register();
}
catch (...)
{
    // Indicate an error was encountered.
}

```











## Remarks

Unlike UWP apps that can run background tasks in modern standby, WinMain apps cannot run code from the lower power phases of modern standby. See [Modern Standby](https://docs.microsoft.com/en-us/windows-hardware/design/device-experiences/modern-standby) to learn more.

## Related topics

* [Respond to system events with background tasks](respond-to-system-events-with-background-tasks.md)
* [Register a background task](register-a-background-task.md)
* [Set conditions for running a background task](set-conditions-for-running-a-background-task.md)
* [Use a maintenance trigger](use-a-maintenance-trigger.md)
* [Handle a cancelled background task](handle-a-cancelled-background-task.md)
* [Monitor background task progress and completion](monitor-background-task-progress-and-completion.md)
* [Run a background task on a timer](run-a-background-task-on-a-timer-.md)
* [Create and register an in-process background task](create-and-register-an-inproc-background-task.md).
* [Convert an out-of-process background task to an in-process background task](convert-out-of-process-background-task.md)  

**Background task guidance**

* [Guidelines for background tasks](guidelines-for-background-tasks.md)
* [Debug a background task](debug-a-background-task.md)
* [How to trigger suspend, resume, and background events in UWP apps (when debugging)](https://msdn.microsoft.com/library/windows/apps/hh974425(v=vs.110).aspx)

**Background Task API Reference**

* [**Windows.ApplicationModel.Background**](https://docs.microsoft.com/uwp/api/Windows.ApplicationModel.Background)
