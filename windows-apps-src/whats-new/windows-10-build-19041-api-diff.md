---
title: Windows 10 Build 19041 API changes
description: Developers can use the following list to identify new or changed namespaces in Windows 10 build 19041
keywords: Windows 10, apis, 19041, 2004
ms.date: 05/12/2020
ms.topic: article
ms.localizationpriority: medium
---
# New APIs in Windows 10 build 19041

New and updated API namespaces have been made available to developers in Windows 10 build 19041 (Also known as SDK version 2004). Below is a full list of documentation published for namespaces added or modified in this release.

For information on APIs added in the previous public release, see [New APIs in the Windows 10 October Update](windows-10-build-18362-api-diff.md).

## Windows.AI

### [Windows.AI.MachineLearning](/uwp/api/windows.ai.machinelearning)

#### [LearningModelSessionOptions](/uwp/api/windows.ai.machinelearning.learningmodelsessionoptions)

LearningModelSessionOptions.CloseModelOnSessionCreation

## Windows.ApplicationModel

### [Windows.ApplicationModel.Background](/uwp/api/windows.applicationmodel.background)

#### [BackgroundTaskBuilder](/uwp/api/windows.applicationmodel.background.backgroundtaskbuilder)

BackgroundTaskBuilder.SetTaskEntryPointClsid

#### [BluetoothLEAdvertisementPublisherTrigger](/uwp/api/windows.applicationmodel.background.bluetoothleadvertisementpublishertrigger)

BluetoothLEAdvertisementPublisherTrigger.IncludeTransmitPowerLevel <br> BluetoothLEAdvertisementPublisherTrigger.IsAnonymous <br> BluetoothLEAdvertisementPublisherTrigger.PreferredTransmitPowerLevelInDBm <br> BluetoothLEAdvertisementPublisherTrigger.UseExtendedFormat

#### [BluetoothLEAdvertisementWatcherTrigger](/uwp/api/windows.applicationmodel.background.bluetoothleadvertisementwatchertrigger)

BluetoothLEAdvertisementWatcherTrigger.AllowExtendedAdvertisements

### [Windows.ApplicationModel.ConversationalAgent](/uwp/api/windows.applicationmodel.conversationalagent)

#### [ActivationSignalDetectionConfiguration](/uwp/api/windows.applicationmodel.conversationalagent.activationsignaldetectionconfiguration)

ActivationSignalDetectionConfiguration <br> ActivationSignalDetectionConfiguration.ApplyTrainingData <br> ActivationSignalDetectionConfiguration.ApplyTrainingDataAsync <br> ActivationSignalDetectionConfiguration.AvailabilityChanged <br> ActivationSignalDetectionConfiguration.AvailabilityInfo <br> ActivationSignalDetectionConfiguration.ClearModelData <br> ActivationSignalDetectionConfiguration.ClearModelDataAsync <br> ActivationSignalDetectionConfiguration.ClearTrainingData <br> ActivationSignalDetectionConfiguration.ClearTrainingDataAsync <br> ActivationSignalDetectionConfiguration.DisplayName <br> ActivationSignalDetectionConfiguration.GetModelData <br> ActivationSignalDetectionConfiguration.GetModelDataAsync <br> ActivationSignalDetectionConfiguration.GetModelDataType <br> ActivationSignalDetectionConfiguration.GetModelDataTypeAsync <br> ActivationSignalDetectionConfiguration.IsActive <br> ActivationSignalDetectionConfiguration.ModelId <br> ActivationSignalDetectionConfiguration.SetEnabled <br> ActivationSignalDetectionConfiguration.SetEnabledAsync <br> ActivationSignalDetectionConfiguration.SetModelData <br> ActivationSignalDetectionConfiguration.SetModelDataAsync <br> ActivationSignalDetectionConfiguration.SignalId <br> ActivationSignalDetectionConfiguration.TrainingDataFormat <br> ActivationSignalDetectionConfiguration.TrainingStepsCompleted <br> ActivationSignalDetectionConfiguration.TrainingStepsRemaining

#### [ActivationSignalDetectionTrainingDataFormat](/uwp/api/windows.applicationmodel.conversationalagent.activationsignaldetectiontrainingdataformat)

ActivationSignalDetectionTrainingDataFormat

#### [ActivationSignalDetector](/uwp/api/windows.applicationmodel.conversationalagent.activationsignaldetector)

ActivationSignalDetector <br> ActivationSignalDetector.CanCreateConfigurations <br> ActivationSignalDetector.CreateConfiguration <br> ActivationSignalDetector.CreateConfigurationAsync <br> ActivationSignalDetector.GetConfiguration <br> ActivationSignalDetector.GetConfigurationAsync <br> ActivationSignalDetector.GetConfigurations <br> ActivationSignalDetector.GetConfigurationsAsync <br> ActivationSignalDetector.GetSupportedModelIdsForSignalId <br> ActivationSignalDetector.GetSupportedModelIdsForSignalIdAsync <br> ActivationSignalDetector.Kind <br> ActivationSignalDetector.ProviderId <br> ActivationSignalDetector.RemoveConfiguration <br> ActivationSignalDetector.RemoveConfigurationAsync <br> ActivationSignalDetector.SupportedModelDataTypes <br> ActivationSignalDetector.SupportedPowerStates <br> ActivationSignalDetector.SupportedTrainingDataFormats

#### [ActivationSignalDetectorKind](/uwp/api/windows.applicationmodel.conversationalagent.activationsignaldetectorkind)

ActivationSignalDetectorKind

#### [ActivationSignalDetectorPowerState](/uwp/api/windows.applicationmodel.conversationalagent.activationsignaldetectorpowerstate)

ActivationSignalDetectorPowerState

#### [ConversationalAgentDetectorManager](/uwp/api/windows.applicationmodel.conversationalagent.conversationalagentdetectormanager)

