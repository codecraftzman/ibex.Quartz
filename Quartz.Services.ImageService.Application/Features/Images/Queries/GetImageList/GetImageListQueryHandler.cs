using Amazon.Runtime.Internal.Util;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Quartz.Services.ImageService.Application.Contracts.Persistence;
using Quartz.Services.ImageService.Domain.Entities;
using Quartz.Shared.Caching;
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
        private readonly IGalleryRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        private readonly ILogger<GetImageListQueryHandler> _logger;

        public GetImageListQueryHandler(IMapper mapper, IGalleryRepository repository, ICacheService cacheService, ILogger<GetImageListQueryHandler> logger)
        {
            _mapper = mapper;
            _repository = repository;
            _cacheService = cacheService;
            _logger = logger;

        }

        public async Task<List<ImageListVm>> Handle(GetImageListQuery request, CancellationToken cancellationToken)
        {
            //var images = await _repository.ListAllAsync();
            //return _mapper.Map<List<ImageListVm>>(images).ToList();
            const string cacheKey = "imageList";

            // Try to get the data from the cache
            var cachedImages = await _cacheService.GetAsync<List<ImageListVm>>(cacheKey);
            if (cachedImages != null)
            {
                _logger.LogInformation("Returning cached image list.");
                return cachedImages;
            }

            // If not in cache, get the data from the repository
            var images = await _repository.ListAllAsync();
            var imageList = _mapper.Map<List<ImageListVm>>(images).ToList();

            // Store the data in the cache
            await _cacheService.SetAsync(cacheKey, imageList, TimeSpan.FromMinutes(10));

            return imageList;
        }
    }
}
