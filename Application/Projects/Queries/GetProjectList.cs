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
   
        public record GetProjectListQuery() : IRequest<List<Project>>;

        public class GetProjectListHandler : IRequestHandler<GetProjectListQuery, List<Project>>
        {
            private readonly IRepository<Project> _repo;

            public GetProjectListHandler(IRepository<Project> repo)
            {
                _repo = repo;
            }

            public async Task<List<Project>> Handle(GetProjectListQuery request, CancellationToken cancellationToken)
            {
                return await _repo.GetAllAsync();
            }
        }
    
}