ConversationalAgentDetectorManager <br> ConversationalAgentDetectorManager.Default <br> ConversationalAgentDetectorManager.GetActivationSignalDetectors <br> ConversationalAgentDetectorManager.GetActivationSignalDetectorsAsync <br> ConversationalAgentDetectorManager.GetAllActivationSignalDetectors <br> ConversationalAgentDetectorManager.GetAllActivationSignalDetectorsAsync

#### [DetectionConfigurationAvailabilityChangeKind](/uwp/api/windows.applicationmodel.conversationalagent.detectionconfigurationavailabilitychangedeventargs)

DetectionConfigurationAvailabilityChangeKind <br> DetectionConfigurationAvailabilityChangedEventArgs

#### [DetectionConfigurationAvailabilityChangedEventArgs](/uwp/api/windows.applicationmodel.conversationalagent.detectionconfigurationavailabilitychangekind)

DetectionConfigurationAvailabilityChangedEventArgs.Kind

#### [DetectionConfigurationAvailabilityInfo](/uwp/api/windows.applicationmodel.conversationalagent.detectionconfigurationavailabilityinfo)

DetectionConfigurationAvailabilityInfo <br> DetectionConfigurationAvailabilityInfo.HasLockScreenPermission <br> DetectionConfigurationAvailabilityInfo.HasPermission <br> DetectionConfigurationAvailabilityInfo.HasSystemResourceAccess <br> DetectionConfigurationAvailabilityInfo.IsEnabled

#### [DetectionConfigurationTrainingStatus](/uwp/api/windows.applicationmodel.conversationalagent.detectionconfigurationtrainingstatus)

DetectionConfigurationTrainingStatus

### [Windows.ApplicationModel](/uwp/api/windows.applicationmodel)

#### [AppInfo](/uwp/api/windows.applicationmodel.appinfo)

AppInfo.Current <br> AppInfo.GetFromAppUserModelId <br> AppInfo.GetFromAppUserModelIdForUser <br> AppInfo.Package

#### [IAppInfoStatics](/uwp/api/windows.applicationmodel.iappinfostatics)

IAppInfoStatics <br> IAppInfoStatics.Current <br> IAppInfoStatics.GetFromAppUserModelId <br> IAppInfoStatics.GetFromAppUserModelIdForUser

#### [Package](/uwp/api/windows.applicationmodel.package)

Package.EffectiveExternalLocation <br> Package.EffectiveExternalPath <br> Package.EffectivePath <br> Package.GetAppListEntries <br> Package.GetLogoAsRandomAccessStreamReference <br> Package.InstalledPath <br> Package.IsStub <br> Package.MachineExternalLocation <br> Package.MachineExternalPath <br> Package.MutablePath <br> Package.UserExternalLocation <br> Package.UserExternalPath

## Windows.Devices

### [Windows.Devices.Bluetooth.Advertisement](/uwp/api/windows.devices.bluetooth.advertisement)

#### [BluetoothLEAdvertisementPublisher](/uwp/api/windows.devices.bluetooth.advertisement.bluetoothleadvertisementpublisher)

BluetoothLEAdvertisementPublisher.IncludeTransmitPowerLevel <br> BluetoothLEAdvertisementPublisher.IsAnonymous <br> BluetoothLEAdvertisementPublisher.PreferredTransmitPowerLevelInDBm <br> BluetoothLEAdvertisementPublisher.UseExtendedAdvertisement

#### [BluetoothLEAdvertisementPublisherStatusChangedEventArgs](/uwp/api/windows.devices.bluetooth.advertisement.bluetoothleadvertisementpublisherstatuschangedeventargs)

BluetoothLEAdvertisementPublisherStatusChangedEventArgs.SelectedTransmitPowerLevelInDBm

#### [BluetoothLEAdvertisementReceivedEventArgs](/uwp/api/windows.devices.bluetooth.advertisement.bluetoothleadvertisementreceivedeventargs)

BluetoothLEAdvertisementReceivedEventArgs.BluetoothAddressType <br> BluetoothLEAdvertisementReceivedEventArgs.IsAnonymous <br> BluetoothLEAdvertisementReceivedEventArgs.IsConnectable <br> BluetoothLEAdvertisementReceivedEventArgs.IsDirected <br> BluetoothLEAdvertisementReceivedEventArgs.IsScanResponse <br> BluetoothLEAdvertisementReceivedEventArgs.IsScannable <br> BluetoothLEAdvertisementReceivedEventArgs.TransmitPowerLevelInDBm

#### [BluetoothLEAdvertisementWatcher](/uwp/api/windows.devices.bluetooth.advertisement.bluetoothleadvertisementwatcher)

BluetoothLEAdvertisementWatcher.AllowExtendedAdvertisements

### [Windows.Devices.Bluetooth.Background](/uwp/api/windows.devices.bluetooth.background)

#### [BluetoothLEAdvertisementPublisherTriggerDetails](/uwp/api/windows.devices.bluetooth.background.bluetoothleadvertisementpublishertriggerdetails)

BluetoothLEAdvertisementPublisherTriggerDetails.SelectedTransmitPowerLevelInDBm

### [Windows.Devices.Bluetooth](/uwp/api/windows.devices.bluetooth)

#### [BluetoothAdapter](/uwp/api/windows.devices.bluetooth.bluetoothadapter)

BluetoothAdapter.IsExtendedAdvertisingSupported <br> BluetoothAdapter.MaxAdvertisementDataLength

### [Windows.Devices.Display](/uwp/api/windows.devices.display)

#### [DisplayMonitor](/uwp/api/windows.devices.display.displaymonitor)

DisplayMonitor.IsDolbyVisionSupportedInHdrMode

### [Windows.Devices.Input](/uwp/api/windows.devices.input)

#### [PenButtonListener](/uwp/api/windows.devices.input.penbuttonlistener)

PenButtonListener <br> PenButtonListener.GetDefault <br> PenButtonListener.IsSupported <br> PenButtonListener.IsSupportedChanged <br> PenButtonListener.TailButtonClicked <br> PenButtonListener.TailButtonDoubleClicked <br> PenButtonListener.TailButtonLongPressed

