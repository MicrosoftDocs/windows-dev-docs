---
ms.assetid: 9347AD7C-3A90-4073-BFF4-9E8237398343
description: This article lists the audio and video codec and format support for UWP apps.
title: Supported codecs
ms.date: 04/01/2021
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Supported codecs

This article lists the audio, video, and image codec and format availability for UWP apps by default for each device family. Note that these tables list the codecs that are included with the Windows 10 installation for the specified device family. Users and apps can install additional codecs that may be available to use. You can query at runtime for the set of codecs that are currently available for a specific device. For more information, see [Query for codecs installed on a device](codec-query.md).

In the tables below "D" indicates decoder support and "E" indicates encoder support.

## Audio codec & format support

The following tables show the audio codec and format support for each device family.

> [!NOTE] 
> Where AMR-NB support is indicated, this codec is not supported on Server SKUs.

 

### Desktop

<table>
<colgroup>
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
</colgroup>
<thead>
<tr class="header">
<th align="left">Codec/Container</th>
<th align="left">MPEG-4</th>
<th align="left">MPEG-3</th>
<th align="left">MPEG-2</th>
<th align="left">ADTS</th>
<th align="left">ASF</th>
<th align="left">RIFF</th>
<th align="left">AVI</th>
<th align="left">AC-3</th>
<th align="left">AMR</th>
<th align="left">3GP</th>
<th align="left">FLAC</th>
<th align="left">WAV</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td align="left">HE-AAC v1 / AAC+</td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
</tr>
<tr class="even">
<td align="left">HE-AAC v2 / eAAC+</td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
</tr>
<tr class="odd">
<td align="left">AAC-LC</td>
<td align="left">D/E</td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
</tr>
<tr class="even">
<td align="left">AC3</td>
<td align="left">D</td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
<td align="left">D</td>
<td align="left">D</td>
<td align="left">D</td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
</tr>

<tr class="even">
<td align="left">ALAC</td>
<td align="left">D/E</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
</tr>
<tr class="odd">
<td align="left">AMR-NB<sup>*</sup></td>
<td align="left">D/E</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D/E</td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
</tr>
<tr class="even">
<td align="left">FLAC</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D/E</td>
<td align="left"></td>
</tr>
<tr class="odd">
<td align="left">G.711 (A-Law, µ-law)</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
</tr>
<tr class="even">
<td align="left">GSM 6.10</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
</tr>
<tr class="odd">
<td align="left">IMA ADPCM</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
</tr>
<tr class="even">
<td align="left">LPCM</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D/E</td>
</tr>
<tr class="odd">
<td align="left">MP3</td>
<td align="left">D/E</td>
<td align="left">D/E</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D/E</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
</tr>
<tr class="even">
<td align="left">MPEG-1/2</td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
</tr>
<tr class="odd">
<td align="left">MS ADPCM</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
</tr>
<tr class="even">
<td align="left">WMA 1/2/3</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D/E</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
</tr>
<tr class="odd">
<td align="left">WMA Pro</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D/E</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
</tr>
<tr class="even">
<td align="left">WMA Voice</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D/E</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
</tr>
</tbody>
</table>

<sup>*</sup> Not available on Windows Server SKUs.

### Mobile

<table>
<colgroup>
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
</colgroup>
<thead>
<tr class="header">
<th align="left">Codec/Container</th>
<th align="left">MPEG-4</th>
<th align="left">MPEG-3</th>
<th align="left">MPEG-2</th>
<th align="left">ADTS</th>
<th align="left">ASF</th>
<th align="left">RIFF</th>
<th align="left">AVI</th>
<th align="left">AC-3</th>
<th align="left">AMR</th>
<th align="left">3GP</th>
<th align="left">FLAC</th>
<th align="left">WAV</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td align="left">HE-AAC v1 / AAC+</td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
</tr>
<tr class="even">
<td align="left">HE-AAC v2 / eAAC+</td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
</tr>
<tr class="odd">
<td align="left">AAC-LC</td>
<td align="left">D/E</td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D/E</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
</tr>
<tr class="even">
<td align="left">AC3</td>
<td align="left">D, Only on Lumia Icon, 830, 930, 1520</td>
<td align="left"></td>
<td align="left">D, Only on Lumia Icon, 830, 930, 1520</td>
<td align="left"></td>
<td align="left">D, Only on Lumia Icon, 830, 930, 1520</td>
<td align="left">D, Only on Lumia Icon, 830, 930, 1520</td>
<td align="left">D, Only on Lumia Icon, 830, 930, 1520</td>
<td align="left">D, Only on Lumia Icon, 830, 930, 1520</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
</tr>

