using System.Globalization;
using System.Text;
using System.Xml.Serialization;
using AutoMapper;
using Cronos;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using WarehouseManagementService.Domain.Dtos;
using WarehouseManagementService.Domain.Services;
using WarehouseManagementService.Domain.Interfaces;

namespace WarehouseManagementService.Infrastructure.BackgroundServices
{
    /// <summary>
    /// Background service that periodically polls for purchase order files,
    /// processes them, and moves them to appropriate directories based on processing results.
    /// </summary>
    public class PurchaseOrderPollingService : BackgroundService
    {
        private readonly ILogger<PurchaseOrderPollingService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IFilePollingService _filePollingService;
        private readonly CronExpression _scheduler;
        private readonly TimeZoneInfo _timeZone;
        private readonly string _filePollingBaseDirectory;

        /// <summary>
        /// Initializes a new instance of the PurchaseOrderPollingService
        /// </summary>
        /// <param name="logger">Logger for tracking service operations</param>
        /// <param name="serviceProvider">Service provider for dependency injection</param>
        /// <param name="filePollingService">Service for file operations</param>
        /// <param name="config">Application configuration</param>
        public PurchaseOrderPollingService(ILogger<PurchaseOrderPollingService> logger, IServiceProvider serviceProvider, IFilePollingService filePollingService, IConfiguration config)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _filePollingService = filePollingService;

            // Configure scheduling - defaults to midnight daily if not specified
            var cronSchedule = config["CronSchedule"] ?? "0 0 * * *";
            _scheduler = CronExpression.Parse(cronSchedule);
            _timeZone = TimeZoneInfo.Local;

            // Set base directory for file operations - defaults to current directory
            _filePollingBaseDirectory = config["FilePolling_BasePath"] ?? ".";
        }

        /// <summary>
        /// Main execution loop for the background service
        /// </summary>
        /// <param name="stoppingToken">Cancellation token to stop the service</param>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Purchase Order Polling Service is starting.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    // Calculate next execution time based on cron schedule
                    DateTimeOffset? nextExecutionTime = _scheduler.GetNextOccurrence(DateTimeOffset.Now, _timeZone);

                    if (nextExecutionTime.HasValue)
                    {
                        TimeSpan delayDuration = nextExecutionTime.Value - DateTimeOffset.Now;
                        _logger.LogInformation($"Next execution in {delayDuration.TotalMinutes:F1} minutes at {nextExecutionTime.Value}.");

                        // Wait until the next scheduled execution time
                        await Task.Delay(delayDuration, stoppingToken);
                    }

                    // Process any available files
                    await ProcessPurchaseOrderFilesAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unexpected error occurred during file processing.");
                }
            }

            _logger.LogInformation("Purchase Order Polling Service is stopping.");
        }

        /// <summary>
        /// Processes all purchase order files in the polling directory
        /// </summary>
        /// <param name="cancellationToken">Cancellation token for the operation</param>
        private async Task ProcessPurchaseOrderFilesAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting purchase order file processing...");

            // Get list of files to process
            IEnumerable<string> filesToProcess = await _filePollingService.ListFilesAsync(_filePollingBaseDirectory);

            if (!filesToProcess.Any())
            {
                _logger.LogInformation("No files found for processing.");
                return;
            }

            _logger.LogInformation($"Found {filesToProcess.Count()} files to process.");

            foreach (string filePath in filesToProcess)
            {
                try
                {
                    _logger.LogDebug($"Processing file: {filePath}");

                    // Read file content
                    string fileContent = await _filePollingService.ReadFileAsync(filePath);

                    // Deserialize XML content to PurchaseOrderDto
                    var xmlSerializer = new XmlSerializer(typeof(PurchaseOrderDto));
                    using var contentReader = new StringReader(fileContent);
                    var purchaseOrder = (PurchaseOrderDto)xmlSerializer.Deserialize(contentReader);

                    // Create service scope for this operation
                    using var serviceScope = _serviceProvider.CreateScope();
                    var purchaseOrderService = serviceScope.ServiceProvider.GetRequiredService<PurchaseOrdersService>();

                    // Attempt to create the purchase order
                    var creationResult = await purchaseOrderService.CreatePurchaseOrderAsync(purchaseOrder);

                    if (creationResult.IsSuccessStatusCode())
                    {
                        // Successful processing
                        _logger.LogInformation($"Successfully processed purchase order from file: {filePath}");

                        // Move to processed directory
                        string processedPath = Path.Combine(_filePollingBaseDirectory, "processed", Path.GetFileName(filePath));
                        await _filePollingService.MoveFileAsync(filePath, processedPath);
                    }
                    else
                    {
                        // Validation failed
                        string errorMessage = $"Validation failed - {creationResult.Message}";
                        _logger.LogWarning($"{errorMessage} in file: {filePath}");

                        // Write error log
                        string logFileName = Path.GetFileNameWithoutExtension(filePath) + ".log";
                        await _filePollingService.WriteLogAsync(_filePollingBaseDirectory, logFileName, errorMessage);

                        // Move to failed directory
                        string failedPath = Path.Combine(_filePollingBaseDirectory, "failed", Path.GetFileName(filePath));
                        await _filePollingService.MoveFileAsync(filePath, failedPath);
                    }
                }
                catch (Exception ex)
                {
                    // Error during processing
                    string errorMessage = $"Processing error: {ex.Message}";
                    _logger.LogError(ex, $"Failed to process file: {filePath}");

                    // Write error log
                    string logFileName = Path.GetFileNameWithoutExtension(filePath) + ".log";
                    await _filePollingService.WriteLogAsync(_filePollingBaseDirectory, logFileName, errorMessage);

                    // Move to failed directory
                    string failedPath = Path.Combine(_filePollingBaseDirectory, "failed", Path.GetFileName(filePath));
                    await _filePollingService.MoveFileAsync(filePath, failedPath);
                }
            }

            _logger.LogInformation("Completed purchase order file processing.");
        }
    }
}