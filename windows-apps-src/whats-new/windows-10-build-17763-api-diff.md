---
title: Windows 10 Build 17763 API changes
description: Developers can use the following list to identify new or changed namespaces in Windows 10 build 17763
keywords: what's new, whats new, updates, Windows 10, newest, apis, 17763, october
ms.date: 10/02/2018
ms.topic: article
ms.localizationpriority: medium
ms.custom: RS5
---
# New APIs in Windows 10 build 17763

New and updated API namespaces have been made available to developers in Windows 10 build 17763 (Also known as the October 2018 Update or version 1809). Below is a full list of documentation published for namespaces added or modified in this release.

For information on APIs added in the previous public release, see [New APIs in the Windows 10 April Update](windows-10-build-17134-api-diff.md).

## Windows.AI

### [Windows.AI.MachineLearning](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning)

#### [ILearningModelFeatureDescriptor](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.ilearningmodelfeaturedescriptor)

ILearningModelFeatureDescriptor <br> ILearningModelFeatureDescriptor.Description <br> ILearningModelFeatureDescriptor.IsRequired <br> ILearningModelFeatureDescriptor.Kind <br> ILearningModelFeatureDescriptor.Name

#### [ILearningModelFeatureValue](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.ilearningmodelfeaturevalue)

ILearningModelFeatureValue <br> ILearningModelFeatureValue.Kind

#### [ILearningModelOperatorProvider](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.ilearningmodeloperatorprovider)

ILearningModelOperatorProvider

#### [ImageFeatureDescriptor](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.imagefeaturedescriptor)

ImageFeatureDescriptor <br> ImageFeatureDescriptor.BitmapAlphaMode <br> ImageFeatureDescriptor.BitmapPixelFormat <br> ImageFeatureDescriptor.Description <br> ImageFeatureDescriptor.Height <br> ImageFeatureDescriptor.IsRequired <br> ImageFeatureDescriptor.Kind <br> ImageFeatureDescriptor.Name <br> ImageFeatureDescriptor.Width

#### [ImageFeatureValue](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.imagefeaturevalue)

ImageFeatureValue <br> ImageFeatureValue.CreateFromVideoFrame <br> ImageFeatureValue.Kind <br> ImageFeatureValue.VideoFrame

#### [ITensor](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.itensor)

ITensor <br> ITensor.Shape <br> ITensor.TensorKind

#### [LearningModel](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.learningmodel)

LearningModel <br> LearningModel.Author <br> LearningModel.Close <br> LearningModel.Description <br> LearningModel.Domain <br> LearningModel.InputFeatures <br> LearningModel.LoadFromFilePath <br> LearningModel.LoadFromFilePath <br> LearningModel.LoadFromStorageFileAsync <br> LearningModel.LoadFromStorageFileAsync <br> LearningModel.LoadFromStream <br> LearningModel.LoadFromStream <br> LearningModel.LoadFromStreamAsync <br> LearningModel.LoadFromStreamAsync <br> LearningModel.Metadata <br> LearningModel.Name <br> LearningModel.OutputFeatures <br> LearningModel.Version

#### [LearningModelBinding](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.learningmodelbinding)

LearningModelBinding <br> LearningModelBinding.Bind <br> LearningModelBinding.Bind <br> LearningModelBinding.Clear <br> LearningModelBinding.First <br> LearningModelBinding.HasKey <br> LearningModelBinding.#ctor <br> LearningModelBinding.Lookup <br> LearningModelBinding.Size <br> LearningModelBinding.Split

#### [LearningModelDevice](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.learningmodeldevice)

LearningModelDevice <br> LearningModelDevice.AdapterId <br> LearningModelDevice.CreateFromDirect3D11Device <br> LearningModelDevice.Direct3D11Device <br> LearningModelDevice.#ctor

#### [LearningModelDeviceKind](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.learningmodeldevicekind)

LearningModelDeviceKind

#### [LearningModelEvaluationResult](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.learningmodelevaluationresult)

LearningModelEvaluationResult <br> LearningModelEvaluationResult.CorrelationId <br> LearningModelEvaluationResult.ErrorStatus <br> LearningModelEvaluationResult.Outputs <br> LearningModelEvaluationResult.Succeeded

#### [LearningModelFeatureKind](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.learningmodelfeaturekind)

LearningModelFeatureKind

#### [LearningModelSession](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.learningmodelsession)

LearningModelSession <br> LearningModelSession.Close <br> LearningModelSession.Device <br> LearningModelSession.Evaluate <br> LearningModelSession.EvaluateAsync <br> LearningModelSession.EvaluateFeatures <br> LearningModelSession.EvaluateFeaturesAsync <br> LearningModelSession.EvaluationProperties <br> LearningModelSession.#ctor <br> LearningModelSession.#ctor <br> LearningModelSession.Model

#### [MapFeatureDescriptor](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.mapfeaturedescriptor)

MapFeatureDescriptor <br> MapFeatureDescriptor.Description <br> MapFeatureDescriptor.IsRequired <br> MapFeatureDescriptor.KeyKind <br> MapFeatureDescriptor.Kind <br> MapFeatureDescriptor.Name <br> MapFeatureDescriptor.ValueDescriptor

#### [SequenceFeatureDescriptor](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.sequencefeaturedescriptor)

SequenceFeatureDescriptor <br> SequenceFeatureDescriptor.Description <br> SequenceFeatureDescriptor.ElementDescriptor <br> SequenceFeatureDescriptor.IsRequired <br> SequenceFeatureDescriptor.Kind <br> SequenceFeatureDescriptor.Name

#### [TensorBoolean](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.tensorboolean)

TensorBoolean <br> TensorBoolean.Create <br> TensorBoolean.Create <br> TensorBoolean.CreateFromArray <br> TensorBoolean.CreateFromIterable <br> TensorBoolean.GetAsVectorView <br> TensorBoolean.Kind <br> TensorBoolean.Shape <br> TensorBoolean.TensorKind

#### [TensorDouble](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.tensordouble)

TensorDouble <br> TensorDouble.Create <br> TensorDouble.Create <br> TensorDouble.CreateFromArray <br> TensorDouble.CreateFromIterable <br> TensorDouble.GetAsVectorView <br> TensorDouble.Kind <br> TensorDouble.Shape <br> TensorDouble.TensorKind

#### [TensorFeatureDescriptor](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.tensorfeaturedescriptor)

TensorFeatureDescriptor <br> TensorFeatureDescriptor.Description <br> TensorFeatureDescriptor.IsRequired <br> TensorFeatureDescriptor.Kind <br> TensorFeatureDescriptor.Name <br> TensorFeatureDescriptor.Shape <br> TensorFeatureDescriptor.TensorKind

#### [TensorFloat](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.tensorfloat)

TensorFloat <br> TensorFloat.Create <br> TensorFloat.Create <br> TensorFloat.CreateFromArray <br> TensorFloat.CreateFromIterable <br> TensorFloat.GetAsVectorView <br> TensorFloat.Kind <br> TensorFloat.Shape <br> TensorFloat.TensorKind

#### [TensorFloat16Bit](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.tensorfloat16bit)

TensorFloat16Bit <br> TensorFloat16Bit.Create <br> TensorFloat16Bit.Create <br> TensorFloat16Bit.CreateFromArray <br> TensorFloat16Bit.CreateFromIterable <br> TensorFloat16Bit.GetAsVectorView <br> TensorFloat16Bit.Kind <br> TensorFloat16Bit.Shape <br> TensorFloat16Bit.TensorKind

#### [TensorInt16Bit](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.tensorint16bit)

TensorInt16Bit <br> TensorInt16Bit.Create <br> TensorInt16Bit.Create <br> TensorInt16Bit.CreateFromArray <br> TensorInt16Bit.CreateFromIterable <br> TensorInt16Bit.GetAsVectorView <br> TensorInt16Bit.Kind <br> TensorInt16Bit.Shape <br> TensorInt16Bit.TensorKind

#### [TensorInt32Bit](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.tensorint32bit)

TensorInt32Bit <br> TensorInt32Bit.Create <br> TensorInt32Bit.Create <br> TensorInt32Bit.CreateFromArray <br> TensorInt32Bit.CreateFromIterable <br> TensorInt32Bit.GetAsVectorView <br> TensorInt32Bit.Kind <br> TensorInt32Bit.Shape <br> TensorInt32Bit.TensorKind

#### [TensorInt64Bit](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.tensorint64bit)

TensorInt64Bit <br> TensorInt64Bit.Create <br> TensorInt64Bit.Create <br> TensorInt64Bit.CreateFromArray <br> TensorInt64Bit.CreateFromIterable <br> TensorInt64Bit.GetAsVectorView <br> TensorInt64Bit.Kind <br> TensorInt64Bit.Shape <br> TensorInt64Bit.TensorKind

#### [TensorInt8Bit](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.tensorint8bit)

TensorInt8Bit <br> TensorInt8Bit.Create <br> TensorInt8Bit.Create <br> TensorInt8Bit.CreateFromArray <br> TensorInt8Bit.CreateFromIterable <br> TensorInt8Bit.GetAsVectorView <br> TensorInt8Bit.Kind <br> TensorInt8Bit.Shape <br> TensorInt8Bit.TensorKind

#### [TensorKind](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.tensorkind)

TensorKind

#### [TensorString](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.tensorstring)

TensorString <br> TensorString.Create <br> TensorString.Create <br> TensorString.CreateFromArray <br> TensorString.CreateFromIterable <br> TensorString.GetAsVectorView <br> TensorString.Kind <br> TensorString.Shape <br> TensorString.TensorKind

#### [TensorUInt16Bit](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.tensoruint16bit)

TensorUInt16Bit <br> TensorUInt16Bit.Create <br> TensorUInt16Bit.Create <br> TensorUInt16Bit.CreateFromArray <br> TensorUInt16Bit.CreateFromIterable <br> TensorUInt16Bit.GetAsVectorView <br> TensorUInt16Bit.Kind <br> TensorUInt16Bit.Shape <br> TensorUInt16Bit.TensorKind

#### [TensorUInt32Bit](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.tensoruint32bit)

TensorUInt32Bit <br> TensorUInt32Bit.Create <br> TensorUInt32Bit.Create <br> TensorUInt32Bit.CreateFromArray <br> TensorUInt32Bit.CreateFromIterable <br> TensorUInt32Bit.GetAsVectorView <br> TensorUInt32Bit.Kind <br> TensorUInt32Bit.Shape <br> TensorUInt32Bit.TensorKind

#### [TensorUInt64Bit](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.tensoruint64bit)

TensorUInt64Bit <br> TensorUInt64Bit.Create <br> TensorUInt64Bit.Create <br> TensorUInt64Bit.CreateFromArray <br> TensorUInt64Bit.CreateFromIterable <br> TensorUInt64Bit.GetAsVectorView <br> TensorUInt64Bit.Kind <br> TensorUInt64Bit.Shape <br> TensorUInt64Bit.TensorKind

#### [TensorUInt8Bit](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.tensoruint8bit)

TensorUInt8Bit <br> TensorUInt8Bit.Create <br> TensorUInt8Bit.Create <br> TensorUInt8Bit.CreateFromArray <br> TensorUInt8Bit.CreateFromIterable <br> TensorUInt8Bit.GetAsVectorView <br> TensorUInt8Bit.Kind <br> TensorUInt8Bit.Shape <br> TensorUInt8Bit.TensorKind

## Windows.ApplicationModel

### [Windows.ApplicationModel.Calls](https://docs.microsoft.com/uwp/api/windows.applicationmodel.calls)

#### [VoipCallCoordinator](https://docs.microsoft.com/uwp/api/windows.applicationmodel.calls.voipcallcoordinator)

VoipCallCoordinator.ReserveCallResourcesAsync

### [Windows.ApplicationModel.Chat](https://docs.microsoft.com/uwp/api/windows.applicationmodel.chat)

#### [ChatCapabilitiesManager](https://docs.microsoft.com/uwp/api/windows.applicationmodel.chat.chatcapabilitiesmanager)

ChatCapabilitiesManager.GetCachedCapabilitiesAsync <br> ChatCapabilitiesManager.GetCapabilitiesFromNetworkAsync

#### [RcsManager](https://docs.microsoft.com/uwp/api/windows.applicationmodel.chat.rcsmanager)

RcsManager.TransportListChanged

### [Windows.ApplicationModel.DataTransfer](https://docs.microsoft.com/uwp/api/windows.applicationmodel.datatransfer)

#### [Clipboard](https://docs.microsoft.com/uwp/api/windows.applicationmodel.datatransfer.clipboard)

Clipboard.ClearHistory <br> Clipboard.DeleteItemFromHistory <br> Clipboard.GetHistoryItemsAsync <br> Clipboard.HistoryChanged <br> Clipboard.HistoryEnabledChanged <br> Clipboard.IsHistoryEnabled <br> Clipboard.IsRoamingEnabled <br> Clipboard.RoamingEnabledChanged <br> Clipboard.SetContentWithOptions <br> Clipboard.SetHistoryItemAsContent

#### [ClipboardContentOptions](https://docs.microsoft.com/uwp/api/windows.applicationmodel.datatransfer.clipboardcontentoptions)

ClipboardContentOptions <br> ClipboardContentOptions.#ctor <br> ClipboardContentOptions.HistoryFormats <br> ClipboardContentOptions.IsAllowedInHistory <br> ClipboardContentOptions.IsRoamable <br> ClipboardContentOptions.RoamingFormats

#### [ClipboardHistoryChangedEventArgs](https://docs.microsoft.com/uwp/api/windows.applicationmodel.datatransfer.clipboardhistorychangedeventargs)

ClipboardHistoryChangedEventArgs

#### [ClipboardHistoryItem](https://docs.microsoft.com/uwp/api/windows.applicationmodel.datatransfer.clipboardhistoryitem)

ClipboardHistoryItem <br> ClipboardHistoryItem.Content <br> ClipboardHistoryItem.Id <br> ClipboardHistoryItem.Timestamp

#### [ClipboardHistoryItemsResult](https://docs.microsoft.com/uwp/api/windows.applicationmodel.datatransfer.clipboardhistoryitemsresult)

ClipboardHistoryItemsResult <br> ClipboardHistoryItemsResult.Items <br> ClipboardHistoryItemsResult.Status

#### [ClipboardHistoryItemsResultStatus](https://docs.microsoft.com/uwp/api/windows.applicationmodel.datatransfer.clipboardhistoryitemsresultstatus)

ClipboardHistoryItemsResultStatus

#### [DataPackagePropertySetView](https://docs.microsoft.com/uwp/api/windows.applicationmodel.datatransfer.datapackagepropertysetview)

DataPackagePropertySetView.IsFromRoamingClipboard

#### [SetHistoryItemAsContentStatus](https://docs.microsoft.com/uwp/api/windows.applicationmodel.datatransfer.sethistoryitemascontentstatus)

SetHistoryItemAsContentStatus

### [Windows.ApplicationModel.Store.Preview.InstallControl](https://docs.microsoft.com/uwp/api/windows.applicationmodel.store.preview.installcontrol)

#### [AppInstallationToastNotificationMode](https://docs.microsoft.com/uwp/api/windows.applicationmodel.store.preview.installcontrol.appinstallationtoastnotificationmode)

AppInstallationToastNotificationMode

#### [AppInstallItem](https://docs.microsoft.com/uwp/api/windows.applicationmodel.store.preview.installcontrol.appinstallitem)

AppInstallItem.CompletedInstallToastNotificationMode <br> AppInstallItem.InstallInProgressToastNotificationMode <br> AppInstallItem.PinToDesktopAfterInstall <br> AppInstallItem.PinToStartAfterInstall <br> AppInstallItem.PinToTaskbarAfterInstall

#### [AppInstallManager](https://docs.microsoft.com/uwp/api/windows.applicationmodel.store.preview.installcontrol.appinstallmanager)

AppInstallManager.CanInstallForAllUsers

#### [AppInstallOptions](https://docs.microsoft.com/uwp/api/windows.applicationmodel.store.preview.installcontrol.appinstalloptions)

AppInstallOptions.CampaignId <br> AppInstallOptions.CompletedInstallToastNotificationMode <br> AppInstallOptions.ExtendedCampaignId <br> AppInstallOptions.InstallForAllUsers <br> AppInstallOptions.InstallInProgressToastNotificationMode <br> AppInstallOptions.PinToDesktopAfterInstall <br> AppInstallOptions.PinToStartAfterInstall <br> AppInstallOptions.PinToTaskbarAfterInstall <br> AppInstallOptions.StageButDoNotInstall

#### [AppUpdateOptions](https://docs.microsoft.com/uwp/api/windows.applicationmodel.store.preview.installcontrol.appupdateoptions)

AppUpdateOptions.AutomaticallyDownloadAndInstallUpdateIfFound

### [Windows.ApplicationModel.Store.Preview](https://docs.microsoft.com/uwp/api/windows.applicationmodel.store.preview)

#### [DeliveryOptimizationDownloadMode](https://docs.microsoft.com/uwp/api/windows.applicationmodel.store.preview.deliveryoptimizationdownloadmode)

DeliveryOptimizationDownloadMode

#### [DeliveryOptimizationDownloadModeSource](https://docs.microsoft.com/uwp/api/windows.applicationmodel.store.preview.deliveryoptimizationdownloadmodesource)

DeliveryOptimizationDownloadModeSource

#### [DeliveryOptimizationSettings](https://docs.microsoft.com/uwp/api/windows.applicationmodel.store.preview.deliveryoptimizationsettings)

