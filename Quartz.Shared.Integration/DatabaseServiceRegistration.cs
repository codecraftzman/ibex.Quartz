using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Quartz.Shared.Database.Contracts;
using Quartz.Shared.Database.Repositories;



//using Quartz.Services.ImageService.Application.Contracts.Persistence;
//using Quartz.Services.ImageService.Persistence.Configurations;
//using Quartz.Services.ImageService.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quartz.Shared.Database
{
    public static class DatabaseServiceRegistration
    {
        private static IServiceProvider? _serviceProvider = null;
        public static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register App settings
            var mongoDbSettings = configuration.GetSection("DbSettings").Get<QuartzDbSettings>();

            // Check if the configuration is valid (not null)
            if (mongoDbSettings == null || string.IsNullOrWhiteSpace(mongoDbSettings.ConnectionString))
            {
                throw new InvalidOperationException("MongoDB configuration is missing or invalid.");
            }

            if (mongoDbSettings != null)
                services.AddSingleton<IDbSettings>(mongoDbSettings);

            // Register MongoDB client
            services.AddSingleton<IMongoClient>(sp =>
            {
                _serviceProvider = sp;
                var settings = sp.GetRequiredService<IDbSettings>();
                return new MongoClient(settings.ConnectionString);
                //return new MongoClient(configuration.GetConnectionString("mongodb://mongodb:27017"));
            });


            // Register MongoDB database
            services.AddScoped(sp =>
            {
                var client = sp.GetRequiredService<IMongoClient>();
                var settings = sp.GetRequiredService<IDbSettings>();
                return client.GetDatabase(settings.DatabaseName);
            });



            //// Configure DbContext
            //services.AddScoped(sp =>
            //{
            //    var client = sp.GetRequiredService<IMongoClient>();
            //    var settings = sp.GetRequiredService<IDbSettings>();
            //    return new ImageDbContext(client, settings);
            //});

            //// Register MongoDB repository
            //services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

            //services.AddScoped<IGalleryRepository, GalleryRepository>();

            ////Configuring class maps for domain 
            //MongoDBConfigurations.RegisterClassMaps();

            return services;
        }
    }
}
