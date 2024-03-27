---
title: Camera Barcode Scanner Advanced Configuration
description: Advanced configuration of the camera barcode scanner
ms.date: 04/08/2019
author: twarwick
ms.author: twarwick
ms.topic: article
keywords: windows 10, uwp, point of service, pos
ms.localizationpriority: medium
---

# Barcode Scanner Advanced Configuration

> [!CAUTION]
> These defaults are set after extensive testing to optimize a balance between decode performance and CPU utilization while not sacrificing battery life.  Cnahging these values can not only increase or decrease scanning performance, but also impact overall system performance by consuming more CPU cycles and decrease battery life.  Use caution and test the impact of each setting changed thoroughly.

The settings which control the behavior of the software barcode decoder are stored in the system registry under ```HKLM\Software\Microsoft\PointOfService\InboxDecoder```.  If you do not see any settings written to the registry in this location, it means they are following the default settings defined here.

## Enable / Disable

The software decoder can be disabled to prevent enumeration as a virtual barcode scanner using this registry setting.  Disabling the softare decoder will have no affect on the use of the camera for other purposes.  Be aware that applications may make assumptions that the decoder is always available, so be sure to test your scenarios that rely on camera access fully if you do disable the decoder.

| Value name   | Value Type | Value | Notes |
|:------------ |:----------:|:-------:|:------|
| Enable       | DWord      | 1</br>0 | Enabled</br>Disabled |

## MinimumQuietTimeMilliseconds

MinimumQuietTimeMilliseconds defines a timeframe where the camera barcode decoder must not see a barcode after a successful read.  This is to prevent an attempt to acquire a single barcode from returning multiple results for the same scanable item.  Moving the barcode out of view and back into view will successfully read the barcode again.  Lowering this value could result in the decoder to return multiple results for a single scanable item.

| Value name   | Value Type | Value | Notes |
|:------------ |:----------:|:-------:|:------|
| MinimumQuietTimeMilliseconds | DWord      | 1200 | Default&nbsp;ms |

## PreferredVideoFrameRate

PreferredVideoFrameRate default is set at 5 frames per second to balance performance with CPU utilization and battery consumption.  Increasing the number of frames per second can consume significantly more CPU cycles and battery consumption.  Reducing the value below 5 frames per second can impact scanning performance.

| Value name   | Value Type | Value | Notes |
|:------------ |:----------:|:-------:|:------|
| PreferredVideoFrameRate      | DWord      | 1 </br>30 </br>5 | Minimum&nbsp;fps</br>Maximum&nbsp;fps</br>Default&nbsp;fps|

## PreferredVideoFrameWidth

PreferredVideoFrameWidth by default is set to 1920 pixels. Increasing the pixel depth can require more CPU cycles and battery consumption to decode frames scanned.  Decreasing the pixel depth can reduce the CPU cycles, however it will also reduce the detection rate.

| Value name   | Value Type | Value | Notes |
|:------------ |:----------:|:-------:|:------|
| PreferredVideoFrameWidth     | DWord      | 800</br>2048</br>1920 | Minimum&nbsp;px</br>Maximum&nbsp;px</br>Default&nbsp;px|

## PreferredVideoFrameHeight

PreferredVideoFrameHeight default is set to 0, which is interpreted as the smallest height paired with the PreferredVideoFrameWidth.  Similar to PreferredVideoFrameWidth, setting the frame height to a large height can increase CPU utilization and battery consumption, with very little benefit.

| Value name   | Value Type | Value | Notes |
|:------------ |:----------:|:-------:|:------|
| PreferredVideoFrameHeight    | DWord      | 0 | Default: *Smallest height paired with width*|
