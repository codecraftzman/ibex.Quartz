using EasyNetQ;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz.Shared.Messaging.Events;
using Quartz.Shared.Messaging.Events.Image;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Quartz.Services.ImageListener.Worker
{
   

    public class ImageConsumerService : IHostedService
    {
        private readonly ILogger<ImageConsumerService> _logger;
        private readonly IBus _bus;
        //private readonly IConfiguration _configuration;
        //private readonly IMessageBusService _messageBusService;

        public ImageConsumerService(ILogger<ImageConsumerService> logger, IBus bus)
        {
            _logger = logger;
            _bus = bus;
            
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Image Consumer Service is starting.");

            _bus.PubSub.SubscribeAsync<IQuartzIntegrationEvent>("imageConsumer", HandleImageCreatedEvent, cfg => cfg.WithTopic("image.local"));
            return Task.CompletedTask;
        }

        private void HandleImageCreatedEvent(IQuartzIntegrationEvent imageCreatedEvent)
        {
            // Process the incoming message
            if (imageCreatedEvent is ImageCreatedEvent createdEvent)
            {
                _logger.LogInformation($"Received image created event: {createdEvent.Name}, ID: {createdEvent.ImageId}");
            }
            //_logger.LogInformation($"Received image created event: {message.Text}");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Image Consumer Service is stopping.");

            // Perform any cleanup if necessary
            return Task.CompletedTask;
        }
    }

}
