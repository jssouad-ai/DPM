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

    public record UpdateProjectCommand(string Id, string Name, string Description, string CategoryId, List<Image> Images) : IRequest<Unit>;

    public class UpdateProjectHandler : IRequestHandler<UpdateProjectCommand, Unit>
    {
        private readonly IRepository<Project> _repo;

        public UpdateProjectHandler(IRepository<Project> repo)
        {
            _repo = repo;
        }

        public async Task<Unit> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var Project = await _repo.GetByIdAsync(request.Id);

            if (Project == null)
                throw new Exception("Project not found");

            Project.ProjectName = request.Name;
            Project.ProjectDescription = request.Description;
            Project.CategoryId = request.CategoryId;
            foreach (var img in request.Images)
            {
                if (!Project.Images.Any(i => i.Id == img.Id))
                {
                    Project.AddImage(img.ImgURL, img.ImgCaption);
                    //Project.AddImage(img);
                }
            }
                await _repo.UpdateAsync(Project);

            return Unit.Value;


        }
    }

}
