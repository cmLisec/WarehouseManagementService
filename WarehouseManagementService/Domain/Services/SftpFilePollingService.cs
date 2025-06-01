using Renci.SshNet;
using Renci.SshNet.Sftp;
using System.Text;
using WarehouseManagementService.Domain.Interfaces;

namespace WarehouseManagementService.Domain.Services
{

    public class SftpFilePollingService : IFilePollingService
    {
        private readonly SftpClient _sftpClient;

        public SftpFilePollingService(IConfiguration config)
        {
            var host = config["SFTP_HOST"];
            var port = int.Parse(config["SFTP_PORT"]);
            var username = config["SFTP_USERNAME"];
            var password = config["SFTP_PASSWORD"];

            _sftpClient = new SftpClient(host, port, username, password);
            _sftpClient.Connect();
        }

        public Task<IEnumerable<string>> ListFilesAsync(string path, string searchPattern = "*.xml")
        {
            var files = _sftpClient.ListDirectory(path)
                .Where(f => !f.IsDirectory && f.Name.EndsWith(".xml"))
                .Select(f => Path.Combine(path, f.Name));

            return Task.FromResult(files);
        }

        public async Task<string> ReadFileAsync(string path)
        {
            using var ms = new MemoryStream();
            _sftpClient.DownloadFile(path, ms);
            return Encoding.UTF8.GetString(ms.ToArray());
        }

        public Task MoveFileAsync(string sourcePath, string destinationPath)
        {
            if (_sftpClient.Exists(destinationPath))
                _sftpClient.DeleteFile(destinationPath);

            _sftpClient.RenameFile(sourcePath, destinationPath);
            return Task.CompletedTask;
        }

        public Task WriteLogAsync(string basePath, string fileName, string message)
        {
            var logsPath = Path.Combine(basePath, "logs");
            if (!_sftpClient.Exists(logsPath))
                _sftpClient.CreateDirectory(logsPath);

            var logFile = Path.Combine(logsPath, $"log_{DateTime.UtcNow:yyyy-MM-dd}.txt");
            var logEntry = $"[{DateTime.UtcNow:HH:mm:ss}] {fileName} - {message}{Environment.NewLine}";

            byte[] contentBytes = Encoding.UTF8.GetBytes(logEntry);

            using var ms = new MemoryStream(contentBytes);
            if (_sftpClient.Exists(logFile))
                _sftpClient.AppendAllText(logFile, logEntry);
            else
                _sftpClient.UploadFile(ms, logFile);

            return Task.CompletedTask;
        }
    }

}
