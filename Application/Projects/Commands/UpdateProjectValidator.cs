using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Projects.Commands
{
    public class UpdateProjectValidator: AbstractValidator<UpdateProjectCommand>
    {
        public UpdateProjectValidator() 
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        }
    }
}
