using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Categories.Commands
{
    public record DeleteCategoryCommand(string Id) : IRequest<Unit>;

    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, Unit>
    {
        private readonly ICategoryRepository _repo;

        public DeleteCategoryHandler(ICategoryRepository repo)
        {
            _repo = repo;
        }

        public async Task<Unit> Handle( DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _repo.GetByIdAsync(request.Id);

            if (category == null)
                throw new Exception("Category not found");

            await _repo.DeleteAsync(category);

            return Unit.Value;
        }
    }
}
