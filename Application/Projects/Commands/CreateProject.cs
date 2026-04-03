using Domain;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Projects.Commands
{
    public record CreateProjectCommand(string Name, string Description, string CategoryId, IEnumerable<string > ImageUrls) : IRequest<Unit>;

    public class CreateProjectHandler : IRequestHandler<CreateProjectCommand, Unit>
    {
        private readonly IRepository<Project> _repo;

        public CreateProjectHandler(IRepository<Project> repo)
        {
            _repo = repo;
        }

        public async Task<Unit> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var Project = new Project
            {
                ProjectName = request.Name,
                ProjectDescription= request.Description, 
                CategoryId= request.CategoryId,
               
            };
            // Project.AddImage("./API/Images/ResidenceLesOliviers.jpeg");
            foreach (var url in request.ImageUrls)
            {
                Project.AddImage(url, "ImageCaption");
            }
                

            await _repo.AddAsync(Project);

            return Unit.Value;
        }
    }
}
