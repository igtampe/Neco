# LandView Requests
This folder holds requests to the backend for operations on Plots

|Request|Description|Included Data|
|-------|-----------|-------------|
|Change Plot Status Request|Request to change a plot's status|Session ID, Plot ID, New Status|
|Create Plot Request|Request to check a plot, create and purchase a plot, or to create a preview image for this plot|Session ID, Country ID (Country this plot will belong to), Name, Defining Points, Bank Account ID (To charge for the purchase of this plot)|
|Transfer Plot Ownership Request|Request to change a plot's owner|Session ID, Plot ID, New Owner ID|