#### [PenDockListener](/uwp/api/windows.devices.input.pendockedeventargs)

PenDockListener

#### [PenDockListener](/uwp/api/windows.devices.input.pendocklistener)

PenDockListener.Docked <br> PenDockListener.GetDefault <br> PenDockListener.IsSupported <br> PenDockListener.IsSupportedChanged <br> PenDockListener.Undocked <br> PenDockedEventArgs

#### [PenTailButtonClickedEventArgs](/uwp/api/windows.devices.input.pentailbuttonclickedeventargs)

PenTailButtonClickedEventArgs

#### [PenTailButtonDoubleClickedEventArgs](/uwp/api/windows.devices.input.pentailbuttondoubleclickedeventargs)

PenTailButtonDoubleClickedEventArgs

#### [PenTailButtonLongPressedEventArgs](/uwp/api/windows.devices.input.pentailbuttonlongpressedeventargs)

PenTailButtonLongPressedEventArgs

#### [PenUndockedEventArgs](/uwp/api/windows.devices.input.penundockedeventargs)

PenUndockedEventArgs

### [Windows.Devices.Sensors](/uwp/api/windows.devices.sensors)

#### [Accelerometer](/uwp/api/windows.devices.sensors.accelerometer)

Accelerometer.ReportThreshold

#### [AccelerometerDataThreshold](/uwp/api/windows.devices.sensors.accelerometerdatathreshold)

AccelerometerDataThreshold <br> AccelerometerDataThreshold.XAxisInGForce <br> AccelerometerDataThreshold.YAxisInGForce <br> AccelerometerDataThreshold.ZAxisInGForce

#### [Barometer](/uwp/api/windows.devices.sensors.barometer)

Barometer.ReportThreshold

#### [BarometerDataThreshold](/uwp/api/windows.devices.sensors.barometerdatathreshold)

BarometerDataThreshold <br> BarometerDataThreshold.Hectopascals

#### [Compass](/uwp/api/windows.devices.sensors.compass)

Compass.ReportThreshold

#### [CompassDataThreshold](/uwp/api/windows.devices.sensors.compassdatathreshold)

CompassDataThreshold <br> CompassDataThreshold.Degrees

#### [Gyrometer](/uwp/api/windows.devices.sensors.gyrometer)

Gyrometer.ReportThreshold

#### [GyrometerDataThreshold](/uwp/api/windows.devices.sensors.gyrometerdatathreshold)

GyrometerDataThreshold <br> GyrometerDataThreshold.XAxisInDegreesPerSecond <br> GyrometerDataThreshold.YAxisInDegreesPerSecond <br> GyrometerDataThreshold.ZAxisInDegreesPerSecond

#### [Inclinometer](/uwp/api/windows.devices.sensors.inclinometer)

Inclinometer.ReportThreshold

#### [InclinometerDataThreshold](/uwp/api/windows.devices.sensors.inclinometerdatathreshold)

InclinometerDataThreshold <br> InclinometerDataThreshold.PitchInDegrees <br> InclinometerDataThreshold.RollInDegrees <br> InclinometerDataThreshold.YawInDegrees

#### [LightSensor](/uwp/api/windows.devices.sensors.lightsensor)

LightSensor.ReportThreshold

#### [LightSensorDataThreshold](/uwp/api/windows.devices.sensors.lightsensordatathreshold)

LightSensorDataThreshold <br> LightSensorDataThreshold.AbsoluteLux <br> LightSensorDataThreshold.LuxPercentage

#### [Magnetometer](/uwp/api/windows.devices.sensors.magnetometer)

Magnetometer.ReportThreshold

#### [MagnetometerDataThreshold](/uwp/api/windows.devices.sensors.magnetometerdatathreshold)

MagnetometerDataThreshold <br> MagnetometerDataThreshold.XAxisMicroteslas <br> MagnetometerDataThreshold.YAxisMicroteslas <br> MagnetometerDataThreshold.ZAxisMicroteslas

## Windows.Foundation

### [Windows.Foundation.Metadata](/uwp/api/windows.foundation.metadata)

#### [AttributeNameAttribute](/uwp/api/windows.foundation.metadata.attributenameattribute)

AttributeNameAttribute <br> AttributeNameAttribute.#ctor

#### [FastAbiAttribute](/uwp/api/windows.foundation.metadata.fastabiattribute)

FastAbiAttribute <br> FastAbiAttribute.#ctor <br> FastAbiAttribute.#ctor <br> FastAbiAttribute.#ctor

#### [NoExceptionAttribute](/uwp/api/windows.foundation.metadata.noexceptionattribute)

NoExceptionAttribute <br> NoExceptionAttribute.#ctor

## Windows.Globalization

### [Windows.Globalization](/uwp/api/windows.globalization)

#### [Language](/uwp/api/windows.globalization.language)

Language.AbbreviatedName <br> Language.GetMuiCompatibleLanguageListFromLanguageTags

## Windows.Graphics

### [Windows.Graphics.Capture](/uwp/api/windows.graphics.capture)

#### [GraphicsCaptureSession](/uwp/api/windows.graphics.capture.graphicscapturesession)

GraphicsCaptureSession.IsCursorCaptureEnabled

### [Windows.Graphics.Holographic](/uwp/api/windows.graphics.holographic)

#### [HolographicFrame](/uwp/api/windows.graphics.holographic.holographicframe)

HolographicFrame.Id

#### [HolographicFrameId](/uwp/api/windows.graphics.holographic.holographicframeid)

HolographicFrameId

#### [HolographicFrameRenderingReport](/uwp/api/windows.graphics.holographic.holographicframerenderingreport)

HolographicFrameRenderingReport <br> HolographicFrameRenderingReport.FrameId <br> HolographicFrameRenderingReport.MissedLatchCount <br> HolographicFrameRenderingReport.SystemRelativeActualGpuFinishTime <br> HolographicFrameRenderingReport.SystemRelativeFrameReadyTime <br> HolographicFrameRenderingReport.SystemRelativeTargetLatchTime

