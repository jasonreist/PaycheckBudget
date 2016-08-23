"# PaycheckBudget"

This is a budgeting web application using C# MVC and SQL Server. It's designed to balance your budget on a paycheck-by-paycheck basis instead of monthly.

Technologies / Requirements:

Visual Studio 2013/2015
ASP.NET 4.5.1
MVC 5
Entity Framework 6
Database: Initially the connection strings are set to Microsoft LocalDB

Steps to Run:
- Once you pull the solution, can need to update the correct connection string for the database you're going to use.
 - This is done in the web.config file for both the PB.UI project and PB.Services
- Open the properties for the Solution and make sure that the PB.UI and PB.Services are both set to Start.
- When you register an account the first time the database and tables will be generated.
 - A complete profile is created for you including settings, paychecks, paydays, bills, and custom bills.

Items of Note:
- As of now, the app only supports "every 2 week" pay periods.
- The application will automatically look for bills that are due on days that don't exist.
 - IE. if you have a bill due on the 30th, and you are in February, it will show that bill as being due on the 28th.
