---
title: Windows 10 Fall Creators Update API changes
description: Developers can use the following list to identify new or changed namespaces in Windows 10 build 16299
keywords: Windows 10, 1709, apis, 16299
ms.date: 11/02/2017
ms.topic: article


ms.localizationpriority: medium
---
# New APIs in Windows 10 build 16299

New and updated API namespaces have been made available to developers in Windows 10 build 16299, also known as the Fall Creators Update or version 1709. Below is a full list of documentation published for namespaces added or modified in this release.

For information on APIs added in the previous public release, see [New APIs in the Windows 10 Creators Update](windows-10-build-15063-api-diff.md).

## windows.applicationmodel

### [windows.applicationmodel.activation](/uwp/api/windows.applicationmodel.activation)

#### [commandlineactivatedeventargs](/uwp/api/windows.applicationmodel.activation.commandlineactivatedeventargs)

commandlineactivatedeventargs <br> commandlineactivatedeventargs.kind <br> commandlineactivatedeventargs.operation <br> commandlineactivatedeventargs.previousexecutionstate <br> commandlineactivatedeventargs.splashscreen <br> commandlineactivatedeventargs.user

#### [commandlineactivationoperation](/uwp/api/windows.applicationmodel.activation.commandlineactivationoperation)

commandlineactivationoperation <br> commandlineactivationoperation.arguments <br> commandlineactivationoperation.currentdirectorypath <br> commandlineactivationoperation.exitcode <br> commandlineactivationoperation.getdeferral

#### [icommandlineactivatedeventargs](/uwp/api/windows.applicationmodel.activation.icommandlineactivatedeventargs)

icommandlineactivatedeventargs <br> icommandlineactivatedeventargs.operation

#### [istartuptaskactivatedeventargs](/uwp/api/windows.applicationmodel.activation.istartuptaskactivatedeventargs)

istartuptaskactivatedeventargs <br> istartuptaskactivatedeventargs.taskid

#### [startuptaskactivatedeventargs](/uwp/api/windows.applicationmodel.activation.startuptaskactivatedeventargs)

startuptaskactivatedeventargs <br> startuptaskactivatedeventargs.kind <br> startuptaskactivatedeventargs.previousexecutionstate <br> startuptaskactivatedeventargs.splashscreen <br> startuptaskactivatedeventargs.taskid <br> startuptaskactivatedeventargs.user

### [windows.applicationmodel.appointments](/uwp/api/windows.applicationmodel.appointments)

#### [appointmentstore](/uwp/api/windows.applicationmodel.appointments.appointmentstore)

appointmentstore.getchangetracker

#### [appointmentstorechangetracker](/uwp/api/windows.applicationmodel.appointments.appointmentstorechangetracker)

appointmentstorechangetracker.istracking

### [windows.applicationmodel.appservice](/uwp/api/windows.applicationmodel.appservice)

#### [appservicetriggerdetails](/uwp/api/windows.applicationmodel.appservice.appservicetriggerdetails)

appservicetriggerdetails.checkcallerforcapabilityasync

### [windows.applicationmodel.background](/uwp/api/windows.applicationmodel.background)

#### [geovisittrigger](/uwp/api/windows.applicationmodel.background.geovisittrigger)

geovisittrigger <br> geovisittrigger.geovisittrigger <br> geovisittrigger.monitoringscope

#### [paymentappcanmakepaymenttrigger](/uwp/api/windows.applicationmodel.background.paymentappcanmakepaymenttrigger)

paymentappcanmakepaymenttrigger <br> paymentappcanmakepaymenttrigger.paymentappcanmakepaymenttrigger

### [windows.applicationmodel.calls](/uwp/api/windows.applicationmodel.calls)

#### [voipcallcoordinator](/uwp/api/windows.applicationmodel.calls.voipcallcoordinator)

voipcallcoordinator.setupnewacceptedcall

#### [voipphonecall](/uwp/api/windows.applicationmodel.calls.voipphonecall)

voipphonecall.tryshowappui

### [windows.applicationmodel.contacts.dataprovider](/uwp/api/windows.applicationmodel.contacts.dataprovider)

#### [contactdataproviderconnection](/uwp/api/windows.applicationmodel.contacts.dataprovider.contactdataproviderconnection)

contactdataproviderconnection.createorupdatecontactrequested <br> contactdataproviderconnection.deletecontactrequested

#### [contactlistcreateorupdatecontactrequest](/uwp/api/windows.applicationmodel.contacts.dataprovider.contactlistcreateorupdatecontactrequest)

contactlistcreateorupdatecontactrequest <br> contactlistcreateorupdatecontactrequest.contact <br> contactlistcreateorupdatecontactrequest.contactlistid <br> contactlistcreateorupdatecontactrequest.reportcompletedasync <br> contactlistcreateorupdatecontactrequest.reportfailedasync

#### [contactlistcreateorupdatecontactrequesteventargs](/uwp/api/windows.applicationmodel.contacts.dataprovider.contactlistcreateorupdatecontactrequesteventargs)

contactlistcreateorupdatecontactrequesteventargs <br> contactlistcreateorupdatecontactrequesteventargs.getdeferral <br> contactlistcreateorupdatecontactrequesteventargs.request

#### [contactlistdeletecontactrequest](/uwp/api/windows.applicationmodel.contacts.dataprovider.contactlistdeletecontactrequest)

contactlistdeletecontactrequest <br> contactlistdeletecontactrequest.contactid <br> contactlistdeletecontactrequest.contactlistid <br> contactlistdeletecontactrequest.reportcompletedasync <br> contactlistdeletecontactrequest.reportfailedasync

#### [contactlistdeletecontactrequesteventargs](/uwp/api/windows.applicationmodel.contacts.dataprovider.contactlistdeletecontactrequesteventargs)

contactlistdeletecontactrequesteventargs <br> contactlistdeletecontactrequesteventargs.getdeferral <br> contactlistdeletecontactrequesteventargs.request

### [windows.applicationmodel.contacts](/uwp/api/windows.applicationmodel.contacts)

#### [contactchangetracker](/uwp/api/windows.applicationmodel.contacts.contactchangetracker)

contactchangetracker.istracking

#### [contactlist](/uwp/api/windows.applicationmodel.contacts.contactlist)

contactlist.getchangetracker <br> contactlist.limitedwriteoperations

#### [contactlistlimitedwriteoperations](/uwp/api/windows.applicationmodel.contacts.contactlistlimitedwriteoperations)

contactlistlimitedwriteoperations <br> contactlistlimitedwriteoperations.trycreateorupdatecontactasync <br> contactlistlimitedwriteoperations.trydeletecontactasync

#### [contactstore](/uwp/api/windows.applicationmodel.contacts.contactstore)

contactstore.getchangetracker

### [windows.applicationmodel.core](/uwp/api/windows.applicationmodel.core)

#### [applistentry](/uwp/api/windows.applicationmodel.core.applistentry)

applistentry.appusermodelid

#### [apprestartfailurereason](/uwp/api/windows.applicationmodel.core.apprestartfailurereason)

apprestartfailurereason

#### [coreapplication](/uwp/api/windows.applicationmodel.core.coreapplication)

coreapplication.requestrestartasync <br> coreapplication.requestrestartforuserasync

#### [coreapplicationview](/uwp/api/windows.applicationmodel.core.coreapplicationview)

coreapplicationview.dispatcherqueue

### [windows.applicationmodel.datatransfer.sharetarget](/uwp/api/windows.applicationmodel.datatransfer.sharetarget)

#### [shareoperation](/uwp/api/windows.applicationmodel.datatransfer.sharetarget.shareoperation)

shareoperation.contacts

### [windows.applicationmodel.datatransfer](/uwp/api/windows.applicationmodel.datatransfer)

#### [datatransfermanager](/uwp/api/windows.applicationmodel.datatransfer.datatransfermanager)

datatransfermanager.showshareui

#### [shareuioptions](/uwp/api/windows.applicationmodel.datatransfer.shareuioptions)

shareuioptions <br> shareuioptions.selectionrect <br> shareuioptions.shareuioptions <br> shareuioptions.theme

#### [shareuitheme](/uwp/api/windows.applicationmodel.datatransfer.shareuitheme)

shareuitheme

### [windows.applicationmodel.email](/uwp/api/windows.applicationmodel.email)

#### [emailmailbox](/uwp/api/windows.applicationmodel.email.emailmailbox)

emailmailbox.getchangetracker

### [windows.applicationmodel.payments.provider](/uwp/api/windows.applicationmodel.payments.provider)

#### [paymentappcanmakepaymenttriggerdetails](/uwp/api/windows.applicationmodel.payments.provider.paymentappcanmakepaymenttriggerdetails)

paymentappcanmakepaymenttriggerdetails <br> paymentappcanmakepaymenttriggerdetails.reportcanmakepaymentresult <br> paymentappcanmakepaymenttriggerdetails.request

### [windows.applicationmodel.payments](/uwp/api/windows.applicationmodel.payments)

#### [paymentcanmakepaymentresult](/uwp/api/windows.applicationmodel.payments.paymentcanmakepaymentresult)

paymentcanmakepaymentresult <br> paymentcanmakepaymentresult.paymentcanmakepaymentresult <br> paymentcanmakepaymentresult.status

#### [paymentcanmakepaymentresultstatus](/uwp/api/windows.applicationmodel.payments.paymentcanmakepaymentresultstatus)

paymentcanmakepaymentresultstatus

#### [paymentmediator](/uwp/api/windows.applicationmodel.payments.paymentmediator)

paymentmediator.canmakepaymentasync

#### [paymentrequest](/uwp/api/windows.applicationmodel.payments.paymentrequest)

paymentrequest.id <br> paymentrequest.paymentrequest

### [windows.applicationmodel.useractivities.core](/uwp/api/windows.applicationmodel.useractivities.core)

#### [coreuseractivitymanager](/uwp/api/windows.applicationmodel.useractivities.core.coreuseractivitymanager)

coreuseractivitymanager <br> coreuseractivitymanager.createuseractivitysessioninbackground <br> coreuseractivitymanager.deleteuseractivitysessionsintimerangeasync

### [windows.applicationmodel.useractivities](/uwp/api/windows.applicationmodel.useractivities)

#### [iuseractivitycontentinfo](/uwp/api/windows.applicationmodel.useractivities.iuseractivitycontentinfo)

iuseractivitycontentinfo <br> iuseractivitycontentinfo.tojson

#### [useractivity](/uwp/api/windows.applicationmodel.useractivities.useractivity)

useractivity <br> useractivity.activationuri <br> useractivity.activityid <br> useractivity.contentinfo <br> useractivity.contenttype <br> useractivity.contenturi <br> useractivity.createsession <br> useractivity.fallbackuri <br> useractivity.saveasync <br> useractivity.state <br> useractivity.visualelements

#### [useractivityattribution](/uwp/api/windows.applicationmodel.useractivities.useractivityattribution)

useractivityattribution <br> useractivityattribution.addimagequery <br> useractivityattribution.alternatetext <br> useractivityattribution.iconuri <br> useractivityattribution.useractivityattribution <br> useractivityattribution.useractivityattribution

#### [useractivitychannel](/uwp/api/windows.applicationmodel.useractivities.useractivitychannel)

useractivitychannel <br> useractivitychannel.deleteactivityasync <br> useractivitychannel.deleteallactivitiesasync <br> useractivitychannel.getdefault <br> useractivitychannel.getorcreateuseractivityasync

#### [useractivitycontentinfo](/uwp/api/windows.applicationmodel.useractivities.useractivitycontentinfo)

useractivitycontentinfo <br> useractivitycontentinfo.fromjson <br> useractivitycontentinfo.tojson

#### [useractivitysession](/uwp/api/windows.applicationmodel.useractivities.useractivitysession)

useractivitysession <br> useractivitysession.activityid <br> useractivitysession.close

#### [useractivitystate](/uwp/api/windows.applicationmodel.useractivities.useractivitystate)

useractivitystate

#### [useractivityvisualelements](/uwp/api/windows.applicationmodel.useractivities.useractivityvisualelements)

useractivityvisualelements <br> useractivityvisualelements.attribution <br> useractivityvisualelements.backgroundcolor <br> useractivityvisualelements.content <br> useractivityvisualelements.description <br> useractivityvisualelements.displaytext

### [windows.applicationmodel](/uwp/api/windows.applicationmodel)

#### [designmode](/uwp/api/windows.applicationmodel.designmode)

designmode.designmode2enabled

#### [packagecatalog](/uwp/api/windows.applicationmodel.packagecatalog)

packagecatalog.removeoptionalpackagesasync

#### [packagecatalogremoveoptionalpackagesresult](/uwp/api/windows.applicationmodel.packagecatalogremoveoptionalpackagesresult)

packagecatalogremoveoptionalpackagesresult <br> packagecatalogremoveoptionalpackagesresult.extendederror <br> packagecatalogremoveoptionalpackagesresult.packagesremoved

## windows.devices

### [windows.devices.bluetooth.genericattributeprofile](/uwp/api/windows.devices.bluetooth.genericattributeprofile)

#### [gattclientnotificationresult](/uwp/api/windows.devices.bluetooth.genericattributeprofile.gattclientnotificationresult)

gattclientnotificationresult.bytessent

### [windows.devices.bluetooth](/uwp/api/windows.devices.bluetooth)

#### [bluetoothdevice](/uwp/api/windows.devices.bluetooth.bluetoothdevice)

bluetoothdevice.bluetoothdeviceid

#### [bluetoothdeviceid](/uwp/api/windows.devices.bluetooth.bluetoothdeviceid)

bluetoothdeviceid.fromid

#### [bluetoothledevice](/uwp/api/windows.devices.bluetooth.bluetoothledevice)

bluetoothledevice.bluetoothdeviceid

### [windows.devices.geolocation](/uwp/api/windows.devices.geolocation)

#### [geovisit](/uwp/api/windows.devices.geolocation.geovisit)

geovisit <br> geovisit.position <br> geovisit.statechange <br> geovisit.timestamp

#### [geovisitmonitor](/uwp/api/windows.devices.geolocation.geovisitmonitor)

geovisitmonitor <br> geovisitmonitor.geovisitmonitor <br> geovisitmonitor.getlastreportasync <br> geovisitmonitor.monitoringscope <br> geovisitmonitor.start <br> geovisitmonitor.stop <br> geovisitmonitor.visitstatechanged

#### [geovisitstatechangedeventargs](/uwp/api/windows.devices.geolocation.geovisitstatechangedeventargs)

geovisitstatechangedeventargs <br> geovisitstatechangedeventargs.visit

#### [geovisittriggerdetails](/uwp/api/windows.devices.geolocation.geovisittriggerdetails)

geovisittriggerdetails <br> geovisittriggerdetails.readreports

#### [visitmonitoringscope](/uwp/api/windows.devices.geolocation.visitmonitoringscope)

visitmonitoringscope

#### [visitstatechange](/uwp/api/windows.devices.geolocation.visitstatechange)

visitstatechange

### [windows.devices.pointofservice](/uwp/api/windows.devices.pointofservice)

#### [claimedlinedisplay](/uwp/api/windows.devices.pointofservice.claimedlinedisplay)

claimedlinedisplay.checkhealthasync <br> claimedlinedisplay.checkpowerstatusasync <br> claimedlinedisplay.customglyphs <br> claimedlinedisplay.getattributes <br> claimedlinedisplay.getstatisticsasync <br> claimedlinedisplay.maxbitmapsizeinpixels <br> claimedlinedisplay.statusupdated <br> claimedlinedisplay.supportedcharactersets <br> claimedlinedisplay.supportedscreensizesincharacters <br> claimedlinedisplay.trycleardescriptorsasync <br> claimedlinedisplay.trycreatewindowasync <br> claimedlinedisplay.trysetdescriptorasync <br> claimedlinedisplay.trystorestoragefilebitmapasync <br> claimedlinedisplay.trystorestoragefilebitmapasync <br> claimedlinedisplay.trystorestoragefilebitmapasync <br> claimedlinedisplay.tryupdateattributesasync

#### [linedisplay](/uwp/api/windows.devices.pointofservice.linedisplay)

linedisplay.checkpowerstatusasync <br> linedisplay.statisticscategoryselector

#### [linedisplayattributes](/uwp/api/windows.devices.pointofservice.linedisplayattributes)

linedisplayattributes <br> linedisplayattributes.blinkrate <br> linedisplayattributes.brightness <br> linedisplayattributes.characterset <br> linedisplayattributes.currentwindow <br> linedisplayattributes.ischaractersetmappingenabled <br> linedisplayattributes.ispowernotifyenabled <br> linedisplayattributes.screensizeincharacters

#### [linedisplaycursor](/uwp/api/windows.devices.pointofservice.linedisplaycursor)

linedisplaycursor <br> linedisplaycursor.cancustomize <br> linedisplaycursor.getattributes <br> linedisplaycursor.isblinksupported <br> linedisplaycursor.isblocksupported <br> linedisplaycursor.ishalfblocksupported <br> linedisplaycursor.isothersupported <br> linedisplaycursor.isreversesupported <br> linedisplaycursor.isunderlinesupported <br> linedisplaycursor.tryupdateattributesasync

