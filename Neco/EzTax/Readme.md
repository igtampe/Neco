# Neco Eztax Folder

This folder contains relevant objects for Static Income items based on misc income and subitems, and the calculation of taxes based on income and jurisdictions in which that income was obtained

## Income Item
Holds a static income item (Typically a business, corporation, or property). Its data is stored primarily in subitems, but this object puts it all together.

### Fields
|Name|Description|
|-|-|
|ID|GUID of this item|
|Name|Name of this item|
|User|User this item belongs to|
|Subitems|A collection of IncomeSubitems, which is an abstract class. This collection is used for general operations in the backend, and is [JSONIgnore]d, because the Json serializer uses the type of the collection, not the type of the object, to serialize. For the objects, there are separate lists detailed below which ARE Json serialized.|
|Apartments|Collection of apartment subitems|
|Businesses|Collection of Business subitems|
|Hotels|Collection of Hotel subitems|
|Misc Income| Miscellaneous income attributed to this income item. This field is typically used for corporate incomes|
|Local Jurisdiction| Local jurisdiction of this item|
|Federal Jurisdiction| Federal jurisdiction of this item|

### Methods
|Method|Description|
|-|-|
|TotalMonthlyIncome()|Calculates the total monthly income of this item, by adding the incomes of the subitems|

## Tax Bracket  
Holds a tax bracket for a speicifc Jursidiction. Primarily specifies a range of income, and how much to tax a user's income if their monthly income falls within that range.

### Fields
|Name|Description|
|-|-|
|ID|GUID of this Bracket|
|Jurisdiction|Jurisdiction this bracket belongs to|
|Name|Name of this bracket|
|Start|Start of the range of this bracket (Inclusive)|
|End|End of the range of this bracket (Exclusive)|
|Type|Type of user this bracket belongs to (IE Corporate users)|
|Rate|Rate to tax the income at.

### Methods
(None)

## Tax Jurisdiction
A Jursdiction, federal or local, that holds tax brackets, and can caluclate the tax on a given user's income.

### Fields
|Name|Description|
|-|-|
|ID|GUID of this Jurisdiction|
|Name|Name of this jurisdiction|
|Brackets|Brackets this Jurisdiction contains|
|Account|Neco Bank Account to which taxes will be paid out to|

### Methods
|Method|Description|
|-|-|
|CalculateTax()|Calculates the tax for a given user based on given income earned in a month. Returns a tuple of the amount, and the bracket used to calculate the tax|

## Tax Report
A tax report for a given month, generated for a user using their static income items, and current month's transactions, along with their federal jurisdiction to tax transactions on.

### Fields
|Name|Description|
|-|-|
|ID|GUID of this report|
|Owner|User this report belongs to|
|PreparedDate|Date this report was prepared for|
|Static Income|Static income during the time this report was prepared for|
|Extra Income|Extra income for the given transactions|
|Extra Income Taxable|Taxable extra income for the given transaction|
|Grand Total Income|Grand total income for the report|
|Grand Total Tax| Calculated grand total tax for the report|
|Report|A text file report that is generated to be human readable|
|CSVReprot|A CSV version of the report used for exporting to other programs|
|PaymentBreakdownDictionary| A dictionary of Jurisdicitons, and the amount of tax to be paid out to each one. Used internally for actual tax payment purposes, and is not saved or json serialized|
|IncomeBreakdownDictionary| A dictionary of jurisdictions, and the amount of income made in each one. Used internally for actual tax payment and calculation purposes, and is not saved or json serialized|

### Methods
|Method|Description|
|-|-|
|GenerateTaxReport()|Generates a Tax Report|

## Tax User Info
A leftover object that basically only holds a collection of listview items, and a calculation for total monthly income. Was originally meant for the frontend, but will probably go unused or rarely used

### Fields
|Name|Description|
|-|-|
|Items|Items in this TUI|

### Methods
|Method|Description|
|-|-|
|TotalMonthlyIncome()|Sum of the monthly incomes of all items|
