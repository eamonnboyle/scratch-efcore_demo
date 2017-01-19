# scratch-efcore_demo
Playing with Entity Framework Core doing a basic example to demo EF core and for a local meetup. 

Demonstrates
- Creating a database from a code first perspective. 
  - Using Lego toys/playsets as sample data.
- Running a few queries
- Talking to a MySQL databse or a SQL Server (edit LegoDbContext to switch)
- One project uses EF Core 1.1 on .NET Core and one uses EF 6 in .NET framework for comparison
- Creating a .NET standard library to share code.

# To Run
I used Visual Studio 2017 RC so you can open the solution in that and simply run.

Or use the MSbuild variant (alpha at time of writing) of the .NET Core SDK and tools on Windows, Mac or Linux

    dotnet restore
    dotnet build
    cd Lego.EFCore
    dotnet run

You will need to modify the connection strings in the LegoDbContexts to mach your own servers.