#### [linedisplaycursorattributes](/uwp/api/windows.devices.pointofservice.linedisplaycursorattributes)

linedisplaycursorattributes <br> linedisplaycursorattributes.cursortype <br> linedisplaycursorattributes.isautoadvanceenabled <br> linedisplaycursorattributes.isblinkenabled <br> linedisplaycursorattributes.position

#### [linedisplaycursortype](/uwp/api/windows.devices.pointofservice.linedisplaycursortype)

linedisplaycursortype

#### [linedisplaycustomglyphs](/uwp/api/windows.devices.pointofservice.linedisplaycustomglyphs)

linedisplaycustomglyphs <br> linedisplaycustomglyphs.sizeinpixels <br> linedisplaycustomglyphs.supportedglyphcodes <br> linedisplaycustomglyphs.tryredefineasync

#### [linedisplaydescriptorstate](/uwp/api/windows.devices.pointofservice.linedisplaydescriptorstate)

linedisplaydescriptorstate

#### [linedisplayhorizontalalignment](/uwp/api/windows.devices.pointofservice.linedisplayhorizontalalignment)

linedisplayhorizontalalignment

#### [linedisplaymarquee](/uwp/api/windows.devices.pointofservice.linedisplaymarquee)

linedisplaymarquee <br> linedisplaymarquee.format <br> linedisplaymarquee.repeatwaitinterval <br> linedisplaymarquee.scrollwaitinterval <br> linedisplaymarquee.trystartscrollingasync <br> linedisplaymarquee.trystopscrollingasync

#### [linedisplaymarqueeformat](/uwp/api/windows.devices.pointofservice.linedisplaymarqueeformat)

linedisplaymarqueeformat

#### [linedisplaypowerstatus](/uwp/api/windows.devices.pointofservice.linedisplaypowerstatus)

linedisplaypowerstatus

#### [linedisplaystatisticscategoryselector](/uwp/api/windows.devices.pointofservice.linedisplaystatisticscategoryselector)

linedisplaystatisticscategoryselector <br> linedisplaystatisticscategoryselector.allstatistics <br> linedisplaystatisticscategoryselector.manufacturerstatistics <br> linedisplaystatisticscategoryselector.unifiedposstatistics

#### [linedisplaystatusupdatedeventargs](/uwp/api/windows.devices.pointofservice.linedisplaystatusupdatedeventargs)

linedisplaystatusupdatedeventargs <br> linedisplaystatusupdatedeventargs.status

#### [linedisplaystoredbitmap](/uwp/api/windows.devices.pointofservice.linedisplaystoredbitmap)

linedisplaystoredbitmap <br> linedisplaystoredbitmap.escapesequence <br> linedisplaystoredbitmap.trydeleteasync

#### [linedisplayverticalalignment](/uwp/api/windows.devices.pointofservice.linedisplayverticalalignment)

linedisplayverticalalignment

#### [linedisplaywindow](/uwp/api/windows.devices.pointofservice.linedisplaywindow)

linedisplaywindow.cursor <br> linedisplaywindow.marquee <br> linedisplaywindow.readcharacteratcursorasync <br> linedisplaywindow.trydisplaystoragefilebitmapatcursorasync <br> linedisplaywindow.trydisplaystoragefilebitmapatcursorasync <br> linedisplaywindow.trydisplaystoragefilebitmapatcursorasync <br> linedisplaywindow.trydisplaystoragefilebitmapatpointasync <br> linedisplaywindow.trydisplaystoragefilebitmapatpointasync <br> linedisplaywindow.trydisplaystoredbitmapatcursorasync

### [windows.devices.sensors.custom](/uwp/api/windows.devices.sensors.custom)

#### [customsensor](/uwp/api/windows.devices.sensors.custom.customsensor)

customsensor.maxbatchsize <br> customsensor.reportlatency

#### [customsensorreading](/uwp/api/windows.devices.sensors.custom.customsensorreading)

customsensorreading.performancecount

### [windows.devices.sensors](/uwp/api/windows.devices.sensors)

#### [accelerometer](/uwp/api/windows.devices.sensors.accelerometer)

accelerometer.fromidasync <br> accelerometer.getdeviceselector

#### [accelerometerreading](/uwp/api/windows.devices.sensors.accelerometerreading)

accelerometerreading.performancecount <br> accelerometerreading.properties

#### [altimeter](/uwp/api/windows.devices.sensors.altimeter)

altimeter.maxbatchsize <br> altimeter.reportlatency

#### [altimeterreading](/uwp/api/windows.devices.sensors.altimeterreading)

altimeterreading.performancecount <br> altimeterreading.properties

#### [barometer](/uwp/api/windows.devices.sensors.barometer)

barometer.fromidasync <br> barometer.getdeviceselector <br> barometer.maxbatchsize <br> barometer.reportlatency

#### [barometerreading](/uwp/api/windows.devices.sensors.barometerreading)

barometerreading.performancecount <br> barometerreading.properties

#### [compass](/uwp/api/windows.devices.sensors.compass)

compass.fromidasync <br> compass.getdeviceselector <br> compass.maxbatchsize <br> compass.reportlatency

#### [compassreading](/uwp/api/windows.devices.sensors.compassreading)

compassreading.performancecount <br> compassreading.properties

#### [gyrometer](/uwp/api/windows.devices.sensors.gyrometer)

gyrometer.fromidasync <br> gyrometer.getdeviceselector <br> gyrometer.maxbatchsize <br> gyrometer.reportlatency

#### [gyrometerreading](/uwp/api/windows.devices.sensors.gyrometerreading)

gyrometerreading.performancecount <br> gyrometerreading.properties

#### [inclinometer](/uwp/api/windows.devices.sensors.inclinometer)

inclinometer.fromidasync <br> inclinometer.getdeviceselector <br> inclinometer.maxbatchsize <br> inclinometer.reportlatency

#### [inclinometerreading](/uwp/api/windows.devices.sensors.inclinometerreading)

inclinometerreading.performancecount <br> inclinometerreading.properties

#### [lightsensor](/uwp/api/windows.devices.sensors.lightsensor)

lightsensor.fromidasync <br> lightsensor.getdeviceselector <br> lightsensor.maxbatchsize <br> lightsensor.reportlatency

#### [lightsensorreading](/uwp/api/windows.devices.sensors.lightsensorreading)

lightsensorreading.performancecount <br> lightsensorreading.properties

#### [magnetometer](/uwp/api/windows.devices.sensors.magnetometer)

magnetometer.fromidasync <br> magnetometer.getdeviceselector <br> magnetometer.maxbatchsize <br> magnetometer.reportlatency

#### [magnetometerreading](/uwp/api/windows.devices.sensors.magnetometerreading)

magnetometerreading.performancecount <br> magnetometerreading.properties

#### [orientationsensor](/uwp/api/windows.devices.sensors.orientationsensor)

orientationsensor.fromidasync <br> orientationsensor.getdeviceselector <br> orientationsensor.getdeviceselector <br> orientationsensor.maxbatchsize <br> orientationsensor.reportlatency

#### [orientationsensorreading](/uwp/api/windows.devices.sensors.orientationsensorreading)

orientationsensorreading.performancecount <br> orientationsensorreading.properties

### [windows.devices.smartcards](/uwp/api/windows.devices.smartcards)

#### [smartcardcryptogramgenerator](/uwp/api/windows.devices.smartcards.smartcardcryptogramgenerator)

smartcardcryptogramgenerator.issupported

#### [smartcardemulator](/uwp/api/windows.devices.smartcards.smartcardemulator)

smartcardemulator.issupported

### [windows.devices.wifi](/uwp/api/windows.devices.wifi)

#### [wifiadapter](/uwp/api/windows.devices.wifi.wifiadapter)

wifiadapter.connectasync <br> wifiadapter.getwpsconfigurationasync

#### [wificonnectionmethod](/uwp/api/windows.devices.wifi.wificonnectionmethod)

wificonnectionmethod

#### [wifiwpsconfigurationresult](/uwp/api/windows.devices.wifi.wifiwpsconfigurationresult)

wifiwpsconfigurationresult <br> wifiwpsconfigurationresult.status <br> wifiwpsconfigurationresult.supportedwpskinds

#### [wifiwpsconfigurationstatus](/uwp/api/windows.devices.wifi.wifiwpsconfigurationstatus)

wifiwpsconfigurationstatus

#### [wifiwpskind](/uwp/api/windows.devices.wifi.wifiwpskind)

wifiwpskind

## windows.gaming

### [windows.gaming.input](/uwp/api/windows.gaming.input)

#### [rawgamecontroller](/uwp/api/windows.gaming.input.rawgamecontroller)

rawgamecontroller.displayname <br> rawgamecontroller.nonroamableid <br> rawgamecontroller.simplehapticscontrollers

### [windows.gaming.preview.gamesenumeration](/uwp/api/windows.gaming.preview.gamesenumeration)

#### [gamelist](/uwp/api/windows.gaming.preview.gamesenumeration.gamelist)

gamelist.mergeentriesasync <br> gamelist.unmergeentryasync

#### [gamelistentry](/uwp/api/windows.gaming.preview.gamesenumeration.gamelistentry)

gamelistentry.gamemodeconfiguration <br> gamelistentry.launchablestate <br> gamelistentry.launcherexecutable <br> gamelistentry.launchparameters <br> gamelistentry.setlauncherexecutablefileasync <br> gamelistentry.setlauncherexecutablefileasync <br> gamelistentry.settitleidasync <br> gamelistentry.titleid

#### [gamelistentrylaunchablestate](/uwp/api/windows.gaming.preview.gamesenumeration.gamelistentrylaunchablestate)

gamelistentrylaunchablestate

#### [gamemodeconfiguration](/uwp/api/windows.gaming.preview.gamesenumeration.gamemodeconfiguration)

gamemodeconfiguration <br> gamemodeconfiguration.affinitizetoexclusivecpus <br> gamemodeconfiguration.cpuexclusivitymaskhigh <br> gamemodeconfiguration.cpuexclusivitymasklow <br> gamemodeconfiguration.isenabled <br> gamemodeconfiguration.maxcpucount <br> gamemodeconfiguration.percentgpumemoryallocatedtogame <br> gamemodeconfiguration.percentgpumemoryallocatedtosystemcompositor <br> gamemodeconfiguration.percentgputimeallocatedtogame <br> gamemodeconfiguration.relatedprocessnames <br> gamemodeconfiguration.saveasync

#### [gamemodeuserconfiguration](/uwp/api/windows.gaming.preview.gamesenumeration.gamemodeuserconfiguration)

gamemodeuserconfiguration <br> gamemodeuserconfiguration.gamingrelatedprocessnames <br> gamemodeuserconfiguration.getdefault <br> gamemodeuserconfiguration.saveasync

### [windows.gaming.ui](/uwp/api/windows.gaming.ui)

#### [gamechatmessageorigin](/uwp/api/windows.gaming.ui.gamechatmessageorigin)

gamechatmessageorigin

#### [gamechatmessagereceivedeventargs](/uwp/api/windows.gaming.ui.gamechatmessagereceivedeventargs)

gamechatmessagereceivedeventargs <br> gamechatmessagereceivedeventargs.appdisplayname <br> gamechatmessagereceivedeventargs.appid <br> gamechatmessagereceivedeventargs.message <br> gamechatmessagereceivedeventargs.origin <br> gamechatmessagereceivedeventargs.sendername

#### [gamechatoverlay](/uwp/api/windows.gaming.ui.gamechatoverlay)

gamechatoverlay <br> gamechatoverlay.addmessage <br> gamechatoverlay.desiredposition <br> gamechatoverlay.getdefault

#### [gamechatoverlaymessagesource](/uwp/api/windows.gaming.ui.gamechatoverlaymessagesource)

gamechatoverlaymessagesource <br> gamechatoverlaymessagesource.gamechatoverlaymessagesource <br> gamechatoverlaymessagesource.messagereceived <br> gamechatoverlaymessagesource.setdelaybeforeclosingaftermessagereceived

#### [gamechatoverlayposition](/uwp/api/windows.gaming.ui.gamechatoverlayposition)

gamechatoverlayposition

#### [gamemonitor](/uwp/api/windows.gaming.ui.gamemonitor)

gamemonitor <br> gamemonitor.getdefault <br> gamemonitor.requestpermissionasync

#### [gamemonitoringpermission](/uwp/api/windows.gaming.ui.gamemonitoringpermission)

gamemonitoringpermission

#### [gameuiprovideractivatedeventargs](/uwp/api/windows.gaming.ui.gameuiprovideractivatedeventargs)

gameuiprovideractivatedeventargs <br> gameuiprovideractivatedeventargs.gameuiargs <br> gameuiprovideractivatedeventargs.kind <br> gameuiprovideractivatedeventargs.previousexecutionstate <br> gameuiprovideractivatedeventargs.reportcompleted <br> gameuiprovideractivatedeventargs.splashscreen

## windows.graphics

### [windows.graphics.holographic](/uwp/api/windows.graphics.holographic)

#### [holographiccamera](/uwp/api/windows.graphics.holographic.holographiccamera)

holographiccamera.isprimarylayerenabled <br> holographiccamera.maxquadlayercount <br> holographiccamera.quadlayers

#### [holographiccamerarenderingparameters](/uwp/api/windows.graphics.holographic.holographiccamerarenderingparameters)

holographiccamerarenderingparameters.iscontentprotectionenabled

#### [holographicdisplay](/uwp/api/windows.graphics.holographic.holographicdisplay)

holographicdisplay.refreshrate

#### [holographicframe](/uwp/api/windows.graphics.holographic.holographicframe)

holographicframe.getquadlayerupdateparameters

#### [holographicquadlayer](/uwp/api/windows.graphics.holographic.holographicquadlayer)

holographicquadlayer <br> holographicquadlayer.close <br> holographicquadlayer.holographicquadlayer <br> holographicquadlayer.holographicquadlayer <br> holographicquadlayer.pixelformat <br> holographicquadlayer.size

#### [holographicquadlayerupdateparameters](/uwp/api/windows.graphics.holographic.holographicquadlayerupdateparameters)

holographicquadlayerupdateparameters <br> holographicquadlayerupdateparameters.acquirebuffertoupdatecontent <br> holographicquadlayerupdateparameters.updatecontentprotectionenabled <br> holographicquadlayerupdateparameters.updateextents <br> holographicquadlayerupdateparameters.updatelocationwithdisplayrelativemode <br> holographicquadlayerupdateparameters.updatelocationwithstationarymode <br> holographicquadlayerupdateparameters.updateviewport

#### [holographicspace](/uwp/api/windows.graphics.holographic.holographicspace)

holographicspace.isconfigured

### [windows.graphics.printing.printticket](/uwp/api/windows.graphics.printing.printticket)

#### [printticketcapabilities](/uwp/api/windows.graphics.printing.printticket.printticketcapabilities)

printticketcapabilities <br> printticketcapabilities.documentbindingfeature <br> printticketcapabilities.documentcollatefeature <br> printticketcapabilities.documentduplexfeature <br> printticketcapabilities.documentholepunchfeature <br> printticketcapabilities.documentinputbinfeature <br> printticketcapabilities.documentnupfeature <br> printticketcapabilities.documentstaplefeature <br> printticketcapabilities.getfeature <br> printticketcapabilities.getparameterdefinition <br> printticketcapabilities.jobpasscodefeature <br> printticketcapabilities.name <br> printticketcapabilities.pageborderlessfeature <br> printticketcapabilities.pagemediasizefeature <br> printticketcapabilities.pagemediatypefeature <br> printticketcapabilities.pageorientationfeature <br> printticketcapabilities.pageoutputcolorfeature <br> printticketcapabilities.pageoutputqualityfeature <br> printticketcapabilities.pageresolutionfeature <br> printticketcapabilities.xmlnamespace <br> printticketcapabilities.xmlnode

#### [printticketfeature](/uwp/api/windows.graphics.printing.printticket.printticketfeature)

printticketfeature <br> printticketfeature.displayname <br> printticketfeature.getoption <br> printticketfeature.getselectedoption <br> printticketfeature.name <br> printticketfeature.options <br> printticketfeature.selectiontype <br> printticketfeature.setselectedoption <br> printticketfeature.xmlnamespace <br> printticketfeature.xmlnode

#### [printticketfeatureselectiontype](/uwp/api/windows.graphics.printing.printticket.printticketfeatureselectiontype)

printticketfeatureselectiontype

#### [printticketoption](/uwp/api/windows.graphics.printing.printticket.printticketoption)

printticketoption <br> printticketoption.displayname <br> printticketoption.getpropertynode <br> printticketoption.getpropertyvalue <br> printticketoption.getscoredpropertynode <br> printticketoption.getscoredpropertyvalue <br> printticketoption.name <br> printticketoption.xmlnamespace <br> printticketoption.xmlnode

