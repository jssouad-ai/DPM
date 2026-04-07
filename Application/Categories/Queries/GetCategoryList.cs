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
    public record GetCategoryListQuery() : IRequest<List<CategoryDTO>>;

    public class GetCategoryListHandler : IRequestHandler<GetCategoryListQuery, List<CategoryDTO>>
    {
        private readonly ICategoryRepository _repo;
        private readonly IMapper _mapper;

        public GetCategoryListHandler(ICategoryRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<CategoryDTO>> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
        {
            var categories = await _repo.GetAllAsync();
           return _mapper.Map<List<CategoryDTO>>(categories);
          

        }
    }
}
