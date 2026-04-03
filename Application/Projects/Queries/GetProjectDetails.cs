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
    
        public record GetProjectDetailQuery(string Id) : IRequest<Project?>;

        public class GetProjectDetailHandler : IRequestHandler<GetProjectDetailQuery, Project?>
        {
            private readonly IRepository<Project> _repo;

            public GetProjectDetailHandler(IRepository<Project> repo)
            {
                _repo = repo;
            }

            public async Task<Project?> Handle(GetProjectDetailQuery request, CancellationToken cancellationToken)
            {
                return await _repo.GetByIdAsync(request.Id);
            }


        }
    
}
