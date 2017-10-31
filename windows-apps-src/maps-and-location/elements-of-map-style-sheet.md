---
author: normesta
description: The entries and properties of a map style sheet
MSHAttr: PreferredLib:/library/windows/apps
Search.Product: eADQiWindows 10XVcnh
title: Map style sheet reference
ms.author: normesta
ms.date: 03/16/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, maps, map style sheet
localizationpriority: medium
---
# Map style sheet reference

You can create map style sheets by using JavaScript Object Notation (JSON).

For example, you'd use the following JSON to make water areas appear in red, water labels appear in green, and land areas appear in blue:

```json
    {"version":"1.*",
        "settings":{"landColor":"#0000FF"},
        "elements":{"water":{"fillColor":"#FF0000", "labelColor":"#00FF00"}}
    }
```
You could also use JSON to remove all labels and points from a map.

```json

    {"version":"1.*", "elements":{"mapElement":{"labelVisible":false},"point":{"visible":false}}}
```

Sometimes the value of a property is transformed to produce the final result.  For example, vegetation fillColor has slightly different shades depending on type of the entity being displayed.  This behavior can be turned off, thereby using the precise provided value, by using the ignoreTransform property.

```json
    {"version":"1.*",
        "settings":{"shadedReliefVisible":false},
        "elements":{"vegetation":{"fillColor":{"value":"#999999","ignoreTransform":true}}}
    }
```

