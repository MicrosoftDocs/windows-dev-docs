---
title: Windows App SDK and suppported Windows releases.
description: Details on the Windows OS versions that are supported by the Windows App SDK.
ms.topic: article
ms.date: 04/25/2024
keywords: Windows, Windows app development, Windows App SDK 
ms.localizationpriority: medium
---

# Windows App SDK and suppported Windows releases

This topic identifies the versions of the [Windows client](/windows/) and [Windows Server](/windows-server/) operating systems (OS) supported by the [Windows App SDK](index.md), along with the various servicing categories, channels, and SKUs for each OS.

This table summarizes the Windows client and server releases supported by the Windows App SDK (the following sections provide details on the various servicing categories, channels, and SKUs for each OS).

<table>
<tr>
<th>Windows App SDK</th><th>Release date</th><th>Windows client</th><th>Windows Server</th>
</tr>
<tr>
<td valign="top">1.4</td>
<td valign="top">2023-08-29</td>
<td valign="top">
Win10 1809<br>
Win10 21H2<br>
Win10 22H2<br>
Win11 21H2<br>
Win11 22H2
</td>
<td valign="top">
Server 2019<br>
Server 2022
</td>
</tr>
<tr>
<td valign="top">1.5</td>
<td valign="top">2024-02-29</td>
<td valign="top">
Win10 1809<br>
Win10 21H2<br>
Win10 22H2<br>
Win11 21H2<br>
Win11 22H2
</td>
<td valign="top">
Server 2019<br>
Server 2022
</td>
</tr>
</table>

## Windows client SKUs and channels

The Windows client OS runs on personal devices such as workstations, desktop PCs, laptops, and tablets.

There are four servicing categories that apply to various combinations of Windows client SKUs and servicing channels.

<table>
<tbody>
<tr>
<th colspan="2" rowspan="2">Servicing channel</th>
<th>SKU</th>
<th>SKU</th>
</tr>
<tr>
<th>Home, Pro</th>
<th>Enterprise</th>
</tr>
<tr>
<td colspan="2">GA*</td>
<td>Shortest servicing</td>
<td>Short servicing</td>
</tr>
<tr>
<td rowspan="2" valign="center">LTSC**</td>
<td>Mainstream</td>
<td>N/A</td>
<td>Long servicing</td>
</tr>
<tr>
<td>Extended</td>
<td>N/A</td>
<td>Longest servicing</td>
</tr>
</tbody>
</table>

\* The General Availability (GA) servicing channel includes the majority of Windows devices.

** The Long Term Servicing Channel (LTSC) includes devices that need to continue running older versions of Windows.

Five older versions of Windows client are still in service for at least one servicing category. Each version will continue to be supported by the Windows App SDK until the latest applicable Windows servicing end-date for that version. The following table shows the end-date for each servicing category.

<table>
<tr>
<th rowspan="2">Version</th>
<th rowspan="2">Build</th>
<th colspan="4">Servicing end-dates</th>
<th rowspan="2">Max end-dates</th>
</tr>
<tr>
<th>GA Home, Pro</td>
<th>GA Enterprise</td>
<th>LTSC Mainstream</td>
<th>LTSC Extended</td>
</tr>
<tr>
<td>Win10 1809</td>
<td>17763</td>
<td>Past</td>
<td>Past</td>
<td>Past</td>
<td>2029-01-09</td>
<td>2029-01-09</td>
</tr>
<tr>
<td>Win10 21H2</td>
<td>19044</td>
<td>Past</td>
<td>2024-06-11</td>
<td>2027-01-12</td>
<td>2032-01-13</td>
<td>2032-01-13</td>
</tr>
<tr>
<td>Win10 22H2</td>
<td>19045</td>
<td>2025-10-14</td>
<td>2025-10-14</td>
<td>N/A</td>
<td>N/A</td>
<td>2025-10-14</td>
</tr>
<tr>
<td>Win11 21H2</td>
<td>22000</td>
<td>Past</td>
<td>2024-10-08</td>
<td>N/A</td>
<td>N/A</td>
<td>2024-10-08</td>
</tr>
<tr>
<td>Win11 22H2</td>
<td>22621</td>
<td>2024-10-08</td>
<td>2025-10-14</td>
<td>N/A</td>
<td>N/A</td>
<td>2025-10-14</td>
</tr>
</table>

> [!NOTE]
> Windows App SDK does not support IoT SKUs.

## Windows Server SKUs and channels

Windows Server is the platform for building an infrastructure of connected applications, networks, and web services, from the workgroup to the data center. Windows Server offers services to other devices, has more features and capacities than a client OS, and can support more network connections.

The following table shows the end-date for each servicing category.

<table>
<tr>
<th rowspan="2">Version</th>
<th rowspan="2">Build</th>
<th colspan="2">Servicing end-dates</td>
<th rowspan="2">Max end-dates</th>
</tr>
<tr>
<th>LTSC Mainstream</th>
<th>LTSC Extended</th>
</tr>
<tr>
<td>Server 2019</td>
<td>17763</td>
<td>Past</td>
<td>2029-01-09</td>
<td>2029-01-09</td>
</tr>
<tr>
<td>Server 2022</td>
<td>20348</td>
<td>2026-10-13</td>
<td>2031-10-14</td>
<td>2032-01-13</td>
</tr>
</table>

## Related articles

- [Check for installed versions of the Windows App SDK runtime](check-windows-app-sdk-versions.md)
