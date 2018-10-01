---
author: QuinnRadich
title: Windows 10 Build 17763 API changes
description: Developers can use the following list to identify new or changed namespaces in Windows 10 build 17763
keywords: what's new, whats new, updates, Windows 10, newest, apis, 17763, october
ms.author: quradic
ms.date: 10/02/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
ms.localizationpriority: medium
---

# New APIs in Windows 10 build 17763

New and updated API namespaces have been made available to developers in Windows 10 build 17763 (Also known as the October 2018 Update or version 1809). Below is a full list of documentation published for namespaces added or modified in this release.

For information on APIs added in the previous public release, see [New APIs in the Windows 10 April Update](windows-10-build-17134-api-diff.md).

## windows.ai

### [windows.ai.machinelearning](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning)

#### [ilearningmodelfeaturedescriptor](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.ilearningmodelfeaturedescriptor)

ilearningmodelfeaturedescriptor <br> ilearningmodelfeaturedescriptor.description <br> ilearningmodelfeaturedescriptor.isrequired <br> ilearningmodelfeaturedescriptor.kind <br> ilearningmodelfeaturedescriptor.name

#### [ilearningmodelfeaturevalue](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.ilearningmodelfeaturevalue)

ilearningmodelfeaturevalue <br> ilearningmodelfeaturevalue.kind

#### [ilearningmodeloperatorprovider](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.ilearningmodeloperatorprovider)

ilearningmodeloperatorprovider

#### [imagefeaturedescriptor](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.imagefeaturedescriptor)

imagefeaturedescriptor <br> imagefeaturedescriptor.bitmapalphamode <br> imagefeaturedescriptor.bitmappixelformat <br> imagefeaturedescriptor.description <br> imagefeaturedescriptor.height <br> imagefeaturedescriptor.isrequired <br> imagefeaturedescriptor.kind <br> imagefeaturedescriptor.name <br> imagefeaturedescriptor.width

#### [imagefeaturevalue](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.imagefeaturevalue)

imagefeaturevalue <br> imagefeaturevalue.createfromvideoframe <br> imagefeaturevalue.kind <br> imagefeaturevalue.videoframe

#### [itensor](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.itensor)

itensor <br> itensor.shape <br> itensor.tensorkind

#### [learningmodel](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.learningmodel)

learningmodel <br> learningmodel.author <br> learningmodel.close <br> learningmodel.description <br> learningmodel.domain <br> learningmodel.inputfeatures <br> learningmodel.loadfromfilepath <br> learningmodel.loadfromfilepath <br> learningmodel.loadfromstoragefileasync <br> learningmodel.loadfromstoragefileasync <br> learningmodel.loadfromstream <br> learningmodel.loadfromstream <br> learningmodel.loadfromstreamasync <br> learningmodel.loadfromstreamasync <br> learningmodel.metadata <br> learningmodel.name <br> learningmodel.outputfeatures <br> learningmodel.version

#### [learningmodelbinding](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.learningmodelbinding)

learningmodelbinding <br> learningmodelbinding.bind <br> learningmodelbinding.bind <br> learningmodelbinding.clear <br> learningmodelbinding.first <br> learningmodelbinding.haskey <br> learningmodelbinding.learningmodelbinding <br> learningmodelbinding.lookup <br> learningmodelbinding.size <br> learningmodelbinding.split

#### [learningmodeldevice](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.learningmodeldevice)

learningmodeldevice <br> learningmodeldevice.adapterid <br> learningmodeldevice.createfromdirect3d11device <br> learningmodeldevice.direct3d11device <br> learningmodeldevice.learningmodeldevice

#### [learningmodeldevicekind](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.learningmodeldevicekind)

learningmodeldevicekind

#### [learningmodelevaluationresult](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.learningmodelevaluationresult)

learningmodelevaluationresult <br> learningmodelevaluationresult.correlationid <br> learningmodelevaluationresult.errorstatus <br> learningmodelevaluationresult.outputs <br> learningmodelevaluationresult.succeeded

#### [learningmodelfeaturekind](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.learningmodelfeaturekind)

learningmodelfeaturekind

#### [learningmodelsession](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.learningmodelsession)

learningmodelsession <br> learningmodelsession.close <br> learningmodelsession.device <br> learningmodelsession.evaluate <br> learningmodelsession.evaluateasync <br> learningmodelsession.evaluatefeatures <br> learningmodelsession.evaluatefeaturesasync <br> learningmodelsession.evaluationproperties <br> learningmodelsession.learningmodelsession <br> learningmodelsession.learningmodelsession <br> learningmodelsession.model

#### [mapfeaturedescriptor](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.mapfeaturedescriptor)

mapfeaturedescriptor <br> mapfeaturedescriptor.description <br> mapfeaturedescriptor.isrequired <br> mapfeaturedescriptor.keykind <br> mapfeaturedescriptor.kind <br> mapfeaturedescriptor.name <br> mapfeaturedescriptor.valuedescriptor

#### [sequencefeaturedescriptor](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.sequencefeaturedescriptor)

sequencefeaturedescriptor <br> sequencefeaturedescriptor.description <br> sequencefeaturedescriptor.elementdescriptor <br> sequencefeaturedescriptor.isrequired <br> sequencefeaturedescriptor.kind <br> sequencefeaturedescriptor.name

#### [tensorboolean](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.tensorboolean)

tensorboolean <br> tensorboolean.create <br> tensorboolean.create <br> tensorboolean.createfromarray <br> tensorboolean.createfromiterable <br> tensorboolean.getasvectorview <br> tensorboolean.kind <br> tensorboolean.shape <br> tensorboolean.tensorkind

#### [tensordouble](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.tensordouble)

tensordouble <br> tensordouble.create <br> tensordouble.create <br> tensordouble.createfromarray <br> tensordouble.createfromiterable <br> tensordouble.getasvectorview <br> tensordouble.kind <br> tensordouble.shape <br> tensordouble.tensorkind

#### [tensorfeaturedescriptor](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.tensorfeaturedescriptor)

tensorfeaturedescriptor <br> tensorfeaturedescriptor.description <br> tensorfeaturedescriptor.isrequired <br> tensorfeaturedescriptor.kind <br> tensorfeaturedescriptor.name <br> tensorfeaturedescriptor.shape <br> tensorfeaturedescriptor.tensorkind

#### [tensorfloat](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.tensorfloat)

tensorfloat <br> tensorfloat.create <br> tensorfloat.create <br> tensorfloat.createfromarray <br> tensorfloat.createfromiterable <br> tensorfloat.getasvectorview <br> tensorfloat.kind <br> tensorfloat.shape <br> tensorfloat.tensorkind

#### [tensorfloat16bit](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.tensorfloat16bit)

tensorfloat16bit <br> tensorfloat16bit.create <br> tensorfloat16bit.create <br> tensorfloat16bit.createfromarray <br> tensorfloat16bit.createfromiterable <br> tensorfloat16bit.getasvectorview <br> tensorfloat16bit.kind <br> tensorfloat16bit.shape <br> tensorfloat16bit.tensorkind

#### [tensorint16bit](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.tensorint16bit)

tensorint16bit <br> tensorint16bit.create <br> tensorint16bit.create <br> tensorint16bit.createfromarray <br> tensorint16bit.createfromiterable <br> tensorint16bit.getasvectorview <br> tensorint16bit.kind <br> tensorint16bit.shape <br> tensorint16bit.tensorkind

#### [tensorint32bit](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.tensorint32bit)

tensorint32bit <br> tensorint32bit.create <br> tensorint32bit.create <br> tensorint32bit.createfromarray <br> tensorint32bit.createfromiterable <br> tensorint32bit.getasvectorview <br> tensorint32bit.kind <br> tensorint32bit.shape <br> tensorint32bit.tensorkind

#### [tensorint64bit](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.tensorint64bit)

tensorint64bit <br> tensorint64bit.create <br> tensorint64bit.create <br> tensorint64bit.createfromarray <br> tensorint64bit.createfromiterable <br> tensorint64bit.getasvectorview <br> tensorint64bit.kind <br> tensorint64bit.shape <br> tensorint64bit.tensorkind

#### [tensorint8bit](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.tensorint8bit)

tensorint8bit <br> tensorint8bit.create <br> tensorint8bit.create <br> tensorint8bit.createfromarray <br> tensorint8bit.createfromiterable <br> tensorint8bit.getasvectorview <br> tensorint8bit.kind <br> tensorint8bit.shape <br> tensorint8bit.tensorkind

#### [tensorkind](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.tensorkind)

tensorkind

#### [tensorstring](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.tensorstring)

tensorstring <br> tensorstring.create <br> tensorstring.create <br> tensorstring.createfromarray <br> tensorstring.createfromiterable <br> tensorstring.getasvectorview <br> tensorstring.kind <br> tensorstring.shape <br> tensorstring.tensorkind

#### [tensoruint16bit](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.tensoruint16bit)

tensoruint16bit <br> tensoruint16bit.create <br> tensoruint16bit.create <br> tensoruint16bit.createfromarray <br> tensoruint16bit.createfromiterable <br> tensoruint16bit.getasvectorview <br> tensoruint16bit.kind <br> tensoruint16bit.shape <br> tensoruint16bit.tensorkind

#### [tensoruint32bit](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.tensoruint32bit)

tensoruint32bit <br> tensoruint32bit.create <br> tensoruint32bit.create <br> tensoruint32bit.createfromarray <br> tensoruint32bit.createfromiterable <br> tensoruint32bit.getasvectorview <br> tensoruint32bit.kind <br> tensoruint32bit.shape <br> tensoruint32bit.tensorkind

#### [tensoruint64bit](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.tensoruint64bit)

tensoruint64bit <br> tensoruint64bit.create <br> tensoruint64bit.create <br> tensoruint64bit.createfromarray <br> tensoruint64bit.createfromiterable <br> tensoruint64bit.getasvectorview <br> tensoruint64bit.kind <br> tensoruint64bit.shape <br> tensoruint64bit.tensorkind

#### [tensoruint8bit](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.tensoruint8bit)

tensoruint8bit <br> tensoruint8bit.create <br> tensoruint8bit.create <br> tensoruint8bit.createfromarray <br> tensoruint8bit.createfromiterable <br> tensoruint8bit.getasvectorview <br> tensoruint8bit.kind <br> tensoruint8bit.shape <br> tensoruint8bit.tensorkind

#### [windows](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.windows)

windows.ai.machinelearning

## windows.applicationmodel

### [windows.applicationmodel.calls](https://docs.microsoft.com/uwp/api/windows.applicationmodel.calls)

#### [voipcallcoordinator](https://docs.microsoft.com/uwp/api/windows.applicationmodel.calls.voipcallcoordinator)

voipcallcoordinator.reservecallresourcesasync

### [windows.applicationmodel.chat](https://docs.microsoft.com/uwp/api/windows.applicationmodel.chat)

#### [chatcapabilitiesmanager](https://docs.microsoft.com/uwp/api/windows.applicationmodel.chat.chatcapabilitiesmanager)

chatcapabilitiesmanager.getcachedcapabilitiesasync <br> chatcapabilitiesmanager.getcapabilitiesfromnetworkasync

#### [rcsmanager](https://docs.microsoft.com/uwp/api/windows.applicationmodel.chat.rcsmanager)

rcsmanager.transportlistchanged

### [windows.applicationmodel.datatransfer](https://docs.microsoft.com/uwp/api/windows.applicationmodel.datatransfer)

#### [clipboard](https://docs.microsoft.com/uwp/api/windows.applicationmodel.datatransfer.clipboard)

clipboard.clearhistory <br> clipboard.deleteitemfromhistory <br> clipboard.gethistoryitemsasync <br> clipboard.historychanged <br> clipboard.historyenabledchanged <br> clipboard.ishistoryenabled <br> clipboard.isroamingenabled <br> clipboard.roamingenabledchanged <br> clipboard.setcontentwithoptions <br> clipboard.sethistoryitemascontent

#### [clipboardcontentoptions](https://docs.microsoft.com/uwp/api/windows.applicationmodel.datatransfer.clipboardcontentoptions)

clipboardcontentoptions <br> clipboardcontentoptions.clipboardcontentoptions <br> clipboardcontentoptions.historyformats <br> clipboardcontentoptions.isallowedinhistory <br> clipboardcontentoptions.isroamable <br> clipboardcontentoptions.roamingformats

#### [clipboardhistorychangedeventargs](https://docs.microsoft.com/uwp/api/windows.applicationmodel.datatransfer.clipboardhistorychangedeventargs)

clipboardhistorychangedeventargs

#### [clipboardhistoryitem](https://docs.microsoft.com/uwp/api/windows.applicationmodel.datatransfer.clipboardhistoryitem)

clipboardhistoryitem <br> clipboardhistoryitem.content <br> clipboardhistoryitem.id <br> clipboardhistoryitem.timestamp

#### [clipboardhistoryitemsresult](https://docs.microsoft.com/uwp/api/windows.applicationmodel.datatransfer.clipboardhistoryitemsresult)

clipboardhistoryitemsresult <br> clipboardhistoryitemsresult.items <br> clipboardhistoryitemsresult.status

#### [clipboardhistoryitemsresultstatus](https://docs.microsoft.com/uwp/api/windows.applicationmodel.datatransfer.clipboardhistoryitemsresultstatus)

clipboardhistoryitemsresultstatus

#### [datapackagepropertysetview](https://docs.microsoft.com/uwp/api/windows.applicationmodel.datatransfer.datapackagepropertysetview)

datapackagepropertysetview.isfromroamingclipboard

#### [sethistoryitemascontentstatus](https://docs.microsoft.com/uwp/api/windows.applicationmodel.datatransfer.sethistoryitemascontentstatus)

sethistoryitemascontentstatus

### [windows.applicationmodel.store.preview.installcontrol](https://docs.microsoft.com/uwp/api/windows.applicationmodel.store.preview.installcontrol)

#### [appinstallationtoastnotificationmode](https://docs.microsoft.com/uwp/api/windows.applicationmodel.store.preview.installcontrol.appinstallationtoastnotificationmode)

appinstallationtoastnotificationmode

#### [appinstallitem](https://docs.microsoft.com/uwp/api/windows.applicationmodel.store.preview.installcontrol.appinstallitem)

appinstallitem.completedinstalltoastnotificationmode <br> appinstallitem.installinprogresstoastnotificationmode <br> appinstallitem.pintodesktopafterinstall <br> appinstallitem.pintostartafterinstall <br> appinstallitem.pintotaskbarafterinstall

#### [appinstallmanager](https://docs.microsoft.com/uwp/api/windows.applicationmodel.store.preview.installcontrol.appinstallmanager)

appinstallmanager.caninstallforallusers

#### [appinstalloptions](https://docs.microsoft.com/uwp/api/windows.applicationmodel.store.preview.installcontrol.appinstalloptions)

appinstalloptions.campaignid <br> appinstalloptions.completedinstalltoastnotificationmode <br> appinstalloptions.extendedcampaignid <br> appinstalloptions.installforallusers <br> appinstalloptions.installinprogresstoastnotificationmode <br> appinstalloptions.pintodesktopafterinstall <br> appinstalloptions.pintostartafterinstall <br> appinstalloptions.pintotaskbarafterinstall <br> appinstalloptions.stagebutdonotinstall

#### [appupdateoptions](https://docs.microsoft.com/uwp/api/windows.applicationmodel.store.preview.installcontrol.appupdateoptions)

appupdateoptions.automaticallydownloadandinstallupdateiffound

### [windows.applicationmodel.store.preview](https://docs.microsoft.com/uwp/api/windows.applicationmodel.store.preview)

#### [deliveryoptimizationdownloadmode](https://docs.microsoft.com/uwp/api/windows.applicationmodel.store.preview.deliveryoptimizationdownloadmode)

deliveryoptimizationdownloadmode

#### [deliveryoptimizationdownloadmodesource](https://docs.microsoft.com/uwp/api/windows.applicationmodel.store.preview.deliveryoptimizationdownloadmodesource)

deliveryoptimizationdownloadmodesource

#### [deliveryoptimizationsettings](https://docs.microsoft.com/uwp/api/windows.applicationmodel.store.preview.deliveryoptimizationsettings)

deliveryoptimizationsettings <br> deliveryoptimizationsettings.downloadmode <br> deliveryoptimizationsettings.downloadmodesource <br> deliveryoptimizationsettings.getcurrentsettings

