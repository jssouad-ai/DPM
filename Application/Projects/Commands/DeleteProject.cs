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
   
        public record DeleteProjectCommand(string Id) : IRequest<Unit>;

        public class DeleteProjectHandler : IRequestHandler<DeleteProjectCommand, Unit>
        {
            private readonly IRepository<Project> _repo;

            public DeleteProjectHandler(IRepository< Project> repo)
            {
                _repo = repo;
            }

            public async Task<Unit> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
            {
                var Project = await _repo.GetByIdAsync(request.Id);

                if (Project == null)
                    throw new Exception("Project not found");

                await _repo.DeleteAsync(Project);

                return Unit.Value;
            }
        }
    
}
