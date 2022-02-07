 <img src="https://raw.githubusercontent.com/igtampe/Neco/master/SPA/ClientApp/public/NecoColor.png" width="300"/>
Neco (.NET Economy) is a successor and complete rework of <a href="https://github.com/igtampe/ViBE">ViBE</a>, working with more industry standard tools. The Backend runs on ASP.NET Core, and uses Entity Framework to communicate to a PostgreSQL database. While we're currently working with the essentials of ViBE (Sending and receiving money, managing income and taxes, and generating statistics data), we hope to make Neco flexible enough eventually to encompass all programs and subprograms in the ViBE Suite, including the <a href="https://github.com/igtampe/UMSAssetTrack">UMS Asset Tracking System (UMSAT)</a>, and a complete overhaul to LandView.<br>
<br>
Due to using a SQL database and changes to the domain, Neco is no longer cross compatible with ViBE (as ViBE was with its own predecessor)<br>
<br>
Neco's primary purpose beyond itself is to help me practice the use of new technologies, including C#, ASP.NET Web APIs, EF Core, Heroku, React.JS and Material UI.<br>
<br>

# Changes from ViBE
- Multiple bank accounts with the same banks are now permitted under one user account
    - Money is now sent to bank account IDs (9 digit numerical codes) rather than going through a user ID, then a bank ID (57174/UMSNB)
- Bank accounts can now be shared between users
    - Several distinctions were moved from the User level to the Bank Account level, including:
        - Type distinctions (Corporate, Personal, Gov., Charity)
            - This along with multiple accounts from the same bank should eliminate the need for ViBE's KeyRing.
        - Income Items
        - Taxes

# Changes from Neco Pre-Reset
<i>This is the second attempt to create Neco. To see the first attempt, see the <a href="https://github.com/igtampe/Neco/tree/pre-reset">Pre-Reset branch</a>.</i><br>
<br>
After learning the lessons of project organization and construction from <a href="https://github.com/igtampe/Clothespin">Clothespin</a>, we decided to restart development in Neco and simplify its scope temporarily. The massiveness of pre-reset Neco made it cahllenging to focus on delivering the minimum viable product. Post-reset Neco expects to be flexible and extensible enough to eventually implement Pre-reset Neco's planned and semi-implemented features. Some of the content cut and changed at reset includes:<br>

   - Only one frontend, the ReactJS frontend.
       - Litterbox and its functionalities have been merged into Neco's ReactJS Frontend
       - The Landview Plotter has been cut, but may be re-introduced into the ReactJS Frontend.
   - Simplification of project organizations
       - Folders and project names are kept simple. Common, Data, API, and SPA (Single Page Application)
   - Integration of the API into the SPA
       - Allows launching Neco from a single application, rather than two.
   - Jurisdiction levels, Parents and Children
       - IncomeItems only require a single jurisdiction, the most specific one that they belong in. Neco will now determine the federal jurisdiction by finding its topmost parent. This allows for up to four levels of taxation at the Federal, State, County, and City level, automatically managed by Neco's backend. 
   - Better use of interfaces and abstract classes
       - Neco uses the same adjective naming system for interfaces as Clothespin. It also leverages abstract and extensible classes, especially for IncomeItems
           - We hope to use the same organization with Landview Items later in development
   - Simplifying of domain:
       - Remove BankAccountType: It's safe to assume banks would not offer different types in as simple of a domain as Neco's
       - Remerge BankAccountDetail: It's silly to separate the balance to a separate object for privacy. There's better ways of doing this
       - Remove interest rates from banks: This should not be stored on platform, at least not without some way to automatically dispense said interest rate's returns
       - Remove transaction state: Transactions should only be added to the database if they have been completed successfully
           - This will mean slightly more data has to be stored on a checkbook item when it is eventually implemented. However, given how flexible these have to be, pre-assigning them a transaction is overkill and should not be done.
       - Remove Usertype: This should've been an enum from the start.
       - Remove UserAuth: This shouldn't've been a separate table and certainly needed to have a link of some sort to the user table
       - Remove IncomeItem SubItems
           - Although it would've been interesting to have incomeitems allow for multiple sources of income as it did with ViBE, making these separate items makes it easier to calculate income and easier to display statistics
   - Removed CertifiedItems
       - Certifications for Transactiosn are now generated images based on the transaction information in the database itself, and are now verifiable by scanning the QR Code and feeding the data to the backend.
   - Remove "Read" state from Notifications
       - Notifications either exist or are deleted. There is now no in between. There really wasn't ever a need for one.
   - Multi-owner bank accounts
   - Type of acounts moved to Bank Account level rather than user level
   - Rebranding to better support dark modes. Sharper Neco Cat logo.
