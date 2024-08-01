using Quartz.Shared.Contracts.Events;

namespace Quartz.Shared.Integration.Events.Image;
public class ImageCreatedEvent : IQuartzIntegrationEvent
{
    public string ImageId { get; set; } = null!;
    public string Name { get; set; } = default!;
    public DateTime Timestamp => DateTime.UtcNow;
}