using WarehouseManagementService.Domain.Interfaces;

namespace WarehouseManagementService.Domain.Services;
public class LocalFilePollingService : IFilePollingService
{
    public Task<IEnumerable<string>> ListFilesAsync(string path, string searchPattern = "*.xml")
    {
        var files = Directory.GetFiles(path, searchPattern);
        return Task.FromResult(files.AsEnumerable());
    }

    public Task<string> ReadFileAsync(string path)
    {
        return File.ReadAllTextAsync(path);
    }

    public Task MoveFileAsync(string sourcePath, string destinationPath)
    {
        if (File.Exists(destinationPath))
            File.Delete(destinationPath);

        File.Move(sourcePath, destinationPath);
        return Task.CompletedTask;
    }

    public Task WriteLogAsync(string basePath, string fileName, string message)
    {
        var logsDir = Path.Combine(basePath, "logs");
        Directory.CreateDirectory(logsDir);

        var logFile = Path.Combine(logsDir, $"log_{DateTime.UtcNow:yyyy-MM-dd}.txt");
        var logEntry = $"[{DateTime.UtcNow:HH:mm:ss}] {fileName} - {message}{Environment.NewLine}";

        return File.AppendAllTextAsync(logFile, logEntry);
    }
}

