# LandvView Folder
Landview should've really been its own package but its so integrated with Neco that It would've been a bit messy so it's best we leave it like this. Along with the following objects, we include the Graphics engine here and a few utils, as they have to be used in common backends. While it is regrettable that such a class is in the common, it is not so big as to cause an issue.

## LandView Item
Every landview item (Country, Road, District, Plot) is based on an abstract class with that name, and is as follows:

### Fields
|Name|Description|
|-|-|
|GraphicalPoints|Array of points that define the shape of this landview item (for Roads a line, for others a polygon)|

### Methods
|Method|Description|
|-|-|
|LeftmostX()|Minimum X coord of all graphical points|
|TopmostY()|Minimum Y Coord of all graphical points|
|Width()|Calcualted graphical width of this item|
|Height()|Calculated graphical height of this item|
|Area()|Calculated Area of this item|

## Country
A Country is the general defining canvas of all other landview items, and is defined as a width and height from a point at its center called the origin. This primarily is designed to plot Minecraft worlds as a Country.

### Fields
|Name|Description|
|-|-|
|ID|ID of this Country|
|Name|Name of this country|
|Districts|List of districts that are in this country|
|Roads|List of roads that are in this country|
|Width|Override for width(), as a Country's width is specified, not calculated|
|Height|Override for Height() for the same reason as Width()|
|PricePerSquareMeter|Although it was originally intended to allow users to buy un-districted land, this has been canceled, so this field is unused|
|FederalBankAccount|This field also is now unused as PPSM|

### Methods
(None)

## District
A district is a small subsection of a country were users can create and buy plots from. Districts can intersect, and can be contained by other districts. 

### Fields
|Name|Description|
|-|-|
|ID|ID of this District|
|Name|Name of this District|
|Points|String that holds a semicolon separated list of comma separated coordinates that can be parsed to build GraphicalPoints (IE: 0,0;1,1;2,2;)|
|Country|Country this district is in|
|Plots|Plots contained by this district|
|PricePerSquareMeter|Used to calculate price of a plot to purchase|
|FederalBankAccount|Account to which payments on plots are paid out to|

### Methods
(None)

## Plot
A plot is a small, user owned section of a district, that once created and purchased cannot be altered in geometry. Used to denote areas in which a user can build.

### Fields
|Name|Description|
|-|-|
|ID|ID of this Plot|
|Name|Name of this Plot|
|Points|String that holds a semicolon separated list of comma separated coordinates that can be parsed to build GraphicalPoints (IE: 0,0;1,1;2,2;)|
|District|District this plot is located in. The Plot should be *entirely* contained within this district, and should be the smallest district it is contained by (IE, if two districts contain this plot, this field should be the smallest one by area as it is likely a sub-district)
|Owner|User who owns this plot|
|Status|Indicates the status of this plot, which can be FOR_SALE, NOT_FOR_SALE (Default) and BUILT (which indicates the plot is currently built upon)|


### Methods
(None)

## Road
A road is a series of points that define lines that define a path for cars in a country to navigate. Unlike other landview items, a Road is not meant to be a polygon, and therefore should not be used with methods in the utils like Contains and Intersects.

### Fields
|Name|Description|
|-|-|
|ID|ID of this Road|
|Name|Name of this Road|
|Points|String that holds a semicolon separated list of comma separated coordinates that can be parsed to build GraphicalPoints (IE: 0,0;1,1;2,2;)|
|Country|Country this road is in|
|Thickness|Thickness of the road's graphical line. Meant to represent how wide the road is from edge to edge. We can't use width though because that is reserved for the graphical width of the entire item|

### Methods
(None)

## Utils
Utils includes common utilities to test landview items, including Point field validity, Calculating a plot's district, calculating if items intersect, or if they contain each other.

The following static utility methods are available:
|Method|Description|
|-|-|
|ValidatePoints()|Validates Point strings in Districts, Plots, and Roads|
|GetIntersectingPlot()|Determines a plot in the given plot's district that intersects with it. Returns null if the plot doesn't intersect with any|
|CalculatePlotDistrict()|Calculates what plot a district is in. Returns null if the plot is not fully contained in any plot (Either is outside of all plots or is in between plots), or is outside the plot's country|
|GetSmallestContainingDistrict()|Calculates smallest district containing a given LandviewItem (Used by Plot)|
|Contains()|Determines if one landview item contains another|
|FastContains()|Checks if a rectangle around one landview item contains another. Used to discard, not to assert.|
|DeepContains()|Checks if a landview item contains all points in another landview item.|
|Intersects()|Checks if a landview item L1 intersects with another landview item L2 by first checking if any points from L2 are in L1, Checking if a rectangle around L1 intersects with a rectangle around L2 (Used to discard, not to assert), and finally checking if any lines from L1 intersect with any lines in L2.|
|LandviewItemBoundingRectangle()|Generates a bounding rectangle around a landview item|

The following methods use code from chsarphelper.com

|Method|Description|
|-|-|
|Contains()| An override of the other Contains(), which checks if a landview item contains a point|
|GetAngle()|Gets angle between points ABC|
|DotProduct()|Gets dot product of AB with BC|
|CrossProductLength()|Returns the cross product of AB with BC|
|LinesIntersect()|Finds if two lines intersect|

## Landview Graphics Engine
The Graphics Engine handles the rendering and drawing of all landview items, either individually, or as a whole image based on a country. If the program is being executed in debug mode, a crosshair on the 0,0 origin point is also drawn.

The following static methods are available:
|Method|Description|
|-|-|
|GenerateDetailedCountryImage()|Generates an image for a country with all districts, plots, and roads|
|GenerateCountryImage()|Generates an image for a country with only districts and roads|
|GenerateDistrictImage()|Generates an image for a district, with all surrounding items|
|GeneratePlotImage()|Generates an image for a plot with all surrounding items|
|GenerateRoadImage()|Generates an image for a road with all surrounding items|
|DrawEverything()|Draws everything in a country onto a specified Graphics object|
|DrawDistrictOutline()|Draws only a district outline onto the specified graphics object|
|DrawDistrictLabel()|Draws only a district label onto the specified graphics object|
|DrawDistrict()|Draws a district outline and label onto the specified graphcis object|
|DrawDistricts()|Draws every district in a given country onto the specified graphics object|
|DrawPlotOutline()|Draws only a plot outline onto the specified graphics object|
|DrawPlotLabel()|Draws only a plot label onto the specified graphics object|
|DrawPlot()|Draws a plot outline and draws label if Height and width are over 50 onto the specified graphcis object|
|DrawPlots()|Draws every plot in a given district onto the specified graphics object|
|DrawRoad()|Draws a road onto the specified Graphics object|
|DrawRoads()|Draws every road in a given country onto the specified graphics object|
|GenerateCanvas()|Generates a blank image canvas with size of the specified landview item plus a configured margin|
|GetOrigin()|Gets location of the origin in a generated canvas of the specified landview item|
|DrawOriginCrosshair()|Draws a small crosshair on the origin (Used for debugging)|
|DrawLabel()|Draws label centralized on given point with given fonts, text, and colors|
|OffsetPoint()|Creates a new copy of a point offset by given X and Y coords (either in separate argument or as another point)|
|GenerateErrorImage()|Generates an error image to indicate that something has gone wrong|