#### [printticketparameterdatatype](/uwp/api/windows.graphics.printing.printticket.printticketparameterdatatype)

printticketparameterdatatype

#### [printticketparameterdefinition](/uwp/api/windows.graphics.printing.printticket.printticketparameterdefinition)

printticketparameterdefinition <br> printticketparameterdefinition.datatype <br> printticketparameterdefinition.name <br> printticketparameterdefinition.rangemax <br> printticketparameterdefinition.rangemin <br> printticketparameterdefinition.unittype <br> printticketparameterdefinition.xmlnamespace <br> printticketparameterdefinition.xmlnode

#### [printticketparameterinitializer](/uwp/api/windows.graphics.printing.printticket.printticketparameterinitializer)

printticketparameterinitializer <br> printticketparameterinitializer.name <br> printticketparameterinitializer.value <br> printticketparameterinitializer.xmlnamespace <br> printticketparameterinitializer.xmlnode

#### [printticketvalue](/uwp/api/windows.graphics.printing.printticket.printticketvalue)

printticketvalue <br> printticketvalue.getvalueasinteger <br> printticketvalue.getvalueasstring <br> printticketvalue.type

#### [printticketvaluetype](/uwp/api/windows.graphics.printing.printticket.printticketvaluetype)

printticketvaluetype

#### [workflowprintticket](/uwp/api/windows.graphics.printing.printticket.workflowprintticket)

workflowprintticket <br> workflowprintticket.documentbindingfeature <br> workflowprintticket.documentcollatefeature <br> workflowprintticket.documentduplexfeature <br> workflowprintticket.documentholepunchfeature <br> workflowprintticket.documentinputbinfeature <br> workflowprintticket.documentnupfeature <br> workflowprintticket.documentstaplefeature <br> workflowprintticket.getcapabilities <br> workflowprintticket.getfeature <br> workflowprintticket.getparameterinitializer <br> workflowprintticket.jobpasscodefeature <br> workflowprintticket.mergeandvalidateticket <br> workflowprintticket.name <br> workflowprintticket.notifyxmlchangedasync <br> workflowprintticket.pageborderlessfeature <br> workflowprintticket.pagemediasizefeature <br> workflowprintticket.pagemediatypefeature <br> workflowprintticket.pageorientationfeature <br> workflowprintticket.pageoutputcolorfeature <br> workflowprintticket.pageoutputqualityfeature <br> workflowprintticket.pageresolutionfeature <br> workflowprintticket.setparameterinitializerasinteger <br> workflowprintticket.setparameterinitializerasstring <br> workflowprintticket.validateasync <br> workflowprintticket.xmlnamespace <br> workflowprintticket.xmlnode

#### [workflowprintticketvalidationresult](/uwp/api/windows.graphics.printing.printticket.workflowprintticketvalidationresult)

workflowprintticketvalidationresult <br> workflowprintticketvalidationresult.extendederror <br> workflowprintticketvalidationresult.validated

### [windows.graphics.printing.workflow](/uwp/api/windows.graphics.printing.workflow)

#### [printworkflowbackgroundsession](/uwp/api/windows.graphics.printing.workflow.printworkflowbackgroundsession)

printworkflowbackgroundsession <br> printworkflowbackgroundsession.setuprequested <br> printworkflowbackgroundsession.start <br> printworkflowbackgroundsession.status <br> printworkflowbackgroundsession.submitted

#### [printworkflowbackgroundsetuprequestedeventargs](/uwp/api/windows.graphics.printing.workflow.printworkflowbackgroundsetuprequestedeventargs)

printworkflowbackgroundsetuprequestedeventargs <br> printworkflowbackgroundsetuprequestedeventargs.configuration <br> printworkflowbackgroundsetuprequestedeventargs.getdeferral <br> printworkflowbackgroundsetuprequestedeventargs.getuserprintticketasync <br> printworkflowbackgroundsetuprequestedeventargs.setrequiresui

#### [printworkflowconfiguration](/uwp/api/windows.graphics.printing.workflow.printworkflowconfiguration)

printworkflowconfiguration <br> printworkflowconfiguration.jobtitle <br> printworkflowconfiguration.sessionid <br> printworkflowconfiguration.sourceappdisplayname

#### [printworkflowforegroundsession](/uwp/api/windows.graphics.printing.workflow.printworkflowforegroundsession)

printworkflowforegroundsession <br> printworkflowforegroundsession.setuprequested <br> printworkflowforegroundsession.start <br> printworkflowforegroundsession.status <br> printworkflowforegroundsession.xpsdataavailable

#### [printworkflowforegroundsetuprequestedeventargs](/uwp/api/windows.graphics.printing.workflow.printworkflowforegroundsetuprequestedeventargs)

printworkflowforegroundsetuprequestedeventargs <br> printworkflowforegroundsetuprequestedeventargs.configuration <br> printworkflowforegroundsetuprequestedeventargs.getdeferral <br> printworkflowforegroundsetuprequestedeventargs.getuserprintticketasync

#### [printworkflowobjectmodelsourcefilecontent](/uwp/api/windows.graphics.printing.workflow.printworkflowobjectmodelsourcefilecontent)

printworkflowobjectmodelsourcefilecontent

#### [printworkflowobjectmodeltargetpackage](/uwp/api/windows.graphics.printing.workflow.printworkflowobjectmodeltargetpackage)

printworkflowobjectmodeltargetpackage

#### [printworkflowsessionstatus](/uwp/api/windows.graphics.printing.workflow.printworkflowsessionstatus)

printworkflowsessionstatus

#### [printworkflowsourcecontent](/uwp/api/windows.graphics.printing.workflow.printworkflowsourcecontent)

printworkflowsourcecontent <br> printworkflowsourcecontent.getjobprintticketasync <br> printworkflowsourcecontent.getsourcespooldataasstreamcontent <br> printworkflowsourcecontent.getsourcespooldataasxpsobjectmodel

#### [printworkflowspoolstreamcontent](/uwp/api/windows.graphics.printing.workflow.printworkflowspoolstreamcontent)

printworkflowspoolstreamcontent <br> printworkflowspoolstreamcontent.getinputstream

#### [printworkflowstreamtarget](/uwp/api/windows.graphics.printing.workflow.printworkflowstreamtarget)

printworkflowstreamtarget <br> printworkflowstreamtarget.getoutputstream

#### [printworkflowsubmittedeventargs](/uwp/api/windows.graphics.printing.workflow.printworkflowsubmittedeventargs)

printworkflowsubmittedeventargs <br> printworkflowsubmittedeventargs.getdeferral <br> printworkflowsubmittedeventargs.gettarget <br> printworkflowsubmittedeventargs.operation

#### [printworkflowsubmittedoperation](/uwp/api/windows.graphics.printing.workflow.printworkflowsubmittedoperation)

printworkflowsubmittedoperation <br> printworkflowsubmittedoperation.complete <br> printworkflowsubmittedoperation.configuration <br> printworkflowsubmittedoperation.xpscontent

#### [printworkflowsubmittedstatus](/uwp/api/windows.graphics.printing.workflow.printworkflowsubmittedstatus)

printworkflowsubmittedstatus

#### [printworkflowtarget](/uwp/api/windows.graphics.printing.workflow.printworkflowtarget)

printworkflowtarget <br> printworkflowtarget.targetasstream <br> printworkflowtarget.targetasxpsobjectmodelpackage

#### [printworkflowtriggerdetails](/uwp/api/windows.graphics.printing.workflow.printworkflowtriggerdetails)

printworkflowtriggerdetails <br> printworkflowtriggerdetails.printworkflowsession

#### [printworkflowuiactivatedeventargs](/uwp/api/windows.graphics.printing.workflow.printworkflowuiactivatedeventargs)

printworkflowuiactivatedeventargs <br> printworkflowuiactivatedeventargs.kind <br> printworkflowuiactivatedeventargs.previousexecutionstate <br> printworkflowuiactivatedeventargs.printworkflowsession <br> printworkflowuiactivatedeventargs.splashscreen <br> printworkflowuiactivatedeventargs.user

#### [printworkflowxpsdataavailableeventargs](/uwp/api/windows.graphics.printing.workflow.printworkflowxpsdataavailableeventargs)

printworkflowxpsdataavailableeventargs <br> printworkflowxpsdataavailableeventargs.getdeferral <br> printworkflowxpsdataavailableeventargs.operation

### [windows.graphics.printing3d](/uwp/api/windows.graphics.printing3d)

#### [printing3d3mfpackage](/uwp/api/windows.graphics.printing3d.printing3d3mfpackage)

printing3d3mfpackage.compression

#### [printing3dpackagecompression](/uwp/api/windows.graphics.printing3d.printing3dpackagecompression)

printing3dpackagecompression

## windows.management

### [windows.management.deployment](/uwp/api/windows.management.deployment)

#### [addpackagebyappinstalleroptions](/uwp/api/windows.management.deployment.addpackagebyappinstalleroptions)

addpackagebyappinstalleroptions

#### [packagemanager](/uwp/api/windows.management.deployment.packagemanager)

packagemanager.addpackageasync <br> packagemanager.addpackagebyappinstallerfileasync <br> packagemanager.provisionpackageforallusersasync <br> packagemanager.requestaddpackageasync <br> packagemanager.requestaddpackagebyappinstallerfileasync <br> packagemanager.stagepackageasync

## windows.media

### [windows.media.appbroadcasting](/uwp/api/windows.media.appbroadcasting)

#### [appbroadcastingmonitor](/uwp/api/windows.media.appbroadcasting.appbroadcastingmonitor)

appbroadcastingmonitor <br> appbroadcastingmonitor.appbroadcastingmonitor <br> appbroadcastingmonitor.iscurrentappbroadcasting <br> appbroadcastingmonitor.iscurrentappbroadcastingchanged

#### [appbroadcastingstatus](/uwp/api/windows.media.appbroadcasting.appbroadcastingstatus)

appbroadcastingstatus <br> appbroadcastingstatus.canstartbroadcast <br> appbroadcastingstatus.details

#### [appbroadcastingstatusdetails](/uwp/api/windows.media.appbroadcasting.appbroadcastingstatusdetails)

appbroadcastingstatusdetails <br> appbroadcastingstatusdetails.isanyappbroadcasting <br> appbroadcastingstatusdetails.isappinactive <br> appbroadcastingstatusdetails.isblockedforapp <br> appbroadcastingstatusdetails.iscaptureresourceunavailable <br> appbroadcastingstatusdetails.isdisabledbysystem <br> appbroadcastingstatusdetails.isdisabledbyuser <br> appbroadcastingstatusdetails.isgamestreaminprogress <br> appbroadcastingstatusdetails.isgpuconstrained

#### [appbroadcastingui](/uwp/api/windows.media.appbroadcasting.appbroadcastingui)

appbroadcastingui <br> appbroadcastingui.getdefault <br> appbroadcastingui.getforuser <br> appbroadcastingui.getstatus <br> appbroadcastingui.showbroadcastui

### [windows.media.apprecording](/uwp/api/windows.media.apprecording)

#### [apprecordingmanager](/uwp/api/windows.media.apprecording.apprecordingmanager)

apprecordingmanager <br> apprecordingmanager.getdefault <br> apprecordingmanager.getstatus <br> apprecordingmanager.recordtimespantofileasync <br> apprecordingmanager.savescreenshottofilesasync <br> apprecordingmanager.startrecordingtofileasync <br> apprecordingmanager.supportedscreenshotmediaencodingsubtypes

#### [apprecordingresult](/uwp/api/windows.media.apprecording.apprecordingresult)

apprecordingresult <br> apprecordingresult.duration <br> apprecordingresult.extendederror <br> apprecordingresult.isfiletruncated <br> apprecordingresult.succeeded

#### [apprecordingsavedscreenshotinfo](/uwp/api/windows.media.apprecording.apprecordingsavedscreenshotinfo)

apprecordingsavedscreenshotinfo <br> apprecordingsavedscreenshotinfo.file <br> apprecordingsavedscreenshotinfo.mediaencodingsubtype

#### [apprecordingsavescreenshotoption](/uwp/api/windows.media.apprecording.apprecordingsavescreenshotoption)

apprecordingsavescreenshotoption

#### [apprecordingsavescreenshotresult](/uwp/api/windows.media.apprecording.apprecordingsavescreenshotresult)

apprecordingsavescreenshotresult <br> apprecordingsavescreenshotresult.extendederror <br> apprecordingsavescreenshotresult.savedscreenshotinfos <br> apprecordingsavescreenshotresult.succeeded

#### [apprecordingstatus](/uwp/api/windows.media.apprecording.apprecordingstatus)

apprecordingstatus <br> apprecordingstatus.canrecord <br> apprecordingstatus.canrecordtimespan <br> apprecordingstatus.details <br> apprecordingstatus.historicalbufferduration

#### [apprecordingstatusdetails](/uwp/api/windows.media.apprecording.apprecordingstatusdetails)

apprecordingstatusdetails <br> apprecordingstatusdetails.isanyappbroadcasting <br> apprecordingstatusdetails.isappinactive <br> apprecordingstatusdetails.isblockedforapp <br> apprecordingstatusdetails.iscaptureresourceunavailable <br> apprecordingstatusdetails.isdisabledbysystem <br> apprecordingstatusdetails.isdisabledbyuser <br> apprecordingstatusdetails.isgamestreaminprogress <br> apprecordingstatusdetails.isgpuconstrained <br> apprecordingstatusdetails.istimespanrecordingdisabled

### [windows.media.capture.frames](/uwp/api/windows.media.capture.frames)

#### [mediaframereader](/uwp/api/windows.media.capture.frames.mediaframereader)

mediaframereader.acquisitionmode

#### [mediaframereaderacquisitionmode](/uwp/api/windows.media.capture.frames.mediaframereaderacquisitionmode)

mediaframereaderacquisitionmode

#### [multisourcemediaframereader](/uwp/api/windows.media.capture.frames.multisourcemediaframereader)

multisourcemediaframereader.acquisitionmode

### [windows.media.capture](/uwp/api/windows.media.capture)

#### [appbroadcastbackgroundservice](/uwp/api/windows.media.capture.appbroadcastbackgroundservice)

appbroadcastbackgroundservice.broadcastchannel <br> appbroadcastbackgroundservice.broadcastchannelchanged <br> appbroadcastbackgroundservice.broadcastlanguage <br> appbroadcastbackgroundservice.broadcastlanguagechanged <br> appbroadcastbackgroundservice.broadcasttitlechanged

#### [appbroadcastbackgroundservicesignininfo](/uwp/api/windows.media.capture.appbroadcastbackgroundservicesignininfo)

appbroadcastbackgroundservicesignininfo.usernamechanged

#### [appbroadcastbackgroundservicestreaminfo](/uwp/api/windows.media.capture.appbroadcastbackgroundservicestreaminfo)

appbroadcastbackgroundservicestreaminfo.reportproblemwithstream

#### [appcapture](/uwp/api/windows.media.capture.appcapture)

appcapture.setallowedasync

#### [appcapturemetadatapriority](/uwp/api/windows.media.capture.appcapturemetadatapriority)

appcapturemetadatapriority

#### [appcapturemetadatawriter](/uwp/api/windows.media.capture.appcapturemetadatawriter)

appcapturemetadatawriter <br> appcapturemetadatawriter.adddoubleevent <br> appcapturemetadatawriter.addint32event <br> appcapturemetadatawriter.addstringevent <br> appcapturemetadatawriter.appcapturemetadatawriter <br> appcapturemetadatawriter.close <br> appcapturemetadatawriter.metadatapurged <br> appcapturemetadatawriter.remainingstoragebytesavailable <br> appcapturemetadatawriter.startdoublestate <br> appcapturemetadatawriter.startint32state <br> appcapturemetadatawriter.startstringstate <br> appcapturemetadatawriter.stopallstates <br> appcapturemetadatawriter.stopstate

### [windows.media.core](/uwp/api/windows.media.core)

#### [audiostreamdescriptor](/uwp/api/windows.media.core.audiostreamdescriptor)

audiostreamdescriptor.label

#### [imediastreamdescriptor2](/uwp/api/windows.media.core.imediastreamdescriptor2)

imediastreamdescriptor2 <br> imediastreamdescriptor2.label

#### [initializemediastreamsourcerequestedeventargs](/uwp/api/windows.media.core.initializemediastreamsourcerequestedeventargs)

initializemediastreamsourcerequestedeventargs <br> initializemediastreamsourcerequestedeventargs.getdeferral <br> initializemediastreamsourcerequestedeventargs.randomaccessstream <br> initializemediastreamsourcerequestedeventargs.source

#### [lowlightfusion](/uwp/api/windows.media.core.lowlightfusion)

lowlightfusion <br> lowlightfusion.fuseasync <br> lowlightfusion.maxsupportedframecount <br> lowlightfusion.supportedbitmappixelformats

#### [lowlightfusionresult](/uwp/api/windows.media.core.lowlightfusionresult)

