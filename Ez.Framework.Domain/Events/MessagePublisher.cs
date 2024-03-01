using MassTransit;
using Microsoft.Extensions.Hosting;

namespace Ez.Domain.Events;

/// <summary>
/// MessagePublisher
/// </summary>
public class MessagePublisher(IBus bus):BackgroundService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="stoppingToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await bus.Publish(
                new
                {
                    Value=$"MessagePublisher  the current time is {DateTime.UtcNow}"
                }, stoppingToken);
            await Task.Delay(1000, stoppingToken);
        }
    }
}

/// <summary>
/// CurrentTime
/// </summary>
public record CurrentTime
{
    /// <summary></summary>
    public string Value
    {
        get;
        init;
    }=string.Empty;
}