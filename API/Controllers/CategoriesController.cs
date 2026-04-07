using Application.Categories.Commands;
using Application.Categories.Queries;
using Application.DTOs;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetCategoryListQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetail(string id)
        {
            var result = await _mediator.Send(new GetCategoryDetailQuery(id));

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryCommand command)
        {
            await _mediator.Send(command);
           // return Ok(new{  Message = "Category created successfully"});
            return Ok(new { success = true, Message = "Category created successfully" });
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCategoryCommand command)
        {
            await _mediator.Send(command);
            return Ok(new { success = true, Message = "Category updated successfully"  });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _mediator.Send(new DeleteCategoryCommand(id));
            return Ok(new { success= true , Message = "Category Deleted successfully"   });
        }

    }
}
