# Neco Common
This common package includes the objects used elsewhere in Neco. This readme includes a definition of the entities and other important concepts in Neco's *d o m a i n*

## Banking
Banking in Neco is defined as holding on to a certain amount of **Pecunia** in a **Bank** **Account**. 

### Bank
*(Identifiable, Nameable, Depictable)* Banks are managed institutions in Neco which hold **Accounts**. Banks may or may not be owned by individuals that can set an **Interest Rate** for funds held in the account.

### Account
*(Identifiable, Nameable, Locatable)* Accounts are the main entities in Neco, and can be owned by multiple **Owners**. These hold a **balance** of **pecunias**, which may be transfered in **transactions**. These also hold **Income Items** and can be **Taxed**. 

#### Owners
Owners are **users** who have access to an account and its details, and can manage its information, and execute commands from it.

#### Balance
Balance of **pecunias** in the account

#### Publicly Listed Account
An account which is publicly listed on the Neco **Account Directory**

#### Account Directory
Directory of all **Publicly Listed Accounts**. Useful when figuring out which account to send money to.

#### Closed Account
An account that has been emptied and can no longer be used for any **transactions** and should not be receiving any income.

### Interest Rate
An Interest Rate is a percentage used to determine how many **Pecunias** will be paid to an **Account** for holding their funds in said account, based on how many are already in the account. *This is not implemented explicitly in Neco, but is part of the domain it represents*. 

### Transaction
*(Identifiable, Nameable)* A Transaction defines the transfering of **Pecunias** from one account (The origin) to another (the destination). *Transactions are only saved in Neco if they are completed.*


## Image
*(Identifiable)* An image file's binary data (PNG, JPEG, GIF) stored in Neco's database.


## Income
Income in Neco is described as any amount of **pecunia** that is transferred into an **account**. For any recurrent quantity, the usual time period is monthly.

### Extra Income
Any income received from transactions to an **account**

### Income Item
*(Identifiable, Nameable, Describable, Locateable, Dateable)* An Income item describes any item that generates monthly income based on a formula. Income items can also have miscellaneous income added to their monthly income calculation

#### High Income Item
In Neco,  high income items are any items that make more than 500,000 p/month, or any **Corporation** or **Airline**. High Income Items must be approved by **The Salary Determination Committee**.

#### Airline
*(Identifiable, Nameable, Describable, Locateable, Dateable)* An Airline is a type of **Corporation** specifically in the aviation industry (Usually for passanger operations only). It includes income bonuses for the number of different types and quantity of gates the airline has.

The income calculation for an airline is the same as a **Corporation** with an addition to the **Base Percentage** determined by the **Gates** the airline has.

##### Gate
A Gate defines a stand at which an airplane can dock at, and board passangers. These are located at airports. Neco supports three types of gates.
- **Small Gates:** Gates designed to hold small (IE: CRJ-200/700/900, Embraer E-Jet, or other regional airliner equivalents) or Ultra-Small (IE: Cessna Grand Caravan, or other general aviation plane equivalents) airplanes
- **Medium Gates:** Gates desined to hold medium (IE: MD-80, Boeing 737, Boeing 757, Airbus A220, Airbus A320 Family, or other narrowbody airliner equivalents) airplanes
- **Large gates:** Gates designed to hold Large (IE: Boeing 777, Boeing 787, Airbus A330, Airbus A350, MD-11, or other widebody airliner equivalents) or Ultra-Large (Boeing 747, Airbus A380, ot other double-decker widebody airliner equivalents) planes.

These provide a boost to the **Corporation Base Percentage** of .2%, .4%, and .5% respectively per each gate of that kind.

#### Apartment
*(Identifiable, Nameable, Describable, Locateable, Dateable)* An Apartment is a type of building that contains multiple **Units** rentable for a monthly fee. These should contain at least 1 kitchen, and a location to put down at least 1 bed. These can be furnished or unfurnished. 

The income calculation for an Apartment is the sum of the multiplication of each **Unit** type's amount by its rent rate.

##### Units
Neco supports 5 types of Apartment Units:
- Studio Units: Units that have no defined bedroom, and instead have a multi-purpose living/bed room.
- 1 Bedroom Units: Units that have one bedroom.
- 2 Bedroom Units: Units that have two bedrooms.
- 3 Bedrooms Units: Units that have three bedrooms.
- 4 Bedroom/Penthouse Units: Units that have four bedrooms OR are at the top of a given building and have at least one bedroom

Rent rates for each of these type of units is customizable, but by default are 500p/month, 750p/month, 1000p/month, 1250p/month, and 1250p/month respectively. These rent rates should not include any fees paid to the apartment building for maintenance, as the income calculation assumes 100% profit from rent.

