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
using static System.Net.Mime.MediaTypeNames;

namespace Application.Categories.Commands
{
    public record UpdateCategoryCommand(string Id, string Name) : IRequest<CategoryDTO>;

    public class UpdateCategoryHandler: IRequestHandler<UpdateCategoryCommand, CategoryDTO>
    {
        private readonly ICategoryRepository _repo;
        private readonly IMapper _mapper;

        public UpdateCategoryHandler(ICategoryRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<CategoryDTO> Handle( UpdateCategoryCommand request,CancellationToken cancellationToken)
        {
            var category = await _repo.GetByIdAsync(request.Id);
            if (category == null) throw new Exception("Category not found");

            //if (string.IsNullOrWhiteSpace(request.Name))  throw new ArgumentException("CategoryName cannot be empty");

            _mapper.Map(request, category);

            await _repo.UpdateAsync(category);

            return _mapper.Map<CategoryDTO>(category);
        }
    }
}
