using Microsoft.Extensions.Configuration;
using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;
using Quartz.Shared; // Ensure this using directive is correct based on your project structure
using System;
using EasyNetQ.DI;
using Quartz.Shared.Messaging;

namespace Quartz.Services.ImageService.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Extract RabbitMQ configuration from appsettings.json
            var rabbitMQConfig = configuration.GetSection("RabbitMQ").Get<RabbitMQConfig>();

            // Check if the configuration is valid (not null)
            if (rabbitMQConfig == null || string.IsNullOrWhiteSpace(rabbitMQConfig.ConnectionString))
            {
                throw new InvalidOperationException("RabbitMQ configuration is missing or invalid.");
            }

            // Register MessageBusService with the extracted configuration
            services.AddSingleton<IBus>(_ => RabbitHutch.CreateBus(rabbitMQConfig.ConnectionString));

            return services;
        }
    }

    
}
