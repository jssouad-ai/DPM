using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Images.Commands
{
    public class DeleteImageValidator : AbstractValidator<DeleteImageCommand>
    {
        public DeleteImageValidator() 
        
        { RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required"); 
        }
        
    }
}
