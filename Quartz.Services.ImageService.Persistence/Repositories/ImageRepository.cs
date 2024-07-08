using MongoDB.Driver;
using Quartz.Services.ImageService.Application.Contracts.Persistence;
using Quartz.Services.ImageService.Domain.Entities;
using Quartz.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quartz.Services.ImageService.Persistence.Repositories
{
    public class ImageRepository : BaseRepository<Image>, IImageRepository 
    {
        public ImageRepository(IMongoDatabase database) :base(database, "Image")
        {
        }
    }
}
