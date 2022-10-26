---
title: Launch the Windows Maps app
description: Learn how to launch the Windows Maps app from your app using the bingmaps, ms-drive-to, ms-walk-to, and ms-settings URI schemes.
ms.assetid: E363490A-C886-4D92-9A64-52E3C24F1D98
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Launch the Windows Maps app




Learn how to launch the Windows Maps app from your app. This topic describes the **bingmaps:**, **ms-drive-to:**, **ms-walk-to:**, and **ms-settings:** Uniform Resource Identifier (URI) schemes. Use these URI schemes to launch the Windows Maps app to specific maps, directions, and search results or to download Windows Maps offline maps from the Settings app.

**Tip** To learn more about launching the Windows Maps app from your app, download the [Universal Windows Platform (UWP) map sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/MapControl) from the [Windows-universal-samples repo](https://github.com/Microsoft/Windows-universal-samples) on GitHub.

## Introducing URIs

URI schemes let you open apps by clicking hyperlinks (or programmatically, in your app). Just as you can start a new email using **mailto:** or open a web browser using **http:**, you can open the Windows maps app using **bingmaps:**, **ms-drive-to:**, and **ms-walk-to:**.

-   The **bingmaps:** URI provides maps for locations, search results, directions, and traffic.
-   The **ms-drive-to:** URI provides turn-by-turn driving directions from your current location.
-   The **ms-walk-to:** URI provides turn-by-turn walking directions from your current location.

For example, the following URI opens the Windows Maps app and displays a map centered over New York City.

```xml
<bingmaps:?cp=40.726966~-74.006076>
```

![a map centered over new york city.](images/mapnyc.png)

Here is a description of the URI scheme:

**bingmaps:?query**

In this URI scheme, *query* is a series of parameter name/value pairs:

**&param1=value1&param2=value2 …**

For a full list of the available parameters, see the [bingmaps:](#bingmaps-param-reference), [ms-drive-to:](#ms-drive-to-param-reference), and [ms-walk-to:](#ms-walk-to-param-reference) parameter reference. There are also examples later in this topic.

## Launch a URI from your app


To launch the Windows Maps app from your app, call the [**LaunchUriAsync**](/uwp/api/windows.system.launcher.launchuriasync) method with a **bingmaps:**, **ms-drive-to:**, or **ms-walk-to:** URI. The following example launches the same URI from the previous example. For more info about launching apps via URI, see [Launch the default app for a URI](launch-default-app.md).

```cs
// Center on New York City
var uriNewYork = new Uri(@"bingmaps:?cp=40.726966~-74.006076");

// Launch the Windows Maps app
var launcherOptions = new Windows.System.LauncherOptions();
launcherOptions.TargetApplicationPackageFamilyName = "Microsoft.WindowsMaps_8wekyb3d8bbwe";
var success = await Windows.System.Launcher.LaunchUriAsync(uriNewYork, launcherOptions);
```

In this example, the [**LauncherOptions**](/uwp/api/Windows.System.LauncherOptions) class is used to help ensure the Windows Maps app is launched.

## Display known locations

There are many options to control which part of the map to show. You can use the *cp* (center point) parameter with either the *rad* (radius) or the *lvl* (zoom level) parameters to show a location and choose how close to zoom in to it. When you use the *cp* parameter, you can also specify a *hdg* (heading) and *pit* (pitch) to control what direction to look. Another method is to use the *bb* (bounding box) parameter to provide the maximum south, east, north, and west coordinates of the area you want to show.

To control the type of view, use the *sty* (style) and *ss* (Streetside) parameters. The *sty* parameter lets you switch between road and aerial views. The *ss* parameter puts the map into a Streetside view. For more info about these and other parameters, see the [bingmaps: parameter reference](#bingmaps-param-reference).


| Sample URI                                                                 | Results                                                                                                                                                                                        |
|----------------------------------------------------------------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| bingmaps:?                                                                 | Opens the Maps app.                                                                                                                                                                            |
| bingmaps:?cp=40.726966~-74.006076                                          | Displays a map centered over New York City.                                                                                                                                                    |
| bingmaps:?cp=40.726966~-74.006076&lvl=10                                   | Displays a map centered over New York City with a zoom level of 10.                                                                                                                            |
| bingmaps:?bb=39.719\_-74.52~41.71\_-73.5                                   | Displays a map of New York City, which is the area specified in the **bb** argument.                                                                                                           |
| bingmaps:?bb=39.719\_-74.52~41.71\_-73.5&cp=47~-122                        | Displays a map of New York City, which is the area specified in the bounding box argument. The center point for Seattle specified in the **cp** argument is ignored because *bb* is specified. |
| bingmaps:?collection=point.36.116584\_-115.176753\_Caesars%20Palace&lvl=16 | Displays a map with a point named Caesars Palace (in Las Vegas) and sets the zoom level to 16.                                                                                                 |
| bingmaps:?collection=point.40.726966\_-74.006076\_Some%255FBusiness        | Displays a map with a point named Some\_Business (in Las Vegas).                                                                                                                               |
| bingmaps:?cp=40.726966~-74.006076&trfc=1&sty=a                             | Displays a map of New York City with traffic on and aerial map style.                                                                                                                          |
| bingmaps:?cp=47.6204~-122.3491&sty=3d                                      | Displays a 3D view of the Space Needle.                                                                                                                                                        |
| bingmaps:?cp=47.6204~-122.3491&sty=3d&rad=200&pit=75&hdg=165               | Displays a 3D view of the Space Needle with a radius of 200m, a pitch of 75 degrees, and a heading of 165 degrees.                                                                             |
| bingmaps:?cp=47.6204~-122.3491&ss=1                                        | Displays a Streetside view of the Space Needle.                                                                                                                                                |


## Display search results

When searching for places using the *q* parameter, we recommend making the terms as specific as possible and using the *cp*, *bb*, or *where* parameters to specify a search location. If you do not specify a search location and the user's current location isn't available, the search may not return meaningful results. Search results are displayed in the most appropriate map view. For more info about these and other parameters, see the [bingmaps: parameter reference](#bingmaps-param-reference).


| Sample URI                                                    | Results                                                                            |
|---------------------------------------------------------------|------------------------------------------------------------------------------------|
| bingmaps:?q=1600%20Pennsylvania%20Ave,%20Washington,%20DC     | Displays a map and searches for the address of the White House in Washington, D.C. |
| bingmaps:?q=coffee&where=Seattle                              | Searches for coffee in Seattle.                                                    |
| bingmaps:?cp=40.726966~-74.006076&where=New%20York            | Searches for New York near the specified center point.                             |
| bingmaps:?bb=39.719\_-74.52~41.71\_-73.5&q=pizza              | Searches for pizza in the specified bounding box (that is, in New York City).      |

 
## Display multiple points


Use the *collection* parameter to show a custom set of points on the map. If there is more than one point, a list of points is displayed. There can be up to 25 points in a collection and they are listed in the order provided. The collection takes precedence over search and directions requests. For more info about this parameter and others, see the [bingmaps: parameter reference](#bingmaps-param-reference).

| Sample URI | Results                                                                                                                   |
|--------------------------------------------------------------------------------------------------------------------------------------------------------------------|---------------------------------------------------------------------------------------------------------------------------|
| bingmaps:?collection=point.36.116584\_-115.176753\_Caesars%20Palace                                                                                                | Searches for Caesar's Palace in Las Vegas and displays the results on a map in the best map view.                         |
| bingmaps:?collection=point.36.116584\_-115.176753\_Caesars%20Palace&lvl=16                                                                                         | Displays a pushpin named Caesars Palace in Las Vegas and zooms to level 16.                                               |
| bingmaps:?collection=point.36.116584\_-115.176753\_Caesars%20Palace~point.36.113126\_-115.175188\_The%20Bellagio&lvl=16&cp=36.114902~-115.176669                   | Displays a pushpin named Caesars Palace and a pushpin named The Bellagio in Las Vegas and zooms to level 16.              |
| bingmaps:?collection=point.40.726966\_-74.006076\_Fake%255FBusiness%255Fwith%255FUnderscore                                                                        | Displays New York with a pushpin named Fake\_Business\_with\_Underscore.                                                  |
| bingmaps:?collection=name.Hotel%20List~point.36.116584\_-115.176753\_Caesars%20Palace~point.36.113126\_-115.175188\_The%20Bellagio&lvl=16&cp=36.114902~-115.176669 | Displays a list named Hotel List and two pushpins for Caesars Palace and The Bellagio in Las Vegas and zooms to level 16. |

 

## Display directions and traffic


You can display directions between two points using the *rtp* parameter; those points can be either addresses or latitude and longitude coordinates. Use the *trfc* parameter to show traffic information. To specify the type of directions: driving, walking, or transit, use the *mode* parameter. If *mode* isn't specified, directions will be provided using the user's preferred mode of transportation. For more info about these parameters and others, see the [bingmaps: parameter reference](#bingmaps-param-reference).

![an example of directions](images/windowsmapgcdirections.png)

| Sample URI                                                                                                              | Results                                                                                                                                                         |
|-------------------------------------------------------------------------------------------------------------------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------|
| bingmaps:?rtp=pos.44.9160\_-110.4158~pos.45.0475\_-109.4187                                                             | Displays a map with point-to-point directions. Because *mode* is not specified, directions will be provided using the user's mode of transportation preference. |
| bingmaps:?cp=43.0332~-87.9167&trfc=1                                                                                    | Displays a map centered over Milwaukee, WI with traffic.                                                                                                        |
| bingmaps:?rtp=adr.One Microsoft Way, Redmond, WA 98052~pos.39.0731\_-108.7238                                           | Displays a map with directions from the specified address to the specified location.                                                                            |
| bingmaps:?rtp=adr.1%20Microsoft%20Way,%20Redmond,%20WA,%2098052~pos.36.1223\_-111.9495\_Grand%20Canyon%20northern%20rim | Displays directions from 1 Microsoft Way, Redmond, WA, 98052 to the Grand Canyon's northern rim.                                                                |
| bingmaps:?rtp=adr.Davenport, CA~adr.Yosemite Village                                                                    | Displays a map with driving directions from the specified location to the specified landmark.                                                                   |
| bingmaps:?rtp=adr.Mountain%20View,%20CA~adr.San%20Francisco%20International%20Airport,%20CA&mode=d                      | Displays driving directions from Mountain View, CA to San Francisco International Airport, CA.                                                                  |
| bingmaps:?rtp=adr.Mountain%20View,%20CA~adr.San%20Francisco%20International%20Airport,%20CA&mode=w                      | Displays walking directions from Mountain View, CA to San Francisco International Airport, CA.                                                                  |
| bingmaps:?rtp=adr.Mountain%20View,%20CA~adr.San%20Francisco%20International%20Airport,%20CA&mode=t                      | Displays transit directions from Mountain View, CA to San Francisco International Airport, CA.                                                                  |

## Display turn-by-turn directions


The **ms-drive-to:** and **ms-walk-to:** URI schemes let you launch directly into a turn-by-turn view of a route. These URI schemes can only provide directions from the user's current location. If you must provide directions between points that do not include the user's current location, use the **bingmaps:** URI scheme as described in the previous section. For more info about these URI schemes, see the [ms-drive-to:](#ms-drive-to-param-reference) and [ms-walk-to:](#ms-walk-to-param-reference) parameter reference.

> **Important**  When the **ms-drive-to:** or **ms-walk-to:** URI schemes are launched, the Maps app will check to see if the device has ever had a GPS location fix. If it has, then the Maps app will proceed to turn-by-turn directions. If it hasn't, the app will display the route overview, as described in [Display directions and traffic](#display-directions-and-traffic).

![an example of turn-by-turn directions](images/windowsmapsappdirections.png)

| Sample URI                                                                                                | Results                                                                                       |
|-----------------------------------------------------------------------------------------------------------|-----------------------------------------------------------------------------------------------|
| ms-drive-to:?destination.latitude=47.680504&destination.longitude=-122.328262&destination.name=Green Lake | Displays a map with turn-by-turn driving directions to Green Lake from your current location. |
| ms-walk-to:?destination.latitude=47.680504&destination.longitude=-122.328262&destination.name=Green Lake  | Displays a map with turn-by-turn walking directions to Green Lake from your current location. |


## Download offline maps

The **ms-settings:** URI scheme lets you launch directly into a particular page in the Settings app. While the **ms-settings:** URI scheme doesn't launch into the Maps app, it does allow you to launch directly to the Offline Maps page in the Settings app and displays a confirmation dialog to download the offline maps used by the Maps app. The URI scheme accepts a point specified by a latitude and longitude and automatically determines if there are offline maps available for a region containing that point.  If the latitude and longitude passed happen to fall within multiple download regions, the confirmation dialog will let the user pick which of those regions to download. If offline maps are not available for a region containing that point, the offline Maps page in the Settings app is displayed with an error dialog.

| Sample URI  | Results |
|-------------|---------|
| ms-settings:maps-downloadmaps?latlong=47.6,-122.3 | Opens the Settings app to the Offline Maps page with a confirmation dialog displayed to download maps for the region containing the specified latitude-longitude point. |

<span id="bingmaps-param-reference"/>

## bingmaps: parameter reference

The syntax for each parameter in this table is shown by using Augmented Backus–Naur Form (ABNF).

<table>
<colgroup>
<col width="25%" />
<col width="25%" />
<col width="25%" />
<col width="25%" />
</colgroup>
<thead>
<tr class="header">
<th align="left">Parameter</th>
<th align="left">Definition</th>
<th align="left">ABNF Definition and Example</th>
<th align="left">Details</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td align="left"><p><b>cp</b></p></td>
<td align="left"><p>Center point</p></td>
<td align="left"><p>cp = "cp=" cpval</p>
<p>cpval = degreeslat "~" degreeslon</p>
<p>degreeslat = ["-"] 1*3DIGIT ["." 1*7DIGIT]</p>
<p>degreeslon = ["-"] 1*2DIGIT ["." 1*7DIGIT]</p>
<p>Example:</p>
<p>cp=40.726966~-74.006076</p></td>
<td align="left"><p>Both values must be expressed in decimal degrees and separated by a tilde(<b>~</b>).</p>
<p>Valid longitude values are between -180 and +180 inclusive.</p>
<p>Valid latitude values are between -90 and +90 inclusive.</p></td>
</tr>
<tr class="even">
<td align="left"><p><b>bb</b></p></td>
<td align="left"><p>Bounding box</p></td>
<td align="left"><p>bb = "bb=" southlatitude "_" westlongitude "~" northlatitude "_" eastlongitude</p>
<p>southlatitude = degreeslat</p>
<p>northlatitude = degreeslat</p>
<p>westlongitude = degreeslon</p>
<p>eastlongitude = degreeslon</p>
<p>degreeslat = ["-"] 13DIGIT ["." 17DIGIT]</p>
<p>degreeslon = ["-"] 12DIGIT ["." 17DIGIT]</p>
<p>Example:</p>
<p>bb=39.719_-74.52~41.71_-73.5</p></td>
<td align="left"><p>A rectangular area that specifies the bounding box expressed in decimal degrees, using a tilde (<b>~</b>) to separate the lower left corner from the upper right corner. Latitude and longitude for each are separated with an underscore (<b>_</b>).</p>
<p>Valid longitude values are between -180 and +180 inclusive.</p>
<p>Valid latitude values are between -90 and +90 inclusive.</p><p>The cp and lvl parameters are ignored when a bounding box is provided.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><b>where</b></p></td>
<td align="left"><p>Location</p></td>
<td align="left"><p>where = "where=" whereval</p>
<p>whereval = 1*( ALPHA / DIGIT / "-" / "." / "_" / pct-encoded / "!" / "$" / "'" / "(" / ")" / "*" / "+" / "," / ";" / ":" / "@" / "/" / "?")</p>
<p>Example:</p>
<p>where=1600%20Pennsylvania%20Ave,%20Washington,%20DC</p></td>
<td align="left"><p>Search term for a specific location, landmark or place.</p></td>
</tr>
<tr class="even">
<td align="left"><p><b>q</b></p></td>
<td align="left"><p>Query Term</p></td>
<td align="left"><p>q = "q="</p>
<p>whereval</p>
<p>Example:</p>
<p>q=mexican%20restaurants</p></td>
<td align="left"><p>Search term for local business or category of businesses.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><b>lvl</b></p></td>
<td align="left"><p>Zoom Level</p></td>
<td align="left"><p>lvl = "lvl=" 1<i>2DIGIT ["." 1</i>2DIGIT]</p>
<p>Example:</p>
<p>lvl=10.50</p></td>
<td align="left"><p>Defines the zoom level of the map view. Valid values are 1-20 where 1 is zoomed all the way out.</p></td>
</tr>
<tr class="even">
<td align="left"><p><b>sty</b></p></td>
<td align="left"><p>Style</p></td>
<td align="left"><p>sty = "sty=" ("a" / "r"/"3d")</p>
<p>Example:</p>
<p>sty=a</p></td>
<td align="left"><p>Defines the map style. Valid values for this parameter include:</p>
<ul>
<li><b>a</b>: Display an aerial view of the map.</li>
<li><b>r</b>: Display a road view of the map.</li>
<li><b>3d</b>: Display a 3D view of the map. Use in conjunction with the <b>cp</b> parameter and optionally with the <b>rad</b> parameter.</li>
</ul>
<p>In Windows 10, the aerial view and 3D view styles are the same.</p>
<div class="alert">
<b>Note</b>  Omitting the <b>sty</b> parameter produces the same results as sty=r.
</div>
<div>
 
</div></td>
</tr>
<tr class="odd">
<td align="left"><p><b>rad</b></p></td>
<td align="left"><p>Radius</p></td>
<td align="left"><p>rad = "rad=" 1*8DIGIT</p>
<p>Example:</p>
<p>rad=1000</p></td>
<td align="left"><p>A circular area that specifies the desired map view. The radius value is measured in meters.</p></td>
</tr>
<tr class="even">
<td align="left"><p><b>pit</b></p></td>
<td align="left"><p>Pitch</p></td>
<td align="left"><p>pit = "pit=" pitch</p>
<p>Example:</p>
<p>pit=60</p></td>
<td align="left"><p>Indicates the angle that the map is viewed at, where 90 is looking out at the horizon (maximum) and 0 is looking straight down (minimum).</p><p>Valid pitch values are between 0 and 90 inclusive.</td>
</tr>
<tr class="odd">
<td align="left"><p><b>hdg</b></p></td>
<td align="left"><p>Heading</p></td>
<td align="left"><p>hdg = "hdg=" heading</p>
<p>Example:</p>
<p>hdg=180</p></td>
<td align="left"><p>Indicates the direction the map is heading in degrees, where 0 or 360 = North, 90 = East, 180 = South, and 270 = West.</p></td>
</tr>
<tr class="even">
<td align="left"><p><b>ss</b></p></td>
<td align="left"><p>Streetside</p></td>
<td align="left"><p>ss = "ss=" BIT</p>
<p>Example:</p>
<p>ss=1</p></td>
<td align="left"><p>Indicates that street-level imagery is shown when <code>ss=1</code>. Omitting the <b>ss</b> parameter produces the same result as <code>ss=0</code>. Use in conjunction with the <b>cp</b> parameter to specify the location of the street-level view.</p>
<div class="alert">
<b>Note</b>  Street-level imagery is not available in all regions.
</div>
<div>
 
</div></td>
</tr>
<tr class="odd">
<td align="left"><p><b>trfc</b></p></td>
<td align="left"><p>Traffic</p></td>
<td align="left"><p>trfc = "trfc=" BIT</p>
<p>Example:</p>
<p>trfc=1</p></td>
<td align="left"><p>Specifies whether traffic information is included on the map. Omitting the trfc parameter produces the same results as <code>trfc=0</code>.</p>
<div class="alert">
<b>Note</b>  Traffic data is not available in all regions.
</div>
<div>
 
</div></td>
</tr>
<tr class="even">
<td align="left"><p><b>rtp</b></p></td>
<td align="left"><p>Route</p></td>
<td align="left"><p>rtp = "rtp=" (waypoint "~" [waypoint]) / ("~" waypoint)</p>
<p>waypoint = ("pos." point ) / ("adr." whereval)</p>
<p>point = "point." pointval ["_" title]</p>
<p>pointval = degreeslat "" degreeslon</p>
<p>degreeslat = ["-"] 13DIGIT ["." 17DIGIT]</p>
<p>degreeslon = ["-"] 12DIGIT ["." 17DIGIT]</p>
<p>title = whereval</p>
<p>whereval = 1( ALPHA / DIGIT / "-" / "." / "_" / pct-encoded / "!" / "$" / "'" / "(" / ")" / "" / "+" / "," / ";" / ":" / "@" / "/" / "?")</p>


<p>Examples:</p>
<p>rtp=adr.Mountain%20View,%20CA~adr.SFO</p>
<p>rtp=adr.One%20Microsoft%20Way,%20Redmond,%20WA~pos.45.23423_-122.1232_My%20Picnic%20Spot</p></td>
<td align="left"><p>Defines the start and end of a route to draw on the map, separated by a tilde (<b>~</b>). Each of the waypoints is defined by either a position using ltitude, longitude, and optional title or an address identifier.</p>
<p>A complete route contains exactly two waypoints. For example, a route with two waypoints is defined by <code>rtp="A"~"B"</code>.</p>
<p>It's also acceptable to specify an incomplete route. For example, you can define only the start of a route with <code>rtp="A"~</code>. In this case, the directions input is displayed with the provided waypoint in the <b>From</b> field and the <b>To</b> field has focus.</p>
<p>If only the end of a route is specified, as with <code>rtp=~"B"</code>, the directions panel is displayed with the provided waypoint in the <b>To</b> field. If an accurate current location is available, the current location is pre-populated in the <b>From</b> field with focus.</p>
<p>No route line is drawn when an incomplete route is given.</p>
<p>Use in conjunction with the <b>mode</b> parameter to specify the mode of transportation (driving, transit, or walking). If <b>mode</b> isn't specified, directions will be provided using the user's mode of transportation preference.</p>
<div class="alert">
<b>Note</b>  A title can be used for a location if the location is specified by the <b>pos</b> parameter value. Rather than showing the latitude and longitude, the title will be displayed.
</div>
<div>
 
</div></td>
</tr>
<tr class="odd">
<td align="left"><p><b>mode</b></p></td>
<td align="left"><p>Transportation mode</p></td>
<td align="left"><p>mode = "mode=" ("d" / "t" / "w")</p>
<p>Example:</p>
<p>mode=d</p></td>
<td align="left"><p>Defines the transportation mode. Valid values for this parameter include:</p>
<ul>
<li><b>d</b>: Displays route overview for driving directions</li>
<li><b>t</b>: Displays route overview for transit directions</li>
<li><b>w</b>: Displays route overview for walking directions</li>
</ul>
<p>Use in conjunction with the <b>rtp</b> parameter for transportation directions. If <b>mode</b> isn't specified, directions will be provided using the user's mode of transportation preference. A <b>mode</b> can be provided with no route parameter to enter directions input for that mode from the current location.</p></td>
</tr>

<tr class="even">
<td align="left"><p><b>collection</b></p></td>
<td align="left"><p>Collection</p></td>
<td align="left"><p>collection = "collection="(name"~"/)point["~"point]</p>
<p>name = "name." whereval </p>
<p>whereval = 1( ALPHA / DIGIT / "-" / "." / "_" / pct-encoded / "!" / "$" / "'" / "(" / ")" / "" / "+" / "," / ";" / ":" / "@" / "/" / "?") </p>
<p>point = "point." pointval ["_" title] </p>
<p>pointval = degreeslat "" degreeslon </p>
<p>degreeslat = ["-"] 13DIGIT ["." 17DIGIT] </p>
<p>degreeslon = ["-"] 12DIGIT ["." 17DIGIT] </p>
<p>title = whereval</p>


<p>Example:</p>
<p>collection=name.My%20Trip%20Stops~point.36.116584_-115.176753_Las%20Vegas~point.37.8268_-122.4798_Golden%20Gate%20Bridge</p></td>
<td align="left"><p>Collection of points to be added to the map and list. The collection of points can be named using the name parameter. A point is specified using a latitude, longitude, and optional title.</p>
<p>Separate name and multiple points with tildes (<b>~</b>).</p>
<p>If the item you specify contains a tilde, make sure the tilde is encoded as <code>%7E</code>. If not accompanied by Center point and Zoom Level parameters, the collection will provide the best map view.</p>

<p><b>Important</b> If the item you specify contains an underscore, make sure the underscore is double encoded as %255F.</p></td>
</tr>
</tbody>
</table>

  
<span id="ms-drive-to-param-reference"/>

## ms-drive-to: parameter reference


The URI to launch a request for turn-by-turn driving directions does not need to be encoded and has the following format.

> **Note**  You don’t specify the starting point in this URI scheme. The starting point is always assumed to be the current location. If you need to specify a starting point other than the current location, see [Display directions and traffic](#display-directions-and-traffic).

 

| Parameter | Definition | Example | Details |
|------------|-----------|---------|---------|
| **destination.latitude** | Destination latitude | Example: destination.latitude=47.6451413797194 | The latitude of the destination. Valid latitude values are between -90 and +90 inclusive. |
| **destination.longitude** | Destination longitude | Example: destination.longitude=-122.141964733601 | The longitude of the destination. Valid longitude values are between -180 and +180 inclusive. |
| **destination.name** | Name of the destination | Example: destination.name=Redmond, WA | The name of the destination. You do not have to encode the **destination.name** value. |

 
<span id="ms-walk-to-param-reference"/>

## ms-walk-to: parameter reference


The URI to launch a request for turn-by-turn walking directions does not need to be encoded and has the following format.

> **Note**  You don’t specify the starting point in this URI scheme. The starting point is always assumed to be the current location. If you need to specify a starting point other than the current location, see [Display directions and traffic](#display-directions-and-traffic).
 

| Parameter | Definition | Example | Details |
|-----------|------------|---------|----------|
| **destination.latitude** | Destination latitude | Example: destination.latitude=47.6451413797194 | The latitude of the destination. Valid latitude values are between -90 and +90 inclusive. |
| **destination.longitude** | Destination longitude | Example: destination.longitude=-122.141964733601 | The longitude of the destination. Valid longitude values are between -180 and +180 inclusive. |
| **destination.name** | Name of the destination | Example: destination.name=Redmond, WA | The name of the destination. You do not have to encode the **destination.name** value. |

## ms-settings: parameter reference

The syntax for maps app specific parameters for the **ms-settings:** URI scheme is defined below. **maps-downloadmaps** is specified along with the **ms-settings:** URI in the form of **ms-settings:maps-downloadmaps?** to indicate the offline maps settings page. 

| Parameter | Definition | Example | Details |
|-----------|------------|---------|----------|
| **latlong** | Point defining offline map region. | Example: latlong=47.6,-122.3 | The geopoint is specified by a comma separated latitude and longitude. Valid latitude values are between -90 and +90 inclusive. Valid longitude values are between -180 and +180 inclusive. |