# Warehouse Management Service API

![.NET Core](https://img.shields.io/badge/.NET-6.0-blue)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

## Table of Contents
- [Project Overview](#project-overview)
- [Architecture](#architecture)
- [Project Structure](#project-structure)
- [Prerequisites](#prerequisites)
- [Configuration](#configuration)
- [Installation](#installation)
- [Running the Application](#running-the-application)
- [API Documentation](#api-documentation)
- [Testing](#testing)
- [License](#license)

## Project Overview
A robust Warehouse Management System built with ASP.NET Core Web API and Entity Framework Core. Manages inventory, customers, purchase orders, and sales orders with full CRUD operations.

## Architecture
The application follows Clean Architecture principles:

## Project Structure
WarehouseManagementService/
├── Controllers/
├── Domain/
│ ├── Dtos/ # Data Transfer Objects
│ ├── Mapper/ # AutoMapper Profiles
│ ├── Models/ # Entity Models
│ ├── Repositories/ # Data Access
│ ├── Services/ # Business Logic
│ └── Utilities/ # Helpers and Extensions
├── Migrations/ # EF Core Migrations
└── WarehouseManagementTest # Unit Tests

## Prerequisites
- [.NET 6 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or Azure SQL
- [Docker](https://www.docker.com/) (optional)

## Configuration

1. Set up `launchSettings.json`:

```json
{
  "profiles": {
    "WarehouseManagementService": {
      "commandName": "Project",
      "launchBrowser": true,
      "launchUrl": "swagger",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development",
        "DB_SERVER": "localhost",
        "DB_NAME": "WarehouseDB",
        "DB_USER": "sa",
        "DB_PASSWORD": "your_password"
      },
      "applicationUrl": "https://localhost:5001;http://localhost:5000"
    }
  }
}
