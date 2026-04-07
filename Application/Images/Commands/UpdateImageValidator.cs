using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Images.Commands
{
    public class UpdateImageValidator: AbstractValidator<UpdateImageCommand>
    { 
        public UpdateImageValidator() 
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
            RuleFor(x => x.caption).NotEmpty().WithMessage("Caption is required");
        }
    }
}
