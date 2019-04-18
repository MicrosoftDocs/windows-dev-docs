---
title: Windows 10 Build 18362 API changes
description: Developers can use the following list to identify new or changed namespaces in Windows 10 build 18362
keywords: what's new, whats new, updates, Windows 10, newest, apis, 18362, may
ms.date: 04/19/2019
ms.topic: article
ms.localizationpriority: medium
ms.custom: 19H1
---
# New APIs in Windows 10 build 18362

New and updated API namespaces have been made available to developers in Windows 10 build 18362 (Also known as SDK version 1903). Below is a full list of documentation published for namespaces added or modified in this release.

For information on APIs added in the previous public release, see [New APIs in the Windows 10 October Update](windows-10-build-17763-api-diff.md).

## Windows.AI

### [Windows.AI.MachineLearning](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning)

#### [LearningModelSessionOptions](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.learningmodelsessionoptions)

LearningModelSessionOptions <br> LearningModelSessionOptions.BatchSizeOverride <br> LearningModelSessionOptions.#ctor

#### [LearningModelSession](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.learningmodelsession)

LearningModelSession.#ctor

#### [TensorBoolean](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.tensorboolean)

TensorBoolean.Close <br> TensorBoolean.CreateFromBuffer <br> TensorBoolean.CreateFromShapeArrayAndDataArray <br> TensorBoolean.CreateReference

#### [TensorDouble](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.tensordouble)

TensorDouble.Close <br> TensorDouble.CreateFromBuffer <br> TensorDouble.CreateFromShapeArrayAndDataArray <br> TensorDouble.CreateReference

#### [TensorFloat16Bit](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.tensorfloat16bit)

TensorFloat16Bit.Close <br> TensorFloat16Bit.CreateFromBuffer <br> TensorFloat16Bit.CreateFromShapeArrayAndDataArray <br> TensorFloat16Bit.CreateReference

#### [TensorFloat](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.tensorfloat)

TensorFloat.Close <br> TensorFloat.CreateFromBuffer <br> TensorFloat.CreateFromShapeArrayAndDataArray <br> TensorFloat.CreateReference

#### [TensorInt16Bit](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.tensorint16bit)

TensorInt16Bit.Close <br> TensorInt16Bit.CreateFromBuffer <br> TensorInt16Bit.CreateFromShapeArrayAndDataArray <br> TensorInt16Bit.CreateReference

#### [TensorInt32Bit](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.tensorint32bit)

TensorInt32Bit.Close <br> TensorInt32Bit.CreateFromBuffer <br> TensorInt32Bit.CreateFromShapeArrayAndDataArray <br> TensorInt32Bit.CreateReference

#### [TensorInt64Bit](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.tensorint64bit)

TensorInt64Bit.Close <br> TensorInt64Bit.CreateFromBuffer <br> TensorInt64Bit.CreateFromShapeArrayAndDataArray <br> TensorInt64Bit.CreateReference

#### [TensorInt8Bit](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.tensorint8bit)

TensorInt8Bit.Close <br> TensorInt8Bit.CreateFromBuffer <br> TensorInt8Bit.CreateFromShapeArrayAndDataArray <br> TensorInt8Bit.CreateReference

#### [TensorString](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.tensorstring)

TensorString.Close <br> TensorString.CreateFromShapeArrayAndDataArray <br> TensorString.CreateReference

#### [TensorUInt16Bit](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.tensoruint16bit)

TensorUInt16Bit.Close <br> TensorUInt16Bit.CreateFromBuffer <br> TensorUInt16Bit.CreateFromShapeArrayAndDataArray <br> TensorUInt16Bit.CreateReference

#### [TensorUInt32Bit](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.tensoruint32bit)

TensorUInt32Bit.Close <br> TensorUInt32Bit.CreateFromBuffer <br> TensorUInt32Bit.CreateFromShapeArrayAndDataArray <br> TensorUInt32Bit.CreateReference

#### [TensorUInt64Bit](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.tensoruint64bit)

TensorUInt64Bit.Close <br> TensorUInt64Bit.CreateFromBuffer <br> TensorUInt64Bit.CreateFromShapeArrayAndDataArray <br> TensorUInt64Bit.CreateReference

#### [TensorUInt8Bit](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.tensoruint8bit)

TensorUInt8Bit.Close <br> TensorUInt8Bit.CreateFromBuffer <br> TensorUInt8Bit.CreateFromShapeArrayAndDataArray <br> TensorUInt8Bit.CreateReference

## Windows.ApplicationModel

### [Windows.ApplicationModel](https://docs.microsoft.com/uwp/api/windows.applicationmodel)

#### [Package](https://docs.microsoft.com/uwp/api/windows.applicationmodel.package)

Package.EffectiveLocation <br> Package.MutableLocation

### [Windows.ApplicationModel.AppService](https://docs.microsoft.com/uwp/api/windows.applicationmodel.appservice)

#### [AppServiceConnection](https://docs.microsoft.com/uwp/api/windows.applicationmodel.appservice.appserviceconnection)

AppServiceConnection.SendStatelessMessageAsync

#### [AppServiceTriggerDetails](https://docs.microsoft.com/uwp/api/windows.applicationmodel.appservice.appservicetriggerdetails)

AppServiceTriggerDetails.CallerRemoteConnectionToken

#### [StatelessAppServiceResponse](https://docs.microsoft.com/uwp/api/windows.applicationmodel.appservice.statelessappserviceresponse)

StatelessAppServiceResponse

#### [StatelessAppServiceResponseStatus](https://docs.microsoft.com/uwp/api/windows.applicationmodel.appservice.statelessappserviceresponsestatus)

StatelessAppServiceResponseStatus

#### [StatelessAppServiceResponse](https://docs.microsoft.com/uwp/api/windows.applicationmodel.appservice.statelessappserviceresponse)

StatelessAppServiceResponse.Message <br> StatelessAppServiceResponse.Status

### [Windows.ApplicationModel.Background](https://docs.microsoft.com/uwp/api/windows.applicationmodel.background)

#### [ConversationalAgentTrigger](https://docs.microsoft.com/uwp/api/windows.applicationmodel.background.conversationalagenttrigger)

ConversationalAgentTrigger <br> ConversationalAgentTrigger.#ctor

### [Windows.ApplicationModel.Calls](https://docs.microsoft.com/uwp/api/windows.applicationmodel.calls)

#### [PhoneLineTransportDevice](https://docs.microsoft.com/uwp/api/windows.applicationmodel.calls.phonelinetransportdevice)

PhoneLineTransportDevice <br> PhoneLineTransportDevice.ConnectAsync <br> PhoneLineTransportDevice.Connect <br> PhoneLineTransportDevice.DeviceId <br> PhoneLineTransportDevice.FromId <br> PhoneLineTransportDevice.GetDeviceSelector <br> PhoneLineTransportDevice.GetDeviceSelector <br> PhoneLineTransportDevice.IsRegistered <br> PhoneLineTransportDevice.RegisterAppForUser <br> PhoneLineTransportDevice.RegisterApp <br> PhoneLineTransportDevice.RequestAccessAsync <br> PhoneLineTransportDevice.Transport <br> PhoneLineTransportDevice.UnregisterAppForUser <br> PhoneLineTransportDevice.UnregisterApp

#### [PhoneLine](https://docs.microsoft.com/uwp/api/windows.applicationmodel.calls.phoneline)

PhoneLine.EnableTextReply <br> PhoneLine.TransportDeviceId

### [Windows.ApplicationModel.Calls.Background](https://docs.microsoft.com/uwp/api/windows.applicationmodel.calls.background)

#### [PhoneIncomingCallDismissedReason](https://docs.microsoft.com/uwp/api/windows.applicationmodel.calls.background.phoneincomingcalldismissedreason)

PhoneIncomingCallDismissedReason

#### [PhoneIncomingCallDismissedTriggerDetails](https://docs.microsoft.com/uwp/api/windows.applicationmodel.calls.background.phoneincomingcalldismissedtriggerdetails)

PhoneIncomingCallDismissedTriggerDetails <br> PhoneIncomingCallDismissedTriggerDetails.DismissalTime <br> PhoneIncomingCallDismissedTriggerDetails.DisplayName <br> PhoneIncomingCallDismissedTriggerDetails.LineId <br> PhoneIncomingCallDismissedTriggerDetails.PhoneNumber <br> PhoneIncomingCallDismissedTriggerDetails.Reason <br> PhoneIncomingCallDismissedTriggerDetails.TextReplyMessage

### [Windows.ApplicationModel.Calls.Provider](https://docs.microsoft.com/uwp/api/windows.applicationmodel.calls.provider)

#### [PhoneCallOriginManager](https://docs.microsoft.com/uwp/api/windows.applicationmodel.calls.provider.phonecalloriginmanager)

PhoneCallOriginManager.IsSupported

### [Windows.ApplicationModel.ConversationalAgent](https://docs.microsoft.com/uwp/api/windows.applicationmodel.conversationalagent)

#### [ConversationalAgentSession](https://docs.microsoft.com/uwp/api/windows.applicationmodel.conversationalagent.conversationalagentsession)

ConversationalAgentSession

#### [ConversationalAgentSessionInterruptedEventArgs](https://docs.microsoft.com/uwp/api/windows.applicationmodel.conversationalagent.conversationalagentsessioninterruptedeventargs)

ConversationalAgentSessionInterruptedEventArgs

#### [ConversationalAgentSessionUpdateResponse](https://docs.microsoft.com/uwp/api/windows.applicationmodel.conversationalagent.conversationalagentsessionupdateresponse)

ConversationalAgentSessionUpdateResponse

#### [ConversationalAgentSession](https://docs.microsoft.com/uwp/api/windows.applicationmodel.conversationalagent.conversationalagentsession)

