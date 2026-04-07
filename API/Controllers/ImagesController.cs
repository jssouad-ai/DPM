using Application.DTOs;
using Application.Images.Commands;
using Application.Images.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        
        private readonly IMediator _mediator;

        public ImagesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetImageListQuery());
            return Ok(result);
        }

       [HttpGet("{id}")]
        public async Task<IActionResult> GetDetail(string id)
        {
            var result = await _mediator.Send(new GetImageDetailQuery(id));

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateImageCommand command)
        {
            await _mediator.Send(command);
            return Ok(new
            {
                Message = "Image created successfully"
            });
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateImageCommand command)
        {
            await _mediator.Send(command);
            return Ok(new
            {
                Message = "Image updated successfully"
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _mediator.Send(new DeleteImageCommand(id));
            return Ok(new
            {
                Message = "Image Deleted successfully"
            });
        }

    }

}

