# Ultrix
> Ultrix is a meme website for collecting memes and sharing them with friends on the website.

For this project the MoSCoW method was used, this can be found in the [Projects](https://github.com/metalglove/Ultrix/projects) tab on this repository.

## Getting Started
Use these instructions to get the project up and running.

### Prerequisites
You will need the following tools:

* [Visual Studio 2017 or 2019](https://www.visualstudio.com/downloads/)
* [.NET Core SDK 2.2](https://www.microsoft.com/net/download/dotnet-core/2.2)

### Setup
Follow these steps to get your development environment set up:

  1. Clone the repository
  2. Create a `appsettings.json` file at the root of the Presentation layer with the connectionstring to a fresh database:
     ```json
        {
            "Logging": {
                "LogLevel": {
                "Default": "Warning"
                }
            },
            "AllowedHosts": "*",
            "ConnectionStrings": {
                "ApplicationDatabase": "Server={Instance};Database={DATABASE};User Id={USER};Password={PW};",
                "InMemoryDatabase": "InMemoryDatabase"
            }
        }
     ```
  3. Next, go to `Tools > NuGet Package Manager > Package Manager Console` in visual studio, in the console type:
     ```
     Add-Migration InitialCreate
     
     Update-Database
     ```
     The `Add-Migration` command scaffolds a migration to create the initial set of tables for the model. The `Update-Database` command creates the database and applies the new migration to it.
  4. Next, build the solution either by selecting it in the `Build > Build solution` in visual studio or hitting `CTRL + SHIFT + B`.
  5. Once the build has run succesfully, start the website to confirm that the database connection is succesfull either by hitting `F5` or go to `Debug > Start`.
  6. Launch [http://localhost:52468/](http://localhost:52468/) in your browser to view the website. If memes start flowing in then it went succesfully, else your connectionstring was probably incorrect. If that is not the case please create an issue if you want help with that.
  7. For the time being the you need to manually insert a `CredentialType` for authentication. Run this query:
  ```SQL
    INSERT INTO [dbo].[CredentialTypes] ([Code], [Name], [Position]) VALUES ('Email', 'Email', 1)
  ```
  8. Now users are able to register and login, have fun!

**NOTE:** If you also want to run all the tests, also create a `appsettings.json` file in the root of the Tests layer. 
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
