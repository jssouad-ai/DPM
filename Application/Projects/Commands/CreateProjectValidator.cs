using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Projects.Commands
{
    public class CreateProjectValidator: AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectValidator() 
        
       {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("CategoryId is required");

        }
    }
}
