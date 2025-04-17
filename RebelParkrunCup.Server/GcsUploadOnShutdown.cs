using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RebelParkrunCup.Server.Services;

public class GcsUploadOnShutdown : IHostedService
{
    private readonly ILogger<GcsUploadOnShutdown> _logger;
    private readonly GcsUploader _uploader;
    private readonly IHostApplicationLifetime _appLifetime;

    public GcsUploadOnShutdown(ILogger<GcsUploadOnShutdown> logger, GcsUploader uploader, IHostApplicationLifetime appLifetime)
    {
        _logger = logger;
        _uploader = uploader;
        _appLifetime = appLifetime;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _appLifetime.ApplicationStopping.Register(OnShutdown);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    private void OnShutdown()
    {
        _logger.LogInformation("Application shutting down — uploading DB...");
        try
        {
            _uploader.UploadDbAsync().GetAwaiter().GetResult(); // blocking here is okay for shutdown
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to upload DB during shutdown.");
        }
    }
}
