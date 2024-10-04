using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Quartz.Services.ImageService.Application.Contracts.Persistence;
using Quartz.Services.ImageService.Persistence.Configurations;
using Quartz.Services.ImageService.Persistence.Repositories;
using Quartz.Shared.Database.Contracts;
using Quartz.Shared.Database.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quartz.Services.ImageService.Persistence
{
    public static class PersistenceServiceRegistration
    {
        private static IServiceProvider? _serviceProvider = null;
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure DbContext
            services.AddScoped(sp =>
            {
                var client = sp.GetRequiredService<IMongoClient>();
                var settings = sp.GetRequiredService<IDbSettings>();
                return new ImageDbContext(client, settings);
            });
            
            // Register MongoDB repository
            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

            services.AddScoped<IGalleryRepository, GalleryRepository>();

            //Configuring class maps for domain 
            MongoDBConfigurations.RegisterClassMaps();

            return services;
        }
    }
}
