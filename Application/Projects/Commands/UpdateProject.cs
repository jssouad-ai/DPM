using Application.DTOs;
using AutoMapper;
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

    public record UpdateProjectCommand(string Id, string Name, string Description, string CategoryId, IEnumerable<string> ImageIds) : IRequest<ProjectDTO>;

    public class UpdateProjectHandler : IRequestHandler<UpdateProjectCommand, ProjectDTO>
    {
        private readonly IRepository<Project> _repo;
        private readonly IRepository<Image> _imageRepo;
        private readonly IMapper _mapper;

        public UpdateProjectHandler(IRepository<Project> repo,IRepository<Image> imageRepo, IMapper mapper)
        {
            _repo = repo;
            _imageRepo = imageRepo;
            _mapper = mapper;

        }

        public async Task<ProjectDTO> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _repo.GetByIdAsync(request.Id);

            if (project == null)
                throw new Exception("Project not found");

            /*Project.ProjectName = request.Name;
            Project.ProjectDescription = request.Description;
            Project.CategoryId = request.CategoryId;*/

           _mapper.Map(request, project); 

            var images = await _imageRepo.GetByIdsAsync(request.ImageIds);
            project.AddImages(images);              
              
            await _repo.UpdateAsync(project);

            return _mapper.Map<ProjectDTO>(project);


        }
    }

}
