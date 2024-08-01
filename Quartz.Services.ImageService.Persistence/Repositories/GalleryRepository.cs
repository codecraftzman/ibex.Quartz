using MongoDB.Driver;
using Quartz.Services.ImageService.Application.Contracts.Persistence;
using Quartz.Services.ImageService.Domain.Entities;
using Quartz.Shared.Contracts;
using Quartz.Shared.Integration.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quartz.Services.ImageService.Persistence.Repositories
{
    public class GalleryRepository : BaseRepository<Image>, IGalleryRepository 
    {
        public GalleryRepository(IMongoDatabase database) : base(database, "images")
        {
        }

        //public GalleryRepository(ImageDbContext context): base(context)
        //{

        //}
    }
}
