using MongoDB.Bson;
using Quartz.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quartz.Services.ImageService.Domain.Entities
{
    public class Image : QuartzEntity
    {
        public string Title { get; set; } = default!;

        public string FileName { get; set; } = default!;

        public string OwnerId { get; set; } = default!;
    }
}
