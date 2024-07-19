public interface IMessageBusService
{
    Task PublishAsync<T>(T message, string topic) where T : IMessage;
    void Subscribe<T>(string subscriptionId, Action<T> onMessage, string topic) where T : class, IMessage;
}