#### Business
*(Identifiable, Nameable, Describable, Locateable, Dateable)* A business is a type of locale that has multiple **points of sale**, which each see a certain amount of **Customers per Hour**, each having a determined **Average Spending** during all of said business' **Hours Open** of the day.

The income calculation for a business is half of its average spending multiplied by its customers per hour, its hours open, and its points of sale. This 

##### Average Spending
The average amount a cusotmer spends at this business on a given visit.

##### Customers Per Hour
The average amount of customers per hour that each point of sale serves

##### Hours Open
Amount of hours per day a business is open

##### Points of Sale
Number of places in which a sale can occur at a business. IE: In a restaurant, this represents every seat.

#### Corporation
*(Identifiable, Nameable, Describable, Locateable, Dateable)* In Neco, a Corporation is any large scale business with headquarter offices. Income for corporations and derivatives are based around the **Corporation Base Percentage** (Plus bonuses from other factors like **Buildings** and **Ads**) multiplied by the **RLE's Net yearly** divided by 12. Only accounts with the **Corporate Income Type** can file corporations and derviatives.

##### Advertisements
Advertisements are a way to announce a corporation exists and inform potential consumers of the products and services it offers. The **SDC** provides two types of advertisements:

- **Metro Ads**: Ads that appear in metro or other public transportation systems, such as a subway or a bus system. These provide a .1% increase to the **Corporation Base Percentage**
- **Airport Ads**: Ads that appear in airprorts. These proide a .3% increase to the **Corporation Base Percentage**

##### Buildings
Amount of additional **Headquarter Offices** this corporation has. The increase to the **Corporation Base Percentage** depends on the **RLE Net Yearly** of a given corporation, broken down in the table below:
|RLE Net Yearly Range (In Billion USD)|Bonus (%)|
|-|-|
|> 10|.05%|
|< 10 &  > 3 |.1%|
|< 3|.15%|

##### Corporation Base Percentage
The Corporation Base Percentage defines the base percentage for all **Corporations** to determine their income in the economy modeled by Neco by multiplying it by the **RLE Net Yearly** divided by 12. In this implementation of Neco, The Base Percentage is calculated as the **Population** of the head **Jurisdiction** of a corporation divided by 100 times the US population (100 * 329.5 million).

##### Headquarters Office
Any standard business office that is furnished. When approving a corporation, the **SDC** should verify the size of the office is comparable to the income that will be generated by the corporation.

##### Internationality
Determines whether or not a corporation provides services in multiple countries (whether or not they are in Neco). The **Salary Determination Committee** should verify that the size of the **Headquarter Offices** can support an international business, and if the business type/product/services can be **international**. Provides a .5% bonus to the **Corporation Base Percentage**

##### Mergers
Amount of corporations that have merged fully into this corporation. The increase to the **Corporation Base Percentage** depends on the **RLE Net Yearly** of a given corporation, broken down in the table below:
|RLE Net Yearly Range (In Billion USD)|Bonus (%)|
|-|-|
|> 10|.1%|
|< 10 &  > 3 |.15%|
|< 3|.2%|

##### Real Life Equivalent (RLE)
One or more (preferably publicly listed) corporation that exists in the real world which this corporation is modeled after. The **SDC** should verify that this corporation provides the same (or parts) of the services or products the RLE provides. If the corporation has more than one RLE, or provides only parts of the services the RLE provides, the **SDC** should work with the applicant of this corporation to determine the percentage of the RLE's net yearly income that should be reported for the **RLE Net Yearly**.

##### RLE Net Yearly
The complete, or percentage of, the **Real Life Equivalent's** Net yearly income in USD. If possible, provide net income before tax, as the taxes of the **Jurisdiction** this corporation is located in will be applied regardless. 

#### Hotel
*(Identifiable, Nameable, Describable, Locateable, Dateable)* A hotel is a building that contains multiple **Rooms** and **Suites**, along with other services including restaurants (filed separately) or a pool. Should contain a front desk and some sort of lobby area or office.

The income calculation for a hotel is the monthly income of rooms and suites added together.
The monthly income for rooms and suites is their rate divided by two multiplied by 365 divided by 12.
- This is meant to account for non-100% occupancy, and non-100% profit. 

##### Rooms
A Room with at least one bed. Comparable to a **Studio Apartment Unit**. The default room rate is 200$/night

##### Suites
A Room or multiple connected rooms with at least one bed and a living room space. Comparable to a **1 or 2 Apartment bedroom unit**. The default suite rate is $400/night

