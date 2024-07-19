using Amazon.Runtime.Internal;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quartz.Services.ImageService.Application.Features.Images.Commands.CreateNewImage
{
    public class CreateImageCommand : MediatR.IRequest
    {
        public string Title { get; set; } = string.Empty;

        public string FileName { get; set; } = string.Empty;

        public string OwnerId { get; set; } = string.Empty;
    }
}