#### [HolographicFrameScanoutMonitor](/uwp/api/windows.graphics.holographic.holographicframescanoutmonitor)

HolographicFrameScanoutMonitor <br> HolographicFrameScanoutMonitor.Close <br> HolographicFrameScanoutMonitor.ReadReports

#### [HolographicFrameScanoutReport](/uwp/api/windows.graphics.holographic.holographicframescanoutreport)

HolographicFrameScanoutReport <br> HolographicFrameScanoutReport.MissedScanoutCount <br> HolographicFrameScanoutReport.RenderingReport <br> HolographicFrameScanoutReport.SystemRelativeLatchTime <br> HolographicFrameScanoutReport.SystemRelativePhotonTime <br> HolographicFrameScanoutReport.SystemRelativeScanoutStartTime

#### [HolographicSpace](/uwp/api/windows.graphics.holographic.holographicspace)

HolographicSpace.CreateFrameScanoutMonitor

## Windows.Management

### [Windows.Management.Deployment](/uwp/api/windows.management.deployment)

#### [AddPackageOptions](/uwp/api/windows.management.deployment.addpackageoptions)

AddPackageOptions <br> AddPackageOptions.#ctor <br> AddPackageOptions.AllowUnsigned <br> AddPackageOptions.DeferRegistrationWhenPackagesAreInUse <br> AddPackageOptions.DependencyPackageUris <br> AddPackageOptions.DeveloperMode <br> AddPackageOptions.ExternalLocationUri <br> AddPackageOptions.ForceAppShutdown <br> AddPackageOptions.ForceTargetAppShutdown <br> AddPackageOptions.ForceUpdateFromAnyVersion <br> AddPackageOptions.InstallAllResources <br> AddPackageOptions.OptionalPackageFamilyNames <br> AddPackageOptions.OptionalPackageUris <br> AddPackageOptions.RelatedPackageUris <br> AddPackageOptions.RequiredContentGroupOnly <br> AddPackageOptions.RetainFilesOnFailure <br> AddPackageOptions.StageInPlace <br> AddPackageOptions.StubPackageOption <br> AddPackageOptions.TargetVolume

#### [PackageManager](/uwp/api/windows.management.deployment.packagemanager)

PackageManager.AddPackageByUriAsync <br> PackageManager.FindProvisionedPackages <br> PackageManager.GetPackageStubPreference <br> PackageManager.RegisterPackageByUriAsync <br> PackageManager.RegisterPackagesByFullNameAsync <br> PackageManager.SetPackageStubPreference <br> PackageManager.StagePackageByUriAsync

#### [PackageStubPreference](/uwp/api/windows.management.deployment.packagestubpreference)

PackageStubPreference

#### [RegisterPackageOptions](/uwp/api/windows.management.deployment.registerpackageoptions)

RegisterPackageOptions <br> RegisterPackageOptions.#ctor <br> RegisterPackageOptions.AllowUnsigned <br> RegisterPackageOptions.AppDataVolume <br> RegisterPackageOptions.DeferRegistrationWhenPackagesAreInUse <br> RegisterPackageOptions.DependencyPackageUris <br> RegisterPackageOptions.DeveloperMode <br> RegisterPackageOptions.ExternalLocationUri <br> RegisterPackageOptions.ForceAppShutdown <br> RegisterPackageOptions.ForceTargetAppShutdown <br> RegisterPackageOptions.ForceUpdateFromAnyVersion <br> RegisterPackageOptions.InstallAllResources <br> RegisterPackageOptions.OptionalPackageFamilyNames <br> RegisterPackageOptions.StageInPlace

#### [StagePackageOptions](/uwp/api/windows.management.deployment.stagepackageoptions)

StagePackageOptions <br> StagePackageOptions.#ctor <br> StagePackageOptions.AllowUnsigned <br> StagePackageOptions.DependencyPackageUris <br> StagePackageOptions.DeveloperMode <br> StagePackageOptions.ExternalLocationUri <br> StagePackageOptions.ForceUpdateFromAnyVersion <br> StagePackageOptions.InstallAllResources <br> StagePackageOptions.OptionalPackageFamilyNames <br> StagePackageOptions.OptionalPackageUris <br> StagePackageOptions.RelatedPackageUris <br> StagePackageOptions.RequiredContentGroupOnly <br> StagePackageOptions.StageInPlace <br> StagePackageOptions.StubPackageOption <br> StagePackageOptions.TargetVolume

#### [StubPackageOption](/uwp/api/windows.management.deployment.stubpackageoption)

StubPackageOption

## Windows.Media

### [Windows.Media.Audio](/uwp/api/windows.media.audio)

#### [AudioPlaybackConnection](/uwp/api/windows.media.audio.audioplaybackconnection)

AudioPlaybackConnection <br> AudioPlaybackConnection.Close <br> AudioPlaybackConnection.DeviceId <br> AudioPlaybackConnection.GetDeviceSelector <br> AudioPlaybackConnection.Open <br> AudioPlaybackConnection.OpenAsync <br> AudioPlaybackConnection.Start <br> AudioPlaybackConnection.StartAsync <br> AudioPlaybackConnection.State <br> AudioPlaybackConnection.StateChanged <br> AudioPlaybackConnection.TryCreateFromId

#### [AudioPlaybackConnectionOpenResult](/uwp/api/windows.media.audio.audioplaybackconnectionopenresult)

AudioPlaybackConnectionOpenResult <br> AudioPlaybackConnectionOpenResult.ExtendedError <br> AudioPlaybackConnectionOpenResult.Status

#### [AudioPlaybackConnectionOpenResultStatus](/uwp/api/windows.media.audio.audioplaybackconnectionopenresultstatus)

AudioPlaybackConnectionOpenResultStatus

#### [AudioPlaybackConnectionState](/uwp/api/windows.media.audio.audioplaybackconnectionstate)

AudioPlaybackConnectionState

### [Windows.Media.Capture.Frames](/uwp/api/windows.media.capture.frames)

#### [MediaFrameSourceInfo](/uwp/api/windows.media.capture.frames.mediaframesourceinfo)

