# Warehouse Management Service API

![.NET Core](https://img.shields.io/badge/.NET-6.0-blue)

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

- [.NET 6 SDK](https://dotnet.microsoft.com/download)
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

The application reads database credentials from environment variables:

| Environment Variable       | Description             |
|---------------------------|-------------------------|
| `database.username`       | SQL Server username     |
| `database.password`       | SQL Server password     |
| `database.hostname`       | SQL Server host/IP      |
| `database.databasename`   | Name of the database    |

---

## 🚀 Running the Project Locally

### 1. Clone the repository

```bash
git clone https://github.com/your-org/warehouse-management-service.git
cd warehouse-management-service
```

### 2. Set environment variables

Create a `.env` file or set environment variables in your terminal:

```bash
export database.username=youruser
export database.password=yourpass
export database.hostname=localhost
export database.databasename=WarehouseDB
```

### 3. Run database migrations (optional)

EF Core automatically applies migrations at startup. To manually apply them:

```bash
dotnet ef migrations add InitialCreate --project WarehouseManagementService
dotnet ef database update --project WarehouseManagementService
```

### 4. Run the application

```bash
dotnet run --project WarehouseManagementService
```

The API will be available at:  
👉 `https://localhost:5001`

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
🔗 `https://localhost:5001/swagger`  
to view the interactive Swagger documentation.

---

## 🔮 Future Improvements

- Authentication and authorization (JWT)
- Logging and exception handling middleware
- Health checks and observability metrics
- CI/CD pipeline setup

---

## 📄 License

MIT License
