 <img src="https://raw.githubusercontent.com/igtampe/Neco/master/SPA/ClientApp/public/NecoColor.png" width="300"/>
Neco (.NET Economy) is a successor to <a href="https://github.com/igtampe/ViBE">ViBE</a>, working with more industry standard tools, and an actual database. The Backend runs on ASP.NET Core, and uses Entity Framework to communicate to a SQL Server database running on the local machine by default, but can be configured to use a different connection string. Neco encompasses the same subprograms as the <a href="https://github.com/igtampe/VibeServer">ViBE Server</a>, along with more integration with other services like <a href="https://github.com/igtampe/Imex">Income Manager Express (IMEX)</a>, and the <a href="https://github.com/igtampe/UMSAssetTrack">UMS Asset Tracking System (UMSAT)</a>. Not only that, it provides more possibilities, like more banks, multiple bank accounts, better income item management, a Reworked LandView, and more. Check the Readme on the Neco Backend for more details on the controllers and what they do.<br>
<br>
Due to using SQL Server, Neco is no longer cross compatible with ViBE (as ViBE was with its own predecessor). We've made a small program to migrate all the data from ViBE to the new DB, which takes in data from ViBE's folders, files, and from the UMSAT Database. See the ViBE Neco Migrator for more information on how it works.<br>
<br>
Neco Also completely reworks Landview, to make it much more powerful than before. Landview will now allow users to create and verify their own plots, and purchase them from relevant districts using their Neco bank accounts. In order to create and update relevant District and Country information, Neco includes the LandView Plotter. See it for more details. Neco also can use the information from plots, districts, countries, and roads to generate images displaying them.<br>
<br>
Neco's Frontend is currently yet to be coded, but will likely just be a Winforms app to keep things simple. I'm a backend developer, not a frontend one. Although perhaps I'll decide to write something *really small* in React JS just to do some transactions....
<br>
hmmm... maybe.<br>
<br>
Anyways here's my next mega project. The whole point of this is to better learn how to work with ASP.NET, EF, And SQL Server. Let's see how it goes!