MediaFrameSourceInfo.GetRelativePanel

### [Windows.Media.Capture](/uwp/api/windows.media.capture)

#### [MediaCapture](/uwp/api/windows.media.capture.mediacapture)

MediaCapture.CreateRelativePanelWatcher

#### [MediaCaptureInitializationSettings](/uwp/api/windows.media.capture.mediacaptureinitializationsettings)

MediaCaptureInitializationSettings.DeviceUri <br> MediaCaptureInitializationSettings.DeviceUriPasswordCredential

#### [MediaCaptureRelativePanelWatcher](/uwp/api/windows.media.capture.mediacapturerelativepanelwatcher)

MediaCaptureRelativePanelWatcher <br> MediaCaptureRelativePanelWatcher.Changed <br> MediaCaptureRelativePanelWatcher.Close <br> MediaCaptureRelativePanelWatcher.RelativePanel <br> MediaCaptureRelativePanelWatcher.Start <br> MediaCaptureRelativePanelWatcher.Stop

### [Windows.Media.Devices](/uwp/api/windows.media.devices)

#### [PanelBasedOptimizationControl](/uwp/api/windows.media.devices.panelbasedoptimizationcontrol)

PanelBasedOptimizationControl <br> PanelBasedOptimizationControl.IsSupported <br> PanelBasedOptimizationControl.Panel

#### [VideoDeviceController](/uwp/api/windows.media.devices.videodevicecontroller)

VideoDeviceController.PanelBasedOptimizationControl

### [Windows.Media.MediaProperties](/uwp/api/windows.media.mediaproperties)

#### [MediaEncodingSubtypes](/uwp/api/windows.media.mediaproperties.mediaencodingsubtypes)

MediaEncodingSubtypes.Pgs <br> MediaEncodingSubtypes.Srt <br> MediaEncodingSubtypes.Ssa <br> MediaEncodingSubtypes.VobSub

#### [TimedMetadataEncodingProperties](/uwp/api/windows.media.mediaproperties.timedmetadataencodingproperties)

TimedMetadataEncodingProperties.CreatePgs <br> TimedMetadataEncodingProperties.CreateSrt <br> TimedMetadataEncodingProperties.CreateSsa <br> TimedMetadataEncodingProperties.CreateVobSub

## Windows.Networking

### [Windows.Networking.BackgroundTransfer](/uwp/api/windows.networking.backgroundtransfer)

#### [DownloadOperation](/uwp/api/windows.networking.backgroundtransfer.downloadoperation)

DownloadOperation.RemoveRequestHeader <br> DownloadOperation.SetRequestHeader

#### [UploadOperation](/uwp/api/windows.networking.backgroundtransfer.uploadoperation)

UploadOperation.RemoveRequestHeader <br> UploadOperation.SetRequestHeader

### [Windows.Networking.NetworkOperators](/uwp/api/windows.networking.networkoperators)

#### [NetworkOperatorTetheringAccessPointConfiguration](/uwp/api/windows.networking.networkoperators.networkoperatortetheringaccesspointconfiguration)

NetworkOperatorTetheringAccessPointConfiguration.Band <br> NetworkOperatorTetheringAccessPointConfiguration.IsBandSupported <br> NetworkOperatorTetheringAccessPointConfiguration.IsBandSupportedAsync

#### [NetworkOperatorTetheringManager](/uwp/api/windows.networking.networkoperators.networkoperatortetheringmanager)

NetworkOperatorTetheringManager.DisableNoConnectionsTimeout <br> NetworkOperatorTetheringManager.DisableNoConnectionsTimeoutAsync <br> NetworkOperatorTetheringManager.EnableNoConnectionsTimeout <br> NetworkOperatorTetheringManager.EnableNoConnectionsTimeoutAsync <br> NetworkOperatorTetheringManager.IsNoConnectionsTimeoutEnabled

#### [TetheringWiFiBand](/uwp/api/windows.networking.networkoperators.tetheringwifiband)

TetheringWiFiBand

### [Windows.Networking.PushNotifications](/uwp/api/windows.networking.pushnotifications)

#### [RawNotification](/uwp/api/windows.networking.pushnotifications.rawnotification)

RawNotification.ContentBytes

## Windows.Security

### [Windows.Security.Authentication.Web.Core](/uwp/api/windows.security.authentication.web.core)

#### [WebAccountMonitor](/uwp/api/windows.security.authentication.web.core.webaccountmonitor)

WebAccountMonitor.AccountPictureUpdated

### [Windows.Security.Isolation](/uwp/api/windows.security.isolation)

#### [IsolatedWindowsEnvironment](/uwp/api/windows.security.isolation.isolatedwindowsenvironment)

IsolatedWindowsEnvironment <br> IsolatedWindowsEnvironment.CreateAsync <br> IsolatedWindowsEnvironment.CreateAsync <br> IsolatedWindowsEnvironment.FindByOwnerId <br> IsolatedWindowsEnvironment.GetById <br> IsolatedWindowsEnvironment.Id <br> IsolatedWindowsEnvironment.LaunchFileWithUIAsync <br> IsolatedWindowsEnvironment.LaunchFileWithUIAsync <br> IsolatedWindowsEnvironment.RegisterMessageReceiver <br> IsolatedWindowsEnvironment.ShareFolderAsync <br> IsolatedWindowsEnvironment.ShareFolderAsync <br> IsolatedWindowsEnvironment.StartProcessSilentlyAsync <br> IsolatedWindowsEnvironment.StartProcessSilentlyAsync <br> IsolatedWindowsEnvironment.TerminateAsync <br> IsolatedWindowsEnvironment.TerminateAsync

#### [IsolatedWindowsEnvironment](/uwp/api/windows.security.isolation.isolatedwindowsenvironmentactivator)

IsolatedWindowsEnvironment.UnregisterMessageReceiver

#### [IsolatedWindowsEnvironmentActivator](/uwp/api/windows.security.isolation.isolatedwindowsenvironmentallowedclipboardformats)

