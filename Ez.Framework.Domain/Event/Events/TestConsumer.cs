using MassTransit;

namespace Ez.Domain.Event.Events;

/// <summary>
/// Consumer
/// </summary>
public class TestConsumer:IConsumer,IConsumer<TestMessageEvent>
{
    /// <summary>
    /// Consume
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task Consume(ConsumeContext<TestMessageEvent> context)
    {
       Console.WriteLine("Current Consumer is succeed，"+context.Message.DateTime);
    }
}