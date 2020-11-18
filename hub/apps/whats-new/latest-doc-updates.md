---
description: Discover the latest additions to the Windows developer docs.
title: Latest updates to the Windows developer docs
ms.topic: article
ms.date: 11/05/2020
ms.localizationpriority: medium
ms.author: quradic
author: QuinnRadich
---

# Latest updates to the Windows developer docs

The Windows developer docs are regularly updated with new and improved information and content. Here is a summary of changes as of November 5th, 2020.

Note: For a specific list of APIs added as part of Windows 10 build 19041 (also known as 2004), please see [this list](/windows/uwp/whats-new/windows-10-build-19041-api-diff).


## News

This month we translated another batch of absolute links to relative links in order to speed up navigation and improve the offline reading experience. Please continue to let us know if you find broken links: you can provide feedback directly from most pages, or you can reach us on Twitter with our handle [@WindowsDocs](https://www.twitter.com/windowsdocs).

Highlights this month include:


### New topics

* [Classic Console APIs versus Virtual Terminal Sequences](https://docs.microsoft.com/windows/console/classic-vs-vt)
* [Windows Console and Terminal Ecosystem Roadmap](https://docs.microsoft.com/windows/console/ecosystem-roadmap)
* [Windows API Sets](https://docs.microsoft.com/windows/win32/apiindex/windows-apisets)
* [API set loader operation](https://docs.microsoft.com/windows/win32/apiindex/api-set-loader-operation)
* [Detect API set availability](https://docs.microsoft.com/windows/win32/apiindex/detect-api-set-availability)
* [Windows umbrella libraries](https://docs.microsoft.com/windows/win32/apiindex/windows-umbrella-libraries)


### New and updated samples

* [Walkthrough: Generate a .NET 5 projection from a C++/WinRT component and distribute the NuGet](https://docs.microsoft.com/windows/uwp/csharp-winrt/net-projection-from-cppwinrt-component)


### Other content of interest

* [Azure Communication Services](https://docs.microsoft.com/azure/communication-services/overview)
* [Azure App Configuration Python quickstart](https://docs.microsoft.com/azure/azure-app-configuration/quickstart-python)

### Deprecated content

* [BarcodeScanner.GetSupportedProfiles](https://docs.microsoft.com/uwp/api/windows.devices.pointofservice.barcodescanner.getsupportedprofiles?view=winrt-19041)

### Updated documentation

* [Console docs](https://github.com/MicrosoftDocs/Console-Docs)
* [MIDL 3.0 conceptual content](https://docs.microsoft.com/uwp/midl-3/intro#parameters)
* [Get started with Docker overview](https://docs.microsoft.com/windows/dev-environment/docker/overview)
* [Get started mounting a Linux disk in WSL 2](https://docs.microsoft.com/windows/wsl/wsl2-mount-disk)

The following API reference topics have seen significant updates in the past month:

### Win32 API reference
<ul>
<li><a href="https://docs.microsoft.com/windows/win32/api/commctrl/nf-commctrl-createmappedbitmap">CreateMappedBitmap function (commctrl.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/commctrl/nf-commctrl-createtoolbarex">CreateToolbarEx function (commctrl.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/commctrl/nf-commctrl-getwindowsubclass">GetWindowSubclass function (commctrl.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/commctrl/nf-commctrl-imagelist_addmasked">ImageList_AddMasked function (commctrl.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/commctrl/nf-commctrl-imagelist_create">ImageList_Create function (commctrl.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/commctrl/nf-commctrl-imagelist_draw">ImageList_Draw function (commctrl.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/commctrl/nf-commctrl-imagelist_replaceicon">ImageList_ReplaceIcon function (commctrl.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/commctrl/nf-commctrl-imagelist_setoverlayimage">ImageList_SetOverlayImage function (commctrl.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/commdlg/nf-commdlg-commdlgextendederror">CommDlgExtendedError function (commdlg.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/commdlg/nf-commdlg-findtexta">FindTextA function (commdlg.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/commdlg/nf-commdlg-findtextw">FindTextW function (commdlg.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/commdlg/nf-commdlg-getopenfilenamea">GetOpenFileNameA function (commdlg.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/commdlg/nf-commdlg-getopenfilenamew">GetOpenFileNameW function (commdlg.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/commdlg/nf-commdlg-getsavefilenamea">GetSaveFileNameA function (commdlg.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/commdlg/nf-commdlg-getsavefilenamew">GetSaveFileNameW function (commdlg.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/dde/nf-dde-unpackddelparam">UnpackDDElParam function (dde.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/dpa_dsa/nf-dpa_dsa-dpa_clone">DPA_Clone function (dpa_dsa.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/dpa_dsa/nf-dpa_dsa-dpa_createex">DPA_CreateEx function (dpa_dsa.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/dpa_dsa/nf-dpa_dsa-dpa_destroycallback">DPA_DestroyCallback function (dpa_dsa.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/dpa_dsa/nf-dpa_dsa-dpa_enumcallback">DPA_EnumCallback function (dpa_dsa.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/dpa_dsa/nf-dpa_dsa-dpa_getptr">DPA_GetPtr function (dpa_dsa.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/dpa_dsa/nf-dpa_dsa-dpa_getptrindex">DPA_GetPtrIndex function (dpa_dsa.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/dpa_dsa/nf-dpa_dsa-dpa_grow">DPA_Grow function (dpa_dsa.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/dpa_dsa/nf-dpa_dsa-dpa_insertptr">DPA_InsertPtr function (dpa_dsa.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/dpa_dsa/nf-dpa_dsa-dpa_search">DPA_Search function (dpa_dsa.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/dpa_dsa/nf-dpa_dsa-dpa_setptr">DPA_SetPtr function (dpa_dsa.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/dpa_dsa/nf-dpa_dsa-dpa_sort">DPA_Sort function (dpa_dsa.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/dpa_dsa/nf-dpa_dsa-dsa_create">DSA_Create function (dpa_dsa.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/dpa_dsa/nf-dpa_dsa-dsa_deleteitem">DSA_DeleteItem function (dpa_dsa.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/dpa_dsa/nf-dpa_dsa-dsa_destroycallback">DSA_DestroyCallback function (dpa_dsa.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/dpa_dsa/nf-dpa_dsa-dsa_enumcallback">DSA_EnumCallback function (dpa_dsa.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/dpa_dsa/nf-dpa_dsa-dsa_getitem">DSA_GetItem function (dpa_dsa.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/dpa_dsa/nf-dpa_dsa-dsa_getitemptr">DSA_GetItemPtr function (dpa_dsa.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/dpa_dsa/nf-dpa_dsa-dsa_insertitem">DSA_InsertItem function (dpa_dsa.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/dpa_dsa/nf-dpa_dsa-dsa_setitem">DSA_SetItem function (dpa_dsa.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/objbase/nf-objbase-bindmoniker">BindMoniker function (objbase.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/objbase/nf-objbase-codosdatetimetofiletime">CoDosDateTimeToFileTime function (objbase.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/objbase/nf-objbase-cofiletimetodosdatetime">CoFileTimeToDosDateTime function (objbase.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/objbase/nf-objbase-cofreealllibraries">CoFreeAllLibraries function (objbase.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/objbase/nf-objbase-cofreelibrary">CoFreeLibrary function (objbase.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/objbase/nf-objbase-coinitialize">CoInitialize function (objbase.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/objbase/nf-objbase-coloadlibrary">CoLoadLibrary function (objbase.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/objbase/nf-objbase-createantimoniker">CreateAntiMoniker function (objbase.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/objbase/nf-objbase-createdataadviseholder">CreateDataAdviseHolder function (objbase.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/objbase/nf-objbase-createdatacache">CreateDataCache function (objbase.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/objbase/nf-objbase-createobjrefmoniker">CreateObjrefMoniker function (objbase.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/objbase/nf-objbase-createpointermoniker">CreatePointerMoniker function (objbase.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/objbase/nf-objbase-getrunningobjecttable">GetRunningObjectTable function (objbase.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/ole/nf-ole-oledraw">OleDraw function (ole.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/ole/nf-ole-oleloadfromstream">OleLoadFromStream function (ole.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/ole/nf-ole-olesavetostream">OleSaveToStream function (ole.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/ole2/nf-ole2-createdataadviseholder">CreateDataAdviseHolder function (ole2.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/ole2/nf-ole2-createoleadviseholder">CreateOleAdviseHolder function (ole2.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/ole2/nf-ole2-olecreateembeddinghelper">OleCreateEmbeddingHelper function (ole2.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/ole2/nf-ole2-olecreatefromdata">OleCreateFromData function (ole2.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/ole2/nf-ole2-olecreatemenudescriptor">OleCreateMenuDescriptor function (ole2.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/ole2/nf-ole2-olegetautoconvert">OleGetAutoConvert function (ole2.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/ole2/nf-ole2-olegetclipboard">OleGetClipboard function (ole2.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/ole2/nf-ole2-olegeticonofclass">OleGetIconOfClass function (ole2.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/ole2/nf-ole2-oleinitialize">OleInitialize function (ole2.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/ole2/nf-ole2-oleiscurrentclipboard">OleIsCurrentClipboard function (ole2.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/ole2/nf-ole2-olequerycreatefromdata">OleQueryCreateFromData function (ole2.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/ole2/nf-ole2-oleregenumverbs">OleRegEnumVerbs function (ole2.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/ole2/nf-ole2-olesavetostream">OleSaveToStream function (ole2.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/ole2/nf-ole2-olesetmenudescriptor">OleSetMenuDescriptor function (ole2.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/ole2/nf-ole2-oleuninitialize">OleUninitialize function (ole2.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/ole2/nf-ole2-writefmtusertypestg">WriteFmtUserTypeStg function (ole2.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/prsht/nf-prsht-createpropertysheetpagea">CreatePropertySheetPageA function (prsht.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/prsht/nf-prsht-createpropertysheetpagew">CreatePropertySheetPageW function (prsht.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/prsht/nf-prsht-propertysheeta">PropertySheetA function (prsht.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/prsht/nf-prsht-propertysheetw">PropertySheetW function (prsht.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/setupapi/nf-setupapi-setupcloseinffile">SetupCloseInfFile function (setupapi.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/setupapi/nf-setupapi-setupdigetdevicepropertyw">SetupDiGetDevicePropertyW function (setupapi.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/setupapi/nf-setupapi-setupdiopendeviceinfow">SetupDiOpenDeviceInfoW function (setupapi.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/setupapi/nf-setupapi-setupfindnextline">SetupFindNextLine function (setupapi.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/setupapi/nf-setupapi-setupgetfieldcount">SetupGetFieldCount function (setupapi.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/setupapi/nf-setupapi-setupgetlinecounta">SetupGetLineCountA function (setupapi.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/setupapi/nf-setupapi-setupgetlinecountw">SetupGetLineCountW function (setupapi.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/setupapi/nf-setupapi-setupgetlinetexta">SetupGetLineTextA function (setupapi.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/setupapi/nf-setupapi-setupgetlinetextw">SetupGetLineTextW function (setupapi.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/setupapi/nf-setupapi-setupgetstringfielda">SetupGetStringFieldA function (setupapi.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/setupapi/nf-setupapi-setupgetstringfieldw">SetupGetStringFieldW function (setupapi.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/setupapi/nf-setupapi-setupgetthreadlogtoken">SetupGetThreadLogToken function (setupapi.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/setupapi/nf-setupapi-setupiteratecabineta">SetupIterateCabinetA function (setupapi.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/setupapi/nf-setupapi-setupiteratecabinetw">SetupIterateCabinetW function (setupapi.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/setupapi/nf-setupapi-setuplogerrora">SetupLogErrorA function (setupapi.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/setupapi/nf-setupapi-setuplogerrorw">SetupLogErrorW function (setupapi.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/setupapi/nf-setupapi-setupopeninffilea">SetupOpenInfFileA function (setupapi.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/setupapi/nf-setupapi-setupopenlog">SetupOpenLog function (setupapi.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/setupapi/nf-setupapi-setupverifyinffilea">SetupVerifyInfFileA function (setupapi.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/setupapi/nf-setupapi-setupverifyinffilew">SetupVerifyInfFileW function (setupapi.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/setupapi/nf-setupapi-setupwritetextlog">SetupWriteTextLog function (setupapi.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/shellapi/nf-shellapi-assoccreateforclasses">AssocCreateForClasses function (shellapi.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/shellapi/nf-shellapi-duplicateicon">DuplicateIcon function (shellapi.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/shellapi/nf-shellapi-extracticonexa">ExtractIconExA function (shellapi.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/shellapi/nf-shellapi-extracticonexw">ExtractIconExW function (shellapi.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/shellapi/nf-shellapi-shell_notifyiconw">Shell_NotifyIconW function (shellapi.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/shellapi/nf-shellapi-shgetimagelist">SHGetImageList function (shellapi.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/shellapi/nf-shellapi-shgetstockiconinfo">SHGetStockIconInfo function (shellapi.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/shellapi/nf-shellapi-shsetlocalizedname">SHSetLocalizedName function (shellapi.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/shlobj_core/nf-shlobj_core-dad_autoscroll">DAD_AutoScroll function (shlobj_core.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/shlobj_core/nf-shlobj_core-dad_dragenterex2">DAD_DragEnterEx2 function (shlobj_core.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/shlobj_core/nf-shlobj_core-dad_setdragimage">DAD_SetDragImage function (shlobj_core.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/shlobj_core/nf-shlobj_core-ilcreatefrompath">ILCreateFromPath function (shlobj_core.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/shlobj_core/nf-shlobj_core-ilcreatefrompatha">ILCreateFromPathA function (shlobj_core.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/shlobj_core/nf-shlobj_core-ilcreatefrompathw">ILCreateFromPathW function (shlobj_core.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/shlobj_core/nf-shlobj_core-ilsavetostream">ILSaveToStream function (shlobj_core.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/shlobj_core/nf-shlobj_core-pickicondlg">PickIconDlg function (shlobj_core.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/shlobj_core/nf-shlobj_core-restartdialog">RestartDialog function (shlobj_core.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/shlobj_core/nf-shlobj_core-shaddtorecentdocs">SHAddToRecentDocs function (shlobj_core.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/shlobj_core/nf-shlobj_core-shchangenotify">SHChangeNotify function (shlobj_core.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/shlobj_core/nf-shlobj_core-shcreateshellitem">SHCreateShellItem function (shlobj_core.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/shlobj_core/nf-shlobj_core-shdodragdrop">SHDoDragDrop function (shlobj_core.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/shlobj_core/nf-shlobj_core-shell_getimagelists">Shell_GetImageLists function (shlobj_core.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/shlobj_core/nf-shlobj_core-shgetrealidl">SHGetRealIDL function (shlobj_core.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/shlobj_core/nf-shlobj_core-shgetsetsettings">SHGetSetSettings function (shlobj_core.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/shlobj_core/nf-shlobj_core-shilcreatefrompath">SHILCreateFromPath function (shlobj_core.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/shlobj_core/nf-shlobj_core-shlimitinputedit">SHLimitInputEdit function (shlobj_core.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/shlobj_core/nf-shlobj_core-shopenwithdialog">SHOpenWithDialog function (shlobj_core.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winbase/nf-winbase-backupeventloga">BackupEventLogA function (winbase.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winbase/nf-winbase-backupeventlogw">BackupEventLogW function (winbase.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winbase/nf-winbase-cleareventloga">ClearEventLogA function (winbase.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winbase/nf-winbase-cleareventlogw">ClearEventLogW function (winbase.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winbase/nf-winbase-closeeventlog">CloseEventLog function (winbase.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winbase/nf-winbase-deleteumsthreadcontext">DeleteUmsThreadContext function (winbase.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winbase/nf-winbase-encryptfilea">EncryptFileA function (winbase.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winbase/nf-winbase-encryptfilew">EncryptFileW function (winbase.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winbase/nf-winbase-enterumsschedulingmode">EnterUmsSchedulingMode function (winbase.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winbase/nf-winbase-getcurrenthwprofilea">GetCurrentHwProfileA function (winbase.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winbase/nf-winbase-getcurrenthwprofilew">GetCurrentHwProfileW function (winbase.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winbase/nf-winbase-getnumberofeventlogrecords">GetNumberOfEventLogRecords function (winbase.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winbase/nf-winbase-getumscompletionlistevent">GetUmsCompletionListEvent function (winbase.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winbase/nf-winbase-openbackupeventloga">OpenBackupEventLogA function (winbase.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winbase/nf-winbase-openbackupeventlogw">OpenBackupEventLogW function (winbase.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winbase/nf-winbase-queryumsthreadinformation">QueryUmsThreadInformation function (winbase.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winbase/nf-winbase-readencryptedfileraw">ReadEncryptedFileRaw function (winbase.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winbase/nf-winbase-readeventloga">ReadEventLogA function (winbase.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winbase/nf-winbase-readeventlogw">ReadEventLogW function (winbase.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winbase/nf-winbase-umsthreadyield">UmsThreadYield function (winbase.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winnls/nf-winnls-notifyuilanguagechange">NotifyUILanguageChange function (winnls.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winperf/nc-winperf-pm_collect_proc">PM_COLLECT_PROC (winperf.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winsafer/nf-winsafer-safercreatelevel">SaferCreateLevel function (winsafer.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winsafer/nf-winsafer-safergetlevelinformation">SaferGetLevelInformation function (winsafer.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winsafer/nf-winsafer-safergetpolicyinformation">SaferGetPolicyInformation function (winsafer.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winsafer/nf-winsafer-saferidentifylevel">SaferIdentifyLevel function (winsafer.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winsafer/nf-winsafer-saferrecordeventlogentry">SaferRecordEventLogEntry function (winsafer.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winsafer/nf-winsafer-safersetlevelinformation">SaferSetLevelInformation function (winsafer.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winsafer/nf-winsafer-safersetpolicyinformation">SaferSetPolicyInformation function (winsafer.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-addclipboardformatlistener">AddClipboardFormatListener function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-adjustwindowrectex">AdjustWindowRectEx function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-animatewindow">AnimateWindow function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-appendmenua">AppendMenuA function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-appendmenuw">AppendMenuW function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-attachthreadinput">AttachThreadInput function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-bringwindowtotop">BringWindowToTop function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-callmsgfiltera">CallMsgFilterA function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-callmsgfilterw">CallMsgFilterW function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-changedisplaysettingsa">ChangeDisplaySettingsA function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-changedisplaysettingsw">ChangeDisplaySettingsW function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-changewindowmessagefilterex">ChangeWindowMessageFilterEx function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-checkdlgbutton">CheckDlgButton function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-checkmenuitem">CheckMenuItem function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-checkmenuradioitem">CheckMenuRadioItem function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-childwindowfrompoint">ChildWindowFromPoint function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-childwindowfrompointex">ChildWindowFromPointEx function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-closedesktop">CloseDesktop function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-closewindowstation">CloseWindowStation function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-createcaret">CreateCaret function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-createdesktopa">CreateDesktopA function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-createdesktopw">CreateDesktopW function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-createdialogindirectparama">CreateDialogIndirectParamA function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-createdialogindirectparamw">CreateDialogIndirectParamW function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-createiconindirect">CreateIconIndirect function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-defrawinputproc">DefRawInputProc function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-defwindowproca">DefWindowProcA function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-defwindowprocw">DefWindowProcW function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-deletemenu">DeleteMenu function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-destroycaret">DestroyCaret function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-destroywindow">DestroyWindow function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-dialogboxindirectparama">DialogBoxIndirectParamA function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-dialogboxindirectparamw">DialogBoxIndirectParamW function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-dispatchmessage">DispatchMessage function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-dispatchmessagea">DispatchMessageA function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-dispatchmessagew">DispatchMessageW function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-drawedge">DrawEdge function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-drawicon">DrawIcon function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-drawiconex">DrawIconEx function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-drawtexta">DrawTextA function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-drawtextexa">DrawTextExA function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-drawtextexw">DrawTextExW function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-drawtextw">DrawTextW function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-enablemenuitem">EnableMenuItem function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-enablescrollbar">EnableScrollBar function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-endmenu">EndMenu function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-endpaint">EndPaint function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-enumdisplaydevicesa">EnumDisplayDevicesA function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-enumdisplaydevicesw">EnumDisplayDevicesW function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-fillrect">FillRect function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-findwindowa">FindWindowA function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-findwindoww">FindWindowW function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-flashwindowex">FlashWindowEx function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-getancestor">GetAncestor function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-getcapture">GetCapture function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-getcaretblinktime">GetCaretBlinkTime function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-getclassinfoa">GetClassInfoA function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-getclassinfoexa">GetClassInfoExA function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-getclassinfoexw">GetClassInfoExW function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-getclassinfow">GetClassInfoW function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-getclasslongptra">GetClassLongPtrA function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-getclasslongptrw">GetClassLongPtrW function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-getclassname">GetClassName function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-getclassnamea">GetClassNameA function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-getclassnamew">GetClassNameW function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-getclassword">GetClassWord function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-getclipboardformatnamea">GetClipboardFormatNameA function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-getclipboardformatnamew">GetClipboardFormatNameW function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-getclipboardviewer">GetClipboardViewer function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-getcursorpos">GetCursorPos function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-getdesktopwindow">GetDesktopWindow function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-getdlgitemint">GetDlgItemInt function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-getdlgitemtexta">GetDlgItemTextA function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-getdlgitemtextw">GetDlgItemTextW function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-getdoubleclicktime">GetDoubleClickTime function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-getfocus">GetFocus function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-getforegroundwindow">GetForegroundWindow function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-getkeyboardstate">GetKeyboardState function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-getlastactivepopup">GetLastActivePopup function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-getmenudefaultitem">GetMenuDefaultItem function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-getmenuinfo">GetMenuInfo function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-getmenuitemcount">GetMenuItemCount function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-getmenuitemid">GetMenuItemID function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-getmenuiteminfoa">GetMenuItemInfoA function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-getmenuiteminfow">GetMenuItemInfoW function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-getmenustate">GetMenuState function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-getmessageextrainfo">GetMessageExtraInfo function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-getmessagetime">GetMessageTime function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-getparent">GetParent function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-getpointerdevices">GetPointerDevices function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-getpointerframeinfohistory">GetPointerFrameInfoHistory function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-getpointerframetouchinfo">GetPointerFrameTouchInfo function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-getpointertype">GetPointerType function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-getprocessdefaultlayout">GetProcessDefaultLayout function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-getrawinputdevicelist">GetRawInputDeviceList function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-getscrollbarinfo">GetScrollBarInfo function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-getscrollinfo">GetScrollInfo function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-getshellwindow">GetShellWindow function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-getsubmenu">GetSubMenu function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-getsystemmetrics">GetSystemMetrics function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-getsystemmetricsfordpi">GetSystemMetricsForDpi function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-gettitlebarinfo">GetTitleBarInfo function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-gettopwindow">GetTopWindow function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-gettouchinputinfo">GetTouchInputInfo function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-getwindow">GetWindow function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-getwindowtexta">GetWindowTextA function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-getwindowtextlengtha">GetWindowTextLengthA function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-getwindowtextlengthw">GetWindowTextLengthW function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-getwindowtextw">GetWindowTextW function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-getwindowthreadprocessid">GetWindowThreadProcessId function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-initializetouchinjection">InitializeTouchInjection function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-injecttouchinput">InjectTouchInput function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-insendmessage">InSendMessage function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-insertmenua">InsertMenuA function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-insertmenuitema">InsertMenuItemA function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-insertmenuitemw">InsertMenuItemW function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-insertmenuw">InsertMenuW function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-invalidaterect">InvalidateRect function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-invertrect">InvertRect function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-isclipboardformatavailable">IsClipboardFormatAvailable function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-isdialogmessagea">IsDialogMessageA function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-isdialogmessagew">IsDialogMessageW function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-isdlgbuttonchecked">IsDlgButtonChecked function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-ishungappwindow">IsHungAppWindow function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-isiconic">IsIconic function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-ismenu">IsMenu function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-iswindowunicode">IsWindowUnicode function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-iswineventhookinstalled">IsWinEventHookInstalled function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-iszoomed">IsZoomed function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-loadicona">LoadIconA function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-loadiconw">LoadIconW function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-loadimagea">LoadImageA function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-locksetforegroundwindow">LockSetForegroundWindow function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-lockworkstation">LockWorkStation function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-logicaltophysicalpoint">LogicalToPhysicalPoint function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-mapwindowpoints">MapWindowPoints function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-messagebeep">MessageBeep function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-messagebox">MessageBox function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-messageboxa">MessageBoxA function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-messageboxindirecta">MessageBoxIndirectA function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-messageboxindirectw">MessageBoxIndirectW function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-messageboxw">MessageBoxW function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-modifymenua">ModifyMenuA function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-modifymenuw">ModifyMenuW function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-monitorfrompoint">MonitorFromPoint function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-monitorfromrect">MonitorFromRect function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-monitorfromwindow">MonitorFromWindow function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-oemtocharbuffa">OemToCharBuffA function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-oemtocharbuffw">OemToCharBuffW function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-openinputdesktop">OpenInputDesktop function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-peekmessagea">PeekMessageA function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-peekmessagew">PeekMessageW function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-physicaltologicalpoint">PhysicalToLogicalPoint function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-postmessagea">PostMessageA function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-postmessagew">PostMessageW function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-privateextracticonsa">PrivateExtractIconsA function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-privateextracticonsw">PrivateExtractIconsW function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-querydisplayconfig">QueryDisplayConfig function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-realgetwindowclassw">RealGetWindowClassW function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-redrawwindow">RedrawWindow function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-registerclipboardformata">RegisterClipboardFormatA function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-registerclipboardformatw">RegisterClipboardFormatW function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-registerdevicenotificationa">RegisterDeviceNotificationA function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-registerdevicenotificationw">RegisterDeviceNotificationW function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-removemenu">RemoveMenu function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-removepropa">RemovePropA function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-removepropw">RemovePropW function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-scrolldc">ScrollDC function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-scrollwindow">ScrollWindow function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-scrollwindowex">ScrollWindowEx function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-senddlgitemmessagea">SendDlgItemMessageA function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-senddlgitemmessagew">SendDlgItemMessageW function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-sendmessagetimeouta">SendMessageTimeoutA function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-sendmessagetimeoutw">SendMessageTimeoutW function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-setcaretblinktime">SetCaretBlinkTime function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-setclipboarddata">SetClipboardData function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-setcoalescabletimer">SetCoalescableTimer function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-setcursorpos">SetCursorPos function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-setdlgitemtexta">SetDlgItemTextA function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-setdlgitemtextw">SetDlgItemTextW function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-setdoubleclicktime">SetDoubleClickTime function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-setfocus">SetFocus function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-setforegroundwindow">SetForegroundWindow function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-setlayeredwindowattributes">SetLayeredWindowAttributes function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-setmenuinfo">SetMenuInfo function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-setmenuiteminfoa">SetMenuItemInfoA function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-setmenuiteminfow">SetMenuItemInfoW function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-setmessageextrainfo">SetMessageExtraInfo function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-setparent">SetParent function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-setprocesswindowstation">SetProcessWindowStation function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-setpropa">SetPropA function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-setpropw">SetPropW function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-setwindowdisplayaffinity">SetWindowDisplayAffinity function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-setwindowlonga">SetWindowLongA function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-setwindowlongptra">SetWindowLongPtrA function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-setwindowlongptrw">SetWindowLongPtrW function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-setwindowlongw">SetWindowLongW function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-setwindowpos">SetWindowPos function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-setwindowshookexa">SetWindowsHookExA function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-setwindowshookexw">SetWindowsHookExW function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-setwindowtexta">SetWindowTextA function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-setwindowtextw">SetWindowTextW function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-showscrollbar">ShowScrollBar function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-showwindow">ShowWindow function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-showwindowasync">ShowWindowAsync function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-shutdownblockreasoncreate">ShutdownBlockReasonCreate function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-shutdownblockreasondestroy">ShutdownBlockReasonDestroy function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-systemparametersinfoa">SystemParametersInfoA function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-systemparametersinfow">SystemParametersInfoW function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-trackmouseevent">TrackMouseEvent function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-updatelayeredwindow">UpdateLayeredWindow function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-waitforinputidle">WaitForInputIdle function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-waitmessage">WaitMessage function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-windowfromdc">WindowFromDC function (winuser.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/wtsapi32/nf-wtsapi32-wtsdisconnectsession">WTSDisconnectSession function (wtsapi32.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/wtsapi32/nf-wtsapi32-wtsfreememory">WTSFreeMemory function (wtsapi32.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/wtsapi32/nf-wtsapi32-wtsfreememoryexa">WTSFreeMemoryExA function (wtsapi32.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/wtsapi32/nf-wtsapi32-wtsfreememoryexw">WTSFreeMemoryExW function (wtsapi32.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/wtsapi32/nf-wtsapi32-wtslogoffsession">WTSLogoffSession function (wtsapi32.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/wtsapi32/nf-wtsapi32-wtsvirtualchannelopenex">WTSVirtualChannelOpenEx function (wtsapi32.h) </a></li>
<li><a href="https://docs.microsoft.com/windows/win32/api/wtsapi32/nf-wtsapi32-wtsvirtualchannelquery">WTSVirtualChannelQuery function (wtsapi32.h) </a></li>
</ul>

### UWP API reference

<ul>
<li><a href="https://docs.microsoft.com/uwp/api/windows.devices.bluetooth.bluetoothcachemode">Windows.Devices.Bluetooth.BluetoothCacheMode</a></li>
<li><a href="https://docs.microsoft.com/uwp/api/windows.devices.bluetooth.bluetoothledevice.fromidasync">Windows.Devices.Bluetooth.BluetoothLEDevice.FromIdAsync(System.String)</a></li>
<li><a href="https://docs.microsoft.com/uwp/api/windows.devices.bluetooth.bluetoothledevice.gattserviceschanged">Windows.Devices.Bluetooth.BluetoothLEDevice.GattServicesChanged</a></li>
<li><a href="https://docs.microsoft.com/uwp/api/windows.devices.bluetooth.bluetoothledevice.namechanged">Windows.Devices.Bluetooth.BluetoothLEDevice.NameChanged</a></li>
<li><a href="https://docs.microsoft.com/uwp/api/windows.ui.xaml.automation.peers.patterninterface">Windows.UI.Xaml.Automation.Peers.PatternInterface</a></li>
<li><a href="https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.listviewbase.candragitems">Windows.UI.Xaml.Controls.ListViewBase.CanDragItems</a></li>
</ul>