DeliveryOptimizationSettings <br> DeliveryOptimizationSettings.DownloadMode <br> DeliveryOptimizationSettings.DownloadModeSource <br> DeliveryOptimizationSettings.GetCurrentSettings

#### [StoreConfiguration](https://docs.microsoft.com/uwp/api/windows.applicationmodel.store.preview.storeconfiguration)

StoreConfiguration.IsPinToDesktopSupported <br> StoreConfiguration.IsPinToStartSupported <br> StoreConfiguration.IsPinToTaskbarSupported <br> StoreConfiguration.PinToDesktop <br> StoreConfiguration.PinToDesktopForUser

### [Windows.ApplicationModel.UserActivities](https://docs.microsoft.com/uwp/api/windows.applicationmodel.useractivities)

#### [UserActivity](https://docs.microsoft.com/uwp/api/windows.applicationmodel.useractivities.useractivity)

UserActivity.IsRoamable

### [Windows.ApplicationModel](https://docs.microsoft.com/uwp/api/windows.applicationmodel)

#### [AppInstallerInfo](https://docs.microsoft.com/uwp/api/windows.applicationmodel.appinstallerinfo)

AppInstallerInfo <br> AppInstallerInfo.Uri

#### [LimitedAccessFeatureRequestResult](https://docs.microsoft.com/uwp/api/windows.applicationmodel.limitedaccessfeaturerequestresult)

LimitedAccessFeatureRequestResult <br> LimitedAccessFeatureRequestResult.EstimatedRemovalDate <br> LimitedAccessFeatureRequestResult.FeatureId <br> LimitedAccessFeatureRequestResult.Status

#### [LimitedAccessFeatures](https://docs.microsoft.com/uwp/api/windows.applicationmodel.limitedaccessfeatures)

LimitedAccessFeatures <br> LimitedAccessFeatures.TryUnlockFeature

#### [LimitedAccessFeatureStatus](https://docs.microsoft.com/uwp/api/windows.applicationmodel.limitedaccessfeaturestatus)

LimitedAccessFeatureStatus

#### [Package](https://docs.microsoft.com/uwp/api/windows.applicationmodel.package)

Package.CheckUpdateAvailabilityAsync <br> Package.GetAppInstallerInfo

#### [PackageUpdateAvailability](https://docs.microsoft.com/uwp/api/windows.applicationmodel.packageupdateavailability)

PackageUpdateAvailability

#### [PackageUpdateAvailabilityResult](https://docs.microsoft.com/uwp/api/windows.applicationmodel.packageupdateavailabilityresult)

PackageUpdateAvailabilityResult <br> PackageUpdateAvailabilityResult.Availability <br> PackageUpdateAvailabilityResult.ExtendedError

## Windows.Data

### [Windows.Data.Text](https://docs.microsoft.com/uwp/api/windows.data.text)

#### [TextPredictionGenerator](https://docs.microsoft.com/uwp/api/windows.data.text.textpredictiongenerator)

TextPredictionGenerator.GetCandidatesAsync <br> TextPredictionGenerator.GetNextWordCandidatesAsync <br> TextPredictionGenerator.InputScope

#### [TextPredictionOptions](https://docs.microsoft.com/uwp/api/windows.data.text.textpredictionoptions)

TextPredictionOptions

## Windows.Devices

### [Windows.Devices.Display.Core](https://docs.microsoft.com/uwp/api/windows.devices.display.core)

#### [DisplayAdapter](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displayadapter)

DisplayAdapter <br> DisplayAdapter.DeviceInterfacePath <br> DisplayAdapter.FromId <br> DisplayAdapter.Id <br> DisplayAdapter.PciDeviceId <br> DisplayAdapter.PciRevision <br> DisplayAdapter.PciSubSystemId <br> DisplayAdapter.PciVendorId <br> DisplayAdapter.Properties <br> DisplayAdapter.SourceCount

#### [DisplayBitsPerChannel](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaybitsperchannel)

DisplayBitsPerChannel

#### [DisplayDevice](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaydevice)

DisplayDevice <br> DisplayDevice.CreatePeriodicFence <br> DisplayDevice.CreatePrimary <br> DisplayDevice.CreateScanoutSource <br> DisplayDevice.CreateSimpleScanout <br> DisplayDevice.CreateTaskPool <br> DisplayDevice.IsCapabilitySupported <br> DisplayDevice.WaitForVBlank

#### [DisplayDeviceCapability](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaydevicecapability)

DisplayDeviceCapability

#### [DisplayFence](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displayfence)

DisplayFence

#### [DisplayManager](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaymanager)

DisplayManager <br> DisplayManager.Changed <br> DisplayManager.Close <br> DisplayManager.Create <br> DisplayManager.CreateDisplayDevice <br> DisplayManager.Disabled <br> DisplayManager.Enabled <br> DisplayManager.GetCurrentAdapters <br> DisplayManager.GetCurrentTargets <br> DisplayManager.PathsFailedOrInvalidated <br> DisplayManager.ReleaseTarget <br> DisplayManager.Start <br> DisplayManager.Stop <br> DisplayManager.TryAcquireTarget <br> DisplayManager.TryAcquireTargetsAndCreateEmptyState <br> DisplayManager.TryAcquireTargetsAndCreateSubstate <br> DisplayManager.TryAcquireTargetsAndReadCurrentState <br> DisplayManager.TryReadCurrentStateForAllTargets

#### [DisplayManagerChangedEventArgs](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaymanagerchangedeventargs)

DisplayManagerChangedEventArgs <br> DisplayManagerChangedEventArgs.GetDeferral <br> DisplayManagerChangedEventArgs.Handled

#### [DisplayManagerDisabledEventArgs](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaymanagerdisabledeventargs)

DisplayManagerDisabledEventArgs <br> DisplayManagerDisabledEventArgs.GetDeferral <br> DisplayManagerDisabledEventArgs.Handled

#### [DisplayManagerEnabledEventArgs](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaymanagerenabledeventargs)

DisplayManagerEnabledEventArgs <br> DisplayManagerEnabledEventArgs.GetDeferral <br> DisplayManagerEnabledEventArgs.Handled

#### [DisplayManagerOptions](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaymanageroptions)

DisplayManagerOptions

#### [DisplayManagerPathsFailedOrInvalidatedEventArgs](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaymanagerpathsfailedorinvalidatedeventargs)

DisplayManagerPathsFailedOrInvalidatedEventArgs <br> DisplayManagerPathsFailedOrInvalidatedEventArgs.GetDeferral <br> DisplayManagerPathsFailedOrInvalidatedEventArgs.Handled

#### [DisplayManagerResult](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaymanagerresult)

DisplayManagerResult

#### [DisplayManagerResultWithState](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaymanagerresultwithstate)

DisplayManagerResultWithState <br> DisplayManagerResultWithState.ErrorCode <br> DisplayManagerResultWithState.ExtendedErrorCode <br> DisplayManagerResultWithState.State

#### [DisplayModeInfo](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaymodeinfo)

DisplayModeInfo <br> DisplayModeInfo.GetWireFormatSupportedBitsPerChannel <br> DisplayModeInfo.IsInterlaced <br> DisplayModeInfo.IsStereo <br> DisplayModeInfo.IsWireFormatSupported <br> DisplayModeInfo.PresentationRate <br> DisplayModeInfo.Properties <br> DisplayModeInfo.SourcePixelFormat <br> DisplayModeInfo.SourceResolution <br> DisplayModeInfo.TargetResolution

#### [DisplayModeQueryOptions](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaymodequeryoptions)

DisplayModeQueryOptions

#### [DisplayPath](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaypath)

DisplayPath <br> DisplayPath.ApplyPropertiesFromMode <br> DisplayPath.FindModes <br> DisplayPath.IsInterlaced <br> DisplayPath.IsStereo <br> DisplayPath.PresentationRate <br> DisplayPath.Properties <br> DisplayPath.Rotation <br> DisplayPath.Scaling <br> DisplayPath.SourcePixelFormat <br> DisplayPath.SourceResolution <br> DisplayPath.Status <br> DisplayPath.Target <br> DisplayPath.TargetResolution <br> DisplayPath.View <br> DisplayPath.WireFormat

#### [DisplayPathScaling](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaypathscaling)

DisplayPathScaling

#### [DisplayPathStatus](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaypathstatus)

DisplayPathStatus

#### [DisplayPresentationRate](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaypresentationrate)

DisplayPresentationRate

#### [DisplayPrimaryDescription](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displayprimarydescription)

DisplayPrimaryDescription <br> DisplayPrimaryDescription.ColorSpace <br> DisplayPrimaryDescription.CreateWithProperties <br> DisplayPrimaryDescription.#ctor <br> DisplayPrimaryDescription.Format <br> DisplayPrimaryDescription.Height <br> DisplayPrimaryDescription.IsStereo <br> DisplayPrimaryDescription.MultisampleDescription <br> DisplayPrimaryDescription.Properties <br> DisplayPrimaryDescription.Width

#### [DisplayRotation](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displayrotation)

DisplayRotation

#### [DisplayScanout](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displayscanout)

DisplayScanout

#### [DisplaySource](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaysource)

DisplaySource <br> DisplaySource.AdapterId <br> DisplaySource.GetMetadata <br> DisplaySource.SourceId

#### [DisplayState](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaystate)

DisplayState <br> DisplayState.CanConnectTargetToView <br> DisplayState.Clone <br> DisplayState.ConnectTarget <br> DisplayState.ConnectTarget <br> DisplayState.DisconnectTarget <br> DisplayState.GetPathForTarget <br> DisplayState.GetViewForTarget <br> DisplayState.IsReadOnly <br> DisplayState.IsStale <br> DisplayState.Properties <br> DisplayState.Targets <br> DisplayState.TryApply <br> DisplayState.TryFunctionalize <br> DisplayState.Views

#### [DisplayStateApplyOptions](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaystateapplyoptions)

DisplayStateApplyOptions

#### [DisplayStateFunctionalizeOptions](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaystatefunctionalizeoptions)

DisplayStateFunctionalizeOptions

#### [DisplayStateOperationResult](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaystateoperationresult)

DisplayStateOperationResult <br> DisplayStateOperationResult.ExtendedErrorCode <br> DisplayStateOperationResult.Status

#### [DisplayStateOperationStatus](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaystateoperationstatus)

DisplayStateOperationStatus

#### [DisplaySurface](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaysurface)

DisplaySurface

#### [DisplayTarget](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaytarget)

DisplayTarget <br> DisplayTarget.Adapter <br> DisplayTarget.AdapterRelativeId <br> DisplayTarget.DeviceInterfacePath <br> DisplayTarget.IsConnected <br> DisplayTarget.IsEqual <br> DisplayTarget.IsSame <br> DisplayTarget.IsStale <br> DisplayTarget.IsVirtualModeEnabled <br> DisplayTarget.IsVirtualTopologyEnabled <br> DisplayTarget.MonitorPersistence <br> DisplayTarget.Properties <br> DisplayTarget.StableMonitorId <br> DisplayTarget.TryGetMonitor <br> DisplayTarget.UsageKind

#### [DisplayTargetPersistence](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaytargetpersistence)

DisplayTargetPersistence

#### [DisplayTask](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaytask)

DisplayTask <br> DisplayTask.SetScanout <br> DisplayTask.SetWait

#### [DisplayTaskPool](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaytaskpool)

DisplayTaskPool <br> DisplayTaskPool.CreateTask <br> DisplayTaskPool.ExecuteTask

#### [DisplayTaskSignalKind](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaytasksignalkind)

DisplayTaskSignalKind

#### [DisplayView](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displayview)

DisplayView <br> DisplayView.ContentResolution <br> DisplayView.Paths <br> DisplayView.Properties <br> DisplayView.SetPrimaryPath

#### [DisplayWireFormat](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaywireformat)

DisplayWireFormat <br> DisplayWireFormat.BitsPerChannel <br> DisplayWireFormat.ColorSpace <br> DisplayWireFormat.CreateWithProperties <br> DisplayWireFormat.#ctor <br> DisplayWireFormat.Eotf <br> DisplayWireFormat.HdrMetadata <br> DisplayWireFormat.PixelEncoding <br> DisplayWireFormat.Properties

#### [DisplayWireFormatColorSpace](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaywireformatcolorspace)

DisplayWireFormatColorSpace

#### [DisplayWireFormatEotf](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaywireformateotf)

DisplayWireFormatEotf

#### [DisplayWireFormatHdrMetadata](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaywireformathdrmetadata)

DisplayWireFormatHdrMetadata

#### [DisplayWireFormatPixelEncoding](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaywireformatpixelencoding)

DisplayWireFormatPixelEncoding

### [Windows.Devices.Enumeration](https://docs.microsoft.com/uwp/api/windows.devices.enumeration)

#### [DeviceInformationPairing](https://docs.microsoft.com/uwp/api/windows.devices.enumeration.deviceinformationpairing)

DeviceInformationPairing.TryRegisterForAllInboundPairingRequestsWithProtectionLevel

### [Windows.Devices.Lights.Effects](https://docs.microsoft.com/uwp/api/windows.devices.lights.effects)

#### [ILampArrayEffect](https://docs.microsoft.com/uwp/api/windows.devices.lights.effects.ilamparrayeffect)

ILampArrayEffect <br> ILampArrayEffect.ZIndex

#### [LampArrayBitmapEffect](https://docs.microsoft.com/uwp/api/windows.devices.lights.effects.lamparraybitmapeffect)

LampArrayBitmapEffect <br> LampArrayBitmapEffect.BitmapRequested <br> LampArrayBitmapEffect.Duration <br> LampArrayBitmapEffect.#ctor <br> LampArrayBitmapEffect.StartDelay <br> LampArrayBitmapEffect.SuggestedBitmapSize <br> LampArrayBitmapEffect.UpdateInterval <br> LampArrayBitmapEffect.ZIndex

#### [LampArrayBitmapRequestedEventArgs](https://docs.microsoft.com/uwp/api/windows.devices.lights.effects.lamparraybitmaprequestedeventargs)

LampArrayBitmapRequestedEventArgs <br> LampArrayBitmapRequestedEventArgs.SinceStarted <br> LampArrayBitmapRequestedEventArgs.UpdateBitmap

#### [LampArrayBlinkEffect](https://docs.microsoft.com/uwp/api/windows.devices.lights.effects.lamparrayblinkeffect)

LampArrayBlinkEffect <br> LampArrayBlinkEffect.AttackDuration <br> LampArrayBlinkEffect.Color <br> LampArrayBlinkEffect.DecayDuration <br> LampArrayBlinkEffect.#ctor <br> LampArrayBlinkEffect.Occurrences <br> LampArrayBlinkEffect.RepetitionDelay <br> LampArrayBlinkEffect.RepetitionMode <br> LampArrayBlinkEffect.StartDelay <br> LampArrayBlinkEffect.SustainDuration <br> LampArrayBlinkEffect.ZIndex

#### [LampArrayColorRampEffect](https://docs.microsoft.com/uwp/api/windows.devices.lights.effects.lamparraycolorrampeffect)

LampArrayColorRampEffect <br> LampArrayColorRampEffect.Color <br> LampArrayColorRampEffect.CompletionBehavior <br> LampArrayColorRampEffect.#ctor <br> LampArrayColorRampEffect.RampDuration <br> LampArrayColorRampEffect.StartDelay <br> LampArrayColorRampEffect.ZIndex

#### [LampArrayCustomEffect](https://docs.microsoft.com/uwp/api/windows.devices.lights.effects.lamparraycustomeffect)

LampArrayCustomEffect <br> LampArrayCustomEffect.Duration <br> LampArrayCustomEffect.#ctor <br> LampArrayCustomEffect.UpdateInterval <br> LampArrayCustomEffect.UpdateRequested <br> LampArrayCustomEffect.ZIndex

#### [LampArrayEffectCompletionBehavior](https://docs.microsoft.com/uwp/api/windows.devices.lights.effects.lamparrayeffectcompletionbehavior)

LampArrayEffectCompletionBehavior

#### [LampArrayEffectPlaylist](https://docs.microsoft.com/uwp/api/windows.devices.lights.effects.lamparrayeffectplaylist)

LampArrayEffectPlaylist <br> LampArrayEffectPlaylist.Append <br> LampArrayEffectPlaylist.EffectStartMode <br> LampArrayEffectPlaylist.First <br> LampArrayEffectPlaylist.GetAt <br> LampArrayEffectPlaylist.GetMany <br> LampArrayEffectPlaylist.IndexOf <br> LampArrayEffectPlaylist.#ctor <br> LampArrayEffectPlaylist.Occurrences <br> LampArrayEffectPlaylist.OverrideZIndex <br> LampArrayEffectPlaylist.Pause <br> LampArrayEffectPlaylist.PauseAll <br> LampArrayEffectPlaylist.RepetitionMode <br> LampArrayEffectPlaylist.Size <br> LampArrayEffectPlaylist.Start <br> LampArrayEffectPlaylist.StartAll <br> LampArrayEffectPlaylist.Stop <br> LampArrayEffectPlaylist.StopAll

#### [LampArrayEffectStartMode](https://docs.microsoft.com/uwp/api/windows.devices.lights.effects.lamparrayeffectstartmode)

LampArrayEffectStartMode

#### [LampArrayRepetitionMode](https://docs.microsoft.com/uwp/api/windows.devices.lights.effects.lamparrayrepetitionmode)

LampArrayRepetitionMode

#### [LampArraySolidEffect](https://docs.microsoft.com/uwp/api/windows.devices.lights.effects.lamparraysolideffect)

