using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quartz.Shared.Integration.Events
{
    public class RabbitMQConfig
    {
        public string ConnectionString { get; set; } = default!;
    }
}