lowlightfusionresult <br> lowlightfusionresult.close <br> lowlightfusionresult.frame

#### [mediasource](/uwp/api/windows.media.core.mediasource)

mediasource.createfrommediaframesource

#### [mediasourceappserviceconnection](/uwp/api/windows.media.core.mediasourceappserviceconnection)

mediasourceappserviceconnection <br> mediasourceappserviceconnection.initializemediastreamsourcerequested <br> mediasourceappserviceconnection.mediasourceappserviceconnection <br> mediasourceappserviceconnection.start

#### [mediastreamsource](/uwp/api/windows.media.core.mediastreamsource)

mediastreamsource.islive

#### [msestreamsource](/uwp/api/windows.media.core.msestreamsource)

msestreamsource.liveseekablerange

#### [sceneanalysiseffectframe](/uwp/api/windows.media.core.sceneanalysiseffectframe)

sceneanalysiseffectframe.analysisrecommendation

#### [sceneanalysisrecommendation](/uwp/api/windows.media.core.sceneanalysisrecommendation)

sceneanalysisrecommendation

#### [videostreamdescriptor](/uwp/api/windows.media.core.videostreamdescriptor)

videostreamdescriptor.label

### [windows.media.dialprotocol](/uwp/api/windows.media.dialprotocol)

#### [dialreceiverapp](/uwp/api/windows.media.dialprotocol.dialreceiverapp)

dialreceiverapp <br> dialreceiverapp.current <br> dialreceiverapp.getadditionaldataasync <br> dialreceiverapp.setadditionaldataasync

### [windows.media.mediaproperties](/uwp/api/windows.media.mediaproperties)

#### [mediaencodingprofile](/uwp/api/windows.media.mediaproperties.mediaencodingprofile)

mediaencodingprofile.getaudiotracks <br> mediaencodingprofile.getvideotracks <br> mediaencodingprofile.setaudiotracks <br> mediaencodingprofile.setvideotracks

### [windows.media.playback](/uwp/api/windows.media.playback)

#### [mediaplaybacksessionbufferingstartedeventargs](/uwp/api/windows.media.playback.mediaplaybacksessionbufferingstartedeventargs)

mediaplaybacksessionbufferingstartedeventargs <br> mediaplaybacksessionbufferingstartedeventargs.isplaybackinterruption

#### [mediaplayer](/uwp/api/windows.media.playback.mediaplayer)

mediaplayer.rendersubtitlestosurface <br> mediaplayer.rendersubtitlestosurface <br> mediaplayer.subtitleframechanged

### [windows.media.speechrecognition](/uwp/api/windows.media.speechrecognition)

#### [speechrecognizer](/uwp/api/windows.media.speechrecognition.speechrecognizer)

speechrecognizer.trysetsystemspeechlanguageasync

### [windows.media.speechsynthesis](/uwp/api/windows.media.speechsynthesis)

#### [speechsynthesizer](/uwp/api/windows.media.speechsynthesis.speechsynthesizer)

speechsynthesizer.trysetdefaultvoiceasync

#### [speechsynthesizeroptions](/uwp/api/windows.media.speechsynthesis.speechsynthesizeroptions)

speechsynthesizeroptions.audiopitch <br> speechsynthesizeroptions.audiovolume <br> speechsynthesizeroptions.speakingrate

### [windows.media.streaming.adaptive](/uwp/api/windows.media.streaming.adaptive)

#### [adaptivemediasourcediagnosticavailableeventargs](/uwp/api/windows.media.streaming.adaptive.adaptivemediasourcediagnosticavailableeventargs)

adaptivemediasourcediagnosticavailableeventargs.extendederror

## windows.networking

### [windows.networking.backgroundtransfer](/uwp/api/windows.networking.backgroundtransfer)

#### [backgroundtransferfilerange](/uwp/api/windows.networking.backgroundtransfer.backgroundtransferfilerange)

backgroundtransferfilerange

#### [backgroundtransferrangesdownloadedeventargs](/uwp/api/windows.networking.backgroundtransfer.backgroundtransferrangesdownloadedeventargs)

backgroundtransferrangesdownloadedeventargs <br> backgroundtransferrangesdownloadedeventargs.addedranges <br> backgroundtransferrangesdownloadedeventargs.getdeferral <br> backgroundtransferrangesdownloadedeventargs.wasdownloadrestarted

#### [downloadoperation](/uwp/api/windows.networking.backgroundtransfer.downloadoperation)

downloadoperation.currentweberrorstatus <br> downloadoperation.getdownloadedranges <br> downloadoperation.getresultrandomaccessstreamreference <br> downloadoperation.israndomaccessrequired <br> downloadoperation.rangesdownloaded <br> downloadoperation.recoverableweberrorstatuses

### [windows.networking.connectivity](/uwp/api/windows.networking.connectivity)

#### [connectionprofile](/uwp/api/windows.networking.connectivity.connectionprofile)

connectionprofile.getprovidernetworkusageasync

#### [providernetworkusage](/uwp/api/windows.networking.connectivity.providernetworkusage)

providernetworkusage <br> providernetworkusage.bytesreceived <br> providernetworkusage.bytessent <br> providernetworkusage.providerid

### [windows.networking.networkoperators](/uwp/api/windows.networking.networkoperators)

#### [mobilebroadbandantennasar](/uwp/api/windows.networking.networkoperators.mobilebroadbandantennasar)

mobilebroadbandantennasar <br> mobilebroadbandantennasar.antennaindex <br> mobilebroadbandantennasar.sarbackoffindex

#### [mobilebroadbandcellcdma](/uwp/api/windows.networking.networkoperators.mobilebroadbandcellcdma)

mobilebroadbandcellcdma <br> mobilebroadbandcellcdma.basestationid <br> mobilebroadbandcellcdma.basestationlastbroadcastgpstime <br> mobilebroadbandcellcdma.basestationlatitude <br> mobilebroadbandcellcdma.basestationlongitude <br> mobilebroadbandcellcdma.basestationpncode <br> mobilebroadbandcellcdma.networkid <br> mobilebroadbandcellcdma.pilotsignalstrengthindb <br> mobilebroadbandcellcdma.systemid

#### [mobilebroadbandcellgsm](/uwp/api/windows.networking.networkoperators.mobilebroadbandcellgsm)

mobilebroadbandcellgsm <br> mobilebroadbandcellgsm.basestationid <br> mobilebroadbandcellgsm.cellid <br> mobilebroadbandcellgsm.channelnumber <br> mobilebroadbandcellgsm.locationareacode <br> mobilebroadbandcellgsm.providerid <br> mobilebroadbandcellgsm.receivedsignalstrengthindbm <br> mobilebroadbandcellgsm.timingadvanceinbitperiods

#### [mobilebroadbandcelllte](/uwp/api/windows.networking.networkoperators.mobilebroadbandcelllte)

mobilebroadbandcelllte <br> mobilebroadbandcelllte.cellid <br> mobilebroadbandcelllte.channelnumber <br> mobilebroadbandcelllte.physicalcellid <br> mobilebroadbandcelllte.providerid <br> mobilebroadbandcelllte.referencesignalreceivedpowerindbm <br> mobilebroadbandcelllte.referencesignalreceivedqualityindbm <br> mobilebroadbandcelllte.timingadvanceinbitperiods <br> mobilebroadbandcelllte.trackingareacode

#### [mobilebroadbandcellsinfo](/uwp/api/windows.networking.networkoperators.mobilebroadbandcellsinfo)

mobilebroadbandcellsinfo <br> mobilebroadbandcellsinfo.neighboringcellscdma <br> mobilebroadbandcellsinfo.neighboringcellsgsm <br> mobilebroadbandcellsinfo.neighboringcellslte <br> mobilebroadbandcellsinfo.neighboringcellstdscdma <br> mobilebroadbandcellsinfo.neighboringcellsumts <br> mobilebroadbandcellsinfo.servingcellscdma <br> mobilebroadbandcellsinfo.servingcellsgsm <br> mobilebroadbandcellsinfo.servingcellslte <br> mobilebroadbandcellsinfo.servingcellstdscdma <br> mobilebroadbandcellsinfo.servingcellsumts

#### [mobilebroadbandcelltdscdma](/uwp/api/windows.networking.networkoperators.mobilebroadbandcelltdscdma)

mobilebroadbandcelltdscdma <br> mobilebroadbandcelltdscdma.cellid <br> mobilebroadbandcelltdscdma.cellparameterid <br> mobilebroadbandcelltdscdma.channelnumber <br> mobilebroadbandcelltdscdma.locationareacode <br> mobilebroadbandcelltdscdma.pathlossindb <br> mobilebroadbandcelltdscdma.providerid <br> mobilebroadbandcelltdscdma.receivedsignalcodepowerindbm <br> mobilebroadbandcelltdscdma.timingadvanceinbitperiods

#### [mobilebroadbandcellumts](/uwp/api/windows.networking.networkoperators.mobilebroadbandcellumts)

mobilebroadbandcellumts <br> mobilebroadbandcellumts.cellid <br> mobilebroadbandcellumts.channelnumber <br> mobilebroadbandcellumts.locationareacode <br> mobilebroadbandcellumts.pathlossindb <br> mobilebroadbandcellumts.primaryscramblingcode <br> mobilebroadbandcellumts.providerid <br> mobilebroadbandcellumts.receivedsignalcodepowerindbm <br> mobilebroadbandcellumts.signaltonoiseratioindb

#### [mobilebroadbandmodem](/uwp/api/windows.networking.networkoperators.mobilebroadbandmodem)

mobilebroadbandmodem.getispassthroughenabledasync <br> mobilebroadbandmodem.setispassthroughenabledasync

#### [mobilebroadbandmodemconfiguration](/uwp/api/windows.networking.networkoperators.mobilebroadbandmodemconfiguration)

mobilebroadbandmodemconfiguration.sarmanager

#### [mobilebroadbandmodemstatus](/uwp/api/windows.networking.networkoperators.mobilebroadbandmodemstatus)

mobilebroadbandmodemstatus

#### [mobilebroadbandnetwork](/uwp/api/windows.networking.networkoperators.mobilebroadbandnetwork)

mobilebroadbandnetwork.getcellsinfoasync

#### [mobilebroadbandsarmanager](/uwp/api/windows.networking.networkoperators.mobilebroadbandsarmanager)

mobilebroadbandsarmanager <br> mobilebroadbandsarmanager.antennas <br> mobilebroadbandsarmanager.disablebackoffasync <br> mobilebroadbandsarmanager.enablebackoffasync <br> mobilebroadbandsarmanager.getistransmittingasync <br> mobilebroadbandsarmanager.hysteresistimerperiod <br> mobilebroadbandsarmanager.isbackoffenabled <br> mobilebroadbandsarmanager.issarcontrolledbyhardware <br> mobilebroadbandsarmanager.iswifihardwareintegrated <br> mobilebroadbandsarmanager.revertsartohardwarecontrolasync <br> mobilebroadbandsarmanager.setconfigurationasync <br> mobilebroadbandsarmanager.settransmissionstatechangedhysteresisasync <br> mobilebroadbandsarmanager.starttransmissionstatemonitoring <br> mobilebroadbandsarmanager.stoptransmissionstatemonitoring <br> mobilebroadbandsarmanager.transmissionstatechanged

#### [mobilebroadbandtransmissionstatechangedeventargs](/uwp/api/windows.networking.networkoperators.mobilebroadbandtransmissionstatechangedeventargs)

mobilebroadbandtransmissionstatechangedeventargs <br> mobilebroadbandtransmissionstatechangedeventargs.istransmitting

### [windows.networking.sockets](/uwp/api/windows.networking.sockets)

#### [messagewebsocketcontrol](/uwp/api/windows.networking.sockets.messagewebsocketcontrol)

messagewebsocketcontrol.actualunsolicitedponginterval <br> messagewebsocketcontrol.clientcertificate <br> messagewebsocketcontrol.desiredunsolicitedponginterval <br> messagewebsocketcontrol.receivemode

#### [messagewebsocketmessagereceivedeventargs](/uwp/api/windows.networking.sockets.messagewebsocketmessagereceivedeventargs)

messagewebsocketmessagereceivedeventargs.ismessagecomplete

#### [messagewebsocketreceivemode](/uwp/api/windows.networking.sockets.messagewebsocketreceivemode)

messagewebsocketreceivemode

#### [streamsocketcontrol](/uwp/api/windows.networking.sockets.streamsocketcontrol)

streamsocketcontrol.minprotectionlevel

#### [streamwebsocketcontrol](/uwp/api/windows.networking.sockets.streamwebsocketcontrol)

streamwebsocketcontrol.actualunsolicitedponginterval <br> streamwebsocketcontrol.clientcertificate <br> streamwebsocketcontrol.desiredunsolicitedponginterval

## windows.phone

### [windows.phone.networking.voip](/uwp/api/windows.phone.networking.voip)

#### [voipcallcoordinator](/uwp/api/windows.phone.networking.voip.voipcallcoordinator)

voipcallcoordinator.setupnewacceptedcall

#### [voipphonecall](/uwp/api/windows.phone.networking.voip.voipphonecall)

voipphonecall.tryshowappui

## windows.security

### [windows.security.authentication.web.provider](/uwp/api/windows.security.authentication.web.provider)

#### [webaccountmanager](/uwp/api/windows.security.authentication.web.provider.webaccountmanager)

webaccountmanager.invalidateappcacheforaccountasync <br> webaccountmanager.invalidateappcacheforallaccountsasync

### [windows.security.enterprisedata](/uwp/api/windows.security.enterprisedata)

#### [fileprotectioninfo](/uwp/api/windows.security.enterprisedata.fileprotectioninfo)

fileprotectioninfo.isprotectwhileopensupported

## windows.services

### [windows.services.maps.guidance](/uwp/api/windows.services.maps.guidance)

#### [guidanceroadsegment](/uwp/api/windows.services.maps.guidance.guidanceroadsegment)

guidanceroadsegment.isscenic

### [windows.services.maps.localsearch](/uwp/api/windows.services.maps.localsearch)

#### [placeinfohelper](/uwp/api/windows.services.maps.localsearch.placeinfohelper)

placeinfohelper <br> placeinfohelper.createfromlocallocation

### [windows.services.maps](/uwp/api/windows.services.maps)

#### [maproute](/uwp/api/windows.services.maps.maproute)

maproute.isscenic

#### [maprouteoptimization](/uwp/api/windows.services.maps.maprouteoptimization)

maprouteoptimization.scenic

#### [placeinfo](/uwp/api/windows.services.maps.placeinfo)

placeinfo <br> placeinfo.create <br> placeinfo.create <br> placeinfo.createfromidentifier <br> placeinfo.createfromidentifier <br> placeinfo.createfrommaplocation <br> placeinfo.displayaddress <br> placeinfo.displayname <br> placeinfo.geoshape <br> placeinfo.identifier <br> placeinfo.isshowsupported <br> placeinfo.show <br> placeinfo.show

#### [placeinfocreateoptions](/uwp/api/windows.services.maps.placeinfocreateoptions)

placeinfocreateoptions <br> placeinfocreateoptions.displayaddress <br> placeinfocreateoptions.displayname <br> placeinfocreateoptions.placeinfocreateoptions

## windows.storage

### [windows.storage.provider](/uwp/api/windows.storage.provider)

#### [storageproviderhydrationpolicy](/uwp/api/windows.storage.provider.storageproviderhydrationpolicy)

storageproviderhydrationpolicy

#### [storageproviderhydrationpolicymodifier](/uwp/api/windows.storage.provider.storageproviderhydrationpolicymodifier)

storageproviderhydrationpolicymodifier

#### [insyncpolicy](/uwp/api/windows.storage.provider.storageproviderinsyncpolicy)

storageproviderinsyncpolicy

#### [istorageprovideritempropertysource](/uwp/api/windows.storage.provider.istorageprovideritempropertysource)

istorageprovideritempropertysource <br> istorageprovideritempropertysource.getitemproperties

#### [istorageproviderpropertycapabilities](/uwp/api/windows.storage.provider.istorageproviderpropertycapabilities)

istorageproviderpropertycapabilities <br> istorageproviderpropertycapabilities.ispropertysupported

#### [populationpolicy](/uwp/api/windows.storage.provider.storageproviderpopulationpolicy)

storageproviderpopulationpolicy

#### [protectionmode](/uwp/api/windows.storage.provider.storageproviderprotectionmode)

storageproviderprotectionmode

#### [storageprovideritemproperties](/uwp/api/windows.storage.provider.storageprovideritemproperties)

storageprovideritemproperties <br> storageprovideritemproperties.setasync

#### [storageprovideritemproperty](/uwp/api/windows.storage.provider.storageprovideritemproperty)

storageprovideritemproperty <br> storageprovideritemproperty.iconresource <br> storageprovideritemproperty.id <br> storageprovideritemproperty.storageprovideritemproperty <br> storageprovideritemproperty.value

#### [storageprovideritempropertydefinition](/uwp/api/windows.storage.provider.storageprovideritempropertydefinition)