### Income Type
_(Enum)_ Income type is a property of **Accounts** that receive **Income** that determines how to **Tax** said income. Neco offers four types:
- **Personal:** Any personal account that is taxed as personal income.
- **Corporate:** Any corporate account that is taxed as corporate income.
- **Government:** Any account that is owned by a government entity. Money received from accounts with this type is not taxed.
- **Charity:** Any account that is owned by a Charity. Money sent to this account is **Tax Deductible**

### The Salary Determination Committee
The Salary Determination Committee is a committee of members of the Neco Administration and the **Jurisdictions** registered in the Neco system which oversee **Income Items** and approve those that are **High Income Items**. 

### Static Income
Income that is recurrent, usually from **Income Items**.

## Notification
*(Identifiable)* A bit of text which notifies a **User** of an action relating to their **Accounts**. IE: money received, or taxes payed out.

## The Pecunia
The currency handled in this implementation of Neco. Its abbreviation is `p` .The pecunia has no cents.

The Pecunia is meant to be pegged one to one with the US Dollar. It is a purely virtual currency and is not meant to be used in real world transactions. Although the pecunia is pegged one to one wit hteh US dollar, the Pecunia cannot be exchanged for US Dollars, or vice versa.

_The pecunia can be described as play-money. please senor uncle sam do not sue me_

## Taxes
Taxes in Neco is any amount of money payed to government accoutns determined by a percentage of **Income**. Taxes in Neco are paid monthly at the 15th of every month.

### Bracket
*(Identifiable, Nameable, Describable)* A Range of monthly income to which a percentage of tax is applied. 

Neco does not support partial taxation. As such, if a bracket begins at 5,000p and ends at 10,000p and has a rate of 5%, and an **account** has a total monthly **income** of 7,000p, the full 7,000p will be taxed at 5%.

### Jurisdiction
*(Identifiable, Nameable, Depictable)* A jurisdiction represents any region in which **income items**, **accounts**, and other **jurisdictions** can be located within. These can apply tax on any income received within them, determined by the **brackets** they define.

#### Jurisdiction Type
_(Enum)_ Neco supports 4 types of jurisdictions:
1. **Countries:** The highest level of jurisdiction. Could also be described as _Federal_ jurisdictions.
2. **States:** The second level of jurisdictions. Should be linked to a country jurisdiction. Could also be described as _Districts_.
3. **Counties:** The third level of jurisdictions. Should be linked to a state jurisdiction.
4. **Cities:** The fourth and last level of jurisdictions. Should be linked to a county jurisdiction. Could also be described as _Towns, or Municipalities_

All 4 types of jurisdictions can have different brackets. Taxes are paid out to each jurisdiction on the hierarchy. 

### Report
*(Identifiable)* In Neco, a Tax Report is a text and CSV (Comma Separated Values) summary of all income (**Extra** or **Static**) and **Taxes** paid on their totals depending on the total's **Jurisdiction**.

## User
*(Identifiable, Nameable, Depictable)* A User in the Neco system that has access to **Accounts** and **Roles**

### Roles
Neco provides certain roles to users including:
- **Administrator:** An Administrator of the Neco system. Bypasses all restriction of access
- **SDC:** A member of the **Salary Determination Committee**.
- **Government:** A member of a government registered in Neco (of any **Jurisdiction** and any **Jurisdiction type**) who has been granted access to the **Government**
- **Uploader:** Any user in the Neco system that has been verified by the Neco Administration and allows them to upload **images** to the neco system.

------

## Interfaces
Here are some of the interfaces and abstract classes that Neco uses.
|Interface|Description
|-|-|
|Dateable| Any item that contains both a date at which it was created, and a date at which it was updated
|Depictable| Any item with an image URL that can depict it|
|Describable| Any item with a string description field|
|Identifiable| Any item with an ID of any kind. There are two subtypes for Identifiable. These being **AutomaticallyGeneratableIdentifiable**, which is a GUID based identifiable whose ID is generated by the DB upon creation, and **ManuallyGeneratableIdentifiable**, which is an identifiable of any basic variable type (usually string) which can be generated using an ID Generator by the backend before being saved to the database|
|Locatable| Any item that has an address, and a **Jurisdiction**
|Nameable| Any item with a name|

### ID Generators

ID generators are small objects containing a function that can generate a random ID of any base type (Usually a string). In Neco, these include only one for the moment:
|Generator|Description|
|-|-|
|NumericalGenerator|A configurable on instantiation generator of Numerical string IDs of a certain length. (IE: Generating 5 number IDs for User accounts like 57174)|
