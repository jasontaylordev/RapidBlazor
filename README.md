# Rapid Blazor Template for .NET

[![Nuget](https://img.shields.io/nuget/v/JasonTaylorDev.RapidBlazor?label=NuGet&)](https://www.nuget.org/packages/JasonTaylorDev.RapidBlazor)
[![Nuget](https://img.shields.io/nuget/dt/JasonTaylorDev.RapidBlazor?label=Downloads&)](https://www.nuget.org/packages/JasonTaylorDev.RapidBlazor)
[![Discord](https://img.shields.io/discord/893301913662148658?label=Discord)](https://discord.gg/p9YtBjfgGe)
![Twitter Follow](https://img.shields.io/twitter/follow/jasontaylordev?label=Follow&style=social)

This is a solution template for creating a Blazor WebAssembly application hosted on ASP.NET Core 6 and following the principles of Clean Architecture.

Please consider this a preview, I am still actively working on this template. If you spot a problem or would like to suggest an improvement, please let me know by [creating an issue](https://github.com/jasontaylordev/RapidBlazor/issues).

If you find this project useful, please give it a star. Thanks! ⭐

## Getting Started
The solution template requires the latest version of [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0).

Install the project template:

```bash
dotnet new install JasonTaylorDev.RapidBlazor
```

Create a new app:

```bash
dotnet new rapid-blazor-sln --output AwesomeBlazor
```

Launch the app:
```bash
cd AwesomeBlazor\src\WebUI\Server
dotnet run
```

## Database
### Configuration
The template is currently configured to use [SQL Server Express LocalDB](https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver16) for development and [Azure SQL](https://learn.microsoft.com/en-us/azure/azure-sql/database/sql-database-paas-overview?view=azuresql) once deployed. I understand this will be difficult for some developers, and will look other options in the near future.

### Migrations
The template uses Entity Framework Core and migrations can be run using the EF Core CLI Tools. Install the tools using the following command:

```bash
dotnet tool install --global dotnet-ef
```

Once installed, create a new migration with the following commands:

```bash
cd src\Infrastructure
dotnet ef migrations add "Initial" --startup-project ..\WebUI\Server
```

Review the [Entity Framework Core tools reference - .NET Core CLI | Microsoft Docs](https://learn.microsoft.com/en-us/ef/core/cli/dotnet) to learn more.

## Deploy
The project can easily be deployed to Azure using the included GitHub Actions workflows and Bicep templates. Review the [Deployment instructions](https://github.com/jasontaylordev/RapidBlazor/wiki/Deployment) to learn how.

## Resources
The following resources are highly recommended:

* [Blazor Workshop | .NET Presentations: Events in a Box!](https://github.com/dotnet-presentations/blazor-workshop)

* [Deploy Azure resources by using Bicep and GitHub Actions | Microsoft Learn](https://learn.microsoft.com/en-us/training/paths/bicep-github-actions/)

* [Automate administrative tasks by using PowerShell - Training | Microsoft Learn](https://learn.microsoft.com/en-us/training/paths/powershell/)

## Support
If you are having problems, please let me know by [creating an issue](https://github.com/jasontaylordev/RapidBlazor/issues).

## License
This project is licensed with the [MIT license](https://github.com/jasontaylordev/RapidBlazor/blob/main/LICENSE).
