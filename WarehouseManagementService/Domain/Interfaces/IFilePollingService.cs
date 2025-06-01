namespace WarehouseManagementService.Domain.Interfaces
{
    public interface IFilePollingService
    {
        Task<IEnumerable<string>> ListFilesAsync(string path, string searchPattern = "*.xml");
        Task<string> ReadFileAsync(string path);
        Task MoveFileAsync(string sourcePath, string destinationPath);
        Task WriteLogAsync(string basePath, string fileName, string message);
    }
}
