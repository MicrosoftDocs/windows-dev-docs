---
author: QuinnRadich
title: Windows 10 Build 17134 API changes
description: Developers can use the following list to identify new or changed namespaces in Windows 10 build 17134
keywords: what's new, whats new, updates, Windows 10, newest, apis, 17134
ms.author: quradic
ms.date: 4/10/2018
ms.topic: article
ms.prod: windows
ms.technology: uwp
ms.localizationpriority: medium
---

# New APIs in Windows 10 build 17134

New and updated API namespaces have been made available to developers in Windows 10 build 17134 (Also known as the April Update or version 1803). Below is a full list of documentation published for namespaces added or modified in this release.

For information on APIs added in the previous public release, see [New APIs in the Windows 10 Fall Creators Update](windows-10-build-16299-api-diff.md).

## windows.ai

### [windows.ai.machinelearning.preview](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.preview)

#### [featureelementkindpreview](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.preview.featureelementkindpreview)

featureelementkindpreview

#### [ilearningmodelvariabledescriptorpreview](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.preview.ilearningmodelvariabledescriptorpreview)

ilearningmodelvariabledescriptorpreview <br> ilearningmodelvariabledescriptorpreview.description <br> ilearningmodelvariabledescriptorpreview.modelfeaturekind <br> ilearningmodelvariabledescriptorpreview.name <br> ilearningmodelvariabledescriptorpreview.required

#### [imagevariabledescriptorpreview](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.preview.imagevariabledescriptorpreview)

imagevariabledescriptorpreview <br> imagevariabledescriptorpreview.bitmappixelformat <br> imagevariabledescriptorpreview.description <br> imagevariabledescriptorpreview.modelfeaturekind <br> imagevariabledescriptorpreview.name

#### [inferencingoptionspreview](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.preview.inferencingoptionspreview)

inferencingoptionspreview <br> inferencingoptionspreview.istracingenabled <br> inferencingoptionspreview.maxbatchsize <br> inferencingoptionspreview.minimizememoryallocation <br> inferencingoptionspreview.preferreddevicekind <br> inferencingoptionspreview.reclaimmemoryafterevaluation

#### [learningmodelbindingpreview](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.preview.learningmodelbindingpreview)

learningmodelbindingpreview <br> learningmodelbindingpreview.bind <br> learningmodelbindingpreview.clear

#### [learningmodeldescriptionpreview](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.preview.learningmodeldescriptionpreview)

learningmodeldescriptionpreview <br> learningmodeldescriptionpreview.author <br> learningmodeldescriptionpreview.description <br> learningmodeldescriptionpreview.domain <br> learningmodeldescriptionpreview.inputfeatures <br> learningmodeldescriptionpreview.metadata <br> learningmodeldescriptionpreview.name <br> learningmodeldescriptionpreview.outputfeatures <br> learningmodeldescriptionpreview.version

#### [learningmodeldevicekindpreview](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.preview.learningmodeldevicekindpreview)

learningmodeldevicekindpreview

#### [learningmodelevaluationresultpreview](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.preview.learningmodelevaluationresultpreview)

learningmodelevaluationresultpreview <br> learningmodelevaluationresultpreview.correlationid <br> learningmodelevaluationresultpreview.outputs

#### [learningmodelfeaturekindpreview](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.preview.learningmodelfeaturekindpreview)

learningmodelfeaturekindpreview

#### [learningmodelpreview](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.preview.learningmodelpreview)

learningmodelpreview <br> learningmodelpreview.description <br> learningmodelpreview.evaluatefeaturesasync <br> learningmodelpreview.inferencingoptions <br> learningmodelpreview.loadmodelfromstoragefileasync <br> learningmodelpreview.loadmodelfromstreamasync

#### [mapvariabledescriptorpreview](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.preview.mapvariabledescriptorpreview)

mapvariabledescriptorpreview <br> mapvariabledescriptorpreview.description <br> mapvariabledescriptorpreview.fields <br> mapvariabledescriptorpreview.keykind <br> mapvariabledescriptorpreview.modelfeaturekind <br> mapvariabledescriptorpreview.name

#### [sequencevariabledescriptorpreview](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.preview.sequencevariabledescriptorpreview)

sequencevariabledescriptorpreview <br> sequencevariabledescriptorpreview.description <br> sequencevariabledescriptorpreview.elementtype <br> sequencevariabledescriptorpreview.modelfeaturekind <br> sequencevariabledescriptorpreview.name

#### [tensorvariabledescriptorpreview](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.preview.tensorvariabledescriptorpreview)

tensorvariabledescriptorpreview <br> tensorvariabledescriptorpreview.datatype <br> tensorvariabledescriptorpreview.description <br> tensorvariabledescriptorpreview.modelfeaturekind <br> tensorvariabledescriptorpreview.name <br> tensorvariabledescriptorpreview.shape

#### [windows](https://docs.microsoft.com/uwp/api/windows.ai.machinelearning.preview.windows)

windows.ai.machinelearning.preview

## windows.applicationmodel

### [windows.applicationmodel.activation](https://docs.microsoft.com/uwp/api/windows.applicationmodel.activation)

#### [barcodescannerpreviewactivatedeventargs](https://docs.microsoft.com/uwp/api/windows.applicationmodel.activation.barcodescannerpreviewactivatedeventargs)

barcodescannerpreviewactivatedeventargs <br> barcodescannerpreviewactivatedeventargs.connectionid <br> barcodescannerpreviewactivatedeventargs.kind <br> barcodescannerpreviewactivatedeventargs.previousexecutionstate <br> barcodescannerpreviewactivatedeventargs.splashscreen <br> barcodescannerpreviewactivatedeventargs.user

#### [ibarcodescannerpreviewactivatedeventargs](https://docs.microsoft.com/uwp/api/windows.applicationmodel.activation.ibarcodescannerpreviewactivatedeventargs)

ibarcodescannerpreviewactivatedeventargs <br> ibarcodescannerpreviewactivatedeventargs.connectionid

### [windows.applicationmodel.background](https://docs.microsoft.com/uwp/api/windows.applicationmodel.background)

#### [backgroundaccessrequestkind](https://docs.microsoft.com/uwp/api/windows.applicationmodel.background.backgroundaccessrequestkind)

backgroundaccessrequestkind

#### [backgroundexecutionmanager](https://docs.microsoft.com/uwp/api/windows.applicationmodel.background.backgroundexecutionmanager)

backgroundexecutionmanager.requestaccesskindasync

#### [customsystemeventtrigger](https://docs.microsoft.com/uwp/api/windows.applicationmodel.background.customsystemeventtrigger)

customsystemeventtrigger <br> customsystemeventtrigger.customsystemeventtrigger <br> customsystemeventtrigger.recurrence <br> customsystemeventtrigger.triggerid

#### [customsystemeventtriggerrecurrence](https://docs.microsoft.com/uwp/api/windows.applicationmodel.background.customsystemeventtriggerrecurrence)

customsystemeventtriggerrecurrence

#### [mobilebroadbandpcodatachangetrigger](https://docs.microsoft.com/uwp/api/windows.applicationmodel.background.mobilebroadbandpcodatachangetrigger)

mobilebroadbandpcodatachangetrigger <br> mobilebroadbandpcodatachangetrigger.mobilebroadbandpcodatachangetrigger

#### [networkoperatordatausagetrigger](https://docs.microsoft.com/uwp/api/windows.applicationmodel.background.networkoperatordatausagetrigger)

networkoperatordatausagetrigger <br> networkoperatordatausagetrigger.networkoperatordatausagetrigger

#### [storagelibrarychangetrackertrigger](https://docs.microsoft.com/uwp/api/windows.applicationmodel.background.storagelibrarychangetrackertrigger)

storagelibrarychangetrackertrigger <br> storagelibrarychangetrackertrigger.storagelibrarychangetrackertrigger

#### [tetheringentitlementchecktrigger](https://docs.microsoft.com/uwp/api/windows.applicationmodel.background.tetheringentitlementchecktrigger)

tetheringentitlementchecktrigger <br> tetheringentitlementchecktrigger.tetheringentitlementchecktrigger

### [windows.applicationmodel.calls.provider](https://docs.microsoft.com/uwp/api/windows.applicationmodel.calls.provider)

#### [phonecalloriginmanager](https://docs.microsoft.com/uwp/api/windows.applicationmodel.calls.provider.phonecalloriginmanager)

phonecalloriginmanager

### [windows.applicationmodel.calls](https://docs.microsoft.com/uwp/api/windows.applicationmodel.calls)

#### [voipcallcoordinator](https://docs.microsoft.com/uwp/api/windows.applicationmodel.calls.voipcallcoordinator)

voipcallcoordinator.requestnewappinitiatedcall <br> voipcallcoordinator.requestnewincomingcall

#### [voipphonecall](https://docs.microsoft.com/uwp/api/windows.applicationmodel.calls.voipphonecall)

voipphonecall.notifycallaccepted

### [windows.applicationmodel.core](https://docs.microsoft.com/uwp/api/windows.applicationmodel.core)

#### [applistentry](https://docs.microsoft.com/uwp/api/windows.applicationmodel.core.applistentry)

applistentry.launchforuserasync

### [windows.applicationmodel.store.preview.installcontrol](https://docs.microsoft.com/uwp/api/windows.applicationmodel.store.preview.installcontrol)

#### [appinstallitem](https://docs.microsoft.com/uwp/api/windows.applicationmodel.store.preview.installcontrol.appinstallitem)

appinstallitem.launchafterinstall

#### [appinstallmanager](https://docs.microsoft.com/uwp/api/windows.applicationmodel.store.preview.installcontrol.appinstallmanager)

appinstallmanager.getispackageidentityallowedtoinstallasync <br> appinstallmanager.getispackageidentityallowedtoinstallforuserasync <br> appinstallmanager.searchforallupdatesasync <br> appinstallmanager.searchforallupdatesforuserasync <br> appinstallmanager.searchforupdatesasync <br> appinstallmanager.searchforupdatesforuserasync <br> appinstallmanager.startproductinstallasync <br> appinstallmanager.startproductinstallforuserasync

#### [appinstalloptions](https://docs.microsoft.com/uwp/api/windows.applicationmodel.store.preview.installcontrol.appinstalloptions)

appinstalloptions <br> appinstalloptions.allowforcedapprestart <br> appinstalloptions.appinstalloptions <br> appinstalloptions.catalogid <br> appinstalloptions.forceuseofnonremovablestorage <br> appinstalloptions.launchafterinstall <br> appinstalloptions.repair <br> appinstalloptions.targetvolume

#### [appinstallstatus](https://docs.microsoft.com/uwp/api/windows.applicationmodel.store.preview.installcontrol.appinstallstatus)

appinstallstatus.isstaged

#### [appupdateoptions](https://docs.microsoft.com/uwp/api/windows.applicationmodel.store.preview.installcontrol.appupdateoptions)

appupdateoptions <br> appupdateoptions.allowforcedapprestart <br> appupdateoptions.appupdateoptions <br> appupdateoptions.catalogid

### [windows.applicationmodel.useractivities](https://docs.microsoft.com/uwp/api/windows.applicationmodel.useractivities)

#### [useractivity](https://docs.microsoft.com/uwp/api/windows.applicationmodel.useractivities.useractivity)

useractivity.tojson <br> useractivity.tojsonarray <br> useractivity.tryparsefromjson <br> useractivity.tryparsefromjsonarray <br> useractivity.useractivity

#### [useractivitychannel](https://docs.microsoft.com/uwp/api/windows.applicationmodel.useractivities.useractivitychannel)

useractivitychannel.disableautosessioncreation <br> useractivitychannel.getrecentuseractivitiesasync <br> useractivitychannel.getsessionhistoryitemsforuseractivityasync <br> useractivitychannel.trygetforwebaccount

#### [useractivityrequest](https://docs.microsoft.com/uwp/api/windows.applicationmodel.useractivities.useractivityrequest)

useractivityrequest <br> useractivityrequest.setuseractivity

#### [useractivityrequestedeventargs](https://docs.microsoft.com/uwp/api/windows.applicationmodel.useractivities.useractivityrequestedeventargs)

useractivityrequestedeventargs <br> useractivityrequestedeventargs.getdeferral <br> useractivityrequestedeventargs.request

#### [useractivityrequestmanager](https://docs.microsoft.com/uwp/api/windows.applicationmodel.useractivities.useractivityrequestmanager)

useractivityrequestmanager <br> useractivityrequestmanager.useractivityrequested

#### [useractivitysessionhistoryitem](https://docs.microsoft.com/uwp/api/windows.applicationmodel.useractivities.useractivitysessionhistoryitem)

useractivitysessionhistoryitem <br> useractivitysessionhistoryitem.endtime <br> useractivitysessionhistoryitem.starttime <br> useractivitysessionhistoryitem.useractivity

#### [useractivityvisualelements](https://docs.microsoft.com/uwp/api/windows.applicationmodel.useractivities.useractivityvisualelements)

useractivityvisualelements.attributiondisplaytext

### [windows.applicationmodel](https://docs.microsoft.com/uwp/api/windows.applicationmodel)

#### [addresourcepackageoptions](https://docs.microsoft.com/uwp/api/windows.applicationmodel.addresourcepackageoptions)

addresourcepackageoptions

#### [appinstance](https://docs.microsoft.com/uwp/api/windows.applicationmodel.appinstance)

appinstance <br> appinstance.findorregisterinstanceforkey <br> appinstance.getactivatedeventargs <br> appinstance.getinstances <br> appinstance.iscurrentinstance <br> appinstance.key <br> appinstance.recommendedinstance <br> appinstance.redirectactivationto <br> appinstance.unregister

#### [packagecatalog](https://docs.microsoft.com/uwp/api/windows.applicationmodel.packagecatalog)

packagecatalog.addresourcepackageasync <br> packagecatalog.removeresourcepackagesasync

#### [packagecatalogaddresourcepackageresult](https://docs.microsoft.com/uwp/api/windows.applicationmodel.packagecatalogaddresourcepackageresult)

packagecatalogaddresourcepackageresult <br> packagecatalogaddresourcepackageresult.extendederror <br> packagecatalogaddresourcepackageresult.iscomplete <br> packagecatalogaddresourcepackageresult.package

#### [packagecatalogremoveresourcepackagesresult](https://docs.microsoft.com/uwp/api/windows.applicationmodel.packagecatalogremoveresourcepackagesresult)

packagecatalogremoveresourcepackagesresult <br> packagecatalogremoveresourcepackagesresult.extendederror <br> packagecatalogremoveresourcepackagesresult.packagesremoved

#### [packageinstallprogress](https://docs.microsoft.com/uwp/api/windows.applicationmodel.packageinstallprogress)

packageinstallprogress

## windows.devices

### [windows.devices.bluetooth](https://docs.microsoft.com/uwp/api/windows.devices.bluetooth)

#### [bluetoothadapter](https://docs.microsoft.com/uwp/api/windows.devices.bluetooth.bluetoothadapter)

bluetoothadapter.areclassicsecureconnectionssupported <br> bluetoothadapter.arelowenergysecureconnectionssupported

#### [bluetoothdevice](https://docs.microsoft.com/uwp/api/windows.devices.bluetooth.bluetoothdevice)

bluetoothdevice.wassecureconnectionusedforpairing

#### [bluetoothledevice](https://docs.microsoft.com/uwp/api/windows.devices.bluetooth.bluetoothledevice)

bluetoothledevice.wassecureconnectionusedforpairing

### [windows.devices.display](https://docs.microsoft.com/uwp/api/windows.devices.display)

#### [displaymonitor](https://docs.microsoft.com/uwp/api/windows.devices.display.displaymonitor)

displaymonitor <br> displaymonitor.blueprimary <br> displaymonitor.connectionkind <br> displaymonitor.deviceid <br> displaymonitor.displayadapterdeviceid <br> displaymonitor.displayadapterid <br> displaymonitor.displayadaptertargetid <br> displaymonitor.displayname <br> displaymonitor.fromidasync <br> displaymonitor.frominterfaceidasync <br> displaymonitor.getdescriptor <br> displaymonitor.getdeviceselector <br> displaymonitor.greenprimary <br> displaymonitor.maxaveragefullframeluminanceinnits <br> displaymonitor.maxluminanceinnits <br> displaymonitor.minluminanceinnits <br> displaymonitor.nativeresolutioninrawpixels <br> displaymonitor.physicalconnector <br> displaymonitor.physicalsizeininches <br> displaymonitor.rawdpix <br> displaymonitor.rawdpiy <br> displaymonitor.redprimary <br> displaymonitor.usagekind <br> displaymonitor.whitepoint

