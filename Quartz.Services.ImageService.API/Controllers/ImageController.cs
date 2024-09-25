using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Quartz.Services.ImageService.Application.Features.Images.Commands.CreateNewImage;
using Quartz.Services.ImageService.Application.Features.Images.Queries.GetImageList;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Quartz.Services.ImageService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ImageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<ImageController>
        [HttpGet(Name = "GetAllImages")]
        public async Task<ActionResult<List<ImageListVm>>> GetAllImages()
        {
            var result =  await _mediator.Send(new GetImageListQuery());
            return Ok(result);
           //return Ok(new List<ImageListVm>());

        }
        // POST: api/<ImageController>
        [HttpPost(Name = "CreateImage")]
        public async Task<ActionResult> CreateImage([FromBody] CreateImageCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        // POST: api/<ImageController>
        [HttpPut(Name = "UpdateImage")]
        public async Task<ActionResult> UpdateImage(string val)
        {
            throw new ArgumentException("Not implemented for the given argument");
        }



    }
}