LampArraySolidEffect <br> LampArraySolidEffect.Color <br> LampArraySolidEffect.CompletionBehavior <br> LampArraySolidEffect.Duration <br> LampArraySolidEffect.#ctor <br> LampArraySolidEffect.StartDelay <br> LampArraySolidEffect.ZIndex

#### [LampArrayUpdateRequestedEventArgs](https://docs.microsoft.com/uwp/api/windows.devices.lights.effects.lamparrayupdaterequestedeventargs)

LampArrayUpdateRequestedEventArgs <br> LampArrayUpdateRequestedEventArgs.SetColor <br> LampArrayUpdateRequestedEventArgs.SetColorForIndex <br> LampArrayUpdateRequestedEventArgs.SetColorsForIndices <br> LampArrayUpdateRequestedEventArgs.SetSingleColorForIndices <br> LampArrayUpdateRequestedEventArgs.SinceStarted

### [Windows.Devices.Lights](https://docs.microsoft.com/uwp/api/windows.devices.lights)

#### [LampArray](https://docs.microsoft.com/uwp/api/windows.devices.lights.lamparray)

LampArray <br> LampArray.BoundingBox <br> LampArray.BrightnessLevel <br> LampArray.DeviceId <br> LampArray.FromIdAsync <br> LampArray.GetDeviceSelector <br> LampArray.GetIndicesForKey <br> LampArray.GetIndicesForPurposes <br> LampArray.GetLampInfo <br> LampArray.HardwareProductId <br> LampArray.HardwareVendorId <br> LampArray.HardwareVersion <br> LampArray.IsConnected <br> LampArray.IsEnabled <br> LampArray.LampArrayKind <br> LampArray.LampCount <br> LampArray.MinUpdateInterval <br> LampArray.RequestMessageAsync <br> LampArray.SendMessageAsync <br> LampArray.SetColor <br> LampArray.SetColorForIndex <br> LampArray.SetColorsForIndices <br> LampArray.SetColorsForKey <br> LampArray.SetColorsForKeys <br> LampArray.SetColorsForPurposes <br> LampArray.SetSingleColorForIndices <br> LampArray.SupportsVirtualKeys

#### [LampArrayKind](https://docs.microsoft.com/uwp/api/windows.devices.lights.lamparraykind)

LampArrayKind

#### [LampInfo](https://docs.microsoft.com/uwp/api/windows.devices.lights.lampinfo)

LampInfo <br> LampInfo.BlueLevelCount <br> LampInfo.FixedColor <br> LampInfo.GainLevelCount <br> LampInfo.GetNearestSupportedColor <br> LampInfo.GreenLevelCount <br> LampInfo.Index <br> LampInfo.Position <br> LampInfo.Purposes <br> LampInfo.RedLevelCount <br> LampInfo.UpdateLatency

#### [LampPurposes](https://docs.microsoft.com/uwp/api/windows.devices.lights.lamppurposes)

LampPurposes

### [Windows.Devices.PointOfService.Provider](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.provider)

#### [BarcodeScannerDisableScannerRequest](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.provider.barcodescannerdisablescannerrequest)

BarcodeScannerDisableScannerRequest.ReportFailedAsync <br> BarcodeScannerDisableScannerRequest.ReportFailedAsync

#### [BarcodeScannerEnableScannerRequest](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.provider.barcodescannerenablescannerrequest)

BarcodeScannerEnableScannerRequest.ReportFailedAsync <br> BarcodeScannerEnableScannerRequest.ReportFailedAsync

#### [BarcodeScannerFrameReader](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.provider.barcodescannerframereader)

BarcodeScannerFrameReader <br> BarcodeScannerFrameReader.Close <br> BarcodeScannerFrameReader.Connection <br> BarcodeScannerFrameReader.FrameArrived <br> BarcodeScannerFrameReader.StartAsync <br> BarcodeScannerFrameReader.StopAsync <br> BarcodeScannerFrameReader.TryAcquireLatestFrameAsync

#### [BarcodeScannerFrameReaderFrameArrivedEventArgs](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.provider.barcodescannerframereaderframearrivedeventargs)

BarcodeScannerFrameReaderFrameArrivedEventArgs <br> BarcodeScannerFrameReaderFrameArrivedEventArgs.GetDeferral

#### [BarcodeScannerGetSymbologyAttributesRequest](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.provider.barcodescannergetsymbologyattributesrequest)

BarcodeScannerGetSymbologyAttributesRequest.ReportFailedAsync <br> BarcodeScannerGetSymbologyAttributesRequest.ReportFailedAsync

#### [BarcodeScannerHideVideoPreviewRequest](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.provider.barcodescannerhidevideopreviewrequest)

BarcodeScannerHideVideoPreviewRequest.ReportFailedAsync <br> BarcodeScannerHideVideoPreviewRequest.ReportFailedAsync

#### [BarcodeScannerProviderConnection](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.provider.barcodescannerproviderconnection)

BarcodeScannerProviderConnection.CreateFrameReaderAsync <br> BarcodeScannerProviderConnection.CreateFrameReaderAsync <br> BarcodeScannerProviderConnection.CreateFrameReaderAsync

#### [BarcodeScannerSetActiveSymbologiesRequest](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.provider.barcodescannersetactivesymbologiesrequest)

BarcodeScannerSetActiveSymbologiesRequest.ReportFailedAsync <br> BarcodeScannerSetActiveSymbologiesRequest.ReportFailedAsync

#### [BarcodeScannerSetSymbologyAttributesRequest](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.provider.barcodescannersetsymbologyattributesrequest)

BarcodeScannerSetSymbologyAttributesRequest.ReportFailedAsync <br> BarcodeScannerSetSymbologyAttributesRequest.ReportFailedAsync

#### [BarcodeScannerStartSoftwareTriggerRequest](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.provider.barcodescannerstartsoftwaretriggerrequest)

BarcodeScannerStartSoftwareTriggerRequest.ReportFailedAsync <br> BarcodeScannerStartSoftwareTriggerRequest.ReportFailedAsync

#### [BarcodeScannerStopSoftwareTriggerRequest](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.provider.barcodescannerstopsoftwaretriggerrequest)

BarcodeScannerStopSoftwareTriggerRequest.ReportFailedAsync <br> BarcodeScannerStopSoftwareTriggerRequest.ReportFailedAsync

#### [BarcodeScannerVideoFrame](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.provider.barcodescannervideoframe)

BarcodeScannerVideoFrame <br> BarcodeScannerVideoFrame.Close <br> BarcodeScannerVideoFrame.Format <br> BarcodeScannerVideoFrame.Height <br> BarcodeScannerVideoFrame.PixelData <br> BarcodeScannerVideoFrame.Width

### [Windows.Devices.PointOfService](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice)

#### [BarcodeScannerCapabilities](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.barcodescannercapabilities)

BarcodeScannerCapabilities.IsVideoPreviewSupported

#### [ClaimedBarcodeScanner](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.claimedbarcodescanner)

ClaimedBarcodeScanner.Closed

#### [ClaimedBarcodeScannerClosedEventArgs](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.claimedbarcodescannerclosedeventargs)

ClaimedBarcodeScannerClosedEventArgs

#### [ClaimedCashDrawer](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.claimedcashdrawer)

ClaimedCashDrawer.Closed

#### [ClaimedCashDrawerClosedEventArgs](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.claimedcashdrawerclosedeventargs)

ClaimedCashDrawerClosedEventArgs

#### [ClaimedLineDisplay](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.claimedlinedisplay)

ClaimedLineDisplay.Closed

#### [ClaimedLineDisplayClosedEventArgs](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.claimedlinedisplayclosedeventargs)

ClaimedLineDisplayClosedEventArgs

#### [ClaimedMagneticStripeReader](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.claimedmagneticstripereader)

ClaimedMagneticStripeReader.Closed

#### [ClaimedMagneticStripeReaderClosedEventArgs](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.claimedmagneticstripereaderclosedeventargs)

ClaimedMagneticStripeReaderClosedEventArgs

#### [ClaimedPosPrinter](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.claimedposprinter)

ClaimedPosPrinter.Closed

#### [ClaimedPosPrinterClosedEventArgs](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.claimedposprinterclosedeventargs)

ClaimedPosPrinterClosedEventArgs

### [Windows.Devices.Sensors](https://docs.microsoft.com/uwp/api/windows.devices.sensors)

#### [SimpleOrientationSensor](https://docs.microsoft.com/uwp/api/windows.devices.sensors.simpleorientationsensor)

SimpleOrientationSensor.FromIdAsync <br> SimpleOrientationSensor.GetDeviceSelector

### [Windows.Devices.SmartCards](https://docs.microsoft.com/uwp/api/windows.devices.smartcards)

#### [KnownSmartCardAppletIds](https://docs.microsoft.com/uwp/api/windows.devices.smartcards.knownsmartcardappletids)

KnownSmartCardAppletIds <br> KnownSmartCardAppletIds.PaymentSystemEnvironment <br> KnownSmartCardAppletIds.ProximityPaymentSystemEnvironment

#### [SmartCardAppletIdGroup](https://docs.microsoft.com/uwp/api/windows.devices.smartcards.smartcardappletidgroup)

SmartCardAppletIdGroup.Description <br> SmartCardAppletIdGroup.Logo <br> SmartCardAppletIdGroup.Properties <br> SmartCardAppletIdGroup.SecureUserAuthenticationRequired

#### [SmartCardAppletIdGroupRegistration](https://docs.microsoft.com/uwp/api/windows.devices.smartcards.smartcardappletidgroupregistration)

SmartCardAppletIdGroupRegistration.SetPropertiesAsync <br> SmartCardAppletIdGroupRegistration.SmartCardReaderId

## Windows.Foundation

### [Windows.Foundation](https://docs.microsoft.com/uwp/api/windows.foundation)

#### [GuidHelper](https://docs.microsoft.com/uwp/api/windows.foundation.guidhelper)

GuidHelper <br> GuidHelper.CreateNewGuid <br> GuidHelper.Empty <br> GuidHelper.Equals

## Windows.Globalization

### [Windows.Globalization](https://docs.microsoft.com/uwp/api/windows.globalization)

#### [CurrencyIdentifiers](https://docs.microsoft.com/uwp/api/windows.globalization.currencyidentifiers)

CurrencyIdentifiers.MRU <br> CurrencyIdentifiers.SSP <br> CurrencyIdentifiers.STN <br> CurrencyIdentifiers.VES

## Windows.Graphics

### [Windows.Graphics.Capture](https://docs.microsoft.com/uwp/api/windows.graphics.capture)

#### [Direct3D11CaptureFramePool](https://docs.microsoft.com/uwp/api/windows.graphics.capture.direct3d11captureframepool)

Direct3D11CaptureFramePool.CreateFreeThreaded

#### [GraphicsCaptureItem](https://docs.microsoft.com/uwp/api/windows.graphics.capture.graphicscaptureitem)

GraphicsCaptureItem.CreateFromVisual

### [Windows.Graphics.Display.Core](https://docs.microsoft.com/uwp/api/windows.graphics.display.core)

#### [HdmiDisplayMode](https://docs.microsoft.com/uwp/api/windows.graphics.display.core.hdmidisplaymode)

HdmiDisplayMode.IsDolbyVisionLowLatencySupported

### [Windows.Graphics.Holographic](https://docs.microsoft.com/uwp/api/windows.graphics.holographic)

#### [HolographicCamera](https://docs.microsoft.com/uwp/api/windows.graphics.holographic.holographiccamera)

HolographicCamera.IsHardwareContentProtectionEnabled <br> HolographicCamera.IsHardwareContentProtectionSupported

#### [HolographicQuadLayerUpdateParameters](https://docs.microsoft.com/uwp/api/windows.graphics.holographic.holographicquadlayerupdateparameters)

HolographicQuadLayerUpdateParameters.AcquireBufferToUpdateContentWithHardwareProtection <br> HolographicQuadLayerUpdateParameters.CanAcquireWithHardwareProtection

### [Windows.Graphics.Imaging](https://docs.microsoft.com/uwp/api/windows.graphics.imaging)

#### [BitmapDecoder](https://docs.microsoft.com/uwp/api/windows.graphics.imaging.bitmapdecoder)

BitmapDecoder.HeifDecoderId <br> BitmapDecoder.WebpDecoderId

#### [BitmapEncoder](https://docs.microsoft.com/uwp/api/windows.graphics.imaging.bitmapencoder)

BitmapEncoder.HeifEncoderId

## Windows.Management

### [Windows.Management.Deployment](https://docs.microsoft.com/uwp/api/windows.management.deployment)

#### [PackageManager](https://docs.microsoft.com/uwp/api/windows.management.deployment.packagemanager)

PackageManager.DeprovisionPackageForAllUsersAsync

## Windows.Media

### [Windows.Media.Audio](https://docs.microsoft.com/uwp/api/windows.media.audio)

#### [CreateAudioDeviceInputNodeResult](https://docs.microsoft.com/uwp/api/windows.media.audio.createaudiodeviceinputnoderesult)

CreateAudioDeviceInputNodeResult.ExtendedError

#### [CreateAudioDeviceOutputNodeResult](https://docs.microsoft.com/uwp/api/windows.media.audio.createaudiodeviceoutputnoderesult)

CreateAudioDeviceOutputNodeResult.ExtendedError

#### [CreateAudioFileInputNodeResult](https://docs.microsoft.com/uwp/api/windows.media.audio.createaudiofileinputnoderesult)

CreateAudioFileInputNodeResult.ExtendedError

#### [CreateAudioFileOutputNodeResult](https://docs.microsoft.com/uwp/api/windows.media.audio.createaudiofileoutputnoderesult)

CreateAudioFileOutputNodeResult.ExtendedError

#### [CreateAudioGraphResult](https://docs.microsoft.com/uwp/api/windows.media.audio.createaudiographresult)

CreateAudioGraphResult.ExtendedError

#### [CreateMediaSourceAudioInputNodeResult](https://docs.microsoft.com/uwp/api/windows.media.audio.createmediasourceaudioinputnoderesult)

CreateMediaSourceAudioInputNodeResult.ExtendedError

#### [MixedRealitySpatialAudioFormatPolicy](https://docs.microsoft.com/uwp/api/windows.media.audio.mixedrealityspatialaudioformatpolicy)

MixedRealitySpatialAudioFormatPolicy

#### [SetDefaultSpatialAudioFormatResult](https://docs.microsoft.com/uwp/api/windows.media.audio.setdefaultspatialaudioformatresult)

SetDefaultSpatialAudioFormatResult <br> SetDefaultSpatialAudioFormatResult.Status

#### [SetDefaultSpatialAudioFormatStatus](https://docs.microsoft.com/uwp/api/windows.media.audio.setdefaultspatialaudioformatstatus)

SetDefaultSpatialAudioFormatStatus

#### [SpatialAudioDeviceConfiguration](https://docs.microsoft.com/uwp/api/windows.media.audio.spatialaudiodeviceconfiguration)

SpatialAudioDeviceConfiguration <br> SpatialAudioDeviceConfiguration.ActiveSpatialAudioFormat <br> SpatialAudioDeviceConfiguration.ConfigurationChanged <br> SpatialAudioDeviceConfiguration.DefaultSpatialAudioFormat <br> SpatialAudioDeviceConfiguration.DeviceId <br> SpatialAudioDeviceConfiguration.GetForDeviceId <br> SpatialAudioDeviceConfiguration.IsSpatialAudioFormatSupported <br> SpatialAudioDeviceConfiguration.IsSpatialAudioSupported <br> SpatialAudioDeviceConfiguration.SetDefaultSpatialAudioFormatAsync

#### [SpatialAudioFormatConfiguration](https://docs.microsoft.com/uwp/api/windows.media.audio.spatialaudioformatconfiguration)

SpatialAudioFormatConfiguration <br> SpatialAudioFormatConfiguration.GetDefault <br> SpatialAudioFormatConfiguration.MixedRealityExclusiveModePolicy <br> SpatialAudioFormatConfiguration.ReportConfigurationChangedAsync <br> SpatialAudioFormatConfiguration.ReportLicenseChangedAsync

#### [SpatialAudioFormatSubtype](https://docs.microsoft.com/uwp/api/windows.media.audio.spatialaudioformatsubtype)

SpatialAudioFormatSubtype <br> SpatialAudioFormatSubtype.DolbyAtmosForHeadphones <br> SpatialAudioFormatSubtype.DolbyAtmosForHomeTheater <br> SpatialAudioFormatSubtype.DolbyAtmosForSpeakers <br> SpatialAudioFormatSubtype.DTSHeadphoneX <br> SpatialAudioFormatSubtype.DTSXUltra <br> SpatialAudioFormatSubtype.WindowsSonic

### [Windows.Media.Control](https://docs.microsoft.com/uwp/api/windows.media.control)

#### [CurrentSessionChangedEventArgs](https://docs.microsoft.com/uwp/api/windows.media.control.currentsessionchangedeventargs)

CurrentSessionChangedEventArgs

#### [GlobalSystemMediaTransportControlsSession](https://docs.microsoft.com/uwp/api/windows.media.control.globalsystemmediatransportcontrolssession)

