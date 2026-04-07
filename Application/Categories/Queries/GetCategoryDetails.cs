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
          public record GetCategoryDetailQuery(string Id) : IRequest<CategoryDTO?>;

        public class GetCategoryDetailHandler: IRequestHandler<GetCategoryDetailQuery, CategoryDTO?>
        {
            private readonly ICategoryRepository _repo;
            private readonly IMapper _mapper;

        public GetCategoryDetailHandler(ICategoryRepository repo, IMapper mapper)
            {
                _repo = repo;
                _mapper = mapper;
            }

            public async Task<CategoryDTO?> Handle(GetCategoryDetailQuery request, CancellationToken cancellationToken)
            {
              var   category= await _repo.GetByIdAsync(request.Id);
            return _mapper.Map<CategoryDTO>(category);
        }


        }
    
}
