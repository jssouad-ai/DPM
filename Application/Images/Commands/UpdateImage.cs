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

namespace Application.Images.Commands
{

    public record UpdateImageCommand(string Id, string caption) : IRequest<ImageDTO>;

    public class UpdateImageHandler : IRequestHandler<UpdateImageCommand, ImageDTO>
    {
        private readonly IRepository<Image> _repo;
        private readonly IMapper  _mapper;

        public UpdateImageHandler(IRepository<Image> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ImageDTO> Handle(UpdateImageCommand request, CancellationToken cancellationToken)
        {
            var image = await _repo.GetByIdAsync(request.Id);

            if (image == null) throw new Exception("Image not found");
           // if (string.IsNullOrWhiteSpace(request.caption)) throw new ArgumentException("Caption cannot be empty");

            _mapper.Map(request, image);

            await _repo.UpdateAsync(image);

            return  _mapper.Map<ImageDTO>(image);
        }
    }
}