GlobalSystemMediaTransportControlsSession <br> GlobalSystemMediaTransportControlsSession.GetPlaybackInfo <br> GlobalSystemMediaTransportControlsSession.GetTimelineProperties <br> GlobalSystemMediaTransportControlsSession.MediaPropertiesChanged <br> GlobalSystemMediaTransportControlsSession.PlaybackInfoChanged <br> GlobalSystemMediaTransportControlsSession.SourceAppUserModelId <br> GlobalSystemMediaTransportControlsSession.TimelinePropertiesChanged <br> GlobalSystemMediaTransportControlsSession.TryChangeAutoRepeatModeAsync <br> GlobalSystemMediaTransportControlsSession.TryChangeChannelDownAsync <br> GlobalSystemMediaTransportControlsSession.TryChangeChannelUpAsync <br> GlobalSystemMediaTransportControlsSession.TryChangePlaybackPositionAsync <br> GlobalSystemMediaTransportControlsSession.TryChangePlaybackRateAsync <br> GlobalSystemMediaTransportControlsSession.TryChangeShuffleActiveAsync <br> GlobalSystemMediaTransportControlsSession.TryFastForwardAsync <br> GlobalSystemMediaTransportControlsSession.TryGetMediaPropertiesAsync <br> GlobalSystemMediaTransportControlsSession.TryPauseAsync <br> GlobalSystemMediaTransportControlsSession.TryPlayAsync <br> GlobalSystemMediaTransportControlsSession.TryRecordAsync <br> GlobalSystemMediaTransportControlsSession.TryRewindAsync <br> GlobalSystemMediaTransportControlsSession.TrySkipNextAsync <br> GlobalSystemMediaTransportControlsSession.TrySkipPreviousAsync <br> GlobalSystemMediaTransportControlsSession.TryStopAsync <br> GlobalSystemMediaTransportControlsSession.TryTogglePlayPauseAsync

#### [GlobalSystemMediaTransportControlsSessionManager](https://docs.microsoft.com/uwp/api/windows.media.control.globalsystemmediatransportcontrolssessionmanager)

GlobalSystemMediaTransportControlsSessionManager <br> GlobalSystemMediaTransportControlsSessionManager.CurrentSessionChanged <br> GlobalSystemMediaTransportControlsSessionManager.GetCurrentSession <br> GlobalSystemMediaTransportControlsSessionManager.GetSessions <br> GlobalSystemMediaTransportControlsSessionManager.RequestAsync <br> GlobalSystemMediaTransportControlsSessionManager.SessionsChanged

#### [GlobalSystemMediaTransportControlsSessionMediaProperties](https://docs.microsoft.com/uwp/api/windows.media.control.globalsystemmediatransportcontrolssessionmediaproperties)

GlobalSystemMediaTransportControlsSessionMediaProperties <br> GlobalSystemMediaTransportControlsSessionMediaProperties.AlbumArtist <br> GlobalSystemMediaTransportControlsSessionMediaProperties.AlbumTitle <br> GlobalSystemMediaTransportControlsSessionMediaProperties.AlbumTrackCount <br> GlobalSystemMediaTransportControlsSessionMediaProperties.Artist <br> GlobalSystemMediaTransportControlsSessionMediaProperties.Genres <br> GlobalSystemMediaTransportControlsSessionMediaProperties.PlaybackType <br> GlobalSystemMediaTransportControlsSessionMediaProperties.Subtitle <br> GlobalSystemMediaTransportControlsSessionMediaProperties.Thumbnail <br> GlobalSystemMediaTransportControlsSessionMediaProperties.Title <br> GlobalSystemMediaTransportControlsSessionMediaProperties.TrackNumber

#### [GlobalSystemMediaTransportControlsSessionPlaybackControls](https://docs.microsoft.com/uwp/api/windows.media.control.globalsystemmediatransportcontrolssessionplaybackcontrols)

GlobalSystemMediaTransportControlsSessionPlaybackControls <br> GlobalSystemMediaTransportControlsSessionPlaybackControls.IsChannelDownEnabled <br> GlobalSystemMediaTransportControlsSessionPlaybackControls.IsChannelUpEnabled <br> GlobalSystemMediaTransportControlsSessionPlaybackControls.IsFastForwardEnabled <br> GlobalSystemMediaTransportControlsSessionPlaybackControls.IsNextEnabled <br> GlobalSystemMediaTransportControlsSessionPlaybackControls.IsPauseEnabled <br> GlobalSystemMediaTransportControlsSessionPlaybackControls.IsPlaybackPositionEnabled <br> GlobalSystemMediaTransportControlsSessionPlaybackControls.IsPlaybackRateEnabled <br> GlobalSystemMediaTransportControlsSessionPlaybackControls.IsPlayEnabled <br> GlobalSystemMediaTransportControlsSessionPlaybackControls.IsPlayPauseToggleEnabled <br> GlobalSystemMediaTransportControlsSessionPlaybackControls.IsPreviousEnabled <br> GlobalSystemMediaTransportControlsSessionPlaybackControls.IsRecordEnabled <br> GlobalSystemMediaTransportControlsSessionPlaybackControls.IsRepeatEnabled <br> GlobalSystemMediaTransportControlsSessionPlaybackControls.IsRewindEnabled <br> GlobalSystemMediaTransportControlsSessionPlaybackControls.IsShuffleEnabled <br> GlobalSystemMediaTransportControlsSessionPlaybackControls.IsStopEnabled

#### [GlobalSystemMediaTransportControlsSessionPlaybackInfo](https://docs.microsoft.com/uwp/api/windows.media.control.globalsystemmediatransportcontrolssessionplaybackinfo)

GlobalSystemMediaTransportControlsSessionPlaybackInfo <br> GlobalSystemMediaTransportControlsSessionPlaybackInfo.AutoRepeatMode <br> GlobalSystemMediaTransportControlsSessionPlaybackInfo.Controls <br> GlobalSystemMediaTransportControlsSessionPlaybackInfo.IsShuffleActive <br> GlobalSystemMediaTransportControlsSessionPlaybackInfo.PlaybackRate <br> GlobalSystemMediaTransportControlsSessionPlaybackInfo.PlaybackStatus <br> GlobalSystemMediaTransportControlsSessionPlaybackInfo.PlaybackType

#### [GlobalSystemMediaTransportControlsSessionPlaybackStatus](https://docs.microsoft.com/uwp/api/windows.media.control.globalsystemmediatransportcontrolssessionplaybackstatus)

GlobalSystemMediaTransportControlsSessionPlaybackStatus

#### [GlobalSystemMediaTransportControlsSessionTimelineProperties](https://docs.microsoft.com/uwp/api/windows.media.control.globalsystemmediatransportcontrolssessiontimelineproperties)

GlobalSystemMediaTransportControlsSessionTimelineProperties <br> GlobalSystemMediaTransportControlsSessionTimelineProperties.EndTime <br> GlobalSystemMediaTransportControlsSessionTimelineProperties.LastUpdatedTime <br> GlobalSystemMediaTransportControlsSessionTimelineProperties.MaxSeekTime <br> GlobalSystemMediaTransportControlsSessionTimelineProperties.MinSeekTime <br> GlobalSystemMediaTransportControlsSessionTimelineProperties.Position <br> GlobalSystemMediaTransportControlsSessionTimelineProperties.StartTime

#### [MediaPropertiesChangedEventArgs](https://docs.microsoft.com/uwp/api/windows.media.control.mediapropertieschangedeventargs)

MediaPropertiesChangedEventArgs

#### [PlaybackInfoChangedEventArgs](https://docs.microsoft.com/uwp/api/windows.media.control.playbackinfochangedeventargs)

PlaybackInfoChangedEventArgs

#### [SessionsChangedEventArgs](https://docs.microsoft.com/uwp/api/windows.media.control.sessionschangedeventargs)

SessionsChangedEventArgs

#### [TimelinePropertiesChangedEventArgs](https://docs.microsoft.com/uwp/api/windows.media.control.timelinepropertieschangedeventargs)

TimelinePropertiesChangedEventArgs

### [Windows.Media.Core](https://docs.microsoft.com/uwp/api/windows.media.core)

#### [MediaStreamSample](https://docs.microsoft.com/uwp/api/windows.media.core.mediastreamsample)

MediaStreamSample.CreateFromDirect3D11Surface <br> MediaStreamSample.Direct3D11Surface

### [Windows.Media.Devices.Core](https://docs.microsoft.com/uwp/api/windows.media.devices.core)

#### [CameraIntrinsics](https://docs.microsoft.com/uwp/api/windows.media.devices.core.cameraintrinsics)

CameraIntrinsics.#ctor

### [Windows.Media.Import](https://docs.microsoft.com/uwp/api/windows.media.import)

#### [PhotoImportItem](https://docs.microsoft.com/uwp/api/windows.media.import.photoimportitem)

PhotoImportItem.Path

### [Windows.Media.MediaProperties](https://docs.microsoft.com/uwp/api/windows.media.mediaproperties)

#### [ImageEncodingProperties](https://docs.microsoft.com/uwp/api/windows.media.mediaproperties.imageencodingproperties)

ImageEncodingProperties.CreateHeif

#### [MediaEncodingSubtypes](https://docs.microsoft.com/uwp/api/windows.media.mediaproperties.mediaencodingsubtypes)

MediaEncodingSubtypes.Heif

### [Windows.Media.Protection.PlayReady](https://docs.microsoft.com/uwp/api/windows.media.protection.playready)

#### [PlayReadyStatics](https://docs.microsoft.com/uwp/api/windows.media.protection.playready.playreadystatics)

PlayReadyStatics.HardwareDRMDisabledAtTime <br> PlayReadyStatics.HardwareDRMDisabledUntilTime <br> PlayReadyStatics.ResetHardwareDRMDisabled

## Windows.Networking

### [Windows.Networking.Connectivity](https://docs.microsoft.com/uwp/api/windows.networking.connectivity)

#### [ConnectionProfile](https://docs.microsoft.com/uwp/api/windows.networking.connectivity.connectionprofile)

ConnectionProfile.CanDelete <br> ConnectionProfile.TryDeleteAsync

#### [ConnectionProfileDeleteStatus](https://docs.microsoft.com/uwp/api/windows.networking.connectivity.connectionprofiledeletestatus)

ConnectionProfileDeleteStatus

## Windows.Perception

### [Windows.Perception.Spatial.Preview](https://docs.microsoft.com/uwp/api/windows.perception.spatial.preview)

#### [SpatialGraphInteropPreview](https://docs.microsoft.com/uwp/api/windows.perception.spatial.preview.spatialgraphinteroppreview)

SpatialGraphInteropPreview <br> SpatialGraphInteropPreview.CreateCoordinateSystemForNode <br> SpatialGraphInteropPreview.CreateCoordinateSystemForNode <br> SpatialGraphInteropPreview.CreateCoordinateSystemForNode <br> SpatialGraphInteropPreview.CreateLocatorForNode

### [Windows.Perception.Spatial](https://docs.microsoft.com/uwp/api/windows.perception.spatial)

#### [SpatialAnchorExporter](https://docs.microsoft.com/uwp/api/windows.perception.spatial.spatialanchorexporter)

SpatialAnchorExporter <br> SpatialAnchorExporter.GetAnchorExportSufficiencyAsync <br> SpatialAnchorExporter.GetDefault <br> SpatialAnchorExporter.RequestAccessAsync <br> SpatialAnchorExporter.TryExportAnchorAsync

#### [SpatialAnchorExportPurpose](https://docs.microsoft.com/uwp/api/windows.perception.spatial.spatialanchorexportpurpose)

SpatialAnchorExportPurpose

#### [SpatialAnchorExportSufficiency](https://docs.microsoft.com/uwp/api/windows.perception.spatial.spatialanchorexportsufficiency)

SpatialAnchorExportSufficiency <br> SpatialAnchorExportSufficiency.IsMinimallySufficient <br> SpatialAnchorExportSufficiency.RecommendedSufficiencyLevel <br> SpatialAnchorExportSufficiency.SufficiencyLevel

#### [SpatialLocation](https://docs.microsoft.com/uwp/api/windows.perception.spatial.spatiallocation)

SpatialLocation.AbsoluteAngularAccelerationAxisAngle <br> SpatialLocation.AbsoluteAngularVelocityAxisAngle

### [Windows.Perception](https://docs.microsoft.com/uwp/api/windows.perception)

#### [PerceptionTimestamp](https://docs.microsoft.com/uwp/api/windows.perception.perceptiontimestamp)

PerceptionTimestamp.SystemRelativeTargetTime

#### [PerceptionTimestampHelper](https://docs.microsoft.com/uwp/api/windows.perception.perceptiontimestamphelper)

PerceptionTimestampHelper.FromSystemRelativeTargetTime

## Windows.Services

### [Windows.Services.Cortana](https://docs.microsoft.com/uwp/api/windows.services.cortana)

#### [CortanaActionableInsights](https://docs.microsoft.com/uwp/api/windows.services.cortana.cortanaactionableinsights)

CortanaActionableInsights <br> CortanaActionableInsights.GetDefault <br> CortanaActionableInsights.GetForUser <br> CortanaActionableInsights.IsAvailableAsync <br> CortanaActionableInsights.ShowInsightsAsync <br> CortanaActionableInsights.ShowInsightsAsync <br> CortanaActionableInsights.ShowInsightsForImageAsync <br> CortanaActionableInsights.ShowInsightsForImageAsync <br> CortanaActionableInsights.ShowInsightsForTextAsync <br> CortanaActionableInsights.ShowInsightsForTextAsync <br> CortanaActionableInsights.User

#### [CortanaActionableInsightsOptions](https://docs.microsoft.com/uwp/api/windows.services.cortana.cortanaactionableinsightsoptions)

CortanaActionableInsightsOptions <br> CortanaActionableInsightsOptions.ContentSourceWebLink <br> CortanaActionableInsightsOptions.#ctor <br> CortanaActionableInsightsOptions.SurroundingText

### [Windows.Services.Store](https://docs.microsoft.com/uwp/api/windows.services.store)

#### [StoreAppLicense](https://docs.microsoft.com/uwp/api/windows.services.store.storeapplicense)

StoreAppLicense.IsDiscLicense

#### [StoreContext](https://docs.microsoft.com/uwp/api/windows.services.store.storecontext)

StoreContext.RequestRateAndReviewAppAsync <br> StoreContext.SetInstallOrderForAssociatedStoreQueueItemsAsync

#### [StoreQueueItem](https://docs.microsoft.com/uwp/api/windows.services.store.storequeueitem)

StoreQueueItem.CancelInstallAsync <br> StoreQueueItem.PauseInstallAsync <br> StoreQueueItem.ResumeInstallAsync

#### [StoreRateAndReviewResult](https://docs.microsoft.com/uwp/api/windows.services.store.storerateandreviewresult)

StoreRateAndReviewResult <br> StoreRateAndReviewResult.ExtendedError <br> StoreRateAndReviewResult.ExtendedJsonData <br> StoreRateAndReviewResult.Status <br> StoreRateAndReviewResult.WasUpdated

#### [StoreRateAndReviewStatus](https://docs.microsoft.com/uwp/api/windows.services.store.storerateandreviewstatus)

StoreRateAndReviewStatus

## Windows.Storage

### [Windows.Storage.Provider](https://docs.microsoft.com/uwp/api/windows.storage.provider)

#### [StorageProviderSyncRootInfo](https://docs.microsoft.com/uwp/api/windows.storage.provider.storageprovidersyncrootinfo)

StorageProviderSyncRootInfo.ProviderId

## Windows.System

### [Windows.System.Implementation.Holographic](https://docs.microsoft.com/uwp/api/windows.system.implementation.holographic)

#### [SysHolographicDeploymentProgress](https://docs.microsoft.com/uwp/api/windows.system.implementation.holographic.sysholographicdeploymentprogress)

SysHolographicDeploymentProgress

#### [SysHolographicDeploymentResult](https://docs.microsoft.com/uwp/api/windows.system.implementation.holographic.sysholographicdeploymentresult)

SysHolographicDeploymentResult <br> SysHolographicDeploymentResult.DeploymentState <br> SysHolographicDeploymentResult.ExtendedError

#### [SysHolographicDeploymentState](https://docs.microsoft.com/uwp/api/windows.system.implementation.holographic.sysholographicdeploymentstate)

SysHolographicDeploymentState

#### [SysHolographicDisplay](https://docs.microsoft.com/uwp/api/windows.system.implementation.holographic.sysholographicdisplay)

SysHolographicDisplay <br> SysHolographicDisplay.DeviceId <br> SysHolographicDisplay.Display <br> SysHolographicDisplay.ExperienceMode <br> SysHolographicDisplay.LeftViewportParameters <br> SysHolographicDisplay.OutputAdapterId <br> SysHolographicDisplay.RightViewportParameters

#### [SysHolographicDisplayExperienceMode](https://docs.microsoft.com/uwp/api/windows.system.implementation.holographic.sysholographicdisplayexperiencemode)

SysHolographicDisplayExperienceMode

#### [SysHolographicDisplayWatcher](https://docs.microsoft.com/uwp/api/windows.system.implementation.holographic.sysholographicdisplaywatcher)

SysHolographicDisplayWatcher <br> SysHolographicDisplayWatcher.Added <br> SysHolographicDisplayWatcher.EnumerationCompleted <br> SysHolographicDisplayWatcher.Removed <br> SysHolographicDisplayWatcher.Start <br> SysHolographicDisplayWatcher.Status <br> SysHolographicDisplayWatcher.Stop <br> SysHolographicDisplayWatcher.Stopped <br> SysHolographicDisplayWatcher.#ctor

#### [SysHolographicDisplayWatcherStatus](https://docs.microsoft.com/uwp/api/windows.system.implementation.holographic.sysholographicdisplaywatcherstatus)

SysHolographicDisplayWatcherStatus

#### [SysHolographicPreviewMediaSource](https://docs.microsoft.com/uwp/api/windows.system.implementation.holographic.sysholographicpreviewmediasource)

SysHolographicPreviewMediaSource <br> SysHolographicPreviewMediaSource.Create