#### [displaymonitorconnectionkind](https://docs.microsoft.com/uwp/api/windows.devices.display.displaymonitorconnectionkind)

displaymonitorconnectionkind

#### [displaymonitordescriptorkind](https://docs.microsoft.com/uwp/api/windows.devices.display.displaymonitordescriptorkind)

displaymonitordescriptorkind

#### [displaymonitorphysicalconnectorkind](https://docs.microsoft.com/uwp/api/windows.devices.display.displaymonitorphysicalconnectorkind)

displaymonitorphysicalconnectorkind

#### [displaymonitorusagekind](https://docs.microsoft.com/uwp/api/windows.devices.display.displaymonitorusagekind)

displaymonitorusagekind

#### [windows](https://docs.microsoft.com/uwp/api/windows.devices.display.windows)

windows.devices.display

### [windows.devices.input.preview](https://docs.microsoft.com/uwp/api/windows.devices.input.preview)

#### [gazedeviceconfigurationstatepreview](https://docs.microsoft.com/uwp/api/windows.devices.input.preview.gazedeviceconfigurationstatepreview)

gazedeviceconfigurationstatepreview

#### [gazedevicepreview](https://docs.microsoft.com/uwp/api/windows.devices.input.preview.gazedevicepreview)

gazedevicepreview <br> gazedevicepreview.cantrackeyes <br> gazedevicepreview.cantrackhead <br> gazedevicepreview.configurationstate <br> gazedevicepreview.getbooleancontroldescriptions <br> gazedevicepreview.getnumericcontroldescriptions <br> gazedevicepreview.id <br> gazedevicepreview.requestcalibrationasync

#### [gazedevicewatcheraddedprevieweventargs](https://docs.microsoft.com/uwp/api/windows.devices.input.preview.gazedevicewatcheraddedprevieweventargs)

gazedevicewatcheraddedprevieweventargs <br> gazedevicewatcheraddedprevieweventargs.device

#### [gazedevicewatcherpreview](https://docs.microsoft.com/uwp/api/windows.devices.input.preview.gazedevicewatcherpreview)

gazedevicewatcherpreview <br> gazedevicewatcherpreview.added <br> gazedevicewatcherpreview.enumerationcompleted <br> gazedevicewatcherpreview.removed <br> gazedevicewatcherpreview.start <br> gazedevicewatcherpreview.stop <br> gazedevicewatcherpreview.updated

#### [gazedevicewatcherremovedprevieweventargs](https://docs.microsoft.com/uwp/api/windows.devices.input.preview.gazedevicewatcherremovedprevieweventargs)

gazedevicewatcherremovedprevieweventargs <br> gazedevicewatcherremovedprevieweventargs.device

#### [gazedevicewatcherupdatedprevieweventargs](https://docs.microsoft.com/uwp/api/windows.devices.input.preview.gazedevicewatcherupdatedprevieweventargs)

gazedevicewatcherupdatedprevieweventargs <br> gazedevicewatcherupdatedprevieweventargs.device

#### [gazeenteredprevieweventargs](https://docs.microsoft.com/uwp/api/windows.devices.input.preview.gazeenteredprevieweventargs)

gazeenteredprevieweventargs <br> gazeenteredprevieweventargs.currentpoint <br> gazeenteredprevieweventargs.handled

#### [gazeexitedprevieweventargs](https://docs.microsoft.com/uwp/api/windows.devices.input.preview.gazeexitedprevieweventargs)

gazeexitedprevieweventargs <br> gazeexitedprevieweventargs.currentpoint <br> gazeexitedprevieweventargs.handled

#### [gazeinputsourcepreview](https://docs.microsoft.com/uwp/api/windows.devices.input.preview.gazeinputsourcepreview)

gazeinputsourcepreview <br> gazeinputsourcepreview.createwatcher <br> gazeinputsourcepreview.gazeentered <br> gazeinputsourcepreview.gazeexited <br> gazeinputsourcepreview.gazemoved <br> gazeinputsourcepreview.getforcurrentview

#### [gazemovedprevieweventargs](https://docs.microsoft.com/uwp/api/windows.devices.input.preview.gazemovedprevieweventargs)

gazemovedprevieweventargs <br> gazemovedprevieweventargs.currentpoint <br> gazemovedprevieweventargs.getintermediatepoints <br> gazemovedprevieweventargs.handled

#### [gazepointpreview](https://docs.microsoft.com/uwp/api/windows.devices.input.preview.gazepointpreview)

gazepointpreview <br> gazepointpreview.eyegazeposition <br> gazepointpreview.headgazeposition <br> gazepointpreview.hidinputreport <br> gazepointpreview.sourcedevice <br> gazepointpreview.timestamp

#### [windows](https://docs.microsoft.com/uwp/api/windows.devices.input.preview.windows)

windows.devices.input.preview

### [windows.devices.pointofservice.provider](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.provider)

#### [barcodescannerdisablescannerrequest](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.provider.barcodescannerdisablescannerrequest)

barcodescannerdisablescannerrequest <br> barcodescannerdisablescannerrequest.reportcompletedasync <br> barcodescannerdisablescannerrequest.reportfailedasync

#### [barcodescannerdisablescannerrequesteventargs](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.provider.barcodescannerdisablescannerrequesteventargs)

barcodescannerdisablescannerrequesteventargs <br> barcodescannerdisablescannerrequesteventargs.getdeferral <br> barcodescannerdisablescannerrequesteventargs.request

#### [barcodescannerenablescannerrequest](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.provider.barcodescannerenablescannerrequest)

barcodescannerenablescannerrequest <br> barcodescannerenablescannerrequest.reportcompletedasync <br> barcodescannerenablescannerrequest.reportfailedasync

#### [barcodescannerenablescannerrequesteventargs](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.provider.barcodescannerenablescannerrequesteventargs)

barcodescannerenablescannerrequesteventargs <br> barcodescannerenablescannerrequesteventargs.getdeferral <br> barcodescannerenablescannerrequesteventargs.request

#### [barcodescannergetsymbologyattributesrequest](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.provider.barcodescannergetsymbologyattributesrequest)

barcodescannergetsymbologyattributesrequest <br> barcodescannergetsymbologyattributesrequest.reportcompletedasync <br> barcodescannergetsymbologyattributesrequest.reportfailedasync <br> barcodescannergetsymbologyattributesrequest.symbology

#### [barcodescannergetsymbologyattributesrequesteventargs](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.provider.barcodescannergetsymbologyattributesrequesteventargs)

barcodescannergetsymbologyattributesrequesteventargs <br> barcodescannergetsymbologyattributesrequesteventargs.getdeferral <br> barcodescannergetsymbologyattributesrequesteventargs.request

#### [barcodescannerhidevideopreviewrequest](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.provider.barcodescannerhidevideopreviewrequest)

barcodescannerhidevideopreviewrequest <br> barcodescannerhidevideopreviewrequest.reportcompletedasync <br> barcodescannerhidevideopreviewrequest.reportfailedasync

#### [barcodescannerhidevideopreviewrequesteventargs](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.provider.barcodescannerhidevideopreviewrequesteventargs)

barcodescannerhidevideopreviewrequesteventargs <br> barcodescannerhidevideopreviewrequesteventargs.getdeferral <br> barcodescannerhidevideopreviewrequesteventargs.request

#### [barcodescannerproviderconnection](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.provider.barcodescannerproviderconnection)

barcodescannerproviderconnection <br> barcodescannerproviderconnection.close <br> barcodescannerproviderconnection.companyname <br> barcodescannerproviderconnection.disablescannerrequested <br> barcodescannerproviderconnection.enablescannerrequested <br> barcodescannerproviderconnection.getbarcodesymbologyattributesrequested <br> barcodescannerproviderconnection.hidevideopreviewrequested <br> barcodescannerproviderconnection.id <br> barcodescannerproviderconnection.name <br> barcodescannerproviderconnection.reporterrorasync <br> barcodescannerproviderconnection.reporterrorasync <br> barcodescannerproviderconnection.reportscanneddataasync <br> barcodescannerproviderconnection.reporttriggerstateasync <br> barcodescannerproviderconnection.setactivesymbologiesrequested <br> barcodescannerproviderconnection.setbarcodesymbologyattributesrequested <br> barcodescannerproviderconnection.start <br> barcodescannerproviderconnection.startsoftwaretriggerrequested <br> barcodescannerproviderconnection.stopsoftwaretriggerrequested <br> barcodescannerproviderconnection.supportedsymbologies <br> barcodescannerproviderconnection.version <br> barcodescannerproviderconnection.videodeviceid

#### [barcodescannerprovidertriggerdetails](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.provider.barcodescannerprovidertriggerdetails)

barcodescannerprovidertriggerdetails <br> barcodescannerprovidertriggerdetails.connection

#### [barcodescannersetactivesymbologiesrequest](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.provider.barcodescannersetactivesymbologiesrequest)

barcodescannersetactivesymbologiesrequest <br> barcodescannersetactivesymbologiesrequest.reportcompletedasync <br> barcodescannersetactivesymbologiesrequest.reportfailedasync <br> barcodescannersetactivesymbologiesrequest.symbologies

#### [barcodescannersetactivesymbologiesrequesteventargs](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.provider.barcodescannersetactivesymbologiesrequesteventargs)

barcodescannersetactivesymbologiesrequesteventargs <br> barcodescannersetactivesymbologiesrequesteventargs.getdeferral <br> barcodescannersetactivesymbologiesrequesteventargs.request

#### [barcodescannersetsymbologyattributesrequest](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.provider.barcodescannersetsymbologyattributesrequest)

barcodescannersetsymbologyattributesrequest <br> barcodescannersetsymbologyattributesrequest.attributes <br> barcodescannersetsymbologyattributesrequest.reportcompletedasync <br> barcodescannersetsymbologyattributesrequest.reportfailedasync <br> barcodescannersetsymbologyattributesrequest.symbology

#### [barcodescannersetsymbologyattributesrequesteventargs](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.provider.barcodescannersetsymbologyattributesrequesteventargs)

barcodescannersetsymbologyattributesrequesteventargs <br> barcodescannersetsymbologyattributesrequesteventargs.getdeferral <br> barcodescannersetsymbologyattributesrequesteventargs.request

#### [barcodescannerstartsoftwaretriggerrequest](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.provider.barcodescannerstartsoftwaretriggerrequest)

barcodescannerstartsoftwaretriggerrequest <br> barcodescannerstartsoftwaretriggerrequest.reportcompletedasync <br> barcodescannerstartsoftwaretriggerrequest.reportfailedasync

#### [barcodescannerstartsoftwaretriggerrequesteventargs](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.provider.barcodescannerstartsoftwaretriggerrequesteventargs)

barcodescannerstartsoftwaretriggerrequesteventargs <br> barcodescannerstartsoftwaretriggerrequesteventargs.getdeferral <br> barcodescannerstartsoftwaretriggerrequesteventargs.request

#### [barcodescannerstopsoftwaretriggerrequest](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.provider.barcodescannerstopsoftwaretriggerrequest)

barcodescannerstopsoftwaretriggerrequest <br> barcodescannerstopsoftwaretriggerrequest.reportcompletedasync <br> barcodescannerstopsoftwaretriggerrequest.reportfailedasync

#### [barcodescannerstopsoftwaretriggerrequesteventargs](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.provider.barcodescannerstopsoftwaretriggerrequesteventargs)

barcodescannerstopsoftwaretriggerrequesteventargs <br> barcodescannerstopsoftwaretriggerrequesteventargs.getdeferral <br> barcodescannerstopsoftwaretriggerrequesteventargs.request

#### [barcodescannertriggerstate](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.provider.barcodescannertriggerstate)

barcodescannertriggerstate

#### [barcodesymbologyattributesbuilder](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.provider.barcodesymbologyattributesbuilder)

barcodesymbologyattributesbuilder <br> barcodesymbologyattributesbuilder.barcodesymbologyattributesbuilder <br> barcodesymbologyattributesbuilder.createattributes <br> barcodesymbologyattributesbuilder.ischeckdigittransmissionsupported <br> barcodesymbologyattributesbuilder.ischeckdigitvalidationsupported <br> barcodesymbologyattributesbuilder.isdecodelengthsupported

#### [windows](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.provider.windows)

windows.devices.pointofservice.provider

### [windows.devices.pointofservice](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice)

#### [barcodescannerreport](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.barcodescannerreport)

barcodescannerreport.barcodescannerreport

#### [claimedbarcodescanner](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.claimedbarcodescanner)

claimedbarcodescanner.hidevideopreview <br> claimedbarcodescanner.isvideopreviewshownonenable <br> claimedbarcodescanner.showvideopreviewasync

#### [unifiedposerrordata](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.unifiedposerrordata)

unifiedposerrordata.unifiedposerrordata

## windows.foundation

### [windows.foundation.numerics](https://docs.microsoft.com/uwp/api/windows.foundation.numerics)

#### [rational](https://docs.microsoft.com/uwp/api/windows.foundation.numerics.rational)

rational

## windows.globalization

### [windows.globalization](https://docs.microsoft.com/uwp/api/windows.globalization)

#### [applicationlanguages](https://docs.microsoft.com/uwp/api/windows.globalization.applicationlanguages)

applicationlanguages.getlanguagesforuser

#### [language](https://docs.microsoft.com/uwp/api/windows.globalization.language)

language.layoutdirection

#### [languagelayoutdirection](https://docs.microsoft.com/uwp/api/windows.globalization.languagelayoutdirection)

languagelayoutdirection

## windows.graphics

### [windows.graphics.capture](https://docs.microsoft.com/uwp/api/windows.graphics.capture)

#### [direct3d11captureframe](https://docs.microsoft.com/uwp/api/windows.graphics.capture.direct3d11captureframe)

direct3d11captureframe <br> direct3d11captureframe.close <br> direct3d11captureframe.contentsize <br> direct3d11captureframe.surface <br> direct3d11captureframe.systemrelativetime

#### [direct3d11captureframepool](https://docs.microsoft.com/uwp/api/windows.graphics.capture.direct3d11captureframepool)

direct3d11captureframepool <br> direct3d11captureframepool.close <br> direct3d11captureframepool.create <br> direct3d11captureframepool.createcapturesession <br> direct3d11captureframepool.dispatcherqueue <br> direct3d11captureframepool.framearrived <br> direct3d11captureframepool.recreate <br> direct3d11captureframepool.trygetnextframe

#### [graphicscaptureitem](https://docs.microsoft.com/uwp/api/windows.graphics.capture.graphicscaptureitem)

graphicscaptureitem <br> graphicscaptureitem.closed <br> graphicscaptureitem.displayname <br> graphicscaptureitem.size

#### [graphicscapturepicker](https://docs.microsoft.com/uwp/api/windows.graphics.capture.graphicscapturepicker)

graphicscapturepicker <br> graphicscapturepicker.graphicscapturepicker <br> graphicscapturepicker.picksingleitemasync

#### [graphicscapturesession](https://docs.microsoft.com/uwp/api/windows.graphics.capture.graphicscapturesession)

graphicscapturesession <br> graphicscapturesession.close <br> graphicscapturesession.issupported <br> graphicscapturesession.startcapture

#### [windows](https://docs.microsoft.com/uwp/api/windows.graphics.capture.windows)

windows.graphics.capture

### [windows.graphics.display](https://docs.microsoft.com/uwp/api/windows.graphics.display)

#### [advancedcolorinfo](https://docs.microsoft.com/uwp/api/windows.graphics.display.advancedcolorinfo)

advancedcolorinfo <br> advancedcolorinfo.blueprimary <br> advancedcolorinfo.currentadvancedcolorkind <br> advancedcolorinfo.greenprimary <br> advancedcolorinfo.isadvancedcolorkindavailable <br> advancedcolorinfo.ishdrmetadataformatcurrentlysupported <br> advancedcolorinfo.maxaveragefullframeluminanceinnits <br> advancedcolorinfo.maxluminanceinnits <br> advancedcolorinfo.minluminanceinnits <br> advancedcolorinfo.redprimary <br> advancedcolorinfo.sdrwhitelevelinnits <br> advancedcolorinfo.whitepoint

#### [advancedcolorkind](https://docs.microsoft.com/uwp/api/windows.graphics.display.advancedcolorkind)

advancedcolorkind

#### [brightnessoverridesettings](https://docs.microsoft.com/uwp/api/windows.graphics.display.brightnessoverridesettings)

brightnessoverridesettings <br> brightnessoverridesettings.createfromdisplaybrightnessoverridescenario <br> brightnessoverridesettings.createfromlevel <br> brightnessoverridesettings.createfromnits <br> brightnessoverridesettings.desiredlevel <br> brightnessoverridesettings.desirednits