ConversationalAgentSession.AgentState <br> ConversationalAgentSession.Close <br> ConversationalAgentSession.CreateAudioDeviceInputNodeAsync <br> ConversationalAgentSession.CreateAudioDeviceInputNode <br> ConversationalAgentSession.GetAudioCaptureDeviceIdAsync <br> ConversationalAgentSession.GetAudioCaptureDeviceId <br> ConversationalAgentSession.GetAudioClientAsync <br> ConversationalAgentSession.GetAudioClient <br> ConversationalAgentSession.GetAudioRenderDeviceIdAsync <br> ConversationalAgentSession.GetAudioRenderDeviceId <br> ConversationalAgentSession.GetCurrentSessionAsync <br> ConversationalAgentSession.GetCurrentSessionSync <br> ConversationalAgentSession.GetSignalModelIdAsync <br> ConversationalAgentSession.GetSignalModelId <br> ConversationalAgentSession.GetSupportedSignalModelIdsAsync <br> ConversationalAgentSession.GetSupportedSignalModelIds <br> ConversationalAgentSession.IsIndicatorLightAvailable <br> ConversationalAgentSession.IsInterrupted <br> ConversationalAgentSession.IsInterruptible <br> ConversationalAgentSession.IsScreenAvailable <br> ConversationalAgentSession.IsUserAuthenticated <br> ConversationalAgentSession.IsVoiceActivationAvailable <br> ConversationalAgentSession.RequestAgentStateChangeAsync <br> ConversationalAgentSession.RequestAgentStateChange <br> ConversationalAgentSession.RequestForegroundActivationAsync <br> ConversationalAgentSession.RequestForegroundActivation <br> ConversationalAgentSession.RequestInterruptibleAsync <br> ConversationalAgentSession.RequestInterruptible <br> ConversationalAgentSession.SessionInterrupted <br> ConversationalAgentSession.SetSignalModelIdAsync <br> ConversationalAgentSession.SetSignalModelId <br> ConversationalAgentSession.Signal <br> ConversationalAgentSession.SignalDetected <br> ConversationalAgentSession.SystemStateChanged

#### [ConversationalAgentSignal](https://docs.microsoft.com/uwp/api/windows.applicationmodel.conversationalagent.conversationalagentsignal)

ConversationalAgentSignal

#### [ConversationalAgentSignalDetectedEventArgs](https://docs.microsoft.com/uwp/api/windows.applicationmodel.conversationalagent.conversationalagentsignaldetectedeventargs)

ConversationalAgentSignalDetectedEventArgs

#### [ConversationalAgentSignal](https://docs.microsoft.com/uwp/api/windows.applicationmodel.conversationalagent.conversationalagentsignal)

ConversationalAgentSignal.IsSignalVerificationRequired <br> ConversationalAgentSignal.SignalContext <br> ConversationalAgentSignal.SignalEnd <br> ConversationalAgentSignal.SignalId <br> ConversationalAgentSignal.SignalName <br> ConversationalAgentSignal.SignalStart

#### [ConversationalAgentState](https://docs.microsoft.com/uwp/api/windows.applicationmodel.conversationalagent.conversationalagentstate)

ConversationalAgentState

#### [ConversationalAgentSystemStateChangedEventArgs](https://docs.microsoft.com/uwp/api/windows.applicationmodel.conversationalagent.conversationalagentsystemstatechangedeventargs)

ConversationalAgentSystemStateChangedEventArgs <br> ConversationalAgentSystemStateChangedEventArgs.SystemStateChangeType

#### [ConversationalAgentSystemStateChangeType](https://docs.microsoft.com/uwp/api/windows.applicationmodel.conversationalagent.conversationalagentsystemstatechangetype)

ConversationalAgentSystemStateChangeType

### [Windows.ApplicationModel.Preview.Holographic](https://docs.microsoft.com/uwp/api/windows.applicationmodel.preview.holographic)

#### [HolographicKeyboardPlacementOverridePreview](https://docs.microsoft.com/uwp/api/windows.applicationmodel.preview.holographic.holographickeyboardplacementoverridepreview)

HolographicKeyboardPlacementOverridePreview <br> HolographicKeyboardPlacementOverridePreview.GetForCurrentView <br> HolographicKeyboardPlacementOverridePreview.ResetPlacementOverride <br> HolographicKeyboardPlacementOverridePreview.SetPlacementOverride <br> HolographicKeyboardPlacementOverridePreview.SetPlacementOverride

### [Windows.ApplicationModel.Resources](https://docs.microsoft.com/uwp/api/windows.applicationmodel.resources)

#### [ResourceLoader](https://docs.microsoft.com/uwp/api/windows.applicationmodel.resources.resourceloader)

ResourceLoader.GetForUIContext

### [Windows.ApplicationModel.Resources.Core](https://docs.microsoft.com/uwp/api/windows.applicationmodel.resources.core)

#### [ResourceCandidateKind](https://docs.microsoft.com/uwp/api/windows.applicationmodel.resources.core.resourcecandidatekind)

ResourceCandidateKind

#### [ResourceCandidate](https://docs.microsoft.com/uwp/api/windows.applicationmodel.resources.core.resourcecandidate)

ResourceCandidate.Kind

#### [ResourceContext](https://docs.microsoft.com/uwp/api/windows.applicationmodel.resources.core.resourcecontext)

ResourceContext.GetForUIContext

### [Windows.ApplicationModel.UserActivities](https://docs.microsoft.com/uwp/api/windows.applicationmodel.useractivities)

#### [UserActivityChannel](https://docs.microsoft.com/uwp/api/windows.applicationmodel.useractivities.useractivitychannel)

UserActivityChannel.GetForUser

## Windows.Devices

### [Windows.Devices.Bluetooth.GenericAttributeProfile](https://docs.microsoft.com/uwp/api/windows.devices.bluetooth.genericattributeprofile)

#### [GattServiceProviderAdvertisingParameters](https://docs.microsoft.com/uwp/api/windows.devices.bluetooth.genericattributeprofile.gattserviceprovideradvertisingparameters)

GattServiceProviderAdvertisingParameters.ServiceData

### [Windows.Devices.Enumeration](https://docs.microsoft.com/uwp/api/windows.devices.enumeration)

#### [DevicePairingRequestedEventArgs](https://docs.microsoft.com/uwp/api/windows.devices.enumeration.devicepairingrequestedeventargs)

DevicePairingRequestedEventArgs.AcceptWithPasswordCredential

### [Windows.Devices.Input](https://docs.microsoft.com/uwp/api/windows.devices.input)

#### [PenDevice](https://docs.microsoft.com/uwp/api/windows.devices.input.pendevice)

PenDevice <br> PenDevice.GetFromPointerId <br> PenDevice.PenId

### [Windows.Devices.PointOfService](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice)

#### [JournalPrinterCapabilities](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.journalprintercapabilities)

JournalPrinterCapabilities.IsReversePaperFeedByLineSupported <br> JournalPrinterCapabilities.IsReversePaperFeedByMapModeUnitSupported <br> JournalPrinterCapabilities.IsReverseVideoSupported <br> JournalPrinterCapabilities.IsStrikethroughSupported <br> JournalPrinterCapabilities.IsSubscriptSupported <br> JournalPrinterCapabilities.IsSuperscriptSupported

#### [JournalPrintJob](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.journalprintjob)

JournalPrintJob.FeedPaperByLine <br> JournalPrintJob.FeedPaperByMapModeUnit <br> JournalPrintJob.Print

#### [PosPrinterFontProperty](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.posprinterfontproperty)

PosPrinterFontProperty <br> PosPrinterFontProperty.CharacterSizes <br> PosPrinterFontProperty.IsScalableToAnySize <br> PosPrinterFontProperty.TypeFace

#### [PosPrinterPrintOptions](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.posprinterprintoptions)

PosPrinterPrintOptions <br> PosPrinterPrintOptions.Alignment <br> PosPrinterPrintOptions.Bold <br> PosPrinterPrintOptions.CharacterHeight <br> PosPrinterPrintOptions.CharacterSet <br> PosPrinterPrintOptions.DoubleHigh <br> PosPrinterPrintOptions.DoubleWide <br> PosPrinterPrintOptions.Italic <br> PosPrinterPrintOptions.#ctor <br> PosPrinterPrintOptions.ReverseVideo <br> PosPrinterPrintOptions.Strikethrough <br> PosPrinterPrintOptions.Subscript <br> PosPrinterPrintOptions.Superscript <br> PosPrinterPrintOptions.TypeFace <br> PosPrinterPrintOptions.Underline

#### [PosPrinter](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.posprinter)

PosPrinter.GetFontProperty <br> PosPrinter.SupportedBarcodeSymbologies

#### [ReceiptPrinterCapabilities](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.receiptprintercapabilities)

ReceiptPrinterCapabilities.IsReversePaperFeedByLineSupported <br> ReceiptPrinterCapabilities.IsReversePaperFeedByMapModeUnitSupported <br> ReceiptPrinterCapabilities.IsReverseVideoSupported <br> ReceiptPrinterCapabilities.IsStrikethroughSupported <br> ReceiptPrinterCapabilities.IsSubscriptSupported <br> ReceiptPrinterCapabilities.IsSuperscriptSupported

#### [ReceiptPrintJob](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.receiptprintjob)

ReceiptPrintJob.FeedPaperByLine <br> ReceiptPrintJob.FeedPaperByMapModeUnit <br> ReceiptPrintJob.Print <br> ReceiptPrintJob.StampPaper

#### [SizeUInt32](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.sizeuint32)

SizeUInt32

#### [SlipPrinterCapabilities](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.slipprintercapabilities)

SlipPrinterCapabilities.IsReversePaperFeedByLineSupported <br> SlipPrinterCapabilities.IsReversePaperFeedByMapModeUnitSupported <br> SlipPrinterCapabilities.IsReverseVideoSupported <br> SlipPrinterCapabilities.IsStrikethroughSupported <br> SlipPrinterCapabilities.IsSubscriptSupported <br> SlipPrinterCapabilities.IsSuperscriptSupported

#### [SlipPrintJob](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.slipprintjob)

SlipPrintJob.FeedPaperByLine <br> SlipPrintJob.FeedPaperByMapModeUnit <br> SlipPrintJob.Print

## Windows.Globalization

### [Windows.Globalization](https://docs.microsoft.com/uwp/api/windows.globalization)

#### [CurrencyAmount](https://docs.microsoft.com/uwp/api/windows.globalization.currencyamount)

CurrencyAmount <br> CurrencyAmount.Amount <br> CurrencyAmount.Currency <br> CurrencyAmount.#ctor

## Windows.Graphics

### [Windows.Graphics.DirectX](https://docs.microsoft.com/uwp/api/windows.graphics.directx)

#### [DirectXPrimitiveTopology](https://docs.microsoft.com/uwp/api/windows.graphics.directx.directxprimitivetopology)

DirectXPrimitiveTopology

