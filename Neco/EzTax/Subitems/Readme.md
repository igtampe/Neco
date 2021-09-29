# EzTax Subitems
This folder contains subitems for EzTax. These all have the following common fields

|Name|Description|
|-|-|
|ID|GUID of this item|
|Name|Name of this Item|
|IncomeItem| IncomeItem this subitem belongs to|

They also include an abstract `Income()` method that must be implemented by the other classes. The following includes the list of the three types of subitems:

## Apartment
Aparment comprised of units of Studio, 1 bedroom, 2 Bedroom, 3 Bedroom, and Penthouse/4+ Bedroom Size each with tied monthly rent rates.

**Income Calculation**: `(SRent * SUnits) + (B1Rent * B1Units) + (B2Rent * B2Units) + (B3Rent * B3Units) + (PRent * PUnits)`

## Hotel
Hotel comprised of two types of rooms: Rooms and Suites, each with a tied nightly rate, and potential misc monthly income

**Income Calculation**: `(RateToMonthlyIncome(RoomRate) * Rooms) + (RateToMonthlyIncome(SuiteRate) * Suites) + MiscIncome`,
Where `RateToMonthlyIncome()` is defined as `((Rate / 2) * 365) / 12`

## Business
Business comprised of hours a day open, average customers per hour, and average spending. For Restaurants, a Chairs field is also specified, to define Customers per hour, as customers per chair per hour, making it easier to calculate. For businesses that are not restaurants and want to avoid the chairs field, it should be set to 1.

**Income Calculation**: `(((AvgSpend / 2) * CustPerHour * HoursOpen) * Chairs) * 30`