<tr class="even">
<td align="left">ALAC</td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
</tr>
<tr class="odd">
<td align="left">AMR-NB</td>
<td align="left">D/E</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D/E</td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
</tr>
<tr class="even">
<td align="left">FLAC</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
</tr>
<tr class="odd">
<td align="left">G.711 (A-Law, µ-law)</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
</tr>
<tr class="even">
<td align="left">GSM 6.10</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
</tr>
<tr class="odd">
<td align="left">IMA ADPCM</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
</tr>
<tr class="even">
<td align="left">LPCM</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D/E</td>
</tr>
<tr class="odd">
<td align="left">MP3</td>
<td align="left">D</td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
</tr>
<tr class="even">
<td align="left">MPEG-1/2</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
</tr>
<tr class="odd">
<td align="left">MS ADPCM</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
</tr>
<tr class="even">
<td align="left">WMA 1/2/3</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
</tr>
<tr class="odd">
<td align="left">WMA Pro</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
</tr>
<tr class="even">
<td align="left">WMA Voice</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
</tr>
</tbody>
</table>

 

### WIndows 10 IoT

<table>
<colgroup>
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
</colgroup>
<thead>
<tr class="header">
<th align="left">Codec/Container</th>
<th align="left">MPEG-4</th>
<th align="left">MPEG-3</th>
<th align="left">MPEG-2</th>
<th align="left">ADTS</th>
<th align="left">ASF</th>
<th align="left">RIFF</th>
<th align="left">AVI</th>
<th align="left">AC-3</th>
<th align="left">AMR</th>
<th align="left">3GP</th>
<th align="left">FLAC</th>
<th align="left">WAV</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td align="left">HE-AAC v1 / AAC+</td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
</tr>
<tr class="even">
<td align="left">HE-AAC v2 / eAAC+</td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
</tr>
<tr class="odd">
<td align="left">AAC-LC</td>
<td align="left">D/E</td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
</tr>
<tr class="even">
<td align="left">AC3</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
</tr>

<tr class="even">
<td align="left">ALAC</td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
</tr>
<tr class="odd">
<td align="left">AMR-NB</td>
<td align="left">D/E</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D/E</td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
</tr>
<tr class="even">
<td align="left">FLAC</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
</tr>
<tr class="odd">
<td align="left">G.711 (A-Law, µ-law)</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
</tr>
<tr class="even">
<td align="left">GSM 6.10</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
</tr>
<tr class="odd">
<td align="left">IMA ADPCM</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
</tr>
<tr class="even">
<td align="left">LPCM</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D/E</td>
</tr>
<tr class="odd">
<td align="left">MP3</td>
<td align="left">D</td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
</tr>
<tr class="even">
<td align="left">MPEG-1/2</td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
</tr>
<tr class="odd">
<td align="left">MS ADPCM</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
</tr>
<tr class="even">
<td align="left">WMA 1/2/3</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D/E</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
</tr>
<tr class="odd">
<td align="left">WMA Pro</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D/E</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
</tr>
<tr class="even">
<td align="left">WMA Voice</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D/E</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
</tr>
</tbody>
</table>



### Xbox

<table>
<colgroup>
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
</colgroup>
<thead>
<tr class="header">
<th align="left">Codec/Container</th>
<th align="left">MPEG-4</th>
<th align="left">MPEG-3</th>
<th align="left">MPEG-2</th>
<th align="left">ADTS</th>
<th align="left">ASF</th>
<th align="left">RIFF</th>
<th align="left">AVI</th>
<th align="left">AC-3</th>
<th align="left">AMR</th>
<th align="left">3GP</th>
<th align="left">FLAC</th>
<th align="left">WAV</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td align="left">HE-AAC v1 / AAC+</td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
</tr>
<tr class="even">
<td align="left">HE-AAC v2 / eAAC+</td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
</tr>
<tr class="odd">
<td align="left">AAC-LC</td>
<td align="left">D/E</td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
</tr>
<tr class="even">
<td align="left">AC3</td>
<td align="left">D</td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
<td align="left">D</td>
<td align="left">D</td>
<td align="left">D</td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
</tr>

