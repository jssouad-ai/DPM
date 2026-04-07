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
   
        public record GetProjectListQuery() : IRequest<List<ProjectDTO>>;

        public class GetProjectListHandler : IRequestHandler<GetProjectListQuery, List<ProjectDTO>>
        {
            private readonly IRepository<Project> _repo;
            private readonly IMapper _mapper;

            public GetProjectListHandler(IRepository<Project> repo,IMapper mapper)
            {
                _repo = repo;
            _mapper = mapper;
            }

            public async Task<List<ProjectDTO>> Handle(GetProjectListQuery request, CancellationToken cancellationToken)
            {
                var projects=  await _repo.GetAllAsync();
                return _mapper.Map<List<ProjectDTO>>(projects);
            }
        }
    
}
