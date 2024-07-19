using AutoMapper;
using MediatR;
using Quartz.Services.ImageService.Application.Contracts.Persistence;
using Quartz.Services.ImageService.Domain.Entities;
using Quartz.Shared.Contracts;
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

        public CreateImageCommandHandler(IMapper mapper, IGalleryRepository imageRepository)
        {
            _mapper = mapper;
            _imageRepository = imageRepository;
        }

        
        //public async Task<Unit> Handle(CreateImageCommand request, CancellationToken cancellationToken)
        //{
        //    var validator = new CreateImageCommandValidator();
        //    var validationResult = await validator.ValidateAsync(request);

        //    if (validationResult.Errors.Count > 0)
        //    {

        //    }
        //    else
        //    {
        //        var image = new Image { FileName = request.FileName, Title = request.Title, OwnerId = request.OwnerId };
        //        await _imageRepository.AddAsync(image);
        //    }

        //    return Unit.Value;
        //}

        public async Task Handle(CreateImageCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateImageCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {

            }
            else
            {
                var image = new Image { FileName = request.FileName, Title = request.Title, OwnerId = request.OwnerId };
                await _imageRepository.AddAsync(image);
            }
        }
    }

}
