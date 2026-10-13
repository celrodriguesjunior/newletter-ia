using Newsletter.Core.Services.Abstraction;

namespace Newsletter.Api.Worker;

public class NewsletterWorker(ILogger<NewsletterWorker> logger, IServiceScopeFactory serviceScopeFactory) : BackgroundService
{
    private readonly TimeSpan _scheduleTime = new TimeSpan(8, 0, 0); // 8:00 AM

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("NewsletterWorker is running.");
        while (!stoppingToken.IsCancellationRequested)
        {
            var now = DateTime.Now;
            var nextRunTime = GetNextSummerAtEight(now);
            var delay = nextRunTime - now;
            logger.LogInformation($"Next newsletter will be sent at: {nextRunTime}");
            try
            {
                logger.LogInformation("Starting newsletter sending process...");
                await DoWorkAsync(stoppingToken);
            }
            catch (OperationCanceledException)
            {
                break;
            }

            await Task.Delay(delay, stoppingToken);
        }
    }

    private DateTime GetNextSummerAtEight(DateTime current)
    {
        var daysUntilNextSunday = (((int)DayOfWeek.Sunday - (int)current.DayOfWeek + 7) % 7);
        var nextSunday = current.AddDays(daysUntilNextSunday).Add(_scheduleTime);

        if (nextSunday <= current)
        {
            nextSunday = nextSunday.AddDays(7);
        }
        return nextSunday;
    }

    private async Task DoWorkAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("NewsletterWorker is starting.");
        using var scope = serviceScopeFactory.CreateScope();
        var newsletterService = scope.ServiceProvider.GetRequiredService<INewsletterServices>();
        await newsletterService.SendAsync(stoppingToken);
    }

}
