using AutoMapper;
using EasyNetQ;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Quartz.Services.ImageService.Application.Contracts.Persistence;
using Quartz.Services.ImageService.Domain.Entities;
using Quartz.Shared.Messaging.Events;
using Quartz.Shared.Messaging.Events.Image;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quartz.Services.ImageService.Application.Features.Images.Commands.CreateNewImage
{
    public class CreateImageCommandHandler : IRequestHandler<CreateImageCommand>
    {
        private readonly IMapper _mapper;
        private readonly IGalleryRepository _imageRepository;
        private readonly IBus _bus;
        public CreateImageCommandHandler(IMapper mapper, IGalleryRepository imageRepository, IBus bus)
        {
            _mapper = mapper;
            _imageRepository = imageRepository;
            _bus = bus;
        }


        public async Task Handle(CreateImageCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateImageCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                // Handle validation errors
                throw new ValidationException(validationResult.Errors);
            }
            else
            {
                var image = _mapper.Map<Image>(request); // Assuming AutoMapper setup is correct and maps CreateImageCommand to Image entity
                await _imageRepository.AddAsync(image);

                // Prepare the ImageCreatedEvent
                var imageCreatedEvent = new ImageCreatedEvent
                {
                    ImageId = image.Id.ToString(), // Assuming Id is a property of Image and is set by the repository upon saving
                    Name = image.Title, // Assuming you want to use the Title as the Name in the event
                                        // Timestamp is automatically set to DateTime.Now in the ImageCreatedEvent class
                };

                // Publish the event to a specific topic
                //await _busService.PublishAsync<ImageCreatedEvent>(imageCreatedEvent, "image.local");
                //var rabbitMQConfig = _configuration.GetSection("RabbitMQ").Get<RabbitMQConfig>();
                //using (var bus = RabbitHutch.CreateBus(rabbitMQConfig?.ConnectionString))
                //{
                //    //await bus.PubSub.PublishAsync(imageCreatedEvent, cfg=> cfg.WithTopic("image.local"));
                //    await bus.PubSub.PublishAsync(imageCreatedEvent);
                //}

                await _bus.PubSub.PublishAsync<IQuartzIntegrationEvent>(imageCreatedEvent, cfg => cfg.WithTopic("image.local"));
            }
        }
    }

}
