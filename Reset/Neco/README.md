# Neco Common Package
This package contains all the commmon Neco Objects, used by both Backends, frontends and anything in between. Included are some subfolders with other items that maybe should've been their own projects, but its ok. See them for more information on subitems

Here are the items at this level though. They mostly deal with Neco's primary function: Sending and storing money

## Bank
Holds a Neco Bank, which primarily holds bank accounts tied to it, and the types of bank accounts it offers.

### Fields
|Name|Description|
|-|-|
|ID|ID of this bank. Despite most IDs in Neco being GUIDs, this is a more easily memorable string, based on the abreviations from the ViBE Server (IE: UMS National Bank -> UMSNB). It must be 5 characters long|
|Name|Full name of this bank|
|Accounts|Collection of accounts this bank account holds. This collection is [JsonIgnore]d|
|AccountTypes|Collection of account types offered by this bank.|

### Methods
(None)

## Bank Account
Holds a Neco Bank Account, which primarily holds its owner, Its details, its type, and its bank

### Fields
|Name|Description|
|-|-|
|ID|ID of this bank account. Again, despite most Neco IDs being GUIDs, this is also a more memorable string. It must be 9 characters long (Preferably, though not requried to be numerical), and is meant to be split in groups of 3 (IE: A bank account at 444 333 222)|
|Bank|Bank this account belongs to|
|Type|Account type of this account|
|Owner|User this account belongs to. This field is [JsonIgnore]d|
|Closed|Indicates whether or not this bank account is closed. Bank accounts in Neco are not deleted, and are instead closed. This is necessary for transaction logging|

### Methods
(None)

## Bank Account Details
Holds confidential Neco Bank Account Details to make sure EF doesn't immediately include them. For now this is only the balance of the account

### Fields
|Name|Description|
|-|-|
|ID|GUID of this details object|
|Balance|Balance of this bank account|

### Methods
|Method|Description|
|-|-|
|IsOverdrafted()|Checks if this bank account is overdrafted|

## Bank Account Type
Holds a Neco Bank Account Type, designed to distinguish the different offerings from a bank, currently only including interest rates

### Fields
|Name|Description|
|-|-|
|ID|GUID of the Type|
|Name|Name of this bank account type|
|Bank|Bank this type belongs to|
|InterestRate|Interest rate of this bank account|

### Methods
(None)

## Certified Item
Holds a Neco Certified Item, or an item accessible in a public ledger. Usually used for Transactions or from Income Items

### Fields
|Name|Description|
|-|-|
|ID|GUID of this item|
|Text|Text of the certification|
|CertifiedBy|User who certified this item|
|Date|DateTime at which this item was certified|

### Methods
(None)

## Checkbook Item
Holds a pending transaction, either as a Bill or as a Check, so that another user may approve it.

### Fields
|Name|Description|
|-|-|
|ID|GUID of this item|
|Transaction|Attatched transaction|
|ItemType|Indicates whether this is a BILL or a CHECK|
|Variant|Graphical variant of this item (Carryover from ViBE which may not actually be implemented)|
|Comment|Comment tied to this check|

### Methods
(None)

## Notification
Notification to a user (usually used to indicate if a transaction has occurred)

### Fields
|Name|Description|
|-|-|
|ID|GUID of this object|
|Text|Text of this notification|
|Time|DateTime at which this notification was sent|
|User|User this Notification belongs to|
|Read|Indicates whether or not this notification has been read|

### Methods
(None)

## Transaction
Holds data on a transaction between two bank accounts

### Fields
|Name|Description|
|-|-|
|ID|GUID of this Transaction|
|Amount|Amount of this transaction|
|Time|DateTime this transaction was executed|
|Name|Name of this transaction|
|FromAccount|Account transaction's amount will be taken from|
|ToAccount|Account transaction's amount will be deposited to|
|State|Indicates whether this transaction is PENDING, COMPLETED, or FAILED|

### Methods
(None)

## User
Holds a Neco User, which holds a user's accounts, and user's notifications

### Fields
|Name|Description|
|-|-|
|ID|ID of this user, which is a string (not a GUID). Must be 5 characters in length, in keeping with the ViBE Server ID system (IE: 57174)|
|Name|Name of this user|
|Type|Type of this user|
|Accounts|Bank accounts of this user|
|Notifications|Notifications of this user|

### Methods
(None)

## User Auth
User object used for Authentication. Holds a User ID and Password

### Fields
|Name|Description|
|-|-|
|ID|ID with the same format and restrictions as a User|
|Pin|4 character long pin for this user|

### Methods
|Method|Description|
|-|-|
|CheckPin()|Checks a pin against the stored pin in this object|

## User Type
Type of a user, primarily used to distinguish Corporate, Government, and General accounts, along with their taxation status

### Fields
|Name|Description|
|-|-|
|ID|GUID of this type|
|Name|Name of this type|
|Taxation Type| Indicates whether this user is TAXABLE, A NON-TAXABLE DESTINATION (IE: Money sent to this account is tax deductible), or a NON-TAXABLE ORIGIN (IE: Money sent from this account is not counted towards income)|

### Methods
(None)