### [Windows.Graphics.Holographic](https://docs.microsoft.com/uwp/api/windows.graphics.holographic)

#### [HolographicCamera](https://docs.microsoft.com/uwp/api/windows.graphics.holographic.holographiccamera)

HolographicCamera.ViewConfiguration

#### [HolographicDisplay](https://docs.microsoft.com/uwp/api/windows.graphics.holographic.holographicdisplay)

HolographicDisplay.TryGetViewConfiguration

#### [HolographicViewConfiguration](https://docs.microsoft.com/uwp/api/windows.graphics.holographic.holographicviewconfiguration)

HolographicViewConfiguration

#### [HolographicViewConfigurationKind](https://docs.microsoft.com/uwp/api/windows.graphics.holographic.holographicviewconfigurationkind)

HolographicViewConfigurationKind

#### [HolographicViewConfiguration](https://docs.microsoft.com/uwp/api/windows.graphics.holographic.holographicviewconfiguration)

HolographicViewConfiguration.Display <br> HolographicViewConfiguration.IsEnabled <br> HolographicViewConfiguration.IsStereo <br> HolographicViewConfiguration.Kind <br> HolographicViewConfiguration.NativeRenderTargetSize <br> HolographicViewConfiguration.PixelFormat <br> HolographicViewConfiguration.RefreshRate <br> HolographicViewConfiguration.RenderTargetSize <br> HolographicViewConfiguration.RequestRenderTargetSize <br> HolographicViewConfiguration.SupportedPixelFormats

## Windows.Media

### [Windows.Media.Devices](https://docs.microsoft.com/uwp/api/windows.media.devices)

#### [InfraredTorchControl](https://docs.microsoft.com/uwp/api/windows.media.devices.infraredtorchcontrol)

InfraredTorchControl <br> InfraredTorchControl.CurrentMode <br> InfraredTorchControl.IsSupported <br> InfraredTorchControl.MaxPower <br> InfraredTorchControl.MinPower <br> InfraredTorchControl.Power <br> InfraredTorchControl.PowerStep <br> InfraredTorchControl.SupportedModes

#### [InfraredTorchMode](https://docs.microsoft.com/uwp/api/windows.media.devices.infraredtorchmode)

InfraredTorchMode

#### [VideoDeviceController](https://docs.microsoft.com/uwp/api/windows.media.devices.videodevicecontroller)

VideoDeviceController.InfraredTorchControl

### [Windows.Media.Miracast](https://docs.microsoft.com/uwp/api/windows.media.miracast)

#### [MiracastReceiver](https://docs.microsoft.com/uwp/api/windows.media.miracast.miracastreceiver)

MiracastReceiver

#### [MiracastReceiverApplySettingsResult](https://docs.microsoft.com/uwp/api/windows.media.miracast.miracastreceiverapplysettingsresult)

MiracastReceiverApplySettingsResult <br> MiracastReceiverApplySettingsResult.ExtendedError <br> MiracastReceiverApplySettingsResult.Status

#### [MiracastReceiverApplySettingsStatus](https://docs.microsoft.com/uwp/api/windows.media.miracast.miracastreceiverapplysettingsstatus)

MiracastReceiverApplySettingsStatus

#### [MiracastReceiverAuthorizationMethod](https://docs.microsoft.com/uwp/api/windows.media.miracast.miracastreceiverauthorizationmethod)

MiracastReceiverAuthorizationMethod

#### [MiracastReceiverConnection](https://docs.microsoft.com/uwp/api/windows.media.miracast.miracastreceiverconnection)

MiracastReceiverConnection

#### [MiracastReceiverConnectionCreatedEventArgs](https://docs.microsoft.com/uwp/api/windows.media.miracast.miracastreceiverconnectioncreatedeventargs)

MiracastReceiverConnectionCreatedEventArgs <br> MiracastReceiverConnectionCreatedEventArgs.Connection <br> MiracastReceiverConnectionCreatedEventArgs.GetDeferral <br> MiracastReceiverConnectionCreatedEventArgs.Pin

#### [MiracastReceiverConnection](https://docs.microsoft.com/uwp/api/windows.media.miracast.miracastreceiverconnection)

MiracastReceiverConnection.Close <br> MiracastReceiverConnection.CursorImageChannel <br> MiracastReceiverConnection.Disconnect <br> MiracastReceiverConnection.Disconnect <br> MiracastReceiverConnection.InputDevices <br> MiracastReceiverConnection.PauseAsync <br> MiracastReceiverConnection.Pause <br> MiracastReceiverConnection.ResumeAsync <br> MiracastReceiverConnection.Resume <br> MiracastReceiverConnection.StreamControl <br> MiracastReceiverConnection.Transmitter

#### [MiracastReceiverCursorImageChannel](https://docs.microsoft.com/uwp/api/windows.media.miracast.miracastreceivercursorimagechannel)

MiracastReceiverCursorImageChannel

#### [MiracastReceiverCursorImageChannelSettings](https://docs.microsoft.com/uwp/api/windows.media.miracast.miracastreceivercursorimagechannelsettings)

MiracastReceiverCursorImageChannelSettings <br> MiracastReceiverCursorImageChannelSettings.IsEnabled <br> MiracastReceiverCursorImageChannelSettings.MaxImageSize

#### [MiracastReceiverCursorImageChannel](https://docs.microsoft.com/uwp/api/windows.media.miracast.miracastreceivercursorimagechannel)

MiracastReceiverCursorImageChannel.ImageStream <br> MiracastReceiverCursorImageChannel.ImageStreamChanged <br> MiracastReceiverCursorImageChannel.IsEnabled <br> MiracastReceiverCursorImageChannel.MaxImageSize <br> MiracastReceiverCursorImageChannel.Position <br> MiracastReceiverCursorImageChannel.PositionChanged

#### [MiracastReceiverDisconnectedEventArgs](https://docs.microsoft.com/uwp/api/windows.media.miracast.miracastreceiverdisconnectedeventargs)

MiracastReceiverDisconnectedEventArgs <br> MiracastReceiverDisconnectedEventArgs.Connection

#### [MiracastReceiverDisconnectReason](https://docs.microsoft.com/uwp/api/windows.media.miracast.miracastreceiverdisconnectreason)

MiracastReceiverDisconnectReason

#### [MiracastReceiverGameControllerDevice](https://docs.microsoft.com/uwp/api/windows.media.miracast.miracastreceivergamecontrollerdevice)

MiracastReceiverGameControllerDevice

#### [MiracastReceiverGameControllerDeviceUsageMode](https://docs.microsoft.com/uwp/api/windows.media.miracast.miracastreceivergamecontrollerdeviceusagemode)

MiracastReceiverGameControllerDeviceUsageMode

#### [MiracastReceiverGameControllerDevice](https://docs.microsoft.com/uwp/api/windows.media.miracast.miracastreceivergamecontrollerdevice)

MiracastReceiverGameControllerDevice.Changed <br> MiracastReceiverGameControllerDevice.IsRequestedByTransmitter <br> MiracastReceiverGameControllerDevice.IsTransmittingInput <br> MiracastReceiverGameControllerDevice.Mode <br> MiracastReceiverGameControllerDevice.TransmitInput

#### [MiracastReceiverInputDevices](https://docs.microsoft.com/uwp/api/windows.media.miracast.miracastreceiverinputdevices)

MiracastReceiverInputDevices <br> MiracastReceiverInputDevices.GameController <br> MiracastReceiverInputDevices.Keyboard

#### [MiracastReceiverKeyboardDevice](https://docs.microsoft.com/uwp/api/windows.media.miracast.miracastreceiverkeyboarddevice)

MiracastReceiverKeyboardDevice <br> MiracastReceiverKeyboardDevice.Changed <br> MiracastReceiverKeyboardDevice.IsRequestedByTransmitter <br> MiracastReceiverKeyboardDevice.IsTransmittingInput <br> MiracastReceiverKeyboardDevice.TransmitInput

#### [MiracastReceiverListeningStatus](https://docs.microsoft.com/uwp/api/windows.media.miracast.miracastreceiverlisteningstatus)

MiracastReceiverListeningStatus

#### [MiracastReceiverMediaSourceCreatedEventArgs](https://docs.microsoft.com/uwp/api/windows.media.miracast.miracastreceivermediasourcecreatedeventargs)

MiracastReceiverMediaSourceCreatedEventArgs <br> MiracastReceiverMediaSourceCreatedEventArgs.Connection <br> MiracastReceiverMediaSourceCreatedEventArgs.CursorImageChannelSettings <br> MiracastReceiverMediaSourceCreatedEventArgs.GetDeferral <br> MiracastReceiverMediaSourceCreatedEventArgs.MediaSource

#### [MiracastReceiverSession](https://docs.microsoft.com/uwp/api/windows.media.miracast.miracastreceiversession)

MiracastReceiverSession

#### [MiracastReceiverSessionStartResult](https://docs.microsoft.com/uwp/api/windows.media.miracast.miracastreceiversessionstartresult)

MiracastReceiverSessionStartResult <br> MiracastReceiverSessionStartResult.ExtendedError <br> MiracastReceiverSessionStartResult.Status

#### [MiracastReceiverSessionStartStatus](https://docs.microsoft.com/uwp/api/windows.media.miracast.miracastreceiversessionstartstatus)

MiracastReceiverSessionStartStatus

#### [MiracastReceiverSession](https://docs.microsoft.com/uwp/api/windows.media.miracast.miracastreceiversession)

MiracastReceiverSession.AllowConnectionTakeover <br> MiracastReceiverSession.Close <br> MiracastReceiverSession.ConnectionCreated <br> MiracastReceiverSession.Disconnected <br> MiracastReceiverSession.MaxSimultaneousConnections <br> MiracastReceiverSession.MediaSourceCreated <br> MiracastReceiverSession.StartAsync <br> MiracastReceiverSession.Start

#### [MiracastReceiverSettings](https://docs.microsoft.com/uwp/api/windows.media.miracast.miracastreceiversettings)

MiracastReceiverSettings <br> MiracastReceiverSettings.AuthorizationMethod <br> MiracastReceiverSettings.FriendlyName <br> MiracastReceiverSettings.ModelName <br> MiracastReceiverSettings.ModelNumber <br> MiracastReceiverSettings.RequireAuthorizationFromKnownTransmitters

