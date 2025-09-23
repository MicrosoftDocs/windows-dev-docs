#include "pch.h"
#include "App.h"

#include <ppltasks.h>
#include <collection.h>
using namespace AppRecordingExample;

using namespace concurrency;
using namespace Windows::ApplicationModel;
using namespace Windows::ApplicationModel::Core;
using namespace Windows::ApplicationModel::Activation;
using namespace Windows::UI::Core;
using namespace Windows::UI::Input;
using namespace Windows::System;
using namespace Windows::Foundation;
using namespace Windows::Graphics::Display;
using namespace Windows::Storage;


using namespace Windows::Media::AppRecording;
using namespace Windows::Media::Capture;

// The main function is only used to initialize our IFrameworkView class.
[Platform::MTAThread]
int main(Platform::Array<Platform::String^>^)
{
	auto direct3DApplicationSource = ref new Direct3DApplicationSource();
	CoreApplication::Run(direct3DApplicationSource);
	return 0;
}

IFrameworkView^ Direct3DApplicationSource::CreateView()
{
	return ref new App();
}

App::App() :
	m_windowClosed(false),
	m_windowVisible(true)
{
}

// The first method called when the IFrameworkView is being created.
void App::Initialize(CoreApplicationView^ applicationView)
{
	// Register event handlers for app lifecycle. This example includes Activated, so that we
	// can make the CoreWindow active and start rendering on the window.
	applicationView->Activated +=
		ref new TypedEventHandler<CoreApplicationView^, IActivatedEventArgs^>(this, &App::OnActivated);

	CoreApplication::Suspending +=
		ref new EventHandler<SuspendingEventArgs^>(this, &App::OnSuspending);

	CoreApplication::Resuming +=
		ref new EventHandler<Platform::Object^>(this, &App::OnResuming);

	// At this point we have access to the device. 
	// We can create the device-dependent resources.
	m_deviceResources = std::make_shared<DX::DeviceResources>();


	// <SnippetGetAppRecordingManager>
	if (Windows::Foundation::Metadata::ApiInformation::IsApiContractPresent(
		"Windows.Media.AppRecording.AppRecordingContract", 1, 0))
	{
		m_appRecordingManager = AppRecordingManager::GetDefault();
	}
	// </SnippetGetAppRecordingManager>
	

	// <SnippetGetMetadataWriter>
	if (Windows::Foundation::Metadata::ApiInformation::IsApiContractPresent("Windows.Media.Capture.AppCaptureMetadataContract", 1, 0))
	{
		m_appCaptureMetadataWriter = ref new AppCaptureMetadataWriter();
	}
	// </SnippetGetMetadataWriter>

}

// Called when the CoreWindow object is created (or re-created).
void App::SetWindow(CoreWindow^ window)
{
	window->SizeChanged += 
		ref new TypedEventHandler<CoreWindow^, WindowSizeChangedEventArgs^>(this, &App::OnWindowSizeChanged);

	window->VisibilityChanged +=
		ref new TypedEventHandler<CoreWindow^, VisibilityChangedEventArgs^>(this, &App::OnVisibilityChanged);

	window->Closed += 
		ref new TypedEventHandler<CoreWindow^, CoreWindowEventArgs^>(this, &App::OnWindowClosed);

	DisplayInformation^ currentDisplayInformation = DisplayInformation::GetForCurrentView();

	currentDisplayInformation->DpiChanged +=
		ref new TypedEventHandler<DisplayInformation^, Object^>(this, &App::OnDpiChanged);

	currentDisplayInformation->OrientationChanged +=
		ref new TypedEventHandler<DisplayInformation^, Object^>(this, &App::OnOrientationChanged);

	DisplayInformation::DisplayContentsInvalidated +=
		ref new TypedEventHandler<DisplayInformation^, Object^>(this, &App::OnDisplayContentsInvalidated);

	window->KeyDown +=
		ref new TypedEventHandler<CoreWindow^, KeyEventArgs^>(this, &App::OnKeyDown);

	// <SnippetRegisterCapturingChanged>
	Windows::Media::Capture::AppCapture^ appCapture = Windows::Media::Capture::AppCapture::GetForCurrentView();
	appCapture->CapturingChanged +=
		ref new TypedEventHandler<Windows::Media::Capture::AppCapture^, Platform::Object^>(this, &App::OnCapturingChanged);
	// </SnippetRegisterCapturingChanged>

	// <SnippetRegisterMetadataPurged>
	if (m_appCaptureMetadataWriter != nullptr)
	{
		m_appCaptureMetadataWriter->MetadataPurged += 
			ref new TypedEventHandler<AppCaptureMetadataWriter^, Platform::Object^>(this, &App::OnMetadataPurged);

	}
	// </SnippetRegisterMetadataPurged>

	m_deviceResources->SetWindow(window);
}

