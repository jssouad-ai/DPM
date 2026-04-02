using Domain;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Categories.Commands
{
    public record CreateCategoryCommand(string Name) : IRequest<Unit>;
  
    public class CreateCategoryHandler: IRequestHandler<CreateCategoryCommand, Unit>
    {
        private readonly ICategoryRepository _repo;

        public CreateCategoryHandler(ICategoryRepository repo)
        {
            _repo = repo;
        }

        public async Task<Unit> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Category
            {
                CategoryName = request.Name
            };

            await _repo.AddAsync(category);

            return Unit.Value;
        }
    }
}
