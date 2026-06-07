# HRMS Backend - Clean Architecture

## Overview
Enterprise Human Resource Management System built with ASP.NET Core 9, Clean Architecture, DDD, CQRS, and MediatR.

## Architecture
- **Domain** - Entities, Value Objects, Enums, Domain Events, Exceptions
- **Application** - CQRS Commands/Queries, Handlers, DTOs, Validators, Interfaces
- **Infrastructure** - EF Core, SQL Server, Identity, Services, Repositories
- **WebApi** - Controllers, Middlewares, Filters, Swagger, Health Checks
- **Contracts** - Shared request/response models
- **Shared** - Common utilities, Result pattern, Guards

## Modules
1. Employees, Departments, Positions, Branches
2. Attendance (Check-In/Out, Shifts)
3. Leave Management (Requests, Approvals, Balances)
4. Payroll (Batches, Payslips, Salary Structures)
5. Recruitment (Job Vacancies, Candidates, Interviews)
6. Performance Management (KPIs, Reviews)
7. Notifications (Email, SMS, InApp, Push)
8. Authentication & Authorization (JWT, Roles, Permissions)
9. Audit Logs

## Quick Start

### Prerequisites
- .NET 9 SDK
- SQL Server / Docker

### Run with Docker
```bash
cd docker && docker-compose up -d
```

### Run locally
```bash
# Update connection string in src/HRMS.WebApi/appsettings.json
dotnet run --project src/HRMS.WebApi
```

### Default Admin Credentials
- Email: admin@hrms.com
- Password: Admin@12345

### API Documentation
Navigate to: https://localhost:7001/swagger

## Running Tests
```bash
dotnet test HRMS.sln
```