#### [storeconfiguration](https://docs.microsoft.com/uwp/api/windows.applicationmodel.store.preview.storeconfiguration)

storeconfiguration.ispintodesktopsupported <br> storeconfiguration.ispintostartsupported <br> storeconfiguration.ispintotaskbarsupported <br> storeconfiguration.pintodesktop <br> storeconfiguration.pintodesktopforuser

### [windows.applicationmodel.useractivities](https://docs.microsoft.com/uwp/api/windows.applicationmodel.useractivities)

#### [useractivity](https://docs.microsoft.com/uwp/api/windows.applicationmodel.useractivities.useractivity)

useractivity.isroamable

### [windows.applicationmodel](https://docs.microsoft.com/uwp/api/windows.applicationmodel)

#### [appinstallerinfo](https://docs.microsoft.com/uwp/api/windows.applicationmodel.appinstallerinfo)

appinstallerinfo <br> appinstallerinfo.uri

#### [limitedaccessfeaturerequestresult](https://docs.microsoft.com/uwp/api/windows.applicationmodel.limitedaccessfeaturerequestresult)

limitedaccessfeaturerequestresult <br> limitedaccessfeaturerequestresult.estimatedremovaldate <br> limitedaccessfeaturerequestresult.featureid <br> limitedaccessfeaturerequestresult.status

#### [limitedaccessfeatures](https://docs.microsoft.com/uwp/api/windows.applicationmodel.limitedaccessfeatures)

limitedaccessfeatures <br> limitedaccessfeatures.tryunlockfeature

#### [limitedaccessfeaturestatus](https://docs.microsoft.com/uwp/api/windows.applicationmodel.limitedaccessfeaturestatus)

limitedaccessfeaturestatus

#### [package](https://docs.microsoft.com/uwp/api/windows.applicationmodel.package)

package.checkupdateavailabilityasync <br> package.getappinstallerinfo

#### [packageupdateavailability](https://docs.microsoft.com/uwp/api/windows.applicationmodel.packageupdateavailability)

packageupdateavailability

#### [packageupdateavailabilityresult](https://docs.microsoft.com/uwp/api/windows.applicationmodel.packageupdateavailabilityresult)

packageupdateavailabilityresult <br> packageupdateavailabilityresult.availability <br> packageupdateavailabilityresult.extendederror

## windows.data

### [windows.data.text](https://docs.microsoft.com/uwp/api/windows.data.text)

#### [textpredictiongenerator](https://docs.microsoft.com/uwp/api/windows.data.text.textpredictiongenerator)

textpredictiongenerator.getcandidatesasync <br> textpredictiongenerator.getnextwordcandidatesasync <br> textpredictiongenerator.inputscope

#### [textpredictionoptions](https://docs.microsoft.com/uwp/api/windows.data.text.textpredictionoptions)

textpredictionoptions

## windows.devices

### [windows.devices.display.core](https://docs.microsoft.com/uwp/api/windows.devices.display.core)

#### [displayadapter](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displayadapter)

displayadapter <br> displayadapter.deviceinterfacepath <br> displayadapter.fromid <br> displayadapter.id <br> displayadapter.pcideviceid <br> displayadapter.pcirevision <br> displayadapter.pcisubsystemid <br> displayadapter.pcivendorid <br> displayadapter.properties <br> displayadapter.sourcecount

#### [displaybitsperchannel](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaybitsperchannel)

displaybitsperchannel

#### [displaydevice](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaydevice)

displaydevice <br> displaydevice.createperiodicfence <br> displaydevice.createprimary <br> displaydevice.createscanoutsource <br> displaydevice.createsimplescanout <br> displaydevice.createtaskpool <br> displaydevice.iscapabilitysupported <br> displaydevice.waitforvblank

#### [displaydevicecapability](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaydevicecapability)

displaydevicecapability

#### [displayfence](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displayfence)

displayfence

#### [displaymanager](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaymanager)

displaymanager <br> displaymanager.changed <br> displaymanager.close <br> displaymanager.create <br> displaymanager.createdisplaydevice <br> displaymanager.disabled <br> displaymanager.enabled <br> displaymanager.getcurrentadapters <br> displaymanager.getcurrenttargets <br> displaymanager.pathsfailedorinvalidated <br> displaymanager.releasetarget <br> displaymanager.start <br> displaymanager.stop <br> displaymanager.tryacquiretarget <br> displaymanager.tryacquiretargetsandcreateemptystate <br> displaymanager.tryacquiretargetsandcreatesubstate <br> displaymanager.tryacquiretargetsandreadcurrentstate <br> displaymanager.tryreadcurrentstateforalltargets

#### [displaymanagerchangedeventargs](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaymanagerchangedeventargs)

displaymanagerchangedeventargs <br> displaymanagerchangedeventargs.getdeferral <br> displaymanagerchangedeventargs.handled

#### [displaymanagerdisabledeventargs](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaymanagerdisabledeventargs)

displaymanagerdisabledeventargs <br> displaymanagerdisabledeventargs.getdeferral <br> displaymanagerdisabledeventargs.handled

#### [displaymanagerenabledeventargs](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaymanagerenabledeventargs)

displaymanagerenabledeventargs <br> displaymanagerenabledeventargs.getdeferral <br> displaymanagerenabledeventargs.handled

#### [displaymanageroptions](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaymanageroptions)

displaymanageroptions

#### [displaymanagerpathsfailedorinvalidatedeventargs](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaymanagerpathsfailedorinvalidatedeventargs)

displaymanagerpathsfailedorinvalidatedeventargs <br> displaymanagerpathsfailedorinvalidatedeventargs.getdeferral <br> displaymanagerpathsfailedorinvalidatedeventargs.handled

#### [displaymanagerresult](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaymanagerresult)

displaymanagerresult

#### [displaymanagerresultwithstate](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaymanagerresultwithstate)

displaymanagerresultwithstate <br> displaymanagerresultwithstate.errorcode <br> displaymanagerresultwithstate.extendederrorcode <br> displaymanagerresultwithstate.state

#### [displaymodeinfo](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaymodeinfo)

displaymodeinfo <br> displaymodeinfo.getwireformatsupportedbitsperchannel <br> displaymodeinfo.isinterlaced <br> displaymodeinfo.isstereo <br> displaymodeinfo.iswireformatsupported <br> displaymodeinfo.presentationrate <br> displaymodeinfo.properties <br> displaymodeinfo.sourcepixelformat <br> displaymodeinfo.sourceresolution <br> displaymodeinfo.targetresolution

#### [displaymodequeryoptions](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaymodequeryoptions)

displaymodequeryoptions

#### [displaypath](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaypath)

displaypath <br> displaypath.applypropertiesfrommode <br> displaypath.findmodes <br> displaypath.isinterlaced <br> displaypath.isstereo <br> displaypath.presentationrate <br> displaypath.properties <br> displaypath.rotation <br> displaypath.scaling <br> displaypath.sourcepixelformat <br> displaypath.sourceresolution <br> displaypath.status <br> displaypath.target <br> displaypath.targetresolution <br> displaypath.view <br> displaypath.wireformat

#### [displaypathscaling](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaypathscaling)

displaypathscaling

#### [displaypathstatus](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaypathstatus)

displaypathstatus

#### [displaypresentationrate](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaypresentationrate)

displaypresentationrate

#### [displayprimarydescription](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displayprimarydescription)

displayprimarydescription <br> displayprimarydescription.colorspace <br> displayprimarydescription.createwithproperties <br> displayprimarydescription.displayprimarydescription <br> displayprimarydescription.format <br> displayprimarydescription.height <br> displayprimarydescription.isstereo <br> displayprimarydescription.multisampledescription <br> displayprimarydescription.properties <br> displayprimarydescription.width

#### [displayrotation](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displayrotation)

displayrotation

#### [displayscanout](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displayscanout)

displayscanout

#### [displaysource](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaysource)

displaysource <br> displaysource.adapterid <br> displaysource.getmetadata <br> displaysource.sourceid

#### [displaystate](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaystate)

displaystate <br> displaystate.canconnecttargettoview <br> displaystate.clone <br> displaystate.connecttarget <br> displaystate.connecttarget <br> displaystate.disconnecttarget <br> displaystate.getpathfortarget <br> displaystate.getviewfortarget <br> displaystate.isreadonly <br> displaystate.isstale <br> displaystate.properties <br> displaystate.targets <br> displaystate.tryapply <br> displaystate.tryfunctionalize <br> displaystate.views

#### [displaystateapplyoptions](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaystateapplyoptions)

displaystateapplyoptions

#### [displaystatefunctionalizeoptions](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaystatefunctionalizeoptions)

displaystatefunctionalizeoptions

#### [displaystateoperationresult](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaystateoperationresult)

displaystateoperationresult <br> displaystateoperationresult.extendederrorcode <br> displaystateoperationresult.status

#### [displaystateoperationstatus](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaystateoperationstatus)

displaystateoperationstatus

#### [displaysurface](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaysurface)

displaysurface

#### [displaytarget](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaytarget)

displaytarget <br> displaytarget.adapter <br> displaytarget.adapterrelativeid <br> displaytarget.deviceinterfacepath <br> displaytarget.isconnected <br> displaytarget.isequal <br> displaytarget.issame <br> displaytarget.isstale <br> displaytarget.isvirtualmodeenabled <br> displaytarget.isvirtualtopologyenabled <br> displaytarget.monitorpersistence <br> displaytarget.properties <br> displaytarget.stablemonitorid <br> displaytarget.trygetmonitor <br> displaytarget.usagekind

#### [displaytargetpersistence](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaytargetpersistence)

displaytargetpersistence

#### [displaytask](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaytask)

displaytask <br> displaytask.setscanout <br> displaytask.setwait

#### [displaytaskpool](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaytaskpool)

displaytaskpool <br> displaytaskpool.createtask <br> displaytaskpool.executetask

#### [displaytasksignalkind](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaytasksignalkind)

displaytasksignalkind

#### [displayview](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displayview)

displayview <br> displayview.contentresolution <br> displayview.paths <br> displayview.properties <br> displayview.setprimarypath

#### [displaywireformat](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaywireformat)

displaywireformat <br> displaywireformat.bitsperchannel <br> displaywireformat.colorspace <br> displaywireformat.createwithproperties <br> displaywireformat.displaywireformat <br> displaywireformat.eotf <br> displaywireformat.hdrmetadata <br> displaywireformat.pixelencoding <br> displaywireformat.properties

#### [displaywireformatcolorspace](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaywireformatcolorspace)

displaywireformatcolorspace

#### [displaywireformateotf](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaywireformateotf)

displaywireformateotf

#### [displaywireformathdrmetadata](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaywireformathdrmetadata)

displaywireformathdrmetadata

#### [displaywireformatpixelencoding](https://docs.microsoft.com/uwp/api/windows.devices.display.core.displaywireformatpixelencoding)

displaywireformatpixelencoding

#### [windows](https://docs.microsoft.com/uwp/api/windows.devices.display.core.windows)

windows.devices.display.core

### [windows.devices.enumeration](https://docs.microsoft.com/uwp/api/windows.devices.enumeration)

#### [deviceinformationpairing](https://docs.microsoft.com/uwp/api/windows.devices.enumeration.deviceinformationpairing)

deviceinformationpairing.tryregisterforallinboundpairingrequestswithprotectionlevel

### [windows.devices.lights.effects](https://docs.microsoft.com/uwp/api/windows.devices.lights.effects)

#### [ilamparrayeffect](https://docs.microsoft.com/uwp/api/windows.devices.lights.effects.ilamparrayeffect)

ilamparrayeffect <br> ilamparrayeffect.zindex

#### [lamparraybitmapeffect](https://docs.microsoft.com/uwp/api/windows.devices.lights.effects.lamparraybitmapeffect)

lamparraybitmapeffect <br> lamparraybitmapeffect.bitmaprequested <br> lamparraybitmapeffect.duration <br> lamparraybitmapeffect.lamparraybitmapeffect <br> lamparraybitmapeffect.startdelay <br> lamparraybitmapeffect.suggestedbitmapsize <br> lamparraybitmapeffect.updateinterval <br> lamparraybitmapeffect.zindex

#### [lamparraybitmaprequestedeventargs](https://docs.microsoft.com/uwp/api/windows.devices.lights.effects.lamparraybitmaprequestedeventargs)

lamparraybitmaprequestedeventargs <br> lamparraybitmaprequestedeventargs.sincestarted <br> lamparraybitmaprequestedeventargs.updatebitmap

#### [lamparrayblinkeffect](https://docs.microsoft.com/uwp/api/windows.devices.lights.effects.lamparrayblinkeffect)

lamparrayblinkeffect <br> lamparrayblinkeffect.attackduration <br> lamparrayblinkeffect.color <br> lamparrayblinkeffect.decayduration <br> lamparrayblinkeffect.lamparrayblinkeffect <br> lamparrayblinkeffect.occurrences <br> lamparrayblinkeffect.repetitiondelay <br> lamparrayblinkeffect.repetitionmode <br> lamparrayblinkeffect.startdelay <br> lamparrayblinkeffect.sustainduration <br> lamparrayblinkeffect.zindex

#### [lamparraycolorrampeffect](https://docs.microsoft.com/uwp/api/windows.devices.lights.effects.lamparraycolorrampeffect)

lamparraycolorrampeffect <br> lamparraycolorrampeffect.color <br> lamparraycolorrampeffect.completionbehavior <br> lamparraycolorrampeffect.lamparraycolorrampeffect <br> lamparraycolorrampeffect.rampduration <br> lamparraycolorrampeffect.startdelay <br> lamparraycolorrampeffect.zindex

#### [lamparraycustomeffect](https://docs.microsoft.com/uwp/api/windows.devices.lights.effects.lamparraycustomeffect)

lamparraycustomeffect <br> lamparraycustomeffect.duration <br> lamparraycustomeffect.lamparraycustomeffect <br> lamparraycustomeffect.updateinterval <br> lamparraycustomeffect.updaterequested <br> lamparraycustomeffect.zindex

#### [lamparrayeffectcompletionbehavior](https://docs.microsoft.com/uwp/api/windows.devices.lights.effects.lamparrayeffectcompletionbehavior)

lamparrayeffectcompletionbehavior

#### [lamparrayeffectplaylist](https://docs.microsoft.com/uwp/api/windows.devices.lights.effects.lamparrayeffectplaylist)

lamparrayeffectplaylist <br> lamparrayeffectplaylist.append <br> lamparrayeffectplaylist.effectstartmode <br> lamparrayeffectplaylist.first <br> lamparrayeffectplaylist.getat <br> lamparrayeffectplaylist.getmany <br> lamparrayeffectplaylist.indexof <br> lamparrayeffectplaylist.lamparrayeffectplaylist <br> lamparrayeffectplaylist.occurrences <br> lamparrayeffectplaylist.overridezindex <br> lamparrayeffectplaylist.pause <br> lamparrayeffectplaylist.pauseall <br> lamparrayeffectplaylist.repetitionmode <br> lamparrayeffectplaylist.size <br> lamparrayeffectplaylist.start <br> lamparrayeffectplaylist.startall <br> lamparrayeffectplaylist.stop <br> lamparrayeffectplaylist.stopall

#### [lamparrayeffectstartmode](https://docs.microsoft.com/uwp/api/windows.devices.lights.effects.lamparrayeffectstartmode)

lamparrayeffectstartmode

#### [lamparrayrepetitionmode](https://docs.microsoft.com/uwp/api/windows.devices.lights.effects.lamparrayrepetitionmode)

lamparrayrepetitionmode

#### [lamparraysolideffect](https://docs.microsoft.com/uwp/api/windows.devices.lights.effects.lamparraysolideffect)

lamparraysolideffect <br> lamparraysolideffect.color <br> lamparraysolideffect.completionbehavior <br> lamparraysolideffect.duration <br> lamparraysolideffect.lamparraysolideffect <br> lamparraysolideffect.startdelay <br> lamparraysolideffect.zindex

#### [lamparrayupdaterequestedeventargs](https://docs.microsoft.com/uwp/api/windows.devices.lights.effects.lamparrayupdaterequestedeventargs)

