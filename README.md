# Warehouse Management Service API

![.NET Core](https://img.shields.io/badge/.NET-9.0-blue)

## 📦 Project Overview

This is a .NET Core Web API project for managing warehouse operations, including customers, products, sales orders, and purchase orders. The application is structured using a clean, layered architecture and Entity Framework Core for database interaction.

---

## 🏗 Architecture

The application follows Clean Architecture principles, separating concerns between:

- Controllers
- Services (Business Logic)
- Repositories (Data Access)
- DTOs and AutoMapper
- EF Core for persistence

---

## 📁 Project Structure

```
WarehouseManagementService/
│
├── Controllers/                 # API Controllers
├── Domain/                     # Core application logic
│   ├── Dtos/                   # Data Transfer Objects
│   ├── Mapper/                 # AutoMapper profiles
│   ├── Migrations/             # EF Core Migrations
│   ├── Models/                 # Entity Models
│   ├── Repositories/           # Data access layer
│   ├── Services/               # Business logic layer
│   └── Utilities/              # Common utilities (e.g., response classes)
│
├── WarehouseManagementDbContext.cs   # EF Core DbContext
├── Program.cs                        # Application entry point
├── Dockerfile                        # For containerization
└── WarehouseManagementTest/          # Unit test project
```

---

## ✅ Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or Azure SQL
- [Docker](https://www.docker.com/) (optional)

---

## 🛠 Technologies Used

- ASP.NET Core Web API  
- Entity Framework Core (Code-First)  
- AutoMapper  
- Swagger for API documentation  
- SQL Server  
- xUnit or NUnit (for testing)

---

## ⚙️ Configuration

The application uses the following environment variables to configure database access:

| Environment Variable       | Description             |
|---------------------------|-------------------------|
| `database.username`       | SQL Server username     |
| `database.password`       | SQL Server password     |
| `database.hostname`       | SQL Server host/IP      |
| `database.databasename`   | Name of the database    |

For local development, these are configured in `launchSettings.json`:

```json
"environmentVariables": {
  "ASPNETCORE_ENVIRONMENT": "Development",
  "database.username": "username",
  "database.password": "****",
  "database.hostname": "hostname",
  "database.databasename": "dbname"
}
```

---

## 🚀 Running the Project Locally

### 1. Clone the repository

```bash
git clone https://github.com/your-org/warehouse-management-service.git
cd warehouse-management-service
```

### 2. Run the application

Launch the API using the built-in settings from Visual Studio or via CLI:

```bash
dotnet run --project WarehouseManagementService
```

The API will be available at:  
👉 `http://localhost:5107/swagger`

### 3. Apply EF Core Migrations (if needed)

EF Core migrations are applied automatically at startup. To manually create or apply:

```bash
dotnet ef migrations add InitialCreate --project WarehouseManagementService
dotnet ef database update --project WarehouseManagementService
```

---

## 🐳 Docker Support

### 1. Build the Docker image

```bash
docker build -t warehouse-management-service .
```

### 2. Run the container

```bash
docker run -e database.username=youruser \
           -e database.password=yourpass \
           -e database.hostname=host.docker.internal \
           -e database.databasename=WarehouseDB \
           -p 5001:5001 \
           warehouse-management-service
```

---

## 🧪 Running Tests

```bash
dotnet test WarehouseManagementTest
```

---

## 📖 Swagger API Docs

Navigate to:  
🔗 `http://localhost:5107/swagger`  
to view the interactive Swagger documentation.

---

## 🔮 Future Improvements

- Authentication and authorization (JWT)
- Logging and exception handling middleware
- Health checks and observability metrics
- CI/CD pipeline setup

---