#### [coloroverridesettings](https://docs.microsoft.com/uwp/api/windows.graphics.display.coloroverridesettings)

coloroverridesettings <br> coloroverridesettings.createfromdisplaycoloroverridescenario <br> coloroverridesettings.desireddisplaycoloroverridescenario

#### [displaybrightnessoverridescenario](https://docs.microsoft.com/uwp/api/windows.graphics.display.displaybrightnessoverridescenario)

displaybrightnessoverridescenario

#### [displaycoloroverridescenario](https://docs.microsoft.com/uwp/api/windows.graphics.display.displaycoloroverridescenario)

displaycoloroverridescenario

#### [displayenhancementoverride](https://docs.microsoft.com/uwp/api/windows.graphics.display.displayenhancementoverride)

displayenhancementoverride <br> displayenhancementoverride.brightnessoverridesettings <br> displayenhancementoverride.canoverride <br> displayenhancementoverride.canoverridechanged <br> displayenhancementoverride.coloroverridesettings <br> displayenhancementoverride.displayenhancementoverridecapabilitieschanged <br> displayenhancementoverride.getcurrentdisplayenhancementoverridecapabilities <br> displayenhancementoverride.getforcurrentview <br> displayenhancementoverride.isoverrideactive <br> displayenhancementoverride.isoverrideactivechanged <br> displayenhancementoverride.requestoverride <br> displayenhancementoverride.stopoverride

#### [displayenhancementoverridecapabilities](https://docs.microsoft.com/uwp/api/windows.graphics.display.displayenhancementoverridecapabilities)

displayenhancementoverridecapabilities <br> displayenhancementoverridecapabilities.getsupportednitranges <br> displayenhancementoverridecapabilities.isbrightnesscontrolsupported <br> displayenhancementoverridecapabilities.isbrightnessnitscontrolsupported

#### [displayenhancementoverridecapabilitieschangedeventargs](https://docs.microsoft.com/uwp/api/windows.graphics.display.displayenhancementoverridecapabilitieschangedeventargs)

displayenhancementoverridecapabilitieschangedeventargs <br> displayenhancementoverridecapabilitieschangedeventargs.capabilities

#### [displayinformation](https://docs.microsoft.com/uwp/api/windows.graphics.display.displayinformation)

displayinformation.advancedcolorinfochanged <br> displayinformation.getadvancedcolorinfo

#### [hdrmetadataformat](https://docs.microsoft.com/uwp/api/windows.graphics.display.hdrmetadataformat)

hdrmetadataformat

#### [nitrange](https://docs.microsoft.com/uwp/api/windows.graphics.display.nitrange)

nitrange

### [windows.graphics.holographic](https://docs.microsoft.com/uwp/api/windows.graphics.holographic)

#### [holographiccamera](https://docs.microsoft.com/uwp/api/windows.graphics.holographic.holographiccamera)

holographiccamera.canoverrideviewport

#### [holographiccamerapose](https://docs.microsoft.com/uwp/api/windows.graphics.holographic.holographiccamerapose)

holographiccamerapose.overrideprojectiontransform <br> holographiccamerapose.overrideviewport <br> holographiccamerapose.overrideviewtransform

#### [holographicframepresentationmonitor](https://docs.microsoft.com/uwp/api/windows.graphics.holographic.holographicframepresentationmonitor)

holographicframepresentationmonitor <br> holographicframepresentationmonitor.close <br> holographicframepresentationmonitor.readreports

#### [holographicframepresentationreport](https://docs.microsoft.com/uwp/api/windows.graphics.holographic.holographicframepresentationreport)

holographicframepresentationreport <br> holographicframepresentationreport.appgpuduration <br> holographicframepresentationreport.appgpuoverrun <br> holographicframepresentationreport.compositorgpuduration <br> holographicframepresentationreport.missedpresentationopportunitycount <br> holographicframepresentationreport.presentationcount

#### [holographicspace](https://docs.microsoft.com/uwp/api/windows.graphics.holographic.holographicspace)

holographicspace.createframepresentationmonitor <br> holographicspace.userpresence <br> holographicspace.userpresencechanged <br> holographicspace.waitfornextframeready <br> holographicspace.waitfornextframereadywithheadstart

#### [holographicspaceuserpresence](https://docs.microsoft.com/uwp/api/windows.graphics.holographic.holographicspaceuserpresence)

holographicspaceuserpresence

### [windows.graphics.printing.optiondetails](https://docs.microsoft.com/uwp/api/windows.graphics.printing.optiondetails)

#### [printbindingoptiondetails](https://docs.microsoft.com/uwp/api/windows.graphics.printing.optiondetails.printbindingoptiondetails)

printbindingoptiondetails.description <br> printbindingoptiondetails.warningtext

#### [printborderingoptiondetails](https://docs.microsoft.com/uwp/api/windows.graphics.printing.optiondetails.printborderingoptiondetails)

printborderingoptiondetails.description <br> printborderingoptiondetails.warningtext

#### [printcollationoptiondetails](https://docs.microsoft.com/uwp/api/windows.graphics.printing.optiondetails.printcollationoptiondetails)

printcollationoptiondetails.description <br> printcollationoptiondetails.warningtext

#### [printcolormodeoptiondetails](https://docs.microsoft.com/uwp/api/windows.graphics.printing.optiondetails.printcolormodeoptiondetails)

printcolormodeoptiondetails.description <br> printcolormodeoptiondetails.warningtext

#### [printcopiesoptiondetails](https://docs.microsoft.com/uwp/api/windows.graphics.printing.optiondetails.printcopiesoptiondetails)

printcopiesoptiondetails.description <br> printcopiesoptiondetails.warningtext

#### [printcustomitemlistoptiondetails](https://docs.microsoft.com/uwp/api/windows.graphics.printing.optiondetails.printcustomitemlistoptiondetails)

printcustomitemlistoptiondetails.additem <br> printcustomitemlistoptiondetails.description <br> printcustomitemlistoptiondetails.warningtext

#### [printcustomtextoptiondetails](https://docs.microsoft.com/uwp/api/windows.graphics.printing.optiondetails.printcustomtextoptiondetails)

printcustomtextoptiondetails.description <br> printcustomtextoptiondetails.warningtext

#### [printcustomtoggleoptiondetails](https://docs.microsoft.com/uwp/api/windows.graphics.printing.optiondetails.printcustomtoggleoptiondetails)

printcustomtoggleoptiondetails <br> printcustomtoggleoptiondetails.description <br> printcustomtoggleoptiondetails.displayname <br> printcustomtoggleoptiondetails.errortext <br> printcustomtoggleoptiondetails.optionid <br> printcustomtoggleoptiondetails.optiontype <br> printcustomtoggleoptiondetails.state <br> printcustomtoggleoptiondetails.trysetvalue <br> printcustomtoggleoptiondetails.value <br> printcustomtoggleoptiondetails.warningtext

#### [printduplexoptiondetails](https://docs.microsoft.com/uwp/api/windows.graphics.printing.optiondetails.printduplexoptiondetails)

printduplexoptiondetails.description <br> printduplexoptiondetails.warningtext

#### [printholepunchoptiondetails](https://docs.microsoft.com/uwp/api/windows.graphics.printing.optiondetails.printholepunchoptiondetails)

printholepunchoptiondetails.description <br> printholepunchoptiondetails.warningtext

#### [printmediasizeoptiondetails](https://docs.microsoft.com/uwp/api/windows.graphics.printing.optiondetails.printmediasizeoptiondetails)

printmediasizeoptiondetails.description <br> printmediasizeoptiondetails.warningtext

#### [printmediatypeoptiondetails](https://docs.microsoft.com/uwp/api/windows.graphics.printing.optiondetails.printmediatypeoptiondetails)

printmediatypeoptiondetails.description <br> printmediatypeoptiondetails.warningtext

#### [printorientationoptiondetails](https://docs.microsoft.com/uwp/api/windows.graphics.printing.optiondetails.printorientationoptiondetails)

printorientationoptiondetails.description <br> printorientationoptiondetails.warningtext

#### [printpagerangeoptiondetails](https://docs.microsoft.com/uwp/api/windows.graphics.printing.optiondetails.printpagerangeoptiondetails)

printpagerangeoptiondetails <br> printpagerangeoptiondetails.description <br> printpagerangeoptiondetails.errortext <br> printpagerangeoptiondetails.optionid <br> printpagerangeoptiondetails.optiontype <br> printpagerangeoptiondetails.state <br> printpagerangeoptiondetails.trysetvalue <br> printpagerangeoptiondetails.value <br> printpagerangeoptiondetails.warningtext

#### [printqualityoptiondetails](https://docs.microsoft.com/uwp/api/windows.graphics.printing.optiondetails.printqualityoptiondetails)

printqualityoptiondetails.description <br> printqualityoptiondetails.warningtext

#### [printstapleoptiondetails](https://docs.microsoft.com/uwp/api/windows.graphics.printing.optiondetails.printstapleoptiondetails)

printstapleoptiondetails.description <br> printstapleoptiondetails.warningtext

#### [printtaskoptiondetails](https://docs.microsoft.com/uwp/api/windows.graphics.printing.optiondetails.printtaskoptiondetails)

printtaskoptiondetails.createtoggleoption

### [windows.graphics.printing](https://docs.microsoft.com/uwp/api/windows.graphics.printing)

#### [printpagerange](https://docs.microsoft.com/uwp/api/windows.graphics.printing.printpagerange)

printpagerange <br> printpagerange.firstpagenumber <br> printpagerange.lastpagenumber <br> printpagerange.printpagerange <br> printpagerange.printpagerange

#### [printpagerangeoptions](https://docs.microsoft.com/uwp/api/windows.graphics.printing.printpagerangeoptions)

printpagerangeoptions <br> printpagerangeoptions.allowallpages <br> printpagerangeoptions.allowcurrentpage <br> printpagerangeoptions.allowcustomsetofpages

#### [printtaskoptions](https://docs.microsoft.com/uwp/api/windows.graphics.printing.printtaskoptions)

printtaskoptions.custompageranges <br> printtaskoptions.pagerangeoptions

#### [standardprinttaskoptions](https://docs.microsoft.com/uwp/api/windows.graphics.printing.standardprinttaskoptions)

standardprinttaskoptions.custompageranges

## windows.management

### [windows.management.deployment](https://docs.microsoft.com/uwp/api/windows.management.deployment)

#### [packagemanager](https://docs.microsoft.com/uwp/api/windows.management.deployment.packagemanager)

packagemanager.requestaddpackageasync

### [windows.management.update](https://docs.microsoft.com/uwp/api/windows.management.update)

#### [previewbuildsmanager](https://docs.microsoft.com/uwp/api/windows.management.update.previewbuildsmanager)

previewbuildsmanager <br> previewbuildsmanager.arepreviewbuildsallowed <br> previewbuildsmanager.getcurrentstate <br> previewbuildsmanager.getdefault <br> previewbuildsmanager.issupported <br> previewbuildsmanager.syncasync

#### [previewbuildsstate](https://docs.microsoft.com/uwp/api/windows.management.update.previewbuildsstate)

previewbuildsstate <br> previewbuildsstate.properties

#### [windows](https://docs.microsoft.com/uwp/api/windows.management.update.windows)

windows.management.update

## windows.media

### [windows.media.audio](https://docs.microsoft.com/uwp/api/windows.media.audio)

#### [audiograph](https://docs.microsoft.com/uwp/api/windows.media.audio.audiograph)

audiograph.createmediasourceaudioinputnodeasync <br> audiograph.createmediasourceaudioinputnodeasync

#### [audiographsettings](https://docs.microsoft.com/uwp/api/windows.media.audio.audiographsettings)

audiographsettings.maxplaybackspeedfactor

#### [audiostatemonitor](https://docs.microsoft.com/uwp/api/windows.media.audio.audiostatemonitor)

audiostatemonitor <br> audiostatemonitor.createforcapturemonitoring <br> audiostatemonitor.createforcapturemonitoring <br> audiostatemonitor.createforcapturemonitoring <br> audiostatemonitor.createforcapturemonitoringwithcategoryanddeviceid <br> audiostatemonitor.createforrendermonitoring <br> audiostatemonitor.createforrendermonitoring <br> audiostatemonitor.createforrendermonitoring <br> audiostatemonitor.createforrendermonitoringwithcategoryanddeviceid <br> audiostatemonitor.soundlevel <br> audiostatemonitor.soundlevelchanged

#### [createmediasourceaudioinputnoderesult](https://docs.microsoft.com/uwp/api/windows.media.audio.createmediasourceaudioinputnoderesult)

createmediasourceaudioinputnoderesult <br> createmediasourceaudioinputnoderesult.node <br> createmediasourceaudioinputnoderesult.status

#### [mediasourceaudioinputnode](https://docs.microsoft.com/uwp/api/windows.media.audio.mediasourceaudioinputnode)

mediasourceaudioinputnode <br> mediasourceaudioinputnode.addoutgoingconnection <br> mediasourceaudioinputnode.addoutgoingconnection <br> mediasourceaudioinputnode.close <br> mediasourceaudioinputnode.consumeinput <br> mediasourceaudioinputnode.disableeffectsbydefinition <br> mediasourceaudioinputnode.duration <br> mediasourceaudioinputnode.effectdefinitions <br> mediasourceaudioinputnode.emitter <br> mediasourceaudioinputnode.enableeffectsbydefinition <br> mediasourceaudioinputnode.encodingproperties <br> mediasourceaudioinputnode.endtime <br> mediasourceaudioinputnode.loopcount <br> mediasourceaudioinputnode.mediasource <br> mediasourceaudioinputnode.mediasourcecompleted <br> mediasourceaudioinputnode.outgoingconnections <br> mediasourceaudioinputnode.outgoinggain <br> mediasourceaudioinputnode.playbackspeedfactor <br> mediasourceaudioinputnode.position <br> mediasourceaudioinputnode.removeoutgoingconnection <br> mediasourceaudioinputnode.reset <br> mediasourceaudioinputnode.seek <br> mediasourceaudioinputnode.start <br> mediasourceaudioinputnode.starttime <br> mediasourceaudioinputnode.stop

#### [mediasourceaudioinputnodecreationstatus](https://docs.microsoft.com/uwp/api/windows.media.audio.mediasourceaudioinputnodecreationstatus)

mediasourceaudioinputnodecreationstatus

### [windows.media.capture.frames](https://docs.microsoft.com/uwp/api/windows.media.capture.frames)

#### [audiomediaframe](https://docs.microsoft.com/uwp/api/windows.media.capture.frames.audiomediaframe)

audiomediaframe <br> audiomediaframe.audioencodingproperties <br> audiomediaframe.framereference <br> audiomediaframe.getaudioframe

#### [mediaframeformat](https://docs.microsoft.com/uwp/api/windows.media.capture.frames.mediaframeformat)

mediaframeformat.audioencodingproperties

#### [mediaframereference](https://docs.microsoft.com/uwp/api/windows.media.capture.frames.mediaframereference)

mediaframereference.audiomediaframe

#### [mediaframesourcecontroller](https://docs.microsoft.com/uwp/api/windows.media.capture.frames.mediaframesourcecontroller)

mediaframesourcecontroller.audiodevicecontroller

#### [mediaframesourceinfo](https://docs.microsoft.com/uwp/api/windows.media.capture.frames.mediaframesourceinfo)

mediaframesourceinfo.profileid <br> mediaframesourceinfo.videoprofilemediadescription

### [windows.media.capture](https://docs.microsoft.com/uwp/api/windows.media.capture)

#### [capturedframe](https://docs.microsoft.com/uwp/api/windows.media.capture.capturedframe)

capturedframe.bitmapproperties <br> capturedframe.controlvalues

#### [mediacapturesettings](https://docs.microsoft.com/uwp/api/windows.media.capture.mediacapturesettings)

mediacapturesettings.direct3d11device

#### [mediacapturevideoprofile](https://docs.microsoft.com/uwp/api/windows.media.capture.mediacapturevideoprofile)

mediacapturevideoprofile.framesourceinfos <br> mediacapturevideoprofile.properties

#### [mediacapturevideoprofilemediadescription](https://docs.microsoft.com/uwp/api/windows.media.capture.mediacapturevideoprofilemediadescription)

mediacapturevideoprofilemediadescription.properties <br> mediacapturevideoprofilemediadescription.subtype

### [windows.media.core](https://docs.microsoft.com/uwp/api/windows.media.core)

#### [audiostreamdescriptor](https://docs.microsoft.com/uwp/api/windows.media.core.audiostreamdescriptor)

audiostreamdescriptor.copy

#### [mediabindingeventargs](https://docs.microsoft.com/uwp/api/windows.media.core.mediabindingeventargs)