#### [SysHolographicWindowingEnvironment](https://docs.microsoft.com/uwp/api/windows.system.implementation.holographic.sysholographicwindowingenvironment)

SysHolographicWindowingEnvironment <br> SysHolographicWindowingEnvironment.DeployAsync <br> SysHolographicWindowingEnvironment.GetDefault <br> SysHolographicWindowingEnvironment.GetDeploymentStateAsync <br> SysHolographicWindowingEnvironment.IsDeviceSetupComplete <br> SysHolographicWindowingEnvironment.IsLearningExperienceComplete <br> SysHolographicWindowingEnvironment.IsPreviewActive <br> SysHolographicWindowingEnvironment.IsPreviewActiveChanged <br> SysHolographicWindowingEnvironment.IsProtectedContentPresent <br> SysHolographicWindowingEnvironment.IsProtectedContentPresentChanged <br> SysHolographicWindowingEnvironment.IsSpeechPersonalizationSupported <br> SysHolographicWindowingEnvironment.SetIsSpeechPersonalizationEnabledAsync <br> SysHolographicWindowingEnvironment.StartAsync <br> SysHolographicWindowingEnvironment.Status <br> SysHolographicWindowingEnvironment.StatusChanged <br> SysHolographicWindowingEnvironment.StopAsync

#### [SysHolographicWindowingEnvironmentComponentKind](https://docs.microsoft.com/uwp/api/windows.system.implementation.holographic.sysholographicwindowingenvironmentcomponentkind)

SysHolographicWindowingEnvironmentComponentKind

#### [SysHolographicWindowingEnvironmentComponentState](https://docs.microsoft.com/uwp/api/windows.system.implementation.holographic.sysholographicwindowingenvironmentcomponentstate)

SysHolographicWindowingEnvironmentComponentState

#### [SysHolographicWindowingEnvironmentComponentStatus](https://docs.microsoft.com/uwp/api/windows.system.implementation.holographic.sysholographicwindowingenvironmentcomponentstatus)

SysHolographicWindowingEnvironmentComponentStatus

#### [SysHolographicWindowingEnvironmentState](https://docs.microsoft.com/uwp/api/windows.system.implementation.holographic.sysholographicwindowingenvironmentstate)

SysHolographicWindowingEnvironmentState

#### [SysHolographicWindowingEnvironmentStatus](https://docs.microsoft.com/uwp/api/windows.system.implementation.holographic.sysholographicwindowingenvironmentstatus)

SysHolographicWindowingEnvironmentStatus <br> SysHolographicWindowingEnvironmentStatus.ComponentStatuses <br> SysHolographicWindowingEnvironmentStatus.State

#### [SysSpatialInputDevice](https://docs.microsoft.com/uwp/api/windows.system.implementation.holographic.sysspatialinputdevice)

SysSpatialInputDevice <br> SysSpatialInputDevice.Handedness <br> SysSpatialInputDevice.HasPositionalTracking <br> SysSpatialInputDevice.TryGetBatteryReport

#### [SysSpatialInputDeviceWatcher](https://docs.microsoft.com/uwp/api/windows.system.implementation.holographic.sysspatialinputdevicewatcher)

SysSpatialInputDeviceWatcher <br> SysSpatialInputDeviceWatcher.Added <br> SysSpatialInputDeviceWatcher.EnumerationCompleted <br> SysSpatialInputDeviceWatcher.Removed <br> SysSpatialInputDeviceWatcher.Start <br> SysSpatialInputDeviceWatcher.Status <br> SysSpatialInputDeviceWatcher.Stop <br> SysSpatialInputDeviceWatcher.Stopped <br> SysSpatialInputDeviceWatcher.#ctor <br> SysSpatialInputDeviceWatcher.Updated

#### [SysSpatialInputDeviceWatcherStatus](https://docs.microsoft.com/uwp/api/windows.system.implementation.holographic.sysspatialinputdevicewatcherstatus)

SysSpatialInputDeviceWatcherStatus

#### [SysSpatialLocator](https://docs.microsoft.com/uwp/api/windows.system.implementation.holographic.sysspatiallocator)

SysSpatialLocator <br> SysSpatialLocator.GetFloorLocator

#### [SysSpatialStageBoundaryDisposition](https://docs.microsoft.com/uwp/api/windows.system.implementation.holographic.sysspatialstageboundarydisposition)

SysSpatialStageBoundaryDisposition

#### [SysSpatialStageManager](https://docs.microsoft.com/uwp/api/windows.system.implementation.holographic.sysspatialstagemanager)

SysSpatialStageManager <br> SysSpatialStageManager.DoesAnyStageHaveBoundariesAsync <br> SysSpatialStageManager.GetBoundaryDisposition <br> SysSpatialStageManager.SetAndSaveNewStageAsync <br> SysSpatialStageManager.SetBoundaryEnabled <br> SysSpatialStageManager.#ctor <br> SysSpatialStageManager.UpdateStageAnchorAsync

### [Windows.System.Preview](https://docs.microsoft.com/uwp/api/windows.system.preview)

#### [HingeState](https://docs.microsoft.com/uwp/api/windows.system.preview.hingestate)

HingeState

#### [TwoPanelHingedDevicePosturePreview](https://docs.microsoft.com/uwp/api/windows.system.preview.twopanelhingeddeviceposturepreview)

TwoPanelHingedDevicePosturePreview <br> TwoPanelHingedDevicePosturePreview.GetCurrentPostureAsync <br> TwoPanelHingedDevicePosturePreview.GetDefaultAsync <br> TwoPanelHingedDevicePosturePreview.PostureChanged

#### [TwoPanelHingedDevicePosturePreviewReading](https://docs.microsoft.com/uwp/api/windows.system.preview.twopanelhingeddeviceposturepreviewreading)

TwoPanelHingedDevicePosturePreviewReading <br> TwoPanelHingedDevicePosturePreviewReading.HingeState <br> TwoPanelHingedDevicePosturePreviewReading.Panel1Id <br> TwoPanelHingedDevicePosturePreviewReading.Panel1Orientation <br> TwoPanelHingedDevicePosturePreviewReading.Panel2Id <br> TwoPanelHingedDevicePosturePreviewReading.Panel2Orientation <br> TwoPanelHingedDevicePosturePreviewReading.Timestamp

#### [TwoPanelHingedDevicePosturePreviewReadingChangedEventArgs](https://docs.microsoft.com/uwp/api/windows.system.preview.twopanelhingeddeviceposturepreviewreadingchangedeventargs)

TwoPanelHingedDevicePosturePreviewReadingChangedEventArgs <br> TwoPanelHingedDevicePosturePreviewReadingChangedEventArgs.Reading

### [Windows.System.Profile.SystemManufacturers](https://docs.microsoft.com/uwp/api/windows.system.profile.systemmanufacturers)

#### [SystemSupportDeviceInfo](https://docs.microsoft.com/uwp/api/windows.system.profile.systemmanufacturers.systemsupportdeviceinfo)

SystemSupportDeviceInfo <br> SystemSupportDeviceInfo.FriendlyName <br> SystemSupportDeviceInfo.OperatingSystem <br> SystemSupportDeviceInfo.SystemFirmwareVersion <br> SystemSupportDeviceInfo.SystemHardwareVersion <br> SystemSupportDeviceInfo.SystemManufacturer <br> SystemSupportDeviceInfo.SystemProductName <br> SystemSupportDeviceInfo.SystemSku

#### [SystemSupportInfo](https://docs.microsoft.com/uwp/api/windows.system.profile.systemmanufacturers.systemsupportinfo)

SystemSupportInfo.LocalDeviceInfo

### [Windows.System.Profile](https://docs.microsoft.com/uwp/api/windows.system.profile)

#### [SystemOutOfBoxExperienceState](https://docs.microsoft.com/uwp/api/windows.system.profile.systemoutofboxexperiencestate)

SystemOutOfBoxExperienceState

#### [SystemSetupInfo](https://docs.microsoft.com/uwp/api/windows.system.profile.systemsetupinfo)

SystemSetupInfo <br> SystemSetupInfo.OutOfBoxExperienceState <br> SystemSetupInfo.OutOfBoxExperienceStateChanged

#### [WindowsIntegrityPolicy](https://docs.microsoft.com/uwp/api/windows.system.profile.windowsintegritypolicy)

WindowsIntegrityPolicy <br> WindowsIntegrityPolicy.CanDisable <br> WindowsIntegrityPolicy.IsDisableSupported <br> WindowsIntegrityPolicy.IsEnabled <br> WindowsIntegrityPolicy.IsEnabledForTrial <br> WindowsIntegrityPolicy.PolicyChanged

### [Windows.System.RemoteSystems](https://docs.microsoft.com/uwp/api/windows.system.remotesystems)

#### [RemoteSystem](https://docs.microsoft.com/uwp/api/windows.system.remotesystems.remotesystem)

RemoteSystem.Apps

#### [RemoteSystemApp](https://docs.microsoft.com/uwp/api/windows.system.remotesystems.remotesystemapp)

RemoteSystemApp <br> RemoteSystemApp.Attributes <br> RemoteSystemApp.DisplayName <br> RemoteSystemApp.Id <br> RemoteSystemApp.IsAvailableByProximity <br> RemoteSystemApp.IsAvailableBySpatialProximity

#### [RemoteSystemAppRegistration](https://docs.microsoft.com/uwp/api/windows.system.remotesystems.remotesystemappregistration)

RemoteSystemAppRegistration <br> RemoteSystemAppRegistration.Attributes <br> RemoteSystemAppRegistration.GetDefault <br> RemoteSystemAppRegistration.GetForUser <br> RemoteSystemAppRegistration.SaveAsync <br> RemoteSystemAppRegistration.User

#### [RemoteSystemConnectionInfo](https://docs.microsoft.com/uwp/api/windows.system.remotesystems.remotesystemconnectioninfo)

RemoteSystemConnectionInfo <br> RemoteSystemConnectionInfo.IsProximal <br> RemoteSystemConnectionInfo.TryCreateFromAppServiceConnection

#### [RemoteSystemConnectionRequest](https://docs.microsoft.com/uwp/api/windows.system.remotesystems.remotesystemconnectionrequest)

RemoteSystemConnectionRequest.CreateForApp <br> RemoteSystemConnectionRequest.RemoteSystemApp

#### [RemoteSystemWebAccountFilter](https://docs.microsoft.com/uwp/api/windows.system.remotesystems.remotesystemwebaccountfilter)

RemoteSystemWebAccountFilter <br> RemoteSystemWebAccountFilter.Account <br> RemoteSystemWebAccountFilter.#ctor

### [Windows.System.Update](https://docs.microsoft.com/uwp/api/windows.system.update)

#### [SystemUpdateAttentionRequiredReason](https://docs.microsoft.com/uwp/api/windows.system.update.systemupdateattentionrequiredreason)

SystemUpdateAttentionRequiredReason

#### [SystemUpdateItem](https://docs.microsoft.com/uwp/api/windows.system.update.systemupdateitem)

SystemUpdateItem <br> SystemUpdateItem.Description <br> SystemUpdateItem.DownloadProgress <br> SystemUpdateItem.ExtendedError <br> SystemUpdateItem.Id <br> SystemUpdateItem.InstallProgress <br> SystemUpdateItem.Revision <br> SystemUpdateItem.State <br> SystemUpdateItem.Title

#### [SystemUpdateItemState](https://docs.microsoft.com/uwp/api/windows.system.update.systemupdateitemstate)

SystemUpdateItemState

#### [SystemUpdateLastErrorInfo](https://docs.microsoft.com/uwp/api/windows.system.update.systemupdatelasterrorinfo)

SystemUpdateLastErrorInfo <br> SystemUpdateLastErrorInfo.ExtendedError <br> SystemUpdateLastErrorInfo.IsInteractive <br> SystemUpdateLastErrorInfo.State

#### [SystemUpdateManager](https://docs.microsoft.com/uwp/api/windows.system.update.systemupdatemanager)

SystemUpdateManager <br> SystemUpdateManager.AttentionRequiredReason <br> SystemUpdateManager.BlockAutomaticRebootAsync <br> SystemUpdateManager.DownloadProgress <br> SystemUpdateManager.ExtendedError <br> SystemUpdateManager.GetAutomaticRebootBlockIds <br> SystemUpdateManager.GetFlightRing <br> SystemUpdateManager.GetUpdateItems <br> SystemUpdateManager.InstallProgress <br> SystemUpdateManager.IsSupported <br> SystemUpdateManager.LastErrorInfo <br> SystemUpdateManager.LastUpdateCheckTime <br> SystemUpdateManager.LastUpdateInstallTime <br> SystemUpdateManager.RebootToCompleteInstall <br> SystemUpdateManager.SetFlightRing <br> SystemUpdateManager.StartCancelUpdates <br> SystemUpdateManager.StartInstall <br> SystemUpdateManager.State <br> SystemUpdateManager.StateChanged <br> SystemUpdateManager.TrySetUserActiveHours <br> SystemUpdateManager.UnblockAutomaticRebootAsync <br> SystemUpdateManager.UserActiveHoursEnd <br> SystemUpdateManager.UserActiveHoursMax <br> SystemUpdateManager.UserActiveHoursStart

#### [SystemUpdateManagerState](https://docs.microsoft.com/uwp/api/windows.system.update.systemupdatemanagerstate)

SystemUpdateManagerState

#### [SystemUpdateStartInstallAction](https://docs.microsoft.com/uwp/api/windows.system.update.systemupdatestartinstallaction)

SystemUpdateStartInstallAction

### [Windows.System.UserProfile](https://docs.microsoft.com/uwp/api/windows.system.userprofile)

#### [AssignedAccessSettings](https://docs.microsoft.com/uwp/api/windows.system.userprofile.assignedaccesssettings)

AssignedAccessSettings <br> AssignedAccessSettings.GetDefault <br> AssignedAccessSettings.GetForUser <br> AssignedAccessSettings.IsEnabled <br> AssignedAccessSettings.IsSingleAppKioskMode <br> AssignedAccessSettings.User

### [Windows.System](https://docs.microsoft.com/uwp/api/windows.system)

#### [AppUriHandlerHost](https://docs.microsoft.com/uwp/api/windows.system.appurihandlerhost)

AppUriHandlerHost <br> AppUriHandlerHost.#ctor <br> AppUriHandlerHost.#ctor <br> AppUriHandlerHost.Name

#### [AppUriHandlerRegistration](https://docs.microsoft.com/uwp/api/windows.system.appurihandlerregistration)

AppUriHandlerRegistration <br> AppUriHandlerRegistration.GetAppAddedHostsAsync <br> AppUriHandlerRegistration.Name <br> AppUriHandlerRegistration.SetAppAddedHostsAsync <br> AppUriHandlerRegistration.User

#### [AppUriHandlerRegistrationManager](https://docs.microsoft.com/uwp/api/windows.system.appurihandlerregistrationmanager)

AppUriHandlerRegistrationManager <br> AppUriHandlerRegistrationManager.GetDefault <br> AppUriHandlerRegistrationManager.GetForUser <br> AppUriHandlerRegistrationManager.TryGetRegistration <br> AppUriHandlerRegistrationManager.User

#### [Launcher](https://docs.microsoft.com/uwp/api/windows.system.launcher)

Launcher.LaunchFolderPathAsync <br> Launcher.LaunchFolderPathAsync <br> Launcher.LaunchFolderPathForUserAsync <br> Launcher.LaunchFolderPathForUserAsync

## Windows.UI

### [Windows.UI.Accessibility](https://docs.microsoft.com/uwp/api/windows.ui.accessibility)

#### [ScreenReaderPositionChangedEventArgs](https://docs.microsoft.com/uwp/api/windows.ui.accessibility.screenreaderpositionchangedeventargs)

ScreenReaderPositionChangedEventArgs <br> ScreenReaderPositionChangedEventArgs.IsReadingText <br> ScreenReaderPositionChangedEventArgs.ScreenPositionInRawPixels

#### [ScreenReaderService](https://docs.microsoft.com/uwp/api/windows.ui.accessibility.screenreaderservice)

ScreenReaderService <br> ScreenReaderService.CurrentScreenReaderPosition <br> ScreenReaderService.ScreenReaderPositionChanged <br> ScreenReaderService.#ctor

### [Windows.UI.Composition.Interactions](https://docs.microsoft.com/uwp/api/windows.ui.composition.interactions)

#### [InteractionSourceConfiguration](https://docs.microsoft.com/uwp/api/windows.ui.composition.interactions.interactionsourceconfiguration)

InteractionSourceConfiguration <br> InteractionSourceConfiguration.PositionXSourceMode <br> InteractionSourceConfiguration.PositionYSourceMode <br> InteractionSourceConfiguration.ScaleSourceMode

#### [InteractionSourceRedirectionMode](https://docs.microsoft.com/uwp/api/windows.ui.composition.interactions.interactionsourceredirectionmode)

InteractionSourceRedirectionMode

#### [InteractionTracker](https://docs.microsoft.com/uwp/api/windows.ui.composition.interactions.interactiontracker)

InteractionTracker.IsInertiaFromImpulse <br> InteractionTracker.TryUpdatePosition <br> InteractionTracker.TryUpdatePositionBy

#### [InteractionTrackerClampingOption](https://docs.microsoft.com/uwp/api/windows.ui.composition.interactions.interactiontrackerclampingoption)

InteractionTrackerClampingOption

#### [InteractionTrackerInertiaStateEnteredArgs](https://docs.microsoft.com/uwp/api/windows.ui.composition.interactions.interactiontrackerinertiastateenteredargs)

