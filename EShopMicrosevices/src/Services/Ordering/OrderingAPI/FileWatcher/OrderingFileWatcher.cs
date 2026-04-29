namespace OrderingAPI.FileWatcher;

public sealed class OrderingFileWatcher : BackgroundService
{
    private readonly ILogger<OrderingFileWatcher> _logger;
    private readonly string _watchPath;
    private FileSystemWatcher? _watcher;

    public OrderingFileWatcher(ILogger<OrderingFileWatcher> logger, IConfiguration configuration)
    {
        _logger = logger;
        _watchPath = configuration["FileWatcher:OrderingPath"]
                     ?? Path.Combine(AppContext.BaseDirectory, "data", "orders");
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

        _logger.LogInformation("OrderingFileWatcher started. Watching: {Path}", _watchPath);

        stoppingToken.Register(Dispose);

        return Task.CompletedTask;
    }

    private void OnFileChanged(object sender, FileSystemEventArgs e)
    {
        _logger.LogInformation("Order data file detected: {ChangeType} – {FilePath}", e.ChangeType, e.FullPath);
    }

    private void OnFileDeleted(object sender, FileSystemEventArgs e)
    {
        _logger.LogInformation("Order data file deleted: {FilePath}", e.FullPath);
    }

    private void OnWatcherError(object sender, ErrorEventArgs e)
    {
        _logger.LogError(e.GetException(), "OrderingFileWatcher encountered an error");
    }

    public override void Dispose()
    {
        _watcher?.Dispose();
        base.Dispose();
    }
}