<tr class="even">
<td align="left">ALAC</td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
</tr>
<tr class="odd">
<td align="left">AMR-NB</td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D/E</td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
</tr>
<tr class="even">
<td align="left">FLAC</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
</tr>
<tr class="odd">
<td align="left">G.711 (A-Law, µ-law)</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
</tr>
<tr class="even">
<td align="left">GSM 6.10</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
</tr>
<tr class="odd">
<td align="left">IMA ADPCM</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
</tr>
<tr class="even">
<td align="left">LPCM</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D/E</td>
</tr>
<tr class="odd">
<td align="left">MP3</td>
<td align="left">D</td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
</tr>
<tr class="even">
<td align="left">MPEG-1/2</td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
</tr>
<tr class="odd">
<td align="left">MS ADPCM</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
</tr>
<tr class="even">
<td align="left">WMA 1/2/3</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
</tr>
<tr class="odd">
<td align="left">WMA Pro</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
</tr>
<tr class="even">
<td align="left">WMA Voice</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
</tr>
</tbody>
</table>

 

## Video codec & format support

The following tables show the video codec and format support for each device family.

> [!NOTE] 
> Where H.265 support is indicated, it is not necessarily supported by all devices within the device family.

 

### Desktop

<table>
<colgroup>
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
</colgroup>
<thead>
<tr class="header">
<th align="left">Codec/Container</th>
<th align="left">fMP4</th>
<th align="left">MPEG-4</th>
<th align="left">MPEG-2 PS</th>
<th align="left">MPEG-2 TS</th>
<th align="left">MPEG-1</th>
<th align="left">3GPP</th>
<th align="left">3GPP2</th>
<th align="left">AVCHD</th>
<th align="left">ASF</th>
<th align="left">AVI</th>
<th align="left">MKV</th>
<th align="left">DV</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td align="left">MPEG-1<sup>*</sup></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D/E</td>
<td align="left">D/E</td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D/E</td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
</tr>
<tr class="even">
<td align="left">MPEG-2<sup>*</sup></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D/E</td>
<td align="left">D/E</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D/E</td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
</tr>
<tr class="odd">
<td align="left">MPEG-4 (Part 2)</td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left">D</td>
<td align="left"></td>
</tr>
<tr class="even">
<td align="left">H.265<sup>**</sup></td>
<td align="left">D</td>
<td align="left">D/E</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
</tr>
<tr class="odd">
<td align="left">H.264</td>
<td align="left">D/E</td>
<td align="left">D/E</td>
<td align="left">D/E</td>
<td align="left">D/E</td>
<td align="left"></td>
<td align="left">D/E</td>
<td align="left"></td>
<td align="left">D/E</td>
<td align="left"></td>
<td align="left">D</td>
<td align="left">D</td>
<td align="left"></td>
</tr>
<tr class="even">
<td align="left">H.263</td>
<td align="left"></td>
<td align="left">D/E</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D/E</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
</tr>
<tr class="odd">
<td align="left">VC-1</td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left">D</td>
<td align="left">D</td>
<td align="left"></td>
</tr>
<tr class="even">
<td align="left">WMV7/8/9</td>
<td align="left"></td>
<td align="left">D/E</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D/E</td>
<td align="left">D/E</td>
<td align="left"></td>
<td align="left"></td>
</tr>
<tr class="odd">
<td align="left">WMV9 Screen</td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
</tr>
<tr class="even">
<td align="left">DV</td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
<td align="left">D</td>
</tr>

<tr class="even">
<td align="left">VP8</td>
<td align="left"></td>
<td align="left">D/E</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
</tr>

<tr class="even">
<td align="left">VP9</td>
<td align="left">D</td>
<td align="left">D/E</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
</tr>

<tr class="even">
<td align="left">AV1<sup>**</sup></td>
<td align="left">D</td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
</tr>

<tr class="odd">
<td align="left">Motion JPEG</td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
</tr>
</tbody>
</table>

<sup>*</sup> MPEG-2/MPEG-1 codec is available with install of optional Microsoft UWP DVD application.

<sup>**</sup> H.265 and AV1 are avaliable with the install of the corresponding optional codec pack.

### Mobile

<table>
<colgroup>
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
</colgroup>
<thead>
<tr class="header">
<th align="left">Codec/Container</th>
<th align="left">fMP4</th>
<th align="left">MPEG-4</th>
<th align="left">MPEG-2 PS</th>
<th align="left">MPEG-2 TS</th>
<th align="left">3GPP</th>
<th align="left">AVCHD</th>
<th align="left">ASF</th>
<th align="left">AVI</th>
<th align="left">MKV</th>
</tr>
</thead>
<tbody>

