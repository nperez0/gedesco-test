
## Requirements

- Visual Studio 2019 or later
- .NET Core 5
- Docker (Linux Container)

## Instructions

On the docker folder select docker-compose and choose it as Startup Project and run it.

No need to run migrations manually, it's going to trigger when the application starts.

## Summary:
You are expected to implement the ASP.NET Core API below as a level test. We expect you to complete it within 2-3 h. of work, but you are free to spend more time if needed. You can commit right to master branch or create a working one.

## Problem description:
You must develop an ASP.NET Core API containing users, and their addresses history, with the features below:

- All entities must have basic CRUD operations.
- Additionally, you must develop an endpoint, with pagination and search features, that returns a list of users, with at least one direction each.

## Specs:
- All endpoints must be designed to be consumed.
- Focus on providing a high quality code, making use of good coding practices and design principles.
- CQRS pattern is not mandatory but stands as the preferred way to aproach this test.

## Mandatory:
- .NET 5
- Dependency Injection
- Use of EF Core combined with SQL Server/SQLite
- Unit tests for, at least, a couple of use cases
- At least one Code First Migration
- Use of Swagger

## Deliverables:
A solution with the implementation above. If the project is in a runnable state and other dependencies are required, please include a short description of how to run it in a file named how-to-run.md.
