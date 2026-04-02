using Domain;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Categories.Queries
{
    public class GetCategoryDetails
    {
        public record GetCategoryDetailQuery(string Id) : IRequest<Category?>;

        public class GetCategoryDetailHandler
            : IRequestHandler<GetCategoryDetailQuery, Category?>
        {
            private readonly ICategoryRepository _repo;

            public GetCategoryDetailHandler(ICategoryRepository repo)
            {
                _repo = repo;
            }

            public async Task<Category?> Handle(
                GetCategoryDetailQuery request,
                CancellationToken cancellationToken)
            {
                return await _repo.GetByIdAsync(request.Id);
            }


        }
    }
}