<tr class="odd">
<td align="left">MPEG-4 (Part 2)</td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left">D</td>
</tr>
<tr class="even">
<td align="left">H.265</td>
<td align="left">D</td>
<td align="left">D/E</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
</tr>
<tr class="odd">
<td align="left">H.264</td>
<td align="left">D/E</td>
<td align="left">D/E</td>
<td align="left">D/E</td>
<td align="left">D/E</td>
<td align="left">D/E</td>
<td align="left">D/E</td>
<td align="left"></td>
<td align="left">D</td>
<td align="left">D</td>
</tr>
<tr class="even">
<td align="left">H.263</td>
<td align="left"></td>
<td align="left">D/E</td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D/E</td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left">D</td>
</tr>
<tr class="odd">
<td align="left">VC-1</td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left">D</td>
</tr>

<tr class="odd">
<td align="left">Motion JPEG</td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
</tr>
</tbody>
</table>

 

### Windows 10 IoT

<table>
<colgroup>
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
</colgroup>
<thead>
<tr class="header">
<th align="left">Codec/Container</th>
<th align="left">fMP4</th>
<th align="left">MPEG-4</th>
<th align="left">MPEG-2 PS</th>
<th align="left">MPEG-2 TS</th>
<th align="left">MPEG-1</th>
<th align="left">3GPP</th>
<th align="left">3GPP2</th>
<th align="left">AVCHD</th>
<th align="left">ASF</th>
<th align="left">AVI</th>
<th align="left">MKV</th>
<th align="left">DV</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td align="left">MPEG-1</td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D/E</td>
<td align="left">D/E</td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D/E</td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
</tr>
<tr class="even">
<td align="left">MPEG-2<sup>*</sup></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D/E</td>
<td align="left">D/E</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D/E</td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
</tr>
<tr class="odd">
<td align="left">MPEG-4 (Part 2)</td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left">D</td>
<td align="left"></td>
</tr>
<tr class="even">
<td align="left">H.265<sup>**</sup></td>
<td align="left">D</td>
<td align="left">D/E</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
</tr>
<tr class="odd">
<td align="left">H.264</td>
<td align="left">D/E</td>
<td align="left">D/E</td>
<td align="left">D/E</td>
<td align="left">D/E</td>
<td align="left"></td>
<td align="left">D/E</td>
<td align="left"></td>
<td align="left">D/E</td>
<td align="left"></td>
<td align="left">D</td>
<td align="left">D</td>
<td align="left"></td>
</tr>
<tr class="even">
<td align="left">H.263</td>
<td align="left"></td>
<td align="left">D/E</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D/E</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
</tr>
<tr class="odd">
<td align="left">VC-1</td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left">D</td>
<td align="left">D</td>
<td align="left"></td>
</tr>
<tr class="even">
<td align="left">WMV7/8/9</td>
<td align="left"></td>
<td align="left">D/E</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D/E</td>
<td align="left">D/E</td>
<td align="left"></td>
<td align="left"></td>
</tr>
<tr class="odd">
<td align="left">WMV9 Screen</td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
</tr>
<tr class="odd">
<td align="left">VP8</td>
<td align="left"></td>
<td align="left">D/E</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
</tr>
<tr class="odd">
<td align="left">VP9</td>
<td align="left">D</td>
<td align="left">D/E</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
</tr>
<tr class="odd">
<td align="left">AV1<sup>**</sup></td>
<td align="left">D</td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
</tr>
<tr class="even">
<td align="left">DV</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
</tr>
<tr class="odd">
<td align="left">Motion JPEG</td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
</tr>
</tbody>
</table>

<sup>*</sup> MPEG-2/MPEG-1 codec is available with install of optional Microsoft UWP DVD application.

<sup>**</sup> H.265 and AV1 are avaliable with the install of the corresponding optional codec pack.
 



 

### Xbox

