# Appointment REST API
REST API for viewing and editing appointments.

## Dev stack & libraries
- C# & .NET Core 3.1
- Entity Framework Core 3.1.15
- MSSQL 
- Swashbuckle.AspNetCore 6.0.1 (Swagger is available on '/' URL path)

## Requirements for running locally
- .NET Core 3.1
- MSSQL Express Local DB

## Docs
- The DEV database is automatically created before the first startup.
- There are two users prepared for testing.
- The API implements simple resource-based authentication for editing and deleting data.