using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Projects.Commands
{
    public class DeleteProjectValidator: AbstractValidator<DeleteProjectCommand>
    {
        public DeleteProjectValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("id is required");
        }
    }
}
