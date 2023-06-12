#pragma once

#include "pch.h"
#include "Common\DeviceResources.h"
#include "AppRecordingExampleMain.h"

namespace AppRecordingExample
{


	// Main entry point for our app. Connects the app with the Windows shell and handles application lifecycle events.
	ref class App sealed : public Windows::ApplicationModel::Core::IFrameworkView
	{
	public:
		App();

		// IFrameworkView Methods.
		virtual void Initialize(Windows::ApplicationModel::Core::CoreApplicationView^ applicationView);
		virtual void SetWindow(Windows::UI::Core::CoreWindow^ window);
		virtual void Load(Platform::String^ entryPoint);
		virtual void Run();
		virtual void Uninitialize();

	protected:
		// Application lifecycle event handlers.
		void OnActivated(Windows::ApplicationModel::Core::CoreApplicationView^ applicationView, Windows::ApplicationModel::Activation::IActivatedEventArgs^ args);
		void OnSuspending(Platform::Object^ sender, Windows::ApplicationModel::SuspendingEventArgs^ args);
		void OnResuming(Platform::Object^ sender, Platform::Object^ args);

		// Window event handlers.
		void OnWindowSizeChanged(Windows::UI::Core::CoreWindow^ sender, Windows::UI::Core::WindowSizeChangedEventArgs^ args);
		void OnVisibilityChanged(Windows::UI::Core::CoreWindow^ sender, Windows::UI::Core::VisibilityChangedEventArgs^ args);
		void OnWindowClosed(Windows::UI::Core::CoreWindow^ sender, Windows::UI::Core::CoreWindowEventArgs^ args);
		void OnKeyDown(Windows::UI::Core::CoreWindow^ sender, Windows::UI::Core::KeyEventArgs^ args);

		// DisplayInformation event handlers.
		void OnDpiChanged(Windows::Graphics::Display::DisplayInformation^ sender, Platform::Object^ args);
		void OnOrientationChanged(Windows::Graphics::Display::DisplayInformation^ sender, Platform::Object^ args);
		void OnDisplayContentsInvalidated(Windows::Graphics::Display::DisplayInformation^ sender, Platform::Object^ args);

		// AppCapture event handlers
		void OnRecordingComplete();
		void OnCapturingChanged(Windows::Media::Capture::AppCapture^ sender, Platform::Object^ args);
		void OnMetadataPurged(Windows::Media::Capture::AppCaptureMetadataWriter^ sender, Platform::Object^ args);

	private:
		std::shared_ptr<DX::DeviceResources> m_deviceResources;
		std::unique_ptr<AppRecordingExampleMain> m_main;
		bool m_windowClosed;
		bool m_windowVisible;


		bool m_appCaptureAllowed;

		bool CanRecord();
		bool CanRecordTimeSpan(Windows::Foundation::TimeSpan &historicalBufferDuration);

		void StartRecordToFile(Windows::Storage::StorageFile^ file);
		void FinishRecordToFile();
		void RecordTimeSpanToFile(Windows::Storage::StorageFile^ file);

		void SaveScreenShotToFiles(Windows::Storage::StorageFolder^ folder, Platform::String^ filenamePrefix);

		void SetAppCaptureAllowed(bool allowed);

		Windows::Media::AppRecording::AppRecordingManager^ m_appRecordingManager;
		Windows::Foundation::IAsyncOperation<Windows::Media::AppRecording::AppRecordingResult^>^ m_recordOperation;

		Windows::Media::Capture::AppCaptureMetadataWriter^ m_appCaptureMetadataWriter;

		void StartSession(Platform::String^ sessionId, double averageFps, int resolutionWidth, int resolutionHeight);

		void StartMap(Platform::String^ mapName);
		void EndMap(Platform::String^ mapName);

		void LevelUp(int newLevel);
		void LapComplete(double lapTimeSeconds);

		void RaceComplete();

		int64 m_myLowStorageLevelInBytes = 10000;
		bool m_writeLowPriorityMetadata = true;
		void CheckMetadataStorage();
		void ComboExecuted(Platform::String^ comboName);

		void UpdateStatusText(Platform::String^ status);
		void LogTelemetryMessage(Platform::String^ status);



	};
}

ref class Direct3DApplicationSource sealed : Windows::ApplicationModel::Core::IFrameworkViewSource
{
public:
	virtual Windows::ApplicationModel::Core::IFrameworkView^ CreateView();
};
