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

    public record UpdateImageCommand(string Id, string caption) : IRequest<Unit>;

    public class UpdateImageHandler : IRequestHandler<UpdateImageCommand, Unit>
    {
        private readonly IRepository<Image> _repo;

        public UpdateImageHandler(IRepository<Image> repo)
        {
            _repo = repo;
        }

        public async Task<Unit> Handle(UpdateImageCommand request, CancellationToken cancellationToken)
        {
            var Image = await _repo.GetByIdAsync(request.Id);

            if (Image == null)
                throw new Exception("Image not found");

            Image.ImgCaption = request.caption;

            await _repo.UpdateAsync(Image);

            return Unit.Value;
        }
    }
}
