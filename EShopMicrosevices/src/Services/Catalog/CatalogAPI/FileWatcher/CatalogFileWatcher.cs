namespace CatalogAPI.FileWatcher;

public sealed class CatalogFileWatcher : BackgroundService
{
    private readonly ILogger<CatalogFileWatcher> _logger;
    private readonly string _watchPath;
    private FileSystemWatcher? _watcher;

    public CatalogFileWatcher(ILogger<CatalogFileWatcher> logger, IConfiguration configuration)
    {
        _logger = logger;
        _watchPath = configuration["FileWatcher:CatalogPath"]
                     ?? Path.Combine(AppContext.BaseDirectory, "data", "imports");
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Directory.CreateDirectory(_watchPath);

        _watcher = new FileSystemWatcher(_watchPath)
        {
            NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.Size,
            Filter = "*.json",
            EnableRaisingEvents = true,
            IncludeSubdirectories = false
        };

        _watcher.Created += OnFileChanged;
        _watcher.Changed += OnFileChanged;
        _watcher.Deleted += OnFileDeleted;
        _watcher.Error += OnWatcherError;

        _logger.LogInformation("CatalogFileWatcher started. Watching: {Path}", _watchPath);

        stoppingToken.Register(Dispose);

        return Task.CompletedTask;
    }

    private void OnFileChanged(object sender, FileSystemEventArgs e)
    {
        _logger.LogInformation("Catalog import file detected: {ChangeType} – {FilePath}", e.ChangeType, e.FullPath);
    }

    private void OnFileDeleted(object sender, FileSystemEventArgs e)
    {
        _logger.LogInformation("Catalog import file deleted: {FilePath}", e.FullPath);
    }

    private void OnWatcherError(object sender, ErrorEventArgs e)
    {
        _logger.LogError(e.GetException(), "CatalogFileWatcher encountered an error");
    }

    public override void Dispose()
    {
        _watcher?.Dispose();
        base.Dispose();
    }
}
