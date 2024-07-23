using Amazon.SecurityToken;
using AutoMapper;
using Quartz.Services.ImageService.Application.Features.Images.Commands.CreateNewImage;
using Quartz.Services.ImageService.Application.Features.Images.Queries.GetImageList;
using Quartz.Services.ImageService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quartz.Services.ImageService.Application.Profiles
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Image, ImageListVm>().ReverseMap();
            CreateMap<Image, CreateImageCommand>().ReverseMap();
        }
    }
}
