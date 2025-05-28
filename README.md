Project Overview
This is a Warehouse Management Service built as an ASP.NET Core Web API using Microsoft.EntityFrameworkCore for data access. The service manages customers, products, purchase orders, and sales orders for a warehouse operation.

Project Structure
(Structure remains the same as previously described)

Configuration (Updated)
The application is configured through launchSettings.json with the following settings:

Development Profile
Port: 5107

Swagger URL: http://localhost:5107/swagger/index.html

Environment Variables:

ASPNETCORE_ENVIRONMENT: "Development"

database.username: Your database username

database.password: Your database password

database.hostname: Your database server hostname

database.databasename: Your database name

Docker Profile
Port: 8080

SSL: Disabled

HTTP Ports: 8080

How to Run the Application (Updated)
Development Mode
Ensure your launchSettings.json has the correct database credentials

Run the application using:

bash
dotnet run
or through your IDE using the "WarehouseManagementService" profile

The application will:

Start on http://localhost:5107

Automatically open Swagger UI in your browser

Apply any pending database migrations

Docker Deployment
Build the Docker image:

bash
docker build -t warehouse-management .
Run the container with your environment variables:

bash
docker run -p 8080:8080 \
  -e ASPNETCORE_ENVIRONMENT=Development \
  -e database__username=your_username \
  -e database__password=your_password \
  -e database__hostname=your_host \
  -e database__databasename=your_db \
  warehouse-management
Access the API at http://localhost:8080

Environment Configuration
For production deployments, you can override the launch settings by:

Setting environment variables directly on the host

Using a production appsettings.json file

Using secrets manager for sensitive data

Important Notes
The development profile uses HTTP (not HTTPS) for easier debugging

Database credentials in launchSettings.json are only for development

For production, use proper secret management

The Docker profile runs on port 8080 by default

API Documentation
The Swagger UI is automatically available at:

Development: http://localhost:5107/swagger/index.html

Docker: http://localhost:8080/swagger/index.html

Troubleshooting
If you encounter connection issues:

Verify your database credentials in launchSettings.json

Check that your database server is accessible

Ensure the database exists and the user has proper permissions

For Docker, verify all environment variables are correctly set