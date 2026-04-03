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
    public record DeleteImageCommand(string Id) : IRequest<Unit>;

    public class DeleteImageHandler : IRequestHandler<DeleteImageCommand, Unit>
    {
        private readonly IRepository<Image> _repo;

        public DeleteImageHandler(IRepository<Image> repo)
        {
            _repo = repo;
        }

        public async Task<Unit> Handle(DeleteImageCommand request, CancellationToken cancellationToken)
        {
            var Image = await _repo.GetByIdAsync(request.Id);

            if (Image == null)
                throw new Exception("Image not found");

            await _repo.DeleteAsync(Image);

            return Unit.Value;
        }
    }
}
