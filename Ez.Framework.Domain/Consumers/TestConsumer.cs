using Ez.Domain.CommonDtos;
using MassTransit;

namespace Ez.Domain.Consumers;

/// <summary>
/// Consumer
/// </summary>
public class TestConsumer:IConsumer,IConsumer<TestMessage>
{
    /// <summary>
    /// Consume
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task Consume(ConsumeContext<TestMessage> context)
    {
       Console.WriteLine("Current Consumer is succeed，"+context.Message.DateTime);
    }
}