storageprovideritempropertydefinition <br> storageprovideritempropertydefinition.displaynameresource <br> storageprovideritempropertydefinition.id <br> storageprovideritempropertydefinition.storageprovideritempropertydefinition

#### [storageprovidersyncrootinfo](/uwp/api/windows.storage.provider.storageprovidersyncrootinfo)

storageprovidersyncrootinfo <br> storageprovidersyncrootinfo.allowpinning <br> storageprovidersyncrootinfo.context <br> storageprovidersyncrootinfo.displaynameresource <br> storageprovidersyncrootinfo.hydrationpolicy <br> storageprovidersyncrootinfo.hydrationpolicymodifier <br> storageprovidersyncrootinfo.iconresource <br> storageprovidersyncrootinfo.id <br> storageprovidersyncrootinfo.insyncpolicy <br> storageprovidersyncrootinfo.path <br> storageprovidersyncrootinfo.populationpolicy <br> storageprovidersyncrootinfo.protectionmode <br> storageprovidersyncrootinfo.recyclebinuri <br> storageprovidersyncrootinfo.showsiblingsasgroup <br> storageprovidersyncrootinfo.storageprovideritempropertydefinitions <br> storageprovidersyncrootinfo.storageprovidersyncrootinfo <br> storageprovidersyncrootinfo.version

#### [storageprovidersyncrootmanager](/uwp/api/windows.storage.provider.storageprovidersyncrootmanager)

storageprovidersyncrootmanager <br> storageprovidersyncrootmanager.getcurrentsyncroots <br> storageprovidersyncrootmanager.getsyncrootinformationforfolder <br> storageprovidersyncrootmanager.getsyncrootinformationforid <br> storageprovidersyncrootmanager.register <br> storageprovidersyncrootmanager.unregister

### [windows.storage.streams](/uwp/api/windows.storage.streams)

#### [fileopendisposition](/uwp/api/windows.storage.streams.fileopendisposition)

fileopendisposition

#### [filerandomaccessstream](/uwp/api/windows.storage.streams.filerandomaccessstream)

filerandomaccessstream.openasync <br> filerandomaccessstream.openasync <br> filerandomaccessstream.openforuserasync <br> filerandomaccessstream.openforuserasync <br> filerandomaccessstream.opentransactedwriteasync <br> filerandomaccessstream.opentransactedwriteasync <br> filerandomaccessstream.opentransactedwriteforuserasync <br> filerandomaccessstream.opentransactedwriteforuserasync

### [windows.storage](/uwp/api/windows.storage)

#### [appdatapaths](/uwp/api/windows.storage.appdatapaths)

appdatapaths <br> appdatapaths.cookies <br> appdatapaths.desktop <br> appdatapaths.documents <br> appdatapaths.favorites <br> appdatapaths.getdefault <br> appdatapaths.getforuser <br> appdatapaths.history <br> appdatapaths.internetcache <br> appdatapaths.localappdata <br> appdatapaths.programdata <br> appdatapaths.roamingappdata

#### [storagelibrary](/uwp/api/windows.storage.storagelibrary)

storagelibrary.arefoldersuggestionsavailableasync

#### [storageprovider](/uwp/api/windows.storage.storageprovider)

storageprovider.ispropertysupportedforpartialfileasync

#### [systemdatapaths](/uwp/api/windows.storage.systemdatapaths)

systemdatapaths <br> systemdatapaths.fonts <br> systemdatapaths.getdefault <br> systemdatapaths.programdata <br> systemdatapaths.public <br> systemdatapaths.publicdesktop <br> systemdatapaths.publicdocuments <br> systemdatapaths.publicdownloads <br> systemdatapaths.publicmusic <br> systemdatapaths.publicpictures <br> systemdatapaths.publicvideos <br> systemdatapaths.system <br> systemdatapaths.systemarm <br> systemdatapaths.systemhost <br> systemdatapaths.systemx64 <br> systemdatapaths.systemx86 <br> systemdatapaths.userprofiles <br> systemdatapaths.windows

#### [userdatapaths](/uwp/api/windows.storage.userdatapaths)

userdatapaths <br> userdatapaths.cameraroll <br> userdatapaths.cookies <br> userdatapaths.desktop <br> userdatapaths.documents <br> userdatapaths.downloads <br> userdatapaths.favorites <br> userdatapaths.getdefault <br> userdatapaths.getforuser <br> userdatapaths.history <br> userdatapaths.internetcache <br> userdatapaths.localappdata <br> userdatapaths.localappdatalow <br> userdatapaths.music <br> userdatapaths.pictures <br> userdatapaths.profile <br> userdatapaths.recent <br> userdatapaths.roamingappdata <br> userdatapaths.savedpictures <br> userdatapaths.screenshots <br> userdatapaths.templates <br> userdatapaths.videos

## windows.system

### [windows.system.diagnostics](/uwp/api/windows.system.diagnostics)

#### [diagnosticactionresult](/uwp/api/windows.system.diagnostics.diagnosticactionresult)

diagnosticactionresult <br> diagnosticactionresult.extendederror <br> diagnosticactionresult.results

#### [diagnosticactionstate](/uwp/api/windows.system.diagnostics.diagnosticactionstate)

diagnosticactionstate

#### [diagnosticinvoker](/uwp/api/windows.system.diagnostics.diagnosticinvoker)

diagnosticinvoker <br> diagnosticinvoker.getdefault <br> diagnosticinvoker.getforuser <br> diagnosticinvoker.issupported <br> diagnosticinvoker.rundiagnosticactionasync

#### [processdiagnosticinfo](/uwp/api/windows.system.diagnostics.processdiagnosticinfo)

processdiagnosticinfo.getappdiagnosticinfos <br> processdiagnosticinfo.ispackaged <br> processdiagnosticinfo.trygetforprocessid

### [windows.system.profile.systemmanufacturers](/uwp/api/windows.system.profile.systemmanufacturers)

#### [oemsupportinfo](/uwp/api/windows.system.profile.systemmanufacturers.oemsupportinfo)

oemsupportinfo <br> oemsupportinfo.supportapplink <br> oemsupportinfo.supportlink <br> oemsupportinfo.supportprovider

#### [systemsupportinfo](/uwp/api/windows.system.profile.systemmanufacturers.systemsupportinfo)

systemsupportinfo <br> systemsupportinfo.localsystemedition <br> systemsupportinfo.oemsupportinfo

### [windows.system.remotesystems](/uwp/api/windows.system.remotesystems)

#### [remotesystem](/uwp/api/windows.system.remotesystems.remotesystem)

remotesystem.manufacturerdisplayname <br> remotesystem.modeldisplayname

#### [remotesystemkinds](/uwp/api/windows.system.remotesystems.remotesystemkinds)

remotesystemkinds.iot <br> remotesystemkinds.laptop <br> remotesystemkinds.tablet

### [windows.system.userprofile](/uwp/api/windows.system.userprofile)

#### [globalizationpreferences](/uwp/api/windows.system.userprofile.globalizationpreferences)

globalizationpreferences.trysethomegeographicregion <br> globalizationpreferences.trysetlanguages

### [windows.system](/uwp/api/windows.system)

#### [appdiagnosticinfo](/uwp/api/windows.system.appdiagnosticinfo)

appdiagnosticinfo.createresourcegroupwatcher <br> appdiagnosticinfo.createwatcher <br> appdiagnosticinfo.getresourcegroups <br> appdiagnosticinfo.requestaccessasync <br> appdiagnosticinfo.requestinfoforappasync <br> appdiagnosticinfo.requestinfoforappasync <br> appdiagnosticinfo.requestinfoforpackageasync

#### [appdiagnosticinfowatcher](/uwp/api/windows.system.appdiagnosticinfowatcher)

appdiagnosticinfowatcher <br> appdiagnosticinfowatcher.added <br> appdiagnosticinfowatcher.enumerationcompleted <br> appdiagnosticinfowatcher.removed <br> appdiagnosticinfowatcher.start <br> appdiagnosticinfowatcher.status <br> appdiagnosticinfowatcher.stop <br> appdiagnosticinfowatcher.stopped

#### [appdiagnosticinfowatchereventargs](/uwp/api/windows.system.appdiagnosticinfowatchereventargs)

appdiagnosticinfowatchereventargs <br> appdiagnosticinfowatchereventargs.appdiagnosticinfo

#### [appdiagnosticinfowatcherstatus](/uwp/api/windows.system.appdiagnosticinfowatcherstatus)

appdiagnosticinfowatcherstatus

#### [appmemoryreport](/uwp/api/windows.system.appmemoryreport)

appmemoryreport.expectedtotalcommitlimit

#### [appresourcegroupbackgroundtaskreport](/uwp/api/windows.system.appresourcegroupbackgroundtaskreport)

appresourcegroupbackgroundtaskreport <br> appresourcegroupbackgroundtaskreport.entrypoint <br> appresourcegroupbackgroundtaskreport.name <br> appresourcegroupbackgroundtaskreport.taskid <br> appresourcegroupbackgroundtaskreport.trigger

#### [appresourcegroupenergyquotastate](/uwp/api/windows.system.appresourcegroupenergyquotastate)

appresourcegroupenergyquotastate

#### [appresourcegroupexecutionstate](/uwp/api/windows.system.appresourcegroupexecutionstate)

appresourcegroupexecutionstate

#### [appresourcegroupinfo](/uwp/api/windows.system.appresourcegroupinfo)

appresourcegroupinfo <br> appresourcegroupinfo.getbackgroundtaskreports <br> appresourcegroupinfo.getmemoryreport <br> appresourcegroupinfo.getprocessdiagnosticinfos <br> appresourcegroupinfo.getstatereport <br> appresourcegroupinfo.instanceid <br> appresourcegroupinfo.isshared

#### [appresourcegroupinfowatcher](/uwp/api/windows.system.appresourcegroupinfowatcher)

appresourcegroupinfowatcher <br> appresourcegroupinfowatcher.added <br> appresourcegroupinfowatcher.enumerationcompleted <br> appresourcegroupinfowatcher.executionstatechanged <br> appresourcegroupinfowatcher.removed <br> appresourcegroupinfowatcher.start <br> appresourcegroupinfowatcher.status <br> appresourcegroupinfowatcher.stop <br> appresourcegroupinfowatcher.stopped

#### [appresourcegroupinfowatchereventargs](/uwp/api/windows.system.appresourcegroupinfowatchereventargs)

appresourcegroupinfowatchereventargs <br> appresourcegroupinfowatchereventargs.appdiagnosticinfos <br> appresourcegroupinfowatchereventargs.appresourcegroupinfo

#### [appresourcegroupinfowatcherexecutionstatechangedeventargs](/uwp/api/windows.system.appresourcegroupinfowatcherexecutionstatechangedeventargs)

appresourcegroupinfowatcherexecutionstatechangedeventargs <br> appresourcegroupinfowatcherexecutionstatechangedeventargs.appdiagnosticinfos <br> appresourcegroupinfowatcherexecutionstatechangedeventargs.appresourcegroupinfo

#### [appresourcegroupinfowatcherstatus](/uwp/api/windows.system.appresourcegroupinfowatcherstatus)

appresourcegroupinfowatcherstatus

#### [appresourcegroupmemoryreport](/uwp/api/windows.system.appresourcegroupmemoryreport)

appresourcegroupmemoryreport <br> appresourcegroupmemoryreport.commitusagelevel <br> appresourcegroupmemoryreport.commitusagelimit <br> appresourcegroupmemoryreport.privatecommitusage <br> appresourcegroupmemoryreport.totalcommitusage

#### [appresourcegroupstatereport](/uwp/api/windows.system.appresourcegroupstatereport)

appresourcegroupstatereport <br> appresourcegroupstatereport.energyquotastate <br> appresourcegroupstatereport.executionstate

#### [datetimesettings](/uwp/api/windows.system.datetimesettings)

datetimesettings <br> datetimesettings.setsystemdatetime

#### [diagnosticaccessstatus](/uwp/api/windows.system.diagnosticaccessstatus)

diagnosticaccessstatus

#### [dispatcherqueue](/uwp/api/windows.system.dispatcherqueue)

dispatcherqueue <br> dispatcherqueue.createtimer <br> dispatcherqueue.getforcurrentthread <br> dispatcherqueue.shutdowncompleted <br> dispatcherqueue.shutdownstarting <br> dispatcherqueue.tryenqueue <br> dispatcherqueue.tryenqueue

#### [dispatcherqueuecontroller](/uwp/api/windows.system.dispatcherqueuecontroller)

dispatcherqueuecontroller <br> dispatcherqueuecontroller.createondedicatedthread <br> dispatcherqueuecontroller.dispatcherqueue <br> dispatcherqueuecontroller.shutdownqueueasync

#### [dispatcherqueuehandler](/uwp/api/windows.system.dispatcherqueuehandler)

dispatcherqueuehandler

#### [dispatcherqueuepriority](/uwp/api/windows.system.dispatcherqueuepriority)

dispatcherqueuepriority

#### [dispatcherqueueshutdownstartingeventargs](/uwp/api/windows.system.dispatcherqueueshutdownstartingeventargs)

dispatcherqueueshutdownstartingeventargs <br> dispatcherqueueshutdownstartingeventargs.getdeferral

#### [dispatcherqueuetimer](/uwp/api/windows.system.dispatcherqueuetimer)

dispatcherqueuetimer <br> dispatcherqueuetimer.interval <br> dispatcherqueuetimer.isrepeating <br> dispatcherqueuetimer.isrunning <br> dispatcherqueuetimer.start <br> dispatcherqueuetimer.stop <br> dispatcherqueuetimer.tick

#### [memorymanager](/uwp/api/windows.system.memorymanager)

memorymanager.expectedappmemoryusagelimit

## windows.ui

### [windows.ui.composition.effects](/uwp/api/windows.ui.composition.effects)

#### [scenelightingeffect](/uwp/api/windows.ui.composition.effects.scenelightingeffect)

scenelightingeffect.reflectancemodel

#### [scenelightingeffectreflectancemodel](/uwp/api/windows.ui.composition.effects.scenelightingeffectreflectancemodel)

scenelightingeffectreflectancemodel

### [windows.ui.composition.interactions](/uwp/api/windows.ui.composition.interactions)

#### [interactiontracker](/uwp/api/windows.ui.composition.interactions.interactiontracker)

interactiontracker.configurevector2positioninertiamodifiers

#### [interactiontrackerinertianaturalmotion](/uwp/api/windows.ui.composition.interactions.interactiontrackerinertianaturalmotion)

interactiontrackerinertianaturalmotion <br> interactiontrackerinertianaturalmotion.condition <br> interactiontrackerinertianaturalmotion.create <br> interactiontrackerinertianaturalmotion.naturalmotion

#### [interactiontrackervector2inertiamodifier](/uwp/api/windows.ui.composition.interactions.interactiontrackervector2inertiamodifier)

interactiontrackervector2inertiamodifier

#### [interactiontrackervector2inertianaturalmotion](/uwp/api/windows.ui.composition.interactions.interactiontrackervector2inertianaturalmotion)

interactiontrackervector2inertianaturalmotion <br> interactiontrackervector2inertianaturalmotion.condition <br> interactiontrackervector2inertianaturalmotion.create <br> interactiontrackervector2inertianaturalmotion.naturalmotion

### [windows.ui.composition](/uwp/api/windows.ui.composition)

#### [ambientlight](/uwp/api/windows.ui.composition.ambientlight)

ambientlight.intensity

#### [compositionanimation](/uwp/api/windows.ui.composition.compositionanimation)

compositionanimation.initialvalueexpressions

#### [compositioncolorgradientstop](/uwp/api/windows.ui.composition.compositioncolorgradientstop)

compositioncolorgradientstop <br> compositioncolorgradientstop.color <br> compositioncolorgradientstop.offset

#### [compositioncolorgradientstopcollection](/uwp/api/windows.ui.composition.compositioncolorgradientstopcollection)

compositioncolorgradientstopcollection <br> compositioncolorgradientstopcollection.append <br> compositioncolorgradientstopcollection.clear <br> compositioncolorgradientstopcollection.first <br> compositioncolorgradientstopcollection.getat <br> compositioncolorgradientstopcollection.getmany <br> compositioncolorgradientstopcollection.getview <br> compositioncolorgradientstopcollection.indexof <br> compositioncolorgradientstopcollection.insertat <br> compositioncolorgradientstopcollection.removeat <br> compositioncolorgradientstopcollection.removeatend <br> compositioncolorgradientstopcollection.replaceall <br> compositioncolorgradientstopcollection.setat <br> compositioncolorgradientstopcollection.size

#### [compositiondropshadowsourcepolicy](/uwp/api/windows.ui.composition.compositiondropshadowsourcepolicy)

compositiondropshadowsourcepolicy

#### [compositiongradientbrush](/uwp/api/windows.ui.composition.compositiongradientbrush)

