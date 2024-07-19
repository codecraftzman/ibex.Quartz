using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz.Shared.Contracts; // Ensure this using directive is correct based on your project structure
using System;

namespace Quartz.Services.ImageService.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Extract RabbitMQ configuration from appsettings.json
            var rabbitMQConfig = configuration.GetSection("RabbitMQ").Get<RabbitMQOptions>();

            // Check if the configuration is valid (not null)
            if (rabbitMQConfig == null || string.IsNullOrWhiteSpace(rabbitMQConfig.ConnectionString))
            {
                throw new InvalidOperationException("RabbitMQ configuration is missing or invalid.");
            }

            // Register MessageBusService with the extracted configuration
            services.AddSingleton<IMessageBusService>(provider =>
                new MessageBusService(rabbitMQConfig.ConnectionString));

            return services;
        }
    }

    // Define a class to bind the RabbitMQ configuration section
    public class RabbitMQOptions
    {
        public string ConnectionString { get; set; } = string.Empty;
    }
}
