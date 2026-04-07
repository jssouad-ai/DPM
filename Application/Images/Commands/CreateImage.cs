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
    public record CreateImageCommand(string Url, string Caption) : IRequest<ImageDTO?>;

    public class CreateImageHandler : IRequestHandler<CreateImageCommand, ImageDTO?>
    {
        private readonly IRepository<Image> _repo;
        private readonly IMapper _mapper;
        public CreateImageHandler(IRepository<Image> repo , IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
           
        }

        public async Task<ImageDTO?> Handle(CreateImageCommand request, CancellationToken cancellationToken)
        {

           /* if (string.IsNullOrWhiteSpace(request.Url)) throw new ArgumentException("Url cannot be empty");

            if (string.IsNullOrWhiteSpace(request.Caption)) throw new ArgumentException("Caption cannot be empty");*/

            var image = _mapper.Map<Image>(request);
          
            var exists = await _repo.ExistsByUrlAsync(request.Url);

            if (exists) throw new Exception("An image with this URL already exists");

           await _repo.AddAsync(image);

            return _mapper.Map<ImageDTO>(image);
        }
    }
}