compositiongradientbrush <br> compositiongradientbrush.anchorpoint <br> compositiongradientbrush.centerpoint <br> compositiongradientbrush.colorstops <br> compositiongradientbrush.extendmode <br> compositiongradientbrush.interpolationspace <br> compositiongradientbrush.offset <br> compositiongradientbrush.rotationangle <br> compositiongradientbrush.rotationangleindegrees <br> compositiongradientbrush.scale <br> compositiongradientbrush.transformmatrix

#### [compositiongradientextendmode](/uwp/api/windows.ui.composition.compositiongradientextendmode)

compositiongradientextendmode

#### [compositionlight](/uwp/api/windows.ui.composition.compositionlight)

compositionlight.exclusionsfromtargets

#### [compositionlineargradientbrush](/uwp/api/windows.ui.composition.compositionlineargradientbrush)

compositionlineargradientbrush <br> compositionlineargradientbrush.endpoint <br> compositionlineargradientbrush.startpoint

#### [compositionobject](/uwp/api/windows.ui.composition.compositionobject)

compositionobject.dispatcherqueue

#### [compositor](/uwp/api/windows.ui.composition.compositor)

compositor.createcolorgradientstop <br> compositor.createcolorgradientstop <br> compositor.createlineargradientbrush <br> compositor.createspringscalaranimation <br> compositor.createspringvector2animation <br> compositor.createspringvector3animation

#### [distantlight](/uwp/api/windows.ui.composition.distantlight)

distantlight.intensity

#### [dropshadow](/uwp/api/windows.ui.composition.dropshadow)

dropshadow.sourcepolicy

#### [initialvalueexpressioncollection](/uwp/api/windows.ui.composition.initialvalueexpressioncollection)

initialvalueexpressioncollection <br> initialvalueexpressioncollection.clear <br> initialvalueexpressioncollection.first <br> initialvalueexpressioncollection.getview <br> initialvalueexpressioncollection.haskey <br> initialvalueexpressioncollection.insert <br> initialvalueexpressioncollection.lookup <br> initialvalueexpressioncollection.remove <br> initialvalueexpressioncollection.size

#### [layervisual](/uwp/api/windows.ui.composition.layervisual)

layervisual.shadow

#### [naturalmotionanimation](/uwp/api/windows.ui.composition.naturalmotionanimation)

naturalmotionanimation <br> naturalmotionanimation.delaybehavior <br> naturalmotionanimation.delaytime <br> naturalmotionanimation.stopbehavior

#### [pointlight](/uwp/api/windows.ui.composition.pointlight)

pointlight.intensity

#### [scalarnaturalmotionanimation](/uwp/api/windows.ui.composition.scalarnaturalmotionanimation)

scalarnaturalmotionanimation <br> scalarnaturalmotionanimation.finalvalue <br> scalarnaturalmotionanimation.initialvalue <br> scalarnaturalmotionanimation.initialvelocity

#### [spotlight](/uwp/api/windows.ui.composition.spotlight)

spotlight.innerconeintensity <br> spotlight.outerconeintensity

#### [springscalarnaturalmotionanimation](/uwp/api/windows.ui.composition.springscalarnaturalmotionanimation)

springscalarnaturalmotionanimation <br> springscalarnaturalmotionanimation.dampingratio <br> springscalarnaturalmotionanimation.period

#### [springvector2naturalmotionanimation](/uwp/api/windows.ui.composition.springvector2naturalmotionanimation)

springvector2naturalmotionanimation <br> springvector2naturalmotionanimation.dampingratio <br> springvector2naturalmotionanimation.period

#### [springvector3naturalmotionanimation](/uwp/api/windows.ui.composition.springvector3naturalmotionanimation)

springvector3naturalmotionanimation <br> springvector3naturalmotionanimation.dampingratio <br> springvector3naturalmotionanimation.period

#### [vector2naturalmotionanimation](/uwp/api/windows.ui.composition.vector2naturalmotionanimation)

vector2naturalmotionanimation <br> vector2naturalmotionanimation.finalvalue <br> vector2naturalmotionanimation.initialvalue <br> vector2naturalmotionanimation.initialvelocity

#### [vector3naturalmotionanimation](/uwp/api/windows.ui.composition.vector3naturalmotionanimation)

vector3naturalmotionanimation <br> vector3naturalmotionanimation.finalvalue <br> vector3naturalmotionanimation.initialvalue <br> vector3naturalmotionanimation.initialvelocity

### [windows.ui.core](/uwp/api/windows.ui.core)

#### [corewindow](/uwp/api/windows.ui.core.corewindow)

corewindow.activationmode <br> corewindow.dispatcherqueue

#### [corewindowactivationmode](/uwp/api/windows.ui.core.corewindowactivationmode)

corewindowactivationmode

### [windows.ui.input.inking.core](/uwp/api/windows.ui.input.inking.core)

#### [coreincrementalinkstroke](/uwp/api/windows.ui.input.inking.core.coreincrementalinkstroke)

coreincrementalinkstroke <br> coreincrementalinkstroke.appendinkpoints <br> coreincrementalinkstroke.boundingrect <br> coreincrementalinkstroke.coreincrementalinkstroke <br> coreincrementalinkstroke.createinkstroke <br> coreincrementalinkstroke.drawingattributes <br> coreincrementalinkstroke.pointtransform

#### [coreinkpresenterhost](/uwp/api/windows.ui.input.inking.core.coreinkpresenterhost)

coreinkpresenterhost <br> coreinkpresenterhost.coreinkpresenterhost <br> coreinkpresenterhost.inkpresenter <br> coreinkpresenterhost.rootvisual

### [windows.ui.input.preview.injection](/uwp/api/windows.ui.input.preview.injection)

#### [injectedinputgamepadinfo](/uwp/api/windows.ui.input.preview.injection.injectedinputgamepadinfo)

injectedinputgamepadinfo <br> injectedinputgamepadinfo.buttons <br> injectedinputgamepadinfo.injectedinputgamepadinfo <br> injectedinputgamepadinfo.injectedinputgamepadinfo <br> injectedinputgamepadinfo.leftthumbstickx <br> injectedinputgamepadinfo.leftthumbsticky <br> injectedinputgamepadinfo.lefttrigger <br> injectedinputgamepadinfo.rightthumbstickx <br> injectedinputgamepadinfo.rightthumbsticky <br> injectedinputgamepadinfo.righttrigger

#### [inputinjector](/uwp/api/windows.ui.input.preview.injection.inputinjector)

inputinjector.initializegamepadinjection <br> inputinjector.injectgamepadinput <br> inputinjector.trycreateforappbroadcastonly <br> inputinjector.uninitializegamepadinjection

### [windows.ui.input.spatial](/uwp/api/windows.ui.input.spatial)

#### [spatialinteractioncontroller](/uwp/api/windows.ui.input.spatial.spatialinteractioncontroller)

spatialinteractioncontroller.trygetrenderablemodelasync

#### [spatialinteractionsource](/uwp/api/windows.ui.input.spatial.spatialinteractionsource)

spatialinteractionsource.handedness

#### [spatialinteractionsourcehandedness](/uwp/api/windows.ui.input.spatial.spatialinteractionsourcehandedness)

spatialinteractionsourcehandedness

#### [spatialinteractionsourcelocation](/uwp/api/windows.ui.input.spatial.spatialinteractionsourcelocation)

spatialinteractionsourcelocation.angularvelocity <br> spatialinteractionsourcelocation.positionaccuracy <br> spatialinteractionsourcelocation.sourcepointerpose

#### [spatialinteractionsourcepositionaccuracy](/uwp/api/windows.ui.input.spatial.spatialinteractionsourcepositionaccuracy)

spatialinteractionsourcepositionaccuracy

#### [spatialpointerinteractionsourcepose](/uwp/api/windows.ui.input.spatial.spatialpointerinteractionsourcepose)

spatialpointerinteractionsourcepose.orientation <br> spatialpointerinteractionsourcepose.positionaccuracy

### [windows.ui.input](/uwp/api/windows.ui.input)

#### [radialcontrollerconfiguration](/uwp/api/windows.ui.input.radialcontrollerconfiguration)

radialcontrollerconfiguration.appcontroller <br> radialcontrollerconfiguration.isappcontrollerenabled

### [windows.ui.shell](/uwp/api/windows.ui.shell)

#### [adaptivecardbuilder](/uwp/api/windows.ui.shell.adaptivecardbuilder)

adaptivecardbuilder <br> adaptivecardbuilder.createadaptivecardfromjson

#### [iadaptivecard](/uwp/api/windows.ui.shell.iadaptivecard)

iadaptivecard <br> iadaptivecard.tojson

#### [iadaptivecardbuilderstatics](/uwp/api/windows.ui.shell.iadaptivecardbuilderstatics)

iadaptivecardbuilderstatics <br> iadaptivecardbuilderstatics.createadaptivecardfromjson

#### [taskbarmanager](/uwp/api/windows.ui.shell.taskbarmanager)

taskbarmanager <br> taskbarmanager.getdefault <br> taskbarmanager.isapplistentrypinnedasync <br> taskbarmanager.iscurrentapppinnedasync <br> taskbarmanager.ispinningallowed <br> taskbarmanager.issupported <br> taskbarmanager.requestpinapplistentryasync <br> taskbarmanager.requestpincurrentappasync

### [windows.ui.startscreen](/uwp/api/windows.ui.startscreen)

#### [secondarytilevisualelements](/uwp/api/windows.ui.startscreen.secondarytilevisualelements)

secondarytilevisualelements.mixedrealitymodel

#### [tilemixedrealitymodel](/uwp/api/windows.ui.startscreen.tilemixedrealitymodel)

tilemixedrealitymodel <br> tilemixedrealitymodel.boundingbox <br> tilemixedrealitymodel.uri

### [windows.ui.viewmanagement.core](/uwp/api/windows.ui.viewmanagement.core)

#### [coreinputview](/uwp/api/windows.ui.viewmanagement.core.coreinputview)

coreinputview <br> coreinputview.getcoreinputviewocclusions <br> coreinputview.getforcurrentview <br> coreinputview.occlusionschanged <br> coreinputview.tryhideprimaryview <br> coreinputview.tryshowprimaryview

#### [coreinputviewocclusion](/uwp/api/windows.ui.viewmanagement.core.coreinputviewocclusion)

coreinputviewocclusion <br> coreinputviewocclusion.occludingrect <br> coreinputviewocclusion.occlusionkind

#### [coreinputviewocclusionkind](/uwp/api/windows.ui.viewmanagement.core.coreinputviewocclusionkind)

coreinputviewocclusionkind

#### [coreinputviewocclusionschangedeventargs](/uwp/api/windows.ui.viewmanagement.core.coreinputviewocclusionschangedeventargs)

coreinputviewocclusionschangedeventargs <br> coreinputviewocclusionschangedeventargs.handled <br> coreinputviewocclusionschangedeventargs.occlusions

### [windows.ui.webui](/uwp/api/windows.ui.webui)

#### [webuiapplication](/uwp/api/windows.ui.webui.webuiapplication)

webuiapplication.requestrestartasync <br> webuiapplication.requestrestartforuserasync

#### [webuicommandlineactivatedeventargs](/uwp/api/windows.ui.webui.webuicommandlineactivatedeventargs)

webuicommandlineactivatedeventargs <br> webuicommandlineactivatedeventargs.activatedoperation <br> webuicommandlineactivatedeventargs.kind <br> webuicommandlineactivatedeventargs.operation <br> webuicommandlineactivatedeventargs.previousexecutionstate <br> webuicommandlineactivatedeventargs.splashscreen <br> webuicommandlineactivatedeventargs.user

#### [webuistartuptaskactivatedeventargs](/uwp/api/windows.ui.webui.webuistartuptaskactivatedeventargs)

webuistartuptaskactivatedeventargs <br> webuistartuptaskactivatedeventargs.kind <br> webuistartuptaskactivatedeventargs.previousexecutionstate <br> webuistartuptaskactivatedeventargs.splashscreen <br> webuistartuptaskactivatedeventargs.taskid <br> webuistartuptaskactivatedeventargs.user

### [windows.ui.xaml.automation.peers](/uwp/api/windows.ui.xaml.automation.peers)

#### [automationnotificationkind](/uwp/api/windows.ui.xaml.automation.peers.automationnotificationkind)

automationnotificationkind

#### [automationnotificationprocessing](/uwp/api/windows.ui.xaml.automation.peers.automationnotificationprocessing)

automationnotificationprocessing

#### [automationpeer](/uwp/api/windows.ui.xaml.automation.peers.automationpeer)

automationpeer.raisenotificationevent

#### [colorpickersliderautomationpeer](/uwp/api/windows.ui.xaml.automation.peers.colorpickersliderautomationpeer)

colorpickersliderautomationpeer <br> colorpickersliderautomationpeer.colorpickersliderautomationpeer

#### [colorspectrumautomationpeer](/uwp/api/windows.ui.xaml.automation.peers.colorspectrumautomationpeer)

colorspectrumautomationpeer <br> colorspectrumautomationpeer.colorspectrumautomationpeer

#### [navigationviewitemautomationpeer](/uwp/api/windows.ui.xaml.automation.peers.navigationviewitemautomationpeer)

navigationviewitemautomationpeer <br> navigationviewitemautomationpeer.navigationviewitemautomationpeer

#### [personpictureautomationpeer](/uwp/api/windows.ui.xaml.automation.peers.personpictureautomationpeer)

personpictureautomationpeer <br> personpictureautomationpeer.personpictureautomationpeer

#### [ratingcontrolautomationpeer](/uwp/api/windows.ui.xaml.automation.peers.ratingcontrolautomationpeer)

ratingcontrolautomationpeer <br> ratingcontrolautomationpeer.ratingcontrolautomationpeer

### [windows.ui.xaml.controls.maps](/uwp/api/windows.ui.xaml.controls.maps)

#### [mapcontrol](/uwp/api/windows.ui.xaml.controls.maps.mapcontrol)

mapcontrol.layers <br> mapcontrol.layersproperty <br> mapcontrol.trygetlocationfromoffset <br> mapcontrol.trygetlocationfromoffset

#### [mapcontroldatahelper](/uwp/api/windows.ui.xaml.controls.maps.mapcontroldatahelper)

mapcontroldatahelper.createmapcontrol

#### [mapelement3d](/uwp/api/windows.ui.xaml.controls.maps.mapelement3d)

mapelement3d <br> mapelement3d.heading <br> mapelement3d.headingproperty <br> mapelement3d.location <br> mapelement3d.locationproperty <br> mapelement3d.mapelement3d <br> mapelement3d.model <br> mapelement3d.pitch <br> mapelement3d.pitchproperty <br> mapelement3d.roll <br> mapelement3d.rollproperty <br> mapelement3d.scale <br> mapelement3d.scaleproperty

#### [mapelement](/uwp/api/windows.ui.xaml.controls.maps.mapelement)

mapelement.mapstylesheetentry <br> mapelement.mapstylesheetentryproperty <br> mapelement.mapstylesheetentrystate <br> mapelement.mapstylesheetentrystateproperty <br> mapelement.tag <br> mapelement.tagproperty

#### [mapelementslayer](/uwp/api/windows.ui.xaml.controls.maps.mapelementslayer)

mapelementslayer <br> mapelementslayer.mapcontextrequested <br> mapelementslayer.mapelementclick <br> mapelementslayer.mapelementpointerentered <br> mapelementslayer.mapelementpointerexited <br> mapelementslayer.mapelements <br> mapelementslayer.mapelementslayer <br> mapelementslayer.mapelementsproperty

#### [mapelementslayerclickeventargs](/uwp/api/windows.ui.xaml.controls.maps.mapelementslayerclickeventargs)

mapelementslayerclickeventargs <br> mapelementslayerclickeventargs.location <br> mapelementslayerclickeventargs.mapelements <br> mapelementslayerclickeventargs.mapelementslayerclickeventargs <br> mapelementslayerclickeventargs.position

#### [mapelementslayercontextrequestedeventargs](/uwp/api/windows.ui.xaml.controls.maps.mapelementslayercontextrequestedeventargs)

mapelementslayercontextrequestedeventargs <br> mapelementslayercontextrequestedeventargs.location <br> mapelementslayercontextrequestedeventargs.mapelements <br> mapelementslayercontextrequestedeventargs.mapelementslayercontextrequestedeventargs <br> mapelementslayercontextrequestedeventargs.position

#### [mapelementslayerpointerenteredeventargs](/uwp/api/windows.ui.xaml.controls.maps.mapelementslayerpointerenteredeventargs)

mapelementslayerpointerenteredeventargs <br> mapelementslayerpointerenteredeventargs.location <br> mapelementslayerpointerenteredeventargs.mapelement <br> mapelementslayerpointerenteredeventargs.mapelementslayerpointerenteredeventargs <br> mapelementslayerpointerenteredeventargs.position

#### [mapelementslayerpointerexitedeventargs](/uwp/api/windows.ui.xaml.controls.maps.mapelementslayerpointerexitedeventargs)