IsolatedWindowsEnvironmentActivator

#### [IsolatedWindowsEnvironmentAllowedClipboardFormats](/uwp/api/windows.security.isolation.isolatedwindowsenvironmentavailableprinters)

IsolatedWindowsEnvironmentAllowedClipboardFormats

#### [IsolatedWindowsEnvironmentAvailablePrinters](/uwp/api/windows.security.isolation.isolatedwindowsenvironmentclipboardcopypastedirections)

IsolatedWindowsEnvironmentAvailablePrinters

#### [IsolatedWindowsEnvironmentClipboardCopyPasteDirections](/uwp/api/windows.security.isolation.isolatedwindowsenvironmentcreateprogress)

IsolatedWindowsEnvironmentClipboardCopyPasteDirections

#### [IsolatedWindowsEnvironmentCreateProgress](/uwp/api/windows.security.isolation.isolatedwindowsenvironmentcreateresult)

IsolatedWindowsEnvironmentCreateProgress <br> IsolatedWindowsEnvironmentCreateResult <br> IsolatedWindowsEnvironmentCreateResult.Environment <br> IsolatedWindowsEnvironmentCreateResult.ExtendedError

#### [IsolatedWindowsEnvironmentCreateResult](/uwp/api/windows.security.isolation.isolatedwindowsenvironmentcreatestatus)

IsolatedWindowsEnvironmentCreateResult.Status

#### [IsolatedWindowsEnvironmentCreateStatus](/uwp/api/windows.security.isolation.isolatedwindowsenvironmentfile)

IsolatedWindowsEnvironmentCreateStatus <br> IsolatedWindowsEnvironmentFile <br> IsolatedWindowsEnvironmentFile.Close <br> IsolatedWindowsEnvironmentFile.HostPath

#### [IsolatedWindowsEnvironmentFile](/uwp/api/windows.security.isolation.isolatedwindowsenvironmenthost)

IsolatedWindowsEnvironmentFile.Id <br> IsolatedWindowsEnvironmentHost <br> IsolatedWindowsEnvironmentHost.HostErrors

#### [IsolatedWindowsEnvironmentHost](/uwp/api/windows.security.isolation.isolatedwindowsenvironmenthosterror)

IsolatedWindowsEnvironmentHost.IsReady

#### [IsolatedWindowsEnvironmentHostError](/uwp/api/windows.security.isolation.isolatedwindowsenvironmentlaunchfileresult)

IsolatedWindowsEnvironmentHostError <br> IsolatedWindowsEnvironmentLaunchFileResult <br> IsolatedWindowsEnvironmentLaunchFileResult.ExtendedError <br> IsolatedWindowsEnvironmentLaunchFileResult.File

#### [IsolatedWindowsEnvironmentLaunchFileResult](/uwp/api/windows.security.isolation.isolatedwindowsenvironmentlaunchfilestatus)

IsolatedWindowsEnvironmentLaunchFileResult.Status

#### [IsolatedWindowsEnvironmentLaunchFileStatus](/uwp/api/windows.security.isolation.isolatedwindowsenvironmentoptions)

IsolatedWindowsEnvironmentLaunchFileStatus <br> IsolatedWindowsEnvironmentOptions <br> IsolatedWindowsEnvironmentOptions.#ctor <br> IsolatedWindowsEnvironmentOptions.AllowCameraAndMicrophoneAccess <br> IsolatedWindowsEnvironmentOptions.AllowGraphicsHardwareAcceleration <br> IsolatedWindowsEnvironmentOptions.AllowedClipboardFormats <br> IsolatedWindowsEnvironmentOptions.AvailablePrinters <br> IsolatedWindowsEnvironmentOptions.ClipboardCopyPasteDirections <br> IsolatedWindowsEnvironmentOptions.EnvironmentOwnerId <br> IsolatedWindowsEnvironmentOptions.PersistUserProfile <br> IsolatedWindowsEnvironmentOptions.ShareHostFolderForUntrustedItems <br> IsolatedWindowsEnvironmentOptions.SharedFolderNameInEnvironment

#### [IsolatedWindowsEnvironmentOptions](/uwp/api/windows.security.isolation.isolatedwindowsenvironmentownerregistration)

IsolatedWindowsEnvironmentOptions.SharedHostFolderPath <br> IsolatedWindowsEnvironmentOwnerRegistration <br> IsolatedWindowsEnvironmentOwnerRegistration.Register

#### [IsolatedWindowsEnvironmentOwnerRegistration](/uwp/api/windows.security.isolation.isolatedwindowsenvironmentownerregistrationdata)

IsolatedWindowsEnvironmentOwnerRegistration.Unregister <br> IsolatedWindowsEnvironmentOwnerRegistrationData <br> IsolatedWindowsEnvironmentOwnerRegistrationData.#ctor <br> IsolatedWindowsEnvironmentOwnerRegistrationData.ActivationFileExtensions <br> IsolatedWindowsEnvironmentOwnerRegistrationData.ProcessesRunnableAsSystem <br> IsolatedWindowsEnvironmentOwnerRegistrationData.ProcessesRunnableAsUser

#### [IsolatedWindowsEnvironmentOwnerRegistrationData](/uwp/api/windows.security.isolation.isolatedwindowsenvironmentownerregistrationresult)

IsolatedWindowsEnvironmentOwnerRegistrationData.ShareableFolders <br> IsolatedWindowsEnvironmentOwnerRegistrationResult <br> IsolatedWindowsEnvironmentOwnerRegistrationResult.ExtendedError

#### [IsolatedWindowsEnvironmentOwnerRegistrationResult](/uwp/api/windows.security.isolation.isolatedwindowsenvironmentownerregistrationstatus)

IsolatedWindowsEnvironmentOwnerRegistrationResult.Status

#### [IsolatedWindowsEnvironmentOwnerRegistrationStatus](/uwp/api/windows.security.isolation.isolatedwindowsenvironmentprocess)

