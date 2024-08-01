using Quartz.Shared.Contracts;

namespace Quartz.Shared.Contracts.Events
{
    public interface IQuartzIntegrationEvent
    {
        DateTime Timestamp { get; }
    }
}
