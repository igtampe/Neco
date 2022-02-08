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

### Income Item
*(Identifiable, Nameable, Describable, Locateable, Dateable)* An Income item describes any item that generates monthly income based on a formula. Income items can also have miscellaneous income added to their monthly income calculation

#### Airline
An Airline is a type of **Corporation** specifically in the aviation industry (Usually for passanger operations only). It includes income bonuses for the number of different types and quantity of gates the airline has.

The income calculation for an airline is the same as a **Corporation** with an addition to the **Base Percentage** determined by the **Gates** the airline has.

##### Gate
A Gate defines a stand at which an airplane can dock at, and board passangers. These are located at airports. Neco supports three types of gates.
- Small Gates: Gates designed to hold small (IE: CRJ-200/700/900, Embraer E-Jet, or other regional airliner equivalents) or Ultra-Small (IE: Cessna Grand Caravan, or other general aviation plane equivalents) airplanes
- Medium Gates: Gates desined to hold medium (IE: MD-80, Boeing 737, Boeing 757, Airbus A220, Airbus A320 Family, or other narrowbody airliner equivalents) airplanes
- Large gates: Gates designed to hold Large (IE: Boeing 777, Boeing 787, Airbus A330, Airbus A350, MD-11, or other widebody airliner equivalents) or Ultra-Large (Boeing 747, Airbus A380, ot other double-decker widebody airliner equivalents) planes.

These provide a boost to the **Corporation Base Percentage** of .2%, .4%, and .5% respectively per each gate of that kind.

#### Apartment
An Apartment is a type of building that contains multiple **Units** rentable for a monthly fee. These should contain at least 1 kitchen, and a location to put down at least 1 bed. These can be furnished or unfurnished. 

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
A business is a type of locale that has multiple **points of sale**, which each see a certain amount of **Customers per Hour**, each having a determined **Average Spending** during all of said business' **Hours Open** of the day.

The income calculation for a business is half of its average spending multiplied by its customers per hour, its hours open, and its points of sale. This 

##### Average Spending
##### Customers Per Hour
##### Hours Open
##### Points of Sale
#### Corporation
##### Advertisements
##### Buildings
##### Internationality
##### Mergers
##### Real Life Equivalent (RLE)
##### RLE Net Yearly
#### Hotel
##### Rooms
##### Suites
### Income Type
### The Salary Determination Committee
## Notification
## The Pecunia
## Taxes
### Bracket
### Jurisdiction
#### Jurisdiction Type
### Report
## User
