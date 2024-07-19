using EasyNetQ;
using System.Threading.Tasks;

public class MessageBusService : IMessageBusService
{
    private readonly IBus _bus;

    public MessageBusService(string connectionString)
    {
        _bus = RabbitHutch.CreateBus(connectionString);
    }

    public async Task PublishAsync<T>(T message, string topic) where T : IMessage
    {
        await _bus.PubSub.PublishAsync(message, topic);
    }

    public void Subscribe<T>(string subscriptionId, Action<T> onMessage, string topic) where T : class, IMessage
    {
        _bus.PubSub.Subscribe<T>(subscriptionId, onMessage, x=> x.WithTopic(topic));
    }
}
