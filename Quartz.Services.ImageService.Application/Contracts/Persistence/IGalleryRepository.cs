using Quartz.Services.ImageService.Domain.Entities;
using Quartz.Shared.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quartz.Services.ImageService.Application.Contracts.Persistence
{
    public interface IGalleryRepository : IAsyncRepository<Image> 
    {
    }
}