// Initializes scene resources, or loads a previously saved app state.
void App::Load(Platform::String^ entryPoint)
{
	if (m_main == nullptr)
	{
		m_main = std::unique_ptr<AppRecordingExampleMain>(new AppRecordingExampleMain(m_deviceResources));
	}
}

// This method is called after the window becomes active.
void App::Run()
{
	while (!m_windowClosed)
	{
		if (m_windowVisible)
		{
			CoreWindow::GetForCurrentThread()->Dispatcher->ProcessEvents(CoreProcessEventsOption::ProcessAllIfPresent);

			m_main->Update();

			if (m_main->Render())
			{
				m_deviceResources->Present();
			}
		}
		else
		{
			CoreWindow::GetForCurrentThread()->Dispatcher->ProcessEvents(CoreProcessEventsOption::ProcessOneAndAllPending);
		}
	}
}

// Required for IFrameworkView.
// Terminate events do not cause Uninitialize to be called. It will be called if your IFrameworkView
// class is torn down while the app is in the foreground.
void App::Uninitialize()
{
}

// Application lifecycle event handlers.

void App::OnActivated(CoreApplicationView^ applicationView, IActivatedEventArgs^ args)
{
	// Run() won't start until the CoreWindow is activated.
	CoreWindow::GetForCurrentThread()->Activate();
}

void App::OnSuspending(Platform::Object^ sender, SuspendingEventArgs^ args)
{
	// Save app state asynchronously after requesting a deferral. Holding a deferral
	// indicates that the application is busy performing suspending operations. Be
	// aware that a deferral may not be held indefinitely. After about five seconds,
	// the app will be forced to exit.
	SuspendingDeferral^ deferral = args->SuspendingOperation->GetDeferral();

	create_task([this, deferral]()
	{
        m_deviceResources->Trim();

		// Insert your code here.

		deferral->Complete();
	});
}

void App::OnResuming(Platform::Object^ sender, Platform::Object^ args)
{
	// Restore any data or state that was unloaded on suspend. By default, data
	// and state are persisted when resuming from suspend. Note that this event
	// does not occur if the app was previously terminated.

	// Insert your code here.
}

// Window event handlers.

void App::OnWindowSizeChanged(CoreWindow^ sender, WindowSizeChangedEventArgs^ args)
{
	m_deviceResources->SetLogicalSize(Size(sender->Bounds.Width, sender->Bounds.Height));
	m_main->CreateWindowSizeDependentResources();
}

void App::OnVisibilityChanged(CoreWindow^ sender, VisibilityChangedEventArgs^ args)
{
	m_windowVisible = args->Visible;
}

void App::OnWindowClosed(CoreWindow^ sender, CoreWindowEventArgs^ args)
{
	m_windowClosed = true;
}

// DisplayInformation event handlers.

void App::OnDpiChanged(DisplayInformation^ sender, Object^ args)
{
	// Note: The value for LogicalDpi retrieved here may not match the effective DPI of the app
	// if it is being scaled for high resolution devices. Once the DPI is set on DeviceResources,
	// you should always retrieve it using the GetDpi method.
	// See DeviceResources.cpp for more details.
	m_deviceResources->SetDpi(sender->LogicalDpi);
	m_main->CreateWindowSizeDependentResources();
}

void App::OnOrientationChanged(DisplayInformation^ sender, Object^ args)
{
	m_deviceResources->SetCurrentOrientation(sender->CurrentOrientation);
	m_main->CreateWindowSizeDependentResources();
}

