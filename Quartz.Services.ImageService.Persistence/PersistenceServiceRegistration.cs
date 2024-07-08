using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Quartz.Services.ImageService.Application.Contracts.Persistence;
using Quartz.Services.ImageService.Persistence.Repositories;
using Quartz.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quartz.Services.ImageService.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContext<ImageDbContext>(options =>
            //    options..UseSqlServer(configuration.GetConnectionString("ImageConnectionString")));


            // Register App settings
            var mongoDbSettings = configuration.GetSection("DbSettings").Get<DbSettings>();
            services.AddSingleton(mongoDbSettings);

            // Register MongoDB client
            services.AddSingleton<IMongoClient>(sp =>
            {
                var settings = sp.GetRequiredService<DbSettings>();
                return new MongoClient(settings.ConnectionString);
                //return new MongoClient(configuration.GetConnectionString("mongodb://mongodb:27017"));
            });


            // Register MongoDB database
            services.AddScoped(sp =>
            {
                var client = sp.GetRequiredService<IMongoClient>();
                var settings = sp.GetRequiredService<DbSettings>();
                return client.GetDatabase(settings.DatabaseName);
            });

            // Register MongoDB repository
            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

            services.AddScoped<IImageRepository, ImageRepository>();

            return services;
        }
    }
}
