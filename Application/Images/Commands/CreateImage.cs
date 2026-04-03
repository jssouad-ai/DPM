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
    public record CreateImageCommand(string Url, string Caption) : IRequest<Unit>;

    public class CreateImageHandler : IRequestHandler<CreateImageCommand, Unit>
    {
        private readonly IRepository<Image> _repo;

       public CreateImageHandler(IRepository<Image> repo)
        {
            _repo = repo;
        }

        public async Task<Unit> Handle(CreateImageCommand request, CancellationToken cancellationToken)
        {
            var exists = await _repo.ExistsByUrlAsync(request.Url);

            if (exists)
                throw new Exception("An image with this URL already exists");

            var Image = new Image(request.Url, request.Caption);

            await _repo.AddAsync(Image);

            return Unit.Value;
        }
    }
}
