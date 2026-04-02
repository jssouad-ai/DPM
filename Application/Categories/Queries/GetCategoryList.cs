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
    /* public class GetCategoryList
     {
         public class Query : IRequest<List<Category>> { }

         public class Handler(AppDBContext context) : IRequestHandler<Query, List<Category>>
         {
             public async Task<List<Category>> Handle(Query request, CancellationToken cancellationToken)
             {
                 return await context.Categories.ToListAsync(cancellationToken);
             }
         }
     }*/
    public record GetCategoryListQuery() : IRequest<List<Category>>;

    public class GetCategoryListHandler
        : IRequestHandler<GetCategoryListQuery, List<Category>>
    {
        private readonly ICategoryRepository _repo;

        public GetCategoryListHandler(ICategoryRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Category>> Handle(
            GetCategoryListQuery request,
            CancellationToken cancellationToken)
        {
            return await _repo.GetAllAsync();
        }
    }
}
