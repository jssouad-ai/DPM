using Application.DTOs;
using AutoMapper;
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
    public record CreateCategoryCommand(string Name) : IRequest<CategoryDTO>;
      
    public class CreateCategoryHandler: IRequestHandler<CreateCategoryCommand, CategoryDTO>
    {
        private readonly ICategoryRepository _repo;
        private readonly IMapper _mapper;


        public CreateCategoryHandler(ICategoryRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }


        public async Task<CategoryDTO> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
           /* if (string.IsNullOrWhiteSpace(request.Name))
                throw new ArgumentException("CategoryName cannot be empty");*/

            var category = _mapper.Map<Category>(request);
             
            var exists = await _repo.ExistsAsync(request.Name);

            if (exists) throw new Exception("An Category with this Name already exists");

            await _repo.AddAsync(category);
           
            return _mapper.Map<CategoryDTO>(category);

        }
        
    }
}