IsolatedWindowsEnvironmentOwnerRegistrationStatus <br> IsolatedWindowsEnvironmentProcess <br> IsolatedWindowsEnvironmentProcess.ExitCode <br> IsolatedWindowsEnvironmentProcess.State <br> IsolatedWindowsEnvironmentProcess.WaitForExit <br> IsolatedWindowsEnvironmentProcess.WaitForExitAsync

#### [IsolatedWindowsEnvironmentProcess](/uwp/api/windows.security.isolation.isolatedwindowsenvironmentprocessstate)

IsolatedWindowsEnvironmentProcess.WaitForExitWithTimeout

#### [IsolatedWindowsEnvironmentProcessState](/uwp/api/windows.security.isolation.isolatedwindowsenvironmentprogressstate)

IsolatedWindowsEnvironmentProcessState

#### [IsolatedWindowsEnvironmentProgressState](/uwp/api/windows.security.isolation.isolatedwindowsenvironmentsharefolderrequestoptions)

IsolatedWindowsEnvironmentProgressState <br> IsolatedWindowsEnvironmentShareFolderRequestOptions <br> IsolatedWindowsEnvironmentShareFolderRequestOptions.#ctor

#### [IsolatedWindowsEnvironmentShareFolderRequestOptions](/uwp/api/windows.security.isolation.isolatedwindowsenvironmentsharefolderresult)

IsolatedWindowsEnvironmentShareFolderRequestOptions.AllowWrite <br> IsolatedWindowsEnvironmentShareFolderResult <br> IsolatedWindowsEnvironmentShareFolderResult.ExtendedError

#### [IsolatedWindowsEnvironmentShareFolderResult](/uwp/api/windows.security.isolation.isolatedwindowsenvironmentsharefolderstatus)

IsolatedWindowsEnvironmentShareFolderResult.Status

#### [IsolatedWindowsEnvironmentShareFolderStatus](/uwp/api/windows.security.isolation.isolatedwindowsenvironmentstartprocessresult)

IsolatedWindowsEnvironmentShareFolderStatus <br> IsolatedWindowsEnvironmentStartProcessResult <br> IsolatedWindowsEnvironmentStartProcessResult.ExtendedError <br> IsolatedWindowsEnvironmentStartProcessResult.Process

#### [IsolatedWindowsEnvironmentStartProcessResult](/uwp/api/windows.security.isolation.isolatedwindowsenvironmentstartprocessstatus)

IsolatedWindowsEnvironmentStartProcessResult.Status

#### [IsolatedWindowsEnvironmentStartProcessStatus](/uwp/api/windows.security.isolation.isolatedwindowsenvironmenttelemetryparameters)

IsolatedWindowsEnvironmentStartProcessStatus <br> IsolatedWindowsEnvironmentTelemetryParameters <br> IsolatedWindowsEnvironmentTelemetryParameters.#ctor

#### [IsolatedWindowsEnvironmentTelemetryParameters](/uwp/api/windows.security.isolation.isolatedwindowshostmessenger)

IsolatedWindowsEnvironmentTelemetryParameters.CorrelationId <br> IsolatedWindowsHostMessenger <br> IsolatedWindowsHostMessenger.GetFileId

#### [IsolatedWindowsHostMessenger](/uwp/api/windows.security.isolation.messagereceivedcallback)

IsolatedWindowsHostMessenger.PostMessageToReceiver

#### [MessageReceivedCallback](/uwp/api/windows.security.isolation.windows)

MessageReceivedCallback

## Windows.Storage

### [Windows.Storage.Provider](/uwp/api/windows.storage.provider)

#### [StorageProviderSyncRootManager](/uwp/api/windows.storage.provider.storageprovidersyncrootmanager)

StorageProviderSyncRootManager.IsSupported

### [Windows.Storage](/uwp/api/windows.storage)

#### [KnownFolders](/uwp/api/windows.storage.knownfolders)

KnownFolders.GetFolderAsync <br> KnownFolders.RequestAccessAsync <br> KnownFolders.RequestAccessForUserAsync

#### [KnownFoldersAccessStatus](/uwp/api/windows.storage.knownfoldersaccessstatus)

KnownFoldersAccessStatus

#### [StorageFile](/uwp/api/windows.storage.storagefile)

StorageFile.GetFileFromPathForUserAsync

#### [StorageFolder](/uwp/api/windows.storage.storagefolder)

StorageFolder.GetFolderFromPathForUserAsync

## Windows.System

### [Windows.System](/uwp/api/windows.system)

#### [UserChangedEventArgs](/uwp/api/windows.system.userchangedeventargs)

UserChangedEventArgs.ChangedPropertyKinds

#### [UserWatcherUpdateKind](/uwp/api/windows.system.userwatcherupdatekind)

UserWatcherUpdateKind

## Windows.UI

### [Windows.UI.Composition.Interactions](/uwp/api/windows.ui.composition.interactions)

#### [InteractionTracker](/uwp/api/windows.ui.composition.interactions.interactiontracker)

InteractionTracker.TryUpdatePosition

#### [InteractionTrackerPositionUpdateOption](/uwp/api/windows.ui.composition.interactions.interactiontrackerpositionupdateoption)

InteractionTrackerPositionUpdateOption

### [Windows.UI.Core.Preview.Communications](/uwp/api/windows.ui.core.preview.communications)

#### [PreviewMeetingInfoDisplayKind](/uwp/api/windows.ui.core.preview.communications.previewmeetinginfodisplaykind)

PreviewMeetingInfoDisplayKind

#### [PreviewSystemState](/uwp/api/windows.ui.core.preview.communications.previewsystemstate)

PreviewSystemState

#### [PreviewTeamCleanupRequestedEventArgs](/uwp/api/windows.ui.core.preview.communications.previewteamcleanuprequestedeventargs)

PreviewTeamCleanupRequestedEventArgs <br> PreviewTeamCleanupRequestedEventArgs.GetDeferral

#### [PreviewTeamCommandInvokedEventArgs](/uwp/api/windows.ui.core.preview.communications.previewteamcommandinvokedeventargs)

