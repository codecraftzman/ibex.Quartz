using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quartz.Services.ImageService.Infrastructure.Messaging
{
    public class NewImageCreated : IMessage
    {
        public string ImageId { get; set; } = null!;
        public string Name { get; set; } = default!;
        public DateTime Timestamp => DateTime.Now;
    }
}
