---
description: The entries and properties of a map style sheet
MSHAttr: PreferredLib:/library/windows/apps
Search.Product: eADQiWindows 10XVcnh
title: Map style sheet reference
ms.date: 03/19/2017
ms.topic: article
keywords: windows 10, uwp, maps, map style sheet
ms.localizationpriority: medium
---
# Map style sheet reference

Microsoft mapping technologies use _map style sheets_ to define the appearance of maps.  A map style sheet is defined using JavaScript Object Notation (JSON) and can be used in various ways including in a Windows Store application's [MapControl](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.maps.mapcontrol) through the [MapStyleSheet.ParseFromJson](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.maps.mapstylesheet.parsefromjson#Windows_UI_Xaml_Controls_Maps_MapStyleSheet_ParseFromJson_System_String_) method.

Style sheets can be created interactively using the [Map Style Sheet Editor](https://www.microsoft.com/p/map-style-sheet-editor/9nbhtcjt72ft) application.

The following JSON can be used to make water areas appear in red, water labels appear in green, and land areas appear in blue:

```json
    {"version":"1.*",
        "settings":{"landColor":"#0000FF"},
        "elements":{"water":{"fillColor":"#FF0000","labelColor":"#00FF00"}}
    }
```

This JSON can be used to remove all labels and points from a map.

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

This topic shows the JSON entries and [properties](#properties) that you can use to customize the look and feel of your maps.  These properties can also be applied to user map elements through the [MapStyleSheetEntry](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.maps.mapelement.mapstylesheetentry) property.

<a id="entries" />

## Entries
This table uses ">" characters to represent levels in the entry hierarchy.  It also shows which versions of Windows support each entry and which ignore it.

| Version | Windows Release Name |
|---------|----------------------|
|  1703   | Creators Update      |
|  1709   | Fall Creators Update |
|  1803   | April 2018 Update    |
|  1809   | October 2018 Update  |
|  1903   | May 2019 Update      |

| Name                               | Property Group            | 1703 | 1709 | 1803 | 1809 | 1903 | Description    |
|------------------------------------|---------------------------|------|------|------|------|------|----------------|
| version                            | [Version](#version)       |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | The style sheet version that you want to use. |
| settings                           | [Settings](#settings)     |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | The settings that apply to the whole style sheet. |
| mapElement                         | [MapElement](#mapelement) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | The parent entry to all map entries. |
| > baseMapElement                   | [MapElement](#mapelement) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | The parent entry to all non-user entries. |
| >> area                            | [MapElement](#mapelement) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Areas describing land use.  These should not to be confused with the physical buildings which are under the structure entry. |
| >>> airport                        | [MapElement](#mapelement) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Areas that encompass airports. |
| >>> areaOfInterest                 | [MapElement](#mapelement) |      |  ✔   |  ✔   |  ✔   |  ✔   | Areas in which there are a high concentration of businesses or interesting points. |
| >>> cemetery                       | [MapElement](#mapelement) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Areas that encompass cemeteries. |
| >>> continent                      | [MapElement](#mapelement) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Continent area labels. |
| >>> education                      | [MapElement](#mapelement) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Areas that encompass schools and other educational facilities. |
| >>> indigenousPeoplesReserve       | [MapElement](#mapelement) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Areas that encompass indigenous peoples reserves. |
| >>> industrial                     | [MapElement](#mapelement) |      |  ✔   |  ✔   |  ✔   |  ✔   | Areas that are used for industrial purposes. |
| >>> island                         | [MapElement](#mapelement) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Island area labels. |
| >>> medical                        | [MapElement](#mapelement) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Areas that are used for medical purposes (For example: a hospital campus). |
| >>> military                       | [MapElement](#mapelement) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Areas that encompass military bases or have military uses. |
| >>> nautical                       | [MapElement](#mapelement) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Areas that are used for nautical related purposes. |
| >>> neighborhood                   | [MapElement](#mapelement) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Neighborhood area labels. |
| >>> runway                         | [MapElement](#mapelement) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Areas that is used as an airplane runway. |
| >>> sand                           | [MapElement](#mapelement) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Sandy areas like beaches. |
| >>> shoppingCenter                 | [MapElement](#mapelement) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Areas of ground allocated for malls or other shopping centers. |
| >>> stadium                        | [MapElement](#mapelement) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Areas that encompass stadiums. |
| >>> underground                    | [MapElement](#mapelement) |      |  ✔   |  ✔   |  ✔   |  ✔   | Underground areas (For example: a metro station footprint). |
| >>> vegetation                     | [MapElement](#mapelement) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Forests, grassy areas, etc. |
| >>>> forest                        | [MapElement](#mapelement) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Areas of forest land. |
| >>>> golfCourse                    | [MapElement](#mapelement) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Areas that encompass golf courses. |
| >>>> park                          | [MapElement](#mapelement) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Areas that encompass parks. |
| >>>> playingField                  | [MapElement](#mapelement) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Extracted pitches such as a baseball field or tennis court. |
| >>>> reserve                       | [MapElement](#mapelement) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Areas that encompass nature reserves. |
| >> frozenWater                     | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Frozen water, like glacier. |
| >> point                           | [PointStyle](#pointstyle) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | All point features that are drawn with an icon of some sort. |
| >>> address                        | [PointStyle](#pointstyle) |      |      |  ✔   |  ✔   |  ✔   | Address numbers labels. |
| >>> naturalPoint                   | [PointStyle](#pointstyle) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Icons that represent natural features. |
| >>>> peak                          | [PointStyle](#pointstyle) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Icons that represent mountain peaks. |
| >>>>> volcanicPeak                 | [PointStyle](#pointstyle) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Icons that represent volcano peaks. |
| >>>> waterPoint                    | [PointStyle](#pointstyle) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Icons that represent water feature locations such as a waterfall. |
| >>> pointOfInterest                | [PointStyle](#pointstyle) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Icons that represent any interesting location. |
| >>>> business                      | [PointStyle](#pointstyle) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Icons that represent any business location. |
| >>>>> attractionPoint              | [PointStyle](#pointstyle) |      |  ✔   |  ✔   |  ✔   |  ✔   | Icons that represent tourist attractions such as museums, zoos, etc. |
| >>>>>> amusementPlacePoint         | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Amusement place icon. |
| >>>>>> aquariumPoint               | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Aquarium icon. |
| >>>>>> artGalleryPoint             | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Art gallery icon. |
| >>>>>> campPoint                   | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Camp icon. |
| >>>>>> fishingPoint                | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Fishing icon. |
| >>>>>> gardenPoint                 | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Garden icon. |
| >>>>>> hikingPoint                 | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Hiking icon. |
| >>>>>> libraryPoint                | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Library icon. |
| >>>>>> museumPoint                 | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Museum icon. |
| >>>>>> naturalPlacePoint           | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Natural place icon. |
| >>>>>> parkPoint                   | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Park icon. |
| >>>>>> restAreaPoint               | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Rest area icon. |
| >>>>>> touristAttractionPoint      | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Tourist attraction icon. |
| >>>>>> zooPoint                    | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Zoo icon. |
| >>>>> communityPoint               | [PointStyle](#pointstyle) |      |  ✔   |  ✔   |  ✔   |  ✔   | Icons that represent locations of general use to the community. |
| >>>>>> conventionCenterPoint       | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Convention center icon. |
| >>>>>> financialPoint              | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Financial icon. |
| >>>>>> governmentPoint             | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Government icon. |
| >>>>>> informationTechnologyPoint  | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Information technology icon. |
| >>>>>> palacePoint                 | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Palace icon. |
| >>>>>> pollingStationPoint         | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Polling station icon. |
| >>>>>> researchPoint               | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Research icon. |
| >>>>> educationPoint               | [PointStyle](#pointstyle) |      |  ✔   |  ✔   |  ✔   |  ✔   | Icons that represent schools and other education related locations. |
| >>>>> entertainmentPoint           | [PointStyle](#pointstyle) |      |  ✔   |  ✔   |  ✔   |  ✔   | Icons that represent entertainment venues such as theaters, cinemas, etc. |
| >>>>>> artsPoint                   | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Arts icon. |
| >>>>>> bowlingPoint                | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Bowling icon. |
| >>>>>> casinoPoint                 | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Casino icon. |
| >>>>>> golfCoursePoint             | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Golf course icon. |
| >>>>>> gymPoint                    | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Gym icon. |
| >>>>>> marinaPoint                 | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Marina icon. |
| >>>>>> movieTheaterPoint           | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Movie theater icon. |
| >>>>>> nightClubPoint              | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Night club icon. |
| >>>>>> recreationPoint             | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Recreation icon. |
| >>>>>> skatingPoint                | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Skating icon. |
| >>>>>> skiAreaPoint                | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Ski area icon. |
| >>>>>> stadiumPoint                | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Swimming pool icon. |
| >>>>>> swimmingPoolPoint           | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Swimming pool icon. |
| >>>>>> theaterPoint                | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Theater icon. |
| >>>>>> wineryPoint                 | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Winery icon. |
| >>>>> essentialServicePoint        | [PointStyle](#pointstyle) |      |  ✔   |  ✔   |  ✔   |  ✔   | Icons that represent essential services such as parking, banks, gas, etc. |
| >>>>>> aTMPoint                    | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | ATM icon. |
| >>>>>> automobileRentalPoint       | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Automobile rental icon. |
| >>>>>> automobileRepairPoint       | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Automobile repair icon. |
| >>>>>> bankPoint                   | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Bank icon. |
| >>>>>> clinicPoint                 | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Clinic icon. |
| >>>>>> electricChargingStationPoint| [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Electric charging station icon. |
| >>>>>> fireStationPoint            | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | FireStation icon. |
| >>>>>> gasStationPoint             | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | GasStation icon. |
| >>>>>> groceryPoint                | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Grocery icon. |
| >>>>>> hospitalPoint               | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Hospital icon. |
| >>>>>> laundryPoint                | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Laundry icon. |
| >>>>>> liquorAndWineStorePoint     | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Liquor and wine store icon. |
| >>>>>> mailPoint                   | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Mail icon. |
| >>>>>> marketPoint                 | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Market icon. |
| >>>>>> parkingPoint                | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Parking icon. |
| >>>>>> petsPoint                   | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Pets icon. |
| >>>>>> pharmacyPoint               | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Pharmacy icon. |
| >>>>>> policePoint                 | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Police icon. |
| >>>>>> postalServicePoint          | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Postal service icon. |
| >>>>>> professionalPoint           | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Professional service icon. |
| >>>>>> toiletPoint                 | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Toilet icon. |
| >>>>>> veterinarianPoint           | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Veterinarian icon. |
| >>>>> foodPoint                    | [PointStyle](#pointstyle) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Icons that represent restaurants, cafés, etc. |
| >>>>>> barAndGrillPoint            | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Bar and grill icon. |
| >>>>>> barPoint                    | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Bar icon. |
| >>>>>> breweryPoint                | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Brewery icon. |
| >>>>>> cafePoint                   | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Cafe icon. |
| >>>>>> iceCreamShopPoint           | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Ice cream shop icon. |
| >>>>>> restaurantPoint             | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Restaurant icon. |
| >>>>>> teaShopPoint                | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | TeaShop icon. |
| >>>>> lodgingPoint                 | [PointStyle](#pointstyle) |      |  ✔   |  ✔   |  ✔   |  ✔   | Icons that represent hotels and other lodging businesses. |
| >>>>>> gotelPoint                  | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Hotel icon. |
| >>>>> realEstatePoint              | [PointStyle](#pointstyle) |      |  ✔   |  ✔   |  ✔   |  ✔   | Icons that represent real estate businesses. |
| >>>>> shoppingPoint                | [PointStyle](#pointstyle) |      |  ✔   |  ✔   |  ✔   |  ✔   | Icons that represent hotels and other lodging businesses. |
| >>>>>> automobileDealerPoint       | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Automobile dealer icon. |
| >>>>>> beautyAndSpaPoint           | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Beauty and spa icon. |
| >>>>>> bookstorePoint              | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Bookstore icon. |
| >>>>>> departmentStorePoint        | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Department store icon. |
| >>>>>> electronicsShopPoint        | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Electronics shop icon. |
| >>>>>> golfShopPoint               | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Golf shop icon. |
| >>>>>> homeApplianceShopPoint      | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Home appliance shop icon. |
| >>>>>> mallPoint                   | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Mall icon. |
| >>>>>> phoneShopPoint              | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Phone shop icon. |
| >>> populatedPlace                 | [PointStyle](#pointstyle) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Icons that represent the size of populated place (For example: a city or town). |
| >>>> capital                       | [PointStyle](#pointstyle) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Icons that represent the capital of a populated place. |
| >>>>> adminDistrictCapital         | [PointStyle](#pointstyle) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Icons that represent the capital of a state or province. |
| >>>>>> majorAdminDistrictCapital   | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Icons that represent the major capital of a state or province. |
| >>>>>> minorAdminDistrictCapital   | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Icons that represent the minor capital of a state or province. |
| >>>>> countryRegionCapital         | [PointStyle](#pointstyle) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Icons that represent the capital of a country or region. |
| >>>> majorPopulatedPlace           | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Icons that represent the size of major populated place. |
| >>>> minorPopulatedPlace           | [PointStyle](#pointstyle) |      |      |      |      |  ✔   | Icons that represent the size of minor populated place. |
| >>> roadShield                     | [PointStyle](#pointstyle) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Signs that represent the compact name for a road. (For example: I-5). Use only palette values if you set the **ImageFamily** property of the settings entry to a value of *Palette* |
| >>> roadExit                       | [PointStyle](#pointstyle) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Icons that represent exits, typically from a controlled access highway. |
| >>> transit                        | [PointStyle](#pointstyle) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Icons that represent bus stops, train stops, airports, etc. |
| >> political                       | [BorderedMapElement](#borderedmapelement) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Political regions such as countries, regions and states. |
| >>> countryRegion                  | [BorderedMapElement](#borderedmapelement) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Country region borders and labels. |
| >>> adminDistrict                  | [BorderedMapElement](#borderedmapelement) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Admin1, states, provinces, etc., borders and labels. |
| >>> district                       | [BorderedMapElement](#borderedmapelement) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Admin2, counties, etc., borders and labels. |
| >> structure                       | [MapElement](#mapelement) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Buildings and other building-like structures. |
| >>> building                       | [MapElement](#mapelement) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Buildings. |
| >>>> educationBuilding             | [MapElement](#mapelement) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Buildings used for education. |
| >>>> medicalBuilding               | [MapElement](#mapelement) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Buildings used for medical purposes such as hospitals. |
| >>>> transitBuilding               | [MapElement](#mapelement) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Buildings used for transit such as airports. |
| >> transportation                  | [MapElement](#mapelement) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Lines that are part of the transportation network (For example: roads, trains, and ferries). |
| >>> road                           | [MapElement](#mapelement) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Lines that represent all roads. |
| >>>> controlledAccessHighway       | [MapElement](#mapelement) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Lines that represent large, controlled access highways. |
| >>>>> highSpeedRamp                | [MapElement](#mapelement) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Lines that represent high speed ramps that typically connect to controlled access highways. |
| >>>> highway                       | [MapElement](#mapelement) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Lines that represent highways. |
| >>>> majorRoad                     | [MapElement](#mapelement) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Lines that represent major roads. |
| >>>> arterialRoad                  | [MapElement](#mapelement) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Lines that represent arterial roads. |
| >>>> street                        | [MapElement](#mapelement) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Lines that represent streets. |
| >>>>> ramp                         | [MapElement](#mapelement) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Lines that represent ramps that typically connect to highways. |
| >>>>> unpavedStreet                | [MapElement](#mapelement) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Lines that represent unpaved streets. |
| >>>> tollRoad                      | [MapElement](#mapelement) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Lines that represent roads that cost money to use. |
| >>> railway                        | [MapElement](#mapelement) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Railway lines. |
| >>> trail                          | [MapElement](#mapelement) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Walking trails through parks or hiking trails. |
| >>> walkway                        | [MapElement](#mapelement) |      |  ✔   |  ✔   |  ✔   |  ✔   | Elevated walkway. |
| >>> waterRoute                     | [MapElement](#mapelement) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Ferry route lines. |
| >> water                           | [MapElement](#mapelement) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Anything that looks like water. This includes oceans and streams. |
| >>> river                          | [MapElement](#mapelement) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Rivers, streams, or other water passages.  Note that this may be a line or polygon and might connect to non-river water bodies. |
| > routeMapElement                  | [MapElement](#mapelement) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | All routing related entries. |
| >> routeLine                       | [MapElement](#mapelement) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Route line related entries. |
| >>> drivingRoute                   | [MapElement](#mapelement) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Lines that represent driving routes. |
| >>> scenicRoute                    | [MapElement](#mapelement) |      |  ✔   |  ✔   |  ✔   |  ✔   | Lines that represent scenic driving routes. |
| >>> walkingRoute                   | [MapElement](#mapelement) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Lines that represent walking routes. |
| > userMapElement                   | [MapElement](#mapelement) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | All user entries. |
| >> userBillboard                   | [MapElement](#mapelement) |      |  ✔   |  ✔   |  ✔   |  ✔   | The styling for default [MapBillboard](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.maps.mapbillboard) instances. |
| >> userLine                        | [MapElement](#mapelement) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | The styling for default [MapPolyline](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.maps.mappolyline) instances. |
| >> userModel3D                     | [MapElement3D](#mapelement3d) |      |  ✔   |  ✔   |  ✔   |  ✔   | The styling for default [MapModel3D](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.maps.mapmodel3d) instances.  This is primarily for setting renderAsSurface. |
| >> userPoint                       | [PointStyle](#pointstyle) |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | The styling for default [MapIcon](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.maps.mapicon) instances. |

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

| Property                     | Type    | 1703 | 1709 | 1803 | 1809 | 1903 | Description |
|------------------------------|---------|------|------|------|------|------|-------------|
| atmosphereVisible            | Bool    |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | A flag that indicates whether the atmosphere appears in the 3D control. |
| buildingTexturesVisible      | Bool    |      |      |  ✔   |  ✔   |  ✔   | A flag that indicates whether or not to show textures on symbolic 3D buildings that have textures. |
| fogColor                     | Color   |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | The ARGB color value of the distance fog that appears in the 3D control. |
| glowColor                    | Color   |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | The ARGB color value that might be applied to label glow and icon glow. |
| imageFamily                  | String  |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | The name of image set to use for this style. Set this value to *Default* for signs that use fixed colors that are based on the real-world sign. Set this value to *Palette* for signs that use palette configurable colors. |
| landColor                    | Color   |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | The ARGB color value of the land before anything is drawn on that land. |
| logosVisible                 | Bool    |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | A flag that indicates whether items that have an **Organization** property should draw the appropriate Logos or use a generic icon. |
| officialColorVisible         | Bool    |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | A flag that indicates whether items that have an official color property (such as transit lines in China) should draw that color. For example, turn this value off for a black and white map. |
| rasterRegionsVisible         | Bool    |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | A flag that indicates whether or not to draw raster regions where they have a better representation than vectors (Japan and Korea). |
| shadedReliefVisible          | Bool    |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | A flag that indicates whether or not to draw elevation shading on the map. |
| shadedReliefDarkColor        | Color   |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | The color of the dark-side of shaded relief.  Alpha channel represents the maximum alpha value. |
| shadedReliefLightColor       | Color   |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | The color of the light-side of shaded relief.  Alpha channel represents the maximum alpha value. |
| shadowColor                  | Color   |      |      |      |  ✔   |  ✔   | The color of the shadow behind icons that use shadows. |
| spaceColor                   | Color   |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | The ARGB color value for area around the map. |
| useDefaultImageColors        | Bool    |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | A flag that indicates whether the original colors in the SVG should be used rather than looking up the palette entry for colors in an image. |

<a id="mapelement" />

### MapElement properties

| Property                     | Type    | 1703 | 1709 | 1803 | 1809 | 1903 | Description |
|------------------------------|---------|------|------|------|------|------|-------------|
| backgroundScale              | Float   |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Amount by which the background element of an icon should be scaled.  For example, use *1* for default and *2* for twice as large. |
| fillColor                    | Color   |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | The color that is used for filling polygons, the background of point icons, and for the center of lines if they have split. |
| fontFamily                   | String  |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   |  |
| fontWeight                   | String  |      |      |      |      |  ✔   | The density of a typeface, in terms of the lightness or heaviness of the strokes. "**Light**", "**Normal**", "**SemiBold**" and "**Bold**" can be set |
| iconColor                    | Color   |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | The color of the glyph shown in the middle of a point icon. |
| iconScale                    | Float   |      |  ✔   |  ✔   |  ✔   |  ✔   | Amount by which the glyph of an icon should be scaled.  For example, use *1* for default and *2* for twice as large. |
| labelColor                   | Color   |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   |  |
| labelOutlineColor            | Color   |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   |  |
| labelScale                   | Float   |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | The amount by which default label sizes are scaled. For example, use *1* for default and *2* for twice as large. |
| labelVisible                 | Bool    |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   |  |
| overwriteColor               | Bool    |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Makes The alpha value of the **FillColor** overwrite the **StrokeColor** rather than blend with it. |
| scale                        | Float   |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | The amount by which the whole point's size is scaled. For example, use *1* for default and *2* for twice as large. |
| strokeColor                  | Color   |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | The color to use for the outline around polygons, the outline around point icons, and the color of lines. |
| strokeWidthScale             | Float   |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | The amount by which the stroke of lines are scaled. For example, use *1* for default and *2* for twice as large. |
| visible                      | Bool    |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   |  |

<a id="borderedmap" />

### BorderedMapElement

This property group inherits from the [MapElement](#mapelement) property group.

| Property                     | Type    | 1703 | 1709 | 1803 | 1809 | 1903 | Description |
|------------------------------|---------|------|------|------|------|------|-------------|
| borderOutlineColor           | Color   |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | The secondary or casing line color of the border of a filled polygon. |
| borderStrokeColor            | Color   |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | The primary line color of the border of a filled polygon. |
| borderVisible                | Bool    |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   |  |
| borderWidthScale             | Float   |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | The amount by which the stroke of borders are scaled. For example, use *1* for default and *2* for twice as large. |

<a id="pointstyle" />

### PointStyle properties

This property group inherits from the [MapElement](#mapelement) property group.

| Property                     | Type    | 1703 | 1709 | 1803 | 1809 | 1903 | Description |
|------------------------------|---------|------|------|------|------|------|-------------|
| shadowVisible                | Bool    |      |      |      |      |  ✔   | The flag that indicates whether the shadow of icon should be visible or not |
| shape-Background             | String  |      |      |      |      |  ✔   | Shape to use as the background of the icon--replacing any shape that exists there. |
| shape-Icon                   | String  |      |      |      |      |  ✔   | Shape to use as the foreground glyph of the icon--replacing any shape that exists there. |
| stemAnchorRadiusScale        | Float   |      |      |  ✔   |  ✔   |  ✔   | Amount by which the anchor point of an icon stem should be scaled.  For example, use *1* for default and *2* for twice as large. |
| stemColor                    | Color   |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | The color of the stem coming out of the bottom of the icon in 3D mode. |
| stemHeightScale              | Float   |      |      |  ✔   |  ✔   |  ✔   | Amount by which the length of the stem of an icon should be scaled.  For example, use *1* for default and *2* for twice as long. |
| stemOutlineColor             | Color   |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | The color of the outline around the stem coming out of the bottom of the icon in 3D mode. |
| stemWidthScale               | Float   |  ✔   |  ✔   |  ✔   |  ✔   |  ✔   | Amount by which the width of the stem of an icon should be scaled.  For example, use *1* for default and *2* for twice as long. |

<a id="mapelement3d" />

### MapElement3D

This property group inherits from the [MapElement](#mapelement) property group.

| Property                     | Type    | 1703 | 1709 | 1803 | 1809 | 1903 | Description |
|------------------------------|---------|------|------|------|------|------|------------|
| renderAsSurface              | Bool    |      |      |  ✔   |  ✔   |  ✔   | A flag that indicates that a 3D model should be rendered like a building--without depth fading against the ground. |
