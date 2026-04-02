using Domain.Interfaces;
using MediatR;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Categories.Commands
{
    public record UpdateCategoryCommand(string Id, string Name) : IRequest<Unit>;

    public class UpdateCategoryHandler: IRequestHandler<UpdateCategoryCommand, Unit>
    {
        private readonly ICategoryRepository _repo;

        public UpdateCategoryHandler(ICategoryRepository repo)
        {
            _repo = repo;
        }

        public async Task<Unit> Handle(
            UpdateCategoryCommand request,
            CancellationToken cancellationToken)
        {
            var category = await _repo.GetByIdAsync(request.Id);

            if (category == null)
                throw new Exception("Category not found");

            category.CategoryName = request.Name;

            await _repo.UpdateAsync(category);

            return Unit.Value;
        }
    }
}