lamparrayupdaterequestedeventargs <br> lamparrayupdaterequestedeventargs.setcolor <br> lamparrayupdaterequestedeventargs.setcolorforindex <br> lamparrayupdaterequestedeventargs.setcolorsforindices <br> lamparrayupdaterequestedeventargs.setsinglecolorforindices <br> lamparrayupdaterequestedeventargs.sincestarted

#### [windows](https://docs.microsoft.com/uwp/api/windows.devices.lights.effects.windows)

windows.devices.lights.effects

### [windows.devices.lights](https://docs.microsoft.com/uwp/api/windows.devices.lights)

#### [lamparray](https://docs.microsoft.com/uwp/api/windows.devices.lights.lamparray)

lamparray <br> lamparray.boundingbox <br> lamparray.brightnesslevel <br> lamparray.deviceid <br> lamparray.fromidasync <br> lamparray.getdeviceselector <br> lamparray.getindicesforkey <br> lamparray.getindicesforpurposes <br> lamparray.getlampinfo <br> lamparray.hardwareproductid <br> lamparray.hardwarevendorid <br> lamparray.hardwareversion <br> lamparray.isconnected <br> lamparray.isenabled <br> lamparray.lamparraykind <br> lamparray.lampcount <br> lamparray.minupdateinterval <br> lamparray.requestmessageasync <br> lamparray.sendmessageasync <br> lamparray.setcolor <br> lamparray.setcolorforindex <br> lamparray.setcolorsforindices <br> lamparray.setcolorsforkey <br> lamparray.setcolorsforkeys <br> lamparray.setcolorsforpurposes <br> lamparray.setsinglecolorforindices <br> lamparray.supportsvirtualkeys

#### [lamparraykind](https://docs.microsoft.com/uwp/api/windows.devices.lights.lamparraykind)

lamparraykind

#### [lampinfo](https://docs.microsoft.com/uwp/api/windows.devices.lights.lampinfo)

lampinfo <br> lampinfo.bluelevelcount <br> lampinfo.fixedcolor <br> lampinfo.gainlevelcount <br> lampinfo.getnearestsupportedcolor <br> lampinfo.greenlevelcount <br> lampinfo.index <br> lampinfo.position <br> lampinfo.purposes <br> lampinfo.redlevelcount <br> lampinfo.updatelatency

#### [lamppurposes](https://docs.microsoft.com/uwp/api/windows.devices.lights.lamppurposes)

lamppurposes

### [windows.devices.pointofservice.provider](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.provider)

#### [barcodescannerdisablescannerrequest](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.provider.barcodescannerdisablescannerrequest)

barcodescannerdisablescannerrequest.reportfailedasync <br> barcodescannerdisablescannerrequest.reportfailedasync

#### [barcodescannerenablescannerrequest](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.provider.barcodescannerenablescannerrequest)

barcodescannerenablescannerrequest.reportfailedasync <br> barcodescannerenablescannerrequest.reportfailedasync

#### [barcodescannerframereader](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.provider.barcodescannerframereader)

barcodescannerframereader <br> barcodescannerframereader.close <br> barcodescannerframereader.connection <br> barcodescannerframereader.framearrived <br> barcodescannerframereader.startasync <br> barcodescannerframereader.stopasync <br> barcodescannerframereader.tryacquirelatestframeasync

#### [barcodescannerframereaderframearrivedeventargs](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.provider.barcodescannerframereaderframearrivedeventargs)

barcodescannerframereaderframearrivedeventargs <br> barcodescannerframereaderframearrivedeventargs.getdeferral

#### [barcodescannergetsymbologyattributesrequest](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.provider.barcodescannergetsymbologyattributesrequest)

barcodescannergetsymbologyattributesrequest.reportfailedasync <br> barcodescannergetsymbologyattributesrequest.reportfailedasync

#### [barcodescannerhidevideopreviewrequest](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.provider.barcodescannerhidevideopreviewrequest)

barcodescannerhidevideopreviewrequest.reportfailedasync <br> barcodescannerhidevideopreviewrequest.reportfailedasync

#### [barcodescannerproviderconnection](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.provider.barcodescannerproviderconnection)

barcodescannerproviderconnection.createframereaderasync <br> barcodescannerproviderconnection.createframereaderasync <br> barcodescannerproviderconnection.createframereaderasync

#### [barcodescannersetactivesymbologiesrequest](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.provider.barcodescannersetactivesymbologiesrequest)

barcodescannersetactivesymbologiesrequest.reportfailedasync <br> barcodescannersetactivesymbologiesrequest.reportfailedasync

#### [barcodescannersetsymbologyattributesrequest](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.provider.barcodescannersetsymbologyattributesrequest)

barcodescannersetsymbologyattributesrequest.reportfailedasync <br> barcodescannersetsymbologyattributesrequest.reportfailedasync

#### [barcodescannerstartsoftwaretriggerrequest](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.provider.barcodescannerstartsoftwaretriggerrequest)

barcodescannerstartsoftwaretriggerrequest.reportfailedasync <br> barcodescannerstartsoftwaretriggerrequest.reportfailedasync

#### [barcodescannerstopsoftwaretriggerrequest](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.provider.barcodescannerstopsoftwaretriggerrequest)

barcodescannerstopsoftwaretriggerrequest.reportfailedasync <br> barcodescannerstopsoftwaretriggerrequest.reportfailedasync

#### [barcodescannervideoframe](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.provider.barcodescannervideoframe)

barcodescannervideoframe <br> barcodescannervideoframe.close <br> barcodescannervideoframe.format <br> barcodescannervideoframe.height <br> barcodescannervideoframe.pixeldata <br> barcodescannervideoframe.width

### [windows.devices.pointofservice](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice)

#### [barcodescannercapabilities](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.barcodescannercapabilities)

barcodescannercapabilities.isvideopreviewsupported

#### [claimedbarcodescanner](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.claimedbarcodescanner)

claimedbarcodescanner.closed

#### [claimedbarcodescannerclosedeventargs](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.claimedbarcodescannerclosedeventargs)

claimedbarcodescannerclosedeventargs

#### [claimedcashdrawer](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.claimedcashdrawer)

claimedcashdrawer.closed

#### [claimedcashdrawerclosedeventargs](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.claimedcashdrawerclosedeventargs)

claimedcashdrawerclosedeventargs

#### [claimedlinedisplay](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.claimedlinedisplay)

claimedlinedisplay.closed

#### [claimedlinedisplayclosedeventargs](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.claimedlinedisplayclosedeventargs)

claimedlinedisplayclosedeventargs

#### [claimedmagneticstripereader](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.claimedmagneticstripereader)

claimedmagneticstripereader.closed

#### [claimedmagneticstripereaderclosedeventargs](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.claimedmagneticstripereaderclosedeventargs)

claimedmagneticstripereaderclosedeventargs

#### [claimedposprinter](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.claimedposprinter)

claimedposprinter.closed

#### [claimedposprinterclosedeventargs](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.claimedposprinterclosedeventargs)

claimedposprinterclosedeventargs

### [windows.devices.sensors](https://docs.microsoft.com/uwp/api/windows.devices.sensors)

#### [hingeanglereading](https://docs.microsoft.com/uwp/api/windows.devices.sensors.hingeanglereading)

hingeanglereading <br> hingeanglereading.angleindegrees <br> hingeanglereading.properties <br> hingeanglereading.timestamp

#### [hingeanglesensor](https://docs.microsoft.com/uwp/api/windows.devices.sensors.hingeanglesensor)

hingeanglesensor <br> hingeanglesensor.deviceid <br> hingeanglesensor.fromidasync <br> hingeanglesensor.getcurrentreadingasync <br> hingeanglesensor.getdefaultasync <br> hingeanglesensor.getdeviceselector <br> hingeanglesensor.getrelatedtoadjacentpanelsasync <br> hingeanglesensor.minreportthresholdindegrees <br> hingeanglesensor.readingchanged <br> hingeanglesensor.reportthresholdindegrees

#### [hingeanglesensorreadingchangedeventargs](https://docs.microsoft.com/uwp/api/windows.devices.sensors.hingeanglesensorreadingchangedeventargs)

hingeanglesensorreadingchangedeventargs <br> hingeanglesensorreadingchangedeventargs.reading

#### [simpleorientationsensor](https://docs.microsoft.com/uwp/api/windows.devices.sensors.simpleorientationsensor)

simpleorientationsensor.fromidasync <br> simpleorientationsensor.getdeviceselector

### [windows.devices.smartcards](https://docs.microsoft.com/uwp/api/windows.devices.smartcards)

#### [knownsmartcardappletids](https://docs.microsoft.com/uwp/api/windows.devices.smartcards.knownsmartcardappletids)

knownsmartcardappletids <br> knownsmartcardappletids.paymentsystemenvironment <br> knownsmartcardappletids.proximitypaymentsystemenvironment

#### [smartcardappletidgroup](https://docs.microsoft.com/uwp/api/windows.devices.smartcards.smartcardappletidgroup)

smartcardappletidgroup.description <br> smartcardappletidgroup.logo <br> smartcardappletidgroup.properties <br> smartcardappletidgroup.secureuserauthenticationrequired

#### [smartcardappletidgroupregistration](https://docs.microsoft.com/uwp/api/windows.devices.smartcards.smartcardappletidgroupregistration)

smartcardappletidgroupregistration.setpropertiesasync <br> smartcardappletidgroupregistration.smartcardreaderid

## windows.foundation

### [windows.foundation](https://docs.microsoft.com/uwp/api/windows.foundation)

#### [guidhelper](https://docs.microsoft.com/uwp/api/windows.foundation.guidhelper)

guidhelper <br> guidhelper.createnewguid <br> guidhelper.empty <br> guidhelper.equals

## windows.globalization

### [windows.globalization](https://docs.microsoft.com/uwp/api/windows.globalization)

#### [currencyidentifiers](https://docs.microsoft.com/uwp/api/windows.globalization.currencyidentifiers)

currencyidentifiers.mru <br> currencyidentifiers.ssp <br> currencyidentifiers.stn <br> currencyidentifiers.ves

## windows.graphics

### [windows.graphics.capture](https://docs.microsoft.com/uwp/api/windows.graphics.capture)

#### [direct3d11captureframepool](https://docs.microsoft.com/uwp/api/windows.graphics.capture.direct3d11captureframepool)

direct3d11captureframepool.createfreethreaded

#### [graphicscaptureitem](https://docs.microsoft.com/uwp/api/windows.graphics.capture.graphicscaptureitem)

graphicscaptureitem.createfromvisual

### [windows.graphics.display.core](https://docs.microsoft.com/uwp/api/windows.graphics.display.core)

#### [hdmidisplaymode](https://docs.microsoft.com/uwp/api/windows.graphics.display.core.hdmidisplaymode)

hdmidisplaymode.isdolbyvisionlowlatencysupported

### [windows.graphics.holographic](https://docs.microsoft.com/uwp/api/windows.graphics.holographic)

#### [holographiccamera](https://docs.microsoft.com/uwp/api/windows.graphics.holographic.holographiccamera)

holographiccamera.ishardwarecontentprotectionenabled <br> holographiccamera.ishardwarecontentprotectionsupported

#### [holographicquadlayerupdateparameters](https://docs.microsoft.com/uwp/api/windows.graphics.holographic.holographicquadlayerupdateparameters)

holographicquadlayerupdateparameters.acquirebuffertoupdatecontentwithhardwareprotection <br> holographicquadlayerupdateparameters.canacquirewithhardwareprotection

### [windows.graphics.imaging](https://docs.microsoft.com/uwp/api/windows.graphics.imaging)

#### [bitmapdecoder](https://docs.microsoft.com/uwp/api/windows.graphics.imaging.bitmapdecoder)

bitmapdecoder.heifdecoderid <br> bitmapdecoder.webpdecoderid

#### [bitmapencoder](https://docs.microsoft.com/uwp/api/windows.graphics.imaging.bitmapencoder)

bitmapencoder.heifencoderid

## windows.management

### [windows.management.deployment](https://docs.microsoft.com/uwp/api/windows.management.deployment)

#### [packagemanager](https://docs.microsoft.com/uwp/api/windows.management.deployment.packagemanager)

packagemanager.deprovisionpackageforallusersasync

## windows.media

### [windows.media.audio](https://docs.microsoft.com/uwp/api/windows.media.audio)

#### [createaudiodeviceinputnoderesult](https://docs.microsoft.com/uwp/api/windows.media.audio.createaudiodeviceinputnoderesult)

createaudiodeviceinputnoderesult.extendederror

#### [createaudiodeviceoutputnoderesult](https://docs.microsoft.com/uwp/api/windows.media.audio.createaudiodeviceoutputnoderesult)

createaudiodeviceoutputnoderesult.extendederror

#### [createaudiofileinputnoderesult](https://docs.microsoft.com/uwp/api/windows.media.audio.createaudiofileinputnoderesult)

createaudiofileinputnoderesult.extendederror

#### [createaudiofileoutputnoderesult](https://docs.microsoft.com/uwp/api/windows.media.audio.createaudiofileoutputnoderesult)

createaudiofileoutputnoderesult.extendederror

#### [createaudiographresult](https://docs.microsoft.com/uwp/api/windows.media.audio.createaudiographresult)

createaudiographresult.extendederror

#### [createmediasourceaudioinputnoderesult](https://docs.microsoft.com/uwp/api/windows.media.audio.createmediasourceaudioinputnoderesult)

createmediasourceaudioinputnoderesult.extendederror

#### [mixedrealityspatialaudioformatpolicy](https://docs.microsoft.com/uwp/api/windows.media.audio.mixedrealityspatialaudioformatpolicy)

mixedrealityspatialaudioformatpolicy

#### [setdefaultspatialaudioformatresult](https://docs.microsoft.com/uwp/api/windows.media.audio.setdefaultspatialaudioformatresult)

setdefaultspatialaudioformatresult <br> setdefaultspatialaudioformatresult.status

#### [setdefaultspatialaudioformatstatus](https://docs.microsoft.com/uwp/api/windows.media.audio.setdefaultspatialaudioformatstatus)

setdefaultspatialaudioformatstatus

#### [spatialaudiodeviceconfiguration](https://docs.microsoft.com/uwp/api/windows.media.audio.spatialaudiodeviceconfiguration)

spatialaudiodeviceconfiguration <br> spatialaudiodeviceconfiguration.activespatialaudioformat <br> spatialaudiodeviceconfiguration.configurationchanged <br> spatialaudiodeviceconfiguration.defaultspatialaudioformat <br> spatialaudiodeviceconfiguration.deviceid <br> spatialaudiodeviceconfiguration.getfordeviceid <br> spatialaudiodeviceconfiguration.isspatialaudioformatsupported <br> spatialaudiodeviceconfiguration.isspatialaudiosupported <br> spatialaudiodeviceconfiguration.setdefaultspatialaudioformatasync

#### [spatialaudioformatconfiguration](https://docs.microsoft.com/uwp/api/windows.media.audio.spatialaudioformatconfiguration)

spatialaudioformatconfiguration <br> spatialaudioformatconfiguration.getdefault <br> spatialaudioformatconfiguration.mixedrealityexclusivemodepolicy <br> spatialaudioformatconfiguration.reportconfigurationchangedasync <br> spatialaudioformatconfiguration.reportlicensechangedasync

#### [spatialaudioformatsubtype](https://docs.microsoft.com/uwp/api/windows.media.audio.spatialaudioformatsubtype)

spatialaudioformatsubtype <br> spatialaudioformatsubtype.dolbyatmosforheadphones <br> spatialaudioformatsubtype.dolbyatmosforhometheater <br> spatialaudioformatsubtype.dolbyatmosforspeakers <br> spatialaudioformatsubtype.dtsheadphonex <br> spatialaudioformatsubtype.dtsxultra <br> spatialaudioformatsubtype.windowssonic

### [windows.media.control](https://docs.microsoft.com/uwp/api/windows.media.control)

#### [currentsessionchangedeventargs](https://docs.microsoft.com/uwp/api/windows.media.control.currentsessionchangedeventargs)

currentsessionchangedeventargs

#### [globalsystemmediatransportcontrolssession](https://docs.microsoft.com/uwp/api/windows.media.control.globalsystemmediatransportcontrolssession)

