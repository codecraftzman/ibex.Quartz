namespace Quartz.Shared.Contracts;
public class ImageCreatedEvent_old : IQuartzMessage
{
    public string ImageId { get; set; } = null!;
    public string Name { get; set; } = default!;
    public DateTime Timestamp => DateTime.UtcNow;
}