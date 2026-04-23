---
title: Reserved file and URI scheme names in Windows
description: This topic lists the reserved file and URI scheme names that are not available to your Windows app.
ms.date: 02/11/2025
ms.topic: concept-article
keywords: windows 10, uwp, winui, windows 11
ms.localizationpriority: medium
# customer-intent: As a Windows developer, I want to learn about the reserved file and URI scheme names that aren't available to my Windows app.
---

# Reserved file and URI scheme names in Windows

You can use URI associations to automatically launch your app when another app launches a specific URI scheme. But there are some URI associations that you can’t use because they are reserved. If your app registers for a reserved association, that registration will be ignored. This topic lists the reserved file and URI scheme names that are not available to your app.

## Reserved file types

There are two types of reserved file types: file types reserved for built-in apps and file types reserved for the operating system. When a file type reserved for a built-in app is launched, only the built-in app will launch. Any attempt to register your app with that file type is ignored. Similarly, any attempt to register your app with a file type reserved for the operating system also will be ignored.

File types reserved for built-in apps:

| Types | | | |
|---|---|---|---|
| .aac | .icon | .pem | .wdp |
| .aetx | .jpeg | .png | .wmv |
| .asf | .jxr | .pptm | .xap |
| .bmp | .m4a | .pptx | .xht |
| .cer | .m4r | .qcp | .xhtml |
| .dotm | .m4v | .rtf | .xltm |
| .dotx | .mov | .tif | .xltx |
| .gif | .mp3 | .tiff | .xml |
| .hdp | .mp4 | .txt | .xsl |
| .htm | .one | .url | .zip |
| .html | .onetoc2 | .vcf |  |
| .ico | .p7b | .wav |  |

## File types reserved for the operating system

The following file types are reserved for the operating system:

| Types | | | |
|---|---|---|---|
| .accountpicture-ms | .its | .ops | .url |
| .ade | .jar | .pcd | .vb |
| .adp | .js | .pif | .vbe |
| .app | .jse | .pl | .vbp |
| .appx | .ksh | .plg | .vbs |
| .application | .lnk | .plsc | .vhd |
| .appref-ms | .mad | .prf | .vhdx |
| .asp | .maf | .prg | .vsmacros |
| .bas | .mag | .printerexport | .vsw |
| .bat | .mam | .provxml | .webpnp |
| .cab | .maq | .ps1 | .ws |
| .chm | .mar | .ps1xml | .wsc |
| .cmd | .mas | .ps2 | .wsf |
| .cnt | .mat | .ps2xml | .wsh |
| .com | .mau | .psc1 | .xaml |
| .cpf | .mav | .psc2 | .xdp |
| .cpl | .maw | .psm1 | .xip |
| .crd | .mcf | .pst | .xnk |
| .crds | .mda | .pvw |  |
| .crt | .mdb | .py |  |
| .csh | .mde | .pyc |  |
| .der | .mdt | .pyo |  |
| .dll | .mdw | .rb |  |
| .drv | .mdz | .rbw |  |
| .exe | .msc | .rdp |  |
| .fxp | .msh | .reg |  |
| .fon | .msh1 | .rgu |  |
| .gadget | .msh1xml | .scf |  |
| .grp | .msh2 | .scr |  |
| .hlp | .msh2xml | .shb |  |
| .hme | .mshxml | .shs |  |
| .hpj | .msi | .sys |  |
| .hta | .msp | .theme |  |
| .inf | .mst | .tmp |  |
| .ins | .msu | .tsk |  |
| .isp | .ocx | .ttf |  |

## Reserved URI scheme names

The following URI scheme names are reserved and can't be used by your app:

| Types | | | |
|---|---|---|---|
| application.manifest | inffile | ms-settings:network-dialup | scrfile |
| application.reference | insfile | ms-settings:network-ethernet | scriptletfile |
| batfile | internetshortcut | ms-settings:network-mobilehotspot | shbfile |
| bing | javascript | ms-settings:network-proxy | shcmdfile |
| blob | jscript | ms-settings:network-wifi | shsfile |
| callto | jsefile | ms-settings:nfctransactions | smb |
| cerfile | ldap | ms-settings:notifications | stickynotes |
| chm.file | lnkfile | ms-settings:personalization | sysfile |
| cmdfile | mailto | ms-settings:privacy-calendar | tel |
| comfile | maps | ms-settings:privacy-contacts | telnet |
| cplfile | microsoft.powershellscript.1 | ms-settings:privacy-customdevices | tn3270 |
| dllfile | ms-accountpictureprovider | ms-settings:privacy-feedback | ttffile |
| drvfile | ms-appdata | ms-settings:privacy-location | unknown |
| dtmf | ms-appx | ms-settings:privacy-messaging | usertileprovider |
| exefile | ms-autoplay | ms-settings:privacy-microphone | vbefile |
| explorer.assocactionid.burnselection | ms-excel | ms-settings:privacy-speechtyping | vbscript |
| explorer.assocactionid.closesession | msi.package | ms-settings:privacy-webcam | vbsfile |
| explorer.assocactionid.erasedisc | msi.patch | ms-settings:proximity | wallet |
| explorer.assocactionid.zipselection | ms-powerpoint | ms-settings:regionlanguage | windows.gadget |
| explorer.assocprotocol.search-ms | ms-settings | ms-settings:screenrotation | windowsmediacenterapp |
| explorer.burnselection | ms-settings:batterysaver | ms-settings:speech | windowsmediacenterssl |
| explorer.burnselectionexplorer.closesession | ms-settings:batterysaver-settings | ms-settings:storagesense | windowsmediacenterweb |
| explorer.closesession | ms-settings:batterysaver-usagedetails | ms-settings:windowsupdate | wmp11.assocprotocol.mms |
| explorer.erasedisc | ms-settings:bluetooth | ms-settings:workplace | wsffile |
| explorer.zipselection | ms-settings:connecteddevices | ms-windows-store | wsfile |
| file | ms-settings:cortanasearch | ms-word | wshfile |
| fonfile | ms-settings:datasense | ocxfile | xbls |
| hlpfile | ms-settings:dateandtime | office | zune |
| htafile | ms-settings:display | onenote |  |
| http | ms-settings:lockscreen | piffile |  |
| https | ms-settings:maps | regfile |  |
| iehistory | ms-settings:network-airplanemode | res |  |
| ierss | ms-settings:network-cellular | rlogin |  |

## Related content

- [Launch a Windows app with a URI](index.md)
- [Handle URI activation](handle-uri-activation.md)