globalsystemmediatransportcontrolssession <br> globalsystemmediatransportcontrolssession.getplaybackinfo <br> globalsystemmediatransportcontrolssession.gettimelineproperties <br> globalsystemmediatransportcontrolssession.mediapropertieschanged <br> globalsystemmediatransportcontrolssession.playbackinfochanged <br> globalsystemmediatransportcontrolssession.sourceappusermodelid <br> globalsystemmediatransportcontrolssession.timelinepropertieschanged <br> globalsystemmediatransportcontrolssession.trychangeautorepeatmodeasync <br> globalsystemmediatransportcontrolssession.trychangechanneldownasync <br> globalsystemmediatransportcontrolssession.trychangechannelupasync <br> globalsystemmediatransportcontrolssession.trychangeplaybackpositionasync <br> globalsystemmediatransportcontrolssession.trychangeplaybackrateasync <br> globalsystemmediatransportcontrolssession.trychangeshuffleactiveasync <br> globalsystemmediatransportcontrolssession.tryfastforwardasync <br> globalsystemmediatransportcontrolssession.trygetmediapropertiesasync <br> globalsystemmediatransportcontrolssession.trypauseasync <br> globalsystemmediatransportcontrolssession.tryplayasync <br> globalsystemmediatransportcontrolssession.tryrecordasync <br> globalsystemmediatransportcontrolssession.tryrewindasync <br> globalsystemmediatransportcontrolssession.tryskipnextasync <br> globalsystemmediatransportcontrolssession.tryskippreviousasync <br> globalsystemmediatransportcontrolssession.trystopasync <br> globalsystemmediatransportcontrolssession.trytoggleplaypauseasync

#### [globalsystemmediatransportcontrolssessionmanager](https://docs.microsoft.com/uwp/api/windows.media.control.globalsystemmediatransportcontrolssessionmanager)

globalsystemmediatransportcontrolssessionmanager <br> globalsystemmediatransportcontrolssessionmanager.currentsessionchanged <br> globalsystemmediatransportcontrolssessionmanager.getcurrentsession <br> globalsystemmediatransportcontrolssessionmanager.getsessions <br> globalsystemmediatransportcontrolssessionmanager.requestasync <br> globalsystemmediatransportcontrolssessionmanager.sessionschanged

#### [globalsystemmediatransportcontrolssessionmediaproperties](https://docs.microsoft.com/uwp/api/windows.media.control.globalsystemmediatransportcontrolssessionmediaproperties)

globalsystemmediatransportcontrolssessionmediaproperties <br> globalsystemmediatransportcontrolssessionmediaproperties.albumartist <br> globalsystemmediatransportcontrolssessionmediaproperties.albumtitle <br> globalsystemmediatransportcontrolssessionmediaproperties.albumtrackcount <br> globalsystemmediatransportcontrolssessionmediaproperties.artist <br> globalsystemmediatransportcontrolssessionmediaproperties.genres <br> globalsystemmediatransportcontrolssessionmediaproperties.playbacktype <br> globalsystemmediatransportcontrolssessionmediaproperties.subtitle <br> globalsystemmediatransportcontrolssessionmediaproperties.thumbnail <br> globalsystemmediatransportcontrolssessionmediaproperties.title <br> globalsystemmediatransportcontrolssessionmediaproperties.tracknumber

#### [globalsystemmediatransportcontrolssessionplaybackcontrols](https://docs.microsoft.com/uwp/api/windows.media.control.globalsystemmediatransportcontrolssessionplaybackcontrols)

globalsystemmediatransportcontrolssessionplaybackcontrols <br> globalsystemmediatransportcontrolssessionplaybackcontrols.ischanneldownenabled <br> globalsystemmediatransportcontrolssessionplaybackcontrols.ischannelupenabled <br> globalsystemmediatransportcontrolssessionplaybackcontrols.isfastforwardenabled <br> globalsystemmediatransportcontrolssessionplaybackcontrols.isnextenabled <br> globalsystemmediatransportcontrolssessionplaybackcontrols.ispauseenabled <br> globalsystemmediatransportcontrolssessionplaybackcontrols.isplaybackpositionenabled <br> globalsystemmediatransportcontrolssessionplaybackcontrols.isplaybackrateenabled <br> globalsystemmediatransportcontrolssessionplaybackcontrols.isplayenabled <br> globalsystemmediatransportcontrolssessionplaybackcontrols.isplaypausetoggleenabled <br> globalsystemmediatransportcontrolssessionplaybackcontrols.ispreviousenabled <br> globalsystemmediatransportcontrolssessionplaybackcontrols.isrecordenabled <br> globalsystemmediatransportcontrolssessionplaybackcontrols.isrepeatenabled <br> globalsystemmediatransportcontrolssessionplaybackcontrols.isrewindenabled <br> globalsystemmediatransportcontrolssessionplaybackcontrols.isshuffleenabled <br> globalsystemmediatransportcontrolssessionplaybackcontrols.isstopenabled

#### [globalsystemmediatransportcontrolssessionplaybackinfo](https://docs.microsoft.com/uwp/api/windows.media.control.globalsystemmediatransportcontrolssessionplaybackinfo)

globalsystemmediatransportcontrolssessionplaybackinfo <br> globalsystemmediatransportcontrolssessionplaybackinfo.autorepeatmode <br> globalsystemmediatransportcontrolssessionplaybackinfo.controls <br> globalsystemmediatransportcontrolssessionplaybackinfo.isshuffleactive <br> globalsystemmediatransportcontrolssessionplaybackinfo.playbackrate <br> globalsystemmediatransportcontrolssessionplaybackinfo.playbackstatus <br> globalsystemmediatransportcontrolssessionplaybackinfo.playbacktype

#### [globalsystemmediatransportcontrolssessionplaybackstatus](https://docs.microsoft.com/uwp/api/windows.media.control.globalsystemmediatransportcontrolssessionplaybackstatus)

globalsystemmediatransportcontrolssessionplaybackstatus

#### [globalsystemmediatransportcontrolssessiontimelineproperties](https://docs.microsoft.com/uwp/api/windows.media.control.globalsystemmediatransportcontrolssessiontimelineproperties)

globalsystemmediatransportcontrolssessiontimelineproperties <br> globalsystemmediatransportcontrolssessiontimelineproperties.endtime <br> globalsystemmediatransportcontrolssessiontimelineproperties.lastupdatedtime <br> globalsystemmediatransportcontrolssessiontimelineproperties.maxseektime <br> globalsystemmediatransportcontrolssessiontimelineproperties.minseektime <br> globalsystemmediatransportcontrolssessiontimelineproperties.position <br> globalsystemmediatransportcontrolssessiontimelineproperties.starttime

#### [mediapropertieschangedeventargs](https://docs.microsoft.com/uwp/api/windows.media.control.mediapropertieschangedeventargs)

mediapropertieschangedeventargs

#### [playbackinfochangedeventargs](https://docs.microsoft.com/uwp/api/windows.media.control.playbackinfochangedeventargs)

playbackinfochangedeventargs

#### [sessionschangedeventargs](https://docs.microsoft.com/uwp/api/windows.media.control.sessionschangedeventargs)

sessionschangedeventargs

#### [timelinepropertieschangedeventargs](https://docs.microsoft.com/uwp/api/windows.media.control.timelinepropertieschangedeventargs)

timelinepropertieschangedeventargs

#### [windows](https://docs.microsoft.com/uwp/api/windows.media.control.windows)

windows.media.control

### [windows.media.core](https://docs.microsoft.com/uwp/api/windows.media.core)

#### [mediastreamsample](https://docs.microsoft.com/uwp/api/windows.media.core.mediastreamsample)

mediastreamsample.createfromdirect3d11surface <br> mediastreamsample.direct3d11surface

### [windows.media.devices.core](https://docs.microsoft.com/uwp/api/windows.media.devices.core)

#### [cameraintrinsics](https://docs.microsoft.com/uwp/api/windows.media.devices.core.cameraintrinsics)

cameraintrinsics.cameraintrinsics

### [windows.media.import](https://docs.microsoft.com/uwp/api/windows.media.import)

#### [photoimportitem](https://docs.microsoft.com/uwp/api/windows.media.import.photoimportitem)

photoimportitem.path

### [windows.media.mediaproperties](https://docs.microsoft.com/uwp/api/windows.media.mediaproperties)

#### [imageencodingproperties](https://docs.microsoft.com/uwp/api/windows.media.mediaproperties.imageencodingproperties)

imageencodingproperties.createheif

#### [mediaencodingsubtypes](https://docs.microsoft.com/uwp/api/windows.media.mediaproperties.mediaencodingsubtypes)

mediaencodingsubtypes.heif

### [windows.media.protection.playready](https://docs.microsoft.com/uwp/api/windows.media.protection.playready)

#### [playreadystatics](https://docs.microsoft.com/uwp/api/windows.media.protection.playready.playreadystatics)

playreadystatics.hardwaredrmdisabledattime <br> playreadystatics.hardwaredrmdisableduntiltime <br> playreadystatics.resethardwaredrmdisabled

## windows.networking

### [windows.networking.connectivity](https://docs.microsoft.com/uwp/api/windows.networking.connectivity)

#### [connectionprofile](https://docs.microsoft.com/uwp/api/windows.networking.connectivity.connectionprofile)

connectionprofile.candelete <br> connectionprofile.trydeleteasync

#### [connectionprofiledeletestatus](https://docs.microsoft.com/uwp/api/windows.networking.connectivity.connectionprofiledeletestatus)

connectionprofiledeletestatus

## windows.perception

### [windows.perception.spatial.preview](https://docs.microsoft.com/uwp/api/windows.perception.spatial.preview)

#### [spatialgraphinteroppreview](https://docs.microsoft.com/uwp/api/windows.perception.spatial.preview.spatialgraphinteroppreview)

spatialgraphinteroppreview <br> spatialgraphinteroppreview.createcoordinatesystemfornode <br> spatialgraphinteroppreview.createcoordinatesystemfornode <br> spatialgraphinteroppreview.createcoordinatesystemfornode <br> spatialgraphinteroppreview.createlocatorfornode

#### [windows](https://docs.microsoft.com/uwp/api/windows.perception.spatial.preview.windows)

windows.perception.spatial.preview

### [windows.perception.spatial](https://docs.microsoft.com/uwp/api/windows.perception.spatial)

#### [spatialanchorexporter](https://docs.microsoft.com/uwp/api/windows.perception.spatial.spatialanchorexporter)

spatialanchorexporter <br> spatialanchorexporter.getanchorexportsufficiencyasync <br> spatialanchorexporter.getdefault <br> spatialanchorexporter.requestaccessasync <br> spatialanchorexporter.tryexportanchorasync

#### [spatialanchorexportpurpose](https://docs.microsoft.com/uwp/api/windows.perception.spatial.spatialanchorexportpurpose)

spatialanchorexportpurpose

#### [spatialanchorexportsufficiency](https://docs.microsoft.com/uwp/api/windows.perception.spatial.spatialanchorexportsufficiency)

spatialanchorexportsufficiency <br> spatialanchorexportsufficiency.isminimallysufficient <br> spatialanchorexportsufficiency.recommendedsufficiencylevel <br> spatialanchorexportsufficiency.sufficiencylevel

#### [spatiallocation](https://docs.microsoft.com/uwp/api/windows.perception.spatial.spatiallocation)

spatiallocation.absoluteangularaccelerationaxisangle <br> spatiallocation.absoluteangularvelocityaxisangle

### [windows.perception](https://docs.microsoft.com/uwp/api/windows.perception)

#### [perceptiontimestamp](https://docs.microsoft.com/uwp/api/windows.perception.perceptiontimestamp)

perceptiontimestamp.systemrelativetargettime

#### [perceptiontimestamphelper](https://docs.microsoft.com/uwp/api/windows.perception.perceptiontimestamphelper)

perceptiontimestamphelper.fromsystemrelativetargettime

## windows.services

### [windows.services.cortana](https://docs.microsoft.com/uwp/api/windows.services.cortana)

#### [cortanaactionableinsights](https://docs.microsoft.com/uwp/api/windows.services.cortana.cortanaactionableinsights)

cortanaactionableinsights <br> cortanaactionableinsights.getdefault <br> cortanaactionableinsights.getforuser <br> cortanaactionableinsights.isavailableasync <br> cortanaactionableinsights.showinsightsasync <br> cortanaactionableinsights.showinsightsasync <br> cortanaactionableinsights.showinsightsforimageasync <br> cortanaactionableinsights.showinsightsforimageasync <br> cortanaactionableinsights.showinsightsfortextasync <br> cortanaactionableinsights.showinsightsfortextasync <br> cortanaactionableinsights.user

#### [cortanaactionableinsightsoptions](https://docs.microsoft.com/uwp/api/windows.services.cortana.cortanaactionableinsightsoptions)

cortanaactionableinsightsoptions <br> cortanaactionableinsightsoptions.contentsourceweblink <br> cortanaactionableinsightsoptions.cortanaactionableinsightsoptions <br> cortanaactionableinsightsoptions.surroundingtext

### [windows.services.store](https://docs.microsoft.com/uwp/api/windows.services.store)

#### [storeapplicense](https://docs.microsoft.com/uwp/api/windows.services.store.storeapplicense)

storeapplicense.isdisclicense

#### [storecontext](https://docs.microsoft.com/uwp/api/windows.services.store.storecontext)

storecontext.requestrateandreviewappasync <br> storecontext.setinstallorderforassociatedstorequeueitemsasync

#### [storequeueitem](https://docs.microsoft.com/uwp/api/windows.services.store.storequeueitem)

storequeueitem.cancelinstallasync <br> storequeueitem.pauseinstallasync <br> storequeueitem.resumeinstallasync

#### [storerateandreviewresult](https://docs.microsoft.com/uwp/api/windows.services.store.storerateandreviewresult)

storerateandreviewresult <br> storerateandreviewresult.extendederror <br> storerateandreviewresult.extendedjsondata <br> storerateandreviewresult.status <br> storerateandreviewresult.wasupdated

#### [storerateandreviewstatus](https://docs.microsoft.com/uwp/api/windows.services.store.storerateandreviewstatus)

storerateandreviewstatus

## windows.storage

### [windows.storage.provider](https://docs.microsoft.com/uwp/api/windows.storage.provider)

#### [storageprovidersyncrootinfo](https://docs.microsoft.com/uwp/api/windows.storage.provider.storageprovidersyncrootinfo)

storageprovidersyncrootinfo.providerid

## windows.system

### [windows.system.implementation.holographic](https://docs.microsoft.com/uwp/api/windows.system.implementation.holographic)

#### [sysholographicdeploymentprogress](https://docs.microsoft.com/uwp/api/windows.system.implementation.holographic.sysholographicdeploymentprogress)

sysholographicdeploymentprogress

#### [sysholographicdeploymentresult](https://docs.microsoft.com/uwp/api/windows.system.implementation.holographic.sysholographicdeploymentresult)

sysholographicdeploymentresult <br> sysholographicdeploymentresult.deploymentstate <br> sysholographicdeploymentresult.extendederror

#### [sysholographicdeploymentstate](https://docs.microsoft.com/uwp/api/windows.system.implementation.holographic.sysholographicdeploymentstate)

sysholographicdeploymentstate

#### [sysholographicdisplay](https://docs.microsoft.com/uwp/api/windows.system.implementation.holographic.sysholographicdisplay)

sysholographicdisplay <br> sysholographicdisplay.deviceid <br> sysholographicdisplay.display <br> sysholographicdisplay.experiencemode <br> sysholographicdisplay.leftviewportparameters <br> sysholographicdisplay.outputadapterid <br> sysholographicdisplay.rightviewportparameters

#### [sysholographicdisplayexperiencemode](https://docs.microsoft.com/uwp/api/windows.system.implementation.holographic.sysholographicdisplayexperiencemode)

sysholographicdisplayexperiencemode

#### [sysholographicdisplaywatcher](https://docs.microsoft.com/uwp/api/windows.system.implementation.holographic.sysholographicdisplaywatcher)

sysholographicdisplaywatcher <br> sysholographicdisplaywatcher.added <br> sysholographicdisplaywatcher.enumerationcompleted <br> sysholographicdisplaywatcher.removed <br> sysholographicdisplaywatcher.start <br> sysholographicdisplaywatcher.status <br> sysholographicdisplaywatcher.stop <br> sysholographicdisplaywatcher.stopped <br> sysholographicdisplaywatcher.sysholographicdisplaywatcher

#### [sysholographicdisplaywatcherstatus](https://docs.microsoft.com/uwp/api/windows.system.implementation.holographic.sysholographicdisplaywatcherstatus)