mapelementslayerpointerexitedeventargs <br> mapelementslayerpointerexitedeventargs.location <br> mapelementslayerpointerexitedeventargs.mapelement <br> mapelementslayerpointerexitedeventargs.mapelementslayerpointerexitedeventargs <br> mapelementslayerpointerexitedeventargs.position

#### [maplayer](/uwp/api/windows.ui.xaml.controls.maps.maplayer)

maplayer <br> maplayer.maplayer <br> maplayer.maptabindex <br> maplayer.maptabindexproperty <br> maplayer.visible <br> maplayer.visibleproperty <br> maplayer.zindex <br> maplayer.zindexproperty

#### [mapmodel3d](/uwp/api/windows.ui.xaml.controls.maps.mapmodel3d)

mapmodel3d <br> mapmodel3d.createfrom3mfasync <br> mapmodel3d.createfrom3mfasync <br> mapmodel3d.mapmodel3d

#### [mapmodel3dshadingoption](/uwp/api/windows.ui.xaml.controls.maps.mapmodel3dshadingoption)

mapmodel3dshadingoption

#### [mapstylesheetentries](/uwp/api/windows.ui.xaml.controls.maps.mapstylesheetentries)

mapstylesheetentries <br> mapstylesheetentries.admindistrict <br> mapstylesheetentries.admindistrictcapital <br> mapstylesheetentries.airport <br> mapstylesheetentries.area <br> mapstylesheetentries.arterialroad <br> mapstylesheetentries.building <br> mapstylesheetentries.business <br> mapstylesheetentries.capital <br> mapstylesheetentries.cemetery <br> mapstylesheetentries.continent <br> mapstylesheetentries.controlledaccesshighway <br> mapstylesheetentries.countryregion <br> mapstylesheetentries.countryregioncapital <br> mapstylesheetentries.district <br> mapstylesheetentries.drivingroute <br> mapstylesheetentries.education <br> mapstylesheetentries.educationbuilding <br> mapstylesheetentries.foodpoint <br> mapstylesheetentries.forest <br> mapstylesheetentries.golfcourse <br> mapstylesheetentries.highspeedramp <br> mapstylesheetentries.highway <br> mapstylesheetentries.indigenouspeoplesreserve <br> mapstylesheetentries.island <br> mapstylesheetentries.majorroad <br> mapstylesheetentries.medical <br> mapstylesheetentries.medicalbuilding <br> mapstylesheetentries.military <br> mapstylesheetentries.naturalpoint <br> mapstylesheetentries.nautical <br> mapstylesheetentries.neighborhood <br> mapstylesheetentries.park <br> mapstylesheetentries.peak <br> mapstylesheetentries.playingfield <br> mapstylesheetentries.point <br> mapstylesheetentries.pointofinterest <br> mapstylesheetentries.political <br> mapstylesheetentries.populatedplace <br> mapstylesheetentries.railway <br> mapstylesheetentries.ramp <br> mapstylesheetentries.reserve <br> mapstylesheetentries.river <br> mapstylesheetentries.road <br> mapstylesheetentries.roadexit <br> mapstylesheetentries.roadshield <br> mapstylesheetentries.routeline <br> mapstylesheetentries.runway <br> mapstylesheetentries.sand <br> mapstylesheetentries.shoppingcenter <br> mapstylesheetentries.stadium <br> mapstylesheetentries.street <br> mapstylesheetentries.structure <br> mapstylesheetentries.tollroad <br> mapstylesheetentries.trail <br> mapstylesheetentries.transit <br> mapstylesheetentries.transitbuilding <br> mapstylesheetentries.transportation <br> mapstylesheetentries.unpavedstreet <br> mapstylesheetentries.vegetation <br> mapstylesheetentries.volcanicpeak <br> mapstylesheetentries.walkingroute <br> mapstylesheetentries.water <br> mapstylesheetentries.waterpoint <br> mapstylesheetentries.waterroute

#### [mapstylesheetentrystates](/uwp/api/windows.ui.xaml.controls.maps.mapstylesheetentrystates)

mapstylesheetentrystates <br> mapstylesheetentrystates.disabled <br> mapstylesheetentrystates.hover <br> mapstylesheetentrystates.selected

### [windows.ui.xaml.controls.primitives](/uwp/api/windows.ui.xaml.controls.primitives)

#### [colorpickerslider](/uwp/api/windows.ui.xaml.controls.primitives.colorpickerslider)

colorpickerslider <br> colorpickerslider.colorchannel <br> colorpickerslider.colorchannelproperty <br> colorpickerslider.colorpickerslider

#### [colorspectrum](/uwp/api/windows.ui.xaml.controls.primitives.colorspectrum)

colorspectrum <br> colorspectrum.color <br> colorspectrum.colorchanged <br> colorspectrum.colorproperty <br> colorspectrum.colorspectrum <br> colorspectrum.components <br> colorspectrum.componentsproperty <br> colorspectrum.hsvcolor <br> colorspectrum.hsvcolorproperty <br> colorspectrum.maxhue <br> colorspectrum.maxhueproperty <br> colorspectrum.maxsaturation <br> colorspectrum.maxsaturationproperty <br> colorspectrum.maxvalue <br> colorspectrum.maxvalueproperty <br> colorspectrum.minhue <br> colorspectrum.minhueproperty <br> colorspectrum.minsaturation <br> colorspectrum.minsaturationproperty <br> colorspectrum.minvalue <br> colorspectrum.minvalueproperty <br> colorspectrum.shape <br> colorspectrum.shapeproperty

#### [flyoutbase](/uwp/api/windows.ui.xaml.controls.primitives.flyoutbase)

flyoutbase.onprocesskeyboardaccelerators <br> flyoutbase.tryinvokekeyboardaccelerator

#### [layoutinformation](/uwp/api/windows.ui.xaml.controls.primitives.layoutinformation)

layoutinformation.getavailablesize

#### [listviewitempresenter](/uwp/api/windows.ui.xaml.controls.primitives.listviewitempresenter)

listviewitempresenter.revealbackground <br> listviewitempresenter.revealbackgroundproperty <br> listviewitempresenter.revealbackgroundshowsabovecontent <br> listviewitempresenter.revealbackgroundshowsabovecontentproperty <br> listviewitempresenter.revealborderbrush <br> listviewitempresenter.revealborderbrushproperty <br> listviewitempresenter.revealborderthickness <br> listviewitempresenter.revealborderthicknessproperty

### [windows.ui.xaml.controls](/uwp/api/windows.ui.xaml.controls)

#### [bitmapiconsource](/uwp/api/windows.ui.xaml.controls.bitmapiconsource)

bitmapiconsource <br> bitmapiconsource.bitmapiconsource <br> bitmapiconsource.showasmonochrome <br> bitmapiconsource.showasmonochromeproperty <br> bitmapiconsource.urisource <br> bitmapiconsource.urisourceproperty

#### [charactercasing](/uwp/api/windows.ui.xaml.controls.charactercasing)

charactercasing

#### [colorchangedeventargs](/uwp/api/windows.ui.xaml.controls.colorchangedeventargs)

colorchangedeventargs <br> colorchangedeventargs.newcolor <br> colorchangedeventargs.oldcolor

#### [colorpicker](/uwp/api/windows.ui.xaml.controls.colorpicker)

colorpicker <br> colorpicker.color <br> colorpicker.colorchanged <br> colorpicker.colorpicker <br> colorpicker.colorproperty <br> colorpicker.colorspectrumcomponents <br> colorpicker.colorspectrumcomponentsproperty <br> colorpicker.colorspectrumshape <br> colorpicker.colorspectrumshapeproperty <br> colorpicker.isalphaenabled <br> colorpicker.isalphaenabledproperty <br> colorpicker.isalphaslidervisible <br> colorpicker.isalphaslidervisibleproperty <br> colorpicker.isalphatextinputvisible <br> colorpicker.isalphatextinputvisibleproperty <br> colorpicker.iscolorchanneltextinputvisible <br> colorpicker.iscolorchanneltextinputvisibleproperty <br> colorpicker.iscolorpreviewvisible <br> colorpicker.iscolorpreviewvisibleproperty <br> colorpicker.iscolorslidervisible <br> colorpicker.iscolorslidervisibleproperty <br> colorpicker.iscolorspectrumvisible <br> colorpicker.iscolorspectrumvisibleproperty <br> colorpicker.ishexinputvisible <br> colorpicker.ishexinputvisibleproperty <br> colorpicker.ismorebuttonvisible <br> colorpicker.ismorebuttonvisibleproperty <br> colorpicker.maxhue <br> colorpicker.maxhueproperty <br> colorpicker.maxsaturation <br> colorpicker.maxsaturationproperty <br> colorpicker.maxvalue <br> colorpicker.maxvalueproperty <br> colorpicker.minhue <br> colorpicker.minhueproperty <br> colorpicker.minsaturation <br> colorpicker.minsaturationproperty <br> colorpicker.minvalue <br> colorpicker.minvalueproperty <br> colorpicker.previouscolor <br> colorpicker.previouscolorproperty

#### [colorpickerhsvchannel](/uwp/api/windows.ui.xaml.controls.colorpickerhsvchannel)

colorpickerhsvchannel

#### [colorspectrumcomponents](/uwp/api/windows.ui.xaml.controls.colorspectrumcomponents)

colorspectrumcomponents

#### [colorspectrumshape](/uwp/api/windows.ui.xaml.controls.colorspectrumshape)

colorspectrumshape

#### [combobox](/uwp/api/windows.ui.xaml.controls.combobox)

combobox.placeholderforeground <br> combobox.placeholderforegroundproperty

#### [contentdialog](/uwp/api/windows.ui.xaml.controls.contentdialog)

contentdialog.showasync

#### [contentdialogplacement](/uwp/api/windows.ui.xaml.controls.contentdialogplacement)

contentdialogplacement

#### [control](/uwp/api/windows.ui.xaml.controls.control)

control.oncharacterreceived <br> control.onpreviewkeydown <br> control.onpreviewkeyup

#### [disabledformattingaccelerators](/uwp/api/windows.ui.xaml.controls.disabledformattingaccelerators)

disabledformattingaccelerators

#### [fonticonsource](/uwp/api/windows.ui.xaml.controls.fonticonsource)

fonticonsource <br> fonticonsource.fontfamily <br> fonticonsource.fontfamilyproperty <br> fonticonsource.fonticonsource <br> fonticonsource.fontsize <br> fonticonsource.fontsizeproperty <br> fonticonsource.fontstyle <br> fonticonsource.fontstyleproperty <br> fonticonsource.fontweight <br> fonticonsource.fontweightproperty <br> fonticonsource.glyph <br> fonticonsource.glyphproperty <br> fonticonsource.istextscalefactorenabled <br> fonticonsource.istextscalefactorenabledproperty <br> fonticonsource.mirroredwhenrighttoleft <br> fonticonsource.mirroredwhenrighttoleftproperty

#### [grid](/uwp/api/windows.ui.xaml.controls.grid)

grid.columnspacing <br> grid.columnspacingproperty <br> grid.rowspacing <br> grid.rowspacingproperty

#### [iconsource](/uwp/api/windows.ui.xaml.controls.iconsource)

iconsource <br> iconsource.foreground <br> iconsource.foregroundproperty

#### [istexttrimmedchangedeventargs](/uwp/api/windows.ui.xaml.controls.istexttrimmedchangedeventargs)

istexttrimmedchangedeventargs

#### [mediatransportcontrols](/uwp/api/windows.ui.xaml.controls.mediatransportcontrols)

mediatransportcontrols.hide <br> mediatransportcontrols.isrepeatbuttonvisible <br> mediatransportcontrols.isrepeatbuttonvisibleproperty <br> mediatransportcontrols.isrepeatenabled <br> mediatransportcontrols.isrepeatenabledproperty <br> mediatransportcontrols.show <br> mediatransportcontrols.showandhideautomatically <br> mediatransportcontrols.showandhideautomaticallyproperty

#### [navigationview](/uwp/api/windows.ui.xaml.controls.navigationview)

navigationview <br> navigationview.alwaysshowheader <br> navigationview.alwaysshowheaderproperty <br> navigationview.autosuggestbox <br> navigationview.autosuggestboxproperty <br> navigationview.compactmodethresholdwidth <br> navigationview.compactmodethresholdwidthproperty <br> navigationview.compactpanelength <br> navigationview.compactpanelengthproperty <br> navigationview.containerfrommenuitem <br> navigationview.displaymode <br> navigationview.displaymodechanged <br> navigationview.displaymodeproperty <br> navigationview.expandedmodethresholdwidth <br> navigationview.expandedmodethresholdwidthproperty <br> navigationview.header <br> navigationview.headerproperty <br> navigationview.headertemplate <br> navigationview.headertemplateproperty <br> navigationview.ispaneopen <br> navigationview.ispaneopenproperty <br> navigationview.ispanetogglebuttonvisible <br> navigationview.ispanetogglebuttonvisibleproperty <br> navigationview.issettingsvisible <br> navigationview.issettingsvisibleproperty <br> navigationview.iteminvoked <br> navigationview.menuitemcontainerstyle <br> navigationview.menuitemcontainerstyleproperty <br> navigationview.menuitemcontainerstyleselector <br> navigationview.menuitemcontainerstyleselectorproperty <br> navigationview.menuitemfromcontainer <br> navigationview.menuitems <br> navigationview.menuitemsproperty <br> navigationview.menuitemssource <br> navigationview.menuitemssourceproperty <br> navigationview.menuitemtemplate <br> navigationview.menuitemtemplateproperty <br> navigationview.menuitemtemplateselector <br> navigationview.menuitemtemplateselectorproperty <br> navigationview.navigationview <br> navigationview.openpanelength <br> navigationview.openpanelengthproperty <br> navigationview.panefooter <br> navigationview.panefooterproperty <br> navigationview.panetogglebuttonstyle <br> navigationview.panetogglebuttonstyleproperty <br> navigationview.selecteditem <br> navigationview.selecteditemproperty <br> navigationview.selectionchanged <br> navigationview.settingsitem <br> navigationview.settingsitemproperty

#### [navigationviewdisplaymode](/uwp/api/windows.ui.xaml.controls.navigationviewdisplaymode)

navigationviewdisplaymode

#### [navigationviewdisplaymodechangedeventargs](/uwp/api/windows.ui.xaml.controls.navigationviewdisplaymodechangedeventargs)

navigationviewdisplaymodechangedeventargs <br> navigationviewdisplaymodechangedeventargs.displaymode

#### [navigationviewitem](/uwp/api/windows.ui.xaml.controls.navigationviewitem)

navigationviewitem <br> navigationviewitem.compactpanelength <br> navigationviewitem.compactpanelengthproperty <br> navigationviewitem.icon <br> navigationviewitem.iconproperty <br> navigationviewitem.navigationviewitem

#### [navigationviewitembase](/uwp/api/windows.ui.xaml.controls.navigationviewitembase)

navigationviewitembase

#### [navigationviewitemheader](/uwp/api/windows.ui.xaml.controls.navigationviewitemheader)

navigationviewitemheader <br> navigationviewitemheader.navigationviewitemheader

#### [navigationviewiteminvokedeventargs](/uwp/api/windows.ui.xaml.controls.navigationviewiteminvokedeventargs)

navigationviewiteminvokedeventargs <br> navigationviewiteminvokedeventargs.invokeditem <br> navigationviewiteminvokedeventargs.issettingsinvoked <br> navigationviewiteminvokedeventargs.navigationviewiteminvokedeventargs

#### [navigationviewitemseparator](/uwp/api/windows.ui.xaml.controls.navigationviewitemseparator)

navigationviewitemseparator <br> navigationviewitemseparator.navigationviewitemseparator

#### [navigationviewlist](/uwp/api/windows.ui.xaml.controls.navigationviewlist)

navigationviewlist <br> navigationviewlist.navigationviewlist

#### [navigationviewselectionchangedeventargs](/uwp/api/windows.ui.xaml.controls.navigationviewselectionchangedeventargs)

navigationviewselectionchangedeventargs <br> navigationviewselectionchangedeventargs.issettingsselected <br> navigationviewselectionchangedeventargs.selecteditem

#### [parallaxsourceoffsetkind](/uwp/api/windows.ui.xaml.controls.parallaxsourceoffsetkind)

parallaxsourceoffsetkind

#### [parallaxview](/uwp/api/windows.ui.xaml.controls.parallaxview)