#### [MiracastReceiverStatus](https://docs.microsoft.com/uwp/api/windows.media.miracast.miracastreceiverstatus)

MiracastReceiverStatus <br> MiracastReceiverStatus.IsConnectionTakeoverSupported <br> MiracastReceiverStatus.KnownTransmitters <br> MiracastReceiverStatus.ListeningStatus <br> MiracastReceiverStatus.MaxSimultaneousConnections <br> MiracastReceiverStatus.WiFiStatus

#### [MiracastReceiverStreamControl](https://docs.microsoft.com/uwp/api/windows.media.miracast.miracastreceiverstreamcontrol)

MiracastReceiverStreamControl <br> MiracastReceiverStreamControl.GetVideoStreamSettingsAsync <br> MiracastReceiverStreamControl.GetVideoStreamSettings <br> MiracastReceiverStreamControl.MuteAudio <br> MiracastReceiverStreamControl.SuggestVideoStreamSettingsAsync <br> MiracastReceiverStreamControl.SuggestVideoStreamSettings

#### [MiracastReceiverVideoStreamSettings](https://docs.microsoft.com/uwp/api/windows.media.miracast.miracastreceivervideostreamsettings)

MiracastReceiverVideoStreamSettings <br> MiracastReceiverVideoStreamSettings.Bitrate <br> MiracastReceiverVideoStreamSettings.Size

#### [MiracastReceiverWiFiStatus](https://docs.microsoft.com/uwp/api/windows.media.miracast.miracastreceiverwifistatus)

MiracastReceiverWiFiStatus

#### [MiracastReceiver](https://docs.microsoft.com/uwp/api/windows.media.miracast.miracastreceiver)

MiracastReceiver.ClearKnownTransmitters <br> MiracastReceiver.CreateSessionAsync <br> MiracastReceiver.CreateSession <br> MiracastReceiver.DisconnectAllAndApplySettingsAsync <br> MiracastReceiver.DisconnectAllAndApplySettings <br> MiracastReceiver.GetCurrentSettingsAsync <br> MiracastReceiver.GetCurrentSettings <br> MiracastReceiver.GetDefaultSettings <br> MiracastReceiver.GetStatusAsync <br> MiracastReceiver.GetStatus <br> MiracastReceiver.#ctor <br> MiracastReceiver.RemoveKnownTransmitter <br> MiracastReceiver.StatusChanged

#### [MiracastTransmitter](https://docs.microsoft.com/uwp/api/windows.media.miracast.miracasttransmitter)

MiracastTransmitter

#### [MiracastTransmitterAuthorizationStatus](https://docs.microsoft.com/uwp/api/windows.media.miracast.miracasttransmitterauthorizationstatus)

MiracastTransmitterAuthorizationStatus

#### [MiracastTransmitter](https://docs.microsoft.com/uwp/api/windows.media.miracast.miracasttransmitter)

MiracastTransmitter.AuthorizationStatus <br> MiracastTransmitter.GetConnections <br> MiracastTransmitter.LastConnectionTime <br> MiracastTransmitter.MacAddress <br> MiracastTransmitter.Name

## Windows.Networking

### [Windows.Networking.NetworkOperators](https://docs.microsoft.com/uwp/api/windows.networking.networkoperators)

#### [ESimDiscoverEvent](https://docs.microsoft.com/uwp/api/windows.networking.networkoperators.esimdiscoverevent)

ESimDiscoverEvent <br> ESimDiscoverEvent.MatchingId <br> ESimDiscoverEvent.RspServerAddress

#### [ESimDiscoverResult](https://docs.microsoft.com/uwp/api/windows.networking.networkoperators.esimdiscoverresult)

ESimDiscoverResult

#### [ESimDiscoverResultKind](https://docs.microsoft.com/uwp/api/windows.networking.networkoperators.esimdiscoverresultkind)

ESimDiscoverResultKind

#### [ESimDiscoverResult](https://docs.microsoft.com/uwp/api/windows.networking.networkoperators.esimdiscoverresult)

ESimDiscoverResult.Events <br> ESimDiscoverResult.Kind <br> ESimDiscoverResult.ProfileMetadata <br> ESimDiscoverResult.Result

#### [ESim](https://docs.microsoft.com/uwp/api/windows.networking.networkoperators.esim)

ESim.DiscoverAsync <br> ESim.DiscoverAsync <br> ESim.Discover <br> ESim.Discover

### [Windows.Networking.PushNotifications](https://docs.microsoft.com/uwp/api/windows.networking.pushnotifications)

#### [PushNotificationChannelManager](https://docs.microsoft.com/uwp/api/windows.networking.pushnotifications.pushnotificationchannelmanager)

PushNotificationChannelManager.ChannelsRevoked

#### [PushNotificationChannelsRevokedEventArgs](https://docs.microsoft.com/uwp/api/windows.networking.pushnotifications.pushnotificationchannelsrevokedeventargs)

PushNotificationChannelsRevokedEventArgs

## Windows.Perception

### [Windows.Perception.People](https://docs.microsoft.com/uwp/api/windows.perception.people)

#### [EyesPose](https://docs.microsoft.com/uwp/api/windows.perception.people.eyespose)

EyesPose <br> EyesPose.Gaze <br> EyesPose.IsCalibrationValid <br> EyesPose.IsSupported <br> EyesPose.RequestAccessAsync <br> EyesPose.UpdateTimestamp

#### [HandJointKind](https://docs.microsoft.com/uwp/api/windows.perception.people.handjointkind)

HandJointKind

#### [HandMeshObserver](https://docs.microsoft.com/uwp/api/windows.perception.people.handmeshobserver)

HandMeshObserver <br> HandMeshObserver.GetTriangleIndices <br> HandMeshObserver.GetVertexStateForPose <br> HandMeshObserver.ModelId <br> HandMeshObserver.NeutralPose <br> HandMeshObserver.NeutralPoseVersion <br> HandMeshObserver.Source <br> HandMeshObserver.TriangleIndexCount <br> HandMeshObserver.VertexCount

#### [HandMeshVertex](https://docs.microsoft.com/uwp/api/windows.perception.people.handmeshvertex)

HandMeshVertex

#### [HandMeshVertexState](https://docs.microsoft.com/uwp/api/windows.perception.people.handmeshvertexstate)

HandMeshVertexState <br> HandMeshVertexState.CoordinateSystem <br> HandMeshVertexState.GetVertices <br> HandMeshVertexState.UpdateTimestamp

#### [HandPose](https://docs.microsoft.com/uwp/api/windows.perception.people.handpose)

HandPose <br> HandPose.GetRelativeJoints <br> HandPose.GetRelativeJoint <br> HandPose.TryGetJoints <br> HandPose.TryGetJoint

#### [JointPose](https://docs.microsoft.com/uwp/api/windows.perception.people.jointpose)

JointPose

#### [JointPoseAccuracy](https://docs.microsoft.com/uwp/api/windows.perception.people.jointposeaccuracy)

JointPoseAccuracy

### [Windows.Perception.Spatial](https://docs.microsoft.com/uwp/api/windows.perception.spatial)

#### [SpatialRay](https://docs.microsoft.com/uwp/api/windows.perception.spatial.spatialray)

SpatialRay

### [Windows.Perception.Spatial.Preview](https://docs.microsoft.com/uwp/api/windows.perception.spatial.preview)

#### [SpatialGraphInteropFrameOfReferencePreview](https://docs.microsoft.com/uwp/api/windows.perception.spatial.preview.spatialgraphinteropframeofreferencepreview)

SpatialGraphInteropFrameOfReferencePreview <br> SpatialGraphInteropFrameOfReferencePreview.CoordinateSystem <br> SpatialGraphInteropFrameOfReferencePreview.CoordinateSystemToNodeTransform <br> SpatialGraphInteropFrameOfReferencePreview.NodeId

#### [SpatialGraphInteropPreview](https://docs.microsoft.com/uwp/api/windows.perception.spatial.preview.spatialgraphinteroppreview)

SpatialGraphInteropPreview.TryCreateFrameOfReference <br> SpatialGraphInteropPreview.TryCreateFrameOfReference <br> SpatialGraphInteropPreview.TryCreateFrameOfReference

## Windows.Storage

### [Windows.Storage.AccessCache](https://docs.microsoft.com/uwp/api/windows.storage.accesscache)

#### [StorageApplicationPermissions](https://docs.microsoft.com/uwp/api/windows.storage.accesscache.storageapplicationpermissions)

StorageApplicationPermissions.GetFutureAccessListForUser <br> StorageApplicationPermissions.GetMostRecentlyUsedListForUser

### [Windows.Storage.Pickers](https://docs.microsoft.com/uwp/api/windows.storage.pickers)

#### [FileOpenPicker](https://docs.microsoft.com/uwp/api/windows.storage.pickers.fileopenpicker)

FileOpenPicker.CreateForUser <br> FileOpenPicker.User

#### [FileSavePicker](https://docs.microsoft.com/uwp/api/windows.storage.pickers.filesavepicker)

FileSavePicker.CreateForUser <br> FileSavePicker.User

#### [FolderPicker](https://docs.microsoft.com/uwp/api/windows.storage.pickers.folderpicker)

FolderPicker.CreateForUser <br> FolderPicker.User

## Windows.System

### [Windows.System](https://docs.microsoft.com/uwp/api/windows.system)

#### [DispatcherQueue](https://docs.microsoft.com/uwp/api/windows.system.dispatcherqueue)

DispatcherQueue.HasThreadAccess

### [Windows.System.Implementation.Holographic](https://docs.microsoft.com/uwp/api/windows.system.implementation.holographic)

#### [SysHolographicRuntimeRegistration](https://docs.microsoft.com/uwp/api/windows.system.implementation.holographic.sysholographicruntimeregistration)

SysHolographicRuntimeRegistration <br> SysHolographicRuntimeRegistration.ActiveRuntime <br> SysHolographicRuntimeRegistration.IsActive <br> SysHolographicRuntimeRegistration.MakeActiveAsync <br> SysHolographicRuntimeRegistration.#ctor

#### [SysHolographicWindowingEnvironment](https://docs.microsoft.com/uwp/api/windows.system.implementation.holographic.sysholographicwindowingenvironment)

SysHolographicWindowingEnvironment.HomeGestureDetected <br> SysHolographicWindowingEnvironment.IsHomeGestureReady <br> SysHolographicWindowingEnvironment.IsHomeGestureReadyChanged <br> SysHolographicWindowingEnvironment.IsSystemHomeGestureHandlerSuppressed

