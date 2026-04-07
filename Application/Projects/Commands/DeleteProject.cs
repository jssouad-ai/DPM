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

    public record DeleteProjectCommand(string Id) : IRequest<ProjectDTO>;

    public class DeleteProjectHandler : IRequestHandler<DeleteProjectCommand, ProjectDTO>
    {
        private readonly IRepository<Project> _repo;
        private readonly IMapper _mapper;

        public DeleteProjectHandler(IRepository<Project> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ProjectDTO> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _repo.GetByIdAsync(request.Id);

            if (project == null)
                throw new Exception("Project not found");

            await _repo.DeleteAsync(project);

            return _mapper.Map<ProjectDTO>(project);
        }
    }

}