mediabindingeventargs.setdownloadoperation

#### [mediasource](https://docs.microsoft.com/uwp/api/windows.media.core.mediasource)

mediasource.createfromdownloadoperation <br> mediasource.downloadoperation

#### [timedmetadatastreamdescriptor](https://docs.microsoft.com/uwp/api/windows.media.core.timedmetadatastreamdescriptor)

timedmetadatastreamdescriptor <br> timedmetadatastreamdescriptor.copy <br> timedmetadatastreamdescriptor.encodingproperties <br> timedmetadatastreamdescriptor.isselected <br> timedmetadatastreamdescriptor.label <br> timedmetadatastreamdescriptor.language <br> timedmetadatastreamdescriptor.name <br> timedmetadatastreamdescriptor.timedmetadatastreamdescriptor

#### [videostreamdescriptor](https://docs.microsoft.com/uwp/api/windows.media.core.videostreamdescriptor)

videostreamdescriptor.copy

### [windows.media.devices](https://docs.microsoft.com/uwp/api/windows.media.devices)

#### [videodevicecontroller](https://docs.microsoft.com/uwp/api/windows.media.devices.videodevicecontroller)

videodevicecontroller.videotemporaldenoisingcontrol

#### [videotemporaldenoisingcontrol](https://docs.microsoft.com/uwp/api/windows.media.devices.videotemporaldenoisingcontrol)

videotemporaldenoisingcontrol <br> videotemporaldenoisingcontrol.mode <br> videotemporaldenoisingcontrol.supported <br> videotemporaldenoisingcontrol.supportedmodes

#### [videotemporaldenoisingmode](https://docs.microsoft.com/uwp/api/windows.media.devices.videotemporaldenoisingmode)

videotemporaldenoisingmode

### [windows.media.dialprotocol](https://docs.microsoft.com/uwp/api/windows.media.dialprotocol)

#### [dialreceiverapp](https://docs.microsoft.com/uwp/api/windows.media.dialprotocol.dialreceiverapp)

dialreceiverapp.getuniquedevicenameasync

### [windows.media.effects](https://docs.microsoft.com/uwp/api/windows.media.effects)

#### [videotransformeffectdefinition](https://docs.microsoft.com/uwp/api/windows.media.effects.videotransformeffectdefinition)

videotransformeffectdefinition.sphericalprojection

#### [videotransformsphericalprojection](https://docs.microsoft.com/uwp/api/windows.media.effects.videotransformsphericalprojection)

videotransformsphericalprojection <br> videotransformsphericalprojection.frameformat <br> videotransformsphericalprojection.horizontalfieldofviewindegrees <br> videotransformsphericalprojection.isenabled <br> videotransformsphericalprojection.projectionmode <br> videotransformsphericalprojection.vieworientation

### [windows.media.mediaproperties](https://docs.microsoft.com/uwp/api/windows.media.mediaproperties)

#### [audioencodingproperties](https://docs.microsoft.com/uwp/api/windows.media.mediaproperties.audioencodingproperties)

audioencodingproperties.copy

#### [containerencodingproperties](https://docs.microsoft.com/uwp/api/windows.media.mediaproperties.containerencodingproperties)

containerencodingproperties.copy

#### [imageencodingproperties](https://docs.microsoft.com/uwp/api/windows.media.mediaproperties.imageencodingproperties)

imageencodingproperties.copy

#### [mediaencodingprofile](https://docs.microsoft.com/uwp/api/windows.media.mediaproperties.mediaencodingprofile)

mediaencodingprofile.gettimedmetadatatracks <br> mediaencodingprofile.settimedmetadatatracks

#### [mediaencodingsubtypes](https://docs.microsoft.com/uwp/api/windows.media.mediaproperties.mediaencodingsubtypes)

mediaencodingsubtypes.p010

#### [timedmetadataencodingproperties](https://docs.microsoft.com/uwp/api/windows.media.mediaproperties.timedmetadataencodingproperties)

timedmetadataencodingproperties <br> timedmetadataencodingproperties.copy <br> timedmetadataencodingproperties.getformatuserdata <br> timedmetadataencodingproperties.properties <br> timedmetadataencodingproperties.setformatuserdata <br> timedmetadataencodingproperties.subtype <br> timedmetadataencodingproperties.timedmetadataencodingproperties <br> timedmetadataencodingproperties.type

#### [videoencodingproperties](https://docs.microsoft.com/uwp/api/windows.media.mediaproperties.videoencodingproperties)

videoencodingproperties.copy

### [windows.media.playback](https://docs.microsoft.com/uwp/api/windows.media.playback)

#### [mediaplaybacksession](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplaybacksession)

mediaplaybacksession.getoutputdegradationpolicystate <br> mediaplaybacksession.playbackrotation

#### [mediaplaybacksessionoutputdegradationpolicystate](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplaybacksessionoutputdegradationpolicystate)

mediaplaybacksessionoutputdegradationpolicystate <br> mediaplaybacksessionoutputdegradationpolicystate.videoconstrictionreason

#### [mediaplaybacksessionvideoconstrictionreason](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplaybacksessionvideoconstrictionreason)

mediaplaybacksessionvideoconstrictionreason

#### [mediaplayer](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplayer)

mediaplayer.audiostatemonitor

### [windows.media.speechsynthesis](https://docs.microsoft.com/uwp/api/windows.media.speechsynthesis)

#### [speechappendedsilence](https://docs.microsoft.com/uwp/api/windows.media.speechsynthesis.speechappendedsilence)

speechappendedsilence

#### [speechpunctuationsilence](https://docs.microsoft.com/uwp/api/windows.media.speechsynthesis.speechpunctuationsilence)

speechpunctuationsilence

#### [speechsynthesizeroptions](https://docs.microsoft.com/uwp/api/windows.media.speechsynthesis.speechsynthesizeroptions)

speechsynthesizeroptions.appendedsilence <br> speechsynthesizeroptions.punctuationsilence

### [windows.media.streaming.adaptive](https://docs.microsoft.com/uwp/api/windows.media.streaming.adaptive)

#### [adaptivemediasourcediagnosticavailableeventargs](https://docs.microsoft.com/uwp/api/windows.media.streaming.adaptive.adaptivemediasourcediagnosticavailableeventargs)

adaptivemediasourcediagnosticavailableeventargs.resourcecontenttype <br> adaptivemediasourcediagnosticavailableeventargs.resourceduration

#### [adaptivemediasourcedownloadcompletedeventargs](https://docs.microsoft.com/uwp/api/windows.media.streaming.adaptive.adaptivemediasourcedownloadcompletedeventargs)

adaptivemediasourcedownloadcompletedeventargs.resourcecontenttype <br> adaptivemediasourcedownloadcompletedeventargs.resourceduration

#### [adaptivemediasourcedownloadfailedeventargs](https://docs.microsoft.com/uwp/api/windows.media.streaming.adaptive.adaptivemediasourcedownloadfailedeventargs)

adaptivemediasourcedownloadfailedeventargs.resourcecontenttype <br> adaptivemediasourcedownloadfailedeventargs.resourceduration

#### [adaptivemediasourcedownloadrequestedeventargs](https://docs.microsoft.com/uwp/api/windows.media.streaming.adaptive.adaptivemediasourcedownloadrequestedeventargs)

adaptivemediasourcedownloadrequestedeventargs.resourcecontenttype <br> adaptivemediasourcedownloadrequestedeventargs.resourceduration

### [windows.media](https://docs.microsoft.com/uwp/api/windows.media)

#### [videoframe](https://docs.microsoft.com/uwp/api/windows.media.videoframe)

videoframe.copytoasync <br> videoframe.createasdirect3d11surfacebacked <br> videoframe.createasdirect3d11surfacebacked <br> videoframe.createwithdirect3d11surface <br> videoframe.createwithsoftwarebitmap

## windows.networking

### [windows.networking.backgroundtransfer](https://docs.microsoft.com/uwp/api/windows.networking.backgroundtransfer)

#### [downloadoperation](https://docs.microsoft.com/uwp/api/windows.networking.backgroundtransfer.downloadoperation)

downloadoperation.makecurrentintransfergroup

#### [uploadoperation](https://docs.microsoft.com/uwp/api/windows.networking.backgroundtransfer.uploadoperation)

uploadoperation.makecurrentintransfergroup

### [windows.networking.connectivity](https://docs.microsoft.com/uwp/api/windows.networking.connectivity)

#### [cellularapncontext](https://docs.microsoft.com/uwp/api/windows.networking.connectivity.cellularapncontext)

cellularapncontext.profilename

#### [connectionprofilefilter](https://docs.microsoft.com/uwp/api/windows.networking.connectivity.connectionprofilefilter)

connectionprofilefilter.purposeguid

#### [wwanconnectionprofiledetails](https://docs.microsoft.com/uwp/api/windows.networking.connectivity.wwanconnectionprofiledetails)

wwanconnectionprofiledetails.ipkind <br> wwanconnectionprofiledetails.purposeguids

#### [wwannetworkipkind](https://docs.microsoft.com/uwp/api/windows.networking.connectivity.wwannetworkipkind)

wwannetworkipkind

### [windows.networking.networkoperators](https://docs.microsoft.com/uwp/api/windows.networking.networkoperators)

#### [esim](https://docs.microsoft.com/uwp/api/windows.networking.networkoperators.esim)

esim <br> esim.availablememoryinbytes <br> esim.deleteprofileasync <br> esim.downloadprofilemetadataasync <br> esim.eid <br> esim.firmwareversion <br> esim.getprofiles <br> esim.mobilebroadbandmodemdeviceid <br> esim.policy <br> esim.profilechanged <br> esim.resetasync <br> esim.state

#### [esimaddedeventargs](https://docs.microsoft.com/uwp/api/windows.networking.networkoperators.esimaddedeventargs)

esimaddedeventargs <br> esimaddedeventargs.esim

#### [esimauthenticationpreference](https://docs.microsoft.com/uwp/api/windows.networking.networkoperators.esimauthenticationpreference)

esimauthenticationpreference

#### [esimdownloadprofilemetadataresult](https://docs.microsoft.com/uwp/api/windows.networking.networkoperators.esimdownloadprofilemetadataresult)

esimdownloadprofilemetadataresult <br> esimdownloadprofilemetadataresult.profilemetadata <br> esimdownloadprofilemetadataresult.result

#### [esimmanager](https://docs.microsoft.com/uwp/api/windows.networking.networkoperators.esimmanager)

esimmanager <br> esimmanager.serviceinfo <br> esimmanager.serviceinfochanged <br> esimmanager.trycreateesimwatcher

#### [esimoperationresult](https://docs.microsoft.com/uwp/api/windows.networking.networkoperators.esimoperationresult)

esimoperationresult <br> esimoperationresult.status

#### [esimoperationstatus](https://docs.microsoft.com/uwp/api/windows.networking.networkoperators.esimoperationstatus)

esimoperationstatus

#### [esimpolicy](https://docs.microsoft.com/uwp/api/windows.networking.networkoperators.esimpolicy)

esimpolicy <br> esimpolicy.shouldenablemanagingui

#### [esimprofile](https://docs.microsoft.com/uwp/api/windows.networking.networkoperators.esimprofile)

esimprofile <br> esimprofile.class <br> esimprofile.disableasync <br> esimprofile.enableasync <br> esimprofile.id <br> esimprofile.nickname <br> esimprofile.policy <br> esimprofile.providericon <br> esimprofile.providerid <br> esimprofile.providername <br> esimprofile.setnicknameasync <br> esimprofile.state

#### [esimprofileclass](https://docs.microsoft.com/uwp/api/windows.networking.networkoperators.esimprofileclass)

esimprofileclass

#### [esimprofileinstallprogress](https://docs.microsoft.com/uwp/api/windows.networking.networkoperators.esimprofileinstallprogress)

esimprofileinstallprogress

#### [esimprofilemetadata](https://docs.microsoft.com/uwp/api/windows.networking.networkoperators.esimprofilemetadata)

esimprofilemetadata <br> esimprofilemetadata.confirminstallasync <br> esimprofilemetadata.confirminstallasync <br> esimprofilemetadata.denyinstallasync <br> esimprofilemetadata.id <br> esimprofilemetadata.isconfirmationcoderequired <br> esimprofilemetadata.policy <br> esimprofilemetadata.postponeinstallasync <br> esimprofilemetadata.providericon <br> esimprofilemetadata.providerid <br> esimprofilemetadata.providername <br> esimprofilemetadata.state <br> esimprofilemetadata.statechanged

#### [esimprofilemetadatastate](https://docs.microsoft.com/uwp/api/windows.networking.networkoperators.esimprofilemetadatastate)

esimprofilemetadatastate

#### [esimprofilepolicy](https://docs.microsoft.com/uwp/api/windows.networking.networkoperators.esimprofilepolicy)

esimprofilepolicy <br> esimprofilepolicy.candelete <br> esimprofilepolicy.candisable <br> esimprofilepolicy.ismanagedbyenterprise

#### [esimprofilestate](https://docs.microsoft.com/uwp/api/windows.networking.networkoperators.esimprofilestate)

esimprofilestate

#### [esimremovedeventargs](https://docs.microsoft.com/uwp/api/windows.networking.networkoperators.esimremovedeventargs)

esimremovedeventargs <br> esimremovedeventargs.esim

#### [esimserviceinfo](https://docs.microsoft.com/uwp/api/windows.networking.networkoperators.esimserviceinfo)

esimserviceinfo <br> esimserviceinfo.authenticationpreference <br> esimserviceinfo.isesimuienabled

#### [esimstate](https://docs.microsoft.com/uwp/api/windows.networking.networkoperators.esimstate)

esimstate

#### [esimupdatedeventargs](https://docs.microsoft.com/uwp/api/windows.networking.networkoperators.esimupdatedeventargs)

esimupdatedeventargs <br> esimupdatedeventargs.esim

#### [esimwatcher](https://docs.microsoft.com/uwp/api/windows.networking.networkoperators.esimwatcher)

esimwatcher <br> esimwatcher.added <br> esimwatcher.enumerationcompleted <br> esimwatcher.removed <br> esimwatcher.start <br> esimwatcher.status <br> esimwatcher.stop <br> esimwatcher.stopped <br> esimwatcher.updated

#### [esimwatcherstatus](https://docs.microsoft.com/uwp/api/windows.networking.networkoperators.esimwatcherstatus)

esimwatcherstatus

#### [mobilebroadbandantennasar](https://docs.microsoft.com/uwp/api/windows.networking.networkoperators.mobilebroadbandantennasar)

mobilebroadbandantennasar.mobilebroadbandantennasar

#### [mobilebroadbandmodem](https://docs.microsoft.com/uwp/api/windows.networking.networkoperators.mobilebroadbandmodem)

mobilebroadbandmodem.isinemergencycallmode <br> mobilebroadbandmodem.isinemergencycallmodechanged <br> mobilebroadbandmodem.trygetpcoasync

#### [mobilebroadbandmodemisolation](https://docs.microsoft.com/uwp/api/windows.networking.networkoperators.mobilebroadbandmodemisolation)

mobilebroadbandmodemisolation <br> mobilebroadbandmodemisolation.addallowedhost <br> mobilebroadbandmodemisolation.addallowedhostrange <br> mobilebroadbandmodemisolation.applyconfigurationasync <br> mobilebroadbandmodemisolation.clearconfigurationasync <br> mobilebroadbandmodemisolation.mobilebroadbandmodemisolation

#### [mobilebroadbandpco](https://docs.microsoft.com/uwp/api/windows.networking.networkoperators.mobilebroadbandpco)

mobilebroadbandpco <br> mobilebroadbandpco.data <br> mobilebroadbandpco.deviceid <br> mobilebroadbandpco.iscomplete

#### [mobilebroadbandpcodatachangetriggerdetails](https://docs.microsoft.com/uwp/api/windows.networking.networkoperators.mobilebroadbandpcodatachangetriggerdetails)

mobilebroadbandpcodatachangetriggerdetails <br> mobilebroadbandpcodatachangetriggerdetails.updateddata

#### [networkoperatordatausagenotificationkind](https://docs.microsoft.com/uwp/api/windows.networking.networkoperators.networkoperatordatausagenotificationkind)

networkoperatordatausagenotificationkind

#### [networkoperatordatausagetriggerdetails](https://docs.microsoft.com/uwp/api/windows.networking.networkoperators.networkoperatordatausagetriggerdetails)

networkoperatordatausagetriggerdetails <br> networkoperatordatausagetriggerdetails.notificationkind

#### [tetheringentitlementchecktriggerdetails](https://docs.microsoft.com/uwp/api/windows.networking.networkoperators.tetheringentitlementchecktriggerdetails)

