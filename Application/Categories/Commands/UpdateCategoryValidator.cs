using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Categories.Commands
{
      public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryCommand>
        {
            public UpdateCategoryValidator()
            {
                RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");

            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
                    
            }
        }
  
}
