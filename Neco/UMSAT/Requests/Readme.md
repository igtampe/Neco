# UMSAT Requests
This folder holds requests to the backend for operations on Assets

|Request|Description|Included Data|
|-------|-----------|-------------|
|Asset Delete Request|Request to delete an asset|Session ID, Asset ID|
|Asset Image Upload Request|Request to upload or update an asset's image|Session ID, Asset ID, Image Byte Array|
|Asset Mod Request|Request to modify or create an asset|Session ID, Asset ID, Name, Description, Specific Location, Complete, PlotID
|Transfer Plot Ownership Request|Request to change a plot's owner|Session ID, Plot ID, New Owner ID|
