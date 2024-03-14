using Ez.Domain.CommonDtos;
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
            await bus.Publish<TestMessage>(
                new TestMessage
                {
                   DateTime = DateTime.Now
                }, stoppingToken);
            Console.WriteLine("Current Publisher is Succeed!!!!!!!!!!!!!!!");
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }
}