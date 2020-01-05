# Bucks Calendar 🗓

This repo contains the calendar application developed for the final project of the Web Applications module.


## Requisites

- An IDE for ASP.NET Core. Rider is the recommended option.
- ASP.NET Core v.3.0 SDK
- Docker


## How to run the application

1. Clone the repository
2. Open the solution in your IDE
3. In the command line, run `dotnet restore` to install all dependencies
4. Pull a docker image for MS SQL Server for Linux using: `docker pull microsoft/mssql-server-linux:2017-latest`
5. Run the SQL Server image using: `docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=<secure_password_here>' -p 1433:1433 --name sql_db -d microsoft/mssql-server-linux:2017-latest`
6. Once the database is up, run the application in your IDE

**Note:** when running the command to create and run the SQL Server image, ensure the password is secure enough, otherwise it will fail to spin up the instance.
