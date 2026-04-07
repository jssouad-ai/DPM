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

namespace Application.Projects.Queries
{
    
        public record GetProjectDetailQuery(string Id) : IRequest<ProjectDTO?>;

        public class GetProjectDetailHandler : IRequestHandler<GetProjectDetailQuery, ProjectDTO?>
        {
            private readonly IRepository<Project> _repo;
        private readonly IMapper _mapper;

            public GetProjectDetailHandler(IRepository<Project> repo, IMapper mapper)
            {
                _repo = repo;
            _mapper = mapper;
            }

            public async Task<ProjectDTO?> Handle(GetProjectDetailQuery request, CancellationToken cancellationToken)
            {
            return _mapper.Map<ProjectDTO>(await _repo.GetByIdAsync(request.Id));

            }


        }
    
}
