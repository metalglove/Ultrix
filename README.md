# Ultrix
> Ultrix is a meme website for collecting memes and sharing them with friends on the website.

For this project the MoSCoW method was used, this can be found in the [Projects](https://github.com/metalglove/Ultrix/projects) tab on this repository.

## Getting Started
Use these instructions to get the project up and running.

### Prerequisites
You will need the following tools:

* [Visual Studio 2017 or 2019](https://www.visualstudio.com/downloads/) (with the ASP.NET Core module)
* [.NET Core SDK 2.2](https://www.microsoft.com/net/download/dotnet-core/2.2)
* [SQLServer 2017](https://www.microsoft.com/nl-nl/sql-server/sql-server-downloads)

### Setup
Follow these steps to get your development environment set up:

  1. Clone the repository.
  2. Open PowerShell, connect to your local sqlserver and create a new database:
     ```sql
     SQLCMD.EXE -S "(LocalDb)\MSSQLLocalDB" -E

     CREATE DATABASE UltrixDb

     GO
     ```
  3. Create an `appsettings.json` file at the root of the Presentation layer with the connectionstring to the new database:
     ```json
        {
            "Logging": {
                "LogLevel": {
                "Default": "Warning"
                }
            },
            "AllowedHosts": "*",
            "ConnectionStrings": {
                "ApplicationDatabase": "Server=(LocalDb)\\MSSQLLocalDB;Database=UltrixDb;",
                "InMemoryDatabase": "InMemoryDatabase"
            }
        }
     ```
  4. Next, go to `Tools > NuGet Package Manager > Package Manager Console` in visual studio, To restore all dependencies:
     ```
     dotnet restore
     ```
     Followed by:
     ```
     dotnet build
     ```
     To make sure all dependencies were added succesfully, it should build without dependency warnings else you have probably not installed .NET core 2.2 SDK.
  5. Next, to add the code first database to your new database (make sure the default project is Ultrix.Persistance):
     ```
     Add-Migration InitialCreate
     ```
     Finally, update the database:
     ```
     Update-Database
     ```
     *The `Add-Migration` command scaffolds a migration to create the initial set of tables for the entities in the Persistance layer. The `Update-Database` command creates the database and applies the new migration to it.*
  6. Next, build the solution either by selecting it in the `Build > Build solution` in visual studio or hitting `CTRL + SHIFT + B` or if you are still in the package manager console by typing `dotnet build`.
  7. Once the build has run succesfully, start the website to confirm that the database connection is succesfull either by hitting `F5` or go to `Debug > Start`.
  8. Launch [http://localhost:60216/](http://localhost:60216/) in your browser to view the website.
  9. For the time being the you need to manually insert a `CredentialType` for authentication. Run this query (in PowerShell):
     ```sql
     USE UltrixDb

     GO

     INSERT INTO [dbo].[CredentialTypes] ([Code], [Name], [Position]) VALUES ('Email', 'Email', 1)

     GO
     ```
  10. Now users are able to register and login, have fun!

---

**NOTE:** If you also want to run all the tests, also create an `appsettings.json` file in the root of the Tests layer. 
```json
    {
        "ConnectionStrings": {
            "InMemoryDatabase": "InMemoryDatabase"
        }
    }
```
For the tests each method will create its own database with a random GUID and delete itself after completion of the test. This is done so that it can run in parallel and each test does not depend on anything. Because InMemoryDatabase is not *yet* a relational-database provider some tests will use the repository instead of the service to make the tests work as intended.

## Usage
*Explanation coming soon..*

## Contributing
PRs accepted.
