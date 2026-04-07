using Application.DTOs;
using Application.Projects.Commands;
using Application.Projects.Queries;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController: ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetProjectListQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetail(string id)
        {
            var result = await _mediator.Send(new GetProjectDetailQuery(id));

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProjectCommand command)
        {
            await _mediator.Send(command);
            return Ok(new
            {
                Message = "Project created successfully"
            });
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateProjectCommand command)
        {
            await _mediator.Send(command);
            return Ok(new
            {
                Message = "Project updated successfully"
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _mediator.Send(new DeleteProjectCommand(id));
            return Ok(new
            {
                Message = "Project Deleted successfully"
            });
        }

    }


}

