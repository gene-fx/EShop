namespace BasketAPI.FileWatcher;

public sealed class BasketFileWatcher : BackgroundService
{
    private readonly ILogger<BasketFileWatcher> _logger;
    private readonly string _watchPath;
    private FileSystemWatcher? _watcher;

    public BasketFileWatcher(ILogger<BasketFileWatcher> logger, IConfiguration configuration)
    {
        _logger = logger;
        _watchPath = configuration["FileWatcher:BasketPath"]
                     ?? Path.Combine(AppContext.BaseDirectory, "data", "exports");
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

        _logger.LogInformation("BasketFileWatcher started. Watching: {Path}", _watchPath);

        stoppingToken.Register(Dispose);

        return Task.CompletedTask;
    }

    private void OnFileChanged(object sender, FileSystemEventArgs e)
    {
        _logger.LogInformation("Basket export file detected: {ChangeType} – {FilePath}", e.ChangeType, e.FullPath);
    }

    private void OnFileDeleted(object sender, FileSystemEventArgs e)
    {
        _logger.LogInformation("Basket export file deleted: {FilePath}", e.FullPath);
    }

    private void OnWatcherError(object sender, ErrorEventArgs e)
    {
        _logger.LogError(e.GetException(), "BasketFileWatcher encountered an error");
    }

    public override void Dispose()
    {
        _watcher?.Dispose();
        base.Dispose();
    }
}
