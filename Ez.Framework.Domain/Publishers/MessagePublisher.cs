using Ez.Domain.CommonDtos;
using Ez.Domain.CommonDtos.Events;
using MassTransit;
using Microsoft.Extensions.Hosting;

namespace Ez.Domain.Publishers;

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
            await bus.Publish<TestMessageEvent>(
                new 
                {
                   DateTime = DateTime.Now
                }, stoppingToken);
            Console.WriteLine("Current Publisher is Succeed!!!!!!!!!!!!!!!");
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }
}