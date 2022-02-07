# ViBE Neco Migrator
![V2N](https://raw.githubusercontent.com/igtampe/Neco/master/Images/Vibe2NecoMigrator.png)
<br>
The ViBE To Neco Migrator migrates relevant data from folders and files the ViBE Server used to store data, and the Access database used for UMSAT. It also downloads the images from the URLs specified in the UMSAT database.<br>
<br>
Due to the new flexibility of Neco (Multiple bank accounts and bank account types), it creates the three banks (UMSNB, GBANK, and RIVER) and one type per bank, assigning bank accounts to users. It also assumes the following government bank accounts exist and are supposed to be tied to EzTax/IMEX Jurisdictions:<br>
<br>
|Account ID|Account Name/Jurisdiction|
|-|-|
|33118|The UMS|
|86700|The District of Newpond|
|86701|The District of Paradisus|
|86702|The District of Urbia|
|86703|The District of Laertes|
|86704|The District of North Osten|
|86705|The District of South Osten|
<br>
It also uses the last Tax jurisdiction information of the UMS Government.<br>
<br>
This code MUST be run on windows because we do the Access Database communication so do not run it on anything else. The ViBE Server is windows only so I don't imagine you'll be migrating data from a non-windows platform.<br>