sysholographicdisplaywatcherstatus

#### [sysholographicpreviewmediasource](https://docs.microsoft.com/uwp/api/windows.system.implementation.holographic.sysholographicpreviewmediasource)

sysholographicpreviewmediasource <br> sysholographicpreviewmediasource.create

#### [sysholographicwindowingenvironment](https://docs.microsoft.com/uwp/api/windows.system.implementation.holographic.sysholographicwindowingenvironment)

sysholographicwindowingenvironment <br> sysholographicwindowingenvironment.deployasync <br> sysholographicwindowingenvironment.getdefault <br> sysholographicwindowingenvironment.getdeploymentstateasync <br> sysholographicwindowingenvironment.isdevicesetupcomplete <br> sysholographicwindowingenvironment.islearningexperiencecomplete <br> sysholographicwindowingenvironment.ispreviewactive <br> sysholographicwindowingenvironment.ispreviewactivechanged <br> sysholographicwindowingenvironment.isprotectedcontentpresent <br> sysholographicwindowingenvironment.isprotectedcontentpresentchanged <br> sysholographicwindowingenvironment.isspeechpersonalizationsupported <br> sysholographicwindowingenvironment.setisspeechpersonalizationenabledasync <br> sysholographicwindowingenvironment.startasync <br> sysholographicwindowingenvironment.status <br> sysholographicwindowingenvironment.statuschanged <br> sysholographicwindowingenvironment.stopasync

#### [sysholographicwindowingenvironmentcomponentkind](https://docs.microsoft.com/uwp/api/windows.system.implementation.holographic.sysholographicwindowingenvironmentcomponentkind)

sysholographicwindowingenvironmentcomponentkind

#### [sysholographicwindowingenvironmentcomponentstate](https://docs.microsoft.com/uwp/api/windows.system.implementation.holographic.sysholographicwindowingenvironmentcomponentstate)

sysholographicwindowingenvironmentcomponentstate

#### [sysholographicwindowingenvironmentcomponentstatus](https://docs.microsoft.com/uwp/api/windows.system.implementation.holographic.sysholographicwindowingenvironmentcomponentstatus)

sysholographicwindowingenvironmentcomponentstatus

#### [sysholographicwindowingenvironmentstate](https://docs.microsoft.com/uwp/api/windows.system.implementation.holographic.sysholographicwindowingenvironmentstate)

sysholographicwindowingenvironmentstate

#### [sysholographicwindowingenvironmentstatus](https://docs.microsoft.com/uwp/api/windows.system.implementation.holographic.sysholographicwindowingenvironmentstatus)

sysholographicwindowingenvironmentstatus <br> sysholographicwindowingenvironmentstatus.componentstatuses <br> sysholographicwindowingenvironmentstatus.state

#### [sysspatialinputdevice](https://docs.microsoft.com/uwp/api/windows.system.implementation.holographic.sysspatialinputdevice)

sysspatialinputdevice <br> sysspatialinputdevice.handedness <br> sysspatialinputdevice.haspositionaltracking <br> sysspatialinputdevice.trygetbatteryreport

#### [sysspatialinputdevicewatcher](https://docs.microsoft.com/uwp/api/windows.system.implementation.holographic.sysspatialinputdevicewatcher)

sysspatialinputdevicewatcher <br> sysspatialinputdevicewatcher.added <br> sysspatialinputdevicewatcher.enumerationcompleted <br> sysspatialinputdevicewatcher.removed <br> sysspatialinputdevicewatcher.start <br> sysspatialinputdevicewatcher.status <br> sysspatialinputdevicewatcher.stop <br> sysspatialinputdevicewatcher.stopped <br> sysspatialinputdevicewatcher.sysspatialinputdevicewatcher <br> sysspatialinputdevicewatcher.updated

#### [sysspatialinputdevicewatcherstatus](https://docs.microsoft.com/uwp/api/windows.system.implementation.holographic.sysspatialinputdevicewatcherstatus)

sysspatialinputdevicewatcherstatus

#### [sysspatiallocator](https://docs.microsoft.com/uwp/api/windows.system.implementation.holographic.sysspatiallocator)

sysspatiallocator <br> sysspatiallocator.getfloorlocator

#### [sysspatialstageboundarydisposition](https://docs.microsoft.com/uwp/api/windows.system.implementation.holographic.sysspatialstageboundarydisposition)

sysspatialstageboundarydisposition

#### [sysspatialstagemanager](https://docs.microsoft.com/uwp/api/windows.system.implementation.holographic.sysspatialstagemanager)

sysspatialstagemanager <br> sysspatialstagemanager.doesanystagehaveboundariesasync <br> sysspatialstagemanager.getboundarydisposition <br> sysspatialstagemanager.setandsavenewstageasync <br> sysspatialstagemanager.setboundaryenabled <br> sysspatialstagemanager.sysspatialstagemanager <br> sysspatialstagemanager.updatestageanchorasync

#### [windows](https://docs.microsoft.com/uwp/api/windows.system.implementation.holographic.windows)

windows.system.implementation.holographic

### [windows.system.preview](https://docs.microsoft.com/uwp/api/windows.system.preview)

#### [hingestate](https://docs.microsoft.com/uwp/api/windows.system.preview.hingestate)

hingestate

#### [twopanelhingeddeviceposturepreview](https://docs.microsoft.com/uwp/api/windows.system.preview.twopanelhingeddeviceposturepreview)

twopanelhingeddeviceposturepreview <br> twopanelhingeddeviceposturepreview.getcurrentpostureasync <br> twopanelhingeddeviceposturepreview.getdefaultasync <br> twopanelhingeddeviceposturepreview.posturechanged

#### [twopanelhingeddeviceposturepreviewreading](https://docs.microsoft.com/uwp/api/windows.system.preview.twopanelhingeddeviceposturepreviewreading)

twopanelhingeddeviceposturepreviewreading <br> twopanelhingeddeviceposturepreviewreading.hingestate <br> twopanelhingeddeviceposturepreviewreading.panel1id <br> twopanelhingeddeviceposturepreviewreading.panel1orientation <br> twopanelhingeddeviceposturepreviewreading.panel2id <br> twopanelhingeddeviceposturepreviewreading.panel2orientation <br> twopanelhingeddeviceposturepreviewreading.timestamp

#### [twopanelhingeddeviceposturepreviewreadingchangedeventargs](https://docs.microsoft.com/uwp/api/windows.system.preview.twopanelhingeddeviceposturepreviewreadingchangedeventargs)

twopanelhingeddeviceposturepreviewreadingchangedeventargs <br> twopanelhingeddeviceposturepreviewreadingchangedeventargs.reading

#### [windows](https://docs.microsoft.com/uwp/api/windows.system.preview.windows)

windows.system.preview

### [windows.system.profile.systemmanufacturers](https://docs.microsoft.com/uwp/api/windows.system.profile.systemmanufacturers)

#### [systemsupportdeviceinfo](https://docs.microsoft.com/uwp/api/windows.system.profile.systemmanufacturers.systemsupportdeviceinfo)

systemsupportdeviceinfo <br> systemsupportdeviceinfo.friendlyname <br> systemsupportdeviceinfo.operatingsystem <br> systemsupportdeviceinfo.systemfirmwareversion <br> systemsupportdeviceinfo.systemhardwareversion <br> systemsupportdeviceinfo.systemmanufacturer <br> systemsupportdeviceinfo.systemproductname <br> systemsupportdeviceinfo.systemsku

#### [systemsupportinfo](https://docs.microsoft.com/uwp/api/windows.system.profile.systemmanufacturers.systemsupportinfo)

systemsupportinfo.localdeviceinfo

### [windows.system.profile](https://docs.microsoft.com/uwp/api/windows.system.profile)

#### [systemoutofboxexperiencestate](https://docs.microsoft.com/uwp/api/windows.system.profile.systemoutofboxexperiencestate)

systemoutofboxexperiencestate

#### [systemsetupinfo](https://docs.microsoft.com/uwp/api/windows.system.profile.systemsetupinfo)

systemsetupinfo <br> systemsetupinfo.outofboxexperiencestate <br> systemsetupinfo.outofboxexperiencestatechanged

#### [windowsintegritypolicy](https://docs.microsoft.com/uwp/api/windows.system.profile.windowsintegritypolicy)

windowsintegritypolicy <br> windowsintegritypolicy.candisable <br> windowsintegritypolicy.isdisablesupported <br> windowsintegritypolicy.isenabled <br> windowsintegritypolicy.isenabledfortrial <br> windowsintegritypolicy.policychanged

### [windows.system.remotesystems](https://docs.microsoft.com/uwp/api/windows.system.remotesystems)

#### [remotesystem](https://docs.microsoft.com/uwp/api/windows.system.remotesystems.remotesystem)

remotesystem.apps

#### [remotesystemapp](https://docs.microsoft.com/uwp/api/windows.system.remotesystems.remotesystemapp)

remotesystemapp <br> remotesystemapp.attributes <br> remotesystemapp.displayname <br> remotesystemapp.id <br> remotesystemapp.isavailablebyproximity <br> remotesystemapp.isavailablebyspatialproximity

#### [remotesystemappregistration](https://docs.microsoft.com/uwp/api/windows.system.remotesystems.remotesystemappregistration)

remotesystemappregistration <br> remotesystemappregistration.attributes <br> remotesystemappregistration.getdefault <br> remotesystemappregistration.getforuser <br> remotesystemappregistration.saveasync <br> remotesystemappregistration.user

#### [remotesystemconnectioninfo](https://docs.microsoft.com/uwp/api/windows.system.remotesystems.remotesystemconnectioninfo)

remotesystemconnectioninfo <br> remotesystemconnectioninfo.isproximal <br> remotesystemconnectioninfo.trycreatefromappserviceconnection

#### [remotesystemconnectionrequest](https://docs.microsoft.com/uwp/api/windows.system.remotesystems.remotesystemconnectionrequest)

remotesystemconnectionrequest.createforapp <br> remotesystemconnectionrequest.remotesystemapp

#### [remotesystemwebaccountfilter](https://docs.microsoft.com/uwp/api/windows.system.remotesystems.remotesystemwebaccountfilter)

remotesystemwebaccountfilter <br> remotesystemwebaccountfilter.account <br> remotesystemwebaccountfilter.remotesystemwebaccountfilter

### [windows.system.update](https://docs.microsoft.com/uwp/api/windows.system.update)

#### [systemupdateattentionrequiredreason](https://docs.microsoft.com/uwp/api/windows.system.update.systemupdateattentionrequiredreason)

systemupdateattentionrequiredreason

#### [systemupdateitem](https://docs.microsoft.com/uwp/api/windows.system.update.systemupdateitem)

systemupdateitem <br> systemupdateitem.description <br> systemupdateitem.downloadprogress <br> systemupdateitem.extendederror <br> systemupdateitem.id <br> systemupdateitem.installprogress <br> systemupdateitem.revision <br> systemupdateitem.state <br> systemupdateitem.title

#### [systemupdateitemstate](https://docs.microsoft.com/uwp/api/windows.system.update.systemupdateitemstate)

systemupdateitemstate

#### [systemupdatelasterrorinfo](https://docs.microsoft.com/uwp/api/windows.system.update.systemupdatelasterrorinfo)

systemupdatelasterrorinfo <br> systemupdatelasterrorinfo.extendederror <br> systemupdatelasterrorinfo.isinteractive <br> systemupdatelasterrorinfo.state

#### [systemupdatemanager](https://docs.microsoft.com/uwp/api/windows.system.update.systemupdatemanager)

systemupdatemanager <br> systemupdatemanager.attentionrequiredreason <br> systemupdatemanager.blockautomaticrebootasync <br> systemupdatemanager.downloadprogress <br> systemupdatemanager.extendederror <br> systemupdatemanager.getautomaticrebootblockids <br> systemupdatemanager.getflightring <br> systemupdatemanager.getupdateitems <br> systemupdatemanager.installprogress <br> systemupdatemanager.issupported <br> systemupdatemanager.lasterrorinfo <br> systemupdatemanager.lastupdatechecktime <br> systemupdatemanager.lastupdateinstalltime <br> systemupdatemanager.reboottocompleteinstall <br> systemupdatemanager.setflightring <br> systemupdatemanager.startcancelupdates <br> systemupdatemanager.startinstall <br> systemupdatemanager.state <br> systemupdatemanager.statechanged <br> systemupdatemanager.trysetuseractivehours <br> systemupdatemanager.unblockautomaticrebootasync <br> systemupdatemanager.useractivehoursend <br> systemupdatemanager.useractivehoursmax <br> systemupdatemanager.useractivehoursstart

#### [systemupdatemanagerstate](https://docs.microsoft.com/uwp/api/windows.system.update.systemupdatemanagerstate)

systemupdatemanagerstate

#### [systemupdatestartinstallaction](https://docs.microsoft.com/uwp/api/windows.system.update.systemupdatestartinstallaction)

systemupdatestartinstallaction

#### [windows](https://docs.microsoft.com/uwp/api/windows.system.update.windows)

windows.system.update

### [windows.system.userprofile](https://docs.microsoft.com/uwp/api/windows.system.userprofile)

#### [assignedaccesssettings](https://docs.microsoft.com/uwp/api/windows.system.userprofile.assignedaccesssettings)

assignedaccesssettings <br> assignedaccesssettings.getdefault <br> assignedaccesssettings.getforuser <br> assignedaccesssettings.isenabled <br> assignedaccesssettings.issingleappkioskmode <br> assignedaccesssettings.user

### [windows.system](https://docs.microsoft.com/uwp/api/windows.system)

#### [appurihandlerhost](https://docs.microsoft.com/uwp/api/windows.system.appurihandlerhost)

appurihandlerhost <br> appurihandlerhost.appurihandlerhost <br> appurihandlerhost.appurihandlerhost <br> appurihandlerhost.name

#### [appurihandlerregistration](https://docs.microsoft.com/uwp/api/windows.system.appurihandlerregistration)

appurihandlerregistration <br> appurihandlerregistration.getappaddedhostsasync <br> appurihandlerregistration.name <br> appurihandlerregistration.setappaddedhostsasync <br> appurihandlerregistration.user

#### [appurihandlerregistrationmanager](https://docs.microsoft.com/uwp/api/windows.system.appurihandlerregistrationmanager)

appurihandlerregistrationmanager <br> appurihandlerregistrationmanager.getdefault <br> appurihandlerregistrationmanager.getforuser <br> appurihandlerregistrationmanager.trygetregistration <br> appurihandlerregistrationmanager.user

#### [launcher](https://docs.microsoft.com/uwp/api/windows.system.launcher)

launcher.launchfolderpathasync <br> launcher.launchfolderpathasync <br> launcher.launchfolderpathforuserasync <br> launcher.launchfolderpathforuserasync

## windows.ui

### [windows.ui.accessibility](https://docs.microsoft.com/uwp/api/windows.ui.accessibility)

#### [screenreaderpositionchangedeventargs](https://docs.microsoft.com/uwp/api/windows.ui.accessibility.screenreaderpositionchangedeventargs)

screenreaderpositionchangedeventargs <br> screenreaderpositionchangedeventargs.isreadingtext <br> screenreaderpositionchangedeventargs.screenpositioninrawpixels

#### [screenreaderservice](https://docs.microsoft.com/uwp/api/windows.ui.accessibility.screenreaderservice)

screenreaderservice <br> screenreaderservice.currentscreenreaderposition <br> screenreaderservice.screenreaderpositionchanged <br> screenreaderservice.screenreaderservice

#### [windows](https://docs.microsoft.com/uwp/api/windows.ui.accessibility.windows)

windows.ui.accessibility

### [windows.ui.composition.interactions](https://docs.microsoft.com/uwp/api/windows.ui.composition.interactions)

#### [interactionsourceconfiguration](https://docs.microsoft.com/uwp/api/windows.ui.composition.interactions.interactionsourceconfiguration)

interactionsourceconfiguration <br> interactionsourceconfiguration.positionxsourcemode <br> interactionsourceconfiguration.positionysourcemode <br> interactionsourceconfiguration.scalesourcemode

#### [interactionsourceredirectionmode](https://docs.microsoft.com/uwp/api/windows.ui.composition.interactions.interactionsourceredirectionmode)

interactionsourceredirectionmode

