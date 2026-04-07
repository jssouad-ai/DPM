using Application.Images.Queries;
using Application.DTOs;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Images.Queries
{
    public record GetImageListQuery() : IRequest<List<ImageDTO>>;
    public class GetImageListHandler : IRequestHandler<GetImageListQuery, List<ImageDTO>>
    {
        private readonly IRepository<Image> _repo;
        private readonly IMapper _mapper;

        public GetImageListHandler(IRepository<Image> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<ImageDTO>> Handle(GetImageListQuery request, CancellationToken cancellationToken)
        {
            var images = await _repo.GetAllAsync();
            return _mapper.Map<List<ImageDTO>>(images);


        }
    }
}
