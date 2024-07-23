using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quartz.Services.ImageListener.Worker
{
    using EasyNetQ;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Quartz.Shared.Contracts;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

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
            //_configuration = configuration;
            //_messageBusService = messageBusService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Image Consumer Service is starting.");

            // Subscribe to the "image.local" topic
            //_messageBusService.SubscribeAsync<ImageCreatedEvent>("imageConsumer", HandleImageCreatedEvent, "image.local");

            //var connectionString = _configuration.GetSection("RabbitMQ").Get<RabbitMQConfig>()?.ConnectionString;
            //using (var bus = RabbitHutch.CreateBus(connectionString))
            //{
            //    //bus.PubSub.SubscribeAsync<IMessage>("imageConsumer",  HandleImageCreatedEvent, cfg => cfg.WithTopic("image.local"));
            //    bus.PubSub.SubscribeAsync<Message>("imageConsumer", HandleImageCreatedEvent);
            //    Console.WriteLine("Listening for messages. Hit <return> to quit.");
            //    Console.ReadLine();
            //}


            _bus.PubSub.SubscribeAsync<IQuartzMessage>("imageConsumer", HandleImageCreatedEvent, cfg => cfg.WithTopic("image.local"));
            return Task.CompletedTask;
        }

        private void HandleImageCreatedEvent(IQuartzMessage imageCreatedEvent)
        {
            // Process the incoming message
            if (imageCreatedEvent is ImageCreatedEvent_old createdEvent)
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