#### [interactiontracker](https://docs.microsoft.com/uwp/api/windows.ui.composition.interactions.interactiontracker)

interactiontracker.isinertiafromimpulse <br> interactiontracker.tryupdateposition <br> interactiontracker.tryupdatepositionby

#### [interactiontrackerclampingoption](https://docs.microsoft.com/uwp/api/windows.ui.composition.interactions.interactiontrackerclampingoption)

interactiontrackerclampingoption

#### [interactiontrackerinertiastateenteredargs](https://docs.microsoft.com/uwp/api/windows.ui.composition.interactions.interactiontrackerinertiastateenteredargs)

interactiontrackerinertiastateenteredargs.isinertiafromimpulse

#### [visualinteractionsource](https://docs.microsoft.com/uwp/api/windows.ui.composition.interactions.visualinteractionsource)

visualinteractionsource.pointerwheelconfig

### [windows.ui.composition](https://docs.microsoft.com/uwp/api/windows.ui.composition)

#### [animationpropertyaccessmode](https://docs.microsoft.com/uwp/api/windows.ui.composition.animationpropertyaccessmode)

animationpropertyaccessmode

#### [animationpropertyinfo](https://docs.microsoft.com/uwp/api/windows.ui.composition.animationpropertyinfo)

animationpropertyinfo <br> animationpropertyinfo.accessmode

#### [booleankeyframeanimation](https://docs.microsoft.com/uwp/api/windows.ui.composition.booleankeyframeanimation)

booleankeyframeanimation <br> booleankeyframeanimation.insertkeyframe

#### [compositionanimation](https://docs.microsoft.com/uwp/api/windows.ui.composition.compositionanimation)

compositionanimation.setexpressionreferenceparameter

#### [compositiongeometricclip](https://docs.microsoft.com/uwp/api/windows.ui.composition.compositiongeometricclip)

compositiongeometricclip <br> compositiongeometricclip.geometry <br> compositiongeometricclip.viewbox

#### [compositiongradientbrush](https://docs.microsoft.com/uwp/api/windows.ui.composition.compositiongradientbrush)

compositiongradientbrush.mappingmode

#### [compositionmappingmode](https://docs.microsoft.com/uwp/api/windows.ui.composition.compositionmappingmode)

compositionmappingmode

#### [compositionobject](https://docs.microsoft.com/uwp/api/windows.ui.composition.compositionobject)

compositionobject.populatepropertyinfo <br> compositionobject.startanimationgroupwithianimationobject <br> compositionobject.startanimationwithianimationobject

#### [compositor](https://docs.microsoft.com/uwp/api/windows.ui.composition.compositor)

compositor.createbooleankeyframeanimation <br> compositor.creategeometricclip <br> compositor.creategeometricclip <br> compositor.createredirectvisual <br> compositor.createredirectvisual

#### [ianimationobject](https://docs.microsoft.com/uwp/api/windows.ui.composition.ianimationobject)

ianimationobject <br> ianimationobject.populatepropertyinfo

#### [redirectvisual](https://docs.microsoft.com/uwp/api/windows.ui.composition.redirectvisual)

redirectvisual <br> redirectvisual.source

### [windows.ui.input.inking.preview](https://docs.microsoft.com/uwp/api/windows.ui.input.inking.preview)

#### [palmrejectiondelayzonepreview](https://docs.microsoft.com/uwp/api/windows.ui.input.inking.preview.palmrejectiondelayzonepreview)

palmrejectiondelayzonepreview <br> palmrejectiondelayzonepreview.close <br> palmrejectiondelayzonepreview.createforvisual <br> palmrejectiondelayzonepreview.createforvisual

#### [windows](https://docs.microsoft.com/uwp/api/windows.ui.input.inking.preview.windows)

windows.ui.input.inking.preview

### [windows.ui.input.inking](https://docs.microsoft.com/uwp/api/windows.ui.input.inking)

#### [handwritinglineheight](https://docs.microsoft.com/uwp/api/windows.ui.input.inking.handwritinglineheight)

handwritinglineheight

#### [penandinksettings](https://docs.microsoft.com/uwp/api/windows.ui.input.inking.penandinksettings)

penandinksettings <br> penandinksettings.fontfamilyname <br> penandinksettings.getdefault <br> penandinksettings.handwritinglineheight <br> penandinksettings.ishandwritingdirectlyintotextfieldenabled <br> penandinksettings.istouchhandwritingenabled <br> penandinksettings.penhandedness <br> penandinksettings.userconsentstohandwritingtelemetrycollection

#### [penhandedness](https://docs.microsoft.com/uwp/api/windows.ui.input.inking.penhandedness)

penhandedness

### [windows.ui.notifications](https://docs.microsoft.com/uwp/api/windows.ui.notifications)

#### [scheduledtoastnotificationshowingeventargs](https://docs.microsoft.com/uwp/api/windows.ui.notifications.scheduledtoastnotificationshowingeventargs)

scheduledtoastnotificationshowingeventargs <br> scheduledtoastnotificationshowingeventargs.cancel <br> scheduledtoastnotificationshowingeventargs.getdeferral <br> scheduledtoastnotificationshowingeventargs.scheduledtoastnotification

#### [toastnotifier](https://docs.microsoft.com/uwp/api/windows.ui.notifications.toastnotifier)

toastnotifier.scheduledtoastnotificationshowing

### [windows.ui.shell](https://docs.microsoft.com/uwp/api/windows.ui.shell)

#### [securityappkind](https://docs.microsoft.com/uwp/api/windows.ui.shell.securityappkind)

securityappkind

#### [securityappmanager](https://docs.microsoft.com/uwp/api/windows.ui.shell.securityappmanager)

securityappmanager <br> securityappmanager.register <br> securityappmanager.securityappmanager <br> securityappmanager.unregister <br> securityappmanager.updatestate

#### [securityappstate](https://docs.microsoft.com/uwp/api/windows.ui.shell.securityappstate)

securityappstate

#### [securityappsubstatus](https://docs.microsoft.com/uwp/api/windows.ui.shell.securityappsubstatus)

securityappsubstatus

#### [taskbarmanager](https://docs.microsoft.com/uwp/api/windows.ui.shell.taskbarmanager)

taskbarmanager.issecondarytilepinnedasync <br> taskbarmanager.requestpinsecondarytileasync <br> taskbarmanager.tryunpinsecondarytileasync

### [windows.ui.startscreen](https://docs.microsoft.com/uwp/api/windows.ui.startscreen)

#### [startscreenmanager](https://docs.microsoft.com/uwp/api/windows.ui.startscreen.startscreenmanager)

startscreenmanager.containssecondarytileasync <br> startscreenmanager.tryremovesecondarytileasync

### [windows.ui.text.core](https://docs.microsoft.com/uwp/api/windows.ui.text.core)

#### [coretextlayoutrequest](https://docs.microsoft.com/uwp/api/windows.ui.text.core.coretextlayoutrequest)

coretextlayoutrequest.layoutboundsvisualpixels

### [windows.ui.text](https://docs.microsoft.com/uwp/api/windows.ui.text)

#### [richedittextdocument](https://docs.microsoft.com/uwp/api/windows.ui.text.richedittextdocument)

richedittextdocument.clearundoredohistory

### [windows.ui.viewmanagement.core](https://docs.microsoft.com/uwp/api/windows.ui.viewmanagement.core)

#### [coreinputview](https://docs.microsoft.com/uwp/api/windows.ui.viewmanagement.core.coreinputview)

coreinputview.tryhide <br> coreinputview.tryshow <br> coreinputview.tryshow

#### [coreinputviewkind](https://docs.microsoft.com/uwp/api/windows.ui.viewmanagement.core.coreinputviewkind)

coreinputviewkind

### [windows.ui.webui](https://docs.microsoft.com/uwp/api/windows.ui.webui)

#### [backgroundactivatedeventargs](https://docs.microsoft.com/uwp/api/windows.ui.webui.backgroundactivatedeventargs)

backgroundactivatedeventargs <br> backgroundactivatedeventargs.taskinstance

#### [backgroundactivatedeventhandler](https://docs.microsoft.com/uwp/api/windows.ui.webui.backgroundactivatedeventhandler)

backgroundactivatedeventhandler

#### [newwebuiviewcreatedeventargs](https://docs.microsoft.com/uwp/api/windows.ui.webui.newwebuiviewcreatedeventargs)

newwebuiviewcreatedeventargs <br> newwebuiviewcreatedeventargs.activatedeventargs <br> newwebuiviewcreatedeventargs.getdeferral <br> newwebuiviewcreatedeventargs.haspendingnavigate <br> newwebuiviewcreatedeventargs.webuiview

#### [webuiapplication](https://docs.microsoft.com/uwp/api/windows.ui.webui.webuiapplication)

webuiapplication.backgroundactivated <br> webuiapplication.newwebuiviewcreated

#### [webuiview](https://docs.microsoft.com/uwp/api/windows.ui.webui.webuiview)

webuiview <br> webuiview.activated <br> webuiview.addinitializescript <br> webuiview.applicationviewid <br> webuiview.buildlocalstreamuri <br> webuiview.cangoback <br> webuiview.cangoforward <br> webuiview.capturepreviewtostreamasync <br> webuiview.captureselectedcontenttodatapackageasync <br> webuiview.closed <br> webuiview.containsfullscreenelement <br> webuiview.containsfullscreenelementchanged <br> webuiview.contentloading <br> webuiview.createasync <br> webuiview.createasync <br> webuiview.defaultbackgroundcolor <br> webuiview.deferredpermissionrequests <br> webuiview.documenttitle <br> webuiview.domcontentloaded <br> webuiview.framecontentloading <br> webuiview.framedomcontentloaded <br> webuiview.framenavigationcompleted <br> webuiview.framenavigationstarting <br> webuiview.getdeferredpermissionrequestbyid <br> webuiview.goback <br> webuiview.goforward <br> webuiview.ignoreapplicationcontenturirulesnavigationrestrictions <br> webuiview.invokescriptasync <br> webuiview.longrunningscriptdetected <br> webuiview.navigate <br> webuiview.navigatetolocalstreamuri <br> webuiview.navigatetostring <br> webuiview.navigatewithhttprequestmessage <br> webuiview.navigationcompleted <br> webuiview.navigationstarting <br> webuiview.newwindowrequested <br> webuiview.permissionrequested <br> webuiview.refresh <br> webuiview.scriptnotify <br> webuiview.settings <br> webuiview.source <br> webuiview.stop <br> webuiview.unsafecontentwarningdisplaying <br> webuiview.unsupportedurischemeidentified <br> webuiview.unviewablecontentidentified <br> webuiview.webresourcerequested

### [windows.ui.xaml.automation.peers](https://docs.microsoft.com/uwp/api/windows.ui.xaml.automation.peers)

#### [appbarbuttonautomationpeer](https://docs.microsoft.com/uwp/api/windows.ui.xaml.automation.peers.appbarbuttonautomationpeer)

appbarbuttonautomationpeer.collapse <br> appbarbuttonautomationpeer.expand <br> appbarbuttonautomationpeer.expandcollapsestate

#### [automationpeer](https://docs.microsoft.com/uwp/api/windows.ui.xaml.automation.peers.automationpeer)

automationpeer.isdialog <br> automationpeer.isdialogcore

#### [menubarautomationpeer](https://docs.microsoft.com/uwp/api/windows.ui.xaml.automation.peers.menubarautomationpeer)

menubarautomationpeer <br> menubarautomationpeer.menubarautomationpeer

#### [menubaritemautomationpeer](https://docs.microsoft.com/uwp/api/windows.ui.xaml.automation.peers.menubaritemautomationpeer)

menubaritemautomationpeer <br> menubaritemautomationpeer.collapse <br> menubaritemautomationpeer.expand <br> menubaritemautomationpeer.expandcollapsestate <br> menubaritemautomationpeer.invoke <br> menubaritemautomationpeer.menubaritemautomationpeer

### [windows.ui.xaml.automation](https://docs.microsoft.com/uwp/api/windows.ui.xaml.automation)

#### [automationelementidentifiers](https://docs.microsoft.com/uwp/api/windows.ui.xaml.automation.automationelementidentifiers)

automationelementidentifiers.isdialogproperty

#### [automationproperties](https://docs.microsoft.com/uwp/api/windows.ui.xaml.automation.automationproperties)

automationproperties.getisdialog <br> automationproperties.isdialogproperty <br> automationproperties.setisdialog

### [windows.ui.xaml.controls.maps](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.maps)

#### [maptileanimationstate](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.maps.maptileanimationstate)

maptileanimationstate

#### [maptilebitmaprequestedeventargs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.maps.maptilebitmaprequestedeventargs)

maptilebitmaprequestedeventargs.frameindex

#### [maptilesource](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.maps.maptilesource)

maptilesource.animationstate <br> maptilesource.animationstateproperty <br> maptilesource.autoplay <br> maptilesource.autoplayproperty <br> maptilesource.framecount <br> maptilesource.framecountproperty <br> maptilesource.frameduration <br> maptilesource.framedurationproperty <br> maptilesource.pause <br> maptilesource.play <br> maptilesource.stop

#### [maptileurirequestedeventargs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.maps.maptileurirequestedeventargs)

maptileurirequestedeventargs.frameindex

### [windows.ui.xaml.controls.primitives](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.primitives)

#### [commandbarflyoutcommandbar](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.primitives.commandbarflyoutcommandbar)

commandbarflyoutcommandbar <br> commandbarflyoutcommandbar.commandbarflyoutcommandbar <br> commandbarflyoutcommandbar.flyouttemplatesettings

#### [commandbarflyoutcommandbartemplatesettings](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.primitives.commandbarflyoutcommandbartemplatesettings)

commandbarflyoutcommandbartemplatesettings <br> commandbarflyoutcommandbartemplatesettings.closeanimationendposition <br> commandbarflyoutcommandbartemplatesettings.contentcliprect <br> commandbarflyoutcommandbartemplatesettings.currentwidth <br> commandbarflyoutcommandbartemplatesettings.expanddownanimationendposition <br> commandbarflyoutcommandbartemplatesettings.expanddownanimationholdposition <br> commandbarflyoutcommandbartemplatesettings.expanddownanimationstartposition <br> commandbarflyoutcommandbartemplatesettings.expanddownoverflowverticalposition <br> commandbarflyoutcommandbartemplatesettings.expandedwidth <br> commandbarflyoutcommandbartemplatesettings.expandupanimationendposition <br> commandbarflyoutcommandbartemplatesettings.expandupanimationholdposition <br> commandbarflyoutcommandbartemplatesettings.expandupanimationstartposition <br> commandbarflyoutcommandbartemplatesettings.expandupoverflowverticalposition <br> commandbarflyoutcommandbartemplatesettings.openanimationendposition <br> commandbarflyoutcommandbartemplatesettings.openanimationstartposition <br> commandbarflyoutcommandbartemplatesettings.overflowcontentcliprect <br> commandbarflyoutcommandbartemplatesettings.widthexpansionanimationendposition <br> commandbarflyoutcommandbartemplatesettings.widthexpansionanimationstartposition <br> commandbarflyoutcommandbartemplatesettings.widthexpansiondelta <br> commandbarflyoutcommandbartemplatesettings.widthexpansionmorebuttonanimationendposition <br> commandbarflyoutcommandbartemplatesettings.widthexpansionmorebuttonanimationstartposition

#### [flyoutbase](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.primitives.flyoutbase)

flyoutbase.areopencloseanimationsenabled <br> flyoutbase.areopencloseanimationsenabledproperty <br> flyoutbase.inputdeviceprefersprimarycommands <br> flyoutbase.inputdeviceprefersprimarycommandsproperty <br> flyoutbase.isopen <br> flyoutbase.isopenproperty <br> flyoutbase.showat <br> flyoutbase.showmode <br> flyoutbase.showmodeproperty <br> flyoutbase.targetproperty

#### [flyoutshowmode](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.primitives.flyoutshowmode)

flyoutshowmode

#### [flyoutshowoptions](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.primitives.flyoutshowoptions)

flyoutshowoptions <br> flyoutshowoptions.exclusionrect <br> flyoutshowoptions.flyoutshowoptions <br> flyoutshowoptions.placement <br> flyoutshowoptions.position <br> flyoutshowoptions.showmode

#### [navigationviewitempresenter](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.primitives.navigationviewitempresenter)

