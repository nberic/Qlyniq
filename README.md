# Qlyniq
Qlyniq is an ASP.NET Core MVC application serving as a project for the Databases 2 exam. 
The app supports CRUD actions on records of patient and employee data, patient files etc. 
The data is being stored by a MySQL database, 
while the querying of the data is being performed by Entity Framework Core.

## Prerequisites:
The SQL script that was used to generate the database and reverse engineer it into models 
contains template instructions for scaffolding dbcontext and aspnet-codegenerator.
Also, don't forget to install dotnet ef tool globally as well as dotnet aspnet-codegenerator tool.

## Installation
Clone the repository on your local computer to a preferred location and navigate to the location.
If one desires, they may checkout a branch they wish, for specific changes, features or improvements.

## Configuring runtime
Visit [https://dotnet.microsoft.com/download/dotnet-core/2.2](https://dotnet.microsoft.com/download/dotnet-core/2.2)
and download newest version of **.NET Core 2.2** SDK for your operating system.
Thorough installtion instructions can be found at the previous URL.
If necessary reopen your terminal / command prompt or even restart your machine.
Runtime configuration can be found in file **global.json**, which instructs dotnet runtime
about required SDK for running the _Qlyniq_ app.

## Database configuration
The **Qlyniq** has been configured to connect to a **MySQL** server instance, 
so in order to run **Qlyniq** one should have a **MySQL** server instance running on their machine.
Database configuration, i.e. _ConnecetionString_ can be found in **Qlyniq** > **appsettings.json**.
Input your instance-specific _ConnectionString_ details in the **appsettings.json** file.

## Initial Migrations
Now, when the database is running and the _ConnectionString_ is set up, 
we can create (i.e. migrate) the **Qlyniq**'s database tables to your local **MySQL** server.
In any case, delete the **Qlyniq** > **Migrations** directory by executing:

- if running on Windows


    `del Qlyniq\Migratoins /s /q`
    

- if running on Linux / Mac


    `rm -rf Qlyniq/Migrations -y`

Navigate inside the **Qlyniq** directory inside the **Qlyniq** solution by running `cd Qlyniq` .
Now migrate the database informations from the **Qlyniq** to **MySQL** server by running

    dotnet ef migrations add InitialMigration

After the database informations from the **Qlyniq** have been migrated to your local **MySQL** server,
update / push changes to the database by running:

    dotnet ef database update

## Running
Before running, on Windows machines you could add the app's certificate to trusted certificates by running:

    dotnet dev-certs https --trust

Inside the **Qlyniq** project directory execute

    dotnet run

Now, open your web browser and open `https://localhost:5001` and there is the **Qlyniq** .

### Seeding the Database
In fact, for the first run, the database will be seeded with some values in order to see frontend properties at the first run.
