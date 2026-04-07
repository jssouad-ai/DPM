using Application.DTOs;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Categories.Commands
{
    public record DeleteCategoryCommand(string Id) : IRequest<CategoryDTO>;

    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, CategoryDTO>
    {
        private readonly ICategoryRepository _repo;
        private readonly IMapper _mapper;

        public DeleteCategoryHandler(ICategoryRepository repo , IMapper mapper )
        {
            _repo = repo;
            _mapper = mapper;   
        }

        public async Task<CategoryDTO> Handle( DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _repo.GetByIdAsync(request.Id);

            if (category == null)
                throw new Exception("Category not found");

            await _repo.DeleteAsync(category);

            return _mapper.Map<CategoryDTO>(category);
        }
    }
}