InteractionTrackerInertiaStateEnteredArgs.IsInertiaFromImpulse

#### [VisualInteractionSource](https://docs.microsoft.com/uwp/api/windows.ui.composition.interactions.visualinteractionsource)

VisualInteractionSource.PointerWheelConfig

### [Windows.UI.Composition](https://docs.microsoft.com/uwp/api/windows.ui.composition)

#### [AnimationPropertyAccessMode](https://docs.microsoft.com/uwp/api/windows.ui.composition.animationpropertyaccessmode)

AnimationPropertyAccessMode

#### [AnimationPropertyInfo](https://docs.microsoft.com/uwp/api/windows.ui.composition.animationpropertyinfo)

AnimationPropertyInfo <br> AnimationPropertyInfo.AccessMode

#### [BooleanKeyFrameAnimation](https://docs.microsoft.com/uwp/api/windows.ui.composition.booleankeyframeanimation)

BooleanKeyFrameAnimation <br> BooleanKeyFrameAnimation.InsertKeyFrame

#### [CompositionAnimation](https://docs.microsoft.com/uwp/api/windows.ui.composition.compositionanimation)

CompositionAnimation.SetExpressionReferenceParameter

#### [CompositionGeometricClip](https://docs.microsoft.com/uwp/api/windows.ui.composition.compositiongeometricclip)

CompositionGeometricClip <br> CompositionGeometricClip.Geometry <br> CompositionGeometricClip.ViewBox

#### [CompositionGradientBrush](https://docs.microsoft.com/uwp/api/windows.ui.composition.compositiongradientbrush)

CompositionGradientBrush.MappingMode

#### [CompositionMappingMode](https://docs.microsoft.com/uwp/api/windows.ui.composition.compositionmappingmode)

CompositionMappingMode

#### [CompositionObject](https://docs.microsoft.com/uwp/api/windows.ui.composition.compositionobject)

CompositionObject.PopulatePropertyInfo <br> CompositionObject.StartAnimationGroupWithIAnimationObject <br> CompositionObject.StartAnimationWithIAnimationObject

#### [Compositor](https://docs.microsoft.com/uwp/api/windows.ui.composition.compositor)

Compositor.CreateBooleanKeyFrameAnimation <br> Compositor.CreateGeometricClip <br> Compositor.CreateGeometricClip <br> Compositor.CreateRedirectVisual <br> Compositor.CreateRedirectVisual

#### [IAnimationObject](https://docs.microsoft.com/uwp/api/windows.ui.composition.ianimationobject)

IAnimationObject <br> IAnimationObject.PopulatePropertyInfo

#### [RedirectVisual](https://docs.microsoft.com/uwp/api/windows.ui.composition.redirectvisual)

RedirectVisual <br> RedirectVisual.Source

### [Windows.UI.Input.Inking.Preview](https://docs.microsoft.com/uwp/api/windows.ui.input.inking.preview)

#### [PalmRejectionDelayZonePreview](https://docs.microsoft.com/uwp/api/windows.ui.input.inking.preview.palmrejectiondelayzonepreview)

PalmRejectionDelayZonePreview <br> PalmRejectionDelayZonePreview.Close <br> PalmRejectionDelayZonePreview.CreateForVisual <br> PalmRejectionDelayZonePreview.CreateForVisual

### [Windows.UI.Input.Inking](https://docs.microsoft.com/uwp/api/windows.ui.input.inking)

#### [HandwritingLineHeight](https://docs.microsoft.com/uwp/api/windows.ui.input.inking.handwritinglineheight)

HandwritingLineHeight

#### [PenAndInkSettings](https://docs.microsoft.com/uwp/api/windows.ui.input.inking.penandinksettings)

PenAndInkSettings <br> PenAndInkSettings.FontFamilyName <br> PenAndInkSettings.GetDefault <br> PenAndInkSettings.HandwritingLineHeight <br> PenAndInkSettings.IsHandwritingDirectlyIntoTextFieldEnabled <br> PenAndInkSettings.IsTouchHandwritingEnabled <br> PenAndInkSettings.PenHandedness <br> PenAndInkSettings.UserConsentsToHandwritingTelemetryCollection

#### [PenHandedness](https://docs.microsoft.com/uwp/api/windows.ui.input.inking.penhandedness)

PenHandedness

### [Windows.UI.Notifications](https://docs.microsoft.com/uwp/api/windows.ui.notifications)

#### [ScheduledToastNotificationShowingEventArgs](https://docs.microsoft.com/uwp/api/windows.ui.notifications.scheduledtoastnotificationshowingeventargs)

ScheduledToastNotificationShowingEventArgs <br> ScheduledToastNotificationShowingEventArgs.Cancel <br> ScheduledToastNotificationShowingEventArgs.GetDeferral <br> ScheduledToastNotificationShowingEventArgs.ScheduledToastNotification

#### [ToastNotifier](https://docs.microsoft.com/uwp/api/windows.ui.notifications.toastnotifier)

ToastNotifier.ScheduledToastNotificationShowing

### [Windows.UI.Shell](https://docs.microsoft.com/uwp/api/windows.ui.shell)

#### [SecurityAppKind](https://docs.microsoft.com/uwp/api/windows.ui.shell.securityappkind)

SecurityAppKind

#### [SecurityAppManager](https://docs.microsoft.com/uwp/api/windows.ui.shell.securityappmanager)

SecurityAppManager <br> SecurityAppManager.Register <br> SecurityAppManager.#ctor <br> SecurityAppManager.Unregister <br> SecurityAppManager.UpdateState

#### [SecurityAppState](https://docs.microsoft.com/uwp/api/windows.ui.shell.securityappstate)

SecurityAppState

#### [SecurityAppSubstatus](https://docs.microsoft.com/uwp/api/windows.ui.shell.securityappsubstatus)

SecurityAppSubstatus

#### [TaskbarManager](https://docs.microsoft.com/uwp/api/windows.ui.shell.taskbarmanager)

TaskbarManager.IsSecondaryTilePinnedAsync <br> TaskbarManager.RequestPinSecondaryTileAsync <br> TaskbarManager.TryUnpinSecondaryTileAsync

### [Windows.UI.StartScreen](https://docs.microsoft.com/uwp/api/windows.ui.startscreen)

#### [StartScreenManager](https://docs.microsoft.com/uwp/api/windows.ui.startscreen.startscreenmanager)

StartScreenManager.ContainsSecondaryTileAsync <br> StartScreenManager.TryRemoveSecondaryTileAsync

### [Windows.UI.Text.Core](https://docs.microsoft.com/uwp/api/windows.ui.text.core)

#### [CoreTextLayoutRequest](https://docs.microsoft.com/uwp/api/windows.ui.text.core.coretextlayoutrequest)

CoreTextLayoutRequest.LayoutBoundsVisualPixels

### [Windows.UI.Text](https://docs.microsoft.com/uwp/api/windows.ui.text)

#### [RichEditTextDocument](https://docs.microsoft.com/uwp/api/windows.ui.text.richedittextdocument)

RichEditTextDocument.ClearUndoRedoHistory

### [Windows.UI.ViewManagement.Core](https://docs.microsoft.com/uwp/api/windows.ui.viewmanagement.core)

#### [CoreInputView](https://docs.microsoft.com/uwp/api/windows.ui.viewmanagement.core.coreinputview)

CoreInputView.TryHide <br> CoreInputView.TryShow <br> CoreInputView.TryShow

#### [CoreInputViewKind](https://docs.microsoft.com/uwp/api/windows.ui.viewmanagement.core.coreinputviewkind)

CoreInputViewKind

### [Windows.UI.WebUI](https://docs.microsoft.com/uwp/api/windows.ui.webui)

#### [BackgroundActivatedEventArgs](https://docs.microsoft.com/uwp/api/windows.ui.webui.backgroundactivatedeventargs)

BackgroundActivatedEventArgs <br> BackgroundActivatedEventArgs.TaskInstance

#### [BackgroundActivatedEventHandler](https://docs.microsoft.com/uwp/api/windows.ui.webui.backgroundactivatedeventhandler)

BackgroundActivatedEventHandler

#### [NewWebUIViewCreatedEventArgs](https://docs.microsoft.com/uwp/api/windows.ui.webui.newwebuiviewcreatedeventargs)

NewWebUIViewCreatedEventArgs <br> NewWebUIViewCreatedEventArgs.ActivatedEventArgs <br> NewWebUIViewCreatedEventArgs.GetDeferral <br> NewWebUIViewCreatedEventArgs.HasPendingNavigate <br> NewWebUIViewCreatedEventArgs.WebUIView

#### [WebUIApplication](https://docs.microsoft.com/uwp/api/windows.ui.webui.webuiapplication)

WebUIApplication.BackgroundActivated <br> WebUIApplication.NewWebUIViewCreated

#### [WebUIView](https://docs.microsoft.com/uwp/api/windows.ui.webui.webuiview)

WebUIView <br> WebUIView.Activated <br> WebUIView.AddInitializeScript <br> WebUIView.ApplicationViewId <br> WebUIView.BuildLocalStreamUri <br> WebUIView.CanGoBack <br> WebUIView.CanGoForward <br> WebUIView.CapturePreviewToStreamAsync <br> WebUIView.CaptureSelectedContentToDataPackageAsync <br> WebUIView.Closed <br> WebUIView.ContainsFullScreenElement <br> WebUIView.ContainsFullScreenElementChanged <br> WebUIView.ContentLoading <br> WebUIView.CreateAsync <br> WebUIView.CreateAsync <br> WebUIView.DefaultBackgroundColor <br> WebUIView.DeferredPermissionRequests <br> WebUIView.DocumentTitle <br> WebUIView.DOMContentLoaded <br> WebUIView.FrameContentLoading <br> WebUIView.FrameDOMContentLoaded <br> WebUIView.FrameNavigationCompleted <br> WebUIView.FrameNavigationStarting <br> WebUIView.GetDeferredPermissionRequestById <br> WebUIView.GoBack <br> WebUIView.GoForward <br> WebUIView.IgnoreApplicationContentUriRulesNavigationRestrictions <br> WebUIView.InvokeScriptAsync <br> WebUIView.LongRunningScriptDetected <br> WebUIView.Navigate <br> WebUIView.NavigateToLocalStreamUri <br> WebUIView.NavigateToString <br> WebUIView.NavigateWithHttpRequestMessage <br> WebUIView.NavigationCompleted <br> WebUIView.NavigationStarting <br> WebUIView.NewWindowRequested <br> WebUIView.PermissionRequested <br> WebUIView.Refresh <br> WebUIView.ScriptNotify <br> WebUIView.Settings <br> WebUIView.Source <br> WebUIView.Stop <br> WebUIView.UnsafeContentWarningDisplaying <br> WebUIView.UnsupportedUriSchemeIdentified <br> WebUIView.UnviewableContentIdentified <br> WebUIView.WebResourceRequested

### [Windows.UI.Xaml.Automation.Peers](https://docs.microsoft.com/uwp/api/windows.ui.xaml.automation.peers)

#### [AppBarButtonAutomationPeer](https://docs.microsoft.com/uwp/api/windows.ui.xaml.automation.peers.appbarbuttonautomationpeer)

AppBarButtonAutomationPeer.Collapse <br> AppBarButtonAutomationPeer.Expand <br> AppBarButtonAutomationPeer.ExpandCollapseState

#### [AutomationPeer](https://docs.microsoft.com/uwp/api/windows.ui.xaml.automation.peers.automationpeer)

AutomationPeer.IsDialog <br> AutomationPeer.IsDialogCore

#### [MenuBarAutomationPeer](https://docs.microsoft.com/uwp/api/windows.ui.xaml.automation.peers.menubarautomationpeer)

MenuBarAutomationPeer <br> MenuBarAutomationPeer.#ctor

#### [MenuBarItemAutomationPeer](https://docs.microsoft.com/uwp/api/windows.ui.xaml.automation.peers.menubaritemautomationpeer)

MenuBarItemAutomationPeer <br> MenuBarItemAutomationPeer.Collapse <br> MenuBarItemAutomationPeer.Expand <br> MenuBarItemAutomationPeer.ExpandCollapseState <br> MenuBarItemAutomationPeer.Invoke <br> MenuBarItemAutomationPeer.#ctor

### [Windows.UI.Xaml.Automation](https://docs.microsoft.com/uwp/api/windows.ui.xaml.automation)

#### [AutomationElementIdentifiers](https://docs.microsoft.com/uwp/api/windows.ui.xaml.automation.automationelementidentifiers)

AutomationElementIdentifiers.IsDialogProperty

#### [AutomationProperties](https://docs.microsoft.com/uwp/api/windows.ui.xaml.automation.automationproperties)

AutomationProperties.GetIsDialog <br> AutomationProperties.IsDialogProperty <br> AutomationProperties.SetIsDialog

### [Windows.UI.Xaml.Controls.Maps](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.maps)

#### [MapTileAnimationState](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.maps.maptileanimationstate)

MapTileAnimationState

#### [MapTileBitmapRequestedEventArgs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.maps.maptilebitmaprequestedeventargs)

MapTileBitmapRequestedEventArgs.FrameIndex

#### [MapTileSource](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.maps.maptilesource)

MapTileSource.AnimationState <br> MapTileSource.AnimationStateProperty <br> MapTileSource.AutoPlay <br> MapTileSource.AutoPlayProperty <br> MapTileSource.FrameCount <br> MapTileSource.FrameCountProperty <br> MapTileSource.FrameDuration <br> MapTileSource.FrameDurationProperty <br> MapTileSource.Pause <br> MapTileSource.Play <br> MapTileSource.Stop

#### [MapTileUriRequestedEventArgs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.maps.maptileurirequestedeventargs)

MapTileUriRequestedEventArgs.FrameIndex

### [Windows.UI.Xaml.Controls.Primitives](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.primitives)

#### [CommandBarFlyoutCommandBar](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.primitives.commandbarflyoutcommandbar)

CommandBarFlyoutCommandBar <br> CommandBarFlyoutCommandBar.#ctor <br> CommandBarFlyoutCommandBar.FlyoutTemplateSettings

#### [CommandBarFlyoutCommandBarTemplateSettings](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.primitives.commandbarflyoutcommandbartemplatesettings)

CommandBarFlyoutCommandBarTemplateSettings <br> CommandBarFlyoutCommandBarTemplateSettings.CloseAnimationEndPosition <br> CommandBarFlyoutCommandBarTemplateSettings.ContentClipRect <br> CommandBarFlyoutCommandBarTemplateSettings.CurrentWidth <br> CommandBarFlyoutCommandBarTemplateSettings.ExpandDownAnimationEndPosition <br> CommandBarFlyoutCommandBarTemplateSettings.ExpandDownAnimationHoldPosition <br> CommandBarFlyoutCommandBarTemplateSettings.ExpandDownAnimationStartPosition <br> CommandBarFlyoutCommandBarTemplateSettings.ExpandDownOverflowVerticalPosition <br> CommandBarFlyoutCommandBarTemplateSettings.ExpandedWidth <br> CommandBarFlyoutCommandBarTemplateSettings.ExpandUpAnimationEndPosition <br> CommandBarFlyoutCommandBarTemplateSettings.ExpandUpAnimationHoldPosition <br> CommandBarFlyoutCommandBarTemplateSettings.ExpandUpAnimationStartPosition <br> CommandBarFlyoutCommandBarTemplateSettings.ExpandUpOverflowVerticalPosition <br> CommandBarFlyoutCommandBarTemplateSettings.OpenAnimationEndPosition <br> CommandBarFlyoutCommandBarTemplateSettings.OpenAnimationStartPosition <br> CommandBarFlyoutCommandBarTemplateSettings.OverflowContentClipRect <br> CommandBarFlyoutCommandBarTemplateSettings.WidthExpansionAnimationEndPosition <br> CommandBarFlyoutCommandBarTemplateSettings.WidthExpansionAnimationStartPosition <br> CommandBarFlyoutCommandBarTemplateSettings.WidthExpansionDelta <br> CommandBarFlyoutCommandBarTemplateSettings.WidthExpansionMoreButtonAnimationEndPosition <br> CommandBarFlyoutCommandBarTemplateSettings.WidthExpansionMoreButtonAnimationStartPosition

#### [FlyoutBase](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.primitives.flyoutbase)

FlyoutBase.AreOpenCloseAnimationsEnabled <br> FlyoutBase.AreOpenCloseAnimationsEnabledProperty <br> FlyoutBase.InputDevicePrefersPrimaryCommands <br> FlyoutBase.InputDevicePrefersPrimaryCommandsProperty <br> FlyoutBase.IsOpen <br> FlyoutBase.IsOpenProperty <br> FlyoutBase.ShowAt <br> FlyoutBase.ShowMode <br> FlyoutBase.ShowModeProperty <br> FlyoutBase.TargetProperty

#### [FlyoutShowMode](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.primitives.flyoutshowmode)

FlyoutShowMode

#### [FlyoutShowOptions](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.primitives.flyoutshowoptions)

FlyoutShowOptions <br> FlyoutShowOptions.ExclusionRect <br> FlyoutShowOptions.#ctor <br> FlyoutShowOptions.Placement <br> FlyoutShowOptions.Position <br> FlyoutShowOptions.ShowMode

#### [NavigationViewItemPresenter](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.primitives.navigationviewitempresenter)

NavigationViewItemPresenter <br> NavigationViewItemPresenter.Icon <br> NavigationViewItemPresenter.IconProperty <br> NavigationViewItemPresenter.#ctor

### [Windows.UI.Xaml.Controls](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls)

