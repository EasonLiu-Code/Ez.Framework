
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Ez.Infrastructure.LocalEventExtension;

/// <summary></summary>
internal sealed class InMemoryMessageQueue
{
    private const string MessageQueueKey = "message_queue";
    private readonly IDatabase _database;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="connectionMultiplexer"></param>
    public InMemoryMessageQueue(IConnectionMultiplexer connectionMultiplexer)
    {
        _database = connectionMultiplexer.GetDatabase();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <typeparam name="T"></typeparam>
    public async Task EnqueueAsync<T>(T message)
    {
        var serializedMessage = JsonConvert.SerializeObject(message);
        await _database.ListRightPushAsync(MessageQueueKey, serializedMessage);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public async Task<T> DequeueAsync<T>()
    {
        var serializedMessage = await _database.ListLeftPopAsync(MessageQueueKey);
        return JsonConvert.DeserializeObject<T>(serializedMessage);
    }
}