tetheringentitlementchecktriggerdetails <br> tetheringentitlementchecktriggerdetails.allowtethering <br> tetheringentitlementchecktriggerdetails.denytethering <br> tetheringentitlementchecktriggerdetails.networkaccountid

### [windows.networking.sockets](https://docs.microsoft.com/uwp/api/windows.networking.sockets)

#### [messagewebsocket](https://docs.microsoft.com/uwp/api/windows.networking.sockets.messagewebsocket)

messagewebsocket.sendfinalframeasync <br> messagewebsocket.sendnonfinalframeasync

#### [servermessagewebsocket](https://docs.microsoft.com/uwp/api/windows.networking.sockets.servermessagewebsocket)

servermessagewebsocket <br> servermessagewebsocket.close <br> servermessagewebsocket.close <br> servermessagewebsocket.closed <br> servermessagewebsocket.control <br> servermessagewebsocket.information <br> servermessagewebsocket.messagereceived <br> servermessagewebsocket.outputstream

#### [servermessagewebsocketcontrol](https://docs.microsoft.com/uwp/api/windows.networking.sockets.servermessagewebsocketcontrol)

servermessagewebsocketcontrol <br> servermessagewebsocketcontrol.messagetype

#### [servermessagewebsocketinformation](https://docs.microsoft.com/uwp/api/windows.networking.sockets.servermessagewebsocketinformation)

servermessagewebsocketinformation <br> servermessagewebsocketinformation.bandwidthstatistics <br> servermessagewebsocketinformation.localaddress <br> servermessagewebsocketinformation.protocol

#### [serverstreamwebsocket](https://docs.microsoft.com/uwp/api/windows.networking.sockets.serverstreamwebsocket)

serverstreamwebsocket <br> serverstreamwebsocket.close <br> serverstreamwebsocket.close <br> serverstreamwebsocket.closed <br> serverstreamwebsocket.information <br> serverstreamwebsocket.inputstream <br> serverstreamwebsocket.outputstream

#### [serverstreamwebsocketinformation](https://docs.microsoft.com/uwp/api/windows.networking.sockets.serverstreamwebsocketinformation)

serverstreamwebsocketinformation <br> serverstreamwebsocketinformation.bandwidthstatistics <br> serverstreamwebsocketinformation.localaddress <br> serverstreamwebsocketinformation.protocol

### [windows.networking.vpn](https://docs.microsoft.com/uwp/api/windows.networking.vpn)

#### [vpnchannel](https://docs.microsoft.com/uwp/api/windows.networking.vpn.vpnchannel)

vpnchannel.addandassociatetransport <br> vpnchannel.currentrequesttransportcontext <br> vpnchannel.getslottypefortransportcontext <br> vpnchannel.replaceandassociatetransport <br> vpnchannel.startreconnectingtransport <br> vpnchannel.startwithtrafficfilter

#### [vpnpacketbuffer](https://docs.microsoft.com/uwp/api/windows.networking.vpn.vpnpacketbuffer)

vpnpacketbuffer.transportcontext

## windows.phone

### [windows.phone.networking.voip](https://docs.microsoft.com/uwp/api/windows.phone.networking.voip)

#### [voipcallcoordinator](https://docs.microsoft.com/uwp/api/windows.phone.networking.voip.voipcallcoordinator)

voipcallcoordinator.requestnewappinitiatedcall <br> voipcallcoordinator.requestnewincomingcall

#### [voipphonecall](https://docs.microsoft.com/uwp/api/windows.phone.networking.voip.voipphonecall)

voipphonecall.notifycallaccepted

## windows.security

### [windows.security.authentication.web.core](https://docs.microsoft.com/uwp/api/windows.security.authentication.web.core)

#### [findallaccountsresult](https://docs.microsoft.com/uwp/api/windows.security.authentication.web.core.findallaccountsresult)

findallaccountsresult <br> findallaccountsresult.accounts <br> findallaccountsresult.providererror <br> findallaccountsresult.status

#### [findallwebaccountsstatus](https://docs.microsoft.com/uwp/api/windows.security.authentication.web.core.findallwebaccountsstatus)

findallwebaccountsstatus

#### [webauthenticationcoremanager](https://docs.microsoft.com/uwp/api/windows.security.authentication.web.core.webauthenticationcoremanager)

webauthenticationcoremanager.findallaccountsasync <br> webauthenticationcoremanager.findallaccountsasync <br> webauthenticationcoremanager.findsystemaccountproviderasync <br> webauthenticationcoremanager.findsystemaccountproviderasync <br> webauthenticationcoremanager.findsystemaccountproviderasync

### [windows.security.authentication.web.provider](https://docs.microsoft.com/uwp/api/windows.security.authentication.web.provider)

#### [webprovidertokenrequest](https://docs.microsoft.com/uwp/api/windows.security.authentication.web.provider.webprovidertokenrequest)

webprovidertokenrequest.applicationpackagefamilyname <br> webprovidertokenrequest.applicationprocessname <br> webprovidertokenrequest.checkapplicationforcapabilityasync

### [windows.security.credentials](https://docs.microsoft.com/uwp/api/windows.security.credentials)

#### [webaccountprovider](https://docs.microsoft.com/uwp/api/windows.security.credentials.webaccountprovider)

webaccountprovider.issystemprovider

## windows.services

### [windows.services.maps](https://docs.microsoft.com/uwp/api/windows.services.maps)

#### [maproutedrivingoptions](https://docs.microsoft.com/uwp/api/windows.services.maps.maproutedrivingoptions)

maproutedrivingoptions.departuretime

#### [placeinfo](https://docs.microsoft.com/uwp/api/windows.services.maps.placeinfo)

placeinfo.createfromaddress <br> placeinfo.createfromaddress

### [windows.services.store](https://docs.microsoft.com/uwp/api/windows.services.store)

#### [storecanacquirelicenseresult](https://docs.microsoft.com/uwp/api/windows.services.store.storecanacquirelicenseresult)

storecanacquirelicenseresult <br> storecanacquirelicenseresult.extendederror <br> storecanacquirelicenseresult.licensablesku <br> storecanacquirelicenseresult.status

#### [storecanlicensestatus](https://docs.microsoft.com/uwp/api/windows.services.store.storecanlicensestatus)

storecanlicensestatus

#### [storecontext](https://docs.microsoft.com/uwp/api/windows.services.store.storecontext)

storecontext.canacquirestorelicenseasync <br> storecontext.canacquirestorelicenseforoptionalpackageasync <br> storecontext.cansilentlydownloadstorepackageupdates <br> storecontext.downloadandinstallstorepackagesasync <br> storecontext.getassociatedstorequeueitemsasync <br> storecontext.getstoreproductsasync <br> storecontext.getstorequeueitemsasync <br> storecontext.requestdownloadandinstallstorepackagesasync <br> storecontext.requestuninstallstorepackageasync <br> storecontext.requestuninstallstorepackagebystoreidasync <br> storecontext.trysilentdownloadandinstallstorepackageupdatesasync <br> storecontext.trysilentdownloadstorepackageupdatesasync <br> storecontext.uninstallstorepackageasync <br> storecontext.uninstallstorepackagebystoreidasync

#### [storepackageinstalloptions](https://docs.microsoft.com/uwp/api/windows.services.store.storepackageinstalloptions)

storepackageinstalloptions <br> storepackageinstalloptions.allowforcedapprestart <br> storepackageinstalloptions.storepackageinstalloptions

#### [storepackageupdateresult](https://docs.microsoft.com/uwp/api/windows.services.store.storepackageupdateresult)

storepackageupdateresult.storequeueitems

#### [storeproductoptions](https://docs.microsoft.com/uwp/api/windows.services.store.storeproductoptions)

storeproductoptions <br> storeproductoptions.actionfilters <br> storeproductoptions.storeproductoptions

#### [storequeueitem](https://docs.microsoft.com/uwp/api/windows.services.store.storequeueitem)

storequeueitem <br> storequeueitem.completed <br> storequeueitem.getcurrentstatus <br> storequeueitem.installkind <br> storequeueitem.packagefamilyname <br> storequeueitem.productid <br> storequeueitem.statuschanged

#### [storequeueitemcompletedeventargs](https://docs.microsoft.com/uwp/api/windows.services.store.storequeueitemcompletedeventargs)

storequeueitemcompletedeventargs <br> storequeueitemcompletedeventargs.status

#### [storequeueitemextendedstate](https://docs.microsoft.com/uwp/api/windows.services.store.storequeueitemextendedstate)

storequeueitemextendedstate

#### [storequeueitemkind](https://docs.microsoft.com/uwp/api/windows.services.store.storequeueitemkind)

storequeueitemkind

#### [storequeueitemstate](https://docs.microsoft.com/uwp/api/windows.services.store.storequeueitemstate)

storequeueitemstate

#### [storequeueitemstatus](https://docs.microsoft.com/uwp/api/windows.services.store.storequeueitemstatus)

storequeueitemstatus <br> storequeueitemstatus.extendederror <br> storequeueitemstatus.packageinstallextendedstate <br> storequeueitemstatus.packageinstallstate <br> storequeueitemstatus.updatestatus

#### [storeuninstallstorepackageresult](https://docs.microsoft.com/uwp/api/windows.services.store.storeuninstallstorepackageresult)

storeuninstallstorepackageresult <br> storeuninstallstorepackageresult.extendederror <br> storeuninstallstorepackageresult.status

#### [storeuninstallstorepackagestatus](https://docs.microsoft.com/uwp/api/windows.services.store.storeuninstallstorepackagestatus)

storeuninstallstorepackagestatus

## windows.storage

### [windows.storage.fileproperties](https://docs.microsoft.com/uwp/api/windows.storage.fileproperties)

#### [windows](https://docs.microsoft.com/uwp/api/windows.storage.fileproperties.windows)

windows.storage.fileproperties

### [windows.storage.provider](https://docs.microsoft.com/uwp/api/windows.storage.provider)

#### [istorageproviderurisource](https://docs.microsoft.com/uwp/api/windows.storage.provider.istorageproviderurisource)

istorageproviderurisource.getcontentinfoforpath <br> istorageproviderurisource.getpathforcontenturi

#### [storageprovidergetcontentinfoforpathresult](https://docs.microsoft.com/uwp/api/windows.storage.provider.storageprovidergetcontentinfoforpathresult)

storageprovidergetcontentinfoforpathresult <br> storageprovidergetcontentinfoforpathresult.contentid <br> storageprovidergetcontentinfoforpathresult.contenturi <br> storageprovidergetcontentinfoforpathresult.status <br> storageprovidergetcontentinfoforpathresult.storageprovidergetcontentinfoforpathresult

#### [storageprovidergetpathforcontenturiresult](https://docs.microsoft.com/uwp/api/windows.storage.provider.storageprovidergetpathforcontenturiresult)

storageprovidergetpathforcontenturiresult <br> storageprovidergetpathforcontenturiresult.path <br> storageprovidergetpathforcontenturiresult.status <br> storageprovidergetpathforcontenturiresult.storageprovidergetpathforcontenturiresult

#### [storageproviderhardlinkpolicy](https://docs.microsoft.com/uwp/api/windows.storage.provider.storageproviderhardlinkpolicy)

storageproviderhardlinkpolicy

#### [storageproviderhydrationpolicy](https://docs.microsoft.com/uwp/api/windows.storage.provider.storageproviderhydrationpolicy)

storageproviderhydrationpolicy

#### [storageproviderhydrationpolicymodifier](https://docs.microsoft.com/uwp/api/windows.storage.provider.storageproviderhydrationpolicymodifier)

storageproviderhydrationpolicymodifier

#### [storageproviderinsyncpolicy](https://docs.microsoft.com/uwp/api/windows.storage.provider.storageproviderinsyncpolicy)

storageproviderinsyncpolicy

#### [storageproviderpopulationpolicy](https://docs.microsoft.com/uwp/api/windows.storage.provider.storageproviderpopulationpolicy)

storageproviderpopulationpolicy

#### [storageproviderprotectionmode](https://docs.microsoft.com/uwp/api/windows.storage.provider.storageproviderprotectionmode)

storageproviderprotectionmode

#### [storageprovidersyncrootinfo](https://docs.microsoft.com/uwp/api/windows.storage.provider.storageprovidersyncrootinfo)

storageprovidersyncrootinfo.hardlinkpolicy

#### [storageproviderurisourcestatus](https://docs.microsoft.com/uwp/api/windows.storage.provider.storageproviderurisourcestatus)

storageproviderurisourcestatus

### [windows.storage.search](https://docs.microsoft.com/uwp/api/windows.storage.search)

#### [storagelibrarychangetrackertriggerdetails](https://docs.microsoft.com/uwp/api/windows.storage.search.storagelibrarychangetrackertriggerdetails)

storagelibrarychangetrackertriggerdetails <br> storagelibrarychangetrackertriggerdetails.changetracker <br> storagelibrarychangetrackertriggerdetails.folder

### [windows.storage](https://docs.microsoft.com/uwp/api/windows.storage)

#### [storagefolder](https://docs.microsoft.com/uwp/api/windows.storage.storagefolder)

storagefolder.trygetchangetracker

## windows.system

### [windows.system.diagnostics.deviceportal](https://docs.microsoft.com/uwp/api/windows.system.diagnostics.deviceportal)

#### [deviceportalconnection](https://docs.microsoft.com/uwp/api/windows.system.diagnostics.deviceportal.deviceportalconnection)

deviceportalconnection.getservermessagewebsocketforrequest <br> deviceportalconnection.getservermessagewebsocketforrequest <br> deviceportalconnection.getservermessagewebsocketforrequest <br> deviceportalconnection.getserverstreamwebsocketforrequest <br> deviceportalconnection.getserverstreamwebsocketforrequest

#### [deviceportalconnectionrequestreceivedeventargs](https://docs.microsoft.com/uwp/api/windows.system.diagnostics.deviceportal.deviceportalconnectionrequestreceivedeventargs)

deviceportalconnectionrequestreceivedeventargs.getdeferral <br> deviceportalconnectionrequestreceivedeventargs.iswebsocketupgraderequest <br> deviceportalconnectionrequestreceivedeventargs.websocketprotocolsrequested

### [windows.system.diagnostics](https://docs.microsoft.com/uwp/api/windows.system.diagnostics)

#### [diagnosticinvoker](https://docs.microsoft.com/uwp/api/windows.system.diagnostics.diagnosticinvoker)

diagnosticinvoker.rundiagnosticactionfromstringasync

### [windows.system.inventory](https://docs.microsoft.com/uwp/api/windows.system.inventory)

#### [installeddesktopapp](https://docs.microsoft.com/uwp/api/windows.system.inventory.installeddesktopapp)

installeddesktopapp <br> installeddesktopapp.displayname <br> installeddesktopapp.displayversion <br> installeddesktopapp.getinventoryasync <br> installeddesktopapp.id <br> installeddesktopapp.publisher <br> installeddesktopapp.tostring

#### [windows](https://docs.microsoft.com/uwp/api/windows.system.inventory.windows)

windows.system.inventory

### [windows.system.profile](https://docs.microsoft.com/uwp/api/windows.system.profile)

#### [analyticsinfo](https://docs.microsoft.com/uwp/api/windows.system.profile.analyticsinfo)

analyticsinfo.getsystempropertiesasync

### [windows.system.remotesystems](https://docs.microsoft.com/uwp/api/windows.system.remotesystems)

#### [remotesystem](https://docs.microsoft.com/uwp/api/windows.system.remotesystems.remotesystem)

remotesystem.platform

#### [remotesystemenumerationcompletedeventargs](https://docs.microsoft.com/uwp/api/windows.system.remotesystems.remotesystemenumerationcompletedeventargs)

remotesystemenumerationcompletedeventargs

#### [remotesystemplatform](https://docs.microsoft.com/uwp/api/windows.system.remotesystems.remotesystemplatform)

remotesystemplatform

#### [remotesystemwatcher](https://docs.microsoft.com/uwp/api/windows.system.remotesystems.remotesystemwatcher)

remotesystemwatcher.enumerationcompleted

#### [remotesystemwatchererror](https://docs.microsoft.com/uwp/api/windows.system.remotesystems.remotesystemwatchererror)

remotesystemwatchererror

### [windows.system.userprofile](https://docs.microsoft.com/uwp/api/windows.system.userprofile)

#### [globalizationpreferences](https://docs.microsoft.com/uwp/api/windows.system.userprofile.globalizationpreferences)

globalizationpreferences.getforuser

#### [globalizationpreferencesforuser](https://docs.microsoft.com/uwp/api/windows.system.userprofile.globalizationpreferencesforuser)

