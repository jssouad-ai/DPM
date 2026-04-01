using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController(AppDBContext context) : BaseAPIController
    {

        [HttpGet]
        public async Task<ActionResult<List<Project>>> GetProjects()
        {
            return await context.Projects.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetprojectDetail(string id)
        {
            var project = await context.Projects.FindAsync(id);

            if (project == null) return NotFound();

            return project;
        }
    }
}
