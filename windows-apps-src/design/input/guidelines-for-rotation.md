---
Description: This topic describes the new Windows UI for rotation and provides user experience guidelines that should be considered when using this new interaction mechanism in your Windows app.
title: Rotation
ms.assetid: f098bc05-35b3-46b2-9e9b-9ff292d067ca
label: Rotation
template: detail.hbs
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Rotation


This article describes the new Windows UI for rotation and provides user experience guidelines that should be considered when using this new interaction mechanism in your Windows app.

> **Important APIs**: [**Windows.UI.Input**](/uwp/api/Windows.UI.Input), [**Windows.UI.Xaml.Input**](/uwp/api/Windows.UI.Xaml.Input)

## Dos and don'ts

-   Use rotation to help users directly rotate UI elements.

## Additional usage guidance


**Overview of rotation**

Rotation is the touch-optimized technique used by Windows apps to enable users to turn an object in a circular direction (clockwise or counter-clockwise).

Depending on the input device, the rotation interaction is performed using:

-   A mouse or active pen/stylus to move the rotation gripper of a selected object.
-   Touch or passive pen/stylus to turn the object in the desired direction using the rotate gesture.

**When to use rotation**

Use rotation to help users directly rotate UI elements. The following diagrams show some of the supported finger positions for the rotation interaction.

![diagram demonstrating various finger postures supported by rotation.](images/ux-rotate-positions.png)

**Note**  
Intuitively, and in most cases, the rotation point is one of the two touch points unless the user can specify a rotation point unrelated to the contact points (for example, in a drawing or layout application). The following images demonstrate how the user experience can be degraded if the rotation point is not constrained in this way.

This first picture shows the initial (thumb) and secondary (index finger) touch points: the index finger is touching a tree and the thumb is touching a log.

![image showing the two initial touch points for the rotation gesture.](images/ux-rotate-points1.png)
In this second picture, rotation is performed around the initial (thumb) touch point. After the rotation, the index finger is still touching the tree trunk and the thumb is still touching the log (the rotation point).

![image showing a rotated picture with the rotation point constrained to one of the two initial touch points.](images/ux-rotate-points2.png)
In this third picture, the center of rotation has been defined by the application (or set by the user) to be the center point of the picture. After the rotation, because the picture did not rotate around one of the fingers, the illusion of direct manipulation is broken (unless the user has chosen this setting).

![image showing a rotated picture with the rotation point constrained to the center of the picture rather than either of the two initial touch points.](images/ux-rotate-points3.png)
In this last picture, the center of rotation has been defined by the application (or set by the user) to be a point in the middle of the left edge of the picture. Again, unless the user has chosen this setting, the illusion of direct manipulation is broken in this case.

![image showing a rotated picture with the rotation point constrained to the leftmost center of the picture rather than either of the two initial touch points.](images/ux-rotate-points4.png)

 

Windows 10 supports three types of rotation: free, constrained, and combined.

<table>
<colgroup>
<col width="50%" />
<col width="50%" />
</colgroup>
<thead>
<tr class="header">
<th align="left">Type</th>
<th align="left">Description</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td align="left">Free rotation</td>
<td align="left"><p>Free rotation enables a user to rotate content freely anywhere in a 360 degree arc. When the user releases the object, the object remains in the chosen position. Free rotation is useful for drawing and layout applications such as Microsoft PowerPoint, Word, Visio, and Paint; and Adobe Photoshop, Illustrator, and Flash.</p></td>
</tr>
<tr class="even">
<td align="left">Constrained rotation</td>
<td align="left"><p>Constrained rotation supports free rotation during the manipulation but enforces snap points at 90 degree increments (0, 90, 180, and 270) upon release. When the user releases the object, the object automatically rotates to the nearest snap point.</p>
<p>Constrained rotation is the most common method of rotation, and it functions in a similar way to scrolling content. Snap points let a user be imprecise and still achieve their goal. Constrained rotation is useful for applications such as web browsers and photo albums.</p></td>
</tr>
<tr class="odd">
<td align="left">Combined rotation</td>
<td align="left"><p>Combined rotation supports free rotation with zones (similar to rails in <a href="guidelines-for-panning.md">Guidelines for panning</a>) at each of the 90 degree snap points enforced by constrained rotation. If the user releases the object outside of one of 90 degree zones, the object remains in that position; otherwise, the object automatically rotates to a snap point.</p>
<div class="alert">
<strong>Note</strong>  A user interface rail is a feature in which an area around a target constrains movement towards some specific value or location to influence its selection.
</div>
<div>
 
</div></td>
</tr>
</tbody>
</table>

## Related topics

### Samples

- [Basic input sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/BasicInput)
- [Low latency input sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/LowLatencyInput)
- [User interaction mode sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/UserInteractionMode)
- [Focus visuals sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/XamlFocusVisuals)

### Archive samples

- [Input: XAML user input events sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/411c271e537727d737a53fa2cbe99eaecac00cc0/Official%20Windows%20Platform%20Sample/Input%20XAML%20user%20input%20events%20sample)
- [Input: Device capabilities sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/411c271e537727d737a53fa2cbe99eaecac00cc0/Official%20Windows%20Platform%20Sample/Windows%208%20app%20samples/%5BC%23%5D-Windows%208%20app%20samples/C%23/Windows%208%20app%20samples/Input%20Device%20capabilities%20sample%20(Windows%208))
- [Input: Touch hit testing sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/411c271e537727d737a53fa2cbe99eaecac00cc0/Official%20Windows%20Platform%20Sample/Windows%208%20desktop%20samples/%5BC%2B%2B%5D-Windows%208%20desktop%20samples/C%2B%2B/Windows%208%20desktop%20samples/Input%20Touch%20hit%20testing%20sample)
- [XAML scrolling, panning, and zooming sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/411c271e537727d737a53fa2cbe99eaecac00cc0/Official%20Windows%20Platform%20Sample/Universal%20Windows%20app%20samples/111487-Universal%20Windows%20app%20samples/XAML%20scrolling%2C%20panning%2C%20and%20zooming%20sample)
- [Input: Simplified ink sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/411c271e537727d737a53fa2cbe99eaecac00cc0/Official%20Windows%20Platform%20Sample/Input%20Simplified%20ink%20sample)
- [Input: Gestures and manipulations with GestureRecognizer](/samples/browse/?redirectedfrom=MSDN-samples)
- [Input: Manipulations and gestures sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/411c271e537727d737a53fa2cbe99eaecac00cc0/Official%20Windows%20Platform%20Sample/Input%20Gestures%20and%20manipulations%20with%20GestureRecognizer)
- [DirectX touch input sample](https://github.com/microsoftarchive/msdn-code-gallery-microsoft/tree/411c271e537727d737a53fa2cbe99eaecac00cc0/Official%20Windows%20Platform%20Sample/Windows%208%20app%20samples/%5BC%2B%2B%5D-Windows%208%20app%20samples/C%2B%2B/Windows%208%20app%20samples/DirectX%20touch%20input%20sample%20(Windows%208))