globalizationpreferencesforuser <br> globalizationpreferencesforuser.calendars <br> globalizationpreferencesforuser.clocks <br> globalizationpreferencesforuser.currencies <br> globalizationpreferencesforuser.homegeographicregion <br> globalizationpreferencesforuser.languages <br> globalizationpreferencesforuser.user <br> globalizationpreferencesforuser.weekstartson

### [windows.system](https://docs.microsoft.com/uwp/api/windows.system)

#### [appactivationresult](https://docs.microsoft.com/uwp/api/windows.system.appactivationresult)

appactivationresult <br> appactivationresult.appresourcegroupinfo <br> appactivationresult.extendederror

#### [appdiagnosticinfo](https://docs.microsoft.com/uwp/api/windows.system.appdiagnosticinfo)

appdiagnosticinfo.launchasync

#### [appexecutionstatechangeresult](https://docs.microsoft.com/uwp/api/windows.system.appexecutionstatechangeresult)

appexecutionstatechangeresult <br> appexecutionstatechangeresult.extendederror

#### [appresourcegroupinfo](https://docs.microsoft.com/uwp/api/windows.system.appresourcegroupinfo)

appresourcegroupinfo.startresumeasync <br> appresourcegroupinfo.startsuspendasync <br> appresourcegroupinfo.startterminateasync

#### [autoupdatetimezonestatus](https://docs.microsoft.com/uwp/api/windows.system.autoupdatetimezonestatus)

autoupdatetimezonestatus

#### [timezonesettings](https://docs.microsoft.com/uwp/api/windows.system.timezonesettings)

timezonesettings.autoupdatetimezoneasync

## windows.ui

### [windows.ui.composition.core](https://docs.microsoft.com/uwp/api/windows.ui.composition.core)

#### [compositorcontroller](https://docs.microsoft.com/uwp/api/windows.ui.composition.core.compositorcontroller)

compositorcontroller <br> compositorcontroller.close <br> compositorcontroller.commit <br> compositorcontroller.commitneeded <br> compositorcontroller.compositor <br> compositorcontroller.compositorcontroller <br> compositorcontroller.ensurepreviouscommitcompletedasync

#### [windows](https://docs.microsoft.com/uwp/api/windows.ui.composition.core.windows)

windows.ui.composition.core

### [windows.ui.composition.desktop](https://docs.microsoft.com/uwp/api/windows.ui.composition.desktop)

#### [desktopwindowtarget](https://docs.microsoft.com/uwp/api/windows.ui.composition.desktop.desktopwindowtarget)

desktopwindowtarget <br> desktopwindowtarget.istopmost

#### [windows](https://docs.microsoft.com/uwp/api/windows.ui.composition.desktop.windows)

windows.ui.composition.desktop

### [windows.ui.composition.diagnostics](https://docs.microsoft.com/uwp/api/windows.ui.composition.diagnostics)

#### [compositiondebugheatmaps](https://docs.microsoft.com/uwp/api/windows.ui.composition.diagnostics.compositiondebugheatmaps)

compositiondebugheatmaps <br> compositiondebugheatmaps.showmemoryusage <br> compositiondebugheatmaps.showoverdraw

#### [compositiondebugoverdrawcontentkinds](https://docs.microsoft.com/uwp/api/windows.ui.composition.diagnostics.compositiondebugoverdrawcontentkinds)

compositiondebugoverdrawcontentkinds

#### [compositiondebugsettings](https://docs.microsoft.com/uwp/api/windows.ui.composition.diagnostics.compositiondebugsettings)

compositiondebugsettings <br> compositiondebugsettings.heatmaps <br> compositiondebugsettings.trygetsettings

#### [windows](https://docs.microsoft.com/uwp/api/windows.ui.composition.diagnostics.windows)

windows.ui.composition.diagnostics

### [windows.ui.composition](https://docs.microsoft.com/uwp/api/windows.ui.composition)

#### [animationcontroller](https://docs.microsoft.com/uwp/api/windows.ui.composition.animationcontroller)

animationcontroller <br> animationcontroller.maxplaybackrate <br> animationcontroller.minplaybackrate <br> animationcontroller.pause <br> animationcontroller.playbackrate <br> animationcontroller.progress <br> animationcontroller.progressbehavior <br> animationcontroller.resume

#### [animationcontrollerprogressbehavior](https://docs.microsoft.com/uwp/api/windows.ui.composition.animationcontrollerprogressbehavior)

animationcontrollerprogressbehavior

#### [bouncescalarnaturalmotionanimation](https://docs.microsoft.com/uwp/api/windows.ui.composition.bouncescalarnaturalmotionanimation)

bouncescalarnaturalmotionanimation <br> bouncescalarnaturalmotionanimation.acceleration <br> bouncescalarnaturalmotionanimation.restitution

#### [bouncevector2naturalmotionanimation](https://docs.microsoft.com/uwp/api/windows.ui.composition.bouncevector2naturalmotionanimation)

bouncevector2naturalmotionanimation <br> bouncevector2naturalmotionanimation.acceleration <br> bouncevector2naturalmotionanimation.restitution

#### [bouncevector3naturalmotionanimation](https://docs.microsoft.com/uwp/api/windows.ui.composition.bouncevector3naturalmotionanimation)

bouncevector3naturalmotionanimation <br> bouncevector3naturalmotionanimation.acceleration <br> bouncevector3naturalmotionanimation.restitution

#### [compositioncontainershape](https://docs.microsoft.com/uwp/api/windows.ui.composition.compositioncontainershape)

compositioncontainershape <br> compositioncontainershape.shapes

#### [compositionellipsegeometry](https://docs.microsoft.com/uwp/api/windows.ui.composition.compositionellipsegeometry)

compositionellipsegeometry <br> compositionellipsegeometry.center <br> compositionellipsegeometry.radius

#### [compositiongeometry](https://docs.microsoft.com/uwp/api/windows.ui.composition.compositiongeometry)

compositiongeometry <br> compositiongeometry.trimend <br> compositiongeometry.trimoffset <br> compositiongeometry.trimstart

#### [compositionlight](https://docs.microsoft.com/uwp/api/windows.ui.composition.compositionlight)

compositionlight.isenabled

#### [compositionlinegeometry](https://docs.microsoft.com/uwp/api/windows.ui.composition.compositionlinegeometry)

compositionlinegeometry <br> compositionlinegeometry.end <br> compositionlinegeometry.start

#### [compositionobject](https://docs.microsoft.com/uwp/api/windows.ui.composition.compositionobject)

compositionobject.trygetanimationcontroller

#### [compositionpath](https://docs.microsoft.com/uwp/api/windows.ui.composition.compositionpath)

compositionpath <br> compositionpath.compositionpath

#### [compositionpathgeometry](https://docs.microsoft.com/uwp/api/windows.ui.composition.compositionpathgeometry)

compositionpathgeometry <br> compositionpathgeometry.path

#### [compositionrectanglegeometry](https://docs.microsoft.com/uwp/api/windows.ui.composition.compositionrectanglegeometry)

compositionrectanglegeometry <br> compositionrectanglegeometry.offset <br> compositionrectanglegeometry.size

#### [compositionroundedrectanglegeometry](https://docs.microsoft.com/uwp/api/windows.ui.composition.compositionroundedrectanglegeometry)

compositionroundedrectanglegeometry <br> compositionroundedrectanglegeometry.cornerradius <br> compositionroundedrectanglegeometry.offset <br> compositionroundedrectanglegeometry.size

#### [compositionshape](https://docs.microsoft.com/uwp/api/windows.ui.composition.compositionshape)

compositionshape <br> compositionshape.centerpoint <br> compositionshape.offset <br> compositionshape.rotationangle <br> compositionshape.rotationangleindegrees <br> compositionshape.scale <br> compositionshape.transformmatrix

#### [compositionshapecollection](https://docs.microsoft.com/uwp/api/windows.ui.composition.compositionshapecollection)

compositionshapecollection <br> compositionshapecollection.append <br> compositionshapecollection.clear <br> compositionshapecollection.first <br> compositionshapecollection.getat <br> compositionshapecollection.getmany <br> compositionshapecollection.getview <br> compositionshapecollection.indexof <br> compositionshapecollection.insertat <br> compositionshapecollection.removeat <br> compositionshapecollection.removeatend <br> compositionshapecollection.replaceall <br> compositionshapecollection.setat <br> compositionshapecollection.size

#### [compositionspriteshape](https://docs.microsoft.com/uwp/api/windows.ui.composition.compositionspriteshape)

compositionspriteshape <br> compositionspriteshape.fillbrush <br> compositionspriteshape.geometry <br> compositionspriteshape.isstrokenonscaling <br> compositionspriteshape.strokebrush <br> compositionspriteshape.strokedasharray <br> compositionspriteshape.strokedashcap <br> compositionspriteshape.strokedashoffset <br> compositionspriteshape.strokeendcap <br> compositionspriteshape.strokelinejoin <br> compositionspriteshape.strokemiterlimit <br> compositionspriteshape.strokestartcap <br> compositionspriteshape.strokethickness

#### [compositionstrokecap](https://docs.microsoft.com/uwp/api/windows.ui.composition.compositionstrokecap)

compositionstrokecap

#### [compositionstrokedasharray](https://docs.microsoft.com/uwp/api/windows.ui.composition.compositionstrokedasharray)

compositionstrokedasharray <br> compositionstrokedasharray.append <br> compositionstrokedasharray.clear <br> compositionstrokedasharray.first <br> compositionstrokedasharray.getat <br> compositionstrokedasharray.getmany <br> compositionstrokedasharray.getview <br> compositionstrokedasharray.indexof <br> compositionstrokedasharray.insertat <br> compositionstrokedasharray.removeat <br> compositionstrokedasharray.removeatend <br> compositionstrokedasharray.replaceall <br> compositionstrokedasharray.setat <br> compositionstrokedasharray.size

#### [compositionstrokelinejoin](https://docs.microsoft.com/uwp/api/windows.ui.composition.compositionstrokelinejoin)

compositionstrokelinejoin

#### [compositionviewbox](https://docs.microsoft.com/uwp/api/windows.ui.composition.compositionviewbox)

compositionviewbox <br> compositionviewbox.horizontalalignmentratio <br> compositionviewbox.offset <br> compositionviewbox.size <br> compositionviewbox.stretch <br> compositionviewbox.verticalalignmentratio

#### [compositor](https://docs.microsoft.com/uwp/api/windows.ui.composition.compositor)

compositor.comment <br> compositor.createbouncescalaranimation <br> compositor.createbouncevector2animation <br> compositor.createbouncevector3animation <br> compositor.createcontainershape <br> compositor.createellipsegeometry <br> compositor.createlinegeometry <br> compositor.createpathgeometry <br> compositor.createpathgeometry <br> compositor.createpathkeyframeanimation <br> compositor.createrectanglegeometry <br> compositor.createroundedrectanglegeometry <br> compositor.createshapevisual <br> compositor.createspriteshape <br> compositor.createspriteshape <br> compositor.createviewbox <br> compositor.globalplaybackrate <br> compositor.maxglobalplaybackrate <br> compositor.minglobalplaybackrate <br> compositor.requestcommitasync

#### [pathkeyframeanimation](https://docs.microsoft.com/uwp/api/windows.ui.composition.pathkeyframeanimation)

pathkeyframeanimation <br> pathkeyframeanimation.insertkeyframe <br> pathkeyframeanimation.insertkeyframe

#### [pointlight](https://docs.microsoft.com/uwp/api/windows.ui.composition.pointlight)

pointlight.maxattenuationcutoff <br> pointlight.minattenuationcutoff

#### [shapevisual](https://docs.microsoft.com/uwp/api/windows.ui.composition.shapevisual)

shapevisual <br> shapevisual.shapes <br> shapevisual.viewbox

#### [spotlight](https://docs.microsoft.com/uwp/api/windows.ui.composition.spotlight)

spotlight.maxattenuationcutoff <br> spotlight.minattenuationcutoff

### [windows.ui.core](https://docs.microsoft.com/uwp/api/windows.ui.core)

#### [corecomponentinputsource](https://docs.microsoft.com/uwp/api/windows.ui.core.corecomponentinputsource)

corecomponentinputsource.dispatcherqueue

#### [coreindependentinputsource](https://docs.microsoft.com/uwp/api/windows.ui.core.coreindependentinputsource)

coreindependentinputsource.dispatcherqueue

#### [icorepointerinputsource2](https://docs.microsoft.com/uwp/api/windows.ui.core.icorepointerinputsource2)

icorepointerinputsource2 <br> icorepointerinputsource2.dispatcherqueue

### [windows.ui.input.core](https://docs.microsoft.com/uwp/api/windows.ui.input.core)

#### [radialcontrollerindependentinputsource](https://docs.microsoft.com/uwp/api/windows.ui.input.core.radialcontrollerindependentinputsource)

radialcontrollerindependentinputsource.dispatcherqueue

### [windows.ui.input.inking](https://docs.microsoft.com/uwp/api/windows.ui.input.inking)

#### [inkdrawingattributes](https://docs.microsoft.com/uwp/api/windows.ui.input.inking.inkdrawingattributes)

inkdrawingattributes.modelerattributes

#### [inkinputconfiguration](https://docs.microsoft.com/uwp/api/windows.ui.input.inking.inkinputconfiguration)

inkinputconfiguration <br> inkinputconfiguration.iseraserinputenabled <br> inkinputconfiguration.isprimarybarrelbuttoninputenabled

#### [inkmodelerattributes](https://docs.microsoft.com/uwp/api/windows.ui.input.inking.inkmodelerattributes)

inkmodelerattributes <br> inkmodelerattributes.predictiontime <br> inkmodelerattributes.scalingfactor

#### [inkpresenter](https://docs.microsoft.com/uwp/api/windows.ui.input.inking.inkpresenter)

inkpresenter.inputconfiguration

### [windows.ui.input.spatial](https://docs.microsoft.com/uwp/api/windows.ui.input.spatial)

#### [spatialinteractioncontroller](https://docs.microsoft.com/uwp/api/windows.ui.input.spatial.spatialinteractioncontroller)

spatialinteractioncontroller.trygetbatteryreport

### [windows.ui.notifications](https://docs.microsoft.com/uwp/api/windows.ui.notifications)

#### [scheduledtoastnotification](https://docs.microsoft.com/uwp/api/windows.ui.notifications.scheduledtoastnotification)

scheduledtoastnotification.expirationtime

### [windows.ui.text](https://docs.microsoft.com/uwp/api/windows.ui.text)

#### [contentlinkinfo](https://docs.microsoft.com/uwp/api/windows.ui.text.contentlinkinfo)

contentlinkinfo <br> contentlinkinfo.contentlinkinfo <br> contentlinkinfo.displaytext <br> contentlinkinfo.id <br> contentlinkinfo.linkcontentkind <br> contentlinkinfo.secondarytext <br> contentlinkinfo.uri

#### [richedittextrange](https://docs.microsoft.com/uwp/api/windows.ui.text.richedittextrange)

richedittextrange <br> richedittextrange.canpaste <br> richedittextrange.changecase <br> richedittextrange.character <br> richedittextrange.characterformat <br> richedittextrange.collapse <br> richedittextrange.contentlinkinfo <br> richedittextrange.copy <br> richedittextrange.cut <br> richedittextrange.delete <br> richedittextrange.endof <br> richedittextrange.endposition <br> richedittextrange.expand <br> richedittextrange.findtext <br> richedittextrange.formattedtext <br> richedittextrange.getcharacterutf32 <br> richedittextrange.getclone <br> richedittextrange.getindex <br> richedittextrange.getpoint <br> richedittextrange.getrect <br> richedittextrange.gettext <br> richedittextrange.gettextviastream <br> richedittextrange.gravity <br> richedittextrange.inrange <br> richedittextrange.insertimage <br> richedittextrange.instory <br> richedittextrange.isequal <br> richedittextrange.length <br> richedittextrange.link <br> richedittextrange.matchselection <br> richedittextrange.move <br> richedittextrange.moveend <br> richedittextrange.movestart <br> richedittextrange.paragraphformat <br> richedittextrange.paste <br> richedittextrange.scrollintoview <br> richedittextrange.setindex <br> richedittextrange.setpoint <br> richedittextrange.setrange <br> richedittextrange.settext <br> richedittextrange.settextviastream <br> richedittextrange.startof <br> richedittextrange.startposition <br> richedittextrange.storylength <br> richedittextrange.text

### [windows.ui.viewmanagement.core](https://docs.microsoft.com/uwp/api/windows.ui.viewmanagement.core)

#### [coreinputview](https://docs.microsoft.com/uwp/api/windows.ui.viewmanagement.core.coreinputview)

