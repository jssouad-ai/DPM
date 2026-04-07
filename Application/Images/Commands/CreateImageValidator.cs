using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Images.Commands
{
    public class CreateImageValidator: AbstractValidator<CreateImageCommand>
    {
        public CreateImageValidator() 
        {
            RuleFor(x => x.Url).NotEmpty().WithMessage("URL is required");
            RuleFor(x => x.Caption).NotEmpty().WithMessage("Caption is required");
        }
       
    }
}