### [Windows.System.Profile](https://docs.microsoft.com/uwp/api/windows.system.profile)

#### [AppApplicability](https://docs.microsoft.com/uwp/api/windows.system.profile.appapplicability)

AppApplicability <br> AppApplicability.GetUnsupportedAppRequirements

#### [UnsupportedAppRequirement](https://docs.microsoft.com/uwp/api/windows.system.profile.unsupportedapprequirement)

UnsupportedAppRequirement

#### [UnsupportedAppRequirementReasons](https://docs.microsoft.com/uwp/api/windows.system.profile.unsupportedapprequirementreasons)

UnsupportedAppRequirementReasons

#### [UnsupportedAppRequirement](https://docs.microsoft.com/uwp/api/windows.system.profile.unsupportedapprequirement)

UnsupportedAppRequirement.Reasons <br> UnsupportedAppRequirement.Requirement

## Windows.UI

### [Windows.UI](https://docs.microsoft.com/uwp/api/windows.ui)

#### [UIContentRoot](https://docs.microsoft.com/uwp/api/windows.ui.uicontentroot)

UIContentRoot <br> UIContentRoot.UIContext

#### [UIContext](https://docs.microsoft.com/uwp/api/windows.ui.uicontext)

UIContext

### [Windows.UI.Composition](https://docs.microsoft.com/uwp/api/windows.ui.composition)

#### [CompositionGraphicsDevice](https://docs.microsoft.com/uwp/api/windows.ui.composition.compositiongraphicsdevice)

CompositionGraphicsDevice.CreateMipmapSurface <br> CompositionGraphicsDevice.Trim

#### [CompositionMipmapSurface](https://docs.microsoft.com/uwp/api/windows.ui.composition.compositionmipmapsurface)

CompositionMipmapSurface <br> CompositionMipmapSurface.AlphaMode <br> CompositionMipmapSurface.GetDrawingSurfaceForLevel <br> CompositionMipmapSurface.LevelCount <br> CompositionMipmapSurface.PixelFormat <br> CompositionMipmapSurface.SizeInt32

#### [CompositionProjectedShadow](https://docs.microsoft.com/uwp/api/windows.ui.composition.compositionprojectedshadow)

CompositionProjectedShadow

#### [CompositionProjectedShadowCaster](https://docs.microsoft.com/uwp/api/windows.ui.composition.compositionprojectedshadowcaster)

CompositionProjectedShadowCaster

#### [CompositionProjectedShadowCasterCollection](https://docs.microsoft.com/uwp/api/windows.ui.composition.compositionprojectedshadowcastercollection)

CompositionProjectedShadowCasterCollection <br> CompositionProjectedShadowCasterCollection.Count <br> CompositionProjectedShadowCasterCollection.First <br> CompositionProjectedShadowCasterCollection.InsertAbove <br> CompositionProjectedShadowCasterCollection.InsertAtBottom <br> CompositionProjectedShadowCasterCollection.InsertAtTop <br> CompositionProjectedShadowCasterCollection.InsertBelow <br> CompositionProjectedShadowCasterCollection.MaxRespectedCasters <br> CompositionProjectedShadowCasterCollection.RemoveAll <br> CompositionProjectedShadowCasterCollection.Remove

#### [CompositionProjectedShadowCaster](https://docs.microsoft.com/uwp/api/windows.ui.composition.compositionprojectedshadowcaster)

CompositionProjectedShadowCaster.Brush <br> CompositionProjectedShadowCaster.CastingVisual

#### [CompositionProjectedShadowReceiver](https://docs.microsoft.com/uwp/api/windows.ui.composition.compositionprojectedshadowreceiver)

CompositionProjectedShadowReceiver

#### [CompositionProjectedShadowReceiverUnorderedCollection](https://docs.microsoft.com/uwp/api/windows.ui.composition.compositionprojectedshadowreceiverunorderedcollection)

CompositionProjectedShadowReceiverUnorderedCollection <br> CompositionProjectedShadowReceiverUnorderedCollection.Add <br> CompositionProjectedShadowReceiverUnorderedCollection.Count <br> CompositionProjectedShadowReceiverUnorderedCollection.First <br> CompositionProjectedShadowReceiverUnorderedCollection.RemoveAll <br> CompositionProjectedShadowReceiverUnorderedCollection.Remove

#### [CompositionProjectedShadowReceiver](https://docs.microsoft.com/uwp/api/windows.ui.composition.compositionprojectedshadowreceiver)

CompositionProjectedShadowReceiver.ReceivingVisual

#### [CompositionProjectedShadow](https://docs.microsoft.com/uwp/api/windows.ui.composition.compositionprojectedshadow)

CompositionProjectedShadow.BlurRadiusMultiplier <br> CompositionProjectedShadow.Casters <br> CompositionProjectedShadow.LightSource <br> CompositionProjectedShadow.MaxBlurRadius <br> CompositionProjectedShadow.MinBlurRadius <br> CompositionProjectedShadow.Receivers

#### [CompositionRadialGradientBrush](https://docs.microsoft.com/uwp/api/windows.ui.composition.compositionradialgradientbrush)

CompositionRadialGradientBrush <br> CompositionRadialGradientBrush.EllipseCenter <br> CompositionRadialGradientBrush.EllipseRadius <br> CompositionRadialGradientBrush.GradientOriginOffset

#### [CompositionSurfaceBrush](https://docs.microsoft.com/uwp/api/windows.ui.composition.compositionsurfacebrush)

CompositionSurfaceBrush.SnapToPixels

#### [CompositionTransform](https://docs.microsoft.com/uwp/api/windows.ui.composition.compositiontransform)

CompositionTransform

#### [CompositionVisualSurface](https://docs.microsoft.com/uwp/api/windows.ui.composition.compositionvisualsurface)

CompositionVisualSurface <br> CompositionVisualSurface.SourceOffset <br> CompositionVisualSurface.SourceSize <br> CompositionVisualSurface.SourceVisual

#### [Compositor](https://docs.microsoft.com/uwp/api/windows.ui.composition.compositor)

Compositor.CreateProjectedShadowCaster <br> Compositor.CreateProjectedShadowReceiver <br> Compositor.CreateProjectedShadow <br> Compositor.CreateRadialGradientBrush <br> Compositor.CreateVisualSurface

#### [IVisualElement](https://docs.microsoft.com/uwp/api/windows.ui.composition.ivisualelement)

IVisualElement

### [Windows.UI.Composition.Interactions](https://docs.microsoft.com/uwp/api/windows.ui.composition.interactions)

#### [InteractionBindingAxisModes](https://docs.microsoft.com/uwp/api/windows.ui.composition.interactions.interactionbindingaxismodes)

InteractionBindingAxisModes

#### [InteractionTrackerCustomAnimationStateEnteredArgs](https://docs.microsoft.com/uwp/api/windows.ui.composition.interactions.interactiontrackercustomanimationstateenteredargs)

InteractionTrackerCustomAnimationStateEnteredArgs.IsFromBinding

#### [InteractionTrackerIdleStateEnteredArgs](https://docs.microsoft.com/uwp/api/windows.ui.composition.interactions.interactiontrackeridlestateenteredargs)

InteractionTrackerIdleStateEnteredArgs.IsFromBinding

#### [InteractionTrackerInertiaStateEnteredArgs](https://docs.microsoft.com/uwp/api/windows.ui.composition.interactions.interactiontrackerinertiastateenteredargs)

InteractionTrackerInertiaStateEnteredArgs.IsFromBinding

#### [InteractionTrackerInteractingStateEnteredArgs](https://docs.microsoft.com/uwp/api/windows.ui.composition.interactions.interactiontrackerinteractingstateenteredargs)

InteractionTrackerInteractingStateEnteredArgs.IsFromBinding

#### [InteractionTracker](https://docs.microsoft.com/uwp/api/windows.ui.composition.interactions.interactiontracker)

InteractionTracker.GetBindingMode <br> InteractionTracker.SetBindingMode

#### [VisualInteractionSource](https://docs.microsoft.com/uwp/api/windows.ui.composition.interactions.visualinteractionsource)

VisualInteractionSource.CreateFromIVisualElement

### [Windows.UI.Composition.Scenes](https://docs.microsoft.com/uwp/api/windows.ui.composition.scenes)

#### [SceneAlphaMode](https://docs.microsoft.com/uwp/api/windows.ui.composition.scenes.scenealphamode)

SceneAlphaMode

#### [SceneAttributeSemantic](https://docs.microsoft.com/uwp/api/windows.ui.composition.scenes.sceneattributesemantic)

SceneAttributeSemantic

#### [SceneBoundingBox](https://docs.microsoft.com/uwp/api/windows.ui.composition.scenes.sceneboundingbox)

SceneBoundingBox <br> SceneBoundingBox.Center <br> SceneBoundingBox.Extents <br> SceneBoundingBox.Max <br> SceneBoundingBox.Min <br> SceneBoundingBox.Size

#### [SceneComponent](https://docs.microsoft.com/uwp/api/windows.ui.composition.scenes.scenecomponent)

SceneComponent

#### [SceneComponentCollection](https://docs.microsoft.com/uwp/api/windows.ui.composition.scenes.scenecomponentcollection)

SceneComponentCollection <br> SceneComponentCollection.Append <br> SceneComponentCollection.Clear <br> SceneComponentCollection.First <br> SceneComponentCollection.GetAt <br> SceneComponentCollection.GetMany <br> SceneComponentCollection.GetView <br> SceneComponentCollection.IndexOf <br> SceneComponentCollection.InsertAt <br> SceneComponentCollection.RemoveAtEnd <br> SceneComponentCollection.RemoveAt <br> SceneComponentCollection.ReplaceAll <br> SceneComponentCollection.SetAt <br> SceneComponentCollection.Size

#### [SceneComponentType](https://docs.microsoft.com/uwp/api/windows.ui.composition.scenes.scenecomponenttype)

SceneComponentType

#### [SceneComponent](https://docs.microsoft.com/uwp/api/windows.ui.composition.scenes.scenecomponent)

SceneComponent.ComponentType

#### [SceneMaterial](https://docs.microsoft.com/uwp/api/windows.ui.composition.scenes.scenematerial)

SceneMaterial

