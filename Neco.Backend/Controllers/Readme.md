# Neco Controllers

The Neco Controllers handle each their own section of Neco. They are based on the actions the ViBE Server could do, which are represented in this (massive) flow:
![ViBE Flow](https://github.com/igtampe/Neco/blob/master/Images/Flowchart/ViBE%20Flow.png)

This was separated into the following flows for the different controllers:

## Neco (And Auth)
Handles the Neco core operations (Transactions, bank accounts, users and Auth)
![NecoAuth](https://github.com/igtampe/Neco/blob/master/Images/Flowchart/NecoAuthController.png)

## Checkbook Controller
Handles sending, retrieving, and executing Checkbook items
![Checkbook](https://github.com/igtampe/Neco/blob/master/Images/Flowchart/CheckbookController.png)

## Contractus Controller
Handles Posting, bidding, assigning, and completing contracts
![Contractus](https://github.com/igtampe/Neco/blob/master/Images/Flowchart/ContractusController.png)

## EzTax Controller
Handles Managing and updating EzTax Static Income Items and Subitems, and can also handle taxing and income monthly operations
![EzTax](https://github.com/igtampe/Neco/blob/master/Images/Flowchart/EzTaxController.png)

## Landview Controller
Handles getting all countries, districts, and plots, getting images for these items, and handles creating new plots
![Landview](https://github.com/igtampe/Neco/blob/master/Images/Flowchart/LandViewController.png)

## UMSAT Controller
Handles managing UMSAT assets, and getting list of assets or specific assets
![UMSAT](https://github.com/igtampe/Neco/blob/master/Images/Flowchart/UMSATController.png)

