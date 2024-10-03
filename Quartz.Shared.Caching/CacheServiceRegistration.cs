using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quartz.Shared.Caching
{
    public static class CacheServiceRegistration
    {
        public static IServiceCollection AddCachingServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("Redis");
                options.InstanceName = "Quartz";
            });

            services.AddSingleton<ICacheService, RedisCacheService>();

            return services;
        }
    }


}