#### [SceneMaterialInput](https://docs.microsoft.com/uwp/api/windows.ui.composition.scenes.scenematerialinput)

SceneMaterialInput

#### [SceneMesh](https://docs.microsoft.com/uwp/api/windows.ui.composition.scenes.scenemesh)

SceneMesh

#### [SceneMeshMaterialAttributeMap](https://docs.microsoft.com/uwp/api/windows.ui.composition.scenes.scenemeshmaterialattributemap)

SceneMeshMaterialAttributeMap <br> SceneMeshMaterialAttributeMap.Clear <br> SceneMeshMaterialAttributeMap.First <br> SceneMeshMaterialAttributeMap.GetView <br> SceneMeshMaterialAttributeMap.HasKey <br> SceneMeshMaterialAttributeMap.Insert <br> SceneMeshMaterialAttributeMap.Lookup <br> SceneMeshMaterialAttributeMap.Remove <br> SceneMeshMaterialAttributeMap.Size

#### [SceneMeshRendererComponent](https://docs.microsoft.com/uwp/api/windows.ui.composition.scenes.scenemeshrenderercomponent)

SceneMeshRendererComponent <br> SceneMeshRendererComponent.Create <br> SceneMeshRendererComponent.Material <br> SceneMeshRendererComponent.Mesh <br> SceneMeshRendererComponent.UVMappings

#### [SceneMesh](https://docs.microsoft.com/uwp/api/windows.ui.composition.scenes.scenemesh)

SceneMesh.Bounds <br> SceneMesh.Create <br> SceneMesh.FillMeshAttribute <br> SceneMesh.PrimitiveTopology

#### [SceneMetallicRoughnessMaterial](https://docs.microsoft.com/uwp/api/windows.ui.composition.scenes.scenemetallicroughnessmaterial)

SceneMetallicRoughnessMaterial <br> SceneMetallicRoughnessMaterial.BaseColorFactor <br> SceneMetallicRoughnessMaterial.BaseColorInput <br> SceneMetallicRoughnessMaterial.Create <br> SceneMetallicRoughnessMaterial.MetallicFactor <br> SceneMetallicRoughnessMaterial.MetallicRoughnessInput <br> SceneMetallicRoughnessMaterial.RoughnessFactor

#### [SceneModelTransform](https://docs.microsoft.com/uwp/api/windows.ui.composition.scenes.scenemodeltransform)

SceneModelTransform <br> SceneModelTransform.Orientation <br> SceneModelTransform.RotationAngle <br> SceneModelTransform.RotationAngleInDegrees <br> SceneModelTransform.RotationAxis <br> SceneModelTransform.Scale <br> SceneModelTransform.Translation

#### [SceneNode](https://docs.microsoft.com/uwp/api/windows.ui.composition.scenes.scenenode)

SceneNode

#### [SceneNodeCollection](https://docs.microsoft.com/uwp/api/windows.ui.composition.scenes.scenenodecollection)

SceneNodeCollection <br> SceneNodeCollection.Append <br> SceneNodeCollection.Clear <br> SceneNodeCollection.First <br> SceneNodeCollection.GetAt <br> SceneNodeCollection.GetMany <br> SceneNodeCollection.GetView <br> SceneNodeCollection.IndexOf <br> SceneNodeCollection.InsertAt <br> SceneNodeCollection.RemoveAtEnd <br> SceneNodeCollection.RemoveAt <br> SceneNodeCollection.ReplaceAll <br> SceneNodeCollection.SetAt <br> SceneNodeCollection.Size

#### [SceneNode](https://docs.microsoft.com/uwp/api/windows.ui.composition.scenes.scenenode)

SceneNode.Children <br> SceneNode.Components <br> SceneNode.Create <br> SceneNode.FindFirstComponentOfType <br> SceneNode.Parent <br> SceneNode.Transform

#### [SceneObject](https://docs.microsoft.com/uwp/api/windows.ui.composition.scenes.sceneobject)

SceneObject

#### [ScenePbrMaterial](https://docs.microsoft.com/uwp/api/windows.ui.composition.scenes.scenepbrmaterial)

ScenePbrMaterial <br> ScenePbrMaterial.AlphaCutoff <br> ScenePbrMaterial.AlphaMode <br> ScenePbrMaterial.EmissiveFactor <br> ScenePbrMaterial.EmissiveInput <br> ScenePbrMaterial.IsDoubleSided <br> ScenePbrMaterial.NormalInput <br> ScenePbrMaterial.NormalScale <br> ScenePbrMaterial.OcclusionInput <br> ScenePbrMaterial.OcclusionStrength

#### [SceneRendererComponent](https://docs.microsoft.com/uwp/api/windows.ui.composition.scenes.scenerenderercomponent)

SceneRendererComponent

#### [SceneSurfaceMaterialInput](https://docs.microsoft.com/uwp/api/windows.ui.composition.scenes.scenesurfacematerialinput)

SceneSurfaceMaterialInput <br> SceneSurfaceMaterialInput.BitmapInterpolationMode <br> SceneSurfaceMaterialInput.Create <br> SceneSurfaceMaterialInput.Surface <br> SceneSurfaceMaterialInput.WrappingUMode <br> SceneSurfaceMaterialInput.WrappingVMode

#### [SceneVisual](https://docs.microsoft.com/uwp/api/windows.ui.composition.scenes.scenevisual)

SceneVisual <br> SceneVisual.Create <br> SceneVisual.Root

#### [SceneWrappingMode](https://docs.microsoft.com/uwp/api/windows.ui.composition.scenes.scenewrappingmode)

SceneWrappingMode

### [Windows.UI.Core](https://docs.microsoft.com/uwp/api/windows.ui.core)

#### [CoreWindow](https://docs.microsoft.com/uwp/api/windows.ui.core.corewindow)

CoreWindow.UIContext

### [Windows.UI.Core.Preview](https://docs.microsoft.com/uwp/api/windows.ui.core.preview)

#### [CoreAppWindowPreview](https://docs.microsoft.com/uwp/api/windows.ui.core.preview.coreappwindowpreview)

CoreAppWindowPreview <br> CoreAppWindowPreview.GetIdFromWindow

### [Windows.UI.Input](https://docs.microsoft.com/uwp/api/windows.ui.input)

#### [AttachableInputObject](https://docs.microsoft.com/uwp/api/windows.ui.input.attachableinputobject)

AttachableInputObject <br> AttachableInputObject.Close

#### [GazeInputAccessStatus](https://docs.microsoft.com/uwp/api/windows.ui.input.gazeinputaccessstatus)

GazeInputAccessStatus

#### [InputActivationListener](https://docs.microsoft.com/uwp/api/windows.ui.input.inputactivationlistener)

InputActivationListener

#### [InputActivationListenerActivationChangedEventArgs](https://docs.microsoft.com/uwp/api/windows.ui.input.inputactivationlisteneractivationchangedeventargs)

InputActivationListenerActivationChangedEventArgs <br> InputActivationListenerActivationChangedEventArgs.State

#### [InputActivationListener](https://docs.microsoft.com/uwp/api/windows.ui.input.inputactivationlistener)

InputActivationListener.InputActivationChanged <br> InputActivationListener.State

#### [InputActivationState](https://docs.microsoft.com/uwp/api/windows.ui.input.inputactivationstate)

InputActivationState

### [Windows.UI.Input.Preview](https://docs.microsoft.com/uwp/api/windows.ui.input.preview)

#### [InputActivationListenerPreview](https://docs.microsoft.com/uwp/api/windows.ui.input.preview.inputactivationlistenerpreview)

InputActivationListenerPreview <br> InputActivationListenerPreview.CreateForApplicationWindow

### [Windows.UI.Input.Spatial](https://docs.microsoft.com/uwp/api/windows.ui.input.spatial)

#### [SpatialInteractionManager](https://docs.microsoft.com/uwp/api/windows.ui.input.spatial.spatialinteractionmanager)

SpatialInteractionManager.IsSourceKindSupported

#### [SpatialInteractionSourceState](https://docs.microsoft.com/uwp/api/windows.ui.input.spatial.spatialinteractionsourcestate)

SpatialInteractionSourceState.TryGetHandPose

#### [SpatialInteractionSource](https://docs.microsoft.com/uwp/api/windows.ui.input.spatial.spatialinteractionsource)

SpatialInteractionSource.TryCreateHandMeshObserverAsync <br> SpatialInteractionSource.TryCreateHandMeshObserver

#### [SpatialPointerPose](https://docs.microsoft.com/uwp/api/windows.ui.input.spatial.spatialpointerpose)

SpatialPointerPose.Eyes <br> SpatialPointerPose.IsHeadCapturedBySystem

### [Windows.UI.Notifications](https://docs.microsoft.com/uwp/api/windows.ui.notifications)

#### [ToastActivatedEventArgs](https://docs.microsoft.com/uwp/api/windows.ui.notifications.toastactivatedeventargs)

ToastActivatedEventArgs.UserInput

#### [ToastNotification](https://docs.microsoft.com/uwp/api/windows.ui.notifications.toastnotification)

ToastNotification.ExpiresOnReboot

### [Windows.UI.ViewManagement](https://docs.microsoft.com/uwp/api/windows.ui.viewmanagement)

#### [ApplicationView](https://docs.microsoft.com/uwp/api/windows.ui.viewmanagement.applicationview)

ApplicationView.ClearAllPersistedState <br> ApplicationView.ClearPersistedState <br> ApplicationView.GetDisplayRegions <br> ApplicationView.PersistedStateId <br> ApplicationView.UIContext <br> ApplicationView.WindowingEnvironment

#### [InputPane](https://docs.microsoft.com/uwp/api/windows.ui.viewmanagement.inputpane)

InputPane.GetForUIContext

#### [UISettingsAutoHideScrollBarsChangedEventArgs](https://docs.microsoft.com/uwp/api/windows.ui.viewmanagement.uisettingsautohidescrollbarschangedeventargs)

UISettingsAutoHideScrollBarsChangedEventArgs

#### [UISettings](https://docs.microsoft.com/uwp/api/windows.ui.viewmanagement.uisettings)

UISettings.AutoHideScrollBars <br> UISettings.AutoHideScrollBarsChanged

### [Windows.UI.ViewManagement.Core](https://docs.microsoft.com/uwp/api/windows.ui.viewmanagement.core)