PreviewTeamCommandInvokedEventArgs </br> PreviewTeamCommandInvokedEventArgs.Command

#### [PreviewTeamDeviceCredentials](/uwp/api/windows.ui.core.preview.communications.previewteamdevicecredentials)

PreviewTeamDeviceCredentials <br> PreviewTeamDeviceCredentials.DomainUserName <br> PreviewTeamDeviceCredentials.Password <br> PreviewTeamDeviceCredentials.UserPrincipalName

#### [PreviewTeamEndMeetingKind](/uwp/api/windows.ui.core.preview.communications.previewteamendmeetingkind)

PreviewTeamEndMeetingKind

#### [PreviewTeamEndMeetingRequestedEventArgs](/uwp/api/windows.ui.core.preview.communications.previewteamendmeetingrequestedeventargs)

PreviewTeamEndMeetingRequestedEventArgs <br> PreviewTeamEndMeetingRequestedEventArgs.GetDeferral

#### [PreviewTeamJoinMeetingRequestedEventArgs](/uwp/api/windows.ui.core.preview.communications.previewteamjoinmeetingrequestedeventargs)

PreviewTeamJoinMeetingRequestedEventArgs <br> PreviewTeamJoinMeetingRequestedEventArgs.GetDeferral <br> PreviewTeamJoinMeetingRequestedEventArgs.MeetingUri

#### [PreviewTeamView](/uwp/api/windows.ui.core.preview.communications.previewteamview)

PreviewTeamView <br> PreviewTeamView.CleanupRequested <br> PreviewTeamView.CommandInvoked <br> PreviewTeamView.EndMeetingRequested <br> PreviewTeamView.EnterFullScreen <br> PreviewTeamView.GetForCurrentView <br> PreviewTeamView.IsFullScreen <br> PreviewTeamView.IsFullScreenChanged <br> PreviewTeamView.IsScreenSharing <br> PreviewTeamView.IsScreenSharingChanged <br> PreviewTeamView.JoinMeetingRequested <br> PreviewTeamView.JoinMeetingWithUri <br> PreviewTeamView.LeaveFullScreen <br> PreviewTeamView.MeetingInfoDisplayType <br> PreviewTeamView.MeetingUri <br> PreviewTeamView.NotifyMeetingEnded <br> PreviewTeamView.RequestForeground <br> PreviewTeamView.SetButtonLabel <br> PreviewTeamView.SetTitle <br> PreviewTeamView.SharingScreenBounds <br> PreviewTeamView.SharingScreenBoundsChanged <br> PreviewTeamView.StartSharingScreen <br> PreviewTeamView.StopSharingScreen <br> PreviewTeamView.SystemState

#### [PreviewTeamView](/uwp/api/windows.ui.core.preview.communications.previewteamview)

PreviewTeamView.SystemStateChanged

#### [PreviewTeamViewCommand](/uwp/api/windows.ui.core.preview.communications.previewteamviewcommand)

PreviewTeamViewCommand

### [Windows.UI.Input.Inking](/uwp/api/windows.ui.input.inking)

#### [InkModelerAttributes](/uwp/api/windows.ui.input.inking.inkmodelerattributes)

InkModelerAttributes.UseVelocityBasedPressure

### [Windows.UI.Input](/uwp/api/windows.ui.input)

#### [CrossSlidingEventArgs](/uwp/api/windows.ui.input.crossslidingeventargs)

CrossSlidingEventArgs.ContactCount

#### [DraggingEventArgs](/uwp/api/windows.ui.input.draggingeventargs)

DraggingEventArgs.ContactCount

#### [GestureRecognizer](/uwp/api/windows.ui.input.gesturerecognizer)

GestureRecognizer.HoldMaxContactCount <br> GestureRecognizer.HoldMinContactCount <br> GestureRecognizer.HoldRadius <br> GestureRecognizer.HoldStartDelay <br> GestureRecognizer.TapMaxContactCount <br> GestureRecognizer.TapMinContactCount <br> GestureRecognizer.TranslationMaxContactCount <br> GestureRecognizer.TranslationMinContactCount

#### [HoldingEventArgs](/uwp/api/windows.ui.input.holdingeventargs)

HoldingEventArgs.ContactCount <br> HoldingEventArgs.CurrentContactCount

#### [ManipulationCompletedEventArgs](/uwp/api/windows.ui.input.manipulationcompletedeventargs)

ManipulationCompletedEventArgs.ContactCount <br> ManipulationCompletedEventArgs.CurrentContactCount

#### [ManipulationInertiaStartingEventArgs](/uwp/api/windows.ui.input.manipulationinertiastartingeventargs)

ManipulationInertiaStartingEventArgs.ContactCount

#### [ManipulationStartedEventArgs](/uwp/api/windows.ui.input.manipulationstartedeventargs)

ManipulationStartedEventArgs.ContactCount

#### [ManipulationUpdatedEventArgs](/uwp/api/windows.ui.input.manipulationupdatedeventargs)

ManipulationUpdatedEventArgs.ContactCount <br> ManipulationUpdatedEventArgs.CurrentContactCount

#### [RightTappedEventArgs](/uwp/api/windows.ui.input.righttappedeventargs)

RightTappedEventArgs.ContactCount

#### [TappedEventArgs](/uwp/api/windows.ui.input.tappedeventargs)

TappedEventArgs.ContactCount 

### [Windows.UI.ViewManagement](/uwp/api/windows.ui.viewmanagement)

#### [UISettings](/uwp/api/windows.ui.viewmanagement.uisettings)

UISettings.AnimationsEnabledChanged <br> UISettings.MessageDurationChanged

#### [UISettingsAnimationsEnabledChangedEventArgs](/uwp/api/windows.ui.viewmanagement.uisettingsanimationsenabledchangedeventargs)

UISettingsAnimationsEnabledChangedEventArgs

#### [UISettingsMessageDurationChangedEventArgs](/uwp/api/windows.ui.viewmanagement.uisettingsmessagedurationchangedeventargs)

UISettingsMessageDurationChangedEventArgs