navigationviewitempresenter <br> navigationviewitempresenter.icon <br> navigationviewitempresenter.iconproperty <br> navigationviewitempresenter.navigationviewitempresenter

### [windows.ui.xaml.controls](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls)

#### [anchorrequestedeventargs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.anchorrequestedeventargs)

anchorrequestedeventargs <br> anchorrequestedeventargs.anchor <br> anchorrequestedeventargs.anchorcandidates

#### [appbarelementcontainer](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.appbarelementcontainer)

appbarelementcontainer <br> appbarelementcontainer.appbarelementcontainer <br> appbarelementcontainer.dynamicoverfloworder <br> appbarelementcontainer.dynamicoverfloworderproperty <br> appbarelementcontainer.iscompact <br> appbarelementcontainer.iscompactproperty <br> appbarelementcontainer.isinoverflow <br> appbarelementcontainer.isinoverflowproperty

#### [autosuggestbox](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.autosuggestbox)

autosuggestbox.description <br> autosuggestbox.descriptionproperty

#### [backgroundsizing](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.backgroundsizing)

backgroundsizing

#### [border](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.border)

border.backgroundsizing <br> border.backgroundsizingproperty <br> border.backgroundtransition

#### [calendardatepicker](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.calendardatepicker)

calendardatepicker.description <br> calendardatepicker.descriptionproperty

#### [combobox](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.combobox)

combobox.description <br> combobox.descriptionproperty <br> combobox.iseditableproperty <br> combobox.text <br> combobox.textboxstyle <br> combobox.textboxstyleproperty <br> combobox.textproperty <br> combobox.textsubmitted

#### [comboboxtextsubmittedeventargs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.comboboxtextsubmittedeventargs)

comboboxtextsubmittedeventargs <br> comboboxtextsubmittedeventargs.handled <br> comboboxtextsubmittedeventargs.text

#### [commandbarflyout](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.commandbarflyout)

commandbarflyout <br> commandbarflyout.commandbarflyout <br> commandbarflyout.primarycommands <br> commandbarflyout.secondarycommands

#### [contentpresenter](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.contentpresenter)

contentpresenter.backgroundsizing <br> contentpresenter.backgroundsizingproperty <br> contentpresenter.backgroundtransition

#### [control](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.control)

control.backgroundsizing <br> control.backgroundsizingproperty <br> control.cornerradius <br> control.cornerradiusproperty

#### [datatemplateselector](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.datatemplateselector)

datatemplateselector.getelement <br> datatemplateselector.recycleelement

#### [datepicker](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.datepicker)

datepicker.selecteddate <br> datepicker.selecteddatechanged <br> datepicker.selecteddateproperty

#### [datepickerselectedvaluechangedeventargs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.datepickerselectedvaluechangedeventargs)

datepickerselectedvaluechangedeventargs <br> datepickerselectedvaluechangedeventargs.newdate <br> datepickerselectedvaluechangedeventargs.olddate

#### [dropdownbutton](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.dropdownbutton)

dropdownbutton <br> dropdownbutton.dropdownbutton

#### [dropdownbuttonautomationpeer](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.dropdownbuttonautomationpeer)

dropdownbuttonautomationpeer <br> dropdownbuttonautomationpeer.collapse <br> dropdownbuttonautomationpeer.dropdownbuttonautomationpeer <br> dropdownbuttonautomationpeer.expand <br> dropdownbuttonautomationpeer.expandcollapsestate

#### [frame](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.frame)

frame.isnavigationstackenabled <br> frame.isnavigationstackenabledproperty <br> frame.navigatetotype

#### [grid](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.grid)

grid.backgroundsizing <br> grid.backgroundsizingproperty

#### [iconsourceelement](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.iconsourceelement)

iconsourceelement <br> iconsourceelement.iconsource <br> iconsourceelement.iconsourceelement <br> iconsourceelement.iconsourceproperty

#### [iscrollanchorprovider](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.iscrollanchorprovider)

iscrollanchorprovider <br> iscrollanchorprovider.currentanchor <br> iscrollanchorprovider.registeranchorcandidate <br> iscrollanchorprovider.unregisteranchorcandidate

#### [menubar](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.menubar)

menubar <br> menubar.items <br> menubar.itemsproperty <br> menubar.menubar

#### [menubaritem](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.menubaritem)

menubaritem <br> menubaritem.items <br> menubaritem.itemsproperty <br> menubaritem.menubaritem <br> menubaritem.title <br> menubaritem.titleproperty

#### [menubaritemflyout](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.menubaritemflyout)

menubaritemflyout <br> menubaritemflyout.menubaritemflyout

#### [navigationview](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.navigationview)

navigationview.contentoverlay <br> navigationview.contentoverlayproperty <br> navigationview.ispanevisible <br> navigationview.ispanevisibleproperty <br> navigationview.overflowlabelmode <br> navigationview.overflowlabelmodeproperty <br> navigationview.panecustomcontent <br> navigationview.panecustomcontentproperty <br> navigationview.panedisplaymode <br> navigationview.panedisplaymodeproperty <br> navigationview.paneheader <br> navigationview.paneheaderproperty <br> navigationview.selectionfollowsfocus <br> navigationview.selectionfollowsfocusproperty <br> navigationview.shouldernavigationenabled <br> navigationview.shouldernavigationenabledproperty <br> navigationview.templatesettings <br> navigationview.templatesettingsproperty

#### [navigationviewitem](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.navigationviewitem)

navigationviewitem.selectsoninvoked <br> navigationviewitem.selectsoninvokedproperty

#### [navigationviewiteminvokedeventargs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.navigationviewiteminvokedeventargs)

navigationviewiteminvokedeventargs.invokeditemcontainer <br> navigationviewiteminvokedeventargs.recommendednavigationtransitioninfo

#### [navigationviewoverflowlabelmode](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.navigationviewoverflowlabelmode)

navigationviewoverflowlabelmode

#### [navigationviewpanedisplaymode](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.navigationviewpanedisplaymode)

navigationviewpanedisplaymode

#### [navigationviewselectionchangedeventargs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.navigationviewselectionchangedeventargs)

navigationviewselectionchangedeventargs.recommendednavigationtransitioninfo <br> navigationviewselectionchangedeventargs.selecteditemcontainer

#### [navigationviewselectionfollowsfocus](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.navigationviewselectionfollowsfocus)

navigationviewselectionfollowsfocus

#### [navigationviewshouldernavigationenabled](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.navigationviewshouldernavigationenabled)

navigationviewshouldernavigationenabled

#### [navigationviewtemplatesettings](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.navigationviewtemplatesettings)

navigationviewtemplatesettings <br> navigationviewtemplatesettings.backbuttonvisibility <br> navigationviewtemplatesettings.backbuttonvisibilityproperty <br> navigationviewtemplatesettings.leftpanevisibility <br> navigationviewtemplatesettings.leftpanevisibilityproperty <br> navigationviewtemplatesettings.navigationviewtemplatesettings <br> navigationviewtemplatesettings.overflowbuttonvisibility <br> navigationviewtemplatesettings.overflowbuttonvisibilityproperty <br> navigationviewtemplatesettings.panetogglebuttonvisibility <br> navigationviewtemplatesettings.panetogglebuttonvisibilityproperty <br> navigationviewtemplatesettings.singleselectionfollowsfocus <br> navigationviewtemplatesettings.singleselectionfollowsfocusproperty <br> navigationviewtemplatesettings.toppadding <br> navigationviewtemplatesettings.toppaddingproperty <br> navigationviewtemplatesettings.toppanevisibility <br> navigationviewtemplatesettings.toppanevisibilityproperty

#### [panel](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.panel)

panel.backgroundtransition

#### [passwordbox](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.passwordbox)

passwordbox.canpasteclipboardcontent <br> passwordbox.canpasteclipboardcontentproperty <br> passwordbox.description <br> passwordbox.descriptionproperty <br> passwordbox.pastefromclipboard <br> passwordbox.selectionflyout <br> passwordbox.selectionflyoutproperty

#### [relativepanel](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.relativepanel)

relativepanel.backgroundsizing <br> relativepanel.backgroundsizingproperty

#### [richeditbox](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.richeditbox)

richeditbox.description <br> richeditbox.descriptionproperty <br> richeditbox.proofingmenuflyout <br> richeditbox.proofingmenuflyoutproperty <br> richeditbox.selectionchanging <br> richeditbox.selectionflyout <br> richeditbox.selectionflyoutproperty <br> richeditbox.textdocument

#### [richeditboxselectionchangingeventargs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.richeditboxselectionchangingeventargs)

richeditboxselectionchangingeventargs <br> richeditboxselectionchangingeventargs.cancel <br> richeditboxselectionchangingeventargs.selectionlength <br> richeditboxselectionchangingeventargs.selectionstart

#### [richtextblock](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.richtextblock)

richtextblock.copyselectiontoclipboard <br> richtextblock.selectionflyout <br> richtextblock.selectionflyoutproperty

#### [scrollcontentpresenter](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.scrollcontentpresenter)

scrollcontentpresenter.cancontentrenderoutsidebounds <br> scrollcontentpresenter.cancontentrenderoutsideboundsproperty <br> scrollcontentpresenter.sizescontenttotemplatedparent <br> scrollcontentpresenter.sizescontenttotemplatedparentproperty

#### [scrollviewer](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.scrollviewer)

scrollviewer.anchorrequested <br> scrollviewer.cancontentrenderoutsidebounds <br> scrollviewer.cancontentrenderoutsideboundsproperty <br> scrollviewer.currentanchor <br> scrollviewer.getcancontentrenderoutsidebounds <br> scrollviewer.horizontalanchorratio <br> scrollviewer.horizontalanchorratioproperty <br> scrollviewer.reduceviewportforcoreinputviewocclusions <br> scrollviewer.reduceviewportforcoreinputviewocclusionsproperty <br> scrollviewer.registeranchorcandidate <br> scrollviewer.setcancontentrenderoutsidebounds <br> scrollviewer.unregisteranchorcandidate <br> scrollviewer.verticalanchorratio <br> scrollviewer.verticalanchorratioproperty

#### [splitbutton](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.splitbutton)

splitbutton <br> splitbutton.click <br> splitbutton.command <br> splitbutton.commandparameter <br> splitbutton.commandparameterproperty <br> splitbutton.commandproperty <br> splitbutton.flyout <br> splitbutton.flyoutproperty <br> splitbutton.splitbutton

#### [splitbuttonautomationpeer](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.splitbuttonautomationpeer)

splitbuttonautomationpeer <br> splitbuttonautomationpeer.collapse <br> splitbuttonautomationpeer.expand <br> splitbuttonautomationpeer.expandcollapsestate <br> splitbuttonautomationpeer.invoke <br> splitbuttonautomationpeer.splitbuttonautomationpeer

#### [splitbuttonclickeventargs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.splitbuttonclickeventargs)

splitbuttonclickeventargs

#### [stackpanel](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.stackpanel)

stackpanel.backgroundsizing <br> stackpanel.backgroundsizingproperty

#### [textblock](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.textblock)

textblock.copyselectiontoclipboard <br> textblock.selectionflyout <br> textblock.selectionflyoutproperty

#### [textbox](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.textbox)

textbox.canpasteclipboardcontent <br> textbox.canpasteclipboardcontentproperty <br> textbox.canredo <br> textbox.canredoproperty <br> textbox.canundo <br> textbox.canundoproperty <br> textbox.clearundoredohistory <br> textbox.copyselectiontoclipboard <br> textbox.cutselectiontoclipboard <br> textbox.description <br> textbox.descriptionproperty <br> textbox.pastefromclipboard <br> textbox.proofingmenuflyout <br> textbox.proofingmenuflyoutproperty <br> textbox.redo <br> textbox.selectionchanging <br> textbox.selectionflyout <br> textbox.selectionflyoutproperty <br> textbox.undo

#### [textboxselectionchangingeventargs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.textboxselectionchangingeventargs)

textboxselectionchangingeventargs <br> textboxselectionchangingeventargs.cancel <br> textboxselectionchangingeventargs.selectionlength <br> textboxselectionchangingeventargs.selectionstart

#### [textcommandbarflyout](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.textcommandbarflyout)

textcommandbarflyout <br> textcommandbarflyout.textcommandbarflyout

#### [timepicker](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.timepicker)

timepicker.selectedtime <br> timepicker.selectedtimechanged <br> timepicker.selectedtimeproperty

#### [timepickerselectedvaluechangedeventargs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.timepickerselectedvaluechangedeventargs)

timepickerselectedvaluechangedeventargs <br> timepickerselectedvaluechangedeventargs.newtime <br> timepickerselectedvaluechangedeventargs.oldtime

#### [togglesplitbutton](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.togglesplitbutton)

togglesplitbutton <br> togglesplitbutton.ischecked <br> togglesplitbutton.ischeckedchanged <br> togglesplitbutton.togglesplitbutton

#### [togglesplitbuttonautomationpeer](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.togglesplitbuttonautomationpeer)

togglesplitbuttonautomationpeer <br> togglesplitbuttonautomationpeer.collapse <br> togglesplitbuttonautomationpeer.expand <br> togglesplitbuttonautomationpeer.expandcollapsestate <br> togglesplitbuttonautomationpeer.toggle <br> togglesplitbuttonautomationpeer.togglesplitbuttonautomationpeer <br> togglesplitbuttonautomationpeer.togglestate

#### [togglesplitbuttonischeckedchangedeventargs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.togglesplitbuttonischeckedchangedeventargs)

togglesplitbuttonischeckedchangedeventargs

#### [tooltip](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.tooltip)

tooltip.placementrect <br> tooltip.placementrectproperty

#### [treeview](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.treeview)

treeview.candragitems <br> treeview.candragitemsproperty <br> treeview.canreorderitems <br> treeview.canreorderitemsproperty <br> treeview.containerfromitem <br> treeview.containerfromnode <br> treeview.dragitemscompleted <br> treeview.dragitemsstarting <br> treeview.itemcontainerstyle <br> treeview.itemcontainerstyleproperty <br> treeview.itemcontainerstyleselector <br> treeview.itemcontainerstyleselectorproperty <br> treeview.itemcontainertransitions <br> treeview.itemcontainertransitionsproperty <br> treeview.itemfromcontainer <br> treeview.itemssource <br> treeview.itemssourceproperty <br> treeview.itemtemplate <br> treeview.itemtemplateproperty <br> treeview.itemtemplateselector <br> treeview.itemtemplateselectorproperty <br> treeview.nodefromcontainer

#### [treeviewcollapsedeventargs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.treeviewcollapsedeventargs)

treeviewcollapsedeventargs.item

#### [treeviewdragitemscompletedeventargs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.treeviewdragitemscompletedeventargs)

treeviewdragitemscompletedeventargs <br> treeviewdragitemscompletedeventargs.dropresult <br> treeviewdragitemscompletedeventargs.items

#### [treeviewdragitemsstartingeventargs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.treeviewdragitemsstartingeventargs)

treeviewdragitemsstartingeventargs <br> treeviewdragitemsstartingeventargs.cancel <br> treeviewdragitemsstartingeventargs.data <br> treeviewdragitemsstartingeventargs.items

#### [treeviewexpandingeventargs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.treeviewexpandingeventargs)

treeviewexpandingeventargs.item

#### [treeviewitem](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.treeviewitem)

treeviewitem.hasunrealizedchildren <br> treeviewitem.hasunrealizedchildrenproperty <br> treeviewitem.itemssource <br> treeviewitem.itemssourceproperty

#### [webview](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.webview)

webview.webresourcerequested

#### [webviewwebresourcerequestedeventargs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.webviewwebresourcerequestedeventargs)

webviewwebresourcerequestedeventargs <br> webviewwebresourcerequestedeventargs.getdeferral <br> webviewwebresourcerequestedeventargs.request <br> webviewwebresourcerequestedeventargs.response

### [windows.ui.xaml.core.direct](https://docs.microsoft.com/uwp/api/windows.ui.xaml.core.direct)

#### [ixamldirectobject](https://docs.microsoft.com/uwp/api/windows.ui.xaml.core.direct.ixamldirectobject)

ixamldirectobject

#### [windows](https://docs.microsoft.com/uwp/api/windows.ui.xaml.core.direct.windows)

windows.ui.xaml.core.direct