#### [CoreInputView](https://docs.microsoft.com/uwp/api/windows.ui.viewmanagement.core.coreinputview)

CoreInputView.GetForUIContext

### [Windows.UI.WindowManagement](https://docs.microsoft.com/uwp/api/windows.ui.windowmanagement)

#### [AppWindow](https://docs.microsoft.com/uwp/api/windows.ui.windowmanagement.appwindow)

AppWindow

#### [AppWindowChangedEventArgs](https://docs.microsoft.com/uwp/api/windows.ui.windowmanagement.appwindowchangedeventargs)

AppWindowChangedEventArgs <br> AppWindowChangedEventArgs.DidAvailableWindowPresentationsChange <br> AppWindowChangedEventArgs.DidDisplayRegionsChange <br> AppWindowChangedEventArgs.DidFrameChange <br> AppWindowChangedEventArgs.DidSizeChange <br> AppWindowChangedEventArgs.DidTitleBarChange <br> AppWindowChangedEventArgs.DidVisibilityChange <br> AppWindowChangedEventArgs.DidWindowingEnvironmentChange <br> AppWindowChangedEventArgs.DidWindowPresentationChange

#### [AppWindowClosedEventArgs](https://docs.microsoft.com/uwp/api/windows.ui.windowmanagement.appwindowclosedeventargs)

AppWindowClosedEventArgs <br> AppWindowClosedEventArgs.Reason

#### [AppWindowClosedReason](https://docs.microsoft.com/uwp/api/windows.ui.windowmanagement.appwindowclosedreason)

AppWindowClosedReason

#### [AppWindowCloseRequestedEventArgs](https://docs.microsoft.com/uwp/api/windows.ui.windowmanagement.appwindowcloserequestedeventargs)

AppWindowCloseRequestedEventArgs <br> AppWindowCloseRequestedEventArgs.Cancel <br> AppWindowCloseRequestedEventArgs.GetDeferral

#### [AppWindowFrame](https://docs.microsoft.com/uwp/api/windows.ui.windowmanagement.appwindowframe)

AppWindowFrame

#### [AppWindowFrameStyle](https://docs.microsoft.com/uwp/api/windows.ui.windowmanagement.appwindowframestyle)

AppWindowFrameStyle

#### [AppWindowFrame](https://docs.microsoft.com/uwp/api/windows.ui.windowmanagement.appwindowframe)

AppWindowFrame.DragRegionVisuals <br> AppWindowFrame.GetFrameStyle <br> AppWindowFrame.SetFrameStyle

#### [AppWindowPlacement](https://docs.microsoft.com/uwp/api/windows.ui.windowmanagement.appwindowplacement)

AppWindowPlacement <br> AppWindowPlacement.DisplayRegion <br> AppWindowPlacement.Offset <br> AppWindowPlacement.Size

#### [AppWindowPresentationConfiguration](https://docs.microsoft.com/uwp/api/windows.ui.windowmanagement.appwindowpresentationconfiguration)

AppWindowPresentationConfiguration <br> AppWindowPresentationConfiguration.Kind

#### [AppWindowPresentationKind](https://docs.microsoft.com/uwp/api/windows.ui.windowmanagement.appwindowpresentationkind)

AppWindowPresentationKind

#### [AppWindowPresenter](https://docs.microsoft.com/uwp/api/windows.ui.windowmanagement.appwindowpresenter)

AppWindowPresenter <br> AppWindowPresenter.GetConfiguration <br> AppWindowPresenter.IsPresentationSupported <br> AppWindowPresenter.RequestPresentation <br> AppWindowPresenter.RequestPresentation

#### [AppWindowTitleBar](https://docs.microsoft.com/uwp/api/windows.ui.windowmanagement.appwindowtitlebar)

AppWindowTitleBar

#### [AppWindowTitleBarOcclusion](https://docs.microsoft.com/uwp/api/windows.ui.windowmanagement.appwindowtitlebarocclusion)

AppWindowTitleBarOcclusion <br> AppWindowTitleBarOcclusion.OccludingRect

#### [AppWindowTitleBarVisibility](https://docs.microsoft.com/uwp/api/windows.ui.windowmanagement.appwindowtitlebarvisibility)

AppWindowTitleBarVisibility

#### [AppWindowTitleBar](https://docs.microsoft.com/uwp/api/windows.ui.windowmanagement.appwindowtitlebar)

AppWindowTitleBar.BackgroundColor <br> AppWindowTitleBar.ButtonBackgroundColor <br> AppWindowTitleBar.ButtonForegroundColor <br> AppWindowTitleBar.ButtonHoverBackgroundColor <br> AppWindowTitleBar.ButtonHoverForegroundColor <br> AppWindowTitleBar.ButtonInactiveBackgroundColor <br> AppWindowTitleBar.ButtonInactiveForegroundColor <br> AppWindowTitleBar.ButtonPressedBackgroundColor <br> AppWindowTitleBar.ButtonPressedForegroundColor <br> AppWindowTitleBar.ExtendsContentIntoTitleBar <br> AppWindowTitleBar.ForegroundColor <br> AppWindowTitleBar.GetPreferredVisibility <br> AppWindowTitleBar.GetTitleBarOcclusions <br> AppWindowTitleBar.InactiveBackgroundColor <br> AppWindowTitleBar.InactiveForegroundColor <br> AppWindowTitleBar.IsVisible <br> AppWindowTitleBar.SetPreferredVisibility

#### [AppWindow](https://docs.microsoft.com/uwp/api/windows.ui.windowmanagement.appwindow)

AppWindow.Changed <br> AppWindow.ClearAllPersistedState <br> AppWindow.ClearPersistedState <br> AppWindow.CloseAsync <br> AppWindow.Closed <br> AppWindow.CloseRequested <br> AppWindow.Content <br> AppWindow.DispatcherQueue <br> AppWindow.Frame <br> AppWindow.GetDisplayRegions <br> AppWindow.GetPlacement <br> AppWindow.IsVisible <br> AppWindow.PersistedStateId <br> AppWindow.Presenter <br> AppWindow.RequestMoveAdjacentToCurrentView <br> AppWindow.RequestMoveAdjacentToWindow <br> AppWindow.RequestMoveRelativeToDisplayRegion <br> AppWindow.RequestMoveToDisplayRegion <br> AppWindow.RequestSize <br> AppWindow.Title <br> AppWindow.TitleBar <br> AppWindow.TryCreateAsync <br> AppWindow.TryShowAsync <br> AppWindow.UIContext <br> AppWindow.WindowingEnvironment

#### [CompactOverlayPresentationConfiguration](https://docs.microsoft.com/uwp/api/windows.ui.windowmanagement.compactoverlaypresentationconfiguration)

CompactOverlayPresentationConfiguration <br> CompactOverlayPresentationConfiguration.#ctor

#### [DefaultPresentationConfiguration](https://docs.microsoft.com/uwp/api/windows.ui.windowmanagement.defaultpresentationconfiguration)

DefaultPresentationConfiguration <br> DefaultPresentationConfiguration.#ctor

#### [DisplayRegion](https://docs.microsoft.com/uwp/api/windows.ui.windowmanagement.displayregion)

DisplayRegion <br> DisplayRegion.Changed <br> DisplayRegion.DisplayMonitorDeviceId <br> DisplayRegion.IsVisible <br> DisplayRegion.WindowingEnvironment <br> DisplayRegion.WorkAreaOffset <br> DisplayRegion.WorkAreaSize

#### [FullScreenPresentationConfiguration](https://docs.microsoft.com/uwp/api/windows.ui.windowmanagement.fullscreenpresentationconfiguration)

FullScreenPresentationConfiguration <br> FullScreenPresentationConfiguration.#ctor <br> FullScreenPresentationConfiguration.IsExclusive

#### [WindowingEnvironment](https://docs.microsoft.com/uwp/api/windows.ui.windowmanagement.windowingenvironment)

WindowingEnvironment

#### [WindowingEnvironmentAddedEventArgs](https://docs.microsoft.com/uwp/api/windows.ui.windowmanagement.windowingenvironmentaddedeventargs)

WindowingEnvironmentAddedEventArgs <br> WindowingEnvironmentAddedEventArgs.WindowingEnvironment

#### [WindowingEnvironmentChangedEventArgs](https://docs.microsoft.com/uwp/api/windows.ui.windowmanagement.windowingenvironmentchangedeventargs)

WindowingEnvironmentChangedEventArgs

#### [WindowingEnvironmentKind](https://docs.microsoft.com/uwp/api/windows.ui.windowmanagement.windowingenvironmentkind)

WindowingEnvironmentKind

#### [WindowingEnvironmentRemovedEventArgs](https://docs.microsoft.com/uwp/api/windows.ui.windowmanagement.windowingenvironmentremovedeventargs)

WindowingEnvironmentRemovedEventArgs <br> WindowingEnvironmentRemovedEventArgs.WindowingEnvironment

#### [WindowingEnvironment](https://docs.microsoft.com/uwp/api/windows.ui.windowmanagement.windowingenvironment)

WindowingEnvironment.Changed <br> WindowingEnvironment.FindAll <br> WindowingEnvironment.FindAll <br> WindowingEnvironment.GetDisplayRegions <br> WindowingEnvironment.IsEnabled <br> WindowingEnvironment.Kind

### [Windows.UI.WindowManagement.Preview](https://docs.microsoft.com/uwp/api/windows.ui.windowmanagement.preview)

#### [WindowManagementPreview](https://docs.microsoft.com/uwp/api/windows.ui.windowmanagement.preview.windowmanagementpreview)

WindowManagementPreview <br> WindowManagementPreview.SetPreferredMinSize

### [Windows.UI.Xaml](https://docs.microsoft.com/uwp/api/windows.ui.xaml)

#### [UIElementWeakCollection](https://docs.microsoft.com/uwp/api/windows.ui.xaml.uielementweakcollection)

UIElementWeakCollection <br> UIElementWeakCollection.Append <br> UIElementWeakCollection.Clear <br> UIElementWeakCollection.First <br> UIElementWeakCollection.GetAt <br> UIElementWeakCollection.GetMany <br> UIElementWeakCollection.GetView <br> UIElementWeakCollection.IndexOf <br> UIElementWeakCollection.InsertAt <br> UIElementWeakCollection.RemoveAtEnd <br> UIElementWeakCollection.RemoveAt <br> UIElementWeakCollection.ReplaceAll <br> UIElementWeakCollection.SetAt <br> UIElementWeakCollection.Size <br> UIElementWeakCollection.#ctor