void App::OnDisplayContentsInvalidated(DisplayInformation^ sender, Object^ args)
{
	m_deviceResources->ValidateDevice();
}
// <SnippetCanRecord>
bool App::CanRecord()
{

	if (m_appRecordingManager == nullptr)
	{
		return false;
	}

	AppRecordingStatus^ recordingStatus = m_appRecordingManager->GetStatus();

	if (!recordingStatus->CanRecord)
	{
		AppRecordingStatusDetails^ details = recordingStatus->Details;
	
		if (details->IsAnyAppBroadcasting)
		{
			UpdateStatusText("Another app is currently broadcasting.");
			return false;
		}

		if (details->IsCaptureResourceUnavailable)
		{
			UpdateStatusText("The capture resource is currently unavailable.");
			return false;
		}

		if (details->IsGameStreamInProgress)
		{
			UpdateStatusText("A game stream is currently in progress.");
			return false;
		}

		if (details->IsGpuConstrained)
		{
			// Typically, this means that the GPU software does not include an H264 encoder
			UpdateStatusText("The GPU does not support app recording.");
			return false;
		}

		
		if (details->IsAppInactive)
		{
			// Broadcasting can only be started when the application's window is the active window.
			UpdateStatusText("The app window to be recorded is not active.");
			return false;
		}

		if (details->IsBlockedForApp)
		{
			UpdateStatusText("Recording is blocked for this app.");
			return false;
		}

		if (details->IsDisabledByUser)
		{
			UpdateStatusText("The user has disabled GameBar in Windows Settings.");
			return false;
		}

		if (details->IsDisabledBySystem)
		{
			UpdateStatusText("Recording is disabled by the system.");
			return false;
		}

		
		return false;
	}


	return true;
}
// </SnippetCanRecord>

// <SnippetStartRecordToFile>
void App::StartRecordToFile(Windows::Storage::StorageFile^ file)
{

	if (m_appRecordingManager == nullptr)
	{
		return;
	}

	if (!CanRecord())
	{
		return;
	}


	// Start a recording operation to record starting from 
	// now until the operation fails or is cancelled. 
	m_recordOperation = m_appRecordingManager->StartRecordingToFileAsync(file);

	create_task(m_recordOperation).then(
		[this](AppRecordingResult^ result)
	{
		OnRecordingComplete();
	}).then([this](task<void> t)
	{
		try
		{
			t.get();
		}
		catch (const task_canceled&)
		{
			OnRecordingComplete();
		}
	});
}
// </SnippetStartRecordToFile>


// <SnippetOnRecordingComplete>
void App::OnRecordingComplete()
{
	if (m_recordOperation)
	{
		auto result = m_recordOperation->GetResults();

		if (result->Succeeded)
		{
			Windows::Foundation::TimeSpan duration = result->Duration;
			boolean isTruncated = result->IsFileTruncated;

			UpdateStatusText("Recording completed.");
		}
		else
		{
			// If the recording failed, ExtendedError 
			// can be retrieved and used for diagnostic purposes 
			HResult extendedError = result->ExtendedError;
			LogTelemetryMessage("Error during recording: " + extendedError);
		}

		m_recordOperation = nullptr;
	}
}
// </SnippetOnRecordingComplete>

// <SnippetFinishRecordToFile>
void App::FinishRecordToFile()
{
	m_recordOperation->Cancel();
}
// </SnippetFinishRecordToFile>

// <SnippetCanRecordTimeSpan>
bool App::CanRecordTimeSpan(TimeSpan &historicalDurationBuffer)
{

	if (m_appRecordingManager == nullptr)
	{
		return false;
	}

	AppRecordingStatus^ recordingStatus = m_appRecordingManager->GetStatus();
	if (recordingStatus->Details->IsTimeSpanRecordingDisabled)
	{
		UpdateStatusText("Historical time span recording is disabled by the system.");
		return false;
	}

	historicalDurationBuffer = recordingStatus->HistoricalBufferDuration;

	return true;
}
// </SnippetCanRecordTimeSpan>
// <SnippetRecordTimeSpanToFile>
void App::RecordTimeSpanToFile(Windows::Storage::StorageFile^ file)
{


	if (m_appRecordingManager == nullptr)
	{
		return;
	}

	if (!CanRecord())
	{
		return;
	}

	Windows::Foundation::TimeSpan historicalBufferDuration;
	if (!CanRecordTimeSpan(historicalBufferDuration))
	{
		return;
	}
	

	AppRecordingStatus^ recordingStatus = m_appRecordingManager->GetStatus();
	
	Windows::Globalization::Calendar^ calendar = ref new Windows::Globalization::Calendar();
	calendar->SetToNow();

	Windows::Foundation::DateTime nowTime = calendar->GetDateTime();

	int secondsToRecord = min(30, historicalBufferDuration.Duration / 10000000);
	calendar->AddSeconds(-1 * secondsToRecord);

	Windows::Foundation::DateTime  startTime = calendar->GetDateTime();

	Windows::Foundation::TimeSpan duration;

	duration.Duration = nowTime.UniversalTime - startTime.UniversalTime;

	create_task(m_appRecordingManager->RecordTimeSpanToFileAsync(startTime, duration, file)).then(
		[this](AppRecordingResult^ result)
	{
		if (result->Succeeded)
		{
			Windows::Foundation::TimeSpan duration = result->Duration;
			boolean isTruncated = result->IsFileTruncated;
			UpdateStatusText("Recording completed.");
		}
		else
		{
			// If the recording failed, ExtendedError
			// can be retrieved and used for diagnostic purposes
			HResult extendedError = result->ExtendedError;
			LogTelemetryMessage("Error during recording: " + extendedError);
		}
	});

}
// </SnippetRecordTimeSpanToFile>


