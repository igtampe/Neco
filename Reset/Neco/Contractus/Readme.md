# Neco Contractus Folder
This folder contains all relevant objects to handle Contractus items. Its used to delegate, bid for, and complete Contracts on the UMS

## Contract
Holds a Contractus Contract, which is a contract meant to be bid on for the lowest possible bid, then to be assigned, and completed.

### Fields
|Name|Description|
|-|-|
|ID|GUID of this contract|
|Name|Name of this contract|
|Description|Description of this contract|
|FromUser|User who posted this contract|
|TopBidder|Top Bidder of this contract, and the assignee after this contract has been assigned.|
|Amount|Amount bidded on this contract|
|Status|Status of this Contract, which follows the Contract Lifecycle|

![Lifecycle](https://raw.githubusercontent.com/igtampe/Neco/master/Images/Flowchart/Contract%20Lifecycle.png)


### Methods
(None)

