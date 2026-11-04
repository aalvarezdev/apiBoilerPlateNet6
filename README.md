# API Boilerplate (.NET 10)

Placeholder API used for CI/CD exercises. The runnable solution targets .NET 10 and exposes a deterministic endpoint without needing a database or cloud credentials:

```text
GET /api/dummies
```

It returns three sample objects (`PocoID` and `POCODescription`) and has a unit test in `Backend.Api.Tests`.

## Run locally

```bash
dotnet restore Backend.sln
dotnet run --project Backend.Api
```

Browse to `http://localhost:5000/api/dummies` (or the HTTPS URL shown by `dotnet run`). Run the test suite with `dotnet test Backend.sln`.

## Run in Docker

From the repository root:

```bash
docker build -f Backend.Api/Dockerfile -t api-boilerplate:local .
docker run --rm -p 8080:8080 api-boilerplate:local
```

The endpoint is available at `http://localhost:8080/api/dummies`.

## Architecture

Backend.Application - Class Library

This component encapsulates all application business logic. It employs the Command and Query Responsibility Segregation (CQRS) pattern to facilitate the execution of all supported operations.

Backend.Common – Class Library

This library houses shared enumerations and values utilized throughout the application, including environment settings and configurations.

Backend.Domain – Class Library

This section details the structure of Database Objects in Plain Old CLR Object (POCO) format, allowing for adaptability with various Relational Database Management Systems (RDBMS). It leverages Entity Framework Core for fluent configuration of POCO properties.

Backend.Infrastructure - Class Library

This module is responsible for configuring the infrastructure, including databases, services, and the definition of entities within the RDBMS. It employs a fluent language for domain validation, independent of the database vendor. This flexibility allows for support of various vendors (e.g., SQLite, PostgreSQL, SQL Server, etc.), enhancing the system's versatility and reuse potential.
