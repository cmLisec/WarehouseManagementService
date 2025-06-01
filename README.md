# Warehouse Management Service API

![.NET 9.0](https://img.shields.io/badge/.NET-9.0-blue)

## 📦 Overview

Warehouse Management Service is a .NET 9 Web API designed to manage warehouse operations such as customers, products, sales orders, and purchase orders.  
It follows clean architecture principles and supports file-based integration via a built-in **background service**.

### ✅ Key Features

- RESTful API with Swagger for customer, product, sales, and purchase order management
- **Background service** that polls XML files from a local folder or SFTP, processes them, and logs the results
- Configurable using environment variables (via `launchSettings.json` or runtime environment)
- Extensible and testable design using interface-driven architecture
- Docker-compatible for containerized deployment

## 🧱 Project Structure
📦 Solution 'WarehouseManagementService' (2 projects)
│
└── 📂 WarehouseManagementService/
    ├── 📂 Controllers/
    ├── 📂 Domain/
    │   ├── 📂 Dtos/
    │   ├── 📂 Interfaces/
    │   ├── 📂 Mapper/
    │   ├── 📂 Models/
    │   ├── 📂 Repositories/
    │   ├── 📂 Services/
    │   └── 📂 Utilities/
    │   └── 📂 Migrations/
    │
    └── 📂 WarehouseManagementTest/ (Test project)


## 🔧 Configuration

The application is configured using environment variables defined in `launchSettings.json` or your hosting environment.

| Key                      | Description                        |
|--------------------------|------------------------------------|
| `database.username`      | SQL Server username                |
| `database.password`      | SQL Server password                |
| `database.hostname`      | SQL Server hostname                |
| `database.databasename`  | Name of the database               |
| `USE_SFTP`               | `true` or `false` (use SFTP?)      |
| `SFTP_*`                 | SFTP connection details            |
| `FilePolling_BasePath`   | Directory to poll files from       |
| `CronSchedule`           | Cron expression for polling        |
| `PollingIntervalMinutes` | Backup polling frequency           |
| `RetryCount`             | Number of retries on failure       |
| `RetryDelaySeconds`      | Delay between retries              |

## ⏱️ Background Service – PurchaseOrderPollingService

The `PurchaseOrderPollingService` is a `BackgroundService` that:

- Polls a configured directory (or SFTP) using a CRON schedule
- Deserializes XML files into purchase order DTOs
- Invokes the `PurchaseOrdersService` to validate and insert data
- Moves processed files to a `processed/` folder
- Logs and moves failed files to a `failed/` folder with `.log` files

It runs automatically in the background when the API starts.

## 🚀 Running Locally

### 1. Clone the Repository
```bash
git clone https://github.com/your-org/warehouse-management-service.git
cd warehouse-management-service
2. Update Environment Variables
Check Properties/launchSettings.json and set your values:

json
"environmentVariables": {
  "database.username": "SA",
  "database.password": "Lisec@123",
  "database.hostname": "atse-ngd-bc1",
  "database.databasename": "Chanchal-Test",
  "USE_SFTP": "false",
  "FilePolling_BasePath": "C:\\.Net 9 Learning\\WMS\\Test\\inbox",
  "CronSchedule": "* * * * *"
}
3. Apply EF Core Migrations
bash
dotnet ef database update --project WarehouseManagementService
4. Run the Application
bash
dotnet run --project WarehouseManagementService
Swagger UI will be available at:
📎 http://localhost:5107/swagger/index.html

The background service will start automatically and begin polling as per your CRON configuration.

🐳 Docker Support
Build the Image
bash
docker build -t warehouse-management-service .
Run the Container
bash
docker run -e database.username=SA \
           -e database.password=Lisec@123 \
           -e database.hostname=atse-ngd-bc1 \
           -e database.databasename=Chanchal-Test \
           -e FilePolling_BasePath=/app/inbox \
           -e USE_SFTP=false \
           -e CronSchedule="* * * * *" \
           -p 5107:80 \
           warehouse-management-service
🧪 Running Tests
bash
dotnet test WarehouseManagementTest