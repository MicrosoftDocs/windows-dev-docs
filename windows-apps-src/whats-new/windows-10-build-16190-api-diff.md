---
author: QuinnRadich
title: Windows 10 build 16190 API changes
description: Developers can use the following list to identify new or changed namespaces in Windows 10 SDK Preview Build 16190
keywords: what's new, whats new, update, flighted, flights, API, 15021
ms.author: quradic
ms.date: 5/11/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
ms.assetid: 6932c7cb-300b-4558-a346-dd6d18a969f6
---


# New APIs in the Windows 10 SDK Preview Build 16190

New and updated API namespaces have been made available to [Windows Insiders](https://insider.windows.com/) in the Windows 10 SDK Preview Build 16190. This release accompanies the [Microsoft Build 2017 developer conference](https://developer.microsoft.com/windows/projects/events/build/2017?ocid=wdgbld17_intreferral_devcenterhp_null_null_devcenter_hppost&utm_campaign=wdgbld17&utm_medium=internalreferral&utm_source=devcenterhp&utm_content=devcenter_hppost).

Below is a full list of prelease documentation published for namespaces added or modified since the last public Windows 10 release, [Version 1703](windows-10-version-1703-api-diff.md). Please note that prerelease documentation may be incomplete and subject to change.

## windows.applicationmodel

### [windows.applicationmodel.activation](https://docs.microsoft.com/uwp/api/windows.applicationmodel.activation)

#### [activationkind](https://docs.microsoft.com/uwp/api/windows.applicationmodel.activation.activationkind)

activationkind

#### [consoleactivatedeventargs](https://docs.microsoft.com/uwp/api/windows.applicationmodel.activation.consoleactivatedeventargs)

consoleactivatedeventargs <br> consoleactivatedeventargs.arguments <br> consoleactivatedeventargs.kind <br> consoleactivatedeventargs.previousexecutionstate <br> consoleactivatedeventargs.splashscreen

#### [iconsoleactivatedeventargs](https://docs.microsoft.com/uwp/api/windows.applicationmodel.activation.iconsoleactivatedeventargs)

iconsoleactivatedeventargs <br> iconsoleactivatedeventargs.arguments

### [windows.applicationmodel.cards](https://docs.microsoft.com/uwp/api/windows.applicationmodel.cards)

#### [cardbuilder](https://docs.microsoft.com/uwp/api/windows.applicationmodel.cards.cardbuilder)

cardbuilder <br> cardbuilder.createcardelementfromjson

#### [icardbuilderstatics](https://docs.microsoft.com/uwp/api/windows.applicationmodel.cards.icardbuilderstatics)

icardbuilderstatics <br> icardbuilderstatics.createcardelementfromjson

#### [icardelement](https://docs.microsoft.com/uwp/api/windows.applicationmodel.cards.icardelement)

icardelement <br> icardelement.tojson

### [windows.applicationmodel.core](https://docs.microsoft.com/uwp/api/windows.applicationmodel.core)

#### [coreapplication](https://docs.microsoft.com/uwp/api/windows.applicationmodel.core.coreapplication)

coreapplication.requestrestartasync <br> coreapplication.requestrestartforuserasync

#### [coreapplicationview](https://docs.microsoft.com/uwp/api/windows.applicationmodel.core.coreapplicationview)

coreapplicationview.dispatcherqueue

#### [restartresult](https://docs.microsoft.com/uwp/api/windows.applicationmodel.core.restartresult)

restartresult

### [windows.applicationmodel.datatransfer.sharetarget](https://docs.microsoft.com/uwp/api/windows.applicationmodel.datatransfer.sharetarget)

#### [shareoperation](https://docs.microsoft.com/uwp/api/windows.applicationmodel.datatransfer.sharetarget.shareoperation)

shareoperation.contacts

### [windows.applicationmodel.datatransfer](https://docs.microsoft.com/uwp/api/windows.applicationmodel.datatransfer)

#### [datatransfermanager](https://docs.microsoft.com/uwp/api/windows.applicationmodel.datatransfer.datatransfermanager)

datatransfermanager.showshareui

#### [idatatransfermanagerstatics3](https://docs.microsoft.com/uwp/api/windows.applicationmodel.datatransfer.idatatransfermanagerstatics3)

idatatransfermanagerstatics3 <br> idatatransfermanagerstatics3.showshareui

#### [shareuioptions](https://docs.microsoft.com/uwp/api/windows.applicationmodel.datatransfer.shareuioptions)

shareuioptions <br> shareuioptions.invocationrect <br> shareuioptions.sharetheme <br> shareuioptions.shareuioptions

#### [shareuitheme](https://docs.microsoft.com/uwp/api/windows.applicationmodel.datatransfer.shareuitheme)

shareuitheme

### [windows.applicationmodel.payments](https://docs.microsoft.com/uwp/api/windows.applicationmodel.payments)

#### [paymentmediator](https://docs.microsoft.com/uwp/api/windows.applicationmodel.payments.paymentmediator)

paymentmediator.canmakepaymentasync

### [windows.applicationmodel.useractivities.core](https://docs.microsoft.com/uwp/api/windows.applicationmodel.useractivities.core)

#### [coreuseractivitymanager](https://docs.microsoft.com/uwp/api/windows.applicationmodel.useractivities.core.coreuseractivitymanager)

coreuseractivitymanager <br> coreuseractivitymanager.createuseractivitysessioninbackground

### [windows.applicationmodel.useractivities](https://docs.microsoft.com/uwp/api/windows.applicationmodel.useractivities)

#### [useractivity](https://docs.microsoft.com/uwp/api/windows.applicationmodel.useractivities.useractivity)

useractivity <br> useractivity.activationuri <br> useractivity.activityid <br> useractivity.contentmetadata <br> useractivity.contenttype <br> useractivity.contenturi <br> useractivity.createsession <br> useractivity.fallbackuri <br> useractivity.saveasync <br> useractivity.state <br> useractivity.visualelements

#### [useractivitychannel](https://docs.microsoft.com/uwp/api/windows.applicationmodel.useractivities.useractivitychannel)

useractivitychannel <br> useractivitychannel.getdefault <br> useractivitychannel.getorcreateuseractivityasync <br> useractivitychannel.getorcreateuseractivityasync

#### [useractivitysession](https://docs.microsoft.com/uwp/api/windows.applicationmodel.useractivities.useractivitysession)

useractivitysession <br> useractivitysession.activityid <br> useractivitysession.close

#### [useractivitystate](https://docs.microsoft.com/uwp/api/windows.applicationmodel.useractivities.useractivitystate)

useractivitystate

#### [useractivityvisualelements](https://docs.microsoft.com/uwp/api/windows.applicationmodel.useractivities.useractivityvisualelements)

useractivityvisualelements <br> useractivityvisualelements.backgroundcolor <br> useractivityvisualelements.content <br> useractivityvisualelements.description <br> useractivityvisualelements.displaytext <br> useractivityvisualelements.imageicon

## windows.devices

### [windows.devices.bluetooth.genericattributeprofile](https://docs.microsoft.com/uwp/api/windows.devices.bluetooth.genericattributeprofile)

#### [gattclientnotificationresult](https://docs.microsoft.com/uwp/api/windows.devices.bluetooth.genericattributeprofile.gattclientnotificationresult)

gattclientnotificationresult.bytessent

### [windows.devices.bluetooth](https://docs.microsoft.com/uwp/api/windows.devices.bluetooth)

#### [bluetoothdevice](https://docs.microsoft.com/uwp/api/windows.devices.bluetooth.bluetoothdevice)

bluetoothdevice.bluetoothdeviceid

#### [bluetoothdeviceid](https://docs.microsoft.com/uwp/api/windows.devices.bluetooth.bluetoothdeviceid)

bluetoothdeviceid.fromid

#### [bluetoothledevice](https://docs.microsoft.com/uwp/api/windows.devices.bluetooth.bluetoothledevice)

bluetoothledevice.bluetoothdeviceid

## windows.graphics

### [windows.graphics.printing.printticket](https://docs.microsoft.com/uwp/api/windows.graphics.printing.printticket)

#### [printticketcapabilities](https://docs.microsoft.com/uwp/api/windows.graphics.printing.printticket.printticketcapabilities)

printticketcapabilities <br> printticketcapabilities.documentbindingfeature <br> printticketcapabilities.documentcollatefeature <br> printticketcapabilities.documentduplexfeature <br> printticketcapabilities.documentholepunchfeature <br> printticketcapabilities.documentinputbinfeature <br> printticketcapabilities.documentnupfeature <br> printticketcapabilities.documentstaplefeature <br> printticketcapabilities.getfeature <br> printticketcapabilities.getparameterdefinition <br> printticketcapabilities.jobpasscodefeature <br> printticketcapabilities.name <br> printticketcapabilities.pageborderlessfeature <br> printticketcapabilities.pagemediasizefeature <br> printticketcapabilities.pagemediatypefeature <br> printticketcapabilities.pageorientationfeature <br> printticketcapabilities.pageoutputcolorfeature <br> printticketcapabilities.pageoutputqualityfeature <br> printticketcapabilities.pageresolutionfeature <br> printticketcapabilities.xmlnamespace <br> printticketcapabilities.xmlnode

#### [printticketfeature](https://docs.microsoft.com/uwp/api/windows.graphics.printing.printticket.printticketfeature)

printticketfeature <br> printticketfeature.displayname <br> printticketfeature.getoption <br> printticketfeature.getselectedoption <br> printticketfeature.name <br> printticketfeature.options <br> printticketfeature.selectiontype <br> printticketfeature.setselectedoption <br> printticketfeature.xmlnamespace <br> printticketfeature.xmlnode

#### [printticketfeatureselectiontype](https://docs.microsoft.com/uwp/api/windows.graphics.printing.printticket.printticketfeatureselectiontype)

printticketfeatureselectiontype

#### [printticketoption](https://docs.microsoft.com/uwp/api/windows.graphics.printing.printticket.printticketoption)

printticketoption <br> printticketoption.displayname <br> printticketoption.getpropertynode <br> printticketoption.getpropertyvalue <br> printticketoption.getscoredpropertynode <br> printticketoption.getscoredpropertyvalue <br> printticketoption.name <br> printticketoption.xmlnamespace <br> printticketoption.xmlnode

#### [printticketparameterdatatype](https://docs.microsoft.com/uwp/api/windows.graphics.printing.printticket.printticketparameterdatatype)

printticketparameterdatatype

#### [printticketparameterdefinition](https://docs.microsoft.com/uwp/api/windows.graphics.printing.printticket.printticketparameterdefinition)

printticketparameterdefinition <br> printticketparameterdefinition.datatype <br> printticketparameterdefinition.name <br> printticketparameterdefinition.rangemax <br> printticketparameterdefinition.rangemin <br> printticketparameterdefinition.unittype <br> printticketparameterdefinition.xmlnamespace <br> printticketparameterdefinition.xmlnode

#### [printticketparameterinitializer](https://docs.microsoft.com/uwp/api/windows.graphics.printing.printticket.printticketparameterinitializer)

printticketparameterinitializer <br> printticketparameterinitializer.name <br> printticketparameterinitializer.value <br> printticketparameterinitializer.xmlnamespace <br> printticketparameterinitializer.xmlnode

#### [printticketvalue](https://docs.microsoft.com/uwp/api/windows.graphics.printing.printticket.printticketvalue)

printticketvalue <br> printticketvalue.getvalueasinteger <br> printticketvalue.getvalueasstring <br> printticketvalue.type

#### [printticketvaluetype](https://docs.microsoft.com/uwp/api/windows.graphics.printing.printticket.printticketvaluetype)

printticketvaluetype

#### [workflowprintticket](https://docs.microsoft.com/uwp/api/windows.graphics.printing.printticket.workflowprintticket)

workflowprintticket <br> workflowprintticket.documentbindingfeature <br> workflowprintticket.documentcollatefeature <br> workflowprintticket.documentduplexfeature <br> workflowprintticket.documentholepunchfeature <br> workflowprintticket.documentinputbinfeature <br> workflowprintticket.documentnupfeature <br> workflowprintticket.documentstaplefeature <br> workflowprintticket.getcapabilities <br> workflowprintticket.getfeature <br> workflowprintticket.getparameterinitializer <br> workflowprintticket.jobpasscodefeature <br> workflowprintticket.mergeandvalidateticket <br> workflowprintticket.name <br> workflowprintticket.notifyxmlchangedasync <br> workflowprintticket.pageborderlessfeature <br> workflowprintticket.pagemediasizefeature <br> workflowprintticket.pagemediatypefeature <br> workflowprintticket.pageorientationfeature <br> workflowprintticket.pageoutputcolorfeature <br> workflowprintticket.pageoutputqualityfeature <br> workflowprintticket.pageresolutionfeature <br> workflowprintticket.setparameterinitializerasinteger <br> workflowprintticket.setparameterinitializerasstring <br> workflowprintticket.validateasync <br> workflowprintticket.xmlnamespace <br> workflowprintticket.xmlnode

#### [workflowprintticketvalidationresult](https://docs.microsoft.com/uwp/api/windows.graphics.printing.printticket.workflowprintticketvalidationresult)

workflowprintticketvalidationresult <br> workflowprintticketvalidationresult.extendederror <br> workflowprintticketvalidationresult.validated

### [windows.graphics.printing.workflow](https://docs.microsoft.com/uwp/api/windows.graphics.printing.workflow)

#### [printworkflowbackgroundsession](https://docs.microsoft.com/uwp/api/windows.graphics.printing.workflow.printworkflowbackgroundsession)

printworkflowbackgroundsession <br> printworkflowbackgroundsession.setuprequested <br> printworkflowbackgroundsession.start <br> printworkflowbackgroundsession.status <br> printworkflowbackgroundsession.submitted

#### [printworkflowbackgroundsetuprequestedeventargs](https://docs.microsoft.com/uwp/api/windows.graphics.printing.workflow.printworkflowbackgroundsetuprequestedeventargs)

printworkflowbackgroundsetuprequestedeventargs <br> printworkflowbackgroundsetuprequestedeventargs.configuration <br> printworkflowbackgroundsetuprequestedeventargs.getdeferral <br> printworkflowbackgroundsetuprequestedeventargs.getuserprintticketasync <br> printworkflowbackgroundsetuprequestedeventargs.setrequiresui

#### [printworkflowconfiguration](https://docs.microsoft.com/uwp/api/windows.graphics.printing.workflow.printworkflowconfiguration)

printworkflowconfiguration <br> printworkflowconfiguration.jobtitle <br> printworkflowconfiguration.sourceapplicationexecutablename

#### [printworkflowforegroundsession](https://docs.microsoft.com/uwp/api/windows.graphics.printing.workflow.printworkflowforegroundsession)

printworkflowforegroundsession <br> printworkflowforegroundsession.setuprequested <br> printworkflowforegroundsession.start <br> printworkflowforegroundsession.status <br> printworkflowforegroundsession.xpsdataavailable

#### [printworkflowforegroundsetuprequestedeventargs](https://docs.microsoft.com/uwp/api/windows.graphics.printing.workflow.printworkflowforegroundsetuprequestedeventargs)

printworkflowforegroundsetuprequestedeventargs <br> printworkflowforegroundsetuprequestedeventargs.configuration <br> printworkflowforegroundsetuprequestedeventargs.getdeferral <br> printworkflowforegroundsetuprequestedeventargs.getuserprintticketasync

#### [printworkflowobjectmodelsourcefilecontent](https://docs.microsoft.com/uwp/api/windows.graphics.printing.workflow.printworkflowobjectmodelsourcefilecontent)

printworkflowobjectmodelsourcefilecontent

#### [printworkflowobjectmodeltargetpackage](https://docs.microsoft.com/uwp/api/windows.graphics.printing.workflow.printworkflowobjectmodeltargetpackage)

printworkflowobjectmodeltargetpackage

#### [printworkflowsessionstatus](https://docs.microsoft.com/uwp/api/windows.graphics.printing.workflow.printworkflowsessionstatus)

printworkflowsessionstatus

#### [printworkflowsourcecontent](https://docs.microsoft.com/uwp/api/windows.graphics.printing.workflow.printworkflowsourcecontent)

printworkflowsourcecontent <br> printworkflowsourcecontent.getjobprintticketasync <br> printworkflowsourcecontent.getsourcespooldataasstreamcontent <br> printworkflowsourcecontent.getsourcespooldataasxpsobjectmodel

#### [printworkflowspoolstreamcontent](https://docs.microsoft.com/uwp/api/windows.graphics.printing.workflow.printworkflowspoolstreamcontent)

printworkflowspoolstreamcontent <br> printworkflowspoolstreamcontent.getinputstream

#### [printworkflowstreamtarget](https://docs.microsoft.com/uwp/api/windows.graphics.printing.workflow.printworkflowstreamtarget)

printworkflowstreamtarget <br> printworkflowstreamtarget.getoutputstream

#### [printworkflowsubmittedeventargs](https://docs.microsoft.com/uwp/api/windows.graphics.printing.workflow.printworkflowsubmittedeventargs)

printworkflowsubmittedeventargs <br> printworkflowsubmittedeventargs.getdeferral <br> printworkflowsubmittedeventargs.gettarget <br> printworkflowsubmittedeventargs.operation

#### [printworkflowsubmittedoperation](https://docs.microsoft.com/uwp/api/windows.graphics.printing.workflow.printworkflowsubmittedoperation)

printworkflowsubmittedoperation <br> printworkflowsubmittedoperation.complete <br> printworkflowsubmittedoperation.configuration <br> printworkflowsubmittedoperation.xpscontent

#### [printworkflowsubmittedstatus](https://docs.microsoft.com/uwp/api/windows.graphics.printing.workflow.printworkflowsubmittedstatus)

printworkflowsubmittedstatus

#### [printworkflowtarget](https://docs.microsoft.com/uwp/api/windows.graphics.printing.workflow.printworkflowtarget)

printworkflowtarget <br> printworkflowtarget.targetasstream <br> printworkflowtarget.targetasxpsobjectmodelpackage

#### [printworkflowtriggerdetails](https://docs.microsoft.com/uwp/api/windows.graphics.printing.workflow.printworkflowtriggerdetails)

printworkflowtriggerdetails <br> printworkflowtriggerdetails.printworkflowsession

#### [printworkflowuiactivatedeventargs](https://docs.microsoft.com/uwp/api/windows.graphics.printing.workflow.printworkflowuiactivatedeventargs)

printworkflowuiactivatedeventargs <br> printworkflowuiactivatedeventargs.kind <br> printworkflowuiactivatedeventargs.previousexecutionstate <br> printworkflowuiactivatedeventargs.printworkflowsession <br> printworkflowuiactivatedeventargs.splashscreen <br> printworkflowuiactivatedeventargs.user

#### [printworkflowxpsdataavailableeventargs](https://docs.microsoft.com/uwp/api/windows.graphics.printing.workflow.printworkflowxpsdataavailableeventargs)

printworkflowxpsdataavailableeventargs <br> printworkflowxpsdataavailableeventargs.getdeferral <br> printworkflowxpsdataavailableeventargs.operation

### [windows.graphics.printing3d](https://docs.microsoft.com/uwp/api/windows.graphics.printing3d)

#### [printing3d3mfpackage](https://docs.microsoft.com/uwp/api/windows.graphics.printing3d.printing3d3mfpackage)

printing3d3mfpackage.compression

#### [printing3dpackagecompression](https://docs.microsoft.com/uwp/api/windows.graphics.printing3d.printing3dpackagecompression)

printing3dpackagecompression

## windows.media

### [windows.media.core](https://docs.microsoft.com/uwp/api/windows.media.core)

#### [mediastreamsource](https://docs.microsoft.com/uwp/api/windows.media.core.mediastreamsource)

mediastreamsource.islive

#### [msestreamsource](https://docs.microsoft.com/uwp/api/windows.media.core.msestreamsource)

msestreamsource.liveseekablerange

### [windows.media.playback](https://docs.microsoft.com/uwp/api/windows.media.playback)

#### [mediaplaybacksessionbufferingstartedeventargs](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplaybacksessionbufferingstartedeventargs)

mediaplaybacksessionbufferingstartedeventargs <br> mediaplaybacksessionbufferingstartedeventargs.isplaybackinterruption

#### [mediaplayer](https://docs.microsoft.com/uwp/api/windows.media.playback.mediaplayer)

mediaplayer.rendersubtitlestosurface <br> mediaplayer.rendersubtitlestosurface <br> mediaplayer.subtitleframechanged

### [windows.media.protection.playready](https://docs.microsoft.com/uwp/api/windows.media.protection.playready)

#### [playreadyencryptionalgorithm](https://docs.microsoft.com/uwp/api/windows.media.protection.playready.playreadyencryptionalgorithm)

playreadyencryptionalgorithm

#### [playreadyhardwaredrmfeatures](https://docs.microsoft.com/uwp/api/windows.media.protection.playready.playreadyhardwaredrmfeatures)

playreadyhardwaredrmfeatures

### [windows.media.streaming.adaptive](https://docs.microsoft.com/uwp/api/windows.media.streaming.adaptive)

#### [adaptivemediasourcediagnosticavailableeventargs](https://docs.microsoft.com/uwp/api/windows.media.streaming.adaptive.adaptivemediasourcediagnosticavailableeventargs)

adaptivemediasourcediagnosticavailableeventargs.extendederror

#### [adaptivemediasourcediagnostictype](https://docs.microsoft.com/uwp/api/windows.media.streaming.adaptive.adaptivemediasourcediagnostictype)

adaptivemediasourcediagnostictype

## windows.security

### [windows.security.authentication.web.provider](https://docs.microsoft.com/uwp/api/windows.security.authentication.web.provider)

#### [webaccountmanager](https://docs.microsoft.com/uwp/api/windows.security.authentication.web.provider.webaccountmanager)

webaccountmanager.invalidateappcacheasync <br> webaccountmanager.invalidateappcacheasync

## windows.storage

### [windows.storage](https://docs.microsoft.com/uwp/api/windows.storage)

#### [storagelibrary](https://docs.microsoft.com/uwp/api/windows.storage.storagelibrary)

storagelibrary.arefoldersuggestionsavailableasync

## windows.system

### [windows.system.diagnostics](https://docs.microsoft.com/uwp/api/windows.system.diagnostics)

#### [processdiagnosticinfo](https://docs.microsoft.com/uwp/api/windows.system.diagnostics.processdiagnosticinfo)

processdiagnosticinfo.ispackaged <br> processdiagnosticinfo.trygetappdiagnosticinfo <br> processdiagnosticinfo.trygetforprocessid

### [windows.system.userprofile](https://docs.microsoft.com/uwp/api/windows.system.userprofile)

#### [globalizationpreferences](https://docs.microsoft.com/uwp/api/windows.system.userprofile.globalizationpreferences)

globalizationpreferences.trysethomegeographicregion <br> globalizationpreferences.trysetlanguages

### [windows.system](https://docs.microsoft.com/uwp/api/windows.system)

#### [appdiagnosticinfo](https://docs.microsoft.com/uwp/api/windows.system.appdiagnosticinfo)

appdiagnosticinfo.createresourcegroupwatcher <br> appdiagnosticinfo.createwatcher <br> appdiagnosticinfo.getresourcegroups <br> appdiagnosticinfo.requestinfoforappasync <br> appdiagnosticinfo.requestinfoforpackageasync <br> appdiagnosticinfo.requestpermissionasync

#### [appdiagnosticinfowatcher](https://docs.microsoft.com/uwp/api/windows.system.appdiagnosticinfowatcher)

appdiagnosticinfowatcher <br> appdiagnosticinfowatcher.added <br> appdiagnosticinfowatcher.enumerationcompleted <br> appdiagnosticinfowatcher.removed <br> appdiagnosticinfowatcher.start <br> appdiagnosticinfowatcher.status <br> appdiagnosticinfowatcher.stop <br> appdiagnosticinfowatcher.stopped

#### [appdiagnosticinfowatchereventargs](https://docs.microsoft.com/uwp/api/windows.system.appdiagnosticinfowatchereventargs)

appdiagnosticinfowatchereventargs <br> appdiagnosticinfowatchereventargs.appdiagnosticinfo

#### [appdiagnosticinfowatcherstatus](https://docs.microsoft.com/uwp/api/windows.system.appdiagnosticinfowatcherstatus)

appdiagnosticinfowatcherstatus

#### [backgroundtaskreport](https://docs.microsoft.com/uwp/api/windows.system.backgroundtaskreport)

backgroundtaskreport <br> backgroundtaskreport.name <br> backgroundtaskreport.trigger

#### [diagnosticpermission](https://docs.microsoft.com/uwp/api/windows.system.diagnosticpermission)

diagnosticpermission

#### [dispatcherqueue](https://docs.microsoft.com/uwp/api/windows.system.dispatcherqueue)

dispatcherqueue <br> dispatcherqueue.createtimer <br> dispatcherqueue.getforcurrentthread <br> dispatcherqueue.tryenqueue <br> dispatcherqueue.tryenqueue

#### [dispatcherqueuecontroller](https://docs.microsoft.com/uwp/api/windows.system.dispatcherqueuecontroller)

dispatcherqueuecontroller <br> dispatcherqueuecontroller.createqueuewithdedicatedthread <br> dispatcherqueuecontroller.dispatcherqueue <br> dispatcherqueuecontroller.queueshutdown <br> dispatcherqueuecontroller.shutdownqueueasync

#### [dispatcherqueuehandler](https://docs.microsoft.com/uwp/api/windows.system.dispatcherqueuehandler)

dispatcherqueuehandler

#### [dispatcherqueuepriority](https://docs.microsoft.com/uwp/api/windows.system.dispatcherqueuepriority)

dispatcherqueuepriority

#### [dispatcherqueuetimer](https://docs.microsoft.com/uwp/api/windows.system.dispatcherqueuetimer)

dispatcherqueuetimer <br> dispatcherqueuetimer.interval <br> dispatcherqueuetimer.isrepeating <br> dispatcherqueuetimer.isstarted <br> dispatcherqueuetimer.start <br> dispatcherqueuetimer.stop <br> dispatcherqueuetimer.tick

#### [energyquotastate](https://docs.microsoft.com/uwp/api/windows.system.energyquotastate)

energyquotastate

#### [executionstate](https://docs.microsoft.com/uwp/api/windows.system.executionstate)

executionstate

#### [memoryreport](https://docs.microsoft.com/uwp/api/windows.system.memoryreport)

memoryreport <br> memoryreport.commitusagelevel <br> memoryreport.commitusagelimit <br> memoryreport.privatecommitusage <br> memoryreport.totalcommitusage

#### [resourcegroupinfo](https://docs.microsoft.com/uwp/api/windows.system.resourcegroupinfo)

resourcegroupinfo <br> resourcegroupinfo.getbackgroundtaskreports <br> resourcegroupinfo.getmemoryreport <br> resourcegroupinfo.getprocessdiagnostics <br> resourcegroupinfo.getstatereport <br> resourcegroupinfo.instanceid <br> resourcegroupinfo.isshared

#### [resourcegroupinfowatcher](https://docs.microsoft.com/uwp/api/windows.system.resourcegroupinfowatcher)

resourcegroupinfowatcher <br> resourcegroupinfowatcher.added <br> resourcegroupinfowatcher.enumerationcompleted <br> resourcegroupinfowatcher.executionstatechanged <br> resourcegroupinfowatcher.removed <br> resourcegroupinfowatcher.start <br> resourcegroupinfowatcher.status <br> resourcegroupinfowatcher.stop <br> resourcegroupinfowatcher.stopped

#### [resourcegroupinfowatchereventargs](https://docs.microsoft.com/uwp/api/windows.system.resourcegroupinfowatchereventargs)

resourcegroupinfowatchereventargs <br> resourcegroupinfowatchereventargs.appdiagnosticinfo <br> resourcegroupinfowatchereventargs.resourcegroupinfo

#### [resourcegroupinfowatcherexecutionstatechangedeventargs](https://docs.microsoft.com/uwp/api/windows.system.resourcegroupinfowatcherexecutionstatechangedeventargs)

resourcegroupinfowatcherexecutionstatechangedeventargs <br> resourcegroupinfowatcherexecutionstatechangedeventargs.appdiagnosticinfo <br> resourcegroupinfowatcherexecutionstatechangedeventargs.resourcegroupinfo

#### [resourcegroupinfowatcherstatus](https://docs.microsoft.com/uwp/api/windows.system.resourcegroupinfowatcherstatus)

resourcegroupinfowatcherstatus

#### [statereport](https://docs.microsoft.com/uwp/api/windows.system.statereport)

statereport <br> statereport.energyquotastate <br> statereport.executionstate

## windows.ui

### [windows.ui.composition.effects](https://docs.microsoft.com/uwp/api/windows.ui.composition.effects)

#### [scenelightingeffect](https://docs.microsoft.com/uwp/api/windows.ui.composition.effects.scenelightingeffect)

scenelightingeffect.brdftype

#### [scenelightingeffectbrdftype](https://docs.microsoft.com/uwp/api/windows.ui.composition.effects.scenelightingeffectbrdftype)

scenelightingeffectbrdftype

### [windows.ui.composition](https://docs.microsoft.com/uwp/api/windows.ui.composition)

#### [ambientlight](https://docs.microsoft.com/uwp/api/windows.ui.composition.ambientlight)

ambientlight.intensity

#### [compositionanchor](https://docs.microsoft.com/uwp/api/windows.ui.composition.compositionanchor)

compositionanchor

#### [compositionanimation](https://docs.microsoft.com/uwp/api/windows.ui.composition.compositionanimation)

compositionanimation.expressionproperties

#### [compositiondropshadowsourcepolicy](https://docs.microsoft.com/uwp/api/windows.ui.composition.compositiondropshadowsourcepolicy)

compositiondropshadowsourcepolicy

#### [compositionisland](https://docs.microsoft.com/uwp/api/windows.ui.composition.compositionisland)

compositionisland <br> compositionisland.actualsize <br> compositionisland.actualsizechanged <br> compositionisland.primarydpiscale <br> compositionisland.rasterizationscale <br> compositionisland.rasterizationscaleanchor <br> compositionisland.requestedsize <br> compositionisland.scalechanged <br> compositionisland.snaptopixeladjustment <br> compositionisland.snaptopixeladjustmentchanged <br> compositionisland.visibilityhintschanged <br> compositionisland.visiblityhints

#### [compositionislandeventargs](https://docs.microsoft.com/uwp/api/windows.ui.composition.compositionislandeventargs)

compositionislandeventargs

#### [compositionislandvisibilityhints](https://docs.microsoft.com/uwp/api/windows.ui.composition.compositionislandvisibilityhints)

compositionislandvisibilityhints

#### [compositionlight](https://docs.microsoft.com/uwp/api/windows.ui.composition.compositionlight)

compositionlight.exclusions

#### [compositor](https://docs.microsoft.com/uwp/api/windows.ui.composition.compositor)

compositor.createvisualislandsite <br> compositor.createvisualtreeisland

#### [distantlight](https://docs.microsoft.com/uwp/api/windows.ui.composition.distantlight)

distantlight.intensity

#### [dropshadow](https://docs.microsoft.com/uwp/api/windows.ui.composition.dropshadow)

dropshadow.sourcepolicy

#### [expressionproperties](https://docs.microsoft.com/uwp/api/windows.ui.composition.expressionproperties)

expressionproperties <br> expressionproperties.clear <br> expressionproperties.first <br> expressionproperties.getview <br> expressionproperties.haskey <br> expressionproperties.insert <br> expressionproperties.lookup <br> expressionproperties.remove <br> expressionproperties.size

#### [framedislandsite](https://docs.microsoft.com/uwp/api/windows.ui.composition.framedislandsite)

framedislandsite <br> framedislandsite.connectisland

#### [hwndislandsite](https://docs.microsoft.com/uwp/api/windows.ui.composition.hwndislandsite)

hwndislandsite <br> hwndislandsite.connectisland <br> hwndislandsite.placementanchor <br> hwndislandsite.placementanchorpoint

#### [icompositionislandsite](https://docs.microsoft.com/uwp/api/windows.ui.composition.icompositionislandsite)

icompositionislandsite <br> icompositionislandsite.connectisland

#### [ivisual3](https://docs.microsoft.com/uwp/api/windows.ui.composition.ivisual3)

ivisual3 <br> ivisual3.createanchor

#### [pointlight](https://docs.microsoft.com/uwp/api/windows.ui.composition.pointlight)

pointlight.intensity

#### [popupislandsite](https://docs.microsoft.com/uwp/api/windows.ui.composition.popupislandsite)

popupislandsite <br> popupislandsite.connectisland <br> popupislandsite.placementanchor <br> popupislandsite.placementanchorpoint

#### [spotlight](https://docs.microsoft.com/uwp/api/windows.ui.composition.spotlight)

spotlight.innerconeintensity <br> spotlight.outerconeintensity

#### [visual](https://docs.microsoft.com/uwp/api/windows.ui.composition.visual)

visual.createanchor

#### [visualislandsite](https://docs.microsoft.com/uwp/api/windows.ui.composition.visualislandsite)

visualislandsite <br> visualislandsite.actualsize <br> visualislandsite.connectisland <br> visualislandsite.requestedsize <br> visualislandsite.requestedsizechanged <br> visualislandsite.sitevisual

#### [visualislandsiteeventargs](https://docs.microsoft.com/uwp/api/windows.ui.composition.visualislandsiteeventargs)

visualislandsiteeventargs

#### [visualtreeisland](https://docs.microsoft.com/uwp/api/windows.ui.composition.visualtreeisland)

visualtreeisland <br> visualtreeisland.children

### [windows.ui.core](https://docs.microsoft.com/uwp/api/windows.ui.core)

#### [corewindow](https://docs.microsoft.com/uwp/api/windows.ui.core.corewindow)

corewindow.dispatcherqueue

### [windows.ui.viewmanagement](https://docs.microsoft.com/uwp/api/windows.ui.viewmanagement)

#### [coreinputview](https://docs.microsoft.com/uwp/api/windows.ui.viewmanagement.coreinputview)

coreinputview <br> coreinputview.frameworkoccludinginputviewschanged <br> coreinputview.getforcurrentview <br> coreinputview.occludinginputviews <br> coreinputview.occludinginputviewschanged <br> coreinputview.tryhideprimaryview <br> coreinputview.tryshowprimaryview

#### [coreinputviewframeworkoccludinginputviewschangedeventargs](https://docs.microsoft.com/uwp/api/windows.ui.viewmanagement.coreinputviewframeworkoccludinginputviewschangedeventargs)

coreinputviewframeworkoccludinginputviewschangedeventargs <br> coreinputviewframeworkoccludinginputviewschangedeventargs.handled <br> coreinputviewframeworkoccludinginputviewschangedeventargs.occludinginputviews

#### [coreinputviewoccludinginputviewschangedeventargs](https://docs.microsoft.com/uwp/api/windows.ui.viewmanagement.coreinputviewoccludinginputviewschangedeventargs)

coreinputviewoccludinginputviewschangedeventargs <br> coreinputviewoccludinginputviewschangedeventargs.handled <br> coreinputviewoccludinginputviewschangedeventargs.occludinginputviews

#### [coreoccludinginputview](https://docs.microsoft.com/uwp/api/windows.ui.viewmanagement.coreoccludinginputview)

coreoccludinginputview

#### [coreoccludinginputviews](https://docs.microsoft.com/uwp/api/windows.ui.viewmanagement.coreoccludinginputviews)

coreoccludinginputviews <br> coreoccludinginputviews.first

#### [coreoccludinginputviewtype](https://docs.microsoft.com/uwp/api/windows.ui.viewmanagement.coreoccludinginputviewtype)

coreoccludinginputviewtype

#### [viewmodepreferences](https://docs.microsoft.com/uwp/api/windows.ui.viewmanagement.viewmodepreferences)

viewmodepreferences.custommaxsize <br> viewmodepreferences.customminsize

### [windows.ui.webui](https://docs.microsoft.com/uwp/api/windows.ui.webui)

#### [webuiconsoleactivatedeventargs](https://docs.microsoft.com/uwp/api/windows.ui.webui.webuiconsoleactivatedeventargs)

webuiconsoleactivatedeventargs <br> webuiconsoleactivatedeventargs.activatedoperation <br> webuiconsoleactivatedeventargs.arguments <br> webuiconsoleactivatedeventargs.kind <br> webuiconsoleactivatedeventargs.previousexecutionstate <br> webuiconsoleactivatedeventargs.splashscreen

### [windows.ui.xaml.controls](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls)

#### [colorchangedeventargs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.colorchangedeventargs)

colorchangedeventargs <br> colorchangedeventargs.newcolor <br> colorchangedeventargs.oldcolor

#### [colorchannel](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.colorchannel)

colorchannel

#### [colorpicker](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.colorpicker)

colorpicker <br> colorpicker.color <br> colorpicker.colorchanged <br> colorpicker.colorpicker <br> colorpicker.colorproperty <br> colorpicker.colorspectrumcomponents <br> colorpicker.colorspectrumcomponentsproperty <br> colorpicker.colorspectrumshape <br> colorpicker.colorspectrumshapeproperty <br> colorpicker.isalphaenabled <br> colorpicker.isalphaenabledproperty <br> colorpicker.isalphaslidervisible <br> colorpicker.isalphaslidervisibleproperty <br> colorpicker.isalphatextinputvisible <br> colorpicker.isalphatextinputvisibleproperty <br> colorpicker.iscolorchanneltextinputvisible <br> colorpicker.iscolorchanneltextinputvisibleproperty <br> colorpicker.iscolorpreviewvisible <br> colorpicker.iscolorpreviewvisibleproperty <br> colorpicker.iscolorslidervisible <br> colorpicker.iscolorslidervisibleproperty <br> colorpicker.iscolorspectrumvisible <br> colorpicker.iscolorspectrumvisibleproperty <br> colorpicker.ishexinputvisible <br> colorpicker.ishexinputvisibleproperty <br> colorpicker.ismorebuttonvisible <br> colorpicker.ismorebuttonvisibleproperty <br> colorpicker.maxhue <br> colorpicker.maxhueproperty <br> colorpicker.maxsaturation <br> colorpicker.maxsaturationproperty <br> colorpicker.maxvalue <br> colorpicker.maxvalueproperty <br> colorpicker.minhue <br> colorpicker.minhueproperty <br> colorpicker.minsaturation <br> colorpicker.minsaturationproperty <br> colorpicker.minvalue <br> colorpicker.minvalueproperty <br> colorpicker.previouscolor <br> colorpicker.previouscolorproperty

#### [colorpickerslider](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.colorpickerslider)

colorpickerslider <br> colorpickerslider.colorchannel <br> colorpickerslider.colorchannelproperty <br> colorpickerslider.colorpickerslider

#### [colorpickersliderautomationpeer](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.colorpickersliderautomationpeer)

colorpickersliderautomationpeer <br> colorpickersliderautomationpeer.colorpickersliderautomationpeer

#### [colorspectrum](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.colorspectrum)

colorspectrum <br> colorspectrum.alpha <br> colorspectrum.alphaproperty <br> colorspectrum.color <br> colorspectrum.colorchanged <br> colorspectrum.colorproperty <br> colorspectrum.colorspectrum <br> colorspectrum.components <br> colorspectrum.componentsproperty <br> colorspectrum.hsvcolor <br> colorspectrum.hsvcolorproperty <br> colorspectrum.maxhue <br> colorspectrum.maxhueproperty <br> colorspectrum.maxsaturation <br> colorspectrum.maxsaturationproperty <br> colorspectrum.maxvalue <br> colorspectrum.maxvalueproperty <br> colorspectrum.minhue <br> colorspectrum.minhueproperty <br> colorspectrum.minsaturation <br> colorspectrum.minsaturationproperty <br> colorspectrum.minvalue <br> colorspectrum.minvalueproperty <br> colorspectrum.shape <br> colorspectrum.shapeproperty

#### [colorspectrumautomationpeer](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.colorspectrumautomationpeer)

colorspectrumautomationpeer <br> colorspectrumautomationpeer.colorspectrumautomationpeer

#### [colorspectrumcomponents](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.colorspectrumcomponents)

colorspectrumcomponents

#### [colorspectrumshape](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.colorspectrumshape)

colorspectrumshape

#### [control](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.control)

control.onpreviewkeydown <br> control.onpreviewkeyup

#### [displaymodechangedeventargs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.displaymodechangedeventargs)

displaymodechangedeventargs <br> displaymodechangedeventargs.displaymode <br> displaymodechangedeventargs.displaymodechangedeventargs

#### [hsvcolor](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.hsvcolor)

hsvcolor

#### [navigationmenuitem](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.navigationmenuitem)

navigationmenuitem <br> navigationmenuitem.compactpanelength <br> navigationmenuitem.icon <br> navigationmenuitem.iconproperty <br> navigationmenuitem.invoked <br> navigationmenuitem.isselected <br> navigationmenuitem.isselectedproperty <br> navigationmenuitem.navigationmenuitem <br> navigationmenuitem.text <br> navigationmenuitem.textproperty

#### [navigationmenuitemautomationpeer](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.navigationmenuitemautomationpeer)

navigationmenuitemautomationpeer <br> navigationmenuitemautomationpeer.navigationmenuitemautomationpeer

#### [navigationmenuitembase](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.navigationmenuitembase)

navigationmenuitembase <br> navigationmenuitembase.navigationmenuitembase

#### [navigationmenuitembaseobservablecollection](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.navigationmenuitembaseobservablecollection)

navigationmenuitembaseobservablecollection <br> navigationmenuitembaseobservablecollection.append <br> navigationmenuitembaseobservablecollection.clear <br> navigationmenuitembaseobservablecollection.first <br> navigationmenuitembaseobservablecollection.getat <br> navigationmenuitembaseobservablecollection.getmany <br> navigationmenuitembaseobservablecollection.getview <br> navigationmenuitembaseobservablecollection.indexof <br> navigationmenuitembaseobservablecollection.insertat <br> navigationmenuitembaseobservablecollection.removeat <br> navigationmenuitembaseobservablecollection.removeatend <br> navigationmenuitembaseobservablecollection.replaceall <br> navigationmenuitembaseobservablecollection.setat <br> navigationmenuitembaseobservablecollection.size <br> navigationmenuitembaseobservablecollection.vectorchanged

#### [navigationmenuitemseparator](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.navigationmenuitemseparator)

navigationmenuitemseparator <br> navigationmenuitemseparator.navigationmenuitemseparator

#### [navigationview](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.navigationview)

navigationview <br> navigationview.alwaysshowheader <br> navigationview.alwaysshowheaderproperty <br> navigationview.compactmodethresholdwidth <br> navigationview.compactmodethresholdwidthproperty <br> navigationview.compactpanelength <br> navigationview.compactpanelengthproperty <br> navigationview.displaymode <br> navigationview.displaymodechanged <br> navigationview.displaymodeproperty <br> navigationview.expandedmodethresholdwidth <br> navigationview.expandedmodethresholdwidthproperty <br> navigationview.header <br> navigationview.headerproperty <br> navigationview.headertemplate <br> navigationview.headertemplateproperty <br> navigationview.ispaneopen <br> navigationview.ispaneopenproperty <br> navigationview.ispanetogglebuttonvisible <br> navigationview.ispanetogglebuttonvisibleproperty <br> navigationview.issettingsvisible <br> navigationview.issettingsvisibleproperty <br> navigationview.menuitems <br> navigationview.menuitemsproperty <br> navigationview.navigationview <br> navigationview.openpanelength <br> navigationview.openpanelengthproperty <br> navigationview.panefooter <br> navigationview.panefooterproperty <br> navigationview.panetogglebuttonstyle <br> navigationview.panetogglebuttonstyleproperty <br> navigationview.settingsinvoked

#### [navigationviewdisplaymode](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.navigationviewdisplaymode)

navigationviewdisplaymode

#### [parallaxsourceoffsetkind](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.parallaxsourceoffsetkind)

parallaxsourceoffsetkind

#### [parallaxview](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.parallaxview)

parallaxview <br> parallaxview.child <br> parallaxview.childproperty <br> parallaxview.horizontalshift <br> parallaxview.horizontalshiftproperty <br> parallaxview.horizontalsourceendoffset <br> parallaxview.horizontalsourceendoffsetproperty <br> parallaxview.horizontalsourceoffsetkind <br> parallaxview.horizontalsourceoffsetkindproperty <br> parallaxview.horizontalsourcestartoffset <br> parallaxview.horizontalsourcestartoffsetproperty <br> parallaxview.ishorizontalshiftclamped <br> parallaxview.ishorizontalshiftclampedproperty <br> parallaxview.isverticalshiftclamped <br> parallaxview.isverticalshiftclampedproperty <br> parallaxview.maxhorizontalshiftratio <br> parallaxview.maxhorizontalshiftratioproperty <br> parallaxview.maxverticalshiftratio <br> parallaxview.maxverticalshiftratioproperty <br> parallaxview.parallaxview <br> parallaxview.refreshautomatichorizontaloffsets <br> parallaxview.refreshautomaticverticaloffsets <br> parallaxview.source <br> parallaxview.sourceproperty <br> parallaxview.verticalshift <br> parallaxview.verticalshiftproperty <br> parallaxview.verticalsourceendoffset <br> parallaxview.verticalsourceendoffsetproperty <br> parallaxview.verticalsourceoffsetkind <br> parallaxview.verticalsourceoffsetkindproperty <br> parallaxview.verticalsourcestartoffset <br> parallaxview.verticalsourcestartoffsetproperty

#### [personpicture](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.personpicture)

personpicture <br> personpicture.badgeglyph <br> personpicture.badgeglyphproperty <br> personpicture.badgeimagesource <br> personpicture.badgeimagesourceproperty <br> personpicture.badgenumber <br> personpicture.badgenumberproperty <br> personpicture.badgetext <br> personpicture.badgetextproperty <br> personpicture.contact <br> personpicture.contactproperty <br> personpicture.displayname <br> personpicture.displaynameproperty <br> personpicture.initials <br> personpicture.initialsproperty <br> personpicture.isgroup <br> personpicture.isgroupproperty <br> personpicture.personpicture <br> personpicture.prefersmallimage <br> personpicture.prefersmallimageproperty <br> personpicture.profilepicture <br> personpicture.profilepictureproperty

#### [personpictureautomationpeer](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.personpictureautomationpeer)

personpictureautomationpeer <br> personpictureautomationpeer.personpictureautomationpeer

#### [ratingscontrol](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.ratingscontrol)

ratingscontrol <br> ratingscontrol.caption <br> ratingscontrol.captionproperty <br> ratingscontrol.initialsetvalue <br> ratingscontrol.initialsetvalueproperty <br> ratingscontrol.isclearenabled <br> ratingscontrol.isclearenabledproperty <br> ratingscontrol.isreadonly <br> ratingscontrol.isreadonlyproperty <br> ratingscontrol.maxrating <br> ratingscontrol.maxratingproperty <br> ratingscontrol.placeholdervalue <br> ratingscontrol.placeholdervalueproperty <br> ratingscontrol.ratingscontrol <br> ratingscontrol.value <br> ratingscontrol.valuechanged <br> ratingscontrol.valueproperty

#### [ratingscontrolautomationpeer](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.ratingscontrolautomationpeer)

ratingscontrolautomationpeer <br> ratingscontrolautomationpeer.ratingscontrolautomationpeer


#### [reveallistviewitempresenter](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.reveallistviewitempresenter)

reveallistviewitempresenter <br> reveallistviewitempresenter.reveallistviewitempresenter

#### [treeview](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.treeview)

treeview <br> treeview.collapsenode <br> treeview.expanding <br> treeview.expandnode <br> treeview.itemclicked <br> treeview.listcontrol <br> treeview.rootnode <br> treeview.rootnodeproperty <br> treeview.treeview

#### [treeviewexpandingeventargs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.treeviewexpandingeventargs)

treeviewexpandingeventargs <br> treeviewexpandingeventargs.node

#### [treeviewitem](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.treeviewitem)

treeviewitem <br> treeviewitem.treeviewitem

#### [treeviewitemautomationpeer](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.treeviewitemautomationpeer)

treeviewitemautomationpeer <br> treeviewitemautomationpeer.treeviewitemautomationpeer

#### [treeviewitemclickeventargs](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.treeviewitemclickeventargs)

treeviewitemclickeventargs <br> treeviewitemclickeventargs.clickeditem <br> treeviewitemclickeventargs.ishandled

#### [treeviewlist](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.treeviewlist)

treeviewlist <br> treeviewlist.treeviewlist

#### [treeviewlistautomationpeer](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.treeviewlistautomationpeer)

treeviewlistautomationpeer <br> treeviewlistautomationpeer.treeviewlistautomationpeer

#### [treeviewnode](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.treeviewnode)

treeviewnode <br> treeviewnode.append <br> treeviewnode.clear <br> treeviewnode.data <br> treeviewnode.depth <br> treeviewnode.depthproperty <br> treeviewnode.first <br> treeviewnode.getat <br> treeviewnode.getview <br> treeviewnode.hasitems <br> treeviewnode.hasitemsproperty <br> treeviewnode.hasunrealizeditems <br> treeviewnode.indexof <br> treeviewnode.insertat <br> treeviewnode.isexpanded <br> treeviewnode.isexpandedchanged <br> treeviewnode.isexpandedproperty <br> treeviewnode.parentnode <br> treeviewnode.removeat <br> treeviewnode.removeatend <br> treeviewnode.setat <br> treeviewnode.size <br> treeviewnode.treeviewnode <br> treeviewnode.vectorchanged

#### [xamlbooleantovisibilityconverter](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.xamlbooleantovisibilityconverter)

xamlbooleantovisibilityconverter <br> xamlbooleantovisibilityconverter.convert <br> xamlbooleantovisibilityconverter.convertback <br> xamlbooleantovisibilityconverter.xamlbooleantovisibilityconverter

#### [xamlintegertoindentationconverter](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.xamlintegertoindentationconverter)

xamlintegertoindentationconverter <br> xamlintegertoindentationconverter.convert <br> xamlintegertoindentationconverter.convertback <br> xamlintegertoindentationconverter.xamlintegertoindentationconverter

### [windows.ui.xaml.documents](https://docs.microsoft.com/uwp/api/windows.ui.xaml.documents)

#### [hyperlink](https://docs.microsoft.com/uwp/api/windows.ui.xaml.documents.hyperlink)

hyperlink.istabstop <br> hyperlink.istabstopproperty <br> hyperlink.tabindex <br> hyperlink.tabindexproperty

### [windows.ui.xaml.media](https://docs.microsoft.com/uwp/api/windows.ui.xaml.media)

#### [acrylicbackgroundsource](https://docs.microsoft.com/uwp/api/windows.ui.xaml.media.acrylicbackgroundsource)

acrylicbackgroundsource

#### [acrylicbrush](https://docs.microsoft.com/uwp/api/windows.ui.xaml.media.acrylicbrush)

acrylicbrush <br> acrylicbrush.acrylicbrush <br> acrylicbrush.backgroundsource <br> acrylicbrush.backgroundsourceproperty <br> acrylicbrush.fallbackforced <br> acrylicbrush.fallbackforcedproperty <br> acrylicbrush.tintcolor <br> acrylicbrush.tintcolorproperty <br> acrylicbrush.tintopacity <br> acrylicbrush.tintopacityproperty

#### [revealbackgroundbrush](https://docs.microsoft.com/uwp/api/windows.ui.xaml.media.revealbackgroundbrush)

revealbackgroundbrush <br> revealbackgroundbrush.revealbackgroundbrush

#### [revealborderbrush](https://docs.microsoft.com/uwp/api/windows.ui.xaml.media.revealborderbrush)

revealborderbrush <br> revealborderbrush.revealborderbrush

#### [revealbrush](https://docs.microsoft.com/uwp/api/windows.ui.xaml.media.revealbrush)

revealbrush <br> revealbrush.color <br> revealbrush.colorproperty <br> revealbrush.fallbackforced <br> revealbrush.fallbackforcedproperty <br> revealbrush.revealbrush <br> revealbrush.targettheme <br> revealbrush.targetthemeproperty

#### [revealbrushhelper](https://docs.microsoft.com/uwp/api/windows.ui.xaml.media.revealbrushhelper)

revealbrushhelper <br> revealbrushhelper.getstate <br> revealbrushhelper.setstate <br> revealbrushhelper.stateproperty

#### [revealbrushhelperstate](https://docs.microsoft.com/uwp/api/windows.ui.xaml.media.revealbrushhelperstate)

revealbrushhelperstate

#### [xamlambientlight](https://docs.microsoft.com/uwp/api/windows.ui.xaml.media.xamlambientlight)

xamlambientlight <br> xamlambientlight.color <br> xamlambientlight.colorproperty <br> xamlambientlight.getistarget <br> xamlambientlight.istargetproperty <br> xamlambientlight.setistarget <br> xamlambientlight.xamlambientlight

### [windows.ui.xaml](https://docs.microsoft.com/uwp/api/windows.ui.xaml)

#### [uielement](https://docs.microsoft.com/uwp/api/windows.ui.xaml.uielement)

uielement.previewkeydown <br> uielement.previewkeydownevent <br> uielement.previewkeyup <br> uielement.previewkeyupevent

