using MongoDB.Bson;
using Quartz.Shared.Database.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quartz.Shared.Database.Entities
{

    public class QuartzEntity : IAuditableEntity
    {
        public ObjectId Id { get; set; } = default!;
        public string CreatedBy { get; set; } = default!;
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; } = default!;
        public DateTime? LastModifiedOn { get; set; }

        public QuartzEntity()
        {
            if (Id == ObjectId.Empty)
            {
                //Id = ObjectId.GenerateNewId();
                CreatedOn = DateTime.UtcNow;
                LastModifiedOn = DateTime.UtcNow;
            }

        }
    }
}
