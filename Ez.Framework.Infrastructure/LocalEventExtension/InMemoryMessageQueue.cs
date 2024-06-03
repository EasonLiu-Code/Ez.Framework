
using System.Threading.Channels;

namespace Ez.Infrastructure.LocalEventExtension;

/// <summary></summary>
internal sealed class InMemoryMessageQueue
{
    // 生产者平均每秒生成10条消息。
    // 消费者平均每秒处理5条消息。
    // 消息处理时间平均为100毫秒。
    // 根据这些假设，我们可以计算出所需的缓冲区大小：
    // 生产者每秒产生10条消息，因此平均每秒需要有10个空位来存储这些消息。
    // 消费者每秒处理5条消息，因此平均每秒需要释放5个空位。
    // 消息处理时间为100毫秒，因此每条消息在队列中停留的时间为100毫秒。
    // 根据这些信息，我们可以计算出所需的缓冲区大小：
    // 缓冲区大小 = 生产者速度 × 消息停留时间
    // 缓冲区大小 = 10（每秒产生的消息数） × 0.1（每条消息停留的时间，以秒为单位）
    // private readonly Channel<IIntegrationEvent> _channel;
    // public InMemoryMessageQueue(int capacity=5)
    // {
    //     _channel = Channel.CreateBounded<IIntegrationEvent>(new BoundedChannelOptions(capacity)
    //     {
    //         FullMode = BoundedChannelFullMode.Wait
    //     });
    // }
    private readonly Channel<IIntegrationEvent> _channel = Channel.CreateUnbounded<IIntegrationEvent>();
    /// <summary></summary>
    public ChannelWriter<IIntegrationEvent> Writer => _channel.Writer;
    /// <summary></summary>
    public ChannelReader<IIntegrationEvent> Reader => _channel.Reader;
}