<table>
<colgroup>
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
<col width="7%" />
</colgroup>
<thead>
<tr class="header">
<th align="left">Codec/Container</th>
<th align="left">fMP4</th>
<th align="left">MPEG-4</th>
<th align="left">MPEG-2 PS</th>
<th align="left">MPEG-2 TS</th>
<th align="left">MPEG-1</th>
<th align="left">3GPP</th>
<th align="left">AVCHD</th>
<th align="left">ASF</th>
<th align="left">AVI</th>
<th align="left">MKV</th>
<th align="left">DV</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td align="left">MPEG-1</td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D/E</td>
<td align="left">D/E</td>
<td align="left">D</td>
<td align="left"></td>
<td align="left">D/E</td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
</tr>
<tr class="even">
<td align="left">MPEG-2</td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D/E</td>
<td align="left">D/E</td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D/E</td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
</tr>
<tr class="odd">
<td align="left">MPEG-4 (Part 2)</td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left">D</td>
<td align="left"></td>
</tr>
<tr class="even">
<td align="left">H.265</td>
<td align="left">D</td>
<td align="left">D/E</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
</tr>
<tr class="odd">
<td align="left">H.264</td>
<td align="left">D/E</td>
<td align="left">D/E</td>
<td align="left">D/E</td>
<td align="left">D/E</td>
<td align="left"></td>
<td align="left">D/E</td>
<td align="left">D/E</td>
<td align="left"></td>
<td align="left">D</td>
<td align="left">D</td>
<td align="left"></td>
</tr>
<tr class="even">
<td align="left">H.263</td>
<td align="left"></td>
<td align="left">D/E</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D/E</td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
</tr>
<tr class="odd">
<td align="left">VC-1</td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left">D</td>
<td align="left"></td>
</tr>
<tr class="even">
<td align="left">VP8</td>
<td align="left"></td>
<td align="left">D/E</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
</tr>
<tr class="even">
<td align="left">VP9</td>
<td align="left">D</td>
<td align="left">D/E</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
</tr>
<tr class="even">
<td align="left">AV1</td>
<td align="left">D</td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
</tr>
<tr class="even">
<td align="left">DV</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
</tr>
<tr class="odd">
<td align="left">Motion JPEG</td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left"></td>
<td align="left">D</td>
<td align="left"></td>
<td align="left"></td>
</tr>
</tbody>
</table>

## Image codec & format support 

<table>
<colgroup>
<col width="7%" />
<col width="7%" />
<col width="7%" />
</colgroup>
<thead>
<tr class="header">
<th align="left">Codec</th>
<th align="left">Desktop</th>
<th align="left">Other device families</th>
</tr>
</thead>
<tr class="odd">
<td align="left">BMP</td>
<td align="left">D/E</td>
<td align="left">D/E</td>
</tr>
<tr class="even">
<td align="left">DDS</td>
<td align="left">D/E<sup>1</sup></td>
<td align="left">D/E<sup>1</sup></td>
</tr>
<tr class="odd">
<td align="left">DNG</td>
<td align="left">D<sup>2</sup></td>
<td align="left">D<sup>2</sup></td>
</tr>
<tr class="even">
<td align="left">GIF</td>
<td align="left">D/E</td>
<td align="left">D/E</td>
</tr>
<tr class="odd">
<td align="left">ICO</td>
<td align="left">D</td>
<td align="left">D</td>
</tr>
<tr class="even">
<td align="left">JPEG</td>
<td align="left">D/E</td>
<td align="left">D/E</td>
</tr>
<tr class="odd">
<td align="left">JPEG-XR</td>
<td align="left">D/E</td>
<td align="left">D/E</td>
</tr>
<tr class="even">
<td align="left">PNG</td>
<td align="left">D/E</td>
<td align="left">D/E</td>
</tr>
<tr class="odd">
<td align="left">TIFF</td>
<td align="left">D/E</td>
<td align="left">D/E</td>
</tr>
<tr class="even">
<td align="left">Camera RAW</td>
<td align="left">D<sup>3</sup></td>
<td align="left">No</td>
</tr>
</table>

<sup>1</sup> DDS images using BC1 through BC5 compression are supported.  
<sup>2</sup> DNG images with a non-RAW embedded preview are supported.  
<sup>3</sup> Only certain camera RAW formats are supported.  

For more information on image codecs, see [Native WIC Codecs](/windows/desktop/wic/native-wic-codecs).


## Timed text format support

Windows has native support for the following formats for timed text. For more information on implementing timed text, see [Add external timed text with TimedTextSource](/windows/uwp/audio-video-camera/media-playback-with-mediasource#add-external-timed-text-with-timedtextsource), [TimedTextSource Class](/uwp/api/windows.media.core.timedtextsource), [IMFTimedText interface](/windows/win32/api/mfmediaengine/nn-mfmediaengine-imftimedtext).

* [TTML (Timed Text Markup Language)](https://en.wikipedia.org/wiki/Timed_Text_Markup_Language)
* [WebVTT (Web Video Text Tracks)](https://en.wikipedia.org/wiki/WebVTT)
* [SubRip](https://en.wikipedia.org/wiki/SubRip)
* [Substation Alpha](https://en.wikipedia.org/wiki/Subtitles#Subtitle_formats)
* [VobSub](https://en.wikipedia.org/wiki/DirectVobSub)
* [PGS (Presentation Graphics Stream)](https://en.wikipedia.org/wiki/Presentation_Graphic_Stream)