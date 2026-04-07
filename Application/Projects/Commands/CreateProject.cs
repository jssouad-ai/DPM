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
    public record CreateProjectCommand(string Name, string Description, string CategoryId, IEnumerable<string > ImageIds) : IRequest<ProjectDTO>;

    public class CreateProjectHandler : IRequestHandler<CreateProjectCommand, ProjectDTO>
    {
        private readonly IRepository<Project> _repo;
        private readonly IRepository<Image> _imageRepo;
        private readonly IMapper _mapper;

        public CreateProjectHandler(IRepository<Project> repo, IRepository<Image> imageRepo, IMapper mapper)
        {
            _repo = repo;
            _imageRepo = imageRepo;
            _mapper = mapper;
        }

        public async Task<ProjectDTO> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            // if (string.IsNullOrWhiteSpace(request.Name)) throw new ArgumentException("ProjectName cannot be empty");

            var project = _mapper.Map<Project>(request);

            var images = await _imageRepo.GetByIdsAsync(request.ImageIds);
            project.AddImages(images);


            var exists = await _repo.ExistsByNameAsync(request.Name);

            if (exists) throw new Exception("An Project with this Name already exists");

            await _repo.AddAsync(project);

            return _mapper.Map<ProjectDTO>(project);






        }
    }
}
