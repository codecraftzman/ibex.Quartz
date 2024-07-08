using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Quartz.Services.ImageService.Domain.Entities;
using Quartz.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quartz.Services.ImageService.Application.Features.Images.Queries.GetImageList
{
    public class GetImageListQueryHandler: IRequestHandler<GetImageListQuery, List<ImageListVm>>
    {
        private readonly IAsyncRepository<Image> _respository;
        private readonly IMapper _mapper;
        public GetImageListQueryHandler(IMapper mapper, IAsyncRepository<Image> respository)
        {
            _mapper = mapper;
            _respository = respository;
        }

        public async Task<List<ImageListVm>> Handle(GetImageListQuery request, CancellationToken cancellationToken)
        {
            var images = await _respository.ListAllAsync();
            return _mapper.Map<List<ImageListVm>>(images).ToList();
        }
    }
}
