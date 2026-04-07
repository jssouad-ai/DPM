using Application.Categories.Queries;
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

namespace Application.Images.Queries
{
    public record GetImageDetailQuery(string Id) : IRequest<ImageDTO?>;
    public class GetImageDetailsHandler : IRequestHandler<GetImageDetailQuery, ImageDTO?>
    {
        private readonly IRepository<Image> _repo;
        private readonly IMapper _mapper;

        public GetImageDetailsHandler(IRepository<Image> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ImageDTO?> Handle(GetImageDetailQuery request, CancellationToken cancellationToken)
        {
            var image = await _repo.GetByIdAsync(request.Id);
            return _mapper.Map<ImageDTO>(image);
        }
    
    }
}