// <SnippetSaveScreenShotToFiles>
void App::SaveScreenShotToFiles(Windows::Storage::StorageFolder^ folder, Platform::String^ filenamePrefix)
{

	if (m_appRecordingManager == nullptr)
	{
		return;
	}


	Windows::Foundation::Collections::IVectorView<Platform::String^>^ supportedFormats = 
		m_appRecordingManager->SupportedScreenshotMediaEncodingSubtypes;

	
	Platform::Collections::Vector<Platform::String^>^ requestedFormats = 
		ref new Platform::Collections::Vector<Platform::String^>();

	for (Platform::String^ format : requestedFormats)
	{
		if (format == Windows::Media::MediaProperties::MediaEncodingSubtypes::Png)
		{
			requestedFormats->Append(format);
		}
		else if (format == Windows::Media::MediaProperties::MediaEncodingSubtypes::JpegXr)
		{
			requestedFormats->Append(format);
		}
	}


	create_task(m_appRecordingManager->SaveScreenshotToFilesAsync(folder, filenamePrefix, AppRecordingSaveScreenshotOption::None,
		requestedFormats->GetView())).then(
			[this](AppRecordingSaveScreenshotResult^ result)
	{
		if (result->Succeeded)
		{
			Windows::Foundation::Collections::IVectorView<AppRecordingSavedScreenshotInfo^>^ returnedScreenshots = result->SavedScreenshotInfos;

			for (AppRecordingSavedScreenshotInfo^ screenshotInfo : returnedScreenshots)
			{
				Windows::Storage::StorageFile^ file = screenshotInfo->File;
				Platform::String^ type = screenshotInfo->MediaEncodingSubtype;
			}
		}
		else
		{
			// If the recording failed, ExtendedError 
			// can be retrieved and used for diagnostic purposes 
			HResult extendedError = result->ExtendedError;
			LogTelemetryMessage("Error during screenshot: " + extendedError);
		}
	});
}
// </SnippetSaveScreenShotToFiles>


void App::SetAppCaptureAllowed(bool allowed)
{
	// <SnippetSetAppCaptureAllowed>
	Windows::Media::Capture::AppCapture::SetAllowedAsync(allowed);
	// </SnippetSetAppCaptureAllowed>
}

// <SnippetOnCapturingChanged>
void App::OnCapturingChanged(Windows::Media::Capture::AppCapture^ sender, Platform::Object^ args)
{
	Platform::String^ captureStatusText = "";
	if (sender->IsCapturingAudio)
	{
		captureStatusText += "Capturing audio.";
	}
	if (sender->IsCapturingVideo)
	{
		captureStatusText += "Capturing video.";
	}
	UpdateStatusText(captureStatusText);
}
// </SnippetOnCapturingChanged>


// <SnippetStartSession>
void App::StartSession(Platform::String^ sessionId, double averageFps, int resolutionWidth, int resolutionHeight)
{
	if (m_appCaptureMetadataWriter != nullptr)
	{
		m_appCaptureMetadataWriter->AddStringEvent("sessionId", sessionId, AppCaptureMetadataPriority::Informational);
		m_appCaptureMetadataWriter->AddDoubleEvent("averageFps", averageFps, AppCaptureMetadataPriority::Informational);
		m_appCaptureMetadataWriter->AddInt32Event("resolutionWidth", resolutionWidth, AppCaptureMetadataPriority::Informational);
		m_appCaptureMetadataWriter->AddInt32Event("resolutionHeight", resolutionHeight, AppCaptureMetadataPriority::Informational);
	}
}
// </SnippetStartSession>

// <SnippetStartMap>
void App::StartMap(Platform::String^ mapName)
{
	m_appCaptureMetadataWriter->StartStringState("map", mapName, AppCaptureMetadataPriority::Important);
}
// </SnippetStartMap>

