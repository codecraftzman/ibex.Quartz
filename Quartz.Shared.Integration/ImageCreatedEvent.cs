namespace Quartz.Shared.Integration;
public class ImageCreatedEvent : IQuartzMessage
{
    public string ImageId { get; set; } = null!;
    public string Name { get; set; } = default!;
    public DateTime Timestamp => DateTime.UtcNow;
}