coreinputview.trytransferxyfocustoprimaryview <br> coreinputview.xyfocustransferredtoprimaryview <br> coreinputview.xyfocustransferringfromprimaryview

#### [coreinputviewtransferringxyfocuseventargs](https://docs.microsoft.com/uwp/api/windows.ui.viewmanagement.core.coreinputviewtransferringxyfocuseventargs)

coreinputviewtransferringxyfocuseventargs <br> coreinputviewtransferringxyfocuseventargs.direction <br> coreinputviewtransferringxyfocuseventargs.keepprimaryviewvisible <br> coreinputviewtransferringxyfocuseventargs.origin <br> coreinputviewtransferringxyfocuseventargs.transferhandled

#### [coreinputviewxyfocustransferdirection](https://docs.microsoft.com/uwp/api/windows.ui.viewmanagement.core.coreinputviewxyfocustransferdirection)

coreinputviewxyfocustransferdirection

### [windows.ui.webui](https://docs.microsoft.com/uwp/api/windows.ui.webui)

#### [webuistartuptaskactivatedeventargs](https://docs.microsoft.com/uwp/api/windows.ui.webui.webuistartuptaskactivatedeventargs)

webuistartuptaskactivatedeventargs.activatedoperation

### [windows.ui.xaml.automation.peers](https://docs.microsoft.com/uwp/api/windows.ui.xaml.automation.peers)

#### [automationheadinglevel](https://docs.microsoft.com/uwp/api/windows.ui.xaml.automation.peers.automationheadinglevel)

automationheadinglevel

#### [automationpeer](https://docs.microsoft.com/uwp/api/windows.ui.xaml.automation.peers.automationpeer)

automationpeer.getheadinglevel <br> automationpeer.getheadinglevelcore

#### [autosuggestboxautomationpeer](https://docs.microsoft.com/uwp/api/windows.ui.xaml.automation.peers.autosuggestboxautomationpeer)

autosuggestboxautomationpeer.invoke

#### [calendardatepickerautomationpeer](https://docs.microsoft.com/uwp/api/windows.ui.xaml.automation.peers.calendardatepickerautomationpeer)

calendardatepickerautomationpeer <br> calendardatepickerautomationpeer.calendardatepickerautomationpeer <br> calendardatepickerautomationpeer.invoke <br> calendardatepickerautomationpeer.isreadonly <br> calendardatepickerautomationpeer.setvalue <br> calendardatepickerautomationpeer.value

#### [treeviewitemautomationpeer](https://docs.microsoft.com/uwp/api/windows.ui.xaml.automation.peers.treeviewitemautomationpeer)

treeviewitemautomationpeer <br> treeviewitemautomationpeer.collapse <br> treeviewitemautomationpeer.expand <br> treeviewitemautomationpeer.expandcollapsestate <br> treeviewitemautomationpeer.treeviewitemautomationpeer

#### [treeviewlistautomationpeer](https://docs.microsoft.com/uwp/api/windows.ui.xaml.automation.peers.treeviewlistautomationpeer)

treeviewlistautomationpeer <br> treeviewlistautomationpeer.treeviewlistautomationpeer

### [windows.ui.xaml.automation](https://docs.microsoft.com/uwp/api/windows.ui.xaml.automation)

#### [automationelementidentifiers](https://docs.microsoft.com/uwp/api/windows.ui.xaml.automation.automationelementidentifiers)

automationelementidentifiers.headinglevelproperty

#### [automationproperties](https://docs.microsoft.com/uwp/api/windows.ui.xaml.automation.automationproperties)

automationproperties.getheadinglevel <br> automationproperties.headinglevelproperty <br> automationproperties.setheadinglevel

### [windows.ui.xaml.controls.maps](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.maps)

#### [mapcontrol](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.maps.mapcontrol)

mapcontrol.region <br> mapcontrol.regionproperty

#### [mapelement](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.maps.mapelement)

mapelement.isenabled <br> mapelement.isenabledproperty

### [windows.ui.xaml.controls.primitives](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.primitives)

#### [appbarbuttontemplatesettings](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.primitives.appbarbuttontemplatesettings)

appbarbuttontemplatesettings <br> appbarbuttontemplatesettings.keyboardacceleratortextminwidth

#### [appbartogglebuttontemplatesettings](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.primitives.appbartogglebuttontemplatesettings)

appbartogglebuttontemplatesettings <br> appbartogglebuttontemplatesettings.keyboardacceleratortextminwidth

#### [menuflyoutitemtemplatesettings](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.primitives.menuflyoutitemtemplatesettings)

menuflyoutitemtemplatesettings <br> menuflyoutitemtemplatesettings.keyboardacceleratortextminwidth

### [windows.ui.xaml.controls](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls)

#### [appbarbutton](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.appbarbutton)

appbarbutton.keyboardacceleratortextoverride <br> appbarbutton.keyboardacceleratortextoverrideproperty <br> appbarbutton.templatesettings

#### [appbartogglebutton](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.appbartogglebutton)

appbartogglebutton.keyboardacceleratortextoverride <br> appbartogglebutton.keyboardacceleratortextoverrideproperty <br> appbartogglebutton.templatesettings

#### [contentlinkchangedeventargs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.contentlinkchangedeventargs)

contentlinkchangedeventargs <br> contentlinkchangedeventargs.changekind <br> contentlinkchangedeventargs.contentlinkinfo <br> contentlinkchangedeventargs.textrange

#### [contentlinkchangekind](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.contentlinkchangekind)

contentlinkchangekind

#### [handwritingpanelclosedeventargs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.handwritingpanelclosedeventargs)

handwritingpanelclosedeventargs

#### [handwritingpanelopenedeventargs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.handwritingpanelopenedeventargs)

handwritingpanelopenedeventargs

#### [handwritingpanelplacementalignment](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.handwritingpanelplacementalignment)

handwritingpanelplacementalignment

#### [handwritingview](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.handwritingview)

handwritingview <br> handwritingview.arecandidatesenabled <br> handwritingview.arecandidatesenabledproperty <br> handwritingview.closed <br> handwritingview.handwritingview <br> handwritingview.isopen <br> handwritingview.isopenproperty <br> handwritingview.opened <br> handwritingview.placementalignment <br> handwritingview.placementalignmentproperty <br> handwritingview.placementtarget <br> handwritingview.placementtargetproperty <br> handwritingview.tryclose <br> handwritingview.tryopen

#### [mediatransportcontrols](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.mediatransportcontrols)

mediatransportcontrols.iscompactoverlaybuttonvisible <br> mediatransportcontrols.iscompactoverlaybuttonvisibleproperty <br> mediatransportcontrols.iscompactoverlayenabled <br> mediatransportcontrols.iscompactoverlayenabledproperty

#### [menuflyoutitem](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.menuflyoutitem)

menuflyoutitem.keyboardacceleratortextoverride <br> menuflyoutitem.keyboardacceleratortextoverrideproperty <br> menuflyoutitem.templatesettings

#### [navigationview](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.navigationview)

navigationview.backrequested <br> navigationview.isbackbuttonvisible <br> navigationview.isbackbuttonvisibleproperty <br> navigationview.isbackenabled <br> navigationview.isbackenabledproperty <br> navigationview.paneclosed <br> navigationview.paneclosing <br> navigationview.paneopened <br> navigationview.paneopening <br> navigationview.panetitle <br> navigationview.panetitleproperty

#### [navigationviewbackbuttonvisible](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.navigationviewbackbuttonvisible)

navigationviewbackbuttonvisible

#### [navigationviewbackrequestedeventargs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.navigationviewbackrequestedeventargs)

navigationviewbackrequestedeventargs

#### [navigationviewpaneclosingeventargs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.navigationviewpaneclosingeventargs)

navigationviewpaneclosingeventargs <br> navigationviewpaneclosingeventargs.cancel

#### [refreshcontainer](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.refreshcontainer)

refreshcontainer <br> refreshcontainer.pulldirection <br> refreshcontainer.pulldirectionproperty <br> refreshcontainer.refreshcontainer <br> refreshcontainer.refreshrequested <br> refreshcontainer.requestrefresh <br> refreshcontainer.visualizer <br> refreshcontainer.visualizerproperty

#### [refreshinteractionratiochangedeventargs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.refreshinteractionratiochangedeventargs)

refreshinteractionratiochangedeventargs <br> refreshinteractionratiochangedeventargs.interactionratio

#### [refreshpulldirection](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.refreshpulldirection)

refreshpulldirection

#### [refreshrequestedeventargs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.refreshrequestedeventargs)

refreshrequestedeventargs <br> refreshrequestedeventargs.getdeferral

#### [refreshstatechangedeventargs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.refreshstatechangedeventargs)

refreshstatechangedeventargs <br> refreshstatechangedeventargs.newstate <br> refreshstatechangedeventargs.oldstate

#### [refreshvisualizer](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.refreshvisualizer)

refreshvisualizer <br> refreshvisualizer.content <br> refreshvisualizer.contentproperty <br> refreshvisualizer.infoproviderproperty <br> refreshvisualizer.orientation <br> refreshvisualizer.orientationproperty <br> refreshvisualizer.refreshrequested <br> refreshvisualizer.refreshstatechanged <br> refreshvisualizer.refreshvisualizer <br> refreshvisualizer.requestrefresh <br> refreshvisualizer.state <br> refreshvisualizer.stateproperty

#### [refreshvisualizerorientation](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.refreshvisualizerorientation)

refreshvisualizerorientation

#### [refreshvisualizerstate](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.refreshvisualizerstate)

refreshvisualizerstate

#### [richeditbox](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.richeditbox)

richeditbox.contentlinkbackgroundcolor <br> richeditbox.contentlinkbackgroundcolorproperty <br> richeditbox.contentlinkchanged <br> richeditbox.contentlinkforegroundcolor <br> richeditbox.contentlinkforegroundcolorproperty <br> richeditbox.contentlinkinvoked <br> richeditbox.contentlinkproviders <br> richeditbox.contentlinkprovidersproperty <br> richeditbox.handwritingview <br> richeditbox.handwritingviewproperty <br> richeditbox.ishandwritingviewenabled <br> richeditbox.ishandwritingviewenabledproperty

#### [textbox](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.textbox)

textbox.handwritingview <br> textbox.handwritingviewproperty <br> textbox.ishandwritingviewenabled <br> textbox.ishandwritingviewenabledproperty

#### [treeview](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.treeview)

treeview <br> treeview.collapse <br> treeview.collapsed <br> treeview.expand <br> treeview.expanding <br> treeview.iteminvoked <br> treeview.rootnodes <br> treeview.selectall <br> treeview.selectednodes <br> treeview.selectionmode <br> treeview.selectionmodeproperty <br> treeview.treeview

#### [treeviewcollapsedeventargs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.treeviewcollapsedeventargs)

treeviewcollapsedeventargs <br> treeviewcollapsedeventargs.node

#### [treeviewexpandingeventargs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.treeviewexpandingeventargs)

treeviewexpandingeventargs <br> treeviewexpandingeventargs.node

#### [treeviewitem](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.treeviewitem)

treeviewitem <br> treeviewitem.collapsedglyph <br> treeviewitem.collapsedglyphproperty <br> treeviewitem.expandedglyph <br> treeviewitem.expandedglyphproperty <br> treeviewitem.glyphbrush <br> treeviewitem.glyphbrushproperty <br> treeviewitem.glyphopacity <br> treeviewitem.glyphopacityproperty <br> treeviewitem.glyphsize <br> treeviewitem.glyphsizeproperty <br> treeviewitem.isexpanded <br> treeviewitem.isexpandedproperty <br> treeviewitem.treeviewitem <br> treeviewitem.treeviewitemtemplatesettings <br> treeviewitem.treeviewitemtemplatesettingsproperty

#### [treeviewiteminvokedeventargs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.treeviewiteminvokedeventargs)

treeviewiteminvokedeventargs <br> treeviewiteminvokedeventargs.handled <br> treeviewiteminvokedeventargs.invokeditem

#### [treeviewitemtemplatesettings](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.treeviewitemtemplatesettings)

treeviewitemtemplatesettings <br> treeviewitemtemplatesettings.collapsedglyphvisibility <br> treeviewitemtemplatesettings.collapsedglyphvisibilityproperty <br> treeviewitemtemplatesettings.dragitemscount <br> treeviewitemtemplatesettings.dragitemscountproperty <br> treeviewitemtemplatesettings.expandedglyphvisibility <br> treeviewitemtemplatesettings.expandedglyphvisibilityproperty <br> treeviewitemtemplatesettings.indentation <br> treeviewitemtemplatesettings.indentationproperty <br> treeviewitemtemplatesettings.treeviewitemtemplatesettings

#### [treeviewlist](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.treeviewlist)

treeviewlist <br> treeviewlist.treeviewlist

#### [treeviewnode](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.treeviewnode)

treeviewnode <br> treeviewnode.children <br> treeviewnode.content <br> treeviewnode.contentproperty <br> treeviewnode.depth <br> treeviewnode.depthproperty <br> treeviewnode.haschildren <br> treeviewnode.haschildrenproperty <br> treeviewnode.hasunrealizedchildren <br> treeviewnode.isexpanded <br> treeviewnode.isexpandedproperty <br> treeviewnode.parent <br> treeviewnode.treeviewnode

#### [treeviewselectionmode](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.treeviewselectionmode)

treeviewselectionmode

#### [webview](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.webview)

webview.separateprocesslost

#### [webviewseparateprocesslosteventargs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.webviewseparateprocesslosteventargs)

webviewseparateprocesslosteventargs

### [windows.ui.xaml.documents](https://docs.microsoft.com/uwp/api/windows.ui.xaml.documents)

#### [contactcontentlinkprovider](https://docs.microsoft.com/uwp/api/windows.ui.xaml.documents.contactcontentlinkprovider)

contactcontentlinkprovider <br> contactcontentlinkprovider.contactcontentlinkprovider

#### [contentlink](https://docs.microsoft.com/uwp/api/windows.ui.xaml.documents.contentlink)

contentlink <br> contentlink.background <br> contentlink.backgroundproperty <br> contentlink.contentlink <br> contentlink.cursor <br> contentlink.cursorproperty <br> contentlink.elementsoundmode <br> contentlink.elementsoundmodeproperty <br> contentlink.focus <br> contentlink.focusstate <br> contentlink.focusstateproperty <br> contentlink.gotfocus <br> contentlink.info <br> contentlink.invoked <br> contentlink.istabstop <br> contentlink.istabstopproperty <br> contentlink.lostfocus <br> contentlink.tabindex <br> contentlink.tabindexproperty <br> contentlink.xyfocusdown <br> contentlink.xyfocusdownnavigationstrategy <br> contentlink.xyfocusdownnavigationstrategyproperty <br> contentlink.xyfocusdownproperty <br> contentlink.xyfocusleft <br> contentlink.xyfocusleftnavigationstrategy <br> contentlink.xyfocusleftnavigationstrategyproperty <br> contentlink.xyfocusleftproperty <br> contentlink.xyfocusright <br> contentlink.xyfocusrightnavigationstrategy <br> contentlink.xyfocusrightnavigationstrategyproperty <br> contentlink.xyfocusrightproperty <br> contentlink.xyfocusup <br> contentlink.xyfocusupnavigationstrategy <br> contentlink.xyfocusupnavigationstrategyproperty <br> contentlink.xyfocusupproperty

#### [contentlinkinvokedeventargs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.documents.contentlinkinvokedeventargs)

contentlinkinvokedeventargs <br> contentlinkinvokedeventargs.contentlinkinfo <br> contentlinkinvokedeventargs.handled

#### [contentlinkprovider](https://docs.microsoft.com/uwp/api/windows.ui.xaml.documents.contentlinkprovider)

contentlinkprovider <br> contentlinkprovider.contentlinkprovider

#### [contentlinkprovidercollection](https://docs.microsoft.com/uwp/api/windows.ui.xaml.documents.contentlinkprovidercollection)

contentlinkprovidercollection <br> contentlinkprovidercollection.append <br> contentlinkprovidercollection.clear <br> contentlinkprovidercollection.contentlinkprovidercollection <br> contentlinkprovidercollection.first <br> contentlinkprovidercollection.getat <br> contentlinkprovidercollection.getmany <br> contentlinkprovidercollection.getview <br> contentlinkprovidercollection.indexof <br> contentlinkprovidercollection.insertat <br> contentlinkprovidercollection.removeat <br> contentlinkprovidercollection.removeatend <br> contentlinkprovidercollection.replaceall <br> contentlinkprovidercollection.setat <br> contentlinkprovidercollection.size

#### [placecontentlinkprovider](https://docs.microsoft.com/uwp/api/windows.ui.xaml.documents.placecontentlinkprovider)