// <SnippetEndMap>
void App::EndMap(Platform::String^ mapName)
{
	m_appCaptureMetadataWriter->StopState("map");
}
// </SnippetEndMap>


// <SnippetLevelUp>
void App::LevelUp(int newLevel)
{
	m_appCaptureMetadataWriter->StartInt32State("currentLevel", newLevel, AppCaptureMetadataPriority::Important);
}
// </SnippetLevelUp>

// <SnippetLapComplete>
void App::LapComplete(double lapTimeSeconds)
{
	m_appCaptureMetadataWriter->AddDoubleEvent("lapComplete", lapTimeSeconds, AppCaptureMetadataPriority::Important);
}
// </SnippetLapComplete>

// <SnippetRaceComplete>
void App::RaceComplete()
{
	m_appCaptureMetadataWriter->StopAllStates();
}
// </SnippetRaceComplete>

// <SnippetCheckMetadataStorage>
void App::CheckMetadataStorage()
{
	INT64 storageRemaining = m_appCaptureMetadataWriter->RemainingStorageBytesAvailable;

	if (storageRemaining < m_myLowStorageLevelInBytes)
	{
		m_writeLowPriorityMetadata = false;
	}
}
// </SnippetCheckMetadataStorage>

// <SnippetComboExecuted>
void App::ComboExecuted(Platform::String^ comboName)
{
	if (m_writeLowPriorityMetadata)
	{
		m_appCaptureMetadataWriter->AddStringEvent("combo", comboName, AppCaptureMetadataPriority::Informational);
	}
}
// </SnippetComboExecuted>

// <SnippetOnMetadataPurged>
void App::OnMetadataPurged(Windows::Media::Capture::AppCaptureMetadataWriter^ sender, Platform::Object^ args)
{
	// Reduce metadata by stopping a low-priority state.
	//m_appCaptureMetadataWriter->StopState("map");

	// Reduce metadata by stopping all states.
	//m_appCaptureMetadataWriter->StopAllStates();

	// Change app-specific behavior to write less metadata.
	//m_writeLowPriorityMetadata = false;

	// Take no action. Let the system purge data as needed. Record event for telemetry.
	OutputDebugString(TEXT("Low-priority metadata purged."));

}
// </SnippetOnMetadataPurged>

void App::UpdateStatusText(Platform::String^ status)
{
	OutputDebugString(status->Data());
}
void App::LogTelementryMessage(Platform::String^ status)
{
	OutputDebugString(status->Data());
}
void App::OnKeyDown(CoreWindow^ sender, KeyEventArgs^ args)
{
	if (args->VirtualKey == Windows::System::VirtualKey::R)
	{

		if (m_recordOperation == nullptr)
		{

			// <SnippetCallStartRecordToFile>
			StorageFolder^ storageFolder = ApplicationData::Current->LocalFolder;
			concurrency::create_task(storageFolder->CreateFileAsync("recordtofile_example.mp4", CreationCollisionOption::ReplaceExisting)).then(
				[this](StorageFile^ file)
			{
				StartRecordToFile(file);
			});
			// </SnippetCallStartRecordToFile>
		}
		else
		{
			FinishRecordToFile();
		}
	}
	if (args->VirtualKey == Windows::System::VirtualKey::T)
	{

		// <SnippetCallRecordTimeSpanToFile>
		StorageFolder^ storageFolder = ApplicationData::Current->LocalFolder;
		concurrency::create_task(storageFolder->CreateFileAsync("recordtimespantofile_example.mp4", CreationCollisionOption::ReplaceExisting)).then(
			[this](StorageFile^ file)
		{
			RecordTimeSpanToFile(file);
		});
		// </SnippetCallRecordTimeSpanToFile>
	}

	if (args->VirtualKey == Windows::System::VirtualKey::S)
	{

		// <SnippetCallSaveScreenShotToFiles>
		StorageFolder^ storageFolder = ApplicationData::Current->LocalFolder;
		SaveScreenShotToFiles(storageFolder, "screen_capture");
		// </SnippetCallSaveScreenShotToFiles>
	}
	if (args->VirtualKey == Windows::System::VirtualKey::C)
	{
		auto appCapture = Windows::Media::Capture::AppCapture::GetForCurrentView();
		m_appCaptureAllowed = !m_appCaptureAllowed;
		SetAppCaptureAllowed(m_appCaptureAllowed);
		
	}
}