FOOTBALLWEB PROJECT SETUP

Requirements:
- Visual Studio 2022
- SQL Server / LocalDB
- .NET SDK

Steps:

1) Open FootballWEB.slnx in Visual Studio.

2) Open SQL Server Management Studio.

3) Run FootballDB_SCRIPT.sql to create the database.

4) Check appsettings.json connection string.

5) Open Terminal and run:

dotnet restore

6) Run the project with Ctrl + F5.

Extra:
FootballDB_NEW.bak is included as a backup database file.

NOTE:
This project was developed and tested using Microsoft SQL Server / LocalDB.

If another DBMS (such as Oracle or MySQL) is preferred, database-specific SQL syntax and connection configurations may need to be adapted.