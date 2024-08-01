using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quartz.Shared.Contracts.Entities
{
    public interface IEntity<T> where T : struct
    {
        T Id { get; set; }
    }
}