#### [AnchorRequestedEventArgs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.anchorrequestedeventargs)

AnchorRequestedEventArgs <br> AnchorRequestedEventArgs.Anchor <br> AnchorRequestedEventArgs.AnchorCandidates

#### [AppBarElementContainer](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.appbarelementcontainer)

AppBarElementContainer <br> AppBarElementContainer.#ctor <br> AppBarElementContainer.DynamicOverflowOrder <br> AppBarElementContainer.DynamicOverflowOrderProperty <br> AppBarElementContainer.IsCompact <br> AppBarElementContainer.IsCompactProperty <br> AppBarElementContainer.IsInOverflow <br> AppBarElementContainer.IsInOverflowProperty

#### [AutoSuggestBox](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.autosuggestbox)

AutoSuggestBox.Description <br> AutoSuggestBox.DescriptionProperty

#### [BackgroundSizing](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.backgroundsizing)

BackgroundSizing

#### [Border](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.border)

Border.BackgroundSizing <br> Border.BackgroundSizingProperty <br> Border.BackgroundTransition

#### [CalendarDatePicker](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.calendardatepicker)

CalendarDatePicker.Description <br> CalendarDatePicker.DescriptionProperty

#### [ComboBox](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.combobox)

ComboBox.Description <br> ComboBox.DescriptionProperty <br> ComboBox.IsEditableProperty <br> ComboBox.Text <br> ComboBox.TextBoxStyle <br> ComboBox.TextBoxStyleProperty <br> ComboBox.TextProperty <br> ComboBox.TextSubmitted

#### [ComboBoxTextSubmittedEventArgs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.comboboxtextsubmittedeventargs)

ComboBoxTextSubmittedEventArgs <br> ComboBoxTextSubmittedEventArgs.Handled <br> ComboBoxTextSubmittedEventArgs.Text

#### [CommandBarFlyout](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.commandbarflyout)

CommandBarFlyout <br> CommandBarFlyout.#ctor <br> CommandBarFlyout.PrimaryCommands <br> CommandBarFlyout.SecondaryCommands

#### [ContentPresenter](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.contentpresenter)

ContentPresenter.BackgroundSizing <br> ContentPresenter.BackgroundSizingProperty <br> ContentPresenter.BackgroundTransition

#### [Control](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.control)

Control.BackgroundSizing <br> Control.BackgroundSizingProperty <br> Control.CornerRadius <br> Control.CornerRadiusProperty

#### [DataTemplateSelector](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.datatemplateselector)

DataTemplateSelector.GetElement <br> DataTemplateSelector.RecycleElement

#### [DatePicker](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.datepicker)

DatePicker.SelectedDate <br> DatePicker.SelectedDateChanged <br> DatePicker.SelectedDateProperty

#### [DatePickerSelectedValueChangedEventArgs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.datepickerselectedvaluechangedeventargs)

DatePickerSelectedValueChangedEventArgs <br> DatePickerSelectedValueChangedEventArgs.NewDate <br> DatePickerSelectedValueChangedEventArgs.OldDate

#### [DropDownButton](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.dropdownbutton)

DropDownButton <br> DropDownButton.#ctor

#### [DropDownButtonAutomationPeer](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.dropdownbuttonautomationpeer)

DropDownButtonAutomationPeer <br> DropDownButtonAutomationPeer.Collapse <br> DropDownButtonAutomationPeer.#ctor <br> DropDownButtonAutomationPeer.Expand <br> DropDownButtonAutomationPeer.ExpandCollapseState

#### [Frame](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.frame)

Frame.IsNavigationStackEnabled <br> Frame.IsNavigationStackEnabledProperty <br> Frame.NavigateToType

#### [Grid](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.grid)

Grid.BackgroundSizing <br> Grid.BackgroundSizingProperty

#### [IconSourceElement](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.iconsourceelement)

IconSourceElement <br> IconSourceElement.IconSource <br> IconSourceElement.#ctor <br> IconSourceElement.IconSourceProperty

#### [IScrollAnchorProvider](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.iscrollanchorprovider)

IScrollAnchorProvider <br> IScrollAnchorProvider.CurrentAnchor <br> IScrollAnchorProvider.RegisterAnchorCandidate <br> IScrollAnchorProvider.UnregisterAnchorCandidate

#### [MenuBar](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.menubar)

MenuBar <br> MenuBar.Items <br> MenuBar.ItemsProperty <br> MenuBar.#ctor

#### [MenuBarItem](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.menubaritem)

MenuBarItem <br> MenuBarItem.Items <br> MenuBarItem.ItemsProperty <br> MenuBarItem.#ctor <br> MenuBarItem.Title <br> MenuBarItem.TitleProperty

#### [MenuBarItemFlyout](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.menubaritemflyout)

MenuBarItemFlyout <br> MenuBarItemFlyout.#ctor

#### [NavigationView](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.navigationview)

NavigationView.ContentOverlay <br> NavigationView.ContentOverlayProperty <br> NavigationView.IsPaneVisible <br> NavigationView.IsPaneVisibleProperty <br> NavigationView.OverflowLabelMode <br> NavigationView.OverflowLabelModeProperty <br> NavigationView.PaneCustomContent <br> NavigationView.PaneCustomContentProperty <br> NavigationView.PaneDisplayMode <br> NavigationView.PaneDisplayModeProperty <br> NavigationView.PaneHeader <br> NavigationView.PaneHeaderProperty <br> NavigationView.SelectionFollowsFocus <br> NavigationView.SelectionFollowsFocusProperty <br> NavigationView.ShoulderNavigationEnabled <br> NavigationView.ShoulderNavigationEnabledProperty <br> NavigationView.TemplateSettings <br> NavigationView.TemplateSettingsProperty

#### [NavigationViewItem](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.navigationviewitem)

NavigationViewItem.SelectsOnInvoked <br> NavigationViewItem.SelectsOnInvokedProperty

#### [NavigationViewItemInvokedEventArgs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.navigationviewiteminvokedeventargs)

NavigationViewItemInvokedEventArgs.InvokedItemContainer <br> NavigationViewItemInvokedEventArgs.RecommendedNavigationTransitionInfo

#### [NavigationViewOverflowLabelMode](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.navigationviewoverflowlabelmode)

NavigationViewOverflowLabelMode

#### [NavigationViewPaneDisplayMode](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.navigationviewpanedisplaymode)

NavigationViewPaneDisplayMode

#### [NavigationViewSelectionChangedEventArgs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.navigationviewselectionchangedeventargs)

NavigationViewSelectionChangedEventArgs.RecommendedNavigationTransitionInfo <br> NavigationViewSelectionChangedEventArgs.SelectedItemContainer

#### [NavigationViewSelectionFollowsFocus](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.navigationviewselectionfollowsfocus)

NavigationViewSelectionFollowsFocus

#### [NavigationViewShoulderNavigationEnabled](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.navigationviewshouldernavigationenabled)

NavigationViewShoulderNavigationEnabled

#### [NavigationViewTemplateSettings](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.navigationviewtemplatesettings)

NavigationViewTemplateSettings <br> NavigationViewTemplateSettings.BackButtonVisibility <br> NavigationViewTemplateSettings.BackButtonVisibilityProperty <br> NavigationViewTemplateSettings.LeftPaneVisibility <br> NavigationViewTemplateSettings.LeftPaneVisibilityProperty <br> NavigationViewTemplateSettings.#ctor <br> NavigationViewTemplateSettings.OverflowButtonVisibility <br> NavigationViewTemplateSettings.OverflowButtonVisibilityProperty <br> NavigationViewTemplateSettings.PaneToggleButtonVisibility <br> NavigationViewTemplateSettings.PaneToggleButtonVisibilityProperty <br> NavigationViewTemplateSettings.SingleSelectionFollowsFocus <br> NavigationViewTemplateSettings.SingleSelectionFollowsFocusProperty <br> NavigationViewTemplateSettings.TopPadding <br> NavigationViewTemplateSettings.TopPaddingProperty <br> NavigationViewTemplateSettings.TopPaneVisibility <br> NavigationViewTemplateSettings.TopPaneVisibilityProperty

#### [Panel](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.panel)

Panel.BackgroundTransition

#### [PasswordBox](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.passwordbox)

PasswordBox.CanPasteClipboardContent <br> PasswordBox.CanPasteClipboardContentProperty <br> PasswordBox.Description <br> PasswordBox.DescriptionProperty <br> PasswordBox.PasteFromClipboard <br> PasswordBox.SelectionFlyout <br> PasswordBox.SelectionFlyoutProperty

#### [RelativePanel](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.relativepanel)

RelativePanel.BackgroundSizing <br> RelativePanel.BackgroundSizingProperty

#### [RichEditBox](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.richeditbox)

RichEditBox.Description <br> RichEditBox.DescriptionProperty <br> RichEditBox.ProofingMenuFlyout <br> RichEditBox.ProofingMenuFlyoutProperty <br> RichEditBox.SelectionChanging <br> RichEditBox.SelectionFlyout <br> RichEditBox.SelectionFlyoutProperty <br> RichEditBox.TextDocument

#### [RichEditBoxSelectionChangingEventArgs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.richeditboxselectionchangingeventargs)

RichEditBoxSelectionChangingEventArgs <br> RichEditBoxSelectionChangingEventArgs.Cancel <br> RichEditBoxSelectionChangingEventArgs.SelectionLength <br> RichEditBoxSelectionChangingEventArgs.SelectionStart

#### [RichTextBlock](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.richtextblock)

RichTextBlock.CopySelectionToClipboard <br> RichTextBlock.SelectionFlyout <br> RichTextBlock.SelectionFlyoutProperty

#### [ScrollContentPresenter](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.scrollcontentpresenter)

ScrollContentPresenter.CanContentRenderOutsideBounds <br> ScrollContentPresenter.CanContentRenderOutsideBoundsProperty <br> ScrollContentPresenter.SizesContentToTemplatedParent <br> ScrollContentPresenter.SizesContentToTemplatedParentProperty

#### [ScrollViewer](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.scrollviewer)

ScrollViewer.AnchorRequested <br> ScrollViewer.CanContentRenderOutsideBounds <br> ScrollViewer.CanContentRenderOutsideBoundsProperty <br> ScrollViewer.CurrentAnchor <br> ScrollViewer.GetCanContentRenderOutsideBounds <br> ScrollViewer.HorizontalAnchorRatio <br> ScrollViewer.HorizontalAnchorRatioProperty <br> ScrollViewer.ReduceViewportForCoreInputViewOcclusions <br> ScrollViewer.ReduceViewportForCoreInputViewOcclusionsProperty <br> ScrollViewer.RegisterAnchorCandidate <br> ScrollViewer.SetCanContentRenderOutsideBounds <br> ScrollViewer.UnregisterAnchorCandidate <br> ScrollViewer.VerticalAnchorRatio <br> ScrollViewer.VerticalAnchorRatioProperty

#### [SplitButton](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.splitbutton)

SplitButton <br> SplitButton.Click <br> SplitButton.Command <br> SplitButton.CommandParameter <br> SplitButton.CommandParameterProperty <br> SplitButton.CommandProperty <br> SplitButton.Flyout <br> SplitButton.FlyoutProperty <br> SplitButton.#ctor

#### [SplitButtonAutomationPeer](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.splitbuttonautomationpeer)

SplitButtonAutomationPeer <br> SplitButtonAutomationPeer.Collapse <br> SplitButtonAutomationPeer.Expand <br> SplitButtonAutomationPeer.ExpandCollapseState <br> SplitButtonAutomationPeer.Invoke <br> SplitButtonAutomationPeer.#ctor

#### [SplitButtonClickEventArgs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.splitbuttonclickeventargs)

SplitButtonClickEventArgs

#### [StackPanel](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.stackpanel)

StackPanel.BackgroundSizing <br> StackPanel.BackgroundSizingProperty

#### [TextBlock](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.textblock)

TextBlock.CopySelectionToClipboard <br> TextBlock.SelectionFlyout <br> TextBlock.SelectionFlyoutProperty

#### [TextBox](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.textbox)

TextBox.CanPasteClipboardContent <br> TextBox.CanPasteClipboardContentProperty <br> TextBox.CanRedo <br> TextBox.CanRedoProperty <br> TextBox.CanUndo <br> TextBox.CanUndoProperty <br> TextBox.ClearUndoRedoHistory <br> TextBox.CopySelectionToClipboard <br> TextBox.CutSelectionToClipboard <br> TextBox.Description <br> TextBox.DescriptionProperty <br> TextBox.PasteFromClipboard <br> TextBox.ProofingMenuFlyout <br> TextBox.ProofingMenuFlyoutProperty <br> TextBox.Redo <br> TextBox.SelectionChanging <br> TextBox.SelectionFlyout <br> TextBox.SelectionFlyoutProperty <br> TextBox.Undo

#### [TextBoxSelectionChangingEventArgs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.textboxselectionchangingeventargs)

TextBoxSelectionChangingEventArgs <br> TextBoxSelectionChangingEventArgs.Cancel <br> TextBoxSelectionChangingEventArgs.SelectionLength <br> TextBoxSelectionChangingEventArgs.SelectionStart

#### [TextCommandBarFlyout](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.textcommandbarflyout)

TextCommandBarFlyout <br> TextCommandBarFlyout.#ctor

#### [TimePicker](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.timepicker)

TimePicker.SelectedTime <br> TimePicker.SelectedTimeChanged <br> TimePicker.SelectedTimeProperty

#### [TimePickerSelectedValueChangedEventArgs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.timepickerselectedvaluechangedeventargs)

TimePickerSelectedValueChangedEventArgs <br> TimePickerSelectedValueChangedEventArgs.NewTime <br> TimePickerSelectedValueChangedEventArgs.OldTime

#### [ToggleSplitButton](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.togglesplitbutton)

ToggleSplitButton <br> ToggleSplitButton.IsChecked <br> ToggleSplitButton.IsCheckedChanged <br> ToggleSplitButton.#ctor

#### [ToggleSplitButtonAutomationPeer](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.togglesplitbuttonautomationpeer)

ToggleSplitButtonAutomationPeer <br> ToggleSplitButtonAutomationPeer.Collapse <br> ToggleSplitButtonAutomationPeer.Expand <br> ToggleSplitButtonAutomationPeer.ExpandCollapseState <br> ToggleSplitButtonAutomationPeer.Toggle <br> ToggleSplitButtonAutomationPeer.#ctor <br> ToggleSplitButtonAutomationPeer.ToggleState

#### [ToggleSplitButtonIsCheckedChangedEventArgs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.togglesplitbuttonischeckedchangedeventargs)

ToggleSplitButtonIsCheckedChangedEventArgs

#### [ToolTip](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.tooltip)

ToolTip.PlacementRect <br> ToolTip.PlacementRectProperty

#### [TreeView](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.treeview)

TreeView.CanDragItems <br> TreeView.CanDragItemsProperty <br> TreeView.CanReorderItems <br> TreeView.CanReorderItemsProperty <br> TreeView.ContainerFromItem <br> TreeView.ContainerFromNode <br> TreeView.DragItemsCompleted <br> TreeView.DragItemsStarting <br> TreeView.ItemContainerStyle <br> TreeView.ItemContainerStyleProperty <br> TreeView.ItemContainerStyleSelector <br> TreeView.ItemContainerStyleSelectorProperty <br> TreeView.ItemContainerTransitions <br> TreeView.ItemContainerTransitionsProperty <br> TreeView.ItemFromContainer <br> TreeView.ItemsSource <br> TreeView.ItemsSourceProperty <br> TreeView.ItemTemplate <br> TreeView.ItemTemplateProperty <br> TreeView.ItemTemplateSelector <br> TreeView.ItemTemplateSelectorProperty <br> TreeView.NodeFromContainer

#### [TreeViewCollapsedEventArgs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.treeviewcollapsedeventargs)

TreeViewCollapsedEventArgs.Item

#### [TreeViewDragItemsCompletedEventArgs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.treeviewdragitemscompletedeventargs)

TreeViewDragItemsCompletedEventArgs <br> TreeViewDragItemsCompletedEventArgs.DropResult <br> TreeViewDragItemsCompletedEventArgs.Items

#### [TreeViewDragItemsStartingEventArgs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.treeviewdragitemsstartingeventargs)

TreeViewDragItemsStartingEventArgs <br> TreeViewDragItemsStartingEventArgs.Cancel <br> TreeViewDragItemsStartingEventArgs.Data <br> TreeViewDragItemsStartingEventArgs.Items

#### [TreeViewExpandingEventArgs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.treeviewexpandingeventargs)

TreeViewExpandingEventArgs.Item

#### [TreeViewItem](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.treeviewitem)

TreeViewItem.HasUnrealizedChildren <br> TreeViewItem.HasUnrealizedChildrenProperty <br> TreeViewItem.ItemsSource <br> TreeViewItem.ItemsSourceProperty

#### [WebView](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.webview)

WebView.WebResourceRequested

#### [WebViewWebResourceRequestedEventArgs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.webviewwebresourcerequestedeventargs)

WebViewWebResourceRequestedEventArgs <br> WebViewWebResourceRequestedEventArgs.GetDeferral <br> WebViewWebResourceRequestedEventArgs.Request <br> WebViewWebResourceRequestedEventArgs.Response

### [Windows.UI.Xaml.Core.Direct](https://docs.microsoft.com/uwp/api/windows.ui.xaml.core.direct)

#### [IXamlDirectObject](https://docs.microsoft.com/uwp/api/windows.ui.xaml.core.direct.ixamldirectobject)

IXamlDirectObject

### [Windows.UI.Xaml.Core.Direct](https://docs.microsoft.com/uwp/api/windows.ui.xaml.core.direct)