parallaxview <br> parallaxview.child <br> parallaxview.childproperty <br> parallaxview.horizontalshift <br> parallaxview.horizontalshiftproperty <br> parallaxview.horizontalsourceendoffset <br> parallaxview.horizontalsourceendoffsetproperty <br> parallaxview.horizontalsourceoffsetkind <br> parallaxview.horizontalsourceoffsetkindproperty <br> parallaxview.horizontalsourcestartoffset <br> parallaxview.horizontalsourcestartoffsetproperty <br> parallaxview.ishorizontalshiftclamped <br> parallaxview.ishorizontalshiftclampedproperty <br> parallaxview.isverticalshiftclamped <br> parallaxview.isverticalshiftclampedproperty <br> parallaxview.maxhorizontalshiftratio <br> parallaxview.maxhorizontalshiftratioproperty <br> parallaxview.maxverticalshiftratio <br> parallaxview.maxverticalshiftratioproperty <br> parallaxview.parallaxview <br> parallaxview.refreshautomatichorizontaloffsets <br> parallaxview.refreshautomaticverticaloffsets <br> parallaxview.source <br> parallaxview.sourceproperty <br> parallaxview.verticalshift <br> parallaxview.verticalshiftproperty <br> parallaxview.verticalsourceendoffset <br> parallaxview.verticalsourceendoffsetproperty <br> parallaxview.verticalsourceoffsetkind <br> parallaxview.verticalsourceoffsetkindproperty <br> parallaxview.verticalsourcestartoffset <br> parallaxview.verticalsourcestartoffsetproperty

#### [passwordbox](/uwp/api/windows.ui.xaml.controls.passwordbox)

passwordbox.passwordchanging

#### [passwordboxpasswordchangingeventargs](/uwp/api/windows.ui.xaml.controls.passwordboxpasswordchangingeventargs)

passwordboxpasswordchangingeventargs <br> passwordboxpasswordchangingeventargs.iscontentchanging

#### [pathiconsource](/uwp/api/windows.ui.xaml.controls.pathiconsource)

pathiconsource <br> pathiconsource.data <br> pathiconsource.dataproperty <br> pathiconsource.pathiconsource

#### [personpicture](/uwp/api/windows.ui.xaml.controls.personpicture)

personpicture <br> personpicture.badgeglyph <br> personpicture.badgeglyphproperty <br> personpicture.badgeimagesource <br> personpicture.badgeimagesourceproperty <br> personpicture.badgenumber <br> personpicture.badgenumberproperty <br> personpicture.badgetext <br> personpicture.badgetextproperty <br> personpicture.contact <br> personpicture.contactproperty <br> personpicture.displayname <br> personpicture.displaynameproperty <br> personpicture.initials <br> personpicture.initialsproperty <br> personpicture.isgroup <br> personpicture.isgroupproperty <br> personpicture.personpicture <br> personpicture.prefersmallimage <br> personpicture.prefersmallimageproperty <br> personpicture.profilepicture <br> personpicture.profilepictureproperty

#### [ratingcontrol](/uwp/api/windows.ui.xaml.controls.ratingcontrol)

ratingcontrol <br> ratingcontrol.caption <br> ratingcontrol.captionproperty <br> ratingcontrol.initialsetvalue <br> ratingcontrol.initialsetvalueproperty <br> ratingcontrol.isclearenabled <br> ratingcontrol.isclearenabledproperty <br> ratingcontrol.isreadonly <br> ratingcontrol.isreadonlyproperty <br> ratingcontrol.iteminfo <br> ratingcontrol.iteminfoproperty <br> ratingcontrol.maxrating <br> ratingcontrol.maxratingproperty <br> ratingcontrol.placeholdervalue <br> ratingcontrol.placeholdervalueproperty <br> ratingcontrol.ratingcontrol <br> ratingcontrol.value <br> ratingcontrol.valuechanged <br> ratingcontrol.valueproperty

#### [ratingitemfontinfo](/uwp/api/windows.ui.xaml.controls.ratingitemfontinfo)

ratingitemfontinfo <br> ratingitemfontinfo.disabledglyph <br> ratingitemfontinfo.disabledglyphproperty <br> ratingitemfontinfo.glyph <br> ratingitemfontinfo.glyphproperty <br> ratingitemfontinfo.placeholderglyph <br> ratingitemfontinfo.placeholderglyphproperty <br> ratingitemfontinfo.pointeroverglyph <br> ratingitemfontinfo.pointeroverglyphproperty <br> ratingitemfontinfo.pointeroverplaceholderglyph <br> ratingitemfontinfo.pointeroverplaceholderglyphproperty <br> ratingitemfontinfo.ratingitemfontinfo <br> ratingitemfontinfo.unsetglyph <br> ratingitemfontinfo.unsetglyphproperty

#### [ratingitemimageinfo](/uwp/api/windows.ui.xaml.controls.ratingitemimageinfo)

ratingitemimageinfo <br> ratingitemimageinfo.disabledimage <br> ratingitemimageinfo.disabledimageproperty <br> ratingitemimageinfo.image <br> ratingitemimageinfo.imageproperty <br> ratingitemimageinfo.placeholderimage <br> ratingitemimageinfo.placeholderimageproperty <br> ratingitemimageinfo.pointeroverimage <br> ratingitemimageinfo.pointeroverimageproperty <br> ratingitemimageinfo.pointeroverplaceholderimage <br> ratingitemimageinfo.pointeroverplaceholderimageproperty <br> ratingitemimageinfo.ratingitemimageinfo <br> ratingitemimageinfo.unsetimage <br> ratingitemimageinfo.unsetimageproperty

#### [ratingiteminfo](/uwp/api/windows.ui.xaml.controls.ratingiteminfo)

ratingiteminfo <br> ratingiteminfo.ratingiteminfo

#### [richeditbox](/uwp/api/windows.ui.xaml.controls.richeditbox)

richeditbox.charactercasing <br> richeditbox.charactercasingproperty <br> richeditbox.copyingtoclipboard <br> richeditbox.cuttingtoclipboard <br> richeditbox.disabledformattingaccelerators <br> richeditbox.disabledformattingacceleratorsproperty <br> richeditbox.horizontaltextalignment <br> richeditbox.horizontaltextalignmentproperty

#### [richtextblock](/uwp/api/windows.ui.xaml.controls.richtextblock)

richtextblock.horizontaltextalignment <br> richtextblock.horizontaltextalignmentproperty <br> richtextblock.istexttrimmed <br> richtextblock.istexttrimmedchanged <br> richtextblock.istexttrimmedproperty <br> richtextblock.texthighlighters

#### [richtextblockoverflow](/uwp/api/windows.ui.xaml.controls.richtextblockoverflow)

richtextblockoverflow.istexttrimmed <br> richtextblockoverflow.istexttrimmedchanged <br> richtextblockoverflow.istexttrimmedproperty

#### [splitview](/uwp/api/windows.ui.xaml.controls.splitview)

splitview.paneopened <br> splitview.paneopening

#### [stackpanel](/uwp/api/windows.ui.xaml.controls.stackpanel)

stackpanel.spacing <br> stackpanel.spacingproperty

#### [swipebehavioroninvoked](/uwp/api/windows.ui.xaml.controls.swipebehavioroninvoked)

swipebehavioroninvoked

#### [swipecontrol](/uwp/api/windows.ui.xaml.controls.swipecontrol)

swipecontrol <br> swipecontrol.bottomitems <br> swipecontrol.bottomitemsproperty <br> swipecontrol.close <br> swipecontrol.leftitems <br> swipecontrol.leftitemsproperty <br> swipecontrol.rightitems <br> swipecontrol.rightitemsproperty <br> swipecontrol.swipecontrol <br> swipecontrol.topitems <br> swipecontrol.topitemsproperty

#### [swipeitem](/uwp/api/windows.ui.xaml.controls.swipeitem)

swipeitem <br> swipeitem.background <br> swipeitem.backgroundproperty <br> swipeitem.behavioroninvoked <br> swipeitem.behavioroninvokedproperty <br> swipeitem.command <br> swipeitem.commandparameter <br> swipeitem.commandparameterproperty <br> swipeitem.commandproperty <br> swipeitem.foreground <br> swipeitem.foregroundproperty <br> swipeitem.iconsource <br> swipeitem.iconsourceproperty <br> swipeitem.invoked <br> swipeitem.swipeitem <br> swipeitem.text <br> swipeitem.textproperty

#### [swipeiteminvokedeventargs](/uwp/api/windows.ui.xaml.controls.swipeiteminvokedeventargs)

swipeiteminvokedeventargs <br> swipeiteminvokedeventargs.swipecontrol

#### [swipeitems](/uwp/api/windows.ui.xaml.controls.swipeitems)

swipeitems <br> swipeitems.append <br> swipeitems.clear <br> swipeitems.first <br> swipeitems.getat <br> swipeitems.getmany <br> swipeitems.getview <br> swipeitems.indexof <br> swipeitems.insertat <br> swipeitems.mode <br> swipeitems.modeproperty <br> swipeitems.removeat <br> swipeitems.removeatend <br> swipeitems.replaceall <br> swipeitems.setat <br> swipeitems.size <br> swipeitems.swipeitems

#### [swipemode](/uwp/api/windows.ui.xaml.controls.swipemode)

swipemode

#### [symboliconsource](/uwp/api/windows.ui.xaml.controls.symboliconsource)

symboliconsource <br> symboliconsource.symbol <br> symboliconsource.symboliconsource <br> symboliconsource.symbolproperty

#### [textblock](/uwp/api/windows.ui.xaml.controls.textblock)

textblock.horizontaltextalignment <br> textblock.horizontaltextalignmentproperty <br> textblock.istexttrimmed <br> textblock.istexttrimmedchanged <br> textblock.istexttrimmedproperty <br> textblock.texthighlighters

#### [textbox](/uwp/api/windows.ui.xaml.controls.textbox)

textbox.beforetextchanging <br> textbox.charactercasing <br> textbox.charactercasingproperty <br> textbox.copyingtoclipboard <br> textbox.cuttingtoclipboard <br> textbox.horizontaltextalignment <br> textbox.horizontaltextalignmentproperty <br> textbox.placeholderforeground <br> textbox.placeholderforegroundproperty

#### [textboxbeforetextchangingeventargs](/uwp/api/windows.ui.xaml.controls.textboxbeforetextchangingeventargs)

textboxbeforetextchangingeventargs <br> textboxbeforetextchangingeventargs.cancel <br> textboxbeforetextchangingeventargs.newtext

#### [textcontrolcopyingtoclipboardeventargs](/uwp/api/windows.ui.xaml.controls.textcontrolcopyingtoclipboardeventargs)

textcontrolcopyingtoclipboardeventargs <br> textcontrolcopyingtoclipboardeventargs.handled

#### [textcontrolcuttingtoclipboardeventargs](/uwp/api/windows.ui.xaml.controls.textcontrolcuttingtoclipboardeventargs)

textcontrolcuttingtoclipboardeventargs <br> textcontrolcuttingtoclipboardeventargs.handled

### [windows.ui.xaml.documents](/uwp/api/windows.ui.xaml.documents)

#### [block](/uwp/api/windows.ui.xaml.documents.block)

block.horizontaltextalignment <br> block.horizontaltextalignmentproperty

#### [hyperlink](/uwp/api/windows.ui.xaml.documents.hyperlink)

hyperlink.istabstop <br> hyperlink.istabstopproperty <br> hyperlink.tabindex <br> hyperlink.tabindexproperty

#### [texthighlighter](/uwp/api/windows.ui.xaml.documents.texthighlighter)

texthighlighter <br> texthighlighter.background <br> texthighlighter.backgroundproperty <br> texthighlighter.foreground <br> texthighlighter.foregroundproperty <br> texthighlighter.ranges <br> texthighlighter.texthighlighter

#### [texthighlighterbase](/uwp/api/windows.ui.xaml.documents.texthighlighterbase)

texthighlighterbase

#### [textrange](/uwp/api/windows.ui.xaml.documents.textrange)

textrange

### [windows.ui.xaml.hosting](/uwp/api/windows.ui.xaml.hosting)

#### [designerappmanager](/uwp/api/windows.ui.xaml.hosting.designerappmanager)

designerappmanager <br> designerappmanager.appusermodelid <br> designerappmanager.close <br> designerappmanager.createnewviewasync <br> designerappmanager.designerappexited <br> designerappmanager.designerappmanager <br> designerappmanager.loadobjectintoappasync

#### [designerappview](/uwp/api/windows.ui.xaml.hosting.designerappview)

designerappview <br> designerappview.applicationviewid <br> designerappview.appusermodelid <br> designerappview.close <br> designerappview.updateviewasync <br> designerappview.viewsize <br> designerappview.viewstate

#### [designerappviewstate](/uwp/api/windows.ui.xaml.hosting.designerappviewstate)

designerappviewstate

### [windows.ui.xaml.input](/uwp/api/windows.ui.xaml.input)

#### [characterreceivedroutedeventargs](/uwp/api/windows.ui.xaml.input.characterreceivedroutedeventargs)

characterreceivedroutedeventargs <br> characterreceivedroutedeventargs.character <br> characterreceivedroutedeventargs.handled <br> characterreceivedroutedeventargs.keystatus

#### [keyboardaccelerator](/uwp/api/windows.ui.xaml.input.keyboardaccelerator)

keyboardaccelerator <br> keyboardaccelerator.invoked <br> keyboardaccelerator.isenabled <br> keyboardaccelerator.isenabledproperty <br> keyboardaccelerator.key <br> keyboardaccelerator.keyboardaccelerator <br> keyboardaccelerator.keyproperty <br> keyboardaccelerator.modifiers <br> keyboardaccelerator.modifiersproperty <br> keyboardaccelerator.scopeowner <br> keyboardaccelerator.scopeownerproperty

#### [keyboardacceleratorinvokedeventargs](/uwp/api/windows.ui.xaml.input.keyboardacceleratorinvokedeventargs)

keyboardacceleratorinvokedeventargs <br> keyboardacceleratorinvokedeventargs.element <br> keyboardacceleratorinvokedeventargs.handled

#### [pointerroutedeventargs](/uwp/api/windows.ui.xaml.input.pointerroutedeventargs)

pointerroutedeventargs.isgenerated

#### [processkeyboardacceleratoreventargs](/uwp/api/windows.ui.xaml.input.processkeyboardacceleratoreventargs)

processkeyboardacceleratoreventargs <br> processkeyboardacceleratoreventargs.handled <br> processkeyboardacceleratoreventargs.key <br> processkeyboardacceleratoreventargs.modifiers

### [windows.ui.xaml.markup](/uwp/api/windows.ui.xaml.markup)

#### [markupextension](/uwp/api/windows.ui.xaml.markup.markupextension)

markupextension <br> markupextension.markupextension <br> markupextension.providevalue

#### [markupextensionreturntypeattribute](/uwp/api/windows.ui.xaml.markup.markupextensionreturntypeattribute)

markupextensionreturntypeattribute <br> markupextensionreturntypeattribute.markupextensionreturntypeattribute

### [windows.ui.xaml.media](/uwp/api/windows.ui.xaml.media)

#### [acrylicbackgroundsource](/uwp/api/windows.ui.xaml.media.acrylicbackgroundsource)

acrylicbackgroundsource

#### [acrylicbrush](/uwp/api/windows.ui.xaml.media.acrylicbrush)

acrylicbrush <br> acrylicbrush.acrylicbrush <br> acrylicbrush.alwaysusefallback <br> acrylicbrush.alwaysusefallbackproperty <br> acrylicbrush.backgroundsource <br> acrylicbrush.backgroundsourceproperty <br> acrylicbrush.tintcolor <br> acrylicbrush.tintcolorproperty <br> acrylicbrush.tintopacity <br> acrylicbrush.tintopacityproperty <br> acrylicbrush.tinttransitionduration <br> acrylicbrush.tinttransitiondurationproperty

#### [revealbackgroundbrush](/uwp/api/windows.ui.xaml.media.revealbackgroundbrush)

revealbackgroundbrush <br> revealbackgroundbrush.revealbackgroundbrush

#### [revealborderbrush](/uwp/api/windows.ui.xaml.media.revealborderbrush)

revealborderbrush <br> revealborderbrush.revealborderbrush

#### [revealbrush](/uwp/api/windows.ui.xaml.media.revealbrush)

revealbrush <br> revealbrush.alwaysusefallback <br> revealbrush.alwaysusefallbackproperty <br> revealbrush.color <br> revealbrush.colorproperty <br> revealbrush.getstate <br> revealbrush.revealbrush <br> revealbrush.setstate <br> revealbrush.stateproperty <br> revealbrush.targettheme <br> revealbrush.targetthemeproperty

#### [revealbrushstate](/uwp/api/windows.ui.xaml.media.revealbrushstate)

revealbrushstate

### [windows.ui.xaml](/uwp/api/windows.ui.xaml)

#### [frameworkelement](/uwp/api/windows.ui.xaml.frameworkelement)

frameworkelement.actualtheme <br> frameworkelement.actualthemechanged <br> frameworkelement.actualthemeproperty

#### [uielement](/uwp/api/windows.ui.xaml.uielement)

uielement.characterreceived <br> uielement.characterreceivedevent <br> uielement.getchildrenintabfocusorder <br> uielement.keyboardaccelerators <br> uielement.onprocesskeyboardaccelerators <br> uielement.previewkeydown <br> uielement.previewkeydownevent <br> uielement.previewkeyup <br> uielement.previewkeyupevent <br> uielement.processkeyboardaccelerators <br> uielement.tryinvokekeyboardaccelerator