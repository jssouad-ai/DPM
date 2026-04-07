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
    public record DeleteImageCommand(string Id) : IRequest<ImageDTO>;

    public class DeleteImageHandler : IRequestHandler<DeleteImageCommand, ImageDTO>
    {
        private readonly IRepository<Image> _repo;
        private readonly    IMapper _mapper;

        public DeleteImageHandler(IRepository<Image> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ImageDTO> Handle(DeleteImageCommand request, CancellationToken cancellationToken)
        {
            var image = await _repo.GetByIdAsync(request.Id);
            if (image == null) throw new Exception("Image not found");
            
            await _repo.DeleteAsync(image);

            return _mapper.Map<ImageDTO>(image);
        }
    }
}