#### [xamldirect](https://docs.microsoft.com/uwp/api/windows.ui.xaml.core.direct.xamldirect)

xamldirect <br> xamldirect.addeventhandler <br> xamldirect.addeventhandler <br> xamldirect.addtocollection <br> xamldirect.clearcollection <br> xamldirect.clearproperty <br> xamldirect.createinstance <br> xamldirect.getbooleanproperty <br> xamldirect.getcollectioncount <br> xamldirect.getcolorproperty <br> xamldirect.getcornerradiusproperty <br> xamldirect.getdatetimeproperty <br> xamldirect.getdefault <br> xamldirect.getdoubleproperty <br> xamldirect.getdurationproperty <br> xamldirect.getenumproperty <br> xamldirect.getgridlengthproperty <br> xamldirect.getint32property <br> xamldirect.getmatrix3dproperty <br> xamldirect.getmatrixproperty <br> xamldirect.getobject <br> xamldirect.getobjectproperty <br> xamldirect.getpointproperty <br> xamldirect.getrectproperty <br> xamldirect.getsizeproperty <br> xamldirect.getstringproperty <br> xamldirect.getthicknessproperty <br> xamldirect.gettimespanproperty <br> xamldirect.getxamldirectobject <br> xamldirect.getxamldirectobjectfromcollectionat <br> xamldirect.getxamldirectobjectproperty <br> xamldirect.insertintocollectionat <br> xamldirect.removeeventhandler <br> xamldirect.removefromcollection <br> xamldirect.removefromcollectionat <br> xamldirect.setbooleanproperty <br> xamldirect.setcolorproperty <br> xamldirect.setcornerradiusproperty <br> xamldirect.setdatetimeproperty <br> xamldirect.setdoubleproperty <br> xamldirect.setdurationproperty <br> xamldirect.setenumproperty <br> xamldirect.setgridlengthproperty <br> xamldirect.setint32property <br> xamldirect.setmatrix3dproperty <br> xamldirect.setmatrixproperty <br> xamldirect.setobjectproperty <br> xamldirect.setpointproperty <br> xamldirect.setrectproperty <br> xamldirect.setsizeproperty <br> xamldirect.setstringproperty <br> xamldirect.setthicknessproperty <br> xamldirect.settimespanproperty <br> xamldirect.setxamldirectobjectproperty

#### [xamleventindex](https://docs.microsoft.com/uwp/api/windows.ui.xaml.core.direct.xamleventindex)

xamleventindex

#### [xamlpropertyindex](https://docs.microsoft.com/uwp/api/windows.ui.xaml.core.direct.xamlpropertyindex)

xamlpropertyindex

#### [xamltypeindex](https://docs.microsoft.com/uwp/api/windows.ui.xaml.core.direct.xamltypeindex)

xamltypeindex

### [windows.ui.xaml.hosting](https://docs.microsoft.com/uwp/api/windows.ui.xaml.hosting)

#### [desktopwindowxamlsource](https://docs.microsoft.com/uwp/api/windows.ui.xaml.hosting.desktopwindowxamlsource)

desktopwindowxamlsource <br> desktopwindowxamlsource.close <br> desktopwindowxamlsource.content <br> desktopwindowxamlsource.desktopwindowxamlsource <br> desktopwindowxamlsource.gotfocus <br> desktopwindowxamlsource.hasfocus <br> desktopwindowxamlsource.navigatefocus <br> desktopwindowxamlsource.takefocusrequested

#### [desktopwindowxamlsourcegotfocuseventargs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.hosting.desktopwindowxamlsourcegotfocuseventargs)

desktopwindowxamlsourcegotfocuseventargs <br> desktopwindowxamlsourcegotfocuseventargs.request

#### [desktopwindowxamlsourcetakefocusrequestedeventargs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.hosting.desktopwindowxamlsourcetakefocusrequestedeventargs)

desktopwindowxamlsourcetakefocusrequestedeventargs <br> desktopwindowxamlsourcetakefocusrequestedeventargs.request

#### [windowsxamlmanager](https://docs.microsoft.com/uwp/api/windows.ui.xaml.hosting.windowsxamlmanager)

windowsxamlmanager <br> windowsxamlmanager.close <br> windowsxamlmanager.initializeforcurrentthread

#### [xamlsourcefocusnavigationreason](https://docs.microsoft.com/uwp/api/windows.ui.xaml.hosting.xamlsourcefocusnavigationreason)

xamlsourcefocusnavigationreason

#### [xamlsourcefocusnavigationrequest](https://docs.microsoft.com/uwp/api/windows.ui.xaml.hosting.xamlsourcefocusnavigationrequest)

xamlsourcefocusnavigationrequest <br> xamlsourcefocusnavigationrequest.correlationid <br> xamlsourcefocusnavigationrequest.hintrect <br> xamlsourcefocusnavigationrequest.reason <br> xamlsourcefocusnavigationrequest.xamlsourcefocusnavigationrequest <br> xamlsourcefocusnavigationrequest.xamlsourcefocusnavigationrequest <br> xamlsourcefocusnavigationrequest.xamlsourcefocusnavigationrequest

#### [xamlsourcefocusnavigationresult](https://docs.microsoft.com/uwp/api/windows.ui.xaml.hosting.xamlsourcefocusnavigationresult)

xamlsourcefocusnavigationresult <br> xamlsourcefocusnavigationresult.wasfocusmoved <br> xamlsourcefocusnavigationresult.xamlsourcefocusnavigationresult

### [windows.ui.xaml.input](https://docs.microsoft.com/uwp/api/windows.ui.xaml.input)

#### [canexecuterequestedeventargs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.input.canexecuterequestedeventargs)

canexecuterequestedeventargs <br> canexecuterequestedeventargs.canexecute <br> canexecuterequestedeventargs.parameter

#### [executerequestedeventargs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.input.executerequestedeventargs)

executerequestedeventargs <br> executerequestedeventargs.parameter

#### [focusmanager](https://docs.microsoft.com/uwp/api/windows.ui.xaml.input.focusmanager)

focusmanager.gettingfocus <br> focusmanager.gotfocus <br> focusmanager.losingfocus <br> focusmanager.lostfocus

#### [focusmanagergotfocuseventargs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.input.focusmanagergotfocuseventargs)

focusmanagergotfocuseventargs <br> focusmanagergotfocuseventargs.correlationid <br> focusmanagergotfocuseventargs.newfocusedelement

#### [focusmanagerlostfocuseventargs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.input.focusmanagerlostfocuseventargs)

focusmanagerlostfocuseventargs <br> focusmanagerlostfocuseventargs.correlationid <br> focusmanagerlostfocuseventargs.oldfocusedelement

#### [gettingfocuseventargs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.input.gettingfocuseventargs)

gettingfocuseventargs.correlationid

#### [losingfocuseventargs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.input.losingfocuseventargs)

losingfocuseventargs.correlationid

#### [standarduicommand](https://docs.microsoft.com/uwp/api/windows.ui.xaml.input.standarduicommand)

standarduicommand <br> standarduicommand.kind <br> standarduicommand.kindproperty <br> standarduicommand.standarduicommand <br> standarduicommand.standarduicommand

#### [standarduicommandkind](https://docs.microsoft.com/uwp/api/windows.ui.xaml.input.standarduicommandkind)

standarduicommandkind

#### [xamluicommand](https://docs.microsoft.com/uwp/api/windows.ui.xaml.input.xamluicommand)

xamluicommand <br> xamluicommand.accesskey <br> xamluicommand.accesskeyproperty <br> xamluicommand.canexecute <br> xamluicommand.canexecutechanged <br> xamluicommand.canexecuterequested <br> xamluicommand.command <br> xamluicommand.commandproperty <br> xamluicommand.description <br> xamluicommand.descriptionproperty <br> xamluicommand.execute <br> xamluicommand.executerequested <br> xamluicommand.iconsource <br> xamluicommand.iconsourceproperty <br> xamluicommand.keyboardaccelerators <br> xamluicommand.keyboardacceleratorsproperty <br> xamluicommand.label <br> xamluicommand.labelproperty <br> xamluicommand.notifycanexecutechanged <br> xamluicommand.xamluicommand

### [windows.ui.xaml.markup](https://docs.microsoft.com/uwp/api/windows.ui.xaml.markup)

#### [fullxamlmetadataproviderattribute](https://docs.microsoft.com/uwp/api/windows.ui.xaml.markup.fullxamlmetadataproviderattribute)

fullxamlmetadataproviderattribute <br> fullxamlmetadataproviderattribute.fullxamlmetadataproviderattribute

#### [ixamlbindscopediagnostics](https://docs.microsoft.com/uwp/api/windows.ui.xaml.markup.ixamlbindscopediagnostics)

ixamlbindscopediagnostics <br> ixamlbindscopediagnostics.disable

#### [ixamltype2](https://docs.microsoft.com/uwp/api/windows.ui.xaml.markup.ixamltype2)

ixamltype2 <br> ixamltype2.boxedtype

### [windows.ui.xaml.media.animation](https://docs.microsoft.com/uwp/api/windows.ui.xaml.media.animation)

#### [basicconnectedanimationconfiguration](https://docs.microsoft.com/uwp/api/windows.ui.xaml.media.animation.basicconnectedanimationconfiguration)

basicconnectedanimationconfiguration <br> basicconnectedanimationconfiguration.basicconnectedanimationconfiguration

#### [connectedanimation](https://docs.microsoft.com/uwp/api/windows.ui.xaml.media.animation.connectedanimation)

connectedanimation.configuration

#### [connectedanimationconfiguration](https://docs.microsoft.com/uwp/api/windows.ui.xaml.media.animation.connectedanimationconfiguration)

connectedanimationconfiguration

#### [directconnectedanimationconfiguration](https://docs.microsoft.com/uwp/api/windows.ui.xaml.media.animation.directconnectedanimationconfiguration)

directconnectedanimationconfiguration <br> directconnectedanimationconfiguration.directconnectedanimationconfiguration

#### [gravityconnectedanimationconfiguration](https://docs.microsoft.com/uwp/api/windows.ui.xaml.media.animation.gravityconnectedanimationconfiguration)

gravityconnectedanimationconfiguration <br> gravityconnectedanimationconfiguration.gravityconnectedanimationconfiguration

#### [slidenavigationtransitioneffect](https://docs.microsoft.com/uwp/api/windows.ui.xaml.media.animation.slidenavigationtransitioneffect)

slidenavigationtransitioneffect

#### [slidenavigationtransitioninfo](https://docs.microsoft.com/uwp/api/windows.ui.xaml.media.animation.slidenavigationtransitioninfo)

slidenavigationtransitioninfo.effect <br> slidenavigationtransitioninfo.effectproperty

### [windows.ui.xaml.media](https://docs.microsoft.com/uwp/api/windows.ui.xaml.media)

#### [brush](https://docs.microsoft.com/uwp/api/windows.ui.xaml.media.brush)

brush.populatepropertyinfo <br> brush.populatepropertyinfooverride

### [windows.ui.xaml.navigation](https://docs.microsoft.com/uwp/api/windows.ui.xaml.navigation)

#### [framenavigationoptions](https://docs.microsoft.com/uwp/api/windows.ui.xaml.navigation.framenavigationoptions)

framenavigationoptions <br> framenavigationoptions.framenavigationoptions <br> framenavigationoptions.isnavigationstackenabled <br> framenavigationoptions.transitioninfooverride

### [windows.ui.xaml](https://docs.microsoft.com/uwp/api/windows.ui.xaml)

#### [brushtransition](https://docs.microsoft.com/uwp/api/windows.ui.xaml.brushtransition)

brushtransition <br> brushtransition.brushtransition <br> brushtransition.duration

#### [colorpaletteresources](https://docs.microsoft.com/uwp/api/windows.ui.xaml.colorpaletteresources)

colorpaletteresources <br> colorpaletteresources.accent <br> colorpaletteresources.althigh <br> colorpaletteresources.altlow <br> colorpaletteresources.altmedium <br> colorpaletteresources.altmediumhigh <br> colorpaletteresources.altmediumlow <br> colorpaletteresources.basehigh <br> colorpaletteresources.baselow <br> colorpaletteresources.basemedium <br> colorpaletteresources.basemediumhigh <br> colorpaletteresources.basemediumlow <br> colorpaletteresources.chromealtlow <br> colorpaletteresources.chromeblackhigh <br> colorpaletteresources.chromeblacklow <br> colorpaletteresources.chromeblackmedium <br> colorpaletteresources.chromeblackmediumlow <br> colorpaletteresources.chromedisabledhigh <br> colorpaletteresources.chromedisabledlow <br> colorpaletteresources.chromegray <br> colorpaletteresources.chromehigh <br> colorpaletteresources.chromelow <br> colorpaletteresources.chromemedium <br> colorpaletteresources.chromemediumlow <br> colorpaletteresources.chromewhite <br> colorpaletteresources.colorpaletteresources <br> colorpaletteresources.errortext <br> colorpaletteresources.listlow <br> colorpaletteresources.listmedium

#### [datatemplate](https://docs.microsoft.com/uwp/api/windows.ui.xaml.datatemplate)

datatemplate.getelement <br> datatemplate.recycleelement

#### [debugsettings](https://docs.microsoft.com/uwp/api/windows.ui.xaml.debugsettings)

debugsettings.failfastonerrors

#### [effectiveviewportchangedeventargs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.effectiveviewportchangedeventargs)

effectiveviewportchangedeventargs <br> effectiveviewportchangedeventargs.bringintoviewdistancex <br> effectiveviewportchangedeventargs.bringintoviewdistancey <br> effectiveviewportchangedeventargs.effectiveviewport <br> effectiveviewportchangedeventargs.maxviewport

#### [elementfactorygetargs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.elementfactorygetargs)

elementfactorygetargs <br> elementfactorygetargs.data <br> elementfactorygetargs.elementfactorygetargs <br> elementfactorygetargs.parent

#### [elementfactoryrecycleargs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.elementfactoryrecycleargs)

elementfactoryrecycleargs <br> elementfactoryrecycleargs.element <br> elementfactoryrecycleargs.elementfactoryrecycleargs <br> elementfactoryrecycleargs.parent

#### [frameworkelement](https://docs.microsoft.com/uwp/api/windows.ui.xaml.frameworkelement)

frameworkelement.effectiveviewportchanged <br> frameworkelement.invalidateviewport <br> frameworkelement.isloaded

#### [ielementfactory](https://docs.microsoft.com/uwp/api/windows.ui.xaml.ielementfactory)

ielementfactory <br> ielementfactory.getelement <br> ielementfactory.recycleelement

#### [scalartransition](https://docs.microsoft.com/uwp/api/windows.ui.xaml.scalartransition)

scalartransition <br> scalartransition.duration <br> scalartransition.scalartransition

#### [uielement](https://docs.microsoft.com/uwp/api/windows.ui.xaml.uielement)

uielement.canbescrollanchor <br> uielement.canbescrollanchorproperty <br> uielement.centerpoint <br> uielement.opacitytransition <br> uielement.populatepropertyinfo <br> uielement.populatepropertyinfooverride <br> uielement.rotation <br> uielement.rotationaxis <br> uielement.rotationtransition <br> uielement.scale <br> uielement.scaletransition <br> uielement.startanimation <br> uielement.stopanimation <br> uielement.transformmatrix <br> uielement.translation <br> uielement.translationtransition

#### [vector3transition](https://docs.microsoft.com/uwp/api/windows.ui.xaml.vector3transition)

vector3transition <br> vector3transition.components <br> vector3transition.duration <br> vector3transition.vector3transition

#### [vector3transitioncomponents](https://docs.microsoft.com/uwp/api/windows.ui.xaml.vector3transitioncomponents)

vector3transitioncomponents

## windows.web

### [windows.web.ui.interop](https://docs.microsoft.com/uwp/api/windows.web.ui.interop)

#### [webviewcontrol](https://docs.microsoft.com/uwp/api/windows.web.ui.interop.webviewcontrol)

webviewcontrol.addinitializescript <br> webviewcontrol.gotfocus <br> webviewcontrol.lostfocus

### [windows.web.ui](https://docs.microsoft.com/uwp/api/windows.web.ui)

#### [iwebviewcontrol2](https://docs.microsoft.com/uwp/api/windows.web.ui.iwebviewcontrol2)

iwebviewcontrol2 <br> iwebviewcontrol2.addinitializescript

