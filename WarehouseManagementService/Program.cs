using Microsoft.EntityFrameworkCore;
using WarehouseManagementService.Domain;
using WarehouseManagementService.Domain.Interfaces;
using WarehouseManagementService.Domain.Mapper;
using WarehouseManagementService.Domain.Repositories;
using WarehouseManagementService.Domain.Services;
using WarehouseManagementService.Infrastructure.BackgroundServices;

var builder = WebApplication.CreateBuilder(args);

// Read database parts from environment variables
var dbUser = builder.Configuration["database.username"];
var dbPass = builder.Configuration["database.password"];
var dbHost = builder.Configuration["database.hostname"];
var dbName = builder.Configuration["database.databasename"];

// Construct connection string
var connectionString = $"Server={dbHost};Database={dbName};User Id={dbUser};Password={dbPass};TrustServerCertificate=True;MultipleActiveResultSets=true;";


builder.Services.AddDbContext<WarehouseManagementDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Register services
builder.Services.AddScoped<CustomersService>();
builder.Services.AddScoped<CustomersRepository>();
builder.Services.AddScoped<ProductsService>();
builder.Services.AddScoped<ProductsRepository>();
builder.Services.AddScoped<PurchaseOrdersService>();
builder.Services.AddScoped<PurchaseOrdersRepository>();
builder.Services.AddScoped<SalesOrdersService>();
builder.Services.AddScoped<SalesOrdersRepository>();
if (builder.Configuration["USE_SFTP"] == "true")
    builder.Services.AddSingleton<IFilePollingService, SftpFilePollingService>();
else
    builder.Services.AddSingleton<IFilePollingService, LocalFilePollingService>();

builder.Services.AddHostedService<PurchaseOrderPollingService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<WarehouseManagementDbContext>();
    dbContext?.Database.Migrate(); // Applies any pending migrations
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