#### [UIElement](https://docs.microsoft.com/uwp/api/windows.ui.xaml.uielement)

UIElement.ActualOffset <br> UIElement.ActualSize <br> UIElement.Shadow <br> UIElement.ShadowProperty <br> UIElement.UIContext <br> UIElement.XamlRoot

#### [Window](https://docs.microsoft.com/uwp/api/windows.ui.xaml.window)

Window.UIContext

#### [XamlRoot](https://docs.microsoft.com/uwp/api/windows.ui.xaml.xamlroot)

XamlRoot

#### [XamlRootChangedEventArgs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.xamlrootchangedeventargs)

XamlRootChangedEventArgs

#### [XamlRoot](https://docs.microsoft.com/uwp/api/windows.ui.xaml.xamlroot)

XamlRoot.Changed <br> XamlRoot.Content <br> XamlRoot.IsHostVisible <br> XamlRoot.RasterizationScale <br> XamlRoot.Size <br> XamlRoot.UIContext

### [Windows.UI.Xaml.Controls](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls)

#### [DatePickerFlyoutPresenter](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.datepickerflyoutpresenter)

DatePickerFlyoutPresenter.IsDefaultShadowEnabled <br> DatePickerFlyoutPresenter.IsDefaultShadowEnabledProperty

#### [FlyoutPresenter](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.flyoutpresenter)

FlyoutPresenter.IsDefaultShadowEnabled <br> FlyoutPresenter.IsDefaultShadowEnabledProperty

#### [InkToolbar](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.inktoolbar)

InkToolbar.TargetInkPresenter <br> InkToolbar.TargetInkPresenterProperty

#### [MenuFlyoutPresenter](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.menuflyoutpresenter)

MenuFlyoutPresenter.IsDefaultShadowEnabled <br> MenuFlyoutPresenter.IsDefaultShadowEnabledProperty

#### [TimePickerFlyoutPresenter](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.timepickerflyoutpresenter)

TimePickerFlyoutPresenter.IsDefaultShadowEnabled <br> TimePickerFlyoutPresenter.IsDefaultShadowEnabledProperty

#### [TwoPaneView](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.twopaneview)

TwoPaneView

#### [TwoPaneViewMode](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.twopaneviewmode)

TwoPaneViewMode

#### [TwoPaneViewPriority](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.twopaneviewpriority)

TwoPaneViewPriority

#### [TwoPaneViewTallModeConfiguration](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.twopaneviewtallmodeconfiguration)

TwoPaneViewTallModeConfiguration

#### [TwoPaneView](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.twopaneview)

TwoPaneView.MinTallModeHeight <br> TwoPaneView.MinTallModeHeightProperty <br> TwoPaneView.MinWideModeWidth <br> TwoPaneView.MinWideModeWidthProperty <br> TwoPaneView.Mode <br> TwoPaneView.ModeChanged <br> TwoPaneView.ModeProperty <br> TwoPaneView.Pane1 <br> TwoPaneView.Pane1Length <br> TwoPaneView.Pane1LengthProperty <br> TwoPaneView.Pane1Property <br> TwoPaneView.Pane2 <br> TwoPaneView.Pane2Length <br> TwoPaneView.Pane2LengthProperty <br> TwoPaneView.Pane2Property <br> TwoPaneView.PanePriority <br> TwoPaneView.PanePriorityProperty <br> TwoPaneView.TallModeConfiguration <br> TwoPaneView.TallModeConfigurationProperty <br> TwoPaneView.#ctor <br> TwoPaneView.WideModeConfiguration <br> TwoPaneView.WideModeConfigurationProperty

### [Windows.UI.Xaml.Controls.Maps](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.maps)

#### [MapControl](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.maps.mapcontrol)

MapControl.CanTiltDown <br> MapControl.CanTiltDownProperty <br> MapControl.CanTiltUp <br> MapControl.CanTiltUpProperty <br> MapControl.CanZoomIn <br> MapControl.CanZoomInProperty <br> MapControl.CanZoomOut <br> MapControl.CanZoomOutProperty

### [Windows.UI.Xaml.Controls.Primitives](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.primitives)

#### [AppBarTemplateSettings](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.primitives.appbartemplatesettings)

AppBarTemplateSettings.NegativeCompactVerticalDelta <br> AppBarTemplateSettings.NegativeHiddenVerticalDelta <br> AppBarTemplateSettings.NegativeMinimalVerticalDelta

#### [CommandBarTemplateSettings](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.primitives.commandbartemplatesettings)

CommandBarTemplateSettings.OverflowContentCompactYTranslation <br> CommandBarTemplateSettings.OverflowContentHiddenYTranslation <br> CommandBarTemplateSettings.OverflowContentMinimalYTranslation

#### [FlyoutBase](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.primitives.flyoutbase)

FlyoutBase.IsConstrainedToRootBounds <br> FlyoutBase.ShouldConstrainToRootBounds <br> FlyoutBase.ShouldConstrainToRootBoundsProperty <br> FlyoutBase.XamlRoot

#### [Popup](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.primitives.popup)

Popup.IsConstrainedToRootBounds <br> Popup.ShouldConstrainToRootBounds <br> Popup.ShouldConstrainToRootBoundsProperty

### [Windows.UI.Xaml.Documents](https://docs.microsoft.com/uwp/api/windows.ui.xaml.documents)

#### [TextElement](https://docs.microsoft.com/uwp/api/windows.ui.xaml.documents.textelement)

TextElement.XamlRoot

### [Windows.UI.Xaml.Hosting](https://docs.microsoft.com/uwp/api/windows.ui.xaml.hosting)

#### [ElementCompositionPreview](https://docs.microsoft.com/uwp/api/windows.ui.xaml.hosting.elementcompositionpreview)

ElementCompositionPreview.GetAppWindowContent <br> ElementCompositionPreview.SetAppWindowContent

### [Windows.UI.Xaml.Input](https://docs.microsoft.com/uwp/api/windows.ui.xaml.input)

#### [FocusManager](https://docs.microsoft.com/uwp/api/windows.ui.xaml.input.focusmanager)

FocusManager.GetFocusedElement

### [Windows.UI.Xaml.Media](https://docs.microsoft.com/uwp/api/windows.ui.xaml.media)

#### [AcrylicBrush](https://docs.microsoft.com/uwp/api/windows.ui.xaml.media.acrylicbrush)

AcrylicBrush.TintLuminosityOpacity <br> AcrylicBrush.TintLuminosityOpacityProperty

#### [Shadow](https://docs.microsoft.com/uwp/api/windows.ui.xaml.media.shadow)

Shadow

#### [ThemeShadow](https://docs.microsoft.com/uwp/api/windows.ui.xaml.media.themeshadow)

ThemeShadow <br> ThemeShadow.Receivers <br> ThemeShadow.#ctor

#### [VisualTreeHelper](https://docs.microsoft.com/uwp/api/windows.ui.xaml.media.visualtreehelper)

VisualTreeHelper.GetOpenPopupsForXamlRoot

### [Windows.UI.Xaml.Media.Animation](https://docs.microsoft.com/uwp/api/windows.ui.xaml.media.animation)

#### [GravityConnectedAnimationConfiguration](https://docs.microsoft.com/uwp/api/windows.ui.xaml.media.animation.gravityconnectedanimationconfiguration)

GravityConnectedAnimationConfiguration.IsShadowEnabled

## Windows.Web

### [Windows.Web.Http](https://docs.microsoft.com/uwp/api/windows.web.http)

#### [HttpClient](https://docs.microsoft.com/uwp/api/windows.web.http.httpclient)

HttpClient.TryDeleteAsync <br> HttpClient.TryGetAsync <br> HttpClient.TryGetAsync <br> HttpClient.TryGetBufferAsync <br> HttpClient.TryGetInputStreamAsync <br> HttpClient.TryGetStringAsync <br> HttpClient.TryPostAsync <br> HttpClient.TryPutAsync <br> HttpClient.TrySendRequestAsync <br> HttpClient.TrySendRequestAsync

#### [HttpGetBufferResult](https://docs.microsoft.com/uwp/api/windows.web.http.httpgetbufferresult)

HttpGetBufferResult <br> HttpGetBufferResult.Close <br> HttpGetBufferResult.ExtendedError <br> HttpGetBufferResult.RequestMessage <br> HttpGetBufferResult.ResponseMessage <br> HttpGetBufferResult.Succeeded <br> HttpGetBufferResult.ToString <br> HttpGetBufferResult.Value

#### [HttpGetInputStreamResult](https://docs.microsoft.com/uwp/api/windows.web.http.httpgetinputstreamresult)

HttpGetInputStreamResult <br> HttpGetInputStreamResult.Close <br> HttpGetInputStreamResult.ExtendedError <br> HttpGetInputStreamResult.RequestMessage <br> HttpGetInputStreamResult.ResponseMessage <br> HttpGetInputStreamResult.Succeeded <br> HttpGetInputStreamResult.ToString <br> HttpGetInputStreamResult.Value

#### [HttpGetStringResult](https://docs.microsoft.com/uwp/api/windows.web.http.httpgetstringresult)

HttpGetStringResult <br> HttpGetStringResult.Close <br> HttpGetStringResult.ExtendedError <br> HttpGetStringResult.RequestMessage <br> HttpGetStringResult.ResponseMessage <br> HttpGetStringResult.Succeeded <br> HttpGetStringResult.ToString <br> HttpGetStringResult.Value

#### [HttpRequestResult](https://docs.microsoft.com/uwp/api/windows.web.http.httprequestresult)

HttpRequestResult <br> HttpRequestResult.Close <br> HttpRequestResult.ExtendedError <br> HttpRequestResult.RequestMessage <br> HttpRequestResult.ResponseMessage <br> HttpRequestResult.Succeeded <br> HttpRequestResult.ToString

### [Windows.Web.Http.Filters](https://docs.microsoft.com/uwp/api/windows.web.http.filters)

#### [HttpBaseProtocolFilter](https://docs.microsoft.com/uwp/api/windows.web.http.filters.httpbaseprotocolfilter)

HttpBaseProtocolFilter.CreateForUser <br> HttpBaseProtocolFilter.User

IWebViewControl2 <br> IWebViewControl2.AddInitializeScript
