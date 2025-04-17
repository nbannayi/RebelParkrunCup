using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Logging;

namespace RebelParkrunCup.Server.Services
{
    public class GcsUploader
    {
        private readonly StorageClient _storageClient;
        private readonly ILogger<GcsUploader> _logger;
        private const string BucketName = "parkrun-cup-db";
        private const string FileName = "parkrun.db";
        private readonly bool _isRunningOnCloudRun;

        public GcsUploader(ILogger<GcsUploader> logger)
        {
            _logger = logger;
            _storageClient = StorageClient.Create();
            _isRunningOnCloudRun = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("K_SERVICE"));
        }

        public async Task UploadDbAsync()
        {
            if (!_isRunningOnCloudRun)
            {
                _logger.LogInformation("Running locally — skipping GCS upload.");
                return;
            }

            var filePath = Path.Combine(AppContext.BaseDirectory, FileName);
            using var fileStream = File.OpenRead(filePath);
            await _storageClient.UploadObjectAsync(BucketName, FileName, null, fileStream);
            _logger.LogInformation("Uploaded parkrun.db to GCS.");
        }
    }
}