This topic shows the JSON entries and [properties](#properties) that you can use to customize the look and feel of your maps.

<a id="entries" />
## Entries
This table uses ">" characters to represent levels in the entry hierarchy.   

| Name                         | Property Group            | Description    |
|------------------------------|---------------------------|----------------|
| version                      | [Version](#version)       | The style sheet version that you want to use. |
| settings                     | [Settings](#settings)     | The settings that apply to the whole style sheet. |
| mapElement                   | [MapElement](#mapelement) | The parent entry to all map entries. |
| > baseMapElement             | [MapElement](#mapelement) | The parent entry to all non-user entries. |
| >> area                      | [MapElement](#mapelement) | Areas of land use (not to be confused with the structure entry). |
| >>> airport                  | [MapElement](#mapelement) | Areas that encompass an airports. |
| >>> cemetery                 | [MapElement](#mapelement) | Areas of cemeteries. |
| >>> continent                | [MapElement](#mapelement) | Areas of entire continents. |
| >>> education                | [MapElement](#mapelement) |  |
| >>> indigenousPeoplesReserve | [MapElement](#mapelement) |  |
| >>> island                   | [MapElement](#mapelement) | Labels in island areas. |
| >>> medical                  | [MapElement](#mapelement) | Areas of land that are used for medical purposes (For example: a hospital campus). |
| >>> military                 | [MapElement](#mapelement) | Areas of military bases. |
| >>> nautical                 | [MapElement](#mapelement) | Areas of land that are used for nautical purposes. |
| >>> neighborhood             | [MapElement](#mapelement) | Labels in areas defined as neighborhoods. |
| >>> runway                   | [MapElement](#mapelement) | Land areas that are covered by a runway. |
| >>> sand                     | [MapElement](#mapelement) | Sandy areas like beaches. |
| >>> shoppingCenter           | [MapElement](#mapelement) | Areas of ground allocated for malls or other shopping centers. |
| >>> stadium                  | [MapElement](#mapelement) | Area of a stadium. |
| >>> vegetation               | [MapElement](#mapelement) | Forests, grassy areas, etc. |
| >>>> forest                  | [MapElement](#mapelement) | Areas of forest land. |
| >>>> golfCourse              | [MapElement](#mapelement) |  |
| >>>> park                    | [MapElement](#mapelement) | Park areas. |
| >>>> playingField            | [MapElement](#mapelement) | Extracted pitches such as a baseball field or tennis court. |
| >>>> reserve                 | [MapElement](#mapelement) | Areas of nature reserves. |
| >> point                     | [PointStyle](#pointstyle) | All point features that are rendered with an icon of some sort. |
| >>> naturalPoint             | [PointStyle](#pointstyle) |  |
| >>>> peak                    | [PointStyle](#pointstyle) | Icons that represent mountain peaks. |
| >>>>> volcanicPeak           | [PointStyle](#pointstyle) | Icons that represent volcano peaks. |
| >>>> waterPoint              | [PointStyle](#pointstyle) | Icons that represent water feature locations such as a waterfall. |
| >>> pointOfInterest          | [PointStyle](#pointstyle) | Restaurants, hospitals, schools, marinas, ski areas, etc. |
| >>>> business                | [PointStyle](#pointstyle) | Restaurants, hospitals, schools, etc. |
| >>>>> foodPoint              | [PointStyle](#pointstyle) | Restaurants, cafés, etc. |
| >>> populatedPlace           | [PointStyle](#pointstyle) | Icons that represent the size of populated place (For example: a city or town). |
| >>>> capital                 | [PointStyle](#pointstyle) | Icons that represent the capital of a populated place. |
| >>>>> adminDistrictCapital   | [PointStyle](#pointstyle) | Icons that represent the capital of a state or province. |
| >>>>> countryRegionCapital   | [PointStyle](#pointstyle) | Icons that represent the capital of a country or region. |
| >>> roadShield               | [PointStyle](#pointstyle) | Signs that represent the compact name for a road. (For example: I-5). Use only palette values if you set the **ImageFamily** property of the settings entry to a value of *Palette* |
| >>> roadExit                 | [PointStyle](#pointstyle) | Icons that represent exits, typically from a controlled access highway. |
| >>> transit                  | [PointStyle](#pointstyle) | Icons that represent bus stops, train stops, airports, etc. |
| >> political                 | [BorderedMapElement](#borderedmapelement) | Political regions such as countries, regions and states. |
| >>> countryRegion            | [BorderedMapElement](#borderedmapelement) |  |
| >>> adminDistrict            | [BorderedMapElement](#borderedmapelement) | Admin1, states, provinces, etc. |
| >>> district                 | [BorderedMapElement](#borderedmapelement) | Admin2, counties, etc. |
| >> structure                 | [MapElement](#mapelement) | Buildings and other building-like structures. |
| >>> building                 | [MapElement](#mapelement) |  |
| >>>> educationBuilding       | [MapElement](#mapelement) |  |
| >>>> medicalBuilding         | [MapElement](#mapelement) |  |
| >>>> transitBuilding         | [MapElement](#mapelement) |  |
| >> transportation            | [TwoToneLineStyle](#twotonelinestyle) | Lines that are part of the transportation network (For example: roads, trains, and ferries). |
| >>> road                     | [TwoToneLineStyle](#twotonelinestyle) | Lines that represent all roads. |
| >>>> controlledAccessHighway | [TwoToneLineStyle](#twotonelinestyle) |  |
| >>>>> highSpeedRamp          | [TwoToneLineStyle](#twotonelinestyle) | Lines that represent ramps. These ramps typically appear alongside of controlled access highways. |
| >>>> highway                 | [TwoToneLineStyle](#twotonelinestyle) |  |
| >>>> majorRoad               | [TwoToneLineStyle](#twotonelinestyle) |  |
| >>>> arterialRoad            | [TwoToneLineStyle](#twotonelinestyle) |  |
| >>>> street                  | [TwoToneLineStyle](#twotonelinestyle) |  |
| >>>>> ramp                   | [TwoToneLineStyle](#twotonelinestyle) | Lines that represent the entrance and exit of a highway. |
| >>>>> unpavedStreet          | [TwoToneLineStyle](#twotonelinestyle) |  |
| >>>> tollRoad                | [TwoToneLineStyle](#twotonelinestyle) | Roads that cost money to use. |
| >>> railway                  | [TwoToneLineStyle](#twotonelinestyle) | Railway lines. |
| >>> trail                    | [TwoToneLineStyle](#twotonelinestyle) | Walking trails through parks or hiking trails. |
| >>> waterRoute               | [TwoToneLineStyle](#twotonelinestyle) | Ferry route lines. |
| >> water                     | [MapElement](#mapelement) | Anything that looks like water. This includes oceans and streams. |
| >>> river                    | [MapElement](#mapelement) | Rivers, streams, or other water passages.  Note that this may be a line or polygon and might connect to non-river water bodies. |
| > routeMapElement            | [MapElement](#mapelement) | All routing entries are under this entry. |
| >> routeLine                 | [TwoToneLineStyle](#twotonelinestyle) | The styling for all route lines. |
| >>> walkingRoute             | [TwoToneLineStyle](#twotonelinestyle) |  |
| >>> drivingRoute             | [TwoToneLineStyle](#twotonelinestyle) |  |
| > userMapElement             | [MapElement](#mapelement) | All user entries are under this entry. |
| >> userPoint                 | [PointStyle](#pointstyle) | The styling for default user points. |
| >> userLine                  | [MapElement](#mapelement) | The styling for default user lines. |

<a id="properties" />
## Properties

This section describes the properties that you can use for each entry.

<a id="version" />
### Version properties

| Property                     | Type    | Description                                                                                                           |
|------------------------------|---------|-----------------------------------------------------------------------------------------------------------------------|
| version                      | String  | Targeted style sheet version. Used for applicability. "1.0" for default, "1.*" for additional minor features updates. |

<a id="settings" />
### Settings properties

| Property                     | Type    | Description                                                                                                                                                                                                                 |
|------------------------------|---------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| atmosphereVisible            | Bool    | A flag that indicates whether the atmosphere appears in the 3D control.                                                                                                                                                     |
| fogColor                     | Color   | The ARGB color value of the distance fog that appears in the 3D control.                                                                                                                                                    |
| glowColor                    | Color   | The ARGB color value that might be applied to label glow and icon glow.                                                                                                                                                     |
| imageFamily                  | String  | The name of image set to use for this style. Set this value to *Default* for signs that use fixed colors that are based on the real-world sign. Set this value to *Palette* for signs that use palette configurable colors. |
| landColor                    | Color   | The ARGB color value of the land before anything is drawn on that land.                                                                                                                                                     |
| logosVisible                 | Bool    | A flag that indicates whether items that have an **Organization** property should draw the appropriate Logos or use a generic icon.                                                                                         |
| officialColorVisible         | Bool    | A flag that indicates whether items that have an official color property (such as transit lines in China) should draw that color. For example, turn this value off for a black and white map.                               |
| rasterRegionsVisible         | Bool    | A flag that indicates whether or not to draw raster regions where we don't render by vectors (For example: Japan and Korea).                                                                                                |
| shadedReliefVisible          | Bool    | A flag that indicates whether or not to draw elevation shading on the map.                                                                                                                                                  |
| shadedReliefDarkColor        | Color   | The color of the dark-side of shaded relief.  Alpha channel represents the maximum alpha. value.                                                                                                                            |
| shadedReliefLightColor       | Color   | The color of the light-side of shaded relief.  Alpha channel represents the maximum alpha. value.                                                                                                                           |
| spaceColor                   | Color   | The ARGB color value for area around the map.                                                                                                                                                                               |
| useDefaultImageColors        | Bool    | A flag that indicates whetehr the original colors in the SVG should be used rather than looking up the palette entry for colors in an image.                                                                                |

<a id="mapelement" />
### MapElement properties

| Property                     | Type    | Description                                                                                                                 |
|------------------------------|---------|-----------------------------------------------------------------------------------------------------------------------------|
| fillColor                    | Color   | The color that is used for filling polygons, the background of point icons, and for the center of lines if they have split. |
| fontFamily                   | String  |                                                                                                                             |
| labelColor                   | Color   |                                                                                                                             |
| labelOutlineColor            | Color   |                                                                                                                             |
| labelScale                   | Float   | The amount by which default label sizes are scaled. For example, use *1* for default and *2* for twice as large.            |
| labelVisible                 | Bool    |                                                                                                                             |
| strokeColor                  | Color   | The color to use for the outline around polygons, the outline around point icons, and the color of lines.                   |
| strokeWidthScale             | Float   | The amount by which the stroke of lines are scaled. For example, use *1* for default and *2* for twice as large.            |
| visible                      | Bool    |                                                                                                                             |

<a id="borderedmap" />
### BorderedMapElement

This property group inherits from the [MapElement](#mapelement) property group.

| Property                     | Type    | Description                                                           |
|------------------------------|---------|-----------------------------------------------------------------------|
| borderOutlineColor           | Color   | The secondary or casing line color of the border of a filled polygon. |
| borderStrokeColor            | Color   | The primary line color of the border of a filled polygon.             |
| borderVisible                | Bool    |                                                                       |
| borderWidthScale             | Float   |                                                                       |

<a id="pointstyle" />
### PointStyle properties

This property group inherits from the [MapElement](#mapelement) property group.

| Property                     | Type    | Description                                                                                                        |
|------------------------------|---------|--------------------------------------------------------------------------------------------------------------------|
| iconColor                    | Color   | The color of the glyph shown in the middle of a point icon.                                                        |
| scale                        | Float   | The amount by which the whole point's size is scaled. For example, use *1* for default and *2* for twice as large. |
| stemColor                    | Color   | The color of the stem coming out of the bottom of the icon in 3D mode.                                             |
| stemOutlineColor             | Color   | The color of the outline around the stem coming out of the bottom of the icon in 3D mode.                          |

<a id="twotonelinestyle" />
### TwoToneLineStyle properties

This property group inherits from the [MapElement](#mapelement) property group.

| Property                     | Type    | Description                                                                                          |
|------------------------------|---------|------------------------------------------------------------------------------------------------------|
| overwriteColor               | Bool    |  Makes The alpha value of the **FillColor** overwrite the **StrokeColor** rather than blend with it. |