#### [XamlDirect](https://docs.microsoft.com/uwp/api/windows.ui.xaml.core.direct.xamldirect)

XamlDirect <br> XamlDirect.AddEventHandler <br> XamlDirect.AddEventHandler <br> XamlDirect.AddToCollection <br> XamlDirect.ClearCollection <br> XamlDirect.ClearProperty <br> XamlDirect.CreateInstance <br> XamlDirect.GetBooleanProperty <br> XamlDirect.GetCollectionCount <br> XamlDirect.GetColorProperty <br> XamlDirect.GetCornerRadiusProperty <br> XamlDirect.GetDateTimeProperty <br> XamlDirect.GetDefault <br> XamlDirect.GetDoubleProperty <br> XamlDirect.GetDurationProperty <br> XamlDirect.GetEnumProperty <br> XamlDirect.GetGridLengthProperty <br> XamlDirect.GetInt32Property <br> XamlDirect.GetMatrix3DProperty <br> XamlDirect.GetMatrixProperty <br> XamlDirect.GetObject <br> XamlDirect.GetObjectProperty <br> XamlDirect.GetPointProperty <br> XamlDirect.GetRectProperty <br> XamlDirect.GetSizeProperty <br> XamlDirect.GetStringProperty <br> XamlDirect.GetThicknessProperty <br> XamlDirect.GetTimeSpanProperty <br> XamlDirect.GetXamlDirectObject <br> XamlDirect.GetXamlDirectObjectFromCollectionAt <br> XamlDirect.GetXamlDirectObjectProperty <br> XamlDirect.InsertIntoCollectionAt <br> XamlDirect.RemoveEventHandler <br> XamlDirect.RemoveFromCollection <br> XamlDirect.RemoveFromCollectionAt <br> XamlDirect.SetBooleanProperty <br> XamlDirect.SetColorProperty <br> XamlDirect.SetCornerRadiusProperty <br> XamlDirect.SetDateTimeProperty <br> XamlDirect.SetDoubleProperty <br> XamlDirect.SetDurationProperty <br> XamlDirect.SetEnumProperty <br> XamlDirect.SetGridLengthProperty <br> XamlDirect.SetInt32Property <br> XamlDirect.SetMatrix3DProperty <br> XamlDirect.SetMatrixProperty <br> XamlDirect.SetObjectProperty <br> XamlDirect.SetPointProperty <br> XamlDirect.SetRectProperty <br> XamlDirect.SetSizeProperty <br> XamlDirect.SetStringProperty <br> XamlDirect.SetThicknessProperty <br> XamlDirect.SetTimeSpanProperty <br> XamlDirect.SetXamlDirectObjectProperty

#### [XamlEventIndex](https://docs.microsoft.com/uwp/api/windows.ui.xaml.core.direct.xamleventindex)

XamlEventIndex

#### [XamlPropertyIndex](https://docs.microsoft.com/uwp/api/windows.ui.xaml.core.direct.xamlpropertyindex)

XamlPropertyIndex

#### [XamlTypeIndex](https://docs.microsoft.com/uwp/api/windows.ui.xaml.core.direct.xamltypeindex)

XamlTypeIndex

### [Windows.UI.Xaml.Hosting](https://docs.microsoft.com/uwp/api/windows.ui.xaml.hosting)

#### [DesktopWindowXamlSource](https://docs.microsoft.com/uwp/api/windows.ui.xaml.hosting.desktopwindowxamlsource)

DesktopWindowXamlSource <br> DesktopWindowXamlSource.Close <br> DesktopWindowXamlSource.Content <br> DesktopWindowXamlSource.#ctor <br> DesktopWindowXamlSource.GotFocus <br> DesktopWindowXamlSource.HasFocus <br> DesktopWindowXamlSource.NavigateFocus <br> DesktopWindowXamlSource.TakeFocusRequested

#### [DesktopWindowXamlSourceGotFocusEventArgs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.hosting.desktopwindowxamlsourcegotfocuseventargs)

DesktopWindowXamlSourceGotFocusEventArgs <br> DesktopWindowXamlSourceGotFocusEventArgs.Request

#### [DesktopWindowXamlSourceTakeFocusRequestedEventArgs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.hosting.desktopwindowxamlsourcetakefocusrequestedeventargs)

DesktopWindowXamlSourceTakeFocusRequestedEventArgs <br> DesktopWindowXamlSourceTakeFocusRequestedEventArgs.Request

#### [WindowsXamlManager](https://docs.microsoft.com/uwp/api/windows.ui.xaml.hosting.windowsxamlmanager)

WindowsXamlManager <br> WindowsXamlManager.Close <br> WindowsXamlManager.InitializeForCurrentThread

#### [XamlSourceFocusNavigationReason](https://docs.microsoft.com/uwp/api/windows.ui.xaml.hosting.xamlsourcefocusnavigationreason)

XamlSourceFocusNavigationReason

#### [XamlSourceFocusNavigationRequest](https://docs.microsoft.com/uwp/api/windows.ui.xaml.hosting.xamlsourcefocusnavigationrequest)

XamlSourceFocusNavigationRequest <br> XamlSourceFocusNavigationRequest.CorrelationId <br> XamlSourceFocusNavigationRequest.HintRect <br> XamlSourceFocusNavigationRequest.Reason <br> XamlSourceFocusNavigationRequest.#ctor <br> XamlSourceFocusNavigationRequest.#ctor <br> XamlSourceFocusNavigationRequest.#ctor

#### [XamlSourceFocusNavigationResult](https://docs.microsoft.com/uwp/api/windows.ui.xaml.hosting.xamlsourcefocusnavigationresult)

XamlSourceFocusNavigationResult <br> XamlSourceFocusNavigationResult.WasFocusMoved <br> XamlSourceFocusNavigationResult.#ctor

### [Windows.UI.Xaml.Input](https://docs.microsoft.com/uwp/api/windows.ui.xaml.input)

#### [CanExecuteRequestedEventArgs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.input.canexecuterequestedeventargs)

CanExecuteRequestedEventArgs <br> CanExecuteRequestedEventArgs.CanExecute <br> CanExecuteRequestedEventArgs.Parameter

#### [ExecuteRequestedEventArgs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.input.executerequestedeventargs)

ExecuteRequestedEventArgs <br> ExecuteRequestedEventArgs.Parameter

#### [FocusManager](https://docs.microsoft.com/uwp/api/windows.ui.xaml.input.focusmanager)

FocusManager.GettingFocus <br> FocusManager.GotFocus <br> FocusManager.LosingFocus <br> FocusManager.LostFocus

#### [FocusManagerGotFocusEventArgs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.input.focusmanagergotfocuseventargs)

FocusManagerGotFocusEventArgs <br> FocusManagerGotFocusEventArgs.CorrelationId <br> FocusManagerGotFocusEventArgs.NewFocusedElement

#### [FocusManagerLostFocusEventArgs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.input.focusmanagerlostfocuseventargs)

FocusManagerLostFocusEventArgs <br> FocusManagerLostFocusEventArgs.CorrelationId <br> FocusManagerLostFocusEventArgs.OldFocusedElement

#### [GettingFocusEventArgs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.input.gettingfocuseventargs)

GettingFocusEventArgs.CorrelationId

#### [LosingFocusEventArgs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.input.losingfocuseventargs)

LosingFocusEventArgs.CorrelationId

#### [StandardUICommand](https://docs.microsoft.com/uwp/api/windows.ui.xaml.input.standarduicommand)

StandardUICommand <br> StandardUICommand.Kind <br> StandardUICommand.KindProperty <br> StandardUICommand.#ctor <br> StandardUICommand.#ctor

#### [StandardUICommandKind](https://docs.microsoft.com/uwp/api/windows.ui.xaml.input.standarduicommandkind)

StandardUICommandKind

#### [XamlUICommand](https://docs.microsoft.com/uwp/api/windows.ui.xaml.input.xamluicommand)

XamlUICommand <br> XamlUICommand.AccessKey <br> XamlUICommand.AccessKeyProperty <br> XamlUICommand.CanExecute <br> XamlUICommand.CanExecuteChanged <br> XamlUICommand.CanExecuteRequested <br> XamlUICommand.Command <br> XamlUICommand.CommandProperty <br> XamlUICommand.Description <br> XamlUICommand.DescriptionProperty <br> XamlUICommand.Execute <br> XamlUICommand.ExecuteRequested <br> XamlUICommand.IconSource <br> XamlUICommand.IconSourceProperty <br> XamlUICommand.KeyboardAccelerators <br> XamlUICommand.KeyboardAcceleratorsProperty <br> XamlUICommand.Label <br> XamlUICommand.LabelProperty <br> XamlUICommand.NotifyCanExecuteChanged <br> XamlUICommand.#ctor

### [Windows.UI.Xaml.Markup](https://docs.microsoft.com/uwp/api/windows.ui.xaml.markup)

#### [FullXamlMetadataProviderAttribute](https://docs.microsoft.com/uwp/api/windows.ui.xaml.markup.fullxamlmetadataproviderattribute)

FullXamlMetadataProviderAttribute <br> FullXamlMetadataProviderAttribute.#ctor

#### [IXamlBindScopeDiagnostics](https://docs.microsoft.com/uwp/api/windows.ui.xaml.markup.ixamlbindscopediagnostics)

IXamlBindScopeDiagnostics <br> IXamlBindScopeDiagnostics.Disable

#### [IXamlType2](https://docs.microsoft.com/uwp/api/windows.ui.xaml.markup.ixamltype2)

IXamlType2 <br> IXamlType2.BoxedType

### [Windows.UI.Xaml.Media.Animation](https://docs.microsoft.com/uwp/api/windows.ui.xaml.media.animation)

#### [BasicConnectedAnimationConfiguration](https://docs.microsoft.com/uwp/api/windows.ui.xaml.media.animation.basicconnectedanimationconfiguration)

BasicConnectedAnimationConfiguration <br> BasicConnectedAnimationConfiguration.#ctor

#### [ConnectedAnimation](https://docs.microsoft.com/uwp/api/windows.ui.xaml.media.animation.connectedanimation)

ConnectedAnimation.Configuration

#### [ConnectedAnimationConfiguration](https://docs.microsoft.com/uwp/api/windows.ui.xaml.media.animation.connectedanimationconfiguration)

ConnectedAnimationConfiguration

#### [DirectConnectedAnimationConfiguration](https://docs.microsoft.com/uwp/api/windows.ui.xaml.media.animation.directconnectedanimationconfiguration)

DirectConnectedAnimationConfiguration <br> DirectConnectedAnimationConfiguration.#ctor

#### [GravityConnectedAnimationConfiguration](https://docs.microsoft.com/uwp/api/windows.ui.xaml.media.animation.gravityconnectedanimationconfiguration)

GravityConnectedAnimationConfiguration <br> GravityConnectedAnimationConfiguration.#ctor

#### [SlideNavigationTransitionEffect](https://docs.microsoft.com/uwp/api/windows.ui.xaml.media.animation.slidenavigationtransitioneffect)

SlideNavigationTransitionEffect

#### [SlideNavigationTransitionInfo](https://docs.microsoft.com/uwp/api/windows.ui.xaml.media.animation.slidenavigationtransitioninfo)

SlideNavigationTransitionInfo.Effect <br> SlideNavigationTransitionInfo.EffectProperty

### [Windows.UI.Xaml.Media](https://docs.microsoft.com/uwp/api/windows.ui.xaml.media)

#### [Brush](https://docs.microsoft.com/uwp/api/windows.ui.xaml.media.brush)

Brush.PopulatePropertyInfo <br> Brush.PopulatePropertyInfoOverride

### [Windows.UI.Xaml.Navigation](https://docs.microsoft.com/uwp/api/windows.ui.xaml.navigation)

#### [FrameNavigationOptions](https://docs.microsoft.com/uwp/api/windows.ui.xaml.navigation.framenavigationoptions)

FrameNavigationOptions <br> FrameNavigationOptions.#ctor <br> FrameNavigationOptions.IsNavigationStackEnabled <br> FrameNavigationOptions.TransitionInfoOverride

### [Windows.UI.Xaml](https://docs.microsoft.com/uwp/api/windows.ui.xaml)

#### [BrushTransition](https://docs.microsoft.com/uwp/api/windows.ui.xaml.brushtransition)

BrushTransition <br> BrushTransition.#ctor <br> BrushTransition.Duration

#### [ColorPaletteResources](https://docs.microsoft.com/uwp/api/windows.ui.xaml.colorpaletteresources)

ColorPaletteResources <br> ColorPaletteResources.Accent <br> ColorPaletteResources.AltHigh <br> ColorPaletteResources.AltLow <br> ColorPaletteResources.AltMedium <br> ColorPaletteResources.AltMediumHigh <br> ColorPaletteResources.AltMediumLow <br> ColorPaletteResources.BaseHigh <br> ColorPaletteResources.BaseLow <br> ColorPaletteResources.BaseMedium <br> ColorPaletteResources.BaseMediumHigh <br> ColorPaletteResources.BaseMediumLow <br> ColorPaletteResources.ChromeAltLow <br> ColorPaletteResources.ChromeBlackHigh <br> ColorPaletteResources.ChromeBlackLow <br> ColorPaletteResources.ChromeBlackMedium <br> ColorPaletteResources.ChromeBlackMediumLow <br> ColorPaletteResources.ChromeDisabledHigh <br> ColorPaletteResources.ChromeDisabledLow <br> ColorPaletteResources.ChromeGray <br> ColorPaletteResources.ChromeHigh <br> ColorPaletteResources.ChromeLow <br> ColorPaletteResources.ChromeMedium <br> ColorPaletteResources.ChromeMediumLow <br> ColorPaletteResources.ChromeWhite <br> ColorPaletteResources.#ctor <br> ColorPaletteResources.ErrorText <br> ColorPaletteResources.ListLow <br> ColorPaletteResources.ListMedium

#### [DataTemplate](https://docs.microsoft.com/uwp/api/windows.ui.xaml.datatemplate)

DataTemplate.GetElement <br> DataTemplate.RecycleElement

#### [DebugSettings](https://docs.microsoft.com/uwp/api/windows.ui.xaml.debugsettings)

DebugSettings.FailFastOnErrors

#### [EffectiveViewportChangedEventArgs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.effectiveviewportchangedeventargs)

EffectiveViewportChangedEventArgs <br> EffectiveViewportChangedEventArgs.BringIntoViewDistanceX <br> EffectiveViewportChangedEventArgs.BringIntoViewDistanceY <br> EffectiveViewportChangedEventArgs.EffectiveViewport <br> EffectiveViewportChangedEventArgs.MaxViewport

#### [ElementFactoryGetArgs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.elementfactorygetargs)

ElementFactoryGetArgs <br> ElementFactoryGetArgs.Data <br> ElementFactoryGetArgs.#ctor <br> ElementFactoryGetArgs.Parent

#### [ElementFactoryRecycleArgs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.elementfactoryrecycleargs)

ElementFactoryRecycleArgs <br> ElementFactoryRecycleArgs.Element <br> ElementFactoryRecycleArgs.#ctor <br> ElementFactoryRecycleArgs.Parent

#### [FrameworkElement](https://docs.microsoft.com/uwp/api/windows.ui.xaml.frameworkelement)

FrameworkElement.EffectiveViewportChanged <br> FrameworkElement.InvalidateViewport <br> FrameworkElement.IsLoaded

#### [IElementFactory](https://docs.microsoft.com/uwp/api/windows.ui.xaml.ielementfactory)

IElementFactory <br> IElementFactory.GetElement <br> IElementFactory.RecycleElement

#### [ScalarTransition](https://docs.microsoft.com/uwp/api/windows.ui.xaml.scalartransition)

ScalarTransition <br> ScalarTransition.Duration <br> ScalarTransition.#ctor

#### [UIElement](https://docs.microsoft.com/uwp/api/windows.ui.xaml.uielement)

UIElement.CanBeScrollAnchor <br> UIElement.CanBeScrollAnchorProperty <br> UIElement.CenterPoint <br> UIElement.OpacityTransition <br> UIElement.PopulatePropertyInfo <br> UIElement.PopulatePropertyInfoOverride <br> UIElement.Rotation <br> UIElement.RotationAxis <br> UIElement.RotationTransition <br> UIElement.Scale <br> UIElement.ScaleTransition <br> UIElement.StartAnimation <br> UIElement.StopAnimation <br> UIElement.TransformMatrix <br> UIElement.Translation <br> UIElement.TranslationTransition

#### [Vector3Transition](https://docs.microsoft.com/uwp/api/windows.ui.xaml.vector3transition)

Vector3Transition <br> Vector3Transition.Components <br> Vector3Transition.Duration <br> Vector3Transition.#ctor

#### [Vector3TransitionComponents](https://docs.microsoft.com/uwp/api/windows.ui.xaml.vector3transitioncomponents)

Vector3TransitionComponents

## Windows.Web

### [Windows.Web.UI.Interop](https://docs.microsoft.com/uwp/api/windows.web.ui.interop)

#### [WebViewControl](https://docs.microsoft.com/uwp/api/windows.web.ui.interop.webviewcontrol)

WebViewControl.AddInitializeScript <br> WebViewControl.GotFocus <br> WebViewControl.LostFocus

### [Windows.Web.UI](https://docs.microsoft.com/uwp/api/windows.web.ui)

#### [IWebViewControl2](https://docs.microsoft.com/uwp/api/windows.web.ui.iwebviewcontrol2)

IWebViewControl2 <br> IWebViewControl2.AddInitializeScript