placecontentlinkprovider <br> placecontentlinkprovider.placecontentlinkprovider

### [windows.ui.xaml.input](https://docs.microsoft.com/uwp/api/windows.ui.xaml.input)

#### [focusmanager](https://docs.microsoft.com/uwp/api/windows.ui.xaml.input.focusmanager)

focusmanager.tryfocusasync <br> focusmanager.trymovefocusasync <br> focusmanager.trymovefocusasync

#### [focusmovementresult](https://docs.microsoft.com/uwp/api/windows.ui.xaml.input.focusmovementresult)

focusmovementresult <br> focusmovementresult.succeeded

#### [gettingfocuseventargs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.input.gettingfocuseventargs)

gettingfocuseventargs.trycancel <br> gettingfocuseventargs.trysetnewfocusedelement

#### [keyboardacceleratorinvokedeventargs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.input.keyboardacceleratorinvokedeventargs)

keyboardacceleratorinvokedeventargs.keyboardaccelerator

#### [keyboardacceleratorplacementmode](https://docs.microsoft.com/uwp/api/windows.ui.xaml.input.keyboardacceleratorplacementmode)

keyboardacceleratorplacementmode

#### [losingfocuseventargs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.input.losingfocuseventargs)

losingfocuseventargs.trycancel <br> losingfocuseventargs.trysetnewfocusedelement

### [windows.ui.xaml.media](https://docs.microsoft.com/uwp/api/windows.ui.xaml.media)

#### [compositiontarget](https://docs.microsoft.com/uwp/api/windows.ui.xaml.media.compositiontarget)

compositiontarget.rendered

#### [renderedeventargs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.media.renderedeventargs)

renderedeventargs <br> renderedeventargs.frameduration

### [windows.ui.xaml](https://docs.microsoft.com/uwp/api/windows.ui.xaml)

#### [bringintoviewoptions](https://docs.microsoft.com/uwp/api/windows.ui.xaml.bringintoviewoptions)

bringintoviewoptions.horizontalalignmentratio <br> bringintoviewoptions.horizontaloffset <br> bringintoviewoptions.verticalalignmentratio <br> bringintoviewoptions.verticaloffset

#### [bringintoviewrequestedeventargs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.bringintoviewrequestedeventargs)

bringintoviewrequestedeventargs <br> bringintoviewrequestedeventargs.animationdesired <br> bringintoviewrequestedeventargs.handled <br> bringintoviewrequestedeventargs.horizontalalignmentratio <br> bringintoviewrequestedeventargs.horizontaloffset <br> bringintoviewrequestedeventargs.targetelement <br> bringintoviewrequestedeventargs.targetrect <br> bringintoviewrequestedeventargs.verticalalignmentratio <br> bringintoviewrequestedeventargs.verticaloffset

#### [elementsoundplayer](https://docs.microsoft.com/uwp/api/windows.ui.xaml.elementsoundplayer)

elementsoundplayer.spatialaudiomode

#### [elementspatialaudiomode](https://docs.microsoft.com/uwp/api/windows.ui.xaml.elementspatialaudiomode)

elementspatialaudiomode

#### [uielement](https://docs.microsoft.com/uwp/api/windows.ui.xaml.uielement)

uielement.bringintoviewrequested <br> uielement.bringintoviewrequestedevent <br> uielement.contextrequestedevent <br> uielement.keyboardacceleratorplacementmode <br> uielement.keyboardacceleratorplacementmodeproperty <br> uielement.keyboardacceleratorplacementtarget <br> uielement.keyboardacceleratorplacementtargetproperty <br> uielement.keytiptarget <br> uielement.keytiptargetproperty <br> uielement.onbringintoviewrequested <br> uielement.onkeyboardacceleratorinvoked <br> uielement.registerasscrollport

## windows.web

### [windows.web.ui.interop](https://docs.microsoft.com/uwp/api/windows.web.ui.interop)

#### [webviewcontrol](https://docs.microsoft.com/uwp/api/windows.web.ui.interop.webviewcontrol)

webviewcontrol <br> webviewcontrol.acceleratorkeypressed <br> webviewcontrol.bounds <br> webviewcontrol.buildlocalstreamuri <br> webviewcontrol.cangoback <br> webviewcontrol.cangoforward <br> webviewcontrol.capturepreviewtostreamasync <br> webviewcontrol.captureselectedcontenttodatapackageasync <br> webviewcontrol.close <br> webviewcontrol.containsfullscreenelement <br> webviewcontrol.containsfullscreenelementchanged <br> webviewcontrol.contentloading <br> webviewcontrol.defaultbackgroundcolor <br> webviewcontrol.deferredpermissionrequests <br> webviewcontrol.documenttitle <br> webviewcontrol.domcontentloaded <br> webviewcontrol.framecontentloading <br> webviewcontrol.framedomcontentloaded <br> webviewcontrol.framenavigationcompleted <br> webviewcontrol.framenavigationstarting <br> webviewcontrol.getdeferredpermissionrequestbyid <br> webviewcontrol.goback <br> webviewcontrol.goforward <br> webviewcontrol.invokescriptasync <br> webviewcontrol.isvisible <br> webviewcontrol.longrunningscriptdetected <br> webviewcontrol.movefocus <br> webviewcontrol.movefocusrequested <br> webviewcontrol.navigate <br> webviewcontrol.navigatetolocalstreamuri <br> webviewcontrol.navigatetostring <br> webviewcontrol.navigatewithhttprequestmessage <br> webviewcontrol.navigationcompleted <br> webviewcontrol.navigationstarting <br> webviewcontrol.newwindowrequested <br> webviewcontrol.permissionrequested <br> webviewcontrol.process <br> webviewcontrol.refresh <br> webviewcontrol.scale <br> webviewcontrol.scriptnotify <br> webviewcontrol.settings <br> webviewcontrol.source <br> webviewcontrol.stop <br> webviewcontrol.unsafecontentwarningdisplaying <br> webviewcontrol.unsupportedurischemeidentified <br> webviewcontrol.unviewablecontentidentified <br> webviewcontrol.webresourcerequested

#### [webviewcontrolacceleratorkeypressedeventargs](https://docs.microsoft.com/uwp/api/windows.web.ui.interop.webviewcontrolacceleratorkeypressedeventargs)

webviewcontrolacceleratorkeypressedeventargs <br> webviewcontrolacceleratorkeypressedeventargs.eventtype <br> webviewcontrolacceleratorkeypressedeventargs.keystatus <br> webviewcontrolacceleratorkeypressedeventargs.routingstage <br> webviewcontrolacceleratorkeypressedeventargs.virtualkey

#### [webviewcontrolacceleratorkeyroutingstage](https://docs.microsoft.com/uwp/api/windows.web.ui.interop.webviewcontrolacceleratorkeyroutingstage)

webviewcontrolacceleratorkeyroutingstage

#### [webviewcontrolmovefocusreason](https://docs.microsoft.com/uwp/api/windows.web.ui.interop.webviewcontrolmovefocusreason)

webviewcontrolmovefocusreason

#### [webviewcontrolmovefocusrequestedeventargs](https://docs.microsoft.com/uwp/api/windows.web.ui.interop.webviewcontrolmovefocusrequestedeventargs)

webviewcontrolmovefocusrequestedeventargs <br> webviewcontrolmovefocusrequestedeventargs.reason

#### [webviewcontrolprocess](https://docs.microsoft.com/uwp/api/windows.web.ui.interop.webviewcontrolprocess)

webviewcontrolprocess <br> webviewcontrolprocess.createwebviewcontrolasync <br> webviewcontrolprocess.enterpriseid <br> webviewcontrolprocess.getwebviewcontrols <br> webviewcontrolprocess.isprivatenetworkclientservercapabilityenabled <br> webviewcontrolprocess.processexited <br> webviewcontrolprocess.processid <br> webviewcontrolprocess.terminate <br> webviewcontrolprocess.webviewcontrolprocess <br> webviewcontrolprocess.webviewcontrolprocess

#### [webviewcontrolprocesscapabilitystate](https://docs.microsoft.com/uwp/api/windows.web.ui.interop.webviewcontrolprocesscapabilitystate)

webviewcontrolprocesscapabilitystate

#### [webviewcontrolprocessoptions](https://docs.microsoft.com/uwp/api/windows.web.ui.interop.webviewcontrolprocessoptions)

webviewcontrolprocessoptions <br> webviewcontrolprocessoptions.enterpriseid <br> webviewcontrolprocessoptions.privatenetworkclientservercapability <br> webviewcontrolprocessoptions.webviewcontrolprocessoptions

#### [windows](https://docs.microsoft.com/uwp/api/windows.web.ui.interop.windows)

windows.web.ui.interop

### [windows.web.ui](https://docs.microsoft.com/uwp/api/windows.web.ui)

#### [iwebviewcontrol](https://docs.microsoft.com/uwp/api/windows.web.ui.iwebviewcontrol)

iwebviewcontrol <br> iwebviewcontrol.buildlocalstreamuri <br> iwebviewcontrol.cangoback <br> iwebviewcontrol.cangoforward <br> iwebviewcontrol.capturepreviewtostreamasync <br> iwebviewcontrol.captureselectedcontenttodatapackageasync <br> iwebviewcontrol.containsfullscreenelement <br> iwebviewcontrol.containsfullscreenelementchanged <br> iwebviewcontrol.contentloading <br> iwebviewcontrol.defaultbackgroundcolor <br> iwebviewcontrol.deferredpermissionrequests <br> iwebviewcontrol.documenttitle <br> iwebviewcontrol.domcontentloaded <br> iwebviewcontrol.framecontentloading <br> iwebviewcontrol.framedomcontentloaded <br> iwebviewcontrol.framenavigationcompleted <br> iwebviewcontrol.framenavigationstarting <br> iwebviewcontrol.getdeferredpermissionrequestbyid <br> iwebviewcontrol.goback <br> iwebviewcontrol.goforward <br> iwebviewcontrol.invokescriptasync <br> iwebviewcontrol.longrunningscriptdetected <br> iwebviewcontrol.navigate <br> iwebviewcontrol.navigatetolocalstreamuri <br> iwebviewcontrol.navigatetostring <br> iwebviewcontrol.navigatewithhttprequestmessage <br> iwebviewcontrol.navigationcompleted <br> iwebviewcontrol.navigationstarting <br> iwebviewcontrol.newwindowrequested <br> iwebviewcontrol.permissionrequested <br> iwebviewcontrol.refresh <br> iwebviewcontrol.scriptnotify <br> iwebviewcontrol.settings <br> iwebviewcontrol.source <br> iwebviewcontrol.stop <br> iwebviewcontrol.unsafecontentwarningdisplaying <br> iwebviewcontrol.unsupportedurischemeidentified <br> iwebviewcontrol.unviewablecontentidentified <br> iwebviewcontrol.webresourcerequested

#### [webviewcontrolcontentloadingeventargs](https://docs.microsoft.com/uwp/api/windows.web.ui.webviewcontrolcontentloadingeventargs)

webviewcontrolcontentloadingeventargs <br> webviewcontrolcontentloadingeventargs.uri

#### [webviewcontroldeferredpermissionrequest](https://docs.microsoft.com/uwp/api/windows.web.ui.webviewcontroldeferredpermissionrequest)

webviewcontroldeferredpermissionrequest <br> webviewcontroldeferredpermissionrequest.allow <br> webviewcontroldeferredpermissionrequest.deny <br> webviewcontroldeferredpermissionrequest.id <br> webviewcontroldeferredpermissionrequest.permissiontype <br> webviewcontroldeferredpermissionrequest.uri

#### [webviewcontroldomcontentloadedeventargs](https://docs.microsoft.com/uwp/api/windows.web.ui.webviewcontroldomcontentloadedeventargs)

webviewcontroldomcontentloadedeventargs <br> webviewcontroldomcontentloadedeventargs.uri

#### [webviewcontrollongrunningscriptdetectedeventargs](https://docs.microsoft.com/uwp/api/windows.web.ui.webviewcontrollongrunningscriptdetectedeventargs)

webviewcontrollongrunningscriptdetectedeventargs <br> webviewcontrollongrunningscriptdetectedeventargs.executiontime <br> webviewcontrollongrunningscriptdetectedeventargs.stoppagescriptexecution

#### [webviewcontrolnavigationcompletedeventargs](https://docs.microsoft.com/uwp/api/windows.web.ui.webviewcontrolnavigationcompletedeventargs)

webviewcontrolnavigationcompletedeventargs <br> webviewcontrolnavigationcompletedeventargs.issuccess <br> webviewcontrolnavigationcompletedeventargs.uri <br> webviewcontrolnavigationcompletedeventargs.weberrorstatus

#### [webviewcontrolnavigationstartingeventargs](https://docs.microsoft.com/uwp/api/windows.web.ui.webviewcontrolnavigationstartingeventargs)

webviewcontrolnavigationstartingeventargs <br> webviewcontrolnavigationstartingeventargs.cancel <br> webviewcontrolnavigationstartingeventargs.uri

#### [webviewcontrolnewwindowrequestedeventargs](https://docs.microsoft.com/uwp/api/windows.web.ui.webviewcontrolnewwindowrequestedeventargs)

webviewcontrolnewwindowrequestedeventargs <br> webviewcontrolnewwindowrequestedeventargs.handled <br> webviewcontrolnewwindowrequestedeventargs.referrer <br> webviewcontrolnewwindowrequestedeventargs.uri

#### [webviewcontrolpermissionrequest](https://docs.microsoft.com/uwp/api/windows.web.ui.webviewcontrolpermissionrequest)

webviewcontrolpermissionrequest <br> webviewcontrolpermissionrequest.allow <br> webviewcontrolpermissionrequest.defer <br> webviewcontrolpermissionrequest.deny <br> webviewcontrolpermissionrequest.id <br> webviewcontrolpermissionrequest.permissiontype <br> webviewcontrolpermissionrequest.state <br> webviewcontrolpermissionrequest.uri

#### [webviewcontrolpermissionrequestedeventargs](https://docs.microsoft.com/uwp/api/windows.web.ui.webviewcontrolpermissionrequestedeventargs)

webviewcontrolpermissionrequestedeventargs <br> webviewcontrolpermissionrequestedeventargs.permissionrequest

#### [webviewcontrolpermissionstate](https://docs.microsoft.com/uwp/api/windows.web.ui.webviewcontrolpermissionstate)

webviewcontrolpermissionstate

#### [webviewcontrolpermissiontype](https://docs.microsoft.com/uwp/api/windows.web.ui.webviewcontrolpermissiontype)

webviewcontrolpermissiontype

#### [webviewcontrolscriptnotifyeventargs](https://docs.microsoft.com/uwp/api/windows.web.ui.webviewcontrolscriptnotifyeventargs)

webviewcontrolscriptnotifyeventargs <br> webviewcontrolscriptnotifyeventargs.uri <br> webviewcontrolscriptnotifyeventargs.value

#### [webviewcontrolsettings](https://docs.microsoft.com/uwp/api/windows.web.ui.webviewcontrolsettings)

webviewcontrolsettings <br> webviewcontrolsettings.isindexeddbenabled <br> webviewcontrolsettings.isjavascriptenabled <br> webviewcontrolsettings.isscriptnotifyallowed

#### [webviewcontrolunsupportedurischemeidentifiedeventargs](https://docs.microsoft.com/uwp/api/windows.web.ui.webviewcontrolunsupportedurischemeidentifiedeventargs)

webviewcontrolunsupportedurischemeidentifiedeventargs <br> webviewcontrolunsupportedurischemeidentifiedeventargs.handled <br> webviewcontrolunsupportedurischemeidentifiedeventargs.uri

#### [webviewcontrolunviewablecontentidentifiedeventargs](https://docs.microsoft.com/uwp/api/windows.web.ui.webviewcontrolunviewablecontentidentifiedeventargs)

webviewcontrolunviewablecontentidentifiedeventargs <br> webviewcontrolunviewablecontentidentifiedeventargs.mediatype <br> webviewcontrolunviewablecontentidentifiedeventargs.referrer <br> webviewcontrolunviewablecontentidentifiedeventargs.uri

#### [webviewcontrolwebresourcerequestedeventargs](https://docs.microsoft.com/uwp/api/windows.web.ui.webviewcontrolwebresourcerequestedeventargs)

webviewcontrolwebresourcerequestedeventargs <br> webviewcontrolwebresourcerequestedeventargs.getdeferral <br> webviewcontrolwebresourcerequestedeventargs.request <br> webviewcontrolwebresourcerequestedeventargs.response

#### [windows](https://docs.microsoft.com/uwp/api/windows.web.ui.windows)

windows.web.ui

