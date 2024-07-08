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
        public string Title { get; set; } = string.Empty;

        public string FileName { get; set; } = string.Empty;

        public string OwnerId { get; set; } = string.Empty;
    }
}
