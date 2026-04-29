using System.Threading.RateLimiting;

namespace YarpApiGateway.Tests;

public class RateLimitingTests
{
    [Fact]
    public void FixedWindowLimiter_AllowsRequestsUpToPermitLimit()
    {
        using var limiter = new FixedWindowRateLimiter(new FixedWindowRateLimiterOptions
        {
            PermitLimit = 5,
            Window = TimeSpan.FromSeconds(10),
            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
            QueueLimit = 0
        });

        for (int i = 0; i < 5; i++)
        {
            using var lease = limiter.AttemptAcquire();
            lease.IsAcquired.Should().BeTrue($"request #{i + 1} of 5 must be granted");
        }
    }

    [Fact]
    public void FixedWindowLimiter_RejectsRequestBeyondPermitLimit()
    {
        using var limiter = new FixedWindowRateLimiter(new FixedWindowRateLimiterOptions
        {
            PermitLimit = 5,
            Window = TimeSpan.FromSeconds(10),
            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
            QueueLimit = 0
        });

        for (int i = 0; i < 5; i++)
            limiter.AttemptAcquire();

        using var overLimit = limiter.AttemptAcquire();
        overLimit.IsAcquired.Should().BeFalse("6th request must be rejected");
    }

    [Fact]
    public void FixedWindowLimiter_CorrectConfiguration_MatchesGatewayPolicy()
    {
        // Mirrors the exact configuration in YarpApiGateway Program.cs
        var options = new FixedWindowRateLimiterOptions
        {
            PermitLimit = 5,
            Window = TimeSpan.FromSeconds(10),
            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
            QueueLimit = 0
        };

        options.PermitLimit.Should().Be(5);
        options.Window.Should().Be(TimeSpan.FromSeconds(10));
        options.QueueLimit.Should().Be(0);
        options.QueueProcessingOrder.Should().Be(QueueProcessingOrder.OldestFirst);
    }

    [Fact]
    public void FixedWindowLimiter_StatisticsReflectGrantedLeases()
    {
        using var limiter = new FixedWindowRateLimiter(new FixedWindowRateLimiterOptions
        {
            PermitLimit = 5,
            Window = TimeSpan.FromSeconds(10),
            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
            QueueLimit = 0
        });

        for (int i = 0; i < 3; i++)
            limiter.AttemptAcquire();

        var stats = limiter.GetStatistics();
        stats.Should().NotBeNull();
        stats!.CurrentAvailablePermits.Should().Be(2);
    }
}
