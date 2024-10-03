namespace Quartz.Shared.Messaging.Events
{
    public interface IQuartzIntegrationEvent
    {
        DateTime Timestamp { get; }
    }
}
